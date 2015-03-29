<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Profil.aspx.cs" Inherits="TP1_ASP.Profil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <asp:Panel ID="MainPanel" runat="server" CssClass="MainPanel">
        <asp:Label ID="FullName" runat="server" Text="Nom Complet:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_NomComplet" runat="server" CssClass="Textbox"></asp:TextBox>

        <asp:CustomValidator ID="CVal_Name" runat="server" ErrorMessage="Nom complet ne peut être vide!" Text="!"
            ControlToValidate="TBX_UserName" OnServerValidate="CV_UsernameIsEmpty" ValidateEmptyText="true" />
        <br />
        <br />
        <asp:Label ID="UserName" runat="server" Text="Nom d'Usager:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Username" runat="server" CssClass="Textbox"></asp:TextBox>

        <asp:CustomValidator ID="CVal_Username" runat="server" ErrorMessage="Nom d'usager ne peut être vide!" Text="!"
            ControlToValidate="TBX_Username" OnServerValidate="CV_UsernameIsEmpty" ValidateEmptyText="true" />
        <br />
        <br />
        <asp:Label ID="Password" runat="server" Text="Mot de Passe:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Password" runat="server" CssClass="Textbox"></asp:TextBox>
        <asp:CustomValidator ID="CVal_EmptyPassword" runat="server" ErrorMessage="Le motde passe ne peut pas être vide!" Text="!"
            ControlToValidate="TBX_Password" OnServerValidate="CV_PasswordIsEmpty" ValidateEmptyText="True" />

        <br />
        <br />
        <asp:Label ID="Password_Confirm" runat="server" Text="Confirmation du Mot de Passe:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_ConfirmPassword" runat="server" CssClass="Textbox"></asp:TextBox>
        <asp:CustomValidator ID="CVal_PasswordsMatch" runat="server" ErrorMessage="Les mots de passe ne correspondent pas!" Text="!"
            ControlToValidate="TBX_ConfirmPassword" OnServerValidate="CV_PasswordsMatch" ValidateEmptyText="true" />
        <br />
        <br />
        <asp:Label ID="Email" runat="server" Text="Adresse Courriel:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Email" runat="server" CssClass="Textbox"></asp:TextBox>
        <asp:CustomValidator ID="CVal_EmptyEmail" runat="server" ErrorMessage="L'email ne peut pas être vide!" Text="!"
            ControlToValidate="TBX_Email" OnServerValidate="CV_EmailIsEmpty" ValidateEmptyText="True" />
        <br />
        <br />
        <asp:Label ID="Email_Confirm" runat="server" Text="Confirmation du courriel:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_ConfirmEmail" runat="server" CssClass="Textbox"></asp:TextBox>
        <asp:CustomValidator ID="CVal_EmailsMatch" runat="server" ErrorMessage="Les emails ne correspondent pas!" Text="!"
            ControlToValidate="TBX_ConfirmEmail" OnServerValidate="CV_EmailIsEmpty" ValidateEmptyText="true" />
        <br />
        <br />
        <table border="0">
            <tr>
                <td rowspan="3">
                    <asp:Label ID="Avatar" runat="server" Text="Avatar:" CssClass="Label" Width="124px"></asp:Label></td>
                <td class="auto-style2">
                    <asp:Image ID="IMG_Avatar" runat="server" CssClass="thumbnail" ImageUrl="~/Images/ADD.png" Height="120px" Width="133px" /></td>
                <td class="auto-style1">
                    <asp:FileUpload ID="FU_Avatar" runat="server" onchange="PreLoadImage();"  />
                </td>
            </tr>
        </table>
       <br />

       
        <br />
         <asp:Button ID="BTT_Modifer" runat="server" Text="Mettre à jour..." CssClass="Button" OnClick="BTT_Modifier_Click" />
        <asp:Button ID="BTT_Annuler" runat="server" Text="Annuler" CssClass="Button" OnClick="BTT_Annuler_Click" />
        <br />
        <asp:ValidationSummary ID="Subscrive_Validation" runat="server" />

    </asp:Panel>
</asp:Content>
