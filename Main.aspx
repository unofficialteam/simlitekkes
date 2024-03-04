<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="simlitekkes.Main1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .level1 .page-item {
            padding: 5px 10px!important;
            color: black;
        }
        .level1 .page-item:hover {
            background-color: gray;
            color: white!important;
        }

        a.level1.page-item.selected.static {
            background-color: #37a000 !important;
            color: white!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="phContentForm" runat="server"></asp:PlaceHolder>
</asp:Content>
