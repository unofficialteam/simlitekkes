using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using simlitekkes.UIControllers;
using simlitekkes;

namespace simlitekkes.UserControls.Pengusul.mitraPengabdian
{
    public partial class mitraPelaksanaPTPPUPIK : System.Web.UI.UserControl
    {
        uiNotify noty = new uiNotify();
        Models.Pengusul.mitraAbdimas objMitraAbdimas = new Models.Pengusul.mitraAbdimas();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitData(Guid idUsulanKegiatan, Guid idMitra)
        {           
            if (idMitra == Guid.Empty)
            {
                // data baru
                ViewState["isNew"] = true;            
                ViewState["idMitra"] = Guid.NewGuid();
                ViewState["id_usulan_kegiatan"] = idUsulanKegiatan;
            }
            else
            {
                // load data
            }
        }

        public bool Simpan()
        {
            //var isNew = bool.Parse(ViewState["IsNew"].ToString());

            List<string> emptyField = new List<string>();
            if (tbNamaPimpinan.Text.Trim().Length == 0) emptyField.Add("Nama Pimpinan");
            if (tbAlamatInstitusi.Text.Trim().Length == 0) emptyField.Add("Alamat Institusi Mitra");
            if (tbJabatan.Text.Trim().Length == 0) emptyField.Add("Jabatan");

            if (emptyField.Count > 0)
            {
                var errorMessage = "Data berikut harus diisi terlebih dahulu : " + string.Join(", ", emptyField.ToArray());
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan", errorMessage);
                return true;
            }

            var idMitraPelaksanaPT = Guid.Parse(ViewState["idMitra"].ToString());
            var idUsulanKeg = Guid.Parse(ViewState["id_usulan_kegiatan"].ToString());
            int idTipeMitraPelaksanaPT = 1;
            string namaMitraPelaksanaPT = tbNamaPimpinan.Text;
            string jabatan = tbJabatan.Text;
            string alamatInstitusiMitra = tbAlamatInstitusi.Text;
            string danaThn1 = tbPendanaanThn1.Text;
            string danaThn2 = tbPendanaanThn2.Text;
            string danaThn3 = tbPendanaanThn3.Text;
            int kdKategoriMitra = 1;

            if (!objMitraAbdimas.insupMitraPelaksanaPPK(Guid.Parse(ViewState["id_usulan_kegiatan"].ToString()), kdKategoriMitra
                        , idTipeMitraPelaksanaPT, namaMitraPelaksanaPT, alamatInstitusiMitra, jabatan
                        , idMitraPelaksanaPT))
            {
                noty.Notify(this.Page, uiNotify.NotifyType.error, "Terjadi Kesalahan",
                   objMitraAbdimas.errorMessage);
            }

            return true;
        }
    }
}