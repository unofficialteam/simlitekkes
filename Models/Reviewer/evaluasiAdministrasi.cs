using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Reviewer
{
    public class evaluasiAdministrasi : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public evaluasiAdministrasi()
        {
            setInitValues();
        }

        ~evaluasiAdministrasi()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getDaftarEvaluasiSkema(ref DataTable dataTable, Guid IDPersonal, string KdTahapanKegiatan,
                                                string ThnUsulanKegiatan, string ThnPelaksanaanKegiatan, string id_institusi_yg_menugasi)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_evaluasi_skema_penelitian_rb('{0}','{1}',ARRAY['1','6'],'{2}','{3}','{4}');",
                                          IDPersonal, KdTahapanKegiatan, ThnUsulanKegiatan, ThnPelaksanaanKegiatan, id_institusi_yg_menugasi);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarEvaluasiSkemaPengabdian(ref DataTable dataTable, Guid IDPersonal, string KdTahapanKegiatan,
                                                string ThnUsulanKegiatan, string ThnPelaksanaanKegiatan, string id_institusi_yg_menugasi)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_evaluasi_skema_penelitian_rb('{0}','{1}',ARRAY['3','7','8'],'{2}','{3}','{4}');",
                                          IDPersonal, KdTahapanKegiatan, ThnUsulanKegiatan, ThnPelaksanaanKegiatan, id_institusi_yg_menugasi);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarInstitusiYgMenugasiSeminarHasil(ref DataTable dataTable, Guid IDPersonal, string KdTahapanKegiatan,
                                                string ThnUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_institusi_yang_menugasi('{0}','{1}',ARRAY['1','6','3','7'],'{2}');",
                                          IDPersonal, KdTahapanKegiatan, ThnUsulanKegiatan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getJmlData(string id_personal, string p_id_skema, string p_kd_tahapan_kegiatan, string p_thn_usulan_kegiatan, string p_thn_pelaksanaan_kegiatan, string p_id_institusi_yg_menugasi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_data FROM (
                            SELECT 1 FROM hibah.list_evaluasi_administrasi_penelitian(@p_id_personal::uuid,
                            @p_id_skema::integer,@p_kd_tahapan_kegiatan::character(2),
                            @p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4),@p_id_institusi_yg_menugasi::uuid)) AS t1;";
            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@p_id_skema", p_id_skema)
                , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", p_kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", p_thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_id_institusi_yg_menugasi", p_id_institusi_yg_menugasi)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool setPermanenEvaluasiUsulanPenelitian(Guid IDPersonal, int IDSkema, string KdTahapanKegiatan,
                                        string ThnUsulanKegiatan, string ThnPelaksanaanKegiatan, Guid id_institusi_yg_menugasi)
        {
            bool isSuccess = false;

            string strSQL = "SELECT hibah.update_permanen_evaluasi_usulan_penelitian(@p_id_personal, " +
                                "@p_id_skema, @p_kd_tahapan_kegiatan, @p_thn_usulan_kegiatan,@p_thn_pelaksanaan_kegiatan, @p_id_institusi_yg_menugasi);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_personal", IDPersonal)
                    , new Npgsql.NpgsqlParameter("@p_id_skema", IDSkema)
                    , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@p_id_institusi_yg_menugasi", id_institusi_yg_menugasi)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool generatetahapanevaluasisubtansi(Guid IDPersonal, int IDSkema, string KdTahapanKegiatan,
                                        string ThnUsulanKegiatan, string ThnPelaksanaanKegiatan, Guid id_institusi_yg_menugasi)
        {
            bool isSuccess = false;

            string strSQL = "SELECT hibah.insert_tahapan_evaluasi_usulan(@p_id_personal::uuid, " +
                                "@p_id_skema::integer, @p_kd_tahapan_kegiatan::character(2), @p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4), @p_id_institusi_yg_menugasi::uuid);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_personal", IDPersonal)
                    , new Npgsql.NpgsqlParameter("@p_id_skema", IDSkema)
                    , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@p_id_institusi_yg_menugasi", id_institusi_yg_menugasi)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool ListEvaluasiAdmrbPaging(string id_personal, string p_id_skema, string p_kd_tahapan_kegiatan, string p_thn_usulan_kegiatan, string p_thn_pelaksanaan_kegiatan, string p_id_institusi_yg_menugasi)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.list_evaluasi_administrasi_penelitian(@p_id_personal::uuid,
                            @p_id_skema::integer,@p_kd_tahapan_kegiatan::character(2),
                            @p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4),@p_id_institusi_yg_menugasi::uuid, @p_jml_data::integer, @offset::integer);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@p_id_skema", p_id_skema)
                , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", p_kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", p_thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_id_institusi_yg_menugasi", p_id_institusi_yg_menugasi)
                , new Npgsql.NpgsqlParameter("@p_jml_data", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getDataUsulan(ref DataTable dataTable, Guid IDPlottingReviewer)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM penelitian.get_evaluasi_usulan_kegiatan('{0}');",
                                          IDPlottingReviewer);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDatadaftarplotting(ref string[] arrUsulan, string id_personal, string p_id_skema, string p_kd_tahapan_kegiatan, string p_thn_usulan_kegiatan, string p_thn_pelaksanaan_kegiatan, string p_id_institusi_yg_menugasi)
        {
            bool isSuccess = false;
            DataTable dataTable = new DataTable();

            //string strSQL = string.Format("SELECT * FROM penelitian.get_evaluasi_usulan_kegiatan('{0}');",
            //                              IDPlottingReviewer);

            string strSQL = @"SELECT * FROM hibah.list_evaluasi_administrasi_penelitian(@p_id_personal::uuid,
                            @p_id_skema::integer,@p_kd_tahapan_kegiatan::character(2),
                            @p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4),@p_id_institusi_yg_menugasi::uuid);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@p_id_skema", p_id_skema)
                , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", p_kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", p_thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_id_institusi_yg_menugasi", p_id_institusi_yg_menugasi)
            //, new Npgsql.NpgsqlParameter("@p_jml_data", this._rowsPerPage)
            //, new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
            );
            //isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (isSuccess)
            {
                DataRow[] rows = dataTable.Select();
                arrUsulan = Array.ConvertAll(rows, row => row["id_plotting_reviewer"].ToString());
            }
            else
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getdaftarplotting(ref DataTable dt, string id_personal, string p_id_skema, string p_kd_tahapan_kegiatan, string p_thn_usulan_kegiatan, string p_thn_pelaksanaan_kegiatan, string p_id_institusi_yg_menugasi)
        {
            bool isSuccess = false;
            DataTable dataTable = new DataTable();

            //string strSQL = string.Format("SELECT * FROM penelitian.get_evaluasi_usulan_kegiatan('{0}');",
            //                              IDPlottingReviewer);

            string strSQL = @"SELECT * FROM hibah.list_evaluasi_administrasi_penelitian(@p_id_personal::uuid,
                            @p_id_skema::integer,@p_kd_tahapan_kegiatan::character(2),
                            @p_thn_usulan_kegiatan::character(4),@p_thn_pelaksanaan_kegiatan::character(4),@p_id_institusi_yg_menugasi::uuid);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dt
                , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@p_id_skema", p_id_skema)
                , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", p_kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", p_thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_id_institusi_yg_menugasi", p_id_institusi_yg_menugasi)
            //, new Npgsql.NpgsqlParameter("@p_jml_data", this._rowsPerPage)
            //, new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
            );
            //isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
            {
                this._errorMessage = this._db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool insupHasilPenilaian(Guid idPlottingReviewer, int idKomponenPenilaian, int skor, string catatan)
        {
            bool isSuccess = false;

            string strSQL = "SELECT hibah.insup_hasil_penilaian(@p_id_plotting_reviewer, " +
                                "@p_id_komponen_penilaian, @p_skor, @p_catatan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_plotting_reviewer", idPlottingReviewer)
                    , new Npgsql.NpgsqlParameter("@p_id_komponen_penilaian", idKomponenPenilaian)
                    , new Npgsql.NpgsqlParameter("@p_skor", skor)
                    , new Npgsql.NpgsqlParameter("@p_catatan", catatan)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupHasilmonev(Guid idPlottingReviewer, string komentar, string tempat, string kd_sts_kehadiran)
        {
            bool isSuccess = false;

            string strSQL = "SELECT hibah.insup_hasil_review_monev(@p_id_plotting_reviewer, @p_komentar::text, @p_tempat::varchar, @p_kd_sts_kehadiran::varchar);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_plotting_reviewer", idPlottingReviewer)
                    , new Npgsql.NpgsqlParameter("@p_komentar", komentar)
                    , new Npgsql.NpgsqlParameter("@p_tempat", tempat)
                    , new Npgsql.NpgsqlParameter("@p_kd_sts_kehadiran", kd_sts_kehadiran)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getHasilEvaluasi(ref DataTable dataTable, Guid IDPlottingReviewer)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_hasil_evaluasi('{0}');",
                                          IDPlottingReviewer);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getBeranda(ref DataTable dataTable, Guid idPersonal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from hibah.getberanda where now() between tgl_mulai and tgl_berakhir and id_personal= '{0}';", idPersonal);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getHasilReview(ref DataTable dataTable, Guid IDPlottingReviewer)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.hasil_review WHERE id_plotting_reviewer = '{0}';",
                                          IDPlottingReviewer);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        //public bool ListEvaluasiAdmrb(string id_personal, string p_id_skema, string p_kd_tahapan_kegiatan, string p_thn_usulan_kegiatan, string p_thn_pelaksanaan_kegiatan, string p_id_institusi_yg_menugasi)
        //{
        //    bool isSuccess = false;
        //    string strSQL = @"SELECT 1 FROM hibah.list_evaluasi_usulan_penelitian_rb(@p_id_personal::uuid,
        //                    @p_id_skema::integer,@p_kd_tahapan_kegiatan::character,
        //                    @p_thn_usulan_kegiatan::character,@p_thn_pelaksanaan_kegiatan::character,@p_id_institusi_yg_menugasi::uuid, @p_jml_data::integer, @offset::integer);";
        //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
        //        , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
        //        , new Npgsql.NpgsqlParameter("@p_id_skema", p_id_skema)
        //        , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", p_kd_tahapan_kegiatan)
        //        , new Npgsql.NpgsqlParameter("@p_thn_usulan_kegiatan", p_thn_usulan_kegiatan)
        //        , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)
        //        , new Npgsql.NpgsqlParameter("@p_id_institusi_yg_menugasi", p_id_institusi_yg_menugasi)
        //        , new Npgsql.NpgsqlParameter("@p_jml_data", this._rowsPerPage)
        //        , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
        //    );
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        #endregion

        #region Private Function

        #endregion
    }
}