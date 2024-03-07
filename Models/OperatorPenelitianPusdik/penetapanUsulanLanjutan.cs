using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class penetapanUsulanLanjutan : _abstractModels
    {
        public penetapanUsulanLanjutan()
        {
            this.setInitValues();
        }

        #region Rekap Skema

        public bool getTotalRekapSkema(ref DataTable dataTable, string kd_program_hibah, 
            string thn_pelaksanaan_kegiatan, string kd_tahapan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT COALESCE(SUM(jml_usulan),0::INTEGER) AS total_jml_usulan, " +
                $"COALESCE(SUM(jml_tdk_lolos), 0::INTEGER) AS total_jml_tdk_lolos, " +
                $"COALESCE(SUM(jml_lolos), 0::INTEGER) AS total_jml_lolos, " +
                $"COALESCE(SUM(jml_blm_ditetapkan), 0::INTEGER) AS total_jml_blm_ditetapkan, " +
                $"COALESCE(SUM(jml_dana),0::money) AS total_jml_dana " +
                $"FROM hibah.list_rekap_skema_penetapan_pemenang_usulan_lanjutan_opt_ppsdm('{kd_program_hibah}', " +
                $"'{thn_pelaksanaan_kegiatan}', '{kd_tahapan_kegiatan}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listRekapSkema(ref DataTable dataTable, string kd_program_hibah, 
            string thn_pelaksanaan_kegiatan, string kd_tahapan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_rekap_skema_penetapan_pemenang_usulan_lanjutan_opt_ppsdm('{kd_program_hibah}', " +
                $"'{thn_pelaksanaan_kegiatan}', '{kd_tahapan_kegiatan}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;

        }

        #endregion

        #region Rekap PT

        public bool getTotalRekapPT(ref DataTable dataTable, string kd_program_hibah, 
            string thn_pelaksanaan_kegiatan, string kd_tahapan_kegiatan, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT COALESCE(SUM(jml_usulan),0::INTEGER) AS total_jml_usulan, " +
                $"COALESCE(SUM(jml_tdk_lolos), 0::INTEGER) AS total_jml_tdk_lolos, " +
                $"COALESCE(SUM(jml_lolos), 0::INTEGER) AS total_jml_lolos, " +
                $"COALESCE(SUM(jml_blm_ditetapkan), 0::INTEGER) AS total_jml_blm_ditetapkan, " +
                $"COALESCE(SUM(jml_dana),0::money) AS total_jml_dana " +
                $"FROM hibah.list_rekap_institusi_usulan_lanjutan_opt_ppsdm('{kd_program_hibah}', " +
                $"'{thn_pelaksanaan_kegiatan}', '{kd_tahapan_kegiatan}', {id_skema});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlDataRekapPT(string kd_program_hibah, 
            string thn_pelaksanaan_kegiatan, string kd_tahapan_kegiatan, int id_skema,
            string nama_institusi, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT count(*)::int AS jml_data " +
                $"FROM hibah.list_rekap_institusi_usulan_lanjutan_opt_ppsdm('{kd_program_hibah}', " +
                $"'{thn_pelaksanaan_kegiatan}', '{kd_tahapan_kegiatan}', " +
                $"{id_skema}, '{nama_institusi}', {limit}, {offset});";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listRekapPT(ref DataTable dataTable, string kd_program_hibah, 
            string thn_pelaksanaan_kegiatan, string kd_tahapan_kegiatan, int id_skema,
            string nama_institusi, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_rekap_institusi_usulan_lanjutan_opt_ppsdm('{kd_program_hibah}', " +
                $"'{thn_pelaksanaan_kegiatan}', '{kd_tahapan_kegiatan}', " +
                $"{id_skema}, '{nama_institusi}', {limit}, {offset});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getExcelRekapPT(ref DataTable dataTable, string kd_program_hibah, 
            string thn_pelaksanaan_kegiatan, string kd_tahapan_kegiatan, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT kd_perguruan_tinggi, nama_institusi, jml_usulan, jml_tdk_lolos, " +
                $"jml_lolos, jml_blm_ditetapkan, total_rekomendasi_dana, jml_dana AS jml_dana_disetujui " +
                $"FROM hibah.list_rekap_institusi_usulan_lanjutan_opt_ppsdm('{kd_program_hibah}', " +
                $"'{thn_pelaksanaan_kegiatan}', '{kd_tahapan_kegiatan}', {id_skema});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion

        #region Penetapan

        public bool getJmlDataPenetapan(string thn_pelaksanaan_kegiatan, string kd_tahapan_kegiatan, 
            Guid id_institusi, int id_skema, string nama, string sts_penetapan, 
            int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT count(*)::int AS jml_data " +
                $"FROM hibah.list_penetapan_pemenang_lajutan_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"'{kd_tahapan_kegiatan}', '{id_institusi}', " +
                $"{id_skema}, '{nama}', '{sts_penetapan}', {limit}, {offset});";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listPenetapan(ref DataTable dataTable, string thn_pelaksanaan_kegiatan, 
            string kd_tahapan_kegiatan, Guid id_institusi, int id_skema, string nama, string sts_penetapan, 
            int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_penetapan_pemenang_lajutan_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"'{kd_tahapan_kegiatan}', '{id_institusi}', " +
                $"{id_skema}, '{nama}', '{sts_penetapan}', {limit}, {offset});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool cekPermanenPenetapan(ref DataTable dataTable, string thn_pelaksanaan_kegiatan,
            string kd_tahapan_kegiatan, Guid id_institusi, int id_skema, string nama, string sts_penetapan,
            int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT count(CASE WHEN t1.kd_sts_permanen = '1' THEN 1 END) AS jml_permanen, " +
                $"COUNT(*)::int AS jml_data " +
                $"FROM hibah.list_penetapan_pemenang_lajutan_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"'{kd_tahapan_kegiatan}', '{id_institusi}', " +
                $"{id_skema}, '{nama}', '{sts_penetapan}', {limit}, {offset}) t1;";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupPenetapanLanjutan(Guid id_transaksi_kegiatan, int id_skema, string thn_usulan_kegiatan,
            string kd_klaster, string kd_sts_penetapan_pemenang, string nilai, decimal dana_disetujui,
            string thn_pelaksanaan_kegiatan, Guid id_usulan_kegiatan, Guid id_personal_ketua,
            Guid id_institusi, string kd_tahapan_kegiatan)
        {
            string strSQL = $@"SELECT * FROM hibah.insup_penetapan_pemenang_usulan_lanjutan_opt_ppsdm('{id_transaksi_kegiatan}'::uuid, 
                                {id_skema}::integer, '{thn_usulan_kegiatan}'::character(4), '{kd_klaster}'::character, 
                                '{kd_sts_penetapan_pemenang}'::character, {nilai}::numeric, {dana_disetujui}::money, 
                                '{thn_pelaksanaan_kegiatan}'::character(4), '{id_usulan_kegiatan}'::uuid, 
                                '{id_personal_ketua}'::uuid, '{id_institusi}'::uuid, '{kd_tahapan_kegiatan}'::character(2));";
            bool isSuccess = _db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlBlmDitetapkan(ref DataTable dataTable, string thn_pelaksanaan_kegiatan,
            string kd_tahapan_kegiatan, Guid id_institusi, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT COUNT(*)::int AS jml_data " +
                $"FROM hibah.list_penetapan_pemenang_lajutan_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"'{kd_tahapan_kegiatan}', '{id_institusi}', " +
                $"{id_skema}) WHERE kd_sts_penetapan_pemenang = '9';";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool setSimpanPermanen(string thn_pelaksanaan_kegiatan,
            string kd_tahapan_kegiatan, Guid id_institusi, int id_skema)
        {
            string strSQL = $@"SELECT * FROM hibah.set_permanen_penetapan_lanjutan_opt_ppsdm('{thn_pelaksanaan_kegiatan}', 
'{kd_tahapan_kegiatan}', '{id_institusi}', {id_skema});";
            bool isSuccess = _db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool setBukaPermanenLanjutan(string thn_pelaksanaan_kegiatan,
            string kd_tahapan_kegiatan, Guid id_institusi, int id_skema)
        {
            string strSQL = $@"SELECT * FROM hibah.set_buka_permanen_penetapan_usulan_lanjutan_opt_ppsdm('{thn_pelaksanaan_kegiatan}', 
'{kd_tahapan_kegiatan}', '{id_institusi}', {id_skema});";
            bool isSuccess = _db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool setBukaPermanen(string thn_pelaksanaan_kegiatan,
            string kd_tahapan_kegiatan, Guid id_institusi, int id_skema)
        {
            string strSQL = $@"SELECT * FROM hibah.set_buka_permanen_penetapan_usulan_baru_opt_ppsdm('{thn_pelaksanaan_kegiatan}', 
'{kd_tahapan_kegiatan}', '{id_institusi}', {id_skema});";
            bool isSuccess = _db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJmlPermanen(ref DataTable dataTable, string thn_pelaksanaan_kegiatan,
            string kd_tahapan_kegiatan, Guid id_institusi, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT COUNT(*)::int AS jml_data " +
                $"FROM hibah.list_penetapan_pemenang_lajutan_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"'{kd_tahapan_kegiatan}', '{id_institusi}', " +
                $"{id_skema}) WHERE kd_sts_permanen = '1';";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getExcelPenetapan(ref DataTable dataTable, string thn_pelaksanaan_kegiatan,
            string kd_tahapan_kegiatan, Guid id_institusi, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT nidn, nama, judul, nama_skema, kd_perguruan_tinggi, nama_institusi, " +
                $"thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, nama_rev_1, nilai_rev_1::text, rek_dana_rev_1, " +
                $"nama_rev_2, nilai_rev_2::text, rek_dana_rev_2, rerata_nilai::text, rerata_rek_dana, " +
                $"sts_penetapan_pemenang, dana_disetujui, " +
                $"CASE WHEN kd_sts_permanen = '0' THEN 'Belum permanen' ELSE 'Sudah permanen' END::varchar AS sts_permanen " +
                $"FROM hibah.list_penetapan_pemenang_lajutan_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"'{kd_tahapan_kegiatan}', '{id_institusi}', {id_skema});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}