using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.luaran2019
{
    public partial class naskahAkademik : System.Web.UI.UserControl
    {
        public event EventHandler OnChildEventBatal;

        private int TahunKe
        {
            get { return Convert.ToInt32(ViewState["TahunKe"] ?? "0"); }
            set { ViewState["TahunKe"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Refresh(int tahunKe)
        {
            TahunKe = tahunKe;
            refreshBuktiLuaran();
        }

        protected void refreshBuktiLuaran()
        {
            var listBuktiLuaran = new List<string>();

            switch (TahunKe)
            {
                case 1:
                    listBuktiLuaran.Add("Naskah Policy brief, rekomendasi kebijakan, atau model kebijakan strategis");
                    break;
                default:
                    listBuktiLuaran.Add("Data Bukti Luaran tidak ditemukan !");
                    break;
            }

            rptrBuktiLuaran.DataSource = listBuktiLuaran;
            rptrBuktiLuaran.DataBind();
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {

        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            if (OnChildEventBatal != null)
                OnChildEventBatal(sender, null);
        }
    }
}