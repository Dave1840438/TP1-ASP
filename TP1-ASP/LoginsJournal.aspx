﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="LoginsJournal.aspx.cs" Inherits="TP1_ASP.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Table {
            background-color: lightgray;
            border: 1px solid black;
            border-spacing: 1px;
            border-collapse: separate;
        }
        .DIV{
            overflow:auto;
            height:350px;
            width:820px;
            margin:auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <hr />
    <asp:Timer runat="server" ID="RefreshPanel" Interval="3000" OnTick="RefreshPanel_Tick"></asp:Timer>
    <div id="DIV_Users" class="DIV">
        <asp:UpdatePanel runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RefreshPanel" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:Table CssClass="Table" GridLines="Both" ID="TB_Log" runat="server"></asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </div>
    <div class="DIV"><asp:Button ID="BTN_Return" runat="server" Text="Retour..." OnClick="BTN_Return_Click" CssClass="Button" /></div>
    
    
</asp:Content>
