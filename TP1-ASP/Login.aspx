<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TP1_ASP.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <hr />
    <asp:Panel ID="Main_Panel" runat="server" CssClass="MainPanel" HorizontalAlign="Justify" Width="387px">
        <table>
            <tr>
                <td><asp:Label ID="LBL_Username" runat="server" Text="Nom d'utilisateur : " CssClass="Label"></asp:Label></td><td><asp:TextBox ID="TBX_Username" runat="server" CssClass="Textbox"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="LBL_Password" runat="server" Text="Password : " CssClass="Label"></asp:Label></td><td><asp:TextBox ID="TBX_Password" runat="server" CssClass="Textbox" type="password"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td><td><asp:Button ID="BTT_Login" runat="server" CssClass="Button" Text="Connexion..." OnClick="BTT_Connect_Click" /></td>
            </tr>
            <tr>
                <td></td><td><asp:Button ID="BTT_Inscription" runat="server" CssClass="Button" Text="Inscription..." OnClick="BTT_Inscription_Click"/></td>
            </tr>
            <tr>
                <td></td><td><asp:Button ID="BTT_ForgotPassword" runat="server" CssClass="Button" Text="Mot de passe oublié..." OnClick="BTT_ForgotPassword_Click"/></td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
