using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace simlitekkes.Helper
{
    public partial class captchaImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Bitmap objBMP = new Bitmap(80, 38);
            Graphics objGraphics = Graphics.FromImage(objBMP);
            objGraphics.Clear(ColorTranslator.FromHtml("#37a000"));

            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //' Configure font to use for text
            Font objFont = new Font("Arial", 15, FontStyle.Bold);
            string randomStr = "";
            int[] myIntArray = new int[5];

            //That is to create the random # and add it to our string
            Random autoRand = new Random();

            int bil1 = Convert.ToInt32(autoRand.Next(0, 10));
            int bil2 = Convert.ToInt32(autoRand.Next(0, 10));
            int jumlah = bil1 + bil2;
            string hasil = Convert.ToString(jumlah);
            randomStr = Convert.ToString(bil1) + " + " + Convert.ToString(bil2) + " = ";

            //This is to add the string to session cookie, to be compared later
            Session.Add("hasil", hasil);

            //' Write out the text
            objGraphics.DrawString(randomStr, objFont, Brushes.White, 3, 7);

            //' Set the content type and return the image
            Response.ContentType = "image/png";
            objBMP.Save(Response.OutputStream, ImageFormat.Png);

            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();
        }
    }
}