using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarPeran : _abstractModels
    {
        public daftarPeran()
        {
            this.setInitValues();
        }

        #region Methods

        public bool getListKelompokPeran(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pengguna.list_kelompok_peran();");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listData(ref DataTable dataTable, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pengguna.list_peran2('{0}',{1},{2});",
                            pencarian, this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarData(string pencarian)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengguna.list_peran2(@pencarian, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@pencarian", pencarian)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertData(string nama_peran, string keterangan, string kd_kelompok_peran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM  pengguna.insup_peran(@id_peran, @nama_peran, @keterangan, @kd_kelompok_peran);";
            int id_peran = -1;
            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_peran", id_peran)
                , new Npgsql.NpgsqlParameter("@nama_peran", nama_peran)
                , new Npgsql.NpgsqlParameter("@keterangan", keterangan)
                , new Npgsql.NpgsqlParameter("@kd_kelompok_peran", kd_kelompok_peran));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateData(int id_peran, string nama_peran, string keterangan, string kd_kelompok_peran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM  pengguna.insup_peran(@id_peran, @nama_peran, @keterangan, @kd_kelompok_peran);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_peran", id_peran)
                , new Npgsql.NpgsqlParameter("@nama_peran", nama_peran)
                , new Npgsql.NpgsqlParameter("@keterangan", keterangan)
                , new Npgsql.NpgsqlParameter("@kd_kelompok_peran", kd_kelompok_peran));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteData(int id_peran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pengguna.set_status_aktif_peran({0},'{1}');"
                            , id_peran, "0");
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}