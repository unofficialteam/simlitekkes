using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using simlitekkes.Models.pelaksanaan;
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
    public partial class pdfLaporanKemajuanKontrol : System.Web.UI.UserControl
    {


        //usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        Models.Pengusul.mitraPenelitian objMitra = new Models.Pengusul.mitraPenelitian();
        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();
        identitasUsulan objMdlIdUsulan = new identitasUsulan();
        berandaPengusul objPengusul = new berandaPengusul();
        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        Models.Pengusul.lapKemajuan modelLapKemajuan = new Models.Pengusul.lapKemajuan();

        protected void Page_Load(object sender, EventArgs e)
        {
            //UnduhProposalLengkap("7414f38d-bc90-4117-b996-4ea131c299c0");
        }
        //public void UnduhProposalLengkap(string pIdUsulanKegiatan)
        public void unduhLapKemajuan(string pIdTransaksiKegiatan)
        {
            string pIdUsulanKegiatan = string.Empty; // "031914c1-9b36-489d-9b70-c49ba59e4baf";
            string thnPelaksanaan = string.Empty; //"2019";
            DataTable dt0 = new DataTable();
            mdlLapKemajuan.getRingkasan(ref dt0, pIdTransaksiKegiatan); 
            if (dt0.Rows.Count > 0)
            {
                pIdUsulanKegiatan = dt0.Rows[0]["id_usulan_kegiatan"].ToString();
                thnPelaksanaan = dt0.Rows[0]["thn_pelaksanaan_kegiatan"].ToString();                
            }
            else
            {
                return;
            }

            DataTable dtm = new DataTable();
            modelLapKemajuan.getIdTransaksiDimonev(ref dtm, pIdUsulanKegiatan);
            string sts_laporan_akhir = dtm.Rows[0]["sts_laporan_akhir"].ToString();
            //sts_laporan_akhir = "0"; // sementara simlitabkes lap kemajuan saja

            string page = Session["page"].ToString();
            if (page == "5" || page == "29" || page == "35")
            {
                sts_laporan_akhir = "1";
            }

            DataTable dt = new DataTable();
            objMdlIdUsulan.getDetailUsulanKegiatan(ref dt, pIdUsulanKegiatan);
            ViewState["thn_pelaksanaan_kegiatan"] = dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString();
            initUnduh(pIdUsulanKegiatan, pIdTransaksiKegiatan, dt.Rows[0]["id_personal"].ToString(),
                dt.Rows[0]["nama_ketua"].ToString(), dt.Rows[0]["thn_usulan_kegiatan"].ToString(),
                dt.Rows[0]["id_skema"].ToString(), thnPelaksanaan, sts_laporan_akhir);
        }

        private void initUnduh(string pIdUsulanKegiatan, string pIdTransaksiKegiatan, string pIdPersonal, 
            string namaKetua, string thnUsulan,
            string idSkema, string thnPelaksanaan,
            string sts_laporan_akhir)
        {
            byte[] pdfDepan = GetBytePdfDepan(pIdTransaksiKegiatan);
            ByteArrayOutputStream ms = new ByteArrayOutputStream();
            PdfDocument pdocDepan = new PdfDocument(new PdfReader(new MemoryStream(pdfDepan)), new PdfWriter(ms));
            pdocDepan.SetDefaultPageSize(new PageSize(PageSize.A4));

            PdfDocument pdfLapKemajuan = getPdfDokumenLapKemajuan(pIdTransaksiKegiatan, thnPelaksanaan, sts_laporan_akhir);
            if (pdfLapKemajuan != null)
                pdfLapKemajuan.CopyPagesTo(1, pdfLapKemajuan.GetNumberOfPages(), pdocDepan);
            
            // get all file bukti luaran
            // Wajib
            DataTable dtWajib = new DataTable();
            mdlLapKemajuan.ListDokumenLuaranDicapai(ref dtWajib, Guid.Parse( pIdTransaksiKegiatan), 1);
            string luaranBelumDiisi = string.Empty;
            int jmlBelumDiisi = 1;
            
            for(int a=0; a<dtWajib.Rows.Count; a++)
            {
                if (dtWajib.Rows[a]["nama_target_jenis_luaran"].ToString().Trim() != "")
                {
                    string[] arr_jenis_dokumen_bukti_luaran = dtWajib.Rows[a]["arr_jenis_dokumen_bukti_luaran"].ToString().Trim().Split('#');
                    string[] arr_jenis_dokumen_bukti_luaran_all = dtWajib.Rows[a]["arr_jenis_dokumen_bukti_luaran_all"].ToString().Trim().Split('#');

                    string strWajib = string.Empty;
                    for (int b = 0; b < arr_jenis_dokumen_bukti_luaran_all.Length; b++)
                    {
                        strWajib += " \t" + (b + 1).ToString() + ". " + arr_jenis_dokumen_bukti_luaran_all[b] + "\n";
                    }
                    string strSdhDiunggah = string.Empty;

                    int jmlDokSdhDiunggah = arr_jenis_dokumen_bukti_luaran.Length;
                    if (jmlDokSdhDiunggah > arr_jenis_dokumen_bukti_luaran_all.Length)
                        jmlDokSdhDiunggah = arr_jenis_dokumen_bukti_luaran_all.Length;

                    if (arr_jenis_dokumen_bukti_luaran.Length == 1 && arr_jenis_dokumen_bukti_luaran[0].Trim() == "")
                    {
                        strSdhDiunggah = " \t-";
                        jmlDokSdhDiunggah = 0;
                    }
                    else
                    {
                        for (int b = 0; b < jmlDokSdhDiunggah; b++)
                        {
                            strSdhDiunggah += " \t" + (b + 1).ToString() + ". " + arr_jenis_dokumen_bukti_luaran[b] + "\n";
                        }
                    }
                    //List<string> lstBelum = arr_jenis_dokumen_bukti_luaran_all.ToList();
                    //for (int b = 0; b < arr_jenis_dokumen_bukti_luaran.Length; b++)
                    //{
                    //    lstBelum.Remove(arr_jenis_dokumen_bukti_luaran[b].Trim());
                    //}
                    int jmlBlmDIunggah = 0;
                    if (arr_jenis_dokumen_bukti_luaran_all.Length > jmlDokSdhDiunggah)
                    {
                        jmlBlmDIunggah = arr_jenis_dokumen_bukti_luaran_all.Length - jmlDokSdhDiunggah;
                    }
                    string[] arrBelum = new string[jmlBlmDIunggah];


                    string strBelumDiunggah = string.Empty;
                    if (arrBelum.Length <= 0)
                    {
                        strBelumDiunggah = " \t-";
                    }
                    else
                    {
                        int idx = 0;
                        for (int x = 0; x < arr_jenis_dokumen_bukti_luaran_all.Length; x++)
                        {
                            bool isExists = false;
                            //string strYgBlm = string.Empty;
                            for (int y = 0; y < jmlDokSdhDiunggah; y++)
                            {
                                if (arr_jenis_dokumen_bukti_luaran_all[x].Trim().ToLower().Equals(arr_jenis_dokumen_bukti_luaran[y].Trim().ToLower()))
                                {
                                    //arrBelum[idx] = arr_jenis_dokumen_bukti_luaran_all
                                    //strYgBlm = arr_jenis_dokumen_bukti_luaran_all[y];
                                    isExists = true;
                                }
                            }
                            if (!isExists)
                            {
                                if (idx < arrBelum.Length)
                                    arrBelum[idx] = arr_jenis_dokumen_bukti_luaran_all[x];
                                idx++;
                            }
                        }

                        for (int b = 0; b < arrBelum.Length; b++)
                        {
                            strBelumDiunggah += " \t" + (b + 1).ToString() + ". " + arrBelum[b] + "\n";
                        }
                    }

                    string id_luaran_dijanjikan = dtWajib.Rows[a]["id_luaran_dijanjikan"].ToString();
                    DataTable dtInfo = new DataTable();
                    mdlLapKemajuan.getInfoCapaianLuaran(ref dtInfo, id_luaran_dijanjikan, pIdTransaksiKegiatan);
                    string informasi = dtInfo.Rows[0]["informasi"].ToString();
                    informasi = informasi.Replace("<br>", " \n ");
                    byte[] judulLuaran = CreateBreakTitlePage("Dokumen pendukung luaran Wajib #" + (a + 1).ToString(),
                        "Luaran dijanjikan: " + dtWajib.Rows[a]["nama_jenis_luaran"].ToString().Trim() + "\n" +
                        "\nTarget: \t" + dtWajib.Rows[a]["nama_target_capaian_luaran"].ToString().Trim() + "\n" +
                        "Dicapai: \t" + dtWajib.Rows[a]["nama_target_jenis_luaran"].ToString().Trim() + "\n" +
                        "\nDokumen wajib diunggah: \n" + strWajib +
                        "\nDokumen sudah diunggah: \n" + strSdhDiunggah +
                        "\nDokumen belum diunggah: \n" + strBelumDiunggah +
                        "\n" +
                        informasi
                        ); 

                    PdfDocument pdfJudulLuaran = new PdfDocument(new PdfReader(new MemoryStream(judulLuaran)));
                    pdfJudulLuaran.CopyPagesTo(1, pdfJudulLuaran.GetNumberOfPages(), pdocDepan);
                    //nama_jenis_luaran

                    string[] arr_id_dokumen_bukti_luaran = dtWajib.Rows[a]["arr_id_dokumen_bukti_luaran"].ToString().Trim().Split('#');


                    DataTable dtIdDokumen = new DataTable();
                    mdlLapKemajuan.listIDDokumenDiunggah(ref dtIdDokumen, Guid.Parse(id_luaran_dijanjikan),
                        Guid.Parse(pIdTransaksiKegiatan));

                    for (int c=0; c< dtIdDokumen.Rows.Count; c++)
                    {
                        if (c < arr_jenis_dokumen_bukti_luaran.Length)
                        {
                            PdfDocument pdfDokLapKemajuan = getPdfDokumenBuktiLuaran(dtIdDokumen.Rows[c]["id_dokumen_bukti_luaran"].ToString()
                            , arr_jenis_dokumen_bukti_luaran[c], thnPelaksanaan, sts_laporan_akhir);
                            if (pdfDokLapKemajuan != null)
                                pdfDokLapKemajuan.CopyPagesTo(1, pdfDokLapKemajuan.GetNumberOfPages(), pdocDepan);
                        }
                    }
                }
                else
                {
                    //byte[] judulLuaran = CreateBreakTitlePage("Luaran belum diisi",  
                    luaranBelumDiisi += jmlBelumDiisi.ToString() +". "+ dtWajib.Rows[a]["nama_jenis_luaran"].ToString().Trim() 
                        +", target: "+ dtWajib.Rows[a]["nama_target_capaian_luaran"].ToString().Trim() + "\n";
                    jmlBelumDiisi++;
                }

                

            }
            if(luaranBelumDiisi != "")
            {
                byte[] judulLuaranBlm = CreateBreakTitlePage("Daftar capaian Luaran Wajib belum diisi:", luaranBelumDiisi);
                PdfDocument pdfBJudulLuaranBlm = new PdfDocument(new PdfReader(new MemoryStream(judulLuaranBlm)));
                pdfBJudulLuaranBlm.CopyPagesTo(1, pdfBJudulLuaranBlm.GetNumberOfPages(), pdocDepan);
            }

           
            //byte[] judulLuaran = CreateBreakTitlePage(string judul, string pesan = "")
            
            // Tambahan 
            dtWajib = new DataTable();
            dtWajib.Clear();
            mdlLapKemajuan.ListDokumenLuaranDicapai(ref dtWajib, Guid.Parse(pIdTransaksiKegiatan), 2);
            luaranBelumDiisi = string.Empty;
            jmlBelumDiisi = 1;
            for (int a = 0; a < dtWajib.Rows.Count; a++)
            {
                if (dtWajib.Rows[a]["nama_target_jenis_luaran"].ToString().Trim() != "")
                {
                    string[] arr_jenis_dokumen_bukti_luaran = dtWajib.Rows[a]["arr_jenis_dokumen_bukti_luaran"].ToString().Trim().Split('#');
                    string[] arr_jenis_dokumen_bukti_luaran_all = dtWajib.Rows[a]["arr_jenis_dokumen_bukti_luaran_all"].ToString().Trim().Split('#');

                    string strWajib = string.Empty;
                    for (int b = 0; b < arr_jenis_dokumen_bukti_luaran_all.Length; b++)
                    {
                        strWajib += " \t" + (b + 1).ToString() + ". " + arr_jenis_dokumen_bukti_luaran_all[b] + "\n";
                    }
                    string strSdhDiunggah = string.Empty;
                    int jmlDokSdhDiunggah = arr_jenis_dokumen_bukti_luaran.Length;
                    if (jmlDokSdhDiunggah > arr_jenis_dokumen_bukti_luaran_all.Length)
                        jmlDokSdhDiunggah = arr_jenis_dokumen_bukti_luaran_all.Length;

                    if (arr_jenis_dokumen_bukti_luaran.Length == 1 && arr_jenis_dokumen_bukti_luaran[0].Trim() == "")
                    {
                        strSdhDiunggah = " \t-";
                        jmlDokSdhDiunggah = 0;
                    }
                    else
                    {
                        for (int b = 0; b < jmlDokSdhDiunggah; b++)
                        {
                            strSdhDiunggah += " \t" + (b + 1).ToString() + ". " + arr_jenis_dokumen_bukti_luaran[b] + "\n";
                        }
                    }
                    //List<string> lstBelum = arr_jenis_dokumen_bukti_luaran_all.ToList();
                    //for (int b = 0; b < arr_jenis_dokumen_bukti_luaran.Length; b++)
                    //{
                    //    lstBelum.Remove(arr_jenis_dokumen_bukti_luaran[b].Trim());
                    //}
                    int jmlBlmDIunggah = 0;
                    if (arr_jenis_dokumen_bukti_luaran_all.Length > jmlDokSdhDiunggah)
                    {
                        jmlBlmDIunggah = arr_jenis_dokumen_bukti_luaran_all.Length - jmlDokSdhDiunggah;
                    }
                    string[] arrBelum = new string[jmlBlmDIunggah];



                    string strBelumDiunggah = string.Empty;
                    if (arrBelum.Length <= 0)
                    {
                        strBelumDiunggah = " \t-";
                    }
                    else
                    {
                        int idx = 0;
                        for (int x = 0; x < arr_jenis_dokumen_bukti_luaran_all.Length; x++)
                        {
                            bool isExists = false;
                            //string strYgBlm = string.Empty;
                            for (int y = 0; y < jmlDokSdhDiunggah; y++)
                            {
                                if (arr_jenis_dokumen_bukti_luaran_all[x].Trim().ToLower().Equals(arr_jenis_dokumen_bukti_luaran[y].Trim().ToLower()))
                                {
                                    //arrBelum[idx] = arr_jenis_dokumen_bukti_luaran_all
                                    //strYgBlm = arr_jenis_dokumen_bukti_luaran_all[y];
                                    isExists = true;
                                }
                            }
                            if (!isExists)
                            {
                                if (idx < arrBelum.Length)
                                    arrBelum[idx] = arr_jenis_dokumen_bukti_luaran_all[x];
                                idx++;
                            }
                        }

                        for (int b = 0; b < arrBelum.Length; b++)
                        {
                            strBelumDiunggah += " \t" + (b + 1).ToString() + ". " + arrBelum[b] + "\n";
                        }
                    }


                    string id_luaran_dijanjikan = dtWajib.Rows[a]["id_luaran_dijanjikan"].ToString();
                    DataTable dtInfo = new DataTable();
                    mdlLapKemajuan.getInfoCapaianLuaran(ref dtInfo, id_luaran_dijanjikan, pIdTransaksiKegiatan);
                    string informasi = dtInfo.Rows[0]["informasi"].ToString();
                    informasi = informasi.Replace("<br>", " \n ");
                    byte[] judulLuaran = CreateBreakTitlePage("Dokumen pendukung luaran Tambahan #" + (a + 1).ToString(),
                        "Luaran dijanjikan: " + dtWajib.Rows[a]["nama_jenis_luaran"].ToString().Trim() + "\n" +
                        "\nTarget: \t" + dtWajib.Rows[a]["nama_target_capaian_luaran"].ToString().Trim() + "\n" +
                        "Dicapai: \t" + dtWajib.Rows[a]["nama_target_jenis_luaran"].ToString().Trim() + "\n" +
                        "\nDokumen wajib diunggah: \n" + strWajib +
                        "\nDokumen sudah diunggah: \n" + strSdhDiunggah +
                        "\nDokumen belum diunggah: \n" + strBelumDiunggah +
                        "\n" +
                        informasi
                        );

                    PdfDocument pdfJudulLuaran = new PdfDocument(new PdfReader(new MemoryStream(judulLuaran)));
                    pdfJudulLuaran.CopyPagesTo(1, pdfJudulLuaran.GetNumberOfPages(), pdocDepan);
                    //nama_jenis_luaran

                    string[] arr_id_dokumen_bukti_luaran = dtWajib.Rows[a]["arr_id_dokumen_bukti_luaran"].ToString().Trim().Split('#');
                    
                    DataTable dtIdDokumen = new DataTable();
                    mdlLapKemajuan.listIDDokumenDiunggah(ref dtIdDokumen, Guid.Parse(id_luaran_dijanjikan), 
                        Guid.Parse(pIdTransaksiKegiatan)
                        );

                    for (int c = 0; c < dtIdDokumen.Rows.Count; c++)
                    {
                        if (c < arr_jenis_dokumen_bukti_luaran.Length)
                        {
                            PdfDocument pdfDokLapKemajuan = getPdfDokumenBuktiLuaran(dtIdDokumen.Rows[c]["id_dokumen_bukti_luaran"].ToString()
                                , arr_jenis_dokumen_bukti_luaran[c], thnPelaksanaan, sts_laporan_akhir);
                            if (pdfDokLapKemajuan != null)
                                pdfDokLapKemajuan.CopyPagesTo(1, pdfDokLapKemajuan.GetNumberOfPages(), pdocDepan);
                        }
                    }
                }
                else
                {
                    //byte[] judulLuaran = CreateBreakTitlePage("Luaran belum diisi",  
                    luaranBelumDiisi += jmlBelumDiisi.ToString() + ". " + dtWajib.Rows[a]["nama_jenis_luaran"].ToString().Trim()
                        + ", target: " + dtWajib.Rows[a]["nama_target_capaian_luaran"].ToString().Trim() + "\n";
                    jmlBelumDiisi++;
                }



            }
            if (luaranBelumDiisi != "")
            {
                byte[] judulLuaranBlm = CreateBreakTitlePage("Daftar capaian Luaran Tambahan belum diisi:", luaranBelumDiisi);
                PdfDocument pdfBJudulLuaranBlm = new PdfDocument(new PdfReader(new MemoryStream(judulLuaranBlm)));
                pdfBJudulLuaranBlm.CopyPagesTo(1, pdfBJudulLuaranBlm.GetNumberOfPages(), pdocDepan);
            }

            PdfDocument pdfDokRealisasiMitra = getMitraDokumenRealisasi( pIdTransaksiKegiatan, thnPelaksanaan);
            if (pdfDokRealisasiMitra != null)
            {

                byte[] judulDokRealisasiMitra = CreateBreakTitlePage("Dokumen Realisasi Mitra");
                PdfDocument pdfJudulDokRealisasiMitra = new PdfDocument(new PdfReader(new MemoryStream(judulDokRealisasiMitra)));
                pdfJudulDokRealisasiMitra.CopyPagesTo(1, pdfJudulDokRealisasiMitra.GetNumberOfPages(), pdocDepan);
                pdfDokRealisasiMitra.CopyPagesTo(1, pdfDokRealisasiMitra.GetNumberOfPages(), pdocDepan);
            }            

            Document document = new Document(pdocDepan);
            document.Close();
            downloadPdf(ms.ToArray(), namaKetua, sts_laporan_akhir);
        }


        protected PdfDocument getPdfDokumenLapKemajuan(string pIdTransaksiKegiatan, string thnPelaksanaan, string sts_laporan_akhir)
        {
            string path = string.Format("~/fileUpload/laporan_kemajuan/{0}/", thnPelaksanaan);

            if (sts_laporan_akhir == "1")
            {
                path = string.Format("~/fileUpload/laporan_akhir/{0}/", thnPelaksanaan);
            }

            string filePath = string.Format("{0}{1}.pdf", path.ToString(), pIdTransaksiKegiatan);

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

        protected PdfDocument getPdfDokumenBuktiLuaran(string idDokumen, string namaDokumen, string thnPelaksanaan, string sts_laporan_akhir)
        {

            string path = string.Format("~/fileUpload/laporan_kemajuan/BuktiLuaran/{0}/", thnPelaksanaan);
            if (sts_laporan_akhir == "1")
            {
                path = string.Format("~/fileUpload/laporan_akhir/BuktiLuaran/{0}/", thnPelaksanaan);
            }

            string filePath = string.Format("{0}{1}.pdf", path.ToString(), idDokumen);
            if (!File.Exists(Server.MapPath(filePath)))
            {
                byte[] doCTdkDitemukan = CreateBreakTitlePage(string.Format("Dokumen \"{0}\" tidak ditemukan.", namaDokumen));
                PdfDocument pdfDoCTdkDitemukan = new PdfDocument(new PdfReader(new MemoryStream(doCTdkDitemukan)));
                return pdfDoCTdkDitemukan;
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

            
            document.SetMargins(60, 60, 60, 70);
            
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

        void downloadPdf(byte[] bytes, string namaKetua, string sts_laporan_akhir)
        {
            string namaFile = "LaporanKemajuan " + namaKetua + ".pdf";

            if (sts_laporan_akhir == "1")
            {
                namaFile = "LaporanAkhir " + namaKetua + ".pdf";
            }
            //byte[] docData = mem.GetBuffer();
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.End();
        }

        protected byte[] GetBytePdfDepan(string pIdTransaksiKegiatan)
        {
            //get html
            //string pIdTransaksiKegiatan = "814f022b-2a3f-465f-8f8b-45be5da4cb4b";
            var outHTML = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                //string aspxUrl = "~/Helper/WebForm1.aspx";
                //aspxUrl = ("~/UserControls/Pengusul/report/pdfFullProposal.aspx?id_usulan_kegiatan=" + pIdUsulanKegiatan + "&is_perbaikan=" + thnUsulan);
                string aspxUrl = "~/UserControls/Pengusul/report/pdfLaporanKemajuan.aspx?id_transaksi_kegiatan=" + pIdTransaksiKegiatan ;

                Server.Execute(aspxUrl, stringWriter);
                
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


        protected PdfDocument getMitraDokumenRealisasi(string idTransaksiKegiatan, string thnPelaksanaan)
        {
            //string filePath = "~/fileUpload/mitra/" + idMitra + ".pdf";
            //'~/fileUpload/realisasi_mitra/2021/d872ceec-8f82-45c7-ae19-83c172a9f975.pdf'

            string filePath = "~/fileUpload/realisasi_mitra/{0}/{1}.pdf";
            filePath = string.Format(filePath, thnPelaksanaan, idTransaksiKegiatan);


            bool exists = true;
            if (!File.Exists(Server.MapPath(filePath)))
            {
                exists = false;
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