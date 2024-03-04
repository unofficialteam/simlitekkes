using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class OperatorPT : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public OperatorPT()
        {
            setInitValues();
        }

        ~OperatorPT()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool ListRekapUsulanBaru(ref DataTable dataTable, Guid idInstitusi, string thnPelaksanaan, string kodeProgram)
        {
            bool isSuccess = false;
            var strSQL = @"SELECT * from hibah.list_rekap_usulan_baru(@p_id_institusi::uuid,@p_thn_pelaksanaan_kegiatan::character(4),@p_kd_program_hibah::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
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
            var strSQL = @"SELECT * from hibah.list_rekap_usulan_Lanjutan(@p_id_institusi::uuid,@p_thn_pelaksanaan_kegiatan::character(4),@p_kd_program_hibah::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kodeProgram)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListUsulanBaruPaging(Guid idInstitusi, string thnPelaksanaan, int idSkema, string kode)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.list_summary_usulan_baru(
                                @p_id_institusi::uuid,@p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer,@p_kode, @limit, @offset);";
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

        public bool ListUsulanBaruExcel(ref DataTable dataTable, Guid idInstitusi, string thnPelaksanaan, int idSkema, string kode)
        {
            bool isSuccess = false;
            string strSQL = @"select nama_ketua,nidn,judul,thn_usulan_kegiatan,thn_pelaksanaan_kegiatan,lama_kegiatan,bidang_fokus,jml_anggota
                                from hibah.list_summary_usulan_baru(
                                @p_id_institusi::uuid,@p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer,@p_kode, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
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
            string strSQL = @"select * from hibah.list_summary_usulan_lanjutan(
                                @p_id_institusi::uuid,@p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer,@p_kode, @limit, @offset);";
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
        public bool ListUsulanLanjutanExcel(ref DataTable dataTable, Guid idInstitusi, string thnPelaksanaan, int idSkema, string kode)
        {
            bool isSuccess = false;
            string strSQL = @"select nama_ketua,nidn,judul,thn_usulan_kegiatan,thn_pelaksanaan_kegiatan,lama_kegiatan,bidang_fokus,jml_anggota
                             from hibah.list_summary_usulan_lanjutan(
                                @p_id_institusi::uuid,@p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer,@p_kode, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
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
                                @p_id_institusi::uuid,@p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer,@p_kode)) AS t1;";
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
                                @p_id_institusi::uuid,@p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::integer,@p_kode)) AS t1;";
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


        public bool getKlasterPt(Guid idInstitusi, string kdProgramHibah, ref string namaKlaster, ref string kdKlaster)
        {
            bool isSuccess = false;
            DataTable dt = new DataTable();
            string strSQL = string.Format(@"SELECT * FROM referensi.get_klaster_perguruan_tinggi_by_prog_hibah('{0}','{1}');",
                idInstitusi.ToString(), kdProgramHibah);
            isSuccess = this._db.FetchDataTable(strSQL, ref dt);
            if (isSuccess && dt.Rows.Count > 0)
            {
                namaKlaster = dt.Rows[0]["nama_klaster"].ToString();
                kdKlaster = dt.Rows[0]["kd_klaster"].ToString();
            }
            else if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListRekapUsulan(ref DataTable dataTable, Guid idInstitusi, string thnUsulanKegiatan, string kdJenisKegiatan)
        {
            bool isSuccess = false;
            var strSQL = @"SELECT * from hibah.list_summary_usulan_proposal_rb(@p_id_institusi::uuid,@p_thn_usulan_kegiatan::character(4),@p_kd_jenis_kegiatan::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", thnUsulanKegiatan)
            , new Npgsql.NpgsqlParameter("@p_kd_jenis_kegiatan", kdJenisKegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListRekapRevInternal(ref DataTable dataTable, Guid idInstitusi, int idPeran)
        {
            bool isSuccess = false;
            var strSQL = @"SELECT * from hibah.list_resume_reviewer_internal(@p_id_institusi::uuid, @p_id_peran::integer);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
            , new Npgsql.NpgsqlParameter("@p_id_peran", idPeran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListRekapUsulanDonut(ref DataTable dataTable, Guid idInstitusi, string thnUsulanKegiatan, string kdJenisKegiatan)
        {
            bool isSuccess = false;
            var strSQL = @"SELECT * from hibah.list_summary_usulan_proposal_baru_donut(@p_id_institusi::uuid,@p_thn_usulan_kegiatan::character(4),@p_kd_jenis_kegiatan::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", thnUsulanKegiatan)
            , new Npgsql.NpgsqlParameter("@p_kd_jenis_kegiatan", kdJenisKegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getRekapUsulanDonut(ref DataTable dataTable, Guid idInstitusi, string thnUsulanKegiatan, string kdJenisKegiatan)
        {
            bool isSuccess = false;
            var strSQL = @"SELECT SUM(value) AS jml_usulan from hibah.list_summary_usulan_proposal_baru_donut(@p_id_institusi::uuid,@p_thn_usulan_kegiatan::character(4),@p_kd_jenis_kegiatan::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_institusi", idInstitusi)
            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", thnUsulanKegiatan)
            , new Npgsql.NpgsqlParameter("@p_kd_jenis_kegiatan", kdJenisKegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getBidangTopikUnggulanPT(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;
            var strSQL = @"select * from hibah.list_bidang_topik_unggulan_pt_by_usulan_kegiatan('{0}');";
            strSQL = string.Format(strSQL, idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getBidangTopikRirn(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;
            var strSQL = @"select * from hibah.list_bidang_topik_rirn('{0}');";
            strSQL = string.Format(strSQL, idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}