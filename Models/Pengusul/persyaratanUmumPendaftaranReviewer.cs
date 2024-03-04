using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class persyaratanUmumPendaftaranReviewer : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public persyaratanUmumPendaftaranReviewer()
        {
            setInitValues();
        }

        ~persyaratanUmumPendaftaranReviewer()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getCekDataDosen(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_cek_data_dosen_pendaftaran_reviewer('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekJmlKetuaPpm(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_cek_jml_ketua_ppm('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekJmlKetuaAnggotaPpm(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_cek_jml_ketua_anggota_ppm('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool getJmlPenulisPertamaArtikelJurnal(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_penulis_pertama FROM penelitian.list_publikasi('{0}') WHERE kd_jenis_publikasi_jurnal = '1' AND peran_penulis = 'first author' OR peran_penulis = 'corresponding author';", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlPenulisAnggotaArtikelJurnal(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_penulis_anggota FROM penelitian.list_publikasi('{0}') WHERE kd_jenis_publikasi_jurnal = '1' AND peran_penulis = 'co-author';", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlBuku(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_buku FROM penelitian.list_buku('{0}');", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlHki(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            //Difilter selain jenis HKI Hak Cipta
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_hki FROM penelitian.list_hak_kekayaan_intelektual('{0}');", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlPenyajiTerbaik(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_penyaji_terbaik FROM hibah.list_pemenang_seminar_hasil_by_personal('{0}') WHERE kd_jenis_pemenang = '1';", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlPosterTerbaik(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_poster_terbaik FROM hibah.list_pemenang_seminar_hasil_by_personal('{0}') WHERE kd_jenis_pemenang = '2';", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlSeminar(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_seminar FROM pendaftaran_reviewer.list_pemakalah('{0}');", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDokumen(ref DataTable dataTable, string id_personal, string thn_pendaftaran, Guid id_jenis_dok_pendaftaran_reviewer)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_data_dok_pengusul('{0}','{1}','{2}');", id_personal, thn_pendaftaran, id_jenis_dok_pendaftaran_reviewer);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDokumenBaru(ref DataTable dataTable, string id_personal, string thn_pendaftaran)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_data_dok_pengusul_baru('{0}','{1}');", id_personal, thn_pendaftaran);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDokumenUnggah(ref DataTable dataTable, string id_pendaftaran, string id_jenis_dokumen)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_dokumen_unggah('{0}','{1}');", id_pendaftaran, id_jenis_dokumen);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDataSetifikatPelatihanReviewer(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_data_sertifikat_pelatihan_reviewer('{0}'::uuid);", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        
        public bool getDataSetifikatPelatihanReviewerInternal(ref DataTable dataTable, string id_personal, string p_kd_kategori_reviewer)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_data_sertifikat_pelatihan_reviewer_internal('{0}'::uuid);", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool insupDokPendaftaranReviewer
        (
            Guid id_dokumen_reviewer,
            Guid id_pendaftaran,
            Guid id_jenis_dok_pendaftaran_reviewer,
            string kd_sts_dokumen
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from pendaftaran_reviewer.insup_dok_pendaftaran_reviewer (      
                            @p_id_dokumen_reviewer::uuid,
                            @p_id_pendaftaran::uuid,
                            @p_id_jenis_dok_pendaftaran_reviewer::uuid,
                            @p_kd_sts_dokumen::character(1)
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_dokumen_reviewer", id_dokumen_reviewer)
                    , new Npgsql.NpgsqlParameter("@p_id_pendaftaran", id_pendaftaran)
                    , new Npgsql.NpgsqlParameter("@p_id_jenis_dok_pendaftaran_reviewer", id_jenis_dok_pendaftaran_reviewer)
                    , new Npgsql.NpgsqlParameter("@p_kd_sts_dokumen", kd_sts_dokumen)
                    ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool getCekJmlKetuaPenelitian(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_cek_jml_ketua_penelitian('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekJmlKetuaPenelitianKompetitifNasional(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_cek_jml_ketua_penelitian_kompetitif_nasional('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPemakalah(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.list_pemakalah('{0}');", idPersonal); ;
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NpgsqlParameter("@id_personal", idPersonal)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlHkiPenelitianGranted(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            //Difilter jenis HKI = Paten, dengan status granted
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_paten FROM penelitian.list_hak_kekayaan_intelektual('{0}') WHERE kd_jenis_hki = '1' AND kd_sts_hki = '2';", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getJmlHkiPenelitianTerdaftar(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            //Difilter jenis HKI = Paten, dengan status granted
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_paten FROM penelitian.list_hak_kekayaan_intelektual('{0}') WHERE kd_jenis_hki = '1' AND kd_sts_hki = '1';", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlKaryaSeni(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            // Function nunggu asep
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_karya_seni FROM pendaftaran_reviewer.list_karya_seni('{0}');", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlPenulisPertamaProsiding(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_penulis_pertama_prosiding FROM penelitian.list_prosiding_new('{0}') WHERE kd_peran_penulis = '1' OR kd_peran_penulis = '3';", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlPenulisAnggotaProsiding(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_penulis_anggota_prosiding FROM penelitian.list_prosiding_new('{0}') WHERE kd_peran_penulis = '2';", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool Updatepemakalah(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT COALESCE(count(*),0)::bigint AS jml_penulis_anggota_prosiding FROM penelitian.list_prosiding_new('{0}') WHERE kd_peran_penulis = '2';", idPersonal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updatesbgPemakalah(string kdUpdate, string id_prosiding)
        {
            var query = string.Format("update penelitian.prosiding set is_penyaji={0} where id_prosiding='{1}';", kdUpdate, id_prosiding);

            if (!this._db.ExecuteNonQuery(query
                        ))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool updateberkassbgPemakalah(string kdUpdate, string id_prosiding)
        {
            var query = string.Format("update penelitian.prosiding set kd_sts_unggah_berkas_penyaji='{0}' where id_prosiding='{1}';", kdUpdate, id_prosiding);

            if (!this._db.ExecuteNonQuery(query
                        ))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool updateberkassbgPemakalahtanpaprosiding(string id_pemakalah)
        {
            var query = string.Format("update pendaftaran_reviewer.pemakalah set kd_sts_unggah_berkas_penyaji='1' where id_pemakalah='{0}';", id_pemakalah);

            if (!this._db.ExecuteNonQuery(query
                        ))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool deleteDataPemakalah(string id_pemakalah)
        {
            bool isSuccess = false;

            string strSQL = string.Format("delete from pendaftaran_reviewer.pemakalah where id_pemakalah='{0}'::uuid;", id_pemakalah);

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool insupDatapemakalah(string p_thn_pemakalah, Guid p_id_personal, string p_judul, string p_nama_forum, Guid p_id_pemakalah,Boolean is_penyaji)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pendaftaran_reviewer.insup_pemakalah(
                    @p_thn_pemakalah::character(4),
                    @p_id_personal::uuid,
                    @p_judul::text,
                    @p_nama_forum::text,
                    @p_id_pemakalah::uuid,
                    @p_is_penyaji::boolean);";
            if (this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@p_thn_pemakalah", p_thn_pemakalah)
                , new Npgsql.NpgsqlParameter("@p_id_personal", p_id_personal)
                , new Npgsql.NpgsqlParameter("@p_judul", p_judul)
                , new Npgsql.NpgsqlParameter("@p_nama_forum", p_nama_forum)
                , new Npgsql.NpgsqlParameter("@p_id_pemakalah", p_id_pemakalah)
                , new Npgsql.NpgsqlParameter("@p_is_penyaji", is_penyaji)
                ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;


        }

        public bool getKaryaSeni(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.list_karya_seni('{0}');", idPersonal); ;
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NapgsqlParameter("@id_personal", idPersonal)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool deleteDataKaryaSeni(string id_karya_seni)
        {
            bool isSuccess = false;

            string strSQL = string.Format("delete from pendaftaran_reviewer.karya_seni where id_karya_seni='{0}'::uuid;", id_karya_seni);

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupDataKaryaSeni(string kd_jn_karya_seni, string id_personal, string judul_pementasan, string kd_tingkat_pementasan, string tempat_pelaksanaan, string tgl_mulai, string tgl_akhir, string kd_sts_berkas,string id_karya_seni)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pendaftaran_reviewer.insup_karya_seni(
                        @p_kd_jn_karya_seni::smallint,
                        @p_id_personal::uuid,
                        @p_judul_pementasan::text,
                        @p_kd_tingkat_pementasan::character,
                        @p_tempat_pelaksanaan::text,
                        @p_tgl_mulai::date,
                        @p_tgl_akhir::date,
                        @p_kd_sts_berkas::character,
                        @p_id_karya_seni::uuid
);";
            if (this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@p_kd_jn_karya_seni", kd_jn_karya_seni)
                , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@p_judul_pementasan", judul_pementasan)
                , new Npgsql.NpgsqlParameter("@p_kd_tingkat_pementasan", kd_tingkat_pementasan)
                , new Npgsql.NpgsqlParameter("@p_tempat_pelaksanaan", tempat_pelaksanaan)
                , new Npgsql.NpgsqlParameter("@p_tgl_mulai", tgl_mulai)
                , new Npgsql.NpgsqlParameter("@p_tgl_akhir", tgl_akhir)
                , new Npgsql.NpgsqlParameter("@p_kd_sts_berkas", kd_sts_berkas)
                , new Npgsql.NpgsqlParameter("@p_id_karya_seni", id_karya_seni)

                ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;


        }

        public bool getTingkatKegiatan(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.tingkat_kegiatan;"); ;
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NapgsqlParameter("@id_personal", idPersonal)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenisKaryaSeni(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.jenis_karya_seni;"); ;
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NapgsqlParameter("@id_personal", idPersonal)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getStatusReviewerNasionalPenelitianTersertifikat(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_status_reviewer_nasional_tersertifikat('{0}'::uuid);", id_personal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getStatusReviewerNasionalPpmTersertifikat(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_status_reviewer_ppm_tersertifikat('{0}'::uuid);", id_personal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        
        public bool getStatusReviewer(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_status_reviewer ('{0}');", id_personal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool getStatusReviewerInternal(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_status_reviewer_internal ('{0}'::uuid);", id_personal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        

        public bool insertPendaftaranReviewerPPM
        (
            Guid p_id_personal,
            string p_thn_pendaftaran,
            string p_kd_reviewer_mono,
            string p_kd_reviewer_multi,
            string p_is_baru
        )
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pendaftaran_reviewer.insup_pendaftaran_reviewer_ppm (
            @p_id_personal,
            @p_thn_pendaftaran,
            @p_kd_reviewer_mono,
            @p_kd_reviewer_multi,
            @p_is_baru);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_personal", p_id_personal)
                    , new Npgsql.NpgsqlParameter("@p_thn_pendaftaran", p_thn_pendaftaran)
                    , new Npgsql.NpgsqlParameter("@p_kd_reviewer_mono", p_kd_reviewer_mono)
                    , new Npgsql.NpgsqlParameter("@p_kd_reviewer_multi", p_kd_reviewer_multi)
                    , new Npgsql.NpgsqlParameter("@p_is_baru", p_is_baru)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertPendaftaranReviewerPenelitian
        (
            Guid p_id_personal,
            string p_thn_pendaftaran,
            string p_kd_kategori_reviewer,
            string p_is_baru,
            string p_arr_id_bidang_kepakaran
        )
        {
            bool isSuccess = false;
            string strSQL = String.Format( @"SELECT * FROM pendaftaran_reviewer.insup_pendaftaran_reviewer_penelitian(
            '{0}'::uuid,
            '{1}'::char(4),
            '{2}'::char(1),
            '{3}'::char(1),
            ARRAY[{4}]
            );", p_id_personal, p_thn_pendaftaran, p_kd_kategori_reviewer, p_is_baru, p_arr_id_bidang_kepakaran);

            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDataPendaftaranReviewerPPM(ref DataTable dataTable, string idPersonal, string thnPendaftaran)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_data_pendaftaran_ppm ('{0}','{1}');", idPersonal, thnPendaftaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateStsSertifikat
        (
            Guid id_personal,
            string no_sertifikat,
            string kd_sts_unggah_sertifikat
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from pendaftaran_reviewer.insup_sts_sertifikat (      
                            @p_id_personal::uuid,
                            @p_no_sertifikat::character varying,
                            @p_kd_sts_unggah_sertifikat::character(1)
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                    , new Npgsql.NpgsqlParameter("@p_no_sertifikat", no_sertifikat)
                    , new Npgsql.NpgsqlParameter("@p_kd_sts_unggah_sertifikat", kd_sts_unggah_sertifikat)
                    ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool updatePernyataanPaktaIntegritas
        (
            Guid id_pendaftaran,
            string kd_sts_pakta_integritas
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from pendaftaran_reviewer.update_pernyataan_pakta_integritas (      
                            @p_id_pendaftaran::uuid,
                            @p_kd_sts_pernyataan_pakta_integritas::character(1)
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_pendaftaran", id_pendaftaran)
                    , new Npgsql.NpgsqlParameter("@p_kd_sts_pernyataan_pakta_integritas", kd_sts_pakta_integritas)
                    ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }


        public bool updatePernyataanKodeEtik
        (
            Guid id_pendaftaran,
            string p_kd_sts_pernyataan_kode_etik
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from pendaftaran_reviewer.update_pernyataan_kode_etik (      
                            @p_id_pendaftaran::uuid,
                            @p_kd_sts_pernyataan_kode_etik::character(1)
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_pendaftaran", id_pendaftaran)
                    , new Npgsql.NpgsqlParameter("@p_kd_sts_pernyataan_kode_etik", p_kd_sts_pernyataan_kode_etik)
                    ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }


        public bool hapusPendaftaran
        (
            Guid id_pendaftaran
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from pendaftaran_reviewer.del_pendaftaran_reviewer (      
                            @p_id_pendaftaran::uuid
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_pendaftaran", id_pendaftaran)
                    ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }


        public bool kirimPendaftaran
        (
            Guid id_pendaftaran
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from pendaftaran_reviewer.insup_kirim_pendaftaran (      
                            @p_id_pendaftaran::uuid
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_pendaftaran", id_pendaftaran)
                    ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }


        public bool listJadwalKegiatan(ref DataTable dataTable, string p_thn_seleksi, 
            int p_id_jenis_jadwal=1, int p_periode=1)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM " +
                "   pendaftaran_reviewer.list_jadwal_pendaftaran_reviewer('{0}',{1},{2});",
                p_thn_seleksi, p_id_jenis_jadwal, p_periode); 
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NapgsqlParameter("@id_personal", idPersonal)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listJadwalKegiatan(ref DataTable dataTable, Guid id_personal, string p_thn_seleksi,
            int p_id_jenis_jadwal = 1, int p_periode = 1)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM " +
                "   pendaftaran_reviewer.list_jadwal_pendaftaran_reviewer('{0}'::uuid,'{1}'::char(4),{2}::int,{3}::int);",
                id_personal, p_thn_seleksi, p_id_jenis_jadwal, p_periode);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NapgsqlParameter("@id_personal", idPersonal)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion

    }
}