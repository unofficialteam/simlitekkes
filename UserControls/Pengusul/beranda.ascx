<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="beranda.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.beranda" %>
<%----%>
<%@ Register Src="~/UserControls/Pengusul/identitas.ascx" TagPrefix="uc" TagName="identitas" %>
<%@ Register Src="~/UserControls/Pengusul/sinta.ascx" TagPrefix="uc" TagName="sinta" %>
<%@ Register Src="~/UserControls/Pengusul/riwayatPenelitian.ascx" TagPrefix="uc" TagName="riwayatPenelitian" %>
<%@ Register Src="~/UserControls/Pengusul/riwayatPengabdian.ascx" TagPrefix="uc" TagName="riwayatPengabdian" %>
<%@ Register Src="~/UserControls/Pengusul/artikelJurnal.ascx" TagPrefix="uc" TagName="artikelJurnal" %>
<%@ Register Src="~/UserControls/Pengusul/artikelProsiding.ascx" TagPrefix="uc" TagName="prosiding" %>
<%@ Register Src="~/UserControls/Pengusul/hki.ascx" TagPrefix="uc" TagName="hki" %>
<%@ Register Src="~/UserControls/Pengusul/buku.ascx" TagPrefix="uc" TagName="buku" %>

<!--Content Header (Page header)-->
<div class="content-header row align-items-center m-0">
    <%--<nav aria-label="breadcrumb" class="col-sm-12 order-sm-last mb-3 mb-sm-0 p-0 ">
        <ol class="breadcrumb d-inline-flex font-weight-600 fs-13 bg-white mb-0 float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item active">Dashboard</li>
        </ol>
    </nav>--%>
<%--    <div class="col-sm-8 header-title p-0">
        <div class="media">
            <div class="header-icon text-success mr-3"><i class="typcn typcn-spiral"></i></div>
            <div class="media-body">
                <h1 class="font-weight-bold">Dashboard</h1>
                <small>From now on you will start your activities.</small>
            </div>
        </div>
    </div>--%>
</div>
<!--/.Content Header (Page header)-->

<div class="body-content">
<%--    <div class="row">
        <div class="col-md-12">
            <div class="alert alert-info alert-dismissible">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                <asp:Label runat="server" ID="lblInfoPenetapanReviewerNasional" Text="-"></asp:Label>
            </div>
        </div>
    </div>--%>
    <%--<asp:UpdatePanel ID="upBeranda" runat="server" UpdateMode="Conditional">            
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-9">
                    <asp:MultiView runat="server" ID="mv1">
                        <asp:View runat="server" ID="view0">
                            <uc:identitas runat="server" ID="identitas" />
                        </asp:View>
                        <asp:View runat="server" ID="view1">
                            <uc:sinta runat="server" ID="sinta" />
                        </asp:View>
                        <asp:View runat="server" ID="view2">
                            <uc:riwayatPenelitian runat="server" ID="riwayatPenelitian" />
                        </asp:View>
                        <asp:View runat="server" ID="view3">
                            <uc:riwayatPengabdian runat="server" ID="riwayatPengabdian" />
                        </asp:View>
                        <asp:View runat="server" ID="view4">
                            <uc:artikelJurnal runat="server" ID="artikelJurnal" />
                        </asp:View>
                        <asp:View runat="server" ID="view6">
                            <uc:prosiding runat="server" ID="prosiding" />
                        </asp:View>
                        <asp:View runat="server" ID="view5">
                            <uc:hki runat="server" ID="hki" />
                        </asp:View>
                        <asp:View runat="server" ID="view7">
                            <uc:buku runat="server" ID="buku" />
                        </asp:View>
                    </asp:MultiView>
                </div>
                <div class="col-sm-12 col-md-12 col-lg-3">
                    <div class="card mb-4">
                        <div class="card-body">
                            <div class="dd" id="nestable">
                                <ol class="dd-list">
                                    <li class="dd-item">
                                        <asp:LinkButton ID="menu_identitas_personal" runat="server" CssClass="dd-handle bg-info text-white" OnClick="menu_identitas_personal_Click">
                                            <span class="label bg-green"><i class="fas fa-user-tie"></i></span>IDENTITAS PERSONAL
                                        </asp:LinkButton>
                                    </li>
                                    <li class="dd-item">
                                        <asp:LinkButton ID="menu_sinta" runat="server" OnClick="menu_sinta_Click" CssClass="dd-handle">
                                            <span class="label bg-green"><i class="fas fa-globe"></i></span>SINTA
                                        </asp:LinkButton>
                                    </li>
                                    <li class="dd-item">
                                        <asp:LinkButton ID="menu_penelitian" runat="server" OnClick="menu_penelitian_Click" CssClass="dd-handle">
                                            <span class="label bg-green"><i class="fas fa-search"></i></span>PENELITIAN
                                        </asp:LinkButton>
                                    </li>
                                    <li class="dd-item">
                                        <asp:LinkButton ID="menu_pengabdian" runat="server" OnClick="menu_pengabdian_Click" CssClass="dd-handle">
                                            <span class="label bg-green"><i class="fas fa-search"></i></span>PENGABDIAN
                                        </asp:LinkButton>
                                    </li>
                                    <li class="dd-item">
                                        <asp:LinkButton ID="menu_artikel_jurnal" runat="server" OnClick="menu_artikel_jurnal_Click" CssClass="dd-handle">
                                            <span class="label bg-green"><i class="fas fa-journal-whills"></i></span>ARTIKEL JURNAL
                                        </asp:LinkButton>
                                    </li>
                                    <li class="dd-item">
                                        <asp:LinkButton ID="menu_artikel_prosiding" runat="server" OnClick="menu_artikel_prosiding_Click" CssClass="dd-handle">
                                            <span class="label bg-green"><i class="fas fa-journal-whills"></i></span>ARTIKEL PROSIDING
                                        </asp:LinkButton>
                                    </li>
                                    <li class="dd-item">
                                        <asp:LinkButton ID="menu_karya_intelektual" runat="server" OnClick="menu_karya_intelektual_Click" CssClass="dd-handle">
                                            <span class="label bg-green"><i class="fas fa-brain"></i></span>KARYA INTELEKTUAL
                                        </asp:LinkButton>
                                    </li>
                                    <li class="dd-item">
                                        <asp:LinkButton ID="menu_buku" runat="server" OnClick="menu_buku_Click" CssClass="dd-handle">
                                            <span class="label bg-green"><i class="fas fa-book"></i></span>BUKU
                                        </asp:LinkButton>
                                    </li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
