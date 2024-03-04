using simlitekkes.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class beranda : System.Web.UI.UserControl
    {
        daftarRunningText modelRunningText = new daftarRunningText();
        Models.login objLogin;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            if (!IsPostBack)
            {
                this.mrqRunningText.InnerHtml = "Tidak Ada Pengumuman";
                DataTable dt = new DataTable();
                modelRunningText.getRunningText(ref dt, objLogin.idPeran, Guid.Parse(objLogin.idPersonal));
                if (dt.Rows.Count > 0)
                {
                    this.mrqRunningText.InnerHtml = dt.Rows[0]["get_running_text_by_peran2"].ToString().Replace("</div>", "").Replace("<div>", "");
                }
            }
        }
    }
}