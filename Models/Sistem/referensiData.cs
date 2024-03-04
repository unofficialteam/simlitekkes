using System.Data;

namespace simlitekkes.Models.Sistem
{
    public class referensiData:_abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public referensiData()
        {
            setInitValues();
        }

        ~referensiData()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getBidangFokus(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_bidang_fokus();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenisInstitusi(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_jenis_institusi();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenisKegiatan(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_jenis_kegiatan();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenisKelamin(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_jenis_kelamin();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenisKategoriTahapan(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_kategori_tahapan();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getKelompokKegiatan(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_kelompok_kegiatan();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getKlaster(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_klaster();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPeranPersonil(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_peran_personil();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getProgramHibah(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_program_hibah();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getRumpunIlmu(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_rumpun_ilmu();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getSkemaKegiatan(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_skema_kegiatan();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getTahapanKegiatan(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_tahapan_kegiatan();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getTahapanKegiatanSkema(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_tahapan_kegiatan_skema();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPerguruanTinggi(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pdpt.list_perguruan_tinggi();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getProvinsi(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pdpt.list_provinsi();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenDik(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pdpt.list_jenjang_pendidikan();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarKota(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"select no_baris, kd_kota, nama_kota, kd_provinsi from pdpt.list_kota (0,0);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getJabatanFungsional(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pdpt.list_jabatan_fungsional();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getTargetCapaianLuaran(ref DataTable dataTable)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM referensi.list_target_capaian_luaran();";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getTahapanKegiatan(string kdJenisKegiatan, ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM referensi.list_tahapan_kegiatan_per_jenis_kegiatan(:kd_jenis_kegiatan);";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", kdJenisKegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getListBidangUnggulanStranas(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM referensi.list_bidang_unggulan_stranas();";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getListTemaIsuStrategis(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM referensi.list_tema_isu_strategis();";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getListBidangRapid(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM referensi.list_bidang_rapid();";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getListFokusKoridor(ref DataTable dataTable) 
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM referensi.list_fokus_koridor();";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getListKegiatanEkonomiUtamKoridor(ref DataTable dataTable, string idFokusKoridor)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_kegiatan_ekonomi_utama_koridor('{0}');", idFokusKoridor);
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