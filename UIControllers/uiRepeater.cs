using System;
using System.Web.UI.WebControls;
using System.Data;

namespace simlitekkes.UIControllers
{
    public class uiRepeater
    {
        #region Fields

        private string _errorMessage;

        #endregion

        #region Konstruktor dan Desruktor

        public uiRepeater()
        {
            this._errorMessage = "";
        }

        ~uiRepeater()
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

        public bool bindToRepeater(ref Repeater objRepeater, DataTable objDataTable)
        {
            bool retValue = false;

            try
            {
                if (objDataTable != null)
                {
                    objRepeater.DataSource = objDataTable;
                    objRepeater.DataBind();
                }                

                retValue = true;
                this._errorMessage = "Repeater binding sukses.";
            }
            catch (Exception e)
            {
                this._errorMessage = "Repeater binding bermasalah... Hubungi administrator.\n" + e.Message;
            }

            return retValue;
        }

        //internal bool bindToRepeater(ref Repeater repeaterPengumuman, DataTable currentRecords, string[] namaKolomsPengumuman)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}