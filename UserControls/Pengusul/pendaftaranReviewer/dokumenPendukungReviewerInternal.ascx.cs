using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.Models;
using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System.IO;
using simlitekkes.Helper;

namespace simlitekkes.UserControls.Pengusul.pendaftaranReviewer
{

    public partial class dokumenPendukungReviewerInternal : System.Web.UI.UserControl
    {
        login objLogin;
        persyaratanUmumPendaftaranReviewer objPersyaratan = new persyaratanUmumPendaftaranReviewer();
        Models.Pengusul.berandaPengusul objBerandaPengusul = new Models.Pengusul.berandaPengusul();

        uiNotify noty = new uiNotify();
        uiDropdownList objDdl = new uiDropdownList();

        public Guid idJenisDokMotivasi = Guid.Parse("53032032-4aca-47ce-a7cd-c6d429ed773a");
        public Guid idJenisDokPaktaIntegritas = Guid.Parse("47ab2a10-7ecb-45e5-b74c-a9f96940d6f0");
        public Guid idJenisDokPernyataan = Guid.Parse("ad786ebc-f590-4687-8162-cdb5dc613062");
        public Guid idJenisDokSertifikat = Guid.Parse("4378f6c3-56e5-456c-b7a1-256bcb681c98");

        public event EventHandler OnChildEventOccursKembali;
        protected void Page_Load(object sender, EventArgs e)
        {
            //isiDdlThnPublikasi();
            //isiDataDosen();            
        }

        public void init(bool kdStsReviewerBaru)
        {
            ViewState["kdStsReviewerBaru"] = kdStsReviewerBaru;
        }


        public void setIdPendaftaran(string p_id_pendaftaran, string p_sts_pernyataan_pakta_integritas, string p_sts_pernyataan_kode_etik)
        {
            ViewState["id_pendaftaran"] = p_id_pendaftaran;
            if (p_sts_pernyataan_pakta_integritas == "1")
            {
                cbPernyataanIntegritas.Checked = true;
            }
            else cbPernyataanIntegritas.Checked = false;
            if (p_sts_pernyataan_kode_etik == "1")
            {
                cbKodeEtik.Checked = true;
            }
            else cbKodeEtik.Checked = false;

            isiDdlThnPublikasi();
            isiDataDosen();
        }

