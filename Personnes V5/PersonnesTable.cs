using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace LABO_1
{
    public class PersonnesTable : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }
        public String Prenom { get; set; }
        public String Nom { get; set; }
        public String Telephone { get; set; }
        public String CodePostal { get; set; }
        public String Avatar { get; set; }
        public DateTime Naissance { get; set; }
        public int Sexe { get; set; }
        public int EtatCivil { get; set; }
        public PersonnesTable(String connexionString, System.Web.UI.Page Page)
            : base(connexionString, Page)
        {
            SQLTableName = "PERSONNES";
        }
        ////////////////////////////////////////////////////////////////////////////////
        // Nouvelles fonctionnalité plus "user friendly"
        public override void GetValues()
        {
            ID = long.Parse(this["ID"]);
            Prenom = this["PRENOM"];
            Nom = this["NOM"];
            Telephone = this["TELEPHONE"];
            CodePostal = this["CODE_POSTAL"];
            Avatar = this["Avatar"];
            Naissance = DateTime.Parse(this["NAISSANCE"]);
            Sexe = int.Parse(this["SEXE"]);
            EtatCivil = int.Parse(this["ETAT_CIVIL"]);
        }

        public override void InitColumnsVisibility()
        {
            base.InitColumnsVisibility();
            SetColumnVisibility("Avatar", false);
        }

        public override void InitCellsContentDelegate()
        {
            base.InitCellsContentDelegate();
            SetCellContentDelegate("SEXE", ContentDelegateSexe);
            SetCellContentDelegate("ETAT_CIVIL", ContentDelegateEtatCivil);
        }

        public override void InitColumnsSortEnable()
        {
            base.InitColumnsSortEnable();
            SetColumnSortEnable("ID", false);
            SetColumnSortEnable("CODE_POSTAL", false);
            SetColumnSortEnable("TELEPHONE", false);
        }

        public override void InitColumnsTitles()
        {
            base.InitColumnsTitles();
            SetColumnTitle("ID", "Id");
            SetColumnTitle("PRENOM", "Prénom");
            SetColumnTitle("NOM", "Nom");
            SetColumnTitle("TELEPHONE", "Téléphone");
            SetColumnTitle("CODE_POSTAL", "Code postal");
            SetColumnTitle("NAISSANCE", "Date de naissance");
            SetColumnTitle("SEXE", "Sexe");
            SetColumnTitle("ETAT_CIVIL", "État civil");
        }
        //
        ///////////////////////////////////////////////////////////////////////////////
        System.Web.UI.WebControls.WebControl ContentDelegateSexe()
        {
            Label lbl = new Label();
            if (Sexe == 0)
                lbl.Text = "Masculin";
            else
                lbl.Text = "Féminin"; 
            return lbl;
        }
        System.Web.UI.WebControls.WebControl ContentDelegateEtatCivil()
        {
            Label lbl = new Label();
            switch (EtatCivil)
            {
                case 0: lbl.Text = "Célibataire"; break;
                case 1: lbl.Text = "Marié(e)"; break;
                case 2: lbl.Text = "conjoint(e) de fait"; break;
                case 3: lbl.Text = "Séparé(e)"; break;
                case 4: lbl.Text = "Veuf/Veuve"; break;
            } return lbl;
        }
        public override void Insert()
        {
            InsertRecord(Prenom, Nom, Telephone, CodePostal, Avatar, Naissance, Sexe, EtatCivil);
        }
        public override void Update()
        {
            UpdateRecord(ID, Prenom, Nom, Telephone, CodePostal, Avatar, Naissance, Sexe, EtatCivil);
        }
    }

}