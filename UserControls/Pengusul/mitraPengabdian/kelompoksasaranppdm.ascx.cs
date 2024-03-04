using simlitekkes.Models.Pengusul.Mitra;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class kelompoksasaranppdm : System.Web.UI.UserControl
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

        private Guid IdMitraAbdimas1
        {
            get
            {
                if (ViewState["IdMitraAbdimas1"] == null) ViewState["IdMitraAbdimas1"] = Guid.Empty;
                return Guid.Parse(ViewState["IdMitraAbdimas1"].ToString());
            }
            set { ViewState["IdMitraAbdimas1"] = value; }
        }

        private Guid IdMitraAbdimas2
        {
            get
            {
                if (ViewState["IdMitraAbdimas2"] == null) ViewState["IdMitraAbdimas2"] = Guid.Empty;
                return Guid.Parse(ViewState["IdMitraAbdimas2"].ToString());
            }
            set { ViewState["IdMitraAbdimas2"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, string tahun_ke, Guid refidmitra)
        {
            urutan_tahun.Value = tahun_ke;
            ViewState["IdUsulanKegiatan"] = idUsulanKegiatan;
            ViewState["refidmitra"] = refidmitra;

            isiDDLTipeMitra1();
            isiDDLJenisMitra1();

            isiDDLTipeMitra2();
            isiDDLJenisMitra2();


            DataTable dtMitra = new DataTable();
            modelAbdimas.getMitraKelompokibdm(ref dtMitra, idUsulanKegiatan.ToString(), tahun_ke, refidmitra.ToString());
            if (dtMitra.Rows.Count > 0)
            {
                IdMitraAbdimas1 = Guid.Parse(dtMitra.Rows[0]["id_mitra_abdimas"].ToString());

                tbNamaMitra1.Text = dtMitra.Rows[0]["nama_organisasi_institusi"].ToString();
                        tbNamaPimpinan1.Text = dtMitra.Rows[0]["nama_pimpinan_mitra"].ToString();
                        tbAlamatMitra1.Text = dtMitra.Rows[0]["alamat_organisasi_institusi"].ToString();
                        tbBidangUsahaMitra1.Text = dtMitra.Rows[0]["bidang_masalah_mitra"].ToString();

                        ddlTipeMitra1.SelectedValue = dtMitra.Rows[0]["id_tipe_mitra"].ToString();
                        ddlJenisMitra1.SelectedValue = dtMitra.Rows[0]["id_jenis_mitra"].ToString();

                if (dtMitra.Rows.Count > 1)
                {
                    IdMitraAbdimas2 = Guid.Parse(dtMitra.Rows[1]["id_mitra_abdimas"].ToString());

                    tbNamaMitra2.Text = dtMitra.Rows[1]["nama_organisasi_institusi"].ToString();
                    tbNamaPimpinan2.Text = dtMitra.Rows[1]["nama_pimpinan_mitra"].ToString();
                    tbAlamatMitra2.Text = dtMitra.Rows[1]["alamat_organisasi_institusi"].ToString();
                    tbBidangUsahaMitra2.Text = dtMitra.Rows[1]["bidang_masalah_mitra"].ToString();

                    ddlTipeMitra2.SelectedValue = dtMitra.Rows[1]["id_tipe_mitra"].ToString();
                    ddlJenisMitra2.SelectedValue = dtMitra.Rows[1]["id_jenis_mitra"].ToString();

                }
                else
                {
                    IdMitraAbdimas2 = Guid.NewGuid();
                    tbNamaMitra2.Text = string.Empty;
                    tbNamaPimpinan2.Text = string.Empty;
                    tbAlamatMitra2.Text = string.Empty;
                    tbBidangUsahaMitra2.Text = string.Empty;

                    ddlTipeMitra2.SelectedIndex = 0;
                    ddlJenisMitra2.SelectedIndex = 0;

                }
            }
            else
            {

                IdMitraAbdimas1 = Guid.NewGuid();
                IdMitraAbdimas2 = Guid.NewGuid();

                tbNamaMitra1.Text = string.Empty;
                tbNamaPimpinan1.Text = string.Empty;
                tbAlamatMitra1.Text = string.Empty;
                tbBidangUsahaMitra1.Text = string.Empty;

                ddlTipeMitra1.SelectedIndex = 0;
                ddlJenisMitra1.SelectedIndex = 0;

                tbNamaMitra2.Text = string.Empty;
                tbNamaPimpinan2.Text = string.Empty;
                tbAlamatMitra2.Text = string.Empty;
                tbBidangUsahaMitra2.Text = string.Empty;

                ddlTipeMitra2.SelectedIndex = 0;
                ddlJenisMitra2.SelectedIndex =  0;

            };


            //IdUsulanKegiatan = idUsulanKegiatan;

            //isiDDLTipeMitra1();
            //isiDDLJenisMitra1();

            //isiDDLTipeMitra2();
            //isiDDLJenisMitra2();


            //if (idMitra1 == Guid.Empty)
            //{
            //    // data baru
            //    IdMitraAbdimas1 = Guid.NewGuid();

            //    tbNamaMitra1.Text = string.Empty;
            //    tbNamaPimpinan1.Text = string.Empty;
            //    tbAlamatMitra1.Text = string.Empty;
            //    tbBidangUsahaMitra1.Text = string.Empty;

            //    ddlTipeMitra1.SelectedIndex = 0;
            //    ddlJenisMitra1.SelectedIndex = 0;

            //}
            //else
            //{
            //    // load data
            //    IdMitraAbdimas1 = idMitra1;

            //    var mitra = new UMKM();
            //    modelAbdimas.getUMKM(ref mitra, idMitra1);
            //    if (mitra != null)
            //    {
            //        tbNamaMitra1.Text = mitra.NamaUMKM;
            //        tbNamaPimpinan1.Text = mitra.NamaPimpinan;
            //        tbAlamatMitra1.Text = mitra.Alamat;
            //        tbBidangUsahaMitra1.Text = mitra.BidangUsaha;

            //        ddlTipeMitra1.SelectedValue = mitra.IdTipeMitra.ToString();
            //        ddlJenisMitra1.SelectedValue = mitra.IdJenisMitra.ToString();

            //    }
            //}


            //if (idMitra1 == Guid.Empty)
            //{
            //    // data baru
            //    IdMitraAbdimas1 = Guid.NewGuid();

            //    tbNamaMitra1.Text = string.Empty;
            //    tbNamaPimpinan1.Text = string.Empty;
            //    tbAlamatMitra1.Text = string.Empty;
            //    tbBidangUsahaMitra1.Text = string.Empty;

            //    ddlTipeMitra1.SelectedIndex = 0;
            //    ddlJenisMitra1.SelectedIndex = 0;

            //}
            //else
            //{
            //    // load data
            //    IdMitraAbdimas1 = idMitra1;

            //    var mitra = new UMKM();
            //    modelAbdimas.getUMKM(ref mitra, idMitra1);
            //    if (mitra != null)
            //    {
            //        tbNamaMitra1.Text = mitra.NamaUMKM;
            //        tbNamaPimpinan1.Text = mitra.NamaPimpinan;
            //        tbAlamatMitra1.Text = mitra.Alamat;
            //        tbBidangUsahaMitra1.Text = mitra.BidangUsaha;

            //        ddlTipeMitra1.SelectedValue = mitra.IdTipeMitra.ToString();
            //        ddlJenisMitra1.SelectedValue = mitra.IdJenisMitra.ToString();

            //    }
            //}

        }

        public bool CekSimpan()
        {
            List<string> emptyField = new List<string>();
            if (ddlTipeMitra1.SelectedValue == "-1") emptyField.Add("Tipe Mitra 1");
            if (ddlJenisMitra1.SelectedValue == "-1") emptyField.Add("Jenis Mitra 1");
            if (tbNamaMitra1.Text.Trim().Length == 0) emptyField.Add("Nama Mitra 1");
            if (tbNamaPimpinan1.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan 1");
            if (tbAlamatMitra1.Text.Trim().Length == 0) emptyField.Add("Alamat Mitra 1");
            if (tbBidangUsahaMitra1.Text.Trim().Length == 0) emptyField.Add("Bidang Usaha 1");

            //if (ddlTipeMitra2.SelectedValue == "-1") emptyField.Add("Tipe Mitra 2");
            //if (ddlJenisMitra2.SelectedValue == "-1") emptyField.Add("Jenis Mitra 2");
            //if (tbNamaMitra2.Text.Trim().Length == 0) emptyField.Add("Nama Mitra 2");
            //if (tbNamaPimpinan2.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan 2");
            //if (tbAlamatMitra2.Text.Trim().Length == 0) emptyField.Add("Alamat Mitra 2");
            //if (tbBidangUsahaMitra2.Text.Trim().Length == 0) emptyField.Add("Bidang Usaha 2");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            return true;

        }

        public bool Simpan()
        {
            List<string> emptyField = new List<string>();
            if (ddlTipeMitra1.SelectedValue == "-1") emptyField.Add("Tipe Mitra 1");
            if (ddlJenisMitra1.SelectedValue == "-1") emptyField.Add("Jenis Mitra 1");
            if (tbNamaMitra1.Text.Trim().Length == 0) emptyField.Add("Nama Mitra 1");
            if (tbNamaPimpinan1.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan 1");
            if (tbAlamatMitra1.Text.Trim().Length == 0) emptyField.Add("Alamat Mitra 1");
            if (tbBidangUsahaMitra1.Text.Trim().Length == 0) emptyField.Add("Bidang Usaha 1");

            //if (ddlTipeMitra2.SelectedValue == "-1") emptyField.Add("Tipe Mitra 2");
            //if (ddlJenisMitra2.SelectedValue == "-1") emptyField.Add("Jenis Mitra 2");
            //if (tbNamaMitra2.Text.Trim().Length == 0) emptyField.Add("Nama Mitra 2");
            //if (tbNamaPimpinan2.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan 2");
            //if (tbAlamatMitra2.Text.Trim().Length == 0) emptyField.Add("Alamat Mitra 2");
            //if (tbBidangUsahaMitra2.Text.Trim().Length == 0) emptyField.Add("Bidang Usaha 2");


            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            var mitra1 = new KelompokMasyarakatPPDM();
            mitra1.IdMitraAbdimas = IdMitraAbdimas1;
            mitra1.IdUsulanKegiatan = IdUsulanKegiatan;
            mitra1.KdKategoriMitra = KD_KATEGORI_MITRA_PELAKSANA;
            mitra1.IdTipeMitra = int.Parse(ddlTipeMitra1.SelectedValue);
            mitra1.IdJenisMitra = int.Parse(ddlJenisMitra1.SelectedValue);
            mitra1.NamaMitra = tbNamaMitra1.Text;
            mitra1.NamaPimpinanMitra = tbNamaPimpinan1.Text;
            mitra1.AlamatMitra = tbAlamatMitra1.Text;
            mitra1.BidangMasalah = tbBidangUsahaMitra1.Text;
            mitra1.IdMitraReferensi = Guid.Parse(ViewState["refidmitra"].ToString());
            mitra1.UrutanTahun = int.Parse(urutan_tahun.Value.ToString());// decimal.Parse(tbPendanaanThn1.Text.Replace(".", ""));

            if (!modelAbdimas.insupPPDM(mitra1))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelAbdimas.errorMessage);
                return false;
            }

            //var mitra2 = new KelompokMasyarakatPPDM();
            //mitra2.IdMitraAbdimas = IdMitraAbdimas2;
            //mitra2.IdUsulanKegiatan = IdUsulanKegiatan;
            //mitra2.KdKategoriMitra = KD_KATEGORI_MITRA_PELAKSANA;
            //mitra2.IdTipeMitra = int.Parse(ddlTipeMitra2.SelectedValue);
            //mitra2.IdJenisMitra = int.Parse(ddlJenisMitra2.SelectedValue);
            //mitra2.NamaMitra = tbNamaMitra2.Text;
            //mitra2.NamaPimpinanMitra = tbNamaPimpinan2.Text;
            //mitra2.AlamatMitra = tbAlamatMitra2.Text;
            //mitra2.BidangMasalah = tbBidangUsahaMitra2.Text;
            //mitra2.IdMitraReferensi = Guid.Parse(ViewState["refidmitra"].ToString());
            //mitra2.UrutanTahun = int.Parse(urutan_tahun.Value.ToString());// decimal.Parse(tbPendanaanThn1.Text.Replace(".", ""));

            //if (!modelAbdimas.insupPPDM(mitra2))
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelAbdimas.errorMessage);
            //    return false;
            //}

            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan mitra berhasil");
            return true;


        } 

        private void isiDDLTipeMitra1()
        {
            var dt = new DataTable();
            if (modelAbdimas.listTipeMitraSasaranUMKMPKM(ref dt))
            {
                ddlTipeMitra1.AppendDataBoundItems = true;
                ddlTipeMitra1.Items.Clear();
                ddlTipeMitra1.Items.Add(new ListItem { Text = "-- Pilih Tipe Mitra --", Value = "-1", Selected = true });
                ddlTipeMitra1.DataSource = dt;
                ddlTipeMitra1.DataBind();
            }
        }
        private void isiDDLTipeMitra2()
        {
            var dt = new DataTable();
            if (modelAbdimas.listTipeMitraSasaranUMKMPKM(ref dt))
            {
                ddlTipeMitra2.AppendDataBoundItems = true;
                ddlTipeMitra2.Items.Clear();
                ddlTipeMitra2.Items.Add(new ListItem { Text = "-- Pilih Tipe Mitra --", Value = "-1", Selected = true });
                ddlTipeMitra2.DataSource = dt;
                ddlTipeMitra2.DataBind();
            }
        }


        private void isiDDLJenisMitra1()
        {
            var dt = new DataTable();
            if (modelAbdimas.listJenisMitra(ref dt))
            {
                ddlJenisMitra1.AppendDataBoundItems = true;
                ddlJenisMitra1.Items.Clear();
                ddlJenisMitra1.Items.Add(new ListItem { Text = "-- Pilih Jenis Mitra --", Value = "-1", Selected = true });
                ddlJenisMitra1.DataSource = dt;
                ddlJenisMitra1.DataBind();
            }
        }
        private void isiDDLJenisMitra2()
        {
            var dt = new DataTable();
            if (modelAbdimas.listJenisMitra(ref dt))
            {
                ddlJenisMitra2.AppendDataBoundItems = true;
                ddlJenisMitra2.Items.Clear();
                ddlJenisMitra2.Items.Add(new ListItem { Text = "-- Pilih Jenis Mitra --", Value = "-1", Selected = true });
                ddlJenisMitra2.DataSource = dt;
                ddlJenisMitra2.DataBind();
            }
        }

        //private void isiDDLProvinsi()
        //{
        //    var dt = new DataTable();
        //    if (modelAbdimas.listProvinsi(ref dt))
        //    {
        //        ddlProvinsi.AppendDataBoundItems = true;
        //        ddlProvinsi.Items.Clear();
        //        ddlProvinsi.Items.Add(new ListItem { Text = "-- Pilih Provinsi --", Value = "-1", Selected = true });
        //        ddlProvinsi.DataSource = dt;
        //        ddlProvinsi.DataBind();
        //    }
        //}

        //private void isiDDLKota()
        //{
        //    var dt = new DataTable();

        //    if (modelAbdimas.listKota(ref dt, ddlProvinsi.SelectedValue))
        //    {
        //        ddlKota.AppendDataBoundItems = true;
        //        ddlKota.Items.Clear();
        //        ddlKota.Items.Add(new ListItem { Text = "-- Pilih Kota --", Value = "-1", Selected = true });
        //        ddlKota.DataSource = dt;
        //        ddlKota.DataBind();
        //    }
        //}

        //private void isiDDLKecamatan()
        //{
        //    var dt = new DataTable();

        //    if (modelAbdimas.listKecamatan(ref dt, ddlKota.SelectedValue))
        //    {
        //        ddlKecamatan.AppendDataBoundItems = true;
        //        ddlKecamatan.Items.Clear();
        //        ddlKecamatan.Items.Add(new ListItem { Text = "-- Pilih Kecamatan --", Value = "-1", Selected = true });
        //        ddlKecamatan.DataSource = dt;
        //        ddlKecamatan.DataBind();
        //    }
        //}

        //private void isiDDLDesa()
        //{
        //    var dt = new DataTable();

        //    if (modelAbdimas.listDesa(ref dt, ddlKecamatan.SelectedValue))
        //    {
        //        ddlDesa.AppendDataBoundItems = true;
        //        ddlDesa.Items.Clear();
        //        ddlDesa.Items.Add(new ListItem { Text = "-- Pilih Desa --", Value = "-1", Selected = true });
        //        ddlDesa.DataSource = dt;
        //        ddlDesa.DataBind();
        //    }
        //}

        //protected void ddlProvinsi_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlKota.Enabled = true;
        //    isiDDLKota();
        //}

        //protected void ddlKota_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlKecamatan.Enabled = true;
        //    isiDDLKecamatan();
        //}

        //protected void ddlKecamatan_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlDesa.Enabled = true;
        //    isiDDLDesa();
        //}

        //protected void ddlDesa_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    modelAbdimas.getDaerahPrioritas(ref dt, ddlDesa.SelectedValue, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));

        //    if (dt.Rows.Count > 0)
        //    {
        //        lblInfoDesaPrioritas.Visible = true;
        //        lblInfoDesaPrioritas.Text = dt.Rows[0]["daerah_prioritas"].ToString();
        //    }
        //    else
        //    {
        //        lblInfoDesaPrioritas.Visible = false;
        //    }
        //}
    }
}