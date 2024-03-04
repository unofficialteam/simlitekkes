using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using simlitekkes.Core;
using simlitekkes.Models.Sistem;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class usulanLanjutan : System.Web.UI.UserControl
    {

        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        Models.Pengusul.identitasUsulanLanjutan objModelIdentitasUsulanLanjutan = new Models.Pengusul.identitasUsulanLanjutan();
        UIControllers.uiDropdownList obj_uiDDL = new UIControllers.uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        Models.Sistem.referensiData objRefData = new Models.Sistem.referensiData();
        Models.login objLogin;
        kontrolUnggah ktUnggah = new kontrolUnggah();
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        uiNotify noty = new uiNotify();

        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();
        Models.Pengusul.mitraPenelitian objMitra = new Models.Pengusul.mitraPenelitian();
        //string idUsulanKegiatan = "7414f38d-bc90-4117-b996-4ea131c299c0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
                //initUsulan(ViewState["id_usulan_kegiatan"].ToString());
                mvMain.SetActiveView(vPersyaratan);
                int totalSyarat = persyaratan.isiDataPersyaratanUmum();
                bool isEligible = true;
                if (totalSyarat < 4)
                    isEligible = false;
                lstUsulanLanjutan.isiListUsulanLanjutan(isEligible);
                //lbPengajuanBaru.Visible = false;
                //lblPengajuanBaru.Visible = false;
            }

            lstUsulanLanjutan.OnChildEventOccurs += new EventHandler(Child1_OnChildEventOccurs);
            lstUsulanLanjutan.OnChildEventUnduhProposalLengkap += new EventHandler(Child1_OnChildEventUnduhProposalLengkap);
            ktAnggota.OnChildEventOccurs += new EventHandler(Anggota_OnChildEventOccurs);
            ktAnggota.OnChildBatalEventOccurs += new EventHandler(AnggotaBatal_OnChildEventOccurs);

        }

        void Child1_OnChildEventUnduhProposalLengkap(object sender, EventArgs e)
        {
            pdfUsulanLengkap.UnduhProposalLengkap(Session["id_usulan_kegiatan_unduh"].ToString());
            Session.Remove("id_usulan_kegiatan_unduh");
            //CinitUsulan(Session["id_usulan_kegiatan"].ToString());
            //UnduhProposalLengkap();
        }

        void Anggota_OnChildEventOccurs(object sender, EventArgs e)
        {
            lbLanjutkanAtAnggota.Visible = false;
        }

        void AnggotaBatal_OnChildEventOccurs(object sender, EventArgs e)
        {
            lbLanjutkanAtAnggota.Visible = true;
        }

        void Child1_OnChildEventOccurs(object sender, EventArgs e)
        {
            ViewState["id_usulan_kegiatan"] = lstUsulanLanjutan.getIdUsulanKegiatan();
            initUsulan(lstUsulanLanjutan.getIdUsulanKegiatan());

            mvMain.SetActiveView(vUsulan);
            // start steps 1 Identitas
            Session["isEdit"] = true;
            clearAllButtonStepsIndikator();
            lbIdentitas1.ForeColor = System.Drawing.Color.Green;
            lbIdentitas2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vIdentitas);
            mvIdentitas.SetActiveView(vIDUsulan);

            IdentitasUsulan.InitIdentitasUsulanLanjutan(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            cekStatusKirimUsulan(ViewState["id_usulan_kegiatan"].ToString());

        }

        void ChildPersyaratan_OnOccurs(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vCVKetua);
            cvKetua.isiCvKetua(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
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
                tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString()), // test
                idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString()),
                idBidangFokus = int.Parse(dt.Rows[0]["id_bidang_fokus"].ToString()),
                bidangFokus = dt.Rows[0]["bidang_fokus"].ToString()
            };
            Session["usulan_kegiatan"] = objUsulanKegiatan;
            ViewState["usulan_kegiatan"] = objUsulanKegiatan;
            ViewState["id_skema"] = objUsulanKegiatan.idSkema;
        }

        private void clearAllButtonStepsIndikator()
        {
            lbIdentitas2.CssClass = "btn btn-outline-primary";
            lbSubstansi2.CssClass = "btn btn-outline-primary";
            lbRab2.CssClass = "btn btn-outline-primary";
            lbDokPendukung2.CssClass = "btn btn-outline-primary";
            lbKirimUsulan2.CssClass = "btn btn-outline-primary";

            lbIdentitas1.ForeColor = System.Drawing.Color.Gray;
            lbSubstansi1.ForeColor = System.Drawing.Color.Gray;
            lbRab1.ForeColor = System.Drawing.Color.Gray;
            lbDokPendukung1.ForeColor = System.Drawing.Color.Gray;
            lbKirimUsulan1.ForeColor = System.Drawing.Color.Gray;
            //lblKirimUsulan.Visible = true;
            //lbKirimUsulan.Visible = false;
        }

        protected void lbLanjutkanAtCVKetua_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vUsulan);
            // start steps 1 Identitas

            clearAllButtonStepsIndikator();
            lbIdentitas1.ForeColor = System.Drawing.Color.Green;
            lbIdentitas2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vIdentitas);
            mvIdentitas.SetActiveView(vIDUsulan);

            if (Session["isEdit"] != null)
            {
                if (bool.Parse(Session["isEdit"].ToString()))
                {
                    initUsulan(ViewState["id_usulan_kegiatan"].ToString());
                    cekStatusKirimUsulan(ViewState["id_usulan_kegiatan"].ToString());
                }
                else
                {
                    IdentitasUsulan.InitIdentitasUsulanLanjutan();
                }
            }
            else
            {
                Session["isEdit"] = false;
                IdentitasUsulan.InitIdentitasUsulanLanjutan();
            }

        }

        private void cekStatusKirimUsulan(string pIdUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objModelIdentitasUsulan.getStatusUsulanDikirim(ref dt, pIdUsulanKegiatan);
            if (dt.Rows.Count > 0)
            {
                panelUsulan.Enabled = false;
            }
            else
            {
                panelUsulan.Enabled = true;
            }
        }

        protected void lbLanjutkanAtIDUsulan_Click(object sender, EventArgs e)
        {
            if (!IdentitasUsulan.Simpan()) return;
            mvIdentitas.SetActiveView(vCVAnggota);

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            //initUsulan(objUsulanKegiatan.idUsulanKegiatan);
            string idSkema = ViewState["id_skema"].ToString();
            DataTable dt = new DataTable();
            dt = ktAnggota.isiAnggotaDikti(objUsulanKegiatan.idUsulanKegiatan,
                int.Parse(idSkema));
            ktAnggota.isiAnggotaNonDikti();
            ktAnggota.isiDurasiUsulan();

            ViewState["dt_anggota"] = dt;
        }

        protected void lbLanjutkanAtAnggota_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus diisi terlebih dahulu");
                return;
            }

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            // start steps 2 Substansi
            clearAllButtonStepsIndikator();
            lbSubstansi1.ForeColor = System.Drawing.Color.Green;
            lbSubstansi2.CssClass = "btn btn-success";

            // Inisialisasi form unggah dokumen
            string dirFile = "~/fileUpload/dokumenUsulan/" + objUsulanKegiatan.thnUsulan;
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            ktUnggah.path2save = String.Format(dirFile + "/{0}.pdf", objUsulanKegiatan.idUsulanKegiatan);
            ktUnggah.max_size = 5 * 1000 * 1000; // 5 MB
            ktUnggah.alllowed_ext = "pdf";
            ktUnggah.success_info = "Unggah dokumen usulan berhasil";
            ktUnggah.failed_info = "Unggah dokumen usulan gagal";
            Session.Add("ktUnggah", ktUnggah);

            mvUsulan.SetActiveView(vSubstansi);
            mvSubstansi.SetActiveView(vUnggahDokUsulan);


            ktDokUsulan.setDataUsulan(objUsulanKegiatan.idUsulanKegiatan, objUsulanKegiatan.thnUsulan, ktUnggah.path2save,
                objUsulanKegiatan);//.urutanTahunUsulanKegiatan, objUsulanKegiatan.lamaKegiatan);
        }

        protected void lbLanjutkanAtUnggahDokUsulan_Click(object sender, EventArgs e)
        {
            mvSubstansi.SetActiveView(vLuaran);
            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            //string pidUsulan, int plamakegiatan,int urutan_thn,string pidUsulankegiatan,string pnamaSkema
            //ktLuaran.setDataLuaran(objUsulanKegiatan.idUsulan, objUsulanKegiatan.lamaKegiatan,
            //        objUsulanKegiatan.urutanTahunUsulanKegiatan, objUsulanKegiatan.idUsulanKegiatan,
            //        objUsulanKegiatan.namaSkema, objUsulanKegiatan.tktTarget, objUsulanKegiatan.idSkema);
            //ktLuaran.isiluaran()
        }

        protected void lbLanjutkanAtUnggahDokLuaran_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus diisi terlebih dahulu");
                return;
            }
            Session.Remove("ktUnggah");
            // start steps 3 RAB

            rab.InitRAB((usulanKegiatan)ViewState["usulan_kegiatan"]);

            clearAllButtonStepsIndikator();
            lbRab1.ForeColor = System.Drawing.Color.Green;
            lbRab2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vRAB);
            mvRAB.SetActiveView(vRABIsi);
        }
        protected void lbLanjutkanAtIsiRab_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus diisi terlebih dahulu");
                return;
            }
            // start steps 4 Unggah dokumen pendukung
            if (!rab.Simpan()) return;

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            mitraPenelitian.setData(objUsulanKegiatan.thnUsulan);
            mitraPenelitian.isiMitra(objUsulanKegiatan.idUsulanKegiatan);
            mitraPenelitian.isiMitraPenelitianPerSkema();

            clearAllButtonStepsIndikator();
            lbDokPendukung1.ForeColor = System.Drawing.Color.Green;
            lbDokPendukung2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vUnggahDokPendukung);
            mvDokPendukung.SetActiveView(vUnggahDokPendukungIsi);
            //cekStatusEligibilitasKirimUsulan();

        }

        //private void cekStatusEligibilitasKirimUsulan()
        //{
        //    //lblKirimUsulan.Visible = false;
        //    //lbKirimUsulan.Visible = true;
        //}

        protected void lbLanjutkanAtDataPendukung_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus diisi terlebih dahulu");
                return;
            }
            // start steps 5 kirim usulan     

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            bool enableKirimUsulan = ktRekapUsulan.initUsulan(objUsulanKegiatan.idUsulanKegiatan);
            mvUsulan.SetActiveView(viewKirimUsulan);
            clearAllButtonStepsIndikator();
            lbKirimUsulan1.ForeColor = System.Drawing.Color.Green;
            lbKirimUsulan2.CssClass = "btn btn-success";
            //mvDokPendukung.SetActiveView(vUnggahDokPendukungIsi);
            //cekStatusEligibilitasKirimUsulan();
            lbSubmitUsulan.Visible = enableKirimUsulan;
            lblSubmitUsulan.Visible = !enableKirimUsulan;
        }

        protected void lbKirimUsulan_Click(object sender, EventArgs e)
        {

        }

        protected void lbLanjutkanAtPersyaratanUmum_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vCVKetua);
            cvKetua.isiCvKetua(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
        }

        protected void lbUnduhPdfDokLengkap_Click(object sender, EventArgs e)
        {
            pdfUsulanLengkap.UnduhProposalLengkap(ViewState["id_usulan_kegiatan"].ToString());
        }
        
        protected void lbSubmitUsulanModal_Click(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalKonfirmasiKirim");

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            lblJudulDiKirim.Text = objUsulanKegiatan.judul;
        }

        protected void lbSubmitUsulan_Click(object sender, EventArgs e)
        {
            //if (Session["kd_sts_eligibilitas_ketua"] != null)
            //{
            //    if (Session["kd_sts_eligibilitas_ketua"].ToString() == "1")
            //    {
                    if (objModelIdentitasUsulan.kirimUsulan(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                            "Kirim usulan berhasil.");
                        cekStatusKirimUsulan(ViewState["id_usulan_kegiatan"].ToString());
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                            "Silakan hubungi administrator.");
                    }
                //}
                //else
                //{
                //    string ErrorMsg = "Silakan hubungi administrator.";
                //    if (Session["info_sts_eligibilitas_ketua"] != null)
                //    {
                //        ErrorMsg = Session["info_sts_eligibilitas_ketua"].ToString();
                //    }
                //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                //        ErrorMsg);
                //}
            //}
            //else
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //        "Silakan hubungi administrator.");
            //}
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlThnPelaksanaanKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}