using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace simlitekkes.Models.pelaksanaan
{
    public class dokBisnisPlan : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public dokBisnisPlan()
        {
            setInitValues();
        }

        ~dokBisnisPlan()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool insupTargetDokBisnisPlan(
            //int p_id_luaran_publikasi_jurnal,
            int p_id_target_luaran,
            string p_NamaProduk,
            string p_Merek,
            string p_NoSertifikatMerek,

            string p_TglSertifikatMerek,
            string p_NoSertifikatDesainIndustri,
            string p_TglSertifikatDesainIndustri

    )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran_bisnis_plan
                            ( 
                                 @p_id_target_luaran::integer,  
                                 @p_nama_produk::text,
                                 @p_merek::text,
  
                                 @p_no_sertifikat_merek::varchar,
                                 @p_tgl_sertifikat_merek::varchar,
                                 @p_no_sertifikat_desain_industri::varchar,
                                 @p_tgl_sertifikat_desain_industri::varchar
                            );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            //, new Npgsql.NpgsqlParameter("@p_id_luaran_publikasi_jurnal", p_id_luaran_publikasi_jurnal)
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_nama_produk", p_NamaProduk)
            , new Npgsql.NpgsqlParameter("@p_merek", p_Merek)

            , new Npgsql.NpgsqlParameter("@p_no_sertifikat_merek", p_NoSertifikatMerek)
            , new Npgsql.NpgsqlParameter("@p_tgl_sertifikat_merek", p_TglSertifikatMerek)
            , new Npgsql.NpgsqlParameter("@p_no_sertifikat_desain_industri", p_NoSertifikatDesainIndustri)
            , new Npgsql.NpgsqlParameter("@p_tgl_sertifikat_desain_industri", p_TglSertifikatDesainIndustri)


            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getDataTargetLuaranBisnisPlan(ref DataTable dataTable, int p_id_target_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_target_luaran_bisnis_plan(@p_id_target_luaran);";
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