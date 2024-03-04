using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class konfirmasiPersetujuan : _abstractModels
    {
        #region Variable Class
        
        #endregion

        #region Konstruktor dan Destruktor

        public konfirmasiPersetujuan()
        {
            setInitValues();
        }

        ~konfirmasiPersetujuan()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getJmlRecords(ref DataTable dataTable, Guid id_personal)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.get_jml_konfirmasi_anggota('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarUsulanKonfirmasi(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_konfirmasi_anggota('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        
        public bool updateStsKonfirmasi(string kd_sts_konfirmasi, Guid id_personil)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.update_konfirmasi_persetujuan_anggota_new(
                    @p_kd_sts_konfirmasi::character,
                    @p_id_personil::uuid);";
            if (this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@p_kd_sts_konfirmasi", kd_sts_konfirmasi)
                , new Npgsql.NpgsqlParameter("@p_id_personil", id_personil)
                ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool deleteDataPersonil(Guid id_personil)
        {
            bool isSuccess = false;
            
            string strSQL = string.Format(@"DELETE FROM hibah.personil WHERE id_personil = '{0}';", id_personil);

            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}