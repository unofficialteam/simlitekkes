using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class konferensi : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public konferensi()
        {
            setInitValues();
        }

        ~konferensi()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool insupTargetLuaranKonferensi(
            int p_id_target_luaran ,
            string p_kd_peran_penulis,
            string p_nama_konference,

            string p_nama_lembaga_penyelenggara,
            string p_tempat_penyelenggara,
            string p_tgl_penyelenggaraan_mulai,
            string p_tgl_penyelenggaraan_selesai,

            string p_nama_lembaga_pengindeks,
            string p_url_website,
            string p_judul_artikel 
    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_konferensi
                            ( 
                                 @p_id_target_luaran::INTEGER,
                                 @p_kd_peran_penulis::CHAR(1),
                                 @p_nama_konference::TEXT,

                                 @p_nama_lembaga_penyelenggara::VARCHAR(100),
                                 @p_tempat_penyelenggara::VARCHAR(50),
                                 @p_tgl_penyelenggaraan_mulai::DATE,
                                 @p_tgl_penyelenggaraan_selesai::DATE,

                                 @p_nama_lembaga_pengindeks::VARCHAR(100),
                                 @p_url_website::TEXT,
                                 @p_judul_artikel::TEXT
                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            //, new Npgsql.NpgsqlParameter("@p_id_luaran_publikasi_jurnal", p_id_luaran_publikasi_jurnal)
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_kd_peran_penulis", p_kd_peran_penulis)
            , new Npgsql.NpgsqlParameter("@p_nama_konference", p_nama_konference)

            , new Npgsql.NpgsqlParameter("@p_nama_lembaga_penyelenggara", p_nama_lembaga_penyelenggara)
            , new Npgsql.NpgsqlParameter("@p_tempat_penyelenggara", p_tempat_penyelenggara)
            , new Npgsql.NpgsqlParameter("@p_tgl_penyelenggaraan_mulai", p_tgl_penyelenggaraan_mulai)
            , new Npgsql.NpgsqlParameter("@p_tgl_penyelenggaraan_selesai", p_tgl_penyelenggaraan_selesai)

            , new Npgsql.NpgsqlParameter("@p_nama_lembaga_pengindeks", p_nama_lembaga_pengindeks)
            , new Npgsql.NpgsqlParameter("@p_url_website", p_url_website)
            , new Npgsql.NpgsqlParameter("@p_judul_artikel", p_judul_artikel)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool insupTargetLuaranKonferensi(
            int p_id_target_luaran,
            string p_kd_peran_penulis,
            string p_nama_konference,

            string p_nama_lembaga_penyelenggara,
            string p_tempat_penyelenggara,
            string p_tgl_penyelenggaraan_mulai,
            string p_tgl_penyelenggaraan_selesai,

            string p_nama_lembaga_pengindeks,
            string p_url_website,
            string p_judul_artikel,

            string p_isbn_issn,
            string p_url_video
    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_konferensi
                            ( 
                                 @p_id_target_luaran::INTEGER,
                                 @p_kd_peran_penulis::CHAR(1),
                                 @p_nama_konference::TEXT,

                                 @p_nama_lembaga_penyelenggara::VARCHAR(100),
                                 @p_tempat_penyelenggara::VARCHAR(50),
                                 @p_tgl_penyelenggaraan_mulai::DATE,
                                 @p_tgl_penyelenggaraan_selesai::DATE,

                                 @p_nama_lembaga_pengindeks::VARCHAR(100),
                                 @p_url_website::TEXT,
                                 @p_judul_artikel::TEXT,

                                 @p_isbn_issn::VARCHAR(30),
                                 @p_url_video::TEXT

                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            //, new Npgsql.NpgsqlParameter("@p_id_luaran_publikasi_jurnal", p_id_luaran_publikasi_jurnal)
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_kd_peran_penulis", p_kd_peran_penulis)
            , new Npgsql.NpgsqlParameter("@p_nama_konference", p_nama_konference)

            , new Npgsql.NpgsqlParameter("@p_nama_lembaga_penyelenggara", p_nama_lembaga_penyelenggara)
            , new Npgsql.NpgsqlParameter("@p_tempat_penyelenggara", p_tempat_penyelenggara)
            , new Npgsql.NpgsqlParameter("@p_tgl_penyelenggaraan_mulai", p_tgl_penyelenggaraan_mulai)
            , new Npgsql.NpgsqlParameter("@p_tgl_penyelenggaraan_selesai", p_tgl_penyelenggaraan_selesai)

            , new Npgsql.NpgsqlParameter("@p_nama_lembaga_pengindeks", p_nama_lembaga_pengindeks)
            , new Npgsql.NpgsqlParameter("@p_url_website", p_url_website)
            , new Npgsql.NpgsqlParameter("@p_judul_artikel", p_judul_artikel)

            , new Npgsql.NpgsqlParameter("@p_isbn_issn", p_isbn_issn)
            , new Npgsql.NpgsqlParameter("@p_url_video", p_url_video)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDataTargetLuaranKonferensi(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_konferensi(@p_id_target_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupDokumenBuktiLuaran(Guid p_id_dokumen_bukti_luaran, int p_id_target_luaran, 
            int p_id_jenis_dokumen_bukti_luaran, string p_kd_sts_unggah
            )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_dokumen_bukti_luaran
                            (@p_id_dokumen_bukti_luaran, @p_id_target_luaran, @p_id_jenis_dokumen_bukti_luaran, @p_kd_sts_unggah );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_dokumen_bukti_luaran", p_id_dokumen_bukti_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_jenis_dokumen_bukti_luaran", p_id_jenis_dokumen_bukti_luaran)
            , new Npgsql.NpgsqlParameter("@p_kd_sts_unggah", p_kd_sts_unggah)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        #endregion

    }
}