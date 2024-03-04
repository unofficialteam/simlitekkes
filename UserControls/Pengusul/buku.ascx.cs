using simlitekkes.Core;
using simlitekkes.Helper;
using simlitekkes.Models;
using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class buku : System.Web.UI.UserControl
    {
        uiDropdownList ddlHelper = new uiDropdownList();
        uiNotify noty = new uiNotify();
        manipulasiData objManipData = new manipulasiData();

        login objLogin;
        berandaPengusul modelPengusul = new berandaPengusul();

        const string PATH_UNGGAH_BERKAS = "~/fileUpload/fileBuku/";

        public Guid idBuku
        {
            get
            {
                if (ViewState["idBuku"] == null) ViewState["idBuku"] = default(Guid);
                return Guid.Parse(ViewState["idBuku"].ToString());
            }
            set { ViewState["idBuku"] = value; }
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
            tbNamaPenerbit.Text = string.Empty;
            tbISBN.Text = string.Empty;
            tbJumlahHalaman.Text = string.Empty;
            tbURL.Text = string.Empty;
            tbTahunTerbit.Text = string.Empty;

        }

        public void isiBuku()
        {
            var dt = new DataTable();

            if (modelPengusul.getBuku(ref dt, Guid.Parse(objLogin.idPersonal)))
            {
                lvBuku.DataSource = dt;
                lvBuku.DataBind();
            }
        }

        public void tambahDataRekamJejak()
        {
            ViewState["isUnggah"] = false;
            ViewState["IsNew"] = true;
            resetForm();
            mvBuku.SetActiveView(vData);
        }

        protected void lbDataBaru_Click(object sender, EventArgs e)
        {
            ViewState["isUnggah"] = false;
            ViewState["IsNew"] = true;
            resetForm();
            idBuku = Guid.NewGuid();
            mvBuku.SetActiveView(vData);
        }

        protected void lvBuku_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ViewState["isUnggah"] = false;
            ViewState["IsNew"] = false;
            resetForm();

            var idBuku = Guid.Parse(lvBuku.DataKeys[e.NewEditIndex]["id_buku"].ToString());
            var dt = new DataTable();
            if (modelPengusul.getDataBuku(ref dt, idBuku))
            {
                if (dt.Rows.Count > 0)
                {
                    tbJudul.Text = dt.Rows[0]["judul"].ToString();
                    tbTahunTerbit.Text = dt.Rows[0]["thn_penerbitan"].ToString();
                    tbNamaPenerbit.Text = dt.Rows[0]["penerbit"].ToString();
                    tbJumlahHalaman.Text = dt.Rows[0]["jml_halaman"].ToString();
                    tbURL.Text = dt.Rows[0]["url"].ToString();
                    tbISBN.Text = dt.Rows[0]["isbn"].ToString();

                    ViewState["IdBuku"] = idBuku;
                    mvBuku.SetActiveView(vData);
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPengusul.errorMessage);
            }

        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            var isNew = bool.Parse(ViewState["IsNew"].ToString());

            List<string> emptyField = new List<string>();
            if (tbJudul.Text.Trim().Length == 0) emptyField.Add("Judul");
            if (tbTahunTerbit.Text.Trim().Length == 0) emptyField.Add("Tahun Terbit");
            if (tbISBN.Text.Trim().Length == 0) emptyField.Add("ISBN");
            if (tbJumlahHalaman.Text.Trim().Length == 0) emptyField.Add("Jumlah Halaman");
            if (tbNamaPenerbit.Text.Trim().Length == 0) emptyField.Add("Nama Penerbit");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }

            //var idBuku = (bool.Parse(ViewState["IsNew"].ToString())) ?

            // Guid.NewGuid() : Guid.Parse(ViewState["IdBuku"].ToString());

            if (modelPengusul.insupBuku(
                        Guid.Parse(objLogin.idPersonal),
                        tbTahunTerbit.Text,
                        tbJudul.Text,
                        tbJumlahHalaman.Text,
                        tbNamaPenerbit.Text,
                        tbISBN.Text,
                        tbURL.Text,
                        idBuku
                        ))
            {


                //if (ViewState["isUnggah"] != null)
                //    if (bool.Parse(ViewState["isUnggah"].ToString()))
                //    {
                //        modelPengusul.updateStatusUnggahBuku(idBuku);
                //    }
                if(fileUpload1.HasFile)
                    prosesUnggah(ref fileUpload1);
                isiBuku();
                mvBuku.SetActiveView(vDaftar);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil disimpan...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPengusul.errorMessage);
            }
        }

        private void prosesUnggah(ref FileUpload fUpload)
        {
            string filePath = string.Format(PATH_UNGGAH_BERKAS);
            if (!Directory.Exists(Server.MapPath(filePath)))
            {
                Directory.CreateDirectory(Server.MapPath(filePath));
            }
            string namaFile = PATH_UNGGAH_BERKAS + idBuku.ToString() + ".pdf";

            if (fUpload.HasFile)
            {
                if (fUpload.FileName.EndsWith(".pdf") || fUpload.FileName.EndsWith(".PDF"))
                {
                    if (fUpload.PostedFile.ContentLength < (50 * 1024 * 1024))
                    {
                        fUpload.SaveAs(Server.MapPath(namaFile));
                        modelPengusul.updateStatusUnggahBuku(idBuku);
                        isiBuku();
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah berkas berhasil.");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Ukuran maksimal 50 MB");
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File belum dipilih.");
            }
        }
        protected void btnBatal_Click(object sender, EventArgs e)
        {
            mvBuku.SetActiveView(vDaftar);
        }

        protected void lvBuku_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            ViewState["IdBuku"] = lvBuku.DataKeys[e.ItemIndex]["id_buku"];
            lblJudulDihapus.Text = lvBuku.DataKeys[e.ItemIndex]["judul"].ToString();

            new uiModal().ShowModal(this.Page, "modalKonfirmasiHapus");
        }


        protected void lvBuku_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var rowIndex = int.Parse(e.CommandArgument.ToString());
            idBuku = Guid.Parse(lvBuku.DataKeys[rowIndex]["id_buku"].ToString());
            if (e.CommandName == "UnduhBerkas")
            {
                var kdStatusBerkasBuku = lvBuku.DataKeys[rowIndex]["kd_sts_berkas_buku"].ToString();
                if (kdStatusBerkasBuku == "1")
                {
                    var judul = lvBuku.DataKeys[rowIndex]["judul"].ToString();

                    string namaFile = (judul.Length > 30) ? judul.Substring(0, 30) : judul;
                    namaFile = namaFile.Replace(" ", "_");
                    namaFile = objManipData.removeUnicode(namaFile);

                    string namaBerkas = "buku_" + namaFile + ".pdf";

                    string filePath = string.Format(PATH_UNGGAH_BERKAS + idBuku.ToString() + ".pdf");

                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = PATH_UNGGAH_BERKAS,
                        NamaBerkas = idBuku.ToString() + ".pdf",
                        NamaBerkasdiUnduh = namaBerkas
                    };
                    Session["AtributUnduh"] = atributUnduh;

                    var unduhForm = "Helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Berkas pendukung belum diunggah");
                }
            }
            if (e.CommandName == "unggahBuku")
            {
                new uiModal().ShowModal(this.Page, "modalUnggahBerkas");
            }
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            //var idBuku = Guid.Parse(ViewState["IdBuku"].ToString());
            string filePath = string.Format(PATH_UNGGAH_BERKAS);
            if (!Directory.Exists(Server.MapPath(filePath)))
            {
                Directory.CreateDirectory(Server.MapPath(filePath));
            }
            prosesUnggah(ref fileUpload1b);
            //var ktUnggah = new kontrolUnggah
            //{
            //    path2save = PATH_UNGGAH_BERKAS + idBuku.ToString() + ".pdf",
            //    max_size = 50 * 1024 * 1024, // 500KB
            //    alllowed_ext = "pdf;PDF",
            //    success_info = "Unggah Buku berhasil, silahkan klik tombol selesai",
            //    failed_info = "Unggah Buku gagal !"
            //};
            //Session.Add("ktUnggah", ktUnggah);

            //new uiModal().ShowModal(this.Page, "modalUnggahBerkas");
        }

        protected void btnKonfirmasiHapus_Click(object sender, EventArgs e)
        {
            var idBuku = Guid.Parse(ViewState["IdBuku"].ToString());
            new uiModal().HideModal(this.Page, "modalKonfirmasiHapus");

            if (modelPengusul.delBuku(idBuku))
            {
                isiBuku();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Sukses", "Data berhasil dihapus...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPengusul.errorMessage);
            }

        }

        protected void lbSelesaiUnggah_Click(object sender, EventArgs e)
        {
            //var ktUnggah = (kontrolUnggah)Session["ktUnggah"];
            //if (ktUnggah.isSuccess)
            //{
            //    ViewState["isUnggah"] = ktUnggah.isSuccess;
            //}
            isiBuku();
            new uiModal().HideModal(this.Page, "modalUnggahBerkas");
        }

        protected void lvBuku_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                LinkButton lbUnduh = (LinkButton)e.Item.FindControl("lbUnduh");
                String kd_sts_berkas_buku = lvBuku.DataKeys[e.Item.DataItemIndex]["kd_sts_berkas_buku"].ToString();
                if (kd_sts_berkas_buku != "1")
                {
                    lbUnduh.ForeColor = System.Drawing.Color.Gray;
                }
                else
                {
                    lbUnduh.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}