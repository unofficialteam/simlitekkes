using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.Models;
using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System.IO;
using simlitekkes.Core;


namespace simlitekkes.UserControls.Pengusul.pendaftaranReviewer
{
    public partial class penyajiSeminar : System.Web.UI.UserControl
    {
        login objLogin;
        Models.Pengusul.berandaPengusul objPengusul = new Models.Pengusul.berandaPengusul();
        Models.Pengusul.persyaratanUmumPendaftaranReviewer objPemakalah = new Models.Pengusul.persyaratanUmumPendaftaranReviewer();
        uiNotify noty = new uiNotify();
        uiDropdownList objDdl = new uiDropdownList();
        uiModal objModal = new uiModal();
        //Core.kontrolUnggah ktUnggah = new Core.kontrolUnggah();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvProsiding);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnggahDokumen);

            objLogin = (login)Session["objLogin"];

            isiDataProsiding();
            isiDataPemakalah();
        }

        public void init(string thnPendaftaran)
        {
            ViewState["thn_pendaftaran"] = thnPendaftaran;
        }

        public void isiDataProsiding()
        {
            DataTable dt = new DataTable();
            if (objPengusul.getArtikelProsiding(ref dt, Guid.Parse(objLogin.idPersonal)))
            {
                gvProsiding.DataSource = dt;
                gvProsiding.DataBind();
            }
            ViewState["jml_prosiding"] = dt.Rows.Count;
        }

        public int getJmlProsiding()
        {
            return int.Parse(ViewState["jml_prosiding"].ToString());
        }

        public void isiDataPemakalah()
        {
            DataTable dt = new DataTable();
            if (objPemakalah.getPemakalah(ref dt, objLogin.idPersonal))
            {
                gvPemakalah.DataSource = dt;
                gvPemakalah.DataBind();
            }
        }

        public void tambahDataRekamJejak()
        {
            //ViewState["modeDataBaru"] = true;
            //mvIdentitas.SetActiveView(vInsup);
            //isiddlThnProsiding();
            ////isiDataInsup();
            //isiDdlPeranPenulis();
            //isiDdlJenisProsiding();
        }

        protected void lbTambah_click(object sender, EventArgs e)
        {
            ViewState["id_prosiding_unggah"] = Guid.NewGuid();
            ViewState["modeDataBaru"] = true;
            mvIdentitas.SetActiveView(vInsup);
            isiddlThnProsiding();
            tbJudul.Text="";
            tbNamaProsiding.Text="";

            //isiDataInsup();
            //isiDdlPeranPenulis();
            //isiDdlJenisProsiding();
        }

