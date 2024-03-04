using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarJenisSKDokumen : _abstractModels
    {
        public daftarJenisSKDokumen()
        {
            setInitValues();
        }
        #region Methods
        public bool listJenisSKDokumen(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.get_jenis_sk_dokumen({0},{1});"
                            , this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarJenisSKDokumen()
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.get_jenis_sk_dokumen(@limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertJenisSKDokumen(string kd_sk_dokumen, string nama_dokumen)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.insup_sk_dokumen('{0}','{1}');", kd_sk_dokumen, nama_dokumen);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateJenisSKDokumen(string kd_sk_dokumen, string nama_dokumen)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.insup_sk_dokumen('{0}','{1}');", kd_sk_dokumen, nama_dokumen);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteJenisSKDokumen(string kd_sk_dokumen)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.set_status_jenis_sk_dokumen('{0}','{1}');"
                            , kd_sk_dokumen, "0");
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}