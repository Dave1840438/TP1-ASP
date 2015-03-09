using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmailSender
{
    public partial class MailForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static void ClientAlert(System.Web.UI.Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + message + "');", true);
        }

        protected void BTN_Send_Click(object sender, EventArgs e)
        {
            EMail eMail = new EMail();

            // Vous devez avoir un compte gmail
            eMail.From = "votrecompte@gmail.com";
            eMail.Password = "mot de passe gmail";
            eMail.SenderName = "votre nom";

            eMail.Host = "smtp.gmail.com";
            eMail.HostPort = 587;
            eMail.SSLSecurity = true;

            eMail.To = TB_To.Text;
            eMail.Subject = TB_Subject.Text;
            eMail.Body = TB_Body.Text;

            if (eMail.Send())
            {
                ClientAlert(this, "This eMail has been sent with success!");
                TB_To.Text = "";
                TB_Subject.Text = "";
                TB_Body.Text = "";
            }
            else
                ClientAlert(this, "An error occured while sendind this eMail!!!");
        }
    }
}