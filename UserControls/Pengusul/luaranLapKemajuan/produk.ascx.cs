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
    public partial class produk : System.Web.UI.UserControl
    {
        public event EventHandler OnChildEvent;
        uiNotify noty = new uiNotify();

        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        publikasiJurnal mdlPublikasiJurnal = new publikasiJurnal();
        luaranProduk mdlProduk = new luaranProduk();

        const string FOLDER_BERKAS_LAP_KEMAJUAN = "~/fileUpload/laporan_kemajuan/BuktiLuaran/";
        const string FOLDER_BERKAS_LAP_AKHIR = "~/fileUpload/laporan_akhir/BuktiLuaran/";
        const string KD_TAHAP_LAP_AKHIR = "34";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isiDdlStatusTargetStrategi();
            }
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok2);

        }

        public void setData(string idTransaksiKegiatan, string idLuaranDijanjikan, string strJudulForm, int idKelompokLuaran, 
            string thnPelaksanaan, int idJenisLuaran, string kdTahapanKegiatan)
        {
            ViewState["id_transaksi_kegiatan"] = idTransaksiKegiatan;
            ViewState["id_luaran_dijanjikan"] = idLuaranDijanjikan;
            ViewState["id_kelompok_luaran"] = idKelompokLuaran;
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            ViewState["id_jenis_luaran"] = idJenisLuaran;
            ViewState["kd_tahapan_kegiatan"] = kdTahapanKegiatan;
            lblJudulForm.Text = strJudulForm;

            DataTable dt = new DataTable();
            mdlLapKemajuan.cekTargetLuaran(ref dt, idTransaksiKegiatan, idKelompokLuaran, idJenisLuaran);
            if (dt.Rows.Count > 0)
            {
                isiDdlStatusTargetStrategi();
                ddlStatusProduk.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                initData(int.Parse(dt.Rows[0]["id_target_jenis_luaran"].ToString()), true);
            }
            else
            {
                isiDdlStatusTargetStrategi();
                initData(int.Parse(ddlStatusProduk.SelectedValue), false);
            }
            //initData(0);
            //isiDdlStatusTargetStrategi();
            //initData(int.Parse(ddlStatusProduk.SelectedValue), false);
            showHidePanelProduk();
        }

        protected void ddlStatusProduk_SelectedIndexChanged(object sender, EventArgs e)
        {
            initData(int.Parse(ddlStatusProduk.SelectedValue), true);
            showHidePanelProduk();
            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }

        private void showHidePanelProduk()
        {
            lblPeriodePenerapan.Visible = false;
            lblPeriodeCoba.Visible = false;
            lblLinkVideoModel.Visible = false;
            lblLinkVideoProduk.Visible = false;
            if (ddlStatusProduk.SelectedValue == "100")
            {
                lblPeriodeCoba.Visible = true;
                lblLinkVideoProduk.Visible = true;
            }
            if(ddlStatusProduk.SelectedValue == "101")
            {
                lblPeriodePenerapan.Visible = true;
                lblLinkVideoModel.Visible = true;
            }

            DataTable dt = new DataTable();
            mdlLapKemajuan.ListJenisDokumen(ref dt, int.Parse(ddlStatusProduk.SelectedValue));
            pnlUnggah1.Visible = false;
            pnlUnggah2.Visible = false;
            pnlUnggah3.Visible = false;
            ViewState["jml_dokumen_pendukung"] = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                lblJudulUnggah1.Text = dt.Rows[0]["jenis_dokumen_bukti_luaran"].ToString();
                lblInfoFileUnggah1.Text = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                ViewState["ukuran_file1_max"] = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                ViewState["id_jenis_dokumen_bukti_luaran1"] = dt.Rows[0]["id_jenis_dokumen_bukti_luaran"].ToString();
                pnlUnggah1.Visible = true;
            }
            if (dt.Rows.Count > 1)
            {
                lblJudulUnggah2.Text = dt.Rows[1]["jenis_dokumen_bukti_luaran"].ToString();
                lblInfoFileUnggah2.Text = (int.Parse(dt.Rows[1]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                ViewState["ukuran_file2_max"] = (int.Parse(dt.Rows[1]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                ViewState["id_jenis_dokumen_bukti_luaran2"] = dt.Rows[1]["id_jenis_dokumen_bukti_luaran"].ToString();
                pnlUnggah2.Visible = true;
            }
            if (dt.Rows.Count > 2)
            {
                lblJudulUnggah3.Text = dt.Rows[2]["jenis_dokumen_bukti_luaran"].ToString();
                lblInfoFileUnggah3.Text = (int.Parse(dt.Rows[2]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                ViewState["ukuran_file3_max"] = (int.Parse(dt.Rows[2]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                ViewState["id_jenis_dokumen_bukti_luaran3"] = dt.Rows[2]["id_jenis_dokumen_bukti_luaran"].ToString();
                pnlUnggah3.Visible = true;
            }

            //initData(int.Parse(ddlTargetJenisLuaranPublikasi.SelectedValue));
        }

        private void initData(int idTargetJenisLuaran, bool pilih = false)
        {
            lbUnduhPdfDok.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdfDok2.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdfDok3.ForeColor = System.Drawing.Color.Gray;
            DataTable dt = new DataTable();
            if (!pilih)
            {
                mdlLapKemajuan.ListDokumenBuktiLuaran(ref dt, Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString()),
                    int.Parse(ViewState["id_kelompok_luaran"].ToString()), idTargetJenisLuaran);
            }
            else
            {
                mdlLapKemajuan.ListDokumenBuktiLuaranPilih(ref dt, Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString()),
                    int.Parse(ViewState["id_kelompok_luaran"].ToString()), idTargetJenisLuaran);
            }
            clearTextBox();
            if (dt.Rows.Count > 0)
            {
                ViewState["id_target_luaran"] = dt.Rows[0]["id_target_luaran"].ToString();
                isiAtributProduk(int.Parse(dt.Rows[0]["id_target_luaran"].ToString()));

                ViewState["id_target_jenis_luaran"] = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                ViewState["id_dokumen_bukti_luaran1"] = dt.Rows[0]["id_dokumen_bukti_luaran"].ToString();
                ViewState["id_jenis_dokumen_bukti_luaran1"] = dt.Rows[0]["id_jenis_dokumen_bukti_luaran"].ToString();
                ViewState["kd_sts_unggah1"] = dt.Rows[0]["kd_sts_unggah"].ToString();

                if (dt.Rows[0]["kd_sts_unggah"].ToString() == "1")
                {
                    lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                }
                ddlStatusProduk.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
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

            if (dt.Rows.Count > 2)
            {
                ViewState["id_target_luaran"] = dt.Rows[2]["id_target_luaran"].ToString();
                ViewState["id_target_jenis_luaran"] = dt.Rows[2]["id_target_jenis_luaran"].ToString();
                ViewState["id_dokumen_bukti_luaran3"] = dt.Rows[2]["id_dokumen_bukti_luaran"].ToString();
                ViewState["id_jenis_dokumen_bukti_luaran3"] = dt.Rows[2]["id_jenis_dokumen_bukti_luaran"].ToString();
                ViewState["kd_sts_unggah3"] = dt.Rows[2]["kd_sts_unggah"].ToString();
                if (dt.Rows[2]["kd_sts_unggah"].ToString() == "1")
                {
                    lbUnduhPdfDok3.ForeColor = System.Drawing.Color.Red;
                }

            }

            if (dt.Rows.Count <= 0)
            {
                ViewState["id_target_luaran"] = -1;
                ViewState["id_dokumen_bukti_luaran1"] = Guid.NewGuid();
                ViewState["id_dokumen_bukti_luaran2"] = Guid.NewGuid();
                ViewState["id_dokumen_bukti_luaran3"] = Guid.NewGuid();

            }
        }

        private void isiAtributProduk(int id_target_luaran)
        {
            DataTable dt = new DataTable();
            mdlProduk.getDataTargetLuaranProduk(ref dt, id_target_luaran);
            if (dt.Rows.Count > 0)
            {
                tbNamaProduk.Text = dt.Rows[0]["nama_produk"].ToString();
                tbPemegangProduk.Text = dt.Rows[0]["pemegang_produk"].ToString();
                tbTglAwal.Text = dt.Rows[0]["tgl_awal_periode_uji"].ToString();
                tbTglAkhir.Text = dt.Rows[0]["tgl_akhir_periode_uji"].ToString();
                tbLinkVideo.Text = dt.Rows[0]["link_video_dokumentasi_pengujian"].ToString();
            }
        }

        private void isiDdlStatusTargetStrategi()
        {
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListTargetJenisLuaran(ref dt, 35); // produk
            ddlStatusProduk.Items.Clear();
            ddlStatusProduk.DataSource = dt;
            ddlStatusProduk.DataBind();
            //showHidePanelPublikasi();
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
            string namaFileDiunduh = lblJudulUnggah1.Text + ".pdf";

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
            string namaFileDiunduh = lblJudulUnggah2.Text + ".pdf";

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

        protected void lbUnduhPdfDok3_Click(object sender, EventArgs e)
        {
            string folderUnduh = "~/fileUpload/laporan_kemajuan/BuktiLuaran/" + ViewState["thn_pelaksanaan"].ToString();
            if (ViewState["kd_tahapan_kegiatan"].ToString() == KD_TAHAP_LAP_AKHIR)
            {
                folderUnduh = FOLDER_BERKAS_LAP_AKHIR + ViewState["thn_pelaksanaan"].ToString();
            }
            string namaFileAsli = ViewState["id_dokumen_bukti_luaran3"].ToString();
            string namaFileDiunduh = lblJudulUnggah3.Text + ".pdf";

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

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            List<string> emptyField = new List<string>();
            if (tbNamaProduk.Text.Trim().Length == 0) emptyField.Add("Nama Produk");
            if (tbPemegangProduk.Text.Trim().Length == 0) emptyField.Add("Nama Pemegang Produk");
            if (tbTglAwal.Text.Trim().Length == 0) emptyField.Add("Tgl Awal");
            if (tbTglAkhir.Text.Trim().Length == 0) emptyField.Add("Tgl Akhir");
            if (tbLinkVideo.Text.Trim().Length == 0) emptyField.Add("Link Video");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
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
            int idTargetLuaran = int.Parse(ViewState["id_target_luaran"].ToString());
            Guid idLuaranDijanjikan = Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString());
            Guid idTransaksiKegiatan = Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString());
            int idKelompokLuaran = int.Parse(ViewState["id_kelompok_luaran"].ToString());
            int status = int.Parse(ddlStatusProduk.SelectedValue);


            DataTable dtTarget = new DataTable();
            if (mdlLapKemajuan.insupTargetLuaran(ref dtTarget,
                    idTargetLuaran, idLuaranDijanjikan, idTransaksiKegiatan, idKelompokLuaran, status                    
           ))

            {
                ViewState["id_target_luaran"] = dtTarget.Rows[0]["id_target_luaran"].ToString();

                if (mdlProduk.insupTargetLuaranProduk(
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    tbNamaProduk.Text,
                    tbPemegangProduk.Text,
                    tbTglAwal.Text,
                    tbTglAkhir.Text,
                    tbLinkVideo.Text
                    ))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil.");
                    }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal.");
            }

            int jmlDokPendukung = int.Parse(ViewState["jml_dokumen_pendukung"].ToString());
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

            if (jmlDokPendukung > 2)
            {
                bool file3 = cekFile(ref fileUpload3, int.Parse(ViewState["ukuran_file3_max"].ToString()) * 1024);
                if (file3)
                {
                    if (ViewState["id_dokumen_bukti_luaran3"] == null)
                    {
                        ViewState["id_dokumen_bukti_luaran3"] = Guid.NewGuid().ToString();
                    }
                    prosesUnggah(ref fileUpload3, dir, ViewState["id_dokumen_bukti_luaran3"].ToString());
                    mdlLapKemajuan.insupDokumenBuktiLuaran(Guid.Parse(ViewState["id_dokumen_bukti_luaran3"].ToString()),
                        int.Parse(ViewState["id_target_luaran"].ToString()),
                        int.Parse(ViewState["id_jenis_dokumen_bukti_luaran3"].ToString()),
                        "1");
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah dokumen berhasil.");
                }
            }
            initData(int.Parse(ddlStatusProduk.SelectedValue), true);
            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }
        private void clearTextBox()
        {
            tbNamaProduk.Text = string.Empty;
            tbPemegangProduk.Text = string.Empty;
            tbTglAwal.Text = string.Empty;
            tbTglAkhir.Text = string.Empty;
            tbLinkVideo.Text = string.Empty;
        }
    }
}