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
    public partial class paten : System.Web.UI.UserControl
    {
        uiDropdownList ddlHelper = new uiDropdownList();
        uiNotify noty = new uiNotify();
        manipulasiData objManipData = new manipulasiData();

        login objLogin;
        Models.Pengusul.luaran modelPengusul = new Models.Pengusul.luaran();
        //berandaPengusul modelPengusul = new berandaPengusul();

        const string PATH_UNGGAH_BERKAS = "~/fileUpload/HKI/";
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
            tbJudul.Text = string.Empty;
            tbNoHKI.Text = string.Empty;
            tbNoPendaftaran.Text = string.Empty;
            tbPemegangHKI.Text = string.Empty;
        }

        public void isiLuaranHKI(Guid idLuaranDijanjikan, string idJenisLuaran, string thnPelaksanaan)
        {
            var dt = new DataTable();
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            ViewState["id_luaran_dijanjikan"] = idLuaranDijanjikan;
            ViewState["id_jenis_luaran"] = idJenisLuaran;
            modelPengusul.getLuaranHKI(ref dt, idLuaranDijanjikan);
            if (dt.Rows.Count > 0)
            {
                tbJudul.Text = dt.Rows[0]["judul"].ToString();
                tbNoHKI.Text = dt.Rows[0]["nama_pemegang_hak_cipta"].ToString();
                tbNoPendaftaran.Text = dt.Rows[0]["no_pendaftaran"].ToString();
                tbPemegangHKI.Text = dt.Rows[0]["no_hki"].ToString();

                ViewState["id_luaran_hki"] = dt.Rows[0]["id_luaran_hki"].ToString();
                ViewState["id_sertifikat_pencatatan"] = dt.Rows[0]["id_sertifikat_pencatatan"].ToString();
                ViewState["id_dokumentasi"] = dt.Rows[0]["id_dokumentasi"].ToString();
                ViewState["id_manual_book"] = dt.Rows[0]["id_manual_book"].ToString();
                ViewState["IsNew"] = false;
            }
            else
            {
                ViewState["IsNew"] = true;
                ViewState["id_luaran_hki"] = Guid.NewGuid();
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            var isNew = bool.Parse(ViewState["IsNew"].ToString());

            List<string> emptyField = new List<string>();
            if (tbJudul.Text.Trim().Length == 0) emptyField.Add("Judul");
            //if (tbNoHKI.Text.Trim().Length == 0) emptyField.Add("Nomor HKI");
            if (tbNoPendaftaran.Text.Trim().Length == 0) emptyField.Add("Nomor Pendaftaran");
            if (tbPemegangHKI.Text.Trim().Length == 0) emptyField.Add("Pemegang HKI");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }


            string dirFile = "~/fileUpload/HKI";
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            dirFile = "~/fileUpload/HKI/" + ViewState["thn_pelaksanaan"].ToString();
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            // Jika baru, get id dr database jika update
            Guid idSertifikatPencatatan = Guid.NewGuid();
            if (!isNew)
            {
                idSertifikatPencatatan = Guid.Parse(ViewState["id_sertifikat_pencatatan"].ToString());
            }

            string file2saved = dirFile + "/" + idSertifikatPencatatan.ToString() + ".pdf";
            // unggah 
            if (cekFile(ref fileUploadSertifikat))
            {
                unggahDokumen(ref fileUploadSertifikat, file2saved);
            }
            //else
            //{
            //    return;
            //}

            // Jika baru, get id dr database jika update
            Guid idDokumentasi = Guid.NewGuid();
            if (!isNew)
            {
                idDokumentasi = Guid.Parse(ViewState["id_dokumentasi"].ToString());
            }

            file2saved = dirFile + "/" + idDokumentasi.ToString() + ".pdf";
            if (cekFile2(ref fileUploadDok))
            {
                unggahDokumen(ref fileUploadDok, file2saved);
            }
            else
            {
                return;
            }

            // Jika baru, get id dr database jika update
            //Guid idManualBook = Guid.NewGuid();
            //if (!isNew)
            //{
            //    idManualBook = Guid.Parse(ViewState["id_manual_book"].ToString());
            //}

            //file2saved = dirFile + "/" + idManualBook.ToString() + ".pdf";
            //if (cekFile(ref fileUploadManualBook))
            //{
            //    unggahDokumen(ref fileUploadManualBook, file2saved);
            //}
            //else
            //{
            //    return;
            //}
            Guid idManualBook = Guid.Parse("00000000-0000-0000-0000-000000000000");
            if (!isNew)
            {
                idSertifikatPencatatan = Guid.Parse(ViewState["id_manual_book"].ToString());
            }
            if (modelPengusul.insupLuaranHKI(
                        Guid.Parse(ViewState["id_luaran_hki"].ToString()),
                        Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()),
                        ViewState["id_jenis_luaran"].ToString(),
                        tbJudul.Text,
                        tbNoHKI.Text,
                        tbNoPendaftaran.Text,
                        tbPemegangHKI.Text,
                        idSertifikatPencatatan,
                        idDokumentasi,
                        idManualBook
                        ))
            {
                //if (bool.Parse(ViewState["isUnggah"].ToString()))
                //{
                modelPengusul.updateStatusUnggahHKI(Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()));
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
                        //noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File harus berekstensi: " + extFile);
                    }
                }
                else
                {
                    //noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Ukuran file maksimal: " + (maxSize / 1024).ToString() + " KByte");

                }
            }
            else
            {
                //noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File belum dipilih");
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

        private bool cekFile2(ref FileUpload fileUpload1)
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
    }
}