using simlitekkes.Models.OperatorPenelitianPusdik;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class eksepsiPengusul : System.Web.UI.UserControl
    {
        daftarEksepsiPengusul objDaftarEksepsi = new daftarEksepsiPengusul();
        uiDropdownList obj_uiDDL = new uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        uiNotify noty = new uiNotify();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isiDdlThnUsulan();
                isiDdlThnPelaksanaan();
                mvMain.SetActiveView(vDaftarEksepsi);
                isiGridview();
            }
        }
        
        private void isiDdlThnUsulan()
        {
            ddlThnUsulan.Items.Clear();
            ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2013; i--)
            {
                ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void isiDdlThnPelaksanaan()
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem("--Pilih--", "0000"));
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
        }

        private void isiGridview()
        {
            DataTable dt = new DataTable();
            objDaftarEksepsi.listData(ref dt, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            gvDaftarEksepsi.DataSource = dt;
            gvDaftarEksepsi.DataBind();            
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 0; i < 2; i++)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
            ddlThnPelaksanaan.SelectedIndex = 0;
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGridview();
        }

        protected void lbTambah_Click(object sender, EventArgs e)
        {
            if(ddlThnUsulan.SelectedValue != "0000" && ddlThnPelaksanaan.SelectedValue != "0000")
            {
                lbTambah.Visible = false;
                lbKembali.Visible = true;
                ddlThnUsulan.Enabled = false;
                ddlThnPelaksanaan.Enabled = false;                
                mvMain.SetActiveView(vIsiEksepsiDosen);
            }      
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Maaf, ",
                "Tahun Usulan atau Tahun Pelaksanaan belum dipilih !");
            }
        }

        protected void lbCari_Click(object sender, EventArgs e)
        {
            if (tbCari.Text.Length == 10)
            {
                DataTable dt = objDaftarEksepsi.getRowByNIDN(tbCari.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblNama.Text = dt.Rows[0]["nama"].ToString();
                    lblInstitusi.Text = dt.Rows[0]["nama_institusi"].ToString();
                    ViewState["idPersonal"] = dt.Rows[0]["id_personal"];
                    ViewState["idInstitusi"] = dt.Rows[0]["id_institusi"];
                    isiddlSkema(ref ddlSkema, "0");
                    isiddlTahapanKegiatan(ref ddlTahapan);
                    ddlSkema.Enabled = true;
                    ddlTahapan.Enabled = true;
                    tbTglMulai.Enabled = true;
                    tbTglBerakhir.Enabled = true;
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Maaf, Data Dosen tidak dapat ditemukan !");

                    clearDataDosen();
                }
            }
        }

        private void clearDataDosen()
        {
            lblNama.Text = "-";
            lblInstitusi.Text = "-";
            tbCari.Text = "";
            ddlSkema.Enabled = false;
            ddlTahapan.Enabled = false;
            tbTglMulai.Text = "";
            tbTglBerakhir.Text = "";

            lblNamaTendik.Text = "-";
            lblInstitusiTendik.Text = "-";
            tbCariNoKTP.Text = "";
            ddlSkemaTendik.Enabled = false;
            ddlTahapanTendik.Enabled = false;
            tbTglMulaiTendik.Text = "";
            tbTglBerakhirTendik.Text = "";

            ViewState.Remove("idPersonal");
            ViewState.Remove("idInstitusi");
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {            
            string stsPengusul = "0"; // stsPengusul Dosen
            Guid idPersonal = Guid.Parse(ViewState["idPersonal"].ToString());
            if (!objDaftarEksepsi.insertData(idPersonal, stsPengusul, int.Parse(ddlSkema.SelectedValue), ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue
                                    , tbTglMulai.Text, tbTglBerakhir.Text, ddlTahapan.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objDaftarEksepsi.errorMessage);
            }

            noty.Notify(this.Page, uiNotify.NotifyType.success, "Info",
                 "Simpan Data Berhasil!");

            mvMain.SetActiveView(vDaftarEksepsi);
            isiGridview();
            lbKembali.Visible = false;
            lbTambah.Visible = true;
            ddlThnUsulan.Enabled = true;
            ddlThnPelaksanaan.Enabled = true;
            clearDataDosen();
        }

        protected void lbCariNoKTP_Click(object sender, EventArgs e)
        {
            if (tbCariNoKTP.Text.Length == 16)
            {
                DataTable dt = objDaftarEksepsi.getRowByKTP(tbCariNoKTP.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblNamaTendik.Text = dt.Rows[0]["nama"].ToString();
                    lblInstitusiTendik.Text = dt.Rows[0]["nama_institusi"].ToString();
                    ViewState["idPersonal"] = dt.Rows[0]["id_personal"];
                    ViewState["idInstitusi"] = dt.Rows[0]["id_institusi"];
                    isiddlSkema(ref ddlSkemaTendik, "1");
                    isiddlTahapanKegiatan(ref ddlTahapanTendik);
                    ddlSkemaTendik.Enabled = true;
                    ddlTahapanTendik.Enabled = true;
                    tbTglMulaiTendik.Enabled = true;
                    tbTglBerakhirTendik.Enabled = true;
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Maaf, Data Dosen tidak dapat ditemukan !");

                    clearDataDosen();
                }
            }
        }

        protected void lbSimpanTendik_Click(object sender, EventArgs e)
        {
            string stsPengusul = "1"; // stsPengusul Tenaga Kependidikan
            Guid idPersonal = Guid.Parse(ViewState["idPersonal"].ToString());
            if (!objDaftarEksepsi.insertData(idPersonal, stsPengusul, int.Parse(ddlSkemaTendik.SelectedValue), ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue
                                    , tbTglMulaiTendik.Text, tbTglBerakhirTendik.Text, ddlTahapanTendik.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objDaftarEksepsi.errorMessage);
            }
            mvMain.SetActiveView(vDaftarEksepsi);
            isiGridview();
            lbKembali.Visible = false;
            lbTambah.Visible = true;
            ddlThnUsulan.Enabled = true;
            ddlThnPelaksanaan.Enabled = true;
            clearDataDosen();
        }

        protected void lbKembali_Click(object sender, EventArgs e)
        {
            lbTambah.Visible = true;
            lbKembali.Visible = false;
            ddlThnUsulan.Enabled = true;
            ddlThnPelaksanaan.Enabled = true;
            mvMain.SetActiveView(vDaftarEksepsi);
        }

        private void isiddlSkema(ref DropDownList ddlSkema, string stsPengusul)
        {
            if (Application["SkemaKegiatan"] != null)
            {
                if (stsPengusul == "0")
                {
                    DataTable SkemaKegiatan = (DataTable)Application["SkemaKegiatan"];
                    if (!obj_uiDDL.bindToDropDownList(ref ddlSkema, SkemaKegiatan, "nama_skema", "id_skema"))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + obj_uiDDL.errorMessage);
                        return;
                    }
                }
                else
                {
                    DataTable SkemaKegiatan = objManipData.filterData((DataTable)Application["SkemaKegiatan"], "id_skema IN ('7') ");
                    if (!obj_uiDDL.bindToDropDownList(ref ddlSkema, SkemaKegiatan, "nama_skema", "id_skema"))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + obj_uiDDL.errorMessage);
                        return;
                    }
                }
            }                
        }

        private void isiddlTahapanKegiatan(ref DropDownList ddlTahapan)
        {
            if (Application["TahapanKegiatan"] != null)
            {
                DataTable TahapanKegiatan = objManipData.filterData((DataTable)Application["TahapanKegiatan"], "kd_tahapan_kegiatan IN ('11') ");
                if (!obj_uiDDL.bindToDropDownList(ref ddlTahapan, TahapanKegiatan, "tahapan", "kd_tahapan_kegiatan"))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + obj_uiDDL.errorMessage);
                    return;
                }
            }
        }

        private void isiddlStatus(ref DropDownList ddlStatus)
        {            
            var status = new Dictionary<int, string>()
            {
                { 1, "Aktif" },
                { 0, "Non Aktif" }
            };

            ddlStatus.DataSource = status;
            ddlStatus.DataTextField = "Value";
            ddlStatus.DataValueField = "Key";
            ddlStatus.DataBind();
            ddlStatus.SelectedIndex = 0;
        }

        protected void gvDaftarEksepsi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id_whitelist_usulan_personal = Guid.Parse(gvDaftarEksepsi.DataKeys[e.RowIndex]["id_whitelist_usulan_personal"].ToString());
            ViewState["idWhitelist"] = id_whitelist_usulan_personal;
            string stsPengusul = gvDaftarEksepsi.DataKeys[e.RowIndex]["kd_sts_pengusul"].ToString();

            DataTable dt = new DataTable();
            objDaftarEksepsi.getWhitelist(ref dt, id_whitelist_usulan_personal);
            if(dt.Rows.Count > 0)
            {
                lblNamaEdit.Text = dt.Rows[0]["nama"].ToString();
                lblInstitusiEdit.Text = dt.Rows[0]["nama_institusi"].ToString();
                if(stsPengusul == "0")
                {
                    isiddlSkema(ref ddlSkemaEdit, "0");
                }
                else
                {
                    isiddlSkema(ref ddlSkemaEdit, "1");
                }                
                ddlSkemaEdit.SelectedValue = dt.Rows[0]["id_skema"].ToString() == "" ? "0" :
                    dt.Rows[0]["id_skema"].ToString();
                isiddlTahapanKegiatan(ref ddlTahapanEdit);
                ddlTahapanEdit.SelectedValue = dt.Rows[0]["kd_tahapan_kegiatan"].ToString() == "" ? "0" :
                    dt.Rows[0]["kd_tahapan_kegiatan"].ToString();
                tbtglMulaiEdit.Text = dt.Rows[0]["tgl_mulai"].ToString() != "" ?
                    DateTime.Parse(dt.Rows[0]["tgl_mulai"].ToString()).ToString("yyyy-MM-dd") : "";
                tbTglBerakhirEdit.Text = dt.Rows[0]["tgl_berakhir"].ToString() != "" ?
                    DateTime.Parse(dt.Rows[0]["tgl_berakhir"].ToString()).ToString("yyyy-MM-dd") : "";
                isiddlStatus(ref ddlStatus);
                ddlStatus.SelectedValue = dt.Rows[0]["kd_sts_aktif"].ToString() == "" ? "0" :
                    dt.Rows[0]["kd_sts_aktif"].ToString();
            }

            lbTambah.Visible = false;
            lbKembali.Visible = true;
            mvMain.SetActiveView(vUpdateEksepsi);
        }

        protected void gvDaftarEksepsi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                int status = int.Parse(drv.Row["status"].ToString());

                if (status == 0 )
                {
                    lbEdit.Visible = false;
                }
                else
                {
                    lblStatus.Visible = false;
                }
            }
        }

        protected void lbSimpanEdit_Click(object sender, EventArgs e)
        {            
            if (!objDaftarEksepsi.updateData(Guid.Parse(ViewState["idWhitelist"].ToString()), int.Parse(ddlSkemaEdit.SelectedValue), ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue
                                  , ddlStatus.SelectedValue, tbtglMulaiEdit.Text, tbTglBerakhirEdit.Text, ddlTahapanEdit.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objDaftarEksepsi.errorMessage);
            }

            noty.Notify(this.Page, uiNotify.NotifyType.success, "Info",
                  "Ubah Data Berhasil!");

            mvMain.SetActiveView(vDaftarEksepsi);
            isiGridview();
            lbKembali.Visible = false;
            lbTambah.Visible = true;
            ddlThnUsulan.Enabled = true;
            ddlThnPelaksanaan.Enabled = true;
            clearDataDosen();
        }
    }
}