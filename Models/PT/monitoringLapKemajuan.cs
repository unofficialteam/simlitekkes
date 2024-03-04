using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class monitoringLapKemajuan : _abstractModels
    {
        public monitoringLapKemajuan()
        {
            this.setInitValues();
        }

        #region Rekap Skema

        public bool listRekapSkema(ref DataTable dataTable, string thn_pelaksanaan_kegiatan, 
            string kd_jenis_kegiatan, Guid id_institusi)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_rekap_skema_laporan_kemajuan_opt_pt('{thn_pelaksanaan_kegiatan}', " +
                $"'{kd_jenis_kegiatan}', '{id_institusi}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;

        }

        #endregion

        #region Daftar Laporan Kemajuan

        public bool getJmlDataLapKemajuan(string thn_pelaksanaan_kegiatan, int id_skema, Guid id_institusi, 
            string kd_sts_pelaksanaan, string nama, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT count(*)::int AS jml_data " +
                $"FROM hibah.list_monitoring_laporan_kemajuan_opt_pt('{thn_pelaksanaan_kegiatan}', " +
                $"{id_skema}, '{id_institusi}', '{kd_sts_pelaksanaan}', '{nama}',  {limit}, {offset});";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listDaftarLapKemajuan(ref DataTable dataTable, string thn_pelaksanaan_kegiatan, int id_skema, Guid id_institusi,
            string kd_sts_pelaksanaan, string nama, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_monitoring_laporan_kemajuan_opt_pt('{thn_pelaksanaan_kegiatan}', " +
                $"{id_skema}, '{id_institusi}', '{kd_sts_pelaksanaan}', '{nama}',  {limit}, {offset});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getExcelDaftarLapKemajuan(ref DataTable dataTable, string thn_pelaksanaan_kegiatan, int id_skema, Guid id_institusi)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT nidn, nama, judul, nama_singkat_skema, sts_pelaksanaan, tahun_ke " +
                $"FROM hibah.list_monitoring_laporan_kemajuan_opt_pt('{thn_pelaksanaan_kegiatan}', " +
                $"{id_skema}, '{id_institusi}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion

    }
}