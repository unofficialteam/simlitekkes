using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using simlitekkes;
using System.Data;
using simlitekkes.Models.Pengusul.Mitra;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraPelaksanaPT : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        Models.Pengusul.mitraAbdimas objMitraAbdimas = new Models.Pengusul.mitraAbdimas();

        const int KD_KATEGORI_MITRA_PELAKSANA = 1;
        const int ID_PELAKSANA_PT = 1;
        const int ID_SKEMA_PPK = 15;
        const int ID_SKEMA_PPUPIK = 17;
        const int ID_SKEMA_PPDM = 55;
        const decimal DANA_MINIMAL_PPK = 10000000;
        const decimal DANA_MINIMAL_PPUPIK = 30000000;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitra)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            getSkema();
            if (idMitra == Guid.Empty)
            {
                // data baru
                ViewState["isNew"] = true;
                ViewState["idMitra"] = Guid.NewGuid();

                tbNamaPimpinan.Text = string.Empty;
                tbJabatan.Text = string.Empty;
                tbAlamatInstitusi.Text = string.Empty;
                tbPendanaanThn1.Text = string.Empty;
                if (ViewState["idSkema"].ToString() != ID_SKEMA_PPK.ToString() && ViewState["idSkema"].ToString() != "78" && ViewState["idSkema"].ToString() != ID_SKEMA_PPDM.ToString())
                {

                    tbPendanaanThn2.Text = string.Empty;
                    tbPendanaanThn3.Text = string.Empty;
                }
                else
                {
                    lblPendanaanThn2.Visible = false;
                    lblPendanaanThn3.Visible = false;
                    tbPendanaanThn2.Visible = false;
                    tbPendanaanThn3.Visible = false;
                    tbPendanaanThn2.Text = "0";
                    tbPendanaanThn3.Text = "0";
                }
            }
            else
            {
                // load data
                ViewState["idMitra"] = idMitra;

                DataTable dt = new DataTable();
                var pt = new PTPelaksana();
                objMitraAbdimas.getPTPelaksana(ref pt, idMitra);
                if (pt != null)
                {
                    tbNamaPimpinan.Text = pt.NamaPimpinanInstitusi;
                    tbJabatan.Text = pt.Jabatan;
                    tbAlamatInstitusi.Text = pt.AlamatInstitusi;
                    tbPendanaanThn1.Text = pt.DanaTahun1.ToString();
                    if (ViewState["idSkema"].ToString() != ID_SKEMA_PPK.ToString() && ViewState["idSkema"].ToString() != "78" && ViewState["idSkema"].ToString() !=  ID_SKEMA_PPDM.ToString())
                    {
                        tbPendanaanThn2.Text = pt.DanaTahun2.ToString();
                        tbPendanaanThn3.Text = pt.DanaTahun3.ToString();
                    }
                    else
                    {
                        lblPendanaanThn2.Visible = false;
                        lblPendanaanThn3.Visible = false;
                        tbPendanaanThn2.Visible = false;
                        tbPendanaanThn3.Visible = false;
                        tbPendanaanThn2.Text = "0";
                        tbPendanaanThn3.Text = "0";
                    }
                }
            }
        }

        private void getSkema()
        {
            DataTable dt = new DataTable();
            objMitraAbdimas.getMitraPengabdianPerSkema(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            ViewState["idSkema"] = dt.Rows[0]["id_skema"].ToString();
            ViewState["lama_kegiatan"] = dt.Rows[0]["lama_kegiatan"].ToString();
        }

        public bool Simpan()
        {
            List<string> emptyField = new List<string>();
            if (tbNamaPimpinan.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan");
            if (tbAlamatInstitusi.Text.Trim().Length == 0) emptyField.Add("Alamat Institusi Mitra");
            if (tbJabatan.Text.Trim().Length == 0) emptyField.Add("Jabatan");

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

            if (ViewState["idSkema"].ToString() != ID_SKEMA_PPK.ToString() && ViewState["idSkema"].ToString() != "78" && ViewState["idSkema"].ToString() != ID_SKEMA_PPDM.ToString())
            {
                if (tbPendanaanThn2.Text == "")
                {
                    tbPendanaanThn2.Text = "0";
                };
                if (tbPendanaanThn3.Text == "")
                {
                    tbPendanaanThn3.Text = "0";
                };
            }

            var pt = new PTPelaksana();
            pt.IdMitraAbdimas = Guid.Parse(ViewState["idMitra"].ToString());
            pt.IdUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            pt.KdKategoriMitra = KD_KATEGORI_MITRA_PELAKSANA;
            pt.IdTipeMitra = ID_PELAKSANA_PT;
            pt.NamaPimpinanInstitusi = tbNamaPimpinan.Text;
            pt.Jabatan = tbJabatan.Text;
            pt.AlamatInstitusi = tbAlamatInstitusi.Text;


            pt.DanaTahun1 = decimal.Parse(tbPendanaanThn1.Text.Replace(",", "").Replace(".", ""));
            if (ViewState["idSkema"].ToString() != ID_SKEMA_PPK.ToString())
            {
                pt.DanaTahun2 = decimal.Parse(tbPendanaanThn2.Text.Replace(",", "").Replace(".", ""));
                pt.DanaTahun3 = decimal.Parse(tbPendanaanThn3.Text.Replace(",", "").Replace(".", ""));
            }
            pt.LamaKegiatan = int.Parse(ViewState["lama_kegiatan"].ToString());

            if (int.Parse(ViewState["idSkema"].ToString()) == ID_SKEMA_PPK ||
                int.Parse(ViewState["idSkema"].ToString()) == ID_SKEMA_PPDM)
            {
                if (ViewState["idSkema"].ToString() != ID_SKEMA_PPK.ToString() && ViewState["idSkema"].ToString() != "78" && ViewState["idSkema"].ToString() != ID_SKEMA_PPDM.ToString())
                {
                    if (pt.DanaTahun1 < DANA_MINIMAL_PPK || pt.DanaTahun2 < DANA_MINIMAL_PPK || pt.DanaTahun3 < DANA_MINIMAL_PPK)
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Dana minimal tahun ke 1, 2, 3 adalah Rp. " + DANA_MINIMAL_PPK);
                        return false;
                    }
                }
                else
                {
                    if (pt.DanaTahun1 < DANA_MINIMAL_PPK)
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Dana minimal tahun ke 1 adalah Rp. " + DANA_MINIMAL_PPK);
                        return false;
                    }
                }
            }
            else if (int.Parse(ViewState["idSkema"].ToString()) == ID_SKEMA_PPUPIK)
            {
                if (pt.DanaTahun1 < DANA_MINIMAL_PPUPIK || pt.DanaTahun2 < DANA_MINIMAL_PPUPIK || pt.DanaTahun3 < DANA_MINIMAL_PPUPIK)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Dana minimal tahun ke 1, 2, 3 adalah Rp. " + DANA_MINIMAL_PPUPIK);
                    return false;
                }
            }

            if (!objMitraAbdimas.insupPTPelaksana(pt))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitraAbdimas.errorMessage);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan mitra pelaksana PT berhasil");
            }

            return true;
        }
    }
}