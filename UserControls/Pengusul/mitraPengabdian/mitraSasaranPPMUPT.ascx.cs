using simlitekkes.Models.Pengusul.Mitra;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraSasaranPPMUPT : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        Models.Pengusul.mitraAbdimas modelAbdimas = new Models.Pengusul.mitraAbdimas();

        const int KD_KATEGORI_MITRA_PELAKSANA = 5; // Mitra Sasaran
        const int ID_TIPE_MITRA = 5;
        const int ID_JENIS_MITRA = 1;

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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitra)
        {
            IdUsulanKegiatan = idUsulanKegiatan;

            //isiDDLTipeMitra();
            //isiDDLJenisMitra();
            isiDDLProvinsi();

            if (idMitra == Guid.Empty)
            {
                // data baru
                IdMitraAbdimas = Guid.NewGuid();

                //tbNamaMitra.Text = string.Empty;
                tbNamaPimpinan.Text = string.Empty;
                tbAlamatMitra.Text = string.Empty;
                tbJarak.Text = "0";
                //tbBidangUsahaMitra.Text = string.Empty;
                //tbAsset.Text = string.Empty;
                //tbOmzet.Text = string.Empty;
                tbPendanaanThn1.Text = string.Empty;
                tbPendanaanThn2.Text = string.Empty;
                tbPendanaanThn3.Text = string.Empty;

                //ddlTipeMitra.SelectedIndex = 0;
                //ddlJenisMitra.SelectedIndex = 0;

                ddlKota.Enabled = false;
                ddlKecamatan.Enabled = false;
                ddlDesa.Enabled = false;

            }
            else
            {
                // load data
                IdMitraAbdimas = idMitra;

                var mitra = new UMKM();
                modelAbdimas.getUMKM(ref mitra, idMitra);
                if (mitra != null)
                {
                    //tbNamaMitra.Text = mitra.NamaUMKM;
                    tbNamaPimpinan.Text = mitra.NamaPimpinan;
                    tbAlamatMitra.Text = mitra.Alamat;
                    tbJarak.Text = mitra.Jarak.ToString();
                    //tbBidangUsahaMitra.Text = mitra.BidangUsaha;
                    //tbAsset.Text = mitra.Asset.ToString();
                    //tbOmzet.Text = mitra.Omzet.ToString();
                    tbPendanaanThn1.Text = mitra.DanaTahun1.ToString();
                    tbPendanaanThn2.Text = mitra.DanaTahun2.ToString();
                    tbPendanaanThn3.Text = mitra.DanaTahun3.ToString();

                    //ddlTipeMitra.SelectedValue = mitra.IdTipeMitra.ToString();
                    //ddlJenisMitra.SelectedValue = mitra.IdJenisMitra.ToString();

                    ddlProvinsi.SelectedValue = mitra.KdDesa.Substring(0, 2);
                    isiDDLKota();
                    ddlKota.SelectedValue = mitra.KdDesa.Substring(0, 5);
                    isiDDLKecamatan();
                    ddlKecamatan.SelectedValue = mitra.KdDesa.Substring(0, 7);
                    isiDDLDesa();
                    ddlDesa.SelectedValue = mitra.KdDesa;
                }
            }

            ucMitra1Tahun1.InitData(idUsulanKegiatan, idMitra);
            ucMitra2Tahun1.InitData(idUsulanKegiatan, idMitra);
            ucMitra1Tahun2.InitData(idUsulanKegiatan, idMitra);
            ucMitra2Tahun2.InitData(idUsulanKegiatan, idMitra);
            ucMitra1Tahun3.InitData(idUsulanKegiatan, idMitra);
            ucMitra2Tahun3.InitData(idUsulanKegiatan, idMitra);

        }

        public bool Simpan()
        {
            List<string> emptyField = new List<string>();
            //if (ddlTipeMitra.SelectedValue == "-1") emptyField.Add("Tipe Mitra");
            //if (ddlJenisMitra.SelectedValue == "-1") emptyField.Add("Jenis Mitra");
            //if (tbNamaMitra.Text.Trim().Length == 0) emptyField.Add("Nama Mitra");
            if (tbNamaPimpinan.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan");
            if (tbAlamatMitra.Text.Trim().Length == 0) emptyField.Add("Alamat");
            if (ddlDesa.SelectedValue == "-1") emptyField.Add("Jenis Mitra");
            if (tbJarak.Text.Trim().Length == 0) emptyField.Add("Jarak");
            //if (tbBidangUsahaMitra.Text.Trim().Length == 0) emptyField.Add("Bidang Usaha");
            //if (tbAsset.Text.Trim().Length == 0) emptyField.Add("Asset");
            //if (tbOmzet.Text.Trim().Length == 0) emptyField.Add("Omzet");
            if (tbPendanaanThn1.Text.Trim().Length == 0) emptyField.Add("Dana Tahun 1");
            if (tbPendanaanThn2.Text.Trim().Length == 0) emptyField.Add("Dana Tahun 2");
            if (tbPendanaanThn3.Text.Trim().Length == 0) emptyField.Add("Dana Tahun 3");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            if (!ucMitra1Tahun1.VerifikasiData() || !ucMitra2Tahun1.VerifikasiData())
            {
                rblTahun.SelectedValue = "1";
                mvKelompokSasaran.SetActiveView(vTahun1);
                return false;
            };
            if (!ucMitra1Tahun2.VerifikasiData() || !ucMitra2Tahun2.VerifikasiData())
            {
                rblTahun.SelectedValue = "2";
                mvKelompokSasaran.SetActiveView(vTahun2);
                return false;
            };            
            if (!ucMitra1Tahun3.VerifikasiData() || !ucMitra2Tahun3.VerifikasiData())
            {
                rblTahun.SelectedValue = "3";
                mvKelompokSasaran.SetActiveView(vTahun3);
                return false;
            };            

            var mitra = new UMKM();
            mitra.IdMitraAbdimas = IdMitraAbdimas;
            mitra.IdUsulanKegiatan = IdUsulanKegiatan;
            mitra.KdKategoriMitra = KD_KATEGORI_MITRA_PELAKSANA;
            mitra.IdTipeMitra = ID_TIPE_MITRA; //int.Parse(ddlTipeMitra.SelectedValue);
            mitra.IdJenisMitra = ID_JENIS_MITRA; //int.Parse(ddlJenisMitra.SelectedValue);
            //mitra.NamaUMKM = tbNamaMitra.Text;
            mitra.NamaPimpinan = tbNamaPimpinan.Text;
            mitra.Alamat = tbAlamatMitra.Text;
            mitra.Jarak = int.Parse(tbJarak.Text.Replace(".", ""));
            mitra.KdDesa = ddlDesa.SelectedValue;
            //mitra.BidangUsaha = tbBidangUsahaMitra.Text;
            //mitra.Asset = decimal.Parse(tbAsset.Text.Replace(".", ""));
            //mitra.Omzet = decimal.Parse(tbOmzet.Text.Replace(".", ""));
            mitra.DanaTahun1 = decimal.Parse(tbPendanaanThn1.Text.Replace(".", ""));
            mitra.DanaTahun2 = decimal.Parse(tbPendanaanThn2.Text.Replace(".", ""));
            mitra.DanaTahun3 = decimal.Parse(tbPendanaanThn3.Text.Replace(".", ""));
            mitra.LamaKegiatan = 3;

            if (!modelAbdimas.insupUMKM(mitra))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelAbdimas.errorMessage);
                return false;
            }

            if (!ucMitra1Tahun1.Simpan(mitra.IdMitraAbdimas)) return false;
            if (!ucMitra2Tahun1.Simpan(mitra.IdMitraAbdimas)) return false;
            if (!ucMitra1Tahun2.Simpan(mitra.IdMitraAbdimas)) return false;
            if (!ucMitra2Tahun2.Simpan(mitra.IdMitraAbdimas)) return false;
            if (!ucMitra1Tahun3.Simpan(mitra.IdMitraAbdimas)) return false;
            if (!ucMitra2Tahun3.Simpan(mitra.IdMitraAbdimas)) return false;

            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan mitra berhasil");
            return true;
        }

        //private void isiDDLTipeMitra()
        //{
        //    var dt = new DataTable();
        //    if (modelAbdimas.listTipeMitraSasaranUMKMPKM(ref dt))
        //    {
        //        ddlTipeMitra.AppendDataBoundItems = true;
        //        ddlTipeMitra.Items.Clear();
        //        ddlTipeMitra.Items.Add(new ListItem { Text = "-- Pilih Tipe Mitra --", Value = "-1", Selected = true });
        //        ddlTipeMitra.DataSource = dt;
        //        ddlTipeMitra.DataBind();
        //    }
        //}

        //private void isiDDLJenisMitra()
        //{
        //    var dt = new DataTable();
        //    if (modelAbdimas.listJenisMitra(ref dt))
        //    {
        //        ddlJenisMitra.AppendDataBoundItems = true;
        //        ddlJenisMitra.Items.Clear();
        //        ddlJenisMitra.Items.Add(new ListItem { Text = "-- Pilih Jenis Mitra --", Value = "-1", Selected = true });
        //        ddlJenisMitra.DataSource = dt;
        //        ddlJenisMitra.DataBind();
        //    }
        //}

        private void isiDDLProvinsi()
        {
            var dt = new DataTable();
            if (modelAbdimas.listProvinsi(ref dt))
            {
                ddlProvinsi.AppendDataBoundItems = true;
                ddlProvinsi.Items.Clear();
                ddlProvinsi.Items.Add(new ListItem { Text = "-- Pilih Provinsi --", Value = "-1", Selected = true });
                ddlProvinsi.DataSource = dt;
                ddlProvinsi.DataBind();
            }
        }

        private void isiDDLKota()
        {
            var dt = new DataTable();

            if (modelAbdimas.listKota(ref dt, ddlProvinsi.SelectedValue))
            {
                ddlKota.AppendDataBoundItems = true;
                ddlKota.Items.Clear();
                ddlKota.Items.Add(new ListItem { Text = "-- Pilih Kota --", Value = "-1", Selected = true });
                ddlKota.DataSource = dt;
                ddlKota.DataBind();
            }
        }

        private void isiDDLKecamatan()
        {
            var dt = new DataTable();

            if (modelAbdimas.listKecamatan(ref dt, ddlKota.SelectedValue))
            {
                ddlKecamatan.AppendDataBoundItems = true;
                ddlKecamatan.Items.Clear();
                ddlKecamatan.Items.Add(new ListItem { Text = "-- Pilih Kecamatan --", Value = "-1", Selected = true });
                ddlKecamatan.DataSource = dt;
                ddlKecamatan.DataBind();
            }
        }

        private void isiDDLDesa()
        {
            var dt = new DataTable();

            if (modelAbdimas.listDesa(ref dt, ddlKecamatan.SelectedValue))
            {
                ddlDesa.AppendDataBoundItems = true;
                ddlDesa.Items.Clear();
                ddlDesa.Items.Add(new ListItem { Text = "-- Pilih Desa --", Value = "-1", Selected = true });
                ddlDesa.DataSource = dt;
                ddlDesa.DataBind();
            }
        }

        protected void ddlProvinsi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlKota.Enabled = true;
            isiDDLKota();
        }

        protected void ddlKota_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlKecamatan.Enabled = true;
            isiDDLKecamatan();
        }

        protected void ddlKecamatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDesa.Enabled = true;
            isiDDLDesa();
        }

        protected void ddlDesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelAbdimas.getDaerahPrioritas(ref dt, ddlDesa.SelectedValue, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));

            if (dt.Rows.Count > 0)
            {
                lblInfoDesaPrioritas.Visible = true;
                lblInfoDesaPrioritas.Text = dt.Rows[0]["daerah_prioritas"].ToString();
            }
            else
            {
                lblInfoDesaPrioritas.Visible = false;
            }
        }

        protected void rblTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (rblTahun.SelectedValue)
            {
                case "1":
                    mvKelompokSasaran.SetActiveView(vTahun1);
                    break;
                case "2":
                    mvKelompokSasaran.SetActiveView(vTahun2);
                    break;
                case "3":
                    mvKelompokSasaran.SetActiveView(vTahun3);
                    break;
            }
        }
    }
}