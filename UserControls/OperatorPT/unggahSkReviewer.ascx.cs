using simlitekkes.Models.PT;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class unggahSkReviewer : System.Web.UI.UserControl
    {
        SKReviewer modelSkReviewer = new SKReviewer();        
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        Models.login objLogin;

        private const string folderSK = "~/fileUpload/Upload/SK/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiddlTahun(ref ddlThnSk);
                isiddlTahun(ref ddlThnSkUnggah);
                refreshGridView();
                kewenanganOperatorPt();
            }
        }

        private void kewenanganOperatorPt()
        {
            if (objLogin.idPeran == 6) 
            {
                lbUnggahSk.Enabled = true;
            }
            else
            {
                lbUnggahSk.Visible = false;
                lbUnggahSk.Enabled = false;
            }
        }

        private void isiddlTahun(ref DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2013; i--)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddl.SelectedValue = thnSKg.ToString();
        }

        protected void ddlThnSk_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGridView();
        }

        private void refreshGridView()
        {
            var idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());

            var dtDokumenSk = new DataTable();

            if (modelSkReviewer.getDokumenSkReviewer(ref dtDokumenSk, idInstitusi, ddlThnSk.SelectedValue))
            {
                gvDaftarDokumenSkReviewer.DataSource = dtDokumenSk;
                gvDaftarDokumenSkReviewer.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelSkReviewer.errorMessage);
            }
        }

        protected void lbUnggahSk_Click(object sender, EventArgs e)
        {
            ViewState["modeDataBaru"] = true;
            uiMdl.ShowModal(this.Page, "modalUploadDokumen");
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());

            if (tbNomorSk.Text.Trim() == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Nomor SK harap diisi, jika tidak ada beri tanda strip (-).");
                return;
            }
            
            if (ddlThnSkUnggah.SelectedIndex <= 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Tahun SK harap dipilih.");
                return;
            }

            if (!fileUpload1.HasFile)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Dokumen yang akan diunggah tidak dapat ditemukan !");
                return;
            }

            if (!fileUpload1.FileName.EndsWith(".pdf"))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                return;
            }

            if (fileUpload1.PostedFile.ContentLength >= (5 * 1024 * 1024))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File yang akan diunggah ukurannya tidak boleh melebihi 5 MByte !!!");
                return;
            }

            if (bool.Parse(ViewState["modeDataBaru"].ToString()))
            {
                Guid id_sk = Guid.NewGuid();
                prosesUnggahSK(id_sk);
            }
            else
            {
                Guid id_sk_update = Guid.Parse(ViewState["id_sk"].ToString());
                prosesUnggahSK(id_sk_update);
            }
        }

        private void prosesUnggahSK(Guid idSK)
        {
            //TODO simpan berkas berdasarkan tahun
            var namaFile = folderSK + $"{idSK}.pdf";

            try
            {
                fileUpload1.SaveAs(Server.MapPath(namaFile));                
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                return;
            }


            if (insertUpdateStatusUnggahSk(idSK))
            {
                refreshGridView();
            }

            
        }

        protected bool insertUpdateStatusUnggahSk(Guid idSK)
        {
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thn_upload = DateTime.Now.Year.ToString();
            string kd_jenis_sk = "01";
            string thnSKUnggah = ddlThnSkUnggah.SelectedValue;

            if (modelSkReviewer.InsupDokumenSK(idSK, tbNomorSk.Text.Trim(), kd_jenis_sk, id_institusi, thnSKUnggah, thn_upload))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah SK Reviewer berhasil");
                refreshGridView();
                clearIsian();
                return true;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Unggah SK Reviewer data gagal" + modelSkReviewer.errorMessage);
                clearIsian();
                return false;
            }
        }

        protected void gvDaftarDokumenSkReviewer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName;
            int idx = Convert.ToInt32(e.CommandArgument);
            Guid id_sk = Guid.Parse(gvDaftarDokumenSkReviewer.DataKeys[idx]["id_sk"].ToString());

            daftarPenugasanReviewerPT modelPenugasanReviewer = new daftarPenugasanReviewerPT();

            if (cmd == "unduhDokumen")
            {
                if (objLogin.idPeran == 6) // opt. pt penelitian
                {
                    unduhDokumen(id_sk);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Dokumen hanya bisa diunduh oleh Operator PT Penelitian");
                }
            }
            else if (cmd == "updateDokumen")
            {
                if (objLogin.idPeran == 6) // && modelPenugasanReviewer.cekApakahKlasterNonBinaanDanPtn(Guid.Parse(objLogin.idInstitusi.ToString())) == true) // opt. pt penelitian
                {
                    ViewState["modeDataBaru"] = false;

                    ViewState["id_sk"] = gvDaftarDokumenSkReviewer.DataKeys[idx]["id_sk"].ToString();
                    tbNomorSk.Text = gvDaftarDokumenSkReviewer.DataKeys[idx]["no_sk"].ToString();
                    ddlThnSkUnggah.SelectedValue = gvDaftarDokumenSkReviewer.DataKeys[idx]["thn_sk"].ToString();

                    uiMdl.ShowModal(this.Page, "modalUploadDokumen");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Dokumen hanya bisa diunggah oleh Operator PT Penelitian dan PT Klaster Non Binaan");
                }
            }
        }

        private void unduhDokumen(Guid id_sk)
        {
            try
            {
                Response.ContentType = "application/pdf";
                string namaInstitusi = objLogin.namaInstitusi.Replace(" ", "_");
                string namaFile = "SK_Reviewer_" + namaInstitusi + ".pdf";

                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
                Response.ContentType = "application/pdf";
                string filePath = folderSK + $"{id_sk}.pdf";
                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }
            catch (Exception ex)
            {                
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", ex.Message);
                return;
            }
        }

        private void clearIsian()
        {
            tbNomorSk.Text = string.Empty;
            //ddlThnSkUnggah.SelectedValue = string.Empty;
        }
    }
}