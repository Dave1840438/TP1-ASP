using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Labo_22
{
   public partial class ShowUsers : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         USERS users = new USERS((string)Application["MainDB"], this);
         users.SelectAll();
         users.MakeGridView(PN_GridView, "" /*pas de page d’édition pour l’instant */);
      }
   }
}