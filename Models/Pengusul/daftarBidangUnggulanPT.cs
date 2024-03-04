using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class daftarBidangUnggulanPT : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public daftarBidangUnggulanPT()
        {
            setInitValues();
        }

        ~daftarBidangUnggulanPT()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods
        //PENELITIAN
        public bool getJmlRecords(string idInstitusi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int as jml_bidang_unggulan_perguruan_tinggi FROM (
                            SELECT 1 FROM penelitian.bidang_unggulan_perguruan_tinggi WHERE id_institusi = '{0}') AS t1;";
            strSQL = string.Format(strSQL, idInstitusi);

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecords(ref DataTable dataTable, string idInstitusi)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM  penelitian.list_bidang_unggulan_perguruan_tinggi('{0}');", idInstitusi);

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaru(Guid id_institusi, string bidang_unggulan_perguruan_tinggi, string tahun_penetapan, string kode_status_aktif)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.insup_bidang_unggulan_pt(:id_institusi, :bidang_unggulan_perguruan_tinggi, :tahun_penetapan, :kode_status_aktif);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_institusi", id_institusi)
                    , new Npgsql.NpgsqlParameter(":bidang_unggulan_perguruan_tinggi", bidang_unggulan_perguruan_tinggi)
                    , new Npgsql.NpgsqlParameter(":tahun_penetapan", tahun_penetapan)
                    , new Npgsql.NpgsqlParameter(":kode_status_aktif", kode_status_aktif));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateData(Guid id_institusi, string bidang_unggulan_perguruan_tinggi, string tahun_penetapan, string kode_status_aktif, Guid id_bidang_unggulan_perguruan_tinggi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.insup_bidang_unggulan_pt(:id_institusi::uuid, :bidang_unggulan_perguruan_tinggi::character varying, :tahun_penetapan::character(4), :kode_status_aktif::character(1), :id_bidang_unggulan_perguruan_tinggi::uuid);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter(":id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter(":bidang_unggulan_perguruan_tinggi", bidang_unggulan_perguruan_tinggi)
                , new Npgsql.NpgsqlParameter(":tahun_penetapan", tahun_penetapan)
                , new Npgsql.NpgsqlParameter(":kode_status_aktif", kode_status_aktif)
                , new Npgsql.NpgsqlParameter(":id_bidang_unggulan_perguruan_tinggi", id_bidang_unggulan_perguruan_tinggi));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool DaftarBidangUnggulanPtAktif(ref DataTable dataTable, Guid idInstitusi)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM  penelitian.list_bidang_unggulan_perguruan_tinggi('{0}') WHERE kode_status_aktif = '1';", idInstitusi);

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        //PENGABDIAN
        public bool getJmlRecordsPengabdian(string idInstitusi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int as jml_bidang_unggulan_perguruan_tinggi FROM (
                            SELECT 1 FROM pengabdian.bidang_unggulan_perguruan_tinggi WHERE id_institusi = '{0}') AS t1;";
            strSQL = string.Format(strSQL, idInstitusi);

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsPengabdian(ref DataTable dataTable, string idInstitusi)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM  pengabdian.list_bidang_unggulan_perguruan_tinggi('{0}');", idInstitusi);

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaruPengabdian(Guid id_institusi, string bidang_unggulan_perguruan_tinggi, string tahun_penetapan, string kode_status_aktif)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pengabdian.insup_bidang_unggulan_pt(:id_institusi, :bidang_unggulan_perguruan_tinggi, :tahun_penetapan, :kode_status_aktif);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_institusi", id_institusi)
                    , new Npgsql.NpgsqlParameter(":bidang_unggulan_perguruan_tinggi", bidang_unggulan_perguruan_tinggi)
                    , new Npgsql.NpgsqlParameter(":tahun_penetapan", tahun_penetapan)
                    , new Npgsql.NpgsqlParameter(":kode_status_aktif", kode_status_aktif));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateDataPengabdian(Guid id_institusi, string bidang_unggulan_perguruan_tinggi, string tahun_penetapan, string kode_status_aktif, Guid id_bidang_unggulan_perguruan_tinggi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pengabdian.insup_bidang_unggulan_pt(:id_institusi::uuid, :bidang_unggulan_perguruan_tinggi::character varying, :tahun_penetapan::character(4), :kode_status_aktif::character(1), :id_bidang_unggulan_perguruan_tinggi::uuid);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter(":id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter(":bidang_unggulan_perguruan_tinggi", bidang_unggulan_perguruan_tinggi)
                , new Npgsql.NpgsqlParameter(":tahun_penetapan", tahun_penetapan)
                , new Npgsql.NpgsqlParameter(":kode_status_aktif", kode_status_aktif)
                , new Npgsql.NpgsqlParameter(":id_bidang_unggulan_perguruan_tinggi", id_bidang_unggulan_perguruan_tinggi));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool DaftarBidangUnggulanPengabdianPtAktif(ref DataTable dataTable, Guid idInstitusi)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM  pengabdian.list_bidang_unggulan_perguruan_tinggi('{0}') WHERE kode_status_aktif = '1';", idInstitusi);

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        #endregion

        #region Private Function

        #endregion
    }
}
