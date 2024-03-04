using simlitekkes.Models.Sistem;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.pendaftaranReviewer
{
    public partial class pendaftaranReviewerInternal : System.Web.UI.UserControl
    {
        referensiData refData = new referensiData();
        uiNotify noty = new uiNotify();
        simlitekkes.Models.Pengusul.persyaratanUmum objPersyaratanUmum = new simlitekkes.Models.Pengusul.persyaratanUmum();
        simlitekkes.Models.Pengusul.persyaratanUmumPendaftaranReviewer objPersyUmum = new simlitekkes.Models.Pengusul.persyaratanUmumPendaftaranReviewer();
        Models.login objLogin;

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbDaftar);
            if (!IsPostBack)
            {
                objLogin = (Models.login)Session["objLogin"];
                DataTable dtr = new DataTable();
                objPersyUmum.getStatusReviewerInternal(ref dtr, objLogin.idPersonal);
                string kd_sts_aktif_reviewer_internal = "0";
                ViewState["kd_sts_aktif_reviewer_internal"] = "0";
                //lblJudulForm.Text = "Pendaftaran Reviewer Internal Penelitian";
                if (dtr.Rows.Count > 0)
                {
                    //string kd_sts_aktif_reviewer_nasional = dtr.Rows[0]["kd_sts_aktif_reviewer_nasional"].ToString();
                    kd_sts_aktif_reviewer_internal = dtr.Rows[0]["kd_sts_aktif"].ToString();

                    //ViewState["kd_sts_aktif_reviewer_nasional"] = kd_sts_aktif_reviewer_nasional;
                    ViewState["kd_sts_aktif_reviewer_internal"] = kd_sts_aktif_reviewer_internal;

                    int kd_sts = 9;
                    kd_sts = int.Parse(ViewState["kd_sts_aktif_reviewer_internal"].ToString());

                    if (kd_sts == 1)
                    {
                        ktDokPendukung.init(false);
                        //lblJudulForm.Text = "Daftar Ulang Reviewer Internal PT (Penelitian)";
                    }
                    else
                    {
                        ktDokPendukung.init(true);
                    }

                }
                else
                {
                    ktSyaratUmumRevPenelitian.showPanelReviewerBaru(true);
                    ktDokPendukung.init(true);
                }

                mvMain.SetActiveView(vPenelitian);
                DataTable dt = new DataTable();
                refData.getRumpunIlmu(ref dt);
                var dr1 = dt.Select("level_rumpun_ilmu=3");
                DataTable dt2 = dr1.CopyToDataTable();
                ddlKepakaran.DataSource = dt2;
                ddlKepakaran.DataBind();


                tampilkanIsianPendaftaran();
                int jmlTotalSyarat = ktSyaratUmumRevPenelitian.isiDataPersyaratanUmum();
                lbDaftar.Visible = false;
                lblDaftar.Visible = false;
                //if (jmlTotalSyarat >= 3 || kd_sts_aktif_reviewer_internal == "1")
                if (kd_sts_aktif_reviewer_internal == "1")
                {
                    //lbDaftar.Visible = true;
                    ViewState["eligible"] = "1";
                    setJadwal();
                }
                else
                {
                    ViewState["eligible"] = "0";
                    lblDaftar.Visible = true;
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Anda belum terdaftar sebagai reviewer internal.");
                }
            }

            ktDokPendukung.OnChildEventOccursKembali += new EventHandler(lbKembali_Click);
            ktSyaratUmumRevPenelitian.childEventEdit += new EventHandler(editDokSeminar);
        }

        //private void infoHanyaUntukDaftarUlang()
        //{
        //    mvMain.Visible = false;
        //    pnlDaftarNEdit.Visible = false;
        //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Anda belum terdaftar sebagai reviewer internal.");
        //}


        private void setJadwal()
        {
            objLogin = (Models.login)Session["objLogin"];
            lbDaftar.Visible = false;
            lblDaftar.Visible = false;
            DataTable dtJadwal = new DataTable();
            //if (ViewState["jadwal_daftar_ulang"] == null && ViewState["jadwal_daftar_baru"] == null)
            //{
            objPersyUmum.listJadwalKegiatan(ref dtJadwal,
                Guid.Parse(objLogin.idPersonal),
                ddlTahunUsulan.SelectedValue);
            ViewState["jadwal_daftar_ulang"] = "0";
            if (dtJadwal.Rows.Count > 0)
            {
                int pjg = 5;
                pjg = dtJadwal.Select("kd_jalur_seleksi='daftar_ulang_rev_internal'").Length;

                if (dtJadwal.Select("kd_jalur_seleksi='daftar_ulang_rev_internal'").Length > 0 && ViewState["kd_sts_aktif_reviewer_internal"].ToString() == "1")
                {
                    DataTable dtDaftarUlang = dtJadwal.Select("kd_jalur_seleksi = 'daftar_ulang_rev_internal' ").CopyToDataTable();
                    if (dtDaftarUlang.Rows[0]["sts_jadwal"].ToString() == "1" && ViewState["eligible"].ToString() == "1")
                    {
                        if (ViewState["is_sdh_dikirim"] == null || ViewState["is_sdh_dikirim"].ToString() == "0") // belum dikirim
                        {
                            ViewState["jadwal_daftar_ulang"] = "1";
                            lbDaftar.Visible = true;
                        }
                    }
                    else
                    {
                        lblDaftar.Visible = true;
                        lblDaftar.ToolTip = "Bukan dalam jadwal pendaftaran/anda belum terdaftar sebagai reviewer internal.";
                    }
                }
            }
            else
            {
                lblDaftar.Visible = true;
                lblDaftar.ToolTip = "Bukan dalam jadwal pendaftaran.";
            }
        }


        private void tampilkanIsianPendaftaran()
        {
            objLogin = (Models.login)Session["objLogin"];
            panelKelengkapan.Visible = false;
            lstKepakaran.Items.Clear();
            lblBidangKepakaran.Text = "";
            string p_kd_kategori_reviewer = "3"; // reviewer internal
            DataTable dtDaftar = new DataTable();
            objPersyaratanUmum.getDataPendaftaranReviewerPenelitian(ref dtDaftar,
                objLogin.idPersonal, ddlTahunUsulan.SelectedValue, p_kd_kategori_reviewer);
            if (dtDaftar.Rows.Count > 0)  // Sudah mendaftar
            {
                panelKelengkapan.Visible = true;
                mvDaftar.SetActiveView(vEdit);
                //cekStsDok();
                string sts_pengiriman_pendaftaran = "0";
                for (int a = 0; a < dtDaftar.Rows.Count; a++)
                {
                    if (a == dtDaftar.Rows.Count - 1)
                        lblBidangKepakaran.Text += " - " + dtDaftar.Rows[a]["bidang_kepakaran"].ToString();
                    else
                        lblBidangKepakaran.Text += " - " + dtDaftar.Rows[a]["bidang_kepakaran"].ToString() + "<br>";

                    lstKepakaran.Items.Add(new ListItem(dtDaftar.Rows[a]["bidang_kepakaran"].ToString(),
                        dtDaftar.Rows[a]["id_bidang_kepakaran"].ToString()));
                    ViewState["id_pendaftaran"] = dtDaftar.Rows[a]["id_pendaftaran"].ToString();
                    ViewState["sts_perny_pakta_integritas"] = dtDaftar.Rows[a]["sts_pernyataan_pakta_integritas"].ToString();
                    ViewState["sts_perny_kode_etik"] = dtDaftar.Rows[a]["sts_pernyataan_kode_etik"].ToString();
                    sts_pengiriman_pendaftaran = dtDaftar.Rows[a]["sts_pengiriman_pendaftaran"].ToString();
                    ViewState["sts_pengiriman_pendaftaran"] = sts_pengiriman_pendaftaran;
                }

                lbHapusKepakaran.Visible = true;
                string stsPPintegritas = ViewState["sts_perny_pakta_integritas"].ToString();
                string stsPKdEtik = ViewState["sts_perny_kode_etik"].ToString();
                ktDokPendukung.setIdPendaftaran(ViewState["id_pendaftaran"].ToString(),
                    ViewState["sts_perny_pakta_integritas"].ToString(),
                    ViewState["sts_perny_kode_etik"].ToString()
                    );


                string stsDokumen = cekStsDok();
                string[] arrStsDok = stsDokumen.Split(new char[] { ',' });
                if (stsPPintegritas == "1" && stsPKdEtik == "1" && arrStsDok[0] == "1")
                {
                    lblHeaderInfoPendaftaran.ForeColor = System.Drawing.Color.Green;
                    lblInfoPendaftaran.ForeColor = System.Drawing.Color.Red;
                    //lblHeaderInfoPendaftaran.Text = "Isian pendaftaran telah lengkap.";
                    //lblInfoPendaftaran.Text = "Silakan klik \"Kirim Pendaftaran.\"";
                    cekStatusPengiriman(sts_pengiriman_pendaftaran);
                    //lblInfoPendaftaran.Text = "";
                    //lbKirimPendaftaran.Visible = true;
                }
                else
                {
                    lblHeaderInfoPendaftaran.ForeColor = System.Drawing.Color.Red;
                    lblInfoPendaftaran.ForeColor = System.Drawing.Color.Black;
                    lblHeaderInfoPendaftaran.Text = "Isian pendaftaran belum lengkap.";
                    lblInfoPendaftaran.Text = "";

                    if (arrStsDok[0] != "1")
                    {
                        lblInfoPendaftaran.Text += " - Dokumen Motivasi sebagai reviewer belum diunggah.<br>";
                    }
                    if (stsPPintegritas != "1")
                    {
                        lblInfoPendaftaran.Text += " - Pakta integritas belum disetujui.<br>";
                    }
                    if (stsPKdEtik != "1")
                    {
                        lblInfoPendaftaran.Text += " - Pernyataan mematuhi Kode Etik dan Kesanggupan Melaksanakan Tugas belum disetujui.<br>";
                    }
                    lblInfoPendaftaran.Text += "<br /> *Klik \"EDIT\" pada \"Dokumen pendukung\" untuk melengkapi.<br>";
                    lbKirimPendaftaran.Visible = false;
                }
            }
            else // Belum mendaftar
            {
                mvDaftar.SetActiveView(vDaftar);
                lbHapusKepakaran.Visible = false;
            }
        }



        private void cekStatusPengiriman(string sts_pengiriman_pendaftaran)
        {
            bool isEditVisible = true;
            if (sts_pengiriman_pendaftaran == "1")
            {
                isEditVisible = false;
            }

            lbEditBidKepakaran.Visible = isEditVisible;
            lbEdit.Visible = isEditVisible;

            lblHeaderInfoPendaftaran.ForeColor = System.Drawing.Color.Green;
            lblInfoPendaftaran.ForeColor = System.Drawing.Color.Red;
            lblHeaderInfoPendaftaran.Text = "Isian pendaftaran telah lengkap.";
            if (isEditVisible)
            {
                lblInfoPendaftaran.Text = "Silakan klik \"Kirim Pendaftaran.\"";
                ViewState["is_sdh_dikirim"] = "0";
            }
            else
            {
                lblInfoPendaftaran.Text = "Sedang menunggu proses seleksi.";
                ViewState["is_sdh_dikirim"] = "1";
            }
            lbKirimPendaftaranModal.Visible = isEditVisible;
            lbKirimPendaftaran.Visible = isEditVisible;
            lbBatalkan.Visible = isEditVisible;
            ktSyaratUmumRevPenelitian.setEditable(isEditVisible);

        }

        private string cekStsDok()
        {
            string stsDokumen = string.Empty;
            objLogin = (Models.login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_pendaftaran = ddlTahunUsulan.SelectedValue;


            lbUnduhPdfDokMotivasi.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokPaktaIntegritas.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokPernyataan.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokPengalaman.ForeColor = System.Drawing.Color.Gray;
            //lbUnduhPdfDokNarasumber.ForeColor = System.Drawing.Color.Gray;

            lbUnduhPdfDokSertifikat.ForeColor = System.Drawing.Color.Gray;

            var dtMotivasi = new DataTable();
            if (objPersyUmum.getDokumenUnggah(ref dtMotivasi, ViewState["id_pendaftaran"].ToString(), ktDokPendukung.idJenisDokMotivasi.ToString()))
            {
                if (dtMotivasi.Rows.Count > 0)
                {
                    if (dtMotivasi.Rows[0]["id_dokumen_reviewer"].ToString().Trim() != "")
                    {
                        lbUnduhPdfDokMotivasi.ForeColor = System.Drawing.Color.Red;
                        stsDokumen = "1,";
                    }
                    else
                    {
                        stsDokumen = "0,";
                    }
                }
                else
                {
                    stsDokumen = "0,";
                }
            }


            //DataTable dtDataSertifikat = new DataTable();
            //pnlSertifikatRevBaru.Visible = false;
            //objPersyUmum.getDataSetifikatPelatihanReviewer(ref dtDataSertifikat, id_personal);
            //if (dtDataSertifikat.Rows.Count > 0)
            //{
            //    pnlSertifikatRevBaru.Visible = true;
            var dtPengalaman = new DataTable();
            if (objPersyUmum.getDokumen(ref dtPengalaman, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokSertifikat))
            {
                if (dtPengalaman.Rows.Count > 0)
                {
                    stsDokumen += "1,1";
                    lbUnduhPdfDokSertifikat.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    stsDokumen += "1,0";
                }
            }
            //}
            //else
            //{
            //    stsDokumen += "0,0";
            //}


            //var dtPaktaIntegritas = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtPaktaIntegritas, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokPaktaIntegritas))
            //{
            //    if (dtPaktaIntegritas.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPaktaIntegritas.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            //var dtPernyataan = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtPernyataan, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokPernyataan))
            //{
            //    if (dtPernyataan.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPernyataan.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            //var dtPengalaman = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtPengalaman, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokPengalaman))
            //{
            //    if (dtPengalaman.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokPengalaman.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            //var dtNarasumber = new DataTable();
            //if (objPersyUmum.getDokumen(ref dtNarasumber, id_personal, thn_pendaftaran, ktDokPendukung.idJenisDokNarasumber))
            //{
            //    if (dtNarasumber.Rows.Count > 0)
            //    {
            //        lbUnduhPdfDokNarasumber.ForeColor = System.Drawing.Color.Red;
            //    }
            //}
            return stsDokumen;
        }


        protected void lbKembali_Click(object sender, EventArgs e)
        {
            mvDaftar.SetActiveView(vEdit);
            mvMain.SetActiveView(vPenelitian);
            pnlDaftarNEdit.Visible = true;
            //cekStsDok();
            tampilkanIsianPendaftaran();
        }

        protected void lbEdit_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(vDokumenPendukung);
            pnlDaftarNEdit.Visible = false;
            ktDokPendukung.setIdPendaftaran(ViewState["id_pendaftaran"].ToString(),
                ViewState["sts_perny_pakta_integritas"].ToString(),
                ViewState["sts_perny_kode_etik"].ToString()
                );
        }

        protected void lbUnduhPdfDokMotivasi_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokMotivasi.ToString());
            ktDokPendukung.unduhDokMotivasi();
        }

        protected void lbUnduhPdfDokMotivasi2_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokMotivasi.ToString());
            ktDokPendukung.unduhDokMotivasi();
        }

        protected void lbUnduhPdfDokPaktaIntegritas_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokPaktaIntegritas.ToString());
            ktDokPendukung.unduhDokPaktaIntegritas();
        }

        protected void lbUnduhPdfDokPaktaIntegritas2_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokPaktaIntegritas.ToString());
            ktDokPendukung.unduhDokPaktaIntegritas();
        }

        protected void lbUnduhPdfDokPernyataan_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokPernyataan.ToString());
            ktDokPendukung.unduhDokPernyataan();
        }

        protected void lbUnduhPdfDokPernyataan2_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokPernyataan.ToString());
            ktDokPendukung.unduhDokPernyataan();
        }

        protected void lbUnduhPdfDokSertifikat_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokSertifikat.ToString());
            ktDokPendukung.UnduhDokSertifikat();
        }

        protected void lbUnduhPdfDokSertifikat2_Click(object sender, EventArgs e)
        {
            ktDokPendukung.cekDokumen(ktDokPendukung.idJenisDokSertifikat.ToString());
            ktDokPendukung.UnduhDokSertifikat();
        }

        protected void editDokSeminar(object sender, EventArgs e)
        {
            ktkaryaSeniMonumental.init(ddlTahunUsulan.SelectedValue);
            mvMain.SetActiveView(vKaryaSeniMonumental);
            pnlDaftarNEdit.Visible = false;
        }

        protected void lbTambahKepakaran_Click(object sender, EventArgs e)
        {
            if (lstKepakaran.Items.Count < 3)
            {
                bool isExist = false;
                foreach (ListItem item in lstKepakaran.Items)
                {
                    if (item.Value == ddlKepakaran.SelectedValue)
                    {
                        isExist = true;
                    }
                }
                if (!isExist)
                {
                    lstKepakaran.Items.Add(new ListItem(ddlKepakaran.SelectedItem.Text, ddlKepakaran.SelectedValue));
                    lbHapusKepakaran.Visible = true;
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Bidang kepakaran " + ddlKepakaran.SelectedItem.Text + " sudah dipilih");
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Jumlah maksimal bidang kepakaran 3.");
            }
        }

        protected void lbHapusKepakaran_Click(object sender, EventArgs e)
        {
            lstKepakaran.Items.Clear();
            lbHapusKepakaran.Visible = false;
        }

        protected void lbDaftar_Click(object sender, EventArgs e)
        {
            if (lstKepakaran.Items.Count <= 0)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Bidang kepakaran belum diisi.");
                return;
            }

            objLogin = (Models.login)Session["objLogin"];
            string p_kd_kategori_reviewer = "3";
            string p_is_baru = "1";
            if (ViewState["kd_sts_aktif_reviewer_internal"].ToString() == "1")
                p_is_baru = "0";


            string strlstKepakaran = "";
            int jmlKepakaran = lstKepakaran.Items.Count;
            int c = 0;
            foreach (ListItem item in lstKepakaran.Items)
            {
                //if (item.Value == ddlKepakaran.SelectedValue)
                //{
                if (c == (jmlKepakaran - 1))
                    strlstKepakaran += "'" + item.Value + "'::uuid";
                else
                    strlstKepakaran += "'" + item.Value + "'::uuid,";
                //}
                c++;
            }

            if (objPersyUmum.insertPendaftaranReviewerPenelitian(Guid.Parse(objLogin.idPersonal), ddlTahunUsulan.SelectedValue,
                p_kd_kategori_reviewer, p_is_baru, strlstKepakaran))
            {
                //if (lbDaftar.Text.ToLower().Contains("daftar"))
                //{
                //    mvMain.SetActiveView(vKaryaSeniMonumental);
                //    pnlDaftarNEdit.Visible = false;
                //}
                //else if (lbDaftar.Text.ToLower().Contains("update"))
                //{
                mvDaftar.SetActiveView(vEdit);
                //}
                tampilkanIsianPendaftaran();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan/update pendaftaran reviewer berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Simpan pendaftaran reviewer gagal.");
            }

        }

        protected void lbEditBidKepakaran_Click1(object sender, EventArgs e)
        {
            mvDaftar.SetActiveView(vDaftar);
            lbDaftar.Text = "Update";
        }

        protected void lbHapusPendaftaranModal_Click(object sender, EventArgs e)
        {
            Guid id_pendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            string idDokMotivasi = ktDokPendukung.getDokMotivasi();
            string idSertifikat = ktDokPendukung.getDokSertifikat();
            ktkaryaSeniMonumental.hapusKaryaSeni();
            if (objPersyUmum.hapusPendaftaran(id_pendaftaran))
            {
                ktDokPendukung.hapusDokMotivasi(idDokMotivasi);
                ktDokPendukung.hapusDokSertifikat(idSertifikat);
                tampilkanIsianPendaftaran();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus pendaftaran reviewer berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus pendaftaran reviewer gagal.");
            }
        }
        protected void lbBatalkan_Click(object sender, EventArgs e)
        {
            lblModalTitle.Text = "Konfirmasi pembatalan pendaftaran";
            lblModalInfo.Text = "Apakah Anda yakin akan membatalkan usulan pendaftaran sebagai reviewer ini?<br />Data isian dan dokumen akan dihapus.";
            uiModal objModal = new uiModal();
            objModal.ShowModal(this.Page, "modalHapusPendaftaran");
            lbHapusPendaftaranModal.Visible = true;
            lbKirimPendaftaranModal.Visible = false;
        }

        protected void lbKirimPendaftaran_Click(object sender, EventArgs e)
        {

            lblModalTitle.Text = "Konfirmasi pengiriman pendaftaran";
            lblModalInfo.Text = "Apakah Anda yakin akan mengirim usulan pendaftaran sebagai reviewer ini?<br />Data isian dan dokumen tidak akan bisa diubah.";
            uiModal objModal = new uiModal();
            objModal.ShowModal(this.Page, "modalHapusPendaftaran");
            lbHapusPendaftaranModal.Visible = false;
            lbKirimPendaftaranModal.Visible = true;
        }

        protected void lbKirimPendaftaranFinal_Click(object sender, EventArgs e)
        {

            Guid id_pendaftaran = Guid.Parse(ViewState["id_pendaftaran"].ToString());
            //string idDokMotivasi = ktDokPendukung.getDokMotivasi();
            if (objPersyUmum.kirimPendaftaran(id_pendaftaran))
            {
                tampilkanIsianPendaftaran();
                noty.Notify(this.Page, uiNotify.NotifyType.info, "Informasi", "Kirim pendaftaran reviewer berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kirim pendaftaran reviewer gagal.");
            }
        }

        protected void ddlTahunUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            tampilkanIsianPendaftaran();
            setJadwal();
        }
    }
}