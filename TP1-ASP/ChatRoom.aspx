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
        .auto-style2 {
            width: 67px;
        }
        .auto-style4 {
            height: 49px;
            width: 282px;
        }
        .auto-style5 {
            width: 282px;
        }
        .auto-style6 {
            width: 67px;
            height: 49px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <hr />
    <asp:Timer runat="server" ID="RefreshChat" Interval="3000" OnTick="RefreshChat_Tick"></asp:Timer>
    <asp:Timer runat="server" ID="RefreshUsers" Interval="3000" OnTick="RefreshUsers_Tick"></asp:Timer>
    <table class="MainTable">
        <tr>
            

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
        </tr>
        <tr>
            <td class="auto-style4">
                Liste des conversations<asp:UpdatePanel ID="UPN_ConvoList" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_ConvoList" style="overflow: auto; border: thin solid black; height: 200px; width: 278px; display: inline-block">
                            <asp:Table ID="TB_ConvoList" runat="server" CssClass="Table_Convo">
                            </asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="auto-style6"></td><td class="auto-style1" style="border-style: solid; ">
                <asp:UpdatePanel ID="UPN_Chat" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RefreshChat" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_Chat" style="overflow: auto; height: 200px; width:600px" >
                            <asp:Table GridLines="Horizontal" CssClass="Salles" ID="TB_Chat" runat="server">
                            </asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                Usagers en ligne
                <asp:UpdatePanel ID="UPN_OnlineUsers" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RefreshUsers" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_OnlineUsers" style="overflow: auto; height: 200px; border-width: thin; border-style: solid;">
                            <asp:Table CssClass="UserTable" GridLines="Both" ID="TB_UserList" runat="server"></asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            
            <td class="auto-style2"/>
            <td style="vertical-align: top; text-align:center"> <%-- ERREUR DE CSS CAVE DE MARDE --%>
                <asp:UpdatePanel ID="UPN_BTN_Send" runat="server" UpdateMode="Conditional">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <asp:TextBox ID="TBX_ChatInput" TextMode="MultiLine" Rows="3" Columns="50" runat="server" onkeyup="char = (event.which || event.keyCode); if (char == 13) document.getElementById('BTN_Send').click();" Height="96px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="BTN_Send" runat="server" Text="Envoyer..." OnClick="BTN_Send_Click" CssClass="Button" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="auto-style5"><asp:Button ID="BTN_Return" runat="server" Text="Retour..." OnClick="BTN_Return_Click" CssClass="Button" /></td>
        </tr>
        
    </table>
    <br />
    
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
