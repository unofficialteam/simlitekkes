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
    public partial class mitraAbdimasPPK : System.Web.UI.UserControl
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
                isiMitraPengabdianPerSkema();
                isigvMitraPelaksanaPT();
                isigvMitraPelaksanaPemdaKota();
            }
        }

        protected void selesai_Click(object sender, EventArgs e)
        {

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
            ViewState["thn_usulan"] = thn_usulan;
        }

        public void isigvMitraPelaksanaPT()
        {
            var listPT = new List<PTPelaksana>();
            if (!objMitra.listPTPelaksana(ref listPT, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), ID_TIPE_PELAKSANA_PT))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objMitra.errorMessage);
                return;
            }

            try
            {
                gvMitraPelaksanaPengabdian.DataSource = listPT;
                gvMitraPelaksanaPengabdian.DataBind();
                lblJumlahPTPelaksana.Text = listPT.Count.ToString();
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
                gvMitraPelaksanaPemdaKota.DataSource = listMitra;
                gvMitraPelaksanaPemdaKota.DataBind();
                lblJmlPemda.Text = listMitra.Count.ToString();
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

        protected void lbTambahPTPelaksana_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraPelaksanaPT.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            mvMitra.SetActiveView(vEditPemda);
        }

        protected void lbBatalEditPT_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vDaftarMitra);
        }

        protected void lbSimpanEditPT_Click(object sender, EventArgs e)
        {
            if (mitraPelaksanaPT.Simpan()) mvMitra.SetActiveView(vDaftarMitra);
            isigvMitraPelaksanaPT();
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

        protected void lbTambahPemdaKota_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraPelaksanaPemdaKota.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            mvMitra.SetActiveView(vEditPemda);
        }

        protected void lbSimpanEditPemdaKota_Click(object sender, EventArgs e)
        {
            if (mitraPelaksanaPemdaKota.Simpan()) mvMitra.SetActiveView(vDaftarMitra);
            isigvMitraPelaksanaPemdaKota();
        }

        protected void lbBatalEditPemdaKota_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vDaftarMitra);
        }

        protected void lbBatalEditPemdaCSR_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vDaftarMitra);
        }

        protected void gvMitraPelaksanaPengabdian_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id_mitra_abdimas = Guid.Parse(gvMitraPelaksanaPengabdian.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());

            mitraPelaksanaPT.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
            mvMitra.SetActiveView(vEditPT);
        }

        protected void gvMitraPelaksanaPengabdian_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdMitraAbdimas"] = Guid.Parse(gvMitraPelaksanaPengabdian.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            lblNamaMitraPelaksana.Text = gvMitraPelaksanaPengabdian.DataKeys[e.RowIndex]["NamaPimpinanInstitusi"].ToString();
            ViewState["isPelaksanaPt"] = true;
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

                if (bool.Parse(ViewState["isPelaksanaPt"].ToString()))
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

        protected void gvMitraPelaksanaPengabdian_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraPelaksanaPengabdian.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());
            ViewState["idMitra"] = idMitra.ToString();

            if (cmd == "unggahDokMitraPengabdian")
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
            else if (cmd == "unduhDokumenMitraPelaksana")
            {
                string namaMitra = gvMitraPelaksanaPengabdian.DataKeys[rowIndex]["NamaPimpinanInstitusi"].ToString();
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

        protected void gvMitraPelaksanaPemdaKota_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Guid id_mitra_abdimas = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());

            //mitraPelaksanaPemdaKota.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
            mvMitra.SetActiveView(vEditPemda);
        }

        protected void gvMitraPelaksanaPemdaKota_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ViewState["IdMitraAbdimas"] = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            //lblNamaMitraPelaksana.Text = gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["NamaPimpinanInstitusi"].ToString();
            ViewState["isPelaksanaPt"] = false;
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvMitraPelaksanaPemdaKota_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            //Guid idMitra = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());
            //ViewState["idMitra"] = idMitra.ToString();

            //if (cmd == "unggahDokMitraPemkot")
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
            //    ktUnggah.success_info = "Unggah dokumen Mitra berhasil";
            //    ktUnggah.failed_info = "Unggah dokumen Mitra gagal";
            //    Session.Add("ktUnggah", ktUnggah);
            //    uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");
            //}
            //else if (cmd == "unduhDokumenMitraPemkot")
            //{
            //    string namaMitra = gvMitraPelaksanaPemdaKota.DataKeys[rowIndex]["NamaPimpinanInstitusi"].ToString();
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
    }
}