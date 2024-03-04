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
    public partial class monitoringPlottingReviewer : System.Web.UI.UserControl
    {
        Models.login objLogin;

        Core.manipulasiData objManipData = new Core.manipulasiData();

        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiGridView obj_uiGridView = new uiGridView();
        uiModal objModal = new uiModal();

        Models.OperatorPenelitianPusdik.monitoringPlottingReviewer modelPlotingReviewerPerSkema = new Models.OperatorPenelitianPusdik.monitoringPlottingReviewer();
        Models.OperatorPenelitianPusdik.monitoringPlottingReviewer modelPlotingReviewerPerPT = new Models.OperatorPenelitianPusdik.monitoringPlottingReviewer();
        Models.OperatorPenelitianPusdik.monitoringPlottingReviewer modelPlotingReviewerPerKegiatan = new Models.OperatorPenelitianPusdik.monitoringPlottingReviewer();
        Models.OperatorPenelitianPusdik.monitoringPlottingReviewer modelPlotingReviewerPerModal = new Models.OperatorPenelitianPusdik.monitoringPlottingReviewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                // set jenis hibah
                if (ViewState["KdProgHibah"] == null)
                {
                    ViewState["KdProgHibah"] = "1"; //desentralisasi
                    lbDesentralisasi.CssClass = "btn btn-outline-success";
                    lbPenugasan.CssClass = "btn btn-outline-secondary";
                }

                // set pilihan
                isiTahapanKegiatan();
                ddlTahapan.SelectedIndex = 0;
                ViewState["Tahapan"] = ddlTahapan.SelectedItem.Text;
                isiDdlThnUsulan();
                ddlThnUsulan.SelectedIndex = 0;
                ViewState["TahunUsulan"] = ddlThnUsulan.SelectedItem.Text;
                isiDdlThnPelaksanaan();
                ddlThnPelaksanaan.SelectedIndex = 0;
                ViewState["TahunPelaksanaan"] = ddlThnPelaksanaan.SelectedItem.Text;

                // Default View Active = Hasil Review Per Skema
                isiGvPlotingReviewerPerSkema();
                mvPlotingReviewer.SetActiveView(vPlotingReviewer_perSkema);
            }
        }

        #region Monitoring Ploting Reviewer Per Skema
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

        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Tahapan"] = ddlTahapan.SelectedItem.Text;
            isiGvPlotingReviewerPerSkema();
        }

        protected void ddlTahunUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["TahunUsulan"] = ddlThnUsulan.SelectedItem.Text;
            isiGvPlotingReviewerPerSkema();
        }

        protected void ddlTahunPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["TahunPelaksanaan"] = ddlThnPelaksanaan.SelectedItem.Text;
            isiGvPlotingReviewerPerSkema();
        }
        protected void lbDesentralisasi_Click(object sender, EventArgs e)
        {
            lbDesentralisasi.CssClass = "btn btn-outline-success";
            lbPenugasan.CssClass = "btn btn-outline-secondary";
            ViewState["KdProgHibah"] = "1";
            isiGvPlotingReviewerPerSkema();
        }

        protected void lbPenugasan_Click(object sender, EventArgs e)
        {
            lbDesentralisasi.CssClass = "btn btn-outline-secondary";
            lbPenugasan.CssClass = "btn btn-outline-success";
            ViewState["KdProgHibah"] = "6";
            isiGvPlotingReviewerPerSkema();
        }
        private void isiGvPlotingReviewerPerSkema()
        {
            var dt = new DataTable();
            if (modelPlotingReviewerPerSkema.listPlotingReviewerPerSkema(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue))
            {
                gvPlotingReviewerPerSkema.DataSource = dt;
                gvPlotingReviewerPerSkema.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_per_skema"] = dt.Rows[0]["jml_record"].ToString();
                lblTotalProposal_perSkema.Text = dt.Rows[0]["total_judul"].ToString();
                lblPlotingReviewerLengkap_perSkema.Text = dt.Rows[0]["total_plotting_lengkap"].ToString();
                lblPlotingReviewerBelumLengkap_perSkema.Text = dt.Rows[0]["total_plotting_blm_lengkap"].ToString();
            }
            else
            {
                ViewState["jml_record_per_skema"] = 1;
                lblTotalProposal_perSkema.Text = "0";
                lblPlotingReviewerLengkap_perSkema.Text = "0";
                lblPlotingReviewerBelumLengkap_perSkema.Text = "0";
            }
        }

        protected void gvPlotingReviewerPerSkema_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DetailPlotingPerSkema")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                int idSkema = int.Parse(gvPlotingReviewerPerSkema.DataKeys[idx]["id_skema"].ToString());
                string namaSkema = gvPlotingReviewerPerSkema.DataKeys[idx]["nama_skema"].ToString();

                ViewState["IdSkema"] = idSkema;
                ViewState["NamaSkema"] = namaSkema;

                if (ViewState["KdProgHibah"].ToString() == "1")
                    lblProgramKegiatan_perPT.Text = "Penelitian Desentralisasi";
                else
                    lblProgramKegiatan_perPT.Text = "Penelitian Penugasan";
                lblNamaSkema_perPT.Text = ViewState["NamaSkema"].ToString();
                lblTahapan_perPT.Text = ViewState["Tahapan"].ToString();
                lblThnUsulan_perPT.Text = ViewState["TahunUsulan"].ToString();
                lblThnPelaksanaan_perPT.Text = ViewState["TahunPelaksanaan"].ToString();

                isiGvPlotingReviewerPerPT();
                mvPlotingReviewer.SetActiveView(vPlotingReviewer_perPT);
            }
        }

        protected void lbExcel_perSkema_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Ploting Reviewer Per Skema.xlsx");
                var dt = new DataTable();
                if (modelPlotingReviewerPerSkema.listPlotingReviewerPerSkema(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue))
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
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPlotingReviewerPerSkema.errorMessage);
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
        #endregion

        #region
        protected void lbKembali_perPT_Click(object sender, EventArgs e)
        {
            isiGvPlotingReviewerPerSkema();
            mvPlotingReviewer.SetActiveView(vPlotingReviewer_perSkema);
        }

        private void isiGvPlotingReviewerPerPT()
        {
            var dt = new DataTable();
            modelPlotingReviewerPerPT.currentPage = pagingPlottingReviewerPerPT.currentPage;
            modelPlotingReviewerPerPT.rowsPerPage = Int32.Parse(ddlJumlahBaris_perPT.SelectedValue);
            if (modelPlotingReviewerPerPT.listPlottingReviewerPerPT(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], tbPencarian_perPT.Text))
            {
                gvPlottingReviewerPerPT.DataSource = dt;
                gvPlottingReviewerPerPT.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_per_pt"] = dt.Rows[0]["total_perguruan_tinggi"].ToString();
                lblTotalProposal_perPT.Text = dt.Rows[0]["total_proposal"].ToString();
                lblPlotingReviewerLengkap_perPT.Text = dt.Rows[0]["total_plotting_selesai"].ToString();
                lblPlotingReviewerBelumLengkap_perPT.Text = dt.Rows[0]["total_plotting_blm_selesai"].ToString();
                lblJmlReviewer_perPT.Text = dt.Rows[0]["jml_reviewer_on_skema"].ToString(); ;
            }
            else
            {
                ViewState["jml_record_per_pt"] = 1;
                lblTotalProposal_perPT.Text = "0";
                lblPlotingReviewerLengkap_perPT.Text = "0";
                lblPlotingReviewerBelumLengkap_perPT.Text = "0";
                lblJmlReviewer_perPT.Text = "0";
            }
            refreshPagingPlottingReviewerPerPT();
        }

        private void refreshPagingPlottingReviewerPerPT()
        {
            if (ViewState["jml_record_per_pt"] != null)
                pagingPlottingReviewerPerPT.setPaging(int.Parse(ddlJumlahBaris_perPT.SelectedValue), int.Parse(ViewState["jml_record_per_pt"].ToString()));
            else
                pagingPlottingReviewerPerPT.setPaging(1, 1);
            pagingPlottingReviewerPerPT.refreshPaging();
        }

        protected void daftarDataPlottingReviewerPerPT_PageChanging(object sender, EventArgs e)
        {
            modelPlotingReviewerPerPT.currentPage = pagingPlottingReviewerPerPT.currentPage;
            modelPlotingReviewerPerPT.rowsPerPage = int.Parse(ddlJumlahBaris_perPT.SelectedValue);
            if (!modelPlotingReviewerPerPT.getDaftarPlottingReviewerPerPT(ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], tbPencarian_perPT.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvPlottingReviewerPerPT.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvPlottingReviewerPerPT, modelPlotingReviewerPerPT.currentRecords);
        }

        protected void gvPlottingReviewerPerPT_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DetailReviewerPerPT")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                Guid idPT = Guid.Parse(gvPlottingReviewerPerPT.DataKeys[idx]["id_institusi"].ToString());
                string kdPT = gvPlottingReviewerPerPT.DataKeys[idx]["kd_perguruan_tinggi"].ToString();
                string namaPT = gvPlottingReviewerPerPT.DataKeys[idx]["nama_institusi"].ToString();
                string namaKlaster = gvPlottingReviewerPerPT.DataKeys[idx]["nama_klaster"].ToString();
                string jmlJudul = gvPlottingReviewerPerPT.DataKeys[idx]["jml_proposal"].ToString();

                ViewState["IdPT"] = idPT;
                ViewState["KdPT"] = kdPT;
                ViewState["NamaPT"] = namaPT;
                ViewState["NamaKlaster"] = namaKlaster;
                ViewState["jmlJudul"] = jmlJudul;

                ViewState["StatusPlotting"] = 0;

                if (ViewState["KdProgHibah"].ToString() == "1")
                    lblProgramKegiatan_perKegiatan.Text = "Penelitian Desentralisasi";
                else
                    lblProgramKegiatan_perKegiatan.Text = "Penelitian Penugasan";
                lblSkema_perKegiatan.Text = ViewState["NamaSkema"].ToString();
                lblTahapan_perKegiatan.Text = ViewState["Tahapan"].ToString();
                lblTahunUsulan_perKegiatan.Text = ViewState["TahunUsulan"].ToString();
                lblTahunPelaksanaan_perKegiatan.Text = ViewState["TahunPelaksanaan"].ToString();
                lblJumlahJudul_perKegiatan.Text = ViewState["jmlJudul"].ToString();
                lbNamaPT_perKegiatan.Text = ViewState["KdPT"].ToString() + " | " + ViewState["NamaPT"].ToString() + " - " + ViewState["NamaKlaster"].ToString();

                isiGvPlottingReviewer_perKegiatan();
                gantiStatusProposal();
                mvPlotingReviewer.SetActiveView(vPlotingReviewer_perKegiatan);
            }
        }

        protected void ddlJmlBarisPerPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvPlotingReviewerPerPT();
            refreshPagingPlottingReviewerPerPT();
        }

        protected void lbPencarian_perPT_Click(object sender, EventArgs e)
        {
            isiGvPlotingReviewerPerPT();
            refreshPagingPlottingReviewerPerPT();
        }

        protected void lbExcel_perPT_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Monitoring Plotting Reviewer Per PT.xlsx");
                var dt = new DataTable();
                if (modelPlotingReviewerPerPT.listPlottingReviewerPerPT(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], tbPencarian_perPT.Text))
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
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPlotingReviewerPerPT.errorMessage);
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
        #endregion

        #region Hasi Review Per Kegiatan
        protected void lbKembali_perKegiatan_Click(object sender, EventArgs e)
        {
            isiGvPlotingReviewerPerPT();
            refreshPagingPlottingReviewerPerPT(); 
            mvPlotingReviewer.SetActiveView(vPlotingReviewer_perPT);
        }

        private void isiGvPlottingReviewer_perKegiatan()
        {
            var dt = new DataTable();
            modelPlotingReviewerPerKegiatan.currentPage = pagingPlottingReviewerPerKegiatan.currentPage;
            modelPlotingReviewerPerKegiatan.rowsPerPage = Int32.Parse(ddlJmlBaris_perKegiatan.SelectedValue);
            if (modelPlotingReviewerPerKegiatan.listPlottingReviewerPerKegiatan(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], (Guid)ViewState["IdPT"], (int)ViewState["StatusPlotting"], tbPencarian_perKegiatan.Text))
            {
                gvPlottingReviewer_perKegiatan.DataSource = dt;
                gvPlottingReviewer_perKegiatan.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_per_kegiatan"] = dt.Rows[0]["jml_record"].ToString();
                lblJumlahJudul_perKegiatan.Text = dt.Rows[0]["jml_record"].ToString();
                lblJmlReviewer_perKegiatan.Text = dt.Rows[0]["total_reviewer_on_institusi"].ToString();
            }
            else
            {
                ViewState["jml_record_per_kegiatan"] = 1;
                lblJumlahJudul_perKegiatan.Text = "0";
                lblJmlReviewer_perKegiatan.Text = "0";
            }
            refreshPagingPlottingReviewerPerKegiatan();
        }

        private void refreshPagingPlottingReviewerPerKegiatan()
        {
            if (ViewState["jml_record_per_kegiatan"] != null)
                pagingPlottingReviewerPerKegiatan.setPaging(int.Parse(ddlJmlBaris_perKegiatan.SelectedValue), int.Parse(ViewState["jml_record_per_kegiatan"].ToString()));
            else
                pagingPlottingReviewerPerKegiatan.setPaging(1, 1);
            pagingPlottingReviewerPerKegiatan.refreshPaging();
        }

        protected void daftarDataPlottingReviewerPerKegiatan_PageChanging(object sender, EventArgs e)
        {
            modelPlotingReviewerPerKegiatan.currentPage = pagingPlottingReviewerPerKegiatan.currentPage;
            modelPlotingReviewerPerKegiatan.rowsPerPage = int.Parse(ddlJmlBaris_perKegiatan.SelectedValue);
            if (!modelPlotingReviewerPerKegiatan.getDaftarPlottingReviewerPerKegiatan(ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], (Guid)ViewState["IdPT"], (int)ViewState["StatusPlotting"], tbPencarian_perKegiatan.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvPlottingReviewer_perKegiatan.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvPlottingReviewer_perKegiatan, modelPlotingReviewerPerKegiatan.currentRecords);
        }

        protected void gvPlottingReviewerPerKegiatan_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DetailReviewerPerKegiatan")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                Guid idUsulan = Guid.Parse(gvPlottingReviewer_perKegiatan.DataKeys[idx]["id_usulan_kegiatan"].ToString());
                string nidnKetua = gvPlottingReviewer_perKegiatan.DataKeys[idx]["nidn_ketua"].ToString();
                string namaKetua = gvPlottingReviewer_perKegiatan.DataKeys[idx]["nama_ketua_usulan"].ToString();
                string namaPT = gvPlottingReviewer_perKegiatan.DataKeys[idx]["nama_institusi"].ToString();
                string judul = gvPlottingReviewer_perKegiatan.DataKeys[idx]["judul"].ToString();
                string jmlAnggota = gvPlottingReviewer_perKegiatan.DataKeys[idx]["jml_personil_penelitian"].ToString();

                ViewState["IdUsulan"] = idUsulan;

                if (ViewState["KdProgHibah"].ToString() == "1")
                    lblProgramKegiatan_modal.Text = "Penelitian Desentralisasi";
                else
                    lblProgramKegiatan_modal.Text = "Penelitian Penugasan";
                lblSkema_modal.Text = ViewState["NamaSkema"].ToString();
                lblTahapan_modal.Text = ViewState["Tahapan"].ToString();
                lblTahunUsulan_modal.Text = ViewState["TahunUsulan"].ToString();
                lblTahunPelaksanaan_modal.Text = ViewState["TahunPelaksanaan"].ToString();
                lblNamaNIDN_modal.Text = "Ketua: " +  nidnKetua + " <b>" + namaKetua + "</b>" + " | Jml. Anggota: " + jmlAnggota;
                lblJudul_modal.Text = judul;
                isiGvPlottingReviewerModal();
                objModal.ShowModal(this.Page, "modalDetail");
            }
        }

        private void gantiStatusProposal()
        {
            switch (ViewState["StatusPlotting"].ToString())
            {
                case "0":
                    lbSemua_perKegiatan.CssClass = "btn btn-outline-success";
                    lbPlottingLengkap_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPlottingBlmLengkap_perKegiatan.CssClass = "btn btn-outline-secondary";
                    break;
                case "1":
                    lbSemua_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPlottingLengkap_perKegiatan.CssClass = "btn btn-outline-success";
                    lbPlottingBlmLengkap_perKegiatan.CssClass = "btn btn-outline-secondary";
                    break;
                case "2":
                    lbSemua_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPlottingLengkap_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPlottingBlmLengkap_perKegiatan.CssClass = "btn btn-outline-success";
                    break;
            }
        }

        protected void ddlJmlBarisPerKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvPlottingReviewer_perKegiatan();
            refreshPagingPlottingReviewerPerKegiatan();
        }

        protected void lbPencarian_perKegiatan_Click(object sender, EventArgs e)
        {
            isiGvPlottingReviewer_perKegiatan();
            refreshPagingPlottingReviewerPerKegiatan();
        }

        protected void lbSemua_perKegiatan_Click(object sender, EventArgs e)
        {
            ViewState["StatusPlotting"] = 0;
            gantiStatusProposal();
            isiGvPlottingReviewer_perKegiatan();
            refreshPagingPlottingReviewerPerKegiatan();
        }

        protected void lbPlottingLengkap_perKegiatan_Click(object sender, EventArgs e)
        {
            ViewState["StatusPlotting"] = 1;
            gantiStatusProposal();
            isiGvPlottingReviewer_perKegiatan();
            refreshPagingPlottingReviewerPerKegiatan();
        }

        protected void lbPlottingBlmLengkap_perKegiatan_Click(object sender, EventArgs e)
        {
            ViewState["StatusPlotting"] = 2;
            gantiStatusProposal();
            isiGvPlottingReviewer_perKegiatan();
            refreshPagingPlottingReviewerPerKegiatan();
        }

        protected void lbExcel_perKegiatan_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Hasil Review Per Judul Kegiatan.xlsx");
                var dt = new DataTable();
                if (modelPlotingReviewerPerKegiatan.listPlottingReviewerPerKegiatan(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], (Guid)ViewState["IdPT"], (int)ViewState["StatusPlotting"], tbPencarian_perKegiatan.Text))
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
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPlotingReviewerPerKegiatan.errorMessage);
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
        #endregion

        #region Hasil Review Modal
        private void isiGvPlottingReviewerModal()
        {
            var dt = new DataTable();
            modelPlotingReviewerPerModal.currentPage = pagingDataModal.currentPage;
            modelPlotingReviewerPerModal.rowsPerPage = 5;
            if (modelPlotingReviewerPerModal.listPlottingReviewerModal(ref dt, (Guid)ViewState["IdUsulan"], ddlTahapan.SelectedValue))
            {
                gvplottingReviewerModal.DataSource = dt;
                gvplottingReviewerModal.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_modal"] = dt.Rows[0]["jml_record"].ToString();
            }
            else
            {
                ViewState["jml_record_modal"] = 1;
            }
            refreshPagingPlottingReviewerModal();
        }

        private void refreshPagingPlottingReviewerModal()
        {
            if (ViewState["jml_record_modal"] != null)
                pagingDataModal.setPaging(5, int.Parse(ViewState["jml_record_modal"].ToString()));
            else
                pagingDataModal.setPaging(1, 1);
            pagingDataModal.refreshPaging();
        }

        protected void daftarDataPlottingReviewerModal_PageChanging(object sender, EventArgs e)
        {
            string jsScript = "$('.modal-backdrop').hide();";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "removeBackDrop", jsScript, true);
            modelPlotingReviewerPerModal.currentPage = pagingDataModal.currentPage;
            modelPlotingReviewerPerModal.rowsPerPage = 5;
            if (!modelPlotingReviewerPerModal.getDaftarPlottingReviewerModal((Guid)ViewState["IdUsulan"], ddlTahapan.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvplottingReviewerModal.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvplottingReviewerModal, modelPlotingReviewerPerModal.currentRecords);
            objModal.ShowModal(this.Page, "modalDetail");
        }
        #endregion
    }
}