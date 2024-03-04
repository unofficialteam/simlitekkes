using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using System.Data;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class targetLuaranPengabdian : System.Web.UI.UserControl
    {
        Models.login objLogin;
        Models.Pengusul.luaran objLuaran = new Models.Pengusul.luaran();
        //int lama_kegiatan = 3;
        //string id_usulan = "a4360037-62dc-4e75-ad89-2a5e862e1001";
        //uiGridView obj_uiGridView = new uiGridView();
        uiDropdownList obj_uiDropdownList = new uiDropdownList();
        Core.manipulasiData objManipData = new Core.manipulasiData();
        //ViewState["id_usulan"] = id_usulan;
        //ViewState["lama_kegiatan"] = lama_kegiatan;




        uiNotify noty = new uiNotify();
        uiModal obj_uiMdl = new uiModal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];



            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);
            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.ddlThn);
            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.ddlJenisLuaran);
            //if (!IsPostBack)
            //{
            //    mvLuaran.SetActiveView(vDaftar);
            //    isiSintaPengusul();
            //}
        }

        public void setDataLuaranpengabdian(string pidUsulan, int plamakegiatan, int urutan_thn, string pidUsulankegiatan,
            string pnamaSkema, int pidskema)
        {
            ViewState["id_usulan"] = pidUsulan;
            ViewState["lama_kegiatan"] = plamakegiatan;
            ViewState["id_usulan_kegiatan"] = pidUsulankegiatan;
            ViewState["urutan_thn"] = urutan_thn;
            ViewState["nama_Skema"] = pnamaSkema;
            ViewState["id_skema"] = pidskema;

            lblUrutanUsulan.Text = ViewState["urutan_thn"].ToString();
            lblLamaUsulan.Text = ViewState["lama_kegiatan"].ToString();
            lblSkema.Text = ViewState["nama_Skema"].ToString();

            isiluaran();
        }


        public void isiluaran()
        {
            clearitem();
            isiLuaranWajib();
            isiLuarantambahan();
            mvLuaran.SetActiveView(vDaftarLuaranWajib);
            ddlThn.Items.Clear();
            for (int i = int.Parse(lblUrutanUsulan.Text) ; i < int.Parse(ViewState["lama_kegiatan"].ToString()) + 1; i++)
            {
                ddlThn.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }

        protected void label_ket(int e)
        {
            lbketJanjiLuaran.Visible = true;
            tbKetLuaran.Visible = true;
            tbKetLuaran.Text = "";
            tbKetLuaran2.Visible = false;
            tbKetLuaran3.Visible = false;
            lbketJanjiLuaran3.Visible = false;
            lbketJanjiLuaran2.Visible = false;
            tbKetLuaran3.Visible = false;
            switch (e)
            {
                case 1:
                case 2:
                case 3:
                        lbketJanjiLuaran.Text = "Nama Jurnal";
                        lbketJanjiLuaran2.Text = "URL Jurnal (Jika ada)";
                        lbketJanjiLuaran2.Visible = true;
                        tbKetLuaran2.Visible = true;

                        tbKetLuaran3.Visible = false;
                        lbketJanjiLuaran3.Visible = false;

                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                        lbketJanjiLuaran.Text = "Nama Konferensi";
                    break;
                case 11:
                case 12:
                case 54:
                    lbketJanjiLuaran.Text = "Nama media";
                    break;
                case 13:
                        lbketJanjiLuaran.Text = "Nama Website";
                    break;
                case 39:
                default:
                    lbketJanjiLuaran.Visible = false;
                    tbKetLuaran.Visible = false;
                    break;
            }
        }

        //protected void label_ket_tambahan(int e)
        //{
        //    lbketJanjiLuaran.Visible = true;
        //    tbKetLuaran.Text = "";
        //    tbKetLuaran.Visible = true;
        //    tbKetLuaran2.Visible = false;
        //    tbKetLuaran3.Visible = false;
        //    lbketJanjiLuaran3.Visible = false;
        //    tbKetLuaran3.Visible = false;
        //    switch (e)
        //    {
        //        case 1:
        //        case 2:
        //        case 21:
        //            lbketJanjiLuaran.Text = "Nama Jurnal";
        //            //lbketJanjiLuaran2.Visible = false;
        //            //tbKetLuaran2.Visible = false;
        //            //tbKetLuaran3.Visible = false;
        //            //lbketJanjiLuaran3.Visible = false;
        //            //tbKetLuaran3.Visible = false;                   
        //            break;
        //        case 3:
        //        case 22:
        //        case 4:
        //        case 5:
        //        case 6:
        //        case 23:
        //            lbketJanjiLuaran.Text = "Nama Konferensi dituju";
        //            //lbketJanjiLuaran2.Text = "Nama Konferensi dituju ke 2";
        //            //lbketJanjiLuaran3.Text = "Nama Konferensi dituju ke 3";
        //            //lbketJanjiLuaran2.Visible = true;
        //            //tbKetLuaran2.Visible = true;
        //            //lbketJanjiLuaran3.Visible = true;
        //            //tbKetLuaran3.Visible = true;

        //            //}
        //            break;
        //        case 37:
        //        case 38:
        //            lbketJanjiLuaran.Text = "Nama Seminar";
        //            break;
        //        case 39:
        //            lbketJanjiLuaran.Text = "Nama penerbit book chapter ";
        //            //lbketJanjiLuaran2.Text = "Nama penerbit book chapter ke 2";
        //            //lbketJanjiLuaran3.Text = "Nama penerbit book chapter ke 3";
        //            break;
        //        case 8:
        //        case 9:
        //        case 10:
        //        case 11:
        //        case 12:
        //        case 13:
        //        case 14:
        //        case 15:
        //        case 16:
        //        case 17:
        //        case 24:
        //        case 25:
        //        case 26:
        //        case 32:
        //        case 33:
        //        case 34:
        //            lbketJanjiLuaran.Visible = false;
        //            tbKetLuaran.Visible = false;
        //            tbKetLuaran.Text = "-";
        //            break;
        //        default:
        //            lbketJanjiLuaran.Text = "Keterangan";
        //            break;
        //    }
        //}


        

        private void isiJenisLuaranwajib(string id)
        {
            DataTable dtJenisLuaran = new DataTable();
            ddlJenisLuaran.Items.Clear();
            if (objLuaran.ListJenisLuaranWajibPengabdian(ref dtJenisLuaran, Guid.Parse(ViewState["id_usulan"].ToString()), int.Parse(ddlThn.SelectedValue.ToString()),id))
            {
                if (dtJenisLuaran.Rows.Count > 0)
                {
                    if (obj_uiDropdownList.bindToDropDownList(ref ddlJenisLuaran, dtJenisLuaran, "nama_jenis_luaran", "id_jenis_luaran"))
                    {
                        //ddlJenisLuaran.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
                        //ddlJenisLuaran.SelectedIndex = 0;
                        return;

                    }
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "luaran wajib tahun ke " + ddlThn.SelectedValue + " sudah terpenuhi");
                    clearitem();
                }
            }
        }

        private void clearitem()
        {
            ddlJenisLuaran.Items.Clear();
            ddlTarget.Items.Clear();
            tbKetLuaran.Text = "";
            tbKetLuaran2.Text = "";
            tbKetLuaran3.Text = "";
            tbKetLuaran2.Visible = false;
            lbketJanjiLuaran2.Visible = false;
            tbKetLuaran3.Visible = false;
            lbketJanjiLuaran3.Visible = false;
            lbketJanjiLuaran.Visible = true;
        }

        private void isiJenisLuarantambahan()
        {
            DataTable dtJenisLuaran = new DataTable();
            ddlJenisLuaran.Items.Clear();
            //ddlJenisHKI.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            if (objLuaran.ListLuaranTambahanPengabdian(ref dtJenisLuaran, Guid.Parse(ViewState["id_usulan"].ToString()), int.Parse(ddlThn.SelectedValue.ToString())))
            {
                if (dtJenisLuaran.Rows.Count > 0)
                {
                    if (obj_uiDropdownList.bindToDropDownList(ref ddlJenisLuaran, dtJenisLuaran, "nama_jenis_luaran", "id_jenis_luaran"))
                    {
                        return;
                    }
                }
                else
                {

                }
            }
        }

        private void isiTargetLuaranWajib()
        {
            ddlTarget.Items.Clear();
            DataTable dtTargetLuaran = new DataTable();
            //ddlJenisHKI.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            if (ddlJenisLuaran.Items.Count > 0)
            {
                if (objLuaran.ListTargetLuaranWajibPengabdian(ref dtTargetLuaran, Guid.Parse(ViewState["id_usulan"].ToString()), int.Parse(ddlThn.SelectedValue.ToString()),  int.Parse(ddlJenisLuaran.SelectedValue.ToString())))
                {
                    if (dtTargetLuaran.Rows.Count > 0)
                    {
                        if (obj_uiDropdownList.bindToDropDownList(ref ddlTarget, dtTargetLuaran, "nama_target_capaian_luaran", "id_target_capaian_luaran"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Luaran Wajib Tahun " + ddlThn.SelectedValue.ToString() + " Sudah Terpenuhi ");

                    }
                }
            }
        }
        private void isiTargetLuaranTambahan()
        {
            ddlTarget.Items.Clear();
            DataTable dtTargetLuaran = new DataTable();
            //ddlJenisHKI.Items.Add(new System.Web.UI.WebControls.ListItem("--Pilih--", "0"));
            if (objLuaran.ListTargetLuaranPengabdian(ref dtTargetLuaran, int.Parse(ddlJenisLuaran.SelectedValue.ToString())))
            {
                if (dtTargetLuaran.Rows.Count > 0)
                {
                    if (obj_uiDropdownList.bindToDropDownList(ref ddlTarget, dtTargetLuaran, "nama_target_capaian_luaran", "id_target_capaian_luaran"))
                    {
                        return;
                    }
                }
                else
                {

                }
            }
        }

        private void isiLuaranWajib()
        {

            DataTable dt = new DataTable();
            if (objLuaran.ListTargetLuaranWajibPengabdian(ref dt, Guid.Parse(ViewState["id_usulan"].ToString())))
            {
                ViewState["tahun_ke"] = "";
                ViewState["show_kategori"] = "";
                gvluaranwajib.DataSource = dt;
                gvluaranwajib.DataBind();
                Boolean cek = (CekLuaran(gvluaranwajib));
                if (cek)
                {
                    //noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Semua luaran wajib sudah terpenuhi");
                    Label1.Visible = true;
                    Label2.Visible = false;
                    //Label1.Text = "Isian luaran wajib sudah lengkap";
                    //Label1.CssClass = "class='label bg-success'";

                }
                else
                {
                    //noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Semua luaran wajib sudah terpenuhi");
                    //Label1.Visible = false;
                    //Label1.Text = "Isian luaran wajib belum lengkap";
                    //Label1.CssClass = "class='label label-danger'";
                    Label1.Visible = false;
                    Label2.Visible = true;

                }

            }

        }

        private void isiLuarantambahan()
        {
            DataTable dt = new DataTable();
            if (objLuaran.ListTargetLuaranTambahanPengabdian(ref dt, Guid.Parse(ViewState["id_usulan"].ToString())))
            {
                ViewState["tahun_ke"] = "";
                //ViewState["awal"] = "";
                gvluarantambahan.DataSource = dt;
                gvluarantambahan.DataBind();
            }

        }


        //public class GridDecorator
        //{
        //    public static void MergeRows(GridView gridView)
        //    {
        //        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        //        {
        //            GridViewRow row = gridView.Rows[rowIndex];
        //            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

        //            //for (int i = 0; i < 1; i++)
        //            //{
        //                if (row.Cells[0].Text == previousRow.Cells[0].Text)
        //                {
        //                    //row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
        //                    //                       previousRow.Cells[i].RowSpan + 1;
        //                    previousRow.Cells[0].Visible = false;
        //                }
        //            //}
        //        }
        //    }
        //}


        public static Boolean CekLuaran(GridView gridView)
        {
            Boolean cek = true;
            //for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            //for (int i = 0; i < gridView.Rows.Count - 1; i++)
            //{
            //    GridViewRow row = gridView.Rows[i];
            //    //GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            //    //for (int i = 0; i < 1; i++)
            //    //{
            //    if ((row.Cells[1].Text == "") || (row.Cells[0].Text == "-"))
            //    {
            //        //row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
            //        //                       previousRow.Cells[i].RowSpan + 1;
            //        cek = false;
            //    }
            //    //}
            //}
            foreach (GridViewRow gvr in gridView.Rows)
            {
                Label namaluaran = gvr.FindControl("lblnamaluaran") as Label;
                if (namaluaran != null)
                {
                    if ((namaluaran.Text == "") ) //|| (namaluaran.Text == "-")
                    {
                        cek = false;
                    }
                }
            }

            return cek;
        }

        protected void gvluarantambahan_PreRender(object sender, EventArgs e)
        {
            //GridDecorator.MergeRows(gvluarantambahan);
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
                string id_kategori = gvluaranwajib.DataKeys[e.Row.RowIndex]["id_kategori"].ToString();
                //    LinkButton lbUnggahLaporanakhir = new LinkButton();
                //    lbUnggahLaporanakhir = (LinkButton)e.Row.FindControl("lbUnggahLaporanakhir");

                Label lblTahunke = new Label();
                LinkButton lbTambah = new LinkButton();
                LinkButton lbHapus = new LinkButton();
                Label lbltargetluaran = new Label();
                Label lblnamaluaran = new Label();
                Label lblkategori = new Label();
                lblTahunke = (Label)e.Row.FindControl("lblTahunke");
                lbTambah = (LinkButton)e.Row.FindControl("lbTambah");
                lbHapus = (LinkButton)e.Row.FindControl("lbHapus");
                lbltargetluaran = (Label)e.Row.FindControl("lbltargetluaran");
                lblnamaluaran = (Label)e.Row.FindControl("lblnamaluaran");
                lblkategori = (Label)e.Row.FindControl("lblkategori");
                

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
                if (id_luaran_dijanjikan == "")
                {
                    lbTambah.Visible = true;
                    lbHapus.Visible = false;
                }
                else
                {
                    lbTambah.Visible = false;
                    if ((id_jenis_luaran == "3") || (id_jenis_luaran == "4") || (id_jenis_luaran == "22") || (id_jenis_luaran == "39"))
                    {
                        if (ViewState["tahun_ke"].ToString() == "")
                        {
                            lbHapus.Visible = true;
                        }
                        else if (ViewState["tahun_ke"].ToString() == tahun_ke)
                        {
                            if (ViewState["id_jenis_luaran"].ToString() == id_jenis_luaran)
                            {
                                lbHapus.Visible = false;
                            }
                            else
                            {
                                lbHapus.Visible = true;
                            }
                        }
                        else
                        {
                            lbHapus.Visible = true;
                        }


                    }
                    else
                    {
                        lbHapus.Visible = true;
                    }


                };

                if (ViewState["tahun_ke"].ToString() == "")
                {
                    if (lblTahunke != null)
                    {
                        lblTahunke.Visible = true;
                        lblTahunke.Text = "Tahun " + tahun_ke.ToString();
                        lblkategori.Visible = true;
                    }
                    ViewState["tahun_ke"] = tahun_ke;
                }
                else if (ViewState["tahun_ke"].ToString() == tahun_ke)
                {
                    if (lblTahunke != null) { lblTahunke.Visible = false; lblkategori.Visible = false; }
                    ViewState["tahun_ke"] = tahun_ke;
                }
                else
                {
                    if (lblTahunke != null)
                    {
                        lblTahunke.Visible = true;
                        lblkategori.Visible = true;
                        lblTahunke.Text = "Tahun " + tahun_ke.ToString();
                        ViewState["tahun_ke"] = tahun_ke;
                    }
                }
                ViewState["id_jenis_luaran"] = id_jenis_luaran;
                if (id_kategori == "4")
                { 
                if (ViewState["show_kategori"].ToString() != lblTahunke.Text)
                {
                    lblkategori.Visible = true;
                }
                else
                {
                    lblkategori.Visible = false;

                }
                ViewState["show_kategori"] = lblTahunke.Text;
                }
                else
                {
                    lblkategori.Visible = true;
                }
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

                //    LinkButton lbUnggahLaporanakhir = new LinkButton();
                //    lbUnggahLaporanakhir = (LinkButton)e.Row.FindControl("lbUnggahLaporanakhir");

                Label lblTahunke = new Label();
                Label lbltargetluaran = new Label();
                LinkButton lbHapus = new LinkButton();
                lblTahunke = (Label)e.Row.FindControl("lblTahunke");
                lbHapus = (LinkButton)e.Row.FindControl("lbHapus");
                lbltargetluaran = (Label)e.Row.FindControl("lbltargetluaran");

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
                if (id_luaran_dijanjikan == "")
                {
                    lbHapus.Visible = false;
                }
                else
                {
                    lbHapus.Visible = true;
                };

                if (ViewState["tahun_ke"].ToString() == "")
                {
                    if (lblTahunke != null) { lblTahunke.Visible = true; lblTahunke.Text = "Tahun " + tahun_ke.ToString(); }
                    ViewState["tahun_ke"] = tahun_ke;
                }
                else if (ViewState["tahun_ke"].ToString() == tahun_ke)
                {
                    if (lblTahunke != null) { lblTahunke.Visible = false; }
                    ViewState["tahun_ke"] = tahun_ke;
                }
                else
                {
                    if (lblTahunke != null) { lblTahunke.Visible = true; lblTahunke.Text = "Tahun " + tahun_ke.ToString(); }
                    ViewState["tahun_ke"] = tahun_ke;
                }
            }

        }

        protected void lbtambahwajib_Click(object sender, EventArgs e)
        {
            //lbKet.Text = "Tambah Data Luaran Wajib";
            //mvLuaran.SetActiveView(vTambahLuaran);
            //isiJenisLuaranwajib();
            ////trigjenisluaran();
            //if (ddlJenisLuaran.SelectedValue.ToString() != "")
            //{
            //    label_ket(int.Parse(ddlJenisLuaran.SelectedValue.ToString()));
            //}
            //isiTargetLuaranWajib();
        }

        private void trigjenisluaran()
        {
            if ((ddlJenisLuaran.SelectedValue.ToString() == "3") || (ddlJenisLuaran.SelectedValue.ToString() == "4") || (ddlJenisLuaran.SelectedValue.ToString() == "22") || (ddlJenisLuaran.SelectedValue.ToString() == "39"))
            {
                lbketJanjiLuaran2.Visible = true;
                tbKetLuaran2.Visible = true;
                lbketJanjiLuaran3.Visible = true;
                tbKetLuaran3.Visible = true;
            }
            else
            if ((ddlJenisLuaran.SelectedValue.ToString() == "4") & (ViewState["id_skema"].ToString() == "76"))
            {
                lbketJanjiLuaran2.Visible = true;
                tbKetLuaran2.Visible = true;
                lbketJanjiLuaran3.Visible = false;
                tbKetLuaran3.Visible = false;
            }
            else
            if ((ddlJenisLuaran.SelectedValue.ToString() == "2") & (ViewState["id_skema"].ToString() == "76"))
            {
                lbketJanjiLuaran2.Visible = true;
                tbKetLuaran2.Visible = true;
                lbketJanjiLuaran3.Visible = false;
                tbKetLuaran3.Visible = false;
            }
            else
            {
                lbketJanjiLuaran2.Visible = false;
                tbKetLuaran2.Visible = false;
                lbketJanjiLuaran3.Visible = false;
                tbKetLuaran3.Visible = false;

            }
        }


        protected void lbtambahtambahan_Click(object sender, EventArgs e)
        {
            lbKet.Text = "Tambah Data Luaran Tambahan";
            mvLuaran.SetActiveView(vTambahLuaran);
            isiJenisLuarantambahan();
            isiTargetLuaranTambahan();
            if (ddlJenisLuaran.SelectedValue.ToString() != "")
            {
                label_ket(int.Parse(ddlJenisLuaran.SelectedValue.ToString()));
            }
            ddlThn.Enabled = true;
        }

        protected void ddlThn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbKet.Text == "Tambah Data Luaran Wajib")
            {
                isiJenisLuaranwajib(ViewState["id_kategori"].ToString());
                isiTargetLuaranWajib();
            }
            ddlThn.Enabled = true;
        }

        protected void ddlJenisLuaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbKet.Text == "Tambah Data Luaran Wajib")
            {
                isiTargetLuaranWajib();
                //if ((ddlJenisLuaran.SelectedValue.ToString() == "3") || (ddlJenisLuaran.SelectedValue.ToString() == "4") || (ddlJenisLuaran.SelectedValue.ToString() == "22") || (ddlJenisLuaran.SelectedValue.ToString() == "39"))
                //{
                //    lbketJanjiLuaran2.Visible = true;
                //    tbKetLuaran2.Visible = true;
                //    lbketJanjiLuaran3.Visible = true;
                //    tbKetLuaran3.Visible = true;

                //}
                //else
                //{
                //    lbketJanjiLuaran2.Visible = false;
                //    tbKetLuaran2.Visible = false;
                //    lbketJanjiLuaran3.Visible = false;
                //    tbKetLuaran3.Visible = false;

                //}
                //trigjenisluaran();
                label_ket(int.Parse(ddlJenisLuaran.SelectedValue.ToString()));
            }
            else
            {
                isiTargetLuaranTambahan();
                label_ket(int.Parse(ddlJenisLuaran.SelectedValue.ToString()));

            };
            //if (ddlJenisLuaran.SelectedValue.ToString() != "")
            //{
            //    label_ket_tambahan(int.Parse(ddlJenisLuaran.SelectedValue.ToString()));
            //};

        }

        protected void lbSimpan_click(object sender, EventArgs e)
        {
            string keterangan;
            if ((ddlJenisLuaran.Items.Count == 0) || (ddlTarget.Items.Count == 0))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Isian tidak lengkap");
                return;
            };
            if ((tbKetLuaran.Text == "") && (tbKetLuaran.Visible == true))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Isian Keterangan belum diisi");
                return;
            };
            //if ((tbKetLuaran2.Text == "") && (tbKetLuaran2.Visible == true))
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Isian konferens 1 di tuju belum di isi");
            //    return;
            //};
            //if ((tbKetLuaran2.Text == "") && (tbKetLuaran2.Visible == true))
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Isian konferens 2 di tuju belum di isi");
            //    return;
            //};
            //if ((tbKetLuaran3.Text == "") && (tbKetLuaran3.Visible == true))
            //{
            //    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Isian konferens 3 di tuju belum di isi");
            //    return;
            //};

            if (lbKet.Text == "Tambah Data Luaran Wajib")
            {
                if (tbKetLuaran2.Visible == true)
                {
                    if (tbKetLuaran2.Text != "")
                    {
                         keterangan = "Nama Jurnal : " + tbKetLuaran.Text + "<br />" + "Url : " + tbKetLuaran2.Text;
                    }
                    else
                    {
                        keterangan = "Nama Jurnal : " + tbKetLuaran.Text;
                    };
                    if (objLuaran.insupluaranPengabdian(ViewState["id_usulan"].ToString(), ddlThn.SelectedValue, ddlJenisLuaran.SelectedValue, ddlTarget.SelectedValue, "1", "1", keterangan))
                    {
                        //isiluaran();

                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah data berhasil");

                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah data prosiding ke 2 gagal. error = " + objLuaran.errorMessage);

                    };
                }
                else if (objLuaran.insupluaranPengabdian(ViewState["id_usulan"].ToString(), ddlThn.SelectedValue, ddlJenisLuaran.SelectedValue, ddlTarget.SelectedValue, "1", "1", tbKetLuaran.Text))
                {

                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah data berhasil");

                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah data gagal error = " + objLuaran.errorMessage);
                };

                //if (tbKetLuaran3.Visible == true)
                //{
                //    if (objLuaran.insupluaranPengabdian(ViewState["id_usulan"].ToString(), ddlThn.SelectedValue, ddlJenisLuaran.SelectedValue, ddlTarget.SelectedValue, "1", "1", tbKetLuaran3.Text))
                //    {
                //        //isiluaran();
                //
                //        //noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah data berhasil");
                //
                //    }
                //    else
                //    {
                //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah data prosiding ke 3 gagal. error = " + objLuaran.errorMessage);
                //
                 //   }


                //}

                isiluaran();

                //Boolean cek = (CekLuaran(gvluaranwajib));
                //if (cek)
                //{
                //noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Semua luaran wajib sudah terpenuhi");
                //Label1.Visible = true;
                //Label1.Text = "Isian luaran wajib sudah lengkap";
                //Label1.CssClass = "class='label bg-success'";
                //     Label1.Visible = true;
                //     Label2.Visible = false;


                //}
                //else
                // {
                //noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Semua luaran wajib sudah terpenuhi");
                //Label1.Visible = false;
                //Label1.Text = "Isian luaran wajib belum lengkap";
                //Label1.CssClass= "class='label label-danger'";
                //      Label1.Visible = false;
                //      Label2.Visible = true;

                //  }

            }
            else
            {
                if (tbKetLuaran2.Visible == true)
                {
                    if (tbKetLuaran2.Text != "")
                    {
                        keterangan = "Nama Jurnal : " + tbKetLuaran.Text + "<br />" + "Url : " + tbKetLuaran2.Text;
                    }
                    else
                    {
                        keterangan = "Nama Jurnal : " + tbKetLuaran.Text;
                    };
                    if (objLuaran.insupluaranPengabdian(ViewState["id_usulan"].ToString(), ddlThn.SelectedValue, ddlJenisLuaran.SelectedValue, ddlTarget.SelectedValue, "1", "2", keterangan))
                    {
                        //isiluaran();

                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah data berhasil");

                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah data prosiding ke 2 gagal. error = " + objLuaran.errorMessage);

                    };
                }
                else if (objLuaran.insupluaranPengabdian(ViewState["id_usulan"].ToString(), ddlThn.SelectedValue, ddlJenisLuaran.SelectedValue, ddlTarget.SelectedValue, "1", "2", tbKetLuaran.Text))
                {

                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Tambah data berhasil");

                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tambah data gagal error = " + objLuaran.errorMessage);
                };
                isiluaran();


            }
        }

        protected void lbBatal_click(object sender, EventArgs e)
        {
            isiluaran();
        }

        protected void gvluarantambahan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "hapus")
            {
                string id_luaran_dijanjikan = gvluarantambahan.DataKeys[rowIndex]["id_luaran_dijanjikan"].ToString();
                ViewState["id_luaran_dijanjikan"] = id_luaran_dijanjikan;
                ViewState["kategorijeniluaran"] = "Tambahan";
                obj_uiMdl.ShowModal(this.Page, "myModal");
            }
        }

        protected void gvluaranwajib_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "hapus")
            {
                string id_luaran_dijanjikan = gvluaranwajib.DataKeys[rowIndex]["id_luaran_dijanjikan"].ToString();
                string id_jenis_luaran = gvluaranwajib.DataKeys[rowIndex]["id_jenis_luaran"].ToString();
                string tahun_ke = gvluaranwajib.DataKeys[rowIndex]["tahun_ke"].ToString();
                ViewState["id_luaran_dijanjikan"] = id_luaran_dijanjikan;
                ViewState["id_jenis_luaran"] = id_jenis_luaran;
                ViewState["kategorijeniluaran"] = "Wajib";
                ViewState["tahun_ke"] = tahun_ke;
                obj_uiMdl.ShowModal(this.Page, "myModal");
            }
            else if (e.CommandName == "tambah")
            {
                string tahun_ke = gvluaranwajib.DataKeys[rowIndex]["tahun_ke"].ToString();
                string id_kategori = gvluaranwajib.DataKeys[rowIndex]["id_kategori"].ToString();
                ViewState["id_kategori"] = id_kategori;

                lbKet.Text = "Tambah Data Luaran Wajib";
                mvLuaran.SetActiveView(vTambahLuaran);
                if (ddlThn.Items.Count > 0)
                {
                    //ddlThn.SelectedIndex = int.Parse(tahun_ke) - 1;
                    ddlThn.Text = tahun_ke;
                };
                isiJenisLuaranwajib(id_kategori);
                if (ddlJenisLuaran.SelectedValue.ToString() != "")
                {
                    label_ket(int.Parse(ddlJenisLuaran.SelectedValue.ToString()));
                };
                isiTargetLuaranWajib();

                ddlThn.Enabled = false;
                //trigjenisluaran();
                //ddlThn.Text = (int.Parse(tahun_ke)-1).ToString();
            }
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            string id_luaran_dijanjikan = ViewState["id_luaran_dijanjikan"].ToString();
            if (ViewState["kategorijeniluaran"].ToString() == "Wajib")
            {
                //if ((ViewState["id_jenis_luaran"].ToString() == "3") || (ViewState["id_jenis_luaran"].ToString() == "4") || (ViewState["id_jenis_luaran"].ToString() == "22"))
                //{
                //    if (objLuaran.deleteJanjiLuaranprosiding(ViewState["id_usulan"].ToString(), ViewState["tahun_ke"].ToString()))
                //    {
                //        isiluaran();
                //        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");

                //    }
                //    else
                //    {
                //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objLuaran.errorMessage);
                //    }

                //}
                //else
                //if (ViewState["id_jenis_luaran"].ToString() == "39")
                //{
                //    if (objLuaran.deleteJanjiLuaranbookchapter(ViewState["id_usulan"].ToString(), ViewState["tahun_ke"].ToString()))
                //    {
                //        isiluaran();
                //        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");

                //    }
                //    else
                //    {
                //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objLuaran.errorMessage);
                //    }

                //}
                //else
                //if ((ViewState["id_jenis_luaran"].ToString() == "2") && (ViewState["id_skema"].ToString() == "76"))
                //{
                //    if (objLuaran.deleteJanjiLuaranpublikasi(ViewState["id_usulan"].ToString(), ViewState["tahun_ke"].ToString()))
                //    {
                //        isiluaran();
                //        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");

                //    }
                //    else
                //    {
                //        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objLuaran.errorMessage);
                //    }

                //}
                //else
                //{
                    if (objLuaran.deleteJanjiLuaranpengabdiian(id_luaran_dijanjikan))
                    {
                        isiluaran();
                        noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");

                    }
                    else
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objLuaran.errorMessage);
                    }
                //}
            }
            else
            {
                if (objLuaran.deleteJanjiLuaranpengabdiian(id_luaran_dijanjikan))
                {
                    isiluaran();
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil");

                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objLuaran.errorMessage);
                }

            }
        }

        protected void lbinfo_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            objLuaran.infoluaran(ref dt, ViewState["id_usulan_kegiatan"].ToString());
            if (dt.Rows.Count > 0)
            {
                //Label4.Text= dt.Rows[0]["id_sinta"].ToString();
                GvInfoLuaran.DataSource = dt;
                GvInfoLuaran.DataBind();
            }
            obj_uiMdl.ShowModal(this.Page, "myModal1");
        }
    }
}