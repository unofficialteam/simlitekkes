using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.PDDIKTI
{
    public class GelarRiwayat
    {
        public int id { get; set; }
        public string nama { get; set; }
        public string singkatan { get; set; }
        public int posisi { get; set; }
    }

    public class BidangStudiRiwayat
    {
        public int id { get; set; }
        public string nama { get; set; }
    }

    public class JenjangDidikRiwayat
    {
        public int id { get; set; }
        public string nama { get; set; }
    }

    public class ProdiRiwayat
    {
        public object id { get; set; }
        public object kode { get; set; }
        public object nama { get; set; }
    }

    public class riwayatPendidikan
    {
        public string id { get; set; }
        public string pt { get; set; }
        public int thn_masuk { get; set; }
        public int thn_lulus { get; set; }
        public GelarRiwayat gelar { get; set; }
        public BidangStudiRiwayat bidang_studi { get; set; }
        public JenjangDidikRiwayat jenjang_didik { get; set; }
        public ProdiRiwayat prodi { get; set; }
        public string last_update { get; set; }
    }
}