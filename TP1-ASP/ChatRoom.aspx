<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="ChatRoom.aspx.cs" Inherits="TP1_ASP.ChatRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jquery-1.11.2.min.js"></script>
    <style>
        .UserTable {
            background-color: ghostwhite;
            border: 1px solid black;
            border-spacing: 1px;
            border-collapse: separate;
        }

        .MainTable {
            background-color: lightgray;
        }

        td {
            padding: 5px;
        }

        .Salles {
            border: 2px solid black;
        }

        .auto-style1 {
            height: 49px;
        }

        .TableConvo {
            padding: 0px;
        }

        .ConvoButton {
            text-align: left;
            width: 178px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <hr />
    <asp:Timer runat="server" ID="RefreshChat" Interval="3000" OnTick="RefreshChat_Tick"></asp:Timer>
    <asp:Timer runat="server" ID="RefreshUsers" Interval="3000" OnTick="RefreshUsers_Tick"></asp:Timer>
    <table class="MainTable">
        <tr>
            <td class="auto-style1">
                <div id="DIV_ConvoList" style="overflow: auto; border: 2px; border-color: black; border-style: solid; height: 200px; width: 200px; display: inline-block">
                    <asp:Table ID="TB_ConvoList" runat="server" CssClass="Table_Convo">
                    </asp:Table>
                </div>
            </td>
            <td class="auto-style1" style="border-style: solid; border-width: medium">
                <asp:UpdatePanel ID="UPN_Chat" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RefreshChat" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_Chat" style="overflow: auto">
                            <asp:Table GridLines="Horizontal" CssClass="Salles" ID="TB_Chat" runat="server">
                            </asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="auto-style1">
                <asp:UpdatePanel ID="UPN_OnlineUsers" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RefreshUsers" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_OnlineUsers" style="overflow:auto">
                            <asp:Table CssClass="UserTable" GridLines="Both" ID="TB_UserList" runat="server"></asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td />
            <td>
                <asp:TextBox ID="TBX_ChatInput" runat="server" style="display:none"></asp:TextBox>
                <textarea rows="2" maxlength="80" onkeyup="document.getElementById('TBX_ChatInput').value = this.value; char = (event.which || event.keyCode); if (char == 13) document.getElementById('BTN_Send').click();"></textarea>
                <asp:Button ID="BTN_Send" runat="server" Text="Envoyer..." OnClick="BTN_Send_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="BTN_Return" runat="server" Text="Retour..." OnClick="BTN_Return_Click" />

     
</asp:Content>
