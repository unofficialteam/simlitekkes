using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class luaranModel : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaranModel()
        {
            setInitValues();
        }

        ~luaranModel()
        {

        }

        #endregion

        #region Properties

        #endregion
        
        #region Methods

        public bool getDataTargetLuaranModel(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL;
            strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_target_luaran_model({0});", p_id_target_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        
        public bool insupTargetLuaranModel(int p_id_target_luaran, string p_nama_model,
            string p_pemegang_model, string p_periode_uji_coba_mulai,
            string p_periode_uji_coba_berakhir, string p_link_video_dokumentasi)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_model
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_nama_model::character varying,
                                    @p_pemegang_model::character varying,
                                    @p_periode_uji_coba_mulai::date,
                                    @p_periode_uji_coba_berakhir::date,
                                    @p_link_video_dokumentasi::text
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_model", p_nama_model)
            , new Npgsql.NpgsqlParameter("@p_pemegang_model", p_pemegang_model)
            , new Npgsql.NpgsqlParameter("@p_periode_uji_coba_mulai", p_periode_uji_coba_mulai)
            , new Npgsql.NpgsqlParameter("@p_periode_uji_coba_berakhir", p_periode_uji_coba_berakhir)
            , new Npgsql.NpgsqlParameter("@p_link_video_dokumentasi", p_link_video_dokumentasi)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion

    }
}