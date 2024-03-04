using System;
using System.Collections.Generic;
using System.Data;

namespace daftarKonfigurasi
{
    [Serializable]
    public class eligibleSkemaInstitusi : simlitekkes.Models._abstractModels
    {
        #region Fields

        private string _kdJenisKegiatan;
        private string _kdKlaster;
        private int _idKategoriPerguruanTinggi;

        #endregion

        #region Konstruktor dan Destruktor

        public eligibleSkemaInstitusi()
        {
            setInitValues();
        }

        public eligibleSkemaInstitusi(string kdJenisKegiatan, string kdKlaster, int idKategoriPerguruanTinggi)
        {
            setInitValues();
            this._kdJenisKegiatan = kdJenisKegiatan;
            this._kdKlaster = kdKlaster;
            this._idKategoriPerguruanTinggi = idKategoriPerguruanTinggi;
        }

        ~eligibleSkemaInstitusi()
        {

        }

        #endregion

        #region Properties

        public string kdJenisKegiatan
        {
            get { return this._kdJenisKegiatan; }
            set { this._kdJenisKegiatan = value; }
        }

        public string kdKlaster
        {
            get { return this._kdKlaster; }
            set { this._kdKlaster = value; }
        }

        public int idKategoriPerguruanTinggi
        {
            get { return this._idKategoriPerguruanTinggi; }
            set { this._idKategoriPerguruanTinggi = value; }
        }

        #endregion

        #region Methods

