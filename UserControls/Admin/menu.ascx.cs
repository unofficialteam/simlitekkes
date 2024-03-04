using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Admin
{
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            set_menu(Int32.Parse(Session["page"].ToString()));
        }
        protected void set_menu(int halaman)
        {
            switch (halaman)
            {
                case 1:
                    menu_beranda.Attributes.Add("class", "mm-active");
                    menu_rab.Attributes.Remove("class");
                    menu_referensi.Attributes.Remove("class");
                    menu_referensi_mitra.Attributes.Remove("class");
                    menu_referensi_luaran.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_informasi.Attributes.Remove("class");
                    break;
                case 2:
                case 3:
                case 4:
                    menu_beranda.Attributes.Remove("class");
                    menu_referensi.Attributes.Add("class", "mm-active"); 
                    menu_rab.Attributes.Remove("class");
                    menu_referensi_mitra.Attributes.Remove("class");
                    menu_referensi_luaran.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_informasi.Attributes.Remove("class");
                    break;
                case 5:
                case 6:
                    menu_beranda.Attributes.Remove("class");
                    menu_referensi.Attributes.Remove("class");
                    menu_rab.Attributes.Add("class", "mm-active");
                    menu_referensi_mitra.Attributes.Remove("class");
                    menu_referensi_luaran.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_informasi.Attributes.Remove("class");
                    break;
                case 7:
                case 8:
                    menu_beranda.Attributes.Remove("class");
                    menu_referensi.Attributes.Remove("class");
                    menu_rab.Attributes.Remove("class");
                    menu_referensi_mitra.Attributes.Add("class", "mm-active");
                    menu_referensi_luaran.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_informasi.Attributes.Remove("class");
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    menu_beranda.Attributes.Remove("class");
                    menu_referensi.Attributes.Remove("class");
                    menu_rab.Attributes.Remove("class");
                    menu_referensi_mitra.Attributes.Remove("class");
                    menu_referensi_luaran.Attributes.Add("class", "mm-active");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_informasi.Attributes.Remove("class");
                    break;
                case 15:
                case 20:
                case 21:
                case 22:
                    menu_beranda.Attributes.Remove("class");
                    menu_referensi.Attributes.Remove("class");
                    menu_rab.Attributes.Remove("class");
                    menu_referensi_mitra.Attributes.Remove("class");
                    menu_referensi_luaran.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Add("class", "mm-active"); 
                    menu_informasi.Attributes.Remove("class");
                    break;
                case 16:
                case 17:
                    menu_beranda.Attributes.Remove("class");
                    menu_referensi.Attributes.Remove("class");
                    menu_rab.Attributes.Remove("class");
                    menu_referensi_mitra.Attributes.Remove("class");
                    menu_referensi_luaran.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_informasi.Attributes.Add("class", "mm-active");
                    break;
            }
        }
        protected void setLaman(int kodeLaman)
        {
            Session["page"] = kodeLaman;
            Response.Redirect("Main.aspx");
        }

        protected void lbBeranda_Click(object sender, EventArgs e)
        {
            setLaman(1);
        }

        protected void lbPerguruanTinggi_Click(object sender, EventArgs e)
        {
            setLaman(2);
        }

        protected void lbGenerateToken_Click(object sender, EventArgs e)
        {
            setLaman(3);
        }

        protected void lbProdi_Click(object sender, EventArgs e)
        {
            setLaman(4);
        }

        protected void lbRabKelompokBiaya_Click(object sender, EventArgs e)
        {
            setLaman(5);
        }

        protected void lbRabKomponenBelanja_Click(object sender, EventArgs e)
        {
            setLaman(6);
        }

        protected void lbKategoriMitra_Click(object sender, EventArgs e)
        {
            setLaman(7);
        }

        protected void lbMitraWajib_Click(object sender, EventArgs e)
        {
            setLaman(8);
        }

        protected void lbKategoriJenisLuaran_Click(object sender, EventArgs e)
        {
            setLaman(9);
        }

        protected void lbJenisLuaran_Click(object sender, EventArgs e)
        {
            setLaman(10);
        }

        protected void lbBuktiLuaran_Click(object sender, EventArgs e)
        {
            setLaman(11);
        }

        protected void lbPeranPenulis_Click(object sender, EventArgs e)
        {
            setLaman(12);
        }

        protected void lbJenisProsiding_Click(object sender, EventArgs e)
        {
            setLaman(13);
        }

        protected void lbJenisSKDokumen_Click(object sender, EventArgs e)
        {
            setLaman(14);
        }

        protected void lbDataPendukungPusat_Click(object sender, EventArgs e)
        {
            setLaman(15);
        }

        protected void lbPengumuman_Click(object sender, EventArgs e)
        {
            setLaman(16);
        }

        protected void lbRunningText_Click(object sender, EventArgs e)
        {
            setLaman(17);
        }

        protected void lbPersonalPPSDM_Click(object sender, EventArgs e)
        {
            setLaman(18);
        }
        protected void lbDataKlasterPerguruanTinggi_Click(object sender, EventArgs e)
        {
            setLaman(20);
        }
        protected void lbDataKategoriSBK_Click(object sender, EventArgs e)
        {
            setLaman(21);
        }
        protected void lbDataPeran_Click(object sender, EventArgs e)
        {
            setLaman(22);
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void lbkelola_Click(object sender, EventArgs e)
        {
            setLaman(19);
        }
    }
}