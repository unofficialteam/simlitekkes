using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using System.Data;
using simlitekkes;
using simlitekkes.Models.Pengusul.Mitra;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraPelaksanaPemdaKota : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        Models.Pengusul.mitraAbdimas objMitraAbdimas = new Models.Pengusul.mitraAbdimas();

        const int KD_KATEGORI_MITRA_PELAKSANA = 1;

        public bool IsTipeMono
        {
            get
            {
                if (ViewState["IsTipeMono"] == null) ViewState["IsTipeMono"] = false;
                return bool.Parse(ViewState["IsTipeMono"].ToString());
            }
            set { ViewState["IdUsulanKegiatan"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitra)
        {
            isiDDLTipeMitra();
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            skema();

            if (idMitra == Guid.Empty)
            {
                // data baru

                ViewState["isNew"] = true;
                ViewState["idMitra"] = Guid.NewGuid();

                tbNamaMitra.Text = string.Empty;
                tbNamaPimpinanMitra.Text = string.Empty;
                tbJabatan.Text = string.Empty;
                tbAlamatInstitusi.Text = string.Empty;
                tbPendanaanThn1.Text = string.Empty;
                if (ViewState["idSkema"].ToString() != "15")
                {
                    tbPendanaanThn2.Text = string.Empty;
                    tbPendanaanThn3.Text = string.Empty;
                } else
                {
                    lblPendanaanThn2.Visible = false;
                    lblPendanaanThn3.Visible = false;
                    tbPendanaanThn2.Visible = false;
                    tbPendanaanThn3.Visible = false;
                }
                ddlTipeMitra.SelectedIndex = 0;
            }
            else
            {
                // load data
                ViewState["idMitra"] = idMitra;

                DataTable dt = new DataTable();
                var pemdaPemkot = new PemdaPemkot();
                objMitraAbdimas.getPemdaPemkot(ref pemdaPemkot, idMitra);
                if (pemdaPemkot != null)
                {
                    tbNamaMitra.Text = pemdaPemkot.NamaOrganisasiInstitusi;
                    tbNamaPimpinanMitra.Text = pemdaPemkot.NamaPimpinanInstitusi;
                    tbJabatan.Text = pemdaPemkot.Jabatan;
                    tbAlamatInstitusi.Text = pemdaPemkot.AlamatInstitusi;
                    tbPendanaanThn1.Text = pemdaPemkot.DanaTahun1.ToString();
                    if (ViewState["idSkema"].ToString() != "15")
                    {
                        tbPendanaanThn2.Text = pemdaPemkot.DanaTahun2.ToString();
                        tbPendanaanThn3.Text = pemdaPemkot.DanaTahun3.ToString();
                    } else
                    {
                        lblPendanaanThn2.Visible = false;
                        lblPendanaanThn3.Visible = false;
                        tbPendanaanThn2.Visible = false;
                        tbPendanaanThn3.Visible = false;
                        tbPendanaanThn2.Text = "0";
                        tbPendanaanThn3.Text = "0";
                    }
                    ddlTipeMitra.SelectedValue = pemdaPemkot.IdTipeMitra.ToString();
                }
            }

            if (ViewState["idSkema"].ToString() == "28")
            {
                lblPendanaanThn2.Visible = false;
                tbPendanaanThn2.Visible = false;
                lblPendanaanThn3.Visible = false;
                tbPendanaanThn3.Visible = false;
                tbPendanaanThn2.Text = "0";
                tbPendanaanThn3.Text = "0";
            }
        }

        private void skema()
        {
            DataTable dt = new DataTable();
            objMitraAbdimas.getMitraPengabdianPerSkema(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            ViewState["idSkema"] = dt.Rows[0]["id_skema"].ToString();
            ViewState["lama_kegiatan"] = dt.Rows[0]["lama_kegiatan"].ToString();
        }

        public bool Simpan()
        {
            List<string> emptyField = new List<string>();
            if (ddlTipeMitra.SelectedValue == "0") emptyField.Add("Tipe Mitra");
            if (tbNamaMitra.Text.Trim().Length == 0) emptyField.Add("Nama Lembaga");
            if (tbNamaPimpinanMitra.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan");
            if (tbAlamatInstitusi.Text.Trim().Length == 0) emptyField.Add("Alamat Mitra");
            if (tbJabatan.Text.Trim().Length == 0) emptyField.Add("Jabatan");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            if (tbPendanaanThn1.Text == "")
            {
                tbPendanaanThn1.Text = "0";
            };
            if (tbPendanaanThn2.Text == "")
            {
                tbPendanaanThn2.Text = "0";
            };
            if (tbPendanaanThn3.Text == "")
            {
                tbPendanaanThn3.Text = "0";
            };

            var pemdaPemkot = new PemdaPemkot();
            pemdaPemkot.IdMitraAbdimas = Guid.Parse(ViewState["idMitra"].ToString());
            pemdaPemkot.IdUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            pemdaPemkot.KdKategoriMitra = KD_KATEGORI_MITRA_PELAKSANA;
            pemdaPemkot.IdTipeMitra = int.Parse(ddlTipeMitra.SelectedValue);
            pemdaPemkot.NamaPimpinanInstitusi = tbNamaPimpinanMitra.Text;
            pemdaPemkot.Jabatan = tbJabatan.Text;
            pemdaPemkot.NamaOrganisasiInstitusi = tbNamaMitra.Text;
            pemdaPemkot.AlamatInstitusi = tbAlamatInstitusi.Text;
            pemdaPemkot.DanaTahun1 = decimal.Parse(tbPendanaanThn1.Text.Replace(".", ""));
            pemdaPemkot.DanaTahun2 = decimal.Parse(tbPendanaanThn2.Text.Replace(".", ""));
            pemdaPemkot.DanaTahun3 = decimal.Parse(tbPendanaanThn3.Text.Replace(".", ""));
            pemdaPemkot.LamaKegiatan = int.Parse(ViewState["lama_kegiatan"].ToString());

            if (!objMitraAbdimas.insupPemdaPemkot(pemdaPemkot))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitraAbdimas.errorMessage);
                return false;
            }


            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan mitra berhasil");
            return true;
        }

        private void isiDDLTipeMitra()
        {
            if (IsTipeMono)
            {
                ddlTipeMitra.Items.Add(new ListItem
                {
                    Text = "Pemda/Pemkot",
                    Value = "3",
                    Selected = true
                });
            }
            else
            {
                Models.Pengusul.mitraAbdimas modelAbdimas = new Models.Pengusul.mitraAbdimas();
                var dt = new DataTable();

                if (modelAbdimas.listTipeMitraPelaksanaPPK(ref dt))
                {
                    ddlTipeMitra.AppendDataBoundItems = true;
                    ddlTipeMitra.Items.Clear();
                    ddlTipeMitra.Items.Add(new ListItem { Text = "-- Pilih Tipe Mitra --", Value = "-1", Selected = true });
                    ddlTipeMitra.DataSource = dt;
                    ddlTipeMitra.DataBind();
                }
            }
        }
    }
}