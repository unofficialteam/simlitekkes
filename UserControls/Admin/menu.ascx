<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="simlitekkes.UserControls.Admin.menu" %>

<ul class="metismenu">
    <li class="nav-label">Menu Utama</li>
    <li class="mm-active" runat="server" id="menu_beranda">
        <asp:LinkButton runat="server" ID="lbBeranda" OnClick="lbBeranda_Click">
            <i class="typcn typcn-home-outline mr-2"></i>Beranda
        </asp:LinkButton>
    </li>
    <li runat="server" id="menu_referensi">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Referensi
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbPerguruanTinggi" OnClick="lbPerguruanTinggi_Click">
                    <i class="fas fa-list mr-2"></i>Perguruan Tinggi
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbGenerateToken" OnClick="lbGenerateToken_Click">
                    <i class="fas fa-list mr-2"></i>Generate Token
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbProdi" OnClick="lbProdi_Click">
                    <i class="fas fa-list mr-2"></i>Program Studi
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li runat="server" id="menu_rab">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            RAB
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbRabKelompokBiaya" OnClick="lbRabKelompokBiaya_Click">
                    <i class="fas fa-list mr-2"></i>Kelompok Biaya
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbRabKomponenBelanja" OnClick="lbRabKomponenBelanja_Click">
                    <i class="fas fa-list mr-2"></i>Komponen Belanja
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li runat="server" id="menu_referensi_mitra">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Referensi Mitra
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbKategoriMitra" OnClick="lbKategoriMitra_Click">
                    <i class="fas fa-list mr-2"></i>Kategori Mitra
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbMitraWajib" OnClick="lbMitraWajib_Click">
                    <i class="fas fa-list mr-2"></i>Mitra Wajib
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li runat="server" id="menu_referensi_luaran">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Referensi Luaran
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbKategoriJenisLuaran" OnClick="lbKategoriJenisLuaran_Click">
                    <i class="fas fa-list mr-2"></i>Kategori Jenis Luaran
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbJenisLuaran" OnClick="lbJenisLuaran_Click">
                    <i class="fas fa-list mr-2"></i>Jenis Luaran
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbBuktiLuaran" OnClick="lbBuktiLuaran_Click">
                    <i class="fas fa-list mr-2"></i>Bukti Luaran
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbPeranPenulis" OnClick="lbPeranPenulis_Click" >
                    <i class="fas fa-list mr-2"></i>Peran Penulis
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbJenisProsiding" OnClick="lbJenisProsiding_Click">
                    <i class="fas fa-list mr-2"></i>Jenis Prosiding
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbJenisSKDokumen" OnClick="lbJenisSKDokumen_Click">
                    <i class="fas fa-list mr-2"></i>Jenis SK Dokumen
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li runat="server" id="menu_data_pendukung">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Data Pendukung
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbDataPendukungPusat" OnClick="lbDataPendukungPusat_Click">
                    <i class="fas fa-list mr-2"></i>Data Pendukung Pusat
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbDataKlasterPerguruanTinggi" OnClick="lbDataKlasterPerguruanTinggi_Click">
                    <i class="fas fa-list mr-2"></i>Data Klaster Perguruan Tinggi
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbDataKategoriSBK" OnClick="lbDataKategoriSBK_Click">
                    <i class="fas fa-list mr-2"></i>Data Kategori SBK
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbDataPeran" OnClick="lbDataPeran_Click">
                    <i class="fas fa-list mr-2"></i>Data Peran
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li runat="server" id="Li1">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Konfigurasi
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbkelola" OnClick="lbkelola_Click">
                    <i class="fas fa-list mr-2"></i>Pengelolaan Kegiatan
                </asp:LinkButton>
            </li>
<%--            <li>
                <asp:LinkButton runat="server" ID="LinkButton2" OnClick="lbMitraWajib_Click">
                    <i class="fas fa-list mr-2"></i>Mitra Wajib
                </asp:LinkButton>
            </li>--%>
        </ul>
    </li>
    <li runat="server" id="menu_informasi">
        <a class="has-arrow material-ripple" href="#">
            <i class="typcn typcn-beaker mr-2"></i>
            Informasi
        </a>
        <ul class="nav-second-level">
            <li>
                <asp:LinkButton runat="server" ID="lbPengumuman" OnClick="lbPengumuman_Click">
                    <i class="fas fa-list mr-2"></i>Pengumuman
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="lbRunningText" OnClick="lbRunningText_Click">
                    <i class="fas fa-list mr-2"></i>Running Text
                </asp:LinkButton>
            </li>
        </ul>
    </li>
    <li>
        <asp:LinkButton runat="server" ID="lbPersonalPPSDM" OnClick="lbPersonalPPSDM_Click">
            <i class="typcn typcn-group-outline mr-2"></i>Personal Pusdik SDM Kesehatan
        </asp:LinkButton>
    </li>
    <li>
        <asp:LinkButton runat="server" ID="lbLogout" OnClick="lbLogout_Click">
            <i class="typcn typcn-arrow-minimise mr-2"></i>Logout
        </asp:LinkButton>
    </li>
</ul>
