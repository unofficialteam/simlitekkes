using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class berandaPengusul : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public berandaPengusul()
        {
            setInitValues();
        }

        ~berandaPengusul()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getInfoPemenangPendaftaranReviewer(ref DataTable dataTable, Guid idPersonal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_ucapan_pemenang_reviewer_nasional_penelitian('{0}');",
                idPersonal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPengumuman(ref DataTable dataTable, int p_jml_data, int p_offset)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM news.list_pengumuman_new({0},{1});",
                p_jml_data, p_offset);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecordsFrontpages()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM news.list_pengumuman_frontpages_limit(:limit, :offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getLampiranPengumuman(ref DataTable dataTable, string id_pengumuman)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM news.list_lampiran_pengumuman_new('{0}');", id_pengumuman);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPersonal(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pdpt.get_data_dosen('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getSinta(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM penelitian.get_sinta('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getSintabyId(ref DataTable dataTable, string idSinta)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM penelitian.get_sinta_by_id({0});", idSinta);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateIdentitasPersonal(Guid id_personal, string nomor_ktp, string alamat,
            string tempat_lahir, string tanggal_lahir, string nomor_telepon, string nomor_hp,
            string surel, string website_personal)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pdpt.update_identitas_personal(
                    @p_id_personal::uuid,
                    @p_nomor_ktp::character varying,
                    @p_alamat::character varying,
                    @p_tempat_lahir::character varying,
                    @p_tanggal_lahir::date,
                    @p_nomor_telepon::character varying,
                    @p_nomor_hp::character varying,
                    @p_surel::character varying,
                    @p_website_personal::character varying);";
            if (this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@p_nomor_ktp", nomor_ktp)
                , new Npgsql.NpgsqlParameter("@p_alamat", alamat)
                , new Npgsql.NpgsqlParameter("@p_tempat_lahir", tempat_lahir)
                , new Npgsql.NpgsqlParameter("@p_tanggal_lahir", tanggal_lahir)
                , new Npgsql.NpgsqlParameter("@p_nomor_telepon", nomor_telepon)
                , new Npgsql.NpgsqlParameter("@p_nomor_hp", nomor_hp)
                , new Npgsql.NpgsqlParameter("@p_surel", surel)
                , new Npgsql.NpgsqlParameter("@p_website_personal", website_personal)
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

        public bool updateSintaPersonal(Guid id_personal, string nomor_ktp, string alamat,
    string tempat_lahir, string tanggal_lahir, string nomor_telepon, string nomor_hp,
    string surel, string website_personal)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pdpt.update_identitas_personal(
                    @p_id_personal::uuid,
                    @p_nomor_ktp::character varying,
                    @p_alamat::character varying,
                    @p_tempat_lahir::character varying,
                    @p_tanggal_lahir::date,
                    @p_nomor_telepon::character varying,
                    @p_nomor_hp::character varying,
                    @p_surel::character varying,
                    @p_website_personal::character varying);";
            if (this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@p_nomor_ktp", nomor_ktp)
                , new Npgsql.NpgsqlParameter("@p_alamat", alamat)
                , new Npgsql.NpgsqlParameter("@p_tempat_lahir", tempat_lahir)
                , new Npgsql.NpgsqlParameter("@p_tanggal_lahir", tanggal_lahir)
                , new Npgsql.NpgsqlParameter("@p_nomor_telepon", nomor_telepon)
                , new Npgsql.NpgsqlParameter("@p_nomor_hp", nomor_hp)
                , new Npgsql.NpgsqlParameter("@p_surel", surel)
                , new Npgsql.NpgsqlParameter("@p_website_personal", website_personal)
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

        public bool getRiwayatPenelitian(ref DataTable dataTable, Guid idpersonal)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_riwayat_penelitian_dan_pengabdian(@idpersonal)
                              WHERE kd_jenis_kegiatan = '1';";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                                , new Npgsql.NpgsqlParameter("@idpersonal", idpersonal));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getRiwayatPengabdian(ref DataTable dataTable, Guid idpersonal)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_riwayat_penelitian_dan_pengabdian(@idpersonal)
                              WHERE kd_jenis_kegiatan = '2';";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@idpersonal", idpersonal));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getArtikelJurnal(ref DataTable dataTable, Guid idPersonal)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_publikasi(@idpersonal);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@idpersonal", idPersonal));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDataArtikelJurnal(ref DataTable dataTable, Guid idPublikasiJurnal)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.publikasi WHERE id_publikasi = @idPublikasiJurnal;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@idPublikasiJurnal", idPublikasiJurnal));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenisPublikasiJurnal(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM kinerja_penelitian.jenis_publikasi_jurnal ORDER BY kd_jenis_publikasi_jurnal;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPeranPenulisPublikasi(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.peran_penulis WHERE kd_sts_aktif ='1' ORDER BY kd_peran_penulis;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupArtikelJurnal
        (
            Guid idPersonal,
            string tahunPublikasi,
            string kdJenisPublikasiJurnal,
            string judul,
            string namaJurnal,
            string volume,
            string nomor,
            string URL,
            string ISSN,
            string kdPeranPenulis,
            Guid idPublikasi
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_publikasi_rb (                          
                              @p_id_personal,
                              @p_thn_publikasi,
                              @p_kd_jenis_publikasi_jurnal,
                              @p_judul,
                              @p_nama_jurnal,
                              @p_volume,
                              @p_nomor,
                              @p_url,
                              @p_issn,
                              @p_kd_peran_penulis,
                              @p_id_publikasi
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_personal", idPersonal)
                    , new Npgsql.NpgsqlParameter("@p_thn_publikasi", tahunPublikasi)
                    , new Npgsql.NpgsqlParameter("@p_kd_jenis_publikasi_jurnal", kdJenisPublikasiJurnal)
                    , new Npgsql.NpgsqlParameter("@p_judul", judul)
                    , new Npgsql.NpgsqlParameter("@p_nama_jurnal", namaJurnal)
                    , new Npgsql.NpgsqlParameter("@p_volume", volume)
                    , new Npgsql.NpgsqlParameter("@p_nomor", nomor)
                    , new Npgsql.NpgsqlParameter("@p_url", URL)
                    , new Npgsql.NpgsqlParameter("@p_issn", ISSN)
                    , new Npgsql.NpgsqlParameter("@p_kd_peran_penulis", kdPeranPenulis)
                    , new Npgsql.NpgsqlParameter("@p_id_publikasi", idPublikasi)))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool delArtikelJurnal(Guid idPublikasi)
        {
            bool isSuccess = false;
            string strSQL = "DELETE FROM penelitian.publikasi WHERE id_publikasi = @idPublikasiJurnal;";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                                , new Npgsql.NpgsqlParameter("@idPublikasiJurnal", idPublikasi));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateStatusUnggahArtikelJurnal(Guid idPublikasi)
        {
            bool isSuccess = false;
            var strSQL = $@"UPDATE penelitian.publikasi SET kd_sts_berkas_jurnal ='1'
                            WHERE id_publikasi ='{idPublikasi}'";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listHKI(ref DataTable dataTable, Guid idpersonal)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_hak_kekayaan_intelektual(@idpersonal, @limit, @offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
            , new Npgsql.NpgsqlParameter("@idpersonal", idpersonal)
            , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
            , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listHKIAll(ref DataTable dataTable, Guid idpersonal)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_hak_kekayaan_intelektual(@idpersonal::uuid, @limit::int, @offset::int);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idpersonal", idpersonal.ToString())
            , new Npgsql.NpgsqlParameter("@offset", "0")
            , new Npgsql.NpgsqlParameter("@limit", "0"));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getHKI(ref DataTable dataTable, Guid idHKI)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.get_hki(@idHKI);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idHKI", idHKI));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listJenisHKI(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT kd_jenis_hki, jenis_hki FROM kinerja_penelitian.jenis_hak_kekayaan_intelektual order by kd_jenis_hki");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listStatusHKI(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT kd_sts_hki, status_hki FROM kinerja_penelitian.status_hak_kekayaan_intelektual order by kd_sts_hki");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insupDataBaruHKI
        (
            Guid id_hak_kekayaan_intelektual,
            string thn_pelaksanaan,
            Guid id_personal,
            string judul_hki,
            string kd_jenis_hki,
            string no_pendaftaran,
            string kd_sts_hki,
            string no_hki,
            string url
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_hak_kekayaan_intelektual_idhki (                          
                          @p_thn_pelaksanaan::character(4),
                          @p_id_personal::uuid,
                          @p_judul_hki::text,
                          @p_kd_jenis_hki::character(1),
                          @p_no_pendaftaran::character varying,
                          @p_kd_sts_hki::character(1),
                          @p_no_hki::character varying,
                          @p_url::text,
                          @p_id_hak_kekayaan_intelektual::uuid
                        );";

            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan", thn_pelaksanaan)
                    , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal)
                    , new Npgsql.NpgsqlParameter("@p_judul_hki", judul_hki)
                    , new Npgsql.NpgsqlParameter("@p_kd_jenis_hki", kd_jenis_hki)
                    , new Npgsql.NpgsqlParameter("@p_no_pendaftaran", no_pendaftaran)
                    , new Npgsql.NpgsqlParameter("@p_kd_sts_hki", kd_sts_hki)
                    , new Npgsql.NpgsqlParameter("@p_no_hki", no_hki)
                    , new Npgsql.NpgsqlParameter("@p_url", url)
                    , new Npgsql.NpgsqlParameter("@p_id_hak_kekayaan_intelektual", id_hak_kekayaan_intelektual)

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

        public bool hapusHKI(Guid idHKI)
        {
            bool isSuccess = false;

            string strSQL = string.Format("Select * FROM penelitian.del_hki('{0}')", idHKI);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public bool updateStsDokHKI(Guid idHKI)
        {
            bool isSuccess = false;
            string strSQL = string.Format("UPDATE penelitian.hak_kekayaan_intelektual SET kd_sts_berkas_hki ='1' WHERE id_hak_kekayaan_intelektual ='{0}'", idHKI);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getArtikelProsiding(ref DataTable dataTable, Guid id_personal)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_prosiding_new(@id_personal);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@id_personal", id_personal));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPeranPenulisProsiding(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_peran_penulis_prosiding();";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenisProsiding(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_jenis_prosiding();";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteDataProsiding(Guid id_prosiding)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.del_prosiding(@id_prosiding);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_prosiding", id_prosiding));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupDataProsiding(
            string p_thn_prosiding, Guid p_id_personal, string p_judul, string p_nama_prosiding,
            string p_volume, string p_nomor, string p_url, string p_issn, string p_kd_peran_penulis,
            string p_kd_jenis_prosiding, Guid p_id_prosiding)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.insup_prosiding_new(
                    @p_thn_prosiding::character(4),
                    @p_id_personal::uuid,
                    @p_judul::text,
                    @p_nama_prosiding::text,
                    @p_volume::character varying,
                    @p_nomor::character varying,
                    @p_url::text,
                    @p_issn::character varying,
                    @p_kd_peran_penulis::character,
                    @p_kd_jenis_prosiding::character,
                    @p_id_prosiding::uuid);";
            if (this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@p_thn_prosiding", p_thn_prosiding)
                , new Npgsql.NpgsqlParameter("@p_id_personal", p_id_personal)
                , new Npgsql.NpgsqlParameter("@p_judul", p_judul)
                , new Npgsql.NpgsqlParameter("@p_nama_prosiding", p_nama_prosiding)
                , new Npgsql.NpgsqlParameter("@p_volume", p_volume)
                , new Npgsql.NpgsqlParameter("@p_nomor", p_nomor)
                , new Npgsql.NpgsqlParameter("@p_url", p_url)
                , new Npgsql.NpgsqlParameter("@p_issn", p_issn)
                , new Npgsql.NpgsqlParameter("@p_kd_peran_penulis", p_kd_peran_penulis)
                , new Npgsql.NpgsqlParameter("@p_kd_jenis_prosiding", p_kd_jenis_prosiding)
                , new Npgsql.NpgsqlParameter("@p_id_prosiding", p_id_prosiding)
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

        public bool getBuku(ref DataTable dataTable, Guid idPersonal)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_buku(@idpersonal);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@idpersonal", idPersonal));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDataBuku(ref DataTable dataTable, Guid idBuku)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.buku WHERE id_buku = @idBuku;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@idBuku", idBuku));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupBuku
        (
            Guid idPersonal,
            string tahunPenerbitan,
            string judul,
            string jumlahHalaman,
            string namaPenerbit,
            string ISBN,
            string URL,
            Guid idBuku
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_buku (                          
                              @p_thn_penerbitan,
                              @p_id_personal,
                              @p_judul,
                              @p_isbn,
                              @p_jml_halaman,
                              @p_penerbit,
                              @p_url,
                              @p_id_buku
                            );";

            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter("@p_thn_penerbitan", tahunPenerbitan)
                    , new Npgsql.NpgsqlParameter("@p_id_personal", idPersonal)
                    , new Npgsql.NpgsqlParameter("@p_judul", judul)
                    , new Npgsql.NpgsqlParameter("@p_isbn", ISBN)
                    , new Npgsql.NpgsqlParameter("@p_jml_halaman", jumlahHalaman)
                    , new Npgsql.NpgsqlParameter("@p_penerbit", namaPenerbit)
                    , new Npgsql.NpgsqlParameter("@p_url", URL)
                    , new Npgsql.NpgsqlParameter("@p_id_buku", idBuku)))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool delBuku(Guid idBuku)
        {
            bool isSuccess = false;
            string strSQL = "DELETE FROM penelitian.buku WHERE id_buku = @idBuku;";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                                , new Npgsql.NpgsqlParameter("@idBuku", idBuku));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateStatusUnggahBuku(Guid idBuku)
        {
            bool isSuccess = false;
            var strSQL = $@"UPDATE penelitian.buku SET kd_sts_berkas_buku ='1'
                            WHERE id_buku ='{idBuku}'";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateStatusUnggah(Guid id_prosiding)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"UPDATE penelitian.prosiding SET kd_sts_berkas_prosiding = '1' WHERE id_prosiding = '{0}';", id_prosiding);
            if (this._db.ExecuteNonQuery(strSQL))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool getdatadosen(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT nidn, kd_perguruan_tinggi, nama_perguruan_tinggi
                            , nama, gelar_akademik_depan, gelar_akademik_belakang, nama_lengkap
                            , golongan, pangkat, jabatan_fungsional, jabatan_struktural
                            , nomor_ktp, kode_jenis_kelamin, jenis_kelamin, tempat_lahir, tanggal_lahir, alamat
                            , nomor_telepon, nomor_hp, surel, id_program_studi, nama_program_studi
                            , kode_status_aktif, status_aktif, kode_jenjang_pendidikan_tertinggi
                            , jenjang_pendidikan_tertinggi, no_sertifikasi_dosen, no_pegawai, web_personal
                            , id_pdpt, kd_program_studi, bidang_keahlian
                            , id_kategori_perguruan_tinggi
                            FROM pdpt.get_full_data_dosen('{0}')", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;


        }

        public bool gettmpdosen(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT *
                            FROM pdpt.get_tmp_dosen('{0}')", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;


        }

        public bool listUsulanPerbaikan(ref DataTable dataTable, Guid idpersonal, string thnPelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_perbaikan_usulan_penelitian(@idpersonal, @thnPelaksanaan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
            , new Npgsql.NpgsqlParameter("@idpersonal", idpersonal)
            , new Npgsql.NpgsqlParameter("@thnPelaksanaan", thnPelaksanaan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getArtikelJurnalKinerja(ref DataTable dataTable, Guid idPersonal, Guid idPublikasiJurnal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM penelitian.list_publikasi('{0}')" +
                "WHERE id_publikasi_jurnal = '{1}'", idPersonal, idPublikasiJurnal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updatePeranPenulis(Guid idPublikasiJurnal, Guid idPersonal, string kdPeranPenulis)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM kinerja_penelitian.update_peran_penulis_publikasi_dosen('{0}','{1}','{2}');"
                            , idPublikasiJurnal, idPersonal, kdPeranPenulis);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateUrl(Guid idPublikasiJurnal, string url)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM kinerja_penelitian.update_url_publikasi_jurnal('{0}','{1}');"
                            , idPublikasiJurnal, url);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insupSintaScopus(Guid id_personal, int id_sinta, int rangking_nasional, int rangking_afiliasi,
            double skor_sinta, int jml_artikel_google_scholar, int jml_sitasi_google_scholar, int hindex_google_scholar,
            int i_10_hindex_google_scholar, string nama_personal_sinta, string id_scopus, int hindex, int jml_dokumen,
            int jml_sitasi, string thn_terakhir_publikasi, string id_google_scholar)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM penelitian.insup_sinta_scopus(@id_personal::UUID, @id_sinta::BIGINT, " +
                "@rangking_nasional, @rangking_afiliasi, @skor_sinta::NUMERIC, @jml_artikel_google_scholar, " +
                "@jml_sitasi_google_scholar, @hindex_google_scholar, @i_10_hindex_google_scholar, " +
                "@nama_personal_sinta, @id_scopus, @hindex, @jml_dokumen, @jml_sitasi, " +
                "@thn_terakhir_publikasi::DATE, @id_google_scholar);";
            isSuccess = this._db.ExecuteNonQuery(strSQL, 
                new Npgsql.NpgsqlParameter("@id_personal", id_personal),
                new Npgsql.NpgsqlParameter("@id_sinta", id_sinta),
                new Npgsql.NpgsqlParameter("@rangking_nasional", rangking_nasional),
                new Npgsql.NpgsqlParameter("@rangking_afiliasi", rangking_afiliasi),
                new Npgsql.NpgsqlParameter("@skor_sinta", skor_sinta),
                new Npgsql.NpgsqlParameter("@jml_artikel_google_scholar", jml_artikel_google_scholar),
                new Npgsql.NpgsqlParameter("@jml_sitasi_google_scholar", jml_sitasi_google_scholar),
                new Npgsql.NpgsqlParameter("@hindex_google_scholar", hindex_google_scholar),
                new Npgsql.NpgsqlParameter("@i_10_hindex_google_scholar", i_10_hindex_google_scholar),
                new Npgsql.NpgsqlParameter("@nama_personal_sinta", nama_personal_sinta),
                new Npgsql.NpgsqlParameter("@id_scopus", id_scopus),
                new Npgsql.NpgsqlParameter("@hindex", hindex),
                new Npgsql.NpgsqlParameter("@jml_dokumen", jml_dokumen),
                new Npgsql.NpgsqlParameter("@jml_sitasi", jml_sitasi),
                new Npgsql.NpgsqlParameter("@thn_terakhir_publikasi", thn_terakhir_publikasi),
                new Npgsql.NpgsqlParameter("@id_google_scholar", id_google_scholar)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupTokenSinta(string token)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM referensi.insup_token_sinta('{token}');";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getTokenSinta(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM referensi.get_token_sinta();";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}
