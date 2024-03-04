using simlitekkes.Models;
using simlitekkes.Models.Pengusul;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class riwayatPengabdian : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
        }

        public void isiRiwayatPengabdian()
        {
            var modelPengusul = new berandaPengusul();
            var dt = new DataTable();
            var objLogin = (login)Session["objLogin"];

            if (modelPengusul.getRiwayatPengabdian(ref dt, Guid.Parse(objLogin.idPersonal)))
            {
                lvRiwayatPengabdian.DataSource = dt;
                lvRiwayatPengabdian.DataBind();
            }
        }
    }
}