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
using System.Web.Hosting;
using System.Text;
using simlitekkes.Helper;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class artikelProsiding : System.Web.UI.UserControl
    {
        login objLogin;
        Models.Pengusul.berandaPengusul objPengusul = new Models.Pengusul.berandaPengusul();
        uiNotify noty = new uiNotify();
        uiDropdownList objDdl = new uiDropdownList();
        uiModal objModal = new uiModal();
        Core.kontrolUnggah ktUnggah = new Core.kontrolUnggah();
        Core.manipulasiData objManipData = new Core.manipulasiData();

        const string PATH_BERKAS = "~/fileUpload/fileProsiding/";

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvProsiding);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnggahDokumen);

            objLogin = (login)Session["objLogin"];
        }

        public void isiDataProsiding()
        {
            DataTable dt = new DataTable();
            if (objPengusul.getArtikelProsiding(ref dt, Guid.Parse(objLogin.idPersonal)))
            {
                gvProsiding.DataSource = dt;
                gvProsiding.DataBind();
            }
        }

        public void tambahDataRekamJejak()
        {
            ViewState["modeDataBaru"] = true;
            mvIdentitas.SetActiveView(vInsup);
            isiddlThnProsiding();
            //isiDataInsup();
            isiDdlPeranPenulis();
            isiDdlJenisProsiding();
        }

        protected void lbTambah_click(object sender, EventArgs e)
        {
            ViewState["id_prosiding_unggah"] = Guid.NewGuid();
            ViewState["modeDataBaru"] = true;
            mvIdentitas.SetActiveView(vInsup);
            isiddlThnProsiding();
            //isiDataInsup();
            isiDdlPeranPenulis();
            isiDdlJenisProsiding();
        }

        private void isiddlThnProsiding()
        {
            ddlThnProsiding.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2005; i--)
            {
                ddlThnProsiding.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void isiDdlPeranPenulis()
        {
            ddlPeranPenulis.Items.Clear();
            DataTable dt = new DataTable();
            objPengusul.getPeranPenulisProsiding(ref dt);
            ddlPeranPenulis.Items.Add(new ListItem("--Pilih--", "0"));
            objDdl.bindToDropDownList(ref ddlPeranPenulis, dt, "peran_penulis", "kd_peran_penulis");
            ddlPeranPenulis.SelectedIndex = 0;
        }

        private void isiDdlJenisProsiding()
        {
            ddlJenisProsiding.Items.Clear();
            DataTable dt = new DataTable();
            objPengusul.getJenisProsiding(ref dt);
            ddlJenisProsiding.Items.Add(new ListItem("--Pilih--", "0"));
            objDdl.bindToDropDownList(ref ddlJenisProsiding, dt, "jenis_prosiding", "kd_jenis_prosiding");
            ddlJenisProsiding.SelectedIndex = 0;
        }

        protected void lbSimpan_click(object sender, EventArgs e)
        {
            if (bool.Parse(ViewState["modeDataBaru"].ToString())) //tambah data baru
            {
                //Cek Isian
                List<string> isianKosong = new List<string>();
                if (tbJudul.Text.Trim().Length == 0) isianKosong.Add("Judul Prosiding");
                if (tbNamaProsiding.Text.Trim().Length == 0) isianKosong.Add("Nama Prosiding");
                //if (tbVolume.Text.Trim().Length == 0) isianKosong.Add("Volume Prosiding");
                //if (tbNomor.Text.Trim().Length == 0) isianKosong.Add("Nomor Prosiding");
                if (tbIssn.Text.Trim().Length == 0) isianKosong.Add("ISSN Prosiding");
                //if (tbUrl.Text.Trim().Length == 0) isianKosong.Add("URL Prosiding");
                if (ddlJenisProsiding.SelectedValue == "9") isianKosong.Add("Jenis Prosiding");

                if (isianKosong.Count > 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi kesalahan", "Data berikut harus disi :<br />" + string.Join(", ", isianKosong.ToArray()));
                    return;
                }
                else
                {
                    //Guid id_prosiding = Guid.NewGuid(); //Guid.Empty;
                    Guid id_prosiding_unggah = Guid.Parse(ViewState["id_prosiding_unggah"].ToString());
                    if (!objPengusul.insupDataProsiding(ddlThnProsiding.SelectedValue, Guid.Parse(objLogin.idPersonal),
                        tbJudul.Text, tbNamaProsiding.Text, tbVolume.Text, tbNomor.Text, tbUrl.Text, tbIssn.Text,
                        ddlPeranPenulis.SelectedValue, ddlJenisProsiding.SelectedValue, id_prosiding_unggah))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                        mvIdentitas.SetActiveView(vDaftar);
                        objPengusul.updateStatusUnggah(id_prosiding_unggah);
                        isiDataProsiding();
                    }
                }
            }
            else // update data
            {
                //Cek Isian
                if (tbNomor.Text.Trim().Length == 0) tbNomor.Text = "-";
                if (tbVolume.Text.Trim().Length == 0) tbVolume.Text = "-";

                List<string> isianKosong = new List<string>();
                if (tbJudul.Text.Trim().Length == 0) isianKosong.Add("Judul Prosiding");
                if (tbNamaProsiding.Text.Trim().Length == 0) isianKosong.Add("Nama Prosiding");
                if (tbVolume.Text.Trim().Length == 0) isianKosong.Add("Volume Prosiding");
                if (tbNomor.Text.Trim().Length == 0) isianKosong.Add("Nomor Prosiding");
                if (tbIssn.Text.Trim().Length == 0) isianKosong.Add("ISSN Prosiding");
                //if (tbUrl.Text.Trim().Length == 0) isianKosong.Add("URL Prosiding");
                if (ddlJenisProsiding.SelectedValue == "9") isianKosong.Add("Jenis Prosiding");

                if (isianKosong.Count > 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi kesalahan", "Data berikut harus disi :<br />" + string.Join(", ", isianKosong.ToArray()));
                    return;
                }
                else
                {
                    Guid id_prosiding_Update = Guid.Parse(ViewState["id_prosiding"].ToString());
                    if (!objPengusul.insupDataProsiding(ddlThnProsiding.SelectedValue, Guid.Parse(objLogin.idPersonal),
                        tbJudul.Text, tbNamaProsiding.Text, tbVolume.Text, tbNomor.Text, tbUrl.Text, tbIssn.Text,
                        ddlPeranPenulis.SelectedValue, ddlJenisProsiding.SelectedValue, id_prosiding_Update))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                        mvIdentitas.SetActiveView(vDaftar);
                        objPengusul.updateStatusUnggah(id_prosiding_Update);
                        isiDataProsiding();
                    }
                }
            }
        }

        protected void lbBatal_click(object sender, EventArgs e)
        {
            mvIdentitas.SetActiveView(vDaftar);
            isiDataProsiding();
        }

        protected void gvProsiding_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["id_prosiding"] = gvProsiding.DataKeys[e.RowIndex]["id_prosiding"].ToString();
            string id_prosiding = ViewState["id_prosiding"].ToString();

            lblJudulProsidingHapus.Text = gvProsiding.DataKeys[e.RowIndex]["judul"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (objPengusul.deleteDataProsiding(Guid.Parse(ViewState["id_prosiding"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data Prosiding berhasil");
                isiDataProsiding();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objPengusul.errorMessage);
                isiDataProsiding();
            }
        }

        protected void gvProsiding_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["modeDataBaru"] = false;

            ViewState["id_prosiding"] = gvProsiding.DataKeys[e.RowIndex]["id_prosiding"].ToString();

            tbNamaProsiding.Text = gvProsiding.DataKeys[e.RowIndex]["nama_prosiding"].ToString();
            tbJudul.Text = gvProsiding.DataKeys[e.RowIndex]["judul"].ToString();
            tbVolume.Text = gvProsiding.DataKeys[e.RowIndex]["volume"].ToString();
            tbNomor.Text = gvProsiding.DataKeys[e.RowIndex]["nomor"].ToString();
            tbUrl.Text = gvProsiding.DataKeys[e.RowIndex]["url"].ToString();
            tbIssn.Text = gvProsiding.DataKeys[e.RowIndex]["issn"].ToString();

            mvIdentitas.SetActiveView(vInsup);
            isiddlThnProsiding();
            ddlThnProsiding.SelectedValue = gvProsiding.DataKeys[e.RowIndex]["thn_prosiding"].ToString();
            isiDdlPeranPenulis();
            ddlPeranPenulis.SelectedValue = gvProsiding.DataKeys[e.RowIndex]["kd_peran_penulis"].ToString();
            isiDdlJenisProsiding();
            ddlJenisProsiding.SelectedValue = gvProsiding.DataKeys[e.RowIndex]["kd_jenis_prosiding"].ToString();
        }

        protected void lbUnggah_Click(object sender, EventArgs e)
        {
            objModal.ShowModal(this.Page, "modalUnggahBerkas");
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            Guid id_prosiding = Guid.Empty;
            if (bool.Parse(ViewState["modeDataBaru"].ToString())) //tambah data baru
            {
                id_prosiding = Guid.NewGuid();

                objModal.ShowModal(this.Page, "modalUnggahBerkas");

                ktUnggah.path2save = "~/fileUpload/fileProsiding/" + id_prosiding + ".pdf";
                ktUnggah.max_size = 1024 * 1024; // 500KB
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah artikel prosiding berhasil silahkan klik tombol selesai";
                ktUnggah.failed_info = "Unggah artikel prosiding gagal";
                Session.Add("ktUnggah", ktUnggah);

                ViewState["id_prosiding_unggah"] = id_prosiding;
            }
            else
            {
                id_prosiding = Guid.Parse(ViewState["id_prosiding"].ToString());
                objModal.ShowModal(this.Page, "modalUnggahBerkas");

                ktUnggah.path2save = "~/fileUpload/fileProsiding/" + id_prosiding + ".pdf";
                ktUnggah.max_size = 1024 * 1024; // 500KB
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah artikel prosiding berhasil silahkan klik tombol selesai";
                ktUnggah.failed_info = "Unggah artikel prosiding gagal";
                Session.Add("ktUnggah", ktUnggah);
            }
        }

        protected void gvProsiding_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "unduhDokumen")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                string id_prosiding = gvProsiding.DataKeys[rowIndex]["id_prosiding"].ToString();
                string kd_sts_berkas_prosiding = gvProsiding.DataKeys[rowIndex]["kd_sts_berkas_prosiding"].ToString();
                string nama_prosiding = gvProsiding.DataKeys[rowIndex]["nama_prosiding"].ToString();

                string namaFile = (nama_prosiding.Length > 30) ? nama_prosiding.Substring(0, 30) : nama_prosiding;
                namaFile = namaFile.Replace(" ", "_");
                namaFile = objManipData.removeUnicode(namaFile);

                string namaBerkas = "prosiding_" + namaFile + ".pdf";

                if (kd_sts_berkas_prosiding == "1")
                {
                    string filePath = string.Format(PATH_BERKAS + id_prosiding + ".pdf");

                    if (File.Exists(Server.MapPath(filePath)))
                    {
                        //Response.ContentType = "application/pdf";
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                        //Response.TransmitFile(file.ToString());
                        //Response.End();
                        var atributUnduh = new AtributUnduh
                        {
                            FolderUnduh = PATH_BERKAS,
                            NamaBerkas = id_prosiding + ".pdf",
                            NamaBerkasdiUnduh = namaBerkas
                        };
                        Session["AtributUnduh"] = atributUnduh;

                        var unduhForm = "~/Helper/unduhFile.aspx";
                        Response.Redirect(unduhForm);
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

        protected void gvProsiding_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_berkas_prosiding = gvProsiding.DataKeys[e.Row.DataItemIndex]["kd_sts_berkas_prosiding"].ToString();
                LinkButton lbUnduh = (LinkButton)e.Row.FindControl("lbUnduhBerkas");
                if (kd_sts_berkas_prosiding != "1")
                {
                    lbUnduh.ForeColor = System.Drawing.Color.Gray;
                }
                else
                {
                    lbUnduh.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}