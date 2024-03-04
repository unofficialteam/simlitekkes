using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{    
    public class luaranPurwarupaPrototipe : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaranPurwarupaPrototipe()
        {
            setInitValues();
        }

        ~luaranPurwarupaPrototipe()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool insupTargetLuaranPurwarupaPrototipe(
            int p_id_target_luaran,
            string p_nama_purwarupa_prototipe,
            string p_pemegang_purwarupa_prototipe,
            string p_tgl_awal,
            string p_tgl_akhir,
            string p_link_video

    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_purwarupa_prototipe
                            ( 
                                 @p_id_target_luaran::integer,  
                                 @p_nama_purwarupa_prototipe::text,
                                 @p_pemegang_purwarupa_prototipe::text, 
                                 @p_tgl_awal::date,
                                 @p_tgl_akhir::date,
                                 @p_link_video::text
                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_purwarupa_prototipe", p_nama_purwarupa_prototipe)
            , new Npgsql.NpgsqlParameter("@p_pemegang_purwarupa_prototipe", p_pemegang_purwarupa_prototipe)
            , new Npgsql.NpgsqlParameter("@p_tgl_awal", p_tgl_awal)
            , new Npgsql.NpgsqlParameter("@p_tgl_akhir", p_tgl_akhir)
            , new Npgsql.NpgsqlParameter("@p_link_video", p_link_video)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getDataTargetLuaranPurwarupaPrototipe(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_purwarupa_prototipe(@p_id_target_luaran);";
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
