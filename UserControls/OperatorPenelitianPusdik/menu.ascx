<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.menu" ClientIDMode="Static" %>
<ul class="metismenu">
    <li class="nav-label">Menu Utama</li>
    <li class="mm-active" id="menu_beranda" runat="server">
        <asp:LinkButton runat="server" ID="lbBeranda" OnClick="lbBeranda_Click">
            <i class="typcn typcn-home-outline mr-2"></i>Beranda
        </asp:LinkButton>
    </li>
    <li id="menu_penilaian" runat="server">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Proses Penilaian
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbPenugasanReviewer" OnClick="lbPenugasanReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Penugasan Reviewer
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbPlottingReviewer" OnClick="lbPlottingReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Plotting Reviewer
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbPenetapanUsulanBaru" OnClick="lbPenetapanUsulanBaru_Click">
                    <i class="fas fa-list mr-2"></i>Penetapan Usulan
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li id="menu_monitoring" runat="server">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Monitoring Penelitian
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbMonitoringUsulanBaru" OnClick="lbMonitoringUsulanBaru_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Baru
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbMonitoringUsulanLanjutan" OnClick="lbMonitoringUsulanLanjutan_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Lanjutan
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbMonitoringLuaranTambahan" OnClick="lbMonitoringLuaranTambahan_Click">
                    <i class="fas fa-list mr-2"></i>Luaran Tambahan
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbMonitoringPenugasanReviewer" OnClick="lbMonitoringPenugasanReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Penugasan Reviewer
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbMonitoringPlottingReviewer" OnClick="lbMonitoringPlottingReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Plotting Reviewer
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbMonitoringHasilReview" OnClick="lbMonitoringHasilReview_Click">
                    <i class="fas fa-list mr-2"></i>Hasil Review
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbMonitoringMonevEksternal" OnClick="lbMonitoringMonevEksternal_Click">
                    <i class="fas fa-list mr-2"></i>Hasil Monev Eksternal
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbMonitoringPerbaikanPenelitian" OnClick="lbMonitoringPerbaikanPenelitian_Click">
                    <i class="fas fa-list mr-2"></i>Perbaikan Usulan
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbMonitoringLaporanKemajuan" OnClick="lbMonitoringLaporanKemajuan_Click">
                    <i class="fas fa-list mr-2"></i>Laporan Kemajuan
                </asp:LinkButton>
            </li>
             <li>
                <asp:LinkButton runat="server" ID="lbMonitoringLapAkhirTahun" OnClick="lbMonitoringLapAkhirTahun_Click">
                    <i class="fas fa-list mr-2"></i>Laporan Akhir Tahun
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li id="menu_monitoring_pengabdian" runat="server">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Monitoring Pengabdian
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbabdiusulanbaru" OnClick="lbabdiusulanbaru_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Baru
                </asp:LinkButton>
            </li>
<%--            <li>
                <asp:LinkButton runat="server" ID="LinkButton2" OnClick="lbMonitoringUsulanLanjutan_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Lanjutan
                </asp:LinkButton>
            </li>--%>
            <li>
                <asp:LinkButton runat="server" ID="lbabditugasrev" OnClick="lbabditugasrev_Click">
                    <i class="fas fa-list mr-2"></i>Penugasan Reviewer
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbabdiplotting" OnClick="lbabdiplotting_Click">
                    <i class="fas fa-list mr-2"></i>Plotting Reviewer
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbabdihasilreview" OnClick="lbabdihasilreview_Click">
                    <i class="fas fa-list mr-2"></i>Hasil Review
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li id="menu_data_pendukung" runat="server">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Data Pendukung
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbOperatorPT" OnClick="lbOperatorPT_Click">
                    <i class="fas fa-list mr-2"></i>Operator PT
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbDaftarReviewer" OnClick="lbDaftarReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Daftar Reviewer
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbPerubahanPersonil" OnClick="lbPerubahanPersonil_Click">
                    <i class="fas fa-list mr-2"></i>Perubahan Personil <br />dan Judul
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbDaftarPerubahanJudul" OnClick="lbDaftarPerubahanJudul_Click">
                    <i class="fas fa-list mr-2"></i>Daftar Perubahan <br />Judul
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li id="menu_kakas_bantu" runat="server">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Kakas Bantu
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbPengirimanPswdOperatorPT" OnClick="lbPengirimanPswdOperatorPT_Click">
                    <i class="fas fa-list mr-2"></i>Kirim Password
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbEksepsiPengusul" OnClick="lbEksepsiPengusul_Click">
                    <i class="fas fa-list mr-2"></i>Eksepsi Pengusul
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
