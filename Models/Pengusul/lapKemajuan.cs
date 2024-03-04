using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class lapKemajuan : _abstractModels
    {
        #region Variable Class
        
        #endregion

        #region Konstruktor dan Destruktor

        public lapKemajuan()
        {
            setInitValues();
        }

        ~lapKemajuan()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getDaftarLaporanKemajuan(ref DataTable dataTable, Guid idPersonal, string thnPelaksanaanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_laporan_kemajuan_by_personal('{0}','{1}') 
                                            WHERE kd_program_hibah IN ('1','6');",
                                            idPersonal, thnPelaksanaanKegiatan);
            
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getDaftarLaporanAkhir(ref DataTable dataTable, Guid idPersonal, string thnPelaksanaanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_laporan_akhir_by_personal('{0}','{1}') 
                                            WHERE kd_program_hibah IN ('1','2','6');",
                                            idPersonal, thnPelaksanaanKegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
       
        public bool insupRingkasan(Guid p_id_transaksi_kegiatan, string p_ringkasan)
        {
            string strSQL = @"SELECT * FROM pelaksanaan.insup_ringkasan(      
                              @p_id_transaksi_kegiatan::uuid,
                              @p_ringkasan::text
                              );";

            if (!this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_ringkasan", p_ringkasan)
                    ))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool insupKeyword(Guid p_id_transaksi_kegiatan, string p_keyword)
        {
            string strSQL = @"SELECT * FROM pelaksanaan.insup_keyword(      
                              @p_id_transaksi_kegiatan::uuid,
                              @p_keyword::text
                              );";

            if (!this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", p_id_transaksi_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_keyword", p_keyword)
                    ))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getDaftarLuaranWajib(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_luaran_laporan_kemajuan('{0}',{1});",
                                            idUsulanKegiatan, 1);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarLuaranTambahan(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_luaran_laporan_kemajuan('{0}',{1});",
                                            idUsulanKegiatan, 2);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateStsPelaksanaan(Guid id_transaksi_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.update_sts_pelaksanaan_transaksi_kegiatan(@id_transaksi_kegiatan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_transaksi_kegiatan", id_transaksi_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataUnggahDokumen(Guid id_transaksi_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insert_unggah_dokumen(@id_transaksi_kegiatan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_transaksi_kegiatan", id_transaksi_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool cekApakahSudahAdaTahapLaporanAkhir(Guid id_usulan_kegiatan)
        {
            bool ret = false;
            int jmlRow = 0;
            string strSQL = @"SELECT count(*)::int FROM hibah.get_laporan_akhir(@id_usulan_kegiatan);";

            if (!this._db.fetchDataSkalar(strSQL, ref jmlRow
                , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", id_usulan_kegiatan)))
            {

                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            if (jmlRow > 0)
                ret = true;

            return ret;
        }

        public bool generateTahapLaporanAkhir(Guid id_usulan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insert_tahap_laporan_akhir(@id_usulan_kegiatan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", id_usulan_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupDokumenMitra(Guid id_transaksi_kegiatan, string lokasiFile)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pelaksanaan.insup_dokumen_mitra(@p_id_transaksi_kegiatan, @p_lokasi_file);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_transaksi_kegiatan", id_transaksi_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_lokasi_file", lokasiFile)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDokumenMitra(ref DataTable dataTable, Guid idTransaksiKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_dokumen_mitra('{0}');",
                                            idTransaksiKegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getIdTransaksiLaporanKemajuan(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_id_transaksi_laporan_kemajuan('{0}'::uuid);",
                                            idUsulanKegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getIdTransaksiDimonev(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pelaksanaan.get_id_transaksi_kegiatan_dimonev('{0}'::uuid);",
                                            idUsulanKegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getnamaluaran(ref DataTable dataTable, string id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT id_jenis_luaran,t1.kd_kategori_jenis_luaran,
case when nama_kategori_jenis_luaran =nama_jenis_luaran then nama_jenis_luaran
else 
nama_kategori_jenis_luaran || ' - ' ||nama_jenis_luaran end::character varying as nama_jenis_luaran
FROM penelitian.jenis_luaran  t1 left join referensi.kategori_jenis_luaran t2 on t1.kd_kategori_jenis_luaran=t2.kd_kategori_jenis_luaran where id_jenis_luaran={0};",
                                            id_jenis_luaran);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarLuaranWajibLapAkhir(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_luaran_laporan_akhir('{0}',{1});",
                                            idUsulanKegiatan, 1);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getDaftarLuaranTambahanLapAkhir(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_luaran_laporan_akhir('{0}',{1});",
                                            idUsulanKegiatan, 2);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}