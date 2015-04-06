<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="ThreadsManager.aspx.cs" Inherits="TP1_ASP.ThreadsManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <style>
        Listview {
            height: 50%;
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
                <asp:ListView ID="LV_Discussions" runat="server">
                </asp:ListView>
                <asp:DataGrid ID="DGV_Discussions" runat="server" BackColor="Red" SelectedItemStyle-BackColor="Pink">
                    <Columns>
                        <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                    </Columns>

                </asp:DataGrid>
            </td>
            <td>
                <asp:TextBox ID="TBX_TitreDiscussion" runat="server"></asp:TextBox>
                 <asp:CustomValidator ID="CVal_TitreDiscussion" runat="server" ErrorMessage="Le titre ne peut pas être vide!" Text="!"
                        ControlToValidate="TBX_TitreDiscussion" OnServerValidate="CVal_TitreDiscussion_ServerValidate" ValidateEmptyText="True" />
               <asp:CustomValidator ID="CVal_DiscussionExiste" runat="server" ErrorMessage="Le titre existe déjà!" Text="!"
                        ControlToValidate="TBX_TitreDiscussion" OnServerValidate="CVal_DiscussionExiste_Exists" ValidateEmptyText="True" />
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="CBOX_AllUsers" runat="server" Text="Tous les usagers" OnCheckedChanged="CBOX_AllUsers_CheckedChanged" />
                <asp:Table ID="TB_AllExistingUsers" runat="server"></asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_New" Text="Nouvelle discussion..." OnClick="BTN_New_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Modify" Text="Modifier la discussion..." OnClick="BTN_Modify_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Delete" Text="Effacer la discussion..." OnClick="BTN_Delete_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Return" Text="Retour..." OnClick="BTT_Return_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
