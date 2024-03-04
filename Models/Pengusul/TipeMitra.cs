using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Pengusul.Mitra
{
    public class TipeMitra
    {
        public Guid IdMitraAbdimas { get; set; }
        public Guid IdUsulanKegiatan { get; set; }
        public int KdKategoriMitra { get; set; }
        public int IdTipeMitra { get; set; }
        public int LamaKegiatan { get; set; }
        public decimal? DanaTahun1 { get; set; }
        public decimal? DanaTahun2 { get; set; }
        public decimal? DanaTahun3 { get; set; }
        public string KdStatusUnggahPernyataan { get; set; }
        public DateTime TglUnggahPernyataan { get; set; }

    }

    public class PTPelaksana : TipeMitra
    {
        public string NamaPimpinanInstitusi { get; set; }
        public string Jabatan { get; set; }
        public string AlamatInstitusi { get; set; }
    }

    public class PTMitra : TipeMitra
    {
        public string NamaPimpinanInstitusi { get; set; }
        public string Jabatan { get; set; }
        public string AlamatInstitusi { get; set; }
        public Guid IdInstitusi { get; set; }
    }

    public class PemdaPemkot : TipeMitra
    {
        public string NamaPimpinanInstitusi { get; set; }
        public string Jabatan { get; set; }
        public string NamaOrganisasiInstitusi { get; set; }
        public string AlamatInstitusi { get; set; }
    }

    public class LembagaCSR : TipeMitra
    {
        public string NamaPimpinanInstitusi { get; set; }
        public string Jabatan { get; set; }
        public string NamaOrganisasiInstitusi { get; set; }
        public string AlamatInstitusi { get; set; }
    }

    public class KelompokMasyarakat : TipeMitra
    {
        public int IdJenisMitra { get; set; }
        public string NamaPimpinanMitra { get; set; }
        public string NamaMitra { get; set; }
        public string KdDesa { get; set; }
        public string AlamatMitra { get; set; }
        public int Jarak { get; set; }
    }

    public class LembagaPemerintahan : TipeMitra
    {
        public int IdJenisMitra { get; set; }
        public string NamaPimpinanLembaga { get; set; }
        public string KdDesa { get; set; }
        public string AlamatLembaga { get; set; }
        public int Jarak { get; set; }
        public string BidangMasalah { get; set; }
    }

    public class InstansiSwasta : TipeMitra
    {
        public int IdJenisMitra { get; set; }
        public string NamaPimpinanInstansi { get; set; }
        public string KdDesa { get; set; }
        public string AlamatInstansi { get; set; }
        public int Jarak { get; set; }
        public string BidangMasalah { get; set; }
    }

    public class UMKM : TipeMitra
    {
        public int IdJenisMitra { get; set; }
        public string NamaPimpinan { get; set; }
        public string NamaUMKM { get; set; }
        public string KdDesa { get; set; }
        public string Alamat { get; set; }
        public int Jarak { get; set; }
        public string BidangUsaha { get; set; }
        public decimal Asset { get; set; }
        public decimal Omzet { get; set; }
        public string BidangMasalah { get; set; }
        public string Jabatan { get; set; }
        public int UrutanTahun { get; set; }
    }

    public class KelompokSasaran : TipeMitra
    {
        public int IdJenisMitra { get; set; }
        public string NamaPimpinan { get; set; }
        public string NamaKelompok { get; set; }
        public string KdDesa { get; set; }
        public string Alamat { get; set; }        
        public string BidangPengembangan { get; set; }
        public Guid IdMitraReferensi { get; set; }
    }

    public class KelompokMasyarakatPPDM : TipeMitra
    {
        public int IdJenisMitra { get; set; }
        public string NamaPimpinanMitra { get; set; }
        public string NamaMitra { get; set; }
        public string AlamatMitra { get; set; }
        public string BidangMasalah { get; set; }
        public Guid IdMitraReferensi { get; set; }
        public int UrutanTahun { get; set; }        
    }

    public class KelompokSasaranPPMUPT : TipeMitra
    {
        public int IdJenisMitra { get; set; }
        public string NamaPimpinan { get; set; }
        public string NamaKelompok { get; set; }
        public string Alamat { get; set; }
        public string BidangPengembangan { get; set; }
        public Guid IdMitraReferensi { get; set; }
        public int UrutanTahun { get; set; }
        public int UrutanMitra { get; set; }
    }
}