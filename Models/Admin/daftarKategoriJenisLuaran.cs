using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarKategoriJenisLuaran : _abstractModels
    {
        public daftarKategoriJenisLuaran()
        {
            setInitValues();
        }

        #region Methods
        public bool getStatusAktif(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_daftar_status_aktif({0}, {1});", 0, -1);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listKategoriJenisLuaran(ref DataTable dataTable, string kd_sts_aktif)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_kategori_jenis_luaran('{0}', {1}, {2});"
                            , kd_sts_aktif, this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarKategoriJenisLuaran(string kd_sts_aktif)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.get_kategori_jenis_luaran(@kd_sts_aktif, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kd_sts_aktif", kd_sts_aktif)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertKategoriJenisLuaran(string kd_kategori_jenis_luaran, string kategori_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_kategori_jenis_luaran('{0}','{1}');", kd_kategori_jenis_luaran, kategori_jenis_luaran);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateKategoriJenisLuaran(string kd_kategori_jenis_luaran, string kategori_jenis_luaran, string kd_sts_aktif)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_kategori_jenis_luaran('{0}','{1}', '{2}');", kd_kategori_jenis_luaran, kategori_jenis_luaran, kd_sts_aktif);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteKategoriJenisLuaran(string kd_kategori_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.set_status_aktif_kategori_jenis_luaran('{0}',{1});"
                            , kd_kategori_jenis_luaran, 0);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}