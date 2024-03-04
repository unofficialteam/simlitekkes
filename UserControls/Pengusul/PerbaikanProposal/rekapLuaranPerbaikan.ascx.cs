using System;
using System.Collections.Generic;
using simlitekkes.Models.Pengusul;
//using System;
//using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using System.IO;
using simlitekkes.Models.report;

namespace simlitekkes.UserControls.Pengusul.PerbaikanProposal
{
    public partial class rekapLuaranPerbaikan : System.Web.UI.UserControl
    {

        simlitekkes.Models.Pengusul.luaran objLuaran = new simlitekkes.Models.Pengusul.luaran();
        simlitekkes.Models.Pengusul.PerbaikanUsulan objPerbaikan = new simlitekkes.Models.Pengusul.PerbaikanUsulan();

        Models.login objLogin;
        Core.manipulasiData objManipData = new Core.manipulasiData();
        uiNotify noty = new uiNotify();

        pdfUsulanBaru objMdlPdfusulanBaru = new pdfUsulanBaru();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //mvMain.SetActiveView(vDaftarUsulanLuaran);
                //isiDdlThnPelaksanaan();
                //isiGvLuaranDicapai();
            }

            ktPublikasi.OnChildEventKembaliOccurs += new EventHandler(parent_OnChildEventKembaliOccurs);
            ktBuku.OnChildEventKembaliOccurs += new EventHandler(parent_OnChildEventKembaliOccurs);
            ktLainya.OnChildEventKembaliOccurs += new EventHandler(parent_OnChildEventKembaliOccurs);
            ktHki.OnChildEventKembaliOccurs += new EventHandler(parent_OnChildEventKembaliOccurs);
            ktPaten.OnChildEventKembaliOccurs += new EventHandler(parent_OnChildEventKembaliOccurs);
            ktProsiding.OnChildEventKembaliOccurs += new EventHandler(parent_OnChildEventKembaliOccurs);
            ktPVT.OnChildEventKembaliOccurs += new EventHandler(parent_OnChildEventKembaliOccurs);
        }

        void parent_OnChildEventKembaliOccurs(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vKelengkapanLuaran);
            isiGvLuaranDetail();
        }

        public void init(string idUsulanKegiatan)
        {
            ViewState["idUsulanKegiatan"] = idUsulanKegiatan;
            isiLuaran(idUsulanKegiatan);

            mvMain.SetActiveView(vDaftarUsulanLuaran);
            isiDdlThnPelaksanaan();
            isiGvLuaranDicapai(idUsulanKegiatan);
        }

        private void isiLuaran(string pidUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objPerbaikan.GetLuaranWajib(ref dt, Guid.Parse(pidUsulanKegiatan));
            rptLuaranWajib.DataSource = dt;
            rptLuaranWajib.DataBind();

            //int kdStatusMemenuhi = 1; // cekLuaranWajib(dt);// dt.Rows.Count;

            dt.Clear();
            dt = new DataTable();
            objPerbaikan.GetLuaranTambahan(ref dt, Guid.Parse(pidUsulanKegiatan));
            rptLuaranTambahan.DataSource = dt;
            rptLuaranTambahan.DataBind();
        }

        private void isiDdlThnPelaksanaan()
        {
            ddlThnPelaksanaan.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2022; i--)
            {
                ddlThnPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlThnPelaksanaan.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void isiGvLuaranDicapai(string pidUsulanKegiatan)
        {
            objLogin = (Models.login)Session["objLogin"];
            DataTable dt = new DataTable();
            Guid idPersonal = Guid.Parse(objLogin.idPersonal);
            objLuaran.rekapLuaranPerbaikan(ref dt, idPersonal, ddlThnPelaksanaan.SelectedValue);


            var dr1 = dt.Select("id_usulan_kegiatan='" + pidUsulanKegiatan + "'");
            DataTable dt3 = dr1.CopyToDataTable();
            gvLuaranDicapai.DataSource = dt3;
            gvLuaranDicapai.DataBind();
        }
        private void isiGvLuaranDetail()
        {
            objLogin = (Models.login)Session["objLogin"];
            DataTable dt = new DataTable();
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            int idKelompokLuaran = int.Parse(ViewState["id_kelompok"].ToString());
            objLuaran.detailLuaran(ref dt, idUsulanKegiatan, idKelompokLuaran);
            gvDetail.DataSource = dt;
            gvDetail.DataBind();
        }
        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvLuaranDicapai(ViewState["idUsulanKegiatan"].ToString());
        }

        protected void gvLuaranDicapai_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            string id_usulan_kegiatan = gvLuaranDicapai.DataKeys[row.RowIndex]["id_usulan_kegiatan"].ToString();
            switch (e.CommandName)
            {
                case "ubah_luaran_wajib":
                    ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
                    ViewState["id_kelompok"] = "1";
                    lblKelompokLuaran.Text = "Luaran Wajib";
                    mvMain.SetActiveView(vKelengkapanLuaran);
                    isiGvLuaranDetail();
                    break;
                case "ubah_luaran_tambahan":
                    ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
                    ViewState["id_kelompok"] = "2";
                    lblKelompokLuaran.Text = "Luaran Tambahan";
                    mvMain.SetActiveView(vKelengkapanLuaran);
                    isiGvLuaranDetail();
                    break;
            }
        }

        protected void lbKembali_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vDaftarUsulanLuaran);
        }

        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            string id_luaran_dijanjikan = gvDetail.DataKeys[row.RowIndex]["id_luaran_dijanjikan"].ToString();
            string id_jenis_luaran = gvDetail.DataKeys[row.RowIndex]["id_jenis_luaran"].ToString();
            switch (e.CommandName)
            {
                case "lengkapi":
                    switch (id_jenis_luaran)
                    {
                        case "1": // Publikasi Ilmiah Jurnal Internasional
                        case "2": // Publikasi Ilmiah Jurnal Nasional Terakreditasi
                        case "21":// Publikasi Ilmiah Jurnal Nasional Tidak Terakreditasi
                            mvMain.SetActiveView(vIsianLuaran);
                            mvIsianLuaran.SetActiveView(vPublikasi);
                            ktPublikasi.isiLuaranPublikasJurnal(Guid.Parse(id_luaran_dijanjikan), id_jenis_luaran, ddlThnPelaksanaan.SelectedValue);
                            break;
                        case "3": // Prosiding dalam pertemuan ilmiah Nasional
                        case "4": // Prosiding dalam pertemuan ilmiah Internasional
                        case "22":// Prosiding dalam pertemuan ilmiah Lokal
                            mvMain.SetActiveView(vIsianLuaran);
                            mvIsianLuaran.SetActiveView(vProsiding);
                            ktProsiding.isiLuaranPublikasProsiding(Guid.Parse(id_luaran_dijanjikan), id_jenis_luaran, ddlThnPelaksanaan.SelectedValue);
                            break;
                        case "19": // Buku Ajar (ISBN)
                        case "29": // Bahan Ajar
                        case "44": // Buku Hasil Penelitian
                            mvMain.SetActiveView(vIsianLuaran);
                            mvIsianLuaran.SetActiveView(vBuku);
                            ktBuku.isiLuaranBuku(Guid.Parse(id_luaran_dijanjikan), id_jenis_luaran, ddlThnPelaksanaan.SelectedValue);
                            break;
                        //case "5": // Pemakalah Keynote Speaker dalam pertemuan ilmiah Internasional
                        //case "6": // Pemakalah Keynote Speaker dalam pertemuan ilmiah Nasional
                        //case "23":// Pemakalah Keynote Speaker dalam pertemuan ilmiah Lokal 
                        //case "37":// Keikutsertaan dalam Seminar Internasional
                        //case "38":// Keikutsertaan dalam Seminar Nasional
                        //    mvMain.SetActiveView(vIsianLuaran);
                        //    mvIsianLuaran.SetActiveView(vPemakalah);
                        //    break;                        
                        case "10":// HKI Hak Cipta
                                  //case "11":// HKI Merk Dagang
                                  //case "12":// HKI Rahasia Dagang
                                  //case "13":// HKI Desain Produk Industri
                                  //case "14":// HKI Indikasi Geografis                        
                                  //case "16":// HKI Perlindungan Topografi Sirkuit Terpadu
                            mvMain.SetActiveView(vIsianLuaran);
                            mvIsianLuaran.SetActiveView(vHki);
                            ktHki.isiLuaranHKI(Guid.Parse(id_luaran_dijanjikan), id_jenis_luaran, ddlThnPelaksanaan.SelectedValue);
                            break;
                        case "8": // HKI Paten
                        case "9": // HKI Paten Sederhana
                            mvMain.SetActiveView(vIsianLuaran);
                            mvIsianLuaran.SetActiveView(vPaten);
                            ktPaten.isiLuaranHKI(Guid.Parse(id_luaran_dijanjikan), id_jenis_luaran, ddlThnPelaksanaan.SelectedValue);
                            break;
                        case "15":// HKI Perlindungan Varietas Tanaman
                            mvMain.SetActiveView(vIsianLuaran);
                            mvIsianLuaran.SetActiveView(vPVT);
                            ktPVT.isiLuaranPVT(Guid.Parse(id_luaran_dijanjikan), id_jenis_luaran, ddlThnPelaksanaan.SelectedValue);
                            break;
                        case "17": // Luaran Lain Teknologi Tepat Guna
                        case "24": // Luaran Lain Model
                        case "25": // Luaran Lain Purwarupa/Prototipe
                        case "26": // Luaran Lain Desain
                        case "27": // Luaran Lain Karya Seni
                        case "28": // Luaran Lain Rekayasa Sosial
                        case "32": // Luaran Lain Kebijakan
                        case "35": // Luaran Lain Produk
                        case "33": // Sistem
                        case "34": // Metode
                        case "36": // Strategi
                            mvMain.SetActiveView(vIsianLuaran);
                            mvIsianLuaran.SetActiveView(vLuaranLainya);
                            ktLainya.isiLuaranLainnya(Guid.Parse(id_luaran_dijanjikan), id_jenis_luaran, ddlThnPelaksanaan.SelectedValue);
                            break;
                    }

                    break;

                case "unduhBerkasSurat":
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string stsBerkas = gvDetail.DataKeys[rowIndex]["sts_berkas"].ToString();
                    string idJenisLuaran = gvDetail.DataKeys[rowIndex]["id_jenis_luaran"].ToString();
                    string idLuaranDijanjikan = gvDetail.DataKeys[rowIndex]["id_luaran_dijanjikan"].ToString();
                    string namaJenisLuaran = gvDetail.DataKeys[rowIndex]["nama_jenis_luaran"].ToString();
                    string thnKegiatan = ddlThnPelaksanaan.SelectedValue;
                    string id_surat = gvDetail.DataKeys[rowIndex]["id_surat"].ToString();

                    string namaFile = (namaJenisLuaran.Length > 50) ? namaJenisLuaran.Substring(0, 50) : namaJenisLuaran;
                    namaFile = namaFile.Replace(" ", "_");
                    namaFile = objManipData.removeUnicode(namaFile);

                    if (stsBerkas == "Sdh diunggah")
                    {
                        if (idJenisLuaran == "3" || idJenisLuaran == "4" || idJenisLuaran == "22" || idJenisLuaran == "1" || idJenisLuaran == "2" || idJenisLuaran == "21")
                        {
                            string namaBerkas = "surat_publikasi_" + namaFile + ".pdf";
                            string filePath1 = string.Format("~/fileUpload/Publikasi/" + thnKegiatan + "/" + id_surat + ".pdf");
                            if (File.Exists(Server.MapPath(filePath1)))
                            {
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                                Response.TransmitFile(filePath1);
                                Response.End();
                            }
                        }

                        if (idJenisLuaran == "19" || idJenisLuaran == "29" || idJenisLuaran == "44")
                        {
                            string namaBerkas = "surat_buku_" + namaFile + ".pdf";
                            string filePath1 = string.Format("~/fileUpload/Buku/" + thnKegiatan + "/" + id_surat + ".pdf");
                            if (File.Exists(Server.MapPath(filePath1)))
                            {
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                                Response.TransmitFile(filePath1);
                                Response.End();
                            }
                        }

                        if (idJenisLuaran == "8" || idJenisLuaran == "9" || idJenisLuaran == "15" || idJenisLuaran == "10")
                        {
                            string namaBerkas = "surat_hki_" + namaFile + ".pdf";
                            string filePath1 = string.Format("~/fileUpload/HKI/" + thnKegiatan + "/" + id_surat + ".pdf");
                            if (File.Exists(Server.MapPath(filePath1)))
                            {
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                                Response.TransmitFile(filePath1);
                                Response.End();
                            }
                        }
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "File tidak ditemukan.");
                    }
                    break;

                case "unduhBerkasDokumen":
                    rowIndex = int.Parse(e.CommandArgument.ToString());
                    stsBerkas = gvDetail.DataKeys[rowIndex]["sts_berkas"].ToString();
                    idJenisLuaran = gvDetail.DataKeys[rowIndex]["id_jenis_luaran"].ToString();
                    idLuaranDijanjikan = gvDetail.DataKeys[rowIndex]["id_luaran_dijanjikan"].ToString();
                    namaJenisLuaran = gvDetail.DataKeys[rowIndex]["nama_jenis_luaran"].ToString();
                    thnKegiatan = ddlThnPelaksanaan.SelectedValue;
                    string id_dok = gvDetail.DataKeys[rowIndex]["id_dok"].ToString();

                    namaFile = (namaJenisLuaran.Length > 50) ? namaJenisLuaran.Substring(0, 50) : namaJenisLuaran;
                    namaFile = namaFile.Replace(" ", "_");
                    namaFile = objManipData.removeUnicode(namaFile);

                    if (stsBerkas == "Sdh diunggah")
                    {
                        if (idJenisLuaran == "3" || idJenisLuaran == "4" || idJenisLuaran == "22" || idJenisLuaran == "1" || idJenisLuaran == "2" || idJenisLuaran == "21")
                        {
                            string namaBerkas = "dok_publikasi_" + namaFile + ".pdf";
                            string filePath2 = string.Format("~/fileUpload/Publikasi/" + thnKegiatan + "/" + id_dok + ".pdf");
                            if (File.Exists(Server.MapPath(filePath2)))
                            {
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                                Response.TransmitFile(filePath2);
                                Response.End();
                            }
                        }

                        if (idJenisLuaran == "19" || idJenisLuaran == "29" || idJenisLuaran == "44")
                        {
                            string namaBerkas = "dok_buku_" + namaFile + ".pdf";
                            string filePath1 = string.Format("~/fileUpload/Buku/" + thnKegiatan + "/" + id_dok + ".pdf");
                            if (File.Exists(Server.MapPath(filePath1)))
                            {
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                                Response.TransmitFile(filePath1);
                                Response.End();
                            }
                        }

                        if (idJenisLuaran == "8" || idJenisLuaran == "9" || idJenisLuaran == "15" || idJenisLuaran == "10")
                        {
                            string namaBerkas = "dok_hki_" + namaFile + ".pdf";
                            string filePath1 = string.Format("~/fileUpload/HKI/" + thnKegiatan + "/" + id_dok + ".pdf");
                            if (File.Exists(Server.MapPath(filePath1)))
                            {
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                                Response.TransmitFile(filePath1);
                                Response.End();
                            }
                        }

                        if (idJenisLuaran == "17" || idJenisLuaran == "24" || idJenisLuaran == "25" || idJenisLuaran == "26" || idJenisLuaran == "27" || idJenisLuaran == "28" || idJenisLuaran == "32" || idJenisLuaran == "35" || idJenisLuaran == "33" || idJenisLuaran == "34" || idJenisLuaran == "36")
                        {
                            string namaBerkas = "dok_lainnya_" + namaFile + ".pdf";
                            string filePath1 = string.Format("~/fileUpload/Lainnya/" + thnKegiatan + "/" + id_dok + ".pdf");
                            if (File.Exists(Server.MapPath(filePath1)))
                            {
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                                Response.TransmitFile(filePath1);
                                Response.End();
                            }
                        }
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "File tidak ditemukan.");
                    }
                    break;

                case "unduhBerkasManual":
                    rowIndex = int.Parse(e.CommandArgument.ToString());
                    stsBerkas = gvDetail.DataKeys[rowIndex]["sts_berkas"].ToString();
                    idJenisLuaran = gvDetail.DataKeys[rowIndex]["id_jenis_luaran"].ToString();
                    idLuaranDijanjikan = gvDetail.DataKeys[rowIndex]["id_luaran_dijanjikan"].ToString();
                    namaJenisLuaran = gvDetail.DataKeys[rowIndex]["nama_jenis_luaran"].ToString();
                    thnKegiatan = ddlThnPelaksanaan.SelectedValue;
                    string id_manual = gvDetail.DataKeys[rowIndex]["id_manual"].ToString();

                    namaFile = (namaJenisLuaran.Length > 50) ? namaJenisLuaran.Substring(0, 50) : namaJenisLuaran;
                    namaFile = namaFile.Replace(" ", "_");
                    namaFile = objManipData.removeUnicode(namaFile);

                    if (stsBerkas == "Sdh diunggah")
                    {
                        if (idJenisLuaran == "10")
                        {
                            string namaBerkas = "manual_hki_" + namaFile + ".pdf";
                            string filePath3 = string.Format("~/fileUpload/HKI/" + thnKegiatan + "/" + id_manual + ".pdf");
                            if (File.Exists(Server.MapPath(filePath3)))
                            {
                                Response.ContentType = "application/pdf";
                                Response.AppendHeader("Content-Disposition", "attachment; filename=" + namaBerkas);
                                Response.TransmitFile(filePath3);
                                Response.End();
                            }
                        }
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "File tidak ditemukan.");
                    }
                    break;
            }

        }

        protected void lbKembali2_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vKelengkapanLuaran);
        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sts_berkas = gvDetail.DataKeys[e.Row.RowIndex]["sts_berkas"].ToString();
                string sts_didanai = gvDetail.DataKeys[e.Row.RowIndex]["sts_didanai"].ToString();

                string surat = gvDetail.DataKeys[e.Row.RowIndex]["surat"].ToString();
                string dok = gvDetail.DataKeys[e.Row.RowIndex]["dok"].ToString();
                string manual = gvDetail.DataKeys[e.Row.RowIndex]["manual"].ToString();
                string id_jenis_luaran = gvDetail.DataKeys[e.Row.RowIndex]["id_jenis_luaran"].ToString();

                LinkButton lbStatusBerkas = new LinkButton();
                lbStatusBerkas = (LinkButton)e.Row.FindControl("lbStatusBerkas");

                LinkButton lbStatusBerkasSurat = new LinkButton();
                lbStatusBerkasSurat = (LinkButton)e.Row.FindControl("lbStatusBerkasSurat");
                LinkButton lbStatusBerkasDokumen = new LinkButton();
                lbStatusBerkasDokumen = (LinkButton)e.Row.FindControl("lbStatusBerkasDokumen");
                LinkButton lbStatusBerkasManual = new LinkButton();
                lbStatusBerkasManual = (LinkButton)e.Row.FindControl("lbStatusBerkasManual");

                if (sts_berkas != "Sdh diunggah")
                {
                    lbStatusBerkas.Visible = true;
                }
                else
                {
                    lbStatusBerkas.Visible = false;
                }

                if (surat == "Sdh diunggah")
                {
                    lbStatusBerkasSurat.Visible = true;
                    lbStatusBerkasSurat.CssClass = "fa fa-file-pdf-o btn btn-danger";
                }
                else
                {
                    lbStatusBerkasSurat.Visible = false;
                }

                if (dok == "Sdh diunggah")
                {
                    lbStatusBerkasDokumen.Visible = true;
                    lbStatusBerkasDokumen.CssClass = "fa fa-file-pdf-o btn btn-danger";
                }
                else
                {
                    lbStatusBerkasDokumen.Visible = false;
                }

                if (manual == "Sdh diunggah")
                {
                    lbStatusBerkasManual.Visible = true;
                    lbStatusBerkasManual.CssClass = "fa fa-file-pdf-o btn btn-danger";
                }
                else
                {
                    lbStatusBerkasManual.Visible = false;
                }

                LinkButton lbKeterangan = new LinkButton();
                lbKeterangan = (LinkButton)e.Row.FindControl("lbKeterangan");

                if (sts_didanai == "1")
                {
                    lbKeterangan.Enabled = false;
                    lbKeterangan.ToolTip = "Proses melengkapi sedang ditutup untuk keperluan validasi"; ;
                    lbKeterangan.Text = "ditutup";
                }
                else
                {
                    lbKeterangan.Enabled = true;
                    lbKeterangan.ToolTip = "Silahkan dilengkapi";
                    lbKeterangan.Text = "Lengkapi";
                }
            }
        }
    }
}