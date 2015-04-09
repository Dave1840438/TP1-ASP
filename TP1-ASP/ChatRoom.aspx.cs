using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
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

            Page.Application.Lock();

            if (Session["Thread_Name"] != null && (String)Session["Thread_Name"] != "")
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT ID AS 'En ligne', USERNAME AS 'Nom d''usager', Avatar FROM USERS WHERE ID IN (SELECT USERS.ID FROM USERS INNER JOIN THREADS_ACCESS ON USERS.ID = THREADS_ACCESS.USER_ID WHERE THREAD_ID = " + DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]) + ")", (String)Application["MainDB"]);
                DBUtilities.AppendToTable(TB_UserList, sda, true, (List<long>)Application["OnlineUsers"]);

                String sqlCommand = "SELECT USERS.AVATAR AS Avatar, USERS.FULLNAME, CONVERT(VARCHAR(30), THREADS_MESSAGES.DATE_OF_CREATION, 0), THREADS_MESSAGES.ID AS 'Delete button', THREADS_MESSAGES.ID AS 'Edit button', THREADS_MESSAGES.MESSAGE FROM THREADS_MESSAGES INNER JOIN USERS ON THREADS_MESSAGES.USER_ID = USERS.ID WHERE THREADS_MESSAGES.THREAD_ID = " + DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]);
                SqlDataAdapter sdaMessages = new SqlDataAdapter(sqlCommand, (String)Application["MainDB"]);
                DBUtilities.AppendToTable(TB_Chat, sdaMessages);
                addDelegate(TB_Chat);
            }


            SqlCommand sqlFetchThreads = new SqlCommand();
            sqlFetchThreads.CommandText = "SELECT THREADS.TITLE FROM THREADS INNER JOIN THREADS_ACCESS ON THREADS.ID = THREADS_ACCESS.THREAD_ID WHERE USER_ID = 0 OR UERS_ID = " + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name);
            sqlFetchThreads.Connection = connection;


            connection.Open();
            SqlDataReader threadsReader = sqlFetchThreads.ExecuteReader();

            while (threadsReader.Read())
            {
                Button btn = new Button();
                btn.Text = threadsReader.GetString(0);
                btn.ID = "BTN_Thread_" + btn.Text;
                btn.Click += BTN_ConvoName_Click;
                PN_ConvoList.Controls.Add(btn);
            }

            threadsReader.Close();
            connection.Close();






            Page.Application.UnLock();




        }

        protected void addDelegate(Control control)
        {
            List<long> messagesID = new List<long>();
            Page.Application.Lock();
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

            Page.Application.UnLock();


            foreach (Control c in control.Controls)
            {





                if (c.ID != null)
                {
                    if (c.ID.Contains("BTN_DeleteMessage"))
                    {
                        long id = long.Parse(c.ID.Remove(0, 18));
                        if (messagesID.Contains(id))
                            ((ImageButton)c).Click += BTN_Delete_Click;
                        else
                            control.Controls.Remove(c);

                    }
                    if (c.ID.Contains("BTN_EditMessage"))
                    {
                        long id = long.Parse(c.ID.Remove(0, 16));
                        if (messagesID.Contains(id))
                            ((ImageButton)c).Click += BTN_Modify_Click;
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
            Session["Thread_Name"] = ((Button)sender).ID.Remove(0, 11);
        }

        public void BTN_Modify_Click(object sender, EventArgs e)
        {
            String id = ((ImageButton)sender).ID.Remove(0, 16);

            Page.Application.Lock();
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            SqlCommand sqlEdit = new SqlCommand();
            sqlEdit.Connection = connection;
            sqlEdit.CommandText = "UPDATE THREADS_MESSAGES SET MESSAGE = '" + TBX_ChatInput.Text + "' WHERE ID = " + id;
            connection.Open();

            sqlEdit.ExecuteNonQuery();

            connection.Close();

            Page.Application.UnLock();
        }

        public void BTN_Delete_Click(object sender, EventArgs e)
        {
            String id = ((ImageButton)sender).ID.Remove(0, 18);

            Page.Application.Lock();
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            SqlCommand sqlEdit = new SqlCommand();
            sqlEdit.Connection = connection;
            sqlEdit.CommandText = "DELETE FROM THREADS_MESSAGES WHERE ID = " + id;
            connection.Open();

            sqlEdit.ExecuteNonQuery();

            connection.Close();

            Page.Application.UnLock();
        }

        protected void BTN_Send_Click(object sender, EventArgs e)
        {
            if (TBX_ChatInput.Text != "")
            {
                Page.Application.Lock();
                SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

                long threadID = DBUtilities.getThreadID(connection, (String)Session["Thread_Name"]);
                long userID = DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name);

                connection.Open();

                SqlCommand sqlInsertMessage = new SqlCommand();
                sqlInsertMessage.CommandText = "INSERT INTO THREADS_MESSAGES VALUES(" + threadID + ", " + userID + ", '" + DateTime.Now.ToString() + "', '" + TBX_ChatInput.Text + "')";
                sqlInsertMessage.Connection = connection;

                sqlInsertMessage.ExecuteNonQuery();

                connection.Close();
                Page.Application.UnLock();
            }

            TBX_ChatInput.Text = "";
        }

        protected void RefreshChat_Tick(object sender, EventArgs e)
        {
        }
        protected void RefreshUsers_Tick(object sender, EventArgs e)
        {
        }


    }
}