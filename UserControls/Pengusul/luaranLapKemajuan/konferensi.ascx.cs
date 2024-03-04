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
    public partial class konferensi : System.Web.UI.UserControl
    {
        public event EventHandler OnChildEvent;
        uiNotify noty = new uiNotify();

        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        Models.pelaksanaan.konferensi mdlKonferensi = new Models.pelaksanaan.konferensi();
        const string FOLDER_BERKAS_LAP_KEMAJUAN = "~/fileUpload/laporan_kemajuan/BuktiLuaran/";
        const string FOLDER_BERKAS_LAP_AKHIR = "~/fileUpload/laporan_akhir/BuktiLuaran/";
        const string KD_TAHAP_LAP_AKHIR = "34";

        protected void Page_Load(object sender, EventArgs e)
        {
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
            initData(int.Parse(ddlTargetJenisLuaranPublikasi.SelectedValue), false);
            showHidePanelPublikasi(idJenisLuaran.ToString());
        }

        private void initData(int idTargetJenisLuaran, bool pilih = false)
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
                if (dt.Rows[0]["kd_sts_unggah"].ToString() == "1")
                {
                    lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                }
                ddlTargetJenisLuaranPublikasi.SelectedValue = dt.Rows[dt.Rows.Count -  1]["id_target_jenis_luaran"].ToString();
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
            tbNamaKonferens.Text = string.Empty;
            tbLembagaPenyelenggara.Text = string.Empty;

            tbTempatPenyelenggaraan.Text = string.Empty;
            tbTglPenyelenggaraanMulai.Text = string.Empty;
            tbTglPenyelenggaraanSelesai.Text = string.Empty;

            tbLembagaPengindek.Text = string.Empty;
            tbUrlKonferensi.Text = string.Empty;
            tbJudulArtikel.Text = string.Empty;

            tbISSN_EISSN.Text = string.Empty;
        }

        private void isiAtributJurnal(int id_target_luaran)
        {
            DataTable dt = new DataTable();
            mdlKonferensi.getDataTargetLuaranKonferensi(ref dt, id_target_luaran);
            if (dt.Rows.Count > 0)
            {
                ddlPeranPenulis.SelectedValue = dt.Rows[0]["kd_peran_penulis"].ToString();
                tbNamaKonferens.Text = dt.Rows[0]["nama_konference"].ToString();
                tbLembagaPenyelenggara.Text = dt.Rows[0]["nama_lembaga_penyelenggara"].ToString();

                tbTempatPenyelenggaraan.Text = dt.Rows[0]["tempat_penyelenggara"].ToString();
                tbTglPenyelenggaraanMulai.Text = DateTime.Parse(dt.Rows[0]["tgl_penyelenggaraan_mulai"].ToString()).ToString("yyyy-MM-dd");
                //dt.Rows[0]["tgl_penyelenggaraan_mulai"].ToString();
                tbTglPenyelenggaraanSelesai.Text = DateTime.Parse(dt.Rows[0]["tgl_penyelenggaraan_selesai"].ToString()).ToString("yyyy-MM-dd");
                //dt.Rows[0]["tgl_penyelenggaraan_selesai"].ToString();

                tbLembagaPengindek.Text = dt.Rows[0]["nama_lembaga_pengindeks"].ToString();
                tbUrlKonferensi.Text = dt.Rows[0]["url_website"].ToString();
                tbJudulArtikel.Text = dt.Rows[0]["judul_artikel"].ToString();

                tbISSN_EISSN.Text = dt.Rows[0]["isbn_issn"].ToString();
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

            pnlISSN.Visible = false;
            if (ddlTargetJenisLuaranPublikasi.SelectedItem.Text.Trim().ToLower() == "published")
            {
                pnlISSN.Visible = true;
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

        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            List<string> emptyField = new List<string>();
            if (tbNamaKonferens.Text.Trim().Length == 0) emptyField.Add("Nama konferens");
            if (tbLembagaPenyelenggara.Text.Trim().Length == 0) emptyField.Add("Lembaga penyelenggara");
            if (tbTempatPenyelenggaraan.Text.Trim().Length == 0) emptyField.Add("Tempat penyelenggaraan");
            
            if (tbTglPenyelenggaraanMulai.Text.Trim().Length == 0) emptyField.Add("Tgl mulai");
            if (tbTglPenyelenggaraanSelesai.Text.Trim().Length == 0) emptyField.Add("Tgl Selesai");
            if (tbJudulArtikel.Text.Trim().Length == 0) emptyField.Add("Judul Artikel");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            }

            if (ddlTargetJenisLuaranPublikasi.SelectedItem.Text.Trim().ToLower() == "published") // published
            {
                emptyField.Clear();
                if (tbISSN_EISSN.Text.Trim().Length < 4) emptyField.Add("Nomor ISSN ISBN");
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
            if (jmlDokPendukung > 0)
                stsFile = cekFile(ref fileUpload1, int.Parse(ViewState["ukuran_file1_max"].ToString()) * 1024);
            if (jmlDokPendukung > 1)
                stsFile = stsFile & cekFile(ref fileUpload2, int.Parse(ViewState["ukuran_file2_max"].ToString()) * 1024);

            if (!stsFile)
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

            string isbn_issn = "";
            if (pnlISSN.Visible)
            {
                isbn_issn = tbISSN_EISSN.Text;
            }
            // simpan data dokumen ke data base
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
                if (mdlKonferensi.insupTargetLuaranKonferensi(
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    ddlPeranPenulis.SelectedValue,
                    tbNamaKonferens.Text,
                    tbLembagaPenyelenggara.Text,

                    tbTempatPenyelenggaraan.Text,
                    tbTglPenyelenggaraanMulai.Text,
                    tbTglPenyelenggaraanSelesai.Text,

                    tbLembagaPengindek.Text,
                    tbUrlKonferensi.Text,
                    tbJudulArtikel.Text,

                    isbn_issn, // isbn issn
                    "" //linkVideo

                    ))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil.");
                }
            }
            else
            {
                //noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal.");
            }

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
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah dokumen berhasil.");
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
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah dokumen berhasil.");
                }
            }
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
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File belum dipilih");
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
