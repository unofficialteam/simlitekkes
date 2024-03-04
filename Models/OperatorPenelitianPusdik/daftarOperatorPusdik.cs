using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{   
    public class daftarOperatorPusdik : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public daftarOperatorPusdik()
        {
            setInitValues();
        }

        ~daftarOperatorPusdik()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool ListRekapUsulanBaru(ref DataTable dataTable, Guid idInstitusi, string thnPelaksanaan, string kodeProgram)
        {
            bool isSuccess = false;
            var strSQL = @"SELECT * from hibah.list_rekap_usulan_baru(null::uuid,@p_thn_pelaksanaan_kegiatan::character(4),@p_kd_program_hibah::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kodeProgram)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool ListRekapUsulanLanjutan(ref DataTable dataTable, Guid idInstitusi, string thnPelaksanaan, string kodeProgram)
        {
            bool isSuccess = false;
            var strSQL = @"SELECT * from hibah.list_rekap_usulan_lanjutan(null::uuid,@p_thn_pelaksanaan_kegiatan::character(4),@p_kd_program_hibah::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kodeProgram)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListRekapUsulanBaruInstitusi(ref DataTable dataTable, string thnPelaksanaan, int idSkema)
        {
            bool isSuccess = false;
            var strSQL = @"SELECT * from hibah.list_rekap_usulan_baru_institusi(
                                @p_thn_pelaksanaan_kegiatan, @p_id_skema);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
                            , new Npgsql.NpgsqlParameter("@p_id_skema", idSkema)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListRekapUsulanLanjutanInstitusi(ref DataTable dataTable, string thnPelaksanaan,
            int idSkema, int? idKategoriPT = null)
        {
            bool isSuccess = false;
            var strSQL = @"SELECT * from hibah.list_rekap_usulan_lanjutan_institusi(
                                @p_thn_pelaksanaan_kegiatan, @p_id_skema,
                                @p_id_kategori_pt);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
                            , new Npgsql.NpgsqlParameter("@p_id_skema", idSkema)
                            , new Npgsql.NpgsqlParameter("@p_id_kategori_pt", (object)idKategoriPT ?? DBNull.Value)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListUsulanBaruPaging(Guid idInstitusi, string thnPelaksanaan, int idSkema, string kode)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.list_summary_usulan_baru(
                                @p_id_institusi::uuid, @p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer, @p_kode, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
            , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_id_skema", idSkema)
            , new Npgsql.NpgsqlParameter("@p_kode", kode)
            , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
            , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;           
        }

        public bool ListUsulanLanjutanPaging(Guid idInstitusi, string thnPelaksanaan, int idSkema, string kode)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.list_summary_usulan_Lanjutan(
                                @p_id_institusi::uuid, @p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer, @p_kode::character, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
            , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_id_skema", idSkema)
            , new Npgsql.NpgsqlParameter("@p_kode", kode)
            , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
            , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlData(Guid idInstitusi, string thnPelaksanaan, int idSkema, string kode)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_data FROM (
                              SELECT 1 FROM hibah.list_summary_usulan_baru(
                                @p_id_institusi::uuid, @p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer, @p_kode)) AS t1;";
            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@p_id_skema", idSkema)
                , new Npgsql.NpgsqlParameter("@p_kode", kode)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlDataLanjutan(Guid idInstitusi, string thnPelaksanaan, int idSkema, string kode)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_data FROM (
                              SELECT 1 FROM hibah.list_summary_usulan_lanjutan(
                                @p_id_institusi::uuid, @p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer, @p_kode::character)) AS t1;";
            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter("@p_id_skema", idSkema)
                , new Npgsql.NpgsqlParameter("@p_kode", kode)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool excelListUsulanBaruPaging(ref DataTable dataTable, Guid idInstitusi, string thnPelaksanaan, int idSkema, string kode)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_summary_usulan_baru_excel('{idInstitusi}','{thnPelaksanaan}',{idSkema},'{kode}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}
