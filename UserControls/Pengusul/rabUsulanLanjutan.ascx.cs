using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.Models.Pengusul;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class rabUsulanLanjutan : System.Web.UI.UserControl
    {
        UIControllers.uiNotify noty = new UIControllers.uiNotify();
        RAB objModel = new Models.Pengusul.RAB();

        public string KodeJenisKegiatan
        {
            get
            {
                if (ViewState["KodeJenisKegiatan"] == null) ViewState["KodeJenisKegiatan"] = "1"; //default penelitian
                return ViewState["KodeJenisKegiatan"].ToString();
            }
            set { ViewState["KodeJenisKegiatan"] = value; }
        }

        private bool isUpdated
        {
            get
            {
                if (ViewState["IsUpdated"] == null) ViewState["IsUpdated"] = false;
                return bool.Parse(ViewState["IsUpdated"].ToString());
            }
            set { ViewState["IsUpdated"] = value; }
        }

        private List<string> listTahunInvalid
        {
            get
            {
                if (ViewState["ListTahunInvalid"] == null) ViewState["ListTahunInvalid"] = new List<string>();
                return ViewState["ListTahunInvalid"] as List<string>;
            }
            set { ViewState["ListTahunInvalid"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.btnUnggah);
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }

        public void InitRAB(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
            ViewState["IdUsulan"] = usulanKegiatan.idUsulan;
            ViewState["LamaKegiatan"] = usulanKegiatan.lamaKegiatan;
            ViewState["UrutanTahunUsulanKegiatan"] = usulanKegiatan.urutanTahunUsulanKegiatan;

            DataTable dt = new DataTable();
            if (objModel.GetRABUsulan(ref dt, Guid.Parse(usulanKegiatan.idUsulanKegiatan)))
            {
                var listAnggaran = new List<ItemRencanaAnggaran>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var itemAnggaran = new ItemRencanaAnggaran();
                    itemAnggaran.IdRABUsulan = Guid.Parse(dt.Rows[i]["id_rab_usulan"].ToString());
                    itemAnggaran.KodeJenisPembelanjaan = dt.Rows[i]["kd_jenis_pembelanjaan"].ToString();
                    itemAnggaran.JenisPembelanjaan = dt.Rows[i]["jenis_pembelanjaan"].ToString();
                    itemAnggaran.Item = dt.Rows[i]["nama_item"].ToString();
                    itemAnggaran.Satuan = dt.Rows[i]["xsatuan"].ToString();
                    itemAnggaran.Volume = decimal.Parse(dt.Rows[i]["xvolume"].ToString());
                    itemAnggaran.Honor = decimal.Parse(dt.Rows[i]["harga_satuan"].ToString());
                    itemAnggaran.Total = decimal.Parse(dt.Rows[i]["xvolume"].ToString()) *
                                         decimal.Parse(dt.Rows[i]["harga_satuan"].ToString());
                    itemAnggaran.TahunKegiatan = int.Parse(dt.Rows[i]["urutan_thn_usulan_kegiatan"].ToString());

                    listAnggaran.Add(itemAnggaran);
                }

                dt.Clear();
                objModel.GetBidangFokus(ref dt, Guid.Parse(usulanKegiatan.idUsulan));
                if (dt.Rows.Count > 0) lblBidangFokus.Text = dt.Rows[0]["bidang_fokus"].ToString();

                dt.Clear();

                if (KodeJenisKegiatan == "1")
                {
                    var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
                    objIdentitasUsulan.getPlafonDanaSbkThnImplementasi2018(ref dt,
                        usulanKegiatan.idKategoriSBK, usulanKegiatan.idSkema, usulanKegiatan.idBidangFokus);
                    if (dt.Rows.Count > 0)
                    {
                        lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString()).ToString("N0");
                        lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString()).ToString("N0");
                    }
                    lblBidangFokus.Visible = true;
                    lblBidangFokusJudul.Visible = true;
                }
                else if (KodeJenisKegiatan == "2")
                {
                    objModel.GetRentangDanaSkemaKegiatan(ref dt, usulanKegiatan.idSkema);
                    if (dt.Rows.Count > 0)
                    {
                        lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal_thn_berjalan"].ToString()).ToString("N0");
                        lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal_thn_berjalan"].ToString()).ToString("N0");
                    }
                    lblBidangFokus.Visible = false;
                    lblBidangFokusJudul.Visible = false;
                }

                isiRAB(listAnggaran);

            }
            else
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
                            objModel.errorMessage);
            }
        }

        private List<ItemRencanaAnggaran> importExcel(ExcelWorksheet workSheet)
        {
            const int KOLOM_AWAL = 2, KOLOM_AWAL_TAHUN_1 = 4, KOLOM_AWAL_TAHUN_2 = 8, KOLOM_AWAL_TAHUN_3 = 12;
            const int BARIS_AWAL = 9, JUMLAH_BARIS = 200;

            var listAnggaran = new List<ItemRencanaAnggaran>();

            for (var rowNumber = BARIS_AWAL; rowNumber < (BARIS_AWAL + JUMLAH_BARIS); rowNumber++)
            {
                //Cek apakah sudah mencapai baris Sub Total 
                if (workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_1 + 2].Text.ToLower() == "sub total")
                {
                    break;
                }

                int nomor;
                string item, satuan;
                decimal volume, honor, total;

                int.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL].Text, out nomor);
                item = workSheet.Cells[rowNumber, KOLOM_AWAL + 1].Text;

                //Ambil Data Tahun 1
                //decimal.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_1].Text, out volume);
                //satuan = workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_1 + 1].Text;
                //decimal.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_1 + 2].Text, out honor);
                //decimal.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_1 + 3].Text, out total);

                //var anggaranTahun1 = new ItemRencanaAnggaran()
                //{
                //    Nomor = nomor,
                //    Item = item,
                //    Volume = volume,
                //    Satuan = satuan,
                //    Honor = honor,
                //    Total = total,
                //    TahunKegiatan = 1
                //};

                //listAnggaran.Add(anggaranTahun1);

                //Ambil Data Tahun 2
                decimal.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_2].Text, out volume);
                satuan = workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_2 + 1].Text;
                decimal.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_2 + 2].Text, out honor);
                decimal.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_2 + 3].Text, out total);

                var anggaranTahun2 = new ItemRencanaAnggaran()
                {
                    Nomor = nomor,
                    Item = item,
                    Volume = volume,
                    Satuan = satuan,
                    Honor = honor,
                    Total = total,
                    TahunKegiatan = 2
                };

                listAnggaran.Add(anggaranTahun2);

                //Ambil Data Tahun 3
                decimal.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_3].Text, out volume);
                satuan = workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_3 + 1].Text;
                decimal.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_3 + 2].Text, out honor);
                decimal.TryParse(workSheet.Cells[rowNumber, KOLOM_AWAL_TAHUN_3 + 3].Text, out total);

                var anggaranTahun3 = new ItemRencanaAnggaran()
                {
                    Nomor = nomor,
                    Item = item,
                    Volume = volume,
                    Satuan = satuan,
                    Honor = honor,
                    Total = total,
                    TahunKegiatan = 3
                };

                listAnggaran.Add(anggaranTahun3);
            }

            return listAnggaran.Where(x => x.Item != string.Empty && x.Volume > 0).ToList();
        }

        private void isiRAB(List<ItemRencanaAnggaran> listAnggaran)
        {
            var lamaKegiatan = int.Parse(ViewState["LamaKegiatan"].ToString());
            var urutanTahunUsulanKegiatan = int.Parse(ViewState["UrutanTahunUsulanKegiatan"].ToString());
            listTahunInvalid.Clear();

            //lvRABTahun1.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 1).ToList();

            //lvRABTahun1.DataBind();
            //var totalTahun1 = listAnggaran.Where(i => i.TahunKegiatan == 1)
            //                              .Sum(i => i.Total);
            //lblTahun1.Text = totalTahun1.ToString("N0");
            //cekDanaPertahun(totalTahun1, 1);

            for (int x = urutanTahunUsulanKegiatan; x <= lamaKegiatan; x++)
            {
                switch (x)
                {
                    case 2:
                        lvRABTahun2.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 2).ToList();
                        lvRABTahun2.DataBind();
                        var totalTahun2 = listAnggaran.Where(i => i.TahunKegiatan == 2)
                                                      .Sum(i => i.Total);
                        lblTahun2.Text = totalTahun2.ToString("N0");
                        cekDanaPertahun(totalTahun2, 2);
                        break;

                    case 3:
                        lvRABTahun3.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 3).ToList();
                        lvRABTahun3.DataBind();
                        var totalTahun3 = listAnggaran.Where(i => i.TahunKegiatan == 3)
                                                      .Sum(i => i.Total);
                        lblTahun3.Text = totalTahun3.ToString("N0");
                        cekDanaPertahun(totalTahun3, 3);
                        break;

                }
            }

            lblTotalDana.Text = listAnggaran.Where(i => i.TahunKegiatan <= lamaKegiatan)
                                            .Sum(i => i.Total)
                                            .ToString("N0");
        }

        protected void cekDanaPertahun(decimal dana, int tahun)
        {
            var danaMinimal = decimal.Parse(lblDanaMinimal.Text);
            var danaMaksimal = decimal.Parse(lblDanaMaksimal.Text);

            if (dana < danaMinimal || dana > danaMaksimal)
            {
                listTahunInvalid.Add(tahun.ToString());
            }
        }

        protected void btnUnggah_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName).ToUpper() == ".XLSX")
                {
                    List<ItemRencanaAnggaran> listAnggaran;

                    try
                    {
                        ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);

                        var list1 = importExcel(package.Workbook.Worksheets[1]);
                        list1.ForEach(i =>
                        {
                            i.KodeJenisPembelanjaan = "521213";
                            i.JenisPembelanjaan = "Honor Output Kegiatan";
                        }); //Honor Output Kegiatan

                        var list2 = importExcel(package.Workbook.Worksheets[2]);
                        list2.ForEach(i =>
                        {
                            i.KodeJenisPembelanjaan = "522151";
                            i.JenisPembelanjaan = "Belanja Bahan";
                        }); //Belanja Bahan

                        var list3 = importExcel(package.Workbook.Worksheets[3]);
                        list3.ForEach(i =>
                        {
                            i.KodeJenisPembelanjaan = "521219";
                            i.JenisPembelanjaan = "Belanja Barang Non Operasional Lainnya";
                        }); //Belanja Barang Non Operasional Lainnya

                        var list4 = importExcel(package.Workbook.Worksheets[4]);
                        list4.ForEach(i =>
                        {
                            i.KodeJenisPembelanjaan = "524119";
                            i.JenisPembelanjaan = "Belanja Perjalanan Lainnya";
                        }); //Belanja Perjalanan Lainnya

                        listAnggaran = list1.Concat(list2)
                                            .Concat(list3)
                                            .Concat(list4)
                                            .ToList();
                    }
                    catch (Exception ex)
                    {
                        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                        return;
                    }

                    isiRAB(listAnggaran);

                    ViewState["ListKegiatan"] = listAnggaran;
                    isUpdated = true;
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Berkas yang diunggah harus berjenis Excel sesuai Template yang ditentukan !");
                }
            }
            else
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Berkas yang diunggah tidak dapat ditemukan !");
            }
        }

        public bool Simpan()
        {
            if (isUpdated)
            {
                if (listTahunInvalid.Count > 0)
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
                                $"Dana tahun ke {string.Join(",", listTahunInvalid.ToArray())} tidak dalam rentang dana yang ditentukan !");
                    return false;
                }

                try
                {
                    var idUsulan = Guid.Parse(ViewState["IdUsulan"].ToString());
                    var lamaKegiatan = int.Parse(ViewState["LamaKegiatan"].ToString());
                    var urutanTahunUsulanKegiatan = int.Parse(ViewState["UrutanTahunUsulanKegiatan"].ToString());
                    var listAnggaran = ViewState["ListKegiatan"] as List<ItemRencanaAnggaran>;

                    if (!objModel.ImportToDatabase(idUsulan, listAnggaran.Where(i => i.TahunKegiatan >= urutanTahunUsulanKegiatan && 
                                                                                     i.TahunKegiatan <= lamaKegiatan ).ToList()))
                    {
                        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                    return false;
                }

                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi",
                            "Data RAB telah berhasil disimpan...");
            }

            return true;
        }
    }
}