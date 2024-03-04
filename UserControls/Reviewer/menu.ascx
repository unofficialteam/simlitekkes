<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="simlitekkes.UserControls.Reviewer.menu" %>

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
            Penelitian Baru
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbEvaluasiAdministrasi" OnClick="lbEvaluasiAdministrasi_Click">
                    <i class="fas fa-list mr-2"></i>Evaluasi Administrasi
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbEvaluasiSubstansi" OnClick="lbEvaluasiSubstansi_Click">
                    <i class="fas fa-list mr-2"></i>Evaluasi Substansi
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbEvaluasiPembahasandanVisitasi" OnClick="lbEvaluasiPembahasandanVisitasi_Click">
                    <i class="fas fa-list mr-2"></i>Evaluasi Pembahasan dan Visitasi
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Pengabdian Baru
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbEvaluasiAdministrasiPengabdian" OnClick="lbEvaluasiAdministrasiPengabdian_Click1">
                    <i class="fas fa-list mr-2"></i>Evaluasi Administrasi
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbEvaluasiSubstansiPengabdian" OnClick="lbEvaluasiSubstansiPengabdian_Click1">
                    <i class="fas fa-list mr-2"></i>Evaluasi Substansi
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbEvaluasiPembahasandanVisitasiPengabdian" OnClick="lbEvaluasiPembahasandanVisitasiPengabdian_Click">
                    <i class="fas fa-list mr-2"></i>Evaluasi Pembahasan dan Visitasi
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Pelaksanaan
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbMonevPenelitian" OnClick="lbMonevPenelitian_Click">
                    <i class="fas fa-list mr-2"></i>Monev Penelitian
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbPenilaianLuaran" OnClick="lbPenilaianLuaran_Click">
                    <i class="fas fa-list mr-2"></i>Penilaian Luaran
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
