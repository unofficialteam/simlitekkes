using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using simlitekkes.Helper;
using simlitekkes.Models.Pengusul;
//using simlitekkes.Models.Reviewer;
using simlitekkes.UIControllers;

namespace simlitekkes.UserControls.Reviewer
{
    public partial class evaluasiSubstansiPengabdian : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Reviewer.evaluasiSubstansiPengabdian objModel = new Models.Reviewer.evaluasiSubstansiPengabdian();
        identitasUsulan objMdlIdUsulan = new identitasUsulan();

        uiNotify noty = new uiNotify();
        uiModal modal = new uiModal();

        const string KODE_TAHAPAN_KEGIATAN = "22"; //Tahapan Evaluasi Dokumen

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

        private Guid IdRabItemBelanja
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
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiDdlThnUsulan();
                isiDdlThnPelaksanaan();
                isilvDaftarPenugasan();
            }
        }

        #region Rekap Penugasan

        private void isiDdlThnUsulan()
        {
            ddlTahunUsulan.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlTahunUsulan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlTahunUsulan.SelectedIndex = 0;
        }

        private void isiDdlThnPelaksanaan()
        {
            string thnUsulan = ddlTahunUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 1; i >= 0; i--)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
            ddlThnPelaksanaan.SelectedIndex = 0;
        }

        protected void ddlTahunUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isilvDaftarPenugasan();
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isilvDaftarPenugasan();
        }

        protected void isilvDaftarPenugasan()
        {
            var dt = new DataTable();
            if (!objModel.getListEvaluasiSubstansi(ref dt, Guid.Parse(objLogin.idPersonal),
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

        protected void lvDaftarPenugasan_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            lblTahunUsulan.Text = ddlTahunUsulan.SelectedItem.Text;
            lblThnPelaksanaan.Text = ddlThnPelaksanaan.SelectedItem.Text;

            var kdStatusPermanen = lvDaftarPenugasan.DataKeys[e.ItemIndex]["kd_sts_permanen"].ToString();
            IsPermanen = (kdStatusPermanen == "1") ? true : false;

            lblNamaSkema.Text = lvDaftarPenugasan.DataKeys[e.ItemIndex]["nama_skema"].ToString();
            IdPenugasanReviewer = Guid.Parse(lvDaftarPenugasan.DataKeys[e.ItemIndex]["id_penugasan_reviewer"].ToString());

            SetModePermanen();
            isilvDaftarUsulan();
            mvEvaluasi.SetActiveView(vDaftarUsulan);
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

        #endregion

        #region Daftar Usulan

        protected void SetModePermanen()
        {
            lbSimpanPermanen.Enabled = !IsPermanen;

            lbSimpanRekamJejak.Visible = !IsPermanen;
            lbSimpanEvaluasiUsulan.Visible = !IsPermanen;
            lbSimpanRAB.Visible = !IsPermanen;
            lbSimpanKomentar.Visible = !IsPermanen;

            tbKomentar.Enabled = !IsPermanen;
            tbKota.Enabled = !IsPermanen;
            lblStatus.Text = IsPermanen ? "PERMANEN" : "TERBUKA";
            lblStatus.CssClass = IsPermanen ? "label label-md label-danger" : "label label-md label-success";
        }

        protected void isilvDaftarUsulan()
        {
            var dt = new DataTable();
            if (!objModel.getListEvaluasiUsulan(ref dt, IdPenugasanReviewer))
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

        protected void lvDaftarUsulan_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            IdPlottingReviewer = Guid.Parse(lvDaftarUsulan.DataKeys[e.ItemIndex]["id_plotting_reviewer"].ToString());
            IdUsulanKegiatan = Guid.Parse(lvDaftarUsulan.DataKeys[e.ItemIndex]["id_usulan_kegiatan"].ToString());
            lblJudul.Text = lvDaftarUsulan.DataKeys[e.ItemIndex]["judul"].ToString();

            isiDataEvaluasi();
            isilvEvaluasiRekamJejak();
            isilvEvaluasiUsulan();

            rblUrutanTahunRAB.SelectedValue = "1";
            if (lvRekomendasiRAB.EditIndex != -1) lvRekomendasiRAB.EditIndex = -1;
            isilvRekomendasiRAB();

            mvEvaluasi.SetActiveView(vEvaluasi);
            mvPenilaian.SetActiveView(vRekamJejak);
            rblEvaluasi.SelectedValue = "1";
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

        protected void lbKembaliKePenugasan_Click(object sender, EventArgs e)
        {
            isilvDaftarPenugasan();
            mvEvaluasi.SetActiveView(vRekapEvaluasi);
        }

        protected void lbSimpanPermanen_Click(object sender, EventArgs e)
        {
            //foreach (ListViewItem lvdi in lvDaftarUsulan.Items)
            //{
            //    if (lvDaftarUsulan.DataKeys[lvdi.DisplayIndex][3].ToString() != lvDaftarUsulan.DataKeys[lvdi.DisplayIndex][4].ToString())
            //    {
            //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //           "Skor Kriteria Penilaian harus diisi semua !");
            //        return;
            //    }
            //}

            var dtJmlUsulan = new DataTable();
            if (!objModel.getListEvaluasiUsulan(ref dtJmlUsulan, IdPenugasanReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }
            int jmlUsulan = dtJmlUsulan.Rows.Count;

            var dtJmlDinilai = new DataTable();
            if (!objModel.getListEvaluasiUsulanBolehPermanen(ref dtJmlDinilai, IdPenugasanReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }
            int jmlDinilai = int.Parse(dtJmlDinilai.Rows[0]["jml_boleh_permanen"].ToString());

            if (jmlUsulan != jmlDinilai)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       "Komponen Penilaian, tempat menilai, dan komentar harus diisi semua !");
                return;
            }

            if (!objModel.setSimpanPermanen(IdPenugasanReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }
            noty.Notify(this.Page, uiNotify.NotifyType.success, "Infomasi", "Simpan data berhasil");
            IsPermanen = true;
            SetModePermanen();

        }

        protected void lbUnduhHasilPenilaian_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["daftarUsulan"];
            ktPdfHasilPenilaian.InitPdf(dt, objLogin.idPersonal);
        }

        #endregion

        #region Penilaian

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

        protected void isilvEvaluasiRekamJejak()
        {
            var dt = new DataTable();
            if (!objModel.getListPenilaianRekamJejak(ref dt, IdPlottingReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            try
            {
                lvEvaluasiRekamJejak.DataSource = dt;
                lvEvaluasiRekamJejak.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void isilvEvaluasiUsulan()
        {
            var dt = new DataTable();
            if (!objModel.getListPenilaianUsulanPengabdian(ref dt, IdPlottingReviewer))
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

        protected void isilvRekomendasiRAB()
        {
            var dt = new DataTable();
            if (!objModel.getListRABRekomendasi(ref dt, IdPlottingReviewer,
                    int.Parse(rblUrutanTahunRAB.SelectedValue)))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            try
            {
                lvRekomendasiRAB.DataSource = dt;
                lvRekomendasiRAB.DataBind();

                if (dt.Rows.Count > 0)
                {
                    if (lvRekomendasiRAB.FindControl("lblTotalBiaya") is Label lblTotalBiaya)
                    {
                        var totalBiaya = Convert.ToDecimal(dt.Compute("SUM(total_biaya)", string.Empty));
                        lblTotalBiaya.Text = totalBiaya.ToString("N0");
                    }

                    if (lvRekomendasiRAB.FindControl("lblTotalJustifikasi") is Label lblTotalJustifikasi)
                    {
                        var totalJustifikasi = Convert.ToDecimal(dt.Compute("SUM(total_justifikasi)", string.Empty));
                        lblTotalJustifikasi.Text = totalJustifikasi.ToString("N0");
                    }
                }
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        public string FormatUang(string nominal)
        {
            var cultureInfo = new System.Globalization.CultureInfo("id-ID");
            var nominalDecimal = Convert.ToDecimal(nominal);

            return nominalDecimal.ToString("N0", cultureInfo);
        }

        protected void lbKembaliKeDaftarUsulan_Click(object sender, EventArgs e)
        {
            isilvDaftarUsulan();
            mvEvaluasi.SetActiveView(vDaftarUsulan);
        }

        protected void rblEvaluasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblEvaluasi.SelectedValue)
            {
                case "1":
                    mvPenilaian.SetActiveView(vRekamJejak);
                    break;
                case "2":
                    mvPenilaian.SetActiveView(vUsulanPenelitian);
                    break;
                case "3":
                    mvPenilaian.SetActiveView(vUsulanRAB);
                    break;
                case "4":
                    mvPenilaian.SetActiveView(vKomentar);
                    break;
            }
        }

        protected void lbUnduhPDFRekamJejak_Click(object sender, EventArgs e)
        {
            ktRekamJejak.unduhRekamJejakKetuaNAnggota(IdUsulanKegiatan.ToString());
        }

        protected void lvEvaluasiRekamJejak_ItemDataBound(object sender, ListViewItemEventArgs e)
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

                if (IsPermanen)
                {
                    if (e.Item.FindControl("lbSimpanRekamJejak") is LinkButton lbSimpanRekamJejak)
                    {
                        lbSimpanRekamJejak.Visible = !IsPermanen;
                    }
                }
            }
        }

        protected void lbSimpanRekamJejak_Click(object sender, EventArgs e)
        {
            if (SimpanPenilaianRekamJejak())
            {
                isiDataEvaluasi();
                isilvEvaluasiUsulan();
                rblEvaluasi.SelectedValue = "2";
                mvPenilaian.SetActiveView(vUsulanPenelitian);
            }
        }

        protected bool SimpanPenilaianRekamJejak()
        {
            for (int i = 0; i < lvEvaluasiRekamJejak.Items.Count; i++)
            {
                if (lvEvaluasiRekamJejak.Items[i].FindControl("rblOpsi") is RadioButtonList rblOpsi)
                {
                    if (!string.IsNullOrEmpty(rblOpsi.SelectedValue))
                    {
                        var idOpsi = int.Parse(rblOpsi.SelectedValue);
                        var idKomponen = int.Parse(lvEvaluasiRekamJejak.DataKeys[i]["id_komponen_penilaian"].ToString());

                        if (!objModel.insupPenilaianReviewer(IdPlottingReviewer, idKomponen, idOpsi, "0"))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        protected void lbUnduhPDFUsulan_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            objMdlIdUsulan.getDetailUsulanKegiatan(ref dt, IdUsulanKegiatan.ToString());
            string dirFile = "~/fileUpload/dokumenUsulan/" + dt.Rows[0]["thn_usulan_kegiatan"].ToString();
            string dirFile2 = "~/fileUpload20/dokumenUsulan/" + dt.Rows[0]["thn_usulan_kegiatan"].ToString();
            string path2save = String.Format(dirFile + "/{0}.pdf", IdUsulanKegiatan.ToString());
            string path2save2 = String.Format(dirFile2 + "/{0}.pdf", IdUsulanKegiatan.ToString());
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
                if (File.Exists(Server.MapPath(path2save2)))
                {
                    string namaBerkas = String.Format("DokumenUsulan_{0}.pdf", dt.Rows[0]["nama_ketua"].ToString());
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = dirFile2,
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
            }
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

                if (IsPermanen)
                {
                    if (e.Item.FindControl("lbSimpanEvaluasiUsulan") is LinkButton lbSimpanEvaluasiUsulan)
                    {
                        lbSimpanRekamJejak.Visible = !IsPermanen;
                    }
                }
            }
        }

        protected void lbSimpanEvaluasiUsulan_Click(object sender, EventArgs e)
        {
            if (SimpanPenilaianUsulan())
            {
                isiDataEvaluasi();
                rblEvaluasi.SelectedValue = "3";
                rblUrutanTahunRAB.SelectedValue = "1";
                isilvRekomendasiRAB();
                mvPenilaian.SetActiveView(vUsulanRAB);
            }
        }

        protected bool SimpanPenilaianUsulan()
        {
            for (int i = 0; i < lvEvaluasiUsulan.Items.Count; i++)
            {
                if (lvEvaluasiUsulan.Items[i].FindControl("rblOpsi") is RadioButtonList rblOpsi)
                {
                    if (!string.IsNullOrEmpty(rblOpsi.SelectedValue))
                    {
                        var idOpsi = int.Parse(rblOpsi.SelectedValue);
                        var idKomponen = int.Parse(lvEvaluasiUsulan.DataKeys[i]["id_komponen_penilaian"].ToString());
                        var urutanThnPelaksanaan = lvEvaluasiUsulan.DataKeys[i]["urutan_thn_pelaksanaan"].ToString();

                        if (!objModel.insupPenilaianReviewer(IdPlottingReviewer, idKomponen, idOpsi, urutanThnPelaksanaan))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        protected void rblUrutanTahunRAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            isilvRekomendasiRAB();
        }

        protected void lvRekomendasiRAB_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvRekomendasiRAB.EditIndex = e.NewEditIndex;
            isilvRekomendasiRAB();
        }

        protected void lvRekomendasiRAB_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            IdRabItemBelanja = Guid.Parse(lvRekomendasiRAB.DataKeys[e.ItemIndex]["id_rab_item_belanja"].ToString());
            var tbVolume = lvRekomendasiRAB.Items[e.ItemIndex].FindControl("tbVolume") as TextBox;
            var tbHargaSatuan = lvRekomendasiRAB.Items[e.ItemIndex].FindControl("tbHargaSatuan") as TextBox;
            var tbKomentar = lvRekomendasiRAB.Items[e.ItemIndex].FindControl("tbKomentar") as TextBox;

            if (tbVolume.Text.Trim().Length == 0 || tbHargaSatuan.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Data Volume dan Harga Satuan wajib diisi !");
                return;
            }

            if (!objModel.insupRABRekomendasi(IdRabItemBelanja, IdPlottingReviewer,
                    decimal.Parse(tbVolume.Text), string.Empty, decimal.Parse(tbHargaSatuan.Text.Replace(".", "")),
                    tbKomentar.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            lvRekomendasiRAB.EditIndex = -1;
            isilvRekomendasiRAB();
        }

        protected void lvRekomendasiRAB_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvRekomendasiRAB.EditIndex = -1;
            isilvRekomendasiRAB();
        }

        protected void lvRekomendasiRAB_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var drv = e.Item.DataItem as DataRowView;
                if (drv["kd_sts_rekomendasi"].ToString() == "1")
                {
                    if (e.Item.FindControl("lblTotalJustifikasiItem") is Label lblTotalJustifikasiItem)
                    {
                        lblTotalJustifikasiItem.Font.Bold = true;
                        lblTotalJustifikasiItem.ForeColor = System.Drawing.Color.Red;
                    }
                }

                if (IsPermanen)
                {
                    if (e.Item.FindControl("lbJustifikasi") is LinkButton lbJustifikasi)
                    {
                        lbJustifikasi.Visible = !IsPermanen;
                    }
                }
            }
        }

        protected void lbSimpanRAB_Click(object sender, EventArgs e)
        {
            mvPenilaian.SetActiveView(vKomentar);
        }

        protected void lbSimpanKomentar_Click(object sender, EventArgs e)
        {
            if (!SimpanKomentar()) return;

            isilvDaftarUsulan();
            mvEvaluasi.SetActiveView(vDaftarUsulan);
        }

        protected bool SimpanKomentar()
        {
            //Cek Isian
            List<string> isianKosong = new List<string>();
            if (tbKomentar.Text.Trim().Length < 50) isianKosong.Add("Komentar minimal 50 karakter");
            if (tbKota.Text.Trim().Length == 0) isianKosong.Add("Kota tempat menilai");

            if (isianKosong.Count > 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi",
                   "Maaf, Data berikut harus disi :<br />" + string.Join(", ", isianKosong.ToArray()));
                return false;
            }

            if (!objModel.insupHasilReview(IdPlottingReviewer, tbKota.Text, tbKomentar.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return false;
            }

            return true;
        }

        #endregion
    }
}