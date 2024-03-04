using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace simlitekkes.UIControllers
{
    public class uiDropdownList
    {
        #region Fields

        private string _errorMessage;

        #endregion

        #region Konstruktor dan Desruktor

        public uiDropdownList()
        {
            this._errorMessage = "";
        }

        ~uiDropdownList()
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

        public bool bindToDropDownList(ref DropDownList objDrowDownList, DataTable objDataTable, string kolomTeks, string kolomValue)
        {
            bool retValue = false;

            objDrowDownList.Items.Clear();
            try
            {
                if (objDataTable != null && objDataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in objDataTable.Rows)
                    {
                        objDrowDownList.Items.Add(new ListItem(dr[kolomTeks].ToString(), dr[kolomValue].ToString()));
                    }
                }
                retValue = true;
                this._errorMessage = "DropDownList binding sukses.";
            }
            catch (Exception e)
            {
                this._errorMessage = "DropDownList binding bermasalah... \nHubungi administrator.\n" + e.Message;
            }

            return retValue;
        }

        public bool bindToDropDownListRemove(ref DropDownList objDrowDownList, DataTable objDataTable, string kolomNilai, string kolomValue)
        {
            bool retValue = false;

            try
            {
                if (objDataTable != null && objDataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in objDataTable.Rows)
                    {
                        if (dr[kolomValue].ToString() != kolomNilai)
                        {
                            objDrowDownList.Items.Remove(objDrowDownList.Items.FindByValue(dr[kolomValue].ToString()));
                        }
                        //else
                        //{
                        //    objDrowDownList.Items.Add(objDrowDownList.Items.FindByValue(dr[kolomValue].ToString()));
                        //}
                    }
                    objDrowDownList.SelectedValue = kolomNilai;
                }
                retValue = true;
                this._errorMessage = "DropDownList binding sukses.";
            }
            catch (Exception e)
            {
                this._errorMessage = "DropDownList binding bermasalah... \nHubungi administrator.\n" + e.Message;
            }

            return retValue;
        }

        #endregion
    }
}