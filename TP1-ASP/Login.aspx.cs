using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mime.MediaTypeNames;

namespace TP1_ASP
{
    public partial class Login : System.Web.UI.Page
    {

        
           
         

        protected void Page_Load(object sender, EventArgs e)
        {
            // instancier l'objet de collection
            SqlConnection connection = new SqlConnection((String)Application["MaindDB"]);
            // bâtir l'objet de requête
            SqlCommand sqlcmd = new SqlCommand("//LA COMMANDE");
            // affecter l'objet de connection à l'objet de requête
            sqlcmd.Connection = connection;
            // bloquer l'objet Page.Application afin d'empêcher d'autres sessions concurentes
            // d'avoir accès à la base de données concernée par la requête en cours
            Page.Application.Lock();
            // ouvrir la connection avec la bd
            connection.Open();

            connection.Close();

            Page.Application.UnLock();
        }
    }
}