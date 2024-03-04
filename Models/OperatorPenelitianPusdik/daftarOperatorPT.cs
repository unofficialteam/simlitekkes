using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class daftarOperatorPT : _abstractModels
    {
        public daftarOperatorPT()
        {
            setInitValues();
        }

        #region Methods

        public bool listData(ref DataTable dataTable, string kd_jenis_kegiatan, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pengguna.list_kontak_pic_pengguna_institusi2('{0}','{1}',{2},{3},'{4}');"
                            , "1", kd_jenis_kegiatan, this.rowsPerPage, this.currentPage * this._rowsPerPage, pencarian);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarData(string kd_jenis_kegiatan, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengguna.list_kontak_pic_pengguna_institusi2(@kd_jenis_institusi, @kd_jenis_kegiatan, @limit, @offset, @pencarian);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kd_jenis_institusi", "1")
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@pencarian", pencarian)
                , new Npgsql.NpgsqlParameter("@kd_jenis_kegiatan", kd_jenis_kegiatan)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool generateAkun(ref DataTable dt, Guid id_institusi, int id_peran, int jumlah)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengguna.insert_user_password_operator_institusi(@id_institusi, @id_peran, @jumlah);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dt
               , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@id_peran", id_peran)
                , new Npgsql.NpgsqlParameter("@jumlah", jumlah));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insupData(Guid id_institusi, string kd_jenis_kegiatan, string surel, string telepon, string fax)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengguna.insup_kontak_pic_pengguna_institusi2(@id_institusi, @kd_jenis_kegiatan, @surel, @telepon, @fax);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@kd_jenis_kegiatan", kd_jenis_kegiatan)
                , new Npgsql.NpgsqlParameter("@surel", surel)
                , new Npgsql.NpgsqlParameter("@telepon", telepon)
                , new Npgsql.NpgsqlParameter("@fax", fax));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}