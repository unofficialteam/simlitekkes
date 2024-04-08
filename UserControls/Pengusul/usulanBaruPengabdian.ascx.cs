using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
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
    public partial class usulanBaruPengabdian : System.Web.UI.UserControl
    {
        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        UIControllers.uiDropdownList obj_uiDDL = new UIControllers.uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        Models.Sistem.referensiData objRefData = new Models.Sistem.referensiData();
        Models.login objLogin;
        kontrolUnggah ktUnggah = new kontrolUnggah();
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        uiNotify noty = new uiNotify();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                init();
            }

            lstUsulanBaru.OnChildEventOccurs += new EventHandler(Child1_OnChildEventOccurs);
            lstUsulanBaru.OnChildEventDelete += new EventHandler(Child1_OnChildEventDelete);
            lstUsulanBaru.OnChildEventUnduhProposalLengkap += new EventHandler(Child1_OnChildEventUnduhProposalLengkap);

            ktAnggota.OnChildEventOccurs += new EventHandler(Anggota_OnChildEventOccurs);
            ktAnggota.OnChildBatalEventOccurs += new EventHandler(AnggotaBatal_OnChildEventOccurs);

            mitraAbdimas.OnChildEventMitra += new EventHandler(MitraChildEvent);
            mitraAbdimas.OnChildEventMitraEnd += new EventHandler(AnggotaBatal_OnChildEventOccurs);
            //public event EventHandler OnChildEventMitra;
            //public event EventHandler OnChildEventMitraEnd;


        }

        private void init()
        {
            isiDdlThnUsulan();
            isiDdlThnPelaksanaan();
            //ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            //initUsulan(ViewState["id_usulan_kegiatan"].ToString());
            mvMain.SetActiveView(vPersyaratan);
            int totalSyarat = persyaratan.isiDataPersyaratanUmum(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            lstUsulanBaru.isiListUsulanBaru(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);

            objLogin = (Models.login)Session["objLogin"];
            //if (objLogin.nidn == "9999901122") // Atong
            //    totalSyarat = 6;

            //bool isEligible = (totalSyarat >= 5);
            bool isEligible = (totalSyarat >= 4);
            lblPengajuanBaru.Visible = !isEligible;
            lbPengajuanBaru.Visible = isEligible;
        }

        void Child1_OnChildEventUnduhProposalLengkap(object sender, EventArgs e)
        {
            pdfUsulanLengkap.UnduhProposalLengkap(lstUsulanBaru.getIdUsulanKegiatan());
        }

        void Child1_OnChildEventDelete(object sender, EventArgs e)
        {
            persyaratan.isiDataPersyaratanUmum(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            int totalSyarat = persyaratan.isiDataPersyaratanUmum(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            //int totalSyarat = 5;
            lstUsulanBaru.isiListUsulanBaru(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);

            //bool isEligible = (totalSyarat >= 5);
            bool isEligible = (totalSyarat >= 4);
            lblPengajuanBaru.Visible = !isEligible;
            lbPengajuanBaru.Visible = isEligible;
        }

        void Child1_OnChildEventOccurs(object sender, EventArgs e)
        {
            ////ViewState["id_usulan_kegiatan"] = lstUsulanBaru.getIdUsulanKegiatan();
            ////initUsulan(lstUsulanBaru.getIdUsulanKegiatan());

            //mvMain.SetActiveView(vUsulan);
            //// start steps 1 Identitas
            //clearAllButtonStepsIndikator();
            //lbIdentitas1.ForeColor = System.Drawing.Color.Green;
            //lbIdentitas2.CssClass = "btn btn-success";
            //mvUsulan.SetActiveView(vIdentitas);
            //mvIdentitas.SetActiveView(vIDUsulan);

            //Guid idUsulanKegiatan = Guid.Parse(lstUsulanBaru.getIdUsulanKegiatan());
            //IdentitasUsulanAbdimas.InitIdentitasUsulan(idUsulanKegiatan);

            ViewState["id_usulan_kegiatan"] = lstUsulanBaru.getIdUsulanKegiatan();
            initUsulan(lstUsulanBaru.getIdUsulanKegiatan());

            mvMain.SetActiveView(vUsulan);
            // start steps 1 Identitas
            Session["isEdit"] = true;
            clearAllButtonStepsIndikator();
            lbIdentitas1.ForeColor = System.Drawing.Color.Green;
            lbIdentitas2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vIdentitas);
            mvIdentitas.SetActiveView(vIDUsulan);

            IdentitasUsulanAbdimas.InitIdentitasUsulan(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            cekStatusKirimUsulan(ViewState["id_usulan_kegiatan"].ToString());

        }

        void Anggota_OnChildEventOccurs(object sender, EventArgs e)
        {
            bool show = false;
            if (ViewState["show_lanjutkan_at_anggota"] != null)
                show = (bool)ViewState["show_lanjutkan_at_anggota"];
            lbLanjutkanAtAnggota.Visible = show;
            ViewState.Remove("show_lanjutkan_at_anggota");
        }

        void AnggotaBatal_OnChildEventOccurs(object sender, EventArgs e)
        {
            lbLanjutkanAtAnggota.Visible = true;
        }
        void MitraChildEvent(object sender, EventArgs e)
        {
            lbLanjutkanAtAnggota.Visible = false;
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
        void ChildPersyaratan_OnOccurs(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vCVKetua);
            //cvKetua.isiCvKetua();
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
                //tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString()), // test
                //idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString()),
                idBidangFokus = int.Parse(dt.Rows[0]["id_bidang_fokus"].ToString())
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

        protected void lbLanjutkanAtPersyaratanUmum_Click(object sender, EventArgs e)
        {
            ViewState["isEdit"] = false;
            mvMain.SetActiveView(vCVKetua);
            cvKetuaAbdimas.isiCvKetua(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
        }

        protected void lbLanjutkanAtCVKetua_Click(object sender, EventArgs e)
        {
            //mvMain.SetActiveView(vUsulan);
            //// start steps 1 Identitas
            //clearAllButtonStepsIndikator();
            //lbIdentitas1.ForeColor = System.Drawing.Color.Green;
            //lbIdentitas2.CssClass = "btn btn-success";
            //mvUsulan.SetActiveView(vIdentitas);
            //mvIdentitas.SetActiveView(vIDUsulan);

            ////if (bool.Parse(Session["isEdit"].ToString()))
            ////{
            ////    initUsulan(IdentitasUsulan.IdUsulanKegiatan.ToString());
            ////}
            ////else
            ////{
            ////    IdentitasUsulan.InitIdentitasUsulan();
            ////}

            //IdentitasUsulanAbdimas.InitIdentitasUsulan();


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
                    initUsulan(IdentitasUsulanAbdimas.IdUsulanKegiatan.ToString());
                    cekStatusKirimUsulan(IdentitasUsulanAbdimas.IdUsulanKegiatan.ToString());
                }
                else
                {
                    IdentitasUsulanAbdimas.InitIdentitasUsulan(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
                }
            }
            else
            {
                Session["isEdit"] = false;
                IdentitasUsulanAbdimas.InitIdentitasUsulan(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            }

            //IdentitasUsulanAbdimas.InitIdentitasUsulan();

        }

        protected void lbLanjutkanAtIDUsulan_Click(object sender, EventArgs e)
        {
            if (!IdentitasUsulanAbdimas.Simpan()) return;

            mvIdentitas.SetActiveView(vCVAnggota);

            initUsulan(IdentitasUsulanAbdimas.IdUsulanKegiatan.ToString());
            var objUsulanKegiatan = ViewState["usulan_kegiatan"] as usulanKegiatan;
            DataTable dt = new DataTable();
            dt = ktAnggota.isiAnggotaDikti(IdentitasUsulanAbdimas.IdUsulanKegiatan.ToString(),
                 objUsulanKegiatan.idSkema);
            ktAnggota.isiAnggotaTendik(IdentitasUsulanAbdimas.IdUsulanKegiatan.ToString(),
                objUsulanKegiatan.idSkema);
            ktAnggota.isiAnggotaNonDikti();
            ktAnggota.isiDurasiUsulan();
        }

        protected void lbLanjutkanAtAnggota_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Identitas usulan harap disimpan terlebih dahulu.");
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
            ktDokUsulan.setJenisKegiatan("pengabdian kepada masyarakat");
        }

        protected void lbLanjutkanAtUnggahDokUsulan_Click(object sender, EventArgs e)
        {
            mvSubstansi.SetActiveView(vLuaran);
            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            //string pidUsulan, int plamakegiatan,int urutan_thn,string pidUsulankegiatan,string pnamaSkema
            ktLuaran.setDataLuaranpengabdian(objUsulanKegiatan);
        }

        protected void lbLanjutkanAtUnggahDokLuaran_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Identitas usulan harap disimpan terlebih dahulu.");
                return;
            }
            Session.Remove("ktUnggah");
            // start steps 3 RAB

            rab.InitRAB((usulanKegiatan)ViewState["usulan_kegiatan"], "2");

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
                    "Identitas usulan harap disimpan terlebih dahulu.");
                return;
            }
            // start steps 4 Unggah dokumen pendukung
            //if (!rab.Simpan()) return;

            mitraAbdimas.InitMitra((usulanKegiatan)ViewState["usulan_kegiatan"]);
            //mitraAbdimas.isiMitraPengabdianPerSkema();

            clearAllButtonStepsIndikator();
            lbDokPendukung1.ForeColor = System.Drawing.Color.Green;
            lbDokPendukung2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vUnggahDokPendukung);
            mvDokPendukung.SetActiveView(vUnggahDokPendukungIsi);
            cekStatusEligibilitasKirimUsulan();

        }

        private void cekStatusEligibilitasKirimUsulan()
        {
            //lblKirimUsulan.Visible = false;
            //lbKirimUsulan.Visible = true;
        }

        protected void lbLanjutkanAtDataPendukung_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Identitas usulan harap disimpan terlebih dahulu.");
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
            if (objModelIdentitasUsulan.kirimUsulan(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                    "Kirim usulan berhasil.");
                cekStatusKirimUsulan(ViewState["id_usulan_kegiatan"].ToString());
                CleanSession();
                init();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Silakan hubungi administrator.");
            }
        }

        private void CleanSession()
        {
            if (Session["usulan_kegiatan"] != null)
                Session.Remove("usulan_kegiatan");
            if (Session["ktUnggah"] != null)
                Session.Remove("ktUnggah");
            if (Session["id_usulan_kegiatan"] != null)
                Session.Remove("id_usulan_kegiatan");
            if (Session["isEdit"] != null)
                Session.Remove("isEdit");
            if (Session["idPersonalAnggota"] != null)
                Session.Remove("idPersonalAnggota");
            if (Session["urutanAnggota"] != null)
                Session.Remove("urutanAnggota");
            if (Session["AtributUnduh"] != null)
                Session.Remove("AtributUnduh");

        }
        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vPersyaratan);
            int totalSyarat = persyaratan.isiDataPersyaratanUmum(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            //string thnUsulan = (int.Parse( ddlThnPelaksanaanKegiatan.SelectedValue) - 1).ToString();
            lstUsulanBaru.isiListUsulanBaru(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            lbPengajuanBaru.Visible = false;
            lblPengajuanBaru.Visible = false;
            //if (totalSyarat >= 5)
            if (totalSyarat >= 4)
                lbPengajuanBaru.Visible = true;
            else
                lblPengajuanBaru.Visible = true;
        }

        protected void ddlThnPelaksanaanKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vPersyaratan);
            int totalSyarat = persyaratan.isiDataPersyaratanUmum(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            //string thnUsulan = (int.Parse( ddlThnPelaksanaanKegiatan.SelectedValue) - 1).ToString();
            lstUsulanBaru.isiListUsulanBaru(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            lbPengajuanBaru.Visible = false;
            lblPengajuanBaru.Visible = false;
            //if (totalSyarat >= 5
            if (totalSyarat >= 4)
                    lbPengajuanBaru.Visible = true;
            else
                lblPengajuanBaru.Visible = true;
        }

        private void isiDdlThnUsulan()
        {
            int thnSkg = int.Parse(DateTime.Now.Year.ToString());
            ddlThnUsulan.Items.Clear();
            int maxThn = thnSkg + 1;
            int minThn = 2018;
            //ddlThnPelaksanaanKegiatan.Items.Add(new ListItem("-- Pilih --",""));
            for (int a = maxThn; a >= minThn; a--)
            {
                ddlThnUsulan.Items.Add(new ListItem(a.ToString(), a.ToString()));
            }
            ddlThnUsulan.Items.FindByValue(thnSkg.ToString()).Selected = true;
        }
        private void isiDdlThnPelaksanaan()
        {
            int thnSkg = int.Parse(DateTime.Now.Year.ToString());
            ddlThnPelaksanaanKegiatan.Items.Clear();
            int maxThn = thnSkg + 1;
            int minThn = 2019;
            //ddlThnPelaksanaanKegiatan.Items.Add(new ListItem("-- Pilih --",""));
            for (int a = maxThn; a >= minThn; a--)
            {
                ddlThnPelaksanaanKegiatan.Items.Add(new ListItem(a.ToString(), a.ToString()));
            }
            ddlThnPelaksanaanKegiatan.Items.FindByValue(maxThn.ToString()).Selected = true;
        }
    }
}
