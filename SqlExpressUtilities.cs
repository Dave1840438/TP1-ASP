using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace SqlExpressUtilities
{
    public delegate System.Web.UI.WebControls.WebControl CellContentDelegate();

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // SqlExpressWrapper version beta
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // Cette classe offre une interface conviviale au programmeur utilisateur pour des transactions SQL
    // avec une table d'une base de données SQL Express
    // Note importante:
    // Afin de profiter des toutes les fonctionnalités de cette classe
    // assurez-vous que le premier champ soit ID de type BigInt INDETITY(1,1) dans la structure des 
    // tables visées
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // Auteur : Nicolas Chourot
    // Départment d'informatique
    // Collège Lionel-Groulx
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SqlExpressWrapper
    {
        // objet de connection
        SqlConnection connection;
        // chaine de connection
        string connexionString;
        // Objet de lecture issue de la dernière requête SQL
        public SqlDataReader reader;
        // Objet Page de classe "Web Form" donnant accès à l'objet Application, Session et Response, etc.
        public System.Web.UI.Page Page;
        // Nom de la table
        public String SQLTableName = "";
        // Liste des valeur des champs lors de la lecture de la requête 
        public List<string> FieldsValues = new List<string>();
        // Liste des noms des champs de la table en cours de lecture
        public List<string> FieldsNames = new List<string>();
        public List<string> ColumnTitles = new List<string>();
        public List<bool> ColumnsSortEnable = new List<bool>();

        // Liste des types des champs de la table en cours de lecture
        public List<Type> FieldsTypes = new List<Type>();
        public List<bool> ColumnsVisibility = new List<bool>();

        // Liste des délégates chargés de construire contenu personnalisé pour les champs
        public List<CellContentDelegate> CellsContentDelegate = new List<CellContentDelegate>();
        
         // contructeur obligatoire auquel il faut fournir la chaine de connection et l'objet Page
        public SqlExpressWrapper(String connexionString, System.Web.UI.Page Page)
        {
            this.Page = Page;
            this.connexionString = connexionString;
        }

        // Extraire les valeur des champs de l'enregistrement suivant du lecteur Reader
        bool GetfieldsValues()
        {
            bool endOfReader = false;
            // Effacer la liste des valeurs
            FieldsValues.Clear();
            // si il reste des enregistrements à lire
            if (endOfReader = reader.Read())
            {
                // Extraire les valeurs des champs
                for (int f = 0; f < reader.FieldCount; f++)
                {
                    // la fonction Trim permet l'effacement des espaces en trop 
                    // avant et après la valeur proprement dite
                    FieldsValues.Add(SQLHelper.FromSql(reader[f].ToString().Trim()));
                }
            }
            return endOfReader;
        }

        public virtual void InitColumnsVisibility()
        {
            ColumnsVisibility.Clear();
            for (int f = 0; f < FieldsNames.Count; f++)
            {
                ColumnsVisibility.Add(true);
            }
        }
        public virtual void InitColumnsTitles()
        {
            ColumnTitles.Clear();
            for (int f = 0; f < FieldsNames.Count; f++)
            {
                ColumnTitles.Add(FieldsNames[f]);
            }
        }

        public virtual void InitColumnsSortEnable()
        {
            ColumnsSortEnable.Clear();
            for (int f = 0; f < FieldsNames.Count; f++)
            {
                ColumnsSortEnable.Add(true);
            }
        }

        public virtual void InitCellsContentDelegate()
        {
            CellsContentDelegate.Clear();
            for (int f = 0; f < FieldsNames.Count; f++)
            {
                if (f == 0)
                    // Gestion du contenu à produire pour le champ ID
                    CellsContentDelegate.Add(IDContentDelegate);
                else
                    CellsContentDelegate.Add(null);
            }
        }

        private bool Valid(int index, int count)
        {
            return ((index >= 0) && (index < count));
        }

        public void SetColumnVisibility(String fieldName, bool visibility)
        {
            SetColumnVisibility(IndexOf(fieldName), visibility);
        }
        public void SetColumnVisibility(int fieldIndex, bool visibility)
        {
            if (Valid(fieldIndex, ColumnsVisibility.Count))
                ColumnsVisibility[fieldIndex] = visibility;
        }
        public void SetColumnTitle(String fieldName, String title)
        {
            SetColumnTitle(IndexOf(fieldName), title);
        }
        public void SetColumnTitle(int fieldIndex, String title)
        {
            if (Valid(fieldIndex, ColumnTitles.Count))
                ColumnTitles[fieldIndex] = title;
        }
        public void SetColumnSortEnable(String fieldName, bool enable)
        {
            SetColumnSortEnable(IndexOf(fieldName), enable);
        }
        public void SetColumnSortEnable(int fieldIndex, bool enable)
        {
            if (Valid(fieldIndex, ColumnsSortEnable.Count))
                ColumnsSortEnable[fieldIndex] = enable;
        }
        public void SetCellContentDelegate(String fieldName, CellContentDelegate ccd)
        {
            SetCellContentDelegate(IndexOf(fieldName), ccd);
        }

        public void SetCellContentDelegate(int fieldIndex, CellContentDelegate ccd)
        {
            if (Valid(fieldIndex, CellsContentDelegate.Count))
                CellsContentDelegate[fieldIndex] = ccd;
        }
        
        // Extraire les noms et types des champs 
        void GetFieldsNameAndType()
        {
            if (reader != null)
            {
                FieldsNames.Clear();
                FieldsTypes.Clear();
                
                for (int f = 0; f < reader.FieldCount; f++)
                {
                    FieldsNames.Add(reader.GetName(f));
                    FieldsTypes.Add(reader.GetFieldType(f));
                } 
            }
        }

        public virtual void GetValues()
        {
            // Doit être surcharché par les classes dérivées
        }

        // Saisir les valeurs du prochain enregistrement du Reader
        public bool Next()
        {
            bool more = NextRecord();
            if (more)
                GetValues();
            else
                EndQuerySQL();
            return more;
        }

        // Passer à l'enregistrement suivant du lecteur de requête
        public bool NextRecord()
        {
            return GetfieldsValues();
        }

        // Exécuter une commande SQL
        public int QuerySQL(string sqlCommand)
        {
            // instancier l'objet de collection
            connection = new SqlConnection(connexionString);
            // bâtir l'objet de requête
            SqlCommand sqlcmd = new SqlCommand(sqlCommand);
            // affecter l'objet de connection à l'objet de requête
            sqlcmd.Connection = connection;
            // bloquer l'objet Page.Application afin d'empêcher d'autres sessions concurentes
            // d'avoir accès à la base de données concernée par la requête en cours
            Page.Application.Lock();
            // ouvrir la connection avec la bd
            connection.Open();
            // éxécuter la requête SQL et récupérer les enregistrements qui en découlent dans l'objet Reader
            reader = sqlcmd.ExecuteReader();
            // Saisir les noms et types des champs de la table impliquée dans la requête
            GetFieldsNameAndType();

            InitColumnsVisibility();
            InitColumnsTitles();
            InitColumnsSortEnable();
            InitCellsContentDelegate();

            // retourner le nombre d'enregistrements générés
            return reader.RecordsAffected;
           
        }

        // Conclure la dernière requête
        public void EndQuerySQL()
        {
            // Fermer la connection
            if (connection.State != System.Data.ConnectionState.Closed)
            connection.Close();
            // Débloquer l'objet Page.Application afin que d'autres session puissent
            // accéder à leur tour à la base de données
            Page.Application.UnLock();
        }

        // Extraire tous les enregistrements
        public virtual bool SelectAll(string orderBy = "")
        {
            string sql = "SELECT * FROM " + SQLTableName;
            if (orderBy != "")
                sql += " ORDER BY " + orderBy;
            QuerySQL(sql);
            return reader.HasRows;
        }

        // Extraire l'enregistrement d'id ID
        public bool SelectByID(String ID)
        {
            string sql = "SELECT * FROM " + SQLTableName + " WHERE ID = " + ID;
            QuerySQL(sql);
            if (reader.HasRows)
                Next();
            return reader.HasRows;
        }

        public bool SelectByFieldName(String FieldName, object value)
        {
            string SQL = "SELECT * FROM " + SQLTableName + " WHERE " + FieldName + " = ";
            Type type = value.GetType();
            if (SQLHelper.IsNumericType(type))
                SQL += value.ToString().Replace(',', '.');
            else
                if (type == typeof(DateTime))
                    SQL += "'" + SQLHelper.DateSQLFormat((DateTime)value) + "'";
                else
                    SQL += "'" + SQLHelper.PrepareForSql(value.ToString()) + "'";
            QuerySQL(SQL);
            if (reader.HasRows)
                Next();
            return reader.HasRows;
        }
        // Mise à jour de l'enregistrement
        public virtual void Update()
        {
            UpdateRecord();
        }

        // Met à jour de l'enregistrement courant par le biais des valeurs inscrites dans la liste
        // FieldsValues
        public int UpdateRecord()
        {
            String SQL = "UPDATE " + SQLTableName + " ";
            SQL += "SET ";
            int nb_fields = FieldsNames.Count();
            for (int fieldNum = 1; fieldNum < nb_fields; fieldNum++)
            {
                SQL += "[" + FieldsNames[fieldNum] + "] = ";
                if (FieldsTypes[fieldNum] == typeof(DateTime))
                    SQL += "'" + SQLHelper.DateSQLFormat(DateTime.Parse(FieldsValues[fieldNum])) + "'";
                else
                    SQL += "'" + SQLHelper.PrepareForSql(FieldsValues[fieldNum]) + "'";
                if (fieldNum < (nb_fields - 1)) SQL += ", ";
            }
            SQL += " WHERE [" + FieldsNames[0] + "] = " + FieldsValues[0];

            return NonQuerySQL(SQL);
        }

        // Met à jour de l'enregistrement courant par le biais des valeurs inscrites dans la liste
        // FieldsValues fournie en paramètre
        public int UpdateRecord(params object[] FieldsValues)
        {
            String SQL = "UPDATE " + SQLTableName + " ";
            SQL += "SET ";
            int nb_fields = FieldsValues.Length;
            for (int i = 1; i < nb_fields; i++)
            {
                SQL += "[" + FieldsNames[i] + "] = ";
                Type type = FieldsValues[i].GetType();
                if (SQLHelper.IsNumericType(type))
                    SQL += FieldsValues[i].ToString().Replace(',', '.');
                else
                    if (type == typeof(DateTime))
                        SQL += "'" + SQLHelper.DateSQLFormat((DateTime)FieldsValues[i]) + "'";
                    else
                        SQL += "'" + SQLHelper.PrepareForSql(FieldsValues[i].ToString()) + "'";
                if (i < (nb_fields - 1)) SQL += ", ";
            }
            SQL += " WHERE [" + FieldsNames[0] + "] = " + FieldsValues[0];

            return NonQuerySQL(SQL);
        }

        // Effacer l'enregistrement d'id ID
        public void DeleteRecordByID(String ID)
        {
            String sql = "DELETE FROM " + SQLTableName + " WHERE ID = " + ID;
            NonQuerySQL(sql);
        }

        // Insérer un nouvel enregistrement
        public virtual void Insert()
        {
            InsertRecord();
        }

        // insérer un nouvel enregistrement en utilisant les valeurs stockées dans FieldValues
        public void InsertRecord()
        {
            // Petite patch pour s'assurer que les noms des champs et leur type soient initialisés
            SelectAll();
            NextRecord();
            EndQuerySQL();

            string sql = "INSERT INTO " + SQLTableName + "(";
            for (int i = 1; i < FieldsNames.Count; i++)
            {
                sql += FieldsNames[i];
                if (i < FieldsNames.Count - 1)
                    sql += ", ";
                else
                    sql += ") VALUES (";
            }
            for (int i = 0; i < FieldsValues.Count; i++)
            {
                Type type = FieldsValues[i].GetType();
                if (SQLHelper.IsNumericType(type))
                    sql += FieldsValues[i].ToString().Replace(',', '.');
                else
                    if (type == typeof(DateTime))
                        sql += "'" + SQLHelper.DateSQLFormat((DateTime)DateTime.Parse(FieldsValues[i])) + "'";
                    else
                        sql += "'" + SQLHelper.PrepareForSql(FieldsValues[i].ToString()) + "'";

                if (i < FieldsValues.Count - 1)
                    sql += ", ";
                else
                    sql += ")";
            }
            NonQuerySQL(sql);
        }

        // insérer un nouvel enregistrement en utilisant les valeurs stockées dans FieldValues passé en paramètre
        public void InsertRecord(params object[] FieldsValues)
        {
            // Petite patch pour s'assurer que les noms des champs et leur type soient initialisés
            SelectAll();
            NextRecord();
            EndQuerySQL();

            string sql = "INSERT INTO " + SQLTableName + "(";
            for (int i = 1; i < FieldsValues.Length+1; i++)
            {
                sql += FieldsNames[i];
                if (i < FieldsValues.Length)
                    sql += ", ";
                else
                    sql += ") VALUES (";
            }
            for (int i = 0; i < FieldsValues.Length; i++)
            {
                Type type = FieldsValues[i].GetType();
                if (SQLHelper.IsNumericType(type))
                    sql += FieldsValues[i].ToString().Replace(',', '.');
                else
                    if (type == typeof(DateTime))
                        sql += "'" + SQLHelper.DateSQLFormat((DateTime)FieldsValues[i]) + "'";
                    else
                        sql += "'" + SQLHelper.PrepareForSql(FieldsValues[i].ToString()) + "'";

                if (i < FieldsValues.Length - 1)
                    sql += ", ";
                else
                    sql += ")";
            }
            NonQuerySQL(sql);
        }

        // Éxécuter une requête SQL qui ne génère pas d'enregistrement
        public int NonQuerySQL(string sqlCommand)
        {
            int recordsAffected = QuerySQL(sqlCommand);
            EndQuerySQL();
            return recordsAffected;
        }

        // retourne l'indexe du champs de nom fieldName
        public int IndexOf(string fieldName)
        {
            return FieldsNames.IndexOf(fieldName);
        }

        // Surchage de l'opérateur [] pour pouvoir atteindre un champ par son nom
        // par exemple : tableUsers["UserName"]
        public string this[string fieldName]
        {
            get { return FieldsValues[IndexOf(fieldName)]; }
            set { FieldsValues[IndexOf(fieldName)] = value; }
        }

        // Panneau parent à la GridView généré par MakeGriewView
        private Panel PN_GridView = null;
        public virtual void MakeGridView(Panel PN_GridView, String EditPage)
        {
           
            // converver le panneau parent (utilisé dans certaines méthodes de cette classe)
            this.PN_GridView = PN_GridView;
            Page.Session["EditPage"] = EditPage;
            Table Grid = null;
            if (reader.HasRows)
            {
                Grid = new Table();

                // Construction de l'entête de la GridView
                TableRow tr = new TableRow();
                for (int columnIndex = 0; columnIndex < ColumnTitles.Count; columnIndex++)
                {
                    if (ColumnsVisibility[columnIndex])
                    {
                        TableCell td = new TableCell();
                        tr.Cells.Add(td);
                        Label LBL_Header = new Label();
                        LBL_Header.Text = "<b>" + ColumnTitles[columnIndex] + "</b>";

                        if (ColumnsSortEnable[columnIndex])
                        {
                            ImageButton BTN_Sort = new ImageButton();
                            // assignation du delegate du clic (voir sa définition plus bas dans le code)
                            BTN_Sort.Click += new ImageClickEventHandler(SortField_Click);
                            // IMPORTANT!!!
                            // il faut placer dans le répertoire Images du projet l'icône qui représente un tri
                            BTN_Sort.ImageUrl = @"~/Images/Sort.png";
                            // afin de bien reconnaitre quel champ il faudra trier on construit ici un ID
                            // pour le bouton
                            BTN_Sort.ID = "Sort_" + FieldsNames[columnIndex];
                            td.Controls.Add(BTN_Sort);
                        }
                        td.Controls.Add(LBL_Header);
                    }
                }
                Grid.Rows.Add(tr);
               
                // Construction des rangées de la GridView
                while (Next())
                {
                    tr = new TableRow();
                    for (int fieldIndex = 0; fieldIndex < FieldsValues.Count; fieldIndex++)
                    {
                        if (ColumnsVisibility[fieldIndex])
                        {
                            TableCell td = new TableCell();
                            if (CellsContentDelegate[fieldIndex] != null)
                            {
                                // construction spécialisée du contenu d'une cellule
                                // définie dans les sous classes
                                td.Controls.Add(CellsContentDelegate[fieldIndex]());
                            }
                            else
                            {
                                Type type = FieldsTypes[fieldIndex];
                                if (SQLHelper.IsNumericType(type))
                                {
                                    td.Text = FieldsValues[fieldIndex].ToString();
                                    // IMPORTANT! Il faut inclure dans la section style
                                    // une classe numeric qui impose l'alignement à droite
                                    td.CssClass = "numeric";
                                }
                                else
                                    if (type == typeof(DateTime))
                                        td.Text = DateTime.Parse(FieldsValues[fieldIndex]).ToShortDateString();
                                    else
                                        td.Text = SQLHelper.FromSql(FieldsValues[fieldIndex]);
                            }
                            tr.Cells.Add(td);
                        }
                        Grid.Rows.Add(tr);
                    }
                }
            }
            PN_GridView.Controls.Clear();
            if (Grid!=null)
                PN_GridView.Controls.Add(Grid);
            EndQuerySQL();
        }

        // Spécialisation de la construction de contenu des cellules de tableau html
        // correspondant au champ ID des enregistrements
        public System.Web.UI.WebControls.WebControl IDContentDelegate()
        {
            // Construction d'un hyperlien servant à mener vers une page web d'édition d'enregistrement
            LinkButton lb = new LinkButton();
            lb.ID = "LKBTN_" + this["ID"]; // Doit absoluement le premier champ dans les enregistrement
            lb.Text = this["ID"];
            lb.ToolTip = "Modifier l'enregistrement d'id = " + this["ID"];
            lb.Click += new EventHandler(ID_Click);
            return lb;
        }

        // Gestionnaire du clic sur les icônes qui commandent un tri sur un champ
        protected void SortField_Click(Object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;
            // extraire de l'ID le nom du champ ciblé pour le tri
            string fieldName = ib.ID.Substring(ib.ID.IndexOf("_") + 1);

            // déduire si le tri est ascendant ou descendant celon le dernier 
            // qui a été trié
            if (((string)Page.Session["OrderBy"]) == fieldName)
            {
                if (((string)Page.Session["order"]) == " ASC")
                    Page.Session["order"] = " DESC";
                else
                    Page.Session["order"] = " ASC";
            }
            else
            {
                Page.Session["OrderBy"] = fieldName;
                Page.Session["order"] = " ASC";
            }
            // Extraire tous les enregistrements
            SelectAll((String)Page.Session["OrderBy"] + (String)Page.Session["order"]);
            // reconstruire la GridView
            MakeGridView(PN_GridView, (String)Page.Session["EditPage"]);
        }

        // Gestionnaire du clic sur les IDs (hyperliens)
        // Assigner le "Selected_ID" dans l'objet session
        // ainsi rediriger la session vers la page d'édition
        // pré-réglé dans EditPage
        protected void ID_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            // Assigner le ID ciblé
            string ID = lb.ID.Substring(lb.ID.IndexOf("_") + 1);
            // rediriger la session vers la "Web Form" désignée pour l'édition
            // de l'enregistement d'id Selected_ID
            Page.Session["Selected_ID"] = ID;
            if (Page.Session["EditPage"]!=null)
                Page.Response.Redirect((String)Page.Session["EditPage"]);
        }
    }

    public class SQLHelper
    {

        // pour éviter des erreurs de syntaxe dans les requêtes sql
        static public string PrepareForSql(string text)
        {
            return text.Replace("'", "&c&");
        }

        static public string FromSql(string text)
        {
            return text.Replace("&c&", "'");
        }

        static string TwoDigit(int n)
        {
            string s = n.ToString();
            if (n < 10)
                s = "0" + s;
            return s;

        }

        public static string DateSQLFormat(DateTime date)
        {
            return date.Year + "-" + TwoDigit(date.Month) + "-" + TwoDigit(date.Day) + " " + TwoDigit(date.Hour) + ":" + TwoDigit(date.Minute) + ":" + TwoDigit(date.Second) + ".000";
        }

        public static bool IsNumericType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    }
                    return false;
            }
            return false;
        }
    }
}