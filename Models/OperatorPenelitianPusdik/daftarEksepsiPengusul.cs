using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class daftarEksepsiPengusul : _abstractModels
    {
        public daftarEksepsiPengusul()
        {
            setInitValues();
        }

        #region Methods

        public bool listData(ref DataTable dataTable, string thnUsulan, string thnPelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.list_whitelist_usulan_personal('{0}','{1}');"
                            , thnUsulan, thnPelaksanaan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public DataTable getRowByNIDN(string NIDN)
        {
            DataTable dt = new DataTable();
            string strSQL = @"SELECT * FROM pdpt.get_dosen_by_nidn_rb(:nidn);";

            if (!this._db.FetchDataTable(strSQL, ref dt
                , new Npgsql.NpgsqlParameter(":nidn", NIDN)))
                this._errorMessage = this._db.ErrorMessage;

            return dt;
        }

        public DataTable getRowByKTP(string KTP)
        {
            DataTable dt = new DataTable();
            string strSQL = @"SELECT * FROM pdpt.get_tendik_by_ktp(:nomorktp);";

            if (!this._db.FetchDataTable(strSQL, ref dt
                , new Npgsql.NpgsqlParameter(":nomorktp", KTP)))
                this._errorMessage = this._db.ErrorMessage;

            return dt;
        }

        public bool insertData(Guid id_personal, string kd_sts_pengusul, int id_skema, string thn_usulan, string thn_pelaksanaan, string tgl_mulai, string tgl_berakhir, string kd_tahapan_kegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.insert_whitelist_usulan_personal('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}');"
                            , id_personal, kd_sts_pengusul, id_skema, thn_usulan, thn_pelaksanaan, tgl_mulai, tgl_berakhir, kd_tahapan_kegiatan);
            //dataTable = new DataTable();
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateData(Guid id_whitelist_usulan_personal, int id_skema, string thn_usulan, string thn_pelaksanaan, string kd_sts_aktif, string tgl_mulai, string tgl_berakhir, string kd_tahapan_kegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM referensi.update_whitelist_usulan_personal('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}');"
                            , id_whitelist_usulan_personal, id_skema, thn_usulan, thn_pelaksanaan, kd_sts_aktif, tgl_mulai, tgl_berakhir, kd_tahapan_kegiatan);
            //dataTable = new DataTable();
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getWhitelist(ref DataTable dataTable, Guid id_whitelist_usulan_personal)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM referensi.get_whitelist_usulan_personal('{id_whitelist_usulan_personal}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        

        #endregion
    }
}
