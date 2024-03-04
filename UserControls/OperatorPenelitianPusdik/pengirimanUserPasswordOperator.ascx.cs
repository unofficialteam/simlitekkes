using simlitekkes.Models.pelaksanaan;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class pengirimanUserPasswordOperator : System.Web.UI.UserControl
    {
        pengirimanUserPassword objKirimUserPwd = new pengirimanUserPassword();
        uiNotify noty = new uiNotify();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isiGridview();
            }
        }

        private void isiGridview()
        {
            DataTable dt = new DataTable();
            objKirimUserPwd.listPoltekesKemenkes(ref dt);
            gvPengirimanPassword.DataSource = dt;
            gvPengirimanPassword.DataBind();
        }

        protected void gvPengirimanPassword_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id_institusi = gvPengirimanPassword.DataKeys[e.RowIndex]["id_institusi"].ToString();
            string strUsrs = gvPengirimanPassword.DataKeys[e.RowIndex]["nama_user"].ToString();
            string strPwds = gvPengirimanPassword.DataKeys[e.RowIndex]["pswd"].ToString();
            if (strUsrs.Trim() == "")
            {
                objKirimUserPwd.insertUserPasswordOperatorPT(id_institusi, 6, 5);
                DataTable dtUsrPwd = new DataTable();
                objKirimUserPwd.listUserPasswordOptPtByInstitusi(ref dtUsrPwd, id_institusi);
                strUsrs = dtUsrPwd.Rows[0]["nama_user"].ToString();
                strPwds = dtUsrPwd.Rows[0]["pswd"].ToString();
            }
            string[] arrUsr = strUsrs.Split(';');
            string[] arrPwd = strPwds.Split(';');

            // receiver mail             
            string surelTujuan = gvPengirimanPassword.DataKeys[e.RowIndex]["surel"].ToString();
            string[] arrSurelPengirimNpassword = { "sendermail@gmail.com", "p_sendermail" };
            
            NetworkCredential cred = new NetworkCredential(arrSurelPengirimNpassword[0], arrSurelPengirimNpassword[1]);
            MailMessage msg = new MailMessage();
            DataTable dt = new DataTable();

            msg.From = new MailAddress(arrSurelPengirimNpassword[0]);

            msg.To.Add(surelTujuan);
            msg.Subject = string.Format("Akun SIMLITABKES");

            //modelVerifikasi.insertAkunPic(ref dt, Guid.Parse(idRegistrasi), Guid.Parse(objLogin.idPersonal), surel_pengirim);
            if (dt.Rows.Count >= 0) //(dt.Rows.Count > 0)
            {
                System.Text.StringBuilder st = new System.Text.StringBuilder("");

                st.Append("Klik link dibawah ini untuk Login ke SIMLITABKES");
                st.Append(" \n");
                //idRegistrasi = dt.Rows[0]["id_registrasi"].ToString();

                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");

                st.Append(strUrl + "Login.aspx");
                st.Append("\n\n");
                int i = 1;
                //foreach (DataRow dr in dt.Rows)
                for (int a = 0; a < arrUsr.Length; a++)
                {
                    // isi content surel
                    st.Append("Nama User: ");
                    //st.Append(dr["nama_user"].ToString());
                    st.Append(arrUsr[a].ToString());
                    st.Append(" | ");
                    st.Append("Password: ");
                    //st.Append(dr["pswd"].ToString());
                    st.Append(arrPwd[a].ToString());
                    st.Append(" \n");
                    i++;
                }

                st.Append("\nAlamat surel ini bersifat sementara hanya untuk mengirim nama user dan password anda, mohon tidak dibalas.");
                //st.Append("\nApabila ingin berkomunikasi melalui surel, gunakan alamat simlitabkes@kemenkes.go.id.\nTerimakasih.");
                st.Append("\n\nHormat kami,\nAdministrator SIMLITABKES");
                msg.Body = st.ToString();

                SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Host = "mail.kemdikbud.go.id";
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.Port = 465;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = cred;  
                smtpClient.Send(msg);
                objKirimUserPwd.insertLogPengirimanAkunOptPT(surelTujuan, arrSurelPengirimNpassword[0]);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Akun telah berhasil dikirim");
                isiGridview();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.info, "Kirim akun gagal", objKirimUserPwd.errorMessage);
            }
        }
    }
}