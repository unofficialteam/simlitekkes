using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PT
{
    public class plottingReviewerPT : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public plottingReviewerPT()
        {
            setInitValues();
        }

        ~plottingReviewerPT()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool daftar_skema(ref DataTable dataTable, string idInstitusi)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.list_skim_klaster_desentralisasi('{0}');"
                            , idInstitusi);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool daftar_skema_thn_pelaksanaan(ref DataTable dataTable, string idInstitusi, string thn_pelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.list_skim_klaster_desentralisasi_thn_pelaksanaan('{0}', '{1}');"
                            , idInstitusi, thn_pelaksanaan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool daftar_skema_desentralisasi(ref DataTable dataTable, string idInstitusi)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.list_skim_desentralisasi('1');"
                            , idInstitusi);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool daftar_tahapan(ref DataTable dataTable, string idSkema)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"select t2.kd_tahapan_kegiatan,tahapan from referensi.skema_kegiatan t1 inner join referensi.tahapan_kegiatan_skema t2 on t1.id_skema=t2.id_skema
                                            inner JOIN referensi.tahapan_kegiatan t3 on t3.kd_tahapan_kegiatan=t2.kd_tahapan_kegiatan
                                            where t1.id_skema='{0}' and t2.kd_tahapan_kegiatan in ('22','23','32','35');"
                            , idSkema);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool get_cek_plotting(ref DataTable dataTable, string idTransaksi, string idReviewer, string nourut)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"select * from hibah.get_cek_plotting('{0}'::uuid,'{1}'::uuid,{2}::smallint);"
                            , idTransaksi, idReviewer, nourut);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool get_cek_plotting_edisi_xi(ref DataTable dataTable, string idTransaksi, string idReviewer, string nourut)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"select * from hibah.get_cek_plotting_pt_edisi_xi('{0}'::uuid,'{1}'::uuid,{2}::smallint);"
                            , idTransaksi, idReviewer, nourut);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListPlottingReviewer(ref DataTable dataTable, string idInstitusi, string idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, string jmlData, string offset
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.list_plotting_reviewer_desentralisasi('{0}',{1},'{2}','{3}','{4}',{5},{6});",
                            idInstitusi, idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, jmlData, offset);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListPlottingReviewerLanjutan(ref DataTable dataTable, string idInstitusi, string idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, string jmlData, string offset
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.list_plotting_reviewer_desentralisasi_lanjutan ('{0}',{1},'{2}','{3}','{4}',{5},{6});",
                            idInstitusi, idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan, jmlData, offset);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }


        public bool getJmlPlottingReviewer(ref DataTable dataTable, string idInstitusi, string idSkema, string thnUsulan, string thnPelaksanaan, string kdTahapanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT COUNT(id_usulan_kegiatan) as JmlProposal,
                                            COUNT(CASE WHEN NOT nama_reviewer_1 IS NULL THEN 1 END) AS JmlRev1,
                                            COUNT(CASE WHEN NOT nama_reviewer_2 IS NULL THEN 1 END) AS JmlRev2
                                            FROM penelitian.list_plotting_reviewer_desentralisasi ('{0}',{1},'{2}','{3}','{4}',0,0);",
                            idInstitusi, idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan);
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

        public bool getListReviewer(ref DataTable dataTable, string idSkema, string thnUsulan, string thnPelaksanaan,
            string kdTahapanKegiatan, Guid idInstitusiYgMenugasi, string jmlData, string offset, string nama
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_penugasan_reviewer({0},'{1}','{2}','{3}','{4}',{5},{6}) where (nama_reviewer ~* '{7}' or nidn ~* '{7}');"
                            , idSkema, thnUsulan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaan, jmlData, offset, nama);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getJmlDaftarReviewerDitugaskan(int idSkema, string thnUsulan, string kdTahapanKegiatan,
            Guid idInstitusiYgMenugasi, string thnPelaksanaan, Guid idTransaksiKegiatan, string nama, int limit = 0, int offset = 0)
        {
            bool isSuccess = false;
            string strSQL;

            if (nama == "")
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_reviewer 
                                        FROM hibah.list_penugasan_reviewer_rb({0},'{1}','{2}','{3}','{4}','{5}',{6},{7});",
                                        idSkema, thnUsulan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaan,
                                        idTransaksiKegiatan, limit, offset);
                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_reviewer 
                                        FROM hibah.list_penugasan_reviewer_rb({0},'{1}','{2}','{3}','{4}','{5}',{6},{7}) 
                                        WHERE nama_reviewer ~* '{8}' LIMIT {9} OFFSET {10};",
                                        idSkema, thnUsulan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaan,
                                        idTransaksiKegiatan, 0, 0, nama,
                                        this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
        }

        public bool getDaftarReviewerDitugaskan(int idSkema, string thnUsulan, string kdTahapanKegiatan,
            Guid idInstitusiYgMenugasi, string thnPelaksanaan, Guid idTransaksiKegiatan, string nama)
        {
            bool isSuccess = false;
            string strSQL;

            if (nama == "")
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_penugasan_reviewer_rb({0},'{1}','{2}','{3}','{4}','{5}',{6},{7});",
                    idSkema, thnUsulan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaan, idTransaksiKegiatan,
                    this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_penugasan_reviewer_rb({0},'{1}','{2}','{3}','{4}','{5}',{6},{7}) 
                                            WHERE nama_reviewer ~* '{8}' LIMIT {9} OFFSET {10};",
                                            idSkema, thnUsulan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaan, idTransaksiKegiatan,
                                            0, 0, nama, this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
        }

        public bool getListBebanReviewer(ref DataTable dataTable, string thnUsulan, string thnPelaksanaan,
    string kdTahapanKegiatan, Guid idInstitusiYgMenugasi, string kdProgramHibah, string jmlData, string offset, string nama
    )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_beban_reviewer('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7});"
                            , thnUsulan, thnPelaksanaan, kdTahapanKegiatan, idInstitusiYgMenugasi, kdProgramHibah, nama, jmlData, offset);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getExcelBebanReviewer(ref DataTable dataTable, string thnUsulan, string thnPelaksanaan,
    string kdTahapanKegiatan, Guid idInstitusiYgMenugasi, string kdProgramHibah, string jmlData, string offset, string nama
    )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT nomor_baris AS no, nama_reviewer, nidn, nama_institusi, 
                                            kompetensi, jml_proposal, jml_skema 
                                            FROM hibah.list_beban_reviewer('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7});"
                            , thnUsulan, thnPelaksanaan, kdTahapanKegiatan, idInstitusiYgMenugasi, kdProgramHibah, nama, jmlData, offset);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListReviewerEdisixi(ref DataTable dataTable, string idSkema, string thnUsulan, string kdTahapanKegiatan,
            Guid idInstitusiYgMenugasi, string thnPelaksanaan, Guid idTransaksiKegiatan, string jmlData, string offset, string nama
            )
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_penugasan_reviewer_opt_pt_edisi_xi({0},'{1}','{2}','{3}','{4}','{5}',{6},{7}) WHERE (nama_reviewer ~* '{8}' OR nidn ~* '{8}');"
                            , idSkema, thnUsulan, kdTahapanKegiatan, idInstitusiYgMenugasi, thnPelaksanaan, idTransaksiKegiatan, jmlData, offset, nama);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertDataPlottingReviewer(ref DataTable dataTable, Guid idTransaksiKegiatan, Guid idReviewer, int noUrut, Guid idPenugasanReviewer)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.insert_data_plotting_reviewer('{0}','{1}',{2},'{3}');"
                            , idTransaksiKegiatan, idReviewer, noUrut, idPenugasanReviewer);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
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

        public bool getExportExcel(ref DataTable dataTable, string idInstitusi, string idSkema, string thnUsulan, string thnPelaksanaan, string kdTahapanKegiatan)
        {
            bool isSuccess = false;
            string strSQL;
            if (kdTahapanKegiatan == "20")
            {
                strSQL = string.Format(@"SELECT nomor_baris AS nomor, kd_perguruan_tinggi, nama_institusi, judul, bidang_fokus, nama_reviewer_1 AS reviewer
                                            FROM penelitian.list_plotting_reviewer_desentralisasi ('{0}',{1},'{2}','{3}','{4}');",
                            idInstitusi, idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan);
            }
            else
            {
                strSQL = string.Format(@"SELECT nomor_baris AS nomor, kd_perguruan_tinggi, nama_institusi, judul, bidang_fokus, nama_reviewer_1, nama_reviewer_2
                                            FROM penelitian.list_plotting_reviewer_desentralisasi ('{0}',{1},'{2}','{3}','{4}');",
                            idInstitusi, idSkema, thnUsulan, thnPelaksanaan, kdTahapanKegiatan);
            }

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

        public bool getListSkemaNJmlReviewer(ref DataTable dataTable, string thnUsulan, string thnPelaksanaan
                            , Guid idInstitusi, string kdJenisKegiatan, string kdTahapanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_plotting_jml_reviewer_perskema('{0}','{1}','{2}','{3}','{4}');"
                            , thnUsulan, thnPelaksanaan, idInstitusi, kdJenisKegiatan, kdTahapanKegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListPlottingReviewerPengabdian(ref DataTable dataTable, string thnUsulan, string thnPelaksanaan
                            , Guid idInstitusi, string kdTahapanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_plotting_reviewer_pengabdian('{0}','{1}','{2}','{3}');"
                            , thnUsulan, thnPelaksanaan, idInstitusi, kdTahapanKegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getListSkemaNJmlReviewerLanjutan(ref DataTable dataTable, string thnUsulan, string thnPelaksanaan
                            , Guid idInstitusi, string kdJenisKegiatan, string kdTahapanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_plotting_jml_reviewer_perskema_lanjutan('{0}','{1}','{2}','{3}','{4}');"
                            , thnUsulan, thnPelaksanaan, idInstitusi, kdJenisKegiatan, kdTahapanKegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }



        public bool getListSkemaNJmlReviewerExcel(ref DataTable dataTable, string thnUsulan, string thnPelaksanaan
                           , Guid idInstitusi, string kdJenisKegiatan, string kdTahapanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT nama_skema, jml_usulan, jml_reviewer FROM hibah.list_plotting_jml_reviewer_perskema('{0}','{1}','{2}','{3}','{4}');"
                            , thnUsulan, thnPelaksanaan, idInstitusi, kdJenisKegiatan, kdTahapanKegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion
    }
}