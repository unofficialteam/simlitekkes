using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;
using System.Net;
using System.Net.Mail;

namespace simlitekkes.UserControls.Admin
{
    public partial class personalPPSDM : System.Web.UI.UserControl
    {
        Models.Admin.personalPPSDM modelPersonal = new Models.Admin.personalPPSDM();
        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiModal modal = new uiModal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                mvMain.SetActiveView(vDaftarPersonal);
                isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
            }
        }

        #region Daftar Personal
        protected void isiLvPersonal(int limit, int offset)
        {
            if (!modelPersonal.getJmlDataPersonal(tbPencarian.Text, 0, offset))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);

            paging.currentPage = offset;
            paging.setPaging(limit, modelPersonal.numOfRecords);

            modelPersonal.currentPage = offset;
            modelPersonal.rowsPerPage = limit;

            DataTable dt = new DataTable();
            if (modelPersonal.listDataPersonal(ref dt, tbPencarian.Text,
                int.Parse(ddlJmlBaris.SelectedValue), offset))
            {
                lvPersonal.DataSource = dt;
                lvPersonal.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
            }

            if (modelPersonal.numOfRecords < 1)
            {
                paging.setPaging(limit, 1);
            }
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void lbCariData_Click(object sender, EventArgs e)
        {
            isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void lbTambahData_Click(object sender, EventArgs e)
        {
            ViewState["idPersonal"] = "00000000-0000-0000-0000-000000000000";
            isiddlPropinsi(ref ddlPropinsi);
            mvMain.SetActiveView(vInsupPersonal);
            lblSimpan.Text = "Simpan";
        }

        protected void lvPersonal_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;

                Label lblNo = (Label)e.Item.FindControl("lblNo");
                lblNo.Text = (e.Item.DataItemIndex + 1 + int.Parse(ddlJmlBaris.SelectedValue) * (paging.currentPage)).ToString();
            }
        }

        protected void lvPersonal_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            mvMain.SetActiveView(vInsupPersonal);
            tbNama.Text = lvPersonal.DataKeys[e.ItemIndex]["nama"].ToString();
            tbGelarDepan.Text = lvPersonal.DataKeys[e.ItemIndex]["gelar_akademik_depan"].ToString();
            tbGelarBelakang.Text = lvPersonal.DataKeys[e.ItemIndex]["gelar_akademik_belakang"].ToString();
            ddlJenisKelamin.SelectedValue = lvPersonal.DataKeys[e.ItemIndex]["kd_jenis_kelamin"].ToString();
            tbNoKTP.Text = lvPersonal.DataKeys[e.ItemIndex]["nomor_ktp"].ToString();
            tbTempatLahir.Text = lvPersonal.DataKeys[e.ItemIndex]["tempat_lahir"].ToString();
            tbTglLahir.Text = lvPersonal.DataKeys[e.ItemIndex]["tanggal_lahir"].ToString() != "" ?
                DateTime.Parse(lvPersonal.DataKeys[e.ItemIndex]["tanggal_lahir"].ToString()).ToString("yyyy-MM-dd") : "";
            tbAlamat.Text = lvPersonal.DataKeys[e.ItemIndex]["alamat"].ToString();
            isiddlPropinsi(ref ddlPropinsi);
            ddlPropinsi.SelectedValue = lvPersonal.DataKeys[e.ItemIndex]["kd_provinsi"].ToString() == "" ? "99" :
                lvPersonal.DataKeys[e.ItemIndex]["kd_provinsi"].ToString();
            isiddlKabKota(ref ddlKabKota, ddlPropinsi.SelectedValue);

            string kdKota = lvPersonal.DataKeys[e.ItemIndex]["kd_kota"].ToString().Trim();
            ddlKabKota.SelectedValue = kdKota == "" ? "" : kdKota;
                //lvPersonal.DataKeys[e.ItemIndex]["kd_kota"].ToString();
            tbKodePos.Text = lvPersonal.DataKeys[e.ItemIndex]["kd_pos"].ToString();
            tbNoTlp.Text = lvPersonal.DataKeys[e.ItemIndex]["nomor_telepon"].ToString();
            tbNoHP.Text = lvPersonal.DataKeys[e.ItemIndex]["nomor_hp"].ToString();
            tbSurel.Text = lvPersonal.DataKeys[e.ItemIndex]["surel"].ToString();
            tbWebsite.Text = lvPersonal.DataKeys[e.ItemIndex]["website_personal"].ToString();
            ddlStatusEdit.SelectedValue = lvPersonal.DataKeys[e.ItemIndex]["kd_sts_aktif"].ToString();

            ViewState["idPersonal"] = lvPersonal.DataKeys[e.ItemIndex]["id_personal"].ToString();
            lblSimpan.Text = "Update";
        }

        protected void lvPersonal_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int indek = int.Parse(e.CommandArgument.ToString());

            string id_kontak_pic_pengguna_personal = lvPersonal.DataKeys[indek]["id_kontak_pic_pengguna_personal"].ToString();
            string id_institusi = lvPersonal.DataKeys[indek]["id_institusi"].ToString();
            var surel = lvPersonal.DataKeys[indek]["surel"].ToString();
            Guid idPersonal = Guid.Parse(lvPersonal.DataKeys[indek]["id_personal"].ToString());
            ViewState["idPersonal"] = idPersonal;
            int[] jmlKiriman = { Convert.ToInt32(Application["simlitekkes"]) };
            string[] namaSurel = { "pusdikbppsdmkemkes@gmail.com" };
            string stsPengiriman = "0";
            int initIdx = 0;

            switch (e.CommandName)
            {
                case "jmlPeran":
                    mvMain.SetActiveView(vperan);
                    //Guid idPersonal = Guid.Parse(lvPersonal.DataKeys[e.Item.DataItemIndex]["id_personal"].ToString());
                    //ViewState["idPersonal"] = idPersonal;
                    isilvPeran(Guid.Parse(ViewState["idPersonal"].ToString()));
                    break;

                case "unitKegiatan":
                    mvMain.SetActiveView(vUnitKegiatan);
                    isilvUnitKegiatan(Guid.Parse(ViewState["idPersonal"].ToString()));
                    break;

                case "kirimAkun":
                    try
                    {
                        if (surel.Length == 0)
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Surel harus diisi terlebih dahulu");
                            return;
                        }
                        else
                        {
                            string surel_pengirim = "pusdikbppsdmkemkes@gmail.com";
                            DateTime sekarang = DateTime.Now;
                            for (int idx = 1; idx < jmlKiriman.Length; idx++)
                            {
                                if (jmlKiriman[idx] < jmlKiriman[idx - 1])
                                    initIdx = idx;
                            }
                            string alamatEmailSumber = namaSurel[initIdx].ToString();

                            NetworkCredential cred = new NetworkCredential(alamatEmailSumber, "pwd_PusdikBpp5dm");
                            MailMessage msg = new MailMessage();
                            DataTable dt = new DataTable();

                            msg.From = new MailAddress(alamatEmailSumber);

                            modelPersonal.getPenggunaPersonalByPic(ref dt, id_kontak_pic_pengguna_personal);
                            if (dt.Rows[0]["surel"].ToString() != "")
                            {
                                surel = dt.Rows[0]["surel"].ToString();
                                msg.To.Add(surel);
                                msg.Subject = string.Format("Akun SIMLITABKES {0} {1}", dt.Rows[0]["nama"].ToString(),
                                    dt.Rows[0]["nama_institusi"].ToString());

                                DataTable dtPic = new DataTable();
                                modelPersonal.getPenggunaPersonalByPic(ref dtPic, id_kontak_pic_pengguna_personal);
                                if (dtPic.Rows.Count > 0)
                                {
                                    System.Text.StringBuilder st = new System.Text.StringBuilder("");
                                    int i = 1;
                                    foreach (DataRow dr in dtPic.Rows)
                                    {
                                        // isi content surel
                                        //st.Append(i.ToString());
                                        st.Append("Nama User: ");
                                        st.Append(dr["nama_user"].ToString());
                                        st.Append(" | ");
                                        st.Append("Password: ");
                                        st.Append(dr["pswd"].ToString());
                                        st.Append(" \n");
                                        i++;
                                    }
                                    st.Append("\nAlamat surel ini bersifat sementara hanya untuk mengirim nama user dan password anda, mohon tidak dibalas.\nApabila ingin berkomunikasi melalui surel, gunakan alamat ........(alamat email resmi SIMLITABKES).\nTerimakasih.\n\nHormat kami,\nAdministrator SIMLITABKES");
                                    msg.Body = st.ToString();

                                    SmtpClient smtpClient = new SmtpClient();
                                    smtpClient.Host = "smtp.gmail.com";
                                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtpClient.Port = 587;
                                    smtpClient.EnableSsl = true;
                                    smtpClient.UseDefaultCredentials = false;
                                    smtpClient.Credentials = new System.Net.NetworkCredential(alamatEmailSumber, "pwd_PusdikBpp5dm");
                                    try
                                    {
                                        smtpClient.Send(msg);
                                    }
                                    catch(Exception ex)
                                    {
                                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", ex.Message);
                                        return;
                                    }

                                    stsPengiriman = "1";
                                    if (modelPersonal.insertPengirimanUser(Guid.Parse(id_kontak_pic_pengguna_personal),
                                        surel, stsPengiriman, surel_pengirim))
                                    {
                                        isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
                                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "User dan password telah berhasil dikirim");
                                    }
                                    else
                                    {
                                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", modelPersonal.errorMessage);
                                        return;
                                    }
                                }
                                else
                                {
                                    noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Pengiriman user dibatalkan karena akun belum ada");
                                }
                            }
                            else
                            {
                                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Pengiriman user dibatalkan karena surel belum ada");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi",
                            "pengiriman data user dan password gagal... <br />" + ex.InnerException);
                        isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
                    }
                    break;
            }
        }

        protected void lvPersonal_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            ViewState["idPersonal"] = lvPersonal.DataKeys[e.ItemIndex]["id_personal"].ToString();

            lblNamaHapus.Text = lvPersonal.DataKeys[e.ItemIndex]["nama"].ToString();
            modal.ShowModal(this.Page, "modalHapus");
        }

        protected void Paging_PageChanging(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelPersonal.currentPage = paging.currentPage;
            modelPersonal.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            ViewState["currentPage"] = paging.currentPage * int.Parse(ddlJmlBaris.SelectedValue);

            if (modelPersonal.listDataPersonal(ref dt, tbPencarian.Text, 
                int.Parse(ddlJmlBaris.SelectedValue), int.Parse(ViewState["currentPage"].ToString())))
            {
                lvPersonal.DataSource = dt;
                lvPersonal.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
            }
        }

        protected void lbHapusDataPersonal_Click(object sender, EventArgs e)
        {
            if (modelPersonal.delPersonalPPSDM(Guid.Parse(ViewState["idPersonal"].ToString())))
            {
                mvMain.SetActiveView(vDaftarPersonal);
                isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
                clearIsian();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Data berhasil dihapus...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
                return;
            }
        }

        #endregion

        #region Peran

        protected void isilvPeran(Guid idPersonal)
        {
            DataTable dt = new DataTable();
            if (modelPersonal.getPeran(ref dt, idPersonal))
            {
                lvPeran.DataSource = dt;
                lvPeran.DataBind();

                ViewState["peran"] = dt;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
            }
        }

        protected void lvPeran_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var drv = (DataRowView)e.Item.DataItem;
            var cbPeranAktif = e.Item.FindControl("cbPeranAktif") as CheckBox;
            string peranAktif = drv["peran_aktif"].ToString();
            var cbStsDefault = e.Item.FindControl("cbStsDefault") as CheckBox;
            int stsDefault = int.Parse(drv["is_default_"].ToString());

            if (peranAktif == "1")
            {
                cbPeranAktif.Checked = true;
            }

            if (stsDefault == 1)
            {
                cbStsDefault.Checked = true;
            }
        }

        protected void cbPeranAktif_CheckedChanged(object sender, EventArgs e)
        {
            ListViewItem item = (sender as CheckBox).NamingContainer as ListViewItem;
            var cbPeranAktif = item.FindControl("cbPeranAktif") as CheckBox;
            var cbStsDefault = item.FindControl("cbStsDefault") as CheckBox;
            int idPeran = int.Parse(lvPeran.DataKeys[item.DataItemIndex].Values["id_peran"].ToString());

            if (cbPeranAktif.Checked == false)
            {
                cbStsDefault.Checked = false;
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Peran tidak aktif");
            }

            DataTable dt = new DataTable();
            modelPersonal.getPeranDefault(ref dt, Guid.Parse(ViewState["idPersonal"].ToString()));
            int jmlDefault = int.Parse(dt.Rows[0]["jml_default"].ToString());

            if (jmlDefault > 0)
            {
                if (cbPeranAktif.Checked == false)
                {
                    string isDefault = "0";
                    modelPersonal.updatePeranPengguna(idPeran, Guid.Parse(ViewState["idPersonal"].ToString()), isDefault);
                }
            }
        }

        protected void cbStsDefault_CheckedChanged(object sender, EventArgs e)
        {
            ListViewItem item = (sender as CheckBox).NamingContainer as ListViewItem;
            var cbPeranAktif = item.FindControl("cbPeranAktif") as CheckBox;
            var cbStsDefault = item.FindControl("cbStsDefault") as CheckBox;
            int idPeran = int.Parse(lvPeran.DataKeys[item.DataItemIndex].Values["id_peran"].ToString());

            if (cbPeranAktif.Checked == false)
            {
                cbStsDefault.Checked = false;
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Peran tidak aktif");
            }

            if (cbStsDefault.Checked == false)
            {
                string isDefault = "0";
                modelPersonal.updatePeranPengguna(idPeran, Guid.Parse(ViewState["idPersonal"].ToString()), isDefault);
            }

            DataTable dt = new DataTable();
            modelPersonal.getPeranDefault(ref dt, Guid.Parse(ViewState["idPersonal"].ToString()));
            int jmlDefault = int.Parse(dt.Rows[0]["jml_default"].ToString());

            if (jmlDefault > 0)
            {
                cbStsDefault.Checked = false;
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Status default sudah ada di peran lain");
            }
            else
            {
                if (cbStsDefault.Checked == true)
                {
                    string isDefault = "1";
                    modelPersonal.updatePeranPengguna(idPeran, Guid.Parse(ViewState["idPersonal"].ToString()), isDefault);
                }
            }
        }

        protected void lbKembaliPeran_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vDaftarPersonal);
            isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void lbSimpanPeran_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["peran"];
            Guid idPersonal = Guid.Parse(ViewState["idPersonal"].ToString());
            string isDataAktif, isDefault;
            int idPeran;

            foreach (ListViewItem item in lvPeran.Items)
            {
                var cbPeranAktif = item.FindControl("cbPeranAktif") as CheckBox;
                var cbStsDefault = item.FindControl("cbStsDefault") as CheckBox;
                var lblIdPeran = item.FindControl("lblIdPeran") as Label;

                idPeran = int.Parse(lblIdPeran.Text);

                if (cbPeranAktif.Checked == true)
                {
                    isDataAktif = "1";
                }
                else
                {
                    isDataAktif = "0";
                }

                if (cbStsDefault.Checked == true)
                {
                    isDefault = "1";
                }
                else
                {
                    isDefault = "0";
                }

                DataTable dtv = new DataTable();
                modelPersonal.getPeranAktif(ref dtv, idPersonal, idPeran);
                if (dtv.Rows.Count > 0)
                {
                    if (!modelPersonal.insupPeranPengguna(idPersonal, isDataAktif, idPeran, isDefault))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
                        return;
                    }
                }
                else
                {
                    if (cbPeranAktif.Checked == true)
                    {
                        if (!modelPersonal.insupPeranPengguna(idPersonal, isDataAktif, idPeran, isDefault))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
                            return;
                        }
                    }
                }
                mvMain.SetActiveView(vDaftarPersonal);
                isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan berhasil.. ");
            }
        }

        #endregion

        #region Unit Kegiatan Pengguna

        protected void isilvUnitKegiatan(Guid idPersonal)
        {
            DataTable dt = new DataTable();
            if (modelPersonal.getUnitKegiatan(ref dt, idPersonal))
            {
                lvUnitKegiatan.DataSource = dt;
                lvUnitKegiatan.DataBind();

                ViewState["nama_unit"] = dt;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
            }
        }

        protected void lvUnitKegiatan_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var drv = (DataRowView)e.Item.DataItem;
            var cbStsUnitPengguna = e.Item.FindControl("cbStsUnitPengguna") as CheckBox;
            string unitKegiatanAktif = drv["unit_kegiatan_aktif"].ToString();

            if (unitKegiatanAktif == "1")
            {
                cbStsUnitPengguna.Checked = true;
            }
        }

        //protected void cbStsUnitPengguna_CheckedChanged(object sender, EventArgs e)
        //{
        //    ListViewItem item = (sender as CheckBox).NamingContainer as ListViewItem;
        //    var cbStsUnitPengguna = item.FindControl("cbStsUnitPengguna") as CheckBox;
        //    Guid id_unit_kegiatan = Guid.Parse(lvPeran.DataKeys[item.DataItemIndex].Values["id_unit_kegiatan"].ToString());

            //if (cbStsUnitPengguna.Checked == false)
            //{
            //    cbStsDefault.Checked = false;
            //    noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Peran tidak aktif");
            //}

            //DataTable dt = new DataTable();
            //modelPersonal.getUnitKegiatanAktif(ref dt, Guid.Parse(ViewState["idPersonal"].ToString()));
            //int jmlDefault = int.Parse(dt.Rows[0]["jml_default"].ToString());

            //if (jmlDefault > 0)
            //{
            //    if (cbStsUnitPengguna.Checked == false)
            //    {
            //        modelPersonal.updatePeranPengguna(idPeran, Guid.Parse(ViewState["idPersonal"].ToString()), isDefault);
            //    }
            //}
        //}

        protected void lbKembaliUnitKegiatan_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vDaftarPersonal);
            isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void lbSimpanUnitKegiatan_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["nama_unit"];
            Guid idPersonal = Guid.Parse(ViewState["idPersonal"].ToString());
            string unit_kegiatan_aktif;
            Guid idUnitKegiatan;

            foreach (ListViewItem item in lvUnitKegiatan.Items)
            {
                var cbStsUnitPengguna = item.FindControl("cbStsUnitPengguna") as CheckBox;
                var lblIdUnitKegiatan = item.FindControl("lblIdUnitKegiatan") as Label;
                idUnitKegiatan = Guid.Parse(lblIdUnitKegiatan.Text.ToString());

                if (cbStsUnitPengguna.Checked == true)
                {
                    unit_kegiatan_aktif = "1";
                }
                else
                {
                    unit_kegiatan_aktif = "0";
                }

                DataTable dtv = new DataTable();
                modelPersonal.getUnitKegiatanAktif(ref dtv, idPersonal, idUnitKegiatan);
                if (dtv.Rows.Count > 0)
                {
                    if (!modelPersonal.insupUnitKegiatanPengguna(idPersonal, idUnitKegiatan, unit_kegiatan_aktif))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
                        return;
                    }
                }
                else
                {
                    if (cbStsUnitPengguna.Checked == true)
                    {
                        if (!modelPersonal.insupUnitKegiatanPengguna(idPersonal, idUnitKegiatan, unit_kegiatan_aktif))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
                            return;
                        }
                    }
                }
                mvMain.SetActiveView(vDaftarPersonal);
                isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan berhasil.. ");
            }
        }

        #endregion

        #region Insup Data Personal

        protected void ddlPropinsi_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlKabKota(ref ddlKabKota, ddlPropinsi.SelectedValue);
        }

        protected void lbBatalPersonal_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vDaftarPersonal);
            isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
            clearIsian();
        }

        protected void lbSimpanPersonal_Click(object sender, EventArgs e)
        {
            //Cek Isian
            List<string> isianKosong = new List<string>();
            if (tbNama.Text.Trim().Length == 0) isianKosong.Add("Nama");
            if (tbNoKTP.Text.Trim().Length == 0) isianKosong.Add("Nomor KTP");
            if (ddlKabKota.SelectedValue == "") isianKosong.Add("Kota belum dipilih");
            if (tbKodePos.Text.Trim().Length != 5) isianKosong.Add("Kode pos harus 5 karakter");
            if (tbSurel.Text.Trim().Length == 0) isianKosong.Add("Email");

            if (isianKosong.Count > 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi",
                   "Maaf, Data berikut harus disi :<br />" + string.Join(", ", isianKosong.ToArray()));
                return;
            }

            Guid id_personal = Guid.Parse(ViewState["idPersonal"].ToString());
            if (modelPersonal.insupPersonalPpsdm(id_personal, ddlStatusEdit.SelectedValue, tbNama.Text,
                tbGelarDepan.Text, tbGelarBelakang.Text, tbNoKTP.Text, ddlJenisKelamin.SelectedValue, tbTempatLahir.Text,
                tbTglLahir.Text, tbAlamat.Text, ddlKabKota.SelectedValue, tbKodePos.Text, tbNoTlp.Text, tbNoHP.Text,
                tbSurel.Text, Guid.Parse("28ed5db7-12f9-4c80-aeb3-23f35541eef1"), tbWebsite.Text))
            {
                clearIsian();
                mvMain.SetActiveView(vDaftarPersonal);
                isiLvPersonal(int.Parse(ddlJmlBaris.SelectedValue), 0);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Data berhasil disimpan...");
            }

            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPersonal.errorMessage);
                return;
            }

        }

        private void isiddlPropinsi(ref DropDownList ddlPropinsi)
        {
            DataTable dt = new DataTable();
            if (modelPersonal.listProvinsi(ref dt, 70, 0))
            {
                ddlPropinsi.Items.Add(new ListItem { Text = "-- Pilih --", Value = "99", Selected = true });
                ddlPropinsi.DataSource = dt;
                ddlPropinsi.AppendDataBoundItems = true;
                ddlPropinsi.DataTextField = "nama_provinsi";
                ddlPropinsi.DataValueField = "kd_provinsi";
                ddlPropinsi.DataBind();
            }
        }

        private void isiddlKabKota(ref DropDownList ddlKabKota, string ddlPropinsi)
        {
            DataTable dt = new DataTable();
            if (modelPersonal.listKota(ref dt, ddlPropinsi.Trim()))
            {
                ddlKabKota.DataSource = dt;
                ddlKabKota.AppendDataBoundItems = true;
                ddlKabKota.DataTextField = "nama_kota";
                ddlKabKota.DataValueField = "kd_kota";
                ddlKabKota.DataBind();
            }
        }

        private void clearIsian()
        {
            tbGelarDepan.Text = string.Empty;
            tbNama.Text = string.Empty;
            tbGelarBelakang.Text = string.Empty;
            tbNoKTP.Text = string.Empty;
            tbTempatLahir.Text = string.Empty;
            tbTglLahir.Text = string.Empty;
            tbAlamat.Text = string.Empty;
            ddlPropinsi.Items.Clear();
            ddlKabKota.Items.Clear();
            tbKodePos.Text = string.Empty;
            tbNoTlp.Text = string.Empty;
            tbNoHP.Text = string.Empty;
            tbSurel.Text = string.Empty;
            tbWebsite.Text = string.Empty;
            ViewState["idPersonal"] = string.Empty;
        }

        #endregion

    }
}