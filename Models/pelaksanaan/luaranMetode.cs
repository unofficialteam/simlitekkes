using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class luaranMetode : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaranMetode()
        {
            setInitValues();
        }

        ~luaranMetode()
        {

        }

        #endregion

        #region Properties

        #endregion
        
        #region Methods

        public bool getDataTargetLuaranMetode(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL;
            strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_target_luaran_metode({0});", p_id_target_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupTargetLuaranMetode(int p_id_target_luaran, string p_nama_metode,
            string p_pemegang_metode, string p_periode_uji_coba_mulai, string p_periode_uji_coba_berakhir,
            string p_link_video_dokumentasi)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_metode
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_nama_metode::character varying,
                                    @p_pemegang_metode::character varying,
                                    @p_periode_uji_coba_mulai::date,
                                    @p_periode_uji_coba_berakhir::date,
                                    @p_link_video_dokumentasi::text
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_metode", p_nama_metode)
            , new Npgsql.NpgsqlParameter("@p_pemegang_metode", p_pemegang_metode)
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