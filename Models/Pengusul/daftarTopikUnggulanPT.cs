using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class daftarTopikUnggulanPT : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor
        //
        public daftarTopikUnggulanPT()
        {
            setInitValues();
        }

        ~daftarTopikUnggulanPT()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        //PENELITIAN

        public bool getJmlRecords(string idBidangUnggulanPT)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int as jml_topik_unggulan_perguruan_tinggi FROM (
                            SELECT 1 FROM penelitian.topik_unggulan_perguruan_tinggi WHERE id_bidang_unggulan_perguruan_tinggi = '{0}') AS t1;";
            strSQL = string.Format(strSQL, idBidangUnggulanPT);

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecords(ref DataTable dataTablexxx, string idBidangUnggulanPT)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM  penelitian.list_topik_unggulan_perguruan_tinggi('{0}');", idBidangUnggulanPT);

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaru(Guid id_bidang_unggulan_perguruan_tinggi, string topik_unggulan_perguruan_tinggi, string kode_status_aktif)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.insup_topik_unggulan_pt(:id_bidang_unggulan_perguruan_tinggi, :topik_unggulan_perguruan_tinggi, :kode_status_aktif);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_bidang_unggulan_perguruan_tinggi", id_bidang_unggulan_perguruan_tinggi)
                    , new Npgsql.NpgsqlParameter(":topik_unggulan_perguruan_tinggi", topik_unggulan_perguruan_tinggi)
                    , new Npgsql.NpgsqlParameter(":kode_status_aktif", kode_status_aktif));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateData(Guid id_bidang_unggulan_perguruan_tinggi, string topik_unggulan_perguruan_tinggi, string kode_status_aktif, string id_topik_unggulan_perguruan_tinggi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.insup_topik_unggulan_pt(:id_bidang_unggulan_perguruan_tinggi::uuid, :topik_unggulan_perguruan_tinggi::text, :kode_status_aktif::character(1), :id_topik_unggulan_perguruan_tinggi::uuid);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter(":id_bidang_unggulan_perguruan_tinggi", id_bidang_unggulan_perguruan_tinggi)
                , new Npgsql.NpgsqlParameter(":topik_unggulan_perguruan_tinggi", topik_unggulan_perguruan_tinggi)
                , new Npgsql.NpgsqlParameter(":kode_status_aktif", kode_status_aktif)
                , new Npgsql.NpgsqlParameter(":id_topik_unggulan_perguruan_tinggi", id_topik_unggulan_perguruan_tinggi));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getTopikUnggulanPt(ref DataTable dataTable, string idTopikUnggulanPT)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT t1.id_bidang_unggulan_perguruan_tinggi, t1.topik_unggulan_perguruan_tinggi,
                                        t1.kode_status_aktif, t2.kode_status_aktif AS kode_status_aktif_bidang
                                        FROM penelitian.topik_unggulan_perguruan_tinggi t1 
                                        INNER JOIN penelitian.bidang_unggulan_perguruan_tinggi t2
                                        ON t1.id_bidang_unggulan_perguruan_tinggi = t2.id_bidang_unggulan_perguruan_tinggi
                                        WHERE t1.id_topik_unggulan_perguruan_tinggi = '{0}'", idTopikUnggulanPT);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        //public DataTable getRow(int idBidangFokus)
        //{
        //    DataTable dt = new DataTable();

        //    string strSQL = @"SELECT * FROM referensi.get_bidang_fokus(:id_bidang_fokus);";

        //    if (!this._db.FetchDataTable(strSQL, ref dt
        //        , new Npgsql.NpgsqlParameter(":id_bidang_fokus", idBidangFokus)))
        //        this._errorMessage = this._db.ErrorMessage;

        //    return dt;
        //}



        //PENGABDIAN

        public bool getJmlRecordsPengabdian(string idBidangUnggulanPT)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int as jml_topik_unggulan_perguruan_tinggi FROM (
                            SELECT 1 FROM pengabdian.topik_unggulan_perguruan_tinggi WHERE id_bidang_unggulan_perguruan_tinggi = '{0}') AS t1;";
            strSQL = string.Format(strSQL, idBidangUnggulanPT);

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsPengabdian(ref DataTable dataTablexxx, string idBidangUnggulanPT)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM  pengabdian.list_topik_unggulan_perguruan_tinggi('{0}');", idBidangUnggulanPT);

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaruPengabdian(Guid id_bidang_unggulan_perguruan_tinggi, string topik_unggulan_perguruan_tinggi, string kode_status_aktif)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pengabdian.insup_topik_unggulan_pt(:id_bidang_unggulan_perguruan_tinggi, :topik_unggulan_perguruan_tinggi, :kode_status_aktif);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_bidang_unggulan_perguruan_tinggi", id_bidang_unggulan_perguruan_tinggi)
                    , new Npgsql.NpgsqlParameter(":topik_unggulan_perguruan_tinggi", topik_unggulan_perguruan_tinggi)
                    , new Npgsql.NpgsqlParameter(":kode_status_aktif", kode_status_aktif));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateDataPengabdian(Guid id_bidang_unggulan_perguruan_tinggi, string topik_unggulan_perguruan_tinggi, string kode_status_aktif, string id_topik_unggulan_perguruan_tinggi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pengabdian.insup_topik_unggulan_pt(:id_bidang_unggulan_perguruan_tinggi::uuid, :topik_unggulan_perguruan_tinggi::text, :kode_status_aktif::character(1), :id_topik_unggulan_perguruan_tinggi::uuid);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter(":id_bidang_unggulan_perguruan_tinggi", id_bidang_unggulan_perguruan_tinggi)
                , new Npgsql.NpgsqlParameter(":topik_unggulan_perguruan_tinggi", topik_unggulan_perguruan_tinggi)
                , new Npgsql.NpgsqlParameter(":kode_status_aktif", kode_status_aktif)
                , new Npgsql.NpgsqlParameter(":id_topik_unggulan_perguruan_tinggi", id_topik_unggulan_perguruan_tinggi));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getTopikUnggulanPengabdianPt(ref DataTable dataTable, string idTopikUnggulanPT)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT t1.id_bidang_unggulan_perguruan_tinggi, t1.topik_unggulan_perguruan_tinggi,
                                        t1.kode_status_aktif, t2.kode_status_aktif AS kode_status_aktif_bidang
                                        FROM pengabdian.topik_unggulan_perguruan_tinggi t1 
                                        INNER JOIN pengabdian.bidang_unggulan_perguruan_tinggi t2
                                        ON t1.id_bidang_unggulan_perguruan_tinggi = t2.id_bidang_unggulan_perguruan_tinggi
                                        WHERE t1.id_topik_unggulan_perguruan_tinggi = '{0}'", idTopikUnggulanPT);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        //public DataTable getRow(int idBidangFokus)
        //{
        //    DataTable dt = new DataTable();

        //    string strSQL = @"SELECT * FROM referensi.get_bidang_fokus(:id_bidang_fokus);";

        //    if (!this._db.FetchDataTable(strSQL, ref dt
        //        , new Npgsql.NpgsqlParameter(":id_bidang_fokus", idBidangFokus)))
        //        this._errorMessage = this._db.ErrorMessage;

        //    return dt;
        //}

        #endregion

        #region Private Function

        #endregion
    }
}
