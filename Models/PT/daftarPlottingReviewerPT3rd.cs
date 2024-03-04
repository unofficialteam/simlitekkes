using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class daftarPlottingReviewerPT3rd : _abstractModels
    {
        #region Fields
        #endregion

        #region Konstruktor dan Destruktor

        public daftarPlottingReviewerPT3rd()
        {
            setInitValues();
        }

        ~daftarPlottingReviewerPT3rd()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool daftar_skema_thn_pelaksanaan(ref DataTable dataTable, string idInstitusi, string thn_pelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.list_skim_klaster_desentralisasi_thn_pelaksanaan('{0}', '{1}');"
                            , idInstitusi, thn_pelaksanaan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getJmlData(int id_skema, string thn_usulan_kegiatan, string thn_pelaksanaan_kegiatan, string kd_tahapan_kegiatan, Guid id_institusi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_data FROM (
                            SELECT 1 FROM penelitian.list_hasil_evaluasi_pt_diatas_standart_deviasi(@id_skema, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan, @kd_tahapan_kegiatan, @id_institusi)) AS t1;";

            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecords(int id_skema, string thn_usulan_kegiatan, string thn_pelaksanaan_kegiatan, string kd_tahapan_kegiatan, Guid id_institusi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM penelitian.list_hasil_evaluasi_pt_diatas_standart_deviasi(@id_skema, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan, @kd_tahapan_kegiatan, @id_institusi, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getReviewer3(int id_skema, string thn_usulan_kegiatan, string thn_pelaksanaan_kegiatan, Guid id_institusi, string kd_tahapan_kegiatan, Guid id_transaksi_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM penelitian.list_kandidat_reviewer3_pt(@id_skema, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan, @id_institusi, @kd_tahapan_kegiatan, @id_transaksi_kegiatan);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_transaksi_kegiatan", id_transaksi_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getFilterReviewer3(int id_skema, string thn_usulan_kegiatan, string thn_pelaksanaan_kegiatan, Guid id_institusi, string kd_tahapan_kegiatan, Guid id_transaksi_kegiatan, string id_reviewer_3)
        {
            bool isSuccess = false;
            string strSQL;

            strSQL = string.Format(@"SELECT * FROM penelitian.list_kandidat_reviewer3_pt(@id_skema, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan, @id_institusi, @kd_tahapan_kegiatan, @id_transaksi_kegiatan) WHERE id_reviewer = '{0}';", id_reviewer_3);

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_transaksi_kegiatan", id_transaksi_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteData(Guid id_penugasan_reviewer)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.del_penugasan_reviewer(@id_penugasan_reviewer);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_penugasan_reviewer", id_penugasan_reviewer));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion

        #region Private Function

        #endregion
    }
}