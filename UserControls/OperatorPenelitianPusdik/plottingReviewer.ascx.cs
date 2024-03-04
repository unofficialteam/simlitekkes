using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class plottingReviewer : System.Web.UI.UserControl
    {
        Models.login objLogin;
        //Models.OperatorPenelitianPusdik.plottingReviewerAdministrasi objModel = new Models.DRPM.plottingReviewerAdministrasi();
        Models.OperatorPenelitianPusdik.plottingReviewer objModelPlottingReviewer = new Models.OperatorPenelitianPusdik.plottingReviewer();
        Models.OperatorPenelitianPusdik.daftarPenugasanReviewer modelPenugasanReviewer = new Models.OperatorPenelitianPusdik.daftarPenugasanReviewer();
        Core.manipulasiData objManipData = new Core.manipulasiData();

        uiNotify noty = new uiNotify();
        uiModal modal = new uiModal();
        uiListView listView = new uiListView();
        uiGridView obj_uiGridView = new uiGridView();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();

        const string ID_HAPUS_PLOTTING = "1";
        const string ID_HAPUS_REVIEWER = "2";
        static readonly Guid ID_INSTITUSI_DRPM = new Guid("ad34091d-893d-4adc-af4d-d18061bea699");

        #region Properties

        private int PagePlotting
        {
            get
            {
                if (ViewState["PagePlotting"] == null)
                    return default(int);
                else
                    return int.Parse(ViewState["PagePlotting"].ToString());
            }
            set
            {
                ViewState["PagePlotting"] = value;
            }
        }

        private int PagePilihReviewer
        {
            get
            {
                if (ViewState["PagePilihReviewer"] == null)
                    return default(int);
                else
                    return int.Parse(ViewState["PagePilihReviewer"].ToString());
            }
            set
            {
                ViewState["PagePilihReviewer"] = value;
            }
        }

        private int IdSkema
        {
            get
            {
                if (ViewState["IdSkema"] == null)
                    return default(int);
                else
                    return int.Parse(ViewState["IdSkema"].ToString());
            }
            set
            {
                ViewState["IdSkema"] = value;
            }
        }

        private string IdHapus
        {
            get => (ViewState["IdHapus"] == null) ? null : ViewState["IdHapus"].ToString();
            set
            {
                ViewState["IdHapus"] = value;
            }
        }

        private Guid IdTransaksiKegiatan
        {
            get
            {
                if (ViewState["IdTransaksiKegiatan"] == null)
                    return default(Guid);
                else
                    return Guid.Parse(ViewState["IdTransaksiKegiatan"].ToString());
            }
            set
            {
                ViewState["IdTransaksiKegiatan"] = value;
            }
        }

        private Guid IdPenugasanReviewer
        {
            get
            {
                if (ViewState["IdPenugasanReviewer"] == null)
                    return default(Guid);
                else
                    return Guid.Parse(ViewState["IdPenugasanReviewer"].ToString());
            }
            set
            {
                ViewState["IdPenugasanReviewer"] = value;
            }
        }

        private int NoUrutReviewer
        {
            get
            {
                if (ViewState["no_urut"] == null)
                    return -1;
                else
                    return int.Parse(ViewState["no_urut"].ToString());
            }
            set
            {
                ViewState["no_urut"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];
            //KonfirmasiHapus.OnDelete += new EventHandler(KonfirmasiHapus_OnDelete);

            if (!IsPostBack)
            {
                PagePlotting = 0;
                PagePilihReviewer = 0;

                isiDdlThnUsulan();
                isiDdlThnPelaksanaan();
                isiTahapanKegiatan();

                refreshlvDaftarSkema();
            }
        }

        //protected void KonfirmasiHapus_OnDelete(object sender, EventArgs e)
        //{
        //    switch (IdHapus)
        //    {
        //        case ID_HAPUS_PLOTTING:
        //            if (objModelPlottingReviewer.deleteDataPlotting(IdTransaksiKegiatan, 1))
        //            {
        //                refreshPaging();
        //                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
        //            }
        //            else
        //            {
        //                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. Error = " + objModelPlottingReviewer.errorMessage);
        //            }
        //            break;

        //        case ID_HAPUS_REVIEWER:
        //            break;
        //    }
        //}

        #region View Skema

        protected void refreshlvDaftarSkema()
        {
            var dt = new DataTable();
            if (!objModelPlottingReviewer.getListSkema(ref dt, Guid.Parse(objLogin.idPersonal),
                    ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
                return;
            }

            if (ddlTahapan.SelectedValue == "20")
            {
                gvRekapSkema.Columns[2].HeaderText = "Plotting Reviewer";
                gvRekapSkema.Columns[3].Visible = false;

            }
            else
            {
                gvRekapSkema.Columns[2].HeaderText = "Plotting Rev. 1";
                gvRekapSkema.Columns[3].Visible = true;
            }

            try
            {
                gvRekapSkema.DataSource = dt;
                gvRekapSkema.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void gvRekapSkema_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //IdSkema = int.Parse(gvRekapSkema.DataKeys[e.RowIndex]["id_skema"].ToString());
            //string namaSkema = gvRekapSkema.DataKeys[e.RowIndex]["nama_skema"].ToString();
            //lblSkemaPlotting.Text = namaSkema;
            //lblThnUsulanPlot.Text = ddlThnUsulan.SelectedValue;
            //lblThnPelaksanaanPlot.Text = ddlThnPelaksanaan.SelectedValue;

            //lblTahapanViewReviewer.Text = ddlTahapan.SelectedItem.Text;
            //lblTahapanViewPlotting.Text = ddlTahapan.SelectedItem.Text;
            //lblTahapanViewPilih.Text = ddlTahapan.SelectedItem.Text;

            //RefreshlvPlotting(0);

            //mvPlotAdministrasi.SetActiveView(vPlotting);
        }

        protected void gvRekapSkema_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int Idx = int.Parse(e.CommandArgument.ToString());
            IdSkema = int.Parse(gvRekapSkema.DataKeys[Idx]["id_skema"].ToString());
            string namaSkema = gvRekapSkema.DataKeys[Idx]["nama_skema"].ToString();

            lblSkemaPlotting.Text = namaSkema;
            lblThnUsulanPlot.Text = ddlThnUsulan.SelectedValue;
            lblThnPelaksanaanPlot.Text = ddlThnPelaksanaan.SelectedValue;

            //lblTahapanViewReviewer.Text = ddlTahapan.SelectedItem.Text;
            lblTahapanViewPlotting.Text = ddlTahapan.SelectedItem.Text;
            lblTahapanViewPilih.Text = ddlTahapan.SelectedItem.Text;

            switch (e.CommandName.ToUpper())
            {
                case "TAMBAHREVIEWER":
                    //mvDaftarPlotting.SetActiveView(viewDaftarPlotting);
                    //lblThnUsulan.Text = ddlThnUsulan.SelectedValue;
                    //lblThnUsulan2.Text = ddlThnUsulan.SelectedValue;
                    //lblTahapan.Text = ddlTahapan.SelectedItem.Text;
                    //lblNamaSkemaNTahun.Text = nama_skema + " - Pelaksanaan " + ddlThnPelaksanaan.SelectedValue;
                    //lblTahapan2.Text = ddlTahapan.SelectedItem.Text;
                    //lblNamaSkemaNTahun2.Text = nama_skema + " - Pelaksanaan " + ddlThnPelaksanaan.SelectedValue;
                    //setListDataPloting(0);
                    break;
            }
            //if (e.CommandName == "TambahReviewer")
            //{


            //    ViewState["id_skema"] = id_skema;

            //    mvDaftarPlotting.SetActiveView(viewDaftarPlotting);
            //    lblThnUsulan.Text = ddlThnUsulan.SelectedValue;
            //    lblThnUsulan2.Text = ddlThnUsulan.SelectedValue;
            //    lblTahapan.Text = ddlTahapan.SelectedItem.Text;
            //    lblNamaSkemaNTahun.Text = nama_skema + " - Pelaksanaan " + ddlThnPelaksanaan.SelectedValue;
            //    lblTahapan2.Text = ddlTahapan.SelectedItem.Text;
            //    lblNamaSkemaNTahun2.Text = nama_skema + " - Pelaksanaan " + ddlThnPelaksanaan.SelectedValue;
            //    setListDataPloting(0);
            //}
            RefreshlvPlotting(0);
            mvPlotAdministrasi.SetActiveView(vPlotting);
        }

        #endregion

        #region View Reviewer

        protected void refreshlvReviewer()
        {
            //var dt = new DataTable();
            //if (!objModel.getListReviewer(ref dt, IdSkema,
            //        ddlThnUsulan.SelectedItem.Text, ddlThnPelaksanaan.SelectedItem.Text))
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
            //    return;
            //}

            //try
            //{
            //    lvReviewer.DataSource = dt;
            //    lvReviewer.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            //}
        }

        protected void lvReviewer_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            //IdPenugasanReviewer = Guid.Parse(lvReviewer.DataKeys[e.ItemIndex]["id_penugasan_reviewer"].ToString());

            //mvPlotAdministrasi.SetActiveView(vProposalReviewer);
        }

        protected void lbKembaliKeSkema_Click(object sender, EventArgs e)
        {
            refreshlvDaftarSkema();
            mvPlotAdministrasi.SetActiveView(vDaftarSkema);
        }

        #endregion

        #region View Plotting

        protected void ddlJumlahBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            PagePlotting = 0;
            RefreshlvPlotting(0);
        }

        private void RefreshlvPlotting(int pageIndex)
        {
            objModelPlottingReviewer.currentPage = pageIndex;
            objModelPlottingReviewer.rowsPerPage = int.Parse(ddlJumlahBaris.SelectedValue);

            if (!objModelPlottingReviewer.getJmlPlottingAdm(IdSkema, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, tbCariProposal.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
                return;
            }

            lblJumlahProposal.Text = objModelPlottingReviewer.numOfRecords.ToString();
            ktPagingPlotting.currentPage = pageIndex;
            ktPagingPlotting.setPaging(int.Parse(ddlJumlahBaris.SelectedValue), objModelPlottingReviewer.numOfRecords);

            objModelPlottingReviewer.currentPage = pageIndex;
            objModelPlottingReviewer.rowsPerPage = int.Parse(ddlJumlahBaris.SelectedValue);

            if (!objModelPlottingReviewer.getDaftarPlottingAdm(IdSkema, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, tbCariProposal.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
                return;
            }

            gvPlotting.DataSource = objModelPlottingReviewer.currentRecords;
            if (ddlTahapan.SelectedValue == "32" || ddlTahapan.SelectedValue == "16" ||
            ddlTahapan.SelectedValue == "20")
            {
                gvPlotting.Columns[2].HeaderText = "Reviewer";
                gvPlotting.Columns[3].Visible = false;
            }
            else
            {
                gvPlotting.Columns[2].HeaderText = "Reviewer 1";
                gvPlotting.Columns[3].Visible = true;
            }

            if (objModelPlottingReviewer.numOfRecords < 1)
            {
                ktPagingPlotting.setPaging(int.Parse(ddlJumlahBaris.SelectedValue), 1);
            }

            gvPlotting.DataBind();
        }

        protected void gvPlotting_Init(object sender, System.EventArgs e)
        {
            //foreach (DataControlField kolom in gvPlotting.Columns)
            //{
            //    if (ddlTahapan.SelectedValue == "32" || ddlTahapan.SelectedValue == "16" ||
            //                ddlTahapan.SelectedValue == "20")
            //    {
            //        if (kolom.HeaderText == "Reviewer 1")
            //            kolom.Visible = false;
            //    }
            //}
        }

        protected void gvPlotting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNamaReviewer1 = (Label)e.Row.FindControl("lblNamaReviewer1");
                Label lblNamaReviewer2 = (Label)e.Row.FindControl("lblNamaReviewer2");
                //Label lblTglReviewRev1 = (Label)e.Row.FindControl("lblTglReviewRev1");
                //Label lblTglReviewRev2 = (Label)e.Row.FindControl("lblTglReviewRev2");
                LinkButton lbTambahReviewer1 = (LinkButton)e.Row.FindControl("lbTambahReviewer1");
                LinkButton lbTambahReviewer2 = (LinkButton)e.Row.FindControl("lbTambahReviewer2");
                LinkButton lbDelReviewer1 = (LinkButton)e.Row.FindControl("lbDelReviewer1");
                LinkButton lbDelReviewer2 = (LinkButton)e.Row.FindControl("lbDelReviewer2");
                lbTambahReviewer1.Visible = false;
                lbTambahReviewer2.Visible = false;
                lbDelReviewer1.Visible = false;
                lbDelReviewer2.Visible = false;
                Boolean status = false;

                DataTable dtcek = new DataTable();
                if (objModelPlottingReviewer.cekjadwal(ref dtcek, IdSkema.ToString(), ddlTahapan.SelectedValue))
                {
                    if (dtcek.Rows.Count > 0)
                    {
                        status = true;
                    }
                    else
                    {
                        Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());
                        if (objModelPlottingReviewer.getDaftarWhitelist(id_institusi, IdSkema,
                            ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue) == true)
                        {
                            status = true;
                        }
                        else
                        {
                            status = true;
                        }
                    }
                }
                if (lblNamaReviewer1 != null)
                {
                    if (lblNamaReviewer1.Text.Trim() == "")
                    {
                        lbTambahReviewer1.Visible = true;
                        lbTambahReviewer1.Enabled = status;
                    }
                    else
                    {
                        lbDelReviewer1.Visible = true;
                        lbDelReviewer1.Enabled = status;
                    }
                    if (lblNamaReviewer2.Text.Trim() == "")
                    {
                        lbTambahReviewer2.Visible = true;
                        lbTambahReviewer2.Enabled = status;
                        if (ddlTahapan.SelectedValue == "32" || ddlTahapan.SelectedValue == "16" ||
                            ddlTahapan.SelectedValue == "20")
                        {
                            lbTambahReviewer2.Visible = false;
                        }
                    }
                    else
                    {
                        lbDelReviewer2.Visible = true;
                        lbDelReviewer2.Enabled = status;
                        if (ddlTahapan.SelectedValue == "32" || ddlTahapan.SelectedValue == "16" ||
                            ddlTahapan.SelectedValue == "20")
                        {
                            lbDelReviewer2.Visible = false;
                        }
                    }
                }
                if (ddlTahapan.SelectedValue == "33" || ddlTahapan.SelectedValue == "49")
                {
                    lbTambahReviewer2.Visible = false;
                    lbDelReviewer2.Visible = false;
                }
            }
        }

        protected void gvPlotting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int indek = int.Parse(e.CommandArgument.ToString());
            IdTransaksiKegiatan = Guid.Parse(gvPlotting.DataKeys[indek]["id_transaksi_kegiatan"].ToString());
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblJudul = (Label)row.FindControl("lblJudul");
            Label lblInstitusi1 = (Label)row.FindControl("lblInstitusi");
            Label lblBidFokus = (Label)row.FindControl("lblBidFokus");

            if (e.CommandName == "tambah_rev1")
            {
                lblJudulPage.Text = "Plotting Reviewer 1";
                lblJudulProposal.Text = lblJudul.Text;
                lblInstitusiPengusul.Text = lblInstitusi1.Text;
                lblBidangFokus.Text = lblBidFokus.Text;
                NoUrutReviewer = 1;
                mvPlotAdministrasi.SetActiveView(vPilihReviewer);
                refreshlvPilihReviewer();
            }
            else if (e.CommandName == "tambah_rev2")
            {
                lblJudulPage.Text = "Plotting Reviewer 2";
                lblJudulProposal.Text = lblJudul.Text;
                lblInstitusiPengusul.Text = lblInstitusi1.Text;
                lblBidangFokus.Text = lblBidFokus.Text;
                NoUrutReviewer = 2;
                mvPlotAdministrasi.SetActiveView(vPilihReviewer);
                refreshlvPilihReviewer();
            }
            else if (e.CommandName == "hapus_rev1")
            {
                NoUrutReviewer = 1;
                lblJudulDihapus.Text = lblJudul.Text;

                Label lblNamaReviewer = (Label)row.FindControl("lblNamaReviewer1");
                lblPlottingDihapus.Text = lblNamaReviewer.Text;
                modal.ShowModal(this.Page, "modalHapus");
            }
            else if (e.CommandName == "hapus_rev2")
            {
                NoUrutReviewer = 2;
                lblJudulDihapus.Text = lblJudul.Text;

                Label lblNamaReviewer = (Label)row.FindControl("lblNamaReviewer2");
                lblPlottingDihapus.Text = lblNamaReviewer.Text;
                modal.ShowModal(this.Page, "modalHapus");
            }
        }

        protected void ktPagingPlotting_PageChanging(object sender, EventArgs e)
        {

            objModelPlottingReviewer.currentPage = ktPagingPlotting.currentPage;
            objModelPlottingReviewer.rowsPerPage = int.Parse(ddlJumlahBaris.SelectedValue);

            if (!objModelPlottingReviewer.getDaftarPlottingAdm(IdSkema, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, tbCariProposal.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
                return;
            }

            lblJumlahProposal.Text = objModelPlottingReviewer.numOfRecords.ToString();

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvPlotting, objModelPlottingReviewer.currentRecords);
        }

        protected void lvPlotting_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            //lblJudulProposal.Text = ((Label)lvPlotting.Items[e.ItemIndex].FindControl("lblJudul")).Text;
            //lblInstitusiPengusul.Text = ((Label)lvPlotting.Items[e.ItemIndex].FindControl("lblInstitusi")).Text;
            //IdTransaksiKegiatan = Guid.Parse(lvPlotting.DataKeys[e.ItemIndex]["id_transaksi_kegiatan"].ToString());

            //refreshlvPilihReviewer();
            //mvPlotAdministrasi.SetActiveView(vPilihReviewer);
        }

        protected void lvPlotting_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            //var lblNamaReviewer = lvPlotting.Items[e.ItemIndex].FindControl("lblNamaReviewer") as Label;
            //IdTransaksiKegiatan = Guid.Parse(lvPlotting.DataKeys[e.ItemIndex]["id_transaksi_kegiatan"].ToString());
            //IdHapus = ID_HAPUS_PLOTTING;

            //KonfirmasiHapus.TextKonfirmasi = $"Apakah Anda ingin menghapus <b>{lblNamaReviewer.Text}</b> sebagai Reviewer ?";
            //KonfirmasiHapus.Show();
        }

        protected void lbCariProposal_Click(object sender, EventArgs e)
        {
            RefreshlvPlotting(0);
        }

        protected void lvPlotting_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var drv = (DataRowView)e.Item.DataItem;
                var lblNomor = (Label)e.Item.FindControl("lblNo");
                lblNomor.Text = (e.Item.DataItemIndex + 1 + int.Parse(ddlJumlahBaris.SelectedValue) * (ktPagingPlotting.currentPage)).ToString();
                var lbDelReviewer = (LinkButton)e.Item.FindControl("lbDelReviewer");
                var lbTambahReviewer = (LinkButton)e.Item.FindControl("lbTambahReviewer");

                if (!string.IsNullOrEmpty(drv["nama_reviewer_1"].ToString()))
                {
                    lbDelReviewer.Visible = true;
                    lbTambahReviewer.Visible = false;
                }
                else
                {
                    lbDelReviewer.Visible = false;
                    lbTambahReviewer.Visible = true;
                }
            }
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (objModelPlottingReviewer.deleteDataPlotting(IdTransaksiKegiatan, NoUrutReviewer))
            {
                objModelPlottingReviewer.currentPage = ktPagingPlotting.currentPage;
                objModelPlottingReviewer.rowsPerPage = int.Parse(ddlJumlahBaris.SelectedValue);

                if (!objModelPlottingReviewer.getDaftarPlottingAdm(IdSkema, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                    ddlTahapan.SelectedValue, tbCariProposal.Text))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
                    return;
                }

                lblJumlahProposal.Text = objModelPlottingReviewer.numOfRecords.ToString();

                obj_uiGridView = new UIControllers.uiGridView();
                obj_uiGridView.bindToGridView(ref gvPlotting, objModelPlottingReviewer.currentRecords);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Reviewer tidak bisa dihapus karena sudah melakukan penilaian");
            }
        }

        #endregion

        #region Pilih Reviewer

        protected void refreshlvPilihReviewer()
        {
            var dt = new DataTable();
            if (!objModelPlottingReviewer.getListPilihanReviewer(ref dt, IdTransaksiKegiatan, tbCariReviewer.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
                return;
            }

            try
            {
                lvPilihReviewer.DataSource = dt;
                lvPilihReviewer.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void lblKembaliKePlotting_Click(object sender, EventArgs e)
        {
            mvPlotAdministrasi.SetActiveView(vPlotting);
        }

        protected void lvPilihReviewer_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var idReviewer = Guid.Parse(lvPilihReviewer.DataKeys[e.ItemIndex]["id_reviewer"].ToString());
            var idPenugasanReviewer = Guid.Parse(lvPilihReviewer.DataKeys[e.ItemIndex]["id_penugasan_reviewer"].ToString());

            if (!objModelPlottingReviewer.insertPlottingReviewer(IdTransaksiKegiatan, idReviewer, NoUrutReviewer, idPenugasanReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
                return;
            }
            objModelPlottingReviewer.currentPage = ktPagingPlotting.currentPage;
            objModelPlottingReviewer.rowsPerPage = int.Parse(ddlJumlahBaris.SelectedValue);

            if (!objModelPlottingReviewer.getDaftarPlottingAdm(IdSkema, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, tbCariProposal.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
                return;
            }

            lblJumlahProposal.Text = objModelPlottingReviewer.numOfRecords.ToString();

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvPlotting, objModelPlottingReviewer.currentRecords);

            mvPlotAdministrasi.SetActiveView(vPlotting);
        }

        //private void refreshPaging()
        //{
        //    objModelPlottingReviewer.currentPage = ktPagingProposal.currentPage;
        //    objModelPlottingReviewer.rowsPerPage = int.Parse(ddlJumlahBaris.SelectedValue);
        //    if (!objModelPlottingReviewer.getDaftarPlottingAdm(IdSkema, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
        //        ddlTahapan.SelectedValue, tbCariProposal.Text))
        //    {
        //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
        //        return;
        //    }

        //    listView = new UIControllers.uiListView();
        //    listView.bindToListView(ref gvPlotting, objModelPlottingReviewer.currentRecords);
        //}

        #endregion

        #region Proposal Reviewer

        //protected void refreshlvProposalReviewer()
        //{
        //    var dt = new DataTable();
        //    if (!objModel.getListPilihanReviewer(ref dt, IdTransaksiKegiatan, tbCariReviewer.Text))
        //    {
        //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
        //        return;
        //    }

        //    try
        //    {
        //        lvProposalReviewer.DataSource = dt;
        //        lvProposalReviewer.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
        //    }
        //}

        protected void lbKembaliKeReviewer_Click(object sender, EventArgs e)
        {
            //refreshlvReviewer();
            //mvPlotAdministrasi.SetActiveView(vDaftarReviewer);
        }

        protected void lbCariProposalReviewer_Click(object sender, EventArgs e)
        {

        }

        protected void btnClearCariProposalReviewer_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void isiDdlThnUsulan()
        {
            ddlThnUsulan.Items.Clear();
            ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void isiDdlThnPelaksanaan()
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem("--Pilih--", "0000"));
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
        }

        private void isiTahapanKegiatan()
        {
            Guid idInstitusi = new Guid("00000000-0000-0000-0000-000000000000");
            ddlTahapan.Items.Clear();
            DataTable Tahapan = new DataTable();
            modelPenugasanReviewer.daftar_tahapan_thn_pelaksanaan(ref Tahapan, idInstitusi.ToString(), ddlThnUsulan.SelectedValue);

            obj_uiDropdownlist.bindToDropDownList(ref ddlTahapan, Tahapan, "tahapan", "kd_tahapan_kegiatan");
            if (ddlTahapan.Items.Count == 0)
                ddlTahapan.Items.Add(new ListItem("--Pilih--", "0"));
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
            ddlThnPelaksanaan.SelectedIndex = 0;
            isiTahapanKegiatan();
            refreshlvDaftarSkema();
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshlvDaftarSkema();
        }

        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshlvDaftarSkema();
        }
    }
}