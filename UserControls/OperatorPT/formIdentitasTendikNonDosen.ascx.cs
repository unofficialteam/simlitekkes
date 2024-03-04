using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class formIdentitasTendikNonDosen : System.Web.UI.UserControl
    {
        daftarTendikNonDosen modelTendikNonDosen = new daftarTendikNonDosen();
        Models.login objLogin;

        uiNotify noty = new uiNotify();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            if (!IsPostBack)
            {
                isiKota(ddlKdKota);
                if (Session["IdPersonalTendik"] != null)
                {
                    if (Session["IdPersonalTendik"].ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        DataTable result = new DataTable();
                        modelTendikNonDosen.getDetailData(Guid.Parse(Session["IdPersonalTendik"].ToString()), ref result);
                        if (result.Rows.Count > 0)
                        {
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
                else
                    Session["IdPersonalTendik"] = "00000000-0000-0000-0000-000000000000";
            }
        }

        private void isiKota(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelTendikNonDosen.getListKota(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "nama_kota", "kd_kota");
            ddl.SelectedIndex = 0;
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            returnBack();
        }

        private void returnBack()
        {
            Session["IdPersonalTendik"] = "00000000-0000-0000-0000-000000000000";
            Session["page"] = 43;
            Response.Redirect("Main.aspx");
        }

        protected void lbSimpanData_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            modelTendikNonDosen.cekNoKTP(ref dt, tbNomorKtp.Text);
            if(dt.Rows.Count > 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Nomor KTP sudah digunakan " + dt.Rows[0]["identitas"].ToString());
                //returnBack();
            }

            else
            {
                if (tbNama.Text.Trim().Equals("") || tbNomorKtp.Text.Trim().Equals("") || tbTempatLahir.Text.Trim().Equals("") || tbTglLahir.Text.Trim().Equals("") || tbAlamat.Text.Trim().Equals("") || tbNomorHp.Text.Trim().Equals("") || tbSurel.Text.Trim().Equals("") || tbBidangKeahlian.Text.Trim().Equals(""))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pastikan semua data yang wajid di isi sudah terisi");
                }
                else
                {
                    if (Session["IdPersonalTendik"].ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        if (modelTendikNonDosen.insertData(objLogin.idInstitusi, tbNama.Text, tbNomorKtp.Text, tbGelarDepan.Text, tbGelarBelakang.Text, ddlKdJenisKelamin.SelectedValue, tbTempatLahir.Text, tbTglLahir.Text, tbAlamat.Text, ddlKdKota.SelectedValue, tbKodePos.Text, tbNomorTelepon.Text, tbNomorHp.Text, tbSurel.Text, tbBidangKeahlian.Text, tbWebsitePersonal.Text))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah Data Berhasil");
                            returnBack();
                        }
                        else
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah Data Gagal " + modelTendikNonDosen.errorMessage);
                        }
                    }
                    else
                    {
                        if (modelTendikNonDosen.updateData(Guid.Parse(Session["IdPersonalTendik"].ToString()), objLogin.idInstitusi, tbNama.Text, tbNomorKtp.Text, tbGelarDepan.Text, tbGelarBelakang.Text, ddlKdJenisKelamin.SelectedValue, tbTempatLahir.Text, tbTglLahir.Text, tbAlamat.Text, ddlKdKota.SelectedValue, tbKodePos.Text, tbNomorTelepon.Text, tbNomorHp.Text, tbSurel.Text, tbBidangKeahlian.Text, tbWebsitePersonal.Text))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Ubah Data Berhasil");
                            returnBack();
                        }
                        else
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Ubah Data Gagal " + modelTendikNonDosen.errorMessage);
                        }
                    }
                }
            }            
        }
    }
}