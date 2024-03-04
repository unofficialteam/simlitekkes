using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class hasilReview : _abstractModels
    {
        public hasilReview()
        {
            this.setInitValues();
        }

        #region Hasil Review Per Skema
        public bool listHasilReviewPerSkema(ref DataTable dataTable, string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, Guid idInstitusi)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_rekap_monitoring_hasil_reviewer_opt(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan, @idInstitusi);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idInstitusi", idInstitusi));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion

        #region Hasil Review Per Kegiatan
        public bool listHasilReviewPerKegiatan(ref DataTable dataTable, string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, Guid idInstitusi, int statusPenilaian, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_monitoring_hasil_reviewer_by_institusi_v1(@thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @idInstitusi, @statusPenilaian, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@idInstitusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@statusPenilaian", statusPenilaian)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarHasilReviewPerKegiatan(string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, Guid idInstitusi, int statusPenilaian, string pencarian)
        {
            bool isSuccess = false;
            //string strSQL = string.Format(@"SELECT * FROM hibah.rekap_monitoring_hasil_reviewer_by_institusi(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @idInstitusi, @statusPenilaian, @search, @limit, @offset);");
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_monitoring_hasil_reviewer_by_institusi_v1(@thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @idInstitusi, @statusPenilaian, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                //, new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@idInstitusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@statusPenilaian", statusPenilaian)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion

        #region Hasil Review Modal
        public bool listHasilReviewModal(ref DataTable dataTable, Guid idUsulan, string kdTahapan, string pencarian)
        {
            bool isSuccess = false;
//            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_monitoring_hasil_reviewer_by_usulan_kegiatan(@idUsulan, @kdTahapan, @search, @limit, @offset, 1);");
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_monitoring_hasil_reviewer_by_usulan_kegiatan_v1(@idUsulan, @kdTahapan);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@idUsulan", idUsulan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                //, new Npgsql.NpgsqlParameter("@search", pencarian)
                //, new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                //, new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarHasilReviewModal(Guid idUsulan, string kdTahapan, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_monitoring_hasil_reviewer_by_usulan_kegiatan(@idUsulan, @kdTahapan, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@idUsulan", idUsulan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}