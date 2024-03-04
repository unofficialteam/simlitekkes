using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarKategoriMitra : _abstractModels
    {
        public daftarKategoriMitra()
        {
            setInitValues();
        }
        #region Methods
        public bool getNamaSkema(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_daftar_skema_kegiatan({0}, {1});", 0, -1);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getNamaSkema(ref DataTable dataTable, int id_kategori_mitra)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_skema_kegiatan_by_kategori_mitra({0});", id_kategori_mitra);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listKategoriMitra(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_daftar_kategori_mitra({0}, {1});"
                            , this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarKategoriMitra()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.get_daftar_kategori_mitra(@limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertKategoriMitra(string kategori_mitra, int[] id_skema)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_kategori_mitra({0},'{1}','{2}');", 0, kategori_mitra, string.Join(",", id_skema));
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateKategoriMitra(int id_kategori_mitra, string kategori_mitra, int[] id_skema)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_kategori_mitra({0},'{1}','{2}');"
                            , id_kategori_mitra, kategori_mitra, string.Join(",", id_skema));
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteKategoriMitra(int id_kategori_mitra)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.non_aktif_kategori_mitra({0});"
                            , id_kategori_mitra);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}