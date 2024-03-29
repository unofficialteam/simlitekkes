﻿using simlitekkes.Models.Pengusul.Mitra;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraSasaranUMKM : System.Web.UI.UserControl
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitra)
        {
            IdUsulanKegiatan = idUsulanKegiatan;

            skema();
            isiDDLTipeMitra();
            isiDDLJenisMitra();
            isiDDLProvinsi();

            if (idMitra == Guid.Empty)
            {
                // data baru
                IdMitraAbdimas = Guid.NewGuid();

                tbNamaMitra.Text = string.Empty;
                tbJabatan.Text = string.Empty;
                tbNamaKelMitra.Text = string.Empty;
                tbJarak.Text = string.Empty;
                tbAlamatMitraSasaran.Text = string.Empty;
                tbBidangMasalahMitra.Text = string.Empty;
                tbPendanaanThn1.Text = "0";
                tbPendanaanThn2.Text = "0";
                tbPendanaanThn3.Text = "0";

                ddlTipeMitra.SelectedIndex = 0;
                ddlJenisMitra.SelectedIndex = 0;

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
                    tbNamaMitra.Text = mitra.NamaPimpinan;
                    tbJabatan.Text = mitra.Jabatan;
                    tbNamaKelMitra.Text = mitra.NamaUMKM;
                    tbAlamatMitraSasaran.Text = mitra.Alamat;
                    tbJarak.Text = mitra.Jarak.ToString();
                    tbBidangMasalahMitra.Text = mitra.BidangMasalah;
                    tbPendanaanThn1.Text = mitra.DanaTahun1.ToString();
                    tbPendanaanThn2.Text = string.Empty;
                    tbPendanaanThn3.Text = string.Empty;

                    ddlTipeMitra.SelectedValue = mitra.IdTipeMitra.ToString();
                    ddlJenisMitra.SelectedValue = mitra.IdJenisMitra.ToString();

                    ddlProvinsi.SelectedValue = mitra.KdDesa.Substring(0, 2);
                    isiDDLKota();
                    ddlKota.SelectedValue = mitra.KdDesa.Substring(0, 5);
                    isiDDLKecamatan();
                    ddlKecamatan.SelectedValue = mitra.KdDesa.Substring(0, 7);
                    isiDDLDesa();
                    ddlDesa.SelectedValue = mitra.KdDesa;

                }
            }

            if (ViewState["idSkema"].ToString() == "14" || ViewState["idSkema"].ToString() == "26")
            {
                lblPendanaanThn2.Visible = false;
                tbPendanaanThn2.Visible = false;
                lblPendanaanThn3.Visible = false;
                tbPendanaanThn3.Visible = false;
            }

            if (ViewState["idSkema"].ToString() == "29")
            {
                lblinfoBidang.Text = "(Minimal 1 Bidang)";
                lblPendanaanThn2.Visible = false;
                tbPendanaanThn2.Visible = false;
                lblPendanaanThn3.Visible = false;
                tbPendanaanThn3.Visible = false;
            }
        }

        private void skema()
        {
            DataTable dt = new DataTable();
            modelAbdimas.getMitraPengabdianPerSkema(ref dt, IdUsulanKegiatan);
            ViewState["idSkema"] = dt.Rows[0]["id_skema"].ToString();
            ViewState["urutanThnUsulan"] = dt.Rows[0]["urutan_thn_usulan"].ToString();
        }

        public bool Simpan()
        {
            List<string> emptyField = new List<string>();
            //if (ddlTipeMitra.SelectedValue == "-1") emptyField.Add("Tipe Mitra");
            //if (ddlJenisMitra.SelectedValue == "-1") emptyField.Add("Jenis Mitra");
            if (tbNamaMitra.Text.Trim().Length == 0) emptyField.Add("Nama Mitra");
            if (tbJabatan.Text.Trim().Length == 0) emptyField.Add("Jabatan");
            if (tbAlamatMitraSasaran.Text.Trim().Length == 0) emptyField.Add("Alamat Mitra Sasaran");
            if (ddlDesa.SelectedValue == "-1") emptyField.Add("Jenis Mitra");
            if (tbJarak.Text.Trim().Length == 0) emptyField.Add("Jarak");
            if (tbBidangMasalahMitra.Text.Trim().Length == 0) emptyField.Add("Bidang Masalah Mitra");
            if (tbPendanaanThn1.Text.Trim().Length == 0) emptyField.Add("Dana Tahun 1");
            //if (tbPendanaanThn2.Text.Trim().Length == 0) emptyField.Add("Dana Tahun 2");
            //if (tbPendanaanThn3.Text.Trim().Length == 0) emptyField.Add("Dana Tahun 3");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            var mitra = new UMKM();
            mitra.IdMitraAbdimas = IdMitraAbdimas;
            mitra.IdUsulanKegiatan = IdUsulanKegiatan;
            mitra.KdKategoriMitra = KD_KATEGORI_MITRA_PELAKSANA;
            mitra.IdTipeMitra = int.Parse(ddlTipeMitra.SelectedValue); // Default kelmas
            mitra.IdJenisMitra = int.Parse(ddlJenisMitra.SelectedValue); // 
            mitra.NamaPimpinan = tbNamaMitra.Text;
            mitra.Jabatan = tbJabatan.Text;
            mitra.NamaUMKM = tbNamaKelMitra.Text;
            mitra.Alamat = tbAlamatMitraSasaran.Text;
            mitra.Jarak = int.Parse(tbJarak.Text.Replace(".", ""));
            mitra.KdDesa = ddlDesa.SelectedValue;
            mitra.BidangUsaha = "";
            mitra.Asset = decimal.Parse("0");
            mitra.Omzet = decimal.Parse("0");
            mitra.BidangMasalah = tbBidangMasalahMitra.Text;
            mitra.DanaTahun1 = decimal.Parse(tbPendanaanThn1.Text.Replace(".", ""));
            tbPendanaanThn2.Text = "0";
            tbPendanaanThn3.Text = "0";
            mitra.DanaTahun2 = decimal.Parse(tbPendanaanThn2.Text.Replace(".", ""));
            mitra.DanaTahun3 = decimal.Parse(tbPendanaanThn3.Text.Replace(".", ""));
            mitra.LamaKegiatan = 1;
            mitra.UrutanTahun = int.Parse(ViewState["urutanThnUsulan"].ToString());

            if (!modelAbdimas.insupUMKM(mitra))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelAbdimas.errorMessage);
                return false;
            }


            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan mitra berhasil");
            return true;
        }

        private void isiDDLTipeMitra()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            //if (ViewState["idSkema"].ToString() == "14")
            //{

            // Default dipilih: 5	Kelompok Masyarakat

            //if (modelAbdimas.listTipeMitraSasaranUMKMPKM(ref dt))
            //    {
            //        ddlTipeMitra.AppendDataBoundItems = true;
            //        ddlTipeMitra.Items.Clear();
            //        ddlTipeMitra.Items.Add(new ListItem { Text = "-- Pilih Tipe Mitra --", Value = "-1", Selected = true });
            //        ddlTipeMitra.DataSource = dt;
            //        ddlTipeMitra.DataBind();
            //    }
            //}

            //if (ViewState["idSkema"].ToString() == "29")
            //{
            //    if (modelAbdimas.listTipeMitraSasaranUMKM(ref dt))
            //    {
            //        ddlTipeMitra.AppendDataBoundItems = true;
            //        ddlTipeMitra.Items.Clear();
            //        ddlTipeMitra.Items.Add(new ListItem { Text = "-- Pilih Tipe Mitra --", Value = "-1", Selected = true });
            //        ddlTipeMitra.DataSource = dt;
            //        ddlTipeMitra.DataBind();
            //    }
            //}
        }

        private void isiDDLJenisMitra()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            //## Default value 0 Tidak ada 

            //if (modelAbdimas.listJenisMitra(ref dt))
            //{
            //    ddlJenisMitra.AppendDataBoundItems = true;
            //    ddlJenisMitra.Items.Clear();
            //    ddlJenisMitra.Items.Add(new ListItem { Text = "-- Pilih Jenis Mitra --", Value = "-1", Selected = true });
            //    ddlJenisMitra.DataSource = dt;
            //    ddlJenisMitra.DataBind();
            //}
        }

        private void isiDDLProvinsi()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

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
            var objLogin = (Models.login)Session["objLogin"];

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
            var objLogin = (Models.login)Session["objLogin"];

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
            var objLogin = (Models.login)Session["objLogin"];

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
            modelAbdimas.getDaerahPrioritas(ref dt, ddlDesa.SelectedValue, IdUsulanKegiatan);

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