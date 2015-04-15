using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace TP1_ASP
{
   public class Global : System.Web.HttpApplication
   {

      public struct unUsagerEnLigne
      {
         public long id;
         public DateTime sessionStart;
         public DateTime sessionTimeOut;
         public string userIP;
      }


      protected void Application_Start(object sender, EventArgs e)
      {
         string DB_Path = Server.MapPath(@"~\App_Data\MainDB.mdf");
         // Toutes les Pages (WebForm) pourront accéder à la propriété Application["MainDB"]
         Application["MainDB"] = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='" + DB_Path + "';Integrated Security=True";
         Application["SessionTimeout"] = 1;

         Application["OnlineUsers"] = new List<long>();
         AddTask("DoStuff", 3);

         Application["OnlineUsersTwo"] = new Dictionary<string, unUsagerEnLigne>();
      }

      private static CacheItemRemovedCallback OnCacheRemove = null;

      private void AddTask(string name, int seconds)
      {
         OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);
         HttpRuntime.Cache.Insert(name, seconds, null,
             DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration,
             CacheItemPriority.NotRemovable, OnCacheRemove);
      }

      public void CacheItemRemoved(string name, object v, CacheItemRemovedReason r)
      {
         if (name == "DoStuff")
         {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            SqlCommand insert = new SqlCommand();
            insert.Connection = connection;

            Application.Lock();

            List<string> keysToDelete = new List<string>();

            foreach (KeyValuePair<string, unUsagerEnLigne> k in (Dictionary<string, unUsagerEnLigne>)Application["OnlineUsersTwo"])
            {
               if (k.Value.sessionTimeOut < DateTime.Now)
               {
                  insert.CommandText = "INSERT INTO LOGINS VALUES(" + k.Value.id + ", '" + k.Value.sessionStart + "', '" + DateTime.Now + "', '" + k.Value.userIP + "')";

                  connection.Open();

                  insert.ExecuteNonQuery();

                  connection.Close();

                  keysToDelete.Add(k.Key);

                  if (((List<long>)Application["OnlineUsers"]).Contains(k.Value.id))
                  {
                     ((List<long>)Application["OnlineUsers"]).Remove(k.Value.id);
                  }
               }
            }

            foreach (String key in keysToDelete)
            {
               ((Dictionary<string, unUsagerEnLigne>)Application["OnlineUsersTwo"]).Remove(key);
            }

            Application.UnLock();
         }
         // do stuff here if it matches our taskname, like WebRequest
         // re-add our task so it recurs
         AddTask(name, Convert.ToInt32(v));
      }

      protected void Session_Start(object sender, EventArgs e)
      {
         Session["bidon"] = 3;
         Session["Timeout"] = DateTime.Now;
         Session["tries"] = 0;

         
      }

      protected void Session_End(object sender, EventArgs e)
      {
         if (Session["isAuthenticated"] != null && (bool)Session["isAuthenticated"] == true)
         {
            Page pg = (Page)Session["Page"];

            LoginTable loginTable = new LoginTable((String)Application["MainDB"], pg);

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            loginTable.InsertRecord(DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name), (DateTime)Session["SessionStartTime"], DateTime.Now, GetUserIP());

            pg.Application.Lock();

            if (((List<long>)Application["OnlineUsers"]).Contains(DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name)))
            {
               ((List<long>)Application["OnlineUsers"]).Remove(DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name));
            }

            pg.Application.UnLock();

            Session["isAuthenticated"] = false;
            FormsAuthentication.SignOut();
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
   }
}