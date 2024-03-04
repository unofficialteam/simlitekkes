using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarKategoriSBK : _abstractModels
    {
        public daftarKategoriSBK()
        {
            this.setInitValues();
        }


        #region Methods

        public bool listData(ref DataTable dataTable, int id_kategor_sbk, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_kategori_sbk({0},'{1}',{2},{3});",
                            id_kategor_sbk, pencarian, this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarData(int id_kategori_skb, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM referensi.list_kategori_sbk(@id_kategori_skb, @pencarian, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_kategori_skb", id_kategori_skb)
                , new Npgsql.NpgsqlParameter("@pencarian", pencarian)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertData(string kategori_sbk)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM  referensi.insup_kategori_sbk(@id_kategori_sbk, @kategori_sbk);";
            int id_kategori_sbk= -1;
            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_kategori_sbk", id_kategori_sbk)
                , new Npgsql.NpgsqlParameter("@kategori_sbk", kategori_sbk));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateData(int id_kategori_sbk, string kategori_sbk)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM  referensi.insup_kategori_sbk(@id_kategori_sbk, @kategori_sbk);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_kategori_sbk", id_kategori_sbk)
                , new Npgsql.NpgsqlParameter("@kategori_sbk", kategori_sbk));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteData(int id_kategori_sbk)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.set_status_aktif_kategori_sbk({0},'{1}');"
                            , id_kategori_sbk, "0");
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}