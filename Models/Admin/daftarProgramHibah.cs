using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarProgramHibah : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public daftarProgramHibah()
        {
            setInitValues();
        }

        ~daftarProgramHibah()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getJmlRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int as jml_kd_program_hibah FROM (
                            SELECT 1 FROM referensi.program_hibah) AS t1;";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_program_hibah(:limit, :offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaru(string kdProgramHibah, string programHibah,
                string kdJenisKegiatan, string kdJenisInstitusiPenyelenggara)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT referensi.insup_program_hibah(:kd_program_hibah, :program_hibah, 
                        :kd_jenis_kegiatan, :kd_jenis_institusi_penyelenggara);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":kd_program_hibah", kdProgramHibah)
                    , new Npgsql.NpgsqlParameter(":program_hibah", programHibah)
                    , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", kdJenisKegiatan)
                    , new Npgsql.NpgsqlParameter(":kd_jenis_institusi_penyelenggara", kdJenisInstitusiPenyelenggara)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateData(string kdProgramHibah, string programHibah,
                string kdJenisKegiatan, string kdJenisInstitusiPenyelenggara, string kdStsAktif = "1")
        {
            bool isSuccess = false;

            string strSQL = @"SELECT referensi.insup_program_hibah(:kd_program_hibah, :program_hibah, 
                        :kd_jenis_kegiatan, :kd_jenis_institusi_penyelenggara, :kd_sts_aktif);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":kd_program_hibah", kdProgramHibah)
                    , new Npgsql.NpgsqlParameter(":program_hibah", programHibah)
                    , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", kdJenisKegiatan)
                    , new Npgsql.NpgsqlParameter(":kd_jenis_institusi_penyelenggara", kdJenisInstitusiPenyelenggara)
                    , new Npgsql.NpgsqlParameter(":kd_sts_aktif", kdStsAktif));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteData(string kdProgramHibah)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT referensi.del_program_hibah(:kd_program_hibah);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":kd_program_hibah", kdProgramHibah));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public DataTable getRow(string kdProgramHibah)
        {
            DataTable dt = new DataTable();

            string strSQL = @"SELECT * FROM referensi.get_program_hibah(:kd_program_hibah);";

            if (!this._db.FetchDataTable(strSQL, ref dt
                , new Npgsql.NpgsqlParameter(":kd_program_hibah", kdProgramHibah)))
                this._errorMessage = this._db.ErrorMessage;

            return dt;
        }

        #endregion

        #region Private Function

        #endregion
    }
}