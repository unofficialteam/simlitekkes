using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarTahapanKegiatan : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public daftarTahapanKegiatan()
        {
            setInitValues();
        }

        ~daftarTahapanKegiatan()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        //public bool getJmlRecords()
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT count(*)::int AS jml_tahapan_kegiatan FROM (
        //                    SELECT 1 FROM referensi.tahapan_kegiatan) AS t1;";

        //    isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        public DataTable getTahunPelaksanaan(string thnUsulan)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("thn_pelaksanaan", typeof(string)));
            string strSQL = @"SELECT * FROM public.list_konfig_tahun_pelaksanaan(:thn_usulan);";

            //Core.database aDB = new Core.database();
            //if (!aDB.FetchDataTable(strSQL, ref dtResult
            //    , new Npgsql.NpgsqlParameter(":thn_usulan", thnUsulan)))
            //    this._errorMessage = aDB.errorMessage;

            if (!this._db.FetchDataTable(strSQL, ref dtResult
                , new Npgsql.NpgsqlParameter(":thn_usulan", thnUsulan)))
                this._errorMessage = this._db.ErrorMessage;

            return dtResult;

        }

        public bool getTahapanKegiatan(string kdJenisKegiatan, ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM referensi.list_tahapan_kegiatan_per_jenis_kegiatan(:kd_jenis_kegiatan);";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", kdJenisKegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        //public bool getCurrRecords()
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM referensi.list_tahapan_kegiatan(:limit, :offset);";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
        //        , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
        //        , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool insertDataBaru(string kd_tahapan_kegiatan, string tahapan, string kd_kategori_tahapan, string kd_sts_tahapan_evaluasi)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT referensi.insup_tahapan_kegiatan(:kd_tahapan_kegiatan, :tahapan, :kd_kategori_tahapan, :kd_sts_tahapan_evaluasi);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", kd_tahapan_kegiatan)
        //            , new Npgsql.NpgsqlParameter(":tahapan", tahapan)
        //            , new Npgsql.NpgsqlParameter(":kd_kategori_tahapan", kd_kategori_tahapan)
        //            , new Npgsql.NpgsqlParameter(":kd_sts_tahapan_evaluasi", kd_sts_tahapan_evaluasi));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool updateData(string tahapan, string kd_kategori_tahapan, string kd_sts_tahapan_evaluasi, string kdStsAktif = "1")
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT referensi.insup_tahapan_kegiatan(:tahapan, :kd_kategori_tahapan, :kd_sts_tahapan_evaluasi, :kd_sts_aktif);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":tahapan", tahapan)
        //            , new Npgsql.NpgsqlParameter(":kd_kategori_tahapan", kd_kategori_tahapan)
        //            , new Npgsql.NpgsqlParameter(":kd_sts_tahapan_evaluasi", kd_sts_tahapan_evaluasi)
        //            , new Npgsql.NpgsqlParameter(":kd_sts_aktif", kdStsAktif));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool deleteData(string kd_tahapan_kegiatan)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT referensi.del_tahapan_kegiatan(:kd_tahapan_kegiatan);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", kd_tahapan_kegiatan));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public DataTable getRow(string kd_tahapan_kegiatan)
        //{
        //    DataTable dt = new DataTable();

        //    string strSQL = @"SELECT * FROM referensi.get_tahapan_kegiatan(:kd_tahapan_kegiatan);";

        //    if (!this._db.FetchDataTable(strSQL, ref dt
        //        , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", kd_tahapan_kegiatan)))
        //        this._errorMessage = this._db.ErrorMessage;

        //    return dt;
        //}

        #endregion

        #region Private Function

        #endregion
    }
}