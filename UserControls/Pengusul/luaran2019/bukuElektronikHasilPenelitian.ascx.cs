using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.luaran2019
{
    public partial class bukuElektronikHasilPenelitian : System.Web.UI.UserControl
    {
        Models.Pengusul.luaran objLuaran = new Models.Pengusul.luaran();
        uiNotify noty = new uiNotify();
        public event EventHandler OnChildEventBatal;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void init(string kd_kategori_jenis_luaran, string id_usulan, string tahun_ke, int id_skema, string idKelompokLuaran, string idKelompokLuaranRiil = "0")
        {
            ViewState["id_usulan"] = id_usulan;
            ViewState["tahun_ke"] = tahun_ke;
            ViewState["kd_kategori_jenis_luaran"] = kd_kategori_jenis_luaran;
            ViewState["id_skema"] = id_skema;
            ViewState["id_kelompok_luaran"] = idKelompokLuaran;
            ViewState["id_kelompok_luaran_riil"] = idKelompokLuaranRiil;
            lblThnKe.Text = tahun_ke;

            DataTable dt = new DataTable();
            objLuaran.ListJenisLuaran2(ref dt, kd_kategori_jenis_luaran, id_skema, int.Parse(idKelompokLuaran));
            ddlJenisLuaran.Items.Clear();
            ddlJenisLuaran.AppendDataBoundItems = true;
            ddlJenisLuaran.Items.Add(new ListItem("-- Pilih --", "0"));
            ddlJenisLuaran.DataSource = dt;
            ddlJenisLuaran.DataBind();
            ddlJenisLuaran.Enabled = true;
            tbRencanaNamaPerbit.Text = "";

            DataTable dt2 = new DataTable();
            objLuaran.ListTargetStatusLuaran(ref dt2, kd_kategori_jenis_luaran);
            ddlStatus.DataSource = dt2;
            ddlStatus.DataBind();
            if (dt2.Rows.Count > 0)
            {
                ddlStatus.ClearSelection();
                ddlStatus.Items.FindByValue(dt2.Rows[0]["id_target_capaian_luaran"].ToString()).Selected = true;
                isiInfoBuktiLuaran();
            }
        }

        public void setData(Guid p_id_luaran_dijanjikan, string tahun_ke,
            string p_id_jenis_luaran = "", string kd_kategori_jenis_luaran = "")
        {
            ViewState["id_luaran_dijanjikan"] = p_id_luaran_dijanjikan;
            ViewState["id_jenis_luaran"] = p_id_jenis_luaran;
            lblThnKe.Text = tahun_ke;
            tbRencanaNamaPerbit.Text = "";
            if (p_id_jenis_luaran != "") // untuk update data
            {
                ddlJenisLuaran.ClearSelection();
                ddlJenisLuaran.Items.FindByValue(p_id_jenis_luaran.ToString()).Selected = true;
                ddlJenisLuaran.Enabled = false;
                DataTable dt = new DataTable();
                objLuaran.getLuaranDijanjikan(ref dt, p_id_luaran_dijanjikan);
                if (dt.Rows.Count > 0)
                {
                    tbRencanaNamaPerbit.Text = dt.Rows[0]["keterangan"].ToString();
                    ddlStatus.ClearSelection();
                    ddlStatus.Items.FindByValue(dt.Rows[0]["id_target_capaian_luaran"].ToString()).Selected = true;
                    isiInfoBuktiLuaran();
                }
            }
            else
            {
                ddlJenisLuaran.ClearSelection();
                ddlJenisLuaran.SelectedIndex = 0;
                ddlJenisLuaran.Enabled = true;
            }

        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            Guid p_id_luaran_dijanjikan = Guid.Empty;
            if (ViewState["id_luaran_dijanjikan"] != null)
                p_id_luaran_dijanjikan = Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString());
            Guid p_id_usulan = Guid.Parse(ViewState["id_usulan"].ToString());

            int p_id_kelompok_luaran = int.Parse(ViewState["id_kelompok_luaran"].ToString()); // 1=wajib, 2=tambahan

            if (ViewState["id_kelompok_luaran_riil"].ToString() != "0")
                p_id_kelompok_luaran = int.Parse(ViewState["id_kelompok_luaran_riil"].ToString());

            int tahun_ke = int.Parse(ViewState["tahun_ke"].ToString());

            string new_id_luaran_dijanjikan = string.Empty;
            if (objLuaran.insupLuaranWajibDijanjikanDasar_xii(
                //ref new_id_luaran_dijanjikan,
                p_id_luaran_dijanjikan,
              p_id_usulan,
              int.Parse(ddlJenisLuaran.SelectedValue),
              p_id_kelompok_luaran,
              tahun_ke,
              int.Parse(ddlStatus.SelectedValue),
              new string[] { tbRencanaNamaPerbit.Text }
            ))
            {
                ViewState["id_luaran_dijanjikan"] = new_id_luaran_dijanjikan;
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                       "Simpan luaran buku elektronik hasil penelitian berhasil.");
                lbBatal.Text = "Kembali";
                if (OnChildEventBatal != null)
                    OnChildEventBatal(sender, null);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Simpan luaran buku elektronik hasil penelitian gagal. Silakan hubungi administrator.");
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            if (OnChildEventBatal != null)
                OnChildEventBatal(sender, null);
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiInfoBuktiLuaran();
        }

        private void isiInfoBuktiLuaran()
        {
            DataTable dt = new DataTable();
            objLuaran.ListInfoBuktiLuaran(ref dt, ddlJenisLuaran.SelectedValue,
                int.Parse(ViewState["tahun_ke"].ToString()),
                int.Parse(ddlStatus.SelectedValue),
                int.Parse(ViewState["id_skema"].ToString()),
                int.Parse(ViewState["id_kelompok_luaran"].ToString())
                );
            if (dt.Rows.Count > 0)
            {
                string info = "";
                info += dt.Rows[0]["get_info_bukti_luaran_26_juli"].ToString();

                lblInfoBuktiLuaran.Text = info;
            }
        }


    }
}