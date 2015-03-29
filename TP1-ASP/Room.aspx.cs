using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Web.UI.HtmlControls;

namespace TP1_ASP
{
    public partial class Room : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Master_page;
            if (master != null)
                master.setTitre("Usagers en ligne...");
            CreateTable();
        }


        protected void CreateTable()
        {
            UserTable users = new UserTable((String)Application["MaindDB"], this);
            users.SelectAll();
            users.MakeGridView(PN_OnlineUsers, null);
        }
    }
}