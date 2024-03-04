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
    public partial class publikasiJurnal : System.Web.UI.UserControl
    {
        uiDropdownList ddlHelper = new uiDropdownList();
        uiNotify noty = new uiNotify();
        manipulasiData objManipData = new manipulasiData();

        login objLogin;
        Models.Pengusul.luaran modelPengusul = new Models.Pengusul.luaran();
        //berandaPengusul modelPengusul = new berandaPengusul();

        const string PATH_UNGGAH_BERKAS = "~/fileUpload/Publikasi/";
        public event EventHandler OnChildEventKembaliOccurs;

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiDdlThnPublikasi();
            }
        }

        private void resetForm()
        {
            tbJudul.Text = string.Empty;
            tbNamaJurnal.Text = string.Empty;
            tbISSN.Text = string.Empty;
            tbVolume.Text = string.Empty;
            tbNomor.Text = string.Empty;
            tbURL.Text = string.Empty;
            ddlThnPublikasi.Text = string.Empty;
        }

        private void isiDdlThnPublikasi()
        {
            ddlThnPublikasi.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            thnSKg = thnSKg + 1;
            for (int i = thnSKg; i >= 2013; i--)
            {
                ddlThnPublikasi.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlThnPublikasi.SelectedValue = DateTime.Now.Year.ToString();
        }

        public void isiLuaranPublikasJurnal(Guid idLuaranDijanjikan, string idJenisLuaran, string thnPelaksanaan)
        {
            var dt = new DataTable();
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            ViewState["id_luaran_dijanjikan"] = idLuaranDijanjikan;
            ViewState["id_jenis_luaran"] = idJenisLuaran;
            modelPengusul.getLuaranPublikasiJurnal(ref dt, idLuaranDijanjikan);
            if (dt.Rows.Count > 0)
            {
                tbJudul.Text = dt.Rows[0]["judul"].ToString();
                tbNamaJurnal.Text = dt.Rows[0]["nama_jurnal"].ToString();
                tbNomor.Text = dt.Rows[0]["nomor"].ToString();
                tbURL.Text = dt.Rows[0]["url"].ToString();
                tbVolume.Text = dt.Rows[0]["volume"].ToString();
                tbISSN.Text = dt.Rows[0]["issn"].ToString();
                tbHalAwal.Text = dt.Rows[0]["halaman_awal"].ToString();
                tbHalAkhir.Text = dt.Rows[0]["halaman_akhir"].ToString();
               // ddlThnPublikasi.SelectedItem.Text = dt.Rows[0]["thn_publikasi"].ToString();
                tbDOI.Text = dt.Rows[0]["doi"].ToString();
                
                ViewState["id_luaran_publikasi_jurnal"] = dt.Rows[0]["id_luaran_publikasi_jurnal"].ToString();
                ViewState["id_surat_ket"] = dt.Rows[0]["id_surat_keterangan"].ToString();
                ViewState["id_artikel"] = dt.Rows[0]["id_artikel"].ToString();
                ViewState["IsNew"] = false;
            }
            else
            {
                ViewState["IsNew"] = true;
                ViewState["id_luaran_publikasi_jurnal"] = Guid.NewGuid();
            }

            if(idJenisLuaran == "1")
            {
                lblJurnalInternasional.Visible = true;
                lblJurnalNasionalTer.Visible = false;
                lblJurnalNasionalTdkTer.Visible = false;
            }
            else if(idJenisLuaran == "2")
            {
                lblJurnalInternasional.Visible = false;
                lblJurnalNasionalTer.Visible = true;
                lblJurnalNasionalTdkTer.Visible = false;
            }
            else if (idJenisLuaran == "21")
            {
                lblJurnalInternasional.Visible = false;
                lblJurnalNasionalTer.Visible = false;
                lblJurnalNasionalTdkTer.Visible = true;
            }
        }
        
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            var isNew = bool.Parse(ViewState["IsNew"].ToString());
            
            List<string> emptyField = new List<string>();
            if (tbJudul.Text.Trim().Length == 0) emptyField.Add("Judul");
            if (tbNamaJurnal.Text.Trim().Length == 0) emptyField.Add("Nama Jurnal");
            if (tbISSN.Text.Trim().Length == 0) emptyField.Add("ISSN");
            if (tbVolume.Text.Trim().Length == 0) emptyField.Add("Volume");
            if (tbNomor.Text.Trim().Length == 0) emptyField.Add("Nomor");
            if (tbURL.Text.Trim().Length == 0) emptyField.Add("URL");
            if (ddlThnPublikasi.SelectedValue == "-1") emptyField.Add("Tahun Publikasi");
            if (tbHalAwal.Text.Trim().Length == 0) emptyField.Add("Halaman Awal");
            if (tbHalAkhir.Text.Trim().Length == 0) emptyField.Add("Halaman Akhir");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }


            string dirFile = "~/fileUpload/Publikasi";
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }
            dirFile = "~/fileUpload/Publikasi/" + ViewState["thn_pelaksanaan"].ToString();
            if (!Directory.Exists(Server.MapPath(dirFile)))
            {
                Directory.CreateDirectory(Server.MapPath(dirFile));
            }

            // Jika baru, get id dr database jika update
            Guid idSuratKet = Guid.NewGuid();
            if(!isNew)
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
            Guid idArtikel = Guid.NewGuid();
            if (!isNew)
            {
                idArtikel = Guid.Parse(ViewState["id_artikel"].ToString());
            }

            file2saved = dirFile + "/" + idArtikel.ToString() + ".pdf";
            if (cekFile(ref fileUploadArtikel))
            {
                unggahDokumen(ref fileUploadArtikel, file2saved);
            }
            else
            {
                return;
            }

            if (modelPengusul.insupLuaranPublikasiJurnal(
                        Guid.Parse(ViewState["id_luaran_publikasi_jurnal"].ToString()),
                        Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()),
                        ddlThnPublikasi.SelectedValue,
                        ViewState["id_jenis_luaran"].ToString(),
                        tbJudul.Text,
                        tbNamaJurnal.Text,
                        tbVolume.Text,
                        tbNomor.Text,
                        tbURL.Text,
                        idSuratKet,
                        idArtikel,
                        tbISSN.Text,
                        tbHalAwal.Text,
                        tbHalAkhir.Text,   
                        tbDOI.Text
                        ))
            {
                //if (bool.Parse(ViewState["isUnggah"].ToString()))
                //{
                modelPengusul.updateStatusUnggahPublikasiJurnal(Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString()));
                //}                
                if (OnChildEventKembaliOccurs != null)
                    OnChildEventKembaliOccurs(null, null);

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