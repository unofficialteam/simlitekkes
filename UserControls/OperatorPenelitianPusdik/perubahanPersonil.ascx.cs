using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using System.Data;
using System.IO;
using OfficeOpenXml;
using simlitekkes.Core;
using simlitekkes.Helper;

namespace simlitekkes.UserControls.OperatorLLDikti
{
    public partial class perubahanPersonil : System.Web.UI.UserControl
    {
        Models.OperatorPenelitianPusdik.perubahanPersonil modelPerubahanPersonil = new Models.OperatorPenelitianPusdik.perubahanPersonil();

        UIControllers.uiGridView obj_uiGridView = new UIControllers.uiGridView();
        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        uiModal objModal = new uiModal();
        Core.manipulasiData objManipData = new Core.manipulasiData();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["objLogin"] == null)
            //    Response.Redirect("login.aspx");
            //else
            //    objLogin = (Models.login)Session["objLogin"];
            objLogin = new Models.login();

            objLogin.autentifikasi("4284036", "123456");

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvUsulanDidanai);
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvDaftarPersonil);

            if (!IsPostBack)
            {
                isiDdlSkema();
                isiDdlThnPelaksanaan();
                isiGvUsulanDidanai(0);
                mvMain.SetActiveView(vUsulanDidanai);
            }
        }

        #region Daftar Usulan Didanai

        private void isiGvUsulanDidanai(int idxPage)
        {
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            int idSkema = int.Parse(ddlSkema.SelectedValue.ToString());
            string judul = tbCariJudul.Text;

            modelPerubahanPersonil.currentPage = idxPage;
            modelPerubahanPersonil.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!modelPerubahanPersonil.getJmlUsulanDidanai(thnPelaksanaanKegiatan, objLogin.idInstitusi.ToString(), idSkema, judul))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPerubahanPersonil.errorMessage);

            //NEW PAGING CONTROL

            pagingUsulanDidanai.currentPage = idxPage;
            pagingUsulanDidanai.setPaging(int.Parse(ddlJmlBaris.SelectedValue), modelPerubahanPersonil.numOfRecords);

            if (!modelPerubahanPersonil.getDaftarUsulanDidanai(thnPelaksanaanKegiatan, objLogin.idInstitusi.ToString(), idSkema, judul))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPerubahanPersonil.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvUsulanDidanai, modelPerubahanPersonil.currentRecords))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPerubahanPersonil.errorMessage);

            if (modelPerubahanPersonil.numOfRecords < 1)
            {
                pagingUsulanDidanai.setPaging(int.Parse(ddlJmlBaris.SelectedValue), 1);
            }
        }

        private void isiDdlSkema()
        {
            DataTable dt = new DataTable();
            ddlSkema.AppendDataBoundItems = true;
            ddlSkema.DataTextField = "nama_skema";
            ddlSkema.DataValueField = "id_skema";
            if (modelPerubahanPersonil.listSkemaKegiatan(ref dt))
            {
                ddlSkema.Items.Clear();
                ddlSkema.Items.Add(new ListItem { Text = "-- Pilih Skema Kegiatan --", Value = "-1", Selected = true });
                ddlSkema.DataSource = dt;
                ddlSkema.DataBind();
            }
        }

        private void isiDdlThnPelaksanaan()
        {
            ddlThnPelaksanaan.Items.Clear();
            ddlThnPelaksanaan.Items.Add(new ListItem("-- Pilih --", "0000"));
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = (thnSKg + 1); i >= 2021; i--)
            {
                ddlThnPelaksanaan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        protected void ddlSkema_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvUsulanDidanai(0);
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvUsulanDidanai(0);
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvUsulanDidanai(0);
        }

        protected void lbCariJudul_Click(object sender, EventArgs e)
        {
            isiGvUsulanDidanai(0);
        }

        protected void lbExportExcel_Click(object sender, EventArgs e)
        {
            if (ddlThnPelaksanaan.SelectedValue == "0000")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi", "Tahun pelaksanaan belum dipilih");
            }
            else
            {
                string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
                int idSkema = int.Parse(ddlSkema.SelectedValue.ToString());

                var dt = new DataTable();

                using (ExcelPackage pck = new ExcelPackage())
                {
                    var fileName = string.Format("Pergantian personil" + " skema {0}" + " tahun {1}.xlsx",
                        ddlSkema.SelectedItem.ToString(), ddlThnPelaksanaan.SelectedItem.Text);
                    if (modelPerubahanPersonil.daftarUsulanDidanaiExcel(ref dt, thnPelaksanaanKegiatan, objLogin.idInstitusi.ToString(), idSkema))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("data");
                        ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);
                        //Autofit Column
                        for (int i = 1; i <= dt.Columns.Count; i++)
                        {
                            ws.Column(i).AutoFit(10);
                        }
                    }
                    else
                    {
                        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", modelPerubahanPersonil.errorMessage);
                        return;
                    }

                    HttpResponse httpResponse = Response;
                    httpResponse.Clear();
                    httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        pck.SaveAs(memoryStream);
                        memoryStream.WriteTo(httpResponse.OutputStream);
                        memoryStream.Close();
                    }

                    httpResponse.End();
                    pck.Dispose();
                }
            }
        }

        protected void gvUsulanDidanai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNomor = (Label)e.Row.FindControl("lblNoUsulanDidanai");
                lblNomor.Text = (e.Row.RowIndex + 1 + int.Parse(ddlJmlBaris.SelectedValue) * (pagingUsulanDidanai.currentPage)).ToString();
            }
        }

        protected void gvUsulanDidanai_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblJudulPerubahan.Text = gvUsulanDidanai.DataKeys[e.RowIndex]["judul"].ToString();
            lblProgramPerubahan.Text = ddlProgram.SelectedItem.ToString();
            lblSKemaPerubahan.Text = ddlSkema.SelectedItem.ToString();
            lblTahunKePerubahan.Text = gvUsulanDidanai.DataKeys[e.RowIndex]["urutan_thn_usulan_kegiatan"].ToString();
            lblDurasiPerubahan.Text = gvUsulanDidanai.DataKeys[e.RowIndex]["lama_kegiatan"].ToString();
            lblThnPelaksanaanPerubahan.Text = ddlThnPelaksanaan.SelectedItem.ToString();
            lblDanaDisetujuiPerubahan.Text = decimal.Parse(gvUsulanDidanai.DataKeys[e.RowIndex]["dana_disetujui"].ToString()).ToString("N0");
            tbNoSurat.Text = gvUsulanDidanai.DataKeys[e.RowIndex]["nomor_surat"].ToString();
            tbTglSurat.Text = gvUsulanDidanai.DataKeys[e.RowIndex]["tgl_surat"].ToString() != "" ?
                DateTime.Parse(gvUsulanDidanai.DataKeys[e.RowIndex]["tgl_surat"].ToString()).ToString("yyyy-MM-dd") : "";
            tbCatatan.Text = gvUsulanDidanai.DataKeys[e.RowIndex]["catatan"].ToString();

            string idUsulanKegiatan = gvUsulanDidanai.DataKeys[e.RowIndex]["id_usulan_kegiatan"].ToString();
            ViewState["idUsulanKegiatan"] = idUsulanKegiatan;
            string idUsulan = gvUsulanDidanai.DataKeys[e.RowIndex]["id_usulan"].ToString();
            ViewState["idUsulan"] = idUsulan;
            ViewState["dataBaru"] = true;

            bSimpan.Visible = true;
            lbSimpan.Visible = false;

            isiGvDaftarPersonil();
            mvMain.SetActiveView(vPerubahanPersonil);
        }

        protected void pagingUsulanDidanai_PageChanging(object sender, EventArgs e)
        {
            string thnPelaksanaanKegiatan = ddlThnPelaksanaan.SelectedValue;
            int idSkema = int.Parse(ddlSkema.SelectedValue.ToString());
            string judul = tbCariJudul.Text;

            modelPerubahanPersonil.currentPage = pagingUsulanDidanai.currentPage;
            modelPerubahanPersonil.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!modelPerubahanPersonil.getDaftarUsulanDidanai(thnPelaksanaanKegiatan, objLogin.idInstitusi.ToString(), idSkema, judul))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvUsulanDidanai, modelPerubahanPersonil.currentRecords);
        }

        protected void gvUsulanDidanai_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "perubahanJudul")
            {
                int idx = Convert.ToInt32(e.CommandArgument.ToString());

                mvMain.SetActiveView(vPerubahanJudul);
                lblProgramPerubahanJudul.Text = ddlProgram.SelectedItem.ToString();
                lblSkemaPerubahanJudul.Text = ddlSkema.SelectedItem.ToString();
                Label5.Text = gvUsulanDidanai.DataKeys[idx]["urutan_thn_usulan_kegiatan"].ToString();
                Label6.Text = gvUsulanDidanai.DataKeys[idx]["lama_kegiatan"].ToString();
                Label7.Text = ddlThnPelaksanaan.SelectedItem.ToString();
                Label8.Text = decimal.Parse(gvUsulanDidanai.DataKeys[idx]["dana_disetujui"].ToString()).ToString("N0");

                lblJudulLama.Text = gvUsulanDidanai.DataKeys[idx]["judul"].ToString();
                tbJudulBaru.Text = gvUsulanDidanai.DataKeys[idx]["judul_baru"].ToString();
                tbCatatanPerubahanJudul.Text = gvUsulanDidanai.DataKeys[idx]["catatan_perubahan_judul"].ToString();

                ViewState["idUsulan"] = gvUsulanDidanai.DataKeys[idx]["id_usulan"].ToString();
                ViewState["idUsulanKegiatan"] = gvUsulanDidanai.DataKeys[idx]["id_usulan_kegiatan"].ToString();
            }
        }

        #endregion


        #region Proses Pergantian Personil

        private void isiGvDaftarPersonil()
        {
            DataTable dt = new DataTable();
            if (modelPerubahanPersonil.daftarPersonil(ref dt, ViewState["idUsulanKegiatan"].ToString()))
            {
                gvDaftarPersonil.DataSource = dt;
                gvDaftarPersonil.DataBind();

                lblJmlPersonil.Text = dt.Rows.Count.ToString();
            }
        }

        protected void gvDaftarPersonil_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblNamaPeranPerubahan.Text = gvDaftarPersonil.DataKeys[e.RowIndex]["peran"].ToString();
            lblNidnNidkLama.Text = gvDaftarPersonil.DataKeys[e.RowIndex]["nidn"].ToString();
            lblNamaLama.Text = gvDaftarPersonil.DataKeys[e.RowIndex]["nama"].ToString();
            lblInstitusiLama.Text = gvDaftarPersonil.DataKeys[e.RowIndex]["nama_institusi"].ToString();

            string kdPeranPersonil = gvDaftarPersonil.DataKeys[e.RowIndex]["kd_peran_personil"].ToString();
            ViewState["kdPeranPersonil"] = kdPeranPersonil;
            string idPersonil = gvDaftarPersonil.DataKeys[e.RowIndex]["id_personil"].ToString();
            ViewState["idPersonil"] = idPersonil;

            ViewState["dataBaru"] = false;
            pnlPeranPersonil.Visible = false;

        }

        protected void lbKembaliPerubahan_Click(object sender, EventArgs e)
        {
            clearData();
            isiGvUsulanDidanai(0);
            mvMain.SetActiveView(vUsulanDidanai);
        }

        private void clearData()
        {
            //tbNoSurat.Text = string.Empty;
            //tbTglSurat.Text = string.Empty;
            //tbCatatan.Text = string.Empty;

            lblNamaPeranPerubahan.Text = string.Empty;
            lblNidnNidkLama.Text = string.Empty;
            lblNamaLama.Text = string.Empty;
            lblInstitusiLama.Text = string.Empty;

            tbNidnPengganti.Text = string.Empty;
            lblNamaBaru.Text = string.Empty;
            lblInstitusiBaru.Text = string.Empty;

            //ViewState["idUsulanKegiatan"] = string.Empty;
            //ViewState["idUsulan"] = string.Empty;

            pnlPeranPersonil.Visible = false;

        }

        protected void lbCariNidnPengganti_Click(object sender, EventArgs e)
        {
            if (bool.Parse(ViewState["dataBaru"].ToString()))
            {
                var dtBaru = new DataTable();
                if (modelPerubahanPersonil.getdataDosen(ref dtBaru, tbNidnPengganti.Text.Trim()))
                {
                    if (dtBaru.Rows.Count > 0)
                    {
                        if (ddlPeranPersonil.SelectedValue == "B")
                        {
                            // CekNIDN eligibilitas sebagai anggota
                            DataTable dtStatusEliAnggota = new DataTable();
                            dtStatusEliAnggota = modelPerubahanPersonil.getStsEligibleSbgAnggota(tbNidnPengganti.Text.Trim(),
                                ViewState["idUsulanKegiatan"].ToString());

                            if (dtStatusEliAnggota.Rows[0]["kd_sts_eleigible"].ToString() == "1")
                            {
                                lblNamaBaru.Text = dtBaru.Rows[0]["nama"].ToString();
                                lblInstitusiBaru.Text = dtBaru.Rows[0]["nama_institusi"].ToString();
                                ViewState["idPersonalBaru"] = dtBaru.Rows[0]["id_personal"];
                                ViewState["idInstitusi"] = dtBaru.Rows[0]["id_institusi"];

                                lbSimpan.Visible = true;
                                bSimpan.Visible = false;
                            }
                            else
                            {
                                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Terjadi Kesalahan",
                                "Maaf, " + dtStatusEliAnggota.Rows[0]["keterangan"].ToString());
                                return;
                            }
                        }
                        else if (ddlPeranPersonil.SelectedValue == "A")
                        {
                            // CekNIDN eligibilitas sebagai ketua
                            DataTable dtStatusEliKetua = new DataTable();
                            dtStatusEliKetua = modelPerubahanPersonil.getStsEligibleSbgKetua(ViewState["idUsulanKegiatan"].ToString());

                            if (dtStatusEliKetua.Rows[0]["is_terpenuhi_calon_keanggotaan_setara_ketua"].ToString() == "1")
                            {
                                lblNamaBaru.Text = dtBaru.Rows[0]["nama"].ToString();
                                lblInstitusiBaru.Text = dtBaru.Rows[0]["nama_institusi"].ToString();
                                ViewState["idPersonalBaru"] = dtBaru.Rows[0]["id_personal"];
                                ViewState["idInstitusi"] = dtBaru.Rows[0]["id_institusi"];

                                lbSimpan.Visible = true;
                                bSimpan.Visible = false;
                            }
                            else
                            {
                                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Pengganti tidak eligible");
                                return;
                            }
                        }
                        else
                        {
                            lbSimpan.Visible = true;
                            bSimpan.Visible = false;
                        }
                    }
                    else
                    {
                        bSimpan.Visible = true;
                        lbSimpan.Visible = false;
                        noty.Notify(this.Page, uiNotify.NotifyType.warning, "Terjadi Kesalahan", "NIDN tidak ditemukan");
                    }
                }
            }
            else
            {
                if (lblNamaPeranPerubahan.Text.Length > 0)
                {
                    var dt1 = new DataTable();
                    if (modelPerubahanPersonil.getdataDosen(ref dt1, tbNidnPengganti.Text.Trim()))
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            if (ViewState["kdPeranPersonil"].ToString() == "B")
                            {
                                // CekNIDN eligibilitas sebagai anggota
                                DataTable dtStatusEliAnggota = new DataTable();
                                dtStatusEliAnggota = modelPerubahanPersonil.getStsEligibleSbgAnggota(tbNidnPengganti.Text.Trim(),
                                    ViewState["idUsulanKegiatan"].ToString());

                                if (dtStatusEliAnggota.Rows[0]["kd_sts_eleigible"].ToString() == "1")
                                {
                                    lblNamaBaru.Text = dt1.Rows[0]["nama"].ToString();
                                    lblInstitusiBaru.Text = dt1.Rows[0]["nama_institusi"].ToString();
                                    ViewState["idPersonalBaru"] = dt1.Rows[0]["id_personal"];
                                    ViewState["idInstitusi"] = dt1.Rows[0]["id_institusi"];

                                    lbSimpan.Visible = true;
                                    bSimpan.Visible = false;
                                }
                                else
                                {
                                    noty.Notify(this.Page, uiNotify.NotifyType.warning, "Terjadi Kesalahan",
                                    "Maaf, " + dtStatusEliAnggota.Rows[0]["keterangan"].ToString());
                                    return;
                                }
                            }
                            else if (ViewState["kdPeranPersonil"].ToString() == "A")
                            {
                                // CekNIDN eligibilitas sebagai ketua
                                DataTable dtStatusEliKetua = new DataTable();
                                dtStatusEliKetua = modelPerubahanPersonil.getStsEligibleSbgKetua(ViewState["idUsulanKegiatan"].ToString());

                                if (dtStatusEliKetua.Rows[0]["is_terpenuhi_calon_keanggotaan_setara_ketua"].ToString() == "1")
                                {
                                    lblNamaBaru.Text = dt1.Rows[0]["nama"].ToString();
                                    lblInstitusiBaru.Text = dt1.Rows[0]["nama_institusi"].ToString();
                                    ViewState["idPersonalBaru"] = dt1.Rows[0]["id_personal"];
                                    ViewState["idInstitusi"] = dt1.Rows[0]["id_institusi"];

                                    lbSimpan.Visible = true;
                                    bSimpan.Visible = false;
                                }
                                else
                                {
                                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Pengganti tidak eligible");
                                    return;
                                }
                            }
                            else
                            {
                                lbSimpan.Visible = true;
                                bSimpan.Visible = false;
                            }
                        }
                        else
                        {
                            bSimpan.Visible = true;
                            lbSimpan.Visible = false;
                            noty.Notify(this.Page, uiNotify.NotifyType.warning, "Terjadi Kesalahan", "NIDN tidak ditemukan");
                        }
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.warning, "Terjadi Kesalahan", "personil lama belum dipilih");
                }
            }
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            var emptyData = new List<string>();
            var noSurat = tbNoSurat.Text;
            if (noSurat.Trim().Length == 0) emptyData.Add($"Nomor Surat");
            var tglSUrat = tbTglSurat.Text;
            if (tglSUrat.Trim().Length == 0) emptyData.Add($"Tanggal Surat");
            var namaBaru = lblNamaBaru.Text;
            if (namaBaru.Trim().Length == 0) emptyData.Add($"Nama Personil Pengganti");

            if (emptyData.Count > 0)
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.warning, "Informasi",
                    "Data berikut harus diisi :<br />" + string.Join(", ", emptyData.ToArray()));
                return;
            }
            else
            {
                if (bool.Parse(ViewState["dataBaru"].ToString()))
                {
                    Guid idPersonil = Guid.Parse(ViewState["idPersonil"].ToString());
                    Guid idUsulanKegiatan = Guid.Parse(ViewState["idUsulanKegiatan"].ToString());
                    Guid idPersonalUpdater = Guid.Parse(objLogin.idPersonal);
                    Guid idPersonal = Guid.Parse(ViewState["idPersonalBaru"].ToString());
                    string kdPeranPersonil = ddlPeranPersonil.SelectedValue;
                    int urutanPeran = 0;
                    int alokasiWaktu = 0;
                    string bidangTugas = "";
                    DataTable dtUrutan = new DataTable();
                    modelPerubahanPersonil.daftarPeranPersonil(ref dtUrutan, ViewState["idUsulanKegiatan"].ToString());
                    if (dtUrutan.Rows.Count > 0)
                    {
                        urutanPeran = int.Parse(dtUrutan.Rows[0]["urutan_personil"].ToString());
                    }

                    if (modelPerubahanPersonil.insupPersonil(idPersonil, idUsulanKegiatan, idPersonalUpdater, idPersonal,
                        kdPeranPersonil, urutanPeran, alokasiWaktu, bidangTugas, tbNoSurat.Text, tbTglSurat.Text, tbCatatan.Text))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", " Ganti personil berhasil");
                        isiGvDaftarPersonil();
                        clearData();
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPerubahanPersonil.errorMessage);
                        return;
                    }
                }
                else
                {
                    Guid idPersonil = Guid.Parse(ViewState["idPersonil"].ToString());
                    Guid idUsulanKegiatan = Guid.Parse(ViewState["idUsulanKegiatan"].ToString());
                    Guid idPersonalUpdater = Guid.Parse(objLogin.idPersonal);
                    Guid idPersonal = Guid.Parse(ViewState["idPersonalBaru"].ToString()); //Guid.Parse("00000000-0000-0000-0000-000000000000");
                    string kdPeranPersonil = "0";
                    int urutanPeran = 0;
                    int alokasiWaktu = 0;
                    string bidangTugas = "";

                    if (modelPerubahanPersonil.insupPersonil(idPersonil, idUsulanKegiatan, idPersonalUpdater, idPersonal,
                        kdPeranPersonil, urutanPeran, alokasiWaktu, bidangTugas, tbNoSurat.Text, tbTglSurat.Text, tbCatatan.Text))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", " Ganti personil berhasil");
                        isiGvDaftarPersonil();
                        clearData();
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPerubahanPersonil.errorMessage);
                        return;
                    }
                }
            }
        }

        protected void lbTambahPersonilBaru_Click(object sender, EventArgs e)
        {
            ViewState["dataBaru"] = true;
            string idPersonil = "00000000-0000-0000-0000-000000000000".ToString();
            ViewState["idPersonil"] = idPersonil;
            pnlPeranPersonil.Visible = true;
            isiddlPeranPersonil();
        }

        private void isiddlPeranPersonil()
        {
            var dt = new DataTable();
            ddlPeranPersonil.Items.Clear();
            ddlPeranPersonil.Items.Add(new ListItem("-- Pilih --", "0"));
            modelPerubahanPersonil.daftarPeranPersonil(ref dt, ViewState["idUsulanKegiatan"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlPeranPersonil.DataSource = dt;
                ddlPeranPersonil.DataBind();
            }
        }

        protected void gvDaftarPersonil_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());

            string idPersonil = gvDaftarPersonil.DataKeys[index]["id_personil"].ToString();
            ViewState["idPersonilHapus"] = idPersonil;

            if (e.CommandName == "Hapus")
            {
                objModal.ShowModal(this.Page, "modalKonfirmasiHapus");
            }
        }

        protected void lbModalStsKonfirmasiHapus_Click(object sender, EventArgs e)
        {
            Guid idPersonilHapus = Guid.Parse(ViewState["idPersonilHapus"].ToString());
            Guid idPersonalUpdater = Guid.Parse(objLogin.idPersonal);

            if (!modelPerubahanPersonil.hapusPersonil(idPersonilHapus, idPersonalUpdater))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPerubahanPersonil.errorMessage);
                return;
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus personil berhasil");
                isiGvDaftarPersonil();
                mvMain.SetActiveView(vPerubahanPersonil);
            }
        }



        #endregion

        #region Perubahan Judul

        protected void lbKembaliPerubahanJudul_Click(object sender, EventArgs e)
        {
            clearDataPerubahanJudul();
            isiGvUsulanDidanai(0);
            mvMain.SetActiveView(vUsulanDidanai);
        }

        private void clearDataPerubahanJudul()
        {
            lblProgramPerubahanJudul.Text = string.Empty;
            lblSkemaPerubahanJudul.Text = string.Empty;
            Label5.Text = string.Empty;
            Label6.Text = string.Empty;
            Label7.Text = string.Empty;
            Label8.Text = string.Empty;
            lblInstitusiBaru.Text = string.Empty;
            lblJudulLama.Text = string.Empty;
            tbJudulBaru.Text = string.Empty;
            tbCatatanPerubahanJudul.Text = string.Empty;
        }

        protected void lbSimpanPerubahanJudul_Click(object sender, EventArgs e)
        {
            //Cek Isian
            List<string> isianKosong = new List<string>();
            if (tbJudulBaru.Text.Trim().Length == 0) isianKosong.Add("Judul Baru");
            
            if (isianKosong.Count > 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.warning, "Informasi isian",
                   "Maaf, Data berikut harus disi :<br />" + string.Join(", ", isianKosong.ToArray()));
                return;
            }
            
            if (modelPerubahanPersonil.insertPerubahanJudul(Guid.Parse(ViewState["idUsulan"].ToString()),
                Guid.Parse(ViewState["idUsulanKegiatan"].ToString()),
                    lblJudulLama.Text.ToString(), tbJudulBaru.Text.Replace("'", " "), 
                    tbCatatanPerubahanJudul.Text.Replace("'", " ")))
            {
                if (modelPerubahanPersonil.updateJudul(Guid.Parse(ViewState["idUsulan"].ToString()),
                tbJudulBaru.Text.Replace("'", " ")))
                {
                    clearDataPerubahanJudul();
                    isiGvUsulanDidanai(0);
                    mvMain.SetActiveView(vUsulanDidanai);
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Data berhasil disimpan...");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPerubahanPersonil.errorMessage);
                    return;
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPerubahanPersonil.errorMessage);
                return;
            }


            
        }

        #endregion


    }
}