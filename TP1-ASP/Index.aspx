<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TP1_ASP.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Button ID="BTT_ManageProfile" runat="server" Text="Gérer votre profil..." OnClick="BTT_ManageProfile_Click" />
    <br />
    <asp:Button ID="BTT_Log" runat="server" Text="Journal des visites..." OnClick="BTT_Log_Click"/>
    <br />
    <asp:Button ID="BTT_OnlineUsers" runat="server" Text="Usagers en ligne..." OnClick="BTT_OnlineUsers_Click" />
    <br />
    <asp:Button ID="BTT_Disconnect" runat="server" Text="Déconnexion" OnClick="BTT_Disconnect_Click"/>    
    <br />
</asp:Content>
