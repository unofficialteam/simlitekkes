<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.menuAbdimas" %>
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
            Monitoring
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbRekapUsulanBaru" OnClick="lbRekapUsulanBaru_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Baru
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Data Pendukung
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbSinkronisasi" OnClick="lbSinkronisasi_Click">
                    <i class="fas fa-list mr-2"></i>Sinkronisasi Dosen
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbPencarianAkunDosen" OnClick="lbPencarianAkunDosen_Click">
                    <i class="fas fa-list mr-2"></i>Pencarian Akun Dosen
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