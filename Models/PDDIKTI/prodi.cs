using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simlitekkes.Models.PDDIKTI
{
    public class KabKotaProdi
    {
        public string id { get; set; }
        public string nama { get; set; }
    }

    public class PT
    {
        public string id { get; set; }
        public string kode { get; set; }
        public string nama { get; set; }
        public object nama_singkat { get; set; }
        public object sk_pendirian { get; set; }
        public object tgl_sk_pendirian { get; set; }
        public object sk_operasional { get; set; }
        public object tgl_sk_operasional { get; set; }
        public object status { get; set; }
        public object alamat { get; set; }
        public object propinsi { get; set; }
        public object telepon { get; set; }
        public object faksimile { get; set; }
        public object website { get; set; }
        public object email { get; set; }
        public object status_milik { get; set; }
        public object pembina { get; set; }
        public object bentuk_pendidikan { get; set; }
        public object last_update { get; set; }
    }

    public class JenjangDidik
    {
        public int id { get; set; }
        public string nama { get; set; }
    }

    public class Prodi
    {
        public string id { get; set; }
        public string kode { get; set; }
        public string nama { get; set; }
        public string status { get; set; }
        public string visi { get; set; }
        public string misi { get; set; }
        public string kompetensi { get; set; }
        public string jalan { get; set; }
        public string telepon { get; set; }
        public string faksimile { get; set; }
        public string website { get; set; }
        public string email { get; set; }
        public KabKotaProdi kab_kota { get; set; }
        public PT pt { get; set; }
        public JenjangDidik jenjang_didik { get; set; }
        public string tgl_berdiri { get; set; }
        public string sk_selenggara { get; set; }
        public string tgl_sk_selenggara { get; set; }
        public int sks_lulus { get; set; }
        public object kode_pos { get; set; }
        public string last_update { get; set; }
    }
}