using simlitekkes.Models.Pengusul.Mitra;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class kelompokSasaranPPMUPT : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        Models.Pengusul.mitraAbdimas modelAbdimas = new Models.Pengusul.mitraAbdimas();

        const int KD_KATEGORI_MITRA_PELAKSANA = 5; // Mitra Sasaran

        private Guid IdUsulanKegiatan
        {
            get
            {
                if (ViewState["IdUsulanKegiatan"] == null) ViewState["IdUsulanKegiatan"] = Guid.Empty;
                return Guid.Parse(ViewState["IdUsulanKegiatan"].ToString());
            }
            set { ViewState["IdUsulanKegiatan"] = value; }
        }

        private Guid IdMitraAbdimas
        {
            get
            {
                if (ViewState["IdMitraAbdimas"] == null) ViewState["IdMitraAbdimas"] = Guid.Empty;
                return Guid.Parse(ViewState["IdMitraAbdimas"].ToString());
            }
            set { ViewState["IdMitraAbdimas"] = value; }
        }

        private Guid IdMitraAbdimasReferensi
        {
            get
            {
                if (ViewState["IdMitraAbdimasRef"] == null) ViewState["IdMitraAbdimasRef"] = Guid.Empty;
                return Guid.Parse(ViewState["IdMitraAbdimasRef"].ToString());
            }
            set { ViewState["IdMitraAbdimasRef"] = value; }
        }

        public int UrutanTahun
        {
            get
            {
                if (ViewState["UrutanTahun"] == null) ViewState["UrutanTahun"] = Guid.Empty;
                return int.Parse(ViewState["UrutanTahun"].ToString());
            }
            set { ViewState["UrutanTahun"] = value; }
        }

        public int UrutanMitra
        {
            get
            {
                if (ViewState["UrutanMitra"] == null) ViewState["UrutanMitra"] = Guid.Empty;
                return int.Parse(ViewState["UrutanMitra"].ToString());
            }
            set { ViewState["UrutanMitra"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitraReferensi)
        {
            IdUsulanKegiatan = idUsulanKegiatan;
            IdMitraAbdimasReferensi = idMitraReferensi;
            isiDDLTipeMitra();
            isiDDLJenisMitra();

            if (idMitraReferensi == Guid.Empty)
            {
                // data baru
                IdMitraAbdimas = Guid.NewGuid();

                tbNamaMitra.Text = string.Empty;
                tbNamaPimpinan.Text = string.Empty;
                tbAlamatMitra.Text = string.Empty;
                tbBidangUsahaMitra.Text = string.Empty;

                ddlTipeMitra.SelectedIndex = 0;
                ddlJenisMitra.SelectedIndex = 0;

            }
            else
            {
                // load data
                var mitra = new KelompokSasaranPPMUPT();
                modelAbdimas.getKelompokSasaranPPMUPT(ref mitra, idMitraReferensi, UrutanTahun, UrutanMitra);
                if (mitra != null)
                {
                    IdMitraAbdimas = mitra.IdMitraAbdimas;
                    tbNamaMitra.Text = mitra.NamaKelompok;
                    tbNamaPimpinan.Text = mitra.NamaPimpinan;
                    tbAlamatMitra.Text = mitra.Alamat;
                    tbBidangUsahaMitra.Text = mitra.BidangPengembangan;

                    ddlTipeMitra.SelectedValue = mitra.IdTipeMitra.ToString();
                    ddlJenisMitra.SelectedValue = mitra.IdJenisMitra.ToString();
                }
            }
        }

        public bool VerifikasiData()
        {
            List<string> emptyField = new List<string>();
            if (ddlTipeMitra.SelectedValue == "-1") emptyField.Add("Tipe Mitra");
            if (ddlJenisMitra.SelectedValue == "-1") emptyField.Add("Jenis Mitra");
            if (tbNamaMitra.Text.Trim().Length == 0) emptyField.Add("Nama Mitra");
            if (tbNamaPimpinan.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan");
            if (tbAlamatMitra.Text.Trim().Length == 0) emptyField.Add("Alamat Mitra");
            if (tbBidangUsahaMitra.Text.Trim().Length == 0) emptyField.Add("Bidang Usaha");
            
            if (emptyField.Count > 0)
            {
                var errorMessage = $"Data Mitra {UrutanMitra} Tahun Ke {UrutanTahun} harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            return true;
        }

        public bool Simpan(Guid idMitraReferensi)
        {
            var kelompok = new KelompokSasaranPPMUPT();
            kelompok.IdMitraAbdimas = IdMitraAbdimas;
            kelompok.IdUsulanKegiatan = IdUsulanKegiatan;
            kelompok.KdKategoriMitra = KD_KATEGORI_MITRA_PELAKSANA;
            kelompok.IdTipeMitra = int.Parse(ddlTipeMitra.SelectedValue);
            kelompok.IdJenisMitra = int.Parse(ddlJenisMitra.SelectedValue);
            kelompok.NamaKelompok = tbNamaMitra.Text;
            kelompok.NamaPimpinan = tbNamaPimpinan.Text;
            kelompok.Alamat = tbAlamatMitra.Text;
            kelompok.BidangPengembangan = tbBidangUsahaMitra.Text;
            kelompok.IdMitraReferensi = idMitraReferensi;
            kelompok.UrutanTahun = UrutanTahun;
            kelompok.UrutanMitra = UrutanMitra;

            if (!modelAbdimas.insupKelompokSasaranPPMUPT(kelompok))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, $"Terjadi Kesalahan pada Mitra {UrutanMitra} Tahun ke {UrutanTahun}", 
                    modelAbdimas.errorMessage);
                return false;
            }

            //noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan mitra berhasil");
            return true;
        }

        private void isiDDLTipeMitra()
        {
            var dt = new DataTable();
            if (modelAbdimas.listTipeMitraSasaranUMKMPKM(ref dt))
            {
                ddlTipeMitra.AppendDataBoundItems = true;
                ddlTipeMitra.Items.Clear();
                ddlTipeMitra.Items.Add(new ListItem { Text = "-- Pilih Tipe Mitra --", Value = "-1", Selected = true });
                ddlTipeMitra.DataSource = dt;
                ddlTipeMitra.DataBind();
            }
        }

        private void isiDDLJenisMitra()
        {
            var dt = new DataTable();
            if (modelAbdimas.listJenisMitra(ref dt))
            {
                ddlJenisMitra.AppendDataBoundItems = true;
                ddlJenisMitra.Items.Clear();
                ddlJenisMitra.Items.Add(new ListItem { Text = "-- Pilih Jenis Mitra --", Value = "-1", Selected = true });
                ddlJenisMitra.DataSource = dt;
                ddlJenisMitra.DataBind();
            }
        }

    }
}