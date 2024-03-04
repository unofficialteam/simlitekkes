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

namespace simlitekkes.UserControls.Pengusul.pendanaan2021
{
    public partial class rekapUsulan : System.Web.UI.UserControl
    {
        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        UIControllers.uiDropdownList obj_uiDDL = new UIControllers.uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        Models.Sistem.referensiData objRefData = new Models.Sistem.referensiData();
        Models.Pengusul.persyaratanUmum objPersyaratan = new Models.Pengusul.persyaratanUmum();

        Models.login objLogin = new Models.login();
        kontrolUnggah ktUnggah = new kontrolUnggah();
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        Models.Pengusul.berandaPengusul objPengusul = new Models.Pengusul.berandaPengusul();
        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();

        pdfUsulanBaru objMdlPdfusulanBaru = new pdfUsulanBaru();
        uiNotify noty = new uiNotify();

        RAB objModel = new Models.Pengusul.RAB();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //objLogin.autentifikasi("9999901122", "ato123");
                //Session["objLogin"] = objLogin;
                //string idUsulanKegiatan = "c01bc9c0-638f-4023-8046-3dfaaac39904";
                //initUsulan(idUsulanKegiatan);

                //ImgPdf.ImageUrl = Server.MapPath("~/Images/icon/pdf-red.png");
                //imgSetujuAnggota.ImageUrl = Server.MapPath("~/Images/icon/setuju.png");
                //isiIdentitasKetua();
                //isiRekamJejak();
                //isiIdentitasUsulan();
                //isiAnggotaDosen();
                //isiAnggotaNonDosen();

            }
        }



        public bool initUsulan(string idUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objModelIdentitasUsulan.getDataDataUsulanKegiatan(ref dt, idUsulanKegiatan);
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            Session["id_usulan_kegiatan_unduh"] = idUsulanKegiatan;
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
                idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString()),
                thnPertamaUsulan = dt.Rows[0]["thn_pertama_usulan"].ToString()
            };
            ViewState["thn_usulan"] = objUsulanKegiatan.thnUsulan;
            ViewState["tktTarget"] = objUsulanKegiatan.tktTarget;
            ViewState["lamaKegiatan"] = objUsulanKegiatan.lamaKegiatan;
            string info = "Penelitian Dasar (Tahun ke-{0} dari {1} tahun)";
            if (objUsulanKegiatan.tktTarget <= 3)
            {
                lblInfoPenelitianSbk.Text = string.Format(info, objUsulanKegiatan.urutanTahunUsulanKegiatan, objUsulanKegiatan.lamaKegiatan);
            }
            else if (objUsulanKegiatan.tktTarget <= 6)
            {
                info = "Penelitian Terapan (Tahun ke-{0} dari {1} tahun)";
                lblInfoPenelitianSbk.Text = string.Format(info, objUsulanKegiatan.urutanTahunUsulanKegiatan, objUsulanKegiatan.lamaKegiatan);
            }
            else if (objUsulanKegiatan.tktTarget <= 9)
            {
                info = "Penelitian Pengembangan (Tahun ke-{0} dari {1} tahun)";
                lblInfoPenelitianSbk.Text = string.Format(info, objUsulanKegiatan.urutanTahunUsulanKegiatan, objUsulanKegiatan.lamaKegiatan);
            }

            lblJudul.Text = objUsulanKegiatan.judul;
            lblInfoSkema.Text = objUsulanKegiatan.namaSkema;
            lblInfoThn.Text = string.Format("Thn Usulan {0} | Thn. Pelaksanaan {1}-{2}", objUsulanKegiatan.thnUsulan,
                objUsulanKegiatan.thnPelaksanaan, (int.Parse(objUsulanKegiatan.thnPertamaUsulan) + objUsulanKegiatan.lamaKegiatan));

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
            lblBidFokus.Text = objUsulanKegiatan.bidangFokus;
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
            lblJudul.Text = objUsulanKegiatan.judul;
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

            bool isMakroRiset = true;
            if (!cekDdlMakro(objUsulanKegiatan.idUsulan))
            {
                isMakroRiset = false;
            }

            string dirFile = "~/fileUpload/dokumenUsulan/" + objUsulanKegiatan.thnUsulan;
            string path2save = String.Format(dirFile + "/{0}.pdf", objUsulanKegiatan.idUsulanKegiatan);
            if (!cekFileExists(path2save) || !isMakroRiset)
            {
                pnlKekDokumenUsulan.Visible = true;
                enableKirimUsulan = false;
            }

            if (retVal == 0)
            {
                pnlKekAnggota.Visible = true;
                enableKirimUsulan = false;
            }

            //if (ViewState["sts_persetujuan_anggota"].ToString() != "1" || ViewState["sts_persetujuan_anggota_tendik"].ToString() != "1")
            //{
            //    pnlKekAnggota.Visible = true;
            //    enableKirimUsulan = false;
            //}

            retVal = isiRab(objUsulanKegiatan);

            if (retVal == 0)
            {
                pnlKekRab.Visible = true;
                enableKirimUsulan = false;
            }


            retVal = isiLuaran(idUsulanKegiatan, objUsulanKegiatan.tktTarget);
            if (retVal == 0)
            {
                pnlKekLuaran.Visible = true;
                enableKirimUsulan = false;
            }

            if (objUsulanKegiatan.idSkema == 72) // KRUPT
            {
                panelKekuranganWbs.Visible = false;
                panelWbs.Visible = true;
                dirFile = "~/fileUpload/dokumenWbs/" + objUsulanKegiatan.thnUsulan;
                path2save = String.Format(dirFile + "/{0}.pdf", objUsulanKegiatan.idUsulanKegiatan);
                if (!cekFileWbsExists(path2save))
                {
                    panelKekuranganWbs.Visible = true;
                    enableKirimUsulan = false;
                }
            }
            ViewState["dokumen_mitra_blmdiunggah"] = "0";
            retVal = isiMitra(idUsulanKegiatan, objUsulanKegiatan.idSkema);
            
            if (retVal == 0 || ViewState["dokumen_mitra_blmdiunggah"].ToString() == "1")
            {
                pnlKekMitra.Visible = true;
                enableKirimUsulan = false;
            }

            return enableKirimUsulan;
        }
        private bool cekDdlMakro(string idUsulan)
        {
            simlitekkes.Models.Pengusul.PerbaikanUsulan objPerbaikan = new simlitekkes.Models.Pengusul.PerbaikanUsulan();
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


        //pdfUsulanLengkap.UnduhProposalLengkap(Session["id_usulan_kegiatan_unduh"].ToString());
        //    Session.Remove("id_usulan_kegiatan_unduh");
        protected void lbUnduhPdfDokumenLengkap_Click(object sender, EventArgs e)
        {
            pdfUsulanLengkap.UnduhProposalLengkap(Session["id_usulan_kegiatan_unduh"].ToString());
            Session.Remove("id_usulan_kegiatan_unduh");
        }

        protected void lbUnduhPdfDok_Click(object sender, EventArgs e)
        {
            unduhPdf("dokumenUsulan");
        }

        protected void lbUnduhPdfDokWbs_Click(object sender, EventArgs e)
        {
            unduhPdf("dokumenWbs");
        }

        private void unduhPdf(string pathJenisFile)
        {
            string path = String.Format("fileUpload/{0}/{1}/{2}.pdf", pathJenisFile, ViewState["thn_usulan"].ToString(), ViewState["id_usulan_kegiatan"].ToString());
            string path2 = String.Format("fileUpload/{0}/{1}/{2}.pdf", pathJenisFile, ViewState["thn_usulan"].ToString(), ViewState["id_usulan_kegiatan"].ToString());

            if (File.Exists(Server.MapPath(path)))
            {
                string PATH_UNGGAH_BERKAS = "../fileUpload/" + pathJenisFile + "/" + ViewState["thn_usulan"].ToString();
                string namaBerkas = pathJenisFile + ".pdf";
                var atributUnduh = new AtributUnduh
                {
                    FolderUnduh = PATH_UNGGAH_BERKAS,
                    NamaBerkas = ViewState["id_usulan_kegiatan"] + ".pdf",
                    NamaBerkasdiUnduh = namaBerkas
                };
                Session["AtributUnduh"] = atributUnduh;
                var unduhForm = "helper/unduhFile.aspx";
                Response.Redirect(unduhForm);
            }
            else if (File.Exists(Server.MapPath(path2)))
            {
                string PATH_UNGGAH_BERKAS = "../fileUpload/" + pathJenisFile + "/" + ViewState["thn_usulan"].ToString();
                string namaBerkas = pathJenisFile + ".pdf";
                var atributUnduh = new AtributUnduh
                {
                    FolderUnduh = PATH_UNGGAH_BERKAS,
                    NamaBerkas = ViewState["id_usulan_kegiatan"] + ".pdf",
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

                lbUnduhPdfDokumen.ForeColor = System.Drawing.Color.Red;
                lbUnduhPdfDokumen.Enabled = true;

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

                lbUnduhPdfDokumen.ForeColor = System.Drawing.Color.Gray;
                lbUnduhPdfDokumen.Enabled = false;

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

            
            DataTable dtAnggotaTendik = new DataTable();
            if (objAnggota.listAnggotaTendik(ref dtAnggotaTendik, Guid.Parse(p_idUsulanKegiatan)))
            {
                cekStatusPersetujuanTendik(dtAnggotaTendik);
                rptAnggotaTendik.DataSource = dtAnggotaTendik;
                rptAnggotaTendik.DataBind();
                lblJmlAnggotaTendik.Text = dtAnggotaTendik.Rows.Count.ToString();
            };

            int jmlNonDikti = isiAnggotaNonDikti(p_idUsulanKegiatan);
            //return cekJumlahAnggotaMemenuhi(p_idUsulanKegiatan, dtAnggota.Rows.Count + dtAnggotaTendik.Rows.Count + jmlNonDikti);
            return 1;
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

        void cekStatusPersetujuanTendik(DataTable dt)
        {
            ViewState["sts_persetujuan_anggota_tendik"] = 1;
            for (int a = 0; a < dt.Rows.Count; a++)
            {
                string sts = dt.Rows[a]["kd_sts_konfirmasi"].ToString();
                if (sts == "" || sts == "0")
                {
                    ViewState["sts_persetujuan_anggota_tendik"] = 0;
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
            //if (lblKdSts.Text == "0")
            //{
            //    lbBelumSetuju.Visible = true;
            //    lblKdSts.Text = "Belum";
            //    lblKdSts.ForeColor = System.Drawing.Color.Blue;
            //}
            //else if (lblKdSts.Text == "1")
            //{
                lbSetuju.Visible = true;
                lblKdSts.Text = "Setuju";
                lblKdSts.ForeColor = System.Drawing.Color.Green;
            //}
            //else
            //{
            //    lbTidakSetuju.Visible = true;
            //    lblKdSts.Text = "Menolak";
            //    lblKdSts.ForeColor = System.Drawing.Color.Red;
            //}
            lblKdSts.Visible = false;
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

        private int isiLuaran(string pidUsulanKegiatan, int level_tkt)
        {
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.GetLuaranWajib(ref dt, Guid.Parse(pidUsulanKegiatan));
            ViewState["jml_luaran_wajib"] = dt.Rows.Count;
            rptLuaranWajib.DataSource = dt;
            rptLuaranWajib.DataBind();

            int kdStatusMemenuhi = cekLuaranWajib(dt);// dt.Rows.Count;

            //if (level_tkt > 3 && dt.Rows.Count > 0) // terapan & pengembangan
            //{
            //    kdStatusMemenuhi = 1;
            //}


            dt = new DataTable();
            objMdlPdfusulanBaru.GetLuaranTambahan(ref dt, Guid.Parse(pidUsulanKegiatan));
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


        //rab objModel = new Models.Pengusul.rab();
        /*
        public int InitRAB(Models.sistem.usulanKegiatan usulanKegiatan)
        {
            
            int retVal = 0;
            DataTable dt = new DataTable();
            if (objModel.GetRABUsulan(ref dt, Guid.Parse(usulanKegiatan.idUsulanKegiatan)))
            {
                var listAnggaran = new List<ItemRencanaAnggaran>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var itemAnggaran = new ItemRencanaAnggaran();
                    itemAnggaran.IdRABUsulan = Guid.Parse(dt.Rows[i]["id_rab_usulan"].ToString());
                    itemAnggaran.KodeJenisPembelanjaan = dt.Rows[i]["kd_jenis_pembelanjaan"].ToString();
                    itemAnggaran.JenisPembelanjaan = dt.Rows[i]["jenis_pembelanjaan"].ToString();
                    itemAnggaran.Item = dt.Rows[i]["nama_item"].ToString();
                    itemAnggaran.Satuan = dt.Rows[i]["xsatuan"].ToString();
                    itemAnggaran.Volume = decimal.Parse(dt.Rows[i]["xvolume"].ToString());
                    itemAnggaran.Honor = decimal.Parse(dt.Rows[i]["harga_satuan"].ToString());
                    itemAnggaran.Total = decimal.Parse(dt.Rows[i]["xvolume"].ToString()) *
                                         decimal.Parse(dt.Rows[i]["harga_satuan"].ToString());
                    itemAnggaran.TahunKegiatan = int.Parse(dt.Rows[i]["urutan_thn_usulan_kegiatan"].ToString());

                    listAnggaran.Add(itemAnggaran);
                }
                if (listAnggaran.Count > 0)
                    retVal = isiRAB(listAnggaran);
                else
                {

                    objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
                    dt.Clear();
                    dt = new DataTable();
                    var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
                    objIdentitasUsulan.getPlafonDanaSbkThnImplementasi2018(ref dt,
                        objUsulanKegiatan.idKategoriSBK, objUsulanKegiatan.idSkema, objUsulanKegiatan.idBidangFokus);
                    if (dt.Rows.Count > 0)
                    {
                        lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString()).ToString("N0");
                        lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString()).ToString("N0");
                    }
                }
                
                //dt.Clear();
                //dt = new DataTable();
                //objModel.GetBidangFokus(ref dt, Guid.Parse(usulanKegiatan.idUsulan));
                //if (dt.Rows.Count > 0) lblBidangFokus.Text = dt.Rows[0]["bidang_fokus"].ToString();

            }
            //else
            //{
            //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //                objModel.errorMessage);
            //}

            ViewState["IdUsulan"] = usulanKegiatan.idUsulan;

            return retVal;

        }
        */
        private int isiRab(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
            int retval = 1;
            int hitungPertahunRabDiisi = 0;
            var dt = new DataTable();
            if (objModel.GetKelompokBiaya(ref dt, "1"))
            {

            }
            //dtAll.Merge(dtTwo);
            DataTable[] arrDt = new DataTable[usulanKegiatan.lamaKegiatan];
            DataTable dtw = new DataTable();
            DataTable dtt = new DataTable();
            decimal totalRab = 0;
            decimal totalThn1d = 0, totalThn2d = 0, totalThn3d = 0;
            for (int a = usulanKegiatan.urutanTahunUsulanKegiatan; a <= usulanKegiatan.lamaKegiatan; a++) // urutan tahun
            {
                DataTable dtPertHn = new DataTable();

                for (int b = 0; b < dt.Rows.Count; b++) // kelompok biaya rab dt.Rows.Count
                {
                    int id_rab_kelompok_biaya = int.Parse(dt.Rows[b]["id_rab_kelompok_biaya"].ToString()); // id_rab_kelompok_biaya
                    if (objModel.GetItemBelanja(ref dtw, Guid.Parse(usulanKegiatan.idUsulanKegiatan), a, id_rab_kelompok_biaya))
                    {
                        dtPertHn.Merge(dtw);
                    }
                }
                if (a == 1)
                {
                    if (dtPertHn.Rows.Count > 0)
                    {
                        var dr1 = dtPertHn.Select("volume > 0");
                        if (dr1.Length > 0)
                        {
                            DataTable dtt1 = dr1.CopyToDataTable();
                            //lvRABTahun1.DataSource = dtt1;
                            //lvRABTahun1.DataBind();
                            var totalThn1 = dtPertHn.Compute("SUM(total_harga)", string.Empty);
                            totalThn1d = decimal.Parse(totalThn1.ToString());
                            totalRab += decimal.Parse(totalThn1.ToString());
                            lblTahun1.Text = decimal.Parse(totalThn1.ToString()).ToString("N0");
                            if (decimal.Parse(totalThn1.ToString()) > 0)
                                hitungPertahunRabDiisi++;
                        }
                    }
                }
                else if (a == 2)
                {
                    if (dtPertHn.Rows.Count > 0)
                    {
                        var dr2 = dtPertHn.Select("volume > 0");
                        if (dr2.Length > 0)
                        {
                            DataTable dtt2 = dr2.CopyToDataTable();
                            //lvRABTahun2.DataSource = dtt2;
                            //lvRABTahun2.DataBind();
                            var totalThn2 = dtPertHn.Compute("SUM(total_harga)", string.Empty);
                            totalThn2d = decimal.Parse(totalThn2.ToString());
                            totalRab += decimal.Parse(totalThn2.ToString());
                            lblTahun2.Text = decimal.Parse(totalThn2.ToString()).ToString("N0");
                            if (decimal.Parse(totalThn2.ToString()) > 0)
                                hitungPertahunRabDiisi++;
                        }
                    }
                }
                else if (a == 3)
                {
                    if (dtPertHn.Rows.Count > 0)
                    {
                        var dr3 = dtPertHn.Select("volume > 0");
                        if (dr3.Length > 0)
                        {
                            DataTable dtt3 = dr3.CopyToDataTable();
                            //lvRABTahun3.DataSource = dtt3;
                            //lvRABTahun3.DataBind();
                            var totalThn3 = dtPertHn.Compute("SUM(total_harga)", string.Empty);
                            totalThn3d = decimal.Parse(totalThn3.ToString());
                            totalRab += decimal.Parse(totalThn3.ToString());
                            lblTahun3.Text = decimal.Parse(totalThn3.ToString()).ToString("N0");
                            if (decimal.Parse(totalThn3.ToString()) > 0)
                                hitungPertahunRabDiisi++;
                        }
                    }
                }
                dtPertHn.Clear();
            }
            lblTotalDana.Text = "Total RAB " + usulanKegiatan.lamaKegiatan.ToString() + " Tahun Rp. " + totalRab.ToString("N0");

            if (hitungPertahunRabDiisi < usulanKegiatan.lamaKegiatan)
                return 0;

            int kdStatusMemenuhi = 1;
            dt.Clear();
            dt = new DataTable();
            var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
            objIdentitasUsulan.getPlafonDanaSbkThnImplementasi2018(ref dt,
                usulanKegiatan.idKategoriSBK, usulanKegiatan.idSkema, usulanKegiatan.idBidangFokus);

            decimal totalMinimal = 0, totalMaksimal = 0;
            if (dt.Rows.Count > 0)
            {
                totalMinimal = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString());
                totalMaksimal = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString());
                lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString()).ToString("N0");
                lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString()).ToString("N0");
            }
            lblBidFokus.Text = usulanKegiatan.bidangFokus;
            pnlThn2.Visible = true;
            pnlThn3.Visible = true;
            if (usulanKegiatan.lamaKegiatan == 1)
            {
                pnlThn2.Visible = false;
                pnlThn3.Visible = false;

                if (totalThn1d < totalMinimal || totalThn1d > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }

            }
            else if (usulanKegiatan.lamaKegiatan == 2)
            {
                pnlThn3.Visible = false;
                if (totalThn1d < totalMinimal || totalThn1d > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
                if (totalThn2d < totalMinimal || totalThn2d > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
            }
            else if (usulanKegiatan.lamaKegiatan == 3)
            {
                if (totalThn1d < totalMinimal || totalThn1d > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
                if (totalThn2d < totalMinimal || totalThn2d > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
                if (totalThn3d < totalMinimal || totalThn3d > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
            }

            if (totalRab > 0)
                kdStatusMemenuhi = 1;

            return kdStatusMemenuhi;
        }


        /*
        private int isiRAB(List<ItemRencanaAnggaran> listAnggaran)
        {
            objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
            //lvRABTahun1.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 1).ToList();
            //lvRABTahun1.DataBind();

            lblTahun1.Text = listAnggaran.Where(i => i.TahunKegiatan == 1)
                                         .Sum(i => i.Total)
                                         .ToString("N0");
            decimal totalThn1 = listAnggaran.Where(i => i.TahunKegiatan == 1)
                                         .Sum(i => i.Total);
            //lvRABTahun2.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 2).ToList();
            //lvRABTahun2.DataBind();
            lblTahun2.Text = listAnggaran.Where(i => i.TahunKegiatan == 2)
                                         .Sum(i => i.Total)
                                         .ToString("N0");
            decimal totalThn2 = listAnggaran.Where(i => i.TahunKegiatan == 2)
                                         .Sum(i => i.Total);
            //lvRABTahun3.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 3).ToList();
            //lvRABTahun3.DataBind();
            lblTahun3.Text = listAnggaran.Where(i => i.TahunKegiatan == 3)
                                         .Sum(i => i.Total)
                                         .ToString("N0");
            decimal totalThn3 = listAnggaran.Where(i => i.TahunKegiatan == 3)
                                         .Sum(i => i.Total);
            lblTotalDana.Text = "Total RAB " + objUsulanKegiatan.lamaKegiatan.ToString() + " Tahun Rp. " + listAnggaran.Sum(i => i.Total).ToString("N0");

            int kdStatusMemenuhi = 1;
            DataTable dt = new DataTable();            
            var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
            objIdentitasUsulan.getPlafonDanaSbkThnImplementasi2018(ref dt,
                objUsulanKegiatan.idKategoriSBK, objUsulanKegiatan.idSkema, objUsulanKegiatan.idBidangFokus);

            decimal totalMinimal=0, totalMaksimal=0;
            if (dt.Rows.Count > 0)
            {
                totalMinimal = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString());
                totalMaksimal = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString());
                lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString()).ToString("N0");
                lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString()).ToString("N0");
            }
            lblBidFokus.Text = objUsulanKegiatan.bidangFokus;
            pnlThn2.Visible = true;
            pnlThn3.Visible = true;
            if (objUsulanKegiatan.lamaKegiatan == 1)
            {
                pnlThn2.Visible = false;
                pnlThn3.Visible = false;

                if(totalThn1 < totalMinimal || totalThn1 > totalMaksimal )
                {
                    kdStatusMemenuhi = 0;
                }

            }
            else if (objUsulanKegiatan.lamaKegiatan == 2)
            {
                pnlThn3.Visible = false;
                if (totalThn1 < totalMinimal || totalThn1 > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
                if (totalThn2 < totalMinimal || totalThn2 > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
            }
            else if (objUsulanKegiatan.lamaKegiatan == 3)
            {
                if (totalThn1 < totalMinimal || totalThn1 > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
                if (totalThn2 < totalMinimal || totalThn2 > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
                if (totalThn3 < totalMinimal || totalThn3 > totalMaksimal)
                {
                    kdStatusMemenuhi = 0;
                }
            }

            return kdStatusMemenuhi;
        }
        */
        protected void gvMitraPelaksanaPenelitian_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvMitraPelaksanaPenelitian_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id_mitra = gvMitraPelaksanaPenelitian.DataKeys[e.Row.RowIndex]["id_mitra"].ToString();
                string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf",
                    ViewState["thn_usulan"].ToString(), id_mitra);

                LinkButton lbUnduhDokMitraPelaksanaPenelitian = (LinkButton)e.Row.FindControl("lbUnduhDokMitraPelaksanaPenelitian");
                if (File.Exists(Server.MapPath(filePath)))
                {
                    lbUnduhDokMitraPelaksanaPenelitian.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    ViewState["dokumen_mitra_blmdiunggah"] = 1;
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

            kdStatusMemenuhi = 1;
            return kdStatusMemenuhi;
        }

        private int cekDokumenMitraWajibAda(DataTable dtMitra)
        {
            int kdStatusMemenuhi = 1;
            string folderPath = "~/fileUpload/Mitra/{0}/{1}.pdf";
            for (int a = 0; a < dtMitra.Rows.Count; a++)
            {
                string id_mitra = dtMitra.Rows[a]["id_mitra"].ToString();
                string filePath = string.Format(folderPath, ViewState["thn_usulan"].ToString(), id_mitra);

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
                string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf",
                    ViewState["thn_usulan"].ToString(), id_mitra);
                LinkButton lbUnduhDokMitraCalonPengguna = (LinkButton)e.Row.FindControl("lbUnduhDokMitraCalonPengguna");
                if (File.Exists(Server.MapPath(filePath)))
                {
                    lbUnduhDokMitraCalonPengguna.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    ViewState["dokumen_mitra_blmdiunggah"] = 1;
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
                string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf", ViewState["thn_usulan"].ToString(), id_mitra);
                LinkButton lbUnduhDokMitraInvestorPenelitian = (LinkButton)e.Row.FindControl("lbUnduhDokMitraInvestorPenelitian");
                if (File.Exists(Server.MapPath(filePath)))
                {
                    lbUnduhDokMitraInvestorPenelitian.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    ViewState["dokumen_mitra_blmdiunggah"] = 1;
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
                string dirFile = "~/fileUpload/mitra/" + ViewState["thn_usulan"].ToString() + "/";
                string filePath = string.Format(dirFile + "{0}.pdf", idMitra);

                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = dirFile,//PATH_UNGGAH_BERKAS,
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
                string dirFile = "~/fileUpload/mitra/" + ViewState["thn_usulan"].ToString() + "/";
                string filePath = string.Format(dirFile + "{0}.pdf", idMitra);

                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = dirFile,//PATH_UNGGAH_BERKAS,
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
                string dirFile = "~/fileUpload/mitra/" + ViewState["thn_usulan"].ToString() + "/";
                string filePath = string.Format(dirFile + "{0}.pdf", idMitra);
                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = dirFile, //PATH_UNGGAH_BERKAS,
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

        protected void rptLuaranWajib_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Label lblThnKe = (Label)e.Item.FindControl("lblThnKe");
            if (lblThnKe == null) return;

            int tktTarget = int.Parse(ViewState["tktTarget"].ToString());
            int jml_luaran_wajib = int.Parse(ViewState["jml_luaran_wajib"].ToString());
            int lamaKegiatan = int.Parse(ViewState["lamaKegiatan"].ToString());
            if (tktTarget > 3 && jml_luaran_wajib == 1 && lamaKegiatan > 1) // pt pp 
            {
                lblThnKe.Text = "1 sd " + lamaKegiatan.ToString();
            }

        }
    }


}