using simlitekkes.Models;
using System;
using System.Data;

namespace simlitekkes.Models.PT
{
    public class ReviewerInternal : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public ReviewerInternal()
        {
            setInitValues();
        }

        ~ReviewerInternal()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        //public bool getJmlRecords(string kd_perguruan_tinggi, string kd_sts_aktif_reviewer_internal)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT count(*)::int AS jml_reviewer FROM hibah.reviewer t1 
        //                    INNER JOIN pdpt.personal t2 ON t1.id_personal = t2.id_personal
        //                    INNER JOIN pdpt.dosen t3 ON t2.id_personal = t3.id_personal
        //                    INNER JOIN hibah.reviewer_penelitian t4 ON t1.id_reviewer = t4.id_reviewer
        //                    WHERE t1.kd_sts_aktif = '1' AND t4.kd_sts_aktif_reviewer_internal = @kd_sts_aktif_reviewer_internal AND t3.kd_perguruan_tinggi = @kd_perguruan_tinggi";
        //    isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
        //        , new Npgsql.NpgsqlParameter("@kd_sts_aktif_reviewer_internal", kd_sts_aktif_reviewer_internal)
        //        , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kd_perguruan_tinggi));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool getJmlRecords(string kd_perguruan_tinggi, string kd_sts_aktif_reviewer_internal, string keyword)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT count(*)::int AS jml_reviewer FROM hibah.reviewer t1 
        //                    INNER JOIN pdpt.personal t2 ON t1.id_personal = t2.id_personal
        //                    INNER JOIN pdpt.dosen t3 ON t2.id_personal = t3.id_personal
        //                    INNER JOIN hibah.reviewer_penelitian t4 ON t1.id_reviewer = t4.id_reviewer
        //                    WHERE t1.kd_sts_aktif = '1' AND t4.kd_sts_aktif_reviewer_internal = @kd_sts_aktif_reviewer_internal 
        //                        AND t3.kd_perguruan_tinggi = @kd_perguruan_tinggi AND t2.nama ILIKE '%'||@keyword||'%';";
        //    isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
        //        , new Npgsql.NpgsqlParameter("@kd_sts_aktif_reviewer_internal", kd_sts_aktif_reviewer_internal)
        //        , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kd_perguruan_tinggi)
        //        , new Npgsql.NpgsqlParameter("@keyword", keyword)
        //        );
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool getCurrRecords(string kd_perguruan_tinggi, string kd_sts_aktif_reviewer_internal)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM hibah.list_reviewer(@kd_perguruan_tinggi, @kd_sts_aktif_reviewer_internal, @limit, @offset);";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
        //        , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kd_perguruan_tinggi)
        //        , new Npgsql.NpgsqlParameter("@kd_sts_aktif_reviewer_internal", kd_sts_aktif_reviewer_internal)
        //        , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
        //        , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool getCurrRecords(string kd_perguruan_tinggi, string kd_sts_aktif_reviewer_internal, string keyword)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM hibah.list_reviewer(@kd_perguruan_tinggi, @kd_sts_aktif_reviewer_internal, @keyword, @limit, @offset)";
        //    if (keyword.Trim().Length > 0) strSQL += $" WHERE nama ilike '%'|| '{keyword}' || '%'";
        //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
        //        , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kd_perguruan_tinggi)
        //        , new Npgsql.NpgsqlParameter("@kd_sts_aktif_reviewer_internal", kd_sts_aktif_reviewer_internal)
        //        , new Npgsql.NpgsqlParameter("@keyword", keyword)
        //        , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
        //        , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool getCurrRecords1(string kd_perguruan_tinggi, string kd_sts_aktif_reviewer_internal)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM hibah.list_reviewer_sts_sertifikasi(@kd_perguruan_tinggi, @kd_sts_aktif_reviewer_internal, @limit, @offset);";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
        //        , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kd_perguruan_tinggi)
        //        , new Npgsql.NpgsqlParameter("@kd_sts_aktif_reviewer_internal", kd_sts_aktif_reviewer_internal)
        //        , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
        //        , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool getCurrRecords1(string kd_perguruan_tinggi, string kd_sts_aktif_reviewer_internal, string keyword)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM hibah.list_reviewer_search_sts_sertifikasi(@kd_perguruan_tinggi, @kd_sts_aktif_reviewer_internal, @keyword, @limit, @offset)";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
        //        , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kd_perguruan_tinggi)
        //        , new Npgsql.NpgsqlParameter("@kd_sts_aktif_reviewer_internal", kd_sts_aktif_reviewer_internal)
        //        , new Npgsql.NpgsqlParameter("@keyword", keyword)
        //        , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
        //        , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public DataTable getRow()
        //{
        //    DataTable dt = new DataTable();

