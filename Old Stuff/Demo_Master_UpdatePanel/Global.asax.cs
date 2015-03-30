using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Demo_Master_UpdatePanel
{
   public class Global : System.Web.HttpApplication
   {
      protected void Application_Start(object sender, EventArgs e)
      {
      }

      void Session_Start(object sender, EventArgs e)
      {

      }

      void Session_End(object sender, EventArgs e)
      {
         FormsAuthentication.SignOut();
      }

   }
}