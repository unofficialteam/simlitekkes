using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace simlitekkes.UIControllers
{
    public class uiListBox
    {
        #region Fields

        private string _errorMessage;

        #endregion

        #region Konstruktor dan Desruktor

        public uiListBox()
        {
            this._errorMessage = "";
        }

        ~uiListBox()
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

        public bool bindToListBox(ref ListBox objListBox, DataTable objDataTable, string kolomTeks, string kolomValue)
        {
            bool retValue = false;

            objListBox.Items.Clear();
            try
            {
                if (objDataTable != null && objDataTable.Rows.Count > 0)
                {
                    foreach(DataRow dr in objDataTable.Rows)
                    {
                        objListBox.Items.Add(new ListItem(dr[kolomTeks].ToString(), dr[kolomValue].ToString()));
                    }
                }
                retValue = true;
                this._errorMessage = "ListBox binding sukses.";
            }
            catch (Exception e)
            {
                this._errorMessage = "ListBox binding bermasalah... \nHubungi administrator.\n" + e.Message;
            }

            return retValue;
        }

        #endregion
    }
}