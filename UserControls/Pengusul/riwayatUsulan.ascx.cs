using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.Core;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class riwayatUsulan : System.Web.UI.UserControl
    {

        simlitekkes.Models.Pengusul.identitasUsulan objModelIdentitasUsulan =
            new simlitekkes.Models.Pengusul.identitasUsulan();

        Models.login objLogin;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objLogin"] == null) Response.Redirect("~/login.aspx");
            objLogin = (Models.login)Session["objLogin"];
            if (!IsPostBack)
            {
                setUsulan(0);
                ktPagging.setPaging(4, Int32.Parse(lblJmlRecords.Text));
            }
        }

        private void setUsulan(int offset)
        {
            DataTable dtListUsulan = new DataTable();
            objModelIdentitasUsulan.listRiwayatUsulan(ref dtListUsulan, objLogin.idPersonal, offset);
            gvUsulan.DataSource = dtListUsulan;
            gvUsulan.DataBind();
            if (dtListUsulan.Rows.Count > 0)
                lblJmlRecords.Text = dtListUsulan.Rows[0]["total_usulan"].ToString();
        }

        protected void Paging_PageChanging(object sender, EventArgs e)
        {
            setUsulan(ktPagging.currentPage * 4);
        }

        protected void gvUsulan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string kd_sts_unggah_proposal = gvUsulan.DataKeys[e.Row.RowIndex]["kd_sts_unggah_proposal"].ToString();
                string id_transaksi_unggah_proposal = gvUsulan.DataKeys[e.Row.RowIndex]["id_transaksi_unggah_proposal"].ToString();
                string kd_peran_personil = gvUsulan.DataKeys[e.Row.RowIndex]["kd_peran_personil"].ToString();

                DataRowView drv = (DataRowView)e.Row.DataItem;
                LinkButton lbUnduhProposal = (LinkButton)e.Row.FindControl("lbUnduhProposal");
                if (kd_sts_unggah_proposal == "1")
                {
                    lbUnduhProposal.Enabled = true;
                    lbUnduhProposal.CssClass = "btn btn-success float-right text-white";
                }
                else
                {
                    lbUnduhProposal.Enabled = false;
                    lbUnduhProposal.CssClass = "btn btn-danger float-right text-white disabled";
                }
                if (kd_peran_personil != "A")
                {
                    lbUnduhProposal.Visible = false;
                }

                Label lblNoBaris = (Label)e.Row.FindControl("lblNoBaris");
                lblNoBaris.Text = ((e.Row.RowIndex + 1) + (4 * (ktPagging.currentPage))).ToString();
            }
        }

        protected void gvUsulan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "unduhProposal")
            {
                string id_usulan_kegiatan = e.CommandArgument.ToString();
                pdfUsulanLengkap.UnduhProposalLengkap(id_usulan_kegiatan);  
            }
        }
    }
}