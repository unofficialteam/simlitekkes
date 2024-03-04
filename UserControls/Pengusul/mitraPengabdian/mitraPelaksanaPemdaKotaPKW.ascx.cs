using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes;
using simlitekkes.Models.Pengusul.Mitra;
using simlitekkes.UIControllers;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraPelaksanaPemdaKotaPKW : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        Models.Pengusul.mitraAbdimas objMitraAbdimas = new Models.Pengusul.mitraAbdimas();

        const int KD_KATEGORI_MITRA_PELAKSANA = 1;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitra)
        {
            isiDDLTipeMitra();

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
                tbPendanaanThn2.Text = string.Empty;
                tbPendanaanThn3.Text = string.Empty;

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
                    tbPendanaanThn2.Text = pemdaPemkot.DanaTahun2.ToString();
                    tbPendanaanThn3.Text = pemdaPemkot.DanaTahun3.ToString();

                    ddlTipeMitra.SelectedValue = pemdaPemkot.IdTipeMitra.ToString();
                }

            }
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
        }

        public bool Simpan()
        {
            List<string> emptyField = new List<string>();
            if (ddlTipeMitra.SelectedValue == "0") emptyField.Add("Tipe Mitra");
            if (tbNamaPimpinanMitra.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan");
            if (tbAlamatInstitusi.Text.Trim().Length == 0) emptyField.Add("Alamat Institusi Mitra");
            if (tbJabatan.Text.Trim().Length == 0) emptyField.Add("Jabatan");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            var pemdaPemkot = new PemdaPemkot();
            pemdaPemkot.IdMitraAbdimas = Guid.Parse(ViewState["idMitra"].ToString());
            pemdaPemkot.IdUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            pemdaPemkot.KdKategoriMitra = KD_KATEGORI_MITRA_PELAKSANA;
            pemdaPemkot.IdTipeMitra = int.Parse(ddlTipeMitra.SelectedValue);
            pemdaPemkot.NamaPimpinanInstitusi = tbNamaPimpinanMitra.Text;
            pemdaPemkot.Jabatan = tbJabatan.Text;
            pemdaPemkot.NamaOrganisasiInstitusi = tbNamaMitra.Text;
            pemdaPemkot.AlamatInstitusi = tbAlamatInstitusi.Text;
            if(ddlTipeMitra.SelectedValue == "3")
            {
                pemdaPemkot.DanaTahun1 = decimal.Parse(0.ToString().Replace(",", "").Replace(".", ""));
            }
            else
            {
                pemdaPemkot.DanaTahun1 = decimal.Parse(tbPendanaanThn1.Text.Replace(",", "").Replace(".", ""));               
            }

            //pemdaPemkot.DanaTahun2 = decimal.Parse(tbPendanaanThn2.Text.Replace(",", "").Replace(".", ""));
            //pemdaPemkot.DanaTahun3 = decimal.Parse(tbPendanaanThn3.Text.Replace(",", "").Replace(".", ""));
            pemdaPemkot.LamaKegiatan = 1;

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
            Models.Pengusul.mitraAbdimas modelAbdimas = new Models.Pengusul.mitraAbdimas();
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (modelAbdimas.listTipeMitraPelaksanaPPK(ref dt))
            {
                ddlTipeMitra.AppendDataBoundItems = true;
                ddlTipeMitra.Items.Clear();
                ddlTipeMitra.Items.Add(new ListItem { Text = "-- Pilih Tipe Mitra --", Value = "-1", Selected = true });
                ddlTipeMitra.DataSource = dt;
                ddlTipeMitra.DataBind();
            }
        }

        protected void ddlTipeMitra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTipeMitra.SelectedValue == "3")
            {
                tbPendanaanThn1.Enabled = false;
            }
            else
            {
                tbPendanaanThn1.Enabled = true;
            }
        }
    }
}