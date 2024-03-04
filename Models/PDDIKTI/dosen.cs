using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simlitekkes.Models.PDDIKTI
{
    public class KabKota
    {
        public string id { get; set; }
        public string nama { get; set; }
    }

    public class Alamat
    {
        public string jalan { get; set; }
        public object rt { get; set; }
        public object rw { get; set; }
        public object dusun { get; set; }
        public string kelurahan { get; set; }
        public object kode_pos { get; set; }
        public KabKota kab_kota { get; set; }
    }

    public class Agama
    {
        public int id { get; set; }
        public string nama { get; set; }
    }

    public class IkatanKerja
    {
        public string id { get; set; }
        public string nama { get; set; }
    }

    public class StatusKepegawaian
    {
        public int id { get; set; }
        public string nama { get; set; }
    }

    public class StatusKeaktifan
    {
        public int id { get; set; }
        public string nama { get; set; }
    }

    public class PangkatGolongan
    {
        public int id { get; set; }
        public string golongan { get; set; }
        public string pangkat { get; set; }
    }

    public class JabatanFungsional
    {
        public int id { get; set; }
        public string nama { get; set; }
    }
    
    public class Dosen
    {
        public string id { get; set; }
        public string nidn { get; set; }
        public string nip { get; set; }
        public object npwp { get; set; }
        public string nik { get; set; }
        public string nama { get; set; }
        public string gelar_depan { get; set; }
        public string gelar_belakang { get; set; }
        public string pendidikan_terakhir { get; set; }
        public string jenis_kelamin { get; set; }
        public string tgl_lahir { get; set; }
        public string tempat_lahir { get; set; }
        public string telepon { get; set; }
        public object handphone { get; set; }
        public Alamat alamat { get; set; }
        public string email { get; set; }
        public string kode_pt { get; set; }
        public string kode_prodi { get; set; }
        public object sk_cpns { get; set; }
        public object tgl_sk_cpns { get; set; }
        public object sk_pengangkatan { get; set; }
        public object tgl_sk_pengangkatan { get; set; }
        public string tmt_sk_pengangkatan { get; set; }
        public object tmt_pns { get; set; }
        public string kewarganegaraan { get; set; }
        public Agama agama { get; set; }
        public IkatanKerja ikatan_kerja { get; set; }
        public StatusKepegawaian status_kepegawaian { get; set; }
        public StatusKeaktifan status_keaktifan { get; set; }
        public PangkatGolongan pangkat_golongan { get; set; }
        public JabatanFungsional jabatan_fungsional { get; set; }
        public string last_update { get; set; }
    }
}