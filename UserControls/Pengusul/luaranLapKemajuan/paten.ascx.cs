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
    public partial class paten : System.Web.UI.UserControl
    {
        simlitekkes.Models.pelaksanaan.luaranLaporanKemajuan mdlLapKemajuan = new simlitekkes.Models.pelaksanaan.luaranLaporanKemajuan();
        simlitekkes.Models.pelaksanaan.luaranPaten mdlPaten = new simlitekkes.Models.pelaksanaan.luaranPaten();

        public event EventHandler OnChildEvent;
        uiNotify noty = new uiNotify();

        const int ID_JENIS_LUARAN_PATEN = 8;
       
        const int DRAFT_PATEN = 64;
        const int DRAFT_PATEN_SEDERHANA = 68;

        const int GRANTED_PATEN = 67;
        const int GRANTED_PATEN_SEDERHANA = 71;

        const int TERDAFTAR_PATEN = 66;
        const int TERDAFTAR_PATEN_SEDERHANA = 70;

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
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.ddlStatusPaten);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok1);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok2);

            if (!IsPostBack)
            {
                //isiDdlStatusPaten();
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
                isiDdlStatusPaten();
                ddlStatusPaten.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                initData(int.Parse(dt.Rows[0]["id_target_jenis_luaran"].ToString()), true);
            }
            else
            {
                isiDdlStatusPaten();
                initData(int.Parse(ddlStatusPaten.SelectedValue), false);
            }

            //isiDdlStatusPaten();
            //initData(int.Parse(ddlStatusPaten.SelectedValue), false);
            showHidePanel();
        }

        private void isiDdlStatusPaten()
        {
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListTargetJenisLuaran(ref dt, idJenisLuaran);
            ddlStatusPaten.Items.Clear();
            ddlStatusPaten.DataSource = dt;
            ddlStatusPaten.DataBind();
        }

        protected void ddlStatusPaten_SelectedIndexChanged(object sender, EventArgs e)
        {
            initData(int.Parse(ddlStatusPaten.SelectedValue), true);
            showHidePanel();

            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }

        private void showHidePanel()
        {
            // Show Hide Label Isian

            if (idJenisLuaran == ID_JENIS_LUARAN_PATEN)
            {
                lblStatusPaten.Text = "Status paten";
                lblNamaPaten.Text = "Nama paten";
                lblPemegangPaten.Text = "Pemegang paten";
            }
            else
            {
                lblStatusPaten.Text = "Status paten sederhana";
                lblNamaPaten.Text = "Nama paten sederhana";
                lblPemegangPaten.Text = "Pemegang paten sederhana";
            }

            // Show Hide Panel Isian
            if (int.Parse(ddlStatusPaten.SelectedValue) == TERDAFTAR_PATEN ||
                int.Parse(ddlStatusPaten.SelectedValue) == TERDAFTAR_PATEN_SEDERHANA)
            {
                pnlNoPendaftaran.Visible = true;
                pnlGranted.Visible = false;
            }
            else if (int.Parse(ddlStatusPaten.SelectedValue) == GRANTED_PATEN ||
                int.Parse(ddlStatusPaten.SelectedValue) == GRANTED_PATEN_SEDERHANA)
            {
                pnlNoPendaftaran.Visible = false;
                pnlGranted.Visible = true;
            }
            else
            {
                pnlNoPendaftaran.Visible = false;
                pnlGranted.Visible = false;
            }

            // Show Hide Panel Unggah
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListJenisDokumen(ref dt, int.Parse(ddlStatusPaten.SelectedValue));
            ViewState["jml_dokumen_pendukung"] = dt.Rows.Count;

            if (int.Parse(ddlStatusPaten.SelectedValue) == DRAFT_PATEN ||
                int.Parse(ddlStatusPaten.SelectedValue) == DRAFT_PATEN_SEDERHANA)
            {
                pnlDokumen2.Visible = false;

                if (dt.Rows.Count > 0)
                {
                    lblNamaJenisDokumen1.Text = dt.Rows[0]["jenis_dokumen_bukti_luaran"].ToString();
                    lblUkuranMaksDok1.Text = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString();
                    ViewState["ukuran_file1_max"] = (int.Parse(dt.Rows[0]["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                    ViewState["id_jenis_dokumen_bukti_luaran1"] = dt.Rows[0]["id_jenis_dokumen_bukti_luaran"].ToString();
                }
            }
            else
            {
                pnlDokumen2.Visible = true;

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
            }
        }

        private void initData(int idTargetJenisLuaran, bool pilih = false)
        {
            lbUnduhPdfDok1.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdfDok2.ForeColor = System.Drawing.Color.Gray;

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
            if (int.Parse(ddlStatusPaten.SelectedValue) == DRAFT_PATEN ||
                int.Parse(ddlStatusPaten.SelectedValue) == DRAFT_PATEN_SEDERHANA)
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
                    ddlStatusPaten.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                }

                if (dt.Rows.Count <= 0)
                {
                    ViewState["id_target_luaran"] = -1;
                    ViewState["id_dokumen_bukti_luaran1"] = Guid.NewGuid();
                }
            }
            else
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
                    ddlStatusPaten.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
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
        }

        private void isiAtribut(int id_target_luaran)
        {
            DataTable dt = new DataTable();
            mdlPaten.getDataTargetLuaranPaten(ref dt, id_target_luaran);
            if (dt.Rows.Count > 0)
            {
                tbNamaPaten.Text = dt.Rows[0]["nama_paten"].ToString();
                tbPemegangPaten.Text = dt.Rows[0]["pemegang_paten"].ToString();
                tbNomorPendaftaran.Text = dt.Rows[0]["no_pendaftaran"].ToString();
                tbNoGrantedSertifikat.Text = dt.Rows[0]["no_granted_sertifikat"].ToString();
                tbTglPencatatan.Text = dt.Rows[0]["tgl_pencatatan"].ToString() != "" ?
                DateTime.Parse(dt.Rows[0]["tgl_pencatatan"].ToString()).ToString("yyyy-MM-dd") : "";
            }
            else
            {
                clearTextBox();
            }
        }

        private void clearTextBox()
        {
            tbNamaPaten.Text = string.Empty;
            tbPemegangPaten.Text = string.Empty;
            tbNomorPendaftaran.Text = string.Empty;
            tbNoGrantedSertifikat.Text = string.Empty;
            tbTglPencatatan.Text = string.Empty;
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
            if (tbNamaPaten.Text.Trim().Length == 0) emptyField.Add("Nama paten");
            if (tbPemegangPaten.Text.Trim().Length == 0) emptyField.Add("Pemegang paten");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            }

            if (ddlStatusPaten.SelectedValue == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Status paten belum dipilih");
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            }
            else
            {
                if (int.Parse(ddlStatusPaten.SelectedValue) == TERDAFTAR_PATEN ||
                int.Parse(ddlStatusPaten.SelectedValue) == TERDAFTAR_PATEN_SEDERHANA)
                {
                    emptyField.Clear();
                    if (tbNamaPaten.Text.Trim().Length == 0) emptyField.Add("Nama paten");
                    if (tbPemegangPaten.Text.Trim().Length == 0) emptyField.Add("Pemegang paten");
                    if (tbNomorPendaftaran.Text.Trim().Length == 0) emptyField.Add("Nomor pendaftaran");

                    if (emptyField.Count > 0)
                    {
                        var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                        if (OnChildEvent != null)
                            OnChildEvent(sender, null);
                        return;
                    }
                }

                if (int.Parse(ddlStatusPaten.SelectedValue) == GRANTED_PATEN ||
                    int.Parse(ddlStatusPaten.SelectedValue) == GRANTED_PATEN_SEDERHANA)
                {
                    emptyField.Clear();
                    if (tbNamaPaten.Text.Trim().Length == 0) emptyField.Add("Nama paten");
                    if (tbPemegangPaten.Text.Trim().Length == 0) emptyField.Add("Pemegang paten");
                    if (tbNoGrantedSertifikat.Text.Trim().Length == 0) emptyField.Add("Nomor granted");
                    if (tbTglPencatatan.Text.Trim().Length == 0) emptyField.Add("Tanggal pencatatan");

                    if (emptyField.Count > 0)
                    {
                        var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                        if (OnChildEvent != null)
                            OnChildEvent(sender, null);
                        return;
                    }
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
                        int.Parse(ddlStatusPaten.SelectedValue)))
                {
                    ViewState["id_target_luaran"] = dtTarget.Rows[0]["id_target_luaran"].ToString();
                    if (int.Parse(ddlStatusPaten.SelectedValue) == TERDAFTAR_PATEN ||
                        int.Parse(ddlStatusPaten.SelectedValue) == TERDAFTAR_PATEN_SEDERHANA)
                    {
                        if (mdlPaten.insupTargetLuaranPaten(
                            int.Parse(ViewState["id_target_luaran"].ToString()),
                            tbNamaPaten.Text, tbPemegangPaten.Text, tbNomorPendaftaran.Text,
                            "-", "0001-01-01", int.Parse(ddlStatusPaten.SelectedValue)
                            ))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil.");
                        }
                    }
                    else if (int.Parse(ddlStatusPaten.SelectedValue) == GRANTED_PATEN ||
                        int.Parse(ddlStatusPaten.SelectedValue) == GRANTED_PATEN_SEDERHANA)
                    {
                        if (mdlPaten.insupTargetLuaranPaten(
                            int.Parse(ViewState["id_target_luaran"].ToString()),
                            tbNamaPaten.Text, tbPemegangPaten.Text, "-",
                            tbNoGrantedSertifikat.Text, tbTglPencatatan.Text, int.Parse(ddlStatusPaten.SelectedValue)
                            ))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil.");
                        }
                    }
                    else
                    {
                        if (mdlPaten.insupTargetLuaranPaten(
                            int.Parse(ViewState["id_target_luaran"].ToString()),
                            tbNamaPaten.Text, tbPemegangPaten.Text,
                            "-", "-", "0001-01-01", int.Parse(ddlStatusPaten.SelectedValue)
                            ))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil.");
                        }
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

                initData(int.Parse(ddlStatusPaten.SelectedValue), true);
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
            }
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