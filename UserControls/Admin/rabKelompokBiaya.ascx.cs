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
    public partial class rabKelompokBiaya : System.Web.UI.UserControl
    {
        daftarRabKelompokBiaya modelRabKelompokBiaya = new daftarRabKelompokBiaya();

        uiGridView obj_uiGridView = new uiGridView();
        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiModal objModal = new uiModal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            if (!IsPostBack)
            {
                isiJenisKegiatan(ddlJenisKegiatan);
                isiJenisKegiatan(ddlJenisKegiatanAdd);
                isiGvDaftarRabKelompokBiaya();
            }
        }

        private void isiJenisKegiatan(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            modelRabKelompokBiaya.getJenisKegiatan(ref dt);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, dt, "jenis_kegiatan", "kd_jenis_kegiatan");
        }

        protected void ddlJenisKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarRabKelompokBiaya();
        }

        protected void ddlJmlBarisKelompokBiaya_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarRabKelompokBiaya();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarRabKelompokBiaya.setPaging(int.Parse(ddlJmlBarisKelompokBiaya.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarRabKelompokBiaya.setPaging(1, 1);
            pagingDaftarRabKelompokBiaya.refreshPaging();
        }

        protected void lbSimpanRabKelompokBiaya_Click(object sender, EventArgs e)
        {
            if (modelRabKelompokBiaya.insertRabKelompokBiaya(ddlJenisKegiatanAdd.SelectedValue, tbKelompokBiaya.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah RAB kelompok biaya berhasil");
                isiGvDaftarRabKelompokBiaya();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah RAB kelompok biaya gagal " + modelRabKelompokBiaya.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            ddlJenisKegiatanAdd.SelectedIndex = 0;
            tbKelompokBiaya.Text = "";
        }

        private void isiGvDaftarRabKelompokBiaya()
        {
            var dt = new DataTable();
            modelRabKelompokBiaya.currentPage = pagingDaftarRabKelompokBiaya.currentPage;
            modelRabKelompokBiaya.rowsPerPage = Int32.Parse(ddlJmlBarisKelompokBiaya.SelectedValue);
            if (modelRabKelompokBiaya.listRabKelompokBiaya(ref dt, ddlJenisKegiatan.SelectedValue))
            {
                gvDaftarRabKelompokBiaya.DataSource = dt;
                gvDaftarRabKelompokBiaya.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarRabKelompokBiaya_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoDaftarPenugasan = (Label)e.Row.FindControl("lblNoDaftarKelompokBiaya");
                lblNoDaftarPenugasan.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisKelompokBiaya.SelectedValue) * (pagingDaftarRabKelompokBiaya.currentPage)).ToString();
            }
        }

        protected void gvDaftarRabKelompokBiaya_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdRabKelompokBiaya"] = Int32.Parse(gvDaftarRabKelompokBiaya.DataKeys[e.RowIndex]["id_rab_kelompok_biaya"].ToString());
            lblKelompokBiaya.Text = gvDaftarRabKelompokBiaya.DataKeys[e.RowIndex]["kelompok_biaya"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarRabKelompokBiaya_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarRabKelompokBiaya.EditIndex = e.NewEditIndex;
            isiGvDaftarRabKelompokBiaya();
        }

        protected void gvDaftarRabKelompokBiaya_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarRabKelompokBiaya.EditIndex = -1;
            isiGvDaftarRabKelompokBiaya();
        }

        protected void gvDaftarRabKelompokBiaya_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox kelompok_biaya = gvDaftarRabKelompokBiaya.Rows[e.RowIndex].FindControl("tbKelompokBiayaEdit") as TextBox;
            if (!modelRabKelompokBiaya.updateRabKelompokBiaya(Int32.Parse(gvDaftarRabKelompokBiaya.DataKeys[e.RowIndex]["id_rab_kelompok_biaya"].ToString()), ddlJenisKegiatan.SelectedValue, kelompok_biaya.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarRabKelompokBiaya.EditIndex = -1;
            isiGvDaftarRabKelompokBiaya();
        }

        protected void lbHapusRabKelompokBiaya_Click(object sender, EventArgs e)
        {
            if (modelRabKelompokBiaya.deleteRabKelompokBiaya(Int32.Parse(ViewState["IdRabKelompokBiaya"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus RAB kelompok biaya berhasil");
                isiGvDaftarRabKelompokBiaya();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus RAB kelompok biaya gagal " + modelRabKelompokBiaya.errorMessage);
            }
        }

        protected void daftarRabKelompokBiaya_PageChanging(object sender, EventArgs e)
        {
            modelRabKelompokBiaya.currentPage = pagingDaftarRabKelompokBiaya.currentPage;
            modelRabKelompokBiaya.rowsPerPage = int.Parse(ddlJmlBarisKelompokBiaya.SelectedValue);

            if (!modelRabKelompokBiaya.getDaftarRabKelompokBiaya(ddlJenisKegiatan.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarRabKelompokBiaya.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarRabKelompokBiaya, modelRabKelompokBiaya.currentRecords);
        }
    }
}