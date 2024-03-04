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
    public partial class pdfFullProposal : System.Web.UI.Page
    {
        pdfUsulanBaru objMdlPdfusulanBaru = new pdfUsulanBaru();
        simlitekkes.Models.Pengusul.PerbaikanUsulan objPerbaikan = new simlitekkes.Models.Pengusul.PerbaikanUsulan();

        protected void Page_Load(object sender, EventArgs e)
        {
            string id_uk = Request.QueryString.Get("id_usulan_kegiatan");
            string is_perbaikan = Request.QueryString.Get("is_perbaikan");
            panelPerbaikan.Visible = false;
            if (id_uk != "")
            {
                if (is_perbaikan == "1")
                {
                    mvJenisProposalPenelitian.SetActiveView(vJenisUsulanPerbaikan);
                    panelPerbaikan.Visible = true;
                }
                else 
                {
                    mvJenisProposalPenelitian.SetActiveView(vJenisUsulanBaru);
                }
                imgKop.ImageUrl = Server.MapPath("~/assets/dist/img/kemenkes.png");
                isiPdfUsulanBaru(id_uk);
                isiAnggota(id_uk);
                isiMitra(id_uk);
                isiLuaran(id_uk);
                pdfRabPerbaikan.Visible = false;
                pdfRab2018.Visible = false;
                pdfRab2019.Visible = false;
                if (is_perbaikan == "1")
                {
                    pdfRabPerbaikan.Visible = true;
                    pdfRabPerbaikan.InitRab(id_uk);
                }
                else if(is_perbaikan == "2018")
                {
                    pdfRab2018.Visible = true;
                    pdfRab2018.InitRab(id_uk);
                }
                else if (int.Parse(is_perbaikan) >= 2019)
                {
                    pdfRab2019.Visible = true;
                    pdfRab2019.InitRab(id_uk);
                }
            }
        }

        private void isiPdfUsulanBaru(string pIdUsulanKegiatan)
        {
            string s = Request.QueryString.Get("id_usulan_kegiatan");
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.GetIdentitasUsulan(ref dt, Guid.Parse(pIdUsulanKegiatan));

            lblIdUsulanKegiatan.Text = pIdUsulanKegiatan;
            lblRencanaPelaksanaan.Text = dt.Rows[0]["rencana_pelaksanaan_penelitian"].ToString();

            lblJudulBlock.Text = "PROPOSAL PENELITIAN " + dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString();

            if (dt.Rows[0]["kd_jenis_kegiatan"].ToString() == "2")
            {
                lblJudulBlock.Text = "PROPOSAL PENGABDIAN KEPADA MASYARAKAT (PPM) " + dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString();
            }

            lblJudul.Text = dt.Rows[0]["judul"].ToString();

            lblRirn.Text = dt.Rows[0]["nama_pilar_transformasi"].ToString();
            lblTema.Text = dt.Rows[0]["bidang"].ToString();
            lblTopik.Text = dt.Rows[0]["topik"].ToString();
            lblRumpunIlmu.Text = dt.Rows[0]["rumpun_ilmu"].ToString();

            lblKategori.Text = dt.Rows[0]["program_hibah"].ToString();
            lblNamaSkema.Text = dt.Rows[0]["nama_skema"].ToString();
            lblStrata.Text = dt.Rows[0]["strata"].ToString();
            lblTktSaatIni.Text = dt.Rows[0]["level_tkt"].ToString();
            lblTkt.Text = dt.Rows[0]["target_akhir_tkt"].ToString();
            lblLama.Text = dt.Rows[0]["lama_penelitian"].ToString();
            lblJudulBlockPerbaikan.Text = "PROPOSAL PENELITIAN " + dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString();
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
            DataTable dt = new DataTable();
            objMdlPdfusulanBaru.GetMitra(ref dt, Guid.Parse(pIdUsulanKegiatan));
            rptMitra.DataSource = dt;
            rptMitra.DataBind();
        }

        private void isiLuaran(string pIdUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objPerbaikan.GetLuaranWajib(ref dt, Guid.Parse(pIdUsulanKegiatan));
            rptLuaranWajib.DataSource = dt;
            rptLuaranWajib.DataBind();

            dt.Clear();
            dt = new DataTable();
            objPerbaikan.GetLuaranTambahan(ref dt, Guid.Parse(pIdUsulanKegiatan));
            rptLuaranTambahan.DataSource = dt;
            rptLuaranTambahan.DataBind();
        }

    }
}