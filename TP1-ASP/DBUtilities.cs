using System;
using System.Collections.Generic;
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
    }
}