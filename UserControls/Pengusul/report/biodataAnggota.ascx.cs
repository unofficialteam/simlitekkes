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
    public partial class biodataAnggota : System.Web.UI.UserControl
    {

        Models.Pengusul.berandaPengusul objBerandaPengusul = new Models.Pengusul.berandaPengusul();
        pdfUsulanBaru mdlPdfBaru = new pdfUsulanBaru();
        //Models.login objLogin = new Models.login();
        protected void Page_Load(object sender, EventArgs e)
        {
            //objLogin.autentifikasi("9999901122", "ato123");
            //Session["objLogin"] = objLogin;
            //string idUsulanKegiatan = "c01bc9c0-638f-4023-8046-3dfaaac39904";
            //Session["idPersonalAnggota"] = dta.Rows[a]["id_personal"].ToString();
            //Session["urutanAnggota"] = a + 1;

            if(Request.QueryString.Get("id_personal") != null)
            {
                isiBioDataKetua(Request.QueryString.Get("id_personal"));
                string[] hrf = { "B","C","D","E","F", "G", "H", "I", "J","K","L","M","N","O" };
                int noUrut = int.Parse(Request.QueryString.Get("urutan").ToString());
                lblKeteranganAnggota.Text = hrf[noUrut-1]+". ANGGOTA PENGUSUL " +noUrut.ToString();
            }
        }

        public void isiBioDataKetua(string idPersonal)
        {
            // isi data personal
            DataTable dtCvKetua = new DataTable();
            objBerandaPengusul.getPersonal(ref dtCvKetua, idPersonal);
            if (dtCvKetua.Rows.Count > 0)
            {
                lblNama.Text = dtCvKetua.Rows[0]["nama_lengkap"].ToString();
                lblNidn.Text = dtCvKetua.Rows[0]["nidn"].ToString();
                string pangkat = dtCvKetua.Rows[0]["pangkat"].ToString();
                if (pangkat == "") pangkat = "-";
                lblPangkatJabatan.Text = pangkat + "/" +
                    dtCvKetua.Rows[0]["jabatan_fungsional"].ToString();

                lblIdSinta.Text = dtCvKetua.Rows[0]["id_sinta"].ToString();

                lblHindex.Text = dtCvKetua.Rows[0]["hindex"].ToString();

                lblSurel.Text = dtCvKetua.Rows[0]["surel"].ToString();
            }
            isiJurnal(idPersonal);
            isiBuku(idPersonal);
            isiHki(idPersonal);

            pnlRiwayatAbdimas.Visible = false;
            //if (Request.QueryString.Get("abdimas") != null)
            //{
            //    pnlRiwayatAbdimas.Visible = true;
            //    isiRiwayatPengabdian(idPersonal);
            //}
            //else
            //    pnlRiwayatAbdimas.Visible = false;
        }

        void isiJurnal(string idPersonal)
        {
            DataTable dt = new DataTable();
            objBerandaPengusul.getArtikelJurnal(ref dt, Guid.Parse(idPersonal));

            var dtInter = dt.Select("kd_jenis_publikasi_jurnal = 1");
            if (dtInter.Count() > 0)
            {
                var tblInter = dtInter.CopyToDataTable();
                rptInter.DataSource = tblInter;
                rptInter.DataBind();
            }
            var dtNas = dt.Select("kd_jenis_publikasi_jurnal = 2");
            if (dtNas.Count() > 0)
            {
                var tblNas = dtNas.CopyToDataTable();
                rptNas.DataSource = tblNas;
                rptNas.DataBind();
            }

            DataTable tblPros = new DataTable();
            if (objBerandaPengusul.getArtikelProsiding(ref tblPros, Guid.Parse(idPersonal)))
            {
                rptPros.DataSource = tblPros;
                rptPros.DataBind();
            }
        }

        void isiBuku(string idPersonal)
        {
            var dt = new DataTable();
            objBerandaPengusul.getBuku(ref dt, Guid.Parse(idPersonal));
            rptBuku.DataSource = dt;
            rptBuku.DataBind();
        }

        void isiHki(string idPersonal)
        {
            var dt = new DataTable();
            objBerandaPengusul.listHKIAll(ref dt, Guid.Parse(idPersonal));
            int rr = dt.Rows.Count;
            rptKI.DataSource = dt;
            rptKI.DataBind();
        }

        void isiRiwayatPengabdian(string idPersonal)
        {
            DataTable dt = new DataTable();
            mdlPdfBaru.GetRiwayatPengabdianDidanai(ref dt, idPersonal);
            rptRiwayatPPM.DataSource = dt;
            rptRiwayatPPM.DataBind();
        }

        protected void rptInter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblUrlArtikel = (Label)e.Item.FindControl("lblUrlArtikel");
            if (lblUrlArtikel == null) return;

            if (lblUrlArtikel.Text.Length > 4)
            {
                string linkUrl = (lblUrlArtikel.Text.Length > 20) ? lblUrlArtikel.Text.Substring(0, 20) : lblUrlArtikel.Text;
                lblUrlArtikel.Text = string.Format("<a href='{0}'>{1}</a>", lblUrlArtikel.Text, linkUrl);
            }
            else
            {
                lblUrlArtikel.Text = "-";
            }
        }

        protected void rptNas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblUrlArtikel = (Label)e.Item.FindControl("lblUrlArtikel");
            if (lblUrlArtikel == null) return;

            if (lblUrlArtikel.Text.Length > 4)
            {
                string linkUrl = (lblUrlArtikel.Text.Length > 20) ? lblUrlArtikel.Text.Substring(0, 20) : lblUrlArtikel.Text;
                lblUrlArtikel.Text = string.Format("<a href='{0}'>{1}</a>", lblUrlArtikel.Text, linkUrl);
            }
            else
            {
                lblUrlArtikel.Text = "-";
            }
        }

        protected void rptPros_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblUrlArtikel = (Label)e.Item.FindControl("lblUrlArtikel");
            if (lblUrlArtikel == null) return;

            if (lblUrlArtikel.Text.Length > 4)
            {
                string linkUrl = (lblUrlArtikel.Text.Length > 20) ? lblUrlArtikel.Text.Substring(0, 20) : lblUrlArtikel.Text;
                lblUrlArtikel.Text = string.Format("<a href='{0}'>{1}</a>", lblUrlArtikel.Text, linkUrl);
            }
            else
            {
                lblUrlArtikel.Text = "-";
            }
        }

        protected void rptBuku_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblUrlBuku = (Label)e.Item.FindControl("lblUrlBuku");
            if (lblUrlBuku == null) return;

            if (lblUrlBuku.Text.Length > 4)
            {
                string linkUrl = (lblUrlBuku.Text.Length > 20) ? lblUrlBuku.Text.Substring(0, 20) : lblUrlBuku.Text;
                lblUrlBuku.Text = string.Format("<a href='{0}'>{1}</a>", lblUrlBuku.Text, linkUrl);
            }
            else
            {
                lblUrlBuku.Text = "-";
            }
        }

        protected void rptKI_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblUrlKI = (Label)e.Item.FindControl("lblUrlKI");
            if (lblUrlKI == null) return;

            if (lblUrlKI.Text.Length > 4)
            {
                string linkUrl = (lblUrlKI.Text.Length > 20) ? lblUrlKI.Text.Substring(0, 20) : lblUrlKI.Text;
                lblUrlKI.Text = string.Format("<a href='{0}'>{1}</a>", lblUrlKI.Text, linkUrl);
            }
            else
            {
                lblUrlKI.Text = "-";
            }
        }

    }
}