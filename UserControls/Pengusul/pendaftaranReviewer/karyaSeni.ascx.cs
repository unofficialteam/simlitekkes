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
    public partial class karyaSeni : System.Web.UI.UserControl
    {
        login objLogin;
        Models.Pengusul.berandaPengusul objPengusul = new Models.Pengusul.berandaPengusul();
        Models.Pengusul.persyaratanUmumPendaftaranReviewer objKaryaSeni = new Models.Pengusul.persyaratanUmumPendaftaranReviewer();
        uiNotify noty = new uiNotify();
        uiDropdownList objDdl = new uiDropdownList();
        uiModal objModal = new uiModal();
        //Core.kontrolUnggah ktUnggah = new Core.kontrolUnggah();
        Core.manipulasiData objManipData = new Core.manipulasiData();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnggahDokumen);

            objLogin = (login)Session["objLogin"];

            isiDataKaryaSeni();
            isiDdlTingkatKegiatan();
            isiDdlJenisKaryaSeni();
        }

        public void init(string thnPendaftaran)
        {
            ViewState["thn_pendaftaran"] = thnPendaftaran;
        }

        private void isiDdlTingkatKegiatan()
        {
            ddlTingkat.Items.Clear();
            DataTable dt = new DataTable();
            objKaryaSeni.getTingkatKegiatan(ref dt);
            ddlTingkat.Items.Add(new ListItem("--Pilih--", "0"));
            objDdl.bindToDropDownList(ref ddlTingkat, dt, "tingkat_kegiatan", "kd_tingkat");
            ddlTingkat.SelectedIndex = 0;
        }

        private void isiDdlJenisKaryaSeni()
        {
            ddlJnKaryaSeni.Items.Clear();
            DataTable dt = new DataTable();
            objKaryaSeni.getJenisKaryaSeni(ref dt);
            ddlJnKaryaSeni.Items.Add(new ListItem("--Pilih--", "0"));
            objDdl.bindToDropDownList(ref ddlJnKaryaSeni, dt, "jn_karya_seni", "kd_jn_karya_seni");
            ddlJnKaryaSeni.SelectedIndex = 0;
        }

        public void isiDataKaryaSeni()
        {
            DataTable dt = new DataTable();
            if (objKaryaSeni.getKaryaSeni(ref dt, objLogin.idPersonal))
            {
                gvDaftar.DataSource = dt;
                gvDaftar.DataBind();
            }
        }


        protected void lbTambah_click(object sender, EventArgs e)
        {
            ViewState["id_karya_seni_unggah"] = Guid.NewGuid();
            ViewState["modeDataBaru"] = true;
            mvIdentitas.SetActiveView(vInsup);
        }




        protected void lbClose_click(object sender, EventArgs e)
        {
            //mvIdentitas.SetActiveView(vDaftar);
            //isiDataProsiding();
        }

        protected void lbSimpan_click(object sender, EventArgs e)
        {
            DateTime tgl;
            if (bool.Parse(ViewState["modeDataBaru"].ToString())) //tambah data baru
            {
                //Cek Isian


                List<string> isianKosong = new List<string>();
                if (tbJudul.Text.Trim().Length == 0) isianKosong.Add("Judul Karya Seni");
                if (!DateTime.TryParse(tbTglMulai.Text, out tgl)) isianKosong.Add("Tanggal Mulai");
                if (!DateTime.TryParse(tbTglAkhir.Text, out tgl)) isianKosong.Add("Tanggal Akhir");
                if (ddlJnKaryaSeni.SelectedValue == "0") isianKosong.Add("Jenis Karya seni");
                if (ddlTingkat.SelectedValue == "0") isianKosong.Add("Tingkat Kegiatan");

                if (isianKosong.Count > 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi kesalahan", "Data berikut harus disi :<br />" + string.Join(", ", isianKosong.ToArray()));
                    return;
                }
                else
                {
                    //Guid id_karya_seni = Guid.NewGuid(); //Guid.Empty;
                    var ktUnggah = (kontrolUnggah)Session["ktUnggah"];

                    if (ktUnggah == null || ktUnggah.isSuccess == false)
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Error Unggah Berkas", "Dokumen Belum di unggah");
                        return;

                    };

                    Guid id_karya_seni_unggah = Guid.Parse(ViewState["id_karya_seni_unggah"].ToString());
                    if (!objKaryaSeni.insupDataKaryaSeni(ddlJnKaryaSeni.SelectedValue, objLogin.idPersonal,
                        tbJudul.Text,ddlTingkat.SelectedValue, tbLokasi.Text, tbTglMulai.Text, tbTglAkhir.Text,"1", id_karya_seni_unggah.ToString()))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                        mvIdentitas.SetActiveView(vDaftar);
                        isiDataKaryaSeni();
                        //objKaryaSeni.updateberkassbgKaryaSenitanpaprosiding(id_karya_seni_unggah.ToString());
                    }
                }
            }
            else // update data
            {
                //Cek Isian

                List<string> isianKosong = new List<string>();
                if (tbJudul.Text.Trim().Length == 0) isianKosong.Add("Judul Karya Seni");
                if (!DateTime.TryParse(tbTglMulai.Text, out tgl)) isianKosong.Add("Tanggal Mulai");
                if (!DateTime.TryParse(tbTglAkhir.Text, out tgl)) isianKosong.Add("Tanggal Akhir");
                if (ddlJnKaryaSeni.SelectedValue == "0") isianKosong.Add("Jenis Karya seni");
                if (ddlTingkat.SelectedValue == "0") isianKosong.Add("Tingkat Kegiatan");

                if (isianKosong.Count > 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi kesalahan", "Data berikut harus disi :<br />" + string.Join(", ", isianKosong.ToArray()));
                    return;
                }
                else
                {
                    Guid id_karya_seni_Update = Guid.Parse(ViewState["id_karya_seni"].ToString());
                    if (!objKaryaSeni.insupDataKaryaSeni(ddlJnKaryaSeni.SelectedValue, objLogin.idPersonal,
                        tbJudul.Text, ddlTingkat.SelectedValue, tbLokasi.Text, tbTglMulai.Text, tbTglAkhir.Text, "1", id_karya_seni_Update.ToString()))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                        //objPengusul.updateStatusUnggah(id_karya_seni_Update);
                        mvIdentitas.SetActiveView(vDaftar);
                        isiDataKaryaSeni();
                    }
                }
            }
        }

        protected void lbBatal_click(object sender, EventArgs e)
        {
            mvIdentitas.SetActiveView(vDaftar);
            isiDataKaryaSeni();
        }

        protected void gvProsiding_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ViewState["id_karya_seni"] = gvProsiding.DataKeys[e.RowIndex]["id_karya_seni"].ToString();
            //string id_karya_seni = ViewState["id_karya_seni"].ToString();

            //lblJudulProsidingHapus.Text = gvProsiding.DataKeys[e.RowIndex]["judul"].ToString();
            //objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            hapusKaryaSeni();
        }

        public void hapusKaryaSeni()
        {
            if (ViewState["id_karya_seni"] == null)
                return;

            if (objKaryaSeni.deleteDataKaryaSeni(ViewState["id_karya_seni"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data KaryaSeni berhasil");
                string path = "~/fileUpload/DokumenPendukung/" + ViewState["id_karya_seni"].ToString() + ".pdf";
                if (File.Exists(Server.MapPath(path)))
                {
                    File.Delete(Server.MapPath(path));
                }
                isiDataKaryaSeni();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objPengusul.errorMessage);
                isiDataKaryaSeni();
            }
        }

        protected void lbUnggah_Click(object sender, EventArgs e)
        {
            //objModal.ShowModal(this.Page, "modalUnggahBerkas");
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            Guid id_karya_seni = Guid.Empty;
            var ktUnggah = new Core.kontrolUnggah();
            
            if (bool.Parse(ViewState["modeDataBaru"].ToString())) //tambah data baru
            {
                id_karya_seni = Guid.NewGuid();

                objModal.ShowModal(this.Page, "modalUnggahBerkas");

                ktUnggah.path2save = "~/fileUpload/DokumenPendukung/" + id_karya_seni + ".pdf";
                ktUnggah.max_size = 1024 * 1024; // 500KB
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah Dokumen Karya Seni silahkan klik tombol selesai";
                ktUnggah.failed_info = "Unggah Dokumen Karya seni gagal";
                Session.Add("ktUnggah", ktUnggah);
                ktUnggah.isSuccess = false;
                ViewState["id_karya_seni_unggah"] = id_karya_seni;
            }
            else
            {
                id_karya_seni = Guid.Parse(ViewState["id_karya_seni"].ToString());
                objModal.ShowModal(this.Page, "modalUnggahBerkas");

                ktUnggah.path2save = "~/fileUpload/DokumenPendukung/" + id_karya_seni + ".pdf";
                ktUnggah.max_size = 1024 * 1024; // 500KB
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah dokumen Karya Seni berhasil silahkan klik tombol selesai";
                ktUnggah.failed_info = "Unggah dokumen Karya Seni gagal";
                Session.Add("ktUnggah", ktUnggah);

            }
        }


        //protected void lbModalStsKonfirmasi_Click(object sender, EventArgs e)
        //{
        //    if (objKaryaSeni.updatesbgKaryaSeni(ViewState["penyaji"].ToString(), ViewState["id_karya_seni"].ToString()))
        //    {
        //        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Berhasil.");
        //    }
        //    else
        //    {
        //        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Gagal.");

        //    }
        //    objModal.HideModal(this.Page, "PopupValidasi");

        //}

        protected void lbsimpanberkas_Click(object sender, EventArgs e)
        {

            //var ktUnggah = (kontrolUnggah)Session["ktUnggah"];

            //if (ViewState["kd_unggah"].ToString() == "prosiding")
            //{
            //    if (ktUnggah.isSuccess == true)
            //    {
            //        if (objKaryaSeni.updateberkassbgKaryaSeni("1", ViewState["id_karya_seni"].ToString()))
            //        {
            //            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Berhasil.");
            //        }
            //        else
            //        {
            //            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Gagal.");

            //        }

            //    }
            //    else
            //    {
            //        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data Gagal.");

            //    }
            //}
            objModal.HideModal(this.Page, "modalUnggahBerkas");
        }

        protected void gvDaftar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "unduhDokumen")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                string id_karya_seni = gvDaftar.DataKeys[rowIndex]["id_karya_seni"].ToString();
                string kd_sts_berkas = gvDaftar.DataKeys[rowIndex]["kd_sts_berkas"].ToString();
                string judul_pementasan = gvDaftar.DataKeys[rowIndex]["judul_pementasan"].ToString();

                string namaFile = (judul_pementasan.Length > 30) ? judul_pementasan.Substring(0, 30) : judul_pementasan;
                namaFile = namaFile.Replace(" ", "_");
                namaFile = objManipData.removeUnicode(namaFile);

                string namaBerkas = "KaryaSeni_" + namaFile + ".pdf";

                if (kd_sts_berkas == "1")
                {
                    string filePath = string.Format("~/fileUpload/DokumenPendukung/" + id_karya_seni + ".pdf");

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

        protected void gvDaftar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_berkas_prosiding = gvDaftar.DataKeys[e.Row.DataItemIndex]["kd_sts_berkas"].ToString();
                //Boolean is_penyaji = Boolean.Parse(gvDaftar.DataKeys[e.Row.DataItemIndex]["is_penyaji"].ToString());
                LinkButton lbUnduh = (LinkButton)e.Row.FindControl("lbUnduhBerkas");
                //RadioButtonList rbKaryaSeni = (RadioButtonList)e.Row.FindControl("rbKaryaSeni");
                //LinkButton lbUnggahDokumen = (LinkButton)e.Row.FindControl("lbUnggahDokumen");

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

        protected void gvDaftar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["modeDataBaru"] = false;

            ViewState["id_karya_seni"] = gvDaftar.DataKeys[e.RowIndex]["id_karya_seni"].ToString();

            tbJudul.Text = gvDaftar.DataKeys[e.RowIndex]["judul_pementasan"].ToString();
            tbLokasi.Text = gvDaftar.DataKeys[e.RowIndex]["tempat_pelaksanaan"].ToString();

            ddlJnKaryaSeni.SelectedValue = gvDaftar.DataKeys[e.RowIndex]["kd_jn_karya_seni"].ToString();
            ddlTingkat.SelectedValue = gvDaftar.DataKeys[e.RowIndex]["kd_tingkat_pementasan"].ToString();

            DateTime tgl_mulai = DateTime.Parse(gvDaftar.DataKeys[e.RowIndex]["tgl_mulai"].ToString());
            tbTglMulai.Text = tgl_mulai.Month + "" + tgl_mulai.Day + "" + tgl_mulai.Year;

            DateTime tgl_akhir = DateTime.Parse(gvDaftar.DataKeys[e.RowIndex]["tgl_akhir"].ToString());
            tbTglAkhir.Text = tgl_akhir.Month + "" + tgl_akhir.Day + "" + tgl_akhir.Year;


            
            mvIdentitas.SetActiveView(vInsup);

        }

        protected void gvDaftar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["id_karya_seni"] = gvDaftar.DataKeys[e.RowIndex]["id_karya_seni"].ToString();
            //string id_KaryaSeni = ViewState["id_karya_seni"].ToString();

            lblJudulProsidingHapus.Text = gvDaftar.DataKeys[e.RowIndex]["judul_pementasan"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");

        }
    }
}