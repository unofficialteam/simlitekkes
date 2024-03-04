using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class daftarReviewer : _abstractModels
    {
        public daftarReviewer()
        {
            setInitValues();
        }
        #region Methods

        public bool getListStatusKepegawaian(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_status_aktif_kepegawaian();");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listData(ref DataTable dataTable, string pencarian, string kd_sts_aktif, string kd_kategori_reviewer)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_reviewer_nasional('{0}','{1}','{2}',{3},{4});"
                            , pencarian, kd_sts_aktif, kd_kategori_reviewer, this.currentPage * this._rowsPerPage, this.rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarData(string pencarian, string kd_sts_aktif)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.list_reviewer_nasional(@pencarian, @kd_sts_aktif,@offset, @limit);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@pencarian", pencarian)
                , new Npgsql.NpgsqlParameter("@kd_sts_aktif", kd_sts_aktif)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDetailData(Guid id_personal, ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pdpt.get_data_tendik(@id_personal, @id_institusi, @limit, @offset);";
            int limit = 1, offset = 0;
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@id_institusi", Guid.Parse("00000000-0000-0000-0000-000000000000"))
                , new Npgsql.NpgsqlParameter("@offset", offset)
                , new Npgsql.NpgsqlParameter("@limit", limit));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getReviewerByNIDN(string nidn, ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.list_dosen(@nidn);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@nidn", nidn));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertData(string nidn, string kompetensi, string no_sertifikasi, string kd_kategori_reviewer)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.insup_reviewer_nasional(@nidn, @kompetensi, @no_sertifikasi, @kd_kategori_reviewer);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@nidn", nidn)
                , new Npgsql.NpgsqlParameter("@kompetensi", kompetensi)
                , new Npgsql.NpgsqlParameter("@no_sertifikasi", no_sertifikasi)
                , new Npgsql.NpgsqlParameter("@kd_kategori_reviewer", kd_kategori_reviewer)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateData(Guid id_reviewer_nasional, string kompetensi, string no_sertifikasi)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.update_identitas_reviewer_nasional(@id_reviewer_nasional, @kompetensi, @no_sertifikasi);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_reviewer_nasional", id_reviewer_nasional)
                , new Npgsql.NpgsqlParameter("@kompetensi", kompetensi)
                , new Npgsql.NpgsqlParameter("@no_sertifikasi", no_sertifikasi));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteData(Guid id_reviewer, string p_kd_kategori_reviewer)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.set_status_aktif_reviewer_nasional('{0}','{1}','{2}');"
                            , id_reviewer, "0", p_kd_kategori_reviewer);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool setStatusKepegawaian(Guid id_personal, string kd_sts_aktif)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pdpt.set_status_aktif_tendik('{0}','{1}');"
                            , id_personal, kd_sts_aktif);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}