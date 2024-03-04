using simlitekkes.Helper;
using simlitekkes.Models.pelaksanaan;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.luaranLapKemajuan
{
    public partial class publikasi : System.Web.UI.UserControl
    {
        public event EventHandler OnChildEvent;
        uiNotify noty = new uiNotify();

        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        publikasiJurnal mdlPublikasiJurnal = new publikasiJurnal();
        Models.login objLogin;
        const string FOLDER_BERKAS_LAP_KEMAJUAN = "~/fileUpload/laporan_kemajuan/BuktiLuaran/";
        const string FOLDER_BERKAS_LAP_AKHIR = "~/fileUpload/laporan_akhir/BuktiLuaran/";
        const string KD_TAHAP_LAP_AKHIR = "34";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                //isiDdlStatusTargetArtikel();
            }
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok2);
        }

        public void setData(string idTransaksiKegiatan, string idLuaranDijanjikan, string strJudulForm, 
            int idKelompokLuaran, string thnPelaksanaan, int idJenisLuaran, string kdTahapanKegiatan)
        {
            ViewState["id_transaksi_kegiatan"] = idTransaksiKegiatan;
            ViewState["id_luaran_dijanjikan"] = idLuaranDijanjikan;
            ViewState["id_kelompok_luaran"] = idKelompokLuaran;
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            ViewState["id_jenis_luaran"] = idJenisLuaran;
            ViewState["kd_tahapan_kegiatan"] = kdTahapanKegiatan;
            lblJudulForm.Text = strJudulForm;
            isiDdlStatusTargetArtikel(idJenisLuaran);
            initData(int.Parse(ddlTargetJenisLuaranPublikasi.SelectedValue));
            showHidePanelPublikasi(idJenisLuaran.ToString());
        }

        private void initData(int idTargetJenisLuaran, bool pilih=false)
        {
            lbUnduhPdfDok.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdfDok2.ForeColor = System.Drawing.Color.Gray;
            DataTable dt = new DataTable();
            if (!pilih)
            {
                mdlLapKemajuan.ListDokumenBuktiLuaran(ref dt, Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString()),
                    Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()),
                    int.Parse(ViewState["id_kelompok_luaran"].ToString()), idTargetJenisLuaran);
            }
            else
            {
                mdlLapKemajuan.ListDokumenBuktiLuaranPilih(ref dt, Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString()),
                    Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()),
                    int.Parse(ViewState["id_kelompok_luaran"].ToString()), idTargetJenisLuaran);
            }
            clearTextBox();
            if (dt.Rows.Count > 0)
            {
                ViewState["id_target_luaran"] = dt.Rows[0]["id_target_luaran"].ToString();
                isiAtributJurnal(int.Parse(dt.Rows[0]["id_target_luaran"].ToString()));

                ViewState["id_target_jenis_luaran"] = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                ViewState["id_dokumen_bukti_luaran1"] = dt.Rows[0]["id_dokumen_bukti_luaran"].ToString();
                ViewState["id_jenis_dokumen_bukti_luaran1"] = dt.Rows[0]["id_jenis_dokumen_bukti_luaran"].ToString();
                ViewState["kd_sts_unggah1"] = dt.Rows[0]["kd_sts_unggah"].ToString();

                var drv = dt.Select("id_target_jenis_luaran="+ddlTargetJenisLuaranPublikasi.SelectedValue);
                //if(drv.Length > 0 )
                //{
                //    DataTable dt2 = new DataTable();
                //    dt2 = drv.CopyToDataTable();
                //    if (dt2.Rows[0]["kd_sts_unggah"].ToString() == "1")
                //    {
                //        lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                //    }
                //}
                if (dt.Rows[0]["kd_sts_unggah"].ToString() == "1")
                {
                    lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                }

                ddlTargetJenisLuaranPublikasi.SelectedValue = dt.Rows[dt.Rows.Count - 1]["id_target_jenis_luaran"].ToString();
            }
            if (dt.Rows.Count > 1)
            {
                ViewState["id_target_luaran"] = dt.Rows[1]["id_target_luaran"].ToString();
                ViewState["id_target_jenis_luaran"] = dt.Rows[1]["id_target_jenis_luaran"].ToString();
                ViewState["id_dokumen_bukti_luaran2"] = dt.Rows[1]["id_dokumen_bukti_luaran"].ToString();
                ViewState["id_jenis_dokumen_bukti_luaran2"] = dt.Rows[1]["id_jenis_dokumen_bukti_luaran"].ToString();
                ViewState["kd_sts_unggah2"] = dt.Rows[1]["kd_sts_unggah"].ToString();
                if (dt.Rows[1]["kd_sts_unggah"].ToString() == "1")
                {
                    lbUnduhPdfDok2.ForeColor = System.Drawing.Color.Red;
                }
            }
            
            if (dt.Rows.Count <= 0)
            {
                ViewState["id_target_luaran"] = -1;
                ViewState["id_dokumen_bukti_luaran1"] = Guid.NewGuid();
                ViewState["id_dokumen_bukti_luaran2"] = Guid.NewGuid();
            }
        }

        private void clearTextBox()
        {
            tbNamaJurnal.Text = string.Empty;

            tbISSN_EISSN.Text = string.Empty;
            tbLembagaPengindek.Text = string.Empty;
            tbUrlJurnal.Text = string.Empty;
            tbJudulArtikel.Text = string.Empty;

            tbTahunPublikasi.Text = string.Empty;
            tbVolume.Text = string.Empty;
            tbNomor.Text = string.Empty;
            tbHalamanAwal.Text = string.Empty;

            tbHalamanAkhir.Text = string.Empty;
            tbUrlArtikel.Text = string.Empty;
            tbDOI.Text = string.Empty;

            if(panelPeringkatAkreditasi.Visible)
                tbPeringkatAkreditasi.Text = string.Empty;
        }

        private void isiAtributJurnal(int id_target_luaran)
        {
            DataTable dt = new DataTable();
            mdlPublikasiJurnal.getDataTargetLuaranPublikasiJurnal(ref dt, id_target_luaran);
            if (dt.Rows.Count > 0)
            {
                ddlPeranPenulis.SelectedValue = dt.Rows[0]["kd_peran_penulis"].ToString();
                tbNamaJurnal.Text = dt.Rows[0]["nama_jurnal"].ToString();

                tbISSN_EISSN.Text = dt.Rows[0]["e_issn"].ToString();
                tbLembagaPengindek.Text = dt.Rows[0]["nama_lembaga_pengindek"].ToString();
                tbUrlJurnal.Text = dt.Rows[0]["url_jurnal"].ToString();
                tbJudulArtikel.Text = dt.Rows[0]["judul_artikel"].ToString();

                tbTahunPublikasi.Text = dt.Rows[0]["tahun"].ToString();
                tbVolume.Text = dt.Rows[0]["volume"].ToString();
                tbNomor.Text = dt.Rows[0]["nomor"].ToString();
                tbHalamanAwal.Text = dt.Rows[0]["halaman_awal"].ToString();

                tbHalamanAkhir.Text = dt.Rows[0]["halaman_akhir"].ToString();
                tbUrlArtikel.Text = dt.Rows[0]["url_artikel"].ToString();
                tbDOI.Text = dt.Rows[0]["doi"].ToString();

                if (panelPeringkatAkreditasi.Visible)
                    tbPeringkatAkreditasi.Text = dt.Rows[0]["peringkat_akreditasi"].ToString();
            }
        }

        private void isiDdlStatusTargetArtikel(int pIdJenisLuaran)
        {
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListTargetJenisLuaran(ref dt, pIdJenisLuaran); // 1 publikasi prosiding
            ddlTargetJenisLuaranPublikasi.Items.Clear();
            ddlTargetJenisLuaranPublikasi.DataSource = dt;
            ddlTargetJenisLuaranPublikasi.DataBind();
        }

        protected void ddlTargetJenisLuaranPublikasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            initData(int.Parse(ddlTargetJenisLuaranPublikasi.SelectedValue), true);
            showHidePanelPublikasi(ViewState["id_jenis_luaran"].ToString());
            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }

        private void showHidePanelPublikasi(string idJenisLuaran)
        {
            panelAtributPublikasi.Visible = false;
            if (ddlTargetJenisLuaranPublikasi.SelectedItem.Text.ToLower() == "published") // published
            {
                panelAtributPublikasi.Visible = true;
            }
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListJenisDokumen(ref dt, int.Parse(ddlTargetJenisLuaranPublikasi.SelectedValue));
            pnlDokumen1.Visible = false;
            pnlDokumen2.Visible = false;
            ViewState["jml_dokumen_pendukung"] = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                lblNamaJenisDokumen1.Text = dt.Rows[0]["jenis_dokumen_bukti_luaran"].ToString();
                lblUkuranMaksDok1.Text = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                ViewState["ukuran_file1_max"] = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                ViewState["id_jenis_dokumen_bukti_luaran1"] = dt.Rows[0]["id_jenis_dokumen_bukti_luaran"].ToString();
                pnlDokumen1.Visible = true;
            }
            if (dt.Rows.Count > 1)
            {
                lblNamaJenisDokumen2.Text = dt.Rows[1]["jenis_dokumen_bukti_luaran"].ToString();
                lblUkuranMaksDok2.Text = (int.Parse(dt.Rows[1]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                ViewState["ukuran_file2_max"] = (int.Parse(dt.Rows[1]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                ViewState["id_jenis_dokumen_bukti_luaran2"] = dt.Rows[1]["id_jenis_dokumen_bukti_luaran"].ToString();
                pnlDokumen2.Visible = true;
            }
            switch(idJenisLuaran)
            {
                case "1":   // Publikasi Ilmiah Jurnal Internasional
                case "21":  // Publikasi Ilmiah Jurnal Nasional Tidak Terakreditasi
                case "54":  // Artikel di Jurnal Internasional Terindeks di Pengindeks Bereputasi
                    panelPeringkatAkreditasi.Visible = false;
                    break;
                case "2":   // Publikasi Ilmiah Jurnal Nasional Terakreditasi
                    panelPeringkatAkreditasi.Visible = true;
                    break;
            }

        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            List<string> emptyField = new List<string>();
            if (tbNamaJurnal.Text.Trim().Length == 0) emptyField.Add("Nama jurnal");
            if (tbISSN_EISSN.Text.Trim().Length == 0) emptyField.Add("ISSN EISSN");
            if (tbLembagaPengindek.Text.Trim().Length == 0) emptyField.Add("Lembaga Pengindek");
            if (tbUrlJurnal.Text.Trim().Length == 0) emptyField.Add("Url Jurnal");
            if (tbJudulArtikel.Text.Trim().Length == 0) emptyField.Add("Judul Artikel");

            if (panelPeringkatAkreditasi.Visible)
            {
                if (tbPeringkatAkreditasi.Text.Trim().Length == 0) emptyField.Add("Peringkat akreditasi");
            }

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            }

            if(ddlTargetJenisLuaranPublikasi.SelectedValue=="4") // published
            {
                emptyField.Clear();
                if (tbTahunPublikasi.Text.Trim().Length < 4) emptyField.Add("Tahun Publikasi");
                if (tbVolume.Text.Trim().Length == 0) emptyField.Add("Volume");
                if (tbNomor.Text.Trim().Length == 0) emptyField.Add("Nomor");
                if (tbHalamanAwal.Text.Trim().Length == 0) emptyField.Add("Halaman Awal");

                if (tbHalamanAkhir.Text.Trim().Length == 0) emptyField.Add("Halaman Akhir");
                if (tbUrlArtikel.Text.Trim().Length == 0) emptyField.Add("Url Artikel");
                if (tbDOI.Text.Trim().Length == 0) emptyField.Add("DOI");

                if (emptyField.Count > 0)
                {
                    var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                    if (OnChildEvent != null)
                        OnChildEvent(sender, null);
                    return;
                }
            }

            int jmlDokPendukung = int.Parse(ViewState["jml_dokumen_pendukung"].ToString());
            string infoUnggah = string.Empty;

            bool stsFile = true;
            if(jmlDokPendukung > 0)
                stsFile = cekFile(ref fileUpload1, int.Parse(ViewState["ukuran_file1_max"].ToString()) * 1024);
            if (jmlDokPendukung > 1)
                stsFile = stsFile & cekFile(ref fileUpload2, int.Parse(ViewState["ukuran_file2_max"].ToString()) * 1024);

            if(!stsFile)
            {
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            }

            string dir = string.Empty;
            if (ViewState["kd_tahapan_kegiatan"].ToString() == KD_TAHAP_LAP_AKHIR)
            {
                dir = FOLDER_BERKAS_LAP_AKHIR;
            }
            else
            {
                dir = FOLDER_BERKAS_LAP_KEMAJUAN; 
            }

            if (!Directory.Exists(Server.MapPath(dir)))
            {
                Directory.CreateDirectory(Server.MapPath(dir));
            }
            dir = string.Format(dir + "{0}/", ViewState["thn_pelaksanaan"].ToString());

            if (!Directory.Exists(Server.MapPath(dir)))
            {
                Directory.CreateDirectory(Server.MapPath(dir));
            }

            // simpan data dokumen ke data base
            bool stsSimpan = false;
            DataTable dtTarget = new DataTable(); 
            if (mdlLapKemajuan.insupTargetLuaran(ref dtTarget, 
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()),
                    Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString()),
                    int.Parse(ViewState["id_kelompok_luaran"].ToString()), 
                    int.Parse(ddlTargetJenisLuaranPublikasi.SelectedValue)
           ))
            {
                ViewState["id_target_luaran"] = dtTarget.Rows[0]["id_target_luaran"].ToString();
                int nomor = 0;
                if(tbNomor.Text.Trim() != "")
                {
                    nomor = int.Parse(tbNomor.Text);
                }
                int halamanAwal = 0;
                if (tbHalamanAwal.Text.Trim() != "")
                {
                    halamanAwal = int.Parse(tbHalamanAwal.Text);
                }

                int halamanAkhir = 0;
                if (tbHalamanAkhir.Text.Trim() != "")
                {
                    halamanAkhir = int.Parse(tbHalamanAkhir.Text);
                }

                //int.Parse(tbNomor.Text),
                //int.Parse(tbHalamanAwal.Text),

                //int.Parse(tbHalamanAkhir.Text),
                string strPeringkatAkreditasi = string.Empty;
                if(panelPeringkatAkreditasi.Visible)
                {
                    strPeringkatAkreditasi = tbPeringkatAkreditasi.Text;
                }

                if (mdlPublikasiJurnal.insupTargetLuaranPublikasiJurnal(
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    ddlPeranPenulis.SelectedValue,
                    tbNamaJurnal.Text,

                    tbISSN_EISSN.Text,
                    tbLembagaPengindek.Text,
                    tbUrlJurnal.Text,
                    tbJudulArtikel.Text,

                    tbTahunPublikasi.Text,
                    tbVolume.Text,
                    
                    nomor,
                    halamanAwal,
                    halamanAkhir,

                    tbUrlArtikel.Text,
                    tbDOI.Text,
                    strPeringkatAkreditasi
                    ))
                {
                    //noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil.");
                    stsSimpan = true;
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal. <br>Kesalahan: " + mdlLapKemajuan.errorMessage);
                return;
            }

            //int jmlDokPendukung = int.Parse(ViewState["jml_dokumen_pendukung"].ToString());

            if (jmlDokPendukung > 0)
            {
                bool file1 = cekFile(ref fileUpload1, int.Parse(ViewState["ukuran_file1_max"].ToString()) * 1024);
                if (file1)
                {
                    prosesUnggah(ref fileUpload1, dir, ViewState["id_dokumen_bukti_luaran1"].ToString());
                    mdlLapKemajuan.insupDokumenBuktiLuaran(Guid.Parse(ViewState["id_dokumen_bukti_luaran1"].ToString()),
                        int.Parse(ViewState["id_target_luaran"].ToString()),
                        int.Parse(ViewState["id_jenis_dokumen_bukti_luaran1"].ToString()),
                        "1");
                    //noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah dokumen berhasil.");
                }
            }
            if (jmlDokPendukung > 1)
            {
                bool file2 = cekFile(ref fileUpload2, int.Parse(ViewState["ukuran_file2_max"].ToString()) * 1024);
                if (file2)
                {
                    if (ViewState["id_dokumen_bukti_luaran2"] == null)
                    {
                        ViewState["id_dokumen_bukti_luaran2"] = Guid.NewGuid().ToString();
                    }
                    prosesUnggah(ref fileUpload2, dir, ViewState["id_dokumen_bukti_luaran2"].ToString());
                    mdlLapKemajuan.insupDokumenBuktiLuaran(Guid.Parse(ViewState["id_dokumen_bukti_luaran2"].ToString()),
                        int.Parse(ViewState["id_target_luaran"].ToString()),
                        int.Parse(ViewState["id_jenis_dokumen_bukti_luaran2"].ToString()),
                        "1");
                }
            }

            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan dan Unggah dokumen berhasil.");

            initData(int.Parse(ddlTargetJenisLuaranPublikasi.SelectedValue), true);
            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }

        private bool cekFile(ref FileUpload fUpload, int maxSize)
        {
            bool isOk = false;
            if (fUpload.HasFile)
            {
                if (fUpload.FileName.ToLower().EndsWith(".pdf"))
                {
                    if (fUpload.PostedFile.ContentLength < (maxSize))
                    {
                        isOk = true;
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan",
                           string.Format("File yang akan diunggah ukurannya tidak boleh melebihi {0} MByte !!!", maxSize / 1000000));
                        //info += string.Format("<br>File yang akan diunggah ukurannya tidak boleh melebihi {0} MByte !!!", maxSize / 1000000);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                    //info += "<br>Silahkan upload File bertipe PDF !!! ";
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File belum dipilih");
                //info = "<br>File belum dipilih. ";
            }
            return isOk;
        }

        private void prosesUnggah(ref FileUpload fUpload, string dir, string id_transaksi_kegiatan)
        {
            if (!Directory.Exists(Server.MapPath(dir)))
            {
                Directory.CreateDirectory(Server.MapPath(dir));
            }

            string namaFile = string.Format("{0}{1}.pdf", dir.ToString(), id_transaksi_kegiatan.ToString());
            fUpload.SaveAs(Server.MapPath(namaFile));
        }

        protected void lbUnduhPdfDok_Click(object sender, EventArgs e)
        {
            string folderUnduh = "~/fileUpload/laporan_kemajuan/BuktiLuaran/" + ViewState["thn_pelaksanaan"].ToString();
            if (ViewState["kd_tahapan_kegiatan"].ToString() == KD_TAHAP_LAP_AKHIR)
            {
                folderUnduh = FOLDER_BERKAS_LAP_AKHIR + ViewState["thn_pelaksanaan"].ToString();
            }
            string namaFileAsli = ViewState["id_dokumen_bukti_luaran1"].ToString();
            string namaFileDiunduh = lblNamaJenisDokumen1.Text + ".pdf";

            if (File.Exists(Server.MapPath(folderUnduh + "/" + namaFileAsli + ".pdf")))
            {
                unduhPDF(folderUnduh, namaFileAsli, namaFileDiunduh);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }

        protected void lbUnduhPdfDok2_Click(object sender, EventArgs e)
        {
            string folderUnduh = "~/fileUpload/laporan_kemajuan/BuktiLuaran/" + ViewState["thn_pelaksanaan"].ToString();
            if (ViewState["kd_tahapan_kegiatan"].ToString() == KD_TAHAP_LAP_AKHIR)
            {
                folderUnduh = FOLDER_BERKAS_LAP_AKHIR + ViewState["thn_pelaksanaan"].ToString();
            }
            string namaFileAsli = ViewState["id_dokumen_bukti_luaran2"].ToString();
            string namaFileDiunduh = lblNamaJenisDokumen2.Text + ".pdf";

            if (File.Exists(Server.MapPath(folderUnduh + "/" + namaFileAsli + ".pdf")))
            {
                unduhPDF(folderUnduh, namaFileAsli, namaFileDiunduh);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }

        private void unduhPDF(string folderUnduh, string namaFileAsli, string namaFileDiunduh)
        {
            //string namaBerkas = "dokumenUsulan.pdf";
            var atributUnduh = new AtributUnduh
            {
                FolderUnduh = folderUnduh,
                NamaBerkas = namaFileAsli + ".pdf",
                NamaBerkasdiUnduh = namaFileDiunduh
            };
            Session["AtributUnduh"] = atributUnduh;
            var unduhForm = "helper/unduhFile.aspx";
            Response.Redirect(unduhForm);
        }

    }
}