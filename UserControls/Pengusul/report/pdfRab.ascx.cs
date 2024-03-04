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
    public partial class pdfRab : System.Web.UI.UserControl
    {
        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        RAB objModel = new Models.Pengusul.RAB();
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

            if(dt.Rows[0]["level_tkt_target"].ToString()!="")
            {
                objUsulanKegiatan.tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString());
            }
            if (dt.Rows[0]["id_kategori_sbk"].ToString() != "")
            {
                objUsulanKegiatan.idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString());
            }

            panelThn2.Visible = true;
            panelThn3.Visible = true;
            if (objUsulanKegiatan.lamaKegiatan==1)
            {
                panelThn2.Visible = false;
                panelThn3.Visible = false;
            }
            else if(objUsulanKegiatan.lamaKegiatan == 2)
            {
                panelThn3.Visible = false;
            }

            //ViewState["usulan_kegiatan"] = objUsulanKegiatan;

            InitRAB(objUsulanKegiatan);
            //List<ItemRencanaAnggaran> listAnggaran;
        }


        private void InitRAB(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
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

                isiRAB(listAnggaran, usulanKegiatan.lamaKegiatan);

                dt.Clear();
                objModel.GetBidangFokus(ref dt, Guid.Parse(usulanKegiatan.idUsulan));
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

            lblTotalDana.Text = "Total RAB "+ lamaKegiatan.ToString() + " Tahun Rp. "+ listAnggaran.Sum(i => i.Total).ToString("N0");
        }
    }
}