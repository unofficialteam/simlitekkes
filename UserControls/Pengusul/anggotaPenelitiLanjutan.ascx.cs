using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class anggotaPenelitiLanjutan : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.anggotaPeneliti objAnggota = new Models.Pengusul.anggotaPeneliti();
        uiGridView obj_uiGridView = new uiGridView();
        uiNotify noty = new uiNotify();
        public event EventHandler OnChildEventOccurs;
        public event EventHandler OnChildBatalEventOccurs;
        //string idUsulanKegiatan = "303760dc-3d94-49ac-9799-3739a6993142";
        //int idSkema = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];
            //ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;

            if (!IsPostBack)
            {
                //isiAnggotaDikti(ViewState["id_usulan_kegiatan"].ToString(), idSkema);
                //isiAnggotaNonDikti();
                //isiDurasiUsulan();
            }
        }

        public void isiDurasiUsulan()
        {
            DataTable dtDurasi = new DataTable();
            objAnggota.getDurasiUsulan(ref dtDurasi, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            if (dtDurasi.Rows.Count > 0)
            {
                lblSkema.Text = dtDurasi.Rows[0]["nama_skema"].ToString();
                lblLamaUsulan.Text = dtDurasi.Rows[0]["lama_kegiatan"].ToString();
                lblUrutanUsulan.Text = dtDurasi.Rows[0]["urutan_thn_usulan"].ToString();
                lblMaksAnggota.Text = dtDurasi.Rows[0]["jml_maksimal_anggota"].ToString();
                lblStsAnggotaDikti.Text = dtDurasi.Rows[0]["sts_anggota_dikti"].ToString();
                lblStsAnggotaDikti2.Text = dtDurasi.Rows[0]["sts_anggota_dikti"].ToString();
                lblStsAnggotaNonDikti.Text = dtDurasi.Rows[0]["sts_anggota_non_dikti"].ToString();
                string sts_jml_anggota = dtDurasi.Rows[0]["sts_jml_anggota"].ToString();

                if (sts_jml_anggota == "1")
                {
                    lbDataBaruAnggotaDikti.Enabled = false;
                    lbDataBaruAnggotaDikti.CssClass = "btn btn-default btn-sm";
                    lbDataBaruAnggotaDikti.ToolTip = "jumlah anggota sesuai batas maksimal";

                    lbDataBaruNonDikti.Enabled = false;
                    lbDataBaruNonDikti.CssClass = "btn btn-default btn-sm";
                    lbDataBaruNonDikti.ToolTip = "jumlah anggota sesuai batas maksimal";
                }
                else
                {
                    lbDataBaruAnggotaDikti.Enabled = true;
                    lbDataBaruAnggotaDikti.CssClass = "btn btn-success btn-sm";
                    lbDataBaruAnggotaDikti.ToolTip = "tambah anggota";

                    lbDataBaruNonDikti.Enabled = true;
                    lbDataBaruNonDikti.CssClass = "btn btn-success btn-sm";
                    lbDataBaruNonDikti.ToolTip = "tambah anggota";
                }
            }
        }

        private void isiAnggotaBaruDikti()
        {
            DataTable dtDurasi = new DataTable();
            objAnggota.getDurasiUsulan(ref dtDurasi, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            if (dtDurasi.Rows.Count > 0)
            {
                lblAnggotaBaruDikti.Text = dtDurasi.Rows[0]["sts_anggota_baru_dikti"].ToString();
                lblAnggotaBaruNonDikti.Text = dtDurasi.Rows[0]["sts_anggota_non_dikti"].ToString();
            }
        }

        public DataTable isiAnggotaDikti(string idUsulanKegiatan, int idSkema)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            ViewState["id_skema"] = idSkema;
            DataTable dtAnggota = new DataTable();
            if (objAnggota.listAnggotaDikti(ref dtAnggota, Guid.Parse(idUsulanKegiatan)))
            {
                gvAnggotaPengusulDikti.DataSource = dtAnggota;
                gvAnggotaPengusulDikti.DataBind();

                //ViewState["peran_personil"] = dtAnggota.Rows[0]["peran_personil"].ToString();
            };

            DataTable dtJmlAnggota = new DataTable();
            objAnggota.getAnggotaDikti(ref dtJmlAnggota, Guid.Parse(idUsulanKegiatan));
            if (dtJmlAnggota.Rows.Count > 0)
            {
                lblJmlAnggotaPengusul.Text = dtJmlAnggota.Rows[0]["jml_anggota"].ToString();
            }

            return dtAnggota;
        }

        protected void lbDataBaruAnggotaDikti_Click(object sender, EventArgs e)
        {
            mvAnggota.SetActiveView(vIsiAnggotaDikti);
            isiAnggotaBaruDikti();
            clearDataDosen();

            if (OnChildEventOccurs != null)
                OnChildEventOccurs(sender, null);
        }

        public void isiAnggotaNonDikti()
        {
            DataTable dtAnggota = new DataTable();
            if (objAnggota.listAnggotaNonDikti(ref dtAnggota, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                gvAnggotaNonDikti.DataSource = dtAnggota;
                gvAnggotaNonDikti.DataBind();
            };

            DataTable dtJmlAnggotaNonDikti = new DataTable();
            objAnggota.getAnggotaNonDikti(ref dtJmlAnggotaNonDikti, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            if (dtJmlAnggotaNonDikti.Rows.Count > 0)
            {
                lblJmlAnggotaNonDikti.Text = dtJmlAnggotaNonDikti.Rows[0]["jml_anggota_non_dikti"].ToString();
            }

        }

        protected void lbDataBaruNonDikti_Click(object sender, EventArgs e)
        {
            isiDdlNegara();
            isiDdlJenjangPendidikan();
            isiDdlPeranPersonilDikti();
            isiAnggotaBaruDikti();
            mvAnggota.SetActiveView(vIsiAnggotaNonDikti);
            if (OnChildEventOccurs != null)
                OnChildEventOccurs(sender, null);
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            clearDataDosen();
            mvAnggota.SetActiveView(vDaftarAnggota);

            if (OnChildBatalEventOccurs != null)
                OnChildBatalEventOccurs(sender, null);
        }

        protected void lbCari_Click(object sender, EventArgs e)
        {
            //daftarDosen objDaftarDosen = new daftarDosen();
            DataTable dtStatusEliAnggota = new DataTable();
            isiDdlPeranPersonilDikti();

            //if (ViewState["id_skema"].ToString() == "5" &&
            //      (ddlPeran.SelectedValue == "C1" || ddlPeran.SelectedValue == "D1")
            //) // pekerti cek tpm
            //{
            //DataTable dt = objAnggota.getRowByNIDN(tbCari.Text);
            //if (dt.Rows.Count <= 0)
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //    "Maaf, Data Dosen tidak dapat ditemukan !");
            //    clearDataDosen();
            //}
            //else
            //{
            //    string idJenjPend = dt.Rows[0]["id_jenjang_pendidikan_tertinggi"].ToString();
            //    if (idJenjPend == "7" ||  // 7 doktor
            //        idJenjPend == "10")  // 10 SP3
            //    {
            //        lblNama.Text = dt.Rows[0]["nama"].ToString();
            //    lblInstitusi.Text = dt.Rows[0]["nama_institusi"].ToString();
            //    lblProgStudi.Text = dt.Rows[0]["nama_program_studi"].ToString();
            //    lblKualifikasi.Text = dt.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
            //    //lblJabatanFungsional.Text = dt.Rows[0]["jabatan_fungsional"].ToString();
            //    lblSurel.Text = dt.Rows[0]["surel"].ToString();
            //        ViewState["idPersonal"] = dt.Rows[0]["id_personal"];
            //        ViewState["idInstitusi"] = dt.Rows[0]["id_institusi"];
            //    }
            //    else
            //    {
            //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //        "Maaf, Personil TPM harus bergelar Doktor!");
            //        clearDataDosen();

            //    }
            //}
            //return;
            //}

            if (tbCari.Text.Length == 10)
            {
                DataTable dt = objAnggota.getRowByNIDN(tbCari.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    // objLogin = (Models.sistem.login)Session["objLogin"];
                    if (dt.Rows[0]["id_personal"].ToString() != objLogin.idPersonal.ToString())
                    {
                        // CekNIDN eligibilitas sebagai anggota
                        dtStatusEliAnggota = objAnggota.getStsEligibleSbgAnggota(tbCari.Text, ViewState["id_usulan_kegiatan"].ToString());

                        if (dtStatusEliAnggota.Rows[0]["kd_sts_eleigible"].ToString() == "1")
                        {
                            lblNama.Text = dt.Rows[0]["nama"].ToString();
                            lblInstitusi.Text = dt.Rows[0]["nama_institusi"].ToString();
                            lblProgStudi.Text = dt.Rows[0]["nama_program_studi"].ToString();
                            lblKualifikasi.Text = dt.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
                            lblSurel.Text = dt.Rows[0]["surel"].ToString();
                            ViewState["idPersonal"] = dt.Rows[0]["id_personal"];
                            ViewState["idInstitusi"] = dt.Rows[0]["id_institusi"];
                        }
                        else
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                            "Maaf, " + dtStatusEliAnggota.Rows[0]["keterangan"].ToString());

                            clearDataDosen();
                        }
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Maaf, Ketua pengusul tidak boleh menjadi anggota pengusul !");

                        clearDataDosen();
                    }
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
            lblProgStudi.Text = "-";
            lblKualifikasi.Text = "-";
            lblSurel.Text = "-";
            ddlPeran.SelectedIndex = 0;
            lblInstitusi.Text = "-";
            tbCari.Text = "";
            tbTugasdlmPenelitian.Text = "";
            lblinfoEligibleAnggota.Visible = false;


            //tbNamaAnggotaPmdsuMhs.Text = "";
            //tbNimAnggotaPmdsuMhs.Text = "";
            //ddlThnMasukMhsPmdsu.SelectedIndex = 0;

            //ddlSemester.SelectedIndex = 0;
            //tbInstitusiAsalAnggMhsPmdsu.Text = "";
            //ddlJenjangPendidikanAnggPmdsu.SelectedIndex = 0;
            //ddlProdiAnggotaPmdsuMhs.SelectedIndex = 0;
            ViewState.Remove("idPersonal");
            ViewState.Remove("idInstitusi");
        }

        private void isiDdlPeranPersonilDikti()
        {
            int idSkema = int.Parse(ViewState["id_skema"].ToString());
            ddlPeran.Items.Clear();
            ddlPeran.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            ddlPeranNonDikti.Items.Clear();
            ddlPeranNonDikti.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            DataTable dt = new DataTable();
            //if (objAnggota.getPeranPersonil(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            if (objAnggota.getPeranPersonil(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                try
                {
                    ddlPeran.DataTextField = "peran_personil";
                    ddlPeran.DataValueField = "kd_peran_personil_gabungan";
                    ddlPeran.DataSource = dt;
                    ddlPeran.DataBind();

                    if (idSkema == 6 || idSkema == 76 || idSkema == 30 || idSkema == 52) //skema PPS
                    {
                        DataTable dtPPS = new DataTable();
                        objAnggota.getPeranPersonilSkemaPPS(ref dtPPS, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "J");

                        ddlPeranNonDikti.DataTextField = "peran_personil";
                        ddlPeranNonDikti.DataValueField = "kd_peran_personil_gabungan";
                        ddlPeranNonDikti.DataSource = dtPPS;
                        ddlPeranNonDikti.DataBind();
                    }
                    else
                    {
                        ddlPeranNonDikti.DataTextField = "peran_personil";
                        ddlPeranNonDikti.DataValueField = "kd_peran_personil_gabungan";
                        ddlPeranNonDikti.DataSource = dt;
                        ddlPeranNonDikti.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                }
            }
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            if (ViewState["id_personil"] == null)
            {
                if (ddlPeran.SelectedValue == "0")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
                       "Peran personil belum dipilih.");
                    return;
                }

                if (ViewState["idPersonal"] == null)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
                       "Pilih Dosen menggunakan identitas NIDN terlebih dahulu...");
                    return;
                }

                if (ddlPeran.SelectedValue == "0")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
                       "Silahkan Pilih peran personil terlebih dahulu...");
                    return;
                }

                if (tbTugasdlmPenelitian.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
                       "Silahkan tugas dalam penelitian terlebih dahulu...");
                    return;
                }


                int idSkema = int.Parse(ViewState["id_skema"].ToString());

                if (idSkema == 6 || idSkema == 30 || idSkema == 76 || idSkema == 52)
                {
                    if (ddlPeran.SelectedValue == "J1" && lblKualifikasi.Text != "S-3")
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
                                    "Kualifikasi/Jenjang Pendidikan Dosen Pembimbing Anggota tidak eligible...");
                        lblinfoEligibleAnggota.Visible = true;
                        lblinfoEligibleAnggota.Text = "Kualifikasi/jenjang pendidikan dosen pembimbing anggota harus S-3";
                        return;
                    }
                }

                // Recek eligibilitas calon anggota
                DataTable dtStatusEliAnggota = new DataTable();
                dtStatusEliAnggota = objAnggota.getStsEligibleSbgAnggota(tbCari.Text, ViewState["id_usulan_kegiatan"].ToString());

                if (dtStatusEliAnggota.Rows[0]["kd_sts_eleigible"].ToString() == "1")
                {
                    if (ddlPeran.SelectedValue != "C1")
                    {
                        if (ddlPeran.SelectedValue != "D1")
                        {
                            dtStatusEliAnggota = objAnggota.getStsEligibleSbgAnggotaTPM(tbCari.Text, ViewState["id_usulan_kegiatan"].ToString());
                            if (dtStatusEliAnggota.Rows[0]["kd_sts_eleigible"].ToString() != "1")
                            {
                                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                                    "Maaf, " + dtStatusEliAnggota.Rows[0]["keterangan"].ToString());

                                clearDataDosen();
                                return;
                            }
                        }
                    }

                    Guid idPersonal = Guid.Parse(ViewState["idPersonal"].ToString());
                    string kdPeranGabungan = ddlPeran.SelectedValue;
                    string kdPeranPersonil = kdPeranGabungan.Substring(0, 1);
                    int urutanPeran = Convert.ToInt32(kdPeranGabungan.Substring(1, kdPeranGabungan.Length - 1));
                    //float alokasi_waktu = float.Parse(tbAlokasiWaktu.Text.Trim().Replace(",", "."));
                    string bidang_tugas = tbTugasdlmPenelitian.Text;

                    if (!objAnggota.insertDataBaruAnggota(idPersonal, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()),
                                kdPeranPersonil, urutanPeran, bidang_tugas))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                           objAnggota.errorMessage);
                    }

                    mvAnggota.SetActiveView(vDaftarAnggota);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Maaf, " + dtStatusEliAnggota.Rows[0]["keterangan"].ToString());

                    clearDataDosen();
                }
            }
            else
            {
                if (tbTugasdlmPenelitian.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
                       "Silahkan tugas dalam penelitian terlebih dahulu...");
                    return;
                }

                Guid idPersonil = Guid.Parse(ViewState["id_personil"].ToString());
                string bidangTugas = tbTugasdlmPenelitian.Text;
                if (!objAnggota.updateBidangTugas(idPersonil, bidangTugas))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objAnggota.errorMessage);
                }

                mvAnggota.SetActiveView(vDaftarAnggota);               
            }

            ViewState.Remove("idPersonal");
            ViewState.Remove("idInstitusi");
            isiAnggotaDikti(ViewState["id_usulan_kegiatan"].ToString(), int.Parse(ViewState["id_skema"].ToString()));
            isiDurasiUsulan();
            isiAnggotaBaruDikti();
            clearDataDosen();
            tbCari.Text = string.Empty;
        }

        private void isiDdlNegara()
        {
            ddlNegara.Items.Clear();
            ddlNegara.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            DataTable dt = new DataTable();
            if (objAnggota.listNegara(ref dt))
            {
                try
                {
                    ddlNegara.DataTextField = "nama_negara";
                    ddlNegara.DataValueField = "kd_negara";
                    ddlNegara.DataSource = dt;
                    ddlNegara.DataBind();
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                }
            }
        }

        private void isiDdlJenjangPendidikan()
        {
            ddlKualifikasi.Items.Clear();
            ddlKualifikasi.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            DataTable dt = new DataTable();
            if (objAnggota.listJenjangPendidikan(ref dt))
            {
                try
                {
                    ddlKualifikasi.DataTextField = "jenjang_pendidikan";
                    ddlKualifikasi.DataValueField = "id_jenjang_pendidikan";
                    ddlKualifikasi.DataSource = dt;
                    ddlKualifikasi.DataBind();
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                }
            }
        }

        protected void gvAnggotaPengusulDikti_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblNamaPersonil.Text = gvAnggotaPengusulDikti.DataKeys[e.RowIndex]["nama_anggota"].ToString();
            ViewState["idPersonil"] = gvAnggotaPengusulDikti.DataKeys[e.RowIndex]["id_personil"].ToString();

            uiModal uiMdl = new uiModal();
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvAnggotaNonDikti_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblNamaNonDikti.Text = gvAnggotaNonDikti.DataKeys[e.RowIndex]["nama"].ToString();
            ViewState["idPersonil"] = gvAnggotaNonDikti.DataKeys[e.RowIndex]["id_personil"].ToString();

            uiModal uiMdl = new uiModal();
            uiMdl.ShowModal(this.Page, "modalHapusNonDIkti");
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            Guid idPersonil = Guid.Parse(ViewState["idPersonil"].ToString());
            if (objAnggota.deleteData(idPersonil))
            {
                ViewState.Remove("idPersonil");
                isiAnggotaDikti(ViewState["id_usulan_kegiatan"].ToString(), int.Parse(ViewState["id_skema"].ToString()));
                isiDurasiUsulan();
                isiAnggotaBaruDikti();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                   "Data Personil berhasil dihapus...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objAnggota.errorMessage);
            }
        }

        protected void lbHapusNonDikti_Click(object sender, EventArgs e)
        {
            Guid idPersonil = Guid.Parse(ViewState["idPersonil"].ToString());
            if (objAnggota.deleteDataNonDosen(idPersonil))
            {
                ViewState.Remove("idPersonil");
                isiAnggotaNonDikti();
                isiAnggotaBaruDikti();
                isiDurasiUsulan();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                   "Data Personil berhasil dihapus...");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objAnggota.errorMessage);
            }
        }

        protected void gvAnggotaPengusulDikti_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int idSkema = int.Parse(ViewState["id_skema"].ToString());

            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_konfirmasi = gvAnggotaPengusulDikti.DataKeys[e.Row.RowIndex]["kd_sts_konfirmasi"].ToString();

                Label lblStsPersetujuan = new Label();
                lblStsPersetujuan = (Label)e.Row.FindControl("lblStsPersetujuan");
                Label lblPersetujuan = new Label();
                lblPersetujuan = (Label)e.Row.FindControl("lblPersetujuan");
                LinkButton lbHapus = new LinkButton();
                lbHapus = (LinkButton)e.Row.FindControl("lbHapus");
                LinkButton lbUbahAnggota = new LinkButton();
                lbUbahAnggota = (LinkButton)e.Row.FindControl("lbUbahAnggota");

                if (kd_sts_konfirmasi == "0")
                {
                    lblStsPersetujuan.CssClass = "fa fa-question-circle";
                    lblStsPersetujuan.ForeColor = System.Drawing.Color.DodgerBlue;
                    lblPersetujuan.ForeColor = System.Drawing.Color.DodgerBlue;
                }
                else
                {
                    lblStsPersetujuan.CssClass = "fa fa-check-square";
                    lblStsPersetujuan.ForeColor = System.Drawing.Color.Green;
                    lblPersetujuan.ForeColor = System.Drawing.Color.Green;
                }

                if (idSkema == 6)
                {
                    lbHapus.Visible = true;
                    lbDataBaruAnggotaDikti.Visible = true;
                    lbUbahAnggota.Visible = true;
                }
                else
                {
                    lbHapus.Visible = false;
                    lbDataBaruAnggotaDikti.Visible = false;
                    lbUbahAnggota.Visible = true;
                }
            }
            else
            {
                if (idSkema == 6)
                {
                    lbDataBaruAnggotaDikti.Visible = true;
                }
                else
                {
                    lbDataBaruAnggotaDikti.Visible = false;
                }
            }
        }

        protected void lbSimpanNonDikti_Click(object sender, EventArgs e)
        {
            if (ViewState["id_personil"] == null)
            {
                if (ddlPeranNonDikti.SelectedIndex == 0)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
                       "Peran personil belum dipilih.");
                    return;
                }

                if (ddlPeranNonDikti.SelectedValue == "0")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan Pilih peran personil terlebih dahulu...");
                    return;
                }


                if (tbTugasPenelitian.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan isi tugas dalam penelitian");
                    return;
                }
                if (tbNamaLengkap.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan isi nama lengkap dan gelar");
                    return;
                }
                if (tbNoIdentitas.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan isi nomor identitas KPT/Paspor");
                    return;
                }
                if (tbAlamatTinggal.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan isi alamat tinggal dengan lengkap");
                    return;
                }
                if (tbNoHP.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan isi nomor hp aktif");
                    return;
                }
                if (tbAlamatSurel.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan isi alamat surel/email aktif");
                    return;
                }
                if (tbInstansi.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan isi instansi");
                    return;
                }
                if (ddlNegara.SelectedValue == "0")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan pilih nama negara");
                    return;
                }
                if (tbBidangKeahlian.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan isi bidang keahlian");
                    return;
                }
                if (ddlKualifikasi.SelectedValue == "0")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan Pilih pendidikan terakhir");
                    return;
                }



                // Recek eligibilitas calon anggota
                DataTable dtStatusEliAnggota = new DataTable();

                string kdPeranGabungan = ddlPeranNonDikti.SelectedValue;
                string kdPeranPersonil = kdPeranGabungan.Substring(0, 1);
                int urutanPeran = Convert.ToInt32(kdPeranGabungan.Substring(1, kdPeranGabungan.Length - 1));

                string bidang_tugas = tbTugasPenelitian.Text;
                string nama = tbNamaLengkap.Text;
                string no_ktp = tbNoIdentitas.Text;
                string alamat = tbAlamatTinggal.Text;
                string no_hp = tbNoHP.Text;
                string surel = tbAlamatSurel.Text;
                string nama_instansi_asal = tbInstansi.Text;
                string kd_negara = ddlNegara.SelectedValue;
                string bidang_keahlian = tbBidangKeahlian.Text;
                int id_jenjang_pendidikan = int.Parse(ddlKualifikasi.SelectedValue);

                if (!objAnggota.insertDataBaruAnggotaNonDikti(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), urutanPeran, nama, no_ktp, alamat, no_hp
                            , surel, nama_instansi_asal, bidang_tugas, kd_negara, kdPeranPersonil, bidang_keahlian, id_jenjang_pendidikan))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objAnggota.errorMessage);
                }
            }
            else
            {
                if (tbTugasPenelitian.Text == "")
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
                       "Silahkan tugas dalam penelitian terlebih dahulu...");
                    return;
                }

                Guid idPersonil = Guid.Parse(ViewState["id_personil"].ToString());
                string bidangTugas = tbTugasPenelitian.Text;
                if (!objAnggota.updateBidangTugasNonDosen(idPersonil, bidangTugas))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                       objAnggota.errorMessage);
                }

                mvAnggota.SetActiveView(vDaftarAnggota);
            }

            mvAnggota.SetActiveView(vDaftarAnggota);
            ViewState.Remove("idInstitusi");
            isiAnggotaNonDikti();
            isiAnggotaBaruDikti();
            clearDataNonDosen();
            isiDurasiUsulan();
            tbCari.Text = string.Empty;
        }

        private void clearDataNonDosen()
        {
            ddlPeranNonDikti.SelectedValue = "0";
            tbTugasdlmPenelitian.Text = "";
            tbNamaLengkap.Text = "";
            tbNoIdentitas.Text = "";
            tbAlamatTinggal.Text = "";
            tbNoHP.Text = "";
            tbAlamatSurel.Text = "";
            tbInstansi.Text = "";
            ddlNegara.SelectedValue = "0";
            tbBidangKeahlian.Text = "";
            ddlKualifikasi.SelectedValue = "0";

        }

        protected void lbBatalNonDikti_Click(object sender, EventArgs e)
        {
            clearDataNonDosen();
            mvAnggota.SetActiveView(vDaftarAnggota);
        }

        protected void gvAnggotaPengusulDikti_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            mvAnggota.SetActiveView(vIsiAnggotaDikti);
            isiAnggotaBaruDikti();

            var idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            var idPersonil = Guid.Parse(gvAnggotaPengusulDikti.DataKeys[e.RowIndex]["id_personil"].ToString());
            ViewState["id_personil"] = idPersonil;
            DataTable dtAnggota = new DataTable();
            if (objAnggota.listAnggotaDiktiPersonil(ref dtAnggota, idUsulanKegiatan, idPersonil))
            {
                if (dtAnggota.Rows.Count > 0)
                {
                    tbCari.Text = dtAnggota.Rows[0]["nidn"].ToString();
                    lblNama.Text = dtAnggota.Rows[0]["nama_anggota"].ToString();
                    lblInstitusi.Text = dtAnggota.Rows[0]["nama_institusi"].ToString();
                    lblProgStudi.Text = dtAnggota.Rows[0]["nama_program_studi"].ToString();
                    lblKualifikasi.Text = dtAnggota.Rows[0]["jenjang_pendidikan"].ToString();
                    lblSurel.Text = dtAnggota.Rows[0]["surel"].ToString();
                    ddlPeran.SelectedItem.Text = dtAnggota.Rows[0]["peran_personil"].ToString();
                    tbTugasdlmPenelitian.Text = dtAnggota.Rows[0]["bidang_tugas"].ToString();
                }
            };
        }

        protected void gvAnggotaNonDikti_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            mvAnggota.SetActiveView(vIsiAnggotaNonDikti);
            isiAnggotaNonDikti();

            var idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            var idPersonil = Guid.Parse(gvAnggotaPengusulDikti.DataKeys[e.RowIndex]["id_personil"].ToString());
            ViewState["id_personil"] = idPersonil;
            DataTable dtAnggota = new DataTable();
            if (objAnggota.listAnggotaNonDiktiPersonil(ref dtAnggota, idUsulanKegiatan, idPersonil))
            {
                if (dtAnggota.Rows.Count > 0)
                {
                    tbNamaLengkap.Text = dtAnggota.Rows[0]["nama"].ToString();
                    tbNoIdentitas.Text = dtAnggota.Rows[0]["no_ktp"].ToString();
                    tbAlamatTinggal.Text = dtAnggota.Rows[0]["alamat"].ToString();
                    ddlNegara.SelectedItem.Text = dtAnggota.Rows[0]["nama_negara"].ToString();
                    tbInstansi.Text = dtAnggota.Rows[0]["nama_instansi_asal"].ToString();
                    ddlKualifikasi.Text = dtAnggota.Rows[0]["jenjang_pendidikan"].ToString();
                    tbBidangKeahlian.Text = dtAnggota.Rows[0]["bidang_keahlian"].ToString();
                    tbAlamatSurel.Text = dtAnggota.Rows[0]["surel"].ToString();
                    ddlPeranNonDikti.SelectedItem.Text = dtAnggota.Rows[0]["peran_personil"].ToString();
                    tbTugasPenelitian.Text = dtAnggota.Rows[0]["bidang_tugas"].ToString();
                }
            };
        }

        protected void gvAnggotaNonDikti_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int idSkema = int.Parse(ViewState["id_skema"].ToString());

            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbHapus = new LinkButton();
                lbHapus = (LinkButton)e.Row.FindControl("lbHapus");
                LinkButton lbUbah = new LinkButton();
                lbUbah = (LinkButton)e.Row.FindControl("lbUbah");

                if (idSkema == 6)
                {
                    lbHapus.Visible = true;
                    lbDataBaruNonDikti.Visible = true;
                    lbUbah.Visible = true;
                }
                else
                {
                    lbHapus.Visible = false;
                    lbDataBaruNonDikti.Visible = false;
                    lbUbah.Visible = true;
                }
            }
            else
            {
                if (idSkema == 6)
                {
                    lbDataBaruNonDikti.Visible = true;
                }
                else
                {
                    lbDataBaruNonDikti.Visible = false;
                }
            }
        }
    }
}