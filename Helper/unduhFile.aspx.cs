using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.Helper
{
    public partial class unduhFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AtributUnduh"] == null)
            {
                Response.Write("Informasi Berkas Unduh tidak dapat ditemukan !");
            }
            else
            {
                UnduhFile();
            }
        }

        public void UnduhFile()
        {
            var atributUnduh = (AtributUnduh)Session["AtributUnduh"];
          
            var filePath = Path.Combine(Server.MapPath(atributUnduh.FolderUnduh),
                            atributUnduh.NamaBerkas);
            var extension = Path.GetExtension(atributUnduh.NamaBerkas);


            try
            {
                Response.ContentType = getContentType(extension);
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + atributUnduh.NamaBerkasdiUnduh + "\"");
                Response.TransmitFile(filePath);
            }
            catch (Exception)
            {
                //Response.Write(ex.Message);
            }
            finally
            {
                Response.End();
                Session.Remove("AtributUnduh");
            }

        }

        private string getContentType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                case ".docx":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".xlsx":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                case ".pptx":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }
    }

    public class AtributUnduh
    {
        public string FolderUnduh { get; set; }
        public string NamaBerkas { get; set; }
        public string NamaBerkasdiUnduh { get; set; }
    }
}