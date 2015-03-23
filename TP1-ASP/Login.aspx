<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TP1_ASP.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Main_Panel" runat="server" CssClass="MainPanel">
        <asp:Label ID="LBL_Username" runat="server" Text="Nom d'utilisateur :" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Username" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="LBL_Password" runat="server" Text="Password" CssClass="Label"></asp:Label>
        <asp:TextBox ID="TBX_Password" runat="server" CssClass="Textbox"></asp:TextBox>
        <br />
    </asp:Panel>
</asp:Content>
