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

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class perubahanJudul : System.Web.UI.UserControl
    {
        Models.login objLogin;

        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiModal objModal = new uiModal();
        Models.OperatorPenelitianPusdik.daftarPerubahanJudul modelPerubahanJudul = new Models.OperatorPenelitianPusdik.daftarPerubahanJudul();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            
            if (!IsPostBack)
            {
                isiDdlThnPelaksanaan();
                isiLvPerubahanJudul(int.Parse(ddlJmlBaris.SelectedValue), 0);
            }
        }

        private void isiDdlThnPelaksanaan()
        {
            ddlTahunPelaksanaan.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2022; i--)
            {
                ddlTahunPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlTahunPelaksanaan.SelectedIndex = 0;
        }

        protected void ddlTahunPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiLvPerubahanJudul(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void lbCariNamaKetua_Click(object sender, EventArgs e)
        {
            isiLvPerubahanJudul(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiLvPerubahanJudul(int.Parse(ddlJmlBaris.SelectedValue), 0);
        }

        protected void lbExcelPerubahanJudul_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Perubahan Judul Pendanaan Tahun " + 
                    ddlTahunPelaksanaan.SelectedItem + ".xlsx");
                if (modelPerubahanJudul.excelPerubahanJudul(ref dt, ddlTahunPelaksanaan.SelectedValue))
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
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPerubahanJudul.errorMessage);
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

        protected void lvPerubahanJudul_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DataRowView drv1 = (DataRowView)e.Item.DataItem;
                Label lblNo = (Label)e.Item.FindControl("lblNo");
                lblNo.Text = (e.Item.DataItemIndex + 1 + int.Parse(ddlJmlBaris.SelectedValue) * (pagingPerubahanJudul.currentPage)).ToString();
            }
        }

        protected void pagingPerubahanJudul_PageChanging(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelPerubahanJudul.currentPage = pagingPerubahanJudul.currentPage;
            modelPerubahanJudul.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            ViewState["currentPage"] = pagingPerubahanJudul.currentPage * int.Parse(ddlJmlBaris.SelectedValue);

            if (modelPerubahanJudul.listPerubahanJudul(ref dt, ddlTahunPelaksanaan.SelectedValue,
                tbPencarianNama.Text, int.Parse(ddlJmlBaris.SelectedValue),
                int.Parse(ViewState["currentPage"].ToString())))
            {
                lvPerubahanJudul.DataSource = dt;
                lvPerubahanJudul.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPerubahanJudul.errorMessage);
            }
        }

        private void isiLvPerubahanJudul(int limit, int offset)
        {
            if (modelPerubahanJudul.getJmlDataPerubahanJudul(ddlTahunPelaksanaan.SelectedValue,
                tbPencarianNama.Text, 0, 0))
            {
                lblJmlJudulPerubahan.Text = modelPerubahanJudul.numOfRecords.ToString();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPerubahanJudul.errorMessage);
            }

            pagingPerubahanJudul.currentPage = offset;
            pagingPerubahanJudul.setPaging(limit, modelPerubahanJudul.numOfRecords);

            modelPerubahanJudul.currentPage = offset;
            modelPerubahanJudul.rowsPerPage = limit;

            DataTable dt = new DataTable();
            if (modelPerubahanJudul.listPerubahanJudul(ref dt, ddlTahunPelaksanaan.SelectedValue,
                tbPencarianNama.Text, int.Parse(ddlJmlBaris.SelectedValue), offset))
            {
                lvPerubahanJudul.DataSource = dt;
                lvPerubahanJudul.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPerubahanJudul.errorMessage);
            }

            if (modelPerubahanJudul.numOfRecords < 1)
            {
                pagingPerubahanJudul.setPaging(limit, 1);
            }
        }
    }
}