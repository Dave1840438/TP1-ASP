<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListerPersonnes.aspx.cs" Inherits="LABO_1.ListerPersonne" EnableSessionState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        table {
            border-collapse: collapse;
        }

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
      </style></head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
            Liste des personnes&nbsp;&nbsp;
            <asp:Button ID="BTN_Add" runat="server" Text="Ajouter..." PostBackUrl="~/AjouterPersonneV2.aspx" />  
         </h2>
        <hr />
        <asp:Panel id="PN_GridView" runat="server"></asp:Panel>
    </div>
    </form>
</body>
</html>
