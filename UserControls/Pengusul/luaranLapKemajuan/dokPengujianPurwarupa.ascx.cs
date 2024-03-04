using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;

namespace simlitekkes.UserControls.Pengusul.luaranLapKemajuan
{
    public partial class dokPengujianPurwarupa : System.Web.UI.UserControl
    {
        public event EventHandler OnChildEvent;
        uiNotify noty = new uiNotify();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this.lbSimpan);
        }

        public void setData(string idTransaksiKegiatan, string strJudulForm, int idKelompokLuaran, string thnPelaksanaan)
        {
            ViewState["id_transaksi_kegiatan"] = idTransaksiKegiatan;
            ViewState["id_kelompok_luaran"] = idKelompokLuaran;
            ViewState["thn_pelaksanaan"] = thnPelaksanaan;
            lblJudulForm.Text = strJudulForm;
        }

        protected void ddlStatusDokumen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnChildEvent != null)
                OnChildEvent(sender, null);
        }
    }
}