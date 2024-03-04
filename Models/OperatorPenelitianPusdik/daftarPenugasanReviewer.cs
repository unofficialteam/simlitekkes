using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class daftarPenugasanReviewer : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public daftarPenugasanReviewer()
        {
            setInitValues();
        }

        ~daftarPenugasanReviewer()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool daftarRekapPenugasanSkema(ref DataTable dataTable, string idPersonal, string kdProgramHibah,
            string thnUsulanKegiatan, string thnPelaksanaanKegiatan, string kdTahapanKegiatan, string idInstitusi
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_rekap_skema_penugasan_reviewer('{0}','{1}','{2}','{3}','{4}','{5}');"
                            , idPersonal, kdProgramHibah, thnUsulanKegiatan, thnPelaksanaanKegiatan,
                            kdTahapanKegiatan, idInstitusi);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getJmlPenugasan(int idSkema, string thnUsulanKegiatan, string kdTahapanKegiatan,
            Guid idInstitusiYgMenugasi, string thnPelaksanaanKegiatan, int limit = 0, int offset = 0)
        {
            bool isSuccess = false;
            string strSQL;
            strSQL = string.Format(@"SELECT count(*)::int AS jml_penugasan 
                                        FROM hibah.list_penugasan_reviewer_opt_risbang({0},'{1}','{2}','{3}','{4}',{5},{6});",
                                        idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusiYgMenugasi,
                                        thnPelaksanaanKegiatan, limit, offset);
            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarPenugasan(int idSkema, string thnUsulanKegiatan, string kdTahapanKegiatan,
            Guid idInstitusiYgMenugasi, string thnPelaksanaanKegiatan)
        {
            bool isSuccess = false;
            string strSQL;

            strSQL = string.Format(@"SELECT * FROM hibah.list_penugasan_reviewer_opt_risbang({0},'{1}','{2}','{3}','{4}',{5},{6});",
                                    idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaanKegiatan,
                                    this._rowsPerPage, (this._currentPage * this._rowsPerPage));
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getExcelDaftarPenugasan(ref DataTable dataTable, int idSkema, string thnUsulanKegiatan, string kdTahapanKegiatan,
            Guid idInstitusiYgMenugasi, string thnPelaksanaanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT no, id_reviewer, nidn, nama_reviewer, kd_perguruan_tinggi, nama_institusi, 
                                            kompetensi, nomor_telepon, nomor_hp, surel
                                            FROM hibah.list_penugasan_reviewer_opt_risbang({0},'{1}','{2}','{3}','{4}');",
                                            idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getJmlReviewer(int idSkema, string thnUsulanKegiatan, string kdTahapanKegiatan,
            Guid idInstitusiYgMenugasi, string thnPelaksanaanKegiatan, string kdKategoriReviewer,
            string namaReviewer, int limit = 0, int offset = 0)
        {
            bool isSuccess = false;
            string strSQL;

            if (namaReviewer == "")
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_reviewer 
                                        FROM hibah.list_reviewer_blm_ditugaskan({0},'{1}','{2}','{3}','{4}','{5}',{6},{7});",
                                        idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusiYgMenugasi,
                                        thnPelaksanaanKegiatan, kdKategoriReviewer, limit, offset);
                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_reviewer 
                                        FROM hibah.list_reviewer_blm_ditugaskan({0},'{1}','{2}','{3}','{4}','{5}',{6},{7}) 
                                        WHERE nama ~* '{8}' LIMIT {9} OFFSET {10};",
                                        idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusiYgMenugasi,
                                        thnPelaksanaanKegiatan, kdKategoriReviewer, 0, 0, namaReviewer,
                                        this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }

        }

        public bool getDaftarReviewer(int idSkema, string thnUsulanKegiatan, string kdTahapanKegiatan,
            Guid idInstitusiYgMenugasi, string thnPelaksanaanKegiatan, string kdKategoriReviewer,
            string namaReviewer)
        {
            bool isSuccess = false;
            string strSQL;

            if (namaReviewer == "")
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_reviewer_blm_ditugaskan({0},'{1}','{2}','{3}','{4}','{5}',{6},{7});",
                    idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusiYgMenugasi,
                    thnPelaksanaanKegiatan, kdKategoriReviewer, this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_reviewer_blm_ditugaskan({0},'{1}','{2}','{3}','{4}','{5}',{6},{7}) 
                                            WHERE nama ~* '{8}' LIMIT {9} OFFSET {10};",
                                            idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusiYgMenugasi,
                                            thnPelaksanaanKegiatan, kdKategoriReviewer, 0, 0, namaReviewer,
                                            this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
        }

        public bool getExcelBlmDitugaskan(ref DataTable dataTable, int idSkema, string thnUsulanKegiatan, string kdTahapanKegiatan,
            Guid idInstitusiYgMenugasi, string thnPelaksanaanKegiatan, string kdKategoriReviewer)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT no_baris AS no, nidn, nama AS nama_reviewer, kd_perguruan_tinggi, nama_institusi, 
                                            kompetensi, nomor_telepon, nomor_hp, surel
                                            FROM hibah.list_reviewer_blm_ditugaskan({0},'{1}','{2}','{3}','{4}','{5}',{6},{7});",
                                            idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusiYgMenugasi,
                                            thnPelaksanaanKegiatan, kdKategoriReviewer, 0, 0);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertDataBaru(int id_skema, string thn_usulan_kegiatan, Guid id_reviewer, string kd_tahapan_kegiatan, Guid id_institusi_yg_menugasi, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insert_penugasan_reviewer(@id_skema, @thn_usulan_kegiatan, @id_reviewer, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@id_reviewer", id_reviewer)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", id_institusi_yg_menugasi)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteData(Guid id_penugasan_reviewer)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.del_penugasan_reviewer(@id_penugasan_reviewer);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_penugasan_reviewer", id_penugasan_reviewer));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlRecordsReviewerSeleksiAdministrasi(int id_skema, string thn_usulan_kegiatan, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_penugasan FROM (
                            SELECT 1 FROM hibah.list_reviewer_blm_ditugaskan_seleksi_administrasi(@id_skema, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan)) AS t1;";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsReviewerSeleksiAdministrasi(int id_skema, string thn_usulan_kegiatan, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.list_reviewer_blm_ditugaskan_seleksi_administrasi(@id_skema, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlRecordsPenugasanSeleksiAdministrasi(int id_skema, string thn_usulan_kegiatan, string kd_tahapan_kegiatan, Guid id_institusi_yg_menugasi, string thn_pelaksanaan_kegiatan, int limit = 9999, int offset = 0)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_penugasan FROM (
                            SELECT 1 FROM hibah.list_penugasan_reviewer_seleksi_administrasi(@id_skema, @thn_usulan_kegiatan, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, @limit, @offset)) AS t1;";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", id_institusi_yg_menugasi)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@offset", offset)
                , new Npgsql.NpgsqlParameter("@limit", limit));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsPenugasanSeleksiAdministrasi(int id_skema, string thn_usulan_kegiatan, string kd_tahapan_kegiatan, Guid id_institusi_yg_menugasi, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.list_penugasan_reviewer_seleksi_administrasi(@id_skema, @thn_usulan_kegiatan, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", id_institusi_yg_menugasi)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListUsulanBaruExcelOptDRPM(ref DataTable dataTable, string kdTahapan, string thnPertamaUsulan, string thnPelaksanaan, int idSkema)
        {
            bool isSuccess = false;
            string strSQL = @"select *
                                from hibah.list_usulan_baru_penugasan_opt_drpm_rb(
                                @p_kd_tahapan_kegiatan::character(2),
                                @p_thn_pertama_usulan::character(4),
                                @p_thn_pelaksanaan_kegiatan::character(4),
                                @p_id_skema::int, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", kdTahapan)
            , new Npgsql.NpgsqlParameter("@p_thn_pertama_usulan", thnPertamaUsulan)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_id_skema", idSkema)
            , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
            , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListReviewerNasionalExcelOptDRPM(ref DataTable dataTable, string thnPelaksanaan, string thnUsulan, string kdTahapan, Guid idInstitusi)
        {
            bool isSuccess = false;
            string strSQL = @"select *
                                from hibah.list_reviewer_nasional_penugasan_rb(
                                @p_thn_pelaksanaan_kegiatan::character(4),
                                @p_thn_usulan_kegiatan::character(4),
                                @p_kd_tahapan_kegiatan::character(2),
                                @p_id_institusi_yg_menugasi::uuid);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
            , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", thnUsulan)
            , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", kdTahapan)
            , new Npgsql.NpgsqlParameter("@p_id_institusi_yg_menugasi", idInstitusi)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool daftar_tahapan_thn_pelaksanaan(ref DataTable dataTable, string idInstitusi, string thnUsulan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM public.list_konfig_eligible_tahapan_by_klaster_institusi('{0}', '{1}');"
                            , idInstitusi, thnUsulan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion

        #region Private Function

        #endregion
    }
}