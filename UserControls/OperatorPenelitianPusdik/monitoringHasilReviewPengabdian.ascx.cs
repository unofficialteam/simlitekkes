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
    public partial class monitoringHasilReviewPengabdian : System.Web.UI.UserControl
    {
        Models.login objLogin;

        Core.manipulasiData objManipData = new Core.manipulasiData();

        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiGridView obj_uiGridView = new uiGridView();
        uiModal objModal = new uiModal();

        Models.OperatorPenelitianPusdik.monitoringHasilReview modelHasilReviewPerSkema = new Models.OperatorPenelitianPusdik.monitoringHasilReview();
        Models.OperatorPenelitianPusdik.monitoringHasilReview modelHasilReviewPerPT = new Models.OperatorPenelitianPusdik.monitoringHasilReview();
        Models.OperatorPenelitianPusdik.monitoringHasilReview modelHasilReviewPerKegiatan = new Models.OperatorPenelitianPusdik.monitoringHasilReview();
        Models.OperatorPenelitianPusdik.monitoringHasilReview modelHasilReviewModal = new Models.OperatorPenelitianPusdik.monitoringHasilReview();
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
                    ViewState["KdProgHibah"] = "3"; //desentralisasi
                    lbnasional.CssClass = "btn btn-outline-success";
                    lbpt.CssClass = "btn btn-outline-secondary";
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
                isiGvHasilReviewPerSkema();
                mvHasilReview.SetActiveView(vHasilReviewPerSkema);
            }
        }

        #region Tampilan Hasil Review Per Skema
        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Tahapan"] = ddlTahapan.SelectedItem.Text;
            isiGvHasilReviewPerSkema();
        }

        protected void ddlTahunUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["TahunUsulan"] = ddlThnUsulan.SelectedItem.Text;
            isiGvHasilReviewPerSkema();
        }

        protected void ddlTahunPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["TahunPelaksanaan"] = ddlThnPelaksanaan.SelectedItem.Text;
            isiGvHasilReviewPerSkema();
        }
        protected void lbnasional_Click(object sender, EventArgs e)
        {
            lbnasional.CssClass = "btn btn-outline-success";
            lbpt.CssClass = "btn btn-outline-secondary";
            ViewState["KdProgHibah"] = "3";
            isiGvHasilReviewPerSkema();
        }

        protected void lbpt_Click(object sender, EventArgs e)
        {
            lbnasional.CssClass = "btn btn-outline-secondary";
            lbpt.CssClass = "btn btn-outline-success";
            ViewState["KdProgHibah"] = "7";
            isiGvHasilReviewPerSkema();
        }
        private void isiGvHasilReviewPerSkema()
        {
            var dt = new DataTable();
            if (modelHasilReviewPerSkema.listHasilReviewPerSkema(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue))
            {
                gvHasilReviewPerSkema.DataSource = dt;
                gvHasilReviewPerSkema.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_per_skema"] = dt.Rows[0]["jml_record"].ToString();
                lblTotalJudulKegiatan_perSkema.Text = dt.Rows[0]["total_judul"].ToString();
                lblPenilaianSelesai_perSkema.Text = dt.Rows[0]["total_data_selesai"].ToString();
                lblPenilaianBelumSelesai_perSkema.Text = dt.Rows[0]["total_data_blm_selesai"].ToString();
                lblPenilaianTelahDitetapkan_perSkema.Text = dt.Rows[0]["total_data_ditetapkan"].ToString();
            }
            else
            {
                ViewState["jml_record_per_skema"] = 1;
                lblTotalJudulKegiatan_perSkema.Text = "0";
                lblPenilaianSelesai_perSkema.Text = "0";
                lblPenilaianBelumSelesai_perSkema.Text = "0";
                lblPenilaianTelahDitetapkan_perSkema.Text = "0";
            }
        }

        protected void gvHasilReviewPerSkema_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DetailReviewPerSkema")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                int idSkema = int.Parse(gvHasilReviewPerSkema.DataKeys[idx]["id_skema"].ToString());
                string namaSkema = gvHasilReviewPerSkema.DataKeys[idx]["nama_skema"].ToString();

                ViewState["IdSkema"] = idSkema;
                ViewState["NamaSkema"] = namaSkema;

                if (ViewState["KdProgHibah"].ToString() == "3")
                    lblProgramKegiatan_perPT.Text = "Unggulan Nasional";
                else
                    lblProgramKegiatan_perPT.Text = "Unggulan Perguruan Tinggi";
                lblNamaSkema_perPT.Text = ViewState["NamaSkema"].ToString();
                lblTahapan_perPT.Text = ViewState["Tahapan"].ToString();
                lblThnUsulan_perPT.Text = ViewState["TahunUsulan"].ToString();
                lblThnPelaksanaan_perPT.Text = ViewState["TahunPelaksanaan"].ToString();

                isiGvHasilReviewPerPT();
                mvHasilReview.SetActiveView(vHasilReviewPerPT);
            }
        }
        #endregion

        #region Hasil Review Per PT
        protected void lbKembali_perPT_Click(object sender, EventArgs e)
        {
            isiGvHasilReviewPerSkema();
            mvHasilReview.SetActiveView(vHasilReviewPerSkema);
        }

        private void isiGvHasilReviewPerPT()
        {
            var dt = new DataTable();
            modelHasilReviewPerPT.currentPage = pagingHasilReviewPerPT.currentPage;
            modelHasilReviewPerPT.rowsPerPage = Int32.Parse(ddlJumlahBaris_perPT.SelectedValue);
            if (modelHasilReviewPerPT.listHasilReviewPerPT(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], tbPencarian_perPT.Text))
            {
                gvHasilReviewPerPT.DataSource = dt;
                gvHasilReviewPerPT.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_per_pt"] = dt.Rows[0]["total_perguruan_tinggi"].ToString();
                lblTotalPT_perPT.Text = dt.Rows[0]["total_perguruan_tinggi"].ToString();
                lblTotalJudulKegiatan_perPT.Text = dt.Rows[0]["total_proposal"].ToString();
                lblPenilaianSelesai_perPT.Text = dt.Rows[0]["total_data_selesai"].ToString();
                lblPenilaianBelumSelesai_perPT.Text = dt.Rows[0]["total_data_blm_selesai"].ToString();
                lblPenilaianTelahDitetapkan_perPT.Text = dt.Rows[0]["total_data_ditetapkan"].ToString();
            }
            else
            {
                ViewState["jml_record_per_pt"] = 1;
                lblTotalPT_perPT.Text = "0";
                lblTotalJudulKegiatan_perPT.Text = "0";
                lblPenilaianSelesai_perPT.Text = "0";
                lblPenilaianBelumSelesai_perPT.Text = "0";
                lblPenilaianTelahDitetapkan_perPT.Text = "0";
            }
            refreshPagingHasilReviewPerPT();
        }

        private void refreshPagingHasilReviewPerPT()
        {
            if (ViewState["jml_record_per_pt"] != null)
                pagingHasilReviewPerPT.setPaging(int.Parse(ddlJumlahBaris_perPT.SelectedValue), int.Parse(ViewState["jml_record_per_pt"].ToString()));
            else
                pagingHasilReviewPerPT.setPaging(1, 1);
            pagingHasilReviewPerPT.refreshPaging();
        }

        protected void daftarDataHasilReviewPerPT_PageChanging(object sender, EventArgs e)
        {
            modelHasilReviewPerPT.currentPage = pagingHasilReviewPerPT.currentPage;
            modelHasilReviewPerPT.rowsPerPage = int.Parse(ddlJumlahBaris_perPT.SelectedValue);
            if (!modelHasilReviewPerPT.getDaftarHasilReviewPerPT(ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], tbPencarian_perPT.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvHasilReviewPerPT.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvHasilReviewPerPT, modelHasilReviewPerPT.currentRecords);
        }

        protected void gvHasilReviewPerPT_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DetailReviewPerPT")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                Guid idPT = Guid.Parse(gvHasilReviewPerPT.DataKeys[idx]["id_institusi"].ToString());
                string kdPT = gvHasilReviewPerPT.DataKeys[idx]["kd_perguruan_tinggi"].ToString();
                string namaPT = gvHasilReviewPerPT.DataKeys[idx]["nama_institusi"].ToString();
                string namaKlaster = gvHasilReviewPerPT.DataKeys[idx]["nama_klaster"].ToString();
                string jmlJudul = gvHasilReviewPerPT.DataKeys[idx]["jml_proposal"].ToString();

                ViewState["IdPT"] = idPT;
                ViewState["KdPT"] = kdPT;
                ViewState["NamaPT"] = namaPT;
                ViewState["NamaKlaster"] = namaKlaster;
                ViewState["jmlJudul"] = jmlJudul;

                ViewState["StatusPenilaian"] = 0;

                if (ViewState["KdProgHibah"].ToString() == "3")
                    lblProgramKegiatan_perKegiatan.Text = "Unggulan Nasional";
                else
                    lblProgramKegiatan_perKegiatan.Text = "Unggulan Perguruan Tinggi";
                lblSkema_perKegiatan.Text = ViewState["NamaSkema"].ToString();
                lblTahapan_perKegiatan.Text = ViewState["Tahapan"].ToString();
                lblTahunUsulan_perKegiatan.Text = ViewState["TahunUsulan"].ToString();
                lblTahunPelaksanaan_perKegiatan.Text = ViewState["TahunPelaksanaan"].ToString();
                lblJumlahJudul_perKegiatan.Text = ViewState["jmlJudul"].ToString();
                lbNamaPT_perKegiatan.Text = ViewState["KdPT"].ToString() + " | " + ViewState["NamaPT"].ToString() + " - " + ViewState["NamaKlaster"].ToString();

                isiGvHasilReview_perKegiatan();
                gantiStatusProposal();
                mvHasilReview.SetActiveView(vHasilReviewPerKegiatan);
            }
        }

        protected void ddlJmlBarisPerPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvHasilReviewPerPT();
            refreshPagingHasilReviewPerPT();
        }

        protected void lbPencarian_perPT_Click(object sender, EventArgs e)
        {
            isiGvHasilReviewPerPT();
            refreshPagingHasilReviewPerPT();
        }

        protected void lbExcel_perPT_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Hasil Review Per PT.xlsx");
                var dt = new DataTable();
                if (modelHasilReviewPerPT.listHasilReviewPerPT(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], tbPencarian_perPT.Text))
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
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelHasilReviewPerPT.errorMessage);
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
            isiGvHasilReviewPerPT();
            refreshPagingHasilReviewPerPT();
            mvHasilReview.SetActiveView(vHasilReviewPerPT);
        }

        private void isiGvHasilReview_perKegiatan()
        {
            var dt = new DataTable();
            modelHasilReviewPerKegiatan.currentPage = pagingHasilReviewPerKegiatan.currentPage;
            modelHasilReviewPerKegiatan.rowsPerPage = Int32.Parse(ddlJmlBaris_perKegiatan.SelectedValue);
            if (modelHasilReviewPerKegiatan.listHasilReviewPerKegiatan(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], (Guid)ViewState["IdPT"], (int)ViewState["StatusPenilaian"], tbPencarian_perKegiatan.Text))
            {
                gvHasilReview_perKegiatan.DataSource = dt;
                gvHasilReview_perKegiatan.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_per_kegiatan"] = dt.Rows[0]["jml_record"].ToString();
                lblJumlahJudul_perKegiatan.Text = dt.Rows[0]["jml_record"].ToString();
            }
            else
            {
                ViewState["jml_record_per_kegiatan"] = 1;
                lblJumlahJudul_perKegiatan.Text = "0";
            }
            refreshPagingHasilReviewPerKegiatan();
        }

        private void refreshPagingHasilReviewPerKegiatan()
        {
            if (ViewState["jml_record_per_kegiatan"] != null)
                pagingHasilReviewPerKegiatan.setPaging(int.Parse(ddlJmlBaris_perKegiatan.SelectedValue), int.Parse(ViewState["jml_record_per_kegiatan"].ToString()));
            else
                pagingHasilReviewPerKegiatan.setPaging(1, 1);
            pagingHasilReviewPerKegiatan.refreshPaging();
        }

        protected void daftarDataHasilReviewPerKegiatan_PageChanging(object sender, EventArgs e)
        {
            modelHasilReviewPerKegiatan.currentPage = pagingHasilReviewPerKegiatan.currentPage;
            modelHasilReviewPerKegiatan.rowsPerPage = int.Parse(ddlJmlBaris_perKegiatan.SelectedValue);
            if (!modelHasilReviewPerKegiatan.getDaftarHasilReviewPerKegiatan(ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], (Guid)ViewState["IdPT"], (int)ViewState["StatusPenilaian"], tbPencarian_perKegiatan.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvHasilReview_perKegiatan.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvHasilReview_perKegiatan, modelHasilReviewPerKegiatan.currentRecords);
        }

        protected void gvHasilReview_perKegiatan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label hasil_review = (Label)e.Row.FindControl("lbHasilReview");
                Label hasil_penetapan = (Label)e.Row.FindControl("lbStatusPenetapan");
                if (ddlTahapan.SelectedValue.Equals("20"))
                {
                    string status_penetapan = gvHasilReview_perKegiatan.DataKeys[e.Row.RowIndex]["hasil_penetapan"].ToString().ToLower();
                    hasil_review.Text = gvHasilReview_perKegiatan.DataKeys[e.Row.RowIndex]["hasil_review"].ToString();

                    if (status_penetapan.Equals("lolos tahapan"))
                    {
                        hasil_penetapan.Text = "<i class='far fa-check-square fa-2x mr-2'></i>Lolos tahapan";
                    }
                    else if (status_penetapan.Equals("tidak lolos tahapan"))
                    {
                        hasil_penetapan.Text = "<i class='fas fa-times fa-2x mr-2'></i>Tidak lolos tahapan";
                    }
                    else
                    {
                        hasil_penetapan.Text = "Belum ditetapkan";
                    }
                }
                else
                {
                    hasil_review.Text = gvHasilReview_perKegiatan.DataKeys[e.Row.RowIndex]["hasil_review_evaluasi_substansi"].ToString();
                    string status_penetapan = gvHasilReview_perKegiatan.DataKeys[e.Row.RowIndex]["hasil_penetapan_evaluasi_substansi"].ToString().ToLower();

                    if (status_penetapan.Equals("didanai"))
                    {
                        hasil_penetapan.Text = "<i class='far fa-check-square fa-2x mr-2'></i>Didanai";
                    }
                    else if (status_penetapan.Equals("tidak didanai"))
                    {
                        hasil_penetapan.Text = "<i class='fas fa-times fa-2x mr-2'></i>Tidak Didanai";
                    }
                    else
                    {
                        hasil_penetapan.Text = "Belum ditetapkan";
                    }
                }
            }
        }

        protected void gvHasilReviewPerKegiatan_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DetailReviewPerKegiatan")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                Guid idUsulan = Guid.Parse(gvHasilReview_perKegiatan.DataKeys[idx]["id_usulan_kegiatan"].ToString());
                string nidnKetua = gvHasilReview_perKegiatan.DataKeys[idx]["nidn_ketua"].ToString();
                string namaKetua = gvHasilReview_perKegiatan.DataKeys[idx]["nama_ketua_usulan"].ToString();
                string namaPT = gvHasilReview_perKegiatan.DataKeys[idx]["nama_institusi"].ToString();
                string judul = gvHasilReview_perKegiatan.DataKeys[idx]["judul"].ToString();
                string nilai = gvHasilReview_perKegiatan.DataKeys[idx]["rata_rata_nilai"].ToString();

                ViewState["IdUsulan"] = idUsulan;

                if (ViewState["KdProgHibah"].ToString() == "3")
                    lblProgramKegiatan_modal.Text = "Unggulan Nasional";
                else
                    lblProgramKegiatan_modal.Text = "Unggulan Perguruan Tinggi";
                lblSkema_modal.Text = ViewState["NamaSkema"].ToString();
                lblTahapan_modal.Text = ViewState["Tahapan"].ToString();
                lblTahunUsulan_modal.Text = ViewState["TahunUsulan"].ToString();
                lblTahunPelaksanaan_modal.Text = ViewState["TahunPelaksanaan"].ToString();
                lblNidnNama_modal.Text = nidnKetua + " <b>" + namaKetua + "</b>" + " - " + namaPT;
                lblJudul_modal.Text = judul;

                if (ddlTahapan.SelectedValue.Equals("20"))
                {
                    lblNilai_modal.Text = "";
                }
                else
                {
                    lblNilai_modal.Text = nilai;
                }
                isiGvHasilReviewModal();
                objModal.ShowModal(this.Page, "modalDetail");
            }
        }

        private void gantiStatusProposal()
        {
            switch (ViewState["StatusPenilaian"].ToString())
            {
                case "0":
                    lbSemua_perKegiatan.CssClass = "btn btn-outline-success";
                    lbPenilaianSelesai_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPenilaianBelumSelesai_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbTelahDitetapkan_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbBelumDitetapkan_perKegiatan.CssClass = "btn btn-outline-secondary";
                    break;
                case "1":
                    lbSemua_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPenilaianSelesai_perKegiatan.CssClass = "btn btn-outline-success";
                    lbPenilaianBelumSelesai_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbTelahDitetapkan_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbBelumDitetapkan_perKegiatan.CssClass = "btn btn-outline-secondary";
                    break;
                case "2":
                    lbSemua_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPenilaianSelesai_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPenilaianBelumSelesai_perKegiatan.CssClass = "btn btn-outline-success";
                    lbTelahDitetapkan_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbBelumDitetapkan_perKegiatan.CssClass = "btn btn-outline-secondary";
                    break;
                case "3":
                    lbSemua_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPenilaianSelesai_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPenilaianBelumSelesai_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbTelahDitetapkan_perKegiatan.CssClass = "btn btn-outline-success";
                    lbBelumDitetapkan_perKegiatan.CssClass = "btn btn-outline-secondary";
                    break;
                case "4":
                    lbSemua_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPenilaianSelesai_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbPenilaianBelumSelesai_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbTelahDitetapkan_perKegiatan.CssClass = "btn btn-outline-secondary";
                    lbBelumDitetapkan_perKegiatan.CssClass = "btn btn-outline-success";
                    break;
            }
        }

        protected void ddlJmlBarisPerKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvHasilReview_perKegiatan();
            refreshPagingHasilReviewPerKegiatan();
        }

        protected void lbPencarian_perKegiatan_Click(object sender, EventArgs e)
        {
            isiGvHasilReview_perKegiatan();
            refreshPagingHasilReviewPerKegiatan();
        }

        protected void lbSemua_perKegiatan_Click(object sender, EventArgs e)
        {
            ViewState["StatusPenilaian"] = 0;
            gantiStatusProposal();
            isiGvHasilReview_perKegiatan();
            refreshPagingHasilReviewPerKegiatan();
        }

        protected void lbPenilaianSelesai_perKegiatan_Click(object sender, EventArgs e)
        {
            ViewState["StatusPenilaian"] = 1;
            gantiStatusProposal();
            isiGvHasilReview_perKegiatan();
            refreshPagingHasilReviewPerKegiatan();
        }

        protected void lbPenilaianBelumSelesai_perKegiatan_Click(object sender, EventArgs e)
        {
            ViewState["StatusPenilaian"] = 2;
            gantiStatusProposal();
            isiGvHasilReview_perKegiatan();
            refreshPagingHasilReviewPerKegiatan();
        }

        protected void lbTelahDitetapkan_perKegiatan_Click(object sender, EventArgs e)
        {
            ViewState["StatusPenilaian"] = 3;
            gantiStatusProposal();
            isiGvHasilReview_perKegiatan();
            refreshPagingHasilReviewPerKegiatan();
        }

        protected void lbBelumDitetapkan_perKegiatan_Click(object sender, EventArgs e)
        {
            ViewState["StatusPenilaian"] = 4;
            gantiStatusProposal();
            isiGvHasilReview_perKegiatan();
            refreshPagingHasilReviewPerKegiatan();
        }

        protected void lbExcel_perKegiatan_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Hasil Review Per Judul Kegiatan.xlsx");
                var dt = new DataTable();
                if (modelHasilReviewPerKegiatan.listHasilReviewPerKegiatan(ref dt, ViewState["KdProgHibah"].ToString(), ViewState["TahunUsulan"].ToString(), ViewState["TahunPelaksanaan"].ToString(), ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], (Guid)ViewState["IdPT"], (int)ViewState["StatusPenilaian"], tbPencarian_perKegiatan.Text))
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
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelHasilReviewPerKegiatan.errorMessage);
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
        private void isiGvHasilReviewModal()
        {
            var dt = new DataTable();
            modelHasilReviewModal.currentPage = pagingDataModal.currentPage;
            modelHasilReviewModal.rowsPerPage = 5;
            if (modelHasilReviewModal.listHasilReviewModal(ref dt, (Guid)ViewState["IdUsulan"], ddlTahapan.SelectedValue, ""))
            {
                gvHasilReviewModal.DataSource = dt;
                gvHasilReviewModal.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record_modal"] = dt.Rows[0]["jml_record"].ToString();
            }
            else
            {
                ViewState["jml_record_modal"] = 1;
            }
            refreshPagingHasilReviewModal();
        }

        protected void gvHasilReviewModal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label hasil_review = (Label)e.Row.FindControl("lblHasilReview");
                if (ddlTahapan.SelectedValue.Equals("20"))
                {
                    hasil_review.Text = gvHasilReviewModal.DataKeys[e.Row.RowIndex]["hasil_review"].ToString();
                }
                else
                {
                    hasil_review.Text = gvHasilReviewModal.DataKeys[e.Row.RowIndex]["nilai_reviewer"].ToString();
                    if (hasil_review.Text == "")
                    {
                        hasil_review.Text = "-";
                    }
                }
            }
        }

        private void refreshPagingHasilReviewModal()
        {
            if (ViewState["jml_record_modal"] != null)
                pagingDataModal.setPaging(5, int.Parse(ViewState["jml_record_modal"].ToString()));
            else
                pagingDataModal.setPaging(1, 1);
            pagingDataModal.refreshPaging();
        }

        protected void daftarDataHasilReviewModal_PageChanging(object sender, EventArgs e)
        {
            string jsScript = "$('.modal-backdrop').hide();";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "removeBackDrop", jsScript, true);
            modelHasilReviewModal.currentPage = pagingDataModal.currentPage;
            modelHasilReviewModal.rowsPerPage = 5;
            if (!modelHasilReviewModal.getDaftarHasilReviewModal((Guid)ViewState["IdUsulan"], ddlTahapan.SelectedValue, ""))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvHasilReviewModal.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvHasilReviewModal, modelHasilReviewModal.currentRecords);
            objModal.ShowModal(this.Page, "modalDetail");
        }
        #endregion

        #region private method
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
        #endregion
    }
}