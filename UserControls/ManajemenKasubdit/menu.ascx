<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="simlitekkes.UserControls.ManajemenKasubdit.menu" %>
<ul class="metismenu">
    <li class="nav-label">Menu Utama</li>
    <li class="mm-active">
        <asp:LinkButton runat="server" ID="lbBeranda" OnClick="lbBeranda_Click">
            <i class="typcn typcn-home-outline mr-2"></i>Beranda
        </asp:LinkButton>
    </li>
    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Seleksi Reviewer
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbPenetapanReviewerPpm" OnClick="lbPenetapanReviewerPpm_Click">
                    <i class="fas fa-list mr-2"></i>Penetapan Reviewer PPM
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li>
        <asp:LinkButton runat="server" ID="lbLogout" OnClick="lbLogout_Click">
            <i class="typcn typcn-arrow-minimise mr-2"></i>Logout
        </asp:LinkButton>
    </li>
</ul>