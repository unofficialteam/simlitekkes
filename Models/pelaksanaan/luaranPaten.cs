using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class luaranPaten : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaranPaten()
        {
            setInitValues();
        }

        ~luaranPaten()
        {

        }

        #endregion

        #region Properties

        #endregion
        
        #region Methods

        public bool getDataTargetLuaranPaten(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL;
            strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_target_luaran_paten({0});", p_id_target_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupTargetLuaranPaten(int p_id_target_luaran, string p_nama_paten, 
            string p_pemegang_paten, string p_no_pendaftaran, string p_no_granted_sertifikat, 
            string p_tgl_pencatatan, int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_paten
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_nama_paten::character varying,
                                    @p_pemegang_paten::character varying,
                                    @p_no_pendaftaran::character varying,
                                    @p_no_granted_sertifikat::character varying,
                                    @p_tgl_pencatatan::date,
                                    @p_id_target_jenis_luaran::integer
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_paten", p_nama_paten)
            , new Npgsql.NpgsqlParameter("@p_pemegang_paten", p_pemegang_paten)
            , new Npgsql.NpgsqlParameter("@p_no_pendaftaran", p_no_pendaftaran)
            , new Npgsql.NpgsqlParameter("@p_no_granted_sertifikat", p_no_granted_sertifikat)
            , new Npgsql.NpgsqlParameter("@p_tgl_pencatatan", p_tgl_pencatatan)
            , new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion

    }
}