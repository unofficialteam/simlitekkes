using DataAccess;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Configuration;

namespace simlitekkes.Models
{
    public class login:_abstractModels
    {
        #region Fields

        // ==========================================================================================
        // Fields yang digunakan untuk kontrol login
        // ==========================================================================================
        //private PostgresData _objDatabase;
        //private string _connectionString;
        //private string _errorMessage;
        //private string _strSQL;

        private string _ipAddress;
        private string _userName;
        private string _userPswd;
        private string _idPersonal;
        private string _idSertifikat;
        private bool _stsLogin;
        private DateTime _waktuLogin;
        private bool _stsLicensingTime;

        private string _namaLengkap;
        private Guid _idInstitusi;
        private string _kdJenisInstitusi;
        private string _jenisInstitusi;
        private string _namaInstitusi;
        private string _kdInstitusi;
        private string _nidn;

        private Dictionary<int, string> _daftarPeran;
        private int _currIdPeran;
        private string _currNamaPeran;

        private string _pesanError;
        //private string _pesanErrKoneksi;

        #endregion

        #region Konstruktor dan destruktor
        // ==========================================================================================
        // Konstruktor dan Destruktor yang digunakan dalam kontrol login
        // ==========================================================================================

        public login()
        {
            ////this._connectionString = WebConfigurationManager.ConnectionStrings["simlitekkes@localhost"].ConnectionString;
            ////this._objDatabase = new PostgresData(this._connectionString);
            initValues();
        }

        public login(string connectionString)
        {
            //this._objDatabase = new PostgresData(connectionString);
            initValues();

            //this._currIdPeran = idPeran;
            //this._userName = this.strUsername;
            //this._userPswd = strPassword;

            //autentifikasi(this._userName, this._userPswd); //, this._currIdPeran);
        }

        ~login()
        {
        }

        #endregion

        #region Properties
        // ==========================================================================================
        // Properties yang digunakan dalam kontrol login
        // ==========================================================================================
        public string ipAddress
        {
            get
            {
                return this._ipAddress;
            }
        }

        public string userName
        {
            get
            {
                return this._userName;
            }
            set
            {
                this._userName = value;
            }
        }

        public string idPersonal
        {
            get
            {
                return this._idPersonal;
            }
            //set
            //{
            //    this._idPersonal = value;
            //}
        }

        public string idSertifikat
        {
            get
            {
                return this._idSertifikat;
            }
        }

        public bool stsLogin
        {
            get
            {
                return this._stsLogin;
            }
        }

        public DateTime waktuLogin
        {
            get
            {
                return this._waktuLogin;
            }
        }

        public bool stsLicensingTime
        {
            get
            {
                return this._stsLicensingTime;
            }
        }

        public int idPeran
        {
            get
            {
                return this._currIdPeran;
            }
        }

        public string namaPeran
        {
            get
            {
                return this._currNamaPeran;
            }
        }

        public Dictionary<int, string> daftarPeran
        {
            get { return this._daftarPeran; }
        }

        public string namaLengkap
        {
            get
            {
                return this._namaLengkap;
            }
        }

        public Guid idInstitusi
        {
            get
            {
                return this._idInstitusi;
            }
        }

        public string kdJenisInstitusi
        {
            get
            {
                return this._kdJenisInstitusi;
            }
        }

        public string jenisInstitusi
        {
            get
            {
                return this._jenisInstitusi;
            }
        }

        public string namaInstitusi
        {
            get
            {
                return this._namaInstitusi;
            }
        }

        public string kdInstitusi
        {
            get
            {
                return this._kdInstitusi;
            }
        }

        public string nidn
        {
            get
            {
                return this._nidn;
            }
        }

        //public string errorMessage
        //{
        //    get
        //    {
        //        return this._errorMessage;
        //    }
        //}

        public string pesanError
        {
            get
            {
                return this._pesanError;
            }
        }

        #endregion

        #region Methods
        // ==========================================================================================
        // Methods yang digunakan dalam kontrol login
        // ==========================================================================================

