﻿using simlitekkes.Helper;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.PerbaikanProposal
{
    public partial class perbaikanProposal : System.Web.UI.UserControl
    {
        Models.Pengusul.identitasUsulan objModelIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        Models.Sistem.usulanKegiatan objUsulanKegiatan = new Models.Sistem.usulanKegiatan();
        uiNotify noty = new uiNotify();
        //rekapUsulanPerbaikan objRekap = new rekapUsulanPerbaikan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                mvMain.SetActiveView(vListUsulan);
                //init();
            }
            ktListUsulan.OnChildRowUpdate += new EventHandler(NewEvt_OnChildRowUpdate);
            ktListUsulan.OnChildUnduhPdfUsulanLengkap += new EventHandler(NewEvt_OnChildUnduhPdfLengkap);
            ktDokumenUsulan.OnChildUnggah += new EventHandler(NewEvt_OnChildUnggah);
        }

        protected void NewEvt_OnChildUnduhPdfLengkap(object sender, EventArgs e)
        {
            UnduhPdfLengkap();
        }

        private void UnduhPdfLengkap()
        {
            string idUsulanKegiatan = ktListUsulan.getIdUsulanKegiatan();
            string id_transaksi_kegiatan = ktListUsulan.getIdTransaksiKegiatan();
            string isPerbaikan = "1";
            ktPdfLengkap.UnduhProposalLengkap(idUsulanKegiatan, id_transaksi_kegiatan, isPerbaikan);
        }

            protected void NewEvt_OnChildRowUpdate(object sender, EventArgs e)
        {
            objUsulanKegiatan = ktListUsulan.getObjUsulanKegiatan();
            string idUsulanKegiatan = objUsulanKegiatan.idUsulanKegiatan;
            string id_transaksi_kegiatan = ktListUsulan.getIdTransaksiKegiatan();
            // string idTransaksiKegiatan, string idUsulan, string idUsulanKegiatan, int idSkema, int targetTkt, string thnPelaksanaan
            ktDokumenUsulan.setData(id_transaksi_kegiatan, objUsulanKegiatan.idUsulan, objUsulanKegiatan.idUsulanKegiatan,
                objUsulanKegiatan.idSkema, objUsulanKegiatan.tktTarget, objUsulanKegiatan.thnPelaksanaan);
            ViewState["nomor_wizard"] = 0;
            ViewState["UsulanKegiatan"] = objUsulanKegiatan;
            ViewState["thnPelaksanaan"] = objUsulanKegiatan.thnPelaksanaan;
            ViewState["idTransaksiKegiatan"] = id_transaksi_kegiatan;
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            mvMain.SetActiveView(vWizardUsulan);
            mvWizard.SetActiveView(vDokumenUsulan);
            string info = "Penelitian Dasar (Tahun ke-{0} dari {1} tahun)";
            if (objUsulanKegiatan.tktTarget <= 3)
            {
                lblInfoPenelitianSbk.Text = string.Format(info, objUsulanKegiatan.urutanTahunUsulanKegiatan, objUsulanKegiatan.lamaKegiatan);
            }
            else if (objUsulanKegiatan.tktTarget <= 6)
            {
                info = "Penelitian Terapan (Tahun ke-{0} dari {1} tahun)";
                lblInfoPenelitianSbk.Text = string.Format(info, objUsulanKegiatan.urutanTahunUsulanKegiatan, objUsulanKegiatan.lamaKegiatan);
            }
            else if (objUsulanKegiatan.tktTarget <= 9)
            {
                info = "Penelitian Pengembangan (Tahun ke-{0} dari {1} tahun)";
                lblInfoPenelitianSbk.Text = string.Format(info, objUsulanKegiatan.urutanTahunUsulanKegiatan, objUsulanKegiatan.lamaKegiatan);
            }

            lblJudul.Text = objUsulanKegiatan.judul;
            lblInfoSkema.Text = objUsulanKegiatan.namaSkema;
            lblInfoThn.Text = string.Format("Thn Usulan {0} | Thn. Pelaksanaan {1}-{2}", objUsulanKegiatan.thnUsulan,
                objUsulanKegiatan.thnPelaksanaan, (int.Parse(objUsulanKegiatan.thnPertamaUsulan) + objUsulanKegiatan.lamaKegiatan));

            string path = String.Format("~/fileUpload/dokumenUsulanRevisi/{0}/{1}.pdf", objUsulanKegiatan.thnPelaksanaan, id_transaksi_kegiatan);
            cekFileExists(path);
        }

        protected void NewEvt_OnChildUnggah(object sender, EventArgs e)
        {
            string path = String.Format("~/fileUpload/dokumenUsulanRevisi/{0}/{1}.pdf", ViewState["thnPelaksanaan"].ToString(), ViewState["idTransaksiKegiatan"].ToString());
            cekFileExists(path);
        }

        private void cekFileExists(string ppathDokumenUsulanPdf)
        {
            if (File.Exists(Server.MapPath(ppathDokumenUsulanPdf)))
            {
                lbUnduhPdfDok.ForeColor = System.Drawing.Color.Red;
                lbUnduhPdfDok.Enabled = true;
                ViewState["dokumen_exists"] = true;
            }
            else
            {
                lbUnduhPdfDok.ForeColor = System.Drawing.Color.Gray;
                lbUnduhPdfDok.Enabled = false;
                ViewState["dokumen_exists"] = false;
            }
        }

        protected void lbLanjutkan_Click(object sender, EventArgs e)
        {
            int no_wz = int.Parse(ViewState["nomor_wizard"].ToString());
            // simpan di form dokumen usulan
            if(no_wz == 0)
            {
                //if(!ktDokumenUsulan.simpan())
                //{
                //    return;
                //}
            }

            if(no_wz <= 2)
            no_wz++;

            gantiWIzard(no_wz);
            ViewState["nomor_wizard"] = no_wz;
        }

        private void gantiWIzard(int no_wz) {
            Models.Sistem.usulanKegiatan objUsulanKegiatan = (Models.Sistem.usulanKegiatan)ViewState["UsulanKegiatan"];

            lbKirimUsulan.Visible = false;
            lblKirimUsulan.Visible = false;
            if (no_wz==0)
            {
                mvWizard.SetActiveView(vDokumenUsulan);
            }
            else if (no_wz == 1)
            {
                mvWizard.SetActiveView(vRekapLuaran);
                ktRekapLuaran.init(ViewState["id_usulan_kegiatan"].ToString());
            }
            else if (no_wz == 2)
            {
                mvWizard.SetActiveView(vRab);
                rabRevisiControl.Refresh(objUsulanKegiatan);
            }
            else if (no_wz == 3)
            {
                mvWizard.SetActiveView(vSubmitUsulan);
                lbKirimUsulan.Visible = true;
                lbLanjutkan.Visible = false;
                bool enableKirimUsulan = ktKirimUsulan.initUsulan(objUsulanKegiatan.idUsulanKegiatan);
                //bool dokumenExists = bool.Parse( ViewState["dokumen_exists"].ToString());
                //dokumenExists = dokumenExists & enableKirimUsulan;
                lbKirimUsulan.Visible = enableKirimUsulan;
                lblKirimUsulan.Visible =  !enableKirimUsulan; 
            }
        }

        protected void lbSubstansi1_Click(object sender, EventArgs e)
        {
            int no_wzd = 0;
            ViewState["nomor_wizard"] = no_wzd;
            string path = String.Format("fileUpload/dokumenUsulanRevisi/{0}/{1}.pdf", ViewState["thnPelaksanaan"].ToString(), ViewState["idTransaksiKegiatan"].ToString());
            cekFileExists(path);
            lbKirimUsulan.Visible = false;
            lbLanjutkan.Visible = true;
            gantiWIzard(no_wzd);
        }

        protected void lbRab1_Click(object sender, EventArgs e)
        {
            int no_wzd = 2;
            ViewState["nomor_wizard"] = no_wzd;
            lbKirimUsulan.Visible = false;
            lbLanjutkan.Visible = true;
            gantiWIzard(no_wzd);
        }

        protected void lbKirimUsulan1_Click(object sender, EventArgs e)
        {
            int no_wzd = 3;
            ViewState["nomor_wizard"] = no_wzd;
            lbKirimUsulan.Visible = true;
            lbLanjutkan.Visible = false;
            gantiWIzard(no_wzd);
        }

        protected void lbUnduhPdfDok_Click(object sender, EventArgs e)
        {
            UnduhPdfLengkap();
            //string path = String.Format("fileUpload/dokumenUsulanRevisi/{0}/{1}.pdf", ViewState["thnPelaksanaan"].ToString(), ViewState["idTransaksiKegiatan"].ToString());
            //if (File.Exists(Server.MapPath(path)))
            //{
            //    string PATH_UNGGAH_BERKAS = "../fileUpload/dokumenUsulanRevisi/" + ViewState["thnPelaksanaan"].ToString();
            //    string namaBerkas = "dokumenUsulan.pdf";
            //    var atributUnduh = new AtributUnduh
            //    {
            //        FolderUnduh = PATH_UNGGAH_BERKAS,
            //        NamaBerkas = ViewState["idTransaksiKegiatan"] + ".pdf",
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

        protected void lbSubmitUsulan_Click(object sender, EventArgs e)
        {
            if (objModelIdentitasUsulan.kirimUsulanPerbaikan(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                    "Kirim perbaikan usulan berhasil.");

                mvMain.SetActiveView(vListUsulan);
                //cekStatusKirimUsulan(ViewState["id_usulan_kegiatan"].ToString());
                string path = String.Format("~/fileUpload/dokumenUsulanRevisi/{0}/{1}.pdf", ViewState["thnPelaksanaan"].ToString(), ViewState["idTransaksiKegiatan"].ToString());
                cekFileExists(path);
                CleanSession();
                init();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objModelIdentitasUsulan.errorMessage);
            }
        }

        private void init()
        {
            mvMain.SetActiveView(vListUsulan);
            ktListUsulan.init();
        }

        private void CleanSession()
        {
            //if (Session["usulan_kegiatan"] != null)
            //    Session.Remove("usulan_kegiatan");
            if (Session["ktListUsulan"] != null)
                Session.Remove("ktListUsulan");
            //if (Session["id_usulan_kegiatan"] != null)
            //    Session.Remove("id_usulan_kegiatan");
            //if (Session["isEdit"] != null)
            //    Session.Remove("isEdit");
            //if (Session["idPersonalAnggota"] != null)
            //    Session.Remove("idPersonalAnggota");
            //if (Session["urutanAnggota"] != null)
            //    Session.Remove("urutanAnggota");
            //if (Session["AtributUnduh"] != null)
            //    Session.Remove("AtributUnduh");

        }
    }
}