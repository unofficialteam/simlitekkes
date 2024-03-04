using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.Models;
using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System.IO;

namespace simlitekkes.UserControls.Pengusul.pendaftaranReviewer
{
    public partial class persyaratanUmumReviewerPPM : System.Web.UI.UserControl
    {
        Models.Pengusul.persyaratanUmumPendaftaranReviewer objPersyaratan = new Models.Pengusul.persyaratanUmumPendaftaranReviewer();
        Models.Pengusul.berandaPengusul objBerandaPengusul = new Models.Pengusul.berandaPengusul();
        

        login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList objDdl = new uiDropdownList();
        uiModal objModal = new uiModal();
        public event EventHandler childEventEdit;
        public event EventHandler childEventEditPresentasiNPoster;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void showPanelReviewerBaru(bool isShow)
        {
            mvPersyaratanWajibPpm.SetActiveView(vPersyaratanWajibPpm);
            pnlDaftarRevBaru.Visible = isShow;
        }


        public void showTombolEdit(bool isShow)
        {
            lbEditPresentasiNPoster.Visible = isShow;
        }

        public int isiDataPersyaratanUmum()
        {
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;

            // Cek Eligibilitas Dosen
            var dtCekDataDosen = new DataTable();
            if (objPersyaratan.getCekDataDosen(ref dtCekDataDosen, id_personal))
            {
                if (dtCekDataDosen.Rows.Count > 0)
                {
                    string kd_sts_eligible_dosen = dtCekDataDosen.Rows[0]["kd_sts_eligible_dosen"].ToString();
                    ViewState["kdStsEligibleDosen"] = kd_sts_eligible_dosen;
                    string statusGabungan = dtCekDataDosen.Rows[0]["status_gabungan"].ToString();
                    ViewState["statusGabungan"] = statusGabungan;
                    string status_aktif_dosen = dtCekDataDosen.Rows[0]["status_aktif_dosen"].ToString();
                    ViewState["status_aktif_dosen"] = status_aktif_dosen;
                }
            }

            // Isi Data Dosen
            var dtPersonal = new DataTable();
            if (objBerandaPengusul.getPersonal(ref dtPersonal, id_personal))
            {
                if (dtPersonal.Rows.Count > 0)
                {
                    lblNamaInstitusi.Text = dtPersonal.Rows[0]["nama_institusi"].ToString();
                    lblNamaProdi.Text = dtPersonal.Rows[0]["nama_program_studi"].ToString();
                    lblJenjangPendidikan.Text = dtPersonal.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
                    lblJabatanFungsional.Text = dtPersonal.Rows[0]["jabatan_fungsional"].ToString();
                    lblStstusAktif.Text = dtPersonal.Rows[0]["status_aktif"].ToString();
                    lblHindex.Text = dtPersonal.Rows[0]["hindex"].ToString();

                    int idJabatanFungsional = int.Parse(dtPersonal.Rows[0]["id_jabatan_fungsional"].ToString());
                    ViewState["idJabatanFungsional"] = idJabatanFungsional;
                    int idJenjangPendidikanTertinggi = int.Parse(dtPersonal.Rows[0]["id_jenjang_pendidikan_tertinggi"].ToString());
                    ViewState["idJenjangPendidikanTertinggi"] = idJenjangPendidikanTertinggi;
                    int hindex = int.Parse(dtPersonal.Rows[0]["hindex"].ToString());
                    ViewState["hindex"] = hindex;
                }
            }

            // Cek Jumlah Ketua
            var dtJmlKetua = new DataTable();
            if (objPersyaratan.getCekJmlKetuaAnggotaPpm(ref dtJmlKetua, id_personal))
            {
                if (dtJmlKetua.Rows.Count > 0)
                {
                    lblJmlKetuaMono.Text = dtJmlKetua.Rows[0]["jml_ketua_mono"].ToString();
                    lblJmlKetuaMulti.Text = dtJmlKetua.Rows[0]["jml_ketua_multi"].ToString();
                    lblJmlAnggotaMono.Text = dtJmlKetua.Rows[0]["jml_anggota_mono"].ToString();
                    lblJmlAnggotaMulti.Text = dtJmlKetua.Rows[0]["jml_anggota_multi"].ToString();
                }
            }

            // Cek Jumlah Penulis Pertama Artikel
            var dtJmlPenulisPertamaArtikel = new DataTable();
            ViewState["jmlPenulisPertama"] = 0;
            if (objPersyaratan.getJmlPenulisPertamaArtikelJurnal(ref dtJmlPenulisPertamaArtikel, id_personal))
            {
                if (dtJmlPenulisPertamaArtikel.Rows.Count > 0)
                {
                    lblJmlPenulisPertama.Text = dtJmlPenulisPertamaArtikel.Rows[0]["jml_penulis_pertama"].ToString();
                    int jmlPenulisPertama = int.Parse(dtJmlPenulisPertamaArtikel.Rows[0]["jml_penulis_pertama"].ToString());
                    ViewState["jmlPenulisPertama"] = jmlPenulisPertama;
                }
            }


            DataTable dtProsiding = new DataTable();
            objBerandaPengusul.getArtikelProsiding(ref dtProsiding, Guid.Parse(id_personal));

            DataTable dtPemakalah = new DataTable();
            objPersyaratan.getPemakalah(ref dtPemakalah, id_personal);


            lblJmlSeminar.Text = (dtProsiding.Rows.Count + dtPemakalah.Rows.Count).ToString();
            ViewState["jmlSeminar"] = lblJmlSeminar.Text;


            //getJmlProsiding()
            // Cek Jumlah Seminar
            //var dtJmlSeminar = new DataTable();
            //if (objPersyaratan.getJmlSeminar(ref dtJmlSeminar, id_personal))
            //{
            //    if (dtJmlSeminar.Rows.Count > 0)
            //    {
            //        lblJmlSeminar.Text = dtJmlSeminar.Rows[0]["jml_seminar"].ToString();
            //        int jmlSeminar = int.Parse(dtJmlSeminar.Rows[0]["jml_seminar"].ToString());
            //        ViewState["jmlSeminar"] = jmlSeminar;
            //    }
            //}

            // Cek Jumlah Penulis Anggota Artikel
            var dtJmlPenulisAnggotaArtikel = new DataTable();
            if (objPersyaratan.getJmlPenulisAnggotaArtikelJurnal(ref dtJmlPenulisAnggotaArtikel, id_personal))
            {
                if (dtJmlPenulisAnggotaArtikel.Rows.Count > 0)
                {
                    lblJmlPenulisAnggota.Text = dtJmlPenulisAnggotaArtikel.Rows[0]["jml_penulis_anggota"].ToString();
                }
            }

            // Cek Jumlah Buku
            var dtJmlBuku = new DataTable();
            if (objPersyaratan.getJmlBuku(ref dtJmlBuku, id_personal))
            {
                if (dtJmlBuku.Rows.Count > 0)
                {
                    lblJmlBukuAjar.Text = dtJmlBuku.Rows[0]["jml_buku"].ToString();
                    int jmlBuku = int.Parse(dtJmlBuku.Rows[0]["jml_buku"].ToString());
                    ViewState["jmlBuku"] = jmlBuku;
                }
            }

            // Cek Jumlah HKI
            var dtJmlHki = new DataTable();
            if (objPersyaratan.getJmlHki(ref dtJmlHki, id_personal))
            {
                if (dtJmlHki.Rows.Count > 0)
                {
                    lblJmlHki.Text = dtJmlHki.Rows[0]["jml_hki"].ToString();
                    int jmlHki = int.Parse(dtJmlHki.Rows[0]["jml_hki"].ToString());
                    ViewState["jmlHki"] = jmlHki;
                }
            }

            //// Cek Jumlah Penyaji Terbaik
            //var dtJmlPenyajiTerbaik = new DataTable();
            //if (objPersyaratan.getJmlPenyajiTerbaik(ref dtJmlPenyajiTerbaik, id_personal))
            //{
            //    if (dtJmlPenyajiTerbaik.Rows.Count > 0)
            //    {
            //        lblJmlPenyajiTerbaik.Text = dtJmlPenyajiTerbaik.Rows[0]["jml_penyaji_terbaik"].ToString();
            //    }
            //}

            //// Cek Jumlah Poster Terbaik
            //var dtJmlPosterTerbaik = new DataTable();
            //if (objPersyaratan.getJmlPosterTerbaik(ref dtJmlPosterTerbaik, id_personal))
            //{
            //    if (dtJmlPosterTerbaik.Rows.Count > 0)
            //    {
            //        lblJmlPosterTerbaik.Text = dtJmlPosterTerbaik.Rows[0]["jml_poster_terbaik"].ToString();
            //    }
            //}

            int totalSyarat = konfigurPersyaratan();
            return totalSyarat;
        }

