using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Pengusul
{
    public class luaran : _abstractModels
    {
        #region Variable Class

        #endregion

        #region Konstruktor dan Destruktor

        public luaran()
        {
            setInitValues();
        }

        ~luaran()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool ListTargetLuaranWajib(ref DataTable dataTable, Guid id_usulan)
        {
            bool isSuccess = false;
            // string strSQL = @"SELECT * FROM penelitian.list_janji_luaran_wajib_xiia(@p_id_usulan);";
            string strSQL = @"SELECT * FROM penelitian.list_luaran_wajib_dijanjikan_dasar_xii(@p_id_usulan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getKategoriLuaranTerapanPengembangan(ref DataTable dataTable, Guid p_id_usulan)
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.get_kategori_luaran_wajib_terapan_xii(@p_id_usulan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", p_id_usulan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool getLuaranPublikasi(ref DataTable dataTable, Guid p_id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = @"select nama_jurnal, id_target_capaian_luaran from penelitian.get_luaran_publikasi((@p_id_luaran_dijanjikan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", p_id_luaran_dijanjikan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listLuaranPengabdian(ref DataTable dataTable, Guid id_usulan, int p_id_kelompok)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_luaran_usulan_xii(@p_id_usulan, @p_id_kelompok);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok", p_id_kelompok)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListTargetLuaranWajibPengabdian(ref DataTable dataTable, Guid id_usulan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_janji_luaran_wajib_xii(@p_id_usulan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListTargetLuaranTambahan(ref DataTable dataTable, Guid id_usulan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_luaran_tambahan_dijanjikan_xii(@p_id_usulan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool ListTargetLuaranTambahanPengabdian2019(ref DataTable dataTable, Guid id_usulan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_luaran_tambahan_dijanjikan_xii(@p_id_usulan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool ListTargetLuaranTambahanPengabdian(ref DataTable dataTable, Guid id_usulan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_janji_luaran_tambahan_xii(@p_id_usulan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListLuaranWajib(ref DataTable dataTable, Guid id_usulan, int tahun_ke, int tkt_target)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_luaran_wajib_xii_rb(@p_id_usulan::uuid,@p_tahun_ke::smallint,@p_tkt_target::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", tahun_ke)
            , new Npgsql.NpgsqlParameter("@p_tkt_target", tkt_target)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool ListLuaranWajib_xiia_rb(ref DataTable dataTable, Guid id_usulan, int tahun_ke, int tkt_target)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT distinct kd_kategori_jenis_luaran, nama_kategori_jenis_luaran  
                FROM penelitian.list_luaran_wajib_xiia_rb(@p_id_usulan::uuid,@p_tahun_ke::smallint, 
                @p_tkt_target::smallint) order by kd_kategori_jenis_luaran asc;";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", tahun_ke)
            , new Npgsql.NpgsqlParameter("@p_tkt_target", tkt_target)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listKategoriLuaran_xiia(ref DataTable dataTable, int id_skema,int id_kelompok_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.list_kategori_luaran_xiia ({0},{1});";
            strSQL = string.Format(strSQL, id_skema,id_kelompok_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool listKategoriLuaranTambahanAbdimas_xiia(ref DataTable dataTable)
        {
            bool isSuccess = false;
            string strSQL = @"select * from pengabdian.list_kategori_luaran_tambahan_xiia();";
            strSQL = string.Format(strSQL);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool getKategoriLuaranTambahanAbdimas_xiia(ref DataTable dataTable, string p_id_usulan)
        {
            bool isSuccess = false;
            string strSQL = @"select * from pengabdian.get_kategori_luaran_tambahan_xii('{0}'::uuid);";
            strSQL = string.Format(strSQL, p_id_usulan);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool ListJenisLuaran2(ref DataTable dataTable, string p_kd_kategori_jenis_luaran, int id_skema, int id_kelompok_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.list_jenis_luaran_xiia ('{0}',{1},{2});";
            strSQL = string.Format(strSQL, p_kd_kategori_jenis_luaran, id_skema,id_kelompok_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListJenisLuaran(ref DataTable dataTable, string p_kd_kategori_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"select id_jenis_luaran, nama_jenis_luaran from penelitian.list_jenis_luaran (@p_kd_kategori_jenis_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_kd_kategori_jenis_luaran", p_kd_kategori_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListJenisLuaranTambahanAbdimas(ref DataTable dataTable, string p_kd_kategori_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"select * from pengabdian.list_jenis_luaran_tambahan('{0}');";
            strSQL = string.Format(strSQL, p_kd_kategori_jenis_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool ListTargetStatusLuaran(ref DataTable dataTable, string p_kd_kategori_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"select id_target_capaian_luaran, nama_target_capaian_luaran from referensi.list_target_capaian_luaran(@p_kd_kategori_jenis_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_kd_kategori_jenis_luaran", p_kd_kategori_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool ListTargetStatusLuaranPengabdian(ref DataTable dataTable, string p_id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"select * from pengabdian.list_target_luaran (@p_id_jenis_luaran::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListTargetStatusLuaranTambahanPengabdian(ref DataTable dataTable, string p_id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"select * from pengabdian.list_target_luaran (@p_id_jenis_luaran::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool ListTargetStatusLuaranTambahanPengabdian_xii(ref DataTable dataTable, string p_kd_kategori_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"select * from pengabdian.list_target_luaran_tambahan_xii(@p_kd_kategori_jenis_luaran::char);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_kd_kategori_jenis_luaran", p_kd_kategori_jenis_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListInfoBuktiLuaran(ref DataTable dataTable, string p_kd_kategori_jenis_luaran,
            int p_tahun_ke,
            int p_id_target_capaian_luaran,
            int p_id_skema,
            int p_id_kelompok_luaran
            )
        {
            bool isSuccess = false;
            string strSQL = @"select * from hibah.get_info_bukti_luaran_26_juli(
                                @p_id_jenis_luaran::smallint,
                                @p_tahun_ke::smallint,
                                @p_id_target_capaian_luaran::integer,
                                @p_id_skema::integer,
                                @p_id_kelompok_luaran::integer);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_kd_kategori_jenis_luaran)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", p_tahun_ke)
            , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_skema", p_id_skema)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool ListInfoBuktiLuaranAbdimas(ref DataTable dataTable, string p_id_jenis_luaran,
            int p_id_target_capaian_luaran
            )
        {
            bool isSuccess = false;
            string strSQL = @"select * from pengabdian.get_info_bukti_luaran_tambahan(
                                @p_id_jenis_luaran::smallint,
                                @p_id_target_capaian_luaran::integer);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListInfoBuktiLuaranAbdimas(ref DataTable dataTable, string p_kd_kategori_jenis_luaran,
            int p_tahun_ke,
            int p_id_target_capaian_luaran,
            int p_id_skema,
            int p_id_kelompok_luaran
            )
        {
            bool isSuccess = false;
            string strSQL = @"select * from pengabdian.get_info_bukti_luaran_26_juli(
                                @p_id_jenis_luaran::smallint,
                                @p_tahun_ke::smallint,
                                @p_id_target_capaian_luaran::integer,
                                @p_id_skema::integer,
                                @p_id_kelompok_luaran::integer);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_kd_kategori_jenis_luaran)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", p_tahun_ke)
            , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_skema", p_id_skema)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }



        public bool insupLuaranWajibDijanjikanDasar_xii
        (
             Guid p_id_luaran_dijanjikan,
             Guid p_id_usulan,
             int p_id_jenis_luaran,
             int p_id_kelompok_luaran,
             int p_urutan_thn_usulan_kegiatan,
             int p_id_target_capaian_luaran,
             string[] p_keterangan
        )
        {
            bool isSuccess = false;
            Npgsql.NpgsqlParameter param = new Npgsql.NpgsqlParameter("p_keterangan", NpgsqlDbType.Array | NpgsqlDbType.Text);
            param.Value = p_keterangan;
            string strSQL = @"select * from penelitian.insert_luaran_dijanjikan_penelitian_dasar_xii (      
                            @p_id_luaran_dijanjikan::uuid,
                            @p_id_usulan::uuid,
                            @p_id_jenis_luaran::integer,
                            @p_id_kelompok_luaran::integer,
                            @p_urutan_thn_usulan_kegiatan::smallint,
                            @p_id_target_capaian_luaran::integer,
	                        @p_keterangan::text[] 
                            );";

            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", p_id_luaran_dijanjikan)
                    , new Npgsql.NpgsqlParameter("@p_id_usulan", p_id_usulan)
                    , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
                    , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
                    , new Npgsql.NpgsqlParameter("@p_urutan_thn_usulan_kegiatan", p_urutan_thn_usulan_kegiatan)
                    , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
                    , param
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




        public bool ListLuaranWajibPengabdian(ref DataTable dataTable, Guid id_usulan, int tahun_ke)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_luaran_wajib_xii(@p_id_usulan::uuid,@p_tahun_ke::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", tahun_ke)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListJenisLuaranWajib(ref DataTable dataTable, Guid id_usulan, int tahun_ke, int tkt_target)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT distinct nama_jenis_luaran,id_jenis_luaran FROM penelitian.list_luaran_wajib_xii_rb(@p_id_usulan::uuid,@p_tahun_ke::smallint,@p_tkt_target::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", tahun_ke)
            , new Npgsql.NpgsqlParameter("@p_tkt_target", tkt_target)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListJenisLuaranPengabdian(ref DataTable dataTable, string p_kd_kategori_jenis_luaran, int p_id_skema, int p_id_kelompok_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"select * from pengabdian.list_jenis_luaran_xii(@p_kd_kategori_jenis_luaran::char,@p_id_skema::integer,@p_id_kelompok_luaran::integer) ;");
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_kd_kategori_jenis_luaran", p_kd_kategori_jenis_luaran)
            , new Npgsql.NpgsqlParameter("@p_id_skema", p_id_skema)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListJenisLuaranWajibPengabdian(ref DataTable dataTable, Guid id_usulan, int tahun_ke, string id)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT distinct nama_jenis_luaran,id_jenis_luaran FROM pengabdian.list_luaran_wajib_xii(@p_id_usulan::uuid,@p_tahun_ke::smallint) where id_kategori={0};", id);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", tahun_ke)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListTargetLuaranWajib(ref DataTable dataTable, Guid id_usulan, int tahun_ke, int tkt_target, int id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM penelitian.list_luaran_wajib_xii_rb(@p_id_usulan::uuid,@p_tahun_ke::smallint,@p_tkt_target::smallint) where id_jenis_luaran={0};", id_jenis_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", tahun_ke)
            , new Npgsql.NpgsqlParameter("@p_tkt_target", tkt_target)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListTargetLuaranWajibPengabdian(ref DataTable dataTable, Guid id_usulan, int tahun_ke, int id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = string.Format(@"SELECT * FROM pengabdian.list_luaran_wajib_xii(@p_id_usulan::uuid,@p_tahun_ke::smallint) where id_jenis_luaran={0};", id_jenis_luaran);
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", tahun_ke)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool deleteLuaranDijanjikan_xii(string id_luaran_dijanjikan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from penelitian.del_luaran_dijanjikan_xii ('{0}'::uuid);", id_luaran_dijanjikan);
            string tes = strSQL;
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteLuaranDijanjikanAbdimas_xii(string id_luaran_dijanjikan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"select * from pengabdian.del_luaran_dijanjikan_xii ('{0}'::uuid);", id_luaran_dijanjikan);
            string tes = strSQL;
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteJanjiLuaran(string id_luaran_dijanjikan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"delete from penelitian.luaran_dijanjikan_xii where id_luaran_dijanjikan='{0}';", id_luaran_dijanjikan);
            string tes = strSQL;
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool deleteJanjiLuaranpengabdiian(string id_luaran_dijanjikan)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"delete from pengabdian.luaran_dijanjikan_xii where id_luaran_dijanjikan='{0}';", id_luaran_dijanjikan);
            string tes = strSQL;
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteJanjiLuaranprosiding(string id_usulan, string tahun_ke)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"delete from penelitian.luaran_dijanjikan_xii where id_usulan='{0}' and id_kelompok_luaran='1' and id_jenis_luaran in (3,4,22) and urutan_thn_usulan_kegiatan='{1}';", id_usulan, tahun_ke);
            string tes = strSQL;
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool deleteJanjiLuaranbookchapter(string id_usulan, string tahun_ke)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"delete from penelitian.luaran_dijanjikan_xii where id_usulan='{0}' and id_kelompok_luaran='1' and id_jenis_luaran in (39) and urutan_thn_usulan_kegiatan='{1}';", id_usulan, tahun_ke);
            string tes = strSQL;
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool deleteJanjiLuaranpublikasi(string id_usulan, string tahun_ke)
        {
            bool isSuccess = false;

            string strSQL = string.Format(@"delete from penelitian.luaran_dijanjikan_xii where id_usulan='{0}' and id_kelompok_luaran='1' and id_jenis_luaran in (2) and urutan_thn_usulan_kegiatan='{1}';", id_usulan, tahun_ke);
            string tes = strSQL;
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool infoluaran(ref DataTable dataTable, string id_usulan_kegiatan)
        {
            bool isSuccess = false;

            string strSQL = string.Format("select * from hibah.list_info_luaran('{0}');", id_usulan_kegiatan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListLuaranTambahan(ref DataTable dataTable, Guid id_usulan, int tahun_ke)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_luaran_tambahan_xii(@p_id_usulan::uuid,@p_tahun_ke::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", tahun_ke)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool ListLuaranTambahanPengabdian(ref DataTable dataTable, Guid id_usulan, int tahun_ke)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_luaran_tambahan_xii(@p_id_usulan::uuid,@p_tahun_ke::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan", id_usulan)
            , new Npgsql.NpgsqlParameter("@p_tahun_ke", tahun_ke)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupluaran(string p_id_usulan, string p_urutan_thn_usulan_kegiatan, string p_id_jenis_luaran, string p_id_target_capaian_luaran, string p_volume, string p_id_kelompok_luaran, string p_keterangan)
        {
            bool isSuccess = false;
            string strSQL = @"select penelitian.insup_luaran_dijanjikan_xii(@p_id_usulan::uuid, @p_urutan_thn_usulan_kegiatan::smallint, " +
                             "@p_id_jenis_luaran::integer,@p_id_target_capaian_luaran::integer,@p_volume::smallint," +
                             "@p_id_kelompok_luaran::integer,@p_keterangan::character varying)";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter("@p_id_usulan", p_id_usulan)
                            , new Npgsql.NpgsqlParameter("@p_urutan_thn_usulan_kegiatan", p_urutan_thn_usulan_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
                            , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
                            , new Npgsql.NpgsqlParameter("@p_volume", p_volume)
                            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
                            , new Npgsql.NpgsqlParameter("@p_keterangan", p_keterangan)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insupluaranPengabdian(string p_id_luaran_dijanjikan, string p_id_usulan, string p_urutan_thn_usulan_kegiatan, string p_id_jenis_luaran, string p_id_target_capaian_luaran, string p_volume, string p_id_kelompok_luaran, string p_keterangan)
        {
            bool isSuccess = false;
            string strSQL = @"select pengabdian.insup_luaran_dijanjikan_pengabdian_xii(@p_id_luaran_dijanjikan::uuid, @p_id_usulan::uuid, @p_urutan_thn_usulan_kegiatan::smallint, " +
                             "@p_id_jenis_luaran::integer,@p_id_target_capaian_luaran::integer,@p_volume::smallint," +
                             "@p_id_kelompok_luaran::integer,@p_keterangan::character varying)";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                            , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", p_id_luaran_dijanjikan)
                            , new Npgsql.NpgsqlParameter("@p_id_usulan", p_id_usulan)
                            , new Npgsql.NpgsqlParameter("@p_urutan_thn_usulan_kegiatan", p_urutan_thn_usulan_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
                            , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
                            , new Npgsql.NpgsqlParameter("@p_volume", p_volume)
                            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
                            , new Npgsql.NpgsqlParameter("@p_keterangan", p_keterangan)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool insupluaranPengabdian(string p_id_usulan, string p_urutan_thn_usulan_kegiatan, string p_id_jenis_luaran, string p_id_target_capaian_luaran, string p_volume, string p_id_kelompok_luaran, string p_keterangan)
        {
            bool isSuccess = false;
            string strSQL = @"select pengabdian.insup_luaran_dijanjikan_pengabdian_xii(@p_id_usulan::uuid, @p_urutan_thn_usulan_kegiatan::smallint, " +
                             "@p_id_jenis_luaran::integer,@p_id_target_capaian_luaran::integer,@p_volume::smallint," +
                             "@p_id_kelompok_luaran::integer,@p_keterangan::character varying)";
            isSuccess = this._db.ExecuteNonQuery(strSQL     
                            , new Npgsql.NpgsqlParameter("@p_id_usulan", p_id_usulan)
                            , new Npgsql.NpgsqlParameter("@p_urutan_thn_usulan_kegiatan", p_urutan_thn_usulan_kegiatan)
                            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
                            , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
                            , new Npgsql.NpgsqlParameter("@p_volume", p_volume)
                            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_id_kelompok_luaran)
                            , new Npgsql.NpgsqlParameter("@p_keterangan", p_keterangan)
                );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool ListTargetLuaran(ref DataTable dataTable, int id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.list_target_luaran_tambahan(@p_id_jenis_luaran::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", id_jenis_luaran)

            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool ListTargetLuaranPengabdian_xii(ref DataTable dataTable, int id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_target_luaran(@p_id_jenis_luaran::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", id_jenis_luaran)

            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool ListTargetLuaranPengabdian(ref DataTable dataTable, int id_jenis_luaran)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM pengabdian.list_target_luaran_tambahan(@p_id_jenis_luaran::smallint);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", id_jenis_luaran)

            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool rekapLuaran(ref DataTable dataTable, Guid id_personal_ketua, string thnPelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.rekap_luaran_dijanjikan(@p_id_personal, @p_thn_pelaksanaan_kegiatan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal_ketua)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool rekapLuaranPerbaikan(ref DataTable dataTable, Guid id_personal_ketua, string thnPelaksanaan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.rekap_luaran_dijanjikan_perbaikan(@p_id_personal, @p_thn_pelaksanaan_kegiatan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_personal", id_personal_ketua)
            , new Npgsql.NpgsqlParameter("@p_thn_pelaksanaan_kegiatan", thnPelaksanaan)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool detailLuaran(ref DataTable dataTable, Guid idUsulanKegiatan, int idKelompokLuaran)
        {
            bool isSuccess = false;
            //string strSQL = @"SELECT * FROM penelitian.detil_luaran_dijanjikan(@p_id_usulan_kegiatan, @p_id_kelompok_luaran);";
            string strSQL = @"SELECT * FROM penelitian.detil_luaran_dijanjikan_by_status(@p_id_usulan_kegiatan, @p_id_kelompok_luaran);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
            , new Npgsql.NpgsqlParameter("@p_id_usulan_kegiatan", idUsulanKegiatan)
            , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", idKelompokLuaran)
            );
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool insupLuaranPublikasiJurnal
        (
            Guid id_luaran_publikasi_jurnal,
            Guid id_luaran_dijanjikan,
            string tahunPublikasi,
            string kdJenisPublikasiJurnal,
            string judul,
            string namaJurnal,
            string volume,
            string nomor,
            string url,
            Guid id_surat_keterangan,
            Guid id_artikel,
            string issn,
            string halamanAwal,
            string halamanAkhir,
            string doi
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_luaran_publikasi_jurnal_rb (      
                            @p_id_luaran_publikasi_jurnal::uuid,
                            @p_id_luaran_dijanjikan::uuid,
	                        @p_thn_publikasi::character(4),
	                        @p_kd_jenis_publikasi_jurnal::character(1),
	                        @p_judul::text,
	                        @p_nama_jurnal::text,
	                        @p_volume::character varying,
	                        @p_nomor::character varying,
	                        @p_url::text,
                            @p_id_surat_keterangan::uuid,
                            @p_id_artikel::uuid,
	                        @p_issn::character varying,
	                        @p_halaman_awal::character varying,
	                        @p_halaman_akhir::character varying,
                            @p_doi::character varying
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_publikasi_jurnal", id_luaran_publikasi_jurnal)
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", id_luaran_dijanjikan)
                    , new Npgsql.NpgsqlParameter("@p_thn_publikasi", tahunPublikasi)
                    , new Npgsql.NpgsqlParameter("@p_kd_jenis_publikasi_jurnal", kdJenisPublikasiJurnal)
                    , new Npgsql.NpgsqlParameter("@p_judul", judul)
                    , new Npgsql.NpgsqlParameter("@p_nama_jurnal", namaJurnal)
                    , new Npgsql.NpgsqlParameter("@p_volume", volume)
                    , new Npgsql.NpgsqlParameter("@p_nomor", nomor)
                    , new Npgsql.NpgsqlParameter("@p_url", url)
                    , new Npgsql.NpgsqlParameter("@p_id_surat_keterangan", id_surat_keterangan)
                    , new Npgsql.NpgsqlParameter("@p_id_artikel", id_artikel)
                    , new Npgsql.NpgsqlParameter("@p_issn", issn)
                    , new Npgsql.NpgsqlParameter("@p_halaman_awal", halamanAwal)
                    , new Npgsql.NpgsqlParameter("@p_halaman_akhir", halamanAkhir)
                    , new Npgsql.NpgsqlParameter("@p_doi", doi)
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

        public bool insupLuaranPublikasiProsiding
        (
            Guid id_luaran_publikasi_jurnal,
            Guid id_luaran_dijanjikan,
            string tahunPublikasi,
            string kdJenisPublikasiJurnal,
            string judul,
            string namaJurnal,
            string volume,
            string nomor,
            string url,
            Guid id_surat_keterangan,
            Guid id_artikel,
            string issn,
            string halamanAwal,
            string halamanAkhir
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_luaran_publikasi_prosiding_rb (      
                            @p_id_luaran_publikasi_jurnal::uuid,
                            @p_id_luaran_dijanjikan::uuid,
	                        @p_thn_publikasi::character(4),
	                        @p_kd_jenis_publikasi_jurnal::character(1),
	                        @p_judul::text,
	                        @p_nama_jurnal::text,
	                        @p_volume::character varying,
	                        @p_nomor::character varying,
	                        @p_url::text,
                            @p_id_surat_keterangan::uuid,
                            @p_id_artikel::uuid,
	                        @p_issn::character varying,
	                        @p_halaman_awal::character varying,
	                        @p_halaman_akhir::character varying
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_publikasi_jurnal", id_luaran_publikasi_jurnal)
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", id_luaran_dijanjikan)
                    , new Npgsql.NpgsqlParameter("@p_thn_publikasi", tahunPublikasi)
                    , new Npgsql.NpgsqlParameter("@p_kd_jenis_publikasi_jurnal", kdJenisPublikasiJurnal)
                    , new Npgsql.NpgsqlParameter("@p_judul", judul)
                    , new Npgsql.NpgsqlParameter("@p_nama_jurnal", namaJurnal)
                    , new Npgsql.NpgsqlParameter("@p_volume", volume)
                    , new Npgsql.NpgsqlParameter("@p_nomor", nomor)
                    , new Npgsql.NpgsqlParameter("@p_url", url)
                    , new Npgsql.NpgsqlParameter("@p_id_surat_keterangan", id_surat_keterangan)
                    , new Npgsql.NpgsqlParameter("@p_id_artikel", id_artikel)
                    , new Npgsql.NpgsqlParameter("@p_issn", issn)
                    , new Npgsql.NpgsqlParameter("@p_halaman_awal", halamanAwal)
                    , new Npgsql.NpgsqlParameter("@p_halaman_akhir", halamanAkhir)))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool getLuaranPublikasiJurnal(ref DataTable dataTable, Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.get_luaran_publikasi_jurnal(@p_id_luaran_dijanjikan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", id_luaran_dijanjikan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getLuaranDijanjikan(ref DataTable dataTable, Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT keterangan, id_target_capaian_luaran FROM penelitian.get_luaran_dijanjikani(@p_id_luaran_dijanjikan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", id_luaran_dijanjikan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool updateStatusUnggahPublikasiJurnal(Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            var strSQL = $@"UPDATE penelitian.luaran_publikasi_jurnal SET kd_sts_berkas_jurnal='1'
                            WHERE id_luaran_dijanjikan ='{id_luaran_dijanjikan}'";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getLuaranBuku(ref DataTable dataTable, Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.get_luaran_buku(@id_luaran_dijanjikan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@id_luaran_dijanjikan", id_luaran_dijanjikan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupLuaranBuku
        (
            Guid id_luaran_buku,
            Guid id_luaran_dijanjikan,
            string kd_jenis_buku,
            string judul,
            string isbn,
            string jml_halaman,
            string penerbit,
            string url,
            Guid id_surat_keterangan,
            Guid id_buku
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_luaran_buku_rb (      
                            @p_id_luaran_buku::uuid,
                            @p_id_luaran_dijanjikan::uuid,
	                        @p_kd_jenis_buku::character(1),
	                        @p_judul::text,
	                        @p_isbn::character varying,
	                        @p_jml_halaman::character varying,
                            @p_penerbit::text,
	                        @p_url::text,
                            @p_id_surat_keterangan::uuid,
                            @p_id_buku::uuid
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_buku", id_luaran_buku)
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", id_luaran_dijanjikan)
                    , new Npgsql.NpgsqlParameter("@p_kd_jenis_buku", kd_jenis_buku)
                    , new Npgsql.NpgsqlParameter("@p_judul", judul)
                    , new Npgsql.NpgsqlParameter("@p_isbn", isbn)
                    , new Npgsql.NpgsqlParameter("@p_jml_halaman", jml_halaman)
                    , new Npgsql.NpgsqlParameter("@p_penerbit", penerbit)
                    , new Npgsql.NpgsqlParameter("@p_url", url)
                    , new Npgsql.NpgsqlParameter("@p_id_surat_keterangan", id_surat_keterangan)
                    , new Npgsql.NpgsqlParameter("@p_id_buku", id_buku)))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool updateStatusUnggahBuku(Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            var strSQL = $@"UPDATE penelitian.luaran_buku SET kd_sts_berkas_buku='1'
                            WHERE id_luaran_dijanjikan ='{id_luaran_dijanjikan}'";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getLuaranLainnya(ref DataTable dataTable, Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.get_luaran_lainnya(@id_luaran_dijanjikan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@id_luaran_dijanjikan", id_luaran_dijanjikan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupLuaranLainnya
        (
            Guid id_luaran_lain,
            Guid id_luaran_dijanjikan,
            string kd_jenis_luaran_lain,
            string uraian_luaran_lainnya,
            Guid id_surat_keterangan,
            Guid id_luaran
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_luaran_lainnya_rb (      
                            @p_id_luaran_lain::uuid,
                            @p_id_luaran_dijanjikan::uuid,
	                        @p_kd_jenis_luaran_lain::character(1),
	                        @p_uraian_luaran_lainnya::text,
                            @p_id_surat_keterangan::uuid,
                            @p_id_luaran::uuid
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_lain", id_luaran_lain)
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", id_luaran_dijanjikan)
                    , new Npgsql.NpgsqlParameter("@p_kd_jenis_luaran_lain", kd_jenis_luaran_lain)
                    , new Npgsql.NpgsqlParameter("@p_uraian_luaran_lainnya", uraian_luaran_lainnya)
                    , new Npgsql.NpgsqlParameter("@p_id_surat_keterangan", id_surat_keterangan)
                    , new Npgsql.NpgsqlParameter("@p_id_luaran", id_luaran)))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool updateStatusUnggahLainnya(Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            var strSQL = $@"UPDATE penelitian.luaran_lain SET kd_sts_berkas_luaran_lain='1'
                            WHERE id_luaran_dijanjikan ='{id_luaran_dijanjikan}'";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getLuaranHKI(ref DataTable dataTable, Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.get_luaran_hki(@id_luaran_dijanjikan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@id_luaran_dijanjikan", id_luaran_dijanjikan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupLuaranHKI
        (
            Guid id_luaran_hki,
            Guid id_luaran_dijanjikan,
            string kd_jenis_hki,
            string judul,
            string no_pendaftaran,
            string nama_pemegang_hak_cipta,
            string no_hki,
            Guid id_sertifikat_pencatatan,
            Guid id_dokumentasi,
            Guid id_manual_book
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_luaran_hki_rb (      
                            @p_id_luaran_hki::uuid,
                            @p_id_luaran_dijanjikan::uuid,
	                        @p_kd_jenis_hki::character(1),
	                        @p_judul::text,
	                        @p_no_pendaftaran::character varying,	                        
                            @p_nama_pemegang_hak_cipta::text,
                            @p_no_hki::text,
                            @p_id_sertifikat_pencatatan::uuid,
                            @p_id_dokumentasi::uuid,
                            @p_id_manual_book::uuid
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_hki", id_luaran_hki)
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", id_luaran_dijanjikan)
                    , new Npgsql.NpgsqlParameter("@p_kd_jenis_hki", kd_jenis_hki)
                    , new Npgsql.NpgsqlParameter("@p_judul", judul)
                    , new Npgsql.NpgsqlParameter("@p_no_pendaftaran", no_pendaftaran)
                    , new Npgsql.NpgsqlParameter("@p_nama_pemegang_hak_cipta", nama_pemegang_hak_cipta)
                    , new Npgsql.NpgsqlParameter("@p_no_hki", no_hki)
                    , new Npgsql.NpgsqlParameter("@p_id_sertifikat_pencatatan", id_sertifikat_pencatatan)
                    , new Npgsql.NpgsqlParameter("@p_id_dokumentasi", id_dokumentasi)
                    , new Npgsql.NpgsqlParameter("@p_id_manual_book", id_manual_book)))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool updateStatusUnggahHKI(Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            var strSQL = $@"UPDATE penelitian.luaran_hki SET kd_sts_berkas_hki='1'
                            WHERE id_luaran_dijanjikan ='{id_luaran_dijanjikan}'";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getLuaranPVT(ref DataTable dataTable, Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM penelitian.get_luaran_pvt(@id_luaran_dijanjikan);";
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable
                            , new Npgsql.NpgsqlParameter("@id_luaran_dijanjikan", id_luaran_dijanjikan));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupLuaranPVT
        (
            Guid id_luaran_pvt,
            Guid id_luaran_dijanjikan,
            string kd_jenis_pvt,
            string nama_varietas,
            string no_permohonan,
            string tgl_permohonan,
            string nama_pemohon,
            string alamat_pemohon,
            Guid id_dok_permohonan,
            Guid id_sertifikat_hak_pvt
        )
        {
            bool isSuccess = false;
            string strSQL = @"select * from penelitian.insup_luaran_pvt_rb (      
                            @p_id_luaran_pvt::uuid,
                            @p_id_luaran_dijanjikan::uuid,
	                        @p_kd_jenis_pvt::character(1),
	                        @p_nama_varietas::text,
	                        @p_no_permohonan::character varying,	                        
                            @p_tgl_permohonan::date,
                            @p_nama_pemohon::character varying,
                            @p_alamat_pemohon::text,
                            @p_id_dok_permohonan::uuid,
                            @p_id_sertifikat_hak_pvt::uuid
                            );";

            DataTable dt = new DataTable();
            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_pvt", id_luaran_pvt)
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", id_luaran_dijanjikan)
                    , new Npgsql.NpgsqlParameter("@p_kd_jenis_pvt", kd_jenis_pvt)
                    , new Npgsql.NpgsqlParameter("@p_nama_varietas", nama_varietas)
                    , new Npgsql.NpgsqlParameter("@p_no_permohonan", no_permohonan)
                    , new Npgsql.NpgsqlParameter("@p_tgl_permohonan", tgl_permohonan)
                    , new Npgsql.NpgsqlParameter("@p_nama_pemohon", nama_pemohon)
                    , new Npgsql.NpgsqlParameter("@p_alamat_pemohon", alamat_pemohon)
                    , new Npgsql.NpgsqlParameter("@p_id_dok_permohonan", id_dok_permohonan)
                    , new Npgsql.NpgsqlParameter("@p_id_sertifikat_hak_pvt", id_sertifikat_hak_pvt)))
            {
                isSuccess = true;
            }
            else
            {
                _errorMessage = _db.ErrorMessage;
            }
            return isSuccess;
        }

        public bool updateStatusUnggahPVT(Guid id_luaran_dijanjikan)
        {
            bool isSuccess = false;
            var strSQL = $@"UPDATE penelitian.luaran_pvt SET kd_sts_berkas_pvt='1'
                            WHERE id_luaran_dijanjikan ='{id_luaran_dijanjikan}'";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }


        public bool getLuaranProdukIndustri(ref DataTable dataTable, int idJenisLuaran, int idTargetCapaianLuaran,
            Guid idUsulan,int id_kelompok_luaran,int tahun_ke)
        {
            var strSQL = "SELECT * FROM hibah.list_bukti_luaran_xiia(@p_id_jenis_luaran::integer, @p_id_target_capaian_luaran::integer, @p_id_usulan::uuid,@p_id_kelompok_luaran::integer,@p_tahun_ke::smallint)";

            if (!this._db.FetchDataTable(strSQL, ref dataTable,
                    new NpgsqlParameter("@p_id_jenis_luaran", idJenisLuaran),
                    new NpgsqlParameter("@p_id_target_capaian_luaran", idTargetCapaianLuaran),
                    new NpgsqlParameter("@p_id_usulan", idUsulan),
                    new NpgsqlParameter("@p_id_kelompok_luaran", id_kelompok_luaran),
                    new NpgsqlParameter("@p_tahun_ke", tahun_ke)
                    ))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getDaftarBuktiLuaran(ref DataTable dataTable, int idJenisLuaran, int idTargetCapaianLuaran, Guid idUsulan)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM hibah.list_bukti_luaran_xiia({0},{1},'{2}'); ",
                idJenisLuaran, idTargetCapaianLuaran, idUsulan);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        /*
        public bool insertLuaranWajibTerapanPengembangan
        (
            int p_id_jenis_luaran,
            int p_id_target_capaian_luaran,
            Guid p_id_usulan
        )
        {
            bool isSuccess = false;
            string strSQL = @"SELECT * FROM hibah.insup_bukti_luaran_wajib_terapan_pengembangan_xiia(      
                            @p_id_jenis_luaran::integer,
                            @p_id_target_capaian_luaran::integer,
                            @p_id_usulan::uuid 
                            );";

            if (this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
                    , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
                    , new Npgsql.NpgsqlParameter("@p_id_usulan", p_id_usulan)
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
        */
        public bool insertLuaranWajibTerapanPengembangan (
            Guid p_id_luaran_dijanjikan,
            int p_id_jenis_luaran,
            int p_id_target_capaian_luaran,
            Guid p_id_usulan,
            int p_volume,
            string p_keterangan,
            int p_tahun_ke,
            int p_kelompok_luaran
        )
        {
            string strSQL = @"SELECT * FROM penelitian.insup_bukti_luaran_wajib_terapan_pengembangan_xiia (      
                              @p_id_luaran_dijanjikan,
                              @p_id_jenis_luaran,
                              @p_id_target_capaian_luaran,
                              @p_id_usulan,
                              @p_volume::SMALLINT,
                              @p_keterangan::VARCHAR,
                              @p_tahun_ke::SMALLINT,
                              @p_id_kelompok_luaran::INTEGER
                              );";

            if (!this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", p_id_luaran_dijanjikan)
                    , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", p_id_jenis_luaran)
                    , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", p_id_target_capaian_luaran)
                    , new Npgsql.NpgsqlParameter("@p_id_usulan", p_id_usulan)
                    , new Npgsql.NpgsqlParameter("@p_volume", p_volume)
                    , new Npgsql.NpgsqlParameter("@p_keterangan", p_keterangan)
                    , new Npgsql.NpgsqlParameter("@p_tahun_ke", p_tahun_ke)
                    , new Npgsql.NpgsqlParameter("@p_id_kelompok_luaran", p_kelompok_luaran)
                    ))
            {
                _errorMessage = _db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool updateKeterangan(Guid idLuaranDijanjikan, string keterangan)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT penelitian.update_keterangan_bukti_luaran_wajib_terapan_pengembangan(
                            @p_id_luaran_dijanjikan,@p_keterangan);";
            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_luaran_dijanjikan", idLuaranDijanjikan)
                    , new Npgsql.NpgsqlParameter("@p_keterangan", keterangan)
                    );

            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }
        public bool updateKeterangan(Guid idUsulan, int urutanTahun, int idJenisLuaran,
                       int idTargetCapaian, string keterangan)
        {
            string strSQL = @"SELECT penelitian.update_keterangan_bukti_luaran_wajib_terapan_pengembangan(
                                @p_id_usulan, @p_urutan_tahun::SMALLINT, @p_id_jenis_luaran, 
                                @p_id_target_capaian_luaran, @p_keterangan);";
            if (!this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@p_id_usulan", idUsulan)
                    , new Npgsql.NpgsqlParameter("@p_urutan_tahun", urutanTahun)
                    , new Npgsql.NpgsqlParameter("@p_id_jenis_luaran", idJenisLuaran)
                    , new Npgsql.NpgsqlParameter("@p_id_target_capaian_luaran", idTargetCapaian)
                    , new Npgsql.NpgsqlParameter("@p_keterangan", keterangan)
                    ))

            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getJenisTargetLuaran(ref DataTable dataTable, Guid idUsulan)
        {
            var strSQL = @"SELECT DISTINCT l.id_jenis_luaran, l.id_target_capaian_luaran 
                           FROM penelitian.luaran_dijanjikan_xii l
                           WHERE l.id_usulan = @p_id_usulan";

            if (!this._db.FetchDataTable(strSQL, ref dataTable,
                    new NpgsqlParameter("@p_id_usulan", idUsulan)))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        #endregion
    }
}