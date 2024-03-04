using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.pengusul
{
    public class unggahBerkas : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public unggahBerkas()
        {
            setInitValues();
        }

        ~unggahBerkas()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool insupTransaksiUnggahProposal(Guid idUsulanKegiatan, Guid idTransaksiKegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insup_transaksi_unggah_proposal(:p_id_usulan_kegiatan,:p_id_transaksi_kegiatan);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":p_id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter(":p_id_transaksi_kegiatan", idTransaksiKegiatan)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupTransaksiUnggahProposalLanjutan(Guid idUsulanKegiatan, Guid idTransaksiKegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insup_transaksi_unggah_proposal_lanjutan(:p_id_usulan_kegiatan,:p_id_transaksi_kegiatan);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":p_id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter(":p_id_transaksi_kegiatan", idTransaksiKegiatan)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getPathDokumenTemplate(int idSkema, int tktTarget, ref string pathFile)
        {
            bool isSuccess = false;
            DataTable dt = new DataTable();
            string strSQL = @"SELECT file_path from referensi.get_file_path_dokumen_template(:p_id_skema,:p_tkt_target);";

            if (tktTarget <= 3) tktTarget = 3;
            else if (tktTarget <= 6) tktTarget = 6;
            else if (tktTarget > 6) tktTarget = 9;

            isSuccess = this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter(":p_id_skema", idSkema)
                    , new Npgsql.NpgsqlParameter(":p_tkt_target", tktTarget)
                    );
            if(isSuccess && dt.Rows.Count>0)
            {
                pathFile = dt.Rows[0]["file_path"].ToString();
            }
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getPathDokumenTemplateAbdimas(int idSkema, ref string pathFile)
        {
            bool isSuccess = false;
            DataTable dt = new DataTable();
            string strSQL = @"SELECT file_path from referensi.get_file_path_dokumen_template_abdimas(:p_id_skema);";


            isSuccess = this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter(":p_id_skema", idSkema)
                    );
            if (isSuccess && dt.Rows.Count > 0)
            {
                pathFile = dt.Rows[0]["file_path"].ToString();
            }
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getPathDokumenHasilAkreditasiTppTpm(ref DataTable dt, string idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = @"select t1.dokumen_akreditasi_tpp, t1.dokumen_akreditasi_tpm 
                                from penelitian.list_dokumen_akreditasi_tpp_tpm 
                                (:p_id_usulan_kegiatan::uuid) t1;";


            isSuccess = this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter(":p_id_usulan_kegiatan", idUsulanKegiatan)
                    );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupDokumenUsulanAkreditasiTppTpm(string idUsulanKegiatan, string idDokumen, string idJnsDokumen)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT referensi.insup_dokumen_akreditasi_tpp_tpm(:p_id_usulan_kegiatan::uuid,:p_id_dokumen::uuid,:p_id_jenis_dokumen::char(1));";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter(":p_id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter(":p_id_dokumen", idDokumen)
                    , new Npgsql.NpgsqlParameter(":p_id_jenis_dokumen", idJnsDokumen)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}