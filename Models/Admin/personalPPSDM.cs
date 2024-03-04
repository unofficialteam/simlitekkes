using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace simlitekkes.Models.Admin
{
    public class personalPPSDM : _abstractModels
    {

        public personalPPSDM()
        {
            setInitValues();
        }

        #region Methods

        public bool getJmlDataPersonal(string nama, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT count(*)::int AS jml_data " +
                $"FROM pdpt.list_personal_ppsdm('{nama}', {limit}, {offset});";

            isSuccess = this._db.fetchDataSkalar(strSQL, ref this._numOfRecords);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listDataPersonal(ref DataTable dataTable, string nama, int limit, int offset)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM pdpt.list_personal_ppsdm('{nama}', {limit}, {offset});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPeran(ref DataTable dataTable, Guid idPersonal)
        {
            bool isSuccess = false;

            string strSQL = $@"SELECT * FROM pengguna.list_peran_pengguna_by_personal('{idPersonal}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPeranDefault(ref DataTable dataTable, Guid idPersonal)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT SUM(is_default_)::integer AS jml_default " +
                $"FROM pengguna.list_peran_pengguna_by_personal('{idPersonal}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool updatePeranPengguna(int p_id_peran, Guid p_id_personal, string p_is_default)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM pengguna.update_peran_pengguna({p_id_peran},'{p_id_personal}','{p_is_default}');";
            isSuccess = this._db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPeranAktif(ref DataTable dataTable, Guid idPersonal, int peranTerdaftar)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM pengguna.list_peran_pengguna_by_personal('{idPersonal}') " +
                $"WHERE id_peran_terdaftar = {peranTerdaftar};";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupPeranPengguna(Guid id_personal, string kd_sts_aktif,
            int id_peran, string is_default)
        {
            string strSQL = $"SELECT * FROM pengguna.insup_peran_pengguna('{id_personal}', '{kd_sts_aktif}', " +
                $"{id_peran}, '{is_default}');";
            bool isSuccess = _db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getPenggunaPersonalByPic(ref DataTable dataTable, string id_kontak_pic_pengguna_personal)
        {
            bool isSuccess = false;

            string strSQL = string.Format("SELECT * FROM pengguna.get_kontak_pic_pengguna_by_personal('{0}');",
                id_kontak_pic_pengguna_personal);

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insertPengirimanUser(Guid id_kontak_pic_pengguna_personal,string surel,
            string kd_sts_pengiriman, string surel_pengirim)
        {
            string strSQL = $"SELECT * FROM pengguna.insert_pengiriman_user_personal('{id_kontak_pic_pengguna_personal}', " +
                $"'{surel}', '{kd_sts_pengiriman}', '{surel_pengirim}');";
            bool isSuccess = _db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getUnitKegiatan(ref DataTable dataTable, Guid idPersonal)
        {
            bool isSuccess = false;

            string strSQL = $@"SELECT * FROM pengguna.list_unit_kegiatan_pengguna('{idPersonal}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool getUnitKegiatanAktif(ref DataTable dataTable, Guid idPersonal, Guid unitKegiatanTerdaftar)
        {
            bool isSuccess = false;

            string strSQL = $"SELECT * FROM pengguna.list_unit_kegiatan_pengguna('{idPersonal}') " +
                $"WHERE id_unit_kegiatan_terdaftar = '{unitKegiatanTerdaftar}';";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listProvinsi(ref DataTable dataTable, int jmlBaris, int offset)
        {

            bool isSuccess = false;

            string strSQL = $@"SELECT * FROM pdpt.list_provinsi({jmlBaris},{offset});";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listKota(ref DataTable dataTable, string kdProvinsi)
        {
            bool isSuccess = false;

            string strSQL = $@"SELECT * FROM pdpt.list_kabkota_by_propinsi('{kdProvinsi}');";

            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupPersonalPpsdm(
            Guid id_personal,
            string kd_sts_aktif,
            string nama,
            string gelar_akademik_depan,
            string gelar_akademik_belakang,
            string nomor_ktp,
            string kd_jenis_kelamin,
            string tempat_lahir,
            string tanggal_lahir,
            string alamat,
            string kd_kota,
            string kd_pos,
            string nomor_telepon,
            string nomor_hp,
            string surel,
            Guid id_institusi,
            string website_personal
            )
        {
            string strSQL = $"SELECT * FROM pdpt.insup_personal_ppsdm('{id_personal}', '{kd_sts_aktif}', " +
                $"'{nama}', '{gelar_akademik_depan}', '{gelar_akademik_belakang}', '{nomor_ktp}', '{kd_jenis_kelamin}', " +
                $"'{tempat_lahir}', '{tanggal_lahir}', '{alamat}', '{kd_kota}', '{kd_pos}', '{nomor_telepon}', " +
                $"'{nomor_hp}', '{surel}', '{id_institusi}', '{website_personal}');";
            bool isSuccess = _db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool delPersonalPPSDM(Guid id_personal)
        {
            string strSQL = $@"SELECT * FROM pdpt.del_personal_ppsdm('{id_personal}');";
            bool isSuccess = _db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool insupUnitKegiatanPengguna(Guid id_personal, Guid id_unit_kegiatan, string kd_sts_aktif)
        {
            string strSQL = $"SELECT * FROM pengguna.insup_unit_kegiatan_pengguna('{id_personal}', " +
                $"'{id_unit_kegiatan}', '{kd_sts_aktif}');";
            bool isSuccess = _db.ExecuteNonQuery(strSQL);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}