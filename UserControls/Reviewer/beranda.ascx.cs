using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Reviewer
{
    public partial class beranda : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Reviewer.evaluasiAdministrasi objEvaluasi = new Models.Reviewer.evaluasiAdministrasi();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];


            DataTable dtBeranda = new DataTable();
            Guid id_personal = Guid.Parse(objLogin.idPersonal.ToString());

            if (!IsPostBack)
            {
                if (objEvaluasi.getBeranda(ref dtBeranda, id_personal) == true)
                {
                    if (dtBeranda.Rows.Count > 0)
                    {
                        pnl_panduan.Visible = false;

                    }
                    else
                    {
                        pnl_panduan.Visible = false;
                    }
                }
            }

        }
    }
}