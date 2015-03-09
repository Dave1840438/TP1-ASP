using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LABO_1
{
    public partial class CreateUser2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BTN_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListerPersonnes.aspx");
        }

        protected void AddPersonneFormData()
        {
            String avatar_ID = "";
            if (FU_Avatar.FileName != "")
            {
                String Avatar_Path = "";

                avatar_ID = Guid.NewGuid().ToString();
                Avatar_Path = Server.MapPath(@"~\Avatars\") + avatar_ID + ".png";
                FU_Avatar.SaveAs(Avatar_Path);
            }

            PersonnesTable personne = new PersonnesTable((String)Application["MainDB"], this);
            personne.Prenom = TB_Prenom.Text;
            personne.Nom =TB_Nom.Text;
            personne.Telephone = TB_Telephone.Text;
            personne.CodePostal = TB_CodePostal.Text;
            personne.Naissance = DateTime.Parse(TB_Naissance.Text);
            personne.Sexe = RBL_Sexe.SelectedIndex;
            personne.EtatCivil = RBL_EtatCivil.SelectedIndex;
            personne.Avatar = avatar_ID;
            personne.Insert();
        }

        protected void BTN_Add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AddPersonneFormData();
                Response.Redirect("ListerPersonnes.aspx");
            }
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