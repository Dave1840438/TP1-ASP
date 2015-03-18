using System;
using System.Collections.Generic;
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

      }

      public void setTitre(String titre)
      {
         Master_Page_Titre.Text = titre;
      }
   }
}