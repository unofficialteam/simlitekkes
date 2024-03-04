using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class pengirimanUserPassword : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public pengirimanUserPassword()
        {
            setInitValues();
        }

        ~pengirimanUserPassword()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool listPoltekesKemenkes(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pdpt.list_pengiriman_user_password_operator();";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool listUserPasswordOptPtByInstitusi(ref DataTable dataTable, string p_id_institusi)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pdpt.list_pengiriman_user_password_operator() t1 where t1.id_institusi='{0}';";
            strSQL = string.Format(strSQL, p_id_institusi);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertUserPasswordOperatorPT(string p_id_institusi, int p_id_peran, int p_jml_user)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengguna.insert_user_password_operator_institusi(@p_id_institusi::uuid, @p_id_peran::integer, @p_jml_user::integer);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_institusi", p_id_institusi)
            , new Npgsql.NpgsqlParameter("@p_id_peran", p_id_peran)
            , new Npgsql.NpgsqlParameter("@p_jml_user", p_jml_user)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertLogPengirimanAkunOptPT(string p_surel_tujuan, string p_surel_pengirim)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengguna.insert_log_pengiriman_akun_opt_pt(@p_surel_tujuan::varchar, @p_surel_pengirim::varchar);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_surel_tujuan", p_surel_tujuan)
            , new Npgsql.NpgsqlParameter("@p_surel_pengirim", p_surel_pengirim)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertLogPengirimanAkunOptPT2(Guid id_kontak_pic_pengguna_institusi, string p_surel_tujuan, string kd_status_pengiriman, string p_surel_pengirim)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengguna.insert_pengiriman_user_password_kontak_pic(@id_kontak_pic_pengguna_institusi, @surel, @kd_sts_pengiriman, @surel_pengirim);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@id_kontak_pic_pengguna_institusi", id_kontak_pic_pengguna_institusi)
            , new Npgsql.NpgsqlParameter("@surel", p_surel_tujuan)
            , new Npgsql.NpgsqlParameter("@kd_sts_pengiriman", kd_status_pengiriman)
            , new Npgsql.NpgsqlParameter("@surel_pengirim", p_surel_pengirim)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion

    }
}