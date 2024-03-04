using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using System.Data;
using System.IO;
using simlitekkes.Core;
using simlitekkes.Helper;
using iText.Kernel.Pdf;
using iText.IO.Source;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class laporanKemajuan : System.Web.UI.UserControl
    {
        Models.Pengusul.lapKemajuan modelLapKemajuan = new Models.Pengusul.lapKemajuan();
        UIControllers.uiGridView obj_uiGridView = new UIControllers.uiGridView();
        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();

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
                isiDdlThnPelaksanaan();
                string page = Session["page"].ToString();
                if (page == "4")
                {
                    lblJudulForm.Text = "LAPORAN KEMAJUAN";
                    lblJudulHeader.Text = "LAPORAN KEMAJUAN";
                    lblInfo1FormUnggah.Text = "Unggah dokumen substansi laporan kemajuan dalam format PDF sesuai dengan template yang disediakan";
                    lblInfo2FormUnggah.Text = "Dokumen substansi laporan kemajuan";

                    isiGvLapKemajuan();
                }
                //else if (page == "5")
                //{
                //    lblJudulForm.Text = "LAPORAN AKHIR";
                //    lblJudulHeader.Text = "LAPORAN AKHIR";
                //    lblInfo1FormUnggah.Text = "Unggah dokumen substansi laporan akhir dalam format PDF sesuai dengan template yang disediakan";
                //    lblInfo2FormUnggah.Text = "Dokumen substansi laporan akhir";
                //    isiGvLapAkhir();
                //}
                mvMain.SetActiveView(vDaftarLapKemajuan);
            }
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnggahDokumen);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhTemplateDok);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnggahDokumenMitra);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbUnduhPdfMitra);

            ktLuaranPublikasi.OnChildEvent += new EventHandler(ChildEvent_publikasi);
            ktKonferensi.OnChildEvent += new EventHandler(ChildEvent_publikasi);
            ktKeyNoteSpeaker.OnChildEvent += new EventHandler(ChildEvent_keyNoteSpeaker);
            ktBukuAjar.OnChildEvent += new EventHandler(ChildEvent_bukuAjar);
            ktVisitingLecturer.OnChildEvent += new EventHandler(ChildEvent_visitingLecturer);
            ktBookChapter.OnChildEvent += new EventHandler(ChildEvent_bookChapter);
            ktBukuHasilPenelitian.OnChildEvent += new EventHandler(ChildEvent_bukuHasilPenelitian);
            ktDesain.OnChildEvent += new EventHandler(ChildEvent_desain);
            ktDesainProduk.OnChildEvent += new EventHandler(ChildEvent_desainProduk);
            ktDesainProdukIndustri.OnChildEvent += new EventHandler(ChildEvent_desainProdukIndustri);
            ktDokPurwarupa.OnChildEvent += new EventHandler(ChildEvent_dokPurwarupa);
            ktDokBisnisPlan.OnChildEvent += new EventHandler(ChildEvent_dokBisnisPlan);
            ktDokHasilUjiCobaLingkungan.OnChildEvent += new EventHandler(ChildEvent_dokHasilUjiCobaLingkungan);
            ktDokPengujianPurwarupa.OnChildEvent += new EventHandler(ChildEvent_dokPengujianPurwarupa);
            ktPVT.OnChildEvent += new EventHandler(ChildEvent_PVT);
            ktHakCipta.OnChildEvent += new EventHandler(ChildEvent_hakCipta);
            ktTTG.OnChildEvent += new EventHandler(ChildEvent_TTG);
            ktRS.OnChildEvent += new EventHandler(ChildEvent_RS);
            ktPaten.OnChildEvent += new EventHandler(ChildEvent_bukuAjar);
            ktStrategi.OnChildEvent += new EventHandler(ChildEvent_Strategi);
            ktSistem.OnChildEvent += new EventHandler(ChildEvent_Sistem);
            ktProduk.OnChildEvent += new EventHandler(ChildEvent_Produk);
            ktKebijakan.OnChildEvent += new EventHandler(ChildEvent_Kebijakan);
            ktPurwarupaPrototipe.OnChildEvent += new EventHandler(ChildEvent_PurwarupaPrototipe);
            ktMetode.OnChildEvent += new EventHandler(ChildEvent_metode);
            ktPurwarupaLaikIndustri.OnChildEvent += new EventHandler(ChildEvent_PurwarupaLaikIndustri);
            ktModel.OnChildEvent += new EventHandler(ChildEvent_model);
            ktKaryaSeni.OnChildEvent += new EventHandler(ChildEvent_model);
            ktNaskahAkademik.OnChildEvent += new EventHandler(ChildEvent_model);
            ktmerekDagang.OnChildEvent += new EventHandler(ChildEvent_model);
            ktindikasiGeografis.OnChildEvent += new EventHandler(ChildEvent_model);
            ktluaranLain.OnChildEvent += new EventHandler(ChildEvent_model);
        }

        #region Daftar Laporan Kemajuan


        void ChildEvent_publikasi(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_keyNoteSpeaker(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_bukuAjar(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_visitingLecturer(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_bookChapter(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_bukuHasilPenelitian(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_desain(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }
        void ChildEvent_desainProduk(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }
        void ChildEvent_desainProdukIndustri(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }
        void ChildEvent_dokPurwarupa(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }
        void ChildEvent_dokBisnisPlan(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }
        void ChildEvent_dokHasilUjiCobaLingkungan(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }
        void ChildEvent_dokPengujianPurwarupa(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }
        void ChildEvent_PVT(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }
        void ChildEvent_hakCipta(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_TTG(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_RS(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_paten(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_Strategi(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_Sistem(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_Produk(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_Kebijakan(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_PurwarupaPrototipe(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_metode(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_PurwarupaLaikIndustri(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        void ChildEvent_model(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        private void isiGvLapKemajuan()
        {
            Guid idPersonal = Guid.Parse(objLogin.idPersonal);
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;

            DataTable dt = new DataTable();
            if (modelLapKemajuan.getDaftarLaporanKemajuan(ref dt, idPersonal, thnPelaksanaanKegiatan))
            {
                gvLapKemajuan.DataSource = dt;
                gvLapKemajuan.DataBind();
            }
        }

        //private void isiGvLapAkhir()
        //{
        //    Guid idPersonal = Guid.Parse(objLogin.idPersonal);
        //    string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;

        //    DataTable dt = new DataTable();
        //    if (modelLapKemajuan.getDaftarLaporanAkhir(ref dt, idPersonal, thnPelaksanaanKegiatan))
        //    {
        //        gvLapKemajuan.DataSource = dt;
        //        gvLapKemajuan.DataBind();
        //    }
        //}

        private void isiDdlThnPelaksanaan()
        {
            //ddlThnPelaksanaan.Items.Clear();
            //int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            //for (int i = thnSKg; i >= 2019; i--)
            //{
            //    ddlThnPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            //}

            ddlThnPelaksanaan.Items.Clear();
            for (int i = DateTime.Now.Year + 1; i >= 2020; i--)
            {
                ddlThnPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }

        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string page = Session["page"].ToString();
            if (page == "4")
            {
                isiGvLapKemajuan();
            }
            //else if (page == "5")
            //{
            //    isiGvLapAkhir();
            //}
        }

        protected void gvLapKemajuan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblSkemaIsian.Text = gvLapKemajuan.DataKeys[e.RowIndex]["nama_skema"].ToString();
            lblThnPelaksanaanIsian.Text = ddlThnPelaksanaan.SelectedItem.ToString();
            lblTahunKeIsian.Text = gvLapKemajuan.DataKeys[e.RowIndex]["urutan_thn_usulan_kegiatan"].ToString();
            lblDurasiIsian.Text = gvLapKemajuan.DataKeys[e.RowIndex]["lama_kegiatan"].ToString();
            lblJudulIsian.Text = gvLapKemajuan.DataKeys[e.RowIndex]["judul"].ToString();
            tbRingkasan.Text = gvLapKemajuan.DataKeys[e.RowIndex]["ringkasan"].ToString();
            tbKeyword.Text = gvLapKemajuan.DataKeys[e.RowIndex]["keyword"].ToString();

            ViewState["idUsulanKegiatan"] = gvLapKemajuan.DataKeys[e.RowIndex]["id_usulan_kegiatan"].ToString();
            ViewState["idTransaksiKegiatan"] = gvLapKemajuan.DataKeys[e.RowIndex]["id_transaksi_kegiatan"].ToString();
            ViewState["kd_sts_pelaksanaan"] = gvLapKemajuan.DataKeys[e.RowIndex]["kd_sts_pelaksanaan"].ToString();
            ViewState["kd_tahapan_kegiatan"] = gvLapKemajuan.DataKeys[e.RowIndex]["kd_tahapan_kegiatan"].ToString();
            int level_tkt_target = int.Parse(gvLapKemajuan.DataKeys[e.RowIndex]["level_tkt_target"].ToString());
            int idKategoriRiset = int.Parse(gvLapKemajuan.DataKeys[e.RowIndex]["id_kategori_riset"].ToString());

            ViewState["idSkema"] = int.Parse(gvLapKemajuan.DataKeys[e.RowIndex]["id_skema"].ToString());
            ViewState["urutanThnUsulanKegiatan"] = int.Parse(gvLapKemajuan.DataKeys[e.RowIndex]["urutan_thn_usulan_kegiatan"].ToString());

            isiGvLuaranWajib();
            isiGvLuaranTambahan();
            mvMain.SetActiveView(vIsianLapKemajuan);
            cekFileSdhDiunggah(ViewState["idTransaksiKegiatan"].ToString());

            // Show hide panel dokumen mitra berdasarkan level target TKT
            if (idKategoriRiset > 1)
            {
                pnlDokumenMitra.Visible = true;
                cekFileSdhDiunggahDokumenMitra(ViewState["idTransaksiKegiatan"].ToString());
            }
            else
            {
                pnlDokumenMitra.Visible = false;
            }
        }

        protected void gvLapKemajuan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id_transaksi_kegiatan = gvLapKemajuan.DataKeys[rowIndex]["id_transaksi_kegiatan"].ToString();
            if (e.CommandName == "unduhLaporanKemajuan")
            {
                ktPdfLaporanKemajuanKontrol.unduhLapKemajuan(id_transaksi_kegiatan);
            }
        }

        protected void gvLapKemajuan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_pelaksanaan = gvLapKemajuan.DataKeys[e.Row.RowIndex]["kd_sts_pelaksanaan"].ToString();
                string id_transaksi_kegiatan = gvLapKemajuan.DataKeys[e.Row.RowIndex]["id_transaksi_kegiatan"].ToString();
                LinkButton lbUnduhLaporanKemajuan = new LinkButton();
                lbUnduhLaporanKemajuan = (LinkButton)e.Row.FindControl("lbUnduhLaporanKemajuan");
                lbUnduhLaporanKemajuan.ForeColor = System.Drawing.Color.Gray;
                lbUnduhLaporanKemajuan.Enabled = false;
                //bool sdhDiunggah = cekFileSdhDiunggah(id_transaksi_kegiatan);

                //if (kd_sts_pelaksanaan == "1" && sdhDiunggah)
                if (kd_sts_pelaksanaan == "1")
                {
                    lbUnduhLaporanKemajuan.ForeColor = System.Drawing.Color.Red;
                    lbUnduhLaporanKemajuan.Enabled = true;
                }
            }
        }

        #endregion

        #region Isian Laporan Kemajuan

        protected void lbUnduhTemplateDok_Click(object sender, EventArgs e)
        {
            string namaFileTemplate = "template_laporan_kemajuan.docx";
            string path = "dokumen/template/" + namaFileTemplate;
            string page = Session["page"].ToString();
            if (page == "5")
            {
                namaFileTemplate = "template_laporan_akhir.docx";
                path = "dokumen/template/" + namaFileTemplate;
            }

            if (File.Exists(Server.MapPath(path)))
            {
                Response.Redirect(path);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan");
            }
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            bool isFileSdhDipilih = fileUpload1.HasFile;
            if (ddlThnPelaksanaan.SelectedValue == "0000")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tahun pelaksanaan belum dipilih");
            }
            else if (isFileSdhDipilih)
            {
                if (fileUpload1.FileName.ToLower().EndsWith(".pdf"))
                {
                    if (fileUpload1.PostedFile.ContentLength < (5 * 1024 * 1024))
                    {
                        prosesUnggah(Guid.Parse(ViewState["idTransaksiKegiatan"].ToString()));
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File yang akan diunggah ukurannya tidak boleh melebihi 5 MByte !!!");
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
            cekFileSdhDiunggah(ViewState["idTransaksiKegiatan"].ToString());
        }

        private void prosesUnggah(Guid id_transaksi_kegiatan)
        {
            string dir = "~/fileUpload/laporan_kemajuan";
            if (!Directory.Exists(Server.MapPath(dir)))
            {
                Directory.CreateDirectory(Server.MapPath(dir));
            }

            dir = "~/fileUpload/laporan_akhir";
            if (!Directory.Exists(Server.MapPath(dir)))
            {
                Directory.CreateDirectory(Server.MapPath(dir));
            }

            string path = string.Format("~/fileUpload/laporan_kemajuan/{0}/", ddlThnPelaksanaan.SelectedValue);

            string page = Session["page"].ToString();
            if (page == "5")
            {
                path = string.Format("~/fileUpload/laporan_akhir/{0}/", ddlThnPelaksanaan.SelectedValue);
            }

            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

            string namaFile = string.Format("{0}{1}.pdf", path.ToString(), id_transaksi_kegiatan.ToString());

            if (fileUpload1.HasFile)
            {
                if (fileUpload1.FileName.EndsWith(".pdf") || fileUpload1.FileName.EndsWith(".PDF"))
                {
                    fileUpload1.SaveAs(Server.MapPath(namaFile));
                    modelLapKemajuan.updateStsPelaksanaan(id_transaksi_kegiatan); // update sts pelaksanaan transaksi kegiatan
                    modelLapKemajuan.insertDataUnggahDokumen(id_transaksi_kegiatan); // insert histori unggah

                    // sebelumnya hanya tahun terakhir
                    //if (int.Parse(lblTahunKeIsian.Text.Trim()) == int.Parse(lblDurasiIsian.Text.Trim()))
                    //{
                    if (modelLapKemajuan.cekApakahSudahAdaTahapLaporanAkhir(Guid.Parse(ViewState["idUsulanKegiatan"].ToString())) == false)
                    {
                        modelLapKemajuan.generateTahapLaporanAkhir(Guid.Parse(ViewState["idUsulanKegiatan"].ToString()));
                    }
                    //}

                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Unggah template substansi laporan kemajuan berhasil.");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
        }

        protected void lbKembaliIsian_Click(object sender, EventArgs e)
        {
            string page = Session["page"].ToString();
            if (page == "4")
            {
                isiGvLapKemajuan();
            }
            //else if (page == "5")
            //{
            //    isiGvLapAkhir();
            //}
            mvMain.SetActiveView(vDaftarLapKemajuan);
        }

        protected void lbSimpanRingkasan_Click(object sender, EventArgs e)
        {
            if (!modelLapKemajuan.insupRingkasan(Guid.Parse(ViewState["idTransaksiKegiatan"].ToString()), tbRingkasan.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelLapKemajuan.errorMessage);
                return;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan ringkasan berhasil");
            }

        }

        protected void lbSimpanKeyword_Click(object sender, EventArgs e)
        {
            if (!modelLapKemajuan.insupKeyword(Guid.Parse(ViewState["idTransaksiKegiatan"].ToString()), tbKeyword.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelLapKemajuan.errorMessage);
                return;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan keyword berhasil");
            }
        }

        private void isiGvLuaranWajib()
        {
            DataTable dt = new DataTable();
            if (modelLapKemajuan.getDaftarLuaranWajib(ref dt, Guid.Parse(ViewState["idUsulanKegiatan"].ToString())))
            {
                gvLuaranWajib.DataSource = dt;
                gvLuaranWajib.DataBind();
            }
        }

        private void isiGvLuaranTambahan()
        {
            DataTable dt = new DataTable();
            if (modelLapKemajuan.getDaftarLuaranTambahan(ref dt, Guid.Parse(ViewState["idUsulanKegiatan"].ToString())))
            {
                gvLuaranTambahan.DataSource = dt;
                gvLuaranTambahan.DataBind();
            }
        }

        protected void gvLuaranWajib_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id_jenis_luaran = gvLuaranWajib.DataKeys[e.RowIndex]["id_jenis_luaran"].ToString();
            ViewState["idLuaranDijanjikan"] = gvLuaranWajib.DataKeys[e.RowIndex]["id_luaran_dijanjikan"].ToString();
            ViewState["idTargetCapaianLuaran"] = gvLuaranWajib.DataKeys[e.RowIndex]["id_target_capaian_luaran"].ToString();

            pilihJenisLuaran(id_jenis_luaran, 1);
            lblModalTitle.Text = "LUARAN WAJIB";
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        protected void gvLuaranWajib_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string arr_id_dokumen_bukti_luaran = gvLuaranWajib.DataKeys[rowIndex]["arr_id_dokumen_bukti_luaran"].ToString();
            if (e.CommandName == "UnduhLuaran")
            {
                Char[] splitter =  { ','};
                string []arrayIdDokumen= arr_id_dokumen_bukti_luaran.Split(splitter);

                if(arrayIdDokumen.Length > 0)
                {
                    unduhPDFBuktiLuaran(arrayIdDokumen, "wajib");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Belum ada Dokumen yang diunggah");
                }
            }
        }


        private void unduhPDFBuktiLuaran(string[] arrayIdDokumen, string kelompokLuaran)
        {

            ByteArrayOutputStream ms = new ByteArrayOutputStream();
            PdfDocument pdocDepan = new PdfDocument(new PdfWriter(ms));
            pdocDepan.SetDefaultPageSize(new PageSize(PageSize.A4));
            for (int a = 0; a < arrayIdDokumen.Length; a++)
            {
                PdfDocument pdfDokumenBuktiLuaran = getPdfBuktiLuaran(arrayIdDokumen[a], ddlThnPelaksanaan.SelectedItem.Text);
                if (pdfDokumenBuktiLuaran != null)
                    pdfDokumenBuktiLuaran.CopyPagesTo(1, pdfDokumenBuktiLuaran.GetNumberOfPages(), pdocDepan);
            }

            Document document = new Document(pdocDepan);
            document.Close();
            downloadPdf(ms.ToArray(), kelompokLuaran);

        }

        void downloadPdf(byte[] bytes, string attrFile)
        {
            string namaFile = "DokumenBuktiLuaran " + attrFile + ".pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.End();
        }

        protected PdfDocument getPdfBuktiLuaran(string pIdDokumenBuktiLuaran, string thnPelaksanaan)
        {
            string filePath = FOLDER_BERKAS_LAP_KEMAJUAN + thnPelaksanaan + "/" +
                    pIdDokumenBuktiLuaran + ".pdf";

            if (!File.Exists(Server.MapPath(filePath)))
            {
                return null;
            }
            try
            {
                PdfDocument doc = new PdfDocument(new PdfReader(Server.MapPath(filePath)).SetUnethicalReading(true));
                return doc;
            }
            catch (Exception pe)
            {
                byte[] breakLampiran2 = CreateBreakTitlePage("- Dokumen usulan tidak dapat dibuka.", "Kesalahan: " + pe.Message);
                PdfDocument pdfBrokenInfo = new PdfDocument(new PdfReader(new MemoryStream(breakLampiran2)).SetUnethicalReading(true));
                return pdfBrokenInfo;
            }
        }
        public byte[] CreateBreakTitlePage(string judul, string pesan = "")
        {
            var stream = new MemoryStream();
            WriterProperties prop = new WriterProperties();

            var writer = new PdfWriter(stream, prop);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            //PdfFont font = PdfFontFactory.createFont(FontConstants.);
            document.Add(new Paragraph(judul));
            document.Add(new Paragraph(pesan));
            document.Close();

            return stream.ToArray();
        }

        protected void gvLuaranWajib_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //arr_kd_sts_unggah,arr_id_dokumen_bukti_luaran,jml_dokumen

                string strJmlDokumen = gvLuaranWajib.DataKeys[e.Row.RowIndex]["jml_dokumen"].ToString();
                int intJmlDokumen = int.Parse(strJmlDokumen);
                LinkButton lbUnduhLuaran = new LinkButton();
                lbUnduhLuaran = (LinkButton)e.Row.FindControl("lbUnduhLuaran");
                lbUnduhLuaran.ForeColor = System.Drawing.Color.Gray;
                lbUnduhLuaran.Enabled = false;
                if (intJmlDokumen > 0)
                {
                    lbUnduhLuaran.ForeColor = System.Drawing.Color.Red;
                    lbUnduhLuaran.Enabled = true;
                }
            }
        }
        protected void gvLuaranTambahan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id_jenis_luaran = gvLuaranTambahan.DataKeys[e.RowIndex]["id_jenis_luaran"].ToString();
            ViewState["idLuaranDijanjikan"] = gvLuaranTambahan.DataKeys[e.RowIndex]["id_luaran_dijanjikan"].ToString();
            ViewState["idTargetCapaianLuaran"] = gvLuaranTambahan.DataKeys[e.RowIndex]["id_target_capaian_luaran"].ToString();
            pilihJenisLuaran(id_jenis_luaran, 2);
            lblModalTitle.Text = "LUARAN TAMBAHAN";
            new uiModal().ShowModal(this.Page, "modalLuaran");
        }

        protected void gvLuaranTambahan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //arr_kd_sts_unggah,arr_id_dokumen_bukti_luaran,jml_dokumen

                string strJmlDokumen = gvLuaranTambahan.DataKeys[e.Row.RowIndex]["jml_dokumen"].ToString();
                int intJmlDokumen = int.Parse(strJmlDokumen);
                LinkButton lbUnduhLuaran = new LinkButton();
                lbUnduhLuaran = (LinkButton)e.Row.FindControl("lbUnduhLuaran");
                lbUnduhLuaran.ForeColor = System.Drawing.Color.Gray;
                lbUnduhLuaran.Enabled = false;
                if (intJmlDokumen > 0)
                {
                    lbUnduhLuaran.ForeColor = System.Drawing.Color.Red;
                    lbUnduhLuaran.Enabled = true;
                }
            }
        }
        private void pilihJenisLuaran(string id_jenis_luaran, int idKelompokLuaran)
        {
            DataTable dt = new DataTable();

            string judulForm = string.Empty;
            switch (id_jenis_luaran.Trim())
            {
                case "1": // jurnal internasional
                    judulForm = "Artikel di Jurnal Internasional Terindeks di Pengindeks Bereputasi";
                    ktLuaranPublikasi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vPublikasi);
                    break;
                case "2": // jurnal nasional terakreditasi
                    judulForm = "Publikasi Ilmiah Jurnal Nasional Terakreditasi";
                    ktLuaranPublikasi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vPublikasi);
                    break;
                case "21": // Publikasi Ilmiah Jurnal Nasional Tidak Terakreditasi
                    judulForm = "Publikasi Ilmiah Jurnal Nasional Tidak Terakreditasi";
                    ktLuaranPublikasi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vPublikasi);
                    break;
                case "97": // Artikel di Jurnal Nasional terakreditasi peringkat 1-6
                case "98": // Artikel di Jurnal Nasional terakreditasi peringkat 1-3
                case "203": //Artikel di Jurnal Nasional terakreditasi peringkat 1-2
                case "101": //
                case "54": // Artikel di Jurnal Internasional Terindeks di Pengindeks Bereputasi
                case "194": //
                    //judulForm = "Artikel di Jurnal Internasional Terindeks di Pengindeks Bereputasi";
                    modelLapKemajuan.getnamaluaran(ref dt, id_jenis_luaran.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        judulForm = dt.Rows[0]["nama_jenis_luaran"].ToString();
                    }
                    ktLuaranPublikasi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vPublikasi);
                    break;
                case "3": // prosiding
                    judulForm = "Prosiding dalam pertemuan ilmiah Nasional";
                    ktKonferensi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vProsiding);
                    break;
                case "4": // prosiding
                    judulForm = "Prosiding dalam pertemuan ilmiah Internasional";
                    ktKonferensi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vProsiding);
                    break;
                case "22": // prosiding lokal
                    judulForm = "Prosiding dalam pertemuan ilmiah Lokal";
                    ktKonferensi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vProsiding);
                    break;
                case "102":
                case "57": // prosiding Seminar Internasional
                    judulForm = "Artikel pada Conference/Seminar Internasional di Pengindeks Bereputasi";
                    ktKonferensi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vProsiding);
                    break;
                case "37": // Seminar Internasional
                    judulForm = "Keikutsertaan dalam Seminar Internasional";
                    ktKonferensi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vProsiding);
                    break;
                case "38": // Seminar nasional
                    judulForm = "Keikutsertaan dalam Seminar Nasional";
                    ktKonferensi.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vProsiding);
                    break;
                case "5": // Keynote Speaker
                    judulForm = "Keynote Speaker dalam pertemuan ilmiah Internasional";
                    ktKeyNoteSpeaker.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran));
                    mvModal.SetActiveView(vKeyNoteSpeaker);
                    break;
                case "6": // Keynote Speaker
                    judulForm = "Keynote Speaker dalam pertemuan ilmiah Nasional";
                    ktKeyNoteSpeaker.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran));
                    mvModal.SetActiveView(vKeyNoteSpeaker);
                    break;
                case "23": // Keynote Speaker
                    judulForm = "Keynote Speaker dalam pertemuan ilmiah Lokal";
                    ktKeyNoteSpeaker.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran));
                    mvModal.SetActiveView(vKeyNoteSpeaker);
                    break;
                case "7": // Visiting Lecturer
                    judulForm = "Visiting Lecturer Internasional";
                    ktVisitingLecturer.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran));
                    mvModal.SetActiveView(vVisitingLecturer);
                    break;
                case "19": // Buku Ajar (ISBN)
                    judulForm = "Buku Ajar";
                    ktBukuAjar.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vBukuAjar);
                    break;
                case "59":
                case "61":
                case "60": //Buku Ajar
                           //judulForm = "Buku Ajar";
                    modelLapKemajuan.getnamaluaran(ref dt, id_jenis_luaran.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        judulForm = dt.Rows[0]["nama_jenis_luaran"].ToString();
                    }

                    ktBukuAjar.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vBukuAjar);
                    break;
                case "62": //Buku Ajar
                    judulForm = "Buku Ajar";
                    ktBukuAjar.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vBukuAjar);
                    break;
                case "26": //Desain
                    if (ViewState["idTargetCapaianLuaran"].ToString() == "54")
                    {
                        judulForm = "Desain";
                        ktDesain.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString(),
                            judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                            int.Parse(ViewState["idTargetCapaianLuaran"].ToString()),
                            ViewState["kd_tahapan_kegiatan"].ToString());
                        mvModal.SetActiveView(vDesain);
                    }
                    else
                    {
                        judulForm = "Desain Produk";
                        ktDesainProduk.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString(),
                            judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                            int.Parse(ViewState["idTargetCapaianLuaran"].ToString()),
                            ViewState["kd_tahapan_kegiatan"].ToString());
                        mvModal.SetActiveView(vDesainProduk);
                    }
                    break;
                case "27": //Karya Seni
                    judulForm = "Karya Seni";
                    ktKaryaSeni.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vKaryaSeni);
                    break;
                case "13": //Desain Produk Industri
                    judulForm = "Desain Produk Industri";
                    ktDesainProdukIndustri.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vDesainProdukIndustri);
                    break;
                case "44": //Buku Hasil Penelitian
                    judulForm = "Buku Hasil Penelitian";
                    ktBukuHasilPenelitian.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vBukuHasilPenelitian);
                    break;
                case "52":
                    judulForm = "Dokumen berupa deskripsi dan spesifikasi purwarupa laik industri";
                    ktDokPurwarupa.setData(ViewState["idTransaksiKegiatan"].ToString(),
                                            ViewState["idLuaranDijanjikan"].ToString(),
                                            judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                                            int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vDokPurwarupa);
                    break;
                case "58": //Book Chapter
                    judulForm = "Book Chapter";
                    ktBookChapter.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vBookChapter);
                    break;
                case "39": //Book-chapter (ISBN)
                    judulForm = "Book Chapter";
                    ktBookChapter.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vBookChapter);
                    //ktBookChapter.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                    //    , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue);
                    //mvModal.SetActiveView(vBookChapter);
                    break;
                case "41": //Dokumen Business Plan
                    judulForm = "Dokumen Business Plan";
                    ktDokBisnisPlan.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vDokBisnisPlan);
                    break;
                case "40": //Dokumen Feasibility Study
                    judulForm = "Dokumen Feasibility Study";
                    ktDokBisnisPlan.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vDokBisnisPlan);
                    break;
                case "48": //Dokumen hasil uji coba di lingkungan yang sebenarnya
                    judulForm = "Dokumen Hasil Uji Coba di Lingkungan Sebenarnya";
                    ktDokHasilUjiCobaLingkungan.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vDokHasilUjiCobaLingkingan);
                    break;
                case "47": //Dokumen hasil uji coba di lingkungan yang terbatas
                    judulForm = "Dokumen Hasil Uji Coba di Lingkungan Terbatas";
                    ktDokHasilUjiCobaLingkungan.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vDokHasilUjiCobaLingkingan);
                    break;
                case "45": //Dokumen hasil uji laik industri
                    judulForm = "Dokumen Hasil Uji Laik Industri";
                    ktDokHasilUjiCobaLingkungan.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vDokHasilUjiCobaLingkingan);
                    break;
                case "46": //Dokumentasi hasil uji coba produk
                    judulForm = "Dokumen Hasil Uji Coba Produk";
                    ktDokHasilUjiCobaLingkungan.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vDokHasilUjiCobaLingkingan);
                    break;
                case "51": //Dokumen pengujian purwarupa berupa foto atau video
                    judulForm = "Dokumen Pengujian Purwarupa berupa Foto atau Video";
                    ktDokHasilUjiCobaLingkungan.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(),
                        judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vDokHasilUjiCobaLingkingan);
                    //ktDokPengujianPurwarupa.setData(ViewState["idTransaksiKegiatan"].ToString(), judulForm,
                    //    idKelompokLuaran, ddlThnPelaksanaan.SelectedValue);
                    //mvModal.SetActiveView(vDokPengujianPurwarupa);
                    break;
                case "14": //indikasi geografis
                    judulForm = "Indikasi Geografis";
                    ktindikasiGeografis.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(), judulForm,
                        idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vIndikasiGeografis);
                    break;
                case "15": //PVT
                    judulForm = "Perlindungan Varietas Tanaman";
                    ktPVT.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vPVT);
                    break;
                case "10": //Hak Cipta
                case "63": //Program komputer
                case "64": //Alat peraga
                case "65": //Lagu (musik dengan teks)
                case "66": //Drama Musikal
                case "67": //Karya seni lukis
                case "68": //Karya arsitektur
                case "69": //Peta
                case "70": //Karya seni motif lainnya
                case "71": //Karya sinematografi
                case "72": //Karya fotografi atau potret
                case "73": //Terjemahan
                case "74": //Tafsir
                case "103": //Atlas
                case "104": //Baliho
                case "105": //Banner
                case "106": //Basis data
                case "107": //Biografi
                case "108": //Brosur
                case "109": //Buku (berupa buku ajar, monograf, atau buku referensi)
                case "110": //Buku mewarnai
                case "111": //Cerita bergambar
                case "112": //Diorama
                case "113": //Dongeng
                case "114": //Drama/pertunjukan
                case "115": //Ensiklopedia
                case "116": //Film cerita
                case "117": //Film dokumenter
                case "118": //Film iklan
                case "119": //Film kartun
                case "120": //Flyer
                case "121": //Kaligrafi
                case "122": //Kamus
                case "123": //Karya rekaman suara atau bunyi
                case "124": //Karya rekaman video
                case "125": //Karya seni bantik
                case "126": //Karya seni gambar
                case "127": //Karya seni ilustrasi
                case "128": //Karya seni pahat
                case "129": //Karya seni patung
                case "130": //Karya seni rupa
                case "131": //Karya seni rupa
                case "132": //Karya seni songket
                case "133": //Karya seni terapan
                case "134": //Karya seni ukir
                case "135": //Karya seni umum
                case "136": //Karya siaran
                case "137": //Karya siaran media radio
                case "138": //Karya siaran media televisi dan film
                case "139": //Karya siaran video
                case "140": //Ketoprak
                case "141": //Kolase
                case "142": //Komedi/lawak
                case "143": //Komik
                case "144": //Kompilasi ciptaan/data
                case "145": //Koreografi
                case "146": //Leaflet
                case "147": //Lenong
                case "148": //Ludruk
                case "149": //Musik (berupa aransemen)
                case "150": //Motif sasirangan
                case "151": //Motif tapis
                case "152": //Motif tenun ikat
                case "153": //Motif ulos
                case "154": //Musik blues
                case "155": //Musik country
                case "156": //Musik dangdut
                case "157": //Musik elektronik
                case "158": //Musik funk
                case "159": //Musik gospel
                case "160": //Musik hiphop rap rapcore
                case "161": //Musik jazz
                case "162": //Musik karawitan
                case "163": //Musik klasik
                case "164": //Musik latin
                case "165": //Musik metal
                case "166": //Musik pop
                case "167": //Musik rhythm blues
                case "168": //Musik rock
                case "169": //Musik ska reggae dub
                case "170": //Musik tanpa teks
                case "171": //Musik tradisional
                case "172": //Naskah drama/pertunjukkan
                case "173": //Naskah film
                case "174": //Naskah karya siaran
                case "175": //Naskah karya sinematografi
                case "176": //Novel
                case "177": //Opera
                case "178": //Pamflet
                case "179": //Pantomim
                case "180": //Pentas musik
                case "181": //Permainan video
                case "182": //Perwajahan
                case "183": //Poster
                case "184": //Puisi
                case "185": //Seni acrobat
                case "186": //Seni pertunjukan
                case "187": //Seni pewayangan
                case "188": //Seni tari (sendra tari)
                case "189": //Senjata tradisional
                case "190": //Sirkus
                case "191": //Sketsa
                case "192": //Spanduk
                case "193": //Sulap
                    judulForm = "Hak Cipta";
                    ktHakCipta.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(), judulForm,
                        idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vHakCipta);
                    break;
                case "11": //merek dagang
                    judulForm = "merek dagang";
                    ktmerekDagang.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(), judulForm,
                        idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vMerekDagang);
                    break;
                case "17": //TTG
                    judulForm = "Teknologi Tepat Guna";
                    ktTTG.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vTTG);
                    break;
                case "28": //Rekayasa Sosial
                    judulForm = "Rekayasa Sosial";
                    ktRS.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vRS);
                    break;
                case "8": //Paten
                case "75": //Paten
                case "76": //Paten
                case "77": //Paten
                case "78": //Paten
                          //case "12": //Rahasia Dagang
                          //case "16": //Bahan Ajar
                          //DataTable dt = new DataTable();
                    modelLapKemajuan.getnamaluaran(ref dt, id_jenis_luaran.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        judulForm = dt.Rows[0]["nama_jenis_luaran"].ToString();
                    }
                    //judulForm = "Paten";
                    ktPaten.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(), judulForm,
                        idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vPaten);
                    break;
                case "9": //Paten Sederhana
                    judulForm = "Paten Sederhana";
                    ktPaten.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(), judulForm,
                        idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vPaten);
                    break;
                case "36": //Strategi
                    judulForm = "Strategi";
                    ktStrategi.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vStrategi);
                    break;
                case "33": //Strategi
                    judulForm = "Sistem";
                    ktSistem.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vSistem);
                    break;
                case "35": //Produk
                    judulForm = "Produk";
                    ktProduk.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vProduk);
                    break;
                case "32": //Kebijakan
                    judulForm = "Kebijakan";
                    ktKebijakan.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(), judulForm,
                        idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vKebijakan);
                    break;
                case "25": //Purwarupa Prototipe 
                    judulForm = "Purwarupa (Prototipe)";
                    ktPurwarupaPrototipe.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vPurwarupaPrototipe);
                    break;
                case "34": //Metode
                    judulForm = "Metode";
                    ktMetode.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vMetode);
                    break;
                case "42": //Naskah akademik (policy brief, rekomendasi kebijakan, atau model kebijakan strategis)
                case "99":
                    judulForm = "Naskah Akademik (policy brief, rekomendasi kebijakan, atau model kebijakan strategis)";
                    ktNaskahAkademik.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString(), judulForm,
                        idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vNaskahAkademik);
                    break;

                case "43": //Purwarupa Laik Industri
                    judulForm = "Purwarupa Laik Industri";
                    ktPurwarupaLaikIndustri.setData(ViewState["idTransaksiKegiatan"].ToString(),
                        ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue,
                        int.Parse(id_jenis_luaran), ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vPurwarupaLaikIndustri);
                    break;
                case "24": //Model
                    judulForm = "Model";
                    ktModel.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString());
                    mvModal.SetActiveView(vModel);
                    break;
                default:

                    //DataTable dt = new DataTable();
                    modelLapKemajuan.getnamaluaran(ref dt, id_jenis_luaran.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        judulForm = dt.Rows[0]["nama_jenis_luaran"].ToString();
                    }
                    ktluaranLain.setData(ViewState["idTransaksiKegiatan"].ToString(), ViewState["idLuaranDijanjikan"].ToString()
                        , judulForm, idKelompokLuaran, ddlThnPelaksanaan.SelectedValue, int.Parse(id_jenis_luaran),
                        ViewState["kd_tahapan_kegiatan"].ToString(), int.Parse(ViewState["urutanThnUsulanKegiatan"].ToString()),
                        int.Parse(ViewState["idSkema"].ToString()));
                    mvModal.SetActiveView(vLuaranLain);
                    break;
            }
        }

        protected void gvLuaranTambahan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string arr_id_dokumen_bukti_luaran = gvLuaranTambahan.DataKeys[rowIndex]["arr_id_dokumen_bukti_luaran"].ToString();
            if (e.CommandName == "UnduhLuaran")
            {
                Char[] splitter = { ',' };
                string[] arrayIdDokumen = arr_id_dokumen_bukti_luaran.Split(splitter);

                if (arrayIdDokumen.Length > 0)
                {
                    unduhPDFBuktiLuaran(arrayIdDokumen, "tambahan");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Belum ada Dokumen yang diunggah");
                }
            }
        }

        #endregion

        protected void lbUnduhPdfDok_Click(object sender, EventArgs e)
        {
            string namaFileDiunduh = "laporanKemajuan.pdf";
            string path = string.Format("~/fileUpload/laporan_kemajuan/{0}/", ddlThnPelaksanaan.SelectedValue);

            string page = Session["page"].ToString();
            if (page == "5")
            {
                path = string.Format("~/fileUpload/laporan_akhir/{0}/", ddlThnPelaksanaan.SelectedValue);
                namaFileDiunduh = "laporanAkhir.pdf";
            }

            string namaFile = string.Format("{0}{1}.pdf", path.ToString(), ViewState["idTransaksiKegiatan"].ToString());

            if (File.Exists(Server.MapPath(namaFile)))
            {
                unduhPDF(path, ViewState["idTransaksiKegiatan"].ToString(), namaFileDiunduh);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Dokumen belum diunggah");
            }
        }

        private bool cekFileSdhDiunggah(string idTransaksiKegiatan)
        {
            bool sdh = false;
            string path = string.Format("~/fileUpload/laporan_kemajuan/{0}/", ddlThnPelaksanaan.SelectedValue);

            string page = Session["page"].ToString();
            if (page == "5")
            {
                path = string.Format("~/fileUpload/laporan_akhir/{0}/", ddlThnPelaksanaan.SelectedValue);
            }

            string namaFile = string.Format("{0}{1}.pdf", path.ToString(), idTransaksiKegiatan);


            lbUnduhPdfDok.ForeColor = System.Drawing.Color.Gray;
            //if (File.Exists(Server.MapPath(namaFile)))
            lblStsUnggah.Text = "(Belum diunggah)";
            lblStsUnggah.ForeColor = System.Drawing.Color.Red;
            if (ViewState["kd_sts_pelaksanaan"].ToString() == "1")
            {
                lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                lblStsUnggah.Text = "(Sudah diunggah)";
                lblStsUnggah.ForeColor = System.Drawing.Color.Blue;
                sdh = true;
            }
            return sdh;
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

        #region Unggah Dokumen Mitra

        protected void lbUnduhPdfMitra_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            modelLapKemajuan.getDokumenMitra(ref dt, Guid.Parse(ViewState["idTransaksiKegiatan"].ToString()));
            if (dt.Rows.Count > 0)
            {
                string filePath = Server.MapPath(string.Format(dt.Rows[0]["lokasi_file"].ToString()));
                string NamaBerkas = "dokumen_mitra.pdf";

                if (File.Exists(filePath))
                {
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + NamaBerkas);
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Dokumen tidak ditemukan");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Dokumen tidak ditemukan");
            }
        }

        protected void lbUnggahDokumenMitra_Click(object sender, EventArgs e)
        {
            if (ddlThnPelaksanaan.SelectedValue == "0000")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tahun pelaksanaan belum dipilih");
            }
            else if (fileUploadMitra.HasFile)
            {
                if (fileUploadMitra.FileName.ToLower().EndsWith(".pdf") ||
                    fileUploadMitra.FileName.EndsWith(".PDF") ||
                    fileUploadMitra.FileName.EndsWith(".Pdf"))
                {
                    if (fileUploadMitra.PostedFile.ContentLength < (2 * 1024 * 1024))
                    {
                        prosesUnggahDokumenMitra(Guid.Parse(ViewState["idTransaksiKegiatan"].ToString()));
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File yang akan diunggah ukurannya tidak boleh melebihi 2 MByte !!!");
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
            cekFileSdhDiunggahDokumenMitra(ViewState["idTransaksiKegiatan"].ToString());
        }

        private void prosesUnggahDokumenMitra(Guid id_transaksi_kegiatan)
        {
            string dir = "~/fileUpload/realisasi_mitra";
            if (!Directory.Exists(Server.MapPath(dir)))
            {
                Directory.CreateDirectory(Server.MapPath(dir));
            }

            string path = string.Format("~/fileUpload/realisasi_mitra/{0}/", ddlThnPelaksanaan.SelectedValue);
            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

            string namaFile = string.Format("{0}{1}.pdf", path.ToString(), id_transaksi_kegiatan.ToString());

            if (fileUploadMitra.HasFile)
            {
                if (fileUploadMitra.FileName.EndsWith(".pdf") || fileUpload1.FileName.EndsWith(".PDF"))
                {
                    fileUploadMitra.SaveAs(Server.MapPath(namaFile));
                    modelLapKemajuan.insupDokumenMitra(id_transaksi_kegiatan, namaFile);
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Unggah dokumen mitra berhasil.");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
        }

        private bool cekFileSdhDiunggahDokumenMitra(string idTransaksiKegiatan)
        {
            bool sdh = false;
            string path = string.Format("~/fileUpload/realisasi_mitra/{0}/", ddlThnPelaksanaan.SelectedValue);
            string namaFile = string.Format("{0}{1}.pdf", path.ToString(), idTransaksiKegiatan);

            DataTable dt = new DataTable();
            modelLapKemajuan.getDokumenMitra(ref dt, Guid.Parse(idTransaksiKegiatan));
            if (dt.Rows.Count > 0)
            {
                lbUnduhPdfMitra.ForeColor = System.Drawing.Color.Red;
                lblStsUnggahMitra.Text = "(Sudah diunggah)";
                lblStsUnggahMitra.ForeColor = System.Drawing.Color.Blue;
                sdh = true;
            }
            else
            {
                lbUnduhPdfMitra.ForeColor = System.Drawing.Color.Gray;
                lblStsUnggahMitra.Text = "(Belum diunggah)";
                lblStsUnggahMitra.ForeColor = System.Drawing.Color.Red;
            }
            return sdh;
        }

        #endregion

    }
}