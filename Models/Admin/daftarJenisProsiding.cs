using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarJenisProsiding : _abstractModels
    {
        public daftarJenisProsiding()
        {
            setInitValues();
        }
        #region Methods
        public bool listJenisProsiding(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.get_jenis_prosiding({0},{1});"
                            , this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarJenisProsiding()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM penelitian.get_jenis_prosiding(@limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertJenisProsiding(string kd_jenis_prosiding, string jenis_prosiding)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.insup_jenis_prosiding('{0}','{1}');", kd_jenis_prosiding, jenis_prosiding);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateJenisProsiding(string kd_jenis_prosiding, string jenis_prosiding)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.insup_jenis_prosiding('{0}','{1}');", kd_jenis_prosiding, jenis_prosiding);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteJenisProsiding(string kd_jenis_prosiding)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.del_jenis_prosiding('{0}');"
                            , kd_jenis_prosiding);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}