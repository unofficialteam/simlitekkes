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
    public partial class dataPendukungPusat : System.Web.UI.UserControl
    {
        daftarDataPendukungPusat modelData = new daftarDataPendukungPusat();

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
                isiKota(this.ddlKdKota);
                isiInstitusi(this.ddlIdInstitusi, this.ddlKdKota.SelectedValue);
                ddlIdInstitusi.Items.Insert(0, new ListItem("-- Semua Institusi --", "00000000-0000-0000-0000-000000000000"));
                ddlIdInstitusi.SelectedValue = "00000000-0000-0000-0000-000000000000";
                isiKota(this.ddlKdKotaGenerateAkun);
                isiInstitusi(this.ddlIdInstitusiAsalGenerateAkun, this.ddlKdKotaGenerateAkun.SelectedValue);
                isiStatusKepegaiawan(this.ddlEditStatusKepegawaian);
                isiStatusKepegaiawan(this.ddlFilterStatusKepegawaian);
                ddlFilterStatusKepegawaian.Items.Insert(0, new ListItem("-- Semua Status --", "-1"));
                ddlFilterStatusKepegawaian.SelectedValue = "-1";
                isiGvDaftarData();

            }
        }


        protected void ddlJmlBarisData_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }

        protected void ddlFilterStatusKepegawaian_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }

        protected void ddlKdKota_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiInstitusi(ddlIdInstitusi, ddlKdKota.SelectedValue);
            isiGvDaftarData();
        }

        protected void ddlKdKotaGenerateAkun_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiInstitusi(ddlIdInstitusiAsalGenerateAkun, ddlKdKotaGenerateAkun.SelectedValue);
            objModal.ShowModal(this.Page, "modalGenerateAkun");
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarData.setPaging(int.Parse(ddlJmlBarisData.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarData.setPaging(1, 1);
            pagingDaftarData.refreshPaging();
        }

        private void isiStatusKepegaiawan(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelData.getListStatusKepegawaian(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "status_aktif", "kd_status_aktif");
            ddl.SelectedIndex = 0;
        }

        private void isiKota(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelData.getListKota(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "nama_kota", "kd_kota");
            ddl.SelectedIndex = 0;
        }

        private void isiInstitusi(DropDownList ddl, string kd_kota)
        {
            DataTable data = new DataTable();
            modelData.getListIntitusiByKdKota(ref data, kd_kota);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "nama_institusi", "id_institusi");
        }

        private void isiGvDaftarData()
        {
            var dt = new DataTable();
            modelData.currentPage = pagingDaftarData.currentPage;
            modelData.rowsPerPage = Int32.Parse(ddlJmlBarisData.SelectedValue);
            Guid idInstitusi = Guid.Parse("00000000-0000-0000-0000-000000000000");
            if (ddlIdInstitusi.SelectedValue != "")
            {
                idInstitusi = Guid.Parse(ddlIdInstitusi.SelectedValue);
            }
            if (modelData.listData( idInstitusi, ref dt, Int32.Parse(ddlIdPeran.SelectedValue), ddlFilterStatusKepegawaian.SelectedValue, tbPencarian.Text))
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
                Label lblNodaftarData = (Label)e.Row.FindControl("lblNodaftarData");
                lblNodaftarData.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisData.SelectedValue) * (pagingDaftarData.currentPage)).ToString();
            }
        }

        protected void gvDaftarData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdPersonal"] = (Guid)gvDaftarData.DataKeys[e.RowIndex]["id_personal"];
            lblData.Text = gvDaftarData.DataKeys[e.RowIndex]["nama_lengkap"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["IdPersonalDataPendukung"] = gvDaftarData.DataKeys[e.NewEditIndex]["id_personal"].ToString();
            Session["page"] = 44;
            Response.Redirect("Main.aspx");
        }

        protected void gvDaftarData_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "StatusKepegawaian")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                ViewState["IdPersonal"] = (Guid)gvDaftarData.DataKeys[gvr.RowIndex]["id_personal"];
                lblNamaPegawai.Text = gvDaftarData.DataKeys[Convert.ToInt32(gvr.RowIndex)]["nama_lengkap"].ToString();
                this.ddlEditStatusKepegawaian.SelectedValue = gvDaftarData.DataKeys[Convert.ToInt32(gvr.RowIndex)]["kd_sts_aktif"].ToString();
                objModal.ShowModal(this.Page, "modalEditStatus");
            }
        }

        protected void lbSimpanStatusKepegawaian_Click(object sender, EventArgs e)
        {
            if (modelData.setStatusKepegawaian(Guid.Parse(ViewState["IdPersonal"].ToString()), ddlEditStatusKepegawaian.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Status Kepegawaian Berhasil Dirubah");
                isiGvDaftarData();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Perubahan Data Gagal " + modelData.errorMessage);
            }
        }

        protected void lbSimpanGenerateAkun_Click(object sender, EventArgs e)
        {
            DataTable result = new DataTable();
            if (modelData.generateAkun(Guid.Parse(ddlIdInstitusiAsalGenerateAkun.SelectedValue), Int32.Parse(ddlIdPeranGenerateAkun.SelectedValue), 1, ref result))
            {
                if (result.Rows[0]["status"].ToString() == "1")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Akun Berhasil Di Generate");
                    isiGvDaftarData();
                    refreshPaging();
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", result.Rows[0]["keterangan"].ToString());
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Generate Akun Gagal" + modelData.errorMessage);
            }
        }

        protected void lbHapusData_Click(object sender, EventArgs e)
        {
            if (modelData.deleteData(Guid.Parse(ViewState["IdPersonal"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Data Berhasil");
                isiGvDaftarData();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Data Gagal " + modelData.errorMessage);
            }
        }

        protected void daftarData_PageChanging(object sender, EventArgs e)
        {
            modelData.currentPage = pagingDaftarData.currentPage;
            modelData.rowsPerPage = int.Parse(ddlJmlBarisData.SelectedValue);
            if (!modelData.getDaftarData(Guid.Parse(ddlIdInstitusi.SelectedValue), Int32.Parse(ddlIdPeran.SelectedValue), ddlFilterStatusKepegawaian.SelectedValue, tbPencarian.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarData.EditIndex = -1;
            obj_uiGridView = new uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarData, modelData.currentRecords);
        }

        protected void lbTambahData_Click(object sender, EventArgs e)
        {
            /*Session["page"] = 44;
            Response.Redirect("Main.aspx");*/
            objModal.ShowModal(this.Page, "modalGenerateAkun");
        }

        protected void lbCariData_Click(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }
    }
}