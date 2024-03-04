using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class daftarPenugasanReviewerPT : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public daftarPenugasanReviewerPT()
        {
            setInitValues();
        }

        ~daftarPenugasanReviewerPT()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getJmlBebanReviewerReviewer(Guid IdInstitusi, string ThnUsulanKegiatan, string ThnPelaksanaanKegiatan, string KdTahapanKegiatan, string NamaReviewer, int limit = 0, int offset = 0)
        {
            bool isSuccess = false;
            string strSQL;

            if (NamaReviewer == "")
            {
                strSQL = @"SELECT count(*)::int AS jml_beban_reviewer FROM (
                    SELECT 1 FROM hibah.list_beban_penugasan_reviewer(@id_institusi, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan, @kd_tahapan_kegiatan, @limit, @offset)) AS t1;";

                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                    , new Npgsql.NpgsqlParameter("@id_institusi", IdInstitusi)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@offset", offset)
                    , new Npgsql.NpgsqlParameter("@limit", limit));
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_beban_reviewer FROM (
                    SELECT 1 FROM hibah.list_beban_penugasan_reviewer(@id_institusi, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan, @kd_tahapan_kegiatan, @limit, @offset) WHERE nama_reviewer ~* '{0}') AS t1;", NamaReviewer);

                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                    , new Npgsql.NpgsqlParameter("@id_institusi", IdInstitusi)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@offset", offset)
                    , new Npgsql.NpgsqlParameter("@limit", limit));
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }

        }

        public bool getDaftarBebanReviewer(Guid IdInstitusi, string ThnUsulanKegiatan, string ThnPelaksanaanKegiatan, string KdTahapanKegiatan, string NamaReviewer)
        {
            bool isSuccess = false;
            string strSQL;

            if (NamaReviewer == "")
            {
                strSQL = @"SELECT * FROM hibah.list_beban_penugasan_reviewer(@id_institusi, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan, @kd_tahapan_kegiatan, @limit, @offset);";

                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                    , new Npgsql.NpgsqlParameter("@id_institusi", IdInstitusi)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                    , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_beban_penugasan_reviewer(@id_institusi, @thn_usulan_kegiatan, @thn_pelaksanaan_kegiatan, @kd_tahapan_kegiatan, @limit, @offset) WHERE nama_reviewer ~* '{0}';", NamaReviewer);

                int offset = 0;
                int limit = 0;
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                    , new Npgsql.NpgsqlParameter("@id_institusi", IdInstitusi)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@offset", offset)
                    , new Npgsql.NpgsqlParameter("@limit", limit));
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
        }

        public bool daftarRekapPenugasanSkema(ref DataTable dataTable, string idInstitusi,
            string thnUsulanKegiatan, string thnPelaksanaanKegiatan, string kdJenisKegiatan,
            string kdTahapanKegiatan, bool isForm = true)
        {
            bool isSuccess = false;
            string strSQL = "";
            if (isForm)
            {
                strSQL = string.Format(@"SELECT * FROM penelitian.list_jml_skema_penugasan_reviewer('{0}','{1}','{2}','{3}','{4}');"
                                , idInstitusi, thnUsulanKegiatan, thnPelaksanaanKegiatan,
                                kdJenisKegiatan, kdTahapanKegiatan);
            }
            else
            {
                strSQL = string.Format(@"SELECT nama_skema, jml_proposal, jml_reviewer FROM penelitian.list_jml_skema_penugasan_reviewer('{0}','{1}','{2}','{3}','{4}');"
                            , idInstitusi, thnUsulanKegiatan, thnPelaksanaanKegiatan,
                            kdJenisKegiatan, kdTahapanKegiatan);
            }

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        //public bool daftarRekapPenugasanSkemaExl(ref DataTable dataTable, string idInstitusi,
        //    string thnUsulanKegiatan, string thnPelaksanaanKegiatan, string kdJenisKegiatan,
        //    string kdTahapanKegiatan)
        //{
        //    bool isSuccess = false;
        //    string strSQL = string.Format(@"SELECT nama_skema, jml_proposal, jml_reviewer FROM penelitian.list_jml_skema_penugasan_reviewer('{0}','{1}','{2}','{3}','{4}');"
        //                    , idInstitusi, thnUsulanKegiatan, thnPelaksanaanKegiatan,
        //                    kdJenisKegiatan, kdTahapanKegiatan);
        //    isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

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

        public bool getInstitusiReviewer(ref DataTable dataTable, int idSkema, string thnUsulanKegiatan,
            string kdTahapanKegiatan, Guid idInstitusi, string thnPelaksanaanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_reviewer_blm_ditugaskan_sts_sertifikasi_group_by_institusi({0},'{1}','{2}','{3}','{4}');",
                            idSkema, thnUsulanKegiatan, kdTahapanKegiatan, idInstitusi, thnPelaksanaanKegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool daftar_program_hibah_thn_pelaksanaan(ref DataTable dataTable, string idInstitusi, string KdTahapanKegiatan, string thnPelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM public.list_konfig_eligible_program_hibah_by_klaster_institusi('{0}', '{1}', '{2}');"
                            , idInstitusi, KdTahapanKegiatan, thnPelaksanaan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool daftar_skema_thn_pelaksanaan(ref DataTable dataTable, string idInstitusi, string KdProgramHibah, string KdTahapanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM public.list_konfig_eligible_skema_tahapan_by_klaster_institusi('{0}', '{1}', '{2}');"
                            , idInstitusi, KdProgramHibah, KdTahapanKegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool daftar_tahapan_skema_by_thn_pelaksanaan(ref DataTable dataTable, string idInstitusi, string KdProgramHibah, string KdTahapanKegiatan, string thnPelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM public.list_konfig_eligible_skema_tahapan_by_klaster_institusi('{0}', '{1}', '{2}', '{3}');"
                            , idInstitusi, KdProgramHibah, KdTahapanKegiatan, thnPelaksanaan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getJmlDaftarPenugasan(int id_skema, string thn_usulan_kegiatan, string kd_tahapan_kegiatan, Guid id_institusi_yg_menugasi, string thn_pelaksanaan_kegiatan, string nama = "", int limit = 9999, int offset = 0)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_penugasan FROM (
                            SELECT 1 FROM hibah.list_penugasan_reviewer(@id_skema, @thn_usulan_kegiatan, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, @nama, @limit, @offset)) AS t1;";

            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", id_institusi_yg_menugasi)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@nama", nama)
                , new Npgsql.NpgsqlParameter("@offset", offset)
                , new Npgsql.NpgsqlParameter("@limit", limit));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarPenugasan(int id_skema, string thn_usulan_kegiatan, string kd_tahapan_kegiatan, Guid id_institusi_yg_menugasi, string thn_pelaksanaan_kegiatan, string nama="")
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.list_penugasan_reviewer(@id_skema, @thn_usulan_kegiatan, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, @nama, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", id_institusi_yg_menugasi)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@nama", nama)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsPenugasan1(int id_skema, string thn_usulan_kegiatan, string kd_tahapan_kegiatan, Guid id_institusi_yg_menugasi, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.list_penugasan_reviewer_sts_sertifikasi(@id_skema, @thn_usulan_kegiatan, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, @limit, @offset);";

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

        public bool getJmlReviewerBlmDitugaskan(int IdSkema, string ThnUsulanKegiatan, string KdTahapanKegiatan,
            Guid IdInstitusiYgMenugasi, string ThnPelaksanaanKegiatan, string KdKategoriReviewer,
            string NamaReviewer, int limit = 999999, int offset = 0)
        {
            bool isSuccess = false;
            string strSQL;

            if (NamaReviewer == "")
            {
                strSQL = @"SELECT count(*)::int AS jml_rev_blm_ditugaskan FROM (
                    SELECT 1 FROM hibah.list_reviewer_blm_ditugaskan_sts_sertifikasi(@id_skema, @thn_usulan_kegiatan, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, @kd_kategori_reviewer, @limit, @offset)) AS t1;";

                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                    , new Npgsql.NpgsqlParameter("@id_skema", IdSkema)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", IdInstitusiYgMenugasi)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_kategori_reviewer", KdKategoriReviewer)
                    , new Npgsql.NpgsqlParameter("@offset", offset)
                    , new Npgsql.NpgsqlParameter("@limit", limit));
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_rev_blm_ditugaskan FROM (
                    SELECT 1 FROM hibah.list_reviewer_blm_ditugaskan_sts_sertifikasi(@id_skema, @thn_usulan_kegiatan, 
                    @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, 
                    @kd_kategori_reviewer, @limit, @offset) WHERE nama ~* '{0}') AS t1;", NamaReviewer);

                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                    , new Npgsql.NpgsqlParameter("@id_skema", IdSkema)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", IdInstitusiYgMenugasi)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_kategori_reviewer", KdKategoriReviewer)
                    , new Npgsql.NpgsqlParameter("@offset", offset)
                    , new Npgsql.NpgsqlParameter("@limit", limit));
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
        }

        public bool getDaftarReviewerBlmDitugaskan(int IdSkema, string ThnUsulanKegiatan, string KdTahapanKegiatan, Guid IdInstitusiYgMenugasi, string ThnPelaksanaanKegiatan, string KdKategoriReviewer, string NamaReviewer)
        {
            bool isSuccess = false;
            string strSQL;

            if (NamaReviewer == "")
            {
                strSQL = @"SELECT * FROM hibah.list_reviewer_blm_ditugaskan_sts_sertifikasi(@id_skema, @thn_usulan_kegiatan, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, @kd_kategori_reviewer, @limit, @offset);";

                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                    , new Npgsql.NpgsqlParameter("@id_skema", IdSkema)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", IdInstitusiYgMenugasi)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_kategori_reviewer", KdKategoriReviewer)
                    , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                    , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_reviewer_blm_ditugaskan_sts_sertifikasi(@id_skema, @thn_usulan_kegiatan, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, @kd_kategori_reviewer, @limit, @offset) WHERE nama ~* '{0}';", NamaReviewer);

                int offset = 0;
                int limit = 999999;
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                    , new Npgsql.NpgsqlParameter("@id_skema", IdSkema)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", IdInstitusiYgMenugasi)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_kategori_reviewer", KdKategoriReviewer)
                    , new Npgsql.NpgsqlParameter("@offset", offset) 
                    , new Npgsql.NpgsqlParameter("@limit", limit));
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
        }

        public bool insertPenugasanReviewer(int IdSkema, string ThnUsulanKegiatan, Guid IdReviewer, string KdTahapanKegiatan, Guid IdInstitusiYgMenugasi, string ThnPelaksanaanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insert_penugasan_reviewer(@id_skema, @thn_usulan_kegiatan, @id_reviewer, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_skema", IdSkema)
                    , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", ThnUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@id_reviewer", IdReviewer)
                    , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", KdTahapanKegiatan)
                    , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", IdInstitusiYgMenugasi)
                    , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", ThnPelaksanaanKegiatan));
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

        public bool getKlasterPT(ref DataTable dataTable, string kd_program_hibah, Guid id_institusi)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM referensi.list_klaster_perguruan_tinggi('1') WHERE id_institusi = '{0}';", id_institusi);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool cekUnggahSkReviewer(Guid id_institusi)
        {
            bool ret = false;
            int jmlRow = 0;
            string strSQL = @"SELECT count(*)::int FROM penelitian.list_cek_unggah_dokumen_sk_reviewer(@id_institusi);";
            if (!this._db.ReadSkalar(strSQL, ref jmlRow
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)))
            {

                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            if (jmlRow > 0)
                ret = true;

            return ret;
        }

        public bool cekApakahKlasterMandiridDanUtama(Guid id_institusi)
        {
            bool ret = false;
            int jmlRow = 0;
            string strSQL = @"SELECT count(*)::int FROM referensi.list_cek_klaster_pt_mandiri_dan_utama(@id_institusi);";
            if (!this._db.ReadSkalar(strSQL, ref jmlRow
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)))
            {

                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            if (jmlRow > 0)
                ret = true;

            return ret;
        }

        public bool cekJadwal(string kd_tahapan_kegiatan, int id_skema)
        {
            bool ret = false;
            int jmlRow = 0;
            string strSQL = string.Format(@"SELECT count(*)::int FROM public.list_konfig_pengelolaan_kegiatan_aktif_xi() WHERE kd_tahapan_kegiatan = '{0}' AND id_skema = {1};", kd_tahapan_kegiatan, id_skema);
            if (!this._db.ReadSkalar(strSQL, ref jmlRow))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            if (jmlRow > 0)
                ret = true;

            return ret;
        }

        public bool getDaftarWhitelist(Guid id_institusi, int id_skema, string kd_tahapan_kegiatan, string thn_usulan, string thn_pelaksanaan)
        {
            bool ada = false;
            string strSQL;

            strSQL = string.Format(@"SELECT * FROM referensi.whitelist_pengelolaan_kegiatan
                                    WHERE id_institusi = '{0}' AND id_skema = {1} AND kd_tahapan_kegiatan = '{2}'
                                    AND thn_usulan = '{3}' AND thn_pelaksanaan = '{4}' AND kd_sts_aktif = '1';",
                                    id_institusi, id_skema, kd_tahapan_kegiatan, thn_usulan, thn_pelaksanaan);

            DataTable dt = new DataTable();
            ada = this._db.FetchDataTable(strSQL, ref dt);
            if (!ada)
                this._errorMessage = this._db.ErrorMessage;
            {
                if (dt.Rows.Count > 0)
                {
                    id_institusi = Guid.Parse(dt.Rows[0]["id_institusi"].ToString());
                    ada = true;
                }
                else
                {
                    ada = false;
                }
            }

            return ada;
        }

        public bool getDaftarReviewerBlmDitugaskanExcel(ref DataTable dataTable, int IdSkema, string ThnUsulanKegiatan, string KdTahapanKegiatan, Guid IdInstitusiYgMenugasi, string ThnPelaksanaanKegiatan, string KdKategoriReviewer)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT nama, nama_institusi, kompetensi, kategori_reviewer, nomor_telepon, nomor_hp, surel
                                FROM hibah.list_reviewer_blm_ditugaskan_sts_sertifikasi({0},'{1}','{2}','{3}','{4}','{5}',{6},{7});"
                            , IdSkema, ThnUsulanKegiatan, KdTahapanKegiatan, IdInstitusiYgMenugasi, ThnPelaksanaanKegiatan
                            , KdKategoriReviewer, 0, 0);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getExcelPenugasanReviewer(ref DataTable dataTable, int id_skema, string thn_usulan_kegiatan, string kd_tahapan_kegiatan, 
            Guid id_institusi_yg_menugasi, string thn_pelaksanaan_kegiatan, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT no_baris AS nomor, nama_reviewer, nama_institusi, kompetensi, nomor_hp, surel FROM hibah.list_penugasan_reviewer(@id_skema, @thn_usulan_kegiatan, @kd_tahapan_kegiatan, @id_institusi_yg_menugasi, @thn_pelaksanaan_kegiatan, '', @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_usulan_kegiatan", thn_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                , new Npgsql.NpgsqlParameter("@id_institusi_yg_menugasi", id_institusi_yg_menugasi)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@limit", limit)
                , new Npgsql.NpgsqlParameter("@offset", offset));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}