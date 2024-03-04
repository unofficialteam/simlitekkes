using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace simlitekkes.Models.pelaksanaan
{
    public class naskahAkademik : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public naskahAkademik()
        {
            setInitValues();
        }

        ~naskahAkademik()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool ListJenisNaskahAkademik(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_jenis_naskah_akademik();";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDataTargetLuaranAkademik(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL;
            strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_target_luaran_naskah_akademik({0});", p_id_target_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupTargetLuaranAkademik(int p_id_target_luaran, int p_id_jenis_naskah_akademik,
            string p_lembaga_yg_menerima)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_naskah_akademik
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_id_jenis_naskah_akademik::integer,
                                    @p_lembaga_yg_menerima::character varying
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_jenis_naskah_akademik", p_id_jenis_naskah_akademik)
            , new Npgsql.NpgsqlParameter("@p_lembaga_yg_menerima", p_lembaga_yg_menerima)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion

    }
}