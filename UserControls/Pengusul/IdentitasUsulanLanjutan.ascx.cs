using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System.Data;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class IdentitasUsulanLanjutan : System.Web.UI.UserControl
    {
        identitasUsulan modelIdentitas = new identitasUsulan();
        identitasUsulanLanjutan modelIdentitasLanjutan = new identitasUsulanLanjutan();
        uiNotify noty = new uiNotify();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        public void InitIdentitasUsulanLanjutan(Guid idUsulanKegiatan = default(Guid))
        {
            ViewState["idUsulanKegiatan"] = idUsulanKegiatan;

            var dt = new DataTable();
            if (modelIdentitasLanjutan.getIdentitasUsulanLanjutan(ref dt, idUsulanKegiatan))
            {
                lblJudul.Text = dt.Rows[0]["judul"].ToString();
                lblKategoriPenelitian.Text = dt.Rows[0]["program_hibah"].ToString();
                lblSkemaPenelitian.Text = dt.Rows[0]["nama_skema"].ToString();
                lblRumpunIlmu.Text = dt.Rows[0]["rumpun_ilmu"].ToString();
                lblBidangFokus.Text = dt.Rows[0]["bidang_fokus"].ToString();
                lblThnPelaksanaan.Text = dt.Rows[0]["thn_pelaksanaan_kegiatan"].ToString();
                lblLamaKegiatan.Text = dt.Rows[0]["lama_kegiatan"].ToString();

                ViewState["idSkema"] = dt.Rows[0]["id_skema"].ToString();
                ViewState["idKategoriSbk"] = dt.Rows[0]["id_kategori_sbk"].ToString();

                isiTktSaatIni();
                setValueDropDownList(ref DdlTKTSaatIni, dt.Rows[0]["level_tkt"].ToString());
                setValueDropDownList(ref ddlTargetTKT, dt.Rows[0]["level_tkt_target"].ToString());
                
                ViewState["idBidangFokus"] = dt.Rows[0]["id_bidang_fokus"].ToString();

                var KdProgramHibah = dt.Rows[0]["kd_program_hibah"].ToString();
                ViewState["KdProgramHibah"] = KdProgramHibah;

                if (KdProgramHibah == "1")
                {
                    panelTopikUnggulanPT.Visible = true;
                    panelTopikPenelitian.Visible = false;

                    isiddlBidangUnggulanPT();
                    setValueDropDownList(ref ddlBidangUnggulanPT, dt.Rows[0]["id_bidang_unggulan_perguruan_tinggi"].ToString());

                    isiddlTopikUnggulanPT();
                    setValueDropDownList(ref ddlTopikUnggulanPT, dt.Rows[0]["id_topik_unggulan_perguruan_tinggi"].ToString());
                }
                else
                {
                    panelTopikUnggulanPT.Visible = false;
                    panelTopikPenelitian.Visible = true;

                    isiddlTemaPenelitian();
                    setValueDropDownList(ref ddlTemaPenelitian, dt.Rows[0]["id_tema"].ToString());

                    isiddlTopikPenelitian(Guid.Parse(dt.Rows[0]["id_tema"].ToString()));
                    setValueDropDownList(ref ddlTopikPenelitian, dt.Rows[0]["id_topik"].ToString());
                };
            }
        }

        private void setValueDropDownList(ref DropDownList ddl, string selectedValue)
        {
            if (ddl.Items.FindByValue(selectedValue) != null) ddl.SelectedValue = selectedValue;
        }

        private void isiTktSaatIni()
        {
            var dt = new DataTable();

            int idSkema = int.Parse(ViewState["idSkema"].ToString());
            int idKategoriSbk = int.Parse(ViewState["idKategoriSbk"].ToString());

            if (modelIdentitasLanjutan.getLevelTktSaatIni(ref dt, idSkema, idKategoriSbk))
            {
                int tktMin = int.Parse(dt.Rows[0]["min_level_tkt"].ToString()); //4
                int tktMaks = int.Parse(dt.Rows[0]["maks_level_tkt"].ToString()); //6

                DdlTKTSaatIni.AppendDataBoundItems = true;
                DdlTKTSaatIni.Items.Clear();
                DdlTKTSaatIni.Items.Add(new ListItem { Text = "Pilih Level", Value = "0", Selected = true });

                ddlTargetTKT.AppendDataBoundItems = true;
                ddlTargetTKT.Items.Clear();
                ddlTargetTKT.Items.Add(new ListItem { Text = "Pilih Level", Value = "0", Selected = true });

                for (int a = tktMaks; a >= tktMin; a--)
                {
                    DdlTKTSaatIni.Items.Add(new ListItem { Text = a.ToString(), Value = a.ToString(), Selected = true });
                    ddlTargetTKT.Items.Add(new ListItem { Text = a.ToString(), Value = a.ToString(), Selected = true });
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

        private void isiddlTemaPenelitian()
        {
            var dt = new DataTable();
            if (modelIdentitas.getTemaPenelitian(ref dt, int.Parse(ViewState["idBidangFokus"].ToString())))
            {
                ddlTemaPenelitian.AppendDataBoundItems = true;
                ddlTemaPenelitian.Items.Clear();
                ddlTemaPenelitian.Items.Add(new ListItem { Text = "-- Pilih Tema Penelitian --", Value = "00000000-0000-0000-0000-000000000000" });
                ddlTemaPenelitian.DataSource = dt;
                ddlTemaPenelitian.DataBind();
            }
        }

        private void isiddlTopikPenelitian(Guid idTema)
        {
            var dt = new DataTable();
            if (modelIdentitas.getTopikPenelitian(ref dt, idTema))
            {
                ddlTopikPenelitian.AppendDataBoundItems = true;
                ddlTopikPenelitian.Items.Clear();
                ddlTopikPenelitian.Items.Add(new ListItem { Text = "-- Pilih Topik Penelitian --", Value = "00000000-0000-0000-0000-000000000000" });
                ddlTopikPenelitian.DataSource = dt;
                ddlTopikPenelitian.DataBind();
            }
        }

        private void setTargetLevelTKT(int levelTKTAwal)
        {
            List<int> listTargetTKT = new List<int>();

            switch (levelTKTAwal)
            {
                case 0:
                    listTargetTKT.Add(0);
                    break;
                case 1:
                    listTargetTKT.Add(1);
                    listTargetTKT.Add(2);
                    listTargetTKT.Add(3);
                    break;
                case 2:
                    listTargetTKT.Add(2);
                    listTargetTKT.Add(3);
                    break;
                case 3:
                    listTargetTKT.Add(3);
                    break;
                case 4:
                    listTargetTKT.Add(4);
                    listTargetTKT.Add(5);
                    listTargetTKT.Add(6);
                    break;
                case 5:
                    listTargetTKT.Add(5);
                    listTargetTKT.Add(6);
                    break;
                case 6:
                    listTargetTKT.Add(6);
                    break;
                case 7:
                    listTargetTKT.Add(7);
                    listTargetTKT.Add(8);
                    listTargetTKT.Add(9);
                    break;
                case 8:
                    listTargetTKT.Add(8);
                    listTargetTKT.Add(9);
                    break;
                case 9:
                    listTargetTKT.Add(9);
                    break;
            }

            ddlTargetTKT.Items.Clear();
            ddlTargetTKT.DataSource = listTargetTKT;
            ddlTargetTKT.DataBind();
            if (ddlTargetTKT.Items.Count > 0) ddlTargetTKT.SelectedIndex = 0;
        }

        protected void DdlTKTSaatIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlTKTSaatIni.SelectedValue == "0")
                setTargetLevelTKT(default(int));
            else
                setTargetLevelTKT(int.Parse(DdlTKTSaatIni.SelectedValue));
        }

        protected void ddlTemaPenelitian_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTemaPenelitian.SelectedValue == "00000000-0000-0000-0000-000000000000")
                isiddlTopikPenelitian(default(Guid));
            else
                isiddlTopikPenelitian(Guid.Parse(ddlTemaPenelitian.SelectedValue));
        }

        protected void ddlBidangUnggulanPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiddlTopikUnggulanPT();
        }

        public bool Simpan()
        {
            if (DdlTKTSaatIni.SelectedValue == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "TKT saat ini harus dipilih terlebih dahulu");
                return false;
            }
            if (ddlTargetTKT.SelectedValue == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Target TKT harus dipilih terlebih dahulu");
                return false;
            }
            List<string> invalidData = new List<string>();
           
            switch (ViewState["KdProgramHibah"].ToString())
            {
                case "1":  //Desentralisasi
                    if (ddlTopikUnggulanPT.SelectedValue == "00000000-0000-0000-0000-000000000000") invalidData.Add("Bidang Unggulan PT");
                    break;

                case "2":  //Kompetitif Nasional
                    if (ddlTemaPenelitian.SelectedValue == "00000000-0000-0000-0000-000000000000") invalidData.Add("Tema Penelitian");
                    if (ddlTopikPenelitian.SelectedValue == "00000000-0000-0000-0000-000000000000") invalidData.Add("Topik Penelitian");
                    break;
            }

            if (invalidData.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", invalidData.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return false;
            }

            Guid? idTopikUnggulanPT = (ViewState["KdProgramHibah"].ToString() == "1") ?
                Guid.Parse(ddlTopikUnggulanPT.SelectedValue) : default(Guid?);

            Guid? idTopikPenelitian = (ViewState["KdProgramHibah"].ToString() == "2") ?
                Guid.Parse(ddlTopikPenelitian.SelectedValue) : default(Guid?);

            if (ViewState["KdProgramHibah"].ToString() == "1")
            {
                if (modelIdentitasLanjutan.updateIdentitasLanjutanDesentralisasi(
                    Guid.Parse(ViewState["idUsulanKegiatan"].ToString()),
                    int.Parse(ddlTargetTKT.SelectedValue),
                    int.Parse(DdlTKTSaatIni.SelectedValue),
                    Guid.Parse(ddlTopikUnggulanPT.SelectedValue)

                ))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Data berhasil disimpan...");
                    return true;
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelIdentitasLanjutan.errorMessage);
                    return false;
                }
            }
            else
            {
                if (modelIdentitasLanjutan.updateIdentitasLanjutanKompetitifNasional(
                    Guid.Parse(ViewState["idUsulanKegiatan"].ToString()),
                    int.Parse(ddlTargetTKT.SelectedValue),
                    int.Parse(DdlTKTSaatIni.SelectedValue),
                    Guid.Parse(ddlTopikPenelitian.SelectedValue)

                ))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Data berhasil disimpan...");
                    return true;
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelIdentitasLanjutan.errorMessage);
                    return false;
                }
            }
        }
    }
}