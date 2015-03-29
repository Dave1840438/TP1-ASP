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

            var mp = Master as Master_page;
            String result = "...?";

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            SqlCommand sqlcmdUserCheck = new SqlCommand("SELECT USERNAME FROM USERS WHERE USERNAME = '" + TBX_Username.Text + "'");
            sqlcmdUserCheck.Connection = connection;
            Page.Application.Lock();
            connection.Open();

            SqlDataReader userReader = sqlcmdUserCheck.ExecuteReader();

            String lele = "";
            if (userReader.Read())
                lele = userReader.GetString(0);

            userReader.Close();

            if (lele != "")
            {
                SqlCommand sqlcmdVerifyPassword = new SqlCommand("SELECT PASSWORD FROM USERS WHERE USERNAME = '" + TBX_Username.Text + "'");
                sqlcmdVerifyPassword.Connection = connection;
                SqlDataReader reader = sqlcmdVerifyPassword.ExecuteReader();

                result = "Wrong Password";

                reader.Read();
                String PW = reader.GetString(0);
                if (PW == TBX_Password.Text)
                {
                   FormsAuthentication.RedirectFromLoginPage(TBX_Username.Text, false);

                   // result = "Connection success";
                   // Session["isConnected"] = true;
                   // Session["Username"] = TBX_Username.Text;
                   // Response.Redirect("Index.aspx");
                }
            }
            else
                result = "Wrong username";

            connection.Close();
            Page.Application.UnLock();

            mp.setTitre(result);
        }

        protected void BTT_Inscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inscription.aspx");
        }

        protected void BTT_ForgotPassword_Click(object sender, EventArgs e)
        {
            //??????????
        }
    }
}