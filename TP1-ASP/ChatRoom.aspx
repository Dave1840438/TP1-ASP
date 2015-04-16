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

        #TB_Chat td:nth-child(3){
        font-size:10px;
        width:1%;
    white-space:nowrap;
        }

        #TB_Chat td:nth-child(2){
        width:1%;
    white-space:nowrap;
        }

        #TB_Chat td:nth-child(1){
        width:1%;
    white-space:nowrap;
        }

        #TB_Chat td:nth-child(4){
        width:1%;
    white-space:nowrap;
        }

        #TB_Chat td:nth-child(5){
        width:1%;
    white-space:nowrap;
        }

        .auto-style1 {
            height: 49px;
        }

        .TableConvo {
            padding: 0px;
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
        .auto-style7 {
            width: 402px;
            font-weight:bold;
        }
        .auto-style8 {
            height: 49px;
            width: 402px;
        }
        .auto-style9 {
            width: 132px;
        }
        .auto-style10 {
            height: 49px;
            width: 132px;
        }
        .padd
        {
            padding-left:50px;
            white-space: nowrap; 
            overflow: hidden;
            text-overflow: ellipsis; 
        }
        .Table_Convo {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <hr />
    <asp:Timer runat="server" ID="RefreshChat" Interval="3000" OnTick="RefreshChat_Tick"></asp:Timer>
    <asp:Timer runat="server" ID="RefreshUsers" Interval="3000" OnTick="RefreshUsers_Tick"></asp:Timer>
    <table class="MainTable">
        <tr>
            <td><b><u>Liste des conversations :</u></b></td>
            <td class="auto-style9"></td>
            <td class="auto-style7" colspan="2">
                <asp:UpdatePanel ID="UPN_Creator" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:Label ID="LBL_Titre_Convo" runat="server" CssClass="padd" Width="200px"></asp:Label>
                    <asp:Label ID="LBL_Creator" runat="server" CssClass="padd" Width="300px" ></asp:Label>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:UpdatePanel ID="UPN_ConvoList" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_ConvoList" style="overflow: auto; border: thin solid black; height: 200px; width: 278px; display: inline-block; text-align: center;">
                            <asp:Table ID="TB_ConvoList" runat="server" CssClass="Table_Convo" Width="272px">
                            </asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="auto-style10"></td><td class="auto-style8" style="border-style: solid; ">
                <asp:UpdatePanel ID="UPN_Chat" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RefreshChat" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="DIV_Chat" style="overflow: auto; height: 200px; width:765px" >
                            <asp:Table GridLines="Horizontal" CssClass="Salles" ID="TB_Chat" runat="server" Width="746px">
                            </asp:Table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <b><u>Usagers en ligne :</u></b>
                <br />
                <br />
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
            
            <td class="auto-style9"/>
            <td style="vertical-align: top; text-align:center" class="auto-style7">
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
