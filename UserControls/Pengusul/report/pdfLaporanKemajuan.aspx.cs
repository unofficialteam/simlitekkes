using simlitekkes.Models.pelaksanaan;
using simlitekkes.Models.report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.report
{
    public partial class pdfLaporanKemajuan : System.Web.UI.Page
    {
        pdfUsulanBaru objMdlPdfusulanBaru = new pdfUsulanBaru();
        simlitekkes.Models.Pengusul.PerbaikanUsulan objPerbaikan = new simlitekkes.Models.Pengusul.PerbaikanUsulan();

        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        Models.Pengusul.lapKemajuan modelLapKemajuan = new Models.Pengusul.lapKemajuan();

        protected void Page_Load(object sender, EventArgs e)
        {
            imgKop.ImageUrl = Server.MapPath("~/assets/dist/img/kemenkes.png");
            string id_transaksi_kegiatan = Request.QueryString.Get("id_transaksi_kegiatan");
            //string id_usulan_kegiatan = Request.QueryString.Get("id_usulan_kegiatan");


            DataTable dt = new DataTable();
            mdlLapKemajuan.getRingkasan(ref dt, id_transaksi_kegiatan);
            if (dt.Rows.Count > 0)
            {
                string id_usulan_kegiatan = dt.Rows[0]["id_usulan_kegiatan"].ToString();

                DataTable dtm = new DataTable();
                modelLapKemajuan.getIdTransaksiDimonev(ref dtm, id_usulan_kegiatan);

                lblProteksi.Text = "PROTEKSI ISI LAPORAN KEMAJUAN PENELITIAN";
                lblJudulBlock.Text = "LAPORAN KEMAJUAN PENELITIAN"; //  MULTI TAHUN";
                lblJudulKemajuanAkhir.Text = "Kemajuan";
                lbNo6.Text = "6. KEMAJUAN PENELITIAN";
                string page = Session["page"].ToString();
                if (page == "5" || page == "29" || page == "35")
                {
                    //string sts_laporan_akhir = dtm.Rows[0]["sts_laporan_akhir"].ToString();
                    //sts_laporan_akhir = "0"; // sementara simlitabkes lap kemajuan saja
                    //if (sts_laporan_akhir == "1")
                    //{
                        lblProteksi.Text = "PROTEKSI ISI LAPORAN AKHIR PENELITIAN";
                    lblJudulBlock.Text = "LAPORAN AKHIR PENELITIAN"; // TAHUN TUNGGAL";
                        lblJudulKemajuanAkhir.Text = "Akhir";
                        lbNo6.Text = "6. HASIL PENELITIAN";
                    //}
                }

                // sts_laporan_akhir

                //lblRingkasan.Text = dt.Rows[0]["ringkasan"].ToString();

                string ringkasan = dt.Rows[0]["ringkasan"].ToString();
                string[] lines = ringkasan.Split('\n');

                string paragraph = string.Empty;
                for (int a = 0; a < lines.Length; a++)
                {
                    lines[a] = "<div style=\"text-align: justify; font-family: 'Arial', Arial, sans-serif; font-size: 14px;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lines[a] + "</div>";
                    paragraph += lines[a];
                }
                lblRingkasan.Text = paragraph;

                lblKeyword.Text = dt.Rows[0]["keyword"].ToString();
                isiIdentitas(id_usulan_kegiatan);
                isiAnggota(id_usulan_kegiatan);
                isiMitra(id_usulan_kegiatan);
                isiLuaran(id_usulan_kegiatan);
                //pdfRab2018.Visible = true;
                //pdfRab2018.InitRab(id_usulan_kegiatan);

                string is_perbaikan = dt.Rows[0]["sdh_diperbaiki"].ToString(); // usulan perbaikan
                pdfRabPerbaikan.Visible = false;
                pdfRab2018.Visible = false;
                pdfRab2019.Visible = false;
                //if (is_perbaikan == "1")
                //{
                //    pdfRabPerbaikan.Visible = true;
                //    pdfRabPerbaikan.InitRab(id_usulan_kegiatan);
                //}
                //else if (is_perbaikan == "2018")
                //{
                //    pdfRab2018.Visible = true;
                //    pdfRab2018.InitRab(id_usulan_kegiatan);
                //}
                //else
                //{
                //    pdfRab2019.Visible = true;
                //    pdfRab2019.InitRab(id_usulan_kegiatan);
                //}
                
                //pdfRab2019.Visible = true;
                //pdfRab2019.InitRab(id_usulan_kegiatan);

                pdfRabPerbaikan.Visible = true;
                pdfRabPerbaikan.InitRab(id_usulan_kegiatan);
            }
        }

        private void isiIdentitas(string pIdUsulanKegiatan)
        {
            //string s = Request.QueryString.Get("id_usulan_kegiatan");
            //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.GetIdentitasUsulan(ref dt, Guid.Parse(pIdUsulanKegiatan));

            lblIdUsulanKegiatan.Text = pIdUsulanKegiatan;
            lblRencanaPelaksanaan.Text = //dt.Rows[0]["rencana_pelaksanaan_penelitian"].ToString();
                string.Format("tahun ke-{0} dari {1} tahun",
                dt.Rows[0]["urutan_thn_usulan_kegiatan"].ToString(),
                dt.Rows[0]["lama_penelitian"].ToString()
                );

            //lblJudulBlock.Text = "PROPOSAL PENELITIAN " + dt.Rows[0]["thn_usulan_kegiatan"].ToString();

            lblJudul.Text = dt.Rows[0]["judul"].ToString();

            lblRirn.Text = dt.Rows[0]["bidang"].ToString();
            lblTema.Text = dt.Rows[0]["tema"].ToString();
            lblTopik.Text = dt.Rows[0]["topik"].ToString();
            lblRumpunIlmu.Text = dt.Rows[0]["rumpun_ilmu"].ToString();

            lblKategori.Text = dt.Rows[0]["program_hibah"].ToString();
            lblNamaSkema.Text = dt.Rows[0]["nama_skema"].ToString();
            lblStrata.Text = dt.Rows[0]["strata"].ToString();
            lblSbk.Text = dt.Rows[0]["sbk"].ToString();
            lblTkt.Text = dt.Rows[0]["target_akhir_tkt"].ToString();
            lblLama.Text = dt.Rows[0]["lama_penelitian"].ToString();
        }

        private void isiAnggota(string pIdUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.GetPersonil(ref dt, Guid.Parse(pIdUsulanKegiatan));
            rptAnggota.DataSource = dt;
            rptAnggota.DataBind();
        }

        private void isiMitra(string pIdUsulanKegiatan)
        {
            //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.GetMitra(ref dt, Guid.Parse(pIdUsulanKegiatan));
            rptMitra.DataSource = dt;
            rptMitra.DataBind();
        }

        private void isiLuaran(string pIdUsulanKegiatan)
        {
            //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
            DataTable dt = new DataTable();
            //objMdlPdfusulanBaru.GetLuaranWajib(ref dt, Guid.Parse(pIdUsulanKegiatan));
            objMdlPdfusulanBaru.GetLuaran(ref dt, Guid.Parse(pIdUsulanKegiatan), 1);

            rptLuaranWajib.DataSource = dt;
            rptLuaranWajib.DataBind();

            dt.Clear();
            dt = new DataTable();
            //objMdlPdfusulanBaru.GetLuaranTambahan(ref dt, Guid.Parse(pIdUsulanKegiatan));
            objMdlPdfusulanBaru.GetLuaran(ref dt, Guid.Parse(pIdUsulanKegiatan), 2);
            rptLuaranTambahan.DataSource = dt;
            rptLuaranTambahan.DataBind();
        }

    }
}