using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using simlitekkes.Core;
using DataAccess;
using System.Web.Configuration;

namespace simlitekkes.Models
{
    [Serializable]
    public abstract class _abstractModels
    {
        #region Common Enum

        public enum GROUP_KONFIG
        {
            PENGELOLAAN_KEGIATAN = 1,
            PERSYARATAN_USULAN = 2,
            ATURAN_PENILAIAN = 3
        }

        public enum GROUP_PERSYARATAN
        {
            USULAN_KEGIATAN_PENELITIAN = 1,
            USULAN_KEGIATAN_PENGABDIAN = 2
        }

        #endregion

        #region Fields

        protected int _currentPage;
        protected int _numOfPage;
        protected int _rowsPerPage;
        protected int _numOfRecords;
        protected DataTable _currentRecords;
        protected PostgresData _db;
        protected string _connectionString;

        protected string _errorMessage;
        protected string _strSQL;

        #endregion

        #region Properties

        public int currentPage
        {
            get { return this._currentPage; }
            set { this._currentPage = value; }
        }

        public int rowsPerPage
        {
            get { return this._rowsPerPage; }
            set { this._rowsPerPage = value; }
        }

        public int numOfPage
        {
            get { return this._numOfPage; }
            set { this._numOfPage = value; }
        }

        public int numOfRecords
        {
            get { return this._numOfRecords; }
            set { this._numOfRecords = value; }
        }

        public string errorMessage
        {
            get { return this._errorMessage; }
        }

        public DataTable currentRecords
        {
            get { return this._currentRecords; }
        }

        #endregion

        #region Konstruktor & Destructor

        public _abstractModels()
        {
            string runningMode = WebConfigurationManager.AppSettings["runnigmode"];
            if(runningMode.ToUpper() == "PRODUCTION")
                this._connectionString = WebConfigurationManager.ConnectionStrings["productionDB"].ConnectionString;
            else if (runningMode.ToUpper() == "DEVELOPMENT")
                this._connectionString = WebConfigurationManager.ConnectionStrings["developmentDB"].ConnectionString;
            else if (runningMode.ToUpper() == "PELATIHAN")
                this._connectionString = WebConfigurationManager.ConnectionStrings["pelatihanDB"].ConnectionString;

            this._db = new PostgresData(this._connectionString);
        }

        #endregion

        #region Private Function

        protected void setInitValues()
        {
            this._errorMessage = "";
            this._currentPage = 0;
            this._rowsPerPage = 0;
            this._currentRecords = new DataTable();
            this._numOfPage = 5;
            this._numOfRecords = 0;
            this._strSQL = "";
        }

        #endregion
    }
}