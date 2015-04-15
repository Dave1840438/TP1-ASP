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

            if ((int)Session["tries"] == 2)
            {
                ClientAlert(this, "Désolé que vous éprouviez des difficultés, veuillez réassayer plus tard.");
                Response.End();
            }
        }

        protected void BTT_Connect_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            if (Page.IsValid)
            {
               Page.Application.Lock();

               Global.unUsagerEnLigne u = new Global.unUsagerEnLigne();
               u.id = DBUtilities.getUserID(connection, TBX_Username.Text);
               u.sessionStart = DateTime.Now;
               u.sessionTimeOut = DateTime.Now.AddMinutes((int)Application["SessionTimeout"]);
               u.userIP = GetUserIP();

               ((Dictionary<string, Global.unUsagerEnLigne>)Application["OnlineUsersTwo"]).Add(TBX_Username.Text, u);

                if (!((List<long>)Application["OnlineUsers"]).Contains(DBUtilities.getUserID(connection, TBX_Username.Text)))
                {
                    ((List<long>)Application["OnlineUsers"]).Add(DBUtilities.getUserID(connection, TBX_Username.Text));
                }
                Page.Application.UnLock();

                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(TBX_Username.Text, true);
                authCookie.Expires = DateTime.Now.AddMinutes((int)Application["SessionTimeout"]);
                Session["isAuthenticated"] = true;
                Session["SessionStartTime"] = DateTime.Now;
                Response.Cookies.Add(authCookie);

                Page.Application.Lock();

                Response.Redirect("Index.aspx");
            }
        }

        public string GetUserIP()
        {
           string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
           if (!string.IsNullOrEmpty(ipList))
              return ipList.Split(',')[0];
           string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
           if (ipAddress == "::1") // local host
              ipAddress = "127.0.0.1";
           return ipAddress;
        }
        protected void BTT_Inscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inscription.aspx");
        }

        protected void BTT_ForgotPassword_Click(object sender, EventArgs e)
        {
            

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

            
        }

        public static void ClientAlert(System.Web.UI.Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + message + "');", true);
        }

        bool usernameIsValid;

        protected void CV_Password_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool passwordIsGood = false;

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
                Session["tries"] = (int)Session["tries"] + 1;
                CV_Password.ErrorMessage = "Le mot de passe est incorrect!";
                CV_Password.Text = "!";
            }
        }

        protected void CV_Username_ServerValidate(object source, ServerValidateEventArgs args)
        {
            

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

            
        }

        protected void CV_UserIsOnline_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (usernameIsValid)
            {
                SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

                Page.Application.Lock();

                args.IsValid = !((List<long>)Application["OnlineUsers"]).Contains(DBUtilities.getUserID(connection, TBX_Username.Text));

                Page.Application.UnLock();
            }
        }

    }
}