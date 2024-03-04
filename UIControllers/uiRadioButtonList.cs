using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

namespace simlitekkes.UIControllers
{   
    public class uiRadioButtonList
    {
        #region Fields

        private string _errorMessage;

        #endregion

        #region Konstruktor dan Desruktor

        public uiRadioButtonList()
        {
            this._errorMessage = "";
        }

        ~uiRadioButtonList()
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

        public bool bindToRadioButtonList(ref RadioButtonList objRadioButtonList, DataTable objDataTable, string kolomTeks, string kolomValue)
        {
            bool retValue = false;

            objRadioButtonList.Items.Clear();
            try
            {
                if (objDataTable != null && objDataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in objDataTable.Rows)
                    {
                        objRadioButtonList.Items.Add(new ListItem(dr[kolomTeks].ToString(), dr[kolomValue].ToString()));
                    }
                }
                retValue = true;
                this._errorMessage = "RadioButtonList binding sukses.";
            }
            catch (Exception e)
            {
                this._errorMessage = "RadioButtonList binding bermasalah... \nHubungi administrator.\n" + e.Message;
            }

            return retValue;
        }

        #endregion
    }
}
