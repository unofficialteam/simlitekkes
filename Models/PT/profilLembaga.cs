using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace simlitekkes.Models.PT
{
    public class profilLembaga : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public profilLembaga()
        {
            setInitValues();
        }

        ~profilLembaga()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getRow(ref DataTable dataTable, Guid idInstitusi, string kd_jenis_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM pdpt.get_lembaga(@id_institusi,@kd_jenis_kegiatan);");

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_institusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@kd_jenis_kegiatan", kd_jenis_kegiatan)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getRowPimpinan(ref DataTable dataTable, Guid idInstitusi, string nidn)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM pdpt.get_pimpinan(@nidn,@id_institusi);");

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_institusi", idInstitusi)
                , new Npgsql.NpgsqlParameter("@nidn", nidn)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getRowPimpinanNIDN(ref DataTable dataTable, string nidn)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM pdpt.get_pimpinan_by_nidn(@nidn);");

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@nidn", nidn)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getRowPimpinanPersonal(ref DataTable dataTable, Guid idPersonal)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM pdpt.get_pimpinan_by_id_personal(@id_personal);");

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_personal", idPersonal)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataBaru(Guid id_institusi, string kd_jenis_kegiatan, string no_sk_pendirian, string nama_lembaga, string alamat_lembaga, string no_telepon, string no_fax, string surel, string url, string nama_jabatan, Guid id_personal_pimpinan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pdpt.insup_lembaga(@id_institusi,
	                        @kd_jenis_kegiatan, 
                            @no_sk_pendirian,
                            @nama_lembaga,
                            @alamat_lembaga,
                            @no_telepon,
                            @no_fax,
                            @surel,
                            @url,	
                            @nama_jabatan,
                            @id_personal_pimpinan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                    , new Npgsql.NpgsqlParameter("@kd_jenis_kegiatan", kd_jenis_kegiatan)
                    , new Npgsql.NpgsqlParameter("@no_sk_pendirian", no_sk_pendirian)
                    , new Npgsql.NpgsqlParameter("@nama_lembaga", nama_lembaga)
                    , new Npgsql.NpgsqlParameter("@alamat_lembaga", alamat_lembaga)
                    , new Npgsql.NpgsqlParameter("@no_telepon", no_telepon)
                    , new Npgsql.NpgsqlParameter("@no_fax", no_fax)
                    , new Npgsql.NpgsqlParameter("@surel", surel)
                    , new Npgsql.NpgsqlParameter("@url", url)
                    , new Npgsql.NpgsqlParameter("@nama_jabatan", nama_jabatan)
                    , new Npgsql.NpgsqlParameter("@id_personal_pimpinan", id_personal_pimpinan)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        //public bool insertDataBaru(Guid id_institusi, string kd_jenis_kegiatan, string no_sk_pendirian, string nama_lembaga, string alamat_lembaga, string no_telepon, string no_fax, string surel, string url, string nama_jabatan, Guid id_personal_pimpinan)
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM pdpt.insup_lembaga(id_institusi::uuid,
        //                 kd_jenis_kegiatan::character, 
        //                    no_sk_pendirian::character varying,
        //                    nama_lembaga::character varying,
        //                    alamat_lembaga::character varying,
        //                    no_telepon::character varying,
        //                    no_fax::character varying,
        //                    surel::character varying,
        //                    url::character varying,	
        //                    nama_jabatan::character varying,
        //                    id_personal_pimpinan::uuid);";

        //    isSuccess = this._db.ExecuteNonQuery(strSQL
        //            , new Npgsql.NpgsqlParameter(":id_institusi", id_institusi)
        //            , new Npgsql.NpgsqlParameter(":kd_jenis_kegiatan", kd_jenis_kegiatan)
        //            , new Npgsql.NpgsqlParameter(":no_sk_pendirian", no_sk_pendirian)
        //            , new Npgsql.NpgsqlParameter(":nama_lembaga", nama_lembaga)
        //            , new Npgsql.NpgsqlParameter(":alamat_lembaga", alamat_lembaga)
        //            , new Npgsql.NpgsqlParameter(":no_telepon", no_telepon)
        //            , new Npgsql.NpgsqlParameter(":no_fax", no_fax)
        //            , new Npgsql.NpgsqlParameter(":surel", surel)
        //            , new Npgsql.NpgsqlParameter(":url", url)
        //            , new Npgsql.NpgsqlParameter(":nama_jabatan", nama_jabatan)
        //            , new Npgsql.NpgsqlParameter(":id_personal_pimpinan", id_personal_pimpinan)
        //            );
        //    if (!isSuccess)
        //        this._errorMessage = this._db.errorMessage;

        //    return isSuccess;
        //}

        #endregion

        #region Private Function

        #endregion
    }
}