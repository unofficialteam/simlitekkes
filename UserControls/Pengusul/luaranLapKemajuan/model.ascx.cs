using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using simlitekkes.Helper;
using simlitekkes.Models.pelaksanaan;
using System.Data;
using System.IO;

namespace simlitekkes.UserControls.Pengusul.luaranLapKemajuan
{
    public partial class model : System.Web.UI.UserControl
    {
        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        luaranModel mdlModel = new luaranModel();

        public event EventHandler OnChildEvent;
        uiNotify noty = new uiNotify();

        const int ID_STATUS_PRODUK = 129;
        const int ID_STATUS_PENERAPAN = 130;

        const string FOLDER_BERKAS_LAP_KEMAJUAN = "~/fileUpload/laporan_kemajuan/BuktiLuaran/";
        const string FOLDER_BERKAS_LAP_AKHIR = "~/fileUpload/laporan_akhir/BuktiLuaran/";
        const string KD_TAHAP_LAP_AKHIR = "34";

        private int idJenisLuaran
        {
            get
            {
                if (ViewState["id_jenis_luaran"] == null) ViewState["id_jenis_luaran"] = int.MinValue;
                return int.Parse(ViewState["id_jenis_luaran"].ToString());
            }
            set { ViewState["id_jenis_luaran"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok1);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok2);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok3);

            if (!IsPostBack)
            {
                isiDdlStatusModel();                
            }
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

            DataTable dt = new DataTable();
            mdlLapKemajuan.cekTargetLuaran(ref dt, idTransaksiKegiatan, idKelompokLuaran, idJenisLuaran);
            if (dt.Rows.Count > 0)
            {
                isiDdlStatusModel();
                ddlStatusModel.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                initData(int.Parse(dt.Rows[0]["id_target_jenis_luaran"].ToString()), true);
            }
            else
            {
                isiDdlStatusModel();
                initData(int.Parse(ddlStatusModel.SelectedValue), false);
            }

            //isiDdlStatusModel();
            //initData(int.Parse(ddlStatusModel.SelectedValue), false);
            showHidePanel();
        }

        private void isiDdlStatusModel()
        {
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListTargetJenisLuaran(ref dt, idJenisLuaran);
            ddlStatusModel.Items.Clear();
            ddlStatusModel.DataSource = dt;
            ddlStatusModel.DataBind();
        }

