using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace simlitekkes.UIControllers
{
    public class uiModal
    {
        public void ShowModal(Page thePage, string modalID)
        {
            string jsScript = "$('#" + modalID + "').modal({keyboard: true});";
            ScriptManager.RegisterStartupScript(thePage, this.GetType(), "modalShow", jsScript, true);
        }

        public void HideModal(Page thePage, string modalID)
        {
            string jsScript = "$('#" + modalID + "').modal('hide');";
            ScriptManager.RegisterStartupScript(thePage, this.GetType(), "modalHide", jsScript, true);
        }
    }
}