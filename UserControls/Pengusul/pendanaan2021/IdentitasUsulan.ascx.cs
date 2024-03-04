using simlitekkes.Models;
using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.pendanaan2021
{
    public partial class IdentitasUsulan : System.Web.UI.UserControl
    {
        identitasUsulan modelIdentitas = new identitasUsulan();
        uiNotify noty = new uiNotify();
        daftarTendikNonDosen modelTendik = new daftarTendikNonDosen();
        login objLogin;
        private bool IsNew
        {
            get
            {
                if (ViewState["IsNew"] == null) ViewState["IsNew"] = false;
                return bool.Parse(ViewState["IsNew"].ToString());
            }
            set { ViewState["IsNew"] = value; }
        }

        private Guid IdUsulan
        {
            get
            {
                if (ViewState["IdUsulan"] == null) ViewState["IdUsulan"] = default(Guid);
                return Guid.Parse(ViewState["IdUsulan"].ToString());
            }
            set { ViewState["IdUsulan"] = value; }
        }

        public Guid IdUsulanKegiatan
        {
            get
            {
                if (ViewState["IdUsulanKegiatan"] == null) ViewState["IdUsulanKegiatan"] = default(Guid);
                return Guid.Parse(ViewState["IdUsulanKegiatan"].ToString());
            }
            set { ViewState["IdUsulanKegiatan"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (login)Session["objLogin"];
            if (!IsPostBack)
            {
                //InitIdentitasUsulan();
                modePemilihanSkema(true);
            }

            tkt.OnChildEventOccurs += new EventHandler(Child1_OnChildEventOccurs);
            tkt.OnSimpanClick += new EventHandler(SimpanTKT_Click);
        }

        protected void SimpanTKT_Click(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalTKT");
            tbLevelTKT.Text = tkt.LevelTKT.ToString();
            setTargetLevelTKT(tkt.LevelTKT);
            panelCloseModal.Visible = true;
            eventRadioBtnPilihanSkema();
        }

        void Child1_OnChildEventOccurs(object sender, EventArgs e)
        {
            new uiModal().ShowModal(this.Page, "modalTKT");
        }

        public void InitIdentitasUsulan(string thn_usulan, string thn_pelaksanaan, Guid idUsulanKegiatan = default(Guid))
        {
            isiddlRumpunIlmu(ref ddlRumpunIlmuLevel1, 1);
            isiddlBidangUnggulanPT();

            ViewState["thn_usulan"] = thn_usulan;
            ViewState["thn_pelaksanaan"] = thn_pelaksanaan;

            if (idUsulanKegiatan == default(Guid))
            {
                IsNew = true;
                IdUsulan = Guid.NewGuid();
                IdUsulanKegiatan = Guid.NewGuid();
            }
            else
            {
                IsNew = false;
                IdUsulanKegiatan = idUsulanKegiatan;

                var dt = new DataTable();
                if (modelIdentitas.getUsulanKegiatan(ref dt, idUsulanKegiatan))
                {
                    IdUsulan = Guid.Parse(dt.Rows[0]["id_usulan"].ToString());
                }

                dt.Clear();
                if (modelIdentitas.getIdentitasUsulan(ref dt, IdUsulan) && dt.Rows.Count > 0)
                {
                    tbJudul.Text = dt.Rows[0]["judul"].ToString();
                    var levelTKTTarget = dt.Rows[0]["level_tkt_target"].ToString();
                    string id_kategori_sbk = dt.Rows[0]["id_kategori_sbk"].ToString();
                    if (levelTKTTarget == "" || levelTKTTarget=="0")
                    {
                        DataTable dtTS = new DataTable();
                        modelIdentitas.getTktSbkbySkema(ref dtTS, int.Parse(dt.Rows[0]["id_skema"].ToString()));
                        levelTKTTarget = dtTS.Rows[0]["level_tkt"].ToString();
                        id_kategori_sbk = dtTS.Rows[0]["id_kategori_sbk"].ToString();
                        ViewState["tkt_target"] = levelTKTTarget;
                        tbLevelTKT.Text = levelTKTTarget;
                        ddlTargetTKT.Items.Clear();
                        ddlTargetTKT.Items.Add(new ListItem
                        {
                            Value = levelTKTTarget,
                            Text = levelTKTTarget,
                            Selected = true
                        });
                    }
                    else
                    {
                        tbLevelTKT.Text = dt.Rows[0]["level_tkt"].ToString();
                        ViewState["tkt_target"] = levelTKTTarget;
                        setTargetLevelTKT(int.Parse(tbLevelTKT.Text));
                        setValueDropDownList(ref ddlTargetTKT, dt.Rows[0]["level_tkt_target"].ToString());
                    }

                    //if (dt.Rows[0]["level_tkt"].ToString() == "0" || dt.Rows[0]["level_tkt"].ToString() == ""
                    //    || dt.Rows[0]["level_tkt"].ToString() == "-")
                    //{
                    //    tbLevelTKT.Text = "-";
                    //    ddlTargetTKT.Items.Clear();
                    //    ddlTargetTKT.Items.Add(new ListItem
                    //    {
                    //        Value = levelTKTTarget,
                    //        Text = levelTKTTarget,
                    //        Selected = true
                    //    });
                    //}
                    //else
                    //{
                    //    tbLevelTKT.Text = dt.Rows[0]["level_tkt"].ToString();
                    //    setTargetLevelTKT(int.Parse(tbLevelTKT.Text));
                    //    setValueDropDownList(ref ddlTargetTKT, dt.Rows[0]["level_tkt_target"].ToString());
                    //}

                    rblKategoriPenelitian.SelectedValue = dt.Rows[0]["kd_program_hibah"].ToString();
                    isiddlSkemaKegiatan();
                    setValueDropDownList(ref ddlSkemaPenelitian, dt.Rows[0]["id_skema"].ToString());
                    ViewState["id_skema"] = dt.Rows[0]["id_skema"].ToString();

                    isiddlLamaKegiatan();
                    setValueDropDownList(ref ddlLamaKegiatan, dt.Rows[0]["lama_kegiatan"].ToString());

                    setValueDropDownList(ref ddlRumpunIlmuLevel1, dt.Rows[0]["id_rumpun_ilmu_level1"].ToString());
                    isiddlRumpunIlmu(ref ddlRumpunIlmuLevel2, 2, Guid.Parse(ddlRumpunIlmuLevel1.SelectedValue));
                    setValueDropDownList(ref ddlRumpunIlmuLevel2, dt.Rows[0]["id_rumpun_ilmu_level2"].ToString());
                    isiddlRumpunIlmu(ref ddlRumpunIlmuLevel3, 3, Guid.Parse(ddlRumpunIlmuLevel2.SelectedValue));
                    setValueDropDownList(ref ddlRumpunIlmuLevel3, dt.Rows[0]["id_rumpun_ilmu"].ToString());

                    isiddlSBK();
                    setValueDropDownList(ref ddlSBK, id_kategori_sbk);

                    isiddlBidangFokus();
                    setValueDropDownList(ref ddlBidangFokus, dt.Rows[0]["id_bidang_fokus"].ToString());

                    panelTopikUnggulanPT.Visible = true;
                    panelTopikPenelitian.Visible = false;

                    isiddlBidangUnggulanPT();
                    setValueDropDownList(ref ddlBidangUnggulanPT, dt.Rows[0]["id_bidang_unggulan_perguruan_tinggi"].ToString());

                    isiddlTopikUnggulanPT();
                    setValueDropDownList(ref ddlTopikUnggulanPT, dt.Rows[0]["id_topik_unggulan_perguruan_tinggi"].ToString());
                }

                tbLevelTKT.Enabled = false;
                ddlTargetTKT.Enabled = false;
                btnUkurTKT.Enabled = false;
                lbSimpan.Visible = false;
                //cekEligibilitas();
            }
        }

        private void setTargetLevelTKT(int levelTKTAwal)
        {
            List<int> listTargetTKT = new List<int>();

            switch (levelTKTAwal)
            {
                case 1:
                    listTargetTKT.Add(2);
                    listTargetTKT.Add(3);
                    break;
                case 2:
                    listTargetTKT.Add(3);
                    break;
                case 3:
                    listTargetTKT.Add(4);
                    listTargetTKT.Add(5);
                    listTargetTKT.Add(6);
                    break;
                case 4:
                    listTargetTKT.Add(5);
                    listTargetTKT.Add(6);
                    break;
                case 5:
                    listTargetTKT.Add(6);
                    break;
                case 6:
                    listTargetTKT.Add(7);
                    listTargetTKT.Add(8);
                    listTargetTKT.Add(9);
                    break;
                case 7:
                    listTargetTKT.Add(8);
                    listTargetTKT.Add(9);
                    break;
                case 8:
                    listTargetTKT.Add(9);
                    break;
            }

            ddlTargetTKT.Items.Clear();
            ddlTargetTKT.DataSource = listTargetTKT;
            ddlTargetTKT.DataBind();
            if (ddlTargetTKT.Items.Count > 0) ddlTargetTKT.SelectedIndex = 0;
        }

        private void modePemilihanSkema(bool isEnabled)
        {
            tbLevelTKT.Enabled = !isEnabled;
            ddlTargetTKT.Enabled = !isEnabled;
            btnUkurTKT.Enabled = !isEnabled;
            lbSimpan.Enabled = !isEnabled;

            rblKategoriPenelitian.Enabled = false;
            ddlBidangUnggulanPT.Enabled = isEnabled;
            ddlTopikUnggulanPT.Enabled = isEnabled;
            ddlSkemaPenelitian.Enabled = false;
            ddlRumpunIlmuLevel1.Enabled = isEnabled;
            ddlRumpunIlmuLevel2.Enabled = isEnabled;
            ddlRumpunIlmuLevel3.Enabled = isEnabled;
            ddlSBK.Enabled = false;
            ddlBidangFokus.Enabled = isEnabled;
            ddlTemaPenelitian.Enabled = isEnabled;
            ddlTopikPenelitian.Enabled = isEnabled;
            //ddlTahunKegiatan.Enabled = isEnabled;
            ddlLamaKegiatan.Enabled = false;

        }

        private void isiddlSkemaKegiatan()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];
            string status;

            if (Session["isEdit"] != null)
            {
                if (Session["isEdit"].ToString() != "true")
                {
                    if (modelTendik.isTendik(Guid.Parse(objLogin.idPersonal)))
                    {
                        status = "4";
                    }
                    else
                    {
                        if (ViewState["kd_sts_eligible"] != null)
                        {
                            string kd_sts_eligible = ViewState["kd_sts_eligible"].ToString();
                            string kd_sts_eleigible_pasca = ViewState["kd_sts_eleigible_pasca"].ToString();
                            if (kd_sts_eleigible_pasca == "0" && kd_sts_eligible == "1")
                            {
                                status = "1";
                            }
                            else
                            if (kd_sts_eleigible_pasca == "1" && kd_sts_eligible == "0")
                            {
                                status = "2";
                            }
                            else
                            {
                                status = "3";
                            }
                        }
                        else
                        {
                            status = "3";
                        };
                    }
                    string tkttarget = ddlTargetTKT.SelectedValue;
                    if (tkttarget == "") { tkttarget = "0"; }
                    // TMP
                    if (modelIdentitas.getSkemaKegiatanByTKTOngoing(ref dt, rblKategoriPenelitian.SelectedValue,
                            int.Parse(tkttarget), ViewState["thn_usulan"].ToString(), ViewState["thn_pelaksanaan"].ToString(), Guid.Parse(objLogin.idPersonal), status.ToString()))
                    {
                        ddlSkemaPenelitian.AppendDataBoundItems = true;
                        ddlSkemaPenelitian.Items.Clear();
                        ddlSkemaPenelitian.Items.Add(new ListItem { Text = "-- Pilih Skema Kegiatan --", Value = "-1", Selected = true });
                        ddlSkemaPenelitian.DataSource = dt;
                        ddlSkemaPenelitian.DataBind();
                    }
                }
            }
            else
            {
                if (modelTendik.isTendik(Guid.Parse(objLogin.idPersonal)))
                {
                    status = "4";
                }
                else
                {
                    if (ViewState["kd_sts_eligible"] != null)
                    {
                        string kd_sts_eligible = ViewState["kd_sts_eligible"].ToString();
                        string kd_sts_eleigible_pasca = ViewState["kd_sts_eleigible_pasca"].ToString();
                        if (kd_sts_eleigible_pasca == "0" && kd_sts_eligible == "1")
                        {
                            status = "1";
                        }
                        else
                        if (kd_sts_eleigible_pasca == "1" && kd_sts_eligible == "0")
                        {
                            status = "2";
                        }
                        else
                        {
                            status = "3";
                        }
                    }
                    else
                    {
                        status = "3";
                    };
                }

                if (modelIdentitas.getSkemaKegiatanByTKT(ref dt, rblKategoriPenelitian.SelectedValue,
                        int.Parse(ddlTargetTKT.SelectedValue), ViewState["thn_usulan"].ToString(), ViewState["thn_pelaksanaan"].ToString(),
                        Guid.Parse(objLogin.idPersonal), status.ToString()))
                {
                    ddlSkemaPenelitian.AppendDataBoundItems = true;
                    ddlSkemaPenelitian.Items.Clear();
                    ddlSkemaPenelitian.Items.Add(new ListItem { Text = "-- Pilih Skema Kegiatan --", Value = "-1", Selected = true });
                    ddlSkemaPenelitian.DataSource = dt;
                    ddlSkemaPenelitian.DataBind();
                }

            }

        }

        private void isiddlBidangUnggulanPT()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (modelIdentitas.getBidangUnggulanPenelitianPT(ref dt, objLogin.idInstitusi))
            {
                ddlBidangUnggulanPT.AppendDataBoundItems = true;
                ddlBidangUnggulanPT.Items.Clear();
                ddlBidangUnggulanPT.Items.Add(new ListItem { Text = "-- Pilih Bidang Unggulan PT --", Value = "00000000-0000-0000-0000-000000000000", Selected = true });
                ddlBidangUnggulanPT.DataSource = dt;
                ddlBidangUnggulanPT.DataBind();
            }
        }

        private void isiddlTopikUnggulanPT()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (modelIdentitas.getTopikUnggulanPenelitianPT(ref dt, Guid.Parse(ddlBidangUnggulanPT.SelectedValue)))
            {
                ddlTopikUnggulanPT.AppendDataBoundItems = true;
                ddlTopikUnggulanPT.Items.Clear();
                ddlTopikUnggulanPT.Items.Add(new ListItem { Text = "-- Pilih Topik Unggulan PT --", Value = "00000000-0000-0000-0000-000000000000", Selected = true });
                ddlTopikUnggulanPT.DataSource = dt;
                ddlTopikUnggulanPT.DataBind();
            }
        }

        private void isiddlRumpunIlmu(ref DropDownList ddl, int level, Guid idReferensi = default(Guid))
        {
            var dt = new DataTable();
            if (modelIdentitas.getRumpunIlmu(ref dt, level, idReferensi))
            {
                ddl.AppendDataBoundItems = true;
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem { Text = $"-- Pilih Rumpun Ilmu Level {level} --", Value = "00000000-0000-0000-0000-000000000000" });
                ddl.DataSource = dt;
                ddl.DataBind();
            }
        }

        private void isiddlSBK()
        {
            if (Session["isEdit"] != null)
            {
                if (Session["isEdit"].ToString() != "true")
                {

                    var dt = new DataTable();
                    if (modelIdentitas.getSBK(ref dt, int.Parse(ddlSkemaPenelitian.SelectedValue),
                        int.Parse(ddlTargetTKT.SelectedValue)))
                    {
                        ddlSBK.AppendDataBoundItems = true;
                        ddlSBK.Items.Clear();
                        ddlSBK.Items.Add(new ListItem { Text = "-- Pilih SBK --", Value = "-1" });
                        ddlSBK.DataSource = dt;
                        ddlSBK.DataBind();
                    }
                }
            }
            else
            {
                var dt = new DataTable();
                if (modelIdentitas.getSBK(ref dt, int.Parse(ddlSkemaPenelitian.SelectedValue),
                    int.Parse(ddlTargetTKT.SelectedValue)))
                {
                    ddlSBK.AppendDataBoundItems = true;
                    ddlSBK.Items.Clear();
                    ddlSBK.Items.Add(new ListItem { Text = "-- Pilih SBK --", Value = "-1" });
                    ddlSBK.DataSource = dt;
                    ddlSBK.DataBind();

                }
            }
        }

        private void isiddlBidangFokus()
        {
            var dt = new DataTable();
            if (modelIdentitas.getBidangFokus(ref dt, int.Parse(ddlSkemaPenelitian.SelectedValue), int.Parse(ViewState["tkt_target"].ToString()),
                    int.Parse(ddlSBK.SelectedValue)))
            {
                ddlBidangFokus.AppendDataBoundItems = true;
                ddlBidangFokus.Items.Clear();
                ddlBidangFokus.Items.Add(new ListItem { Text = "-- Pilih Bidang Fokus --", Value = "-1" });
                ddlBidangFokus.DataSource = dt;
                ddlBidangFokus.DataBind();
            }
        }

        private void isiddlTemaPenelitian()
        {
            //var dt = new DataTable();
            //if (modelIdentitas.getTemaPenelitian(ref dt, int.Parse(ddlBidangFokus.SelectedValue)))
            //{
            //    ddlTemaPenelitian.AppendDataBoundItems = true;
            //    ddlTemaPenelitian.Items.Clear();
            //    ddlTemaPenelitian.Items.Add(new ListItem { Text = "-- Pilih Tema Penelitian --", Value = "-1" });
            //    ddlTemaPenelitian.DataSource = dt;
            //    ddlTemaPenelitian.DataBind();
            //}
        }

        private void isiddlTopikPenelitian(Guid idTema)
        {
            //var dt = new DataTable();
            //if (modelIdentitas.getTopikPenelitian(ref dt, idTema))
            //{
            //    ddlTopikPenelitian.AppendDataBoundItems = true;
            //    ddlTopikPenelitian.Items.Clear();
            //    ddlTopikPenelitian.Items.Add(new ListItem { Text = "-- Pilih Topik Penelitian --", Value = "-1" });
            //    ddlTopikPenelitian.DataSource = dt;
            //    ddlTopikPenelitian.DataBind();
            //}
        }

        private void isiddlLamaKegiatan()
        {
            var dt = new DataTable();
            if (modelIdentitas.getDataSkemaKegiatan(ref dt, int.Parse(ddlSkemaPenelitian.SelectedValue)))
            {
                ddlLamaKegiatan.Items.Clear();
                ddlLamaKegiatan.Items.Add(new ListItem { Text = "--", Value = "-1" });

                int tahunMinimal = 0, tahunMaksimal = 0;
                if (dt.Rows.Count > 0)
                {
                    //int.TryParse(dt.Rows[0]["thn_minimal"].ToString(), out tahunMinimal);
                    //int.TryParse(dt.Rows[0]["thn_maksimal"].ToString(), out tahunMaksimal);
                    tahunMinimal = 1; 
                    tahunMaksimal = 3;
                    for (int i = tahunMinimal; i <= tahunMaksimal; i++)
                    {
                        ddlLamaKegiatan.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
                    }

                    if (ddlLamaKegiatan.Items.Count > 0) ddlLamaKegiatan.SelectedIndex = 0;

                }
            }
        }

        private void setValueDropDownList(ref DropDownList ddl, string selectedValue)
        {
            if (ddl.Items.FindByValue(selectedValue) != null) ddl.SelectedValue = selectedValue;
        }

        public bool Simpan()
        {
            var objLogin = (Models.login)Session["objLogin"];
            if (!IsNew)
            {
                if (!modelIdentitas.updateJudulUsulanKegiatan(IdUsulan, tbJudul.Text))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelIdentitas.errorMessage);
                    return false;
                }
                //return true;
            }
            //
            //cek apakah ada di white list
            Boolean whitelistdibuka = false;
            Boolean whitelistada = false;
            Boolean whitejml = false;

            //ViewState["thn_usulan"] = thn_usulan;
            //ViewState["thn_pelaksanaan"] = thn_pelaksanaan;

            var dt = new DataTable();
            if (modelIdentitas.checkwhitelist(ref dt, objLogin.idPersonal, ddlSkemaPenelitian.SelectedValue,
                ViewState["thn_usulan"].ToString(), ViewState["thn_pelaksanaan"].ToString()))
            {
                whitelistdibuka = Boolean.Parse(dt.Rows[0]["status_white_list"].ToString());
                whitelistada = Boolean.Parse(dt.Rows[0]["status_boleh"].ToString());
                whitejml = Boolean.Parse(dt.Rows[0]["status_jumlah"].ToString());
            }

            if (whitelistdibuka == true)
            {
                if (whitelistada == false || whitejml == false)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Anda tidak diperkenankan mengusulkan");
                    return false;

                }
            }
            else
            {
                // cek kuota usulan
                //var dtPersyaratan = new DataTable();
                //modelIdentitas.getCekKuotaUsulan(ref dtPersyaratan, objLogin.idPersonal, ViewState["thn_pelaksanaan"].ToString());
                //string kd_sts_eligible = dtPersyaratan.Rows[0]["kd_sts_eleigible"].ToString();

                //string keterangan = dtPersyaratan.Rows[0]["keterangan"].ToString();
                //string kd_sts_eleigible_pasca = dtPersyaratan.Rows[0]["kd_sts_eleigible_pasca"].ToString();
                //string keterangan_pasca = dtPersyaratan.Rows[0]["keterangan_pasca"].ToString();

                //if (ddlSkemaPenelitian.SelectedValue == "6" ||
                //    ddlSkemaPenelitian.SelectedValue == "30" ||
                //    ddlSkemaPenelitian.SelectedValue == "52" ||
                //    ddlSkemaPenelitian.SelectedValue == "76")
                //{
                //    if (kd_sts_eleigible_pasca == "0")
                //    {
                //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", keterangan_pasca);
                //        return false;
                //    }
                //    else if (kd_sts_eleigible_pasca == "1")
                //    {

                //    }

                //}
                //else
                //{
                //    if (kd_sts_eligible == "0")
                //    {
                //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", keterangan);
                //        return false;
                //    }
                //    else if (kd_sts_eligible == "1")
                //    {

                //    }
                //}

                // cek didanai skema pdp
                //var dtSkemaPdp = new DataTable();
                //modelIdentitas.getCekDidanaiSkemaPdp(ref dtSkemaPdp, objLogin.idPersonal);
                //string kd_sts_eligible_pdp = dtSkemaPdp.Rows[0]["kd_sts_eligible"].ToString();
                //string keterangan_pdp = dtSkemaPdp.Rows[0]["keterangan"].ToString();

                //if (ddlSkemaPenelitian.SelectedValue == "7")
                //{
                //    if (kd_sts_eligible_pdp == "0")
                //    {
                //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", keterangan_pdp);
                //        return false;
                //    }
                //}

                // cek eligibilitas ketua
                setValueDropDownList(ref ddlSkemaPenelitian, ddlSkemaPenelitian.SelectedValue);
                //if (!modelTendik.isTendik(Guid.Parse(objLogin.idPersonal)))
                //{
                //    var dtEligibilitasKetua = new DataTable();
                //    modelIdentitas.getCekEligibilitasKetua(ref dtEligibilitasKetua, objLogin.idPersonal, int.Parse(ddlSkemaPenelitian.SelectedValue));
                //    if (dtEligibilitasKetua.Rows.Count > 0)
                //    {
                //        string kd_sts_eligibilitas_ketua = dtEligibilitasKetua.Rows[0]["kd_sts_eligibilitas_ketua"].ToString();
                //        string keterangan_persyaratan = dtEligibilitasKetua.Rows[0]["keterangan_persyaratan"].ToString();

                //        if (kd_sts_eligibilitas_ketua == "0")
                //        {
                //            string info = "Persyaratan Skema " +
                //                ddlSkemaPenelitian.SelectedItem.Text + " " + keterangan_persyaratan;
                //            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", info);
                //            Session["kd_sts_eligibilitas_ketua"] = "0";
                //            Session["info_sts_eligibilitas_ketua"] = info;
                //            return false;
                //        }
                //    }
                //}
            }

            Session["kd_sts_eligibilitas_ketua"] = "1";

            if (ddlTargetTKT.SelectedValue == "-1")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus dipilih terlebih dahulu");
                return false;
            }
            List<string> invalidData = new List<string>();
            if (tbJudul.Text.Trim().Length == 0) invalidData.Add("Judul");
            if (ddlSkemaPenelitian.SelectedValue == "-1") invalidData.Add("Skema Penelitian");
            if (ddlRumpunIlmuLevel3.SelectedValue == "-1") invalidData.Add("Rumpun Ilmu");
            if (ddlSBK.SelectedValue == "-1") invalidData.Add("SBK");
            if (ddlBidangFokus.SelectedValue == "-1") invalidData.Add("Bidang Fokus");
            if (ddlLamaKegiatan.SelectedValue == "-1") invalidData.Add("Lama Kegiatan");

            switch (rblKategoriPenelitian.SelectedValue)
            {
                case "1":  //Desentralisasi
                    if (ddlTopikUnggulanPT.SelectedValue == "-1") invalidData.Add("Bidang Unggulan PT");
                    break;

                case "2":  //Kompetitif Nasional
                    if (ddlTemaPenelitian.SelectedValue == "-1") invalidData.Add("Tema Penelitian");
                    if (ddlTopikPenelitian.SelectedValue == "-1") invalidData.Add("Topik Penelitian");
                    break;
                case "6":  //Penugasan
                    if (ddlTopikUnggulanPT.SelectedValue == "-1") invalidData.Add("Bidang Unggulan PT");
                    break;
            }

            if (invalidData.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", invalidData.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            Guid? idTopikUnggulanPT = (rblKategoriPenelitian.SelectedValue == "1") || (rblKategoriPenelitian.SelectedValue == "6") ?
                Guid.Parse(ddlTopikUnggulanPT.SelectedValue) : default(Guid?);

            Guid? idTopikPenelitian = (rblKategoriPenelitian.SelectedValue == "2") ?// || (rblKategoriPenelitian.SelectedValue == "6") ?
                //Guid.Parse(ddlTopikPenelitian.SelectedValue) : default(Guid?);
                Guid.Parse(ddlTopikUnggulanPT.SelectedValue) : default(Guid?);

            //if (IsNew)
            //{
                if (modelIdentitas.insertDataBaru(
                    IdUsulan,
                    IdUsulanKegiatan,
                    objLogin.idInstitusi,
                    int.Parse(ddlSkemaPenelitian.SelectedValue),
                    int.Parse(ddlBidangFokus.SelectedValue),
                    tbJudul.Text,
                    int.Parse(ddlLamaKegiatan.SelectedValue),
                    int.Parse(ddlSBK.SelectedValue),
                    ViewState["thn_usulan"].ToString(),
                    ViewState["thn_pelaksanaan"].ToString(),
                    int.Parse(ddlTargetTKT.SelectedValue),
                    Guid.Parse(objLogin.idPersonal.ToString()),
                    Guid.Parse(ddlRumpunIlmuLevel3.SelectedValue),
                    idTopikUnggulanPT,
                    idTopikPenelitian,
                    int.Parse(tbLevelTKT.Text)
                ))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Data berhasil disimpan...");
                    Session["kd_sts_eligibilitas_ketua"] = "1";
                    Session["info_sts_eligibilitas_ketua"] = "";
                    return true;
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelIdentitas.errorMessage);
                    return false;
                }
            //}
            //return true;
        }

        protected void btnUkurTKT_Click(object sender, EventArgs e)
        {
            tkt.InitTKT();
            new uiModal().ShowModal(this.Page, "modalTKT");
            panelCloseModal.Visible = false;
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            if (tbJudul.Text == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Judul harus diisi terlebih dahulu !");
                return;
            }

            if (ddlTargetTKT.SelectedValue == "-1")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus dipilih terlebih dahulu !");
                return;
            }

            modePemilihanSkema(true);
            isiddlSkemaKegiatan();
            new uiModal().HideModal(this.Page, "modalTKT");
        }

        protected void rblKategoriPenelitian_SelectedIndexChanged(object sender, EventArgs e)
        {
            eventRadioBtnPilihanSkema();
        }

        private void eventRadioBtnPilihanSkema()
        {
            isiddlSkemaKegiatan();
        }

        protected void ddlSkemaPenelitian_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlSBK();
            isiddlLamaKegiatan();
        }

        protected void ddlRumpunIlmuLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRumpunIlmuLevel1.SelectedValue != "-1")
                isiddlRumpunIlmu(ref ddlRumpunIlmuLevel2, 2, Guid.Parse(ddlRumpunIlmuLevel1.SelectedValue));
        }

        protected void ddlRumpunIlmuLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRumpunIlmuLevel2.SelectedValue != "-1")
                isiddlRumpunIlmu(ref ddlRumpunIlmuLevel3, 3, Guid.Parse(ddlRumpunIlmuLevel2.SelectedValue));
        }

        protected void ddlSBK_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlBidangFokus();
        }

        protected void ddlBidangFokus_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlTemaPenelitian();
        }

        protected void ddlTemaPenelitian_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTemaPenelitian.SelectedValue == "-1")
                isiddlTopikPenelitian(default(Guid));
            else
                isiddlTopikPenelitian(Guid.Parse(ddlTemaPenelitian.SelectedValue));
        }

        protected void ddlBidangUnggulanPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlTopikUnggulanPT();
        }

        private void cekEligibilitas()
        {
            Session["kd_sts_eligibilitas_ketua"] = "1";
            Session["info_sts_eligibilitas_ketua"] = "";
            var objLogin = (Models.login)Session["objLogin"];

            // cek kuota usulan
            var dtPersyaratan = new DataTable();
            modelIdentitas.getCekKuotaUsulanEdit(ref dtPersyaratan, objLogin.idPersonal, ViewState["thn_pelaksanaan"].ToString());
            string kd_sts_eligible = dtPersyaratan.Rows[0]["kd_sts_eleigible"].ToString();

            string keterangan = dtPersyaratan.Rows[0]["keterangan"].ToString();
            string kd_sts_eleigible_pasca = dtPersyaratan.Rows[0]["kd_sts_eleigible_pasca"].ToString();
            string keterangan_pasca = dtPersyaratan.Rows[0]["keterangan_pasca"].ToString();

            if (ddlSkemaPenelitian.SelectedValue == "6" ||
                ddlSkemaPenelitian.SelectedValue == "30" ||
                ddlSkemaPenelitian.SelectedValue == "52" ||
                ddlSkemaPenelitian.SelectedValue == "76")
            {
                if (kd_sts_eleigible_pasca == "0")
                {
                    Session["kd_sts_eligibilitas_ketua"] = "0";
                    Session["info_sts_eligibilitas_ketua"] = keterangan_pasca;
                    return;
                }
            }
            else
            {
                if (kd_sts_eligible == "0" && IsNew)
                {
                    Session["kd_sts_eligibilitas_ketua"] = "0";
                    Session["info_sts_eligibilitas_ketua"] = keterangan;
                    return;
                }
            }


            // cek didanai skema pdp
            var dtSkemaPdp = new DataTable();
            modelIdentitas.getCekDidanaiSkemaPdp(ref dtSkemaPdp, objLogin.idPersonal);
            string kd_sts_eligible_pdp = dtSkemaPdp.Rows[0]["kd_sts_eligible"].ToString();
            string keterangan_pdp = dtSkemaPdp.Rows[0]["keterangan"].ToString();

            if (ddlSkemaPenelitian.SelectedValue == "7")
            {
                if (kd_sts_eligible_pdp == "0")
                {
                    Session["kd_sts_eligibilitas_ketua"] = "0";
                    Session["info_sts_eligibilitas_ketua"] = keterangan_pdp;
                }
            }

            // cek eligibilitas ketua
            if (!modelTendik.isTendik(Guid.Parse(objLogin.idPersonal)))
            {
                setValueDropDownList(ref ddlSkemaPenelitian, ViewState["id_skema"].ToString());
                var dtEligibilitasKetua = new DataTable();
                modelIdentitas.getCekEligibilitasKetua(ref dtEligibilitasKetua, objLogin.idPersonal, int.Parse(ViewState["id_skema"].ToString()));
                string kd_sts_eligibilitas_ketua = dtEligibilitasKetua.Rows[0]["kd_sts_eligibilitas_ketua"].ToString();
                string keterangan_persyaratan = dtEligibilitasKetua.Rows[0]["keterangan_persyaratan"].ToString();

                if (kd_sts_eligibilitas_ketua == "0")
                {
                    string info = "Persyaratan Skema " +
                        ddlSkemaPenelitian.SelectedItem.Text + " " + keterangan_persyaratan;
                    Session["kd_sts_eligibilitas_ketua"] = "0";
                    Session["info_sts_eligibilitas_ketua"] = info;
                }
            }
        }
    }
}