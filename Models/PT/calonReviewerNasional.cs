using simlitekkes.Models;
using System;
using System.Data;

namespace simlitekkes.Models.PT
{
    public class calonReviewerNasional : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public calonReviewerNasional()
        {
            setInitValues();
        }

        ~calonReviewerNasional()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool listReviewer(ref DataTable dataTable, Guid id_institusi, string p_is_disetujui, int p_jml_data=0, int p_offset=0)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.list_calon_reviewer_penelitian_by_institusi(@p_id_institusi::uuid, @p_is_disetujui::char);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@p_id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@p_is_disetujui", p_is_disetujui)
                , new Npgsql.NpgsqlParameter("@p_jml_data", p_jml_data)
                , new Npgsql.NpgsqlParameter("@p_offset", p_offset)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertCalonReviewerPenelitian(Guid p_id_personal, string p_no_sertifikat,
                string p_tgl_akhir_berlaku, string p_kompetensi, Guid p_id_personal_pengentri)
        {
            bool isSuccess = false;

            string strSQL = @"select * from hibah.insert_calon_reviewer_penelitian(
                                @p_id_personal::uuid,
                                @p_no_sertifikat::text,
                                @p_tgl_akhir_berlaku::date,
                                @p_kompetensi::text,
                                @p_id_personal_pengentri::uuid);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_personal", p_id_personal)
                    , new Npgsql.NpgsqlParameter("@p_no_sertifikat", p_no_sertifikat)
                    , new Npgsql.NpgsqlParameter("@p_tgl_akhir_berlaku", p_tgl_akhir_berlaku)
                    , new Npgsql.NpgsqlParameter("@p_kompetensi", p_kompetensi)
                    , new Npgsql.NpgsqlParameter("@p_id_personal_pengentri", p_id_personal_pengentri)
                    );

            return isSuccess;
        }

        public bool updateCalonReviewerPenelitian(Guid p_id_reviewer_nasional_penelitian, 
                string p_no_sertifikat,
                string p_tgl_akhir_berlaku, 
                string p_kompetensi, 
                Guid p_id_personal_pengentri)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.update_calon_reviewer_penelitian(
                                @p_id_calon_reviewer_penelitian::uuid,
                                @p_no_sertifikat::text,
                                @p_tgl_akhir_berlaku::date,
                                @p_kompetensi::text,
                                @p_id_personal_pengentri::uuid);";
            isSuccess = this._db.ExecuteNonQuery(strSQL 
                    , new Npgsql.NpgsqlParameter("@p_id_calon_reviewer_penelitian", p_id_reviewer_nasional_penelitian)
                    , new Npgsql.NpgsqlParameter("@p_no_sertifikat", p_no_sertifikat)
                    , new Npgsql.NpgsqlParameter("@p_tgl_akhir_berlaku", p_tgl_akhir_berlaku)
                    , new Npgsql.NpgsqlParameter("@p_kompetensi", p_kompetensi)
                    , new Npgsql.NpgsqlParameter("@p_id_personal_pengentri", p_id_personal_pengentri)
                    );

            return isSuccess;
        }

        public bool hapusCalonReviewerPenelitian(string p_id_reviewer_nasional_penelitian)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.del_calon_reviewer_penelitian(
                              @p_id_reviewer_nasional_penelitian::uuid);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_reviewer_nasional_penelitian", p_id_reviewer_nasional_penelitian)
                    );

            return isSuccess;
        }

        public bool updatePenetapansCalonReviewerPenelitian(string p_id_reviewer_nasional_penelitian, string p_is_disetujui)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.update_penetapan_calon_reviewer_penelitian(
                                @p_id_reviewer_nasional_penelitian::uuid,
                                @p_is_disetujui::char);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_reviewer_nasional_penelitian", p_id_reviewer_nasional_penelitian)
                    , new Npgsql.NpgsqlParameter("@p_is_disetujui", p_is_disetujui)
                    );

            return isSuccess;
        }

        public bool updateStatusUnggahSertifikatCalonReviewerPenelitian(string p_id_reviewer_nasional_penelitian)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.update_status_unggah_sertifikat(
                              @p_id_reviewer_nasional_penelitian::uuid);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_reviewer_nasional_penelitian", p_id_reviewer_nasional_penelitian)
                    );

            return isSuccess;
        }
        
        public bool getDosen(ref DataTable dt, string nidn, string idInstitusi)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pdpt.get_dosen_by_nidn_rb(@nidn::char(10)) t1 where t1.id_institusi='{0}'::uuid;";
            strSQL = string.Format(strSQL, idInstitusi);
            isSuccess = this._db.FetchDataTable(strSQL, ref dt
                , new Npgsql.NpgsqlParameter("@nidn", nidn)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        #endregion

        #region Private Function

        #endregion
    }
}
