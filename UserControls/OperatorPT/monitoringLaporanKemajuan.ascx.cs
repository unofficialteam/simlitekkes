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
    public partial class monitoringLaporanKemajuan : System.Web.UI.UserControl
    {
        Models.login objLogin;

        Core.manipulasiData objManipData = new Core.manipulasiData();

        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiGridView obj_uiGridView = new uiGridView();
        uiModal objModal = new uiModal();
        Models.PT.monitoringLapKemajuan modelMonitoringLapKemajuan = new Models.PT.monitoringLapKemajuan();
        string KD_JENIS_KEGIATAN = "0";

        private Guid IdInstitusi
        {
            get
            {
                if (objLogin.idInstitusi == null) return Guid.Empty;
                return objLogin.idInstitusi;
            }
            set
            {
                ViewState["IdUsulanKegiatan"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (objLogin.idPeran == 6)
            {
                KD_JENIS_KEGIATAN = "1";
            }
            else
            {
                KD_JENIS_KEGIATAN = "2";
            }

            if (!IsPostBack)
            {
                isiDdlThnPelaksanaan();
                mvMain.SetActiveView(vRekapSkema);
                isiGvRekapSkema();
            }
        }

        #region Rekap Skema

        private void isiDdlThnPelaksanaan()
        {
            ddlTahunPelaksanaan.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2021; i--)
            {
                ddlTahunPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlTahunPelaksanaan.SelectedIndex = 0;
        }

        protected void ddlTahunPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvRekapSkema();
        }

        private void isiGvRekapSkema()
        {
            var dt = new DataTable();
            if (modelMonitoringLapKemajuan.listRekapSkema(ref dt, ddlTahunPelaksanaan.SelectedValue, KD_JENIS_KEGIATAN,
                IdInstitusi))
            {
                gvRekapSkema.DataSource = dt;
                gvRekapSkema.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapKemajuan.errorMessage);
            }
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

                lblNamaSkemaDaftarLapKemajuan.Text = ViewState["NamaSkema"].ToString();
                lblThnPelaksanaanDaftarLapKemajuan.Text = ddlTahunPelaksanaan.Text.ToString();

                isiLvDaftarLapKemajuan(int.Parse(ddlJmlBarisDaftarLapKemajuan.SelectedValue), 0);
                tbPencarianDaftarLapKemajuan.Text = string.Empty;
                mvMain.SetActiveView(vDaftarLapKemajuan);
            }
        }

        #endregion

        #region Daftar Laporan Kemajuan

        protected void lbKembaliDaftarLapKemajuan_Click(object sender, EventArgs e)
        {
            tbPencarianDaftarLapKemajuan.Text = string.Empty;
            rblStsPelaksanaan.SelectedIndex = 0;
            isiGvRekapSkema();
            mvMain.SetActiveView(vRekapSkema);
        }

        protected void rblStsPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiLvDaftarLapKemajuan(int.Parse(ddlJmlBarisDaftarLapKemajuan.SelectedValue), 0);
        }

        protected void lbPencarianDaftarLapKemajuan_Click(object sender, EventArgs e)
        {
            isiLvDaftarLapKemajuan(int.Parse(ddlJmlBarisDaftarLapKemajuan.SelectedValue), 0);
        }

        protected void ddlJmlBarisDaftarLapKemajuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiLvDaftarLapKemajuan(int.Parse(ddlJmlBarisDaftarLapKemajuan.SelectedValue), 0);
        }

        protected void lbExcelDaftarLapKemajuan_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Laporan Kemajuan " + ViewState["NamaSkema"].ToString() + " Pendanaan Tahun " +
                    ddlTahunPelaksanaan.SelectedItem + ".xlsx");
                if (modelMonitoringLapKemajuan.getExcelDaftarLapKemajuan(ref dt, ddlTahunPelaksanaan.SelectedValue,
                int.Parse(ViewState["IdSkema"].ToString()), IdInstitusi))
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
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapKemajuan.errorMessage);
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

        private void isiLvDaftarLapKemajuan(int limit, int offset)
        {
            if (modelMonitoringLapKemajuan.getJmlDataLapKemajuan(ddlTahunPelaksanaan.SelectedValue,
                int.Parse(ViewState["IdSkema"].ToString()), IdInstitusi, rblStsPelaksanaan.SelectedValue, 
                tbPencarianDaftarLapKemajuan.Text, 0, 0))
            {
                lblJmlJudulPenetapan.Text = modelMonitoringLapKemajuan.numOfRecords.ToString();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapKemajuan.errorMessage);
            }

            pagingDaftarLapKemajuan.currentPage = offset;
            pagingDaftarLapKemajuan.setPaging(limit, modelMonitoringLapKemajuan.numOfRecords);

            modelMonitoringLapKemajuan.currentPage = offset;
            modelMonitoringLapKemajuan.rowsPerPage = limit;

            DataTable dt = new DataTable();
            if (modelMonitoringLapKemajuan.listDaftarLapKemajuan(ref dt, ddlTahunPelaksanaan.SelectedValue,
                int.Parse(ViewState["IdSkema"].ToString()), IdInstitusi, rblStsPelaksanaan.SelectedValue,
                tbPencarianDaftarLapKemajuan.Text, int.Parse(ddlJmlBarisDaftarLapKemajuan.SelectedValue), offset))
            {
                lvDaftarLapKemajuan.DataSource = dt;
                lvDaftarLapKemajuan.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapKemajuan.errorMessage);
            }

            if (modelMonitoringLapKemajuan.numOfRecords < 1)
            {
                pagingDaftarLapKemajuan.setPaging(limit, 1);
            }
        }

        protected void pagingDaftarLapKemajuan_PageChanging(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelMonitoringLapKemajuan.currentPage = pagingDaftarLapKemajuan.currentPage;
            modelMonitoringLapKemajuan.rowsPerPage = int.Parse(ddlJmlBarisDaftarLapKemajuan.SelectedValue);
            ViewState["currentPage"] = pagingDaftarLapKemajuan.currentPage * int.Parse(ddlJmlBarisDaftarLapKemajuan.SelectedValue);

            if (modelMonitoringLapKemajuan.listDaftarLapKemajuan(ref dt, ddlTahunPelaksanaan.SelectedValue,
                int.Parse(ViewState["IdSkema"].ToString()), IdInstitusi, rblStsPelaksanaan.SelectedValue,
                tbPencarianDaftarLapKemajuan.Text, int.Parse(ddlJmlBarisDaftarLapKemajuan.SelectedValue),
                int.Parse(ViewState["currentPage"].ToString())))
            {
                lvDaftarLapKemajuan.DataSource = dt;
                lvDaftarLapKemajuan.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapKemajuan.errorMessage);
            }
        }

        protected void lvDaftarLapKemajuan_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DataRowView drv1 = (DataRowView)e.Item.DataItem;
                Label lblNoPenetapan = (Label)e.Item.FindControl("lblNoPenetapan");
                lblNoPenetapan.Text = (e.Item.DataItemIndex + 1 + int.Parse(ddlJmlBarisDaftarLapKemajuan.SelectedValue) * (pagingDaftarLapKemajuan.currentPage)).ToString();

                LinkButton lbUnduhLaporanKemajuan = (LinkButton)e.Item.FindControl("lbUnduhLaporanKemajuan");
                string kd_sts_pelaksanaan = lvDaftarLapKemajuan.DataKeys[e.Item.DataItemIndex]["kd_sts_pelaksanaan"].ToString();
                if (kd_sts_pelaksanaan == "1")
                {
                    lbUnduhLaporanKemajuan.ForeColor = System.Drawing.Color.Green;
                    lbUnduhLaporanKemajuan.Enabled = true;
                }
            }
        }

        protected void lvDaftarLapKemajuan_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());

            ViewState["idTransaksiKegiatan"] = lvDaftarLapKemajuan.DataKeys[index]["id_transaksi_kegiatan"].ToString();

            switch (e.CommandName)
            {
                case "unduhLaporanKemajuan":
                    ktPdfLaporanKemajuanKontrol.unduhLapKemajuan(ViewState["idTransaksiKegiatan"].ToString());

                    break;
            }
        }

        #endregion

    }
}