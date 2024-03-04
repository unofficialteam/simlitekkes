using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.PT
{
    public class daftarRenstra : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public daftarRenstra()
        {
            setInitValues();
        }

        ~daftarRenstra()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getDokumenRenstra(ref DataTable dataTable, Guid id_personal, Guid id_institusi, string thn_upload)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM penelitian.list_dokumen_renstra_institusi(@id_personal, @id_institusi, @thn_upload);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@thn_upload", thn_upload));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getJenisDokumenRenstra(ref DataTable dataTable, Guid id_personal)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM penelitian.list_jenis_dokumen_renstra(@id_personal);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@id_personal", id_personal));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool InsupDokumenRenstra(Guid id_sk, string no_sk, string kd_jenis_sk, Guid id_institusi_penerbit_sk, string thn_sk, string thn_upload)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.insup_dokumen_renstra(@id_sk::uuid, @no_sk::character varying, @kd_jenis_sk::character(2), @id_institusi_penerbit_sk::uuid, @thn_sk::character(4), @thn_upload::character(4))";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_sk", id_sk)
                , new Npgsql.NpgsqlParameter("@no_sk", no_sk)
                , new Npgsql.NpgsqlParameter("@kd_jenis_sk", kd_jenis_sk)
                , new Npgsql.NpgsqlParameter("@id_institusi_penerbit_sk", id_institusi_penerbit_sk)
                , new Npgsql.NpgsqlParameter("@thn_sk", thn_sk)
                , new Npgsql.NpgsqlParameter("@thn_upload", thn_upload));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool apaSK_RIPSudahDiunggah(Guid id_institusi, ref Guid id_sk, string p_kd_jenis_sk, string thn_sk, string thn_upload)
        {
            bool sudah = false;
            string strSQL = @"select t1.id_sk from penelitian.get_id_sk (
                                  @p_id_institusi_penerbit_sk::uuid,
                                  @p_kd_jenis_sk::char(2),
                                  @p_thn_upload::CHAR(4),
                                  @p_thn_sk::CHAR(4)
                                ) t1;";
            DataTable dt = new DataTable();
            sudah = this._db.FetchDataTable(strSQL, ref dt
               , new Npgsql.NpgsqlParameter("@p_id_institusi_penerbit_sk", id_institusi)
               , new Npgsql.NpgsqlParameter("@p_kd_jenis_sk", p_kd_jenis_sk)
               , new Npgsql.NpgsqlParameter("@p_thn_upload", thn_upload)
               , new Npgsql.NpgsqlParameter("@p_thn_sk", thn_sk)
               );
            if (!sudah)
                this._errorMessage = this._db.ErrorMessage;
            {
                if (dt.Rows.Count > 0)
                {
                    id_sk = Guid.Parse(dt.Rows[0]["id_sk"].ToString());
                    sudah = true;
                }
            }

            return sudah;
        }

        public bool apaDokumenRIPSudahDiunggah(Guid id_institusi, ref Guid id_sk, string p_kd_jenis_sk, string thn_sk, string thn_upload)
        {
            bool sudah = false;

            string strSQL = @"select t1.id_sk from penelitian.get_id_sk (
                                  @p_id_institusi_penerbit_sk::uuid,
                                  @p_kd_jenis_sk::char(2),
                                  @p_thn_upload::CHAR(4),
                                  @p_thn_sk::CHAR(4)
                                ) t1;";
            DataTable dt = new DataTable();
            sudah = this._db.FetchDataTable(strSQL, ref dt
               , new Npgsql.NpgsqlParameter("@p_id_institusi_penerbit_sk", id_institusi)
               , new Npgsql.NpgsqlParameter("@p_kd_jenis_sk", p_kd_jenis_sk)
               , new Npgsql.NpgsqlParameter("@p_thn_upload", thn_upload)
               , new Npgsql.NpgsqlParameter("@p_thn_sk", thn_sk)
               );
            if (!sudah)
                this._errorMessage = this._db.ErrorMessage;
            {
                if (dt.Rows.Count > 0)
                {
                    id_sk = Guid.Parse(dt.Rows[0]["id_sk"].ToString());
                    sudah = true;
                }
            }

            return sudah;
        }

        public bool cekSudahPernahUnggahSkRip(Guid id_institusi)
        {
            bool sudah = false;

            int jmlRow = 0;
            string strSQL = @"SELECT count(*)::int FROM penelitian.sk_rektor WHERE 
                                id_institusi_penerbit_sk = @id_institusi_penerbit_sk AND 
                                kd_jenis_sk = '02' ";
            if (!this._db.fetchDataSkalar(strSQL, ref jmlRow
               , new Npgsql.NpgsqlParameter("@id_institusi_penerbit_sk", id_institusi)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }
            if (jmlRow > 0)
                sudah = true;

            return sudah;
        }

        public bool cekSudahPernahUnggahDokumenRip(Guid id_institusi)
        {
            bool sudah = false;

            int jmlRow = 0;
            string strSQL = @"SELECT count(*)::int FROM penelitian.sk_rektor WHERE 
                                id_institusi_penerbit_sk = @id_institusi_penerbit_sk AND 
                                kd_jenis_sk = '03' ";
            if (!this._db.fetchDataSkalar(strSQL, ref jmlRow
               , new Npgsql.NpgsqlParameter("@id_institusi_penerbit_sk", id_institusi)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }
            if (jmlRow > 0)
                sudah = true;

            return sudah;
        }

        public bool apaSuratPengantarRenstraPpmSdhDiunggah(Guid id_institusi, ref Guid id_sk, string thn_sk, string thn_upload)
        {
            bool sudah = false;

            string strSQL = @"SELECT id_sk FROM penelitian.sk_rektor WHERE
                                id_institusi_penerbit_sk = @id_institusi_penerbit_sk AND
                                kd_jenis_sk = '06' AND thn_sk = @thn_sk AND thn_upload = @thn_upload ";
            DataTable dt = new DataTable();
            sudah = this._db.FetchDataTable(strSQL, ref dt
               , new Npgsql.NpgsqlParameter("@id_institusi_penerbit_sk", id_institusi)
               , new Npgsql.NpgsqlParameter("@thn_sk", thn_sk)
               , new Npgsql.NpgsqlParameter("@thn_upload", thn_upload));
            if (!sudah)
                this._errorMessage = this._db.ErrorMessage;
            {
                if (dt.Rows.Count > 0)
                {
                    id_sk = Guid.Parse(dt.Rows[0]["id_sk"].ToString());
                    sudah = true;
                }
            }

            return sudah;
        }

        public bool apaDokumenRenstraPpmSudahDiunggah(Guid id_institusi, ref Guid id_sk, string thn_sk, string thn_upload)
        {
            bool sudah = false;

            string strSQL = @"SELECT id_sk FROM penelitian.sk_rektor WHERE 
                            id_institusi_penerbit_sk = @id_institusi_penerbit_sk AND 
                            kd_jenis_sk='07' AND thn_sk = @thn_sk AND thn_upload = @thn_upload ";
            DataTable dt = new DataTable();
            sudah = this._db.FetchDataTable(strSQL, ref dt
               , new Npgsql.NpgsqlParameter("@id_institusi_penerbit_sk", id_institusi)
               , new Npgsql.NpgsqlParameter("@thn_sk", thn_sk)
               , new Npgsql.NpgsqlParameter("@thn_upload", thn_upload));
            if (!sudah)
                this._errorMessage = this._db.ErrorMessage;
            {
                if (dt.Rows.Count > 0)
                {
                    id_sk = Guid.Parse(dt.Rows[0]["id_sk"].ToString());
                    sudah = true;
                }
            }

            return sudah;
        }

        public bool cekSudahPernahUnggahSuratPengantarRenstraPpm(Guid id_institusi)
        {
            bool sudah = false;

            int jmlRow = 0;
            string strSQL = @"SELECT count(*)::int FROM penelitian.sk_rektor WHERE 
                                id_institusi_penerbit_sk = @id_institusi_penerbit_sk AND 
                                kd_jenis_sk = '06' ";
            if (!this._db.fetchDataSkalar(strSQL, ref jmlRow
               , new Npgsql.NpgsqlParameter("@id_institusi_penerbit_sk", id_institusi)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }
            if (jmlRow > 0)
                sudah = true;

            return sudah;
        }

        public bool cekSudahPernahUnggahDokumenRenstraPpm(Guid id_institusi)
        {
            bool sudah = false;

            int jmlRow = 0;
            string strSQL = @"SELECT count(*)::int FROM penelitian.sk_rektor WHERE 
                                id_institusi_penerbit_sk = @id_institusi_penerbit_sk AND 
                                kd_jenis_sk = '07' ";
            if (!this._db.fetchDataSkalar(strSQL, ref jmlRow
               , new Npgsql.NpgsqlParameter("@id_institusi_penerbit_sk", id_institusi)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }
            if (jmlRow > 0)
                sudah = true;

            return sudah;
        }

        public bool deleteRenstra(Guid id_sk)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.del_dokumen_renstra(@id_sk);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_sk", id_sk));
            if (!isSuccess)
            { 
                this._errorMessage = this._db.ErrorMessage;
            }
            else
            {

            }

            return isSuccess;
        }

        #endregion
    }
}