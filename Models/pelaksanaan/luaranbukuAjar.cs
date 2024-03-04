using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{    
    public class luaranbukuAjar : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaranbukuAjar()
        {
            setInitValues();
        }

        ~luaranbukuAjar()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool insupTargetLuaranBuku(
            int p_id_target_luaran,
            string p_judul_buku,
            string p_nama_penerbit,
            string p_isbn,
            string p_website_penerbit,
            string p_tahun_terbit,
            string p_jml_halaman,
            string p_url_buku

    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_buku
                            ( 
                                 @p_id_target_luaran::integer,  
                                 @p_judul_buku::text,
                                 @p_nama_penerbit::text,  
                                 @p_isbn::varchar,
                                 @p_website_penerbit::varchar,
                                 @p_tahun_terbit::char(4),
                                 @p_jml_halaman::varchar,  
                                 @p_url_buku::text
                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_judul_buku", p_judul_buku)
            , new Npgsql.NpgsqlParameter("@p_nama_penerbit", p_nama_penerbit)
            , new Npgsql.NpgsqlParameter("@p_isbn", p_isbn)
            , new Npgsql.NpgsqlParameter("@p_website_penerbit", p_website_penerbit)
            , new Npgsql.NpgsqlParameter("@p_tahun_terbit", p_tahun_terbit)
            , new Npgsql.NpgsqlParameter("@p_jml_halaman", p_jml_halaman)
            , new Npgsql.NpgsqlParameter("@p_url_buku", p_url_buku)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getDataTargetLuaranBuku(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_buku(@p_id_target_luaran);";
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
