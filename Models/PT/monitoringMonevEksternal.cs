using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class monitoringMonevEksternal : _abstractModels
    {
        public monitoringMonevEksternal()
        {
            this.setInitValues();
        }

        #region Method

        public bool getJmlData(string thn_pelaksanaan_kegiatan, Guid id_institusi, 
            string judul, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT count(*)::int AS jml_data " +
                $"FROM hibah.list_hasil_penilaian_monev_eksternal_opt_pt('{thn_pelaksanaan_kegiatan}', " +
                $"'{id_institusi}', '{judul}', {limit}, {offset});";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listData(ref DataTable dataTable, string thn_pelaksanaan_kegiatan, Guid id_institusi, 
            string judul, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_hasil_penilaian_monev_eksternal_opt_pt('{thn_pelaksanaan_kegiatan}', " +
                $"'{id_institusi}', '{judul}', {limit}, {offset});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getExcel(ref DataTable dataTable, string thn_pelaksanaan_kegiatan, Guid id_institusi)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_hasil_penilaian_monev_eksternal_opt_pt('{thn_pelaksanaan_kegiatan}', '{id_institusi}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}