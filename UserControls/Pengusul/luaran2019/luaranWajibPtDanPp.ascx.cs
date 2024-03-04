using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace simlitekkes.UserControls.Pengusul.luaran2019
{
    public partial class luaranWajibPtDanPp : System.Web.UI.UserControl
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

        public void Refresh(Guid idUsulan, string kdKategoriJenisLuaran, string idJenisLuaran, 
            string idTargetLuaran, string idKelompokLuaran, int thnKe, 
            int idSkema, string idKelompokLuaranRiil = "0")
        {
            ViewState["tahun_ke"] = thnKe;
            refreshddlJenisLuaran(kdKategoriJenisLuaran);
            refreshddlTargetStatusLuaran(kdKategoriJenisLuaran);

            selectDdlValue(ref ddlJenisLuaran, idJenisLuaran);
            selectDdlValue(ref ddlTargetStatusLuaran, idTargetLuaran);

            IdUsulan = idUsulan;
            KdKategoriJenisLuaran = kdKategoriJenisLuaran;
            IdKelompokLuaran = idKelompokLuaran;
            ViewState["id_kelompok_luaran_riil"] = idKelompokLuaranRiil;
            lbBatal.Visible = false;
            if (IdKelompokLuaran.Trim() == "2")
            {
                lbBatal.Visible = true;
            }

            if(idSkema == 77) // KKS
            {
                lbBatal.Visible = true;
            }

            refreshlvLuaranWajib();
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
            model.ListJenisLuaran(ref dt, kdKategoriJenisLuaran);
            ddlJenisLuaran.DataSource = dt;
            ddlJenisLuaran.DataBind();

            if (ddlJenisLuaran.Items.Count > 0) ddlJenisLuaran.SelectedIndex = 0;
        }

        public void refreshddlTargetStatusLuaran(string kdKategoriJenisLuaran)
        {
            ddlTargetStatusLuaran.Items.Clear();
            var dt = new DataTable();
            model.ListTargetStatusLuaran(ref dt, kdKategoriJenisLuaran);
            ddlTargetStatusLuaran.DataSource = dt;
            ddlTargetStatusLuaran.DataBind();

            if (ddlTargetStatusLuaran.Items.Count > 0) ddlTargetStatusLuaran.SelectedIndex = 0;
        }

        protected void refreshlvLuaranWajib()
        {
            var dt = new DataTable();

            if (!model.getLuaranProdukIndustri(ref dt, Convert.ToInt32(ddlJenisLuaran.SelectedValue),
                    Convert.ToInt32(ddlTargetStatusLuaran.SelectedValue), IdUsulan, 
                    int.Parse(IdKelompokLuaran), int.Parse(ViewState["tahun_ke"].ToString())))
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", model.errorMessage);
                return;
            }

            try
            {
                lvLuaranWajib.DataSource = dt;
                lvLuaranWajib.DataBind();
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

            switch (KdKategoriJenisLuaran)
            {
                case "9 ": // Hak Cipta
                    lblSubstansi.Text = "Rencana Substansi ciptaan yang diuji:";
                    break;
                case "10": // Paten
                    lblSubstansi.Text = "Rencana Substansi paten yang diuji:";
                    break;
                case "11": // Paten Sederhana
                    lblSubstansi.Text = "Rencana Substansi Paten Sederhana yang diuji:";
                    break;
                case "12": // PVT
                    lblSubstansi.Text = "Rencana Multilokasi Pengujian BUSS:";
                    break;
                case "13": // DTLST
                    lblSubstansi.Text = "Rencana Substansi DTLST yang diuji:";
                    break;
                case "14": // Naskah Kebijakan
                    lblSubstansi.Text = "Rencana substansi naskah yang diuji:";
                    break;
                case "15": // Produk Industri
                    lblSubstansi.Text = "Rencana pengujian produk:";
                    break;
                case "P": // Kebijakan
                    lblSubstansi.Text = "Rencana substansi kebijakan dalam advokasi:";
                    break;
                case "18": // Naskah Akademik
                    lblSubstansi.Text = "Rencana substansi kebijakan:";
                    break;
            }
        }

        protected void ddlJenisLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshlvLuaranWajib();
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            var emptyData = new List<string>();

            for (int i = 0; i < lvLuaranWajib.Items.Count; i++)
            {
                if (lvLuaranWajib.DataKeys[i]["is_edit"].ToString() == "1")
                {
                    var tbSubstansi = lvLuaranWajib.Items[i].FindControl("tbSubstansi") as TextBox;
                    if (tbSubstansi.Text.Trim().Length == 0) emptyData.Add($"Tahun ke {i + 1}");
                }
            }

            if (emptyData.Count > 0)
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Data Substansi berikut harus diisi :<br />" + string.Join(", ", emptyData.ToArray()));
                return;
            }

            int jmlSukses = 0;
            for (int i = 0; i < lvLuaranWajib.Items.Count; i++)
            {
                var substansi = string.Empty;

                if (lvLuaranWajib.DataKeys[i]["is_edit"].ToString() == "1")
                {
                    var tbSubstansi = lvLuaranWajib.Items[i].FindControl("tbSubstansi") as TextBox;
                    substansi = tbSubstansi.Text;
                }

                var idLuaranDijanjikan = (string.IsNullOrEmpty(lvLuaranWajib.DataKeys[i]["id_luaran_dijanjikan"].ToString()) ?
                                                              Guid.NewGuid() : Guid.Parse(lvLuaranWajib.DataKeys[i]["id_luaran_dijanjikan"].ToString()));
                var tahunKe = Convert.ToInt32(lvLuaranWajib.DataKeys[i]["tahun_ke"].ToString());
                var volume = Convert.ToInt32(lvLuaranWajib.DataKeys[i]["volume"].ToString());

                if (IdKelompokLuaran == "1")
                {
                    if (ViewState["id_kelompok_luaran_riil"].ToString() != "0")
                        IdKelompokLuaran = ViewState["id_kelompok_luaran_riil"].ToString();

                    if (!model.insertLuaranWajibTerapanPengembangan(idLuaranDijanjikan, int.Parse(ddlJenisLuaran.SelectedValue),
                            int.Parse(ddlTargetStatusLuaran.SelectedValue), IdUsulan,volume , substansi, tahunKe,int.Parse(IdKelompokLuaran)))
                    {
                        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", model.errorMessage);
                        return;
                    }
                    else
                    {
                        jmlSukses++;
                    }
                }
                else if (IdKelompokLuaran == "2")
                {
                    if (ViewState["id_kelompok_luaran_riil"].ToString() != "0")
                        IdKelompokLuaran = ViewState["id_kelompok_luaran_riil"].ToString();

                    if (!model.insertLuaranWajibTerapanPengembangan(idLuaranDijanjikan, int.Parse(ddlJenisLuaran.SelectedValue),
                            int.Parse(ddlTargetStatusLuaran.SelectedValue), IdUsulan, volume, substansi, tahunKe, int.Parse(IdKelompokLuaran)

                    ))
                    {
                        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan", model.errorMessage);
                        return;
                    }
                    else
                    {
                        if (OnChildEventBatal != null)
                            OnChildEventBatal(sender, null);
                    }
                }
            }

            if (IdKelompokLuaran == "1" && jmlSukses == lvLuaranWajib.Items.Count)
            {
                if (OnChildEventBatal != null)
                    OnChildEventBatal(sender, null);
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Data Luaran Wajib berhasil disimpan...");
            }
            
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            if (OnChildEventBatal != null)
                OnChildEventBatal(sender, null);
        }
    }
}