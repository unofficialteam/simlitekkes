using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace simlitekkes.UIControllers
{
    public class uiListView
    {
        #region Fields

        private string _errorMessage;

        #endregion

        #region Konstruktor dan Desruktor

        public uiListView()
        {
            this._errorMessage = "";
        }

        ~uiListView()
        {

        }

        #endregion

        #region Properties

        public string errorMessage
        {
            get
            {
                return _errorMessage;
            }
        }

        #endregion

        #region Methods

        public bool bindToListView(ref ListView objListView, DataTable objDataTable)
        {
            bool retValue = false;

            try
            {
                if (objDataTable != null)
                {
                    objListView.DataSource = objDataTable;
                    objListView.DataBind();
                }

                retValue = true;
                this._errorMessage = "ListView binding sukses.";
            }
            catch (Exception e)
            {
                this._errorMessage = "ListView binding bermasalah... Hubungi administrator.\n" + e.Message;
            }

            return retValue;
        }

        #endregion
    }
}