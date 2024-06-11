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

namespace simlitekkes.UserControls.Pengusul
{
    public partial class rekapUsulanAbdimas : System.Web.UI.UserControl
    {

        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        UIControllers.uiDropdownList obj_uiDDL = new UIControllers.uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        Models.Sistem.referensiData objRefData = new Models.Sistem.referensiData();
        Models.Pengusul.persyaratanUmum objPersyaratan = new Models.Pengusul.persyaratanUmum();
        Models.Pengusul.persyaratanUmumAbdimas objPersyaratanAbdimas = new Models.Pengusul.persyaratanUmumAbdimas();
        Models.Pengusul.Pengusul oPengusul = new Models.Pengusul.Pengusul();

        Models.login objLogin = new Models.login();
        kontrolUnggah ktUnggah = new kontrolUnggah();
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        Models.Pengusul.berandaPengusul objPengusul = new Models.Pengusul.berandaPengusul();
        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();

        pdfUsulanBaru objMdlPdfusulanBaru = new pdfUsulanBaru();
        uiNotify noty = new uiNotify();

        Models.Pengusul.luaran objLuaran = new Models.Pengusul.luaran();
        RAB objModel = new Models.Pengusul.RAB();
        Models.Pengusul.mitraAbdimas objMitraAbdimas = new Models.Pengusul.mitraAbdimas();
        //int jmlKelengkapan = 0;
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
                //tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString()), // test
                idBidangFokus = int.Parse(dt.Rows[0]["id_bidang_fokus"].ToString()),
                bidangFokus = dt.Rows[0]["bidang_fokus"].ToString(),
                //idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString())
                thnPertamaUsulan = dt.Rows[0]["thn_pertama_usulan"].ToString()
            };
            ViewState["thn_usulan"] = objUsulanKegiatan.thnUsulan;
            ViewState["id_usulan"] = objUsulanKegiatan.idUsulan;
            ViewState["lama_kegiatan"] = objUsulanKegiatan.lamaKegiatan;
            ViewState["idSkema"] = objUsulanKegiatan.idSkema;


            lblInfoAtUnggahDokUsulan.Text = String.Format("{0} (tahun ke-{1} dari {2} tahun)",
                objUsulanKegiatan.namaSkema, objUsulanKegiatan.urutanTahunUsulanKegiatan,
                objUsulanKegiatan.lamaKegiatan);
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
            string dirFile = "~/fileUpload/dokumenUsulan/" + objUsulanKegiatan.thnUsulan;

            string path2save = String.Format(dirFile + "/{0}.pdf", objUsulanKegiatan.idUsulanKegiatan);
            if (!cekFileExists(path2save))
            {
                pnlKekDokumenUsulan.Visible = true;
                enableKirimUsulan = false;
            }

            if (retVal == 0)
            {
                pnlKekAnggota.Visible = true;
                enableKirimUsulan = false;
            }

            if (ViewState["sts_persetujuan_anggota"].ToString() != "1")
            {
                pnlKekAnggota.Visible = true;
                enableKirimUsulan = false;
            }

            retVal = isiRab(objUsulanKegiatan);

            if (retVal == 0)
            {
                pnlKekRab.Visible = true;
                enableKirimUsulan = false;
            }


            retVal = isiLuaran(objUsulanKegiatan.idUsulan);
            if (retVal == 0)
            {
                if (ViewState["idSkema"].ToString() == "56" || ViewState["idSkema"].ToString() == "57")
                {

                }
                else
                {
                    pnlKekLuaran.Visible = true;
                    enableKirimUsulan = false;
                }

            }



            retVal = isiMitra(idUsulanKegiatan, objUsulanKegiatan.idSkema);
            if (retVal == 0)
            {
                pnlKekMitra.Visible = true;
                enableKirimUsulan = false;
            }

            return enableKirimUsulan;
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

        protected void lbUnduhPdfDokumenLengkap_Click(object sender, EventArgs e)
        {
            pdfUsulanLengkap.UnduhProposalLengkap(Session["id_usulan_kegiatan_unduh"].ToString());
            Session.Remove("id_usulan_kegiatan_unduh");
        }

