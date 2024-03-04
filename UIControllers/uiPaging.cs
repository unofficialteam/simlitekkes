using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace simlitekkes.UIControllers
{
    [Serializable()]
    public class uiPaging
    {
        #region Fields

        private int _jmlKolomPaging;
        private int _jmlPages;
        private int _jmlBarisLastPage;
        private int _jmlTotalBaris;
        private int _barisBelakang;
        private int _barisDepan;

        private string _errorMessage;

        #endregion

        #region Konstruktor dan Destruktor

        public uiPaging()
        {
            this._jmlKolomPaging = 5;
        }

        ~uiPaging()
        {

        }

        #endregion

        #region Properties

        public int jmlTotalBaris
        {
            get { return this._jmlTotalBaris; }
        }

        public int jmlPages
        {
            get { return this._jmlPages; }
            set { this._jmlPages = value; }
        }

        public int jmlKolomPaging
        {
            get { return this._jmlKolomPaging; }
            set { this._jmlKolomPaging = value; }
        }

        public string errorMessage
        {
            get { return this._errorMessage; }
        }

        #endregion

        #region Methods

        public bool setPaging(ref Menu objMenuPage, int barisPerPage, int jmlTotalBaris)
        {
            bool retValue = false;
            objMenuPage.StaticMenuItemStyle.CssClass = "page-item";
            try
            {
                if (jmlTotalBaris < 1) return retValue;

                this._jmlTotalBaris = jmlTotalBaris;
                objMenuPage.Items.Clear();
                if (barisPerPage == -1)
                {
                    barisPerPage = jmlTotalBaris;
                }

                int sisaBaris = jmlTotalBaris % barisPerPage;
                this._jmlBarisLastPage = sisaBaris;

                this._jmlPages = 0;
                if (sisaBaris == 0)
                    this._jmlPages = jmlTotalBaris / barisPerPage;
                else
                    this._jmlPages = (jmlTotalBaris / barisPerPage) + 1;

                if (this._jmlPages <= this._jmlKolomPaging)
                {
                    //MenuItem mi1 = new MenuItem("Prev", "Prev");
                    MenuItem mi1 = new MenuItem("Prev", "-1");
                    objMenuPage.Items.Add(mi1);
                    for (int i = 0; i < this._jmlPages; i++)
                    {
                        objMenuPage.Items.Add(new MenuItem((i + 1).ToString(), i.ToString()));
                    }
                    objMenuPage.Items[1].Selected = true;
                    objMenuPage.Items[0].Enabled = false;
                }
                else if (this._jmlPages > this._jmlKolomPaging)
                {
                    //MenuItem mi1 = new MenuItem("Prev", "Prev");
                    MenuItem mi1 = new MenuItem("Prev", "-1");
                    objMenuPage.Items.Add(mi1);
                    for (int i = 0; i < this._jmlKolomPaging; i++)
                    {
                        objMenuPage.Items.Add(new MenuItem((i + 1).ToString(), i.ToString()));
                    }
                    //MenuItem miAkhir = new MenuItem("Next", "Next");
                    MenuItem miAkhir = new MenuItem("Next", this._jmlKolomPaging.ToString());
                    objMenuPage.Items.Add(miAkhir);
                    this._barisBelakang = 0;
                    this._barisDepan = jmlPages - this._jmlKolomPaging;

                    objMenuPage.Items[1].Selected = true;
                    objMenuPage.Items[0].Enabled = false;
                }

                retValue = true;
            }
            catch (Exception e)
            {
                this._errorMessage = "Set Paging.\n" + e.Message;
            }

            return retValue;
        }

        public bool changePage(ref Menu objMenuPage, string strPage)
        {
            bool retValue = false;
            string itemsText = strPage;

            try
            {
                switch (itemsText)
                {
                    case "Prev":
                        if (this._barisDepan == 0)
                        {
                            int itemPertama = int.Parse(objMenuPage.Items[1].Text);
                            if (this._barisBelakang >= this._jmlKolomPaging)
                            {
                                this._barisBelakang = this._barisBelakang - this._jmlKolomPaging;
                                this._barisDepan = this._barisDepan + objMenuPage.Items.Count - 2;
                            }
                            objMenuPage.Items.Clear();
                            MenuItem mi1 = new MenuItem("Prev", "Prev");
                            objMenuPage.Items.Add(mi1);
                            for (int i = 0; i < this._jmlKolomPaging; i++)
                            {
                                int a = itemPertama + i - this._jmlKolomPaging;
                                objMenuPage.Items.Add(new MenuItem((a).ToString(), (a - 1).ToString()));
                            }
                            MenuItem miAkhir = new MenuItem("Next", "Next");
                            objMenuPage.Items.Add(miAkhir);
                            if (this._barisBelakang == 0)
                            {
                                objMenuPage.Items[0].Enabled = false;
                            }
                        }
                        else if (this._barisBelakang >= this._jmlKolomPaging)
                        {

                            this._barisBelakang = this._barisBelakang - this._jmlKolomPaging;
                            this._barisDepan = this._barisDepan + this._jmlKolomPaging;

                            int itemPertama = int.Parse(objMenuPage.Items[1].Text);
                            objMenuPage.Items.Clear();
                            MenuItem mi1 = new MenuItem("Prev", "Prev");
                            objMenuPage.Items.Add(mi1);
                            for (int i = 0; i < this._jmlKolomPaging; i++)
                            {
                                int a = itemPertama + i - this._jmlKolomPaging;
                                objMenuPage.Items.Add(new MenuItem((a).ToString(), (a - 1).ToString()));
                            }
                            MenuItem miAkhir = new MenuItem("Next", "Next");
                            objMenuPage.Items.Add(miAkhir);

                            if (this._barisBelakang == 0)
                            {
                                objMenuPage.Items[0].Enabled = false;
                            }
                        }
                        objMenuPage.Items[this._jmlKolomPaging + 1].Enabled = true;
                        objMenuPage.Items[this._jmlKolomPaging].Selected = true;
                        break;
                    case "Next":
                        if (this._barisDepan >= this._jmlKolomPaging)
                        {
                            this._barisBelakang = this._barisBelakang + this._jmlKolomPaging;
                            this._barisDepan = this._barisDepan - this._jmlKolomPaging;

                            int itemPertama2 = int.Parse(objMenuPage.Items[1].Text);
                            objMenuPage.Items.Clear();

                            MenuItem mi1 = new MenuItem("Prev", "Prev");
                            objMenuPage.Items.Add(mi1);
                            for (int i = 0; i < this._jmlKolomPaging; i++)
                            {
                                int a = itemPertama2 + i + this._jmlKolomPaging;
                                objMenuPage.Items.Add(new MenuItem((a).ToString(), (a - 1).ToString()));
                            }
                            MenuItem miAkhir = new MenuItem("Next", "Next");
                            objMenuPage.Items.Add(miAkhir);

                            if (this._barisDepan == 0)
                            {
                                objMenuPage.Items[this._jmlKolomPaging + 1].Enabled = false;
                            }
                            objMenuPage.Items[0].Enabled = true;
                            objMenuPage.Items[1].Selected = true;
                        }
                        else if (this._barisDepan < this._jmlKolomPaging)
                        {
                            int itemPertama3 = int.Parse(objMenuPage.Items[1].Text);
                            int sisaPage = this._barisDepan;
                            if (this._barisDepan > 0)
                            {
                                this._barisBelakang = this._barisBelakang + this._jmlKolomPaging;
                                int nextD = this._barisDepan;

                                objMenuPage.Items.Clear();
                                MenuItem mi1 = new MenuItem("Prev", "Prev");
                                objMenuPage.Items.Add(mi1);
                                for (int i = 0; i < this._barisDepan; i++)
                                {
                                    int a = itemPertama3 + i + this._jmlKolomPaging;
                                    objMenuPage.Items.Add(new MenuItem((a).ToString(), (a - 1).ToString()));
                                }
                                MenuItem miAkhir = new MenuItem("Next", "Next");
                                objMenuPage.Items.Add(miAkhir);
                                objMenuPage.Items[0].Enabled = true;
                                objMenuPage.Items[1].Selected = true;
                                objMenuPage.Items[nextD + 1].Enabled = false;
                                this._barisDepan = 0;
                                string s = objMenuPage.Items[1].Value;
                            }
                        }
                        break;
                    default:
                        break;
                }
                retValue = true;
                this._errorMessage = "Perubahan halaman sukses.";
            }
            catch (Exception e)
            {
                this._errorMessage = "Perubahan halaman gagal.\n" + e.Message;
            }

            return retValue;
        }

        #endregion
    }
}