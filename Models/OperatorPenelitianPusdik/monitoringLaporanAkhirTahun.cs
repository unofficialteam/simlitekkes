using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class monitoringLaporanAkhirTahun : _abstractModels
    {
        public monitoringLaporanAkhirTahun()
        {
            this.setInitValues();
        }

        #region Rekap Skema

        public bool listRekapSkema(ref DataTable dataTable, string thn_pelaksanaan_kegiatan,
            string kd_jenis_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_rekap_skema_laporan_akhir_thn_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"'{kd_jenis_kegiatan}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;

        }

        #endregion

        #region Daftar Laporan Akhir Tahun

        public bool getJmlDataLapAkhir(string thn_pelaksanaan_kegiatan, int id_skema,
            string kd_sts_pelaksanaan, string nama, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT count(*)::int AS jml_data " +
                $"FROM hibah.list_monitoring_laporan_akhir_thn_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"{id_skema}, '{kd_sts_pelaksanaan}', '{nama}',  {limit}, {offset});";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listDaftarLapAkhir(ref DataTable dataTable, string thn_pelaksanaan_kegiatan, int id_skema,
            string kd_sts_pelaksanaan, string nama, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_monitoring_laporan_akhir_thn_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"{id_skema}, '{kd_sts_pelaksanaan}', '{nama}', {limit}, {offset});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getExcelDaftarLapAkhir(ref DataTable dataTable, string thn_pelaksanaan_kegiatan, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT nidn, nama, nama_institusi, judul, nama_singkat_skema, sts_pelaksanaan, tahun_ke " +
                $"FROM hibah.list_monitoring_laporan_akhir_thn_opt_ppsdm('{thn_pelaksanaan_kegiatan}', " +
                $"{id_skema});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}
