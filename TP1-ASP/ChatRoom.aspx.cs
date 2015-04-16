using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
   public partial class ChatRoom : System.Web.UI.Page
   {

      protected void Page_Load(object sender, EventArgs e)
      {
         var master = Master as Master_page;
         if (master != null)
            master.setTitre("Liste des conversations...");

         SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
         //LBL_Creator.Text = "";
         //if (Session["Thread_Name"] != null && (String)Session["Thread_Name"] != "")
         //{
         //    SqlDataAdapter sda = new SqlDataAdapter("SELECT ID AS 'En ligne', USERNAME AS 'Nom d''usager', Avatar FROM USERS WHERE ID IN (SELECT USERS.ID FROM USERS INNER JOIN THREADS_ACCESS ON USERS.ID = THREADS_ACCESS.USER_ID WHERE THREAD_ID = " + DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]) + ")" + " OR (SELECT ACCESS_TO_ALL FROM THREADS WHERE ID = " + DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]) + ") = 1", (String)Application["MainDB"]);
         //    DBUtilities.AppendToTable(TB_UserList, sda, true, (List<long>)Application["OnlineUsers"]);

         //    String sqlCommand = "SELECT USERS.AVATAR AS Avatar, USERS.FULLNAME, CONVERT(VARCHAR(30), THREADS_MESSAGES.DATE_OF_CREATION, 0), THREADS_MESSAGES.ID AS 'Delete button', THREADS_MESSAGES.ID AS 'Edit button', THREADS_MESSAGES.MESSAGE FROM THREADS_MESSAGES INNER JOIN USERS ON THREADS_MESSAGES.USER_ID = USERS.ID WHERE THREADS_MESSAGES.THREAD_ID = " + DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]) + " ORDER BY THREADS_MESSAGES.DATE_OF_CREATION DESC";
         //    SqlDataAdapter sdaMessages = new SqlDataAdapter(sqlCommand, (String)Application["MainDB"]);
         //    DBUtilities.AppendToTable(TB_Chat, sdaMessages);
         //    addDelegate(TB_Chat);

         //    connection.Open();
         //    SqlCommand sqlGetCreator = new SqlCommand("SELECT USERS.FULLNAME, CONVERT(VARCHAR(11), DATE_OF_CREATION, 111) FROM THREADS INNER JOIN USERS ON THREADS.CREATOR = USERS.ID WHERE THREADS.TITLE = '" + (String)Session["Thread_Name"] + "'");
         //    sqlGetCreator.Connection = connection;

         //    SqlDataReader creatorReader = sqlGetCreator.ExecuteReader();

         //    creatorReader.Read();

         //    LBL_Creator.Text = "Créateur: " + creatorReader.GetString(0) + " " + creatorReader.GetString(1);
         //    creatorReader.Close();
         //    connection.Close();
         //}

         updateChat();


         SqlCommand sqlFetchThreads = new SqlCommand();
         sqlFetchThreads.CommandText = "SELECT DISTINCT(THREADS.TITLE), DATE_OF_CREATION FROM THREADS LEFT JOIN THREADS_ACCESS ON THREADS.ID = THREADS_ACCESS.THREAD_ID WHERE + '" + HttpContext.Current.User.Identity.Name + "' = 'admin' OR ACCESS_TO_ALL = 1 OR USER_ID = " + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name) + " ORDER BY DATE_OF_CREATION DESC";
         sqlFetchThreads.Connection = connection;


         connection.Open();
         SqlDataReader threadsReader = sqlFetchThreads.ExecuteReader();

         while (threadsReader.Read())
         {
            Button btn = new Button();
            btn.Text = threadsReader.GetString(0);
            btn.CssClass = "Button";
            btn.ID = "BTN_Thread_" + btn.Text;
            btn.Click += BTN_ConvoName_Click;
            TableRow tr = new TableRow();
            tr.CssClass = "TableConvo";
            TableCell td = new TableCell();
            td.CssClass = "TableConvo";
            tr.Controls.Add(td);
            td.Controls.Add(btn);
            TB_ConvoList.Controls.Add(tr);
         }

         threadsReader.Close();
         connection.Close();

         if (!Page.IsPostBack)
            Session["ModifierLeMessage"] = false;
      }

      protected void updateChat()
      {
         SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
         LBL_Creator.Text = "";
         LBL_Titre_Convo.Text = (String)Session["Thread_Name"];
         TB_UserList.Controls.Clear();
         TB_Chat.Controls.Clear();
         if (Session["Thread_Name"] != null && (String)Session["Thread_Name"] != "")
         {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ID AS 'En ligne', USERNAME AS 'Nom d''usager', Avatar FROM USERS WHERE ID IN (SELECT USERS.ID FROM USERS INNER JOIN THREADS_ACCESS ON USERS.ID = THREADS_ACCESS.USER_ID WHERE THREAD_ID = " + DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]) + ")" + " OR (SELECT ACCESS_TO_ALL FROM THREADS WHERE ID = " + DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]) + ") = 1", (String)Application["MainDB"]);
            DBUtilities.AppendToTable(TB_UserList, sda, true, (List<long>)Application["OnlineUsers"]);

            String sqlCommand = "SELECT USERS.AVATAR AS Avatar, USERS.FULLNAME, CONVERT(VARCHAR(30), THREADS_MESSAGES.DATE_OF_CREATION, 0), THREADS_MESSAGES.ID AS 'Delete button', THREADS_MESSAGES.ID AS 'Edit button', THREADS_MESSAGES.MESSAGE FROM THREADS_MESSAGES INNER JOIN USERS ON THREADS_MESSAGES.USER_ID = USERS.ID WHERE THREADS_MESSAGES.THREAD_ID = " + DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]) + " ORDER BY THREADS_MESSAGES.DATE_OF_CREATION DESC";
            SqlDataAdapter sdaMessages = new SqlDataAdapter(sqlCommand, (String)Application["MainDB"]);
            DBUtilities.AppendToTable(TB_Chat, sdaMessages);
            addDelegate(TB_Chat);

            connection.Open();
            SqlCommand sqlGetCreator = new SqlCommand("SELECT USERS.FULLNAME, CONVERT(VARCHAR(11), DATE_OF_CREATION, 111) FROM THREADS INNER JOIN USERS ON THREADS.CREATOR = USERS.ID WHERE THREADS.TITLE = '" + (String)Session["Thread_Name"] + "'");
            sqlGetCreator.Connection = connection;

            SqlDataReader creatorReader = sqlGetCreator.ExecuteReader();

            creatorReader.Read();

            LBL_Creator.Text = "Créateur: " + creatorReader.GetString(0) + " " + creatorReader.GetString(1);
            creatorReader.Close();
            connection.Close();
         }
         UPN_Chat.Update();
         UPN_OnlineUsers.Update();
         UPN_Creator.Update();
      }

      protected void addDelegate(Control control)
      {
         List<long> messagesID = new List<long>();

         SqlConnection connection = new SqlConnection((String)Application["MainDB"]);


         SqlCommand sqlFetchMessagesID = new SqlCommand();
         sqlFetchMessagesID.CommandText = "SELECT ID FROM THREADS_MESSAGES WHERE USER_ID = " + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name);
         sqlFetchMessagesID.Connection = connection;


         connection.Open();
         SqlDataReader reader = sqlFetchMessagesID.ExecuteReader();

         while (reader.Read())
            messagesID.Add(reader.GetInt64(0));

         reader.Close();
         connection.Close();




         foreach (Control c in control.Controls)
         {
            if (c.ID != null)
            {
               if (c.ID.Contains("BTN_DeleteMessage"))
               {
                  long id = long.Parse(c.ID.Remove(0, 18));
                  if (messagesID.Contains(id) || HttpContext.Current.User.Identity.Name == "admin")
                  {
                     ((ImageButton)c).Click += BTN_Delete_Click;
                     ((ImageButton)c).OnClientClick = "return confirm(\"Êtes-vous sûr de vouloir supprimer le message?\")";
                     ((ImageButton)c).CssClass = "MicroAvatar";
                  }
                  else
                     control.Controls.Remove(c);
               }
               if (c.ID.Contains("BTN_EditMessage"))
               {
                  long id = long.Parse(c.ID.Remove(0, 16));
                  if (messagesID.Contains(id))
                  {
                     ((ImageButton)c).Click += BTN_Modify_Click;
                     ((ImageButton)c).CssClass = "MicroAvatar";
                  }

                  else
                     control.Controls.Remove(c);
               }
            }

            if (c.Controls.Count > 0)
               addDelegate(c);
         }

      }


      protected void BTN_Return_Click(object sender, EventArgs e)
      {
         Response.Redirect("Index.aspx");
      }

      protected void BTN_ConvoName_Click(object sender, EventArgs e)
      {
         Session["ModifierLeMessage"] = false;
         Session["Thread_Name"] = ((Button)sender).ID.Remove(0, 11);
         BTN_Send.Text = "Envoyer...";
         updateChat();
         UPN_BTN_Send.Update();
         //Response.Redirect(Request.Url.ToString());
      }

      public void BTN_Modify_Click(object sender, EventArgs e)
      {
         Session["messageID"] = ((ImageButton)sender).ID.Remove(0, 16);

         Session["ModifierLeMessage"] = true;
         BTN_Send.Text = "Modifier...";

         String id = ((ImageButton)sender).ID.Remove(0, 16);


         SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

         SqlCommand sqlEdit = new SqlCommand();
         sqlEdit.Connection = connection;
         sqlEdit.CommandText = "SELECT MESSAGE FROM THREADS_MESSAGES WHERE ID = " + id;
         connection.Open();

         SqlDataReader reader = sqlEdit.ExecuteReader();
         reader.Read();
         TBX_ChatInput.Text = reader.GetString(0);

         reader.Close();
         connection.Close();


         UPN_BTN_Send.Update();

         //String id = ((ImageButton)sender).ID.Remove(0, 16);


         //SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

         //SqlCommand sqlEdit = new SqlCommand();
         //sqlEdit.Connection = connection;
         //sqlEdit.CommandText = "UPDATE THREADS_MESSAGES SET MESSAGE = '" + TBX_ChatInput.Text + "' WHERE ID = " + id;
         //connection.Open();

         //sqlEdit.ExecuteNonQuery();

         //connection.Close();


      }

      public void BTN_Delete_Click(object sender, EventArgs e)
      {
         String id = ((ImageButton)sender).ID.Remove(0, 18);


         SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

         SqlCommand sqlEdit = new SqlCommand();
         sqlEdit.Connection = connection;
         sqlEdit.CommandText = "DELETE FROM THREADS_MESSAGES WHERE ID = " + id;
         connection.Open();

         sqlEdit.ExecuteNonQuery();

         connection.Close();
         updateChat();

      }

      protected void BTN_Send_Click(object sender, EventArgs e)
      {
         TBX_ChatInput.Text = TBX_ChatInput.Text.Replace("'", "''");

         if (Session["ModifierLeMessage"] == null || !(bool)Session["ModifierLeMessage"])
         {
            if (TBX_ChatInput.Text != "")
            {
               SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

               long threadID = DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]);
               long userID = DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name);

               connection.Open();

               SqlCommand sqlInsertMessage = new SqlCommand();
               sqlInsertMessage.CommandText = "INSERT INTO THREADS_MESSAGES VALUES(" + threadID + ", " + userID + ", '" + DateTime.Now.ToString() + "', '" + TBX_ChatInput.Text + "')";
               sqlInsertMessage.Connection = connection;

               sqlInsertMessage.ExecuteNonQuery();

               connection.Close();
            }
         }
         else
         {
            String id = (String)Session["messageID"];

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            SqlCommand sqlEdit = new SqlCommand();
            sqlEdit.Connection = connection;
            sqlEdit.CommandText = "UPDATE THREADS_MESSAGES SET MESSAGE = '" + TBX_ChatInput.Text + "' WHERE ID = " + id;
            connection.Open();

            sqlEdit.ExecuteNonQuery();

            connection.Close();

            Session["ModifierLeMessage"] = false;
            BTN_Send.Text = "Envoyer...";
            UPN_BTN_Send.Update();
         }
         TBX_ChatInput.Text = "";
         updateChat();
         TBX_ChatInput.Focus();
      }

      protected void RefreshChat_Tick(object sender, EventArgs e)
      {
      }
      protected void RefreshUsers_Tick(object sender, EventArgs e)
      {
      }


   }
}