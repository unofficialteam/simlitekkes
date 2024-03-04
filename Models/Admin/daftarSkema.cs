using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class daftarSkema : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public daftarSkema()
        {
            setInitValues();
        }

        ~daftarSkema()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getJmlRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_skema FROM referensi.skema_kegiatan;";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_skema_kegiatan(:limit, :offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool insertDataBaru(string kd_program_hibah, string kd_skema, string nama_skema, string nama_singkat_skema, int jml_minimal_personil,
            int jml_maksimal_personil, int jml_maksimal_keikutsertaan, int jml_maksimal_sbg_ketua, int kd_jenjang_pendidikan_minimal_ketua,
            decimal dana_maksimal_thn_berjalan, decimal dana_minimal_thn_berjalan, string keterangan, int beda_nilai_maks, decimal passing_grade)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT referensi.insup_skema_kegiatan(0::integer, @kd_program_hibah::character, @kd_skema::character varying,
                @nama_skema::character varying, @nama_singkat_skema::character varying, @jml_minimal_personil::smallint, @jml_maksimal_personil::smallint,
                @jml_maksimal_keikutsertaan::smallint, @jml_maksimal_sbg_ketua::smallint, @kd_jenjang_pendidikan_minimal_ketua::integer,
                @dana_maksimal_thn_berjalan::money, @dana_minimal_thn_berjalan::money, @keterangan::text, @beda_nilai_maks::integer,
                @passing_grade::numeric);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@kd_program_hibah", kd_program_hibah)
                    , new Npgsql.NpgsqlParameter("@kd_skema", kd_skema)
                    , new Npgsql.NpgsqlParameter("@nama_skema", nama_skema)
                    , new Npgsql.NpgsqlParameter("@nama_singkat_skema", nama_singkat_skema)
                    , new Npgsql.NpgsqlParameter("@jml_minimal_personil", jml_minimal_personil)
                    , new Npgsql.NpgsqlParameter("@jml_maksimal_personil", jml_maksimal_personil)
                    , new Npgsql.NpgsqlParameter("@jml_maksimal_keikutsertaan", jml_maksimal_keikutsertaan)
                    , new Npgsql.NpgsqlParameter("@jml_maksimal_sbg_ketua", jml_maksimal_sbg_ketua)
                    , new Npgsql.NpgsqlParameter("@kd_jenjang_pendidikan_minimal_ketua", kd_jenjang_pendidikan_minimal_ketua)
                    , new Npgsql.NpgsqlParameter("@dana_maksimal_thn_berjalan", dana_maksimal_thn_berjalan)
                    , new Npgsql.NpgsqlParameter("@dana_minimal_thn_berjalan", dana_minimal_thn_berjalan)
                    , new Npgsql.NpgsqlParameter("@keterangan", keterangan)
                    , new Npgsql.NpgsqlParameter("@beda_nilai_maks", beda_nilai_maks)
                    , new Npgsql.NpgsqlParameter("@passing_grade", passing_grade));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateData(int id_skema, string kd_program_hibah, string kd_skema, string nama_skema, string nama_singkat_skema,
            int jml_minimal_personil, int jml_maksimal_personil, int jml_maksimal_keikutsertaan, int jml_maksimal_sbg_ketua,
            int kd_jenjang_pendidikan_minimal_ketua, decimal dana_maksimal_thn_berjalan, decimal dana_minimal_thn_berjalan,
            string keterangan, int beda_nilai_maks, decimal passing_grade, string kd_sts_aktif)
        {

            bool isSuccess = false;

            string strSQL = @"SELECT referensi.insup_skema_kegiatan(@id_skema::integer, @kd_program_hibah::character, @kd_skema::character varying,
                @nama_skema::character varying, @nama_singkat_skema::character varying, @jml_minimal_personil::smallint, @jml_maksimal_personil::smallint,
                @jml_maksimal_keikutsertaan::smallint, @jml_maksimal_sbg_ketua::smallint, @kd_jenjang_pendidikan_minimal_ketua::integer,
                @dana_maksimal_thn_berjalan::money, @dana_minimal_thn_berjalan::money, @keterangan::text, @beda_nilai_maks::integer,
                @passing_grade::numeric, @kd_sts_aktif::character);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_skema", id_skema)
                    , new Npgsql.NpgsqlParameter(":kd_program_hibah", kd_program_hibah)
                    , new Npgsql.NpgsqlParameter(":kd_skema", kd_skema)
                    , new Npgsql.NpgsqlParameter(":nama_skema", nama_skema)
                    , new Npgsql.NpgsqlParameter(":nama_singkat_skema", nama_singkat_skema)
                    , new Npgsql.NpgsqlParameter(":jml_minimal_personil", jml_minimal_personil)
                    , new Npgsql.NpgsqlParameter(":jml_maksimal_personil", jml_maksimal_personil)
                    , new Npgsql.NpgsqlParameter(":jml_maksimal_keikutsertaan", jml_maksimal_keikutsertaan)
                    , new Npgsql.NpgsqlParameter(":jml_maksimal_sbg_ketua", jml_maksimal_sbg_ketua)
                    , new Npgsql.NpgsqlParameter(":kd_jenjang_pendidikan_minimal_ketua", kd_jenjang_pendidikan_minimal_ketua)
                    , new Npgsql.NpgsqlParameter(":dana_maksimal_thn_berjalan", dana_maksimal_thn_berjalan)
                    , new Npgsql.NpgsqlParameter(":dana_minimal_thn_berjalan", dana_minimal_thn_berjalan)
                    , new Npgsql.NpgsqlParameter(":keterangan", keterangan)
                    , new Npgsql.NpgsqlParameter(":beda_nilai_maks", beda_nilai_maks)
                    , new Npgsql.NpgsqlParameter(":passing_grade", passing_grade)
                    , new Npgsql.NpgsqlParameter(":kd_sts_aktif", kd_sts_aktif));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteData(int id_skema)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT referensi.del_skema_kegiatan(:id_skema);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_skema", id_skema));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public DataTable getRow(int id_skema)
        {
            DataTable dt = new DataTable();

            string strSQL = @"SELECT * FROM referensi.get_skema_kegiatan(:id_skema);";

            if (!this._db.FetchDataTable(strSQL, ref dt
                , new Npgsql.NpgsqlParameter(":id_skema", id_skema)))
                this._errorMessage = this._db.ErrorMessage;

            return dt;
        }

        #endregion

        #region Private Function

        #endregion
    }
}