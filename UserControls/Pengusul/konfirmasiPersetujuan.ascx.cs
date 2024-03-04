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

namespace simlitekkes.UserControls.Pengusul
{
    public partial class konfirmasiPersetujuan : System.Web.UI.UserControl
    {
        Models.Pengusul.konfirmasiPersetujuan objKonfirmasi = new Models.Pengusul.konfirmasiPersetujuan();

        login objLogin;
        uiNotify noty = new uiNotify();
        uiModal objModal = new uiModal();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isiDataUsulanKonfirmasi();
            }
        }

        public void isiDataUsulanKonfirmasi()
        {
            objLogin = (login)Session["objLogin"];

            // isi daftar usulan Konfirmasi
            var dtDaftarUsulanKonfirmasi = new DataTable();
            if (objKonfirmasi.getDaftarUsulanKonfirmasi(ref dtDaftarUsulanKonfirmasi, objLogin.idPersonal.ToString()))
            {
                lvDaftarUsulanKonfirmasi.DataSource = dtDaftarUsulanKonfirmasi;
                lvDaftarUsulanKonfirmasi.DataBind();
            }
        }

        protected void lvDaftarUsulanKonfirmasi_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int itemIndex = int.Parse(e.CommandArgument.ToString());

            string id_personil = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["id_personil"].ToString();
            ViewState["id_personil"] = id_personil;

            //Untuk isian modal konfirmasi persetujuan
            string judul = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["judul"].ToString();
            ViewState["judul"] = judul;
            string nama_skema = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["nama_skema"].ToString();
            ViewState["nama_skema"] = nama_skema;
            string thn_usulan_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["thn_usulan_kegiatan"].ToString();
            ViewState["thn_usulan_kegiatan"] = thn_usulan_kegiatan;
            string thn_pelaksanaan_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["thn_pelaksanaan_kegiatan"].ToString();
            ViewState["thn_pelaksanaan_kegiatan"] = thn_pelaksanaan_kegiatan;
            string nama_ketua = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["nama_ketua"].ToString();
            ViewState["nama_ketua"] = nama_ketua;
            string nama_institusi_ketua = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["nama_institusi_ketua"].ToString();
            ViewState["nama_institusi_ketua"] = nama_institusi_ketua;

            string id_usulan_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["id_usulan_kegiatan"].ToString();
            string kd_jenis_kegiatan = lvDaftarUsulanKonfirmasi.DataKeys[itemIndex]["kd_jenis_kegiatan"].ToString();

            if (e.CommandName == "UnduhPdf")
            {
                if(kd_jenis_kegiatan == "1")
                {
                    pdfUsulanLengkap.UnduhProposalLengkap(id_usulan_kegiatan);
                }
                else
                {
                    pdfUsulanLengkapabdimas.UnduhProposalLengkap(id_usulan_kegiatan);
                }
            }
            else if (e.CommandName == "Setuju")
            {
                ViewState["disetujui"] = true;

                lblModalInfoStsPersetujuan.Text = "menyetujui";
                lbModalStsKonfirmasi.Text = "Ya, setuju";

                lblModalJudul.Text = judul;
                lblModalSkema.Text = nama_skema;
                lblModalThnUsulan.Text = thn_usulan_kegiatan;
                lblModalThnPelaksanaan.Text = thn_pelaksanaan_kegiatan;
                lblModalKetua.Text = nama_ketua;
                lblModalInstitusiKetua.Text = nama_institusi_ketua;

                objModal.ShowModal(this.Page, "modalKonfirmasi");

            }
            else if (e.CommandName == "Tolak")
            {
                ViewState["disetujui"] = false;

                lblModalInfoStsPersetujuan.Text = "menolak";
                lbModalStsKonfirmasi.Text = "Ya, ditolak";

                lblModalJudul.Text = judul;
                lblModalSkema.Text = nama_skema;
                lblModalThnUsulan.Text = thn_usulan_kegiatan;
                lblModalThnPelaksanaan.Text = thn_pelaksanaan_kegiatan;
                lblModalKetua.Text = nama_ketua;
                lblModalInstitusiKetua.Text = nama_institusi_ketua;

                objModal.ShowModal(this.Page, "modalKonfirmasi");
            }
        }

        protected void lbModalStsKonfirmasi_Click(object sender, EventArgs e)
        {
            Guid id_personil = Guid.Parse(ViewState["id_personil"].ToString());

            if (bool.Parse(ViewState["disetujui"].ToString())) //anggota menyetujui
            {
                if (!objKonfirmasi.updateStsKonfirmasi("1", id_personil))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Konfirmasi anggota gagal" + objKonfirmasi.errorMessage);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Konfirmasi anggota berhasil");
                    isiDataUsulanKonfirmasi();

                    Session["page"] = 22;
                    Response.Redirect("Main.aspx");
                }
            }
            else // anggota tidak menyetujui
            {
                if (!objKonfirmasi.deleteDataPersonil(id_personil))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Konfirmasi anggota gagal" + objKonfirmasi.errorMessage);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Konfirmasi anggota berhasil");
                    isiDataUsulanKonfirmasi();

                    Session["page"] = 22;
                    Response.Redirect("Main.aspx");
                }
            }
        }
    }
}