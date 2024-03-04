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

namespace simlitekkes.UserControls.Pengusul
{
    public partial class artikelJurnal : System.Web.UI.UserControl
    {
        uiDropdownList ddlHelper = new uiDropdownList();
        uiNotify noty = new uiNotify();
        manipulasiData objManipData = new manipulasiData();

        login objLogin;
        berandaPengusul modelPengusul = new berandaPengusul();

        const string PATH_UNGGAH_BERKAS = "~/fileUpload/fileArtikelJurnal/";

        public Guid idPublikasi
        {
            get
            {
                if (ViewState["IdPublikasi"] == null) ViewState["IdPublikasi"] = default(Guid);
                return Guid.Parse(ViewState["IdPublikasi"].ToString());
            }
            set { ViewState["IdPublikasi"] = value; }
        }

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
            tbNamaJurnal.Text = string.Empty;
            tbISSN.Text = string.Empty;
            tbVolume.Text = string.Empty;
            tbNomor.Text = string.Empty;
            tbURL.Text = string.Empty;
            tbTahunPublikasi.Text = string.Empty;

            ddlJenisPublikasi.SelectedIndex = 0;
            ddlPeranPenulis.SelectedIndex = 0;
        }

        private void isiJenisPublikasiJurnal()
        {
            if (ddlJenisPublikasi.Items.Count > 0) return;

            var dt = new DataTable();
            if (modelPengusul.getJenisPublikasiJurnal(ref dt))
            {
                ddlJenisPublikasi.AppendDataBoundItems = true;
                ddlJenisPublikasi.Items.Add(new ListItem("Pilih Jenis Publikasi Jurnal", "-1"));

                ddlJenisPublikasi.DataValueField = "kd_jenis_publikasi_jurnal";
                ddlJenisPublikasi.DataTextField = "jenis_publikasi_jurnal";
                ddlJenisPublikasi.DataSource = dt;
                ddlJenisPublikasi.DataBind();

                ddlJenisPublikasi.SelectedIndex = 0;
            }
        }

        private void isiPeranPenulis()
        {
            if (ddlPeranPenulis.Items.Count > 0) return;

            var dt = new DataTable();
            if (modelPengusul.getPeranPenulisPublikasi(ref dt))
            {
                ddlPeranPenulis.AppendDataBoundItems = true;
                ddlPeranPenulis.Items.Add(new ListItem("Pilih Peran Penulis", "0"));

                ddlPeranPenulis.DataValueField = "kd_peran_penulis";
                ddlPeranPenulis.DataTextField = "peran_penulis";
                ddlPeranPenulis.DataSource = dt;
                ddlPeranPenulis.DataBind();

                ddlPeranPenulis.SelectedIndex = 0;
            }
        }

        private void isiPeranPenulisKinerja()
        {
            if (ddlPeranPenulisKinerja.Items.Count > 0) return;

            var dt = new DataTable();
            if (modelPengusul.getPeranPenulisPublikasi(ref dt))
            {
                ddlPeranPenulisKinerja.AppendDataBoundItems = true;
                ddlPeranPenulisKinerja.Items.Add(new ListItem("Pilih Peran Penulis", "0"));

                ddlPeranPenulisKinerja.DataValueField = "kd_peran_penulis";
                ddlPeranPenulisKinerja.DataTextField = "peran_penulis";
                ddlPeranPenulisKinerja.DataSource = dt;
                ddlPeranPenulisKinerja.DataBind();

                ddlPeranPenulisKinerja.SelectedIndex = 0;
            }
        }

        public void isiArtikelJurnal()
        {
            var dt = new DataTable();

            if (modelPengusul.getArtikelJurnal(ref dt, Guid.Parse(objLogin.idPersonal)))
            {
                lvArtikelJurnal.DataSource = dt;
                lvArtikelJurnal.DataBind();
            }
        }

        public void tambahDataRekamJejak()
        {
            ViewState["IsNew"] = true;
            isiJenisPublikasiJurnal();
            isiPeranPenulis();
            resetForm();

            idPublikasi = Guid.NewGuid();
            mvArtikelJurnal.SetActiveView(vData);
        }

        public string ShortenedURL(string url)
        {
            if (url.Length > 7 && url.ToLower().Substring(0, 4) != "http") url = "http://" + url;

            if (url.Length > 200)
                return url.Substring(0, 200) + "...";
            else
                return url;
        }

        protected void lbDataBaru_Click(object sender, EventArgs e)
        {
            tambahDataRekamJejak();
        }

        protected void lvArtikelJurnal_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ViewState["IsNew"] = false;

