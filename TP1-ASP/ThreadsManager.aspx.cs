using System;
using System.Collections.Generic;
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
        }

        protected void BTN_New_Click(object sender, EventArgs e)
        {
        }

        protected void BTN_Modify_Click(object sender, EventArgs e)
        {
        }
        protected void BTN_Delete_Click(object sender, EventArgs e)
        {
        }
        protected void BTT_Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}