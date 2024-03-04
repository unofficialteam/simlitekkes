using simlitekkes.Core;
using simlitekkes.Models.Pengusul;
using simlitekkes.Models.report;
using simlitekkes.Models.Sistem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;
using simlitekkes.Helper;
using simlitekkes.UIControllers;

namespace simlitekkes.UserControls.Pengusul.PerbaikanProposal
{
    public partial class rekapUsulanPerbaikan : System.Web.UI.UserControl
    {
        //PerbaikanUsulan modelPerbaikanProposal = new PerbaikanUsulan();
        Models.Pengusul.RAB repo = new Models.Pengusul.RAB();

        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        UIControllers.uiDropdownList obj_uiDDL = new UIControllers.uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        Models.Sistem.referensiData objRefData = new Models.Sistem.referensiData();
        Models.Pengusul.persyaratanUmum objPersyaratan = new Models.Pengusul.persyaratanUmum();
        simlitekkes.Models.Pengusul.PerbaikanUsulan objPerbaikan = new simlitekkes.Models.Pengusul.PerbaikanUsulan();

        Models.login objLogin = new Models.login();
        kontrolUnggah ktUnggah = new kontrolUnggah();
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        Models.Pengusul.berandaPengusul objPengusul = new Models.Pengusul.berandaPengusul();
        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();

        pdfUsulanBaru objMdlPdfusulanBaru = new pdfUsulanBaru();
        uiNotify noty = new uiNotify();

