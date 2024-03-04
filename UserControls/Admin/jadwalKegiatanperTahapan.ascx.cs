using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using daftarKonfigurasi;
using System.Data;
using simlitekkes.UIControllers;

namespace simlitekkes.UserControls.Admin
{
    public partial class jadwalKegiatanperTahapan : System.Web.UI.UserControl
    {
        //Models.sistem.konfigurasi obj_konfigurasi;
        jadwalKegiatanPerTahapan objJadwalKegiatan;

        UIControllers.uiGridView obj_uiGVDaftarJadwalKegiatan = new UIControllers.uiGridView();
        UIControllers.uiDropdownList obj_uiDDL = new UIControllers.uiDropdownList();
        //UIControllers.uiModal uiMdl = new UIControllers.uiModal();
        uiModal uiMdl = new uiModal();
        uiNotify noty = new uiNotify();

        Core.manipulasiData obj_MD = new Core.manipulasiData();

        string[] namaKolomsDaftarJadwalKegiatan = { "no_baris", "id_konfig", "id_skema", "nama_skema", "tgl_mulai", "tgl_berakhir" };
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblError.Text = "";
            if (!IsPostBack)
            {
                //ddlKategori.SelectedIndex = 0;
                //if (Application["JenisKegiatan"] != null)
                //    if (!obj_uiDDL.bindToDropDownList(ref ddlJenisKegiatan, (DataTable)Application["JenisKegiatan"], "jenis_kegiatan", "kd_jenis_kegiatan"))
                //    {
                //        lblError.Text = obj_uiDDL.errorMessage;
                //        return;
                //    }
                isiJenisKegiatan(ddlJenisKegiatan);
                ddlJenisKegiatan.SelectedIndex = 0;

                //DataTable objTahunUsulan = (DataTable)Application["TahunUsulan"];
                //foreach (DataRow dr in objTahunUsulan.Rows)
                //{
                //    ddlThnUsulan.Items.Add(new ListItem(dr["thn_usulan"].ToString(), dr["thn_usulan"].ToString()));
                //}
                isiDdlThnUsulan();
                ddlThnUsulan.SelectedIndex = 0;
                setThnPelaksanaan();
                //ddlThnPelaksanaan.Items.Clear();
                //for (int i = DateTime.Now.Year+1; i >= 2013; i--)
                //{
                //    ddlThnPelaksanaan.Items.Add(new ListItem(i.ToString(), i.ToString()));
                //}
                //ddlThnUsulan.Items.Clear();
                //for (int i = DateTime.Now.Year; i >= 2013; i--)
                //{
                //    ddlThnUsulan.Items.Add(new ListItem(i.ToString(), i.ToString()));
                //}

                ddlThnPelaksanaan.SelectedIndex = 0;

                //setThnPelaksanaan();
                setTahapanKegiatan();

                objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
                calcJmlRecord();
                resetPage();
                refreshGvJadwalKegiatan(0);

                MultiViewJadwalKegiatan.SetActiveView(ViewDaftarJadwalKegiatan);
            }
        }

        private void isiDdlThnUsulan()
        {
            ddlThnUsulan.Items.Clear();
            int thnSKg = int.Parse(DateTime.Now.Year.ToString());
            for (int i = thnSKg; i >= 2020; i--)
            {
                ddlThnUsulan.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlThnUsulan.SelectedIndex = 0;
        }

        private void isiJenisKegiatan(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            jadwalKegiatanPerTahapan objJadwalKegiatan=new jadwalKegiatanPerTahapan();
            objJadwalKegiatan.getJenisKegiatan(ref dt);
            obj_uiDDL.bindToDropDownList(ref ddl, dt, "jenis_kegiatan", "kd_jenis_kegiatan");
        }

        protected void ddlJenisKegiatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTahapanKegiatan();

            objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            calcJmlRecord();
            resetPage();
            refreshGvJadwalKegiatan(0);
        }

        protected void ddlThnUsulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            setThnPelaksanaan();

            objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            calcJmlRecord();
            resetPage();
            refreshGvJadwalKegiatan(0);
        }

        protected void ddlThnPelaksanaan_SelectedIndexChanged(object sender, EventArgs e)
        {
            objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            calcJmlRecord();
            resetPage();
            refreshGvJadwalKegiatan(0);
        }

        protected void ddlTahapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            calcJmlRecord();
            resetPage();
            refreshGvJadwalKegiatan(0);
        }

        protected void lbMultiSkema_Click(object sender, EventArgs e)
        {
            uiMdl.ShowModal(this.Page, "multiJadwalModal");
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            resetPage();
            refreshGvJadwalKegiatan(0);
        }

        protected void PagingJadwalKegiatan_PageChanging(object sender, EventArgs e)
        {
            objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            refreshGvJadwalKegiatan(PagingJadwalKegiatan.currentPage);
        }

        protected void gvDaftarJadwalKegiatan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                LinkButton lbtn = (LinkButton)e.Row.FindControl("lbStatus");
                if (lbtn != null)
                {
                    if (drv["kd_sts_aktif"].ToString() == "1")
                    {
                        lbtn.CssClass = "btn btn-success";
                        lbtn.Text = "<i class=\"fa fa-check - square - o\"></i>&nbsp;Buka";
                    }
                    else
                    {
                        lbtn.CssClass = "btn btn-danger";
                        lbtn.Text = "<i class=\"fa fa-square-o\"></i>&nbsp;Tutup";
                    }
                }
            }
        }

        protected void gvDaftarJadwalKegiatan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            switch (e.CommandName.ToUpper())
            {
                case "EDITJADWAL":
                    ViewState["idKonfig"] = e.CommandArgument.ToString() != "" ? e.CommandArgument.ToString() : "0";

                    ViewState["idSkema"] = gvDaftarJadwalKegiatan.DataKeys[gvr.RowIndex]["id_skema"].ToString();
                    lblNamaSkema.Text = gvDaftarJadwalKegiatan.DataKeys[gvr.RowIndex]["nama_skema"].ToString();
                    lblTahapanKegiatan.Text = ddlTahapan.SelectedItem.ToString();
                    lblThnUsulan.Text = ddlThnUsulan.Text;
                    lblThnPelaksanaan.Text = ddlThnPelaksanaan.Text;
                    tbTglMulai.Text = gvDaftarJadwalKegiatan.DataKeys[gvr.RowIndex]["tgl_mulai"].ToString() != "" ? DateTime.Parse(gvDaftarJadwalKegiatan.DataKeys[gvr.RowIndex]["tgl_mulai"].ToString()).ToString("MM/dd/yyyy") : "";
                    tbTglBerakhir.Text = gvDaftarJadwalKegiatan.DataKeys[gvr.RowIndex]["tgl_berakhir"].ToString() != "" ? DateTime.Parse(gvDaftarJadwalKegiatan.DataKeys[gvr.RowIndex]["tgl_berakhir"].ToString()).ToString("MM/dd/yyyy") : "";

                    uiMdl.ShowModal(this.Page, "singleJadwalModal");

                    break;
                case "SETOFF":
                    int idKonfig = e.CommandArgument.ToString() != "" ? int.Parse(e.CommandArgument.ToString()) : 0;
                    int idSkema = int.Parse(gvDaftarJadwalKegiatan.DataKeys[gvr.RowIndex]["id_skema"].ToString());
                    tbTglMulai.Text = gvDaftarJadwalKegiatan.DataKeys[gvr.RowIndex]["tgl_mulai"].ToString() != "" ? DateTime.Parse(gvDaftarJadwalKegiatan.DataKeys[gvr.RowIndex]["tgl_mulai"].ToString()).ToString("MM/dd/yyyy") : "";

                    if (idKonfig != 0)
                    {
                        objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
                        if (!objJadwalKegiatan.setOff(idSkema, tbTglMulai.Text, idKonfig))
                            //lblError.Text = objJadwalKegiatan.errorMessage;
                            //var errorMessage2 = objJadwalKegiatan.errorMessage.ToString();
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objJadwalKegiatan.errorMessage);
                        //return;

                        refreshGvJadwalKegiatan(PagingJadwalKegiatan.currentPage);
                    }
                    break;
            }
        }

        protected void lbSimpanJadwal_Click(object sender, EventArgs e)
        {

            if ((tbTglMulai.Text=="") || (tbTglBerakhir.Text == ""))
            {
                var errorMessage = "Tanggal Awal atau akhir harus di isi dengan benar";
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            };

            objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);

            if (ViewState["idKonfig"].ToString() == "0")
            {
                if (!objJadwalKegiatan.insertDataBaru(int.Parse(ViewState["idSkema"].ToString()),
                    tbTglMulai.Text, tbTglBerakhir.Text))
                    //lblError.Text = objJadwalKegiatan.errorMessage;
                    //var errorMessage2 = objJadwalKegiatan.errorMessage.ToString();
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objJadwalKegiatan.errorMessage);
                //return;

            }
            else
            {
                if (!objJadwalKegiatan.updateData(int.Parse(ViewState["idSkema"].ToString()),
                    tbTglMulai.Text, tbTglBerakhir.Text, int.Parse(ViewState["idKonfig"].ToString())))
                    //lblError.Text = objJadwalKegiatan.errorMessage;
                //lblError.Text = objJadwalKegiatan.errorMessage;
                //var errorMessage2 = objJadwalKegiatan.errorMessage.ToString();
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objJadwalKegiatan.errorMessage);

            }

            refreshGvJadwalKegiatan(PagingJadwalKegiatan.currentPage);
            ViewState.Remove("idSkema");
            ViewState.Remove("idKonfig");
        }

        protected void lbOpenMultiSkema_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);

            if (objJadwalKegiatan.getNoJadwalSkema(ref dt))
            {
                lblTahapanJMS.Text = ddlTahapan.SelectedItem.ToString();
                lblThnUsulanJMS.Text = ddlThnUsulan.Text;
                lblThnPelaksanaanJMS.Text = ddlThnPelaksanaan.Text;

                gvJadwalMultiSkema.DataSource = dt;
                gvJadwalMultiSkema.DataBind();

                tbMultiSkemaTglMulai.Text = "";
                tbMultiSkematglBerakhir.Text = "";

                uiMdl.ShowModal(this.Page, "jadwalMultiSkemaModal");

            }
            else
            {
                //lblError.Text = objJadwalKegiatan.errorMessage;
                //lblError.Text = objJadwalKegiatan.errorMessage;
                //var errorMessage2 = objJadwalKegiatan.errorMessage.ToString();
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objJadwalKegiatan.errorMessage);
                //return;

            }
        }

        protected void lbOpenClearJadwal_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);

            if (objJadwalKegiatan.getCurrentJadwalSkema(ref dt))
            {
                lblTahapanClear.Text = ddlTahapan.SelectedItem.ToString();
                lblThnUsulanClear.Text = ddlThnUsulan.Text;
                lblThnPelaksanaanClear.Text = ddlThnPelaksanaan.Text;

                gvClearJadwalSkema.DataSource = dt;
                gvClearJadwalSkema.DataBind();

                uiMdl.ShowModal(this.Page, "clearJadwalModal");
            }
            else
            {
                //lblError.Text = objJadwalKegiatan.errorMessage;
                //var errorMessage2 = objJadwalKegiatan.errorMessage.ToString();
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objJadwalKegiatan.errorMessage);
                

            }

            //List<int> idKonfig = new List<int>();
            //List<int> idSkema = new List<int>();
            //foreach(DataKey dk in gvDaftarJadwalKegiatan.DataKeys)
            //{
            //    if (dk["id_konfig"].ToString() != "")
            //    {
            //        idKonfig.Add(int.Parse(dk["id_konfig"].ToString()));
            //        idSkema.Add(int.Parse(dk["id_skema"].ToString()));
            //    }
            //}

            //if(idKonfig.Count > 0)
            //{
            //    objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
            //    if (!objJadwalKegiatan.setOffAll(idSkema, tbMultiSkemaTglMulai.Text, idKonfig))
            //        lblError.Text = objJadwalKegiatan.errorMessage;
            //}

            //refreshGvJadwalKegiatan(PagingJadwalKegiatan.currentPage);
        }

        protected void gvJadwalMultiSkema_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                CheckBox cb = (CheckBox)e.Row.FindControl("cbKdStatusAktif");
                if (cb != null)
                {
                    if (drv["kd_sts_aktif"].ToString() == "1")
                    {
                        cb.Checked = true;
                        //lbtn.CssClass = "btn btn-success";
                        //lbtn.Text = "<i class=\"fa fa-check - square - o\"></i>&nbsp;On";
                    }
                    else
                    {
                        cb.Checked = false;
                        //lbtn.CssClass = "btn btn-danger";
                        //lbtn.Text = "<i class=\"fa fa-square-o\"></i>&nbsp;Off";
                    }
                }
            }
        }

        protected void gvClearJadwalSkema_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                CheckBox cb = (CheckBox)e.Row.FindControl("cbKdStatusAktif");
                if (cb != null)
                {
                    if (drv["kd_sts_aktif"].ToString() == "1")
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        cb.Checked = false;
                    }
                }
            }
        }

        protected void lbSimpanMultiSkema_Click(object sender, EventArgs e)
        {
            if ((tbMultiSkemaTglMulai.Text == "") || (tbMultiSkematglBerakhir.Text == ""))
            {
                var errorMessage = "Tanggal Awal atau akhir harus di isi dengan benar";
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            };

            int idx = 0;
            List<int> idSkema = new List<int>();
            foreach (GridViewRow gvr in gvJadwalMultiSkema.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("cbKdStatusAktif");
                if (cb.Checked)
                    idSkema.Add(int.Parse(gvJadwalMultiSkema.DataKeys[idx][0].ToString()));
                idx++;
            }

            if (idSkema.Count == 0)
            {
                var errorMessage = "Tidak ada skema yang di pilih";
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;

            }

            if (idSkema.Count > 0 & tbMultiSkemaTglMulai.Text.Trim() != "" & tbMultiSkematglBerakhir.Text.Trim() != "")
            {
                objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
                if (!objJadwalKegiatan.insertMultiDataBaru(idSkema, tbMultiSkemaTglMulai.Text, tbMultiSkematglBerakhir.Text))
                    //lblError.Text = objJadwalKegiatan.errorMessage;
                    //var errorMessage2 = objJadwalKegiatan.errorMessage.ToString();
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objJadwalKegiatan.errorMessage);
               

            }

            refreshGvJadwalKegiatan(PagingJadwalKegiatan.currentPage);
        }

        protected void lbClearJadwal_Click(object sender, EventArgs e)
        {
            List<int> idKonfig = new List<int>();
            List<int> idSkema = new List<int>();

            int idx = 0;
            foreach (GridViewRow gvr in gvClearJadwalSkema.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("cbKdStatusAktif");
                if (cb.Checked)
                {
                    idKonfig.Add(int.Parse(gvClearJadwalSkema.DataKeys[idx][1].ToString()));
                    idSkema.Add(int.Parse(gvClearJadwalSkema.DataKeys[idx][0].ToString()));
                }
                idx++;
            }

            if (idKonfig.Count > 0)
            {
                objJadwalKegiatan = new jadwalKegiatanPerTahapan(ddlJenisKegiatan.SelectedValue, ddlTahapan.SelectedValue, ddlThnUsulan.SelectedValue, ddlThnPelaksanaan.SelectedValue);
                if (!objJadwalKegiatan.setOffAll(idSkema, idKonfig))
                    //lblError.Text = objJadwalKegiatan.errorMessage;
                    //var errorMessage2 = objJadwalKegiatan.errorMessage.ToString();
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objJadwalKegiatan.errorMessage);
               // return;

            }

            refreshGvJadwalKegiatan(PagingJadwalKegiatan.currentPage);
        }

        private void setThnPelaksanaan()
        {
            //obj_konfigurasi = new Models.sistem.konfigurasi();
            //Models.Admin.daftarTahapanKegiatan objJadwalKegiatan = new Models.Admin.daftarTahapanKegiatan();

            //DataTable objTahunPelaksanaan = objJadwalKegiatan.getTahunPelaksanaan(ddlThnUsulan.SelectedValue);
            //ddlThnPelaksanaan.Items.Clear();
            //foreach (DataRow dr in objTahunPelaksanaan.Rows)
            //{
            //    ddlThnPelaksanaan.Items.Add(new ListItem(dr["thn_pelaksanaan"].ToString(), dr["thn_pelaksanaan"].ToString()));
            //}
            //ddlThnPelaksanaan.SelectedIndex = 0;

            string thnUsulan = ddlThnUsulan.SelectedValue;
            ddlThnPelaksanaan.Items.Clear();
            for (int i = 1; i >= 0; i--)
            {
                ddlThnPelaksanaan.Items.Add(new ListItem((int.Parse(thnUsulan) + i).ToString(), (int.Parse(thnUsulan) + i).ToString()));
            }
            ddlThnPelaksanaan.SelectedIndex = 0;
        }

        private void setTahapanKegiatan()
        {
            DataTable dtTahapanKegiatan = new DataTable();
            Models.Admin.daftarTahapanKegiatan objRefData = new Models.Admin.daftarTahapanKegiatan();
            ddlTahapan.Items.Clear();
            if (objRefData.getTahapanKegiatan(ddlJenisKegiatan.SelectedValue, ref dtTahapanKegiatan))
            {
                foreach (DataRow dr in dtTahapanKegiatan.Rows)
                {
                    ddlTahapan.Items.Add(new ListItem(dr["tahapan"].ToString(), dr["kd_tahapan_kegiatan"].ToString()));
                }
            }
            ddlTahapan.SelectedIndex = 0;
        }

        private void calcJmlRecord()
        {
            if (!objJadwalKegiatan.getJmlRecords())
            {
                //lblError.Text = objJadwalKegiatan.errorMessage;
                //var errorMessage2 = objJadwalKegiatan.errorMessage.ToString();
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objJadwalKegiatan.errorMessage);

                return;
            }
            ViewState["jmlRecords"] = objJadwalKegiatan.numOfRecords;
            lblJmlRecords.Text = objJadwalKegiatan.numOfRecords.ToString();
        }

        private void resetPage()
        {
            PagingJadwalKegiatan.currentPage = 0;
            ViewState["currPageJadwalKegiatan"] = 0;
            PagingJadwalKegiatan.setPaging(int.Parse(ddlJmlBaris.SelectedValue), int.Parse(ViewState["jmlRecords"].ToString()));
        }

        private void refreshGvJadwalKegiatan(int idxPage)
        {
            objJadwalKegiatan.currentPage = idxPage;
            objJadwalKegiatan.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);
            if (!objJadwalKegiatan.getCurrRecords())
            {
                //lblError.Text = objJadwalKegiatan.errorMessage;
                //var errorMessage2 = objJadwalKegiatan.errorMessage.ToString();
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objJadwalKegiatan.errorMessage);
                return;

            }

            if (!obj_uiGVDaftarJadwalKegiatan.bindToGridView(ref gvDaftarJadwalKegiatan, objJadwalKegiatan.currentRecords, namaKolomsDaftarJadwalKegiatan))
                //lblError.Text = obj_uiGVDaftarJadwalKegiatan.errorMessage;
            noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", obj_uiGVDaftarJadwalKegiatan.errorMessage);

        }

    }
}