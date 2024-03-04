using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;
using System.IO;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class unggahDokumenRenstra : System.Web.UI.UserControl
    {
        Models.PT.daftarRenstra modelRenstra = new Models.PT.daftarRenstra();

        UIControllers.uiGridView obj_uiGridView = new UIControllers.uiGridView();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        UIControllers.uiDropdownList obj_uiDropdownlist = new UIControllers.uiDropdownList();
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        Models.login objLogin;

        string[] namaKolom = { "no_baris, id_sk, nama_dokumen, no_sk, thn_sk" };
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvDaftarDokumenRenstra);
            
            if (!IsPostBack)
            {
                isiDdlTahun();
                isiDdlJenisDokumen();
                ddlJenisDokumenEvent();
                refreshGridView();
                konfigurasiLabel();
            }
        }

        private void konfigurasiLabel()
        {
            switch (objLogin.idPeran)
            {
                case 6:    // opt. pt penelitian
                    {
                        lblJudulPage.Text = "Unggah Dokumen Renstra Penelitian";
                        lblNoSkAtauNoSuratPengantar.Text = "Nomor SK";
                    }
                    break;
                case 40:    // opt. pt Pengabdian
                    {
                        lblJudulPage.Text = "Unggah Dokumen Renstra Pengabdian kepada Masyarakat";
                        lblNoSkAtauNoSuratPengantar.Text = "Nomor Surat";
                    }
                    break;
            }
        }

        private void isiDdlTahun()
        {
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());

            ddlThnUpload.Items.Clear();
            for (int i = thnSKg; i >= 2013; i--)
            {
                ddlThnUpload.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }

            ddlThnTerbit.Items.Clear();
            for (int i = thnSKg; i >= 2013; i--)
            {
                ddlThnTerbit.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void isiDdlJenisDokumen()
        {
            Guid id_personal = Guid.Parse(objLogin.idPersonal.ToString());
            
            DataTable dtJenisDokumenRenstra = new DataTable();

            if (modelRenstra.getJenisDokumenRenstra(ref dtJenisDokumenRenstra, id_personal))
            {
                dtJenisDokumenRenstra = modelRenstra.currentRecords;
                if (modelRenstra.getJenisDokumenRenstra(ref dtJenisDokumenRenstra, id_personal))
                    dtJenisDokumenRenstra = modelRenstra.currentRecords;
                if (dtJenisDokumenRenstra.Rows.Count > 0)
                {
                    string nama_dokumen = dtJenisDokumenRenstra.Rows[0]["nama_dokumen"].ToString();
                    string kd_jenis_sk = dtJenisDokumenRenstra.Rows[0]["kd_jenis_sk"].ToString();

                    if (!obj_uiDropdownlist.bindToDropDownList(ref ddlJenisDokumen, dtJenisDokumenRenstra, "nama_dokumen", "kd_jenis_sk"))
                    {
                        ddlJenisDokumen.Items.Add(new ListItem("--Pilih--", "00"));
                        lblError.Text = obj_uiDropdownlist.errorMessage;
                        return;
                    }
                }                
            }
        }

        protected void ddlThnUpload_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGridView();
            isiDdlJenisDokumen();
        }

        private void refreshGridView()
        {
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());
            Guid id_personal = Guid.Parse(objLogin.idPersonal.ToString());

            DataTable dtDokumenRenstra = new DataTable();
            Models.PT.daftarRenstra modelRenstra = new Models.PT.daftarRenstra();

            if (modelRenstra.getDokumenRenstra(ref dtDokumenRenstra, id_personal, id_institusi, ddlThnUpload.SelectedValue))
            {
                dtDokumenRenstra = modelRenstra.currentRecords;
                if (modelRenstra.getDokumenRenstra(ref dtDokumenRenstra, id_personal, id_institusi, ddlThnUpload.SelectedValue))
                    dtDokumenRenstra = modelRenstra.currentRecords;
                if (!obj_uiGridView.bindToGridView(ref gvDaftarDokumenRenstra, dtDokumenRenstra, namaKolom))
                    if (dtDokumenRenstra.Rows.Count == 0)
                    {
                        //noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
                    };
            }
        }

        protected void lbUnggah_Click(object sender, EventArgs e)
        {
            ViewState["modeDataBaru"] = true;
            uiMdl.ShowModal(this.Page, "modalUploadDokumen");
        }
        
        protected void ddlJenisDokumen_SelectedIndexChanged(object sender, EventArgs e)
        {
            uiMdl.ShowModal(this.Page, "modalUploadDokumen");
            ddlJenisDokumenEvent();
        }

        private void ddlJenisDokumenEvent()
        {
            if (ddlJenisDokumen.SelectedValue == "03" || ddlJenisDokumen.SelectedValue == "07")
            {
                tbNomorSk.Enabled = false;
                tbNomorSk.Text = "-";
            }
            else
            {
                tbNomorSk.Enabled = true;
            }
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());

            if (tbNomorSk.Text.Trim() == "" && ddlJenisDokumen.SelectedValue == "02")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Nomor SK harap diisi, jika tidak ada beri tanda strip (-).");
            }
            else if (ddlJenisDokumen.SelectedValue == "00")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Jenis dokumen/sk harap dipilih.");
            }
            else if (fileUpload1.HasFile)
            {
                if (fileUpload1.FileName.EndsWith(".pdf") || fileUpload1.FileName.EndsWith(".PDF"))
                {
                    if (fileUpload1.PostedFile.ContentLength < (5 * 1024 * 1024))
                    {
                        Guid id_sk = Guid.NewGuid();
                        if (ddlJenisDokumen.SelectedValue == "02")
                        {
                            if (modelRenstra.apaSK_RIPSudahDiunggah(id_institusi, ref id_sk, "02", ddlThnTerbit.SelectedValue, ddlThnUpload.SelectedValue))
                                lblError.Text = modelRenstra.errorMessage;
                        }
                        else if (ddlJenisDokumen.SelectedValue == "03")
                        {
                            if (modelRenstra.apaDokumenRIPSudahDiunggah(id_institusi, ref id_sk, "03", ddlThnTerbit.SelectedValue, ddlThnUpload.SelectedValue))
                                lblError.Text = modelRenstra.errorMessage;
                        }
                        else if (ddlJenisDokumen.SelectedValue == "06")
                        {
                            if (modelRenstra.apaSuratPengantarRenstraPpmSdhDiunggah(id_institusi, ref id_sk, ddlThnTerbit.SelectedValue, ddlThnUpload.SelectedValue))
                                lblError.Text = modelRenstra.errorMessage;
                        }
                        else if (ddlJenisDokumen.SelectedValue == "07")
                        {
                            if (modelRenstra.apaDokumenRenstraPpmSudahDiunggah(id_institusi, ref id_sk, ddlThnTerbit.SelectedValue, ddlThnUpload.SelectedValue))
                                lblError.Text = modelRenstra.errorMessage;
                        }
                        unggahSK_Dokumen(id_sk);
                    }
                    else
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File yang akan diunggah ukurannya tidak boleh melebihi 5 MByte !!!");
                }
                else
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
            }
            else
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File belum dipilih!");
        }

        private void unggahSK_Dokumen(Guid id_sk)
        {
            string dirFile = "~/fileUpload/SK/"+ddlThnUpload.SelectedValue;
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            string namaFile = string.Format("~/fileUpload/SK/{0}/{1}.pdf", ddlThnUpload.SelectedValue, id_sk);
            try
            {
                fileUpload1.SaveAs(Server.MapPath(namaFile));
                insertUpdateStatusUnggahSk(id_sk);
                refreshGridView();
                isiDdlJenisDokumen();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Terjadi Kesalahan" + modelRenstra.errorMessage);
            }
        }

        protected void insertUpdateStatusUnggahSk(Guid id_sk)
        {
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thn_upload = DateTime.Now.Year.ToString();

            if (modelRenstra.InsupDokumenRenstra(id_sk, tbNomorSk.Text, ddlJenisDokumen.SelectedValue, id_institusi, ddlThnTerbit.SelectedValue, thn_upload))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                refreshGridView();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal" + modelRenstra.errorMessage);
            }
        }

        protected void gvDaftarDokumenRenstra_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName;
            int idx = Convert.ToInt32(e.CommandArgument);
            Guid id_sk = Guid.Parse(gvDaftarDokumenRenstra.DataKeys[idx]["id_sk"].ToString());
            switch (cmd.ToUpper())
            {
                case "UNDUHDOKUMEN":
                    unduhDokumen(id_sk);
                    break;
                case "HAPUSDOKUMENSK":
                    hapusDokumen(id_sk);
                    break;
            }
        }

        private void unduhDokumen(Guid id_sk)
        {
            try
            {
                Response.ContentType = "application/pdf";
                string namaInstitusi = objLogin.namaInstitusi.Replace(" ", "_");
                string namaFile = "SK_" + namaInstitusi + ".pdf";

                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
                Response.ContentType = "application/pdf";
                string filePath = string.Format("~/fileUpload/SK/{0}/{1}.pdf", ddlThnUpload.SelectedValue, id_sk);
                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }
            catch (Exception err)
            {
                string errorMessage = err.Message;
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File PDF corrupted...<br/> Harap diunggah ulang...!!!");
                return;
            }
        }

        private void hapusDokumen(Guid id_dokumen)
        {
            ViewState["idDok"] = id_dokumen;
            new uiModal().ShowModal(this.Page, "modalKonfirmasiHapus");
        }

        protected void btnKonfirmasiHapus_Click(object sender, EventArgs e)
        {            
            string thnUnggah = ddlThnUpload.SelectedValue;
            const string FolderName = "SK";
            string idDok = ViewState["idDok"].ToString();
            if (ApaBerkasMasihAda(this.Page, idDok, thnUnggah, FolderName))
            {
                if (!HapusBerkas(this.Page, idDok, thnUnggah, FolderName))
                {
                }
            }

            string filePath = string.Format("~/fileUpload/SK/{0}/{1}.pdf",
                thnUnggah, idDok);

            if (File.Exists(Server.MapPath(filePath)))
            {
                File.Delete(Server.MapPath(filePath));
            }


            if (modelRenstra.deleteRenstra(Guid.Parse(ViewState["idDok"].ToString())))
            {
                refreshGridView();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil dihapus...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelRenstra.errorMessage);
            }
            new uiModal().HideModal(this.Page, "modalKonfirmasiHapus");
        }

        public bool ApaBerkasMasihAda(Page thePage, string fileName, string thnUnggah, string fileFolder)
        {
            string path = string.Format("~/fileUpload/{0}/{1}/{2}.pdf", fileFolder, thnUnggah, fileName);
            
            string filePath = thePage.Server.MapPath(path);
            return (File.Exists(filePath));
        }

        public bool HapusBerkas(Page thePage, string fileName, string thnUnggah, string fileFolder)
        {
            bool status = false;
            string path = string.Format("~/fileUpload/{0}/{1}/{2}.pdf", fileFolder, thnUnggah, fileName);            
            string filePath = thePage.Server.MapPath(path);

            try
            {
                if (ApaBerkasMasihAda(thePage, fileName, thnUnggah, fileFolder))
                {
                    File.Delete(filePath);
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }

            return status;
        }
    }
}
