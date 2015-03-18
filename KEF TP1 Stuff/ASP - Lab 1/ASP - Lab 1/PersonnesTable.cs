using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class PersonnesTable : SqlExpressUtilities.SqlExpressWrapper
{
   public long ID { get; set; }
   public String Prenom { get; set; }
   public String Nom { get; set; }
   public String Telephone { get; set; }
   public String Avatar { get; set; }
   public DateTime Naissance { get; set; }
   public String CodePostal { get; set; }
   public int Sexe { get; set; }
   public int EtatCivil { get; set; }
   public PersonnesTable(String connexionString, System.Web.UI.Page Page)
      : base(connexionString, Page)
   {
      SQLTableName = "PERSONNES";
   }
   public override void GetValues()
   {
      ID = long.Parse(FieldsValues[0]);
      Prenom = FieldsValues[1];
      Nom = FieldsValues[2];
      Telephone = FieldsValues[3];
      CodePostal = FieldsValues[4];
      Avatar = FieldsValues[5];
      Naissance = DateTime.Parse(FieldsValues[6]);
      Sexe = int.Parse(FieldsValues[7]);
      ContentDelegates[7] = ContentDelegateSexe;
      EtatCivil = int.Parse(FieldsValues[8]);
      ContentDelegates[8] = ContentDelegateEtatCivil;
   }
   System.Web.UI.WebControls.WebControl ContentDelegateSexe()
   {
      Label lbl = new Label();
      if (Sexe == 0)
         lbl.Text = "Masculin";
      else
         lbl.Text = "Féminin"; return lbl;
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