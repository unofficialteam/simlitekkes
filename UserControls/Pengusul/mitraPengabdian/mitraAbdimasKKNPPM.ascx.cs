using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;
using System.IO;
using simlitekkes.Core;
using simlitekkes.Helper;
using System.Globalization;
using simlitekkes.Models.Pengusul.Mitra;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraAbdimasKKNPPM : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.mitraAbdimas objMitra = new Models.Pengusul.mitraAbdimas();
        uiNotify noty = new uiNotify();
        kontrolUnggah ktUnggah = new kontrolUnggah();
        uiModal uiMdl = new uiModal();
        manipulasiData objManipData = new manipulasiData();
        public event EventHandler OnChildEventOccurs;
        
        const int KD_KATEGORI_MITRA_SASARAN = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            kontrolUnggah.OnChildEventOccurs += new EventHandler(unggahDokMitra_OnChildEventSuccess);
        }

        void unggahDokMitra_OnChildEventSuccess(object sender, EventArgs e)
        {
            if (!objMitra.updateStsDokMitra(Guid.Parse(ViewState["idMitra"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objMitra.errorMessage);
            }
            else
            {
                isigvMitraPelaksanaPemdaKota();
                isiMitraPengabdianPerSkema();
                isigvMitraSasaranUMKM();
            }
        }

        public void InitData(Guid idUsulan, Guid idUsulanKegiatan)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            isigvMitraPelaksanaPemdaKota();
            isiMitraPengabdianPerSkema();
            isigvMitraSasaranUMKM();
        }

        public void setThnUsulan(string thn_usulan)
        {
            ViewState["thn_usulan"] = thn_usulan;
        }

        public void isigvMitraPelaksanaPemdaKota()
        {
            var listMitraPemdakota = new List<PemdaPemkot>();
            if (!objMitra.listMitraPelaksanaPPPUD(ref listMitraPemdakota, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
                return;
            }

            try
            {
                gvMitraPelaksanaPemdaKota.DataSource = listMitraPemdakota;
                gvMitraPelaksanaPemdaKota.DataBind();
                lblJmlPemda.Text = listMitraPemdakota.Count.ToString();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        public void isigvMitraSasaranUMKM()
        {
            var listSasaranUMKM = new List<UMKM>();
            if (!objMitra.listMitraSasaranKKNPPM(ref listSasaranUMKM, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
                return;
            }

            try
            {
                gvMitraSasaranUMKM.DataSource = listSasaranUMKM;
                gvMitraSasaranUMKM.DataBind();
                lblJmlUMKM.Text = listSasaranUMKM.Count.ToString();
                if (listSasaranUMKM.Count >= 1)
                {
                    lbTambahUMKM.Enabled = false;
                    lbTambahUMKM.CssClass = "btn btn-default wave";
                }
                else
                {
                    lbTambahUMKM.Enabled = true;
                    lbTambahUMKM.CssClass = "btn btn-primary wave";
                }
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }
        
        public void isiMitraPengabdianPerSkema()
        {
            DataTable dtMitra = new DataTable();
            objMitra.getMitraPengabdianPerSkema(ref dtMitra, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            if (dtMitra.Rows.Count > 0)
            {
                lblSkema.Text = dtMitra.Rows[0]["nama_skema"].ToString();
                //lblLamaUsulan.Text = dtMitra.Rows[0]["lama_kegiatan"].ToString();
                //ViewState["lama_kegiatan"] = lblLamaUsulan.Text;
                //lblUrutanUsulan.Text = dtMitra.Rows[0]["urutan_thn_usulan"].ToString();
                //ViewState["mitraPelaksana"] = dtMitra.Rows[0]["mitra_pelaksana"].ToString();
                ViewState["mitraSasaran"] = dtMitra.Rows[0]["mitra_sasaran"].ToString();
                ViewState["idSkema"] = dtMitra.Rows[0]["id_skema"].ToString();
                ViewState["thn_usulan_kegiatan"] = dtMitra.Rows[0]["thn_usulan_kegiatan"].ToString();
            }
        }

        public bool Simpan()
        {
            return false;
        }

        //protected void lbTambahPTPelaksana_Click(object sender, EventArgs e)
        //{
        //    Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
        //    mitraPelaksanaPT.InitData(idUsulanKegiatan, Guid.Empty);
        //    ViewState["id_usulan_kegiatan"].ToString();
        //    mvMitra.SetActiveView(vEditPT);
        //}

        protected void lbTambahPemdaKota_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraPelaksanaPemdaKota.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            mvMitra.SetActiveView(vEditPemda);
        }

        protected void lbTambahUMKM_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraSasaranUMKM.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            mvMitra.SetActiveView(vEditUMKM);
        }

        //protected void lbSimpanEditPT_Click(object sender, EventArgs e)
        //{
        //    if (mitraPelaksanaPT.Simpan()) mvMitra.SetActiveView(vDaftarMitra);
        //    isiMitra(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
        //}

        //protected void lbBatalEditPT_Click(object sender, EventArgs e)
        //{
        //    mvMitra.SetActiveView(vDaftarMitra);
        //}

        protected void lbSimpanEditPemdaKota_Click(object sender, EventArgs e)
        {
            if (mitraPelaksanaPemdaKota.Simpan()) mvMitra.SetActiveView(vDaftarMitra);
            isigvMitraPelaksanaPemdaKota();
        }

        protected void lbBatalEditPemdaKota_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vDaftarMitra);
        }

        protected void lbSimpanEditUMKM_Click(object sender, EventArgs e)
        {
            if (mitraSasaranUMKM.Simpan())
            {
                isigvMitraSasaranUMKM();
                mvMitra.SetActiveView(vDaftarMitra);
            }
        }

        protected void lbBatalEditUMKM_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vDaftarMitra);
        }

        //protected void gvMitraPelaksanaPengabdiana_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    Guid id_mitra_abdimas = Guid.Parse(gvMitraPelaksanaPengabdian.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());

        //    mitraPelaksanaPT.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
        //    mvMitra.SetActiveView(vEditPT);
        //}

        //protected void gvMitraPelaksanaPengabdian_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    ViewState["IdMitraAbdimas"] = Guid.Parse(gvMitraPelaksanaPengabdian.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
        //    lblNamaMitraPelaksana.Text = gvMitraPelaksanaPengabdian.DataKeys[e.RowIndex]["NamaPimpinanInstitusi"].ToString();
        //    uiMdl.ShowModal(this.Page, "modalHapus");
        //}

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (objMitra.hapusMitra(Guid.Parse(ViewState["IdMitraAbdimas"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data mitra berhasil");

                if (ViewState["jenisHapus"].ToString() == "1")
                {
                    isigvMitraPelaksanaPemdaKota();
                }
                else
                {
                    isigvMitraSasaranUMKM();
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objMitra.errorMessage);
            }
        }

        protected void gvMitraSasaranUMKM_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id_mitra_abdimas = Guid.Parse(gvMitraSasaranUMKM.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());

            mitraSasaranUMKM.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
            mvMitra.SetActiveView(vEditUMKM);
        }

        protected void gvMitraSasaranUMKM_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdMitraAbdimas"] = Guid.Parse(gvMitraSasaranUMKM.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            lblNamaMitra.Text = gvMitraSasaranUMKM.DataKeys[e.RowIndex]["NamaPimpinan"].ToString();
            ViewState["jenisHapus"] = "2";
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvMitraSasaranUMKM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraSasaranUMKM.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());
            ViewState["idMitra"] = idMitra.ToString();

            if (cmd == "unggahDokMitraUMKM")
            {
                string dirFile = "~/fileUpload/Mitra";
                if (int.Parse(ViewState["thn_usulan"].ToString()) >= 2019)
                {
                    dirFile = "~/fileUpload/Mitra";
                }

                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }
                dirFile += "/" + ViewState["thn_usulan_kegiatan"].ToString();
                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }

                ktUnggah.path2save = dirFile + string.Format("/{0}.pdf", idMitra);
                ktUnggah.max_size = 1024 * 1024;
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah dokumen Mitra berhasil";
                ktUnggah.failed_info = "Unggah dokumen Mitra gagal";
                Session.Add("ktUnggah", ktUnggah);
                uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");

            }
            else if (cmd == "unduhDokumenMitraUMKM")
            {
                string namaMitra = gvMitraSasaranUMKM.DataKeys[rowIndex]["NamaPimpinan"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "Mitra" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string dirFile = "~/fileUpload/Mitra/" + ViewState["thn_usulan_kegiatan"].ToString() + "/";

                if (int.Parse(ViewState["thn_usulan"].ToString()) >= 2019)
                {
                    dirFile = dirFile.Replace("fileUpload/", "fileUpload/");
                }

                string filePath = string.Format(dirFile + "{0}.pdf", idMitra);

                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = dirFile, //"~/fileUpload/Mitra/" + ViewState["thn_usulan_kegiatan"].ToString(),
                        NamaBerkas = idMitra + ".pdf",
                        NamaBerkasdiUnduh = namaFile
                    };
                    Session["AtributUnduh"] = atributUnduh;

                    var unduhForm = "helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
                    return;
                }
            }
        }

        protected void gvMitraPelaksanaPemdaKota_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id_mitra_abdimas = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            mitraPelaksanaPemdaKota.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
            mvMitra.SetActiveView(vEditPemda);
        }

        protected void gvMitraPelaksanaPemdaKota_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdMitraAbdimas"] = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            lblNamaMitra.Text = gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["NamaPimpinanInstitusi"].ToString();
            ViewState["jenisHapus"] = "1";
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvMitraPelaksanaPemdaKota_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());
            ViewState["idMitra"] = idMitra.ToString();

            if (cmd == "unggahDokMitraPemdaKota")
            {
                string dirFile = "~/fileUpload/Mitra";
                if (int.Parse(ViewState["thn_usulan"].ToString()) >= 2019)
                {
                    dirFile = "~/fileUpload/Mitra";
                }
                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }
                dirFile += "/" + ViewState["thn_usulan_kegiatan"].ToString();
                if (!Directory.Exists(Server.MapPath(dirFile)))
                {
                    Directory.CreateDirectory(Server.MapPath(dirFile));
                }

                ktUnggah.path2save = dirFile + string.Format("/{0}.pdf", idMitra);
                ktUnggah.max_size = 1024 * 1024;
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah dokumen Mitra berhasil";
                ktUnggah.failed_info = "Unggah dokumen Mitra gagal";
                Session.Add("ktUnggah", ktUnggah);
                uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");

            }
            else if (cmd == "unduhDokumenMitraPemdaKota")
            {
                string namaMitra = gvMitraPelaksanaPemdaKota.DataKeys[rowIndex]["NamaPimpinanInstitusi"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "Mitra" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string dirFile = "~/fileUpload/Mitra/" + ViewState["thn_usulan_kegiatan"].ToString() + "/";
                string filePath = string.Format(dirFile + "{0}.pdf", idMitra);

                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = dirFile,
                        NamaBerkas = idMitra + ".pdf",
                        NamaBerkasdiUnduh = namaFile
                    };
                    Session["AtributUnduh"] = atributUnduh;

                    var unduhForm = "helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                        "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
                    return;
                }
            }
        }
    }
}