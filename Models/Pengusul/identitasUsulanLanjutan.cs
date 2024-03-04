using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Pengusul
{
    public class identitasUsulanLanjutan : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public identitasUsulanLanjutan()
        {
            setInitValues();
        }

        ~identitasUsulanLanjutan()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getCekEligibilitasKetua(ref DataTable dataTable, string id_personal, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.get_eligibilitas_ketua('{0}',{1});", id_personal, id_skema);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekDidanaiSkemaPdp(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.cek_jml_didanai_skema_pdp('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekKuotaUsulan(ref DataTable dataTable, string id_personal, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.cek_jml_usulan_persyaratan_umum('{0}','{1}');",
                id_personal, thn_pelaksanaan_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekKuotaUsulanEdit(ref DataTable dataTable, string id_personal, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.cek_jml_usulan_persyaratan_umum_edit('{0}','{1}');",
                id_personal, thn_pelaksanaan_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getIdentitasUsulan(ref DataTable dataTable, string idPersonal, string thnUsulan,
            string thnPelaksanaan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_usulan_baru_by_personal ('{0}','{1}','{2}',0,0);",
                idPersonal, thnUsulan, thnPelaksanaan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getIdentitasUsulanLanjutan(ref DataTable dataTable, Guid IdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.get_identias_usulan_lanjutan_penelitian('{0}');", IdUsulanKegiatan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listRiwayatUsulan(ref DataTable dataTable, string idPersonal, int offset)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_riwayat_usulan_personal2 ('{0}',{1},{2});",
                idPersonal, "4", offset.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getIdentitasUsulanDidanaiBlmLengkap(ref DataTable dataTable, string idPersonal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.list_usulan_didanai_data_blm_lengkap('{0}');",
                idPersonal);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getJmlRecords(string idUsulanKegiatan)
        {
            bool isSuccess = false;
            return isSuccess;
        }

        //public bool getCurrRecords()
        //{
        //    bool isSuccess = false;

        //    string strSQL = @"SELECT * FROM referensi.list_kelompok_kegiatan();";

        //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.errorMessage;

        //    return isSuccess;
        //}

        public bool updateIdentitasLanjutanDesentralisasi
        (
            Guid p_id_usulan_kegiatan,
            int p_level_tkt_target,
            int p_level_tkt,
            Guid p_id_topik_unggulan_perguruan_tinggi
        )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.update_identitas_usulan_lanjutan_penelitian_desentralisasi(
                            @p_id_usulan_kegiatan,
                            @p_level_tkt_target,
                            @p_level_tkt::SMALLINT,
                            @p_id_topik_unggulan_perguruan_tinggi
                        );";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", p_id_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_level_tkt_target", p_level_tkt_target)
                    , new Npgsql.NpgsqlParameter("@p_level_tkt", p_level_tkt)
                    , new Npgsql.NpgsqlParameter("@p_id_topik_unggulan_perguruan_tinggi", p_id_topik_unggulan_perguruan_tinggi)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateIdentitasLanjutanKompetitifNasional
        (
            Guid p_id_usulan_kegiatan,
            int p_level_tkt_target,
            int p_level_tkt,
            Guid p_id_topik_rirn
        )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.update_identitas_usulan_lanjutan_penelitian_kompetitif_nasional(
                            @p_id_usulan_kegiatan,
                            @p_level_tkt_target,
                            @p_level_tkt::SMALLINT,
                            @p_id_topik_rirn
                        );";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", p_id_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_level_tkt_target", p_level_tkt_target)
                    , new Npgsql.NpgsqlParameter("@p_level_tkt", p_level_tkt)
                    , new Npgsql.NpgsqlParameter("@p_id_topik_rirn", p_id_topik_rirn)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertDataLanjutanPenelitianRb (ref string idUsulanKegiatan, Guid idPersonal,
            Guid idUsulan, int urutanTahun, string thnUsulan, string thnPelaksanaan, int idSkema)
        {
            bool isSuccess = false;
            //          p_id_personal uuid,
            //p_id_usulan uuid,
            //p_urutan_thn_kegiatan smallint,
            //p_thn_usulan_kegiatan char,
            //p_thn_pelaksanaan_kegiatan char,
            //p_id_skema integer = 0
            string strSQL = @"SELECT * FROM hibah.insert_usulan_lanjutan (
                              :p_id_personal::uuid,
                              :p_id_usulan::uuid,
                              :p_urutan_thn_kegiatan::smallint,
                              :p_thn_usulan_kegiatan::char(4),
                              :p_thn_pelaksanaan_kegiatan::char(4),
                              :p_id_skema::integer
                        );";

            isSuccess = this._db.ReadSkalar(strSQL, ref idUsulanKegiatan
                    , new Npgsql.NpgsqlParameter(":p_id_personal", idPersonal)
                    , new Npgsql.NpgsqlParameter(":p_id_usulan", idUsulan)

                    , new Npgsql.NpgsqlParameter(":p_urutan_thn_kegiatan", urutanTahun)
                    , new Npgsql.NpgsqlParameter(":p_thn_usulan_kegiatan", thnUsulan)
                    , new Npgsql.NpgsqlParameter(":p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
                    , new Npgsql.NpgsqlParameter(":p_id_skema", idSkema)

                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool insertDataLanjutanPenelitian
        (
                Guid p_id_usulan,
                Guid p_id_usulan_kegiatan,
                Guid p_id_institusi,
                int p_id_skema,
                string p_id_bidang_fokus,
                string p_judul,
                string p_lama_kegiatan,
                string p_thn_pertama_usulan,
                string p_id_kategori_sbk,
                string p_abstrak,
                string p_keyword,
                string p_thn_pelaksanaan_kegiatan,
                string p_id_personal_ketua
        )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.insup_usulan_lanjutan_penelitian (
                          :p_id_usulan::uuid,
                          :p_id_usulan_kegiatan::uuid,
                          :p_id_institusi::uuid,
                          :p_id_skema::integer,
                          :p_id_bidang_fokus::integer,
                          :p_judul::text,
                          :p_lama_kegiatan::smallint,
                          :p_thn_pertama_usulan::char(4),
                          :p_id_kategori_sbk::integer,
                          :p_abstrak::text,
                          :p_keyword::text,
                          :p_thn_pelaksanaan_kegiatan::char(4),
                          :p_id_personal_ketua::uuid
                        );";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":p_id_usulan", p_id_usulan)
                    , new Npgsql.NpgsqlParameter(":p_id_usulan_kegiatan", p_id_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter(":p_id_institusi", p_id_institusi)
                    , new Npgsql.NpgsqlParameter(":p_id_skema", p_id_skema)
                    , new Npgsql.NpgsqlParameter(":p_id_bidang_fokus", p_id_bidang_fokus)

                    , new Npgsql.NpgsqlParameter(":p_judul", p_judul)
                    , new Npgsql.NpgsqlParameter(":p_lama_kegiatan", p_lama_kegiatan)
                    , new Npgsql.NpgsqlParameter(":p_thn_pertama_usulan", p_thn_pertama_usulan)
                    , new Npgsql.NpgsqlParameter(":p_id_kategori_sbk", p_id_kategori_sbk)

                    , new Npgsql.NpgsqlParameter(":p_abstrak", p_abstrak)
                    , new Npgsql.NpgsqlParameter(":p_keyword", p_keyword)
                    , new Npgsql.NpgsqlParameter(":p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)
                    , new Npgsql.NpgsqlParameter(":p_id_personal_ketua", p_id_personal_ketua)

                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteData(string idUsulan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.del_usulan(:p_id_usulan::uuid);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":p_id_usulan", idUsulan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getKekuranganKelengkapanIdentitas(string pIdUsulanKegiatan, ref string kekurangan)
        {
            bool isSuccess = false;

            //string strSQL = string.Format(@"SELECT hibah.get_kekurangan_kelengkapan_usulan ('{0}')", pIdUsulanKegiatan);

            //isSuccess = this._db.fetchDataSkalar(strSQL, ref kekurangan);
            //if (!isSuccess)
            //    this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getKekhususanUsulan(string idPersonal, int idSkema, string thnUsulan, string thnPelaksanaan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * from hibah.get_kekhususan_usulan ('{0}',{1},'{2}','{3}');"
                        , idPersonal, idSkema, thnUsulan, thnPelaksanaan);
            DataTable dt = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dt);
            if (dt.Rows.Count > 0)
                isSuccess = true;
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getKategoriRiset(ref DataTable dataTable, int id_skema, int id_bidang_fokus)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.list_kategori_riset_by_skema_bf({0},{1});",
                id_skema, id_bidang_fokus);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getPlafonDanaSbkThnImplementasi2016(ref DataTable dataTable, int id_kategori_sbk, int id_skema, int id_bidang_fokus)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.get_plafon_sbk_utama_thn_implementasi_2016({0},{1},{2});",
                id_kategori_sbk, id_skema, id_bidang_fokus);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getPlafonDanaSbkThnImplementasi2017(ref DataTable dataTable, int id_kategori_sbk, int id_skema, int id_bidang_fokus)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.get_plafon_sbk_utama_thn_implementasi_2017({0},{1},{2});",
                id_kategori_sbk, id_skema, id_bidang_fokus);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getPlafonDanaSbkThnImplementasi2018(ref DataTable dataTable, int id_kategori_sbk, int id_skema, int id_bidang_fokus)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.get_plafon_sbk_utama_thn_implementasi_2018({0},{1},{2});",
                id_kategori_sbk, id_skema, id_bidang_fokus);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getKategoriRisetThnImplementasi2016(ref DataTable dataTable, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.list_kategori_riset_thn_implementasi_2016({0});", id_skema);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getKategoriRisetThnImplementasi2017(ref DataTable dataTable, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.list_kategori_riset_thn_implementasi_2017({0});", id_skema);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getBidangFokusThnImplementasi2016(ref DataTable dataTable, int id_skema, int id_kategori_sbk)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.list_bidang_fokus_by_kategori_sbk_thn_implementasi_2016({0},{1});", id_skema, id_kategori_sbk);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getBidangFokusThnImplementasi2017(ref DataTable dataTable, int id_skema, int id_kategori_sbk)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.list_bidang_fokus_by_kategori_sbk_thn_implementasi_2017({0},{1});", id_skema, id_kategori_sbk);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getUsulanKegiatan(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM hibah.usulan_kegiatan WHERE id_usulan_kegiatan = '{idUsulanKegiatan}';";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getIdentitasUsulan(ref DataTable dataTable, Guid idUsulan)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM hibah.get_identitas_usulan('{idUsulan}');";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getLevelTktSaatIni(ref DataTable dataTable, int idSkema, int idKategoriSbk)
        {
            bool isSuccess = false;
            
            string strSQL = string.Format("SELECT * FROM referensi.list_level_tkt_by_skema_kegiatan({0},{1});", idSkema, idKategoriSbk);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getTemaPenelitian(ref DataTable dataTable, int idBidangFokus)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM referensi.list_tema_rirn_by_bidang_fokus({idBidangFokus});";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getTopikPenelitian(ref DataTable dataTable, Guid idTema)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM referensi.list_topik_rirn_by_tema('{idTema}');";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDataSkemaKegiatan(ref DataTable dataTable, int idSkemaKegiatan)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM referensi.skema_kegiatan WHERE id_skema = {idSkemaKegiatan};";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getBidangUnggulanPenelitianPT(ref DataTable dataTable, Guid idInstitusi)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM penelitian.list_bidang_unggulan_perguruan_tinggi('{idInstitusi}')
                           WHERE kode_status_aktif = '1' ORDER BY bidang_unggulan_perguruan_tinggi;";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getTopikUnggulanPenelitianPT(ref DataTable dataTable, Guid idBidangUnggulanPT)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM penelitian.list_topik_unggulan_perguruan_tinggi('{idBidangUnggulanPT}')
                           WHERE kode_status_aktif = '1' ORDER BY topik_unggulan_perguruan_tinggi;";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDataDataUsulanKegiatan(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;

            var query = String.Format(@"SELECT * FROM hibah.get_usulan_kegiatan('{0}')", idUsulanKegiatan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDetailUsulanKegiatan(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;

            var query = String.Format(@"SELECT * FROM hibah.get_usulan_kegiatan('{0}')", idUsulanKegiatan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateJudulUsulanKegiatan(Guid idUsulan, string judul)
        {
            bool isSuccess = false;

            string strSQL = @"UPDATE hibah.usulan SET judul = @judul WHERE id_usulan = @id_usulan;";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_usulan", idUsulan)
                    , new Npgsql.NpgsqlParameter("@judul", judul));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool kirimUsulan(Guid idUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.insup_kirim_usulan_baru(:p_id_usulan_kegiatan::uuid);";

           isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":p_id_usulan_kegiatan", idUsulanKegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getStatusUsulanDikirim(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;

            var query = String.Format(@"SELECT * FROM hibah.get_status_kirim_usulan('{0}')", idUsulanKegiatan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool hapusTransaksiKirimUsulan(Guid idUsulanKegiatan )
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.del_transaksi_approval(@p_id_usulan_kegiatan);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", idUsulanKegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion

        #region Private Function

        #endregion


    }
}
