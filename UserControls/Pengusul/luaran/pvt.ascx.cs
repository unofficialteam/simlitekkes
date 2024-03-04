using simlitekkes.Core;
using simlitekkes.Helper;
using simlitekkes.Models;
using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.luaran
{
    public partial class pvt : System.Web.UI.UserControl
    {

        uiDropdownList ddlHelper = new uiDropdownList();
        uiNotify noty = new uiNotify();
        manipulasiData objManipData = new manipulasiData();

        login objLogin;
        Models.Pengusul.luaran modelPengusul = new Models.Pengusul.luaran();
        //berandaPengusul modelPengusul = new berandaPengusul();

        const string PATH_UNGGAH_BERKAS = "~/fileUpload/PVT/";
        public event EventHandler OnChildEventKembaliOccurs;


        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (login)Session["objLogin"];

            if (!IsPostBack)
            {

            }
        }

        private void resetForm()
        {
            tbNamaVarietas.Text = string.Empty;
            tbNamaPemohon.Text = string.Empty;
            tbAlamatPemohon.Text = string.Empty;
            tbNoPermohonan.Text = string.Empty;
            tbTglPermohonan.Text = string.Empty;
        }

        public void isiLuaranPVT(Guid idLuaranDijanjikan, string idJenisLuaran, string thnPelaksanaan)
        {
            var dt = new DataTable();
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            ViewState["id_luaran_dijanjikan"] = idLuaranDijanjikan;
            ViewState["id_jenis_luaran"] = idJenisLuaran;
            modelPengusul.getLuaranPVT(ref dt, idLuaranDijanjikan);
            if (dt.Rows.Count > 0)
            {
                tbNamaVarietas.Text = dt.Rows[0]["nama_varietas"].ToString();
                tbNamaPemohon.Text = dt.Rows[0]["nama_pemohon"].ToString();
                tbAlamatPemohon.Text = dt.Rows[0]["alamat_pemohon"].ToString();
                tbNoPermohonan.Text = dt.Rows[0]["no_permohonan"].ToString();
                tbTglPermohonan.Text = dt.Rows[0]["tgl_permohonan"].ToString();

                ViewState["id_luaran_pvt"] = dt.Rows[0]["id_luaran_pvt"].ToString();
                ViewState["id_dok_permohonan"] = dt.Rows[0]["id_dok_permohonan"].ToString();
                ViewState["id_sertifikat_hak_pvt"] = dt.Rows[0]["id_sertifikat_hak_pvt"].ToString();
                ViewState["IsNew"] = false;
            }
            else
            {
                ViewState["IsNew"] = true;
                ViewState["id_luaran_pvt"] = Guid.NewGuid();
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            var isNew = bool.Parse(ViewState["IsNew"].ToString());

            List<string> emptyField = new List<string>();
            if (tbNamaVarietas.Text.Trim().Length == 0) emptyField.Add("Nama Varietas");
            if (tbNamaPemohon.Text.Trim().Length == 0) emptyField.Add("Nama Pemohon");
            if (tbAlamatPemohon.Text.Trim().Length == 0) emptyField.Add("Alamat Pemohon");
            if (tbNoPermohonan.Text.Trim().Length == 0) emptyField.Add("No Permohonan");
            if (tbTglPermohonan.Text.Trim().Length == 0) emptyField.Add("Tgl Permohonan");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }


            string dirFile = "~/fileUpload/PVT";
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            dirFile = "~/fileUpload/PVT/" + ViewState["thn_pelaksanaan"].ToString();
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            // Jika baru, get id dr database jika update
            Guid idSertifikatPVT = Guid.NewGuid();
            if (!isNew)
            {
                idSertifikatPVT = Guid.Parse(ViewState["id_sertifikat_hak_pvt"].ToString());
            }

            string file2saved = dirFile + "/" + idSertifikatPVT.ToString() + ".pdf";
            // unggah 
            if (cekFile(ref fileUploadSertifikat))
            {
                unggahDokumen(ref fileUploadSertifikat, file2saved);
            }
            else
            {
                return;
            }

            // Jika baru, get id dr database jika update
            Guid idDokumentasi = Guid.NewGuid();
            if (!isNew)
            {
                idDokumentasi = Guid.Parse(ViewState["id_dok_permohonan"].ToString());
            }

            file2saved = dirFile + "/" + idDokumentasi.ToString() + ".pdf";
            if (cekFile(ref fileUploadDok))
            {
                unggahDokumen(ref fileUploadDok, file2saved);
            }
            else
            {
                return;
            }

            if (modelPengusul.insupLuaranPVT(
                        Guid.Parse(ViewState["id_luaran_pvt"].ToString()),
                        Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()),
                        ViewState["id_jenis_luaran"].ToString(),
                        tbNamaVarietas.Text,
                        tbNoPermohonan.Text,
                        tbTglPermohonan.Text,
                        tbNamaPemohon.Text,
                        tbAlamatPemohon.Text,
                        idDokumentasi,
                        idSertifikatPVT
                        ))
            {
                //if (bool.Parse(ViewState["isUnggah"].ToString()))
                //{
                modelPengusul.updateStatusUnggahPVT(Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()));
                //}                
                if (OnChildEventKembaliOccurs != null)
                    OnChildEventKembaliOccurs(null, null);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil disimpan...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPengusul.errorMessage);
            }
        }

        protected void btnBatal_Click(object sender, EventArgs e)
        {
            if (OnChildEventKembaliOccurs != null)
                OnChildEventKembaliOccurs(null, null);
        }

        private bool cekFile(ref FileUpload fileUpload1)
        {
            bool isSuccess = false;
            string extFile = ".pdf";
            int maxSize = 5 * 1000 * 1000; // 5 MB
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
    }
}