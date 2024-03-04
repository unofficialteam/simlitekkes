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
    public partial class klasterPerguruanTinggi : System.Web.UI.UserControl
    {
        Models.Admin.daftarKlasterPerguruanTinggi modelDaftarReviewer = new Models.Admin.daftarKlasterPerguruanTinggi();

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
                //Filter Component
                isiInstitusi(this.ddlFilterInstitusi);
                this.ddlFilterInstitusi.Items.Insert(0, new ListItem("-- Semua Institusi --", "00000000-0000-0000-0000-000000000000"));
                this.ddlFilterInstitusi.SelectedIndex = 0;
                isiKlaster(this.ddlFilterKlaster);
                this.ddlFilterKlaster.Items.Insert(0, new ListItem("-- Semua Klaster --", "0"));
                this.ddlFilterKlaster.SelectedIndex = 0;
                isiTahunKlaster(this.ddlFilterTahunKlaster);
                this.ddlFilterTahunKlaster.Items.Insert(0, new ListItem("-- Semua Tahun --", "0000"));
                this.ddlFilterTahunKlaster.SelectedIndex = 0;
                // Add Component
                isiInstitusi(this.ddlAddIdInstitusi);
                isiKlaster(this.ddlAddKlaster);
                isiProgramHibah(this.ddlAddProgramHibah);
                isiGvDaftarData();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "$('.select2').select2()", true);
        }


        protected void ddlJmlBarisData_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarData.setPaging(int.Parse(ddlJmlBarisData.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarData.setPaging(1, 1);
            pagingDaftarData.refreshPaging();
        }

        private void isiInstitusi(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelDaftarReviewer.getListInstitusi(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "nama_institusi", "id_institusi");
            ddl.SelectedIndex = 0;
        }

        private void isiKlaster(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelDaftarReviewer.getListKlaster(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "nama_klaster", "kd_klaster");
            ddl.SelectedIndex = 0;
        }

        private void isiProgramHibah(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelDaftarReviewer.getListProgramHibah(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "program_hibah", "kd_program_hibah");
            ddl.SelectedIndex = 0;
        }

        private void isiTahunKlaster(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelDaftarReviewer.getListTahunKlaster(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "thn_klaster", "thn_klaster");
            ddl.SelectedIndex = 0;
        }

        private void isiGvDaftarData()
        {
            var dt = new DataTable();
            modelDaftarReviewer.currentPage = pagingDaftarData.currentPage;
            modelDaftarReviewer.rowsPerPage = Int32.Parse(ddlJmlBarisData.SelectedValue);
            if (modelDaftarReviewer.listData(ref dt, Guid.Parse(ddlFilterInstitusi.SelectedValue), ddlFilterKlaster.SelectedValue, ddlFilterTahunKlaster.SelectedValue, tbPencarian.Text))
            {
                gvDaftarData.DataSource = dt;
                gvDaftarData.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNodaftarData = (Label)e.Row.FindControl("lblNoDaftarData");
                lblNodaftarData.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisData.SelectedValue) * (pagingDaftarData.currentPage)).ToString();
            }
        }

        protected void gvDaftarData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdKlasterPerguruanTinggi"] = (Guid)gvDaftarData.DataKeys[e.RowIndex]["id_klaster_perguruan_tinggi"];
            lblData.Text = gvDaftarData.DataKeys[e.RowIndex]["nama_institusi"].ToString() + " Pada Klaster " + gvDaftarData.DataKeys[e.RowIndex]["nama_klaster"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["IdKlasterPerguruanTinggi"] = gvDaftarData.DataKeys[e.NewEditIndex]["id_klaster_perguruan_tinggi"].ToString();
            ddlAddIdInstitusi.ClearSelection();
            ddlAddIdInstitusi.SelectedValue = gvDaftarData.DataKeys[e.NewEditIndex]["id_institusi"].ToString();
            DateTime date = DateTime.Parse(gvDaftarData.DataKeys[e.NewEditIndex]["tgl_data"].ToString());
            tbAddTanggalData.Text = date.ToString("yyyy-MM-dd");
            tbAddTahunKlaster.Text = gvDaftarData.DataKeys[e.NewEditIndex]["thn_klaster"].ToString();
            ddlAddKlaster.ClearSelection();
            ddlAddKlaster.SelectedValue = gvDaftarData.DataKeys[e.NewEditIndex]["kd_klaster"].ToString();
            ddlAddProgramHibah.ClearSelection();
            ddlAddProgramHibah.SelectedValue = gvDaftarData.DataKeys[e.NewEditIndex]["kd_program_hibah"].ToString();
            lbAddData.Text = "<i class='fas fa-save mr-2'></i>Ubah Data";
            DataTable result = new DataTable();
            mvDaftarReviewer.SetActiveView(vInsupData);
        }

        protected void gvDaftarData_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void lbHapusData_Click(object sender, EventArgs e)
        {
            if (modelDaftarReviewer.deleteData(Guid.Parse(ViewState["IdKlasterPerguruanTinggi"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Data Berhasil");
                isiTahunKlaster(this.ddlFilterTahunKlaster);
                isiGvDaftarData();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Data Gagal " + modelDaftarReviewer.errorMessage);
            }
        }

        protected void daftarData_PageChanging(object sender, EventArgs e)
        {
            modelDaftarReviewer.currentPage = pagingDaftarData.currentPage;
            modelDaftarReviewer.rowsPerPage = int.Parse(ddlJmlBarisData.SelectedValue);
            if (!modelDaftarReviewer.getDaftarData(Guid.Parse(ddlFilterInstitusi.SelectedValue), ddlFilterKlaster.SelectedValue, ddlFilterTahunKlaster.SelectedValue, tbPencarian.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarData.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarData, modelDaftarReviewer.currentRecords);
        }

        protected void lbTambahData_Click(object sender, EventArgs e)
        {
            Session["IdKlasterPerguruanTinggi"] = "0";
            lbAddData.Text = "<i class='fas fa-save mr-2'></i>Simpan Data";
            mvDaftarReviewer.SetActiveView(vInsupData);
        }

        protected void lbSimpanData_Click(object sender, EventArgs e)
        {
            if (tbAddTahunKlaster.Text == "" || tbAddTanggalData.Text == "")
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pastikan semua data terisi");
            else
            {
                if (Session["IdKlasterPerguruanTinggi"].ToString().Equals("0"))
                {
                    if (modelDaftarReviewer.insertData(Guid.Parse(ddlAddIdInstitusi.SelectedValue), tbAddTahunKlaster.Text, ddlAddKlaster.SelectedValue, tbAddTanggalData.Text, ddlAddProgramHibah.SelectedValue))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Penambahan Data Berhasil");
                        isiTahunKlaster(this.ddlFilterTahunKlaster);
                        isiGvDaftarData();
                        mvDaftarReviewer.SetActiveView(vListData);
                    }
                    else
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", modelDaftarReviewer.errorMessage);
                }
                else
                {
                    if (modelDaftarReviewer.updateData(Guid.Parse(Session["IdKlasterPerguruanTinggi"].ToString()), Guid.Parse(ddlAddIdInstitusi.SelectedValue), tbAddTahunKlaster.Text, ddlAddKlaster.SelectedValue, tbAddTanggalData.Text, ddlAddProgramHibah.SelectedValue))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Perubahan Data Berhasil");
                        isiTahunKlaster(this.ddlFilterTahunKlaster);
                        isiGvDaftarData();
                        mvDaftarReviewer.SetActiveView(vListData);
                    }
                    else
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", modelDaftarReviewer.errorMessage);
                }
            }
        }

        private void resetForm()
        {
            this.ddlAddKlaster.ClearSelection();
            this.ddlAddIdInstitusi.ClearSelection();
            this.ddlAddProgramHibah.ClearSelection();
            this.tbAddTanggalData.Text = "";
            this.tbAddTahunKlaster.Text = "";
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            Session["IdKlasterPerguruanTinggi"] = "0";
            lbAddData.Text = "<i class='fas fa-save mr-2'></i>Simpan Data";
            resetForm();
            mvDaftarReviewer.SetActiveView(vListData);
        }

        protected void lbCariData_Click(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }
    }
}