﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           var master = Master as Master_page;
           if (master != null)
              master.setTitre("Logout...");
        }

        protected void RefreshSession_Tick(object sender, EventArgs e)
        {
            Session["VraiTimeout"] = 10;
        }
    }
}