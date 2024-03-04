using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.luaranAbdimas2019
{
    public partial class publikasiNBuku : System.Web.UI.UserControl
    {

        Models.Pengusul.luaran objLuaran = new Models.Pengusul.luaran();
        public event EventHandler OnChildEventBatal;
        uiNotify noty = new uiNotify();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void init(string kd_kategori_jenis_luaran, string id_usulan, string tahun_ke, int id_skema, string idKelompokLuaran)
        {
            ViewState["id_usulan"] = id_usulan;
            ViewState["tahun_ke"] = tahun_ke;
            ViewState["kd_kategori_jenis_luaran"] = kd_kategori_jenis_luaran;
            ViewState["id_skema"] = id_skema;
            ViewState["id_kelompok_luaran"] = idKelompokLuaran;
            lblThnKe.Text = tahun_ke;

            DataTable dt = new DataTable();
            objLuaran.ListJenisLuaranTambahanAbdimas(ref dt, kd_kategori_jenis_luaran);
            ddlJenisLuaran.Items.Clear();
            ddlJenisLuaran.AppendDataBoundItems = true;
            //ddlJenisLuaran.Items.Add(new ListItem("-- Pilih --", "0"));
            ddlJenisLuaran.DataSource = dt;
            ddlJenisLuaran.DataBind();
            setLabelInfo(kd_kategori_jenis_luaran);
            ddlJenisLuaran.Enabled = true;
            tbRencanaNama.Text = "";
            isiTargetStatusLuaran(kd_kategori_jenis_luaran);
        }

        private void isiTargetStatusLuaran(string kd_kategori_jenis_luaran)
        {
            DataTable dt2 = new DataTable();
            objLuaran.ListTargetStatusLuaranTambahanPengabdian_xii(ref dt2, kd_kategori_jenis_luaran);
            ddlTargetStatusLuaran.DataSource = dt2;
            ddlTargetStatusLuaran.DataBind();
            if (dt2.Rows.Count > 0)
            {
                ddlTargetStatusLuaran.ClearSelection();
                ddlTargetStatusLuaran.Items.FindByValue(dt2.Rows[0]["id_target_capaian_luaran"].ToString()).Selected = true;

                //if(ViewState["id_kelompok_luaran"] != null)
                //{
                //    if(ViewState["id_kelompok_luaran"].ToString()  == "1") // wajib
                isiInfoBuktiLuaran();
                //}
            }
        }

        private void setLabelInfo(string KdKategoriJenisLuaran)
        {

            switch (KdKategoriJenisLuaran.Trim())
            {
                case "1": // Publikasi di Jurnal Internasional
                    lblRencanaNama.Text = "Rencana Nama Jurnal:";
                    break;
                case "2": // Publikasi di prosiding Seminar Internasional
                    lblRencanaNama.Text = "Rencana Nama Conference:";
                    break;
                case "3": // Buku Cetak Hasil Pengabdian
                case "4": // Buku Elektronik hasil Pengabdian
                case "5": // Book Chapter
                    lblRencanaNama.Text = "Rencana Nama Penerbit:";
                    break;
                case "6": // Paten
                    lblRencanaNama.Text = "Rencana Substansi paten yang diuji:";
                    break;
                case "7": // Paten Sederhana
                    lblRencanaNama.Text = "Rencana Substansi Paten Sederhana yang diuji:";
                    break;
                case "8": // Hak Cipta
                    lblRencanaNama.Text = "Rencana Substansi ciptaan yang diuji:";
                    break;
            }
        }

        public void setData(Guid p_id_luaran_dijanjikan,
            string p_id_jenis_luaran = "")
        {
            ViewState["id_luaran_dijanjikan"] = p_id_luaran_dijanjikan;
            ViewState["id_jenis_luaran"] = p_id_jenis_luaran;
            tbRencanaNama.Text = "";
            if (p_id_jenis_luaran != "") // untuk update data
            {
                ddlJenisLuaran.ClearSelection();
                ddlJenisLuaran.Items.FindByValue(p_id_jenis_luaran.ToString()).Selected = true;
                setLabelInfo(ViewState["kd_kategori_jenis_luaran"].ToString());
                ddlJenisLuaran.Enabled = false;
                DataTable dt = new DataTable();
                objLuaran.getLuaranDijanjikan(ref dt, p_id_luaran_dijanjikan);
                if (dt.Rows.Count > 0)
                {
                    tbRencanaNama.Text = dt.Rows[0]["keterangan"].ToString();
                    //if (dt.Rows.Count > 1)
                    //    tbRencanaNamaKonferensi2.Text = dt.Rows[1]["keterangan"].ToString();

                    //if (dt.Rows.Count > 2)
                    //    tbRencanaNamaKonferensi3.Text = dt.Rows[2]["keterangan"].ToString();

                    ddlTargetStatusLuaran.ClearSelection();
                    ddlTargetStatusLuaran.Items.FindByValue(dt.Rows[0]["id_target_capaian_luaran"].ToString()).Selected = true;
                    //if (ViewState["id_kelompok_luaran"] != null)
                    //{
                    isiInfoBuktiLuaran();
                    //}
                }
            }
            else
            {
                ddlJenisLuaran.ClearSelection();
                ddlJenisLuaran.Items.FindByValue("0").Selected = true;
                ddlJenisLuaran.Enabled = true;
            }

        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            if (tbRencanaNama.Text.Trim() == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Ada rencana nama konferensi/prosiding yang belum diisi.");
                return;
            }
            Guid p_id_luaran_dijanjikan = Guid.Empty;
            if (ViewState["id_luaran_dijanjikan"] != null)
                if (ViewState["id_luaran_dijanjikan"].ToString().Trim() != "")
                    p_id_luaran_dijanjikan = Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString());
            Guid p_id_usulan = Guid.Parse(ViewState["id_usulan"].ToString());

            //int p_id_kelompok_luaran = 1; // 1=wajib, 2=tambahan
            int p_id_kelompok_luaran = int.Parse(ViewState["id_kelompok_luaran"].ToString());
            int tahun_ke = int.Parse(ViewState["tahun_ke"].ToString());

            string[] arrRencanaNamaProsiding = new string[] { tbRencanaNama.Text };

            string new_id_luaran_dijanjikan = string.Empty;
            //if (objLuaran.insupLuaranWajibDijanjikanDasar_xii(
            //  p_id_luaran_dijanjikan,
            //  p_id_usulan,
            //  int.Parse(ddlJenisLuaran.SelectedValue),
            //  p_id_kelompok_luaran,
            //  tahun_ke,
            //  int.Parse(ddlTargetStatusLuaran.SelectedValue),
            //  arrRencanaNamaProsiding
            //)
            if (objLuaran.insupluaranPengabdian(
                  p_id_luaran_dijanjikan.ToString(),
                  p_id_usulan.ToString(),
                  lblThnKe.Text,
                  (ddlJenisLuaran.SelectedValue),
                  ddlTargetStatusLuaran.SelectedValue,
                  "1",
                  ViewState["id_kelompok_luaran"].ToString(),
                  tbRencanaNama.Text
                ))
            {
                ViewState["id_luaran_dijanjikan"] = new_id_luaran_dijanjikan;
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                       "Simpan luaran konferensi/prosiding berhasil.");
                //lbBatal.Text = "Kembali";
                if (OnChildEventBatal != null)
                    OnChildEventBatal(sender, null);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Simpan luaran konferensi/prosiding jurnal gagal. Silakan hubungi administrator.");
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            if (OnChildEventBatal != null)
                OnChildEventBatal(sender, null);
        }

        protected void ddlTargetStatusLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiInfoBuktiLuaran();
        }

        private void isiInfoBuktiLuaran()
        {
            //string tahun_ke = "1"; // 2 3 info sama
            DataTable dt = new DataTable();
            objLuaran.ListInfoBuktiLuaranAbdimas(ref dt, ddlJenisLuaran.SelectedValue,
                int.Parse(ddlTargetStatusLuaran.SelectedValue)
                );
            if (dt.Rows.Count > 0)
            {
                string info = "";
                for(int a=0; a<dt.Rows.Count; a++)
                info += dt.Rows[a]["bukti_luaran"].ToString();

                lblInfoBuktiLuaran.Text = info;
            }
        }

        protected void ddlJenisLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiTargetStatusLuaran(ViewState["kd_kategori_jenis_luaran"].ToString());
        }
    }
}