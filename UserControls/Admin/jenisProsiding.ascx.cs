using simlitekkes.Models.Admin;
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
    public partial class jenisProsiding : System.Web.UI.UserControl
    {
        daftarJenisProsiding modelJenisProsiding = new daftarJenisProsiding();

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
                isiGvDaftarJenisProsiding();
            }
        }

        protected void ddlJmlBarisJenisProsiding_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarJenisProsiding();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarJenisProsiding.setPaging(int.Parse(ddlJmlBarisJenisProsiding.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarJenisProsiding.setPaging(1, 1);
            pagingDaftarJenisProsiding.refreshPaging();
        }

        protected void lbSimpanJenisProsiding_Click(object sender, EventArgs e)
        {
            if (modelJenisProsiding.insertJenisProsiding(tbKdJenisProsidingAdd.Text, tbJenisProsidingAdd.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Jenis Prosiding Berhasil");
                isiGvDaftarJenisProsiding();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Jenis Prosiding Gagal " + modelJenisProsiding.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            tbKdJenisProsidingAdd.Text = "";
            tbJenisProsidingAdd.Text = "";
        }

        private void isiGvDaftarJenisProsiding()
        {
            var dt = new DataTable();
            modelJenisProsiding.currentPage = pagingDaftarJenisProsiding.currentPage;
            modelJenisProsiding.rowsPerPage = Int32.Parse(ddlJmlBarisJenisProsiding.SelectedValue);
            if (modelJenisProsiding.listJenisProsiding(ref dt))
            {
                gvDaftarJenisProsiding.DataSource = dt;
                gvDaftarJenisProsiding.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarJenisProsiding_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoDaftarJenisProsiding = (Label)e.Row.FindControl("lblNoDaftarJenisProsiding");
                lblNoDaftarJenisProsiding.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisJenisProsiding.SelectedValue) * (pagingDaftarJenisProsiding.currentPage)).ToString();
            }
        }

        protected void gvDaftarJenisProsiding_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["KdJenisProsiding"] = gvDaftarJenisProsiding.DataKeys[e.RowIndex]["kd_jenis_prosiding"].ToString();
            lblJenisProsiding.Text = gvDaftarJenisProsiding.DataKeys[e.RowIndex]["jenis_prosiding"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarJenisProsiding_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarJenisProsiding.EditIndex = e.NewEditIndex;
            isiGvDaftarJenisProsiding();
        }

        protected void gvDaftarJenisProsiding_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarJenisProsiding.EditIndex = -1;
            isiGvDaftarJenisProsiding();
        }

        protected void gvDaftarJenisProsiding_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox JenisProsiding = gvDaftarJenisProsiding.Rows[e.RowIndex].FindControl("tbJenisProsidingEdit") as TextBox;
            if (!modelJenisProsiding.updateJenisProsiding(gvDaftarJenisProsiding.DataKeys[e.RowIndex]["kd_jenis_prosiding"].ToString(), JenisProsiding.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarJenisProsiding.EditIndex = -1;
            isiGvDaftarJenisProsiding();
        }

        protected void lbHapusJenisProsiding_Click(object sender, EventArgs e)
        {
            if (modelJenisProsiding.deleteJenisProsiding(ViewState["KdJenisProsiding"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Jenis Prosiding Berhasil");
                isiGvDaftarJenisProsiding();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Jenis Prosiding Gagal " + modelJenisProsiding.errorMessage);
            }
        }

        protected void daftarJenisProsiding_PageChanging(object sender, EventArgs e)
        {
            modelJenisProsiding.currentPage = pagingDaftarJenisProsiding.currentPage;
            modelJenisProsiding.rowsPerPage = int.Parse(ddlJmlBarisJenisProsiding.SelectedValue);
            if (!modelJenisProsiding.getDaftarJenisProsiding())
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarJenisProsiding.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarJenisProsiding, modelJenisProsiding.currentRecords);
        }
    }
}