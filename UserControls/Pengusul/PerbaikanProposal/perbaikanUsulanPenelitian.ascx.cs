using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.Models;
using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System.IO;
using simlitekkes.Helper;
using simlitekkes.Models.report;

namespace simlitekkes.UserControls.Pengusul.PerbaikanProposal
{
    public partial class perbaikanUsulanPenelitian : System.Web.UI.UserControl
    {
        login objLogin;
        Models.Pengusul.berandaPengusul objBerandaPengusul = new Models.Pengusul.berandaPengusul();
        Models.Sistem.usulanKegiatan objUsulanKegiatan = new Models.Sistem.usulanKegiatan();
        PerbaikanUsulan objPerbaikanUsulan = new PerbaikanUsulan();
        uiNotify noty = new uiNotify();
        uiDropdownList objDdl = new uiDropdownList();
        uiGridView objGridView = new uiGridView();
        Models.Pengusul.luaran objLuaran = new Models.Pengusul.luaran();

        pdfUsulanBaru objPdfLuaran = new pdfUsulanBaru();

        public event EventHandler OnChildRowUpdate;
        public event EventHandler OnChildUnduhPdfUsulanLengkap;

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (login)Session["objLogin"];
            if (!IsPostBack)
            {
                isiDdlThnPublikasi();
                isiDataDosen();
                isiUsulanPerbaikan();
            }
        }

        public void init()
        {
            isiDdlThnPublikasi();
            isiDataDosen();
            isiUsulanPerbaikan();
        }

