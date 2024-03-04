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
    public partial class pVT : System.Web.UI.UserControl
    {
        public event EventHandler OnChildEvent;
        uiNotify noty = new uiNotify();

        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        publikasiJurnal mdlPublikasiJurnal = new publikasiJurnal();
        luaranPVT mdlPVT = new luaranPVT();

        const string FOLDER_BERKAS_LAP_KEMAJUAN = "~/fileUpload/laporan_kemajuan/BuktiLuaran/";
        const string FOLDER_BERKAS_LAP_AKHIR = "~/fileUpload/laporan_akhir/BuktiLuaran/";
        const string KD_TAHAP_LAP_AKHIR = "34";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isiDdlStatusTargetBuku();
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
                isiDdlStatusTargetBuku();
                ddlStatusPVT.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                initData(int.Parse(dt.Rows[0]["id_target_jenis_luaran"].ToString()), true);
            }
            else
            {
                isiDdlStatusTargetBuku();
                initData(int.Parse(ddlStatusPVT.SelectedValue), false);
            }
            //initData(0);
            //isiDdlStatusTargetBuku();
            //initData(int.Parse(ddlStatusPVT.SelectedValue), false);
            showHidePanelPVT();
        }

        protected void ddlStatusPVT_SelectedIndexChanged(object sender, EventArgs e)
        {
            initData(int.Parse(ddlStatusPVT.SelectedValue), true);
            showHidePanelPVT();
            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }

        private void showHidePanelPVT()
        {

            pnlNomorSurat.Visible = false;
            pnlNoSertifikat.Visible = false;
            if (ddlStatusPVT.SelectedValue == "62")
            {
                pnlNomorSurat.Visible = true;                
            }
            if (ddlStatusPVT.SelectedValue == "63")
            {
                pnlNoSertifikat.Visible = true;
            }
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListJenisDokumen(ref dt, int.Parse(ddlStatusPVT.SelectedValue));
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
                isiAtributBuku(int.Parse(dt.Rows[0]["id_target_luaran"].ToString()));

                ViewState["id_target_jenis_luaran"] = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                ViewState["id_dokumen_bukti_luaran1"] = dt.Rows[0]["id_dokumen_bukti_luaran"].ToString();
                ViewState["id_jenis_dokumen_bukti_luaran1"] = dt.Rows[0]["id_jenis_dokumen_bukti_luaran"].ToString();
                ViewState["kd_sts_unggah1"] = dt.Rows[0]["kd_sts_unggah"].ToString();

                if (dt.Rows[0]["kd_sts_unggah"].ToString() == "1")
                {
                    lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                }
                ddlStatusPVT.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
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

        private void isiAtributBuku(int id_target_luaran)
        {
            DataTable dt = new DataTable();
            mdlPVT.getDataTargetLuaranPVT(ref dt, id_target_luaran);
            if (dt.Rows.Count > 0)
            {
                tbNamaVarietas.Text = dt.Rows[0]["nama_varietas"].ToString();
                tbNamaPemuliaVarietas.Text = dt.Rows[0]["nama_pemulia_varietas"].ToString();                

                tbNoSurat.Text = dt.Rows[0]["nomor_surat_perlindungan_sementara"].ToString();
                tbTglTerbitSurat.Text = dt.Rows[0]["tgl_terbit_surat_perlindungan_sementara"].ToString() != "" ?
                    DateTime.Parse(dt.Rows[0]["tgl_terbit_surat_perlindungan_sementara"].ToString()).ToString("yyyy-MM-dd") : "";

                tbNoSertifikatPVT.Text = dt.Rows[0]["nomor_sertifikat_pvt"].ToString();
                tbTglTerbitSertifikat.Text = dt.Rows[0]["tgl_terbit_sertifikat"].ToString() != "" ?
                    DateTime.Parse(dt.Rows[0]["tgl_terbit_sertifikat"].ToString()).ToString("yyyy-MM-dd") : "";
            }
        }

        private void isiDdlStatusTargetBuku()
        {
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListTargetJenisLuaran(ref dt, 15); // 1 pvt
            ddlStatusPVT.Items.Clear();
            ddlStatusPVT.DataSource = dt;
            ddlStatusPVT.DataBind();
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
            if (tbNamaVarietas.Text.Trim().Length == 0) emptyField.Add("Nama Varietas");
            if (tbNamaPemuliaVarietas.Text.Trim().Length == 0) emptyField.Add("Nama Pemulia Varietas");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            }

            if (ddlStatusPVT.SelectedValue == "63") // bersertifikat/granted
            {
                emptyField.Clear();
                if (tbNamaVarietas.Text.Trim().Length == 0) emptyField.Add("Nama Varietas");
                if (tbNamaPemuliaVarietas.Text.Trim().Length == 0) emptyField.Add("Nama Pemulia Varietas");

                if (tbNoSertifikatPVT.Text.Trim().Length == 0) emptyField.Add("Nomor Sertifikat");
                if (tbTglTerbitSertifikat.Text.Trim().Length == 0) emptyField.Add("Tgl terbit sertifikat");

                if (emptyField.Count > 0)
                {
                    var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                    if (OnChildEvent != null)
                        OnChildEvent(sender, null);
                    return;
                }
            }

            if (ddlStatusPVT.SelectedValue == "62") // terbit surat perlindungan sementara
            {
                emptyField.Clear();
                if (tbNamaVarietas.Text.Trim().Length == 0) emptyField.Add("Nama Varietas");
                if (tbNamaPemuliaVarietas.Text.Trim().Length == 0) emptyField.Add("Nama Pemulia Varietas");

                if (tbNoSurat.Text.Trim().Length == 0) emptyField.Add("No surat");
                if (tbTglTerbitSurat.Text.Trim().Length == 0) emptyField.Add("Tgl terbit surat");

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
            int idTargetLuaran = int.Parse(ViewState["id_target_luaran"].ToString());
            Guid idLuaranDijanjikan = Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString());
            Guid idTransaksiKegiatan = Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString());
            int idKelompokLuaran = int.Parse(ViewState["id_kelompok_luaran"].ToString());
            int status = int.Parse(ddlStatusPVT.SelectedValue);


            DataTable dtTarget = new DataTable();
            if (mdlLapKemajuan.insupTargetLuaran(ref dtTarget,
                    idTargetLuaran, idLuaranDijanjikan, idTransaksiKegiatan, idKelompokLuaran, status                    
           ))

            {
                ViewState["id_target_luaran"] = dtTarget.Rows[0]["id_target_luaran"].ToString();

                if (ddlStatusPVT.SelectedValue == "62")
                {
                    if (mdlPVT.insupTargetLuaranPVT(
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    tbNamaVarietas.Text,
                    tbNamaPemuliaVarietas.Text,
                    tbNoSurat.Text,
                    tbTglTerbitSurat.Text,
                    "-",
                    "0001-01-01"))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil.");
                    }
                }

                else if (ddlStatusPVT.SelectedValue == "63")
                {
                    if (mdlPVT.insupTargetLuaranPVT(
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    tbNamaVarietas.Text,
                    tbNamaPemuliaVarietas.Text,
                    "-",
                    "0001-01-01",
                    tbNoSertifikatPVT.Text,
                    tbTglTerbitSertifikat.Text))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil.");
                    }
                }

                else
                {
                    if (mdlPVT.insupTargetLuaranPVT(
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    tbNamaVarietas.Text,
                    tbNamaPemuliaVarietas.Text,
                    "-",
                    "0001-01-01",
                    "-",
                    "0001-01-01"
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
            initData(int.Parse(ddlStatusPVT.SelectedValue), true);
            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }
        private void clearTextBox()
        {
            tbNamaVarietas.Text = string.Empty;
            tbNamaPemuliaVarietas.Text = string.Empty;
            tbNoSurat.Text = string.Empty;
            tbTglTerbitSurat.Text = string.Empty;
            tbNoSertifikatPVT.Text = string.Empty;
            tbTglTerbitSertifikat.Text = string.Empty;
        }
    }
}