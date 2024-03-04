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
    public partial class mitraWajib : System.Web.UI.UserControl
    {
        daftarMitraWajib modelMitraWajib = new daftarMitraWajib();

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
                isiNamaSkema(ddlNamaSkema);
                isiKategoriMitra(ddlKategoriMitra);
                isiGvDaftarMitraWajib();
            }
        }

        private void isiNamaSkema(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            modelMitraWajib.getNamaSkema(ref dt);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, dt, "nama_skema", "id_skema");
        }

        private void isiKategoriMitra(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            modelMitraWajib.getKategoriMitra(ref dt);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, dt, "kategori_mitra", "kd_kategori_mitra");
        }

        protected void ddlJmlBarisMitraWajib_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarMitraWajib();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarMitraWajib.setPaging(int.Parse(ddlJmlBarisMitraWajib.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarMitraWajib.setPaging(1, 1);
            pagingDaftarMitraWajib.refreshPaging();
        }

        protected void lbSimpanMitraWajib_Click(object sender, EventArgs e)
        {
            if (modelMitraWajib.insertMitraWajib(Int32.Parse(ddlNamaSkema.SelectedValue), int.Parse(ddlKategoriMitra.SelectedValue)))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Mitra Wajib Berhasil");
                isiGvDaftarMitraWajib();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Mitra Wajib Gagal " + modelMitraWajib.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            ddlNamaSkema.ClearSelection();
            ddlKategoriMitra.ClearSelection();
        }

        private void isiGvDaftarMitraWajib()
        {
            var dt = new DataTable();
            modelMitraWajib.currentPage = pagingDaftarMitraWajib.currentPage;
            modelMitraWajib.rowsPerPage = Int32.Parse(ddlJmlBarisMitraWajib.SelectedValue);
            if (modelMitraWajib.listMitraWajib(ref dt))
            {
                gvDaftarMitraWajib.DataSource = dt;
                gvDaftarMitraWajib.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarMitraWajib_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                DropDownList idSkema = e.Row.FindControl("ddlIdSkemaEdit") as DropDownList;
                DropDownList kdKategoriMitra = e.Row.FindControl("ddlKategoriMitraEdit") as DropDownList;
                int id_skema_wajib = (int)gvDaftarMitraWajib.DataKeys[e.Row.RowIndex]["id_skema_wajib"];
                // Bind Skema
                DataTable dtSkema = new DataTable();
                modelMitraWajib.getNamaSkema(ref dtSkema);
                obj_uiDropdownlist.bindToDropDownList(ref idSkema, dtSkema, "nama_skema", "id_skema");
                foreach (ListItem item in idSkema.Items)
                {
                    if ((int)gvDaftarMitraWajib.DataKeys[e.Row.RowIndex]["id_skema"] == int.Parse(item.Value))
                        item.Selected = true;
                }
                // Bind Kategori
                DataTable dtKategoriMitra = new DataTable();
                modelMitraWajib.getKategoriMitra(ref dtKategoriMitra);
                obj_uiDropdownlist.bindToDropDownList(ref kdKategoriMitra, dtKategoriMitra, "kategori_mitra", "kd_kategori_mitra");
                foreach (ListItem item in kdKategoriMitra.Items)
                {
                    if ((int)gvDaftarMitraWajib.DataKeys[e.Row.RowIndex]["kd_kategori_mitra"] == int.Parse(item.Value))
                        item.Selected = true;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoDaftarMitraWajib = (Label)e.Row.FindControl("lblNoDaftarMitraWajib");
                lblNoDaftarMitraWajib.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisMitraWajib.SelectedValue) * (pagingDaftarMitraWajib.currentPage)).ToString();
            }
        }

        protected void gvDaftarMitraWajib_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdSkemaWajib"] = Int32.Parse(gvDaftarMitraWajib.DataKeys[e.RowIndex]["id_skema_wajib"].ToString());
            lblMitraWajib.Text = "ini";
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarMitraWajib_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarMitraWajib.EditIndex = e.NewEditIndex;
            isiGvDaftarMitraWajib();
        }

        protected void gvDaftarMitraWajib_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarMitraWajib.EditIndex = -1;
            isiGvDaftarMitraWajib();
        }

        protected void gvDaftarMitraWajib_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList idSkema = gvDaftarMitraWajib.Rows[e.RowIndex].FindControl("ddlIdSkemaEdit") as DropDownList;
            DropDownList kdKategoriMitra = gvDaftarMitraWajib.Rows[e.RowIndex].FindControl("ddlKategoriMitraEdit") as DropDownList;
            if (!modelMitraWajib.updateMitraWajib((int)gvDaftarMitraWajib.DataKeys[e.RowIndex]["id_skema_wajib"], int.Parse(idSkema.SelectedValue), int.Parse(kdKategoriMitra.SelectedValue)))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarMitraWajib.EditIndex = -1;
            isiGvDaftarMitraWajib();
        }

        protected void lbHapusMitraWajib_Click(object sender, EventArgs e)
        {
            if (modelMitraWajib.deleteMitraWajib(Int32.Parse(ViewState["IdSkemaWajib"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Mitra Wajib Berhasil");
                isiGvDaftarMitraWajib();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Mitra Wajib Gagal " + modelMitraWajib.errorMessage);
            }
        }

        protected void daftarMitraWajib_PageChanging(object sender, EventArgs e)
        {
            modelMitraWajib.currentPage = pagingDaftarMitraWajib.currentPage;
            modelMitraWajib.rowsPerPage = int.Parse(ddlJmlBarisMitraWajib.SelectedValue);
            if (!modelMitraWajib.getDaftarMitraWajib())
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarMitraWajib.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarMitraWajib, modelMitraWajib.currentRecords);
        }
    }
}