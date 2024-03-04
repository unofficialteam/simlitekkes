using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarPeranPenulis : _abstractModels
    {
        public daftarPeranPenulis()
        {
            setInitValues();
        }
        #region Methods
        
        public bool listPeranPenulis(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.get_peran_penulis('{0}',{1},{2});"
                            , "1", this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarPeranPenulis()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM penelitian.get_peran_penulis(@kd_sts_aktif, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kd_sts_aktif", "1")
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertPeranPenulis(string kd_peran_penulis, string peran_penulis)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.insup_peran_penulis('{0}','{1}');", kd_peran_penulis, peran_penulis);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updatePeranPenulis(string kd_peran_penulis, string peran_penulis)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.insup_peran_penulis('{0}','{1}');", kd_peran_penulis, peran_penulis);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deletePeranPenulis(string kd_peran_penulis)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.set_status_peran_penulis('{0}','{1}');"
                            , kd_peran_penulis, "0");
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}