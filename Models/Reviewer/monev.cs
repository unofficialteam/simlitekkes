using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Reviewer
{
    public class monev : _abstractModels
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public monev()
        {
            setInitValues();
        }

        ~monev()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods
        public bool getDaftarUsulanMonev(ref DataTable dataTable, Guid idPenugasanReviewer)
        {
            var query = $"SELECT * FROM penelitian.list_monev_usulan('{idPenugasanReviewer}');";

            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getDaftarLuaranWajib(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            var query = $"SELECT * FROM hibah.list_monev_luaran_dijanjikan('{idUsulanKegiatan}',{1});";

            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getDaftarLuaranTambahan(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            var query = $"SELECT * FROM hibah.list_monev_luaran_dijanjikan('{idUsulanKegiatan}',{2});";

            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool getDaftarOpsiNilaiMonev(ref DataTable dataTable, int idKategoriNilai)
        {
            var query = $"SELECT * FROM penelitian.list_opsi_nilai_monev({idKategoriNilai});";

            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }
        
        public bool getNilaiMonevLuaran(ref DataTable dataTable, Guid idPlottingReviewer)
        {
            var query = $"SELECT * FROM penelitian.get_penilaian_monev_luaran('{idPlottingReviewer}');";

            if (!this._db.FetchDataTable(query, ref dataTable))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool insupListPenilaian(Guid IdPlottingReviewer, Dictionary<int, string> dictNilaiPelaksanaan,
                        List<HasilMonevLuaran> listNilaiLuaran)
        {

            foreach (var nilai in dictNilaiPelaksanaan)
            {
                var idOpsi = int.Parse(nilai.Value);
                if (idOpsi == -1) continue;

                var query = @"SELECT * FROM penelitian.insup_hasil_penilaian_monev_pelaksanaan(@p_id_plotting_reviewer,
                                    @p_id_sub_kategori_penilaian_komponen_monev, @p_id_opsi_nilai_monev);";

                if (!this._db.ExecuteNonQuery(query
                            , new NpgsqlParameter("@p_id_plotting_reviewer", IdPlottingReviewer)
                            , new NpgsqlParameter("@p_id_sub_kategori_penilaian_komponen_monev", nilai.Key)
                            , new NpgsqlParameter("@p_id_opsi_nilai_monev", idOpsi)
                            ))
                {
                    this._errorMessage = this._db.ErrorMessage;
                    return false;
                }

            }

            foreach (var nilaiLuaran in listNilaiLuaran)
            {
                if (nilaiLuaran.IdOpsiNilai == -1) continue;

                var query = @"SELECT * FROM penelitian.insup_hasil_penilaian_monev_luaran(
                                    @p_id_luaran_dijanjikan, @p_id_plotting_reviewer,
                                    @p_id_sub_kategori_penilaian_komponen_monev, @p_id_opsi_nilai_monev);";

                if (!this._db.ExecuteNonQuery(query
                            , new NpgsqlParameter("@p_id_luaran_dijanjikan", nilaiLuaran.IdLuaranDijanjikan)
                            , new NpgsqlParameter("@p_id_plotting_reviewer", IdPlottingReviewer)
                            , new NpgsqlParameter("@p_id_sub_kategori_penilaian_komponen_monev", nilaiLuaran.IdSubKategori)
                            , new NpgsqlParameter("@p_id_opsi_nilai_monev", nilaiLuaran.IdOpsiNilai)
                            ))
                {
                    this._errorMessage = this._db.ErrorMessage;
                    return false;
                }

            }

            return true;
        }

        public bool getDaftarHasilMonevPelaksanaan(ref Dictionary<int, int> dictNilai, Guid idPlottingReviewer,
                        int IdKategoriRiset)
        {
            var dt = new DataTable();
            var query = $"SELECT * FROM penelitian.list_hasil_penilaian_monev_pelaksanaan('{idPlottingReviewer}',{IdKategoriRiset});";

            if (!this._db.FetchDataTable(query, ref dt))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dictNilai.Add(int.Parse(dt.Rows[i]["id_sub_kategori_penilaian_komponen_monev"].ToString()),
                    int.Parse(dt.Rows[i]["id_opsi_nilai_monev"].ToString()));
            }

            return true;
        }
        
        public bool getDaftarHasilMonevLuaran(ref List<HasilMonevLuaran> NilaiHasilMonevLuaran, Guid idPlottingReviewer,
                        int IdKategoriRiset)
        {
            var dt = new DataTable();
            var query = $"SELECT * FROM penelitian.list_hasil_penilaian_monev_luaran('{idPlottingReviewer}',{IdKategoriRiset});";

            if (!this._db.FetchDataTable(query, ref dt))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            NilaiHasilMonevLuaran = new List<HasilMonevLuaran>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NilaiHasilMonevLuaran.Add(new HasilMonevLuaran()
                {
                    IdSubKategori = int.Parse(dt.Rows[i]["id_sub_kategori_penilaian_komponen_monev"].ToString()),
                    IdOpsiNilai = int.Parse(dt.Rows[i]["id_opsi_nilai_monev"].ToString()),
                    IdLuaranDijanjikan = (string.IsNullOrWhiteSpace(dt.Rows[i]["id_luaran_dijanjikan"].ToString()) ? 
                                            Guid.Empty : Guid.Parse(dt.Rows[i]["id_luaran_dijanjikan"].ToString()))
                });
            }

            return true;
        }
        #endregion

        #region Private Function

        #endregion
    }

    [Serializable]
    public class HasilMonevLuaran
    {
        public Guid IdLuaranDijanjikan { get; set; }
        public int IdSubKategori { get; set; }
        public int IdOpsiNilai { get; set; }
    }
}