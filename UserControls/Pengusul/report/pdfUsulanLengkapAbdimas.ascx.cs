using iText.Forms;
using iText.Html2pdf;
using iText.IO.Font;
using iText.IO.Source;
using iText.Kernel;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using simlitekkes.Models.Pengusul;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.report
{
    public partial class pdfUsulanLengkapAbdimas : System.Web.UI.UserControl
    {
        //usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        //Models.Pengusul.mitraPenelitian objMitra = new Models.Pengusul.mitraPenelitian();
        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();
        identitasUsulan objMdlIdUsulan = new identitasUsulan();
        Models.Pengusul.mitraAbdimas objMitraAbdimas = new Models.Pengusul.mitraAbdimas();

        berandaPengusul objPengusul = new berandaPengusul();

        protected void Page_Load(object sender, EventArgs e)
        {
            //UnduhProposalLengkap("7414f38d-bc90-4117-b996-4ea131c299c0");
        }
        public void UnduhProposalLengkap(string pIdUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objMdlIdUsulan.getDetailUsulanKegiatan(ref dt, pIdUsulanKegiatan);

            initUnduh(pIdUsulanKegiatan, dt.Rows[0]["id_personal"].ToString(),
                dt.Rows[0]["nama_ketua"].ToString(), dt.Rows[0]["thn_usulan_kegiatan"].ToString());
        }

        private void initUnduh(string pIdUsulanKegiatan, string pIdPersonal, string namaKetua, string thnUsulan)
        {
            //Session["usulan_kegiatan"] = ViewState["usulan_kegiatan"];
            //objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];

            byte[] pdfDepan = GetBytePdfDepan(pIdUsulanKegiatan);
            ByteArrayOutputStream ms = new ByteArrayOutputStream();
            PdfDocument pdocDepan = new PdfDocument(new PdfReader(new MemoryStream(pdfDepan)), new PdfWriter(ms));
            pdocDepan.SetDefaultPageSize(new PageSize(PageSize.A4));

            PdfDocument pdfDokumenUsulan = getPdfDokumenUsulan(pIdUsulanKegiatan, thnUsulan);
            if (pdfDokumenUsulan != null)
            {
                pdfDokumenUsulan.CopyPagesTo(1, pdfDokumenUsulan.GetNumberOfPages(), pdocDepan, new PdfPageFormCopier());
                pdfDokumenUsulan.Close();
            }
            /**/
            //objLogin = (Models.login)Session["objLogin"];
            byte[] biodataKetua = GetBytePdfBiodataKetua(pIdPersonal);
            PdfDocument pdocKetua = new PdfDocument(new PdfReader(new MemoryStream(biodataKetua)));
            pdocKetua.CopyPagesTo(1, pdocKetua.GetNumberOfPages(), pdocDepan, new PdfPageFormCopier());
            pdocKetua.Close();

            DataTable dta = new DataTable();
            objAnggota.listAnggotaDikti(ref dta, Guid.Parse(pIdUsulanKegiatan));

            if (dta.Rows.Count > 0)
            {
                for (int a = 0; a < dta.Rows.Count; a++)
                {
                    //Session["idPersonalAnggota"] = dta.Rows[a]["id_personal"].ToString();
                    //Session["urutanAnggota"] = a + 1;
                    byte[] biodataAnggota = GetBytePdfBiodataAnggota(dta.Rows[a]["id_personal"].ToString(), a + 1);
                    PdfDocument pdocAnggota = new PdfDocument(new PdfReader(new MemoryStream(biodataAnggota)));
                    pdocAnggota.CopyPagesTo(1, pdocAnggota.GetNumberOfPages(), pdocDepan, new PdfPageFormCopier());
                    pdocAnggota.Close();
                }
            }

            //byte[] breakLampiran2 = CreateBreakTitlePage("LAMPIRAN 2. SURAT PERNYATAAN MITRA");

            //PdfDocument pdfbreakLampiran2 = new PdfDocument(new PdfReader(new MemoryStream(breakLampiran2)));
            //pdfbreakLampiran2.CopyPagesTo(1, pdfbreakLampiran2.GetNumberOfPages(), pdocDepan, new PdfPageFormCopier());
            //pdfbreakLampiran2.Close();

            DataTable dtm = new DataTable();
            dtm = GetDataListIdMitra(pIdUsulanKegiatan);
            if (dtm.Rows.Count > 0)
            {
                for (int a = 0; a < dtm.Rows.Count; a++)
                {
                    PdfDocument pdfMitra = getMitraDokumen(dtm.Rows[a]["id_mitra_abdimas"].ToString(), thnUsulan);
                    if (pdfMitra != null )
                    {
                        pdfMitra.CopyPagesTo(1, pdfMitra.GetNumberOfPages(), pdocDepan, new PdfPageFormCopier());
                        pdfMitra.Close();
                    }
                }
            }

            byte[] breakLampiran3 = CreateBreakTitlePage("LAMPIRAN 3. BUKTI PEROLEHAN KI");
            PdfDocument pdfbreakLampiran3 = new PdfDocument(new PdfReader(new MemoryStream(breakLampiran3)));
            pdfbreakLampiran3.CopyPagesTo(1, pdfbreakLampiran3.GetNumberOfPages(), pdocDepan, new PdfPageFormCopier());
            pdfbreakLampiran3.Close();

            DataTable dtHki = new DataTable();
            dtHki.Clear();
            dtHki = getDtHKI(pIdPersonal);
            if (dtHki.Rows.Count > 0)
            {
                for (int a = 0; a < dtHki.Rows.Count; a++)
                {
                    PdfDocument pdfHki = getDokumenHki(dtHki.Rows[a]["id_hak_kekayaan_intelektual"].ToString());
                    if (pdfHki != null)
                    {
                        pdfHki.CopyPagesTo(1, pdfHki.GetNumberOfPages(), pdocDepan, new PdfPageFormCopier());
                        pdfHki.Close();
                    }
                }
            }

            // Anggota 
            if (dta.Rows.Count > 0)
            {
                for (int a = 0; a < dta.Rows.Count; a++)
                {
                    DataTable dtHkiAgt = new DataTable();
                    dtHkiAgt.Clear();
                    dtHkiAgt = getDtHKI(dta.Rows[a]["id_personal"].ToString());
                    for (int b = 0; b < dtHkiAgt.Rows.Count; b++)
                    {
                        PdfDocument pdfHkia = getDokumenHki(dtHkiAgt.Rows[b]["id_hak_kekayaan_intelektual"].ToString());
                        if (pdfHkia != null)
                        {
                            pdfHkia.CopyPagesTo(1, pdfHkia.GetNumberOfPages(), pdocDepan, new PdfPageFormCopier());
                            pdfHkia.Close();
                        }
                    }
                }
            }

            byte[] bAproval = GetBytePdfAproval(pIdUsulanKegiatan);
            PdfDocument pdfAproval = new PdfDocument(new PdfReader(new MemoryStream(bAproval)));
            pdfAproval.CopyPagesTo(1, pdfAproval.GetNumberOfPages(), pdocDepan, new PdfPageFormCopier());
            pdfAproval.Close();

            Document document = new Document(pdocDepan);
            document.Close();
            downloadPdf(ms.ToArray(), namaKetua);
        }

        DataTable getDtHKI(string idPersonal)
        {
            DataTable dtHKI = new DataTable();
            //string[] kolomHKI = { "id_hak_kekayaan_intelektual" };
            objPengusul.currentRecords.Clear();
            if (objPengusul.listHKI(ref dtHKI, Guid.Parse(idPersonal)))
                dtHKI = objPengusul.currentRecords;
            return dtHKI;
        }

        void downloadPdf(byte[] bytes, string namaKetua)
        {
            string namaFile = "UsulanLengkapPengabdian " + namaKetua + ".pdf";
            //byte[] docData = mem.GetBuffer();
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.End();
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

        protected byte[] GetBytePdfDepan(string pIdUsulanKegiatan)
        {
            //get html
            var outHTML = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute("~/UserControls/Pengusul/report/pdfFullProposalAbdimas.aspx?id_usulan_kegiatan=" + pIdUsulanKegiatan, stringWriter);
                outHTML = stringWriter.ToString();
                stringWriter.Close();
            }
            var converterProperties = new ConverterProperties();
            converterProperties.SetBaseUri(Server.MapPath("~/"));
            var memoryStream = new MemoryStream();
            HtmlConverter.ConvertToPdf(outHTML, memoryStream, converterProperties);
            return memoryStream.ToArray();
        }

        protected byte[] GetBytePdfBiodataKetua(string pidPersonal)
        {
            //get html
            var outHTML = string.Empty;
            Session["idPersonalKetua"] = pidPersonal;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute(string.Format("~/UserControls/Pengusul/report/biodataKetua.aspx?id_personal={0}&abdimas=1", pidPersonal), stringWriter);
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
                Server.Execute(string.Format(@"~/UserControls/Pengusul/report/biodataAnggota.aspx?id_personal={0}&urutan={1}&abdimas=1",
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

        protected PdfDocument getPdfDokumenUsulan(string pIdUsulanKegiatan, string thnUsulan)
        {
            string filePath = "~/fileUpload/Dokumenusulan/" +
                    thnUsulan + "/" +
                    pIdUsulanKegiatan + ".pdf";

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

        protected DataTable GetDataListIdMitra(string idUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objMitraAbdimas.listMitraPelaksanaPengabdian(ref dt, Guid.Parse(idUsulanKegiatan));
            return dt;
        }

        protected PdfDocument getMitraDokumen(string idMitra, string thnUsulan)
        {
            string filePath = "~/fileUpload/mitra/"+ thnUsulan+ "/" + idMitra + ".pdf";
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
                byte[] breakLampiran2 = CreateBreakTitlePage("- Ada berkas Mitra yang tidak dapat dibuka.", "Kesalahan: " + pe.Message);
                PdfDocument pdfBrokenInfo = new PdfDocument(new PdfReader(new MemoryStream(breakLampiran2)).SetUnethicalReading(true));
                return pdfBrokenInfo;
            }
        }
        
        protected PdfDocument getDokumenHki(string idHki)
        {
            string filePath = "~/fileUpload/Hki/" + idHki + ".pdf";
            bool exists = true;
            if (!File.Exists(Server.MapPath(filePath)))
            {
                exists = false;
                //filePath = "~/fileUpload/Hki/Hki" + idHki + ".pdf";
                //if (!File.Exists(Server.MapPath(filePath)))
                //{
                //    exists = false;
                //}
            }
            if (exists)
            {
                try
                {
                    PdfDocument doc = new PdfDocument(new PdfReader(Server.MapPath(filePath)).SetUnethicalReading(true));
                    return doc;
                }
                catch (Exception pe)
                {
                    byte[] breakLampiran2 = CreateBreakTitlePage("- Ada berkas HKI yang tidak dapat dibuka.", "Kesalahan: " + pe.Message);
                    PdfDocument pdfBrokenInfo = new PdfDocument(new PdfReader(new MemoryStream(breakLampiran2)).SetUnethicalReading(true));
                    return pdfBrokenInfo;
                }
            }
            else
            {
                return null;
            }
        }

        protected byte[] GetBytePdfAproval(string pIdUsulanKegiatan)
        {
            var outHTML = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute("~/UserControls/Pengusul/report/pdfPersetujuan.aspx?id_usulan_kegiatan=" + pIdUsulanKegiatan, stringWriter);
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