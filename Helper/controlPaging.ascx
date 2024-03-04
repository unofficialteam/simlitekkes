<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="controlPaging.ascx.cs" Inherits="simlitekkes.Helper.controlPaging" %>

<style type="text/css">
    .menu {
        background-color: #F7F6F3;
        height: 30px;
    }

    .menu-hover {
        background-color: #7C6F57;
        color: white;
        padding: 5px 10px;
    }

    .level1 {
        list-style: none;
        padding: 5px 10px;
    }

    .static {        
    }

    .menu-static{
        padding: 5px 10px;
    }

    .selected {
        font-weight: bold;
        background-color: #DD7B5D;
        color: white;
    }

    .highlighted {
        font-weight: bold;
        background-color: #7C6F57;
        color: white;
        padding: 15px 10px;
    }
</style>
<asp:Menu ID="MenuPage" runat="server" Orientation="Horizontal" CssClass="pagination"
    OnMenuItemClick="menu_event">
    <Items >
    </Items>
    <StaticHoverStyle CssClass="menu-hover" />
    <StaticMenuItemStyle CssClass="menu-static" />
    
</asp:Menu>
