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
    public partial class jenisLuaran : System.Web.UI.UserControl
    {
        daftarJenisLuaran modelJenisLuaran = new daftarJenisLuaran();

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
                isiKategoriJenisLuaran(ddlKategoriJenisLuaran);
                isiKategoriJenisLuaran(ddlKategoriJenisLuaranAdd);
                isiGvDaftarJenisLuaran();
            }
        }

        private void isiKategoriJenisLuaran(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            modelJenisLuaran.getKategoriJenisLuaran(ref dt);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, dt, "nama_kategori_jenis_luaran", "kd_kategori_jenis_luaran");
            ddl.SelectedIndex = 0;
        }

        protected void ddlJmlBarisJenisLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarJenisLuaran();
        }

        protected void ddlKategoriJenisLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarJenisLuaran();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarJenisLuaran.setPaging(int.Parse(ddlJmlBarisJenisLuaran.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarJenisLuaran.setPaging(1, 1);
            pagingDaftarJenisLuaran.refreshPaging();
        }

        protected void lbSimpanJenisLuaran_Click(object sender, EventArgs e)
        {
            if (modelJenisLuaran.insertJenisLuaran(ddlKategoriJenisLuaranAdd.SelectedValue, tbNamaJenisLuaranAdd.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Jenis Luaran Berhasil");
                isiGvDaftarJenisLuaran();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Jenis Luaran Gagal " + modelJenisLuaran.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            ddlKategoriJenisLuaranAdd.ClearSelection();
            tbNamaJenisLuaranAdd.Text = "";
        }

        private void isiGvDaftarJenisLuaran()
        {
            var dt = new DataTable();
            modelJenisLuaran.currentPage = pagingDaftarJenisLuaran.currentPage;
            modelJenisLuaran.rowsPerPage = Int32.Parse(ddlJmlBarisJenisLuaran.SelectedValue);
            if (modelJenisLuaran.listJenisLuaran(ref dt, ddlKategoriJenisLuaran.SelectedValue))
            {
                gvDaftarJenisLuaran.DataSource = dt;
                gvDaftarJenisLuaran.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarJenisLuaran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                DropDownList kdKategoriJenisLuaran = e.Row.FindControl("ddlKdKategoriJenisLuaranEdit") as DropDownList;
                int id_jenis_luaran = (int) gvDaftarJenisLuaran.DataKeys[e.Row.RowIndex]["id_jenis_luaran"];
                // Bind Skema
                DataTable dt = new DataTable();
                modelJenisLuaran.getKategoriJenisLuaran(ref dt);
                obj_uiDropdownlist.bindToDropDownList(ref kdKategoriJenisLuaran, dt, "nama_kategori_jenis_luaran", "kd_kategori_jenis_luaran");
                foreach (ListItem item in kdKategoriJenisLuaran.Items)
                {
                    if (gvDaftarJenisLuaran.DataKeys[e.Row.RowIndex]["kd_kategori_jenis_luaran"].ToString() == item.Value)
                        item.Selected = true;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoDaftarJenisLuaran = (Label)e.Row.FindControl("lblNoDaftarJenisLuaran");
                lblNoDaftarJenisLuaran.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisJenisLuaran.SelectedValue) * (pagingDaftarJenisLuaran.currentPage)).ToString();
            }
        }

        protected void gvDaftarJenisLuaran_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdJenisLuaran"] = (int) gvDaftarJenisLuaran.DataKeys[e.RowIndex]["id_jenis_luaran"];
            lblJenisLuaran.Text = gvDaftarJenisLuaran.DataKeys[e.RowIndex]["nama_jenis_luaran"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarJenisLuaran_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarJenisLuaran.EditIndex = e.NewEditIndex;
            isiGvDaftarJenisLuaran();
        }

        protected void gvDaftarJenisLuaran_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarJenisLuaran.EditIndex = -1;
            isiGvDaftarJenisLuaran();
        }

        protected void gvDaftarJenisLuaran_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox namaJenisLuaran = gvDaftarJenisLuaran.Rows[e.RowIndex].FindControl("tbNamaJenisLuaranEdit") as TextBox;
            DropDownList kdKategoriJenisLuaran = gvDaftarJenisLuaran.Rows[e.RowIndex].FindControl("ddlKdKategoriJenisLuaranEdit") as DropDownList;
            if (!modelJenisLuaran.updateJenisLuaran( (int) gvDaftarJenisLuaran.DataKeys[e.RowIndex]["id_jenis_luaran"], kdKategoriJenisLuaran.SelectedValue, namaJenisLuaran.Text, gvDaftarJenisLuaran.DataKeys[e.RowIndex]["kd_sts_aktif_jenis_luaran"].ToString()))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarJenisLuaran.EditIndex = -1;
            isiGvDaftarJenisLuaran();
        }

        protected void lbHapusJenisLuaran_Click(object sender, EventArgs e)
        {
            if (modelJenisLuaran.deleteJenisLuaran((int) ViewState["IdJenisLuaran"]))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Jenis Luaran Berhasil");
                isiGvDaftarJenisLuaran();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Kategori Mitra Gagal " + modelJenisLuaran.errorMessage);
            }
        }

        protected void daftarJenisLuaran_PageChanging(object sender, EventArgs e)
        {
            modelJenisLuaran.currentPage = pagingDaftarJenisLuaran.currentPage;
            modelJenisLuaran.rowsPerPage = int.Parse(ddlJmlBarisJenisLuaran.SelectedValue);
            if (!modelJenisLuaran.getDaftarJenisLuaran(ddlKategoriJenisLuaran.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarJenisLuaran.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarJenisLuaran, modelJenisLuaran.currentRecords);
        }
    }
}