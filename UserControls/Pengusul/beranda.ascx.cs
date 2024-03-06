using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class beranda : System.Web.UI.UserControl
    {
        Models.login objLogin;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null)
                Response.Redirect("Login.aspx");
            else
                objLogin = (Models.login)Session["objLogin"];

            if (!IsPostBack)
            {
                mv1.SetActiveView(view0);

                // test

                // Informasi Pemenang pendaftaran Reviewer
                DataTable dt = new DataTable();
                //objPengusul.getInfoPemenangPendaftaranReviewer(ref dt, Guid.Parse(objLogin.idPersonal));
                //if (dt.Rows.Count > 0)
                //{
                //    lblInfoPenetapanReviewerNasional.Text = dt.Rows[0]["pesan_pendaftaran_reviewer"].ToString();
                //}
            }

        }

        protected void menu_identitas_personal_Click(object sender, EventArgs e)
        {
            identitas.goHome();
            mv1.SetActiveView(view0);
            changeMenu(0);
        }

        protected void menu_sinta_Click(object sender, EventArgs e)
        {
            sinta.isiSintaPengusul();
            mv1.SetActiveView(view1);
            changeMenu(1);
        }

        protected void menu_penelitian_Click(object sender, EventArgs e)
        {
            riwayatPenelitian.isiRiwayatPenelitian();
            mv1.SetActiveView(view2);
            changeMenu(2);
        }

        protected void menu_pengabdian_Click(object sender, EventArgs e)
        {
            riwayatPengabdian.isiRiwayatPengabdian();
            mv1.SetActiveView(view3);
            changeMenu(3);
        }

        protected void menu_artikel_jurnal_Click(object sender, EventArgs e)
        {
            artikelJurnal.isiArtikelJurnal();
            mv1.SetActiveView(view4);
            changeMenu(4);
        }

        protected void menu_artikel_prosiding_Click(object sender, EventArgs e)
        {
            prosiding.isiDataProsiding();
            mv1.SetActiveView(view6);
            changeMenu(6);
        }

        protected void menu_karya_intelektual_Click(object sender, EventArgs e)
        {
            hki.isiHKI();
            mv1.SetActiveView(view5);
            changeMenu(5);
        }

        protected void menu_buku_Click(object sender, EventArgs e)
        {
            buku.isiBuku();
            mv1.SetActiveView(view7);
            changeMenu(7);
        }

        private void changeMenu(int menu)
        {
            switch (menu)
            {
                case 0:
                    this.menu_identitas_personal.CssClass = "dd-handle bg-info text-white";
                    this.menu_sinta.CssClass = "dd-handle";
                    this.menu_penelitian.CssClass = "dd-handle";
                    this.menu_pengabdian.CssClass = "dd-handle";
                    this.menu_artikel_jurnal.CssClass = "dd-handle";
                    this.menu_artikel_prosiding.CssClass = "dd-handle";
                    this.menu_karya_intelektual.CssClass = "dd-handle";
                    this.menu_buku.CssClass = "dd-handle";
                    break;
                case 1:
                    this.menu_identitas_personal.CssClass = "dd-handle";
                    this.menu_sinta.CssClass = "dd-handle bg-info text-white";
                    this.menu_penelitian.CssClass = "dd-handle";
                    this.menu_pengabdian.CssClass = "dd-handle";
                    this.menu_artikel_jurnal.CssClass = "dd-handle";
                    this.menu_artikel_prosiding.CssClass = "dd-handle";
                    this.menu_karya_intelektual.CssClass = "dd-handle";
                    this.menu_buku.CssClass = "dd-handle";
                    break;
                case 2:
                    this.menu_identitas_personal.CssClass = "dd-handle";
                    this.menu_sinta.CssClass = "dd-handle";
                    this.menu_penelitian.CssClass = "dd-handle bg-info text-white";
                    this.menu_pengabdian.CssClass = "dd-handle";
                    this.menu_artikel_jurnal.CssClass = "dd-handle";
                    this.menu_artikel_prosiding.CssClass = "dd-handle";
                    this.menu_karya_intelektual.CssClass = "dd-handle";
                    this.menu_buku.CssClass = "dd-handle";
                    break;
                case 3:
                    this.menu_identitas_personal.CssClass = "dd-handle";
                    this.menu_sinta.CssClass = "dd-handle";
                    this.menu_penelitian.CssClass = "dd-handle";
                    this.menu_pengabdian.CssClass = "dd-handle bg-info text-white";
                    this.menu_artikel_jurnal.CssClass = "dd-handle";
                    this.menu_artikel_prosiding.CssClass = "dd-handle";
                    this.menu_karya_intelektual.CssClass = "dd-handle";
                    this.menu_buku.CssClass = "dd-handle";
                    break;
                case 4:
                    this.menu_identitas_personal.CssClass = "dd-handle";
                    this.menu_sinta.CssClass = "dd-handle";
                    this.menu_penelitian.CssClass = "dd-handle";
                    this.menu_pengabdian.CssClass = "dd-handle";
                    this.menu_artikel_jurnal.CssClass = "dd-handle bg-info text-white";
                    this.menu_artikel_prosiding.CssClass = "dd-handle";
                    this.menu_karya_intelektual.CssClass = "dd-handle";
                    this.menu_buku.CssClass = "dd-handle";
                    break;
                case 5:
                    this.menu_identitas_personal.CssClass = "dd-handle";
                    this.menu_sinta.CssClass = "dd-handle";
                    this.menu_penelitian.CssClass = "dd-handle";
                    this.menu_pengabdian.CssClass = "dd-handle";
                    this.menu_artikel_jurnal.CssClass = "dd-handle";
                    this.menu_artikel_prosiding.CssClass = "dd-handle";
                    this.menu_karya_intelektual.CssClass = "dd-handle bg-info text-white";
                    this.menu_buku.CssClass = "dd-handle";
                    break;
                case 6:
                    this.menu_identitas_personal.CssClass = "dd-handle";
                    this.menu_sinta.CssClass = "dd-handle";
                    this.menu_penelitian.CssClass = "dd-handle";
                    this.menu_pengabdian.CssClass = "dd-handle";
                    this.menu_artikel_jurnal.CssClass = "dd-handle";
                    this.menu_artikel_prosiding.CssClass = "dd-handle bg-info text-white";
                    this.menu_karya_intelektual.CssClass = "dd-handle";
                    this.menu_buku.CssClass = "dd-handle";
                    break;
                case 7:
                    this.menu_identitas_personal.CssClass = "dd-handle";
                    this.menu_sinta.CssClass = "dd-handle";
                    this.menu_penelitian.CssClass = "dd-handle";
                    this.menu_pengabdian.CssClass = "dd-handle";
                    this.menu_artikel_jurnal.CssClass = "dd-handle";
                    this.menu_artikel_prosiding.CssClass = "dd-handle";
                    this.menu_karya_intelektual.CssClass = "dd-handle";
                    this.menu_buku.CssClass = "dd-handle bg-info text-white";
                    break;
            }
        }
    }
}