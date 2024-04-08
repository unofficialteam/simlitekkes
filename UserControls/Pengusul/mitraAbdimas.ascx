<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraAbdimas" %>

<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasPKM.ascx" TagPrefix="uc" TagName="mitraAbdimasPKM" %>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasPPK.ascx" TagPrefix="uc" TagName="mitraAbdimasPPK" %>
<%--<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasPPUPIK.ascx" TagPrefix="uc" TagName="mitraPPUPIK" %>--%>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasPKW.ascx" TagPrefix="uc" TagName="mitraPKW" %>
<%--<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasKKNPPM.ascx" TagPrefix="uc" TagName="mitraKKNPPM" %>--%>
<%--<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasPKMS.ascx" TagPrefix="uc" TagName="mitraPKMS" %>--%>
<%--<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasPPPUD.ascx" TagPrefix="uc" TagName="mitraPPPUD" %>--%>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasPPDM.ascx" TagPrefix="uc" TagName="mitraPPDM" %>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasPPDS.ascx" TagPrefix="uc" TagName="mitraPPDS" %>
<%--<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraAbdimasPPMUPT.ascx" TagPrefix="uc" TagName="mitraAbdimasPPMUPT" %>--%>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraPelaksanaPPDS.ascx" TagPrefix="uc" TagName="mitraPelaksanaPPDS" %>

<asp:MultiView ID="mvMitra" runat="server">
    <asp:View ID="vPKM" runat="server">
        <uc:mitraAbdimasPKM ID="ucPKM" runat="server"></uc:mitraAbdimasPKM>
    </asp:View>
    <asp:View ID="vPPK" runat="server">
        <uc:mitraAbdimasPPK ID="ucPPK" runat="server"></uc:mitraAbdimasPPK>
    </asp:View>
    <%--<asp:View ID="vPPUPIK" runat="server">
        <uc:mitraPPUPIK ID="ucPPUPIK" runat="server"></uc:mitraPPUPIK>
    </asp:View>--%>
    <asp:View ID="vPKW" runat="server">
        <uc:mitraPKW ID="ucPKW" runat="server"></uc:mitraPKW>
    </asp:View>
    <%--<asp:View ID="vKKNPPM" runat="server">
        <uc:mitraKKNPPM ID="ucKKNPPM" runat="server"></uc:mitraKKNPPM>
    </asp:View>
    <asp:View ID="vPKMS" runat="server">
        <uc:mitraPKMS ID="ucPKMS" runat="server"></uc:mitraPKMS>
    </asp:View>
    <asp:View ID="vPPPUD" runat="server">
        <uc:mitraPPPUD ID="ucPPPUD" runat="server"></uc:mitraPPPUD>
    </asp:View>--%>
    <asp:View ID="vPPDM" runat="server">
        <uc:mitraPPDM ID="ucPPDM" runat="server"></uc:mitraPPDM>
    </asp:View>
    <asp:View ID="vPPDS" runat="server">
        <%--<uc:mitraPPDM ID="ucPPDS" runat="server"></uc:mitraPPDM>--%>
        <uc:mitraAbdimasPKM ID="ucPPDS" runat="server"></uc:mitraAbdimasPKM>
        <uc:mitraPelaksanaPPDS ID="ucPelaksanaPPDS" runat="server" />
    </asp:View>
    <%--<asp:View ID="vPPMUPT" runat="server">
        <uc:mitraAbdimasPPMUPT ID="ucPPMUPT" runat="server"/>
    </asp:View>--%>
</asp:MultiView>