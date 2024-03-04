using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using simlitekkes;
using simlitekkes.Models.Pengusul.Mitra;
using System.Data;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraPelaksanaPTMitra : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        Models.Pengusul.mitraAbdimas objMitraAbdimas = new Models.Pengusul.mitraAbdimas();

        const int KD_KATEGORI_PT_MITRA = 4;
        const int ID_PT_MITRA = 2;

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
            isiDDLNamaPT();

            if (idMitra == Guid.Empty)
            {
                // data baru
                ViewState["isNew"] = true;
                ViewState["idMitra"] = Guid.NewGuid();
                
                tbNamaPimpinan.Text = string.Empty;
                tbJabatan.Text = string.Empty;
                tbAlamatInstitusi.Text = string.Empty;
                tbPendanaanThn1.Text = string.Empty;
                tbPendanaanThn2.Text = string.Empty;
                tbPendanaanThn3.Text = string.Empty;
            }
            else
            {
                // load data
                ViewState["idMitra"] = idMitra;
                
                var ptMitra = new PTMitra();
                objMitraAbdimas.getPTMitra(ref ptMitra, idMitra);
                if (ptMitra != null)
                {
                    tbNamaPimpinan.Text = ptMitra.NamaPimpinanInstitusi;
                    tbJabatan.Text = ptMitra.Jabatan;
                    tbAlamatInstitusi.Text = ptMitra.AlamatInstitusi;
                    //tbPendanaanThn1.Text = ptMitra.DanaTahun1.ToString();
                    //tbPendanaanThn2.Text = ptMitra.DanaTahun2.ToString();
                    //tbPendanaanThn3.Text = ptMitra.DanaTahun3.ToString();
                    ddlPTMitra.SelectedValue = ptMitra.IdInstitusi.ToString();
                }
            }            
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
        }

        public bool Simpan()
        {
            //var isNew = bool.Parse(ViewState["IsNew"].ToString());

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
            
            var ptMitra = new PTMitra();
            ptMitra.IdMitraAbdimas = Guid.Parse(ViewState["idMitra"].ToString());
            ptMitra.IdUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            ptMitra.KdKategoriMitra = KD_KATEGORI_PT_MITRA;
            ptMitra.IdTipeMitra = ID_PT_MITRA;
            ptMitra.NamaPimpinanInstitusi = tbNamaPimpinan.Text;
            ptMitra.Jabatan = tbJabatan.Text;
            ptMitra.AlamatInstitusi = tbAlamatInstitusi.Text;
            ptMitra.IdInstitusi = Guid.Parse(ddlPTMitra.SelectedValue);//"3fea100d-fc25-41a9-b799-ed9aab32a65c"); // sementara dipantek dengan id institusi univ. suralaya
            ptMitra.DanaTahun1 = decimal.Parse(0.ToString().Replace(",", "").Replace(".", ""));
            ptMitra.DanaTahun2 = decimal.Parse(0.ToString().Replace(",", "").Replace(".", ""));
            ptMitra.DanaTahun3 = decimal.Parse(0.ToString().Replace(",", "").Replace(".", ""));
            ptMitra.LamaKegiatan = 3;


            if (!objMitraAbdimas.insupPTMitra(ptMitra))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitraAbdimas.errorMessage);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan mitra pelaksana PT berhasil");
            }

            return true;
        }

        private void isiDDLNamaPT()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (objMitraAbdimas.listNamaPerguruanTinggi(ref dt))
            {
                ddlPTMitra.AppendDataBoundItems = true;
                ddlPTMitra.Items.Clear();
                ddlPTMitra.Items.Add(new ListItem { Text = "-- Pilih Perguruan Tinggi Mitra --", Value = "-1", Selected = true });
                ddlPTMitra.DataSource = dt;
                ddlPTMitra.DataBind();
            }
        }
    }
}