using simlitekkes.Core;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class identitas : System.Web.UI.UserControl
    {
        //Models.PT.daftarSinkronisasiDosen modelSinkronisasi = new Models.PT.daftarSinkronisasiDosen();
        Models.login objLogin;
        Models.Pengusul.berandaPengusul objPengusul = new Models.Pengusul.berandaPengusul();
        Models.Pengusul.daftarTendikNonDosen objTendik = new Models.Pengusul.daftarTendikNonDosen();
        uiNotify noty = new uiNotify();
        uiModal objModal = new uiModal();
        kontrolUnggah ktUnggah = new kontrolUnggah();
        //string cs = ConfigurationManager.ConnectionStrings["simlitabmas_rb@localhost"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("Login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);

            if (!IsPostBack)
            {
                mvIdentitas.SetActiveView(vDaftar);
                isiIdentitasPengusul();
            }
            refreshFotoProfile();
        }

        private void isiIdentitasPengusul()
        {
            DataTable dt = new DataTable();
            if (Session["identitas"] == null)
            {
                objPengusul.getPersonal(ref dt, objLogin.idPersonal);
                Session["identitas"] = dt;
            }
            else
            {
                dt = (DataTable)Session["identitas"];
            }
            if (dt.Rows.Count > 0)
            {
                lblNidn.Text = dt.Rows[0]["nidn"].ToString();

                lblInstitusi.Text = dt.Rows[0]["nama_institusi"].ToString();
                if (objTendik.isTendik(Guid.Parse(objLogin.idPersonal)))
                {
                    prodi_area.Visible = false;
                    jenjang_pendidikan_area.Visible = false;
                    jabatan_akademik_area.Visible = false;
                } else
                {
                    lblProdi.Text = dt.Rows[0]["nama_program_studi"].ToString();
                    lblJenjangPendidikan.Text = dt.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
                    lblJabatanAkademik.Text = dt.Rows[0]["jabatan_fungsional"].ToString();
                }
                lblTempat.Text = dt.Rows[0]["tempat_lahir"].ToString();
                DateTime tanggalLahir = DateTime.Parse(dt.Rows[0]["tanggal_lahir"].ToString());
                lblTglLahir.Text = tanggalLahir.Day + "-" + tanggalLahir.Month + "-" + tanggalLahir.Year;

                lblNoKtp.Text = dt.Rows[0]["nomor_ktp"].ToString();
                lblAlamat.Text = dt.Rows[0]["alamat"].ToString();
                lblNoTelepon.Text = dt.Rows[0]["nomor_telepon"].ToString();
                lblNoHp.Text = dt.Rows[0]["nomor_hp"].ToString();
                lblSurel.Text = dt.Rows[0]["surel"].ToString();
                lblWebPersonal.Text = dt.Rows[0]["website_personal"].ToString();
                //lbWebPersonal.Text = dt.Rows[0]["website_personal"].ToString().Replace("http://", "").Replace("https://", "");
            }
            else
            {

            }
        }

        protected void lbEdit_click(object sender, EventArgs e)
        {
            mvIdentitas.SetActiveView(vEdit);
            isiEditIdentitasPengusul();
        }

        protected void lbWebPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["WebPersonal"].ToString());
        }

        private void isiEditIdentitasPengusul()
        {
            DataTable dt = new DataTable();
            if (Session["identitas"] == null)
            {
                objPengusul.getPersonal(ref dt, objLogin.idPersonal);
                Session["identitas"] = dt;
            }
            else
            {
                dt = (DataTable)Session["identitas"];
            }

            if (dt.Rows.Count > 0)
            {
                tbNomorKtp.Text = dt.Rows[0]["nomor_ktp"].ToString();
                tbAlamat.Text = dt.Rows[0]["alamat"].ToString();
                tbTempatLahir.Text = dt.Rows[0]["tempat_lahir"].ToString();

                DateTime tanggalLahir = DateTime.Parse(dt.Rows[0]["tanggal_lahir"].ToString());
                tbTglLahir.Text = tanggalLahir.Month + "" + tanggalLahir.Day + "" + tanggalLahir.Year;

                tbTglLahir.Text = dt.Rows[0]["tanggal_lahir"].ToString() != "" ?
                DateTime.Parse(dt.Rows[0]["tanggal_lahir"].ToString()).ToString("yyyy-MM-dd") : "";

                tbNoTelepon.Text = dt.Rows[0]["nomor_telepon"].ToString();
                tbNoHp.Text = dt.Rows[0]["nomor_hp"].ToString();
                tbAlamatSurel.Text = dt.Rows[0]["surel"].ToString();
                tbWebsitePersonal.Text = dt.Rows[0]["website_personal"].ToString();
            }
            else
            {

            }
        }

        protected void lbClose_click(object sender, EventArgs e)
        {
            mvIdentitas.SetActiveView(vDaftar);
            isiIdentitasPengusul();
        }

        protected void lbBatal_click(object sender, EventArgs e)
        {
            mvIdentitas.SetActiveView(vDaftar);
            isiIdentitasPengusul();
        }

        public void goHome()
        {
            mvIdentitas.SetActiveView(vDaftar);
            isiIdentitasPengusul();
        }

        protected void lbSimpan_click(object sender, EventArgs e)
        {
            if (objPengusul.updateIdentitasPersonal(Guid.Parse(objLogin.idPersonal), tbNomorKtp.Text,
                tbAlamat.Text, tbTempatLahir.Text, tbTglLahir.Text, tbNoTelepon.Text, tbNoHp.Text,
                tbAlamatSurel.Text, tbWebsitePersonal.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                mvIdentitas.SetActiveView(vDaftar);
                Session.Remove("identitas");
                isiIdentitasPengusul();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan data gagal");
            }
        }

        public static string ConvertDateTimeToDate(string dateTimeString, string dateTimeFormat, String langCulture)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(langCulture);
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(dateTimeString, out dt))
            {
                return dt.ToString(dateTimeFormat, culture);
            }
            return dateTimeString;
        }

        protected void lbEditFoto_click(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];
            ktUnggah.path2save = "~/fileUpload/fileFoto/" + objLogin.idPersonal + ".jpg";
            ktUnggah.max_size = 100 * 1000; // 100KB
            ktUnggah.alllowed_ext = "JPG;JPEG;PNG";
            ktUnggah.success_info = "Unggah foto profile berhasil";
            ktUnggah.failed_info = "Unggah foto profile gagal";
            ktUnggah.isReloadParentAfterSuccess = true;
            Session.Add("ktUnggah", ktUnggah);

            objModal.ShowModal(this.Page, "modalUpdateFoto");

        }

        private void refreshFotoProfile()
        {
            objLogin = (Models.login)Session["objLogin"];
            string pathfile = "~/fileUpload/fileFoto/" + objLogin.idPersonal + ".";

            string[] exts = { "JPG", "JPEG", "PNG" };
            for (int a = 0; a < exts.Length; a++)
            {
                string _pathfile = pathfile + exts[a].ToLower();
                if (File.Exists(Server.MapPath(_pathfile)))
                {
                    imgProfile.ImageUrl = _pathfile + "?r=" + DateTime.Now.Ticks.ToString();
                }
            }
        }
    }
}