<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Inscription.aspx.cs" Inherits="TP1_ASP.Inscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .thumbnail {
        }

        .auto-style1 {
            width: 257px;
        }

        .auto-style2 {
            width: 136px;
        }

        .auto-style3 {
            width: 13px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <hr />

    <asp:Panel ID="MainPanel" runat="server" CssClass="MainPanel" Width="435px">
        <asp:CustomValidator ID="UserNameExists" runat="server" ErrorMessage="Le nom d'usager existe déjà!" Text="!"
                        ControlToValidate="TBX_Username" OnServerValidate="UserNameExists_ServerValidate" ValidateEmptyText="true" />
        <table style="border: 2px solid black">
            <tr>
                <td>
                    <asp:Label ID="FullName" runat="server" Text="Nom Complet:" CssClass="Label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="TBX_NomComplet" runat="server" CssClass="Textbox"></asp:TextBox></td>
                <td class="auto-style3">
                    <asp:CustomValidator ID="CVal_Name" runat="server" ErrorMessage="Nom complet ne peut être vide!" Text="!"
                        ControlToValidate="TBX_UserName" OnServerValidate="CV_UsernameIsEmpty" ValidateEmptyText="true" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="UserName" runat="server" Text="Nom d'Usager:" CssClass="Label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="TBX_Username" runat="server" CssClass="Textbox"></asp:TextBox></td>
                <td class="auto-style3">
                    <asp:CustomValidator ID="CVal_Username" runat="server" ErrorMessage="Nom d'usager ne peut être vide!" Text="!"
                        ControlToValidate="TBX_Username" OnServerValidate="CV_UsernameIsEmpty" ValidateEmptyText="true" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Password" runat="server" Text="Mot de Passe:" CssClass="Label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="TBX_Password" runat="server" CssClass="Textbox" type="password"></asp:TextBox></td>
                <td class="auto-style3">
                    <asp:CustomValidator ID="CVal_EmptyPassword" runat="server" ErrorMessage="Le motde passe ne peut pas être vide!" Text="!"
                        ControlToValidate="TBX_Password" OnServerValidate="CV_PasswordIsEmpty" ValidateEmptyText="True" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Password_Confirm" runat="server" Text="Confirmation du Mot de Passe:" CssClass="Label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="TBX_ConfirmPassword" runat="server" CssClass="Textbox" type="password"></asp:TextBox></td>
                <td class="auto-style3">
                    <asp:CustomValidator ID="CVal_PasswordsMatch" runat="server" ErrorMessage="Les mots de passe ne correspondent pas!" Text="!"
                        ControlToValidate="TBX_ConfirmPassword" OnServerValidate="CV_PasswordsMatch" ValidateEmptyText="true" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Email" runat="server" Text="Adresse Courriel:" CssClass="Label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="TBX_Email" runat="server" CssClass="Textbox"></asp:TextBox></td>
                <td class="auto-style3">
                    <asp:CustomValidator ID="CVal_EmptyEmail" runat="server" ErrorMessage="L'email ne peut pas être vide!" Text="!"
                        ControlToValidate="TBX_Email" OnServerValidate="CV_EmailIsEmpty" ValidateEmptyText="True" />
                    <asp:RegularExpressionValidator  ErrorMessage="L'adresse de courriel est syntaxiquement invalide!" display="None" ID="RegExEmail" runat="server" ControlToValidate="TBX_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Email_Confirm" runat="server" Text="Confirmation du courriel:" CssClass="Label"></asp:Label></td>
                <td>
                    <asp:TextBox ID="TBX_ConfirmEmail" runat="server" CssClass="Textbox"></asp:TextBox></td>
                <td class="auto-style3">
                    <asp:CustomValidator ID="CVal_EmailsMatch" runat="server" ErrorMessage="Les emails ne correspondent pas!" Text="!"
                        ControlToValidate="TBX_ConfirmEmail" OnServerValidate="CV_EmailsMatch" ValidateEmptyText="true" /></td>
            </tr>
            <tr>
                <td rowspan="3">
                    <asp:Label ID="Avatar" runat="server" Text="Avatar:" CssClass="Label" Width="124px"></asp:Label></td>
                <td class="auto-style2">
                    <asp:Image ID="IMG_Avatar" runat="server" CssClass="thumbnail" ImageUrl="~/Images/ADD.png" Height="120px" Width="133px" Style="margin: 8px" />
                    <asp:FileUpload ID="FU_Avatar" runat="server" onchange="PreLoadImage();" Style="margin-bottom: 2px" /></td>
                <td class="auto-style3">
                    <asp:CustomValidator ID="CVal_Avatar" runat="server" ErrorMessage="L'avatar n'a pas été choisi!" Text="!"
                        ControlToValidate="FU_Avatar" OnServerValidate="CV_AvatarIsChosen" ValidateEmptyText="True" />
                </td>
            </tr>
        </table>
        <br />

        <div>
            <table style="border: 2px solid black">
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="PN_Captcha" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="RegenarateCaptcha" runat="server"
                                                ImageUrl="~/Images/RegenerateCaptcha.png"
                                                CausesValidation="False"
                                                OnClick="RegenarateCaptcha_Click"
                                                Width="48"
                                                ToolTip="Regénérer le captcha..." />
                                        </td>
                                        <td>
                                            <asp:Image ID="IMGCaptcha" ImageUrl="~/captcha.png" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:CustomValidator ID="CVal_Captcha" runat="server" ErrorMessage="Code captcha incorrect!" Text="!"
                            ControlToValidate="TBX_Captcha" OnServerValidate="CV_Captcha_ServerValidate" ValidateEmptyText="True" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="TBX_Captcha" runat="server" MaxLength="5"></asp:TextBox>

                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Button ID="BTT_Inscription" runat="server" Text="Inscription" CssClass="Button" OnClick="BTT_Inscription_Click" />
        <asp:Button ID="BTT_Annuler" runat="server" Text="Annuler" CssClass="Button" OnClick="BTT_Annuler_Click" />
        <br />
        <asp:ValidationSummary ID="Subscrive_Validation" runat="server" />

    </asp:Panel>
</asp:Content>
