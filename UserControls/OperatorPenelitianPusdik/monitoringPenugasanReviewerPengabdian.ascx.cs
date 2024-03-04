using OfficeOpenXml;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class monitoringPenugasanReviewerPengabdian : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.OperatorPenelitianPusdik.monitoringPenugasanReviewer modelPenugasanReviewer = new Models.OperatorPenelitianPusdik.monitoringPenugasanReviewer();
        Models.OperatorPenelitianPusdik.monitoringPenugasanReviewer modelPenugasanReviewerPerSkema = new Models.OperatorPenelitianPusdik.monitoringPenugasanReviewer();
        Models.OperatorPenelitianPusdik.monitoringPenugasanReviewer modelPenugasanReviewerPerPT = new Models.OperatorPenelitianPusdik.monitoringPenugasanReviewer();
        Models.OperatorPenelitianPusdik.monitoringPenugasanReviewer modelPenugasanReviewerModal = new Models.OperatorPenelitianPusdik.monitoringPenugasanReviewer();

        Core.manipulasiData objManipData = new Core.manipulasiData();

        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiGridView obj_uiGridView = new uiGridView();
        uiModal objModal = new uiModal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvMonitoringPenugasanReviewer);
            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvDaftarPenugasan);
            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbExcelUsulanBaru);
            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbExcelDaftarReviewer);

            if (!IsPostBack)
            {
                if (ViewState["kdproghibah"] == null)
                {
                    ViewState["kdproghibah"] = "3"; //Unggulan Nasional
                    lbnasional.CssClass = "btn btn-outline-success";
                    lbpt.CssClass = "btn btn-outline-secondary";
                }
                isiTahapanKegiatan();
                isiDdlThnUsulan();
                ViewState["thnUsulan"] = ddlThnUsulan.SelectedValue;
                isiDdlThnPelaksanaan();
                ViewState["thnPelaksanaan"] = ddlThnPelaksanaan.SelectedItem.Text;

                ddlTahapan.SelectedIndex = 0;
                ViewState["Tahapan"] = ddlTahapan.SelectedItem.Text;

                refreshDataMonitoringPenugasanReveiwer();
                mvMonitoringPenugasanReviewer.SetActiveView(vRekapPenugasanReviewer);
            }
        }

        #region View Rekap Penugasan


        // Private functions
        // --------------------------------

        protected void ddlProgramKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshDataMonitoringPenugasanReveiwer();
        }

        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Tahapan"] = ddlTahapan.SelectedItem.Text;
            refreshDataMonitoringPenugasanReveiwer();
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ViewState["thnUsulan"] = thnUsulan;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 1; i >= 0; i--)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
            ddlThnPelaksanaan.SelectedIndex = 0;
            string thnPelaksanaan = ddlThnPelaksanaan.SelectedItem.Text; ;
            ViewState["thnPelaksanaan"] = thnPelaksanaan;
            refreshDataMonitoringPenugasanReveiwer();
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string thnPelaksanaan = ddlThnPelaksanaan.SelectedItem.Text; ;
            ViewState["thnPelaksanaan"] = thnPelaksanaan;
            refreshDataMonitoringPenugasanReveiwer();
        }

        protected void lbExcelUsulanBaru_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Daftar Usulan.xlsx");
                var dt = new DataTable();
                if (modelPenugasanReviewer.rekap_excel_monitoring_proposal_penugasan_reviewer_pt(ref dt, ViewState["kdproghibah"].ToString(),
                    ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("daftar_proposal");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPenugasanReviewer.errorMessage);
                    return;
                }

                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pck.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
                pck.Dispose();
            }
        }

        protected void lbExcelDaftarReviewer_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Daftar Reviewer.xlsx");
                var dt = new DataTable();
                if (modelPenugasanReviewer.rekap_excel_monitoring_penugasan_reviewer_pt(ref dt, ViewState["kdproghibah"].ToString(),
                    ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("daftar_reviewer");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPenugasanReviewer.errorMessage);
                    return;
                }

                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pck.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
                pck.Dispose();
            }
        }

        protected void gvMonitoringPenugasanReviewer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //if (ddlProgram.SelectedValue == "0" || ddlThnUsulan.SelectedValue == "0000" || ddlTahapan.SelectedValue == "0")
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Program hibah, Tahun Usulan, Tahun pelaksanaan, dan Tahapan harus dipilih");
            //    return;
            //}
            //else
            //{
            //    int idSkema = int.Parse(gvRekapPenugasanSkema.DataKeys[e.RowIndex]["id_skema"].ToString());
            //    ViewState["IdSkema"] = idSkema;
            //    string namaSkema = gvRekapPenugasanSkema.DataKeys[e.RowIndex]["nama_skema"].ToString();
            //    ViewState["namaSkema"] = namaSkema;
            //    lblProgramPenugasan.Text = ddlProgram.SelectedItem.Text;
            //    lblSKemaPenugasan.Text = namaSkema;
            //    lblThnUsulanPenugasan.Text = ddlThnUsulan.SelectedItem.Text;
            //    lblThnPelaksanaanPenugasan.Text = ddlThnPelaksanaan.SelectedItem.Text;

            //    lblProgramReviewer.Text = ddlProgram.SelectedItem.Text;
            //    lblSkemaReviewer.Text = namaSkema;
            //    lblThnUsulanReviewer.Text = ddlThnUsulan.SelectedItem.Text;
            //    lblThnPelaksanaanReviewer.Text = ddlThnPelaksanaan.SelectedItem.Text;

            //    isiGvDaftarSudahDitugaskan(0);
            //    mvMain.SetActiveView(vDaftarSudahDitugaskan);
            //}
        }

        protected void gvMonitoringPenugasanReviewer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idx = Convert.ToInt32(e.CommandArgument.ToString());
            int idSkema = int.Parse(gvMonitoringPenugasanReviewer.DataKeys[idx]["id_skema"].ToString());
            string namaSkema = gvMonitoringPenugasanReviewer.DataKeys[idx]["nama_skema"].ToString();

            string thnUsulan = ViewState["thnUsulan"].ToString(); ;
            string thnPelaksanaan = ViewState["thnPelaksanaan"].ToString();
            string tahapanKegiatan = ViewState["Tahapan"].ToString();

            ViewState["IdSkema"] = idSkema;
            ViewState["namaSkema"] = namaSkema;

            if (thnUsulan == "0000" || tahapanKegiatan == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Program hibah, Tahun Usulan, Tahun pelaksanaan, dan Tahapan harus dipilih");
                return;
            }
            else
            {
                if (ViewState["kdproghibah"].ToString() == "3")
                    lblProgramKegiatan.Text = "Unggulan Nasional";
                else
                    lblProgramKegiatan.Text = "Unggulan Perguruan Tinggi";
                lblNamaSkema.Text = namaSkema;
                lblThnUsulan.Text = thnUsulan;
                lblThnPelaksanaan.Text = thnPelaksanaan;
                lblTahapanPenugasanPerSkema.Text = tahapanKegiatan;
            }
            isiRekapListPT();
            isiGvDaftarPenugasanReviewerBySkema();
            mvMonitoringPenugasanReviewer.SetActiveView(vRekapPenugasanReviewerPerSkema);
        }

        protected void gvDaftarPenugasanReviewerBySkema_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idx = Convert.ToInt32(e.CommandArgument.ToString());
            Guid idPT = Guid.Parse(gvDaftarPenugasanReviewerBySkema.DataKeys[idx]["id_institusi"].ToString());
            string nama_institusi = gvDaftarPenugasanReviewerBySkema.DataKeys[idx]["nama_institusi"].ToString();
            string nama_klaster = gvDaftarPenugasanReviewerBySkema.DataKeys[idx]["nama_klaster"].ToString();
            int jml_penugasan = Int32.Parse(gvDaftarPenugasanReviewerBySkema.DataKeys[idx]["jml_reviewer"].ToString());

            string thnUsulan = ViewState["thnUsulan"].ToString(); ;
            string thnPelaksanaan = ViewState["thnPelaksanaan"].ToString();
            string tahapanKegiatan = ViewState["Tahapan"].ToString();

            ViewState["IdPT"] = idPT;
            ViewState["namaPT"] = nama_institusi;
            ViewState["namaKlaster"] = nama_klaster;

            if (thnUsulan == "0000" || tahapanKegiatan == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Program hibah, Tahun Usulan, Tahun pelaksanaan, dan Tahapan harus dipilih");
                return;
            }
            else
            {
                if (ViewState["kdproghibah"].ToString() == "3")
                    lblProgramKegiatan.Text = "Unggulan Nasional";
                else
                    lblProgramKegiatan.Text = "Unggulan Perguruan Tinggi";

                lblNamaSkeam_perPT.Text = ViewState["namaSkema"].ToString();
                lblThnUsulan_perPT.Text = thnUsulan;
                lblThnPelaksanaan_perPT.Text = thnPelaksanaan;
                lblTahapan_perPT.Text = tahapanKegiatan;
                lblJumlahPenugasan_perPT.Text = jml_penugasan.ToString();
                lblNamaInstitusiPenugasan.Text = nama_institusi + " - " + nama_klaster;
            }
            isiGvDaftarPenugasanReviewerByInstitusi();
            mvMonitoringPenugasanReviewer.SetActiveView(vRekapPenugasanReviewerPerPT);
        }

        protected void gvDaftarPenugasanReviewerByInstitusi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idx = Convert.ToInt32(e.CommandArgument.ToString());
            Guid idReviewer = Guid.Parse(gvDaftarPenugasanReviewerByPT.DataKeys[idx]["id_reviewer"].ToString());
            ViewState["IdReviewer"] = idReviewer;
            string nama = gvDaftarPenugasanReviewerByPT.DataKeys[idx]["nama"].ToString();
            string nidn = gvDaftarPenugasanReviewerByPT.DataKeys[idx]["nidn"].ToString();
            string jmlInstitusiYgMenugasi = gvDaftarPenugasanReviewerByPT.DataKeys[idx]["jml_institusi_yg_menugasi"].ToString();
            string thnUsulan = ViewState["thnUsulan"].ToString(); ;
            string thnPelaksanaan = ViewState["thnPelaksanaan"].ToString();
            string tahapanKegiatan = ViewState["Tahapan"].ToString();

            if (ViewState["kdproghibah"].ToString() == "3")
                lblProgramKegiatan_modal.Text = "Unggulan Nasional";
            else
                lblProgramKegiatan_modal.Text = "Unggulan Perguruan Tinggi";

            lblSkema_modal.Text = ViewState["namaSkema"].ToString();
            lblTahunUsulan_modal.Text = thnUsulan;
            lblPelaksanaan_modal.Text = thnPelaksanaan;
            lblTahapan_modal.Text = tahapanKegiatan;

            lblNamaInstitusi_modal.Text = ViewState["namaPT"].ToString();
            lblNidnNama_modal.Text = nidn + " | " + nama;
            lblJmlData_modal.Text = jmlInstitusiYgMenugasi;
            isiGvListInstitusiModal();
            refreshPagingListInstitusiModal();
            objModal.ShowModal(this.Page, "modalDetail");
        }

        private void isiDdlThnUsulan()
        {
            ddlThnUsulan.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlThnUsulan.SelectedIndex = 0;
        }

        private void isiDdlThnPelaksanaan()
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 1; i >= 0; i--)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
            ddlThnPelaksanaan.SelectedIndex = 0;
        }

        private void isiTahapanKegiatan()
        {
            if (Application["TahapanKegiatan"] != null)
            {
                DataTable TahapanKegiatan = objManipData.filterData((DataTable)Application["TahapanKegiatan"], "kd_tahapan_kegiatan IN ('20','22','25','33','49') ");
                if (!obj_uiDropdownlist.bindToDropDownList(ref ddlTahapan, TahapanKegiatan, "tahapan", "kd_tahapan_kegiatan"))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + obj_uiDropdownlist.errorMessage);
                    return;
                }
            }
        }

        protected void refreshDataMonitoringPenugasanReveiwer()
        {
            var dt = new DataTable();
            string allInstitusi = "00000000-0000-0000-0000-000000000000";
            if (modelPenugasanReviewer.daftarRekapPenugasanReviewer(ref dt, objLogin.idPersonal.ToString(), ViewState["kdproghibah"].ToString(),
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue,
                allInstitusi))
            {
                gvMonitoringPenugasanReviewer.DataSource = dt;
                gvMonitoringPenugasanReviewer.DataBind();
                isiRekap();
            }
        }

        protected void lbnasional_Click(object sender, EventArgs e)
        {
            lbnasional.CssClass = "btn btn-outline-success";
            lbpt.CssClass = "btn btn-outline-secondary";
            ViewState["kdproghibah"] = "3";
            refreshDataMonitoringPenugasanReveiwer();
        }

        protected void lbpt_Click(object sender, EventArgs e)
        {
            lbnasional.CssClass = "btn btn-outline-secondary";
            lbpt.CssClass = "btn btn-outline-success";
            ViewState["kdproghibah"] = "7";
            refreshDataMonitoringPenugasanReveiwer();
        }

        private void isiRekap()
        {
            var dt = new DataTable();
            if (modelPenugasanReviewer.rekap_reviewer_perguruan_tinggi(ref dt, ViewState["kdproghibah"].ToString(),
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue))
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalProposal_listSkema.Text = dt.Rows[0]["total_proposal"].ToString();
                    lblTotalPenugasan_listSkema.Text = dt.Rows[0]["total_penugasan"].ToString();
                }
            }
        }

        private void isiRekapListPT()
        {
            var dt = new DataTable();
            if (modelPenugasanReviewerPerSkema.rekap_penugasan_reviewer_by_skema(ref dt, ViewState["kdproghibah"].ToString(),
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, (int)ViewState["IdSkema"]))
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalPT_listPT.Text = dt.Rows[0]["total_perguruan_tinggi"].ToString();
                    lblTotalProposal_listPT.Text = dt.Rows[0]["total_proposal"].ToString();
                    lblTotalPenugasan_listPT.Text = dt.Rows[0]["total_reviewer"].ToString();
                }
            }
        }

        private void isiGvDaftarPenugasanReviewerBySkema()
        {
            isiRekapListPT();
            var dt = new DataTable();
            modelPenugasanReviewerPerSkema.currentPage = pagingListPT.currentPage;
            modelPenugasanReviewerPerSkema.rowsPerPage = Int32.Parse(ddlJmlBarisPenugasanPerskema.SelectedValue);
            if (modelPenugasanReviewerPerSkema.listRekapPenugasanReviewerBySkema(ref dt, ViewState["kdproghibah"].ToString(),
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], tbPencarianPenugasanReviewerBySkema.Text))
            {
                gvDaftarPenugasanReviewerBySkema.DataSource = dt;
                gvDaftarPenugasanReviewerBySkema.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["total_perguruan_tinggi"].ToString();
                refreshPagingPenugasanReviewerBySkema();
            }
        }

        private void isiGvDaftarPenugasanReviewerByInstitusi()
        {
            var dt = new DataTable();
            modelPenugasanReviewerPerPT.currentPage = pagingListReviewer_perPT.currentPage;
            modelPenugasanReviewerPerPT.rowsPerPage = Int32.Parse(ddlJmlBarisPerPT.SelectedValue);
            if (modelPenugasanReviewer.listRekapPenugasanReviewerByInstitusi(ref dt,
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], Guid.Parse(ViewState["IdPT"].ToString()), tbPencarianNamaReviewer.Text))
            {
                gvDaftarPenugasanReviewerByPT.DataSource = dt;
                gvDaftarPenugasanReviewerByPT.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_perPT"] = dt.Rows[0]["jml_record"].ToString();
                refreshPagingPenugasanReviewerByInstitusi();
            }
        }

        private void isiGvListInstitusiModal()
        {
            var dt = new DataTable();
            modelPenugasanReviewerModal.currentPage = pagingDataModal.currentPage;
            modelPenugasanReviewerModal.rowsPerPage = 5;
            if (modelPenugasanReviewerModal.listRekapPenugasanReviewerPersonal(ref dt,
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], Guid.Parse(ViewState["IdReviewer"].ToString())))
            {
                gvListInstitusiModal.DataSource = dt;
                gvListInstitusiModal.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_modal"] = dt.Rows[0]["jml_record"].ToString();
                refreshPagingListInstitusiModal();
            }
        }

        private void refreshPagingPenugasanReviewerBySkema()
        {
            if (ViewState["jml_record"] != null)
                pagingListPT.setPaging(int.Parse(ddlJmlBarisPenugasanPerskema.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingListPT.setPaging(1, 1);
            pagingListPT.refreshPaging();
        }

        private void refreshPagingPenugasanReviewerByInstitusi()
        {
            if (ViewState["jml_record_perPT"] != null)
                pagingListReviewer_perPT.setPaging(int.Parse(ddlJmlBarisPerPT.SelectedValue), int.Parse(ViewState["jml_record_perPT"].ToString()));
            else
                pagingListReviewer_perPT.setPaging(1, 1);
            pagingListReviewer_perPT.refreshPaging();
        }

        private void refreshPagingListInstitusiModal()
        {
            if (ViewState["jml_record_modal"] != null)
                pagingDataModal.setPaging(5, int.Parse(ViewState["jml_record_modal"].ToString()));
            else
                pagingDataModal.setPaging(1, 1);
            pagingDataModal.refreshPaging();
        }

        protected void daftarDataPenugasanReviewerBySkema_PageChanging(object sender, EventArgs e)
        {
            modelPenugasanReviewerPerSkema.currentPage = pagingListPT.currentPage;
            modelPenugasanReviewerPerSkema.rowsPerPage = int.Parse(ddlJmlBarisPenugasanPerskema.SelectedValue);
            if (!modelPenugasanReviewerPerSkema.getDaftarRekapPenugasanReviewerBySkema(ViewState["kdproghibah"].ToString(),
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], tbPencarianPenugasanReviewerBySkema.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarPenugasanReviewerBySkema.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarPenugasanReviewerBySkema, modelPenugasanReviewerPerSkema.currentRecords);
        }

        protected void daftarDataPenugasanReviewerByInstitusi_PageChanging(object sender, EventArgs e)
        {
            modelPenugasanReviewerPerPT.currentPage = pagingListReviewer_perPT.currentPage;
            modelPenugasanReviewerPerPT.rowsPerPage = int.Parse(ddlJmlBarisPerPT.SelectedValue);
            if (!modelPenugasanReviewerPerPT.getDaftarRekapPenugasanReviewerByInstitusi(
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], Guid.Parse(ViewState["IdPT"].ToString()), tbPencarianNamaReviewer.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarPenugasanReviewerByPT.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarPenugasanReviewerByPT, modelPenugasanReviewerPerPT.currentRecords);
        }

        protected void daftarDataListInstitusiModal_PageChanging(object sender, EventArgs e)
        {
            string jsScript = "$('.modal-backdrop').hide();";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "removeBackDrop", jsScript, true);
            modelPenugasanReviewerModal.currentPage = pagingDataModal.currentPage;
            modelPenugasanReviewerModal.rowsPerPage = 5;
            if (!modelPenugasanReviewerModal.getDaftarRekapPenugasanReviewerByPersonal(
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], Guid.Parse(ViewState["IdReviewer"].ToString())))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvListInstitusiModal.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvListInstitusiModal, modelPenugasanReviewerModal.currentRecords);
            objModal.ShowModal(this.Page, "modalDetail");
        }
        #endregion

        #region Rekap Penugasan Reviewer Per Skema

        protected void lbKembaliPerSkema_Click(object sender, EventArgs e)
        {
            refreshDataMonitoringPenugasanReveiwer();
            refreshPagingPenugasanReviewerBySkema();
            mvMonitoringPenugasanReviewer.SetActiveView(vRekapPenugasanReviewer);
        }

        protected void lbKembaliPerPerPT_Click(object sender, EventArgs e)
        {
            isiGvDaftarPenugasanReviewerBySkema();
            refreshPagingPenugasanReviewerByInstitusi();
            mvMonitoringPenugasanReviewer.SetActiveView(vRekapPenugasanReviewerPerSkema);
        }

        protected void lbPencarianPenugasanReviewerBySkema_Click(object sender, EventArgs e)
        {
            refreshPagingPenugasanReviewerBySkema();
            isiGvDaftarPenugasanReviewerBySkema();
        }

        protected void lbPencarianPenugasanReviewerByPT_Click(object sender, EventArgs e)
        {
            refreshPagingPenugasanReviewerByInstitusi();
            isiGvDaftarPenugasanReviewerByInstitusi();
        }

        protected void ddlJmlBarisPenugasanPerskema_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarPenugasanReviewerBySkema();
            refreshPagingPenugasanReviewerBySkema();
        }

        protected void lbExcelPenugasanPerSkema_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format(ViewState["namaSkema"].ToString() + ".xlsx");
                var dt = new DataTable();
                if (modelPenugasanReviewerPerSkema.listRekapPenugasanReviewerBySkema(ref dt, ViewState["kdproghibah"].ToString(),
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], tbPencarianPenugasanReviewerBySkema.Text))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("daftar_reviewer");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPenugasanReviewer.errorMessage);
                    return;
                }

                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pck.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
                pck.Dispose();
            }
        }

        protected void lbExcelPenugasanPerPT_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format(ViewState["namaPT"].ToString() + ".xlsx");
                var dt = new DataTable();
                if (modelPenugasanReviewer.listRekapPenugasanReviewerByInstitusi(ref dt,
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], Guid.Parse(ViewState["IdPT"].ToString()), tbPencarianNamaReviewer.Text))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("daftar_reviewer");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPenugasanReviewer.errorMessage);
                    return;
                }

                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pck.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
                pck.Dispose();
            }
        }

        protected void ddlJmlBarisPenugasanPerPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarPenugasanReviewerByInstitusi();
            refreshPagingPenugasanReviewerByInstitusi();
        }
        #endregion
    }
}