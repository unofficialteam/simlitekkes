using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarKlasterPerguruanTinggi : _abstractModels
    {
        public daftarKlasterPerguruanTinggi()
        {
            setInitValues();
        }
        #region Methods

        public bool getListInstitusi(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_all_institusi();");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListKlaster(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_all_klaster();");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListProgramHibah(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_all_program_hibah();");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListTahunKlaster(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_tahun_klaster_perguruan_tinggi();");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listData(ref DataTable dataTable, Guid id_instutsi, string kd_klaster, string tahun_klaster, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_klaster_perguruan_tinggi('{0}','{1}','{2}','{3}','{4}',{5},{6});",
                            "00000000-0000-0000-0000-000000000000", id_instutsi, kd_klaster, tahun_klaster, pencarian, this.rowsPerPage, this.currentPage * this._rowsPerPage);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarData(Guid id_instutsi, string kd_klaster, string tahun_klaster, string pencarian)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM referensi.list_klaster_perguruan_tinggi(@id_klaster_perguruan_tinggi, @id_institusi, @kd_klaster, @tahun_klaster, @pencarian, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_klaster_perguruan_tinggi", Guid.Parse("00000000-0000-0000-0000-000000000000"))
                , new Npgsql.NpgsqlParameter("@id_institusi", id_instutsi)
                , new Npgsql.NpgsqlParameter("@kd_klaster", kd_klaster)
                , new Npgsql.NpgsqlParameter("@tahun_klaster", tahun_klaster)
                , new Npgsql.NpgsqlParameter("@pencarian", pencarian)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDetailData(Guid id_klaster_perguruan_tinggi, ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM referensi.list_klaster_perguruan_tinggi(@id_klaster_perguruan_tinggi, @id_institusi, @kd_klaster, @tahun_klaster, @pencarian, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_klaster_perguruan_tinggi", id_klaster_perguruan_tinggi)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertData(Guid id_institusi, string tahun_klaster, string kd_klaster, string tanggal_data, string kd_program_hibah)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM  referensi.insup_klaster_perguruan_tinggi(@id_klaster_perguruan_tinggi, @id_institusi, @tahun_klaster, @kd_klaster, @tgl_data::date, @kd_program_hibah);";
            DateTime tgl_data = DateTime.Parse(tanggal_data);
            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_klaster_perguruan_tinggi", Guid.Parse("00000000-0000-0000-0000-000000000000"))
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@tahun_klaster", tahun_klaster)
                , new Npgsql.NpgsqlParameter("@kd_klaster", kd_klaster)
                , new Npgsql.NpgsqlParameter("@tgl_data", tgl_data.ToString("yyyy-MM-dd"))
                , new Npgsql.NpgsqlParameter("@kd_program_hibah", kd_program_hibah));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateData(Guid id_klaster_perguruan_tinggi, Guid id_institusi, string tahun_klaster, string kd_klaster, string tanggal_data, string kd_program_hibah)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM  referensi.insup_klaster_perguruan_tinggi(@id_klaster_perguruan_tinggi, @id_institusi, @tahun_klaster, @kd_klaster, @tgl_data::date, @kd_program_hibah);";
            DateTime tgl_data = DateTime.Parse(tanggal_data);
            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_klaster_perguruan_tinggi", id_klaster_perguruan_tinggi)
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@tahun_klaster", tahun_klaster)
                , new Npgsql.NpgsqlParameter("@kd_klaster", kd_klaster)
                , new Npgsql.NpgsqlParameter("@tgl_data", tgl_data.ToString("yyyy-MM-dd"))
                , new Npgsql.NpgsqlParameter("@kd_program_hibah", kd_program_hibah));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool deleteData(Guid id_klaster_perguruan_tinggi)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.set_status_aktif_klaster_perguruan_tinggi('{0}','{1}');"
                            , id_klaster_perguruan_tinggi, "0");
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}