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
    public partial class buktiLuaran : System.Web.UI.UserControl
    {
        daftarBuktiLuaran modelBuktiLuaran = new daftarBuktiLuaran();

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
                isiGvDaftarBuktiLuaran();
            }
        }

        protected void ddlJmlBarisBuktiLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarBuktiLuaran();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarBuktiLuaran.setPaging(int.Parse(ddlJmlBarisBuktiLuaran.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarBuktiLuaran.setPaging(1, 1);
            pagingDaftarBuktiLuaran.refreshPaging();
        }

        protected void lbSimpanBuktiLuaran_Click(object sender, EventArgs e)
        {
            string is_luaran_tahun_akhir = "0";
            if (cbIsLuaranAkhirTahunAdd.Checked == true)
                is_luaran_tahun_akhir = "1";
            if (modelBuktiLuaran.insertBuktiLuaran(tbBuktiLuaranAdd.Text, is_luaran_tahun_akhir))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Bukti Luaran Berhasil");
                isiGvDaftarBuktiLuaran();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Bukti Luaran Gagal " + modelBuktiLuaran.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            tbBuktiLuaranAdd.Text = "";
            cbIsLuaranAkhirTahunAdd.Checked = false;
        }

        private void isiGvDaftarBuktiLuaran()
        {
            var dt = new DataTable();
            modelBuktiLuaran.currentPage = pagingDaftarBuktiLuaran.currentPage;
            modelBuktiLuaran.rowsPerPage = Int32.Parse(ddlJmlBarisBuktiLuaran.SelectedValue);
            if (modelBuktiLuaran.listBuktiLuaran(ref dt))
            {
                gvDaftarBuktiLuaran.DataSource = dt;
                gvDaftarBuktiLuaran.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarBuktiLuaran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                CheckBox cbIsLuaranTahunAkhir = e.Row.FindControl("cbIsLuaranThnAkhirEdit") as CheckBox;
                string is_luaran_tahun_akhir = gvDaftarBuktiLuaran.DataKeys[e.Row.RowIndex]["is_luaran_th_akhir"].ToString();
                if (is_luaran_tahun_akhir == "1")
                    cbIsLuaranTahunAkhir.Checked = true;
                else
                    cbIsLuaranTahunAkhir.Checked = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string is_luaran_tahun_akhir = gvDaftarBuktiLuaran.DataKeys[e.Row.RowIndex]["is_luaran_th_akhir"].ToString();
                Label lblStatusLuaranAkhir = (Label)e.Row.FindControl("lblStatusLuaranAkhir");
                string isLuaranThnAkhir = "Luaran Tahun Akhir";
                if (is_luaran_tahun_akhir == "0")
                    isLuaranThnAkhir = "Bukan Luaran Tahun Akhir";
                if (lblStatusLuaranAkhir != null)
                    lblStatusLuaranAkhir.Text = isLuaranThnAkhir;

                Label lblNoDaftarBuktiLuaran = (Label)e.Row.FindControl("lblNoDaftarBuktiLuaran");
                lblNoDaftarBuktiLuaran.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisBuktiLuaran.SelectedValue) * (pagingDaftarBuktiLuaran.currentPage)).ToString();
            }
        }

        protected void gvDaftarBuktiLuaran_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdBuktiLuaran"] = gvDaftarBuktiLuaran.DataKeys[e.RowIndex]["id_bukti_luaran"].ToString();
            lblBuktiLuaran.Text = gvDaftarBuktiLuaran.DataKeys[e.RowIndex]["bukti_luaran"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarBuktiLuaran_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarBuktiLuaran.EditIndex = e.NewEditIndex;
            isiGvDaftarBuktiLuaran();
        }

        protected void gvDaftarBuktiLuaran_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarBuktiLuaran.EditIndex = -1;
            isiGvDaftarBuktiLuaran();
        }

        protected void gvDaftarBuktiLuaran_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox BuktiLuaran = gvDaftarBuktiLuaran.Rows[e.RowIndex].FindControl("tbBuktiLuaranEdit") as TextBox;
            CheckBox IsLuaranTahunAkhir = gvDaftarBuktiLuaran.Rows[e.RowIndex].FindControl("cbIsLuaranThnAkhirEdit") as CheckBox;
            string is_luaran_tahun_akhir = "0";
            if (IsLuaranTahunAkhir.Checked)
                is_luaran_tahun_akhir = "1";
            if (!modelBuktiLuaran.updateBuktiLuaran((int)gvDaftarBuktiLuaran.DataKeys[e.RowIndex]["id_bukti_luaran"], BuktiLuaran.Text, is_luaran_tahun_akhir))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarBuktiLuaran.EditIndex = -1;
            isiGvDaftarBuktiLuaran();
        }

        protected void lbHapusBuktiLuaran_Click(object sender, EventArgs e)
        {
            if (modelBuktiLuaran.deleteBuktiLuaran(int.Parse(ViewState["IdBuktiLuaran"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Bukti Luaran Berhasil");
                isiGvDaftarBuktiLuaran();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Bukti Luaran Gagal " + modelBuktiLuaran.errorMessage);
            }
        }

        protected void daftarBuktiLuaran_PageChanging(object sender, EventArgs e)
        {
            modelBuktiLuaran.currentPage = pagingDaftarBuktiLuaran.currentPage;
            modelBuktiLuaran.rowsPerPage = int.Parse(ddlJmlBarisBuktiLuaran.SelectedValue);
            if (!modelBuktiLuaran.getDaftarBuktiLuaran())
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarBuktiLuaran.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarBuktiLuaran, modelBuktiLuaran.currentRecords);
        }
    }
}