using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using simlitekkes.UIControllers;
using System.Data;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class penetapanUsulanBaru : System.Web.UI.UserControl
    {
        Models.login objLogin;

        Core.manipulasiData objManipData = new Core.manipulasiData();

        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiGridView obj_uiGridView = new uiGridView();
        uiModal objModal = new uiModal();

        Models.OperatorPenelitianPusdik.penetapanUsulanBaru modelPenetapanUsulanBaru = new Models.OperatorPenelitianPusdik.penetapanUsulanBaru();

        int bukanEdit = 0;
        int edit = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiTahapanKegiatan();
                ddlTahapan.SelectedIndex = 0;
                ViewState["Tahapan"] = ddlTahapan.SelectedItem.Text;
                isiDdlThnUsulan();
                ddlThnUsulan.SelectedIndex = 0;
                ViewState["TahunUsulan"] = ddlThnUsulan.SelectedItem.Text;
                isiDdlThnPelaksanaan();
                ddlThnPelaksanaan.SelectedIndex = 0;
                ViewState["TahunPelaksanaan"] = ddlThnPelaksanaan.SelectedItem.Text;

                mvMain.SetActiveView(vRekapSkema);
                isiGvRekapSkema();
            }
        }

        #region Rekap Skema

        private void isiTahapanKegiatan()
        {
            if (Application["TahapanKegiatan"] != null)
            {
                DataTable TahapanKegiatan = objManipData.filterData((DataTable)Application["TahapanKegiatan"], "kd_tahapan_kegiatan IN ('22','25') ");
                if (!obj_uiDropdownlist.bindToDropDownList(ref ddlTahapan, TahapanKegiatan, "tahapan", "kd_tahapan_kegiatan"))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + obj_uiDropdownlist.errorMessage);
                    return;
                }
            }
        }

        private void isiDdlThnUsulan()
        {
            ddlThnUsulan.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlThnUsulan.SelectedIndex = 0;
        }

        private void isiDdlThnPelaksanaan()
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            if (int.Parse(thnUsulan) > 0)
            {
                for (int i = 1; i >= 0; i--)
                {
                    ddlThnPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
                }
            }
            else
            {
                ddlThnPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem("-- Pilih--", "0"));
            }
        }

        private void isiGvRekapSkema()
        {
            var dt = new DataTable();
            if (modelPenetapanUsulanBaru.listRekapSkema(ref dt, rblProgramHibah.SelectedValue, ddlThnUsulan.SelectedValue,
                ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue))
            {
                gvRekapSkema.DataSource = dt;
                gvRekapSkema.DataBind();

                if (dt.Rows.Count > 0)
                {
                    var dtTotal = new DataTable();
                    modelPenetapanUsulanBaru.getTotalRekapSkema(ref dtTotal, rblProgramHibah.SelectedValue, ddlThnUsulan.SelectedValue,
                        ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue);

                    lblTotalJmlUsulanRekapSkema.Text = dtTotal.Rows[0]["total_jml_usulan"].ToString();
                    lblTotalJmlTdkLolosRekapSkema.Text = dtTotal.Rows[0]["total_jml_tdk_lolos"].ToString();
                    lblTotalJmlLolosRekapSkema.Text = dtTotal.Rows[0]["total_jml_lolos"].ToString();
                    lblTotalJmlBlmDitetapkanRekapSkema.Text = dtTotal.Rows[0]["total_jml_blm_ditetapkan"].ToString();
                    //lblTotalJmlDanaRekapSkema.Text = dtTotal.Rows[0]["total_jml_dana"].ToString();
                    lblTotalJmlDanaRekapSkema.Text = objManipData.convertFormatDana(dtTotal.Rows[0]["total_jml_dana"].ToString());
                }
                else
                {
                    lblTotalJmlUsulanRekapSkema.Text = "0";
                    lblTotalJmlTdkLolosRekapSkema.Text = "0";
                    lblTotalJmlLolosRekapSkema.Text = "0";
                    lblTotalJmlBlmDitetapkanRekapSkema.Text = "0";
                    lblTotalJmlDanaRekapSkema.Text = "Rp 0";
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }
        }

        protected void rblProgramHibah_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapSkema();
        }

        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapSkema();
        }

        protected void ddlTahunUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiDdlThnPelaksanaan();
            isiGvRekapSkema();
        }

        protected void ddlTahunPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapSkema();
        }

        protected void gvRekapSkema_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detailRekapSkema")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                int idSkema = int.Parse(gvRekapSkema.DataKeys[idx]["id_skema"].ToString());
                string namaSkema = gvRekapSkema.DataKeys[idx]["nama_skema"].ToString();

                ViewState["IdSkema"] = idSkema;
                ViewState["NamaSkema"] = namaSkema;

                lblProgramHibahRekapPT.Text = rblProgramHibah.SelectedItem.Text.ToString();
                lblNamaSkemaRekapPT.Text = namaSkema;
                lblTahapanRekapPT.Text = ddlTahapan.SelectedItem.Text.ToString();
                lblThnUsulanRekapPT.Text = ddlThnUsulan.SelectedItem.Text.ToString();
                lblThnPelaksanaanRekapPT.Text = ddlThnPelaksanaan.Text.ToString();

                isiGvRekapPT(int.Parse(ddlJmlBarisRekapPT.SelectedValue), 0);
                tbPencarianRekapPT.Text = string.Empty;
                mvMain.SetActiveView(vRekapPT);
            }
        }

        #endregion

        #region Rekap PT

        private void isiGvRekapPT(int limit, int offset)
        {
            if (!modelPenetapanUsulanBaru.getJmlDataRekapPT(rblProgramHibah.SelectedValue, ddlThnUsulan.SelectedValue,
                ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, int.Parse(ViewState["IdSkema"].ToString()),
                tbPencarianRekapPT.Text, 0, offset))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);

            pagingRekapPT.currentPage = offset;
            pagingRekapPT.setPaging(limit, modelPenetapanUsulanBaru.numOfRecords);

            modelPenetapanUsulanBaru.currentPage = offset;
            modelPenetapanUsulanBaru.rowsPerPage = limit;

            DataTable dt = new DataTable();
            if (modelPenetapanUsulanBaru.listRekapPT(ref dt, rblProgramHibah.SelectedValue, ddlThnUsulan.SelectedValue,
                ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, int.Parse(ViewState["IdSkema"].ToString()),
                tbPencarianRekapPT.Text, int.Parse(ddlJmlBarisRekapPT.SelectedValue), offset))
            {
                gvRekapPT.DataSource = dt;
                gvRekapPT.DataBind();

                if (dt.Rows.Count > 0)
                {
                    var dtTotal = new DataTable();
                    modelPenetapanUsulanBaru.getTotalRekapPT(ref dtTotal, rblProgramHibah.SelectedValue, ddlThnUsulan.SelectedValue,
                        ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, int.Parse(ViewState["IdSkema"].ToString()));

                    lblTotalJmlUsulanRekapPT.Text = dtTotal.Rows[0]["total_jml_usulan"].ToString();
                    lblTotalJmlTdkLolosRekapPT.Text = dtTotal.Rows[0]["total_jml_tdk_lolos"].ToString();
                    lblTotalJmlLolosRekapPT.Text = dtTotal.Rows[0]["total_jml_lolos"].ToString();
                    lblTotalJmlBlmDitetapkanRekapPT.Text = dtTotal.Rows[0]["total_jml_blm_ditetapkan"].ToString();
                    lblTotalJmlDanaRekapPT.Text = objManipData.convertFormatDana(dtTotal.Rows[0]["total_jml_dana"].ToString());
                }
                else
                {
                    lblTotalJmlUsulanRekapPT.Text = "0";
                    lblTotalJmlTdkLolosRekapPT.Text = "0";
                    lblTotalJmlLolosRekapPT.Text = "0";
                    lblTotalJmlBlmDitetapkanRekapPT.Text = "0";
                    lblTotalJmlDanaRekapPT.Text = "0";
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }

            if (modelPenetapanUsulanBaru.numOfRecords < 1)
            {
                pagingRekapPT.setPaging(limit, 1);
            }
        }

        protected void lbKembaliRekapPT_Click(object sender, EventArgs e)
        {
            tbPencarianRekapPT.Text = string.Empty;
            isiGvRekapSkema();
            mvMain.SetActiveView(vRekapSkema);
        }

        protected void lbPencarianRekapPT_Click(object sender, EventArgs e)
        {
            isiGvRekapPT(int.Parse(ddlJmlBarisRekapPT.SelectedValue), 0);
        }

        protected void ddlJmlBarisRekapPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapPT(int.Parse(ddlJmlBarisRekapPT.SelectedValue), 0);
        }

        protected void lbExcelRekapPT_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Rekap Institusi " + ViewState["NamaSkema"].ToString() + ".xlsx");
                if (modelPenetapanUsulanBaru.getExcelRekapPT(ref dt, rblProgramHibah.SelectedValue, ddlThnUsulan.SelectedValue,
                ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, int.Parse(ViewState["IdSkema"].ToString())))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Data");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);

                    if (dt.Rows.Count > 0)
                    {
                        var endRow = dt.Rows.Count + 1;

                        //Hitung Total
                        ws.Cells[endRow + 1, 3].Formula = $"=SUM(C2:C{endRow})";
                        ws.Cells[endRow + 1, 4].Formula = $"=SUM(D2:D{endRow})";
                        ws.Cells[endRow + 1, 5].Formula = $"=SUM(E2:E{endRow})";
                        ws.Cells[endRow + 1, 6].Formula = $"=SUM(F2:F{endRow})";
                        ws.Cells[endRow + 1, 7].Formula = $"=SUM(G2:G{endRow})";
                        ws.Cells[endRow + 1, 8].Formula = $"=SUM(H2:H{endRow})";
                        ws.Cells[endRow + 1, 2].Value = "TOTAL";

                        //Format kolom
                        ws.Cells[2, 3, endRow + 1, 8].Style.Numberformat.Format = "#,##0";
                    }

                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
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

        protected void gvRekapPT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detailRekapPT")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());
                Guid.TryParse(gvRekapPT.DataKeys[idx]["id_institusi"].ToString(), out Guid id_institusi);
                string namaInstitusi = gvRekapPT.DataKeys[idx]["nama_institusi"].ToString();

                ViewState["IdInstitusi"] = id_institusi;
                ViewState["NamaInstitusi"] = namaInstitusi;

                lblProgramHibahPenetapan.Text = rblProgramHibah.SelectedItem.Text.ToString();
                lblNamaSkemaPenetapan.Text = ViewState["NamaSkema"].ToString();
                lblTahapanPenetapan.Text = ddlTahapan.SelectedItem.Text.ToString();
                lblThnUsulanPenetapan.Text = ddlThnUsulan.SelectedItem.Text.ToString();
                lblThnPelaksanaanPenetapan.Text = ddlThnPelaksanaan.Text.ToString();
                lblNamaInstitusiPenetapan.Text = namaInstitusi;

                isiLvPenetapan(int.Parse(ddlJmlBarisPenetapan.SelectedValue), 0, bukanEdit);
                tbPencarianPenetapan.Text = string.Empty;
                mvMain.SetActiveView(vPenetapan);
            }
        }

        protected void gvRekapPT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoRekapPT = (Label)e.Row.FindControl("lblNoRekapPT");
                lblNoRekapPT.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisRekapPT.SelectedValue) * (pagingRekapPT.currentPage)).ToString();
            }
        }

        protected void pagingRekapPT_PageChanging(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelPenetapanUsulanBaru.currentPage = pagingRekapPT.currentPage;
            modelPenetapanUsulanBaru.rowsPerPage = int.Parse(ddlJmlBarisRekapPT.SelectedValue);
            ViewState["currentPageRekapPT"] = pagingRekapPT.currentPage * int.Parse(ddlJmlBarisRekapPT.SelectedValue);

            if (modelPenetapanUsulanBaru.listRekapPT(ref dt, rblProgramHibah.SelectedValue, ddlThnUsulan.SelectedValue,
                ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, int.Parse(ViewState["IdSkema"].ToString()),
                tbPencarianRekapPT.Text, int.Parse(ddlJmlBarisRekapPT.SelectedValue), int.Parse(ViewState["currentPageRekapPT"].ToString())))
            {
                gvRekapPT.DataSource = dt;
                gvRekapPT.DataBind();

                if (dt.Rows.Count > 0)
                {
                    var dtTotal = new DataTable();
                    modelPenetapanUsulanBaru.getTotalRekapPT(ref dtTotal, rblProgramHibah.SelectedValue, ddlThnUsulan.SelectedValue,
                        ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue, int.Parse(ViewState["IdSkema"].ToString()));

                    lblTotalJmlUsulanRekapPT.Text = dtTotal.Rows[0]["total_jml_usulan"].ToString();
                    lblTotalJmlTdkLolosRekapPT.Text = dtTotal.Rows[0]["total_jml_tdk_lolos"].ToString();
                    lblTotalJmlLolosRekapPT.Text = dtTotal.Rows[0]["total_jml_lolos"].ToString();
                    lblTotalJmlBlmDitetapkanRekapPT.Text = dtTotal.Rows[0]["total_jml_blm_ditetapkan"].ToString();
                    lblTotalJmlDanaRekapPT.Text = objManipData.convertFormatDana(dtTotal.Rows[0]["total_jml_dana"].ToString());
                }
                else
                {
                    lblTotalJmlUsulanRekapPT.Text = "0";
                    lblTotalJmlTdkLolosRekapPT.Text = "0";
                    lblTotalJmlLolosRekapPT.Text = "0";
                    lblTotalJmlBlmDitetapkanRekapPT.Text = "0";
                    lblTotalJmlDanaRekapPT.Text = "0";
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }
        }

        #endregion

        #region Penetapan

        private void isiLvPenetapan(int limit, int offset, int stsEdit)
        {
            if (modelPenetapanUsulanBaru.getJmlDataPenetapan(ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, Guid.Parse(ViewState["IdInstitusi"].ToString()),
                int.Parse(ViewState["IdSkema"].ToString()), tbPencarianPenetapan.Text,
                rblStsPenetapan.SelectedValue, 0, 0))
            {
                lblJmlJudulPenetapan.Text = modelPenetapanUsulanBaru.numOfRecords.ToString();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }

            modelPenetapanUsulanBaru.currentPage = offset;
            modelPenetapanUsulanBaru.rowsPerPage = limit;
            if (stsEdit == 0)
            {
                pagingPenetapan.setPaging(limit, modelPenetapanUsulanBaru.numOfRecords);
            }

            DataTable dt = new DataTable();
            if (modelPenetapanUsulanBaru.listPenetapan(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, Guid.Parse(ViewState["IdInstitusi"].ToString()),
                int.Parse(ViewState["IdSkema"].ToString()), tbPencarianPenetapan.Text,
                rblStsPenetapan.SelectedValue, int.Parse(ddlJmlBarisPenetapan.SelectedValue), offset))
            {
                lvPenetapan.DataSource = dt;
                lvPenetapan.DataBind();

                cekJmlPermanen();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }

            if (modelPenetapanUsulanBaru.numOfRecords < 1)
            {
                pagingPenetapan.setPaging(limit, 1);
            }

            if (rblProgramHibah.SelectedValue == "1")
            {
                lbSimpanPermanen.Visible = false;
                lblSimpanPermanen.Visible = false;
            }
        }

        protected void lbKembaliPenetapan_Click(object sender, EventArgs e)
        {
            tbPencarianPenetapan.Text = string.Empty;
            rblStsPenetapan.SelectedIndex = 0;
            isiGvRekapPT(int.Parse(ddlJmlBarisRekapPT.SelectedValue), 0);
            mvMain.SetActiveView(vRekapPT);
        }

        protected void rblStsPenetapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiLvPenetapan(int.Parse(ddlJmlBarisPenetapan.SelectedValue), 0, bukanEdit);
        }

        protected void lbPencarianPenetapan_Click(object sender, EventArgs e)
        {
            isiLvPenetapan(int.Parse(ddlJmlBarisPenetapan.SelectedValue), 0, bukanEdit);
        }

        protected void ddlJmlBarisPenetapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiLvPenetapan(int.Parse(ddlJmlBarisPenetapan.SelectedValue), 0, bukanEdit);
        }

        protected void lbExcelPenetapan_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Penetapan " + ViewState["NamaInstitusi"].ToString() + ".xlsx");
                if (modelPenetapanUsulanBaru.getExcelPenetapan(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, Guid.Parse(ViewState["IdInstitusi"].ToString()), int.Parse(ViewState["IdSkema"].ToString())))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Data");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);

                    if (dt.Rows.Count > 0)
                    {
                        var endRow = dt.Rows.Count + 1;

                        //Hitung Total
                        ws.Cells[endRow + 1, 18].Formula = $"=SUM(R2:R{endRow})";
                        ws.Cells[endRow + 1, 17].Value = "TOTAL DANA";

                        //Format kolom
                        ws.Cells[2, 11, endRow + 1, 11].Style.Numberformat.Format = "#,##0";
                        ws.Cells[2, 14, endRow + 1, 14].Style.Numberformat.Format = "#,##0";
                        ws.Cells[2, 16, endRow + 1, 16].Style.Numberformat.Format = "#,##0";
                        ws.Cells[2, 18, endRow + 1, 18].Style.Numberformat.Format = "#,##0";
                    }

                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
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

        protected void pagingPenetapan_PageChanging(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelPenetapanUsulanBaru.currentPage = pagingPenetapan.currentPage;
            modelPenetapanUsulanBaru.rowsPerPage = int.Parse(ddlJmlBarisPenetapan.SelectedValue);
            ViewState["currentPenetepan"] = pagingPenetapan.currentPage * int.Parse(ddlJmlBarisPenetapan.SelectedValue);

            if (modelPenetapanUsulanBaru.listPenetapan(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, Guid.Parse(ViewState["IdInstitusi"].ToString()),
                int.Parse(ViewState["IdSkema"].ToString()), tbPencarianPenetapan.Text, rblStsPenetapan.SelectedValue,
                int.Parse(ddlJmlBarisPenetapan.SelectedValue), int.Parse(ViewState["currentPenetepan"].ToString())))
            {
                lvPenetapan.DataSource = dt;
                lvPenetapan.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }
        }

        protected void lvPenetapan_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvPenetapan.EditIndex = e.NewEditIndex;
            int offset = 0;
            if (ViewState["currentPenetepan"] == null)
            {
                offset = 0;
            }
            else
            {
                offset = int.Parse(ViewState["currentPenetepan"].ToString());
            }
            isiLvPenetapan(int.Parse(ddlJmlBarisPenetapan.SelectedValue), offset, edit);
        }

        protected void lvPenetapan_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvPenetapan.EditIndex = -1;
            int offset = 0;
            if (ViewState["currentPenetepan"] == null)
            {
                offset = 0;
            }
            else
            {
                offset = int.Parse(ViewState["currentPenetepan"].ToString());
            }
            isiLvPenetapan(int.Parse(ddlJmlBarisPenetapan.SelectedValue), offset, edit);
        }

        protected void lvPenetapan_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var ddlStsPenetapan = lvPenetapan.Items[e.ItemIndex].FindControl("ddlStsPenetapan") as DropDownList;
            var tbDanaDisetujui = lvPenetapan.Items[e.ItemIndex].FindControl("tbDanaDisetujui") as TextBox;
            string dana = objManipData.removeUnicode(tbDanaDisetujui.Text);
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", " " };
            foreach (var c in charsToRemove)
            {
                dana = dana.Replace(c, string.Empty);
            }
            dana = Regex.Replace(dana, "[^0-9.]", "");

            //Cek Isian
            List<string> isianKosong = new List<string>();
            if (ddlStsPenetapan.SelectedValue == "9") isianKosong.Add("Status penetapan belum dipilih");
            if (ddlStsPenetapan.SelectedValue == "1")
            {
                if (dana.Trim().Length == 0 || dana == "0") isianKosong.Add("Jumlah dana disetujui belum diisi");
            }

            if (isianKosong.Count > 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi",
                   "" + string.Join(", ", isianKosong.ToArray()));
                return;
            }

            Guid.TryParse(lvPenetapan.DataKeys[e.ItemIndex]["id_transaksi_kegiatan"].ToString(), out Guid id_transaksi_kegiatan);
            int.TryParse(lvPenetapan.DataKeys[e.ItemIndex]["id_skema"].ToString(), out int id_skema);
            string kd_klaster = lvPenetapan.DataKeys[e.ItemIndex]["kd_klaster"].ToString();
            decimal.TryParse(lvPenetapan.DataKeys[e.ItemIndex]["rerata_nilai"].ToString(), out decimal rerata_nilai).ToString();
            Guid.TryParse(lvPenetapan.DataKeys[e.ItemIndex]["id_usulan_kegiatan"].ToString(), out Guid id_usulan_kegiatan);
            Guid.TryParse(lvPenetapan.DataKeys[e.ItemIndex]["id_personal"].ToString(), out Guid id_personal);
            Guid.TryParse(lvPenetapan.DataKeys[e.ItemIndex]["id_institusi"].ToString(), out Guid id_institusi);
            string kd_tahapan_kegiatan = lvPenetapan.DataKeys[e.ItemIndex]["kd_tahapan_kegiatan"].ToString();

            // Cek apakah usulan sudah ada rerata nilai
            string rerataString = rerata_nilai.ToString();
            int cekNilai = int.Parse(rerataString.Substring(0, 1)); // Rerata nilai 1 gidit dari kiri

            if (cekNilai < 1)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Penetapan tidak bisa dilakukan " +
                    "karena usulan belum dinilai oleh reviewer");
                return;
            }

            // Cek jika statusnya lolos, apakah dana penetapan melebihi rerata rekomendasi
            //if (ddlStsPenetapan.SelectedValue == "1")
            //{
            //    decimal rekomendasiDana = decimal.Parse(lvPenetapan.DataKeys[e.ItemIndex]["rerata_rek_dana"].ToString());
            //    if (int.Parse(dana) > rekomendasiDana)
            //    {
            //        noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Dana disetujui tidak boleh melebihi " +
            //            "dana rekomendasi dari reviewer");
            //        return;
            //    }
            //}

            if (tbDanaDisetujui.Text.Length == 0)
            {
                tbDanaDisetujui.Text = "0";
            }

            if (ddlStsPenetapan.SelectedValue == "0")
            {
                tbDanaDisetujui.Text = "0";
            }

            if (modelPenetapanUsulanBaru.insupPenetapan(id_transaksi_kegiatan, id_skema, ddlThnUsulan.SelectedValue,
                kd_klaster, ddlStsPenetapan.SelectedValue, rerata_nilai.ToString().Replace(",", "."),
                decimal.Parse(dana), ddlThnPelaksanaan.SelectedValue, id_usulan_kegiatan, id_personal,
                id_institusi, kd_tahapan_kegiatan))
            {
                lvPenetapan.EditIndex = -1;
                int offset = 0;
                if (ViewState["currentPenetepan"] == null)
                {
                    offset = 0;
                }
                else
                {
                    offset = int.Parse(ViewState["currentPenetepan"].ToString());
                }
                isiLvPenetapan(int.Parse(ddlJmlBarisPenetapan.SelectedValue), offset, edit);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Data berhasil disimpan...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }
        }

        protected void lvPenetapan_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (lvPenetapan.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
            {
                var drv = (DataRowView)e.Item.DataItem;
                var ddlStsPenetapan = e.Item.FindControl("ddlStsPenetapan") as DropDownList;
                isiddlStsPenetapan(ref ddlStsPenetapan);
                ddlStsPenetapan.SelectedValue = drv["kd_sts_penetapan_pemenang"].ToString();
            }

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DataRowView drv1 = (DataRowView)e.Item.DataItem;
                Label lblNoPenetapan = (Label)e.Item.FindControl("lblNoPenetapan");
                lblNoPenetapan.Text = (e.Item.DataItemIndex + 1 + int.Parse(ddlJmlBarisPenetapan.SelectedValue) * (pagingPenetapan.currentPage)).ToString();

                LinkButton lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                if (rblProgramHibah.SelectedValue == "1")
                {
                    lbEdit.Visible = false;
                }
            }
        }

        private void isiddlStsPenetapan(ref DropDownList ddlStsPenetapan)
        {
            var status = new Dictionary<int, string>()
            {
                { 9, "-- Pilih --" },
                { 1, "Lolos" },
                { 0, "Tdk. Lolos" }
            };

            ddlStsPenetapan.DataSource = status;
            ddlStsPenetapan.DataTextField = "Value";
            ddlStsPenetapan.DataValueField = "Key";
            ddlStsPenetapan.DataBind();
            ddlStsPenetapan.SelectedIndex = 0;
        }

        protected void cekJmlPermanen()
        {
            DataTable dt = new DataTable();
            if (modelPenetapanUsulanBaru.getJmlPermanen(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, Guid.Parse(ViewState["IdInstitusi"].ToString()),
                int.Parse(ViewState["IdSkema"].ToString())))
            {
                if (dt.Rows.Count > 0)
                {
                    int jmlPermanen = int.Parse(dt.Rows[0]["jml_data"].ToString());
                    if (jmlPermanen > 0)
                    {
                        lbSimpanPermanen.Visible = false;
                        lblSimpanPermanen.Visible = true;
                    }
                    else
                    {
                        lbSimpanPermanen.Visible = true;
                        lblSimpanPermanen.Visible = false;
                    }
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }
        }

        protected void lbSimpanPermanen_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (modelPenetapanUsulanBaru.getJmlBlmDitetapkan(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, Guid.Parse(ViewState["IdInstitusi"].ToString()),
                int.Parse(ViewState["IdSkema"].ToString())))
            {
                if (dt.Rows.Count > 0)
                {
                    int jmlBlmDitetapkan = int.Parse(dt.Rows[0]["jml_data"].ToString());
                    if (jmlBlmDitetapkan > 0)
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       jmlBlmDitetapkan.ToString() + " Judul yang belum ditetapkan");
                        return;
                    }
                    else
                    {
                        if (!modelPenetapanUsulanBaru.setSimpanPermanen(ddlThnUsulan.SelectedValue,
                            ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue,
                            Guid.Parse(ViewState["IdInstitusi"].ToString()), int.Parse(ViewState["IdSkema"].ToString())))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
                            return;
                        }
                        else
                        {
                            isiLvPenetapan(int.Parse(ddlJmlBarisPenetapan.SelectedValue), 0, bukanEdit);
                            noty.Notify(this.Page, uiNotify.NotifyType.success, "Infomasi", "Simpan permanen berhasil");
                        }
                    }
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }
        }

        #endregion

        protected void lbBukaPermanen_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (modelPenetapanUsulanBaru.cekPermanenPenetapan(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ddlTahapan.SelectedValue, Guid.Parse(ViewState["IdInstitusi"].ToString()),
                int.Parse(ViewState["IdSkema"].ToString()), tbPencarianPenetapan.Text,
                rblStsPenetapan.SelectedValue, 0, 0))
            {
                if (dt.Rows.Count > 0)
                {
                    int jmlPermanen = int.Parse(dt.Rows[0]["jml_permanen"].ToString());
                    int jmlData = int.Parse(dt.Rows[0]["jml_data"].ToString());
                    if (jmlPermanen == jmlData)
                    {
                        objModal.ShowModal(this.Page, "mdlBukaPermanen");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Buka simpan permanen tidak bisa dilakukan, karena ada reviewer yang belum simpan permanen");
                    }
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
            }
        }

        protected void lbSetBukaPermanen_Click(object sender, EventArgs e)
        {
            if (!modelPenetapanUsulanBaru.setBukaPermanen(ddlThnUsulan.SelectedValue,            
                ddlThnPelaksanaan.SelectedValue, ddlTahapan.SelectedValue,            
                Guid.Parse(ViewState["IdInstitusi"].ToString()), int.Parse(ViewState["IdSkema"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPenetapanUsulanBaru.errorMessage);
                return;
            }
            else
            {
                isiLvPenetapan(int.Parse(ddlJmlBarisPenetapan.SelectedValue), 0, bukanEdit);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Infomasi", "Buka simpan permanen berhasil");
            }
        }
    }
}