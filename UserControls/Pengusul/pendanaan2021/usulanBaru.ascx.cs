using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using simlitekkes.Core;
using simlitekkes.Models.Sistem;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace simlitekkes.UserControls.Pengusul.pendanaan2021
{
    public partial class usulanBaru : System.Web.UI.UserControl
    {
        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        UIControllers.uiDropdownList obj_uiDDL = new UIControllers.uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        Models.Sistem.referensiData objRefData = new Models.Sistem.referensiData();
        Models.login objLogin;
        kontrolUnggah ktUnggah = new kontrolUnggah();
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        uiNotify noty = new uiNotify();

        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();
        Models.Pengusul.mitraPenelitian objMitra = new Models.Pengusul.mitraPenelitian();
        //string idUsulanKegiatan = "7414f38d-bc90-4117-b996-4ea131c299c0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                init();
            }

            lstUsulanBaru.OnChildEventOccurs += new EventHandler(Child1_OnChildEventOccurs);
            lstUsulanBaru.OnChildEventUnduhProposalLengkap += new EventHandler(Child1_OnChildEventUnduhProposalLengkap);
            ktAnggota.OnChildEventOccurs += new EventHandler(Anggota_OnChildEventOccurs);
            ktAnggota.OnChildBatalEventOccurs += new EventHandler(AnggotaBatal_OnChildEventOccurs);

        }

        private void init()
        {
            isiDdlThnUsulan();
            isiDdlThnPelaksanaan();
            mvMain.SetActiveView(vPersyaratan);
            int totalSyarat = persyaratan.isiDataPersyaratanUmum(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            lstUsulanBaru.isiListUsulanBaru(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            lbPengajuanBaru.Visible = false;
            lblPengajuanBaru.Visible = false;
            //if (totalSyarat < 2)
            //    lblPengajuanBaru.Visible = true;
            //else
            //    lbPengajuanBaru.Visible = true;
        }

        void Child1_OnChildEventUnduhProposalLengkap(object sender, EventArgs e)
        {
            pdfUsulanLengkap.UnduhProposalLengkap(Session["id_usulan_kegiatan_unduh"].ToString());
            Session.Remove("id_usulan_kegiatan_unduh");
        }

        void Anggota_OnChildEventOccurs(object sender, EventArgs e)
        {
            bool show = false;
            if (ViewState["show_lanjutkan_at_anggota"] != null)
                show = (bool)ViewState["show_lanjutkan_at_anggota"];
            lbLanjutkanAtAnggota.Visible = show;
            ViewState.Remove("show_lanjutkan_at_anggota");
        }

        void AnggotaBatal_OnChildEventOccurs(object sender, EventArgs e)
        {
            lbLanjutkanAtAnggota.Visible = true;
        }

        void Child1_OnChildEventOccurs(object sender, EventArgs e)
        {
            ViewState["id_usulan_kegiatan"] = lstUsulanBaru.getIdUsulanKegiatan();
            initUsulan(lstUsulanBaru.getIdUsulanKegiatan());

            mvMain.SetActiveView(vUsulan);
            // start steps 1 Identitas
            Session["isEdit"] = true;
            clearAllButtonStepsIndikator();
            lbIdentitas1.ForeColor = System.Drawing.Color.Green;
            lbIdentitas2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vIdentitas);
            mvIdentitas.SetActiveView(vIDUsulan);

            IdentitasUsulan.InitIdentitasUsulan(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            cekStatusKirimUsulan(ViewState["id_usulan_kegiatan"].ToString());
        }

        void ChildPersyaratan_OnOccurs(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vCVKetua);
            cvKetua.isiCvKetua(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
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

        private void clearAllButtonStepsIndikator()
        {
            lbIdentitas2.CssClass = "btn btn-outline-primary";
            lbSubstansi2.CssClass = "btn btn-outline-primary";
            lbRab2.CssClass = "btn btn-outline-primary";
            lbDokPendukung2.CssClass = "btn btn-outline-primary";
            lbKirimUsulan2.CssClass = "btn btn-outline-primary";

            lbIdentitas1.ForeColor = System.Drawing.Color.Gray;
            lbSubstansi1.ForeColor = System.Drawing.Color.Gray;
            lbRab1.ForeColor = System.Drawing.Color.Gray;
            lbDokPendukung1.ForeColor = System.Drawing.Color.Gray;
            lbKirimUsulan1.ForeColor = System.Drawing.Color.Gray;
            //lblKirimUsulan.Visible = true;
            //lbKirimUsulan.Visible = false;
        }

        protected void lbLanjutkanAtCVKetua_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vUsulan);
            // start steps 1 Identitas

            clearAllButtonStepsIndikator();
            lbIdentitas1.ForeColor = System.Drawing.Color.Green;
            lbIdentitas2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vIdentitas);
            mvIdentitas.SetActiveView(vIDUsulan);

            if (Session["isEdit"] != null)
            {
                if (bool.Parse(Session["isEdit"].ToString()))
                {
                    initUsulan(IdentitasUsulan.IdUsulanKegiatan.ToString());
                    cekStatusKirimUsulan(IdentitasUsulan.IdUsulanKegiatan.ToString());
                }
                else
                {
                    IdentitasUsulan.InitIdentitasUsulan(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
                }
            }
            else
            {
                Session["isEdit"] = false;
                IdentitasUsulan.InitIdentitasUsulan(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            }

            //IdentitasUsulan.InitIdentitasUsulan();
        }

        private void cekStatusKirimUsulan(string pIdUsulanKegiatan)
        {
            lbSubmitUsulan.Visible = false;
            lblSubmitUsulan.Visible = false;
            DataTable dt = new DataTable();
            objModelIdentitasUsulan.getStatusUsulanDikirim(ref dt, pIdUsulanKegiatan);
            if (dt.Rows.Count > 0)
            {
                //panelUsulan.Enabled = false;
                lblSubmitUsulan.Visible = true;
            }
            else
            {
                //panelUsulan.Enabled = true;
                lbSubmitUsulan.Visible = true;
            }
        }

        protected void lbLanjutkanAtIDUsulan_Click(object sender, EventArgs e)
        {
            if (!IdentitasUsulan.Simpan()) return;
            mvIdentitas.SetActiveView(vCVAnggota);

            initUsulan(IdentitasUsulan.IdUsulanKegiatan.ToString());
            Session["isEdit"] = true;
            string idSkema = ViewState["id_skema"].ToString();
            DataTable dt = new DataTable();
            dt = ktAnggota.isiAnggotaDikti(IdentitasUsulan.IdUsulanKegiatan.ToString(),
                int.Parse(idSkema));
            ktAnggota.isiAnggotaTendik(IdentitasUsulan.IdUsulanKegiatan.ToString(),
                int.Parse(idSkema));
            ktAnggota.isiAnggotaNonDikti();
            ktAnggota.isiDurasiUsulan();

            //ViewState["dt_anggota"] = dt;
        }

        protected void lbLanjutkanAtAnggota_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus diisi terlebih dahulu");
                return;
            }

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            // start steps 2 Substansi
            clearAllButtonStepsIndikator();
            lbSubstansi1.ForeColor = System.Drawing.Color.Green;
            lbSubstansi2.CssClass = "btn btn-success";

            // Inisialisasi form unggah dokumen
            string dirFile = "~/fileUpload/dokumenUsulan/" + objUsulanKegiatan.thnUsulan;

            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            ktUnggah.path2save = String.Format(dirFile + "/{0}.pdf", objUsulanKegiatan.idUsulanKegiatan);
            ktUnggah.max_size = 5 * 1000 * 1000; // 5 MB
            ktUnggah.alllowed_ext = "pdf";
            ktUnggah.success_info = "Unggah dokumen usulan berhasil";
            ktUnggah.failed_info = "Unggah dokumen usulan gagal";
            Session.Add("ktUnggah", ktUnggah);

            mvUsulan.SetActiveView(vSubstansi);
            mvSubstansi.SetActiveView(vUnggahDokUsulan);
            ktDokUsulan.initData(objUsulanKegiatan);
        }

        protected void lbLanjutkanAtUnggahDokUsulan_Click(object sender, EventArgs e)
        {
            mvSubstansi.SetActiveView(vLuaran);
            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            ktLuaran.setDataLuaran(objUsulanKegiatan.idUsulan, objUsulanKegiatan.lamaKegiatan,
                    objUsulanKegiatan.urutanTahunUsulanKegiatan, objUsulanKegiatan.idUsulanKegiatan,
                    objUsulanKegiatan.namaSkema, objUsulanKegiatan.tktTarget, objUsulanKegiatan.idSkema);
        }

        protected void lbLanjutkanAtUnggahDokLuaran_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus diisi terlebih dahulu");
                return;
            }
            Session.Remove("ktUnggah");
            // start steps 3 RAB

            rab.InitRAB((usulanKegiatan)ViewState["usulan_kegiatan"], "1");

            clearAllButtonStepsIndikator();
            lbRab1.ForeColor = System.Drawing.Color.Green;
            lbRab2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vRAB);
            mvRAB.SetActiveView(vRABIsi);
        }
        protected void lbLanjutkanAtIsiRab_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus diisi terlebih dahulu");
                return;
            }
            // start steps 4 Unggah dokumen pendukung
            //if (!rab.Simpan()) return;

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            mitraPenelitian.setData(objUsulanKegiatan.thnUsulan);
            mitraPenelitian.isiMitra(IdentitasUsulan.IdUsulanKegiatan.ToString());
            mitraPenelitian.isiMitraPenelitianPerSkema();

            clearAllButtonStepsIndikator();
            lbDokPendukung1.ForeColor = System.Drawing.Color.Green;
            lbDokPendukung2.CssClass = "btn btn-success";
            mvUsulan.SetActiveView(vUnggahDokPendukung);
            mvDokPendukung.SetActiveView(vUnggahDokPendukungIsi);
            cekStatusEligibilitasKirimUsulan();

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            if (objUsulanKegiatan.idSkema == 72) // KRUPT
            {
                ktDokumenWbs.Visible = true;
                string dirFile = "~/fileUpload/dokumenWBS/" + objUsulanKegiatan.thnUsulan;
                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }
                ktUnggah.path2save = String.Format(dirFile + "/{0}.pdf", objUsulanKegiatan.idUsulanKegiatan);
                ktUnggah.max_size = 5 * 1000 * 1000; // 5 MB
                ktUnggah.alllowed_ext = "pdf";
                ktUnggah.success_info = "Unggah dokumen usulan berhasil";
                ktUnggah.failed_info = "Unggah dokumen usulan gagal";
                Session.Add("ktUnggah", ktUnggah);

                ktDokumenWbs.setDataUsulan(objUsulanKegiatan.idUsulanKegiatan, objUsulanKegiatan.thnUsulan,
                    ktUnggah.path2save, objUsulanKegiatan);
            }
        }

        private void cekStatusEligibilitasKirimUsulan()
        {
            //lblKirimUsulan.Visible = false;
            //lbKirimUsulan.Visible = true;
        }

        protected void lbLanjutkanAtDataPendukung_Click(object sender, EventArgs e)
        {
            if (ViewState["usulan_kegiatan"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus diisi terlebih dahulu");
                return;
            }
            // start steps 5 kirim usulan     

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            bool enableKirimUsulan = ktRekapUsulan.initUsulan(objUsulanKegiatan.idUsulanKegiatan);
            mvUsulan.SetActiveView(viewKirimUsulan);
            clearAllButtonStepsIndikator();
            lbKirimUsulan1.ForeColor = System.Drawing.Color.Green;
            lbKirimUsulan2.CssClass = "btn btn-success";
            //mvDokPendukung.SetActiveView(vUnggahDokPendukungIsi);
            //cekStatusEligibilitasKirimUsulan();
            lbSubmitUsulan.Visible = enableKirimUsulan;
            lblSubmitUsulan.Visible = !enableKirimUsulan;
        }

        protected void lbKirimUsulan_Click(object sender, EventArgs e)
        {

        }

        protected void lbLanjutkanAtPersyaratanUmum_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vCVKetua);
            cvKetua.isiCvKetua(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
        }

        protected void lbUnduhPdfDokLengkap_Click(object sender, EventArgs e)
        {
            pdfUsulanLengkap.UnduhProposalLengkap(ViewState["id_usulan_kegiatan"].ToString());
        }
        /*
        public void UnduhProposalLengkap() { 
            Session["usulan_kegiatan"] = ViewState["usulan_kegiatan"];
            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];

            byte[] pdfDepan = GetBytePdfDepan();
            ByteArrayOutputStream ms = new ByteArrayOutputStream();
            PdfDocument pdocDepan = new PdfDocument(new PdfReader(new MemoryStream(pdfDepan)), new PdfWriter(ms));
            pdocDepan.SetDefaultPageSize(new PageSize(PageSize.A4));

            PdfDocument pdfDokumenUsulan = getPdfDokumenUsulan();
            if(pdfDokumenUsulan!=null)
                pdfDokumenUsulan.CopyPagesTo(1, pdfDokumenUsulan.GetNumberOfPages(), pdocDepan);

            objLogin = (Models.login)Session["objLogin"];
            byte[] biodataKetua = GetBytePdfBiodataKetua(objLogin.idPersonal);
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
            if(dtm.Rows.Count>0)
            {
                for(int a=0; a< dtm.Rows.Count; a++)
                {
                    PdfDocument pdfMitra = getMitraDokumen(dtm.Rows[a]["id_mitra"].ToString());
                    if(pdfMitra!=null)
                        pdfMitra.CopyPagesTo(1, pdfMitra.GetNumberOfPages(), pdocDepan);
                }
            }

            Document document = new Document(pdocDepan);
            document.Close();
            downloadPdf(ms.ToArray());
        }

        void downloadPdf(byte[] bytes)
        {
            objLogin = (Models.login)Session["objLogin"];
            string namaFile = "UsulanLengkap "+objLogin.namaLengkap + ".pdf";
            //byte[] docData = mem.GetBuffer();
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.End();
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

        protected PdfDocument getPdfDokumenUsulan()
        {
            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            string filePath = "~/fileUpload/Dokumenusulan/"+ 
                    objUsulanKegiatan.thnUsulan+"/"+ 
                    objUsulanKegiatan.idUsulanKegiatan+".pdf"; 
            if(!File.Exists(Server.MapPath(filePath)))
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
        */
        protected void lbSubmitUsulanModal_Click(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalKonfirmasiKirim");

            objUsulanKegiatan = (usulanKegiatan)ViewState["usulan_kegiatan"];
            lblJudulDiKirim.Text = objUsulanKegiatan.judul;
        }

        protected void lbSubmitUsulan_Click(object sender, EventArgs e)
        {
            if (ViewState["id_skema"] != null)
            {
                if (ViewState["id_skema"].ToString() == "72" || ViewState["id_skema"].ToString() == "78" || ViewState["id_skema"].ToString() == "77") //72=krupt, 78=WCR 77=KKS
                {
                    if (objModelIdentitasUsulan.kirimUsulan(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                            "Kirim usulan berhasil.");
                        cekStatusKirimUsulan(ViewState["id_usulan_kegiatan"].ToString());
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                            "Silakan hubungi administrator.");
                    }
                }
                else
                {
                    kirimUsulan();
                }
            }
            else
            {
                kirimUsulan();
            }
        }

        private void kirimUsulan()
        {
            //if (Session["kd_sts_eligibilitas_ketua"] != null)
            //{
            //    if (Session["kd_sts_eligibilitas_ketua"].ToString() == "1")
            //    {
                    if (objModelIdentitasUsulan.kirimUsulan(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                            "Kirim usulan berhasil.");
                        cekStatusKirimUsulan(ViewState["id_usulan_kegiatan"].ToString());
                        CleanSession();
                        init();
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                            "Silakan hubungi administrator.");
                    }
                //}
                //else
                //{
                //    string ErrorMsg = "Silakan hubungi administrator.";
                //    if (Session["info_sts_eligibilitas_ketua"] != null)
                //    {
                //        ErrorMsg = Session["info_sts_eligibilitas_ketua"].ToString();
                //    }
                //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                //        ErrorMsg);
                //}
            //}
            //else
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //        "Silakan hubungi administrator.");
            //}
        }

        private void CleanSession()
        {
            if (Session["usulan_kegiatan"] != null)
                Session.Remove("usulan_kegiatan");
            if (Session["ktUnggah"] != null)
                Session.Remove("ktUnggah");
            if (Session["id_usulan_kegiatan"] != null)
                Session.Remove("id_usulan_kegiatan");
            if (Session["isEdit"] != null)
                Session.Remove("isEdit");
            if (Session["idPersonalAnggota"] != null)
                Session.Remove("idPersonalAnggota");
            if (Session["urutanAnggota"] != null)
                Session.Remove("urutanAnggota");
            if (Session["AtributUnduh"] != null)
                Session.Remove("AtributUnduh");

        }
        protected void ddlThnPelaksanaanKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vPersyaratan);
            int totalSyarat = persyaratan.isiDataPersyaratanUmum(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            //string thnUsulan = (int.Parse(ddlThnPelaksanaanKegiatan.SelectedValue) - 1).ToString();
            lstUsulanBaru.isiListUsulanBaru(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            lbPengajuanBaru.Visible = false;
            lblPengajuanBaru.Visible = false;
            if (totalSyarat < 4)
                lblPengajuanBaru.Visible = true;
            else
                lbPengajuanBaru.Visible = true;

        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vPersyaratan);
            int totalSyarat = persyaratan.isiDataPersyaratanUmum(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            //string thnUsulan = (int.Parse( ddlThnPelaksanaanKegiatan.SelectedValue) - 1).ToString();
            lstUsulanBaru.isiListUsulanBaru(ddlThnUsulan.SelectedValue, ddlThnPelaksanaanKegiatan.SelectedValue);
            lbPengajuanBaru.Visible = false;
            lblPengajuanBaru.Visible = false;
            if (totalSyarat < 4)
                lblPengajuanBaru.Visible = true;
            else
                lbPengajuanBaru.Visible = true;

        }

        private void isiDdlThnUsulan()
        {
            //int thnSkg = int.Parse(DateTime.Now.Year.ToString());
            //ddlThnUsulan.Items.Clear();
            //int maxThn = thnSkg + 1;
            //int minThn = 2018;
            ////ddlThnPelaksanaanKegiatan.Items.Add(new ListItem("-- Pilih --",""));
            //for (int a = maxThn; a >= minThn; a--)
            //{
            //    ddlThnUsulan.Items.Add(new ListItem(a.ToString(), a.ToString()));
            //}
            //ddlThnUsulan.Items.FindByValue(thnSkg.ToString()).Selected = true;
        }
        private void isiDdlThnPelaksanaan()
        {
            //int thnSkg = int.Parse(DateTime.Now.Year.ToString());
            //ddlThnPelaksanaanKegiatan.Items.Clear();
            //int maxThn = thnSkg + 1;
            //int minThn = 2019;
            ////ddlThnPelaksanaanKegiatan.Items.Add(new ListItem("-- Pilih --",""));
            //for (int a = maxThn; a >= minThn; a--)
            //{
            //    ddlThnPelaksanaanKegiatan.Items.Add(new ListItem(a.ToString(), a.ToString()));
            //}
            //ddlThnPelaksanaanKegiatan.Items.FindByValue(maxThn.ToString()).Selected = true;
        }

    }
}