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

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class monitoringMonevEksternal : System.Web.UI.UserControl
    {
        Models.login objLogin;

        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiGridView obj_uiGridView = new uiGridView();
        uiModal objModal = new uiModal();

        Models.PT.monitoringMonevEksternal modelHasilMonevEksternal = new Models.PT.monitoringMonevEksternal();

        private Guid IdInstitusi
        {
            get
            {
                if (objLogin.idInstitusi == null) return Guid.Empty;
                return objLogin.idInstitusi;
            }
            set
            {
                ViewState["IdInstitusi"] = value;
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
                isiDdlThnPelaksanaan();
                isiGvData(int.Parse(ddlJmlBaris.SelectedValue), 0);
            }
        }

        private void isiDdlThnPelaksanaan()
        {
            ddlThnPelaksanaan.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlThnPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlThnPelaksanaan.SelectedIndex = 0;
        }

        private void isiGvData(int limit, int offset)
        {
            if (!modelHasilMonevEksternal.getJmlData(ddlThnPelaksanaan.SelectedValue, IdInstitusi,
                tbCari.Text, 0, 0))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelHasilMonevEksternal.errorMessage);

            pagingData.currentPage = offset;
            pagingData.setPaging(limit, modelHasilMonevEksternal.numOfRecords);

            modelHasilMonevEksternal.currentPage = offset;
            modelHasilMonevEksternal.rowsPerPage = limit;

            DataTable dt = new DataTable();
            if (modelHasilMonevEksternal.listData(ref dt, ddlThnPelaksanaan.SelectedValue, IdInstitusi, 
                tbCari.Text, int.Parse(ddlJmlBaris.SelectedValue), offset))
            {
                gvData.DataSource = dt;
                gvData.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelHasilMonevEksternal.errorMessage);
            }

            if (modelHasilMonevEksternal.numOfRecords < 1)
            {
                pagingData.setPaging(limit, 1);
            }
        }

        protected void ddlTahunPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvData(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void lbCari_Click(object sender, EventArgs e)
        {
            isiGvData(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvData(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void lbUnduhExcel_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Hasil Monev Eksternal Penelitian Tahun " + ddlThnPelaksanaan.SelectedItem.ToString() + ".xlsx");
                if (modelHasilMonevEksternal.getExcel(ref dt, ddlThnPelaksanaan.SelectedValue, IdInstitusi))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Data");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);

                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelHasilMonevEksternal.errorMessage);
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

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNo = (Label)e.Row.FindControl("lblNo");
                lblNo.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBaris.SelectedValue) * (pagingData.currentPage)).ToString();
            }
        }

        protected void pagingData_PageChanging(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelHasilMonevEksternal.currentPage = pagingData.currentPage;
            modelHasilMonevEksternal.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            ViewState["currentPagingData"] = pagingData.currentPage * int.Parse(ddlJmlBaris.SelectedValue);

            if (modelHasilMonevEksternal.listData(ref dt, ddlThnPelaksanaan.SelectedValue, IdInstitusi, tbCari.Text,
                int.Parse(ddlJmlBaris.SelectedValue), int.Parse(ViewState["currentPagingData"].ToString())))
            {
                gvData.DataSource = dt;
                gvData.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelHasilMonevEksternal.errorMessage);
            }
        }
    }
}