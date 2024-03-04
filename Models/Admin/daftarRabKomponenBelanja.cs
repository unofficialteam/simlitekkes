using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarRabKomponenBelanja : _abstractModels
    {
        public daftarRabKomponenBelanja()
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

        public bool getKelompokBiaya(ref DataTable dataTable, string kd_jenis_kegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_rab_kelompok_biaya('{0}', {1}, {2});", kd_jenis_kegiatan, 0, -1);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getKategoriPenelitian(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_kategori_penelitian({0}, {1});",0, -1);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getKategoriPenelitian(ref DataTable dataTable, int id_rab_komponen_belanja)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_kategori_penelitian_by_rab_komponen_belanja({0});", id_rab_komponen_belanja);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listRabKomponenBelanja(ref DataTable dataTable, string kd_jenis_kegiatan, int id_rab_kelompok)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_rab_komponen_belanja('{0}', {1}, {2}, {3});"
                            , kd_jenis_kegiatan, id_rab_kelompok, this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarRabKomponenBelanja(string kd_jenis_kegiatan, int id_rab_kelompok)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_rab_komponen_belanja(@kd_jenis_kegiatan, @id_rab_kelompok, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kd_jenis_kegiatan", kd_jenis_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_rab_kelompok", id_rab_kelompok)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertRabKomponenBelanja(string kd_jenis_kegiatan, int id_rab_kelompok_biaya, string komponen_belanja, string satuan, string keterangan, int[] id_kategori_penelitian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_rab_komponen_belanja('{0}',{1},{2},'{3}','{4}','{5}','{6}');", kd_jenis_kegiatan,
                            0, id_rab_kelompok_biaya, komponen_belanja, satuan, keterangan, string.Join(",", id_kategori_penelitian));
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateRabKomponenBelanja(string kd_jenis_kegiatan, int id_rab_komponen_belanja, string komponen_belanja, string satuan, string keterangan, int[] id_kategori_penelitian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insup_rab_komponen_belanja('{0}',{1},{2},'{3}','{4}','{5}','{6}');"
                            , kd_jenis_kegiatan, id_rab_komponen_belanja, -1, komponen_belanja, satuan, keterangan, string.Join("," ,id_kategori_penelitian));
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteRabKomponenBelanja(int id_rab_komponen_belanja)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.non_aktif_rab_komponen_belanja({0});"
                            , id_rab_komponen_belanja);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}