        public Boolean autentifikasi(string strUsername, string strPassword,  int idPeran = 0)
        {
            Boolean isSuccess = false;

            this._userName = strUsername;
            this._userPswd = strPassword;
            this._currIdPeran = idPeran;
            DataTable dt = new DataTable();
            try
            {
                try
                {
                    this._strSQL = @"select id_sertifikat, id_personal, nama_lengkap, waktu_login,id_peran, nama_peran, id_institusi
                            , kd_jenis_institusi, jenis_institusi, nama_institusi, kd_institusi, nidn FROM pengguna.set_autentikasi(:userName, :password, :idPeran, :ipClient);";
                    this._idSertifikat = this._db.FetchDataTable(this._strSQL, ref dt
                        //, new NpgsqlParameter(":idPeran", this._currIdPeran)
                        , new NpgsqlParameter(":userName", this._userName)
                        , new NpgsqlParameter(":password", this._userPswd)
                        , new NpgsqlParameter(":idPeran", this._currIdPeran)
                        , new NpgsqlParameter(":ipClient", this._ipAddress)) ? dt.Rows[0]["id_sertifikat"].ToString() != "" ? dt.Rows[0]["id_sertifikat"].ToString() : null : null;
                    if (this._idSertifikat != null)
                    {
                        this._idPersonal = dt.Rows[0]["id_personal"].ToString();
                        this._stsLicensingTime = true;
                        this._stsLogin = true;
                        this._waktuLogin = Convert.ToDateTime(dt.Rows[0]["waktu_login"].ToString());
                        this._currIdPeran = int.Parse(dt.Rows[0]["id_peran"].ToString());
                        this._currNamaPeran = dt.Rows[0]["nama_peran"].ToString();
                        this._namaLengkap = dt.Rows[0]["nama_lengkap"].ToString();
                        this._idInstitusi = (Guid)dt.Rows[0]["id_institusi"];
                        this._kdJenisInstitusi = dt.Rows[0]["kd_jenis_institusi"].ToString();
                        this._jenisInstitusi = dt.Rows[0]["jenis_institusi"].ToString();
                        this._namaInstitusi = dt.Rows[0]["nama_institusi"].ToString();
                        this._kdInstitusi = dt.Rows[0]["kd_institusi"].ToString();
                        this._nidn = dt.Rows[0]["nidn"].ToString();
                        this._errorMessage = "";

                        //// Baca list kewenangan/peran yang dimiliki
                        //this._strSQL = @"SELECT id_peran, nama_peran FROM pengguna.list_id_peran_aktif(:id_personal);";
                        //dt = new DataTable();
                        //if (this._objDatabase.FetchDataTable(this._strSQL, ref dt
                        //    , new NpgsqlParameter(":id_personal", Guid.Parse(this.idPersonal))))
                        //{
                        //    this._daftarPeran = new Dictionary<int, string>();
                        //    foreach (DataRow dr in dt.Rows)
                        //    {
                        //        this._daftarPeran.Add(int.Parse(dr["id_peran"].ToString()), dr["nama_peran"].ToString());
                        //    }
                        //}

                        this._stsLogin = true;
                        isSuccess = true;
                    }
                    else
                    {
                        this._errorMessage = "Username atau password salah...";
                        if(this._db.getMessageKoneksiError()!="")
                        {
                            this._errorMessage = this._db.getMessageKoneksiError();
                        }
                    }
                }
                catch (Exception e)
                {
                    this._errorMessage = "Akses database bermasalah... \nSilahkan hubungi Administrator. " + e.Message;
                }
            }
            catch
            {
                this._pesanError = this._errorMessage;
                this._errorMessage = "Koneksi database bermasalah... \nSilahkan hubungi Administrator";
            }
            finally
            {
                dt.Dispose();
            }

            return isSuccess;
        }

        public string getErrorKoneksi()
        {
            return this._errorMessage;
        }

