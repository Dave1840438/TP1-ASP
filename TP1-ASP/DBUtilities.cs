using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
    public static class DBUtilities
    {

        public static bool checkIfUsernameExists(SqlConnection connection, String username)
        {
            bool result;

            SqlCommand sqlcmdUserCheck = new SqlCommand("SELECT USERNAME FROM USERS WHERE USERNAME = '" + username + "'");
            sqlcmdUserCheck.Connection = connection;
            connection.Open();

            SqlDataReader userReader = sqlcmdUserCheck.ExecuteReader();

            if (userReader.Read())
                result = userReader.GetString(0) != "";
            else
                result = false;

            userReader.Close();
            connection.Close();

            return result;
        }

        public static String getAvatar(SqlConnection connection, String username)
        {
            String result;

            SqlCommand sqlcmdUserCheck = new SqlCommand("SELECT AVATAR FROM USERS WHERE USERNAME = '" + username + "'");
            sqlcmdUserCheck.Connection = connection;
            connection.Open();

            SqlDataReader userReader = sqlcmdUserCheck.ExecuteReader();

            if (userReader.Read())
            {
                result = @"~\Avatars\";
                result += userReader.GetString(0);
                result += ".png";
            }
            else
                result = "/Images/ADD.png";


            userReader.Close();
            connection.Close();


            return result;
        }

        public static String getAvatarID(SqlConnection connection, String username)
        {
            String result;

            SqlCommand sqlcmdUserCheck = new SqlCommand("SELECT AVATAR FROM USERS WHERE USERNAME = '" + username + "'");
            sqlcmdUserCheck.Connection = connection;
            connection.Open();

            SqlDataReader userReader = sqlcmdUserCheck.ExecuteReader();

            userReader.Read();

            result = userReader.GetString(0);

            userReader.Close();
            connection.Close();


            return result;
        }

        public static long getUserID(SqlConnection connection, String username)
        {
            long result;

            SqlCommand sqlcmdUserCheck = new SqlCommand("SELECT ID FROM USERS WHERE USERNAME = '" + username + "'");
            sqlcmdUserCheck.Connection = connection;
            connection.Open();

            SqlDataReader userReader = sqlcmdUserCheck.ExecuteReader();

            userReader.Read();
            result = userReader.GetInt64(0);

            userReader.Close();
            connection.Close();


            return result;
        }

        public static long getThreadID(SqlConnection connection, String title)
        {
            long result;

            SqlCommand sqlcmdUserCheck = new SqlCommand("SELECT ID FROM THREADS WHERE TITLE = '" + title + "'");
            sqlcmdUserCheck.Connection = connection;
            connection.Open();

            SqlDataReader userReader = sqlcmdUserCheck.ExecuteReader();

            userReader.Read();
            result = userReader.GetInt64(0);

            userReader.Close();
            connection.Close();


            return result;
        }


        public static Table createTable(Control content, SqlDataAdapter sda, List<long> OnlineUsers = null)
        {
            DataSet customersSet = new DataSet();
            DataTable customersTable = null;
            sda.Fill(customersSet);
            customersTable = customersSet.Tables[0];


            TableRow tableRow = null;

            Table tableElem = new Table();
            tableElem.ID = "DynamicTable";

            if (content as UpdatePanel == null)
                content.Controls.Add(tableElem);
            else
                content.TemplateControl.Controls.Add(tableElem);

            TableRow tableHeader = new TableRow();
            tableHeader.ID = "DynamicTableHeader";
            tableHeader.TableSection = TableRowSection.TableHeader;
            tableElem.Controls.Add(tableHeader);

            foreach (DataColumn col in customersTable.Columns)
            {
                TableCell cell = new TableCell();
                cell.Text = col.ColumnName;
                tableHeader.Controls.Add(cell);
            }

            // Create table rows.

            foreach (DataRow dr in customersTable.Rows)
            {


                tableRow = new TableRow();
                tableRow.TableSection = TableRowSection.TableBody;
                tableElem.Controls.Add(tableRow);
                foreach (DataColumn col in customersTable.Columns)
                {
                    Object dbCell = dr[col];
                    TableCell tableCell = new TableCell();
                    if (!(dbCell is DBNull))
                    {
                        if (col.ColumnName == "Email")
                        {
                            HyperLink link = new HyperLink();
                            link.Text = dbCell.ToString();
                            link.NavigateUrl = "mailto:" + dbCell.ToString();

                            tableCell.Controls.Add(link);
                        }
                        else if (col.ColumnName == "Avatar")
                        {
                            Image imgAvatar = new Image();
                            imgAvatar.CssClass = "MicroAvatar";
                            imgAvatar.ImageUrl = @"~\Avatars\" + dbCell.ToString() + ".png";

                            tableCell.Controls.Add(imgAvatar);
                        }
                        else if (col.ColumnName == "En ligne")
                        {
                            Image imgOnline = new Image();
                            imgOnline.CssClass = "MicroAvatar";
                            if (OnlineUsers.Contains(long.Parse(dbCell.ToString())))
                                imgOnline.ImageUrl = "/Images/OnLine.png";
                            else
                                imgOnline.ImageUrl = "/Images/OffLine.png";
                            tableCell.Controls.Add(imgOnline);
                        }
                        else
                            tableCell.Text = dbCell.ToString();
                    }
                    tableRow.Controls.Add(tableCell);
                }
            }
            return tableElem;
        }

        public static void AppendToTable(Table container, SqlDataAdapter sda, bool wantHeader = false, List<long> OnlineUsers = null)
        {
            DataSet customersSet = new DataSet();
            DataTable customersTable = null;
            sda.Fill(customersSet);
            customersTable = customersSet.Tables[0];


            TableRow tableRow = null;

            if (wantHeader)
            {
                TableRow tableHeader = new TableRow();
                tableHeader.ID = "DynamicTableHeader";
                tableHeader.TableSection = TableRowSection.TableHeader;
                container.Controls.Add(tableHeader);

                foreach (DataColumn col in customersTable.Columns)
                {
                    TableCell cell = new TableCell();
                    cell.Text = col.ColumnName;
                    tableHeader.Controls.Add(cell);
                }
            }


            // Create table rows.

            foreach (DataRow dr in customersTable.Rows)
            {


                tableRow = new TableRow();
                tableRow.TableSection = TableRowSection.TableBody;
                container.Controls.Add(tableRow);
                foreach (DataColumn col in customersTable.Columns)
                {
                    Object dbCell = dr[col];
                    TableCell tableCell = new TableCell();
                    if (!(dbCell is DBNull))
                    {
                        if (col.ColumnName == "Email")
                        {
                            HyperLink link = new HyperLink();
                            link.Text = dbCell.ToString();
                            link.NavigateUrl = "mailto:" + dbCell.ToString();

                            tableCell.Controls.Add(link);
                        }
                        else if (col.ColumnName == "Avatar")
                        {
                            Image imgAvatar = new Image();
                            imgAvatar.CssClass = "MicroAvatar";
                            imgAvatar.ImageUrl = @"~\Avatars\" + dbCell.ToString() + ".png";

                            tableCell.Controls.Add(imgAvatar);
                        }
                        else if (col.ColumnName == "CheckBox")
                        {
                            CheckBox ChkBox = new CheckBox();
                            ChkBox.ID = "CHKBOX_" + dbCell.ToString();

                            tableCell.Controls.Add(ChkBox);
                        }
                        else if (col.ColumnName == "En ligne")
                        {
                            Image imgOnline = new Image();
                            imgOnline.CssClass = "MicroAvatar";
                            if (OnlineUsers.Contains(long.Parse(dbCell.ToString())))
                                imgOnline.ImageUrl = "/Images/OnLine.png";
                            else
                                imgOnline.ImageUrl = "/Images/OffLine.png";
                            tableCell.Controls.Add(imgOnline);
                        }
                        else
                            tableCell.Text = dbCell.ToString();
                    }
                    tableRow.Controls.Add(tableCell);
                }
            }
        }
    }
}