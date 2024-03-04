using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.Models.Pengusul.Mitra;
using simlitekkes.UIControllers;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraSasaranUMKMKKNPPM : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        Models.Pengusul.mitraAbdimas modelAbdimas = new Models.Pengusul.mitraAbdimas();
        const int KD_KATEGORI_MITRA_SASARAN = 5;
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitra)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            //isiDDLTipeMitra();
            //isiDDLJenisMitra();
            isiDDLProvinsi();

            if (idMitra == Guid.Empty)
            {
                // data baru
                ViewState["idMitra"] = Guid.NewGuid();

                tbNamaDesa.Text = string.Empty;
                tbNamaPimpinan.Text = string.Empty;
                tbAlamatMitra.Text = string.Empty;
                tbJarak.Text = string.Empty;
                tbBidangMasalahMitra.Text = string.Empty;

                ddlKota.Enabled = false;
                ddlKecamatan.Enabled = false;
                ddlDesa.Enabled = false;

                tbPendanaanThn1.Text = string.Empty;
            }
            else
            {
                // load data
                ViewState["idMitra"] = idMitra;
                var mitra = new UMKM();
                modelAbdimas.getUMKM(ref mitra, idMitra);
                if (mitra != null)
                {
                    tbNamaDesa.Text = mitra.NamaUMKM;
                    tbNamaPimpinan.Text = mitra.NamaPimpinan;
                    tbAlamatMitra.Text = mitra.Alamat;
                    tbJarak.Text = mitra.Jarak.ToString();
                    tbBidangMasalahMitra.Text = mitra.BidangMasalah;
                    
                    tbPendanaanThn1.Text = mitra.DanaTahun1.ToString();
                    tbPendanaanThn2.Text = mitra.DanaTahun2.ToString();
                    tbPendanaanThn3.Text = mitra.DanaTahun3.ToString();
                    
                    ddlProvinsi.SelectedValue = mitra.KdDesa.Substring(0, 2);
                    isiDDLKota();
                    ddlKota.SelectedValue = mitra.KdDesa.Substring(0, 5);
                    isiDDLKecamatan();
                    ddlKecamatan.SelectedValue = mitra.KdDesa.Substring(0, 7);
                    isiDDLDesa();
                    ddlDesa.SelectedValue = mitra.KdDesa;
                }
            }

            skema();
            
            if(ViewState["idSkema"].ToString() == "28")
            {
                lblPendanaanThn2.Visible = false;
                tbPendanaanThn2.Visible = false;
                lblPendanaanThn3.Visible = false;
                tbPendanaanThn3.Visible = false;
            }
        }

        private void skema()
        {
            DataTable dt = new DataTable();
            modelAbdimas.getMitraPengabdianPerSkema(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            ViewState["idSkema"] = dt.Rows[0]["id_skema"].ToString();
            ViewState["lama_kegiatan"] = dt.Rows[0]["lama_kegiatan"].ToString();
        }

        public bool Simpan()
        {
            List<string> emptyField = new List<string>();

            if (tbNamaDesa.Text.Trim().Length == 0) emptyField.Add("Nama Mitra");
            if (tbNamaPimpinan.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan");
            if (tbAlamatMitra.Text.Trim().Length == 0) emptyField.Add("Alamat Mitra");
            if (ddlDesa.SelectedValue == "-1") emptyField.Add("Jenis Mitra");
            if (tbJarak.Text.Trim().Length == 0) emptyField.Add("Jarak");
            if (tbBidangMasalahMitra.Text.Trim().Length == 0) emptyField.Add("Bidang Masalah");

            //if (tbPendanaanThn1.Text.Trim().Length == 0) emptyField.Add("Dana Tahun 1");
            
            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }
            
            var mitra = new UMKM();
            mitra.IdMitraAbdimas = Guid.Parse(ViewState["idMitra"].ToString());
            mitra.IdUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitra.KdKategoriMitra = KD_KATEGORI_MITRA_SASARAN;
            mitra.IdTipeMitra = 1;
            mitra.IdJenisMitra = 0;
            
            mitra.NamaPimpinan = tbNamaPimpinan.Text;
            mitra.Jabatan = null;
            mitra.NamaUMKM = tbNamaDesa.Text;
            mitra.KdDesa = ddlDesa.SelectedValue;
            mitra.Alamat = tbAlamatMitra.Text;
            mitra.Jarak = int.Parse(tbJarak.Text.Replace(".", ""));
            mitra.BidangUsaha = null;

            mitra.Asset = decimal.Parse(0.ToString());
            mitra.Omzet = decimal.Parse(0.ToString());
            mitra.DanaTahun1 = decimal.Parse(tbPendanaanThn1.Text.Replace(",", "").Replace(".", ""));
            mitra.DanaTahun2 = decimal.Parse(0.ToString().Replace(",", "").Replace(".", ""));
            mitra.DanaTahun3 = decimal.Parse(0.ToString().Replace(",", "").Replace(".", ""));
            mitra.BidangMasalah = tbBidangMasalahMitra.Text;
            mitra.LamaKegiatan = int.Parse(ViewState["lama_kegiatan"].ToString());

            if (!modelAbdimas.insupUMKM(mitra))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelAbdimas.errorMessage);
                return false;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan mitra berhasil");
            }

            return true;
        }
        
        //private void isiDDLTipeMitra()
        //{           
        //    var dt = new DataTable();
        //    var objLogin = (Models.login)Session["objLogin"];

        //    if (modelAbdimas.listTipeMitraSasaranUMKM(ref dt))
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
        //    var objLogin = (Models.login)Session["objLogin"];

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
    }
}