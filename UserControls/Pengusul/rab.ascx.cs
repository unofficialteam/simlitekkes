using OfficeOpenXml;
using simlitekkes.Models.Pengusul;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul
{
    public partial class rab : System.Web.UI.UserControl
    {

        UIControllers.uiNotify noty = new UIControllers.uiNotify();
        Models.Pengusul.RAB repo = new Models.Pengusul.RAB();

        private Guid IdUsulanKegiatan
        {
            get
            {
                if (ViewState["IdUsulanKegiatan"] == null) return Guid.Empty;
                return Guid.Parse(ViewState["IdUsulanKegiatan"].ToString());
            }
            set
            {
                ViewState["IdUsulanKegiatan"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //TODO Sesuaikan InitRAB
        public void InitRAB(Models.Sistem.usulanKegiatan usulanKegiatan, string kdJenisKegiatan)
        {
            ViewState["kdJenisKegiatan"] = kdJenisKegiatan;
            isiddlKelompok();
            isiInfoMinimalMaximalDana(usulanKegiatan);
            rblTahun.Items.Clear();
            for (int i = usulanKegiatan.urutanTahunUsulanKegiatan; i <= usulanKegiatan.lamaKegiatan; i++)
            {
                rblTahun.Items.Add(
                    new ListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString()
                    });
            }
            if (rblTahun.Items.Count > 0) rblTahun.SelectedIndex = 0;

            IdUsulanKegiatan = Guid.Parse(usulanKegiatan.idUsulanKegiatan); //Guid.Parse("887ffbe0-1fe4-4021-aadd-f15202ad3bf4");

            isiSummaryRAB();
            isilvRAB();
            isiKomponen(ddlKomponen);
        }

        //TODO Sesuaikan Info Dana Min dan Max
        private void isiInfoMinimalMaximalDana(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
            DataTable dt = new DataTable();
            decimal totalMinimal = 0, totalMaksimal = 0;

            if (ViewState["kdJenisKegiatan"].ToString() == "1")
            {
                var objIdentitasUsulan = new Models.Pengusul.identitasUsulan();
                objIdentitasUsulan.getPlafonDanaSbkThnImplementasi2018(ref dt,
                    usulanKegiatan.idKategoriSBK, usulanKegiatan.idSkema, usulanKegiatan.idBidangFokus);
                if (dt.Rows.Count > 0)
                {
                    totalMinimal = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString());
                    totalMaksimal = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString());
                    lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal"].ToString()).ToString("N0");
                    lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal"].ToString()).ToString("N0");
                }
                lblBidFokus.Visible = true;
                lblBidangFokusJudul.Visible = true;
                lblBidFokus.Text = usulanKegiatan.bidangFokus;
            }
            else
            {
                repo.GetRentangDanaSkemaKegiatan(ref dt, usulanKegiatan.idSkema);
                if (dt.Rows.Count > 0)
                {
                    lblDanaMinimal.Text = decimal.Parse(dt.Rows[0]["dana_minimal_thn_berjalan"].ToString()).ToString("N0");
                    lblDanaMaksimal.Text = decimal.Parse(dt.Rows[0]["dana_maksimal_thn_berjalan"].ToString()).ToString("N0");
                }
                lblBidFokus.Visible = false;
                lblBidangFokusJudul.Visible = false;
            }
        }

        private void isiddlKelompok()
        {
            var dt = new DataTable();
            string kdJenisKegiatan = ViewState["kdJenisKegiatan"].ToString();
            if (repo.GetKelompokBiaya(ref dt, kdJenisKegiatan))
            {
                ddlKelompok.DataValueField = "id_rab_kelompok_biaya";
                ddlKelompok.DataTextField = "kelompok_biaya";
                ddlKelompok.DataSource = dt;
                ddlKelompok.DataBind();

                if (ddlKelompok.Items.Count > 0) ddlKelompok.SelectedIndex = 0;
            }
        }

        private void isiKomponen(DropDownList ddl)
        {
            var dt = new DataTable();
            if (repo.GetKomponenBelanja(ref dt, IdUsulanKegiatan, Convert.ToInt32(rblTahun.SelectedValue),
                    Convert.ToInt32(ddlKelompok.SelectedValue)))
            {
                ddl.DataValueField = "id_rab_komponen_belanja";
                ddl.DataTextField = "komponen_belanja";
                ddl.DataSource = dt;
                ddl.DataBind();

                if (ddl.Items.Count > 0) ddl.SelectedIndex = 0;
            }
        }

        private DataTable isiSummaryRAB()
        {
            var dt = new DataTable();
            if (repo.GetRekapItemRAB(ref dt, IdUsulanKegiatan))
            {
                rptrSummary.DataSource = dt;
                rptrSummary.DataBind();

                if (dt.Rows.Count > 0)
                {
                    var totalDiajukan = dt.Compute("SUM(dana_diajukan)", string.Empty);
                    lblTotalDanaDiajukan.Text = decimal.Parse(totalDiajukan.ToString()).ToString("N0");
                }
                else
                {
                    lblTotalDanaDiajukan.Text = "Rp 0";
                }

            }
            return dt;
        }

        private void isilvRAB()
        {
            var dt = new DataTable();
            if (repo.GetItemBelanja(ref dt, IdUsulanKegiatan, Convert.ToInt32(rblTahun.SelectedValue),
                    Convert.ToInt32(ddlKelompok.SelectedValue)))
            {
                lvRAB.DataSource = dt;
                lvRAB.DataBind();

                if (dt.Rows.Count > 0)
                {
                    var total = dt.Compute("SUM(total_harga)", string.Empty);
                    lblTotalBiaya.Text = decimal.Parse(total.ToString()).ToString("N0");
                }
                else
                    lblTotalBiaya.Text = "0";
            }
        }

        protected void ClearTableFooter()
        {
            tbItem.Text = string.Empty;
            tbSatuan.Text = string.Empty;
            tbVolume.Text = string.Empty;
            tbHargaSatuan.Text = string.Empty;
        }

        protected void rblTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            isilvRAB();
        }

        protected void ddlKelompok_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTableFooter();
            isilvRAB();
            isiKomponen(ddlKomponen);
        }

        protected void lbSimpanItem_Click(object sender, EventArgs e)
        {
            var emptyData = new List<string>();
            if (string.IsNullOrWhiteSpace(tbItem.Text)) emptyData.Add("Nama Item");
            if (string.IsNullOrWhiteSpace(tbSatuan.Text)) emptyData.Add("Satuan");
            if (string.IsNullOrWhiteSpace(tbVolume.Text)) emptyData.Add("Volume");
            if (string.IsNullOrWhiteSpace(tbHargaSatuan.Text)) emptyData.Add("Harga Satuan");

            if (emptyData.Count > 0)
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !",
                            "Data berikut ini harus diisi terlebih dahulu :<br />" + string.Join(", ", emptyData));
                return;
            }

            var item = new RABItemBelanja()
            {
                id_usulan_kegiatan = IdUsulanKegiatan,
                urutan_thn_usulan = Convert.ToInt32(rblTahun.SelectedValue),
                id_rab_komponen_belanja = Convert.ToInt32(ddlKomponen.SelectedValue),
                nama_item = tbItem.Text,
                satuan = tbSatuan.Text,
                volume = Convert.ToInt32(tbVolume.Text),
                harga_satuan = Convert.ToDecimal(tbHargaSatuan.Text)

            };

            bool lanjut = true;
            DataTable dt = isiSummaryRAB();
            if (dt.Rows.Count > 0)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (rblTahun.SelectedValue == dt.Rows[a]["urutan_tahun"].ToString())
                    {
                        Decimal maxDanaPerthun = Convert.ToDecimal(lblDanaMaksimal.Text.Replace(",", "").Replace(".", ""));
                        Decimal totalDanaTahunanDiajukan = Convert.ToDecimal(dt.Rows[a]["dana_diajukan"].ToString());
                        totalDanaTahunanDiajukan += Convert.ToDecimal(tbVolume.Text) * Convert.ToDecimal(tbHargaSatuan.Text);

                        if (totalDanaTahunanDiajukan > maxDanaPerthun)
                        {
                            lanjut = false;
                        }
                    }
                }
            }

            if (!lanjut)
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !",
                           "Dana diajukan tahun ke-" + rblTahun.SelectedValue + " melebihi dana maksimal pertahun.");
                return;
            }

            if (!repo.InsertItemBelanja(item))
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !",
                            repo.errorMessage);
            }
            else
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi",
                            "Data berhasil disimpan...");

                ClearTableFooter();
                isiSummaryRAB();
                isilvRAB();
            }
        }

        protected void lvRAB_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvRAB.EditIndex = e.NewEditIndex;
            isilvRAB();
        }

        protected void lvRAB_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvRAB.EditIndex = -1;
            isilvRAB();
        }

        protected void lvRAB_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (lvRAB.EditIndex == ((ListViewDataItem)e.Item).DataItemIndex)
            {
                var ddlKomponenEdit = e.Item.FindControl("ddlKomponenEdit") as DropDownList;
                isiKomponen(ddlKomponenEdit);

                var drv = e.Item.DataItem as DataRowView;
                //ddlKomponenEdit.Items.FindByValue(drv["id_rab_komponen_belanja"].ToString()).Selected = true;
                ddlKomponenEdit.SelectedValue = drv["id_rab_komponen_belanja"].ToString();
            }
        }

        protected void lvRAB_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var idRABItemBelanja = Guid.Parse(lvRAB.DataKeys[e.ItemIndex]["id_rab_item_belanja"].ToString());
            var ddlKomponenEdit = lvRAB.Items[e.ItemIndex].FindControl("ddlKomponenEdit") as DropDownList;
            var tbItemEdit = lvRAB.Items[e.ItemIndex].FindControl("tbItemEdit") as TextBox;
            var tbSatuanEdit = lvRAB.Items[e.ItemIndex].FindControl("tbSatuanEdit") as TextBox;
            var tbVolumeEdit = lvRAB.Items[e.ItemIndex].FindControl("tbVolumeEdit") as TextBox;
            var tbHargaSatuanEdit = lvRAB.Items[e.ItemIndex].FindControl("tbHargaSatuanEdit") as TextBox;

            var emptyData = new List<string>();
            if (string.IsNullOrWhiteSpace(tbItemEdit.Text)) emptyData.Add("Nama Item");
            if (string.IsNullOrWhiteSpace(tbSatuanEdit.Text)) emptyData.Add("Satuan");
            if (string.IsNullOrWhiteSpace(tbVolumeEdit.Text)) emptyData.Add("Volume");
            if (string.IsNullOrWhiteSpace(tbHargaSatuanEdit.Text)) emptyData.Add("Harga Satuan");

            if (emptyData.Count > 0)
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !",
                            "Data berikut ini harus diisi terlebih dahulu :<br />" + string.Join(", ", emptyData));
                return;
            }

            var item = new RABItemBelanja()
            {
                id_rab_item_belanja = idRABItemBelanja,
                id_usulan_kegiatan = IdUsulanKegiatan,
                urutan_thn_usulan = Convert.ToInt32(rblTahun.SelectedValue),
                id_rab_komponen_belanja = Convert.ToInt32(ddlKomponenEdit.SelectedValue),
                nama_item = tbItemEdit.Text,
                satuan = tbSatuanEdit.Text,
                volume = Convert.ToInt32(tbVolumeEdit.Text),
                harga_satuan = Convert.ToDecimal(tbHargaSatuanEdit.Text)
            };

            bool lanjut = true;
            DataTable dt = isiSummaryRAB();
            if (dt.Rows.Count > 0)
            {
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    if (rblTahun.SelectedValue == dt.Rows[a]["urutan_tahun"].ToString())
                    {
                        Decimal maxDanaPerthun = Convert.ToDecimal(lblDanaMaksimal.Text.Replace(",", "").Replace(".", ""));
                        Decimal totalDanaTahunanDiajukan = Convert.ToDecimal(dt.Rows[a]["dana_diajukan"].ToString());
                        totalDanaTahunanDiajukan += Convert.ToDecimal(tbVolumeEdit.Text) * Convert.ToDecimal(tbHargaSatuanEdit.Text);

                        if (totalDanaTahunanDiajukan > maxDanaPerthun)
                        {
                            lanjut = false;
                        }
                    }
                }
            }

            if (!lanjut)
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !",
                           "Dana diajukan tahun ke-" + rblTahun.SelectedValue + " melebihi dana maksimal pertahun.");
                return;
            }

            if (!repo.UpdateItemBelanja(item))
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !",
                            repo.errorMessage);
            }
            else
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi",
                            "Data berhasil disimpan...");

                lvRAB.EditIndex = -1;
                isiSummaryRAB();
                isilvRAB();
            }
        }

        protected void lvRAB_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var idRABItemBelanja = Guid.Parse(lvRAB.DataKeys[e.ItemIndex]["id_rab_item_belanja"].ToString());

            if (!repo.DeleteItemBelanja(idRABItemBelanja))
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !",
                            repo.errorMessage);
            }
            else
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi",
                            "Data berhasil dihapus...");

                isiSummaryRAB();
                isilvRAB();
            }
        }

        protected void lbExportExcel_Click(object sender, EventArgs e)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var fileName = $"RAB.xlsx";
                var dt = new DataTable();

                for (var idx = 0; idx < rblTahun.Items.Count; idx++)
                {
                    dt.Clear();

                    if (!repo.GetRABItemBelanjaExport(ref dt, IdUsulanKegiatan, Convert.ToInt32(rblTahun.Items[idx].Text)))
                    {
                        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", repo.errorMessage);
                        return;
                    }


                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add($"Tahun {rblTahun.Items[idx].Text}");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);

                    if (dt.Rows.Count > 0)
                    {
                        var endRow = dt.Rows.Count + 1;

                        //Hitung Total Dana
                        ws.Cells[endRow + 1, 8].Formula = $"=SUM(H2:H{endRow})";
                        ws.Cells[endRow + 1, 7].Value = "TOTAL";

                        //Format kolom
                        ws.Cells[2, 7, endRow, 7].Style.Numberformat.Format = "#,##0";
                        ws.Cells[2, 8, endRow + 1, 8].Style.Numberformat.Format = "#,##0";
                    }

                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
                    }
                }

                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pck.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
                pck.Dispose();
            }
        }
    }    
}