using OfficeOpenXml;
using simlitekkes.UIControllers;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class penugasanReviewer : System.Web.UI.UserControl
    {
        Models.OperatorPenelitianPusdik.daftarPenugasanReviewer modelPenugasanReviewer = new Models.OperatorPenelitianPusdik.daftarPenugasanReviewer();
        Models.OperatorPenelitianPusdik.plottingReviewer objModelPlottingReviewer = new Models.OperatorPenelitianPusdik.plottingReviewer();

        uiGridView obj_uiGridView = new uiGridView();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        uiDropdownList obj_uiDDL = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        Models.login objLogin;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvRekapPenugasanSkema);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvDaftarPenugasan);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbExcelUsulanBaru);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbExcelDaftarReviewer);

            if (!IsPostBack)
            {
                isiDdlProgram();
                isiDdlThnUsulan();
                isiDdlThnPelaksanaan();
                isiTahapanKegiatan();
                ddlTahapan.SelectedIndex = 0;

                isiGvRekapPenugasanSkema();
                mvMain.SetActiveView(vRekapPenugasanSkema);
            }
        }

        #region Rekap Skema

        private void isiGvRekapPenugasanSkema()
        {
            var dt = new DataTable();
            if (modelPenugasanReviewer.daftarRekapPenugasanSkema(ref dt, objLogin.idPersonal.ToString(), ddlProgram.SelectedValue,
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue,
                objLogin.idInstitusi.ToString()))
            {
                gvRekapPenugasanSkema.DataSource = dt;
                gvRekapPenugasanSkema.DataBind();
            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapPenugasanSkema();
        }

        protected void ddlSkema_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapPenugasanSkema();
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
            ddlThnPelaksanaan.SelectedIndex = 0;
            isiGvRekapPenugasanSkema();
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapPenugasanSkema();
        }

        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapPenugasanSkema();
        }

        private void isiDdlProgram()
        {
            DataTable dt = objManipData.filterData((DataTable)Application["ProgramHibah"], "kd_sts_aktif IN ('1') ");
            obj_uiDDL.bindToDropDownList(ref ddlProgram, dt, "program_hibah", "kd_program_hibah");
            ddlProgram.Items.Insert(0, new ListItem("-- Pilih --", "0"));
            ddlProgram.SelectedIndex = 0;
        }

        private void isiDdlThnUsulan()
        {
            ddlThnUsulan.Items.Clear();
            ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void isiDdlThnPelaksanaan()
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem("--Pilih--", "0000"));
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
        }

        private void isiTahapanKegiatan()
        {
            if (Application["TahapanKegiatan"] != null)
            {
                DataTable TahapanKegiatan = objManipData.filterData((DataTable)Application["TahapanKegiatan"], "kd_tahapan_kegiatan IN ('20','22','25','33','49') ");
                if (!obj_uiDDL.bindToDropDownList(ref ddlTahapan, TahapanKegiatan, "tahapan", "kd_tahapan_kegiatan"))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + obj_uiDDL.errorMessage);
                    return;
                }
            }
        }

        protected void gvRekapPenugasanSkema_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (ddlProgram.SelectedValue == "0" || ddlThnUsulan.SelectedValue == "0000" || ddlTahapan.SelectedValue == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Program hibah, Tahun Usulan, Tahun pelaksanaan, dan Tahapan harus dipilih");
                return;
            }
            else
            {
                int idSkema = int.Parse(gvRekapPenugasanSkema.DataKeys[e.RowIndex]["id_skema"].ToString());
                ViewState["IdSkema"] = idSkema;
                string namaSkema = gvRekapPenugasanSkema.DataKeys[e.RowIndex]["nama_skema"].ToString();
                ViewState["namaSkema"] = namaSkema;
                lblProgramPenugasan.Text = ddlProgram.SelectedItem.Text;
                lblSKemaPenugasan.Text = namaSkema;
                lblThnUsulanPenugasan.Text = ddlThnUsulan.SelectedItem.Text;
                lblThnPelaksanaanPenugasan.Text = ddlThnPelaksanaan.SelectedItem.Text;

                lblProgramReviewer.Text = ddlProgram.SelectedItem.Text;
                lblSkemaReviewer.Text = namaSkema;
                lblThnUsulanReviewer.Text = ddlThnUsulan.SelectedItem.Text;
                lblThnPelaksanaanReviewer.Text = ddlThnPelaksanaan.SelectedItem.Text;

                isiGvDaftarSudahDitugaskan(0);
                mvMain.SetActiveView(vDaftarSudahDitugaskan);
            }
        }

        #endregion

        #region Daftar Sudah ditugaskan

        protected void ddlJmlBarisPenugasan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarSudahDitugaskan(0);
        }

        protected void lbPenugasanReviewer_Click(object sender, EventArgs e)
        {
            isiGvDaftarReviewer(0);
            mvMain.SetActiveView(vReviewerBlmDitugaskan);
        }

        protected void lbKembaliPenugasan_Click(object sender, EventArgs e)
        {
            isiGvRekapPenugasanSkema();
            mvMain.SetActiveView(vRekapPenugasanSkema);
        }

        protected void gvDaftarPenugasan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["idPenugasanReviewer"] = gvDaftarPenugasan.DataKeys[e.RowIndex]["id_penugasan_reviewer"].ToString();

            lblNamaReviewerHapusDaftarPenugasan.Text = gvDaftarPenugasan.DataKeys[e.RowIndex]["nama_reviewer"].ToString();
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void lbHapusDaftarPenugasan_Click(object sender, EventArgs e)
        {
            if (modelPenugasanReviewer.deleteData(Guid.Parse(ViewState["idPenugasanReviewer"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
                isiGvDaftarSudahDitugaskan(0);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + modelPenugasanReviewer.errorMessage);
            }
        }

        protected void gvDaftarPenugasan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNomor = (Label)e.Row.FindControl("lblNoDaftarPenugasan");
                lblNomor.Text = (e.Row.RowIndex + 1 + int.Parse(ddlJmlBarisPenugasan.SelectedValue) * (pagingDaftarPenugasan.currentPage)).ToString();
            }
        }

        protected void daftarPenugasan_PageChanging(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());

            modelPenugasanReviewer.currentPage = pagingDaftarPenugasan.currentPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisPenugasan.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarPenugasan(idSkema, ddlThnUsulan.SelectedValue, ddlTahapan.SelectedValue,
                objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarPenugasan, modelPenugasanReviewer.currentRecords);
        }

        private void isiGvDaftarSudahDitugaskan(int idxPage)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());

            modelPenugasanReviewer.currentPage = idxPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisPenugasan.SelectedValue);

            if (!modelPenugasanReviewer.getJmlPenugasan(idSkema, ddlThnUsulan.SelectedValue, ddlTahapan.SelectedValue,
                objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            //NEW PAGING CONTROL

            pagingDaftarPenugasan.currentPage = idxPage;
            pagingDaftarPenugasan.setPaging(int.Parse(ddlJmlBarisPenugasan.SelectedValue), modelPenugasanReviewer.numOfRecords);

            if (!modelPenugasanReviewer.getDaftarPenugasan(idSkema, ddlThnUsulan.SelectedValue, ddlTahapan.SelectedValue,
                objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvDaftarPenugasan, modelPenugasanReviewer.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            if (modelPenugasanReviewer.numOfRecords < 1)
            {
                pagingDaftarPenugasan.setPaging(int.Parse(ddlJmlBarisPenugasan.SelectedValue), 1);
            }
        }

        protected void lbExcelPenugasan_Click(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());

            var dt = new DataTable();
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Penugasan Reviewer" + " Skema {0}" + " Tahun {1}.xlsx",
                    ViewState["namaSkema"].ToString(), ddlThnPelaksanaan.SelectedItem.Text);
                if (modelPenugasanReviewer.getExcelDaftarPenugasan(ref dt, idSkema, ddlThnUsulan.SelectedValue,
                    ddlTahapan.SelectedValue, objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("data");
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

        #endregion

        #region Daftar Reviewer Belum Ditugaskan

        private void isiGvDaftarReviewer(int idxPage)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());

            modelPenugasanReviewer.currentPage = idxPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisReviewer.SelectedValue);

            if (!modelPenugasanReviewer.getJmlReviewer(idSkema, ddlThnUsulan.SelectedValue, ddlTahapan.SelectedValue,
                objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue, "1", tbCariReviewer.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            //NEW PAGING CONTROL

            PagingReviewer.currentPage = idxPage;
            PagingReviewer.setPaging(int.Parse(ddlJmlBarisReviewer.SelectedValue), modelPenugasanReviewer.numOfRecords);

            if (!modelPenugasanReviewer.getDaftarReviewer(idSkema, ddlThnUsulan.SelectedValue, ddlTahapan.SelectedValue,
                objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue, "1", tbCariReviewer.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvDaftarReviewer, modelPenugasanReviewer.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            if (modelPenugasanReviewer.numOfRecords < 1)
            {
                PagingReviewer.setPaging(int.Parse(ddlJmlBarisReviewer.SelectedValue), 1);
            }
        }

        protected void ddlJmlBarisReviewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarReviewer(0);
        }

        protected void lbExcelReviewer_Click(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());

            var dt = new DataTable();
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Reviewer Blm Ditugaskan" + " Skema {0}" + " Tahun {1}.xlsx",
                    ViewState["namaSkema"].ToString(), ddlThnPelaksanaan.SelectedItem.Text);
                if (modelPenugasanReviewer.getExcelBlmDitugaskan(ref dt, idSkema, ddlThnUsulan.SelectedValue, ddlTahapan.SelectedValue,
                objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue, "1"))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("data");
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

        protected void lbCariReviewer_Click(object sender, EventArgs e)
        {
            isiGvDaftarReviewer(0);
        }

        protected void lbKembaliReviewer_Click(object sender, EventArgs e)
        {
            tbCariReviewer.Text = string.Empty;
            isiGvDaftarSudahDitugaskan(0);
            mvMain.SetActiveView(vDaftarSudahDitugaskan);
        }

        protected void gvDaftarReviewer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNomor = (Label)e.Row.FindControl("lblNoReviewer");
                lblNomor.Text = (e.Row.RowIndex + 1 + int.Parse(ddlJmlBarisReviewer.SelectedValue) * (PagingReviewer.currentPage)).ToString();
            }
        }

        protected void gvDaftarReviewer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid idReviewer = Guid.Parse(gvDaftarReviewer.DataKeys[e.RowIndex]["id_reviewer"].ToString());
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());

            if (modelPenugasanReviewer.insertDataBaru(idSkema, ddlThnUsulan.SelectedValue, idReviewer,
                ddlTahapan.SelectedValue, objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Reviewer berhasil ditugaskan");
                isiGvDaftarReviewer(0);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal" + modelPenugasanReviewer.errorMessage);
                isiGvDaftarReviewer(0);
            }
        }

        protected void PagingReviewer_PageChanging(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());

            modelPenugasanReviewer.currentPage = PagingReviewer.currentPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisReviewer.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarReviewer(idSkema, ddlThnUsulan.SelectedValue, ddlTahapan.SelectedValue,
                objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue, "1", tbCariReviewer.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarReviewer, modelPenugasanReviewer.currentRecords);
        }

        #endregion

        protected void lbExcelUsulanBaru_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format(" usulan " + ddlProgram.SelectedItem.Text + " " + " tahun {0}.xlsx", ddlThnUsulan.SelectedItem.Text);
                var dt = new DataTable();

                for (var idx = 0; idx < gvRekapPenugasanSkema.Rows.Count; idx++)
                {
                    string namaSingkatSkema = gvRekapPenugasanSkema.DataKeys[idx].Values["nama_singkat_skema"].ToString();
                    int idSkema = int.Parse(gvRekapPenugasanSkema.DataKeys[idx].Values["id_skema"].ToString());
                    if (gvRekapPenugasanSkema.Rows.Count > 0)
                    {
                        dt.Clear();

                        if (modelPenugasanReviewer.ListUsulanBaruExcelOptDRPM(ref dt, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue
                            , ddlThnPelaksanaan.SelectedValue, idSkema))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(namaSingkatSkema.ToString());
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
                    }
                    else
                    {
                        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Data Tidak Ditemukan !", modelPenugasanReviewer.errorMessage);
                        return;
                    }
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



                if (modelPenugasanReviewer.ListReviewerNasionalExcelOptDRPM(ref dt, ddlThnPelaksanaan.SelectedValue, ddlThnUsulan.SelectedValue
                    , ddlTahapan.SelectedValue, Guid.Parse(objLogin.idInstitusi.ToString())))
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
    }
}