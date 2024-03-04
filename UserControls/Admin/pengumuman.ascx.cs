using simlitekkes.Core;
using simlitekkes.Models.Admin;
using simlitekkes.Models.Sistem;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Admin
{
    public partial class pengumuman : System.Web.UI.UserControl
    {
        daftarPengumuman objDaftarPengumuman = new daftarPengumuman();
        daftarPengumumanFile objDaftarPengumumanFile = new daftarPengumumanFile();
        uiGridView obj_uiGVPengumuman = new uiGridView();
        uiGridView obj_uiGVPengumumanFile = new uiGridView();
        uiPaging obj_uiPagingPengumuman = new uiPaging();

        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();

        string[] namaKolomsPengumuman = { "no_baris", "id_pengumuman", "tgl_pemberitaan", "judul", "no_surat", "tgl_surat", "isi_pengumuman", "kd_status_publikasi", "kd_status_frontpages", "file_pengumuman" };
        string[] namaKolomsPengumumanFile = { "no_baris", "id_pengumuman_file", "id_pengumuman", "judul_file", "file_pengumuman", "urutan" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isiGridview();
                MultiViewPengumuman.SetActiveView(ViewDaftar);
            }
            else
            {
                obj_uiPagingPengumuman = (uiPaging)ViewState["uiPagingPengumuman"];
            }
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            obj_uiPagingPengumuman.setPaging(ref MenuPage, int.Parse(ddlJmlBaris.SelectedValue), obj_uiPagingPengumuman.jmlTotalBaris);
            objDaftarPengumuman.currentPage = 0;
            objDaftarPengumuman.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            if (!objDaftarPengumuman.getCurrRecords())
                lblError.Text = objDaftarPengumuman.errorMessage;
            obj_uiGVPengumuman = new uiGridView();
            obj_uiGVPengumuman.bindToGridView(ref gvDaftarPengumuman, objDaftarPengumuman.currentRecords, namaKolomsPengumuman);
        }

        protected void menu_event(object sender, MenuEventArgs e)
        {
            string itemsText = e.Item.Text;
            obj_uiPagingPengumuman.changePage(ref MenuPage, itemsText);
            switch (itemsText)
            {
                case "Prev":
                    objDaftarPengumuman.currentPage = int.Parse(MenuPage.Items[obj_uiPagingPengumuman.jmlKolomPaging].Value);
                    break;
                case "Next":
                    objDaftarPengumuman.currentPage = int.Parse(MenuPage.Items[1].Value);
                    break;
                default:
                    if (ddlJmlBaris.SelectedValue != "Semua")
                    {
                        objDaftarPengumuman.currentPage = int.Parse(itemsText) - 1;
                    }
                    break;
            }

            objDaftarPengumuman.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            if (!objDaftarPengumuman.getCurrRecords())
                lblError.Text = objDaftarPengumuman.errorMessage;
            obj_uiGVPengumuman = new uiGridView();
            obj_uiGVPengumuman.bindToGridView(ref gvDaftarPengumuman, objDaftarPengumuman.currentRecords, namaKolomsPengumuman);
        }

        protected void gvDaftarPengumuman_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            string idPengumuman = gvDaftarPengumuman.DataKeys[gvr.RowIndex]["id_pengumuman"].ToString();
            ViewState["id_pengumuman"] = idPengumuman;

            string kdStatusPublikasi = gvDaftarPengumuman.DataKeys[gvr.RowIndex]["kd_status_publikasi"].ToString();
            string kdStatusFrontpages = gvDaftarPengumuman.DataKeys[gvr.RowIndex]["kd_status_frontpages"].ToString();

            switch (e.CommandName)
            {
                case "StatusPublikasi":
                    string kdPublikasi = "1";
                    if (kdStatusPublikasi != "Aktif")
                    {
                        kdPublikasi = "1";
                    }
                    else
                    {
                        kdPublikasi = "0";
                    }
                    if (!objDaftarPengumuman.updateStatusPublikasi(idPengumuman, kdPublikasi))
                        lblError.Text = objDaftarPengumuman.errorMessage;

                    isiGridview();
                    MultiViewPengumuman.SetActiveView(ViewDaftar);
                    Application["ada_perubahan_pengumuman"] = true;
                    break;

                case "StatusFrontpages":
                    string kdFrontpages = "1";
                    if (kdStatusFrontpages != "Aktif")
                    {
                        kdFrontpages = "1";
                    }
                    else
                    {
                        kdFrontpages = "0";
                    }
                    if (!objDaftarPengumuman.updateStatusFrontpages(idPengumuman, kdFrontpages))
                        lblError.Text = objDaftarPengumuman.errorMessage;

                    isiGridview();
                    MultiViewPengumuman.SetActiveView(ViewDaftar);
                    Application["ada_perubahan_pengumuman"] = true;
                    break;
            }
        }

        protected void gvDaftarPengumuman_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["id_pengumuman_hapus"] = gvDaftarPengumuman.DataKeys[e.RowIndex]["id_pengumuman"].ToString();
            lblNoSurat.Text = gvDaftarPengumuman.DataKeys[e.RowIndex]["no_surat"].ToString();
            lblTglSurat.Text = gvDaftarPengumuman.DataKeys[e.RowIndex]["tgl_surat"].ToString();
            lblJudul.Text = gvDaftarPengumuman.DataKeys[e.RowIndex]["judul"].ToString();
            uiMdl.ShowModal(this.Page, "myModal");
        }

        protected void gvDaftarPengumuman_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lbSimpan.Text = "Update";
            gvDaftarPengumumanFile.Visible = true;

            string idPengumuman = gvDaftarPengumuman.DataKeys[e.RowIndex]["id_pengumuman"].ToString();
            ViewState["id_pengumuman"] = idPengumuman;
            DataTable dt = objDaftarPengumuman.getRow(idPengumuman);

            DateTime tanggal = Convert.ToDateTime(dt.Rows[0]["tgl_surat"].ToString());
            DateTime tanggalBerita = Convert.ToDateTime(dt.Rows[0]["tgl_pemberitaan"].ToString());
            string tahun = tanggal.Year.ToString();
            string bulan = tanggal.Month.ToString();
            ViewState["tahun"] = tahun;
            ViewState["bulan"] = bulan;

            tbNoSurat.Text = dt.Rows[0]["no_surat"].ToString();
            tbTglSurat.Text = tanggal.ToString("yyyy-MM-dd");
            tbJudul.Text = dt.Rows[0]["judul"].ToString();
            tbIsiPengumuman.Text = dt.Rows[0]["isi_pengumuman"].ToString();
            tbTglPemberitaan.Text = tanggalBerita.ToString("yyyy-MM-dd");

            if (dt.Rows[0]["kd_status_publikasi"].ToString().Trim() == "1")
            {
                cbStatusPublikasi.Checked = true;
            }
            else
                cbStatusPublikasi.Checked = false;

            if (dt.Rows[0]["kd_status_frontpages"].ToString().Trim() == "1")
            {
                cbStatusFrontpages.Checked = true;
            }
            else
                cbStatusFrontpages.Checked = false;

            MultiViewPengumuman.SetActiveView(ViewData);
            isiGridviewPengumumanFile();

            lbDataBaru.Visible = false;
            lbLampiran.Visible = true;
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            isiGridview();
            MultiViewPengumuman.SetActiveView(ViewDaftar);
            lbDataBaru.Visible = true;
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            string strIDPengumuman = null;
            if (lbSimpan.Text == "Simpan")
            {
                strIDPengumuman = Guid.NewGuid().ToString();
            }
            else
            {
                strIDPengumuman = ViewState["id_pengumuman"].ToString();
            }

            string kdStatusPublikasi = "1";
            if (!cbStatusPublikasi.Checked)
                kdStatusPublikasi = "0";

            string kdStatusFrontpages = "1";
            if (!cbStatusFrontpages.Checked)
                kdStatusFrontpages = "0";

            if (objDaftarPengumuman.insertDataBaru(
                strIDPengumuman, tbTglPemberitaan.Text, tbJudul.Text, tbNoSurat.Text,
                tbTglSurat.Text, tbIsiPengumuman.Text, kdStatusPublikasi, kdStatusFrontpages)
                )
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                lbLampiran.Visible = true;
                gvDaftarPengumumanFile.Visible = true;
                ViewState["id_pengumuman"] = strIDPengumuman;

                DateTime tanggal = Convert.ToDateTime(tbTglSurat.Text);
                string tahun = tanggal.Year.ToString();
                string bulan = tanggal.Month.ToString();
                ViewState["tahun"] = tahun;
                ViewState["bulan"] = bulan;

                lbSimpan.Text = "Update";

                isiGridviewPengumumanFile();
                Application["ada_perubahan_pengumuman"] = true;

                // Refresh data pengumuman
                updatePengumuman();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
            }
        }

        protected void lbDataBaru_Click(object sender, EventArgs e)
        {
            lbSimpan.Text = "Simpan";
            clearViewData();
            MultiViewPengumuman.SetActiveView(ViewData);
            gvDaftarPengumumanFile.Visible = false;
            lbDataBaru.Visible = false;
            lbLampiran.Visible = false;
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (objDaftarPengumuman.deleteData(ViewState["id_pengumuman_hapus"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
                isiGridview();
                Application["ada_perubahan_pengumuman"] = true;
                updatePengumuman();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objDaftarPengumuman.errorMessage);
            }
        }

        protected void lbLampiran_Click(object sender, EventArgs e)
        {
            string idPengumuman = ViewState["id_pengumuman"].ToString();
            ViewState["id_pengumuman"] = idPengumuman;

            ViewState["id_pengumuman_file"] = null;

            tbJudulFile.Text = "";
            ddlUrutan.SelectedValue = null;

            lbUpload.Text = "Simpan";

            uiMdl.ShowModal(this.Page, "lampiranPengumuman");
        }

        protected void lbUpload_Click(object sender, EventArgs e)
        {
            string strIDPengumumanFile = null;
            string filePengumuman = null;
            string strIDPengumuman = ViewState["id_pengumuman"].ToString();
            string tahun = ViewState["tahun"].ToString();
            string bulan = ViewState["bulan"].ToString();

            if (tbJudulFile.Text.Trim() == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan: Judul file tidak boleh kosong !!!");
                return;
            }

            if (lbUpload.Text == "Simpan")
            {
                strIDPengumumanFile = Guid.NewGuid().ToString();
                if (uploadFilePengumuman.HasFile)
                {
                    string fileExtension = System.IO.Path.GetExtension(uploadFilePengumuman.FileName);

                    if (fileExtension.ToLower() == ".pdf" || fileExtension.ToLower() == ".doc" || fileExtension.ToLower() == ".docx"
                        || fileExtension.ToLower() == ".xls" || fileExtension.ToLower() == ".xlsx"
                        || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".jpg"
                        || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".zip")
                    {
                        if (uploadFilePengumuman.PostedFile.ContentLength < (22 * 1024 * 1024))
                        {
                            try
                            {
                                string path = string.Format("~/fileUpload/pengumuman/{0}/{1}/", tahun, bulan);
                                if (!Directory.Exists(Server.MapPath(path)))
                                {
                                    Directory.CreateDirectory(Server.MapPath(path));
                                }

                                string namaFile = string.Format("~/fileUpload/pengumuman/{0}/{1}/{2}{3}", tahun, bulan, strIDPengumumanFile, fileExtension.ToLower());
                                filePengumuman = string.Format("{0}{1}", strIDPengumumanFile, fileExtension.ToLower());
                                uploadFilePengumuman.PostedFile.SaveAs(HttpContext.Current.Request.MapPath(namaFile));

                                if (objDaftarPengumumanFile.insertDataBaru(strIDPengumumanFile, strIDPengumuman, tbJudulFile.Text, filePengumuman, ddlUrutan.SelectedValue))
                                {
                                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah file surat berhasil");
                                    tbJudulFile.Text = "";
                                    ddlUrutan.SelectedValue = null;
                                    isiGridviewPengumumanFile();
                                }
                                else
                                {
                                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Unggah file surat gagal");
                                }
                                updatePengumuman();
                            }
                            catch (Exception)
                            {
                                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Terjadi Kesalahan");
                            }
                        }
                        else
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan: File yang akan diunggah ukurannya tidak boleh melebihi 10 MByte!!!");
                        }
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan: Silahkan upload File bertipe PDF / DOC / XLS / PNG / JPEG / ZIP !!!");
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan: File belum dipilih...");
                }
            }
            else
            {
                strIDPengumumanFile = ViewState["id_pengumuman_file"].ToString();
                if (uploadFilePengumuman.HasFile)
                {
                    string fileExtension = System.IO.Path.GetExtension(uploadFilePengumuman.FileName);

                    if (fileExtension.ToLower() == ".pdf" || fileExtension.ToLower() == ".doc" || fileExtension.ToLower() == ".docx"
                        || fileExtension.ToLower() == ".xls" || fileExtension.ToLower() == ".xlsx"
                        || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".jpg"
                        || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".zip")
                    {
                        if (uploadFilePengumuman.PostedFile.ContentLength < (22 * 1024 * 1024))
                        {
                            try
                            {
                                string path = string.Format("~/fileUpload/pengumuman/{0}/{1}/", tahun, bulan);
                                if (!Directory.Exists(Server.MapPath(path)))
                                {
                                    Directory.CreateDirectory(Server.MapPath(path));
                                }

                                string namaFile = string.Format("~/fileUpload/pengumuman/{0}/{1}/{2}{3}", tahun, bulan, strIDPengumumanFile, fileExtension.ToLower());
                                filePengumuman = string.Format("{0}{1}", strIDPengumumanFile, fileExtension.ToLower());
                                uploadFilePengumuman.PostedFile.SaveAs(HttpContext.Current.Request.MapPath(namaFile));

                                if (objDaftarPengumumanFile.insertDataBaru(strIDPengumumanFile, strIDPengumuman, tbJudulFile.Text, filePengumuman, ddlUrutan.SelectedValue))
                                {
                                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah file surat berhasil");
                                    tbJudulFile.Text = "";
                                    ddlUrutan.SelectedValue = null;
                                    isiGridviewPengumumanFile();
                                }
                                else
                                {
                                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Unggah file surat gagal");
                                }
                                updatePengumuman();
                            }
                            catch (Exception)
                            {
                                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Terjadi Kesalahan");
                            }
                        }
                        else
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan: File yang akan diunggah ukurannya tidak boleh melebihi 10 MByte!!!");
                        }
                    }
                }
                else
                {
                    if (objDaftarPengumumanFile.insertDataBaru(strIDPengumumanFile, strIDPengumuman, tbJudulFile.Text, "", ddlUrutan.SelectedValue))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah file surat berhasil");
                        tbJudulFile.Text = "";
                        ddlUrutan.SelectedValue = null;
                        isiGridviewPengumumanFile();
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Unggah file surat gagal");
                    }
                    updatePengumuman();
                }
            }
        }

        private void clearViewData()
        {
            tbNoSurat.Text = "";
            tbTglSurat.Text = "";
            tbJudul.Text = "";
            tbTglPemberitaan.Text = "";
            cbStatusPublikasi.Checked = true;
            cbStatusFrontpages.Checked = true;
        }

        private void isiGridview()
        {
            // data pt di gridview
            if (!objDaftarPengumuman.getJmlRecords())
                lblError.Text = objDaftarPengumuman.errorMessage;

            obj_uiPagingPengumuman.setPaging(ref MenuPage, int.Parse(ddlJmlBaris.SelectedValue), objDaftarPengumuman.numOfRecords);
            ViewState["uiPagingPengumuman"] = obj_uiPagingPengumuman;

            objDaftarPengumuman.currentPage = 0;
            objDaftarPengumuman.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            if (!objDaftarPengumuman.getCurrRecords())
                lblError.Text = objDaftarPengumuman.errorMessage;

            if (!obj_uiGVPengumuman.bindToGridView(ref gvDaftarPengumuman, objDaftarPengumuman.currentRecords, namaKolomsPengumuman))
                lblError.Text = obj_uiGVPengumuman.errorMessage;
            lblJmlRecords.Text = objDaftarPengumuman.numOfRecords.ToString();
        }

        private void isiGridviewPengumumanFile()
        {
            // VIEW Daftar Lampiran File
            DataTable dt = new DataTable();
            string idPengumuman = ViewState["id_pengumuman"].ToString();

            if (!objDaftarPengumumanFile.getCurrRecordsAll(ref dt, idPengumuman))
                lblErrorFile.Text = objDaftarPengumumanFile.errorMessage;

            if (!obj_uiGVPengumumanFile.bindToGridView(ref gvDaftarPengumumanFile, dt, namaKolomsPengumumanFile))
                lblErrorFile.Text = obj_uiGVPengumumanFile.errorMessage;
        }

        protected void gvDaftarPengumumanFile_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lbUpload.Text = "Update";

            string idPengumuman = ViewState["id_pengumuman"].ToString();
            string idPengumumanFile = gvDaftarPengumumanFile.DataKeys[e.RowIndex]["id_pengumuman_file"].ToString();
            ViewState["id_pengumuman"] = idPengumuman;
            ViewState["id_pengumuman_file"] = idPengumumanFile;

            DataTable dt = objDaftarPengumumanFile.getRow(idPengumumanFile);

            tbJudulFile.Text = dt.Rows[0]["judul_file"].ToString();

            if (dt.Rows[0]["urutan"].ToString().Trim() != "")
            {
                ddlUrutan.SelectedValue = dt.Rows[0]["urutan"].ToString();
            }
            uiMdl.ShowModal(this.Page, "lampiranPengumuman");
            lbDataBaru.Visible = false;
            lbLampiran.Visible = true;
        }

        protected void gvDaftarPengumumanFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["id_pengumuman_file_hapus"] = gvDaftarPengumumanFile.DataKeys[e.RowIndex]["id_pengumuman_file"].ToString();
            //string idPengumumanFile = ViewState["id_pengumuman_file_hapus"].ToString();
            uiMdl.ShowModal(this.Page, "hapusLampiranPengumuman");
        }

        protected void lbHapusFile_Click(object sender, EventArgs e)
        {
            if (objDaftarPengumumanFile.deleteData(ViewState["id_pengumuman_file_hapus"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
                isiGridviewPengumumanFile();
                Application["ada_perubahan_pengumuman"] = true;
                updatePengumuman();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error");
            }
        }

        protected void gvDaftarPengumuman_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                LinkButton lbStatusPublikasi = (LinkButton)e.Row.FindControl("lbStatusPublikasi");
                LinkButton lbStatusFrontpages = (LinkButton)e.Row.FindControl("lbStatusFrontpages");

                LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");

                if (lbStatusPublikasi != null)
                {
                    if (drv["kd_status_publikasi"].ToString() == "Aktif")
                    {
                        lbStatusPublikasi.CssClass = "btn btn-sm btn-success";
                        lbStatusPublikasi.Text = "<i class=\"fas fa-check-square\"></i>&nbsp;On";
                    }
                    else
                    {
                        lbStatusPublikasi.CssClass = "btn btn-sm btn-danger";
                        lbStatusPublikasi.Text = "<i class=\"fas fa-square\"></i>&nbsp;Off";
                    }
                }

                if (lbStatusFrontpages != null)
                {
                    if (drv["kd_status_frontpages"].ToString() == "Aktif")
                    {
                        lbStatusFrontpages.CssClass = "btn btn-sm btn-success";
                        lbStatusFrontpages.Text = "<i class=\"fas fa-check-square\"></i>&nbsp;On";
                    }
                    else
                    {
                        lbStatusFrontpages.CssClass = "btn btn-sm btn-danger";
                        lbStatusFrontpages.Text = "<i class=\"fas fa-square\"></i>&nbsp;Off";
                    }
                }

                if (lbEdit != null)
                {
                    lbEdit.CssClass = "btn btn-primary btn-sm";
                    lbEdit.Text = "<i class=\"fas fa-edit\"></i>";
                }

                if (lbDelete != null)
                {
                    lbDelete.CssClass = "btn btn-danger btn-sm";
                    lbDelete.Text = "<i class=\"fas fa-trash\"></i>";
                }

            }
        }

        private void updatePengumuman()
        {
            // Refresh data pengumuman
            DataTable dtPengumuman = new DataTable();
            Models.Pengusul.berandaPengusul objPengumuman = new Models.Pengusul.berandaPengusul();
            if (objPengumuman.getPengumuman(ref dtPengumuman, 10, 0))
            {
                Application["JmlDataPengumuman"] = objPengumuman.currentRecords.Rows.Count;
                objPengumuman.currentRecords.Clear();

                objPengumuman.rowsPerPage = 10;
                objPengumuman.getCurrRecordsFrontpages();
                dtPengumuman = objPengumuman.currentRecords;
                Application["Pengumuman"] = dtPengumuman;

                List<DataTable> lstDt = new List<DataTable>();
                for (int a = 0; a < dtPengumuman.Rows.Count; a++)
                {
                    DataTable dtLampiranPengumuman = new DataTable();
                    string id_pengumuman = dtPengumuman.Rows[a]["id_pengumuman"].ToString();
                    if (objPengumuman.getLampiranPengumuman(ref dtLampiranPengumuman, id_pengumuman))
                    {
                        lstDt.Add(dtLampiranPengumuman);
                    }
                }
                Application["LampiranPengumuman"] = lstDt;
            }
        }
    }
}