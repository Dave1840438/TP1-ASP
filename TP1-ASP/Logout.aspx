<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="TP1_ASP.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Timer ID="RefreshSession" runat="server" Interval="3000" OnTick="RefreshSession_Tick"></asp:Timer>
    <hr />
    <label>Votre session a expirée, vous devez vous <a href="Login.aspx">authentifer</a>.</label>
</asp:Content>
