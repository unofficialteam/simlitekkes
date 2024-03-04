using simlitekkes.Models.report;
using simlitekkes.Models.Reviewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Reviewer.report
{
    public partial class hasilPenilaian1 : System.Web.UI.Page
    {
        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        Models.Pengusul.berandaPengusul objBerandaPengusul = new Models.Pengusul.berandaPengusul();

        evaluasiSubstansi objModel = new evaluasiSubstansi();

        pdfUsulanBaru objMdlPdfusulanBaru = new pdfUsulanBaru();

        protected void Page_Load(object sender, EventArgs e)
        {
            imgKop.ImageUrl = Server.MapPath("~/assets/dist/img/kemenkes.png");
            string idUsulanKegiatan = Request.QueryString.Get("id_usulan_kegiatan");
            string idPersonalReviewer = Request.QueryString.Get("id_personal_reviewer");
            initUsulan(idUsulanKegiatan);
            isiRekapRab(idUsulanKegiatan);
            isiDanaMitra(idUsulanKegiatan);
            DataTable dtp = new DataTable();
            objModel.getIdPlotting2Tahapan(ref dtp, Guid.Parse(idUsulanKegiatan), Guid.Parse(idPersonalReviewer));
            if (dtp.Rows.Count > 0)
            {
                isiHasilEvaluasiAdministrasi(dtp.Rows[0]["id_plotting_eval_administrasi"].ToString());//"77c1f0dd-3987-489b-9a94-9c6b1fcbad4e"
                isiHasilEvaluasiRekamJejak(dtp.Rows[0]["id_plotting_eval_substansi"].ToString());
                isiHasilEvaluasiUsulan(dtp.Rows[0]["id_plotting_eval_substansi"].ToString());
                isiJustifikasiRAB(dtp.Rows[0]["id_plotting_eval_substansi"].ToString());
                isiKomentarTglKota(dtp.Rows[0]["id_plotting_eval_substansi"].ToString(), idPersonalReviewer);
            }
        }


        private void initUsulan(string idUsulanKegiatan)
        {
            DataTable dtUsulan = new DataTable();
            objModelIdentitasUsulan.getDetailUsulanKegiatan(ref dtUsulan, idUsulanKegiatan);
            if (dtUsulan.Rows.Count > 0)
            {
                lblJudul.Text = dtUsulan.Rows[0]["judul"].ToString();
                lblBidangFokus.Text = dtUsulan.Rows[0]["bidang_fokus"].ToString();
                lblLamaKegiatan.Text = dtUsulan.Rows[0]["lama_kegiatan"].ToString();
                lblJudul.Text = dtUsulan.Rows[0]["judul"].ToString();
                ViewState["thnPelaksanaanKegiatan"] = dtUsulan.Rows[0]["thn_pelaksanaan_kegiatan"].ToString();

                string strLevelTkt = dtUsulan.Rows[0]["level_tkt_target"].ToString();
                if (strLevelTkt == "" || strLevelTkt == "-") strLevelTkt = "0";
                int level_tkt_target = int.Parse(strLevelTkt);

                if (level_tkt_target >= 7)
                {
                    lblKategoriPenelitian.Text = "HASIL EVALUASI DOKUMEN PROPOSAL PENELITIAN PENGEMBANGAN";
                }
                else if (level_tkt_target >= 4)
                {
                    lblKategoriPenelitian.Text = "HASIL EVALUASI DOKUMEN PROPOSAL PENELITIAN TERAPAN";
                }
                else if (level_tkt_target < 4)
                {
                    lblKategoriPenelitian.Text = "HASIL EVALUASI DOKUMEN PROPOSAL PENELITIAN DASAR";
                }


                DataTable dtCvKetua = new DataTable();
                objBerandaPengusul.getPersonal(ref dtCvKetua, dtUsulan.Rows[0]["id_personal"].ToString());
                if (dtCvKetua.Rows.Count > 0)
                {
                    lblNamaLengkapKetua.Text = dtCvKetua.Rows[0]["nama_lengkap"].ToString();
                    lblNidn.Text = dtCvKetua.Rows[0]["nidn"].ToString();
                    lblNamaPT.Text = dtCvKetua.Rows[0]["nama_institusi"].ToString();
                    lblNamaProdi.Text = dtCvKetua.Rows[0]["nama_program_studi"].ToString();
                    lblJabatanFungsional.Text = dtCvKetua.Rows[0]["jabatan_fungsional"].ToString();
                    // urutan_thn_usulan_kegiatan
                }
                isiMitra(idUsulanKegiatan);
            }
        }

        private void isiMitra(string pIdUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.GetMitra(ref dt, Guid.Parse(pIdUsulanKegiatan));
            if (dt.Rows.Count > 0)
            {
                lblNamaInstitusiMitra.Text = dt.Rows[0]["nama_institusi_mitra"].ToString();
                lblNamaMitra.Text = dt.Rows[0]["nama_mitra"].ToString();
            }
        }

        private void isiRekapRab(string pIdUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.ListRekapRab(ref dt, pIdUsulanKegiatan);
            rptPendanaan.DataSource = dt;
            rptPendanaan.DataBind();
        }

        private void isiDanaMitra(string pIdUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.ListDanaMitra(ref dt, pIdUsulanKegiatan);
            rptDanaMitra.DataSource = dt;
            rptDanaMitra.DataBind();
        }
        Models.Reviewer.evaluasiAdministrasi objEvaluasi = new Models.Reviewer.evaluasiAdministrasi();

        private void isiHasilEvaluasiAdministrasi(string pIdPlotting)
        {
            DataTable dt = new DataTable();
            objEvaluasi.getHasilEvaluasi(ref dt, Guid.Parse(pIdPlotting));
            //objMdlPdfusulanBaru.ListDanaMitra(ref dt, pIdUsulanKegiatan);
            rptHasilPenilaianAdministrasi.DataSource = dt;
            rptHasilPenilaianAdministrasi.DataBind();
        }

        protected void rptHasilPenilaianAdministrasi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RadioButton rbYes = (RadioButton)e.Item.FindControl("rbYes");
                RadioButton rbNo = (RadioButton)e.Item.FindControl("rbNo");
                Label lblYes = (Label)e.Item.FindControl("lblYes");

                if (lblYes.Text == "1")
                {
                    rbYes.Checked = true;
                }
                else
                {
                    rbNo.Checked = true;
                    rbLayak.Checked = false;
                    rbTdkLayak.Checked = true;
                }

            }
        }


        private void isiHasilEvaluasiRekamJejak(string pIdPlotting)
        {
            DataTable dt = new DataTable();
            objModel.getPenilaianRekamJejakPdf(ref dt, Guid.Parse(pIdPlotting));
            //objMdlPdfusulanBaru.ListDanaMitra(ref dt, pIdUsulanKegiatan);
            rptKelayakanRekamJejak.DataSource = dt;
            rptKelayakanRekamJejak.DataBind();
            if (dt.Rows.Count > 0)
            {
                lblSubtotalRekamJejak.Text = dt.Rows[0]["sub_total"].ToString();
            }
        }

        private void isiHasilEvaluasiUsulan(string pIdPlotting)
        {
            DataTable dt = new DataTable();
            DataTable dtTotal = new DataTable();
            objModel.getPenilaianUsulanPdf(ref dt, Guid.Parse(pIdPlotting));
            //objMdlPdfusulanBaru.ListDanaMitra(ref dt, pIdUsulanKegiatan);
            objModel.getTotalPenilaianUsulanPdf(ref dtTotal, Guid.Parse(pIdPlotting));
            rptPenilaianUsulan.DataSource = dt;
            rptPenilaianUsulan.DataBind();
            if (dt.Rows.Count > 0)
            {
                lblSubtotalPenilaian.Text = dt.Rows[0]["sub_total"].ToString();
                lblTotal1N2.Text = dtTotal.Rows[0]["total"].ToString();
                //string strTotalRkmJejak = lblSubtotalRekamJejak.Text.Replace(".", ",");
                //string strPenilaian = lblSubtotalPenilaian.Text.Replace(".", ",");
                //string strTotal = lblTotal1N2.Text.Replace(".", ",");

                //decimal totalRekamJejak = decimal.Parse(strTotalRkmJejak);
                //decimal totalPenilaian = decimal.Parse(strPenilaian);
                //decimal total = totalRekamJejak + totalPenilaian;

                //lblTotal1N2.Text = total.ToString();
                
            }
        }

        private void isiJustifikasiRAB(string pIdPlotting)
        {
            //kd_jenis_pembelanjaan
            //521213 HONOR
            //521219 BELANJA BARANG NON OPERASIONAL LAINNYA
            //522151 BELANJA BAHAN
            //524119 BELANJA PERJALANAN LAINNYA

            DataTable dt1 = new DataTable();
            int thnPelaksanaan = int.Parse(ViewState["thnPelaksanaanKegiatan"].ToString());
            if (thnPelaksanaan > 2019)
            {
                pnlThn12020.Visible = true;
                objModel.getListRABRekomendasi(ref dt1, Guid.Parse(pIdPlotting), 1);
                decimal totalThn1 = 0, totalThn2 = 0, totalThn3 = 0;
                decimal totalThn1Rev = 0, totalThn2Rev = 0, totalThn3Rev = 0;
                if (dt1.Rows.Count > 0)
                {
                    //rptBahanThn1
                    if (dt1.Select("id_rab_kelompok_biaya='1'").Count() > 0)
                    {
                        DataTable dtBahan1 = dt1.Select("id_rab_kelompok_biaya='1'").CopyToDataTable();
                        rptBahanThn1.DataSource = dtBahan1;
                        rptBahanThn1.DataBind();
                    }

                    //rptPengumpulanDataThn1
                    if (dt1.Select("id_rab_kelompok_biaya='2'").Count() > 0)
                    {
                        DataTable dtPengumpulanDataThn1 = dt1.Select("id_rab_kelompok_biaya='2'").CopyToDataTable();
                        rptPengumpulanDataThn1.DataSource = dtPengumpulanDataThn1;
                        rptPengumpulanDataThn1.DataBind();
                    }

                    //rptSewaPeralatanThn1
                    if (dt1.Select("id_rab_kelompok_biaya='3'").Count() > 0)
                    {
                        DataTable dtSewaPeralatanThn1 = dt1.Select("id_rab_kelompok_biaya='3'").CopyToDataTable();
                        rptSewaPeralatanThn1.DataSource = dtSewaPeralatanThn1;
                        rptSewaPeralatanThn1.DataBind();
                    }

                    //rptAnalisisDataThn1
                    if (dt1.Select("id_rab_kelompok_biaya='4'").Count() > 0)
                    {
                        DataTable dtAnalisisDataThn1 = dt1.Select("id_rab_kelompok_biaya='4'").CopyToDataTable();
                        rptAnalisisDataThn1.DataSource = dtAnalisisDataThn1;
                        rptAnalisisDataThn1.DataBind();
                    }

                    //rptPelaporanThn1
                    if (dt1.Select("id_rab_kelompok_biaya='5'").Count() > 0)
                    {
                        DataTable dtPelaporanThn1 = dt1.Select("id_rab_kelompok_biaya='5'").CopyToDataTable();
                        rptPelaporanThn1.DataSource = dtPelaporanThn1;
                        rptPelaporanThn1.DataBind();
                    }

                    totalThn1 = Convert.ToDecimal(dt1.Compute("SUM(total_biaya)", string.Empty));
                    totalThn1Rev = Convert.ToDecimal(dt1.Compute("SUM(total_justifikasi)", string.Empty));

                    lblTotalThn1.Text = totalThn1.ToString("N0");
                    lblTotalThn1Rev.Text = totalThn1Rev.ToString("N0");
                }

                // thn 2
                DataTable dt2 = new DataTable();
                objModel.getListRABRekomendasi(ref dt2, Guid.Parse(pIdPlotting), 2);

                //rptBahanThn2
                if (dt2.Rows.Count > 0)
                {
                    if (dt2.Select("id_rab_kelompok_biaya='1'").Count() > 0)
                    {
                        DataTable dtBahan2 = dt2.Select("id_rab_kelompok_biaya='1'").CopyToDataTable();
                        rptBahanThn2.DataSource = dtBahan2;
                        rptBahanThn2.DataBind();
                    }

                    //rptPengumpulanDataThn2
                    if (dt2.Select("id_rab_kelompok_biaya='2'").Count() > 0)
                    {
                        DataTable dtPengumpulanDataThn2 = dt2.Select("id_rab_kelompok_biaya='2'").CopyToDataTable();
                        rptPengumpulanDataThn2.DataSource = dtPengumpulanDataThn2;
                        rptPengumpulanDataThn2.DataBind();
                    }

                    //rptSewaPeralatanThn2
                    if (dt2.Select("id_rab_kelompok_biaya='3'").Count() > 0)
                    {
                        DataTable dtSewaPeralatanThn2 = dt2.Select("id_rab_kelompok_biaya='3'").CopyToDataTable();
                        rptSewaPeralatanThn2.DataSource = dtSewaPeralatanThn2;
                        rptSewaPeralatanThn2.DataBind();
                    }

                    //rptAnalisisDataThn2
                    if (dt2.Select("id_rab_kelompok_biaya='4'").Count() > 0)
                    {
                        DataTable dtAnalisisDataThn2 = dt2.Select("id_rab_kelompok_biaya='4'").CopyToDataTable();
                        rptAnalisisDataThn2.DataSource = dtAnalisisDataThn2;
                        rptAnalisisDataThn2.DataBind();
                    }

                    //rptPelaporanThn2
                    if (dt2.Select("id_rab_kelompok_biaya='5'").Count() > 0)
                    {
                        DataTable dtPelaporanThn2 = dt2.Select("id_rab_kelompok_biaya='5'").CopyToDataTable();
                        rptPelaporanThn2.DataSource = dtPelaporanThn2;
                        rptPelaporanThn2.DataBind();
                    }

                    totalThn2 = Convert.ToDecimal(dt2.Compute("SUM(total_biaya)", string.Empty));
                    totalThn2Rev = Convert.ToDecimal(dt2.Compute("SUM(total_justifikasi)", string.Empty));

                    lblTotalThn2.Text = totalThn2.ToString("N0");
                    lblTotalThn2Rev.Text = totalThn2Rev.ToString("N0");
                }

                // thn 3
                DataTable dt3 = new DataTable();
                objModel.getListRABRekomendasi(ref dt3, Guid.Parse(pIdPlotting), 3);

                //rptBahanThn3
                if (dt3.Rows.Count > 0)
                {
                    if (dt3.Select("id_rab_kelompok_biaya='1'").Count() > 0)
                    {
                        DataTable dtBahanThn3 = dt3.Select("id_rab_kelompok_biaya='1'").CopyToDataTable();
                        rptBahanThn3.DataSource = dtBahanThn3;
                        rptBahanThn3.DataBind();
                    }

                    //rptPengumpulanDataThn3
                    if (dt3.Select("id_rab_kelompok_biaya='2'").Count() > 0)
                    {
                        DataTable dtPengumpulanDataThn3 = dt3.Select("id_rab_kelompok_biaya='2'").CopyToDataTable();
                        rptPengumpulanDataThn3.DataSource = dtPengumpulanDataThn3;
                        rptPengumpulanDataThn3.DataBind();
                    }

                    //rptSewaPeralatanThn3
                    if (dt3.Select("id_rab_kelompok_biaya='3'").Count() > 0)
                    {
                        DataTable dtSewaPeralatanThn3 = dt3.Select("id_rab_kelompok_biaya='3'").CopyToDataTable();
                        rptSewaPeralatanThn3.DataSource = dtSewaPeralatanThn3;
                        rptSewaPeralatanThn3.DataBind();
                    }

                    //rptAnalisisDataThn3
                    if (dt3.Select("id_rab_kelompok_biaya='4'").Count() > 0)
                    {
                        DataTable dtAnalisisDataThn3 = dt3.Select("id_rab_kelompok_biaya='4'").CopyToDataTable();
                        rptAnalisisDataThn3.DataSource = dtAnalisisDataThn3;
                        rptAnalisisDataThn3.DataBind();
                    }

                    //rptPelaporanThn3
                    if (dt3.Select("id_rab_kelompok_biaya='5'").Count() > 0)
                    {
                        DataTable dtPelaporanThn3 = dt3.Select("id_rab_kelompok_biaya='5'").CopyToDataTable();
                        rptPelaporanThn3.DataSource = dtPelaporanThn3;
                        rptPelaporanThn3.DataBind();
                    }

                    totalThn3 = Convert.ToDecimal(dt3.Compute("SUM(total_biaya)", string.Empty));
                    totalThn3Rev = Convert.ToDecimal(dt3.Compute("SUM(total_justifikasi)", string.Empty));

                    lblTotalThn3.Text = totalThn3.ToString("N0");
                    lblTotalThn3Rev.Text = totalThn3Rev.ToString("N0");
                }

                var totalBiaya = totalThn1 + totalThn2 + totalThn3;
                var totalBiayaRev = totalThn1Rev + totalThn2Rev + totalThn3Rev;
                lblTotalDiusulkan.Text = totalBiaya.ToString("N0");
                lblTotalJustifikasiReviewer.Text = totalBiayaRev.ToString("N0");
            }
            else
            {
                pnlThn12019.Visible = true;
                // thn 1
                objModel.getListRekomendasiRAB(ref dt1, Guid.Parse(pIdPlotting), 1);
                decimal totalThn1 = 0, totalThn2 = 0, totalThn3 = 0;
                decimal totalThn1Rev = 0, totalThn2Rev = 0, totalThn3Rev = 0;
                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Select("kd_jenis_pembelanjaan='521213'").Count() > 0)
                    {
                        DataTable dtHonor1 = dt1.Select("kd_jenis_pembelanjaan='521213'").CopyToDataTable();
                        rptHonorThn1.DataSource = dtHonor1;
                        rptHonorThn1.DataBind();
                    }

                    //rptBelanjaNonOpLainyaThn1
                    if (dt1.Select("kd_jenis_pembelanjaan='521219'").Count() > 0)
                    {
                        DataTable dtBelanjaNonOpLainyaThn1 = dt1.Select("kd_jenis_pembelanjaan='521219'").CopyToDataTable();
                        rptBelanjaNonOpLainyaThn1.DataSource = dtBelanjaNonOpLainyaThn1;
                        rptBelanjaNonOpLainyaThn1.DataBind();
                    }

                    //rptBelanjaThn1
                    if (dt1.Select("kd_jenis_pembelanjaan='522151'").Count() > 0)
                    {
                        DataTable dtBelanjaThn1 = dt1.Select("kd_jenis_pembelanjaan='522151'").CopyToDataTable();
                        rptBelanjaThn1.DataSource = dtBelanjaThn1;
                        rptBelanjaThn1.DataBind();
                    }

                    //rptBelanjaPerjLainyaThn1
                    if (dt1.Select("kd_jenis_pembelanjaan='524119'").Count() > 0)
                    {
                        DataTable dtBelanjaPerjLainyaThn1 = dt1.Select("kd_jenis_pembelanjaan='524119'").CopyToDataTable();
                        rptBelanjaPerjLainyaThn1.DataSource = dtBelanjaPerjLainyaThn1;
                        rptBelanjaPerjLainyaThn1.DataBind();
                    }
                    totalThn1 = Convert.ToDecimal(dt1.Compute("SUM(total_biaya)", string.Empty));
                    totalThn1Rev = Convert.ToDecimal(dt1.Compute("SUM(total_justifikasi)", string.Empty));

                    lblTotalThn1.Text = totalThn1.ToString("N0");
                    lblTotalThn1Rev.Text = totalThn1Rev.ToString("N0");
                }

                // thn 2
                DataTable dt2 = new DataTable();
                objModel.getListRekomendasiRAB(ref dt2, Guid.Parse(pIdPlotting), 2);

                if (dt2.Rows.Count > 0)
                {
                    if (dt2.Select("kd_jenis_pembelanjaan='521213'").Count() > 0)
                    {
                        DataTable dtHonor2 = dt2.Select("kd_jenis_pembelanjaan='521213'").CopyToDataTable();
                        rptHonorThn2.DataSource = dtHonor2;
                        rptHonorThn2.DataBind();
                    }

                    //rptBelanjaNonOpLainyaThn1
                    if (dt2.Select("kd_jenis_pembelanjaan='521219'").Count() > 0)
                    {
                        DataTable dtBelanjaNonOpLainyaThn2 = dt2.Select("kd_jenis_pembelanjaan='521219'").CopyToDataTable();
                        rptBelanjaNonOpLainyaThn2.DataSource = dtBelanjaNonOpLainyaThn2;
                        rptBelanjaNonOpLainyaThn2.DataBind();
                    }

                    //rptBelanjaThn1
                    if (dt2.Select("kd_jenis_pembelanjaan='522151'").Count() > 0)
                    {
                        DataTable dtBelanjaThn2 = dt2.Select("kd_jenis_pembelanjaan='522151'").CopyToDataTable();
                        rptBelanjaThn2.DataSource = dtBelanjaThn2;
                        rptBelanjaThn2.DataBind();
                    }

                    //rptBelanjaPerjLainyaThn1
                    if (dt2.Select("kd_jenis_pembelanjaan='524119'").Count() > 0)
                    {
                        DataTable dtBelanjaPerjLainyaThn2 = dt2.Select("kd_jenis_pembelanjaan='524119'").CopyToDataTable();
                        rptBelanjaPerjLainyaThn2.DataSource = dtBelanjaPerjLainyaThn2;
                        rptBelanjaPerjLainyaThn2.DataBind();
                    }
                    totalThn2 = Convert.ToDecimal(dt2.Compute("SUM(total_biaya)", string.Empty));
                    totalThn2Rev = Convert.ToDecimal(dt2.Compute("SUM(total_justifikasi)", string.Empty));

                    lblTotalThn2.Text = totalThn2.ToString("N0");
                    lblTotalThn2Rev.Text = totalThn2Rev.ToString("N0");
                }
                // thn 3
                DataTable dt3 = new DataTable();
                objModel.getListRekomendasiRAB(ref dt3, Guid.Parse(pIdPlotting), 3);

                if (dt3.Rows.Count > 0)
                {
                    if (dt3.Select("kd_jenis_pembelanjaan='521213'").Count() > 0)
                    {
                        DataTable dtHonor3 = dt3.Select("kd_jenis_pembelanjaan='521213'").CopyToDataTable();
                        rptHonorThn3.DataSource = dtHonor3;
                        rptHonorThn3.DataBind();
                    }

                    //rptBelanjaNonOpLainyaThn1
                    if (dt3.Select("kd_jenis_pembelanjaan='521219'").Count() > 0)
                    {
                        DataTable dtBelanjaNonOpLainyaThn3 = dt3.Select("kd_jenis_pembelanjaan='521219'").CopyToDataTable();
                        rptBelanjaNonOpLainyaThn3.DataSource = dtBelanjaNonOpLainyaThn3;
                        rptBelanjaNonOpLainyaThn3.DataBind();
                    }

                    //rptBelanjaThn1
                    if (dt3.Select("kd_jenis_pembelanjaan='522151'").Count() > 0)
                    {
                        DataTable dtBelanjaThn3 = dt3.Select("kd_jenis_pembelanjaan='522151'").CopyToDataTable();
                        rptBelanjaThn3.DataSource = dtBelanjaThn3;
                        rptBelanjaThn3.DataBind();
                    }

                    //rptBelanjaPerjLainyaThn1
                    if (dt3.Select("kd_jenis_pembelanjaan='524119'").Count() > 0)
                    {
                        DataTable dtBelanjaPerjLainyaThn3 = dt3.Select("kd_jenis_pembelanjaan='524119'").CopyToDataTable();
                        rptBelanjaPerjLainyaThn3.DataSource = dtBelanjaPerjLainyaThn3;
                        rptBelanjaPerjLainyaThn3.DataBind();
                    }
                    totalThn3 = Convert.ToDecimal(dt3.Compute("SUM(total_biaya)", string.Empty));
                    totalThn3Rev = Convert.ToDecimal(dt3.Compute("SUM(total_justifikasi)", string.Empty));

                    lblTotalThn3.Text = totalThn3.ToString("N0");
                    lblTotalThn3Rev.Text = totalThn3Rev.ToString("N0");
                }

                var totalBiaya = totalThn1 + totalThn2 + totalThn3;
                var totalBiayaRev = totalThn1Rev + totalThn2Rev + totalThn3Rev;
                lblTotalDiusulkan.Text = totalBiaya.ToString("N0");
                lblTotalJustifikasiReviewer.Text = totalBiayaRev.ToString("N0");
            }



        }

        private void isiKomentarTglKota(string IdPlottingReviewer, string idPersonalReviewer)
        {
            var dt = new DataTable();
            if (objModel.getEvaluasiUsulan(ref dt, Guid.Parse(IdPlottingReviewer)) && dt.Rows.Count > 0)
            {
                //    lblJmlItem.Text = dt.Rows[0]["jml_komponen"].ToString();
                //    lblJmlDievaluasi.Text = dt.Rows[0]["jml_komponen_dinilai"].ToString();
                //    lblTotalNilai.Text = dt.Rows[0]["total_nilai"].ToString();
                //    tbKota.Text = dt.Rows[0]["tempat"].ToString();
                lblKomentarUmum.Text = dt.Rows[0]["komentar"].ToString();
                lblKotaTglBulanTahun.Text = String.Format("{0}, {1}-{2}-{3}",
                    dt.Rows[0]["tempat"].ToString(), DateTime.Now.Day.ToString(),
                     DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString()
                    );


                DataTable dtReviewer = new DataTable();
                objBerandaPengusul.getPersonal(ref dtReviewer, idPersonalReviewer);
                lblNamaLengkapReviewer.Text = dtReviewer.Rows[0]["nama_lengkap"].ToString();

            }
        }
    }
}