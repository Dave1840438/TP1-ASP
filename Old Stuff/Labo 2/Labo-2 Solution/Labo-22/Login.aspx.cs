using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Labo_22
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["message"] != null)
                LBL_Message.Text = (String)Session["message"];

            if (!Page.IsPostBack)
            {
                Session["CurrentUser"] = new USERS((String)Application["MainDB"], this);
                Session["UserValid"] = false;
            }
        }

        protected void CV_UserName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            USERS users = (USERS)Session["CurrentUser"];
            args.IsValid = users.Exist(TB_UserName.Text);
        }

        protected void CV_Password_ServerValidate(object source, ServerValidateEventArgs args)
        {
            USERS users = (USERS)Session["CurrentUser"];
            args.IsValid = users.Valid(TB_UserName.Text, TB_Password.Text);
        }

        protected void BTN_Login_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Session["UserValid"] = true;
                Response.Redirect("Accueil.aspx");
            }
        }
    }
}