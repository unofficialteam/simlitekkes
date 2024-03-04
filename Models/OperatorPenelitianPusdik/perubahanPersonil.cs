using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.OperatorPenelitianPusdik
{
    public class perubahanPersonil : _abstractModels
    {

        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public perubahanPersonil()
        {
            setInitValues();
        }

        ~perubahanPersonil()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getJmlUsulanDidanai(string thnPelaksanaanKegiatan, string idInstitusi, int idSkema, 
            string judul, int limit = 0, int offset = 0)
        {
            bool isSuccess = false;
            string strSQL;

            //if (judul == "")
            //{
                strSQL = string.Format(@"SELECT count(*)::int AS jml_usulan_didanai 
                                        FROM hibah.list_usulan_perubahan_personil('{0}','{1}',{2},'{3}',{4},{5});",
                                        thnPelaksanaanKegiatan, idInstitusi, idSkema, judul, limit, offset);
                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            //}
            //else
            //{
            //    strSQL = string.Format(@"SELECT count(*)::int AS jml_usulan_didanai 
            //                            FROM hibah.list_usulan_perubahan_personil('{0}','{1}',{2},{3},{4}) 
            //                            WHERE judul ~* '{5}' LIMIT {6} OFFSET {7};",
            //                            thnPelaksanaanKegiatan, idInstitusi, idSkema, 0, 0, judul,
            //                            this._rowsPerPage, (this._currentPage * this._rowsPerPage));
            //    isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
            //    if (!isSuccess)
            //        this._errorMessage = this._db.ErrorMessage;

            //    return isSuccess;
            //}

        }

        public bool getDaftarUsulanDidanai(string ThnPelaksanaanKegiatan, string idInstitusi, int idSkema, string judul)
        {
            bool isSuccess = false;
            string strSQL;

            //if (judul == "")
            //{
                strSQL = string.Format(@"SELECT * FROM hibah.list_usulan_perubahan_personil('{0}','{1}',{2},'{3}',{4},{5});",
                    ThnPelaksanaanKegiatan, idInstitusi, idSkema, judul, this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            //}
            //else
            //{
            //    strSQL = string.Format(@"SELECT * FROM hibah.list_usulan_perubahan_personil('{0}','{1}',{2},{3},{4}) 
            //                                WHERE judul ~* '{5}' LIMIT {6} OFFSET {7};",
            //                                ThnPelaksanaanKegiatan, idInstitusi, idSkema, 0, 0, judul,
            //                                this._rowsPerPage, (this._currentPage * this._rowsPerPage));
            //    isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
            //    if (!isSuccess)
            //        this._errorMessage = this._db.ErrorMessage;

            //    return isSuccess;
            //}
        }

        public bool getJmlUsulanBelumDibatalkan(string thnPelaksanaanKegiatan, string idInstitusi, int idSkema,
            string judul, int limit = 0, int offset = 0)
        {
            bool isSuccess = false;
            string strSQL;

            if (judul == "")
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_usulan_blm_dibatalkan 
                                        FROM hibah.list_usulan_belum_dibatalkan_lldikti('{0}','{1}',{2},{3},{4});",
                                        thnPelaksanaanKegiatan, idInstitusi, idSkema, limit, offset);
                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT count(*)::int AS jml_usulan_blm_dibatalkan 
                                        FROM hibah.list_usulan_belum_dibatalkan_lldikti('{0}','{1}',{2},{3},{4}) 
                                        WHERE judul ~* '{5}' LIMIT {6} OFFSET {7};",
                                        thnPelaksanaanKegiatan, idInstitusi, idSkema, 0, 0, judul,
                                        this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.ReadSkalar(strSQL, ref this._numOfRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }

        }

        public bool getDaftarUsulanBelumDibatalkan(string ThnPelaksanaanKegiatan, string idInstitusi, int idSkema, string judul)
        {
            bool isSuccess = false;
            string strSQL;

            if (judul == "")
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_usulan_belum_dibatalkan_lldikti('{0}','{1}',{2},{3},{4});",
                    ThnPelaksanaanKegiatan, idInstitusi, idSkema, this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
            else
            {
                strSQL = string.Format(@"SELECT * FROM hibah.list_usulan_belum_dibatalkan_lldikti('{0}','{1}',{2},{3},{4}) 
                                            WHERE judul ~* '{5}' LIMIT {6} OFFSET {7};",
                                            ThnPelaksanaanKegiatan, idInstitusi, idSkema, 0, 0, judul,
                                            this._rowsPerPage, (this._currentPage * this._rowsPerPage));
                isSuccess = this._db.FetchDataTable(strSQL, ref this._currentRecords);
                if (!isSuccess)
                    this._errorMessage = this._db.ErrorMessage;

                return isSuccess;
            }
        }

        public bool ListKategoriPembatalan(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT id_kategori_pembatalan, kategori_pembatalan FROM referensi.list_kategori_pembatalan();";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupPembatalanUsulan(
            Guid p_id_usulan_kegiatan,
            Guid p_id_kategori_pembatalan,
            string p_catatan,
            string p_nomor_surat,
            string p_tgl_surat
            )
        {
            string strSQL = @"SELECT * FROM hibah.insup_pembatalan_usulan(      
                              @p_id_usulan_kegiatan::uuid,
                              @p_id_kategori_pembatalan::uuid,
                              @p_catatan::text,
                              @p_nomor_surat::text,
                              @p_tgl_surat::date
                              );";

            if (!this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", p_id_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_id_kategori_pembatalan", p_id_kategori_pembatalan)
                    , new Npgsql.NpgsqlParameter("@p_catatan", p_catatan)
                    , new Npgsql.NpgsqlParameter("@p_nomor_surat", p_nomor_surat)
                    , new Npgsql.NpgsqlParameter("@p_tgl_surat", p_tgl_surat)
                    ))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool hapusPembatalan(Guid idUsulanKegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM hibah.del_pembatalan_usulan_lldikti('{0}');", idUsulanKegiatan);

            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool daftarUsulanDidanaiExcel(ref DataTable dataTable, string ThnPelaksanaanKegiatan, string idInstitusi, int idSkema)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT nidn, nama, kd_perguruan_tinggi, nama_institusi, 
                                            judul, nama_skema, catatan, nomor_surat, tgl_surat_indo AS tgl_surat, dana_disetujui 
                                            FROM hibah.list_usulan_perubahan_personil('{0}','{1}',{2},'');",
                    ThnPelaksanaanKegiatan, idInstitusi, idSkema);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool daftarPersonil(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_personil_dosen_perubahan_personil('{0}');", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getdataDosen(ref DataTable dataTable, string nidn)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pdpt.get_dosen_by_nidn_rb('{0}');", nidn);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;

        }

        public DataTable getStsEligibleSbgAnggota(string nidn, string idUsulanKegiatan)
        {
            DataTable dtResult = new DataTable();
            string strSQL = string.Format(@"SELECT * FROM public.is_syarat_keanggotaan_eligible_perubahan_personil('{0}','{1}');", nidn, idUsulanKegiatan);
            if (!this._db.FetchDataTable(strSQL, ref dtResult))
                this._errorMessage = this._db.ErrorMessage;

            return dtResult;
        }

        public DataTable getStsEligibleSbgKetua(string idUsulanKegiatan)
        {
            DataTable dtResult = new DataTable();
            string strSQL = string.Format(@"SELECT * FROM public.is_terpenuhi_calon_keanggotaan_setara_ketua('{0}');", idUsulanKegiatan);
            if (!this._db.FetchDataTable(strSQL, ref dtResult))
                this._errorMessage = this._db.ErrorMessage;

            return dtResult;
        }
        
        public bool insupPersonil(Guid idPersonil, Guid idUsulanKegiatan, Guid idPersonalUpdater, Guid idPersonal, 
                                    string kdPeranPersonil, int urutanPeran, int alokasiWaktu, string bidangTugas,
                                    string nomorSurat, string tglSurat, string catatan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insup_perubahan_personil(@id_personil::uuid, @id_usulan_kegiatan::uuid,
                                @id_personal_updater::uuid, @id_personal::uuid, @kd_peran_personil::character(1), 
                                @urutan_peran::integer, @alokasi_waktu::numeric(5, 2), @bidang_tugas::text, 
                                @nomor_surat::text, @tgl_surat::date, @catatan::text)";
            
            isSuccess = this._db.ExecuteNonQuery(strSQL
                , new Npgsql.NpgsqlParameter("@id_personil", idPersonil)
                , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", idUsulanKegiatan)
                , new Npgsql.NpgsqlParameter("@id_personal_updater", idPersonalUpdater)
                , new Npgsql.NpgsqlParameter("@id_personal", idPersonal)
                , new Npgsql.NpgsqlParameter("@kd_peran_personil", kdPeranPersonil)
                , new Npgsql.NpgsqlParameter("@urutan_peran", urutanPeran)
                , new Npgsql.NpgsqlParameter("@alokasi_waktu", alokasiWaktu)
                , new Npgsql.NpgsqlParameter("@bidang_tugas", bidangTugas)
                , new Npgsql.NpgsqlParameter("@nomor_surat", nomorSurat)
                , new Npgsql.NpgsqlParameter("@tgl_surat", tglSurat)
                , new Npgsql.NpgsqlParameter("@catatan", catatan)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool daftarPeranPersonil(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM hibah.list_peran_personil_perubahan_personil('{0}');", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool hapusPersonil(Guid idPersonil, Guid idPersonalUpdater)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"SELECT * FROM hibah.del_perubahan_personil_rb20('{0}','{1}');", idPersonil, idPersonalUpdater);

            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool listSkemaKegiatan(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = "select * from referensi.list_skema_kegiatan();";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertPerubahanJudul(Guid idUsulan, Guid idUsulanKegiatan, string judulLama, 
            string judulBaru, string catatan)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.insert_perubahan_judul('{idUsulan}', " +
                $"'{idUsulanKegiatan}', '{judulLama}', '{judulBaru}', '{catatan}');";

            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateJudul(Guid idUsulan, string judul)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM hibah.update_judul_perubahan('{idUsulan}', '{judul}');";

            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}