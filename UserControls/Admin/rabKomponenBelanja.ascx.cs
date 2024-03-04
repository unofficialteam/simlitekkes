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
    public partial class rabKomponenBelanja : System.Web.UI.UserControl
    {
        daftarRabKomponenBelanja modelRabKomponenBelanja = new daftarRabKomponenBelanja();

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
                isiJenisKegiatan(ddlJenisKegiatan);
                isiKelompokBiaya(ddlJenisKegiatan.SelectedValue, ddlRabKelompokBiaya);
                isiJenisKegiatan(ddlJenisKegiatanAdd);
                isiKelompokBiaya(ddlJenisKegiatanAdd.SelectedValue, ddlRabKelompokBiayaAdd);
                isiKategoriPenelitian();
                isiGvDaftarRabKomponenBelanja();
            }
        }

        private void isiJenisKegiatan(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            modelRabKomponenBelanja.getJenisKegiatan(ref dt);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, dt, "jenis_kegiatan", "kd_jenis_kegiatan");
        }

        private void isiKelompokBiaya(string kd_jenis_kegiatan, DropDownList ddl)
        {
            DataTable dt = new DataTable();
            modelRabKomponenBelanja.getKelompokBiaya(ref dt, kd_jenis_kegiatan);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, dt, "kelompok_biaya", "id_rab_kelompok_biaya");
        }

        private void isiKategoriPenelitian()
        {
            DataTable dt = new DataTable();
            modelRabKomponenBelanja.getKategoriPenelitian(ref dt);
            uiListBox.bindToListBox(ref lbxKategoriPenelitian, dt, "kategori_penelitian", "id_kategori_penelitian");
        }

        protected void ddlJenisKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiKelompokBiaya(ddlJenisKegiatan.SelectedValue, ddlRabKelompokBiaya);
            isiGvDaftarRabKomponenBelanja();
        }

        protected void ddlJenisKegiatanAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiKelompokBiaya(ddlJenisKegiatanAdd.SelectedValue, ddlRabKelompokBiayaAdd);
        }

        protected void ddlRabKelompokBiaya_SelectedIndexChanger(object sender, EventArgs e)
        {
            isiGvDaftarRabKomponenBelanja();
        }

        protected void ddlJmlBarisKomponenBelanja_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarRabKomponenBelanja();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarRabKomponenBelanja.setPaging(int.Parse(ddlJmlBarisKomponenBelanja.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarRabKomponenBelanja.setPaging(1, 1);
            pagingDaftarRabKomponenBelanja.refreshPaging();
        }

        protected void lbSimpanRabKomponenBelanja_Click(object sender, EventArgs e)
        {
            List<int> id_kategori = new List<int>();
            foreach (ListItem item in lbxKategoriPenelitian.Items)
            {
                if (item.Selected)
                    id_kategori.Add(Int32.Parse(item.Value));
            }
            if (modelRabKomponenBelanja.insertRabKomponenBelanja(ddlJenisKegiatanAdd.SelectedValue, int.Parse(ddlRabKelompokBiayaAdd.SelectedValue), tbKomponenBelanja.Text, tbSatuan.Text, tbKeterangan.Text, id_kategori.ToArray()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah RAB Komponen Belanja Berhasil");
                isiGvDaftarRabKomponenBelanja();
                refreshPaging();
                resetForm();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah RAB Komponen Belanja Gagal " + modelRabKomponenBelanja.errorMessage);
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            ddlJenisKegiatanAdd.SelectedIndex = 0;
            ddlRabKelompokBiayaAdd.SelectedIndex = 0;
            tbKomponenBelanja.Text = "";
            tbSatuan.Text = "";
            tbKeterangan.Text = "";
            lbxKategoriPenelitian.ClearSelection();
        }

        private void isiGvDaftarRabKomponenBelanja()
        {
            var dt = new DataTable();
            modelRabKomponenBelanja.currentPage = pagingDaftarRabKomponenBelanja.currentPage;
            modelRabKomponenBelanja.rowsPerPage = Int32.Parse(ddlJmlBarisKomponenBelanja.SelectedValue);
            if (modelRabKomponenBelanja.listRabKomponenBelanja(ref dt, ddlJenisKegiatan.SelectedValue, Int32.Parse(ddlRabKelompokBiaya.SelectedValue)))
            {
                gvDaftarRabKomponenBelanja.DataSource = dt;
                gvDaftarRabKomponenBelanja.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarRabKomponenBelanja_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                ListBox kategoriPenelitian = e.Row.FindControl("lbxKategoriPenelitianEdit") as ListBox;
                int id_rab_komponen_biaya = (int)gvDaftarRabKomponenBelanja.DataKeys[e.Row.RowIndex]["id_rab_komponen_belanja"];
                DataTable dt = new DataTable();
                modelRabKomponenBelanja.getKategoriPenelitian(ref dt);
                uiListBox.bindToListBox(ref kategoriPenelitian, dt, "kategori_penelitian", "id_kategori_penelitian");
                DataTable dtKomponen = new DataTable();
                modelRabKomponenBelanja.getKategoriPenelitian(ref dtKomponen, id_rab_komponen_biaya);
                int[] kategoriComponent = (int[])dtKomponen.Rows[0]["id_kategori_penelitian"];
                foreach (ListItem item in kategoriPenelitian.Items)
                {
                    if (Array.Exists(kategoriComponent, value => value == Int32.Parse(item.Value)))
                        item.Selected = true;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoDaftarPenugasan = (Label)e.Row.FindControl("lblNoDaftarKomponenBelanja");
                lblNoDaftarPenugasan.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisKomponenBelanja.SelectedValue) * (pagingDaftarRabKomponenBelanja.currentPage)).ToString();
            }
        }

        protected void gvDaftarRabKomponenBelanja_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdRabKomponenBelanja"] = Int32.Parse(gvDaftarRabKomponenBelanja.DataKeys[e.RowIndex]["id_rab_komponen_belanja"].ToString());
            lblKomponenBelanja.Text = gvDaftarRabKomponenBelanja.DataKeys[e.RowIndex]["komponen_belanja"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarRabKomponenBelanja_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDaftarRabKomponenBelanja.EditIndex = e.NewEditIndex;
            isiGvDaftarRabKomponenBelanja();
        }

        protected void gvDaftarRabKomponenBelanja_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            gvDaftarRabKomponenBelanja.EditIndex = -1;
            isiGvDaftarRabKomponenBelanja();
        }

        protected void gvDaftarRabKomponenBelanja_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox komponen_belanja = gvDaftarRabKomponenBelanja.Rows[e.RowIndex].FindControl("tbKomponenBelanjaEdit") as TextBox;
            TextBox satuan = gvDaftarRabKomponenBelanja.Rows[e.RowIndex].FindControl("tbSatuanEdit") as TextBox;
            TextBox keterangan = gvDaftarRabKomponenBelanja.Rows[e.RowIndex].FindControl("tbKeteranganEdit") as TextBox;
            ListBox kategori_penelitian = gvDaftarRabKomponenBelanja.Rows[e.RowIndex].FindControl("lbxKategoriPenelitianEdit") as ListBox;
            List<int> id_kategori = new List<int>();
            foreach (ListItem item in kategori_penelitian.Items)
            {
                if (item.Selected)
                    id_kategori.Add(Int32.Parse(item.Value));
            }
            if (!modelRabKomponenBelanja.updateRabKomponenBelanja(ddlJenisKegiatan.SelectedValue, (int)gvDaftarRabKomponenBelanja.DataKeys[e.RowIndex]["id_rab_komponen_belanja"], komponen_belanja.Text, satuan.Text, keterangan.Text, id_kategori.ToArray()))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarRabKomponenBelanja.EditIndex = -1;
            isiGvDaftarRabKomponenBelanja();
        }

        protected void lbHapusRabKomponenBelanja_Click(object sender, EventArgs e)
        {
            if (modelRabKomponenBelanja.deleteRabKomponenBelanja(Int32.Parse(ViewState["IdRabKomponenBelanja"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus RAB Komponen Belanja Berhasil");
                isiGvDaftarRabKomponenBelanja();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus RAB Komponen Belanja Gagal " + modelRabKomponenBelanja.errorMessage);
            }
        }

        protected void daftarRabKomponenBelanja_PageChanging(object sender, EventArgs e)
        {
            modelRabKomponenBelanja.currentPage = pagingDaftarRabKomponenBelanja.currentPage;
            modelRabKomponenBelanja.rowsPerPage = int.Parse(ddlJmlBarisKomponenBelanja.SelectedValue);
            if (!modelRabKomponenBelanja.getDaftarRabKomponenBelanja(ddlJenisKegiatan.SelectedValue, Int32.Parse(ddlRabKelompokBiaya.SelectedValue)))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarRabKomponenBelanja.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarRabKomponenBelanja, modelRabKomponenBelanja.currentRecords);
        }
    }
}