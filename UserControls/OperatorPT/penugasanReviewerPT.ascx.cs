using OfficeOpenXml;
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
    public partial class penugasanReviewerPT : System.Web.UI.UserControl
    {
        Models.PT.daftarPenugasanReviewerPT modelPenugasanReviewer = new Models.PT.daftarPenugasanReviewerPT();
        uiGridView obj_uiGridView = new uiGridView();
        uiDropdownList obj_uiDDL = new uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiModal objModal = new uiModal();

        string[] namaKolomPenugasan = { "no_baris", "id_penugasan_reviewer", "id_reviewer", "kd_kategori_reviewer", "kategori_reviewer", "kompetensi",
            "nama_reviewer", "id_institusi", "nama_institusi", "nidn","jml_total_data","nomor_hp","nomor_telepon","surel" };

        string[] namaKolomRevBlmDitugaskan = { "no_baris", "id_reviewer", "id_personal", "kategori_reviewer", "kompetensi",
            "nama", "id_institusi", "nama_institusi","nomor_hp","nomor_telepon","surel" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvRekapPenugasanSkema);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbExcelDaftarReviewer);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbExcelPenugasan);

            if (!IsPostBack)
            {
                isiDdlProgram();
                isiDdlThnUsulan();
                isiDdlThnPelaksanaan();

                if (ddlTahapan.Items.Count <= 1)
                    isiTahapanKegiatan();
                //if (objLogin.idPeran == 6 && modelPenugasanReviewer.cekApakahKlasterNonBinaanDanPtn(Guid.Parse(objLogin.idInstitusi.ToString())) == true)
                //{
                //    //lbReviewerInternal.Visible = true;
                //    //lbReviewereksternal.Visible = true;
                //}
                //else
                //{
                //    //lbReviewerInternal.Visible = false;
                //    //lbReviewereksternal.Visible = true;
                //}

                isiGvRekapPenugasanSkema();
                mvPenugasanReviewer.SetActiveView(viewRekapPenugasanSkema);

                gvRekapPenugasanSkema.Enabled = true;
            }
        }

        #region Rekap Skema Penugasan

        private void isiGvRekapPenugasanSkema()
        {
            var dt = new DataTable();
            string idInstitusi = objLogin.idInstitusi.ToString();
            //string kdJenisKegiatan = "0";
            //if (objLogin.idPeran == 6)
            //{
            //    kdJenisKegiatan = "1";
            //}
            //else
            //{
            //    kdJenisKegiatan = "2";
            //}
            string kdJenisKegiatan = ddlProgram.SelectedValue;

            if (modelPenugasanReviewer.daftarRekapPenugasanSkema(ref dt, idInstitusi, ddlThnUsulan.SelectedValue,
                ddlThnPelaksanaan.SelectedValue, kdJenisKegiatan, ddlTahapan.SelectedValue))
            {
                gvRekapPenugasanSkema.DataSource = dt;
                gvRekapPenugasanSkema.DataBind();
            }
        }

        private void isiDdlProgram()
        {
            DataTable dt = objManipData.filterData((DataTable)Application["JenisKegiatan"], "kd_sts_aktif IN ('1') ");
            obj_uiDDL.bindToDropDownList(ref ddlProgram, dt, "jenis_kegiatan", "kd_jenis_kegiatan");
            ddlProgram.Items.Insert(0, new ListItem("-- Pilih --", "0"));
            ddlProgram.SelectedIndex = 0;
        }

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
            if (int.Parse(thnUsulan) > 0)
            {
                for (int i = 1; i >= 0; i--)
                {
                    ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
                }
            }
            else
            {
                ddlThnPelaksanaan.Items.Add(new ListItem("-- Pilih--", "0"));
            }
        }

        private void isiTahapanKegiatan()
        {
            ddlTahapan.Items.Clear();
            DataTable Tahapan = new DataTable();
            modelPenugasanReviewer.daftar_tahapan_thn_pelaksanaan(ref Tahapan, objLogin.idInstitusi.ToString(), ddlThnUsulan.SelectedValue);
            ddlTahapan.Items.Add(new ListItem("--Pilih--", "0"));
            obj_uiDropdownlist.bindToDropDownList(ref ddlTahapan, Tahapan, "tahapan", "kd_tahapan_kegiatan");
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapPenugasanSkema();
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiDdlThnPelaksanaan();
            isiTahapanKegiatan();
            isiGvRekapPenugasanSkema();
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiTahapanKegiatan();
            isiGvRekapPenugasanSkema();
        }

        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapPenugasanSkema();
        }

        protected void gvRekapPenugasanSkema_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string idInstitusi = gvRekapPenugasanSkema.DataKeys[e.RowIndex]["id_institusi"].ToString();
            ViewState["IdInstitusi"] = idInstitusi;
            int idSkema = int.Parse(gvRekapPenugasanSkema.DataKeys[e.RowIndex]["id_skema"].ToString());
            ViewState["IdSkema"] = idSkema;
            string namaSkema = gvRekapPenugasanSkema.DataKeys[e.RowIndex]["nama_skema"].ToString();
            ViewState["namaSkema"] = namaSkema;

            lblThnUsulanPenugasan.Text = ddlThnUsulan.SelectedValue.ToString();
            lbltahapanPenugasan.Text = ddlTahapan.SelectedItem.ToString();
            lblNamaSkemaPenugasan.Text = namaSkema;
            lblThnPelaksanaanPenugasan.Text = ddlThnPelaksanaan.SelectedValue.ToString();
            mvPenugasanReviewer.SetActiveView(viewPenugasan);
            tbCariPenugasan.Text = "";
            tbCariDaftarReviewer.Text = "";
            isiGvDaftarPenugasan(0);

        }

        protected void lbExcelRekapSkema_Click(object sender, EventArgs e)
        {
            string id_institusi = objLogin.idInstitusi.ToString();
            string thn_usulan_kegiatan = ddlThnUsulan.SelectedValue;
            string thn_pelaksanaan_kegiatan = ddlThnPelaksanaan.SelectedValue;
            string kd_tahapan_kegiatan = ddlTahapan.SelectedValue;
            string kdJenisKegiatan = "0";
            string[] namaKolom = { "nama_skema", "jml_proposal", "jml_reviewer" };

            if (thn_pelaksanaan_kegiatan == "0000" || thn_pelaksanaan_kegiatan == "0" || kd_tahapan_kegiatan == "")
                return;
            using (ExcelPackage pck = new ExcelPackage())
            {
                if (objLogin.idPeran == 6)
                {
                    kdJenisKegiatan = "1";
                }
                else
                {
                    kdJenisKegiatan = "2";
                }

                var fileName = string.Format("plotting " + ddlTahapan.SelectedItem.Text + " tahun {0}.xlsx", ddlThnPelaksanaan.SelectedItem.Text);
                DataTable dt = new DataTable();
                foreach (string item in namaKolom)
                {
                    dt.Columns.Add(item);
                }

                if (modelPenugasanReviewer.daftarRekapPenugasanSkema(ref dt, id_institusi, thn_usulan_kegiatan,
                thn_pelaksanaan_kegiatan, kdJenisKegiatan, kd_tahapan_kegiatan, false))
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
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPenugasanReviewer.errorMessage);
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

        protected void lbBebanReviewer_Click(object sender, EventArgs e)
        {
            mvPenugasanReviewer.SetActiveView(viewBebanReviewer);
            isiGvBebanReviewer(0);
        }

        #endregion

        #region Penugasan

        protected void lbKembaliPenugasan_Click(object sender, EventArgs e)
        {
            isiGvRekapPenugasanSkema();
            mvPenugasanReviewer.SetActiveView(viewRekapPenugasanSkema);
        }

        protected void lbCariPenugasan_Click(object sender, EventArgs e)
        {
            isiGvDaftarPenugasan(0);
        }

        protected void ddlJmlBarisPenugasan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarPenugasan(0);
        }

        private void isiGvDaftarPenugasan(int idxPage)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            if (!modelPenugasanReviewer.getJmlDaftarPenugasan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            //NEW PAGING CONTROL
            lblJmlPenugasan.Text = modelPenugasanReviewer.numOfRecords.ToString();

            pagingDaftarPenugasan.currentPage = idxPage;
            pagingDaftarPenugasan.setPaging(int.Parse(ddlJmlBarisPenugasan.SelectedValue), modelPenugasanReviewer.numOfRecords);
            modelPenugasanReviewer.currentPage = idxPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisPenugasan.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarPenugasan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan, tbCariPenugasan.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvDaftarPenugasan, modelPenugasanReviewer.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            if (modelPenugasanReviewer.numOfRecords < 1)
            {
                pagingDaftarPenugasan.setPaging(int.Parse(ddlJmlBarisPenugasan.SelectedValue), 1);
            }
        }

        protected void daftarPenugasan_PageChanging(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            modelPenugasanReviewer.currentPage = pagingDaftarPenugasan.currentPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisPenugasan.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarPenugasan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarPenugasan, modelPenugasanReviewer.currentRecords);
        }

        protected void gvDaftarPenugasan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdPenugasanReviewer"] = Guid.Parse(gvDaftarPenugasan.DataKeys[e.RowIndex]["id_penugasan_reviewer"].ToString());
            lblNamaReviewerHapusDaftarPenugasan.Text = gvDaftarPenugasan.DataKeys[e.RowIndex]["nama_reviewer"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void lbHapusDaftarPenugasan_Click(object sender, EventArgs e)
        {
            if (modelPenugasanReviewer.deleteData(Guid.Parse(ViewState["IdPenugasanReviewer"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus penugasan reviewer berhasil");
                refreshPagingDaftarPenugasan();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus penugasan reviewer gagal " + modelPenugasanReviewer.errorMessage);
            }
        }

        private void refreshPagingDaftarPenugasan()
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            if (!modelPenugasanReviewer.getJmlDaftarPenugasan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            //NEW PAGING CONTROL
            lblJmlPenugasan.Text = modelPenugasanReviewer.numOfRecords.ToString();

            modelPenugasanReviewer.currentPage = pagingDaftarPenugasan.currentPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisPenugasan.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarPenugasan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarPenugasan, modelPenugasanReviewer.currentRecords);
        }

        protected void gvDaftarPenugasan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNoDaftarPenugasan = (Label)e.Row.FindControl("lblNoDaftarPenugasan");
                lblNoDaftarPenugasan.Text = (e.Row.RowIndex + 1 + int.Parse(ddlJmlBarisPenugasan.SelectedValue) * (pagingDaftarPenugasan.currentPage)).ToString();
            }
        }

        protected void lbReviewerInternal_Click(object sender, EventArgs e)
        {
            ViewState["kdKategoriReviewer"] = "1";
            lblJenisPenugasan.Text = "Penugasan Reviewer Internal";
            lblThnUsulanDaftarReviewer.Text = ddlThnUsulan.SelectedValue.ToString();
            lblTahapanDaftarReviewer.Text = ddlTahapan.SelectedItem.ToString();
            lblNamaSkemaDaftarReviewer.Text = ViewState["namaSkema"].ToString();
            lblThnPelaksanaanDaftarReviewer.Text = ddlThnPelaksanaan.SelectedValue.ToString();
            mvPenugasanReviewer.SetActiveView(viewDaftarReviewer);
            isiGvDaftarReviewer(0);
        }

        protected void lbReviewerEksternal_Click(object sender, EventArgs e)
        {
            ViewState["kdKategoriReviewer"] = "3";
            lblJenisPenugasan.Text = "Penugasan Reviewer Eksternal";
            lblThnUsulanDaftarReviewer.Text = ddlThnUsulan.SelectedValue.ToString();
            lblTahapanDaftarReviewer.Text = ddlTahapan.SelectedItem.ToString();
            lblNamaSkemaDaftarReviewer.Text = ViewState["namaSkema"].ToString();
            lblThnPelaksanaanDaftarReviewer.Text = ddlThnPelaksanaan.SelectedValue.ToString();
            mvPenugasanReviewer.SetActiveView(viewDaftarReviewer);
            isiGvDaftarReviewer(0);
        }

        protected void lbExcelPenugasan_Click(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            string[] namaKolom = { "nomor", "nama_reviewer", "nama_institusi", "kompetensi", "nomor_hp", "surel" };
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Daftar Penugasan " + ddlTahapan.SelectedItem.Text + " tahun {0}.xlsx", ddlThnPelaksanaan.SelectedItem.Text);
                DataTable dt = new DataTable();
                foreach (string item in namaKolom)
                {
                    dt.Columns.Add(item);
                }

                if (modelPenugasanReviewer.getExcelPenugasanReviewer(ref dt, idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan, 0, 0))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("data");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPenugasanReviewer.errorMessage);
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

        #endregion

        #region Daftar Reviewer

        protected void ddlJmlBarisDaftarReviewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarReviewer(0);
        }

        protected void gvDaftarReviewer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid idReviewer = Guid.Parse(gvDaftarReviewer.DataKeys[e.RowIndex]["id_reviewer"].ToString());
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            if (modelPenugasanReviewer.insertPenugasanReviewer(idSkema, thnUsulanKegiatan, idReviewer, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Penugasan reviewer berhasil");
                refreshPagingDaftarReviewer();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Penugasan reviewer gagal" + modelPenugasanReviewer.errorMessage);
            }
        }

        protected void gvDaftarReviewer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNoDaftarReviewer = (Label)e.Row.FindControl("lblNoDaftarReviewer");
                lblNoDaftarReviewer.Text = (e.Row.RowIndex + 1 + int.Parse(ddlJmlBarisDaftarReviewer.SelectedValue) * (pagingDaftarReviewer.currentPage)).ToString();
            }
        }

        private void isiGvDaftarReviewer(int idxPage)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            string kdKategoriReviewer = ViewState["kdKategoriReviewer"].ToString();
            string namaReviewer = tbCariDaftarReviewer.Text;
            if (!modelPenugasanReviewer.getJmlReviewerBlmDitugaskan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan, kdKategoriReviewer, namaReviewer))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            //NEW PAGING CONTROL
            lblJmlDaftarReviewer.Text = modelPenugasanReviewer.numOfRecords.ToString();

            pagingDaftarReviewer.currentPage = idxPage;
            pagingDaftarReviewer.setPaging(int.Parse(ddlJmlBarisDaftarReviewer.SelectedValue), modelPenugasanReviewer.numOfRecords);

            modelPenugasanReviewer.currentPage = idxPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisDaftarReviewer.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarReviewerBlmDitugaskan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan, kdKategoriReviewer, namaReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvDaftarReviewer, modelPenugasanReviewer.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            if (modelPenugasanReviewer.numOfRecords < 1)
            {
                pagingDaftarReviewer.setPaging(int.Parse(ddlJmlBarisDaftarReviewer.SelectedValue), 1);
            }
        }

        private void refreshPagingDaftarReviewer()
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            string kdKategoriReviewer = ViewState["kdKategoriReviewer"].ToString();
            string namaReviewer = tbCariDaftarReviewer.Text;
            if (!modelPenugasanReviewer.getJmlReviewerBlmDitugaskan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan, kdKategoriReviewer, namaReviewer))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            //NEW PAGING CONTROL
            lblJmlDaftarReviewer.Text = modelPenugasanReviewer.numOfRecords.ToString();

            modelPenugasanReviewer.currentPage = pagingDaftarReviewer.currentPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisDaftarReviewer.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarReviewerBlmDitugaskan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan, kdKategoriReviewer, namaReviewer))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarReviewer, modelPenugasanReviewer.currentRecords);
        }

        protected void pagingDaftarReviewer_PageChanging(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            string kdKategoriReviewer = ViewState["kdKategoriReviewer"].ToString();
            string namaReviewer = tbCariDaftarReviewer.Text;
            modelPenugasanReviewer.currentPage = pagingDaftarReviewer.currentPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisDaftarReviewer.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarReviewerBlmDitugaskan(idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan, kdKategoriReviewer, namaReviewer))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarReviewer, modelPenugasanReviewer.currentRecords);
        }

        protected void lbKembaliDaftarReviewer_Click(object sender, EventArgs e)
        {
            tbCariDaftarReviewer.Text = string.Empty;
            mvPenugasanReviewer.SetActiveView(viewPenugasan);
            isiGvDaftarPenugasan(0);
        }

        protected void lbCariDaftarReviewer_Click(object sender, EventArgs e)
        {
            isiGvDaftarReviewer(0);
        }

        protected void lbExcelDaftarReviewer_Click(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            string kdKategoriReviewer = ViewState["kdKategoriReviewer"].ToString();
            string[] namaKolom = { "nama", "nama_institusi", "kompetensi", "kategori_reviewer", "nomor_telepon", "nomor_hp", "surel" };

            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("daftar reviewer belum ditugaskan " + ddlTahapan.SelectedItem.Text + " tahun {0}.xlsx", ddlThnPelaksanaan.SelectedItem.Text);
                DataTable dt = new DataTable();
                foreach (string item in namaKolom)
                {
                    dt.Columns.Add(item);
                }

                if (modelPenugasanReviewer.getDaftarReviewerBlmDitugaskanExcel(ref dt, idSkema, thnUsulanKegiatan, kdTahapanKegiatan,
                idInstitusi, thnPelaksanaanKegiatan, kdKategoriReviewer))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("daftar_reviewer");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPenugasanReviewer.errorMessage);
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

        #endregion

        #region Beban Reviewer

        private void isiGvBebanReviewer(int idxPage)
        {
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            string namaReviewer = tbCariBebanReviewer.Text;
            if (!modelPenugasanReviewer.getJmlBebanReviewerReviewer(idInstitusi, thnUsulanKegiatan, thnPelaksanaanKegiatan,
                kdTahapanKegiatan, namaReviewer))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            //NEW PAGING CONTROL
            lblJmlBebanReviewer.Text = modelPenugasanReviewer.numOfRecords.ToString();

            pagingBebanReviewer.currentPage = idxPage;
            pagingBebanReviewer.setPaging(int.Parse(ddlJmlBarisBebanReviewer.SelectedValue), modelPenugasanReviewer.numOfRecords);

            modelPenugasanReviewer.currentPage = idxPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisBebanReviewer.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarBebanReviewer(idInstitusi, thnUsulanKegiatan, thnPelaksanaanKegiatan,
                kdTahapanKegiatan, namaReviewer))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvBebanReviewer, modelPenugasanReviewer.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPenugasanReviewer.errorMessage);

            if (modelPenugasanReviewer.numOfRecords < 1)
            {
                pagingBebanReviewer.setPaging(int.Parse(ddlJmlBarisBebanReviewer.SelectedValue), 1);
            }
        }

        protected void bebanReviewer_PageChanging(object sender, EventArgs e)
        {
            Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
            string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            string kdTahapanKegiatan = ddlTahapan.SelectedValue;
            string namaReviewer = tbCariBebanReviewer.Text;
            modelPenugasanReviewer.currentPage = pagingBebanReviewer.currentPage;
            modelPenugasanReviewer.rowsPerPage = int.Parse(ddlJmlBarisBebanReviewer.SelectedValue);

            if (!modelPenugasanReviewer.getDaftarBebanReviewer(idInstitusi, thnUsulanKegiatan, thnPelaksanaanKegiatan,
                kdTahapanKegiatan, namaReviewer))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvBebanReviewer, modelPenugasanReviewer.currentRecords);
        }

        protected void gvBebanReviewer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNoBebanReviewer = (Label)e.Row.FindControl("lblNoBebanReviewer");
                lblNoBebanReviewer.Text = (e.Row.RowIndex + 1 + int.Parse(ddlJmlBarisBebanReviewer.SelectedValue) * (pagingBebanReviewer.currentPage)).ToString();
            }
        }

        protected void ddlJmlBarisBebanReviewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvBebanReviewer(0);
        }

        protected void lbKembaliBebanReviewer_Click(object sender, EventArgs e)
        {
            isiGvRekapPenugasanSkema();
            mvPenugasanReviewer.SetActiveView(viewRekapPenugasanSkema);
            tbCariBebanReviewer.Text = string.Empty;
        }

        protected void lbCariBebanReviewer_Click(object sender, EventArgs e)
        {
            isiGvBebanReviewer(0);
        }

        //protected void lbExcelBebanReviewer_Click(object sender, EventArgs e)
        //{
        //    int idSkema = int.Parse(ViewState["IdSkema"].ToString());
        //    string thnUsulanKegiatan = ddlThnUsulan.SelectedValue;
        //    string kdTahapanKegiatan = ddlTahapan.SelectedValue;
        //    Guid idInstitusi = Guid.Parse(objLogin.idInstitusi.ToString());
        //    string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
        //    string kdKategoriReviewer = ViewState["kdKategoriReviewer"].ToString();
        //    string[] namaKolom = { "nama", "nama_institusi", "kompetensi", "kategori_reviewer", "nomor_telepon", "nomor_hp", "surel" };

        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        var fileName = string.Format("daftar reviewer belum ditugaskan " + ddlTahapan.SelectedItem.Text + " tahun {0}.xlsx", ddlThnPelaksanaan.SelectedItem.Text);
        //        DataTable dt = new DataTable();
        //        foreach (string item in namaKolom)
        //        {
        //            dt.Columns.Add(item);
        //        }

        //        if (modelPenugasanReviewer.getDaftarBebanReviewer(idInstitusi, thnUsulanKegiatan, thnPelaksanaanKegiatan, kdTahapanKegiatan, ""))
        //        {
        //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("daftar_reviewer");
        //            ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
        //            Autofit Column
        //            for (int i = 1; i <= dt.Columns.Count; i++)
        //            {
        //                ws.Column(i).AutoFit(10);
        //            }
        //        }
        //        else
        //        {
        //            noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPenugasanReviewer.errorMessage);
        //            return;
        //        }

        //        HttpResponse httpResponse = Response;
        //        httpResponse.Clear();
        //        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

        //        Flush the workbook to the Response.OutputStream
        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            workbook.SaveAs(memoryStream);
        //            pck.SaveAs(memoryStream);
        //            memoryStream.WriteTo(httpResponse.OutputStream);
        //            memoryStream.Close();
        //        }

        //        httpResponse.End();
        //        pck.Dispose();
        //    }
        //}

        #endregion

        
    }
}