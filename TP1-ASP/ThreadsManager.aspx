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
                <asp:ListView ID="LV_Discussions" runat="server" OnSelectedIndexChanged="LV_Discussions_SelectedIndexChanged">
                    <LayoutTemplate>
                        <asp:Label runat="server"></asp:Label>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:DataGrid ID="DGV_Discussions" runat="server" BackColor="Red" SelectedItemStyle-BackColor="Pink" OnSelectedIndexChanged="LV_Discussions_SelectedIndexChanged">
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
                
                <asp:UpdatePanel ID="UPN_UsersCheckboxes" runat="server">
                    <Triggers></Triggers>
                    <ContentTemplate>
                    <asp:Table ID="TB_AllExistingUsers" runat="server"></asp:Table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_New" Text="Nouveau..." OnClick="BTN_Clear_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Modify_Or_Create" Text="Créer..." OnClick="BTN_Modify_Or_Create_Click" />
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
