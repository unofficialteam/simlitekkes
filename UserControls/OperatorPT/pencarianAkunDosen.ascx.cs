using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using simlitekkes.Models.PT;
using System.Data;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class pencarianAkunDosen : System.Web.UI.UserControl
    {
        daftarSinkronisasiDosen mdlSinkronisasi = new daftarSinkronisasiDosen();
        simlitekkes.Models.login objLogin;
        uiNotify noty = new uiNotify();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbCariNIDN);

            if (!IsPostBack)
            {

            }
        }

        protected void lbCariNIDN_Click(object sender, EventArgs e)
        {
            getAkunDosen();
        }

        private void getAkunDosen()
        {
            DataTable dt = new DataTable();
            mdlSinkronisasi.getAkunDosen(ref dt, tbNIDN.Text.Trim(), objLogin.idInstitusi);
            if (dt.Rows.Count > 0)
            {
                lblNama.Text = dt.Rows[0]["nama"].ToString();
                lblProdi.Text = dt.Rows[0]["nama_program_studi"].ToString();
                lblUsername.Text = dt.Rows[0]["nama_user"].ToString();
                lblPassword.Text = dt.Rows[0]["pswd"].ToString();
            }
            else
            {
                lblNama.Text = "-";
                lblProdi.Text = "-";
                lblUsername.Text = "-";
                lblPassword.Text = "-";
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "NIDN " + tbNIDN.Text.Trim() + " belum terdaftar");
            }
        }
    }
}