        private void isiDdlThnPublikasi()
        {
            ddlTahun.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            thnSKg = thnSKg + 1;
            for (int i = thnSKg; i >= 2019; i--)
            {
                ddlTahun.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void isiDataDosen()
        {
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_pendaftaran = ddlTahun.SelectedValue;
            ViewState["thn_pelaksanaan"] = thn_pendaftaran;

            // Isi Data Dosen
            var dtPersonal = new DataTable();
            if (objBerandaPengusul.getPersonal(ref dtPersonal, id_personal))
            {
                if (dtPersonal.Rows.Count > 0)
                {
                    lblInstitusi.Text = dtPersonal.Rows[0]["nama_institusi"].ToString();
                    lblProgramStudi.Text = dtPersonal.Rows[0]["nama_program_studi"].ToString();
                    lblPendidikan.Text = dtPersonal.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
                    lblJabatanFungsional.Text = dtPersonal.Rows[0]["jabatan_fungsional"].ToString();
                    lblStatus.Text = dtPersonal.Rows[0]["status_aktif"].ToString();
                    lblHIndex.Text = dtPersonal.Rows[0]["hindex"].ToString();
                }
            }

            cekStsDok();
        }

        private void cekStsDok()
        {
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_pendaftaran = ddlTahun.SelectedValue;
            var dtMotivasi = new DataTable();
            //if (objPersyaratan.getDokumen(ref dtMotivasi, id_personal, thn_pendaftaran, idJenisDokMotivasi))
            if (objPersyaratan.getDokumenUnggah(ref dtMotivasi, ViewState["id_pendaftaran"].ToString(), idJenisDokMotivasi.ToString()))

                {
                    if (dtMotivasi.Rows.Count > 0)
                {
                    lbUnduhPdfDokMotivasi.CssClass = "fa fa-file-pdf-o";
                    lbUnduhPdfDokMotivasi.ForeColor = System.Drawing.Color.Red;
                }
            }
            //var dtPaktaIntegritas = new DataTable();
            //if (objPersyaratan.getDokumen(ref dtPaktaIntegritas, id_personal, thn_pendaftaran, idJenisDokPaktaIntegritas))
            //{
            //    if (dtPaktaIntegritas.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPaktaIntegritas.CssClass = "fa fa-file-pdf-o";
            //        lbUnduhPdfDokPaktaIntegritas.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            //var dtPernyataan = new DataTable();
            //if (objPersyaratan.getDokumen(ref dtPernyataan, id_personal, thn_pendaftaran, idJenisDokPernyataan))
            //{
            //    if (dtPernyataan.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPernyataan.CssClass = "fa fa-file-pdf-o";
            //        lbUnduhPdfDokPernyataan.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            var dtSertifikat = new DataTable();
            //if (objPersyaratan.getDokumen(ref dtSertifikat, id_personal, thn_pendaftaran, idJenisDokSertifikat))

            if (objPersyaratan.getDokumenUnggah(ref dtSertifikat, ViewState["id_pendaftaran"].ToString(), idJenisDokSertifikat.ToString()))
                {
                if (dtSertifikat.Rows.Count > 0)
                {
                    lbUnduhPdfDokSertifikat.CssClass = "fa fa-file-pdf-o";
                    lbUnduhPdfDokSertifikat.ForeColor = System.Drawing.Color.Red;
                }
            }

            var stsReviewer = bool.Parse(ViewState["kdStsReviewerBaru"].ToString());
            //tblUnggahTambahan.Visible = stsReviewer;
            if (stsReviewer == false)
            {
                lblJudulPendaftaran.Text = "Daftar Ulang Reviewer Nasional (Penelitian)";
            }
            else
            {
                lblJudulPendaftaran.Text = "Pendaftaran Reviewer Nasional (Penelitian)";
            }
        }

        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiDataDosen();
        }

        protected void lbUnduhMotivasi1_Click(object sender, EventArgs e)
        {
            string namaFileTemplate = "motivasi_penelitian";
            unduhTemplate(namaFileTemplate);
        }

        protected void lbUnduhMotivasi2_Click(object sender, EventArgs e)
        {
            string namaFileTemplate = "motivasi_penelitian";
            unduhTemplate(namaFileTemplate);
        }

        protected void lbPaktaIntegritas1_Click(object sender, EventArgs e)
        {
            string namaFileTemplate = "pakta_integritas_penelitian";
            unduhTemplate(namaFileTemplate);
        }

        protected void lbPaktaIntegritas2_Click(object sender, EventArgs e)
        {
            string namaFileTemplate = "pakta_integritas_penelitian";
            unduhTemplate(namaFileTemplate);
        }

        protected void lbMematuhiKodeEtik1_Click(object sender, EventArgs e)
        {
            string namaFileTemplate = "kode_etik_penelitian";
            unduhTemplate(namaFileTemplate);
        }

        protected void lbMematuhiKodeEtik2_Click(object sender, EventArgs e)
        {
            string namaFileTemplate = "kode_etik_penelitian";
            unduhTemplate(namaFileTemplate);
        }

        protected void lbUnduhPdfDokMotivasi_Click(object sender, EventArgs e)
        {
            unduhDokMotivasi();
        }

        public string getStsDokumen()
        {
            string kdStsDok = "1";
            if (ViewState["kd_sts_dokumen"] == null)
            {
                kdStsDok = "0";
            }
            else
            {
                kdStsDok = ViewState["kd_sts_dokumen"].ToString();
            }
            return kdStsDok;
        }


        public string getDokMotivasi()
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokMotivasi.ToString();
            bool kdStsDok = cekUnggahDokumen(id_jenis_dok_pendaftaran_reviewer);
            if (kdStsDok) return ViewState["id_dokumen_reviewer"].ToString();
            else return "";
        }

        public string getDokSertifikat()
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokSertifikat.ToString();
            bool kdStsDok = cekUnggahDokumen(id_jenis_dok_pendaftaran_reviewer);
            if (kdStsDok) return ViewState["id_dokumen_reviewer"].ToString();
            else return "";
        }

        public void hapusDokSertifikat(string idDokRev)
        {
            string namaFilee = "~/fileUpload/Sertifikat_reviewer_internal/" + ddlTahun.SelectedValue + "/" + idDokRev + ".pdf";
            if (File.Exists(Server.MapPath(namaFilee)))
            {
                File.Delete(Server.MapPath(namaFilee));
            }
        }

