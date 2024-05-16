using simlitekkes.Models.OperatorPenelitianPusdik;
using simlitekkes.UIControllers;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Web;
using System.IO;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class rekapUsulanBaru : System.Web.UI.UserControl
    {
        Models.login objLogin;
        daftarOperatorPusdik objOperatorDRPM = new daftarOperatorPusdik();
        uiGridView obj_uiGridView = new uiGridView();
        uiListView obj_uiLisview = new uiListView();
        uiNotify noty = new uiNotify();
        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiDdlThnPelaksanaan();
                if (ViewState["kdproghibah"] == null)
                {
                    ViewState["kdproghibah"] = "1"; //desentralisasi
                    lbdesentralisasi.CssClass = "btn btn-outline-success";
                    lbPenugasan.CssClass = "btn btn-outline-secondary";
                }

                isiRekap();
                mvRekapUsulanBaru.SetActiveView(vDaftar);
            }
        }

        private void isiDdlThnPelaksanaan()
        {
            ddlThn.Items.Clear();
            ddlThn.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString()) + 1;
            for (int i = thnSKg; i >= 2021; i--)
            {
                ddlThn.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void isiRekap()
        {
            DataTable dt = new DataTable();
            if (objOperatorDRPM.ListRekapUsulanBaru(ref dt, objLogin.idInstitusi, ddlThn.SelectedValue.ToString(), ViewState["kdproghibah"].ToString()))
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
                    lblJmlUsulanDikirim.Text = "0";
                    lblJmlBlmDitinjau.Text = "0";
                }
            }
        }

        private void isiRekapInstitusi()
        {
            DataTable dt = new DataTable();
            //int? idKategoriPT = int.Parse(rblKategoriPT.SelectedValue);
            //if (idKategoriPT == -1) idKategoriPT = null;

            if (objOperatorDRPM.ListRekapUsulanBaruInstitusi(ref dt,
                    ddlThn.SelectedValue.ToString(),
                    int.Parse(ViewState["IdSkema"].ToString())))
            {
                gvDaftarInstitusi.DataSource = dt;
                gvDaftarInstitusi.DataBind();

                lblJmlInstitusi.Text = dt.Rows.Count.ToString();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi kesalahan", objOperatorDRPM.errorMessage);
            }
        }

        private void isiUsulanBaru(int idxPage)
        {
            var idInstitusi = Guid.Parse(ViewState["IdInstitusi"].ToString());

            if (!objOperatorDRPM.getJmlData(idInstitusi,
                    ddlThn.SelectedValue.ToString(),
                     int.Parse(ViewState["IdSkema"].ToString()), rblStatusApproval.SelectedValue))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objOperatorDRPM.errorMessage);

            //NEW PAGING CONTROL
            lbJmlUsulan.Text = objOperatorDRPM.numOfRecords.ToString();
            ktPagging.currentPage = idxPage;
            ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), objOperatorDRPM.numOfRecords);

            objOperatorDRPM.currentPage = idxPage;
            objOperatorDRPM.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!objOperatorDRPM.ListUsulanBaruPaging(idInstitusi,
                    ddlThn.SelectedValue.ToString(),
                    int.Parse(ViewState["IdSkema"].ToString()), rblStatusApproval.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objOperatorDRPM.errorMessage);
                return;
            }

            try
            {
                lvDaftarUsulanKonfirmasi.DataSource = objOperatorDRPM.currentRecords;
                lvDaftarUsulanKonfirmasi.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }

            if (objOperatorDRPM.numOfRecords < 1)
            {
                ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), 1);
            }
        }

        private void refreshPaging()
        {
            //string kode = ViewState["kode"].ToString();
            var idInstitusi = Guid.Parse(ViewState["IdInstitusi"].ToString());

            objOperatorDRPM.currentPage = ktPagging.currentPage;
            objOperatorDRPM.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!objOperatorDRPM.ListUsulanBaruPaging(idInstitusi,
                ddlThn.SelectedValue.ToString(),
                int.Parse(ViewState["IdSkema"].ToString()), rblStatusApproval.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    objOperatorDRPM.errorMessage);
                return;
            }

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiLisview.bindToListView(ref lvDaftarUsulanKonfirmasi, objOperatorDRPM.currentRecords);
        }

        protected void lbdesentralisasi_Click(object sender, EventArgs e)
        {
            lbdesentralisasi.CssClass = "btn btn-outline-success";
            lbPenugasan.CssClass = "btn btn-outline-secondary";            
            ViewState["kdproghibah"] = "1";
            isiRekap();
        }

        protected void lbPenugasan_Click(object sender, EventArgs e)
        {
            lbdesentralisasi.CssClass = "btn btn-outline-secondary";
            lbPenugasan.CssClass = "btn btn-outline-success";            
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

                pdfUsulanLengkap.UnduhProposalLengkap(id_usulan_kegiatan);

            }
        }

        protected void lbKembaliKeInstitusi_Click(object sender, EventArgs e)
        {
            mvRekapUsulanBaru.SetActiveView(vDaftarInstitusi);
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
            lblSkemaKegiatan.Text = gvDaftarSkema.DataKeys[e.RowIndex]["nama_skema"].ToString();
            lblTahunUsulan.Text = ddlThn.SelectedItem.Text;

            isiRekapInstitusi();
            mvRekapUsulanBaru.SetActiveView(vDaftarInstitusi);
        }

        protected void gvDaftarInstitusi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["IdInstitusi"] = gvDaftarInstitusi.DataKeys[e.RowIndex]["id_institusi"].ToString();

            lblNamaSkema.Text = lblSkemaKegiatan.Text;
            lblNamaInstitusi.Text = gvDaftarInstitusi.DataKeys[e.RowIndex]["nama_institusi"].ToString();
            lblTahunUsulan2.Text = lblTahunUsulan.Text;
            rblStatusApproval.SelectedValue = "0";
            isiUsulanBaru(0);
            mvRekapUsulanBaru.SetActiveView(vDaftarUsulan);
        }

        protected void rblStatusApproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiUsulanBaru(0);
        }

        protected void lbExcelRekapSkema_Click(object sender, EventArgs e)
        {
            int idSkema = int.Parse(ViewState["IdSkema"].ToString());
            var idInstitusi = Guid.Parse(ViewState["IdInstitusi"].ToString());

            var dt = new DataTable();
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = string.Format("Daftar Usulan" + " Skema {0}" + ".xlsx",
                    idSkema);
                if (objOperatorDRPM.excelListUsulanBaruPaging(ref dt, idInstitusi, ddlThn.SelectedValue.ToString(),
                    idSkema, rblStatusApproval.SelectedValue))
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
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", objOperatorDRPM.errorMessage);
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
    }
}