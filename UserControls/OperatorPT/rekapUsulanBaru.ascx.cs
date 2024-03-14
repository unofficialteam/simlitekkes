using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

namespace simlitekkes.UserControls.OperatorPT
{

    public partial class rekapUsulanBaru : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.PT.OperatorPT objOperatorPT = new Models.PT.OperatorPT();
        uiGridView obj_uiGridView = new uiGridView();
        uiListView obj_uiLisview = new uiListView();
        uiNotify noty = new uiNotify();

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                if (ViewState["kdproghibah"] == null)
                {
                    ViewState["kdproghibah"] = "1"; // desentralisasi
                    lbdesentralisasi.CssClass = "btn btn-outline-success";
                    lbPenugasan.CssClass = "btn btn-outline-secondary";
                }
                isiDdlThnPelaksanaan();
                isiRekap();
                mvRekapUsulanBaru.SetActiveView(vDaftar);
            }
        }

        private void isiDdlThnPelaksanaan()
        {
            ddlThn.Items.Clear();
            //ddlThn.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlThn.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void isiRekap()
        {
            DataTable dt = new DataTable();
            if (objOperatorPT.ListRekapUsulanBaru(ref dt, objLogin.idInstitusi,
                ddlThn.SelectedValue, ViewState["kdproghibah"].ToString()))
            {
                ViewState["tahun_ke"] = "";
                gvDaftarSkema.DataSource = dt;
                gvDaftarSkema.DataBind();

                if (dt.Rows.Count > 0)
                {
                    lblJmlUsulan.Text = Convert.ToDecimal(dt.Compute("SUM(jml_usulan)", string.Empty)).ToString("N0");
                    lblJmlUsulanDikirim.Text = Convert.ToDecimal(dt.Compute("SUM(jml_dikirim)", string.Empty)).ToString("N0");
                    lblJmlDisetujui.Text = Convert.ToDecimal(dt.Compute("SUM(jml_disetujui)", string.Empty)).ToString("N0");
                    lblJmlTdkSetuju.Text = Convert.ToDecimal(dt.Compute("SUM(jml_tdk_disetujui)", string.Empty)).ToString("N0");
                    lblJmlBlmDitinjau.Text = Convert.ToDecimal(dt.Compute("SUM(jml_belum_diapprove)", string.Empty)).ToString("N0");
                }
                else
                {
                    lblJmlUsulan.Text = "0";
                    lblJmlDisetujui.Text = "0";
                    lblJmlTdkSetuju.Text = "0";
                    lblJmlBlmDitinjau.Text = "0";
                }
            }
        }

        private void isiUsulanBaru(int idxPage)
        {
            if (!objOperatorPT.getJmlData(objLogin.idInstitusi, ddlThn.SelectedValue,
                     int.Parse(ViewState["IdSkema"].ToString()), rblStatusApproval.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objOperatorPT.errorMessage);

            //NEW PAGING CONTROL
            lblJmlUsulanBaru.Text = objOperatorPT.numOfRecords.ToString();
            ktPagging.currentPage = idxPage;
            ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), objOperatorPT.numOfRecords);

            objOperatorPT.currentPage = idxPage;
            objOperatorPT.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!objOperatorPT.ListUsulanBaruPaging(objLogin.idInstitusi, ddlThn.SelectedValue,
                    int.Parse(ViewState["IdSkema"].ToString()), rblStatusApproval.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objOperatorPT.errorMessage);
                return;
            }

            try
            {
                lvDaftarUsulanKonfirmasi.DataSource = objOperatorPT.currentRecords;
                lvDaftarUsulanKonfirmasi.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
                        if (objOperatorPT.numOfRecords < 1)
            {
                ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), 1);
            }
        }

        private void refreshPaging()
        {
            //string kode = ViewState["kode"].ToString();
            //var idInstitusi = Guid.Parse(ViewState["IdInstitusi"].ToString());

            objOperatorPT.currentPage = ktPagging.currentPage;
            objOperatorPT.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!objOperatorPT.ListUsulanBaruPaging(objLogin.idInstitusi, ddlThn.SelectedValue,
                int.Parse(ViewState["IdSkema"].ToString()), rblStatusApproval.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    objOperatorPT.errorMessage);
                return;
            }

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiLisview.bindToListView(ref lvDaftarUsulanKonfirmasi, objOperatorPT.currentRecords);
        }

        protected void lbdesentralisasi_Click(object sender, EventArgs e)
        {
            lbPenugasan.CssClass = "btn btn-outline-secondary";
            lbdesentralisasi.CssClass = "btn btn-outline-success";
            ViewState["kdproghibah"] = "1";
            isiRekap();
        }

        protected void lbPenugasan_Click(object sender, EventArgs e)
        {
            lbPenugasan.CssClass = "btn btn-outline-success";
            lbdesentralisasi.CssClass = "btn btn-outline-secondary";
            ViewState["kdproghibah"] = "6";
            isiRekap();
        }

        protected void ddlThn_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiRekap();
        }

        protected void lvDaftarUsulanKonfirmasi_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblNomor = (Label)e.Item.FindControl("lblNomor");
                lblNomor.Text = (e.Item.DataItemIndex + 1 + int.Parse(ddlJmlBaris.SelectedValue) * (ktPagging.currentPage)).ToString();
            }
        }

        protected void lvDaftarUsulanKonfirmasi_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "UnduhPdf")
            {
                var itemIndex = int.Parse(e.CommandArgument.ToString());
                var id_usulan_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["id_usulan_kegiatan"].ToString();
                string kd_jenis_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["kd_jenis_kegiatan"].ToString();
                
                if(kd_jenis_kegiatan=="1")
                    pdfUsulanLengkap.UnduhProposalLengkap(id_usulan_kegiatan);
                else if(kd_jenis_kegiatan=="2")
                    pdfUsulanLengkapAbdimas.UnduhProposalLengkap(id_usulan_kegiatan);

            }
        }

        protected void lbKembali_Click(object sender, EventArgs e)
        {
            mvRekapUsulanBaru.SetActiveView(vDaftar);
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiUsulanBaru(0);
        }

        protected void Paging_PageChanging(object sender, EventArgs e)
        {
            refreshPaging();
        }

        protected void lbKembaliKeSkema_Click(object sender, EventArgs e)
        {
            mvRekapUsulanBaru.SetActiveView(vDaftar);
        }

        protected void gvDaftarSkema_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["IdSkema"] = gvDaftarSkema.DataKeys[e.RowIndex]["id_skema"].ToString();
            lblNamaSkema.Text = gvDaftarSkema.DataKeys[e.RowIndex]["nama_skema"].ToString();
            lblTahunUsulan2.Text = ddlThn.SelectedItem.Text;

            rblStatusApproval.SelectedValue = "0";
            isiUsulanBaru(0);
            mvRekapUsulanBaru.SetActiveView(vDaftarUsulan);
        }

        protected void rblStatusApproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiUsulanBaru(0);
        }

        protected void lbExportExcel_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format(" usulan " + rblStatusApproval.SelectedItem.Text + " " + lblNamaSkema.Text + " tahun {0}.xlsx", ddlThn.SelectedItem.Text);
                var dt = new DataTable();
                if (objOperatorPT.ListUsulanBaruExcel(ref dt, objLogin.idInstitusi, ddlThn.SelectedValue,
                    int.Parse(ViewState["IdSkema"].ToString()), rblStatusApproval.SelectedValue))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("lblNamaSkema.Text");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", objOperatorPT.errorMessage);
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
    }

}