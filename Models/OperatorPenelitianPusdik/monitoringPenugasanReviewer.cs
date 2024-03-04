using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class monitoringPenugasanReviewer : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public monitoringPenugasanReviewer()
        {
            setInitValues();
        }

        ~monitoringPenugasanReviewer()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool daftarRekapPenugasanReviewer(ref DataTable dataTable, string idPersonal, string kdProgramHibah,
        string thnUsulanKegiatan, string thnPelaksanaanKegiatan, string kdTahapanKegiatan, string idInstitusi
        )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_rekap_skema_penugasan_reviewer('{0}','{1}','{2}','{3}','{4}','{5}');"
                , idPersonal, kdProgramHibah, thnUsulanKegiatan, thnPelaksanaanKegiatan,
                kdTahapanKegiatan, idInstitusi);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarPenugasan(int idSkema, string thnUsulanKegiatan, string kdTahapanKegiatan,
        Guid idInstitusiYgMenugasi, string thnPelaksanaanKegiatan)
        {
            bool isSuccess = false;
            string strSQL;

            strSQL = string.Format(@"SELECT * FROM hibah.list_penugasan_reviewer_opt_risbang({0},'{1}','{2}','{3}','{4}',{5},{6});",
                    idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaanKegiatan,
                    this._rowsPerPage, (this._currentPage * this._rowsPerPage));
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        //public bool daftar_tahapan_thn_pelaksanaan(ref DataTable dataTable, string idInstitusi, string thnUsulan)
        //{
        //    bool isSuccess = false;
        //    string strSQL = string.Format(@"SELECT * FROM public.list_konfig_eligible_tahapan_by_klaster_institusi('{0}', '{1}');"
        //                    , idInstitusi, thnUsulan);
        //    dataTable = new DataTable();
        //    isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        public bool rekap_reviewer_perguruan_tinggi(ref DataTable dataTable, string kdProgramHibah,
        string thnUsulanKegiatan, string thnPelaksanaanKegiatan, string kdTahapanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_reviewer_perguruan_tinggi('{0}','{1}','{2}','{3}');",
                kdProgramHibah, thnUsulanKegiatan, thnPelaksanaanKegiatan, kdTahapanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool rekap_penugasan_reviewer_by_skema(ref DataTable dataTable, string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema)
        {
            bool isSuccess = false;
            int limit = 1; int offset = 0;
            string strSQL = @"SELECT * FROM hibah.rekap_penugasan_reviewer_by_skema(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @search, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@search", "")
                , new Npgsql.NpgsqlParameter("@limit", limit)
                , new Npgsql.NpgsqlParameter("@offset", offset));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listRekapPenugasanReviewerBySkema(ref DataTable dataTable, string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_penugasan_reviewer_by_skema(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this.rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this.currentPage * this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarRekapPenugasanReviewerBySkema(string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_penugasan_reviewer_by_skema(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this.rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this.currentPage * this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listRekapPenugasanReviewerByInstitusi(ref DataTable dataTable, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, Guid idInstitusi, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_penugasan_reviewer_by_institusi(@thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @idInstitusi, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@idInstitusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this.rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this.currentPage * this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarRekapPenugasanReviewerByInstitusi(string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, Guid idInstitusi, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_penugasan_reviewer_by_institusi(@thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @idInstitusi, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@idInstitusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this.rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this.currentPage * this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listRekapPenugasanReviewerPersonal(ref DataTable dataTable, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, Guid idReviewer, string pencarian = "")
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_penugasan_reviewer_by_personal(@thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @idReviewer, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@idReviewer", idReviewer)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this.rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this.currentPage * this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarRekapPenugasanReviewerByPersonal(string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, Guid idReviewer, string pencarian = "")
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_penugasan_reviewer_by_personal(@thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @idReviewer, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@idReviewer", idReviewer)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this.rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this.currentPage * this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool rekap_excel_monitoring_proposal_penugasan_reviewer_pt(ref DataTable dataTable, string kdProgramHibah,
        string thnUsulanKegiatan, string thnPelaksanaanKegiatan, string kdTahapanKegiatan
        )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_excel_monitoring_proposal_penugasan_reviewer_pt('{0}','{1}','{2}','{3}');"
                , kdProgramHibah, thnUsulanKegiatan, thnPelaksanaanKegiatan,
                kdTahapanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool rekap_excel_monitoring_penugasan_reviewer_pt(ref DataTable dataTable, string kdProgramHibah,
        string thnUsulanKegiatan, string thnPelaksanaanKegiatan, string kdTahapanKegiatan
        )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_excel_monitoring_penugasan_reviewer_pt('{0}','{1}','{2}','{3}');"
                , kdProgramHibah, thnUsulanKegiatan, thnPelaksanaanKegiatan,
                kdTahapanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion

        #region Private Function

        #endregion
    }
}