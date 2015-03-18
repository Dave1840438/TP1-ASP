<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifierPersonne.aspx.cs" Inherits="LABO_1.ModifierPersonne" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css"/>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css"/>

    <script src="ClientFormUtilities.js"></script>
    <link rel="stylesheet" href="FormStyles.css"/>

    <script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker({
                dateFormat: "yy-mm-dd",
                dayNamesMin: ["S", "M", "T", "W", "T", "F", "S"]
            });
        });


        function BuildForm(targetFormID) {
            // création du div qui englobe le formulaire

            document.getElementById(targetFormID).appendChild(BuildTable(11, 2));
            // création des controles
            AddInputText(0, "Prénom", "Prenom", "ident");
            AddInputText(1, "Nom", "Nom", "ident");

            AddMaskedInputText(2, "Téléphone", "Telephone", "(###) ###-####");
            AddMaskedInputText(3, "Code postal", "CodePostal", "C#C #C#");
            AddMaskedInputText(4, "Naissance", "Naissance", "####-##-##");
            document.getElementById("Naissance").className = " masked datepicker";

            AddRadioButtonGroup(5, "Sexe", "Sexe", "masculin", "féminin");
            AddRadioButtonGroup(6, "État civil", "Etatcivil", "célibataire", "marié", "conjoint de fait", "séparé", "veuf");
            // la rangée 7 est volontairement sautée
            AddSubmitButton(8, "Modifier...","edit",true);
            AddSubmitButton(9, "Effacer...","delete",false,"Êtes-vous sûr de vouloir effacer cet enregistrement?");
            AddSubmitButton(10, "Annuler...","cancel",false);
            InstallHighLiteEmptyDelegates();
        }

        </script>
</head>
<body>
    <form id="form1" runat="server">
       <h2>
            Edition de la personne id: <asp:Label ID="LB_ID" runat="server" Text="..."></asp:Label>
            &nbsp;
        </h2>
        <hr />
        <div class="main">
            <table >
                <tr> 
                    <td id="InfoSection"></td>
                    <td id="AvatarSection" >
                        <asp:Image ID="IMG_Avatar" runat="server" CssClass="thumbnail"  ImageUrl="~/Images/Anonymous.png" />
                        <hr />
                        <asp:FileUpload ID="FU_Avatar" runat="server" ClientIDMode="Static" onchange="PreLoadImage();" />             
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
                BuildForm("InfoSection");
            </script>   
            <asp:Panel ID="PN_Script" runat="server"></asp:Panel>
            <asp:Panel ID="PN_GridView" runat="server"></asp:Panel>
       </div>
    </form>
</body>
</html>
