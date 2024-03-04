using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.report
{
    public partial class bdta : System.Web.UI.Page
    {
        Models.login objLogin = new Models.login();
        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin.autentifikasi("9999901122", "ato123");
            Session["objLogin"] = objLogin;
            //string idUsulanKegiatan = "c01bc9c0-638f-4023-8046-3dfaaac39904";
            pdfBioData.isiBioDataKetua(objLogin.idPersonal);
        }


    }
}