using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class dokPurwarupaLaikIndustri : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public dokPurwarupaLaikIndustri()
        {
            setInitValues();
        }

        ~dokPurwarupaLaikIndustri()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool insupTargetDokHasilUjiCoba(
            //int p_id_luaran_publikasi_jurnal,
            int p_id_target_luaran,
            string p_NamaPurwarupa,
            string p_link_dokumentasi,

            string p_pemegang_purwarupa

    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_dok_purwarupa_laik_industri
                            ( 
                                 @p_id_target_luaran::integer,  
                                 @p_nama_purwarupa::text,
                                 @p_pemegang_purwarupa::text,
                                 @p_link_dokumentasi::text
                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            //, new Npgsql.NpgsqlParameter("@p_id_luaran_publikasi_jurnal", p_id_luaran_publikasi_jurnal)
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_purwarupa", p_NamaPurwarupa)

            , new Npgsql.NpgsqlParameter("@p_pemegang_purwarupa", p_pemegang_purwarupa)
            , new Npgsql.NpgsqlParameter("@p_link_dokumentasi", p_link_dokumentasi)


            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getDataTargetLuaranDokPurwarupaLaikIndustri(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_dok_purwarupa_laik_industri(@p_id_target_luaran);";
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