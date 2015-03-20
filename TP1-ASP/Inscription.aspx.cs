using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
    public partial class Inscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Master_page;
            if (master != null)
            {
                // master.setTitre("fhklasjdhfasdfhoasdfhasdfhuioasfuiofuoasdhfo;usdhfiluasdhfui");
            }
        }

        protected void BTT_Inscription_Click(object sender, EventArgs e)
        {
            if (!HasEmptyField(MainPanel))
            {
                // Création d'une nouvelle instance de Users (reliée à la table MainDB.Users)
                UserTable users = new UserTable((String)Application["MaindDB"], this);

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
                                   Avatar_Path);
            }
        }

        bool HasEmptyField(Control container)
        {
            bool result = false;

            foreach (var control in MainPanel.Controls)
                if (control is TextBox)
                    result |= (((TextBox)control).Text == String.Empty);

            if (result)
                MainPanel.BackColor = System.Drawing.Color.Red;
            else
                MainPanel.BackColor = System.Drawing.Color.Gray;

            return result;
        }
    }
}