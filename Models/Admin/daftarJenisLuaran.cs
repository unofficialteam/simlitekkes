using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarJenisLuaran : _abstractModels
    {
        public daftarJenisLuaran()
        {
            setInitValues();
        }
        #region Methods
        public bool getKategoriJenisLuaran(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_kategori_jenis_luaran('{0}', {1}, {2});", "x", 0, -1);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listJenisLuaran(ref DataTable dataTable, string kd_kategori_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.get_jenis_luaran('{0}', '{1}',{2},{3});"
                            , kd_kategori_jenis_luaran, "x", this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarJenisLuaran(string kd_kategori_jenis_luaran)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM penelitian.get_jenis_luaran(@kd_kategori_jenis_luaran, @kd_sts_aktif, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kd_kategori_jenis_luaran", kd_kategori_jenis_luaran)
                , new Npgsql.NpgsqlParameter("@kd_sts_aktif", "x")
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertJenisLuaran(string kd_kategori_jenis_luaran, string jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.insup_jenis_luaran({0},'{1}','{2}');", 0, kd_kategori_jenis_luaran, jenis_luaran);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateJenisLuaran(int id_jenis_luaran, string kd_kategori_jenis_luaran, string jenis_luaran, string kd_sts_aktif)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.insup_jenis_luaran({0},'{1}','{2}','{3}');", id_jenis_luaran, kd_kategori_jenis_luaran, jenis_luaran, kd_sts_aktif);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteJenisLuaran(int id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.set_status_aktif_jenis_luaran({0},'{1}');"
                            , id_jenis_luaran, "0");
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}