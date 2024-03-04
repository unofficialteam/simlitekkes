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
    public partial class monitoringLaporanAkhir : System.Web.UI.UserControl
    {

        Models.login objLogin;

        Core.manipulasiData objManipData = new Core.manipulasiData();

        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiNotify noty = new uiNotify();
        uiGridView obj_uiGridView = new uiGridView();
        uiModal objModal = new uiModal();
        Models.PT.monitoringLapAkhirThn modelMonitoringLapAkhirThn = new Models.PT.monitoringLapAkhirThn();
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
            if (modelMonitoringLapAkhirThn.listRekapSkema(ref dt, ddlTahunPelaksanaan.SelectedValue, KD_JENIS_KEGIATAN,
                IdInstitusi))
            {
                gvRekapSkema.DataSource = dt;
                gvRekapSkema.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapAkhirThn.errorMessage);
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

                lblNamaSkemaDaftarLapAkhirThn.Text = ViewState["NamaSkema"].ToString();
                lblThnPelaksanaanDaftarLapAkhirThn.Text = ddlTahunPelaksanaan.Text.ToString();

                isiLvDaftarLapAkhirThn(int.Parse(ddlJmlBarisDaftarLapAkhirThn.SelectedValue), 0);
                tbPencarianDaftarLapAkhirThn.Text = string.Empty;
                mvMain.SetActiveView(vDaftarLapAkhirThn);
            }
        }

        #endregion

        #region Daftar Laporan Akhir Thn

        protected void lbKembaliDaftarLapAkhirThn_Click(object sender, EventArgs e)
        {
            tbPencarianDaftarLapAkhirThn.Text = string.Empty;
            rblStsPelaksanaan.SelectedIndex = 0;
            isiGvRekapSkema();
            mvMain.SetActiveView(vRekapSkema);
        }

        protected void rblStsPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiLvDaftarLapAkhirThn(int.Parse(ddlJmlBarisDaftarLapAkhirThn.SelectedValue), 0);
        }

        protected void lbPencarianDaftarLapAkhirThn_Click(object sender, EventArgs e)
        {
            isiLvDaftarLapAkhirThn(int.Parse(ddlJmlBarisDaftarLapAkhirThn.SelectedValue), 0);
        }

        protected void ddlJmlBarisDaftarLapAkhirThn_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiLvDaftarLapAkhirThn(int.Parse(ddlJmlBarisDaftarLapAkhirThn.SelectedValue), 0);
        }

        protected void lbExcelDaftarLapAkhirThn_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Laporan Akhir Tahun " + ViewState["NamaSkema"].ToString() + " Pendanaan Tahun " +
                    ddlTahunPelaksanaan.SelectedItem + ".xlsx");
                if (modelMonitoringLapAkhirThn.getExcelDaftarLapAkhir(ref dt, ddlTahunPelaksanaan.SelectedValue,
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
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapAkhirThn.errorMessage);
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

        private void isiLvDaftarLapAkhirThn(int limit, int offset)
        {
            if (modelMonitoringLapAkhirThn.getJmlDataLapAkhir(ddlTahunPelaksanaan.SelectedValue,
                int.Parse(ViewState["IdSkema"].ToString()), IdInstitusi, rblStsPelaksanaan.SelectedValue,
                tbPencarianDaftarLapAkhirThn.Text, 0, 0))
            {
                lblJmlJudulPenetapan.Text = modelMonitoringLapAkhirThn.numOfRecords.ToString();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapAkhirThn.errorMessage);
            }

            pagingDaftarLapAkhirThn.currentPage = offset;
            pagingDaftarLapAkhirThn.setPaging(limit, modelMonitoringLapAkhirThn.numOfRecords);

            modelMonitoringLapAkhirThn.currentPage = offset;
            modelMonitoringLapAkhirThn.rowsPerPage = limit;

            DataTable dt = new DataTable();
            if (modelMonitoringLapAkhirThn.listDaftarLapAkhir(ref dt, ddlTahunPelaksanaan.SelectedValue,
                int.Parse(ViewState["IdSkema"].ToString()), IdInstitusi, rblStsPelaksanaan.SelectedValue,
                tbPencarianDaftarLapAkhirThn.Text, int.Parse(ddlJmlBarisDaftarLapAkhirThn.SelectedValue), offset))
            {
                lvDaftarLapAkhirThn.DataSource = dt;
                lvDaftarLapAkhirThn.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapAkhirThn.errorMessage);
            }

            if (modelMonitoringLapAkhirThn.numOfRecords < 1)
            {
                pagingDaftarLapAkhirThn.setPaging(limit, 1);
            }
        }

        protected void pagingDaftarLapAkhirThn_PageChanging(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelMonitoringLapAkhirThn.currentPage = pagingDaftarLapAkhirThn.currentPage;
            modelMonitoringLapAkhirThn.rowsPerPage = int.Parse(ddlJmlBarisDaftarLapAkhirThn.SelectedValue);
            ViewState["currentPage"] = pagingDaftarLapAkhirThn.currentPage * int.Parse(ddlJmlBarisDaftarLapAkhirThn.SelectedValue);

            if (modelMonitoringLapAkhirThn.listDaftarLapAkhir(ref dt, ddlTahunPelaksanaan.SelectedValue,
                int.Parse(ViewState["IdSkema"].ToString()), IdInstitusi, rblStsPelaksanaan.SelectedValue,
                tbPencarianDaftarLapAkhirThn.Text, int.Parse(ddlJmlBarisDaftarLapAkhirThn.SelectedValue),
                int.Parse(ViewState["currentPage"].ToString())))
            {
                lvDaftarLapAkhirThn.DataSource = dt;
                lvDaftarLapAkhirThn.DataBind();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelMonitoringLapAkhirThn.errorMessage);
            }
        }

        protected void lvDaftarLapAkhirThn_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DataRowView drv1 = (DataRowView)e.Item.DataItem;
                Label lblNoPenetapan = (Label)e.Item.FindControl("lblNoPenetapan");
                lblNoPenetapan.Text = (e.Item.DataItemIndex + 1 + int.Parse(ddlJmlBarisDaftarLapAkhirThn.SelectedValue) * (pagingDaftarLapAkhirThn.currentPage)).ToString();

                LinkButton lbUnduhLaporanAkhirThn = (LinkButton)e.Item.FindControl("lbUnduhLaporanAkhirThn");
                string kd_sts_pelaksanaan = lvDaftarLapAkhirThn.DataKeys[e.Item.DataItemIndex]["kd_sts_pelaksanaan"].ToString();
                if (kd_sts_pelaksanaan == "1")
                {
                    lbUnduhLaporanAkhirThn.ForeColor = System.Drawing.Color.Green;
                    lbUnduhLaporanAkhirThn.Enabled = true;
                }
            }
        }

        protected void lvDaftarLapAkhirThn_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());

            ViewState["idTransaksiKegiatan"] = lvDaftarLapAkhirThn.DataKeys[index]["id_transaksi_kegiatan"].ToString();

            switch (e.CommandName)
            {
                case "unduhLaporanAkhirThn":
                    ktPdfLaporanKemajuanKontrol.unduhLapKemajuan(ViewState["idTransaksiKegiatan"].ToString());

                    break;
            }
        }

        #endregion
    }
}