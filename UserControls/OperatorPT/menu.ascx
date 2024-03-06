<%@ control language="C#" autoeventwireup="true" codebehind="menu.ascx.cs" inherits="simlitekkes.UserControls.OperatorPT.menu" %>
<ul class="metismenu">
    <li class="nav-label">Menu Utama</li>
    <li class="mm-active">
        <asp:linkbutton runat="server" id="lbBeranda" onclick="lbBeranda_Click">
            <i class="typcn typcn-home-outline mr-2"></i>Beranda
        </asp:linkbutton>
    </li>
    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Usulan Kegiatan
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:linkbutton runat="server" id="lbRekapUsulanBaru" onclick="lbRekapUsulanBaru_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Baru
                </asp:linkbutton>
                <asp:linkbutton runat="server" id="lbRekapUsulanBaruAbdimas" onclick="lbRekapUsulanBaruAbdimas_Click">
                    <i class="fas fa-list mr-2"></i>Usulan Baru Abdimas
                </asp:linkbutton>
            </li>
        </ul>
    </li>
    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Penilaian
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
            <%--            <li>
                <asp:LinkButton runat="server" ID="lbPlottingReviewer3rd" OnClick="lbPlottingReviewer3rd_Click">
                    <i class="fas fa-list mr-2"></i>Plotting Reviewer (3rd)
                </asp:LinkButton>
            </li>--%>
            <li>
                <asp:linkbutton runat="server" id="lbPenetapanTahapan" onclick="lbPenetapanTahapan_Click">
                    <i class="fas fa-list mr-2"></i>Penetapan Usulan Baru
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbPenetapanUsulanLanjutan" onclick="lbPenetapanUsulanLanjutan_Click">
                    <i class="fas fa-list mr-2"></i>Penetapan Usulan Lanjutan
                </asp:linkbutton>
            </li>
            <%--            <li>
                <asp:LinkButton runat="server" ID="lbHasilReview" OnClick="lbHasilReview_Click">
                    <i class="fas fa-list mr-2"></i>Hasil Review
                </asp:LinkButton>
            </li>--%>
        </ul>
    </li>
    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Monitoring Kegiatan
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringHasilReview" onclick="lbMonitoringHasilReview_Click">
                    <i class="fas fa-list mr-2"></i>Hasil Review
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringLapKemajuan" onclick="lbMonitoringLapKemajuan_Click">
                    <i class="fas fa-list mr-2"></i>Laporan Kemajuan
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringMonevEksternal" onclick="lbMonitoringMonevEksternal_Click">
                    <i class="fas fa-list mr-2"></i>Hasil Monev Eksternal
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringLapAkhirTahun" onclick="lbMonitoringLapAkhirTahun_Click">
                    <i class="fas fa-list mr-2"></i>Laporan Akhir Tahun
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbMonitoringPerbaikanProposal" onclick="lbMonitoringPerbaikanProposal_Click">
                    <i class="fas fa-list mr-2"></i>Perbaikan Proposal
                </asp:linkbutton>
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
                <asp:linkbutton runat="server" id="lbProfilLembaga" onclick="lbProfilLembaga_Click">
                    <i class="fas fa-list mr-2"></i>Profil Lembaga
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbSinkronisasi" onclick="lbSinkronisasi_Click">
                    <i class="fas fa-list mr-2"></i>Sinkronisasi Dosen
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbKelolaTendikNonDosen" onclick="lbKelolaTendikNonDosen_Click">
                    <i class="fas fa-list mr-2"></i>Kelola Tendik Non-Dosen
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbPencarianAkunDosen" onclick="lbPencarianAkunDosen_Click">
                    <i class="fas fa-list mr-2"></i>Pencarian Akun Dosen
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbUnggahDokumenRenstra" onclick="lbUnggahDokumenRenstra_Click">
                    <i class="fas fa-list mr-2"></i>Unggah Dokumen Renstra
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbBidangUnngulanPT" onclick="lbBidangUnngulanPT_Click">
                    <i class="fas fa-list mr-2"></i>Bidang Unggulan PT
                </asp:linkbutton>
            </li>
            <li>
                <asp:linkbutton runat="server" id="lbTopikUnggulanPT" onclick="lbTopikUnggulanPT_Click">
                    <i class="fas fa-list mr-2"></i>Topik Unggulan PT
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
