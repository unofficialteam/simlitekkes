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
    public partial class anggotaAbdimas : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.anggotaAbdimas objAnggota = new Models.Pengusul.anggotaAbdimas();
        Models.Pengusul.daftarTendikNonDosen objTendik = new Models.Pengusul.daftarTendikNonDosen();

        uiGridView obj_uiGridView = new uiGridView();
        uiNotify noty = new uiNotify();
        public event EventHandler OnChildEventOccurs;
        public event EventHandler OnChildBatalEventOccurs;
        //string idUsulanKegiatan = "439bea51-592f-4f37-bd72-40c5477fc349";
        //int idSkema = 15;

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];
            lblNamaKetua.Text = objLogin.namaLengkap;
            if (!IsPostBack)
            {
                //isiAnggotaDikti(idUsulanKegiatan, idSkema);                
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
                lblStsAnggotaDikti.Text = dtDurasi.Rows[0]["sts_anggota_dosen_kemkes"].ToString();
                lblStsAnggotaDikti2.Text = dtDurasi.Rows[0]["sts_anggota_dosen_kemkes"].ToString();
                lbAnggotaTendikKemkes.Text = dtDurasi.Rows[0]["sts_anggota_tendik_kemkes"].ToString();
                lblStsAnggotaNonDikti.Text = dtDurasi.Rows[0]["sts_anggota_non_kemkes"].ToString();
                string sts_jml_anggota = dtDurasi.Rows[0]["sts_jml_anggota"].ToString();

                if(sts_jml_anggota == "1")
                {
                    lbDataBaruAnggotaDikti.Enabled = false;
                    lbDataBaruAnggotaDikti.CssClass = "btn btn-default btn-sm";
                    lbDataBaruAnggotaDikti.ToolTip = "jumlah anggota sesuai batas maksimal";

                    lbDataBaruNonDikti.Enabled = false;
                    lbDataBaruNonDikti.CssClass = "btn btn-default btn-sm";
                    lbDataBaruNonDikti.ToolTip = "jumlah anggota sesuai batas maksimal";

                    lbDataBaruTendikKemkes.Enabled = false;
                    lbDataBaruTendikKemkes.CssClass = "btn btn-default btn-sm";
                    lbDataBaruTendikKemkes.ToolTip = "jumlah anggota sesuai batas maksimal";
                }
                else
                {
                    lbDataBaruAnggotaDikti.Enabled = true;
                    lbDataBaruAnggotaDikti.CssClass = "btn btn-success btn-sm";
                    lbDataBaruAnggotaDikti.ToolTip = "tambah anggota";

                    lbDataBaruNonDikti.Enabled = true;
                    lbDataBaruNonDikti.CssClass = "btn btn-success btn-sm";
                    lbDataBaruNonDikti.ToolTip = "tambah anggota";

                    lbDataBaruTendikKemkes.Enabled = true;
                    lbDataBaruTendikKemkes.CssClass = "btn btn-success btn-sm";
                    lbDataBaruTendikKemkes.ToolTip = "tambah anggota";
                }
            }
        }

        private void isiAnggotaBaruDikti()
        {
            DataTable dtDurasi = new DataTable();
            objAnggota.getDurasiUsulan(ref dtDurasi, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            if (dtDurasi.Rows.Count > 0)
            {
                lblAnggotaBaruDikti.Text = dtDurasi.Rows[0]["sts_anggota_baru_kemkes"].ToString();
                lblAnggotaBaruTendikKemkes.Text = dtDurasi.Rows[0]["sts_anggota_baru_kemkes"].ToString();
                lblAnggotaBaruNonDikti.Text = dtDurasi.Rows[0]["sts_anggota_non_kemkes"].ToString();
            }
        }
        public DataTable isiAnggotaDikti(string idUsulanKegiatan, int idSkema)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            ViewState["id_skema"] = idSkema;
            DataTable dtAnggota = new DataTable();
            if (objAnggota.listAnggotaDikti (ref dtAnggota, Guid.Parse(idUsulanKegiatan)))               
                {
                    gvAnggotaPengusulDikti.DataSource = dtAnggota;
                    gvAnggotaPengusulDikti.DataBind();
                };       
            
            DataTable dtJmlAnggota = new DataTable();
            objAnggota.getAnggotaDikti(ref dtJmlAnggota, Guid.Parse(idUsulanKegiatan));
            if (dtJmlAnggota.Rows.Count > 0)
            {
                lblJmlAnggotaPengusul.Text = dtJmlAnggota.Rows[0]["jml_anggota"].ToString();
            }

            return dtAnggota;
        }

        public DataTable isiAnggotaTendik(string idUsulanKegiatan, int idSkema)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            ViewState["id_skema"] = idSkema;
            DataTable dtAnggota = new DataTable();
            if (objAnggota.listAnggotaTendik(ref dtAnggota, Guid.Parse(idUsulanKegiatan)))
            {
                gvAnggotaTendikKemkes.DataSource = dtAnggota;
                gvAnggotaTendikKemkes.DataBind();
            };

            DataTable dtJmlAnggota = new DataTable();
            objAnggota.getAnggotaTendik(ref dtJmlAnggota, Guid.Parse(idUsulanKegiatan));
            if (dtJmlAnggota.Rows.Count > 0)
            {
                lbJmlAnggotaTendikKemkes.Text = dtJmlAnggota.Rows[0]["jml_anggota"].ToString();
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
            clearDataNonDosen();
            isiDdlNegara();
            isiDdlJenjangPendidikan();
            isiDdlPeranPersonilDikti();
            isiAnggotaBaruDikti();
            mvAnggota.SetActiveView(vIsiAnggotaNonDikti);
            if (OnChildEventOccurs != null)
                OnChildEventOccurs(sender, null);
        }

        protected void lbDataBaruTendikKemkes_Click(object sender, EventArgs e)
        {
            isiDdlJenjangPendidikan();
            isiDdlPeranPersonilDikti();
            isiAnggotaBaruDikti();
            mvAnggota.SetActiveView(vIsiAnggotaTendikKemkes);
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
            DataTable dtStatusEliAnggota = new DataTable();
            isiDdlPeranPersonilDikti();
            if (tbCari.Text.Length == 10)
            {
                DataTable dt = objAnggota.getRowByNIDN(tbCari.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
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
            tbTugasdlmPengabdian.Text = "";
            lblinfoEligibleAnggota.Visible = false;

            ViewState.Remove("idPersonal");
            ViewState.Remove("idInstitusi");
        }

        private void isiDdlPeranPersonilDikti()
        {
            int idSkema = int.Parse(ViewState["id_skema"].ToString());
            ddlPeran.Items.Clear();
            ddlPeran.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            ddlPeranTendikKemkes.Items.Clear();
            ddlPeranTendikKemkes.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            ddlPeranNonDikti.Items.Clear();
            ddlPeranNonDikti.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            DataTable dt = new DataTable();
            if (objAnggota.getPeranPersonil(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                try
                {
                    ddlPeran.DataTextField = "peran_personil";
                    ddlPeran.DataValueField = "kd_peran_personil_gabungan";
                    ddlPeran.DataSource = dt;
                    ddlPeran.DataBind();

                    ddlPeranTendikKemkes.DataTextField = "peran_personil";
                    ddlPeranTendikKemkes.DataValueField = "kd_peran_personil_gabungan";
                    ddlPeranTendikKemkes.DataSource = dt;
                    ddlPeranTendikKemkes.DataBind();

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
            if (ddlPeran.SelectedIndex == 0)
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

            if (tbTugasdlmPengabdian.Text == "")
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
                Guid idPersonal = Guid.Parse(ViewState["idPersonal"].ToString());
                string kdPeranGabungan = ddlPeran.SelectedValue;
                string kdPeranPersonil = kdPeranGabungan.Substring(0, 1);
                int urutanPeran = Convert.ToInt32(kdPeranGabungan.Substring(1, kdPeranGabungan.Length - 1));
                string bidang_tugas = tbTugasdlmPengabdian.Text;

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

            ViewState.Remove("idPersonal");
            ViewState.Remove("idInstitusi");
            isiAnggotaDikti(ViewState["id_usulan_kegiatan"].ToString(), int.Parse(ViewState["id_skema"].ToString()));
            isiDurasiUsulan();
            isiAnggotaBaruDikti();
            clearDataDosen();
            tbCari.Text = string.Empty;
        }


        protected void lbSimpanTendikKemkes_Click(object sender, EventArgs e)
        {
            if (ddlPeranTendikKemkes.SelectedIndex == 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
                   "Peran personil belum dipilih.");
                return;
            }

            if (ViewState["idPersonal"] == null)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
                   "Pilih Tendik menggunakan Nomor KTP terlebih dahulu...");
                return;
            }

            if (tbTugasPenelitianTendikKemkes.Text == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
                   "Silahkan tugas dalam penelitian terlebih dahulu...");
                return;
            }


            int idSkema = int.Parse(ViewState["id_skema"].ToString());

            //if (idSkema == 6 || idSkema == 30 || idSkema == 76 || idSkema == 52)
            //{
            //    if (ddlPeran.SelectedValue == "J1" && ddlKualifikasiTendikKemkes.SelectedItem.Text != "S-3")
            //    {
            //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
            //                    "Kualifikasi/Jenjang Pendidikan Tendik Pembimbing Anggota tidak eligible...");
            //        lblinfoEligibleAnggotaTendikKemkes.Visible = true;
            //        lblinfoEligibleAnggotaTendikKemkes.Text = "Kualifikasi/jenjang pendidikan Tendik pembimbing anggota harus S-3";
            //        return;
            //    }
            //}
            // Tendik Tanpa Pengecekan Eligibilitas
            Guid idPersonal = Guid.Parse(ViewState["idPersonal"].ToString());
            string kdPeranGabungan = ddlPeranTendikKemkes.SelectedValue;
            string kdPeranPersonil = kdPeranGabungan.Substring(0, 1);
            int urutanPeran = Convert.ToInt32(kdPeranGabungan.Substring(1, kdPeranGabungan.Length - 1));

            //float alokasi_waktu = float.Parse(tbAlokasiWaktu.Text.Trim().Replace(",", "."));
            string bidang_tugas = tbTugasPenelitianTendikKemkes.Text;

            if (!objAnggota.insertDataBaruAnggota(idPersonal, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()),
                        kdPeranPersonil, urutanPeran, bidang_tugas, true))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objAnggota.errorMessage);
            }
            ViewState["show_lanjutkan_at_anggota"] = true;
            mvAnggota.SetActiveView(vDaftarAnggota);
            if (OnChildBatalEventOccurs != null)
                OnChildBatalEventOccurs(sender, null);

            ViewState.Remove("idPersonal");
            ViewState.Remove("idInstitusi");
            isiAnggotaTendik(ViewState["id_usulan_kegiatan"].ToString(), int.Parse(ViewState["id_skema"].ToString()));
            isiDurasiUsulan();
            isiAnggotaBaruDikti();
            clearDataTendik();
            tbNoKTPTendikKemkes.Text = string.Empty;
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
            //ddlKualifikasiTendikKemkes.Items.Clear();
            ddlKualifikasi.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            //ddlKualifikasiTendikKemkes.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            DataTable dt = new DataTable();
            if (objAnggota.listJenjangPendidikan(ref dt))
            {
                try
                {
                    ddlKualifikasi.DataTextField = "jenjang_pendidikan";
                    ddlKualifikasi.DataValueField = "id_jenjang_pendidikan";
                    ddlKualifikasi.DataSource = dt;
                    ddlKualifikasi.DataBind();
                    //ddlKualifikasiTendikKemkes.DataTextField = "jenjang_pendidikan";
                    //ddlKualifikasiTendikKemkes.DataValueField = "id_jenjang_pendidikan";
                    //ddlKualifikasiTendikKemkes.DataSource = dt;
                    //ddlKualifikasiTendikKemkes.DataBind();
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                }
            }
        }

        protected void gvAnggotaNonDikti_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblNamaNonDikti.Text = gvAnggotaNonDikti.DataKeys[e.RowIndex]["nama"].ToString();
            ViewState["idPersonil"] = gvAnggotaNonDikti.DataKeys[e.RowIndex]["id_personil"].ToString();

            uiModal uiMdl = new uiModal();
            uiMdl.ShowModal(this.Page, "modalHapusNonDIkti");
        }

        protected void gvAnggotaTendikKemkes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblNamaTendikKemkes.Text = gvAnggotaTendikKemkes.DataKeys[e.RowIndex]["nama_anggota"].ToString();
            ViewState["idPersonil"] = gvAnggotaTendikKemkes.DataKeys[e.RowIndex]["id_personil"].ToString();

            uiModal uiMdl = new uiModal();
            uiMdl.ShowModal(this.Page, "modalHapusTendikKemkes");
        }

        protected void gvAnggotaPengusulDikti_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblNamaPersonil.Text = gvAnggotaPengusulDikti.DataKeys[e.RowIndex]["nama_anggota"].ToString();
            ViewState["idPersonil"] = gvAnggotaPengusulDikti.DataKeys[e.RowIndex]["id_personil"].ToString();

            uiModal uiMdl = new uiModal();
            uiMdl.ShowModal(this.Page, "modalHapus");
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

        protected void gvAnggotaPengusulDikti_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_konfirmasi = gvAnggotaPengusulDikti.DataKeys[e.Row.RowIndex]["kd_sts_konfirmasi"].ToString();

                Label lblStsPersetujuan = new Label();
                lblStsPersetujuan = (Label)e.Row.FindControl("lblStsPersetujuan");
                Label lblPersetujuan = new Label();
                lblPersetujuan = (Label)e.Row.FindControl("lblPersetujuan");

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
            }
        }

        protected void gvAnggotaPengusulTendik_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_konfirmasi = gvAnggotaTendikKemkes.DataKeys[e.Row.RowIndex]["kd_sts_konfirmasi"].ToString();

                Label lblStsPersetujuan = new Label();
                lblStsPersetujuan = (Label)e.Row.FindControl("lblStsPersetujuan");
                Label lblPersetujuan = new Label();
                lblPersetujuan = (Label)e.Row.FindControl("lblPersetujuan");

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
            }
        }

        protected void lbCariTendik_Click(object sender, EventArgs e)
        {
            DataTable result = new DataTable();
            objTendik.getTendikByKTP(tbNoKTPTendikKemkes.Text, ref result);
            if (result.Rows.Count == 0)
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
                   "Tidak Ditemukan Tendik Dengan Nomor KTP Tersebut");
            else
            {
                if (result.Rows[0]["id_personal"].ToString().Equals(objLogin.idPersonal))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
                   "Ketua Tidak Boleh Menjadi Anggota");
                }
                else
                {
                    ViewState["idPersonal"] = result.Rows[0]["id_personal"];
                    ViewState["idInstitusi"] = result.Rows[0]["id_institusi"];
                    tbNamaLengkapTendikKemkes.Text = result.Rows[0]["nama"].ToString();
                    tbALamatTendikKemkes.Text = result.Rows[0]["alamat"].ToString();
                    tbInstansiTendikKemkes.Text = result.Rows[0]["nama_institusi"].ToString();
                    tbAlamatSurelTendikKemkes.Text = result.Rows[0]["surel"].ToString();
                    tbBidangKeahlianTendikKemkes.Text = result.Rows[0]["bidang_keahlian"].ToString();
                    tbNoHpTendikKemkes.Text = result.Rows[0]["nomor_hp"].ToString();
                }
            }
        }

        protected void lbSimpanNonDikti_Click(object sender, EventArgs e)
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
            //if (ddlNegara.SelectedValue == "0")
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Silahkan pilih nama negara");
            //    return;
            //}
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
            //dtStatusEliAnggota = objAnggota.getStsEligibleSbgAnggota(tbCari.Text, idUsulanKegiatan);

            //if (dtStatusEliAnggota.Rows[0]["kd_sts_eleigible"].ToString() == "1")
            //{
            //Guid idPersonal = Guid.Parse(ViewState["idPersonal"].ToString());
            string kdPeranGabungan = ddlPeranNonDikti.SelectedValue;
            string kdPeranPersonil = kdPeranGabungan.Substring(0, 1);
            int urutanPeran = Convert.ToInt32(kdPeranGabungan.Substring(1, kdPeranGabungan.Length - 1));
            //float alokasi_waktu = float.Parse(tbAlokasiWaktu.Text.Trim().Replace(",", "."));
            string bidang_tugas = tbTugasPenelitian.Text;
            string nama = tbNamaLengkap.Text;
            string no_ktp = tbNoIdentitas.Text;
            string alamat = tbAlamatTinggal.Text;
            string no_hp = tbNoHP.Text;
            string surel = tbAlamatSurel.Text;
            string nama_instansi_asal = tbInstansi.Text;
            string kd_negara = "ID";//ddlNegara.SelectedValue;
            string bidang_keahlian = tbBidangKeahlian.Text;
            int id_jenjang_pendidikan = int.Parse(ddlKualifikasi.SelectedValue);

            if (!objAnggota.insertDataBaruAnggotaNonDikti(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), urutanPeran, nama, no_ktp, alamat, no_hp
                        , surel, nama_instansi_asal, bidang_tugas, kd_negara, kdPeranPersonil, bidang_keahlian, id_jenjang_pendidikan))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objAnggota.errorMessage);
            }
            ViewState["show_lanjutkan_at_anggota"] = true;
            mvAnggota.SetActiveView(vDaftarAnggota);
            if (OnChildBatalEventOccurs != null)
                OnChildBatalEventOccurs(sender, null);
            //}
            //else
            //{
            //  noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //"Maaf, " + dtStatusEliAnggota.Rows[0]["keterangan"].ToString());

            //clearDataDosen();
            //}

            //ViewState.Remove("idPersonal");
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
            tbTugasdlmPengabdian.Text = "";
            tbNamaLengkap.Text = "";
            tbNoIdentitas.Text = "";
            tbAlamatTinggal.Text = "";
            tbNoHP.Text = "";
            tbAlamatSurel.Text = "";
            tbInstansi.Text = "";
            //ddlNegara.SelectedValue = "0";
            tbBidangKeahlian.Text = "";
            ddlKualifikasi.SelectedValue = "0";

        }

        private void clearDataTendik()
        {
            ddlPeranTendikKemkes.SelectedValue = "0";
            tbTugasPenelitianTendikKemkes.Text = "";
            tbNamaLengkapTendikKemkes.Text = "";
            tbNoKTPTendikKemkes.Text = "";
            tbALamatTendikKemkes.Text = "";
            tbNoHpTendikKemkes.Text = "";
            tbAlamatSurelTendikKemkes.Text = "";
            tbInstansiTendikKemkes.Text = "";
            //ddlNegara.SelectedValue = "0";
            tbBidangKeahlianTendikKemkes.Text = "";
            //ddlKualifikasiTendikKemkes.SelectedValue = "0";

        }

        protected void lbBatalNonDikti_Click(object sender, EventArgs e)
        {
            clearDataNonDosen();
            ViewState["show_lanjutkan_at_anggota"] = true;
            mvAnggota.SetActiveView(vDaftarAnggota);
            if (OnChildBatalEventOccurs != null)
                OnChildBatalEventOccurs(sender, null);
        }

        protected void lbBatalTendikKemkes_Click(object sender, EventArgs e)
        {
            clearDataTendik();
            ViewState["show_lanjutkan_at_anggota"] = true;
            mvAnggota.SetActiveView(vDaftarAnggota);
            if (OnChildBatalEventOccurs != null)
                OnChildBatalEventOccurs(sender, null);
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

        protected void lbHapusTendikKemkes_Click(object sender, EventArgs e)
        {
            Guid idPersonil = Guid.Parse(ViewState["idPersonil"].ToString());
            if (objAnggota.deleteData(idPersonil, true))
            {
                ViewState.Remove("idPersonil");
                isiAnggotaTendik(ViewState["id_usulan_kegiatan"].ToString(), int.Parse(ViewState["id_skema"].ToString()));
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

        protected void lbSimpanBidangTugasKetua_Click(object sender, EventArgs e)
        {
            if (objAnggota.updateBidangTugasKetua(ViewState["id_usulan_kegiatan"].ToString(), tbBidangTugasKetua.Text))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                   "Update bidang tugas ketua berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objAnggota.errorMessage);
            }
        }
    }
}