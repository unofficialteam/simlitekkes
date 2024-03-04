﻿using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{    
    public class luaranStrategi : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaranStrategi()
        {
            setInitValues();
        }

        ~luaranStrategi()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool insupTargetLuaranStrategi(
            int p_id_target_luaran,
            string p_nama_strategi,
            string p_pemegang_strategi,
            string p_tgl_awal,
            string p_tgl_akhir,
            string p_link_video

    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_strategi
                            ( 
                                 @p_id_target_luaran::integer,  
                                 @p_nama_strategi::text,
                                 @p_pemegang_strategi::text, 
                                 @p_tgl_awal::date,
                                 @p_tgl_akhir::date,
                                 @p_link_video::text
                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_strategi", p_nama_strategi)
            , new Npgsql.NpgsqlParameter("@p_pemegang_strategi", p_pemegang_strategi)
            , new Npgsql.NpgsqlParameter("@p_tgl_awal", p_tgl_awal)
            , new Npgsql.NpgsqlParameter("@p_tgl_akhir", p_tgl_akhir)
            , new Npgsql.NpgsqlParameter("@p_link_video", p_link_video)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getDataTargetLuaranStrategi(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_strategi(@p_id_target_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        #endregion
    }
}
