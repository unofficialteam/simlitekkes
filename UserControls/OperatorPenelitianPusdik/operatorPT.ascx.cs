using simlitekkes.Models.OperatorPenelitianPusdik;
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
    public partial class dataPendukungPT : System.Web.UI.UserControl
    {
        daftarOperatorPT modelData = new daftarOperatorPT();
        pengirimanUserPassword objKirimUserPwd = new pengirimanUserPassword();

        uiGridView obj_uiGridView = new uiGridView();
        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiModal objModal = new uiModal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];
            if (!IsPostBack)
            {
                isiGvDaftarData();

            }
        }


        protected void ddlJmlBarisData_SelectedIndexChanged(object sender, EventArgs e)
        {
            pagingDaftarData.currentPage = 0;
            isiGvDaftarData();
            refreshPaging();
        }

        protected void ddlKdJenisKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            pagingDaftarData.currentPage = 0;
            isiGvDaftarData();
            refreshPaging();
        }

        private void refreshPaging()
        {
            if (ViewState["jml_record"] != null)
                pagingDaftarData.setPaging(int.Parse(ddlJmlBarisData.SelectedValue), int.Parse(ViewState["jml_record"].ToString()));
            else
                pagingDaftarData.setPaging(1, 1);
            pagingDaftarData.refreshPaging();
        }

        private void refreshGridView()
        {
            // Membaca halaman yang aktif
            modelData.currentPage = pagingDaftarData.currentPage;
            modelData.rowsPerPage = int.Parse(ddlJmlBarisData.SelectedValue);
            // Bind data
            modelData.getDaftarData(this.ddlKdJenisKegiatan.SelectedValue, this.tbPencarian.Text);
            obj_uiGridView = new uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarData, modelData.currentRecords);
        }

        private void isiGvDaftarData()
        {
            var dt = new DataTable();
            modelData.currentPage = pagingDaftarData.currentPage;
            modelData.rowsPerPage = Int32.Parse(ddlJmlBarisData.SelectedValue);
            if (modelData.listData(ref dt, ddlKdJenisKegiatan.SelectedValue, tbPencarian.Text))
            {
                gvDaftarData.DataSource = dt;
                gvDaftarData.DataBind();
            }
            if (dt.Rows.Count > 0)
            {
                ViewState["jml_record"] = dt.Rows[0]["jml_record"].ToString();
                refreshPaging();
            }
        }

        protected void gvDaftarData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNodaftarData = (Label)e.Row.FindControl("lblNodaftarData");
                lblNodaftarData.Text = (e.Row.RowIndex + 1 + Int32.Parse(ddlJmlBarisData.SelectedValue) * (pagingDaftarData.currentPage)).ToString();
            }
        }

        protected void gvDaftarData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Ganti Index Editing
            gvDaftarData.EditIndex = e.NewEditIndex;
            refreshGridView();
        }

        protected void gvDaftarData_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            // Kembalikan Index Editing (tidak ada yg di edit)
            gvDaftarData.EditIndex = -1;
            refreshGridView();
        }

        protected void gvDaftarData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // ambil data
            TextBox Telepon = gvDaftarData.Rows[e.RowIndex].FindControl("tbEditTelepon") as TextBox;
            TextBox Fax = gvDaftarData.Rows[e.RowIndex].FindControl("tbEditFax") as TextBox;
            TextBox Email = gvDaftarData.Rows[e.RowIndex].FindControl("tbEditEmail") as TextBox;
            if (!modelData.insupData(Guid.Parse(gvDaftarData.DataKeys[e.RowIndex]["id_institusi"].ToString()), ddlKdJenisKegiatan.SelectedValue, Email.Text, Telepon.Text, Fax.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Mengubah Data");
            else
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sukses Mengubah Data");
            gvDaftarData.EditIndex = -1;
            refreshGridView();
        }

        protected void gvDaftarData_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Add"))
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                if (gvDaftarData.DataKeys[gvr.RowIndex]["surel"].ToString().Equals("-"))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pastikan Email Lembaga Terisi");
                }
                else
                {
                    ViewState["IdInstitusiAdd"] = gvDaftarData.DataKeys[gvr.RowIndex]["id_institusi"].ToString();
                    lblJenisKegiatan.Text = ddlKdJenisKegiatan.SelectedItem.Text;
                    lblNamaInstitusi.Text = gvDaftarData.DataKeys[gvr.RowIndex]["nama_institusi"].ToString();
                    objModal.ShowModal(this.Page, "modalKonfirmasiGenerate");
                }
            }
            else if (e.CommandName.Equals("SendMail"))
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                if (gvDaftarData.DataKeys[gvr.RowIndex]["surel"].ToString().Equals("-"))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Pastikan Email Lembaga Terisi");
                }
                else if (gvDaftarData.DataKeys[gvr.RowIndex]["jml_pengguna"].ToString().Equals("0"))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak Ditemukan Akun Untuk Dikirimkan");
                }
                else
                {
                    // Mail Receiver             
                    string surelTujuan = gvDaftarData.DataKeys[gvr.RowIndex]["surel"].ToString();
                    string[] arrSurelPengirimNpassword = { "simlitabkes.kemenkes@gmail.com", "*Simlitabkes*" };
                    // Mail Object
                    MailMessage msg = new MailMessage();
                    // Mail Structur
                    msg.From = new MailAddress(arrSurelPengirimNpassword[0]);
                    msg.To.Add(gvDaftarData.DataKeys[gvr.RowIndex]["surel"].ToString());
                    msg.Subject = string.Format("Akun SIMLITABKES");

                    // Mail Message
                    System.Text.StringBuilder st = new System.Text.StringBuilder("");
                    st.Append("Klik link dibawah ini untuk Login ke SIMLITABKES");
                    st.Append(" \n");
                    // Url For Login Page
                    String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                    String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                    // Mail Message
                    st.Append(strUrl + "Login.aspx");
                    st.Append("\n\n");
                    int i = 1;
                    Array username = (Array) gvDaftarData.DataKeys[gvr.RowIndex]["array_username"];
                    Array password = (Array) gvDaftarData.DataKeys[gvr.RowIndex]["array_password"];
                    List<string> listUsr = new List<string>();
                    List<string> listPswd = new List<string>();
                    foreach (string item in username)
                    {
                        listUsr.Add(item.Trim());
                    }

                    foreach (string item in password)
                    {
                        listPswd.Add(item.Trim());
                    }

                    for (int a = 0; a < listUsr.Count; a++)
                    {
                        // isi content surel
                        st.Append("Nama User: ");
                        //st.Append(dr["nama_user"].ToString());
                        st.Append(listUsr[a].ToString());
                        st.Append(" | ");
                        st.Append("Password: ");
                        //st.Append(dr["pswd"].ToString());
                        st.Append(listPswd[a].ToString());
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
                    NetworkCredential cred = new NetworkCredential(arrSurelPengirimNpassword[0], arrSurelPengirimNpassword[1]);
                    smtpClient.Credentials = cred;
                    try
                    {
                        smtpClient.Send(msg);
                        if (objKirimUserPwd.insertLogPengirimanAkunOptPT2(Guid.Parse(gvDaftarData.DataKeys[gvr.RowIndex]["id_kontak_pic_pengguna_institusi"].ToString()), surelTujuan, "1", arrSurelPengirimNpassword[0]))
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Akun telah berhasil dikirim");
                            refreshGridView();
                        }
                        else
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Kirim akun gagal", objKirimUserPwd.errorMessage);
                    }
                    catch (SmtpException ex)
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.info, "Kirim akun gagal", ex.Message);
                    }
                }
            }
        }

        protected void daftarData_PageChanging(object sender, EventArgs e)
        {
            // Baca paging dan jumlah baris
            modelData.currentPage = pagingDaftarData.currentPage;
            modelData.rowsPerPage = int.Parse(ddlJmlBarisData.SelectedValue);
            // ambil data
            if (!modelData.getDaftarData(this.ddlKdJenisKegiatan.SelectedValue, this.tbPencarian.Text))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");
            gvDaftarData.EditIndex = -1;
            // Bind data
            obj_uiGridView = new uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarData, modelData.currentRecords);
        }

        protected void lbCariData_Click(object sender, EventArgs e)
        {
            pagingDaftarData.currentPage = 0;
            isiGvDaftarData();
            refreshPaging();
        }

        protected void lbGenerateData_Click(object sender, EventArgs e)
        {
            int peran = 6;
            if (ddlKdJenisKegiatan.SelectedValue.Equals("2"))
            {
                peran = 40;
            }
            DataTable result = new DataTable();
            if (!modelData.generateAkun(ref result, Guid.Parse(ViewState["IdInstitusiAdd"].ToString()), peran, Int32.Parse(tbJumlahAkun.Text)))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Gagal Membuat Akun");
            else
            {
                if (result.Rows[0]["status"].ToString().Equals("1"))
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", result.Rows[0]["keterangan"].ToString());
                else
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", result.Rows[0]["keterangan"].ToString());
            }
            refreshGridView();
        }
    }
}