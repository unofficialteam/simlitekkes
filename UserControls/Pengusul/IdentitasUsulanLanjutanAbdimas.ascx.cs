using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class IdentitasUsulanLanjutanAbdimas : System.Web.UI.UserControl
    {

        identitasUsulanAbdimas modelIdentitas = new identitasUsulanAbdimas();
        uiNotify noty = new uiNotify();

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
            if (!IsPostBack)
            {
            }
        }

        public void InitIdentitasUsulan(Guid idUsulanKegiatan = default(Guid))
        {
            isiddlBidangUnggulanPT();

            if (idUsulanKegiatan == default(Guid))
            {
                IsNew = true;
                IdUsulan = Guid.NewGuid();
                IdUsulanKegiatan = Guid.NewGuid();
                isiddlSkemaKegiatan();
                isiddlBidangFokus();
            }
            else
            {
                IsNew = false;
                IdUsulanKegiatan = idUsulanKegiatan;

                var dt = new DataTable();

                if (modelIdentitas.getIdentitasUsulan(ref dt, idUsulanKegiatan) && dt.Rows.Count > 0)
                {
                    IdUsulan = Guid.Parse(dt.Rows[0]["id_usulan"].ToString());
                    tbJudul.Text = dt.Rows[0]["judul"].ToString();
                    tbJumlahMhs.Text = dt.Rows[0]["jml_mhs_terlibat"].ToString();

                    rblKategoriPengabdian.SelectedValue = dt.Rows[0]["kd_program_hibah"].ToString();
                    isiddlSkemaKegiatan();
                    setValueDropDownList(ref ddlSkemaPenelitian, dt.Rows[0]["id_skema"].ToString());

                    isiddlLamaKegiatan();
                    setValueDropDownList(ref ddlLamaKegiatan, dt.Rows[0]["lama_kegiatan"].ToString());

                    isiddlBidangFokus();
                    setValueDropDownList(ref ddlBidangFokus, dt.Rows[0]["id_bidang_fokus"].ToString());

                    if (rblKategoriPengabdian.SelectedValue == "7")
                    {
                        panelTopikUnggulanPT.Visible = true;

                        isiddlBidangUnggulanPT();
                        setValueDropDownList(ref ddlBidangUnggulanPT, dt.Rows[0]["id_bidang_unggulan_perguruan_tinggi"].ToString());

                        isiddlTopikUnggulanPT();
                        setValueDropDownList(ref ddlTopikUnggulanPT, dt.Rows[0]["id_topik_unggulan_perguruan_tinggi"].ToString());
                    }
                }

                modeEdit(true);
            }

        }

        private void modeEdit(bool isEnabled)
        {
            rblKategoriPengabdian.Enabled = !isEnabled;
            ddlSkemaPenelitian.Enabled = !isEnabled;
            ddlTahunKegiatan.Enabled = !isEnabled;
            //ddlBidangUnggulanPT.Enabled = isEnabled;
            //ddlTopikUnggulanPT.Enabled = isEnabled;            
            //ddlBidangFokus.Enabled = isEnabled;

            //ddlLamaKegiatan.Enabled = isEnabled;

        }

        private void isiddlSkemaKegiatan()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];
            //string status;

            //if (Session["isEdit"] != null)
            //{
            //    if (Session["isEdit"].ToString() != "true")
            //    {

            //        if (ViewState["kd_sts_eligible"] != null)
            //        {
            //            string kd_sts_eligible = ViewState["kd_sts_eligible"].ToString();
            //            string kd_sts_eleigible_pasca = ViewState["kd_sts_eleigible_pasca"].ToString();
            //            if (kd_sts_eleigible_pasca == "0" && kd_sts_eligible == "1")
            //            {
            //                status = "1";
            //            }
            //            else
            //            if (kd_sts_eleigible_pasca == "1" && kd_sts_eligible == "0")
            //            {
            //                status = "2";
            //            }
            //            else
            //            {
            //                status = "3";
            //            }
            //        }
            //        else
            //        {
            //            status = "3";

            //        };
            //        string tkttarget = ddlTargetTKT.SelectedValue;
            //        if (tkttarget == "") { tkttarget = "0"; }
            //        if (modelIdentitas.getSkemaKegiatanByTKT(ref dt, rblKategoriPenelitian.SelectedValue,
            //                int.Parse(tkttarget), Guid.Parse(objLogin.idPersonal), status.ToString()))
            //        {
            //            ddlSkemaPenelitian.AppendDataBoundItems = true;
            //            ddlSkemaPenelitian.Items.Clear();
            //            ddlSkemaPenelitian.Items.Add(new ListItem { Text = "-- Pilih Skema Kegiatan --", Value = "-1", Selected = true });
            //            ddlSkemaPenelitian.DataSource = dt;
            //            ddlSkemaPenelitian.DataBind();
            //        }
            //    }
            //}
            //else
            //{
            //    if (ViewState["kd_sts_eligible"] != null)
            //    {
            //        string kd_sts_eligible = ViewState["kd_sts_eligible"].ToString();
            //        string kd_sts_eleigible_pasca = ViewState["kd_sts_eleigible_pasca"].ToString();
            //        if (kd_sts_eleigible_pasca == "0" && kd_sts_eligible == "1")
            //        {
            //            status = "1";
            //        }
            //        else
            //        if (kd_sts_eleigible_pasca == "1" && kd_sts_eligible == "0")
            //        {
            //            status = "2";
            //        }
            //        else
            //        {
            //            status = "3";
            //        }
            //    }
            //    else
            //    {
            //        status = "3";

            //    };

            if (modelIdentitas.getSkemaKegiatanAbdimas(ref dt, rblKategoriPengabdian.SelectedValue, ddlTahunKegiatan.SelectedValue, objLogin.idPersonal))
            {
                ddlSkemaPenelitian.AppendDataBoundItems = true;
                ddlSkemaPenelitian.Items.Clear();
                ddlSkemaPenelitian.Items.Add(new ListItem { Text = "-- Pilih Skema Kegiatan --", Value = "-1", Selected = true });
                ddlSkemaPenelitian.DataSource = dt;
                ddlSkemaPenelitian.DataBind();
            }

            //}

        }

        private void isiddlBidangUnggulanPT()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (modelIdentitas.getBidangUnggulanPengabdianPT(ref dt, objLogin.idInstitusi))
            {
                ddlBidangUnggulanPT.AppendDataBoundItems = true;
                ddlBidangUnggulanPT.Items.Clear();
                ddlBidangUnggulanPT.Items.Add(new ListItem { Text = "-- Pilih Bidang Unggulan PT --", Value = "-1", Selected = true });
                ddlBidangUnggulanPT.DataSource = dt;
                ddlBidangUnggulanPT.DataBind();
            }
        }

        private void isiddlTopikUnggulanPT()
        {
            var dt = new DataTable();
            var objLogin = (Models.login)Session["objLogin"];

            if (modelIdentitas.getTopikUnggulanPengabdianPT(ref dt, Guid.Parse(ddlBidangUnggulanPT.SelectedValue)))
            {
                ddlTopikUnggulanPT.AppendDataBoundItems = true;
                ddlTopikUnggulanPT.Items.Clear();
                ddlTopikUnggulanPT.Items.Add(new ListItem { Text = "-- Pilih Topik Unggulan PT --", Value = "-1", Selected = true });
                ddlTopikUnggulanPT.DataSource = dt;
                ddlTopikUnggulanPT.DataBind();
            }
        }

        private void isiddlBidangFokus()
        {
            var dt = new DataTable();
            if (modelIdentitas.getBidangFokus(ref dt))
            {
                ddlBidangFokus.AppendDataBoundItems = true;
                ddlBidangFokus.Items.Clear();
                ddlBidangFokus.Items.Add(new ListItem { Text = "-- Pilih Bidang --", Value = "-1" });
                ddlBidangFokus.DataSource = dt;
                ddlBidangFokus.DataBind();
            }
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
                    int.TryParse(dt.Rows[0]["thn_minimal"].ToString(), out tahunMinimal);
                    int.TryParse(dt.Rows[0]["thn_maksimal"].ToString(), out tahunMaksimal);

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
            //if (!IsNew)
            //{
            //if (!modelIdentitas.updateJudulUsulanKegiatan(IdUsulan, tbJudul.Text))
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelIdentitas.errorMessage);
            //    return false;
            //}

            //    return true;
            //}

            var objLogin = (Models.login)Session["objLogin"];

            // cek didanai skema PKM
            var dtSkemaPkm = new DataTable();
            modelIdentitas.getCekDidanaiSkemaPkm(ref dtSkemaPkm, objLogin.idPersonal);
            string kd_sts_eligible_pkm = dtSkemaPkm.Rows[0]["kd_sts_eligible"].ToString();
            string keterangan_pkm = dtSkemaPkm.Rows[0]["keterangan"].ToString();

            if (ddlSkemaPenelitian.SelectedValue == "14")
            {
                if (kd_sts_eligible_pkm == "0")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", keterangan_pkm);
                    return false;
                }
            }

            // cek didanai skema PKMS
            var dtSkemaPkms = new DataTable();
            modelIdentitas.getCekDidanaiSkemaPkms(ref dtSkemaPkms, objLogin.idPersonal);
            string kd_sts_eligible_pkms = dtSkemaPkms.Rows[0]["kd_sts_eligible"].ToString();
            string keterangan_pkms = dtSkemaPkms.Rows[0]["keterangan"].ToString();

            if (ddlSkemaPenelitian.SelectedValue == "29")
            {
                if (kd_sts_eligible_pkm == "0")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", keterangan_pkms);
                    return false;
                }
            }

            // cek eligibilitas ketua
            var dtEligibilitasKetua = new DataTable();
            modelIdentitas.getCekEligibilitasKetua(ref dtEligibilitasKetua, objLogin.idPersonal, int.Parse(ddlSkemaPenelitian.SelectedValue));
            string kd_sts_eligibilitas_ketua = dtEligibilitasKetua.Rows[0]["kd_sts_eligibilitas_ketua"].ToString();
            string keterangan_persyaratan = dtEligibilitasKetua.Rows[0]["keterangan_persyaratan"].ToString();

            if (kd_sts_eligibilitas_ketua == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Persyaratan Skema " +
                    ddlSkemaPenelitian.SelectedItem.Text + " " + keterangan_persyaratan);
                return false;
            }

            List<string> invalidData = new List<string>();
            if (tbJudul.Text.Trim().Length == 0) invalidData.Add("Judul");
            if (ddlSkemaPenelitian.SelectedValue == "-1") invalidData.Add("Skema Penelitian");
            if (ddlBidangFokus.SelectedValue == "-1") invalidData.Add("Bidang Fokus");
            if (ddlLamaKegiatan.SelectedValue == "-1") invalidData.Add("Lama Kegiatan");
            if (tbJumlahMhs.Text.Trim().Length == 0) invalidData.Add("Jml. Mahasiswa");

            //desentralisasi
            if (rblKategoriPengabdian.SelectedValue == "7" && ddlTopikUnggulanPT.SelectedValue == "-1") invalidData.Add("Bidang Unggulan PT");

            if (invalidData.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", invalidData.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            Guid? idTopikUnggulanPT = (rblKategoriPengabdian.SelectedValue == "7") ?
                Guid.Parse(ddlTopikUnggulanPT.SelectedValue) : default(Guid?);

            if (modelIdentitas.insertDataBaru(
                    IdUsulan,
                    IdUsulanKegiatan,
                    objLogin.idInstitusi,
                    int.Parse(ddlSkemaPenelitian.SelectedValue),
                    int.Parse(ddlBidangFokus.SelectedValue),
                    tbJudul.Text,
                    int.Parse(ddlLamaKegiatan.SelectedValue),
                    DateTime.Now.Year.ToString(),
                    ddlTahunKegiatan.SelectedValue,
                    Guid.Parse(objLogin.idPersonal.ToString()),
                    idTopikUnggulanPT,
                    int.Parse(tbJumlahMhs.Text)
                ))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Data berhasil disimpan...");
                return true;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelIdentitas.errorMessage);
                return false;
            }
        }


        protected void rblKategoriPengabdian_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlSkemaKegiatan();
            panelTopikUnggulanPT.Visible = (rblKategoriPengabdian.SelectedValue == "7") ? true : false;
        }

        protected void ddlSkemaPenelitian_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlLamaKegiatan();
        }

        protected void ddlBidangUnggulanPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlTopikUnggulanPT();
        }
    }
}