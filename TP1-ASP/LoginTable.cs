using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TP1_ASP
{
   public class LoginTable : SqlExpressUtilities.SqlExpressWrapper
   {
        public long ID { get; set; }
        public String UserID { get; set; }
        public String LoginDate { get; set; }
        public String LogoutDate { get; set; }
        public String IPAdress { get; set; }
       
        public LoginTable(String connexionString, System.Web.UI.Page Page)
            : base(connexionString, Page)
        {
            SQLTableName = "LOGINS";
        }
        ////////////////////////////////////////////////////////////////////////////////
        // Nouvelles fonctionnalité plus "user friendly"
        public override void GetValues()
        {
            ID = long.Parse(this["ID"]);
            UserID = this["FullName"];
            LoginDate = this["UserName"];
            LogoutDate = this["Password"];
            IPAdress = this["Email"];
        }

        public override void InitColumnsVisibility()
        {
            base.InitColumnsVisibility();
        }

        public override void InitCellsContentDelegate()
        {
            base.InitCellsContentDelegate();
        }

        public override void InitColumnsSortEnable()
        {
            base.InitColumnsSortEnable();
            SetColumnSortEnable("ID", false);
        }

        public override void InitColumnsTitles()
        {
            base.InitColumnsTitles();
            SetColumnTitle("ID", "ID");
            SetColumnTitle("UserID", "ID de l'utilisateur");
            SetColumnTitle("LoginDate", "Date de connexion");
            SetColumnTitle("LogoutDate", "Date de déconnexion");
            SetColumnTitle("IPAdress", "Adresse IP");
        }
   
        public override void Insert()
        {
            InsertRecord(UserID, LoginDate, LogoutDate, IPAdress);
        }
        public override void Update()
        {
            UpdateRecord(ID, UserID, LoginDate, LogoutDate, IPAdress);
        }
   }
}