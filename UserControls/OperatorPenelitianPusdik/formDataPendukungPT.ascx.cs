using simlitekkes.Models.DRPM;
using simlitekkes.UIControllers;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class formDataPendukungPT : System.Web.UI.UserControl
    {
        daftarOperatorPT modelData = new daftarOperatorPT();
        Models.login objLogin;

        uiNotify noty = new uiNotify();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiListBox obj_uiListBox = new uiListBox();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            if (!IsPostBack)
            {
                isiKota(ddlKdKota);
                isiKota(ddlKdKotaInstitusiAsal);
                isiInstitusi(ddlIdInstitusiAsal, ddlKdKotaInstitusiAsal.SelectedValue);
                if (Session["IdPersonalDataPendukung"] != null)
                {
                    if (Session["IdPersonalDataPendukung"].ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        DataTable result = new DataTable();
                        //modelData.getDetailData(Guid.Parse(Session["IdPersonalDataPendukung"].ToString()), ref result);
                        if (result.Rows.Count > 0)
                        {
                            this.listBoxIdPeran.ClearSelection();
                            foreach (ListItem item in listBoxIdPeran.Items)
                            {
                                foreach (var idPeran in (Array) result.Rows[0]["id_peran"])
                                {
                                    if (idPeran.ToString() == item.Value)
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }
                            this.tbGelarDepan.Text = result.Rows[0]["gelar_akademik_depan"].ToString();
                            this.tbNama.Text = result.Rows[0]["nama"].ToString();
                            this.tbGelarBelakang.Text = result.Rows[0]["gelar_akademik_belakang"].ToString();
                            this.tbNomorKtp.Text = result.Rows[0]["nomor_ktp"].ToString();
                            this.ddlKdJenisKelamin.SelectedValue = result.Rows[0]["kd_jenis_kelamin"].ToString();
                            this.tbTempatLahir.Text = result.Rows[0]["tempat_lahir"].ToString();
                            DateTime date = DateTime.Parse(result.Rows[0]["tanggal_lahir"].ToString());
                            this.tbTglLahir.Text = date.ToString("yyyy-MM-dd");
                            this.tbAlamat.Text = result.Rows[0]["alamat"].ToString();
                            this.ddlKdKota.SelectedValue = result.Rows[0]["kd_kota"].ToString();
                            this.tbKodePos.Text = result.Rows[0]["kd_pos"].ToString();
                            this.tbNomorTelepon.Text = result.Rows[0]["nomor_telepon"].ToString();
                            this.tbNomorHp.Text = result.Rows[0]["nomor_hp"].ToString();
                            this.tbSurel.Text = result.Rows[0]["surel"].ToString();
                            this.tbBidangKeahlian.Text = result.Rows[0]["bidang_keahlian"].ToString();
                            this.tbWebsitePersonal.Text = result.Rows[0]["website_personal"].ToString();
                        }
                    }
                }
            }
        }

        private void isiInstitusi(DropDownList ddl, string kd_kota)
        {
            DataTable data = new DataTable();
            //modelData.getListIntitusiByKdKota(ref data, kd_kota);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "nama_institusi", "id_institusi");
        }

        private void isiKota(DropDownList ddl)
        {
            DataTable data = new DataTable();
            //modelData.getListKota(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "nama_kota", "kd_kota");
            ddl.SelectedIndex = 0;
        }

        protected void ddlKdKotaInstitusiAsal_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiInstitusi(ddlIdInstitusiAsal, ddlKdKotaInstitusiAsal.SelectedValue);
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            returnBack();
        }

        private void returnBack()
        {
            Session["IdPersonalDataPendukung"] = "00000000-0000-0000-0000-000000000000";
            Session["page"] = 43;
            Response.Redirect("Main.aspx");
        }

        protected void lbSimpanData_Click(object sender, EventArgs e)
        {
            if (Session["IdPersonalDataPendukung"].ToString() == "00000000-0000-0000-0000-000000000000")
            {
                //modelData.insertData(objLogin.idInstitusi, tbNama.Text, tbNomorKtp.Text, tbGelarDepan.Text, tbGelarBelakang.Text, ddlKdJenisKelamin.SelectedValue, tbTempatLahir.Text, tbTglLahir.Text, tbAlamat.Text, ddlKdKota.SelectedValue, tbKodePos.Text, tbNomorTelepon.Text, tbNomorHp.Text, tbSurel.Text, tbBidangKeahlian.Text, tbWebsitePersonal.Text)
                if (true)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Data Berhasil");
                    returnBack();
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Data Gagal " + modelData.errorMessage);
                }
            }
            else
            {
                //modelData.updateData(Guid.Parse(Session["IdPersonalDataPendukung"].ToString()), objLogin.idInstitusi, tbNama.Text, tbNomorKtp.Text, tbGelarDepan.Text, tbGelarBelakang.Text, ddlKdJenisKelamin.SelectedValue, tbTempatLahir.Text, tbTglLahir.Text, tbAlamat.Text, ddlKdKota.SelectedValue, tbKodePos.Text, tbNomorTelepon.Text, tbNomorHp.Text, tbSurel.Text, tbBidangKeahlian.Text, tbWebsitePersonal.Text)
                if (true)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Ubah Data Berhasil");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Ubah Data Gagal " + modelData.errorMessage);
                }
            }
        }
    }
}