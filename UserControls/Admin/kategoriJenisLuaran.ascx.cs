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
    public partial class kategoriJenisLuaran : System.Web.UI.UserControl
    {
        daftarKategoriJenisLuaran modelKategoriJenisLuaran = new daftarKategoriJenisLuaran();

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
                isiStatusAktif(ddlStatusData);
                isiGvDaftarKategoriJenisLuaran();
            }
        }

        private void isiStatusAktif(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            modelKategoriJenisLuaran.getStatusAktif(ref dt);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, dt, "status_aktif", "kd_status_aktif");
            ddl.SelectedIndex = 0;
        }

        protected void ddlJmlBarisKategoriJenisLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarKategoriJenisLuaran();
        }

        protected void ddlStatusData_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarKategoriJenisLuaran();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarKategoriJenisLuaran.setPaging(int.Parse(ddlJmlBarisKategoriJenisLuaran.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarKategoriJenisLuaran.setPaging(1, 1);
            pagingDaftarKategoriJenisLuaran.refreshPaging();
        }

        protected void lbSimpanKategoriJenisLuaran_Click(object sender, EventArgs e)
        {
            if (modelKategoriJenisLuaran.insertKategoriJenisLuaran(tbKdKategoriJenisLuaranAdd.Text, tbKategoriJenisLuaran.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Kategori Jenis Luaran Berhasil");
                isiGvDaftarKategoriJenisLuaran();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Kategori Jenis Luaran Gagal " + modelKategoriJenisLuaran.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            tbKdKategoriJenisLuaranAdd.Text = "";
            tbKdKategoriJenisLuaranAdd.Text = "";
        }

        private void isiGvDaftarKategoriJenisLuaran()
        {
            var dt = new DataTable();
            modelKategoriJenisLuaran.currentPage = pagingDaftarKategoriJenisLuaran.currentPage;
            modelKategoriJenisLuaran.rowsPerPage = Int32.Parse(ddlJmlBarisKategoriJenisLuaran.SelectedValue);
            if (modelKategoriJenisLuaran.listKategoriJenisLuaran(ref dt, ddlStatusData.SelectedValue))
            {
                gvDaftarKategoriJenisLuaran.DataSource = dt;
                gvDaftarKategoriJenisLuaran.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarKategoriJenisLuaran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                DropDownList idStatusData = e.Row.FindControl("ddlStatusDataEdit") as DropDownList;
                string kd_katagori_jenis_luaran = gvDaftarKategoriJenisLuaran.DataKeys[e.Row.RowIndex]["kd_kategori_jenis_luaran"].ToString();
                // Bind Skema
                DataTable dt = new DataTable();
                modelKategoriJenisLuaran.getStatusAktif(ref dt);
                obj_uiDropdownlist.bindToDropDownList(ref idStatusData, dt, "status_aktif", "kd_status_aktif");
                foreach (ListItem item in idStatusData.Items)
                {
                    if (gvDaftarKategoriJenisLuaran.DataKeys[e.Row.RowIndex]["kd_sts_aktif"].ToString() == item.Value)
                        item.Selected = true;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoDaftarKategoriJenisLuaran = (Label)e.Row.FindControl("lblNoDaftarKategoriJenisLuaran");
                lblNoDaftarKategoriJenisLuaran.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisKategoriJenisLuaran.SelectedValue) * (pagingDaftarKategoriJenisLuaran.currentPage)).ToString();
            }
        }

        protected void gvDaftarKategoriJenisLuaran_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["KdKategoriJenisLuaran"] = gvDaftarKategoriJenisLuaran.DataKeys[e.RowIndex]["kd_kategori_jenis_luaran"].ToString();
            lblKategoriJenisLuaran.Text = gvDaftarKategoriJenisLuaran.DataKeys[e.RowIndex]["nama_kategori_jenis_luaran"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarKategoriJenisLuaran_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarKategoriJenisLuaran.EditIndex = e.NewEditIndex;
            isiGvDaftarKategoriJenisLuaran();
        }

        protected void gvDaftarKategoriJenisLuaran_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarKategoriJenisLuaran.EditIndex = -1;
            isiGvDaftarKategoriJenisLuaran();
        }

        protected void gvDaftarKategoriJenisLuaran_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox kategoriJenisLuaran = gvDaftarKategoriJenisLuaran.Rows[e.RowIndex].FindControl("tbKategoriJenisLuaranEdit") as TextBox;
            DropDownList kdStsAktif = gvDaftarKategoriJenisLuaran.Rows[e.RowIndex].FindControl("ddlStatusDataEdit") as DropDownList;
            if (!modelKategoriJenisLuaran.updateKategoriJenisLuaran(gvDaftarKategoriJenisLuaran.DataKeys[e.RowIndex]["kd_kategori_jenis_luaran"].ToString(), kategoriJenisLuaran.Text, kdStsAktif.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarKategoriJenisLuaran.EditIndex = -1;
            isiGvDaftarKategoriJenisLuaran();
        }

        protected void lbHapusKategoriJenisLuaran_Click(object sender, EventArgs e)
        {
            if (modelKategoriJenisLuaran.deleteKategoriJenisLuaran(ViewState["KdKategoriJenisLuaran"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Kategori Jenis Luaran Berhasil");
                isiGvDaftarKategoriJenisLuaran();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Kategori Mitra Gagal " + modelKategoriJenisLuaran.errorMessage);
            }
        }

        protected void daftarKategoriJenisLuaran_PageChanging(object sender, EventArgs e)
        {
            modelKategoriJenisLuaran.currentPage = pagingDaftarKategoriJenisLuaran.currentPage;
            modelKategoriJenisLuaran.rowsPerPage = int.Parse(ddlJmlBarisKategoriJenisLuaran.SelectedValue);
            if (!modelKategoriJenisLuaran.getDaftarKategoriJenisLuaran(ddlStatusData.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarKategoriJenisLuaran.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarKategoriJenisLuaran, modelKategoriJenisLuaran.currentRecords);
        }
    }
}