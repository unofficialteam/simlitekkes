<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.menu" %>

<!-- Sidebar Menu-->
<ul class="metismenu">
    <li class="nav-label">Menu Utama</li>
    <li>
        <asp:LinkButton ID="lbBeranda" runat="server" OnClick="lbBeranda_Click">
                    <i class="fa fa-home mr-2"></i>
                    <span>Beranda</span>
        </asp:LinkButton>
    </li>

    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="fa fa-edit mr-2"></i>
            Penelitian
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton ID="lbUsulanBaru" runat="server" OnClick="lbUsulanBaru_Click">
                        <i class="fa fa-chevron-right mr-2"></i>
                        <span>Usulan Baru</span>
                </asp:LinkButton>

            </li>
            <li>
                <asp:LinkButton ID="lbUsulanPendanaan2021" runat="server" OnClick="lbUsulanPendanaan2021_Click">
                        <i class="fa fa-chevron-right mr-2"></i>
                        <span>Usulan Pendanaan 2021</span>
                </asp:LinkButton>
            </li>

            <li>
                <asp:LinkButton ID="lbPerbaikanUsulan" runat="server" OnClick="lbPerbaikanUsulan_Click">
                    <i class="fa fa-chevron-right mr-2"></i>    
                    <span>Perbaikan Usulan</span>
                </asp:LinkButton>
            </li>

            <%--            <li>
                <asp:LinkButton ID="lbUsulanLanjutan" runat="server" OnClick="lbUsulanLanjutan_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Usulan Lanjutan</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbPerbaikanUsulan" runat="server" OnClick="lbPerbaikanUsulan_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Perbaikan Usulan</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbSPTB" runat="server" OnClick="lbSPTB_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;SPTB</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbLaporanKemajuan" runat="server" OnClick="lbLaporanKemajuan_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Laporan Kemajuan</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbLaporanAkhir" runat="server" OnClick="lbLaporanAkhir_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Laporan Akhir</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbArsip" runat="server" OnClick="lbArsip_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Arsip</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbRekapLuaran" runat="server" OnClick="lbRekapLuaran_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Rekap Luaran</span>
                </asp:LinkButton>
            </li>--%>
        </ul>
    </li>
    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="fa fa-edit mr-2"></i>
            Pengabdian
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton ID="lbUsulanBaruPengabdian" runat="server" OnClick="lbUsulanBaruPengabdian_Click">
                        <i class="fa fa-chevron-right mr-2"></i>
                        <span>Usulan baru</span>
                </asp:LinkButton>

            </li>
        </ul>
    </li>

    <%--<li class="treeview">
        <a class="waves-effect waves-dark" href="#!">
            <i class="fa fa-pencil"></i><span>&nbsp;&nbsp;Pengabdian</span><i class="icon-arrow-down"></i>
        </a>
        <ul class="treeview-menu" style="display: none;">
            <li>
                <asp:LinkButton ID="lbUsulanBaruPengabdian" runat="server" OnClick="lbUsulanBaruPengabdian_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Usulan baru</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbUsulanLanjutanPengabdian" runat="server" OnClick="lbUsulanLanjutanPengabdian_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Usulan lanjutan</span>
                </asp:LinkButton>
            </li>
        </ul>
    </li>--%>


    <li>
        <a class="has-arrow material-ripple" href="#">
            <i class="fa fa-book mr-2"></i>Pelaksanaan Kegiatan</a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton ID="lbCatatanHarian" runat="server" OnClick="lbCatatanHarian_Click">
                        <i class="fa fa-chevron-right mr-2"></i>
                        <span>Catatan Harian</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbLaporanKemajuan" runat="server" OnClick="lbLaporanKemajuan_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Laporan Kemajuan</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbLaporanAkhir" runat="server" OnClick="lbLaporanAkhir_Click">
                        <i class="fa fa-chevron-right"></i>
                        <span>&nbsp;&nbsp;Laporan Akhir Tahun</span>
                </asp:LinkButton>
            </li>
            <%--   <li>
                <asp:LinkButton ID="lbPengembalianDana" runat="server" OnClick="lbPengembalianDana_Click">
                        <i class="fa fa-chevron-right mr-2"></i>
                        <span>Pengembalian Dana</span>
                </asp:LinkButton>
            </li>--%>
        </ul>
    </li>
    <li>
        <asp:LinkButton ID="lbRiwayatUsulan" runat="server" OnClick="lbRiwayatUsulan_Click">
                    <i class="fa fa-copy mr-2"></i>
                    <span>Riwayat Usulan</span>
        </asp:LinkButton>
    </li>


    <%--    <li class="treeview">
        <a class="waves-effect waves-dark" href="#!">
            <i class="fa fa-user mr-2"></i><span>Pendaftaran Reviewer</span><i class="icon-arrow-down"></i>
        </a>
        <ul class="treeview-menu" style="display: none;">
            <li>
                <asp:LinkButton ID="lbPendaftaranRevPenelitian" runat="server" OnClick="lbPendaftaranRevPenelitian_Click">
                        <i class="fa fa-chevron-right mr-2"></i>
                        <span>Penelitian (Nasional)</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbPendaftaranRevInternalPTPenelitian" runat="server" OnClick="lbPendaftaranRevInternalPTPenelitian_Click">
                        <i class="fa fa-chevron-right mr-2"></i>
                        <span>Penelitian (Internal PT)</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbPendaftaranRevPPM" runat="server" OnClick="lbPendaftaranRevPPM_Click">
                        <i class="fa fa-chevron-right mr-2"></i>
                        <span>PPM (Nasional)</span>
                </asp:LinkButton>
            </li>
        </ul>
    </li>--%>
    <li>
        <asp:LinkButton ID="lbLogout" runat="server" OnClick="lbLogout_Click">
                <i class="fas fa-sign-out-alt mr-2"></i>
                <span>Logout</span>
        </asp:LinkButton>
    </li>
    <%--    <li>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div class="modal_loading">
                    <div class="text-center">
                        <i class="fa fa-refresh fa-spin fa-2x"></i>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </li>--%>


    <%-- 
    <li class="treeview">
        <a href="#">
            <i class="fa fa-list-ol"></i>
            <span>Usulan</span>
            <i class="fa fa-angle-left pull-right"></i>
        </a>
       <ul class="treeview-menu">
            <li>
                <asp:LinkButton ID="lbPengabdian" runat="server" >
                        <i class="fa fa-sign-out"></i>
                        <span>&nbsp;&nbsp;Pengabdian</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbPenelitian" runat="server" >
                        <i class="fa fa-sign-out"></i>
                        <span>&nbsp;&nbsp;Penelitian</span>
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    --%>
</ul>

<%--
    <ul class="list-unstyled components">
    <li class="active">
        <li></li>

    </li>
    <li>
        <a href="#pageSubmenu" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle">Pages</a>
        <ul class="collapse list-unstyled" id="pageSubmenu">
            <li>
                <a href="#">Page 1</a>
            </li>
            <li>
                <a href="#">Page 2</a>
            </li>
            <li>
                <a href="#">Page 3</a>
            </li>
        </ul>
    </li>
    
</ul>
--%>
