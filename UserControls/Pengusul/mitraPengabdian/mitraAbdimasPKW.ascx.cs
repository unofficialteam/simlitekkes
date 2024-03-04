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
    public partial class mitraAbdimasPKW : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.mitraAbdimas objMitra = new Models.Pengusul.mitraAbdimas();
        uiNotify noty = new uiNotify();
        kontrolUnggah ktUnggah = new kontrolUnggah();
        uiModal uiMdl = new uiModal();
        manipulasiData objManipData = new manipulasiData();
        public event EventHandler OnChildEventOccurs;

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
                isigvKelMasyarakat();
                isiMitraPengabdianPerSkema();
                isiPelaksanaPTMitra();
                isigvMitraPelaksanaPemdaKota();
            }
        }

        public void setThnUsulan(string thn_usulan)
        {
            ViewState["thn_usulan"] = thn_usulan;
        }

        public void InitData(Guid idUsulan, Guid idUsulanKegiatan)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            isigvKelMasyarakat();
            isiMitraPengabdianPerSkema();
            isiPelaksanaPTMitra();
            isigvMitraPelaksanaPemdaKota();
        }

        public void isigvKelMasyarakat()
        {
            var listMitra = new List<UMKM>();
            if (!objMitra.listMitraSasaranPPPUD(ref listMitra, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
                return;
            }

            try
            {
                gvKelMasyarakat.DataSource = listMitra;
                gvKelMasyarakat.DataBind();

                lblKelMasyarakat.Text = listMitra.Count.ToString();                
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
                lblLamaUsulan.Text = dtMitra.Rows[0]["lama_kegiatan"].ToString();
                ViewState["lama_kegiatan"] = lblLamaUsulan.Text;
                lblUrutanUsulan.Text = dtMitra.Rows[0]["urutan_thn_usulan"].ToString();
                ViewState["mitraPelaksana"] = dtMitra.Rows[0]["mitra_pelaksana"].ToString();
                ViewState["mitraSasaran"] = dtMitra.Rows[0]["mitra_sasaran"].ToString();
                ViewState["idSkema"] = dtMitra.Rows[0]["id_skema"].ToString();
                ViewState["thn_usulan_kegiatan"] = dtMitra.Rows[0]["thn_usulan_kegiatan"].ToString();
            }
        }

        private void isiPelaksanaPTMitra()
        {
            var listPTMitra = new List<PTMitra>();
            if (!objMitra.listPTMitra(ref listPTMitra, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), 2))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
                return;
            }
            try
            {
                gvPTMitra.DataSource = listPTMitra;
                gvPTMitra.DataBind();

                lblJumlahPTMitra.Text = listPTMitra.Count.ToString();
                if (lblJumlahPTMitra.Text == "1")
                {
                    lbTambahPelaksanaPT.Enabled = false;
                    lbTambahPelaksanaPT.CssClass = "btn btn-default wave";
                }
                else
                {
                    lblJumlahPTMitra.Enabled = true;
                    lbTambahPelaksanaPT.CssClass = "btn btn-primary wave";
                }
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        public void isigvMitraPelaksanaPemdaKota()
        {
            var listMitra = new List<PemdaPemkot>();
            if (!objMitra.listMitraPelaksanaPPPUD(ref listMitra, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
                return;
            }

            try
            {
                //gvMitraPelaksanaPemdaKota.DataSource = listMitra;
                //gvMitraPelaksanaPemdaKota.DataBind();

                //lblJmlPemda.Text = listMitra.Count.ToString();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
        }

        public bool Simpan()
        {
            return false;
        }       

        protected void lbTambahPemdaKota_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraPelaksanaPemdaKota.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            mvMitra.SetActiveView(vEditPemda);
        }

        protected void lbSimpanEditPemdaKota_Click(object sender, EventArgs e)
        {
            if (mitraPelaksanaPemdaKota.Simpan())
            {
                isigvMitraPelaksanaPemdaKota();
                mvMitra.SetActiveView(vDaftarMitra);
            }
        }

        protected void lbBatalEditPemdaKota_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vDaftarMitra);
        }       

        protected void lbTambahPelaksanaPT_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraPelaksanaPTMitra.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            mvMitra.SetActiveView(vEditPT);
        }

        protected void lbSimpanPT_Click(object sender, EventArgs e)
        {
            if (mitraPelaksanaPTMitra.Simpan()) mvMitra.SetActiveView(vDaftarMitra);
            isiPelaksanaPTMitra();
        }

        protected void lbBatalPT_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vDaftarMitra);
        }

        protected void gvPTMitra_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id_mitra_abdimas = Guid.Parse(gvPTMitra.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            
            mitraPelaksanaPTMitra.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
            mvMitra.SetActiveView(vEditPT);
        }

        protected void gvPTMitra_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdMitraAbdimas"] = Guid.Parse(gvPTMitra.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            lblNamaMitraPelaksana.Text = gvPTMitra.DataKeys[e.RowIndex]["NamaPimpinanInstitusi"].ToString();
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvPTMitra_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update" || e.CommandName == "Delete") return;

            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvPTMitra.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unggahDokMitraPengabdian")
            {
                string dirFile = "~/fileUpload/Mitra"; //" + ViewState["thn_usulan_kegiatan"].ToString();              
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
                ktUnggah.max_size = 1024 * 1024; // 500KB
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah dokumen Mitra Pelaksana berhasil";
                ktUnggah.failed_info = "Unggah dokumen Mitra Pelaksana gagal";
                Session.Add("ktUnggah", ktUnggah);
                uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");

            }

            else if (e.CommandName == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvPTMitra.DataKeys[rowIndex]["NamaPimpinanInstitusi"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "MitraPelaksana_" + namaFile.Replace(" ", "_") + ".pdf";
                namaFile = objManipData.removeUnicode(namaFile);
                string dirFile = "~/fileUpload/Mitra/" + ViewState["thn_usulan_kegiatan"].ToString() + "/";
                string filePath = string.Format(dirFile + "{0}.pdf", idMitra);
                
                if (File.Exists(Server.MapPath(filePath)))
                {
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = dirFile, //"~/fileUpload/Mitra/" + ViewState["thn_usulan_kegiatan"].ToString(),//PATH_UNGGAH_BERKAS,
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
            var idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            //var idMitraAbdimas = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            //mitraPelaksanaPemdaKota.InitData(idUsulanKegiatan, idMitraAbdimas);

            mvMitra.SetActiveView(vEditPemda);
        }

        protected void gvMitraPelaksanaPemdaKota_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ViewState["IdMitraAbdimas"] = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            //lblNamaMitraPelaksana.Text = gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["NamaPimpinanInstitusi"].ToString();
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvMitraPelaksanaPemdaKota_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update" || e.CommandName == "Delete") return;

            //int rowIndex = int.Parse(e.CommandArgument.ToString());
            //Guid idMitra = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());

            //ViewState["idMitra"] = idMitra.ToString();
            //if (e.CommandName == "unggahDokMitraPengabdian")
            //{
            //    string dirFile = "~/fileUpload/Mitra";
            //    if (!Directory.Exists(Server.MapPath(dirFile)))
            //    {
            //        Directory.CreateDirectory(Server.MapPath(dirFile));
            //    }
            //    dirFile += "/" + ViewState["thn_usulan_kegiatan"].ToString();
            //    if (!Directory.Exists(Server.MapPath(dirFile)))
            //    {
            //        Directory.CreateDirectory(Server.MapPath(dirFile));
            //    }

            //    ktUnggah.path2save = dirFile + string.Format("/{0}.pdf", idMitra);
            //    ktUnggah.max_size = 1024 * 1024; // 500KB
            //    ktUnggah.alllowed_ext = "pdf;PDF";
            //    ktUnggah.success_info = "Unggah dokumen Mitra Pelaksana berhasil";
            //    ktUnggah.failed_info = "Unggah dokumen Mitra Pelaksana gagal";
            //    Session.Add("ktUnggah", ktUnggah);
            //    uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");

            //}

            //else if (e.CommandName == "unduhDokumenMitraPelaksana")
            //{
            //    string namaMitra = gvMitraPelaksanaPemdaKota.DataKeys[rowIndex]["NamaOrganisasiInstitusi"].ToString();
            //    string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
            //    namaFile = "MitraPelaksana_" + namaFile.Replace(" ", "_") + ".pdf";
            //    namaFile = objManipData.removeUnicode(namaFile);
            //    string dirFile = "~/fileUpload/Mitra/" + ViewState["thn_usulan_kegiatan"].ToString() + "/";
            //    string filePath = string.Format(dirFile + "{0}.pdf", idMitra);

            //    if (File.Exists(Server.MapPath(filePath)))
            //    {
            //        var atributUnduh = new AtributUnduh
            //        {
            //            FolderUnduh = dirFile,
            //            NamaBerkas = idMitra + ".pdf",
            //            NamaBerkasdiUnduh = namaFile
            //        };
            //        Session["AtributUnduh"] = atributUnduh;

            //        var unduhForm = "helper/unduhFile.aspx";
            //        Response.Redirect(unduhForm);
            //    }
            //    else
            //    {
            //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
            //            "Berkas tidak dapat ditemukan. Silahkan hubungi Administrator");
            //        return;
            //    }

            //}
        }

        protected void lbKelMasyarakat_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraSasaranKelMasyarakat.InitData(idUsulanKegiatan, Guid.Empty);
            mvMitra.SetActiveView(vEditKelMasyarakat);
        }

        protected void lbSimpanEditKelMasyarakat_Click(object sender, EventArgs e)
        {
            if (mitraSasaranKelMasyarakat.Simpan())
            {
                isigvKelMasyarakat();
                mvMitra.SetActiveView(vDaftarMitra);
            }
        }

        protected void lbBatalEditKelMasyarakat_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vDaftarMitra);
        }

        protected void gvKelMasyarakat_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string row = e.RowIndex.ToString();
            Guid id_mitra_abdimas = Guid.Parse(gvKelMasyarakat.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());

            mitraSasaranKelMasyarakat.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
            mvMitra.SetActiveView(vEditKelMasyarakat);
        }

        protected void gvKelMasyarakat_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdMitraAbdimas"] = Guid.Parse(gvKelMasyarakat.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            lblNamaMitraPelaksana.Text = gvKelMasyarakat.DataKeys[e.RowIndex]["NamaUMKM"].ToString();
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {            
            if (objMitra.hapusMitra(Guid.Parse(ViewState["IdMitraAbdimas"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data mitra berhasil");
                string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf", ViewState["thn_usulan_kegiatan"].ToString(),                            
                    ViewState["IdMitraAbdimas"].ToString());

                if (File.Exists(Server.MapPath(filePath)))
                {
                    File.Delete(Server.MapPath(filePath));
                }
                isiPelaksanaPTMitra();
                isigvMitraPelaksanaPemdaKota();
                isigvKelMasyarakat();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objMitra.errorMessage);
            }
        }

        protected void gvKelMasyarakat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update" || e.CommandName == "Delete") return;

            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (e.CommandName == "unggahDokMitraPengabdian")
            {
                string dirFile = "~/fileUpload/Mitra";           
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
                ktUnggah.max_size = 1024 * 1024; // 500KB
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah dokumen Mitra Pelaksana berhasil";
                ktUnggah.failed_info = "Unggah dokumen Mitra Pelaksana gagal";
                Session.Add("ktUnggah", ktUnggah);
                uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");

            }

            else if (e.CommandName == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvKelMasyarakat.DataKeys[rowIndex]["NamaUMKM"].ToString();
                string namaFile = (namaMitra.Length > 30) ? namaMitra.Substring(0, 30) : namaMitra;
                namaFile = "MitraPelaksana_" + namaFile.Replace(" ", "_") + ".pdf";
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