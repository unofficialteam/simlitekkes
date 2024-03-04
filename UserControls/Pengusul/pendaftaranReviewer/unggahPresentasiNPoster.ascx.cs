using simlitekkes.Helper;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.pendaftaranReviewer
{
    public partial class unggahPresentasiNPoster : System.Web.UI.UserControl
    {
        Models.Pengusul.persyaratanUmumAbdimas objPersyaratan = new Models.Pengusul.persyaratanUmumAbdimas();
        uiNotify noty = new uiNotify();
        public event EventHandler OnChildEventOccursKembali;
        protected void Page_Load(object sender, EventArgs e)
        {
            KonfirmasiHapus.OnDelete += new EventHandler(KonfirmasiHapus_OnDelete);
        }

        protected void lbUnduhPenyajiTerbaik_Click(object sender, EventArgs e)
        {

        }

        public void setData(string idPendaftaran, string thnPelaksanaan)
        {
            ViewState["id_pendaftaran"] = idPendaftaran;
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            isiGvDokPendukung(idPendaftaran);
        }

        private void isiGvDokPendukung(string idPendaftaran)
        {
            DataTable dt = new DataTable();
            objPersyaratan.listDokumenPendukung(ref dt, idPendaftaran);
            //ViewState["list_dokumen"] = dt;
            gvDokumen.DataSource = dt;
            gvDokumen.DataBind();
        }
        //public void listDokumen(ref DataTable rdt)
        //{
        //    rdt = (DataTable)ViewState["list_dokumen"];
        //}

        protected void lbUnggahPenyajiTerbaik_Click(object sender, EventArgs e)
        {
            if (!cekFile(ref fileUploadDokPenyajiTerbaik))
            {
                return;
            }
            string noSertifikat = "-";
            if (tbNoSertifikat.Text != "")
            {
                noSertifikat = tbNoSertifikat.Text;
            }

            string dirFile = "~/fileUpload/" + ddlJenisDokumen.SelectedItem.Text.Replace(" ", "_");
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            dirFile = dirFile + "/" + ViewState["thn_pelaksanaan"].ToString();
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            Guid gIdDok = Guid.NewGuid();
            string filesaved = dirFile + "/" + gIdDok.ToString() + ".pdf";
            if (objPersyaratan.insertDokumenPendukung(gIdDok,
                Guid.Parse(ViewState["id_pendaftaran"].ToString()), noSertifikat,
                ddlJenisDokumen.SelectedValue))
            {
                unggahDokumen(ref fileUploadDokPenyajiTerbaik, filesaved);
                isiGvDokPendukung(ViewState["id_pendaftaran"].ToString());
                tbNoSertifikat.Text = "";
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah dokumen berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Unggah dokumen gagal.");
            }

        }
        private void unggahDokumen(ref FileUpload fileUpload1, string path)
        {
            if (File.Exists(Server.MapPath(path)))
            {
                File.Delete(Server.MapPath(path));
            }

            fileUpload1.SaveAs(Server.MapPath(path));
        }
        protected void lbKembali_Click(object sender, EventArgs e)
        {
            if (OnChildEventOccursKembali != null)
                OnChildEventOccursKembali(sender, null);
        }

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

        protected void KonfirmasiHapus_OnDelete(object sender, EventArgs e)
        {
            deleteDokPendukung(ViewState["id_dokumen"].ToString(), ViewState["jenis_dokumen"].ToString(),
                ViewState["thn_pelaksanaan"].ToString());
            isiGvDokPendukung(ViewState["id_pendaftaran"].ToString());
        }

        public void deleteDokPendukung(string idDokumen, string jenisDokumen, string thnPelaksanaan)
        {
            if (objPersyaratan.deleteDokumen(Guid.Parse(idDokumen)))
            {

                string dirFile = "~/fileUpload/" + jenisDokumen.Replace(" ", "_");

                dirFile = dirFile + "/" + thnPelaksanaan;
                string namaFile = dirFile + "/" + idDokumen + ".pdf";
                if (File.Exists(Server.MapPath(namaFile)))
                {
                    File.Delete(Server.MapPath(namaFile));
                }
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal.");
            }
        }

        protected void gvDokumen_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvDokumen_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["id_dokumen"] = gvDokumen.DataKeys[e.RowIndex]["id_dokumen"].ToString();
            ViewState["jenis_dokumen"] = gvDokumen.DataKeys[e.RowIndex]["jenis_dokumen"].ToString();
            KonfirmasiHapus.TitleKonfirmasi = "Konfirmasi hapus dokumen pendukung";
            KonfirmasiHapus.TextKonfirmasi = "Apakah anda yakin akan menghapus data ini? <br>Dokumen akan dihapus";
            KonfirmasiHapus.Show();
        }

        protected void gvDokumen_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string idDokumen = gvDokumen.DataKeys[e.NewEditIndex]["id_dokumen"].ToString();
            string jnsDokumen = gvDokumen.DataKeys[e.NewEditIndex]["jenis_dokumen"].ToString();

            string dirFile = "~/fileUpload/" + jnsDokumen.Replace(" ", "_");

            dirFile = dirFile + "/" + ViewState["thn_pelaksanaan"].ToString();
            string namaFile = dirFile + "/" + idDokumen + ".pdf";
            //string namaFile = idDokumen + ".pdf";
            if (File.Exists(Server.MapPath(namaFile)))
            {
                unduhPDF(dirFile, idDokumen, jnsDokumen.Replace(" ","_") + "_" + (e.NewEditIndex+1).ToString() + ".pdf" );
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

    }
}