using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using simlitekkes.Models.Pengusul.Mitra;

namespace simlitekkes.Models.Pengusul
{
    public class mitraAbdimas : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public mitraAbdimas()
        {
            setInitValues();
        }

        ~mitraAbdimas()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool getMitraPengabdianPerSkema(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pengabdian.get_kategori_mitra_pengabdian_by_skema('{0}');", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listTipeMitraPelaksanaPPK(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT id_tipe_mitra, tipe_mitra FROM referensi.tipe_mitra WHERE id_tipe_mitra IN (3,4) AND kd_sts_aktif_data ='1' order by id_tipe_mitra");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listTipeMitraSasaranUMKM(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT id_tipe_mitra, tipe_mitra FROM referensi.tipe_mitra WHERE id_tipe_mitra IN (5,6,7,8) AND kd_sts_aktif_data ='1' order by id_tipe_mitra");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listTipeMitraSasaranUMKMPKM(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT id_tipe_mitra, tipe_mitra FROM referensi.tipe_mitra WHERE id_tipe_mitra IN (5,8) AND kd_sts_aktif_data ='1' order by id_tipe_mitra");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listTipeMitraSasaranUMKMPKW(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT id_tipe_mitra, tipe_mitra FROM referensi.tipe_mitra WHERE id_tipe_mitra IN (5) AND kd_sts_aktif_data ='1' order by id_tipe_mitra");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listJenisMitra(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT id_jenis_mitra, jenis_mitra FROM referensi.jenis_mitra WHERE kd_sts_aktif_data ='1' order by id_jenis_mitra");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listNamaPerguruanTinggi(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT i.id_institusi, i.nama_institusi " +
                                            " FROM pdpt.institusi i" +
                                            " JOIN pdpt.perguruan_tinggi pt ON i.id_institusi = pt.id_institusi" +
                                            " WHERE i.kd_sts_aktif ='1' " +
                                            " AND LEFT(pt.kd_perguruan_tinggi, 1) < '2' " +
                                            " ORDER BY i.nama_institusi");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listProvinsi(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT kd_provinsi, nama_provinsi FROM pdpt.provinsi WHERE kd_sts_aktif ='1' order by nama_provinsi");
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listKota(ref DataTable dataTable, string kdProvinsi)
        {
            bool isSuccess = false;
            var query = $@"SELECT kd_kota::character varying, nama_kota FROM pdpt.kota WHERE kd_provinsi = '{kdProvinsi}' AND kd_sts_aktif='1' order by nama_kota;";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listKecamatan(ref DataTable dataTable, string kdKota)
        {
            bool isSuccess = false;
            var query = $@"SELECT kd_kecamatan, nama_kecamatan FROM pdpt.kecamatan WHERE kd_kota = '{kdKota}' order by nama_kecamatan;";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listDesa(ref DataTable dataTable, string kdKecamatan)
        {
            bool isSuccess = false;
            var query = $@"SELECT kd_desa, nama_desa FROM pdpt.desa WHERE kd_kecamatan = '{kdKecamatan}' order by nama_desa;";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getDaerahPrioritas(ref DataTable dataTable, string kdDesa, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            var query = $@"SELECT * FROM pengabdian.get_daerah_prioritas('{kdDesa}', '{idUsulanKegiatan}');";
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(query, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool listMitraPelaksanaPengabdian(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_mitra_abdimas_rb(@idUsulanKegiatan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idUsulanKegiatan", idUsulanKegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool listMitraPelaksanaPpdm(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_mitra_pelaksana_ppdm(@idUsulanKegiatan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idUsulanKegiatan", idUsulanKegiatan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getMitraPelaksanaPtPengabdian(ref DataTable dataTable, Guid idMitra)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.get_mitra_pelaksana_pt_abdimas_rb(@idMitra);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@idMitra", idMitra)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupMitraPelaksanaPPK
        (
            Guid idUsulanKegiatan,
            int kdKategoriMitra,
            int idTipeMitra,
            string namaPimpinan,
            string alamat,
            string jabatan,
            Guid idMitra
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from pengabdian.insup_mitra_abdimas_pelaksana_pt_rb(
                        @id_usulan_kegiatan::uuid,
                        @kd_kategori_mitra::integer,
                        @id_tipe_mitra::integer,
                        @nama_pimpinan_mitra::text,
                        @alamat_oganisasi_institusi::text,
                        @jabatan::text,
                        @id_mitra_abdimas::uuid
                            );";

            DataTable dt = new DataTable();
            if (this._db.FetchDataTable(strSQL, ref dt
                    , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_kategori_mitra", kdKategoriMitra)
                    , new Npgsql.NpgsqlParameter("@id_tipe_mitra", idTipeMitra)
                    , new Npgsql.NpgsqlParameter("@nama_pimpinan_mitra", namaPimpinan)
                    , new Npgsql.NpgsqlParameter("@alamat_oganisasi_institusi", alamat)
                    , new Npgsql.NpgsqlParameter("@jabatan", jabatan)
                    , new Npgsql.NpgsqlParameter("@id_mitra_abdimas", idMitra)
                    ))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool insupPTPelaksana(PTPelaksana pt)
        {
            return insupMitraAbdimas(
                pt.IdMitraAbdimas, pt.IdUsulanKegiatan, pt.KdKategoriMitra, pt.IdTipeMitra,
                null, pt.NamaPimpinanInstitusi, pt.Jabatan, null, null, pt.AlamatInstitusi,
                null, null, null, null, null, null, null, pt.DanaTahun1, pt.DanaTahun2,
                pt.DanaTahun3, null, pt.LamaKegiatan);
        }

        public bool listPTPelaksana(ref List<PTPelaksana> listPT, Guid idUsulanKegiatan, int idTipeMitra)
        {

            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listPT = new List<PTPelaksana>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var pt = new PTPelaksana();
                pt.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                pt.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                pt.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                pt.NamaPimpinanInstitusi = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                pt.Jabatan = dt.Rows[i]["jabatan"].ToString();
                pt.AlamatInstitusi = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                pt.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                pt.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                pt.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                pt.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                pt.TglUnggahPernyataan = tglUnggahPernyataan;

                listPT.Add(pt);
            }

            return true;
        }

        public bool getPTPelaksana(ref PTPelaksana pt, Guid idMitraAbdimas)
        {
            DataTable dt = new DataTable();
            if (!getMitra(ref dt, idMitraAbdimas))
            {
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                pt = new PTPelaksana();
                pt.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                pt.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                pt.NamaPimpinanInstitusi = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                pt.Jabatan = dt.Rows[0]["jabatan"].ToString();
                pt.AlamatInstitusi = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                pt.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[0]["dana_tahun_1"].ToString(), out danaTahun1);
                pt.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[0]["dana_tahun_2"].ToString(), out danaTahun2);
                pt.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[0]["dana_tahun_3"].ToString(), out danaTahun3);
                pt.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                pt.TglUnggahPernyataan = tglUnggahPernyataan;
            }

            return true;
        }

        public bool insupPTMitra(PTMitra pt)
        {
            return insupMitraAbdimas(
                pt.IdMitraAbdimas, pt.IdUsulanKegiatan, pt.KdKategoriMitra, pt.IdTipeMitra,
                null, pt.NamaPimpinanInstitusi, pt.Jabatan, null, null, pt.AlamatInstitusi,
                null, null, pt.IdInstitusi, null, null, null, null, pt.DanaTahun1, pt.DanaTahun2,
                pt.DanaTahun3, null, pt.LamaKegiatan);
        }

        public bool listPTMitra(ref List<PTMitra> listPT, Guid idUsulanKegiatan, int idTipeMitra)
        {
            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listPT = new List<PTMitra>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var pt = new PTMitra();
                pt.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                pt.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                pt.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                pt.NamaPimpinanInstitusi = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                pt.Jabatan = dt.Rows[i]["jabatan"].ToString();
                pt.AlamatInstitusi = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                pt.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();
                pt.IdInstitusi = Guid.Parse(dt.Rows[i]["id_institusi"].ToString());

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                pt.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                pt.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                pt.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                pt.TglUnggahPernyataan = tglUnggahPernyataan;

                listPT.Add(pt);
            }

            return true;
        }

        public bool getPTMitra(ref PTMitra pt, Guid idMitraAbdimas)
        {
            DataTable dt = new DataTable();
            if (!getMitra(ref dt, idMitraAbdimas))
            {
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                pt = new PTMitra();
                pt.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                pt.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                pt.NamaPimpinanInstitusi = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                pt.Jabatan = dt.Rows[0]["jabatan"].ToString();
                pt.AlamatInstitusi = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                pt.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();
                pt.IdInstitusi = Guid.Parse(dt.Rows[0]["id_institusi"].ToString());

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[0]["dana_tahun_1"].ToString(), out danaTahun1);
                pt.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[0]["dana_tahun_2"].ToString(), out danaTahun2);
                pt.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[0]["dana_tahun_3"].ToString(), out danaTahun3);
                pt.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                pt.TglUnggahPernyataan = tglUnggahPernyataan;
            }

            return true;
        }

        public bool insupPemdaPemkot(PemdaPemkot pemda)
        {
            return insupMitraAbdimas(
                pemda.IdMitraAbdimas, pemda.IdUsulanKegiatan, pemda.KdKategoriMitra, pemda.IdTipeMitra,
                null, pemda.NamaPimpinanInstitusi, pemda.Jabatan, pemda.NamaOrganisasiInstitusi,
                null, pemda.AlamatInstitusi, null, null, null, null, null, null, null,
                pemda.DanaTahun1, pemda.DanaTahun2, pemda.DanaTahun3, null, pemda.LamaKegiatan);
        }

        public bool listPemdaPemkot(ref List<PemdaPemkot> listPemda, Guid idUsulanKegiatan, int idTipeMitra)
        {
            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listPemda = new List<PemdaPemkot>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var pemda = new PemdaPemkot();
                pemda.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                pemda.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                pemda.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                pemda.NamaPimpinanInstitusi = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                pemda.Jabatan = dt.Rows[i]["jabatan"].ToString();
                pemda.NamaOrganisasiInstitusi = dt.Rows[i]["nama_organisasi_institusi"].ToString();
                pemda.AlamatInstitusi = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                pemda.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                pemda.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                pemda.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                pemda.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                pemda.TglUnggahPernyataan = tglUnggahPernyataan;

                listPemda.Add(pemda);
            }

            return true;
        }

