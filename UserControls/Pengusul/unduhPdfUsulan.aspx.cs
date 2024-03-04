using simlitekkes.Core;
using simlitekkes.Models.Sistem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class unduhPdfUsulan : System.Web.UI.Page
    {

        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        UIControllers.uiDropdownList obj_uiDDL = new UIControllers.uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        Models.Sistem.referensiData objRefData = new Models.Sistem.referensiData();
        Models.login objLogin;
        kontrolUnggah ktUnggah = new kontrolUnggah();
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();

        string idUsulanKegiatan = "7414f38d-bc90-4117-b996-4ea131c299c0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                initUsulan(idUsulanKegiatan);
                //ImgPdf.ImageUrl = Server.MapPath("~/Images/icon/pdf-red.png");
                //imgSetujuAnggota.ImageUrl = Server.MapPath("~/Images/icon/setuju.png");
                isiIdentitasKetua();
                isiRekamJejak();
                isiIdentitasUsulan();
                isiAnggotaDosen();
                isiAnggotaNonDosen();

            }
        }

        private void initUsulan(string idUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objModelIdentitasUsulan.getDataDataUsulanKegiatan(ref dt, idUsulanKegiatan);
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            var objUsulanKegiatan = new usulanKegiatan()
            {
                idUsulanKegiatan = idUsulanKegiatan,
                idUsulan = dt.Rows[0]["id_usulan"].ToString(),
                judul = dt.Rows[0]["judul"].ToString(),
                idSkema = int.Parse(dt.Rows[0]["id_skema"].ToString()),
                namaSkema = dt.Rows[0]["nama_skema"].ToString(),
                thnUsulan = dt.Rows[0]["thn_usulan_kegiatan"].ToString(),
                thnPelaksanaan = dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString(),
                lamaKegiatan = int.Parse(dt.Rows[0]["lama_kegiatan"].ToString()),
                urutanTahunUsulanKegiatan = int.Parse(dt.Rows[0]["urutan_thn_usulan_kegiatan"].ToString()),
                tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString()) // test
            };

            ViewState["usulan_kegiatan"] = objUsulanKegiatan;
        }


        private void isiIdentitasUsulan()
        {
            setObjUsulanKeg();
            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            lblJudul.Text = objUsulanKegiatan.judul;

            lblSkema.Text = objUsulanKegiatan.namaSkema +" - " +objUsulanKegiatan.kategoriPenelitian;
            lblUrutanDanLamaKegiatan.Text = "(tahun ke-" + objUsulanKegiatan.urutanTahunUsulanKegiatan 
                + " dari "+ objUsulanKegiatan.lamaKegiatan+")";
            //lblSkema.Text = "";
        }
            private void isiIdentitasKetua()
        {

            DataTable dt = new DataTable();
            //objPengusul.getPersonal(ref dt, objLogin.idPersonal);
            getInfoUsulanKeg(ref dt);
            if (dt.Rows.Count > 0)
            {
                lblNamaKetua.Text = dt.Rows[0]["nama_ketua"].ToString();
                lblNidnKetua.Text = "("+dt.Rows[0]["nidn"].ToString()+")";
                lblNamaPtDanProdi.Text = dt.Rows[0]["nama_institusi"].ToString() + " - " +
                    dt.Rows[0]["nama_program_studi"].ToString();
                lblIdSinta.Text = "";
                lblKualifikasi.Text = dt.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
                lblAlamatSurel.Text = dt.Rows[0]["surel"].ToString();
            }
        }

        private void isiRekamJejak()
        {
            DataTable dt = new DataTable();
            getRekamJejak(ref dt);
            lblJmlPubJurnalInternasional.Text = dt.Rows[0]["internasional"].ToString();
            lblJmlPubNasTerakreditasi.Text = dt.Rows[0]["nasional"].ToString();
            lblJmlProsiding.Text = dt.Rows[0]["prosiding"].ToString();
            lblJmlHki.Text = dt.Rows[0]["hki"].ToString();
            lblJmlBuku.Text = dt.Rows[0]["buku"].ToString();
        }

        private void isiAnggotaDosen()
        {
            DataTable dt = new DataTable();
            getAnggota(ref dt);
            lblTotalAnggota.Text = dt.Rows.Count.ToString();
            rptAnggota.DataSource = dt;
            rptAnggota.DataBind();
        }

        private void isiAnggotaNonDosen()
        {
            DataTable dt = new DataTable();
            getAnggotaNonDrpm(ref dt);
            lblTotalNonDrpm.Text = dt.Rows.Count.ToString();
            rptAnggotaNonRistekDikti.DataSource = dt;
            rptAnggotaNonRistekDikti.DataBind();
        }

        //=====================================================
        // Dummy data 
        private void getAnggota(ref DataTable dt)
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("nama_anggota");
            dt.Columns.Add("nama_institusi");
            dt.Columns.Add("urutan_anggota");
            dt.Columns.Add("tugas");
            dt.Columns.Add("status");
            dt.Rows.Add(new object[] { "Joko Waluyo", "Universitas Suralaya", 1, "Membuat program","1" });
            dt.Rows.Add(new object[] { "Siti sundari", "Universitas Suralaya", 2, "Analisis data yang digunakan untuk aplikasi","0" });
        }

        private void getAnggotaNonDrpm(ref DataTable dt)
        {
            //< span style = "color: blue;" ><%# Eval("nama_non_drpm")%></span>&nbsp;(<%# Eval("no_ktp_non_drpm")%>)<br />
            //<%# Eval("nama_institusi")%>&nbsp;&nbsp;<%# Eval("peran")%><br />
            //Tugas: <%# Eval("tugas")%><br />
            //Bidang keahlian: <%# Eval("bidang_keahlian")%>

            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("nama_non_drpm");
            dt.Columns.Add("no_ktp_non_drpm");
            dt.Columns.Add("nama_institusi");
            dt.Columns.Add("peran");
            dt.Columns.Add("tugas");
            dt.Columns.Add("bidang_keahlian");
            dt.Rows.Add(new object[] { "Joko Waluyo","1122334455667788", "Universitas Suralaya", "Mhs bimbingan", "Membuat program", "Programmer" });
            dt.Rows.Add(new object[] { "Siti sundari", "9911223344556677", "Universitas Suralaya", "Mhs bimbingan", "Analisis data yang digunakan untuk aplikasi", "Analis Data" });
        }
        private void setObjUsulanKeg()
        {
            usulanKegiatan obj = new usulanKegiatan
            {
                idUsulanKegiatan = idUsulanKegiatan,
                idUsulan = idUsulanKegiatan,
                judul = "test judul",
                idSkema = 7,
                namaSkema = "Tesis Magister",
                thnUsulan = "2018",
                thnPelaksanaan = "2019",
                lamaKegiatan = 2,
                urutanTahunUsulanKegiatan = 1,
                tktTarget = 5,
                kategoriPenelitian = "Penelitian Terapan"
            };

            ViewState["usulan_kegiatan"] = obj;
        }

        private void getInfoUsulanKeg(ref DataTable dt)
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("nama_ketua");
            dt.Columns.Add("nidn");
            dt.Columns.Add("nama_institusi");
            dt.Columns.Add("nama_program_studi");
            dt.Columns.Add("jenjang_pendidikan_tertinggi");
            dt.Columns.Add("surel");
            dt.Rows.Add(new object[] { "Suryo Waluyo","9999988888", "Universitas Suralaya", "Pertania", "S2", "aaaa@gmail.com" });
        }

        protected void rptAnggota_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Image imgSetuju = (Image)e.Item.FindControl("imgSetujuAnggota");
            Label sts = (Label)e.Item.FindControl("lblStatus");
            if(sts.Text=="1")
            imgSetuju.ImageUrl = Server.MapPath("~/assets/dist/img/setuju.png");
            else if (sts.Text == "0")
                imgSetuju.ImageUrl = Server.MapPath("~/assets/dist/img/pdf-red.png");
        }

        private void getRekamJejak(ref DataTable dt)
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("internasional");
            dt.Columns.Add("nasional");
            dt.Columns.Add("prosiding");
            dt.Columns.Add("hki");
            dt.Columns.Add("buku");
            dt.Rows.Add(new object[] { 1,1,2,3,4 });
        }

        //ktUnggah.path2save = "~/fileUpload/mitra/" + idMitra + ".pdf";
    }
}