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
    public partial class kategoriMitra : System.Web.UI.UserControl
    {
        daftarKategoriMitra modelKategoriMitra = new daftarKategoriMitra();

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
                isiNamaSkema(lbxNamaSkema);
                isiGvDaftarKategoriMitra();
            }
        }

        private void isiNamaSkema(ListBox lbx)
        {
            DataTable dt = new DataTable();
            modelKategoriMitra.getNamaSkema(ref dt);
            uiListBox.bindToListBox(ref lbx, dt, "nama_skema", "id_skema");
        }

        protected void ddlNamaSkema_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarKategoriMitra();
        }

        protected void ddlJmlBarisKategoriMitra_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarKategoriMitra();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarKategoriMitra.setPaging(int.Parse(ddlJmlBarisKategoriMitra.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarKategoriMitra.setPaging(1, 1);
            pagingDaftarKategoriMitra.refreshPaging();
        }

        protected void lbSimpanKategoriMitra_Click(object sender, EventArgs e)
        {
            List<int> id_kategori = new List<int>();
            foreach (ListItem item in lbxNamaSkema.Items)
            {
                if (item.Selected)
                    id_kategori.Add(int.Parse(item.Value));
            }
            if (modelKategoriMitra.insertKategoriMitra(tbKategoriMitra.Text, id_kategori.ToArray()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Kategori Mitra Berhasil");
                isiGvDaftarKategoriMitra();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Kategori Mitra Gagal " + modelKategoriMitra.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            tbKategoriMitra.Text = "";
            lbxNamaSkema.ClearSelection();
        }

        private void isiGvDaftarKategoriMitra()
        {
            var dt = new DataTable();
            modelKategoriMitra.currentPage = pagingDaftarKategoriMitra.currentPage;
            modelKategoriMitra.rowsPerPage = Int32.Parse(ddlJmlBarisKategoriMitra.SelectedValue);
            if (modelKategoriMitra.listKategoriMitra(ref dt))
            {
                gvDaftarKategoriMitra.DataSource = dt;
                gvDaftarKategoriMitra.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarKategoriMitra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                ListBox idSkema = e.Row.FindControl("lbxIdSkemaEdit") as ListBox;
                int kd_kategori_mitra = (int)gvDaftarKategoriMitra.DataKeys[e.Row.RowIndex]["kd_kategori_mitra"];
                DataTable dt = new DataTable();
                modelKategoriMitra.getNamaSkema(ref dt);
                uiListBox.bindToListBox(ref idSkema, dt, "nama_skema", "id_skema");
                DataTable dtKomponen = new DataTable();
                modelKategoriMitra.getNamaSkema(ref dtKomponen, kd_kategori_mitra);
                int[] kategoriComponent = (int[])dtKomponen.Rows[0]["id_skema"];
                foreach (ListItem item in idSkema.Items)
                {
                    if (Array.Exists(kategoriComponent, value => value == int.Parse(item.Value)))
                        item.Selected = true;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoDaftarKategoriMitra = (Label)e.Row.FindControl("lblNoDaftarKategoriMitra");
                lblNoDaftarKategoriMitra.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisKategoriMitra.SelectedValue) * (pagingDaftarKategoriMitra.currentPage)).ToString();
            }
        }

        protected void gvDaftarKategoriMitra_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["KdKategoriMitra"] = Int32.Parse(gvDaftarKategoriMitra.DataKeys[e.RowIndex]["kd_kategori_mitra"].ToString());
            lblKategoriMitra.Text = gvDaftarKategoriMitra.DataKeys[e.RowIndex]["kategori_mitra"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarKategoriMitra_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarKategoriMitra.EditIndex = e.NewEditIndex;
            isiGvDaftarKategoriMitra();
        }

        protected void gvDaftarKategoriMitra_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarKategoriMitra.EditIndex = -1;
            isiGvDaftarKategoriMitra();
        }

        protected void gvDaftarKategoriMitra_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox kategori_mitra = gvDaftarKategoriMitra.Rows[e.RowIndex].FindControl("tbKategoriMitraEdit") as TextBox;
            ListBox nama_skema = gvDaftarKategoriMitra.Rows[e.RowIndex].FindControl("lbxIdSkemaEdit") as ListBox;
            List<int> id_skema = new List<int>();
            foreach (ListItem item in nama_skema.Items)
            {
                if (item.Selected)
                    id_skema.Add(int.Parse(item.Value));
            }
            if (!modelKategoriMitra.updateKategoriMitra((int)gvDaftarKategoriMitra.DataKeys[e.RowIndex]["kd_kategori_mitra"], kategori_mitra.Text, id_skema.ToArray()))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarKategoriMitra.EditIndex = -1;
            isiGvDaftarKategoriMitra();
        }

        protected void lbHapusKategoriMitra_Click(object sender, EventArgs e)
        {
            if (modelKategoriMitra.deleteKategoriMitra(Int32.Parse(ViewState["KdKategoriMitra"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Kategori Mitra Berhasil");
                isiGvDaftarKategoriMitra();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Kategori Mitra Gagal " + modelKategoriMitra.errorMessage);
            }
        }

        protected void daftarKategoriMitra_PageChanging(object sender, EventArgs e)
        {
            modelKategoriMitra.currentPage = pagingDaftarKategoriMitra.currentPage;
            modelKategoriMitra.rowsPerPage = int.Parse(ddlJmlBarisKategoriMitra.SelectedValue);
            if (!modelKategoriMitra.getDaftarKategoriMitra())
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarKategoriMitra.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarKategoriMitra, modelKategoriMitra.currentRecords);
        }
    }
}