using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using System.Data;
using System.IO;

namespace simlitekkes.UserControls.Reviewer.report
{
    public partial class hasilPenilaianPengabdian1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitPdf(DataTable dt, string idPersonalReviewer)
        {
            PdfDocument pdocDepan = null;
            ByteArrayOutputStream ms = new ByteArrayOutputStream();
            for (int a = 0; a < dt.Rows.Count; a++)
            {
                string id_usulan_kegiatan = dt.Rows[a]["id_usulan_kegiatan"].ToString();
                string nama_skema = dt.Rows[a]["nama_skema"].ToString();
                if (a == 0)
                {
                    byte[] pdfDepan = InitPdfAwal(id_usulan_kegiatan, idPersonalReviewer);
                    pdocDepan = new PdfDocument(new PdfReader(new MemoryStream(pdfDepan)), new PdfWriter(ms));
                    pdocDepan.SetDefaultPageSize(new PageSize(PageSize.A4));
                }
                else
                {
                    byte[] bPdfnext = InitPdfNext(id_usulan_kegiatan, idPersonalReviewer);
                    PdfDocument pdocNext = new PdfDocument(new PdfReader(new MemoryStream(bPdfnext)));
                    pdocNext.CopyPagesTo(1, pdocNext.GetNumberOfPages(), pdocDepan);
                }

                if (a == dt.Rows.Count - 1)
                {
                    Document document = new Document(pdocDepan);
                    document.Close();
                    nama_skema = nama_skema.Replace(" ", "");
                    downloadPdf(ms.ToArray(), nama_skema);
                }

            }

        }

        private byte[] InitPdfAwal(string idUsulanKegiatan, string idPersonalReviewer)
        {
            byte[] b4 = GeneratePDF(idUsulanKegiatan, idPersonalReviewer);
            ByteArrayOutputStream ms = new ByteArrayOutputStream();
            PdfDocument pdoc2 = new PdfDocument(new PdfReader(new MemoryStream(b4)), new PdfWriter(ms));
            pdoc2.SetDefaultPageSize(new PageSize(PageSize.A4));
            Document document = new Document(pdoc2);
            document.Close();
            return b4;
        }
        public byte[] InitPdfNext(string idUsulanKegiatan, string idPersonalReviewer)
        {
            byte[] b4 = GeneratePDF(idUsulanKegiatan, idPersonalReviewer);
            PdfDocument pdoc2 = new PdfDocument(new PdfReader(new MemoryStream(b4)));
            pdoc2.SetDefaultPageSize(new PageSize(PageSize.A4));
            Document document = new Document(pdoc2);
            document.Close();
            return b4;
        }
        void downloadPdf(byte[] bytes, string pNamaFile)
        {
            string namaFile = String.Format("HasilPenilaian_{0}.pdf", pNamaFile);
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.End();
        }

        protected byte[] GeneratePDF(string idUsulanKegiatan, string idPersonalReviewer)
        {
            var outHTML = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute(String.Format("~/UserControls/Reviewer/report/hasilPenilaianPengabdian.aspx?id_usulan_kegiatan={0}&id_personal_reviewer={1}",
                    idUsulanKegiatan, idPersonalReviewer),
                    stringWriter);
                outHTML = stringWriter.ToString();
                stringWriter.Close();
            }
            var converterProperties = new ConverterProperties();
            converterProperties.SetBaseUri(Server.MapPath("~/"));
            var memoryStream = new MemoryStream();
            HtmlConverter.ConvertToPdf(outHTML, memoryStream, converterProperties);
            return memoryStream.ToArray();
        }
    }
}