        //public void showInfoKekuranganUsulanDidanai(bool isVisibleMono, bool isVisibleMulti)
        //{
        //    lblInfoKekuranganJmlPemenangMono.Visible = isVisibleMono;
        //    lblInfoKekuranganJmlPemenangMulti.Visible = isVisibleMulti;
        //}

        public int getJmlKetuaMonoTahun()
        {
            return int.Parse(lblJmlKetuaMono.Text);
        }

        public int getJmlKetuaMultiTahun()
        {
            return int.Parse(lblJmlKetuaMulti.Text);
        }
        public int getJmlAnggotaMonoTahun()
        {
            return int.Parse(lblJmlAnggotaMono.Text);
        }

        public int getJmlAnggotaMultiTahun()
        {
            return int.Parse(lblJmlAnggotaMulti.Text);
        }

        public void setDataJmlPresentasiNPoster(int jmlPresentasiTerbaik, int jmlPosterTerbaik)
        {
            lblJmlPenyajiTerbaik.Text = jmlPresentasiTerbaik.ToString();
            lblJmlPosterTerbaik.Text = jmlPosterTerbaik.ToString();
        }

        public void getSyaratDosen(ref string rkdStsEligible, ref int ridJabatanFungsional, ref int ridJenjangPendidikanTertinggi)
        {
            rkdStsEligible = ViewState["kdStsEligibleDosen"].ToString();
            ridJabatanFungsional = int.Parse(ViewState["idJabatanFungsional"].ToString());
            ridJenjangPendidikanTertinggi = int.Parse(ViewState["idJenjangPendidikanTertinggi"].ToString());
        }

