using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using System.Data;
using simlitekkes.UIControllers;
using simlitekkes.Models.Sistem;


namespace simlitekkes.UserControls.KepalaLembaga
{
    public partial class persetujuanPengabdian : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.PT.KepalaLembaga objKaLembaga = new Models.PT.KepalaLembaga();
        uiGridView obj_uiGridView = new uiGridView();
        uiListView obj_uiLisview = new uiListView();
        uiRadioButtonList obj_uiRadioButtonList = new uiRadioButtonList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        uiNotify noty = new uiNotify();
        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();

        uiModal objModal = new uiModal();
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();

        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();
        Models.Pengusul.mitraPenelitian objMitra = new Models.Pengusul.mitraPenelitian();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbnasional);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbpt);

            if (!IsPostBack)
            {
                if (ViewState["kdproghibah"] == null)
                {
                    ViewState["kdproghibah"] = "3";
                    lbnasional.CssClass = "btn btn-outline-success";
                    lbpt.CssClass = "btn btn-outline-secondary";
                }
                isiRekap();
                mvSinta.SetActiveView(vDaftar);
            }
        }

        private void isiRekap()
        {
            DataTable dt = new DataTable();
            if (objKaLembaga.LisResume(ref dt, objLogin.idPeran.ToString(), objLogin.idPersonal.ToString(),
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue, ViewState["kdproghibah"].ToString()))
            {
                ViewState["tahun_ke"] = "";
                gvDataResume.DataSource = dt;
                gvDataResume.DataBind();
                if (dt.Rows.Count > 0)
                {
                    lblJmlUsulan.Text = Convert.ToDecimal(dt.Compute("SUM(jml_identitas)", string.Empty)).ToString("N0");
                    lblJmlUsulanDikirim.Text = Convert.ToDecimal(dt.Compute("SUM(jml_usulan)", string.Empty)).ToString("N0");
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

        protected void lbjmlusulan_Click(object sender, EventArgs e)
        {
            mvSinta.SetActiveView(vDaftarUsulanKonfirmasi);
            string kode = "3";
            ViewState["kode"] = kode;
            isibelumditinjau(0);
            lbjudul.Text = "Daftar Usulan Baru Yang Belum Ditinjau";
        }

        protected void lbdesentralisasi_Click(object sender, EventArgs e)
        {
            lbnasional.CssClass = "btn btn-outline-success";
            lbpt.CssClass = "btn btn-outline-secondary";
            ViewState["kdproghibah"] = "3";
            isiRekap();
        }

        protected void lbPenugasan_Click(object sender, EventArgs e)
        {
            lbnasional.CssClass = "btn btn-outline-secondary";
            lbpt.CssClass = "btn btn-outline-success";
            ViewState["kdproghibah"] = "7";
            isiRekap();
        }

        protected void ddlThn_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiRekap();
        }

        protected void lvDaftarUsulanKonfirmasi_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int itemIndex = int.Parse(e.CommandArgument.ToString());

            //Untuk isian modal konfirmasi persetujuan
            string judul = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["judul"].ToString();
            ViewState["judul"] = judul;
            string nama_skema = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["nama_skema"].ToString();
            ViewState["nama_skema"] = nama_skema;
            string thn_usulan_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["thn_usulan_kegiatan"].ToString();
            ViewState["thn_usulan_kegiatan"] = thn_usulan_kegiatan;
            string thn_pelaksanaan_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["thn_pelaksanaan_kegiatan"].ToString();
            ViewState["thn_pelaksanaan_kegiatan"] = thn_pelaksanaan_kegiatan;
            string nama_ketua = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["nama_ketua"].ToString();
            ViewState["nama_ketua"] = nama_ketua;
            string id_transaksi_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["id_transaksi_kegiatan"].ToString();
            ViewState["id_transaksi_kegiatan"] = id_transaksi_kegiatan;

            if (e.CommandName == "UnduhPdf")
            {
                string id_usulan_kegiatan = "";
                objKaLembaga.getidusulankegiatan(id_transaksi_kegiatan, ref id_usulan_kegiatan);
                pdfUsulanLengkap.UnduhProposalLengkap(id_usulan_kegiatan);
            }
            else if (e.CommandName == "Setuju")
            {
                ViewState["disetujui"] = true;
                lblModalJudul.Text = judul;
                lblModalSkema.Text = nama_skema;
                lblModalThnUsulan.Text = thn_usulan_kegiatan;
                lblModalThnPelaksanaan.Text = thn_pelaksanaan_kegiatan;
                lblModalKetua.Text = nama_ketua;
                cblPeranPengguna.Visible = false;
                Label10.Visible = false;
                objModal.ShowModal(this.Page, "modalKonfirmasi");
                if (ViewState["kode"].ToString() == "1")
                {
                    lblModalInfoStsPersetujuan.Text = "membatalkan persetujuan";
                    lbModalStsKonfirmasi.Text = "Ya, batalkan";
                    ViewState["kd_sts_approvel"] = "8";
                }
                else if (ViewState["kode"].ToString() == "2")
                {
                    lblModalInfoStsPersetujuan.Text = "membatalkan penolakan";
                    lbModalStsKonfirmasi.Text = "Ya, batalkan";
                    ViewState["kd_sts_approvel"] = "8";
                }
                else
                {
                    ViewState["kd_sts_approvel"] = "1";
                    lblModalInfoStsPersetujuan.Text = "menyetujui";
                    lbModalStsKonfirmasi.Text = "Ya, setuju";

                };
            }
            else if (e.CommandName == "Tolak")
            {
                ViewState["disetujui"] = false;

                lblModalInfoStsPersetujuan.Text = "menolak";
                lbModalStsKonfirmasi.Text = "Ya, ditolak";

                lblModalJudul.Text = judul;
                lblModalSkema.Text = nama_skema;
                lblModalThnUsulan.Text = thn_usulan_kegiatan;
                lblModalThnPelaksanaan.Text = thn_pelaksanaan_kegiatan;
                lblModalKetua.Text = nama_ketua;

                DataTable dt = new DataTable();
                if (objKaLembaga.alasanpenolakan(ref dt))
                {
                    cblPeranPengguna.Visible = true;
                    cblPeranPengguna.Items.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cblPeranPengguna.Items.Add(new System.Web.UI.WebControls.ListItem(dr["alasan_penolakan"].ToString(), dr["id_alasan_penolakan_approval"].ToString()));
                    }
                    Label10.Visible = true;
                }

                ViewState["kd_sts_approvel"] = "0";
                objModal.ShowModal(this.Page, "modalKonfirmasi");
            }
        }

        private void isibelumditinjau(int idxPage)
        {
            string kode = ViewState["kode"].ToString();

            if (!objKaLembaga.getJmlData(objLogin.idPeran.ToString(), objLogin.idPersonal.ToString(),
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ViewState["kdproghibah"].ToString(), kode))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objKaLembaga.errorMessage);

            //NEW PAGING CONTROL
            lbjml.Text = objKaLembaga.numOfRecords.ToString();
            ktPagging.currentPage = idxPage;
            ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), objKaLembaga.numOfRecords);

            objKaLembaga.currentPage = idxPage;
            objKaLembaga.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!objKaLembaga.ListBelumdiTinjaurbPaging(objLogin.idPeran.ToString(), objLogin.idPersonal.ToString(),
                ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ViewState["kdproghibah"].ToString(), kode))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objKaLembaga.errorMessage);
                return;
            }

            if (!obj_uiLisview.bindToListView(ref lvDaftarUsulanKonfirmasi, objKaLembaga.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objKaLembaga.errorMessage);

            if (objKaLembaga.numOfRecords < 1)
            {
                ktPagging.setPaging(int.Parse(ddlJmlBaris.SelectedValue), 1);
            }
        }

        protected void lbKembali_Click(object sender, EventArgs e)
        {
            isiRekap();
            mvSinta.SetActiveView(vDaftar);
        }

        protected void lbjmltdksetuju_Click(object sender, EventArgs e)
        {
            mvSinta.SetActiveView(vDaftarUsulanKonfirmasi);
            string kode = "2";
            ViewState["kode"] = kode;
            isibelumditinjau(0);
            lbjudul.Text = "Daftar Usulan Baru Yang Ditolak";

        }

        protected void lbjmldisetujui_Click(object sender, EventArgs e)
        {
            mvSinta.SetActiveView(vDaftarUsulanKonfirmasi);
            string kode = "1";
            ViewState["kode"] = kode;
            isibelumditinjau(0);
            lbjudul.Text = "Daftar Usulan Baru Yang Disetujui";
        }

        protected void lbjmlusulan_Click1(object sender, EventArgs e)
        {
            isiRekap();
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            isibelumditinjau(0);
        }

        protected void Paging_PageChanging(object sender, EventArgs e)
        {
            string kode = ViewState["kode"].ToString();

            objKaLembaga.currentPage = ktPagging.currentPage;
            objKaLembaga.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!objKaLembaga.ListBelumdiTinjaurbPaging(objLogin.idPeran.ToString(),
                objLogin.idPersonal.ToString(), ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ViewState["kdproghibah"].ToString(), kode))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiLisview = new UIControllers.uiListView();
            obj_uiLisview.bindToListView(ref lvDaftarUsulanKonfirmasi, objKaLembaga.currentRecords);
        }

        private void refreshPaging() //buat refresh setelah event approval
        {
            string kode = ViewState["kode"].ToString();

            objKaLembaga.currentPage = ktPagging.currentPage;
            objKaLembaga.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!objKaLembaga.ListBelumdiTinjaurbPaging(objLogin.idPeran.ToString(),
                objLogin.idPersonal.ToString(), ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue,
                ViewState["kdproghibah"].ToString(), kode))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiLisview.bindToListView(ref lvDaftarUsulanKonfirmasi, objKaLembaga.currentRecords);
        }

        protected void lbModalStsKonfirmasi_Click(object sender, EventArgs e)
        {
            string id_alasan;
            id_alasan = "";
            //Boolean cek = false;

            if (cblPeranPengguna.Visible == true)
            {
                for (int i = 0; i < cblPeranPengguna.Items.Count; i++)
                {
                    if (cblPeranPengguna.Items[i].Selected == true)
                    {
                        id_alasan = id_alasan + "," + cblPeranPengguna.Items[i].Value.ToString();
                    }
                }

                if (id_alasan != "")
                {
                    id_alasan = "{" + id_alasan.Substring(1, id_alasan.Length - 1) + "}";
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Alasan belum di pilih");
                    //popup_konfirmasi.Show();
                    objModal.ShowModal(this.Page, "modalKonfirmasi");

                    return;

                    //objModal.ShowModal(this.Page, "modalKonfirmasi");
                }
            }
            else
            {
                id_alasan = "";
            }
            if (objKaLembaga.InsupApproval(ViewState["id_transaksi_kegiatan"].ToString(), ViewState["kd_sts_approvel"].ToString(), id_alasan, "",
                objLogin.idPersonal.ToString(), ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue))
            {
                isibelumditinjau(0);
                if (ViewState["kd_sts_approvel"].ToString() == "1")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Update persetujuan berhasil");
                }
                else if (ViewState["kd_sts_approvel"].ToString() == "0")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Update Penolakan berhasil");

                }

            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Persetujuan gagal. error = " + objKaLembaga.errorMessage);
            }

            //popup_konfirmasi.Hide();
            objModal.HideModal(this.Page, "modalKonfirmasi");

        }

        protected void lvDaftarUsulanKonfirmasi_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;

            LinkButton lbDisetujui = new LinkButton();
            LinkButton lbDitolak = new LinkButton();

            lbDisetujui = (LinkButton)e.Item.FindControl("lbDisetujui");
            lbDitolak = (LinkButton)e.Item.FindControl("lbDitolak");

            if (ViewState["kode"].ToString() == "1") //&& is_dibuka == "1") //semengtara ditutup utk checking
            {
                lbDisetujui.Text = "&nbsp;&nbsp;Batalkan persetujuan";
                lbDisetujui.CssClass = "btn btn-danger fa fa-times waves - effect waves - light";
                lbDitolak.Visible = false;
            }
            else if (ViewState["kode"].ToString() == "2")
            {
                lbDisetujui.Text = "&nbsp;&nbsp;Batalkan penolakan";
                lbDisetujui.CssClass = "btn btn-danger fa fa-times waves - effect waves - light";
                lbDitolak.Visible = false;
            }
            else
            {
                lbDisetujui.Text = "&nbsp;&nbsp;Disetujui";
                lbDisetujui.CssClass = "btn btn-success fa fa-check waves-effect waves-light";
                lbDitolak.Visible = true;
            }

            //Label lblStsApproval = new Label();
            //lblStsApproval = (Label)e.Item.FindControl("lblStsApproval");
            //string kd_sts_approval = drv["kd_sts_approval"].ToString();

            //if (kd_sts_approval == "1")
            //{
            //    lblStsApproval.CssClass = "label bg-success";
            //}
            //else
            //{
            //    lblStsApproval.CssClass = "label bg-warning";
            //}

        }

        protected void cblPeranPengguna_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lbModalStsKonfirmasi.CssClass = "btn btn-success";

        }

        protected void btnTutup_Click(object sender, EventArgs e)
        {
            //popup_konfirmasi.Hide();
        }

        protected byte[] GetBytePdfDepan()
        {
            //get html
            var outHTML = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute("~/UserControls/Pengusul/report/pdfFullProposal.aspx", stringWriter);
                outHTML = stringWriter.ToString();
                stringWriter.Close();
            }
            var converterProperties = new ConverterProperties();
            converterProperties.SetBaseUri(Server.MapPath("~/"));
            var memoryStream = new MemoryStream();
            HtmlConverter.ConvertToPdf(outHTML, memoryStream, converterProperties);
            return memoryStream.ToArray();
        }

        protected PdfDocument getPdfDokumenUsulan()
        {
            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            string filePath = "~/fileUpload/Dokumenusulan/" +
                    objUsulanKegiatan.thnUsulan + "/" +
                    objUsulanKegiatan.idUsulanKegiatan + ".pdf";
            if (!File.Exists(Server.MapPath(filePath)))
            {
                return null;
            }
            PdfDocument doc = new PdfDocument(new PdfReader(Server.MapPath(filePath)));
            return doc;
        }

        protected byte[] GetBytePdfBiodataKetua(string pidPersonal)
        {
            //get html
            var outHTML = string.Empty;
            Session["idPersonalKetua"] = pidPersonal;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute("~/UserControls/Pengusul/report/biodataKetua.aspx", stringWriter);
                outHTML = stringWriter.ToString();
                stringWriter.Close();
            }
            var converterProperties = new ConverterProperties();
            converterProperties.SetBaseUri(Server.MapPath("~/"));
            var memoryStream = new MemoryStream();
            HtmlConverter.ConvertToPdf(outHTML, memoryStream, converterProperties);
            return memoryStream.ToArray();
        }

        protected byte[] GetBytePdfBiodataAnggota()
        {
            //get html
            var outHTML = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                Server.Execute("~/UserControls/Pengusul/report/biodataAnggota.aspx", stringWriter);
                outHTML = stringWriter.ToString();
                stringWriter.Close();
            }
            var converterProperties = new ConverterProperties();
            converterProperties.SetBaseUri(Server.MapPath("~/"));
            var memoryStream = new MemoryStream();
            HtmlConverter.ConvertToPdf(outHTML, memoryStream, converterProperties);
            return memoryStream.ToArray();
        }

        protected PdfDocument getMitraDokumen(string idMitra)
        {
            //objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            //string filePath = "~/fileUpload/Dokumenusulan/" +
            //        objUsulanKegiatan.thnUsulan + "/" +
            //        objUsulanKegiatan.idUsulanKegiatan + ".pdf";
            string filePath = "~/fileUpload/mitra/" + idMitra + ".pdf";
            if (!File.Exists(Server.MapPath(filePath)))
            {
                return null;
            }
            PdfDocument doc = new PdfDocument(new PdfReader(Server.MapPath(filePath)));
            return doc;
        }

        protected DataTable GetDataListIdMitra(string idUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objMitra.listMitraPelaksanaPenelitian(ref dt, Guid.Parse(idUsulanKegiatan), 0, 0);
            return dt;
        }

        void downloadPdf(byte[] bytes)
        {
            objLogin = (Models.login)Session["objLogin"];
            string namaFile = "UsulanLengkap " + objLogin.namaLengkap + ".pdf";
            //byte[] docData = mem.GetBuffer();
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.End();
        }

        private void initUsulan(string idUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objModelIdentitasUsulan.getDataDataUsulanKegiatan(ref dt, idUsulanKegiatan);
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            var objUsulanKegiatan = new usulanKegiatan()
            {
                idUsulanKegiatan = idUsulanKegiatan,
                idUsulan = dt.Rows[0]["id_usulan"].ToString(),
                judul = dt.Rows[0]["judul"].ToString(),
                idSkema = int.Parse(dt.Rows[0]["id_skema"].ToString()),
                namaSkema = dt.Rows[0]["nama_skema"].ToString(),
                thnUsulan = dt.Rows[0]["thn_usulan_kegiatan"].ToString(),
                thnPelaksanaan = dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString(),
                lamaKegiatan = int.Parse(dt.Rows[0]["lama_kegiatan"].ToString()),
                urutanTahunUsulanKegiatan = int.Parse(dt.Rows[0]["urutan_thn_usulan_kegiatan"].ToString()),
                tktTarget = int.Parse(dt.Rows[0]["level_tkt_target"].ToString()), // test
                idKategoriSBK = int.Parse(dt.Rows[0]["id_kategori_sbk"].ToString()),
                idBidangFokus = int.Parse(dt.Rows[0]["id_bidang_fokus"].ToString()),
                bidangFokus = dt.Rows[0]["bidang_fokus"].ToString()
            };
            Session["usulan_kegiatan"] = objUsulanKegiatan;
            ViewState["usulan_kegiatan"] = objUsulanKegiatan;
            ViewState["id_skema"] = objUsulanKegiatan.idSkema;
        }

        public void UnduhProposalLengkap()
        {
            Session["usulan_kegiatan"] = ViewState["usulan_kegiatan"];
            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];

            byte[] pdfDepan = GetBytePdfDepan();
            ByteArrayOutputStream ms = new ByteArrayOutputStream();
            PdfDocument pdocDepan = new PdfDocument(new PdfReader(new MemoryStream(pdfDepan)), new PdfWriter(ms));
            pdocDepan.SetDefaultPageSize(new PageSize(PageSize.A4));

            PdfDocument pdfDokumenUsulan = getPdfDokumenUsulan();
            if (pdfDokumenUsulan != null)
                pdfDokumenUsulan.CopyPagesTo(1, pdfDokumenUsulan.GetNumberOfPages(), pdocDepan);

            //objLogin = (Models.login)Session["objLogin"];
            byte[] biodataKetua = GetBytePdfBiodataKetua(ViewState["id_personal_ketua"].ToString());
            PdfDocument pdocKetua = new PdfDocument(new PdfReader(new MemoryStream(biodataKetua)));
            pdocKetua.CopyPagesTo(1, pdocKetua.GetNumberOfPages(), pdocDepan);

            DataTable dta = new DataTable();
            objAnggota.listAnggotaDikti(ref dta, Guid.Parse(objUsulanKegiatan.idUsulanKegiatan));

            if (dta.Rows.Count > 0)
            {
                for (int a = 0; a < dta.Rows.Count; a++)
                {
                    Session["idPersonalAnggota"] = dta.Rows[a]["id_personal"].ToString();
                    Session["urutanAnggota"] = a + 1;
                    byte[] biodataAnggota = GetBytePdfBiodataAnggota();
                    PdfDocument pdocAnggota = new PdfDocument(new PdfReader(new MemoryStream(biodataAnggota)));
                    pdocAnggota.CopyPagesTo(1, pdocAnggota.GetNumberOfPages(), pdocDepan);
                }
            }

            DataTable dtm = new DataTable();
            dtm = GetDataListIdMitra(objUsulanKegiatan.idUsulanKegiatan);
            if (dtm.Rows.Count > 0)
            {
                for (int a = 0; a < dtm.Rows.Count; a++)
                {
                    PdfDocument pdfMitra = getMitraDokumen(dtm.Rows[a]["id_mitra"].ToString());
                    if (pdfMitra != null)
                        pdfMitra.CopyPagesTo(1, pdfMitra.GetNumberOfPages(), pdocDepan);
                }
            }

            Document document = new Document(pdocDepan);
            document.Close();
            downloadPdf(ms.ToArray());
        }

        protected void lbnasional_Click(object sender, EventArgs e)
        {

            lbnasional.CssClass = "btn btn-outline-success";
            lbpt.CssClass = "btn btn-outline-secondary";
            ViewState["kdproghibah"] = "3";
            isiRekap();
        }


        protected void lbpt_Click(object sender, EventArgs e)
        {
            lbnasional.CssClass = "btn btn-outline-secondary";
            lbpt.CssClass = "btn btn-outline-success";
            ViewState["kdproghibah"] = "7";
            isiRekap();
        }
    }
}

