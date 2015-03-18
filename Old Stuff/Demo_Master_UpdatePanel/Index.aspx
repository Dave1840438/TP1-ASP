<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Demo_Master_UpdatePanel.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">

    <!-- Horloge qui appelle le délégate TimerUpdateCurrency_Tick à tous les 10000 milisecondes (10 secondes) -->
    <asp:Timer runat="server" ID="TimerUpdateCurrency" Interval="10000" OnTick="TimerUpdateCurrency_Tick"></asp:Timer>

    <!-- Le panel qui sera rafraîchi à chaque trigger déclanché par l'événement Tick du timer TimerUpdateCurrency -->
    <asp:UpdatePanel ID="UPN_Currency" runat="server">

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TimerUpdateCurrency" EventName="Tick" />
        </Triggers>

        <ContentTemplate>
            <div class="main">
            <table>
                <tr> <td colspan="2">Dollars canadiens en </td>  </tr>
                <tr> 
                    <td>US : </td>
                    <td> <asp:Label ID="LBL_Currency_CADtoUSD" runat="server">$ ?</asp:Label></td>
                </tr>
                <tr>
                    <td>Euros : </td>
                    <td> <asp:Label ID="LBL_Currency_CADtoEUR" runat="server">€ ? </asp:Label></td>
                </tr>
                <tr>
                    <td>Livres : </td>
                    <td> <asp:Label ID="LBL_Currency_CADtoGBP" runat="server">£ ?</asp:Label></td>
                </tr>
            </table>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
