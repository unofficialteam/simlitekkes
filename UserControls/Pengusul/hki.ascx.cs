using simlitekkes.UIControllers;
using simlitekkes.Models.Pengusul;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using simlitekkes.Core;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class hki : System.Web.UI.UserControl
    {
        Models.login objLogin;
        berandaPengusul objPengusul = new berandaPengusul();

        uiGridView obj_uiGridView = new uiGridView();
        uiDropdownList obj_uiDropdownList = new uiDropdownList();
        uiRadioButtonList obj_uiRadioButtonList = new uiRadioButtonList();
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        manipulasiData objManipData = new manipulasiData();
        kontrolUnggah ktUnggah = new kontrolUnggah();


        public void isiHKI()
        {
            DataTable dtHKI = new DataTable();

            string[] kolomHKI = { "id_hak_kekayaan_intelektual", "thn_pelaksanaan", "judul_hki"
                            , "no_pendaftaran", "no_hki", "url", "kd_jenis_hki", "jenis_hki"
                            , "kd_sts_hki", "status_hki", "sts_data","kd_sts_berkas_hki" };

            dtHKI = objPengusul.currentRecords;
            if (objPengusul.listHKI(ref dtHKI, Guid.Parse(objLogin.idPersonal)))
                dtHKI = objPengusul.currentRecords;
            if (!obj_uiGridView.bindToGridView(ref gvHKI, dtHKI, kolomHKI))
                if (dtHKI.Rows.Count == 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada DATA");
                };
        }

        public void tambahDataRekamJejak()
        {
            mvHKI.SetActiveView(vData);
            isiStatusHKI();
            isiJenisHKI();
            isiDdlThnHKI();
            tbJudulHKI.Text = string.Empty;
            tbNoPendaftaran.Text = string.Empty;
            tbNomorHKI.Text = string.Empty;
            tbURLHKI.Text = string.Empty;
            Guid idHKI = Guid.NewGuid();
            ViewState["id_hak_kekayaan_intelektual"] = idHKI;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvHKI);

            if (!IsPostBack)
            {

            }
        }

        protected void lbDataBaruHKI_Click(object sender, EventArgs e)
        {
            mvHKI.SetActiveView(vData);
            isiStatusHKI();
            isiJenisHKI();
            isiDdlThnHKI();
            tbJudulHKI.Text = string.Empty;
            tbNoPendaftaran.Text = string.Empty; 
            tbNomorHKI.Text = string.Empty;
            tbURLHKI.Text = string.Empty;
            Guid idHKI = Guid.NewGuid();
            ViewState["id_hak_kekayaan_intelektual"] = idHKI;
        }

        protected void lbBatalHKI_Click(object sender, EventArgs e)
        {
            const string FolderName = "HKI";
            string idHKI = ViewState["id_hak_kekayaan_intelektual"].ToString();            
            DataTable dt = new DataTable();
            objPengusul.getHKI(ref dt, Guid.Parse(idHKI));
            if (dt.Rows.Count < 1)
            {
                if (ApaBerkasMasihAda(this.Page, idHKI, FolderName))
                {
                    if (!HapusBerkas(this.Page, idHKI, FolderName))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Berkas Pendukung tidak dapat dihapus !");

                        return;
                    }
                }
            }            
            mvHKI.SetActiveView(vDaftar);
            isiHKI();
        }

        protected void rblStatusHKI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rblStatusHKI.SelectedValue == "2")
            {
                tbNomorHKI.Enabled = true;
            }
            else
            {
                tbNomorHKI.Enabled = false;
            }
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            Guid id_personal = Guid.Parse(objLogin.idPersonal.ToString());
            Guid idHKI = Guid.Parse(ViewState["id_hak_kekayaan_intelektual"].ToString());
            string idHKI2 = idHKI.ToString();
            const string FolderName = "fileHKI";

            if (tbJudulHKI.Text == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Judul HKI belum diisi");
            }
            else
            {
                if (ddlTahunHKI.SelectedValue == "0000")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tahun Pelaksanaan belum diisi");
                }
                else
                {
                    if (tbNoPendaftaran.Text == "")
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Nomor Pendaftaran belum diisi");
                    }
                    else
                    {
                        if (rblStatusHKI.SelectedValue == "")
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Status HKI belum diisi");
                        }
                        else
                        {
                            if (rblStatusHKI.SelectedValue == "2")
                            {
                                if (tbNomorHKI.Text == "")
                                {
                                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Nomor HKI belum diisi");
                                }
                                else
                                {
                                    if (objPengusul.insupDataBaruHKI(idHKI, ddlTahunHKI.SelectedValue, id_personal
                                        , tbJudulHKI.Text, ddlJenisHKI.SelectedValue, tbNoPendaftaran.Text, rblStatusHKI.SelectedValue
                                        , tbNomorHKI.Text, tbURLHKI.Text
                                        ))
                                    {
                                        if (ApaBerkasMasihAda(this.Page, idHKI2 , FolderName)) //if (filePath != "")
                                        {
                                            objPengusul.updateStsDokHKI(idHKI);
                                        }

                                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                                        mvHKI.SetActiveView(vDaftar);
                                        isiHKI();
                                    }
                                    else
                                    {
                                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
                                    }
                                }
                            }
                            else
                            {
                                if (objPengusul.insupDataBaruHKI(idHKI, ddlTahunHKI.SelectedValue, id_personal
                                        , tbJudulHKI.Text, ddlJenisHKI.SelectedValue, tbNoPendaftaran.Text, rblStatusHKI.SelectedValue
                                        , tbNomorHKI.Text, tbURLHKI.Text
                                        ))
                                {
                                    if (ApaBerkasMasihAda(this.Page, idHKI2, FolderName))//if (filePath != "")
                                    {
                                        objPengusul.updateStsDokHKI(idHKI);
                                    }

                                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                                    mvHKI.SetActiveView(vDaftar);
                                    isiHKI();
                                }
                                else
                                {
                                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
                                }
                            }
                        }
                    }
                }
            }            
        }

        protected void gvHKI_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {            
            Guid idHKI = Guid.Parse(gvHKI.DataKeys[e.RowIndex]["id_hak_kekayaan_intelektual"].ToString());
            ViewState["id_hak_kekayaan_intelektual"] = idHKI;

            lblJudulHKI.Text = gvHKI.DataKeys[e.RowIndex]["judul_hki"].ToString();
            uiMdl.ShowModal(this.Page, "modalHapus");
            
        }

        protected void gvHKI_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid idHKI = Guid.Parse(gvHKI.DataKeys[e.RowIndex].Values[0].ToString());
            ViewState["id_hak_kekayaan_intelektual"] = idHKI;
            mvHKI.SetActiveView(vData);
            isiStatusHKI();
            isiJenisHKI();
            isiDdlThnHKI();
            isiIdentitasPengusul();
        }

        protected void gvHKI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_validasi_data = gvHKI.DataKeys[e.Row.RowIndex]["sts_data"].ToString();
                string kd_sts_berkas_hki = gvHKI.DataKeys[e.Row.RowIndex]["kd_sts_berkas_hki"].ToString();

                LinkButton lbHapus = new LinkButton();
                LinkButton lbUbah = new LinkButton();
                lbHapus = (LinkButton)e.Row.FindControl("lbHapus");
                lbUbah = (LinkButton)e.Row.FindControl("lbUbah");

                LinkButton lbUnduhBerkas = new LinkButton();
                lbUnduhBerkas = (LinkButton)e.Row.FindControl("lbUnduhBerkas");


                if (kd_sts_validasi_data == "valid")
                {
                    lbHapus.Visible = false;
                    lbUbah.Visible = false;
                }
                else
                {
                    lbHapus.Visible = true;
                    lbUbah.Visible = true;
                }

                if (kd_sts_berkas_hki == "1")
                {
                    lbUnduhBerkas.ForeColor = System.Drawing.Color.Red;
                    lbUnduhBerkas.Enabled = true;
                }
                else
                {
                    lbUnduhBerkas.ForeColor = System.Drawing.Color.Gray;
                    lbUnduhBerkas.Enabled = false;
                }
            }
        }

        protected void lbInfo_Click(object sender, EventArgs e)
        {
            uiMdl.ShowModal(this.Page, "modalInfo");
        }

        protected void lbStatusHKI_Click(object sender, EventArgs e)
        {
            uiMdl.ShowModal(this.Page, "modalInfoStsHKI");
        }

        protected void lbNoHKI_Click(object sender, EventArgs e)
        {
            uiMdl.ShowModal(this.Page, "modalInfoNoHKI");
        }

        protected void lbURLHKI_Click(object sender, EventArgs e)
        {
            uiMdl.ShowModal(this.Page, "modalInfoURL");
        }

        protected void lbUploadHKI_Click(object sender, EventArgs e)
        {
            uiMdl.ShowModal(this.Page, "modalInfoUpload");
        }
       
        protected void lbHapus_Click(object sender, EventArgs e)
        {
            const string FolderName = "HKI";
            string idHKI = ViewState["id_hak_kekayaan_intelektual"].ToString();
            if (ApaBerkasMasihAda(this.Page, idHKI, FolderName))
            {
                if (!HapusBerkas(this.Page, idHKI, FolderName))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Berkas Pendukung tidak dapat dihapus !");
                    
                    return;
                }
            }

            if (objPengusul.hapusHKI(Guid.Parse(idHKI)))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
                isiHKI();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal");
            }
        }

        protected void lbUnggahDokHKI_Click(object sender, EventArgs e)
        {
            Guid idHKI = Guid.Parse(ViewState["id_hak_kekayaan_intelektual"].ToString());
            string filePath = string.Format("~/fileUpload/fileHKI/");
            if (!Directory.Exists(Server.MapPath(filePath)))
            {
                Directory.CreateDirectory(Server.MapPath(filePath));
            }
            ktUnggah.path2save = filePath + idHKI + ".pdf";
            ktUnggah.max_size = 1024 * 1024;
            ktUnggah.alllowed_ext = "pdf;PDF";
            ktUnggah.success_info = "Unggah dokumen hki berhasil";
            ktUnggah.failed_info = "Unggah dokumen hki gagal";
            Session.Add("ktUnggah", ktUnggah);
            uiMdl.ShowModal(this.Page, "modalUnggahDokHKI");
        }        

        protected void gvHKI_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int indek = int.Parse(e.CommandArgument.ToString());
            Guid idHKI = Guid.Parse(gvHKI.DataKeys[indek]["id_hak_kekayaan_intelektual"].ToString());
            if (e.CommandName == "unduhDokumen")
            {
                string namaFile = (tbJudulHKI.Text.Length > 30) ? tbJudulHKI.Text.Substring(0, 30) : tbJudulHKI.Text;
                namaFile = "HKI_" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);             
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
                Response.ContentType = "application/pdf";
                string filePath = string.Format("~/fileUpload/fileHKI/{0}.pdf", idHKI);
                Response.TransmitFile(Server.MapPath(filePath));
                Response.End();
            }
        }

        #region Private Functions

        private void isiJenisHKI()
        {
            DataTable dtJenisHKI = new DataTable();
            ddlJenisHKI.Items.Clear();
            ddlJenisHKI.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            if (objPengusul.listJenisHKI(ref dtJenisHKI))
            {
                if (dtJenisHKI.Rows.Count > 0)
                {
                    if (obj_uiDropdownList.bindToDropDownList(ref ddlJenisHKI, dtJenisHKI, "jenis_hki", "kd_jenis_hki"))
                    {
                        return;
                    }
                }
                else
                {

                }
            }
        }

        private void isiStatusHKI()
        {
            DataTable dtStsHKI = new DataTable();
            if (objPengusul.listStatusHKI(ref dtStsHKI))
            {
                if (dtStsHKI.Rows.Count > 0)
                {
                    if (obj_uiRadioButtonList.bindToRadioButtonList(ref rblStatusHKI, dtStsHKI, "status_hki", "kd_sts_hki"))
                    {
                        return;
                    }
                }
                else
                {

                }
            }
        }

        private void isiDdlThnHKI()
        {
            ddlTahunHKI.Items.Clear();
            ddlTahunHKI.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2005; i--)
            {
                ddlTahunHKI.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void isiIdentitasPengusul()
        {
            Guid idHKI = Guid.Parse(ViewState["id_hak_kekayaan_intelektual"].ToString());
            DataTable dt = new DataTable();
            objPengusul.getHKI(ref dt, idHKI);
            if (dt.Rows.Count > 0)
            {
                tbJudulHKI.Text = dt.Rows[0]["judul_hki"].ToString();
                ddlTahunHKI.SelectedValue = dt.Rows[0]["thn_pelaksanaan"].ToString();
                ddlJenisHKI.SelectedValue = dt.Rows[0]["kd_jenis_hki"].ToString();
                tbNoPendaftaran.Text = dt.Rows[0]["no_pendaftaran"].ToString();
                rblStatusHKI.SelectedValue = dt.Rows[0]["kd_sts_hki"].ToString();
                tbNomorHKI.Text = dt.Rows[0]["no_hki"].ToString();
                tbURLHKI.Text = dt.Rows[0]["url"].ToString();

                if (rblStatusHKI.SelectedValue == "2")
                {
                    tbNomorHKI.Enabled = true;
                }
                else
                {
                    tbNomorHKI.Enabled = false;
                }
            }
            else
            {

            }
        }

        private bool ApaBerkasMasihAda(Page thePage, string fileName, string fileFolder)
        {
            string filePath = thePage.Server.MapPath(string.Format("~/fileUpload/{0}/{1}.pdf", fileFolder, fileName));
            return (File.Exists(filePath));
        }

        private bool HapusBerkas(Page thePage, string fileName, string fileFolder)
        {
            bool status = false;
            string filePath = thePage.Server.MapPath(string.Format("~/fileUpload/{0}/{1}.pdf", fileFolder, fileName));
            try
            {
                File.Delete(filePath);
                status = true;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }

            return status;
        }

        #endregion
        
    }
}