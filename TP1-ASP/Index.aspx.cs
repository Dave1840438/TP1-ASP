using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TP1_ASP
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Master_page;
            if (master != null)
                master.setTitre("Accueil...");
        }

        protected void BTT_ManageProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profil.aspx");
        }

        protected void BTT_OnlineUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("Room.aspx");
        }

        protected void BTT_Log_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginsJournal.aspx");
        }

        protected void BTT_Disconnect_Click(object sender, EventArgs e)
        {
           var master = Master as Master_page;
           if (master != null)
              master.signOut(true);

           Session["tries"] = 0;
        }

        protected void BTT_ChatRoom_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChatRoom.aspx");
        }

        protected void BTT_ManageThreads_Click(object sender, EventArgs e)
        {
            Response.Redirect("ThreadsManager.aspx");

        }
    }
}