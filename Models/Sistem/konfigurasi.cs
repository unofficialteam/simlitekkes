using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace simlitekkes.Models.Sistem
{
    [Serializable]
    public class konfigurasi : _abstractModels
    {
        #region Fields

        private Guid _idPersonal;
        private Guid _idInstitusi;

        private DataTable _dtTahapanKegiatanAktif;
        private DataTable _dtEligibilitasInstitusi;
        private DataTable _dtEligibilitasKetua;
        private DataTable _dtStatusBatasanPengusul;
        private DataTable _dtSkemaDiusulkan;
        private DataTable _dtPenugasanPengusulan;

        //private string _errorMessage;

        #endregion

        #region Konstruktor dan Destruktor

        public konfigurasi()
        {
            //if (getKonfigPengelolaanKegiatanAktif())
            this._errorMessage = "";
        }

        public konfigurasi(Guid idPersonal, Guid idInstitusi, string kdProgramHibah = "1")
        {
            if (getKonfigPengelolaanKegiatanAktif())
            {
                this._idPersonal = idPersonal;
                if (getKonfigEligibilitasKetua())
                {
                    this._idInstitusi = idInstitusi;
                    if (getKonfigEligibleInstitusi(kdProgramHibah))
                        this._errorMessage = "";
                }
            }
        }

        ~konfigurasi()
        {

        }

        #endregion

        #region Properties

        //public int idSkema
        //{
        //    get { return this._idSkema; }
        //}

        //public DataTable tahapanKegiatanAktif
        //{
        //    get { return this._dtTahapanKegiatanAktif; }
        //}

        //public string errorMessage
        //{
        //    get { return this._errorMessage; }
        //}

        //public int lamaKegiatanMin
        //{
        //    get { return this._lamaKegiatanMin; }
        //}

        //public int lamaKegiatanMaks
        //{
        //    get { return this._lamaKegiatanMaks; }
        //}

        //public string lamaKegiatanSatuan
        //{
        //    get { return this._lamaKegiatanSatuan; }
        //}

        //public int kdJenjangPendKetuaMin
        //{
        //    get { return this._kdJenjangPendKetuaMin; }
        //}

        //public int kdJenjangPendKetuaMaks
        //{
        //    get { return this._kdJenjangPendKetuaMaks; }
        //}

        //public string jenjangPendKetuaMin
        //{
        //    get { return this._jenjangPendKetuaMin; }
        //}

        //public string jenjangPendKetuaMaks
        //{
        //    get { return this._jenjangPendKetuaMaks; }
        //}

        //public string jenjangPendKetuaSatuan
        //{
        //    get { return this._jenjangPendidikanSatuan; }
        //}

        //public int jmlAnggotaMin
        //{
        //    get { return this._jmlAnggotaMin; }
        //}

        //public int jmlAnggotaMaks
        //{
        //    get { return this._jmlAnggotaMaks; }
        //}

        //public string jmlAnggotaSatuan
        //{
        //    get { return this._jmlAnggotaSatuan; }
        //}

        //public decimal jmlDanaMin
        //{
        //    get { return this._jmlDanaMin; }
        //}

        //public decimal jmlDanaMaks
        //{
        //    get { return this._jmlDanaMaks; }
        //}

        //public string jmlDanaSatuan
        //{
        //    get { return this._jmlDanaSatuan; }
        //}

        #endregion

        #region Methods

        public DataTable listEligibilitasSkema(string kdProgramHibah, string thnUsulan, string thnPelaksanaan)
        {
            getStatusBatasanPengusul(thnPelaksanaan, kdProgramHibah);
            getUsulan(thnPelaksanaan, kdProgramHibah);
            getPenugasanPengusulan(thnUsulan, thnPelaksanaan);

            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("id_skema", typeof(int)));
            dtResult.Columns.Add(new DataColumn("nama_skema", typeof(string)));
            dtResult.Columns.Add(new DataColumn("kd_sts_eligible", typeof(int)));
            dtResult.Columns.Add(new DataColumn("keterangan", typeof(string)));

            var skemaInstitusi = from t1 in this._dtEligibilitasInstitusi.AsEnumerable()
                                 where t1.Field<string>("kd_program_hibah") == kdProgramHibah
                                 select new
                                 {
                                     idSkema = t1.Field<int>("id_skema"),
                                     namaSkema = t1.Field<string>("nama_skema")
                                 };

            var tahapanAktif = from t1 in this._dtTahapanKegiatanAktif.AsEnumerable()
                               where t1.Field<string>("kd_tahapan_kegiatan") == "11"
                                  && t1.Field<string>("thn_usulan") == thnUsulan
                                  && t1.Field<string>("thn_pelaksanaan") == thnPelaksanaan
                               select new
                               {
                                   idSkema = t1.Field<int>("id_skema")
                               };

            List<int> lstTahapanAktif = new List<int>();
            foreach (var thp in tahapanAktif)
            {
                lstTahapanAktif.Add(thp.idSkema);
            }

            var eligibilitasKetua = from t1 in this._dtEligibilitasKetua.AsEnumerable()
                                    where t1.Field<string>("kd_sts_eligibilitas") == "1"
                                    select new
                                    {
                                        idSkema = t1.Field<int>("id_skema")
                                    };

            List<int> lstEligibilitasKetua = new List<int>();
            foreach (var eliKetua in eligibilitasKetua)
            {
                lstEligibilitasKetua.Add(eliKetua.idSkema);
            }

            List<int> lstSkemaDiusulkan = new List<int>();
            foreach (DataRow drSkema in this._dtSkemaDiusulkan.Rows)
            {
                lstSkemaDiusulkan.Add(int.Parse(drSkema["id_skema"].ToString()));
            }

            List<int> lstPenugasanPengusulan = new List<int>();
            foreach (DataRow drPenugasan in this._dtPenugasanPengusulan.Rows)
            {
                if (drPenugasan["sts_batas_penugasan"].ToString() == "0")
                    lstPenugasanPengusulan.Add(int.Parse(drPenugasan["id_skema"].ToString()));
            }

            DataRow dr = dtResult.NewRow();
            foreach (var res in skemaInstitusi)
            {
                dr["id_skema"] = res.idSkema;
                dr["nama_skema"] = res.namaSkema;

                // check tahapan pengajuan usulan
                if (lstTahapanAktif.Contains(res.idSkema))
                {
                    dr["kd_sts_eligible"] = 1;
                    dr["keterangan"] = "";

                    // check eligibilitas ketua
                    if (!lstEligibilitasKetua.Contains(res.idSkema))
                    {
                        dr["kd_sts_eligible"] = 0;
                        dr["keterangan"] = "Pengusul tdk eligible untuk skema ini";
                    }
                    else
                    {
                        // Check skema tidak yang termasuk ditugaskan maka cek sesuai aturan umum
                        if (!lstPenugasanPengusulan.Contains(res.idSkema))
                        {
                            // check batasan jumlah usulan sebagai ketua
                            if (this._dtStatusBatasanPengusul.Rows[0]["kd_sts_batas_jml_ketua"].ToString() == "1")
                            {
                                dr["kd_sts_eligible"] = 0;
                                dr["keterangan"] = "Batasan sebagai ketua telah terpenuhi";
                            }
                            else
                            {
                                // check batasan jumlah usulan
                                if (this._dtStatusBatasanPengusul.Rows[0]["kd_sts_batas_jml_usulan"].ToString() == "1")
                                {
                                    dr["kd_sts_eligible"] = 0;
                                    dr["keterangan"] = "Batasan jumlah usulan telah terpenuhi";
                                }
                                else
                                {
                                    // check skema desentralisasi yang telah diusulkan
                                    // bila ada cek pengusul termasuk dalam pt yang allow skema sama atau tidak
                                    if (lstSkemaDiusulkan.Contains(res.idSkema))
                                    {
                                        if (this._dtStatusBatasanPengusul.Rows[0]["allow_skema_des_sama"].ToString() == "0")
                                        {
                                            dr["kd_sts_eligible"] = 0;
                                            dr["keterangan"] = "Pengusul telah mengajukan usulan skema ini";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    dr["kd_sts_eligible"] = 0;
                    dr["keterangan"] = "Bukan dalam masa pengajuan proposal";
                }

                dtResult.Rows.Add(dr);
                dr = dtResult.NewRow();
            }

            return dtResult;
        }

        public DataTable getTahunUsulan()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("thn_usulan", typeof(string)));
            string strSQL = @"SELECT * FROM public.list_konfig_tahun_usulan();";

            if (!this._db.FetchDataTable(strSQL, ref dtResult))
                this._errorMessage = this._db.ErrorMessage;

            return dtResult;
        }

        public DataTable getTahunPelaksanaan(string thnUsulan)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("thn_pelaksanaan", typeof(string)));
            string strSQL = @"SELECT * FROM public.list_konfig_tahun_pelaksanaan(:thn_usulan);";

            if (!this._db.FetchDataTable(strSQL, ref dtResult
                , new Npgsql.NpgsqlParameter(":thn_usulan", thnUsulan)))
                this._errorMessage = this._db.ErrorMessage;

            return dtResult;
        }

        public DataTable getStsEligibleSbgAnggota(string nidn, string idUsulanKegiatan)
        {
            DataTable dtResult = new DataTable();
            string strSQL = @"SELECT * FROM public.is_syarat_keanggotaan_eligible(:nidn::character(10), :id_usulan_kegiatan::uuid);";

            if (!this._db.FetchDataTable(strSQL, ref dtResult
                , new Npgsql.NpgsqlParameter(":nidn", nidn)
                , new Npgsql.NpgsqlParameter(":id_usulan_kegiatan", idUsulanKegiatan)))
            {
                this._errorMessage = this._db.ErrorMessage;
            }

            return dtResult;
        }

        public void getSyaratCalonKeanggotaanSetaraKetua(ref string memenuhi, string idUsulanKegiatan)
        {
            string strSQL = @"SELECT * FROM public.is_terpenuhi_calon_keanggotaan_setara_ketua(:id_usulan_kegiatan::uuid);";

            if (!this._db.fetchDataSkalar(strSQL, ref memenuhi
                , new Npgsql.NpgsqlParameter(":id_usulan_kegiatan", idUsulanKegiatan)))
            {
                this._errorMessage = this._db.ErrorMessage;
                memenuhi = "0";
            }
        }

        #endregion

        #region Private Function

        private bool getKonfigPengelolaanKegiatanAktif()
        {
            bool isSuccess = true;

            string strSQL = @"SELECT * FROM list_konfig_pengelolaan_kegiatan_aktif();";

            this._dtTahapanKegiatanAktif = new DataTable();
            if (!this._db.FetchDataTable(strSQL, ref this._dtTahapanKegiatanAktif))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return isSuccess;
        }

        private bool getKonfigEligibilitasKetua()
        {
            bool isSuccess = true;

            string strSQL = @"SELECT * FROM list_konfig_eligible_skema_by_person(:id_personal);";

            this._dtEligibilitasKetua = new DataTable();
            if (!this._db.FetchDataTable(strSQL, ref this._dtEligibilitasKetua
                , new Npgsql.NpgsqlParameter(":id_personal", this._idPersonal)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return isSuccess;
        }

        private bool getKonfigEligibleInstitusi(string kdProgramHibah = "1")
        {
            bool isSuccess = true;

            string strSQL = @"SELECT * FROM list_konfig_eligible_skema_by_institusi(:id_institusi, :kd_program_hibah);";

            this._dtEligibilitasInstitusi = new DataTable();
            if (!this._db.FetchDataTable(strSQL, ref this._dtEligibilitasInstitusi
                    , new Npgsql.NpgsqlParameter(":id_institusi", this._idInstitusi)
                    , new Npgsql.NpgsqlParameter(":kd_program_hibah", kdProgramHibah)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return isSuccess;
        }

        private bool getStatusBatasanPengusul(string thnPelaksanaan, string kdProgramHibah)
        {
            bool isSuccess = true;

            string strSQL = @"SELECT * FROM public.status_batas_usulan(:id_personal, :thn_pelaksanaan_kegiatan, :kd_program_hibah);";

            this._dtStatusBatasanPengusul = new DataTable();
            if (!this._db.FetchDataTable(strSQL, ref this._dtStatusBatasanPengusul
                , new Npgsql.NpgsqlParameter(":id_personal", this._idPersonal)
                , new Npgsql.NpgsqlParameter(":thn_pelaksanaan_kegiatan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter(":kd_program_hibah", kdProgramHibah)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return isSuccess;
        }

        private bool getUsulan(string thnPelaksanaan, string kdProgramHibah)
        {
            bool isSuccess = true;

            string strSQL = @"SELECT t1.id_skema
                            FROM (
                            SELECT ta.id_skema  FROM hibah.list_usulan_personal(:id_personal, :thn_usulan1, :thn_pelaksanaan_kegiatan) ta
                            UNION
                            SELECT tb.id_skema  FROM hibah.list_usulan_personal(:id_personal, :thn_usulan2, :thn_pelaksanaan_kegiatan) tb
                            ) t1
                            INNER JOIN referensi.skema_kegiatan t2 ON t1.id_skema = t2.id_skema
	                            AND t2.kd_program_hibah = :kd_program_hibah
                            ;";

            this._dtSkemaDiusulkan = new DataTable();
            if (!this._db.FetchDataTable(strSQL, ref this._dtSkemaDiusulkan
                , new Npgsql.NpgsqlParameter(":id_personal", this._idPersonal)
                , new Npgsql.NpgsqlParameter(":thn_usulan1", (int.Parse(thnPelaksanaan) - 1).ToString())
                , new Npgsql.NpgsqlParameter(":thn_usulan2", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter(":thn_pelaksanaan_kegiatan", thnPelaksanaan)
                , new Npgsql.NpgsqlParameter(":kd_program_hibah", kdProgramHibah)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return isSuccess;
        }

        private bool getPenugasanPengusulan(string thnUsulan, string thnPelaksanaan)
        {
            bool isSuccess = true;

            string strSQL = @"SELECT id_skema, jml_penugasan_usulan, jml_sbg_ketua, sts_batas_penugasan FROM hibah.list_penugasan_pengusulan(:id_personal, :thn_usulan, :thn_pelaksanaan);";

            this._dtPenugasanPengusulan = new DataTable();
            if (!this._db.FetchDataTable(strSQL, ref this._dtPenugasanPengusulan
                , new Npgsql.NpgsqlParameter(":id_personal", this._idPersonal)
                , new Npgsql.NpgsqlParameter(":thn_usulan", thnUsulan)
                , new Npgsql.NpgsqlParameter(":thn_pelaksanaan", thnPelaksanaan)
                ))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return isSuccess;
        }

        #endregion
    }
}