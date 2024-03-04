using System;
using System.Data;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class plottingReviewer : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public plottingReviewer()
        {
            setInitValues();
        }

        ~plottingReviewer()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods


        public bool getListSkema(ref DataTable dataTable, Guid idPersonal, string TahunUsulan, string TahunPelaksanaan, string KdTahapanKegiatan)
        {
            var query = $"SELECT * from hibah.list_rekap_skema_evaluasi_opt_ppsdm('{idPersonal}','{TahunUsulan}','{TahunPelaksanaan}', '{KdTahapanKegiatan}');";
            dataTable = new DataTable();

            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListTahapanEvaluasi(ref DataTable dataTable, string idSkema
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_tahapan_evaluasi({0});", idSkema);
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

        public bool getDaftarWhitelist(Guid id_institusi, int id_skema, string kd_tahapan_kegiatan, string thn_usulan, string thn_pelaksanaan)
        {
            bool ada = false;
            string strSQL;

            strSQL = string.Format(@"SELECT * FROM referensi.whitelist_pengelolaan_kegiatan
                                    WHERE id_institusi = '{0}' AND id_skema = {1} AND kd_tahapan_kegiatan = '{2}'
                                    AND thn_usulan = '{3}' AND thn_pelaksanaan = '{4}' AND kd_sts_aktif = '1';",
                                    id_institusi, id_skema, kd_tahapan_kegiatan, thn_usulan, thn_pelaksanaan);

            DataTable dt = new DataTable();
            ada = this._db.FetchDataTable(strSQL, ref dt);
            if (!ada)
                this._errorMessage = this._db.ErrorMessage;
            {
                if (dt.Rows.Count > 0)
                {
                    id_institusi = Guid.Parse(dt.Rows[0]["id_institusi"].ToString());
                    ada = true;
                }
                else
                {
                    ada = false;
                }
            }

            return ada;
        }

        public bool getJmlPlottingAdm(int idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, string judul, int limit = 0, int offset = 0
            )
        {
            bool isSuccess = false;
            string strSQL;

            if (judul == "")
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_data 
                                        FROM hibah.list_plotting_reviewer_litabmas({0},'{1}','{2}','{3}',{4},{5});",
                                        idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, limit, offset);
                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_data 
                                        FROM hibah.list_plotting_reviewer_litabmas({0},'{1}','{2}','{3}',{4},{5}) 
                                        WHERE judul ~* '{6}' LIMIT {7} OFFSET {8};",
                                        idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan,
                                        limit, offset, judul,
                                        this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
        }

        public bool getJmlPlottingReviewer(ref DataTable dataTable, int idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, int limit = 10000, int offset = 0)
        {
            bool isSuccess = false;
            string strSQL;

            strSQL = string.Format(@"WITH rev_1 AS (
                                        SELECT COALESCE(count(*), 0)::integer AS jml_plotting_rev_1
                                        FROM hibah.list_plotting_reviewer_litabmas({0},'{1}','{2}','{3}',{4},{5}) 
                                        WHERE nama_reviewer_1 NOTNULL),
                                        rev_2 AS (
                                        SELECT COALESCE(count(*), 0)::integer AS jml_plotting_rev_2
                                        FROM hibah.list_plotting_reviewer_litabmas({0},'{1}','{2}','{3}',{4},{5}) 
                                        WHERE nama_reviewer_2 NOTNULL)
                                        SELECT * FROM rev_1, rev_2;",
                                        idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, limit, offset);

            //strSQL = string.Format(@"SELECT COALESCE(count(*),0)::int AS jml_plotting_rev_1 
            //                            FROM hibah.list_plotting_reviewer_litabmas({0},'{1}','{2}','{3}',{4},{5}) 
            //                            WHERE nama_reviewer_1 NOTNULL;",
            //                            idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, limit, offset);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getExcelPlotting(ref DataTable dataTable, int idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, int limit = 10000, int offset = 0)
        {
            bool isSuccess = false;
            string strSQL;
            if (kdTahapanKegiatan == "20")
            {
                strSQL = string.Format(@"SELECT nomor_baris AS nomor, kd_perguruan_tinggi, nama_institusi, judul, bidang_fokus, nama_reviewer_1 AS reviewer
                                            FROM hibah.list_plotting_reviewer_litabmas({0},'{1}','{2}','{3}',{4},{5});",
                            idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, limit, offset);
            }
            else
            {
                strSQL = string.Format(@"SELECT nomor_baris AS nomor, kd_perguruan_tinggi, nama_institusi, judul, bidang_fokus, nama_reviewer_1, nama_reviewer_2
                                            FROM hibah.list_plotting_reviewer_litabmas({0},'{1}','{2}','{3}',{4},{5});",
                            idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, limit, offset);
            }

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaftarPlottingAdm(int idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, string judul)
        {
            bool isSuccess = false;
            string strSQL;

            if (judul == "")
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_plotting_reviewer_litabmas({0},'{1}','{2}','{3}',{4},{5});",
                   idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, this._rowsPerPage, (this._currentPage * this._rowsPerPage));

                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_plotting_reviewer_litabmas({0},'{1}','{2}','{3}','{4}',{5},{6});",
                                        idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, judul,
                                          this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
        }

        public bool getListPlottingReviewer(ref DataTable dataTable, string idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, string jmlData, string offset
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_plotting_reviewer_litabmas ({0},'{1}','{2}','{3}',{4},{5});"
                            , idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, jmlData, offset);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListPlottingReviewerExcel(ref DataTable dataTable, string idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, string jmlData, string offset
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT row_number(*) over() as no_baris, 
                                id_transaksi_kegiatan,
                                kd_perguruan_tinggi, nama_institusi, judul, 
                                bidang_fokus, nama_reviewer_1, nama_reviewer_2 
                                FROM hibah.list_plotting_reviewer_litabmas ({0},'{1}','{2}','{3}',{4},{5});"
                            , idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, jmlData, offset);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListReviewer(ref DataTable dataTable, string idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, Guid idInstitusiYgMenugasi, string jmlData, string offset
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT count(*) over() as jml_total_data, * FROM hibah.list_reviewer_nasional_penugasan_reviewer({0},'{1}','{2}','{3}','{4}',{5},{6});"
                            , idSkema, thnUsulan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaan, jmlData, offset);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListPilihanReviewer(ref DataTable dataTable, Guid IdTransaksiKegiatan, string namaReviewer)
        {
            var query = "";
            if (namaReviewer == "")
            {
                query = $"SELECT * from hibah.list_pilihan_reviewer_administrasi ('{IdTransaksiKegiatan}');";
            }
            else
            {
                query = $"SELECT * from hibah.list_pilihan_reviewer_administrasi ('{IdTransaksiKegiatan}') " +
                    $"WHERE nama_reviewer ~* '{namaReviewer}';";
            }

            dataTable = new DataTable();
            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getListReviewerSeleksiAdministrasi(ref DataTable dataTable, string idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, Guid idInstitusiYgMenugasi, string jmlData, string offset
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT count(*) over() as jml_total_data, * FROM hibah.list_reviewer_nasional_penugasan_reviewer_seleksi_administrasi({0},'{1}','{2}','{3}','{4}',{5},{6});"
                            , idSkema, thnUsulan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaan, jmlData, offset);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        //public bool insertDataPlottingReviewer(Guid idTransaksiKegiatan, Guid idReviewer, int noUrut, Guid id_penugasan_reviewer)
        //{
        //    bool isSuccess = false;
        //    string strSQL = string.Format(@"SELECT * FROM hibah.insert_data_plotting_reviewer('{0}','{1}',{2},'{3}');"
        //                    , idTransaksiKegiatan, idReviewer, noUrut, id_penugasan_reviewer);
        //    //dataTable = new DataTable();
        //    isSuccess = this._db.ExecuteNonQuery(strSQL);
        //    if (!isSuccess)
        //        this._errorMessage = this._db.ErrorMessage;
        //    return isSuccess;
        //}

        public bool insertPlottingReviewer(Guid idTransaksiKegiatan, Guid idReviewer, int noUrut, Guid id_penugasan_reviewer)
        {
            var query = string.Format(@"SELECT * FROM hibah.insert_data_plotting_reviewer('{0}','{1}',{2},'{3}');"
                            , idTransaksiKegiatan, idReviewer, noUrut, id_penugasan_reviewer);

            if (!this._db.ExecuteNonQuery(query))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool deleteDataPlotting(Guid idTransaksiKegiatan, int noUrut)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.del_plotting_reviewer('{0}',{1});"
                            , idTransaksiKegiatan, noUrut);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }
        #endregion
    }
}