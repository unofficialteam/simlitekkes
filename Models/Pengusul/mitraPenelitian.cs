using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class mitraPenelitian : _abstractModels
    {
        #region Variable Class
        
        #endregion

        #region Konstruktor dan Destruktor

        public mitraPenelitian()
        {
            setInitValues();
        }

        ~mitraPenelitian()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getMitraPenelitianPerSkema(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM penelitian.get_kategori_mitra_penelitian_by_skema('{0}');", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listNegara(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT kd_negara, nama_negara FROM pdpt.negara order by nama_negara");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listMitraPelaksanaPenelitian(ref DataTable dataTable, Guid idUsulanKegiatan, int p_jml_data, int p_offset)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_mitra_rb(@idUsulanKegiatan, @p_jml_data, @p_offset);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idUsulanKegiatan", idUsulanKegiatan)
            , new Npgsql.NpgsqlParameter("@p_jml_data", p_jml_data)
            , new Npgsql.NpgsqlParameter("@p_offset", p_offset)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupMitraPelaksana
        (
            Guid idUsulanKegiatan,
            string namaMitra,
            string namaInstitusiMitra,
            string alamatInstitusiMitra,
            string surel,
            string kdNegara,
            int kdKategoriMitra,
            Guid idMitra
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_mitra_rb(
                        @id_usulan_kegiatan::uuid,
                        @nama_mitra::character varying,
                        @nama_institusi_mitra::character varying,
                        @alamat_institusi_mitra::character varying,
                        @surel::character varying,
                        @kd_negara::character(3), 
                        @kd_kategori_mitra::integer,
                        @id_mitra::uuid
                            );";

            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@nama_mitra", namaMitra)
                    , new Npgsql.NpgsqlParameter("@nama_institusi_mitra", namaInstitusiMitra)
                    , new Npgsql.NpgsqlParameter("@alamat_institusi_mitra", alamatInstitusiMitra)
                    , new Npgsql.NpgsqlParameter("@surel", surel)
                    , new Npgsql.NpgsqlParameter("@kd_negara", kdNegara)
                    , new Npgsql.NpgsqlParameter("@kd_kategori_mitra", kdKategoriMitra)
                    , new Npgsql.NpgsqlParameter("@id_mitra", idMitra)))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool insupDanaMitraPelaksana
        (
            Guid idUsulanKegiatan,
            int urutanThnUsulan,
            Guid idMitra,
            String dana,
            int kdKategoriMitra
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_dana_mitra_rb(
                        @id_usulan_kegiatan::uuid,
                        @urutan_thn_usulan::smallint,     
                        @id_mitra::uuid,
                        @dana::money,
                        @kd_kategori_mitra::integer
                            );";

            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@urutan_thn_usulan", urutanThnUsulan)
                    , new Npgsql.NpgsqlParameter("@id_mitra", idMitra)
                    , new Npgsql.NpgsqlParameter("@dana", dana)
                    , new Npgsql.NpgsqlParameter("@kd_kategori_mitra", kdKategoriMitra)
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

        public bool deleteData(Guid idMitraPelaksana)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.del_mitra(@id_mitra::uuid);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_mitra", idMitraPelaksana));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDataMitraPelaksana(ref DataTable dataTable, Guid idUsulanKegiatan, Guid idMitra)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_mitra_rb(@idUsulanKegiatan, 0, 0) WHERE id_mitra = @idMitra;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@idUsulanKegiatan", idUsulanKegiatan)
                            , new Npgsql.NpgsqlParameter("@idMitra", idMitra));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateStsDokMitra(Guid idMitra)
        {
            bool isSuccess = false;
            string strSQL = string.Format("UPDATE penelitian.mitra SET kd_sts_unggah_pernyataan ='1', tgl_unggah_pernyataan=now() WHERE id_mitra ='{0}'", idMitra);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getMitraWajibBySkema(ref DataTable dataTable, int idSkema)
        {
            bool isSuccess = false;
            string strSQL = @"select kd_kategori_mitra, kategori_mitra from referensi.get_skema_wajib (:p_id_skema);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@p_id_skema", idSkema));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}