        //public bool getJmlRecords()
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT count(*)::int as jml_konfig FROM (
        //                    SELECT 1 FROM referensi.skema_kegiatan t1
        //                    INNER JOIN referensi.program_hibah t2 ON t1.kd_program_hibah = t2.kd_program_hibah
        //                        AND t1.kd_sts_aktif = '1' AND t2.kd_jenis_kegiatan = :kd_jenis_kegiatan) AS t1;";

        //    isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords
        //            , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan));

        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool getCurrRecords()
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM public.list_konfig_eligibilitas_skema_institusi(:kd_jenis_kegiatan, :kd_klaster, :id_kategori_perguruan_tinggi, :limit, :offset);";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
        //        , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan)
        //        , new Npgsql.NpgsqlParameter(":kd_klaster", this._kdKlaster)
        //        , new Npgsql.NpgsqlParameter(":id_kategori_perguruan_tinggi", this._idKategoriPerguruanTinggi)
        //        , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
        //        , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool insertDataBaru(int idSkema, string kdKlaster, int idKategoriPerguruanTinggi)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT public.insup_konfig_klaster_institusi_pengusul(
        //                        :id_skema, :kd_klaster, :id_kategori_perguruan_tinggi);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
        //            , new Npgsql.NpgsqlParameter(":kd_klaster", kdKlaster)
        //            , new Npgsql.NpgsqlParameter(":id_kategori_perguruan_tinggi", idKategoriPerguruanTinggi));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool updateData(int idSkema, string kdKlaster, int idKategoriPerguruanTinggi, int idKonfig, string kdStsAktif = "1")
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT public.insup_konfig_klaster_institusi_pengusul(
        //                        :id_skema, :kd_klaster, :id_kategori_perguruan_tinggi, :id_konfig, :kd_sts_aktif);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
        //            , new Npgsql.NpgsqlParameter(":kd_klaster", kdKlaster)
        //            , new Npgsql.NpgsqlParameter(":id_kategori_perguruan_tinggi", idKategoriPerguruanTinggi)
        //            , new Npgsql.NpgsqlParameter(":id_konfig", idKonfig)
        //            , new Npgsql.NpgsqlParameter(":kd_sts_aktif", kdStsAktif));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public DataTable get

        #endregion

        #region Private Function

        #endregion
    }

    public class jadwalKegiatanPerTahapan : simlitekkes.Models._abstractModels
    {
        #region Fields

        private string _kdJenisKegiatan;
        private string _kdTahapan;
        private string _thn_usulan;
        private string _thn_pelaksanaan;

        #endregion

        #region Konstruktor dan Destruktor

        public jadwalKegiatanPerTahapan()
        {
            setInitValues();
        }

        public jadwalKegiatanPerTahapan(string kdJenisKegiatan, string kdTahapan, string thnUsulan, string thnKegiatan)
        {
            setInitValues();
            this._kdJenisKegiatan = kdJenisKegiatan;
            this._kdTahapan = kdTahapan;
            this._thn_usulan = thnUsulan;
            this._thn_pelaksanaan = thnKegiatan;
        }

        ~jadwalKegiatanPerTahapan()
        {

        }

        #endregion

        #region Properties


        #endregion

        #region Methods

        //public bool getJmlRecordsLanjutan()
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT count(*)::int as jml_konfig FROM (
        //                    SELECT 1 FROM referensi.program_hibah t1
        //                    INNER JOIN referensi.skema_kegiatan t2 ON t1.kd_program_hibah = t2.kd_program_hibah
        //                        AND t1.kd_jenis_kegiatan = :kd_jenis_kegiatan
        //                    INNER JOIN referensi.tahapan_kegiatan_skema t3 ON t2.id_skema = t3.id_skema
        //            AND t3.kd_tahapan_kegiatan = :kd_tahapan
        //                    ) AS t1;";

        //    isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords
        //            , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan)
        //            , new Npgsql.NpgsqlParameter(":kd_tahapan", this._kdTahapan));

        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        public bool getJmlRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int as jml_konfig FROM (
                            SELECT 1 FROM referensi.program_hibah t1
                            INNER JOIN referensi.skema_kegiatan t2 ON t1.kd_program_hibah = t2.kd_program_hibah
                                AND t1.kd_sts_aktif = '1' AND t2.kd_sts_aktif = '1' AND t1.kd_jenis_kegiatan = :kd_jenis_kegiatan
                            INNER JOIN referensi.tahapan_kegiatan_skema t3 ON t2.id_skema = t3.id_skema
				                AND t3.kd_tahapan_kegiatan = :kd_tahapan
                            ) AS t1;";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords
                    , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan)
                    , new Npgsql.NpgsqlParameter(":kd_tahapan", this._kdTahapan));

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrRecords()
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM public.list_konfig_pengelolaan_kegiatan_by_jenis_kegiatan(
                        :kd_jenis_kegiatan, :kd_tahapan, :thn_usulan, :thn_pelaksanaan, :limit, :offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan)
                , new Npgsql.NpgsqlParameter(":kd_tahapan", this._kdTahapan)
                , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
                , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
                , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaru(int idSkema, string tglMulai, string tglBerakhir)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan(
                        :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
                        :tgl_mulai::date, :tgl_berakhir::date);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
                    , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
                    , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
                    , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
                    , new Npgsql.NpgsqlParameter(":tgl_mulai", tglMulai)
                    , new Npgsql.NpgsqlParameter(":tgl_berakhir", tglBerakhir)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertMultiDataBaru(List<int> idSkema, string tglMulai, string tglBerakhir)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan(
                        :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
                        :tgl_mulai::date, :tgl_berakhir::date);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
                    , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
                    , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
                    , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
                    , new Npgsql.NpgsqlParameter(":tgl_mulai", tglMulai)
                    , new Npgsql.NpgsqlParameter(":tgl_berakhir", tglBerakhir)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateData(int idSkema, string tglMulai, string tglBerakhir, int idKonfig)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan(
                        :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
                        :tgl_mulai::date, :tgl_berakhir::date, :id_konfig);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
                    , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
                    , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
                    , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
                    , new Npgsql.NpgsqlParameter(":tgl_mulai", tglMulai)
                    , new Npgsql.NpgsqlParameter(":tgl_berakhir", tglBerakhir)
                    , new Npgsql.NpgsqlParameter(":id_konfig", idKonfig)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool setOff(int idSkema, string tglMulai, int idKonfig)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan(
                        :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
                        :tgl_mulai::timestamp without time zone, now()::timestamp without time zone, :id_konfig);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
                    , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
                    , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
                    , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
                    , new Npgsql.NpgsqlParameter(":tgl_mulai", tglMulai)
                    , new Npgsql.NpgsqlParameter(":id_konfig", idKonfig)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool setOffAll(List<int> idSkema, List<int> idKonfig)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan(
                        :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
                        now()::timestamp without time zone, now()::timestamp without time zone, :id_konfig);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
                    , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
                    , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
                    , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
                    , new Npgsql.NpgsqlParameter(":id_konfig", idKonfig)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getNoJadwalSkema(ref DataTable dtNoJadwal)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM public.list_konfig_pengelolaan_kegiatan_by_jenis_kegiatan(
                        :kd_jenis_kegiatan, :kd_tahapan, :thn_usulan, :thn_pelaksanaan)
                        WHERE kd_sts_aktif = '0';";

            isSuccess = this._db.FetchDataTable(strSQL, ref dtNoJadwal
                , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan)
                , new Npgsql.NpgsqlParameter(":kd_tahapan", this._kdTahapan)
                , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
                , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCurrentJadwalSkema(ref DataTable dtNoJadwal)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM public.list_konfig_pengelolaan_kegiatan_by_jenis_kegiatan(
                        :kd_jenis_kegiatan, :kd_tahapan, :thn_usulan, :thn_pelaksanaan)
                        WHERE kd_sts_aktif = '1';";

            isSuccess = this._db.FetchDataTable(strSQL, ref dtNoJadwal
                , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan)
                , new Npgsql.NpgsqlParameter(":kd_tahapan", this._kdTahapan)
                , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
                , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenisKegiatan(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.get_jenis_kegiatan({0}, {1});", 0, -1);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        // List Konfigurasi Lanjutan
        //public bool getCurrRecordsLanjutan()
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM public.list_konfig_pengelolaan_kegiatan_lanjutan_by_jenis_kegiatan(
        //                :kd_jenis_kegiatan, :kd_tahapan, :thn_usulan, :thn_pelaksanaan, :limit, :offset);";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
        //        , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan)
        //        , new Npgsql.NpgsqlParameter(":kd_tahapan", this._kdTahapan)
        //        , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
        //        , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
        //        , new Npgsql.NpgsqlParameter(":offset", this._currentPage * this._rowsPerPage)
        //        , new Npgsql.NpgsqlParameter(":limit", this._rowsPerPage));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool getCurrentJadwalSkemaLanjutan(ref DataTable dtNoJadwal)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM public.list_konfig_pengelolaan_kegiatan_lanjutan_by_jenis_kegiatan(
        //                :kd_jenis_kegiatan, :kd_tahapan, :thn_usulan, :thn_pelaksanaan)
        //                WHERE kd_sts_aktif = '1';";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref dtNoJadwal
        //        , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan)
        //        , new Npgsql.NpgsqlParameter(":kd_tahapan", this._kdTahapan)
        //        , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
        //        , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool setOffLanjutan(int idSkema, string tglMulai, int idKonfig)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan_lanjutan(
        //                :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
        //                :tgl_mulai::timestamp without time zone, now()::timestamp without time zone, :id_konfig_lanjutan);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
        //            , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
        //            , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
        //            , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
        //            , new Npgsql.NpgsqlParameter(":tgl_mulai", tglMulai)
        //            , new Npgsql.NpgsqlParameter(":id_konfig_lanjutan", idKonfig)
        //            );
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool insertMultiDataBaruLanjutan(List<int> idSkema, string tglMulai, string tglBerakhir)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan_lanjutan(
        //                :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
        //                :tgl_mulai::date, :tgl_berakhir::date);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
        //            , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
        //            , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
        //            , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
        //            , new Npgsql.NpgsqlParameter(":tgl_mulai", tglMulai)
        //            , new Npgsql.NpgsqlParameter(":tgl_berakhir", tglBerakhir)
        //            );
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool insertDataBaruLanjutan(int idSkema, string tglMulai, string tglBerakhir)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan_lanjutan(
        //                :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
        //                :tgl_mulai::date, :tgl_berakhir::date);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
        //            , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
        //            , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
        //            , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
        //            , new Npgsql.NpgsqlParameter(":tgl_mulai", tglMulai)
        //            , new Npgsql.NpgsqlParameter(":tgl_berakhir", tglBerakhir)
        //            );
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool updateDataLanjutan(int idSkema, string tglMulai, string tglBerakhir, int idKonfig)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan_lanjutan(
        //                :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
        //                :tgl_mulai::date, :tgl_berakhir::date, :id_konfig_lanjutan);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
        //            , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
        //            , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
        //            , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
        //            , new Npgsql.NpgsqlParameter(":tgl_mulai", tglMulai)
        //            , new Npgsql.NpgsqlParameter(":tgl_berakhir", tglBerakhir)
        //            , new Npgsql.NpgsqlParameter(":id_konfig_lanjutan", idKonfig)
        //            );
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool getNoJadwalSkemaLanjutan(ref DataTable dtNoJadwal)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM public.list_konfig_pengelolaan_kegiatan_lanjutan_by_jenis_kegiatan(
        //                :kd_jenis_kegiatan, :kd_tahapan, :thn_usulan, :thn_pelaksanaan)
        //                WHERE kd_sts_aktif = '0';";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref dtNoJadwal
        //        , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", this._kdJenisKegiatan)
        //        , new Npgsql.NpgsqlParameter(":kd_tahapan", this._kdTahapan)
        //        , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
        //        , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan));
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        //public bool setOffAllLanjutan(List<int> idSkema, List<int> idKonfig)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT public.insup_konfig_pengelolaan_kegiatan_lanjutan(
        //                :id_skema, :kd_tahapan_kegiatan, :thn_usulan, :thn_pelaksanaan,
        //                now()::timestamp without time zone, now()::timestamp without time zone, :id_konfig_lanjutan);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":id_skema", idSkema)
        //            , new Npgsql.NpgsqlParameter(":kd_tahapan_kegiatan", this._kdTahapan)
        //            , new Npgsql.NpgsqlParameter(":thn_usulan", this._thn_usulan)
        //            , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", this._thn_pelaksanaan)
        //            , new Npgsql.NpgsqlParameter(":id_konfig_lanjutan", idKonfig)
        //            );
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;

        //    return isSuccess;
        //}

        #endregion

        #region Private Function

        #endregion

    }
}