using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class daftarPerubahanJudul : _abstractModels
    {
        public daftarPerubahanJudul()
        {
            this.setInitValues();
        }

        #region Method

        public bool getJmlDataPerubahanJudul(string thn_pelaksanaan_kegiatan, string nama, 
            int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT count(*)::int AS jml_data " +
                $"FROM hibah.list_perubahan_judul('{thn_pelaksanaan_kegiatan}', " +
                $"'{nama}',  {limit}, {offset});";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listPerubahanJudul(ref DataTable dataTable, string thn_pelaksanaan_kegiatan, 
            string nama, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.list_perubahan_judul('{thn_pelaksanaan_kegiatan}', " +
                $"'{nama}', {limit}, {offset});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool excelPerubahanJudul(ref DataTable dataTable, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT nidn, nama, kd_perguruan_tinggi, nama_institusi, nama_skema, " +
                $"judul_lama, judul_baru, catatan " +
                $"FROM hibah.list_perubahan_judul('{thn_pelaksanaan_kegiatan}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}
