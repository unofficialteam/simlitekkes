using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarBuktiLuaran : _abstractModels
    {
        public daftarBuktiLuaran()
        {
            setInitValues();
        }
        #region Methods
        public bool listBuktiLuaran(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_bukti_luaran('{0}', {1}, {2});"
                            , "x", this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarBuktiLuaran()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.get_bukti_luaran(@is_luaran_th_akhir, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@is_luaran_th_akhir", "x")
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertBuktiLuaran(string bukti_luaran, string is_luaran_tahun_akhir)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_bukti_luaran({0},'{1}', '{2}');", 0, bukti_luaran, is_luaran_tahun_akhir);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateBuktiLuaran(int id_bukti_luaran, string bukti_luaran, string is_luaran_tahun_akhir)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_bukti_luaran({0},'{1}', '{2}');", id_bukti_luaran, bukti_luaran, is_luaran_tahun_akhir);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteBuktiLuaran(int id_bukti_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.set_status_aktif_bukti_luaran({0}, '{1}');"
                            , id_bukti_luaran, "0");
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}