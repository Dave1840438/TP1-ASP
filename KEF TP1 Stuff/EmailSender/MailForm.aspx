<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailForm.aspx.cs" Inherits="EmailSender.MailForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .label {
            text-align: right;
            vertical-align: top;
        }

        .messageBody {
            width: 400px;
            height: 200px;
        }

        .main {
            font-family: Arial;
            padding: 10px;
            width: 500px;
            margin: auto;
            background-color: lightgray;
        }
        .input{
            width:400px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <h2>Email form</h2>
            <hr />
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="LBL_To" runat="server" Text="To"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TB_To" runat="server" Text="nicolaschourot@videotron.ca" CssClass="input"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="LBL_Subject" runat="server" Text="Subject"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TB_Subject" runat="server"  CssClass="input" Text="Test d'envoi de courriel..."></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="LBL_Body" runat="server" Text="Message"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TB_Body" runat="server" TextMode="MultiLine" CssClass="messageBody" Text="Bonjour ceci est un test d'envoi...ne pas répondre à ce courriel..."></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td>
                        <asp:Button ID="BTN_Send" runat="server" text="Send" OnClick="BTN_Send_Click"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
