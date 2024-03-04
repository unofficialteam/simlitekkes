using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class publikasiJurnal : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public publikasiJurnal()
        {
            setInitValues();
        }

        ~publikasiJurnal()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool insupTargetLuaranPublikasiJurnal(
            //int p_id_luaran_publikasi_jurnal,
            int p_id_target_luaran ,
            string p_kd_peran_penulis,
            string p_nama_jurnal,

            string p_e_issn,
            string p_nama_lembaga_pengindek,
            string p_url_jurnal,
            string p_judul_artikel,

            string p_tahun,
            string p_volume,
            int p_nomor,
            int p_halaman_awal,

            int p_halaman_akhir,
            string  p_url_artikel,
            string  p_doi, 
            string p_peringkat_akreditasi
    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_publikasi_jurnal
                            ( 
                                 @p_id_target_luaran::integer,  
                                 @p_kd_peran_penulis::char,
                                 @p_nama_jurnal::text,
  
                                 @p_e_issn::varchar,
                                 @p_nama_lembaga_pengindek::varchar,
                                 @p_url_jurnal::text,
                                 @p_judul_artikel::text,
  
                                 @p_tahun::char(4),
                                 @p_volume::varchar,
                                 @p_nomor::int,
                                 @p_halaman_awal::int,
  
                                 @p_halaman_akhir::int,
                                 @p_url_artikel::text,
                                 @p_doi::varchar,
                                 @p_peringkat_akreditasi::varchar
                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            //, new Npgsql.NpgsqlParameter("@p_id_luaran_publikasi_jurnal", p_id_luaran_publikasi_jurnal)
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_kd_peran_penulis", p_kd_peran_penulis)
            , new Npgsql.NpgsqlParameter("@p_nama_jurnal", p_nama_jurnal)

            , new Npgsql.NpgsqlParameter("@p_e_issn", p_e_issn)
            , new Npgsql.NpgsqlParameter("@p_nama_lembaga_pengindek", p_nama_lembaga_pengindek)
            , new Npgsql.NpgsqlParameter("@p_url_jurnal", p_url_jurnal)
            , new Npgsql.NpgsqlParameter("@p_judul_artikel", p_judul_artikel)

            , new Npgsql.NpgsqlParameter("@p_tahun", p_tahun)
            , new Npgsql.NpgsqlParameter("@p_volume", p_volume)
            , new Npgsql.NpgsqlParameter("@p_nomor", p_nomor)
            , new Npgsql.NpgsqlParameter("@p_halaman_awal", p_halaman_awal)

            , new Npgsql.NpgsqlParameter("@p_halaman_akhir", p_halaman_akhir)
            , new Npgsql.NpgsqlParameter("@p_url_artikel", p_url_artikel)
            , new Npgsql.NpgsqlParameter("@p_doi", p_doi)
            , new Npgsql.NpgsqlParameter("@p_peringkat_akreditasi", p_peringkat_akreditasi)

            
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getDataTargetLuaranPublikasiJurnal(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_publikasi_jurnal(@p_id_target_luaran);";
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