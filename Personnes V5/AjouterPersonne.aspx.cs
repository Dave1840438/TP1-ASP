using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LABO_1
{
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String action = Request["action"];
            if (action == "add")
            {
                if ((Request["Prenom"] != null) && (Request["Prenom"] != ""))
                {
                    String Avatar_Path = "";
                    String avatar_ID = "";
                    if (FU_Avatar.FileName != "")
                    {
                        avatar_ID = Guid.NewGuid().ToString();
                        Avatar_Path = Server.MapPath(@"~\Avatars\") + avatar_ID + ".png";
                        FU_Avatar.SaveAs(Avatar_Path);
                    }

                    PersonnesTable personnes = new PersonnesTable((String)Application["MainDB"], this);
                    personnes.Prenom = Request["Prenom"];
                    personnes.Nom = Request["Nom"];
                    personnes.Telephone = Request["Telephone"];
                    personnes.CodePostal = Request["CodePostal"];
                    personnes.Avatar = avatar_ID;
                    personnes.Naissance = DateTime.Parse(Request["Naissance"]);
                    personnes.Sexe = int.Parse(Request["Sexe"]);
                    personnes.EtatCivil = int.Parse(Request["EtatCivil"]);
                    personnes.Insert();
                    Response.Redirect("ListerPersonnes.aspx");
                }
            }
            if (action=="cancel")
                Response.Redirect("ListerPersonnes.aspx");
        }
    }
}