using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TP1_ASP
{
    public partial class Master_page : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated && !(Request.Url.ToString().Contains("Login.aspx") || Request.Url.ToString().Contains("Logout.aspx") || Request.Url.ToString().Contains("Inscription.aspx")))
            {
                SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

                Master_Page_Avatar.ImageUrl = DBUtilities.getAvatar(connection, HttpContext.Current.User.Identity.Name);


                Master_Page_Username.Text = DBUtilities.getUserFullName(connection, HttpContext.Current.User.Identity.Name);
            }

            if (!Page.IsPostBack)
            {
                Session["VraiTimeout"] = 4 * 60;
                Session["Timeout"] = DateTime.Now;
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(HttpContext.Current.User.Identity.Name, false);
                authCookie.Expires = DateTime.Now.AddMinutes((double)Application["SessionTimeout"]);
                Response.Cookies.Add(authCookie);
            }
        }

        public void setTitre(String titre)
        {
            Master_Page_Titre.Text = titre;
        }

        protected void SessionTimeout_Tick(object sender, EventArgs e)
        {
            if (Session["VraiTimeout"] != null)
            {
                int test = (int)Session["VraiTimeout"] - 1;
                Session["VraiTimeout"] = (int)Session["VraiTimeout"] - 1;

                if ((int)Session["VraiTimeout"] <= 0)
                    signOut();
            }



            if (((DateTime)Session["Timeout"]).AddMinutes(Session.Timeout) < DateTime.Now &&
               !(Request.Url.ToString().Contains("Login.aspx") || Request.Url.ToString().Contains("Inscription.aspx")))
                signOut();

            if (HttpContext.Current.Request.Cookies[".ASPXFORMSAUTH"] == null &&
               !(Request.Url.ToString().Contains("Login.aspx") || Request.Url.ToString().Contains("Inscription.aspx")))
            {
                signOut();
            }

            if (Session["VraiTimeout"] != null && (int)Session["VraiTimeout"] < 120 && !Request.Url.ToString().Contains("Logout.aspx"))
                LBL_SessionTimeLeft.Text = "Temps restant avant l'expiration de la session: " + (int)Session["VraiTimeout"] / 60 + ":" + ((int)Session["VraiTimeout"] % 60 >= 10 ? "" : "0") + (int)Session["VraiTimeout"] % 60;
            else
                LBL_SessionTimeLeft.Text = "";
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

        public void signOut()
        {
            if (Session["isAuthenticated"] != null && (bool)Session["isAuthenticated"] == true)
            {
                LoginTable loginTable = new LoginTable((String)Application["MainDB"], Page);

                SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

                loginTable.InsertRecord(DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name), (DateTime)Session["SessionStartTime"], DateTime.Now, GetUserIP());

                Page.Application.Lock();
                if (((List<long>)Application["OnlineUsers"]).Contains(DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name)))
                {
                    ((List<long>)Application["OnlineUsers"]).Remove(DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name));
                }
                Page.Application.UnLock();
                Session["isAuthenticated"] = false;
                FormsAuthentication.SignOut();
            }
            Session.Abandon();
            Response.Redirect("Logout.aspx");
        }
    }
}