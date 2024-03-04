using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using simlitekkes.Models.Pengusul;
using simlitekkes.Models.Sistem;
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
    public partial class pdfUsulanLengkapPerbaikan : System.Web.UI.UserControl
    {

        //usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        Models.Pengusul.mitraPenelitian objMitra = new Models.Pengusul.mitraPenelitian();
        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();
        identitasUsulan objMdlIdUsulan = new identitasUsulan();
        berandaPengusul objPengusul = new berandaPengusul();

        protected void Page_Load(object sender, EventArgs e)
        {
            //UnduhProposalLengkap("7414f38d-bc90-4117-b996-4ea131c299c0");
        }
        public void UnduhProposalLengkap(string pIdUsulanKegiatan, string pIdTransaksiKegiatan, string isPerbaikan)
        {
            DataTable dt = new DataTable();
            objMdlIdUsulan.getDetailUsulanKegiatan(ref dt, pIdUsulanKegiatan);

            initUnduh(pIdUsulanKegiatan, pIdTransaksiKegiatan, dt.Rows[0]["id_personal"].ToString(),
                dt.Rows[0]["nama_ketua"].ToString(), dt.Rows[0]["thn_usulan_kegiatan"].ToString(),
                dt.Rows[0]["id_skema"].ToString(), isPerbaikan);
        }

        private void initUnduh(string pIdUsulanKegiatan, string pIdTransaksiKegiata, string pIdPersonal, string namaKetua, string thnUsulan,
            string idSkema, string isPerbaikan)
        {
            //Session["usulan_kegiatan"] = ViewState["usulan_kegiatan"];
            //objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];

            byte[] pdfDepan = GetBytePdfDepan(pIdUsulanKegiatan, isPerbaikan);
            ByteArrayOutputStream ms = new ByteArrayOutputStream();
            PdfDocument pdocDepan = new PdfDocument(new PdfReader(new MemoryStream(pdfDepan)), new PdfWriter(ms));
            pdocDepan.SetDefaultPageSize(new PageSize(PageSize.A4));

            PdfDocument pdfDokumenUsulan = getPdfDokumenUsulanPerbaikan(pIdTransaksiKegiata, (int.Parse(thnUsulan) + 1).ToString(),
                "dokumenUsulanRevisi");
            if (pdfDokumenUsulan != null)
                pdfDokumenUsulan.CopyPagesTo(1, pdfDokumenUsulan.GetNumberOfPages(), pdocDepan);
            /**/
            //objLogin = (Models.login)Session["objLogin"];
            byte[] biodataKetua = GetBytePdfBiodataKetua(pIdPersonal);
            PdfDocument pdocKetua = new PdfDocument(new PdfReader(new MemoryStream(biodataKetua)));
            pdocKetua.CopyPagesTo(1, pdocKetua.GetNumberOfPages(), pdocDepan);

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
                    pdocAnggota.CopyPagesTo(1, pdocAnggota.GetNumberOfPages(), pdocDepan);
                }
            }

            //byte[] breakLampiran2 = CreateBreakTitlePage("LAMPIRAN 2. SURAT PERNYATAAN MITRA");

            //PdfDocument pdfbreakLampiran2 = new PdfDocument(new PdfReader(new MemoryStream(breakLampiran2)));
            //pdfbreakLampiran2.CopyPagesTo(1, pdfbreakLampiran2.GetNumberOfPages(), pdocDepan);

            DataTable dtm = new DataTable();
            dtm = GetDataListIdMitra(pIdUsulanKegiatan);
            if (dtm.Rows.Count > 0)
            {
                for (int a = 0; a < dtm.Rows.Count; a++)
                {
                    PdfDocument pdfMitra = getMitraDokumen(dtm.Rows[a]["id_mitra"].ToString(), thnUsulan);
                    if (pdfMitra != null)
                        pdfMitra.CopyPagesTo(1, pdfMitra.GetNumberOfPages(), pdocDepan);
                }
            }

            // lampiran KI
            //LAMPIRAN III BUKTI PEROLEHAN KI 


            byte[] breakLampiran3 = CreateBreakTitlePage("LAMPIRAN 3. BUKTI PEROLEHAN KI");
            PdfDocument pdfbreakLampiran3 = new PdfDocument(new PdfReader(new MemoryStream(breakLampiran3)));
            pdfbreakLampiran3.CopyPagesTo(1, pdfbreakLampiran3.GetNumberOfPages(), pdocDepan);

            DataTable dtHki = new DataTable();
            dtHki = getDtHKI(pIdPersonal);
            if (dtHki.Rows.Count > 0)
            {
                for (int a = 0; a < dtHki.Rows.Count; a++)
                {
                    PdfDocument pdfHki = getDokumenHki(dtHki.Rows[a]["id_hak_kekayaan_intelektual"].ToString());
                    if (pdfHki != null)
                        pdfHki.CopyPagesTo(1, pdfHki.GetNumberOfPages(), pdocDepan);
                }
            }

            // HKI Anggota 
            if (dta.Rows.Count > 0)
            {
                for (int a = 0; a < dta.Rows.Count; a++)
                {
                    DataTable dtHkiAgt = new DataTable();
                    dtHkiAgt = getDtHKI(dta.Rows[a]["id_personal"].ToString());
                    for (int b = 0; b < dtHkiAgt.Rows.Count; b++)
                    {
                        PdfDocument pdfHki = getDokumenHki(dtHkiAgt.Rows[b]["id_hak_kekayaan_intelektual"].ToString());
                        if (pdfHki != null)
                            pdfHki.CopyPagesTo(1, pdfHki.GetNumberOfPages(), pdocDepan);
                    }
                }
            }

            if (idSkema == "72") // KRUPT
            {
                byte[] breakLampiran4 = CreateBreakTitlePage("LAMPIRAN 4. Dokumen Work Breakdown Structure ");
                PdfDocument pdfbreakLampiran4 = new PdfDocument(new PdfReader(new MemoryStream(breakLampiran4)));
                pdfbreakLampiran4.CopyPagesTo(1, pdfbreakLampiran4.GetNumberOfPages(), pdocDepan);

                PdfDocument pdfDokumenWbs = getPdfDokumenUsulan(pIdUsulanKegiatan, thnUsulan, "dokumenWbs");
                if (pdfDokumenWbs != null)
                    pdfDokumenWbs.CopyPagesTo(1, pdfDokumenWbs.GetNumberOfPages(), pdocDepan);
            }

            byte[] bAproval = GetBytePdfAproval(pIdUsulanKegiatan);
            PdfDocument pdfAproval = new PdfDocument(new PdfReader(new MemoryStream(bAproval)));
            pdfAproval.CopyPagesTo(1, pdfAproval.GetNumberOfPages(), pdocDepan);

            Document document = new Document(pdocDepan);
            document.Close();
            downloadPdf(ms.ToArray(), namaKetua);
        }

        protected PdfDocument getDokumenHki(string idHki)
        {
            string filePath = "~/fileUpload/Hki/" + idHki + ".pdf";
            bool exists = true;
            if (!File.Exists(Server.MapPath(filePath)))
            {
                filePath = "~/fileUpload/Hki/Hki" + idHki + ".pdf";
                if (!File.Exists(Server.MapPath(filePath)))
                {
                    exists = false;
                }
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
            string namaFile = "UsulanLengkapPenelitian " + namaKetua + ".pdf";
            //byte[] docData = mem.GetBuffer();
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.End();
        }

        protected byte[] GetBytePdfDepan(string pIdUsulanKegiatan, string isPerbaikan)
        {
            //get html
            var outHTML = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute(("~/UserControls/Pengusul/report/pdfFullProposal.aspx?id_usulan_kegiatan=" + pIdUsulanKegiatan + "&is_perbaikan=" + isPerbaikan), stringWriter);
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

        protected PdfDocument getPdfDokumenUsulan(string pIdUsulanKegiatan, string thnUsulan, string jenisDokumen)
        {
            string filePath = "~/fileUpload/" + jenisDokumen + "/" +
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

        protected PdfDocument getPdfDokumenUsulanPerbaikan(string pIdTransaksiKegiatan, string tahun, string jenisDokumen)
        {
            string filePath = "~/fileUpload/" + jenisDokumen + "/" +
                    tahun + "/" +
                    pIdTransaksiKegiatan + ".pdf";
            if (!File.Exists(Server.MapPath(filePath)))
            {
                int itahun = int.Parse(tahun);
                itahun = itahun - 1;

                filePath = filePath = "~/fileUpload/" + jenisDokumen + "/" +
                    itahun.ToString() + "/" +
                    pIdTransaksiKegiatan + ".pdf";
                if (!File.Exists(Server.MapPath(filePath)))
                {
                    return null;
                }
            }
            //PdfDocument doc = new PdfDocument(new PdfReader(Server.MapPath(filePath)).SetUnethicalReading(true));
            //return doc;
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
            objMitra.listMitraPelaksanaPenelitian(ref dt, Guid.Parse(idUsulanKegiatan), 0, 0);
            return dt;
        }

        protected PdfDocument getMitraDokumen(string idMitra, string thnUsulan)
        {
            string filePath = "~/fileUpload/mitra/" + idMitra + ".pdf";
            bool exists = true;
            if (!File.Exists(Server.MapPath(filePath)))
            {
                filePath = "~/fileUpload/mitra/" + thnUsulan + "/" + idMitra + ".pdf";
                if (!File.Exists(Server.MapPath(filePath)))
                {
                    exists = false;
                }
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
                    byte[] breakLampiran2 = CreateBreakTitlePage("- Ada berkas Mitra yang tidak dapat dibuka.", "Kesalahan: " + pe.Message);
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
            //get html
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