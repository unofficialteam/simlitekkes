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

namespace simlitekkes.UserControls.Pengusul
{
    public partial class cvKetuaAbdimas : System.Web.UI.UserControl
    {
        Models.Pengusul.persyaratanUmumAbdimas objPersyaratan = new Models.Pengusul.persyaratanUmumAbdimas();
        Models.Pengusul.berandaPengusul objBerandaPengusul = new Models.Pengusul.berandaPengusul();
        daftarTendikNonDosen objTendik = new daftarTendikNonDosen();

        login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList objDdl = new uiDropdownList();
        uiModal objModal = new uiModal();
        Core.kontrolUnggah ktUnggah = new Core.kontrolUnggah();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void isiCvKetua(string thnUsulan, string thnPelaksanaan)
        {
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            //string thn_kegiatan = DateTime.Now.Year.ToString();
            string kd_jenis_kegiatan = "2";

            ViewState["thn_usulan"] = thnUsulan;
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;

            // isi h-index
            var dtHindex = new DataTable();
            if (objBerandaPengusul.getPersonal(ref dtHindex, id_personal))
            {
                if (dtHindex.Rows.Count > 0)
                {
                    lblHindex1.Text = dtHindex.Rows[0]["hindex"].ToString();
                }
            }
            bool isTendik = objTendik.isTendik(Guid.Parse(objLogin.idPersonal));

            // isi data personal
            DataTable dtCvKetua = new DataTable();
            objBerandaPengusul.getPersonal(ref dtCvKetua, objLogin.idPersonal);
            if (dtCvKetua.Rows.Count > 0)
            {
                lblNamaLengkap.Text = dtCvKetua.Rows[0]["nama_lengkap"].ToString();
                if (isTendik)
                {
                    nidn_area.Visible = false;
                    prodi_area.Visible = false;
                }
                else
                {
                    lblProdi.Text = dtCvKetua.Rows[0]["nama_program_studi"].ToString();
                    lblNidn.Text = dtCvKetua.Rows[0]["nidn"].ToString();
                }
                lblNamaInstitusi.Text = dtCvKetua.Rows[0]["nama_institusi"].ToString();

                //lblIdSinta1.Text = dtCvKetua.Rows[0]["id_sinta"].ToString();
                lblKualifikasi.Text = dtCvKetua.Rows[0]["jenjang_pendidikan_tertinggi"].ToString();
                lblSurel.Text = dtCvKetua.Rows[0]["surel"].ToString();
            }

            // isi jmulah usulan baru
            var dtJmlUsulanBaru = new DataTable();
            if (objPersyaratan.getJmlUsulanBaruRb(ref dtJmlUsulanBaru, id_personal, thnUsulan, thnPelaksanaan, kd_jenis_kegiatan))
            {
                if (dtJmlUsulanBaru.Rows.Count > 0)
                {
                    lblJmlUsulanBaru1.Text = dtJmlUsulanBaru.Rows[0]["jml_data"].ToString();
                }
                else
                {
                    lblJmlUsulanBaru1.Text = "0";
                }
            }
            
            //Isi nama klaster dan skema sesuai dengan klaster pengusul
            var dtKlasterKetua = new DataTable();
            objPersyaratan.getKlasterPersonal(ref dtKlasterKetua, id_personal, kd_jenis_kegiatan);
            if (dtKlasterKetua.Rows.Count > 0)
            {
                lblKlaster.Text = dtKlasterKetua.Rows[0]["nama_klaster"].ToString();
                string kd_klaster = dtKlasterKetua.Rows[0]["kd_klaster"].ToString();

                var dtSkemaKlaster = new DataTable();
                if (objPersyaratan.getSkemaByKlaster(ref dtSkemaKlaster, kd_klaster, isTendik))
                {
                    lvSkema.DataSource = dtSkemaKlaster;
                    lvSkema.DataBind();
                }
            }

            //Isi rekam jejak
            var dtRekamJejak = new DataTable();
            if (objPersyaratan.getJmlRekamJejak(ref dtRekamJejak, id_personal))
            {
                lvRekamJejak.DataSource = dtRekamJejak;
                lvRekamJejak.DataBind();
            }
        }

        protected void lvRekamJejak_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            var kd_rekam_jejak = lvRekamJejak.DataKeys[e.NewEditIndex]["kd_rekam_jejak"].ToString();
            if(kd_rekam_jejak == "0")
            {
                mvcvKetua.SetActiveView(vPengabdian);
                riwayatPengabdian.isiRiwayatPengabdian();
            }
            else if (kd_rekam_jejak == "1")
            {
                mvcvKetua.SetActiveView(vJurnal);
                artikelJurnal.tambahDataRekamJejak();
            }
            else if(kd_rekam_jejak == "2")
            {
                mvcvKetua.SetActiveView(vProsiding);
                prosiding.tambahDataRekamJejak();
            }
            else if (kd_rekam_jejak == "3")
            {
                mvcvKetua.SetActiveView(vHki);
                hki.tambahDataRekamJejak();
            }
            else if (kd_rekam_jejak == "4")
            {
                mvcvKetua.SetActiveView(vBuku);
                buku.tambahDataRekamJejak();
            }
        }

        protected void lvRekamJejak_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            LinkButton lbEdit = new LinkButton();
            lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
            string kd_rekam_jejak = drv["kd_rekam_jejak"].ToString();
            if (kd_rekam_jejak == "0")
            {
                lbEdit.Visible = false;
            }
            else
            {
                lbEdit.Visible = true;
            }
        }

        protected void lbKembaliPengabdian_Click(object sender, EventArgs e)
        {
            mvcvKetua.SetActiveView(vCvKetua);
            isiCvKetua(ViewState["thn_usulan"].ToString(), ViewState["thn_pelaksanaan"].ToString());
        }

        protected void lbKembaliProsiding_Click(object sender, EventArgs e)
        {
            mvcvKetua.SetActiveView(vCvKetua);
            isiCvKetua(ViewState["thn_usulan"].ToString(), ViewState["thn_pelaksanaan"].ToString());
        }

        protected void lbKembaliJurnal_Click(object sender, EventArgs e)
        {
            mvcvKetua.SetActiveView(vCvKetua);
            isiCvKetua(ViewState["thn_usulan"].ToString(), ViewState["thn_pelaksanaan"].ToString());
        }

        protected void lbKembaliHki_Click(object sender, EventArgs e)
        {
            mvcvKetua.SetActiveView(vCvKetua);
            isiCvKetua(ViewState["thn_usulan"].ToString(), ViewState["thn_pelaksanaan"].ToString());
        }

        protected void lbKembaliBuku_Click(object sender, EventArgs e)
        {
            mvcvKetua.SetActiveView(vCvKetua);
            isiCvKetua(ViewState["thn_usulan"].ToString(), ViewState["thn_pelaksanaan"].ToString());
        }
    }
}