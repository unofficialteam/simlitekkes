using System;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class persyaratanUmum : _abstractModels
    {
        #region Variable Class
        
        #endregion

        #region Konstruktor dan Destruktor

        public persyaratanUmum()
        {
            setInitValues();
        }

        ~persyaratanUmum()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getBlacklist(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pengguna.get_blacklist_personal('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekDataDosen(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pdpt.get_cek_data_dosen('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekSinta(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM penelitian.get_cek_persyaratan_sinta('{0}');", id_personal);
            
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        
        public bool getJmTanggungan(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT COALESCE(sum(t1.jml_tanggungan),0)::bigint AS total_jml_tanggungan FROM hibah.list_cek_tanggungan('{0}') AS t1;",
                id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarTanggungan(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_cek_tanggungan('{0}') WHERE jml_tanggungan > 0;", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekKuotaUsulan(ref DataTable dataTable, string id_personal, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.cek_jml_usulan_persyaratan_umum('{0}','{1}');", 
                id_personal, thn_pelaksanaan_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlUsulanBaru(ref DataTable dataTable, string id_personal, string thn_usulan_kegiatan, string kd_jenis_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT count(*)::int AS jml_data FROM (SELECT 1 FROM hibah.list_daftar_usulan_baru_penelitian_pengabdian('{0}','{1}','{2}')) AS t1;",
                id_personal, thn_usulan_kegiatan, kd_jenis_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlUsulanBaruRb(ref DataTable dataTable, string idPersonal, string thnUsulanKegiatan, string thnPelaksanaanKegiatan, string kdJenisKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT count(*)::int AS jml_data FROM (SELECT 1 FROM hibah.list_daftar_usulan_baru_penelitian_pengabdian_rb('{0}','{1}','{2}','{3}')) AS t1;",
                idPersonal, thnUsulanKegiatan, thnPelaksanaanKegiatan, kdJenisKegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarUsulanBaru(ref DataTable dataTable, string id_personal, string thn_usulan, string thn_pelaksanaan, string kd_jenis_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_usulan_baru_penelitian_pengabdian('{0}','{1}','{2}','{3}');", 
                id_personal, thn_usulan, thn_pelaksanaan, kd_jenis_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarUsulanBaru(ref DataTable dataTable, string id_personal, string thn_pelaksanaan, string kd_jenis_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_usulan_baru_penelitian_pengabdian('{0}','{1}','{2}');",
                id_personal, thn_pelaksanaan, kd_jenis_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarUsulanLanjutan(ref DataTable dataTable, string id_personal, string thn_usulan_kegiatan, string kd_jenis_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_daftar_usulan_lanjutan_penelitian_pengabdian('{0}','{1}','{2}');",
                id_personal, thn_usulan_kegiatan, kd_jenis_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        
        public bool listUsulanLanjutanBlmDiajukan(ref DataTable dataTable, string id_personal, string thn_usulan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_next_usulan_lanjutan_personal('{0}','{1}');",
                id_personal, thn_usulan_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listUsulanAbdimasLanjutanBlmDiajukan(ref DataTable dataTable, string id_personal, string thn_usulan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_next_usulan_abdimas_lanjutan_personal('{0}','{1}');",
                id_personal, thn_usulan_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getKlasterPersonal(ref DataTable dataTable, string id_personal, string kd_jenis_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pdpt.get_klaster_by_personal('{0}','{1}');",
                id_personal, kd_jenis_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getSkemaByKlaster(ref DataTable dataTable, string kd_klaster, bool is_tendik = false)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM referensi.list_skema_by_klaster('{0}');", kd_klaster);
            if (is_tendik)
                strSQL = string.Format("SELECT * FROM referensi.list_skema_by_klaster('{0}') WHERE id_skema = 7;", kd_klaster);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlRekamJejak(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM penelitian.list_jml_rekam_jejak('{0}');", id_personal);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteDataUsulanBaru(Guid id_usulan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.del_usulan_penelitian_rb(@id_usulan_kegiatan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", id_usulan_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDataPendaftaranReviewerPenelitian(ref DataTable dataTable, string idPersonal, string thnPendaftaran, 
            string p_kd_kategori_reviewer)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pendaftaran_reviewer.get_data_pendaftaran_rev_penelitian ('{0}','{1}','{2}');", 
                idPersonal, thnPendaftaran, p_kd_kategori_reviewer);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}