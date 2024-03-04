using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.pendaftaranReviewer
{
    public partial class pendaftaranReviewerPpm : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        Models.login objLogin;
        simlitekkes.Models.Pengusul.persyaratanUmumPendaftaranReviewer objPersyUmum = new simlitekkes.Models.Pengusul.persyaratanUmumPendaftaranReviewer();
        Models.Pengusul.persyaratanUmumAbdimas objPersyaratanAbdimas = new Models.Pengusul.persyaratanUmumAbdimas();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                objLogin = (Models.login)Session["objLogin"];
                DataTable dtr = new DataTable();
                objPersyUmum.getStatusReviewerNasionalPpmTersertifikat(ref dtr, objLogin.idPersonal);
                //kd_sts_aktif_reviewer_nasional_ppm char(1),
                //kd_sts_aktif_reviewer_internal_ppm char(1)
                string kd_sts_aktif_reviewer_nasional_ppm = "0";
                ViewState["kd_sts_aktif_reviewer_nasional_ppm"] = "0";
                //panelPenyajiSeminarEdit4RevBaru.Visible = true;
                if (dtr.Rows.Count > 0) // Sudah terdaftar sebagai reviewer
                {
                    kd_sts_aktif_reviewer_nasional_ppm = dtr.Rows[0]["kd_sts_aktif"].ToString();
                    //string kd_sts_aktif_reviewer_internal_ppm = dtr.Rows[0]["kd_sts_aktif_reviewer_internal_ppm"].ToString();

                    ViewState["kd_sts_aktif_reviewer_nasional_ppm"] = kd_sts_aktif_reviewer_nasional_ppm;
                    //ViewState["kd_sts_aktif_reviewer_internal_ppm"] = kd_sts_aktif_reviewer_internal_ppm;

                    if (kd_sts_aktif_reviewer_nasional_ppm == "1")
                    {
                        ktSyaratRevPpm.showPanelReviewerBaru(false);
                        ktDokPendukung.init(false);
                        //pnlUtkDokRevBaru.Visible = false;
                        lblJudulPendaftranPpm.Text = "Daftar Ulang Reviewer Nasional (PPM)";
                        //panelPenyajiSeminarEdit4RevBaru.Visible = false;
                    }
                    else
                    {
                        ktSyaratRevPpm.showPanelReviewerBaru(true);
                        ktDokPendukung.init(true);
                        //pnlUtkDokRevBaru.Visible = true;
                        lblJudulPendaftranPpm.Text = "Pendaftaran Reviewer Nasional (PPM)";

                    }
                }
                else
                {
                    ktSyaratRevPpm.showPanelReviewerBaru(true);
                    ktDokPendukung.init(true);
                    //pnlUtkDokRevBaru.Visible = true;
                    lblJudulPendaftranPpm.Text = "Pendaftaran Reviewer Nasional (PPM)";
                }

                mvMain.SetActiveView(vPersyaratan);
                int jmlTotalSyarat = ktSyaratRevPpm.isiDataPersyaratanUmum();
                if (kd_sts_aktif_reviewer_nasional_ppm != "1")
                {
                    isiPilihanMonoMulti();
                }
                else
                {
                    lblDaftar.Visible = false;
                    lbDaftar.Visible = true;
                    cbMonoThn.Enabled = true;
                    cbMonoEdit.Enabled = true;
                    cbMultiThn.Enabled = true;
                    cbMultiEdit.Enabled = true;
                    ViewState["eligible"] = "1";
                }
                tampilkanIsianPendaftaran();
                setJadwal();

            }


            ktDokPendukung.OnChildEventOccursKembali += new EventHandler(lbKembali_Click);
            ktUnggahPresentasiNPoster.OnChildEventOccursKembali += new EventHandler(lbKembali_Click);
            ktSyaratRevPpm.childEventEditPresentasiNPoster += new EventHandler(lbEditDokPresentasiNPoster);
            ktSyaratRevPpm.childEventEdit += new EventHandler(editDokSeminar);

        }


        private void setJadwal()
        {
            objLogin = (Models.login)Session["objLogin"];
            lbDaftar.Visible = false;
            lblDaftar.Visible = false;
            DataTable dtJadwal = new DataTable();
            //if (ViewState["jadwal_daftar_ulang"] == null && ViewState["jadwal_daftar_baru"] == null)
            //{
            objPersyUmum.listJadwalKegiatan(ref dtJadwal,
                Guid.Parse(objLogin.idPersonal),
                ddlTahunUsulan.SelectedValue);
            ViewState["jadwal_daftar_ulang"] = "0";
            if (dtJadwal.Rows.Count > 0)
            {
                if (dtJadwal.Select("kd_jalur_seleksi='daftar_ulang_ppm_mono'").Length > 0 && ViewState["kd_sts_aktif_reviewer_nasional_ppm"].ToString() == "1")
                {
                    DataTable dtDaftarUlang = dtJadwal.Select("kd_jalur_seleksi = 'daftar_ulang_ppm_mono' ").CopyToDataTable();
                    if (dtDaftarUlang.Rows[0]["sts_jadwal"].ToString() == "1" && ViewState["eligible"].ToString() == "1")
                    {
                        if (ViewState["is_sdh_dikirim"] == null || ViewState["is_sdh_dikirim"].ToString() == "0") // belum dikirim
                        {
                            ViewState["jadwal_daftar_ulang"] = "1";
                            lbDaftar.Visible = true;
                        }
                    }
                    else
                    {
                        lblDaftar.Visible = true;
                        lblDaftar.ToolTip = "Bukan dalam jadwal pendaftaran/anda belum terdaftar sebagai reviewer internal.";
                    }
                }
                else if (dtJadwal.Select("kd_jalur_seleksi='daftar_ulang_ppm_multi'").Length > 0 && ViewState["kd_sts_aktif_reviewer_nasional_ppm"].ToString() == "1")
                {
                    DataTable dtDaftarUlang = dtJadwal.Select("kd_jalur_seleksi = 'daftar_ulang_ppm_multi' ").CopyToDataTable();
                    if (dtDaftarUlang.Rows[0]["sts_jadwal"].ToString() == "1" && ViewState["eligible"].ToString() == "1")
                    {
                        if (ViewState["is_sdh_dikirim"] == null || ViewState["is_sdh_dikirim"].ToString() == "0") // belum dikirim
                        {
                            ViewState["jadwal_daftar_ulang"] = "1";
                            lbDaftar.Visible = true;
                        }
                    }
                    else
                    {
                        lblDaftar.Visible = true;
                        lblDaftar.ToolTip = "Bukan dalam jadwal pendaftaran/anda belum terdaftar sebagai reviewer internal.";
                    }
                } 
                else if (dtJadwal.Select("kd_jalur_seleksi='daftar_baru_ppm_mono'").Length > 0)
                {
                    DataTable dtDaftarUlang = dtJadwal.Select("kd_jalur_seleksi = 'daftar_baru_ppm_mono' ").CopyToDataTable();
                    if (dtDaftarUlang.Rows[0]["sts_jadwal"].ToString() == "1" && ViewState["eligible"].ToString() == "1")
                    {
                        if (ViewState["is_sdh_dikirim"] == null || ViewState["is_sdh_dikirim"].ToString() == "0") // belum dikirim
                        {
                            ViewState["jadwal_daftar_ulang"] = "1";
                            lbDaftar.Visible = true;
                        }
                    }
                    else
                    {
                        lblDaftar.Visible = true;
                        lblDaftar.ToolTip = "Bukan dalam jadwal pendaftaran/anda belum terdaftar sebagai reviewer internal.";
                    }
                }
                else if (dtJadwal.Select("kd_jalur_seleksi='daftar_baru_ppm_multi'").Length > 0)
                {
                    DataTable dtDaftarUlang = dtJadwal.Select("kd_jalur_seleksi = 'daftar_baru_ppm_multi' ").CopyToDataTable();
                    if (dtDaftarUlang.Rows[0]["sts_jadwal"].ToString() == "1" && ViewState["eligible"].ToString() == "1")
                    {
                        if (ViewState["is_sdh_dikirim"] == null || ViewState["is_sdh_dikirim"].ToString() == "0") // belum dikirim
                        {
                            ViewState["jadwal_daftar_ulang"] = "1";
                            lbDaftar.Visible = true;
                        }
                    }
                    else
                    {
                        lblDaftar.Visible = true;
                        lblDaftar.ToolTip = "Bukan dalam jadwal pendaftaran/anda belum terdaftar sebagai reviewer internal.";
                    }
                }
            }
            else
            {
                lblDaftar.Visible = true;
                lblDaftar.ToolTip = "Bukan dalam jadwal pendaftaran.";
            }
        } 

        private void tampilkanIsianPendaftaran()
        {
            string sts_pengiriman_pendaftaran = "0";
            objLogin = (Models.login)Session["objLogin"];
            DataTable dtDaftar = new DataTable();
            objPersyUmum.getDataPendaftaranReviewerPPM(ref dtDaftar, objLogin.idPersonal, ddlTahunUsulan.SelectedValue);
            if (dtDaftar.Rows.Count > 0)  // Sudah mendaftar
            {
                cbMonoEdit.Checked = false;
                cbMultiEdit.Checked = false;
                cbMonoThn.Checked = false;
                cbMultiThn.Checked = false;
                if (dtDaftar.Rows[0]["kd_reviewer_mono"].ToString() == "1")
                {
                    cbMonoEdit.Checked = true;
                    cbMonoThn.Checked = true;
                }
                if (dtDaftar.Rows[0]["kd_reviewer_multi"].ToString() == "1")
                {
                    cbMultiEdit.Checked = true;
                    cbMultiThn.Checked = true;
                }

                ViewState["id_pendaftaran"] = dtDaftar.Rows[0]["id_pendaftaran"].ToString();
                ViewState["sts_perny_pakta_integritas"] = dtDaftar.Rows[0]["sts_pernyataan_pakta_integritas"].ToString();
                ViewState["sts_perny_kode_etik"] = dtDaftar.Rows[0]["sts_pernyataan_kode_etik"].ToString();
                sts_pengiriman_pendaftaran = dtDaftar.Rows[0]["sts_pengiriman_pendaftaran"].ToString();
                ViewState["sts_pengiriman_pendaftaran"] = sts_pengiriman_pendaftaran;


                string stsPPintegritas = ViewState["sts_perny_pakta_integritas"].ToString();
                string stsPKdEtik = ViewState["sts_perny_kode_etik"].ToString();

                ktDokPendukung.setIdPendaftaran(ViewState["id_pendaftaran"].ToString(),
                    stsPPintegritas, stsPKdEtik);


                mvDaftar.SetActiveView(vEdit);

                string stsDokumen = cekStsDok();
                string[] arrStsDok = stsDokumen.Split(new char[] { ',' });
                if (stsPPintegritas == "1" && stsPKdEtik == "1" && arrStsDok[0] == "1")
                {
                    lblHeaderInfoPendaftaran.ForeColor = System.Drawing.Color.Green;
                    lblInfoPendaftaran.ForeColor = System.Drawing.Color.Red;
                    //lblHeaderInfoPendaftaran.Text = "Isian pendaftaran telah lengkap.";
                    //lblInfoPendaftaran.Text = "Silakan klik \"Kirim Pendaftaran.\"";
                    cekStatusPengiriman(sts_pengiriman_pendaftaran);
                    //lblInfoPendaftaran.Text = "";
                    //lbKirimPendaftaran.Visible = true;
                }
                else
                {
                    lblHeaderInfoPendaftaran.ForeColor = System.Drawing.Color.Red;
                    lblInfoPendaftaran.ForeColor = System.Drawing.Color.Black;
                    lblHeaderInfoPendaftaran.Text = "Isian pendaftaran belum lengkap.";
                    lblInfoPendaftaran.Text = "";

                    if (arrStsDok[0] != "1")
                    {
                        lblInfoPendaftaran.Text += " - Dokumen Motivasi sebagai reviewer belum diunggah.<br>";
                    }
                    if (stsPPintegritas != "1")
                    {
                        lblInfoPendaftaran.Text += " - Pakta integritas belum disetujui.<br>";
                    }
                    if (stsPKdEtik != "1")
                    {
                        lblInfoPendaftaran.Text += " - Pernyataan mematuhi Kode Etik dan Kesanggupan Melaksanakan Tugas belum disetujui.<br>";
                    }
                    lblInfoPendaftaran.Text += "<br /> *Klik \"EDIT\" pada \"Dokumen pendukung\" untuk melengkapi.<br>";
                    lbKirimPendaftaran.Visible = false;
                }
                ktSyaratRevPpm.showTombolEdit(true);
                DataTable dtDokPendukung = new DataTable();
                objPersyaratanAbdimas.listDokumenPendukung(ref dtDokPendukung, ViewState["id_pendaftaran"].ToString());
                ViewState["dt_dok_pendukung"] = dtDokPendukung;
                DataRow[] rowsPresentasi = dtDokPendukung.Select("kd_jenis_dokumen=1");
                DataRow[] rowsPoster = dtDokPendukung.Select("kd_jenis_dokumen=2");
                ktSyaratRevPpm.setDataJmlPresentasiNPoster(rowsPresentasi.Length, rowsPoster.Length);

            }
            else // Belum mendaftar
            {
                mvDaftar.SetActiveView(vDaftar);
                ktSyaratRevPpm.showTombolEdit(false);
            }
        }


        private void cekStatusPengiriman(string sts_pengiriman_pendaftaran)
        {
            bool isEditVisible = true;
            if (sts_pengiriman_pendaftaran == "1")
            {
                isEditVisible = false;
            }


            //lbEditBidKepakaran.Visible = isEditVisible;
            lbEdit.Visible = isEditVisible;
            lbEditMonoMulti.Visible = isEditVisible;
            lbBatalkan.Visible = isEditVisible;

            lblHeaderInfoPendaftaran.ForeColor = System.Drawing.Color.Green;
            lblInfoPendaftaran.ForeColor = System.Drawing.Color.Red;
            lblHeaderInfoPendaftaran.Text = "Isian pendaftaran telah lengkap.";
            if (isEditVisible)
            {
                lblInfoPendaftaran.Text = "Silakan klik \"Kirim Pendaftaran.\"";
                ViewState["is_sdh_dikirim"] = "0";
            }
            else
            {
                lblInfoPendaftaran.Text = "Sedang menunggu proses seleksi.";
                ViewState["is_sdh_dikirim"] = "1";
            }
            lbKirimPendaftaranModal.Visible = isEditVisible;
            lbKirimPendaftaran.Visible = isEditVisible;
            lbBatalkan.Visible = isEditVisible;
            //ktSyaratUmumRevPenelitian.setEditable(isEditVisible);

        }


        protected void editDokSeminar(object sender, EventArgs e)
        {
            ktPenyajiSeminar.init(ddlTahunUsulan.SelectedValue);
            mvMain.SetActiveView(vPenyajiSeminar);
            pnlDaftarNEdit.Visible = false;
        }

        private void isiPilihanMonoMulti()
        {

            lbDaftar.Visible = false;
            lblDaftar.Visible = false;
            string kdStsEligible = "0";
            int idJabatanFungsional = 0;
            int idJenjangPendidikanTertinggi = 0;
            ktSyaratRevPpm.getSyaratDosen(ref kdStsEligible, ref idJabatanFungsional, ref idJenjangPendidikanTertinggi);
            
            //if (jmlTotalSyarat >= 7 || ViewState["kd_sts_aktif_reviewer_nasional_ppm"].ToString() == "1")
            //{
            //    lbDaftar.Visible = true;
            //}
            //else
            //{
            //    lblDaftar.Visible = true;
            //}
            //1 x sbg ketua pelaksana dlm kegiatan abdimas multi tahun & 1 x sbg ketua pelaksana dlm kegiatan abdimas mono tahun atau
            //3 x sbg ketua pelaksana dlm kegiatan abdimas mono tahun

            //2 x sbg ketua pelaksana dlm kegiatan abdimas multi tahun

            cbMonoThn.Enabled = false;
            cbMonoEdit.Enabled = false;
            cbMultiThn.Enabled = false;
            cbMultiEdit.Enabled = false;

            int jmlKetuaMono = ktSyaratRevPpm.getJmlKetuaMonoTahun();
            int jmlKetuaMulti = ktSyaratRevPpm.getJmlKetuaMultiTahun();
            int jmlAnggotaMono = ktSyaratRevPpm.getJmlAnggotaMonoTahun();
            int jmlAnggotaMulti = ktSyaratRevPpm.getJmlAnggotaMultiTahun();
            ViewState["eligible"] = "0";
            if (kdStsEligible == "1")
            {
                if ((idJenjangPendidikanTertinggi == 7 && idJabatanFungsional >= 2) || (idJenjangPendidikanTertinggi == 6 && idJabatanFungsional >= 3))
                {
                    // Eligible multi mono
                    if((jmlKetuaMulti >= 2) || 
                        (jmlKetuaMulti == 1 && jmlKetuaMono >= 1) ||
                        (jmlKetuaMulti == 1 && jmlAnggotaMulti >= 1) ||
                        (jmlKetuaMulti == 1 && jmlAnggotaMono >= 1)
                        )
                    {
                        lbDaftar.Visible = true;
                        cbMultiThn.Enabled = true;
                        cbMultiEdit.Enabled = true;
                        cbMonoThn.Enabled = true;
                        cbMonoEdit.Enabled = true;
                        ViewState["eligible"] = "1";
                    }
                    // eligible mono
                    else if((jmlKetuaMono >= 2) ||
                        (jmlKetuaMono == 1 && jmlAnggotaMono >= 1) ||
                        (jmlKetuaMono == 1 && jmlAnggotaMulti >= 1)
                        )
                    {
                        lbDaftar.Visible = true;
                        cbMonoThn.Enabled = true;
                        cbMonoEdit.Enabled = true;
                        ViewState["eligible"] = "1";
                    }
                    else
                    {
                        lblDaftar.Visible = true;
                    }
                }
                else
                {
                    lblDaftar.Visible = true;
                }
            }
            else
            {
                lblDaftar.Visible = true;
            }

            /*
            bool isShowKekuranganSyaratMono = true;
            if ((jmlKetuaMulti >= 1 && jmlKetuaMono >= 1) || jmlKetuaMono >= 3)
            {
                cbMonoThn.Enabled = true;
                cbMonoEdit.Enabled = true;
                //ktSyaratRevPpm.showInfoKekuranganUsulanDidanai(false);
                //lblDaftar.Visible = false;
                //lbDaftar.Visible = true;
                isShowKekuranganSyaratMono = false;
            }
            else
            {
                //ktSyaratRevPpm.showInfoKekuranganUsulanDidanai(true);
                //lblDaftar.Visible = true;
                //lbDaftar.Visible = false;
            }

            bool isShowKekuranganSyaratMulti = true;
            if (jmlKetuaMulti >= 2)
            {
                cbMultiThn.Enabled = true;
                cbMultiEdit.Enabled = true;
                isShowKekuranganSyaratMulti = false;
            }

            ktSyaratRevPpm.showInfoKekuranganUsulanDidanai(isShowKekuranganSyaratMono, isShowKekuranganSyaratMulti);
            if (isShowKekuranganSyaratMono && isShowKekuranganSyaratMulti)
            {
                lbDaftar.Visible = false;
                lblDaftar.Visible = true;
            }
            */
        }

        protected void lbDaftar_Click(object sender, EventArgs e)
        {
            if(!cbMonoThn.Checked && !cbMultiThn.Checked)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pilihan sebagai mono/multi tahun reviewer belum dipilih.");
                return;
            }

            // PPM
            objLogin = (Models.login)Session["objLogin"];
            string p_kd_reviewer_mono = "0";
            if (cbMonoThn.Checked) p_kd_reviewer_mono = "1";

            string p_kd_reviewer_multi = "0";
            if (cbMultiThn.Checked) p_kd_reviewer_multi = "1";
            simpanPendaftaran(p_kd_reviewer_mono, p_kd_reviewer_multi);

            mvDaftar.SetActiveView(vEdit);
            mvMain.SetActiveView(vPersyaratan);
            pnlDaftarNEdit.Visible = true;
            tampilkanIsianPendaftaran();
            //mvMain.SetActiveView(vPenyajiSeminar);
            //pnlDaftarNEdit.Visible = false;
        }

        private void simpanPendaftaran(string p_kd_reviewer_mono, string p_kd_reviewer_multi)
        {


            //ViewState["kd_sts_aktif_reviewer_nasional_ppm"] = kd_sts_aktif_reviewer_nasional_ppm;
            //ViewState["kd_sts_aktif_reviewer_internal_ppm"] = kd_sts_aktif_reviewer_internal_ppm;

            string p_is_baru = "1";

            if (ViewState["kd_sts_aktif_reviewer_nasional"] != null)
            {
                if (ViewState["kd_sts_aktif_reviewer_nasional_ppm"].ToString() == "1")
                    p_is_baru = "0";
            }

            if (objPersyUmum.insertPendaftaranReviewerPPM(Guid.Parse(objLogin.idPersonal), ddlTahunUsulan.SelectedValue, p_kd_reviewer_mono, p_kd_reviewer_multi, p_is_baru))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan/update pendaftaran reviewer PPM berhasil.");
                pnlDaftarNEdit.Visible = false;
                ktPenyajiSeminar.init(ddlTahunUsulan.SelectedValue);
                //mvDaftar.SetActiveView(vKembali);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan/update pendaftaran reviewer PPM gagal.");
                pnlDaftarNEdit.Visible = true;
            }
        }

        protected void lbKembali_Click(object sender, EventArgs e)
        {
            mvDaftar.SetActiveView(vEdit);
            mvMain.SetActiveView(vPersyaratan);
            pnlDaftarNEdit.Visible = true;
            tampilkanIsianPendaftaran();
        }

        protected void lbEditDokPresentasiNPoster(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vUnggahPresentasiNPoster);
            pnlDaftarNEdit.Visible = false;
            ktUnggahPresentasiNPoster.setData(ViewState["id_pendaftaran"].ToString(), ddlTahunUsulan.SelectedValue);
            //tampilkanIsianPendaftaran();
        }
        protected void lbEdit_Click(object sender, EventArgs e)
        {
            //mvDaftar.SetActiveView(vKembali);
            mvMain.SetActiveView(vDokumenPendukung);
            pnlDaftarNEdit.Visible = false;
            //ktDokPendukung.setIdPendaftaran(ViewState["id_pendaftaran"].ToString(),
            //    ViewState["sts_perny_pakta_integritas"].ToString(),
            //    ViewState["sts_perny_kode_etik"].ToString()
            //    );
        }



        protected void lbEditMonoMulti_Click(object sender, EventArgs e)
        {
            if (!cbMonoEdit.Checked && !cbMultiEdit.Checked)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pilihan sebagai mono/multi tahun reviewer belum dipilih.");
                return;
            }
            // PPM
            objLogin = (Models.login)Session["objLogin"];
            string p_kd_reviewer_mono = "0";
            if (cbMonoEdit.Checked) p_kd_reviewer_mono = "1";

            string p_kd_reviewer_multi = "0";
            if (cbMultiEdit.Checked) p_kd_reviewer_multi = "1";

            simpanPendaftaran(p_kd_reviewer_mono, p_kd_reviewer_multi);
            mvDaftar.SetActiveView(vEdit);
            mvMain.SetActiveView(vPersyaratan);
            pnlDaftarNEdit.Visible = true;
        }


        private string cekStsDok()
        {
            string stsDokumen = string.Empty;
            objLogin = (Models.login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_pendaftaran = ddlTahunUsulan.SelectedValue;


            lbUnduhPdfDokMotivasi.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokPaktaIntegritas.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokPernyataan.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokPengalaman.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokNarasumber.ForeColor = System.Drawing.Color.Gray;

            //lbUnduhPdfDokSertifikat.ForeColor = System.Drawing.Color.Gray;

            var dtMotivasi = new DataTable();
            if (objPersyUmum.getDokumenUnggah(ref dtMotivasi, ViewState["id_pendaftaran"].ToString(), ktDokPendukung.idJenisDokMotivasi.ToString()))
            {
                if (dtMotivasi.Rows.Count > 0)
                {
                    if (dtMotivasi.Rows[0]["id_dokumen_reviewer"].ToString().Trim() != "")
                    {
                        lbUnduhPdfDokMotivasi.ForeColor = System.Drawing.Color.Red;
                        stsDokumen = "1,";
                    }
                    else
                    {
                        stsDokumen = "0,";
                    }
                }
                else
                {
                    stsDokumen = "0,";
                }
            }


            //DataTable dtDataSertifikat = new DataTable();
            //pnlSertifikatRevBaru.Visible = false;
            //objPersyUmum.getDataSetifikatPelatihanReviewer(ref dtDataSertifikat, id_personal);
            //if (dtDataSertifikat.Rows.Count > 0)
            //{
            //    var dtPengalaman = new DataTable();
            //    if (objPersyUmum.getDokumen(ref dtPengalaman, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokSertifikat))
            //    {
            //        if (dtPengalaman.Rows.Count > 0)
            //        {
            //            lbUnduhPdfDokSertifikat.ForeColor = System.Drawing.Color.Red;
            //            pnlSertifikatRevBaru.Visible = true;
            //            stsDokumen += "1,1";
            //        }
            //        else
            //        {
            //            stsDokumen += "1,0";
            //        }
            //    }
            //}
            //else
            //{
            //    stsDokumen += "0,0";
            //}


            //var dtPaktaIntegritas = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtPaktaIntegritas, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokPaktaIntegritas))
            //{
            //    if (dtPaktaIntegritas.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPaktaIntegritas.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            //var dtPernyataan = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtPernyataan, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokPernyataan))
            //{
            //    if (dtPernyataan.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPernyataan.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            //var dtPengalaman = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtPengalaman, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokPengalaman))
            //{
            //    if (dtPengalaman.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPengalaman.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            //var dtNarasumber = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtNarasumber, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokNarasumber))
            //{
            //    if (dtNarasumber.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokNarasumber.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            return stsDokumen;
        }


        /*
        private void cekStsDok()
        {
            objLogin = (Models.login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_pendaftaran = ddlTahunUsulan.SelectedValue;


            lbUnduhPdfDokMotivasi.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokPaktaIntegritas.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokPernyataan.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdfDokPengalaman.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdfDokNarasumber.ForeColor = System.Drawing.Color.Gray;

            var dtMotivasi = new DataTable();
            if (objPersyUmum.getDokumen(ref dtMotivasi, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokMotivasi))
            {
                if (dtMotivasi.Rows.Count > 0)
                {
                    lbUnduhPdfDokMotivasi.ForeColor = System.Drawing.Color.Red;
                }
            }
            //var dtPaktaIntegritas = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtPaktaIntegritas, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokPaktaIntegritas))
            //{
            //    if (dtPaktaIntegritas.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPaktaIntegritas.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            //var dtPernyataan = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtPernyataan, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokPernyataan))
            //{
            //    if (dtPernyataan.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPernyataan.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            var dtPengalaman = new DataTable();
            if (objPersyUmum.getDokumen(ref dtPengalaman, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokPengalaman))
            {
                if (dtPengalaman.Rows.Count > 0)
                {
                    lbUnduhPdfDokPengalaman.ForeColor = System.Drawing.Color.Red;
                }
            }
            var dtNarasumber = new DataTable();
            if (objPersyUmum.getDokumen(ref dtNarasumber, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokNarasumber))
            {
                if (dtNarasumber.Rows.Count > 0)
                {
                    lbUnduhPdfDokNarasumber.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        */
        protected void lbUnduhMotivasi_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokMotivasi.ToString());
            ktDokPendukung.unduhDokMotivasi();
        }

        protected void lbUnduhPdfDokPaktaIntegritas_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokMotivasi.ToString());
            ktDokPendukung.unduhDokPaktaIntegritas();
        }

        //protected void lbUnduhPdfDokPernyataan_Click(object sender, EventArgs e)
        //{
        //    ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokMotivasi.ToString());
        //    ktDokPendukung.unduhDokPernyataan();
        //}

        protected void lbUnduhPdfDokPengalaman_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokMotivasi.ToString());
            ktDokPendukung.UnduhDokPengalaman();
        }

        protected void lbUnduhPdfDokNarasumber_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokMotivasi.ToString());
            ktDokPendukung.unduhDokNarasumber();
        }

        protected void lbBatalkan_Click(object sender, EventArgs e)
        {
            lblModalTitle.Text = "Konfirmasi pembatalan pendaftaran";
            lblModalInfo.Text = "Apakah Anda yakin akan membatalkan usulan pendaftaran sebagai reviewer ini?<br />Data isian dan dokumen akan dihapus.";
            uiModal objModal = new uiModal();
            objModal.ShowModal(this.Page, "modalHapusPendaftaran");
            lbHapusPendaftaranModal.Visible = true;
            lbKirimPendaftaranModal.Visible = false;
        }

        protected void lbHapusPendaftaranModal_Click(object sender, EventArgs e)
        {
            Guid id_pendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            
            //string idDokMotivasi = ktDokPendukung.getDokMotivasi();
            if (objPersyUmum.hapusPendaftaran(id_pendaftaran))
            {
                DataTable dtDokPendukung = (DataTable)ViewState["dt_dok_pendukung"];
                for (int a = 0; a < dtDokPendukung.Rows.Count; a++)
                {
                    ktUnggahPresentasiNPoster.deleteDokPendukung(dtDokPendukung.Rows[a]["id_dokumen"].ToString(),
                        dtDokPendukung.Rows[a]["kd_jenis_dokumen"].ToString(),
                        ddlTahunUsulan.SelectedValue
                        );
                }
                //ktDokPendukung.hapusDokMotivasi(idDokMotivasi);
                //tampilkanIsianPendaftaran();
                mvDaftar.SetActiveView(vDaftar);
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus pendaftaran reviewer berhasil.");
                cbMonoThn.Checked = false;
                cbMultiThn.Checked = false;
                cbMonoEdit.Checked = false;
                cbMultiEdit.Checked = false;
                ktSyaratRevPpm.setDataJmlPresentasiNPoster(0, 0);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus pendaftaran reviewer gagal.");
            }
        }

        protected void lbEditPenyajiSeminar_Click(object sender, EventArgs e)
        {
            ktPenyajiSeminar.init(ddlTahunUsulan.SelectedValue);
            mvMain.SetActiveView(vPenyajiSeminar);
            pnlDaftarNEdit.Visible = false;
        }

        protected void lbKirimPendaftaran_Click(object sender, EventArgs e)
        {

            lblModalTitle.Text = "Konfirmasi pengiriman pendaftaran";
            lblModalInfo.Text = "Apakah Anda yakin akan mengirim usulan pendaftaran sebagai reviewer ini?<br />Data isian dan dokumen tidak akan bisa diubah.";
            uiModal objModal = new uiModal();
            objModal.ShowModal(this.Page, "modalHapusPendaftaran");
            lbHapusPendaftaranModal.Visible = false;
            lbKirimPendaftaranModal.Visible = true;
        }

        protected void lbKirimPendaftaranFinal_Click(object sender, EventArgs e)
        {

            Guid id_pendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            string idDokMotivasi = ktDokPendukung.getDokMotivasi();
            if (objPersyUmum.kirimPendaftaran(id_pendaftaran))
            {
                tampilkanIsianPendaftaran();
                noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Kirim pendaftaran reviewer berhasil.");
                lbEditMonoMulti.Visible = false;
                lbBatalkan.Visible = false;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kirim pendaftaran reviewer gagal.");
            }
        }

        protected void ddlTahunUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbMonoThn.Checked = false;
            cbMonoEdit.Checked = false;
            cbMultiThn.Checked = false;
            cbMultiEdit.Checked = false;
            tampilkanIsianPendaftaran();
            setJadwal();
        }
    }
}