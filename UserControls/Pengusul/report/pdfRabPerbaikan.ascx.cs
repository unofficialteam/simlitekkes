using simlitekkes.Models.Pengusul;
using simlitekkes.Models.Sistem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.report
{
    public partial class pdfRabPerbaikan : System.Web.UI.UserControl
    {

        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        RAB objModelRab = new Models.Pengusul.RAB();
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitRab(string idUsulanKegiatan)
        { //(usulanKegiatan objUsulanKegiatan) { 
          //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
          //string idUsulanKegiatan = "7414f38d-bc90-4117-b996-4ea131c299c0";

            DataTable dt = new DataTable();
            objModelIdentitasUsulan.getDataDataUsulanKegiatan(ref dt, idUsulanKegiatan);

            objUsulanKegiatan = new usulanKegiatan()
            {
                idUsulanKegiatan = idUsulanKegiatan,
                idUsulan = dt.Rows[0]["id_usulan"].ToString(),
                judul = dt.Rows[0]["judul"].ToString(),
                idSkema = int.Parse(dt.Rows[0]["id_skema"].ToString()),
                namaSkema = dt.Rows[0]["nama_skema"].ToString(),
                thnUsulan = dt.Rows[0]["thn_usulan_kegiatan"].ToString(),
                thnPelaksanaan = dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString(),
                lamaKegiatan = int.Parse(dt.Rows[0]["lama_kegiatan"].ToString()),
                urutanTahunUsulanKegiatan = int.Parse(dt.Rows[0]["urutan_thn_usulan_kegiatan"].ToString()),
                //tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString()), // test
                //idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString()),
                idBidangFokus = int.Parse(dt.Rows[0]["id_bidang_fokus"].ToString())
            };

            if (dt.Rows[0]["level_tkt_target"].ToString() != "")
            {
                objUsulanKegiatan.tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString());
            }
            if (dt.Rows[0]["id_kategori_sbk"].ToString() != "")
            {
                objUsulanKegiatan.idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString());
            }

            panelThn2.Visible = true;
            panelThn3.Visible = true;
            if (objUsulanKegiatan.lamaKegiatan == 1)
            {
                panelThn2.Visible = false;
                panelThn3.Visible = false;
            }
            else if (objUsulanKegiatan.lamaKegiatan == 2)
            {
                panelThn3.Visible = false;
            }

            //ViewState["usulan_kegiatan"] = objUsulanKegiatan;

            //InitRAB(objUsulanKegiatan);
            //List<ItemRencanaAnggaran> listAnggaran;
            isiRabPerbaikan(objUsulanKegiatan);
        }

        private void isiRabPerbaikan(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
            var dt = new DataTable();

            if (objModelRab.GetKelompokBiaya(ref dt, "1"))
            {

            }
            //dtAll.Merge(dtTwo);
            DataTable[] arrDt = new DataTable[usulanKegiatan.lamaKegiatan];
            DataTable dtw = new DataTable();
            DataTable dtt = new DataTable();
            decimal totalRab = 0;
            for (int a=usulanKegiatan.urutanTahunUsulanKegiatan; a<=usulanKegiatan.lamaKegiatan; a++) // urutan tahun
            {
                DataTable dtPertHn = new DataTable();
                
                for (int b=0; b<dt.Rows.Count; b++) // kelompok biaya rab dt.Rows.Count
                {
                    int id_rab_kelompok_biaya = int.Parse(dt.Rows[b]["id_rab_kelompok_biaya"].ToString()); // id_rab_kelompok_biaya
                    if (objModelRab.GetKomponenBelanjaRevisi(ref dtw, Guid.Parse(usulanKegiatan.idUsulanKegiatan), a, id_rab_kelompok_biaya))
                    {
                        dtPertHn.Merge(dtw);
                    }
                }
                if (a == 1)
                {
                    if (dtPertHn.Rows.Count > 0)
                    {
                        var dr1 = dtPertHn.Select("volume > 0");
                        if (dr1.Length > 0)
                        {
                            DataTable dtt1 = dr1.CopyToDataTable();
                            dtt1.DefaultView.Sort = "kelompok_biaya ASC";
                            dtt1 = dtt1.DefaultView.ToTable();
                            lvRABTahun1.DataSource = dtt1;
                            lvRABTahun1.DataBind();
                            var totalThn1 = dtPertHn.Compute("SUM(total)", string.Empty);
                            totalRab += decimal.Parse(totalThn1.ToString());
                            lblTahun1.Text = decimal.Parse(totalThn1.ToString()).ToString("N0");
                        }
                    }
                }
                else if (a == 2)
                {
                    if (dtPertHn.Rows.Count > 0)
                    {
                        var dr2 = dtPertHn.Select("volume > 0");
                        if (dr2.Length > 0)
                        {
                            DataTable dtt2 = dr2.CopyToDataTable();

                            dtt2.DefaultView.Sort = "kelompok_biaya ASC";
                            dtt2 = dtt2.DefaultView.ToTable();

                            lvRABTahun2.DataSource = dtt2;
                            lvRABTahun2.DataBind();
                            var totalThn2 = dtPertHn.Compute("SUM(total)", string.Empty);
                            totalRab += decimal.Parse(totalThn2.ToString());
                            lblTahun2.Text = decimal.Parse(totalThn2.ToString()).ToString("N0");
                        }
                    }
                }
                else if (a == 3)
                {
                    if (dtPertHn.Rows.Count > 0)
                    {
                        var dr3 = dtPertHn.Select("volume > 0");
                        if (dr3.Length > 0)
                        {
                            DataTable dtt3 = dr3.CopyToDataTable();
                            dtt3.DefaultView.Sort = "kelompok_biaya ASC";
                            dtt3 = dtt3.DefaultView.ToTable();
                            lvRABTahun3.DataSource = dtt3;
                            lvRABTahun3.DataBind();
                            var totalThn3 = dtPertHn.Compute("SUM(total)", string.Empty);
                            totalRab += decimal.Parse(totalThn3.ToString());
                            lblTahun3.Text = decimal.Parse(totalThn3.ToString()).ToString("N0");
                        }
                    }
                }
                dtPertHn.Clear();
            }
            lblTotalDana.Text = "Total RAB " + usulanKegiatan.lamaKegiatan.ToString() + " Tahun Rp. " + totalRab.ToString("N0");
        }


        private void InitRAB(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
            DataTable dt = new DataTable();
            if (objModelRab.GetRABUsulan(ref dt, Guid.Parse(usulanKegiatan.idUsulanKegiatan)))
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

                isiRAB(listAnggaran, usulanKegiatan.lamaKegiatan);

                dt.Clear();
                objModelRab.GetBidangFokus(ref dt, Guid.Parse(usulanKegiatan.idUsulan));
                //if (dt.Rows.Count > 0) lblBidangFokus.Text = dt.Rows[0]["bidang_fokus"].ToString();

                dt.Clear();
                var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
                objIdentitasUsulan.getPlafonDanaSbkThnImplementasi2018(ref dt,
                    usulanKegiatan.idKategoriSBK, usulanKegiatan.idSkema, usulanKegiatan.idBidangFokus);
                //if (dt.Rows.Count > 0)
                //{
                //    lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString()).ToString("N0");
                //    lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString()).ToString("N0");
                //}
            }
            //else
            //{
            //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //                objModel.errorMessage);
            //}

            // ViewState["IdUsulan"] = usulanKegiatan.idUsulan;

        }
        private void isiRAB(List<ItemRencanaAnggaran> listAnggaran, int lamaKegiatan)
        {
            //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
            lvRABTahun1.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 1).ToList();
            lvRABTahun1.DataBind();
            lblTahun1.Text = listAnggaran.Where(i => i.TahunKegiatan == 1)
                                         .Sum(i => i.Total)
                                         .ToString("N0");

            lvRABTahun2.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 2).ToList();
            lvRABTahun2.DataBind();
            lblTahun2.Text = listAnggaran.Where(i => i.TahunKegiatan == 2)
                                         .Sum(i => i.Total)
                                         .ToString("N0");

            lvRABTahun3.DataSource = listAnggaran.Where(i => i.TahunKegiatan == 3).ToList();
            lvRABTahun3.DataBind();
            lblTahun3.Text = listAnggaran.Where(i => i.TahunKegiatan == 3)
                                         .Sum(i => i.Total)
                                         .ToString("N0");

            lblTotalDana.Text = "Total RAB " + lamaKegiatan.ToString() + " Tahun Rp. " + listAnggaran.Sum(i => i.Total).ToString("N0");
        }

        protected void lvRABTahun1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //Label lblJenisBelanja = (Label)e.Item.FindControl("lblJenisBelanja");

        }
    }
}