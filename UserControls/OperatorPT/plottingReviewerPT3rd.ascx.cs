using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class plottingReviewerPT3rd : System.Web.UI.UserControl
    {
        Models.PT.daftarPlottingReviewerPT3rd modelPlottingReviewer3rd = new Models.PT.daftarPlottingReviewerPT3rd();
        Models.PT.daftarPenugasanReviewerPT modelPenugasanReviewer = new Models.PT.daftarPenugasanReviewerPT();
        Models.PT.plottingReviewerPT modelPlottingReviewer = new Models.PT.plottingReviewerPT();

        UIControllers.uiGridView obj_uiGridView = new UIControllers.uiGridView();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        UIControllers.uiDropdownList obj_uiDropdownlist = new UIControllers.uiDropdownList();
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        Models.login objLogin;

        string[] namaKolom = { "no_baris", "id_usulan_kegiatan", "id_transaksi_kegiatan", "nama_ketua", "judul",
            "bidang_fokus", "id_plotting_reviewer1", "kd_sts_permanen_reviewer1", "reviewer1", "nilai_reviewer1",
            "id_plotting_reviewer2", "kd_sts_permanen_reviewer2", "reviewer2", "nilai_reviewer2", "deviasi_nilai",
            "id_plotting_reviewer3", "kd_sts_permanen_reviewer3", "reviewer3", "nilai_reviewer3", "id_reviewer3" };

        string[] namaKolomKandidatReviewer3 = { "id_reviewer", "nama_reviewer", "nama_institusi", "id_penugasan_reviewer" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvDaftarPlottingReviewerPT3rd);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvKandidatReviewer3);

            if (!IsPostBack)
            {
                isiDdlThnUsulan();
                isiDdlThnPelaksanaan();

                if (ddlTahapan.Items.Count <= 1)
                    isiTahapanKegiatan();

                if (ddlProgram.Items.Count <= 1)
                    isiProgram();

                if (ddlSkemaKegiatan.Items.Count <= 1)
                    isiSkema();

                //if (objLogin.idPeran == 6 && modelPenugasanReviewer.cekApakahKlasterNonBinaanDanPtn(id_institusi) == true)
                //{
                //    //refreshGvPlottingReviewerPT3rd(0);
                //    mvPlottingReviewerPT3rd.SetActiveView(viewDaftarPlottingReviewerPT3rd);
                //}
                //else
                //{
                //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Plotting Reviewer 3 tidak diijinkan diakses");
                //}
            }
        }

        private void isiDdlThnUsulan()
        {
            ddlThnUsulan.Items.Clear();
            ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2013; i--)
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
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
        }

        private void isiTahapanKegiatan()
        {
            if (Application["TahapanKegiatan"] != null)
            {
                DataTable TahapanKegiatan = objManipData.filterData((DataTable)Application["TahapanKegiatan"], "kd_tahapan_kegiatan IN ('22', '23', '32') ");
                if (!obj_uiDropdownlist.bindToDropDownList(ref ddlTahapan, TahapanKegiatan, "tahapan", "kd_tahapan_kegiatan"))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + obj_uiDropdownlist.errorMessage);
                    return;
                }
            }
        }

        private void isiProgram()
        {
            //if (ddlTahapan.SelectedValue == "32")
            //{
            //    DataTable dtMonevInternal = objManipData.filterData((DataTable)Application["ProgramHibah"], "kd_program_hibah IN ('1','2','4') ");
            //    ddlProgram.Items.Add(new ListItem("--Pilih--", "0"));
            //    obj_uiDropdownlist.bindToDropDownList(ref ddlProgram, dtMonevInternal, "program_hibah", "kd_program_hibah");
            //    ddlProgram.SelectedIndex = 0;
            //}
            //else
            //{
            //    DataTable dt = objManipData.filterData((DataTable)Application["ProgramHibah"], "kd_program_hibah = '1' ");
            //    ddlProgram.Items.Add(new ListItem("--Pilih--", "0"));
            //    obj_uiDropdownlist.bindToDropDownList(ref ddlProgram, dt, "program_hibah", "kd_program_hibah");
            //    ddlProgram.SelectedIndex = 0;
            //}

            ddlProgram.Items.Clear();
            DataTable ProgramHibah = new DataTable();
            modelPenugasanReviewer.daftar_program_hibah_thn_pelaksanaan(ref ProgramHibah, objLogin.idInstitusi.ToString(), ddlTahapan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            ddlProgram.Items.Add(new ListItem("--Pilih--", "0"));
            obj_uiDropdownlist.bindToDropDownList(ref ddlProgram, ProgramHibah, "program_hibah", "kd_program_hibah");
            ddlProgram.SelectedIndex = 0;
        }

        private void isiSkema()
        {
            //if (Application["SkemaKegiatan"] != null)
            //{
            //    // Isi skema kegiatan
            //    DataTable dt = new DataTable();
            //    modelPlottingReviewer3rd.daftar_skema_thn_pelaksanaan(ref dt, objLogin.idInstitusi.ToString(), ddlThnPelaksanaan.SelectedValue);
            //    obj_uiDropdownlist.bindToDropDownList(ref ddlSkemaKegiatan, dt, "nama_skema", "id_skema");
            //}

            ddlSkemaKegiatan.Items.Clear();
            DataTable SkemaKegiatan = new DataTable();
            modelPenugasanReviewer.daftar_tahapan_skema_by_thn_pelaksanaan(ref SkemaKegiatan, objLogin.idInstitusi.ToString(), ddlProgram.SelectedValue, ddlTahapan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            ddlSkemaKegiatan.Items.Add(new ListItem("--Pilih--", "0"));
            obj_uiDropdownlist.bindToDropDownList(ref ddlSkemaKegiatan, SkemaKegiatan, "nama_skema", "id_skema");
            ddlSkemaKegiatan.SelectedIndex = 0;
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiDdlThnPelaksanaan();
            isiTahapanKegiatan();
            isiProgram();
            isiSkema();

            if (mvPlottingReviewerPT3rd.ActiveViewIndex == 0)
                refreshGvPlottingReviewerPT3rd(0);
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiTahapanKegiatan();
            isiProgram();
            isiSkema();

            if (mvPlottingReviewerPT3rd.ActiveViewIndex == 0)
                refreshGvPlottingReviewerPT3rd(0);
        }

        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiProgram();
            isiSkema();

            if (mvPlottingReviewerPT3rd.ActiveViewIndex == 0)
                refreshGvPlottingReviewerPT3rd(0);
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Application["SkemaKegiatan"] != null)
            {
                isiSkema();
                ddlSkemaKegiatan.SelectedIndex = 0;
            }
            if (mvPlottingReviewerPT3rd.ActiveViewIndex == 0)
                refreshGvPlottingReviewerPT3rd(0);
        }

        protected void ddlSkemaKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mvPlottingReviewerPT3rd.ActiveViewIndex == 0)
                refreshGvPlottingReviewerPT3rd(0);
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGvPlottingReviewerPT3rd(0);
        }

        protected void Paging_PageChanging(object sender, EventArgs e)
        {
            int id_skema = int.Parse(ddlSkemaKegiatan.SelectedValue);
            string thn_usulan_kegiatan = ddlThnUsulan.SelectedValue;
            string thn_pelaksanaan_kegiatan = ddlThnPelaksanaan.SelectedValue;
            string kd_tahapan_kegiatan = ddlTahapan.SelectedValue;
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());


            modelPlottingReviewer3rd.currentPage = PagingPlottingReviewerPT3rd.currentPage;
            modelPlottingReviewer3rd.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            if (!modelPlottingReviewer3rd.getCurrRecords(id_skema, thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, kd_tahapan_kegiatan, id_institusi))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarPlottingReviewerPT3rd, modelPlottingReviewer3rd.currentRecords, namaKolom);
        }

        private void refreshGvPlottingReviewerPT3rd(int idxPage)
        {
            int id_skema = int.Parse(ddlSkemaKegiatan.SelectedValue);
            string thn_usulan_kegiatan = ddlThnUsulan.SelectedValue;
            string thn_pelaksanaan_kegiatan = ddlThnPelaksanaan.SelectedValue;
            string kd_tahapan_kegiatan = ddlTahapan.SelectedValue;
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());

            if (!modelPlottingReviewer3rd.getJmlData(id_skema, thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, kd_tahapan_kegiatan, id_institusi))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPlottingReviewer3rd.errorMessage);

            //NEW PAGING CONTROL
            PagingPlottingReviewerPT3rd.currentPage = 0;
            PagingPlottingReviewerPT3rd.setPaging(int.Parse(ddlJmlBaris.SelectedValue), modelPlottingReviewer3rd.numOfRecords);

            modelPlottingReviewer3rd.currentPage = idxPage;
            modelPlottingReviewer3rd.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            if (!modelPlottingReviewer3rd.getCurrRecords(id_skema, thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, kd_tahapan_kegiatan, id_institusi))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPlottingReviewer3rd.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvDaftarPlottingReviewerPT3rd, modelPlottingReviewer3rd.currentRecords, namaKolom))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPlottingReviewer3rd.errorMessage);

            lblJmlRecords.Text = modelPlottingReviewer3rd.numOfRecords.ToString();

            if (modelPlottingReviewer3rd.numOfRecords < 1)
            {
                PagingPlottingReviewerPT3rd.setPaging(int.Parse(ddlJmlBaris.SelectedValue), 1);
            }
        }

        protected void gvDaftarPlottingReviewerPT3rd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["id_transaksi_kegiatan"] = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["id_transaksi_kegiatan"].ToString();
            string id_plotting_reviewer_3 = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["id_plotting_reviewer3"].ToString();

            if (id_plotting_reviewer_3 != "")
            {
                uiMdl.ShowModal(this.Page, "modalHapus");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + "Reviewer ke 3 belum diplotting");
            }
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            Guid id_transaksi_kegiatan = Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString());
            int no_urut = int.Parse("3");

            if (modelPlottingReviewer.deleteDataPlotting(id_transaksi_kegiatan, no_urut))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
                refreshGvPlottingReviewerPT3rd(0);

            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal " + "usulan sudah dinilai oleh reviewer ke 3");
                refreshGvPlottingReviewerPT3rd(0);
            }
        }

        protected void gvDaftarPlottingReviewerPT3rd_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string kd_sts_permanen_rev_1 = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["kd_sts_permanen_reviewer1"].ToString();
            string kd_sts_permanen_rev_2 = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["kd_sts_permanen_reviewer2"].ToString();

            if (kd_sts_permanen_rev_1 == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + "Reviewer 1 belum disimpan permanen");
            }
            else if (kd_sts_permanen_rev_2 == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + "Reviewer 2 belum disimpan permanen");
            }
            else
            {
                lblNamaSkim.Text = ddlSkemaKegiatan.SelectedItem.Text;
                ViewState["id_transaksi_kegiatan"] = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["id_transaksi_kegiatan"].ToString();
                ViewState["id_plotting_reviewer3"] = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["id_plotting_reviewer3"].ToString();

                string id_reviewer3 = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["id_reviewer3"].ToString();
                ViewState["id_reviewer3"] = id_reviewer3;

                lblEditNamaKetua.Text = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["nama_ketua"].ToString();
                lblEditBidangFokus.Text = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["bidang_fokus"].ToString();
                lblEditJudul.Text = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["judul"].ToString();

                lblSkorRev1.Text = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["nilai_reviewer1"].ToString();
                lblNamaRev1.Text = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["reviewer1"].ToString();
                lblSkorRev2.Text = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["nilai_reviewer2"].ToString();
                lblNamaRev2.Text = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["reviewer2"].ToString();
                lblEditBedaNilai.Text = gvDaftarPlottingReviewerPT3rd.DataKeys[e.RowIndex]["deviasi_nilai"].ToString();

                refreshGvKandidatReviewer3rd();

                uiMdl.ShowModal(this.Page, "modalTambahReviewer3rd");
            }
        }

        private void refreshGvKandidatReviewer3rd()
        {
            int id_skema = int.Parse(ddlSkemaKegiatan.SelectedValue);
            string thn_usulan_kegiatan = ddlThnUsulan.SelectedValue;
            string thn_pelaksanaan_kegiatan = ddlThnPelaksanaan.SelectedValue;
            string kd_tahapan_kegiatan = ddlTahapan.SelectedValue;
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());
            Guid id_transaksi_kegiatan = Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString());

            string id_plotting_reviewer_3 = ViewState["id_plotting_reviewer3"].ToString();
            string id_reviewer_3 = ViewState["id_reviewer3"].ToString();


            if (id_plotting_reviewer_3 == "")
            {
                if (!modelPlottingReviewer3rd.getReviewer3(id_skema, thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, id_institusi, kd_tahapan_kegiatan, id_transaksi_kegiatan))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPlottingReviewer3rd.errorMessage);
                    return;
                }

                if (!obj_uiGridView.bindToGridView(ref gvKandidatReviewer3, modelPlottingReviewer3rd.currentRecords, namaKolomKandidatReviewer3))
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPlottingReviewer3rd.errorMessage);
            }
            if (id_plotting_reviewer_3 != "")
            {
                if (!modelPlottingReviewer3rd.getFilterReviewer3(id_skema, thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, id_institusi, kd_tahapan_kegiatan, id_transaksi_kegiatan, id_reviewer_3))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPlottingReviewer3rd.errorMessage);
                    return;
                }

                if (!obj_uiGridView.bindToGridView(ref gvKandidatReviewer3, modelPlottingReviewer3rd.currentRecords, namaKolomKandidatReviewer3))
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPlottingReviewer3rd.errorMessage);
            }
        }

        protected void gvKandidatReviewer3_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id_plotting_reviewer_3 = ViewState["id_plotting_reviewer3"].ToString();

            Guid id_transaksi_kegiatan = Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString());
            Guid id_reviewer = Guid.Parse(gvKandidatReviewer3.DataKeys[e.RowIndex]["id_reviewer"].ToString());
            int no_urut = int.Parse("3");
            Guid id_penugasan_reviewer = Guid.Parse(gvKandidatReviewer3.DataKeys[e.RowIndex]["id_penugasan_reviewer"].ToString());

            if (id_plotting_reviewer_3 == "")
            {
                DataTable dtInsert = new DataTable();

                if (modelPlottingReviewer.insertDataPlottingReviewer(ref dtInsert, id_transaksi_kegiatan, id_reviewer, no_urut, id_penugasan_reviewer))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Reviewer berhasil diplotting");
                    refreshGvPlottingReviewerPT3rd(0);
                }
                else
                {
                    mvPlottingReviewerPT3rd.SetActiveView(viewDaftarPlottingReviewerPT3rd);
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal" + modelPlottingReviewer.errorMessage);
                    refreshGvPlottingReviewerPT3rd(0);
                }
            }
            else
            {
                mvPlottingReviewerPT3rd.SetActiveView(viewDaftarPlottingReviewerPT3rd);
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Reviewer sudah pernah diplotting silahkan hapus dahulu");
                refreshGvKandidatReviewer3rd();
            }
        }

        protected void gvDaftarPlottingReviewerPT3rd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                LinkButton lbPlotting = (LinkButton)e.Row.FindControl("lbPlotting");
                LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());

                if (modelPenugasanReviewer.cekJadwal(ddlTahapan.SelectedValue, int.Parse(ddlSkemaKegiatan.SelectedValue)) == true ||
                    modelPenugasanReviewer.getDaftarWhitelist(id_institusi, int.Parse(ddlSkemaKegiatan.SelectedValue),
                    ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue) == true)
                {
                    lbPlotting.Enabled = true;
                    lbDelete.Enabled = true;
                }
                else
                {
                    lbPlotting.CssClass = "btn btn-block btn-primary disabled";
                    lbDelete.CssClass = "btn btn-block btn-danger disabled";
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + " Bukan dalam masa evaluasi");
                }
            }
        }
    }
}