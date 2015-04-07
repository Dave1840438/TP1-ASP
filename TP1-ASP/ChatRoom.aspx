<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="ChatRoom.aspx.cs" Inherits="TP1_ASP.ChatRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                <asp:Panel ID="PN_ConvoList" runat="server" ></asp:Panel>
            </td>
            <td>
                <asp:UpdatePanel ID="UPN_Chat" runat="server"></asp:UpdatePanel>
            </td>
            <td>
                <asp:Table ID="TB_UserList" runat="server"></asp:Table>
            </td>
            <td>
                <asp:Button ID="BTN_Return" runat="server" Text="Retour..." OnClick="BTN_Return_Click" />
            </td>
        </tr>
        <tr>
            <td/>
            <td>
                <asp:TextBox ID="TBX_ChatInput" runat="server" Rows="2" Columns="40" MaxLength="80"></asp:TextBox>
            </td>
        </tr>
    </table>


</asp:Content>
