using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class monitoringPlottingReviewer : _abstractModels
    {
        public monitoringPlottingReviewer()
        {
            this.setInitValues();
        }

        #region Ploting Reviewer Per Skema
        public bool listPlotingReviewerPerSkema(ref DataTable dataTable, string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_rekap_monitoring_plotting_reviewer(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion

        #region Hasil Review Per PT
        public bool listPlottingReviewerPerPT(ref DataTable dataTable, string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_monitoring_plotting_reviewer_by_skema(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarPlottingReviewerPerPT(string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_monitoring_plotting_reviewer_by_skema(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion

        #region Plotting Reviewer Per Kegiatan
        public bool listPlottingReviewerPerKegiatan(ref DataTable dataTable, string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, Guid idInstitusi, int statusPlotting, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_plotting_reviewer_by_institusi(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @idInstitusi, @statusPlotting, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@idInstitusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@statusPlotting", statusPlotting)
                , new Npgsql.NpgsqlParameter("@search", pencarian)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarPlottingReviewerPerKegiatan(string kdHibah, string thnUsulan, string thnPelaksanaan, string kdTahapan, int idSkema, Guid idInstitusi, int statusPlotting, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_plotting_reviewer_by_institusi(@kdHibah, @thnUsulan, @thnPelaksanaan, @kdTahapan, @idSkema, @idInstitusi, @statusPlotting, @search, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kdHibah", kdHibah)
                , new Npgsql.NpgsqlParameter("@thnUsulan", thnUsulan)
                , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@idSkema", idSkema)
                , new Npgsql.NpgsqlParameter("@idInstitusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@statusPlotting", statusPlotting)
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
        public bool listPlottingReviewerModal(ref DataTable dataTable, Guid idUsulan, string kdTahapan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_plotting_reviewer_by_usulan_kegiatan(@idUsulan, @kdTahapan, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@idUsulan", idUsulan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarPlottingReviewerModal(Guid idUsulan, string kdTahapan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.rekap_plotting_reviewer_by_usulan_kegiatan(@idUsulan, @kdTahapan, @limit, @offset);");
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@idUsulan", idUsulan)
                , new Npgsql.NpgsqlParameter("@kdTahapan", kdTahapan)
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