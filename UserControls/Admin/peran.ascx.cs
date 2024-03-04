using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Admin
{
    public partial class peran : System.Web.UI.UserControl
    {
        Models.Admin.daftarPeran modelData = new Models.Admin.daftarPeran();

        uiGridView obj_uiGridView = new uiGridView();
        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiModal objModal = new uiModal();
        uiListBox uiListBox = new uiListBox();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            if (!IsPostBack)
            {
                isiKelompokPeran(this.ddlAddKelompokPeran);
                isiGvDaftarData();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "$('.select2').select2()", true);
        }

        private void isiKelompokPeran(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelData.getListKelompokPeran(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "kelompok_peran", "kd_kelompok_peran");
            ddl.SelectedIndex = 0;
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarData.setPaging(int.Parse(ddlJmlBarisData.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarData.setPaging(1, 1);
            pagingDaftarData.refreshPaging();
        }

        private void isiGvDaftarData()
        {
            var dt = new DataTable();
            modelData.currentPage = pagingDaftarData.currentPage;
            modelData.rowsPerPage = Int32.Parse(ddlJmlBarisData.SelectedValue);
            if (modelData.listData(ref dt, tbPencarian.Text))
            {
                gvDaftarData.DataSource = dt;
                gvDaftarData.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNodaftarData = (Label)e.Row.FindControl("lblNoDaftarData");
                lblNodaftarData.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisData.SelectedValue) * (pagingDaftarData.currentPage)).ToString();
            }
        }

        protected void gvDaftarData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdPeran"] = (Int32)gvDaftarData.DataKeys[e.RowIndex]["id_peran"];
            lblData.Text = gvDaftarData.DataKeys[e.RowIndex]["nama_peran"].ToString() + " Pada Kelompok Peran " + gvDaftarData.DataKeys[e.RowIndex]["kelompok_peran"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["IdPeran"] = gvDaftarData.DataKeys[e.NewEditIndex]["id_peran"].ToString();
            tbAddNamaPeran.Text = gvDaftarData.DataKeys[e.NewEditIndex]["nama_peran"].ToString();
            tbKeterangan.Text = gvDaftarData.DataKeys[e.NewEditIndex]["keterangan"].ToString();
            ddlAddKelompokPeran.ClearSelection();
            ddlAddKelompokPeran.SelectedValue = gvDaftarData.DataKeys[e.NewEditIndex]["kd_kelompok_peran"].ToString();
            lbSaveData.Text = "<i class='fas fa-save mr-2'></i>Ubah Data";
            mvDaftarReviewer.SetActiveView(vInsupData);
        }

        protected void gvDaftarData_RowCommand(Object sender, GridViewCommandEventArgs e)
        {

        }

        protected void lbHapusData_Click(object sender, EventArgs e)
        {
            if (modelData.deleteData(Int32.Parse(ViewState["IdPeran"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Data Berhasil");
                isiGvDaftarData();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Data Gagal " + modelData.errorMessage);
            }
        }

        protected void daftarData_PageChanging(object sender, EventArgs e)
        {
            modelData.currentPage = pagingDaftarData.currentPage;
            modelData.rowsPerPage = int.Parse(ddlJmlBarisData.SelectedValue);
            if (!modelData.getDaftarData(tbPencarian.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarData.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarData, modelData.currentRecords);
        }

        protected void lbTambahData_Click(object sender, EventArgs e)
        {
            resetForm();
            mvDaftarReviewer.SetActiveView(vInsupData);
        }

        protected void lbSimpanData_Click(object sender, EventArgs e)
        {
            if (tbAddNamaPeran.Text == "" || tbKeterangan.Text == "")
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pastikan semua data terisi");
            else
            {
                if (Session["IdPeran"].ToString().Equals("-1"))
                {
                    if (modelData.insertData(tbAddNamaPeran.Text, tbKeterangan.Text, ddlAddKelompokPeran.SelectedValue))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Penambahan Data Berhasil");
                        isiGvDaftarData();
                        mvDaftarReviewer.SetActiveView(vListData);
                        resetForm();
                    }
                    else
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", modelData.errorMessage);
                }
                else
                {
                    if (modelData.updateData(Int32.Parse(Session["IdPeran"].ToString()), tbAddNamaPeran.Text, tbKeterangan.Text, ddlAddKelompokPeran.SelectedValue))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Perubahan Data Berhasil");
                        isiGvDaftarData();
                        mvDaftarReviewer.SetActiveView(vListData);
                        resetForm();
                    }
                    else
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", modelData.errorMessage);
                }
            }
        }

        private void resetForm()
        {
            Session["IdPeran"] = "-1";
            lbSaveData.Text = "<i class='fas fa-save mr-2'></i>Simpan Data";
            this.tbKeterangan.Text = "";
            this.tbAddNamaPeran.Text = "";
            this.ddlAddKelompokPeran.ClearSelection();
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
            mvDaftarReviewer.SetActiveView(vListData);
        }

        protected void lbCariData_Click(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }
    }
}