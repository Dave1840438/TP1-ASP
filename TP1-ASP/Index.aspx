<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TP1_ASP.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <hr />
    <table style="margin:auto">
        <tr>
            <td><asp:Button ID="BTT_ManageProfile" runat="server" Text="Gérer votre profil..." OnClick="BTT_ManageProfile_Click" CssClass="Button"/></td>
        </tr>
        <tr>
            <td><asp:Button ID="BTT_Log" runat="server" Text="Journal des visites..." OnClick="BTT_Log_Click" CssClass="Button"/></td>
        </tr>
        <tr>
            <td><asp:Button ID="BTT_ManageThreads" runat="server" Text="Gérer mes discussions..." OnClick="BTT_ManageThreads_Click" CssClass="Button"/></td>
        </tr>
        <tr>
            <td><asp:Button ID="BTT_ChatRoom" runat="server" Text="Salle de discussion..." OnClick="BTT_ChatRoom_Click" CssClass="Button"/></td>
        </tr>
        <tr>
            <td><asp:Button ID="BTT_OnlineUsers" runat="server" Text="Usagers en ligne..." OnClick="BTT_OnlineUsers_Click" CssClass="Button"/></td>
        </tr>
        <tr>
            <td><asp:Button ID="BTT_Disconnect" runat="server" Text="Déconnexion" OnClick="BTT_Disconnect_Click" CssClass="Button"/>    </td>
        </tr>
    </table>
</asp:Content>
