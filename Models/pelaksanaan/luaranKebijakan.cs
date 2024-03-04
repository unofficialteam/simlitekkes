using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class luaranKebijakan : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaranKebijakan()
        {
            setInitValues();
        }

        ~luaranKebijakan()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool ListJenisNaskahKebijakan(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_jenis_naskah_kebijakan();";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        
        public bool getDataTargetLuaranKebijakan(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL;
            strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_target_luaran_kebijakan({0});", p_id_target_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupTargetLuaranKebijakan(int p_id_target_luaran, int p_id_jenis_naskah_kebijakan, 
            string p_lembaga_yg_menerima, string p_nama_pejabat_yg_menerima, string p_jabatan_yg_menerima, 
            string p_tgl_penyerahan, string p_tgl_mulai_penerapan, int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_kebijakan
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_id_jenis_naskah_kebijakan::integer,
                                    @p_lembaga_yg_menerima::character varying,
                                    @p_nama_pejabat_yg_menerima::character varying,
                                    @p_jabatan_yg_menerima::character varying,
                                    @p_tgl_penyerahan::date,
                                    @p_tgl_mulai_penerapan::date,
                                    @p_id_target_jenis_luaran::integer
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_jenis_naskah_kebijakan", p_id_jenis_naskah_kebijakan)
            , new Npgsql.NpgsqlParameter("@p_lembaga_yg_menerima", p_lembaga_yg_menerima)
            , new Npgsql.NpgsqlParameter("@p_nama_pejabat_yg_menerima", p_nama_pejabat_yg_menerima)
            , new Npgsql.NpgsqlParameter("@p_jabatan_yg_menerima", p_jabatan_yg_menerima)
            , new Npgsql.NpgsqlParameter("@p_tgl_penyerahan", p_tgl_penyerahan)
            , new Npgsql.NpgsqlParameter("@p_tgl_mulai_penerapan", p_tgl_mulai_penerapan)
            , new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion

    }
}