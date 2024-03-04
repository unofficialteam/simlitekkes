using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pelaksanaan
{
    public class luaranLaporanKemajuan : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaranLaporanKemajuan()
        {
            setInitValues();
        }

        ~luaranLaporanKemajuan()
        {

        }

        #endregion

        #region Properties

        #endregion


        #region Methods

        public bool cekTargetLuaran(ref DataTable dataTable, string idTransaksiKegiatan, int idKelompokLuaran, int idJenisLuaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_cek_id_target_luaran('{0}',{1},{2});",
                idTransaksiKegiatan, idKelompokLuaran, idJenisLuaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListTargetJenisLuaran(ref DataTable dataTable, int p_id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_target_jenis_luaran(@p_id_jenis_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListTargetJenisLuaranDesain(ref DataTable dataTable, int p_id_jenis_luaran, int p_id_target_capaian_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_target_jenis_luaran_desain(@p_id_jenis_luaran, @p_id_target_capaian_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListJenisDokumen(ref DataTable dataTable, int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_jenis_dokumen(@p_id_target_jenis_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListDokumenBuktiLuaran(ref DataTable dataTable, Guid p_id_transaksi_kegiatan, int p_id_kelompok_luaran, int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_bukti_luaran(@p_id_transaksi_kegiatan, @p_id_kelompok_luaran, @p_id_target_jenis_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        
        public bool ListDokumenBuktiLuaran(ref DataTable dataTable, Guid p_id_transaksi_kegiatan, 
            Guid id_luaran_dijanjikan, int p_id_kelompok_luaran, int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_bukti_luaran('{0}', '{1}', {2}, {3});";
            strSQL = string.Format(strSQL, p_id_transaksi_kegiatan, id_luaran_dijanjikan, p_id_kelompok_luaran, p_id_target_jenis_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable); 

            //string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_bukti_luaran(@p_id_transaksi_kegiatan, @id_luaran_dijanjikan, @p_id_kelompok_luaran, @p_id_target_jenis_luaran);";
            //isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
            //, new Npgsql.NpgsqlParameter("@id_luaran_dijanjikan", id_luaran_dijanjikan)            
            //, new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            //, new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            //);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListDokumenBuktiLuaranDesain(ref DataTable dataTable, Guid p_id_transaksi_kegiatan, 
            int p_id_kelompok_luaran, int p_id_target_jenis_luaran, int p_id_target_capaian_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_bukti_luaran_desain(@p_id_transaksi_kegiatan, @p_id_kelompok_luaran, @p_id_target_jenis_luaran, @p_id_target_capaian_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListDokumenBuktiLuaranPilih(ref DataTable dataTable, Guid p_id_transaksi_kegiatan, int p_id_kelompok_luaran, int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_bukti_luaran_pilih(@p_id_transaksi_kegiatan, @p_id_kelompok_luaran, @p_id_target_jenis_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListDokumenBuktiLuaranPilih(ref DataTable dataTable, Guid p_id_transaksi_kegiatan,
            Guid id_luaran_dijanjikan,
            int p_id_kelompok_luaran, int p_id_target_jenis_luaran)
        {
            bool isSuccess = false;


            string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_bukti_luaran_pilih('{0}', '{1}', {2}, {3});";
            strSQL = string.Format(strSQL, p_id_transaksi_kegiatan, id_luaran_dijanjikan, p_id_kelompok_luaran, p_id_target_jenis_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);


            //string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_bukti_luaran_pilih(@p_id_transaksi_kegiatan, 
            //                    @id_luaran_dijanjikan,
            //                    @p_id_kelompok_luaran, @p_id_target_jenis_luaran);";

            //isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            //, new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
            //, new Npgsql.NpgsqlParameter("@id_luaran_dijanjikan", id_luaran_dijanjikan)
            //, new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            //, new Npgsql.NpgsqlParameter("@p_id_target_jenis_luaran", p_id_target_jenis_luaran)
            //);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }



        public bool insupTargetLuaran(ref DataTable dt, int p_id_target_luaran, Guid p_id_luaran_dijanjikan,
           Guid p_id_transaksi_kegiatan, int p_id_kelompok_luaran, int id_target_jenis_luaran
            )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_target_luaran
                            (@p_id_target_luaran, @p_id_luaran_dijanjikan, @p_id_transaksi_kegiatan, @p_id_kelompok_luaran, @id_target_jenis_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dt
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", p_id_luaran_dijanjikan)
            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            , new Npgsql.NpgsqlParameter("@id_target_jenis_luaran", id_target_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        
        public bool insupDokumenBuktiLuaran(Guid p_id_dokumen_bukti_luaran, int p_id_target_luaran, 
            int p_id_jenis_dokumen_bukti_luaran, string p_kd_sts_unggah
            )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.insup_dokumen_bukti_luaran
                            (@p_id_dokumen_bukti_luaran, @p_id_target_luaran, @p_id_jenis_dokumen_bukti_luaran, @p_kd_sts_unggah );";
            isSuccess = this._db.ExecuteNonQuery(strSQL
            , new Npgsql.NpgsqlParameter("@p_id_dokumen_bukti_luaran", p_id_dokumen_bukti_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_target_luaran", p_id_target_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_jenis_dokumen_bukti_luaran", p_id_jenis_dokumen_bukti_luaran)
            , new Npgsql.NpgsqlParameter("@p_kd_sts_unggah", p_kd_sts_unggah)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getRingkasan(ref DataTable dataTable, string p_id_transaksi_kegiatan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.get_ringkasan(@p_id_transaksi_kegiatan::uuid);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }



        public bool ListDokumenLuaranDicapai(ref DataTable dataTable, Guid p_id_transaksi_kegiatan, int p_id_kelompok_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_luaran_dicapai(@p_id_transaksi_kegiatan, @p_id_kelompok_luaran) t1 
                            order by t1.arr_kd_sts_unggah, t1.nama_target_jenis_luaran ;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getInfoCapaianLuaran(ref DataTable dataTable, string p_id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = string.Format( @"SELECT * FROM pelaksanaan.get_info_capaian_luaran('{0}'::uuid) t1 ", p_id_luaran_dijanjikan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool getInfoCapaianLuaran(ref DataTable dataTable, string p_id_luaran_dijanjikan, string pIdTransaksiKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_info_capaian_luaran('{0}'::uuid, '{1}'::uuid) t1 ", 
                p_id_luaran_dijanjikan, pIdTransaksiKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        
        public bool listIDDokumenDiunggah(ref DataTable dataTable, Guid p_id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_diunggah(@p_id_luaran_dijanjikan ) t1 ;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", p_id_luaran_dijanjikan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listIDDokumenDiunggah(ref DataTable dataTable, Guid p_id_luaran_dijanjikan, Guid p_id_transaksi_kegiatan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_dokumen_diunggah(@p_id_luaran_dijanjikan, @p_id_transaksi_kegiatan) t1 ;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", p_id_luaran_dijanjikan)
            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listTargetCapaianNDokumen(ref DataTable dataTable, Guid p_id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_target_capaian_n_dokumen(@p_id_luaran_dijanjikan ) t1 ;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", p_id_luaran_dijanjikan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listTargetCapaianNDokumen(ref DataTable dataTable, Guid p_id_luaran_dijanjikan, Guid p_id_transaksi_kegiatan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pelaksanaan.list_target_capaian_n_dokumen(@p_id_luaran_dijanjikan, @p_id_transaksi_kegiatan) t1 ;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", p_id_luaran_dijanjikan)
            , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion

    }
}