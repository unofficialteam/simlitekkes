using iTextSharp.text;
using iTextSharp.text.pdf;
using simlitekkes.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pengusul.report
{
    public class templatePdf
    {

        #region Fields

        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        public int ukuranFont = 10;
        Font timesNormal;
        Font timesTebal;
        Font timesGarisBawah;
        int cellPadding = 2;

        #endregion

        #region Konstruktor dan Destruktor

        public templatePdf()
        {
            timesNormal = new Font(bfTimes, ukuranFont, Font.NORMAL);
            timesTebal = new Font(bfTimes, ukuranFont, Font.BOLD);
            timesGarisBawah = new Font(bfTimes, ukuranFont, Font.UNDERLINE);
        }

        ~templatePdf()
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public templatePdf(int newUkuranFont)
        {
            ukuranFont = newUkuranFont;
            timesNormal = new Font(bfTimes, ukuranFont, Font.NORMAL);
            timesTebal = new Font(bfTimes, ukuranFont, Font.BOLD);
            timesGarisBawah = new Font(bfTimes, ukuranFont, Font.UNDERLINE);
        }

        public void setCellPadding(int padding)
        {
            cellPadding = padding;
        }

        public PdfPTable createTableJudulHeader(string judulHalaman, string namaSkema)
        {
            PdfPTable tblJudul = new PdfPTable(1);
            tblJudul.WidthPercentage = 100;
            judulHalaman = judulHalaman.ToUpper();
            PdfPCell cell = new PdfPCell(new Phrase(judulHalaman, new Font(Font.FontFamily.TIMES_ROMAN, ukuranFont, Font.BOLD | Font.UNDERLINE)));
            cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            tblJudul.AddCell(cell);
            namaSkema = namaSkema.ToUpper();
            PdfPCell cell2 = null;
            cell2 = new PdfPCell(new Phrase(namaSkema, new Font(Font.FontFamily.TIMES_ROMAN, ukuranFont, Font.BOLD | Font.UNDERLINE)));
            cell2.BorderColor = iTextSharp.text.BaseColor.WHITE;
            cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            tblJudul.AddCell(cell2);
            return tblJudul;
        }
        public PdfPTable createTableJudulHeaderUraianUmum(string judulHalaman)
        {
            PdfPTable tblJudul = new PdfPTable(1);
            tblJudul.WidthPercentage = 100;
            judulHalaman = judulHalaman.ToUpper();
            PdfPCell cell = new PdfPCell(new Phrase(judulHalaman, new Font(Font.FontFamily.TIMES_ROMAN, ukuranFont, Font.BOLD | Font.UNDERLINE)));
            cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            tblJudul.AddCell(cell);
            //namaSkema = namaSkema.ToUpper();
            //PdfPCell cell2 = null;
            //cell2 = new PdfPCell(new Phrase(namaSkema, new Font(Font.FontFamily.TIMES_ROMAN, ukuranFont, Font.BOLD | Font.UNDERLINE)));
            //cell2.BorderColor = iTextSharp.text.BaseColor.WHITE;
            //cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            //tblJudul.AddCell(cell2);
            return tblJudul;
        }

        // 3 kolom dengan : 
        public PdfPTable createTable(DataTable dt, int kolMulai, ref int kolPause)
        {
            PdfPTable table = new PdfPTable(3);
            float[] widths = new float[] { 15f, 1f, 34f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);
            PdfPCell[] cell = new PdfPCell[dt.Rows.Count];
            int nCel = kolMulai;
            for (int i = 0; i < dt.Rows.Count + (dt.Rows.Count / 2); i++)
            {
                if ((i - 1) % 3 == 0)
                {
                    PdfPCell ccell = new PdfPCell(new Phrase(":", timesNormal));
                    if (dt.Rows[nCel-1][0].ToString() == "Ketua Peneliti" || // utk menghilangkan : 
                        (dt.Rows[nCel - 1][0].ToString() == "Biaya Penelitian") ||
                        dt.Rows[nCel - 1][0].ToString() == "2. Tim Peneliti" ||
                        dt.Rows[nCel - 1][0].ToString() == "2. Tim Pelaksana" ||
                        dt.Rows[nCel - 1][0].ToString() == "2. Perguruan Tinggi Pengusul"
                        ) 
                            ccell = new PdfPCell(new Phrase("", timesNormal));
                            ccell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                            table.AddCell(ccell);
                }
                else
                {
                    if (nCel < cell.Length)
                    {
                        cell[nCel] = new PdfPCell(new Phrase(dt.Rows[nCel][0].ToString().Replace("<br />","\n"), timesNormal));
                        cell[nCel].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cell[nCel]);
                        nCel++;
                    }
                }
                if (dt.Rows[nCel - 1][0].ToString() == "pause" || dt.Rows[nCel - 1][0].ToString() == "pause2")
                {
                    kolPause = nCel;
                    return table;
                }
            }
            return table;
        }


        // 3 kolom dengan : 
        public PdfPTable createTableIgnoreBlank(DataTable dt, int kolMulai, ref int kolPause)
        {
            PdfPTable table = new PdfPTable(3);
            float[] widths = new float[] { 15f, 1f, 34f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);
            PdfPCell[] cell = new PdfPCell[dt.Rows.Count];
            int nCel = kolMulai;
            for (int i = 0; i < dt.Rows.Count + (dt.Rows.Count / 2); i++)
            {
                //if (dt.Rows[nCel][0].ToString() == "" && dt.Rows[nCel + 1][0].ToString() == "")
                //{
                //    //nCel++;
                //}
                if ((i - 1) % 3 == 0)
                {
                    PdfPCell ccell = new PdfPCell(new Phrase(":", timesNormal));
                    ccell.Padding = cellPadding;
                    if (
                        dt.Rows[nCel - 1][0].ToString() == "Ketua Peneliti" || // utk menghilangkan : 
                        (dt.Rows[nCel - 1][0].ToString() == "Biaya Penelitian") ||
                        dt.Rows[nCel - 1][0].ToString() == "2. Tim Peneliti" ||
                        dt.Rows[nCel - 1][0].ToString() == "2. Tim Pelaksana" ||
                        dt.Rows[nCel - 1][0].ToString() == "2. Perguruan Tinggi Pengusul" ||
                         dt.Rows[nCel - 1][0].ToString().Contains("Anggota") ||
                         dt.Rows[nCel - 1][0].ToString().Trim()=="Pelaksana" ||
                         dt.Rows[nCel - 1][0].ToString().Trim() == "Institusi Mitra"
                        ) //
                    {
                        string ss = dt.Rows[nCel - 1][0].ToString().Trim();
                        ccell = new PdfPCell(new Phrase("", timesNormal));
                        //ccell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                        //table.AddCell(ccell);
                    }

                    //if (dt.Rows[nCel - 1][0].ToString() == "" && dt.Rows[nCel][0].ToString().Trim() == "Institusi Mitra")
                    //{
                    //    ccell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                    //    table.AddCell(ccell);
                    //}
                    //else if (dt.Rows[nCel-1][0].ToString() != "")
                    //{
                    ccell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                    if ((dt.Rows[nCel - 1][0].ToString() == "" && dt.Rows[nCel][0].ToString() == "") ||
                       dt.Rows[nCel - 1][0].ToString() == "pause" || dt.Rows[nCel][0].ToString() == "pause")
                    {

                    }
                    else
                    {
                        table.AddCell(ccell);
                    }
                    //}
                }
                else
                {
                    if (nCel < cell.Length)
                    {
                        string str = dt.Rows[nCel][0].ToString();

                        if(str.ToLower().Contains("anggota") || str.ToLower().Trim() == "pelaksana")
                        cell[nCel] = new PdfPCell(new Phrase(dt.Rows[nCel][0].ToString().Replace("<br />", "\n"), timesTebal));
                        else
                        cell[nCel] = new PdfPCell(new Phrase(dt.Rows[nCel][0].ToString().Replace("<br />", "\n"), timesNormal));
                        cell[nCel].Padding = cellPadding;
                        cell[nCel].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cell[nCel]);
                        nCel++;
                    }
                }
                if (dt.Rows[nCel - 1][0].ToString() == "pause" || dt.Rows[nCel - 1][0].ToString() == "pause2")
                {
                    kolPause = nCel;
                    return table;
                }
            }
            return table;
        }

        // 3 kolom dengan : 
        public PdfPTable createTable3KolomNamaMitraIbm(DataTable dt)
        {
            PdfPTable table = new PdfPTable(3);
            float[] widths = new float[] { 15f, 1f, 34f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);

            PdfPCell ccell = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ccell = new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), timesNormal));
                    ccell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                    table.AddCell(ccell);
                }
            }
            return table;
        }

        // 2 kolom untuk uraian umum 
        public PdfPTable createTableUraianUmum(DataTable dt)
        {
            PdfPTable table = new PdfPTable(2);
            float[] widths = new float[] { 1f, 30f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);
            PdfPCell[] cell = new PdfPCell[dt.Rows.Count];
            string s = string.Empty;
            manipulasiData mnp = new manipulasiData();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s = mnp.removeUnicode(dt.Rows[i][0].ToString()).Trim().Replace("<br />", "\n").Replace("#", " ");
                cell[i] = new PdfPCell(new Paragraph(new Chunk(s, timesNormal)));
                cell[i].BorderColor = iTextSharp.text.BaseColor.WHITE;
                table.AddCell(cell[i]);
            }
            return table;
        }

        // dinamik row dengan : 
        public PdfPTable createTableLokasiMitraIbm(DataTable dt)
        {
            PdfPTable table = new PdfPTable(3);
            float[] widths = new float[] { 15f, 1f, 34f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);
            int jmlBaris = 5 * dt.Rows.Count;
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("judul_kolom1", typeof(string));
            dt2.Columns.Add("ttk_dua", typeof(string));
            dt2.Columns.Add("isian", typeof(string));

            object[] objNull = new object[3];

            int barisSbr = 0;
            for (int i = 0; i < jmlBaris; i++)
            {
                dt2.Rows.Add(objNull);
                for (int j = 0; j < 3; j++) // 3 = jml kolom
                {

                    if (j == 0)
                    {
                        if (i % 5 == 0 && j == 0)
                            dt2.Rows[i][j] = dt.Rows[barisSbr][j];
                        else if (i % 5 == 1)
                            dt2.Rows[i][j] = "    a. Wilayah Mitra \n        (Desa/Kecamatan) ";
                        else if (i % 5 == 2)
                            dt2.Rows[i][j] = "    b. Kabupaten/Kota";
                        else if (i % 5 == 3)
                            dt2.Rows[i][j] = "    c. Propinsi ";
                        else if (i % 5 == 4)
                            dt2.Rows[i][j] = "    d. Jarak PT ke lokasi mitra\n        (Km) ";
                    }

                    else if (j == 1 && i % 5 != 0) dt2.Rows[i][j] = ":";
                    else if (j == 2 && i % 5 != 0)
                    {
                        dt2.Rows[i][j] = dt.Rows[barisSbr][i % 5];
                    }
                }
                if ((i + 1) % 5 == 0)
                {
                    barisSbr++;
                }
            }

            PdfPCell ccell = null;
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Columns.Count; j++)
                {
                    ccell = new PdfPCell(new Phrase(dt2.Rows[i][j].ToString(), timesNormal));
                    ccell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                    table.AddCell(ccell);
                }
            }
            return table;
        }

        // dinamik kolom dengan : 
        public PdfPTable createTableMitraUkm(DataTable dt)
        {
            int jmlKolomIsian = dt.Rows.Count;
            int jmlKolom = 2 + jmlKolomIsian;
            PdfPTable table = new PdfPTable(jmlKolom);
            float[] widths = new float[jmlKolom];// { 15f, 1f, 34f };
            int lbrKolom = 34 / (jmlKolom - 2);
            widths[0] = 15f;
            widths[1] = 1f;
            for (int i = 2; i < (jmlKolomIsian + 2); i++)
            {
                if (i == jmlKolom - 1)
                {
                    widths[i] = lbrKolom - 1 + (34 % jmlKolom);
                }
                else
                    widths[i] = lbrKolom - 1;
            }
            table.WidthPercentage = 100;
            table.SetWidths(widths);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("judul_kolom1", typeof(string));
            dt2.Columns.Add("ttk_dua", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt2.Columns.Add("ukm" + i.ToString(), typeof(string));
            }

            int kol = dt2.Columns.Count;
            object[] objNull = new object[kol];
            for (int i = 0; i < 5; i++) // 5 baris
            {
                dt2.Rows.Add(objNull);
            }
            int bar = dt2.Rows.Count;
            dt2.Rows[0][0] = "4. Usaha Kecil Menengah";
            dt2.Rows[1][0] = "    a. Nama Perusahaan";
            dt2.Rows[2][0] = "    b. Jarak PT ke Lokasi (km)";
            dt2.Rows[3][0] = "    c. Status Usaha";
            dt2.Rows[4][0] = "    d. Jenis Usaha";

            dt2.Rows[0][1] = " ";
            dt2.Rows[1][1] = ":";
            dt2.Rows[2][1] = ":";
            dt2.Rows[3][1] = ":";
            dt2.Rows[4][1] = ":";
            //for (int j = 0; j < 2; j++)
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                for (int i = 2; i < dt2.Columns.Count; i++)
                {
                    dt2.Rows[j][i] = dt.Rows[i - 2][j];
                }
            }

            PdfPCell ccell = null;
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Columns.Count; j++)
                {
                    ccell = new PdfPCell(new Phrase(dt2.Rows[i][j].ToString(), timesNormal));
                    ccell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                    table.AddCell(ccell);
                }
            }
            return table;
        }

        // 3 kolom 
        public PdfPTable createTabelAnggota(DataTable dt)
        {
            PdfPTable table = new PdfPTable(3);
            float[] widths = new float[] { 15f, 1f, 34f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);
            int jmlAnggota = 0;
            jmlAnggota = dt.Rows.Count;
            int jmlKolom = 3;
            int jmlBarisIsian = 4;
            PdfPCell[] cellsAnggota = new PdfPCell[jmlAnggota * jmlBarisIsian * jmlKolom];
                    
            int idx = 0;
            for (int i = 0; i < jmlAnggota; i++)
            {
                for (int j = 0; j < jmlBarisIsian; j++)
                {
                    if (j == 0)
                    {
                        cellsAnggota[idx] = new PdfPCell(new Phrase("Anggota Peneliti (" + (i + 1).ToString() + ")", timesNormal));
                        cellsAnggota[idx].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx]);

                        cellsAnggota[idx + 1] = new PdfPCell(new Phrase("", timesNormal));
                        cellsAnggota[idx + 1].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 1]);

                        cellsAnggota[idx + 2] = new PdfPCell(new Phrase("", timesNormal));
                        cellsAnggota[idx + 2].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 2]);
                    }
                    else if (j == 1)
                    {
                        cellsAnggota[idx + 2] = new PdfPCell(new Phrase("a. Nama Lengkap ", timesNormal));
                        cellsAnggota[idx + 2].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 2]);

                        cellsAnggota[idx + 3] = new PdfPCell(new Phrase(":", timesNormal));
                        cellsAnggota[idx + 3].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 3]);

                        string namaAnggota = dt.Rows[i]["nama_anggota"].ToString().Trim();
                        cellsAnggota[idx + 4] = new PdfPCell(new Phrase(namaAnggota, timesNormal));
                        cellsAnggota[idx + 4].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 4]);
                    }
                    else if (j == 2)
                    {
                        cellsAnggota[idx + 4] = new PdfPCell(new Phrase("b. NIDN", timesNormal));
                        cellsAnggota[idx + 4].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 4]);

                        cellsAnggota[idx + 5] = new PdfPCell(new Phrase(":", timesNormal));
                        cellsAnggota[idx + 5].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 5]);

                        cellsAnggota[idx + 6] = new PdfPCell(new Phrase(dt.Rows[i]["nidn"].ToString().Trim(), timesNormal));
                        cellsAnggota[idx + 6].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 6]);
                    }
                    else if (j == 3)
                    {
                        cellsAnggota[idx + 6] = new PdfPCell(new Phrase("c. Perguruan Tinggi", timesNormal));
                        cellsAnggota[idx + 6].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 6]);
                        cellsAnggota[idx + 7] = new PdfPCell(new Phrase(":", timesNormal));
                        cellsAnggota[idx + 7].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 7]);

                        string namaInstitusi = dt.Rows[i]["nama_institusi"].ToString().Trim();
                        cellsAnggota[idx + 8] = new PdfPCell(new Phrase(namaInstitusi, timesNormal));
                        cellsAnggota[idx + 8].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(cellsAnggota[idx + 8]);
                    }
                    idx++;
                }
                    }
            return table;
        }

        // 2 kolom 
        public PdfPTable createTableTtdYgMengetahui(DataTable dt, ref int kolomStart)
        {
            PdfPTable table = new PdfPTable(2);
            float[] widths = new float[] { 4f, 5f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);

            PdfPCell cellSpase = new PdfPCell(new Phrase("\n", timesNormal));
            cellSpase.BorderColor = iTextSharp.text.BaseColor.WHITE;
            table.AddCell(cellSpase);
            table.AddCell(cellSpase);

            PdfPCell[,] arrCell = new PdfPCell[6, 2];
            for(int i=0; i< arrCell.GetLength(0); i++)
            {
                for (int j = 0; j < arrCell.GetLength(1); j++)
                {
                    if (dt.Rows[kolomStart][0].ToString()=="ttd")
                    {
                        PdfPCell ccel = new PdfPCell(new Phrase("\n\n\n", timesNormal));
                        ccel.Padding = cellPadding;
                        ccel.BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(ccel);
                    }
                    else
                    {
                        arrCell[i, j] = new PdfPCell(new Phrase(dt.Rows[kolomStart][0].ToString(), timesNormal));
                        arrCell[i, j].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        arrCell[i,j].Padding = cellPadding;
                        table.AddCell(arrCell[i, j]);
                    }
                    kolomStart++;
                    if (
                        dt.Rows[kolomStart][0].ToString() == "pause" ||
                        dt.Rows[kolomStart][0].ToString() == "pause2" ||
                        dt.Rows[kolomStart][0].ToString() == "pause3" ||
                        dt.Rows[kolomStart][0].ToString() == "pause4"
                        )
                    {
                        kolomStart++;
                        return table;
                    }
                }
            }
            return table;
        }

        // 3 kolom 
        public PdfPTable createTableTtdYgMenyetujui3Kolom(DataTable dt, ref int kolomStart)
        {
            PdfPTable table = new PdfPTable(3);
            float[] widths = new float[] { 5f, 5f, 5f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);

            PdfPCell cellSpase = new PdfPCell(new Phrase("\n", timesNormal));
            cellSpase.BorderColor = iTextSharp.text.BaseColor.WHITE;
            table.AddCell(cellSpase);
            table.AddCell(cellSpase);

            PdfPCell[,] arrCell = new PdfPCell[8, 2];
            //int count = 0;
            for (int i = 0; i < arrCell.GetLength(0); i++)
            {
                for (int j = 0; j < arrCell.GetLength(1); j++)
                {
                    if (dt.Rows[kolomStart][0].ToString() == "ttd")
                    {
                        PdfPCell ccel = new PdfPCell(new Phrase("\n\n\n", timesNormal));
                        ccel.BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(ccel);
                    }
                    else
                    {
                        string s = dt.Rows[kolomStart][0].ToString().Trim().Replace("<br />", "\n").Replace("#", " ");
                        arrCell[i, j] = new PdfPCell(new Phrase(s, timesNormal));
                        arrCell[i, j].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(arrCell[i, j]);
                    }
                    kolomStart++;
                    //if (dt.Rows[kolomStart][0].ToString() == "pause3")
                    //{
                    //    kolomStart++;
                    //    return table;
                    //}
                }
            }
            return table;
        }
        // 1 kolom
        public PdfPTable createTableTtdYgMenyetujui(DataTable dt, int kolomStart)
        {
            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 80;

            PdfPCell cellSpase = new PdfPCell(new Phrase("\n", timesNormal));
            cellSpase.BorderColor = iTextSharp.text.BaseColor.WHITE;
            table.AddCell(cellSpase);

            PdfPCell[] arrCell = new PdfPCell[5];
            for (int i = 0; i < arrCell.GetLength(0); i++)
            {                 
                    if (dt.Rows[kolomStart][0].ToString() == "ttd")
                    {
                        PdfPCell ccel = new PdfPCell(new Phrase("\n\n\n", timesNormal));
                        ccel.BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(ccel);
                    }
                    else
                    {
                        arrCell[i] = new PdfPCell(new Phrase(dt.Rows[kolomStart][0].ToString(), timesNormal));
                        arrCell[i].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        arrCell[i].HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        arrCell[i].Padding = cellPadding;
                    table.AddCell(arrCell[i]);
                    }
                    kolomStart++;                
            }
            return table;
        }

        public PdfPTable createTable2TtdYgMenyetujui(DataTable dt, int kolomStart)
        {
            PdfPTable table = new PdfPTable(2);
            float[] widths = new float[] { 4f, 5f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);

            PdfPCell cellSpase = new PdfPCell(new Phrase("\n", timesNormal));
            cellSpase.BorderColor = iTextSharp.text.BaseColor.WHITE;
            table.AddCell(cellSpase);
            table.AddCell(cellSpase);
            
            PdfPCell[,] arrCell = new PdfPCell[6,2];
                for (int i = 0; i < arrCell.GetLength(0); i++)
                {
                for (int j = 0; j < arrCell.GetLength(1); j++)
                {
                    if (dt.Rows[kolomStart][0].ToString() == "ttd")
                    {
                        PdfPCell ccel = new PdfPCell(new Phrase("\n\n\n", timesNormal));
                        ccel.Padding = cellPadding;
                        ccel.BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(ccel);
                    }
                    else
                    {
                        arrCell[i, j] = new PdfPCell(new Phrase(dt.Rows[kolomStart][0].ToString(), timesNormal));
                        arrCell[i, j].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        arrCell[i, j].Padding = cellPadding;
                        table.AddCell(arrCell[i, j]);
                    }

                    kolomStart++;
                }
                }
            return table;
        }


        public Paragraph createTabelPersonilUraianUmum(DataTable dt)
        {
            PdfPTable table = new PdfPTable(6);
            Font tebal = new Font(bfTimes, 10, Font.BOLD);//Font(bfTimes, 12);
            float[] widths = new float[] { 1f, 5f, 4f, 5f, 5f, 3f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);
            int kol = dt.Columns.Count;
            PdfPCell[] cell = new PdfPCell[kol * (dt.Rows.Count+1)];
            string[] daftarJudul = { "No", "Nama", "Jabatan ", "Bidang \nKeahlian", "Instansi Asal", "Alokasi Waktu \n(jam/minggu)" };
            for (int i = 0; i < kol; i++)  
            {
                cell[i] = new PdfPCell(new Phrase(daftarJudul[i], timesNormal));
                cell[i].HorizontalAlignment = 1;
                cell[i].VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell[i]);
            }
            int idx = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < kol; j++)
                {
                    cell[idx] = new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), timesNormal));
                    cell[idx].HorizontalAlignment = 1;
                    cell[idx].VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(cell[idx]);
                    idx++;
                }
            }
            Paragraph p = new Paragraph();
            p.IndentationLeft = 15;
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            p.Add(table);
            return p;
        }


        public void cetakFooter(PdfWriter writer)
        {
            BaseFont footerFont = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, false);
            PdfContentByte cb2 = writer.DirectContent;
            cb2.SetFontAndSize(footerFont, 6);
            cb2.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Copyright(c): Ditlitabmas 2012, updated " + DateTime.Now.Year.ToString(), 80, 45, 0);
        }


        public PdfPTable createTableJudulHeader(DataTable dt, ref int kolomStart)
        {
            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;
            PdfPCell cell = null;
            //new PdfPCell(new Phrase(judulHalaman, new Font(Font.FontFamily.TIMES_ROMAN, ukuranFont, Font.BOLD | Font.UNDERLINE)));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
               
                    cell = new PdfPCell(new Phrase(dt.Rows[kolomStart][0].ToString().ToUpper(), new Font(Font.FontFamily.TIMES_ROMAN, ukuranFont, Font.BOLD )));

                cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);
                kolomStart++;
                if (dt.Rows[kolomStart][0].ToString() == "pause")
                {
                    kolomStart++;
                    return table;
                }
            }
            return table;
        }

        // 5 kolom dengan : 
        public PdfPTable createTable5KolomPenilaian(DataTable dt,ref int kolomStart)
        {
            PdfPTable table = new PdfPTable(5);
            float[] widths = new float[] { 1f, 15f, 3f, 2f, 2f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);

            PdfPCell ccell = null;
            int kolKriteria = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ccell = new PdfPCell(new Phrase(dt.Rows[kolomStart][j].ToString(), timesNormal));
                    ccell.BorderColor = iTextSharp.text.BaseColor.BLACK;
                    if((kolKriteria+4) % 5 != 0 ||
                        dt.Rows[kolomStart][j].ToString()== "Kriteria" ||
                        dt.Rows[kolomStart][j].ToString() == "Jumlah")
                        {
                            ccell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            ccell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        }
                    if (kolKriteria < 5)
                        ccell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(ccell);
                    kolomStart++;
                    kolKriteria++;
                    if (dt.Rows[kolomStart][0].ToString() == "pause")
                    {
                        kolomStart++;
                        return table;
                    }
                }
            }
            return table;
        }


        // 5 kolom dengan : 
        public PdfPTable createTable5KolomPenilaianNoMerge(DataTable dt)
        {
            int kolomStart = 0;
            PdfPTable table = new PdfPTable(5);
            float[] widths = new float[] { 1f, 15f, 3f, 2f, 2f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);

            PdfPCell ccell = null;

            string[] header = {"No", "Kriteria" , "Bobot(%)" , "Skor" , "Nilai" };

            for (int i = 0; i < header.Length; i++)
            {
                ccell = new PdfPCell(new Phrase(header[i], timesNormal));
                ccell.BorderColor = iTextSharp.text.BaseColor.BLACK;
                ccell.BackgroundColor = BaseColor.LIGHT_GRAY;
                ccell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(ccell);
            }
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);
            int jmlRow = dt.Rows.Count;
            dt.Rows[jmlRow-1][1] = "Jumlah";
            dt.Rows[jmlRow-1][2] = "100";
            dt.Rows[jmlRow-1][4] = dt.Rows[0][5];

            int kolKriteria = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < header.Length; j++)
                {
                    ccell = new PdfPCell(new Phrase(dt.Rows[i][j].ToString(), timesNormal));
                    ccell.BorderColor = iTextSharp.text.BaseColor.BLACK;
                    if (j != 1 || 
                        dt.Rows[i][j].ToString() == "Jumlah")
                    {
                        ccell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        ccell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    }
                    table.AddCell(ccell);
                    kolomStart++;
                    kolKriteria++;
                   
                }
            }
            return table;
        }


        public PdfPTable createTableKeteranganPenilaian(DataTable dt, ref int kolomStart)
        {
            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;
            PdfPCell cell = null;
            //new PdfPCell(new Phrase(judulHalaman, new Font(Font.FontFamily.TIMES_ROMAN, ukuranFont, Font.BOLD | Font.UNDERLINE)));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i < 3 )
                {
                    cell = new PdfPCell(new Phrase(dt.Rows[kolomStart][0].ToString().Replace("<br />","\n"), new Font(Font.FontFamily.TIMES_ROMAN, 8, Font.NORMAL)));
                    cell.PaddingTop = - 18+(i*8);
                }
                else
                    cell = new PdfPCell(new Phrase(dt.Rows[kolomStart][0].ToString(), new Font(Font.FontFamily.TIMES_ROMAN, ukuranFont, Font.NORMAL)));
                cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                table.AddCell(cell);
               
                kolomStart++;
                if (dt.Rows[kolomStart][0].ToString() == "pause")
                {
                    kolomStart++;
                    return table;
                }
            }
            return table;
        }

        // 2 kolom 
        public PdfPTable createTableTtdReviewer(DataTable dt, ref int kolomStart)
        {
            PdfPTable table = new PdfPTable(2);
            float[] widths = new float[] { 5f, 4f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);

            PdfPCell cellSpase = new PdfPCell(new Phrase("\n", timesNormal));
            cellSpase.BorderColor = iTextSharp.text.BaseColor.WHITE;
            table.AddCell(cellSpase);
            table.AddCell(cellSpase);

            PdfPCell[,] arrCell = new PdfPCell[5, 2];
            //int count = 0;
            for (int i = 0; i < arrCell.GetLength(0); i++)
            {
                for (int j = 0; j < arrCell.GetLength(1); j++)
                {
                    if (dt.Rows[kolomStart][0].ToString() == "ttd")
                    {
                        PdfPCell ccel = new PdfPCell(new Phrase("\n\n\n", timesNormal));
                        ccel.BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(ccel);
                    }
                    else
                    {
                        arrCell[i, j] = new PdfPCell(new Phrase(dt.Rows[kolomStart][0].ToString(), timesNormal));
                        arrCell[i, j].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(arrCell[i, j]);
                    }
                    kolomStart++;
                    if (dt.Rows[kolomStart][0].ToString() == "pause"
                        )
                    {
                        kolomStart++;
                        return table;
                    }
                }
            }
            return table;
        }

        public PdfPTable createTable5KolomPenilaian(DataTable dtInput)
        {
            DataRow row = dtInput.NewRow();
            dtInput.Rows.Add(row);
            int jmlRow = dtInput.Rows.Count;
            dtInput.Rows[jmlRow - 1][1] = "Jumlah";
            dtInput.Rows[jmlRow - 1][2] = "100";
            dtInput.Rows[jmlRow - 1][4] = dtInput.Rows[0][5];

            float[] dtWidth = { 1f, 15f, 3f, 2f, 2f }; //  { 30, 360, 60, 60, 60 };
            string[] Header = { "No", "Kriteria Penilaian", "Bobot(%)", "Skor", "Nilai" };

            PdfPTable table = new PdfPTable(5); //dtInput.Columns.Count
            table.WidthPercentage = 100;
            float[] widths = new float[5]; // 5 dtInput.Columns.Count
            // Setting Header 
            PdfPCell cell = null;
            for (int x = 0; x < 5; x++)
            {
                widths[x] = dtWidth[x];
                string cellText = Header[x];
                cell = new PdfPCell(new Phrase(cellText, timesNormal));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#dddddd"));// (System.Drawing.ColorTranslator.FromHtml("#C2D69B"));// new Color(gvInput.RowStyle.BackColor);// new iTextSharp.text.Color(204, 204, 204); // iTextSharp.text.pdf.CMYKColor.LIGHT_GRAY;// .BaseColor.LIGHT_GRAY;  //new Color(System.Drawing.ColorTranslator.FromHtml("#008000"));
                cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                cell.BorderColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Black);//System.Drawing.ColorTranslator.FromHtml("#aaaaaa"));
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                cell.Padding = 3;
                table.AddCell(cell);
            }
            table.SetWidths(widths);
            //==================================
            ArrayList al1 = new ArrayList();
            // hitung jml Rospan
            int jmlSama = 1;
            int jmlBaris = dtInput.Rows.Count;
            int jmlKolom = dtInput.Rows.Count;
            for (int i = 0; i < jmlBaris; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (i > 0)
                    {
                        if (dtInput.Rows[i - 1][j].ToString() == dtInput.Rows[i][j].ToString())
                        {
                            jmlSama++;
                        }
                        else
                        {
                            al1.Add(jmlSama);
                            jmlSama = 1;
                        }
                    }
                    if (i == jmlBaris - 1)
                    {
                        if (dtInput.Rows[i - 1][j].ToString() == dtInput.Rows[i][j].ToString())
                        {
                            al1.Add(jmlSama);
                        }
                        else
                        {
                            al1.Add(1);
                        }
                    }
                }
            }
            //==================== end hitung jml rowspan
            string a = al1[0].ToString();
            string b = al1[1].ToString();
            //==============================================================
            for (int k = 0; k < al1.Count; k++)
            {
                int awalLoop = 0;
                int jmlLoop = 0;
                if (k == 0)
                {
                    awalLoop = 0;
                    jmlLoop = awalLoop + int.Parse(al1[k].ToString());
                }
                else
                {
                    for (int i = 0; i < k; i++)
                    {
                        awalLoop += int.Parse(al1[i].ToString());
                        if (k == i + 1)
                        {
                            jmlLoop = awalLoop + int.Parse(al1[k].ToString());
                        }
                    }
                }
                string cellText1 = "";
                cellText1 = dtInput.Rows[awalLoop][0].ToString();
                iTextSharp.text.pdf.PdfPCell cell1 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText1, timesNormal));
                cell1 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText1, timesNormal));
                cell1.Rowspan = int.Parse(al1[k].ToString());
                cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                cell1.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.White);
                cell1.BorderColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Black);//System.Drawing.ColorTranslator.FromHtml("#aaaaaa"));
                //cell1.Padding = 5;
                table.AddCell(cell1);

                for (int i = awalLoop; i < jmlLoop; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        cellText1 = dtInput.Rows[i][j].ToString();
                        if (j == 1)
                        {
                            cellText1 = cellText1.Replace("<br/>", "\n");
                            cellText1 = cellText1.Replace("<br />", "\n");
                            cellText1 = cellText1.Replace("<b>", "");
                            cellText1 = cellText1.Replace("</b>", "");     
                        }
                        iTextSharp.text.pdf.PdfPCell cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText1, timesNormal));
                        if (i % 2 == 0)
                        {
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.White);
                        }
                        cell2.BorderColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Black);//System.Drawing.ColorTranslator.FromHtml("#aaaaaa"));
                        if (j != 1)
                        {
                            cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                            if (i % 2 == 0)
                            {
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.White);
                            }
                            cell2.BorderColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Black);//System.Drawing.ColorTranslator.FromHtml("#aaaaaa"));
                        }
                        else if (j == 1 && dtInput.Rows[i][j].ToString() == "Jumlah")
                            cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        cell2.Padding = 3;
                        table.AddCell(cell2);
                    }
                }

            }
            return table;
        }

        public PdfPTable createTable6KolomPenilaian(DataTable dtInput)
        {
            DataRow row = dtInput.NewRow();
            dtInput.Rows.Add(row);
            int jmlRow = dtInput.Rows.Count;
            dtInput.Rows[jmlRow - 1][1] = "Jumlah";
            dtInput.Rows[jmlRow - 1][2] = "100";
            dtInput.Rows[jmlRow - 1][4] = dtInput.Rows[0][6];

            float[] dtWidth = { 2f, 15f, 3f, 2f, 2f, 4f }; //  { 30, 360, 60, 60, 60 };
            string[] Header = { "No", "Kriteria Penilaian", "Bobot(%)", "Skor", "Nilai", "Catatan" };

            PdfPTable table = new PdfPTable(6); //dtInput.Columns.Count
            table.WidthPercentage = 100;
            float[] widths = new float[6]; // 5 dtInput.Columns.Count
            // Setting Header 
            PdfPCell cell = null;
            for (int x = 0; x < 6; x++)
            {
                widths[x] = dtWidth[x];
                string cellText = Header[x];
                cell = new PdfPCell(new Phrase(cellText, timesNormal));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.ColorTranslator.FromHtml("#dddddd"));// (System.Drawing.ColorTranslator.FromHtml("#C2D69B"));// new Color(gvInput.RowStyle.BackColor);// new iTextSharp.text.Color(204, 204, 204); // iTextSharp.text.pdf.CMYKColor.LIGHT_GRAY;// .BaseColor.LIGHT_GRAY;  //new Color(System.Drawing.ColorTranslator.FromHtml("#008000"));
                cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                cell.BorderColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Black);//System.Drawing.ColorTranslator.FromHtml("#aaaaaa"));
                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                cell.Padding = 3;
                table.AddCell(cell);
            }
            table.SetWidths(widths);
            //==================================
            ArrayList al1 = new ArrayList();
            // hitung jml Rospan
            int jmlSama = 1;
            int jmlBaris = dtInput.Rows.Count;
            int jmlKolom = dtInput.Rows.Count;
            for (int i = 0; i < jmlBaris; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (i > 0)
                    {
                        if (dtInput.Rows[i - 1][j].ToString() == dtInput.Rows[i][j].ToString())
                        {
                            jmlSama++;
                        }
                        else
                        {
                            al1.Add(jmlSama);
                            jmlSama = 1;
                        }
                    }
                    if (i == jmlBaris - 1)
                    {
                        if (dtInput.Rows[i - 1][j].ToString() == dtInput.Rows[i][j].ToString())
                        {
                            al1.Add(jmlSama);
                        }
                        else
                        {
                            al1.Add(1);
                        }
                    }
                }
            }
            //==================== end hitung jml rowspan
            string a = al1[0].ToString();
            string b = al1[1].ToString();
            //==============================================================
            for (int k = 0; k < al1.Count; k++)
            {
                int awalLoop = 0;
                int jmlLoop = 0;
                if (k == 0)
                {
                    awalLoop = 0;
                    jmlLoop = awalLoop + int.Parse(al1[k].ToString());
                }
                else
                {
                    for (int i = 0; i < k; i++)
                    {
                        awalLoop += int.Parse(al1[i].ToString());
                        if (k == i + 1)
                        {
                            jmlLoop = awalLoop + int.Parse(al1[k].ToString());
                        }
                    }
                }
                string cellText1 = "";
                cellText1 = dtInput.Rows[awalLoop][0].ToString();
                iTextSharp.text.pdf.PdfPCell cell1 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText1, timesNormal));
                cell1 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText1, timesNormal));
                cell1.Rowspan = int.Parse(al1[k].ToString());
                cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                cell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                cell1.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.White);
                cell1.BorderColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Black);//System.Drawing.ColorTranslator.FromHtml("#aaaaaa"));
                //cell1.Padding = 5;
                table.AddCell(cell1);

                for (int i = awalLoop; i < jmlLoop; i++)
                {
                    for (int j = 1; j < 6; j++)
                    {
                        cellText1 = dtInput.Rows[i][j].ToString();
                        if (j == 1)
                        {
                            cellText1 = cellText1.Replace("<br/>", "\n");
                            cellText1 = cellText1.Replace("<br />", "\n");
                            cellText1 = cellText1.Replace("<b>", "");
                            cellText1 = cellText1.Replace("</b>", "");
                        }
                        iTextSharp.text.pdf.PdfPCell cell2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText1, timesNormal));
                        if (i % 2 == 0)
                        {
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.White);
                        }
                        cell2.BorderColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Black);//System.Drawing.ColorTranslator.FromHtml("#aaaaaa"));
                        if (j != 1)
                        {
                            cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                            if (i % 2 == 0)
                            {
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(System.Drawing.Color.White);
                            }
                            cell2.BorderColor = new iTextSharp.text.BaseColor(System.Drawing.Color.Black);//System.Drawing.ColorTranslator.FromHtml("#aaaaaa"));
                        }
                        else if (j == 1 && dtInput.Rows[i][j].ToString() == "Jumlah")
                            cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        cell2.Padding = 3;
                        table.AddCell(cell2);
                    }
                }

            }
            return table;
        }

        // 2 kolom 
        public PdfPTable createTableTtdMitraKonsorsiumInsinas(DataTable dt, ref int kolomStart)
        {
            PdfPTable table = new PdfPTable(2);
            float[] widths = new float[] { 5f, 4f };
            table.WidthPercentage = 100;
            table.SetWidths(widths);

            PdfPCell cellSpase = new PdfPCell(new Phrase("\n", timesNormal));
            cellSpase.BorderColor = iTextSharp.text.BaseColor.WHITE;
            table.AddCell(cellSpase);
            table.AddCell(cellSpase);

            PdfPCell[,] arrCell = new PdfPCell[20, 2];
            for (int i = 0; i < arrCell.GetLength(0); i++)
            {
                for (int j = 0; j < arrCell.GetLength(1); j++)
                {
                    if (dt.Rows[kolomStart][0].ToString() == "ttd")
                    {
                        PdfPCell ccel = new PdfPCell(new Phrase("\n\n\n", timesNormal));
                        ccel.BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(ccel);
                    }
                    else
                    {
                        string str = dt.Rows[kolomStart][0].ToString();
                        arrCell[i, j] = new PdfPCell(new Phrase(str, timesNormal));
                        arrCell[i, j].BorderColor = iTextSharp.text.BaseColor.WHITE;
                        table.AddCell(arrCell[i, j]);
                    }
                    kolomStart++;
                    if (dt.Rows[kolomStart][0].ToString() == "pause")
                    {
                        kolomStart++;
                        return table;
                    }
                }
            }
            return table;
        }

        #endregion

    }
}