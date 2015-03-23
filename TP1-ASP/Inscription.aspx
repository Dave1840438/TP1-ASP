<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Inscription.aspx.cs" Inherits="TP1_ASP.Inscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .thumbnail {}
        .auto-style1 {
            width: 257px;
        }
        .auto-style2 {
            width: 136px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">

    
    <br />
    <asp:Panel ID="MainPanel" runat="server" CssClass="MainPanel">
        <asp:Label ID="FullName" runat="server" Text="Nom Complet:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_NomComplet" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="UserName" runat="server" Text="Nom d'Usager:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Username" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Password" runat="server" Text="Mot de Passe:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Password" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Password_Confirm" runat="server" Text="Confirmation du Mot de Passe:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_ConfrimPassword" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Email" runat="server" Text="Adresse Courriel:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Email" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Email_Confirm" runat="server" Text="Confirmation du courriel:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_ConfirmEmail" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <table border="0">
            <tr>
                <td rowspan="3"><asp:Label ID="Avatar" runat="server" Text="Avatar:" CssClass="Label" Width="124px"></asp:Label></td>
                <td class="auto-style2"><asp:Image ID="IMG_Avatar" runat="server" CssClass="thumbnail" ImageUrl="~/Images/ADD.png" Height="120px" Width="133px" /></td>
                <td class="auto-style1"><asp:FileUpload ID="FU_Avatar" runat="server" onchange="PreLoadImage();"/></td>
            </tr>
        </table>
        <asp:Button ID="BTT_Inscription" runat="server" Text="Inscription" CssClass="Button" OnClick="BTT_Inscription_Click" />
        <asp:Button ID="BTT_Annuler" runat="server" Text="Annuler" CssClass="Button" OnClick="BTT_Annuler_Click"/>
        <br />
        
    </asp:Panel>
    
</asp:Content>
