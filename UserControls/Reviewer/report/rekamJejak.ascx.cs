using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using simlitekkes.Models.Pengusul;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Reviewer.report
{
    public partial class rekamJejak : System.Web.UI.UserControl
    {
        identitasUsulan objMdlIdUsulan = new identitasUsulan();
        berandaPengusul objPengusul = new berandaPengusul();
        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void unduhRekamJejakKetuaNAnggota(string pIdUsulanKegiatan)
        {
            //byte[] pdfDepan = GetBytePdfDepan(pIdUsulanKegiatan);
            DataTable dt = new DataTable();
            objMdlIdUsulan.getDetailUsulanKegiatan(ref dt, pIdUsulanKegiatan);

            //initUnduh(pIdUsulanKegiatan, dt.Rows[0]["id_personal"].ToString(),
            //    dt.Rows[0]["nama_ketua"].ToString(), dt.Rows[0]["thn_usulan_kegiatan"].ToString());

            byte[] biodataKetua = GetBytePdfBiodataKetua(dt.Rows[0]["id_personal"].ToString());
            ByteArrayOutputStream ms = new ByteArrayOutputStream();
            PdfDocument pdocDepan = new PdfDocument(new PdfReader(new MemoryStream(biodataKetua)), new PdfWriter(ms));
            pdocDepan.SetDefaultPageSize(new PageSize(PageSize.A4));

            DataTable dta = new DataTable();
            objAnggota.listAnggotaDikti(ref dta, Guid.Parse(pIdUsulanKegiatan));

            if (dta.Rows.Count > 0)
            {
                for (int a = 0; a < dta.Rows.Count; a++)
                {
                    byte[] biodataAnggota = GetBytePdfBiodataAnggota(dta.Rows[a]["id_personal"].ToString(), a + 1);
                    PdfDocument pdocAnggota = new PdfDocument(new PdfReader(new MemoryStream(biodataAnggota)));
                    pdocAnggota.CopyPagesTo(1, pdocAnggota.GetNumberOfPages(), pdocDepan);
                }
            }


            Document document = new Document(pdocDepan);
            document.Close();
            downloadPdf(ms.ToArray(), dt.Rows[0]["nama_ketua"].ToString());

        }

        private void downloadPdf(byte[] bytes, string namaKetua)
        {
            string namaFile = "RekamJejak " + namaKetua + ".pdf";
            //byte[] docData = mem.GetBuffer();
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.End();
        }
        protected byte[] GetBytePdfBiodataKetua(string pidPersonal)
        {
            //get html
            var outHTML = string.Empty;
            Session["idPersonalKetua"] = pidPersonal;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute("~/UserControls/Pengusul/report/biodataKetua.aspx?id_personal=" + pidPersonal, stringWriter);
                outHTML = stringWriter.ToString();
                stringWriter.Close();
            }
            var converterProperties = new ConverterProperties();
            converterProperties.SetBaseUri(Server.MapPath("~/"));
            var memoryStream = new MemoryStream();
            HtmlConverter.ConvertToPdf(outHTML, memoryStream, converterProperties);
            return memoryStream.ToArray();
        }


        protected byte[] GetBytePdfBiodataAnggota(string idPersonal, int urutanAngggota)
        {
            //get html
            var outHTML = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute(string.Format(@"~/UserControls/Pengusul/report/biodataAnggota.aspx?id_personal={0}&urutan={1}",
                    idPersonal, urutanAngggota.ToString()),
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