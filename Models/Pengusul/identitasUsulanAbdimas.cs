using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Pengusul
{
    public class identitasUsulanAbdimas : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public identitasUsulanAbdimas()
        {
            setInitValues();
        }

        ~identitasUsulanAbdimas()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

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

        public bool insertDataBaru
        (
                Guid p_id_usulan,
                Guid p_id_usulan_kegiatan,
                Guid p_id_institusi,
                int p_id_skema,
                int p_id_bidang_fokus,
                string p_judul,
                int p_lama_kegiatan,
                string p_thn_pertama_usulan,
                string p_thn_pelaksanaan_kegiatan,
                Guid p_id_personal_ketua,
                Guid? p_id_topik_unggulan_perguruan_tinggi,
                int p_jml_msh_terlibat
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.insup_identitas_usulan_pengabdian (
                            @p_id_usulan,
                            @p_id_usulan_kegiatan,
                            @p_id_institusi,
                            @p_id_skema,
                            @p_id_bidang_fokus,
                            @p_judul,
                            @p_lama_kegiatan::SMALLINT,
                            @p_thn_pertama_usulan::CHAR(4),
                            @p_thn_pelaksanaan_kegiatan::CHAR(4),
                            @p_id_personal_ketua,
                            @p_id_topik_unggulan_perguruan_tinggi,
                            @p_jml_msh_terlibat          
                        );";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan", p_id_usulan)
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", p_id_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_id_institusi", p_id_institusi)
                    , new Npgsql.NpgsqlParameter("@p_id_skema", p_id_skema)
                    , new Npgsql.NpgsqlParameter("@p_id_bidang_fokus", p_id_bidang_fokus)
                    , new Npgsql.NpgsqlParameter("@p_judul", p_judul)
                    , new Npgsql.NpgsqlParameter("@p_lama_kegiatan", p_lama_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_thn_pertama_usulan", p_thn_pertama_usulan)
                    , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", p_thn_pelaksanaan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_id_personal_ketua", p_id_personal_ketua)
                    , new Npgsql.NpgsqlParameter("@p_id_topik_unggulan_perguruan_tinggi",
                            (object)p_id_topik_unggulan_perguruan_tinggi ?? DBNull.Value)
                    , new Npgsql.NpgsqlParameter("@p_jml_msh_terlibat", p_jml_msh_terlibat)
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

        public bool getSkemaKegiatanAbdimas(ref DataTable dataTable, string kdProgramHibah, string thnPelaksanaan, string idPersonal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM referensi.list_skema_kegiatan_pengabdian_by_klaster_personal('{0}','{1}','{2}');", kdProgramHibah, thnPelaksanaan, idPersonal);
            //var query = $@"SELECT * FROM hibah.list_skema_kegiatan_pengabdian_by_klaster_personal('{kdProgramHibah}');";
            //$@"SELECT * FROM referensi.skema_kegiatan sk
            //         WHERE sk.kd_program_hibah = '{kdProgramHibah}'
            //         AND sk.kd_sts_aktif = '1'
            //     ORDER BY nama_skema;";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getCekEligibilitasKetua(ref DataTable dataTable, string id_personal, int id_skema)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.get_eligibilitas_ketua('{0}',{1});", id_personal, id_skema);

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

            var query = $@"SELECT * FROM hibah.get_identitas_usulan_pengabdian('{idUsulan}');";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getBidangFokus(ref DataTable dataTable)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM referensi.bidang_fokus 
                           WHERE kd_sts_aktif='1' 
                           AND id_bidang_fokus <= 13 
                           ORDER BY id_bidang_fokus;";

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

        public bool getDataUsulanKegiatan(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;

            var query = String.Format(@"SELECT * FROM hibah.get_usulan_kegiatan('{0}')", idUsulanKegiatan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getBidangUnggulanPengabdianPT(ref DataTable dataTable, Guid idInstitusi)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM pengabdian.list_bidang_unggulan_perguruan_tinggi('{idInstitusi}')
                           WHERE kode_status_aktif = '1' ORDER BY bidang_unggulan_perguruan_tinggi;";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getTopikUnggulanPengabdianPT(ref DataTable dataTable, Guid idBidangUnggulanPT)
        {
            bool isSuccess = false;

            var query = $@"SELECT * FROM pengabdian.list_topik_unggulan_perguruan_tinggi('{idBidangUnggulanPT}')
                           WHERE kode_status_aktif = '1' ORDER BY topik_unggulan_perguruan_tinggi;";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getCekDidanaiSkemaPkm(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.cek_jml_didanai_skema_pkm('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getCekDidanaiSkemaPkms(ref DataTable dataTable, string id_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.cek_jml_didanai_skema_pkms('{0}');", id_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion

        #region Private Function

        #endregion


    }
}
