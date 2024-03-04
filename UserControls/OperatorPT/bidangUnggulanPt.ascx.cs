using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class bidangUnggulanPt : System.Web.UI.UserControl
    {
        Models.Pengusul.daftarBidangUnggulanPT modelBidUnggulanPt = new Models.Pengusul.daftarBidangUnggulanPT();
        Models.PT.daftarRenstra modelRenstra = new Models.PT.daftarRenstra();
        UIControllers.uiGridView obj_uiGridView = new UIControllers.uiGridView();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        Models.login objLogin;

        string[] namaKolom = { "id_bidang_unggulan_perguruan_tinggi, bidang_unggulan_perguruan_tinggi, tahun_penetapan, kode_status_aktif" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiDdlTahunPenetapan();
                refreshGridView();
                kewenanganOperatorPt();
            }
            else
            {

            }
        }

        private void kewenanganOperatorPt()
        {
            switch (objLogin.idPeran)
            {
                case 6:    // opt. pt penelitian
                    {
                        lbTambahBidang.Enabled = true;
                    }
                    break;
                case 40:    // opt. pt Pengabdian
                    {
                        lbTambahBidang.Visible = false;
                        lbTambahBidang.Enabled = false;
                    }
                    break;
            }
        }

        private void isiDdlTahunPenetapan()
        {
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());

            ddlThnPenetapan.Items.Clear();
            //ddlThnUpload.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            for (int i = thnSKg; i >= 2013; i--)
            {
                ddlThnPenetapan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }

        }

            private void refreshGridView()
        {
            string id_institusi = objLogin.idInstitusi.ToString();

            DataTable dtBidang = new DataTable();
            
                if (modelBidUnggulanPt.getCurrRecords(ref dtBidang, id_institusi))
                    dtBidang = modelBidUnggulanPt.currentRecords;
                if (!obj_uiGridView.bindToGridView(ref gvDaftarBidangUnggulanPt, dtBidang, namaKolom))
                    if (dtBidang.Rows.Count == 0)
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
                    gvDaftarBidangUnggulanPt.DataSource = dtBidang;
                    gvDaftarBidangUnggulanPt.DataBind();
                    };
        }
        
        protected void lbTambahBidang_Click(object sender, EventArgs e)
        {
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());
            DataTable dtCekUnggahRenstra = new DataTable();
            if(modelRenstra.cekSudahPernahUnggahSkRip(id_institusi) == true && modelRenstra.cekSudahPernahUnggahDokumenRip(id_institusi) == true)
            {
                ViewState["modeDataBaru"] = true;
                uiMdl.ShowModal(this.Page, "modalInsupBidang");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "SK dan atau Dokumen Renstra belum diunggah");
            }
        }

        protected void lbInsupBidangUnggulan_Click(object sender, EventArgs e)
        {
            if(tbBidangUnggulanPt.Text.Trim() == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Bidang unggulan PT belum diisi.");
                return;
            }
            if (bool.Parse(ViewState["modeDataBaru"].ToString()))
            {
                if (modelBidUnggulanPt.insertDataBaru(Guid.Parse(objLogin.idInstitusi.ToString()), tbBidangUnggulanPt.Text, ddlThnPenetapan.SelectedValue, rblStsAktif.SelectedValue))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                    refreshGridView();
                    clearTexbox();
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal" + modelBidUnggulanPt.errorMessage);
                }
            }
            else
            {
                Guid id_bidang_unggulan_perguruan_tinggi = Guid.Parse(ViewState["id_bidang_unggulan_perguruan_tinggi"].ToString());

                if (modelBidUnggulanPt.updateData(Guid.Parse(objLogin.idInstitusi.ToString()), tbBidangUnggulanPt.Text, ddlThnPenetapan.SelectedValue, rblStsAktif.SelectedValue, id_bidang_unggulan_perguruan_tinggi))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Update data berhasil");
                    refreshGridView();
                    clearTexbox();
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Update data gagal" + modelBidUnggulanPt.errorMessage);
                }
            }
        }

        private void clearTexbox()
        {
            tbBidangUnggulanPt.Text = string.Empty;
            //tbThnPenetapan.Text = string.Empty;
            ddlThnPenetapan.SelectedIndex = 0;
            rblStsAktif.SelectedValue = string.Empty;
        }

        protected void gvDaftarBidangUnggulanPt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kode_status_aktif = gvDaftarBidangUnggulanPt.DataKeys[e.Row.RowIndex]["kode_status_aktif"].ToString();

                Label lblStsAktif = new Label();
                lblStsAktif = (Label)e.Row.FindControl("lblStsAktif");

                if (kode_status_aktif == "1")
                {
                    lblStsAktif.Text = "Aktif";
                }
                else
                {
                    lblStsAktif.Text = "Tidak aktif";
                }

                LinkButton lbEditBidang = new LinkButton();
                lbEditBidang = (LinkButton)e.Row.FindControl("lbEditBidang");

                if (objLogin.idPeran == 6) // opt. pt penelitian
                {
                    lbEditBidang.Enabled = true;
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Bidang Unggulan PT hanya bisa diedit oleh Operator PT Penelitian");
                    lbEditBidang.Enabled = false;
                }
            }
        }

        protected void gvDaftarBidangUnggulanPt_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lbInsupBidangUnggulan.Text = "Update";
            ViewState["modeDataBaru"] = false;

            ViewState["id_bidang_unggulan_perguruan_tinggi"] = gvDaftarBidangUnggulanPt.DataKeys[e.RowIndex]["id_bidang_unggulan_perguruan_tinggi"].ToString();
            tbBidangUnggulanPt.Text = gvDaftarBidangUnggulanPt.DataKeys[e.RowIndex]["bidang_unggulan_perguruan_tinggi"].ToString();
            string selectedValue = gvDaftarBidangUnggulanPt.DataKeys[e.RowIndex]["tahun_penetapan"].ToString();
            if (ddlThnPenetapan.Items.FindByValue(selectedValue) != null) ddlThnPenetapan.SelectedValue = selectedValue;

            rblStsAktif.SelectedValue = gvDaftarBidangUnggulanPt.DataKeys[e.RowIndex]["kode_status_aktif"].ToString();

            uiMdl.ShowModal(this.Page, "modalInsupBidang");
        }
    }
}
