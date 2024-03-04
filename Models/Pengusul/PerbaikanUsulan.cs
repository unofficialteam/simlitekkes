using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class PerbaikanUsulan : _abstractModels
    {
        #region Variable Class
        
        #endregion

        #region Konstruktor dan Destruktor

        public PerbaikanUsulan()
        {
            setInitValues();
        }

        ~PerbaikanUsulan()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getListMakroRiset(ref DataTable dataTable)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM referensi.list_makro_riset() WHERE kd_sts_aktif = '1';";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateMakroRiset(Guid p_id_usulan, int idMakroRiset)
        {
            bool isSuccess = false;
          
            string strSQL = @"SELECT hibah.update_makro_riset(@p_id_usulan, @p_id_makro_riset)";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter("@p_id_usulan", p_id_usulan)
                            , new Npgsql.NpgsqlParameter("@p_id_makro_riset", idMakroRiset)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getMakroRiset(ref DataTable dataTable, string idUsulan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.get_makro_riset('{0}');", idUsulan);
            
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getKomentar(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.get_komentar_tahap_akhir('{0}');", idUsulanKegiatan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertDataUnggahDokumen(Guid idTransaksiKegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insert_unggah_dokumen(@id_transaksi_kegiatan);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_transaksi_kegiatan", idTransaksiKegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getidTransaksi(ref string id_transaksi, Guid id_usulan_kegiatan, string kd_tahapan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.get_id_transaksi(@p_id_usulan_kegiatan, @p_kd_tahapan_kegiatan);";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref id_transaksi
                , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", id_usulan_kegiatan)
                , new Npgsql.NpgsqlParameter("@p_kd_tahapan_kegiatan", kd_tahapan_kegiatan)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool GetLuaranWajib(ref DataTable dataTable, Guid pIdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from hibah.pdf_luaran_wajib_dan_target_capaian_no_8_perbaikan('{0}');", pIdUsulanKegiatan.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetLuaranTambahan(ref DataTable dataTable, Guid pIdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from hibah.pdf_luaran_tambahan_dan_target_capaian_no_8_perbaikan('{0}');", pIdUsulanKegiatan.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool cekjadwal(ref DataTable dataTable, string idSkema, string kdtahapan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM public.list_konfig_pengelolaan_kegiatan_aktif_xi() where kd_tahapan_kegiatan  = '{0}' and id_skema={1};"
                            , kdtahapan, idSkema);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarWhitelist(Guid id_personal, int id_skema, string kd_tahapan_kegiatan, string thn_usulan, string thn_pelaksanaan)
        {
            bool ada = false;
            string strSQL;

            //strSQL = string.Format(@"SELECT * FROM referensi.whitelist_usulan_personal wup
            //                            JOIN hibah.transaksi_kegiatan tk ON wup.id_usulan_kegiatan = tk.id_usulan_kegiatan
            //                            JOIN referensi.tahapan_kegiatan_skema tks ON tk.id_tahapan_kegiatan_skema = tks.id_tahapan_kegiatan_skema
            //                        WHERE wup.id_personal = '{0}' AND wup.id_skema = {1} AND tks.kd_tahapan_kegiatan = '{2}'
            //                        AND wup.thn_usulan = '{3}' AND wup.thn_pelaksanaan = '{4}' AND wup.kd_sts_aktif = '1';",
            //                        id_personal, id_skema, kd_tahapan_kegiatan, thn_usulan, thn_pelaksanaan);

            strSQL = string.Format(@"SELECT * FROM referensi.list_whitelist_usulan_personal({0}, '{1}', '{2}', '{3}', '{4}');",
                                    id_skema, thn_usulan, thn_pelaksanaan, kd_tahapan_kegiatan, id_personal);

            DataTable dt = new DataTable();
            ada = this._db.FetchDataTable(strSQL, ref dt);
            if (!ada)
                this._errorMessage = this._db.ErrorMessage;
            {
                if (dt.Rows.Count > 0)
                {
                    //id_personal = Guid.Parse(dt.Rows[0]["id_personal"].ToString());
                    ada = true;
                }
                else
                {
                    ada = false;
                }
            }

            return ada;
        }

        public bool getDeskripsiMakroRiset(ref DataTable dataTable, int idMakroRiset)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.get_deskripsi_makro_riset({0});", idMakroRiset);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion
    }
}