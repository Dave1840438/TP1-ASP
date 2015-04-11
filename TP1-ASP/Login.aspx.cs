using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
    public partial class Login : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Master_page;
            if (master != null)
                master.setTitre("Login...");
        }

        protected void BTT_Connect_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            if (Page.IsValid)
            {
                if (!((List<long>)Application["OnlineUsers"]).Contains(DBUtilities.getUserID(connection, TBX_Username.Text)))
                {
                    ((List<long>)Application["OnlineUsers"]).Add(DBUtilities.getUserID(connection, TBX_Username.Text));
                }

                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(TBX_Username.Text, true);
                authCookie.Expires = DateTime.Now.AddMinutes((double)Application["SessionTimeout"]);
                Response.Cookies.Add(authCookie);
                Session["isAuthenticated"] = true;
                Session["SessionStartTime"] = DateTime.Now;
                Response.Redirect("Index.aspx");
            }
        }

        protected void BTT_Inscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inscription.aspx");
        }

        protected void BTT_ForgotPassword_Click(object sender, EventArgs e)
        {
            Page.Application.Lock();

            SqlDataReader reader = null;

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            bool exists = DBUtilities.checkIfUsernameExists(connection, TBX_Username.Text);

            connection.Open();

            if (exists)
            {
                SqlCommand fetchNeededInfo = new SqlCommand();
                fetchNeededInfo.Connection = connection;
                fetchNeededInfo.CommandText = "SELECT EMAIL, PASSWORD FROM USERS WHERE USERNAME = '" + TBX_Username.Text + "'";
                reader = fetchNeededInfo.ExecuteReader();
            }



            if (reader != null)
            {
                reader.Read();

                EMail eMail = new EMail();

                // Vous devez avoir un compte gmail
                eMail.From = "tp1asp27@gmail.com";
                eMail.Password = "asdasdasd2";
                eMail.SenderName = "TravailUn ASP";

                eMail.Host = "smtp.gmail.com";
                eMail.HostPort = 587;
                eMail.SSLSecurity = true;

                eMail.To = reader.GetString(0);
                eMail.Subject = "Forgotten Password";
                eMail.Body = "Your password is: " + reader.GetString(1) + "<br/><br/>--<br/>Cordially, TactikalTeam (T3)";

                if (eMail.Send())
                    ClientAlert(this, "Le EMail a été envoyé avec succès!");
                else
                    ClientAlert(this, "Échec de l'envoi du EMail!");
            }
            else
            {
                ClientAlert(this, "Le nom d'utilisateur n'existe pas!");
            }
            connection.Close();

            Page.Application.UnLock();
        }

        public static void ClientAlert(System.Web.UI.Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + message + "');", true);
        }

        bool usernameIsValid;

        protected void CV_Password_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool passwordIsGood = false;

            Page.Application.Lock();


            if (usernameIsValid)
            {
                SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
                SqlCommand sqlcmdVerifyPassword = new SqlCommand("SELECT PASSWORD FROM USERS WHERE USERNAME = '" + TBX_Username.Text + "'");
                sqlcmdVerifyPassword.Connection = connection;
                connection.Open();
                SqlDataReader reader = sqlcmdVerifyPassword.ExecuteReader();

                reader.Read();

                if (reader.GetString(0) == TBX_Password.Text)
                    args.IsValid = passwordIsGood = true;

                reader.Close();
                connection.Close();
            }

            if (TBX_Password.Text == "")
            {
                args.IsValid = false;
                CV_Password.ErrorMessage = "Le mot de passe est vide!";
                CV_Password.Text = "Vide!";
            }
            else if (!passwordIsGood)
            {
                args.IsValid = false;
                CV_Password.ErrorMessage = "Le mot de passe est incorrect!";
                CV_Password.Text = "!";
            }

            Page.Application.UnLock();
        }

        protected void CV_Username_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Page.Application.Lock();

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            if (TBX_Username.Text == "")
            {
                args.IsValid = usernameIsValid = false;
                CV_Username.ErrorMessage = "Le nom d'usager est vide!";
                CV_Username.Text = "Vide!";
            }
            else if (!DBUtilities.checkIfUsernameExists(connection, TBX_Username.Text))
            {
                args.IsValid = usernameIsValid = false;
                CV_Username.ErrorMessage = "Cet usager n'existe pas!";
                CV_Username.Text = "!";
            }
            else
                args.IsValid = usernameIsValid = true;

            Page.Application.UnLock();
        }

    }
}