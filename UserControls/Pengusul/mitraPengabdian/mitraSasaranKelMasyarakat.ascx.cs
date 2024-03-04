using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;
using simlitekkes;
using simlitekkes.Models.Pengusul.Mitra;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraSasaranKelMasyarakat : System.Web.UI.UserControl
    {  
        uiNotify noty = new uiNotify();
        Models.Pengusul.mitraAbdimas objMitraAbdimas = new Models.Pengusul.mitraAbdimas();
        //internal object hididmitra;
        public Guid hididmitra;
        const int KD_KATEGORI_MITRA = 5;
        const int ID_TIPE_KEL_MASYARAKAT = 6;
        const int ID_JENIS_MITRA = 6;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitra)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            isiDDLProvinsi();
            if (idMitra == Guid.Empty)
            {
                ViewState["idMitra"] = Guid.NewGuid();
                // data baru
                tbNamaPimpinanDesa.Text = string.Empty;
                tbAlamatMitraSasaran.Text = string.Empty;
                tbJarak.Text = string.Empty;
                //tbNamaKelMitra.Text = string.Empty;
                //tbNamaPimpinanMitra.Text = string.Empty;
                //tbAlamatKelSasaran.Text = string.Empty;
                tbPendanaanThn1.Text = "0";// string.Empty;
                //tbPendanaanThn2.Text = "0";//string.Empty;
                //tbPendanaanThn3.Text = "0";//string.Empty;
                ViewState["idMitraRef"] = Guid.Empty;
            }
            else
            {
                // load data               

                ViewState["idMitra"] = idMitra;
                
                var sasaranKelMas = new KelompokMasyarakat();
                objMitraAbdimas.getKelompokMasyarakat(ref sasaranKelMas, idMitra);
                if (sasaranKelMas != null)
                {
                    //ddlJenisMitra.SelectedValue = sasaranKelMas.IdJenisMitra.ToString();
                    tbNamaPimpinanDesa.Text = sasaranKelMas.NamaPimpinanMitra;
                    tbAlamatMitraSasaran.Text = sasaranKelMas.AlamatMitra;
                    //ddlDesa.SelectedValue = sasaranKelMas.KdDesa;
                    tbJarak.Text = sasaranKelMas.Jarak.ToString();
                    
                    tbPendanaanThn1.Text = sasaranKelMas.DanaTahun1.ToString();
                    //tbPendanaanThn2.Text = sasaranKelMas.DanaTahun2.ToString();
                    //tbPendanaanThn3.Text = sasaranKelMas.DanaTahun3.ToString();

                    ddlProvinsi.SelectedValue = sasaranKelMas.KdDesa.Substring(0, 2);
                    isiDDLKota();
                    ddlKota.SelectedValue = sasaranKelMas.KdDesa.Substring(0, 5);
                    isiDDLKecamatan();
                    ddlKecamatan.SelectedValue = sasaranKelMas.KdDesa.Substring(0, 7);
                    isiDDLDesa();
                    ddlDesa.SelectedValue = sasaranKelMas.KdDesa;
                }
                else
                {
                    isiDDLProvinsi();
                    ddlKota.Enabled = false;
                    ddlKecamatan.Enabled = false;
                    ddlDesa.Enabled = false;

                }

                //var sasaranKelSasaran = new KelompokSasaran();
                //objMitraAbdimas.getKelompokSasaran(ref sasaranKelSasaran, idMitra);
                //if (sasaranKelSasaran != null)
                //{
                //    tbNamaKelMitra.Text = sasaranKelSasaran.NamaKelompok;
                //    tbNamaPimpinanMitra.Text = sasaranKelSasaran.NamaPimpinan;
                //    tbAlamatKelSasaran.Text = sasaranKelSasaran.Alamat;
                //    ddlDesaSasaran.SelectedValue = sasaranKelSasaran.KdDesa.ToString();
                //    ddlJenisMitraKelSasaran.SelectedValue = sasaranKelSasaran.IdJenisMitra.ToString();
                //    tbBidPengembanganMitra.Text = sasaranKelSasaran.BidangPengembangan;
                //    Guid idMitraRef = Guid.Parse(sasaranKelSasaran.IdMitraReferensi.ToString());
                //    ViewState["idMitraRef"] = idMitraRef;
                //}
            }
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            hididmitra = Guid.Parse( ViewState["idMitra"].ToString());

            //isiDDLJenisMitra();



            //ddlKotaKelSasaran.Enabled = false;
            //ddlKecamatanSasaran.Enabled = false;
            //ddlDesaSasaran.Enabled = false;
        }

        public bool Simpan()
        {
            List<string> emptyField = new List<string>();
            if (tbNamaPimpinanDesa.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan");
            if (tbAlamatMitraSasaran.Text.Trim().Length == 0) emptyField.Add("Alamat Institusi Mitra");
            if (tbJarak.Text.Trim().Length == 0) emptyField.Add("tbJarak");
            if (ddlDesa.SelectedValue == "-1") emptyField.Add("Desa");
            //if (tbNamaPimpinanMitra.Text.Trim().Length == 0) emptyField.Add("Pimpinan");
            //if (tbJenisSasaran.Text.Trim().Length == 0) emptyField.Add("Jenis mitra");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return true;
            }
            if (tbPendanaanThn1.Text == "")
            {
                tbPendanaanThn1.Text = "0";
            };
            //if (tbPendanaanThn2.Text == "")
            //{
            //    tbPendanaanThn2.Text = "0";
            //};
            //if (tbPendanaanThn3.Text == "")
            //{
            //    tbPendanaanThn3.Text = "0";
            //};
            var sasaranKelMas = new KelompokMasyarakat();
            var sasaranKelSasaran = new KelompokSasaran();
            
            sasaranKelMas.IdMitraAbdimas = Guid.Parse(ViewState["idMitra"].ToString());
            sasaranKelMas.IdUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            sasaranKelMas.KdKategoriMitra = KD_KATEGORI_MITRA;
            sasaranKelMas.IdTipeMitra = ID_TIPE_KEL_MASYARAKAT;
            sasaranKelMas.IdJenisMitra = ID_JENIS_MITRA;// int.Parse(ddlJenisMitra.SelectedValue);
            sasaranKelMas.NamaPimpinanMitra = tbNamaPimpinanDesa.Text;
            sasaranKelMas.KdDesa = ddlDesa.SelectedValue;
            sasaranKelMas.AlamatMitra = tbAlamatMitraSasaran.Text;
            sasaranKelMas.Jarak = int.Parse(tbJarak.Text.Replace(".", ""));

            sasaranKelMas.DanaTahun1 = decimal.Parse(tbPendanaanThn1.Text.Replace(",",""));
            //sasaranKelMas.DanaTahun2 = decimal.Parse(tbPendanaanThn2.Text.Replace(",", ""));
            //sasaranKelMas.DanaTahun3 = decimal.Parse(tbPendanaanThn3.Text.Replace(",", ""));
            sasaranKelMas.LamaKegiatan = 1;
            
            //sasaranKelSasaran.IdMitraAbdimas = Guid.Parse(ViewState["idMitra"].ToString());
            //sasaranKelSasaran.IdUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            //sasaranKelSasaran.KdKategoriMitra = KD_KATEGORI_MITRA;
            //sasaranKelSasaran.IdTipeMitra = ID_TIPE_KEL_MASYARAKAT;
            //sasaranKelSasaran.IdJenisMitra = ID_JENIS_MITRA;// int.Parse(ddlJenisMitra.SelectedValue);
            //sasaranKelSasaran.NamaPimpinan = null;
            //sasaranKelSasaran.NamaKelompok = null;
            //sasaranKelSasaran.KdDesa = null;
            //sasaranKelSasaran.Alamat = null;
            //sasaranKelSasaran.BidangPengembangan = null;
            //sasaranKelSasaran.IdMitraReferensi = Guid.Parse("00000000-0000-0000-0000-000000000000");

            if (!objMitraAbdimas.insupKelompokMasyarakat(sasaranKelMas)) 
                //||!objMitraAbdimas.insupKelompokSasaran(sasaranKelSasaran))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitraAbdimas.errorMessage);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan mitra berhasil");
            }
            
            return true;
        }

        private void isiDDLJenisMitra()
        {
            //var dt = new DataTable();
            //var objLogin = (Models.login)Session["objLogin"];

            //if (objMitraAbdimas.listJenisMitra(ref dt))
            //{
            //    ddlJenisMitra.AppendDataBoundItems = true;
            //    ddlJenisMitra.Items.Clear();
            //    ddlJenisMitra.Items.Add(new ListItem { Text = "-- Pilih Jenis Mitra --", Value = "-1", Selected = true });
            //    ddlJenisMitra.DataSource = dt;
            //    ddlJenisMitra.DataBind();

            //    //ddlJenisMitraKelSasaran.AppendDataBoundItems = true;
            //    //ddlJenisMitraKelSasaran.Items.Clear();
            //    //ddlJenisMitraKelSasaran.Items.Add(new ListItem { Text = "-- Pilih Jenis Mitra --", Value = "-1", Selected = true });
            //    //ddlJenisMitraKelSasaran.DataSource = dt;
            //    //ddlJenisMitraKelSasaran.DataBind();
            //}
        }

        private void isiDDLProvinsi()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (objMitraAbdimas.listProvinsi(ref dt))
            {
                ddlProvinsi.AppendDataBoundItems = true;
                ddlProvinsi.Items.Clear();
                ddlProvinsi.Items.Add(new ListItem { Text = "-- Pilih Provinsi --", Value = "-1", Selected = true });
                ddlProvinsi.DataSource = dt;
                ddlProvinsi.DataBind();
                //ddlProvinsiKelSasaran.AppendDataBoundItems = true;
                //ddlProvinsiKelSasaran.Items.Clear();
                //ddlProvinsiKelSasaran.Items.Add(new ListItem { Text = "-- Pilih Provinsi --", Value = "-1", Selected = true });
                //ddlProvinsiKelSasaran.DataSource = dt;
                //ddlProvinsiKelSasaran.DataBind();
            }
        }

        private void isiDDLKota()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (objMitraAbdimas.listKota(ref dt, ddlProvinsi.SelectedValue))
            {
                ddlKota.AppendDataBoundItems = true;
                ddlKota.Items.Clear();
                ddlKota.Items.Add(new ListItem { Text = "-- Pilih Kota --", Value = "-1", Selected = true });
                ddlKota.DataSource = dt;
                ddlKota.DataBind();
                //ddlKotaKelSasaran.AppendDataBoundItems = true;
                //ddlKotaKelSasaran.Items.Clear();
                //ddlKotaKelSasaran.Items.Add(new ListItem { Text = "-- Pilih Kota --", Value = "-1", Selected = true });
                //ddlKotaKelSasaran.DataSource = dt;
                //ddlKotaKelSasaran.DataBind();
            }
        }

        private void isiDDLKecamatan()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (objMitraAbdimas.listKecamatan(ref dt, ddlKota.SelectedValue))
            {
                ddlKecamatan.AppendDataBoundItems = true;
                ddlKecamatan.Items.Clear();
                ddlKecamatan.Items.Add(new ListItem { Text = "-- Pilih Kecamatan --", Value = "-1", Selected = true });
                ddlKecamatan.DataSource = dt;
                ddlKecamatan.DataBind();
                //ddlKecamatanSasaran.AppendDataBoundItems = true;
                //ddlKecamatanSasaran.Items.Clear();
                //ddlKecamatanSasaran.Items.Add(new ListItem { Text = "-- Pilih Kecamatan --", Value = "-1", Selected = true });
                //ddlKecamatanSasaran.DataSource = dt;
                //ddlKecamatanSasaran.DataBind();
            }
        }

        private void isiDDLDesa()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (objMitraAbdimas.listDesa(ref dt, ddlKecamatan.SelectedValue))
            {
                ddlDesa.AppendDataBoundItems = true;
                ddlDesa.Items.Clear();
                ddlDesa.Items.Add(new ListItem { Text = "-- Pilih Desa --", Value = "-1", Selected = true });
                ddlDesa.DataSource = dt;
                ddlDesa.DataBind();
                //ddlDesaSasaran.AppendDataBoundItems = true;
                //ddlDesaSasaran.Items.Clear();
                //ddlDesaSasaran.Items.Add(new ListItem { Text = "-- Pilih Desa --", Value = "-1", Selected = true });
                //ddlDesaSasaran.DataSource = dt;
                //ddlDesaSasaran.DataBind();
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
            objMitraAbdimas.getDaerahPrioritas(ref dt, ddlDesa.SelectedValue, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));

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

        protected void ddlProvinsiKelSasaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlKotaKelSasaran.Enabled = true;
            //isiDDLKota();
        }

        protected void ddlKotaKelSasaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlKecamatanSasaran.Enabled = true;
            //isiDDLKecamatan();
        }

        protected void ddlKecamatanSasaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlDesaSasaran.Enabled = true;
            //isiDDLDesa();
        }

        protected void ddlDesaSasaran_SelectedIndexChanged(object sender, EventArgs e)
        {

            //DataTable dt = new DataTable();
            //objMitraAbdimas.getDaerahPrioritas(ref dt, ddlDesaSasaran.SelectedValue, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));

            //if (dt.Rows.Count > 0)
            //{
            //    lblInfoDesaPrioritas1.Visible = true;
            //    lblInfoDesaPrioritas1.Text = dt.Rows[0]["daerah_prioritas"].ToString();
            //}
            //else
            //{
            //    lblInfoDesaPrioritas1.Visible = false;
            //}
        }
    }
}