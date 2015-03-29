﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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

        public static void createTable(ContentPlaceHolder CPH_content, SqlDataAdapter sda)
        {
            DataSet customersSet = new DataSet();
            DataTable customersTable = null;
            sda.Fill(customersSet);
            customersTable = customersSet.Tables[0];
            

            TableRow tableRow = null;

            Table tableElem = new Table();
            tableElem.ID = "DynamicTable";
            CPH_content.Controls.Add(tableElem);

            TableRow tableHeader = new TableRow();
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
                        if (col.ColumnName != "Email")
                            tableCell.Text = dbCell.ToString();
                        else
                        {

                            HyperLink link = new HyperLink();
                            link.Text = dbCell.ToString();
                            link.NavigateUrl = "mailto:" + dbCell.ToString();

                            tableCell.Controls.Add(link);
                        }
                    }
                    tableRow.Controls.Add(tableCell);
                }
            }
        }
    }
}