        public Boolean cekLicensingTime()
        {
            Boolean returnValue = false;
            try
            {
                this._strSQL = "select pengguna.get_login_leasing_time(:idSertifikat, :idPersonal);";
                this._db.ReadSkalar(this._strSQL, ref returnValue
                    , new NpgsqlParameter(":idSertifikat", idSertifikat)
                    , new NpgsqlParameter("idPersonal", idPersonal));
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            return returnValue;
        }

        public bool cekStatusPeran(int idPeran)
        {
            bool returnValue = false;
            try
            {
                if (this._daftarPeran[idPeran] != null)
                {
                    returnValue = true;
                }
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            return returnValue;
        }

        public bool gantiPeran(int idPeran)
        {
            bool returnValue = false;
            try
            {
                if (cekStatusPeran(idPeran))
                {
                    this._currIdPeran = idPeran;
                    this._currNamaPeran = this._daftarPeran[idPeran];
                }
                returnValue = true;
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }
            return returnValue;
        }

        public bool cekApakahKlasterMandiriUtama(Guid id_institusi)
        {
            bool ret = false;
            int jmlRow = 0;
            string strSQL = @"SELECT count(*)::int FROM referensi.cek_klaster_mandiri_utama(@id_institusi);";
            if (!this._db.ReadSkalar(strSQL, ref jmlRow
                , new Npgsql.NpgsqlParameter("@id_institusi", id_institusi)))
            {

                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            if (jmlRow > 0)
                ret = true;

            return ret;
        }

        #endregion

        #region private functions
        // ==========================================================================================
        // Private Function yang digunakan dalam kontrol login
        // ==========================================================================================

        private void initValues()
        {
            // inisialisasi fields
            this._idPersonal = null;
            this._idSertifikat = null;
            this._ipAddress = null;
            this._stsLicensingTime = false;
            this._stsLogin = false;
            this._userName = null;
            this._userPswd = null;
            this._waktuLogin = System.DateTime.Now;
            this._currIdPeran = 0;
            this._currNamaPeran = null;
            this._daftarPeran = new Dictionary<int, string>();
            this._namaLengkap = null;
            this._errorMessage = null;


            // get IP Terminal
            string theIP;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                theIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            else
                theIP = HttpContext.Current.Request.UserHostAddress;

            string[] IPForwarder = theIP.Split(',');
            this._ipAddress = IPForwarder[0].ToString();
        }

        public bool cekPasswordLama(Guid id_personal, string pswd_lama)
        {
            bool ret = false;
            int jmlRow = 0;
            string strSQL = @"SELECT count(*)::int FROM pengguna.list_password(@id_personal, @pswd_lama);";
            if (!this._db.ReadSkalar(strSQL, ref jmlRow
                , new Npgsql.NpgsqlParameter("@id_personal", id_personal)
                , new Npgsql.NpgsqlParameter("@pswd_lama", pswd_lama)))
            {

                this._errorMessage = this._db.ErrorMessage;
                return false;
            }

            if (jmlRow > 0)
                ret = true;

            return ret;
        }

        public bool updatePassword(Guid id_personal, string pswd_baru)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pengguna.update_password(@id_personal, @pswd_baru);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_personal", id_personal)
                    , new Npgsql.NpgsqlParameter("@pswd_baru", pswd_baru));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        public bool listGantiPeran(ref DataTable dataTable, Guid id_personal, int id_peran)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pengguna.list_ganti_peran('{0}', {1});", id_personal, id_peran);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool getUserPasswordByPersonal(ref DataTable dataTable, Guid id_personal)
        {
            bool isSuccess = false;
            string strSQL = string.Format("SELECT * FROM pengguna.get_user_password_by_personal('{0}');", id_personal);
            dataTable = new DataTable();
            isSuccess = this._db.FetchDataTable(strSQL, ref dataTable);
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;
            return isSuccess;
        }

        public bool updateWaktuAksesTerakhir(Guid id_sertifikat)
        {
            bool isSuccess = false;

            string strSQL = @"SELECT pengguna.update_waktu_akses_terakhir(@id_sertifikat);";

            isSuccess = this._db.ExecuteNonQuery(strSQL
                    , new Npgsql.NpgsqlParameter("@id_sertifikat", id_sertifikat));
            if (!isSuccess)
                this._errorMessage = this._db.ErrorMessage;

            return isSuccess;
        }

        #endregion
    }
}