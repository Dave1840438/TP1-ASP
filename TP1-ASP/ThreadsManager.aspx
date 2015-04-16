<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="ThreadsManager.aspx.cs" Inherits="TP1_ASP.ThreadsManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <hr />
    <style>
        Listview {
            height: 50%;
        }

        .MainTable {
            background-color: lightgray;
            padding: 5px;
            margin:auto;
        }

        .auto-style2 {
            width: 100px;
        }

        .auto-style3 {
            height: 27px;
        }

        .auto-style4 {
            width: 100px;
            height: 27px;
        }

        .auto-style5 {
            width: 218px;
        }
    </style>
    <table class="MainTable">
        <tr>
            <td class="auto-style3">
                <label>Liste de mes discussions:</label>
                <hr />
            </td>
            <td class="auto-style4"></td>
            <td class="auto-style5" rowspan="2" style="border: thin solid #000000">
                <label>Titre de la discussion</label>
                :
                <br />
                <br />
                <asp:UpdatePanel ID="UPN_Titre_Discussion" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="TBX_TitreDiscussion" runat="server"></asp:TextBox>
                        <asp:CustomValidator ID="CVal_TitreDiscussion" runat="server" ErrorMessage="Le titre ne peut pas être vide!" Text="Vide!"
                            ControlToValidate="TBX_TitreDiscussion" OnServerValidate="CVal_TitreDiscussion_ServerValidate" ValidateEmptyText="True" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
            </td>
        </tr>
        <tr>
            <td rowspan="4">
                <asp:UpdatePanel ID="UPN_Thread_List" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListBox ID="LB_Thread_List" runat="server" OnSelectedIndexChanged="LB_Thread_List_SelectedIndexChanged" Style="height: 250px; width: 165px"></asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style5" rowspan="2" style="border-style: solid; border-width: thin">Droit d&#39;accès à la discussion:<br />
                <asp:CustomValidator ID="CV_AuMoinsUnInvite" runat="server" Text="Il faut au moins un invité!"
                    OnServerValidate="CV_AuMoinsUnInvite_ServerValidate" />
                <br />
                <asp:UpdatePanel ID="UPN_UsersCheckboxes" runat="server" UpdateMode="Conditional">
                    <Triggers></Triggers>
                    <ContentTemplate>
                        <asp:CheckBox ID="CBOX_AllUsers" runat="server" Text="Tous les usagers" OnCheckedChanged="CBOX_AllUsers_CheckedChanged" />
                        <asp:Table ID="TB_AllExistingUsers" runat="server"></asp:Table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <hr />
                <asp:Button runat="server" ID="BTN_New" Text="Nouveau..." OnClick="BTN_Clear_Click" CssClass="Button" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UPN_BTN_Send_Or_Create" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="BTN_Modify_Or_Create" Text="Créer..." OnClick="BTN_Modify_Or_Create_Click" CssClass="Button" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Delete" Text="Effacer la discussion..." OnClick="BTN_Delete_Click" CssClass="Button" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Return" Text="Retour..." OnClick="BTT_Return_Click" CssClass="Button" />
            </td>
        </tr>
    </table>
</asp:Content>
