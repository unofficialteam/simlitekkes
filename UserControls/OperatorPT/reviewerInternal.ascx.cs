using simlitekkes.Models;
using simlitekkes.Models.PT;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class reviewerInternal : System.Web.UI.UserControl
    {

        ReviewerInternal objReviewer = new ReviewerInternal();
        login objLogin;

        uiNotify noty = new uiNotify();
        uiPaging objPaging = new uiPaging();
        uiModal objModal = new uiModal();
        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (login)Session["objLogin"];

            if (!IsPostBack)
            {
                CeKSKRektor();
                isiGVDataReviewer();
            }
        }

        private void CeKSKRektor()
        {
            //Cek SK
            DataTable dtSkReviewer = new DataTable();
            if (!objReviewer.getDokumenSkReviewerOptPt(ref dtSkReviewer, Guid.Parse(objLogin.idInstitusi.ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objReviewer.errorMessage);
                return;
            }

            if (dtSkReviewer.Rows.Count > 0)
            {
                lbDataBaru.Visible = true;
            }
            else
            {
                lbDataBaru.Visible = false;
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Untuk menambah Reviewer Silakan unggah SK Reviewer terlebih dahulu");
            }
        }

        private void isiGVDataReviewer()
        {
            DataTable dt = new DataTable();

            dt.Clear();

            if (!objReviewer.listReviewer(ref dt, objLogin.kdInstitusi, rblAktif.SelectedValue,
                tbCariReviewer.Text.Trim()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objReviewer.errorMessage);
                return;
            }

            try
            {
                gvDaftarReviewer.DataSource = dt;
                gvDaftarReviewer.DataBind();

                lblJmlRecords.Text = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                return;
            }

        }

        private void refreshForm()
        {
            lbSimpan.Visible = false;
            ddlStatus.SelectedValue = "1";

            tbNidn.Text = string.Empty;
            tbNama.Text = string.Empty;
            tbJenisKelamin.Text = string.Empty;
            tbAlamat.Text = string.Empty;
            tbNoHP.Text = string.Empty;
            tbSurel.Text = string.Empty;
            tbKompetensi.Text = string.Empty;
            tbJenjangPendidikan.Text = string.Empty;
        }

        private void loadDataReviewer()
        {
            DataTable dtPenilai = new DataTable();
            if (!objReviewer.getDosen(ref dtPenilai, tbNidn.Text, objLogin.kdInstitusi.ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objReviewer.errorMessage);
                return;
            }

            dtPenilai = objReviewer.currentRecords;

            if (dtPenilai.Rows.Count > 0)
            {
                string stsEligibleReviewer = "0";
                if (!objReviewer.cekDosen(ref stsEligibleReviewer, Guid.Parse(dtPenilai.Rows[0]["id_personal"].ToString())))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objReviewer.errorMessage);
                    return;
                }

                if (stsEligibleReviewer == "1")
                {
                    if (dtPenilai.Rows[0]["kd_sts_aktif_reviewer_internal"].ToString().Trim() != "")
                    {
                        ddlStatus.SelectedValue = dtPenilai.Rows[0]["kd_sts_aktif_reviewer_internal"].ToString();
                    }
                    tbNama.Text = dtPenilai.Rows[0]["nama"].ToString();
                    tbJenisKelamin.Text = dtPenilai.Rows[0]["jenis_kelamin"].ToString();
                    tbAlamat.Text = dtPenilai.Rows[0]["alamat"].ToString();
                    tbNoHP.Text = dtPenilai.Rows[0]["nomor_hp"].ToString();
                    tbSurel.Text = dtPenilai.Rows[0]["surel"].ToString();
                    tbKompetensi.Text = dtPenilai.Rows[0]["kompetensi"].ToString();
                    tbJenjangPendidikan.Text = dtPenilai.Rows[0]["jenjang_pendidikan"].ToString();
                    lbSimpan.Visible = true;
                }
                else
                {
                    refreshForm();
                    lbSimpan.Visible = false;
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dosen tidak eligible menjadi Reviewer");
                }
            }
            else
            {
                refreshForm();
                lbSimpan.Visible = false;
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dosen tidak ditemukan/bukan dosen Perguruan Tinggi");
            }
        }

        protected void gvDaftarReviewer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string idPersonal = gvDaftarReviewer.DataKeys[e.RowIndex]["id_personal"].ToString();
            string idReviewer = gvDaftarReviewer.DataKeys[e.RowIndex]["id_reviewer"].ToString();
            ViewState["id_reviewer"] = gvDaftarReviewer.DataKeys[e.RowIndex]["id_reviewer"].ToString();
            string nidn = gvDaftarReviewer.DataKeys[e.RowIndex]["nidn"].ToString();
            string nama = gvDaftarReviewer.DataKeys[e.RowIndex]["nama"].ToString();
            string kdStsAktif = gvDaftarReviewer.DataKeys[e.RowIndex]["kd_sts_aktif_reviewer_internal"].ToString();

            var kd_sts_aktif_reviewer_internal = (kdStsAktif == "1") ? "0" : "1";
            if (!objReviewer.updateStatusRev(Guid.Parse(idReviewer), kd_sts_aktif_reviewer_internal))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objReviewer.errorMessage);
            if (!objReviewer.updateStatusPeran(Guid.Parse(idPersonal), kd_sts_aktif_reviewer_internal))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objReviewer.errorMessage);

            isiGVDataReviewer();
        }

        protected void gvDaftarReviewer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDaftarReviewer.PageIndex = e.NewPageIndex;
            isiGVDataReviewer();
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJmlBaris.SelectedValue == "-1")
            {
                gvDaftarReviewer.AllowPaging = false;
            }
            else
            {
                if (!gvDaftarReviewer.AllowPaging) gvDaftarReviewer.AllowPaging = true;
                gvDaftarReviewer.PageSize = int.Parse(ddlJmlBaris.SelectedValue);
            }

            isiGVDataReviewer();
        }

        protected void rblAktif_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGVDataReviewer();
        }

        protected void lbCari_Click(object sender, EventArgs e)
        {
            if (tbCariReviewer.Text.Trim().Length < 3)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Pencarian Gagal",
                    "Kata pencarian harus lebih dari 3 huruf !");
                return;
            }
            isiGVDataReviewer();
        }

        protected void lbRefresh_Click(object sender, EventArgs e)
        {
            tbCariReviewer.Text = string.Empty;
            isiGVDataReviewer();
        }

        protected void gvDaftarReviewer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                if (drv["kd_sts_aktif_reviewer_internal"].ToString() == "0")
                {
                    LinkButton lbStatus = (LinkButton)e.Row.FindControl("lbStatus");
                    lbStatus.CssClass = "btn btn-default";
                }

                if (drv["kd_sts_sertifikasi"].ToString() == "1")
                {
                    Label lblStatusSertifikasi = (Label)e.Row.FindControl("lblStatusSertifikasi");
                    lblStatusSertifikasi.Visible = true;
                }
            }
        }

        protected void lbDataBaru_Click(object sender, EventArgs e)
        {
            refreshForm();
            lbSimpan.Visible = false;
            objModal.ShowModal(this.Page, "ReviewerModal");
        }

        protected void lbCek_Click(object sender, EventArgs e)
        {
            daftarPenugasanReviewerPT modelPenugasanReviewer = new daftarPenugasanReviewerPT();

            if (objLogin.idPeran == 6) //&&
               // modelPenugasanReviewer.cekApakahKlasterNonBinaanDanPtn(Guid.Parse(objLogin.idInstitusi.ToString())) == true)
            {
                loadDataReviewer();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
                    "Penambahan Reviewer Internal hanya untuk Perguruan Tinggi Klaster Non Binaan");
            }
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            if (objReviewer.insupReviewer(tbNidn.Text, tbKompetensi.Text, ddlStatus.SelectedValue))
            {
                objModal.HideModal(this.Page, "ReviewerModal");
                isiGVDataReviewer();                
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
            }
        }
    }
}