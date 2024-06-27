using simlitekkes.Models.Sistem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using System.IO;
using simlitekkes.Models.report;

namespace simlitekkes.UserControls.Pengusul.perbaikanProposalAbdimas
{
    public partial class rekapLuaranPerbaikanAbdimas : System.Web.UI.UserControl
    {
        simlitekkes.Models.Pengusul.luaran objLuaran = new simlitekkes.Models.Pengusul.luaran();
        simlitekkes.Models.Pengusul.PerbaikanUsulan objPerbaikan = new simlitekkes.Models.Pengusul.PerbaikanUsulan();

        Models.login objLogin;
        Core.manipulasiData objManipData = new Core.manipulasiData();
        uiNotify noty = new uiNotify();

        pdfUsulanBaru objMdlPdfusulanBaru = new pdfUsulanBaru();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void init(string idUsulanKegiatan, string thnPelaksanaanKegiatan)
        {
            ViewState["idUsulanKegiatan"] = idUsulanKegiatan;
            ViewState["thnPelaksanaanKegiatan"] = thnPelaksanaanKegiatan;
            isiLuaran(idUsulanKegiatan);

            mvMain.SetActiveView(vDaftarUsulanLuaran);
            isiGvLuaranDicapai(idUsulanKegiatan);
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvLuaranDicapai(ViewState["idUsulanKegiatan"].ToString());
        }

        private void isiLuaran(string pidUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            objPerbaikan.GetLuaranWajib2024(ref dt, Guid.Parse(pidUsulanKegiatan));
            rptLuaranWajib.DataSource = dt;
            rptLuaranWajib.DataBind();

            dt.Clear();
            dt = new DataTable();
            objPerbaikan.GetLuaranTambahan2024(ref dt, Guid.Parse(pidUsulanKegiatan));
            rptLuaranTambahan.DataSource = dt;
            rptLuaranTambahan.DataBind();
        }

        private void isiGvLuaranDicapai(string pidUsulanKegiatan)
        {
            objLogin = (Models.login)Session["objLogin"];
            DataTable dt = new DataTable();
            Guid idPersonal = Guid.Parse(objLogin.idPersonal);
            objLuaran.rekapLuaranPerbaikanAbdimas(ref dt, idPersonal, ViewState["thnPelaksanaanKegiatan"].ToString());

            var dr1 = dt.Select("id_usulan_kegiatan='" + pidUsulanKegiatan + "'");
            DataTable dt3 = dr1.CopyToDataTable();
            gvLuaranDicapai.DataSource = dt3;
            gvLuaranDicapai.DataBind();
        }

        protected void gvLuaranDicapai_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            string id_usulan_kegiatan = gvLuaranDicapai.DataKeys[row.RowIndex]["id_usulan_kegiatan"].ToString();
            switch (e.CommandName)
            {
                //case "ubah_luaran_wajib":
                //    ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
                //    ViewState["id_kelompok"] = "1";
                //    lblKelompokLuaran.Text = "Luaran Wajib";
                //    mvMain.SetActiveView(vKelengkapanLuaran);
                //    isiGvLuaranDetail();
                //    break;
                //case "ubah_luaran_tambahan":
                //    ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
                //    ViewState["id_kelompok"] = "2";
                //    lblKelompokLuaran.Text = "Luaran Tambahan";
                //    mvMain.SetActiveView(vKelengkapanLuaran);
                //    isiGvLuaranDetail();
                //    break;
            }
        }

    }
}