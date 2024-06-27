using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using simlitekkes.Models.Pengusul;
using System.Data;
using System.IO;

namespace simlitekkes.UserControls.Pengusul.perbaikanProposalAbdimas
{
    public partial class rabRevisiAbdimas : System.Web.UI.UserControl
    {
        UIControllers.uiNotify noty = new UIControllers.uiNotify();
        Models.Pengusul.RAB repo = new Models.Pengusul.RAB();
        Models.Sistem.usulanKegiatan objUsulanKegiatan = new Models.Sistem.usulanKegiatan();

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

        public void Refresh(Models.Sistem.usulanKegiatan usulanKegiatan)
        {
            isiddlKelompok();
            rblTahun.Items.Clear();
            int urutanSKg = usulanKegiatan.urutanTahunUsulanKegiatan;
            for (int i = urutanSKg; i >= urutanSKg; i--)
            {
                rblTahun.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            if (rblTahun.Items.Count > 0) rblTahun.SelectedIndex = 0;
            IdUsulanKegiatan = Guid.Parse(usulanKegiatan.idUsulanKegiatan);
            isiSummaryRAB();
            isigvRAB();
        }

        private void isiddlKelompok()
        {
            var dt = new DataTable();
            if (repo.GetKelompokBiaya(ref dt, "2"))
            {
                ddlKelompok.DataValueField = "id_rab_kelompok_biaya";
                ddlKelompok.DataTextField = "kelompok_biaya";
                ddlKelompok.DataSource = dt;
                ddlKelompok.DataBind();

                if (ddlKelompok.Items.Count > 0) ddlKelompok.SelectedIndex = 0;
            }
        }

        private void isiSummaryRAB()
        {
            var dt = new DataTable();
            if (repo.GetRekapRABRevisi(ref dt, IdUsulanKegiatan))
            {
                rptrSummary.DataSource = dt;
                rptrSummary.DataBind();

                ViewState["thnPertamaUsulan"] = dt.Rows[0]["thn_pertama_usulan"].ToString();

                if (dt.Rows.Count > 0)
                {
                    var totalDisetujui = dt.Compute("SUM(dana_disetujui)", string.Empty);
                    lblTotalDanaDisetujui.Text = decimal.Parse(totalDisetujui.ToString()).ToString("N0");

                    var totalDiajukan = dt.Compute("SUM(dana_diajukan)", string.Empty);
                    lblTotalDanaDiajukan.Text = decimal.Parse(totalDiajukan.ToString()).ToString("N0");
                }
                else
                {
                    lblTotalDanaDisetujui.Text = "Rp 0";
                    lblTotalDanaDiajukan.Text = "Rp 0";
                }

            }
        }

        private void isigvRAB()
        {
            var dt = new DataTable();
            if (repo.GetKomponenBelanjaRevisi(ref dt, IdUsulanKegiatan, Convert.ToInt32(rblTahun.SelectedValue),
                    Convert.ToInt32(ddlKelompok.SelectedValue)))
            {
                gvRAB.DataSource = dt;
                gvRAB.DataBind();

                if (dt.Rows.Count > 0)
                {
                    var total = dt.Compute("SUM(total)", string.Empty);
                    lblTotalBiaya.Text = decimal.Parse(total.ToString()).ToString("N0");
                }
                else
                    lblTotalBiaya.Text = "Rp 0";
            }
        }

        protected void rblTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            isigvRAB();
        }

        protected void ddlKelompok_SelectedIndexChanged(object sender, EventArgs e)
        {
            isigvRAB();
        }

        protected void lbSimpan_OnClick(object sender, EventArgs e)
        {
            List<RABKomponenBelanjaRevisi> listRABKomponenRevisi = new List<RABKomponenBelanjaRevisi>();
            for (var i = 0; i < gvRAB.Rows.Count; i++)
            {
                var row = gvRAB.Rows[i];
                var tbVolume = row.FindControl("tbVolume") as TextBox;
                var tbBiaya = row.FindControl("tbBiaya") as TextBox;
                var idKomponenBelanja = Convert.ToInt32(gvRAB.DataKeys[i]["id_rab_komponen_belanja"].ToString());
                var idKomponenBelanjaRevisiDataKey = gvRAB.DataKeys[i]["id_rab_komponen_belanja_revisi"].ToString();
                var idKomponenBelanjaRevisi = (string.IsNullOrEmpty(idKomponenBelanjaRevisiDataKey)) ?
                                               Guid.Empty : Guid.Parse(idKomponenBelanjaRevisiDataKey);

                var itemKomponen = new RABKomponenBelanjaRevisi()
                {
                    id_usulan_kegiatan = IdUsulanKegiatan,
                    id_rab_komponen_belanja = idKomponenBelanja,
                    id_rab_komponen_belanja_revisi = idKomponenBelanjaRevisi,
                    volume = Convert.ToInt32(tbVolume.Text),
                    harga_satuan = Convert.ToDecimal(tbBiaya.Text),
                    urutan_thn_usulan = Convert.ToInt32(rblTahun.SelectedValue)
                };

                listRABKomponenRevisi.Add(itemKomponen);

            }

            if (!repo.InsupKomponenBelanjaRevisi(listRABKomponenRevisi))
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !",
                            repo.errorMessage);
            }
            else
            {
                noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.success, "Informasi",
                            "Data berhasil disimpan...");

                isiSummaryRAB();
                isigvRAB();
            }

        }

        protected void lbExportExcel_Click(object sender, EventArgs e)
        {
            string thnPertamanUsulan = ViewState["thnPertamaUsulan"].ToString();
            if (int.Parse(thnPertamanUsulan.ToString()) < 2019)
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    var fileName = $"RAB.xlsx";
                    var dt = new DataTable();
                    if (!repo.GetRABUsulanExport(ref dt, IdUsulanKegiatan))
                    {
                        noty.Notify(this.Page, UIControllers.uiNotify.NotifyType.error, "Terjadi Kesalahan !", repo.errorMessage);
                        return;
                    }

                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("RAB");
                    ws.Cells["A1"].LoadFromDataTable(dt, true, OfficeOpenXml.Table.TableStyles.Light4);

                    //Autofit Column
                    for (int i = 1; i <= dt.Columns.Count; i++)
                    {
                        ws.Column(i).AutoFit(10);
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
            else
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
}