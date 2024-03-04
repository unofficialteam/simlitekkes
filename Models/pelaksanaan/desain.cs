using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class desain : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public desain()
        {
            setInitValues();
        }

        ~desain()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool getDataTargetLuaranDesain(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_desain(@p_id_target_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupTargetLuaranDesain(int p_id_target_luaran, string p_nama_desain, string p_pemegang_desain,
            string p_link_video, string p_tgl_diterapkan, int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_desain
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_nama_desain::text,
                                    @p_pemegang_desain::text,
                                    @p_link_video::text,
                                    @p_tgl_diterapkan::date,
                                    @p_id_target_jenis_luaran::integer
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_desain", p_nama_desain)
            , new Npgsql.NpgsqlParameter("@p_pemegang_desain", p_pemegang_desain)

            , new Npgsql.NpgsqlParameter("@p_link_video", p_link_video)
            , new Npgsql.NpgsqlParameter("@p_tgl_diterapkan", p_tgl_diterapkan)
            , new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insupTargetLuaranDesainProduk(int p_id_target_luaran, string p_nama_desain, string p_pemegang_desain,
            string p_link_video)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_desain_produk
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_nama_desain::text,
                                    @p_pemegang_desain::text,
                                    @p_link_video::text
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_desain", p_nama_desain)
            , new Npgsql.NpgsqlParameter("@p_pemegang_desain", p_pemegang_desain)
            , new Npgsql.NpgsqlParameter("@p_link_video", p_link_video)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insupTargetLuaranDesainProdukIndustri(int p_id_target_luaran, string p_nama_desain, string p_pemegang_desain,
            string p_link_video, string p_no_sertifikat, int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_desain_produk_industri
                                ( 
                                    @p_id_target_luaran::integer,
                                    @p_nama_desain::text,
                                    @p_pemegang_desain::text,
                                    @p_link_video::text,
                                    @p_no_sertifikat::text,
                                    @p_id_target_jenis_luaran::integer
                                );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_desain", p_nama_desain)
            , new Npgsql.NpgsqlParameter("@p_pemegang_desain", p_pemegang_desain)
            , new Npgsql.NpgsqlParameter("@p_link_video", p_link_video)
            , new Npgsql.NpgsqlParameter("@p_no_sertifikat", p_no_sertifikat)
            , new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion

    }
}