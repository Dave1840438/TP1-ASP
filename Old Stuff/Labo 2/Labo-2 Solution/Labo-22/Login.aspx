<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Labo_22.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="FormStyles.css" />
</head>
<body>
    <h2>Login...</h2>
    <asp:Label ID="LBL_Message" runat="server" Text=""></asp:Label>
    <hr />
    <form id="form1" runat="server">
        <div style="margin: auto; width: 350px; background-color: lightgray; padding: 20px; border: 1px solid black;">
            <table>
                <tr>
                    <td>
                        <asp:Label for="TB_UserName" runat="server" Text="Nom d'usager"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TB_UserName" runat="server" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator 
                            ID="RFV_UserName" 
                            runat="server" 
                            Text="Vide!"
                            ErrorMessage="Le nom de l'usager est vide!" 
                            ControlToValidate="TB_UserName" 
                            ValidationGroup="VG_Login"
                            ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label for="TB_Password" runat="server" Text="Mot de passe"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TB_Password" runat="server" CssClass="TextBox" TextMode="Password"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator 
                            ID="RFV_Password" 
                            runat="server" 
                            Text="Vide!"
                            ErrorMessage="Le mot de passe est vide!" 
                            ControlToValidate="TB_Password" 
                            ValidationGroup="VG_Login"
                            ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="BTN_Login" runat="server" 
                            Text="Soumettre..." 
                            class="submitBTN" 
                            ValidationGroup="VG_Login" OnClick="BTN_Login_Click" 
                            />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style =" text-align:left">
                        <asp:ValidationSummary 
                            ID="VS_Login" runat="server" 
                            HeaderText="Sommaire des erreurs <hr/>" 
                            ValidationGroup="VG_Login" 
                            />
                    </td>
                </tr>
            </table>
        </div>
        <asp:CustomValidator 
            ID="CV_UserName" runat="server" 
            ErrorMessage="Cet usager n'existe pas!" 
            ControlToValidate="TB_UserName" 
            OnServerValidate="CV_UserName_ServerValidate"
            ValidationGroup="VG_Login"
            Display="None"
            > </asp:CustomValidator>

        <asp:CustomValidator 
            ID="CV_Password" runat="server" 
            ErrorMessage="Le mot de passe est incorrect!" 
            ControlToValidate="TB_Password"            
            ValidationGroup="VG_Login"
            Display="None" OnServerValidate="CV_Password_ServerValidate"
            > </asp:CustomValidator>
    </form>
</body>
</html>
