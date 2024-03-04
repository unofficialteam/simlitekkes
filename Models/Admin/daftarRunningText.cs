using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarRunningText : _abstractModels
    {
        public daftarRunningText()
        {
            setInitValues();
        }

        ~daftarRunningText()
        {

        }

        #region Methods

        public bool getRunningText(ref DataTable dt,int id_peran, Guid id_personal)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM news.get_running_text_by_peran2(:id_peran, :id_personal);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dt
                , new Npgsql.NpgsqlParameter(":id_peran", id_peran)
                , new Npgsql.NpgsqlParameter(":id_personal", id_personal));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getRunningText(ref DataTable dataTable, int id_peran)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * from news.get_running_text_by_peran(:id_peran::integer);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":id_peran", id_peran)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool getJmlRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int as jml_running_text FROM (
                            SELECT 1 FROM news.running_text) AS t1;";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM news.list_running_text(:limit, :offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaru(string strIDRunningText, DateTime tglMulaiAktif, DateTime tglAkhirAktif, string tbHTMLRunningText)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from news.insup_running_text('{0}','{1}','{2}','{3}');", strIDRunningText, tglMulaiAktif.ToString("yyyy-MM-dd"), tglAkhirAktif.ToString("yyyy-MM-dd"), tbHTMLRunningText);
            DataTable dt = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dt);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;

        }

        public bool insertDataPeran(string strIDPeran, string strIDRunningText)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from news.insup_running_text_peran('{0}','{1}');", strIDPeran, strIDRunningText);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public bool deleteData(string strIDRunningText)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from news.del_running_text('{0}');", strIDRunningText);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public bool deleteDataPeran(string strIDPeran, string strIDRunningText)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from news.del_running_text_peran('{0}','{1}');", strIDPeran, strIDRunningText);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public DataTable getRow(string strIDRunningText)
        {
            string strSQL = string.Format(@"select * from news.get_running_text('{0}');", strIDRunningText);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                this._errorMessage = this._db.ErrorMessage;
            }

            return dt;
        }

        public DataTable getPeran(string kdAplikasi)
        {
            string strSQL = string.Format(@"select * from pengguna.list_peran('{0}');", kdAplikasi);
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