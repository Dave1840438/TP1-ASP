﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session != null && !((bool)Session["isConnected"]))
                Response.Redirect("Login.aspx");

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
            Session["isConnected"] = false;
            Response.Redirect("Login.aspx");
        }
    }
}