        private int konfigurPersyaratan()
        {
            string kdStsEligibleDosen = ViewState["kdStsEligibleDosen"].ToString();
            string statusGabungan = ViewState["statusGabungan"].ToString();
            int idJabatanFungsional = int.Parse(ViewState["idJabatanFungsional"].ToString());
            int idJenjangPendidikanTertinggi = int.Parse(ViewState["idJenjangPendidikanTertinggi"].ToString());
            int jmlPenulisPertama = int.Parse(ViewState["jmlPenulisPertama"].ToString());
            int jmlSeminar = int.Parse(ViewState["jmlSeminar"].ToString());
            int hindex = int.Parse(ViewState["hindex"].ToString());
            int jmlBuku = int.Parse(ViewState["jmlBuku"].ToString());
            int jmlHki = int.Parse(ViewState["jmlHki"].ToString());
            
            int totalSyarat = 0;

            if (kdStsEligibleDosen == "1")
            {
                infoEligibilitasDosen.Visible = false;
                totalSyarat++;
            }
            else
            {
                infoEligibilitasDosen.Visible = true;
                lblInfoEligibilitasDosen.Text = statusGabungan;
            }

            if (idJenjangPendidikanTertinggi == 7 && idJabatanFungsional >= 2)
            {
                infoJenjangPendidikan.Visible = false;
                totalSyarat++;
            }
            else if (idJenjangPendidikanTertinggi == 6 && idJabatanFungsional >= 3)
            {
                infoJenjangPendidikan.Visible = false;
                totalSyarat++;
            }
            else
            {
                infoJenjangPendidikan.Visible = true;
            }

            if (hindex >= 1) // sebelumnya h-index minimal 1
            {
                infoHindex.Visible = false;
                totalSyarat++;
            }
            else
            {
                infoHindex.Visible = true;
            }

            if (jmlPenulisPertama < 1)
            {
                infoJmlArtikelJurnal.Visible = true;
            }
            else
            {
                //infoJmlArtikelJurnal.Visible = false;
                totalSyarat++;
            }

            if (jmlSeminar < 1)
            {
                infoJmlSeminar.Visible = true;
            }
            else
            {
                //infoJmlSeminar.Visible = false;
                totalSyarat++;
            }

            if (jmlBuku < 1)
            {
                infoJmlBuku.Visible = true;
            }
            else
            {
                //infoJmlBuku.Visible = false;
                totalSyarat++;
            }

            if (jmlHki < 1)
            {
                infoJmlHki.Visible = true;
            }
            else
            {
                //infoJmlHki.Visible = false;
                totalSyarat++;
            }
            
            return totalSyarat;
        }

        //protected void lbEditSeminar_Click(object sender, EventArgs e)
        //{
        //    if (childEventEdit != null)
        //        childEventEdit(sender, null);
        //}

        protected void lbEditPresentasiNPoster_Click(object sender, EventArgs e)
        {
            if (childEventEditPresentasiNPoster != null)
                childEventEditPresentasiNPoster(sender, null);
            
        }
    }
}