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
    public partial class mitraAbdimasPPUPIK : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.mitraAbdimas objMitra = new Models.Pengusul.mitraAbdimas();
        uiNotify noty = new uiNotify();
        kontrolUnggah ktUnggah = new kontrolUnggah();
        uiModal uiMdl = new uiModal();
        manipulasiData objManipData = new manipulasiData();
        public event EventHandler OnChildEventOccurs;

        const int ID_TIPE_PELAKSANA_PT = 1;

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
                isigvMitraPelaksanaPT();
                isigvMitraPelaksanaPemdaKota();
                isiMitraPengabdianPerSkema();
            }
        }

        public void InitData(Guid idUsulan, Guid idUsulanKegiatan)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            isiMitraPengabdianPerSkema();
            isigvMitraPelaksanaPT();
            isigvMitraPelaksanaPemdaKota();
        }

        public void setThnUsulan(string thn_usulan)
        {
            ViewState["thn_usulan_kegiatan"] = thn_usulan;
        }

        public void isigvMitraPelaksanaPT()
        {
            var listMitraPelaksanaPT = new List<PTPelaksana>();
            if (!objMitra.listPTPelaksana(ref listMitraPelaksanaPT, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), ID_TIPE_PELAKSANA_PT))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
                return;
            }

            try
            {
                gvMitraPelaksanaPT.DataSource = listMitraPelaksanaPT;
                gvMitraPelaksanaPT.DataBind();
                lblJumlahPTPelaksana.Text = listMitraPelaksanaPT.Count.ToString();

                if (listMitraPelaksanaPT.Count >= 1)
                {
                    lbTambahPTPelaksana.Enabled = false;
                    lbTambahPTPelaksana.CssClass = "btn btn-default wave";
                }
                else
                {
                    lbTambahPTPelaksana.Enabled = true;
                    lbTambahPTPelaksana.CssClass = "btn btn-primary wave";
                }
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
            }
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

        public void isiMitraPengabdianPerSkema()
        {
            DataTable dtMitra = new DataTable();
            objMitra.getMitraPengabdianPerSkema(ref dtMitra, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()));
            if (dtMitra.Rows.Count > 0)
            {
                lblSkema.Text = dtMitra.Rows[0]["nama_skema"].ToString();
                ViewState["mitraSasaran"] = dtMitra.Rows[0]["mitra_sasaran"].ToString();
                ViewState["idSkema"] = dtMitra.Rows[0]["id_skema"].ToString();
                ViewState["thn_usulan_kegiatan"] = dtMitra.Rows[0]["thn_usulan_kegiatan"].ToString();
            }
        }

        public bool Simpan()
        {
            return false;
        }

        protected void lbTambahPTPelaksana_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraPelPT.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            mvMitraPPUPIK.SetActiveView(vEditPelPT);
        }

        protected void lbTambahPemdaKota_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraPelPemdaKota.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            mvMitraPPUPIK.SetActiveView(vEditPelPemda);
        }

        protected void lbSimpanEditPT_Click(object sender, EventArgs e)
        {
            if (mitraPelPT.Simpan()) mvMitraPPUPIK.SetActiveView(vDaftarMitra);
            isigvMitraPelaksanaPT();
        }

        protected void lbBatalEditPT_Click(object sender, EventArgs e)
        {
            mvMitraPPUPIK.SetActiveView(vDaftarMitra);
        }

        protected void lbSimpanEditPemdaKota_Click(object sender, EventArgs e)
        {
            if (mitraPelPemdaKota.Simpan()) mvMitraPPUPIK.SetActiveView(vDaftarMitra);
            isigvMitraPelaksanaPemdaKota();
        }

        protected void lbBatalEditPemdaKota_Click(object sender, EventArgs e)
        {
            mvMitraPPUPIK.SetActiveView(vDaftarMitra);
        }

        protected void gvMitraPelaksanaPT_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id_mitra_abdimas = Guid.Parse(gvMitraPelaksanaPT.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            mitraPelPT.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
            mvMitraPPUPIK.SetActiveView(vEditPelPT);
        }

        protected void gvMitraPelaksanaPT_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdMitraAbdimas"] = Guid.Parse(gvMitraPelaksanaPT.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            lblNamaMitraPelaksana.Text = gvMitraPelaksanaPT.DataKeys[e.RowIndex]["NamaPimpinanInstitusi"].ToString();
            ViewState["jenisHapus"] = "1";
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvMitraPelaksanaPT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraPelaksanaPT.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());
            ViewState["idMitra"] = idMitra.ToString();

            if (cmd == "unggahDokMitraPelaksanaPT")
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
                ktUnggah.max_size = 1024 * 1024;
                ktUnggah.alllowed_ext = "pdf;PDF";
                ktUnggah.success_info = "Unggah dokumen Mitra berhasil";
                ktUnggah.failed_info = "Unggah dokumen Mitra gagal";
                Session.Add("ktUnggah", ktUnggah);
                uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");

            }
            else if (cmd == "unduhDokumenMitraPelaksanaPT")
            {
                string namaMitra = gvMitraPelaksanaPT.DataKeys[rowIndex]["NamaPimpinanInstitusi"].ToString();
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

        protected void gvMitraPelaksanaPemdaKota_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id_mitra_abdimas = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            mitraPelPemdaKota.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
            mvMitraPPUPIK.SetActiveView(vEditPelPemda);
        }

        protected void gvMitraPelaksanaPemdaKota_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdMitraAbdimas"] = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            lblNamaMitraPelaksana.Text = gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["NamaPimpinanInstitusi"].ToString();
            ViewState["jenisHapus"] = "2";
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
                string dirFile = "~/fileUpload/mitra/" + ViewState["thn_usulan_kegiatan"].ToString() + "/";
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

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (objMitra.hapusMitra(Guid.Parse(ViewState["IdMitraAbdimas"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data mitra berhasil");

                if (ViewState["jenisHapus"].ToString() == "1")
                {
                    isigvMitraPelaksanaPT();
                }
                else
                {
                    isigvMitraPelaksanaPemdaKota();
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objMitra.errorMessage);
            }
        }
    }
}