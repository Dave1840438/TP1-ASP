using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo_Master_UpdatePanel
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void TimerTime_Tick(object sender, EventArgs e)
        {

            LBL_Time.Text = DateTime.Now.ToLongTimeString();
        }
    }
}