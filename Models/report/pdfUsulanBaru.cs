using System;
using System.Data;

namespace simlitekkes.Models.report
{
    public class pdfUsulanBaru : _abstractModels
    {
        #region Konstruktor dan Destruktor
        public pdfUsulanBaru()
        {
            setInitValues();
        }
        #endregion

        #region Methods

        public bool GetIdentitasUsulan(ref DataTable dataTable, Guid pIdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from hibah.pdf_judul_penelitian_no_1('{0}');", pIdUsulanKegiatan.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetPersonil(ref DataTable dataTable, Guid pIdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from hibah.pdf_identitas_pengusul_no_2('{0}');", pIdUsulanKegiatan.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetMitra(ref DataTable dataTable, Guid pIdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM hibah.pdf_mitra_no_3('{0}');", pIdUsulanKegiatan.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetMitraAbdimas(ref DataTable dataTable, Guid pIdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pengabdian.list_mitra_abdimas_rb('{0}');", pIdUsulanKegiatan.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetLuaranWajib(ref DataTable dataTable, Guid pIdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from hibah.pdf_luaran_wajib_dan_target_capaian_no_8('{0}');", pIdUsulanKegiatan.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetLuaranTambahan(ref DataTable dataTable, Guid pIdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from hibah.pdf_luaran_tambahan_dan_target_capaian_no_8('{0}');", pIdUsulanKegiatan.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetLuaran(ref DataTable dataTable, Guid pIdUsulanKegiatan, int pIdKelompokLuaran)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from penelitian.list_luaran_dijanjikan('{0}'::uuid,{1}::integer);",
                pIdUsulanKegiatan.ToString(), pIdKelompokLuaran.ToString());          

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetDataAproval(ref DataTable dataTable, Guid pIdUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from hibah.pdf_persetujuan_usulan_no_12('{0}');", pIdUsulanKegiatan.ToString());

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetRiwayatPengabdianDidanai(ref DataTable dataTable, string pIdPersonal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from pengabdian.list_riwayat_usulan_didanai('{0}');", pIdPersonal);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetRiwayatPeenelitianDidanai(ref DataTable dataTable, string pIdPersonal)
        {
            bool isSuccess = false;
            int kdKelompokKegiatan = 2; // 2 penelitian, 3 pengabdian
            string strSQL = string.Format("select * from hibah.riwayat_usulan_didanai('{0}'::uuid,{1}::integer);", pIdPersonal, kdKelompokKegiatan);

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool ListRekapRab(ref DataTable dataTable, string pIdUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_rekap_rab_usulan('{0}'::uuid) t1;", pIdUsulanKegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool ListDanaMitra(ref DataTable dataTable, string pIdUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT t1.kategori_mitra
                        , t1.dana_thn_1, t1.dana_thn_2, t1.dana_thn_3
                        FROM penelitian.list_mitra_rb('{0}'::uuid, 10, 0) t1 
                        where (t1.dana_thn_1 + t1.dana_thn_2 + t1.dana_thn_3) > 0", pIdUsulanKegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool ListRekapRabPerThn(ref DataTable dataTable, string pIdUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_rekap_rab_usulan_per_thn('{0}'::uuid) t1;", pIdUsulanKegiatan);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion
    }
}