        public bool getPemdaPemkot(ref PemdaPemkot pemda, Guid idMitraAbdimas)
        {
            DataTable dt = new DataTable();
            if (!getMitra(ref dt, idMitraAbdimas))
            {
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                pemda = new PemdaPemkot();
                pemda.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                pemda.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                pemda.NamaPimpinanInstitusi = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                pemda.Jabatan = dt.Rows[0]["jabatan"].ToString();
                pemda.NamaOrganisasiInstitusi = dt.Rows[0]["nama_organisasi_institusi"].ToString();
                pemda.AlamatInstitusi = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                pemda.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[0]["dana_tahun_1"].ToString(), out danaTahun1);
                pemda.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[0]["dana_tahun_2"].ToString(), out danaTahun2);
                pemda.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[0]["dana_tahun_3"].ToString(), out danaTahun3);
                pemda.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                pemda.TglUnggahPernyataan = tglUnggahPernyataan;
            }

            return true;
        }

        public bool insupLembagaCSR(LembagaCSR lembaga)
        {
            return insupMitraAbdimas(
                lembaga.IdMitraAbdimas, lembaga.IdUsulanKegiatan, lembaga.KdKategoriMitra, lembaga.IdTipeMitra,
                null, lembaga.NamaPimpinanInstitusi, lembaga.Jabatan, lembaga.NamaOrganisasiInstitusi,
                null, lembaga.AlamatInstitusi, null, null, null, null, null, null, null,
                lembaga.DanaTahun1, lembaga.DanaTahun2, lembaga.DanaTahun3, null, lembaga.LamaKegiatan);
        }

        public bool listLembagaCSR(ref List<LembagaCSR> listLembaga, Guid idUsulanKegiatan, int idTipeMitra)
        {
            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listLembaga = new List<LembagaCSR>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var lembaga = new LembagaCSR();
                lembaga.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                lembaga.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                lembaga.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                lembaga.NamaPimpinanInstitusi = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                lembaga.Jabatan = dt.Rows[i]["jabatan"].ToString();
                lembaga.NamaOrganisasiInstitusi = dt.Rows[i]["nama_organisasi_institusi"].ToString();
                lembaga.AlamatInstitusi = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                lembaga.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                lembaga.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                lembaga.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                lembaga.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                lembaga.TglUnggahPernyataan = tglUnggahPernyataan;

                listLembaga.Add(lembaga);
            }

            return true;
        }

        public bool getLembagaCSR(ref LembagaCSR lembaga, Guid idMitraAbdimas)
        {
            DataTable dt = new DataTable();
            if (!getMitra(ref dt, idMitraAbdimas))
            {
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                lembaga = new LembagaCSR();
                lembaga.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                lembaga.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                lembaga.NamaPimpinanInstitusi = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                lembaga.Jabatan = dt.Rows[0]["jabatan"].ToString();
                lembaga.NamaOrganisasiInstitusi = dt.Rows[0]["nama_organisasi_institusi"].ToString();
                lembaga.AlamatInstitusi = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                lembaga.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[0]["dana_tahun_1"].ToString(), out danaTahun1);
                lembaga.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[0]["dana_tahun_2"].ToString(), out danaTahun2);
                lembaga.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[0]["dana_tahun_3"].ToString(), out danaTahun3);
                lembaga.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                lembaga.TglUnggahPernyataan = tglUnggahPernyataan;
            }

            return true;
        }

        public bool insupKelompokMasyarakat(KelompokMasyarakat kelompok)
        {
            return insupMitraAbdimas(
                kelompok.IdMitraAbdimas, kelompok.IdUsulanKegiatan, kelompok.KdKategoriMitra, kelompok.IdTipeMitra,
                kelompok.IdJenisMitra, kelompok.NamaPimpinanMitra, null, null, kelompok.KdDesa,
                kelompok.AlamatMitra, kelompok.Jarak, null, null, null,
                null, null, null, kelompok.DanaTahun1, kelompok.DanaTahun2, kelompok.DanaTahun3,
                null, kelompok.LamaKegiatan);
        }

        public bool listKelompokMasyarakat(ref List<KelompokMasyarakat> listKelompok, Guid idUsulanKegiatan, int idTipeMitra)
        {
            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listKelompok = new List<KelompokMasyarakat>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var kelompok = new KelompokMasyarakat();
                kelompok.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                kelompok.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                kelompok.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                kelompok.IdJenisMitra = int.Parse(dt.Rows[i]["id_jenis_mitra"].ToString());
                kelompok.NamaPimpinanMitra = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                kelompok.KdDesa = dt.Rows[i]["kd_desa"].ToString();
                kelompok.AlamatMitra = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                kelompok.Jarak = int.Parse(dt.Rows[i]["jarak"].ToString());
                kelompok.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                kelompok.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                kelompok.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                kelompok.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                kelompok.TglUnggahPernyataan = tglUnggahPernyataan;

                listKelompok.Add(kelompok);
            }

            return true;
        }

        public bool getKelompokMasyarakat(ref KelompokMasyarakat kelompok, Guid idMitraAbdimas)
        {
            DataTable dt = new DataTable();
            if (!getMitra(ref dt, idMitraAbdimas))
            {
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                kelompok = new KelompokMasyarakat();
                kelompok.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                kelompok.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                kelompok.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                kelompok.IdJenisMitra = int.Parse(dt.Rows[0]["id_jenis_mitra"].ToString());
                kelompok.NamaPimpinanMitra = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                kelompok.KdDesa = dt.Rows[0]["kd_desa"].ToString();
                kelompok.AlamatMitra = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                kelompok.Jarak = int.Parse(dt.Rows[0]["jarak"].ToString());
                kelompok.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[0]["dana_tahun_1"].ToString(), out danaTahun1);
                kelompok.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[0]["dana_tahun_2"].ToString(), out danaTahun2);
                kelompok.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[0]["dana_tahun_3"].ToString(), out danaTahun3);
                kelompok.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                kelompok.TglUnggahPernyataan = tglUnggahPernyataan;
            }

            return true;
        }

        public bool insupLembagaPemerintahan(LembagaPemerintahan lembaga)
        {
            return insupMitraAbdimas(
                lembaga.IdMitraAbdimas, lembaga.IdUsulanKegiatan, lembaga.KdKategoriMitra, lembaga.IdTipeMitra,
                lembaga.IdJenisMitra, lembaga.NamaPimpinanLembaga, null, null, lembaga.KdDesa,
                lembaga.AlamatLembaga, lembaga.Jarak, null, null, null, null, null, null,
                lembaga.DanaTahun1, lembaga.DanaTahun2, lembaga.DanaTahun3, lembaga.BidangMasalah, lembaga.LamaKegiatan);
        }

        public bool listLembagaPemerintahan(ref List<LembagaPemerintahan> listLembaga, Guid idUsulanKegiatan, int idTipeMitra)
        {
            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listLembaga = new List<LembagaPemerintahan>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var lembaga = new LembagaPemerintahan();
                lembaga.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                lembaga.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                lembaga.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                lembaga.IdJenisMitra = int.Parse(dt.Rows[i]["id_jenis_mitra"].ToString());
                lembaga.NamaPimpinanLembaga = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                lembaga.KdDesa = dt.Rows[i]["kd_desa"].ToString();
                lembaga.AlamatLembaga = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                lembaga.Jarak = int.Parse(dt.Rows[i]["jarak"].ToString());
                lembaga.BidangMasalah = dt.Rows[i]["bidang_masalah_mitra"].ToString();
                lembaga.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                lembaga.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                lembaga.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                lembaga.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                lembaga.TglUnggahPernyataan = tglUnggahPernyataan;

                listLembaga.Add(lembaga);
            }

            return true;
        }

        public bool getLembagaPemerintahan(ref LembagaPemerintahan lembaga, Guid idMitraAbdimas)
        {
            DataTable dt = new DataTable();
            if (!getMitra(ref dt, idMitraAbdimas))
            {
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                lembaga = new LembagaPemerintahan();
                lembaga.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                lembaga.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                lembaga.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                lembaga.IdJenisMitra = int.Parse(dt.Rows[0]["id_jenis_mitra"].ToString());
                lembaga.NamaPimpinanLembaga = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                lembaga.KdDesa = dt.Rows[0]["kd_desa"].ToString();
                lembaga.AlamatLembaga = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                lembaga.Jarak = int.Parse(dt.Rows[0]["jarak"].ToString());
                lembaga.BidangMasalah = dt.Rows[0]["bidang_masalah_mitra"].ToString();
                lembaga.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[0]["dana_tahun_1"].ToString(), out danaTahun1);
                lembaga.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[0]["dana_tahun_2"].ToString(), out danaTahun2);
                lembaga.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[0]["dana_tahun_3"].ToString(), out danaTahun3);
                lembaga.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                lembaga.TglUnggahPernyataan = tglUnggahPernyataan;
            }

            return true;
        }

        public bool insupInstansiSwasta(InstansiSwasta instansi)
        {
            return insupMitraAbdimas(
                instansi.IdMitraAbdimas, instansi.IdUsulanKegiatan, instansi.KdKategoriMitra, instansi.IdTipeMitra,
                instansi.IdJenisMitra, instansi.NamaPimpinanInstansi, null, null, instansi.KdDesa,
                instansi.AlamatInstansi, instansi.Jarak, null, null, null, null, null, null,
                instansi.DanaTahun1, instansi.DanaTahun2, instansi.DanaTahun3,
                instansi.BidangMasalah, instansi.LamaKegiatan);
        }

        public bool listInstansiSwasta(ref List<InstansiSwasta> listInstansi, Guid idUsulanKegiatan, int idTipeMitra)
        {
            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listInstansi = new List<InstansiSwasta>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var instansi = new InstansiSwasta();
                instansi.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                instansi.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                instansi.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                instansi.IdJenisMitra = int.Parse(dt.Rows[i]["id_jenis_mitra"].ToString());
                instansi.NamaPimpinanInstansi = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                instansi.KdDesa = dt.Rows[i]["kd_desa"].ToString();
                instansi.AlamatInstansi = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                instansi.Jarak = int.Parse(dt.Rows[i]["jarak"].ToString());
                instansi.BidangMasalah = dt.Rows[i]["bidang_masalah_mitra"].ToString();
                instansi.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                instansi.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                instansi.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                instansi.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                instansi.TglUnggahPernyataan = tglUnggahPernyataan;

                listInstansi.Add(instansi);
            }

            return true;
        }

        public bool getInstansiSwasta(ref InstansiSwasta instansi, Guid idMitraAbdimas)
        {
            DataTable dt = new DataTable();
            if (!getMitra(ref dt, idMitraAbdimas))
            {
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                instansi = new InstansiSwasta();
                instansi.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                instansi.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                instansi.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                instansi.IdJenisMitra = int.Parse(dt.Rows[0]["id_jenis_mitra"].ToString());
                instansi.NamaPimpinanInstansi = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                instansi.KdDesa = dt.Rows[0]["kd_desa"].ToString();
                instansi.AlamatInstansi = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                instansi.Jarak = int.Parse(dt.Rows[0]["jarak"].ToString());
                instansi.BidangMasalah = dt.Rows[0]["bidang_masalah_mitra"].ToString();
                instansi.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[0]["dana_tahun_1"].ToString(), out danaTahun1);
                instansi.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[0]["dana_tahun_2"].ToString(), out danaTahun2);
                instansi.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[0]["dana_tahun_3"].ToString(), out danaTahun3);
                instansi.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                instansi.TglUnggahPernyataan = tglUnggahPernyataan;
            }

            return true;
        }

        public bool insupUMKM(UMKM unit)
        {
            return insupMitraAbdimas(
                unit.IdMitraAbdimas, unit.IdUsulanKegiatan, unit.KdKategoriMitra, unit.IdTipeMitra,
                unit.IdJenisMitra, unit.NamaPimpinan, unit.Jabatan, unit.NamaUMKM, unit.KdDesa,
                unit.Alamat, unit.Jarak, null, null, unit.BidangUsaha, unit.Asset, unit.Omzet, null,
                unit.DanaTahun1, unit.DanaTahun2, unit.DanaTahun3, unit.BidangMasalah, unit.LamaKegiatan, unit.UrutanTahun);
        }

        public bool insupPPDM(KelompokMasyarakatPPDM unit)
        {
            return insupMitraAbdimas(
                unit.IdMitraAbdimas, unit.IdUsulanKegiatan, unit.KdKategoriMitra, unit.IdTipeMitra,
                unit.IdJenisMitra, unit.NamaPimpinanMitra, null, unit.NamaMitra, null,
                unit.AlamatMitra, null, null, null, null, null, null,unit.IdMitraReferensi,
                null, null, null, unit.BidangMasalah, null,unit.UrutanTahun);
        }

        public bool listUMKM(ref List<UMKM> listUnit, Guid idUsulanKegiatan, int idTipeMitra)
        {
            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listUnit = new List<UMKM>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var unit = new UMKM();
                unit.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                unit.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                unit.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                unit.IdJenisMitra = int.Parse(dt.Rows[i]["id_jenis_mitra"].ToString());
                unit.NamaPimpinan = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                unit.NamaUMKM = dt.Rows[i]["nama_organisasi_institusi"].ToString();
                unit.BidangUsaha = dt.Rows[i]["bidang_usaha"].ToString();
                unit.KdDesa = dt.Rows[i]["kd_desa"].ToString();
                unit.Alamat = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                unit.Jarak = int.Parse(dt.Rows[i]["jarak"].ToString());
                unit.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3, asset, omzet;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                unit.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                unit.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                unit.DanaTahun3 = danaTahun3;
                decimal.TryParse(dt.Rows[i]["asset"].ToString(), out asset);
                unit.Asset = asset;
                decimal.TryParse(dt.Rows[i]["omzet"].ToString(), out omzet);
                unit.Omzet = omzet;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                unit.TglUnggahPernyataan = tglUnggahPernyataan;

                listUnit.Add(unit);
            }

            return true;
        }

        public bool getUMKM(ref UMKM unit, Guid idMitraAbdimas)
        {
            DataTable dt = new DataTable();
            if (!getMitra(ref dt, idMitraAbdimas))
            {
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                unit = new UMKM();
                //unit.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                unit.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                unit.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                unit.IdJenisMitra = int.Parse(dt.Rows[0]["id_jenis_mitra"].ToString());
                unit.NamaPimpinan = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                unit.Jabatan = dt.Rows[0]["jabatan"].ToString();
                unit.NamaUMKM = dt.Rows[0]["nama_organisasi_institusi"].ToString();
                unit.BidangUsaha = dt.Rows[0]["bidang_usaha"].ToString();
                unit.KdDesa = dt.Rows[0]["kd_desa"].ToString();
                unit.Alamat = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                unit.Jarak = int.Parse(dt.Rows[0]["jarak"].ToString());
                unit.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();
                unit.BidangMasalah = dt.Rows[0]["bidang_masalah_mitra"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3, asset, omzet;
                decimal.TryParse(dt.Rows[0]["dana_tahun_1"].ToString(), out danaTahun1);
                unit.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[0]["dana_tahun_2"].ToString(), out danaTahun2);
                unit.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[0]["dana_tahun_3"].ToString(), out danaTahun3);
                unit.DanaTahun3 = danaTahun3;
                decimal.TryParse(dt.Rows[0]["asset"].ToString(), out asset);
                unit.Asset = asset;
                decimal.TryParse(dt.Rows[0]["omzet"].ToString(), out omzet);
                unit.Omzet = omzet;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                unit.TglUnggahPernyataan = tglUnggahPernyataan;
            }

            return true;
        }

        public bool insupKelompokSasaran(KelompokSasaran kelompok)
        {
            return insupMitraAbdimas(
                kelompok.IdMitraAbdimas, kelompok.IdUsulanKegiatan, kelompok.KdKategoriMitra, kelompok.IdTipeMitra,
                kelompok.IdJenisMitra, kelompok.NamaPimpinan, null, kelompok.NamaKelompok, kelompok.KdDesa,
                kelompok.Alamat, null, kelompok.BidangPengembangan, null, null, null, null, kelompok.IdMitraReferensi,
                null, null, null, null, null);
        }

        public bool listKelompokSasaran(ref List<KelompokSasaran> listKelompok, Guid idUsulanKegiatan, int idTipeMitra)
        {
            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listKelompok = new List<KelompokSasaran>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var kelompok = new KelompokSasaran();
                kelompok.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                kelompok.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                kelompok.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                kelompok.IdJenisMitra = int.Parse(dt.Rows[i]["id_jenis_mitra"].ToString());
                kelompok.NamaPimpinan = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                kelompok.KdDesa = dt.Rows[i]["kd_desa"].ToString();
                kelompok.Alamat = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                kelompok.BidangPengembangan = dt.Rows[i]["bidang_pengembangan"].ToString();
                kelompok.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                kelompok.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                kelompok.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                kelompok.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                kelompok.TglUnggahPernyataan = tglUnggahPernyataan;

                listKelompok.Add(kelompok);
            }

            return true;
        }

        public bool getKelompokSasaran(ref KelompokSasaran kelompok, Guid idMitraAbdimas)
        {
            DataTable dt = new DataTable();
            if (!getMitra(ref dt, idMitraAbdimas))
            {
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                kelompok = new KelompokSasaran();
                kelompok.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                kelompok.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                kelompok.IdJenisMitra = int.Parse(dt.Rows[0]["id_jenis_mitra"].ToString());
                kelompok.NamaPimpinan = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                kelompok.KdDesa = dt.Rows[0]["kd_desa"].ToString();
                kelompok.Alamat = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                kelompok.BidangPengembangan = dt.Rows[0]["bidang_pengembangan"].ToString();
                kelompok.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[0]["dana_tahun_1"].ToString(), out danaTahun1);
                kelompok.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[0]["dana_tahun_2"].ToString(), out danaTahun2);
                kelompok.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[0]["dana_tahun_3"].ToString(), out danaTahun3);
                kelompok.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                kelompok.TglUnggahPernyataan = tglUnggahPernyataan;

                kelompok.IdMitraReferensi = Guid.Parse(dt.Rows[0]["id_mitra_abdimas_ref"].ToString());
            }

            return true;
        }

        public bool getKelompokSasaranPPMUPT(ref KelompokSasaranPPMUPT kelompok, Guid idMitraAbdimasRef,
            int urutanTahun, int urutanMitra)
        {
            var dt = new DataTable();
            var query = @"select * from pengabdian.get_mitra_abdimas_by_idref(
                            @p_id_mitra_abdimas::uuid,
                            @p_urutan_tahun::int,
                            @p_urutan_mitra::int);";

            if (!this._db.FetchDataTable(query, ref dt
                    , new Npgsql.NpgsqlParameter("@p_id_mitra_abdimas", idMitraAbdimasRef)
                    , new Npgsql.NpgsqlParameter("@p_urutan_tahun", urutanTahun)
                    , new Npgsql.NpgsqlParameter("@p_urutan_mitra", urutanMitra)
                    ))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }

            if (dt.Rows.Count > 0)
            {
                kelompok = new KelompokSasaranPPMUPT();
                kelompok.IdMitraAbdimas = Guid.Parse(dt.Rows[0]["id_mitra_abdimas"].ToString());
                kelompok.KdKategoriMitra = int.Parse(dt.Rows[0]["kd_kategori_mitra"].ToString());
                kelompok.IdTipeMitra = int.Parse(dt.Rows[0]["id_tipe_mitra"].ToString());
                kelompok.IdJenisMitra = int.Parse(dt.Rows[0]["id_jenis_mitra"].ToString());
                kelompok.NamaKelompok = dt.Rows[0]["nama_organisasi_institusi"].ToString();
                kelompok.NamaPimpinan = dt.Rows[0]["nama_pimpinan_mitra"].ToString();
                kelompok.Alamat = dt.Rows[0]["alamat_oganisasi_institusi"].ToString();
                kelompok.BidangPengembangan = dt.Rows[0]["bidang_pengembangan"].ToString();
                kelompok.KdStatusUnggahPernyataan = dt.Rows[0]["kd_sts_surat_pernyataan"].ToString();

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[0]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                kelompok.TglUnggahPernyataan = tglUnggahPernyataan;

                kelompok.IdMitraReferensi = Guid.Parse(dt.Rows[0]["id_mitra_abdimas_ref"].ToString());
                kelompok.UrutanTahun = int.Parse(dt.Rows[0]["urutan_tahun"].ToString());
                kelompok.UrutanMitra = int.Parse(dt.Rows[0]["jarak"].ToString());
            }

            return true;
        }

        public bool insupKelompokSasaranPPMUPT(KelompokSasaranPPMUPT kelompok)
        {
            return insupMitraAbdimas(
                kelompok.IdMitraAbdimas, kelompok.IdUsulanKegiatan, kelompok.KdKategoriMitra, kelompok.IdTipeMitra,
                kelompok.IdJenisMitra, kelompok.NamaPimpinan, null, kelompok.NamaKelompok, null,
                kelompok.Alamat, kelompok.UrutanMitra, kelompok.BidangPengembangan, null, null, null, null, kelompok.IdMitraReferensi,
                null, null, null, null, null, kelompok.UrutanTahun);
        }

        public bool listMitraSasaranPPMUPT(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            var query = $"SELECT * FROM pengabdian.list_mitra_sasaran_ppmupt('{idUsulanKegiatan}');";
            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool listMitraPelaksanaPPPUD(ref List<PemdaPemkot> listMitraPelaksana, Guid idUsulanKegiatan)
        {
            const string ID_TIPE_MITRA_PPUID = "3,4";
            DataTable dt = new DataTable();

            if (!listMitra(ref dt, idUsulanKegiatan, ID_TIPE_MITRA_PPUID))
            {
                return false;
            }

            listMitraPelaksana = new List<PemdaPemkot>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var pemda = new PemdaPemkot();
                pemda.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                pemda.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                pemda.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                pemda.NamaPimpinanInstitusi = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                pemda.Jabatan = dt.Rows[i]["jabatan"].ToString();
                pemda.NamaOrganisasiInstitusi = dt.Rows[i]["nama_organisasi_institusi"].ToString();
                pemda.AlamatInstitusi = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                pemda.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                pemda.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                pemda.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                pemda.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                pemda.TglUnggahPernyataan = tglUnggahPernyataan;

                listMitraPelaksana.Add(pemda);
            }

            return true;
        }

        public bool listMitraSasaranPPPUD(ref List<UMKM> listMitraSasaran, Guid idUsulanKegiatan)
        {
            const string ID_TIPE_MITRA_PPUID = "5,8";
            DataTable dt = new DataTable();

            if (!listMitra(ref dt, idUsulanKegiatan, ID_TIPE_MITRA_PPUID))
            {
                return false;
            }

            listMitraSasaran = new List<UMKM>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                var unit = new UMKM();
                unit.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                unit.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                unit.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                unit.IdJenisMitra = int.Parse(dt.Rows[i]["id_jenis_mitra"].ToString());
                unit.NamaPimpinan = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                unit.NamaUMKM = dt.Rows[i]["nama_organisasi_institusi"].ToString();
                unit.BidangUsaha = dt.Rows[i]["bidang_usaha"].ToString();
                unit.KdDesa = dt.Rows[i]["kd_desa"].ToString();
                unit.Alamat = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                unit.Jarak = int.Parse(dt.Rows[i]["jarak"].ToString());
                unit.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3, asset, omzet;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                unit.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                unit.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                unit.DanaTahun3 = danaTahun3;
                decimal.TryParse(dt.Rows[i]["asset"].ToString(), out asset);
                unit.Asset = asset;
                decimal.TryParse(dt.Rows[i]["omzet"].ToString(), out omzet);
                unit.Omzet = omzet;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                unit.TglUnggahPernyataan = tglUnggahPernyataan;

                listMitraSasaran.Add(unit); ;
            }

            return true;
        }

        public bool listMitraSasaranPKMS(ref List<UMKM> listMitraSasaran, Guid idUsulanKegiatan)
        {
            const string ID_TIPE_MITRA_PPUID = "5,6,7,8";
            DataTable dt = new DataTable();

            if (!listMitra(ref dt, idUsulanKegiatan, ID_TIPE_MITRA_PPUID))
            {
                return false;
            }

            listMitraSasaran = new List<UMKM>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                var unit = new UMKM();
                unit.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                unit.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                unit.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                unit.IdJenisMitra = int.Parse(dt.Rows[i]["id_jenis_mitra"].ToString());
                unit.NamaPimpinan = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                unit.NamaUMKM = dt.Rows[i]["nama_organisasi_institusi"].ToString();
                unit.BidangUsaha = dt.Rows[i]["bidang_usaha"].ToString();
                unit.KdDesa = dt.Rows[i]["kd_desa"].ToString();
                unit.Alamat = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                unit.Jarak = int.Parse(dt.Rows[i]["jarak"].ToString());
                unit.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3, asset, omzet;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                unit.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                unit.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                unit.DanaTahun3 = danaTahun3;
                decimal.TryParse(dt.Rows[i]["asset"].ToString(), out asset);
                unit.Asset = asset;
                decimal.TryParse(dt.Rows[i]["omzet"].ToString(), out omzet);
                unit.Omzet = omzet;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                unit.TglUnggahPernyataan = tglUnggahPernyataan;

                listMitraSasaran.Add(unit); ;
            }

            return true;
        }

        public bool listMitraSasaranKKNPPM(ref List<UMKM> listMitraSasaran, Guid idUsulanKegiatan)
        {
            const int KD_KATEGORI_MITRA_SASARAN = 5;
            DataTable dt = new DataTable();

            if (!listMitraKategori(ref dt, idUsulanKegiatan, KD_KATEGORI_MITRA_SASARAN))
            {
                return false;
            }

            listMitraSasaran = new List<UMKM>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                var unit = new UMKM();
                unit.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                unit.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                unit.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                unit.IdJenisMitra = int.Parse(dt.Rows[i]["id_jenis_mitra"].ToString());
                unit.NamaPimpinan = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                unit.NamaUMKM = dt.Rows[i]["nama_organisasi_institusi"].ToString();
                unit.BidangMasalah = dt.Rows[i]["bidang_masalah_mitra"].ToString();
                unit.KdDesa = dt.Rows[i]["kd_desa"].ToString();
                unit.Alamat = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                unit.Jarak = int.Parse(dt.Rows[i]["jarak"].ToString());
                unit.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                unit.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                unit.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                unit.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                unit.TglUnggahPernyataan = tglUnggahPernyataan;

                listMitraSasaran.Add(unit); ;
            }

            return true;
        }

        private string CheckForNull(int? value)
        {
            return (value == null) ? "NULL" : value.ToString();
        }

        private string CheckForNull(string value)
        {
            return (value == null) ? "NULL" : $"'{value}'";
        }

        private string CheckForNull(Guid? value)
        {
            return (value == null) ? "NULL" : $"'{value}'";
        }

        private string CheckForNull(decimal? value)
        {
            return (value == null) ? "NULL" : value.ToString();
        }

        public bool insupMitraAbdimas(
            Guid id_mitra_abdimas,
            Guid id_usulan_kegiatan,
            int kd_kategori_mitra,
            int id_tipe_mitra,
            int? id_jenis_mitra,
            string nama_pimpinan_mitra,
            string jabatan,
            string nama_organisasi_institusi,
            string kd_desa,
            string alamat_oganisasi_institusi,
            int? jarak,
            string bidang_pengembangan,
            Guid? id_institusi,
            string bidang_usaha,
            decimal? asset,
            decimal? omzet,
            Guid? id_mitra_abdimas_ref,
            decimal? dana_tahun_1,
            decimal? dana_tahun_2,
            decimal? dana_tahun_3,
            string bidang_masalah_mitra,
            int? lama_kegiatan,
            int? urutan_tahun=null
        )
        {
            string strSQL = $@"select * from pengabdian.insup_mitra_abdimas_ppdm(
                                  '{id_mitra_abdimas}'::uuid,
                                  '{id_usulan_kegiatan}'::uuid,
                                  {kd_kategori_mitra}::integer,
                                  {id_tipe_mitra}::integer,
                                  {CheckForNull(id_jenis_mitra)}::integer,
                                  {CheckForNull(nama_pimpinan_mitra)}::text,
                                  {CheckForNull(jabatan)}::text,
                                  {CheckForNull(nama_organisasi_institusi)}::text,
                                  {CheckForNull(kd_desa)}::char(10),
                                  {CheckForNull(alamat_oganisasi_institusi)}::text,
                                  {CheckForNull(jarak)}::integer,
                                  {CheckForNull(bidang_pengembangan)}::text,
                                  {CheckForNull(id_institusi)}::uuid,
                                  {CheckForNull(bidang_usaha)}::varchar,
                                  {CheckForNull(asset)}::money,
                                  {CheckForNull(omzet)}::money,
                                  {CheckForNull(id_mitra_abdimas_ref)}::uuid,
                                  {CheckForNull(dana_tahun_1)}::money,
                                  {CheckForNull(dana_tahun_2)}::money,
                                  {CheckForNull(dana_tahun_3)}::money,
                                  {CheckForNull(bidang_masalah_mitra)}::text,
                                  {CheckForNull(urutan_tahun)}::smallint
                             );";

            DataTable dt = new DataTable();
            if (!this._db.ExecuteNonQuery(strSQL.Replace("\r\n","")))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }
            //var idJenisMitra = new Npgsql.NpgsqlParameter("@p_id_jenis_mitra", DbType.Int32);
            //idJenisMitra.Value = (id_jenis_mitra == null) ? (object)DBNull.Value : id_jenis_mitra;
            //                   //new Npgsql.NpgsqlParameter("@p_id_jenis_mitra", DBNull.Value) :
            //                   //new Npgsql.NpgsqlParameter("@p_id_jenis_mitra", (int)id_jenis_mitra);
            //var namaPimpinan = string.IsNullOrEmpty(nama_pimpinan_mitra) ?
            //                   new Npgsql.NpgsqlParameter("@p_nama_pimpinan_mitra", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_nama_pimpinan_mitra", nama_pimpinan_mitra);
            //var jabatanPimpinan = string.IsNullOrEmpty(jabatan) ?
            //                   new Npgsql.NpgsqlParameter("@p_jabatan", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_jabatan", jabatan);
            //var namaOrganisasi = string.IsNullOrEmpty(nama_organisasi_institusi) ?
            //                   new Npgsql.NpgsqlParameter("@p_nama_organisasi_institusi", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_nama_organisasi_institusi", nama_organisasi_institusi);
            //var kdDesa = string.IsNullOrEmpty(kd_desa) ?
            //                   new Npgsql.NpgsqlParameter("@p_kd_desa", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_kd_desa", kd_desa);
            //var alamatOrganisasi = string.IsNullOrEmpty(alamat_oganisasi_institusi) ?
            //                   new Npgsql.NpgsqlParameter("@p_alamat_oganisasi_institusi", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_alamat_oganisasi_institusi", alamat_oganisasi_institusi);
            //var jarakMitra = (jarak == null) ?
            //                   new Npgsql.NpgsqlParameter("@p_jarak", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_jarak", jarak);
            //var bidangPengembangan = string.IsNullOrEmpty(bidang_pengembangan) ?
            //                   new Npgsql.NpgsqlParameter("@p_bidang_pengembangan", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_bidang_pengembangan", bidang_pengembangan);
            //var idInstitusi = (id_institusi == null) ?
            //                   new Npgsql.NpgsqlParameter("@p_id_institusi", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_id_institusi", jarak);
            //var bidangUsaha = string.IsNullOrEmpty(bidang_usaha) ?
            //                   new Npgsql.NpgsqlParameter("@p_bidang_usaha", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_bidang_usaha", bidang_usaha);
            //var assetMitra = (asset == null) ?
            //                   new Npgsql.NpgsqlParameter("@p_asset", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_asset", asset);
            //var omzetMitra = (omzet == null) ?
            //                   new Npgsql.NpgsqlParameter("@p_omzet", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_omzet", omzet);
            //var idMitraAbdimasRef = (id_mitra_abdimas == null) ?
            //                   new Npgsql.NpgsqlParameter("@p_id_mitra_abdimas_ref", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_id_mitra_abdimas_ref", id_mitra_abdimas);
            //var danaTahun1 = (dana_tahun_1 == null) ?
            //                   new Npgsql.NpgsqlParameter("@p_dana_tahun_1", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_dana_tahun_1", dana_tahun_1);
            //var danaTahun2 = (dana_tahun_2 == null) ?
            //                   new Npgsql.NpgsqlParameter("@p_dana_tahun_2", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_dana_tahun_2", dana_tahun_2);
            //var danaTahun3 = (dana_tahun_3 == null) ?
            //                   new Npgsql.NpgsqlParameter("@p_dana_tahun_3", DBNull.Value) :
            //                   new Npgsql.NpgsqlParameter("@p_dana_tahun_3", dana_tahun_3);

            //if (!this._db.FetchDataTable(strSQL, ref dt
            //        , new Npgsql.NpgsqlParameter("@p_id_mitra_abdimas", id_mitra_abdimas)
            //        , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", id_usulan_kegiatan)
            //        , new Npgsql.NpgsqlParameter("@p_kd_kategori_mitra", kd_kategori_mitra)
            //        , new Npgsql.NpgsqlParameter("@p_id_tipe_mitra", id_tipe_mitra)
            //        , idJenisMitra
            //        , namaPimpinan
            //        , jabatanPimpinan
            //        , namaOrganisasi
            //        , kdDesa
            //        , alamatOrganisasi
            //        , jarakMitra
            //        , bidangPengembangan
            //        , idInstitusi
            //        , bidangUsaha
            //        , assetMitra
            //        , omzetMitra
            //        , idMitraAbdimasRef
            //        , danaTahun1
            //        , danaTahun2
            //        , danaTahun3
            //    ))
            //{
            //    _errorMessage = _db.ErrorMessage;
            //    return false;
            //}            

            if (lama_kegiatan != null)
            {
                decimal?[] danaMitra = { dana_tahun_1, dana_tahun_2, dana_tahun_3 };

                for (int i = 1; i <= lama_kegiatan; i++)
                {
                    var query = @"SELECT * FROM pengabdian.insup_dana_mitra_abdimas (
                                @p_id_mitra_abdimas::UUID,
                                @p_thn_urutan_kegiatan::INTEGER,
                                @p_kontribusi_dana::MONEY);";
                    var dtDana = new DataTable();
                    if (!this._db.ExecuteNonQuery(query
                            , new Npgsql.NpgsqlParameter("@p_id_mitra_abdimas", id_mitra_abdimas)
                            , new Npgsql.NpgsqlParameter("@p_thn_urutan_kegiatan", i)
                            , new Npgsql.NpgsqlParameter("@p_kontribusi_dana", danaMitra[i - 1])
                       ))
                    {
                        _errorMessage = _db.ErrorMessage;
                        return false;
                    }
                }
            }

            return true;
        }

        private bool listMitraKategori(ref DataTable dt, Guid idUsulanKegiatan, int kdKategoriMitra)
        {
            var query = @"select * from pengabdian.list_mitra_abdimas_kategori(
                            @id_usulan_kegiatan::uuid,
                            @kd_kategori_mitra::integer);";

            if (!this._db.FetchDataTable(query, ref dt
                    , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@kd_kategori_mitra", kdKategoriMitra)
                    ))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }

            return true;
        }

        private bool listMitra(ref DataTable dt, Guid idUsulanKegiatan, int idTipeMitra)
        {
            var query = @"select * from pengabdian.list_mitra_abdimas(
                            @id_usulan_kegiatan::uuid,
                            @id_tipe_mitra::integer);";

            if (!this._db.FetchDataTable(query, ref dt
                    , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@id_tipe_mitra", idTipeMitra)
                    ))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }

            return true;
        }

        private bool listMitra(ref DataTable dt, Guid idUsulanKegiatan, string idTipeMitraArray)
        {
            var query = @"select * from pengabdian.list_mitra_abdimas(
                            @id_usulan_kegiatan::uuid,
                            @id_tipe_mitra::text);";

            if (!this._db.FetchDataTable(query, ref dt
                    , new Npgsql.NpgsqlParameter("@id_usulan_kegiatan", idUsulanKegiatan)
                    , new Npgsql.NpgsqlParameter("@id_tipe_mitra", idTipeMitraArray)
                    ))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }

            return true;
        }

        private bool getMitra(ref DataTable dt, Guid idMitraAbdimas)
        {
            var query = @"select * from pengabdian.get_mitra_abdimas(
                            @p_id_mitra_abdimas::uuid);";

            if (!this._db.FetchDataTable(query, ref dt
                    , new Npgsql.NpgsqlParameter("@p_id_mitra_abdimas", idMitraAbdimas)))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }
            return true;
        }

        public bool hapusMitra(Guid idMitra)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pengabdian.del_mitra_abdimas_rb(@p_id_mitra_abdimas);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_mitra_abdimas", idMitra));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool hapusMitrakelmasppdm(Guid idMitra)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pengabdian.del_mitra_kelmas_ppdm(@p_id_mitra_abdimas);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_mitra_abdimas", idMitra));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updateStsDokMitra(Guid idMitra)
        {
            bool isSuccess = false;
            string strSQL = string.Format("UPDATE pengabdian.mitra_abdimas SET kd_sts_surat_pernyataan ='1', tgl_unggah_pernyataan=now() WHERE id_mitra_abdimas ='{0}'", idMitra);
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getMitraKelompokibdm(ref DataTable dataTable, string idUsulanKegiatan, string urutantahun, string abdimitraabdimasref)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pengabdian.list_mitra_abdimas_kelompok_ppdm('{0}'::uuid,{1}::smallint,'{2}'::uuid);", idUsulanKegiatan, urutantahun, abdimitraabdimasref);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getMitraKelompokMasyibdm(ref DataTable dataTable, string idUsulanKegiatan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pengabdian.list_mitra_kelmas_ppdm('{0}'::uuid);", idUsulanKegiatan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listPemdaPemkotCSR(ref List<PemdaPemkot> listPemda, Guid idUsulanKegiatan, string idTipeMitra)
        {
            DataTable dt = new DataTable();
            if (!listMitra(ref dt, idUsulanKegiatan, idTipeMitra))
            {
                return false;
            }

            listPemda = new List<PemdaPemkot>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var pemda = new PemdaPemkot();
                pemda.IdMitraAbdimas = Guid.Parse(dt.Rows[i]["id_mitra_abdimas"].ToString());
                pemda.KdKategoriMitra = int.Parse(dt.Rows[i]["kd_kategori_mitra"].ToString());
                pemda.IdTipeMitra = int.Parse(dt.Rows[i]["id_tipe_mitra"].ToString());
                pemda.NamaPimpinanInstitusi = dt.Rows[i]["nama_pimpinan_mitra"].ToString();
                pemda.Jabatan = dt.Rows[i]["jabatan"].ToString();
                pemda.NamaOrganisasiInstitusi = dt.Rows[i]["nama_organisasi_institusi"].ToString();
                pemda.AlamatInstitusi = dt.Rows[i]["alamat_oganisasi_institusi"].ToString();
                pemda.KdStatusUnggahPernyataan = dt.Rows[i]["kd_sts_surat_pernyataan"].ToString();

                decimal danaTahun1, danaTahun2, danaTahun3;
                decimal.TryParse(dt.Rows[i]["dana_tahun_1"].ToString(), out danaTahun1);
                pemda.DanaTahun1 = danaTahun1;
                decimal.TryParse(dt.Rows[i]["dana_tahun_2"].ToString(), out danaTahun2);
                pemda.DanaTahun2 = danaTahun2;
                decimal.TryParse(dt.Rows[i]["dana_tahun_3"].ToString(), out danaTahun3);
                pemda.DanaTahun3 = danaTahun3;

                DateTime tglUnggahPernyataan;
                DateTime.TryParse(dt.Rows[i]["tgl_unggah_pernyataan"].ToString(), out tglUnggahPernyataan);
                pemda.TglUnggahPernyataan = tglUnggahPernyataan;

                listPemda.Add(pemda);
            }

            return true;
        }
        

        #endregion
    }
}