<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Profil.aspx.cs" Inherits="TP1_ASP.Profil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 232px;
        }

        .auto-style2 {
            width: 213px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <hr />
    <br />
    <asp:Panel ID="MainPanel" runat="server" CssClass="MainPanel" Width="427px">
        <table>
            <tr>
                <td>
                    <asp:Label ID="FullName" runat="server" Text="Nom Complet:" CssClass="Label"></asp:Label></td>
                <td class="auto-style2">
                    <asp:TextBox ID="TBX_NomComplet" runat="server" CssClass="Textbox"></asp:TextBox>
                    <asp:CustomValidator ID="CV_FullNameIsNotEmpty" runat="server" ErrorMessage="Le nom complet ne peut pas être vide!" Text="!"
                        ControlToValidate="TBX_Username" OnServerValidate="CV_FullNameIsNotEmpty_ServerValidate" ValidateEmptyText="true" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="UserName" runat="server" Text="Nom d'Usager:" CssClass="Label"></asp:Label></td>
                <td class="auto-style2">
                    <asp:TextBox ID="TBX_Username" runat="server" CssClass="Textbox" Enabled="false"></asp:TextBox></td>
                <td>
                    <asp:CustomValidator ID="CVal_Username" runat="server" ErrorMessage="Nom d'usager ne peut être vide!" Text="!"
                        ControlToValidate="TBX_Username" OnServerValidate="CV_UsernameIsEmpty" ValidateEmptyText="true" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Password" runat="server" Text="Mot de Passe:" CssClass="Label"></asp:Label></td>
                <td class="auto-style2">
                    <asp:TextBox ID="TBX_Password" runat="server" CssClass="Textbox" type="password"></asp:TextBox></td>
                <td>
                    <asp:CustomValidator ID="CVal_EmptyPassword" runat="server" ErrorMessage="Le motde passe ne peut pas être vide!" Text="!"
                        ControlToValidate="TBX_Password" OnServerValidate="CV_PasswordIsEmpty" ValidateEmptyText="True" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Password_Confirm" runat="server" Text="Confirmation du Mot de Passe:" CssClass="Label"></asp:Label></td>
                <td class="auto-style2">
                    <asp:TextBox ID="TBX_ConfirmPassword" runat="server" CssClass="Textbox" type="password"></asp:TextBox></td>
                <td>
                    <asp:CustomValidator ID="CVal_PasswordsMatch" runat="server" ErrorMessage="Les mots de passe ne correspondent pas!" Text="!"
                        ControlToValidate="TBX_ConfirmPassword" OnServerValidate="CV_PasswordsMatch" ValidateEmptyText="true" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Email" runat="server" Text="Adresse Courriel:" CssClass="Label"></asp:Label></td>
                <td class="auto-style2">
                    <asp:TextBox ID="TBX_Email" runat="server" CssClass="Textbox"></asp:TextBox></td>
                <td>
                    <asp:CustomValidator ID="CVal_EmptyEmail" runat="server" ErrorMessage="L'email ne peut pas être vide!" Text="!"
                        ControlToValidate="TBX_Email" OnServerValidate="CV_EmailIsEmpty" ValidateEmptyText="True" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Email_Confirm" runat="server" Text="Confirmation du courriel:" CssClass="Label"></asp:Label></td>
                <td class="auto-style2">
                    <asp:TextBox ID="TBX_ConfirmEmail" runat="server" CssClass="Textbox"></asp:TextBox></td>
                <td>
                    <asp:CustomValidator ID="CVal_EmailsMatch" runat="server" ErrorMessage="Les emails ne correspondent pas!" Text="!"
                        ControlToValidate="TBX_ConfirmEmail" OnServerValidate="CV_EmailsMatch" ValidateEmptyText="true" />
                    <asp:RegularExpressionValidator  ErrorMessage="L'adresse de courriel est syntaxiquement invalide!" display="None" ID="RegularExpressionValidator1" runat="server" ControlToValidate="TBX_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                </td>
            </tr>
            <tr>
                <td rowspan="3">
                    <asp:Label ID="Avatar" runat="server" Text="Avatar:" CssClass="Label" Width="124px"></asp:Label></td>
                <td class="auto-style2">
                    <asp:Image ID="IMG_Avatar" runat="server" CssClass="thumbnail" ImageUrl="~/Images/ADD.png" Height="120px" Width="133px" />
                    <asp:FileUpload ID="FU_Avatar" runat="server" onchange="PreLoadImage();" /></td>
            </tr>
        </table>
        <asp:Button ID="BTT_Modifer" runat="server" Text="Mettre à jour..." CssClass="Button" OnClick="BTT_Modifier_Click" />
        <asp:Button ID="BTT_Annuler" runat="server" Text="Annuler" CssClass="Button" OnClick="BTT_Annuler_Click" />
        <br />
        <asp:ValidationSummary ID="Subscrive_Validation" runat="server" />
    </asp:Panel>
</asp:Content>