        private void isiDdlThnPublikasi()
        {
            ddlTahun.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            //thnSKg = thnSKg + 1;
            for (int i = thnSKg; i >= 2022; i--)
            {
                ddlTahun.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void isiDataDosen()
        {
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_pendaftaran = ddlTahun.SelectedValue;
            ViewState["thn_pelaksanaan"] = thn_pendaftaran;

            // Isi Data Dosen
            var dtPersonal = new DataTable();
            if (objBerandaPengusul.getPersonal(ref dtPersonal, id_personal))
            {
                if (dtPersonal.Rows.Count > 0)
                {
                    lblInstitusi.Text = dtPersonal.Rows[0]["nama_institusi"].ToString();
                    lblProgramStudi.Text = dtPersonal.Rows[0]["nama_program_studi"].ToString();
                    lblPendidikan.Text = dtPersonal.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
                    lblHIndex.Text = dtPersonal.Rows[0]["hindex"].ToString();
                    lblIdSinta.Text = dtPersonal.Rows[0]["id_sinta"].ToString();
                    lblNIDN.Text = dtPersonal.Rows[0]["nidn"].ToString();
                    lblSurel.Text = dtPersonal.Rows[0]["surel"].ToString();
                }
            }
        }

        private void isiUsulanPerbaikan()
        {
            DataTable dt = new DataTable();

            string[] kolomUsulanPerbaikan = { "id_usulan", "dana_disetujui", "thn_pertama_usulan","thn_pelaksanaan_kegiatan", "judul", "makro_riset", "bidang_fokus"
                                    , "nama_skema", "lama_kegiatan", "peran_personil", "thn_terakhir", "total_dana_disetujui"
                                    , "sts_perbaikan" };

            dt = objBerandaPengusul.currentRecords;
            if (objBerandaPengusul.listUsulanPerbaikan(ref dt, Guid.Parse(objLogin.idPersonal), ddlTahun.SelectedValue))
                dt = objBerandaPengusul.currentRecords;
            if (!objGridView.bindToGridView(ref gvUsulanPerbaikan, dt, kolomUsulanPerbaikan))
                if (dt.Rows.Count == 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada DATA");
                };
        }

        protected void gvUsulanPerbaikan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            objUsulanKegiatan.idUsulan = gvUsulanPerbaikan.DataKeys[e.RowIndex]["id_usulan"].ToString();
            objUsulanKegiatan.idUsulanKegiatan = gvUsulanPerbaikan.DataKeys[e.RowIndex]["id_usulan_kegiatan"].ToString();
            objUsulanKegiatan.idSkema = int.Parse(gvUsulanPerbaikan.DataKeys[e.RowIndex]["id_skema"].ToString());
            objUsulanKegiatan.thnPelaksanaan = gvUsulanPerbaikan.DataKeys[e.RowIndex]["thn_pelaksanaan_kegiatan"].ToString();
            objUsulanKegiatan.lamaKegiatan = int.Parse(gvUsulanPerbaikan.DataKeys[e.RowIndex]["lama_kegiatan"].ToString());
            objUsulanKegiatan.urutanTahunUsulanKegiatan = int.Parse(gvUsulanPerbaikan.DataKeys[e.RowIndex]["urutan_thn_usulan_kegiatan"].ToString());
            objUsulanKegiatan.namaSkema = gvUsulanPerbaikan.DataKeys[e.RowIndex]["nama_skema"].ToString();
            objUsulanKegiatan.tktTarget = int.Parse(gvUsulanPerbaikan.DataKeys[e.RowIndex]["level_tkt"].ToString());
            objUsulanKegiatan.thnPertamaUsulan = gvUsulanPerbaikan.DataKeys[e.RowIndex]["thn_pertama_usulan"].ToString();
            objUsulanKegiatan.thnUsulan = (int.Parse(objUsulanKegiatan.thnPertamaUsulan) + objUsulanKegiatan.urutanTahunUsulanKegiatan - 1).ToString();
            objUsulanKegiatan.judul = gvUsulanPerbaikan.DataKeys[e.RowIndex]["judul"].ToString();

            string id_transaksi_kegiatan = gvUsulanPerbaikan.DataKeys[e.RowIndex]["id_transaksi_kegiatan"].ToString();
            string id_usulan_kegiatan = gvUsulanPerbaikan.DataKeys[e.RowIndex]["id_usulan_kegiatan"].ToString();

            ViewState["usulan_kegiatan"] = objUsulanKegiatan;
            ViewState["id_transaksi_kegiatan"] = id_transaksi_kegiatan;
            ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
            if (OnChildRowUpdate != null)
                OnChildRowUpdate(sender, null);
        }
        protected void gvUsulanPerbaikan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id_transaksi_kegiatan = gvUsulanPerbaikan.DataKeys[e.Row.RowIndex]["id_transaksi_kegiatan"].ToString();
                string thn_pelaksanaan_kegiatan = gvUsulanPerbaikan.DataKeys[e.Row.RowIndex]["thn_pelaksanaan_kegiatan"].ToString();
                //string filePath = String.Format("~/fileUpload/dokumenUsulanRevisi/{0}/{1}.pdf", thn_pelaksanaan_kegiatan, id_transaksi_kegiatan);
                string filePath = "~/fileUpload/dokumenUsulanRevisi/" + thn_pelaksanaan_kegiatan + "/" + id_transaksi_kegiatan + ".pdf";
                string id_usulan = gvUsulanPerbaikan.DataKeys[e.Row.RowIndex]["id_usulan"].ToString();
                string id_usulan_kegiatan = gvUsulanPerbaikan.DataKeys[e.Row.RowIndex]["id_usulan_kegiatan"].ToString();
                string urutan_thn_usulan_kegiatan = gvUsulanPerbaikan.DataKeys[e.Row.RowIndex]["urutan_thn_usulan_kegiatan"].ToString();
                string level_tkt = gvUsulanPerbaikan.DataKeys[e.Row.RowIndex]["level_tkt"].ToString();
                string sts_perbaikan = gvUsulanPerbaikan.DataKeys[e.Row.RowIndex]["sts_perbaikan"].ToString();
                string id_skema = gvUsulanPerbaikan.DataKeys[e.Row.RowIndex]["id_skema"].ToString();
                string thn_pertama_usulan = gvUsulanPerbaikan.DataKeys[e.Row.RowIndex]["thn_pertama_usulan"].ToString();
                Label lblStsPerbaikan = (Label)e.Row.FindControl("lblStsPerbaikan");
                LinkButton lbUnduhBerkas = (LinkButton)e.Row.FindControl("lbUnduhBerkas");

                if (File.Exists(Server.MapPath(filePath)))
                {
                    lbUnduhBerkas.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbUnduhBerkas.ForeColor = System.Drawing.Color.Gray;
                }

                Label lblPeranPersonil = (Label)e.Row.FindControl("lblPeranPersonil");

                LinkButton lbUbah = (LinkButton)e.Row.FindControl("lbUbah");
                lbUbah.Visible = false;
                if (lblPeranPersonil.Text.ToLower() == "ketua pengusul")
                {
                    //lbUbah.Visible = true;

                    DataTable dtcek = new DataTable();
                    string tahapan = "14";
                    if (objPerbaikanUsulan.cekjadwal(ref dtcek, id_skema, tahapan))
                    {
                        if (dtcek.Rows.Count > 0)
                        {
                            lbUbah.Visible = true;
                        }
                        else
                        {
                            Guid id_personal = Guid.Parse(objLogin.idPersonal.ToString());
                            if (objPerbaikanUsulan.getDaftarWhitelist(id_personal, int.Parse(id_skema),
                                tahapan, thn_pertama_usulan, thn_pelaksanaan_kegiatan) == true)
                            {
                                lbUbah.Visible = true;
                            }
                            else
                            {
                                lbUbah.Visible = false;
                            }
                        }
                    }
                }

                LinkButton lbBatalkan = (LinkButton)e.Row.FindControl("lbBatalkan");
                lbBatalkan.Visible = false;
                if (sts_perbaikan == "Sudah diperbaiki")
                {
                    lbUbah.Visible = false;
                    //lbBatalkan.Visible = true;
                    lblStsPerbaikan.CssClass = "label label-success";
                }


                //GridView gvLuaranWajib = (GridView)e.Row.FindControl("gvLuaranWajib");
                //DataTable dtl = new DataTable();
                //objPdfLuaran.GetLuaranWajib(ref dtl, Guid.Parse(id_usulan_kegiatan));
                //gvLuaranWajib.DataSource = dtl;
                //gvLuaranWajib.DataBind();
            }
        }

        public Models.Sistem.usulanKegiatan getObjUsulanKegiatan()
        {
            Models.Sistem.usulanKegiatan obj = (Models.Sistem.usulanKegiatan)ViewState["usulan_kegiatan"];
            return obj;
        }

        public string getIdTransaksiKegiatan()
        {
            return ViewState["id_transaksi_kegiatan"].ToString();
        }

        public string getIdUsulanKegiatan()
        {
            return ViewState["id_usulan_kegiatan"].ToString();
        }


        protected void gvUsulanPerbaikan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id_transaksi_kegiatan = gvUsulanPerbaikan.DataKeys[rowIndex]["id_transaksi_kegiatan"].ToString();
            string id_usulan_kegiatan = gvUsulanPerbaikan.DataKeys[rowIndex]["id_usulan_kegiatan"].ToString();
            string thn_pelaksanaan_kegiatan = gvUsulanPerbaikan.DataKeys[rowIndex]["thn_pelaksanaan_kegiatan"].ToString();
            string filePath = String.Format("fileUpload/dokumenUsulanRevisi/{0}/{1}.pdf", thn_pelaksanaan_kegiatan, id_transaksi_kegiatan);

            if (e.CommandName == "unduhDokumen")
            {
                ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
                ViewState["id_transaksi_kegiatan"] = id_transaksi_kegiatan;
                if (OnChildUnduhPdfUsulanLengkap != null)
                    OnChildUnduhPdfUsulanLengkap(sender, null);

                //if (File.Exists(Server.MapPath(filePath)))
                //{
                //    string PATH_UNGGAH_BERKAS = "../fileUpload/dokumenUsulanRevisi/" + thn_pelaksanaan_kegiatan;
                //    string namaBerkas = "dokumenUsulan.pdf";
                //    var atributUnduh = new AtributUnduh
                //    {
                //        FolderUnduh = PATH_UNGGAH_BERKAS,
                //        NamaBerkas = id_transaksi_kegiatan + ".pdf",
                //        NamaBerkasdiUnduh = namaBerkas
                //    };
                //    Session["AtributUnduh"] = atributUnduh;
                //    var unduhForm = "helper/unduhFile.aspx";
                //    Response.Redirect(unduhForm);
                //}
                //else
                //{
                //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
                //}
            }


            if (e.CommandName == "batalkan")
            {

            }
        }

        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiUsulanPerbaikan();
        }
    }
}