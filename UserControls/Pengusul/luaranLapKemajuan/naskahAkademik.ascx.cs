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
    public partial class naskahAkademik : System.Web.UI.UserControl
    {
        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        simlitekkes.Models.pelaksanaan.naskahAkademik mdlKebijakan = new simlitekkes.Models.pelaksanaan.naskahAkademik();

        public event EventHandler OnChildEvent;
        uiNotify noty = new uiNotify();

        const int ID_JENIS_LUARAN_KEBIJAKAN = 32;

        const int DRAFT_KEBIJAKAN = 90;
        const int DRAFT_NASKAH_AKADEMIK = 93;

        const int PENERAPAN_KEBIJAKAN = 92;
        const int PENERAPAN_NASKAH_AKADEMIK = 95;

        const int PRODUK_KEBIJAKAN = 91;
        const int PRODUK_NASKAH_AKADEMIK = 94;

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


            if (!IsPostBack)
            {
                isiDdlJenisNaskah();
                isiDdlStatusNaskah();
            }
        }

        private void isiDdlJenisNaskah()
        {
            DataTable dt = new DataTable();
            mdlKebijakan.ListJenisNaskahAkademik(ref dt);
            ddlJenisNaskahKebijakan.Items.Clear();
            ddlJenisNaskahKebijakan.DataSource = dt;
            ddlJenisNaskahKebijakan.DataBind();
        }

        private void isiDdlStatusNaskah()
        {
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListTargetJenisLuaran(ref dt, idJenisLuaran);
            ddlStatusKebijakan.Items.Clear();
            ddlStatusKebijakan.DataSource = dt;
            ddlStatusKebijakan.DataBind();
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

            DataTable dt = new DataTable();
            mdlLapKemajuan.cekTargetLuaran(ref dt, idTransaksiKegiatan, idKelompokLuaran, idJenisLuaran);
            if (dt.Rows.Count > 0)
            {
                isiDdlJenisNaskah();
                isiDdlStatusNaskah();
                ddlStatusKebijakan.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                initData(int.Parse(dt.Rows[0]["id_target_jenis_luaran"].ToString()), true);
            }
            else
            {
                isiDdlJenisNaskah();
                isiDdlStatusNaskah();
                initData(int.Parse(ddlStatusKebijakan.SelectedValue), false);
            }

            //lblJudulForm.Text = strJudulForm;
            //isiDdlJenisNaskah();
            //isiDdlStatusNaskah();
            //initData(int.Parse(ddlStatusKebijakan.SelectedValue));
            showHidePanel();
        }

        protected void ddlStatusKebijakan_SelectedIndexChanged(object sender, EventArgs e)
        {
            initData(int.Parse(ddlStatusKebijakan.SelectedValue), true);
            showHidePanel();

            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }

        private void initData(int idTargetJenisLuaran, bool pilih = false)
        {
            lbUnduhPdfDok1.ForeColor = System.Drawing.Color.Gray;


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
            if (int.Parse(ddlStatusKebijakan.SelectedValue) == 93 ||
                int.Parse(ddlStatusKebijakan.SelectedValue) == 94 ||
                int.Parse(ddlStatusKebijakan.SelectedValue) == 230 ||
                    int.Parse(ddlStatusKebijakan.SelectedValue) == 231)
            {
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
                    ddlStatusKebijakan.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                }


                if (dt.Rows.Count <= 0)
                {
                    ViewState["id_target_luaran"] = -1;
                    ViewState["id_dokumen_bukti_luaran1"] = Guid.NewGuid();
                    ViewState["id_dokumen_bukti_luaran2"] = Guid.NewGuid();
                }
            }

        }

        private void isiAtribut(int id_target_luaran)
        {
            DataTable dt = new DataTable();
            mdlKebijakan.getDataTargetLuaranAkademik(ref dt, id_target_luaran);
            if (dt.Rows.Count > 0)
            {
                ddlJenisNaskahKebijakan.SelectedValue = dt.Rows[0]["id_jenis_naskah_akademik"].ToString();
                tbNamaLembaga.Text = dt.Rows[0]["lembaga_yg_menerima"].ToString();
                //tbNamaPejabatPenerima.Text = dt.Rows[0]["nama_pejabat_yg_menerima"].ToString();
                //tbJabatanPenerima.Text = dt.Rows[0]["jabatan_yg_menerima"].ToString();
                //tbTglPenyerahan.Text = dt.Rows[0]["tgl_penyerahan"].ToString() != "" ?
                //DateTime.Parse(dt.Rows[0]["tgl_penyerahan"].ToString()).ToString("yyyy-MM-dd") : "";
                //tbTglMulaiPenerapan.Text = dt.Rows[0]["tgl_mulai_penerapan"].ToString() != "" ?
                //DateTime.Parse(dt.Rows[0]["tgl_mulai_penerapan"].ToString()).ToString("yyyy-MM-dd") : "";
            }
            else
            {
                clearTextBox();
            }
        }

        private void clearTextBox()
        {
            ddlJenisNaskahKebijakan.SelectedIndex = 0;
            tbNamaLembaga.Text = string.Empty;
            //tbNamaPejabatPenerima.Text = string.Empty;
            //tbJabatanPenerima.Text = string.Empty;
            //tbTglPenyerahan.Text = string.Empty;
            //tbTglMulaiPenerapan.Text = string.Empty;
        }

        private void showHidePanel()
        {
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListJenisDokumen(ref dt, int.Parse(ddlStatusKebijakan.SelectedValue));
            ViewState["jml_dokumen_pendukung"] = dt.Rows.Count;


            if (dt.Rows.Count > 0)
            {
                lblNamaJenisDokumen1.Text = dt.Rows[0]["jenis_dokumen_bukti_luaran"].ToString();
                lblUkuranMaksDok1.Text = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                ViewState["ukuran_file1_max"] = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                ViewState["id_jenis_dokumen_bukti_luaran1"] = dt.Rows[0]["id_jenis_dokumen_bukti_luaran"].ToString();
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
            List<string> emptyField = new List<string>();

            emptyField.Clear();
            if (tbNamaLembaga.Text.Trim().Length == 0) emptyField.Add("Institusi/lembaga yg penerapkan");
            //if (tbTglMulaiPenerapan.Text.Trim().Length == 0) emptyField.Add("Tanggal mulai penerapan");

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
            string cek;
            cek = ViewState["id_target_luaran"].ToString();
            cek = ViewState["id_luaran_dijanjikan"].ToString();
            cek = ViewState["id_transaksi_kegiatan"].ToString();
            cek = ViewState["id_kelompok_luaran"].ToString();
            if (mdlLapKemajuan.insupTargetLuaran(ref dtTarget,
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()),
                    Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString()),
                    int.Parse(ViewState["id_kelompok_luaran"].ToString()),
                    int.Parse(ddlStatusKebijakan.SelectedValue)))
            {
                if (mdlKebijakan.insupTargetLuaranAkademik(
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    int.Parse(ddlJenisNaskahKebijakan.SelectedValue),
                    tbNamaLembaga.Text
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

            initData(int.Parse(ddlStatusKebijakan.SelectedValue), true);
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