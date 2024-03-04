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
    public partial class peranPenulis : System.Web.UI.UserControl
    {
        daftarPeranPenulis modelPeranPenulis = new daftarPeranPenulis();

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
                isiGvDaftarPeranPenulis();
            }
        }

        protected void ddlJmlBarisPeranPenulis_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarPeranPenulis();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarPeranPenulis.setPaging(int.Parse(ddlJmlBarisPeranPenulis.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarPeranPenulis.setPaging(1, 1);
            pagingDaftarPeranPenulis.refreshPaging();
        }

        protected void lbSimpanPeranPenulis_Click(object sender, EventArgs e)
        {
            if (modelPeranPenulis.insertPeranPenulis(tbKdPeranPenulisAdd.Text, tbPeranPenulisAdd.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Peran Penulis Berhasil");
                isiGvDaftarPeranPenulis();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Peran Penulis Gagal " + modelPeranPenulis.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            tbKdPeranPenulisAdd.Text = "";
            tbPeranPenulisAdd.Text = "";
        }

        private void isiGvDaftarPeranPenulis()
        {
            var dt = new DataTable();
            modelPeranPenulis.currentPage = pagingDaftarPeranPenulis.currentPage;
            modelPeranPenulis.rowsPerPage = Int32.Parse(ddlJmlBarisPeranPenulis.SelectedValue);
            if (modelPeranPenulis.listPeranPenulis(ref dt))
            {
                gvDaftarPeranPenulis.DataSource = dt;
                gvDaftarPeranPenulis.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarPeranPenulis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoDaftarPeranPenulis = (Label)e.Row.FindControl("lblNoDaftarPeranPenulis");
                lblNoDaftarPeranPenulis.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisPeranPenulis.SelectedValue) * (pagingDaftarPeranPenulis.currentPage)).ToString();
            }
        }

        protected void gvDaftarPeranPenulis_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["KdPeranPenulis"] = gvDaftarPeranPenulis.DataKeys[e.RowIndex]["kd_peran_penulis"].ToString();
            lblPeranPenulis.Text = gvDaftarPeranPenulis.DataKeys[e.RowIndex]["peran_penulis"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarPeranPenulis_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarPeranPenulis.EditIndex = e.NewEditIndex;
            isiGvDaftarPeranPenulis();
        }

        protected void gvDaftarPeranPenulis_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarPeranPenulis.EditIndex = -1;
            isiGvDaftarPeranPenulis();
        }

        protected void gvDaftarPeranPenulis_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox PeranPenulis = gvDaftarPeranPenulis.Rows[e.RowIndex].FindControl("tbPeranPenulisEdit") as TextBox;
            if (!modelPeranPenulis.updatePeranPenulis(gvDaftarPeranPenulis.DataKeys[e.RowIndex]["kd_peran_penulis"].ToString(), PeranPenulis.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarPeranPenulis.EditIndex = -1;
            isiGvDaftarPeranPenulis();
        }

        protected void lbHapusPeranPenulis_Click(object sender, EventArgs e)
        {
            if (modelPeranPenulis.deletePeranPenulis(ViewState["KdPeranPenulis"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Peran Penulis Berhasil");
                isiGvDaftarPeranPenulis();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Peran Penulis Gagal " + modelPeranPenulis.errorMessage);
            }
        }

        protected void daftarPeranPenulis_PageChanging(object sender, EventArgs e)
        {
            modelPeranPenulis.currentPage = pagingDaftarPeranPenulis.currentPage;
            modelPeranPenulis.rowsPerPage = int.Parse(ddlJmlBarisPeranPenulis.SelectedValue);
            if (!modelPeranPenulis.getDaftarPeranPenulis())
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarPeranPenulis.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarPeranPenulis, modelPeranPenulis.currentRecords);
        }
    }
}