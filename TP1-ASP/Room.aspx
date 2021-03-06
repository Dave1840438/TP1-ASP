﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master_page.Master" AutoEventWireup="true" CodeBehind="Room.aspx.cs" Inherits="TP1_ASP.Room" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Table
        {
            background-color:lightgray;
            border:1px solid black;
            border-spacing:1px;
            border-collapse: separate;
        }
        td{
            padding:5px;
        }
        .DIV{
            overflow:auto;
            height:350px;
            width:600px;
            margin:auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <hr />
    <asp:Timer runat="server" ID="RefreshUsers" Interval="3000" OnTick="RefreshUsers_Tick"></asp:Timer>
   <div id="DIV_Users" class="DIV" >
     <asp:UpdatePanel ID="UPN_OnlineUsers" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="RefreshUsers" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Table CssClass="Table" GridLines="Both" ID="TB_OnlineUsers" runat="server">
            </asp:Table>
            <br />
        </ContentTemplate>
          </asp:UpdatePanel>
       <asp:Button ID="BTN_Return" runat="server" Text="Retour..." OnClick="BTN_Return_Click" CssClass="Button" />
       </div>
</asp:Content>
