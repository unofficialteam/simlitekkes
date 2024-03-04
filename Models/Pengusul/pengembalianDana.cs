using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace simlitekkes.Models.Pengusul
{
    public class pengembalianDana : _abstractModels
    {
        #region Variable Class
        
        #endregion

        #region Konstruktor dan Destruktor

        public pengembalianDana()
        {
            setInitValues();
        }

        ~pengembalianDana()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        //public bool listPengembalianDana(ref DataTable dataTable, Guid id_personal, int JumlahData = 0, int Offset = 0)
        //{
        //    bool isSuccess = false;
        //    string strSQL;

        //    strSQL = string.Format("SELECT * FROM hibah.list_pengembalian_dana('{0}',{1},{2});",
        //                            id_personal, JumlahData, Offset);

        //    dataTable = new DataTable();
        //    isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        public bool getJmlDaftarUsulan(Guid idPersonal)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_usulan FROM (
                            SELECT 1 FROM hibah.list_pengembalian_dana_rb(@p_id_personal)) AS t1;";

            isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@p_id_personal", idPersonal)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarUsulan(Guid idPersonal)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.list_pengembalian_dana_rb(@p_id_personal, @limit, @offset);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@p_id_personal", idPersonal)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupPengembalianDana(Guid id_usulan_kegiatan, string no_ntpn, string jml_setoran, string sts_unggah_berkas)
        {
            bool isSuccess = false;

            string strSQL = "SELECT hibah.insup_pengembalian_dana(@p_id_usulan_kegiatan::uuid, " +
                                "@p_no_ntpn::character varying, @p_jml_setoran::money, @p_sts_unggah_berkas::character);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", id_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_no_ntpn", no_ntpn)
                    , new Npgsql.NpgsqlParameter("@p_jml_setoran", jml_setoran)
                    , new Npgsql.NpgsqlParameter("@p_sts_unggah_berkas", sts_unggah_berkas)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updatePengembalianDana(Guid id_usulan_kegiatan, string sts_unggah_berkas)
        {
            bool isSuccess = false;

            string strSQL = "SELECT hibah.update_sts_berkas_pengembalian_dana(@p_id_usulan_kegiatan::uuid, @p_sts_unggah_berkas::character);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", id_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_sts_unggah_berkas", sts_unggah_berkas)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}