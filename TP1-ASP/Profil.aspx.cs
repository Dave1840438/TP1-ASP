using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TP1_ASP
{
    public partial class Profil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Master_page;
            if (master != null)
                master.setTitre("Gestion de votre profil...");
            if (!Page.IsPostBack)
                Initialize();
        }

        protected void Initialize()
        {
            SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
            


            SqlCommand sqlcmdUserCheck = new SqlCommand("SELECT * FROM USERS WHERE USERNAME = '" + HttpContext.Current.User.Identity.Name + "'");
            sqlcmdUserCheck.Connection = connection;
            connection.Open();

            SqlDataReader userReader = sqlcmdUserCheck.ExecuteReader();

            userReader.Read();

            TBX_NomComplet.Text = userReader.GetString(1);
            TBX_Username.Text = userReader.GetString(2);
            TBX_Password.Text = TBX_ConfirmPassword.Text = userReader.GetString(3);
            TBX_Email.Text = TBX_ConfirmEmail.Text = userReader.GetString(4);
            IMG_Avatar.ImageUrl = @"~\Avatars\" + userReader.GetString(5) + ".png";

            userReader.Close();
            connection.Close();

            
        }

        protected void BTT_Modifier_Click(object sender, EventArgs e)
        {
            long userID;
            if (Page.IsValid)
            {
                SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
                


                // Création d'une nouvelle instance de Users (reliée à la table MainDB.Users)
                UserTable users = new UserTable((String)Application["MainDB"], this);

                String Avatar_Path = "";
                String avatar_ID = "";
                if (FU_Avatar.FileName != "")
                {
                    avatar_ID = Guid.NewGuid().ToString();
                    Avatar_Path = Server.MapPath(@"~\Avatars\") + avatar_ID + ".png";
                    FU_Avatar.SaveAs(Avatar_Path);
                }
                else
                    avatar_ID = DBUtilities.getAvatarID(connection, HttpContext.Current.User.Identity.Name);


                userID = DBUtilities.getUserID(connection, HttpContext.Current.User.Identity.Name);

                

                users.UpdateRecord(userID, TBX_NomComplet.Text, TBX_Username.Text, TBX_Password.Text,
                                   TBX_Email.Text, avatar_ID);

                FormsAuthentication.RedirectFromLoginPage(TBX_Username.Text, false);

                Response.Redirect("Index.aspx");
            }
        }

        protected void BTT_Annuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        bool HasEmptyField(Control container)
        {
            bool result = false;

            foreach (var control in MainPanel.Controls)
            {
                if (control is TextBox)
                    result |= (((TextBox)control).Text == String.Empty);
                if (control is FileUpload)
                    result |= (((FileUpload)control).FileName == "");
            }

            if (!result)
            {
                SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
                

                result |= DBUtilities.checkIfUsernameExists(connection, TBX_Username.Text);

                
            }


            if (result)
                MainPanel.BackColor = System.Drawing.Color.Red;
            else
                MainPanel.BackColor = System.Drawing.Color.Gray;

            return result;
        }

        protected void BTN_Submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Session["message"] = "(Inscription réussie - complétez maintenant votre profil...)";
                Response.Redirect("Profil.aspx");
            }
        }

        #region Validators

        protected void CV_NameIsEmpty(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (TBX_NomComplet.Text.Length > 0);
        }

        protected void CV_UsernameIsEmpty(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (TBX_Username.Text.Length > 0);
        }
        protected void CV_PasswordIsEmpty(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (TBX_Password.Text.Length > 0);
        }
        protected void CV_PasswordsMatch(object source, ServerValidateEventArgs args)
        {
            args.IsValid = TBX_Password.Text == TBX_ConfirmPassword.Text;
        }
        protected void CV_EmailIsEmpty(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (TBX_Email.Text.Length > 0);
        }
        protected void CV_EmailsMatch(object source, ServerValidateEventArgs args)
        {
            args.IsValid = TBX_Email.Text == TBX_ConfirmEmail.Text;
        }

        protected void CV_FullNameIsNotEmpty_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = TBX_NomComplet.Text != "";
        }

        #endregion Validators
    }
}