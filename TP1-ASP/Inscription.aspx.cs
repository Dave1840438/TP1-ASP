using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
   public partial class Inscription : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         var master = Master as Master_page;
         if (master != null)
         {
            // master.setTitre("fhklasjdhfasdfhoasdfhasdfhuioasfuiofuoasdhfo;usdhfiluasdhfui");
         }
      }

      protected void BTT_Inscription_Click(object sender, EventArgs e)
      {
         Verify(MainPanel);
      }

      void Verify(Control container)
      {
         foreach(var control in MainPanel.Controls)
         {
            if(control is TextBox)
            {
               if(((TextBox)control).Text == String.Empty)
               {
                  MainPanel.BackColor = System.Drawing.Color.Red;
               }
               else
               {
                  MainPanel.BackColor = System.Drawing.Color.Gray;
               }
            }
         }
      }
   }
}