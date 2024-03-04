using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using System.Data;
using System.IO;
using simlitekkes.Helper;
using simlitekkes.Models.pelaksanaan;

namespace simlitekkes.UserControls.Pengusul.luaranLapKemajuan
{
    public partial class dokHasilUjiCobaDiLingkungan : System.Web.UI.UserControl
    {
        public event EventHandler OnChildEvent;
        uiNotify noty = new uiNotify();
        Models.login objLogin;
        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        simlitekkes.Models.pelaksanaan.dokHasilUjiCoba mddokHasilUjiCoba = new simlitekkes.Models.pelaksanaan.dokHasilUjiCoba();

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
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfDok3);
        }


        public void setData(string idTransaksiKegiatan, string idLuaranDijanjikan, string strJudulForm, int idKelompokLuaran, 
            string thnPelaksanaan, int idJenisLuaran, string kdTahapanKegiatan)
        {
            ViewState["id_transaksi_kegiatan"] = idTransaksiKegiatan;
            ViewState["id_luaran_dijanjikan"] = idLuaranDijanjikan;
            ViewState["id_kelompok_luaran"] = idKelompokLuaran;
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            ViewState["kd_tahapan_kegiatan"] = kdTahapanKegiatan;
            lblJudulForm.Text = strJudulForm;

            DataTable dt = new DataTable();
            mdlLapKemajuan.cekTargetLuaran(ref dt, idTransaksiKegiatan, idKelompokLuaran, idJenisLuaran);
            if (dt.Rows.Count > 0)
            {
                isiDdlStatusDokumen(idJenisLuaran);
                ddlStatusDokumen.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                initData(int.Parse(dt.Rows[0]["id_target_jenis_luaran"].ToString()), true);
            }
            else
            {
                isiDdlStatusDokumen(idJenisLuaran);
                initData(int.Parse(ddlStatusDokumen.SelectedValue), false);
            }
            //isiDdlStatusDokumen(idJenisLuaran);
            //initData(int.Parse(ddlStatusDokumen.SelectedValue), false);
            showPanelDokumen();
        }


        private void isiDdlStatusDokumen(int pIdJenisLuaran)
        {
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListTargetJenisLuaran(ref dt, pIdJenisLuaran); // 1 publikasi jurnal internasional 
            ddlStatusDokumen.Items.Clear();
            ddlStatusDokumen.DataSource = dt;
            ddlStatusDokumen.DataBind();
            //showHidePanelPublikasi();
        }

        //public void setData(string idTransaksiKegiatan, string strJudulForm, int idKelompokLuaran,
        //    string thnPelaksanaan, string idJenisLuaran)
        //{
        //    ViewState["id_transaksi_kegiatan"] = idTransaksiKegiatan;
        //    ViewState["id_kelompok_luaran"] = idKelompokLuaran;
        //    ViewState["thn_pelaksanaan"] = thnPelaksanaan;
        //    lblJudulForm.Text = strJudulForm;
        //    ViewState["id_jenis_luaran"] = idJenisLuaran;
        //    infoUnggah();

        //}



        protected void ddlStatusDokumen_SelectedIndexChanged(object sender, EventArgs e)
        {
            //infoUnggah();
            initData(int.Parse(ddlStatusDokumen.SelectedValue), true);
            showPanelDokumen();
            //showHidePanelPublikasi();
            if (OnChildEvent != null)
                OnChildEvent(sender, null);


        }

        private void infoUnggah()
        {
            //if (ViewState["id_jenis_luaran"].ToString() == "40")
            //{
            //    lblJudulUnggah1.Text = "Dokumen kelayakan (sesuai dengan topik kajian)";
            //}
        }

        private void initData(int idTargetJenisLuaran, bool pilih = false)
        {
            lbUnduhPdfDok.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdfDok2.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdfDok3.ForeColor = System.Drawing.Color.Gray;

            int id_jenis_dokumen_bukti_luaran;
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
            };

            clearTextBox();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    id_jenis_dokumen_bukti_luaran = int.Parse(dr["id_jenis_dokumen_bukti_luaran"].ToString());

                    if (id_jenis_dokumen_bukti_luaran == 103 || id_jenis_dokumen_bukti_luaran == 106 || id_jenis_dokumen_bukti_luaran == 109 || id_jenis_dokumen_bukti_luaran == 112
                        || id_jenis_dokumen_bukti_luaran == 140 || id_jenis_dokumen_bukti_luaran == 143 || id_jenis_dokumen_bukti_luaran == 146 || id_jenis_dokumen_bukti_luaran == 149
                        || id_jenis_dokumen_bukti_luaran == 152 || id_jenis_dokumen_bukti_luaran == 155 
                        )
                    {
                        ViewState["id_dokumen_bukti_luaran1"] = dr["id_dokumen_bukti_luaran"].ToString();
                        ViewState["id_jenis_dokumen_bukti_luaran1"] = dr["id_dokumen_bukti_luaran"].ToString();
                        ViewState["kd_sts_unggah1"] = dr["kd_sts_unggah"].ToString();
                        if (dr["kd_sts_unggah"].ToString() == "1")
                        {
                            lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                        }
                    };
                    if (id_jenis_dokumen_bukti_luaran == 104 || id_jenis_dokumen_bukti_luaran == 107 || id_jenis_dokumen_bukti_luaran == 110 || id_jenis_dokumen_bukti_luaran == 113
                       || id_jenis_dokumen_bukti_luaran == 141 || id_jenis_dokumen_bukti_luaran == 144 || id_jenis_dokumen_bukti_luaran == 147 || id_jenis_dokumen_bukti_luaran == 150
                        || id_jenis_dokumen_bukti_luaran == 153 || id_jenis_dokumen_bukti_luaran == 156
                        )
                    {
                        ViewState["id_dokumen_bukti_luaran2"] = dr["id_dokumen_bukti_luaran"].ToString();
                        ViewState["id_jenis_dokumen_bukti_luaran2"] = dr["id_dokumen_bukti_luaran"].ToString();
                        ViewState["kd_sts_unggah2"] = dr["kd_sts_unggah"].ToString();
                        if (dr["kd_sts_unggah"].ToString() == "1")
                        {
                            lbUnduhPdfDok2.ForeColor = System.Drawing.Color.Red;
                        }
                    };
                    if (id_jenis_dokumen_bukti_luaran == 105 || id_jenis_dokumen_bukti_luaran == 108 || id_jenis_dokumen_bukti_luaran == 111 || id_jenis_dokumen_bukti_luaran == 114
                       || id_jenis_dokumen_bukti_luaran == 142 || id_jenis_dokumen_bukti_luaran == 145 || id_jenis_dokumen_bukti_luaran == 148 || id_jenis_dokumen_bukti_luaran == 151
                       || id_jenis_dokumen_bukti_luaran == 154 || id_jenis_dokumen_bukti_luaran == 157

                        )
                    {
                        ViewState["id_dokumen_bukti_luaran3"] = dr["id_dokumen_bukti_luaran"].ToString();
                        ViewState["id_jenis_dokumen_bukti_luaran3"] = dr["id_dokumen_bukti_luaran"].ToString();
                        ViewState["kd_sts_unggah3"] = dr["kd_sts_unggah"].ToString();
                        if (dr["kd_sts_unggah"].ToString() == "1")
                        {
                            lbUnduhPdfDok3.ForeColor = System.Drawing.Color.Red;
                        }
                    };
                    
                }
                ViewState["id_target_luaran"] = dt.Rows[0]["id_target_luaran"].ToString();
                isiAtributDokumen(int.Parse(dt.Rows[0]["id_target_luaran"].ToString()));

                ViewState["id_target_jenis_luaran"] = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                //ddlStatusDokumen.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();

                if (!pilih)
                {
                    ddlStatusDokumen.SelectedValue = dt.Rows[0]["id_target_jenis_luaran"].ToString();
                }
            }

            else
            {
                ViewState["id_target_luaran"] = -1;
                ViewState["id_dokumen_bukti_luaran1"] = Guid.NewGuid();
                ViewState["id_dokumen_bukti_luaran2"] = Guid.NewGuid();
                ViewState["id_dokumen_bukti_luaran3"] = Guid.NewGuid();
            }
            
        }

        private void isiAtributDokumen(int id_target_luaran)
        {
            DataTable dt = new DataTable();
            mddokHasilUjiCoba.getDataTargetLuaranDokHasilUjiCoba(ref dt, id_target_luaran);
            if (dt.Rows.Count > 0)
            {
                //ddlStatusDokumen.SelectedValue = dt.Rows[0]["id_target_luaran"].ToString();
                tbNamaProduk.Text = dt.Rows[0]["nama_produk"].ToString();

                tbLinkVideo.Text = dt.Rows[0]["link_dokumentasi"].ToString();

                tbTglPengujianProduk.Text = dt.Rows[0]["tgl_pengujian_produk"].ToString() != "" ?
                    DateTime.Parse(dt.Rows[0]["tgl_pengujian_produk"].ToString()).ToString("yyyy-MM-dd") : "";
                
            }
        }

        private void clearTextBox()
        {
            tbNamaProduk.Text = string.Empty;

            tbLinkVideo.Text = string.Empty;
            tbTglPengujianProduk.Text = string.Empty;
           

        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            List<string> emptyField = new List<string>();
            if (tbNamaProduk.Text.Trim().Length == 0) emptyField.Add("Nama Produk");
            if (tbLinkVideo.Text.Trim().Length == 0) emptyField.Add("Link Pengujian");
            if (tbTglPengujianProduk.Text.Trim().Length == 0) emptyField.Add("Tanggal Pengujian");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            };
            bool file1 = cekFile(ref fileUpload1, int.Parse(ViewState["ukuran_file1_max"].ToString()) * 1024);
            if (!file1 && lbUnduhPdfDok.ForeColor != System.Drawing.Color.Red)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", " File "+ lblJudulUnggah1.Text + " belum di pilih");
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            };
            bool file2 = cekFile(ref fileUpload2, int.Parse(ViewState["ukuran_file2_max"].ToString()) * 1024);
            if (!file2 && lbUnduhPdfDok2.ForeColor != System.Drawing.Color.Red)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", " File " + lblJudulUnggah2.Text + " belum di pilih");
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            };
            bool file3 = cekFile(ref fileUpload3, int.Parse(ViewState["ukuran_file3_max"].ToString()) * 1024);
            if (!file3 && lbUnduhPdfDok3.ForeColor != System.Drawing.Color.Red)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", " File " + lblJudulUnggah3.Text + " belum di pilih");
                if (OnChildEvent != null)
                    OnChildEvent(sender, null);
                return;
            };


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
                    int.Parse(ddlStatusDokumen.SelectedValue)
           ))
            {
                ViewState["id_target_luaran"] = dtTarget.Rows[0]["id_target_luaran"].ToString();


                //int.Parse(tbNomor.Text),
                //int.Parse(tbHalamanAwal.Text),

                //int.Parse(tbHalamanAkhir.Text),

                if (mddokHasilUjiCoba.insupTargetDokHasilUjiCoba(
                    int.Parse(ViewState["id_target_luaran"].ToString()),

                    tbNamaProduk.Text,
                    tbLinkVideo.Text,                                        
                    tbTglPengujianProduk.Text

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
            //if (jmlDokPendukung > 0)
            //{
            //bool file1 = cekFile(ref fileUpload1, int.Parse(ViewState["ukuran_file1_max"].ToString()) * 1024);
            if (file1)
            {
                if (ViewState["id_dokumen_bukti_luaran1"] == null)
                {
                    ViewState["id_dokumen_bukti_luaran1"] = Guid.NewGuid().ToString();
                }
                prosesUnggah(ref fileUpload1, dir, ViewState["id_dokumen_bukti_luaran1"].ToString());
                mdlLapKemajuan.insupDokumenBuktiLuaran(Guid.Parse(ViewState["id_dokumen_bukti_luaran1"].ToString()),
                    int.Parse(ViewState["id_target_luaran"].ToString()),
                    int.Parse(ViewState["id_jenis_dokumen_bukti_luaran1"].ToString()),
                    "1");
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah dokumen berhasil.");
            }


            //}
            //if (jmlDokPendukung > 1)
            //{
            //bool file2 = cekFile(ref fileUpload2, int.Parse(ViewState["ukuran_file2_max"].ToString()) * 1024);
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
            //}

            //bool file3 = cekFile(ref fileUpload3, int.Parse(ViewState["ukuran_file3_max"].ToString()) * 1024);
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

            initData(int.Parse(ddlStatusDokumen.SelectedValue), true);
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

        private void showPanelDokumen()
        {
            DataTable dt = new DataTable();
            mdlLapKemajuan.ListJenisDokumen(ref dt, int.Parse(ddlStatusDokumen.SelectedValue));
            ViewState["jml_dokumen_pendukung"] = dt.Rows.Count;
            int id_jenis_dokumen_bukti_luaran;

            foreach (DataRow dr in dt.Rows)
            {
                id_jenis_dokumen_bukti_luaran = int.Parse(dr["id_jenis_dokumen_bukti_luaran"].ToString());

                if (id_jenis_dokumen_bukti_luaran == 103 || id_jenis_dokumen_bukti_luaran == 106 || id_jenis_dokumen_bukti_luaran == 109 || id_jenis_dokumen_bukti_luaran == 112
                        || id_jenis_dokumen_bukti_luaran == 140 || id_jenis_dokumen_bukti_luaran == 143 || id_jenis_dokumen_bukti_luaran == 146 || id_jenis_dokumen_bukti_luaran == 149
                        || id_jenis_dokumen_bukti_luaran == 152 || id_jenis_dokumen_bukti_luaran == 155

                    )
                {
                    lblJudulUnggah1.Text = dr["jenis_dokumen_bukti_luaran"].ToString();
                    lblInfoFileUnggah1.Text = "(UkuranFile Maksimal " + (int.Parse(dr["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString() + "MB dengan format PDF)";
                    ViewState["ukuran_file1_max"] = (int.Parse(dr["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                    ViewState["id_jenis_dokumen_bukti_luaran1"] = dr["id_jenis_dokumen_bukti_luaran"].ToString();
                };
                if (id_jenis_dokumen_bukti_luaran == 104 || id_jenis_dokumen_bukti_luaran == 107 || id_jenis_dokumen_bukti_luaran == 110 || id_jenis_dokumen_bukti_luaran == 113
                        || id_jenis_dokumen_bukti_luaran == 141 || id_jenis_dokumen_bukti_luaran == 144 || id_jenis_dokumen_bukti_luaran == 147 || id_jenis_dokumen_bukti_luaran == 150
                        || id_jenis_dokumen_bukti_luaran == 153 || id_jenis_dokumen_bukti_luaran == 156
                    )
                {
                    lblJudulUnggah2.Text = dr["jenis_dokumen_bukti_luaran"].ToString();
                    lblInfoFileUnggah2.Text = "(UkuranFile Maksimal " + (int.Parse(dr["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString() + "MB dengan format PDF)";
                    ViewState["ukuran_file2_max"] = (int.Parse(dr["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                    ViewState["id_jenis_dokumen_bukti_luaran2"] = dr["id_jenis_dokumen_bukti_luaran"].ToString();
                };
                if (id_jenis_dokumen_bukti_luaran == 105 || id_jenis_dokumen_bukti_luaran == 108 || id_jenis_dokumen_bukti_luaran == 111 || id_jenis_dokumen_bukti_luaran == 114
                        || id_jenis_dokumen_bukti_luaran == 142 || id_jenis_dokumen_bukti_luaran == 145 || id_jenis_dokumen_bukti_luaran == 148 || id_jenis_dokumen_bukti_luaran == 151
                        || id_jenis_dokumen_bukti_luaran == 154 || id_jenis_dokumen_bukti_luaran == 157

                    )
                {
                    lblJudulUnggah3.Text = dr["jenis_dokumen_bukti_luaran"].ToString();
                    lblInfoFileUnggah3.Text = "(UkuranFile Maksimal " + (int.Parse(dr["ukuran_dokumen_maks_kbyte"].ToString()) / 1000).ToString() + "MB dengan format PDF)";
                    ViewState["ukuran_file3_max"] = (int.Parse(dr["ukuran_dokumen_maks_kbyte"].ToString())).ToString();
                    ViewState["id_jenis_dokumen_bukti_luaran3"] = dr["id_jenis_dokumen_bukti_luaran"].ToString();
                };
                
            }
            
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
    }
}