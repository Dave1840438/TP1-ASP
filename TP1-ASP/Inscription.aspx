<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Inscription.aspx.cs" Inherits="TP1_ASP.Inscription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .MainPanel
        {
            width:500px;
            height:auto;
            background-color:gray;
            padding:5px;
            margin:auto;
        }
        .Textbox
        {
            margin-left:10px;
            float:right;
        }
        .Label
        {
            float:left;
        }
        .Button
        {
            float:right;
            margin-left:5px;
        }
    </style>
    <br />
    <asp:Panel ID="MainPanel" runat="server" CssClass="MainPanel">
        <asp:Label ID="Label1" runat="server" Text="Nom Complet:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_NomComplet" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Nom d'Usager:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Username" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Mot de Passe:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Password" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Confirmation du Mot de Passe:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_ConfrimPassword" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Adresse Courriel:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Email" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Confirmation du courriel:" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_ConfirmEmail" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="BTT_Inscription" runat="server" Text="Inscription" CssClass="Button" OnClick="BTT_Inscription_Click" />
        <asp:Button ID="Button2" runat="server" Text="Annuler" CssClass="Button" />
        <br />
        <br />
    </asp:Panel>
</asp:Content>