        //    return dt;
        //}

        public bool listReviewer(ref DataTable dataTable, string kd_perguruan_tinggi,
            string kd_sts_aktif_reviewer_internal, string keyword = default(string))
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.list_reviewer(@kd_perguruan_tinggi, @kd_sts_aktif_reviewer_internal, '0', '0')";
            if (keyword.Trim().Length > 0) strSQL += $" WHERE nama ilike '%'|| '{keyword}' || '%';";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kd_perguruan_tinggi)
                , new Npgsql.NpgsqlParameter("@kd_sts_aktif_reviewer_internal", kd_sts_aktif_reviewer_internal)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDosen(ref DataTable dataTable, string nidn, string kd_perguruan_tinggi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.get_reviewer(@nidn, @kd_perguruan_tinggi);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@nidn", nidn)
                , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kd_perguruan_tinggi)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool cekDosen(ref string stsEligibleReviewer, Guid id_personal)
        {
            bool isSuccess = false;
            string kd_jenis_kegiatan = "1";

            string strSQL = @"SELECT * FROM public.is_eligible_reviewer(@id_personal, @kd_jenis_kegiatan);";

            isSuccess = this._db.ReadSkalar(strSQL, ref stsEligibleReviewer
                , new Npgsql.NpgsqlParameter("@id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@kd_jenis_kegiatan", kd_jenis_kegiatan)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupReviewer(string nidn, string kompetensi, string kd_sts_aktif_reviewer_internal)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from hibah.insup_reviewer_internal(@p_nidn,@p_kompetensi,@p_kd_sts_aktif_reviewer_internal);",
                nidn, kompetensi, kd_sts_aktif_reviewer_internal);

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_nidn", nidn)
                    , new Npgsql.NpgsqlParameter("@p_kompetensi", kompetensi)
                    , new Npgsql.NpgsqlParameter("@p_kd_sts_aktif_reviewer_internal", kd_sts_aktif_reviewer_internal)
                    );

            return isSuccess;
        }

        public bool updateStatusRev(Guid id_reviewer, string kd_sts_aktif_reviewer_internal)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.update_status_reviewer_internal_penelitian(@p_id_reviewer::uuid, @p_kd_sts_aktif_reviewer_internal::character(1));";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@p_id_reviewer", id_reviewer)
                , new Npgsql.NpgsqlParameter("@p_kd_sts_aktif_reviewer_internal", kd_sts_aktif_reviewer_internal)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateStatusPeran(Guid id_personal, string kd_sts_aktif_reviewer_internal)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pengguna.update_peran_reviewer_internal(@id_personal, @kd_sts_peran_pengguna);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@kd_sts_peran_pengguna", kd_sts_aktif_reviewer_internal)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        //public bool getDokumenSkReviewer(ref DataTable dataTable, Guid id_institusi, string thn_sk)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM penelitian.list_dokumen_sk_reviewer(@id_institusi, @thn_sk);";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
        //        , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
        //        , new Npgsql.NpgsqlParameter("@thn_sk", thn_sk));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        public bool getDokumenSkReviewerOptPt(ref DataTable dataTable, Guid id_institusi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM penelitian.list_dokumen_sk_reviewer_opt_pt(@id_institusi);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion

        #region Private Function

        #endregion
    }
}
