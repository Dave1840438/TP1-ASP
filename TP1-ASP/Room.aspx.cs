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
        static Table laTable;

        protected void Page_Load(object sender, EventArgs e)
        {

            var master = Master as Master_page;
            if (master != null)
                master.setTitre("Usagers en ligne...");

            Page.Application.Lock();

            SqlDataAdapter sda = new SqlDataAdapter("SELECT ID AS 'En ligne', USERNAME AS 'Nom d''usager', FULLNAME AS 'Nom au complet', Email, Avatar FROM USERS", (String)Application["MainDB"]);
            DBUtilities.AppendToTable(MaFuckingTableDeCalissDeCriss, sda, (List<long>)Application["OnlineUsers"]);

            Page.Application.UnLock();
        }


        protected void RefreshUsers_Tick(object sender, EventArgs e)
        {
            RemoveChilds(MaFuckingTableDeCalissDeCriss);
        }

        private void RemoveChilds(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Controls.Count > 0)
                    RemoveChilds(c);
                control.Controls.Remove(c);
            }
        }
    }
}

