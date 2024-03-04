using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Reviewer
{
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbBeranda_Click(object sender, EventArgs e)
        {
            Session["page"] = 1;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiAdministrasi_Click(object sender, EventArgs e)
        {
            Session["page"] = 2;
            Response.Redirect("Main.aspx");

        }

        protected void lbEvaluasiAdministrasiPengabdian_Click(object sender, EventArgs e)
        {
            Session["page"] = 3;
            Response.Redirect("Main.aspx");

        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void lbEvaluasiSubstansi_Click(object sender, EventArgs e)
        {
            Session["page"] = 11;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiSubstansiPengabdian_Click(object sender, EventArgs e)
        {
            Session["page"] = 12;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiSubstansiLanjutan_Click(object sender, EventArgs e)
        {
            Session["page"] = 21;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiSubstansiLanjutanPengabdian_Click(object sender, EventArgs e)
        {
            Session["page"] = 22;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiPembahasan_Click(object sender, EventArgs e)
        {
            Session["page"] = 31;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiPembahasanPengabdian_Click(object sender, EventArgs e)
        {
            Session["page"] = 32;
            Response.Redirect("Main.aspx");
        }

        protected void lbValidasiLuaranTambahan_Click(object sender, EventArgs e)
        {
            Session["page"] = 41;
            Response.Redirect("Main.aspx");
        }

        protected void lbMonevPenelitian_Click(object sender, EventArgs e)
        {
            Session["page"] = 51;
            Response.Redirect("Main.aspx");
        }

        protected void lbPenilaianLuaran_Click(object sender, EventArgs e)
        {
            Session["page"] = 52;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiPembahasandanVisitasi_Click(object sender, EventArgs e)
        {
            Session["page"] = 31;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiAdministrasiPengabdian_Click1(object sender, EventArgs e)
        {
            Session["page"] = 3;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiSubstansiPengabdian_Click1(object sender, EventArgs e)
        {
            Session["page"] = 12;
            Response.Redirect("Main.aspx");
        }

        protected void lbEvaluasiPembahasandanVisitasiPengabdian_Click(object sender, EventArgs e)
        {
            Session["page"] = 32;
            Response.Redirect("Main.aspx");
        }
    }
}