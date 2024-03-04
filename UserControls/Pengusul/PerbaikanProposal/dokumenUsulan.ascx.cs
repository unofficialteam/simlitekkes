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

namespace simlitekkes.UserControls.Pengusul.PerbaikanProposal
{
    public partial class dokumenUsulan : System.Web.UI.UserControl
    {
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

        public void setData(string idTransaksiKegiatan, string idUsulan, string idUsulanKegiatan, int idSkema, int targetTkt, string thnPelaksanaan)
        {
            ViewState["idTransaksiKegiatan"] = idTransaksiKegiatan;
            ViewState["idUsulan"] = idUsulan;
            ViewState["idUsulanKegiatan"] = idUsulanKegiatan;
            ViewState["idSkema"] = idSkema;
            ViewState["targetTkt"] = targetTkt;
            ViewState["thnPelaksanaan"] = thnPelaksanaan;
            
            //isiDdlMakroRiset();
            //ChildUnggahFile_OnSuccess();
            //cekDdlMakro(idUsulan);
            getKomentar(idUsulanKegiatan);
            //getDeskripsiMakroRiset();
            string path = "~/fileUpload/dokumenUsulanRevisi/" + thnPelaksanaan + "/" + idTransaksiKegiatan + ".pdf";
            cekFileExists(path.ToString());
        }

        //private void getDeskripsiMakroRiset()
        //{
        //    int idMakroRiset = int.Parse(ddlMakroRiset.SelectedValue);

        //    DataTable dt = new DataTable();
        //    modelPerbaikanProposal.getDeskripsiMakroRiset(ref dt, idMakroRiset);
        //    if (dt.Rows.Count > 0)
        //    {
        //        lblDeskripsiMakroRiset.Text = dt.Rows[0]["deskripsi"].ToString();
        //    }
        //    else
        //    {
        //        lblDeskripsiMakroRiset.Text = "-";
        //    }
        //}

        private void getKomentar(string idUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            modelPerbaikanProposal.getKomentar(ref dt, idUsulanKegiatan);
            if (dt.Rows.Count > 0)
            {
                komentarRev1.Text = dt.Rows[0]["komentar_rev_1"].ToString();
                komentarRev2.Text = dt.Rows[0]["komentar_rev_2"].ToString();
            }
            else
            {
                komentarRev1.Text = "-";
                komentarRev2.Text = "-";
            }
        }
        
        //private void isiDdlMakroRiset()
        //{
        //    var dt = new DataTable();

        //    if (modelPerbaikanProposal.getListMakroRiset(ref dt))
        //    {
        //        ddlMakroRiset.AppendDataBoundItems = true;
        //        ddlMakroRiset.Items.Clear();
        //        ddlMakroRiset.Items.Add(new ListItem { Text = "-- Pilih kelompok makro riset --", Value = "0", Selected = true });
        //        ddlMakroRiset.DataSource = dt;
        //        ddlMakroRiset.DataBind();
        //    }
        //}

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
        
        //private void cekDdlMakro(string idUsulan)
        //{
        //    DataTable dt = new DataTable();
        //    modelPerbaikanProposal.getMakroRiset(ref dt, idUsulan);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ddlMakroRiset.SelectedValue = dt.Rows[0]["id_makro_riset"].ToString();
        //    }
        //}

        private void ChildUnggahFile_OnSuccess()
        {
            string thnPelaksanaan = ViewState["thnPelaksanaan"].ToString();
            Guid idTransaksiKegiatan = Guid.Parse(ViewState["idTransaksiKegiatan"].ToString());
            string path = "~/fileUpload/dokumenUsulanRevisi/" + thnPelaksanaan + "/" + idTransaksiKegiatan + ".pdf";
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

                //lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                //lbUnduhPdfDok.Enabled = true;

                System.IO.FileInfo fs = new System.IO.FileInfo(Server.MapPath(ppathDokumenUsulanPdf));
                double fSize = (double)fs.Length / 1024.0;

                lblUkuranFile.Text = string.Format("{0:0.00}", fSize) + " KByte";
                lblTglUnggah.Text = File.GetLastWriteTime(Server.MapPath(ppathDokumenUsulanPdf)).ToString();
            }
            else
            {
                lbUnduhTemplateDok2.ForeColor = System.Drawing.Color.Gray;
                lbUnduhTemplateDok2.Enabled = false;
                //lbUnduhPdfDok.ForeColor = System.Drawing.Color.Gray;
                //lbUnduhPdfDok.Enabled = false;
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
            Guid idTransaksiKegiatan = Guid.Parse(ViewState["idTransaksiKegiatan"].ToString());

            if (fileUpload1.HasFile)
            {
                if (fileUpload1.FileName.EndsWith(".pdf") || fileUpload1.FileName.EndsWith(".PDF") || fileUpload1.FileName.EndsWith(".Pdf"))
                {
                    if (fileUpload1.PostedFile.ContentLength < (5 * 1024 * 1024))
                    {
                        prosesUnggah(idTransaksiKegiatan);
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

        private void prosesUnggah(Guid idTransaksiKegiatan)
        {
            string thnPelaksanaanKegiatan = ViewState["thnPelaksanaan"].ToString();
            string path = string.Format("~/fileUpload/dokumenUsulanRevisi/{0}/", thnPelaksanaanKegiatan);
            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

            string namaFile = string.Format("{0}{1}.pdf", path.ToString(), idTransaksiKegiatan.ToString());

            if (fileUpload1.HasFile)
            {
                if (fileUpload1.FileName.EndsWith(".pdf") || fileUpload1.FileName.EndsWith(".PDF"))
                {
                    fileUpload1.SaveAs(Server.MapPath(namaFile));
                    ChildUnggahFile_OnSuccess();
                    modelPerbaikanProposal.insertDataUnggahDokumen(idTransaksiKegiatan);
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

        //public bool simpan()
        //{
            //Guid idUsulan = Guid.Parse(ViewState["idUsulan"].ToString());

            //if (ddlMakroRiset.SelectedValue == "0")
            //{
            //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.warning, "Informasi", "Makro riset belum dipilih");
            //    return false;
            //}
            //else
            //{
            //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Simpan berhasil.");
            //    return true;

                //if (modelPerbaikanProposal.updateMakroRiset(idUsulan, int.Parse(ddlMakroRiset.SelectedValue)))
                //{
                //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Simpan berhasil.");
                //    return true;
                //}
                //else
                //{
                //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.warning, "Informasi", "Simpan gagal " + modelPerbaikanProposal.errorMessage);
                //    return false;
                //}
            //}
        //}

        protected void lbUnduhPdfDok_Click(object sender, EventArgs e)
        {
            string path = String.Format("fileUpload/dokumenUsulanRevisi/{0}/{1}.pdf", ViewState["thnPelaksanaan"].ToString(), ViewState["idTransaksiKegiatan"].ToString());
            if (File.Exists(Server.MapPath(path)))
            {
                string PATH_UNGGAH_BERKAS = "../fileUpload/dokumenUsulanRevisi/" + ViewState["thnPelaksanaan"].ToString();
                string namaBerkas = "dokumenUsulan.pdf";
                var atributUnduh = new AtributUnduh
                {
                    FolderUnduh = PATH_UNGGAH_BERKAS,
                    NamaBerkas = ViewState["idTransaksiKegiatan"] + ".pdf",
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

        //protected void ddlMakroRiset_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    getDeskripsiMakroRiset();
        //}
    }
}