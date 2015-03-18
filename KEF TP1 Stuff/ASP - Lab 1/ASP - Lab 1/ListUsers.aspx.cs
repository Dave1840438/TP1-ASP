using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP___Lab_1
{
    public partial class ListUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // il est important de comprendre ici que même dans le cas d'un PostBack
            // il faut recréer la GridView afin que le gestionnaire du clic soit
            // sur les IDs (LinkButton) soit sur les icônes (ImageButton) de tri
            // puissent êtres invoqués
            ListeUsers();
        }

        public void ListeUsers()
        {
            // Création d'une nouvelle instance de Users (reliée à la table MainDB.Users)
            PersonnesTable users = new PersonnesTable((String)Application["MaindDB"], this);
            users.SelectAll();
            users.MakeGridView(PN_ListUsers, "EditUser.aspx");
        }

        protected void BTN_GotoInscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inscription.aspx");
        }
    }
}