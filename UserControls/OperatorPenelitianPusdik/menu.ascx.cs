using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            set_menu(Int32.Parse(Session["page"].ToString()));
        }

        protected void setLaman(int kodeLaman)
        {
            Session["page"] = kodeLaman;
            Response.Redirect("Main.aspx");
        }

        protected void set_menu(int halaman)
        {
            switch (halaman)
            {
                case 1:
                    menu_beranda.Attributes.Add("class", "mm-active"); 
                    menu_penilaian.Attributes.Remove("class");
                    menu_monitoring.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_kakas_bantu.Attributes.Remove("class");
                    menu_monitoring_pengabdian.Attributes.Remove("class");
                    break;
                case 11:
                case 12:
                    menu_beranda.Attributes.Remove("class");
                    menu_penilaian.Attributes.Add("class", "mm-active");
                    menu_monitoring.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_kakas_bantu.Attributes.Remove("class");
                    menu_monitoring_pengabdian.Attributes.Remove("class");
                    break;
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                    menu_beranda.Attributes.Remove("class");
                    menu_penilaian.Attributes.Remove("class");
                    menu_monitoring.Attributes.Add("class", "mm-active");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_kakas_bantu.Attributes.Remove("class");
                    menu_monitoring_pengabdian.Attributes.Remove("class");
                    break;
                case 31:
                case 32:
                    menu_beranda.Attributes.Remove("class");
                    menu_penilaian.Attributes.Remove("class");
                    menu_monitoring.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Add("class", "mm-active");
                    menu_kakas_bantu.Attributes.Remove("class");
                    menu_monitoring_pengabdian.Attributes.Remove("class");
                    break;
                case 41:
                case 42:
                    menu_beranda.Attributes.Remove("class");
                    menu_penilaian.Attributes.Remove("class");
                    menu_monitoring.Attributes.Remove("class");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_kakas_bantu.Attributes.Add("class", "mm-active");
                    menu_monitoring_pengabdian.Attributes.Remove("class");
                    break;
                case 51:
                case 52:
                case 53:
                case 54:
                    menu_beranda.Attributes.Remove("class");
                    menu_penilaian.Attributes.Remove("class");
                    menu_monitoring_pengabdian.Attributes.Add("class", "mm-active");
                    menu_data_pendukung.Attributes.Remove("class");
                    menu_kakas_bantu.Attributes.Remove("class");
                    menu_monitoring.Attributes.Remove("class");
                    break;

            }
        }

        protected void lbBeranda_Click(object sender, EventArgs e)
        {
            setLaman(1);
        }

        #region Penilaian

        protected void lbPenugasanReviewer_Click(object sender, EventArgs e)
        {
            setLaman(11);
        }

        protected void lbPlottingReviewer_Click(object sender, EventArgs e)
        {
            setLaman(12);
        }

        protected void lbPenetapanUsulanBaru_Click(object sender, EventArgs e)
        {
            setLaman(13);
        }

        #endregion

        #region Menu Monitoring

        protected void lbMonitoringUsulanBaru_Click(object sender, EventArgs e)
        {
            setLaman(21);
        }

        protected void lbMonitoringUsulanLanjutan_Click(object sender, EventArgs e)
        {
            setLaman(22);
        }

        protected void lbMonitoringLuaranTambahan_Click(object sender, EventArgs e)
        {
            setLaman(23);
        }

        protected void lbMonitoringPenugasanReviewer_Click(object sender, EventArgs e)
        {
            setLaman(24);
        }

        protected void lbMonitoringPlottingReviewer_Click(object sender, EventArgs e)
        {
            setLaman(25);
        }

        protected void lbMonitoringHasilReview_Click(object sender, EventArgs e)
        {
            setLaman(26);
        }

        protected void lbMonitoringPerbaikanPenelitian_Click(object sender, EventArgs e)
        {
            setLaman(27);
        }

        protected void lbMonitoringLaporanKemajuan_Click(object sender, EventArgs e)
        {
            setLaman(28);
        }

        protected void lbMonitoringLapAkhirTahun_Click(object sender, EventArgs e)
        {
            //setLaman(29);
            CleanSession();
            Session["page"] = 29;
            Response.Redirect("Main.aspx");
        }

        private void CleanSession()
        {
            if (Session["usulan_kegiatan"] != null)
                Session.Remove("usulan_kegiatan");          

        }

        #endregion

        #region Data Pendukung

        protected void lbOperatorPT_Click(object sender, EventArgs e)
        {
            setLaman(31);
        }

        protected void lbDaftarReviewer_Click(object sender, EventArgs e)
        {
            setLaman(32);
        }

        #endregion

        #region Kakas Bantu

        protected void lbPengirimanPswdOperatorPT_Click(object sender, EventArgs e)
        {
            setLaman(41);
        }

        protected void lbEksepsiPengusul_Click(object sender, EventArgs e)
        {
            setLaman(42);
        }

        #endregion

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void lbabdiusulanbaru_Click(object sender, EventArgs e)
        {
            setLaman(51);
        }

        protected void lbabditugasrev_Click(object sender, EventArgs e)
        {
            setLaman(52);

        }

        protected void lbabdiplotting_Click(object sender, EventArgs e)
        {
            setLaman(53);

        }

        protected void lbabdihasilreview_Click(object sender, EventArgs e)
        {
            setLaman(54);

        }

        protected void lbMonitoringMonevEksternal_Click(object sender, EventArgs e)
        {
            setLaman(55);
        }

        protected void lbPerubahanPersonil_Click(object sender, EventArgs e)
        {
            setLaman(56);
        }

        protected void lbDaftarPerubahanJudul_Click(object sender, EventArgs e)
        {
            setLaman(57);
        }
    }
}