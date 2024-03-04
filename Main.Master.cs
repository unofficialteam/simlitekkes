using simlitekkes.Core;
using simlitekkes.Models;
using simlitekkes.UIControllers;
using System;
using System.Data;

namespace simlitekkes
{
    public partial class Main : System.Web.UI.MasterPage
    {
        login objLogin;        
        uiNotify noty = new uiNotify();
        manipulasiData objManipData = new manipulasiData();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null) Response.Redirect("Login.aspx");

            objLogin = (login)Session["objLogin"];
            if (Session["IdPeranAktif"] == null) Session["IdPeranAktif"] = objLogin.idPeran;
            //SessionTimeoutCounter();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var idPeran = Convert.ToInt32(Session["IdPeranAktif"].ToString());

            prosesMenu(idPeran);
            lblUsername.Text = objLogin.userName;
            if (!IsPostBack)
            {
                lblNamaPengguna.Text = objLogin.namaLengkap;
                lblNamaPeran.Text = objLogin.namaPeran;

                IsiRblPilihPeran();
                //refreshFoto();
            }
            else
            {

            }

        }

        private void IsiRblPilihPeran()
        {
            var idPersonal = Guid.Parse(objLogin.idPersonal);
            var idPeranAktif = Convert.ToInt32(Session["IdPeranAktif"].ToString());

            uiRadioButtonList obj_uiRadioButtonList = new uiRadioButtonList();
            DataTable dtGantiPeran = new DataTable();
            if (objLogin.listGantiPeran(ref dtGantiPeran, idPersonal, idPeranAktif))
            {
                if (dtGantiPeran.Rows.Count > 0)
                {
                    obj_uiRadioButtonList.bindToRadioButtonList(ref rblPeran, dtGantiPeran, "nama_peran", "id_peran");
                }
                else
                {
                    lblKetGantiPeran.Text = "Tidak ada peran selain " + objLogin.namaPeran;
                    panelKetGantiPeran.Visible = true;
                }
            }
        }

        private void prosesMenu(int idPeran)
        {
            switch (idPeran)
            {
                case 1:     // Administrator
                    cphMenu.Controls.Add(LoadControl("~/UserControls/Admin/menu.ascx"));
                    break;
                case 9:     // Administrator
                case 10:     // Administrator
                    cphMenu.Controls.Add(LoadControl("~/UserControls/KepalaLembaga/menu.ascx"));
                    break;
                case 37:     // Pengusul
                    cphMenu.Controls.Add(LoadControl("~/UserControls/Pengusul/menu.ascx"));
                    loadJmlKonfirmasi();
                    lblJmlKonfirmasiPersetujuan.Visible = true;
                    lbKonfirmasi.Visible = true;
                    break;
                case 39:    // Pengusul Non Dosen KemenRistekDikti
                    cphMenu.Controls.Add(LoadControl("~/UserControls/Pengusul/menu.ascx"));
                    //Models.Pengusul.konfirmasiPersetujuan objDaftarKonfirmasi = new Models.Pengusul.konfirmasiPersetujuan();
                    loadJmlKonfirmasi();
                    lblJmlKonfirmasiPersetujuan.Visible = true;
                    lbKonfirmasi.Visible = true;
                    break;

                case 55:     // Operator Penelitian Pusdik Kemkes
                case 56:     // Operator Pengabdian Pusdik Kemkes
                    cphMenu.Controls.Add(LoadControl("~/UserControls/OperatorPenelitianPusdik/menu.ascx"));
                    break;
                case 58:     // Operator Ditlitabmas - Program dan Evaluasi
                case 59:     // Operator Ditlitabmas - HKIP
                case 38:     // Operator Ditlitabmas
                    cphMenu.Controls.Add(LoadControl("~/UserControls/operatorPenelitian/menu.ascx"));
                    break;
                case 57:     // IT Support
                    cphMenu.Controls.Add(LoadControl("~/UserControls/optRisbang/menu.ascx"));
                    break;
                case 6:     // Opt. PT Penelitian
                    cphMenu.Controls.Add(LoadControl("~/UserControls/OperatorPT/menu.ascx"));
                    break;
                case 40:     // Opt. PT Pengabdian
                    cphMenu.Controls.Add(LoadControl("~/UserControls/OperatorPT/menuAbdimas.ascx"));
                    break;
                case 4:     // Reviewer Nasional
                    cphMenu.Controls.Add(LoadControl("~/UserControls/Reviewer/menu.ascx"));
                    break;
                case 76:     // Opt. Risbang Pengolah Data
                    cphMenu.Controls.Add(LoadControl("~/UserControls/optRisbangPengolahData/menu.ascx"));
                    break;
                case 7:     // Reviewer pt
                    cphMenu.Controls.Add(LoadControl("~/UserControls/Reviewer/menu.ascx"));
                    break;
                case 36:     // Opt. Insinas
                    cphMenu.Controls.Add(LoadControl("~/UserControls/optInsinas/menu.ascx"));
                    break;
                case 54: //Manajemen Ditlitabmas -Kasubdit
                case 61: //Manajemen Ditlitabmas -Kasubdit HKIP
                case 62: //Manajemen Ditlitabmas -Kasubdit Penelitian
                    cphMenu.Controls.Add(LoadControl("~/UserControls/ManajemenKasubdit/menu.ascx"));
                    break;
                case 63: //Manajemen Ditlitabmas -Kasubdit PPM
                    cphMenu.Controls.Add(LoadControl("~/UserControls/ManajemenKasubdit/menu.ascx"));
                    break;
                case 64: //Manajemen Ditlitabmas -Kasubdit Program Evaluasi
                case 82: //Opt.Ditlitabmas - Kasi HKIP
                case 81: //Opt.Ditlitabmas - Kasi PE
                case 78: //Opt.Ditlitabmas - Kasi Penelitian
                case 79: //Opt.Ditlitabmas - Kasi Pengabdian
                    cphMenu.Controls.Add(LoadControl("~/UserControls/manajemen/menu.ascx"));
                    break;
                case 70: //Manajemen Kopertis - Pimpinan
                    cphMenu.Controls.Add(LoadControl("~/UserControls/manajemenKopertis/menu.ascx"));
                    break;
                case 5: //Manajemen PT - Pimpinan
                    cphMenu.Controls.Add(LoadControl("~/UserControls/manajemenPT/menu.ascx"));
                    break;
                case 90: // BPK / Irjen
                    cphMenu.Controls.Add(LoadControl("~/UserControls/pemeriksa/menu.ascx"));
                    break;
                case 2:     //Manajemen Ditlitabmas - Direktur
                    cphMenu.Controls.Add(LoadControl("~/UserControls/manajemen/menu.ascx"));
                    break;
                case 87:     // Operator Seleksi Administrasi
                    cphMenu.Controls.Add(LoadControl("~/UserControls/seleksiAdministrasi/menu.ascx"));
                    break;
                case 50:    //Operator Penelitian Kopertis
                case 51:    //Operator Pengabdian Kopertis
                    cphMenu.Controls.Add(LoadControl("~/UserControls/OperatorLLDikti/menu.ascx"));
                    break;
                case 94:     // Verifikator Pendaftaran Reviewer
                    cphMenu.Controls.Add(LoadControl("~/UserControls/VerifikatorReviewer/menu.ascx"));
                    break;
            }
        }

        protected void rblPeran_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPeranGanti = int.Parse(rblPeran.SelectedValue); 
            Session["IdPeranAktif"] = idPeranGanti;
            DataTable dtUserPassword = new DataTable();
            if (objLogin.getUserPasswordByPersonal(ref dtUserPassword, Guid.Parse(objLogin.idPersonal)))
            {
                if (dtUserPassword.Rows.Count > 0)
                {
                    string nama_user = dtUserPassword.Rows[0]["nama_user"].ToString();
                    string password = dtUserPassword.Rows[0]["pswd"].ToString();

                    if (objLogin.autentifikasi(nama_user, password, idPeranGanti))
                    {
                        prosesMenu(idPeranGanti);
                        Session["page"] = 1;
                        Response.Redirect("~/main.aspx");
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada User dan password");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Username atau password tidak sesuai.");
            }

        }

        private void loadJmlKonfirmasi()
        {
            Models.Pengusul.konfirmasiPersetujuan objDaftarKonfirmasi = new Models.Pengusul.konfirmasiPersetujuan();
            Guid id_personal = Guid.Parse(objLogin.idPersonal);
            DataTable dtJmlKonfirmasi = new DataTable();

            if (objDaftarKonfirmasi.getJmlRecords(ref dtJmlKonfirmasi, id_personal))
                lblJmlKonfirmasiPersetujuan.Text = dtJmlKonfirmasi.Rows[0]["jml_konfirmasi"].ToString();
        }

        protected void lbKonfirmasi_Click(object sender, EventArgs e)
        {
            Session["page"] = 22;
            Response.Redirect("Main.aspx");
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void lbGantiPassword_Click(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalGantiPassword");
        }

        protected void lbSimpanGantiPassword_Click(object sender, EventArgs e)
        {
            Guid id_personal = Guid.Parse(objLogin.idPersonal);
            string passwordLama = tbPasswordLama.Text.Trim();
            string passwordBaru = (tbPasswordBaru.Text.Trim()).Replace(" ", "").Replace("'", "").Replace(";", "");
            string passwordBaruOk = objManipData.removeUnicode(passwordBaru);
            string konfirmasiPasswordBaru = (tbKonfirmasiPasswordBaru.Text.Trim()).Replace(" ", "").Replace("'", "").Replace(";", "");
            string konfirmasiPasswordBaruOk = objManipData.removeUnicode(konfirmasiPasswordBaru);

            if (passwordBaruOk.Length >= 6 || konfirmasiPasswordBaruOk.Length >= 6)
            {
                if (passwordBaru == konfirmasiPasswordBaru)
                {
                    DataTable dtGantiPassword = new DataTable();

                    login objGantiPassword = new login();

                    if (objLogin.cekPasswordLama(id_personal, passwordLama) == true)
                    {
                        if (objGantiPassword.updatePassword(id_personal, passwordBaruOk))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Proses penggantian password berhasil....");
                            clearTextBoxGantiPassword();
                        }
                        else
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Kesalahan", "Proses penggantian password gagal " + objGantiPassword.errorMessage);
                            clearTextBoxGantiPassword();
                        }
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Kesalahan", "Password lama tidak sesuai.");
                        clearTextBoxGantiPassword();
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Kesalahan", "Proses penggantian password gagal, konfirmasi password tidak sesuai..!!");
                    clearTextBoxGantiPassword();
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Kesalahan", "Password tidak boleh kurang dari 6 karakter.");
                clearTextBoxGantiPassword();
            }
        }

        protected void lbBatalGantiPassword_Click(object sender, EventArgs e)
        {
            clearTextBoxGantiPassword();
        }

        private void clearTextBoxGantiPassword()
        {
            tbPasswordLama.Text = string.Empty;
            tbPasswordBaru.Text = string.Empty;
            tbKonfirmasiPasswordBaru.Text = string.Empty;
        }
    }
}