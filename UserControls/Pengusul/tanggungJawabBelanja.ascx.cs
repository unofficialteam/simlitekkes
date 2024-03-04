using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using simlitekkes.Models.report;
using System.IO;
using simlitekkes.Models.pengusul.report;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class tanggungJawabBelanja : System.Web.UI.UserControl
    {

        simlitekkes.Models.Pengusul.pelaporan objPelaporan = new simlitekkes.Models.Pengusul.pelaporan();
        Models.login objLogin;
        Core.manipulasiData objManipData = new Core.manipulasiData();
        uiNotify noty = new uiNotify();
        uiModal uiMdl = new uiModal();

        Dictionary<string, string> dictInfoUsulan = new Dictionary<string, string>();
        templatePdf tPdf = new templatePdf(11);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mvMain.SetActiveView(vDaftarUsulanLuaran);
                isiDdlThnPelaksanaan();
                isigvSPTB();
            }

        }
        private void isiDdlThnPelaksanaan()
        {
            ddlThnPelaksanaan.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2019; i--)
            {
                ddlThnPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlThnPelaksanaan.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void isigvSPTB()
        {
            objLogin = (Models.login)Session["objLogin"];
            DataTable dt = new DataTable();
            Guid idPersonal = Guid.Parse(objLogin.idPersonal);
            Guid id_personal = Guid.Parse(objLogin.idPersonal.ToString());

            if (!objPelaporan.getDaftarTanggungJawabBelanja(ref dt, id_personal, ddlThnPelaksanaan.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + objPelaporan.errorMessage);
                return;
            }
            else
            {
                gvSPTB.DataSource = dt;
                gvSPTB.DataBind();
            }
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isigvSPTB();
        }

        protected void lbKembali2_Click(object sender, EventArgs e)
        {

        }

        protected void gvSPTB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status_tunggal = gvSPTB.DataKeys[e.Row.RowIndex]["status_tunggal"].ToString();
                string id_skema = gvSPTB.DataKeys[e.Row.RowIndex]["id_skema"].ToString();

                Label lblketsptb70 = new Label();
                lblketsptb70 = (Label)e.Row.FindControl("lblketsptb70");
                LinkButton lbCetakPengesahan70 = new LinkButton();
                lbCetakPengesahan70 = (LinkButton)e.Row.FindControl("lbCetakPengesahan70");
                LinkButton lbUnggahTanggungJawabBelanja70 = new LinkButton();
                lbUnggahTanggungJawabBelanja70 = (LinkButton)e.Row.FindControl("lbUnggahTanggungJawabBelanja70");
                LinkButton lbUnduhTanggungJawabBelanja70 = new LinkButton();
                lbUnduhTanggungJawabBelanja70 = (LinkButton)e.Row.FindControl("lbUnduhTanggungJawabBelanja70");

                Label lblketsptb30 = new Label();
                lblketsptb30 = (Label)e.Row.FindControl("lblketsptb30");
                LinkButton lbCetakPengesahan30 = new LinkButton();
                lbCetakPengesahan30 = (LinkButton)e.Row.FindControl("lbCetakPengesahan30");
                LinkButton lbUnggahTanggungJawabBelanja30 = new LinkButton();
                lbUnggahTanggungJawabBelanja30 = (LinkButton)e.Row.FindControl("lbUnggahTanggungJawabBelanja30");
                LinkButton lbUnduhTanggungJawabBelanja30 = new LinkButton();
                lbUnduhTanggungJawabBelanja30 = (LinkButton)e.Row.FindControl("lbUnduhTanggungJawabBelanja30");


                Label lblketsptb100 = new Label();
                lblketsptb100 = (Label)e.Row.FindControl("lblketsptb100");
                LinkButton lbCetakPengesahan100 = new LinkButton();
                lbCetakPengesahan100 = (LinkButton)e.Row.FindControl("lbCetakPengesahan100");
                LinkButton lbUnggahTanggungJawabBelanja100 = new LinkButton();
                lbUnggahTanggungJawabBelanja100 = (LinkButton)e.Row.FindControl("lbUnggahTanggungJawabBelanja100");
                LinkButton lbUnduhTanggungJawabBelanja100 = new LinkButton();
                lbUnduhTanggungJawabBelanja100 = (LinkButton)e.Row.FindControl("lbUnduhTanggungJawabBelanja100");


                if (status_tunggal == "1")
                {
                    lblketsptb70.Visible = true;
                    lbCetakPengesahan70.Visible = true;
                    lbUnggahTanggungJawabBelanja70.Visible = true;
                    lbUnduhTanggungJawabBelanja70.Visible = true;

                    lblketsptb30.Visible = true;
                    lbCetakPengesahan30.Visible = true;
                    lbUnggahTanggungJawabBelanja30.Visible = true;
                    lbUnduhTanggungJawabBelanja30.Visible = true;

                    lblketsptb100.Visible = false;
                    lbCetakPengesahan100.Visible = false;
                    lbUnggahTanggungJawabBelanja100.Visible = false;
                    lbUnduhTanggungJawabBelanja100.Visible = false;

                    string kd_sts_unggah_1 = gvSPTB.DataKeys[e.Row.RowIndex]["kd_sts_unggah_1"].ToString();
                    if (kd_sts_unggah_1 == "")
                    {
                        lbUnggahTanggungJawabBelanja70.Enabled = false;
                        lbUnggahTanggungJawabBelanja70.CssClass = "fa fa-upload btn btn-default";
                        lbCetakPengesahan70.Enabled = true;
                        lbUnduhTanggungJawabBelanja70.Enabled = false;
                        lbUnduhTanggungJawabBelanja70.CssClass = "fa fa-file-pdf-o btn btn-default";

                        lbUnggahTanggungJawabBelanja30.Enabled = false;
                        lbUnggahTanggungJawabBelanja30.CssClass = "fa fa-upload btn btn-default";
                        lbCetakPengesahan30.Enabled = false;
                        lbCetakPengesahan30.CssClass = "fa fa-print btn btn-default";
                        lbUnduhTanggungJawabBelanja30.Enabled = false;
                        lbUnduhTanggungJawabBelanja30.CssClass = "fa fa-file-pdf-o btn btn-default";

                    }
                    else if (kd_sts_unggah_1 == "0")
                    {

                        lbUnggahTanggungJawabBelanja70.Enabled = true;
                        //lbUnggahTanggungJawabBelanja70.CssClass = "fa fa-upload btn btn-default";
                        lbCetakPengesahan70.Enabled = true;
                        lbUnduhTanggungJawabBelanja70.Enabled = false;
                        lbUnduhTanggungJawabBelanja70.CssClass = "fa fa-upload btn btn-default";

                        lbUnggahTanggungJawabBelanja30.Enabled = false;
                        lbUnggahTanggungJawabBelanja30.CssClass = "fa fa-upload btn btn-default";
                        lbCetakPengesahan30.Enabled = false;
                        lbCetakPengesahan30.CssClass = "fa fa-print btn btn-default";
                        lbUnduhTanggungJawabBelanja30.Enabled = false;
                        lbUnduhTanggungJawabBelanja30.CssClass = "fa fa-file-pdf-o btn btn-default";

                    }
                    else if (kd_sts_unggah_1 == "1")
                    {

                        lbUnggahTanggungJawabBelanja70.Enabled = true;
                        //lbUnggahTanggungJawabBelanja70.CssClass = "fa fa-upload btn btn-default";
                        lbCetakPengesahan70.Enabled = true;
                        lbUnduhTanggungJawabBelanja70.Enabled = true;
                        lbUnduhTanggungJawabBelanja70.CssClass = "fa fa-file-pdf-o btn btn-success";

                        string kd_sts_unggah_2 = gvSPTB.DataKeys[e.Row.RowIndex]["kd_sts_unggah_2"].ToString();

                        if (kd_sts_unggah_2 == "")
                        {
                            lbUnggahTanggungJawabBelanja30.Enabled = false;
                            lbUnggahTanggungJawabBelanja30.CssClass = "fa fa-upload btn btn-default";
                            lbCetakPengesahan30.Enabled = true;
                            //lbCetakPengesahan30.CssClass = "fa fa-upload btn btn-default";
                            lbUnduhTanggungJawabBelanja30.Enabled = false;
                            lbUnduhTanggungJawabBelanja30.CssClass = "fa fa-file-pdf-o btn btn-default";
                        }
                        else if (kd_sts_unggah_2 == "0")
                        {
                            lbUnggahTanggungJawabBelanja30.Enabled = true;
                            //lbUnggahTanggungJawabBelanja30.CssClass = "fa fa-upload btn btn-default";
                            lbCetakPengesahan30.Enabled = true;
                            //lbCetakPengesahan30.CssClass = "fa fa-upload btn btn-default";
                            lbUnduhTanggungJawabBelanja30.Enabled = false;
                            lbUnduhTanggungJawabBelanja30.CssClass = "fa fa-file-pdf-o btn btn-default";
                        }
                        else if (kd_sts_unggah_2 == "1")
                        {
                            lbUnggahTanggungJawabBelanja30.Enabled = true;
                            //lbUnggahTanggungJawabBelanja30.CssClass = "fa fa-upload btn btn-default";
                            lbCetakPengesahan30.Enabled = true;
                            //lbCetakPengesahan30.CssClass = "fa fa-upload btn btn-default";
                            lbUnduhTanggungJawabBelanja30.Enabled = true;
                            lbUnduhTanggungJawabBelanja30.CssClass = "fa fa-file-pdf-o btn btn-success";
                        }
                    }

                    //if (id_skema != "77")
                    //{

                    //    lblketsptb30.Visible = true;
                    //    lbCetakPengesahan30.Visible = true;
                    //    lbUnggahTanggungJawabBelanja30.Visible = true;
                    //    lbUnduhTanggungJawabBelanja30.Visible = true;
                    //    lbCetakPengesahan30.Enabled = false;
                    //    lbUnggahTanggungJawabBelanja30.Enabled = false;
                    //    lbUnduhTanggungJawabBelanja30.Enabled = false;
                    //}
                }
                else
                {
                    lblketsptb70.Visible = false;
                    lbCetakPengesahan70.Visible = false;
                    lbUnggahTanggungJawabBelanja70.Visible = false;
                    lbUnduhTanggungJawabBelanja70.Visible = false;

                    lblketsptb30.Visible = false;
                    lbCetakPengesahan30.Visible = false;
                    lbUnggahTanggungJawabBelanja30.Visible = false;
                    lbUnduhTanggungJawabBelanja30.Visible = false;

                    lblketsptb100.Visible = true;
                    lbCetakPengesahan100.Visible = true;
                    lbUnggahTanggungJawabBelanja100.Visible = true;
                    lbUnduhTanggungJawabBelanja100.Visible = true;

                    string kd_sts_unggah = gvSPTB.DataKeys[e.Row.RowIndex]["kd_sts_unggah"].ToString();

                    if (kd_sts_unggah == "")
                    {
                        lbUnggahTanggungJawabBelanja100.Enabled = false;//false ngetes
                        lbUnggahTanggungJawabBelanja100.CssClass = "fa fa-upload btn btn-default";
                        lbCetakPengesahan100.Enabled = true;
                        //lbCetakPengesahan100.CssClass = "fa fa-upload btn btn-default";
                        lbUnduhTanggungJawabBelanja100.Enabled = false;
                        lbUnduhTanggungJawabBelanja100.CssClass = "fa fa-file-pdf-o btn btn-default";
                    }
                    else if (kd_sts_unggah == "0")
                    {
                        lbUnggahTanggungJawabBelanja100.Enabled = true;
                        //lbUnggahTanggungJawabBelanja100.CssClass = "fa fa-upload btn btn-default";
                        lbCetakPengesahan100.Enabled = true;
                        //lbCetakPengesahan100.CssClass = "fa fa-upload btn btn-default";
                        lbUnduhTanggungJawabBelanja100.Enabled = false;
                        lbUnduhTanggungJawabBelanja100.CssClass = "fa fa-file-pdf-o btn btn-default";
                    }
                    else if (kd_sts_unggah == "1")
                    {
                        lbUnggahTanggungJawabBelanja100.Enabled = true;
                        //lbUnggahTanggungJawabBelanja100.CssClass = "fa fa-upload btn btn-default";
                        lbCetakPengesahan100.Enabled = true;
                        //lbCetakPengesahan100.CssClass = "fa fa-upload btn btn-default";
                        lbUnduhTanggungJawabBelanja100.Enabled = true;
                        lbUnduhTanggungJawabBelanja100.CssClass = "fa fa-file-pdf-o btn btn-success";
                    }
                    //if (id_skema != "77")
                    //{
                    //    lblketsptb100.Visible = true;

                    //    lbCetakPengesahan100.Visible = true;
                    //    lbUnggahTanggungJawabBelanja100.Visible = true;
                    //    lbUnduhTanggungJawabBelanja100.Visible = true;
                    //    lbCetakPengesahan100.Enabled = false;
                    //    lbUnggahTanggungJawabBelanja100.Enabled = false;
                    //    lbUnduhTanggungJawabBelanja100.Enabled = false;
                    //};
                }




            }

        }
        private void isiDataPengesahan(string idUsulanKegiatan)
        {
            DataTable dt = new DataTable();
            if (!objPelaporan.getPengesahanTanggungJawabBelanja(ref dt, Guid.Parse(idUsulanKegiatan), ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"].ToString())) return;

            if (dt.Rows.Count <= 0)
            {
                lblJudul.Text = "";
                lblNamaLengkapKetua.Text = "";
                lblAlamat.Text = "";

                tbNomorSuratKeputusan.Text = "";
                tbNomorKontrak.Text = "";
                tbAnggaran.Text = "0.00";

                lblJumlahTotal.Text = "0.00";

                taUraian01.Value = "";
                lblUraian1.Text = "";
                tbJumlah1.Text = "0.00";
                lblJumlah1.Text = "0.00";

                taUraian02.Value = "";
                lblUraian2.Text = "";
                tbJumlah2.Text = "0.00";
                lblJumlah2.Text = "0.00";

                taUraian03.Value = "";
                lblUraian3.Text = "";
                tbJumlah3.Text = "0.00";
                lblJumlah3.Text = "0.00";

                taUraian04.Value = "";
                lblUraian4.Text = "";
                tbJumlah4.Text = "0.00";
                lblJumlah4.Text = "0.00";

                //taUraian05.Value = dt.Rows[0]["uraian05"].ToString();
                //lblUraian5.Text = dt.Rows[0]["uraian05"].ToString();
                //tbJumlah5.Text = objManipData.convertFormatDana(dt.Rows[0]["jumlah05"].ToString());
                //lblJumlah5.Text = objManipData.convertFormatDana(dt.Rows[0]["jumlah05"].ToString());
                objLogin = (Models.login)Session["objLogin"];

                lblNamKetua.Text = objLogin.namaLengkap;
                tbNipKetua.Text = "";
                tbKotaNTgl.Text = "";

                string tgl = DateTime.Now.Day.ToString();
                string bulan = DateTime.Now.Month.ToString();
                string tahun = DateTime.Now.Year.ToString();
                lblKotaNTgl.Text = "" + ", " + tgl + " - " + bulan + " - " + tahun;
                tbKotaNTgl.Text = "" + ", " + tgl + " - " + bulan + " - " + tahun;

                lbNomorSuratKeputusan.Visible = false;
                lbNomorKontrak.Visible = false;
                lbAnggaran.Visible = false;

                tbNomorSuratKeputusan.Visible = true;
                tbNomorKontrak.Visible = true;
                tbAnggaran.Visible = true;

            }
            else
            {
                lblJudul.Text = dt.Rows[0]["judul"].ToString();
                lblNamaLengkapKetua.Text = dt.Rows[0]["nama"].ToString();
                lblAlamat.Text = dt.Rows[0]["alamat"].ToString();

                tbNomorSuratKeputusan.Text = dt.Rows[0]["nomor_surat_keputusan"].ToString();
                tbNomorKontrak.Text = dt.Rows[0]["nomor_kontrak"].ToString();
                tbAnggaran.Text = objManipData.convertFormatDana2(dt.Rows[0]["anggaran"].ToString());

                lblJumlahTotal.Text = objManipData.convertFormatDana2(dt.Rows[0]["total_dana"].ToString());

                taUraian01.Value = dt.Rows[0]["uraian01"].ToString();
                lblUraian1.Text = dt.Rows[0]["uraian01"].ToString();
                tbJumlah1.Text = objManipData.convertFormatDana2(dt.Rows[0]["jumlah01"].ToString());
                lblJumlah1.Text = objManipData.convertFormatDana2(dt.Rows[0]["jumlah01"].ToString());

                taUraian02.Value = dt.Rows[0]["uraian02"].ToString();
                lblUraian2.Text = dt.Rows[0]["uraian02"].ToString();
                tbJumlah2.Text = objManipData.convertFormatDana2(dt.Rows[0]["jumlah02"].ToString());
                lblJumlah2.Text = objManipData.convertFormatDana2(dt.Rows[0]["jumlah02"].ToString());

                taUraian03.Value = dt.Rows[0]["uraian03"].ToString();
                lblUraian3.Text = dt.Rows[0]["uraian03"].ToString();
                tbJumlah3.Text = objManipData.convertFormatDana2(dt.Rows[0]["jumlah03"].ToString());
                lblJumlah3.Text = objManipData.convertFormatDana2(dt.Rows[0]["jumlah03"].ToString());

                taUraian04.Value = dt.Rows[0]["uraian04"].ToString();
                lblUraian4.Text = dt.Rows[0]["uraian04"].ToString();
                tbJumlah4.Text = objManipData.convertFormatDana2(dt.Rows[0]["jumlah04"].ToString());
                lblJumlah4.Text = objManipData.convertFormatDana2(dt.Rows[0]["jumlah04"].ToString());

                //taUraian05.Value = dt.Rows[0]["uraian05"].ToString();
                //lblUraian5.Text = dt.Rows[0]["uraian05"].ToString();
                //tbJumlah5.Text = objManipData.convertFormatDana(dt.Rows[0]["jumlah05"].ToString());
                //lblJumlah5.Text = objManipData.convertFormatDana(dt.Rows[0]["jumlah05"].ToString());

                lblNamKetua.Text = dt.Rows[0]["nama"].ToString();
                tbNipKetua.Text = dt.Rows[0]["no_pegawai"].ToString();
                tbKotaNTgl.Text = dt.Rows[0]["nama_kota"].ToString();

                string tgl = DateTime.Now.Day.ToString();
                string bulan = DateTime.Now.Month.ToString();
                string tahun = DateTime.Now.Year.ToString();
                lblKotaNTgl.Text = dt.Rows[0]["nama_kota"].ToString() + ", " + tgl + " - " + bulan + " - " + tahun;
                tbKotaNTgl.Text = dt.Rows[0]["nama_kota"].ToString() + ", " + tgl + " - " + bulan + " - " + tahun;

                lbNomorSuratKeputusan.Visible = false;
                lbNomorKontrak.Visible = false;
                lbAnggaran.Visible = false;

                tbNomorSuratKeputusan.Visible = true;
                tbNomorKontrak.Visible = true;
                tbAnggaran.Visible = true;
            }
        }
        protected void gvSPTB_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];
            string cmd = e.CommandName;
            int idx = Convert.ToInt32(e.CommandArgument);

            ViewState["id_usulan_kegiatan"] = gvSPTB.DataKeys[idx]["id_usulan_kegiatan"].ToString();
            ViewState["kd_program_hibah"] = gvSPTB.DataKeys[idx]["kd_program_hibah"].ToString();
            ViewState["program_hibah"] = gvSPTB.DataKeys[idx]["program_hibah"].ToString();
            ViewState["thn_pelaksanaan_kegiatan"] = gvSPTB.DataKeys[idx]["thn_pelaksanaan_kegiatan"].ToString();
            string kd_sts_unggah = gvSPTB.DataKeys[idx]["kd_sts_unggah"].ToString();
            string kd_sts_unggah_1 = gvSPTB.DataKeys[idx]["kd_sts_unggah_1"].ToString();
            string kd_sts_unggah_2 = gvSPTB.DataKeys[idx]["kd_sts_unggah_2"].ToString();


            if (cmd == "unggahDokumen100")
            {
                ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"] = "3";
                lblSkemaUnggah.Text = gvSPTB.DataKeys[idx]["nama_skema"].ToString();
                lblJudulUnggah.Text = gvSPTB.DataKeys[idx]["judul"].ToString();
                uiMdl.ShowModal(this.Page, "modalUnggahDokumen");
            }
            else
            if (cmd == "unggahDokumen70")
            {
                ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"] = "1";
                lblSkemaUnggah.Text = gvSPTB.DataKeys[idx]["nama_skema"].ToString();
                lblJudulUnggah.Text = gvSPTB.DataKeys[idx]["judul"].ToString();
                uiMdl.ShowModal(this.Page, "modalUnggahDokumen");
            }
            else
            if (cmd == "unggahDokumen30")
            {
                ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"] = "2";
                lblSkemaUnggah.Text = gvSPTB.DataKeys[idx]["nama_skema"].ToString();
                lblJudulUnggah.Text = gvSPTB.DataKeys[idx]["judul"].ToString();
                uiMdl.ShowModal(this.Page, "modalUnggahDokumen");
            }
            else
            if (cmd == "cetakPengesahan100")
            {
                ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"] = "3";
                isiDataPengesahan(ViewState["id_usulan_kegiatan"].ToString());
                lblNamaLengkapKetua.Text = objLogin.namaLengkap.ToString();
                lblJudul.Text = gvSPTB.DataKeys[idx]["judul"].ToString();
                mvMain.SetActiveView(vPengesahan);

            }

            else if (cmd == "cetakPengesahan30")
            {
                ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"] = "2";
                isiDataPengesahan(ViewState["id_usulan_kegiatan"].ToString());
                lblNamaLengkapKetua.Text = objLogin.namaLengkap.ToString();
                lblJudul.Text = gvSPTB.DataKeys[idx]["judul"].ToString();
                mvMain.SetActiveView(vPengesahan);

            }

            if (cmd == "cetakPengesahan70")
            {
                ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"] = "1";
                isiDataPengesahan(ViewState["id_usulan_kegiatan"].ToString());
                //lblNamaLengkapKetua.Text = objLogin.namaLengkap.ToString();
                lblJudul.Text = gvSPTB.DataKeys[idx]["judul"].ToString();
                mvMain.SetActiveView(vPengesahan);

            }
            else if (cmd == "unduhDokumen30")
            {
                ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"] = "2";
                if (kd_sts_unggah_2 == "1")
                {
                    string NamaBerkas = "tanggung_jawab_belanja.pdf";
                    UnduhFile(ViewState["id_usulan_kegiatan"].ToString(), NamaBerkas);
                    return;
                }
                else
                    cmd = "unggahDokumen";
            }
            else if (cmd == "unduhDokumen70")
            {
                ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"] = "1";
                if (kd_sts_unggah_1 == "1")
                {
                    string NamaBerkas = "tanggung_jawab_belanja.pdf";
                    UnduhFile(ViewState["id_usulan_kegiatan"].ToString(), NamaBerkas);
                    return;
                }
                else
                    cmd = "unggahDokumen";
            }
            else if (cmd == "unduhDokumen100")
            {
                ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"] = "3";
                if (kd_sts_unggah == "1")
                {
                    string NamaBerkas = "tanggung_jawab_belanja.pdf";
                    UnduhFile(ViewState["id_usulan_kegiatan"].ToString(), NamaBerkas);
                    return;
                }
                else
                    cmd = "unggahDokumen";
            }
        }

        protected void btKembali_Click(object sender, EventArgs e)
        {
            isigvSPTB();
            mvMain.SetActiveView(vDaftarUsulanLuaran);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            isiLabel();
            cetakPdf();
        }

        void cetakPdf()
        {
            var pdfDoc = new Document();
            pdfDoc.SetPageSize(PageSize.A4);
            pdfDoc.SetMargins(80f, 50f, 110f, 0f);
            

            MemoryStream mem = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, mem);
            for (int i = 0; i < 1; i++)
            {
                pdfDoc.NewPage();
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                //isiIsian();

                panelPengesahan.RenderControl(hw);

                StringReader sr = new StringReader(sw.ToString());
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();

                //cetakFooter(writer);
                PdfContentByte cb = writer.DirectContent;
                cb.MoveTo(70f, 923f);
                cb.LineTo(550f, 923f); // garis
                cb.MoveTo(70f, 921f);
                cb.LineTo(550f, 921f); // garis
                cb.Stroke();

                htmlparser.Parse(sr);
            }
            pdfDoc.Close();
            byte[] docData = mem.GetBuffer();
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"Lembar_tanggung_jawab_belanja"
                + lblNamaLengkapKetua.Text.Replace(" ", "_")
                + ".pdf\"");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(docData);
            Response.End();
        }
        private void isiLabel()
        {


            string nomorSuratKeputusan = tbNomorSuratKeputusan.Text;//  ViewState["nomorSuratKeputusan"].ToString();
            string nomorKontrak = tbNomorKontrak.Text;// ViewState["nomorKontrak"].ToString();
            string anggaran = tbAnggaran.Text.Replace(",", "");//  ViewState["anggaran"].ToString();
            string jumlah1 = tbJumlah1.Text;// ViewState["jumlah1"].ToString();
            string jumlah2 = tbJumlah2.Text; //ViewState["jumlah2"].ToString();
            string jumlah3 = tbJumlah3.Text; //ViewState["jumlah3"].ToString();
            string jumlah4 = tbJumlah4.Text; //ViewState["jumlah4"].ToString();
            string jumlah5 = "0.00"; //ViewState["jumlah5"].ToString();

            lbNomorSuratKeputusan.Text = nomorSuratKeputusan;
            lbNomorKontrak.Text = nomorKontrak;
            lbAnggaran.Text = objManipData.convertFormatDana2(anggaran);

            lbNomorSuratKeputusan.Visible = true;
            lbNomorKontrak.Visible = true;
            lbAnggaran.Visible = true;

            tbNomorSuratKeputusan.Visible = false;
            tbNomorKontrak.Visible = false;
            tbAnggaran.Visible = false;


            lblJudul.Visible = true;
            lblNamaLengkapKetua.Visible = true;
            lblAlamat.Visible = true;

            lblJumlahTotal.Visible = true;

            lblUraian1.Text = taUraian01.Value.ToString();
            taUraian01.Visible = false;
            lblUraian1.Visible = true;
            lblJumlah1.Text = jumlah1;// objManipData.convertFormatDana2(jumlah1);
            lblJumlah1.Visible = true;
            tbJumlah1.Visible = false;

            lblUraian2.Text = taUraian02.Value.ToString();
            lblUraian2.Visible = true;
            taUraian02.Visible = false;
            lblJumlah2.Text = jumlah2;// objManipData.convertFormatDana2(jumlah2);
            lblJumlah2.Visible = true;
            tbJumlah2.Visible = false;

            lblUraian3.Text = taUraian03.Value.ToString();
            lblUraian3.Visible = true;
            taUraian03.Visible = false;
            lblJumlah3.Text = jumlah3;// objManipData.convertFormatDana2(jumlah3);
            lblJumlah3.Visible = true;
            tbJumlah3.Visible = false;

            lblUraian4.Text = taUraian04.Value.ToString();
            lblUraian4.Visible = true;
            taUraian04.Visible = false;
            lblJumlah4.Text = jumlah4;// objManipData.convertFormatDana2(jumlah4);
            lblJumlah4.Visible = true;
            tbJumlah4.Visible = false;

            //lblUraian5.Text = taUraian05.Value.ToString();
            //lblUraian5.Visible = true;
            //taUraian05.Visible = false;
            //lblJumlah5.Text = objManipData.convertFormatDana(jumlah5);
            //lblJumlah5.Visible = true;
            //tbJumlah5.Visible = false;

            lblKotaNTgl.Text = tbKotaNTgl.Text;
            lblKotaNTgl.Visible = true;
            tbKotaNTgl.Visible = false;

            lblNipKetua.Text = tbNipKetua.Text;
            lblNipKetua.Visible = true;
            tbNipKetua.Visible = false;

            jumlah1 = tbJumlah1.Text.Replace(",", "");// ViewState["jumlah1"].ToString();
            jumlah2 = tbJumlah2.Text.Replace(",", ""); //ViewState["jumlah2"].ToString();
            jumlah3 = tbJumlah3.Text.Replace(",", ""); //ViewState["jumlah3"].ToString();
            jumlah4 = tbJumlah4.Text.Replace(",", ""); //ViewState["jumlah4"].ToString();
            jumlah5 = "0.00"; //ViewState["jumlah5"].ToString();



            lblJumlahTotal.Text = objManipData.convertFormatDana((double.Parse(jumlah1.Replace(".", ",")) + double.Parse(jumlah2.Replace(".", ",")) + double.Parse(jumlah3.Replace(".", ",")) + double.Parse(jumlah4.Replace(".", ",")) + double.Parse(jumlah5.Replace(".", ","))).ToString());
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            Guid id_usulan_kegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            string kd_program_hibah = ViewState["kd_program_hibah"].ToString();

            if (ddlThnPelaksanaan.SelectedValue == "0")
            {
                //noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "...................");
            }
            else if (fileUpload1.HasFile)
            {
                if (fileUpload1.FileName.EndsWith(".pdf") || fileUpload1.FileName.EndsWith(".PDF"))
                {
                    if (kd_program_hibah == "4")
                    {
                        if (fileUpload1.PostedFile.ContentLength < (5 * 1024 * 1024))
                        {
                            prosesUnggah(id_usulan_kegiatan);
                        }
                        else
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File yang akan diunggah ukurannya tidak boleh melebihi 20 MByte !!!");
                        }
                    }
                    else
                    {
                        if (fileUpload1.PostedFile.ContentLength < (5 * 1024 * 1024))
                        {
                            prosesUnggah(id_usulan_kegiatan);
                        }
                        else
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "File yang akan diunggah ukurannya tidak boleh melebihi 5 MByte !!!");
                        }
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi kesalahan", "Silahkan upload File bertipe PDF !!!");
                }
            }
        }

        private string removeUnicode(string inputString)
        {
            string asAscii = Encoding.ASCII.GetString(
                Encoding.Convert(
                    Encoding.UTF8,
                    Encoding.GetEncoding(
                        Encoding.ASCII.EncodingName,
                        new EncoderReplacementFallback(string.Empty),
                        new DecoderExceptionFallback()
                        ),
                    Encoding.UTF8.GetBytes(inputString)
                )
            );
            return asAscii;
        }


        protected void lbModalStsKonfirmasi_Click(object sender, EventArgs e)
        {
            simpanisian();
        }

        protected void UnduhFile(string id_usulan_kegiatan, string NamaBerkas)
        {
            //string filePath = Server.MapPath(string.Format("~/fileUpload/tanggung_jawab_belanja/{0}/{1}.pdf",
            //    ViewState["thn_pelaksanaan_kegiatan"].ToString(), id_usulan_kegiatan));
            string kdberkas = "";
            string kd_jenis_dokumen_penggunaan_anggaran_usulan = ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"].ToString();
            if (kd_jenis_dokumen_penggunaan_anggaran_usulan == "3")
            { kdberkas = "100"; }
            else if (kd_jenis_dokumen_penggunaan_anggaran_usulan == "2")
            { kdberkas = "30"; }
            else if (kd_jenis_dokumen_penggunaan_anggaran_usulan == "1")
            { kdberkas = "70"; };

            string filePath = Server.MapPath(string.Format(@"~/fileUpload/tanggung_jawab_belanja/{0}/{1}/{2}.pdf",
                ViewState["thn_pelaksanaan_kegiatan"].ToString(), kdberkas, id_usulan_kegiatan));
            //string filePath1 = Server.MapPath(string.Format(@"~/fileUpload/tanggung_jawab_belanja/{0}/{1}/{2}.pdf",
            //    ViewState["thn_pelaksanaan_kegiatan"].ToString(), id_usulan_kegiatan));
            //string filePath2 = Server.MapPath(string.Format(@"~/fileUpload/tanggung_jawab_belanja/{0}/{1}/{2}.pdf",
            //    ViewState["thn_pelaksanaan_kegiatan"].ToString(), id_usulan_kegiatan));
            //string filePath3 = Server.MapPath(string.Format(@"~/fileUpload/tanggung_jawab_belanja/{0}/{1}/{2}.pdf",
            //    ViewState["thn_pelaksanaan_kegiatan"].ToString(), id_usulan_kegiatan));


            if (File.Exists(filePath))
            {
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + NamaBerkas);
                Response.TransmitFile(filePath);
                Response.End();
            }
            //else
            //if (File.Exists(filePath1))
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + NamaBerkas);
            //    Response.TransmitFile(filePath1);
            //    Response.End();
            //}
            //else
            //if (File.Exists(filePath2))
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + NamaBerkas);
            //    Response.TransmitFile(filePath2);
            //    Response.End();
            //}
            //else
            //if (File.Exists(filePath3))
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + NamaBerkas);
            //    Response.TransmitFile(filePath3);
            //    Response.End();
            //}
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Berkas tidak dapat ditemukan di Server...");
            }
        }

        private void clearData()
        {
            lblSkemaUnggah.Text = string.Empty;
            lblJudulUnggah.Text = string.Empty;
            ViewState["id_usulan_kegiatan"] = string.Empty;
            ViewState["program_hibah"] = string.Empty;
            ViewState["thn_pelaksanaan_kegiatan"] = string.Empty;
        }

        protected void updateStatusPelaksanaan(Guid id_usulan_kegiatan)
        {
            if (objPelaporan.updateStsPelaksanaan(id_usulan_kegiatan, ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Unggah tanggung jawab belanja penelitian berhasil");
                isigvSPTB();
                clearData();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Unggah tanggung jawab belanja penelitian gagal" + objPelaporan.errorMessage);
                clearData();
            }
        }

        private void prosesUnggah(Guid id_usulan_kegiatan)
        {
            string program_hibah = ViewState["program_hibah"].ToString();
            string thn_pelaksanaan_kegiatan = ViewState["thn_pelaksanaan_kegiatan"].ToString();
            string kd_jenis_dokumen_penggunaan_anggaran_usulan = ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"].ToString();
            string path = "";
            if (kd_jenis_dokumen_penggunaan_anggaran_usulan == "3")
            {
                path = string.Format("~/fileUpload/tanggung_jawab_belanja/{0}/100/", thn_pelaksanaan_kegiatan);
            }
            else if (kd_jenis_dokumen_penggunaan_anggaran_usulan == "2")
            {
                path = string.Format("~/fileUpload/tanggung_jawab_belanja/{0}/30/", thn_pelaksanaan_kegiatan);
            }
            else if (kd_jenis_dokumen_penggunaan_anggaran_usulan == "1")
            {
                path = string.Format("~/fileUpload/tanggung_jawab_belanja/{0}/70/", thn_pelaksanaan_kegiatan);
            }

            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

            string namaFile = string.Format("{0}{1}.pdf", path.ToString(), id_usulan_kegiatan.ToString());

            try
            {
                fileUpload1.SaveAs(Server.MapPath(namaFile));
                updateStatusPelaksanaan(id_usulan_kegiatan);
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi", "Unggah tanggung jawab belanja penelitian berhasil.");
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi kesalahan", ex.Message);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (taUraian01.Value.ToString().Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Uraian 01 belum diisi ");
                return;
            };

            if (taUraian02.Value.ToString().Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Uraian 02 belum diisi ");
                return;
            };
            if (taUraian03.Value.ToString().Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Uraian 03 belum diisi ");
                return;
            };
            if (taUraian04.Value.ToString().Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Uraian 04 belum diisi ");
                return;
            };



            if (tbNomorSuratKeputusan.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Normal dan Tanggal Surat Keputusan Penetapan Pelaksanaan Penelitian harus diisi");
                return;
            };
            if (tbNomorKontrak.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Nomor dan Tanggal Kontrak harus diisi");
                return;
            };

            if (tbAnggaran.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Anggaran yang diterima harus diisi");
                return;
            };

            if (tbJumlah1.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Masukan nilai biaya yang dikeluarkan");
                return;
            };
            if (tbJumlah2.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Masukan nilai biaya yang dikeluarkan");
                return;
            };
            if (tbJumlah3.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Masukan nilai biaya yang dikeluarkan");
                return;
            };
            if (tbJumlah4.Text.Trim().Length == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                             "Masukan nilai biaya yang dikeluarkan");
                return;
            };
            //if (tbJumlah5.Text.Trim().Length == 0)
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //                 "Masukan nilai biaya yang dikeluarkan");
            //    return ;
            //};

            lbldanaanggaran.Text = tbAnggaran.Text;
            string jumlah1 = tbJumlah1.Text.Replace(",", "");// ViewState["jumlah1"].ToString();
            string jumlah2 = tbJumlah2.Text.Replace(",", ""); //ViewState["jumlah2"].ToString();
            string jumlah3 = tbJumlah3.Text.Replace(",", ""); //ViewState["jumlah3"].ToString();
            string jumlah4 = tbJumlah4.Text.Replace(",", ""); //ViewState["jumlah4"].ToString();
            string jumlah5 = "0.00"; //ViewState["jumlah5"].ToString();

            lbpemakaian.Text = objManipData.convertFormatDana2((double.Parse(jumlah1.Replace(".", ",")) + double.Parse(jumlah2.Replace(".", ",")) + double.Parse(jumlah3.Replace(".", ",")) + double.Parse(jumlah4.Replace(".", ",")) + double.Parse(jumlah5.Replace(".", ","))).ToString());
            string kdberkas = "";
            string kd_jenis_dokumen_penggunaan_anggaran_usulan = ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"].ToString();
            if (kd_jenis_dokumen_penggunaan_anggaran_usulan == "3")
            { kdberkas = "100"; }
            else if (kd_jenis_dokumen_penggunaan_anggaran_usulan == "2")
            { kdberkas = "30"; }
            else if (kd_jenis_dokumen_penggunaan_anggaran_usulan == "1")
            { kdberkas = "70"; };
            lbpersen.Text = kdberkas;

            uiMdl.ShowModal(this.Page, "modalKonfirmasi");

        }

        private void simpanisian()
        {

            string nomorSuratKeputusan = tbNomorSuratKeputusan.Text.Trim();
            string nomorKontrak = tbNomorKontrak.Text.Trim();
            string anggaran = tbAnggaran.Text.Trim();
            string jumlah1 = tbJumlah1.Text.Trim();
            string jumlah2 = tbJumlah2.Text.Trim();
            string jumlah3 = tbJumlah3.Text.Trim();
            string jumlah4 = tbJumlah4.Text.Trim();
            //string jumlah5 = tbJumlah5.Text.Trim();

            nomorSuratKeputusan = removeUnicode(nomorSuratKeputusan);
            nomorKontrak = removeUnicode(nomorKontrak);
            anggaran = removeUnicode(anggaran);
            jumlah1 = removeUnicode(jumlah1);
            jumlah2 = removeUnicode(jumlah2);
            jumlah3 = removeUnicode(jumlah3);
            jumlah4 = removeUnicode(jumlah4);
            //jumlah5 = removeUnicode(jumlah5);


            //jumlah5 = "0.00"; //ViewState["jumlah5"].ToString();

            anggaran = Regex.Replace(anggaran, "[^a-zA-Z0-9]", "");
            jumlah1 = Regex.Replace(jumlah1, "[^a-zA-Z0-9]", "");
            jumlah2 = Regex.Replace(jumlah2, "[^a-zA-Z0-9]", "");
            jumlah3 = Regex.Replace(jumlah3, "[^a-zA-Z0-9]", "");
            jumlah4 = Regex.Replace(jumlah4, "[^a-zA-Z0-9]", "");
            //jumlah5 = Regex.Replace(jumlah5, "[^a-zA-Z0-9]", "");

            anggaran = anggaran.Replace(",", "").Replace(".", "").Replace(" ", "").Replace("Rp", "").Replace("Rp.", "");
            jumlah1 = jumlah1.Replace(",", "").Replace(".", "").Replace(" ", "").Replace("Rp", "").Replace("Rp.", "");
            jumlah2 = jumlah2.Replace(",", "").Replace(".", "").Replace(" ", "").Replace("Rp", "").Replace("Rp.", "");
            jumlah3 = jumlah3.Replace(",", "").Replace(".", "").Replace(" ", "").Replace("Rp", "").Replace("Rp.", "");
            jumlah4 = jumlah4.Replace(",", "").Replace(".", "").Replace(" ", "").Replace("Rp", "").Replace("Rp.", "");
            //jumlah5 = jumlah5.Replace(",", "").Replace(".", "").Replace(" ", "").Replace("Rp", "").Replace("Rp.", "");

            anggaran = tbAnggaran.Text.Replace(",", "");
            jumlah1 = tbJumlah1.Text.Replace(",", "");// ViewState["jumlah1"].ToString();
            jumlah2 = tbJumlah2.Text.Replace(",", ""); //ViewState["jumlah2"].ToString();
            jumlah3 = tbJumlah3.Text.Replace(",", ""); //ViewState["jumlah3"].ToString();
            jumlah4 = tbJumlah4.Text.Replace(",", ""); //ViewState["jumlah4"].ToString();

            if (!objPelaporan.insupdatauraian(
                Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()),
                nomorSuratKeputusan, nomorKontrak, anggaran,
                taUraian01.Value.ToString(), jumlah1,
                taUraian02.Value.ToString(), jumlah2,
                taUraian03.Value.ToString(), jumlah3,
                taUraian04.Value.ToString(), jumlah4,
                "", "0", ViewState["kd_jenis_dokumen_penggunaan_anggaran_usulan"].ToString()))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objPelaporan.errorMessage);
                return;
            }
            noty.Notify(this.Page, uiNotify.NotifyType.info, "Simpan data",
               "Simpan data Berhasil");
            return;

            ViewState["nomorSuratKeputusan"] = nomorSuratKeputusan;
            ViewState["nomorKontrak"] = nomorKontrak;
            ViewState["anggaran"] = anggaran;
            ViewState["jumlah1"] = jumlah1;
            ViewState["jumlah2"] = jumlah2;
            ViewState["jumlah3"] = jumlah3;
            ViewState["jumlah4"] = jumlah4;
            //ViewState["jumlah5"] = jumlah5;
        }


    }
}