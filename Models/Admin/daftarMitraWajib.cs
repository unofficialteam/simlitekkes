using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarMitraWajib : _abstractModels
    {
        public daftarMitraWajib()
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

        public bool getKategoriMitra(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_daftar_kategori_mitra({0}, {1});", 0, -1);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listMitraWajib(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_mitra_wajib({0}, {1});"
                            , this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarMitraWajib()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.get_mitra_wajib(@limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertMitraWajib(int id_skema, int kd_kategori_mitra)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_mitra_wajib({0},'{1}',{2});", 0,
                            id_skema.ToString(), kd_kategori_mitra);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateMitraWajib(int id_skema_wajib, int id_skema, int kd_kategori_mitra)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_mitra_wajib({0},'{1}',{2});"
                            , id_skema_wajib, id_skema.ToString(), kd_kategori_mitra);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteMitraWajib(int id_skema_wajib)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.non_aktif_mitra_wajib({0});"
                            , id_skema_wajib);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}