using simlitekkes.Models.Pengusul;
using simlitekkes.Models.Reviewer;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Reviewer
{
    public partial class monevPenelitian : System.Web.UI.UserControl
    {
        Models.login objLogin;
        evaluasiSubstansi objModel = new evaluasiSubstansi();
        identitasUsulan objMdlIdUsulan = new identitasUsulan();
        lapKemajuan modelLapKemajuan = new lapKemajuan();
        monev objMonev = new monev();

        uiNotify noty = new uiNotify();

        const string KODE_TAHAPAN_KEGIATAN = "33"; //Tahapan Monev External
        const int ID_RISET_DASAR = 1;
        const int ID_RISET_TERAPAN = 2;
        const int ID_RISET_PENGEMBANGAN = 3;
        const string KODE_TAHAPAN_KEGIATAN_KEMAJUAN = "31";
        const string ID_KATEGORI_PENILAIAN_SKOR = "1";
        const string ID_KATEGORI_PENILAIAN_BOBOT = "2";

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

        private Guid IdTransaksiKegiatan
        {
            get
            {
                if (ViewState["IdTransaksiKegiatan"] == null) ViewState["IdTransaksiKegiatan"] = Guid.Empty;
                return Guid.Parse(ViewState["IdTransaksiKegiatan"].ToString());
            }
            set
            {
                ViewState["IdTransaksiKegiatan"] = value;
            }
        }

        private int IdKategoriRiset
        {
            get
            {
                if (ViewState["IdKategoriRiset"] == null) ViewState["IdKategoriRiset"] = default(int);
                return int.Parse(ViewState["IdKategoriRiset"].ToString());
            }
            set
            {
                ViewState["IdKategoriRiset"] = value;
            }
        }

        private decimal TotalNilai
        {
            get
            {
                if (ViewState["TotalNilai"] == null) ViewState["TotalNilai"] = default(decimal);
                return decimal.Parse(ViewState["TotalNilai"].ToString());
            }
            set
            {
                ViewState["TotalNilai"] = value;
            }
        }

        private DataTable OpsiNilaiMonev
        {
            get
            {
                if (ViewState["OpsiNilaiMonev"] == null) ViewState["OpsiNilaiMonev"] = new DataTable();
                return ViewState["OpsiNilaiMonev"] as DataTable;
            }
            set
            {
                ViewState["OpsiNilaiMonev"] = value;
            }
        }

        private List<HasilMonevLuaran> NilaiMonevLuaran
        {
            get
            {
                if (ViewState["NilaiMonevLuaran"] == null) ViewState["NilaiMonevLuaran"] = new List<HasilMonevLuaran>();
                return ViewState["NilaiMonevLuaran"] as List<HasilMonevLuaran>;
            }
            set
            {
                ViewState["NilaiMonevLuaran"] = value;
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
                setddlThnUsulan();
                setddlThnPelaksanaan();
                isilvDaftarPenugasan();
            }
        }

        private void setddlThnUsulan()
        {
            ddlTahunUsulan.Items.Clear();
            ddlTahunUsulan.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlTahunUsulan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void setddlThnPelaksanaan()
        {

            string thnUsulan = ddlTahunUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            ddlThnPelaksanaan.Items.Add(new ListItem("--Pilih--", "0000"));
            if (ddlTahunUsulan.Items.Count > 0 && ddlTahunUsulan.SelectedValue != "0000")
            {
                for (int i = 0; i < 2; i++)
                {
                    ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
                }
            }
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

        protected void SetModePermanen()
        {
            lbSimpanPermanen.Enabled = !IsPermanen;
            lbSimpanKomentar.Visible = !IsPermanen;

            tbKomentar.Enabled = !IsPermanen;
            lblStatus.Text = IsPermanen ? "PERMANEN" : "TERBUKA";
            lblStatus.CssClass = IsPermanen ? "label label-md label-danger" : "label label-md label-success";
        }

        protected void isilvDaftarUsulan()
        {
            var dt = new DataTable();
            if (!objMonev.getDaftarUsulanMonev(ref dt, IdPenugasanReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            try
            {
                lvDaftarUsulan.DataSource = dt;
                lvDaftarUsulan.DataBind();

                //ViewState["daftarUsulan"] = dt;
                lblJmlUsulan.Text = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void isilvLuaranWajib()
        {
            var dt = new DataTable();
            if (!objMonev.getDaftarLuaranWajib(ref dt, IdUsulanKegiatan))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            try
            {
                lvLuaranWajib.DataSource = dt;
                lvLuaranWajib.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void isilvLuaranTambahan()
        {
            var dt = new DataTable();
            if (!objMonev.getDaftarLuaranTambahan(ref dt, IdUsulanKegiatan))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            try
            {
                lvLuaranTambahan.DataSource = dt;
                lvLuaranTambahan.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void bindDropdownlistPenilaian(ref DropDownList ddl, DataTable dt, string idKategoriPeniliaian = "1")
        {
            var kategoriPenilaian = (idKategoriPeniliaian == ID_KATEGORI_PENILAIAN_SKOR) ? "Skor" : "Bobot";

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem()
            {
                Text = $"-- Pilih {kategoriPenilaian} --",
                Value = "-1",
                Selected = true
            });
            ddl.DataSource = dt;
            ddl.DataBind();
        }

        protected void setSelectedValueDropdownlist(ref DropDownList ddl, string selectedValue)
        {
            if (ddl.Items.FindByValue(selectedValue) == null)
            {
                if (ddl.Items.Count > 0) ddl.SelectedIndex = 0;
            }
            else
                ddl.SelectedValue = selectedValue;
        }

        protected decimal getNilai(int IdOpsiNilai)
        {
            var result = default(decimal);
            var nilai = OpsiNilaiMonev.Select($"id_opsi_nilai_monev = {IdOpsiNilai}");
            if (nilai.Length > 0) result = decimal.Parse(nilai[0]["nilai"].ToString());

            return result;
        }

        protected void isiDataEvaluasi()
        {
            var dt = new DataTable();
            var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();

            if (!objIdentitasUsulan.getDataDataUsulanKegiatan(ref dt, IdUsulanKegiatan.ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objIdentitasUsulan.errorMessage);
                return;
            }

            if (dt.Rows.Count == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Data Usulan tidak ditemukan !");
                return;
            }

            lblJudul.Text = dt.Rows[0]["judul"].ToString();
            lblSkemaUsulan.Text = dt.Rows[0]["nama_skema"].ToString();
            lblKategoriPenelitian.Text = dt.Rows[0]["kategori_riset"].ToString(); ;
            lblNamaKetua.Text = dt.Rows[0]["nama_ketua"].ToString();
            lblThnUsulanData.Text = dt.Rows[0]["thn_usulan_kegiatan"].ToString();
            lblThnPelaksanaanData.Text = dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString();
            lblUrutanTahun.Text = dt.Rows[0]["urutan_thn_usulan_kegiatan"].ToString();
            lblNamaKegiatan.Text = dt.Rows[0]["lama_kegiatan"].ToString();
            lblJmlAnggota.Text = dt.Rows[0]["jml_anggota"].ToString();
            lblBidangFokus.Text = dt.Rows[0]["bidang_fokus"].ToString();

            IdKategoriRiset = int.Parse(dt.Rows[0]["id_kategori_riset"].ToString());
            setModeRiset(IdKategoriRiset);

            var dtOpsi = new DataTable();
            objMonev.getDaftarOpsiNilaiMonev(ref dtOpsi, IdKategoriRiset);
            OpsiNilaiMonev = dtOpsi;

            var dictNilaiPelaksanaan = new Dictionary<int, int>();
            if (!objMonev.getDaftarHasilMonevPelaksanaan(ref dictNilaiPelaksanaan, IdPlottingReviewer, IdKategoriRiset))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objIdentitasUsulan.errorMessage);
                return;
            }

            //Komponen Monev 1
            var dtIdSub1 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 1").CopyToDataTable();
            var dtIdSub2 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 2").CopyToDataTable();

            bindDropdownlistPenilaian(ref ddlSkorID1, dtIdSub1, ID_KATEGORI_PENILAIAN_SKOR);
            bindDropdownlistPenilaian(ref ddlBobotID2, dtIdSub2, ID_KATEGORI_PENILAIAN_BOBOT);

            setSelectedValueDropdownlist(ref ddlSkorID1, dictNilaiPelaksanaan[1].ToString());
            setSelectedValueDropdownlist(ref ddlBobotID2, dictNilaiPelaksanaan[2].ToString());

            var nilai = getNilai(dictNilaiPelaksanaan[1]) * getNilai(dictNilaiPelaksanaan[2]);
            lblNilaiKesesuaian.Text = nilai.ToString("N2");
            lblTotalNilaiKesesuaian.Text = nilai.ToString("N2");

            if (IdKategoriRiset != 1)
            {
                // Komponen Monev 4
                var dtIdSub9 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 9").CopyToDataTable();
                var dtIdSub10 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 10").CopyToDataTable();

                bindDropdownlistPenilaian(ref ddlSkorID9, dtIdSub9, ID_KATEGORI_PENILAIAN_SKOR);
                bindDropdownlistPenilaian(ref ddlBobotID10, dtIdSub10, ID_KATEGORI_PENILAIAN_BOBOT);

                setSelectedValueDropdownlist(ref ddlSkorID9, dictNilaiPelaksanaan[9].ToString());
                setSelectedValueDropdownlist(ref ddlBobotID10, dictNilaiPelaksanaan[10].ToString());

                nilai = getNilai(dictNilaiPelaksanaan[9]) * getNilai(dictNilaiPelaksanaan[10]);
                lblNilaiRealisasiMitra.Text = nilai.ToString("N2");
                lblTotalNilaiRealisasiMitra.Text = nilai.ToString("N2");

                if (IdKategoriRiset == 3)
                {
                    // Komponen Monev 5 & 6
                    var dtIdSub11 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 11").CopyToDataTable();
                    var dtIdSub12 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 12").CopyToDataTable();
                    var dtIdSub13 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 13").CopyToDataTable();
                    var dtIdSub14 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 14").CopyToDataTable();

                    bindDropdownlistPenilaian(ref ddlSkorID11, dtIdSub11, ID_KATEGORI_PENILAIAN_SKOR);
                    bindDropdownlistPenilaian(ref ddlBobotID12, dtIdSub12, ID_KATEGORI_PENILAIAN_BOBOT);
                    bindDropdownlistPenilaian(ref ddlSkorID13, dtIdSub13, ID_KATEGORI_PENILAIAN_SKOR);
                    bindDropdownlistPenilaian(ref ddlBobotID14, dtIdSub14, ID_KATEGORI_PENILAIAN_BOBOT);

                    setSelectedValueDropdownlist(ref ddlSkorID11, dictNilaiPelaksanaan[11].ToString());
                    setSelectedValueDropdownlist(ref ddlBobotID12, dictNilaiPelaksanaan[12].ToString());
                    setSelectedValueDropdownlist(ref ddlSkorID13, dictNilaiPelaksanaan[13].ToString());
                    setSelectedValueDropdownlist(ref ddlBobotID14, dictNilaiPelaksanaan[14].ToString());

                    nilai = getNilai(dictNilaiPelaksanaan[11]) * getNilai(dictNilaiPelaksanaan[12]);
                    lblNilaiIntegritas.Text = nilai.ToString("N2");
                    lblTotalNilaiIntegritas.Text = nilai.ToString("N2");

                    nilai = getNilai(dictNilaiPelaksanaan[13]) * getNilai(dictNilaiPelaksanaan[14]);
                    lblNilaiRealisasiKerjasama.Text = nilai.ToString("N2");
                    lblTotalNilaiRealisasiKerjasama.Text = nilai.ToString("N2");
                }

            }

            var listHasilMonevLuaran = new List<HasilMonevLuaran>();
            if (!objMonev.getDaftarHasilMonevLuaran(ref listHasilMonevLuaran, IdPlottingReviewer, IdKategoriRiset))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objIdentitasUsulan.errorMessage);
                return;
            }

            NilaiMonevLuaran = listHasilMonevLuaran;

            lblTotalNilaiLuaranWajib.Text = "0";
            lblTotalNilaiLuaranTambahan.Text = "0";
            dt = new DataTable();
            if (!objMonev.getNilaiMonevLuaran(ref dt, IdPlottingReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objIdentitasUsulan.errorMessage);
                return;
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalNilaiLuaranWajib.Text = decimal.Parse(dt.Rows[0]["nilai_luaran_wajib"].ToString()).ToString("N2");
                    lblTotalNilaiLuaranTambahan.Text = decimal.Parse(dt.Rows[0]["nilai_luaran_tambahan"].ToString()).ToString("N2");
                }
            }

            var objEvalAdmin = new Models.Reviewer.evaluasiAdministrasi();
            dt = new DataTable();
            if (objEvalAdmin.getHasilReview(ref dt, IdPlottingReviewer) && dt.Rows.Count > 0)
            {
                tbKomentar.Text = dt.Rows[0]["komentar"].ToString();
            }
            else
                tbKomentar.Text = string.Empty;
        }

        protected void setModeRiset(int idKategoriRiset)
        {
            switch (idKategoriRiset)
            {
                case ID_RISET_TERAPAN:
                    panelTerapan.Visible = true;
                    panelPengembangan.Visible = false;
                    break;
                case ID_RISET_PENGEMBANGAN:
                    panelTerapan.Visible = true;
                    panelPengembangan.Visible = true;
                    break;
                case ID_RISET_DASAR:
                default:
                    panelTerapan.Visible = false;
                    panelPengembangan.Visible = false;
                    break;
            }
        }

        protected void ddlTahunUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var thnPelaksanaan = (int.Parse(ddlThnPelaksanaan.SelectedValue) + 1).ToString();
            //if (ddlThnPelaksanaan.Items.FindByValue(thnPelaksanaan) != null)
            //    ddlThnPelaksanaan.SelectedValue = thnPelaksanaan;

            setddlThnPelaksanaan();
            isilvDaftarPenugasan();
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isilvDaftarPenugasan();
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

            SetModePermanen();
            isilvDaftarUsulan();
            mvEvaluasi.SetActiveView(vDaftarUsulan);
        }

        protected void lbKembaliKePenugasan_Click(object sender, EventArgs e)
        {
            isilvDaftarPenugasan();
            mvEvaluasi.SetActiveView(vRekapEvaluasi);
        }

        protected void lvDaftarUsulan_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            IdPlottingReviewer = Guid.Parse(lvDaftarUsulan.DataKeys[e.ItemIndex]["id_plotting_reviewer"].ToString());
            IdUsulanKegiatan = Guid.Parse(lvDaftarUsulan.DataKeys[e.ItemIndex]["id_usulan_kegiatan"].ToString());

            var dt = new DataTable();
            modelLapKemajuan.getIdTransaksiDimonev(ref dt, IdUsulanKegiatan.ToString());
            string statusLaporanAkhir = string.Empty;

            if (dt.Rows.Count > 0)
            {
                IdTransaksiKegiatan = Guid.Parse(dt.Rows[0]["id_transaksi_kegiatan"].ToString());
                statusLaporanAkhir = dt.Rows[0]["sts_laporan_akhir"].ToString();
                lblUndulLapKemajuan.Text = (statusLaporanAkhir == "1") ? "LAPORAN AKHIR" : "LAPORAN KEMAJUAN";
            }
            ViewState["sts_laporan_akhir"] = statusLaporanAkhir;

            isiDataEvaluasi();
            isilvLuaranWajib();
            isilvLuaranTambahan();
            cekStatusDokumenRealisasiMitra();

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

        protected void lvLuaranWajib_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var drv = (DataRowView)e.Item.DataItem;
                var kontrolLuaran = (luaranDicapai)e.Item.FindControl("kontrolLuaranDicapai");
                var idLuaranDijanjikan = (!string.IsNullOrWhiteSpace(drv["id_luaran_dijanjikan"].ToString())) ?
                        Guid.Parse(drv["id_luaran_dijanjikan"].ToString()) : Guid.Empty;

                kontrolLuaran.setData(idLuaranDijanjikan, IdTransaksiKegiatan,
                    ViewState["sts_laporan_akhir"].ToString(), ddlThnPelaksanaan.SelectedValue);

                var ddlSkorID3 = e.Item.FindControl("ddlSkorID3") as DropDownList;
                var ddlBobotID4 = e.Item.FindControl("ddlBobotID4") as DropDownList;
                var ddlBobotID5 = e.Item.FindControl("ddlBobotID5") as DropDownList;
                var ddlBobotID6 = e.Item.FindControl("ddlBobotID6") as DropDownList;

                var dtIdSub3 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 3").CopyToDataTable();
                var dtIdSub4 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 4").CopyToDataTable();
                var dtIdSub5 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 5").CopyToDataTable();
                var dtIdSub6 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 6").CopyToDataTable();

                bindDropdownlistPenilaian(ref ddlSkorID3, dtIdSub3, ID_KATEGORI_PENILAIAN_SKOR);
                bindDropdownlistPenilaian(ref ddlBobotID4, dtIdSub4, ID_KATEGORI_PENILAIAN_BOBOT);
                bindDropdownlistPenilaian(ref ddlBobotID5, dtIdSub5, ID_KATEGORI_PENILAIAN_BOBOT);
                bindDropdownlistPenilaian(ref ddlBobotID6, dtIdSub6, ID_KATEGORI_PENILAIAN_BOBOT);

                var lblNilaiLuaranWajib = e.Item.FindControl("lblNilaiLuaranWajib") as Label;
                decimal nilai = 0;

                if (idLuaranDijanjikan != Guid.Empty)
                {
                    var nilaiID3 = NilaiMonevLuaran.FirstOrDefault(i => i.IdLuaranDijanjikan == idLuaranDijanjikan && i.IdSubKategori == 3);
                    var nilaiID4 = NilaiMonevLuaran.FirstOrDefault(i => i.IdLuaranDijanjikan == idLuaranDijanjikan && i.IdSubKategori == 4);
                    var nilaiID5 = NilaiMonevLuaran.FirstOrDefault(i => i.IdLuaranDijanjikan == idLuaranDijanjikan && i.IdSubKategori == 5);
                    var nilaiID6 = NilaiMonevLuaran.FirstOrDefault(i => i.IdLuaranDijanjikan == idLuaranDijanjikan && i.IdSubKategori == 6);

                    if (nilaiID3 != null) setSelectedValueDropdownlist(ref ddlSkorID3, nilaiID3.IdOpsiNilai.ToString());
                    if (nilaiID4 != null) setSelectedValueDropdownlist(ref ddlBobotID4, nilaiID4.IdOpsiNilai.ToString());
                    if (nilaiID5 != null) setSelectedValueDropdownlist(ref ddlBobotID5, nilaiID5.IdOpsiNilai.ToString());
                    if (nilaiID6 != null) setSelectedValueDropdownlist(ref ddlBobotID6, nilaiID6.IdOpsiNilai.ToString());

                    if (nilaiID3 != null && nilaiID4 != null && nilaiID5 != null && nilaiID6 != null)
                    {
                        nilai = getNilai(nilaiID3.IdOpsiNilai) * getNilai(nilaiID4.IdOpsiNilai) *
                                    getNilai(nilaiID5.IdOpsiNilai) * getNilai(nilaiID6.IdOpsiNilai);
                        //lblNilaiLuaranWajib.Text = nilai.ToString("N2");
                    }
                }

                lblNilaiLuaranWajib.Text = nilai.ToString("N2");
            }
        }

        protected void lvLuaranTambahan_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var drv = (DataRowView)e.Item.DataItem;
                var kontrolLuaran = (luaranDicapai)e.Item.FindControl("kontrolLuaranDicapai");
                var idLuaranDijanjikan = (!string.IsNullOrWhiteSpace(drv["id_luaran_dijanjikan"].ToString())) ?
                        Guid.Parse(drv["id_luaran_dijanjikan"].ToString()) : Guid.Empty;

                kontrolLuaran.setData(idLuaranDijanjikan, IdTransaksiKegiatan,
                    ViewState["sts_laporan_akhir"].ToString(), ddlThnPelaksanaan.SelectedValue);

                var ddlSkorID7 = e.Item.FindControl("ddlSkorID7") as DropDownList;
                var ddlBobotID8 = e.Item.FindControl("ddlBobotID8") as DropDownList;

                var dtIdSub7 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 7").CopyToDataTable();
                var dtIdSub8 = OpsiNilaiMonev.Select("id_sub_kategori_penilaian_komponen_monev = 8").CopyToDataTable();

                bindDropdownlistPenilaian(ref ddlSkorID7, dtIdSub7, ID_KATEGORI_PENILAIAN_SKOR);
                bindDropdownlistPenilaian(ref ddlBobotID8, dtIdSub8, ID_KATEGORI_PENILAIAN_BOBOT);

                var lblNilaiLuaranTambahan = e.Item.FindControl("lblNilaiLuaranTambahan") as Label;
                decimal nilai = 0;

                if (idLuaranDijanjikan != Guid.Empty)
                {
                    var nilaiID7 = NilaiMonevLuaran.FirstOrDefault(i => i.IdLuaranDijanjikan == idLuaranDijanjikan && i.IdSubKategori == 7);
                    var nilaiID8 = NilaiMonevLuaran.FirstOrDefault(i => i.IdLuaranDijanjikan == idLuaranDijanjikan && i.IdSubKategori == 8);

                    if (nilaiID7 != null) setSelectedValueDropdownlist(ref ddlSkorID7, nilaiID7.IdOpsiNilai.ToString());
                    if (nilaiID8 != null) setSelectedValueDropdownlist(ref ddlBobotID8, nilaiID8.IdOpsiNilai.ToString());

                    //var lblNilaiLuaranTambahan = e.Item.FindControl("lblNilaiLuaranTambahan") as Label;
                    if (nilaiID7 != null && nilaiID8 != null)
                    {
                        nilai = getNilai(nilaiID7.IdOpsiNilai) * getNilai(nilaiID8.IdOpsiNilai);

                    }
                }

                lblNilaiLuaranTambahan.Text = nilai.ToString("N2");
            }
        }

        protected void lbSimpanKomentar_Click(object sender, EventArgs e)
        {
            var dictNilaiPelaksanaan = new Dictionary<int, string>();

            dictNilaiPelaksanaan.Add(1, ddlSkorID1.SelectedValue);
            dictNilaiPelaksanaan.Add(2, ddlBobotID2.SelectedValue);

            if (IdKategoriRiset != 1)
            {
                dictNilaiPelaksanaan.Add(9, ddlSkorID9.SelectedValue);
                dictNilaiPelaksanaan.Add(10, ddlBobotID10.SelectedValue);

                if (IdKategoriRiset == 3)
                {
                    dictNilaiPelaksanaan.Add(11, ddlSkorID11.SelectedValue);
                    dictNilaiPelaksanaan.Add(12, ddlBobotID12.SelectedValue);
                    dictNilaiPelaksanaan.Add(13, ddlSkorID13.SelectedValue);
                    dictNilaiPelaksanaan.Add(14, ddlBobotID14.SelectedValue);
                }

            }

            var listNilaiLuaran = new List<HasilMonevLuaran>();

            for (int i = 0; i < lvLuaranWajib.Items.Count; i++)
            {
                var idLuaranDijanjikan = Guid.Parse(lvLuaranWajib.DataKeys[i]["id_luaran_dijanjikan"].ToString());

                var ddlSkorID3 = lvLuaranWajib.Items[i].FindControl("ddlSkorID3") as DropDownList;
                var ddlBobotID4 = lvLuaranWajib.Items[i].FindControl("ddlBobotID4") as DropDownList;
                var ddlBobotID5 = lvLuaranWajib.Items[i].FindControl("ddlBobotID5") as DropDownList;
                var ddlBobotID6 = lvLuaranWajib.Items[i].FindControl("ddlBobotID6") as DropDownList;

                listNilaiLuaran.Add(new HasilMonevLuaran()
                {
                    IdLuaranDijanjikan = idLuaranDijanjikan,
                    IdSubKategori = 3,
                    IdOpsiNilai = int.Parse(ddlSkorID3.SelectedValue)
                });

                listNilaiLuaran.Add(new HasilMonevLuaran()
                {
                    IdLuaranDijanjikan = idLuaranDijanjikan,
                    IdSubKategori = 4,
                    IdOpsiNilai = int.Parse(ddlBobotID4.SelectedValue)
                });

                listNilaiLuaran.Add(new HasilMonevLuaran()
                {
                    IdLuaranDijanjikan = idLuaranDijanjikan,
                    IdSubKategori = 5,
                    IdOpsiNilai = int.Parse(ddlBobotID5.SelectedValue)
                });

                listNilaiLuaran.Add(new HasilMonevLuaran()
                {
                    IdLuaranDijanjikan = idLuaranDijanjikan,
                    IdSubKategori = 6,
                    IdOpsiNilai = int.Parse(ddlBobotID6.SelectedValue)
                });

            }

            if (lvLuaranTambahan.Items.Any())
            {
                for (int i = 0; i < lvLuaranTambahan.Items.Count; i++)
                {
                    var idLuaranDijanjikan = Guid.Parse(lvLuaranTambahan.DataKeys[i]["id_luaran_dijanjikan"].ToString());

                    var ddlSkorID7 = lvLuaranTambahan.Items[i].FindControl("ddlSkorID7") as DropDownList;
                    var ddlBobotID8 = lvLuaranTambahan.Items[i].FindControl("ddlBobotID8") as DropDownList;

                    listNilaiLuaran.Add(new HasilMonevLuaran()
                    {
                        IdLuaranDijanjikan = idLuaranDijanjikan,
                        IdSubKategori = 7,
                        IdOpsiNilai = int.Parse(ddlSkorID7.SelectedValue)
                    });

                    listNilaiLuaran.Add(new HasilMonevLuaran()
                    {
                        IdLuaranDijanjikan = idLuaranDijanjikan,
                        IdSubKategori = 8,
                        IdOpsiNilai = int.Parse(ddlBobotID8.SelectedValue)
                    });
                }
            }

            if (!objMonev.insupListPenilaian(IdPlottingReviewer, dictNilaiPelaksanaan, listNilaiLuaran))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMonev.errorMessage);
                return;
            }

            if (tbKomentar.Text.Trim().Length < 50)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                            "Data Komentar (minimal 50 karakter) harus diisi terlebih dahulu !");
            }

            if (!objModel.insupHasilReview(IdPlottingReviewer, string.Empty, tbKomentar.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            isiDataEvaluasi();
            isilvLuaranWajib();
            isilvLuaranTambahan();

            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Penilaian berhasil disimpan...");

        }

        protected void lbUnduhPdfUsulanLengkap_Click(object sender, EventArgs e)
        {
            pdfUsulanLengkap.UnduhProposalLengkap(IdUsulanKegiatan.ToString());
        }

        protected void lbUnduhLapKemajuan_Click(object sender, EventArgs e)
        {
            pdfLaporanKemajuan.unduhLapKemajuan(IdTransaksiKegiatan.ToString());
        }

        protected void lbUnduhPdfRealisasiMitra_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelLapKemajuan.getDokumenMitra(ref dt, IdTransaksiKegiatan);
            if (dt.Rows.Count > 0)
            {
                string filePath = Server.MapPath(string.Format(dt.Rows[0]["lokasi_file"].ToString()));
                string NamaBerkas = "dokumen_mitra.pdf";

                if (File.Exists(filePath))
                {
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + NamaBerkas);
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Dokumen tidak ditemukan");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Dokumen tidak ditemukan");
            }
        }

        public void cekStatusDokumenRealisasiMitra()
        {
            DataTable dt = new DataTable();
            modelLapKemajuan.getDokumenMitra(ref dt, IdTransaksiKegiatan);
            lbUnduhPdfRealisasiMitra.CssClass = "btn btn-secondary waves-effect waves-light";
            lbUnduhPdfRealisasiMitra.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                string filePath = Server.MapPath(string.Format(dt.Rows[0]["lokasi_file"].ToString()));
                if (File.Exists(filePath))
                {
                    lbUnduhPdfRealisasiMitra.CssClass = "btn btn-danger waves-effect waves-light";
                    lbUnduhPdfRealisasiMitra.Enabled = true;
                }
            }
        }

        protected void lbSimpanPermanen_Click(object sender, EventArgs e)
        {
            foreach (var li in lvDaftarUsulan.Items)
            {
                if (lvDaftarUsulan.DataKeys[li.DisplayIndex]["jml_komponen"].ToString() != lvDaftarUsulan.DataKeys[li.DisplayIndex]["jml_komponen_dinilai"].ToString())
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       "Penelitian harus dinilai semua !");
                    return;
                }
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

        protected void lbCatatanHarian_Click(object sender, EventArgs e)
        {
            Session["thn_pelaksanaan_rev"] = ddlThnPelaksanaan.SelectedValue;
            Session["id_usulan_kegiatan_rev"] = IdUsulanKegiatan;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('RevCatatanHarian.aspx');", true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('UserControls/Reviewer/parentCatatanHarian.aspx');", true);
        }
    }
}