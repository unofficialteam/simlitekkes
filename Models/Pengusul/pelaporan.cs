using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class pelaporan : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public pelaporan()
        {
            setInitValues();
        }

        ~pelaporan()
        {

        }
        #endregion

        #region Properties

        #endregion

        #region Methods



        public bool getDaftarTanggungJawabBelanja(ref DataTable dataTable, Guid id_personal, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.list_tanggung_jawab_belanja_by_personal(@id_personal, @thn_pelaksanaan_kegiatan);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPengesahanTanggungJawabBelanja(ref DataTable dt, Guid idUsulanKegiatan,string kd_jenis_dokumen_penggunaan_anggaran_usulan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.get_tanggung_jawab_belanja(@p_id_usulan_kegiatan,@p_kd_jenis_dokumen_penggunaan_anggaran_usulan);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dt
                , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", idUsulanKegiatan)
                , new Npgsql.NpgsqlParameter("@p_kd_jenis_dokumen_penggunaan_anggaran_usulan", kd_jenis_dokumen_penggunaan_anggaran_usulan)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupdatauraian(
            Guid idUsulanKegiatan
            , string nomor_surat_keputusan
            , string nomor_kontrak
            , string anggaran
            , string uraian01, string jumlah01
            , string uraian02, string jumlah02
            , string uraian03, string jumlah03
            , string uraian04, string jumlah04
            , string uraian05, string jumlah05
            , string kd_jenis_dokumen_penggunaan_anggaran_usulan
            )
        {


            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insup_uraian_pelaksanaan(
                              @p_id_usulan_kegiatan::uuid,
                              @p_nomor_surat_keputusan::text,
                              @p_nomor_kontrak::text,
                              @p_anggaran::money,

                              @p_uraian01::text,@p_jumlah01::money,
                              @p_uraian02::text,@p_jumlah02::money,
                              @p_uraian03::text,@p_jumlah03::money,
                              @p_uraian04::text,@p_jumlah04::money,
                              @p_uraian05::text,@p_jumlah05::money,
                              @p_kd_jenis_dokumen_penggunaan_anggaran_usulan
                            );";


            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@p_nomor_surat_keputusan", nomor_surat_keputusan)
                    , new Npgsql.NpgsqlParameter("@p_nomor_kontrak", nomor_kontrak)
                    , new Npgsql.NpgsqlParameter("@p_anggaran", anggaran)

                    , new Npgsql.NpgsqlParameter("@p_uraian01", uraian01)
                    , new Npgsql.NpgsqlParameter("@p_jumlah01", jumlah01)
                    , new Npgsql.NpgsqlParameter("@p_uraian02", uraian02)
                    , new Npgsql.NpgsqlParameter("@p_jumlah02", jumlah02)
                    , new Npgsql.NpgsqlParameter("@p_uraian03", uraian03)
                    , new Npgsql.NpgsqlParameter("@p_jumlah03", jumlah03)
                    , new Npgsql.NpgsqlParameter("@p_uraian04", uraian04)
                    , new Npgsql.NpgsqlParameter("@p_jumlah04", jumlah04)
                    , new Npgsql.NpgsqlParameter("@p_uraian05", uraian05)
                    , new Npgsql.NpgsqlParameter("@p_jumlah05", jumlah05)
                    , new Npgsql.NpgsqlParameter("@p_kd_jenis_dokumen_penggunaan_anggaran_usulan", kd_jenis_dokumen_penggunaan_anggaran_usulan)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateStsPelaksanaan(Guid id_usulan_kegiatan,string kd_jenis_dokumen_penggunaan_anggaran_usulan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.update_sts_unggah_tanggung_jawab_belanja(@id_usulan_kegiatan,@kd_jenis_dokumen_penggunaan_anggaran_usulan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", id_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_jenis_dokumen_penggunaan_anggaran_usulan", kd_jenis_dokumen_penggunaan_anggaran_usulan)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        #endregion
    }

}