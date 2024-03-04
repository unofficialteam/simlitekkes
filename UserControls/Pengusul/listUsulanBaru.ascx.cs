using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.Models;
using simlitekkes.Models.Pengusul;
using simlitekkes.UIControllers;
using System.IO;
using simlitekkes.Models.Sistem;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class listUsulanBaru : System.Web.UI.UserControl
    {
        Models.Pengusul.persyaratanUmum objPersyaratan = new Models.Pengusul.persyaratanUmum();
        Models.Pengusul.berandaPengusul objBerandaPengusul = new Models.Pengusul.berandaPengusul();
        Models.Pengusul.identitasUsulan objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        login objLogin;
        uiNotify noty = new uiNotify();
        uiDropdownList objDdl = new uiDropdownList();
        uiModal objModal = new uiModal();
        Core.kontrolUnggah ktUnggah = new Core.kontrolUnggah();
        Core.manipulasiData objManipData = new Core.manipulasiData();

        public event EventHandler OnChildEventOccurs;
        public event EventHandler OnChildEventUnduhProposalLengkap;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void isiListUsulanBaru(string thn_usulan, string thn_pelaksanaan)
        {
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            //string thn_kegiatan = "2018"; //DateTime.Now.Year.ToString();

            ViewState["thn_kegiatan"] = thn_usulan;
            ViewState["thn_pelaksanaan"] = thn_pelaksanaan;

            string kd_jenis_kegiatan = "1";
            
            // isi jml usulan baru
            var dtJmlUsulanBaru = new DataTable();

            // isi daftar usulan baru
            var dtDaftarUsulanBaru = new DataTable();
            if (objPersyaratan.getDaftarUsulanBaru(ref dtDaftarUsulanBaru, id_personal, thn_usulan, thn_pelaksanaan, kd_jenis_kegiatan))
            {
                lvDaftarUsulanBaru.DataSource = dtDaftarUsulanBaru;
                lvDaftarUsulanBaru.DataBind();
            }
        }

        protected void lvDaftarUsulanBaru_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;

            LinkButton lbEdit = new LinkButton();
            Label lblStsKonfirmasi = new Label();
            lblStsKonfirmasi = (Label)e.Item.FindControl("lblStsKonfirmasi");
            string is_sts_ketua = drv["is_sts_ketua"].ToString();

            lbHapus = (LinkButton)e.Item.FindControl("lbHapus");
            lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
            LinkButton lbBatalkanusulan = (LinkButton)e.Item.FindControl("lbBatalkanusulan");
            LinkButton lbUnduhProposalLengkap = (LinkButton)e.Item.FindControl("lbUnduhProposalLengkap");
            Panel panelUnduhPdf = (Panel)e.Item.FindControl("panelUnduhPdf");
            lbBatalkanusulan.Visible = false;
            lbUnduhProposalLengkap.Visible = false;
            panelUnduhPdf.Visible = false;
            string is_dibuka = drv["is_dibuka"].ToString();

            if (is_sts_ketua == "1" && is_dibuka == "1")
            {
                lbEdit.Visible = true;
                lbHapus.Visible = true;
                lblStsKonfirmasi.Visible = false;
            }
            else
            {
                lbHapus.Visible = false;
                lbEdit.Visible = false;
                lblStsKonfirmasi.Visible = true;
            }

            Label lblStsApproval = new Label();
            lblStsApproval = (Label)e.Item.FindControl("lblStsApproval");
            string kd_sts_approval = drv["kd_sts_approval"].ToString();

            //if (kd_sts_approval == "1")
            //{
            //    lblStsApproval.CssClass = "label bg-success";
            //}
            //else
            //{
            //    lblStsApproval.CssClass = "label bg-warning";
            //}

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem    item = (ListViewDataItem)e.Item;
                String id_transaksi_kegiatan = lvDaftarUsulanBaru.DataKeys[item.DataItemIndex]["id_transaksi_kegiatan"].ToString();
                if (id_transaksi_kegiatan.Trim() != "")
                {
                    if (is_sts_ketua == "1" && is_dibuka == "1")
                        lbBatalkanusulan.Visible = true;
                    lbEdit.Visible = false;
                    lbHapus.Visible = false;
                    panelUnduhPdf.Visible = true;
                    lbUnduhProposalLengkap.Visible = true;

                    if (kd_sts_approval != "")
                    {
                        lbBatalkanusulan.Visible = false;
                        if (kd_sts_approval == "1")
                        {
                            lblStsApproval.Text = "Proposal disetujui untuk diusulkan";
                            lblStsApproval.BackColor = System.Drawing.Color.LightGreen;
                        }
                        else
                        {
                            lblStsApproval.Text = "Proposal ditolak: " + lvDaftarUsulanBaru.DataKeys[item.DataItemIndex]["alasan_penolakan"].ToString();
                            lblStsApproval.BackColor = System.Drawing.Color.Red;
                            lblStsApproval.ForeColor = System.Drawing.Color.White;
                        }
                    }
                    else
                    {
                        lblStsApproval.Text = "Proposal belum ditinjau";
                        lblStsApproval.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                else
                {
                    lblStsApproval.Text = "Proposal belum dikirim";
                    lblStsApproval.BackColor = System.Drawing.Color.FromArgb(1,255,200,200);
                }
            }
        }

        protected void lvDaftarUsulanBaru_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            string id_usulan_kegiatan = lvDaftarUsulanBaru.DataKeys[e.NewEditIndex]["id_usulan_kegiatan"].ToString();
            ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
            Session["isEdit"] = true; 
            if (OnChildEventOccurs != null)
                OnChildEventOccurs(sender, null);
        }

        public string getIdUsulanKegiatan()
        {
            return ViewState["id_usulan_kegiatan"].ToString();
        }

        protected void lvDaftarUsulanBaru_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            string id_usulan_kegiatan = lvDaftarUsulanBaru.DataKeys[e.ItemIndex]["id_usulan_kegiatan"].ToString();
            ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
            lblModalJudul.Text = lvDaftarUsulanBaru.DataKeys[e.ItemIndex]["judul"].ToString();
            lblModalSkema.Text = lvDaftarUsulanBaru.DataKeys[e.ItemIndex]["nama_skema"].ToString();
            objModal.ShowModal(this.Page, "modalHapus");
        }

        protected void lbHapus_Click(object sender, EventArgs e)
        {
            if (objPersyaratan.deleteDataUsulanBaru(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data usulan berhasil");
                isiListUsulanBaru(ViewState["thn_kegiatan"].ToString(), ViewState["thn_pelaksanaan"].ToString());
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objPersyaratan.errorMessage);
                isiListUsulanBaru(ViewState["thn_kegiatan"].ToString(), ViewState["thn_pelaksanaan"].ToString());
            }
        }

        protected void lvDaftarUsulanBaru_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if(e.CommandName=="batalkan")
            {
                string id_usulan_kegiatan = e.CommandArgument.ToString();
                ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
                lblJudulDibatalkan.Text = lvDaftarUsulanBaru.DataKeys[e.Item.DataItemIndex]["judul"].ToString();
                new uiModal().ShowModal(this.Page, "modalBatalkanUsulan");
            }
            else if(e.CommandName == "unduhProposalLengkap")
            {
                string id_usulan_kegiatan = e.CommandArgument.ToString();
                //Session["id_usulan_kegiatan"] = id_usulan_kegiatan;
                Session["id_usulan_kegiatan_unduh"] = id_usulan_kegiatan;
                if (OnChildEventUnduhProposalLengkap != null)
                    OnChildEventUnduhProposalLengkap(sender, null);
            }
        }

        protected void btnBatalkanUsulan_Click(object sender, EventArgs e)
        {
            Guid id_usulan_kegiatan = Guid.Parse( ViewState["id_usulan_kegiatan"].ToString());
            if(objIdentitasUsulan.hapusTransaksiKirimUsulan(id_usulan_kegiatan))
            {
                isiListUsulanBaru(ViewState["thn_kegiatan"].ToString(), ViewState["thn_pelaksanaan"].ToString());
                new uiModal().HideModal(this.Page, "modalBatalkanUsulan");
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Pembatalan pengiriman usulan berhasil");
            }
        }

    }
}