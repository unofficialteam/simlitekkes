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

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class hasilReview : System.Web.UI.UserControl
    {
        Models.login objLogin;

        Core.manipulasiData objManipData = new Core.manipulasiData();

        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiGridView obj_uiGridView = new uiGridView();
        uiModal objModal = new uiModal();

        Models.PT.hasilReview modelHasilReviewPerSkema = new Models.PT.hasilReview();
        Models.PT.hasilReview modelHasilReviewPerKegiatan = new Models.PT.hasilReview();
        Models.PT.hasilReview modelHasilReviewModal = new Models.PT.hasilReview();

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
                isiGvHasilReviewPerSkema();
                mvHasilReview.SetActiveView(vHasilReviewPerSkema);
            }

        }

        #region Hasil Review Per Skema
        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Tahapan"] = ddlTahapan.SelectedItem.Text;
            isiGvHasilReviewPerSkema();
        }

        protected void ddlTahunUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["TahunUsulan"] = ddlThnUsulan.SelectedItem.Text;
            isiDdlThnPelaksanaan();
            isiGvHasilReviewPerSkema();
        }

        protected void ddlTahunPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["TahunPelaksanaan"] = ddlThnPelaksanaan.SelectedItem.Text;
            isiGvHasilReviewPerSkema();
        }
        protected void lbDesentralisasi_Click(object sender, EventArgs e)
        {
            lbDesentralisasi.CssClass = "btn btn-outline-success";
            lbPenugasan.CssClass = "btn btn-outline-secondary";
            ViewState["KdProgHibah"] = "1";
            isiGvHasilReviewPerSkema();
        }

        protected void lbPenugasan_Click(object sender, EventArgs e)
        {
            lbDesentralisasi.CssClass = "btn btn-outline-secondary";
            lbPenugasan.CssClass = "btn btn-outline-success";
            ViewState["KdProgHibah"] = "6";
            isiGvHasilReviewPerSkema();
        }
        private void isiGvHasilReviewPerSkema()
        {
            var dt = new DataTable();
            if (modelHasilReviewPerSkema.listHasilReviewPerSkema(ref dt, ViewState["KdProgHibah"].ToString(), 
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, 
                ddlTahapan.SelectedValue, objLogin.idInstitusi))
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
        }

        protected void gvHasilReviewPerSkema_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DetailReviewPerSkema")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                Guid idPT = objLogin.idInstitusi;
                string kdPT = objLogin.kdInstitusi;
                string namaPT = objLogin.namaInstitusi;
                string namaKlaster = gvHasilReviewPerSkema.DataKeys[idx]["nama_klaster"].ToString();
                string jmlJudul = gvHasilReviewPerSkema.DataKeys[idx]["jml_judul"].ToString();
                string namaSkema = gvHasilReviewPerSkema.DataKeys[idx]["nama_skema"].ToString();
                int idSkema = Int32.Parse(gvHasilReviewPerSkema.DataKeys[idx]["id_skema"].ToString());

                ViewState["IdPT"] = idPT;
                ViewState["KdPT"] = kdPT;
                ViewState["NamaPT"] = namaPT;
                ViewState["NamaKlaster"] = namaKlaster;
                ViewState["jmlJudul"] = jmlJudul;
                ViewState["NamaSkema"] = namaSkema;
                ViewState["IdSkema"] = idSkema;

                ViewState["StatusPenilaian"] = 0;

                if (ViewState["KdProgHibah"].ToString() == "1")
                    lblProgramKegiatan_perKegiatan.Text = "Penelitian Desentralisasi";
                else
                    lblProgramKegiatan_perKegiatan.Text = "Penelitian Penugasan";
                lblSkema_perKegiatan.Text = ViewState["NamaSkema"].ToString();
                lblTahapan_perKegiatan.Text = ViewState["Tahapan"].ToString();
                lblTahunUsulan_perKegiatan.Text = ddlThnUsulan.SelectedItem.ToString();
                lblTahunPelaksanaan_perKegiatan.Text = ddlThnPelaksanaan.SelectedItem.ToString();
                lblJumlahJudul_perKegiatan.Text = ViewState["jmlJudul"].ToString();
                lbNamaPT_perKegiatan.Text = ViewState["KdPT"].ToString() + " | " + ViewState["NamaPT"].ToString() + " - " + ViewState["NamaKlaster"].ToString();

                isiGvHasilReview_perKegiatan();
                gantiStatusProposal();
                mvHasilReview.SetActiveView(vHasilReviewPerKegiatan);
            }
        }
        #endregion

        #region Hasi Review Per Kegiatan
        protected void lbKembali_perKegiatan_Click(object sender, EventArgs e)
        {
            isiGvHasilReviewPerSkema();
            mvHasilReview.SetActiveView(vHasilReviewPerSkema);
        }

        private void isiGvHasilReview_perKegiatan()
        {
            var dt = new DataTable();
            modelHasilReviewPerKegiatan.currentPage = pagingHasilReviewPerKegiatan.currentPage;
            modelHasilReviewPerKegiatan.rowsPerPage = Int32.Parse(ddlJmlBaris_perKegiatan.SelectedValue);
            try
            {
                if (modelHasilReviewPerKegiatan.listHasilReviewPerKegiatan(ref dt, ViewState["KdProgHibah"].ToString(), 
                   ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, 
                   ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], (Guid)ViewState["IdPT"], 
                   (int)ViewState["StatusPenilaian"], tbPencarian_perKegiatan.Text))
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
            }
            catch(Exception ex)
            {
                string error = ex.Message;
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
            if (!modelHasilReviewPerKegiatan.getDaftarHasilReviewPerKegiatan(ViewState["KdProgHibah"].ToString(), 
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, 
                ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], (Guid)ViewState["IdPT"], 
                (int)ViewState["StatusPenilaian"], tbPencarian_perKegiatan.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvHasilReview_perKegiatan.EditIndex = -1;
            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvHasilReview_perKegiatan, modelHasilReviewPerKegiatan.currentRecords);
        }

        protected void gvHasilReview_perKegiatan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label hasil_review = (Label)e.Row.FindControl("lbHasilReview");
            //    Label hasil_penetapan = (Label)e.Row.FindControl("lbStatusPenetapan");
            //    if (ddlTahapan.SelectedValue.Equals("20"))
            //    {
            //        string status_penetapan = gvHasilReview_perKegiatan.DataKeys[e.Row.RowIndex]["hasil_penetapan"].ToString().ToLower();
            //        hasil_review.Text = gvHasilReview_perKegiatan.DataKeys[e.Row.RowIndex]["hasil_review"].ToString();

            //        if (status_penetapan.Equals("lolos tahapan"))
            //        {
            //            hasil_penetapan.Text = "<i class='far fa-check-square fa-2x mr-2'></i>Lolos tahapan";
            //        }
            //        else if (status_penetapan.Equals("tidak lolos tahapan"))
            //        {
            //            hasil_penetapan.Text = "<i class='fas fa-times fa-2x mr-2'></i>Tidak lolos tahapan";
            //        }
            //        else
            //        {
            //            hasil_penetapan.Text = "Belum ditetapkan";
            //        }
            //    }
            //    else
            //    {
            //        hasil_review.Text = gvHasilReview_perKegiatan.DataKeys[e.Row.RowIndex]["hasil_review_evaluasi_substansi"].ToString();
            //        string status_penetapan = gvHasilReview_perKegiatan.DataKeys[e.Row.RowIndex]["hasil_penetapan_evaluasi_substansi"].ToString().ToLower();

            //        if (status_penetapan.Equals("didanai"))
            //        {
            //            hasil_penetapan.Text = "<i class='far fa-check-square fa-2x mr-2'></i>Didanai";
            //        }
            //        else if (status_penetapan.Equals("tidak didanai"))
            //        {
            //            hasil_penetapan.Text = "<i class='fas fa-times fa-2x mr-2'></i>Tidak Didanai";
            //        }
            //        else
            //        {
            //            hasil_penetapan.Text = "Belum ditetapkan";
            //        }
            //    }
            //}
        }

        protected void gvHasilReviewPerKegiatan_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DetailReviewPerKegiatan")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                Guid idUsulan = Guid.Parse(gvHasilReview_perKegiatan.DataKeys[idx]["id_usulan_kegiatan"].ToString());
                string namaKetua = gvHasilReview_perKegiatan.DataKeys[idx]["nama_ketua_usulan"].ToString();
                string namaPT = gvHasilReview_perKegiatan.DataKeys[idx]["nama_institusi"].ToString();
                string judul = gvHasilReview_perKegiatan.DataKeys[idx]["judul"].ToString();

                ViewState["IdUsulan"] = idUsulan;

                if (ViewState["KdProgHibah"].ToString() == "1")
                    lblProgramKegiatan_modal.Text = "Penelitian Desentralisasi";
                else
                    lblProgramKegiatan_modal.Text = "Penelitian Penugasan";
                lblSkema_modal.Text = ViewState["NamaSkema"].ToString();
                lblTahapan_modal.Text = ViewState["Tahapan"].ToString();
                lblTahunUsulan_modal.Text = ddlThnUsulan.SelectedItem.ToString();
                lblTahunPelaksanaan_modal.Text = ddlThnPelaksanaan.SelectedItem.ToString();
                lblNidnNama_modal.Text = namaKetua + " - " + namaPT;
                lblJudul_modal.Text = judul;

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
                if (modelHasilReviewPerKegiatan.listHasilReviewPerKegiatan(ref dt, ViewState["KdProgHibah"].ToString(), 
                    ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, 
                    ddlTahapan.SelectedValue, (int)ViewState["IdSkema"], (Guid)ViewState["IdPT"], 
                    (int)ViewState["StatusPenilaian"], tbPencarian_perKegiatan.Text))
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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label hasil_review = (Label)e.Row.FindControl("lblHasilReview");
            //    if (ddlTahapan.SelectedValue.Equals("20"))
            //    {
            //        hasil_review.Text = gvHasilReviewModal.DataKeys[e.Row.RowIndex]["hasil_review"].ToString();
            //    }
            //    else
            //    {
            //        hasil_review.Text = gvHasilReviewModal.DataKeys[e.Row.RowIndex]["nilai_reviewer"].ToString();
            //        if (hasil_review.Text == "")
            //        {
            //            hasil_review.Text = "-";
            //        }
            //    }
            //}
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