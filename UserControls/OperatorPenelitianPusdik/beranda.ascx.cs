using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPenelitianPusdik
{
    public partial class beranda : System.Web.UI.UserControl
    {
        Models.OperatorPenelitianPusdik.beranda modelData = new Models.OperatorPenelitianPusdik.beranda();
        Models.login objLogin;
        uiDropdownList obj_uiDropdownlist = new uiDropdownList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiTahunUsulan(ddlTahunUsulan);
                isiTahunPelaksanaan(ddlTahunPelaksanaan);
                loadData();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "$('.select2').select2()", true);
        }

        protected void ddlFilterTahunUsulan(object sender, EventArgs e)
        {
            loadData();
        }

        protected void ddlFilterTahunPelaksanaan(object sender, EventArgs e)
        {
            isiTahunPelaksanaan(ddlTahunPelaksanaan);
            loadData();
        }

        private void isiTahunUsulan(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelData.getTahunUsulan(ref data);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "thn_usulan_kegiatan", "thn_usulan_kegiatan");
            ddl.SelectedIndex = 0;
        }

        private void isiTahunPelaksanaan(DropDownList ddl)
        {
            DataTable data = new DataTable();
            modelData.getTahunPelaksanaan(ref data, ddlTahunUsulan.SelectedValue);
            obj_uiDropdownlist.bindToDropDownList(ref ddl, data, "thn_pelaksanaan_kegiatan", "thn_pelaksanaan_kegiatan");
            ddl.SelectedIndex = 0;
        }

        private void loadData()
        {
            DataTable dt = new DataTable();
            modelData.getRekapTahapanPengajuanUsulan(ref dt, ddlTahunUsulan.SelectedValue, ddlTahunPelaksanaan.SelectedValue);
            List<string> x_axis = new List<string>();
            foreach (DataRow item in dt.Rows)
            {
                DateTime tgl = (DateTime)item["tgl_pelaksanaan"];
                string strTgl = String.Format("{0:d/M/yyyy}", tgl);

                //if (!x_axis.Contains(item["tgl_pelaksanaan"].ToString()))
                //{
                //    x_axis.Add(item["tgl_pelaksanaan"].ToString());
                //}

                if (!x_axis.Contains(strTgl))
                {
                    x_axis.Add(strTgl);
                }

            }
            int[] y_pembuatan_usulan = new int[x_axis.Count];
            int[] y_kirim_usulan = new int[x_axis.Count];
            int[] y_persetujuan_ka_puslit = new int[x_axis.Count];

            for (int i = 0; i < x_axis.Count; i++)
            {
                y_pembuatan_usulan[i] = 0;
                y_kirim_usulan[i] = 0;
                y_persetujuan_ka_puslit[i] = 0;
                foreach (DataRow item in dt.Rows)
                {
                    DateTime tgl = (DateTime)item["tgl_pelaksanaan"];
                    string strTgl = String.Format("{0:d/M/yyyy}", tgl);

                    if (strTgl.Equals(x_axis[i]) && (int)item["urutan"] == 1)
                    {
                        y_pembuatan_usulan[i] = (int)item["jml"];
                    }
                    if (strTgl.Equals(x_axis[i]) && (int)item["urutan"] == 2)
                    {
                        y_kirim_usulan[i] = (int)item["jml"];
                    }
                    if (strTgl.Equals(x_axis[i]) && (int)item["urutan"] == 3)
                    {
                        y_persetujuan_ka_puslit[i] = (int)item["jml"];
                    }

                    //if (item["tgl_pelaksanaan"].ToString().Equals(x_axis[i]) && (int)item["urutan"] == 1)
                    //{
                    //    y_pembuatan_usulan[i] = (int)item["jml"];
                    //}
                    //if (item["tgl_pelaksanaan"].ToString().Equals(x_axis[i]) && (int)item["urutan"] == 2)
                    //{
                    //    y_kirim_usulan[i] = (int)item["jml"];
                    //}
                    //if (item["tgl_pelaksanaan"].ToString().Equals(x_axis[i]) && (int)item["urutan"] == 3)
                    //{
                    //    y_persetujuan_ka_puslit[i] = (int)item["jml"];
                    //}
                }
            }
            Chart1.Series.Add(new Series("Persetujuan Ka. Puslit"));
            Chart1.Series["Persetujuan Ka. Puslit"].Points.DataBindXY(x_axis.ToArray(), y_persetujuan_ka_puslit);
            Chart1.Series["Persetujuan Ka. Puslit"].ChartType = SeriesChartType.Line;
            Chart1.Series["Persetujuan Ka. Puslit"].BorderWidth = 2;
            Chart1.Series["Persetujuan Ka. Puslit"].MarkerSize = 5;
            Chart1.Series["Persetujuan Ka. Puslit"].MarkerStyle = MarkerStyle.Square;
            Chart1.Series["Persetujuan Ka. Puslit"].ToolTip = "Persetujuan Ka. Puslit\nTanggal : #VALX \nJumlah : #VALY";
            Chart1.Series.Add(new Series("Kirim Usulan"));
            Chart1.Series["Kirim Usulan"].Points.DataBindXY(x_axis.ToArray(), y_kirim_usulan);
            Chart1.Series["Kirim Usulan"].ChartType = SeriesChartType.Line;
            Chart1.Series["Kirim Usulan"].BorderWidth = 2;
            Chart1.Series["Kirim Usulan"].MarkerSize = 5;
            Chart1.Series["Kirim Usulan"].MarkerStyle = MarkerStyle.Square;
            Chart1.Series["Kirim Usulan"].ToolTip = "Kirim Usulan\nTanggal : #VALX \nJumlah : #VALY";
            Chart1.Series.Add(new Series("Pembuatan Usulan"));
            Chart1.Series["Pembuatan Usulan"].Points.DataBindXY(x_axis.ToArray(), y_pembuatan_usulan);
            Chart1.Series["Pembuatan Usulan"].ChartType = SeriesChartType.Line;
            Chart1.Series["Pembuatan Usulan"].BorderWidth = 2;
            Chart1.Series["Pembuatan Usulan"].MarkerSize = 5;
            Chart1.Series["Pembuatan Usulan"].MarkerStyle = MarkerStyle.Square;
            Chart1.Series["Pembuatan Usulan"].ToolTip = "Pembuatan Usulan\nTanggal : #VALX \nJumlah : #VALY";
            
            Chart1.Legends[0].Enabled = true;
            Chart1.ChartAreas[0].AxisX.Title = "Tanggal Pelaksanaan";
            Chart1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Center;
            Chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            Chart1.ChartAreas[0].AxisY.Title = "Jumlah Usulan";
            Chart1.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Center;
            Chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
        }
    }
}