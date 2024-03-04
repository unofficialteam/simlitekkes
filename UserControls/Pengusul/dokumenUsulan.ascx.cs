using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using simlitekkes.Core;
using simlitekkes.Helper;
using simlitekkes.Models.pengusul;
using simlitekkes.Models.Sistem;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class dokumenUsulan : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        kontrolUnggah ktUnggahCS = new kontrolUnggah();
        unggahBerkas objUnggahBerkas = new unggahBerkas();

        protected void Page_Load(object sender, EventArgs e)
        {
            ktUnggah.OnChildEventOccurs += new EventHandler(ChildUnggahFile_OnSuccess);
        }

        void ChildUnggahFile_OnSuccess(object sender, EventArgs e)
        {
            int totalHalaman = 0;
            int jmlHalamanTdkDptDIbaca = 0;
            bool isValid = false;
            ReadPdfFile(ViewState["path_file"].ToString(), ref totalHalaman, ref isValid, ref jmlHalamanTdkDptDIbaca);
            // update tgl unggah dokumen
            if (jmlHalamanTdkDptDIbaca > 0)
            {
                File.Delete(Server.MapPath(ViewState["path_file"].ToString()));
                lblErrorInfo.Text = "Total halaman: " + totalHalaman.ToString() 
                    +"<br/>Jumlah halaman tidak dapat dibaca: " + jmlHalamanTdkDptDIbaca.ToString() 
                    +"<br/>Gambar tidak boleh berdiri sendiri dalam satu halaman, gambar harus diikuti teks/penjelasan tentang gambar."
                    +"<br/>Pastikan semua halaman berisi teks yang dapat dibaca/bukan hasil scan.";
            }
            else
            {
                lblErrorInfo.Text = "";
            }
            cekFileExists(ViewState["path_file"].ToString());
        }

        public void setJenisKegiatan(string strJenisKegiatan)
        {
            lblJenisKegiatan.Text = strJenisKegiatan;
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

                //return samplePerPage;
            }
            //return "";
        }
        private void cekFileExists(string ppathDokumenUsulanPdf)
        {
            if (File.Exists(Server.MapPath(ppathDokumenUsulanPdf)))
            {
                lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                lbUnduhPdfDok.Enabled = true;
                System.IO.FileInfo fs = new System.IO.FileInfo(Server.MapPath(ppathDokumenUsulanPdf));
                double fSize = (double)fs.Length/1024.0;

                lblUkuranFile.Text = string.Format("{0:0.00}", fSize) + " KByte";
                lblTglUnggah.Text = File.GetLastWriteTime(Server.MapPath(ppathDokumenUsulanPdf)).ToString();
            }
            else
            {
                lbUnduhPdfDok.ForeColor = System.Drawing.Color.Gray;
                lbUnduhPdfDok.Enabled = false;
                lblUkuranFile.Text = "-";
                lblTglUnggah.Text = "-";
            }
        }

        public void setDataUsulan(string pidUsulanKegiatan, string pTahunUsulan, string pathDokumenUsulanPdf, usulanKegiatan pUK) //int urutanThnusulan, int lamaKegiatan)
        {
            ViewState["id_usulan_kegiatan"] = pidUsulanKegiatan;
            ViewState["thn_usulan"] = pTahunUsulan;
            ViewState["path_file"] = pathDokumenUsulanPdf;
            ViewState["id_skema"] = pUK.idSkema;
            ViewState["tkt_target"] = pUK.tktTarget;
            ViewState["nama_skema"] = pUK.namaSkema;
            cekFileExists(pathDokumenUsulanPdf);
            string kategoriSbk = "Riset dasar";
            if(pUK.tktTarget > 6)
            {
                kategoriSbk = "Riset pengembangan";
            }
             else if(pUK.tktTarget > 3)
            {
                kategoriSbk = "Riset terapan";
            }
            else
            {
                kategoriSbk = pUK.namaSkema;
            }

            lblInfoAtUnggahDokUsulan.Text = String.Format("{0} (tahun ke-{1} dari {2} tahun)", pUK.namaSkema, pUK.urutanTahunUsulanKegiatan, pUK.lamaKegiatan);
        }

        protected void lbUnduhTemplateDok_Click(object sender, EventArgs e)
        {
            string namaFileTemplate = "";
            objUnggahBerkas.getPathDokumenTemplate(int.Parse(ViewState["id_skema"].ToString()),
                int.Parse(ViewState["tkt_target"].ToString()), ref namaFileTemplate);
            string path = "dokumen/template/" + namaFileTemplate;

            if (File.Exists(Server.MapPath(path)))
            {
                Response.Redirect(path);
            }
            else
            {
                objUnggahBerkas.getPathDokumenTemplateAbdimas(int.Parse(ViewState["id_skema"].ToString()),
                 ref namaFileTemplate);
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

            //string namaFileTemplate = "";
            //    objUnggahBerkas.getPathDokumenTemplate(int.Parse(ViewState["id_skema"].ToString()),
            //    int.Parse(ViewState["tkt_target"].ToString()), ref namaFileTemplate);
            //string path = "dokumen/template/"+ namaFileTemplate;
            //if (File.Exists(Server.MapPath(path)))
            //{

            //    string PATH_UNGGAH_BERKAS = "../dokumen/template";
            //    string namaBerkas2download = "template_" + ViewState["nama_skema"].ToString().Replace(" ","") + ".docx";
            //    var atributUnduh = new AtributUnduh
            //    {
            //        FolderUnduh = PATH_UNGGAH_BERKAS,
            //        NamaBerkas = namaFileTemplate,
            //        NamaBerkasdiUnduh = namaBerkas2download
            //    };
            //    Session["AtributUnduh"] = atributUnduh;
            //    var unduhForm = "helper/unduhFile.aspx";
            //    Response.Redirect(unduhForm);
            //}
            //else
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            //}
        }

        protected void lbUnduhPdfDok_Click(object sender, EventArgs e)
        {
            string path = String.Format("fileUpload/dokumenUsulan/{0}/{1}.pdf", ViewState["thn_usulan"].ToString(), ViewState["id_usulan_kegiatan"].ToString());

            if (int.Parse(ViewState["thn_usulan"].ToString()) >= 2019)
            {
                path = path.Replace("fileUpload/", "fileUpload/");
            }

            if (File.Exists(Server.MapPath(path)))
            {
                string PATH_UNGGAH_BERKAS = "../fileUpload/DokumenUsulan/"+ ViewState["thn_usulan"].ToString();
                string namaBerkas = "dokumenUsulan.pdf";
                var atributUnduh = new AtributUnduh
                {
                    FolderUnduh = PATH_UNGGAH_BERKAS,
                    NamaBerkas = ViewState["id_usulan_kegiatan"] + ".pdf",
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
    }
}