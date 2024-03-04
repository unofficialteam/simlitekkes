using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;
using simlitekkes.Models.Pengusul;
using System.IO;
using simlitekkes.Core;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using simlitekkes.Helper;
using simlitekkes.Models;
using simlitekkes.Models.Sistem;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class dokumenUsulan2019 : System.Web.UI.UserControl
    {


        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        PerbaikanUsulan modelPerbaikanProposal = new PerbaikanUsulan();

        Models.pengusul.unggahBerkas objUnggahBerkas = new Models.pengusul.unggahBerkas();
        Core.kontrolUnggah ktUnggah;
        uiNotify noty = new uiNotify();

        string extFile = ".pdf";
        int maxSize = 5 * 1024 * 1024; // 5 MB
        public event EventHandler OnChildUnggah;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void initData(usulanKegiatan objUsulanKegiatan)
        {
            //ViewState["idTransaksiKegiatan"] = idTransaksiKegiatan;
            ViewState["idUsulan"] = objUsulanKegiatan.idUsulan;
            ViewState["idUsulanKegiatan"] = objUsulanKegiatan.idUsulanKegiatan;
            ViewState["idSkema"] = objUsulanKegiatan.idSkema;
            ViewState["targetTkt"] = objUsulanKegiatan.tktTarget;
            ViewState["thnPelaksanaan"] = objUsulanKegiatan.thnPelaksanaan;
            ViewState["thnUsulan"] = objUsulanKegiatan.thnUsulan;

            panelUnggahDokAkreditasiTPPTPM.Visible = false;
            if (objUsulanKegiatan.idSkema == 5) // pkpt
            {
                panelUnggahDokAkreditasiTPPTPM.Visible = false; // panel dipindah ke mitra
            }
            isiDdlMakroRiset();
            ChildUnggahFile_OnSuccess();
            cekDdlMakro(objUsulanKegiatan.idUsulan);
            //getKomentar(objUsulanKegiatan.idUsulanKegiatan);
            getDeskripsiMakroRiset();

            lblInfoAtUnggahDokUsulan.Text = String.Format("{0} (tahun ke-{1} dari {2} tahun)",
                objUsulanKegiatan.namaSkema, objUsulanKegiatan.urutanTahunUsulanKegiatan,
                objUsulanKegiatan.lamaKegiatan);
            cekDokumenTppTpm(objUsulanKegiatan.idUsulanKegiatan);
        }
        public void setData(string idUsulan, string idUsulanKegiatan, int idSkema, int targetTkt, string thnPelaksanaan)
        {

            //string idTransaksiKegiatan,
            //ViewState["idTransaksiKegiatan"] = idTransaksiKegiatan;
            ViewState["idUsulan"] = idUsulan;
            ViewState["idUsulanKegiatan"] = idUsulanKegiatan;
            ViewState["idSkema"] = idSkema;
            ViewState["targetTkt"] = targetTkt;
            ViewState["thnPelaksanaan"] = thnPelaksanaan;

            isiDdlMakroRiset();
            ChildUnggahFile_OnSuccess();
            cekDdlMakro(idUsulan);
            //getKomentar(idUsulanKegiatan);
            getDeskripsiMakroRiset();
        }

        private void getDeskripsiMakroRiset()
        {
            int idMakroRiset = int.Parse(ddlMakroRiset.SelectedValue);

            DataTable dt = new DataTable();
            modelPerbaikanProposal.getDeskripsiMakroRiset(ref dt, idMakroRiset);
            if (dt.Rows.Count > 0)
            {
                //lblDeskripsiMakroRiset.Text = dt.Rows[0]["deskripsi"].ToString();
            }
            else
            {
                //lblDeskripsiMakroRiset.Text = "-";
            }
        }

        //private void getKomentar(string idUsulanKegiatan)
        //{
        //    DataTable dt = new DataTable();
        //    modelPerbaikanProposal.getKomentar(ref dt, idUsulanKegiatan);
        //    if (dt.Rows.Count > 0)
        //    {
        //        komentarRev1.Text = dt.Rows[0]["komentar_rev_1"].ToString();
        //        komentarRev2.Text = dt.Rows[0]["komentar_rev_2"].ToString();
        //    }
        //    else
        //    {
        //        komentarRev1.Text = "-";
        //        komentarRev2.Text = "-";
        //    }
        //}

        private void isiDdlMakroRiset()
        {
            var dt = new DataTable();

            if (modelPerbaikanProposal.getListMakroRiset(ref dt))
            {
                ddlMakroRiset.AppendDataBoundItems = true;
                ddlMakroRiset.Items.Clear();
                ddlMakroRiset.Items.Add(new ListItem { Text = "-- Pilih kelompok makro riset --", Value = "0", Selected = true });
                ddlMakroRiset.DataSource = dt;
                ddlMakroRiset.DataBind();
            }
        }

        protected void lbUnduhTemplateDok_Click(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["idSkema"].ToString()); //1;
            int tktTarget = int.Parse(ViewState["targetTkt"].ToString()); //4;

            string namaFileTemplate = "";
            objUnggahBerkas.getPathDokumenTemplate(idSkema, tktTarget, ref namaFileTemplate);
            string path = "dokumen/template/" + namaFileTemplate;

            if (File.Exists(Server.MapPath(path)))
            {
                Response.Redirect(path);
            }
            else
            {
                objUnggahBerkas.getPathDokumenTemplateAbdimas(idSkema, ref namaFileTemplate);
                path = "dokumen/template/" + namaFileTemplate;
                if (File.Exists(Server.MapPath(path)))
                {
                    Response.Redirect(path);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
                }
            }
        }

        private void cekDdlMakro(string idUsulan)
        {
            DataTable dt = new DataTable();
            modelPerbaikanProposal.getMakroRiset(ref dt, idUsulan);
            if (dt.Rows.Count > 0)
            {
                ddlMakroRiset.SelectedValue = dt.Rows[0]["id_makro_riset"].ToString();
            }
        }

        private void ChildUnggahFile_OnSuccess()
        {
            string thnUsulan = ViewState["thnUsulan"].ToString();
            Guid idUsulanKegiatan = Guid.Parse(ViewState["idUsulanKegiatan"].ToString());
            string path = "~/fileUpload/dokumenUsulan/" + thnUsulan + "/" + idUsulanKegiatan + ".pdf";
            if (int.Parse(thnUsulan) >= 2019)
            {
                path = "~/fileUpload/dokumenUsulan/" + thnUsulan + "/" + idUsulanKegiatan + ".pdf";
            }
            int totalHalaman = 0;
            int jmlHalamanTdkDptDIbaca = 0;
            bool isValid = false;
            ReadPdfFile(path, ref totalHalaman, ref isValid, ref jmlHalamanTdkDptDIbaca);
            // update tgl unggah dokumen
            if (jmlHalamanTdkDptDIbaca > 0)
            {
                File.Delete(Server.MapPath(path.ToString()));
                lblErrorInfo.Text = "Total halaman: " + totalHalaman.ToString()
                    + "<br/>Jumlah halaman tidak dapat dibaca: " + jmlHalamanTdkDptDIbaca.ToString()
                    + "<br/>Gambar tidak boleh berdiri sendiri dalam satu halaman, gambar harus diikuti teks/penjelasan tentang gambar."
                    + "<br/>Pastikan semua halaman berisi teks yang dapat dibaca/bukan hasil scan.";
            }
            else
            {
                lblErrorInfo.Text = "";
            }
            cekFileExists(path.ToString());
        }

        private void cekFileExists(string ppathDokumenUsulanPdf)
        {
            if (File.Exists(Server.MapPath(ppathDokumenUsulanPdf)))
            {
                lbUnduhTemplateDok2.ForeColor = System.Drawing.Color.Red;
                lbUnduhTemplateDok2.Enabled = true;

                lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                lbUnduhPdfDok.Enabled = true;

                System.IO.FileInfo fs = new System.IO.FileInfo(Server.MapPath(ppathDokumenUsulanPdf));
                double fSize = (double)fs.Length / 1024.0;

                lblUkuranFile.Text = string.Format("{0:0.00}", fSize) + " KByte";
                lblTglUnggah.Text = File.GetLastWriteTime(Server.MapPath(ppathDokumenUsulanPdf)).ToString();
            }
            else
            {
                lbUnduhTemplateDok2.ForeColor = System.Drawing.Color.Gray;
                lbUnduhTemplateDok2.Enabled = false;
                lbUnduhPdfDok.ForeColor = System.Drawing.Color.Gray;
                lbUnduhPdfDok.Enabled = false;
                lblUkuranFile.Text = "-";
                lblTglUnggah.Text = "-";
            }
        }

        //protected void ddlMakroRiset_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        private bool cekFile(ref FileUpload fileUpload1)
        {
            bool isSuccess = false;
            if (fileUpload1.HasFile)
            {
                if (fileUpload1.PostedFile.ContentLength < maxSize)
                {
                    if (fileUpload1.FileName.ToLower().EndsWith(extFile))
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File harus berekstensi: " + extFile);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Ukuran file maksimal: " + (maxSize / 1024).ToString() + " KByte");

                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File belum dipilih");
            }

            return isSuccess;
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            /*
            if(ddlMakroRiset.SelectedIndex == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Makro riset belum dipilih.");
                return;
            }
            */
            Guid idUsulanKegiatan = Guid.Parse(ViewState["idUsulanKegiatan"].ToString());
            maxSize = 5 * 1024 * 1024;
            bool fileValid = cekFile(ref fileUpload1);
            if(!fileValid)
            {
                return;
            }

            if (fileUpload1.HasFile)
            {
                if (fileUpload1.FileName.ToLower().EndsWith(".pdf") || fileUpload1.FileName.EndsWith(".PDF") || fileUpload1.FileName.EndsWith(".Pdf"))
                {
                    if (fileUpload1.PostedFile.ContentLength < (5 * 1024 * 1024))
                    {
                        prosesUnggah(idUsulanKegiatan);
                        //simpan();
                        if (OnChildUnggah != null)
                            OnChildUnggah(sender, null);
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
        }

        private void prosesUnggah(Guid idUsulanKegiatan)
        {
            string thnUsulan = ViewState["thnUsulan"].ToString();
            string path = string.Format("~/fileUpload/dokumenUsulan/{0}/", thnUsulan);
            if (int.Parse(thnUsulan) >= 2019)
            {
                path = string.Format("~/fileUpload/dokumenUsulan/{0}/", thnUsulan);
            }
            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

            string namaFile = string.Format("{0}{1}.pdf", path.ToString(), idUsulanKegiatan.ToString());

            if (fileUpload1.HasFile)
            {
                if (fileUpload1.FileName.EndsWith(".pdf") || fileUpload1.FileName.EndsWith(".PDF"))
                {
                    fileUpload1.SaveAs(Server.MapPath(namaFile));
                    ChildUnggahFile_OnSuccess();
                    ////modelPerbaikanProposal.insertDataUnggahDokumen(idTransaksiKegiatan);
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Unggah dokumen berhasil.");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
        }

        private bool isAllowedExtension(string namaFile, string exts)
        {
            bool isAllowed = false;
            string[] arrExt = exts.Split(';');
            for (int a = 0; a < arrExt.Length; a++)
            {
                if (namaFile.ToLower().EndsWith("." + arrExt[a].ToLower()))
                {
                    isAllowed = true;
                }
            }
            return isAllowed;
        }

        public void ReadPdfFile(string fileName, ref int jmlHlman, ref bool valid, ref int jmlHalamanTdkDapatDibaca)
        {
            fileName = Server.MapPath(fileName);
            StringBuilder text = new StringBuilder();
            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);
                PdfDocument pdfDoc = new PdfDocument(pdfReader);
                jmlHlman = pdfDoc.GetNumberOfPages();// NumberOfPages;
                string samplePerPage = "";
                int jmlPageTdkDptDibaca = 0;
                for (int page = 1; page <= jmlHlman; page++)
                {
                    try
                    {

                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page));// (pdfReader, page, strategy);
                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                        if (currentText.Length > 100)
                        {
                            currentText = currentText.Substring(0, 100);
                        }
                        else if (currentText.Length < 10)
                        {
                            jmlPageTdkDptDibaca++;
                        }

                        samplePerPage += page.ToString() + ". " + currentText + "...<br/>";

                    }
                    catch (Exception ex) { }
                }
                valid = false;
                pdfReader.Close();

                string info = String.Format("<br><br>Total halaman: {0}<br>Dapat dibaca: {1}<br>Tidak dapat dibaca: {2}<br>",
                        jmlHlman, (jmlHlman - jmlPageTdkDptDibaca), jmlPageTdkDptDibaca);
                jmlHalamanTdkDapatDibaca = jmlPageTdkDptDibaca;
                samplePerPage = info + " <br/>" + samplePerPage;

            }
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
        }

        protected void lbUnduhPdfDok_Click(object sender, EventArgs e)
        {
            string path = String.Format("~/fileUpload/dokumenUsulan/{0}/{1}.pdf", ViewState["thnUsulan"].ToString(), ViewState["idUsulanKegiatan"].ToString());
            string thnUsulan = ViewState["thnUsulan"].ToString();

            if (File.Exists(Server.MapPath(path)))
            {
                string PATH_UNGGAH_BERKAS = "../fileUpload/dokumenUsulan/" + ViewState["thnUsulan"].ToString();

                string namaBerkas = "dokumenUsulan.pdf";
                var atributUnduh = new AtributUnduh
                {
                    FolderUnduh = PATH_UNGGAH_BERKAS,
                    NamaBerkas = ViewState["idUsulanKegiatan"] + ".pdf",
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

        protected void ddlMakroRiset_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid idUsulan = Guid.Parse(ViewState["idUsulan"].ToString());
            modelPerbaikanProposal.updateMakroRiset(idUsulan, int.Parse(ddlMakroRiset.SelectedValue));
            getDeskripsiMakroRiset();
        }

        protected void lbUnggahDokumenTpp_Click(object sender, EventArgs e)
        {
            maxSize = 2 * 1024 * 1024;
            bool fileValid = cekFile(ref fileUploadTpp);
            if (!fileValid)
            {
                return;
            }
            prosesUnggahDokTppTpm(ref fileUploadTpp, ViewState["idUsulanKegiatan"].ToString(), "1");
        }

        protected void lbUnggahDokumenTpm_Click(object sender, EventArgs e)
        {
            maxSize = 2 * 1024 * 1024;
            bool fileValid = cekFile(ref fileUploadTPM);
            if (!fileValid)
            {
                return;
            }
            prosesUnggahDokTppTpm(ref fileUploadTPM, ViewState["idUsulanKegiatan"].ToString(), "2");
        }

        protected void lbUnduhDokTpp_Click(object sender, EventArgs e)
        {

            string path = String.Format("~/fileUpload/dokAkreditasiTPPTPM/{0}/{1}.pdf", ViewState["thnUsulan"].ToString(), ViewState["dokumen_akreditasi_tpp"].ToString());
            string thnUsulan = ViewState["thnUsulan"].ToString();

            if (File.Exists(Server.MapPath(path)))
            {
                string PATH_UNGGAH_BERKAS = "../fileUpload/dokAkreditasiTPPTPM/" + ViewState["thnUsulan"].ToString();

                string namaBerkas = "dokAkreditasiTPP.pdf";
                var atributUnduh = new AtributUnduh
                {
                    FolderUnduh = PATH_UNGGAH_BERKAS,
                    NamaBerkas = ViewState["dokumen_akreditasi_tpp"] + ".pdf",
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

        protected void lbUnduhDokTpm_Click(object sender, EventArgs e)
        {

            string path = String.Format("~/fileUpload/dokAkreditasiTPPTPM/{0}/{1}.pdf", ViewState["thnUsulan"].ToString(), ViewState["dokumen_akreditasi_tpm"].ToString());
            string thnUsulan = ViewState["thnUsulan"].ToString();

            if (File.Exists(Server.MapPath(path)))
            {
                string PATH_UNGGAH_BERKAS = "../fileUpload/dokAkreditasiTPPTPM/" + ViewState["thnUsulan"].ToString();

                string namaBerkas = "dokAkreditasiTPM.pdf";
                var atributUnduh = new AtributUnduh
                {
                    FolderUnduh = PATH_UNGGAH_BERKAS,
                    NamaBerkas = ViewState["dokumen_akreditasi_tpm"] + ".pdf",
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

        private void prosesUnggahDokTppTpm(ref FileUpload rFU, string idUsulanKegiatan, string idJnsDok)
        {
            string thnUsulan = ViewState["thnUsulan"].ToString();
            string path = string.Format("~/fileUpload/dokAkreditasiTPPTPM/{0}/", thnUsulan);
            if (int.Parse(thnUsulan) >= 2019)
            {
                path = string.Format("~/fileUpload/dokAkreditasiTPPTPM/{0}/", thnUsulan);
            }
            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

            string idDokTppTpm = Guid.NewGuid().ToString();
            if (idJnsDok == "1")
            {
                if (ViewState["dokumen_akreditasi_tpp"].ToString().Length > 10)
                {
                    idDokTppTpm = ViewState["dokumen_akreditasi_tpp"].ToString();
                }
            }
            else if (idJnsDok == "2")
            {
                if (ViewState["dokumen_akreditasi_tpm"].ToString().Length > 10)
                {
                    idDokTppTpm = ViewState["dokumen_akreditasi_tpm"].ToString();
                }
            }
            string namaFile = string.Format("{0}{1}.pdf", path.ToString(), idDokTppTpm);

            if (rFU.HasFile)
            {
                if (rFU.FileName.EndsWith(".pdf") || rFU.FileName.EndsWith(".PDF"))
                {
                    rFU.SaveAs(Server.MapPath(namaFile));
                    ChildUnggahFile_OnSuccess();
                    objUnggahBerkas.insupDokumenUsulanAkreditasiTppTpm(idUsulanKegiatan, idDokTppTpm, idJnsDok);
                    cekDokumenTppTpm(idUsulanKegiatan);
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Unggah dokumen berhasil.");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
        }

        private void cekDokumenTppTpm(string idUsulanKegiatan)
        {
            lbUnduhDokTpp.ForeColor = System.Drawing.Color.Gray;
            lbUnduhDokTpm.ForeColor = System.Drawing.Color.Gray;
            DataTable dt = new DataTable();
            objUnggahBerkas.getPathDokumenHasilAkreditasiTppTpm(ref dt, idUsulanKegiatan);
            ViewState["dokumen_akreditasi_tpp"] = "";
            ViewState["dokumen_akreditasi_tpm"] = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["dokumen_akreditasi_tpp"].ToString().Length > 10) // uuid
                {                    
                    lbUnduhDokTpp.ForeColor = System.Drawing.Color.Red;
                    ViewState["dokumen_akreditasi_tpp"] = dt.Rows[0]["dokumen_akreditasi_tpp"].ToString();
                    string path = String.Format("~/fileUpload/dokAkreditasiTPPTPM/{0}/{1}.pdf", 
                        ViewState["thnUsulan"].ToString(), ViewState["dokumen_akreditasi_tpp"].ToString());
                    if (File.Exists(Server.MapPath(path)))
                    {
                        System.IO.FileInfo fs = new System.IO.FileInfo(Server.MapPath(path));
                        double fSize = (double)fs.Length / 1024.0;
                        lblUkuranFileTpp.Text = string.Format("{0:0.00}", fSize) + " KByte";
                        lblTglUnggahTpp.Text = File.GetLastWriteTime(Server.MapPath(path)).ToString();
                    }
                }
                if (dt.Rows[0]["dokumen_akreditasi_tpm"].ToString().Length > 10) // uuid
                {
                    lbUnduhDokTpm.ForeColor = System.Drawing.Color.Red;
                    ViewState["dokumen_akreditasi_tpm"] = dt.Rows[0]["dokumen_akreditasi_tpm"].ToString();
                    string path = String.Format("~/fileUpload/dokAkreditasiTPPTPM/{0}/{1}.pdf", 
                        ViewState["thnUsulan"].ToString(), ViewState["dokumen_akreditasi_tpm"].ToString());
                    if (File.Exists(Server.MapPath(path)))
                    {
                        System.IO.FileInfo fs = new System.IO.FileInfo(Server.MapPath(path));
                        double fSize = (double)fs.Length / 1024.0;
                        lblUkuranFileTpm.Text = string.Format("{0:0.00}", fSize) + " KByte";
                        lblTglUnggahTpm.Text = File.GetLastWriteTime(Server.MapPath(path)).ToString();
                    }
                }
            }

        }

    }
}