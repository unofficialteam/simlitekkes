using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.Helper;
using simlitekkes.Models.Pengusul;
using simlitekkes.Models.Reviewer;
using simlitekkes.UIControllers;
using simlitekkes.Models.PT;

namespace simlitekkes.UserControls.Reviewer
{
    public partial class evaluasiPembahasanPenelitian : System.Web.UI.UserControl
    {
        Models.login objLogin;
        evaluasiSubstansi objModel = new evaluasiSubstansi();
        identitasUsulan objMdlIdUsulan = new identitasUsulan();

        uiNotify noty = new uiNotify();
        uiModal modal = new uiModal();

        const string KODE_TAHAPAN_KEGIATAN = "25"; //Tahapan Pembahasan & Visitasi

        private Guid IdPenugasanReviewer
        {
            get
            {
                if (ViewState["IdPenugasanReviewer"] == null) ViewState["IdPenugasanReviewer"] = Guid.Empty;
                return Guid.Parse(ViewState["IdPenugasanReviewer"].ToString());
            }
            set
            {
                ViewState["IdPenugasanReviewer"] = value;
            }
        }

        private Guid IdPlottingReviewer
        {
            get
            {
                if (ViewState["IdPlottingReviewer"] == null) ViewState["IdPlottingReviewer"] = Guid.Empty;
                return Guid.Parse(ViewState["IdPlottingReviewer"].ToString());
            }
            set
            {
                ViewState["IdPlottingReviewer"] = value;
            }
        }

        private Guid IdUsulanKegiatan
        {
            get
            {
                if (ViewState["IdUsulanKegiatan"] == null) ViewState["IdUsulanKegiatan"] = Guid.Empty;
                return Guid.Parse(ViewState["IdUsulanKegiatan"].ToString());
            }
            set
            {
                ViewState["IdUsulanKegiatan"] = value;
            }
        }

        private Guid IdRabUsulan
        {
            get
            {
                if (ViewState["IdRabUsulan"] == null) ViewState["IdRabUsulan"] = Guid.Empty;
                return Guid.Parse(ViewState["IdRabUsulan"].ToString());
            }
            set
            {
                ViewState["IdRabUsulan"] = value;
            }
        }

