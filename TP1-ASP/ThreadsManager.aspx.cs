using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
    public partial class ThreadsManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Master_page;
            if (master != null)
                master.setTitre("Gestion de mes discussions...");

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            Page.Application.Lock();

            SqlCommand sqlcmdFetchThreads = new SqlCommand("SELECT TITLE FROM THREADS WHERE CREATOR = " + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name));
            sqlcmdFetchThreads.Connection = connection;
            connection.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmdFetchThreads.CommandText, connection);
            da.Fill(ds);


            DGV_Discussions.DataSource = ds;
            DGV_Discussions.DataBind();

            connection.Close();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT ID AS 'CheckBox', AVATAR AS 'Avatar', USERNAME FROM USERS", (String)Application["MainDB"]);
            DBUtilities.AppendToTable(TB_AllExistingUsers, sda, false);

            Page.Application.UnLock();

        }


        protected void BTN_New_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            Page.Application.Lock();

            int accessToAll = CBOX_AllUsers.Checked ? 1 : 0;

            SqlCommand sqlcmdInsertThread = new SqlCommand("INSERT INTO THREADS VALUES(" + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name) + ", '" + TBX_TitreDiscussion.Text + "', '" + DateTime.Now.ToString() + "', " + accessToAll +")");
            sqlcmdInsertThread.Connection = connection;
            connection.Open();

            sqlcmdInsertThread.ExecuteNonQuery();

            connection.Close();
            ModifyRightsToThread();
            Page.Application.UnLock();
            Response.Redirect(Request.Url.ToString());
        }

        protected void BTN_Modify_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            Page.Application.Lock();

            int accessToAll = CBOX_AllUsers.Checked ? 1 : 0;

            SqlCommand sqlcmdUpdateThread = new SqlCommand("UPDATE THREADS SET TITLE = '" + TBX_TitreDiscussion.Text + "', ACCESS_TO_ALL = " + accessToAll + " WHERE TITLE = '" + (DGV_Discussions.SelectedItem.Cells[1]).Text + "'");
            sqlcmdUpdateThread.Connection = connection;
            connection.Open();

            sqlcmdUpdateThread.ExecuteNonQuery();

            connection.Close();
            ModifyRightsToThread();
            Page.Application.UnLock();
            Response.Redirect(Request.Url.ToString());
        }
        protected void BTN_Delete_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            Page.Application.Lock();

            SqlCommand sqlcmdDeleteThread = new SqlCommand("DELETE FROM THREADS WHERE TITLE = '" + (DGV_Discussions.SelectedItem.Cells[1]).Text + "'");
            sqlcmdDeleteThread.Connection = connection;

            connection.Open();

            ModifyRightsToThread(true);
            sqlcmdDeleteThread.ExecuteNonQuery();
            connection.Close();
            Page.Application.UnLock();
            Response.Redirect(Request.Url.ToString());
        }
        protected void BTT_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void CBOX_AllUsers_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TableRow tr in TB_AllExistingUsers.Rows)
            {
                foreach (TableCell tc in tr.Cells)
                {
                    if (tc.Controls.Count > 0)
                    {
                        var chkBox = tc.Controls[0] as CheckBox;
                        if (chkBox != null)
                            chkBox.Enabled = !CBOX_AllUsers.Checked;
                    }
                }
            }
        }

        protected void ModifyRightsToThread(bool deleting = false)
        {

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            connection.Open();

            SqlCommand getThreadID = new SqlCommand("SELECT ID FROM THREADS WHERE TITLE = '" + TBX_TitreDiscussion.Text + "'");
            getThreadID.Connection = connection;
            SqlDataReader threadIDReader = getThreadID.ExecuteReader();
            threadIDReader.Read();
            long threadID = threadIDReader.GetInt64(0);
            threadIDReader.Close();

            SqlCommand deleteAllPermissionsToThread = new SqlCommand("DELETE FROM THREADS_ACCESS WHERE THREAD_ID = " + threadID.ToString());
            deleteAllPermissionsToThread.Connection = connection;
            deleteAllPermissionsToThread.ExecuteNonQuery();

            if (!deleting && !CBOX_AllUsers.Checked)
            {
                List<long> usersToAdd = new List<long>();

                if (CBOX_AllUsers.Checked)
                {
                    usersToAdd.Add(0);
                }
                else
                {
                    foreach (TableRow tr in TB_AllExistingUsers.Rows)
                    {
                        foreach (TableCell tc in tr.Cells)
                        {
                            if (tc.Controls.Count > 0)
                            {
                                var chkBox = tc.Controls[0] as CheckBox;
                                if (chkBox != null)
                                {
                                    if (chkBox.Checked)
                                    {
                                        String userID = chkBox.ID.Remove(0, 7);
                                        usersToAdd.Add(long.Parse(userID));
                                    }
                                }
                            }
                        }
                    }
                }

                SqlCommand sqlInsert = new SqlCommand();
                sqlInsert.Connection = connection;
                if (!deleting)
                {
                    foreach (long id in usersToAdd)
                    {
                        sqlInsert.CommandText = "INSERT INTO THREADS_ACCESS VALUES(" + threadID.ToString() + ", " + id.ToString() + ")";
                        sqlInsert.ExecuteNonQuery();
                    }
                }
            }
            connection.Close();
        }

        protected void CVal_TitreDiscussion_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = TBX_TitreDiscussion.Text.Length > 0;
        }

        protected void CVal_DiscussionExiste_Exists(object source, ServerValidateEventArgs args)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            Page.Application.Lock();

            SqlCommand sqlcmdDeleteThread = new SqlCommand("SELECT TITLE FROM THREADS WHERE TITLE = '" + TBX_TitreDiscussion.Text + "'");
            sqlcmdDeleteThread.Connection = connection;
            connection.Open();

            SqlDataReader reader = sqlcmdDeleteThread.ExecuteReader();

            //args.IsValid =  reader.Read();
            args.IsValid = true;

            connection.Close();
            Page.Application.UnLock();
        }

    }
}