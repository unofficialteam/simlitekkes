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
using simlitekkes.Models.Sistem;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class mitraAbdimas : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.mitraAbdimas objMitra = new Models.Pengusul.mitraAbdimas();
        uiNotify noty = new uiNotify();
        kontrolUnggah ktUnggah = new kontrolUnggah();
        uiModal uiMdl = new uiModal();
        manipulasiData objManipData = new manipulasiData();  

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitMitra(usulanKegiatan usulanKegiatan)
        {
            switch (usulanKegiatan.idSkema)
            {
                case 14:
                    ucPKM.setThnUsulan(usulanKegiatan.thnUsulan);
                    ucPKM.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                    mvMitra.SetActiveView(vPKM);
                    break;

                case 15:
                    ucPPK.setThnUsulan(usulanKegiatan.thnUsulan);
                    ucPPK.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                    mvMitra.SetActiveView(vPPK);
                    break;

                //case 17:
                //    ucPPUPIK.setThnUsulan(usulanKegiatan.thnUsulan);
                //    ucPPUPIK.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                //    mvMitra.SetActiveView(vPPUPIK);
                //    break;

                //case 23:
                //    ucPPMUPT.setThnUsulan(usulanKegiatan.thnUsulan);
                //    ucPPMUPT.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                //    mvMitra.SetActiveView(vPPMUPT);
                //    break;

                case 26:
                    ucPKW.setThnUsulan(usulanKegiatan.thnUsulan);
                    ucPKW.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                    mvMitra.SetActiveView(vPKW);
                    break;

                //case 28:
                //    ucKKNPPM.setThnUsulan(usulanKegiatan.thnUsulan);
                //    ucKKNPPM.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                //    mvMitra.SetActiveView(vKKNPPM);
                //    break;

                //case 29:
                //    ucPKMS.setThnUsulan(usulanKegiatan.thnUsulan);
                //    ucPKMS.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                //    mvMitra.SetActiveView(vPKMS);
                //    break;

                //case 54:
                //    ucPPPUD.setThnUsulan(usulanKegiatan.thnUsulan);
                //    ucPPPUD.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                //    mvMitra.SetActiveView(vPPPUD);
                //    break;
                case 55:
                    ucPPDM.setThnUsulan(usulanKegiatan.thnUsulan);
                    ucPPDM.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                    mvMitra.SetActiveView(vPPDM);
                    break;
                case 78:
                    ucPPDS.setThnUsulan(usulanKegiatan.thnUsulan);
                    ucPPDS.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                    ucPelaksanaPPDS.setThnUsulan(usulanKegiatan.thnUsulan);
                    ucPelaksanaPPDS.InitData(Guid.Parse(usulanKegiatan.idUsulan), Guid.Parse(usulanKegiatan.idUsulanKegiatan));
                    mvMitra.SetActiveView(vPPDS);
                    break;
            }
        }

        //public void isiMitra(string idUsulanKegiatan)
        //{
        //    ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
        //    int p_jml_data = 0;
        //    int p_offset = 0;
        //    DataTable dtMitra = new DataTable();
        //    if (objMitra.listMitraPelaksanaPenelitian(ref dtMitra, Guid.Parse(idUsulanKegiatan), p_jml_data, p_offset))
        //    {
        //        var drmitrapelaksana = dtMitra.Select("kd_kategori_mitra = 1");
        //        if(drmitrapelaksana.Length > 0)
        //        {
        //            var dt = drmitrapelaksana.CopyToDataTable();
        //            gvMitraPelaksanaPengabdian.DataSource = dt;
        //            //gvMitraPelaksanaPenelitian.DataBind();
        //        }               

        //        var drmitracalonpengguna = dtMitra.Select("kd_kategori_mitra = 2");
        //        if(drmitracalonpengguna.Length > 0)
        //        {
        //            var dtcalon = drmitracalonpengguna.CopyToDataTable();
        //            gvMitraSasaran.DataSource = dtcalon;
        //            //gvMitraCalonPengguna.DataBind();
        //        }
                
        //        gvMitraPelaksanaPengabdian.DataBind();
        //        gvMitraSasaran.DataBind();
        //        isiMitraPengabdianPerSkema();
        //    };
        //}

        //public void isiMitraPengabdianPerSkema()
        //{
        //    DataTable dtMitra = new DataTable();
        //    //objMitra.getMitraPenelitianPerSkema(ref dtDurasi, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
        //    objMitra.getMitraPengabdianPerSkema(ref dtMitra, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
        //    if (dtMitra.Rows.Count > 0)
        //    {
        //        lblSkema.Text = dtMitra.Rows[0]["nama_skema"].ToString();
        //        lblLamaUsulan.Text = dtMitra.Rows[0]["lama_kegiatan"].ToString();
        //        ViewState["lama_kegiatan"] = lblLamaUsulan.Text;
        //        lblUrutanUsulan.Text = dtMitra.Rows[0]["urutan_thn_usulan"].ToString();
        //        ViewState["mitraPelaksana"] = dtMitra.Rows[0]["mitra_pelaksana"].ToString();
        //        ViewState["mitraSasaran"] = dtMitra.Rows[0]["mitra_sasaran"].ToString();
        //        ViewState["idSkema"] = dtMitra.Rows[0]["id_skema"].ToString();
        //    }

        //    if (ViewState["mitraPelaksana"].ToString() == "1")
        //    {
        //        panelMitraPelaksana.Visible = true;
        //    }
        //    else
        //    {
        //        panelMitraPelaksana.Visible = false;
        //    }

        //    if (ViewState["mitraSasaran"].ToString() == "1")
        //    {
        //        panelMitraSasaran.Visible = true;
        //    }
        //    else
        //    {
        //        panelMitraSasaran.Visible = false;
        //    }

        //    if (ViewState["mitraPelaksana"].ToString() != "1" && ViewState["mitraSasaran"].ToString() != "1")
        //    {
        //        lblInfoMitra.Visible = true;
        //    }

        //    //if (lblLamaUsulan.Text == "1")
        //    //{
        //    //    tbPendanaanThn1.Visible = true;
        //    //    tbPendanaanThn2.Visible = false;
        //    //    tbPendanaanThn3.Visible = false;
        //    //    lblPendanaanThn1.Visible = true;
        //    //    lblPendanaanThn2.Visible = false;
        //    //    lblPendanaanThn3.Visible = false;

        //    //    tbDanaMSThn1.Visible = true;
        //    //    tbDanaMSThn2.Visible = false;
        //    //    tbDanaMSThn3.Visible = false;
        //    //    lblDanaMSThn1.Visible = true;
        //    //    lblDanaMSThn2.Visible = false;
        //    //    lblDanaMSThn3.Visible = false;
        //    //}
        //    //else if (lblLamaUsulan.Text == "2")
        //    //{
        //    //    tbPendanaanThn1.Visible = true;
        //    //    tbPendanaanThn2.Visible = true;
        //    //    tbPendanaanThn3.Visible = false;
        //    //    lblPendanaanThn1.Visible = true;
        //    //    lblPendanaanThn2.Visible = true;
        //    //    lblPendanaanThn3.Visible = false;

        //    //    tbDanaMSThn1.Visible = true;
        //    //    tbDanaMSThn2.Visible = true;
        //    //    tbDanaMSThn3.Visible = false;
        //    //    lblDanaMSThn1.Visible = true;
        //    //    lblDanaMSThn2.Visible = true;
        //    //    lblDanaMSThn3.Visible = false;
        //    //}
        //    //else
        //    //{
        //    //    tbPendanaanThn1.Visible = true;
        //    //    tbPendanaanThn2.Visible = true;
        //    //    tbPendanaanThn3.Visible = true;
        //    //    lblPendanaanThn1.Visible = true;
        //    //    lblPendanaanThn2.Visible = true;
        //    //    lblPendanaanThn3.Visible = true;

        //    //    tbDanaMSThn1.Visible = true;
        //    //    tbDanaMSThn2.Visible = true;
        //    //    tbDanaMSThn3.Visible = true;
        //    //    lblDanaMSThn1.Visible = true;
        //    //    lblDanaMSThn2.Visible = true;
        //    //    lblDanaMSThn3.Visible = true;
        //    //}

        //}

        //protected void lbTambahMitraPelaksanaPengabdian_Click(object sender, EventArgs e)
        //{
        //    string idSkema = ViewState["idSkema"].ToString();
        //    if(idSkema == "55")
        //    {
        //        mitraPPDM.InitMitra();
        //        mvMitra.SetActiveView(vMitraPelaksana);
        //    }
            
        //}

        ////private void isiDdlNegara()
        ////{
        ////    ddlNegara.Items.Clear();
        ////    ddlNegara.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
        ////    ddlNegaraMS.Items.Clear();
        ////    ddlNegaraMS.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
        ////    DataTable dt = new DataTable();
        ////    if (objMitra.listNegara(ref dt))
        ////    {
        ////        try
        ////        {
        ////            ddlNegara.DataTextField = "nama_negara";
        ////            ddlNegara.DataValueField = "kd_negara";
        ////            ddlNegara.DataSource = dt;
        ////            ddlNegara.DataBind();
        ////            ddlNegaraMS.DataTextField = "nama_negara";
        ////            ddlNegaraMS.DataValueField = "kd_negara";
        ////            ddlNegaraMS.DataSource = dt;
        ////            ddlNegaraMS.DataBind();
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
        ////        }
        ////    }
        ////}

        //protected void btnBatal_Click(object sender, EventArgs e)
        //{
        //    mvMitra.SetActiveView(vMitraPelaksanaPengabdian);
        //}

        ////protected void btnSimpan_Click(object sender, EventArgs e)
        ////{
        ////    var isNew = bool.Parse(ViewState["IsNew"].ToString());

        ////    List<string> emptyField = new List<string>();
        ////    if (tbNamaMitraPelaksana.Text.Trim().Length == 0) emptyField.Add("Nama Mitra");
        ////    if (tbAlamatInstitusi.Text.Trim().Length == 0) emptyField.Add("Alamat Institusi Mitra");
        ////    if (tbAlamatSurelMitraPelaksana.Text.Trim().Length == 0) emptyField.Add("Alamat Surel Mitra");
        ////    if (tbInstitusiMitraPelaksana.Text.Trim().Length == 0) emptyField.Add("Nama Insitusi Mitra");
        ////    if (ddlNegara.SelectedValue == "-1") emptyField.Add("Nama Negara Mitra");

        ////    if (emptyField.Count > 0)
        ////    {
        ////        var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
        ////        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
        ////        return;
        ////    }

        ////    var idMitra = Guid.Parse(ViewState["idMitra"].ToString());

        ////    string namaMitraPelaksana = tbNamaMitraPelaksana.Text;
        ////    string namaInstitusiMitra = tbInstitusiMitraPelaksana.Text;
        ////    string alamatInstitusiMitra = tbAlamatInstitusi.Text;
        ////    string surel = tbAlamatSurelMitraPelaksana.Text;
        ////    string kdNegara = ddlNegara.SelectedValue;
        ////    string danaThn1 = tbPendanaanThn1.Text;
        ////    string danaThn2 = tbPendanaanThn2.Text;
        ////    string danaThn3 = tbPendanaanThn3.Text;
        ////    int kdKategoriMitra = 1;

        ////        if (!objMitra.insupMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), namaMitraPelaksana, namaInstitusiMitra, alamatInstitusiMitra
        ////                    , surel, kdNegara, kdKategoriMitra, idMitra))
        ////        {
        ////            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        ////               objMitra.errorMessage);
        ////        }

        ////    if(tbPendanaanThn1.Text != "")
        ////    {
        ////        if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 1, idMitra, danaThn1, kdKategoriMitra))
        ////        {
        ////            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        ////               objMitra.errorMessage);
        ////        }
        ////    }

        ////    if (tbPendanaanThn2.Text != "")
        ////    {
        ////        if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 2, idMitra, danaThn2, kdKategoriMitra))
        ////        {
        ////            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        ////               objMitra.errorMessage);
        ////        }
        ////    }

        ////    if (tbPendanaanThn3.Text != "")
        ////    {
        ////        if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 3, idMitra, danaThn3, kdKategoriMitra))
        ////        {
        ////            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        ////               objMitra.errorMessage);
        ////        }
        ////    }

        ////    isiMitra(ViewState["id_usulan_kegiatan"].ToString());
        ////    clearMitraPelaksana();
        ////    mvMitra.SetActiveView(vMitraPelaksanaPengabdian);

        ////}

        ////private void clearMitraPelaksana()
        ////{
        ////    tbNamaMitraPelaksana.Text = "";
        ////    tbAlamatInstitusi.Text = "";
        ////    tbAlamatSurelMitraPelaksana.Text = "";
        ////    tbInstitusiMitraPelaksana.Text = "";
        ////    tbPendanaanThn1.Text = "";
        ////    tbPendanaanThn2.Text = "";
        ////    tbPendanaanThn3.Text = "";
        ////    ddlNegara.SelectedValue = "0";
        ////}

        ////private void clearMS()
        ////{ 
        ////    tbNamaMS.Text = "";
        ////    tbAlamatInstitusiMS.Text = "";
        ////    tbAlamatSurelMS.Text = "";
        ////    tbInstitusiMS.Text = "";
        ////    tbDanaMSThn1.Text = "";
        ////    tbDanaMSThn2.Text = "";
        ////    tbDanaMSThn3.Text = "";
        ////    //ddlNegaraMCP.SelectedValue ="0";
        ////}

        //protected void gvMitraPelaksanaPengabdian_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    lblNamaMitraPelaksana.Text = gvMitraPelaksanaPengabdian.DataKeys[e.RowIndex]["nama_mitra"].ToString();
        //    ViewState["idMitraPelaksana"] = gvMitraPelaksanaPengabdian.DataKeys[e.RowIndex]["id_mitra"].ToString();

        //    uiModal uiMdl = new uiModal();
        //    uiMdl.ShowModal(this.Page, "modalHapus");
        //}

        //protected void lbHapus_Click(object sender, EventArgs e)
        //{
        //    Guid idMitraPelaksana = Guid.Parse(ViewState["idMitraPelaksana"].ToString());

        //    const string FolderName = "mitra";
        //    string idMitra = ViewState["idMitraPelaksana"].ToString();
        //    if (ApaBerkasMasihAda(this.Page, idMitra, FolderName))
        //    {
        //        if (!HapusBerkas(this.Page, idMitra, FolderName))
        //        {
        //            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Berkas Pendukung tidak dapat dihapus !");

        //            return;
        //        }
        //    }

        //    string filePath = string.Format("~/fileUpload/Mitra/{0}.pdf", idMitra);

        //    if (objMitra.deleteData(idMitraPelaksana))
        //    {
        //        ViewState.Remove("idMitra");
        //        isiMitra(ViewState["id_usulan_kegiatan"].ToString());
        //        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
        //           "Data Mitra berhasil dihapus...");
        //        isiMitra(ViewState["id_usulan_kegiatan"].ToString());
        //    }
        //    else
        //    {
        //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        //           objMitra.errorMessage);
        //    }
        //}

        //public bool ApaBerkasMasihAda(Page thePage, string fileName, string fileFolder)
        //{
        //    string filePath = thePage.Server.MapPath(string.Format("~/fileUpload/{0}/{1}.pdf", fileFolder, fileName));
        //    return (File.Exists(filePath));
        //}

        //public bool HapusBerkas(Page thePage, string fileName, string fileFolder)
        //{
        //    bool status = false;
        //    string filePath = thePage.Server.MapPath(string.Format("~/fileUpload/{0}/{1}.pdf", fileFolder, fileName));
        //    try
        //    {
        //        File.Delete(filePath);
        //        status = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = ex.Message;
        //    }

        //    return status;
        //}

        //protected void gvMitraPelaksanaPengabdian_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        ////    ViewState["IsNew"] = false;
        ////    isiMitra(ViewState["id_usulan_kegiatan"].ToString());

        ////    //var idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
        ////    var idMitra = Guid.Parse(gvMitraPelaksanaPengabdian.DataKeys[e.RowIndex]["id_mitra"].ToString());
        ////    var dt = new DataTable();
        ////    if (objMitra.getDataMitraPelaksana(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), idMitra))
        ////    {
        ////        if (dt.Rows.Count > 0)
        ////        {
        ////            tbNamaMitraPelaksana.Text = dt.Rows[0]["nama_mitra"].ToString();
        ////            tbAlamatInstitusi.Text = dt.Rows[0]["alamat_institusi_mitra"].ToString();
        ////            tbInstitusiMitraPelaksana.Text = dt.Rows[0]["nama_institusi_mitra"].ToString();
        ////            tbAlamatSurelMitraPelaksana.Text = dt.Rows[0]["surel"].ToString();
        ////            ddlNegara.SelectedValue = dt.Rows[0]["kd_negara"].ToString();
        ////            var danaThn1 = dt.Rows[0]["dana_thn_1"].ToString();
        ////            var danaThn2 = dt.Rows[0]["dana_thn_2"].ToString();
        ////            var danaThn3 = dt.Rows[0]["dana_thn_3"].ToString();

        ////            if (danaThn1 != "")
        ////            {
        ////                tbPendanaanThn1.Text = decimal.Parse(dt.Rows[0]["dana_thn_1"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
        ////            }
        ////            if (danaThn2 != "")
        ////            {
        ////                tbPendanaanThn2.Text = decimal.Parse(dt.Rows[0]["dana_thn_2"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
        ////            }
        ////            if (danaThn3 != "")
        ////            {
        ////                tbPendanaanThn3.Text = decimal.Parse(dt.Rows[0]["dana_thn_3"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
        ////            }

        ////            isiDdlNegara();
        ////            ViewState["idMitra"] = idMitra;
        ////            mvMitra.SetActiveView(vMitraPelaksana);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
        ////    }
        //}

        //protected void gvMitraPelaksanaPengabdian_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    GridViewRow theRow = e.Row;
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblDanaThn1 = new Label();
        //        lblDanaThn1 = (Label)e.Row.FindControl("lblDanaThn1");
        //        Label lblDanaThn2 = new Label();
        //        lblDanaThn2 = (Label)e.Row.FindControl("lblDanaThn2");
        //        Label lblDanaThn3 = new Label();
        //        lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
        //        Label lblDana1 = new Label();
        //        lblDana1 = (Label)e.Row.FindControl("lblDana1");
        //        Label lblDana2 = new Label();
        //        lblDana2 = (Label)e.Row.FindControl("lblDana2");
        //        Label lblDana3 = new Label();
        //        lblDana3 = (Label)e.Row.FindControl("lblDana3");
        //        if (lblDana1.Text != "")
        //        {
        //            lblDana1.Text = Convert.ToDecimal(lblDana1.Text).ToString("N0");
        //        }
        //        if (lblDana2.Text != "")
        //        {
        //            lblDana2.Text = Convert.ToDecimal(lblDana2.Text).ToString("N0");
        //        }
        //        if (lblDana3.Text != "")
        //        {
        //            lblDana3.Text = Convert.ToDecimal(lblDana3.Text).ToString("N0");
        //        }

        //        if (lblLamaUsulan.Text == "1")
        //        {
        //            lblDana1.Visible = true;
        //            lblDana2.Visible = false;
        //            lblDana3.Visible = false;
        //            lblDanaThn1.Visible = true;
        //            lblDanaThn2.Visible = false;
        //            lblDanaThn3.Visible = false;
        //        }
        //        else if (lblLamaUsulan.Text == "2")
        //        {
        //            lblDana1.Visible = true;
        //            lblDana2.Visible = true;
        //            lblDana3.Visible = false;
        //            lblDanaThn1.Visible = true;
        //            lblDanaThn2.Visible = true;
        //            lblDanaThn3.Visible = false;
        //        }
        //        else
        //        {
        //            lblDana1.Visible = true;
        //            lblDana2.Visible = true;
        //            lblDana3.Visible = true;
        //            lblDanaThn1.Visible = true;
        //            lblDanaThn2.Visible = true;
        //            lblDanaThn3.Visible = true;
        //        }
        //    }
        //}

        //protected void gvMitraPelaksanaPengabdian_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int rowIndex = int.Parse(e.CommandArgument.ToString());
        //    Guid idMitra = Guid.Parse(gvMitraPelaksanaPengabdian.DataKeys[rowIndex]["id_mitra"].ToString());

        //    ViewState["idMitra"] = idMitra.ToString();
        //    if (e.CommandName == "unggahDokMitraPenelitian")
        //    {
        //        ktUnggah.path2save = "~/fileUpload/Mitra/" + idMitra + ".pdf";
        //        ktUnggah.max_size = 1024 * 1024; // 500KB
        //        ktUnggah.alllowed_ext = "pdf;PDF";
        //        ktUnggah.success_info = "Unggah dokumen Mitra Pelaksana berhasil";
        //        ktUnggah.failed_info = "Unggah dokumen Mitra Pelaksana gagal";
        //        Session.Add("ktUnggah", ktUnggah);
        //        uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");
        //    }

        //    if (e.CommandName == "unduhDokumenMitraPelaksana")
        //    {
        //        string namaMitra = gvMitraPelaksanaPengabdian.DataKeys[rowIndex]["nama_mitra"].ToString();
        //        string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
        //        namaFile = "MitraPelaksana_" + namaFile.Replace(" ", "_") + ".pdf";
        //        namaFile = objManipData.removeUnicode(namaFile);
        //        string filePath = string.Format("~/fileUpload/Mitra/{0}.pdf",
        //            idMitra.ToString());

        //        if (File.Exists(Server.MapPath(filePath)))
        //        {
        //            var atributUnduh = new AtributUnduh
        //            {
        //                FolderUnduh = "~/fileUpload/Mitra/",//PATH_UNGGAH_BERKAS,
        //                NamaBerkas = idMitra + ".pdf",
        //                NamaBerkasdiUnduh = namaFile
        //            };
        //            Session["AtributUnduh"] = atributUnduh;

        //            var unduhForm = "helper/unduhFile.aspx";
        //            Response.Redirect(unduhForm);
        //        }
        //        else
        //        {
        //            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        //                "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
        //            return;
        //        }
        //    }
        //}

        //protected void selesai_Click(object sender, EventArgs e)
        //{

        //    if (!objMitra.updateStsDokMitra(Guid.Parse(ViewState["idMitra"].ToString())))
        //    {
        //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        //           objMitra.errorMessage);
        //    }
        //    isiMitra(ViewState["id_usulan_kegiatan"].ToString());
        //    isiMitraPengabdianPerSkema();
        //    mvMitra.SetActiveView(vMitraPelaksanaPengabdian);
        //}

        //protected void lbTambahMitraSasaran_Click(object sender, EventArgs e)
        //{
        //    string idSkema = ViewState["idSkema"].ToString();
        //    if (idSkema == "55")
        //    {
        //        mitraPPDMS.InitMitraSasaran();
        //        //isiDdlNegara();
        //        //clearMitraPelaksana();
        //        mvMitra.SetActiveView(vMitraSasaran);
        //    }
        //}

        //protected void btnSimpanMS_Click(object sender, EventArgs e)
        //{
        ////    var isNew = bool.Parse(ViewState["IsNew"].ToString());

        ////    List<string> emptyField = new List<string>();
        ////    if (tbNamaMS.Text.Trim().Length == 0) emptyField.Add("Nama Mitra");
        ////    if (tbAlamatInstitusiMS.Text.Trim().Length == 0) emptyField.Add("Alamat Institusi Mitra");
        ////    if (tbAlamatSurelMS.Text.Trim().Length == 0) emptyField.Add("Alamat Surel Mitra");
        ////    if (tbInstitusiMS.Text.Trim().Length == 0) emptyField.Add("Nama Insitusi Mitra");
        ////    if (ddlNegaraMS.SelectedValue == "-1") emptyField.Add("Nama Negara Mitra");

        ////    if (emptyField.Count > 0)
        ////    {
        ////        var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
        ////        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
        ////        return;
        ////    }

        ////    var idMitra = Guid.Parse(ViewState["idMitra"].ToString());

        ////    string namaMitraPelaksana = tbNamaMS.Text;
        ////    string namaInstitusiMitra = tbInstitusiMS.Text;
        ////    string alamatInstitusiMitra = tbAlamatInstitusiMS.Text;
        ////    string surel = tbAlamatSurelMS.Text;
        ////    string kdNegara = ddlNegaraMS.SelectedValue;
        ////    string danaThn1 = tbDanaMSThn1.Text;
        ////    string danaThn2 = tbDanaMSThn2.Text;
        ////    string danaThn3 = tbDanaMSThn3.Text;
        ////    int kdKategoriMitra = 2;

        ////    if (!objMitra.insupMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), namaMitraPelaksana, namaInstitusiMitra, alamatInstitusiMitra
        ////                , surel, kdNegara, kdKategoriMitra, idMitra))
        ////    {
        ////        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        ////           objMitra.errorMessage);
        ////    }

        ////    if (tbDanaMSThn1.Text != "")
        ////    {
        ////        if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 1, idMitra, danaThn1, kdKategoriMitra))
        ////        {
        ////            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        ////               objMitra.errorMessage);
        ////        }
        ////    }

        ////    if (tbDanaMSThn2.Text != "")
        ////    {
        ////        if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 2, idMitra, danaThn2, kdKategoriMitra))
        ////        {
        ////            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        ////               objMitra.errorMessage);
        ////        }
        ////    }

        ////    if (tbDanaMSThn3.Text != "")
        ////    {
        ////        if (!objMitra.insupDanaMitraPelaksana(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 3, idMitra, danaThn3, kdKategoriMitra))
        ////        {
        ////            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        ////               objMitra.errorMessage);
        ////        }
        ////    }

        ////    isiMitra(ViewState["id_usulan_kegiatan"].ToString());
        ////    clearMS();
        ////    mvMitra.SetActiveView(vMitraPelaksanaPengabdian);
        //}

        //protected void btnBatalMS_Click(object sender, EventArgs e)
        //{
        ////    mvMitra.SetActiveView(vMitraPelaksanaPengabdian);
        //}

        //protected void gvMitraSasaran_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    lblNamaMitraPelaksana.Text = gvMitraSasaran.DataKeys[e.RowIndex]["nama_mitra"].ToString();
        //    ViewState["idMitraPelaksana"] = gvMitraSasaran.DataKeys[e.RowIndex]["id_mitra"].ToString();

        //    uiModal uiMdl = new uiModal();
        //    uiMdl.ShowModal(this.Page, "modalHapus");
        //}

        //protected void gvMitraSasaran_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        ////    ViewState["IsNew"] = false;
        ////    isiMitra(ViewState["id_usulan_kegiatan"].ToString());

        ////    //var idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
        ////    var idMitra = Guid.Parse(gvMitraSasaran.DataKeys[e.RowIndex]["id_mitra"].ToString());
        ////    var dt = new DataTable();
        ////    if (objMitra.getDataMitraPelaksana(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), idMitra))
        ////    {
        ////        if (dt.Rows.Count > 0)
        ////        {
        ////            tbNamaMS.Text = dt.Rows[0]["nama_mitra"].ToString();
        ////            tbAlamatInstitusiMS.Text = dt.Rows[0]["alamat_institusi_mitra"].ToString();
        ////            tbInstitusiMS.Text = dt.Rows[0]["nama_institusi_mitra"].ToString();
        ////            tbAlamatSurelMS.Text = dt.Rows[0]["surel"].ToString();
        ////            ddlNegaraMS.SelectedValue = dt.Rows[0]["kd_negara"].ToString();
        ////            var danaThn1 = dt.Rows[0]["dana_thn_1"].ToString();
        ////            var danaThn2 = dt.Rows[0]["dana_thn_2"].ToString();
        ////            var danaThn3 = dt.Rows[0]["dana_thn_3"].ToString();

        ////            if (danaThn1 != "")
        ////            {
        ////                tbDanaMSThn1.Text = decimal.Parse(dt.Rows[0]["dana_thn_1"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
        ////            }
        ////            if (danaThn2 != "")
        ////            {
        ////                tbDanaMSThn2.Text = decimal.Parse(dt.Rows[0]["dana_thn_2"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
        ////            }
        ////            if (danaThn3 != "")
        ////            {
        ////                tbDanaMSThn3.Text = decimal.Parse(dt.Rows[0]["dana_thn_3"].ToString()).ToString("N0", CultureInfo.InvariantCulture);
        ////            }

        ////            isiDdlNegara();
        ////            ViewState["idMitra"] = idMitra;
        ////            mvMitra.SetActiveView(vMitraSasaran);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
        ////    }
        //}

        //protected void gvMitraSasaran_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    GridViewRow theRow = e.Row;
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblDanaThn1 = new Label();
        //        lblDanaThn1 = (Label)e.Row.FindControl("lblDanaThn1");
        //        Label lblDanaThn2 = new Label();
        //        lblDanaThn2 = (Label)e.Row.FindControl("lblDanaThn2");
        //        Label lblDanaThn3 = new Label();
        //        lblDanaThn3 = (Label)e.Row.FindControl("lblDanaThn3");
        //        Label lblDana1 = new Label();
        //        lblDana1 = (Label)e.Row.FindControl("lblDana1");
        //        Label lblDana2 = new Label();
        //        lblDana2 = (Label)e.Row.FindControl("lblDana2");
        //        Label lblDana3 = new Label();
        //        lblDana3 = (Label)e.Row.FindControl("lblDana3");
        //        if (lblDana1.Text != "")
        //        {
        //            lblDana1.Text = Convert.ToDecimal(lblDana1.Text).ToString("N0");
        //        }
        //        if (lblDana2.Text != "")
        //        {
        //            lblDana2.Text = Convert.ToDecimal(lblDana2.Text).ToString("N0");
        //        }
        //        if (lblDana3.Text != "")
        //        {
        //            lblDana3.Text = Convert.ToDecimal(lblDana3.Text).ToString("N0");
        //        }

        //        if (lblLamaUsulan.Text == "1")
        //        {
        //            lblDana1.Visible = true;
        //            lblDana2.Visible = false;
        //            lblDana3.Visible = false;
        //            lblDanaThn1.Visible = true;
        //            lblDanaThn2.Visible = false;
        //            lblDanaThn3.Visible = false;
        //        }
        //        else if (lblLamaUsulan.Text == "2")
        //        {
        //            lblDana1.Visible = true;
        //            lblDana2.Visible = true;
        //            lblDana3.Visible = false;
        //            lblDanaThn1.Visible = true;
        //            lblDanaThn2.Visible = true;
        //            lblDanaThn3.Visible = false;
        //        }
        //        else
        //        {
        //            lblDana1.Visible = true;
        //            lblDana2.Visible = true;
        //            lblDana3.Visible = true;
        //            lblDanaThn1.Visible = true;
        //            lblDanaThn2.Visible = true;
        //            lblDanaThn3.Visible = true;
        //        }
        //    }
        //}

        //protected void gvMitraSasaran_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int rowIndex = int.Parse(e.CommandArgument.ToString());
        //    Guid idMitra = Guid.Parse(gvMitraSasaran.DataKeys[rowIndex]["id_mitra"].ToString());

        //    ViewState["idMitra"] = idMitra.ToString();
        //    if (e.CommandName == "unggahDokMitraPenelitian")
        //    {
        //        ktUnggah.path2save = "~/fileUpload/Mitra/" + idMitra + ".pdf";
        //        ktUnggah.max_size = 1024 * 1024; // 500KB
        //        ktUnggah.alllowed_ext = "pdf;PDF";
        //        ktUnggah.success_info = "Unggah dokumen Mitra Calon Pengguna berhasil";
        //        ktUnggah.failed_info = "Unggah dokumen Mitra Calon Pengguna gagal";
        //        Session.Add("ktUnggah", ktUnggah);
        //        uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");
        //    }

        //    if (e.CommandName == "unduhDokumenMitraPelaksana")
        //    {
        //        string namaMitra = gvMitraSasaran.DataKeys[rowIndex]["nama_mitra"].ToString();
        //        string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
        //        namaFile = "MitraCalonPengguna_" + namaFile.Replace(" ", "_") + ".pdf";
        //        namaFile = objManipData.removeUnicode(namaFile);
        //        string filePath = string.Format("~/fileUpload/Mitra/{0}.pdf",
        //            idMitra);

        //        if (File.Exists(Server.MapPath(filePath)))
        //        {
        //            var atributUnduh = new AtributUnduh
        //            {
        //                FolderUnduh = "~/fileUpload/Mitra/",//PATH_UNGGAH_BERKAS,
        //                NamaBerkas = idMitra + ".pdf",
        //                NamaBerkasdiUnduh = namaFile
        //            };
        //            Session["AtributUnduh"] = atributUnduh;

        //            var unduhForm = "helper/unduhFile.aspx";
        //            Response.Redirect(unduhForm);
        //        }
        //        else
        //        {
        //            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
        //                "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
        //            return;
        //        }
        //    }
        //}

        //protected void lbInfoMitra_Click(object sender, EventArgs e)
        //{
        //    uiModal uiMdl = new uiModal();
        //    uiMdl.ShowModal(this.Page, "modalInfo");
        //}

        //public static string ConvertDateTimeToDate(string dateTimeString, string dateTimeFormat, String langCulture)
        //{
        //    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(langCulture);
        //    DateTime dt = DateTime.MinValue;
        //    if (DateTime.TryParse(dateTimeString, out dt))
        //    {
        //        return dt.ToString(dateTimeFormat, culture);
        //    }
        //    return dateTimeString;
        //}

        //void Child1_OnChildEventOccurs(object sender, EventArgs e)
        //{
        //    //new uiModal().ShowModal(this.Page, "modalTKT");
        //    mvMitra.SetActiveView(vMitraPelaksanaPengabdian);
        //}

    }
}