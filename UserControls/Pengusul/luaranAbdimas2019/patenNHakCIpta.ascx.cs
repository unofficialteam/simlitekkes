using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace simlitekkes.UserControls.Pengusul.luaranAbdimas2019
{
    public partial class patenNHakCIpta : System.Web.UI.UserControl
    {

        public event EventHandler OnChildEventBatal;

        Models.Pengusul.luaran model = new Models.Pengusul.luaran();
        UIControllers.uiNotify noty = new UIControllers.uiNotify();

        private Guid IdUsulan
        {
            get
            {
                var idUsulan = Convert.ToString(ViewState["IdUsulan"] ?? Guid.Empty);
                return Guid.Parse(idUsulan);
            }
            set { ViewState["IdUsulan"] = value; }
        }

        private string KdKategoriJenisLuaran
        {
            get { return Convert.ToString(ViewState["KdKategoriJenisLuaran"] ?? string.Empty); }
            set { ViewState["KdKategoriJenisLuaran"] = value; }
        }

        private string IdKelompokLuaran
        {
            get { return Convert.ToString(ViewState["idKelompokLuaran"] ?? string.Empty); }
            set { ViewState["idKelompokLuaran"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //public void Refresh(Guid idUsulan, string kdKategoriJenisLuaran, string idJenisLuaran, string idTargetLuaran, string idKelompokLuaran, int thnKe)
        public void Refresh(Guid idUsulan, string kdKategoriJenisLuaran, string idKelompokLuaran, string thnKe)
        {
            ViewState["tahun_ke"] = thnKe;
            IdUsulan = idUsulan;
            IdKelompokLuaran = idKelompokLuaran;

            DataTable dt = new DataTable();
            model.getKategoriLuaranTambahanAbdimas_xiia(ref dt, idUsulan.ToString());
            refreshddlJenisLuaran(kdKategoriJenisLuaran);
            refreshddlTargetStatusLuaran(kdKategoriJenisLuaran);

            if (dt.Rows.Count > 0)
            {
                selectDdlValue(ref ddlJenisLuaran, dt.Rows[0]["id_jenis_luaran"].ToString());
                selectDdlValue(ref ddlTargetStatusLuaran, dt.Rows[0]["id_target_capaian_luaran"].ToString());

                KdKategoriJenisLuaran = kdKategoriJenisLuaran;
            }


            refreshlvLuaranTambahan();
        }

        private void selectDdlValue(ref DropDownList ddl, string value)
        {
            ListItem item1 = ddl.Items.FindByValue(value);
            if (item1 != null)
            {
                ddl.ClearSelection();
                ddl.Items.FindByValue(value).Selected = true;
            }
        }

        public void refreshddlJenisLuaran(string kdKategoriJenisLuaran)
        {
            ddlJenisLuaran.Items.Clear();
            var dt = new DataTable();
            model.ListJenisLuaranTambahanAbdimas(ref dt, kdKategoriJenisLuaran);
            ddlJenisLuaran.DataSource = dt;
            ddlJenisLuaran.DataBind();

            if (ddlJenisLuaran.Items.Count > 0) ddlJenisLuaran.SelectedIndex = 0;
        }

        public void refreshddlTargetStatusLuaran(string kdKategoriJenisLuaran)
        {
            ddlTargetStatusLuaran.Items.Clear();
            var dt = new DataTable();
            model.ListTargetStatusLuaranTambahanPengabdian_xii(ref dt, kdKategoriJenisLuaran);
            ddlTargetStatusLuaran.DataSource = dt;
            ddlTargetStatusLuaran.DataBind();

            if (ddlTargetStatusLuaran.Items.Count > 0) ddlTargetStatusLuaran.SelectedIndex = 0;
        }

        protected void refreshlvLuaranTambahan()
        {
            var dt = new DataTable();

            if (!model.getLuaranProdukIndustri(ref dt, Convert.ToInt32(ddlJenisLuaran.SelectedValue.Trim()),
                    Convert.ToInt32(ddlTargetStatusLuaran.SelectedValue), IdUsulan,
                    int.Parse(IdKelompokLuaran), int.Parse(ViewState["tahun_ke"].ToString())))
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", model.errorMessage);
                return;
            }

            try
            {
                lvLuaranTambahan.DataSource = dt;
                lvLuaranTambahan.DataBind();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        protected void lvLuaranWajib_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;

            LinkButton lbEdit = new LinkButton();
            Label lblSubstansi = new Label();
            lblSubstansi = (Label)e.Item.FindControl("lblSubstansi");

            switch (KdKategoriJenisLuaran.Trim())
            {
                case "6": // Paten
                    lblSubstansi.Text = "Rencana Substansi paten yang diuji:";
                    break;
                case "7": // Paten Sederhana
                    lblSubstansi.Text = "Rencana Substansi Paten Sederhana yang diuji:";
                    break;
                case "8": // Hak Cipta
                    lblSubstansi.Text = "Rencana Substansi ciptaan yang diuji:";
                    break;
            }
        }

        protected void ddlJenisLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshlvLuaranTambahan();
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            var emptyData = new List<string>();

            for (int i = 0; i < lvLuaranTambahan.Items.Count; i++)
            {
                if (lvLuaranTambahan.DataKeys[i]["is_edit"].ToString() == "1")
                {
                    var tbSubstansi = lvLuaranTambahan.Items[i].FindControl("tbSubstansi") as TextBox;
                    if (tbSubstansi.Text.Trim().Length == 0) emptyData.Add($"Tahun ke {i + 1}");
                }
            }

            if (emptyData.Count > 0)
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Data Substansi berikut harus diisi :<br />" + string.Join(", ", emptyData.ToArray()));
                return;
            }

            // Cek Jenis Luaran berbeda disini...

            // Tulis Kode Simpan Disini.....            

            //if (!model.insertLuaranWajibTerapanPengembangan(int.Parse(ddlJenisLuaran.SelectedValue),
            //        int.Parse(ddlTargetStatusLuaran.SelectedValue), IdUsulan))
            //{
            //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", model.errorMessage);
            //    return;
            //}
            //int jmlSukses = 0;
            for (int i = 0; i < lvLuaranTambahan.Items.Count; i++)
            {
                var substansi = string.Empty;

                if (lvLuaranTambahan.DataKeys[i]["is_edit"].ToString() == "1")
                {
                    var tbSubstansi = lvLuaranTambahan.Items[i].FindControl("tbSubstansi") as TextBox;
                    substansi = tbSubstansi.Text;
                }

                var idLuaranDijanjikan = (string.IsNullOrEmpty(lvLuaranTambahan.DataKeys[i]["id_luaran_dijanjikan"].ToString()) ?
                                                              Guid.NewGuid() : Guid.Parse(lvLuaranTambahan.DataKeys[i]["id_luaran_dijanjikan"].ToString()));
                int tahunKe = Convert.ToInt32(lvLuaranTambahan.DataKeys[i]["tahun_ke"].ToString());
                //var volume = Convert.ToInt32(lvLuaranWajib.DataKeys[i]["volume"].ToString());
                IdKelompokLuaran = "2";
                if (model.insupluaranPengabdian(
                  idLuaranDijanjikan.ToString(),
                  IdUsulan.ToString(),
                  tahunKe.ToString(),
                  ddlJenisLuaran.SelectedValue,
                  ddlTargetStatusLuaran.SelectedValue,
                  "1",
                  IdKelompokLuaran,
                  substansi
                ))
                {
                    if (OnChildEventBatal != null)
                        OnChildEventBatal(sender, null);
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Simpan luaran tambahan berhasil.");
                }
                else
                {
                    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Informasi", "Simpan luaran tambahan gagal. Silakan hubungi administrator");
                }

                //if (IdKelompokLuaran == "1")
                //{
                //    if (!model.insertLuaranWajibTerapanPengembangan(idLuaranDijanjikan, int.Parse(ddlJenisLuaran.SelectedValue),
                //            int.Parse(ddlTargetStatusLuaran.SelectedValue), IdUsulan, volume, substansi, tahunKe, int.Parse(IdKelompokLuaran)))
                //    {
                //        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", model.errorMessage);
                //        return;
                //    }
                //    else
                //    {
                //        jmlSukses++;
                //    }
                //}
                //else if (IdKelompokLuaran == "2")
                //{
                //    //if (!model.insupLuaranWajibDijanjikanDasar_xii(
                //    //  idLuaranDijanjikan,
                //    //  IdUsulan,
                //    //  int.Parse(ddlJenisLuaran.SelectedValue),
                //    //  int.Parse( IdKelompokLuaran),
                //    //  int.Parse(ViewState["tahun_ke"].ToString()),
                //    //  int.Parse(ddlTargetStatusLuaran.SelectedValue),
                //    //  new string[] { substansi }
                //    if (!model.insertLuaranWajibTerapanPengembangan(idLuaranDijanjikan, int.Parse(ddlJenisLuaran.SelectedValue),
                //            int.Parse(ddlTargetStatusLuaran.SelectedValue), IdUsulan, volume, substansi, tahunKe, int.Parse(IdKelompokLuaran)

                //    ))
                //    {
                //        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", model.errorMessage);
                //        return;
                //    }
                //    else
                //    {
                //        if (OnChildEventBatal != null)
                //            OnChildEventBatal(sender, null);
                //    }
                //}
            }

            //if (IdKelompokLuaran == "1" && jmlSukses == lvLuaranWajib.Items.Count)
            //{
            //    if (OnChildEventBatal != null)
            //        OnChildEventBatal(sender, null);
            //    noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Data Luaran Wajib berhasil disimpan...");
            //}

        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            if (OnChildEventBatal != null)
                OnChildEventBatal(sender, null);
        }
    }
}