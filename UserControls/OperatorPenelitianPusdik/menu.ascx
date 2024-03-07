<%@ control language="C#" autoeventwireup="true" codebehind="menu.ascx.cs" inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.menu" clientidmode="Static" %>
<ul class="metismenu">
    <li class="nav-label">Menu Utama</li>
    <li class="mm-active" id="menu_beranda" runat="server">
        <asp:linkbutton runat="server" id="lbBeranda" onclick="lbBeranda_Click">
            <i class="typcn typcn-home-outline mr-2"></i>Beranda
        </asp:linkbutton>
    </li>
    <li id="menu_penilaian" runat="server">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Proses Penilaian
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:linkbutton runat="server" id="lbPenugasanReviewer" onclick="lbPenugasanReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Penugasan Reviewer
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbPlottingReviewer" onclick="lbPlottingReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Plotting Reviewer
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbPenetapanUsulanBaru" onclick="lbPenetapanUsulanBaru_Click">
                    <i class="fas fa-list mr-2"></i>Penetapan Usulan<br /> Baru
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbPenetapanUsulanLanjutan" onclick="lbPenetapanUsulanLanjutan_Click">
                    <i class="fas fa-list mr-2"></i>Penetapan Usulan<br />Lanjutan
                </asp:linkbutton>
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
                <asp:linkbutton runat="server" id="lbMonitoringUsulanBaru" onclick="lbMonitoringUsulanBaru_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Baru
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringUsulanLanjutan" onclick="lbMonitoringUsulanLanjutan_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Lanjutan
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringLuaranTambahan" onclick="lbMonitoringLuaranTambahan_Click">
                    <i class="fas fa-list mr-2"></i>Luaran Tambahan
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringPenugasanReviewer" onclick="lbMonitoringPenugasanReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Penugasan Reviewer
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringPlottingReviewer" onclick="lbMonitoringPlottingReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Plotting Reviewer
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringHasilReview" onclick="lbMonitoringHasilReview_Click">
                    <i class="fas fa-list mr-2"></i>Hasil Review
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringMonevEksternal" onclick="lbMonitoringMonevEksternal_Click">
                    <i class="fas fa-list mr-2"></i>Hasil Monev Eksternal
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringPerbaikanPenelitian" onclick="lbMonitoringPerbaikanPenelitian_Click">
                    <i class="fas fa-list mr-2"></i>Perbaikan Usulan
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringLaporanKemajuan" onclick="lbMonitoringLaporanKemajuan_Click">
                    <i class="fas fa-list mr-2"></i>Laporan Kemajuan
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringLapAkhirTahun" onclick="lbMonitoringLapAkhirTahun_Click">
                    <i class="fas fa-list mr-2"></i>Laporan Akhir Tahun
                </asp:linkbutton>
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
                <asp:linkbutton runat="server" id="lbabdiusulanbaru" onclick="lbabdiusulanbaru_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Baru
                </asp:linkbutton>
            </li>
            <%--            <li>
                <asp:LinkButton runat="server" ID="LinkButton2" OnClick="lbMonitoringUsulanLanjutan_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Lanjutan
                </asp:LinkButton>
            </li>--%>
            <li>
                <asp:linkbutton runat="server" id="lbabditugasrev" onclick="lbabditugasrev_Click">
                    <i class="fas fa-list mr-2"></i>Penugasan Reviewer
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbabdiplotting" onclick="lbabdiplotting_Click">
                    <i class="fas fa-list mr-2"></i>Plotting Reviewer
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbabdihasilreview" onclick="lbabdihasilreview_Click">
                    <i class="fas fa-list mr-2"></i>Hasil Review
                </asp:linkbutton>
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
                <asp:linkbutton runat="server" id="lbOperatorPT" onclick="lbOperatorPT_Click">
                    <i class="fas fa-list mr-2"></i>Operator PT
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbDaftarReviewer" onclick="lbDaftarReviewer_Click">
                    <i class="fas fa-list mr-2"></i>Daftar Reviewer
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbPerubahanPersonil" onclick="lbPerubahanPersonil_Click">
                    <i class="fas fa-list mr-2"></i>Perubahan Personil
                    <br />
                    dan Judul
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbDaftarPerubahanJudul" onclick="lbDaftarPerubahanJudul_Click">
                    <i class="fas fa-list mr-2"></i>Daftar Perubahan
                    <br />
                    Judul
                </asp:linkbutton>
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
                <asp:linkbutton runat="server" id="lbPengirimanPswdOperatorPT" onclick="lbPengirimanPswdOperatorPT_Click">
                    <i class="fas fa-list mr-2"></i>Kirim Password
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbEksepsiPengusul" onclick="lbEksepsiPengusul_Click">
                    <i class="fas fa-list mr-2"></i>Eksepsi Pengusul
                </asp:linkbutton>
            </li>
        </ul>
    </li>
    <li>
        <asp:linkbutton runat="server" id="lbLogout" onclick="lbLogout_Click">
            <i class="typcn typcn-arrow-minimise mr-2"></i>Logout
        </asp:linkbutton>
    </li>
</ul>
