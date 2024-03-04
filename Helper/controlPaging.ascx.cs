using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.Helper
{
    public partial class controlPaging : System.Web.UI.UserControl
    {
        uiPaging obj_uiPaging = new uiPaging();
        public event EventHandler PageChanging;

        private int _rowsPerPage
        {
            get
            {
                if (ViewState["rowsPerPage"] == null) ViewState["rowsPerPage"] = 0;
                return Convert.ToInt32(ViewState["rowsPerPage"]);
            }
            set { ViewState["rowsPerPage"] = value; }
        }
        private int _totalRecords
        {
            get
            {
                if (ViewState["totalRecords"] == null) ViewState["totalRecords"] = 0;
                return Convert.ToInt32(ViewState["totalRecords"]);
            }
            set { ViewState["totalRecords"] = value; }
        }

        public int currentPage
        {
            get
            {
                if (ViewState["currentPage"] == null) ViewState["currentPage"] = 0;
                return Convert.ToInt32(ViewState["currentPage"]);
            }
            set { ViewState["currentPage"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                refreshPaging();
            }
            else
            {
                obj_uiPaging = (uiPaging)ViewState["uiPaging"];
            }
        }

        public void setPaging(int rowsPerPage, int totalRecords)
        {
            _rowsPerPage = rowsPerPage;
            _totalRecords = totalRecords;
            obj_uiPaging.setPaging(ref MenuPage, rowsPerPage, totalRecords);
        }

        public void refreshPaging()
        {
            obj_uiPaging.setPaging(ref MenuPage, _rowsPerPage, _totalRecords);
            ViewState["uiPaging"] = obj_uiPaging;
        }

        protected void menu_event(object sender, MenuEventArgs e)
        {
            string itemsText = e.Item.Text;
            obj_uiPaging.changePage(ref MenuPage, itemsText);

            switch (itemsText)
            {
                case "Prev":
                    currentPage = int.Parse(MenuPage.Items[obj_uiPaging.jmlKolomPaging].Value);
                    break;
                case "Next":
                    currentPage = int.Parse(MenuPage.Items[1].Value);
                    break;
                default:
                    if (_rowsPerPage != 0)
                    {
                        currentPage = int.Parse(itemsText) - 1;
                    }
                    break;
            }

            if (PageChanging != null)
                PageChanging(this, new EventArgs());
        }
    }
}