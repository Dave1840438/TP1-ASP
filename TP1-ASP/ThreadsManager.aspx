<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="ThreadsManager.aspx.cs" Inherits="TP1_ASP.ThreadsManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        Listview {
            height:50%;
        }


</style>
    <table>
        <tr>
            <td>
            <label>Liste de mes discussions</label>
            </td>
            <td>
                <label>Titre de la discussion</label>
            </td>
        </tr>
        <tr>
            <td rowspan="2">
            <asp:ListView ID="LV_Discussions" runat="server"></asp:ListView>
            </td>
            <td>
                <asp:TextBox ID="TBX_TitreDiscussion" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PN_Usagers" runat="server">

                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_New" Text ="Nouvelle discussion..." OnClick="BTN_New_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Modify" Text ="Modifier la discussion..." OnClick="BTN_Modify_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Delete" Text ="Effacer la discussion..." OnClick="BTN_Delete_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Return" Text ="Retour..." OnClick="BTT_Return_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