        private bool IsPermanen
        {
            get
            {
                if (ViewState["IsPermanen"] == null) ViewState["IsPermanen"] = false;
                return bool.Parse(ViewState["IsPermanen"].ToString());
            }
            set
            {
                ViewState["IsPermanen"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiddlTahunUsulan();
                isiddlThnPelaksanaan();
                isilvDaftarPenugasan();
            }
        }

        protected void isiddlTahunUsulan()
        {
            ddlTahunUsulan.Items.Clear();
            for (var i = DateTime.Now.Year; i >= 2019; i--)
            {
                ddlTahunUsulan.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            if (ddlTahunUsulan.Items.Count > 0) ddlTahunUsulan.SelectedIndex = 0;
        }

        protected void isiddlThnPelaksanaan()
        {
            if (ddlTahunUsulan.SelectedIndex == -1) return;
            ddlThnPelaksanaan.Items.Clear();

            var thnTerpilih = int.Parse(ddlTahunUsulan.SelectedValue);
            for (var i = thnTerpilih + 1; i >= thnTerpilih; i--)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            if (ddlThnPelaksanaan.Items.Count > 0) ddlThnPelaksanaan.SelectedIndex = 0;
        }

        protected void isilvDaftarPenugasan()
        {
            var dt = new DataTable();
            if (!objModel.getListEvaluasiSubstansiLanjutan(ref dt, Guid.Parse(objLogin.idPersonal),
                    ddlTahunUsulan.SelectedItem.Text, ddlThnPelaksanaan.SelectedItem.Text, KODE_TAHAPAN_KEGIATAN))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            try
            {
                lvDaftarPenugasan.DataSource = dt;
                lvDaftarPenugasan.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void isilvDaftarUsulan()
        {
            var dt = new DataTable();
            if (!objModel.getListEvaluasiUsulanLanjutan(ref dt, IdPenugasanReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            try
            {
                lvDaftarUsulan.DataSource = dt;
                lvDaftarUsulan.DataBind();
                ViewState["daftarUsulan"] = dt;
                lblJmlUsulan.Text = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void isilvEvaluasiUsulan()
        {
            var dt = new DataTable();
            if (!objModel.getListPenilaianUsulanPenelitianlanjutan(ref dt, IdPlottingReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            try
            {
                lvEvaluasiUsulan.DataSource = dt;
                lvEvaluasiUsulan.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void isiDataEvaluasi()
        {
            lblJmlItem.Text = "0";
            lblJmlDievaluasi.Text = "0";
            lblTotalNilai.Text = "0";
            tbKota.Text = string.Empty;
            tbKomentar.Text = string.Empty;

            var dt = new DataTable();
            if (objModel.getEvaluasiUsulan(ref dt, IdPlottingReviewer) && dt.Rows.Count > 0)
            {
                lblJmlItem.Text = dt.Rows[0]["jml_komponen"].ToString();
                lblJmlDievaluasi.Text = dt.Rows[0]["jml_komponen_dinilai"].ToString();
                lblTotalNilai.Text = dt.Rows[0]["total_nilai"].ToString();
                tbKota.Text = dt.Rows[0]["tempat"].ToString();
                tbKomentar.Text = dt.Rows[0]["komentar"].ToString();
            }
        }

        protected bool SimpanKomentar()
        {
            if (tbKomentar.Text.Trim().Length == 0 || tbKota.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                            "Data Kota dan Komentar harus diisi terlebih dahulu !");
                return false;
            }

            for (int i = 0; i < lvEvaluasiUsulan.Items.Count; i++)
            {
                if (lvEvaluasiUsulan.Items[i].FindControl("rblOpsi") is RadioButtonList rblOpsi)
                {
                    if (!string.IsNullOrEmpty(rblOpsi.SelectedValue))
                    {
                        var idOpsi = int.Parse(rblOpsi.SelectedValue);
                        var idKomponen = int.Parse(lvEvaluasiUsulan.DataKeys[i]["id_komponen_penilaian"].ToString());

                        if (!objModel.insupPenilaianReviewer(IdPlottingReviewer, idKomponen, idOpsi,"0"))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                            return false;
                        }
                    }
                }
            }

            if (!objModel.insupHasilReview(IdPlottingReviewer, tbKota.Text, tbKomentar.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return false;
            }

            return true;
        }

        protected void SetModePermanen()
        {
            lbSimpanPermanen.Enabled = !IsPermanen;
            lbSimpanKomentar.Visible = !IsPermanen;

            tbKomentar.Enabled = !IsPermanen;
            tbKota.Enabled = !IsPermanen;
            lblStatus.Text = IsPermanen ? "PERMANEN" : "TERBUKA";
            lblStatus.CssClass = IsPermanen ? "label label-md label-danger" : "label label-md label-success";
        }

        public string FormatUang(string nominal)
        {
            var cultureInfo = new System.Globalization.CultureInfo("id-ID");
            var nominalDecimal = Convert.ToDecimal(nominal);

            return nominalDecimal.ToString("N0", cultureInfo);
        }

        protected void lvDaftarPenugasan_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var drv = e.Item.DataItem as DataRowView;

                if (drv["kd_sts_permanen"].ToString() == "1")
                {
                    var lblStatus = e.Item.FindControl("lblStatus") as Label;
                    lblStatus.CssClass = " label label-md label-danger";
                }
            }
        }

        protected void lvDaftarPenugasan_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            lblTahunUsulan.Text = ddlTahunUsulan.SelectedItem.Text;
            lblThnPelaksanaan.Text = ddlThnPelaksanaan.SelectedItem.Text;

            var kdStatusPermanen = lvDaftarPenugasan.DataKeys[e.ItemIndex]["kd_sts_permanen"].ToString();
            IsPermanen = (kdStatusPermanen == "1") ? true : false;

            lblNamaSkema.Text = lvDaftarPenugasan.DataKeys[e.ItemIndex]["nama_skema"].ToString();
            IdPenugasanReviewer = Guid.Parse(lvDaftarPenugasan.DataKeys[e.ItemIndex]["id_penugasan_reviewer"].ToString());
            string kd_program_hibah = lvDaftarPenugasan.DataKeys[e.ItemIndex]["kd_program_hibah"].ToString();
            ViewState["kd_program_hibah"] = kd_program_hibah;
            if (kd_program_hibah != "1")
            {
                lbBidangUnggulan.Text = "Bidang Fokus RIRN";
            }
            else
            {
                lbBidangUnggulan.Text = "Bidang-Topik Unggulan PT";
            }
            SetModePermanen();
            isilvDaftarUsulan();
            mvEvaluasi.SetActiveView(vDaftarUsulan);
        }

        protected void lvDaftarUsulan_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            IdPlottingReviewer = Guid.Parse(lvDaftarUsulan.DataKeys[e.ItemIndex]["id_plotting_reviewer"].ToString());
            IdUsulanKegiatan = Guid.Parse(lvDaftarUsulan.DataKeys[e.ItemIndex]["id_usulan_kegiatan"].ToString());
            lblJudul.Text = lvDaftarUsulan.DataKeys[e.ItemIndex]["judul"].ToString();

            isiDataEvaluasi();
            isilvEvaluasiUsulan();

            mvEvaluasi.SetActiveView(vEvaluasi);
        }

        protected void lvDaftarUsulan_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "UnduhPdf")
            {
                var itemIndex = int.Parse(e.CommandArgument.ToString());
                var id_usulan_kegiatan = lvDaftarUsulan.DataKeys[itemIndex]["id_usulan_kegiatan"].ToString();

                pdfUsulanLengkap.UnduhProposalLengkap(id_usulan_kegiatan);

            }
        }

        protected void lbKembaliKeDaftarUsulan_Click(object sender, EventArgs e)
        {
            isilvDaftarUsulan();
            mvEvaluasi.SetActiveView(vDaftarUsulan);
        }

        protected void lbKembaliKePenugasan_Click(object sender, EventArgs e)
        {
            isilvDaftarPenugasan();
            mvEvaluasi.SetActiveView(vRekapEvaluasi);
        }

        protected void lvEvaluasiUsulan_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                if (e.Item.FindControl("rblOpsi") is RadioButtonList rblOpsi)
                {
                    DataRowView drv = e.Item.DataItem as DataRowView;
                    var idOpsi = drv["id_opsi_komponen_penilaian_arr"].ToString().Split('|');
                    var opsi = drv["opsi_komponen_penilaian_arr"].ToString().Split('|');
                    for (int i = 0; i < idOpsi.Length; i++)
                    {
                        rblOpsi.Items.Add(new ListItem
                        {
                            Value = idOpsi[i],
                            Text = opsi[i]
                        });
                    }

                    var selectedIdOpsi = drv["id_opsi_komponen_penilaian"].ToString();
                    if (!string.IsNullOrEmpty(selectedIdOpsi))
                    {
                        if (rblOpsi.Items.FindByValue(selectedIdOpsi) != null) rblOpsi.SelectedValue = selectedIdOpsi;
                    }
                }
            }
        }

        protected void lbSimpanKomentar_Click(object sender, EventArgs e)
        {
            if (!SimpanKomentar()) return;

            isilvDaftarUsulan();
            mvEvaluasi.SetActiveView(vDaftarUsulan);
        }

        protected void lbSimpanPermanen_Click(object sender, EventArgs e)
        {
            if (!objModel.setSimpanPermanenUsulanLanjutan(IdPenugasanReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            IsPermanen = true;
            SetModePermanen();
        }

        protected void lbUnduhPDFRekamJejak_Click(object sender, EventArgs e)
        {
            ktRekamJejak.unduhRekamJejakKetuaNAnggota(IdUsulanKegiatan.ToString());
        }

        protected void lbUnduhPDFUsulan_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            objMdlIdUsulan.getDetailUsulanKegiatan(ref dt, IdUsulanKegiatan.ToString());
            string dirFile = "~/fileUpload20/dokumenUsulan/" + dt.Rows[0]["thn_usulan_kegiatan"].ToString();
            string path2save = String.Format(dirFile + "/{0}.pdf", IdUsulanKegiatan.ToString());
            if (File.Exists(Server.MapPath(path2save)))
            {
                string namaBerkas = String.Format("DokumenUsulan_{0}.pdf", dt.Rows[0]["nama_ketua"].ToString());
                var atributUnduh = new AtributUnduh
                {
                    FolderUnduh = dirFile,
                    NamaBerkas = IdUsulanKegiatan.ToString() + ".pdf",
                    NamaBerkasdiUnduh = namaBerkas
                };
                Session["AtributUnduh"] = atributUnduh;
                var unduhForm = "helper/unduhFile.aspx";
                Response.Redirect(unduhForm);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error,
                    "Terjadi Kesalahan", "Berkas tidak ditemukan.");
            }

            //pdfUsulanLengkap.UnduhProposalLengkap(IdUsulanKegiatan.ToString());
        }

        protected void lbUnduhHasilPenilaian_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["daftarUsulan"];
            ktPdfHasilPenilaian.InitPdf(dt, objLogin.idPersonal);
        }

        protected void lbBidangUnggulan_Click(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalBidangTopikUnggulan");
            DataTable dt = new DataTable();
            gvBidangTopikUnggulan.Visible = false;
            panelRirn.Visible = false;
            if (ViewState["kd_program_hibah"].ToString() == "1")
            {
                simlitekkes.Models.PT.OperatorPT opt = new simlitekkes.Models.PT.OperatorPT();
                opt.getBidangTopikUnggulanPT(ref dt, IdUsulanKegiatan.ToString());
                gvBidangTopikUnggulan.DataSource = dt;
                gvBidangTopikUnggulan.DataBind();
                gvBidangTopikUnggulan.Visible = true;
                lblJudulBidangFokus.Text = "Bidang-Topik Unggulan PT";
            }
            else
            {
                simlitekkes.Models.Pengusul.identitasUsulan idU = new simlitekkes.Models.Pengusul.identitasUsulan();
                idU.getDataDataUsulanKegiatan(ref dt, IdUsulanKegiatan.ToString());
                DataTable dtBidFokus = new DataTable();
                if (dt.Rows.Count > 0)
                {
                    idU.getBidangFokus(ref dtBidFokus, int.Parse(dt.Rows[0]["id_skema"].ToString()),
                        int.Parse(dt.Rows[0]["level_tkt_target"].ToString()),
                        int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString())
                        );

                    gvBidangFokusRirn.DataSource = dt;
                    gvBidangFokusRirn.DataBind();
                    panelRirn.Visible = true;

                    DataTable dt2 = new DataTable();
                    identitasUsulan modelIdentitas = new identitasUsulan();
                    modelIdentitas.getIdentitasUsulan(ref dt2, Guid.Parse(dt.Rows[0]["id_usulan"].ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        lblTema.Text = dt2.Rows[0]["tema"].ToString();
                        lblTopik.Text = dt2.Rows[0]["topik"].ToString();
                        if (lblTema.Text.Trim() == "") lblTema.Text = "-";
                        if (lblTopik.Text.Trim() == "") lblTopik.Text = "-";
                    }
                    lblJudulBidangFokus.Text = "Daftar Bidang Fokus RIRN";
                }
            }
        }

        protected void ddlTahunUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlThnPelaksanaan();
            isilvDaftarPenugasan();
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isilvDaftarPenugasan();
        }
    }
}