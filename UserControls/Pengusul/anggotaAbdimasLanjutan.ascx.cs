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
    public partial class anggotaAbdimasLanjutan : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.anggotaAbdimas objAnggota = new Models.Pengusul.anggotaAbdimas();
        uiGridView obj_uiGridView = new uiGridView();
        uiNotify noty = new uiNotify();
        public event EventHandler OnChildEventOccurs;
        public event EventHandler OnChildBatalEventOccurs;
        //string idUsulanKegiatan = "439bea51-592f-4f37-bd72-40c5477fc349";
        //int idSkema = 15;

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];

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
                lblStsAnggotaDikti.Text = dtDurasi.Rows[0]["sts_anggota_dikti"].ToString();
                lblStsAnggotaDikti2.Text = dtDurasi.Rows[0]["sts_anggota_dikti"].ToString();
                string sts_jml_anggota = dtDurasi.Rows[0]["sts_jml_anggota"].ToString();

                //if(sts_jml_anggota == "1")
                //{
                //    lbDataBaruAnggotaDikti.Enabled = false;
                //    lbDataBaruAnggotaDikti.CssClass = "btn btn-default btn-sm";
                //    lbDataBaruAnggotaDikti.ToolTip = "jumlah anggota sesuai batas maksimal";
                //}
                //else
                //{
                //    lbDataBaruAnggotaDikti.Enabled = true;
                //    lbDataBaruAnggotaDikti.CssClass = "btn btn-success btn-sm";
                //    lbDataBaruAnggotaDikti.ToolTip = "tambah anggota";
                //}
            }
        }

        private void isiAnggotaBaruDikti()
        {
            DataTable dtDurasi = new DataTable();
            objAnggota.getDurasiUsulan(ref dtDurasi, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            if (dtDurasi.Rows.Count > 0)
            {
                lblAnggotaBaruDikti.Text = dtDurasi.Rows[0]["sts_anggota_baru_dikti"].ToString();
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
        //protected void lbDataBaruAnggotaDikti_Click(object sender, EventArgs e)
        //{
        //    mvAnggota.SetActiveView(vIsiAnggotaDikti);
        //    isiAnggotaBaruDikti();
        //    clearDataDosen();
            
        //    if (OnChildEventOccurs != null)
        //        OnChildEventOccurs(sender, null);
        //}       

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
            DataTable dt = new DataTable();
            if (objAnggota.getPeranPersonil(ref dt, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                try
                {
                    ddlPeran.DataTextField = "peran_personil";
                    ddlPeran.DataValueField = "kd_peran_personil_gabungan";
                    ddlPeran.DataSource = dt;
                    ddlPeran.DataBind();                   
                }
                catch (Exception ex)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                }
            }

        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            //if (ddlPeran.SelectedIndex == 0)
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
            //       "Peran personil belum dipilih.");
            //    return;
            //}

            //if (ViewState["idPersonal"] == null)
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
            //       "Pilih Dosen menggunakan identitas NIDN terlebih dahulu...");
            //    return;
            //}

            //if (ddlPeran.SelectedValue == "0")
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
            //       "Silahkan Pilih peran personil terlebih dahulu...");
            //    return;
            //}

            if (tbTugasdlmPengabdian.Text == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi",
                   "Silahkan tugas dalam penelitian terlebih dahulu...");
                return;
            }

            //int idSkema = int.Parse(ViewState["id_skema"].ToString());

            //if (idSkema == 6 || idSkema == 30 || idSkema == 76 || idSkema == 52)
            //{
            //    if (ddlPeran.SelectedValue == "J1" && lblKualifikasi.Text != "S-3")
            //    {
            //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi",
            //                    "Kualifikasi/Jenjang Pendidikan Dosen Pembimbing Anggota tidak eligible...");
            //        lblinfoEligibleAnggota.Visible = true;
            //        lblinfoEligibleAnggota.Text = "Kualifikasi/jenjang pendidikan dosen pembimbing anggota harus S-3";
            //        return;
            //    }
            //}  

            //// Recek eligibilitas calon anggota
            //DataTable dtStatusEliAnggota = new DataTable();
            //dtStatusEliAnggota = objAnggota.getStsEligibleSbgAnggota(tbCari.Text, ViewState["id_usulan_kegiatan"].ToString());

            //if (dtStatusEliAnggota.Rows[0]["kd_sts_eleigible"].ToString() == "1")
            //{
            //    Guid idPersonal = Guid.Parse(ViewState["idPersonal"].ToString());
            //    string kdPeranGabungan = ddlPeran.SelectedValue;
            //    string kdPeranPersonil = kdPeranGabungan.Substring(0, 1);
            //    int urutanPeran = Convert.ToInt32(kdPeranGabungan.Substring(1, kdPeranGabungan.Length - 1));
            //    string bidang_tugas = tbTugasdlmPengabdian.Text;

            //    if (!objAnggota.insertDataBaruAnggota(idPersonal, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()),
            //                kdPeranPersonil, urutanPeran, bidang_tugas))
            //    {
            //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //           objAnggota.errorMessage);
            //    }

            //    mvAnggota.SetActiveView(vDaftarAnggota);
            //}
            //else
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //    "Maaf, " + dtStatusEliAnggota.Rows[0]["keterangan"].ToString());

            //    clearDataDosen();
            //}

            Guid idPersonil = Guid.Parse(ViewState["id_personil"].ToString());
            string bidangTugas = tbTugasdlmPengabdian.Text;
            if (!objAnggota.updateBidangTugas(idPersonil, bidangTugas))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objAnggota.errorMessage);
            }

            mvAnggota.SetActiveView(vDaftarAnggota);

            ViewState.Remove("idPersonal");
            ViewState.Remove("idInstitusi");
            isiAnggotaDikti(ViewState["id_usulan_kegiatan"].ToString(), int.Parse(ViewState["id_skema"].ToString()));
            isiDurasiUsulan();
            isiAnggotaBaruDikti();
            clearDataDosen();
            tbCari.Text = string.Empty;
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

        protected void gvAnggotaPengusulDikti_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            mvAnggota.SetActiveView(vIsiAnggotaDikti);
            isiAnggotaBaruDikti();

            var idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            var idPersonil = Guid.Parse(gvAnggotaPengusulDikti.DataKeys[e.RowIndex]["id_personil"].ToString());
            ViewState["id_personil"] = idPersonil;
            DataTable dtAnggota = new DataTable();
            if (objAnggota.listAnggotaPersonil(ref dtAnggota, idUsulanKegiatan, idPersonil))
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
                    tbTugasdlmPengabdian.Text = dtAnggota.Rows[0]["bidang_tugas"].ToString();
                }
            };
        }
    }
}