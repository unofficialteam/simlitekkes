using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarPengumumanFile : _abstractModels
    {
        public daftarPengumumanFile()
        {
            setInitValues();
        }
        ~daftarPengumumanFile()
        {

        }

        #region Methods

        public bool getJmlRecords(string strIDPengumuman)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT count(*)::int as jml_pengumuman_file FROM (
                            SELECT 1 FROM news.pengumuman_file WHERE id_pengumuman = '{0}') AS t1;", strIDPengumuman);

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecords(string strIDPengumuman)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM news.list_pengumuman_file(:limit, :offset, :id_pengumuman::uuid);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":id_pengumuman", strIDPengumuman)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsAll(ref DataTable dataTable, string strIDPengumuman)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM  news.list_pengumuman_file_all('{0}');", strIDPengumuman);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaru(string strIDPengumumanFile, string strIDPengumuman, string tbJudulFile, string filePengumuman, string kdUrutan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * from news.insup_pengumuman_file(
                                :id_pengumuman_file::uuid, 
                                :id_pengumuman::uuid, 
                                :judul_file::varchar, 
                                :file_pengumuman::varchar,
                                :urutan::char(2)
                            );";

            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter(":id_pengumuman_file", strIDPengumumanFile)
                    , new Npgsql.NpgsqlParameter(":id_pengumuman", strIDPengumuman)
                    , new Npgsql.NpgsqlParameter(":judul_file", tbJudulFile)
                    , new Npgsql.NpgsqlParameter(":file_pengumuman", filePengumuman)
                    , new Npgsql.NpgsqlParameter(":urutan", kdUrutan)
                   ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool deleteData(string strIDPengumumanFile)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from news.del_pengumuman_file('{0}');", strIDPengumumanFile);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public DataTable getRow(string strIDPengumumanFile)
        {
            string strSQL = string.Format(@"select * from news.get_pengumuman_file('{0}');", strIDPengumumanFile);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                this._errorMessage = this._db.ErrorMessage;
            }

            return dt;
        }

        #endregion
    }
}