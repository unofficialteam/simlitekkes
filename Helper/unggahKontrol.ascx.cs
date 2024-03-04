using simlitekkes.Core;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.Helper
{
    public partial class unggahKontrol : System.Web.UI.UserControl
    {
        kontrolUnggah ktUnggah;
        uiNotify noty = new uiNotify();
        public event EventHandler OnChildEventOccurs;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["ktUnggah"] != null)
            //{
            //    ktUnggah = (kontrolUnggah)Session["ktUnggah"];
            //    lblInfo.ForeColor = System.Drawing.Color.Red;
            //    lblInfo.Text = "";
            //    fileUpload1.Enabled = true;
            //    lbUnggahDokumen.Enabled = true;
            //}
            //else
            //{
            //    lblInfo.ForeColor = System.Drawing.Color.Red;
            //    lblInfo.Text = "Informasi Unggah Berkas tidak ditemukan !";
            //    fileUpload1.Enabled = false;
            //    lbUnggahDokumen.Enabled = false;
            //}
        }

        private bool isAllowedExtension(string namaFile, string exts)
        {
            bool isAllowed = false;
            string[] arrExt = exts.Split(';');
            for (int a = 0; a < arrExt.Length; a++)
            {
                if (namaFile.ToLower().EndsWith("." + arrExt[a].ToLower()))
                {
                    isAllowed = true;
                }
            }
            return isAllowed;
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "";
            ktUnggah = (kontrolUnggah)Session["ktUnggah"];
            lblInfo.ForeColor = System.Drawing.Color.Red;
            if (fileUpload1.HasFile)
            {
                if (fileUpload1.PostedFile.ContentLength < ktUnggah.max_size)
                {
                    if (isAllowedExtension(fileUpload1.FileName, ktUnggah.alllowed_ext))
                    {
                        unggahDokumen(ktUnggah.path2save, ktUnggah);
                    }
                    else
                    {
                        lblInfo.Text = "File harus berekstensi: " + ktUnggah.alllowed_ext;
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File harus berekstensi: " + ktUnggah.alllowed_ext);
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Ukuran file maksimal: " + (ktUnggah.max_size / 1024).ToString() + " KByte");
                    lblInfo.Text = "Ukuran file maksimal: " + (ktUnggah.max_size / 1024).ToString() + " KByte";
                }
            }
            else
            {
                lblInfo.Text = "File belum dipilih";
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File belum dipilih");
            }

        }


        private void unggahDokumen(string path, kontrolUnggah p_ktUnggah)
        {
            //try
            //{
                if(File.Exists(Server.MapPath(path)))
                {
                    File.Delete(Server.MapPath(path));
                }

                fileUpload1.SaveAs(Server.MapPath(path));
                //lblInfo.Text = p_ktUnggah.success_info;
                //lblInfo.ForeColor = System.Drawing.Color.DarkGreen;
                //lblInfo.Text = "";
                ktUnggah.isSuccess = true;
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", ktUnggah.success_info);
                if (OnChildEventOccurs != null)
                    OnChildEventOccurs(null, null);
            //}
            //catch (Exception ex)
            //{
            //    //lblInfo.Text = p_ktUnggah.failed_info + "<br>Kesalahan: " + ex.ToString();
            //    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", p_ktUnggah.failed_info + "<br>Kesalahan: " + ex.ToString());
            //}
        }
    }
}