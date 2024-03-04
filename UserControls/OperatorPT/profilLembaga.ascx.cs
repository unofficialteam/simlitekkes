using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class profilLembaga : System.Web.UI.UserControl
    {
        Models.PT.profilLembaga modelProfile = new Models.PT.profilLembaga();

        UIControllers.uiGridView obj_uiGridView = new UIControllers.uiGridView();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        UIControllers.uiDropdownList obj_uiDDL = new UIControllers.uiDropdownList();

        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();
        Models.login objLogin;

        bool StatusForm = false;

        string[] namaKolom = { "id_institusi", "kd_jenis_kegiatan", "no_sk_pendirian", "nama_lembaga", "alamat_lembaga", "no_telepon", "no_fax", "surel", "url", "nama_jabatan", "id_personal_pimpinan" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                refreshGridView();
                kewenanganOperatorPt();
                mvProfile.SetActiveView(vProfile);
            }
        }

        private void kewenanganOperatorPt()
        {
            switch (objLogin.idPeran)
            {
                case 6:    // opt. pt penelitian
                    {
                        lbEdit.Visible = true;
                        lbEdit.Enabled = true;
                    }
                    break;
                case 40:    // opt. pt Pengabdian
                    {
                        lbEdit.Visible = true;
                        lbEdit.Enabled = true;
                    }
                    break;
            }
        }

        private void refreshGridView()
        {
            lbEdit.Visible = true;
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());
            mvProfile.SetActiveView(vProfile);
            loadDataLembaga();
        }

        private void loadDataLembaga()
        {
            clearLabel();

            lbEdit.Visible = true;
            StatusForm = false;
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());

            DataTable dtLembaga = new DataTable();
            if (!modelProfile.getRow(ref dtLembaga, Guid.Parse(objLogin.idInstitusi.ToString()), rbKDJenisKegiatan.SelectedValue))
                lblError.Text = modelProfile.errorMessage;
            dtLembaga = modelProfile.currentRecords;

            if (dtLembaga.Rows.Count > 0)
            {
                lblNoSKPendirian.Text = dtLembaga.Rows[0]["no_sk_pendirian"].ToString();
                lblNamaLembaga.Text = dtLembaga.Rows[0]["nama_lembaga"].ToString();
                lblAlamatLembaga.Text = dtLembaga.Rows[0]["alamat_lembaga"].ToString();
                lblNoTelepon.Text = dtLembaga.Rows[0]["no_telepon"].ToString();
                lblNoFax.Text = dtLembaga.Rows[0]["no_fax"].ToString();
                lblSurel.Text = dtLembaga.Rows[0]["surel"].ToString();
                lblURL.Text = dtLembaga.Rows[0]["url"].ToString();
                lblNamaJabatan.Text = dtLembaga.Rows[0]["nama_jabatan"].ToString();
                lblNidn.Text = dtLembaga.Rows[0]["nidn"].ToString();
                lblNama.Text = dtLembaga.Rows[0]["nama"].ToString();
                lblJenisKelamin.Text = dtLembaga.Rows[0]["jenis_kelamin"].ToString();
                lblJenjangPendidikan.Text = dtLembaga.Rows[0]["jenjang_pendidikan"].ToString();
            }

            lbSimpan.Visible = false;
        }

        private void loadDataEditLembaga()
        {
            lbEdit.Visible = false;
            StatusForm = true;
            Guid id_institusi = Guid.Parse(objLogin.idInstitusi.ToString());

            DataTable dtLembaga = new DataTable();
            if (!modelProfile.getRow(ref dtLembaga, Guid.Parse(objLogin.idInstitusi.ToString()), rbKDJenisKegiatan.SelectedValue))
                lblError.Text = modelProfile.errorMessage;
            dtLembaga = modelProfile.currentRecords;

            if (dtLembaga.Rows.Count > 0)
            {
                rbKDJenisKegiatan.SelectedValue = dtLembaga.Rows[0]["kd_jenis_kegiatan"].ToString();
                tbNoSKPendirian.Text = dtLembaga.Rows[0]["no_sk_pendirian"].ToString();
                tbNamaLembaga.Text = dtLembaga.Rows[0]["nama_lembaga"].ToString();
                tbAlamatLembaga.Text = dtLembaga.Rows[0]["alamat_lembaga"].ToString();
                tbNoTelepon.Text = dtLembaga.Rows[0]["no_telepon"].ToString();
                tbNoFax.Text = dtLembaga.Rows[0]["no_fax"].ToString();
                tbSurel.Text = dtLembaga.Rows[0]["surel"].ToString();
                tbURL.Text = dtLembaga.Rows[0]["url"].ToString();
                tbNamaJabatan.Text = dtLembaga.Rows[0]["nama_jabatan"].ToString();
                tbNidn.Text = dtLembaga.Rows[0]["nidn"].ToString();
                tbNama.Text = dtLembaga.Rows[0]["nama"].ToString();
                tbJenisKelamin.Text = dtLembaga.Rows[0]["jenis_kelamin"].ToString();
                tbJenjangPendidikan.Text = dtLembaga.Rows[0]["jenjang_pendidikan"].ToString();
                if (dtLembaga.Rows[0]["id_personal_pimpinan"].ToString() != "")
                {
                    ViewState["id_personal"] = dtLembaga.Rows[0]["id_personal_pimpinan"].ToString();
                }
            }

            lbSimpan.Visible = true;
        }

        protected void lbEdit_Click(object sender, EventArgs e)
        {
            mvProfile.SetActiveView(vEditProfile);
            loadDataEditLembaga();
        }

        private void clearTexbox()
        {
            //rbKDJenisKegiatan.SelectedValue = "1";
            tbNoSKPendirian.Text = string.Empty;
            tbNamaLembaga.Text = string.Empty;
            tbAlamatLembaga.Text = string.Empty;
            tbNoTelepon.Text = string.Empty;
            tbNoFax.Text = string.Empty;
            tbSurel.Text = string.Empty;
            tbURL.Text = string.Empty;
            tbNamaJabatan.Text = string.Empty;
            tbNidn.Text = string.Empty;
            tbNama.Text = string.Empty;
            tbJenisKelamin.Text = string.Empty;
            tbJenjangPendidikan.Text = string.Empty;
        }

        private void clearLabel()
        {
            lblNoSKPendirian.Text = string.Empty;
            lblNamaLembaga.Text = string.Empty;
            lblAlamatLembaga.Text = string.Empty;
            lblNoTelepon.Text = string.Empty;
            lblNoFax.Text = string.Empty;
            lblSurel.Text = string.Empty;
            lblURL.Text = string.Empty;
            lblNamaJabatan.Text = string.Empty;
            lblNidn.Text = string.Empty;
            lblNama.Text = string.Empty;
            lblJenisKelamin.Text = string.Empty;
            lblJenjangPendidikan.Text = string.Empty;
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            refreshGridView();
            clearTexbox();
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            Guid id_personal_pimpinan = Guid.Parse(ViewState["id_personal"].ToString());

            if (modelProfile.insertDataBaru(Guid.Parse(objLogin.idInstitusi.ToString()), rbKDJenisKegiatan.SelectedValue, tbNoSKPendirian.Text, tbNamaLembaga.Text, tbAlamatLembaga.Text, tbNoTelepon.Text, tbNoFax.Text, tbSurel.Text, tbURL.Text, tbNamaJabatan.Text, id_personal_pimpinan))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                refreshGridView();
                clearTexbox();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal" + modelProfile.errorMessage);
                mvProfile.SetActiveView(vEditProfile);
            }

        }

        protected void lbCek_Click(object sender, EventArgs e)
        {
            DataTable dtPimpinan = new DataTable();
            if (!modelProfile.getRowPimpinanNIDN(ref dtPimpinan, tbNidn.Text.ToString()))
                lblError.Text = modelProfile.errorMessage;
            dtPimpinan = modelProfile.currentRecords;

            if (dtPimpinan.Rows.Count > 0)
            {
                {
                    ViewState["id_personal"] = dtPimpinan.Rows[0]["id_personal"].ToString();
                    tbNidn.Text = dtPimpinan.Rows[0]["nidn"].ToString();
                    tbNama.Text = dtPimpinan.Rows[0]["nama"].ToString();
                    tbJenisKelamin.Text = dtPimpinan.Rows[0]["jenis_kelamin"].ToString();
                    tbJenjangPendidikan.Text = dtPimpinan.Rows[0]["jenjang_pendidikan"].ToString();
                }
            }
        }

        protected void rbKDJenisKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StatusForm)
            {
                mvProfile.SetActiveView(vEditProfile);
                loadDataEditLembaga();
            }
            else
            {
                refreshGridView();
            }
        }
    }
}
