using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarPengumuman : _abstractModels
    {
        public daftarPengumuman()
        {
            setInitValues();
        }

        ~daftarPengumuman()
        {

        }

        #region Methods
        public bool getJmlRecordsFrontpages()
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT count(*)::int as jml_pengumuman FROM (
                            SELECT 1 FROM news.pengumuman WHERE kd_status_publikasi = '1' AND kd_status_frontpages = '1' ORDER BY tgl_pemberitaan DESC) AS t1 ;");

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsFrontpages()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM news.list_pengumuman_frontpages_limit(:limit, :offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getListPengumumanFile(ref DataTable dt, Guid idPengumuman)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from news.list_pengumuman_file_arsip2('{0}');", idPengumuman);

            isSuccess = this._db.FetchDataTable(strSQL, ref dt);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getMenuPengumumanTahun(ref DataTable dt)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select tahun from news.list_pengumuman_menu() group by tahun order by tahun desc;");

            isSuccess = this._db.FetchDataTable(strSQL, ref dt);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getMenuPengumumanBulan(ref DataTable dt, string tahun)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from news.list_pengumuman_menu() where tahun = '{0}';", tahun);

            isSuccess = this._db.FetchDataTable(strSQL, ref dt);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsResultSearch(string keyword) //Hasil Pencarian
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM news.list_pengumuman_arsip_all(:limit, :offset, :keyword::varchar);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":keyword", keyword)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlRecordsResultSearch(string keyword)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT count(*)::int as jml_pengumuman FROM (
                            SELECT 1 FROM news.pengumuman WHERE kd_status_publikasi = '1' AND judul ilike '%{0}%' ORDER BY tgl_surat DESC) AS t1 ;", keyword);

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int as jml_pengumuman FROM news.pengumuman;";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsArsip(ref DataTable dataTable, string tahun, string bulan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM news.list_pengumuman_arsip_menu('{0}','{1}');", tahun, bulan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM news.list_pengumuman(:limit, :offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getLatestPengumuman(ref DataTable dt)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM news.list_pengumuman(5, 0);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dt);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaru(string strIDPengumuman, string tbTglPemberitaan, string tbJudul, string tbNoSurat,
               string tbTglSurat, string isiPengumuman, string kdStatusPublikasi, string kdStatusFrontpages)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * from news.insup_pengumuman(
                            :id_pengumuman::uuid, 
                            :tgl_pemberitaan::date, 
                            :judul::varchar,  
                            :no_surat::varchar, 
                            :tgl_surat::date , 
                            :isi_pengumuman::text,
                            :kd_status_publikasi::char, 
                            :kd_status_frontpages::char
                            );";

            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter(":id_pengumuman", strIDPengumuman)
                    , new Npgsql.NpgsqlParameter(":tgl_pemberitaan", tbTglPemberitaan)
                    , new Npgsql.NpgsqlParameter(":judul", tbJudul)
                    , new Npgsql.NpgsqlParameter(":no_surat", tbNoSurat)
                    , new Npgsql.NpgsqlParameter(":tgl_surat", tbTglSurat)
                    , new Npgsql.NpgsqlParameter(":isi_pengumuman", isiPengumuman)
                    , new Npgsql.NpgsqlParameter(":kd_status_publikasi", kdStatusPublikasi)
                    , new Npgsql.NpgsqlParameter(":kd_status_frontpages", kdStatusFrontpages)
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

        public bool deleteData(string strIDPengumuman)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from news.del_pengumuman('{0}');", strIDPengumuman);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public bool updateStatusPublikasi(string strIDPengumuman, string StatusPublikasi)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"UPDATE news.pengumuman
                                            SET kd_status_publikasi='{1}'
                                            WHERE id_pengumuman='{0}';", strIDPengumuman, StatusPublikasi);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public bool updateStatusFrontpages(string strIDPengumuman, string StatusFrontpages)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"UPDATE news.pengumuman
                                            SET kd_status_frontpages='{1}'
                                            WHERE id_pengumuman='{0}';", strIDPengumuman, StatusFrontpages);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public DataTable getRow(string strIDPengumuman)
        {
            string strSQL = string.Format(@"select * from news.get_pengumuman('{0}');", strIDPengumuman);
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