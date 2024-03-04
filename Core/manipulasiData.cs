using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using System.Text;

namespace simlitekkes.Core
{
    public class manipulasiData
    {
        #region Fields

        #endregion

        #region Konstruktor dan Destruktor

        public manipulasiData()
        { }

        ~manipulasiData() { }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public DataTable filterData(DataTable objDataTabel, string expression, string sortOrder = null)
        {
            DataTable resDT = objDataTabel.Clone();
            DataRow[] resRows;

            if (sortOrder != null)
                resRows = objDataTabel.Select(expression, sortOrder);
            else
                resRows = objDataTabel.Select(expression);

            resRows.CopyToDataTable<DataRow>(resDT, LoadOption.Upsert);

            return resDT;
        }

        public string dataTableToJSON(DataTable objDataTabel, string valueField = null, string dataField = null)
        {
            string[] selectedColumns = new[] { valueField, dataField };
            DataTable dt = new DataView(objDataTabel).ToTable(false, selectedColumns);
            dt.Columns[0].ColumnName = "value";
            dt.Columns[1].ColumnName = "data";

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        public string removeUnicode(string inputString)
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

        public string convertFormatDana(string input, string titikOrKoma=".")
        {
            string strBaru4 = input;
            int loop = 0;
            for (int i = 3; i < input.Length; i += 3)
            {
                strBaru4 = strBaru4.Insert(strBaru4.Length - i - loop, titikOrKoma);
                loop++;
            }
            return strBaru4;
        }

        public string convertFormatDana2a(string input)
        {
            string angka;
            string angkad="";
            string angkab="";
            if (input == "") { input = "0"; };
            if (input.Length > 0 || input!="0")
            {

                input = input.Replace(",", ".");
                int titik = input.IndexOf(".");
                if (titik < 0) { input = input + ".00"; titik = input.IndexOf(".");  };
                 angkad= input.Substring(0, titik);
                 angkab= input.Substring(titik, 3);
                string strBaru4 = angkad;
                int loop = 0;
                for (int i = 3; i < input.Length - 2; i += 3)
                {
                    strBaru4 = strBaru4.Insert(strBaru4.Length - i - loop, ",");
                    loop++;
                }
                angka = strBaru4 + angkab;
                if (angka.Substring(0, 1) == ",") { angka = angka.Substring(1, angka.Length - 1); };
            }
            else
            {
                angka = "0.00";
            }
            return angka;
        }

        public string convertFormatDana2(string input)
        {
            string angka;
            string angkad = "";
            string angkab = "";
            if (input == "") { input = "0"; };
            if (input.Length > 0 || input != "0")
            {

                input = input.Replace(",", ".");
                int titik = input.IndexOf(".");
                if (titik < 0) { input = input + ".00"; titik = input.IndexOf("."); };
                angkad = input.Substring(0, titik);
                angkab = input.Substring(titik, 3);
                string strBaru4 = angkad;
                int loop = 0;
                for (int i = 3; i < input.Length - 2; i += 3)
                {
                    strBaru4 = strBaru4.Insert(strBaru4.Length - i - loop, ",");
                    loop++;
                }
                angka = strBaru4 + angkab;
                if (angka.Substring(0, 1) == ",") { angka = angka.Substring(1, angka.Length - 1); };
            }
            else
            {
                angka = "0.00";
            }


            if (angka.EndsWith(",00") || angka.EndsWith(".00"))
            {
                angka = angka.Substring(0, angka.Length - 3);
            }


            return angka;
        }

        #endregion

        #region Private Functions

        #endregion

    }
}