using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class hakCipta : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public hakCipta()
        {
            setInitValues();
        }

        ~hakCipta()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool getDataTargetLuaranHakCipta(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL;
            strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_target_luaran_hak_cipta({0});", p_id_target_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        
        public bool insupTargetLuaranHakCipta(int p_id_target_luaran, string p_nama_ciptaan, 
            string p_pemegang_hak_cipta, string p_no_pencatatan, string p_tgl_pencatatan, 
            int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_hak_cipta
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_nama_ciptaan::text,
                                    @p_pemegang_hak_cipta::text,
                                    @p_no_pencatatan::text,
                                    @p_tgl_pencatatan::date,
                                    @p_id_target_jenis_luaran::integer
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_ciptaan", p_nama_ciptaan)
            , new Npgsql.NpgsqlParameter("@p_pemegang_hak_cipta", p_pemegang_hak_cipta)
            , new Npgsql.NpgsqlParameter("@p_no_pencatatan", p_no_pencatatan)
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