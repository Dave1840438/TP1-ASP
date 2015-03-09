using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LABO_1
{
    public partial class ModifierPersonne : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) LoadForm();
            String action = Request["action"];
            if (action == "cancel")
                Response.Redirect("ListerPersonnes.aspx");
            if (action == "delete")
                DeleteCurrent();
            if (action == "edit")
                UpdateCurrent();
        }

        private string BuildSetValueScript(String input, String value)
        {
            return "SetValue('" + input + "', '" + value + "'); ";
        }

        private string BuildSetRadioBUttonGroupValueScript(String RBG_Name, String value)
        {
            return "SetRadioButtonGroupValue('" + RBG_Name + "', '" + value + "'); ";
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
        private void LoadForm()
        {
            if (Session["Selected_ID"] != null)
            {
                // Création d'une nouvelle instance de PersonnesTable (reliée à la table MainDB.Personnes)
                PersonnesTable personne = new PersonnesTable((String)Application["MainDB"], this);
                // Conserver dans l'objet session afin qu'au prochain postback on puisse y référer
                Session["Personne"] = personne;
                if (personne.SelectByID((String)Session["Selected_ID"]))
                {
                    LB_ID.Text = (String)Session["Selected_ID"];
                    InsertSetValueScript(PN_Script, personne);
                    if (personne.Avatar != "")
                        IMG_Avatar.ImageUrl = "Avatars/" + personne.Avatar + ".png"; // +"?" + DateTime.Now.Millisecond.ToString();
                    else
                        IMG_Avatar.ImageUrl = "Images/ADD.png"; // +"?" + DateTime.Now.Millisecond.ToString();
                }
            }
        }

        private void DeleteCurrent()
        {
            DeleteImage(((PersonnesTable)Session["Personne"]).Avatar);
            ((PersonnesTable)Session["Personne"]).DeleteRecordByID((String)Session["Selected_ID"]);
            Session["Personne"] = null;
            Session["Selected_ID"] = null;
            Response.Redirect("ListerPersonnes.aspx");
        }
        private void DeleteImage(String ID)
        {
            File.Delete(Server.MapPath(@"~\Avatars\") + ID + ".png");
        }

        private void UpdateCurrent()
        {
            if ((Session["Selected_ID"] != null) && (Session["Personne"] != null))
            {
                PersonnesTable personne = (PersonnesTable)Session["Personne"];
                personne.Prenom = Request["Prenom"];
                personne.Nom = Request["Nom"];
                personne.Telephone = Request["Telephone"];
                personne.CodePostal = Request["CodePostal"];
                personne.Naissance = DateTime.Parse(Request["Naissance"]);
                personne.Sexe = int.Parse(Request["Sexe"]);
                personne.EtatCivil = int.Parse(Request["EtatCivil"]);

                String Avatar_Path = "";
                String avatar_ID = "";
                if (FU_Avatar.FileName != "")
                {
                    DeleteImage(personne.Avatar);
                    avatar_ID = Guid.NewGuid().ToString();
                    Avatar_Path = Server.MapPath(@"~\Avatars\") + avatar_ID + ".png";
                    FU_Avatar.SaveAs(Avatar_Path);
                    personne.Avatar = avatar_ID;
                }

                personne.Update();
                Session["Personne"] = null;
                Session["Selected_ID"] = null;
            }
            Response.Redirect("ListerPersonnes.aspx");
        }
    }
}