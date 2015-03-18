<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjouterPersonneV2.aspx.cs" Inherits="LABO_1.CreateUser2" %>

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

        </script>
    </head>
<body id="body">
    <form id="form1" method="post" runat="server">
        <h2>Ajouter une personne</h2>  
        <hr />
        <div class="main">
            <table >
                <tr> 
                    <td id="InfoSection">
                        <table>
                            <tr>
                                <td> <label for="TB_Prenom" class='label'>Prénom:</label>  </td>
                                <td>
                                    <asp:TextBox ID="TB_Prenom" name="TB_Prenom" runat="server" CssClass="textbox" 
                                        onkeyup = "ConstrainToAlpha(event);"> </asp:TextBox>
                               </td>
                            </tr>
                            <tr>
                                <td> <label for="TB_Nom" class='label'>Nom:</label> </td>
                                <td>
                                    <asp:TextBox ID="TB_Nom" name="TB_Nom" runat="server" CssClass="textbox"
                                         onkeyup = "ConstrainToAlpha(event);"> </asp:TextBox>
                                 </td>
                            </tr>
                           <tr>
                                <td> <label for="TB_Telephone" class='label'>Téléphone:</label> </td>
                                <td>
                                    <asp:TextBox ID="TB_Telephone" name="TB_Telephone" runat="server" CssClass="textbox"
                                        onkeydown="Valide_Masque(event);" onkeyup = "Post_Check_Masque(event);" alt="(###) ###-####"></asp:TextBox>
                                </td>
                            </tr>
                           <tr>
                                <td> <label for="TB_CodePostal" class='label'>Code postal:</label> </td>
                                <td>
                                    <asp:TextBox ID="TB_CodePostal" name="TB_CodePostal" runat="server" CssClass="textbox"
                                        onkeydown="Valide_Masque(event);" onkeyup = "Post_Check_Masque(event);" alt="C#C #C#"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td> <label for="TB_Naissance" class='label'>Naissance:</label> </td>
                                <td>
                                    <asp:TextBox ID="TB_Naissance" name="TB_Naissance" runat="server" CssClass = "textbox datepicker"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td> <label class='label' >Sexe:</label> </td>
                                <td >
                                    <asp:RadioButtonList ID="RBL_Sexe" runat="server" CssClass="radio">
                                        <asp:ListItem>masculin</asp:ListItem>
                                        <asp:ListItem>féminin</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td> <label class='label'>État civil:</label> </td>
                                <td>
                                    <asp:RadioButtonList ID="RBL_EtatCivil" runat="server" CssClass="radio">
                                        <asp:ListItem>célibataire</asp:ListItem>
                                        <asp:ListItem>marié</asp:ListItem>
                                        <asp:ListItem>conjoint de fait</asp:ListItem>
                                        <asp:ListItem>séparé</asp:ListItem>
                                        <asp:ListItem>veuf</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td> <asp:Button ID="BTN_Add" runat="server" Text="Soumettre..." class="submitBTN" 
                                                 OnClick="BTN_Add_Click"
                                                 ValidationGroup="PersonneInfo"/> </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td><asp:Button ID="BTN_Cancel" runat="server" Text="Annuler" class="submitBTN" OnClick="BTN_Cancel_Click"/> </td>
                            </tr>
                         </table>
                    </td >
                    <td id="AvatarSection" >
                        <asp:Image ID="IMG_Avatar" runat="server" CssClass="thumbnail"  ImageUrl="~/Images/Anonymous.png" />
                        <hr />
                        <asp:FileUpload ID="FU_Avatar" runat="server" ClientIDMode="Static" onchange="PreLoadImage();" />             
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:left; font-size:12px;">
                        <asp:ValidationSummary ID="ValidationSummary1" 
                            HeaderText="Les champs suivants sont vides ou syntaxiquement erronés:"
                            DisplayMode="BulletList"
                            EnableClientScript="true"
                            runat="server" ValidationGroup="PersonneInfo" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:CustomValidator ID="CV_Prenom" runat="server"  
            ControlToValidate="TB_Prenom" 
            ErrorMessage="Prénom" Text=""
            OnServerValidate="CV_TB_Prenom_ServerValidate" 
            ValidationGroup="PersonneInfo" 
            ValidateEmptyText="True" 
            Display="None">
        </asp:CustomValidator>
        <asp:CustomValidator ID="CV_Nom" runat="server"  
            ControlToValidate="TB_Nom" 
            ErrorMessage="Nom"
            OnServerValidate="CV_TB_Nom_ServerValidate" 
            ValidationGroup="PersonneInfo" 
            ValidateEmptyText="True" 
            Display="None">
        </asp:CustomValidator>
        <asp:CustomValidator ID="CV_Telephone" runat="server"  
            ControlToValidate="TB_Telephone" 
            ErrorMessage="Téléphone"
            OnServerValidate="CV_TB_Telephone_ServerValidate" 
            ValidationGroup="PersonneInfo" 
            ValidateEmptyText="True"  
            Display="None">
        </asp:CustomValidator>
        <asp:CustomValidator ID="CV_CodePostal" runat="server"  
            ControlToValidate="TB_CodePostal" 
            ErrorMessage="Code postal"
            OnServerValidate="CV_TB_CodePostal_ServerValidate" 
            ValidationGroup="PersonneInfo" 
            ValidateEmptyText="True"  
            Display="None">
        </asp:CustomValidator>
        <asp:CustomValidator ID="CV_Naissance" runat="server"  
            ControlToValidate="TB_Naissance" 
            ErrorMessage="Date de naissance" 
            OnServerValidate="CV_TB_Naissance_ServerValidate" 
            ValidationGroup="PersonneInfo" 
            ValidateEmptyText="True"  
            Display="None">
        </asp:CustomValidator>
        <asp:CustomValidator ID="CV_RBL_Sexe" runat="server"  
            ControlToValidate="RBL_Sexe" 
            ErrorMessage="il faut choisir un sexe..."
            OnServerValidate="CV_RBL_Sexe_ServerValidate" 
            ValidationGroup="PersonneInfo" 
            ValidateEmptyText="True" 
            Display="None">
        </asp:CustomValidator>
        <asp:CustomValidator ID="CV_RBL_EtatCivil" runat="server"  
            ControlToValidate="RBL_EtatCivil" 
            ErrorMessage="il faut choisi un état civil"
            OnServerValidate="CV_RBL_EtatCivil_ServerValidate" 
            ValidationGroup="PersonneInfo" 
            ValidateEmptyText="True"  
            Display="None">
        </asp:CustomValidator>
    </form>
</body>
</html>