            idPublikasi = Guid.Parse(lvArtikelJurnal.DataKeys[e.NewEditIndex]["id_publikasi_jurnal"].ToString());
            ViewState["idPublikasiJurnal"] = idPublikasi;
            string kdSumberData = lvArtikelJurnal.DataKeys[e.NewEditIndex]["kd_sumber_data"].ToString();

            if (kdSumberData == "2") //Isian SIMLITABMAS
            {
                isiJenisPublikasiJurnal();
                isiPeranPenulis();
                resetForm();

                var dt = new DataTable();
                if (modelPengusul.getDataArtikelJurnal(ref dt, idPublikasi))
                {
                    if (dt.Rows.Count > 0)
                    {
                        tbJudul.Text = dt.Rows[0]["judul"].ToString();
                        tbTahunPublikasi.Text = dt.Rows[0]["thn_publikasi"].ToString();
                        tbNamaJurnal.Text = dt.Rows[0]["nama_jurnal"].ToString();
                        tbVolume.Text = dt.Rows[0]["volume"].ToString();
                        tbNomor.Text = dt.Rows[0]["nomor"].ToString();
                        tbURL.Text = dt.Rows[0]["url"].ToString();
                        tbISSN.Text = dt.Rows[0]["issn"].ToString();
                        ddlJenisPublikasi.SelectedValue = dt.Rows[0]["kd_jenis_publikasi_jurnal"].ToString();
                        ddlPeranPenulis.SelectedValue = dt.Rows[0]["kd_peran_penulis"].ToString();

                        mvArtikelJurnal.SetActiveView(vData);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPengusul.errorMessage);
                }
            }
            else
            {
                isiPeranPenulisKinerja();
                var dtKinerja = new DataTable();
                if (modelPengusul.getArtikelJurnalKinerja(ref dtKinerja, Guid.Parse(objLogin.idPersonal), idPublikasi))
                {
                    if (dtKinerja.Rows.Count > 0)
                    {
                        lblJudul.Text = dtKinerja.Rows[0]["judul"].ToString();
                        lblThnPublikasi.Text = dtKinerja.Rows[0]["thn_publikasi"].ToString();
                        lblNamaJurnal.Text = dtKinerja.Rows[0]["nama_jurnal"].ToString();
                        lblJenisPublikasi.Text = dtKinerja.Rows[0]["jenis_publikasi_jurnal"].ToString();
                        lblVolume.Text = dtKinerja.Rows[0]["volume"].ToString();
                        lblNomor.Text = dtKinerja.Rows[0]["nomor"].ToString();
                        tbUrlKinerja.Text = dtKinerja.Rows[0]["url"].ToString();
                        lblIssn.Text = dtKinerja.Rows[0]["issn"].ToString();
                        ddlPeranPenulisKinerja.SelectedValue = dtKinerja.Rows[0]["kd_peran_penulis"].ToString();

                        mvArtikelJurnal.SetActiveView(vDataKinerja);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPengusul.errorMessage);
                }
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
            if (tbTahunPublikasi.Text.Trim().Length == 0) emptyField.Add("Tahun Publikasi");
            if (ddlJenisPublikasi.SelectedValue == "-1") emptyField.Add("Jenis Publikasi");
            if (ddlPeranPenulis.SelectedValue == "0") emptyField.Add("Peran Penulis");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }

