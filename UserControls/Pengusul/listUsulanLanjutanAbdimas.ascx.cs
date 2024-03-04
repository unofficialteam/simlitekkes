using simlitekkes.Models;
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
    public partial class listUsulanLanjutanAbdimas : System.Web.UI.UserControl
    {

        Models.Pengusul.persyaratanUmum objPersyaratan = new Models.Pengusul.persyaratanUmum();
        Models.Pengusul.persyaratanUmumAbdimas objPersyaratanAbdimas = new Models.Pengusul.persyaratanUmumAbdimas();
        Models.Pengusul.berandaPengusul objBerandaPengusul = new Models.Pengusul.berandaPengusul();
        Models.Pengusul.identitasUsulan objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
        login objLogin;
        uiNotify noty = new uiNotify();
        uiModal objModal = new uiModal();
        Core.kontrolUnggah ktUnggah = new Core.kontrolUnggah();
        Core.manipulasiData objManipData = new Core.manipulasiData();

        public event EventHandler OnChildEventDelete;
        public event EventHandler OnChildEventOccurs;
        public event EventHandler OnChildEventUnduhProposalLengkap;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void isiListUsulanLanjutan(bool isEligible=true)
        {
            objLogin = (login)Session["objLogin"];
            string id_personal = objLogin.idPersonal;
            string thn_kegiatan = DateTime.Now.Year.ToString();
            string kd_jenis_kegiatan = "2";
            ViewState["isEligiblle"] = isEligible;

            // usulan lanjutan blm diajukan
            var dtDaftarUsulanLanjutan = new DataTable();
            if (objPersyaratan.listUsulanAbdimasLanjutanBlmDiajukan(ref dtDaftarUsulanLanjutan, id_personal, thn_kegiatan))
            {
                lvDaftarUsulanLanjutan.DataSource = dtDaftarUsulanLanjutan;
                lvDaftarUsulanLanjutan.DataBind();
            }
            

            // isi daftar usulan lanjutan
            var dtDaftarUsulanBaru = new DataTable();
            if (objPersyaratan.getDaftarUsulanLanjutan(ref dtDaftarUsulanBaru, id_personal, thn_kegiatan, kd_jenis_kegiatan))
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

            //lbHapus = (LinkButton)e.Item.FindControl("lbHapus");
            lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
            LinkButton lbBatalkanusulan = (LinkButton)e.Item.FindControl("lbBatalkanusulan");
            LinkButton lbUnduhProposalLengkap = (LinkButton)e.Item.FindControl("lbUnduhProposalLengkap");
            lbBatalkanusulan.Visible = false;
            lbUnduhProposalLengkap.Visible = false;
            string is_dibuka = drv["is_dibuka"].ToString();

            if (is_sts_ketua == "1" && is_dibuka == "1") //&& is_dibuka == "1") //semengtara jadwal dicoment utk testing
            {
                lbEdit.Visible = true;
                //lbHapus.Visible = true;
                lblStsKonfirmasi.Visible = false;
            }
            else
            {
                //lbHapus.Visible = false;
                lbEdit.Visible = false;
                lblStsKonfirmasi.Visible = true;
            }

            Label lblStsApproval = new Label();
            lblStsApproval = (Label)e.Item.FindControl("lblStsApproval");
            string kd_sts_approval = drv["kd_sts_approval"].ToString();

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem item = (ListViewDataItem)e.Item;
                String id_transaksi_kegiatan = lvDaftarUsulanBaru.DataKeys[item.DataItemIndex]["id_transaksi_kegiatan"].ToString();
                if (id_transaksi_kegiatan.Trim() != "")
                {
                    if (is_sts_ketua == "1" && is_dibuka == "1")
                        lbBatalkanusulan.Visible = true;
                    lbEdit.Visible = false;
                    //lbHapus.Visible = false;
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
                            lblStsApproval.Text = "Proposal ditolak";
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
                    lblStsApproval.BackColor = System.Drawing.Color.FromArgb(1, 255, 200, 200);
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
            if (objPersyaratanAbdimas.deleteDataUsulanBaru(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString())))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data usulan berhasil");
                isiListUsulanLanjutan();
                if (OnChildEventDelete != null)
                    OnChildEventDelete(sender, null);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal. error = " + objPersyaratan.errorMessage);
                isiListUsulanLanjutan();
            }
        }

        protected void lvDaftarUsulanBaru_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "batalkan")
            {
                string id_usulan_kegiatan = e.CommandArgument.ToString();
                ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
                lblJudulDibatalkan.Text = lvDaftarUsulanBaru.DataKeys[e.Item.DataItemIndex]["judul"].ToString();
                new uiModal().ShowModal(this.Page, "modalBatalkanUsulan");
            }
            else if (e.CommandName == "unduhProposalLengkap")
            {
                string id_usulan_kegiatan = e.CommandArgument.ToString();
                ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
                if (OnChildEventUnduhProposalLengkap != null)
                    OnChildEventUnduhProposalLengkap(sender, null);
            }
        }

        protected void btnBatalkanUsulan_Click(object sender, EventArgs e)
        {
            Guid id_usulan_kegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            if (objIdentitasUsulan.hapusTransaksiKirimUsulan(id_usulan_kegiatan))
            {
                isiListUsulanLanjutan();
                new uiModal().HideModal(this.Page, "modalBatalkanUsulan");
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Pembatalan pengiriman usulan berhasil");
            }
        }

        protected void lvDaftarUsulanLanjutan_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lbAjukan = new LinkButton();
            lbAjukan = (LinkButton)e.Item.FindControl("lbAjukan");
            Label lblInfoBatasUsulan = new Label();
            lblInfoBatasUsulan = (Label)e.Item.FindControl("lblInfoBatasUsulan");

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem item = (ListViewDataItem)e.Item;
                String urutanThnUsulan = lvDaftarUsulanLanjutan.DataKeys[item.DataItemIndex]["urutan_thn_usulan_kegiatan"].ToString();
                string idSkema = lvDaftarUsulanLanjutan.DataKeys[item.DataItemIndex]["id_skema"].ToString();
                if(bool.Parse(ViewState["isEligiblle"].ToString()))
                {
                    lbAjukan.Enabled = true;
                    lblInfoBatasUsulan.Visible = false;
                    lbAjukan.CssClass = "btn btn-success";
                }
                else
                {
                    lbAjukan.Enabled = false;
                    lblInfoBatasUsulan.Visible = true;
                    lbAjukan.CssClass = "btn btn-default";
                }
            }
        }

        protected void lvDaftarUsulanLanjutan_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lblInfoUsulanLanjutan.Text = lvDaftarUsulanLanjutan.DataKeys[e.NewEditIndex]["judul"].ToString();
            string id_usulan = lvDaftarUsulanLanjutan.DataKeys[e.NewEditIndex]["id_usulan"].ToString();
            string urutan_thn_usulan_kegiatan = lvDaftarUsulanLanjutan.DataKeys[e.NewEditIndex]["urutan_thn_usulan_kegiatan"].ToString();
            string id_skema = lvDaftarUsulanLanjutan.DataKeys[e.NewEditIndex]["id_skema"].ToString();
            string thn_usulan_kegiatan = lvDaftarUsulanLanjutan.DataKeys[e.NewEditIndex]["thn_usulan_kegiatan"].ToString();
            string thn_pelaksanaan_kegiatan = lvDaftarUsulanLanjutan.DataKeys[e.NewEditIndex]["thn_pelaksanaan_kegiatan"].ToString();
            List<string> lstAttrUsulanLanjutan = new List<string>();
            lstAttrUsulanLanjutan.Add(id_usulan);
            lstAttrUsulanLanjutan.Add(urutan_thn_usulan_kegiatan);
            lstAttrUsulanLanjutan.Add(id_skema);
            lstAttrUsulanLanjutan.Add(thn_usulan_kegiatan);
            lstAttrUsulanLanjutan.Add(thn_pelaksanaan_kegiatan);

            ViewState["attr_usulan_lanjutan"] = lstAttrUsulanLanjutan;
            objModal.ShowModal(this.Page, "modalKonfirmasiUsulanLanjutan");

            //if (id_skema == "2")
            //{
            //    rblSkemaPasca.Visible = true;
            //    lblInfoSkema.Visible = true;
            //    ViewState["attr_usulan_lanjutan"] = lstAttrUsulanLanjutan;
            //    objModal.ShowModal(this.Page, "modalKonfirmasiUsulanLanjutan");
            //}
            //else
            //{
            //    rblSkemaPasca.Visible = false;
            //    lblInfoSkema.Visible = false;
            //    ViewState["attr_usulan_lanjutan"] = lstAttrUsulanLanjutan;
            //    objModal.ShowModal(this.Page, "modalKonfirmasiUsulanLanjutan");
            //}
        }

        protected void btAjukan_Click(object sender, EventArgs e)
        {
            List<string> lstAttrUsulanLanjutan = (List<string>)ViewState["attr_usulan_lanjutan"];
            string id_usulan = lstAttrUsulanLanjutan.ElementAt(0);
            string urutan_thn_usulan_kegiatan = lstAttrUsulanLanjutan.ElementAt(1);
            string id_skema = lstAttrUsulanLanjutan.ElementAt(2);
            string thn_usulan_kegiatan = lstAttrUsulanLanjutan.ElementAt(3);
            string thn_pelaksanaan_kegiatan = lstAttrUsulanLanjutan.ElementAt(4);
            objLogin = (login)Session["objLogin"];
            string id_usulan_kegiatan = string.Empty;
            
            
                if (objIdentitasUsulan.insertDataLanjutanPengabdianRb(ref id_usulan_kegiatan,
                                Guid.Parse(objLogin.idPersonal), Guid.Parse(id_usulan), int.Parse(urutan_thn_usulan_kegiatan),
                                thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, int.Parse(id_skema)))
                {
                    ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
                    Session["isEdit"] = true;
                    if (OnChildEventOccurs != null)
                        OnChildEventOccurs(sender, null);
                }
                else
                {

                }
        }
    }
}