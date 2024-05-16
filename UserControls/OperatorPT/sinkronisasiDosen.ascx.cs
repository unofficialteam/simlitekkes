using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;
using OfficeOpenXml;
using System.IO;
using Npgsql;
using System.Web.Configuration;
using System.Configuration;

using simlitekkes.Models.PDDIKTI;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

using simlitekkes.Core;
using simlitekkes.Helper;
using RestSharp.Serialization.Json;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class sinkronisasiDosen : System.Web.UI.UserControl
    {
        Models.PT.daftarSinkronisasiDosen modelSinkronisasi = new Models.PT.daftarSinkronisasiDosen();
        Models.Pengusul.daftarTendikNonDosen modelTendikNonDosen = new Models.Pengusul.daftarTendikNonDosen();

        UIControllers.uiGridView obj_uiGridView = new UIControllers.uiGridView();
        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiModal objModal = new uiModal();

        string cs = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string runningMode = ConfigurationManager.AppSettings["runnigmode"];
            if (runningMode.ToUpper() == "PRODUCTION")
                cs = ConfigurationManager.ConnectionStrings["productionDB"].ConnectionString;
            else if (runningMode.ToUpper() == "DEVELOPMENT")
                cs = ConfigurationManager.ConnectionStrings["developmentDB"].ConnectionString;
            else if (runningMode.ToUpper() == "PELATIHAN")
                cs = WebConfigurationManager.ConnectionStrings["pelatihanDB"].ConnectionString;

            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiGvDaftarDosen(0);
                mvDaftarDosen.SetActiveView(vDaftarDosen);
            }
        }

        public static string ConvertDateTimeToDate(string dateTimeString, string dateTimeFormat, String langCulture)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(langCulture);
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(dateTimeString, out dt))
            {
                return dt.ToString(dateTimeFormat, culture);
            }
            return dateTimeString;
        }

        #region Daftar Dosen

        protected void lbCEK_Click(object sender, EventArgs e)
        {
            if (tbNIDN.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "NIDN belum diisi");
                return;
            }
            else
            {
                int digitDepanNIDN = int.Parse(tbNIDN.Text.Trim().Substring(0, 2));
                if (digitDepanNIDN == 77)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "NIDN bukan dosen");
                    return;
                }
                else
                {
                    getDataPddikti();
                    mvDaftarDosen.SetActiveView(vSinkronisasi);
                }
            }
        }

        protected void ddlJmlBarisDosen_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarDosen(0);
        }

        private void isiGvDaftarDosen(int idxPage)
        {
            //refreshForm();
            string kdPerguruanTinggi = objLogin.kdInstitusi.ToString();

            if (!modelSinkronisasi.getJmlDaftarDosen(kdPerguruanTinggi))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelSinkronisasi.errorMessage);

            //NEW PAGING CONTROL
            lblJmlDosen.Text = modelSinkronisasi.numOfRecords.ToString();

            pagingDaftarDosen.currentPage = idxPage;
            pagingDaftarDosen.setPaging(int.Parse(ddlJmlBarisDosen.SelectedValue), modelSinkronisasi.numOfRecords);

            modelSinkronisasi.currentPage = idxPage;
            modelSinkronisasi.rowsPerPage = int.Parse(ddlJmlBarisDosen.SelectedValue);

            if (!modelSinkronisasi.getDaftarDosen(kdPerguruanTinggi))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelSinkronisasi.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvDaftarDosen, modelSinkronisasi.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelSinkronisasi.errorMessage);

            if (modelSinkronisasi.numOfRecords < 1)
            {
                pagingDaftarDosen.setPaging(int.Parse(ddlJmlBarisDosen.SelectedValue), 1);
            }
        }

        protected void daftarDosen_PageChanging(object sender, EventArgs e)
        {
            string kdPerguruanTinggi = objLogin.kdInstitusi.ToString();

            modelSinkronisasi.currentPage = pagingDaftarDosen.currentPage;
            modelSinkronisasi.rowsPerPage = int.Parse(ddlJmlBarisDosen.SelectedValue);

            if (!modelSinkronisasi.getDaftarDosen(kdPerguruanTinggi))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvDaftarDosen, modelSinkronisasi.currentRecords);
        }

        protected void gvDaftarDosen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNomor = (Label)e.Row.FindControl("lblNomor");
                lblNomor.Text = (e.Row.RowIndex + 1 + int.Parse(ddlJmlBarisDosen.SelectedValue) * (pagingDaftarDosen.currentPage)).ToString();

                //Label lblnamaDosen = (Label)e.Row.FindControl("lblnamaDosen");
            }
        }

        protected void lbExcellDaftarDosen_Click(object sender, EventArgs e)
        {
            string kdPerguruanTinggi = objLogin.kdInstitusi.ToString();
            string namaInstitusi = objLogin.namaInstitusi.ToString();

            DataTable dt = new DataTable();
            if (modelSinkronisasi.getExcelDaftarDosen(ref dt, kdPerguruanTinggi))
            {
                var fileName = string.Format("Data Dosen " + namaInstitusi + ".xlsx");
                string namaSheet = "data";
                kontrolExcel ke = new kontrolExcel();
                ke.WriteExcel(this.Page, dt, "xlsx", fileName, namaSheet);
            }
        }

        protected void lbCari_Click(object sender, EventArgs e)
        {
            string kdPerguruanTinggi = objLogin.kdInstitusi.ToString();
            string nama = tbNama.Text;

            if (!modelSinkronisasi.getJmlDaftarDosenCari(kdPerguruanTinggi, nama))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelSinkronisasi.errorMessage);

            //NEW PAGING CONTROL
            lblJmlDosen.Text = modelSinkronisasi.numOfRecords.ToString();

            pagingDaftarDosen.currentPage = 0;
            pagingDaftarDosen.setPaging(int.Parse(ddlJmlBarisDosen.SelectedValue), modelSinkronisasi.numOfRecords);

            modelSinkronisasi.currentPage = 0;
            modelSinkronisasi.rowsPerPage = int.Parse(ddlJmlBarisDosen.SelectedValue);

            if (!modelSinkronisasi.getDaftarDosenCari(kdPerguruanTinggi, nama))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelSinkronisasi.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvDaftarDosen, modelSinkronisasi.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelSinkronisasi.errorMessage);

            if (modelSinkronisasi.numOfRecords < 1)
            {
                pagingDaftarDosen.setPaging(int.Parse(ddlJmlBarisDosen.SelectedValue), 1);
            }
        }

        protected void gvDaftarDosen_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["idPersonal"] = gvDaftarDosen.DataKeys[e.RowIndex]["id_personal"].ToString();
            objModal.ShowModal(this.Page, "modalEditStatus");
            isiDdlStsDosen(ref ddlStsDosen);
            ddlStsDosen.SelectedValue = gvDaftarDosen.DataKeys[e.RowIndex]["kd_sts_aktif"].ToString();
            lblNamaDosenEdit.Text = gvDaftarDosen.DataKeys[e.RowIndex]["nama"].ToString();
        }

        protected void lbSimpanStsDosen_Click(object sender, EventArgs e)
        {
            if (modelSinkronisasi.updateStsDosen(Guid.Parse(ViewState["idPersonal"].ToString()), ddlStsDosen.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Update status dosen berhasil");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelSinkronisasi.errorMessage);
            }
            objModal.HideModal(this.Page, "modalEditStatus");
            isiGvDaftarDosen(0);
        }

        protected void lbBatalUpdate_Click(object sender, EventArgs e)
        {
            objModal.HideModal(this.Page, "modalEditStatus");
            isiGvDaftarDosen(0);
        }

        private void isiDdlStsDosen(ref DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelTendikNonDosen.getListStatusKepegawaian(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "status_aktif", "kd_status_aktif");
            ddl.SelectedIndex = 0;
        }

        #endregion

        #region Sinkronisasi

        private void getDataPddikti()
        {
            NpgsqlConnection conn = new NpgsqlConnection(cs);
            conn.Open();

            if (tbNIDN.Text.Trim() != "")
            {
                const string BaseUrl = "https://api.kemdikbud.go.id:8443/pddikti-api/1.2/";
                //const string Token = "00000000-0000-0000-0000-000000000000";
                string Token = "00000000-0000-0000-0000-000000000000";
                var dtToken = new DataTable();
                modelSinkronisasi.getTokenPddikti(ref dtToken);
                if (dtToken.Rows.Count > 0)
                {
                    Token = dtToken.Rows[0]["token"].ToString();
                }
                ViewState["token_verifikasi"] = Token;
                var client = new RestClient();
                client.BaseUrl = new Uri(BaseUrl);

                var request = new RestRequest(Method.GET)
                {
                    Resource = "dosen/"
                };

                //request.AddParameter("q", "istiyadi", ParameterType.QueryString); // adds to POST or URL querystring based on Method
                request.AddParameter("nidn", tbNIDN.Text.Trim(), ParameterType.QueryString);

                // easily add HTTP Headers
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", $"Bearer {Token}");

                // execute the request
                var response = client.Execute(request);

                var aa = response.StatusCode.ToString();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var deserialize = new JsonDeserializer();
                    var listDosen = deserialize.Deserialize<List<Models.PDDIKTI.Dosen>>(response);

                    if (listDosen.Count > 0)
                    {
                        lbSInkronisasi.Visible = true;
                        foreach (var dosen in listDosen)
                        {
                            string kdPT = dosen.kode_pt;
                            ViewState["kdPT"] = kdPT;
                            string idPddikti = dosen.id;
                            ViewState["idPddikti"] = idPddikti;
                            string nidn = dosen.nidn;
                            ViewState["nidn"] = nidn;
                            string nip = dosen.nip;
                            if (nip == null)
                            {
                                nip = "0";
                            }
                            string noKTP = dosen.nik;
                            if (noKTP == null)
                            {
                                noKTP = "0";
                            }
                            ViewState["noKTP"] = noKTP;

                            string nama = dosen.nama;
                            ViewState["nama"] = nama;
                            string gelarDepan = dosen.gelar_depan;
                            ViewState["gelarDepan"] = gelarDepan;
                            string gelarBelakang = dosen.gelar_belakang;
                            ViewState["gelarBelakang"] = gelarBelakang;
                            string namaLengkap = (gelarDepan + " " + nama + " " + gelarBelakang).ToString();

                            string jenjangPendidikan = dosen.pendidikan_terakhir;

                            string kdJenjangPendidikan = "11";
                            if (jenjangPendidikan == "S1")
                            {
                                kdJenjangPendidikan = "5";
                            }
                            else if (jenjangPendidikan == "S2" || jenjangPendidikan == "S2 Terapan")
                            {
                                kdJenjangPendidikan = "6";
                            }
                            else if (jenjangPendidikan == "S3" || jenjangPendidikan == "S3 Terapan")
                            {
                                kdJenjangPendidikan = "7";
                            }
                            else if (jenjangPendidikan == "Sp-1")
                            {
                                kdJenjangPendidikan = "9";
                            }
                            else if (jenjangPendidikan == "Sp-2")
                            {
                                kdJenjangPendidikan = "10";
                            }
                            else if (jenjangPendidikan == "Profesi")
                            {
                                kdJenjangPendidikan = "8";
                            }
                            else if (jenjangPendidikan == "D3")
                            {
                                kdJenjangPendidikan = "3";
                            }
                            else if (jenjangPendidikan == "D4")
                            {
                                kdJenjangPendidikan = "4";
                            }
                            else
                            {
                                kdJenjangPendidikan = "11";
                            }
                            ViewState["kdJenjangPendidikan"] = kdJenjangPendidikan;

                            ViewState["jenjangPendidikan"] = jenjangPendidikan;
                            string jenisKelamin = dosen.jenis_kelamin;
                            ViewState["jenisKelamin"] = jenisKelamin;
                            string tempatLahir = dosen.tempat_lahir;
                            ViewState["tempatLahir"] = tempatLahir;
                            string tglLagir = dosen.tgl_lahir;
                            ViewState["tglLagir"] = tglLagir;

                            string tglLagir1 = dosen.tgl_lahir;
                            ViewState["tglLagir1"] = tglLagir1;

                            string surel = dosen.email;
                            if (surel == null)
                            {
                                surel = "";
                            }
                            ViewState["surel"] = surel;
                            string alamat = (dosen.alamat.jalan + " " + dosen.alamat.kelurahan + " " + dosen.alamat.kab_kota.nama).ToString();
                            ViewState["alamat"] = alamat;

                            string kdProdi = dosen.kode_prodi;
                            if (kdProdi == null)
                            {
                                kdProdi = "";
                            }
                            ViewState["kdProdi"] = kdProdi;
                            string jabatanFungsional = dosen.jabatan_fungsional.nama;
                            string idJabatanFungsional = dosen.jabatan_fungsional.id.ToString();
                            if (idJabatanFungsional == "31" || idJabatanFungsional == "52" || idJabatanFungsional == null) // 52 = Peneliti
                            {
                                idJabatanFungsional = "0";
                            }
                            else if (idJabatanFungsional == "40" || idJabatanFungsional == "41")
                            {
                                idJabatanFungsional = "1";
                            }
                            else if (idJabatanFungsional == "43" || idJabatanFungsional == "44")
                            {
                                idJabatanFungsional = "2";
                            }
                            else if (idJabatanFungsional == "46" || idJabatanFungsional == "47" || idJabatanFungsional == "48")
                            {
                                idJabatanFungsional = "3";
                            }
                            else if (idJabatanFungsional == "50" || idJabatanFungsional == "51")
                            {
                                idJabatanFungsional = "4";
                            }

                            ViewState["idJabatanFungsional"] = idJabatanFungsional;
                            string stsAktif = dosen.status_keaktifan.nama;
                            string kdStsAktif = dosen.status_keaktifan.id.ToString();

                            if (kdStsAktif == "1") //Aktif
                            {
                                kdStsAktif = "1";
                            }
                            else if (kdStsAktif == "20") //Cuti
                            {
                                kdStsAktif = "2";
                            }
                            else if (kdStsAktif == "23") //Pensiun
                            {
                                kdStsAktif = "4";
                            }
                            else if (kdStsAktif == "22") //Almarhum
                            {
                                kdStsAktif = "6";
                            }
                            else if (kdStsAktif == "25") //Tugas di Instansi Lain
                            {
                                kdStsAktif = "7";
                            }
                            else if (kdStsAktif == "27") //Tugas Belajar
                            {
                                kdStsAktif = "A";
                            }
                            else if (kdStsAktif == "24") //Ijin Belajar
                            {
                                kdStsAktif = "B";
                            }
                            else if (kdStsAktif == "26") //Ganti NIDN
                            {
                                kdStsAktif = "C";
                            }
                            else
                            {
                                kdStsAktif = "0";
                            }

                            ViewState["kdStsAktif"] = kdStsAktif;

                            string noTelepon = dosen.telepon;
                            if (noTelepon == null)
                            {
                                noTelepon = "";
                            }
                            ViewState["noTelepon"] = noTelepon;

                            string noHp = "";
                            if (dosen.handphone == null)
                            {
                                noHp = "";
                            }
                            else
                            {
                                noHp = dosen.handphone.ToString();
                            }
                            ViewState["noHp"] = noHp;

                            string tglLahirIndo = "0001-01-01";
                            if (tglLagir == null)
                            {
                                tglLagir = tglLahirIndo;
                            }
                            else
                            {
                                var dtTglLahirIndo = new DataTable();
                                modelSinkronisasi.getTanggalIndo(ref dtTglLahirIndo, tglLagir);
                                tglLahirIndo = dtTglLahirIndo.Rows[0]["tgl_indo"].ToString();
                            }

                            getProdiByKode(kdPT, kdProdi);

                            if (objLogin.kdInstitusi.ToString() == kdPT)
                            {

                                lblNidnPddikti.Text = nidn;
                                lblNamaPddikti.Text = namaLengkap;
                                lblJafungPddikti.Text = jabatanFungsional;

                                lblJenjangPendPddikti.Text = jenjangPendidikan;
                                lblTmpTglLahirPddikti.Text = (tempatLahir + "/" + tglLahirIndo);
                                //lblAlamatPddikti.Text = alamat;
                                //lblNoKtpPddikti.Text = noKTP;
                                //lblNoTeleponPddikti.Text = noTelepon;
                                //lblNoHpPddikti.Text = noHp;
                                //lblSurelddikti.Text = surel;
                                //lblStatusddikti.Text = stsAktif;

                                getDataSimlitabmas(nidn);
                            }
                            else
                            {
                                clearDataDosen();
                                lbSInkronisasi.Visible = false;
                                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "homebase dosen bukan dari " + objLogin.namaInstitusi.ToString());
                            }
                        }
                    }
                    else
                    {
                        clearDataDosen();
                        lbSInkronisasi.Visible = false;
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "NIDN tidak ditemukan di PDDIKTI");
                    }
                }
                else
                {
                    var res = response.StatusDescription.ToString();

                    if (response.StatusDescription == "Unauthorized")
                    {
                        string tokenLama = "";
                        DataTable dt = new DataTable();
                        modelSinkronisasi.getTokenPddikti(ref dt);
                        if (dt.Rows.Count > 0)
                        {
                            tokenLama = dt.Rows[0]["token"].ToString();
                            generateToken();
                        }
                    }
                    else if (response.StatusDescription == "Not Found")
                    {
                        string tokenLama = "";
                        DataTable dt = new DataTable();
                        modelSinkronisasi.getTokenPddikti(ref dt);
                        if (dt.Rows.Count > 0)
                        {
                            tokenLama = dt.Rows[0]["token"].ToString();
                            generateToken();
                        }
                    }
                    

                    //Console.WriteLine($"Response : {response.StatusCode}");
                }
            }
        }

        private void clearDataDosen()
        {
            lblNidnPddikti.Text = "-";
            lblNamaPddikti.Text = "-";
            lblJafungPddikti.Text = "-";
            lblJenjangPendPddikti.Text = "-";
            lblTmpTglLahirPddikti.Text = "-";
            lblProdiPddikti.Text = "-";

            lblNidnSimlitabmas.Text = "-";
            lblNamaSimlitabmas.Text = "-";
            lblProdiSimlitabmas.Text = "-";
            lblJafungSimlitabmas.Text = "-";
            lblJenjangPendSimlitabmas.Text = "-";
            lblTmpTglLahirSimlitabmas.Text = "-";
        }

        protected void generateToken()
        {
            const string ConsumerKey = "1phcQgOuXEwm4uh6kN0YF2Bwf9FIA5OK";
            const string ConsumerSecret = "1qB56sSuOBB6pSDRxuFV6TR6KDkw5VGO";

            const string BaseUrl = "https://api.kemdikbud.go.id:8443/pddikti-api/oauth2/token";

            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(ConsumerKey, ConsumerSecret);

            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var deserialize = new JsonDeserializer();
                var token = deserialize.Deserialize<Models.PT.Token>(response);
                modelSinkronisasi.updateToken(token.access_token);
                getDataPddikti();
            }
            else
            {
            }
        }

        private void getProdiByKode(string KodePT, string KodeProdi)
        {
            const string BaseUrl = "https://api.kemdikbud.go.id:8443/pddikti-api/1.2/";
            //const string BaseUrl = "https://api.kemdikbud.go.id:8243/pddikti/1.2/"; lama
            string Token = "00000000-0000-0000-0000-000000000000";
            var dtToken = new DataTable();
            modelSinkronisasi.getTokenPddikti(ref dtToken);
            if (dtToken.Rows.Count > 0)
            {
                Token = dtToken.Rows[0]["token"].ToString();
            }

            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);

            var request = new RestRequest(Method.GET)
            {
                Resource = "pt/{id-pt}/prodi/{id-prodi}"
            };

            request.AddParameter("id-pt", KodePT, ParameterType.UrlSegment);
            request.AddParameter("id-prodi", KodeProdi, ParameterType.UrlSegment);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {Token}");

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var deserialize = new JsonDeserializer();
                var listProdi = deserialize.Deserialize<List<Prodi>>(response);

                foreach (var prodi in listProdi)
                {
                    string idProdiPdpt = prodi.id;
                    string namaProdi = prodi.nama;

                    lblProdiPddikti.Text = namaProdi;
                }
            }
            else
            {
                Console.WriteLine($"Response : {response.StatusCode}");
            }

        }

        private void getDataSimlitabmas(string nidn)
        {
            DataTable dt = new DataTable();
            string kdPerguruanTinggi = objLogin.kdInstitusi.ToString();

            if (tbNIDN.Text.Trim() != "")
            {
                if (!modelSinkronisasi.getDaftarDosenFilterNidn(ref dt, kdPerguruanTinggi, nidn))
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelSinkronisasi.errorMessage);

                if (dt.Rows.Count > 0)
                {
                    lblNidnSimlitabmas.Text = dt.Rows[0]["nidn"].ToString();
                    lblNamaSimlitabmas.Text = dt.Rows[0]["nama"].ToString();
                    lblProdiSimlitabmas.Text = dt.Rows[0]["nama_program_studi"].ToString();
                    lblJafungSimlitabmas.Text = dt.Rows[0]["jabatan_fungsional"].ToString();
                    lblJenjangPendSimlitabmas.Text = dt.Rows[0]["jenjang_pendidikan"].ToString();

                    string tempatLahir = dt.Rows[0]["tempat_lahir"].ToString();
                    if (tempatLahir == null)
                    {
                        tempatLahir = "";
                    }
                    string tglLahir = dt.Rows[0]["tgl_lahir_indo"].ToString();
                    if (tglLahir == null)
                    {
                        tglLahir = "";
                    }

                    lblTmpTglLahirSimlitabmas.Text = (tempatLahir + "/" + tglLahir).ToString();
                }
            }
        }

        protected void lbKembali_Click(object sender, EventArgs e)
        {
            tbNIDN.Text = "";
            isiGvDaftarDosen(0);
            mvDaftarDosen.SetActiveView(vDaftarDosen);
        }

        protected void lbSInkronisasi_Click(object sender, EventArgs e)
        {
            string kd_perguruan_tinggi = ViewState["kdPT"].ToString();
            string kd_program_studi = ViewState["kdProdi"].ToString();
            string kd_jenjang_pendidikan_tertinggi = ViewState["kdJenjangPendidikan"].ToString();
            string nidn = ViewState["nidn"].ToString();
            string nama = ViewState["nama"].ToString();
            string gelar_akademik_depan = ViewState["gelarDepan"].ToString();
            string gelar_akademik_belakang = ViewState["gelarBelakang"].ToString();
            string kd_jenis_kelamin = ViewState["jenisKelamin"].ToString();
            string tempat_lahir = ViewState["tempatLahir"].ToString();
            string tanggal_lahir = ViewState["tglLagir1"].ToString();
            string nomor_ktp = ViewState["noKTP"].ToString();
            string nip_baru = "";
            string kd_jabatan_fungsional = ViewState["idJabatanFungsional"].ToString();
            string alamat = ViewState["alamat"].ToString();
            string nomor_telepon = ViewState["noTelepon"].ToString();
            string nomor_hp = ViewState["noHp"].ToString();
            string surel = ViewState["surel"].ToString();
            if (surel == "" || surel == null)
            {
                surel = "";
            }
            string kd_sts_aktif = ViewState["kdStsAktif"].ToString();
            string id_pdpt = ViewState["idPddikti"].ToString();
            string kdPos = "0";

            DataTable dt = new DataTable();

            if (modelSinkronisasi.insertTmpDosen(kd_perguruan_tinggi, kd_program_studi, nidn,
                            nama, gelar_akademik_depan, gelar_akademik_belakang,
                            kd_jenjang_pendidikan_tertinggi, kd_jenis_kelamin, kd_jabatan_fungsional,
                            tempat_lahir, tanggal_lahir, nomor_ktp, nip_baru,
                            alamat, kdPos, nomor_telepon, nomor_hp,
                            surel, kd_sts_aktif, id_pdpt))
            {
                if (modelSinkronisasi.SinkronisasiWithIDPDPT(ref dt, nidn, id_pdpt))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sinkronisasi data berhasil");
                    getDataSimlitabmas(nidn);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Sinkronisasi data gagal" + modelSinkronisasi.errorMessage);
                }
            }
        }

        #endregion        
    }
}