using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LABO_1
{
    public partial class ModifierPersonne2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                LoadForm();
        }
        private void InsertValuesInForm(PersonnesTable personne)
        {
            TB_Prenom.Text = personne.Prenom;
            TB_Nom.Text = personne.Nom;
            TB_Telephone.Text = personne.Telephone;
            TB_CodePostal.Text = personne.CodePostal;
            TB_Naissance.Text = personne.Naissance.ToShortDateString();
            RBL_Sexe.SelectedIndex = personne.Sexe;
            RBL_EtatCivil.SelectedIndex = personne.EtatCivil;
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
                    InsertValuesInForm(personne);
                    if (personne.Avatar != "")
                        IMG_Avatar.ImageUrl = "Avatars/" + personne.Avatar + ".png"; // +"?" + DateTime.Now.Millisecond.ToString();
                    else
                        IMG_Avatar.ImageUrl = "Images/Anonymous.png"; // +"?" + DateTime.Now.Millisecond.ToString();
                }
            }
        }
        private void DeleteCurrent()
        {
            DeleteImage(((PersonnesTable)Session["Personne"]).Avatar);
            ((PersonnesTable)Session["Personne"]).DeleteRecordByID((String)Session["Selected_ID"]);
            Session["Personne"] = null;
            Session["Selected_ID"] = null;
        }
        private void DeleteImage(String ID)
        {
            File.Delete(Server.MapPath(@"~\Avatars\") + ID + ".png");
        }

        private void GetFormValues(PersonnesTable personne)
        {
            personne.Prenom = TB_Prenom.Text;
            personne.Nom = TB_Nom.Text;
            personne.Telephone = TB_Telephone.Text;
            personne.CodePostal = TB_CodePostal.Text;
            
            personne.Naissance = DateTime.Parse(TB_Naissance.Text);
            personne.Sexe = RBL_Sexe.SelectedIndex;
            personne.EtatCivil = RBL_EtatCivil.SelectedIndex;
        }

        private void UpdateCurrent()
        {
            if ((Session["Selected_ID"] != null) && (Session["Personne"] != null))
            {
                PersonnesTable personne = (PersonnesTable)Session["Personne"];
                GetFormValues(personne);

                if (FU_Avatar.FileName != "")
                {
                    String Avatar_Path = "";
                    String avatar_ID = "";
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
        
        protected void BTN_Update_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UpdateCurrent();
                Response.Redirect("ListerPersonnes.aspx");
            }
        }
        protected void BTN_Delete_Click(object sender, EventArgs e)
        {
            DeleteCurrent();
            Response.Redirect("ListerPersonnes.aspx");
        }

        protected void BTN_Cancel_Click(object sender, EventArgs e)
        {
            Session["Personne"] = null;
            Session["Selected_ID"] = null;
            Response.Redirect("ListerPersonnes.aspx");
        }
        protected void CV_TB_Prenom_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_Prenom.Text == "")
            {
                TB_Prenom.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_Prenom.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }
        protected void CV_TB_Nom_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TB_Nom.Text == "")
            {
                TB_Nom.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_Nom.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }
        protected void CV_TB_Telephone_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((TB_Telephone.Text == "") || (TB_Telephone.Text.Length < TB_Telephone.Attributes["alt"].Length))
            {
                TB_Telephone.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_Telephone.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }
        protected void CV_TB_CodePostal_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((TB_CodePostal.Text == "") || (TB_CodePostal.Text.Length < TB_CodePostal.Attributes["alt"].Length))
            {
                TB_CodePostal.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_CodePostal.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }
        protected void CV_TB_Naissance_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (TB_Naissance.Text == "")
            {
                TB_Naissance.BackColor = System.Drawing.Color.FromArgb(0, 255, 200, 200);
                args.IsValid = false;
            }
            else
            {
                TB_Naissance.BackColor = System.Drawing.Color.White;
                args.IsValid = true;
            }
        }
        protected void CV_RBL_Sexe_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (RBL_Sexe.SelectedIndex == -1)
            {
                RBL_Sexe.CssClass = "radio hightLite";
                args.IsValid = false;
            }
            else
            {
                RBL_Sexe.CssClass = "radio";
                args.IsValid = true;
            }
        }
        protected void CV_RBL_EtatCivil_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (RBL_EtatCivil.SelectedIndex == -1)
            {
                RBL_EtatCivil.CssClass = "radio hightLite";
                args.IsValid = false;
            }
            else
            {
                RBL_EtatCivil.CssClass = "radio";
                args.IsValid = true;
            }
        }
     }
}