using simlitekkes.UIControllers;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class tkt : System.Web.UI.UserControl
    {
        Models.Pengusul.tkt modelTKT = new Models.Pengusul.tkt();
        public event EventHandler OnChildEventOccurs;
        public event EventHandler OnSimpanClick;

        public int LevelTKT
        {
            get
            {
                if (ViewState["LevelTKT"] == null) ViewState["LevelTKT"] = 0;
                return int.Parse(ViewState["LevelTKT"].ToString());
            }
            set { ViewState["LevelTKT"] = value; }
        }

        private int LevelTKTBerjalan
        {
            get
            {
                if (ViewState["LevelTKTBerjalan"] == null) ViewState["LevelTKTBerjalan"] = 1;
                return int.Parse(ViewState["LevelTKTBerjalan"].ToString());
            }
            set { ViewState["LevelTKTBerjalan"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitTKT()
        {
            mvTKT.SetActiveView(vKategori);
            isirblKategori();
            LevelTKTBerjalan = 1;
            LevelTKT = 0;
        }

        private void isirblKategori()
        {
            var dt = new DataTable();
            if (modelTKT.GetListKategoriTKT(ref dt))
            {
                rblKategori.Items.Clear();
                rblKategori.DataValueField = "id_kategori";
                rblKategori.DataTextField = "keterangan";
                rblKategori.DataSource = dt;
                rblKategori.DataBind();
            }
        }

        private void setIndikatorTKT(int level)
        {
            if (rblKategori.SelectedValue != null && tbTeknologi.Text != null)
            {
                var dt = new DataTable();
                if (modelTKT.GetListIndikatorTKT(ref dt, rblKategori.SelectedValue, level.ToString()))
                {
                    gvIndikator.DataSource = dt;
                    gvIndikator.DataBind();

                    lblLevelTKT.Text = LevelTKTBerjalan.ToString();
                    panelInfoLanjutan.Visible = false;
                    panelInfoMaksimal.Visible = false;
                    lbLanjut.Visible = false;
                    lbSimpan.Visible = false;
                }
            }
        }

        private void hitungIndikator()
        {
            lbLanjut.Visible = false;
            lbSimpan.Visible = false;

            DropDownList ddlPersentase;
            int persentase = 0, totalPersentase = 0;

            var jumlahIndikator = gvIndikator.Rows.Count;

            for (int i = 0; i < jumlahIndikator; i++)
            {
                ddlPersentase = (DropDownList)gvIndikator.Rows[i].FindControl("ddlPersentase");
                if (int.TryParse(ddlPersentase.SelectedValue, out persentase))
                {
                    totalPersentase += persentase;
                }
            }

            decimal rerataPersentase = (totalPersentase / jumlahIndikator);
            if (rerataPersentase >= 80)
            {
                if ((LevelTKTBerjalan + 1) == 9)
                {
                    LevelTKT += 1;
                    lblNilaiTKTMaksimal.Text = rerataPersentase.ToString();
                    lblLevelTKTMaksimal.Text = LevelTKT.ToString();                    
                    panelInfoMaksimal.Visible = true;
                    lbHitungTKT.Visible = false;
                    lbSimpan.Visible = true;
                }
                else
                {
                    lblNilaiTKTLanjutan.Text = rerataPersentase.ToString();
                    lblInfoLevelTKT.Text = (LevelTKTBerjalan + 1).ToString();
                    panelInfoLanjutan.Visible = true;

                    lbLanjut.Visible = true;
                }
            }            
            else
            {
                lblNilaiTKTMaksimal.Text = rerataPersentase.ToString();
                lblLevelTKTMaksimal.Text = LevelTKT.ToString();
                panelInfoMaksimal.Visible = true;
                lbSimpan.Visible = true;           
            }
        }

        protected void lbGetKategori_Click(object sender, EventArgs e)
        {
            if (tbTeknologi.Text.Trim().Length == 0)
            {
                new uiNotify().Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "'Teknologi Yang Dikembangkan' harus diisi terlebih dahulu !");
                OnChildEventOccurs(sender, null);
                return;
            }

            if (rblKategori.SelectedItem == null)
            {
                new uiNotify().Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                    "'Kategori Indikator' harus diisi terlebih dahulu !");
                return;
            }
            lblKategori.Text = rblKategori.SelectedItem.Text;

            setIndikatorTKT(LevelTKTBerjalan);

            mvTKT.SetActiveView(vIndikator);

            if (OnChildEventOccurs != null)
                OnChildEventOccurs(sender, null);
        }

        protected void lbHitungTKT_Click(object sender, EventArgs e)
        {
            panelInfoLanjutan.Visible = false;
            panelInfoMaksimal.Visible = false;
            hitungIndikator();
            if (OnChildEventOccurs != null)
                OnChildEventOccurs(sender, null);
        }

        protected void lbLanjut_Click(object sender, EventArgs e)
        {
            LevelTKTBerjalan += 1;
            LevelTKT += 1;
            setIndikatorTKT(LevelTKTBerjalan);
            if (OnChildEventOccurs != null)
                OnChildEventOccurs(sender, null);
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            if (this.OnSimpanClick != null)
                this.OnSimpanClick(this, new EventArgs());
        }
    }
}