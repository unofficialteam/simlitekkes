using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class karyaSeni :  _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public karyaSeni()
        {
            setInitValues();
        }

        ~karyaSeni()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool insupTargetKaryaSeni(
            //int p_id_luaran_publikasi_jurnal,
            int p_id_target_luaran,
            string p_namakaryaseni,
            string p_pemegangkaryaseni,
            string p_nama_pagelaran,
            string p_linkvideopagelaran,

            string p_tglawalpagelaran,

            string p_tglakhirpagelaran

    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_karya_seni
                            ( 
                                 @p_id_target_luaran::integer,  
                                 @p_nama_karya_seni::text,
                                 @p_pemegang_karya_seni::text,
                                 @p_nama_pagelaran::text,
                                 @p_link_video_pagelaran::text,
                                 @p_tgl_awal_pagelaran::varchar,
                                 @p_tgl_awal_pagelaran::varchar
                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            //, new Npgsql.NpgsqlParameter("@p_id_luaran_publikasi_jurnal", p_id_luaran_publikasi_jurnal)
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_karya_seni", p_namakaryaseni)

            , new Npgsql.NpgsqlParameter("@p_pemegang_karya_seni", p_pemegangkaryaseni)
            , new Npgsql.NpgsqlParameter("@p_nama_pagelaran", p_nama_pagelaran)
            , new Npgsql.NpgsqlParameter("@p_link_video_pagelaran", p_linkvideopagelaran)

            , new Npgsql.NpgsqlParameter("@p_tgl_awal_pagelaran", p_tglawalpagelaran)
            , new Npgsql.NpgsqlParameter("@p_tgl_akhir_pagelaran", p_tglakhirpagelaran)


            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getDataTargetLuaranKaryaSeni(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_karya_seni(@p_id_target_luaran);";
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