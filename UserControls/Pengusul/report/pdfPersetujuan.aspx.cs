using simlitekkes.Models.report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace simlitekkes.UserControls.Pengusul.report
{
    public partial class pdfPersetujuan : System.Web.UI.Page
    {
        pdfUsulanBaru mdlPdfUsulanBaru = new pdfUsulanBaru();

        protected void Page_Load(object sender, EventArgs e)
        {
            string param =  Request.QueryString.Get("id_usulan_kegiatan");
            isiPersetujuan(param);
        }

        public void isiPersetujuan(string p_id_usulan_kegiatan)
        {
            DataTable dt = new DataTable();
            mdlPdfUsulanBaru.GetDataAproval(ref dt, Guid.Parse(p_id_usulan_kegiatan));
            if (dt.Rows.Count > 0)
            {
                lblTglKirim.Text = dt.Rows[0]["tgl_kirim_usulan"].ToString();
                lblTglPersetujuan.Text = dt.Rows[0]["tgl_persetujuan"].ToString();
                lblNamaUnit.Text = dt.Rows[0]["nama_lembaga"].ToString();
                lblNamaPimpinan.Text = dt.Rows[0]["nama_pimpinan_pemberi_persetujuan"].ToString();
                lblSebutanJabatan.Text = dt.Rows[0]["nama_jabatan"].ToString();
            }
        }
    }
}