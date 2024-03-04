using RestSharp;
using RestSharp.Serialization.Json;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class sinta : System.Web.UI.UserControl
    {
        Models.login objLogin;
        readonly Models.Pengusul.berandaPengusul objPengusul = new Models.Pengusul.berandaPengusul();
        readonly uiNotify noty = new uiNotify();
        readonly uiModal obj_uiMdl = new uiModal();

        //const string BaseURL = "https://api.sinta.kemdikbud.go.id";
        const string BaseURL = "http://apisinta.kemdikbud.go.id";
        const string username = "SIMLITABKES";
        const string password = "joinSinta&kemenkesJaya";
        const string UniqueIDSinta = "341903311";
        const string ProductionMode = "prod";
        const string DevelopmentMode = "dev";
        const string IdSintaType = "id";
        const string NIDNType = "nidn";

        string cs = ConfigurationManager.ConnectionStrings["productionDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];
        }

        public void isiSintaPengusul()
        {
            var dt = new DataTable();
            objPengusul.getSinta(ref dt, objLogin.idPersonal);
            if (dt.Rows.Count > 0)
            {
                lblSintaId.Text = dt.Rows[0]["id_sinta"].ToString();
                lblSintaSkor.Text = dt.Rows[0]["skor_sinta"].ToString();
                lblNatRank.Text = dt.Rows[0]["rangking_nasional"].ToString();
                lblAffRank.Text = dt.Rows[0]["rangking_afiliasi"].ToString();

                lblScopusid.Text = dt.Rows[0]["id_scopus"].ToString();
                lblScopushindex.Text = dt.Rows[0]["hindex"].ToString();
                lblScopusCitation.Text = dt.Rows[0]["jml_sitasi"].ToString();
                lblScopusArticle.Text = dt.Rows[0]["jml_dokumen"].ToString();

                lblGooglescholarid.Text = dt.Rows[0]["id_google_scholar"].ToString();
                lblGooglehindex.Text = dt.Rows[0]["hindex_google_scholar"].ToString();
                LblGoogleCitation.Text = dt.Rows[0]["jml_sitasi_google_scholar"].ToString();
                lblGoogleArticle.Text = dt.Rows[0]["jml_artikel_google_scholar"].ToString();
                lblGoogleI10.Text = dt.Rows[0]["i_10_hindex_google_scholar"].ToString();

            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Data SINTA masih kosong !<br/> Silahkan klik tombol edit untuk mengisi data SINTA..");

                //if (isisinta("nidn", objLogin.nidn.ToString()) == true)
                //{
                //    //isiEditSintaPengusul();
                //    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sinkronisasi sinta dengan nidn berhasil");
                //    isiSintaPengusul();
                //    mvSinta.SetActiveView(vDaftar);

                //}
                //else
                //{
                //    lbEdit.Text = "Masukan Id SInta";
                //    //lbKet.Text = "Sinkronisasi sinta anda tidak berhasil, silahkan masukan id sinta anda";
                //    //lbKet.Visible = true;
                //}


            }
        }

        private bool generateSintaToken(ref string newToken)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseURL);
            //client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest(Method.POST)
            {
                Resource = "consumer/login"
            };
            request.AddParameter("username", username, ParameterType.GetOrPost);
            request.AddParameter("password", password, ParameterType.GetOrPost);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var deserialize = new JsonDeserializer();
                var token = deserialize.Deserialize<Token>(response);
                newToken = token.token;
                return true;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Koneksi ke server SINTA bermasalah. Silahkan nanti dicoba lagi... ");
                return false;
            }
        }

        private bool getSintaAuthor(string idSinta, ref SintaAuthor result)
        {
            string token = null;
            if (!generateSintaToken(ref token)) return false;

            var client = new RestClient
            {
                BaseUrl = new Uri(BaseURL)
            };

            var request = new RestRequest(Method.POST)
            {
                //Resource = "v2/author/detail/overview/{id}"
                Resource = "v3/{env}/{uniq}/author/profile/{type}/{id}"
            };

            request.AddParameter("env", ProductionMode, ParameterType.UrlSegment);
            request.AddParameter("uniq", UniqueIDSinta, ParameterType.UrlSegment);
            request.AddParameter("type", IdSintaType, ParameterType.UrlSegment);
            request.AddParameter("id", idSinta, ParameterType.UrlSegment);

            // Cek Token di database
            //DataTable dt = new DataTable();
            //objPengusul.getTokenSinta(ref dt);
            //var token = dt.Rows[0]["token"].ToString()

            // easily add HTTP Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");

            // execute the request
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    var deserialize = new JsonDeserializer();
                    result = deserialize.Deserialize<SintaAuthor>(response);
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                    return false;
                }

                // TODO Dirty hack karena json root dengan array kosong tidak bisa di deserialize
                //if (response.Content.Trim().Length > 10)
                if (result.status == "error")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", result.message);
                    //noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    //    $"ID Sinta <b>{idSinta}</b> tidak dapat ditemukan !");
                    return false;
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Koneksi ke server SINTA bermasalah. Silahkan dicoba lagi nanti... ");
                return false;
            }

            return true;
        }

        private bool simpanAuthorSinta(Guid idPersonal, Author author)
        {
            var rankingNational = 0;
            int.TryParse(author.affiliation_score_v3_overall, out int rankingAffiliation);
            int.TryParse(author.id, out int idSinta);
            double.TryParse(author.sinta_score_v3_overall, out double skorSinta);
            int.TryParse(author.google.total_document, out int jmlArtikelGoogleSchoolar);
            int.TryParse(author.google.total_citation, out int jmlSitasiGoogleSchoolar);
            int.TryParse(author.google.h_index, out int hIndexGoogleSchoolar);
            int.TryParse(author.google.i10_index, out int i10IndexGoogleSchoolar);
            int.TryParse(author.scopus.h_index, out int hIndexScopus);
            int.TryParse(author.scopus.total_document, out int jmlDokumenScopus);
            int.TryParse(author.scopus.total_citation, out int jmlSitasiScopus);

            if (!objPengusul.insupSintaScopus(
                    idPersonal, idSinta, rankingNational, rankingAffiliation,
                    skorSinta,
                    jmlArtikelGoogleSchoolar,
                    jmlSitasiGoogleSchoolar,
                    hIndexGoogleSchoolar,
                    i10IndexGoogleSchoolar,
                    author.fullname,
                    "-",
                    hIndexScopus,
                    jmlDokumenScopus,
                    jmlSitasiScopus,
                    "1900-01-01",
                    "-"
                ))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi kesalahan", objPengusul.errorMessage);
                return false;
            }

            return true;
        }

        private void cekIdSinta(string idSinta)
        {
            var result = new SintaAuthor();
            if (getSintaAuthor(idSinta, ref result))
            {
                Session["Author"] = result;

                foreach (var author in result.results.authors)
                {
                    lblNamaAuthor.Text = author.fullname;
                    //lblSintaId2.Text = tbIdSinta.Text.Trim();
                    lblSintaSkor2.Text = author.sinta_score_v3_overall.ToString();
                    lblNatRank2.Text = "-"; //(data.author.ranking_in_national == null) ? "-" : data.author.ranking_in_national.ToString();
                    lblAffRank2.Text = (author.affiliation_score_v3_overall == null) ? "-" : author.affiliation_score_v3_overall;

                    lblGooglescholarid2.Text = "-"; //google_id;
                    lblGooglehindex2.Text = author.google.h_index;
                    lblGoogleArticle2.Text = author.google.total_document;
                    LblGoogleCitation2.Text = author.google.total_citation;
                    lblGoogleI102.Text = author.google.i10_index;

                    lblScopusid2.Text = "-"; //data.author.scopus_id;
                    lblScopushindex2.Text = author.scopus.h_index;
                    lblScopusArticle2.Text = author.scopus.total_document;
                    lblScopusCitation2.Text = author.scopus.total_citation;
                    //lblthnakhir.Text = data.author.last_update_score;
                }

            }
        }

        private void isiEditSintaPengusul()
        {
            lblNamaAuthor.Text = "-";
            //lblSintaId2.Text = "-";
            lblSintaSkor2.Text = "-";
            lblNatRank2.Text = "-";
            lblAffRank2.Text = "-";

            lblGooglescholarid2.Text = "-";
            lblGooglehindex2.Text = "-";
            lblGoogleArticle2.Text = "-";
            LblGoogleCitation2.Text = "-";
            lblGoogleI102.Text = "-";

            //lblScopusid2.Text = "-";
            lblScopushindex2.Text = "-";
            lblScopusArticle2.Text = "-";
            lblScopusCitation2.Text = "-";
            //lblthnakhir.Text = "-";

            if (lblSintaId.Text.Length >= 2)
            {
                tbIdSinta.Text = lblSintaId.Text;
                cekIdSinta(lblSintaId.Text);
            }
        }

        protected void lbEdit_click(object sender, EventArgs e)
        {
            mvSinta.SetActiveView(vEdit);
            ViewState.Remove("Author");
            isiEditSintaPengusul();
        }

        protected void lbSinkronisasi_Click(object sender, EventArgs e)
        {
            if (lblSintaId.Text.Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Id SINTA masih kosong ! Silahkan klik tombol edit untuk mengisi data Id SINTA..");
                return;
            }

            var result = new SintaAuthor();
            if (getSintaAuthor(lblSintaId.Text, ref result))
            {
                foreach (var author in result.results.authors)
                {
                    if (simpanAuthorSinta(Guid.Parse(objLogin.idPersonal), author))
                    {
                        isiSintaPengusul();
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                            "Sinkronisasi data SINTA berhasil...");

                        return;
                    }
                }
            }

            //isisinta("id", lblSintaId.Text);
            //noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //        "Id SINTA masih kosong !<br/> Silahkan klik tombol edit untuk mengisi data Id SINTA..");
        }

        //private Boolean isisinta(string flag, String id)
        //{
        //    Boolean hasil = false;
        //    NpgsqlConnection conn = new NpgsqlConnection(cs);
        //    //conn.Open();

        //    if (!String.IsNullOrEmpty(lblSintaId.Text))
        //    {
        //        string urlSinta = Application["urlSinta1"].ToString();
        //        string sintaApiKey = Application["tokensinta1"].ToString();
        //        var url = urlSinta + flag + "=" + id + "&api_key=" + sintaApiKey;
        //        var n = GetData(url);
        //        if (n == "{\"code\":200,\"data\":[\"empty\"]}")
        //        {
        //            hasil = false;
        //            return false;
        //        }
        //        var k = (SintaAuthor)Newtonsoft.Json.JsonConvert.DeserializeObject(n, typeof(SintaAuthor));
        //        if (k != null)
        //        {
        //            conn.Open();
        //            // Create update command.
        //            NpgsqlCommand command2 = new NpgsqlCommand(@"penelitian.insup_sinta_scopus", conn);
        //            command2.CommandType = System.Data.CommandType.StoredProcedure;
        //            command2.Parameters.Add(new NpgsqlParameter("p_id_personal", NpgsqlTypes.NpgsqlDbType.Uuid));
        //            command2.Parameters.Add(new NpgsqlParameter("p_id_sinta", NpgsqlTypes.NpgsqlDbType.Bigint));
        //            command2.Parameters.Add(new NpgsqlParameter("p_rangking_nasional", NpgsqlTypes.NpgsqlDbType.Integer));
        //            command2.Parameters.Add(new NpgsqlParameter("p_rangking_afiliasi", NpgsqlTypes.NpgsqlDbType.Integer));
        //            command2.Parameters.Add(new NpgsqlParameter("p_skor_sinta", NpgsqlTypes.NpgsqlDbType.Numeric));
        //            command2.Parameters.Add(new NpgsqlParameter("p_jml_artikel_google_scholar", NpgsqlTypes.NpgsqlDbType.Integer));
        //            command2.Parameters.Add(new NpgsqlParameter("p_jml_sitasi_google_scholar", NpgsqlTypes.NpgsqlDbType.Integer));
        //            command2.Parameters.Add(new NpgsqlParameter("p_hindex_google_scholar", NpgsqlTypes.NpgsqlDbType.Integer));
        //            command2.Parameters.Add(new NpgsqlParameter("p_i_10_hindex_google_scholar", NpgsqlTypes.NpgsqlDbType.Integer));
        //            command2.Parameters.Add(new NpgsqlParameter("p_nama_personal_sinta", NpgsqlTypes.NpgsqlDbType.Varchar));
        //            command2.Parameters.Add(new NpgsqlParameter("p_id_scopus", NpgsqlTypes.NpgsqlDbType.Varchar));
        //            command2.Parameters.Add(new NpgsqlParameter("p_hindex", NpgsqlTypes.NpgsqlDbType.Integer));
        //            command2.Parameters.Add(new NpgsqlParameter("p_jml_dokumen", NpgsqlTypes.NpgsqlDbType.Integer));
        //            command2.Parameters.Add(new NpgsqlParameter("p_jml_sitasi", NpgsqlTypes.NpgsqlDbType.Integer));
        //            command2.Parameters.Add(new NpgsqlParameter("p_thn_terakhir_publikasi", NpgsqlTypes.NpgsqlDbType.Date));
        //            command2.Parameters.Add(new NpgsqlParameter("p_id_google_scholar", NpgsqlTypes.NpgsqlDbType.Varchar));

        //            // Prepare the command2
        //            command2.Prepare();
        //            command2.Parameters[0].Value = Guid.Parse(objLogin.idPersonal);
        //            command2.Parameters[1].Value = k.data.id;
        //            command2.Parameters[2].Value = k.data.rank_in_nat;
        //            command2.Parameters[3].Value = k.data.rank_in_affil;
        //            command2.Parameters[4].Value = k.data.sinta_score;
        //            command2.Parameters[5].Value = k.data.google_article;
        //            command2.Parameters[6].Value = k.data.google_citations;
        //            command2.Parameters[7].Value = k.data.google_hindex;
        //            command2.Parameters[8].Value = k.data.google_i10;
        //            command2.Parameters[9].Value = k.data.fullname;
        //            command2.Parameters[10].Value = k.data.scopus_id;
        //            command2.Parameters[11].Value = k.data.scopus_hindex;
        //            command2.Parameters[12].Value = k.data.scopus_article;
        //            command2.Parameters[13].Value = k.data.scopus_citation;
        //            command2.Parameters[14].Value = DateTime.Parse("1900-01-01");
        //            command2.Parameters[15].Value = k.data.google_id;
        //            // Execute SQL command.
        //            NpgsqlDataReader dr2 = command2.ExecuteReader();
        //            while (dr2.Read())
        //            {
        //                //if (String.IsNullOrEmpty(dr2[0].ToString()))
        //                //{
        //                if (dr2[0].ToString() == "True")
        //                {
        //                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Sinkronisasi sinta berhasil");
        //                    isiSintaPengusul();
        //                }
        //                else
        //                {
        //                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "gagal sinkronisasi data sinta");

        //                }
        //                //}
        //            }
        //            conn.Close();
        //            hasil = true;
        //        }
        //    }

        //    return hasil;
        //}

        // Returns JSON string
        //public string GetData(string url)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    try
        //    {
        //        WebResponse response = request.GetResponse();
        //        using (Stream responseStream = response.GetResponseStream())
        //        {
        //            StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
        //            return reader.ReadToEnd();
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        WebResponse errorResponse = ex.Response;
        //        using (Stream responseStream = errorResponse.GetResponseStream())
        //        {
        //            StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
        //            String errorText = reader.ReadToEnd();
        //            // log errorText
        //        }
        //        throw;
        //    }
        //}

        protected void lbCek_Click(object sender, EventArgs e)
        {
            if (tbIdSinta.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Id masih kosong");
                return;
            }

            //var dt = new DataTable();
            //objPengusul.getSintabyId(ref dt, tbIdSinta.Text);
            //if (dt.Rows.Count > 0)
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
            //        $"Id sinta ini sudah ada di dalam database di miliki oleh {dt.Rows[0]["nama_personal_sinta"]} ({dt.Rows[0]["nama_institusi"]})</br>" +
            //        "Silahkan hubungi operator Simlitabkes Institusi anda untuk bantuan");
            //    return;
            //}

            cekIdSinta(tbIdSinta.Text.Trim());
        }

        protected void lbClose_click(object sender, EventArgs e)
        {
            mvSinta.SetActiveView(vDaftar);
            isiSintaPengusul();

            Session["Author"] = null;
        }

        protected void lbBatal_click(object sender, EventArgs e)
        {
            mvSinta.SetActiveView(vDaftar);
            isiSintaPengusul();

            Session["Author"] = null;
        }

        protected void lbSimpan_click(object sender, EventArgs e)
        {
            if (Session["Author"] != null)
            {
                obj_uiMdl.ShowModal(this.Page, "myModal");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Lakukan pengecekan Id SINTA terlebih dahulu !");
            }
        }

        protected void lbSimpanSinta_Click(object sender, EventArgs e)
        {
            var result = (SintaAuthor)Session["Author"];
            foreach (var author in result.results.authors)
            {
                if (simpanAuthorSinta(Guid.Parse(objLogin.idPersonal), author))
                {
                    mvSinta.SetActiveView(vDaftar);
                    isiSintaPengusul();
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil...");

                    Session["Author"] = null;
                    return;
                }
            }
        }

        #region Model API SINTA
        public class Token
        {
            public string token { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
        }

        public class SintaAuthor
        {
            public string status { get; set; }
            public string message { get; set; }
            public int identifier { get; set; }
            public string remote_ip { get; set; }
            public Results results { get; set; }
        }

        public class Results
        {
            public List<Author> authors { get; set; }
        }

        public class Author
        {
            public string id { get; set; }
            public string NIDN { get; set; }
            public string fullname { get; set; }
            public string country { get; set; }
            public string academic_grade_raw { get; set; }
            public string academic_grade { get; set; }
            public string gelar_depan { get; set; }
            public string gelar_belakang { get; set; }
            public string last_education { get; set; }
            public string sinta_score_v2_overall { get; set; }
            public string sinta_score_v2_3year { get; set; }
            public string sinta_score_v3_overall { get; set; }
            public string sinta_score_v3_3year { get; set; }
            public string affiliation_score_v3_overall { get; set; }
            public string affiliation_score_v3_3year { get; set; }
            public Affiliation affiliation { get; set; }
            public Programs programs { get; set; }
            public List<object> subjects { get; set; }
            public Scopus scopus { get; set; }
            public Wos wos { get; set; }
            public Garuda garuda { get; set; }
            public Google google { get; set; }
        }

        public class Affiliation
        {
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
        }

        public class Programs
        {
            public string code { get; set; }
            public string level { get; set; }
            public string name { get; set; }
        }

        public class Scopus
        {
            public string total_document { get; set; }
            public string total_citation { get; set; }
            public string total_cited_doc { get; set; }
            public string h_index { get; set; }
            public string i10_index { get; set; }
            public string g_index { get; set; }
            public string g_index_3year { get; set; }
        }

        public class Wos
        {
            public string total_document { get; set; }
            public string total_citation { get; set; }
            public string total_cited_doc { get; set; }
            public string h_index { get; set; }
        }

        public class Garuda
        {
            public string total_document { get; set; }
            public string total_citation { get; set; }
            public string total_cited_doc { get; set; }
        }

        public class Google
        {
            public string total_document { get; set; }
            public string total_citation { get; set; }
            public string total_cited_doc { get; set; }
            public string h_index { get; set; }
            public string i10_index { get; set; }
            public string g_index { get; set; }
        }

        //class SintaData
        //{
        //    public string code { get; set; }
        //    public Data data { get; set; }
        //}

        //class Data
        //{
        //    public long sinta_id { get; set; }
        //    public string scopus_id { get; set; }
        //    public string fullname { get; set; }
        //    public string NIDN { get; set; }
        //    public decimal? sinta_score { get; set; }
        //    public int? scopus_hindex { get; set; }
        //    public int? scopus_citation { get; set; }
        //    public int? scopus_article { get; set; }
        //    public int? google_hindex { get; set; }
        //    public int? google_citations { get; set; }
        //    public int? google_article { get; set; }
        //    public int? google_i10 { get; set; }
        //    public int? rank_in_nat { get; set; }
        //    public int? rank_in_affil { get; set; }
        //    public int? last_scopus_pub { get; set; }
        //    public string google_id { get; set; }
        //}




        //public class Token
        //{
        //    public string token { get; set; }
        //    public int expires_in { get; set; }
        //    public string refresh_token { get; set; }
        //}

        //public class Afiliation
        //{
        //    public int id { get; set; }
        //    public string kode { get; set; }
        //    public string name { get; set; }
        //}

        //public class Prodi
        //{
        //    public string kode { get; set; }
        //    public string nama { get; set; }
        //    public string jenjang { get; set; }
        //}

        //public class Subject
        //{
        //    public string subject_id { get; set; }
        //    public string subject_name { get; set; }
        //}

        //public class ScopusDetail
        //{
        //    public int citation { get; set; }
        //    public int hindex { get; set; }
        //    public int gindex { get; set; }
        //    public int i10 { get; set; }
        //    public int article { get; set; }
        //}

        //public class GoogleDetail
        //{
        //    public int citations { get; set; }
        //    public int hindex { get; set; }
        //    public int gindex { get; set; }
        //    public int i10 { get; set; }
        //    public int article { get; set; }
        //}

        //public class ScopusResearchOutput
        //{
        //    public int jumlah_journal { get; set; }
        //    public int jumlah_conference { get; set; }
        //    public int jumlah_other { get; set; }
        //}

        //public class ScopusQuartile
        //{
        //    public int undefined { get; set; }
        //}

        //public class PublicationAkreditation
        //{
        //    public int uncategorized { get; set; }
        //    public int S1 { get; set; }
        //    public int S2 { get; set; }
        //    public int S3 { get; set; }
        //    public int S4 { get; set; }
        //    public int S5 { get; set; }
        //    public int S6 { get; set; }
        //}

        //public class Author
        //{
        //    public int id { get; set; }
        //    public string nidn { get; set; }
        //    public string country { get; set; }
        //    public string google_id { get; set; }
        //    public string scopus_id { get; set; }
        //    public string name { get; set; }
        //    public double sinta_score { get; set; }
        //    public double sinta_score_3 { get; set; }
        //    public int sinta_score_v2 { get; set; }
        //    public double sinta_score_v2_3y { get; set; }
        //    public object ranking_in_national { get; set; }
        //    public object ranking_in_national_3year { get; set; }
        //    public object ranking_in_affiliation { get; set; }
        //    public object ranking_in_affiliation_3year { get; set; }
        //    public string last_update_score { get; set; }
        //    public Afiliation afiliation { get; set; }
        //    public Prodi prodi { get; set; }
        //    public List<Subject> subjects { get; set; }
        //    public List<object> scopus_document_per_year { get; set; }
        //    public List<object> google_cite_per_year { get; set; }
        //    public ScopusDetail scopus_detail { get; set; }
        //    public GoogleDetail google_detail { get; set; }
        //    public ScopusResearchOutput scopus_research_output { get; set; }
        //    public ScopusQuartile scopus_quartile { get; set; }
        //    public PublicationAkreditation publication_akreditation { get; set; }
        //}

        //public class Root
        //{
        //    public Author author { get; set; }
        //}

        #endregion

    }
}