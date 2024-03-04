using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace simlitekkes.UIControllers
{
    public class uiNotify
    {
        public enum NotifyType
        {
            error,
            warning,
            success,
            info
        }

        public void Notify(Page thePage, NotifyType notifyType, string title, string message)
        {
            string type = notifyType.ToString();
            message = message.Replace("'", "&quot;");

            string jsScript = "toastr.options = { " +
                              "positionClass: 'toast-top-right'};" +
                              "toastr." + type + "('" + message + "', '" + title + "');";
            ScriptManager.RegisterStartupScript(thePage, this.GetType(), "notify", jsScript, true);
        }
    }
}