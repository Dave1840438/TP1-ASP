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
            <td>Liste des conversations</td>

            <asp:UpdatePanel ID="UPN_Creator" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <td>
                        <asp:Label ID="LBL_Titre_Convo" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LBL_Creator" runat="server"></asp:Label>
                    </td>
                </ContentTemplate>
            </asp:UpdatePanel>
            <td>
                Usagers en ligne
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:UpdatePanel ID="UPN_ConvoList" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_ConvoList" style="overflow: auto; border: 2px; border-color: black; border-style: solid; height: 200px; width: 200px; display: inline-block">
                            <asp:Table ID="TB_ConvoList" runat="server" CssClass="Table_Convo">
                            </asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td></td>
            <td class="auto-style1">
                <asp:UpdatePanel ID="UPN_OnlineUsers" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RefreshUsers" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_OnlineUsers" style="overflow: auto; height: 200px;">
                            <asp:Table CssClass="UserTable" GridLines="Both" ID="TB_UserList" runat="server"></asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td class="auto-style1" style="border-style: solid; border-width: medium">
                <asp:UpdatePanel ID="UPN_Chat" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RefreshChat" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_Chat" style="overflow: auto; height: 200px">
                            <asp:Table GridLines="Horizontal" CssClass="Salles" ID="TB_Chat" runat="server">
                            </asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td />
            <td>
                <asp:UpdatePanel ID="UPN_BTN_Send" runat="server" UpdateMode="Conditional">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <asp:TextBox ID="TBX_ChatInput" runat="server" Style="display: none"></asp:TextBox>
                        <textarea rows="2" maxlength="80" onkeyup="document.getElementById('TBX_ChatInput').value = this.value; char = (event.which || event.keyCode); if (char == 13) document.getElementById('BTN_Send').click();"></textarea>
                        <asp:Button ID="BTN_Send" runat="server" Text="Envoyer..." OnClick="BTN_Send_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="BTN_Return" runat="server" Text="Retour..." OnClick="BTN_Return_Click" />
    <script type="text/javascript">

        // It is important to place this JavaScript code after ScriptManager1
        var xPos1, yPos1, xPos2, yPos2, xPos3, yPos3;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('DIV_Chat') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos1 = $get('DIV_Chat').scrollLeft;
                yPos1 = $get('DIV_Chat').scrollTop;
            }

            if ($get('DIV_OnlineUsers') != null) {
                // Get X and Y positions of scrollbar before the partial postback
                xPos2 = $get('DIV_OnlineUsers').scrollLeft;
                yPos2 = $get('DIV_OnlineUsers').scrollTop;
            }

            if ($get('DIV_ConvoList') != null) {

                // Get X and Y positions of scrollbar before the partial postback
                xPos3 = $get('DIV_ConvoList').scrollLeft;
                yPos3 = $get('DIV_ConvoList').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('DIV_Chat') != null) {
                // Set X and Y positions back to the scrollbar  after partial postback
                $get('DIV_Chat').scrollLeft = xPos1;
                $get('DIV_Chat').scrollTop = yPos1;
            }

            if ($get('DIV_OnlineUsers') != null) {
                // Set X and Y positions back to the scrollbar  after partial postback
                $get('DIV_OnlineUsers').scrollLeft = xPos2;
                $get('DIV_OnlineUsers').scrollTop = yPos2;
            }
            if ($get('DIV_ConvoList') != null) {
                // Set X and Y positions back to the scrollbar  after partial postback
                $get('DIV_ConvoList').scrollLeft = xPos3;
                $get('DIV_ConvoList').scrollTop = yPos3;
            }
        }
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
    </script>
</asp:Content>
