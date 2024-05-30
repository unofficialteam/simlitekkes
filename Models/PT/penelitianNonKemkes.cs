using System;
using System.Data;

namespace simlitekkes.Models.PT
{
    public class penelitianNonKemkes : _abstractModels
    {
        public penelitianNonKemkes()
        {
            setInitValues();
        }
        #region Methods

        public bool getListJenisPenelitian(ref DataTable dataTable)
        {
            var query = "SELECT * FROM kinerja_penelitian.jenis_penelitian ORDER BY kd_jenis_penelitian;";
            var isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        //public bool getListStatusKepegawaian(ref DataTable dataTable)
        //{
        //    bool isSuccess = false;
        //    string strSQL = string.Format(@"SELECT * FROM referensi.list_status_aktif_kepegawaian();");
        //    isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        public bool listData(ref DataTable dataTable, Guid idInstitusi, string tahunKegiatan, string pencarian)
        {
            var strSQL = $@"SELECT * FROM kinerja_penelitian.list_penelitian_non_ditlitabmas(
                            '{idInstitusi}','{tahunKegiatan}',{this.rowsPerPage},{this.currentPage * this._rowsPerPage}, '{pencarian}');";
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess) this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarData(Guid idInstitusi, string tahunKegiatan, string pencarian)
        {
            var query = "SELECT * FROM kinerja_penelitian.list_penelitian_non_ditlitabmas (" +
                            "@idInstitusi, @tahunKegiatan, @limit, @offset, @pencarian);";

            var isSuccess = this._db.FetchDataTable(query, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@idInstitusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@tahunKegiatan", tahunKegiatan)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@pencarian", pencarian)
                //, new Npgsql.NpgsqlParameter("@idPenelitian", pencarian)
                );

