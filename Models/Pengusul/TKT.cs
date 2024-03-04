using System.Data;

namespace simlitekkes.Models.Pengusul
{
    public class tkt : _abstractModels
    {
        #region Konstruktor dan Destruktor
        public tkt()
        {
            setInitValues();
        }
        #endregion

        #region Methods

        public bool GetListKategoriTKT(ref DataTable dataTable)
        {
            var strSQL = "SELECT * FROM tkt.kategori ORDER BY urutan";

            dataTable = new DataTable();
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool GetListIndikatorTKT(ref DataTable dataTable, string IdKategori, string IdLevel)
        {
            var strSQL = $@"SELECT * FROM tkt.indikator 
                            WHERE id_kategori = '{IdKategori}' 
                            AND id_level = '{IdLevel}'::CHAR(2) ORDER BY urutan";

            dataTable = new DataTable();
            var isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        #endregion
    }
}