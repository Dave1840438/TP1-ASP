using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
   public partial class Master_page : System.Web.UI.MasterPage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
          if (Session != null && Session["isConnected"] != null && (bool)Session["isConnected"])
          {
              SqlConnection connection = new SqlConnection((String)Application["MaindDB"]);
              Page.Application.Lock();

              Master_Page_Avatar.ImageUrl = DBUtilities.getAvatar(connection, (String)Session["Username"]);

              Page.Application.UnLock(); 
          }
      }

      public void setTitre(String titre)
      {
         Master_Page_Titre.Text = titre;
      }
   }
}