using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.Helper
{
    public partial class modalKonfirmasi : System.Web.UI.UserControl
    {
        public event EventHandler OnDelete;

        public string TitleKonfirmasi
        {
            set { lblConfirmTitle.Text = value; }
        }
        public string TextKonfirmasi
        {
            set { lblConfirmText.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Show()
        {
            string jsScript = "$('#confirmModal').modal({keyboard: true});";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modalShow", jsScript, true);
        }

        public void Hide()
        {
            string jsScript = "$('#confirmModal').modal('hide');";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modalShow", jsScript, true);
        }

        protected void btnHapus_Click(object sender, EventArgs e)
        {
            OnDelete?.Invoke(this, new EventArgs());
        }
    }
}