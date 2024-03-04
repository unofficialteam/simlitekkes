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
    public partial class topikUnggulanPt : System.Web.UI.UserControl
    {
        Models.Pengusul.daftarBidangUnggulanPT modelBidUnggulanPt = new Models.Pengusul.daftarBidangUnggulanPT();
        Models.Pengusul.daftarTopikUnggulanPT modelTopikUnggulanPt = new Models.Pengusul.daftarTopikUnggulanPT();
        Models.PT.daftarRenstra modelRenstra = new Models.PT.daftarRenstra();
        UIControllers.uiGridView obj_uiGridView = new UIControllers.uiGridView();
        UIControllers.uiDropdownList obj_uiDropdownlist = new UIControllers.uiDropdownList();
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        Models.login objLogin;

        string topikArgumen = "";

        string[] kolomBidangUnggulanPt = { "id_bidang_unggulan_perguruan_tinggi, bidang_unggulan_perguruan_tinggi, tahun_penetapan, kode_status_aktif" };
        string[] kolomTopikUnggulanPt = { "id_topik_unggulan_perguruan_tinggi", "topik_unggulan_perguruan_tinggi", "kode_status_aktif", "status_aktif_topik", "id_bidang_unggulan_perguruan_tinggi" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            
            if (!IsPostBack)
            {
                isiDdlBidangUnggulan();
                refreshGvBidangUnggulan();
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
                        lbTambahTopikUnggulan.Enabled = true;
                    }
                    break;
                case 40:    // opt. pt Pengabdian
                    {
                        lbTambahTopikUnggulan.Visible = false;
                        lbTambahTopikUnggulan.Enabled = false;
                    }
                    break;
            }
        }

        private void refreshGvBidangUnggulan()
        {
            string id_institusi = objLogin.idInstitusi.ToString();

            Models.Pengusul.daftarBidangUnggulanPT modelTopikUnggulanPt = new Models.Pengusul.daftarBidangUnggulanPT();
            DataTable dtBidang = new DataTable();

            if (modelBidUnggulanPt.getCurrRecords(ref dtBidang, id_institusi))
                dtBidang = modelBidUnggulanPt.currentRecords;
            if (!obj_uiGridView.bindToGridView(ref gvBidangUnggulan, dtBidang, kolomBidangUnggulanPt))
                if (dtBidang.Rows.Count == 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
                };
        }

        protected void gvBidangUnggulan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            GridView aGV = new GridView();
            Panel aPanel = new Panel();
            Label lblStsAktif = new Label();
            
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    aGV = (GridView)e.Row.FindControl("gvTopikUnggulan");
                    if (aGV != null)
                    {
                        topikArgumen = e.Row.RowIndex.ToString() + ";" + drv["id_bidang_unggulan_perguruan_tinggi"].ToString();
                        refreshGvTopikUnggulan(ref aGV, drv["id_bidang_unggulan_perguruan_tinggi"].ToString());
                    }

                    lblStsAktif = (Label)e.Row.FindControl("lblStsAktifBidang");
                    string kode_status_aktif = drv["kode_status_aktif"].ToString();

                    if (kode_status_aktif == "1")
                    {
                        lblStsAktif.Text = "Aktif";
                    }
                    else
                    {
                        lblStsAktif.Text = "Tidak aktif";
                    }
                    break;
            }
        }

        private void refreshGvTopikUnggulan(ref GridView objGridView, string idBidangUnggulanPT)
        {
            Models.Pengusul.daftarTopikUnggulanPT modelTopikUnggulanPt = new Models.Pengusul.daftarTopikUnggulanPT();
            DataTable dtTopikUnggulan = new DataTable();
            if (!modelTopikUnggulanPt.getCurrRecords(ref dtTopikUnggulan, idBidangUnggulanPT))
            {
                lblError.Text = modelTopikUnggulanPt.errorMessage;
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref objGridView, modelTopikUnggulanPt.currentRecords, kolomTopikUnggulanPt))
                lblError.Text = obj_uiGridView.errorMessage;

            dtTopikUnggulan.Dispose();
        }
        
        protected void lbTambahTopikUnggulan_Click(object sender, EventArgs e)
        {
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());
            DataTable dtCekUnggahRenstra = new DataTable();
            if (modelRenstra.cekSudahPernahUnggahSkRip(id_institusi) == true && modelRenstra.cekSudahPernahUnggahDokumenRip(id_institusi) == true)
            {
                ViewState["modeDataBaru"] = true;
                uiMdl.ShowModal(this.Page, "modalInsupTopik");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "SK dan atau Dokumen Renstra belum diunggah");
            }
        }

        private void isiDdlBidangUnggulan()
        {
            ddlBidangUnggulan.Items.Clear();
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());

            DataTable dtDdlBidangUngglanPt = new DataTable();
            Models.Pengusul.daftarBidangUnggulanPT modelBidUnggulanPt = new Models.Pengusul.daftarBidangUnggulanPT();

            if (modelBidUnggulanPt.DaftarBidangUnggulanPtAktif(ref dtDdlBidangUngglanPt, id_institusi))
            {
                dtDdlBidangUngglanPt = modelRenstra.currentRecords;
                
                DataTable dtDdlBidangUngglanPt1 = new DataTable();
                Models.Pengusul.daftarBidangUnggulanPT modelBidUnggulanPt1 = new Models.Pengusul.daftarBidangUnggulanPT();
                if (modelBidUnggulanPt1.DaftarBidangUnggulanPtAktif(ref dtDdlBidangUngglanPt1, id_institusi))
                    dtDdlBidangUngglanPt1 = modelBidUnggulanPt1.currentRecords;

                if (dtDdlBidangUngglanPt1.Rows.Count > 0)
                {
                    string nama_dokumenbidang_unggulan_perguruan_tinggi = dtDdlBidangUngglanPt1.Rows[0]["bidang_unggulan_perguruan_tinggi"].ToString();
                    string id_bidang_unggulan_perguruan_tinggi = dtDdlBidangUngglanPt1.Rows[0]["id_bidang_unggulan_perguruan_tinggi"].ToString();

                    if (!obj_uiDropdownlist.bindToDropDownList(ref ddlBidangUnggulan, dtDdlBidangUngglanPt1, "bidang_unggulan_perguruan_tinggi", "id_bidang_unggulan_perguruan_tinggi"))
                    {
                        lblError.Text = obj_uiDropdownlist.errorMessage;
                        return;
                    }
                    else
                    {
                        ddlBidangUnggulan.Items.Insert(0, new ListItem("--Pilih--", "00000000-0000-0000-0000-000000000000"));
                    }
                }
            }
        }

        protected void lbInsupTopikUnggulan_Click(object sender, EventArgs e)
        {
            if (tbTopikUnggulanPt.Text.Trim() == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Topik unggulan PT belum diisi.");
                return;
            }
            if (bool.Parse(ViewState["modeDataBaru"].ToString()))
            {
                if (ddlBidangUnggulan.SelectedValue != "00000000-0000-0000-0000-000000000000")
                {
                    if (modelTopikUnggulanPt.insertDataBaru(Guid.Parse(ddlBidangUnggulan.SelectedValue), tbTopikUnggulanPt.Text.Trim(), rblStsAktif.SelectedValue))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                        refreshGvBidangUnggulan();
                        tbTopikUnggulanPt.Text = string.Empty;
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal" + modelTopikUnggulanPt.errorMessage);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Bidang unggulan PT harus dipilih");
                }
            }
            else
            {
                if (ddlBidangUnggulan.SelectedValue != "00000000-0000-0000-0000-000000000000")
                {
                    string id_topik_unggulan_perguruan_tinggi = ViewState["id_topik_unggulan_perguruan_tinggi"].ToString();

                    if (modelTopikUnggulanPt.updateData(Guid.Parse(ddlBidangUnggulan.SelectedValue), tbTopikUnggulanPt.Text.Trim(), rblStsAktif.SelectedValue, id_topik_unggulan_perguruan_tinggi))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Update data berhasil");
                        refreshGvBidangUnggulan();
                        tbTopikUnggulanPt.Text = string.Empty;
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Update data gagal" + modelBidUnggulanPt.errorMessage);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Bidang unggulan PT harus dipilih");
                }
            }
        }
        
        protected void gvTopikUnggulan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            LinkButton lb = new LinkButton();
            Panel aPanel = new Panel();
            GridView gv = new GridView();

            
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    lb = (LinkButton)e.Row.FindControl("lbEditTopik");
                    if (lb != null)
                    {
                        string arg = drv["id_topik_unggulan_perguruan_tinggi"].ToString() + "|" + 
                            drv["topik_unggulan_perguruan_tinggi"].ToString() + "|" + drv["kode_status_aktif"].ToString() + "|" +
                            drv["id_bidang_unggulan_perguruan_tinggi"].ToString();
                        lb.CommandArgument = arg;
                    }
                    return;
            }
        }
        
        protected void ddlBidangUnggulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            uiMdl.ShowModal(this.Page, "modalInsupTopik");
        }

        protected void gvTopikUnggulan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lbInsupTopikUnggulan.Text = "Update";
            ViewState["modeDataBaru"] = false;
            
            DataTable dtTopik = new DataTable();
            Models.Pengusul.daftarTopikUnggulanPT modelTopik = new Models.Pengusul.daftarTopikUnggulanPT();

            GridView gv = sender as GridView;
            GridViewRow row = gv.Rows[e.RowIndex];

            string id_topik_unggulan_perguruan_tinggi = gv.DataKeys[e.RowIndex]["id_topik_unggulan_perguruan_tinggi"].ToString();
            ViewState["id_topik_unggulan_perguruan_tinggi"] = id_topik_unggulan_perguruan_tinggi;

            //ViewState["id_topik_unggulan_perguruan_tinggi"] = gv.DataKeys[1].Value.ToString();


            if (modelTopik.getTopikUnggulanPt(ref dtTopik, id_topik_unggulan_perguruan_tinggi))
            {
                if (dtTopik.Rows.Count > 0 && objLogin.idPeran == 6)
                {
                    string kode_status_aktif_bidang = dtTopik.Rows[0]["kode_status_aktif_bidang"].ToString();

                    if (kode_status_aktif_bidang != "0")
                    {
                        tbTopikUnggulanPt.Text = dtTopik.Rows[0]["topik_unggulan_perguruan_tinggi"].ToString();
                        rblStsAktif.SelectedValue = dtTopik.Rows[0]["kode_status_aktif"].ToString();
                        ddlBidangUnggulan.SelectedValue = dtTopik.Rows[0]["id_bidang_unggulan_perguruan_tinggi"].ToString();
                        uiMdl.ShowModal(this.Page, "modalInsupTopik");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Topik Unggulan PT Tidak bisa diupdate, <br />karena Bidang Unggulan tidak aktif");
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data Topik Unggulan PT");
                }
            }
        }
    }
}