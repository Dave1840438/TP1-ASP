using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP___Lab_1
{
   public partial class EditUsers : System.Web.UI.Page
   {
      PersonnesTable user;
      protected void Page_Load(object sender, EventArgs e)
      {
         user = new PersonnesTable((String)Application["MaindDB"], this);

         user.SelectByID((String)Session["Selected_ID"]);
         InsertSetValueScript(PN_Script, user);
         if (user.Avatar != "")
            IMG_Avatar.ImageUrl = "Avatars/" + user.Avatar + ".png"; // +"?" + DateTime.Now.Millisecond.ToString();
         else
            IMG_Avatar.ImageUrl = "Images/ADD.png"; // +"?" + DateTime.Now.Millisecond.ToString();

         //if (!Page.IsPostBack)
         //  LoadForm();
         String action = Request["action"];
         if (action == "cancel")
            Response.Redirect("ListUsers.aspx");
         if (action == "confirm")
         {
            //AddPersonne();
            Response.Redirect("ListUsers.aspx");
         }
         if (action == "delete")
         {
            DeleteCurrent();
            Response.Redirect("ListUsers.aspx");
         }
         if (action == "edit")
         {
            UpdateCurrent();
            Response.Redirect("ListUsers.aspx");
         }


      }

      private void DeleteCurrent()
      {
         user.DeleteRecordByID((String)Session["Selected_ID"]);
      }

      private void UpdateCurrent()
      {
         String Avatar_Path = "";
         if (FU_Avatar.FileName != "")
         {
            Avatar_Path = Server.MapPath(@"~\Avatars\") + user.Avatar + ".png";
            FU_Avatar.SaveAs(Avatar_Path);
         }

         user.UpdateRecord((String)Session["Selected_ID"],
                           Request["Prenom"],
                           Request["Nom"],
                           Request["Telephone"],
                           Request["CodePostal"],
                           user.Avatar,
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