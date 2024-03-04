using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace simlitekkes.UIControllers
{
    public class uiGridView
    {
        #region Fields

        private string _errorMessage;

        #endregion

        #region Konstruktor dan Desruktor

        public uiGridView()
        {
            this._errorMessage = "";
        }

        ~uiGridView()
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

        public bool bindToGridView(ref GridView objGridView, DataTable objDataTable)
        {
            bool retValue = false;

            try
            {
                if (objDataTable != null)
                {
                    objGridView.DataSource = objDataTable;
                    objGridView.DataBind();
                }

                retValue = true;
                this._errorMessage = "GridView binding sukses.";
            }
            catch (Exception e)
            {
                this._errorMessage = "Gridview binding bermasalah... Hubungi administrator.\n" + e.Message;
            }

            return retValue;
        }

        public bool bindToGridView(ref GridView objGridView, DataTable objDataTable, string[] namaKolom)
        {
            bool retValue = false;

            try
            {
                if (objDataTable != null && objDataTable.Rows.Count > 0)
                {
                    objGridView.DataSource = objDataTable;
                    objGridView.DataBind();
                }
                else
                {
                    objDataTable = new DataTable();
                    foreach (string strNama in namaKolom)
                    {
                        objDataTable.Columns.Add(strNama, typeof(char));
                    }

                    objDataTable.Rows.Add(objDataTable.NewRow());
                    objGridView.DataSource = objDataTable;
                    objGridView.DataBind();

                    objGridView.Rows[0].Cells.Clear();
                    objGridView.Rows[0].Cells.Add(new TableCell());
                    objGridView.Rows[0].Cells[0].ColumnSpan = objGridView.Columns.Count;
                    objGridView.Rows[0].Cells[0].Text = "Data masih kosong.";
                    objGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    objGridView.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                }

                retValue = true;
                this._errorMessage = "Gridview binding sukses.";
            }
            catch (Exception e)
            {
                this._errorMessage = "Gridview binding bermasalah... Hubungi administrator.\n" + e.Message;
            }

            return retValue;
        }

        #endregion
    }
}