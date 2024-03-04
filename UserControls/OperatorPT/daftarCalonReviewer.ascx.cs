using simlitekkes.Core;
using simlitekkes.Helper;
using simlitekkes.Models;
using simlitekkes.Models.PT;
using simlitekkes.UIControllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.OperatorPT
{
    public partial class daftarCalonReviewer : System.Web.UI.UserControl
    {
        calonReviewerNasional objCalonReviewer = new calonReviewerNasional();
        login objLogin;

        uiNotify noty = new uiNotify();
        uiPaging objPaging = new uiPaging();
        uiModal objModal = new uiModal();
        const string PATH_UNGGAH_BERKAS = "~/fileUpload/sertifikatCalonReviewer/";
        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (login)Session["objLogin"];

            if (!IsPostBack)
            {
                isiGVCalonReviewer();
            }
        }

        private void isiGVCalonReviewer()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            int offset = 0;
            if (!objCalonReviewer.listReviewer(ref dt, objLogin.idInstitusi, ddlStatusPersetujuan.SelectedValue, 
                int.Parse(ddlJmlBaris.SelectedValue), offset))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objCalonReviewer.errorMessage);
                return;
            }

            try
            {
                gvDaftarReviewer.DataSource = dt;
                gvDaftarReviewer.DataBind();

                lblJmlRecords.Text = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", ex.Message);
                return;
            }

        }

        protected void lbRefresh_Click(object sender, EventArgs e)
        {
            isiGVCalonReviewer();
        }

        private void refreshForm()
        {
            lbSimpan.Visible = false;
            tbNidn.Text = string.Empty;
            tbNama.Text = string.Empty;
            tbJabatanFungsional.Text = string.Empty;
            //tbJenjangPendidikan.Text = string.Empty;
            //tbNamaProdi.Text = string.Empty;
            //tbSurel.Text = string.Empty;
            tbKompetensi.Text = string.Empty;
            tbNomorSertifikat.Text = string.Empty;
            tbTanggalAkhirBerlakuSertifikat.Text = string.Empty;
        }

        protected void lbDataBaru_Click(object sender, EventArgs e)
        {
            lbCek.Visible = true;
            tbNidn.Enabled = true;
            refreshForm();
            lbSimpan.Visible = false;
            ViewState["isNew"] = true;
            objModal.ShowModal(this.Page, "ReviewerModal");
        }

        protected void gvDaftarReviewer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvDaftarReviewer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id_calon_reviewer_penelitian = gvDaftarReviewer.DataKeys[e.Row.RowIndex]["id_calon_reviewer_penelitian"].ToString();
                string path = PATH_UNGGAH_BERKAS + "{0}.pdf";
                path = string.Format(path, id_calon_reviewer_penelitian);
                LinkButton lbUnduhSertifikat = (LinkButton)e.Row.FindControl("lbUnduhSertifikat");
                lbUnduhSertifikat.ForeColor = System.Drawing.Color.Gray;
                lbUnduhSertifikat.Enabled = false;
                if (File.Exists(Server.MapPath(path)))
                {
                    lbUnduhSertifikat.ForeColor = System.Drawing.Color.Red;
                    lbUnduhSertifikat.Enabled = true;
                }

                Label lblStatusPersetujuan = (Label)e.Row.FindControl("lblStatusPersetujuan");

                lblStatusPersetujuan.BackColor = System.Drawing.Color.LightPink;
                if (lblStatusPersetujuan.Text == "Disetujui")
                {
                    lblStatusPersetujuan.BackColor = System.Drawing.Color.LightGreen;
                }

                LinkButton lbEditDataCalon = (LinkButton)e.Row.FindControl("lbEditDataCalon");
                lbEditDataCalon.Visible = false;
                if (lblStatusPersetujuan.Text == "Belum ditetapkan")
                {
                    lbEditDataCalon.Visible = true;
                }
            }
        }

        protected void lbCek_Click(object sender, EventArgs e)
        {
            objLogin = (login)Session["objLogin"];
            DataTable dt = new DataTable();
            if (objCalonReviewer.getDosen(ref dt, tbNidn.Text, objLogin.idInstitusi.ToString()))
            {
                if(dt.Rows.Count > 0)
                {
                    ViewState["id_personal"] = dt.Rows[0]["id_personal"].ToString();
                    tbNama.Text = dt.Rows[0]["nama"].ToString();
                    tbJabatanFungsional.Text = dt.Rows[0]["jabatan_fungsional"].ToString();
                    lbSimpan.Visible = true;
                    objModal.ShowModal(this.Page, "ReviewerModal");
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "Data dosen tidak ditemukan");
                    objModal.ShowModal(this.Page, "ReviewerModal");
                }
            };
        }

        protected void lbSimpan_Click(object sender, EventArgs e)
        {
            List<string> emptyField = new List<string>();
            if (tbNomorSertifikat.Text.Trim().Length == 0) emptyField.Add("Nomor Sertifikat");
            if (tbTanggalAkhirBerlakuSertifikat.Text.Trim().Length == 0) emptyField.Add("Tanggal Akhir Berlaku Sertifikat");
            if (tbKompetensi.Text.Trim().Length == 0) emptyField.Add("Kompetensi");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return;
            }

            objLogin = (login)Session["objLogin"];
            bool isNew = bool.Parse(ViewState["isNew"].ToString());
            if(isNew)
            {
                if (objCalonReviewer.insertCalonReviewerPenelitian(
                    Guid.Parse(ViewState["id_personal"].ToString()),
                    tbNomorSertifikat.Text,
                    tbTanggalAkhirBerlakuSertifikat.Text,
                    tbKompetensi.Text,
                    Guid.Parse(objLogin.idPersonal)
                    ))
                {
                    refreshForm();
                    objModal.HideModal(this.Page, "ReviewerModal");
                    isiGVCalonReviewer();

                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                    return;
                }
                else
                {
                    objModal.ShowModal(this.Page, "ReviewerModal");
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objCalonReviewer.errorMessage);
                    return;
                }
            }
            else
            {
                if (objCalonReviewer.updateCalonReviewerPenelitian(
                    Guid.Parse(ViewState["id_calon_reviewer_penelitian"].ToString()),
                    tbNomorSertifikat.Text,
                    tbTanggalAkhirBerlakuSertifikat.Text,
                    tbKompetensi.Text,
                    Guid.Parse(objLogin.idPersonal)
                    ))
                {
                    refreshForm();
                    objModal.HideModal(this.Page, "ReviewerModal");
                    isiGVCalonReviewer();
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Update data berhasil");
                    return;
                }
                else
                {
                    objModal.ShowModal(this.Page, "ReviewerModal");
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", objCalonReviewer.errorMessage);
                    return;
                }
            }
        }

        protected void gvDaftarReviewer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id_calon_reviewer_penelitian = gvDaftarReviewer.DataKeys[rowIndex]["id_calon_reviewer_penelitian"].ToString();
            ViewState["id_calon_reviewer_penelitian"] = id_calon_reviewer_penelitian;
            
            Label lblNama = ((Label)gvDaftarReviewer.Rows[rowIndex].FindControl("lblNama"));
            Label lblKompetensi = ((Label)gvDaftarReviewer.Rows[rowIndex].FindControl("lblKompetensi"));

            if (e.CommandName == "ubah")
            {
                lbCek.Visible = false;
                Label lblNIDN = ((Label)gvDaftarReviewer.Rows[rowIndex].FindControl("lblNIDN"));
                Label lblJabfung = ((Label)gvDaftarReviewer.Rows[rowIndex].FindControl("lblJabfung"));

                Label lblNoSertifikat = ((Label)gvDaftarReviewer.Rows[rowIndex].FindControl("lblNoSertifikat"));
                Label lblTglAkhirBerlaku = ((Label)gvDaftarReviewer.Rows[rowIndex].FindControl("lblTglAkhirBerlaku"));

                tbNidn.Enabled = false;
                tbNidn.Text = lblNIDN.Text;
                tbNama.Text = lblNama.Text;
                tbJabatanFungsional.Text = lblJabfung.Text;

                tbNomorSertifikat.Text = lblNoSertifikat.Text;
                DateTime tglAkhir = DateTime.Parse(lblTglAkhirBerlaku.Text);
                tbTanggalAkhirBerlakuSertifikat.Text = tglAkhir.ToString("yyyy-MM-dd");
                tbKompetensi.Text = lblKompetensi.Text;
                lbSimpan.Visible = true;
                ViewState["isNew"] = false;
                objModal.ShowModal(this.Page, "ReviewerModal");

            }
            else if (e.CommandName == "tetapkan")
            {
                tbNamaAtPenetapan.Text = lblNama.Text;
                tbKompetensiAtPenetapan.Text = lblKompetensi.Text;
                objModal.ShowModal(this.Page, "PenetapanReviewerModal");
            }
            else if (e.CommandName == "hapus")
            {
                tbNamaAtHapus.Text = lblNama.Text;
                tbKompetensiAtHapus.Text = lblKompetensi.Text;
                objModal.ShowModal(this.Page, "ModalHapusDataPencalonan");                
            }
            else if (e.CommandName == "unduh")
            {
                string path = PATH_UNGGAH_BERKAS + "{0}.pdf";
                path = string.Format(path, id_calon_reviewer_penelitian);
                
                if (File.Exists(Server.MapPath(path)))
                {
                    string namaBerkas = string.Format("SertifikatReviewer_{0}.pdf", lblNama.Text.Replace(" ",""));
                    var atributUnduh = new AtributUnduh
                    {
                        FolderUnduh = PATH_UNGGAH_BERKAS,
                        NamaBerkas = id_calon_reviewer_penelitian + ".pdf",
                        NamaBerkasdiUnduh = namaBerkas
                    };
                    Session["AtributUnduh"] = atributUnduh;
                    var unduhForm = "helper/unduhFile.aspx";
                    Response.Redirect(unduhForm);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen tidak ditemukan.");
                }
            }
            else if (e.CommandName == "unggah")
            {
                new uiModal().ShowModal(this.Page, "modalUnggahBerkas");
            }
        }

        protected void lbSimpanPenetapan_Click(object sender, EventArgs e)
        {
            string idCalon = ViewState["id_calon_reviewer_penelitian"].ToString();
            if(objCalonReviewer.updatePenetapansCalonReviewerPenelitian(idCalon, ddlPersetujuan.SelectedValue))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Penetapan calon reviewer berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Penetapan calon reviewer gagal.");
            }
            new uiModal().HideModal(this.Page, "PenetapanReviewerModal");
            isiGVCalonReviewer();
        }

        protected void lbHapusDataCalonReviewer_Click(object sender, EventArgs e)
        {
            if(objCalonReviewer.hapusCalonReviewerPenelitian(ViewState["id_calon_reviewer_penelitian"].ToString()))
            {
                objModal.HideModal(this.Page, "ModalHapusDataPencalonan");
                try
                {
                    string path = PATH_UNGGAH_BERKAS + "{0}.pdf";
                    path = string.Format(path, ViewState["id_calon_reviewer_penelitian"].ToString());
                    if (File.Exists(Server.MapPath(path)))
                        File.Delete(Server.MapPath(path));
                }
                catch (Exception err) { }
                isiGVCalonReviewer();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Hapus data berhasil.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Hapus data gagal.");
            }
        }

        protected void lbUnggah_Click(object sender, EventArgs e)
        {
            List<string> emptyField = new List<string>();
            if (!fileUploadsertifikat.HasFile) emptyField.Add("File belum dipilih");
            if (fileUploadsertifikat.PostedFile.ContentLength > 1024 * 1024) emptyField.Add("Ukuran file maksimal 1 Mbyte");
            if (!fileUploadsertifikat.FileName.ToLower().EndsWith(".pdf")) emptyField.Add("File wajib berekstensi PDF");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Kesalahan: " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                objModal.ShowModal(this.Page, "modalUnggahBerkas");
                return;
            }

            string filePath = string.Format(PATH_UNGGAH_BERKAS);
            if (!Directory.Exists(Server.MapPath(filePath)))
            {
                Directory.CreateDirectory(Server.MapPath(filePath));
            }

            filePath = filePath + ViewState["id_calon_reviewer_penelitian"].ToString() + ".pdf";
            fileUploadsertifikat.SaveAs(Server.MapPath(filePath));
            if(objCalonReviewer.updateStatusUnggahSertifikatCalonReviewerPenelitian(ViewState["id_calon_reviewer_penelitian"].ToString()))
            {
                isiGVCalonReviewer();
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Dokumen berhasil diunggah.");
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Dokumen gagal diunggah.");
            }
        }

        protected void ddlStatusPersetujuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGVCalonReviewer();
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGVCalonReviewer();
        }
    }
}