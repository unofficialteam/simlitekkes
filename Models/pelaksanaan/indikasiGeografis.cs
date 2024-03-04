using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class indikasiGeografis : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public indikasiGeografis()
        {
            setInitValues();
        }

        ~indikasiGeografis()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool getDataTargetLuaranIndikasiGeografis(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL;
            strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_target_luaran_indikasi_geografis({0});", p_id_target_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupTargetLuaranIndikasiGeografis(int p_id_target_luaran, string p_nama_indikasi_geografis,
            string p_nama_pemilik,string p_no_agenda, string p_no_pendaftaran, string p_tgl_pendaftaran,
            int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_indikasi_geografis
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_nama_indikasi_geografis::text,
                                    @p_nama_pemilik::text,
                                    @p_no_agenda::text, 
                                    @p_no_pendaftaran::text,
                                    @p_tgl_pendaftaran::date,
                                    @p_id_target_jenis_luaran::integer
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_indikasi_geografis", p_nama_indikasi_geografis)
            , new Npgsql.NpgsqlParameter("@p_nama_pemilik", p_nama_pemilik)
            , new Npgsql.NpgsqlParameter("@p_no_agenda", p_no_agenda)
            , new Npgsql.NpgsqlParameter("@p_no_pendaftaran", p_no_pendaftaran)
            , new Npgsql.NpgsqlParameter("@p_tgl_pendaftaran", p_tgl_pendaftaran)
            , new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion

    }
}