using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace simlitekkes.Models.Sistem
{
    [Serializable]
    public class usulanKegiatan
    {
        #region Fields

        public string idUsulanKegiatan;
        public string idUsulan;
        public string judul;
        public int idSkema;
        public string namaSkema;
        public string thnUsulan;
        public string thnPelaksanaan;
        public int lamaKegiatan;
        public int urutanTahunUsulanKegiatan;
        public int tktTarget;
        public string kategoriPenelitian;
        public int idKategoriSBK;
        public int idBidangFokus;
        public string bidangFokus;
        public string thnPertamaUsulan;
        #endregion
    }
}