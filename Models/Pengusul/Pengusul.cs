using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Pengusul
{
    public class Pengusul : _abstractModels
    {

        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public Pengusul()
        {
            setInitValues();
        }

        ~Pengusul()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool listSyaratMitraAbdimas(ref DataTable dt, string iDUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"with rekap_jumlah AS(
	                            select
	                             t1.thn_uruan_kegiatan,
	                             unnest(t1.tipe_mitra) as tipe_mitra, unnest(t1.id_tipe_mitra) as id_tipe_mitra
	                            , unnest(t1.kd_sts_surat_pernyataan) as kd_sts_surat_pernyataan, unnest(t1.dukungan_pendanaan) as dukungan_pendanaan
	                            , t1.jml_min_mitra, unnest(t1.jml_min_bidang) as jml_min_bidang
	                             from hibah.list_syarat_mitra(:p_id_usulan_kegiatan::uuid) t1  
	                             group by t1.thn_uruan_kegiatan, t1.tipe_mitra,  t1.id_tipe_mitra, t1.kd_sts_surat_pernyataan, t1.dukungan_pendanaan 
	                             ,t1.jml_min_mitra, t1.jml_min_bidang
	                             order by t1.thn_uruan_kegiatan, id_tipe_mitra
                             )
                             select 
                             t1.thn_uruan_kegiatan,
                             t1.id_tipe_mitra,
                             sum(t1.jml_min_mitra) as jml_min_mitra , 
                             sum(t1.kd_sts_surat_pernyataan::int) as kd_sts_surat_pernyataan, 
                             sum(t1.dukungan_pendanaan) as dukungan_pendanaan, 
                             sum(t1.jml_min_bidang) as jml_min_bidang, 
                             t1.tipe_mitra 
                             from rekap_jumlah t1 
                             group by t1.thn_uruan_kegiatan, t1.id_tipe_mitra, 
                             t1.jml_min_mitra,
                             t1.kd_sts_surat_pernyataan, t1.dukungan_pendanaan,
                             t1.jml_min_bidang, t1.tipe_mitra
                             order by t1.thn_uruan_kegiatan;");

            isSuccess = this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter(":p_id_usulan_kegiatan", iDUsulanKegiatan)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listMitraAbdimasUnnest(ref DataTable dt, string iDUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"
                      select 
                      t1.id_mitra_abdimas, t1.kd_kategori_mitra, 
                     t1.kd_sts_surat_pernyataan,
                     t1.id_tipe_mitra,
                     coalesce(t1.dana_thn_1::int,0::int) as dana_thn_1,
                     coalesce(t1.dana_thn_2::int,0::int) as dana_thn_2,
                     coalesce(t1.dana_thn_3::int,0::int) as dana_thn_3,
                     t1.bidang_masalah_mitra, 
                     coalesce(t1.id_jenis_mitra,0) as id_jenis_mitra
                      from pengabdian.list_mitra_abdimas_rb 
                      (:p_id_usulan_kegiatan::uuid)  t1;");


            isSuccess = this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter(":p_id_usulan_kegiatan", iDUsulanKegiatan)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion


    }
}