        RAB objModel = new Models.Pengusul.RAB();
        //int jmlKelengkapan = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        public bool initUsulan(string idUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objModelIdentitasUsulan.getDataDataUsulanKegiatan(ref dt, idUsulanKegiatan);
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            var objUsulanKegiatan = new usulanKegiatan()
            {
                idUsulanKegiatan = idUsulanKegiatan,
                idUsulan = dt.Rows[0]["id_usulan"].ToString(),
                judul = dt.Rows[0]["judul"].ToString(),
                idSkema = int.Parse(dt.Rows[0]["id_skema"].ToString()),
                namaSkema = dt.Rows[0]["nama_skema"].ToString(),
                thnUsulan = dt.Rows[0]["thn_usulan_kegiatan"].ToString(),
                thnPelaksanaan = dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString(),
                lamaKegiatan = int.Parse(dt.Rows[0]["lama_kegiatan"].ToString()),
                urutanTahunUsulanKegiatan = int.Parse(dt.Rows[0]["urutan_thn_usulan_kegiatan"].ToString()),
                tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString()), // test
                idBidangFokus = int.Parse(dt.Rows[0]["id_bidang_fokus"].ToString()),
                bidangFokus = dt.Rows[0]["bidang_fokus"].ToString(),
                idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString())

            };

            ViewState["thn_usulan"] = objUsulanKegiatan.thnUsulan;
            ViewState["thnPelaksanaan"] = objUsulanKegiatan.thnPelaksanaan;

            DataTable dtSkema = new DataTable();
            objRefData.getSkemaKegiatan(ref dtSkema);
            var dr1 = dtSkema.Select("id_skema=" + objUsulanKegiatan.idSkema);
            DataTable dt3 = dr1.CopyToDataTable();
            int thnMaks = int.Parse(dt3.Rows[0]["thn_maksimal"].ToString());
            int lamaKegiatan = objUsulanKegiatan.lamaKegiatan;
            string strLamaKeg = lamaKegiatan.ToString();
            if (thnMaks < lamaKegiatan)
                strLamaKeg = thnMaks.ToString();
            ViewState["lama_kegiatan"] = strLamaKeg;


            ViewState["urutan_thn_usulan_kegiatan"] = objUsulanKegiatan.urutanTahunUsulanKegiatan;
            //lblBidFokus.Text = objUsulanKegiatan.bidangFokus;
            objLogin = (Models.login)Session["objLogin"];
            dt.Clear();
            dt = new DataTable();
            objPengusul.getPersonal(ref dt, objLogin.idPersonal);
            if (dt.Rows.Count > 0)
            {
                //lblNidn.Text = dt.Rows[0]["nidn"].ToString();

                //lblInstitusi.Text = dt.Rows[0]["nama_institusi"].ToString();
                //lblProdi.Text = dt.Rows[0]["nama_program_studi"].ToString();
                //lblJenjangPendidikan.Text = dt.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
                //lblJabatanAkademik.Text = dt.Rows[0]["jabatan_fungsional"].ToString();
                //lblTempat.Text = dt.Rows[0]["tempat_lahir"].ToString();
                //DateTime tanggalLahir = DateTime.Parse(dt.Rows[0]["tanggal_lahir"].ToString());
                //lblTglLahir.Text = tanggalLahir.Day + "-" + tanggalLahir.Month + "-" + tanggalLahir.Year;

                //lblNoKtp.Text = dt.Rows[0]["nomor_ktp"].ToString();
                //lblAlamat.Text = dt.Rows[0]["alamat"].ToString();
                //lblNoTelepon.Text = dt.Rows[0]["nomor_telepon"].ToString();
                //lblNoHp.Text = dt.Rows[0]["nomor_hp"].ToString();
                //lblSurel.Text = dt.Rows[0]["surel"].ToString();
                //lblWebPersonal.Text = dt.Rows[0]["website_personal"].ToString();
            }
            //lblJudul.Text = objUsulanKegiatan.judul;
            lblNamaNidnKetua.Text = objLogin.namaLengkap + " (" + objLogin.nidn + ")";
            lblNamaInstitusiNProdi.Text = objLogin.namaInstitusi + " - " + dt.Rows[0]["nama_program_studi"].ToString();
            lblJenjangPendidikan.Text = dt.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
            lblSurel.Text = dt.Rows[0]["surel"].ToString();
            isiDataSinta();
            isiRekamJejak(objLogin.idPersonal);
            pnlKekDokumenUsulan.Visible = false;
            pnlKekAnggota.Visible = false;
            pnlKekLuaran.Visible = false;
            pnlKekRab.Visible = false;
            pnlKekMitra.Visible = false;

            int retVal = isiAnggotaDikti(idUsulanKegiatan);
            bool enableKirimUsulan = true;


            //bool isMakroRiset = true;
            //if (!cekDdlMakro(objUsulanKegiatan.idUsulan))
            //{
            //    isMakroRiset = false;
            //}

            //string dirFile = "~/fileUpload/dokumenUsulan/" + objUsulanKegiatan.thnUsulan;
            string thnPelaksanaan = ViewState["thnPelaksanaan"].ToString();
            string id_transaksi_perbaikan = "00000000-0000-0000-0000-000000000000";
            objPerbaikan.getidTransaksi(ref id_transaksi_perbaikan, Guid.Parse(objUsulanKegiatan.idUsulanKegiatan), "14");
            string dirFile = "~/fileUpload/dokumenUsulanRevisi/" + thnPelaksanaan + "/" + id_transaksi_perbaikan + ".pdf";
            
            string path2save = String.Format(dirFile + "/{0}.pdf", id_transaksi_perbaikan);
            if (!cekFileExists(dirFile)) //  || !isMakroRiset)
            {
                pnlKekDokumenUsulan.Visible = true;
                enableKirimUsulan = false;
            }

            //if (retVal == 0)
            //{
            //    pnlKekAnggota.Visible = true;
            //    enableKirimUsulan = false;
            //}

            //if (ViewState["sts_persetujuan_anggota"].ToString() != "1")
            //{
            //    pnlKekAnggota.Visible = true;
            //    enableKirimUsulan = false;
            //}

            //retVal =
            isiSummaryRAB(objUsulanKegiatan);
            InitRAB(objUsulanKegiatan);
            bool cek = cekRAB(objUsulanKegiatan);

            if (cek == false)
            {
                pnlKekRab.Visible = true;
                enableKirimUsulan = false;
            }


            retVal = isiLuaran(idUsulanKegiatan);
            if (retVal == 0)
            {
                pnlKekLuaran.Visible = true;
                enableKirimUsulan = false;
            }

            //if (objUsulanKegiatan.idSkema == 72) // KRUPT
            //{
            //    panelKekuranganWbs.Visible = false;
            //    panelWbs.Visible = true;
            //    dirFile = "~/fileUpload/dokumenWbs/" + objUsulanKegiatan.thnUsulan;
            //    path2save = String.Format(dirFile + "/{0}.pdf", objUsulanKegiatan.idUsulanKegiatan);
            //    if (!cekFileWbsExists(path2save))
            //    {
            //        panelKekuranganWbs.Visible = true;
            //        enableKirimUsulan = false;
            //    }
            //}

            retVal = isiMitra(idUsulanKegiatan, objUsulanKegiatan.idSkema);
            //if (retVal == 0)
            //{
                //pnlKekMitra.Visible = true;
                //enableKirimUsulan = false;
            //}

            return enableKirimUsulan;
        }

        private bool cekDdlMakro(string idUsulan)
        {
            DataTable dt = new DataTable();
            objPerbaikan.getMakroRiset(ref dt, idUsulan);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["id_makro_riset"].ToString() != "0")
                    return true;
                else
                    return false;
            }

            return false;
        }

        private void isiDataSinta()
        {
            DataTable dt = new DataTable();
            objPengusul.getSinta(ref dt, objLogin.idPersonal);
            if (dt.Rows.Count > 0)
            {
                lblIdSinta.Text = dt.Rows[0]["id_sinta"].ToString();
                //lblSintaId.Text = dt.Rows[0]["id_sinta"].ToString();
                //lblSintaSkor.Text = dt.Rows[0]["skor_sinta"].ToString();
                //lblNatRank.Text = dt.Rows[0]["rangking_nasional"].ToString();
                //lblAffRank.Text = dt.Rows[0]["rangking_afiliasi"].ToString();

                //lblScopusid.Text = dt.Rows[0]["id_scopus"].ToString();
                //lblScopushindex.Text = dt.Rows[0]["hindex"].ToString();
                //lblScopusCitation.Text = dt.Rows[0]["jml_sitasi"].ToString();
                //lblScopusArticle.Text = dt.Rows[0]["jml_dokumen"].ToString();

                //lblGooglescholarid.Text = "";
                //lblGooglehindex.Text = dt.Rows[0]["hindex_google_scholar"].ToString();
                //LblGoogleCitation.Text = dt.Rows[0]["jml_sitasi_google_scholar"].ToString();
                //lblGoogleArticle.Text = dt.Rows[0]["jml_artikel_google_scholar"].ToString();
                //lblGoogleI10.Text = dt.Rows[0]["i_10_hindex_google_scholar"].ToString();


                //lbEdit.Text = "Sinkronisasi";
                //lbKet.Visible = false;


            }


        }




        private void isiRekamJejak(string pIdPersonal)
        {
            DataTable dt = new DataTable();
            if (objPersyaratan.getJmlRekamJejak(ref dt, pIdPersonal))
            {
                lblJmlJurnalInternasional.Text = dt.Rows[0]["jml_rekam_jejak"].ToString();
                lblJmlJurnalNasional.Text = dt.Rows[1]["jml_rekam_jejak"].ToString();
                lblJmlProsiding.Text = dt.Rows[2]["jml_rekam_jejak"].ToString();
                lblJmlHki.Text = dt.Rows[3]["jml_rekam_jejak"].ToString();
                lblJmlBuku.Text = dt.Rows[4]["jml_rekam_jejak"].ToString();
            }
        }

        protected void lbUnduhPdfDok_Click(object sender, EventArgs e)
        {
            unduhPdf("dokumenUsulanRevisi");
        }

        protected void lbUnduhPdfDokWbs_Click(object sender, EventArgs e)
        {
            unduhPdf("dokumenWbs");
        }

        private void unduhPdf(string pathJenisFile)
        {
            string path = String.Format("~/fileUpload/{0}/{1}/{2}.pdf", pathJenisFile, ViewState["thnPelaksanaan"].ToString(), 
                ViewState["id_usulan_kegiatan"].ToString());
            string namaFileAsli = ViewState["id_usulan_kegiatan"].ToString();
            if (pathJenisFile == "dokumenUsulanRevisi")
            {
                string id_transaksi_perbaikan = "00000000-0000-0000-0000-000000000000";
                objPerbaikan.getidTransaksi(ref id_transaksi_perbaikan, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "14");
                path = String.Format("~/fileUpload/{0}/{1}/{2}.pdf", pathJenisFile, ViewState["thnPelaksanaan"].ToString(), id_transaksi_perbaikan);
                namaFileAsli = id_transaksi_perbaikan;
            }
            if (File.Exists(Server.MapPath(path)))
            {
                string PATH_UNGGAH_BERKAS = "~/fileUpload/" + pathJenisFile + "/" + ViewState["thnPelaksanaan"].ToString();
                string namaBerkas = pathJenisFile + ".pdf";
                var atributUnduh = new AtributUnduh
                {
                    FolderUnduh = PATH_UNGGAH_BERKAS,
                    NamaBerkas = namaFileAsli + ".pdf",
                    NamaBerkasdiUnduh = namaBerkas
                };
                Session["AtributUnduh"] = atributUnduh;
                var unduhForm = "helper/unduhFile.aspx";
                Response.Redirect(unduhForm);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        private bool cekFileExists(string ppathDokumenUsulanPdf)
        {
            bool isExists = false;
            if (File.Exists(Server.MapPath(ppathDokumenUsulanPdf)))
            {
                lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                lbUnduhPdfDok.Enabled = true;
                System.IO.FileInfo fs = new System.IO.FileInfo(Server.MapPath(ppathDokumenUsulanPdf));
                double fSize = (double)fs.Length / 1024.0;

                lblUkuranFile.Text = string.Format("{0:0.00}", fSize) + " KByte";
                lblTglUnggah.Text = File.GetLastWriteTime(Server.MapPath(ppathDokumenUsulanPdf)).ToString();
                isExists = true;
            }
            else
            {
                lbUnduhPdfDok.ForeColor = System.Drawing.Color.Gray;
                lbUnduhPdfDok.Enabled = false;
                lblUkuranFile.Text = "-";
                lblTglUnggah.Text = "-";
            }

            return isExists;
        }

        private bool cekFileWbsExists(string ppathDokumenUsulanPdf)
        {
            bool isExists = false;
            if (File.Exists(Server.MapPath(ppathDokumenUsulanPdf)))
            {
                lbUnduhPdfDokWbs.ForeColor = System.Drawing.Color.Red;
                lbUnduhPdfDokWbs.Enabled = true;
                System.IO.FileInfo fs = new System.IO.FileInfo(Server.MapPath(ppathDokumenUsulanPdf));
                double fSize = (double)fs.Length / 1024.0;

                lblUkuranFileWbs.Text = string.Format("{0:0.00}", fSize) + " KByte";
                lblTglUnggahWbs.Text = File.GetLastWriteTime(Server.MapPath(ppathDokumenUsulanPdf)).ToString();
                isExists = true;
            }
            else
            {
                lbUnduhPdfDokWbs.ForeColor = System.Drawing.Color.Gray;
                lbUnduhPdfDokWbs.Enabled = false;
                lblUkuranFileWbs.Text = "-";
                lblTglUnggahWbs.Text = "-";
            }

            return isExists;
        }

        private int isiAnggotaDikti(string p_idUsulanKegiatan)
        {
            DataTable dtAnggota = new DataTable();
            if (objAnggota.listAnggotaDikti(ref dtAnggota, Guid.Parse(p_idUsulanKegiatan)))
            {
                cekStatusPersetujuan(dtAnggota);
                rptAnggotaDikti.DataSource = dtAnggota;
                rptAnggotaDikti.DataBind();
                lblJmlAnggota.Text = dtAnggota.Rows.Count.ToString();

            };

            int jmlNonDikti = isiAnggotaNonDikti(p_idUsulanKegiatan);
            return cekJumlahAnggotaMemenuhi(p_idUsulanKegiatan, dtAnggota.Rows.Count + jmlNonDikti);
        }

        int cekJumlahAnggotaMemenuhi(string pIdUsulanKegiatan, int jmlAnggotaDikti)
        {
            int memenuhi = 0;
            DataTable dt = new DataTable();
            objModelIdentitasUsulan.getDataDataUsulanKegiatan(ref dt, pIdUsulanKegiatan);
            int jmlAnggotaMin = int.Parse(dt.Rows[0]["jml_minimal_anggota"].ToString());
            int jmlAnggotaMax = int.Parse(dt.Rows[0]["jml_maksimal_anggota"].ToString());
            lblInfoAnggotaMinMax.Text = string.Format("Minimal: {0}, maksimal: {1}", jmlAnggotaMin, jmlAnggotaMax);
            if (jmlAnggotaDikti >= jmlAnggotaMin && jmlAnggotaDikti <= jmlAnggotaMax)
                memenuhi = 1;
            return memenuhi;
        }

        void cekStatusPersetujuan(DataTable dt)
        {
            ViewState["sts_persetujuan_anggota"] = 1;
            for (int a = 0; a < dt.Rows.Count; a++)
            {
                string sts = dt.Rows[a]["kd_sts_konfirmasi"].ToString();
                if (sts == "" || sts == "0")
                {
                    ViewState["sts_persetujuan_anggota"] = 0;
                }
            }
        }

        protected void rptAnggota_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblKdSts = (Label)e.Item.FindControl("lblKdSts");
            if (lblKdSts == null) return;
            LinkButton lbSetuju = (LinkButton)e.Item.FindControl("lbSetuju");
            LinkButton lbTidakSetuju = (LinkButton)e.Item.FindControl("lbTidakSetuju");
            LinkButton lbBelumSetuju = (LinkButton)e.Item.FindControl("lbBelumSetuju");
            lbSetuju.Visible = false;
            lbTidakSetuju.Visible = false;
            lbBelumSetuju.Visible = false;
            if (lblKdSts.Text == "0")
            {
                lbBelumSetuju.Visible = true;
                lblKdSts.Text = "Belum";
                lblKdSts.ForeColor = System.Drawing.Color.Blue;
            }
            else if (lblKdSts.Text == "1")
            {
                lbSetuju.Visible = true;
                lblKdSts.Text = "Setuju";
                lblKdSts.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbTidakSetuju.Visible = true;
                lblKdSts.Text = "Menolak";
                lblKdSts.ForeColor = System.Drawing.Color.Red;
            }
        }

        private int isiAnggotaNonDikti(string p_idUsulanKegiatan)
        {
            DataTable dtAnggota = new DataTable();
            if (objAnggota.listAnggotaNonDikti(ref dtAnggota, Guid.Parse(p_idUsulanKegiatan)))
            {
                rptAnggota_nonDikti.DataSource = dtAnggota;
                rptAnggota_nonDikti.DataBind();
                lblJmlAnggotaNonDikti.Text = dtAnggota.Rows.Count.ToString();
            };

            return dtAnggota.Rows.Count;
        }

        private int isiLuaran(string pidUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            //objMdlPdfusulanBaru.GetLuaranWajib(ref dt, Guid.Parse(pidUsulanKegiatan));
            objPerbaikan.GetLuaranWajib(ref dt, Guid.Parse(pidUsulanKegiatan));
            rptLuaranWajib.DataSource = dt;
            rptLuaranWajib.DataBind();

            int kdStatusMemenuhi = 1; // cekLuaranWajib(dt);// dt.Rows.Count;

            dt.Clear();
            dt = new DataTable();
            objPerbaikan.GetLuaranTambahan(ref dt, Guid.Parse(pidUsulanKegiatan));
            rptLuaranTambahan.DataSource = dt;
            rptLuaranTambahan.DataBind();

            return kdStatusMemenuhi;
        }


        private int cekLuaranWajib(DataTable dt)
        {
            int kdStatusMemenuhi = 0;
            if (dt.Rows.Count > 0)
            {
                int lamaKegiatan = int.Parse(ViewState["lama_kegiatan"].ToString());    // = objUsulanKegiatan.lamaKegiatan;
                int urutanTahun = int.Parse(ViewState["urutan_thn_usulan_kegiatan"].ToString());// = objUsulanKegiatan.urutanTahunUsulanKegiatan;
                if (dt.Rows.Count < ((lamaKegiatan - urutanTahun) + 1))
                {
                    return 0;
                }
                else
                {
                    for (int a = urutanTahun; a <= lamaKegiatan; a++)
                    {
                        bool isValid = false;
                        for (int b = 0; b < dt.Rows.Count; b++)
                        {
                            string urutanThnDiisi = dt.Rows[b]["tahun_luaran"].ToString();
                            if (urutanThnDiisi == a.ToString())
                                isValid = true;

                        }
                        if (!isValid)
                            return 0;
                        else
                            kdStatusMemenuhi = 1;
                    }
                }
            }

            return kdStatusMemenuhi;
        }

        private Boolean cekRAB(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
            Boolean hasil = true;
            var dt = new DataTable();
            if (repo.GetCekRekapRABRevisiThnBerjalan(ref dt, Guid.Parse(usulanKegiatan.idUsulanKegiatan)))
            {
                if (dt.Rows.Count > 0)
                {


                    foreach (DataRow row in dt.Rows)
                    {
                        if (float.Parse(row["dana_diajukan"].ToString()) == 0 || float.Parse(row["dana_diajukan"].ToString()) != float.Parse(row["dana_disetujui"].ToString()))
                        { hasil = false; }

                }
                }
                else
                {
                    hasil = false;
                }

            }
            return hasil;
        }


        //rab objModel = new Models.Pengusul.rab();
        public void InitRAB(Models.Sistem.usulanKegiatan objUsulanKegiatan)
        {
            //int retVal = 0;
            DataTable dt = new DataTable();
            //if (objModel.GetRABUsulan(ref dt, Guid.Parse(usulanKegiatan.idUsulanKegiatan)))
            //{
            //    var listAnggaran = new List<ItemRencanaAnggaran>();

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        var itemAnggaran = new ItemRencanaAnggaran();
            //        itemAnggaran.IdRABUsulan = Guid.Parse(dt.Rows[i]["id_rab_usulan"].ToString());
            //        itemAnggaran.KodeJenisPembelanjaan = dt.Rows[i]["kd_jenis_pembelanjaan"].ToString();
            //        itemAnggaran.JenisPembelanjaan = dt.Rows[i]["jenis_pembelanjaan"].ToString();
            //        itemAnggaran.Item = dt.Rows[i]["nama_item"].ToString();
            //        itemAnggaran.Satuan = dt.Rows[i]["xsatuan"].ToString();
            //        itemAnggaran.Volume = decimal.Parse(dt.Rows[i]["xvolume"].ToString());
            //        itemAnggaran.Honor = decimal.Parse(dt.Rows[i]["harga_satuan"].ToString());
            //        itemAnggaran.Total = decimal.Parse(dt.Rows[i]["xvolume"].ToString()) *
            //                             decimal.Parse(dt.Rows[i]["harga_satuan"].ToString());
            //        itemAnggaran.TahunKegiatan = int.Parse(dt.Rows[i]["urutan_thn_usulan_kegiatan"].ToString());

            //        listAnggaran.Add(itemAnggaran);
            //    }
            //    if (listAnggaran.Count > 0)
            //        retVal = isiRAB(listAnggaran);
            //    else
            //    {

            //objUsulanKegiatan = (usulanKegiatan)usulanKegiatan; // Session["usulan_kegiatan"];
                    dt.Clear();
                    dt = new DataTable();
                    var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
                    objIdentitasUsulan.getPlafonDanaSbkThnImplementasi2018(ref dt,
                        objUsulanKegiatan.idKategoriSBK, objUsulanKegiatan.idSkema, objUsulanKegiatan.idBidangFokus);
                    if (dt.Rows.Count > 0)
                    {
                        //lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString()).ToString("N0");
                        //lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString()).ToString("N0");
                    }
                //}

                //dt.Clear();
                //dt = new DataTable();
                //objModel.GetBidangFokus(ref dt, Guid.Parse(usulanKegiatan.idUsulan));
                //if (dt.Rows.Count > 0) lblBidangFokus.Text = dt.Rows[0]["bidang_fokus"].ToString();

            //}
            //else
            //{
            //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //                objModel.errorMessage);
            //}

            ViewState["IdUsulan"] = objUsulanKegiatan.idUsulan;

            //return retVal;
        }

        //private int isiRAB(List<ItemRencanaAnggaran> listAnggaran)
        //{
        //    objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
        //    //lvRABTahun1.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 1).ToList();
        //    //lvRABTahun1.DataBind();

        //    lblTahun1.Text = listAnggaran.Where(i => i.TahunKegiatan == 1)
        //                                 .Sum(i => i.Total)
        //                                 .ToString("N0");
        //    decimal totalThn1 = listAnggaran.Where(i => i.TahunKegiatan == 1)
        //                                 .Sum(i => i.Total);
        //    //lvRABTahun2.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 2).ToList();
        //    //lvRABTahun2.DataBind();
        //    lblTahun2.Text = listAnggaran.Where(i => i.TahunKegiatan == 2)
        //                                 .Sum(i => i.Total)
        //                                 .ToString("N0");
        //    decimal totalThn2 = listAnggaran.Where(i => i.TahunKegiatan == 2)
        //                                 .Sum(i => i.Total);
        //    //lvRABTahun3.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 3).ToList();
        //    //lvRABTahun3.DataBind();
        //    lblTahun3.Text = listAnggaran.Where(i => i.TahunKegiatan == 3)
        //                                 .Sum(i => i.Total)
        //                                 .ToString("N0");
        //    decimal totalThn3 = listAnggaran.Where(i => i.TahunKegiatan == 3)
        //                                 .Sum(i => i.Total);
        //    lblTotalDana.Text = "Total RAB " + objUsulanKegiatan.lamaKegiatan.ToString() + " Tahun Rp. " + listAnggaran.Sum(i => i.Total).ToString("N0");

        //    int kdStatusMemenuhi = 1;
        //    DataTable dt = new DataTable();
        //    var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        //    objIdentitasUsulan.getPlafonDanaSbkThnImplementasi2018(ref dt,
        //        objUsulanKegiatan.idKategoriSBK, objUsulanKegiatan.idSkema, objUsulanKegiatan.idBidangFokus);

        //    decimal totalMinimal = 0, totalMaksimal = 0;
        //    if (dt.Rows.Count > 0)
        //    {
        //        totalMinimal = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString());
        //        totalMaksimal = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString());
        //        lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString()).ToString("N0");
        //        lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString()).ToString("N0");
        //    }
        //    lblBidFokus.Text = objUsulanKegiatan.bidangFokus;
        //    pnlThn2.Visible = true;
        //    pnlThn3.Visible = true;
        //    if (objUsulanKegiatan.lamaKegiatan == 1)
        //    {
        //        pnlThn2.Visible = false;
        //        pnlThn3.Visible = false;

        //        if (totalThn1 < totalMinimal || totalThn1 > totalMaksimal)
        //        {
        //            kdStatusMemenuhi = 0;
        //        }

        //    }
        //    else if (objUsulanKegiatan.lamaKegiatan == 2)
        //    {
        //        pnlThn3.Visible = false;
        //        if (totalThn1 < totalMinimal || totalThn1 > totalMaksimal)
        //        {
        //            kdStatusMemenuhi = 0;
        //        }
        //        if (totalThn2 < totalMinimal || totalThn2 > totalMaksimal)
        //        {
        //            kdStatusMemenuhi = 0;
        //        }
        //    }
        //    else if (objUsulanKegiatan.lamaKegiatan == 3)
        //    {
        //        if (totalThn1 < totalMinimal || totalThn1 > totalMaksimal)
        //        {
        //            kdStatusMemenuhi = 0;
        //        }
        //        if (totalThn2 < totalMinimal || totalThn2 > totalMaksimal)
        //        {
        //            kdStatusMemenuhi = 0;
        //        

        //        if (totalThn3 < totalMinimal || totalThn3 > totalMaksimal)
        //        {
        //            kdStatusMemenuhi = 0;
        //        }
        //    }

        //    return kdStatusMemenuhi;
        //}

        private void isiSummaryRAB(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
            var dt = new DataTable();
            if (repo.GetRekapRABRevisi(ref dt, Guid.Parse(usulanKegiatan.idUsulanKegiatan)))

            {
                rptrSummary.DataSource = dt;
                rptrSummary.DataBind();

                if (dt.Rows.Count > 0)
                {
                    var totalDisetujui = dt.Compute("SUM(dana_disetujui)", string.Empty);
                    lblTotalDanaDisetujui.Text = decimal.Parse(totalDisetujui.ToString()).ToString("N0");

                    var totalDiajukan = dt.Compute("SUM(dana_diajukan)", string.Empty);
                    lblTotalDanaDiajukan.Text = decimal.Parse(totalDiajukan.ToString()).ToString("N0");
                }
                else
                {
                    lblTotalDanaDisetujui.Text = "Rp 0";
                    lblTotalDanaDiajukan.Text = "Rp 0";
                }

            }
        }

        protected void gvMitraPelaksanaPenelitian_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvMitraPelaksanaPenelitian_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id_mitra = gvMitraPelaksanaPenelitian.DataKeys[e.Row.RowIndex]["id_mitra"].ToString();
                string filePath = string.Format("~/fileUpload/Mitra/{0}.pdf", id_mitra);
                LinkButton lbUnduhDokMitraPelaksanaPenelitian = (LinkButton)e.Row.FindControl("lbUnduhDokMitraPelaksanaPenelitian");
                if (File.Exists(Server.MapPath(filePath)))
                {
                    lbUnduhDokMitraPelaksanaPenelitian.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbUnduhDokMitraPelaksanaPenelitian.ForeColor = System.Drawing.Color.Gray;
                }
                if (ViewState["lama_kegiatan"].ToString() == "1")
                {
                    Label lblDanaThn2 = (Label)e.Row.FindControl("lblDanaThn2");
                    Label lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
                    Label lblDana2 = (Label)e.Row.FindControl("lblDana2");
                    Label lblDana3 = (Label)e.Row.FindControl("lblDana3");
                    lblDanaThn2.Visible = false;
                    lblDanaThn3.Visible = false;
                    lblDana2.Visible = false;
                    lblDana3.Visible = false;
                }
                else if (ViewState["lama_kegiatan"].ToString() == "2")
                {
                    Label lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
                    Label lblDana3 = (Label)e.Row.FindControl("lblDana3");
                    lblDanaThn3.Visible = false;
                    lblDana3.Visible = false;
                }
            }
        }

        Models.Pengusul.mitraPenelitian objMitra = new Models.Pengusul.mitraPenelitian();
        public int isiMitra(string idUsulanKegiatan, int pIdSkema)
        {
            int kdStatusMemenuhi = 0;
            //panelMitraCalonPengguna.Visible = false;
            //panelMitraInvestor.Visible = false;

            DataTable dtw = new DataTable();
            objMitra.getMitraWajibBySkema(ref dtw, pIdSkema);

            int kdKategoriMitra = 0;
            if (dtw.Rows.Count > 0)
            {
                kdKategoriMitra = int.Parse(dtw.Rows[0]["kd_kategori_mitra"].ToString());
                if (kdKategoriMitra == 2) // Mitra Calon Pengguna
                {
                    //panelMitraCalonPengguna.Visible = true;
                    lblWajibMCP.Text = "Wajib Ada";
                    lblWajibMI.Text = "Tidak Wajib Ada";
                }
                else if (kdKategoriMitra == 3) // Mitra investor
                {
                    //panelMitraCalonPengguna.Visible = true;
                    lblWajibMCP.Text = "Tidak Wajib Ada";
                    lblWajibMI.Text = "Wajib Ada";
                }
            }
            else
            {
                kdStatusMemenuhi = 1;
            }

            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            int p_jml_data = 0;
            int p_offset = 0;
            DataTable dtMitra = new DataTable();
            if (objMitra.listMitraPelaksanaPenelitian(ref dtMitra, Guid.Parse(idUsulanKegiatan), p_jml_data, p_offset))
            {
                var drmitrapelaksana = dtMitra.Select("kd_kategori_mitra = 1");
                if (drmitrapelaksana.Length > 0)
                {
                    var dt = drmitrapelaksana.CopyToDataTable();
                    gvMitraPelaksanaPenelitian.DataSource = dt;
                    //gvMitraPelaksanaPenelitian.DataBind();
                }

                var drmitracalonpengguna = dtMitra.Select("kd_kategori_mitra = 2");
                if (drmitracalonpengguna.Length > 0)
                {
                    var dtcalon = drmitracalonpengguna.CopyToDataTable();
                    gvMitraCalonPengguna.DataSource = dtcalon;

                    if (kdKategoriMitra == 2)
                        kdStatusMemenuhi = cekDokumenMitraWajibAda(dtcalon);
                    //gvMitraCalonPengguna.DataBind();

                }

                var drmitrainvestor = dtMitra.Select("kd_kategori_mitra = 3");
                if (drmitrainvestor.Length > 0)
                {
                    var dtcaloninvestor = drmitrainvestor.CopyToDataTable();
                    gvMitraInvestor.DataSource = dtcaloninvestor;
                    //gvMitraInvestor.DataBind();
                    if (kdKategoriMitra == 3)
                        kdStatusMemenuhi = cekDokumenMitraWajibAda(dtcaloninvestor);
                }
                gvMitraPelaksanaPenelitian.DataBind();
                gvMitraCalonPengguna.DataBind();
                gvMitraInvestor.DataBind();
                isiMitraPenelitianPerSkema();
            };

            return kdStatusMemenuhi;
        }


        private int cekDokumenMitraWajibAda(DataTable dtMitra)
        {
            int kdStatusMemenuhi = 1;
            string folderPath = "~/fileUpload/Mitra/{0}.pdf";
            for (int a = 0; a < dtMitra.Rows.Count; a++)
            {
                string id_mitra = dtMitra.Rows[a]["id_mitra"].ToString();
                string filePath = string.Format(folderPath, id_mitra);

                if (!File.Exists(Server.MapPath(filePath)))
                {
                    kdStatusMemenuhi = 0;
                    return kdStatusMemenuhi;
                }

            }


            return kdStatusMemenuhi;
        }

        protected void gvMitraCalonPengguna_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id_mitra = gvMitraCalonPengguna.DataKeys[e.Row.RowIndex]["id_mitra"].ToString();
                string filePath = string.Format("~/fileUpload/Mitra/{0}.pdf", id_mitra);
                LinkButton lbUnduhDokMitraCalonPengguna = (LinkButton)e.Row.FindControl("lbUnduhDokMitraCalonPengguna");
                if (File.Exists(Server.MapPath(filePath)))
                {
                    lbUnduhDokMitraCalonPengguna.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbUnduhDokMitraCalonPengguna.ForeColor = System.Drawing.Color.Gray;
                }
                if (ViewState["lama_kegiatan"].ToString() == "1")
                {
                    Label lblDanaThn2 = (Label)e.Row.FindControl("lblDanaThn2");
                    Label lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
                    Label lblDana2 = (Label)e.Row.FindControl("lblDana2");
                    Label lblDana3 = (Label)e.Row.FindControl("lblDana3");
                    lblDanaThn2.Visible = false;
                    lblDanaThn3.Visible = false;
                    lblDana2.Visible = false;
                    lblDana3.Visible = false;
                }
                else if (ViewState["lama_kegiatan"].ToString() == "2")
                {
                    Label lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
                    Label lblDana3 = (Label)e.Row.FindControl("lblDana3");
                    lblDanaThn3.Visible = false;
                    lblDana3.Visible = false;
                }
            }
        }

        protected void gvMitraInvestor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id_mitra = gvMitraInvestor.DataKeys[e.Row.RowIndex]["id_mitra"].ToString();
                string filePath = string.Format("~/fileUpload/Mitra/{0}.pdf", id_mitra);
                LinkButton lbUnduhDokMitraInvestorPenelitian = (LinkButton)e.Row.FindControl("lbUnduhDokMitraInvestorPenelitian");
                if (File.Exists(Server.MapPath(filePath)))
                {
                    lbUnduhDokMitraInvestorPenelitian.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbUnduhDokMitraInvestorPenelitian.ForeColor = System.Drawing.Color.Gray;
                }
                if (ViewState["lama_kegiatan"].ToString() == "1")
                {
                    Label lblDanaThn2 = (Label)e.Row.FindControl("lblDanaThn2");
                    Label lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
                    Label lblDana2 = (Label)e.Row.FindControl("lblDana2");
                    Label lblDana3 = (Label)e.Row.FindControl("lblDana3");
                    lblDanaThn2.Visible = false;
                    lblDanaThn3.Visible = false;
                    lblDana2.Visible = false;
                    lblDana3.Visible = false;
                }
                else if (ViewState["lama_kegiatan"].ToString() == "2")
                {
                    Label lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
                    Label lblDana3 = (Label)e.Row.FindControl("lblDana3");
                    lblDanaThn3.Visible = false;
                    lblDana3.Visible = false;
                }
            }
        }


        public void isiMitraPenelitianPerSkema()
        {
            DataTable dtMitra = new DataTable();
            //objMitra.getMitraPenelitianPerSkema(ref dtDurasi, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            objMitra.getMitraPenelitianPerSkema(ref dtMitra, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            if (dtMitra.Rows.Count > 0)
            {
                //lblSkema.Text = dtMitra.Rows[0]["nama_skema"].ToString();
                //lblLamaUsulan.Text = dtMitra.Rows[0]["lama_kegiatan"].ToString();
                ViewState["lama_kegiatan"] = dtMitra.Rows[0]["lama_kegiatan"].ToString();
                //lblUrutanUsulan.Text = dtMitra.Rows[0]["urutan_thn_usulan"].ToString();
                ViewState["mitraPelaksana"] = dtMitra.Rows[0]["mitra_pelaksana"].ToString();
                ViewState["mitraCalonPengguna"] = dtMitra.Rows[0]["mitra_calon_pengguna"].ToString();
                ViewState["mitraInvestor"] = dtMitra.Rows[0]["mitra_investor"].ToString();
            }

            if (ViewState["mitraPelaksana"].ToString() == "1")
            {
                panelMitraPelaksana.Visible = true;
            }
            else
            {
                panelMitraPelaksana.Visible = false;
            }

            if (ViewState["mitraCalonPengguna"].ToString() == "1")
            {
                panelMitraCalonPengguna.Visible = true;
            }
            else
            {
                panelMitraCalonPengguna.Visible = false;
            }

            if (ViewState["mitraInvestor"].ToString() == "1")
            {
                panelMitraInvestor.Visible = true;
            }
            else
            {
                panelMitraInvestor.Visible = false;
            }

            if (ViewState["mitraInvestor"].ToString() != "1" && ViewState["mitraCalonPengguna"].ToString() != "1" && ViewState["mitraInvestor"].ToString() != "1")
            {
                lblInfoMitra.Visible = true;
            }

        }

        protected void gvMitraPelaksanaPenelitian_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraPelaksanaPenelitian.DataKeys[rowIndex]["id_mitra"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvMitraPelaksanaPenelitian.DataKeys[rowIndex]["nama_mitra"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "MitraPelaksana_" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string filePath = string.Format("~/fileUpload/mitra/{0}.pdf",
                    idMitra);

                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = "~/fileUpload/mitra/",//PATH_UNGGAH_BERKAS,
                        NamaBerkas = idMitra + ".pdf",
                        NamaBerkasdiUnduh = namaFile
                    };
                    Session["AtributUnduh"] = atributUnduh;

                    var unduhForm = "helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan.");
                    return;
                }
            }
        }
        public static string ConvertDateTimeToDate(string dateTimeString, string dateTimeFormat, String langCulture)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(langCulture);
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(dateTimeString, out dt))
            {
                return dt.ToString(dateTimeFormat, culture);
            }
            return dateTimeString;
        }
        protected void gvMitraCalonPengguna_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraCalonPengguna.DataKeys[rowIndex]["id_mitra"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvMitraCalonPengguna.DataKeys[rowIndex]["nama_mitra"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "MitraCalonPengguna_" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string filePath = string.Format("~/fileUpload/mitra/{0}.pdf",
                    idMitra);

                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = "~/fileUpload/mitra/",//PATH_UNGGAH_BERKAS,
                        NamaBerkas = idMitra + ".pdf",
                        NamaBerkasdiUnduh = namaFile
                    };
                    Session["AtributUnduh"] = atributUnduh;

                    var unduhForm = "helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan.");
                    return;
                }
            }
        }

        protected void gvMitraInvestor_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraInvestor.DataKeys[rowIndex]["id_mitra"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvMitraInvestor.DataKeys[rowIndex]["nama_mitra"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "MitraInvestor_" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string filePath = string.Format("~/fileUpload/mitra/{0}.pdf",
                    idMitra);

                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = "~/fileUpload/mitra/",//PATH_UNGGAH_BERKAS,
                        NamaBerkas = idMitra + ".pdf",
                        NamaBerkasdiUnduh = namaFile
                    };
                    Session["AtributUnduh"] = atributUnduh;

                    var unduhForm = "helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan.");
                    return;
                }
            }
        }
    }


}