using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CleanSession()
        {
            if(Session["usulan_kegiatan"] != null)
                Session.Remove("usulan_kegiatan");
            if (Session["ktUnggah"] != null)
                Session.Remove("ktUnggah");
            if (Session["id_usulan_kegiatan"] != null)
                Session.Remove("id_usulan_kegiatan");
            if (Session["isEdit"] != null)
                Session.Remove("isEdit");
            if (Session["idPersonalAnggota"] != null)
                Session.Remove("idPersonalAnggota");
            if (Session["urutanAnggota"] != null)
                Session.Remove("urutanAnggota");
            if(Session["AtributUnduh"] != null)
                Session.Remove("AtributUnduh");

        }

        protected void lbBeranda_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 1;
            Response.Redirect("Main.aspx");
        }
        //===========================================================
        protected void lbUsulanBaru_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 2;
            Response.Redirect("Main.aspx");
        }

        protected void lbUsulanLanjutan_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 3;
            Response.Redirect("Main.aspx");
        }

        protected void lbLaporanKemajuan_Click(object sender, EventArgs e)
        {
            Session["page"] = 4;
            Response.Redirect("Main.aspx");
        }

        protected void lbLaporanAkhir_Click(object sender, EventArgs e)
        {
            Session["page"] = 5;
            Response.Redirect("Main.aspx");
        }

        protected void lbArsip_Click(object sender, EventArgs e)
        {
            //Session["page"] = 6;
            //Response.Redirect("Main.aspx");
        }


        protected void lbPerbaikanUsulan_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 7;
            Response.Redirect("Main.aspx");
        }

        //==============================================================
        protected void lbUsulanBaruPengabdian_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 10;
            Response.Redirect("Main.aspx");
            //new uiNotify().Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pengajuan usulan baru Pengabdian Kepada Masyarakat belum dibuka.");
        }

        protected void lbUsulanLanjutanPengabdian_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 11;
            Response.Redirect("Main.aspx");
        }

        protected void lbRiwayatUsulan_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 21;
            Response.Redirect("Main.aspx");
        }

        protected void lbRekapLuaran_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 31;
            Response.Redirect("Main.aspx");
        }

        protected void lbPendaftaranRevPenelitian_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 32;
            Response.Redirect("Main.aspx");
        }

        protected void lbPendaftaranRevInternalPTPenelitian_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 33;
            Response.Redirect("Main.aspx");
        }

        protected void lbPendaftaranRevPPM_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 34;
            Response.Redirect("Main.aspx");
        }

        protected void lbPengembalianDana_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 41;
            Response.Redirect("Main.aspx");
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void lbSPTB_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 42;
            Response.Redirect("Main.aspx");

        }

        protected void lbCatatanHarian_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 43;
            Response.Redirect("Main.aspx");
        }

        protected void lbUsulanPendanaan2021_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 8;
            Response.Redirect("Main.aspx");
        }

        protected void lbPerbaikanUsulanAbdimas_Click(object sender, EventArgs e)
        {
            CleanSession();
            Session["page"] = 12;
            Response.Redirect("Main.aspx");
        }
    }
}