        protected void lbUnduhPdfDok_Click(object sender, EventArgs e)
        {
            string path = String.Format("fileUpload/dokumenUsulan/{0}/{1}.pdf", ViewState["thn_usulan"].ToString(), ViewState["id_usulan_kegiatan"].ToString());
            string path2 = String.Format("fileUpload/DokumenUsulan/{0}/{1}.pdf", ViewState["thn_usulan"].ToString(), ViewState["id_usulan_kegiatan"].ToString());

            if (File.Exists(Server.MapPath(path)))
            {
                string PATH_UNGGAH_BERKAS = "../fileUpload/DokumenUsulan/" + ViewState["thn_usulan"].ToString();
                string namaBerkas = "dokumenUsulan.pdf";
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
                string PATH_UNGGAH_BERKAS = "../fileUpload/DokumenUsulan/" + ViewState["thn_usulan"].ToString();

                string namaBerkas = "dokumenUsulan.pdf";
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
            return cekJumlahAnggotaMemenuhi(p_idUsulanKegiatan, dtAnggota.Rows.Count + dtAnggotaTendik.Rows.Count + jmlNonDikti);
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

        //private int isiAnggotaNonDikti(string p_idUsulanKegiatan)
        //{
        //    DataTable dtAnggota = new DataTable();
        //    if (objAnggota.listAnggotaNonDikti(ref dtAnggota, Guid.Parse(p_idUsulanKegiatan)))
        //    {
        //        rptAnggota_nonDikti.DataSource = dtAnggota;
        //        rptAnggota_nonDikti.DataBind();
        //        lblJmlAnggotaNonDikti.Text = dtAnggota.Rows.Count.ToString();
        //    };

        //    return dtAnggota.Rows.Count;
        //}

        private int isiLuaran(string pidUsulan)
        {
            //DataTable dt = new DataTable();
            //objMdlPdfusulanBaru.GetLuaranWajib(ref dt, Guid.Parse(pidUsulanKegiatan));
            //rptLuaranWajib.DataSource = dt;
            //rptLuaranWajib.DataBind();

            objLogin = (Models.login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_kegiatan = ViewState["thn_usulan"].ToString();// DateTime.Now.Year.ToString();
            string kd_jenis_kegiatan = "2";

            int kdStatusMemenuhi = 1;
            DataTable dt = new DataTable();
            int idKelompok = 1;
            if (objLuaran.listLuaranPengabdian(ref dt, Guid.Parse(pidUsulan), idKelompok))

                //if (objLuaran.ListTargetLuaranWajibPengabdian(ref dt, Guid.Parse(pidUsulan)))
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    string nama_target_capaian_luaran = dt.Rows[a]["nama_target_capaian_luaran"].ToString();
                    if (nama_target_capaian_luaran == "")
                    {
                        kdStatusMemenuhi=0;
                    }
                }
                //rptLuaranWajib.DataSource = dt;
                //rptLuaranWajib.DataBind();
                gvluaranwajib.DataSource = dt;
                gvluaranwajib.DataBind();
                gvluaranwajib_DataBound();
            }

            //for(int a=0; a< dt.Rows.Count; a++)
            //{
            //    if(dt.Rows[a]["id_luaran_dijanjikan"].ToString() != "")
            //    {
            //    }
            //}

            DataTable dtt = new DataTable();
            //if (objLuaran.ListTargetLuaranTambahanPengabdian(ref dtt, Guid.Parse(pidUsulan)))
            //{
            //    for (int a = 0; a < dtt.Rows.Count; a++)
            //    {
            //        string nama_target_capaian_luaran = dtt.Rows[a]["nama_target_capaian_luaran"].ToString();
            //        if (nama_target_capaian_luaran == "")
            //        {
            //            DataRow dr = dtt.Rows[a];
            //            dr.Delete();
            //        }
            //    }
            //    rptLuaranTambahan.DataSource = dtt;
            //    rptLuaranTambahan.DataBind();
            //}

            objLuaran.ListTargetLuaranTambahanPengabdian2019(ref dtt, Guid.Parse(pidUsulan));

            //rptLuaranTambahan.DataSource = dtt;
            //rptLuaranTambahan.DataBind();

            gvluarantambahan.DataSource = dtt;
            gvluarantambahan.DataBind();

            return kdStatusMemenuhi;
        }

        protected void gvluaranwajib_DataBound()
        {
            for (int i = gvluaranwajib.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = gvluaranwajib.Rows[i];
                GridViewRow previousRow = gvluaranwajib.Rows[i - 1];
                int nkol = 1;
                for (int j = 0; j < nkol; j++)
                {
                    string txt2 = ((Label)row.Cells[j].FindControl("lblTahunke")).Text;
                    string txt1 = ((Label)previousRow.Cells[j].FindControl("lblTahunke")).Text;
                    //run this loop for the column which you thing the data will be similar
                    if (((Label)row.Cells[j].FindControl("lblTahunke")).Text == ((Label)previousRow.Cells[j].FindControl("lblTahunke")).Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }

        protected void gvluarantambahan_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string tahun_ke = gvluarantambahan.DataKeys[e.Row.RowIndex]["tahun_ke"].ToString();
                string id_luaran_dijanjikan = gvluarantambahan.DataKeys[e.Row.RowIndex]["id_luaran_dijanjikan"].ToString();
                string nama_target_capaian_luaran = gvluarantambahan.DataKeys[e.Row.RowIndex]["nama_target_capaian_luaran"].ToString();
                string id_jenis_luaran = gvluarantambahan.DataKeys[e.Row.RowIndex]["id_jenis_luaran"].ToString();
                //    LinkButton lbUnggahLaporanakhir = new LinkButton();
                //    lbUnggahLaporanakhir = (LinkButton)e.Row.FindControl("lbUnggahLaporanakhir");

                Label lblTahunke = new Label();
                //LinkButton lbTambah = new LinkButton();
                //LinkButton lbHapus = new LinkButton();
                //LinkButton lbEdit = new LinkButton();
                Label lbltargetluaran = new Label();
                Label lblnamaluaran = new Label();
                Label lblketerangan = new Label();
                lblTahunke = (Label)e.Row.FindControl("lblTahunke");
                //lbTambah = (LinkButton)e.Row.FindControl("lbTambah");
                //lbHapus = (LinkButton)e.Row.FindControl("lbHapus");
                //lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbltargetluaran = (Label)e.Row.FindControl("lbltargetluaran");
                lblnamaluaran = (Label)e.Row.FindControl("lblnamaluaran");
                lblketerangan = (Label)e.Row.FindControl("lblketerangan");

                //lbTambah.Visible = false;
                //lbHapus.Visible = false;
                //lbEdit.Visible = false;
                string keterangan = lblketerangan.Text.Replace("{", "").Replace("}", "").Replace("\"", "");
                keterangan = "- " + keterangan.Replace(",", "<br>- ");
                if (keterangan.Contains("NULL"))
                {
                    lblketerangan.Text = "";
                    //lbTambah.Visible = true;
                }
                else
                {
                    lblketerangan.Text = keterangan;
                    //lbHapus.Visible = true;
                    //lbEdit.Visible = false;
                    //lbTambah.Visible = true;
                }

                if (lbltargetluaran != null)
                {
                    if (nama_target_capaian_luaran != "")
                    {
                        lbltargetluaran.Text = "(" + nama_target_capaian_luaran + ")";
                    }
                    else
                    {
                        lbltargetluaran.Text = "-";
                    }
                };
            }
        }


        protected void gvluaranwajib_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string tahun_ke = gvluaranwajib.DataKeys[e.Row.RowIndex]["tahun_ke"].ToString();
                string id_luaran_dijanjikan = gvluaranwajib.DataKeys[e.Row.RowIndex]["id_luaran_dijanjikan"].ToString();
                string nama_target_capaian_luaran = gvluaranwajib.DataKeys[e.Row.RowIndex]["nama_target_capaian_luaran"].ToString();
                string id_jenis_luaran = gvluaranwajib.DataKeys[e.Row.RowIndex]["id_jenis_luaran"].ToString();
                string arr_nama_kategori_jenis_luaran = gvluaranwajib.DataKeys[e.Row.RowIndex]["arr_nama_kategori_jenis_luaran"].ToString();
                //    LinkButton lbUnggahLaporanakhir = new LinkButton();
                //    lbUnggahLaporanakhir = (LinkButton)e.Row.FindControl("lbUnggahLaporanakhir");

                Label lblTahunke = new Label();
                //LinkButton lbTambah = new LinkButton();
                //LinkButton lbHapus = new LinkButton();
                //LinkButton lbEdit = new LinkButton();
                Label lbltargetluaran = new Label();
                Label lblnamaluaran = new Label();
                Label lblketerangan = new Label();
                lblTahunke = (Label)e.Row.FindControl("lblTahunke");
                //lbTambah = (LinkButton)e.Row.FindControl("lbTambah");
                //lbHapus = (LinkButton)e.Row.FindControl("lbHapus");
                //lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbltargetluaran = (Label)e.Row.FindControl("lbltargetluaran");
                lblnamaluaran = (Label)e.Row.FindControl("lblnamaluaran");
                lblketerangan = (Label)e.Row.FindControl("lblketerangan");

                //lbTambah.Visible = false;
                //lbHapus.Visible = false;
                //lbEdit.Visible = false;

                if (lblnamaluaran.Text == "")
                {
                    string str_nama_kategori_jenis_luaran = arr_nama_kategori_jenis_luaran.Replace("{", "").Replace("}", "").Replace("\"", "");
                    string[] arr = str_nama_kategori_jenis_luaran.Split(new Char[] { ',' });

                    if (arr.Length > 0)
                        lblnamaluaran.Text = " - " + arr[0];

                    if (arr.Length > 1)
                        lblnamaluaran.Text += ", ... ";
                }

                string keterangan = lblketerangan.Text.Replace("{", "").Replace("}", "").Replace("\"", "");
                keterangan = "- " + keterangan.Replace(",", "<br>- ");
                //if (lbltargetluaran.Text == "" || lbltargetluaran.Text == "-")
                //{
                //    lbTambah.Visible = true;
                //}
                //else
                //{
                //    lbHapus.Visible = true;
                //    lbEdit.Visible = true;
                //}

                if (lbltargetluaran != null)
                {
                    if (nama_target_capaian_luaran != "")
                    {
                        lbltargetluaran.Text = "(" + nama_target_capaian_luaran + ")";
                    }
                    else
                    {
                        lbltargetluaran.Text = "-";
                    }
                };
            }
        }

