using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
//

namespace simlitekkes.Models.Pengusul
{
    public class anggotaPeneliti : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public anggotaPeneliti()
        {
            setInitValues();
        }

        ~anggotaPeneliti()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getDurasiUsulan(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM hibah.get_atribut_pendaftaran_anggota2('{0}');", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getAnggotaDikti(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT count(*) AS jml_anggota FROM hibah.list_personil_dosen_selain_ketua_rb('{0}');", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listAnggotaDikti(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.list_personil_dosen_selain_ketua_rb(@idUsulanKegiatan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idUsulanKegiatan", idUsulanKegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getAnggotaTendik(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT count(*) AS jml_anggota FROM hibah.list_personil_tendik_selain_ketua_rb('{0}');", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listAnggotaTendik(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.list_personil_tendik_selain_ketua_rb(@idUsulanKegiatan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idUsulanKegiatan", idUsulanKegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listAnggotaDiktiPersonil(ref DataTable dataTable, Guid idUsulanKegiatan, Guid idPersonil)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.list_personil_dosen_selain_ketua_by_id_personil_rb(@idUsulanKegiatan, @idPersonil);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idUsulanKegiatan", idUsulanKegiatan)
            , new Npgsql.NpgsqlParameter("@idPersonil", idPersonil)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getAnggotaNonDikti(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT count(*) AS jml_anggota_non_dikti FROM hibah.list_personil_non_dosen_selain_ketua_rb('{0}');", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listAnggotaNonDikti(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.list_personil_non_dosen_selain_ketua_rb(@idUsulanKegiatan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idUsulanKegiatan", idUsulanKegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listAnggotaNonDiktiPersonil(ref DataTable dataTable, Guid idUsulanKegiatan, Guid idPersonil)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.list_personil_non_dosen_selain_ketua_by_id_personil_rb(@idUsulanKegiatan, @idPersonil);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idUsulanKegiatan", idUsulanKegiatan)
            , new Npgsql.NpgsqlParameter("@idPersonil", idPersonil)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPeranPersonil(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM hibah.list_peran_personil_rb('{0}');", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPeranPersonilSkemaPPS(ref DataTable dataTable, Guid idUsulanKegiatan, string idSkema)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM hibah.list_peran_personil_rb('{0}') WHERE kd_peran_personil != '{1}';", idUsulanKegiatan, idSkema);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public DataTable getRowByNIDN(string NIDN)
        {
            DataTable dt = new DataTable();
            string strSQL = @"SELECT * FROM pdpt.get_dosen_by_nidn_rb(:nidn);";

            if (!this._db.FetchDataTable(strSQL, ref dt
                , new Npgsql.NpgsqlParameter(":nidn", NIDN)))
                this._errorMessage = this._db.ErrorMessage;

            return dt;
        }

        public DataTable getStsEligibleSbgAnggota(string nidn, string idUsulanKegiatan)
        {
            DataTable dtResult = new DataTable();
            string strSQL = @"SELECT * FROM public.is_syarat_keanggotaan_eligible_2_rb(:nidn::character(10), :id_usulan_kegiatan::uuid);";

            if (!this._db.FetchDataTable(strSQL, ref dtResult
                , new Npgsql.NpgsqlParameter(":nidn", nidn)
                , new Npgsql.NpgsqlParameter(":id_usulan_kegiatan", idUsulanKegiatan)))
                this._errorMessage = this._db.ErrorMessage;

            return dtResult;
        }

        public DataTable getStsEligibleSbgAnggotaTPM(string nidn, string idUsulanKegiatan)
        {
            DataTable dtResult = new DataTable();
            string strSQL = @"SELECT * FROM public.is_syarat_keanggotaan_eligible_3_rb(:nidn::character(10), :id_usulan_kegiatan::uuid);";

            if (!this._db.FetchDataTable(strSQL, ref dtResult
                , new Npgsql.NpgsqlParameter(":nidn", nidn)
                , new Npgsql.NpgsqlParameter(":id_usulan_kegiatan", idUsulanKegiatan)))
                this._errorMessage = this._db.ErrorMessage;

            return dtResult;
        }

        public bool insertDataBaruAnggota(Guid idPersonal, Guid idUsulanKegiatan, string kdPeranPersonil, int urutan_peran, string bidang_tugas, bool is_tendik = false)
        {
            bool isSuccess = false;
            if (is_tendik)
            {
                string strSQL = @"SELECT hibah.insert_personil_tendik_usulan_penelitian_rb(@p_id_personal::uuid, @p_id_usulan_kegiatan::uuid, 
                                @p_kd_peran_personil::char, @p_urutan_peran::integer,@p_bidang_tugas::text);";

                isSuccess = this._db.ExecuteNonQuery(strSQL
                        , new Npgsql.NpgsqlParameter("@p_id_personal", idPersonal)
                        , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", idUsulanKegiatan)
                        , new Npgsql.NpgsqlParameter("@p_kd_peran_personil", kdPeranPersonil)
                        , new Npgsql.NpgsqlParameter("@p_urutan_peran", urutan_peran)
                        , new Npgsql.NpgsqlParameter("@p_bidang_tugas", bidang_tugas)
                        );
            }
            else
            {
                string strSQL = @"SELECT hibah.insert_personil_dosen_usulan_penelitian_rb(@p_id_personal::uuid, @p_id_usulan_kegiatan::uuid, 
                                @p_kd_peran_personil::char, @p_urutan_peran::integer,@p_bidang_tugas::text);";

                isSuccess = this._db.ExecuteNonQuery(strSQL
                        , new Npgsql.NpgsqlParameter("@p_id_personal", idPersonal)
                        , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", idUsulanKegiatan)
                        , new Npgsql.NpgsqlParameter("@p_kd_peran_personil", kdPeranPersonil)
                        , new Npgsql.NpgsqlParameter("@p_urutan_peran", urutan_peran)
                        , new Npgsql.NpgsqlParameter("@p_bidang_tugas", bidang_tugas)
                        );
            }
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteData(Guid idPersonil, bool is_tendik = false)
        {
            bool isSuccess = false;
            if (is_tendik)
            {
                string strSQL = @"SELECT hibah.del_personil_tendik_selain_ketua(@id_personil);";

                isSuccess = this._db.ExecuteNonQuery(strSQL
                        , new Npgsql.NpgsqlParameter("@id_personil", idPersonil));
            } else
            {
                string strSQL = @"SELECT hibah.del_personil_dosen_selain_ketua(@id_personil);";

                isSuccess = this._db.ExecuteNonQuery(strSQL
                        , new Npgsql.NpgsqlParameter("@id_personil", idPersonil));
            }
            
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteDataNonDosen(Guid idPersonil)
        {
            bool isSuccess = false;

            string strSQL = @"DELETE FROM hibah.personil_non_dosen WHERE id_personil = @id_personil;";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_personil", idPersonil));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listNegara(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT kd_negara, nama_negara FROM pdpt.negara order by nama_negara");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listJenjangPendidikan(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT id_jenjang_pendidikan, jenjang_pendidikan FROM pdpt.jenjang_pendidikan order by id_jenjang_pendidikan");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insertDataBaruAnggotaNonDikti(Guid id_usulan_kegiatan, int urutan_peran, string nama, string no_ktp
                                , string alamat, string no_hp, string surel, string nama_instansi_asal, string bidang_tugas
                                , string kd_negara, string kd_peran_personil, string bidang_keahlian, int id_jenjang_pendidikan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT hibah.insert_personil_non_dosen_usulan_penelitian_rb(@p_id_usulan_kegiatan, @p_urutan_peran::integer,
                                @p_nama::character varying(100), @p_no_ktp::character(16), @p_alamat::character varying(200),
                                @p_no_hp::character varying(16), @p_surel::character varying(50), 
                                @p_nama_instansi_asal::character varying(200), @p_bidang_tugas::text,
                                @p_kd_negara::character(3), @p_kd_peran_personil::character(1), @p_bidang_keahlian::character varying(100),
                                @p_id_jenjang_pendidikan::integer);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", id_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_urutan_peran", urutan_peran)
                    , new Npgsql.NpgsqlParameter("@p_nama", nama)
                    , new Npgsql.NpgsqlParameter("@p_no_ktp", no_ktp)
                    , new Npgsql.NpgsqlParameter("@p_alamat", alamat)
                    , new Npgsql.NpgsqlParameter("@p_no_hp", no_hp)
                    , new Npgsql.NpgsqlParameter("@p_surel", surel)
                    , new Npgsql.NpgsqlParameter("@p_nama_instansi_asal", nama_instansi_asal)
                    , new Npgsql.NpgsqlParameter("@p_bidang_tugas", bidang_tugas)
                    , new Npgsql.NpgsqlParameter("@p_kd_negara", kd_negara)
                    , new Npgsql.NpgsqlParameter("@p_kd_peran_personil", kd_peran_personil)
                    , new Npgsql.NpgsqlParameter("@p_bidang_keahlian", bidang_keahlian)
                    , new Npgsql.NpgsqlParameter("@p_id_jenjang_pendidikan", id_jenjang_pendidikan)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateBidangTugas(Guid idPersonil, string bidangTugas)
        {
            bool isSuccess = false;

            string strSQL = @"UPDATE hibah.personil SET bidang_tugas = @bidang_tugas WHERE id_personil = @id_personil;";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_personil", idPersonil)
                    , new Npgsql.NpgsqlParameter("@bidang_tugas", bidangTugas));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateBidangTugasNonDosen(Guid idPersonil, string bidangTugas)
        {
            bool isSuccess = false;

            string strSQL = @"UPDATE hibah.personil_non_dosen SET bidang_tugas = @bidang_tugas WHERE id_personil = @id_personil;";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_personil", idPersonil)
                    , new Npgsql.NpgsqlParameter("@bidang_tugas", bidangTugas));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateBidangTugasKetua(string p_id_usulan_kegiatan, string p_bidang_tugas)
        {
            bool isSuccess = false;

            string strSQL = @"select * FROM hibah.update_bidang_tugas_ketua(@p_bidang_tugas::text, @p_id_usulan_kegiatan::uuid);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_bidang_tugas", p_bidang_tugas)
                    , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", p_id_usulan_kegiatan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        #endregion
    }
}