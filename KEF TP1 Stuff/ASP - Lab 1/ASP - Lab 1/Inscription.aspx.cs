using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP___Lab_1
{
    public partial class Inscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //if (!Page.IsPostBack)
              //  LoadForm();
               String action = Request["action"];
            if (action == "cancel")
                Response.Redirect("ListUsers.aspx");
            if (action == "confirm")
            {
                AddPersonne();
                Response.Redirect("ListUsers.aspx");
            }
           // if (action == "delete")
            //    DeleteCurrent();
           // if (action == "edit")
                //UpdateCurrent();
        }

        public void AddPersonne()
        {
            // Création d'une nouvelle instance de Users (reliée à la table MainDB.Users)
            PersonnesTable users = new PersonnesTable((String)Application["MaindDB"], this);

            String Avatar_Path = "";
            String avatar_ID = "";
            if (FU_Avatar.FileName != "")
            {
               avatar_ID = Guid.NewGuid().ToString();
               Avatar_Path = Server.MapPath(@"~\Avatars\") + avatar_ID + ".png";
               FU_Avatar.SaveAs(Avatar_Path);
            }

            users.InsertRecord(Request["Prenom"],
                               Request["Nom"],
                               Request["Telephone"],
                               Request["CodePostal"],
                               avatar_ID,
                               Request["Naissance"],
                               Request["Sexe"],
                               Request["EtatCivil"]);
        }

        private void InsertSetValueScript(Panel panel, PersonnesTable personne)
        {
            String script = "<script>";

            script += BuildSetValueScript("Prenom", personne.Prenom);
            script += BuildSetValueScript("Nom", personne.Nom);
            script += BuildSetValueScript("Telephone", personne.Telephone);
            script += BuildSetValueScript("CodePostal", personne.CodePostal);
            script += BuildSetValueScript("Naissance", personne.Naissance.ToShortDateString());
            script += BuildSetRadioBUttonGroupValueScript("Sexe", personne.Sexe.ToString());
            script += BuildSetRadioBUttonGroupValueScript("Etatcivil", personne.EtatCivil.ToString());
            script += "</script>";
            panel.Controls.Add(new LiteralControl(script));
        }

        private string BuildSetValueScript(String input, String value)
        {
            return "SetValue('" + input + "', '" + value + "'); ";
        }

        private string BuildSetRadioBUttonGroupValueScript(String RBG_Name, String value)
        {
            return "SetRadioButtonGroupValue('" + RBG_Name + "', '" + value + "'); ";
        }


    }
}