        private int isiRab(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
            int retval = 1;
            var dt = new DataTable();
            if (objModel.GetKelompokBiaya(ref dt, "2"))
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
                        }
                    }
                }
                dtPertHn.Clear();
            }
            lblTotalDana.Text = "Total RAB " + usulanKegiatan.lamaKegiatan.ToString() + " Tahun Rp. " + totalRab.ToString("N0");


            int kdStatusMemenuhi = 1;
            dt.Clear();
            dt = new DataTable();
            var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
            objModel.GetRentangDanaSkemaKegiatan(ref dt, int.Parse(ViewState["idSkema"].ToString()));
            
            decimal totalMinimal = 0, totalMaksimal = 0;
            if (dt.Rows.Count > 0)
            {
                totalMinimal = decimal.Parse(dt.Rows[0]["dana_minimal_thn_berjalan"].ToString());
                totalMaksimal = decimal.Parse(dt.Rows[0]["dana_maksimal_thn_berjalan"].ToString());
                lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal_thn_berjalan"].ToString()).ToString("N0");
                lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal_thn_berjalan"].ToString()).ToString("N0");
            }
            
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

            return kdStatusMemenuhi;
        }

        //rab objModel = new Models.Pengusul.rab();
        //public int InitRAB(Models.sistem.usulanKegiatan usulanKegiatan)
        //{
        //    int retVal = 0;
        //    DataTable dt = new DataTable();
        //    if (objModel.GetRABUsulan(ref dt, Guid.Parse(usulanKegiatan.idUsulanKegiatan)))
        //    {
        //        var listAnggaran = new List<ItemRencanaAnggaran>();

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            var itemAnggaran = new ItemRencanaAnggaran();
        //            itemAnggaran.IdRABUsulan = Guid.Parse(dt.Rows[i]["id_rab_usulan"].ToString());
        //            itemAnggaran.KodeJenisPembelanjaan = dt.Rows[i]["kd_jenis_pembelanjaan"].ToString();
        //            itemAnggaran.JenisPembelanjaan = dt.Rows[i]["jenis_pembelanjaan"].ToString();
        //            itemAnggaran.Item = dt.Rows[i]["nama_item"].ToString();
        //            itemAnggaran.Satuan = dt.Rows[i]["xsatuan"].ToString();
        //            itemAnggaran.Volume = decimal.Parse(dt.Rows[i]["xvolume"].ToString());
        //            itemAnggaran.Honor = decimal.Parse(dt.Rows[i]["harga_satuan"].ToString());
        //            itemAnggaran.Total = decimal.Parse(dt.Rows[i]["xvolume"].ToString()) *
        //                                 decimal.Parse(dt.Rows[i]["harga_satuan"].ToString());
        //            itemAnggaran.TahunKegiatan = int.Parse(dt.Rows[i]["urutan_thn_usulan_kegiatan"].ToString());

        //            listAnggaran.Add(itemAnggaran);
        //        }
        //        if (listAnggaran.Count > 0)
        //            retVal = isiRAB(listAnggaran);
        //        else
        //        {
        //            objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
        //            objModel.GetRentangDanaSkemaKegiatan(ref dt, objUsulanKegiatan.idSkema);
        //            if (dt.Rows.Count > 0)
        //            {
        //                lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal_thn_berjalan"].ToString()).ToString("N0");
        //                lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal_thn_berjalan"].ToString()).ToString("N0");
        //            }
        //        }

        //        dt.Clear();
        //        //objModel.GetBidangFokus(ref dt, Guid.Parse(usulanKegiatan.idUsulan));
        //        //if (dt.Rows.Count > 0) lblBidangFokus.Text = dt.Rows[0]["bidang_fokus"].ToString();

        //    }
        //    //else
        //    //{
        //    //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
        //    //                objModel.errorMessage);
        //    //}

        //    ViewState["IdUsulan"] = usulanKegiatan.idUsulan;



        //    return retVal;
        //}
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
        //    //objIdentitasUsulan.getPlafonDanaSbkThnImplementasi2018(ref dt,
        //    //    objUsulanKegiatan.idKategoriSBK, objUsulanKegiatan.idSkema, objUsulanKegiatan.idBidangFokus);


        //    objModel.GetRentangDanaSkemaKegiatan(ref dt, objUsulanKegiatan.idSkema);
        //    decimal totalMinimal = 0, totalMaksimal = 0;
        //    if (dt.Rows.Count > 0)
        //    {
        //        totalMinimal = decimal.Parse(dt.Rows[0]["dana_minimal_thn_berjalan"].ToString());
        //        totalMaksimal = decimal.Parse(dt.Rows[0]["dana_maksimal_thn_berjalan"].ToString());
        //        lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal_thn_berjalan"].ToString()).ToString("N0");
        //        lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal_thn_berjalan"].ToString()).ToString("N0");
        //    }

        //    //lblBidFokus.Text = objUsulanKegiatan.bidangFokus;
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
        //        }
        //        if (totalThn3 < totalMinimal || totalThn3 > totalMaksimal)
        //        {
        //            kdStatusMemenuhi = 0;
        //        }
        //    }

        //    return kdStatusMemenuhi;
        //}

        protected void gvMitraPelaksanaPenelitian_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvMitraPelaksanaPenelitian_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        Models.Pengusul.mitraPenelitian objMitra = new Models.Pengusul.mitraPenelitian();
        public int isiMitra(string idUsulanKegiatan, int pIdSkema)
        {
            int kdStatusMemenuhi = 1;


            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;

            DataTable dtMitra = new DataTable();
            DataTable dtKelmas = new DataTable();

            if (pIdSkema == 55) // ppdm 
            {
                if (objMitraAbdimas.listMitraPelaksanaPpdm(ref dtMitra, Guid.Parse(idUsulanKegiatan)))
                {
                    gvMitraAbdimas.DataSource = dtMitra;
                    gvMitraAbdimas.DataBind();
                };
                if (objMitraAbdimas.getMitraKelompokMasyibdm(ref dtKelmas, idUsulanKegiatan))
                {
                    gvKelMasyarakat.DataSource = dtKelmas;
                    gvKelMasyarakat.DataBind();
                };
                gvKelMasyarakatPpmUpt.Visible = false;
            }
            else if (pIdSkema == 23) //  ppmupt
            {
                if (objMitraAbdimas.listMitraPelaksanaPpdm(ref dtMitra, Guid.Parse(idUsulanKegiatan)))
                {
                    gvMitraAbdimas.DataSource = dtMitra;
                    gvMitraAbdimas.DataBind();
                };

                if (objMitraAbdimas.listMitraSasaranPPMUPT(ref dtKelmas, Guid.Parse(idUsulanKegiatan)))
                {
                    gvKelMasyarakatPpmUpt.DataSource = dtKelmas;
                    gvKelMasyarakatPpmUpt.DataBind();
                };
                gvKelMasyarakat.Visible = false;

            }
            else
            {
                if (objMitraAbdimas.listMitraPelaksanaPengabdian(ref dtMitra, Guid.Parse(idUsulanKegiatan)))
                {
                    gvMitraAbdimas.DataSource = dtMitra;
                    gvMitraAbdimas.DataBind();
                };
                gvKelMasyarakat.Visible = false;
                gvKelMasyarakatPpmUpt.Visible = false;
            }

            dtMitra.Clear();
            oPengusul.listMitraAbdimasUnnest(ref dtMitra, idUsulanKegiatan);

            DataTable dtSyarat = new DataTable();
            oPengusul.listSyaratMitraAbdimas(ref dtSyarat, idUsulanKegiatan);

            // Cek Minimal jumlah mitra

            bool isValid = true;
            int jmlTdkValid = 0;
            string info = "", lstInfo = "";
            cekMinMitraWajib(dtSyarat, dtMitra, ref isValid, ref info);

            if (!isValid)
            {
                lstInfo += info + "<br />";
                jmlTdkValid++;
            }
            isValid = true;
            info = "";

            //if (pIdSkema != 15) // mitra PPK tdk wajib 
            //{

            //    // query beda
            //    DataTable dtKelmas2 = new DataTable();
            //    objMitraAbdimas.listMitraPelaksanaPengabdian(ref dtKelmas2, Guid.Parse(idUsulanKegiatan));

            //    cekMinMitraWajib(dtSyarat, dtKelmas2, ref isValid, ref info);

            //    if (!isValid)
            //    {
            //        lstInfo += info + "<br />";
            //        jmlTdkValid++;
            //    }
            //    isValid = true;
            //    info = "";


            //    cekDukunganPendanaanPerthun(dtSyarat, dtKelmas2, ref isValid, ref info);

            //    if (!isValid)
            //    {
            //        lstInfo += info + "<br />";
            //        jmlTdkValid++;
            //    }
            //    isValid = true;
            //    info = "";

            //    //cekKdStsSuratPendanaan(dtSyarat, dtKelmas2, ref isValid, ref info);
            //    //if (!isValid)
            //    //{
            //    //    lstInfo += info + "<br />";
            //    //    jmlTdkValid++;
            //    //}
            //    isValid = true;
            //    info = "";

            //    ViewState["first_isi_kelmas"] = true;
            //}

            cekDukunganPendanaanPerthun(dtSyarat, dtMitra, ref isValid, ref info);

            if (!isValid)
            {
                lstInfo += info + "<br />";
                jmlTdkValid++;
            }
            isValid = true;
            info = "";

            cekKdStsSuratPendanaan(dtSyarat, dtMitra, ref isValid, ref info);
            if (!isValid)
            {
                lstInfo += info + "<br />";
                jmlTdkValid++;
            }
            isValid = true;
            info = "";
            if (jmlTdkValid > 0)
            {
                pnlKekMitra.Visible = true;
                lblKekMitra.Text = lstInfo;
                kdStatusMemenuhi = 0;
            }

            return kdStatusMemenuhi;
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
        protected void gvMitraCalonPengguna_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMitraInvestor_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        /*
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

            //if (ViewState["mitraPelaksana"].ToString() == "1")
            //{
            //    panelMitraPelaksana.Visible = true;
            //}
            //else
            //{
            //    panelMitraPelaksana.Visible = false;
            //}

            //if (ViewState["mitraCalonPengguna"].ToString() == "1")
            //{
            //    panelMitraCalonPengguna.Visible = true;
            //}
            //else
            //{
            //    panelMitraCalonPengguna.Visible = false;
            //}

            //if (ViewState["mitraInvestor"].ToString() == "1")
            //{
            //    panelMitraInvestor.Visible = true;
            //}
            //else
            //{
            //    panelMitraInvestor.Visible = false;
            //}

            //if (ViewState["mitraInvestor"].ToString() != "1" && ViewState["mitraCalonPengguna"].ToString() != "1" && ViewState["mitraInvestor"].ToString() != "1")
            //{
            //    lblInfoMitra.Visible = true;
            //}

        }
        */

        protected void gvMitraAbdimas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string id_mitra_abdimas = gvMitraAbdimas.DataKeys[e.Row.RowIndex]["id_mitra_abdimas"].ToString();

                //string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf",
                //    ViewState["thn_usulan"].ToString(), id_mitra_abdimas);

                LinkButton lbUnduhIcon = (LinkButton)e.Row.FindControl("lbUnduhIcon");
                if (lbUnduhIcon == null)
                    return;
                string kd_sts_surat_pernyataan = gvMitraAbdimas.DataKeys[e.Row.RowIndex]["kd_sts_surat_pernyataan"].ToString();
                if (kd_sts_surat_pernyataan == "1")
                {
                    lbUnduhIcon.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbUnduhIcon.ForeColor = System.Drawing.Color.Gray;
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

        protected void gvMitraAbdimas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraAbdimas.DataKeys[rowIndex]["id_mitra_abdimas"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unduhDokumenMitra")
            {
                string namaMitra = gvMitraAbdimas.DataKeys[rowIndex]["nama_pimpinan_mitra"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "Mitra_" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string filePath = string.Format("~/fileUpload/mitra/{0}/{1}.pdf",
                     ViewState["thn_usulan"].ToString(), idMitra);
                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = "~/fileUpload/mitra/" + ViewState["thn_usulan"].ToString(),//PATH_UNGGAH_BERKAS,
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

        private void cekDukunganPendanaanPerthun(DataTable dtSyarat, DataTable dtMitra, ref bool isValid, ref string info)
        {
            if (dtMitra.Columns["dana_thn_1"] == null)
            {
                dtMitra.Columns["dana_tahun_1"].ColumnName = "dana_thn_1";
                dtMitra.Columns["dana_tahun_2"].ColumnName = "dana_thn_2";
                dtMitra.Columns["dana_tahun_3"].ColumnName = "dana_thn_3";
                dtMitra.AcceptChanges();
            }

            for (int a = 0; a < dtSyarat.Rows.Count; a++)
            {
                for (int b = 0; b < dtMitra.Rows.Count; b++)
                {
                    string tipeSyarat = dtSyarat.Rows[a]["id_tipe_mitra"].ToString();
                    string tipeMitra = dtMitra.Rows[b]["id_tipe_mitra"].ToString();
                    if (tipeSyarat == tipeMitra)
                    {
                        string urutanTahun = dtSyarat.Rows[a]["thn_uruan_kegiatan"].ToString();
                        if (urutanTahun == "1")
                        {
                            string strjmlMinDanaMitra = dtSyarat.Rows[a]["dukungan_pendanaan"].ToString();
                            strjmlMinDanaMitra = strjmlMinDanaMitra.Replace(",00", "").Replace(".00", "");

                            int jmlMinDanaMitra = int.Parse(strjmlMinDanaMitra);

                            string dana1 = dtMitra.Rows[b]["dana_thn_1"].ToString();
                            dana1 = dana1.Replace(",00", "").Replace(".00", "");
                            if (dana1 == "")
                                dana1 = "0";
                            int jmlDanaMitra = int.Parse(dana1);
                            if (jmlDanaMitra < jmlMinDanaMitra)
                            {
                                isValid = false;
                                info = "- Pendanaan minimal dari mitra " + dtSyarat.Rows[a]["tipe_mitra"].ToString()
                                        + " tahun ke-" + urutanTahun + " Rp. " + objManipData.convertFormatDana(jmlMinDanaMitra.ToString(), ".");
                                return;
                            }
                            else
                            {
                                //isValid = true;
                            }
                        }
                        else if (urutanTahun == "2")
                        {
                            string strjmlMinDanaMitra = dtSyarat.Rows[a]["dukungan_pendanaan"].ToString();
                            strjmlMinDanaMitra = strjmlMinDanaMitra.Replace(",00", "").Replace(".00", "");

                            int jmlMinDanaMitra = int.Parse(strjmlMinDanaMitra);

                            string dana2 = dtMitra.Rows[b]["dana_thn_2"].ToString();
                            dana2 = dana2.Replace(",00", "").Replace(".00", "");
                            if (dana2 == "")
                                dana2 = "0";
                            int jmlDanaMitra = int.Parse(dana2);
                            if (jmlDanaMitra < jmlMinDanaMitra)
                            {
                                isValid = false;
                                info = "- Pendanaan minimal dari mitra " + dtSyarat.Rows[a]["tipe_mitra"].ToString()
                                        + " tahun ke-" + urutanTahun + " Rp. " + objManipData.convertFormatDana(jmlMinDanaMitra.ToString(), ".");
                                return;
                            }
                            else
                            {
                                //isValid = true;
                            }
                        }
                        else if (urutanTahun == "3")
                        {
                            string strjmlMinDanaMitra = dtSyarat.Rows[a]["dukungan_pendanaan"].ToString();
                            strjmlMinDanaMitra = strjmlMinDanaMitra.Replace(",00", "").Replace(".00", "");

                            int jmlMinDanaMitra = int.Parse(strjmlMinDanaMitra);

                            string dana3 = dtMitra.Rows[b]["dana_thn_3"].ToString();
                            dana3 = dana3.Replace(",00", "").Replace(".00", "");
                            if (dana3 == "")
                                dana3 = "0";
                            int jmlDanaMitra = int.Parse(dana3);
                            if (jmlDanaMitra < jmlMinDanaMitra)
                            {
                                isValid = false;
                                strjmlMinDanaMitra = decimal.Parse(strjmlMinDanaMitra).ToString("N0");
                                info = "- Pendanaan minimal dari mitra " + dtSyarat.Rows[a]["tipe_mitra"].ToString()
                                        + " tahun ke-" + urutanTahun + " Rp. " + objManipData.convertFormatDana(jmlMinDanaMitra.ToString(), ".");
                                return;
                            }
                            else
                            {
                                //isValid = true;
                            }
                        }

                    }
                }
            }
        }

        private void cekMinMitraWajib(DataTable dtSyarat, DataTable dtMitra, ref bool isValid, ref string info)
        {
            for (int a = 0; a < dtSyarat.Rows.Count; a++)
            {
                if (dtMitra.Rows.Count <= 0)
                {
                    int jmlMinMitra = int.Parse(dtSyarat.Rows[a]["jml_min_mitra"].ToString());
                    if (jmlMinMitra > 0)
                    {
                        lblTdkWajibMitra.Text = dtSyarat.Rows[a]["tipe_mitra"].ToString() + " wajib ada";
                        isValid = false;
                        info = "- Jumlah minimal mitra wajib (" + dtSyarat.Rows[a]["tipe_mitra"].ToString() + "): " + jmlMinMitra.ToString();
                        return;
                    }
                }
                string tipeSyarat = dtSyarat.Rows[a]["id_tipe_mitra"].ToString();

                for (int b = 0; b < dtMitra.Rows.Count; b++)
                {
                    //string tipeSyarat = dtSyarat.Rows[a]["id_tipe_mitra"].ToString();
                    string tipeMitra = dtMitra.Rows[b]["id_tipe_mitra"].ToString();
                    if (tipeSyarat == tipeMitra)
                    {
                        int jmlMitraPerTipe = dtMitra.Select("id_tipe_mitra=" + tipeMitra).Count();
                        //dtMitra.Select("id_tipe_mitra=" + tipeMitra).
                        int jmlMinMitra = int.Parse(dtSyarat.Rows[a]["jml_min_mitra"].ToString());

                        if (jmlMinMitra > 0)
                        {
                            lblTdkWajibMitra.Text = dtSyarat.Rows[a]["tipe_mitra"].ToString() + " wajib ada";
                        }

                        //if (jmlMitraPerTipe < jmlMinMitra)
                        //{
                        //    isValid = false;
                        //    info = "- Jumlah minimal mitra wajib (" + dtSyarat.Rows[a]["tipe_mitra"].ToString() + "): " + jmlMinMitra.ToString();
                        //    return;
                        //}

                        // Minimal mitra wajib semua tipe
                        if (dtMitra.Rows.Count < jmlMinMitra)
                        {
                            isValid = false;
                            info = "- Jumlah minimal mitra wajib: " + jmlMinMitra.ToString();
                            return;
                        }


                        //    if (dtMitra.Rows.Count < jmlMinMitra)
                        //{
                        //    isValid = false;
                        //    info = "- Jumlah minimal mitra wajib: " + jmlMinMitra.ToString();
                        //    return;
                        //}


                    }

                    // Minimal mitra wajib by type jika jml minimal terpenuhi
                    string kdStsSyarat = dtMitra.Rows[b]["kd_sts_surat_pernyataan"].ToString();
                    if (kdStsSyarat == "1")
                    {
                        int jmlMitraByTipe = dtMitra.Select("id_tipe_mitra=" + tipeMitra).Count();
                        if (jmlMitraByTipe <= 0)
                        {
                            isValid = false;
                            info = "- Mitra " + dtSyarat.Rows[a]["tipe_mitra"].ToString() + " wajib ada ";
                            return;
                        }
                    }

                }
            }
        }

        private void cekKdStsSuratPendanaan(DataTable dtSyarat, DataTable dtMitra, ref bool isValid, ref string info)
        {
            string[] columnNames = dtMitra.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
            string thnUsulan = ViewState["thn_usulan"].ToString(); // DateTime.Now.Year.ToString();
            for (int a = 0; a < dtSyarat.Rows.Count; a++)
            {
                for (int b = 0; b < dtMitra.Rows.Count; b++)
                {
                    string tipeSyarat = dtSyarat.Rows[a]["id_tipe_mitra"].ToString();
                    string tipeMitra = dtMitra.Rows[b]["id_tipe_mitra"].ToString();
                    if (tipeSyarat == tipeMitra)
                    {
                        string kdStsSyarat = dtMitra.Rows[b]["kd_sts_surat_pernyataan"].ToString();
                        if (kdStsSyarat == "1")
                        {
                            string filePath = "~/fileUpload/Mitra/{0}/{1}.pdf";
                            filePath = string.Format(filePath, thnUsulan, dtMitra.Rows[b]["id_mitra_abdimas"].ToString());
                        }
                        else
                        {
                            int jmlMinMitra = int.Parse(dtSyarat.Rows[a]["jml_min_mitra"].ToString());
                            if (jmlMinMitra > 0)
                            {
                                info = "- Surat pernyataan mitra " + dtSyarat.Rows[a]["tipe_mitra"].ToString() + " belum diunggah.";
                                isValid = false;
                                return;
                            }
                            else
                            {
                                isValid = true;
                                return;
                            }
                        }
                    }
                }
            }
        }

        protected void gvKelMasyarakat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ViewState["first_isi_kelmas"] = true;
                if (bool.Parse(ViewState["first_isi_kelmas"].ToString()))
                {
                    ViewState["first_isi_kelmas"] = false;

                    LinkButton lbUnduh = (LinkButton)e.Row.FindControl("lbUnduh");
                    LinkButton lbUnduh11 = (LinkButton)e.Row.FindControl("lbUnduh11");
                    LinkButton lbUnduh12 = (LinkButton)e.Row.FindControl("lbUnduh12");
                    LinkButton lbUnduh21 = (LinkButton)e.Row.FindControl("lbUnduh21");
                    LinkButton lbUnduh22 = (LinkButton)e.Row.FindControl("lbUnduh22");
                    LinkButton lbUnduh31 = (LinkButton)e.Row.FindControl("lbUnduh31");
                    LinkButton lbUnduh32 = (LinkButton)e.Row.FindControl("lbUnduh32");
                    LinkButton[] arrLb = { lbUnduh11, lbUnduh12, lbUnduh21, lbUnduh22, lbUnduh31, lbUnduh32 };
                    DataTable dtKelmas2 = new DataTable();
                    objMitraAbdimas.listMitraPelaksanaPengabdian(ref dtKelmas2, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
                    int b = 0;
                    for (int a = 0; a < dtKelmas2.Rows.Count; a++)
                    {
                        if (dtKelmas2.Rows[a]["kd_kategori_mitra"].ToString() == "5") //5 sasaran;
                        {

                            if (dtKelmas2.Rows[a]["kd_desa"].ToString() != "")
                            {

                                string sts_unggah_pernyataan = dtKelmas2.Rows[a]["sts_unggah_pernyataan"].ToString();
                                if (sts_unggah_pernyataan == "Sudah unggah")
                                {
                                    lbUnduh.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    lbUnduh.ForeColor = System.Drawing.Color.Gray;
                                }
                            }
                            else
                            {
                                string sts_unggah_pernyataan = dtKelmas2.Rows[a]["sts_unggah_pernyataan"].ToString();
                                if (sts_unggah_pernyataan == "Sudah unggah")
                                {
                                    arrLb[b].ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    arrLb[b].ForeColor = System.Drawing.Color.Gray;
                                }
                                b++;
                            }
                        }
                    }



                }


            }
        }

        protected void gvKelMasyarakatPpmUpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ViewState["first_isi_kelmas"] = true;
                if (bool.Parse(ViewState["first_isi_kelmas"].ToString()))
                {
                    ViewState["first_isi_kelmas"] = false;
                    // gvKelMasyarakat
                    LinkButton lbUnduh = (LinkButton)e.Row.FindControl("lbUnduh");
                    LinkButton lbUnduh11 = (LinkButton)e.Row.FindControl("lbUnduh11");
                    LinkButton lbUnduh12 = (LinkButton)e.Row.FindControl("lbUnduh12");
                    LinkButton lbUnduh21 = (LinkButton)e.Row.FindControl("lbUnduh21");
                    LinkButton lbUnduh22 = (LinkButton)e.Row.FindControl("lbUnduh22");
                    LinkButton lbUnduh31 = (LinkButton)e.Row.FindControl("lbUnduh31");
                    LinkButton lbUnduh32 = (LinkButton)e.Row.FindControl("lbUnduh32");
                    LinkButton[] arrLb = { lbUnduh11, lbUnduh12, lbUnduh21, lbUnduh22, lbUnduh31, lbUnduh32 };
                    DataTable dtKelmas2 = new DataTable();
                    objMitraAbdimas.listMitraPelaksanaPengabdian(ref dtKelmas2, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
                    int b = 0;
                    for (int a = 0; a < dtKelmas2.Rows.Count; a++)
                    {
                        if (dtKelmas2.Rows[a]["kd_kategori_mitra"].ToString() == "5") //5 sasaran;
                        {

                            if (dtKelmas2.Rows[a]["kd_desa"].ToString() != "")
                            {
                                //string id_mitra_abdimas = dtKelmas2.Rows[a]["id_mitra_abdimas"].ToString();

                                //string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf",
                                //    ViewState["thn_usulan"].ToString(), id_mitra_abdimas);

                                string sts_unggah_pernyataan = dtKelmas2.Rows[a]["sts_unggah_pernyataan"].ToString();
                                if (sts_unggah_pernyataan == "Sudah unggah")
                                {
                                    lbUnduh.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    lbUnduh.ForeColor = System.Drawing.Color.Gray;
                                }
                            }
                            else
                            {
                                //string id_mitra_abdimas = dtKelmas2.Rows[a]["id_mitra_abdimas"].ToString();

                                //string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf",
                                //    ViewState["thn_usulan"].ToString(), id_mitra_abdimas);
                                if (b <= arrLb.Length - 1)
                                {
                                    string sts_unggah_pernyataan = dtKelmas2.Rows[a]["sts_unggah_pernyataan"].ToString();
                                    if (sts_unggah_pernyataan == "Sudah unggah")
                                    {
                                        arrLb[b].ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        arrLb[b].ForeColor = System.Drawing.Color.Gray;
                                    }
                                }
                                b++;

                            }
                        }
                    }
                }
            }
        }

        /*
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
                        "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
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
            Guid idMitra = Guid.Parse(gvMitraAbdimas.DataKeys[rowIndex]["id_mitra"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvMitraAbdimas.DataKeys[rowIndex]["nama_mitra"].ToString();
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
                        "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
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
                        "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
                    return;
                }
            }
        }
        */
    }
}