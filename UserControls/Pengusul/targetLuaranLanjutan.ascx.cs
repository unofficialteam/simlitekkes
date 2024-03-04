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
    public partial class targetLuaranLanjutan : System.Web.UI.UserControl
    {

        Models.login objLogin;
        Models.Pengusul.luaran objLuaran = new Models.Pengusul.luaran();
        uiDropdownList obj_uiDropdownList = new uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        uiNotify noty = new uiNotify();
        uiModal obj_uiMdl = new uiModal();
        protected void Page_Load(object sender, EventArgs e)
        {

            ktPublikasi.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktPublikasiProsiding.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktBukuCetakHasilPenelitian.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktBukuElektronikHasilPenelitian.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktBookChapter.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktLuaranWajibPTdanPP.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktLuaranWajibPTdanPPFormTambah.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktPublikasiProsidingLuaranTambahan.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktBookChapterLuaranTambahan.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktPublikasiPasca.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
        }

        void Child1_OnChildEventBatal(object sender, EventArgs e)
        {
            mvLuaran.SetActiveView(vDaftarLuaranWajib);
            isiluaran(int.Parse(ViewState["tkt_target"].ToString()));
            lblJudulTambahLuaran.Text = "TAMBAH DATA LUARAN WAJIB";            
        }

        public void setDataLuaran(string pidUsulan, int plamakegiatan, int urutan_thn, string pidUsulankegiatan,
            string pnamaSkema, int ptkttarget, int pidskema)
        {
            ViewState["id_usulan"] = pidUsulan;
            ViewState["lama_kegiatan"] = plamakegiatan;
            ViewState["id_usulan_kegiatan"] = pidUsulankegiatan;
            ViewState["urutan_thn"] = urutan_thn;
            ViewState["nama_Skema"] = pnamaSkema;
            ViewState["tkt_target"] = ptkttarget;
            ViewState["id_skema"] = pidskema;
            isiDdlKategoriLuaran(pidUsulan, urutan_thn, ptkttarget);

            panelPenelitianDasar.Visible = false;
            panelPenelitianTerapanPengembangan.Visible = false;

            isiluaran(ptkttarget);

            
            mvLuaran.SetActiveView(vDaftarLuaranWajib);
        }

        private void isiDdlKategoriLuaran(string pidUsulan, int urutan_thn, int ptkttarget)
        {
            string id_kelompok_luaran = "1";
            if (lblJudulTambahLuaran.Text == "TAMBAH DATA LUARAN TAMBAHAN")
            {
                id_kelompok_luaran = "2";
            }
            else
            {
                id_kelompok_luaran = "1";
            }
            if (ddlKategoriPenelitianDasar.Items.Count <= 0)
            {
                ddlKategoriLuaranTerapan.Items.Clear();
                ddlKategoriPenelitianDasar.Items.Clear();
                DataTable dt = new DataTable();                                             
                objLuaran.listKategoriLuaran_xiia(ref dt, int.Parse(ViewState["id_skema"].ToString()), int.Parse(id_kelompok_luaran));
                ddlKategoriPenelitianDasar.DataValueField = "kd_kategori_jenis_luaran";
                ddlKategoriPenelitianDasar.DataTextField = "nama_kategori_jenis_luaran";
                ddlKategoriPenelitianDasar.Items.Add(new ListItem("-- Pilih --", "0"));
                ddlKategoriPenelitianDasar.DataSource = dt;
                ddlKategoriPenelitianDasar.DataBind();

                ddlKategoriLuaranTerapan.DataValueField = "kd_kategori_jenis_luaran";
                ddlKategoriLuaranTerapan.DataTextField = "nama_kategori_jenis_luaran";
                ddlKategoriLuaranTerapan.Items.Add(new ListItem("-- Pilih --", "0"));
                ddlKategoriLuaranTerapan.DataSource = dt;
                ddlKategoriLuaranTerapan.DataBind();

            }
        }
        public void isiluaran(int p_tkt_target)
        {
            
            if (p_tkt_target <= 3)
            {
                panelPenelitianDasar.Visible = true;
                isiLuaranWajib();
                isiLuarantambahan();
            }
            else
            {
                panelPenelitianTerapanPengembangan.Visible = true;
                isiluaranTerapanPengembangan();
                isiLuarantambahan();
            }
        }

        public void isiluaranTerapanPengembangan()
        {
            DataTable dt = new DataTable();
            objLuaran.getKategoriLuaranTerapanPengembangan(ref dt,
                Guid.Parse(ViewState["id_usulan"].ToString()));
            if (dt.Rows.Count > 0)
            {
                string kd_kategori_jenis_luaran = dt.Rows[0]["kd_kategori_jenis_luaran"].ToString();
                ViewState["id_jenis_luaran"] = dt.Rows[0]["id_jenis_luaran"].ToString();
                ViewState["id_target_capaian_luaran"] = dt.Rows[0]["id_target_capaian_luaran"].ToString(); ;

                ddlKategoriLuaranTerapan.ClearSelection();
                selectDdlValue(ref ddlKategoriLuaranTerapan, kd_kategori_jenis_luaran);

                int idKelompok = 1; //wajib
                ddlKategoriJenisLuaranEvent(ref mvJenisLuaranTerapan, kd_kategori_jenis_luaran, idKelompok);

                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Semua luaran wajib sudah terpenuhi");

                lblInfoKelengkapanLuaran.Text = "Isian luaran wajib sudah lengkap";
                lblInfoKelengkapanLuaran.BackColor = System.Drawing.Color.FromArgb(255, 102, 102, 221);

            }
            else
            {
                lblInfoKelengkapanLuaran.Text = "Isian luaran wajib belum lengkap";
                lblInfoKelengkapanLuaran.BackColor = System.Drawing.Color.FromArgb(255, 221, 102, 102);
            }
        }

        private void isiDdlJenisLuaran()
        {
        }

        private void isiLuaranWajib()
        {
            ViewState["jml_luaran_wajib"] = 0;
            DataTable dt = new DataTable();
            if (objLuaran.ListTargetLuaranWajib(ref dt, Guid.Parse(ViewState["id_usulan"].ToString())))
            {
                ViewState["tahun_ke"] = "";
                gvluaranwajib.DataSource = dt;
                gvluaranwajib.DataBind();

                Boolean cek = (CekLuaran(gvluaranwajib));

                if (dt.Rows.Count <= 0)
                    cek = false; 

                if (cek)
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Semua luaran wajib sudah terpenuhi");
                    lblInfoKelengkapanLuaran.Text = "Isian luaran wajib sudah lengkap";
                    lblInfoKelengkapanLuaran.BackColor = System.Drawing.Color.FromArgb(255, 102, 102, 221);

                }
                else
                {
                    lblInfoKelengkapanLuaran.Text = "Isian luaran wajib belum lengkap";
                    lblInfoKelengkapanLuaran.BackColor = System.Drawing.Color.FromArgb(255, 221, 102, 102);
                }
            }

        }
        public static Boolean CekLuaran(GridView gridView)
        {
            Boolean cek = true;
            foreach (GridViewRow gvr in gridView.Rows)
            {
                Label namaluaran = gvr.FindControl("lblnamaluaran") as Label;
                if (namaluaran != null)
                {
                    if ((namaluaran.Text == "") || (namaluaran.Text == "-"))
                    {
                        cek = false;
                    }
                }
            }
            return cek;
        }
        private void isiLuarantambahan()
        {
            DataTable dt = new DataTable();
            if (objLuaran.ListTargetLuaranTambahan(ref dt, Guid.Parse(ViewState["id_usulan"].ToString())))
            {
                ViewState["tahun_ke"] = "";
                //ViewState["awal"] = "";
                gvluarantambahan.DataSource = dt;
                gvluarantambahan.DataBind();
                gvluaranTambahan_DataBound();
            }

        }

        protected void gvluaranwajib_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id_luaran_dijanjikan = gvluaranwajib.DataKeys[rowIndex]["id_luaran_dijanjikan"].ToString();
            string id_jenis_luaran = gvluaranwajib.DataKeys[rowIndex]["id_jenis_luaran"].ToString();
            string tahun_ke = gvluaranwajib.DataKeys[rowIndex]["tahun_ke"].ToString();
            string kd_kategori_jenis_luaran = gvluaranwajib.DataKeys[rowIndex]["kd_kategori_jenis_luaran"].ToString();


            id_luaran_dijanjikan = id_luaran_dijanjikan.Replace("{", "").Replace("}", "").Replace("\"", "");
            string[] arr = id_luaran_dijanjikan.Split(new Char[] { ',' });
            id_luaran_dijanjikan = arr[0];

            ViewState["id_luaran_dijanjikan"] = id_luaran_dijanjikan;
            ViewState["tahun_ke"] = tahun_ke;
            ViewState["id_jenis_luaran"] = id_jenis_luaran;
            lblThnKe.Text = tahun_ke;

            int idKelompok = 1;
            ViewState["id_kelompok_luaran"] = idKelompok;
            lblJudulTambahLuaran.Text = "TAMBAH DATA LUARAN WAJIB";
            if (e.CommandName == "hapus")
            {
                obj_uiMdl.ShowModal(this.Page, "myModal");
            }            
            else if (e.CommandName == "ubah")
            {
                ddlKategoriJenisLuaranEvent(ref mvJenisLuaran, kd_kategori_jenis_luaran, idKelompok);   
                setDataLuaranByKdKategori(id_luaran_dijanjikan, id_jenis_luaran, kd_kategori_jenis_luaran, idKelompok);
                ddlKategoriPenelitianDasar.ClearSelection();
                selectDdlValue(ref ddlKategoriPenelitianDasar, kd_kategori_jenis_luaran);
                ddlKategoriPenelitianDasar.Enabled = false;
                mvLuaran.SetActiveView(vTambahLuaran);
            }
            else if (e.CommandName == "tambah")
            {

                ddlKategoriPenelitianDasar.Enabled = true;
                setDataLuaranByKdKategori(Guid.Empty.ToString() , id_jenis_luaran, kd_kategori_jenis_luaran, idKelompok);
                mvLuaran.SetActiveView(vTambahLuaran);
            }
        }

        private void setDataLuaranByKdKategori(string p_id_luaran_dijanjikan, string id_jenis_luaran, string kdKategoriJenisLuaran, int idKelompok)
        {
            switch (kdKategoriJenisLuaran.Trim())
            {
                case "": // tambah
                    mvJenisLuaran.SetActiveView(vBlank);
                    ddlKategoriPenelitianDasar.ClearSelection();
                    ddlKategoriPenelitianDasar.SelectedIndex = 0;
                    break;
                case "4":
                case "16":
                case "17":
                case "19": case "20": case "23":
                    ktPublikasi.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        ViewState["id_kelompok_luaran"].ToString());
                    ktPublikasi.setData(Guid.Parse(p_id_luaran_dijanjikan),
                            ViewState["tahun_ke"].ToString(),
                            id_jenis_luaran, kdKategoriJenisLuaran
                            );
                    mvJenisLuaran.SetActiveView(vPublikasi);
                    break;
                case "5":
                    ktBukuCetakHasilPenelitian.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        ViewState["id_kelompok_luaran"].ToString());
                    ktBukuCetakHasilPenelitian.setData(Guid.Parse(p_id_luaran_dijanjikan),
                            ViewState["tahun_ke"].ToString(),
                            id_jenis_luaran, kdKategoriJenisLuaran
                            );
                    mvJenisLuaran.SetActiveView(vBukuCetakHasilPenelitian);
                    break;
                case "6":
                    ktBukuElektronikHasilPenelitian.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        ViewState["id_kelompok_luaran"].ToString());
                    ktBukuElektronikHasilPenelitian.setData(Guid.Parse(p_id_luaran_dijanjikan),
                            ViewState["tahun_ke"].ToString(),
                            id_jenis_luaran, kdKategoriJenisLuaran
                            );
                    mvJenisLuaran.SetActiveView(vBukuElektronikHasilPenelitian);
                    break;
                case "7":
                    if (idKelompok == 1)
                    {
                        ktPublikasiProsiding.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        ViewState["id_kelompok_luaran"].ToString());
                        ktPublikasiProsiding.setData(Guid.Parse(p_id_luaran_dijanjikan),
                                ViewState["tahun_ke"].ToString(),
                                id_jenis_luaran, kdKategoriJenisLuaran
                                );
                        mvJenisLuaran.SetActiveView(vPublikasiProsiding);
                    }
                    else if (idKelompok == 2) // luaran tambahan
                    {
                        ktPublikasiProsidingLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        idKelompok.ToString());
                        mvJenisLuaran.SetActiveView(vPublikasiProsidingLuaranTambahan);
                    }

                    break;
                case "8":
                    if (idKelompok == 1)
                    {
                        ktBookChapter.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        ViewState["id_kelompok_luaran"].ToString());
                        ktBookChapter.setData(Guid.Parse(p_id_luaran_dijanjikan),
                                ViewState["tahun_ke"].ToString(),
                                id_jenis_luaran, kdKategoriJenisLuaran
                                );
                        mvJenisLuaran.SetActiveView(vBookChapter);
                    }
                    else if (idKelompok == 2) // luaran tambahan
                    {
                        ktBookChapterLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                            ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                            idKelompok.ToString());
                        mvJenisLuaran.SetActiveView(vBookChapterLuaranTambahan);
                    }
                    break;
                case "9":
                    break;
                case "10":
                    break;
                case "11":
                    break;
                case "15":
                    break;
                case "P":
                    break;
            }

        }

        protected void gvluaranwajib_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string tahun_ke = gvluaranwajib.DataKeys[e.Row.RowIndex]["tahun_ke"].ToString();
                string id_luaran_dijanjikan = gvluaranwajib.DataKeys[e.Row.RowIndex]["id_luaran_dijanjikan"].ToString();
                string nama_target_capaian_luaran = gvluaranwajib.DataKeys[e.Row.RowIndex]["nama_target_capaian_luaran"].ToString();
                string id_jenis_luaran = gvluaranwajib.DataKeys[e.Row.RowIndex]["id_jenis_luaran"].ToString();

                Label lblTahunke = new Label();
                LinkButton lbTambah = new LinkButton();
                LinkButton lbHapus = new LinkButton();
                LinkButton lbEdit = new LinkButton();
                Label lbltargetluaran = new Label();
                Label lblnamaluaran = new Label();
                Label lblketerangan = new Label();
                lblTahunke = (Label)e.Row.FindControl("lblTahunke");
                lbTambah = (LinkButton)e.Row.FindControl("lbTambah");
                lbHapus = (LinkButton)e.Row.FindControl("lbHapus");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbltargetluaran = (Label)e.Row.FindControl("lbltargetluaran");
                lblnamaluaran = (Label)e.Row.FindControl("lblnamaluaran");
                lblketerangan = (Label)e.Row.FindControl("lblketerangan");

                lbTambah.Visible = false;
                lbHapus.Visible = false;
                lbEdit.Visible = false;
                string keterangan = lblketerangan.Text.Replace("{", "").Replace("}", "").Replace("\"", "");
                keterangan = "- " + keterangan.Replace(",", "<br>- ");
                if (keterangan.Contains("NULL"))
                {
                    lblketerangan.Text = "";
                    lbTambah.Visible = true;
                }
                else
                {
                    lblketerangan.Text = keterangan;
                    lbHapus.Visible = true;
                    if (ViewState["id_skema"].ToString() == "30")
                    {
                        lbEdit.Visible = false;
                    }
                    else
                    {
                        lbEdit.Visible = true;

                    }
                }

                if (lbltargetluaran != null)
                {
                    if (nama_target_capaian_luaran != "")
                    {
                        lbltargetluaran.Text = "(" + nama_target_capaian_luaran + ")";
                    }
                    else
                    {
                        lbltargetluaran.Text = "-";
                    }
                };
            }

        }

        protected void gvluaranTambahan_DataBound()
        {
            for (int i = gvluarantambahan.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = gvluarantambahan.Rows[i];
                GridViewRow previousRow = gvluarantambahan.Rows[i - 1];
                int nkol = 1;
                for (int j = 0; j < nkol; j++)
                {
                    string txt2 = ((Label)row.Cells[j].FindControl("lblTahunke")).Text;
                    string txt1 = ((Label)previousRow.Cells[j].FindControl("lblTahunke")).Text;
                    //run this loop for the column which you thing the data will be similar
                    if (((Label)row.Cells[j].FindControl("lblTahunke")).Text == ((Label)previousRow.Cells[j].FindControl("lblTahunke")).Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                    //run this loop for the column which you thing the data will be similar
                    //if (((Label)row.Cells[j].FindControl("lblCity")).Text == ((Label)previousRow.Cells[j].FindControl("lblCity")).Text)
                    //{
                    //    if (previousRow.Cells[j].RowSpan == 0)
                    //    {
                    //        if (row.Cells[j].RowSpan == 0)
                    //        {
                    //            previousRow.Cells[j].RowSpan += 2;
                    //        }
                    //        else
                    //        {
                    //            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                    //        }
                    //        row.Cells[j].Visible = false;
                    //    }
                    //}
                }
            }
        }
        protected void gvluarantambahan_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id_luaran_dijanjikan = gvluarantambahan.DataKeys[rowIndex]["id_luaran_dijanjikan"].ToString();
            string id_jenis_luaran = gvluarantambahan.DataKeys[rowIndex]["id_jenis_luaran"].ToString();
            string tahun_ke = gvluarantambahan.DataKeys[rowIndex]["tahun_ke"].ToString();
            string kd_kategori_jenis_luaran = gvluarantambahan.DataKeys[rowIndex]["kd_kategori_jenis_luaran"].ToString();

            id_luaran_dijanjikan = id_luaran_dijanjikan.Replace("{", "").Replace("}", "").Replace("\"", "");
            string[] arr = id_luaran_dijanjikan.Split(new Char[] { ',' });
            id_luaran_dijanjikan = arr[0];

            ViewState["id_luaran_dijanjikan"] = id_luaran_dijanjikan;
            ViewState["tahun_ke"] = tahun_ke;
            ViewState["id_jenis_luaran"] = id_jenis_luaran;
            lblThnKe.Text = tahun_ke;

            int idKelompok = 2;
            ViewState["id_kelompok_luaran"] = idKelompok;
            lblJudulTambahLuaran.Text = "TAMBAH DATA LUARAN TAMBAHAN";
            if (e.CommandName == "hapus")
            {
                //ViewState["kategorijeniluaran"] = "Wajib";
                obj_uiMdl.ShowModal(this.Page, "myModal");
            }
            else if (e.CommandName == "ubah")
            {
                ddlKategoriJenisLuaranEvent(ref mvJenisLuaran, kd_kategori_jenis_luaran, idKelompok);
                setDataLuaranByKdKategori(id_luaran_dijanjikan, id_jenis_luaran, kd_kategori_jenis_luaran, idKelompok);
                ddlKategoriPenelitianDasar.ClearSelection();
                selectDdlValue(ref ddlKategoriPenelitianDasar, kd_kategori_jenis_luaran);
                ddlKategoriPenelitianDasar.Enabled = false;
                mvLuaran.SetActiveView(vTambahLuaran);
            }
            else if (e.CommandName == "tambah")
            {
                ddlKategoriPenelitianDasar.Items.Clear();
                DataTable dt = new DataTable();
                objLuaran.listKategoriLuaran_xiia(ref dt, int.Parse(ViewState["id_skema"].ToString()), int.Parse("2"));
                ddlKategoriPenelitianDasar.DataValueField = "kd_kategori_jenis_luaran";
                ddlKategoriPenelitianDasar.DataTextField = "nama_kategori_jenis_luaran";
                ddlKategoriPenelitianDasar.Items.Add(new ListItem("-- Pilih --", "0"));
                ddlKategoriPenelitianDasar.DataSource = dt;
                ddlKategoriPenelitianDasar.DataBind();

                ddlKategoriPenelitianDasar.Enabled = true;
                setDataLuaranByKdKategori(Guid.Empty.ToString(), id_jenis_luaran, kd_kategori_jenis_luaran, idKelompok);
                mvLuaran.SetActiveView(vTambahLuaran);
            }
        }

        protected void gvluarantambahan_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow theRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string tahun_ke = gvluarantambahan.DataKeys[e.Row.RowIndex]["tahun_ke"].ToString();
                string id_luaran_dijanjikan = gvluarantambahan.DataKeys[e.Row.RowIndex]["id_luaran_dijanjikan"].ToString();
                string nama_target_capaian_luaran = gvluarantambahan.DataKeys[e.Row.RowIndex]["nama_target_capaian_luaran"].ToString();
                string id_jenis_luaran = gvluarantambahan.DataKeys[e.Row.RowIndex]["id_jenis_luaran"].ToString();
                //    LinkButton lbUnggahLaporanakhir = new LinkButton();
                //    lbUnggahLaporanakhir = (LinkButton)e.Row.FindControl("lbUnggahLaporanakhir");

                Label lblTahunke = new Label();
                LinkButton lbTambah = new LinkButton();
                LinkButton lbHapus = new LinkButton();
                LinkButton lbEdit = new LinkButton();
                Label lbltargetluaran = new Label();
                Label lblnamaluaran = new Label();
                Label lblketerangan = new Label();
                lblTahunke = (Label)e.Row.FindControl("lblTahunke");
                lbTambah = (LinkButton)e.Row.FindControl("lbTambah");
                lbHapus = (LinkButton)e.Row.FindControl("lbHapus");
                lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                lbltargetluaran = (Label)e.Row.FindControl("lbltargetluaran");
                lblnamaluaran = (Label)e.Row.FindControl("lblnamaluaran");
                lblketerangan = (Label)e.Row.FindControl("lblketerangan");

                lbTambah.Visible = false;
                lbHapus.Visible = false;
                lbEdit.Visible = false;
                string keterangan = lblketerangan.Text.Replace("{", "").Replace("}", "").Replace("\"", "");
                //keterangan = "- " + keterangan.Replace(",", "<br>- ");
                if (keterangan.Contains("NULL") || keterangan == "")
                {
                    lblketerangan.Text = "";
                    lbTambah.Visible = true;
                }
                else
                {
                    lblketerangan.Text = keterangan;
                    lbHapus.Visible = true;
                    lbEdit.Visible = false;
                    lbTambah.Visible = true;
                }

                if (lbltargetluaran != null)
                {
                    if (nama_target_capaian_luaran != "")
                    {
                        lbltargetluaran.Text = "(" + nama_target_capaian_luaran + ")";
                    }
                    else
                    {
                        lbltargetluaran.Text = "-";
                    }
                };
            }
        }

        protected void gvluarantambahan_PreRender(object sender, EventArgs e)
        {

        }
        private void selectDdlValue(ref DropDownList ddl, string value)
        {
            ListItem item1 = ddl.Items.FindByValue(value);
            if (item1 != null)
            {
                ddl.ClearSelection();
                ddl.Items.FindByValue(value).Selected = true;
            }
        }
        protected void ddlKategoriPenelitianDasar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kdKategoriJenisLuaran = ddlKategoriPenelitianDasar.SelectedValue;
            int idKelompok = int.Parse(ViewState["id_kelompok_luaran"].ToString());
            ddlKategoriJenisLuaranEvent(ref mvJenisLuaran, kdKategoriJenisLuaran, idKelompok);
        }

        protected void ddlKategoriLuaranTerapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kdKategoriJenisLuaran = ddlKategoriLuaranTerapan.SelectedValue;
            int idKelompok = 1; // int.Parse(ViewState["id_kelompok_luaran"].ToString());
            ddlKategoriJenisLuaranEvent(ref mvJenisLuaranTerapan, kdKategoriJenisLuaran, idKelompok);
        }        

        private void ddlKategoriJenisLuaranEvent(ref MultiView p_mvJenisLuaran, string kdKategoriJenisLuaran, int idKelompok)
        {
            switch(kdKategoriJenisLuaran.Trim())
            {
                case "4":
                case "16":
                case "17":
                case "20":
                case "19": case "23":
                    if (ViewState["id_skema"].ToString() == "30")// skema paska
                    {
                        ktPublikasiPasca.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        ViewState["id_kelompok_luaran"].ToString());
                        p_mvJenisLuaran.SetActiveView(vPublikasiPasca);
                    }
                    else
                    { 
                        ktPublikasi.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        ViewState["id_kelompok_luaran"].ToString());
                        p_mvJenisLuaran.SetActiveView(vPublikasi);
                    }
                    break;

                case "5":
                    p_mvJenisLuaran.SetActiveView(vBukuCetakHasilPenelitian);
                    ktBukuCetakHasilPenelitian.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        ViewState["id_kelompok_luaran"].ToString());
                    p_mvJenisLuaran.SetActiveView(vBukuCetakHasilPenelitian);
                    break;
                case "6":
                    p_mvJenisLuaran.SetActiveView(vBukuElektronikHasilPenelitian);
                    ktBukuElektronikHasilPenelitian.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        ViewState["id_kelompok_luaran"].ToString());
                    p_mvJenisLuaran.SetActiveView(vBukuElektronikHasilPenelitian);
                    break;
                case "7":
                case "21":
                    if (idKelompok == 1) // luaran wajib
                    {
                        //if (ViewState["id_skema"].ToString()=="30") //skema paska
                        //{
                        //    ktPublikasiPasca.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        //    ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        //    ViewState["id_kelompok_luaran"].ToString());
                        //    p_mvJenisLuaran.SetActiveView(vPublikasiPasca);

                        //}
                        //else if (ViewState["id_skema"].ToString() == "7") // skema pdp     
                        //{
                        //    ktPublikasiProsidingLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        //    ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        //    idKelompok.ToString());
                        //    p_mvJenisLuaran.SetActiveView(vPublikasiProsidingLuaranTambahan);

                        //}
                        //else if (ViewState["id_skema"].ToString() == "76") // skema pdp     
                        //{
                        //    ktPublikasiProsidingLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        //    ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        //    idKelompok.ToString());
                        //    p_mvJenisLuaran.SetActiveView(vPublikasiProsidingLuaranTambahan);

                        //}
                        //else if (ViewState["id_skema"].ToString() == "5") // skema pkpt
                        //{
                        //    ktPublikasiProsidingLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        //    ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        //    idKelompok.ToString());
                        //    p_mvJenisLuaran.SetActiveView(vPublikasiProsidingLuaranTambahan);


                        //}

                        //else
                        //{
                        //ktPublikasiProsiding.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        //ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        //idKelompok.ToString());
                        //p_mvJenisLuaran.SetActiveView(vPublikasiProsiding);
                        ktPublikasiProsidingLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        idKelompok.ToString());
                        p_mvJenisLuaran.SetActiveView(vPublikasiProsidingLuaranTambahan);

                        //}
                    }
                    else if (idKelompok == 2) // luaran tambahan
                    {
                        ktPublikasiProsidingLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        idKelompok.ToString());
                        p_mvJenisLuaran.SetActiveView(vPublikasiProsidingLuaranTambahan);
                        
                    }
                        break;
                case "8":
                    if (idKelompok == 1) // luaran wajib
                    {
                        //if (ViewState["id_skema"].ToString() == "5") //skema pkpt
                        //{
                        //    ktBookChapterLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        //    ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        //    idKelompok.ToString());
                        //    p_mvJenisLuaran.SetActiveView(vBookChapterLuaranTambahan);
                        //}
                        //else
                        //{
                        //ktBookChapter.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                        //    ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                        //    idKelompok.ToString());
                        //p_mvJenisLuaran.SetActiveView(vBookChapter);
                        ktBookChapterLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                            ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                            idKelompok.ToString());
                        p_mvJenisLuaran.SetActiveView(vBookChapterLuaranTambahan);

                        //}
                    }
                    else if (idKelompok == 2) // luaran tambahan
                    {
                        ktBookChapterLuaranTambahan.init(kdKategoriJenisLuaran, ViewState["id_usulan"].ToString(),
                            ViewState["tahun_ke"].ToString(), int.Parse(ViewState["id_skema"].ToString()),
                            idKelompok.ToString());
                        p_mvJenisLuaran.SetActiveView(vBookChapterLuaranTambahan);
                    }
                        break;
                case "9": // Hak Cipta
                case "10": // Paten
                case "11": // Paten Sederhana
                case "12": // PVT
                case "13": // DTLST
                case "14": // Naskah Kebijakan
                case "15": // Produk Industri
                case "P": // Kebijakan
                case "18": // Naskah Akademik

                    if (ViewState["id_jenis_luaran"] == null )
                    {
                        ViewState["id_jenis_luaran"] = "-1";
                    }
                    if (ViewState["id_target_capaian_luaran"] == null)
                    {
                        ViewState["id_target_capaian_luaran"] = "-1";
                    }
                    if (ViewState["id_kelompok_luaran"] == null)
                    {
                        ViewState["id_kelompok_luaran"] = "1";
                    }
                    if ((ViewState["tahun_ke"] == null) || ((ViewState["tahun_ke"].ToString()=="")))
                    {
                        ViewState["tahun_ke"] = "1";
                    }

                    int idSkema = int.Parse(ViewState["id_skema"].ToString());
                    if (idKelompok == 1) // luaran wajib
                    {
                        if (p_mvJenisLuaran == mvJenisLuaran)
                        {
                            p_mvJenisLuaran.SetActiveView(vLuaranWajibPTdanPPDasar);
                            ktLuaranWajibPTdanPPDasar.Refresh(Guid.Parse(ViewState["id_usulan"].ToString()), kdKategoriJenisLuaran,
                                ViewState["id_jenis_luaran"].ToString(), ViewState["id_target_capaian_luaran"].ToString(),
                                idKelompok.ToString(),
                                int.Parse(ViewState["tahun_ke"].ToString()),
                                idSkema);

                            if(idSkema == 77) // 77 KKS
                            {
                                p_mvJenisLuaran.SetActiveView(vTambahLuaranTerapan);
                                ktLuaranWajibPTdanPPFormTambah.Refresh(Guid.Parse(ViewState["id_usulan"].ToString()), kdKategoriJenisLuaran,
                                ViewState["id_jenis_luaran"].ToString(), ViewState["id_target_capaian_luaran"].ToString(),
                                idKelompok.ToString(),
                                int.Parse(ViewState["tahun_ke"].ToString()),
                                idSkema);
                            }

                        }
                        else
                        {
                            p_mvJenisLuaran.SetActiveView(vLuaranWajibPTdanPP);
                            ktLuaranWajibPTdanPP.Refresh(Guid.Parse(ViewState["id_usulan"].ToString()), kdKategoriJenisLuaran,
                                ViewState["id_jenis_luaran"].ToString(), ViewState["id_target_capaian_luaran"].ToString(),
                                idKelompok.ToString(),
                                int.Parse(ViewState["tahun_ke"].ToString()),
                                idSkema);
                        }
                    }
                    if (idKelompok == 2) // luaran tambahan
                    {
                        p_mvJenisLuaran.SetActiveView(vTambahLuaranTerapan);
                        string id_target_capaian_luaran_tambahan = "-1";
                        ktLuaranWajibPTdanPPFormTambah.Refresh(Guid.Parse(ViewState["id_usulan"].ToString()), kdKategoriJenisLuaran,
                                ViewState["id_jenis_luaran"].ToString(), id_target_capaian_luaran_tambahan,
                                idKelompok.ToString(),
                                int.Parse(ViewState["tahun_ke"].ToString()),
                                idSkema);
                    }



                   
                    break;
            }
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if(objLuaran.deleteLuaranDijanjikan_xii(ViewState["id_luaran_dijanjikan"].ToString()))
            {
                isiluaran(int.Parse(ViewState["tkt_target"].ToString()));
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                       "Hapus luaran berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Hapus luaran gagal. Silakan hubungi administrator.");
            }
        }

        protected void lbTambahLuaranTambahan_Click(object sender, EventArgs e)
        {
            //ViewState["id_kelompok_luaran"] = 2;
            //setDataLuaranByKdKategori(Guid.Empty.ToString(), "", "");
            //mvLuaran.SetActiveView(vTambahLuaran);
            //lblJudulTambahLuaran.Text = "TAMBAH DATA LUARAN TAMBAHAN";
            //ktPublikasi.setIdKelompokLuaran(2,
            //    int.Parse(ViewState["lama_kegiatan"].ToString())); // 2 untuk luaran tambahan
            //ktPublikasiProsiding.setIdKelompokLuaran(2); // 2 untuk luaran tambahan
        }

    }
}