using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Master_page;
            if (master != null)
                master.setTitre("Journal des visites...");

           SqlConnection connection = new SqlConnection((String)Application["MainDB"]);

            Page.Application.Lock();

            String sqlCommand = "SELECT USERID AS ID, CONVERT(VARCHAR(30), LOGINDATE, 0) AS Début, CONVERT(VARCHAR(11), (LOGOUTDATE - LOGINDATE), 8) AS Durée, IPADDRESS AS IP,"
               +" USERNAME AS 'Nom d''usager', FULLNAME AS 'Nom au complet', EMAIL as Email, AVATAR AS Avatar "
               + "FROM LOGINS INNER JOIN USERS ON LOGINS.USERID = USERS.ID WHERE USERS.ID = " + DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name);

            SqlDataAdapter sda = new SqlDataAdapter(sqlCommand, (String)Application["MainDB"]);
            ContentPlaceHolder CPH_content = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            DBUtilities.AppendToTable(TB_Log, sda, true);

            Page.Application.UnLock();

        }

        protected void BTN_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}