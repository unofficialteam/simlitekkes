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
    public partial class jenisSKDokumen : System.Web.UI.UserControl
    {
        daftarJenisSKDokumen modelJenisSKDokumen = new daftarJenisSKDokumen();

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
                isiGvDaftarJenisSKDokumen();
            }
        }

        protected void ddlJmlBarisJenisSKDokumen_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarJenisSKDokumen();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarJenisSKDokumen.setPaging(int.Parse(ddlJmlBarisJenisSKDokumen.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarJenisSKDokumen.setPaging(1, 1);
            pagingDaftarJenisSKDokumen.refreshPaging();
        }

        protected void lbSimpanJenisSKDokumen_Click(object sender, EventArgs e)
        {
            if (modelJenisSKDokumen.insertJenisSKDokumen(tbKdJenisSKDokumenAdd.Text, tbNamaDokumenAdd.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Jenis SK Dokumen Berhasil");
                isiGvDaftarJenisSKDokumen();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Jenis SK Dokumen Gagal " + modelJenisSKDokumen.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            tbKdJenisSKDokumenAdd.Text = "";
            tbNamaDokumenAdd.Text = "";
        }

        private void isiGvDaftarJenisSKDokumen()
        {
            var dt = new DataTable();
            modelJenisSKDokumen.currentPage = pagingDaftarJenisSKDokumen.currentPage;
            modelJenisSKDokumen.rowsPerPage = Int32.Parse(ddlJmlBarisJenisSKDokumen.SelectedValue);
            if (modelJenisSKDokumen.listJenisSKDokumen(ref dt))
            {
                gvDaftarJenisSKDokumen.DataSource = dt;
                gvDaftarJenisSKDokumen.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarJenisSKDokumen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoDaftarJenisSKDokumen = (Label)e.Row.FindControl("lblNoDaftarJenisSKDokumen");
                lblNoDaftarJenisSKDokumen.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisJenisSKDokumen.SelectedValue) * (pagingDaftarJenisSKDokumen.currentPage)).ToString();
            }
        }

        protected void gvDaftarJenisSKDokumen_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["KdJenisSKDokumen"] = gvDaftarJenisSKDokumen.DataKeys[e.RowIndex]["kd_jenis_sk"].ToString();
            lblJenisSKDokumen.Text = gvDaftarJenisSKDokumen.DataKeys[e.RowIndex]["nama_dokumen"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarJenisSKDokumen_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarJenisSKDokumen.EditIndex = e.NewEditIndex;
            isiGvDaftarJenisSKDokumen();
        }

        protected void gvDaftarJenisSKDokumen_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarJenisSKDokumen.EditIndex = -1;
            isiGvDaftarJenisSKDokumen();
        }

        protected void gvDaftarJenisSKDokumen_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox JenisSKDokumen = gvDaftarJenisSKDokumen.Rows[e.RowIndex].FindControl("tbNamaDokumenEdit") as TextBox;
            if (!modelJenisSKDokumen.updateJenisSKDokumen(gvDaftarJenisSKDokumen.DataKeys[e.RowIndex]["kd_jenis_sk"].ToString(), JenisSKDokumen.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarJenisSKDokumen.EditIndex = -1;
            isiGvDaftarJenisSKDokumen();
        }

        protected void lbHapusJenisSKDokumen_Click(object sender, EventArgs e)
        {
            if (modelJenisSKDokumen.deleteJenisSKDokumen(ViewState["KdJenisSKDokumen"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Jenis SK Dokumen Berhasil");
                isiGvDaftarJenisSKDokumen();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Jenis SK Dokumen Gagal " + modelJenisSKDokumen.errorMessage);
            }
        }

        protected void daftarJenisSKDokumen_PageChanging(object sender, EventArgs e)
        {
            modelJenisSKDokumen.currentPage = pagingDaftarJenisSKDokumen.currentPage;
            modelJenisSKDokumen.rowsPerPage = int.Parse(ddlJmlBarisJenisSKDokumen.SelectedValue);
            if (!modelJenisSKDokumen.getDaftarJenisSKDokumen())
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarJenisSKDokumen.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarJenisSKDokumen, modelJenisSKDokumen.currentRecords);
        }
    }
}