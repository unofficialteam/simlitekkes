using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class beranda : _abstractModels
    {
        public beranda()
        {
            this.setInitValues();
        }

        #region Methods

        public bool getRekapTahapanPengajuanUsulan(ref DataTable dataTable, string tahun_pengajuan, string tahun_pelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.rekap_tahapan_pengajuan_usulan(@tahun_pengajuan, @tahun_pelaksanaan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@tahun_pengajuan", tahun_pengajuan)
                , new Npgsql.NpgsqlParameter("@tahun_pelaksanaan", tahun_pelaksanaan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getTahunUsulan(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_thn_usulan_kegiatan();");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getTahunPelaksanaan(ref DataTable dataTable, string tahun_usulan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_thn_pelaksanaan_kegiatan('{0}');", tahun_usulan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}