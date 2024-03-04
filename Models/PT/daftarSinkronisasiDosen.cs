using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class Token
    {
        public string access_token { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
    }

    public class daftarSinkronisasiDosen : _abstractModels
    {

        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public daftarSinkronisasiDosen()
        {
            setInitValues();
        }

        ~daftarSinkronisasiDosen()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool updateToken(Guid token)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT referensi.update_token_pddikti(@p_token);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_token", token));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlDaftarDosen(string kdPerguruanTinggi)
        {
            bool isSuccess = false;

            //int limit = 0;
            //int offset = 0;

            string strSQL = @"SELECT count(*)::int AS jml_dosen FROM (
                            SELECT 1 FROM pdpt.list_dosen_by_pt_rb_list(@kd_perguruan_tinggi)) AS t1;";

            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kdPerguruanTinggi)
                //, new Npgsql.NpgsqlParameter("@limit", limit)
                //, new Npgsql.NpgsqlParameter("@offset", offset)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarDosen(string kdPerguruanTinggi)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pdpt.list_dosen_by_pt_rb_list(@kd_perguruan_tinggi, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kdPerguruanTinggi)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlDaftarDosenCari(string kdPerguruanTinggi, string nama, int limit = 0, int offset = 0)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_dosen FROM (
                            SELECT 1 FROM pdpt.list_dosen_by_pt_rb(@kd_perguruan_tinggi, @nama, @limit, @offset)) AS t1;";

            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kdPerguruanTinggi)
                , new Npgsql.NpgsqlParameter("@nama", nama)
                , new Npgsql.NpgsqlParameter("@offset", offset)
                , new Npgsql.NpgsqlParameter("@limit", limit));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarDosenCari(string kdPerguruanTinggi, string nama)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pdpt.list_dosen_by_pt_rb(@kd_perguruan_tinggi, @nama, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@kd_perguruan_tinggi", kdPerguruanTinggi)
                , new Npgsql.NpgsqlParameter("@nama", nama)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getExcelDaftarDosen(ref DataTable dataTable, string kdPerguruanTinggi)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT no_baris AS no, kd_perguruan_tinggi, nama_institusi, 
                                            nama_program_studi, nidn, nama, jenjang_pendidikan, jenis_kelamin, jabatan_fungsional, 
                                            nomor_ktp, alamat, nomor_hp, surel, status_aktif
                                            FROM pdpt.list_dosen_by_pt_rb_list('{0}');", kdPerguruanTinggi);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarDosenFilterNidn(ref DataTable dataTable, string kdPerguruanTinggi, string nidn)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM pdpt.list_dosen_by_pt_rb_list('{0}') WHERE nidn = '{1}';", kdPerguruanTinggi, nidn);

            //dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getTokenPddikti(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM referensi.get_token_pddikti();");

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getTanggalIndo(ref DataTable dataTable, string tanggal)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT f_tanggal_indonesia AS tgl_indo FROM public.f_tanggal_indonesia('{0}');", tanggal);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertTmpDosen(string kdPerguruanTinggi, string kdProgramStudi, string nidn, string nama, string gelarAkademikDepan,
            string gelarAkademikBelakang, string kdJenjangPendidikanTertinggi, string kdJenisKelamin, string kdJabatanFungsional,
            string tempatLahir, string tanggalLahir, string nomorKtp, string noPegawai, string alamat, string kdPos, string nomorTelepon,
            string nomorHP, string surel, string kdStsAktif, string idPdpt)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pdpt.insup_tmp_dosen(
                                :kd_perguruan_tinggi::char(6), 
								:kd_program_studi::char(6), 
								:nidn::char(10), 
								:nama::varchar, 
								:gelar_akademik_depan::varchar, 
								:gelar_akademik_belakang::varchar, 
								:kd_jenjang_pendidikan_tertinggi::integer, 
								:kd_jenis_kelamin::char(1), 
								:kd_jabatan_fungsional::integer, 
								:tempat_lahir::varchar, 
								:tanggal_lahir::date,
								:nomor_ktp::varchar, 
								:no_pegawai::varchar, 
								:alamat::varchar, 
								:kd_pos::char(5), 
								:nomor_telepon::varchar, 
								:nomor_hp::varchar, 
								:surel::varchar, 
								:kd_sts_aktif::char(1), 
								:id_pdpt::uuid
                            );";

            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter(":kd_perguruan_tinggi", kdPerguruanTinggi)
                    , new Npgsql.NpgsqlParameter(":kd_program_studi", kdProgramStudi)
                    , new Npgsql.NpgsqlParameter(":nidn", nidn)
                    , new Npgsql.NpgsqlParameter(":nama", nama)
                    , new Npgsql.NpgsqlParameter(":gelar_akademik_depan", gelarAkademikDepan)
                    , new Npgsql.NpgsqlParameter(":gelar_akademik_belakang", gelarAkademikBelakang)
                    , new Npgsql.NpgsqlParameter(":kd_jenjang_pendidikan_tertinggi", kdJenjangPendidikanTertinggi)
                    , new Npgsql.NpgsqlParameter(":kd_jenis_kelamin", kdJenisKelamin)
                    , new Npgsql.NpgsqlParameter(":kd_jabatan_fungsional", kdJabatanFungsional)
                    , new Npgsql.NpgsqlParameter(":tempat_lahir", tempatLahir)
                    , new Npgsql.NpgsqlParameter(":tanggal_lahir", tanggalLahir)
                    , new Npgsql.NpgsqlParameter(":nomor_ktp", nomorKtp)
                    , new Npgsql.NpgsqlParameter(":no_pegawai", noPegawai)
                    , new Npgsql.NpgsqlParameter(":alamat", alamat)
                    , new Npgsql.NpgsqlParameter(":kd_pos", kdPos)
                    , new Npgsql.NpgsqlParameter(":nomor_telepon", nomorTelepon)
                    , new Npgsql.NpgsqlParameter(":nomor_hp", nomorHP)
                    , new Npgsql.NpgsqlParameter(":surel", surel)
                    , new Npgsql.NpgsqlParameter(":kd_sts_aktif", kdStsAktif)
                    , new Npgsql.NpgsqlParameter(":id_pdpt", idPdpt)
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

        public bool SinkronisasiWithIDPDPT(ref DataTable dt, string nidn, string id_pdpt)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT pdpt.sinkronisasi_dosen_with_id_pdpt('{0}','{1}');", nidn, id_pdpt);

            isSuccess = this._db.FetchDataTable(strSQL, ref dt);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getAkunDosen(ref DataTable dt, string nidn, Guid idInstitusi)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM pengguna.get_user_password_pengusul('{0}','{1}');", nidn, idInstitusi);

            isSuccess = this._db.FetchDataTable(strSQL, ref dt);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateStsDosen(Guid id_personal, string kd_sts_aktif)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM pdpt.update_sts_dosen('{id_personal}', '{kd_sts_aktif}');";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}