        public void hapusDokMotivasi(string idDokRev)
        {
            string namaFilee = "~/fileUpload/Motivasi_penelitian/" + ddlTahun.SelectedValue + "/" + idDokRev + ".pdf";
            if (File.Exists(Server.MapPath(namaFilee)))
            {
                File.Delete(Server.MapPath(namaFilee));
            }
        }

        private bool cekUnggahDokumen(string id_jenis_dok_pendaftaran_reviewer)
        {
            bool sdhDiunggah = false;
            var dt = new DataTable();
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_pendaftaran = ViewState["thn_pelaksanaan"].ToString();
            //objPersyaratan.get
            if (objPersyaratan.getDokumenUnggah(ref dt, ViewState["id_pendaftaran"].ToString(), id_jenis_dok_pendaftaran_reviewer))
            {
                if (dt.Rows.Count > 0)
                {
                    ViewState["id_dokumen_reviewer"] = dt.Rows[0]["id_dokumen_reviewer"].ToString();
                    ViewState["kd_sts_dokumen"] = dt.Rows[0]["kd_sts_dokumen"].ToString();
                    sdhDiunggah = true;
                }
                //else
                //{
                //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen belum diunggah");
                //}
            }
            return sdhDiunggah;
        }
        public void unduhDokMotivasi()
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokMotivasi.ToString(); // Guid.Parse("564b96c1-802e-4384-8b18-1b254f1c0c38").ToString();
            cekUnggahDokumen(id_jenis_dok_pendaftaran_reviewer);
            string idDokRev = ViewState["id_dokumen_reviewer"].ToString();
            string kdStsDok = getStsDokumen();
            if (kdStsDok == "1")
            {
                //string namaFileTemplate = "~/fileUpload/Motivasi_penelitian/" + ddlTahun.SelectedValue + "/" + idDokRev;
                //unduhDokumen(namaFileTemplate);
                string folderUnduh = "~/fileUpload/Motivasi_penelitian/" + ddlTahun.SelectedValue ;
                string namaFileAsli = idDokRev;
                string namaFileDiunduh = "Dok_motivasi_reviewer_internal.pdf";

                if (File.Exists(Server.MapPath(folderUnduh + "/" + namaFileAsli + ".pdf")))
                {
                    unduhPDF(folderUnduh, namaFileAsli, namaFileDiunduh);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        protected void lbUnggahMotivasi_Click(object sender, EventArgs e)
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokMotivasi.ToString(); // Guid.Parse("564b96c1-802e-4384-8b18-1b254f1c0c38").ToString();
            string dirFile = "~/fileUpload/Motivasi_penelitian";
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            dirFile = "~/fileUpload/Motivasi_penelitian/" + ViewState["thn_pelaksanaan"].ToString();
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            cekDokumen(id_jenis_dok_pendaftaran_reviewer);
            //var isNew = bool.Parse(ViewState["IsNew"].ToString());
            Guid idDokReviewer = Guid.Parse(ViewState["id_dokumen_reviewer"].ToString());

            string filesaved = dirFile + "/" + idDokReviewer.ToString() + ".pdf";
            // unggah 
            if (cekFile(ref fileUploadMotivasi))
            {
                unggahDokumen(ref fileUploadMotivasi, filesaved);
            }
            else
            {
                return;
            }

            Guid idPendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            Guid idJenisDok = Guid.Parse(id_jenis_dok_pendaftaran_reviewer);
            string kdStsDok = "1";
            simpanDok(idDokReviewer, idPendaftaran, idJenisDok, kdStsDok);
            cekStsDok();
        }

        protected void lbUnduhPdfDokPaktaIntegritas_Click(object sender, EventArgs e)
        {
            unduhDokPaktaIntegritas();
        }

        public void unduhDokPaktaIntegritas()
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokPaktaIntegritas.ToString(); //Guid.Parse("5bc61cf5-c568-4e8e-909b-65380f9d1a70").ToString();
            cekUnggahDokumen(id_jenis_dok_pendaftaran_reviewer);
            string idDokRev = ViewState["id_dokumen_reviewer"].ToString();
            string kdStsDok = getStsDokumen();

            if (kdStsDok == "1")
            {
                string namaFileTemplate = "~/fileUpload/Pakta_integritas_penelitian/" + ddlTahun.SelectedValue + "/" + idDokRev;
                unduhDokumen(namaFileTemplate);

            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        protected void lbUnggahPaktaIntegritas_Click(object sender, EventArgs e)
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokPaktaIntegritas.ToString(); // Guid.Parse("5bc61cf5-c568-4e8e-909b-65380f9d1a70").ToString();
            string dirFile = "~/fileUpload/Pakta_integritas_penelitian";
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            dirFile = "~/fileUpload/Pakta_integritas_penelitian/" + ViewState["thn_pelaksanaan"].ToString();
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            cekDokumen(id_jenis_dok_pendaftaran_reviewer);
            //var isNew = bool.Parse(ViewState["IsNew"].ToString());
            Guid idDokReviewer = Guid.Parse(ViewState["id_dokumen_reviewer"].ToString());

            //string filesaved = dirFile + "/" + idDokReviewer.ToString() + ".pdf";
            //// unggah 
            //if (cekFile(ref fileUploadPaktaIntegritas))
            //{
            //    unggahDokumen(ref fileUploadPaktaIntegritas, filesaved);
            //}
            //else
            //{
            //    return;
            //}

            Guid idPendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            Guid idJenisDok = Guid.Parse(id_jenis_dok_pendaftaran_reviewer);
            string kdStsDok = "1";
            simpanDok(idDokReviewer, idPendaftaran, idJenisDok, kdStsDok);
            cekStsDok();
        }

        protected void lbUnduhPdfDokPernyataan_Click(object sender, EventArgs e)
        {
            unduhDokPernyataan();
        }

        public void unduhDokPernyataan()
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokPernyataan.ToString(); // Guid.Parse("4a82118c-ce54-4c42-9304-cb7a2f57383e").ToString();
            cekUnggahDokumen(id_jenis_dok_pendaftaran_reviewer);
            string idDokRev = ViewState["id_dokumen_reviewer"].ToString();
            string kdStsDok = getStsDokumen();

            if (kdStsDok == "1")
            {
                string namaFileTemplate = "~/fileUpload/Pernyataan_kode_etik_penelitian/" + ddlTahun.SelectedValue + "/" + idDokRev;
                unduhDokumen(namaFileTemplate);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        protected void lbUnggahPernyataan_Click(object sender, EventArgs e)
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokPernyataan.ToString(); // Guid.Parse("4a82118c-ce54-4c42-9304-cb7a2f57383e").ToString();
            string dirFile = "~/fileUpload/Pernyataan_kode_etik_penelitian";
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            dirFile = "~/fileUpload/Pernyataan_kode_etik_penelitian/" + ViewState["thn_pelaksanaan"].ToString();
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            cekDokumen(id_jenis_dok_pendaftaran_reviewer);
            //var isNew = bool.Parse(ViewState["IsNew"].ToString());
            Guid idDokReviewer = Guid.Parse(ViewState["id_dokumen_reviewer"].ToString());

            //string filesaved = dirFile + "/" + idDokReviewer.ToString() + ".pdf";
            //// unggah 
            //if (cekFile(ref fileUploadPernyataan))
            //{
            //    unggahDokumen(ref fileUploadPernyataan, filesaved);
            //}
            //else
            //{
            //    return;
            //}

            Guid idPendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            Guid idJenisDok = Guid.Parse(id_jenis_dok_pendaftaran_reviewer);
            string kdStsDok = "1";
            simpanDok(idDokReviewer, idPendaftaran, idJenisDok, kdStsDok);
            cekStsDok();
        }

        protected void lbUnduhPdfDokSertifikat_Click(object sender, EventArgs e)
        {
            UnduhDokSertifikat();
        }

        public void UnduhDokSertifikat()
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokSertifikat.ToString(); // Guid.Parse("a3796de5-f2da-49ac-8d83-33d308ebb617").ToString();
            cekUnggahDokumen(id_jenis_dok_pendaftaran_reviewer);
            string idDokRev = ViewState["id_dokumen_reviewer"].ToString();
            string kdStsDok = getStsDokumen();

            if (kdStsDok == "1")
            {
                //string namaFileTemplate = "~/fileUpload/Sertifikat_reviewer/" + ddlTahun.SelectedValue + "/" + idDokRev+"/";
                //unduhDokumen(namaFileTemplate);
                string folderUnduh = "~/fileUpload/Sertifikat_reviewer_internal/" + ddlTahun.SelectedValue;
                string namaFileAsli = idDokRev ;
                string namaFileDiunduh = "Dok_sertifikat_pelatihan_reviewer_internal.pdf";

                if (File.Exists(Server.MapPath(folderUnduh + "/" + namaFileAsli + ".pdf")))
                {
                    unduhPDF(folderUnduh, namaFileAsli, namaFileDiunduh);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        protected void lbUnggahSertifikat_Click(object sender, EventArgs e)
        {
            string id_jenis_dok_pendaftaran_reviewer = idJenisDokSertifikat.ToString(); // Guid.Parse("a3796de5-f2da-49ac-8d83-33d308ebb617").ToString();
            string dirFile = "~/fileUpload/Sertifikat_reviewer_internal";
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            dirFile = "~/fileUpload/Sertifikat_reviewer_internal/" + ViewState["thn_pelaksanaan"].ToString();
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            cekDokumen(id_jenis_dok_pendaftaran_reviewer);
            //var isNew = bool.Parse(ViewState["IsNew"].ToString());
            Guid idDokReviewer = Guid.Parse(ViewState["id_dokumen_reviewer"].ToString());

            string filesaved = dirFile + "/" + idDokReviewer.ToString() + ".pdf";
            // unggah 
            if (cekFile(ref fileUploadSertifikat))
            {
                unggahDokumen(ref fileUploadSertifikat, filesaved);
            }
            else
            {
                return;
            }

            Guid idPendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            Guid idJenisDok = Guid.Parse(id_jenis_dok_pendaftaran_reviewer);
            string kdStsDok = "1";
            Guid idPersonal = Guid.Parse(objLogin.idPersonal);
            string kdStsUnggahSertifikat = "1";
            string noSertifikat = tbNoSertifikat.Text;
            simpanDok(idDokReviewer, idPendaftaran, idJenisDok, kdStsDok);
            updateDokSertifikat(idPersonal, noSertifikat, kdStsUnggahSertifikat);
            cekStsDok();
        }       

        private void unduhTemplate (string namaFileTemplate)
        {
            string path = string.Format("~/dokumen/template/" + namaFileTemplate + ".docx");

            if (File.Exists(Server.MapPath(path)))
            {
                Response.Redirect(path);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        public void cekDokumen(string id_jenis_dok_pendaftaran_reviewer)
        {
            var dt = new DataTable();
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_pendaftaran = ViewState["thn_pelaksanaan"].ToString();

            //if (objPersyaratan.getDokumen(ref dt, id_personal, thn_pendaftaran, Guid.Parse(id_jenis_dok_pendaftaran_reviewer)))
                if (objPersyaratan.getDokumenUnggah(ref dt, ViewState["id_pendaftaran"].ToString(), id_jenis_dok_pendaftaran_reviewer))

                {
                    if (dt.Rows.Count > 0) 
                {
                    //ViewState["id_pendaftaran"] = dt.Rows[0]["id_pendaftaran"].ToString();
                    ViewState["id_dokumen_reviewer"] = dt.Rows[0]["id_dokumen_reviewer"].ToString();
                    ViewState["id_jenis_dok_pendaftaran_reviewer"] = id_jenis_dok_pendaftaran_reviewer;
                    ViewState["kd_sts_dokumen"] = dt.Rows[0]["kd_sts_dokumen"].ToString();
                   // ViewState["IsNew"] = false;
                }
                else
                {
                    //var dtBaru = new DataTable();
                    //objPersyaratan.getDokumenBaru(ref dtBaru, id_personal, thn_pendaftaran);
                   // ViewState["IsNew"] = true;
                    ViewState["id_dokumen_reviewer"] = Guid.NewGuid();
                    //ViewState["id_pendaftaran"] = dtBaru.Rows[0]["id_pendaftaran"].ToString();
                }
            }            
        }

        //private void cekUnggahDokumen(string id_jenis_dok_pendaftaran_reviewer)
        //{
        //    var dt = new DataTable();
        //    objLogin = (login)Session["objLogin"];
        //    string id_personal = objLogin.idPersonal;
        //    string thn_pendaftaran = ViewState["thn_pelaksanaan"].ToString();

        //    if (objPersyaratan.getDokumen(ref dt, id_personal, thn_pendaftaran, Guid.Parse(id_jenis_dok_pendaftaran_reviewer)))
        //    {
        //        if (dt.Rows.Count > 0) 
        //        {
        //            ViewState["id_dokumen_reviewer"] = dt.Rows[0]["id_dokumen_reviewer"].ToString();
        //            ViewState["kd_sts_dokumen"] = dt.Rows[0]["kd_sts_dokumen"].ToString();
        //        }
        //        else
        //        {
        //            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen belum diunggah");
        //        }
        //    }
        //}

        private bool cekFile(ref FileUpload fileUpload1)
        {
            bool isSuccess = false;
            string extFile = ".pdf";
            int maxSize = 1 * 1000 * 1000; // 1 MB
            if (fileUpload1.HasFile)
            {
                if (fileUpload1.PostedFile.ContentLength < maxSize)
                {
                    if (fileUpload1.FileName.ToLower().EndsWith(extFile))
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File harus berekstensi: " + extFile);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Ukuran file maksimal: " + (maxSize / 1024).ToString() + " KByte");

                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File belum dipilih");
            }

            return isSuccess;
        }

        private void unggahDokumen(ref FileUpload fileUpload1, string path)
        {
            if (File.Exists(Server.MapPath(path)))
            {
                File.Delete(Server.MapPath(path));
            }

            fileUpload1.SaveAs(Server.MapPath(path));
        }

        private void simpanDok(Guid idDokRev, Guid idPendaftaran, Guid idJenisDok, string kdStsDok)
        {
            if (objPersyaratan.insupDokPendaftaranReviewer(
                        idDokRev, idPendaftaran, idJenisDok, kdStsDok
                        ))
            {
                //if (OnChildEventKembaliOccurs != null)
                //    OnChildEventKembaliOccurs(null, null);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil disimpan...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objPersyaratan.errorMessage);
            }
        }

        private void unduhDokumen(string namaFileDok)
        {
            string path = string.Format(namaFileDok + ".pdf");

            if (File.Exists(Server.MapPath(path)))
            {
                Response.Redirect(path);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        private void unduhPDF(string folderUnduh, string namaFileAsli, string namaFileDiunduh)
        {
            //string namaBerkas = "dokumenUsulan.pdf";
            var atributUnduh = new AtributUnduh
            {
                FolderUnduh = folderUnduh,
                NamaBerkas = namaFileAsli + ".pdf",
                NamaBerkasdiUnduh = namaFileDiunduh
            };
            Session["AtributUnduh"] = atributUnduh;
            var unduhForm = "helper/unduhFile.aspx";
            Response.Redirect(unduhForm);
        }


        protected void lbKembali_Click(object sender, EventArgs e)
        {

            if (OnChildEventOccursKembali != null)
                OnChildEventOccursKembali(sender, null);
        }

        private void updateDokSertifikat(Guid idPersonal, string noSertifikat, string kdStsUnggahSertifikat)
        {
            if (objPersyaratan.updateStsSertifikat(
                        idPersonal, noSertifikat, kdStsUnggahSertifikat
                        ))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil disimpan...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objPersyaratan.errorMessage);
            }
        }

        protected void cbPernyataanIntegritas_CheckedChanged(object sender, EventArgs e)
        {
            Guid idPendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            string stsPaktaIntegritas = "0";
            if (cbPernyataanIntegritas.Checked)
                stsPaktaIntegritas = "1";
            if (objPersyaratan.updatePernyataanPaktaIntegritas(idPendaftaran, stsPaktaIntegritas))
            {
                if (stsPaktaIntegritas == "1")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Anda Menyetujui Pernyataan Pakta Integritas.");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Anda Membatalkan Menyetujui Pernyataan Pakta Integritas.");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Terjadi kesalahan, silakan hubungi administrator.");
            }

        }

        protected void cbKodeEtik_CheckedChanged(object sender, EventArgs e)
        {
            Guid idPendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            string stsKodeEtik = "0";
            if (cbKodeEtik.Checked)
                stsKodeEtik = "1";
            if (objPersyaratan.updatePernyataanKodeEtik(idPendaftaran, stsKodeEtik))
            {
                if (stsKodeEtik == "1")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Anda Menyetujui Pernyataan mematuhi Kode Etik dan Kesanggupan Melaksanakan Tugas.");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Anda Membatalkan Menyetujui Pernyataan mematuhi Kode Etik dan Kesanggupan Melaksanakan Tugas.");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Terjadi kesalahan, silakan hubungi administrator.");
            }
        }

    }
}