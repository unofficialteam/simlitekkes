using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class menuAbdimas : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void setLaman(int kodeLaman)
        {
            Session["page"] = kodeLaman;
            Response.Redirect("Main.aspx");
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx");

        }

        protected void lbBeranda_Click(object sender, EventArgs e)
        {
            setLaman(1);
        }

        protected void lbRekapUsulanBaru_Click(object sender, EventArgs e)
        {
            setLaman(11);
        }

        protected void lbSinkronisasi_Click(object sender, EventArgs e)
        {
            setLaman(41);
        }

        protected void lbPencarianAkunDosen_Click(object sender, EventArgs e)
        {
            setLaman(42);
        }
    }
}