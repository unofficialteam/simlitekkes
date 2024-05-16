using simlitekkes.UIControllers;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class daftarReviewer : System.Web.UI.UserControl
    {
        Models.OperatorPenelitianPusdik.daftarReviewer modelDaftarReviewer = new Models.OperatorPenelitianPusdik.daftarReviewer();

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
                isiStatusKepegaiawan(this.ddlEditStatusKepegawaian);
                isiStatusKepegaiawan(this.ddlFilterStatusKepegawaian);
                ddlFilterStatusKepegawaian.Items.Insert(0, new ListItem("-- Semua Status --", ""));
                ddlFilterStatusKepegawaian.SelectedValue = "1";
                isiGvDaftarData();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "$('.select2').select2()", true);
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

        private void isiStatusKepegaiawan(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelDaftarReviewer.getListStatusKepegawaian(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "status_aktif", "kd_status_aktif");
            ddl.SelectedIndex = 0;
        }

        private void isiGvDaftarData()
        {
            var dt = new DataTable();
            modelDaftarReviewer.currentPage = pagingDaftarData.currentPage;
            modelDaftarReviewer.rowsPerPage = Int32.Parse(ddlJmlBarisData.SelectedValue);
            if (modelDaftarReviewer.listData(ref dt, tbPencarian.Text, ddlFilterStatusKepegawaian.SelectedValue, ddlKategoriReviewer.SelectedValue))
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
            ViewState["IdReviewer"] = (Guid)gvDaftarData.DataKeys[e.RowIndex]["id_reviewer_nasional_penelitian"];
            lblData.Text = gvDaftarData.DataKeys[e.RowIndex]["nama_lengkap"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void gvDaftarData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["IdReveiwerNasionalPenelitian"] = gvDaftarData.DataKeys[e.NewEditIndex]["id_reviewer_nasional_penelitian"].ToString();
            tbNIDN.Text = gvDaftarData.DataKeys[e.NewEditIndex]["nidn"].ToString();
            tbKompetensi.Text = gvDaftarData.DataKeys[e.NewEditIndex]["kompetensi"].ToString();
            tbNoSertifikasi.Text = gvDaftarData.DataKeys[e.NewEditIndex]["no_sertifikasi"].ToString();
            lbSimpanData.Text = "<i class='fas fa-save mr-2'></i>Ubah Data Reviewer";
            DataTable result = new DataTable();
            if (modelDaftarReviewer.getReviewerByNIDN(tbNIDN.Text, ref result))
            {
                if (result.Rows.Count > 0)
                {
                    lblDetailNamaLengkap.Text = result.Rows[0]["nama_lengkap"].ToString();
                    lblDetailNIDN.Text = result.Rows[0]["nidn"].ToString();
                    lblDetailJenisKelamin.Text = result.Rows[0]["jenis_kelamin"].ToString();
                    lblDetailAlamatEmail.Text = result.Rows[0]["email"].ToString();
                    lblDetailNamaInstitusi.Text = result.Rows[0]["nama_institusi"].ToString();
                    lblDetailStatus.Text = result.Rows[0]["status_aktif"].ToString();
                    mvResultDosen.SetActiveView(vFound);
                }
                else
                    mvResultDosen.SetActiveView(vNotFound);
            }
            else
                mvResultDosen.SetActiveView(vNotFound);
            mvDaftarReviewer.SetActiveView(vInsupData);
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
            if (modelDaftarReviewer.setStatusKepegawaian(Guid.Parse(ViewState["IdPersonal"].ToString()), ddlEditStatusKepegawaian.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Status Kepegawaian Berhasil Dirubah");
                isiGvDaftarData();
                refreshPaging();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Perubahan Data Gagal " + modelDaftarReviewer.errorMessage);
            }
        }

        protected void lbHapusData_Click(object sender, EventArgs e)
        {
            if (modelDaftarReviewer.deleteData(Guid.Parse(ViewState["IdReviewer"].ToString()), ddlKategoriReviewer.SelectedValue ))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus Data Berhasil");
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
            if (!modelDaftarReviewer.getDaftarData(tbPencarian.Text, ddlFilterStatusKepegawaian.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarData.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarData, modelDaftarReviewer.currentRecords);
        }

        protected void lbTambahData_Click(object sender, EventArgs e)
        {
            Session["IdReveiwerNasionalPenelitian"] = "0";
            lbSimpanData.Text = "<i class='fas fa-save mr-2'></i>Tambah sebagai Reviewer";
            tbNIDN.Text = "";
            mvResultDosen.SetActiveView(vNotFound);
            mvDaftarReviewer.SetActiveView(vInsupData);
            lblKategoriReviewer.Text = ddlKategoriReviewer.SelectedItem.Text;
        }
        protected void lbCekNIDN_Click(object sender, EventArgs e)
        {
            DataTable result = new DataTable();
            if (modelDaftarReviewer.getReviewerByNIDN(tbNIDN.Text, ref result))
            {
                if (result.Rows.Count > 0)
                {
                    lblDetailNamaLengkap.Text = result.Rows[0]["nama_lengkap"].ToString();
                    lblDetailNIDN.Text = result.Rows[0]["nidn"].ToString();
                    lblDetailJenisKelamin.Text = result.Rows[0]["jenis_kelamin"].ToString();
                    lblDetailAlamatEmail.Text = result.Rows[0]["email"].ToString();
                    lblDetailNamaInstitusi.Text = result.Rows[0]["nama_institusi"].ToString();
                    lblDetailStatus.Text = result.Rows[0]["status_aktif"].ToString();
                    mvResultDosen.SetActiveView(vFound);
                }
                else
                    mvResultDosen.SetActiveView(vNotFound);
            }
            else
                mvResultDosen.SetActiveView(vNotFound);
        }

        protected void lbSimpanData_Click(object sender, EventArgs e)
        {
            if (tbKompetensi.Text == "" || tbNoSertifikasi.Text == "" || lblDetailNIDN.Text == "")
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pastikan NIDN, Kompetensi dan Nomor Sertifikasi sudah terisi");
            else
            {
                if (Session["IdReveiwerNasionalPenelitian"].ToString().Equals("0"))
                {
                    if (modelDaftarReviewer.insertData(lblDetailNIDN.Text, tbKompetensi.Text, tbNoSertifikasi.Text, ddlKategoriReviewer.SelectedValue))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Penambahan Data Berhasil");
                        isiGvDaftarData();
                        mvDaftarReviewer.SetActiveView(vListData);
                    }
                    else
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", modelDaftarReviewer.errorMessage);
                } else
                {
                    if (modelDaftarReviewer.updateData(Guid.Parse(Session["IdReveiwerNasionalPenelitian"].ToString()), tbKompetensi.Text, tbNoSertifikasi.Text))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Perubahan Data Berhasil");
                        isiGvDaftarData();
                        mvDaftarReviewer.SetActiveView(vListData);
                    }
                    else
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", modelDaftarReviewer.errorMessage);
                }
            }
        }
        protected void lbDaftarData_Click(object sender, EventArgs e)
        {
            mvDaftarReviewer.SetActiveView(vListData);
        }

        protected void lbCariData_Click(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }

        protected void ddlKategoriReviewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarData();
        }
    }
}