<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="ChatRoom.aspx.cs" Inherits="TP1_ASP.ChatRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Timer runat="server" ID="RefreshChat" Interval="3000" OnTick="RefreshChat_Tick"></asp:Timer>
    <asp:Timer runat="server" ID="RefreshUsers" Interval="3000" OnTick="RefreshUsers_Tick"></asp:Timer>

    <table>
        <tr>
            <td>
                <asp:Panel ID="PN_ConvoList" runat="server"></asp:Panel>
            </td>
            <td>


                <asp:UpdatePanel ID="UPN_Chat" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RefreshChat" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Table ID="TB_Chat" runat="server">
                        </asp:Table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RefreshUsers" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Table ID="TB_UserList" runat="server"></asp:Table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:Button ID="BTN_Return" runat="server" Text="Retour..." OnClick="BTN_Return_Click" />
            </td>
        </tr>
        <tr>
            <td />
            <td>
                <asp:TextBox ID="TBX_ChatInput" runat="server" Rows="2" Columns="40" MaxLength="80"></asp:TextBox>
                <asp:Button ID="BTN_Send" runat="server" Text="Envoyer..." OnClick="BTN_Send_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
