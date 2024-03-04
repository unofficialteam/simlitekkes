using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;

namespace simlitekkes
{
    public class Global : System.Web.HttpApplication
    {
        Models.Sistem.referensiData objRefData = new Models.Sistem.referensiData();
        Models.Pengusul.berandaPengusul objPengumuman = new Models.Pengusul.berandaPengusul();
        Models.Admin.daftarPengumuman modelPengumuman = new Models.Admin.daftarPengumuman();
        Models.Sistem.konfigurasi objKonfig = new Models.Sistem.konfigurasi();
        Models.Admin.daftarRunningText objRunText = new Models.Admin.daftarRunningText();
        protected void Application_Start(object sender, EventArgs e)
        {
            Application["tokensinta"] = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9hcGkuc2ludGEucmlzdGVrZGlrdGkuZ28uaWRcL2Z1c2lvXC9wdWJsaWMiLCJzdWIiOiJjZTVlYmY4NC1iYTY1LTU2ZTYtYTY2NC00MDY5OTIyOGY0NDciLCJpYXQiOjE2MDIxOTY2NDYsImV4cCI6MTYwMjIwMDI0NiwibmFtZSI6IlNJTUxJVEVLS0VTIn0.LmlMJfpKGH9hbOUMY1C17yvJOtqRBuer6oHShs4VphU";
            Application["urlSinta"] = "https://api.sinta.kemdikbud.go.id/v2/author/detail/overview/";

            // URL SINTA SIMLITABMAS
            Application["urlSinta1"] = "https://sinta.kemdikbud.go.id/api/select?";
            Application["tokensinta1"] = "a39e735fd5049ba1f7ff0b4e05c9f207";

            DataTable dt = new DataTable();
            if (objRefData.getBidangFokus(ref dt))
                Application["BidangFokus"] = dt;

            dt = new DataTable();
            if (objRefData.getJenisKegiatan(ref dt))
                Application["JenisKegiatan"] = dt;

            dt = new DataTable();
            if (objRefData.getJenisKelamin(ref dt))
                Application["JenisKelamin"] = dt;

            dt = new DataTable();
            if (objRefData.getJenisKategoriTahapan(ref dt))
                Application["JenisKategoriTahapan"] = dt;

            dt = new DataTable();
            if (objRefData.getKelompokKegiatan(ref dt))
                Application["KelompokKegiatan"] = dt;

            dt = new DataTable();
            if (objRefData.getKlaster(ref dt))
                Application["Klaster"] = dt;

            dt = new DataTable();
            if (objRefData.getRumpunIlmu(ref dt))
                Application["RumpunIlmu"] = dt;

            dt = new DataTable();
            if (objRefData.getPeranPersonil(ref dt))
                Application["PeranPersonil"] = dt;

            if (objRefData.getTahapanKegiatan(ref dt))
                Application["TahapanKegiatan"] = dt;

            dt = new DataTable();
            if (objRefData.getProgramHibah(ref dt))
                Application["ProgramHibah"] = dt;

            dt = new DataTable();
            if (objRefData.getSkemaKegiatan(ref dt))
                Application["SkemaKegiatan"] = dt;

            dt = new DataTable();
            if (objRefData.getTahapanKegiatanSkema(ref dt))
                Application["TahapanKegiatanSkema"] = dt;

            dt = new DataTable();
            if (objRefData.getPerguruanTinggi(ref dt))
                Application["PerguruanTinggi"] = dt;

            dt = new DataTable();
            if (objRefData.getProvinsi(ref dt))
                Application["Provinsi"] = dt;

            dt = new DataTable();
            if (objRefData.getDaftarKota(ref dt))
                Application["DaftarKota"] = dt;

            dt = new DataTable();
            if (objRefData.getJenDik(ref dt))
                Application["Jendik"] = dt;

            dt = new DataTable();
            if (objRefData.getTargetCapaianLuaran(ref dt))
                Application["TargetCapaianLuaran"] = dt;

            Application["TahunUsulan"] = objKonfig.getTahunUsulan();

            //==================================================

            Application["ada_perubahan_pengumuman"] = false;
            modelPengumuman.currentPage = 0;
            modelPengumuman.rowsPerPage = 0;
            
            DataTable dtMenuTahun = new DataTable();
            if (modelPengumuman.getMenuPengumumanTahun(ref dtMenuTahun))
            {
                Application["PengumumanTahun"] = dtMenuTahun;

                List<DataTable> lstDtBulan = new List<DataTable>();
                for (int a = 0; a < dtMenuTahun.Rows.Count; a++)
                {
                    string tahun = dtMenuTahun.Rows[a]["tahun"].ToString();
                    DataTable dtBulan = new DataTable();
                    if (modelPengumuman.getMenuPengumumanBulan(ref dtBulan, tahun))
                    {
                        lstDtBulan.Add(dtBulan);
                    }

                }
                Application["lstDtBulan"] = lstDtBulan;
            }

            DataTable dtrt = new DataTable();
            int id_peran = 0;
            if (objRunText.getRunningText(ref dtrt, id_peran))
            {
                dtrt = objRunText.currentRecords;
                {
                    if (dtrt.Rows.Count > 0)
                    {
                        string strRt = dtrt.Rows[0]["get_running_text_by_peran"].ToString();
                        Application["running_text"] = strRt;
                    }
                }
            }

            DataTable dtPengumuman = new DataTable();
            if (objPengumuman.getPengumuman(ref dtPengumuman, 10, 0))
            {
                Application["JmlDataPengumuman"] = objPengumuman.currentRecords.Rows.Count;
                objPengumuman.currentRecords.Clear();

                objPengumuman.rowsPerPage = 10;
                objPengumuman.getCurrRecordsFrontpages();
                dtPengumuman = objPengumuman.currentRecords;
                Application["Pengumuman"] = dtPengumuman;

                List<DataTable> lstDt = new List<DataTable>();
                for (int a = 0; a < dtPengumuman.Rows.Count; a++)
                {
                    DataTable dtLampiranPengumuman = new DataTable();
                    string id_pengumuman = dtPengumuman.Rows[a]["id_pengumuman"].ToString();
                    if (objPengumuman.getLampiranPengumuman(ref dtLampiranPengumuman, id_pengumuman))
                    {
                        lstDt.Add(dtLampiranPengumuman);
                    }
                }
                Application["LampiranPengumuman"] = lstDt;
            }

            string tgl = DateTime.Now.Day.ToString();
            string jam = DateTime.Now.Hour.ToString();
            Application["tgl_jam"] = tgl + jam;

            Application["OnlineUsers"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            Application.Lock();
            Application["OnlineUsers"] = Convert.ToInt32(Application["OnlineUsers"]) + 1;
            Application.UnLock();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["OnlineUsers"] = Convert.ToInt32(Application["OnlineUsers"]) - 1;
            Application.UnLock();
        }

    }
}