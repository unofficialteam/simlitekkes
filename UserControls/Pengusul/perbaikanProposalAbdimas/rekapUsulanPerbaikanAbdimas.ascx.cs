using simlitekkes.Models.Sistem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.perbaikanProposalAbdimas
{
    public partial class rekapUsulanPerbaikanAbdimas : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool initUsulan(string idUsulanKegiatan)
        {
            //DataTable dt = new DataTable();
            //objModelIdentitasUsulan.getDataDataUsulanKegiatan(ref dt, idUsulanKegiatan);
            //ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            //var objUsulanKegiatan = new usulanKegiatan()
            //{
            //    idUsulanKegiatan = idUsulanKegiatan,
            //    idUsulan = dt.Rows[0]["id_usulan"].ToString(),
            //    judul = dt.Rows[0]["judul"].ToString(),
            //    idSkema = int.Parse(dt.Rows[0]["id_skema"].ToString()),
            //    namaSkema = dt.Rows[0]["nama_skema"].ToString(),
            //    thnUsulan = dt.Rows[0]["thn_usulan_kegiatan"].ToString(),
            //    thnPelaksanaan = dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString(),
            //    lamaKegiatan = int.Parse(dt.Rows[0]["lama_kegiatan"].ToString()),
            //    urutanTahunUsulanKegiatan = int.Parse(dt.Rows[0]["urutan_thn_usulan_kegiatan"].ToString()),
            //    tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString()), // test
            //    idBidangFokus = int.Parse(dt.Rows[0]["id_bidang_fokus"].ToString()),
            //    bidangFokus = dt.Rows[0]["bidang_fokus"].ToString(),
            //    idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString())
            //};

            //ViewState["thn_usulan"] = objUsulanKegiatan.thnUsulan;
            //ViewState["thnPelaksanaan"] = objUsulanKegiatan.thnPelaksanaan;

            //DataTable dtSkema = new DataTable();
            //objRefData.getSkemaKegiatan(ref dtSkema);
            //var dr1 = dtSkema.Select("id_skema=" + objUsulanKegiatan.idSkema);
            //DataTable dt3 = dr1.CopyToDataTable();
            //int thnMaks = int.Parse(dt3.Rows[0]["thn_maksimal"].ToString());
            //int lamaKegiatan = objUsulanKegiatan.lamaKegiatan;
            //string strLamaKeg = lamaKegiatan.ToString();
            //if (thnMaks < lamaKegiatan)
            //    strLamaKeg = thnMaks.ToString();
            //ViewState["lama_kegiatan"] = strLamaKeg;

            //ViewState["urutan_thn_usulan_kegiatan"] = objUsulanKegiatan.urutanTahunUsulanKegiatan;
            //objLogin = (Models.login)Session["objLogin"];
            //dt.Clear();
            //dt = new DataTable();
            //objPengusul.getPersonal(ref dt, objLogin.idPersonal);
            //lblNamaNidnKetua.Text = objLogin.namaLengkap + " (" + objLogin.nidn + ")";
            //lblNamaInstitusiNProdi.Text = objLogin.namaInstitusi + " - " + dt.Rows[0]["nama_program_studi"].ToString();
            //lblJenjangPendidikan.Text = dt.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
            //lblSurel.Text = dt.Rows[0]["surel"].ToString();
            //isiDataSinta();
            //isiRekamJejak(objLogin.idPersonal);
            //pnlKekDokumenUsulan.Visible = false;
            //pnlKekAnggota.Visible = false;
            //pnlKekLuaran.Visible = false;
            //pnlKekRab.Visible = false;
            //pnlKekMitra.Visible = false;

            //int retVal = isiAnggotaDikti(idUsulanKegiatan);
            bool enableKirimUsulan = true;

            //string thnPelaksanaan = ViewState["thnPelaksanaan"].ToString();
            //string id_transaksi_perbaikan = "00000000-0000-0000-0000-000000000000";
            //objPerbaikan.getidTransaksi(ref id_transaksi_perbaikan, Guid.Parse(objUsulanKegiatan.idUsulanKegiatan), "14");
            //string dirFile = "~/fileUpload/dokumenUsulanRevisi/" + thnPelaksanaan + "/" + id_transaksi_perbaikan + ".pdf";

            //string path2save = String.Format(dirFile + "/{0}.pdf", id_transaksi_perbaikan);
            //if (!cekFileExists(dirFile)) //  || !isMakroRiset)
            //{
            //    pnlKekDokumenUsulan.Visible = true;
            //    enableKirimUsulan = false;
            //}

            //isiSummaryRAB(objUsulanKegiatan);
            //InitRAB(objUsulanKegiatan);
            //bool cek = cekRAB(objUsulanKegiatan);

            //if (cek == false)
            //{
            //    pnlKekRab.Visible = true;
            //    enableKirimUsulan = false;
            //}

            //retVal = isiLuaran(idUsulanKegiatan);
            //if (retVal == 0)
            //{
            //    pnlKekLuaran.Visible = true;
            //    enableKirimUsulan = false;
            //}

            //retVal = isiMitra(idUsulanKegiatan, objUsulanKegiatan.idSkema);
            return enableKirimUsulan;
        }
    }
}