        protected void ddlStatusModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            initData(int.Parse(ddlStatusModel.SelectedValue), true);
            showHidePanel();

            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }

        private void initData(int idTargetJenisLuaran, bool pilih = false)
        {
            lbUnduhPdfDok1.ForeColor = System.Drawing.Color.Gray;
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
                isiAtribut(int.Parse(ViewState["id_target_luaran"].ToString()));

                ViewState["id_target_jenis_luaran"] = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                ViewState["id_dokumen_bukti_luaran1"] = dt.Rows[0]["id_dokumen_bukti_luaran"].ToString();
                ViewState["id_jenis_dokumen_bukti_luaran1"] = dt.Rows[0]["id_jenis_dokumen_bukti_luaran"].ToString();
                ViewState["kd_sts_unggah1"] = dt.Rows[0]["kd_sts_unggah"].ToString();
                if (dt.Rows[0]["kd_sts_unggah"].ToString() == "1")
                {
                    lbUnduhPdfDok1.ForeColor = System.Drawing.Color.Red;
                }
                ddlStatusModel.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
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

        private void isiAtribut(int id_target_luaran)
        {
            DataTable dt = new DataTable();
            mdlModel.getDataTargetLuaranModel(ref dt, id_target_luaran);
            if (dt.Rows.Count > 0)
            {
                tbNamaModel.Text = dt.Rows[0]["nama_model"].ToString();
                tbPemegangModel.Text = dt.Rows[0]["pemegang_model"].ToString();

                tbTglPeriodeAwal.Text = dt.Rows[0]["periode_uji_coba_mulai"].ToString() != "" ?
                DateTime.Parse(dt.Rows[0]["periode_uji_coba_mulai"].ToString()).ToString("yyyy-MM-dd") : "";
                tbTglPeriodeBerakhir.Text = dt.Rows[0]["periode_uji_coba_berakhir"].ToString() != "" ?
                DateTime.Parse(dt.Rows[0]["periode_uji_coba_berakhir"].ToString()).ToString("yyyy-MM-dd") : "";

                tbLinkVideoDokumentasi.Text = dt.Rows[0]["link_video_dokumentasi"].ToString();
            }
            else
            {
                clearTextBox();
            }
        }

        private void clearTextBox()
        {
            tbNamaModel.Text = string.Empty;
            tbPemegangModel.Text = string.Empty;
            tbTglPeriodeAwal.Text = string.Empty;
            tbTglPeriodeBerakhir.Text = string.Empty;
            tbLinkVideoDokumentasi.Text = string.Empty;
        }

        private void showHidePanel()
        {
            //Show hide label isian
            if (int.Parse(ddlStatusModel.SelectedValue) == ID_STATUS_PENERAPAN)
            {
                lblPeriode.Text = "Periode uji coba penerapan model";
                lblLinkVideoDokumentasi.Text = "Link video dokumentasi penerapan model";
            }
            else
            {
                lblPeriode.Text = "Periode uji coba model";
                lblLinkVideoDokumentasi.Text = "Link video dokumentasi pengujian model";
            }

            // Show Hide Unggah Dokumen
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListJenisDokumen(ref dt, int.Parse(ddlStatusModel.SelectedValue));
            ViewState["jml_dokumen_pendukung"] = dt.Rows.Count;

            if (dt.Rows.Count > 0)
            {
                lblNamaJenisDokumen1.Text = dt.Rows[0]["jenis_dokumen_bukti_luaran"].ToString();
                lblUkuranMaksDok1.Text = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                ViewState["ukuran_file1_max"] = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                ViewState["id_jenis_dokumen_bukti_luaran1"] = dt.Rows[0]["id_jenis_dokumen_bukti_luaran"].ToString();
            }
            if (dt.Rows.Count > 1)
            {
                lblNamaJenisDokumen2.Text = dt.Rows[1]["jenis_dokumen_bukti_luaran"].ToString();
                lblUkuranMaksDok2.Text = (int.Parse(dt.Rows[1]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                ViewState["ukuran_file2_max"] = (int.Parse(dt.Rows[1]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                ViewState["id_jenis_dokumen_bukti_luaran2"] = dt.Rows[1]["id_jenis_dokumen_bukti_luaran"].ToString();
            }
            if (dt.Rows.Count > 2)
            {
                lblNamaJenisDokumen3.Text = dt.Rows[2]["jenis_dokumen_bukti_luaran"].ToString();
                lblUkuranMaksDok3.Text = (int.Parse(dt.Rows[2]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                ViewState["ukuran_file3_max"] = (int.Parse(dt.Rows[2]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                ViewState["id_jenis_dokumen_bukti_luaran3"] = dt.Rows[2]["id_jenis_dokumen_bukti_luaran"].ToString();
            }
        }

        protected void lbUnduhPdfDok1_Click(object sender, EventArgs e)
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

        protected void lbUnduhPdfDok3_Click(object sender, EventArgs e)
        {
            string folderUnduh = "~/fileUpload/laporan_kemajuan/BuktiLuaran/" + ViewState["thn_pelaksanaan"].ToString();
            if (ViewState["kd_tahapan_kegiatan"].ToString() == KD_TAHAP_LAP_AKHIR)
            {
                folderUnduh = FOLDER_BERKAS_LAP_AKHIR + ViewState["thn_pelaksanaan"].ToString();
            }
            string namaFileAsli = ViewState["id_dokumen_bukti_luaran3"].ToString();
            string namaFileDiunduh = lblNamaJenisDokumen3.Text + ".pdf";

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
            // Cek isian
            List<string> emptyField = new List<string>();
            if (tbNamaModel.Text.Trim().Length == 0) emptyField.Add("Nama model");
            if (tbPemegangModel.Text.Trim().Length == 0) emptyField.Add("Pemegang model");
            if (tbTglPeriodeAwal.Text.Trim().Length == 0) emptyField.Add("Periode uji coba awal");
            if (tbTglPeriodeBerakhir.Text.Trim().Length == 0) emptyField.Add("Periode uji coba berakhir");
            if (tbLinkVideoDokumentasi.Text.Trim().Length == 0) emptyField.Add("Link video dokumentasi");

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
            DataTable dtTarget = new DataTable();
            if (mdlLapKemajuan.insupTargetLuaran(ref dtTarget,
                int.Parse(ViewState["id_target_luaran"].ToString()),
                Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()),
                Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString()),
                int.Parse(ViewState["id_kelompok_luaran"].ToString()),
                int.Parse(ddlStatusModel.SelectedValue)
           ))
            {
                ViewState["id_target_luaran"] = dtTarget.Rows[0]["id_target_luaran"].ToString();
                if (mdlModel.insupTargetLuaranModel(
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    tbNamaModel.Text, tbPemegangModel.Text, tbTglPeriodeAwal.Text,
                    tbTglPeriodeBerakhir.Text, tbLinkVideoDokumentasi.Text
                    ))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil.");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal.");
            }

            //Unggah dokumen
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

            initData(int.Parse(ddlStatusModel.SelectedValue), true);
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
    }
}