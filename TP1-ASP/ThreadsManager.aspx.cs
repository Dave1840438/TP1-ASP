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

            SqlDataAdapter sda = new SqlDataAdapter("SELECT ID AS 'CheckBox', AVATAR AS 'Avatar', USERNAME FROM USERS WHERE ID != " + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name), (String)Application["MainDB"]);
            DBUtilities.AppendToTable(TB_AllExistingUsers, sda, false);

            if (!Page.IsPostBack)
            {

                SqlCommand sqlcmdFetchThreads = new SqlCommand("SELECT TITLE AS 'Titre' FROM THREADS WHERE CREATOR = " + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name) + " OR 'admin' = '" + HttpContext.Current.User.Identity.Name + "'");
                sqlcmdFetchThreads.Connection = connection;

                connection.Open();

                SqlDataReader reader = sqlcmdFetchThreads.ExecuteReader();
                LB_Thread_List.Items.Clear();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.Text = item.Value = reader.GetString(0);
                    LB_Thread_List.Items.Add(item);
                }

                reader.Close();
                connection.Close();
            }

            if (!Page.IsPostBack)
                Session["isModifying"] = false;
        }


        protected void BTN_New_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            int accessToAll = CBOX_AllUsers.Checked ? 1 : 0;

            SqlCommand sqlcmdInsertThread = new SqlCommand("INSERT INTO THREADS VALUES(" + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name) + ", '" + TBX_TitreDiscussion.Text + "', '" + DateTime.Now.ToString() + "', " + accessToAll + ")");
            sqlcmdInsertThread.Connection = connection;
            connection.Open();

            sqlcmdInsertThread.ExecuteNonQuery();

            connection.Close();
            ModifyRightsToThread();

            Response.Redirect(Request.Url.ToString());
        }

        protected void BTN_Modify_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);


            int accessToAll = CBOX_AllUsers.Checked ? 1 : 0;

            SqlCommand sqlcmdUpdateThread = new SqlCommand("UPDATE THREADS SET TITLE = '" + TBX_TitreDiscussion.Text + "', ACCESS_TO_ALL = " + accessToAll + " WHERE TITLE = '" + LB_Thread_List.SelectedValue + "'");
            sqlcmdUpdateThread.Connection = connection;
            connection.Open();

            sqlcmdUpdateThread.ExecuteNonQuery();

            connection.Close();
            ModifyRightsToThread();

            Response.Redirect(Request.Url.ToString());
        }
        protected void BTN_Delete_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);


            TBX_TitreDiscussion.Text = LB_Thread_List.SelectedValue;
            ModifyRightsToThread(true);
            SqlCommand sqlcmdDeleteThread = new SqlCommand("DELETE FROM THREADS WHERE TITLE = '" + LB_Thread_List.SelectedValue + "'");
            sqlcmdDeleteThread.Connection = connection;

            connection.Open();

            sqlcmdDeleteThread.ExecuteNonQuery();
            connection.Close();

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
            UPN_UsersCheckboxes.Update();
        }

        protected void ModifyRightsToThread(bool deleting = false)
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            long currentUsersId = DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name);

            connection.Open();

            SqlCommand getThreadID = new SqlCommand("SELECT ID FROM THREADS WHERE TITLE = '" + TBX_TitreDiscussion.Text + "'");
            getThreadID.Connection = connection;
            SqlDataReader threadIDReader = getThreadID.ExecuteReader();
            threadIDReader.Read();
            long threadID = threadIDReader.GetInt64(0);
            threadIDReader.Close();

            SqlCommand deleteAllMessagesFromThread = new SqlCommand("DELETE FROM THREADS_MESSAGES WHERE THREAD_ID = " + threadID.ToString());
            deleteAllMessagesFromThread.Connection = connection;
            deleteAllMessagesFromThread.ExecuteNonQuery();


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
                    usersToAdd.Add(currentUsersId);
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
            args.IsValid = TBX_TitreDiscussion.Text != "";
            UPN_Titre_Discussion.Update();
        }

        protected bool DiscussionExiste()
        {
            bool result;

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);


            SqlCommand sqlcmdDeleteThread = new SqlCommand("SELECT TITLE FROM THREADS WHERE TITLE = '" + TBX_TitreDiscussion.Text + "'");
            sqlcmdDeleteThread.Connection = connection;
            connection.Open();

            SqlDataReader reader = sqlcmdDeleteThread.ExecuteReader();

            result = reader.Read();

            connection.Close();


            return result;
        }

        protected void BTN_Clear_Click(object sender, EventArgs e)
        {
            BTN_Modify_Or_Create.Text = "Créer...";
            TBX_TitreDiscussion.Text = "";

            Session["isModifying"] = false;

            CBOX_AllUsers.Checked = false;

            foreach (TableRow tr in TB_AllExistingUsers.Rows)
            {
                foreach (TableCell tc in tr.Cells)
                {
                    if (tc.Controls.Count > 0)
                    {
                        var chkBox = tc.Controls[0] as CheckBox;
                        if (chkBox != null)
                        {
                            chkBox.Checked = false;
                        }
                    }
                }
            }
        }

        protected void BTN_Modify_Or_Create_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Session["isModifying"] == null || (bool)Session["isModifying"] == false && !DiscussionExiste())
                {
                    SqlConnection connection = new SqlConnection((String)Application["MainDB"]);


                    int accessToAll = CBOX_AllUsers.Checked ? 1 : 0;

                    SqlCommand sqlcmdInsertThread = new SqlCommand("INSERT INTO THREADS VALUES(" + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name) + ", '" + TBX_TitreDiscussion.Text + "', '" + DateTime.Now.ToString() + "', " + accessToAll + ")");
                    sqlcmdInsertThread.Connection = connection;
                    connection.Open();

                    sqlcmdInsertThread.ExecuteNonQuery();

                    connection.Close();
                    ModifyRightsToThread();

                }
                else if (!DiscussionExiste() || TBX_TitreDiscussion.Text == LB_Thread_List.SelectedValue)
                {
                    SqlConnection connection = new SqlConnection((String)Application["MainDB"]);


                    int accessToAll = CBOX_AllUsers.Checked ? 1 : 0;

                    SqlCommand sqlcmdUpdateThread = new SqlCommand("UPDATE THREADS SET TITLE = '" + TBX_TitreDiscussion.Text + "', ACCESS_TO_ALL = " + accessToAll + " WHERE TITLE = '" + LB_Thread_List.SelectedValue + "'");
                    sqlcmdUpdateThread.Connection = connection;
                    connection.Open();

                    sqlcmdUpdateThread.ExecuteNonQuery();

                    connection.Close();
                    ModifyRightsToThread();

                }
                Response.Redirect("ChatRoom.aspx");
            }

        }

        protected void CV_AuMoinsUnInvite_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = CBOX_AllUsers.Checked || AuMoinsUnInviteEstCoché();
            UPN_UsersCheckboxes.Update();
        }

        protected bool AuMoinsUnInviteEstCoché()
        {
            bool result = false;
            foreach (TableRow tr in TB_AllExistingUsers.Rows)
            {
                foreach (TableCell tc in tr.Cells)
                {
                    if (tc.Controls.Count > 0)
                    {
                        var chkBox = tc.Controls[0] as CheckBox;
                        if (chkBox != null)
                        {
                            result |= chkBox.Checked;
                        }
                    }
                }
            }

            return result;
        }

        protected void LB_Thread_List_SelectedIndexChanged(object sender, EventArgs e)
        {

            Session["isModifying"] = true;
            BTN_Modify_Or_Create.Text = "Modifier...";
            TBX_TitreDiscussion.Text = LB_Thread_List.SelectedValue;
            UPN_BTN_Send_Or_Create.Update();
            UPN_Titre_Discussion.Update();

            CBOX_AllUsers.Checked = false;

            foreach (TableRow tr in TB_AllExistingUsers.Rows)
            {
                foreach (TableCell tc in tr.Cells)
                {
                    if (tc.Controls.Count > 0)
                    {
                        var chkBox = tc.Controls[0] as CheckBox;
                        if (chkBox != null)
                        {
                            chkBox.Checked = false;
                        }
                    }
                }
            }

            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            int count = LB_Thread_List.Items.Count;
            String titre = LB_Thread_List.Items[LB_Thread_List.SelectedIndex].Text;

            String threadID = DBUtilities.getThreadID(connection, LB_Thread_List.SelectedValue).ToString();
            List<String> usersID = new List<String>();

            SqlCommand fetchIDs = new SqlCommand();
            fetchIDs.Connection = connection;
            fetchIDs.CommandText = "select user_id from THREADS_ACCESS where THREAD_ID = " + threadID;

            connection.Open();

            SqlDataReader reader = fetchIDs.ExecuteReader();


            while (reader.Read())
                usersID.Add(reader.GetInt64(0).ToString());

            reader.Close();
            connection.Close();

            if (usersID.Count == 0)
            {
                CBOX_AllUsers.Checked = true;
            }
            else
            {
                CBOX_AllUsers.Checked = false;
                foreach (TableRow tr in TB_AllExistingUsers.Rows)
                {
                    foreach (TableCell tc in tr.Cells)
                    {
                        if (tc.Controls.Count > 0)
                        {
                            var chkBox = tc.Controls[0] as CheckBox;
                            if (chkBox != null)
                            {
                                String userID = chkBox.ID.Remove(0, 7);
                                chkBox.Checked = usersID.Contains(userID);
                            }
                        }
                    }
                }
            }
            UPN_UsersCheckboxes.Update();
            UPN_Titre_Discussion.Update();
            TBX_TitreDiscussion.Focus();
        }
    }
}