        private void isiddlThnProsiding()
        {
            ddlThnProsiding.Items.Clear();
            //ddlThnProsiding.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2005; i--)
            {
                ddlThnProsiding.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void isiDdlPeranPenulis()
        {
            //ddlPeranPenulis.Items.Clear();
            //DataTable dt = new DataTable();
            //objPengusul.getPeranPenulisProsiding(ref dt);
            //ddlPeranPenulis.Items.Add(new ListItem("--Pilih--", "0"));
            //objDdl.bindToDropDownList(ref ddlPeranPenulis, dt, "peran_penulis", "kd_peran_penulis");
            //ddlPeranPenulis.SelectedIndex = 0;
        }

        private void isiDdlJenisProsiding()
        {
            //ddlJenisProsiding.Items.Clear();
            //DataTable dt = new DataTable();
            //objPengusul.getJenisProsiding(ref dt);
            //ddlJenisProsiding.Items.Add(new ListItem("--Pilih--", "0"));
            //objDdl.bindToDropDownList(ref ddlJenisProsiding, dt, "jenis_prosiding", "kd_jenis_prosiding");
            //ddlJenisProsiding.SelectedIndex = 0;
        }

        protected void lbClose_click(object sender, EventArgs e)
        {
            //mvIdentitas.SetActiveView(vDaftar);
            //isiDataProsiding();
        }

        protected void lbSimpan_click(object sender, EventArgs e)
        {
            Boolean is_penyaji=false;
            if (bool.Parse(ViewState["modeDataBaru"].ToString())) //tambah data baru
            {
                //Cek Isian


                List<string> isianKosong = new List<string>();
                if (tbJudul.Text.Trim().Length == 0) isianKosong.Add("Judul Makalah");
                if (tbNamaProsiding.Text.Trim().Length == 0) isianKosong.Add("Nama Forum");
                if (!(rbispemakalah.SelectedValue =="0" || rbispemakalah.SelectedValue == "1")) isianKosong.Add("Pilihan Penyaji belum di isi");

                if (isianKosong.Count > 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi kesalahan", "Data berikut harus disi :<br />" + string.Join(", ", isianKosong.ToArray()));
                    return;
                }
                else
                {
                    //Guid id_prosiding = Guid.NewGuid(); //Guid.Empty;
                    var ktUnggah = (kontrolUnggah)Session["ktUnggah"];

                    if (ktUnggah == null || ktUnggah.isSuccess == false)
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Error Unggah Berkas","Dokumen Belum di unggah");
                        return;

                    };

                    if (rbispemakalah.SelectedValue == "1") { is_penyaji = true; };
                    Guid id_prosiding_unggah = Guid.Parse(ViewState["id_prosiding_unggah"].ToString());
                    if (!objPemakalah.insupDatapemakalah(ddlThnProsiding.SelectedValue, Guid.Parse(objLogin.idPersonal),
                        tbJudul.Text, tbNamaProsiding.Text, id_prosiding_unggah,is_penyaji))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                        mvIdentitas.SetActiveView(vDaftar);
                        objPemakalah.updateberkassbgPemakalahtanpaprosiding(id_prosiding_unggah.ToString());
                        isiDataProsiding();
                    }
                }
            }
            else // update data
            {
                //Cek Isian

                List<string> isianKosong = new List<string>();
                if (tbJudul.Text.Trim().Length == 0) isianKosong.Add("Judul Prosiding");
                if (tbNamaProsiding.Text.Trim().Length == 0) isianKosong.Add("Nama Prosiding");

                if (isianKosong.Count > 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi kesalahan", "Data berikut harus disi :<br />" + string.Join(", ", isianKosong.ToArray()));
                    return;
                }
                else
                {
                    if (rbispemakalah.SelectedValue == "1") { is_penyaji = true; };
                    Guid id_prosiding_Update = Guid.Parse(ViewState["id_pemakalah"].ToString());
                    if (!objPemakalah.insupDatapemakalah(ddlThnProsiding.SelectedValue, Guid.Parse(objLogin.idPersonal),
                        tbJudul.Text, tbNamaProsiding.Text, id_prosiding_Update, is_penyaji))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                        mvIdentitas.SetActiveView(vDaftar);
                        objPengusul.updateStatusUnggah(id_prosiding_Update);
                        isiDataPemakalah();
                    }
                }
            }
        }

        protected void lbBatal_click(object sender, EventArgs e)
        {
            mvIdentitas.SetActiveView(vDaftar);
            isiDataPemakalah();
        }

        protected void gvProsiding_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ViewState["id_prosiding"] = gvProsiding.DataKeys[e.RowIndex]["id_prosiding"].ToString();
            //string id_prosiding = ViewState["id_prosiding"].ToString();

            //lblJudulProsidingHapus.Text = gvProsiding.DataKeys[e.RowIndex]["judul"].ToString();
            //objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (objPemakalah.deleteDataPemakalah(ViewState["id_pemakalah"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data Pemakalah berhasil");
                isiDataPemakalah();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objPengusul.errorMessage);
                isiDataPemakalah();
            }
        }

        protected void gvProsiding_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //ViewState["modeDataBaru"] = false;

            //ViewState["id_prosiding"] = gvProsiding.DataKeys[e.RowIndex]["id_prosiding"].ToString();

            //tbNamaProsiding.Text = gvProsiding.DataKeys[e.RowIndex]["nama_prosiding"].ToString();
            //tbJudul.Text = gvProsiding.DataKeys[e.RowIndex]["judul"].ToString();
            //tbVolume.Text = gvProsiding.DataKeys[e.RowIndex]["volume"].ToString();
            //tbNomor.Text = gvProsiding.DataKeys[e.RowIndex]["nomor"].ToString();
            //tbUrl.Text = gvProsiding.DataKeys[e.RowIndex]["url"].ToString();
            //tbIssn.Text = gvProsiding.DataKeys[e.RowIndex]["issn"].ToString();

            //mvIdentitas.SetActiveView(vInsup);
            //isiddlThnProsiding();
            //ddlThnProsiding.SelectedValue = gvProsiding.DataKeys[e.RowIndex]["thn_prosiding"].ToString();
            //isiDdlPeranPenulis();
            //ddlPeranPenulis.SelectedValue = gvProsiding.DataKeys[e.RowIndex]["kd_peran_penulis"].ToString();
            //isiDdlJenisProsiding();
            //ddlJenisProsiding.SelectedValue = gvProsiding.DataKeys[e.RowIndex]["kd_jenis_prosiding"].ToString();
        }

        protected void lbUnggah_Click(object sender, EventArgs e)
        {
            //objModal.ShowModal(this.Page, "modalUnggahBerkas");
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            Guid id_prosiding = Guid.Empty;
            var ktUnggah = new Core.kontrolUnggah();
            ViewState["kd_unggah"] = "pemakalah";



            if (bool.Parse(ViewState["modeDataBaru"].ToString())) //tambah data baru
            {
                id_prosiding = Guid.NewGuid();

                objModal.ShowModal(this.Page, "modalUnggahBerkas");

                string dirFile = "~/fileUpload/Pemakalah";
                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }
                dirFile = "~/fileUpload/Pemakalah/" + ViewState["thn_pendaftaran"].ToString();
                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }


                ktUnggah.path2save = "~/fileUpload/Pemakalah/" + id_prosiding + ".pdf";
                ktUnggah.max_size = 1024 * 1024; // 500KB
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah Dokumen sebagai penyaji berhasil silahkan klik tombol selesai";
                ktUnggah.failed_info = "Unggah Dokumen sebagai penyaji gagal";
                Session.Add("ktUnggah", ktUnggah);
                ktUnggah.isSuccess = false;
                ViewState["id_prosiding_unggah"] = id_prosiding;
            }
            else
            {
                id_prosiding = Guid.Parse(ViewState["id_pemakalah"].ToString());
                objModal.ShowModal(this.Page, "modalUnggahBerkas");

                ktUnggah.path2save = "~/fileUpload/pemakalah/" + id_prosiding + ".pdf";
                ktUnggah.max_size = 1024 * 1024; // 500KB
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah dokumen pemakalah berhasil silahkan klik tombol selesai";
                ktUnggah.failed_info = "Unggah dokumen pemakalah gagal";
                Session.Add("ktUnggah", ktUnggah);

            }
        }

        protected void gvProsiding_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id_prosiding = gvProsiding.DataKeys[rowIndex]["id_prosiding"].ToString();
            if (e.CommandName == "unduhDokumen")
            {
                string kd_sts_berkas_prosiding = gvProsiding.DataKeys[rowIndex]["kd_sts_unggah_berkas_penyaji"].ToString();
                string nama_prosiding = gvProsiding.DataKeys[rowIndex]["nama_prosiding"].ToString();

                string namaFile = (nama_prosiding.Length > 30) ? nama_prosiding.Substring(0, 30) : nama_prosiding;
                namaFile = namaFile.Replace(" ", "_");
                namaFile = objManipData.removeUnicode(namaFile);

                string namaBerkas = "pemakalah_" + namaFile + ".pdf";

                if (kd_sts_berkas_prosiding == "1")
                {
                    string filePath = string.Format("~/fileUpload/Pemakalah/" + id_prosiding + ".pdf");

                    if (File.Exists(Server.MapPath(filePath)))
                    {
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                        Response.TransmitFile(filePath);
                        Response.End();
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "File tidak ditemukan.");
                        return;
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "File tidak ditemukan.");
                }
            }
            else if (e.CommandName == "unggahDokumen")
            {
                string kd_sts_berkas_prosiding = gvProsiding.DataKeys[rowIndex]["kd_sts_unggah_berkas_penyaji"].ToString();
                string nama_prosiding = gvProsiding.DataKeys[rowIndex]["nama_prosiding"].ToString();

                ViewState["id_prosiding"] = id_prosiding;
                ViewState["kd_unggah"] = "prosiding";

                //ktUnggah.path2save = "~/fileUpload/Pemakalah/" + id_prosiding + ".pdf";
                //ktUnggah.max_size = 1024 * 1024; // 500KB
                //ktUnggah.alllowed_ext = "pdf;PDF";
                //ktUnggah.success_info = "Unggah Dokumen sebagai penyaji berhasil silahkan klik tombol selesai";
                //ktUnggah.failed_info = "Unggah Dokumen sebagai penyaji gagal";
                //Session.Add("ktUnggah", ktUnggah);


                //objModal.ShowModal(this.Page, "modalUnggahBerkas");

                var ktUnggah = new Core.kontrolUnggah();
                {
                    ktUnggah.path2save = "~/fileUpload/Pemakalah/" + id_prosiding + ".pdf";
                    ktUnggah.max_size = 1024 * 1024; // 500KB
                    ktUnggah.alllowed_ext = "pdf;PDF";
                    ktUnggah.success_info = "Unggah Dokumen sebagai penyaji berhasil silahkan klik tombol selesai";
                    ktUnggah.failed_info = "Unggah Dokumen sebagai penyaji gagal";
                    Session.Add("ktUnggah", ktUnggah);
                };
                Session.Add("ktUnggah", ktUnggah);
                ktUnggah.isSuccess = false;
                new uiModal().ShowModal(this.Page, "modalUnggahBerkas");



            }
        }

        //protected void lbBatal_click(object sender, EventArgs e)
        //{
        //    mvIdentitas.SetActiveView(vDaftar);
        //    isiDataKaryaSeni();
        //}


        protected void gvProsiding_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_berkas_prosiding = gvProsiding.DataKeys[e.Row.DataItemIndex]["kd_sts_berkas_prosiding"].ToString();
                bool is_penyaji = bool.Parse(gvProsiding.DataKeys[e.Row.DataItemIndex]["is_penyaji"].ToString());
                LinkButton lbUnduh = (LinkButton)e.Row.FindControl("lbUnduhBerkas");
                RadioButtonList rbProsiding = (RadioButtonList)e.Row.FindControl("rbProsiding");
                LinkButton lbUnggahDokumen = (LinkButton)e.Row.FindControl("lbUnggahDokumen");

                if (is_penyaji == true)
                {
                    rbProsiding.SelectedValue = "1";
                    if (kd_sts_berkas_prosiding != "1")
                    {
                        lbUnduh.ForeColor = System.Drawing.Color.Gray;
                        lbUnduh.Enabled = false;
                        //lbUnggahDokumen.Enabled = true;
                    }
                    else
                    {
                        lbUnduh.ForeColor = System.Drawing.Color.Red;
                        lbUnduh.Enabled = true;
                        //lbUnggahDokumen.Enabled = true;
                    };
                }
                else
                if (is_penyaji == false)
                {
                    rbProsiding.SelectedValue = "0";
                    //lbUnduh.ForeColor = System.Drawing.Color.Gray;
                    //lbUnduh.Enabled = false;
                    //lbUnggahDokumen.Enabled = false;
                    if (kd_sts_berkas_prosiding != "1")
                    {
                        lbUnduh.ForeColor = System.Drawing.Color.Gray;
                        lbUnduh.Enabled = false;
                        //lbUnggahDokumen.Enabled = true;
                    }
                    else
                    {
                        lbUnduh.ForeColor = System.Drawing.Color.Red;
                        lbUnduh.Enabled = true;
                        //lbUnggahDokumen.Enabled = true;
                    };

                }


            }
        }

        protected void rblEvaluasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((RadioButtonList)sender).Parent.Parent)).RowIndex;
            string id_prosiding = gvProsiding.DataKeys[selRowIndex]["id_prosiding"].ToString();
            //string kd_sts_unggah_berkas_penyaji = gvProsiding.DataKeys[selRowIndex]["kd_sts_unggah_berkas_penyaji"].ToString();
            //Boolean is_penyaji = Boolean.Parse(gvProsiding.DataKeys[selRowIndex]["is_penyaji"].ToString());

            ViewState["id_prosiding"] = id_prosiding;

            RadioButtonList rb = (RadioButtonList)gvProsiding.Rows[selRowIndex].FindControl("rbProsiding");
            //LinkButton lbUnggahDokumen = (LinkButton)gvProsiding.Rows[selRowIndex].FindControl("lbUnggahDokumen");
            //LinkButton lbUnduhBerkas = (LinkButton)gvProsiding.Rows[selRowIndex].FindControl("lbUnduhBerkas");
            if (rb.SelectedValue == "1")
            {

                //lbUnggahDokumen.Enabled = true;
                //if (kd_sts_unggah_berkas_penyaji != "1")
                //{
                //    lbUnduhBerkas.ForeColor = System.Drawing.Color.Gray;
                //    lbUnduhBerkas.Enabled = false;
                //}
                //else
                //{
                //    lbUnduhBerkas.ForeColor = System.Drawing.Color.Red;
                //    lbUnduhBerkas.Enabled = true;
                //};
                ViewState["penyaji"] = true;
                lbltanya.Text = "Update Sebagai Penyaji";

            }
            else
            {
                //lbUnggahDokumen.Enabled = false;
                //lbUnduhBerkas.Enabled = false;
                //lbUnduhBerkas.ForeColor = System.Drawing.Color.Gray;
                ViewState["penyaji"] = false;
                lbltanya.Text = "Update Bukan Sebagai Penyaji";

            }
            objModal.ShowModal(this.Page, "PopupValidasi");

        }

        protected void lbModalStsKonfirmasi_Click(object sender, EventArgs e)
        {
            if (objPemakalah.updatesbgPemakalah(ViewState["penyaji"].ToString(), ViewState["id_prosiding"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Gagal.");

            }
            objModal.HideModal(this.Page, "PopupValidasi");
            isiDataProsiding();

        }

        protected void lbsimpanberkas_Click(object sender, EventArgs e)
        {

            var ktUnggah = (kontrolUnggah)Session["ktUnggah"];

            if (ViewState["kd_unggah"].ToString() == "prosiding")
            {
                if (ktUnggah.isSuccess == true)
                {
                    if (objPemakalah.updateberkassbgPemakalah("1", ViewState["id_prosiding"].ToString()))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Berhasil.");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Gagal.");

                    }
                    isiDataProsiding();

                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Gagal.");

                }
            }
            objModal.HideModal(this.Page, "modalUnggahBerkas");
        }

        protected void gvPemakalah_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "unduhDokumen")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                string id_prosiding = gvPemakalah.DataKeys[rowIndex]["id_pemakalah"].ToString();
                string kd_sts_berkas_prosiding = gvPemakalah.DataKeys[rowIndex]["kd_sts_berkas_pemakalah"].ToString();
                string nama_prosiding = gvPemakalah.DataKeys[rowIndex]["nama_forum"].ToString();

                string namaFile = (nama_prosiding.Length > 30) ? nama_prosiding.Substring(0, 30) : nama_prosiding;
                namaFile = namaFile.Replace(" ", "_");
                namaFile = objManipData.removeUnicode(namaFile);

                string namaBerkas = "pemakalah_" + namaFile + ".pdf";

                if (kd_sts_berkas_prosiding == "1")
                {
                    string filePath = string.Format("~/fileUpload/Pemakalah/" + id_prosiding + ".pdf");

                    if (File.Exists(Server.MapPath(filePath)))
                    {
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                        Response.TransmitFile(filePath);
                        Response.End();
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "File tidak ditemukan.");
                        return;
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "File tidak ditemukan.");
                }
            }

        }

        protected void gvPemakalah_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_berkas_prosiding = gvPemakalah.DataKeys[e.Row.DataItemIndex]["kd_sts_berkas_pemakalah"].ToString();
                Boolean is_penyaji = Boolean.Parse(gvPemakalah.DataKeys[e.Row.DataItemIndex]["is_penyaji"].ToString());
                LinkButton lbUnduh = (LinkButton)e.Row.FindControl("lbUnduhBerkas");
                RadioButtonList rbPemakalah = (RadioButtonList)e.Row.FindControl("rbPemakalah");
                //LinkButton lbUnggahDokumen = (LinkButton)e.Row.FindControl("lbUnggahDokumen");

                if (is_penyaji == true)
                {
                    rbPemakalah.SelectedValue = "1";
                    if (kd_sts_berkas_prosiding != "1")
                    {
                        lbUnduh.ForeColor = System.Drawing.Color.Gray;
                        lbUnduh.Enabled = false;
                        lbUnggahDokumen.Enabled = true;
                    }
                    else
                    {
                        lbUnduh.ForeColor = System.Drawing.Color.Red;
                        lbUnduh.Enabled = true;
                        lbUnggahDokumen.Enabled = true;
                    };
                }
                else
                if (is_penyaji == false)
                {
                    rbPemakalah.SelectedValue = "0";
                    //lbUnduh.ForeColor = System.Drawing.Color.Gray;
                    //lbUnduh.Enabled = false;
                    //lbUnggahDokumen.Enabled = false;
                    if (kd_sts_berkas_prosiding != "1")
                    {
                        lbUnduh.ForeColor = System.Drawing.Color.Gray;
                        lbUnduh.Enabled = false;
                        lbUnggahDokumen.Enabled = true;
                    }
                    else
                    {
                        lbUnduh.ForeColor = System.Drawing.Color.Red;
                        lbUnduh.Enabled = true;
                        lbUnggahDokumen.Enabled = true;
                    };

                }


            }

        }

        protected void gvPemakalah_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["modeDataBaru"] = false;

            ViewState["id_pemakalah"] = gvPemakalah.DataKeys[e.RowIndex]["id_pemakalah"].ToString();

            tbNamaProsiding.Text = gvPemakalah.DataKeys[e.RowIndex]["nama_forum"].ToString();
            tbJudul.Text = gvPemakalah.DataKeys[e.RowIndex]["judul"].ToString();

            isiddlThnProsiding();
            ddlThnProsiding.SelectedValue = gvPemakalah.DataKeys[e.RowIndex]["thn_pemakalah"].ToString();

            Boolean is_penyaji = Boolean.Parse(gvPemakalah.DataKeys[e.RowIndex]["is_penyaji"].ToString());
            if (is_penyaji==true)
            {
                rbispemakalah.Items[1].Selected = false;
                rbispemakalah.Items[0].Selected = true;
            }
            else
            {
                rbispemakalah.Items[0].Selected = false;
                rbispemakalah.Items[1].Selected = true;
            }
            mvIdentitas.SetActiveView(vInsup);

        }

        protected void gvPemakalah_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["id_pemakalah"] = gvPemakalah.DataKeys[e.RowIndex]["id_pemakalah"].ToString();
            string id_pemakalah = ViewState["id_pemakalah"].ToString();

            lblJudulProsidingHapus.Text = gvPemakalah.DataKeys[e.RowIndex]["judul"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");

        }


        protected void lbKembali_click(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            objModal.HideModal(this.Page, "PopupValidasi");
            isiDataProsiding();

        }
    }
}