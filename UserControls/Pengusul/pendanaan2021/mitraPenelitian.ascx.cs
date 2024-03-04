using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;
using System.IO;
using simlitekkes.Core;
using simlitekkes.Helper;
using System.Globalization;

namespace simlitekkes.UserControls.Pengusul.pendanaan2021
{
    public partial class mitraPenelitian : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.mitraPenelitian objMitra = new Models.Pengusul.mitraPenelitian();
        uiNotify noty = new uiNotify();
        //kontrolUnggah ktUnggah = new kontrolUnggah();
        uiModal uiMdl = new uiModal();
        manipulasiData objManipData = new manipulasiData();

        //string idUsulanKegiatan = "285cce1a-035a-4864-9b7f-6259ce4a1461";// 7414f38d-bc90-4117-b996-4ea131c299c0";//     

        protected void Page_Load(object sender, EventArgs e)
        {
            //isiMitra();
            //isiDurasiUsulan();
            //mvMitra.SetActiveView(vMitraPelaksanaPenelitian);
        }


        public void setData(string thn_usulan)
        {
            ViewState["thn_usulan"] = thn_usulan;
        }

        public void isiMitra(string idUsulanKegiatan)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            int p_jml_data = 0;
            int p_offset = 0;
            DataTable dtMitra = new DataTable();
            if (objMitra.listMitraPelaksanaPenelitian(ref dtMitra, Guid.Parse(idUsulanKegiatan), p_jml_data, p_offset))
            {
                var drmitrapelaksana = dtMitra.Select("kd_kategori_mitra = 1");
                if (drmitrapelaksana.Length > 0)
                {
                    var dt = drmitrapelaksana.CopyToDataTable();
                    gvMitraPelaksanaPenelitian.DataSource = dt;
                    //gvMitraPelaksanaPenelitian.DataBind();
                }

                var drmitracalonpengguna = dtMitra.Select("kd_kategori_mitra = 2");
                if (drmitracalonpengguna.Length > 0)
                {
                    var dtcalon = drmitracalonpengguna.CopyToDataTable();
                    gvMitraCalonPengguna.DataSource = dtcalon;
                    //gvMitraCalonPengguna.DataBind();
                }

                var drmitrainvestor = dtMitra.Select("kd_kategori_mitra = 3");
                if (drmitrainvestor.Length > 0)
                {
                    var dtcaloninvestor = drmitrainvestor.CopyToDataTable();
                    gvMitraInvestor.DataSource = dtcaloninvestor;
                    //gvMitraInvestor.DataBind();
                }
                gvMitraPelaksanaPenelitian.DataBind();
                gvMitraCalonPengguna.DataBind();
                gvMitraInvestor.DataBind();
            };
        }

        public void isiMitraPenelitianPerSkema()
        {
            DataTable dtMitra = new DataTable();
            //objMitra.getMitraPenelitianPerSkema(ref dtDurasi, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            objMitra.getMitraPenelitianPerSkema(ref dtMitra, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            if (dtMitra.Rows.Count > 0)
            {
                lblSkema.Text = dtMitra.Rows[0]["nama_skema"].ToString();
                lblLamaUsulan.Text = dtMitra.Rows[0]["lama_kegiatan"].ToString();
                ViewState["lama_kegiatan"] = lblLamaUsulan.Text;
                lblUrutanUsulan.Text = dtMitra.Rows[0]["urutan_thn_usulan"].ToString();
                ViewState["mitraPelaksana"] = dtMitra.Rows[0]["mitra_pelaksana"].ToString();
                ViewState["mitraCalonPengguna"] = dtMitra.Rows[0]["mitra_calon_pengguna"].ToString();
                ViewState["mitraInvestor"] = dtMitra.Rows[0]["mitra_investor"].ToString();
            }

            if (ViewState["mitraPelaksana"].ToString() == "1")
            {
                panelMitraPelaksana.Visible = true;
            }
            else
            {
                panelMitraPelaksana.Visible = false;
            }

            if (ViewState["mitraCalonPengguna"].ToString() == "1")
            {
                panelMitraCalonPengguna.Visible = true;
            }
            else
            {
                panelMitraCalonPengguna.Visible = false;
            }

            if (ViewState["mitraInvestor"].ToString() == "1")
            {
                panelMitraInvestor.Visible = true;
            }
            else
            {
                panelMitraInvestor.Visible = false;
            }

            if (ViewState["mitraInvestor"].ToString() != "1" && ViewState["mitraCalonPengguna"].ToString() != "1" && ViewState["mitraPelaksana"].ToString() != "1")
            {
                lblInfoMitra.Visible = true;
            }

            if (lblLamaUsulan.Text == "1")
            {
                tbPendanaanThn1.Visible = true;
                tbPendanaanThn2.Visible = false;
                tbPendanaanThn3.Visible = false;
                lblPendanaanThn1.Visible = true;
                lblPendanaanThn2.Visible = false;
                lblPendanaanThn3.Visible = false;

                tbDanaMCPThn1.Visible = true;
                tbDanaMCPThn2.Visible = false;
                tbDanaMCPThn3.Visible = false;
                lblDanaMCPThn1.Visible = true;
                lblDanaMCPThn2.Visible = false;
                lblDanaMCPThn3.Visible = false;

                tbDanaMIThn1.Visible = true;
                tbDanaMIThn2.Visible = false;
                tbDanaMIThn3.Visible = false;
                lblDanaMIThn1.Visible = true;
                lblDanaMIThn2.Visible = false;
                lblDanaMIThn3.Visible = false;
            }
            else if (lblLamaUsulan.Text == "2")
            {
                tbPendanaanThn1.Visible = true;
                tbPendanaanThn2.Visible = true;
                tbPendanaanThn3.Visible = false;
                lblPendanaanThn1.Visible = true;
                lblPendanaanThn2.Visible = true;
                lblPendanaanThn3.Visible = false;

                tbDanaMCPThn1.Visible = true;
                tbDanaMCPThn2.Visible = true;
                tbDanaMCPThn3.Visible = false;
                lblDanaMCPThn1.Visible = true;
                lblDanaMCPThn2.Visible = true;
                lblDanaMCPThn3.Visible = false;

                tbDanaMIThn1.Visible = true;
                tbDanaMIThn2.Visible = true;
                tbDanaMIThn3.Visible = false;
                lblDanaMIThn1.Visible = true;
                lblDanaMIThn2.Visible = true;
                lblDanaMIThn3.Visible = false;
            }
            else
            {
                tbPendanaanThn1.Visible = true;
                tbPendanaanThn2.Visible = true;
                tbPendanaanThn3.Visible = true;
                lblPendanaanThn1.Visible = true;
                lblPendanaanThn2.Visible = true;
                lblPendanaanThn3.Visible = true;

                tbDanaMCPThn1.Visible = true;
                tbDanaMCPThn2.Visible = true;
                tbDanaMCPThn3.Visible = true;
                lblDanaMCPThn1.Visible = true;
                lblDanaMCPThn2.Visible = true;
                lblDanaMCPThn3.Visible = true;

                tbDanaMIThn1.Visible = true;
                tbDanaMIThn2.Visible = true;
                tbDanaMIThn3.Visible = true;
                lblDanaMIThn1.Visible = true;
                lblDanaMIThn2.Visible = true;
                lblDanaMIThn3.Visible = true;
            }

        }

        protected void lbTambahMitraPelaksanaPenelitian_Click(object sender, EventArgs e)
        {
            ViewState["IsNew"] = true;
            ViewState["idMitra"] = Guid.NewGuid();
            isiDdlNegara();
            clearMitraPelaksana();
            mvMitra.SetActiveView(vMitraPelaksana);
        }

        private void isiDdlNegara()
        {
            ddlNegara.Items.Clear();
            ddlNegara.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            ddlNegaraMCP.Items.Clear();
            ddlNegaraMCP.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            ddlNegaraMI.Items.Clear();
            ddlNegaraMI.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            DataTable dt = new DataTable();
            if (objMitra.listNegara(ref dt))
            {
                try
                {
                    ddlNegara.DataTextField = "nama_negara";
                    ddlNegara.DataValueField = "kd_negara";
                    ddlNegara.DataSource = dt;
                    ddlNegara.DataBind();
                    ddlNegaraMCP.DataTextField = "nama_negara";
                    ddlNegaraMCP.DataValueField = "kd_negara";
                    ddlNegaraMCP.DataSource = dt;
                    ddlNegaraMCP.DataBind();
                    ddlNegaraMI.DataTextField = "nama_negara";
                    ddlNegaraMI.DataValueField = "kd_negara";
                    ddlNegaraMI.DataSource = dt;
                    ddlNegaraMI.DataBind();
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                }
            }
        }

        protected void btnBatal_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vMitraPelaksanaPenelitian);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            var isNew = bool.Parse(ViewState["IsNew"].ToString());

            List<string> emptyField = new List<string>();
            if (tbNamaMitraPelaksana.Text.Trim().Length == 0) emptyField.Add("Nama Mitra");
            if (tbAlamatInstitusi.Text.Trim().Length == 0) emptyField.Add("Alamat Institusi Mitra");
            if (tbAlamatSurelMitraPelaksana.Text.Trim().Length == 0) emptyField.Add("Alamat Surel Mitra");
            if (tbInstitusiMitraPelaksana.Text.Trim().Length == 0) emptyField.Add("Nama Insitusi Mitra");
            if (ddlNegara.SelectedValue == "-1") emptyField.Add("Nama Negara Mitra");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }

            var idMitra = Guid.Parse(ViewState["idMitra"].ToString());

            string namaMitraPelaksana = tbNamaMitraPelaksana.Text;
            string namaInstitusiMitra = tbInstitusiMitraPelaksana.Text;
            string alamatInstitusiMitra = tbAlamatInstitusi.Text;
            string surel = tbAlamatSurelMitraPelaksana.Text;
            string kdNegara = ddlNegara.SelectedValue;
            string danaThn1 = tbPendanaanThn1.Text;
            string danaThn2 = tbPendanaanThn2.Text;
            string danaThn3 = tbPendanaanThn3.Text;
            int kdKategoriMitra = 1;

            if (!objMitra.insupMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), namaMitraPelaksana, namaInstitusiMitra, alamatInstitusiMitra
                        , surel, kdNegara, kdKategoriMitra, idMitra))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objMitra.errorMessage);
            }

            if (tbPendanaanThn1.Text != "")
            {
                if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 1, idMitra, danaThn1, kdKategoriMitra))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objMitra.errorMessage);
                }
            }

            if (tbPendanaanThn2.Text != "")
            {
                if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 2, idMitra, danaThn2, kdKategoriMitra))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objMitra.errorMessage);
                }
            }

            if (tbPendanaanThn3.Text != "")
            {
                if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 3, idMitra, danaThn3, kdKategoriMitra))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objMitra.errorMessage);
                }
            }

            isiMitra(ViewState["id_usulan_kegiatan"].ToString());
            clearMitraPelaksana();
            mvMitra.SetActiveView(vMitraPelaksanaPenelitian);

        }

        private void clearMitraPelaksana()
        {
            tbNamaMitraPelaksana.Text = "";
            tbAlamatInstitusi.Text = "";
            tbAlamatSurelMitraPelaksana.Text = "";
            tbInstitusiMitraPelaksana.Text = "";
            tbPendanaanThn1.Text = "";
            tbPendanaanThn2.Text = "";
            tbPendanaanThn3.Text = "";
            //ddlNegara.SelectedValue = "0";
        }

        private void clearMCP()
        {
            tbNamaMCPengguna.Text = "";
            tbAlamatInstitusiMCP.Text = "";
            tbAlamatSurelMCP.Text = "";
            tbInstitusiMCP.Text = "";
            tbDanaMCPThn1.Text = "";
            tbDanaMCPThn2.Text = "";
            tbDanaMCPThn3.Text = "";
            //ddlNegaraMCP.SelectedValue ="0";
        }

        private void clearMI()
        {
            tbNamaMI.Text = "";
            tbAlamatInstitusiMI.Text = "";
            tbAlamatSurelMI.Text = "";
            tbInstitusiMI.Text = "";
            tbDanaMIThn1.Text = "";
            tbDanaMCPThn2.Text = "";
            tbDanaMIThn3.Text = "";
            //ddlNegaraMI.SelectedValue = "0";
        }

        protected void gvMitraPelaksanaPenelitian_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblNamaMitraPelaksana.Text = gvMitraPelaksanaPenelitian.DataKeys[e.RowIndex]["nama_mitra"].ToString();
            ViewState["idMitraPelaksana"] = gvMitraPelaksanaPenelitian.DataKeys[e.RowIndex]["id_mitra"].ToString();

            uiModal uiMdl = new uiModal();
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            Guid idMitraPelaksana = Guid.Parse(ViewState["idMitraPelaksana"].ToString());

            const string FolderName = "mitra";
            string idMitra = ViewState["idMitraPelaksana"].ToString();
            if (ApaBerkasMasihAda(this.Page, idMitra, FolderName))
            {
                if (!HapusBerkas(this.Page, idMitra, FolderName))
                {
                }
            }

            string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf",
                ViewState["thn_usulan"].ToString(), idMitra);
            if (File.Exists(Server.MapPath(filePath)))
            {
                File.Delete(Server.MapPath(filePath));
            }

            if (objMitra.deleteData(idMitraPelaksana))
            {
                ViewState.Remove("idMitra");
                isiMitra(ViewState["id_usulan_kegiatan"].ToString());
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                   "Data Mitra berhasil dihapus...");
                isiMitra(ViewState["id_usulan_kegiatan"].ToString());
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objMitra.errorMessage);
            }
        }

        public bool ApaBerkasMasihAda(Page thePage, string fileName, string fileFolder)
        {
            string path = string.Format("~/fileUpload/{0}/{1}.pdf", fileFolder, fileName);
            string filePath = thePage.Server.MapPath(path);
            return (File.Exists(filePath));
        }

        public bool HapusBerkas(Page thePage, string fileName, string fileFolder)
        {
            bool status = false;
            string path = string.Format("~/fileUpload/{0}/{1}.pdf", fileFolder, fileName);
            string filePath = thePage.Server.MapPath(path);

            try
            {
                if (ApaBerkasMasihAda(thePage, fileName, fileFolder))
                {
                    File.Delete(filePath);
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }

            return status;
        }

        protected void gvMitraPelaksanaPenelitian_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["IsNew"] = false;
            isiMitra(ViewState["id_usulan_kegiatan"].ToString());

            //var idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            var idMitra = Guid.Parse(gvMitraPelaksanaPenelitian.DataKeys[e.RowIndex]["id_mitra"].ToString());
            var dt = new DataTable();
            if (objMitra.getDataMitraPelaksana(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), idMitra))
            {
                if (dt.Rows.Count > 0)
                {
                    tbNamaMitraPelaksana.Text = dt.Rows[0]["nama_mitra"].ToString();
                    tbAlamatInstitusi.Text = dt.Rows[0]["alamat_institusi_mitra"].ToString();
                    tbInstitusiMitraPelaksana.Text = dt.Rows[0]["nama_institusi_mitra"].ToString();
                    tbAlamatSurelMitraPelaksana.Text = dt.Rows[0]["surel"].ToString();
                    ddlNegara.SelectedValue = dt.Rows[0]["kd_negara"].ToString();
                    var danaThn1 = dt.Rows[0]["dana_thn_1"].ToString();
                    var danaThn2 = dt.Rows[0]["dana_thn_2"].ToString();
                    var danaThn3 = dt.Rows[0]["dana_thn_3"].ToString();

                    if (danaThn1 != "")
                    {
                        tbPendanaanThn1.Text = decimal.Parse(dt.Rows[0]["dana_thn_1"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
                    }
                    if (danaThn2 != "")
                    {
                        tbPendanaanThn2.Text = decimal.Parse(dt.Rows[0]["dana_thn_2"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
                    }
                    if (danaThn3 != "")
                    {
                        tbPendanaanThn3.Text = decimal.Parse(dt.Rows[0]["dana_thn_3"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
                    }

                    isiDdlNegara();
                    ViewState["idMitra"] = idMitra;
                    mvMitra.SetActiveView(vMitraPelaksana);
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
            }
        }

        protected void gvMitraPelaksanaPenelitian_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDanaThn1 = new Label();
                lblDanaThn1 = (Label)e.Row.FindControl("lblDanaThn1");
                Label lblDanaThn2 = new Label();
                lblDanaThn2 = (Label)e.Row.FindControl("lblDanaThn2");
                Label lblDanaThn3 = new Label();
                lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
                Label lblDana1 = new Label();
                lblDana1 = (Label)e.Row.FindControl("lblDana1");
                Label lblDana2 = new Label();
                lblDana2 = (Label)e.Row.FindControl("lblDana2");
                Label lblDana3 = new Label();
                lblDana3 = (Label)e.Row.FindControl("lblDana3");
                if (lblDana1.Text != "")
                {
                    lblDana1.Text = Convert.ToDecimal(lblDana1.Text).ToString("N0");
                }
                if (lblDana2.Text != "")
                {
                    lblDana2.Text = Convert.ToDecimal(lblDana2.Text).ToString("N0");
                }
                if (lblDana3.Text != "")
                {
                    lblDana3.Text = Convert.ToDecimal(lblDana3.Text).ToString("N0");
                }

                if (lblLamaUsulan.Text == "1")
                {
                    lblDana1.Visible = true;
                    lblDana2.Visible = false;
                    lblDana3.Visible = false;
                    lblDanaThn1.Visible = true;
                    lblDanaThn2.Visible = false;
                    lblDanaThn3.Visible = false;
                }
                else if (lblLamaUsulan.Text == "2")
                {
                    lblDana1.Visible = true;
                    lblDana2.Visible = true;
                    lblDana3.Visible = false;
                    lblDanaThn1.Visible = true;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = false;
                }
                else
                {
                    lblDana1.Visible = true;
                    lblDana2.Visible = true;
                    lblDana3.Visible = true;
                    lblDanaThn1.Visible = true;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = true;
                }

                if (lblUrutanUsulan.Text == "3")
                {
                    lblDana1.Visible = false;
                    lblDana2.Visible = false;
                    lblDana3.Visible = true;
                    lblDanaThn1.Visible = false;
                    lblDanaThn2.Visible = false;
                    lblDanaThn3.Visible = true;
                }
                else if (lblUrutanUsulan.Text == "2" && lblLamaUsulan.Text == "3")
                {
                    lblDana1.Visible = false;
                    lblDana2.Visible = true;
                    lblDana3.Visible = true;
                    lblDanaThn1.Visible = false;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = true;
                }
                else if (lblUrutanUsulan.Text == "2" && lblLamaUsulan.Text == "2")
                {
                    lblDana1.Visible = false;
                    lblDana2.Visible = true;
                    lblDana3.Visible = false;
                    lblDanaThn1.Visible = false;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = false;
                }

            }
        }

        protected void gvMitraPelaksanaPenelitian_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraPelaksanaPenelitian.DataKeys[rowIndex]["id_mitra"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unggahDokMitraPenelitian")
            {
                string dirFile = "~/fileUpload/Mitra/" + ViewState["thn_usulan"].ToString();
                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }

                uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");
            }

            if (e.CommandName == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvMitraPelaksanaPenelitian.DataKeys[rowIndex]["nama_mitra"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "MitraPelaksana_" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf",
                    ViewState["thn_usulan"].ToString(), idMitra.ToString());

                if (File.Exists(Server.MapPath(filePath)))
                {
                    string tmpFolderUnduh = "~/fileUpload/Mitra/" + ViewState["thn_usulan"].ToString() + "/";
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = tmpFolderUnduh, //PATH_UNGGAH_BERKAS,
                        NamaBerkas = idMitra + ".pdf",
                        NamaBerkasdiUnduh = namaFile
                    };
                    Session["AtributUnduh"] = atributUnduh;

                    var unduhForm = "helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
                    return;
                }
            }
        }

        protected void selesai_Click(object sender, EventArgs e)
        {

            if (!objMitra.updateStsDokMitra(Guid.Parse(ViewState["idMitra"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objMitra.errorMessage);
            }
            isiMitra(ViewState["id_usulan_kegiatan"].ToString());
            isiMitraPenelitianPerSkema();
            mvMitra.SetActiveView(vMitraPelaksanaPenelitian);
        }

        protected void lbTambahMitraCalonPengguna_Click(object sender, EventArgs e)
        {
            ViewState["IsNew"] = true;
            ViewState["idMitra"] = Guid.NewGuid();
            isiDdlNegara();
            clearMCP();
            mvMitra.SetActiveView(vMitraCalonPengguna);
        }

        protected void btnSimpanMCP_Click(object sender, EventArgs e)
        {
            var isNew = bool.Parse(ViewState["IsNew"].ToString());

            List<string> emptyField = new List<string>();
            if (tbNamaMCPengguna.Text.Trim().Length == 0) emptyField.Add("Nama Mitra");
            if (tbAlamatInstitusiMCP.Text.Trim().Length == 0) emptyField.Add("Alamat Institusi Mitra");
            if (tbAlamatSurelMCP.Text.Trim().Length == 0) emptyField.Add("Alamat Surel Mitra");
            if (tbInstitusiMCP.Text.Trim().Length == 0) emptyField.Add("Nama Insitusi Mitra");
            if (ddlNegaraMCP.SelectedValue == "-1") emptyField.Add("Nama Negara Mitra");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }

            var idMitra = Guid.Parse(ViewState["idMitra"].ToString());

            string namaMitraPelaksana = tbNamaMCPengguna.Text;
            string namaInstitusiMitra = tbInstitusiMCP.Text;
            string alamatInstitusiMitra = tbAlamatInstitusiMCP.Text;
            string surel = tbAlamatSurelMCP.Text;
            string kdNegara = ddlNegaraMCP.SelectedValue;
            string danaThn1 = tbDanaMCPThn1.Text;
            string danaThn2 = tbDanaMCPThn2.Text;
            string danaThn3 = tbDanaMCPThn3.Text;
            int kdKategoriMitra = 2;

            if (!objMitra.insupMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), namaMitraPelaksana, namaInstitusiMitra, alamatInstitusiMitra
                        , surel, kdNegara, kdKategoriMitra, idMitra))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objMitra.errorMessage);
            }

            if (tbDanaMCPThn1.Text != "")
            {
                if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 1, idMitra, danaThn1, kdKategoriMitra))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objMitra.errorMessage);
                }
            }

            if (tbDanaMCPThn2.Text != "")
            {
                if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 2, idMitra, danaThn2, kdKategoriMitra))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objMitra.errorMessage);
                }
            }

            if (tbDanaMCPThn3.Text != "")
            {
                if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 3, idMitra, danaThn3, kdKategoriMitra))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objMitra.errorMessage);
                }
            }

            isiMitra(ViewState["id_usulan_kegiatan"].ToString());
            clearMCP();
            mvMitra.SetActiveView(vMitraPelaksanaPenelitian);
        }

        protected void btnBatalMCP_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vMitraPelaksanaPenelitian);
        }

        protected void gvMitraCalonPengguna_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblNamaMitraPelaksana.Text = gvMitraCalonPengguna.DataKeys[e.RowIndex]["nama_mitra"].ToString();
            ViewState["idMitraPelaksana"] = gvMitraCalonPengguna.DataKeys[e.RowIndex]["id_mitra"].ToString();

            uiModal uiMdl = new uiModal();
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvMitraCalonPengguna_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["IsNew"] = false;
            isiMitra(ViewState["id_usulan_kegiatan"].ToString());

            //var idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            var idMitra = Guid.Parse(gvMitraCalonPengguna.DataKeys[e.RowIndex]["id_mitra"].ToString());
            var dt = new DataTable();
            if (objMitra.getDataMitraPelaksana(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), idMitra))
            {
                if (dt.Rows.Count > 0)
                {
                    tbNamaMCPengguna.Text = dt.Rows[0]["nama_mitra"].ToString();
                    tbAlamatInstitusiMCP.Text = dt.Rows[0]["alamat_institusi_mitra"].ToString();
                    tbInstitusiMCP.Text = dt.Rows[0]["nama_institusi_mitra"].ToString();
                    tbAlamatSurelMCP.Text = dt.Rows[0]["surel"].ToString();
                    ddlNegaraMCP.SelectedValue = dt.Rows[0]["kd_negara"].ToString();
                    var danaThn1 = dt.Rows[0]["dana_thn_1"].ToString();
                    var danaThn2 = dt.Rows[0]["dana_thn_2"].ToString();
                    var danaThn3 = dt.Rows[0]["dana_thn_3"].ToString();

                    if (danaThn1 != "")
                    {
                        tbDanaMCPThn1.Text = decimal.Parse(dt.Rows[0]["dana_thn_1"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
                    }
                    if (danaThn2 != "")
                    {
                        tbDanaMCPThn2.Text = decimal.Parse(dt.Rows[0]["dana_thn_2"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
                    }
                    if (danaThn3 != "")
                    {
                        tbDanaMCPThn3.Text = decimal.Parse(dt.Rows[0]["dana_thn_3"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
                    }

                    isiDdlNegara();
                    ViewState["idMitra"] = idMitra;
                    mvMitra.SetActiveView(vMitraCalonPengguna);
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
            }
        }

        protected void gvMitraCalonPengguna_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDanaThn1 = new Label();
                lblDanaThn1 = (Label)e.Row.FindControl("lblDanaThn1");
                Label lblDanaThn2 = new Label();
                lblDanaThn2 = (Label)e.Row.FindControl("lblDanaThn2");
                Label lblDanaThn3 = new Label();
                lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
                Label lblDana1 = new Label();
                lblDana1 = (Label)e.Row.FindControl("lblDana1");
                Label lblDana2 = new Label();
                lblDana2 = (Label)e.Row.FindControl("lblDana2");
                Label lblDana3 = new Label();
                lblDana3 = (Label)e.Row.FindControl("lblDana3");
                if (lblDana1.Text != "")
                {
                    lblDana1.Text = Convert.ToDecimal(lblDana1.Text).ToString("N0");
                }
                if (lblDana2.Text != "")
                {
                    lblDana2.Text = Convert.ToDecimal(lblDana2.Text).ToString("N0");
                }
                if (lblDana3.Text != "")
                {
                    lblDana3.Text = Convert.ToDecimal(lblDana3.Text).ToString("N0");
                }

                if (lblLamaUsulan.Text == "1")
                {
                    lblDana1.Visible = true;
                    lblDana2.Visible = false;
                    lblDana3.Visible = false;
                    lblDanaThn1.Visible = true;
                    lblDanaThn2.Visible = false;
                    lblDanaThn3.Visible = false;
                }
                else if (lblLamaUsulan.Text == "2")
                {
                    lblDana1.Visible = true;
                    lblDana2.Visible = true;
                    lblDana3.Visible = false;
                    lblDanaThn1.Visible = true;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = false;
                }
                else
                {
                    lblDana1.Visible = true;
                    lblDana2.Visible = true;
                    lblDana3.Visible = true;
                    lblDanaThn1.Visible = true;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = true;
                }


                if (lblUrutanUsulan.Text == "3")
                {
                    lblDana1.Visible = false;
                    lblDana2.Visible = false;
                    lblDana3.Visible = true;
                    lblDanaThn1.Visible = false;
                    lblDanaThn2.Visible = false;
                    lblDanaThn3.Visible = true;
                }
                else if (lblUrutanUsulan.Text == "2" && lblLamaUsulan.Text == "3")
                {
                    lblDana1.Visible = false;
                    lblDana2.Visible = true;
                    lblDana3.Visible = true;
                    lblDanaThn1.Visible = false;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = true;
                }
                else if (lblUrutanUsulan.Text == "2" && lblLamaUsulan.Text == "2")
                {
                    lblDana1.Visible = false;
                    lblDana2.Visible = true;
                    lblDana3.Visible = false;
                    lblDanaThn1.Visible = false;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = false;
                }

            }
        }

        protected void gvMitraCalonPengguna_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraCalonPengguna.DataKeys[rowIndex]["id_mitra"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unggahDokMitraPenelitian")
            {
                string dirFile = "~/fileUpload/Mitra/" + ViewState["thn_usulan"].ToString();
                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }

                uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");
            }

            if (e.CommandName == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvMitraCalonPengguna.DataKeys[rowIndex]["nama_mitra"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "MitraCalonPengguna_" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf",
                    ViewState["thn_usulan"].ToString(), idMitra);

                if (File.Exists(Server.MapPath(filePath)))
                {
                    string tmpFolderUnduh = "~/fileUpload/Mitra/" + ViewState["thn_usulan"].ToString() + "/";
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = tmpFolderUnduh,//PATH_UNGGAH_BERKAS,
                        NamaBerkas = idMitra + ".pdf",
                        NamaBerkasdiUnduh = namaFile
                    };
                    Session["AtributUnduh"] = atributUnduh;

                    var unduhForm = "helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
                    return;
                }
            }
        }

        protected void lbTambahMitraInvestor_Click(object sender, EventArgs e)
        {
            ViewState["IsNew"] = true;
            ViewState["idMitra"] = Guid.NewGuid();
            isiDdlNegara();
            clearMI();
            mvMitra.SetActiveView(vMitraInvestor);
        }

        protected void btnSimpanMI_Click(object sender, EventArgs e)
        {
            var isNew = bool.Parse(ViewState["IsNew"].ToString());

            List<string> emptyField = new List<string>();
            if (tbNamaMI.Text.Trim().Length == 0) emptyField.Add("Nama Mitra");
            if (tbAlamatInstitusiMI.Text.Trim().Length == 0) emptyField.Add("Alamat Institusi Mitra");
            if (tbAlamatSurelMI.Text.Trim().Length == 0) emptyField.Add("Alamat Surel Mitra");
            if (tbInstitusiMI.Text.Trim().Length == 0) emptyField.Add("Nama Insitusi Mitra");
            if (ddlNegaraMI.SelectedValue == "-1") emptyField.Add("Nama Negara Mitra");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }

            var idMitra = Guid.Parse(ViewState["idMitra"].ToString());

            string namaMitraPelaksana = tbNamaMI.Text;
            string namaInstitusiMitra = tbInstitusiMI.Text;
            string alamatInstitusiMitra = tbAlamatInstitusiMI.Text;
            string surel = tbAlamatSurelMI.Text;
            string kdNegara = ddlNegaraMI.SelectedValue;
            string danaThn1 = tbDanaMIThn1.Text;
            string danaThn2 = tbDanaMIThn2.Text;
            string danaThn3 = tbDanaMIThn3.Text;
            int kdKategoriMitra = 3;

            if (!objMitra.insupMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), namaMitraPelaksana, namaInstitusiMitra, alamatInstitusiMitra
                        , surel, kdNegara, kdKategoriMitra, idMitra))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objMitra.errorMessage);
            }

            if (tbDanaMIThn1.Text != "")
            {
                if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 1, idMitra, danaThn1, kdKategoriMitra))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objMitra.errorMessage);
                }
            }

            if (tbDanaMIThn2.Text != "")
            {
                if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 2, idMitra, danaThn2, kdKategoriMitra))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objMitra.errorMessage);
                }
            }

            if (tbDanaMIThn3.Text != "")
            {
                if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 3, idMitra, danaThn3, kdKategoriMitra))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objMitra.errorMessage);
                }
            }

            isiMitra(ViewState["id_usulan_kegiatan"].ToString());
            clearMI();
            mvMitra.SetActiveView(vMitraPelaksanaPenelitian);
        }

        protected void btnBataMI_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vMitraPelaksanaPenelitian);
        }

        protected void gvMitraInvestor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblNamaMitraPelaksana.Text = gvMitraInvestor.DataKeys[e.RowIndex]["nama_mitra"].ToString();
            ViewState["idMitraPelaksana"] = gvMitraInvestor.DataKeys[e.RowIndex]["id_mitra"].ToString();

            uiModal uiMdl = new uiModal();
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvMitraInvestor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ViewState["IsNew"] = false;
            isiMitra(ViewState["id_usulan_kegiatan"].ToString());

            //var idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            var idMitra = Guid.Parse(gvMitraInvestor.DataKeys[e.RowIndex]["id_mitra"].ToString());
            var dt = new DataTable();
            if (objMitra.getDataMitraPelaksana(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), idMitra))
            {
                if (dt.Rows.Count > 0)
                {
                    tbNamaMI.Text = dt.Rows[0]["nama_mitra"].ToString();
                    tbAlamatInstitusiMI.Text = dt.Rows[0]["alamat_institusi_mitra"].ToString();
                    tbInstitusiMI.Text = dt.Rows[0]["nama_institusi_mitra"].ToString();
                    tbAlamatSurelMI.Text = dt.Rows[0]["surel"].ToString();
                    ddlNegaraMI.SelectedValue = dt.Rows[0]["kd_negara"].ToString();
                    var danaThn1 = dt.Rows[0]["dana_thn_1"].ToString();
                    var danaThn2 = dt.Rows[0]["dana_thn_2"].ToString();
                    var danaThn3 = dt.Rows[0]["dana_thn_3"].ToString();

                    if (danaThn1 != "")
                    {
                        tbDanaMIThn1.Text = decimal.Parse(dt.Rows[0]["dana_thn_1"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
                    }
                    if (danaThn2 != "")
                    {
                        tbDanaMIThn2.Text = decimal.Parse(dt.Rows[0]["dana_thn_2"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
                    }
                    if (danaThn3 != "")
                    {
                        tbDanaMIThn3.Text = decimal.Parse(dt.Rows[0]["dana_thn_3"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
                    }

                    isiDdlNegara();
                    ViewState["idMitra"] = idMitra;
                    mvMitra.SetActiveView(vMitraInvestor);
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
            }
        }

        protected void gvMitraInvestor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDanaThn1 = new Label();
                lblDanaThn1 = (Label)e.Row.FindControl("lblDanaThn1");
                Label lblDanaThn2 = new Label();
                lblDanaThn2 = (Label)e.Row.FindControl("lblDanaThn2");
                Label lblDanaThn3 = new Label();
                lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
                Label lblDana1 = new Label();
                lblDana1 = (Label)e.Row.FindControl("lblDana1");
                Label lblDana2 = new Label();
                lblDana2 = (Label)e.Row.FindControl("lblDana2");
                Label lblDana3 = new Label();
                lblDana3 = (Label)e.Row.FindControl("lblDana3");
                if (lblDana1.Text != "")
                {
                    lblDana1.Text = Convert.ToDecimal(lblDana1.Text).ToString("N0");
                }
                if (lblDana2.Text != "")
                {
                    lblDana2.Text = Convert.ToDecimal(lblDana2.Text).ToString("N0");
                }
                if (lblDana3.Text != "")
                {
                    lblDana3.Text = Convert.ToDecimal(lblDana3.Text).ToString("N0");
                }

                if (lblLamaUsulan.Text == "1")
                {
                    lblDana1.Visible = true;
                    lblDana2.Visible = false;
                    lblDana3.Visible = false;
                    lblDanaThn1.Visible = true;
                    lblDanaThn2.Visible = false;
                    lblDanaThn3.Visible = false;
                }
                else if (lblLamaUsulan.Text == "2")
                {
                    lblDana1.Visible = true;
                    lblDana2.Visible = true;
                    lblDana3.Visible = false;
                    lblDanaThn1.Visible = true;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = false;
                }
                else
                {
                    lblDana1.Visible = true;
                    lblDana2.Visible = true;
                    lblDana3.Visible = true;
                    lblDanaThn1.Visible = true;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = true;
                }


                if (lblUrutanUsulan.Text == "3")
                {
                    lblDana1.Visible = false;
                    lblDana2.Visible = false;
                    lblDana3.Visible = true;
                    lblDanaThn1.Visible = false;
                    lblDanaThn2.Visible = false;
                    lblDanaThn3.Visible = true;
                }
                else if (lblUrutanUsulan.Text == "2" && lblLamaUsulan.Text == "3")
                {
                    lblDana1.Visible = false;
                    lblDana2.Visible = true;
                    lblDana3.Visible = true;
                    lblDanaThn1.Visible = false;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = true;
                }
                else if (lblUrutanUsulan.Text == "2" && lblLamaUsulan.Text == "2")
                {
                    lblDana1.Visible = false;
                    lblDana2.Visible = true;
                    lblDana3.Visible = false;
                    lblDanaThn1.Visible = false;
                    lblDanaThn2.Visible = true;
                    lblDanaThn3.Visible = false;
                }
            }
        }

        protected void gvMitraInvestor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraInvestor.DataKeys[rowIndex]["id_mitra"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unggahDokMitraPenelitian")
            {
                string dirFile = "~/fileUpload/Mitra/" + ViewState["thn_usulan"].ToString();
                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }

                uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");
            }

            if (e.CommandName == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvMitraInvestor.DataKeys[rowIndex]["nama_mitra"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "MitraInvestor_" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf",
                    ViewState["thn_usulan"].ToString(), idMitra);

                if (File.Exists(Server.MapPath(filePath)))
                {
                    string tmpFolderUnduh = "~/fileUpload/Mitra/" + ViewState["thn_usulan"].ToString() + "/";
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = tmpFolderUnduh, //PATH_UNGGAH_BERKAS,
                        NamaBerkas = idMitra + ".pdf",
                        NamaBerkasdiUnduh = namaFile
                    };
                    Session["AtributUnduh"] = atributUnduh;

                    var unduhForm = "helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
                    return;
                }
            }
        }

        protected void lbInfoMitra_Click(object sender, EventArgs e)
        {
            uiModal uiMdl = new uiModal();
            uiMdl.ShowModal(this.Page, "modalInfo");
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

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            if (fileUpload1.HasFile)
            {
                if (fileUpload1.FileName.ToLower().EndsWith(".pdf") || fileUpload1.FileName.EndsWith(".PDF") || fileUpload1.FileName.EndsWith(".Pdf"))
                {
                    if (fileUpload1.PostedFile.ContentLength < (5 * 1024 * 1024))
                    {
                        prosesUnggah();
                        //simpan();
                        //if (OnChildUnggah != null)
                        //    OnChildUnggah(sender, null);
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File yang akan diunggah ukurannya tidak boleh melebihi 5 MByte !!!");
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }

        }



        private void prosesUnggah()
        {
            string thnUsulan = ViewState["thn_usulan"].ToString();
            string path = "~/fileUpload/Mitra/" + ViewState["thn_usulan"].ToString();
            string namaFile = path + "/" + ViewState["idMitra"].ToString() + ".pdf";
            if (File.Exists(Server.MapPath(namaFile)))
            {
                File.Delete(Server.MapPath(namaFile));
            }


            if (fileUpload1.HasFile)
            {
                if (fileUpload1.FileName.ToLower().EndsWith(".pdf"))
                {
                    if (fileUpload1.PostedFile.ContentLength < (1 * 1024 * 1024))
                    {
                        fileUpload1.SaveAs(Server.MapPath(namaFile));
                        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Unggah dokumen berhasil.");
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File yang akan diunggah ukurannya tidak boleh melebihi 1 MByte !!!");
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File belum dipilih.");
            }
        }

    }
}