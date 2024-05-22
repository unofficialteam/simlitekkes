using OfficeOpenXml;
using simlitekkes.Core;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class plottingReviewerPT : System.Web.UI.UserControl
    {
        simlitekkes.Models.PT.plottingReviewerPT objModelPlottingReviewer = new simlitekkes.Models.PT.plottingReviewerPT();
        Models.PT.daftarPenugasanReviewerPT modelPenugasanReviewer = new Models.PT.daftarPenugasanReviewerPT();
        Models.Admin.daftarTahapanKegiatan objDaftarTahapan = new Models.Admin.daftarTahapanKegiatan();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiGridView obj_uiGv = new uiGridView();
        uiGridView obj_uiGvRev = new uiGridView();
        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        manipulasiData objManipData = new manipulasiData();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbExportExcel);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvDaftarReviewer);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvBebanReviewer);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvSkemaNJmlReviewer);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvPlotting);

            if (!IsPostBack)
            {
                isiDdlThnUsulan();
                isiDdlThnPelaksanaan();

                if (ddlTahapan.Items.Count <= 1)
                    isiTahapanKegiatan();

                mvDaftarPlotting.SetActiveView(vDaftarSkema);
                isiDataDaftarSkemaNJmlReviewer();
                gvSkemaNJmlReviewer.Enabled = false;
                if (Session["kd_klaster"] != null)
                {
                    string kd_klaster = Session["kd_klaster"].ToString();
                    switch (kd_klaster)
                    {
                        case "D":
                        case "E":
                        case "F":
                            gvSkemaNJmlReviewer.Enabled = true;
                            break;
                        case "G":
                            gvSkemaNJmlReviewer.Visible = false;
                            break;
                        default:
                            gvSkemaNJmlReviewer.Visible = false;
                            break;
                    }
                }
                else
                {
                    gvSkemaNJmlReviewer.Visible = true;
                    gvSkemaNJmlReviewer.Enabled = true;
                }
            }
        }

        protected void menu_evt_paging(object sender, EventArgs e)
        {
            objModelPlottingReviewer.currentPage = ktPaging.currentPage;
            objModelPlottingReviewer.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            DataTable dt = new DataTable();
            int opset = int.Parse(ddlJmlBaris.SelectedValue) * objModelPlottingReviewer.currentPage;
            objModelPlottingReviewer.getListPlottingReviewer(ref dt, objLogin.idInstitusi.ToString(), ViewState["id_skema"].ToString(), ddlThnUsulan.SelectedValue,
                ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, ddlJmlBaris.SelectedValue, opset.ToString());

            string[] namaKolom = new string[] {
                    "nomor_baris","id_usulan_kegiatan", "id_transaksi_kegiatan", "kd_perguruan_tinggi", "nama_institusi"
                    , "judul", "bidang_fokus", "nama_reviewer_1", "nama_reviewer_2", "jml_total_data" };
            obj_uiGv.bindToGridView(ref gvPlotting, dt, namaKolom);
        }

        protected void menu_paging_evt(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["id_skema"].ToString());
            Guid idTransaksiKegiatan = Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString());
            string nama = tbnama.Text;

            objModelPlottingReviewer.currentPage = ControlPaging1.currentPage;
            objModelPlottingReviewer.rowsPerPage = int.Parse(ddljmlbarisrev.SelectedValue);

            if (!objModelPlottingReviewer.getDaftarReviewerDitugaskan(idSkema, ddlThnUsulan.SelectedValue,
                ddlTahapan.SelectedValue, objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue, idTransaksiKegiatan, nama))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGvRev = new UIControllers.uiGridView();
            obj_uiGvRev.bindToGridView(ref gvDaftarReviewer, objModelPlottingReviewer.currentRecords);
        }

        protected void menu_paging_beban_rev(object sender, EventArgs e)
        {
            objModelPlottingReviewer.currentPage = ktPagingBebanRev.currentPage;
            objModelPlottingReviewer.rowsPerPage = 10;// int.Parse(ddljmlbarisrev.SelectedValue);
            int opset = 10 * objModelPlottingReviewer.currentPage;
            DataTable dt = new DataTable();

            objModelPlottingReviewer.getListBebanReviewer(ref dt,
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, objLogin.idInstitusi, "1", "10", opset.ToString(), "");

            gvBebanReviewer.DataSource = dt;
            gvBebanReviewer.DataBind();
        }

        private void isiDdlThnUsulan()
        {
            ddlThnUsulan.Items.Clear();
            //ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2019; i--)
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
            ddlThnPelaksanaan.SelectedIndex = 1;
        }

        private void isiTahapanKegiatan()
        {
            ddlTahapan.Items.Clear();
            DataTable Tahapan = new DataTable();
            modelPenugasanReviewer.daftar_tahapan_thn_pelaksanaan(ref Tahapan, objLogin.idInstitusi.ToString(), ddlThnUsulan.SelectedValue);
            ddlTahapan.Items.Add(new ListItem("--Pilih--", "0"));
            obj_uiDropdownlist.bindToDropDownList(ref ddlTahapan, Tahapan, "tahapan", "kd_tahapan_kegiatan");
        }

        private void isiDataDaftarSkemaNJmlReviewer()
        {
            objLogin = (Models.login)Session["objLogin"];

            if (ddlTahapan.SelectedValue == "20")
            {
                gvSkemaNJmlReviewer.Columns[3].Visible = false;

            }
            else
            {
                gvSkemaNJmlReviewer.Columns[3].Visible = true;
            }

            DataTable dt = new DataTable();
            if (ddlProgram.SelectedValue == "1")

                objModelPlottingReviewer.getListSkemaNJmlReviewer(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                    objLogin.idInstitusi, "1", ddlTahapan.SelectedValue);
            else
                objModelPlottingReviewer.getListPlottingReviewerPengabdian(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                    objLogin.idInstitusi, ddlTahapan.SelectedValue);

            gvSkemaNJmlReviewer.DataSource = dt;
            gvSkemaNJmlReviewer.DataBind();
        }

        private void setListDataPloting(int offset)
        {
            DataTable dt = new DataTable();
            //objModelPlottingReviewer.getListPlottingReviewer(ref dt, objLogin.idInstitusi.ToString(), ViewState["id_skema"].ToString(), ddlThnUsulan.SelectedValue,
            //    ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, ddlJmlBaris.SelectedValue, offset.ToString());

            if (objModelPlottingReviewer.getListPlottingReviewer(ref dt, objLogin.idInstitusi.ToString(), ViewState["id_skema"].ToString(), ddlThnUsulan.SelectedValue,
                ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, ddlJmlBaris.SelectedValue, offset.ToString()))
            {
                if (dt.Rows.Count > 0)
                {
                    ViewState["jml_total_data"] = dt.Rows[0]["jml_total_data"].ToString();
                    ktPaging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), int.Parse(ViewState["jml_total_data"].ToString()));
                    lblJmlUsulan.Text = ViewState["jml_total_data"].ToString();

                    if (ddlTahapan.SelectedValue == "32" || ddlTahapan.SelectedValue == "16" ||
                            ddlTahapan.SelectedValue == "20")
                    {
                        gvPlotting.Columns[3].Visible = false;
                    }
                    else
                    {
                        gvPlotting.Columns[3].Visible = true;
                    }
                }
                else
                {
                    ViewState.Remove("jml_total_data");
                    lblJmlUsulan.Text = "0";

                    if (ddlTahapan.SelectedValue == "32" || ddlTahapan.SelectedValue == "16" ||
                            ddlTahapan.SelectedValue == "20")
                    {
                        gvPlotting.Columns[3].Visible = false;
                    }
                    else
                    {
                        gvPlotting.Columns[3].Visible = true;
                    }
                }
                gvPlotting.DataSource = dt;
                gvPlotting.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelPlottingReviewer.errorMessage);
            }

            DataTable dtJumlah = new DataTable();
            objModelPlottingReviewer.getJmlPlottingReviewer(ref dtJumlah, objLogin.idInstitusi.ToString(), ViewState["id_skema"].ToString(), ddlThnUsulan.SelectedValue,
                ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue);
            if (dtJumlah.Rows.Count > 0)
            {
                if (ddlTahapan.SelectedValue == "32" || ddlTahapan.SelectedValue == "16" ||
                            ddlTahapan.SelectedValue == "20")
                {
                    lblJudulJmlRev2.Visible = false;
                    lblJmlRev2.Visible = false;

                    lblJmlRev1.Text = dtJumlah.Rows[0]["JmlRev1"].ToString();
                    lblJmlRev2.Text = dtJumlah.Rows[0]["JmlRev2"].ToString();
                }
                else
                {
                    lblJudulJmlRev2.Visible = true;
                    lblJmlRev2.Visible = true;

                    lblJmlRev1.Text = dtJumlah.Rows[0]["JmlRev1"].ToString();
                    lblJmlRev2.Text = dtJumlah.Rows[0]["JmlRev2"].ToString();
                }
            }
            else
            {
                if (ddlTahapan.SelectedValue == "32" || ddlTahapan.SelectedValue == "16" ||
                            ddlTahapan.SelectedValue == "20")
                {
                    lblJudulJmlRev2.Visible = false;
                    lblJmlRev2.Visible = false;

                    lblJmlRev1.Text = "0";
                    lblJmlRev2.Text = "0";
                }
                else
                {
                    lblJudulJmlRev2.Visible = true;
                    lblJmlRev2.Visible = true;

                    lblJmlRev1.Text = "0";
                    lblJmlRev2.Text = "0";
                }
            }
        }

        protected void gvPlotting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int indek = int.Parse(e.CommandArgument.ToString());
            string id_transaksi_kegiatan = gvPlotting.DataKeys[indek]["id_transaksi_kegiatan"].ToString();
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            Label lblJudul1 = (Label)row.FindControl("lblJudul");
            Label lblInstitusi1 = (Label)row.FindControl("lblInstitusi");
            Label lblBidFokus = (Label)row.FindControl("lblBidFokus");
            if (e.CommandName == "tambah_rev1")
            {
                lblJudul.Text = lblJudul1.Text;
                lblNamaPt.Text = lblInstitusi1.Text;
                lblBidangFokus.Text = lblBidFokus.Text;
                ViewState["id_transaksi_kegiatan"] = id_transaksi_kegiatan;
                ViewState["no_urut"] = 1;
                mvDaftarPlotting.SetActiveView(viewDaftarReviewer);
                isiGvDaftarReviewerDitugaskan(0);
            }
            else if (e.CommandName == "tambah_rev2")
            {
                lblJudul.Text = lblJudul1.Text;
                lblNamaPt.Text = lblInstitusi1.Text;
                lblBidangFokus.Text = lblBidFokus.Text;
                ViewState["id_transaksi_kegiatan"] = id_transaksi_kegiatan;
                ViewState["no_urut"] = 2;
                mvDaftarPlotting.SetActiveView(viewDaftarReviewer);
                isiGvDaftarReviewerDitugaskan(0);
            }
            else if (e.CommandName == "hapus_rev1")
            {
                ViewState["id_transaksi_kegiatan"] = id_transaksi_kegiatan;
                ViewState["no_urut"] = 1;
                uiMdl.ShowModal(this.Page, "modalHapus");
            }
            else if (e.CommandName == "hapus_rev2")
            {
                ViewState["id_transaksi_kegiatan"] = id_transaksi_kegiatan;
                ViewState["no_urut"] = 2;
                uiMdl.ShowModal(this.Page, "modalHapus");
            }
        }

        protected void gvPlotting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNamaReviewer1 = (Label)e.Row.FindControl("lblNamaReviewer1");
                Label lblNamaReviewer2 = (Label)e.Row.FindControl("lblNamaReviewer2");
                LinkButton lbTambahReviewer1 = (LinkButton)e.Row.FindControl("lbTambahReviewer1");
                LinkButton lbTambahReviewer2 = (LinkButton)e.Row.FindControl("lbTambahReviewer2");
                LinkButton lbDelReviewer1 = (LinkButton)e.Row.FindControl("lbDelReviewer1");
                LinkButton lbDelReviewer2 = (LinkButton)e.Row.FindControl("lbDelReviewer2");
                lbTambahReviewer1.Visible = false;
                lbTambahReviewer2.Visible = false;
                lbDelReviewer1.Visible = false;
                lbDelReviewer2.Visible = false;
                Boolean status = false;

                string kd_klaster = drv["kd_klaster"].ToString();

                DataTable dtcek = new DataTable();
                if (objModelPlottingReviewer.cekjadwal(ref dtcek, ViewState["id_skema"].ToString(), ddlTahapan.SelectedValue))
                {
                    if (dtcek.Rows.Count > 0)
                    {
                        status = true;
                    }
                    else
                    {
                        Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());
                        if (objModelPlottingReviewer.getDaftarWhitelist(id_institusi, int.Parse(ViewState["id_skema"].ToString()),
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

                        //if (ddlProgram.SelectedValue == "1"  && kd_klaster == "3")
                        //{
                        //    lbTambahReviewer1.Visible = false;
                        //}
                    }
                    else
                    {
                        lbDelReviewer1.Visible = true;
                        lbDelReviewer1.Enabled = status;

                        //if (ddlProgram.SelectedValue == "1" && kd_klaster == "3")
                        //{
                        //    lbDelReviewer1.Visible = false;
                        //}
                    }
                    if (lblNamaReviewer2.Text.Trim() == "")
                    {
                        lbTambahReviewer2.Visible = true;
                        lbTambahReviewer2.Enabled = status;

                        //if (ddlProgram.SelectedValue == "1" && kd_klaster == "3")
                        //{
                        //    lbTambahReviewer2.Visible = false;
                        //}

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

                        //if (ddlProgram.SelectedValue == "1" && kd_klaster == "3")
                        //{
                        //    lbDelReviewer2.Visible = false;
                        //}

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

        protected void gvDaftarReviewer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dtcek = new DataTable();
            DataTable dtinsert = new DataTable();
            int indek = int.Parse(e.CommandArgument.ToString());
            string id_reviewer = gvDaftarReviewer.DataKeys[indek]["id_reviewer"].ToString();
            string id_penugasan_reviewer = gvDaftarReviewer.DataKeys[indek]["id_penugasan_reviewer"].ToString();
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            if (e.CommandName == "simpan_rev")
            {
                if (objModelPlottingReviewer.get_cek_plotting_edisi_xi(ref dtcek, ViewState["id_transaksi_kegiatan"].ToString(), id_reviewer, ViewState["no_urut"].ToString()))
                {
                    if (dtcek.Rows.Count > 0)
                    {
                        if (dtcek.Rows[0]["status"].ToString() == "1")
                        {
                            DataTable dtCekJmlPlotting = new DataTable();
                            if (objModelPlottingReviewer.cekJmlPlotting(ref dtCekJmlPlotting, Guid.Parse(id_reviewer),
                                ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue,
                                ddlThnPelaksanaan.SelectedValue, ddlProgram.SelectedValue))
                            {
                                if (dtCekJmlPlotting.Rows.Count > 0)
                                {
                                    if (dtCekJmlPlotting.Rows[0]["kd_sts_memenuhi"].ToString() == "1")
                                    {
                                        if (objModelPlottingReviewer.insertDataPlottingReviewer(ref dtinsert,
                        Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString()),
                        Guid.Parse(id_reviewer), int.Parse(ViewState["no_urut"].ToString()),
                        Guid.Parse(id_penugasan_reviewer.ToString())
                        ))
                                        {
                                            if (dtinsert.Rows[0]["status"].ToString() == "1")
                                            {
                                                mvDaftarPlotting.SetActiveView(viewDaftarPlotting);
                                                setListDataPloting(ktPaging.currentPage * int.Parse(ddlJmlBaris.SelectedValue));
                                            }
                                            else
                                            {
                                                noty.Notify(this.Page, uiNotify.NotifyType.error, "ERROR", dtinsert.Rows[0]["pesan"].ToString());
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Perhatian", "Reviewer sudah diplotting sebanyak " + dtCekJmlPlotting.Rows[0]["jml_proposal_diplotting"].ToString() + ", Jumlah plotting maksimal 30 judul");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "ERROR", dtcek.Rows[0]["pesan"].ToString());
                    }
                }
            }
        }

        protected void gvDaftarReviewer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNo = (Label)e.Row.FindControl("lblNo");
                lblNo.Text = (e.Row.RowIndex + 1 + int.Parse(ddljmlbarisrev.SelectedValue) * (ControlPaging1.currentPage)).ToString();
            }
        }

        private void isiGvDaftarReviewerDitugaskan(int idxPage)
        {
            int idSkema = int.Parse(ViewState["id_skema"].ToString());
            Guid idTransaksiKegiatan = Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString());
            string nama = tbnama.Text;
            objModelPlottingReviewer.currentPage = idxPage;
            objModelPlottingReviewer.rowsPerPage = int.Parse(ddljmlbarisrev.SelectedValue);

            if (!objModelPlottingReviewer.getJmlDaftarReviewerDitugaskan(idSkema, ddlThnUsulan.SelectedValue,
                ddlTahapan.SelectedValue, objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue, idTransaksiKegiatan, nama))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objModelPlottingReviewer.errorMessage);
            lblJmlRecords.Text = objModelPlottingReviewer.numOfRecords.ToString();

            //NEW PAGING CONTROL

            ControlPaging1.currentPage = idxPage;
            ControlPaging1.setPaging(int.Parse(ddljmlbarisrev.SelectedValue), objModelPlottingReviewer.numOfRecords);
            if (!objModelPlottingReviewer.getDaftarReviewerDitugaskan(idSkema, ddlThnUsulan.SelectedValue,
                ddlTahapan.SelectedValue, objLogin.idInstitusi, ddlThnPelaksanaan.SelectedValue, idTransaksiKegiatan, nama))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objModelPlottingReviewer.errorMessage);
                return;
            }

            if (!obj_uiGvRev.bindToGridView(ref gvDaftarReviewer, objModelPlottingReviewer.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objModelPlottingReviewer.errorMessage);

            if (objModelPlottingReviewer.numOfRecords < 1)
            {
                ControlPaging1.setPaging(int.Parse(ddljmlbarisrev.SelectedValue), 1);
            }
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (objModelPlottingReviewer.deleteDataPlotting(Guid.Parse(ViewState["id_transaksi_kegiatan"].ToString()),
                int.Parse(ViewState["no_urut"].ToString())))
            {
                setListDataPloting(ktPaging.currentPage * int.Parse(ddlJmlBaris.SelectedValue));
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Reviewer tidak bisa dihapus karena sudah melakukan penilaian");
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            mvDaftarPlotting.SetActiveView(viewDaftarPlotting);
        }

        protected void lbcari_Click(object sender, EventArgs e)
        {
            isiGvDaftarReviewerDitugaskan(0);
        }

        protected void ddljmlbarisrev_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarReviewerDitugaskan(0);
        }

        protected void lbExportExcel_Click(object sender, EventArgs e)
        {
            string id_institusi = objLogin.idInstitusi.ToString();
            string id_skema = ViewState["id_skema"].ToString();
            string thn_usulan_kegiatan = ddlThnUsulan.SelectedValue;
            string thn_pelaksanaan_kegiatan = ddlThnPelaksanaan.SelectedValue;
            string kd_tahapan_kegiatan = ddlTahapan.SelectedValue;
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("plotting " + ddlTahapan.SelectedItem.Text + " tahun {0}.xlsx", ddlThnPelaksanaan.SelectedItem.Text);
                DataTable dt = new DataTable();
                if (objModelPlottingReviewer.getExportExcel(ref dt, id_institusi, id_skema, thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, kd_tahapan_kegiatan))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("plotting");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", objModelPlottingReviewer.errorMessage);
                    return;
                }

                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pck.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
                pck.Dispose();
            }
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiDdlThnPelaksanaan();
            isiTahapanKegiatan();
            isiDataDaftarSkemaNJmlReviewer();
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiTahapanKegiatan();
            isiDataDaftarSkemaNJmlReviewer();
        }

        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiDataDaftarSkemaNJmlReviewer();
        }

        protected void ddlSkemaKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiDataDaftarSkemaNJmlReviewer();
        }

        protected void lbExcellSkema_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("skema usulan penelitian " + ddlTahapan.SelectedItem.Text + " tahun {0}.xlsx", ddlThnPelaksanaan.SelectedItem.Text);
                var dt = new DataTable();
                if (objModelPlottingReviewer.getListSkemaNJmlReviewerExcel(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                objLogin.idInstitusi, "1", ddlTahapan.SelectedValue))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("plotting");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", objModelPlottingReviewer.errorMessage);
                    return;
                }

                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //workbook.SaveAs(memoryStream);
                    pck.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
                pck.Dispose();
            }
        }

        protected void gvSkemaNJmlReviewer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "tambah_reviewer")
            {
                int indek = int.Parse(e.CommandArgument.ToString());
                string id_skema = gvSkemaNJmlReviewer.DataKeys[indek]["id_skema"].ToString();
                ViewState["id_skema"] = id_skema;
                string nama_skema = gvSkemaNJmlReviewer.DataKeys[indek]["nama_skema"].ToString();
                mvDaftarPlotting.SetActiveView(viewDaftarPlotting);
                lblThnUsulan.Text = ddlThnUsulan.SelectedValue;
                lblThnUsulan2.Text = ddlThnUsulan.SelectedValue;
                lblTahapan.Text = ddlTahapan.SelectedItem.Text;
                lblNamaSkemaNTahun.Text = nama_skema + " - Pelaksanaan " + ddlThnPelaksanaan.SelectedValue;
                lblTahapan2.Text = ddlTahapan.SelectedItem.Text;
                lblNamaSkemaNTahun2.Text = nama_skema + " - Pelaksanaan " + ddlThnPelaksanaan.SelectedValue;
                setListDataPloting(0);
            }
        }

        protected void lbKembali_Click(object sender, EventArgs e)
        {
            mvDaftarPlotting.SetActiveView(vDaftarSkema);
            isiDataDaftarSkemaNJmlReviewer();
        }

        protected void lbKembali2_Click(object sender, EventArgs e)
        {
            mvDaftarPlotting.SetActiveView(viewDaftarPlotting);
        }

        protected void lbBebanReviewer_Click(object sender, EventArgs e)
        {
            lblThnUsulan3.Text = ddlThnUsulan.SelectedValue;
            lblNamaSkemaNTahun3.Text = "Sebaran Beban Reviewer - Pelaksanaan " + ddlThnPelaksanaan.SelectedValue;
            mvDaftarPlotting.SetActiveView(vBebanReviewer);
            isiGvBebanReviewer(0);
        }

        private void isiGvBebanReviewer(int offset)
        {
            DataTable dt = new DataTable();
            objModelPlottingReviewer.getListBebanReviewer(ref dt,
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, objLogin.idInstitusi, "1", "10", "0", "");
            if (dt.Rows.Count > 0)
            {
                int totalBaris = int.Parse(dt.Rows[0]["jml_total_data"].ToString());
                ktPagingBebanRev.setPaging(10, totalBaris);
            }
            else
            {
                ktPagingBebanRev.setPaging(10, 0);
            }
            gvBebanReviewer.DataSource = dt;
            gvBebanReviewer.DataBind();
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            setListDataPloting(0);
        }

        protected void lbExportExcelBeban_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Beban Reviewer " + ddlTahapan.SelectedItem.Text + " tahun {0}.xlsx", ddlThnPelaksanaan.SelectedItem.Text);
                DataTable dt = new DataTable();
                if (objModelPlottingReviewer.getExcelBebanReviewer(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                    ddlTahapan.SelectedValue, objLogin.idInstitusi, "1", "100", "0", ""))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("beban_reviewer");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", objModelPlottingReviewer.errorMessage);
                    return;
                }

                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pck.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
                pck.Dispose();
            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiDataDaftarSkemaNJmlReviewer();
        }
    }
}