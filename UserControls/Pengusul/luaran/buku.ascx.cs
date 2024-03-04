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
    public partial class buku : System.Web.UI.UserControl
    {

        uiDropdownList ddlHelper = new uiDropdownList();
        uiNotify noty = new uiNotify();
        manipulasiData objManipData = new manipulasiData();

        login objLogin;
        Models.Pengusul.luaran modelPengusul = new Models.Pengusul.luaran();
        //berandaPengusul modelPengusul = new berandaPengusul();

        const string PATH_UNGGAH_BERKAS = "~/fileUpload/Buku/";
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
            tbISBN.Text = string.Empty;
            tbJmlHalaman.Text = string.Empty;
            tbPenerbit.Text = string.Empty;
            tbURL.Text = string.Empty;
        }

        public void isiLuaranBuku(Guid idLuaranDijanjikan, string idJenisLuaran, string thnPelaksanaan)
        {
            var dt = new DataTable();
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            ViewState["id_luaran_dijanjikan"] = idLuaranDijanjikan;
            ViewState["id_jenis_luaran"] = idJenisLuaran;
            modelPengusul.getLuaranBuku(ref dt, idLuaranDijanjikan);
            if (dt.Rows.Count > 0)
            {
                tbJudul.Text = dt.Rows[0]["judul"].ToString();
                tbISBN.Text = dt.Rows[0]["isbn"].ToString();
                tbJmlHalaman.Text = dt.Rows[0]["jml_halaman"].ToString();
                tbURL.Text = dt.Rows[0]["url"].ToString();
                tbPenerbit.Text = dt.Rows[0]["penerbit"].ToString();

                ViewState["id_luaran_buku"] = dt.Rows[0]["id_luaran_buku"].ToString();
                ViewState["id_surat_ket"] = dt.Rows[0]["id_surat_keterangan"].ToString();
                ViewState["id_buku"] = dt.Rows[0]["id_buku"].ToString();
                ViewState["IsNew"] = false;
            }
            else
            {
                ViewState["IsNew"] = true;
                ViewState["id_luaran_buku"] = Guid.NewGuid();
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            var isNew = bool.Parse(ViewState["IsNew"].ToString());

            List<string> emptyField = new List<string>();
            if (tbJudul.Text.Trim().Length == 0) emptyField.Add("Judul");
            if (tbISBN.Text.Trim().Length == 0) emptyField.Add("ISBN");
            if (tbJmlHalaman.Text.Trim().Length == 0) emptyField.Add("Jumlah halaman");
            if (tbPenerbit.Text.Trim().Length == 0) emptyField.Add("Penerbit");
            //if (tbURL.Text.Trim().Length == 0) emptyField.Add("url");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }


            string dirFile = "~/fileUpload/Buku";
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            dirFile = "~/fileUpload/Buku/" + ViewState["thn_pelaksanaan"].ToString();
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            // Jika baru, get id dr database jika update
            Guid idSuratKet = Guid.NewGuid();
            if (!isNew)
            {
                idSuratKet = Guid.Parse(ViewState["id_surat_ket"].ToString());
            }

            string file2saved = dirFile + "/" + idSuratKet.ToString() + ".pdf";
            // unggah 
            if (cekFile(ref fileUploadSUratKet))
            {
                unggahDokumen(ref fileUploadSUratKet, file2saved);
            }
            else
            {
                return;
            }

            // Jika baru, get id dr database jika update
            Guid idBuku = Guid.NewGuid();
            if (!isNew)
            {
                idBuku = Guid.Parse(ViewState["id_buku"].ToString());
            }

            file2saved = dirFile + "/" + idBuku.ToString() + ".pdf";
            if (cekFile(ref fileUploadBuku))
            {
                unggahDokumen(ref fileUploadBuku, file2saved);
            }
            else
            {
                return;
            }

            if (modelPengusul.insupLuaranBuku(
                        Guid.Parse(ViewState["id_luaran_buku"].ToString()),
                        Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()),
                        ViewState["id_jenis_luaran"].ToString(),
                        tbJudul.Text,
                        tbISBN.Text,
                        tbJmlHalaman.Text,
                        tbPenerbit.Text,
                        tbURL.Text,
                        idSuratKet,
                        idBuku
                        ))
            {
                //if (bool.Parse(ViewState["isUnggah"].ToString()))
                //{
                modelPengusul.updateStatusUnggahBuku(Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()));
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
            int maxSize = 10 * 1000 * 1000; // 5 MB
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