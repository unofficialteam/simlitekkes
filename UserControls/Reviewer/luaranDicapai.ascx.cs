using simlitekkes.Helper;
using simlitekkes.Models.pelaksanaan;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Reviewer
{
    public partial class luaranDicapai : System.Web.UI.UserControl
    {
        luaranLaporanKemajuan mdlLapKemajuan = new luaranLaporanKemajuan();
        uiNotify noty = new uiNotify();
        const string FOLDER_BERKAS_LAP_KEMAJUAN = "~/fileUpload20/laporan_kemajuan/BuktiLuaran/";
        const string FOLDER_BERKAS_LAP_AKHIR = "~/fileUpload20/laporan_akhir/BuktiLuaran/";
        const string KD_TAHAP_LAP_AKHIR = "34";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //setData(Guid.Parse("56d00fbd-a8a1-4f00-8f4e-eecddeeaec99"), "31", "2019");
            }
        }

        public void setData(Guid idLuaranDijanjikan, Guid idTransaksiKegiatan, string sts_lap_akhir, /*string kdTahapanKegiatan,*/ string thnPelaksanaan)
        {
            DataTable dtInfo = new DataTable();
            mdlLapKemajuan.getInfoCapaianLuaran(ref dtInfo, idLuaranDijanjikan.ToString(), idTransaksiKegiatan.ToString());
            lblInfoLuaran.Text = dtInfo.Rows[0]["informasi"].ToString();

            ViewState["kd_tahapan_kegiatan"] = "31";
            if (sts_lap_akhir == "1")
            {
                ViewState["kd_tahapan_kegiatan"] = "34";
            }

            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            DataTable dt = new DataTable();
            mdlLapKemajuan.listTargetCapaianNDokumen(ref dt, idLuaranDijanjikan, idTransaksiKegiatan);

            pnlDok1.Visible = false;
            pnlDok2.Visible = false;
            pnlDok3.Visible = false;
            pnlDok4.Visible = false;
            lbUnduhPdf1.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdf2.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdf3.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdf4.ForeColor = System.Drawing.Color.Gray;
            lbUnduhPdf1.Enabled = false;
            lbUnduhPdf2.Enabled = false;
            lbUnduhPdf3.Enabled = false;
            lbUnduhPdf4.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                lblJenisLuaran.Text = dt.Rows[0]["nama_jenis_luaran"].ToString();
                lblTarget.Text = dt.Rows[0]["nama_target_capaian_luaran"].ToString();
                lblCapaian.Text = dt.Rows[0]["nama_target_jenis_luaran"].ToString();
                lblCapaian.ForeColor = System.Drawing.Color.Green;
                if (lblCapaian.Text.Trim() == "")
                {
                    lblCapaian.Text = "(Belum diisi)";
                    lblCapaian.ForeColor = System.Drawing.Color.Red;
                }

                if (dt.Rows.Count > 0)
                {
                    pnlDok1.Visible = true;
                    lblJnsDokBuktiLuaran1.Text = dt.Rows[0]["jenis_dokumen_bukti_luaran"].ToString();
                    if (dt.Rows[0]["id_dokumen_bukti_luaran"].ToString().Trim() != "")
                    {
                        lbUnduhPdf1.ForeColor = System.Drawing.Color.Red;
                        lbUnduhPdf1.Enabled = true;
                        ViewState["id_dokumen_bukti_luaran1"] = dt.Rows[0]["id_dokumen_bukti_luaran"].ToString();
                    }
                }
                if (dt.Rows.Count > 1)
                {
                    pnlDok2.Visible = true;
                    lblJnsDokBuktiLuaran2.Text = dt.Rows[1]["jenis_dokumen_bukti_luaran"].ToString();
                    if (dt.Rows[1]["id_dokumen_bukti_luaran"].ToString().Trim() != "")
                    {
                        lbUnduhPdf2.ForeColor = System.Drawing.Color.Red;
                        lbUnduhPdf2.Enabled = true;
                        ViewState["id_dokumen_bukti_luaran2"] = dt.Rows[1]["id_dokumen_bukti_luaran"].ToString();
                    }
                }
                if (dt.Rows.Count > 2)
                {
                    pnlDok3.Visible = true;
                    lblJnsDokBuktiLuaran3.Text = dt.Rows[2]["jenis_dokumen_bukti_luaran"].ToString();
                    if (dt.Rows[2]["id_dokumen_bukti_luaran"].ToString().Trim() != "")
                    {
                        lbUnduhPdf3.ForeColor = System.Drawing.Color.Red;
                        lbUnduhPdf3.Enabled = true;
                        ViewState["id_dokumen_bukti_luaran3"] = dt.Rows[2]["id_dokumen_bukti_luaran"].ToString();
                    }
                }
                if (dt.Rows.Count > 3)
                {
                    pnlDok4.Visible = true;
                    lblJnsDokBuktiLuaran4.Text = dt.Rows[3]["jenis_dokumen_bukti_luaran"].ToString();
                    if (dt.Rows[3]["id_dokumen_bukti_luaran"].ToString().Trim() != "")
                    {
                        lbUnduhPdf4.ForeColor = System.Drawing.Color.Red;
                        lbUnduhPdf4.Enabled = true;
                        ViewState["id_dokumen_bukti_luaran4"] = dt.Rows[3]["id_dokumen_bukti_luaran"].ToString();
                    }
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Capaian luaran belum diisi.");
            }
        }

        public void setPanelDokVisible(bool isVisible)
        {
            pnlDok1.Visible = isVisible;
            pnlDok2.Visible = isVisible;
            pnlDok3.Visible = isVisible;
            pnlDok4.Visible = isVisible;
        }

        protected void lbUnduhPdf1_Click(object sender, EventArgs e)
        {
            string folderUnduh = "~/fileUpload20/laporan_kemajuan/BuktiLuaran/" + ViewState["thn_pelaksanaan"].ToString();
            if (ViewState["kd_tahapan_kegiatan"].ToString() == KD_TAHAP_LAP_AKHIR)
            {
                folderUnduh = FOLDER_BERKAS_LAP_AKHIR + ViewState["thn_pelaksanaan"].ToString();
            }
            string namaFileAsli = ViewState["id_dokumen_bukti_luaran1"].ToString();
            string namaFileDiunduh = lblJnsDokBuktiLuaran1.Text + ".pdf";

            if (File.Exists(Server.MapPath(folderUnduh + "/" + namaFileAsli + ".pdf")))
            {
                unduhPDF(folderUnduh, namaFileAsli, namaFileDiunduh);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        protected void lbUnduhPdf2_Click(object sender, EventArgs e)
        {
            string folderUnduh = "~/fileUpload20/laporan_kemajuan/BuktiLuaran/" + ViewState["thn_pelaksanaan"].ToString();
            if (ViewState["kd_tahapan_kegiatan"].ToString() == KD_TAHAP_LAP_AKHIR)
            {
                folderUnduh = FOLDER_BERKAS_LAP_AKHIR + ViewState["thn_pelaksanaan"].ToString();
            }
            string namaFileAsli = ViewState["id_dokumen_bukti_luaran2"].ToString();
            string namaFileDiunduh = lblJnsDokBuktiLuaran2.Text + ".pdf";

            if (File.Exists(Server.MapPath(folderUnduh + "/" + namaFileAsli + ".pdf")))
            {
                unduhPDF(folderUnduh, namaFileAsli, namaFileDiunduh);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        protected void lbUnduhPdf3_Click(object sender, EventArgs e)
        {
            string folderUnduh = "~/fileUpload20/laporan_kemajuan/BuktiLuaran/" + ViewState["thn_pelaksanaan"].ToString();
            if (ViewState["kd_tahapan_kegiatan"].ToString() == KD_TAHAP_LAP_AKHIR)
            {
                folderUnduh = FOLDER_BERKAS_LAP_AKHIR + ViewState["thn_pelaksanaan"].ToString();
            }
            string namaFileAsli = ViewState["id_dokumen_bukti_luaran3"].ToString();
            string namaFileDiunduh = lblJnsDokBuktiLuaran3.Text + ".pdf";

            if (File.Exists(Server.MapPath(folderUnduh + "/" + namaFileAsli + ".pdf")))
            {
                unduhPDF(folderUnduh, namaFileAsli, namaFileDiunduh);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }

        protected void lbUnduhPdf4_Click(object sender, EventArgs e)
        {
            string folderUnduh = "~/fileUpload20/laporan_kemajuan/BuktiLuaran/" + ViewState["thn_pelaksanaan"].ToString();
            if (ViewState["kd_tahapan_kegiatan"].ToString() == KD_TAHAP_LAP_AKHIR)
            {
                folderUnduh = FOLDER_BERKAS_LAP_AKHIR + ViewState["thn_pelaksanaan"].ToString();
            }
            string namaFileAsli = ViewState["id_dokumen_bukti_luaran4"].ToString();
            string namaFileDiunduh = lblJnsDokBuktiLuaran4.Text + ".pdf";

            if (File.Exists(Server.MapPath(folderUnduh + "/" + namaFileAsli + ".pdf")))
            {
                unduhPDF(folderUnduh, namaFileAsli, namaFileDiunduh);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
            }
        }


        private void unduhPDF(string folderUnduh, string namaFileAsli, string namaFileDiunduh)
        {
            //string namaBerkas = "dokumenUsulan.pdf";
            var atributUnduh = new AtributUnduh
            {
                FolderUnduh = folderUnduh,
                NamaBerkas = namaFileAsli + ".pdf",
                NamaBerkasdiUnduh = namaFileDiunduh
            };
            Session["AtributUnduh"] = atributUnduh;
            var unduhForm = "Helper/unduhFile.aspx";
            Response.Redirect(unduhForm);
        }
    }
}