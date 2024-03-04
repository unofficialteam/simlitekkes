using simlitekkes.Models.report;
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
    public partial class pdfFullProposalAbdimas : System.Web.UI.Page
    {
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        pdfUsulanBaru objMdlPdfusulanBaru = new pdfUsulanBaru();
        Models.Pengusul.luaran objLuaran = new Models.Pengusul.luaran();
        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();

        Models.Pengusul.mitraAbdimas objMitraAbdimas = new Models.Pengusul.mitraAbdimas();
        protected void Page_Load(object sender, EventArgs e)
        {
            string id_uk = Request.QueryString.Get("id_usulan_kegiatan");
            if (id_uk != "")
            {
                DataTable dt = new DataTable();
                objModelIdentitasUsulan.getDataDataUsulanKegiatan(ref dt, id_uk);
                //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
                //imgKop.ImageUrl = Server.MapPath("~/Images/icon/ristekdikti.png");
                imgKop.ImageUrl = Server.MapPath("~/assets/dist/img/kemenkes.png");
                isiPdfUsulanBaru(id_uk);
                isiAnggota(id_uk);
                int idSkema = int.Parse(dt.Rows[0]["id_skema"].ToString());
                isiMitra(id_uk, idSkema);
                string idUsulan = dt.Rows[0]["id_usulan"].ToString();
                isiLuaran(idUsulan);

                int thnPelaksanaan = int.Parse(dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString());
                string thnUsulan = dt.Rows[0]["thn_usulan_kegiatan"].ToString();
                //lblThnPelaksanaanPengabdian.Text = thnPelaksanaan.ToString();

                lblJudulBlock.Text = "PROPOSAL PENGABDIAN KEPADA MASYARAKAT (PPM) " + thnUsulan;

                if (thnPelaksanaan >= 2020)
                {
                    //if (idSkema == 56 || idSkema == 57)
                    //{
                        pdfRab.Visible = false;
                        pdfRab2019.Visible = true;
                        pdfRab2019.InitRab(id_uk);
                    //}
                    //else
                    //{
                    //    pdfRab.Visible = true;
                    //    pdfRab2019.Visible = false;
                    //    pdfRab.InitRab(id_uk);
                    //}
                }
                else
                {
                    pdfRab.Visible = true;
                    pdfRab2019.Visible = false;
                    pdfRab.InitRab(id_uk);
                }
            }
        }

        private void isiPdfUsulanBaru(string pIdUsulanKegiatan)
        {
            string s = Request.QueryString.Get("id_usulan_kegiatan");
            //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.GetIdentitasUsulan(ref dt, Guid.Parse(pIdUsulanKegiatan));

            lblIdUsulanKegiatan.Text = pIdUsulanKegiatan;
            lblRencanaPelaksanaan.Text = dt.Rows[0]["rencana_pelaksanaan_penelitian"].ToString();
            
            lblJudul.Text = dt.Rows[0]["judul"].ToString();
            lblBidangFokus.Text = dt.Rows[0]["bidang"].ToString();
            lblKategori.Text = dt.Rows[0]["program_hibah"].ToString();
            lblNamaSkema.Text = dt.Rows[0]["nama_skema"].ToString();
            lblLamaKegiatan.Text = dt.Rows[0]["lama_penelitian"].ToString();
            lblJmlMhs.Text = dt.Rows[0]["jml_mhs_terlibat"].ToString();
        }

        private void isiAnggota(string pIdUsulanKegiatan)
        {
            //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.GetPersonil(ref dt, Guid.Parse(pIdUsulanKegiatan));
            rptAnggota.DataSource = dt;
            rptAnggota.DataBind();
        }

        private void isiMitra(string idUsulanKegiatan, int pIdSkema)
        {
            //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
            //DataTable dt = new DataTable();
            //objMitraAbdimas.listMitraPelaksanaPengabdian(ref dt, Guid.Parse(pIdUsulanKegiatan));
            //rptMitra.DataSource = dt;
            //rptMitra.DataBind();
            DataTable dtMitra = new DataTable();
            DataTable dtKelmas = new DataTable();
            if (pIdSkema == 55) // ppdm 
            {
                if (objMitraAbdimas.listMitraPelaksanaPpdm(ref dtMitra, Guid.Parse(idUsulanKegiatan)))
                {
                    rptMitra.DataSource = dtMitra;
                    rptMitra.DataBind();
                };
                if (objMitraAbdimas.getMitraKelompokMasyibdm(ref dtKelmas, idUsulanKegiatan))
                {
                    rptMitraSasaranPPDM.DataSource = dtKelmas;
                    rptMitraSasaranPPDM.DataBind();
                };
                rptMitraSasaranPpmUpt.Visible = false;
            }
            else if (pIdSkema == 23) //  ppmupt
            {
                if (objMitraAbdimas.listMitraPelaksanaPpdm(ref dtMitra, Guid.Parse(idUsulanKegiatan)))
                {
                    rptMitra.DataSource = dtMitra;
                    rptMitra.DataBind();
                };

                if (objMitraAbdimas.listMitraSasaranPPMUPT(ref dtKelmas, Guid.Parse(idUsulanKegiatan)))
                {
                    rptMitraSasaranPpmUpt.DataSource = dtKelmas;
                    rptMitraSasaranPpmUpt.DataBind();
                };
                rptMitraSasaranPPDM.Visible = false;

            }
            else
            {
                if (objMitraAbdimas.listMitraPelaksanaPengabdian(ref dtMitra, Guid.Parse(idUsulanKegiatan)))
                {
                    rptMitra.DataSource = dtMitra;
                    rptMitra.DataBind();
                };
                rptMitraSasaranPPDM.Visible = false;
                rptMitraSasaranPpmUpt.Visible = false;
            }

        }


        private void isiLuaran(string pIdUsulan)
        {
            //objUsulanKegiatan = (usulanKegiatan)Session["usulan_kegiatan"];
            //DataTable dt = new DataTable();
            //objMdlPdfusulanBaru.GetLuaranWajib(ref dt, Guid.Parse(pIdUsulanKegiatan));
            //rptLuaranWajib.DataSource = dt;
            //rptLuaranWajib.DataBind();

            DataTable dt = new DataTable();
            int idKelompok = 1;
            if (objLuaran.listLuaranPengabdian(ref dt, Guid.Parse(pIdUsulan), idKelompok))
            {
                for(int a=0; a<dt.Rows.Count; a++)
                {
                    string nama_target_capaian_luaran = dt.Rows[a]["nama_target_capaian_luaran"].ToString();
                    if(nama_target_capaian_luaran=="")
                    {
                        DataRow dr = dt.Rows[a];
                        dr.Delete();
                    }
                }
                rptLuaranWajib.DataSource = dt;
                rptLuaranWajib.DataBind();
            }

            //dt = new DataTable();
            //objMdlPdfusulanBaru.GetLuaranTambahan(ref dt, Guid.Parse(pIdUsulanKegiatan));
            //rptLuaranTambahan.DataSource = dt;
            //rptLuaranTambahan.DataBind();

            DataTable dtt = new DataTable();
            if (objLuaran.ListTargetLuaranTambahanPengabdian2019(ref dtt, Guid.Parse(pIdUsulan)))
            {
                for (int a = 0; a < dtt.Rows.Count; a++)
                {
                    string nama_target_capaian_luaran = dtt.Rows[a]["nama_target_capaian_luaran"].ToString();
                    if (nama_target_capaian_luaran == "")
                    {
                        DataRow dr = dtt.Rows[a];
                        dr.Delete();
                    }
                }
                rptLuaranTambahan.DataSource = dtt;
                rptLuaranTambahan.DataBind();
            }

        }

        protected void rptMitra_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblDana3 = (Label)e.Item.FindControl("lblDana3");
                Label lblDanaThn3 = (Label)e.Item.FindControl("lblDanaThn3");
                Label lblDana2 = (Label)e.Item.FindControl("lblDana2");
                Label lblDanaThn2 = (Label)e.Item.FindControl("lblDanaThn2");
                if (lblLamaKegiatan.Text == "1")
                {
                    lblDana3.Visible = false;
                    lblDanaThn3.Visible = false;
                    lblDana2.Visible = false;
                    lblDanaThn2.Visible = false;
                }
                else if (lblLamaKegiatan.Text == "2")
                {
                    lblDana3.Visible = false;
                    lblDanaThn3.Visible = false;
                }
            }
        }
    }
}