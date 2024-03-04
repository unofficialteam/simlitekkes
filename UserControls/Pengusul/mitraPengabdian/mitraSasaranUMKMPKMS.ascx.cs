using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraSasaranUMKMPKMS : System.Web.UI.UserControl
    {
        Models.Pengusul.mitraAbdimas modelAbdimas = new Models.Pengusul.mitraAbdimas();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitra)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            if (idUsulanKegiatan == Guid.Empty)
            {
                // data baru
                
            }
            else
            {
                // load data
                
            }

            skema();
            isiDDLTipeMitra();
            isiDDLJenisMitra();
            isiDDLProvinsi();
            ddlKota.Enabled = false;
            ddlKecamatan.Enabled = false;
            ddlDesa.Enabled = false;
            if(ViewState["idSkema"].ToString() == "28")
            {
                lblPendanaanThn2.Visible = false;
                tbPendanaanThn2.Visible = false;
                lblPendanaanThn3.Visible = false;
                tbPendanaanThn3.Visible = false;
            }
        }

        private void skema()
        {
            DataTable dt = new DataTable();
            modelAbdimas.getMitraPengabdianPerSkema(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            ViewState["idSkema"] = dt.Rows[0]["id_skema"].ToString();
        }

        public bool Simpan()
        {

            return true;
        }

        private void isiDDLTipeMitra()
        {           
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (modelAbdimas.listTipeMitraSasaranUMKM(ref dt))
            {
                ddlTipeMitra.AppendDataBoundItems = true;
                ddlTipeMitra.Items.Clear();
                ddlTipeMitra.Items.Add(new ListItem { Text = "-- Pilih Tipe Mitra --", Value = "-1", Selected = true });
                ddlTipeMitra.DataSource = dt;
                ddlTipeMitra.DataBind();
            }
        }

        private void isiDDLJenisMitra()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (modelAbdimas.listJenisMitra(ref dt))
            {
                ddlJenisMitra.AppendDataBoundItems = true;
                ddlJenisMitra.Items.Clear();
                ddlJenisMitra.Items.Add(new ListItem { Text = "-- Pilih Jenis Mitra --", Value = "-1", Selected = true });
                ddlJenisMitra.DataSource = dt;
                ddlJenisMitra.DataBind();                
            }
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
            modelAbdimas.getDaerahPrioritas(ref dt, ddlDesa.SelectedValue, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            
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