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
    public partial class mitraAbdimasPPDM : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.mitraAbdimas objMitra = new Models.Pengusul.mitraAbdimas();
        uiNotify noty = new uiNotify();
        kontrolUnggah ktUnggah = new kontrolUnggah();
        uiModal uiMdl = new uiModal();
        manipulasiData objManipData = new manipulasiData();
        public event EventHandler OnChildEventOccurs;
        const int ID_TIPE_PELAKSANA_PT = 1;

        //const int ID_TIPE_PELAKSANA_PT = 1;
        const string ID_TIPE_PEMDA_PEMKOT = "3,4";

        protected void Page_Load(object sender, EventArgs e)
        {
            kontrolUnggah.OnChildEventOccurs += new EventHandler(unggahDokMitra_OnChildEventSuccess);
        }

        public static string ConvertDateTimeToDate(string dateTimeString, string dateTimeFormat, String langCulture)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(langCulture);
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(dateTimeString, out dt))
            {
                return dt.ToString(dateTimeFormat, culture);
            }
            return dateTimeString;
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
                isigvKelMasyarakat();
            }
        }

        public void InitData(Guid idUsulan, Guid idUsulanKegiatan)
        {
            ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            isiMitraPengabdianPerSkema();
            isigvMitraPelaksanaPT();
            isigvMitraPelaksanaPemdaKota();
            isigvKelMasyarakat();

        }

        public void setThnUsulan(string thn_usulan)
        {
            ViewState["thn_usulan"] = thn_usulan;
        }

        public void isigvKelMasyarakat()
        {
            DataTable dt = new DataTable();
            if (objMitra.getMitraKelompokMasyibdm(ref dt, ViewState["id_usulan_kegiatan"].ToString()))
            {
                Label2.Text = dt.Rows.Count.ToString();
                gvKelMasyarakat.DataSource = dt;
                gvKelMasyarakat.DataBind();

                if (dt.Rows.Count==0)
                { 
                lbKelMasyarakat.Visible = true;
                }
                else
                {
                    lbKelMasyarakat.Visible = false;

                }

            }
            else
            {
                Label2.Text = "0";
                lbKelMasyarakat.Visible = true;
            }
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
                if (listPT.Count>0)
                {
                    lbTambahPTPelaksana.Visible = false;
                }
                else
                {
                    lbTambahPTPelaksana.Visible = true;
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
            if (!objMitra.listPemdaPemkotCSR(ref listMitra, Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), ID_TIPE_PEMDA_PEMKOT))
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

        public bool Simpan()
        {
            return false;
        }

        protected void lbTambahPTPelaksana_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraPelaksanaPT.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            mvMitra.SetActiveView(vEditPT);
        }

        protected void lbTambahPemdaKota_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraPelaksanaPemdaKota.InitData(idUsulanKegiatan, Guid.Empty);
            ViewState["id_usulan_kegiatan"].ToString();
            ViewState["IsTipeMono"] = true;
            mvMitra.SetActiveView(vEditPemda);
        }

        protected void lbKelMasyarakat_Click(object sender, EventArgs e)
        {
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraSasaranKelMasyarakat.InitData(idUsulanKegiatan, Guid.Empty);

            ViewState["idMitra"] = mitraSasaranKelMasyarakat.hididmitra;

            kelompok1.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "1", Guid.Parse(ViewState["idMitra"].ToString()));
            //kelompok2.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "2", Guid.Parse(ViewState["idMitra"].ToString()));
            //kelompok3.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "3", Guid.Parse(ViewState["idMitra"].ToString()));

            mvMitra.SetActiveView(vEditKelMasyarakat);
        }

        protected void lbSimpanEditPT_Click(object sender, EventArgs e)
        {
            if (mitraPelaksanaPT.Simpan()) mvMitra.SetActiveView(vDaftarMitra);
            isigvMitraPelaksanaPT();
        }

        protected void lbBatalEditPT_Click(object sender, EventArgs e)
        {
            mvMitra.SetActiveView(vDaftarMitra);
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

        protected void lbSimpanEditKelMasyarakat_Click(object sender, EventArgs e)
        {

            if ((kelompok1.CekSimpan() == true)/* && (kelompok2.CekSimpan() == true) && (kelompok3.CekSimpan() == true)*/)
            {
                if (mitraSasaranKelMasyarakat.Simpan() == true)
                {
                    kelompok1.Simpan();
                    //kelompok2.Simpan();
                    //kelompok3.Simpan();
                    mvMitra.SetActiveView(vDaftarMitra);
                    isiMitraPengabdianPerSkema();
                    isigvMitraPelaksanaPT();
                    isigvMitraPelaksanaPemdaKota();
                    isigvKelMasyarakat();
                }
            }
        }
        
        protected void lbBatalEditPemdaKelMasyarakat_Click(object sender, EventArgs e)
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
            ViewState["tipeHapus"] = "1";
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvMitraPelaksanaPengabdian_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Guid idMitra = Guid.Parse(gvMitraPelaksanaPengabdian.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());

            ViewState["idMitra"] = idMitra.ToString();
            if (cmd == "unggahDokMitraPengabdian")
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
            ViewState["IsTipeMono"] = true;
            mvMitra.SetActiveView(vEditPemda);
        }

        protected void gvMitraPelaksanaPemdaKota_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //ViewState["IdMitraAbdimas"] = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["IdMitraAbdimas"].ToString());
            //lblNamaMitraPelaksana.Text = gvMitraPelaksanaPemdaKota.DataKeys[e.RowIndex]["NamaPimpinanInstitusi"].ToString();
            ViewState["tipeHapus"] = "3";
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvMitraPelaksanaPemdaKota_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            //Guid idMitra = Guid.Parse(gvMitraPelaksanaPemdaKota.DataKeys[rowIndex]["IdMitraAbdimas"].ToString());

            //ViewState["idMitra"] = idMitra.ToString();
            //if (cmd == "unggahDokMitraPemdaPemkot")
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
            //    ktUnggah.success_info = "Unggah dokumen Mitra Pemda Pemkot  berhasil";
            //    ktUnggah.failed_info = "Unggah dokumen Mitra Pemda Pemkot gagal";
            //    Session.Add("ktUnggah", ktUnggah);
            //    uiMdl.ShowModal(this.Page, "modalUnggahDokMitra");
            //}
            //else if (cmd == "unduhDokumenMitraPemdaPemkot")
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

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (ViewState["tipeHapus"].ToString() == "2")
            {
                if (objMitra.hapusMitrakelmasppdm(Guid.Parse(ViewState["IdMitraAbdimas"].ToString())))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data mitra berhasil");
                    string filePath = string.Format("~/fileUpload/Mitra/{0}/{1}.pdf", ViewState["thn_usulan_kegiatan"].ToString(),
                    ViewState["IdMitraAbdimas"].ToString());
                    if (File.Exists(Server.MapPath(filePath)))
                    {
                        File.Delete(Server.MapPath(filePath));
                    }
                    isigvKelMasyarakat();
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objMitra.errorMessage);
                }
            }
            else
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

                    if (ViewState["tipeHapus"].ToString() == "1")
                    {
                        isigvMitraPelaksanaPT();
                    }
                    else if (ViewState["tipeHapus"].ToString() == "3")
                    {
                        isigvMitraPelaksanaPemdaKota();
                    }
                    else if (ViewState["tipeHapus"].ToString() == "2")
                    {
                        isigvKelMasyarakat();
                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objMitra.errorMessage);
                }
            }
        }

        //protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        //{
        //    string ss = e.Item.Value;
        //    switch (ss)
        //    {
        //        case "0":
        //            //kelompokSasaranppdm.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "1",Guid.Parse(ViewState["idMitra"].ToString());
        //            break;
        //        case "1":
        //            //kelompokSasaranppdm.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "2", Guid.Parse(ViewState["idMitra"].ToString());
        //            break;
        //        case "2":
        //            //kelompokSasaranppdm.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "", Guid.Parse(ViewState["idMitra"].ToString());
        //            break;

        //    }
        //}

        protected void gvKelMasyarakat_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid idMitra = Guid.Empty;
            string namaMitra = string.Empty;
            if (e.CommandName == "Update" || e.CommandName == "Delete") return;

            int rowIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Substring(0, 6) == "unggah")
            {
                if (e.CommandName == "unggahDokMitraKelMas")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas"].ToString());
                }
                else if (e.CommandName == "unggah11")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_11"].ToString());
                }
                else if (e.CommandName == "unggah12")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_12"].ToString());
                }
                else if (e.CommandName == "unggah21")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_21"].ToString());
                }
                else if (e.CommandName == "unggah22")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_22"].ToString());
                }
                else if (e.CommandName == "unggah31")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_31"].ToString());
                }
                else if (e.CommandName == "unggah32")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_32"].ToString());
                }
                ViewState["idMitra"] = idMitra.ToString();
                
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
            else if (e.CommandName.Substring(0, 5) == "unduh")
            {
                if (e.CommandName == "unduhDokumenMitraSasaran")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas"].ToString());
                    namaMitra = gvKelMasyarakat.DataKeys[rowIndex]["NamaUMKM"].ToString();
                }
                else if (e.CommandName == "unduh11")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_11"].ToString());
                    namaMitra = "mitra_kel1_tahun1";
                }
                else if (e.CommandName == "unduh12")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_12"].ToString());
                    namaMitra = "mitra_kel2_tahun1";
                }
                else if (e.CommandName == "unduh21")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_21"].ToString());
                    namaMitra = "mitra_kel1_tahun2";

                }
                else if (e.CommandName == "unduh22")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_22"].ToString());
                    namaMitra = "mitra_kel2_tahun2";

                }
                else if (e.CommandName == "unduh31")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_31"].ToString());
                    namaMitra = "mitra_kel1_tahun3";
                }
                else if (e.CommandName == "unduh32")
                {
                    idMitra = Guid.Parse(gvKelMasyarakat.DataKeys[rowIndex]["id_mitra_abdimas_32"].ToString());
                    namaMitra = gvKelMasyarakat.DataKeys[rowIndex]["NamaUMKM"].ToString() + "_kel2_tahun3";
                }

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

        protected void gvKelMasyarakat_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["IdMitraAbdimas"] = Guid.Parse(gvKelMasyarakat.DataKeys[e.RowIndex]["id_mitra_abdimas"].ToString());
            lblNamaMitraPelaksana.Text = gvKelMasyarakat.DataKeys[e.RowIndex]["nama_pimpinan_mitra"].ToString();
            ViewState["tipeHapus"] = "2";
            uiMdl.ShowModal(this.Page, "modalHapus");
        }

        protected void gvKelMasyarakat_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id_mitra_abdimas = Guid.Parse(gvKelMasyarakat.DataKeys[e.RowIndex]["id_mitra_abdimas"].ToString());
            //mitraSasaranKelMasyarakat.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), id_mitra_abdimas);
            //ViewState["IsTipeMono"] = true;
            //mvMitra.SetActiveView(vEditPemda);

            //
            Guid idUsulanKegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            mitraSasaranKelMasyarakat.InitData(idUsulanKegiatan, id_mitra_abdimas);

            ViewState["idMitra"] = id_mitra_abdimas;

            kelompok1.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "1", Guid.Parse(ViewState["idMitra"].ToString()));
            //kelompok2.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "2", Guid.Parse(ViewState["idMitra"].ToString()));
            //kelompok3.InitData(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), "3", Guid.Parse(ViewState["idMitra"].ToString()));

            mvMitra.SetActiveView(vEditKelMasyarakat);

        }
    }
}