            if (!isSuccess) this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        //public bool getDetailData(Guid id_personal, ref DataTable dataTable)
        //{
        //    bool isSuccess = false;
        //    string strSQL = @"SELECT * FROM pdpt.get_data_tendik(@id_personal, @id_institusi, @limit, @offset);";
        //    int limit = 1, offset = 0;
        //    isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
        //        , new Npgsql.NpgsqlParameter("@id_personal", id_personal)
        //        , new Npgsql.NpgsqlParameter("@id_institusi", Guid.Parse("00000000-0000-0000-0000-000000000000"))
        //        , new Npgsql.NpgsqlParameter("@offset", offset)
        //        , new Npgsql.NpgsqlParameter("@limit", limit));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        //public bool getTendikByKTP(string no_ktp, ref DataTable dataTable)
        //{
        //    bool isSuccess = false;
        //    string strSQL = @"SELECT * FROM pdpt.get_tendik_by_ktp2(@no_ktp);";
        //    isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
        //        , new Npgsql.NpgsqlParameter("@no_ktp", no_ktp));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        //public bool isTendik(Guid id_personal)
        //{
        //    bool isSuccess = false;
        //    string strSQL = @"SELECT * FROM pdpt.get_cek_is_tendik(@id_personal);";
        //    DataTable result = new DataTable();
        //    isSuccess = this._db.FetchDataTable(strSQL, ref result
        //        , new Npgsql.NpgsqlParameter("@id_personal", id_personal));
        //    if (isSuccess)
        //    {
        //        if (!result.Rows[0]["status_tendik"].ToString().Trim().Equals("1"))
        //        {
        //            this._errorMessage = result.Rows[0]["keterangan"].ToString();
        //            isSuccess = false;
        //        }
        //    }
        //    else
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        //public bool insertData(Guid id_institusi, string nama, string nomor_ktp, string gelar_depan, string gelar_belakang, string kd_jenis_kelamin, string tempat_lahir, string tgl_lahir, string alamat, string kd_kota, string kd_pos, string nomor_telepon, string nomor_hp, string surel, string bidang_keahlian, string website_personal)
        //{
        //    DateTime tanggal_lahir = DateTime.Parse(tgl_lahir);
        //    bool isSuccess = false;
        //    string strSQL = @"SELECT * FROM pengguna.insup_user_tendik(@id_personal, @id_institusi, @nama, @nomor_ktp, @gelar_depan, @gelar_belakang, @kd_jenis_kelamin, @tempat_lahir, @tgl_lahir::date, @alamat, @kd_kota, @kd_pos, @nomor_telepon, @nomor_hp, @surel, @bidang_keahlian, @website_personal);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //        , new Npgsql.NpgsqlParameter("@id_personal", Guid.Parse("00000000-0000-0000-0000-000000000000"))
        //        , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
        //        , new Npgsql.NpgsqlParameter("@nama", nama)
        //        , new Npgsql.NpgsqlParameter("@nomor_ktp", nomor_ktp)
        //        , new Npgsql.NpgsqlParameter("@gelar_depan", gelar_depan)
        //        , new Npgsql.NpgsqlParameter("@gelar_belakang", gelar_belakang)
        //        , new Npgsql.NpgsqlParameter("@kd_jenis_kelamin", kd_jenis_kelamin)
        //        , new Npgsql.NpgsqlParameter("@tempat_lahir", tempat_lahir)
        //        , new Npgsql.NpgsqlParameter("@tgl_lahir", tanggal_lahir.ToString("yyyy-MM-dd"))
        //        , new Npgsql.NpgsqlParameter("@alamat", alamat)
        //        , new Npgsql.NpgsqlParameter("@kd_kota", kd_kota)
        //        , new Npgsql.NpgsqlParameter("@kd_pos", kd_pos)
        //        , new Npgsql.NpgsqlParameter("@nomor_telepon", nomor_telepon)
        //        , new Npgsql.NpgsqlParameter("@nomor_hp", nomor_hp)
        //        , new Npgsql.NpgsqlParameter("@surel", surel)
        //        , new Npgsql.NpgsqlParameter("@bidang_keahlian", bidang_keahlian)
        //        , new Npgsql.NpgsqlParameter("@website_personal", website_personal));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        //public bool updateData(Guid id_personal, Guid id_institusi, string nama, string nomor_ktp, string gelar_depan, string gelar_belakang, string kd_jenis_kelamin, string tempat_lahir, string tgl_lahir, string alamat, string kd_kota, string kd_pos, string nomor_telepon, string nomor_hp, string surel, string bidang_keahlian, string website_personal)
        //{
        //    DateTime tanggal_lahir = DateTime.Parse(tgl_lahir);
        //    bool isSuccess = false;
        //    string strSQL = @"SELECT * FROM pengguna.insup_user_tendik(@id_personal, @id_institusi, @nama, @nomor_ktp, @gelar_depan, @gelar_belakang, @kd_jenis_kelamin, @tempat_lahir, @tgl_lahir::date, @alamat, @kd_kota, @kd_pos, @nomor_telepon, @nomor_hp, @surel, @bidang_keahlian, @website_personal);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //        , new Npgsql.NpgsqlParameter("@id_personal", id_personal)
        //        , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
        //        , new Npgsql.NpgsqlParameter("@nama", nama)
        //        , new Npgsql.NpgsqlParameter("@nomor_ktp", nomor_ktp)
        //        , new Npgsql.NpgsqlParameter("@gelar_depan", gelar_depan)
        //        , new Npgsql.NpgsqlParameter("@gelar_belakang", gelar_belakang)
        //        , new Npgsql.NpgsqlParameter("@kd_jenis_kelamin", kd_jenis_kelamin)
        //        , new Npgsql.NpgsqlParameter("@tempat_lahir", tempat_lahir)
        //        , new Npgsql.NpgsqlParameter("@tgl_lahir", tanggal_lahir.ToString("yyyy-MM-dd"))
        //        , new Npgsql.NpgsqlParameter("@alamat", alamat)
        //        , new Npgsql.NpgsqlParameter("@kd_kota", kd_kota)
        //        , new Npgsql.NpgsqlParameter("@kd_pos", kd_pos)
        //        , new Npgsql.NpgsqlParameter("@nomor_telepon", nomor_telepon)
        //        , new Npgsql.NpgsqlParameter("@nomor_hp", nomor_hp)
        //        , new Npgsql.NpgsqlParameter("@surel", surel)
        //        , new Npgsql.NpgsqlParameter("@bidang_keahlian", bidang_keahlian)
        //        , new Npgsql.NpgsqlParameter("@website_personal", website_personal));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        //public bool deleteData(Guid id_personal)
        //{
        //    bool isSuccess = false;
        //    string strSQL = string.Format(@"SELECT * FROM pdpt.set_status_aktif_tendik('{0}','{1}');"
        //                    , id_personal, "0");
        //    isSuccess = this._db.ExecuteNonQuery(strSQL);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        //public bool setStatusKepegawaian(Guid id_personal, string kd_sts_aktif)
        //{
        //    bool isSuccess = false;
        //    string strSQL = string.Format(@"SELECT * FROM pdpt.set_status_aktif_tendik('{0}','{1}');"
        //                    , id_personal, kd_sts_aktif);
        //    isSuccess = this._db.ExecuteNonQuery(strSQL);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        //public bool cekNoKTP(ref DataTable dataTable, string nomor_ktp)
        //{
        //    bool isSuccess = false;
        //    string strSQL = string.Format(@"SELECT * FROM pdpt.get_cek_no_ktp('{0}');", nomor_ktp);
        //    isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}
        #endregion
    }
}