using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace simlitekkes.Models.Pengusul
{
    public class RAB : _abstractModels
    {
        #region Konstruktor dan Destruktor
        public RAB()
        {
            setInitValues();
        }
        #endregion

        #region Methods

        public bool ImportToDatabase(Guid IdUsulan, List<ItemRencanaAnggaran> ListRencanaAnggaran)
        {
            //Hapus usulan RAB lama            

            var query = $"DELETE FROM hibah.rab_usulan WHERE id_usulan = '{IdUsulan}';";
            if (!this._db.ExecuteNonQuery(query))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }
            try
            {
                using (var conn = new Npgsql.NpgsqlConnection(_db.ConnectionString))
                {
                    var copyCommand = @"COPY hibah.rab_usulan (id_usulan, urutan_thn_usulan_kegiatan,
                                    kd_jenis_pembelanjaan, nama_item, xvolume, xsatuan, harga_satuan) 
                                    FROM STDIN (FORMAT BINARY)";
                    conn.Open();
                    using (var writer = conn.BeginBinaryImport(copyCommand))
                    {
                        foreach (var rab in ListRencanaAnggaran)
                        {
                            writer.StartRow();
                            writer.Write(IdUsulan, NpgsqlDbType.Uuid);
                            writer.Write(rab.TahunKegiatan, NpgsqlDbType.Smallint);
                            writer.Write(rab.KodeJenisPembelanjaan, NpgsqlDbType.Char);
                            writer.Write(rab.Item, NpgsqlDbType.Varchar);
                            writer.Write(rab.Volume, NpgsqlDbType.Numeric);
                            writer.Write(rab.Satuan, NpgsqlDbType.Varchar);
                            writer.Write(rab.Honor, NpgsqlDbType.Money);
                        }
                        writer.Complete();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
                return false;
            }

            return true;
        }

        public bool GetRABUsulan(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;

            var strSQL = $@"SELECT * FROM hibah.list_rab_usulan('{idUsulanKegiatan}');";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetRABUsulanExport(ref DataTable dataTable, Guid idUsulanKegiatan)
        {
            bool isSuccess = false;

            var fields = @"urutan_thn_usulan_kegiatan AS ""TAHUN"",
                           kd_jenis_pembelanjaan AS ""KODE"",
                           jenis_pembelanjaan AS ""JENIS"",
                           nama_item AS ""ITEM"",
                           xvolume  AS ""VOLUME"",
                           xsatuan AS ""SATUAN"",
                           harga_satuan AS ""HARGA"",
                           total_harga AS ""TOTAL""";
            var strSQL = $@"SELECT {fields} FROM hibah.list_rab_usulan('{idUsulanKegiatan}');";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetBidangFokus(ref DataTable dataTable, Guid idUsulan)
        {
            bool isSuccess = false;

            var strSQL = $@"SELECT b.bidang_fokus
                            FROM (SELECT u.id_bidang_fokus
                                  FROM hibah.usulan u
                                  WHERE u.id_usulan = '{idUsulan}') us
                            JOIN referensi.bidang_fokus b USING(id_bidang_fokus)";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetRentangDanaSkemaKegiatan(ref DataTable dataTable, int idSkema)
        {
            bool isSuccess = false;

            var strSQL = $@"SELECT sk.dana_minimal_thn_berjalan, sk.dana_maksimal_thn_berjalan
                            FROM referensi.skema_kegiatan sk
                            WHERE sk.id_skema = {idSkema}";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetKelompokBiaya(ref DataTable dataTable, string kdJenisKegiatan)
        {
            bool isSuccess = false;

            var strSQL = $@"SELECT * FROM referensi.list_rab_kelompok_biaya('{kdJenisKegiatan}');";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetRekapRABRevisi(ref DataTable dataTable, Guid idUsulanKegiatan)
        {

            var strSQL = $"SELECT * FROM hibah.list_rab_rekap_biaya_revisi('{idUsulanKegiatan}'::UUID)";

            dataTable = new DataTable();
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool GetCekRekapRABRevisiThnBerjalan(ref DataTable dataTable, Guid idUsulanKegiatan)
        {

            var strSQL = $"SELECT * FROM hibah.cek_rab_rekap_biaya_revisi_thn_berjalan('{idUsulanKegiatan}'::UUID)";

            dataTable = new DataTable();
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool GetKomponenBelanjaRevisi(ref DataTable dataTable, Guid idUsulanKegiatan, int urutanTahunRAB,
                                       int idKelompokBiaya)
        {

            var strSQL = $@"SELECT * FROM hibah.list_rab_komponen_biaya_revisi('{idUsulanKegiatan}'::UUID,
                            {urutanTahunRAB}::SMALLINT,{idKelompokBiaya})";

            dataTable = new DataTable();
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool InsupKomponenBelanjaRevisi(List<RABKomponenBelanjaRevisi> listKomponenBelanjaRevisi)
        {
            foreach (var item in listKomponenBelanjaRevisi)
            {
                var query = $@"SELECT * FROM hibah.insup_rab_komponen_belanja_revisi('{item.id_rab_komponen_belanja_revisi}',
                               '{item.id_usulan_kegiatan}',{item.urutan_thn_usulan},{item.id_rab_komponen_belanja},
                               {item.volume},{item.harga_satuan.ToString().Replace(",", ".")}::MONEY);";
                if (!_db.ExecuteNonQuery(query))
                {
                    this._errorMessage = this._db.ErrorMessage;
                    return false;
                }
            }

            return true;
        }

        public bool GetRekapItemRAB(ref DataTable dataTable, Guid idUsulanKegiatan)
        {

            var strSQL = $"SELECT * FROM hibah.list_rab_rekap_item_belanja('{idUsulanKegiatan}'::UUID)";

            dataTable = new DataTable();
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool GetKomponenBelanja(ref DataTable dataTable, Guid idUsulanKegiatan, int urutanTahunRAB,
                                       int idKelompokBiaya)
        {

            var strSQL = $@"SELECT * FROM hibah.list_rab_komponen_biaya('{idUsulanKegiatan}'::UUID,
                            {urutanTahunRAB}::SMALLINT,{idKelompokBiaya})";

            dataTable = new DataTable();
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool GetItemBelanja(ref DataTable dataTable, Guid idUsulanKegiatan, int urutanTahunRAB,
                                       int idKelompokBiaya)
        {

            var strSQL = $@"SELECT * FROM hibah.list_rab_item_belanja('{idUsulanKegiatan}'::UUID,
                            {urutanTahunRAB}::SMALLINT,{idKelompokBiaya})";

            dataTable = new DataTable();
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool GetRabUsulanPerthun(ref DataTable dataTable, Guid idUsulanKegiatan, int urutanTahunRAB)
        {

            var strSQL = $@"SELECT * FROM hibah.list_rab_usulan_pertahun('{idUsulanKegiatan}'::UUID,
                            {urutanTahunRAB}::SMALLINT)";

            dataTable = new DataTable();
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool InsertItemBelanja(RABItemBelanja item)
        {
            var query = $@"SELECT * FROM hibah.insert_rab_item_belanja(
                           '{item.id_usulan_kegiatan}',{item.urutan_thn_usulan},{item.id_rab_komponen_belanja},
                           '{item.nama_item}','{item.satuan}',{item.volume},
                            {item.harga_satuan.ToString().Replace(",", ".")}::MONEY);";
            if (!_db.ExecuteNonQuery(query))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool UpdateItemBelanja(RABItemBelanja item)
        {
            var query = $@"SELECT * FROM hibah.update_rab_item_belanja('{item.id_rab_item_belanja}',
                           '{item.id_usulan_kegiatan}',{item.urutan_thn_usulan},{item.id_rab_komponen_belanja},
                           '{item.nama_item}','{item.satuan}',{item.volume},
                            {item.harga_satuan.ToString().Replace(",", ".")}::MONEY);";
            if (!_db.ExecuteNonQuery(query))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool DeleteItemBelanja(Guid idRABItemBelanja)
        {
            var query = $@"DELETE FROM hibah.rab_item_belanja WHERE id_rab_item_belanja = '{idRABItemBelanja}'";
            if (!_db.ExecuteNonQuery(query))
            {
                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            return true;
        }

        public bool GetRABItemBelanjaExport(ref DataTable dataTable, Guid idUsulanKegiatan, int urutanTahun)
        {
            bool isSuccess = false;

            var fields = @"nomor AS ""No."",
                           kelompok_biaya AS ""KELOMPOK"",
                           komponen_belanja AS ""KOMPONEN"",
                           nama_item AS ""ITEM"",
                           satuan AS ""SATUAN"",
                           volume  AS ""VOLUME"",                           
                           harga_satuan AS ""HARGA"",
                           total_harga AS ""TOTAL""";
            var strSQL = $@"SELECT {fields} FROM hibah.list_rab_item_belanja('{idUsulanKegiatan}',{urutanTahun});";

            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion
    }

    [Serializable]
    public class ItemRencanaAnggaran
    {
        public Guid IdRABUsulan { get; set; }
        public int Nomor { get; set; }
        public string Item { get; set; }
        public decimal Volume { get; set; }
        public string Satuan { get; set; }
        public decimal Honor { get; set; }
        public decimal Total { get; set; }
        public string KodeJenisPembelanjaan { get; set; }
        public string JenisPembelanjaan { get; set; }
        public int TahunKegiatan { get; set; }
    }

    [Serializable]
    public class RABKomponenBelanjaRevisi
    {
        public Guid id_rab_komponen_belanja_revisi { get; set; }
        public Guid id_usulan_kegiatan { get; set; }
        public int urutan_thn_usulan { get; set; }
        public int id_rab_komponen_belanja { get; set; }
        public int volume { get; set; }
        public decimal harga_satuan { get; set; }
    }

    public class RABItemBelanja
    {
        public Guid id_rab_item_belanja { get; set; }
        public Guid id_usulan_kegiatan { get; set; }
        public int urutan_thn_usulan { get; set; }
        public int id_rab_komponen_belanja { get; set; }
        public string nama_item { get; set; }
        public string satuan { get; set; }
        public int volume { get; set; }
        public decimal harga_satuan { get; set; }
    }

}