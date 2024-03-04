using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class KepalaLembaga : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public KepalaLembaga()
        {
            setInitValues();
        }

        ~KepalaLembaga()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool LisResumeLanjutan(ref DataTable dataTable, string IdPeran, string IdPersonal, string ThnUsulan, string ThnPelaksanaan, string KdProgramHibah)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.list_summary_skema_approvel_lanjutan_rb(@p_id_peran::integer,@p_id_personal::uuid,@p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4),@p_kd_program_hibah::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_peran", IdPeran)
            , new Npgsql.NpgsqlParameter("@p_id_personal", IdPersonal)
            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", ThnUsulan)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", ThnPelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", KdProgramHibah)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool LisResume(ref DataTable dataTable, string id_peran, string id_personal, string thn_usulan, string thn_pelaksanaan, string kdprogram)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.list_summary_skema_approvel_edisi_12_rb(@p_id_peran::integer,@p_id_personal::uuid,@p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4),@p_kd_program_hibah::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_peran", id_peran)
            , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", thn_usulan)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thn_pelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kdprogram)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListBelumdiTinjau(ref DataTable dataTable, string id_peran, string id_personal, string thn_pelaksanaan, string kdprogram)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.list_persetujuan_lppm(@p_id_peran::integer,@p_id_personal::uuid,@p_thn_pelaksanaan_kegiatan::character(4),@p_kd_program_hibah::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_peran", id_peran)
            , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thn_pelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kdprogram)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListBelumdiTinjaurb(ref DataTable dataTable, string id_peran, string id_personal,
            string thn_usulan, string thn_pelaksanaan, string kdprogram, string kode)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.list_persetujuan_lppm_rb(@p_id_peran::integer,
                            @p_id_personal::uuid,@p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4),
                            @p_kd_program_hibah::character,@p_kode::character);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_peran", id_peran)
            , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", thn_usulan)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thn_pelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kdprogram)
            , new Npgsql.NpgsqlParameter("@p_kode", kode)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListBelumdiTinjaurbPaging(string id_peran, string id_personal, string thn_usulan, string thn_pelaksanaan, string kdprogram, string kode)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.list_persetujuan_lppm_rb(@p_id_peran::integer,
                            @p_id_personal::uuid,@p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4),
                            @p_kd_program_hibah::character,@p_kode::character, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
            , new Npgsql.NpgsqlParameter("@p_id_peran", id_peran)
            , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", thn_usulan)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thn_pelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kdprogram)
            , new Npgsql.NpgsqlParameter("@p_kode", kode)
            , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
            , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListBelumdiTinjaulanjutanrbPaging(string id_peran, string id_personal, string thn_pelaksanaan, string kdprogram, string kode)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.list_persetujuan_lanjutan_lppm_rb(@p_id_peran::integer,
                            @p_id_personal::uuid,@p_thn_pelaksanaan_kegiatan::character(4),
                            @p_kd_program_hibah::character,@p_kode::character, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
            , new Npgsql.NpgsqlParameter("@p_id_peran", id_peran)
            , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thn_pelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kdprogram)
            , new Npgsql.NpgsqlParameter("@p_kode", kode)
            , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
            , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlData(string id_peran, string id_personal, string thn_usulan, string thn_pelaksanaan, string kdprogram, string kode)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_data FROM (
                            SELECT 1 FROM hibah.list_persetujuan_lppm_rb(@p_id_peran::integer,
                            @p_id_personal::uuid,@p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4),
                            @p_kd_program_hibah::character,@p_kode::character)) AS t1;";
            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@p_id_peran", id_peran)
                , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", thn_usulan)
                , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thn_pelaksanaan)
                , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kdprogram)
                , new Npgsql.NpgsqlParameter("@p_kode", kode)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlDatalanjutan(string id_peran, string id_personal, string thn_pelaksanaan, string kdprogram, string kode)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_data FROM (
                            SELECT 1 FROM hibah.list_persetujuan_lanjutan_lppm_rb(@p_id_peran::integer,
                            @p_id_personal::uuid,@p_thn_pelaksanaan_kegiatan::character(4),
                            @p_kd_program_hibah::character,@p_kode::character)) AS t1;";
            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@p_id_peran", id_peran)
                , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thn_pelaksanaan)
                , new Npgsql.NpgsqlParameter("@p_kd_program_hibah", kdprogram)
                , new Npgsql.NpgsqlParameter("@p_kode", kode)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool alasanpenolakan(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"select * from referensi.list_alasan_penolakan_approval();";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool InsupApproval(string p_id_transaksi_kegiatan, string p_kd_sts_approvel, string p_komentar, string p_tempat, string p_id_personal, string p_thn_usulan_kegiatan, string p_thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;


            string strSQL = @"select hibah.insup_approvel_usulan_rb(@p_id_transaksi_kegiatan::uuid, @p_kd_sts_approvel::character, " +
                             "@p_komentar::text,@p_tempat::character varying,@p_id_personal::uuid,@p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4))";


            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_kd_sts_approvel", p_kd_sts_approvel)
                            , new Npgsql.NpgsqlParameter("@p_komentar", p_komentar)
                            , new Npgsql.NpgsqlParameter("@p_tempat", p_tempat)
                            , new Npgsql.NpgsqlParameter("@p_id_personal", p_id_personal)
                            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", p_thn_usulan_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)

                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool InsupApprovalLanjutan(string p_id_transaksi_kegiatan, string p_kd_sts_approvel, string p_komentar, string p_tempat, string p_id_personal, string p_thn_usulan_kegiatan, string p_thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;


            string strSQL = @"select hibah.insup_approvel_usulan_lanjutan_rb(@p_id_transaksi_kegiatan::uuid, @p_kd_sts_approvel::character, " +
                             "@p_komentar::text,@p_tempat::character varying,@p_id_personal::uuid,@p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4))";


            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_kd_sts_approvel", p_kd_sts_approvel)
                            , new Npgsql.NpgsqlParameter("@p_komentar", p_komentar)
                            , new Npgsql.NpgsqlParameter("@p_tempat", p_tempat)
                            , new Npgsql.NpgsqlParameter("@p_id_personal", p_id_personal)
                            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", p_thn_usulan_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)

                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool InsupApprovalPengabdianLanjutan(string p_id_transaksi_kegiatan, string p_kd_sts_approvel, string p_komentar, string p_tempat, string p_id_personal, string p_thn_usulan_kegiatan, string p_thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;


            string strSQL = @"select hibah.insup_approvel_usulan_pengabdian_lanjutan_rb(@p_id_transaksi_kegiatan::uuid, @p_kd_sts_approvel::character, " +
                             "@p_komentar::text,@p_tempat::character varying,@p_id_personal::uuid,@p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4))";


            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_kd_sts_approvel", p_kd_sts_approvel)
                            , new Npgsql.NpgsqlParameter("@p_komentar", p_komentar)
                            , new Npgsql.NpgsqlParameter("@p_tempat", p_tempat)
                            , new Npgsql.NpgsqlParameter("@p_id_personal", p_id_personal)
                            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", p_thn_usulan_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)

                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool insupApprovalPengabdian(string p_id_transaksi_kegiatan, string p_kd_sts_approvel, string p_komentar, string p_tempat, string p_id_personal, string p_thn_usulan_kegiatan, string p_thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;


            string strSQL = @"select hibah.insup_approvel_usulan_pengabdian_rb(@p_id_transaksi_kegiatan::uuid, @p_kd_sts_approvel::character, " +
                             "@p_komentar::text,@p_tempat::character varying,@p_id_personal::uuid,@p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4))";


            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_kd_sts_approvel", p_kd_sts_approvel)
                            , new Npgsql.NpgsqlParameter("@p_komentar", p_komentar)
                            , new Npgsql.NpgsqlParameter("@p_tempat", p_tempat)
                            , new Npgsql.NpgsqlParameter("@p_id_personal", p_id_personal)
                            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", p_thn_usulan_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)

                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getidusulankegiatan(string idtransaksi, ref string id_usulan_kegiatan)
        {
            bool returnValue = false;
            try
            {
                string strSQL = string.Format("select id_usulan_kegiatan from hibah.transaksi_kegiatan where id_transaksi_kegiatan='{0}';", idtransaksi);
                this._db.ReadSkalar(strSQL, ref id_usulan_kegiatan);
                returnValue = true;
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            return returnValue;
        }



        #endregion
    }
}