            if (modelPengusul.insupArtikelJurnal(
                        Guid.Parse(objLogin.idPersonal),
                        tbTahunPublikasi.Text,
                        ddlJenisPublikasi.SelectedValue,
                        tbJudul.Text,
                        tbNamaJurnal.Text,
                        tbVolume.Text,
                        tbNomor.Text,
                        tbURL.Text,
                        tbISSN.Text,
                        ddlPeranPenulis.SelectedValue,
                        idPublikasi
                        ))
            {
                if (ViewState["isUnggah"] != null && bool.Parse(ViewState["isUnggah"].ToString()))
                {
                    modelPengusul.updateStatusUnggahArtikelJurnal(idPublikasi);
                }

                isiArtikelJurnal();
                mvArtikelJurnal.SetActiveView(vDaftar);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil disimpan...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan ", modelPengusul.errorMessage);
            }
        }

        protected void btnBatal_Click(object sender, EventArgs e)
        {
            mvArtikelJurnal.SetActiveView(vDaftar);
        }

        protected void lvArtikelJurnal_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            idPublikasi = Guid.Parse(lvArtikelJurnal.DataKeys[e.ItemIndex]["id_publikasi_jurnal"].ToString());
            lblJudulDihapus.Text = lvArtikelJurnal.DataKeys[e.ItemIndex]["judul"].ToString();

            new uiModal().ShowModal(this.Page, "modalKonfirmasiHapus");
        }

        protected void btnKonfirmasiHapus_Click(object sender, EventArgs e)
        {
            if (modelPengusul.delArtikelJurnal(idPublikasi))
            {
                isiArtikelJurnal();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil dihapus...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPengusul.errorMessage);
            }
            new uiModal().HideModal(this.Page, "modalKonfirmasiHapus");

        }

        protected void lvArtikelJurnal_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "UnduhBerkas")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                string kdStatusBerkasJurnal = lvArtikelJurnal.DataKeys[rowIndex]["kd_sts_berkas_jurnal"].ToString();

                if (kdStatusBerkasJurnal == "1")
                {
                    idPublikasi = Guid.Parse(lvArtikelJurnal.DataKeys[rowIndex]["id_publikasi_jurnal"].ToString());
                    string judul = lvArtikelJurnal.DataKeys[rowIndex]["judul"].ToString();

                    string namaFile = (judul.Length > 30) ? judul.Substring(0, 30) : judul;
                    namaFile = namaFile.Replace(" ", "_");
                    namaFile = objManipData.removeUnicode(namaFile);

                    string namaBerkas = "publikasi_" + namaFile + ".pdf";

                    string filePath = string.Format(PATH_UNGGAH_BERKAS + idPublikasi + ".pdf");

                    if (File.Exists(Server.MapPath(filePath)))
                    {
                        var atributUnduh = new AtributUnduh
                        {
                            FolderUnduh = PATH_UNGGAH_BERKAS,
                            NamaBerkas = idPublikasi + ".pdf",
                            NamaBerkasdiUnduh = namaBerkas
                        };
                        Session["AtributUnduh"] = atributUnduh;

                        var unduhForm = "~/Helper/unduhFile.aspx";
                       Response.Redirect(unduhForm);
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                            "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
                        return;
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Berkas pendukung belum diunggah");
                }
            }
            else if (e.CommandName.Equals("KunjungiArtikel"))
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                Response.Redirect(ShortenedURL(lvArtikelJurnal.DataKeys[rowIndex]["url"].ToString()));
            }
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            var ktUnggah = new kontrolUnggah
            {
                path2save = PATH_UNGGAH_BERKAS + idPublikasi.ToString() + ".pdf",
                max_size = 1024 * 1024, // 500KB
                alllowed_ext = "pdf;PDF",
                success_info = "Unggah Artikel Jurnal berhasil, silahkan klik tombol selesai",
                failed_info = "Unggah Artikel Jurnal gagal !"
            };

            Session.Add("ktUnggah", ktUnggah);

            new uiModal().ShowModal(this.Page, "modalUnggahBerkas");
        }

        protected void lbSelesaiUnggah_Click(object sender, EventArgs e)
        {
            var ktUnggah = (kontrolUnggah)Session["ktUnggah"];
            if (ktUnggah.isSuccess)
            {
                ViewState["isUnggah"] = ktUnggah.isSuccess;
            }

            new uiModal().HideModal(this.Page, "modalUnggahBerkas");
        }

        protected void lvArtikelJurnal_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                LinkButton lbUnduh = (LinkButton)e.Item.FindControl("lbUnduh");
                String kd_sts_berkas_jurnal = lvArtikelJurnal.DataKeys[e.Item.DataItemIndex]["kd_sts_berkas_jurnal"].ToString();
                if (kd_sts_berkas_jurnal != "1")
                {
                    lbUnduh.ForeColor = System.Drawing.Color.Gray;
                }
                else
                {
                    lbUnduh.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnSimpanKinerja_Click(object sender, EventArgs e)
        {
            Guid idPublikasiJurnal = Guid.Parse(ViewState["idPublikasiJurnal"].ToString());
            Guid idPersonal = Guid.Parse(objLogin.idPersonal.ToString());
            string kdPeranPenulis = ddlPeranPenulisKinerja.SelectedValue;
            string url = tbUrlKinerja.Text;

            if (kdPeranPenulis == "0" || kdPeranPenulis == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan ", "Peran penulis belum dipilih");
            }
            else
            {
                if (modelPengusul.updatePeranPenulis(idPublikasiJurnal, idPersonal, kdPeranPenulis))
                {
                    isiArtikelJurnal();
                    mvArtikelJurnal.SetActiveView(vDaftar);
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil disimpan...");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan ", modelPengusul.errorMessage);
                }
            }

            if (tbUrlKinerja.Text.Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan ", "URL belum diisi");
            }
            else
            {
                if (modelPengusul.updateUrl(idPublikasiJurnal, url))
                {
                    isiArtikelJurnal();
                    mvArtikelJurnal.SetActiveView(vDaftar);
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil disimpan...");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan ", modelPengusul.errorMessage);
                }
            }
        }

        protected void btnBatalKinerja_Click(object sender, EventArgs e)
        {
            mvArtikelJurnal.SetActiveView(vDaftar);
        }
    }
}