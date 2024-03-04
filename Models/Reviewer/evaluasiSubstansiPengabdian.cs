using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Reviewer
{
    public class evaluasiSubstansiPengabdian : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public evaluasiSubstansiPengabdian()
        {
            setInitValues();
        }

        ~evaluasiSubstansiPengabdian()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getListEvaluasiSubstansi(ref DataTable dataTable, Guid idPersonal, string tahunUsulan,
                        string tahunPelaksanaan, string kdTahapanKegiatan)
        {
            var query = @"SELECT * FROM hibah.list_evaluasi_skema_pengabdian_rb(@p_id_personal,
                                @p_thn_usulan_kegiatan, @p_thn_pelaksanaan_kegiatan, @p_kd_tahapan_kegiatan);";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_personal", idPersonal)
                        , new NpgsqlParameter("@p_thn_usulan_kegiatan", tahunUsulan)
                        , new NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", tahunPelaksanaan)
                        , new NpgsqlParameter("@p_kd_tahapan_kegiatan", kdTahapanKegiatan)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListEvaluasiUsulan(ref DataTable dataTable, Guid idPenugasanReviewer, int jmlBaris = 0, int offset = 0)
        {
            var query = @"SELECT * FROM hibah.list_evaluasi_usulan_pengabdian(@p_id_penugasan_reviewer,
                                @p_jml_baris, @p_offset);";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_penugasan_reviewer", idPenugasanReviewer)
                        , new NpgsqlParameter("@p_jml_baris", jmlBaris)
                        , new NpgsqlParameter("@p_offset", offset)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListEvaluasiUsulanBolehPermanen(ref DataTable dataTable, Guid idPenugasanReviewer, int jmlBaris = 0, int offset = 0)
        {
            var query = @"SELECT count(*) AS jml_boleh_permanen FROM hibah.list_evaluasi_usulan_pengabdian(@p_id_penugasan_reviewer,
                                @p_jml_baris, @p_offset) WHERE is_boleh_permanen = '1';";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_penugasan_reviewer", idPenugasanReviewer)
                        , new NpgsqlParameter("@p_jml_baris", jmlBaris)
                        , new NpgsqlParameter("@p_offset", offset)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListEvaluasiUsulanLanjutan(ref DataTable dataTable, Guid idPenugasanReviewer,
                        int jmlBaris = 0, int offset = 0)
        {
            var query = @"SELECT * FROM hibah.list_evaluasi_usulan_lanjutan(@p_id_penugasan_reviewer,
                                @p_jml_baris, @p_offset);";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_penugasan_reviewer", idPenugasanReviewer)
                        , new NpgsqlParameter("@p_jml_baris", jmlBaris)
                        , new NpgsqlParameter("@p_offset", offset)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListEvaluasiSubstansiLanjutan(ref DataTable dataTable, Guid idPersonal, string tahunUsulan,
                        string tahunPelaksanaan, string kdTahapanKegiatan)
        {
            var query = @"SELECT * FROM hibah.list_evaluasi_skema_pengabdian_lanjutan(@p_id_personal,
                                @p_thn_usulan_kegiatan, @p_thn_pelaksanaan_kegiatan, @p_kd_tahapan_kegiatan);";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_personal", idPersonal)
                        , new NpgsqlParameter("@p_thn_usulan_kegiatan", tahunUsulan)
                        , new NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", tahunPelaksanaan)
                        , new NpgsqlParameter("@p_kd_tahapan_kegiatan", kdTahapanKegiatan)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getEvaluasiUsulan(ref DataTable dataTable, Guid idPenugasanReviewer)
        {
            var query = @"SELECT * FROM hibah.get_evaluasi_usulan_pengabdian(@p_id_penugasan_reviewer);";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_penugasan_reviewer", idPenugasanReviewer)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListPenilaianRekamJejak(ref DataTable dataTable, Guid idPlottingReviewer)
        {
            var query = @"SELECT * FROM hibah.list_penilaian_rekam_jejak(@p_id_plotting_reviewer);";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_plotting_reviewer", idPlottingReviewer)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool insupPenilaianReviewer(Guid idPlottingReviewer, int idKomponenPenilaian,
            int idOpsiKomponenPenilaian, string urutanThnPelaksanaan)
        {
            var query = @"SELECT * FROM hibah.insup_hasil_penilaian_reviewer(@p_id_plotting_reviewer,
                            @p_id_komponen_penilaian, @p_id_opsi_komponen_penilaian, @p_urutan_thn_pelaksanaan);";

            if (!this._db.ExecuteNonQuery(query
                        , new NpgsqlParameter("@p_id_plotting_reviewer", idPlottingReviewer)
                        , new NpgsqlParameter("@p_id_komponen_penilaian", idKomponenPenilaian)
                        , new NpgsqlParameter("@p_id_opsi_komponen_penilaian", idOpsiKomponenPenilaian)
                        , new NpgsqlParameter("@p_urutan_thn_pelaksanaan", urutanThnPelaksanaan)
                        ))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListPenilaianUsulanPengabdian(ref DataTable dataTable, Guid idPlottingReviewer)
        {
            var query = @"SELECT * FROM hibah.list_penilaian_usulan_pengabdian(@p_id_plotting_reviewer);";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_plotting_reviewer", idPlottingReviewer)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListRekomendasiRAB(ref DataTable dataTable, Guid idPlottingReviewer,
                        int urutanThnUsulanKegiatan)
        {
            var query = @"SELECT * FROM hibah.list_rekomendasi_rab(@p_id_plotting_reviewer,
                                @p_urutan_thn_usulan_kegiatan);";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_plotting_reviewer", idPlottingReviewer)
                        , new NpgsqlParameter("@p_urutan_thn_usulan_kegiatan", urutanThnUsulanKegiatan)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListRABRekomendasi(ref DataTable dataTable, Guid idPlottingReviewer,
                        int urutanThnUsulanKegiatan)
        {
            var query = @"SELECT * FROM hibah.list_rekomendasi_rab_2019(@p_id_plotting_reviewer,
                                @p_urutan_thn_usulan_kegiatan);";

            if (!this._db.FetchDataTable(query, ref dataTable
                        , new NpgsqlParameter("@p_id_plotting_reviewer", idPlottingReviewer)
                        , new NpgsqlParameter("@p_urutan_thn_usulan_kegiatan", urutanThnUsulanKegiatan)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool insupRekomendasiRAB
        (
            Guid IdRabUsulan,
            Guid IdPlottingReviewer,
            decimal Volume,
            string Satuan,
            decimal HargaSatuan,
            string Komentar
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.insup_rekomendasi_rab (
                                  @p_id_rab_usulan::UUID,
                                  @p_id_plotting_reviewer::UUID,
                                  @p_volume::NUMERIC,
                                  @p_satuan::VARCHAR,
                                  @p_harga_satuan::MONEY,
                                  @p_komentar::TEXT);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_rab_usulan", IdRabUsulan)
                    , new Npgsql.NpgsqlParameter("@p_id_plotting_reviewer", IdPlottingReviewer)
                    , new Npgsql.NpgsqlParameter("@p_volume", Volume)
                    , new Npgsql.NpgsqlParameter("@p_satuan", Satuan)
                    , new Npgsql.NpgsqlParameter("@p_harga_satuan", HargaSatuan)
                    , new Npgsql.NpgsqlParameter("@p_komentar", Komentar)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupRABRekomendasi
        (
            Guid IdRabItemBelanja,
            Guid IdPlottingReviewer,
            decimal Volume,
            string Satuan,
            decimal HargaSatuan,
            string Komentar
        )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.insup_rab_rekomendasi(
                                  @p_id_rab_item_belanja::UUID,
                                  @p_id_plotting_reviewer::UUID,
                                  @p_volume::NUMERIC,
                                  @p_satuan::VARCHAR,
                                  @p_harga_satuan::MONEY,
                                  @p_komentar::TEXT);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_rab_item_belanja", IdRabItemBelanja)
                    , new Npgsql.NpgsqlParameter("@p_id_plotting_reviewer", IdPlottingReviewer)
                    , new Npgsql.NpgsqlParameter("@p_volume", Volume)
                    , new Npgsql.NpgsqlParameter("@p_satuan", Satuan)
                    , new Npgsql.NpgsqlParameter("@p_harga_satuan", HargaSatuan)
                    , new Npgsql.NpgsqlParameter("@p_komentar", Komentar)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupHasilReview
        (
            Guid IdPlottingReviewer,
            string Tempat,
            string Komentar
        )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.insup_hasil_review(
                                  @p_id_plotting_reviewer::UUID,
                                  NULL::MONEY,
                                  @p_komentar::TEXT,
                                  @p_tempat::VARCHAR,
                                  NULL::CHAR,
                                  NULL::TEXT);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_plotting_reviewer", IdPlottingReviewer)
                    , new Npgsql.NpgsqlParameter("@p_komentar", Komentar)
                    , new Npgsql.NpgsqlParameter("@p_tempat", Tempat)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool setSimpanPermanen(Guid IdIdPenugasanReviewer)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.set_simpan_permanen_hasil_review_substansi_pengabdian(
                                  @p_id_penugasan_reviewer::UUID);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_penugasan_reviewer", IdIdPenugasanReviewer)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool setSimpanPermanenUsulanLanjutan(Guid IdIdPenugasanReviewer)
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.set_simpan_permanen_hasil_review_usulan_lanjutan (
                                  @p_id_penugasan_reviewer::UUID);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_penugasan_reviewer", IdIdPenugasanReviewer)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPenilaianUsulanPdf(ref DataTable dataTable, Guid idPlottingReviewer)
        {
            var query = String.Format(@"SELECT * FROM hibah.get_sub_total_nilai_substansi_pengabdian('{0}')", idPlottingReviewer);
            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getTotalPenilaianUsulanPdf(ref DataTable dataTable, Guid idPlottingReviewer)
        {
            var query = String.Format(@"SELECT * FROM hibah.get_total_nilai_pengabdian('{0}')", idPlottingReviewer);
            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        #endregion

        #region Private Function

        #endregion
    }
}