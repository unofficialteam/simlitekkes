using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class SKReviewer: _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public SKReviewer()
        {
            setInitValues();
        }

        ~SKReviewer()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getDokumenSkReviewer(ref DataTable dataTable, Guid id_institusi, string thn_sk)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM penelitian.list_dokumen_sk_reviewer(@id_institusi, @thn_sk);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@thn_sk", thn_sk));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool InsupDokumenSK(Guid id_sk, string no_sk, string kd_jenis_sk, Guid id_institusi_penerbit_sk, string thn_sk, string thn_upload)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.insup_dokumen_sk_reviewer(@id_sk::uuid, @no_sk::character varying, @kd_jenis_sk::character(2), @id_institusi_penerbit_sk::uuid, @thn_sk::character(4), @thn_upload::character(4))";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_sk", id_sk)
                , new Npgsql.NpgsqlParameter("@no_sk", no_sk)
                , new Npgsql.NpgsqlParameter("@kd_jenis_sk", kd_jenis_sk)
                , new Npgsql.NpgsqlParameter("@id_institusi_penerbit_sk", id_institusi_penerbit_sk)
                , new Npgsql.NpgsqlParameter("@thn_sk", thn_sk)
                , new Npgsql.NpgsqlParameter("@thn_upload", thn_upload));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}