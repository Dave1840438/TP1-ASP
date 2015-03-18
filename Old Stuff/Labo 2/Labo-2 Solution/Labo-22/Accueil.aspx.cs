using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Labo_22
{
    public partial class Accueil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["message"] = "vous devez vous authentifier!!!";
            if (Session["UserValid"] == null)
                Response.Redirect("Login.aspx");

            Session["message"] = "";

            if (!(bool)Session["UserValid"])
                Response.Redirect("Login.aspx");
            else
                LBL_FullName.Text = ((USERS)Session["CurrentUser"]).FullName;
        }
    }
}