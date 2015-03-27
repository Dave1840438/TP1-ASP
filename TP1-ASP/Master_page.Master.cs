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
         if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated && !(Request.Url.ToString().Contains("Login.aspx") || Request.Url.ToString().Contains("Inscription.aspx")))
          {
              SqlConnection connection = new SqlConnection((String)Application["MaindDB"]);
              Page.Application.Lock();
              
              Master_Page_Avatar.ImageUrl = DBUtilities.getAvatar(connection, HttpContext.Current.User.Identity.Name);

              Page.Application.UnLock();
              Master_Page_Username.Text = HttpContext.Current.User.Identity.Name;
          }
      }

      public void setTitre(String titre)
      {
         Master_Page_Titre.Text = titre;
      }
   }
}