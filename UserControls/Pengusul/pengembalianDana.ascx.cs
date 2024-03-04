using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using simlitekkes.UIControllers;
using System.IO;
using System.Text;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class pengembalianDana : System.Web.UI.UserControl
    {
        Models.Pengusul.pengembalianDana modelPengembalianDana = new Models.Pengusul.pengembalianDana();

        Models.login objLogin;
        uiNotify noty = new uiNotify();
        uiModal modal = new uiModal();
        UIControllers.uiGridView obj_uiGridView = new UIControllers.uiGridView();
        uiModal objModal = new uiModal();

        protected void Page_Load(object sender, EventArgs e)
        {
            objLogin = (Models.login)Session["objLogin"];

            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.gvUsulan);

            if (!IsPostBack)
            {
                isiGvDaftarUsulan(0);
            }
        }

        private void isiGvDaftarUsulan(int idxPage)
        {
            Guid idPersonal = Guid.Parse(objLogin.idPersonal);

            if (!modelPengembalianDana.getJmlDaftarUsulan(idPersonal))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPengembalianDana.errorMessage);
            
            pagingDaftarUsulan.currentPage = idxPage;
            pagingDaftarUsulan.setPaging(int.Parse(ddlJmlBaris.SelectedValue), modelPengembalianDana.numOfRecords);

            modelPengembalianDana.currentPage = idxPage;
            modelPengembalianDana.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!modelPengembalianDana.getDaftarUsulan(idPersonal))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPengembalianDana.errorMessage);
                return;
            }

            if (!obj_uiGridView.bindToGridView(ref gvUsulan, modelPengembalianDana.currentRecords))

            if (modelPengembalianDana.numOfRecords < 1)
            {
                pagingDaftarUsulan.setPaging(int.Parse(ddlJmlBaris.SelectedValue), 1);
            }
        }

        protected void ddlJmlBaris_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiGvDaftarUsulan(0);
        }

        protected void pagingDaftarUsulan_PageChanging(object sender, EventArgs e)
        {
            Guid idPersonal = Guid.Parse(objLogin.idPersonal);

            modelPengembalianDana.currentPage = pagingDaftarUsulan.currentPage;
            modelPengembalianDana.rowsPerPage = int.Parse(ddlJmlBaris.SelectedValue);

            if (!modelPengembalianDana.getDaftarUsulan(idPersonal))
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Tidak ada data");

            obj_uiGridView = new UIControllers.uiGridView();
            obj_uiGridView.bindToGridView(ref gvUsulan, modelPengembalianDana.currentRecords);
        }

        protected void gvUsulan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblNomor = (Label)e.Row.FindControl("lblNomor");
                lblNomor.Text = (e.Row.RowIndex + 1 + int.Parse(ddlJmlBaris.SelectedValue) * (pagingDaftarUsulan.currentPage)).ToString();

                string stsUnggah = gvUsulan.DataKeys[e.Row.RowIndex]["sts_unggah_berkas"].ToString();
                LinkButton lbUnduhPengembalianDana = (LinkButton)e.Row.FindControl("lbUnduhPengembalianDana");

                if (stsUnggah == "1")
                {
                    lbUnduhPengembalianDana.CssClass = "fa fa-download btn btn-success";
                }
                else
                {
                    lbUnduhPengembalianDana.CssClass = "fa fa-download btn btn-default";
                }
            }
        }

        protected void gvUsulan_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsulan.EditIndex = e.NewEditIndex;
            isiGvDaftarUsulan(0);
        }

        protected void gvUsulan_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsulan.EditIndex = -1;
            isiGvDaftarUsulan(0);
        }

        protected void gvUsulan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string jmlSetoran;
            Guid id_usulan_kegiatan = Guid.Parse(gvUsulan.DataKeys[e.RowIndex].Values[0].ToString());
            string no_ntpn = ((TextBox)gvUsulan.Rows[e.RowIndex].FindControl("tbNoNTPNEdit")).Text.ToString();
            jmlSetoran = ((TextBox)gvUsulan.Rows[e.RowIndex].FindControl("tbJmlSetoranEdit")).Text.Replace(",", "").Replace(".", "").ToString();
            jmlSetoran = removeUnicode(jmlSetoran);

            LinkButton lbUnduhPengembalianDana = (LinkButton)gvUsulan.Rows[e.RowIndex].FindControl("lbUnduhPengembalianDana");
            string sts_unggah_berkas = "0";
            if (no_ntpn == "" || jmlSetoran == "0")
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", "No NTPN dan Jumlah Setoran Wajib diisi!");
            }
            else
            {
                if (!modelPengembalianDana.insupPengembalianDana(id_usulan_kegiatan, no_ntpn, jmlSetoran, sts_unggah_berkas))
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", modelPengembalianDana.errorMessage);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data berhasil");
                }

                DataBoundLiteralControl litCol1 = gvUsulan.Rows[e.RowIndex].Cells[1].Controls[0] as DataBoundLiteralControl;
                if (litCol1 != null) lbUnduhPengembalianDana.Text = litCol1.Text;

                gvUsulan.EditIndex = -1;
                isiGvDaftarUsulan(0);
            }
        }

        protected void gvUsulan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            string namaUtkFile = objLogin.namaLengkap.Replace(" ", "").Replace(".", "_");

            string no_ntpn = gvUsulan.DataKeys[row.RowIndex]["no_ntpn"].ToString();
            string jml_setoran = gvUsulan.DataKeys[row.RowIndex]["jml_setoran"].ToString();
            Guid id_usulan_kegiatan = Guid.Parse(gvUsulan.DataKeys[row.RowIndex]["id_usulan_kegiatan"].ToString());
            ViewState["id_usulan_kegiatan"] = id_usulan_kegiatan;
            string sts_unggah_berkas = gvUsulan.DataKeys[row.RowIndex]["sts_unggah_berkas"].ToString();

            switch (e.CommandName)
            {
                case "unggah":
                    
                    if (jml_setoran == "" || jml_setoran == "0" && no_ntpn.Length == 1 || no_ntpn == "")
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Jumlah Setoran dan No. NTPN Belum Diisi" + modelPengembalianDana.errorMessage);
                        return;
                    }
                    else
                    {
                        modal.ShowModal(this.Page, "modalPengembalianDana");
                    }
                    break;
                case "unduhBerkas":
                    
                    if (sts_unggah_berkas.Trim().Equals("0") || sts_unggah_berkas.Trim().Equals(""))
                    {
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File belum diunggah" + modelPengembalianDana.errorMessage);
                        return;
                    }
                    else
                    {
                        id_usulan_kegiatan = Guid.Parse(gvUsulan.DataKeys[row.RowIndex]["id_usulan_kegiatan"].ToString());
                        string filePath = "~/fileUpload/pengembalianDana/" + id_usulan_kegiatan + ".pdf";
                        if (File.Exists(Server.MapPath(filePath)))
                        {
                            string namaFile = "pengembalianDana_" + namaUtkFile + ".pdf";
                            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + namaFile + "\"");
                            Response.ContentType = "application/pdf";
                            Response.TransmitFile(Server.MapPath(filePath));
                            Response.End();
                        }
                        else
                        {
                            noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File tidak ditemukan di server" + modelPengembalianDana.errorMessage);
                            return;
                        }
                    }
                    break;
            }
        }

        protected void lbUnggahDokumen_Click(object sender, EventArgs e)
        {
            if (fileUpload1.HasFile)
            {
                if (fileUpload1.PostedFile.ContentType == "application/pdf")
                {
                    if (fileUpload1.PostedFile.ContentLength < (5 * 1024 * 1024))
                    {
                        string filePath = "~/fileUpload/pengembalianDana/" + ViewState["id_usulan_kegiatan"].ToString() + ".pdf";
                        if (File.Exists(Server.MapPath(filePath)))
                        {
                            File.Delete(Server.MapPath(filePath));
                        }
                        unggahDokumen(filePath);
                    }
                    else
                        noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File yang akan diuggah ukurannya tidak boleh melebihi 5 MByte!!!" + modelPengembalianDana.errorMessage);
                }
                else
                {
                    noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Untuk Berkas silahkan upload file bertipe pdf/PDF !!!" + modelPengembalianDana.errorMessage);
                }
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "File belum dipilih..." + modelPengembalianDana.errorMessage);
            }
        }

        private void unggahDokumen(string path)
        {
            try
            {
                fileUpload1.SaveAs(Server.MapPath(path));
                insertUpdateStatusUnggah();
            }
            catch (Exception ex)
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Terjadi Kesalahan" + modelPengembalianDana.errorMessage);
            }
        }

        protected void insertUpdateStatusUnggah()
        {
            Guid id_usulan_kegiatan = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            string sts_unggah_berkas = "1";

            if (modelPengembalianDana.updatePengembalianDana(id_usulan_kegiatan, sts_unggah_berkas))
            {
                isiGvDaftarUsulan(0);
                noty.Notify(this.Page, uiNotify.NotifyType.success, "Informasi", "Simpan data dan unggah berhasil" + modelPengembalianDana.errorMessage);
            }
            else
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Informasi", "Kesalahan" + modelPengembalianDana.errorMessage);
            }
        }

        private string removeUnicode(string inputString)
        {
            string asAscii = Encoding.ASCII.GetString(
                Encoding.Convert(
                    Encoding.UTF8,
                    Encoding.GetEncoding(
                        Encoding.ASCII.EncodingName,
                        new EncoderReplacementFallback(string.Empty),
                        new DecoderExceptionFallback()
                        ),
                    Encoding.UTF8.GetBytes(inputString)
                )
            );
            return asAscii;
        }
    }
}