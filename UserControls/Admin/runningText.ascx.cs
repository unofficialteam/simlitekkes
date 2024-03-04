using simlitekkes.Core;
using simlitekkes.Models.Admin;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Admin
{
    public partial class runningText : System.Web.UI.UserControl
    {
        daftarRunningText objDaftarText = new daftarRunningText();
        manipulasiData objManipData = new manipulasiData();

        uiGridView obj_uiGVText = new uiGridView();
        uiPaging obj_uiPagingText = new uiPaging();
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();

        string[] namaKolomsText = { "no_baris", "id_running_text", "tgl_mulai_aktif", "tgl_berakhir_aktif", "item_running_text", "sts_aktif", "jml_peran" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DataTable dt = new DataTable();
                DataTable dt = objDaftarText.getPeran("01");

                if (dt.Rows.Count > 0)
                {
                    cblPeranPengguna.Items.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cblPeranPengguna.Items.Add(new ListItem(dr["nama_peran"].ToString(), dr["id_peran"].ToString()));
                    }
                    cblPeranPengguna.Items[0].Selected = true;
                }

                isiGridview();
                MultiViewText.SetActiveView(ViewDaftar);
            }
            else
            {
                obj_uiPagingText = (UIControllers.uiPaging)ViewState["uiPagingText"];
            }
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            obj_uiPagingText.setPaging(ref MenuPage, int.Parse(ddlJmlBaris.SelectedValue), obj_uiPagingText.jmlTotalBaris);
            objDaftarText.currentPage = 0;
            objDaftarText.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            if (!objDaftarText.getCurrRecords())
                lblError.Text = objDaftarText.errorMessage;
            obj_uiGVText = new UIControllers.uiGridView();
            obj_uiGVText.bindToGridView(ref gvDaftarText, objDaftarText.currentRecords, namaKolomsText);
        }

        protected void menu_event(object sender, MenuEventArgs e)
        {
            string itemsText = e.Item.Text;
            obj_uiPagingText.changePage(ref MenuPage, itemsText);
            switch (itemsText)
            {
                case "Prev":
                    objDaftarText.currentPage = int.Parse(MenuPage.Items[obj_uiPagingText.jmlKolomPaging].Value);
                    break;
                case "Next":
                    objDaftarText.currentPage = int.Parse(MenuPage.Items[1].Value);
                    break;
                default:
                    if (ddlJmlBaris.SelectedValue != "Semua")
                    {
                        objDaftarText.currentPage = int.Parse(itemsText) - 1;
                    }
                    break;
            }
            objDaftarText.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            if (!objDaftarText.getCurrRecords())
                lblError.Text = objDaftarText.errorMessage;
            obj_uiGVText = new UIControllers.uiGridView();
            obj_uiGVText.bindToGridView(ref gvDaftarText, objDaftarText.currentRecords, namaKolomsText);
        }

        private void clearViewData()
        {
            tbHTMLRunningText.Text = "";
            tbTglMulaiTayang.Text = "";
            tbTglAkhirTayang.Text = "";
            cblPeranPengguna.ClearSelection();
        }

        private void isiGridview()
        {
            clearViewData();

            if (!objDaftarText.getJmlRecords())
                lblError.Text = objDaftarText.errorMessage;

            obj_uiPagingText.setPaging(ref MenuPage, int.Parse(ddlJmlBaris.SelectedValue), objDaftarText.numOfRecords);
            ViewState["uiPagingText"] = obj_uiPagingText;

            objDaftarText.currentPage = 0;
            objDaftarText.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            if (!objDaftarText.getCurrRecords())
                lblError.Text = objDaftarText.errorMessage;

            if (!obj_uiGVText.bindToGridView(ref gvDaftarText, objDaftarText.currentRecords, namaKolomsText))
                lblError.Text = obj_uiGVText.errorMessage;
            lblJmlRecords.Text = objDaftarText.numOfRecords.ToString();
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            ViewState.Remove("id_running_text");
            isiGridview();
            MultiViewText.SetActiveView(ViewDaftar);
            lbDataBaru.Visible = true;
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            string strIDRunningText = null;
            if (lbSimpan.Text == "Simpan")
            {
                strIDRunningText = Guid.NewGuid().ToString();
            }
            else
            {
                strIDRunningText = ViewState["id_running_text"].ToString();
            }

            if (tbTglMulaiTayang.Text.Trim() == "" || tbTglAkhirTayang.Text.Trim() == "" || tbHTMLRunningText.Text.Trim() == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pesan text, tgl mulai, dan berakhir belum diisi.");
            }
            else
            {
                DateTime tglMulaiAktif = DateTime.Parse(tbTglMulaiTayang.Text);
                DateTime tglAkhirAktif = DateTime.Parse(tbTglAkhirTayang.Text);
                String tbHTMLText = HttpUtility.HtmlDecode(tbHTMLRunningText.Text.Trim());

                if (objDaftarText.insertDataBaru(strIDRunningText, tglMulaiAktif, tglAkhirAktif, tbHTMLText))
                {

                    for (int i = 0; i < cblPeranPengguna.Items.Count; i++)
                    {
                        if (cblPeranPengguna.Items[i].Selected == true)
                        {
                            if (!objDaftarText.insertDataPeran(cblPeranPengguna.Items[i].Value, strIDRunningText))
                                lblError.Text = objDaftarText.errorMessage;
                        }
                        else
                            if (!objDaftarText.deleteDataPeran(cblPeranPengguna.Items[i].Value, strIDRunningText))
                            lblError.Text = objDaftarText.errorMessage;
                    }

                    DataTable dtrt = new DataTable();
                    int id_peran = 0;
                    if (objDaftarText.getRunningText(ref dtrt, id_peran))
                    {
                        dtrt = objDaftarText.currentRecords;
                        {
                            if (dtrt.Rows.Count > 0)
                            {
                                string strRt = dtrt.Rows[0]["get_running_text_by_peran"].ToString();
                                Application["running_text"] = strRt;
                            }
                        }
                    }

                    //Application["ada_perubahan_pengumuman"] = true;
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                    lbSimpan.Text = "Update";
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
                }

                MultiViewText.SetActiveView(ViewData);
                lbDataBaru.Visible = false;
            }
        }

        protected void lbDataBaru_Click(object sender, EventArgs e)
        {
            MultiViewText.SetActiveView(ViewData);
            lbDataBaru.Visible = false;
        }

        protected void gvDaftarText_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            Label aLbl = new Label();

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    // Tampilan warna label tgl mulai dan berakhir sesuai dengan status aktif
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    if (drv["sts_aktif"].ToString() != "Aktif")
                    {
                        aLbl = (Label)theRow.FindControl("lblStsAktif");
                        if (aLbl != null)
                            aLbl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#aa2222");
                        aLbl = (Label)theRow.FindControl("lblTglMulaiAktif");
                        if (aLbl != null)
                            aLbl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#aa2222");
                        aLbl = (Label)theRow.FindControl("lblTglBerakhirAktif");
                        if (aLbl != null)
                            aLbl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#aa2222");
                    }
                    break;
            }
        }

        protected void gvDaftarText_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lbSimpan.Text = "Update";

            string strIDRunningText = gvDaftarText.DataKeys[e.RowIndex]["id_running_text"].ToString();
            ViewState["id_running_text"] = strIDRunningText;

            DataTable dt = objDaftarText.getRow(strIDRunningText);
            if (dt.Rows.Count > 0)
            {
                tbTglMulaiTayang.Text = Convert.ToDateTime(dt.Rows[0]["tgl_mulai_aktif"]).ToString("yyyy-MM-dd");
                tbTglAkhirTayang.Text = Convert.ToDateTime(dt.Rows[0]["tgl_berakhir_aktif"]).ToString("yyyy-MM-dd");
                tbHTMLRunningText.Text = dt.Rows[0]["item_running_text"].ToString();

                int[] ArrayIdPeran = (int[])dt.Rows[0]["id_peran"];
                cblPeranPengguna.ClearSelection();
                foreach (int idPeran in ArrayIdPeran)
                {
                    if (cblPeranPengguna.Items.FindByValue(idPeran.ToString()) != null)
                        cblPeranPengguna.Items.FindByValue(idPeran.ToString()).Selected = true;
                }

                dt.Dispose();
            }
            MultiViewText.SetActiveView(ViewData);
            lbDataBaru.Visible = false;
        }

        // DELETE
        protected void gvDaftarText_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string strIDRunningText = gvDaftarText.DataKeys[e.RowIndex]["id_running_text"].ToString();
            ViewState["id_running_text"] = strIDRunningText;

            lblHapus.Text = gvDaftarText.DataKeys[e.RowIndex]["item_running_text"].ToString();
            uiMdl.ShowModal(this.Page, "myModal");
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            string strIDRunningText = ViewState["id_running_text"].ToString();

            if (objDaftarText.deleteData(strIDRunningText))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
                ViewState.Remove("id_running_text");
                isiGridview();
                MultiViewText.SetActiveView(ViewDaftar);
                lbDataBaru.Visible = true;
                Application["ada_perubahan_pengumuman"] = true;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objDaftarText.errorMessage);
            }
        }
    }
}