using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{

    public partial class Inscription : System.Web.UI.Page
    {
        Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Master_page;
            if (master != null)
            {
                master.setTitre("Inscription...");
            }

            if (!Page.IsPostBack)
            {
                Session["captcha"] = BuildCaptcha();
            }
        }

        protected void BTT_Inscription_Click(object sender, EventArgs e)
        {
            string img = IMG_Avatar.ImageUrl;
            string lel = FU_Avatar.FileName;

            if (Page.IsValid)
            {
                UserTable users = new UserTable((String)Application["MainDB"], this);

                String Avatar_Path = "";
                String avatar_ID = "";
                if (FU_Avatar.FileName != "")
                {
                    avatar_ID = Guid.NewGuid().ToString();
                    Avatar_Path = Server.MapPath(@"~\Avatars\") + avatar_ID + ".png";
                    FU_Avatar.SaveAs(Avatar_Path);
                }

                users.InsertRecord(TBX_NomComplet.Text,
                                   TBX_Username.Text,
                                   TBX_Password.Text,
                                   TBX_Email.Text,
                                   avatar_ID);

                Response.Redirect("Login.aspx");
            }
        }

        protected void BTT_Annuler_Click(object sender, EventArgs e)
        {
            ////if (!Page.IsPostBack)
            ////  LoadForm();
            //String action = Request["action"];
            //if (action == "cancel")
            //    Response.Redirect("ListUsers.aspx");
            //if (action == "confirm")
            //{
            //    AddPersonne();
            //    Response.Redirect("ListUsers.aspx");
            //}
            //// if (action == "delete")
            ////    DeleteCurrent();
            //// if (action == "edit")
            ////UpdateCurrent();

            Response.Redirect("Login.aspx");
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
                Page.Application.Lock();

                result |= DBUtilities.checkIfUsernameExists(connection, TBX_Username.Text);

                Page.Application.UnLock(); 
            }
               

            if (result)
                MainPanel.BackColor = System.Drawing.Color.Red;
            else
                MainPanel.BackColor = System.Drawing.Color.Gray;

            return result;
        }

        char RandomChar()
        {
            // les lettres comportant des ambiguïtées ne sont pas dans la liste
            // exmple: 0 et O ont été retirés
            string chars = "abcdefghkmnpqrstuvwvxyzABCDEFGHKMNPQRSTUVWXYZ23456789";
            return chars[random.Next(0, chars.Length)];
        }

        Color RandomColor(int min, int max)
        {
            return Color.FromArgb(255, random.Next(min, max), random.Next(min, max), random.Next(min, max));
        }

        string Captcha()
        {
            string captcha = "";

            for (int i = 0; i < 5; i++)
                captcha += RandomChar();
            return captcha;//.ToLower();
        }

        string BuildCaptcha()
        {
            int width = 200;
            int height = 70;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics DC = Graphics.FromImage(bitmap);
            SolidBrush brush = new SolidBrush(RandomColor(0, 127));
            SolidBrush pen = new SolidBrush(RandomColor(172, 255));
            DC.FillRectangle(brush, 0, 0, 200, 100);
            Font font = new Font("Snap ITC", 32, FontStyle.Regular);
            PointF location = new PointF(5f, 5f);
            DC.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            string captcha = Captcha();
            DC.DrawString(captcha, font, pen, location);

            // noise generation
            for (int i = 0; i < 5000; i++)
            {
                bitmap.SetPixel(random.Next(0, width), random.Next(0, height), RandomColor(127, 255));
            }
            bitmap.Save(Server.MapPath("Captcha.png"), ImageFormat.Png);
            return captcha;
        }

        protected void RegenarateCaptcha_Click(object sender, ImageClickEventArgs e)
        {
            String buffer = BuildCaptcha();
            Session["captcha"] = buffer;
            // + DateTime.Now.ToString() pour forcer le fureteur recharger le fichier
            IMGCaptcha.ImageUrl = "~/Captcha.png?ID=" + DateTime.Now.ToString();
            PN_Captcha.Update();
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

        protected void UserNameExists_ServerValidate(object source, ServerValidateEventArgs args)
        {
           SqlConnection connection = new SqlConnection((String)Application["MainDB"]);
           Page.Application.Lock();

           args.IsValid = !DBUtilities.checkIfUsernameExists(connection, TBX_Username.Text);

           Page.Application.UnLock(); 
        }

        protected void CV_Captcha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String lel =  (string)Session["captcha"];
            args.IsValid = (TBX_Captcha.Text == (string)Session["captcha"]);
        }

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
        protected void CV_AvatarIsChosen(object source, ServerValidateEventArgs args)
        {
            args.IsValid = FU_Avatar.FileName != "";
        }

        protected void CV_FullNameIsNotEmpty_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = TBX_Username.Text != "";
        }

        #endregion Validators
    }
}