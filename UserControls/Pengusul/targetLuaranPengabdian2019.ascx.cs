using simlitekkes.Models.Sistem;
using simlitekkes.UIControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class targetLuaranPengabdian2019 : System.Web.UI.UserControl
    {
        usulanKegiatan objUsulanKegiatan = new usulanKegiatan();
        Models.Pengusul.luaran objLuaran = new Models.Pengusul.luaran();
        uiNotify noty = new uiNotify();
        uiModal obj_uiMdl = new uiModal();


        protected void Page_Load(object sender, EventArgs e)
        {
            ktPublikasiNBuku.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
            ktPatenNHakCIpta.OnChildEventBatal += new EventHandler(Child1_OnChildEventBatal);
        }

        void Child1_OnChildEventBatal(object sender, EventArgs e)
        {
            mvLuaran.SetActiveView(vDaftarLuaranWajib);
            isiluaranWajib();
            isiLuarantambahan();
        }

        public void setDataLuaranpengabdian(usulanKegiatan p_objUsulanKegiatan)
        {
            ViewState["id_usulan"] = p_objUsulanKegiatan.idUsulan;
            ViewState["lama_kegiatan"] = p_objUsulanKegiatan.lamaKegiatan;
            ViewState["id_usulan_kegiatan"] = p_objUsulanKegiatan.idUsulanKegiatan;
            ViewState["urutan_thn"] = p_objUsulanKegiatan.urutanTahunUsulanKegiatan;
            ViewState["nama_Skema"] = p_objUsulanKegiatan.namaSkema;
            ViewState["id_skema"] = p_objUsulanKegiatan.idSkema;

            lblInfoAtUnggahDokUsulan.Text = String.Format("{0} (tahun ke-{1} dari {2} tahun)", 
                p_objUsulanKegiatan.namaSkema, p_objUsulanKegiatan.urutanTahunUsulanKegiatan, 
                p_objUsulanKegiatan.lamaKegiatan);
            isiluaranWajib();
            isiLuarantambahan();
        }

        public void isiluaranWajib()
        {

            DataTable dt = new DataTable();
            int idKelompok = 1;
            if (objLuaran.listLuaranPengabdian(ref dt, Guid.Parse(ViewState["id_usulan"].ToString()), idKelompok))
            {
                gvluaranwajib.DataSource = dt;
                gvluaranwajib.DataBind();
                gvluaranwajib_DataBound();
                //ShowingGroupingDataInGridView(gvluaranwajib.Rows, 0, 1);
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
                Label lbltargetluaran = gvr.FindControl("lbltargetluaran") as Label;
                if (lbltargetluaran != null)
                {
                    if ((lbltargetluaran.Text == "") || (lbltargetluaran.Text == "-"))
                    {
                        cek = false;
                    }
                }
            }
            return cek;
        }

    protected void gvluaranwajib_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id_luaran_dijanjikan = gvluaranwajib.DataKeys[rowIndex]["id_luaran_dijanjikan"].ToString();
            string id_jenis_luaran = gvluaranwajib.DataKeys[rowIndex]["id_jenis_luaran"].ToString();
            string tahun_ke = gvluaranwajib.DataKeys[rowIndex]["tahun_ke"].ToString();
            string kd_kategori_jenis_luaran = gvluaranwajib.DataKeys[rowIndex]["kd_kategori_jenis_luaran"].ToString();
            string arr_kd_kategori_jenis_luaran = gvluaranwajib.DataKeys[rowIndex]["arr_kd_kategori_jenis_luaran"].ToString();
            string arr_nama_kategori_jenis_luaran = gvluaranwajib.DataKeys[rowIndex]["arr_nama_kategori_jenis_luaran"].ToString();
            string id_target_capaian_luaran = gvluaranwajib.DataKeys[rowIndex]["id_target_capaian_luaran"].ToString();
            
            //id_luaran_dijanjikan = id_luaran_dijanjikan.Replace("{", "").Replace("}", "").Replace("\"", "");
            //string[] arr = id_luaran_dijanjikan.Split(new Char[] { ',' });
            //id_luaran_dijanjikan = arr[0];

            ViewState["kd_kategori_jenis_luaran"] = kd_kategori_jenis_luaran;
            ViewState["id_luaran_dijanjikan"] = id_luaran_dijanjikan;
            ViewState["tahun_ke"] = tahun_ke;
            ViewState["id_jenis_luaran"] = id_jenis_luaran;
            ViewState["id_target_capaian_luaran"] = id_target_capaian_luaran;
            lblThnKe.Text = tahun_ke;
            lblThnBuktiLuaran.Text = tahun_ke;

            int idKelompok = 1;
            ViewState["id_kelompok_luaran"] = idKelompok;
            lblJudulTambahLuaran.Text = "TAMBAH DATA LUARAN WAJIB";

            ddlKategori.Enabled = true;
            ddlJenisLuaran.Enabled = true;

            if (e.CommandName == "hapus")
            {
                //ViewState["kategorijeniluaran"] = "Wajib";
                obj_uiMdl.ShowModal(this.Page, "myModal");
            }
            else if (e.CommandName == "ubah")
            {
                isiDdlKategori(arr_kd_kategori_jenis_luaran, arr_nama_kategori_jenis_luaran);
                selectDdlValue(ref ddlKategori, kd_kategori_jenis_luaran);

                isiDdlJenis(kd_kategori_jenis_luaran, idKelompok);
                selectDdlValue(ref ddlJenisLuaran, id_jenis_luaran);

                //ddlKategori.Enabled = false;
                //ddlJenisLuaran.Enabled = false;

                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                Label lblketerangan = (Label)row.FindControl("lblketerangan");
                tbRencanaNama.Text = lblketerangan.Text;

                isiDdlTarget(id_jenis_luaran);
                selectDdlValue(ref ddlTargetStatusLuaran, id_target_capaian_luaran);

                mvLuaran.SetActiveView(vTambahLuaran);
                mvJenisLuaran.SetActiveView(vTambahLuaranForm);
                setLabelJudulTambahLuaran(kd_kategori_jenis_luaran);
            }
            else if (e.CommandName == "tambah")
            {
                lblJudulTambahLuaran.Text = "TAMBAH DATA LUARAN WAJIB";
                ddlKategori.Enabled = true;
                tbRencanaNama.Text = "";
                mvLuaran.SetActiveView(vTambahLuaran);
                mvJenisLuaran.SetActiveView(vTambahLuaranForm);
                //setLabelJudulTambahLuaran(kd_kategori_jenis_luaran);
                isiDdlKategori(arr_kd_kategori_jenis_luaran, arr_nama_kategori_jenis_luaran);
            }

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

        private void isiDdlKategori(string arKdKategori, string arrNamaKategori)
        {
            arKdKategori = arKdKategori.Replace("{", "").Replace("}", "").Replace("\"", "");
            string[] arrKode = arKdKategori.Split(new Char[] { ',' });
            arrKode = arrKode.Distinct().ToArray();
            setLabelJudulTambahLuaran(arrKode[0]);

            arrNamaKategori = arrNamaKategori.Replace("{", "").Replace("}", "").Replace("\"", "");
            string[] arrNama = arrNamaKategori.Split(new Char[] { ',' });
            arrNama = arrNama.Distinct().ToArray();

            ddlKategori.AppendDataBoundItems = true;
            ddlKategori.Items.Clear();
            for (int a = 0; a < arrNama.Length; a++)
            {
                ddlKategori.Items.Add(new ListItem(arrNama[a], arrKode[a]));
            }

            isiDdlJenis(ddlKategori.SelectedValue, int.Parse(ViewState["id_kelompok_luaran"].ToString()));
            isiDdlTarget(ddlJenisLuaran.SelectedValue);
            isiInfoBuktiLuaran();
        }
        private void isiDdlJenis(string kdKategori, int idKelompok)
        {
            DataTable dt = new DataTable();
            objLuaran.ListJenisLuaranPengabdian(ref dt, kdKategori,
                int.Parse(ViewState["id_skema"].ToString()), idKelompok);
            ddlJenisLuaran.DataSource = dt;
            ddlJenisLuaran.DataBind();
        }

        private void isiDdlTarget(string p_id_jenis_luaran)
        {
            DataTable dt2 = new DataTable();
            objLuaran.ListTargetStatusLuaranPengabdian(ref dt2, p_id_jenis_luaran);
            ddlTargetStatusLuaran.DataSource = dt2;
            ddlTargetStatusLuaran.DataBind();
            if (dt2.Rows.Count > 0)
            {
                ddlTargetStatusLuaran.ClearSelection();
                ddlTargetStatusLuaran.Items.FindByValue(dt2.Rows[0]["id_target_capaian_luaran"].ToString()).Selected = true;
                isiInfoBuktiLuaran();
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
                string arr_nama_kategori_jenis_luaran = gvluaranwajib.DataKeys[e.Row.RowIndex]["arr_nama_kategori_jenis_luaran"].ToString();
                string id_kategori_sbk = gvluaranwajib.DataKeys[e.Row.RowIndex]["id_kategori_sbk"].ToString();
                
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

                if (lblnamaluaran.Text == "")
                {
                    string str_nama_kategori_jenis_luaran = arr_nama_kategori_jenis_luaran.Replace("{", "").Replace("}", "").Replace("\"", "");
                    string[] arr = str_nama_kategori_jenis_luaran.Split(new Char[] { ',' });

                    if (arr.Length > 0)
                        lblnamaluaran.Text = " - " + arr[0];

                    if (arr.Length > 1)
                        lblnamaluaran.Text += ", ... ";
                }

                string keterangan = lblketerangan.Text.Replace("{", "").Replace("}", "").Replace("\"", "");
                //keterangan = "- " + keterangan.Replace(",", "<br>- ");
                if (lbltargetluaran.Text == "" || lbltargetluaran.Text == "-")
                {
                    lbTambah.Visible = true;
                }
                else
                {
                    if ((id_kategori_sbk=="4") & (ViewState["id_skema"].ToString()!="15"))
                    {
                        lbTambah.Visible = true;
                    }
                    lbHapus.Visible = true;
                    lbEdit.Visible = true;
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
        private void isiLuarantambahan()
        {
            DataTable dt = new DataTable();
            if (objLuaran.ListTargetLuaranTambahanPengabdian2019(ref dt, Guid.Parse(ViewState["id_usulan"].ToString())))
            {
                ViewState["tahun_ke"] = "";
                //ViewState["awal"] = "";
                gvluarantambahan.DataSource = dt;
                gvluarantambahan.DataBind();
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
            lblThnKeLTambahan.Text = tahun_ke;

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
                //ddlKategoriJenisLuaranEvent(ref mvJenisLuaran, kd_kategori_jenis_luaran, idKelompok);
                //setDataLuaranByKdKategori(id_luaran_dijanjikan, id_jenis_luaran, kd_kategori_jenis_luaran, idKelompok);
                //ddlKategoriPenelitianDasar.ClearSelection();
                //ddlKategoriPenelitianDasar.Enabled = false;
                mvLuaran.SetActiveView(vTambahLuaran);
                mvJenisLuaran.SetActiveView(vTambahLTambahan);
                isiKategoriDdlLuaranTambahan();

                selectDdlValue(ref ddlKategoriLTambahan, kd_kategori_jenis_luaran);
                evt_ddl_luaran_tambahan();
                ktPublikasiNBuku.setData(Guid.Parse(id_luaran_dijanjikan), id_jenis_luaran);
            }
            else if (e.CommandName == "tambah")
            {
                //ddlKategoriPenelitianDasar.Enabled = true;
                //setDataLuaranByKdKategori(Guid.Empty.ToString(), id_jenis_luaran, kd_kategori_jenis_luaran, idKelompok);
                
                mvLuaran.SetActiveView(vTambahLuaran);
                mvJenisLuaran.SetActiveView(vTambahLTambahan);
                isiKategoriDdlLuaranTambahan();
                evt_ddl_luaran_tambahan();
            }
        }


        private void isiKategoriDdlLuaranTambahan()
        {
            ddlKategoriLTambahan.Items.Clear();
            DataTable dt = new DataTable();
            objLuaran.listKategoriLuaranTambahanAbdimas_xiia(ref dt);
            ddlKategoriLTambahan.DataValueField = "kd_kategori_jenis_luaran";
            ddlKategoriLTambahan.DataTextField = "nama_kategori_jenis_luaran";
            //ddlKategoriLTambahan.Items.Add(new ListItem("-- Pilih --", "0"));
            ddlKategoriLTambahan.DataSource = dt;
            ddlKategoriLTambahan.DataBind();

        }

        private void evt_ddl_luaran_tambahan()
        {
            string idKelompokLuaran = "2";
            switch (ddlKategoriLTambahan.SelectedValue)
            {
                case "6":
                case "7":
                case "8":
                    //mvTambahLTambahan.SetActiveView(vPatenNHakcipta);
                    //ktPatenNHakCIpta.refreshddlJenisLuaran(ddlKategoriLTambahan.SelectedValue);
                    //if (ViewState["id_jenis_luaran"] == null)
                    //{
                    //    ViewState["id_jenis_luaran"] = "-1";
                    //}
                    //if (ViewState["id_target_capaian_luaran"] == null)
                    //{
                    //    ViewState["id_target_capaian_luaran"] = "-1";
                    //}
                    //ktPatenNHakCIpta.Refresh(Guid.Parse(ViewState["id_usulan"].ToString()),
                    //    ddlKategoriLTambahan.SelectedValue, idKelompokLuaran, ViewState["tahun_ke"].ToString());
                    //break;

                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                    mvTambahLTambahan.SetActiveView(vPublikasiNBuku);
                    ktPublikasiNBuku.init(ddlKategoriLTambahan.SelectedValue,
                        ViewState["id_usulan"].ToString(), ViewState["tahun_ke"].ToString(),
                        int.Parse(ViewState["id_skema"].ToString()), idKelompokLuaran
                        );
                    break;

            }

        }

        private void setLabelJudulTambahLuaran(string kd_kategori_jenis_luaran)
        {
            switch(kd_kategori_jenis_luaran.Trim())
            {
                case "M":
                case "N":
                    lblRencanaNama.Text = "Rencana Nama Jurnal";
                    break;
                case "O":
                case "P":
                    lblRencanaNama.Text = "Rencana Nama Conference";
                    break;
                case "Q":
                case "R":
                    lblRencanaNama.Text = "Rencana Nama Media Massa";
                    break;
                case "S":
                    lblRencanaNama.Text = "Rencana situs publikasi video kegiatan";
                    break;
                case "T":
                case "U":
                    lblRencanaNama.Text = "Rencana capaian peningkatan";
                    break;


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

        protected void ddlKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiDdlJenis(ddlKategori.SelectedValue, int.Parse(ViewState["id_kelompok_luaran"].ToString()));
            isiDdlTarget(ddlJenisLuaran.SelectedValue);
            isiInfoBuktiLuaran();
            setLabelJudulTambahLuaran(ddlKategori.SelectedValue);
        }

        protected void ddlTargetStatusLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiInfoBuktiLuaran();
        }

        private void isiInfoBuktiLuaran()
        {
            string thnKe = ViewState["tahun_ke"].ToString();
            thnKe = "1"; // 2 3 info sama

            DataTable dt = new DataTable();
            objLuaran.ListInfoBuktiLuaranAbdimas(ref dt, ddlJenisLuaran.SelectedValue,
                int.Parse(thnKe),
                int.Parse(ddlTargetStatusLuaran.SelectedValue),
                int.Parse(ViewState["id_skema"].ToString()),
                int.Parse(ViewState["id_kelompok_luaran"].ToString())
                );
            if (dt.Rows.Count > 0)
            {
                string info = "";
                info += dt.Rows[0]["get_info_bukti_luaran_26_juli"].ToString();

                lblInfoBuktiLuaran.Text = info;
            }
        }
        protected void lbSimpan_Click(object sender, EventArgs e)
        {

            if (tbRencanaNama.Text.Trim() == "")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Rencana nama belum diisi.");
                return;
            }
            Guid p_id_luaran_dijanjikan = Guid.Empty;
            if (ViewState["id_luaran_dijanjikan"] != null)
                if (ViewState["id_luaran_dijanjikan"].ToString().Trim() != "")
                    p_id_luaran_dijanjikan = Guid.Parse(ViewState["id_luaran_dijanjikan"].ToString());
            Guid p_id_usulan = Guid.Parse(ViewState["id_usulan"].ToString());

            //int p_id_kelompok_luaran = 1; // 1=wajib, 2=tambahan
            int p_id_kelompok_luaran = int.Parse(ViewState["id_kelompok_luaran"].ToString());
            int tahun_ke = int.Parse(ViewState["tahun_ke"].ToString());

            if (objLuaran.insupluaranPengabdian(
              p_id_luaran_dijanjikan.ToString(),
              p_id_usulan.ToString(),
              lblThnKe.Text,
              (ddlJenisLuaran.SelectedValue),
              ddlTargetStatusLuaran.SelectedValue,
              "1",
              ViewState["id_kelompok_luaran"].ToString(),
              tbRencanaNama.Text
            ))
            {
                //ViewState["id_luaran_dijanjikan"] = new_id_luaran_dijanjikan;
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                       "Simpan luaran berhasil.");
                //lbBatal.Text = "Kembali";
                //if (OnChildEventBatal != null)
                //    OnChildEventBatal(sender, null);

                mvLuaran.SetActiveView(vDaftarLuaranWajib);
                isiluaranWajib();
                isiLuarantambahan();
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Simpan luaran gagal. Silakan hubungi administrator.");
            }
        }

        protected void lbBatal_Click(object sender, EventArgs e)
        {
            mvLuaran.SetActiveView(vDaftarLuaranWajib);
            isiluaranWajib();
            isiLuarantambahan();
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (objLuaran.deleteLuaranDijanjikanAbdimas_xii(ViewState["id_luaran_dijanjikan"].ToString()))
            {
                isiluaranWajib();
                isiLuarantambahan();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi",
                       "Hapus luaran berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "Hapus luaran gagal. Silakan hubungi administrator.");
            }
        }

        protected void gvluaranwajib_DataBound()
        {
            for (int i = gvluaranwajib.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = gvluaranwajib.Rows[i];
                GridViewRow previousRow = gvluaranwajib.Rows[i - 1];
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

        protected void ddlKategoriLTambahan_SelectedIndexChanged(object sender, EventArgs e)
        {
            evt_ddl_luaran_tambahan();
        }
    }
}