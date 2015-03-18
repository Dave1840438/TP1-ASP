using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo_Master_UpdatePanel
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("LBL_Page_Title")).Text = "Taux de change en temps réel (aux 10 secondes)...";
        }


        public static string GetXMLValue(System.Xml.XmlDocument xmlDoc, string XMLElement)
        {
            string value = "";

            System.Xml.XmlElement root = xmlDoc.DocumentElement;
            if (root != null)
            {
                System.Xml.XmlNodeList lst = root.GetElementsByTagName(XMLElement);
                foreach (System.Xml.XmlNode n in lst)
                {
                    value += n.InnerText;
                }
            }
            return value;
        }

        //Pour plus de détails voir:
        //http://www.webservicex.net/currencyconvertor.asmx?op=ConversionRate
        private string GetCurrency(string fromCurrency, String toCurrency)
        {
            String conversion = "";
            string WebServideURL = @"http://www.webservicex.net/currencyconvertor.asmx/ConversionRate?FromCurrency=" + fromCurrency + @"&ToCurrency=" + toCurrency;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                doc.Load(WebServideURL);

                conversion = doc.InnerText;

            }
            catch (Exception e)
            {
                Response.Write(@"Service http://www.earthtools.org/webservices.htm ne répond pas...");
            }
            return conversion;
        }
       protected void TimerUpdateCurrency_Tick(object sender, EventArgs e)
        {
            string USD = "$ " + GetCurrency("CAD", "USD");
            string EUR = "€ " + GetCurrency("CAD", "EUR");
            string GBP = "£ " + GetCurrency("CAD", "GBP");
            if (LBL_Currency_CADtoUSD.Text !=  USD)
            {
                LBL_Currency_CADtoUSD.Text = USD;
                LBL_Currency_CADtoUSD.ForeColor = Color.Red;
            }
            else
                LBL_Currency_CADtoUSD.ForeColor = Color.Black;

            if (LBL_Currency_CADtoEUR.Text != EUR)
            {
                LBL_Currency_CADtoEUR.Text = EUR;
                LBL_Currency_CADtoEUR.ForeColor = Color.Red;
            }
            else
                LBL_Currency_CADtoEUR.ForeColor = Color.Black;

            if (LBL_Currency_CADtoGBP.Text != GBP)
            {
                LBL_Currency_CADtoGBP.Text = GBP;
                LBL_Currency_CADtoGBP.ForeColor = Color.Red;
            }
            else
                LBL_Currency_CADtoGBP.ForeColor = Color.Black;
        }
    }
}