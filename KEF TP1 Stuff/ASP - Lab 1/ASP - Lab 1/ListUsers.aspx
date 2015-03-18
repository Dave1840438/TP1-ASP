<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListUsers.aspx.cs" Inherits="ASP___Lab_1.ListUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        table {
            border-collapse: collapse;
        }
        oh
        table, th, td {
            border: 1px solid black;
            font-family:Arial;
            font-size:12px;
            padding:5px;
        }
        table tr:first-child td {
            color:black;
            background-color:lightgray;
        }
      </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>Liste des usagers inscrits...</h3>
        <hr />
        <asp:Panel ID="PN_ListUsers" runat="server"></asp:Panel>
        <hr />
        <br />
        <asp:Button ID="BTN_GotoInscription" runat="server" Text="Inscription..." OnClick="BTN_GotoInscription_Click" />
    </div>
    </form>
</body>
</html>

