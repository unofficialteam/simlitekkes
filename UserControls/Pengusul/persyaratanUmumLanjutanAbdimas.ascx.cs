using simlitekkes.Models;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class persyaratanUmumLanjutanAbdimas : System.Web.UI.UserControl
    {
        Models.Pengusul.persyaratanUmumAbdimas objPersyaratan = new Models.Pengusul.persyaratanUmumAbdimas();
        Models.Pengusul.berandaPengusul objBerandaPengusul = new Models.Pengusul.berandaPengusul();

        login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList objDdl = new uiDropdownList();
        uiModal objModal = new uiModal();
        Core.kontrolUnggah ktUnggah = new Core.kontrolUnggah();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        public event EventHandler childEvent;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public int isiDataPersyaratanUmum()
        {
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_kegiatan = DateTime.Now.Year.ToString();
            string kd_jenis_kegiatan = "2";
            //string thn_pelaksanaan_kegiatan = ddlThnPelaksanaanKegiatan.SelectedValue;

            // cek jml usulan lanjutan
            var dtJmlUsulanBaru = new DataTable();
            if (objPersyaratan.getJmlUsulanLanjutan(ref dtJmlUsulanBaru, id_personal, thn_kegiatan, kd_jenis_kegiatan))
            {
                if (dtJmlUsulanBaru.Rows.Count > 0)
                {
                    lblJmlUsulanLanjutan.Text = dtJmlUsulanBaru.Rows[0]["jml_data"].ToString();
                }
                else
                {
                    lblJmlUsulanLanjutan.Text = "0";
                }
            }

            // Cek Data SINTA
            var dtSinta = new DataTable();
            if (objPersyaratan.getCekSinta(ref dtSinta, Guid.Parse(objLogin.idPersonal).ToString()))
            {
                if (dtSinta.Rows.Count > 0)
                {
                    string kd_sts_persyaratan_sinta = dtSinta.Rows[0]["kd_sts_persyaratan_sinta"].ToString();
                    ViewState["kd_sts_persyaratan_sinta"] = kd_sts_persyaratan_sinta;

                    string id_sinta = dtSinta.Rows[0]["id_sinta"].ToString();
                    ViewState["id_sinta"] = id_sinta;
                }
            }

            // Cek Data Dosen
            var dtCekDataDosen = new DataTable();
            if (objPersyaratan.getCekDataDosen(ref dtCekDataDosen, id_personal))
            {
                if (dtCekDataDosen.Rows.Count > 0)
                {
                    string kd_sts_eligible_dosen = dtCekDataDosen.Rows[0]["kd_sts_eligible_dosen"].ToString();
                    ViewState["kd_sts_eligible_dosen"] = kd_sts_eligible_dosen;

                    string status_gabungan = dtCekDataDosen.Rows[0]["status_gabungan"].ToString();
                    ViewState["status_gabungan"] = status_gabungan;

                    string status_aktif_dosen = dtCekDataDosen.Rows[0]["status_aktif_dosen"].ToString();
                    ViewState["status_aktif_dosen"] = status_aktif_dosen;

                    if (kd_sts_eligible_dosen == "1")
                    {
                        lblStsPegawai.Text = status_aktif_dosen;
                    }
                    else
                    {

                    }
                }
            }

            // Cek Data Tanggungan
            var dtCekTanggungan = new DataTable();
            if (objPersyaratan.getJmTanggungan(ref dtCekTanggungan, id_personal))
            {
                if (dtCekTanggungan.Rows.Count > 0)
                {
                    int total_jml_tanggungan = int.Parse(dtCekTanggungan.Rows[0]["total_jml_tanggungan"].ToString());
                    ViewState["total_jml_tanggungan"] = total_jml_tanggungan;
                    if (total_jml_tanggungan > 0)
                    {
                        // isi daftar tanggungan
                        var dtDaftarTanggungan = new DataTable();
                        if (objPersyaratan.getDaftarTanggungan(ref dtDaftarTanggungan, id_personal))
                        {
                            lvTanggungan.DataSource = dtDaftarTanggungan;
                            lvTanggungan.DataBind();
                        }
                    }
                    else
                    {
                        lblTanggungan.Text = "Tidak ada";
                    }
                }
            }

            // cek Blacklist
            var dtBlacklist = new DataTable();
            if (objPersyaratan.getBlacklist(ref dtBlacklist, id_personal))
            {
                if (dtBlacklist.Rows.Count > 0)
                {
                    ViewState["isBlacklist"] = true;
                    string tgl_mulai = dtBlacklist.Rows[0]["tgl_mulai"].ToString();
                    ViewState["tgl_mulai"] = tgl_mulai;
                    string tgl_berakhir = dtBlacklist.Rows[0]["tgl_berakhir"].ToString();
                    ViewState["tgl_berakhir"] = tgl_berakhir;
                    string keterangan = dtBlacklist.Rows[0]["keterangan"].ToString();
                    ViewState["keterangan"] = keterangan;
                }
                else
                {
                    ViewState["isBlacklist"] = false;
                }
            }
            
            int totalSyarat = konfigurPersyaratan();
            return totalSyarat;
        }

        private int konfigurPersyaratan()
        {
            string kd_sts_eligible_dosen = ViewState["kd_sts_eligible_dosen"].ToString();
            string status_gabungan = ViewState["status_gabungan"].ToString();
            string kd_sts_persyaratan_sinta = ViewState["kd_sts_persyaratan_sinta"].ToString();
            int total_jml_tanggungan = int.Parse(ViewState["total_jml_tanggungan"].ToString());
            string id_sinta = ViewState["id_sinta"].ToString();
            string status_aktif_dosen = ViewState["status_aktif_dosen"].ToString();

            int totalSyarat = 0;

            if (kd_sts_eligible_dosen == "1")
            {
                pnlPegawai.Visible = true;
                lblStsPegawai.Text = status_aktif_dosen.ToString();
                totalSyarat++;
            }
            else
            {
                pnlPegawai.Visible = false;
                pnlPegawai1.Visible = true;
                lblStsPegawai1.Text = status_gabungan;
            }

            if (kd_sts_persyaratan_sinta == "1")
            {
                pnlCekSinta.Visible = true;
                lblIdSinta.Text = id_sinta;
                totalSyarat++;
            }
            else
            {
                pnlCekSinta.Visible = false;
                pnlCekSinta1.Visible = true;
                lblIdSinta11.Text = "-";
            }

            if (total_jml_tanggungan == 0)
            {
                pnlTanggungan.Visible = true;
                lblTanggungan.Text = "Tidak ada";
                totalSyarat++;
            }
            else
            {
                pnlTanggungan.Visible = false;
                pnlTanggungan1.Visible = true;
                lblTanggungan1.Text = total_jml_tanggungan.ToString();
            }

            if (bool.Parse(ViewState["isBlacklist"].ToString()))
            {
                pnlBlacklist.Visible = true;
                lblMulaiBlacklist.Text = ViewState["tgl_mulai"].ToString();
                lblBerakhirBlacklist.Text = ViewState["tgl_berakhir"].ToString();
                lblKetBlacklist.Text = ViewState["keterangan"].ToString();
            }
            else
            {
                totalSyarat++;
                pnlBlacklist.Visible = false;
            }
            
            return totalSyarat;
        }
    }
}