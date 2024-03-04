using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarRabKelompokBiaya : _abstractModels
    {
        public daftarRabKelompokBiaya()
        {
            setInitValues();
        }
        #region Methods
        public bool getJenisKegiatan(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_jenis_kegiatan({0}, {1});", 0, -1);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listRabKelompokBiaya(ref DataTable dataTable, string id_jenis_kegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_rab_kelompok_biaya_new('{0}', {1}, {2});"
                            , id_jenis_kegiatan, this._rowsPerPage, this._currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarRabKelompokBiaya(string id_jenis_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_rab_kelompok_biaya_new(@id_jenis_kegiatan, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_jenis_kegiatan", id_jenis_kegiatan)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertRabKelompokBiaya(string id_jenis_kegiatan, string kelompok_biaya)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_rab_kelompok_biaya({0},'{1}','{2}');"
                            , 0, id_jenis_kegiatan, kelompok_biaya);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateRabKelompokBiaya(int id_rab_kelompok_biaya, string id_jenis_kegiatan, string kelompok_biaya)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_rab_kelompok_biaya({0},'{1}','{2}');"
                            , id_rab_kelompok_biaya, id_jenis_kegiatan, kelompok_biaya);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteRabKelompokBiaya(int id_rab_kelompok_biaya)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.non_aktif_rab_kelompok_biaya({0});"
                            , id_rab_kelompok_biaya);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}