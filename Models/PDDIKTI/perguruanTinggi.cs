using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simlitekkes.Models.PDDIKTI
{
    public class KabKotaPt
    {
        public string id { get; set; }
        public string nama { get; set; }
    }

    public class PropinsiPt
    {
        public int id { get; set; }
        public string nama { get; set; }
    }

    public class AlamatPt
    {
        public string jalan { get; set; }
        public object rt { get; set; }
        public object rw { get; set; }
        public object dusun { get; set; }
        public string kelurahan { get; set; }
        public string kode_pos { get; set; }
        public KabKota kab_kota { get; set; }
    }

    public class Pembina
    {
        public string id { get; set; }
        public string nama { get; set; }
    }

    public class StatusPt
    {
        public int id { get; set; }
        public string nama { get; set; }
    }

    public class BentukPendidikan
    {
        public int id { get; set; }
        public string nama { get; set; }
    }
    
    public class Pt
    {
        public string id { get; set; }
        public string kode { get; set; }
        public string nama { get; set; }
        public string nama_singkat { get; set; }
        public string sk_pendirian { get; set; }
        public string tgl_sk_pendirian { get; set; }
        public object sk_operasional { get; set; }
        public object tgl_sk_operasional { get; set; }
        public string status { get; set; }
        public AlamatPt alamat { get; set; }
        public PropinsiPt propinsi { get; set; }
        public string telepon { get; set; }
        public string faksimile { get; set; }
        public string website { get; set; }
        public string email { get; set; }
        public StatusPt status_milik { get; set; }
        public Pembina pembina { get; set; }
        public BentukPendidikan bentuk_pendidikan { get; set; }
        public string last_update { get; set; }
    }
}