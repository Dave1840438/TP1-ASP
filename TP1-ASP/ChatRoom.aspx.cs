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
        static String Conversation_Name = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            var master = Master as Master_page;
            if (master != null)
                master.setTitre("Liste des conversations...");



            Page.Application.Lock();

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ID AS 'En ligne', USERNAME AS 'Nom d''usager', Avatar FROM USERS WHERE ID IN (SELECT USERS.ID FROM USERS INNER JOIN THREADS_ACCESS ON USERS.ID = THREADS_ACCESS.USER_ID WHERE THREAD_ID = " + DBUtilities.getThreadID(connection, Conversation_Name), (String)Application["MainDB"]);
            DBUtilities.AppendToTable(TB_UserList, sda, true, (List<long>)Application["OnlineUsers"]);

            SqlCommand sqlFetchThreads = new SqlCommand();
            sqlFetchThreads.CommandText = "SELECT THREADS.TITLE FROM THREADS INNER JOIN THREADS_ACCESS ON THREADS.ID = THREADS_ACCESS.THREAD_ID WHERE USER_ID = " + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name);
            sqlFetchThreads.Connection = connection;


            connection.Open();
            SqlDataReader threadsReader = sqlFetchThreads.ExecuteReader();

            while (threadsReader.Read())
            {
                Button btn = new Button();
                btn.ID = "BTN_Thread_" + threadsReader.GetString(0);
                btn.Click += BTN_ConvoName_Click;
                PN_ConvoList.Controls.Add(btn);
            }

            threadsReader.Close();
            connection.Close();


            Page.Application.UnLock();




        }

        protected void BTN_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void BTN_ConvoName_Click(object sender, EventArgs e)
        {
            Conversation_Name = ((Button)sender).ID;
        }
    }
}