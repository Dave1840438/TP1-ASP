using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SessionInfo
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static String DurationToString(DateTime start, DateTime end)
        {
            TimeSpan duration = end - start;
            double hours = Math.Truncate(duration.TotalHours);
            double minutes = Math.Truncate(duration.TotalMinutes);
            double seconds = Math.Truncate(Math.Round(100 * duration.TotalSeconds) / 100);
            return (hours   < 10? "0" + hours.ToString()  : hours.ToString())   + ":" +
                   (minutes < 10? "0" + minutes.ToString(): minutes.ToString()) + ":" +
                   (seconds < 10? "0" + seconds.ToString(): seconds.ToString());
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            DateTime sessionStart = (DateTime)Session["StartTime"];
            DateTime sessionEnd = DateTime.Now;
            // Abandonner la session
            Session.Abandon();
            
            Response.Write("Durée de la session: " + DurationToString(sessionStart, sessionEnd));
        }
    }
}