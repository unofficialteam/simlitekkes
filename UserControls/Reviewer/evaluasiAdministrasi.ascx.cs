using Npgsql;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Reviewer
{
    public partial class evaluasiAdministrasi : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Reviewer.evaluasiAdministrasi objEvaluasi = new Models.Reviewer.evaluasiAdministrasi();
        uiGridView obj_uiGridView = new uiGridView();
        uiListView obj_uiLisview = new uiListView();
        uiRadioButtonList obj_uiRadioButtonList = new uiRadioButtonList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        uiNotify noty = new uiNotify();
        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        Models.PT.daftarSinkronisasiDosen modelSinkronisasiDosen = new Models.PT.daftarSinkronisasiDosen();
        uiModal objModal = new uiModal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lvDaftarUsulanKonfirmasi);

            //if (mvMain.ActiveViewIndex == 0)
            //{
            //    if (ddlThnUsulan.Items.Count <= 1)
            //    {
            //        setddlThnUsulan();
            //        setddlThnPelaksanaan();
            //        setddlInstitusiYgMenugasi();
            //    }
            //}

            if (!IsPostBack)
            {
                setddlThnUsulan();
                setddlThnPelaksanaan();
                setddlInstitusiYgMenugasi();
                setgvMonev();
            }
        }

        private void setgvMonev()
        {
            DataTable dt = new DataTable();

            objEvaluasi.getDaftarEvaluasiSkema(ref dt, Guid.Parse(objLogin.idPersonal),
                 "20", ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlInstitusiYgMenugasi.SelectedValue);
            gvEvaluasiResume.DataSource = dt;
            gvEvaluasiResume.DataBind();
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
            setddlThnPelaksanaan();
            setddlInstitusiYgMenugasi();
            setgvMonev();
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            setddlInstitusiYgMenugasi();
            setgvMonev();
        }

        private void setddlInstitusiYgMenugasi()
        {
            DataTable dt = new DataTable();
            objEvaluasi.getDaftarInstitusiYgMenugasiSeminarHasil(ref dt, Guid.Parse(objLogin.idPersonal), "20", ddlThnUsulan.SelectedValue);

            try
            {
                ddlInstitusiYgMenugasi.DataTextField = "nama_institusi";
                ddlInstitusiYgMenugasi.DataValueField = "id_institusi";
                ddlInstitusiYgMenugasi.DataSource = dt;
                ddlInstitusiYgMenugasi.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        private void setddlThnUsulan()
        {
            ddlThnUsulan.Items.Clear();
            //ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlThnUsulan.SelectedValue = thnSKg.ToString();
            setddlThnPelaksanaan();
        }

        private void setddlThnPelaksanaan()
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            int intThnUsulan = int.Parse(thnUsulan);
            ddlThnPelaksanaan.Items.Clear();
            //ddlThnPelaksanaan.Items.Add(new ListItem("--Pilih--", "0000"));
            for (int i = intThnUsulan + 1; i >= intThnUsulan; i--)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlThnPelaksanaan.SelectedValue = (intThnUsulan + 1).ToString();
        }

        protected void ddlInstitusiYgMenugasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            setgvMonev();
        }

        protected void gvEvaluasiResume_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["IDSkema"] = gvEvaluasiResume.DataKeys[e.RowIndex]["id_skema"].ToString();

            mvMain.SetActiveView(vDaftarUsulan);

            lblTahapan.Text = "Kelayakan Administrasi";
            DataBoundLiteralControl litCol1 = gvEvaluasiResume.Rows[e.RowIndex].Cells[1].Controls[0] as DataBoundLiteralControl;
            if (litCol1 != null) lblSkema.Text = litCol1.Text;
            if (litCol1 != null) lbskemanilai.Text = litCol1.Text;
            lbTahunUsul.Text = "Tahun Usulan <b>" + ddlThnUsulan.SelectedValue.ToString() + "</b> | Pelaksanaan <b>" + ddlThnPelaksanaan.SelectedValue.ToString() + "</b>";

            setgvUsulan(0);
            setStatusPermanen();

            //if (ddlInstitusiYgMenugasi.SelectedValue == "ad34091d-893d-4adc-af4d-d18061bea699")
            //{
            //    pnlKota.Visible = true;
            //}
            //else
            //{
            //    pnlKota.Visible = false;
            //}
        }
        protected void gvEvaluasiResume_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStsPermanen = (Label)e.Row.FindControl("lblStsPermanen");
                if (lblStsPermanen.Text == "1")
                {
                    lblStsPermanen.Text = "Permanen";
                    lblStsPermanen.CssClass = "badge p-2 badge-danger";
                }
                else
                {
                    lblStsPermanen.Text = "Terbuka";
                    lblStsPermanen.CssClass = "badge p-2 badge-success";
                }
            }
        }

        private void setgvUsulan(int idxPage)
        {
            int IDSkema = int.Parse(ViewState["IDSkema"].ToString());
            DataTable dt = new DataTable();

            if (!objEvaluasi.getJmlData(objLogin.idPersonal.ToString(), IDSkema.ToString(), "20",
                ddlThnUsulan.SelectedValue.ToString(), ddlThnPelaksanaan.SelectedValue.ToString(),
                ddlInstitusiYgMenugasi.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objEvaluasi.errorMessage);

            //NEW PAGING CONTROL
            ktPagging.currentPage = idxPage;
            ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), objEvaluasi.numOfRecords);
            objEvaluasi.currentPage = idxPage;
            objEvaluasi.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!objEvaluasi.ListEvaluasiAdmrbPaging(objLogin.idPersonal.ToString(), IDSkema.ToString(), "20",
                ddlThnUsulan.SelectedValue.ToString(), ddlThnPelaksanaan.SelectedValue.ToString(),
                ddlInstitusiYgMenugasi.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objEvaluasi.errorMessage);
                return;
            }

            if (!obj_uiLisview.bindToListView(ref lvDaftarUsulanKonfirmasi, objEvaluasi.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objEvaluasi.errorMessage);

            if (objEvaluasi.numOfRecords < 1)
            {
                ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), 1);
            }

            //Simpan index id_plotting_reviewer usulan
            string[] arrUsulan = { };
            if (objEvaluasi.getDatadaftarplotting(ref arrUsulan, objLogin.idPersonal.ToString(), IDSkema.ToString(), "20",
            ddlThnUsulan.SelectedValue.ToString(), ddlThnPelaksanaan.SelectedValue.ToString(),
            ddlInstitusiYgMenugasi.SelectedValue))
            {
                ViewState["ArrayUsulan"] = arrUsulan;
            }
        }

        protected void lvDaftarUsulanKonfirmasi_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int itemIndex = int.Parse(e.CommandArgument.ToString());

            //Untuk isian modal konfirmasi persetujuan
            string judul = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["judul"].ToString();
            ViewState["judul"] = judul;
            string nama_skema = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["nama_skema"].ToString();
            ViewState["nama_skema"] = nama_skema;
            string thn_usulan_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["thn_usulan_kegiatan"].ToString();
            ViewState["thn_usulan_kegiatan"] = thn_usulan_kegiatan;
            string thn_pelaksanaan_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["thn_pelaksanaan_kegiatan"].ToString();
            ViewState["thn_pelaksanaan_kegiatan"] = thn_pelaksanaan_kegiatan;
            string nama_ketua = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["nama_ketua"].ToString();
            ViewState["nama_ketua"] = nama_ketua;
            string id_transaksi_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["id_transaksi_kegiatan"].ToString();
            ViewState["id_transaksi_kegiatan"] = id_transaksi_kegiatan;
            string id_usulan_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["id_usulan_kegiatan"].ToString();
            ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
            string nidn = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["nidn"].ToString();
            ViewState["nidn"] = nidn;

            if (e.CommandName == "UnduhPdf")
            {
                // Un-Comment Kalau pdfUsulanLengkap Selesai
                pdfUsulanLengkap.UnduhProposalLengkap(id_usulan_kegiatan);
            }
            else
            if (e.CommandName == "Nilai")
            {
                ViewState["IDPlottingReviewer"] = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["id_plotting_reviewer"].ToString();
                ViewState["thn_pelaksanaan_kegiatan"] = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["thn_pelaksanaan_kegiatan"].ToString();
                ViewState["id_usulan_kegiatan"] = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["id_usulan_kegiatan"].ToString();
                lblJudul.Text = judul;
                if (int.Parse(ViewState["IDSkema"].ToString()) == 30)
                {
                    lbCekRiwayatPendidikan.Visible = true;
                }
                else
                {
                    lbCekRiwayatPendidikan.Visible = false;
                }

                mvMain.SetActiveView(vMonev);
                setgvPenilaian();
                string[] arrUsulan = (string[])ViewState["ArrayUsulan"];
            }
        }

        protected void lbUnduhFormNilai_Click(object sender, EventArgs e)
        {
        }

        protected void lbSimpanPermanen_Click(object sender, EventArgs e)
        {
            objModal.ShowModal(this.Page, "modalKonfirmasiPermanen");
        }

        protected void lbKonfirmasiPermanen_Click(object sender, EventArgs e)
        {
            if (panelStatusPermanent.Visible)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Perhatian",
                    "Usulan sudah dalam status permanen !");
                return;
            }

            DataTable dt = new DataTable();
            string IDSkema = ViewState["IDSkema"].ToString();

            if (objEvaluasi.getdaftarplotting(ref dt, objLogin.idPersonal,
                    IDSkema, "20", ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlInstitusiYgMenugasi.SelectedValue))
            {
                DataRow[] selectedRow = dt.Select("total_nilai IS NULL");
                if (selectedRow.Length > 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Usulan masih ada yang belum diNilai !");
                    return;
                }

                if (objEvaluasi.setPermanenEvaluasiUsulanPenelitian(Guid.Parse(objLogin.idPersonal),
                        int.Parse(IDSkema), "20", ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, Guid.Parse(ddlInstitusiYgMenugasi.SelectedValue)))
                {
                    ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), 1);
                    objEvaluasi.generatetahapanevaluasisubtansi(Guid.Parse(objLogin.idPersonal),
                                            int.Parse(IDSkema), "20", ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, Guid.Parse(ddlInstitusiYgMenugasi.SelectedValue));
                    setStatusPermanen();
                    setgvUsulan(0);

                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                        "Status Penilaian sudah dipermanenkan...");
                }
                else
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        objEvaluasi.errorMessage);
            }
        }


        protected void setStatusPermanen()
        {
            DataTable dt = new DataTable();
            string IDSkema = ViewState["IDSkema"].ToString();
            bool isPermanent = false;

            if (objEvaluasi.getdaftarplotting(ref dt, objLogin.idPersonal,
                        IDSkema, "20", ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ddlInstitusiYgMenugasi.SelectedValue))
            {
                DataRow[] selectedRow = dt.Select("kd_sts_permanen = '1'");
                if (selectedRow.Length == dt.Rows.Count) isPermanent = true;

                panelStatusOpen.Visible = !isPermanent;
                panelStatusPermanent.Visible = isPermanent;
                lbSimpanPermanen.Visible = !isPermanent;
                lbSimpan.Visible = !isPermanent;
            }
        }

        protected void lbUsulanPrevTop_Click(object sender, EventArgs e)
        {
            setUsulan(false);
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            setgvUsulan(0);
        }

        protected void Paging_PageChanging(object sender, EventArgs e)
        {
            string IDSkema = ViewState["IDSkema"].ToString();
            objEvaluasi.currentPage = ktPagging.currentPage;
            objEvaluasi.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            ViewState["currentPage"] = ktPagging.currentPage;

            if (!objEvaluasi.ListEvaluasiAdmrbPaging(objLogin.idPersonal.ToString(), IDSkema.ToString(), "20",
                ddlThnUsulan.SelectedValue.ToString(), ddlThnPelaksanaan.SelectedValue.ToString(),
                ddlInstitusiYgMenugasi.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiLisview = new UIControllers.uiListView();
            obj_uiLisview.bindToListView(ref lvDaftarUsulanKonfirmasi, objEvaluasi.currentRecords);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            mvMain.ActiveViewIndex = 0;
            setgvMonev();
        }

        protected void lvDaftarUsulanKonfirmasi_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;

            string kd_sts_adm = drv["total_nilai"].ToString();

            Label lblNilai = new Label();
            LinkButton lbDisetujui = new LinkButton();

            lblNilai = (Label)e.Item.FindControl("lblNilai");
            lbDisetujui = (LinkButton)e.Item.FindControl("lbDisetujui");

            if (kd_sts_adm == "1") //layak
            {
                lblNilai.Text = " Layak ";
                lblNilai.CssClass = "text-success fa fa-check";
                lblNilai.Visible = true;
            }
            else if (kd_sts_adm == "0")
            {
                lblNilai.Text = " Tidak Layak ";
                lblNilai.CssClass = "text-danger fa fa-times";
                lblNilai.Visible = true;
            }
            else
            {
                lblNilai.Visible = false;
            }

            //lbSimpan.Visible = lbSimpanPermanen.Visible;
        }

        protected void lvDaftarUsulanKonfirmasi_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            int itemIndex = int.Parse(e.ToString());

            ViewState["IDPlottingReviewer"] = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["id_plotting_reviewer"].ToString();
            ViewState["thn_pelaksanaan_kegiatan"] = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["thn_pelaksanaan_kegiatan"].ToString();

            mvMain.SetActiveView(vMonev);

            //Cari index terpilih
            string[] arrUsulan = (string[])ViewState["ArrayUsulan"];
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["Penilaian"];

            ////Cek minimal karakter komentar
            if (tbKomentar.Text.Trim().Length < 50)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   "Maaf, Komentar Penilai minimal 50 karakter !");
                return;
            }
            int[] array = new int[10];
            int y = 0;
            RadioButtonList chk;
            foreach (GridViewRow rowItem in gvPenilaian.Rows)
            {
                chk = (RadioButtonList)(rowItem.Cells[0].FindControl("rblNilai"));
                if (chk.SelectedValue == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       "Skor Kriteria Penilaian harus diisi semua !");
                    return;
                }
                array[y] = int.Parse(chk.SelectedValue);
                y++;
            }
            ////Simpan hasil penilaian
            Guid IDPlottingReviewer = Guid.Parse(ViewState["IDPlottingReviewer"].ToString());
            int IDKomponenPenilaian, Skor;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IDKomponenPenilaian = int.Parse(dt.Rows[i]["id_komponen_penilaian"].ToString());
                Skor = array[i];
                // Sementara Untuk Seminar Hasil Catatan dipantek
                string Catatan = "-";
                if (!objEvaluasi.insupHasilPenilaian(IDPlottingReviewer, IDKomponenPenilaian, Skor, Catatan))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        objEvaluasi.errorMessage);
                    return;
                }
            }

            ////Simpan hasil review
            if (!objEvaluasi.insupHasilmonev(IDPlottingReviewer, tbKomentar.Text, "", "1"))    //rekomendasiDana, 

            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objEvaluasi.errorMessage);
                return;
            }

            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                      "Penilaian berhasil disimpan...");
        }

        protected void setgvPenilaian()
        {
            DataTable dt = new DataTable();
            Guid IDPlottingReviewer = Guid.Parse(ViewState["IDPlottingReviewer"].ToString());

            if (objEvaluasi.getHasilEvaluasi(ref dt, IDPlottingReviewer))
            {
                gvPenilaian.DataSource = dt;
                gvPenilaian.DataBind();

                ViewState["Penilaian"] = dt;
            }
            else
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objEvaluasi.errorMessage);


            DataTable dte = new DataTable();

            if (objEvaluasi.getHasilReview(ref dte, IDPlottingReviewer))
            {
                if (dte.Rows.Count > 0)
                {
                    tbKomentar.Text = dte.Rows[0]["komentar"].ToString();
                    string kd_sts_permanen = dte.Rows[0]["kd_sts_permanen"].ToString();

                    if (kd_sts_permanen == "1")
                    {
                        lbSimpan.Enabled = false;
                        lbSimpan.CssClass = "btn btn-danger";
                    }
                    else
                    {
                        lbSimpan.Enabled = true;
                    }
                }
                else
                {
                    tbKomentar.Text = string.Empty;
                }
            }
            else
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objEvaluasi.errorMessage);
        }

        protected void lbUsulanNextTop_Click(object sender, EventArgs e)
        {
            setUsulan(true);
        }

        protected void lbUsulanPrevBottom_Click(object sender, EventArgs e)
        {
            setUsulan(false);
        }

        protected void lbUsulanNextBottom_Click(object sender, EventArgs e)
        {
            setUsulan(true);
        }

        protected void setUsulan(bool isNext)
        {
            string[] arrUsulan = (string[])ViewState["ArrayUsulan"];
            int indexUsulan = int.Parse(ViewState["IndexUsulan"].ToString());

            if (isNext)
            {
                indexUsulan += 1;
                if (indexUsulan > arrUsulan.Length - 1) return;
            }
            else
            {
                indexUsulan -= 1;
                if (indexUsulan < 0) return;
            }

            ViewState["IDPlottingReviewer"] = arrUsulan[indexUsulan];
            ViewState["IndexUsulan"] = indexUsulan;

            setgvPenilaian();
        }

        protected void gvPenilaian_PreRender(object sender, EventArgs e)
        {
        }

        protected void gvPenilaian_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void lbKembaliKeUsulan_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vDaftarUsulan);
            if (ViewState["currentPage"] == null)
            {
                setgvUsulan(0);
            }
            else
            {
                setgvUsulankembali(int.Parse(ViewState["currentPage"].ToString()));
            }
        }

        private void setgvUsulankembali(int idxPage)
        {
            int IDSkema = int.Parse(ViewState["IDSkema"].ToString());
            DataTable dt = new DataTable();

            if (!objEvaluasi.getJmlData(objLogin.idPersonal.ToString(), IDSkema.ToString(), "20",
                ddlThnUsulan.SelectedValue.ToString(), ddlThnPelaksanaan.SelectedValue.ToString(),
                ddlInstitusiYgMenugasi.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objEvaluasi.errorMessage);

            //NEW PAGING CONTROL
            objEvaluasi.currentPage = idxPage;
            objEvaluasi.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!objEvaluasi.ListEvaluasiAdmrbPaging(objLogin.idPersonal.ToString(), IDSkema.ToString(), "20",
                ddlThnUsulan.SelectedValue.ToString(), ddlThnPelaksanaan.SelectedValue.ToString(),
                ddlInstitusiYgMenugasi.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objEvaluasi.errorMessage);
                return;
            }

            if (!obj_uiLisview.bindToListView(ref lvDaftarUsulanKonfirmasi, objEvaluasi.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objEvaluasi.errorMessage);

            if (objEvaluasi.numOfRecords < 1)
            {
                ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), 1);
            }

            //Simpan index id_plotting_reviewer usulan
            string[] arrUsulan = { };
            if (objEvaluasi.getDatadaftarplotting(ref arrUsulan, objLogin.idPersonal.ToString(), IDSkema.ToString(), "20",
            ddlThnUsulan.SelectedValue.ToString(), ddlThnPelaksanaan.SelectedValue.ToString(),
            ddlInstitusiYgMenugasi.SelectedValue))
            {
                ViewState["ArrayUsulan"] = arrUsulan;
            }
        }

        protected void gvPenilaian_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                if (drv["skor"].ToString() != string.Empty)
                {
                    RadioButtonList rblNilai = (RadioButtonList)e.Row.FindControl("rblNilai");
                    rblNilai.SelectedValue = drv["skor"].ToString();
                }
            }
        }

        protected void lbCekRiwayatPendidikan_Click(object sender, EventArgs e)
        {
            getDataPddikti();
            objModal.ShowModal(this.Page, "modalRiwayatPendidikan");
        }

        private void getDataPddikti()
        {
            Boolean hasil = false;
            //NpgsqlConnection conn = new NpgsqlConnection(cs);
            //conn.Open();

            //if (ViewState["nidn"].ToString() != "")
            //{
            //    const string BaseUrl = "https://api.ristekdikti.go.id:8243/pddikti/1.0/";
            //    string Token = "00000000-0000-0000-0000-000000000000";
            //    var dtToken = new DataTable();
            //    modelSinkronisasiDosen.getTokenPddikti(ref dtToken);
            //    if (dtToken.Rows.Count > 0)
            //    {
            //        Token = dtToken.Rows[0]["token"].ToString();
            //    }
            //}
        }

    }
}