using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.ManajemenKasubdit
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

        protected void lbPenetapanReviewerPpm_Click(object sender, EventArgs e)
        {
            Session["page"] = 2;
            Response.Redirect("Main.aspx");
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}