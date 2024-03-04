using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class catatanHarian : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public catatanHarian()
        {
            setInitValues();
        }

        ~catatanHarian()
        {

        }
        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool daftaritemrincian(ref DataTable dataTable, string idusulankegiatan, string jenispengeluaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format("select * from hibah.daftar_item_rincian_pembelanjaan_usulan_report ('{0}', '{1}')", idusulankegiatan, jenispengeluaran);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this.errorMessage;
            return isSuccess;
        }


        public bool insertTglTerima(string id_usulan_kegiatan, DateTime tanggal
                                    //string tanggal
                                    )
        {

            bool isSuccess = false;
            string strSQL = "select hibah.insup_usulan_tgl_dana_diterima(:id_usulan_kegiatan::uuid, :tgl_pelaksanaan::date)";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter(":id_usulan_kegiatan", id_usulan_kegiatan)
                            , new Npgsql.NpgsqlParameter(":tgl_pelaksanaan", tanggal)
                );
            if (!isSuccess)
                this._errorMessage = this.errorMessage;
            return isSuccess;
        }


        public bool isianidentitas(ref DataTable dataTable, string id_usulan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from hibah.data_usulan_kegiatan_penggunaan_anggaran('{0}')", id_usulan_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this.errorMessage;

            return isSuccess;


        }

        public bool urainpengabdianmonev(ref DataTable dataTable, string id_usulan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select *,pengabdian.get_uraian_umum_pengabdian_monev('{0}') as uraian from hibah.data_usulan_kegiatan_penggunaan_anggaran('{0}')", id_usulan_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this.errorMessage;

            return isSuccess;


        }

        public bool listanggota(ref DataTable dataTable, string idusulankegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("select * from hibah.daftar_anggota ('{0}');", idusulankegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this.errorMessage;
            return isSuccess;
        }


        public bool berkas_catatan_harian(ref DataTable dataTable, Guid idcatatanharian, string tipeberkas)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM  hibah.list_berkas_catatan_harian_pelaksanaan_test('{0}','{1}');", idcatatanharian, tipeberkas);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this.errorMessage;
            return isSuccess;
        }

        public bool listcatatanharian(ref DataTable dataTable, Guid idUsulanKegiatan, string bulan, string tahun, int limit, int offset)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT *,hibah.list_berkas_catatan_harian_img(id_catatan_harian) as daftarimg,hibah.list_berkas_catatan_harian(id_catatan_harian) as daftarberkas,hibah.list_penggunaan_anggaran(id_catatan_harian) as daftarpengeluaran FROM  hibah.list_catatan_harian_pelaksanaan('{0}','{1}','{2}','{3}','{4}');", idUsulanKegiatan, bulan, tahun, limit, offset);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this.errorMessage;
            return isSuccess;
        }

        public bool listCatatanHarian(ref DataTable dataTable, Guid idUsulanKegiatan, int bulan, int tahun)
        {
            var strSQL = $"SELECT * FROM hibah.list_catatan_harian_pelaksanaan_rb('{idUsulanKegiatan}',{bulan},{tahun});";
            dataTable = new DataTable();
            
            if (!this._db.FetchDataTable(strSQL, ref dataTable))
            {
                this._errorMessage = this.errorMessage;
                return false;
            }
            
            return true;
        }

        public bool getIdentitasUsulanKegiatan(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            var strSQL = $"SELECT * FROM hibah.get_identitas_ketua_usulan_kegiatan('{idUsulanKegiatan}');";
            dataTable = new DataTable();

            if (!this._db.FetchDataTable(strSQL, ref dataTable))
            {
                this._errorMessage = this.errorMessage;
                return false;
            }

            return true;
        }


        public bool listusulan(ref DataTable dataTable, string idpersonal, string thnPelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("select * from hibah.list_usulan_catatan_harian2('{0}','{1}');", idpersonal, thnPelaksanaan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this.errorMessage;
            return isSuccess;
        }

        public bool listpengeluaran(ref DataTable dataTable, string IDCatatanHarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format("select * from hibah.daftar_pengeluaran_penugasan('{0}') ORDER BY kd_jenis_pembelanjaan,tgl_pembelanjaan;", IDCatatanHarian);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this.errorMessage;
            return isSuccess;
        }

        public bool listberkas(ref DataTable dataTable, string IDCatatanHarian)
        {
            bool isSuccess = false;
            string strSQL = string.Format("select * from hibah.berkas_catatan_harian WHERE id_catatan_harian='{0}' ORDER BY nama_berkas;", IDCatatanHarian);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this.errorMessage;
            return isSuccess;
        }

        public bool insertlogbook(string IDCatatanHarian, string id_usulan_kegiatan, DateTime tanggal,
                               string kegiatan, string prosentase)
        {


            bool isSuccess = false;



            string strSQL = "SELECT hibah.insup_catatan_harian_pelaksanaan(:id_catatan_harian::uuid, :id_usulan_kegiatan::uuid, :tgl_pelaksanaan::date, " +
                           ":kegiatan_yg_dilakukan::text, :persentase_capaian::smallint) ";


            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter(":id_catatan_harian", IDCatatanHarian)
                            , new Npgsql.NpgsqlParameter(":id_usulan_kegiatan", id_usulan_kegiatan)
                            , new Npgsql.NpgsqlParameter(":tgl_pelaksanaan", tanggal)
                            , new Npgsql.NpgsqlParameter(":kegiatan_yg_dilakukan", kegiatan)
                            , new Npgsql.NpgsqlParameter(":persentase_capaian", prosentase)
                );
            if (!isSuccess)
                this._errorMessage = this.errorMessage;

            return isSuccess;
        }

        public bool uploadberkas(string id_berkas_catatan_harian, string IDCatatanHarian, string fileName,
                               string kegiatan, string prosentase)
        {

            string strQuery;
            bool isSuccess = false;



            strQuery = "INSERT INTO hibah.berkas_catatan_harian ";
            strQuery += "(id_berkas_catatan_harian, id_catatan_harian, nama_berkas, tipe_berkas, ukuran_berkas) ";
            strQuery += "VALUES (:id_berkas_catatan_harian::uuid, :id_catatan_harian::uuid, :nama_berkas::text, :tipe_berkas::character varying, :ukuran_berkas::integer);";

            isSuccess = this._db.ExecuteNonQuery(strQuery
                            , new Npgsql.NpgsqlParameter(":id_berkas_catatan_harian", id_berkas_catatan_harian)
                            , new Npgsql.NpgsqlParameter(":id_catatan_harian", IDCatatanHarian)
                            , new Npgsql.NpgsqlParameter(":nama_berkas", fileName)
                            , new Npgsql.NpgsqlParameter(":tipe_berkas", kegiatan)
                            , new Npgsql.NpgsqlParameter(":ukuran_berkas", prosentase)
                );


            if (!isSuccess)
                this._errorMessage = this.errorMessage;

            return isSuccess;
        }

        public bool insertpenggunaananggaran(string id_pengeluaran_penugasan, string IDCatatanHarian, string kd_jenis_pembelanjaan, DateTime tgl_pembelanjaan,
                               string nama_pembelanjaan, string jml_pembelanjaan, string no_bukti_pengeluaran)
        {


            bool isSuccess = false;



            string strSQL = "SELECT hibah.insup_pengeluaran_penugasan(:id_pengeluaran_penugasan::uuid, :IDCatatanHarian::uuid,:kd_jenis_pembelanjaan::character(6),  " +
                           ":nama_pembelanjaan::text, :jml_pembelanjaan::money,:no_bukti_pengeluaran::character varying,:tgl_pembelanjaan::date) ";


            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter(":id_pengeluaran_penugasan", id_pengeluaran_penugasan)
                            , new Npgsql.NpgsqlParameter(":IDCatatanHarian", IDCatatanHarian)
                            , new Npgsql.NpgsqlParameter(":tgl_pembelanjaan", tgl_pembelanjaan)
                            , new Npgsql.NpgsqlParameter(":nama_pembelanjaan", nama_pembelanjaan)
                            , new Npgsql.NpgsqlParameter(":jml_pembelanjaan", jml_pembelanjaan)
                            , new Npgsql.NpgsqlParameter(":kd_jenis_pembelanjaan", kd_jenis_pembelanjaan)
                            , new Npgsql.NpgsqlParameter(":no_bukti_pengeluaran", no_bukti_pengeluaran)
                );
            if (!isSuccess)
                this._errorMessage = this.errorMessage;

            return isSuccess;
        }


        public bool hapusPengeluaran(string id_pengeluaran_penugasan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"delete from hibah.pengeluaran_penugasan where id_pengeluaran_penugasan ='{0}';", id_pengeluaran_penugasan);
            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public bool hapusCatatanHarian(string id_catatan_harian)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.del_catatan_harian('{id_catatan_harian}');";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool hapusBerkas(string id_berkas_catatan_harian)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"delete from hibah.berkas_catatan_harian where id_berkas_catatan_harian ='{0}';", id_berkas_catatan_harian);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getJmlRecordsoptpt(Guid id_institusi, int id_skema, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT count(*)::int AS jml_data FROM (
                            SELECT 1 FROM hibah.list_usulan_catatan_harian_opt_pt(@id_institusi, @id_skema, @thn_pelaksanaan_kegiatan)) AS t1;";
            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                );
            if (!isSuccess)
                this._errorMessage = this.errorMessage;

            return isSuccess;
        }

        public bool getDaftarCatatanHarian(Guid id_institusi, int id_skema, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT * FROM hibah.list_usulan_catatan_harian_opt_pt(@id_institusi, @id_skema, @thn_pelaksanaan_kegiatan,  @offset, @limit);";

            isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this.errorMessage;

            return isSuccess;
        }

        public bool getDaftarCatatanHarianExcel(ref DataTable dataTable, Guid id_institusi, int id_skema, string thn_pelaksanaan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT nidn, nama, judul, nama_skema, jml_catatan, jml_pembelanjaan, persentase_capaian_maks
                        , dana_disetujui, persentase_dana_terserap, lama_kegiatan, urutan_thn_usulan_kegiatan
                    FROM hibah.list_usulan_catatan_harian_opt_pt(@id_institusi, @id_skema, @thn_pelaksanaan_kegiatan,  @offset, @limit);";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)
                , new Npgsql.NpgsqlParameter("@id_skema", id_skema)
                , new Npgsql.NpgsqlParameter("@thn_pelaksanaan_kegiatan", thn_pelaksanaan_kegiatan)
                , new Npgsql.NpgsqlParameter("@offset", this._currentPage * this._rowsPerPage)
                , new Npgsql.NpgsqlParameter("@limit", this._rowsPerPage));
            if (!isSuccess)
                this._errorMessage = this.errorMessage;

            return isSuccess;
        }

        public bool listJmlCatatanHarianPerBln(ref DataTable dataTable, Guid idUsulanKegiatan, string thn_pelaksanaan_kegiatan)
        {
            var strSQL = $"SELECT * FROM hibah.list_jml_catatan_harian_per_bln('{idUsulanKegiatan}','{thn_pelaksanaan_kegiatan}');";
            dataTable = new DataTable();

            if (!this._db.FetchDataTable(strSQL, ref dataTable))
            {
                this._errorMessage = this.errorMessage;
                return false;
            }

            return true;
        }

        #endregion
    }

}