using simlitekkes.UIControllers;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class penelitianNonKemkes : System.Web.UI.UserControl
    {
        Models.PT.penelitianNonKemkes model = new Models.PT.penelitianNonKemkes();

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
                //isiStatusKepegaiawan(this.ddlEditStatusKepegawaian);
                //isiStatusKepegaiawan(this.ddlFilterStatusKepegawaian);
                ddlTahunKegiatan.Items.Insert(0, new ListItem("2024", "2024"));
                ddlTahunKegiatan.SelectedValue = "-1";
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

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarData.setPaging(int.Parse(ddlJmlBarisData.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarData.setPaging(1, 1);
            pagingDaftarData.refreshPaging();
        }

        //private void isiStatusKepegaiawan(DropDownList ddl)
        //{
        //    DataTable data = new DataTable();
        //    model.getListStatusKepegawaian(ref data);
        //    obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "status_aktif", "kd_status_aktif");
        //    ddl.SelectedIndex = 0;
        //}

        private void isiGvDaftarData()
        {
            var dt = new DataTable();
            model.currentPage = pagingDaftarData.currentPage;
            model.rowsPerPage = Int32.Parse(ddlJmlBarisData.SelectedValue);
            if (!model.listData(ref dt, Guid.Parse(objLogin.idInstitusi.ToString()), ddlTahunKegiatan.SelectedValue, tbPencarian.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan !", model.errorMessage);
                return;
            }

            gvDaftarData.DataSource = dt;
            gvDaftarData.DataBind();

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
            Session["IdPersonalTendik"] = gvDaftarData.DataKeys[e.NewEditIndex]["id_personal"].ToString();
            Session["page"] = 53;
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
            //if (model.setStatusKepegawaian(Guid.Parse(ViewState["IdPersonal"].ToString()), ddlEditStatusKepegawaian.SelectedValue))
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Status Kepegawaian Berhasil Dirubah");
            //    isiGvDaftarData();
            //    refreshPaging();
            //}
            //else
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Perubahan Data Gagal " + model.errorMessage);
            //}
        }

        protected void lbHapusData_Click(object sender, EventArgs e)
        {
            //if (model.deleteData(Guid.Parse(ViewState["IdPersonal"].ToString())))
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Data Berhasil");
            //    isiGvDaftarData();
            //    refreshPaging();
            //}
            //else
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus Data Gagal " + model.errorMessage);
            //}
        }

        protected void daftarData_PageChanging(object sender, EventArgs e)
        {
            model.currentPage = pagingDaftarData.currentPage;
            model.rowsPerPage = int.Parse(ddlJmlBarisData.SelectedValue);
            if (!model.getDaftarData(Guid.Parse(objLogin.idInstitusi.ToString()), ddlTahunKegiatan.SelectedValue, tbPencarian.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarData.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarData, model.currentRecords);
        }

        protected void lbTambahData_Click(object sender, EventArgs e)
        {
            Session["page"] = 53;
            Response.Redirect("Main.aspx");
        }

        protected void lbCariData_Click(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }

        protected void ddlTahunKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }
    }
}