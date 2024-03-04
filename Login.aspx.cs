using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes
{
    public partial class Login : System.Web.UI.Page
    {
        Models.login objLogin = new Models.login();
        uiNotify noty = new uiNotify();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbNamaUser.Text.Length == 0 || tbPassword.Text.Length == 0 ||
    txtCaptcha.Text.Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Nama Pengguna, Password dan Hasil Penjumlahan harus diisi terlebih dahulu !");
                return;
            }

            if (Session["hasil"] == null) Response.Redirect("~/Login.aspx");
            if (txtCaptcha.Text.Equals(Session["hasil"].ToString()))
            {
                string namaUser = tbNamaUser.Text.Trim();
                string psword = tbPassword.Text.Trim();
                string pesanError = String.Empty;
                if (objLogin.autentifikasi(namaUser, psword))
                {
                    Session["objLogin"] = objLogin;
                    Session.Remove("hasil");
                    Session["waktu_awal_login"] = DateTime.Now;

                    //Cek jika bukan PT dibawah Kementerian kesehatan
                    int digitDepanKodePT = 0; // int.Parse(objLogin.kdInstitusi.Substring(0, 1));

                    string StringdigitDepanKodePT = objLogin.kdInstitusi.ToString();

                    if (StringdigitDepanKodePT == "")
                    {
                        digitDepanKodePT = 1;
                    }
                    else
                    {
                        digitDepanKodePT = int.Parse(objLogin.kdInstitusi.Substring(0, 1));
                    }

                    //if (digitDepanKodePT != 4)
                    //{
                        //if (digitDepanKodePT == 9)
                        //{
                            Session["page"] = 1;
                            Response.Redirect("Main.aspx");
                        //}
                        //else
                        //{
                        //    //if (objLogin.kdInstitusi.Substring(0, 5) != "99999")
                        //    //{
                        //    Session.Remove("objLogin");
                        //    Session.Remove("hasil");
                        //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Mohon maaf, PT Anda tidak dibawah Kementerian Kesehatan");
                        //    return;
                        //    //}
                        //}
                    //}

                    Session["page"] = 1;
                    Response.Redirect("Main.aspx");
                }
                else
                {
                    Session.Remove("hasil");
                    string _pesanError = "Nama Pengguna dan/atau Password tidak sesuai.";
                    if (objLogin.getErrorKoneksi() == null)
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", _pesanError);
                    }
                    else
                    {
                        _pesanError = objLogin.getErrorKoneksi();
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", _pesanError);
                    }
                }
            }
            else
            {
                Session.Remove("hasil");
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hasil penjumlahan tidak sesuai.");
            }
        }
    }
}