using simlitekkes.Core;
using simlitekkes.Models.Admin;
using simlitekkes.Models.Sistem;
using simlitekkes.UIControllers;
using simlitekkes.UserControls.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes
{
    public partial class Default : System.Web.UI.Page
    {
        daftarPengumuman modelPengumuman = new daftarPengumuman();

        manipulasiData objManipData = new manipulasiData();
        uiGridView obj_uiGVPengumuman = new UIControllers.uiGridView();
        uiPaging obj_uiPagingPengumuman = new UIControllers.uiPaging();
        referensiData objRefData = new referensiData();
        daftarRunningText objRunText = new daftarRunningText();
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        List<DataTable> lstDt = null;
        //List<DataTable> lstDtBulan = null;

        string[] namaKolomsPengumuman = { "no_baris", "id_pengumuman", "tgl_pemberitaan", "judul", "no_surat", "tgl_surat", "isi_pengumuman", "kd_status_publikasi", "kd_status_frontpages", "file_pengumuman", "file_pengumuman_judul" };
        string[] namaKolomsMenuPengumuman = { "tahun", "bulan", "txtbulan" };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //updateIsiPengumumanBuffer("", 0);
                ViewState["use_buffer"] = true;
                string strJmlData = "0";
                if (Application["JmlDataPengumuman"] != null)
                {
                    strJmlData = Application["JmlDataPengumuman"].ToString();
                }
                int jmlData = int.Parse(strJmlData);
                modelPengumuman.numOfRecords = jmlData;
                obj_uiPagingPengumuman.setPaging(ref MenuPage, 10, jmlData);
                txtCari.Text = "";
                updateTampilanPengumumanDgBuffer();
                lblError.Text = "";
            }
            string perubahanPengumuman = "false";
            if (Application["ada_perubahan_pengumuman"] != null)
            {
                perubahanPengumuman = Application["ada_perubahan_pengumuman"].ToString();
            }
            if (bool.Parse(perubahanPengumuman))
            {
                ViewState["use_buffer"] = true;
                updateBufferIsiPengumuman();
                updateTampilanPengumumanDgBuffer();
                Application["ada_perubahan_pengumuman"] = false;
            }

            if (Application["OnlineUsers"] != null)
            {
                lblUserCount.Text = Application["OnlineUsers"].ToString();
            }
        }

        private void updateBufferIsiPengumuman()
        {
            modelPengumuman.getJmlRecordsFrontpages();
            obj_uiPagingPengumuman.setPaging(ref MenuPage, 10, modelPengumuman.numOfRecords);
            ViewState["uiPagingPengumuman"] = obj_uiPagingPengumuman;

            modelPengumuman.currentPage = 0;
            modelPengumuman.rowsPerPage = 10;

            //==================================================
            DataTable dtPengumuman = new DataTable();
            modelPengumuman.getCurrRecordsFrontpages();
            if (modelPengumuman.currentRecords.Rows.Count > 0)
            {
                dtPengumuman = modelPengumuman.currentRecords;
                Application["Pengumuman"] = dtPengumuman;

                List<DataTable> lstDt = new List<DataTable>();
                for (int a = 0; a < dtPengumuman.Rows.Count; a++)
                {
                    DataTable dtLampiranPengumuman = new DataTable();
                    string id_pengumuman = dtPengumuman.Rows[a]["id_pengumuman"].ToString();
                    if (modelPengumuman.getListPengumumanFile(ref dtLampiranPengumuman, Guid.Parse(id_pengumuman)))
                    {
                        lstDt.Add(dtLampiranPengumuman);
                    }
                }
                Application["LampiranPengumuman"] = lstDt;

            }

            DataTable dtMenuTahun = new DataTable();
            if (modelPengumuman.getMenuPengumumanTahun(ref dtMenuTahun))
            {
                Application["PengumumanTahun"] = dtMenuTahun;

                List<DataTable> lstDtBulan = new List<DataTable>();
                for (int a = 0; a < dtMenuTahun.Rows.Count; a++)
                {
                    string tahun = dtMenuTahun.Rows[a]["tahun"].ToString();
                    DataTable dtBulan = new DataTable();
                    if (modelPengumuman.getMenuPengumumanBulan(ref dtBulan, tahun))
                    {
                        lstDtBulan.Add(dtBulan);
                    }

                }
                Application["lstDtBulan"] = lstDtBulan;
            }

            DataTable dtrt = new DataTable();
            int id_peran = 0;
            if (!objRunText.getRunningText(ref dtrt, id_peran))
            {
                dtrt = objRunText.currentRecords;
                {
                    if (dtrt.Rows.Count > 0)
                    {
                        Application["running_text"] = dtrt.Rows[0]["get_running_text_by_peran"].ToString();
                    }
                }
            }

            string tgl = DateTime.Now.Day.ToString();
            string jam = DateTime.Now.Hour.ToString();
            Application["tgl_jam"] = tgl + jam;
        }

        protected void gvPengumuman_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            //BulletedList BulletedList1 = (BulletedList)row.FindControl("BulletedList1");            
        }

        protected void gvMenuPengumumanTahun_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton Tahun = new LinkButton();
            GridView gvMenuPengumumanBulan = new GridView();

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:

                    gvMenuPengumumanBulan = (GridView)e.Row.FindControl("gvMenuPengumumanBulan");
                    string tahun = gvMenuPengumumanTahun.DataKeys[e.Row.RowIndex]["tahun"].ToString();

                    DataTable dt = new DataTable();
                    //objPengumuman.getMenuPengumumanBulan(ref dt, tahun);
                    List<DataTable> lstDtBulan = (List<DataTable>)Application["lstDtBulan"];
                    dt = lstDtBulan.ElementAt(e.Row.RowIndex);
                    if (dt.Rows.Count > 0)
                    {
                        gvMenuPengumumanBulan.DataSource = dt;
                        gvMenuPengumumanBulan.DataBind();
                    }
                    break;
            }
        }

        protected void gvMenuPengumumanTahun_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void menu_event(object sender, MenuEventArgs e)
        {
            ViewState["use_buffer"] = false;
            if (txtCari.Text != "")
            {

                string itemsText = e.Item.Text;
                obj_uiPagingPengumuman.changePage(ref MenuPage, itemsText);
                switch (itemsText)
                {
                    case "Prev":
                        modelPengumuman.currentPage = int.Parse(MenuPage.Items[obj_uiPagingPengumuman.jmlKolomPaging].Value);
                        break;
                    case "Next":
                        modelPengumuman.currentPage = int.Parse(MenuPage.Items[1].Value);
                        break;
                    default:
                        modelPengumuman.currentPage = int.Parse(itemsText) - 1;
                        break;
                }

                modelPengumuman.rowsPerPage = 10;
                if (!modelPengumuman.getCurrRecordsResultSearch(ViewState["keyword"].ToString()))
                    lblError.Text = modelPengumuman.errorMessage;

            }
            else
            {
                string itemsText = e.Item.Text;
                obj_uiPagingPengumuman.changePage(ref MenuPage, itemsText);
                switch (itemsText)
                {
                    case "Prev":
                        modelPengumuman.currentPage = int.Parse(MenuPage.Items[obj_uiPagingPengumuman.jmlKolomPaging].Value);
                        break;
                    case "Next":
                        modelPengumuman.currentPage = int.Parse(MenuPage.Items[1].Value);
                        break;
                    default:
                        modelPengumuman.currentPage = int.Parse(itemsText) - 1;
                        break;
                }

                modelPengumuman.rowsPerPage = 10;
                if (!modelPengumuman.getCurrRecordsFrontpages())
                    lblError.Text = modelPengumuman.errorMessage;
            }
            obj_uiGVPengumuman = new UIControllers.uiGridView();
            obj_uiGVPengumuman.bindToGridView(ref gvPengumuman, modelPengumuman.currentRecords, namaKolomsPengumuman);
        }

        protected void gvPengumuman_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton LinkButtonJudul = new LinkButton();
            GridView gvInternal = new GridView();

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:

                    gvInternal = (GridView)e.Row.FindControl("gvInternal");
                    string idPengumuman = gvPengumuman.DataKeys[e.Row.RowIndex]["id_pengumuman"].ToString();
                    DataTable dt = new DataTable();
                    getDtLampiranFile(ref dt, e.Row.RowIndex, idPengumuman);
                    //objPengumuman.getListPengumumanFile(ref dt, Guid.Parse(idPengumuman), e.Row.RowIndex);
                    if (dt != null)
                    {
                        gvInternal.DataSource = dt;
                        gvInternal.DataBind();
                    }
                    break;
            }
        }

        private void getDtLampiranFile(ref DataTable dt, int indeks, string idPengumuman)
        {
            if (bool.Parse(ViewState["use_buffer"].ToString()))
            {
                if (Application["LampiranPengumuman"] == null)
                {
                    dt = null;
                    return;
                }

                lstDt = (List<DataTable>)Application["LampiranPengumuman"];
                dt = lstDt.ElementAt(indeks);//[indeks];
            }
            else
            {
                modelPengumuman.getListPengumumanFile(ref dt, Guid.Parse(idPengumuman));
            }
        }

        private void updateTampilanPengumumanDgBuffer()
        {
            isiPengumumanDgBuffer();
            isiMenuPengumuman();

            DateTime dt = DateTime.Now;
            Judul.Text = "Pengumuman : " + ConvertDateTimeToDate(dt.ToString(), "MMMM yyyy", "id-ID");

            isiRunningText();
            //lblJmlPengunjung.Text = Application["OnlineUsers"].ToString();

        }

        private void isiPengumumanDgBuffer()
        {
            DataTable dtPengumuman = new DataTable();
            dtPengumuman = (DataTable)Application["Pengumuman"];
            if (dtPengumuman != null)
            {
                if (!obj_uiGVPengumuman.bindToGridView(ref gvPengumuman, dtPengumuman, namaKolomsPengumuman))
                    lblError.Text = obj_uiGVPengumuman.errorMessage;
            }
        }

        public void isiRunningText()
        {
            if (Application["running_text"] != null)
                lblRunningText.Text = Application["running_text"].ToString();
        }

        private void isiMenuPengumuman()
        {
            if (Application["PengumumanTahun"] == null)
                return;
            DataTable dt = (DataTable)Application["PengumumanTahun"];
            if (!obj_uiGVPengumuman.bindToGridView(ref gvMenuPengumumanTahun, dt, namaKolomsMenuPengumuman))
                lblError.Text = modelPengumuman.errorMessage;
        }

        protected void gvMenuPengumumanBulan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string arg = e.CommandArgument.ToString();
            string rcc = e.CommandName;

            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            if (e.CommandName == "Bulan")
            {
                LinkButton lblBulan = (LinkButton)row.FindControl("lbBulan");

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string tahun = commandArgs[0];
                string bulan = commandArgs[1];

                isiPengumumanArsip(tahun, bulan);

                Judul.Text = "Pengumuman : " + ConvertDateTimeToDate(tahun + "-" + bulan, "MMMM yyyy", "id-ID");

                return;
            }
        }

        protected void gvMenuPengumumanBulan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton Bulan = new LinkButton();
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:

                    Bulan = (LinkButton)e.Row.FindControl("lbBulan");
                    break;
            }
        }

        public static string ConvertDateTimeToDate(string dateTimeString, string dateTimeFormat, String langCulture)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(langCulture);
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(dateTimeString, out dt))
            {
                return dt.ToString(dateTimeFormat, culture);
            }
            return dateTimeString;
        }

        private void isiPengumumanArsip(string tahun, string bulan)
        {
            DataTable dt = new DataTable();

            if (!modelPengumuman.getCurrRecordsArsip(ref dt, tahun, bulan))
                lblError.Text = modelPengumuman.errorMessage;

            if (!obj_uiGVPengumuman.bindToGridView(ref gvPengumuman, dt, namaKolomsPengumuman))
                lblError.Text = modelPengumuman.errorMessage;
        }

        protected void gvInternal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string arg = e.CommandArgument.ToString();
            string rcc = e.CommandName;
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            if (e.CommandName == "Unduh")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string namafile = commandArgs[0];
                string tanggal_surat = commandArgs[1];

                DateTime tanggal = Convert.ToDateTime(tanggal_surat);
                string tahun = tanggal.Year.ToString();
                string bulan = tanggal.Month.ToString();

                LinkButton lblJudul = (LinkButton)row.FindControl("lbJudulFile");
                string path = "~/fileUpload/pengumuman/" + tahun + "/" + bulan + "/" + namafile;
                if (File.Exists(Server.MapPath(path)))
                {
                    UnduhFile(path, lblJudul.Text);
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Kesalahan", "Berkas tidak ditemukan.");
                }
                return;
            }
        }

        protected void lbCari_Click(object sender, EventArgs e)
        {
            ViewState["use_buffer"] = false;
            isiPengumumanHasilPencarian(txtCari.Text, 0);
        }

        private void isiPengumumanHasilPencarian(string keyword, int page)
        {
            Judul.Text = "Hasil Pencarian Pengumuman :";
            if (keyword == "")
            {
                ViewState["use_buffer"] = true;
                string strJmlData = Application["JmlDataPengumuman"].ToString();
                int jmlData = int.Parse(strJmlData);
                modelPengumuman.numOfRecords = jmlData;
                obj_uiPagingPengumuman.setPaging(ref MenuPage, 10, jmlData);
                updateTampilanPengumumanDgBuffer();
                lblError.Text = "";
                return;
            }

            ViewState["keyword"] = keyword;
            if (!modelPengumuman.getJmlRecordsResultSearch(keyword))
                lblError.Text = modelPengumuman.errorMessage;

            if (page == 0)
                obj_uiPagingPengumuman.setPaging(ref MenuPage, 10, modelPengumuman.numOfRecords);
            ViewState["uiPagingPengumuman"] = obj_uiPagingPengumuman;

            modelPengumuman.currentPage = page;
            modelPengumuman.rowsPerPage = 10;
            modelPengumuman.currentRecords.Clear();

            int brs = modelPengumuman.currentRecords.Rows.Count;

            if (!modelPengumuman.getCurrRecordsResultSearch(keyword))
                lblError.Text = modelPengumuman.errorMessage;

            DataTable dt = modelPengumuman.currentRecords;

            if (!obj_uiGVPengumuman.bindToGridView(ref gvPengumuman, modelPengumuman.currentRecords, namaKolomsPengumuman))
                lblError.Text = obj_uiGVPengumuman.errorMessage;

        }

        protected void UnduhFile(string path, string nama_berkas)
        {
            string fileExtension = System.IO.Path.GetExtension(path);
            string filePath = Server.MapPath(path);
            if (File.Exists(filePath))
            {
                Response.ContentType = getContentType(fileExtension);
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + nama_berkas + fileExtension + "\"");
                Response.TransmitFile(filePath);
                Response.End();
            }
        }

        private string getContentType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                case ".docx":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".xlsx":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                case ".pptx":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }

        protected void gvInternal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton LinkButtonJudul = new LinkButton();
            LinkButton lbIkon = new LinkButton();
            LinkButton lbFileHid = new LinkButton();

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    LinkButtonJudul = (LinkButton)e.Row.FindControl("lbJudulFile");
                    lbIkon = (LinkButton)e.Row.FindControl("lbIkon");
                    lbFileHid = (LinkButton)e.Row.FindControl("lbFileHid");

                    string ext = lbFileHid.Text; //fa - file - pdf - o
                    if (ext.ToLower().Contains("pdf"))
                    {
                        lbIkon.Text = "<i class=\"fa fa-file-pdf-o\"></i>";
                    }
                    else if (ext.ToLower().Contains("png") || ext.ToLower().Contains("jpg"))
                    {
                        lbIkon.Text = "<i class=\"fa fa-file-image-o\"></i>";
                    }
                    else if (ext.ToLower().Contains("xls") || ext.ToLower().Contains("xlsx"))
                    {
                        lbIkon.Text = "<i class=\"fa fa-file-excel-o\"></i>";
                    }
                    else if (ext.ToLower().Contains("doc") || ext.ToLower().Contains("docx"))
                    {
                        lbIkon.Text = "<i class=\"fa fa-file-word-o\"></i>";
                    }

                    break;
            }
        }
    }
}