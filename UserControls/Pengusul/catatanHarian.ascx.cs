using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class catatanHarian : System.Web.UI.UserControl
    {
        const string FOLDER_BERKAS = "~/fileUpload/CatatanHarian/";

        Models.login objLogin;
        Models.Pengusul.catatanHarian objModel = new Models.Pengusul.catatanHarian();

        uiNotify noty = new uiNotify();
        uiModal modal = new uiModal();
        uiListView listView = new uiListView();

        protected void Page_Load(object sender, EventArgs e)
        {
            //objLogin = (Models.login)Session["objLogin"];
            modalHapusCatatan.OnDelete += new EventHandler(KonfirmasiHapusCatatan_OnDelete);
            modalHapusBerkasPlusAnggaran.OnDelete += new EventHandler(KonfirmasiHapusBerkasPlusAnggaran_OnDelete);
            if (!IsPostBack)
            {
                setddlThnPelaksanaan();
                cekModeReview();
                setlvDaftarUsulan();
            }
        }

        #region Daftar Usulan

        private void cekModeReview()
        {
            if (Session["thn_pelaksanaan_rev"] != null)
            {
                ddlThnPelaksanaan.SelectedValue = Session["thn_pelaksanaan_rev"].ToString();
                ddlThnPelaksanaan.Enabled = false;
                lbTambahCatatan.Visible = false;
                lbSimpanPembelanjaan.Visible = false;
                btSimpan.Visible = false;
            }
        }

        private void setddlThnPelaksanaan()
        {
            ddlThnPelaksanaan.Items.Clear();
            for (int i = DateTime.Now.Year + 1; i >= 2020; i--)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void setlvDaftarUsulan()
        {
            string idPersonal = string.Empty;
            if (Session["id_usulan_kegiatan_rev"] != null)
            {
                var dtPersonal = new DataTable();
                objModel.getIdentitasUsulanKegiatan(ref dtPersonal, Guid.Parse(Session["id_usulan_kegiatan_rev"].ToString()));
                idPersonal = dtPersonal.Rows[0]["id_personal"].ToString();
            }
            else
            {
                objLogin = (Models.login)Session["objLogin"];
                idPersonal = objLogin.idPersonal.ToString();
            }

            var dt = new DataTable();

            if (objModel.listusulan(ref dt, idPersonal, ddlThnPelaksanaan.SelectedValue))
            {
                try
                {

                    if (Session["id_usulan_kegiatan_rev"] != null)
                    {
                        DataRow[] dr = dt.Select("id_usulan_kegiatan='" + Session["id_usulan_kegiatan_rev"].ToString() + "'");
                        dt = dr.CopyToDataTable();
                    }

                    lvDaftarUsulan.DataSource = dt;
                    lvDaftarUsulan.DataBind();
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        ex.Message);
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    objModel.errorMessage);
            }

        }

        protected void lvDaftarUsulan_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            //generate tahun
            int thnKegiatan = int.Parse(ddlThnPelaksanaan.SelectedValue.ToString()); ;
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem((thnKegiatan - 1).ToString(), (thnKegiatan - 1).ToString()));
            ddlTahun.Items.Add(new ListItem((thnKegiatan).ToString(), (thnKegiatan).ToString()));

            if (thnKegiatan <= DateTime.Now.Year)
            {
                ddlTahun.Items.Add(new ListItem((thnKegiatan + 1).ToString(), (thnKegiatan + 1).ToString()));
            }
            ddlTahun.SelectedValue = thnKegiatan.ToString();
            //ddlBulan.SelectedValue = DateTime.Now.Month.ToString();

            ViewState["IdUsulanKegiatan"] = lvDaftarUsulan.DataKeys[e.NewEditIndex]["id_usulan_kegiatan"].ToString();
            string kegiatan = lvDaftarUsulan.DataKeys[e.NewEditIndex]["jenis_kegiatan"].ToString();
            string namaskema = lvDaftarUsulan.DataKeys[e.NewEditIndex]["nama_skema"].ToString();
            string judul = lvDaftarUsulan.DataKeys[e.NewEditIndex]["judul"].ToString();
            string durasi = lvDaftarUsulan.DataKeys[e.NewEditIndex]["lama_kegiatan"].ToString();
            string tahunke = lvDaftarUsulan.DataKeys[e.NewEditIndex]["urutan_thn_usulan_kegiatan"].ToString();
            ViewState["JenisKegiatan"] = lvDaftarUsulan.DataKeys[e.NewEditIndex]["jenis_kegiatan"].ToString();

            lblKegiatan.Text = kegiatan + " - " + namaskema;
            lblJudul.Text = judul;
            lblTahunPelaksanaan.Text = ddlThnPelaksanaan.SelectedValue.ToString() + $" (Tahun ke {tahunke} Dari {durasi} Tahun)";

            setlvCatatanHarian();

            mvCatatanHarian.SetActiveView(vDaftarCatatan);
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            setlvDaftarUsulan();
        }

        #endregion

        #region Daftar Catatan
        private void setlvCatatanHarian()
        {
            var dt = new DataTable();
            var idUsulanKegiatan = Guid.Parse(ViewState["IdUsulanKegiatan"].ToString());

            if (objModel.listCatatanHarian(ref dt, idUsulanKegiatan, int.Parse(ddlBulan.SelectedValue),
                    int.Parse(ddlTahun.SelectedValue)))
            {
                try
                {
                    lvCatatanHarian.DataSource = dt;
                    lvCatatanHarian.DataBind();
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        ex.Message);
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    objModel.errorMessage);
            }

            if (objModel.listJmlCatatanHarianPerBln(ref dt, idUsulanKegiatan, ddlTahun.SelectedValue))
            {
                if (dt.Rows.Count > 0)
                {
                    lblJmlCatHarPerBln.Visible = true;
                    lblJmlCatHarPerBln.Text = dt.Rows[0]["bln"].ToString();
                }
                else
                {
                    lblJmlCatHarPerBln.Visible = false;
                }
            }

        }

        public string getNamaHari(DateTime tanggal)
        {
            return tanggal.ToString("dddd dd/MM/yyyy", new System.Globalization.CultureInfo("id-ID"));
        }

        protected void lbKembaliKeDaftar_Click(object sender, EventArgs e)
        {
            setlvDaftarUsulan();
            mvCatatanHarian.SetActiveView(vDaftarSkema);
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            setlvCatatanHarian();
        }

        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            setlvCatatanHarian();
        }

        protected void lbTambahCatatan_Click(object sender, EventArgs e)
        {
            //ViewState["IsNew"] = true;
            ViewState["IdCatatanHarian"] = Guid.NewGuid();

            tbTglKegiatan.Text = new DateTime(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue), 1).ToString("yyyy-MM-dd");
            tbKegiatan.Text = string.Empty;
            tbPersentase.Text = string.Empty;

            setlvBerkas();
            //panelBerkas.Enabled = false;
            tbKeteranganBerkas.Enabled = false;
            fuBerkas.Enabled = false;

            if (ViewState["JenisKegiatan"].ToString() == "Penelitian")
            {
                panelAnggaran.Visible = false;

                //setlvAnggaran();
                //panelAnggaran.Enabled = false;
            }
            else
            {
                panelAnggaran.Visible = true;
                setlvAnggaran();
                panelAnggaran.Enabled = false;
            }

            mvCatatanHarian.SetActiveView(vCatatanHarian);
        }

        protected void lvCatatanHarian_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            //ViewState["IsNew"] = false;
            ViewState["IdCatatanHarian"] = Guid.Parse(lvCatatanHarian.DataKeys[e.NewEditIndex]["id_catatan_harian"].ToString());

            DateTime.TryParse(lvCatatanHarian.DataKeys[e.NewEditIndex]["tgl_pelaksanaan"].ToString(), out DateTime tglKegiatan);
            tbTglKegiatan.Text = tglKegiatan.ToString("yyyy-MM-dd");
            tbKegiatan.Text = lvCatatanHarian.DataKeys[e.NewEditIndex]["kegiatan_yg_dilakukan"].ToString();
            tbPersentase.Text = lvCatatanHarian.DataKeys[e.NewEditIndex]["persentase_capaian"].ToString();

            //panelBerkas.Enabled = true;
            tbKeteranganBerkas.Enabled = true;
            fuBerkas.Enabled = true;
            setlvBerkas();

            if (ViewState["JenisKegiatan"].ToString() == "Penelitian")
            {
                panelAnggaran.Visible = false;

                //panelAnggaran.Enabled = true;
                //setlvAnggaran();
                //ClearDataAnggaran();
            }
            else
            {
                panelAnggaran.Visible = true;
                panelAnggaran.Enabled = true;
                setlvAnggaran();
                ClearDataAnggaran();
            }

            mvCatatanHarian.SetActiveView(vCatatanHarian);
        }

        protected void lvCatatanHarian_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            ViewState["IdCatatanHarian"] = Guid.Parse(lvCatatanHarian.DataKeys[e.ItemIndex]["id_catatan_harian"].ToString());
            var kegiatan = lvCatatanHarian.DataKeys[e.ItemIndex]["kegiatan_yg_dilakukan"].ToString();

            modalHapusCatatan.TitleKonfirmasi = "Konfirmasi";
            modalHapusCatatan.TextKonfirmasi = $"Apakah Anda yakin akan menghapus catatan kegiatan: <br /> <b>'{kegiatan}'</b> ?";
            modalHapusCatatan.Show();
        }

        protected void lvCatatanHarian_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var drv = e.Item.DataItem as DataRowView;
                var jmlBerkas = int.Parse(drv["jml_berkas"].ToString());

                if (Session["thn_pelaksanaan_rev"] != null)
                {
                    var lbEdit = e.Item.FindControl("lbEdit") as LinkButton;
                    var lbHapus = e.Item.FindControl("lbHapus") as LinkButton;
                    lbEdit.Visible = false;
                    lbHapus.Visible = false;
                }

                if (jmlBerkas == 0) return;

                var idBerkas = drv["id_berkas_catatan_harian"].ToString().Split('|');
                var namaBerkas = drv["nama_berkas"].ToString().Split('|');
                var tipeBerkas = drv["tipe_berkas"].ToString().Split('|');

                if (idBerkas.Length == 0 || string.IsNullOrWhiteSpace(idBerkas[0])) return;

                var listBerkas = new List<BerkasCatatanHarian>();
                for (int i = 0; i < idBerkas.Length; i++)
                {
                    listBerkas.Add(new BerkasCatatanHarian()
                    {
                        idBerkas = Guid.Parse(idBerkas[i]),
                        namaBerkas = namaBerkas[i],
                        tipeBerkas = tipeBerkas[i]
                    });
                }

                var lvDaftarBerkas = e.Item.FindControl("lvDaftarBerkas") as ListView;
                lvDaftarBerkas.DataSource = listBerkas;
                lvDaftarBerkas.DataBind();

            }
        }

        protected void lbKembaliKeDaftarCatatan_Click(object sender, EventArgs e)
        {
            setlvCatatanHarian();
            mvCatatanHarian.SetActiveView(vDaftarCatatan);
        }

        protected void btSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTglKegiatan.Text) || string.IsNullOrWhiteSpace(tbKegiatan.Text) ||
                    string.IsNullOrWhiteSpace(tbPersentase.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Maaf, Isian Data harus lengkap !<br />Penyimpanan dibatalkan...");
                return;
            }

            if (!int.TryParse(tbPersentase.Text, out _))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Maaf, Persentase dalam bentuk angka");
                tbPersentase.Text = string.Empty;
                return;
            };
            if (!DateTime.TryParse(tbTglKegiatan.Text, out _))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Maaf, Format Tanggal salah");
                tbTglKegiatan.Text = new DateTime(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue), 1).ToString("yyyy-MM-dd");
                return;
            };

            //string IDCatatanHarian = IsNew ? Guid.NewGuid().ToString() : ViewState["id_catatan_harian"].ToString();
            var IdCatatanHarian = ViewState["IdCatatanHarian"].ToString();
            var IdUsulanKegiatan = ViewState["IdUsulanKegiatan"].ToString();
            var tanggal = Convert.ToDateTime(tbTglKegiatan.Text);
            var bulan = int.Parse(tanggal.Month.ToString()) - 1;
            ddlBulan.SelectedIndex = bulan;
            //ViewState["bulan"] = bulan;

            if (!objModel.insertlogbook(IdCatatanHarian, IdUsulanKegiatan, tanggal, tbKegiatan.Text, tbPersentase.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                return;
            }

            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                   "Pengisian berhasil, <br />Lanjutkan ke Unggah Berkas dan Penggunaan Anggaran");

            if (ViewState["JenisKegiatan"].ToString() == "Penelitian")
            {
                //if (!panelBerkas.Enabled) panelBerkas.Enabled = true;
                tbKeteranganBerkas.Enabled = true;
                fuBerkas.Enabled = true;

                //if (!panelAnggaran.Enabled) panelAnggaran.Enabled = true;
            }
            else
            {
                if (!panelAnggaran.Enabled) panelAnggaran.Enabled = true;
            }

        }

        protected void btBatal_Click(object sender, EventArgs e)
        {
            setlvCatatanHarian();
            mvCatatanHarian.SetActiveView(vDaftarCatatan);
        }

        protected void KonfirmasiHapusCatatan_OnDelete(object sender, EventArgs e)
        {
            if (!objModel.hapusCatatanHarian(ViewState["IdCatatanHarian"].ToString()))
            {

                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                                objModel.errorMessage);
                return;
            }

            modalHapusCatatan.Hide();
            setlvCatatanHarian();

            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                   "Data berhasil dihapus...");
        }

        #endregion

        #region Berkas

        private void setlvBerkas()
        {
            var dt = new DataTable();

            if (objModel.listberkas(ref dt, ViewState["IdCatatanHarian"].ToString()))
            {
                try
                {
                    lvBerkas.DataSource = dt;
                    lvBerkas.DataBind();
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        ex.Message);
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    objModel.errorMessage);
            }

        }

        protected void lbUnggah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbKeteranganBerkas.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Maaf, keterangan berkas harus diisi !<br />Penyimpanan dibatalkan...");
                return;
            };

            string[] allowedFileExt = { "pdf", "jpg", "jpeg", "doc", "docx", "xls", "xlsx", "ppt", "pptx" };

            if (!fuBerkas.HasFile)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Pesan", "Kesalahan: File belum dipilih.. ");
                return;
            }

            string fileName = Path.GetFileName(this.fuBerkas.PostedFile.FileName);
            string fileExtention = Path.GetExtension(this.fuBerkas.PostedFile.FileName.ToLower()).Replace(".", "");
            bool isValidFile = false;
            for (int i = 0; i < allowedFileExt.Length; i++)
            {
                if (fileExtention == allowedFileExt[i])
                {
                    isValidFile = true;
                    break;
                }
            }

            if (!isValidFile)
            {
                var errorMessage = "Jenis File '." + fileExtention.ToUpper() + "' tidak diperbolehkan untuk diunggah !";
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }

            int fileSize = fuBerkas.PostedFile.ContentLength;
            if (fileSize > (5 * 1024 * 1024))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Pesan",
                    "Kesalahan: File yang akan diunggah ukurannya tidak boleh melebihi 5 MBytes!!!");
                return;
            }

            var folderUnggah = FOLDER_BERKAS + ddlThnPelaksanaan.SelectedValue;
            try
            {
                if (!Directory.Exists(Server.MapPath(folderUnggah)))
                {
                    Directory.CreateDirectory(Server.MapPath(folderUnggah));
                }
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                return;
            }

            try
            {
                string newID = Guid.NewGuid().ToString();
                string namaFile = folderUnggah + $"/{newID}.{fileExtention}";

                if (fileExtention == "jpeg" || fileExtention == "jpg")
                {
                    var encoder = ImageCodecInfo.GetImageEncoders().First(
                                        c => c.FormatID == ImageFormat.Jpeg.Guid);

                    System.Drawing.Image UploadedImage = System.Drawing.Image.FromStream(fuBerkas.PostedFile.InputStream);
                    if (fileSize > (4 * 1024 * 1024))
                    {
                        var encParams1 = new EncoderParameters()
                        {
                            Param = new[] { new EncoderParameter(Encoder.Quality, 30L) }
                        };
                        UploadedImage.Save(Server.MapPath(namaFile), encoder, encParams1);
                    }
                    else if (fileSize > (1 * 1024 * 1024))
                    {
                        var encParams2 = new EncoderParameters()
                        {
                            Param = new[] { new EncoderParameter(Encoder.Quality, 50L) }
                        };
                        UploadedImage.Save(Server.MapPath(namaFile), encoder, encParams2);
                    }
                    else
                    {
                        var encParams = new EncoderParameters()
                        {
                            Param = new[] { new EncoderParameter(Encoder.Quality, 100L) }
                        };
                        UploadedImage.Save(Server.MapPath(namaFile), encoder, encParams);
                    }
                }
                else
                {
                    fuBerkas.PostedFile.SaveAs(HttpContext.Current.Request.MapPath(namaFile));
                }

                if (objModel.uploadberkas(newID, ViewState["IdCatatanHarian"].ToString(),
                        tbKeteranganBerkas.Text, fileExtention, fileSize.ToString()))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah berkas berhasil...");

                    tbKeteranganBerkas.Text = string.Empty;
                    setlvBerkas();
                }
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }

        }

        protected void lvBerkas_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var idBerkas = lvBerkas.DataKeys[e.ItemIndex]["id_berkas_catatan_harian"].ToString();
            var namaBerkas = lvBerkas.DataKeys[e.ItemIndex]["nama_berkas"].ToString();
            var tipeBerkas = lvBerkas.DataKeys[e.ItemIndex]["tipe_berkas"].ToString();

            ViewState["IdBerkasCatatanHarian"] = idBerkas;
            ViewState["NamaBerkas"] = idBerkas + '.' + tipeBerkas;
            ViewState["ItemHapus"] = "BERKAS";

            modalHapusBerkasPlusAnggaran.TitleKonfirmasi = "Konfirmasi";
            modalHapusBerkasPlusAnggaran.TextKonfirmasi = $"Apakah Anda yakin akan menghapus berkas <b>'{namaBerkas}'</b> ?";
            modalHapusBerkasPlusAnggaran.Show();

        }

        protected void lvBerkas_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            var idBerkas = lvBerkas.DataKeys[e.NewEditIndex]["id_berkas_catatan_harian"].ToString();
            var namaBerkas = lvBerkas.DataKeys[e.NewEditIndex]["nama_berkas"].ToString();
            var tipeBerkas = lvBerkas.DataKeys[e.NewEditIndex]["tipe_berkas"].ToString();

            var filePath = Path.Combine(Server.MapPath(FOLDER_BERKAS + ddlThnPelaksanaan.SelectedValue),
                                $"{idBerkas}.{tipeBerkas}");

            if (!File.Exists(filePath))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan !. Silahkan Hubungi Administrator");
                return;
            }

            Session["AtributUnduh"] = new Helper.AtributUnduh()
            {
                FolderUnduh = FOLDER_BERKAS + ddlThnPelaksanaan.SelectedValue,
                NamaBerkas = $"{idBerkas}.{tipeBerkas}",
                NamaBerkasdiUnduh = $"{namaBerkas}.{tipeBerkas}"
            };


            var responseTo = "Helper/unduhFile.aspx";
            Response.Redirect(responseTo);

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Download", $"window.location.assign('{responseTo}')", true);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Download", "window.location.assign(\"" + responseTo + "\")", true);
        }

        protected void KonfirmasiHapusBerkasPlusAnggaran_OnDelete(object sender, EventArgs e)
        {
            if (ViewState["ItemHapus"].ToString() == "BERKAS")
            {
                var idBerkasCatatanHarian = ViewState["IdBerkasCatatanHarian"].ToString();
                var namaBerkas = ViewState["NamaBerkas"].ToString();

                if (!objModel.hapusBerkas(idBerkasCatatanHarian))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                    return;
                }

                var fileUploadPath = FOLDER_BERKAS + ddlThnPelaksanaan.SelectedValue + $"/{namaBerkas}";
                var filePath = Server.MapPath(fileUploadPath);

                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                catch (IOException ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                };

                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Berkas berhasil dihapus...");

                ViewState.Remove("IdBerkasCatatanHarian");
                ViewState.Remove("NamaBerkas");
                setlvBerkas();
            }
            else
            {
                var idPengeluaranPenugasan = ViewState["IdPengeluaranPenugasan"].ToString();

                if (!objModel.hapusPengeluaran(idPengeluaranPenugasan))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                    return;
                }

                ViewState.Remove("IdPengeluaranPenugasan");
                setlvAnggaran();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Pembelanjaan berhasil dihapus...");

            }

            ViewState.Remove("ItemHapus");
            modalHapusBerkasPlusAnggaran.Hide();
        }

        protected void lvDaftarBerkas_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            var lvDaftarBerkas = sender as ListView;
            var idBerkas = lvDaftarBerkas.DataKeys[e.NewEditIndex]["idBerkas"].ToString();
            var namaBerkas = lvDaftarBerkas.DataKeys[e.NewEditIndex]["namaBerkas"].ToString();
            var tipeBerkas = lvDaftarBerkas.DataKeys[e.NewEditIndex]["tipeBerkas"].ToString();
            var filePath = Path.Combine(Server.MapPath(FOLDER_BERKAS + ddlThnPelaksanaan.SelectedValue),
                                $"{idBerkas}.{tipeBerkas}");

            if (!File.Exists(filePath))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan !. Silahkan Hubungi Administrator");
                return;
            }

            Session["AtributUnduh"] = new Helper.AtributUnduh()
            {
                FolderUnduh = FOLDER_BERKAS + ddlThnPelaksanaan.SelectedValue,
                NamaBerkas = $"{idBerkas}.{tipeBerkas}",
                NamaBerkasdiUnduh = $"{namaBerkas}.{tipeBerkas}"
            };

            var responseTo = "Helper/unduhFile.aspx";
            Response.Redirect(responseTo);
        }

        #endregion

        #region Pembelanjaan

        private void setlvAnggaran()
        {
            var dt = new DataTable();

            if (objModel.listpengeluaran(ref dt, ViewState["IdCatatanHarian"].ToString()))
            {
                try
                {
                    lvAnggaran.DataSource = dt;
                    lvAnggaran.DataBind();
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        ex.Message);
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    objModel.errorMessage);
            }

        }

        private void ClearDataAnggaran()
        {
            ddlJenis.SelectedValue = "-1";
            tbTglPengeluaran.Text = new DateTime(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue), 1).ToString("yyyy-MM-dd");
            tbNoBukti.Text = string.Empty;
            tbPembelanjaan.Text = string.Empty;
            tbTotalPengeluaran.Text = string.Empty;

            ViewState.Remove("IdPengeluaranPenugasan");
        }

        protected void lvAnggaran_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ViewState["IdPengeluaranPenugasan"] = lvAnggaran.DataKeys[e.NewEditIndex]["id_pengeluaran_penugasan"].ToString();
            DateTime.TryParse(lvAnggaran.DataKeys[e.NewEditIndex]["tgl_pembelanjaan"].ToString(), out DateTime tglPengeluaran);
            decimal.TryParse(lvAnggaran.DataKeys[e.NewEditIndex]["jml_pembelanjaan"].ToString(), out decimal jmlPembelanjaan);

            tbTglPengeluaran.Text = tglPengeluaran.ToString("yyyy-MM-dd");
            tbPembelanjaan.Text = lvAnggaran.DataKeys[e.NewEditIndex]["nama_pembelanjaan"].ToString();
            tbNoBukti.Text = lvAnggaran.DataKeys[e.NewEditIndex]["no_bukti_pengeluaran"].ToString();
            tbTotalPengeluaran.Text = jmlPembelanjaan.ToString("N0").Replace(".", "").Replace(",", "");
            ddlJenis.SelectedValue = lvAnggaran.DataKeys[e.NewEditIndex]["kd_jenis_pembelanjaan"].ToString();
        }

        protected void lvAnggaran_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            ViewState["ItemHapus"] = "PEMBELANJAAN";
            ViewState["IdPengeluaranPenugasan"] = lvAnggaran.DataKeys[e.ItemIndex]["id_pengeluaran_penugasan"].ToString();
            var pembelanjaan = lvAnggaran.DataKeys[e.ItemIndex]["nama_pembelanjaan"].ToString();

            modalHapusBerkasPlusAnggaran.TitleKonfirmasi = "Konfirmasi";
            modalHapusBerkasPlusAnggaran.TextKonfirmasi = $"Apakah Anda yakin akan menghapus data pembelanjaan : <br><b>'{pembelanjaan}'</b> ?";
            modalHapusBerkasPlusAnggaran.Show();
        }

        protected void lbSimpanPembelanjaan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTglPengeluaran.Text) || string.IsNullOrWhiteSpace(tbPembelanjaan.Text) ||
                string.IsNullOrWhiteSpace(tbTotalPengeluaran.Text) || string.IsNullOrWhiteSpace(tbNoBukti.Text) ||
                ddlJenis.SelectedValue == "-1")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Maaf, Isian Data harus lengkap !<br />Penyimpanan dibatalkan...");
                return;
            }
            else
            {
                decimal.TryParse(tbTotalPengeluaran.Text, out decimal pengeluaran);
                if (pengeluaran == default(decimal))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Maaf, pengeluaran dalam bentuk angka tanpa titik dan koma");
                    return;
                };


                DateTime.TryParse(tbTglPengeluaran.Text, out DateTime tglPengeluaran);
                if (tglPengeluaran == default(DateTime))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Maaf, Format isian Tanggal salah");
                    return;
                };

                var IdCatatanHarian = ViewState["IdCatatanHarian"].ToString();
                string IdPengeluaranPenugasan;

                if (ViewState["IdPengeluaranPenugasan"] == null)
                    IdPengeluaranPenugasan = Guid.NewGuid().ToString();
                else
                    IdPengeluaranPenugasan = ViewState["IdPengeluaranPenugasan"].ToString();

                if (!objModel.insertpenggunaananggaran(IdPengeluaranPenugasan, IdCatatanHarian,
                        ddlJenis.SelectedValue, tglPengeluaran, tbPembelanjaan.Text, tbTotalPengeluaran.Text, tbNoBukti.Text))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModel.errorMessage);
                }

                setlvAnggaran();
                ClearDataAnggaran();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Pengisian Penggunaan Anggaran berhasil");

            }
        }

        #endregion

    }

    public class BerkasCatatanHarian
    {
        public Guid idBerkas { get; set; }
        public string namaBerkas { get; set; }
        public string tipeBerkas { get; set; }
    }



}
