using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{    
    public class luaranPVT : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaranPVT()
        {
            setInitValues();
        }

        ~luaranPVT()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool insupTargetLuaranPVT(
            int p_id_target_luaran,
            string p_nama_varietas,
            string p_nama_pemulia_varietas,
            string p_nomor_surat_perlindungan_sementara,
            string p_tgl_terbit_surat_perlindungan_sementara,
            string p_nomor_sertifikat_pvt,
            string p_tgl_terbit_sertifikat

    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_pvt
                            ( 
                                 @p_id_target_luaran::integer,  
                                 @p_nama_varietas::text,
                                 @p_nama_pemulia_varietas::text,  
                                 @p_nomor_surat_perlindungan_sementara::varchar,
                                 @p_tgl_terbit_surat_perlindungan_sementara::date,
                                 @p_nomor_sertifikat_pvt::varchar,
                                 @p_tgl_terbit_sertifikat::date
                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_varietas", p_nama_varietas)
            , new Npgsql.NpgsqlParameter("@p_nama_pemulia_varietas", p_nama_pemulia_varietas)
            , new Npgsql.NpgsqlParameter("@p_nomor_surat_perlindungan_sementara", p_nomor_surat_perlindungan_sementara)
            , new Npgsql.NpgsqlParameter("@p_tgl_terbit_surat_perlindungan_sementara", p_tgl_terbit_surat_perlindungan_sementara)
            , new Npgsql.NpgsqlParameter("@p_nomor_sertifikat_pvt", p_nomor_sertifikat_pvt)
            , new Npgsql.NpgsqlParameter("@p_tgl_terbit_sertifikat", p_tgl_terbit_sertifikat)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getDataTargetLuaranPVT(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_pvt(@p_id_target_luaran);";
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
