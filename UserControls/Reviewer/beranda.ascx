<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="beranda.ascx.cs" Inherits="simlitekkes.UserControls.Reviewer.beranda" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upBerandaOptPt" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <%--<div class="alert alert-primary alert-dismissible">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    <strong><i class="fas fa-info-circle mr-2"></i>Unduh Panduan : </strong>
                    <div class="dd" id="nestable">
                        <ol class="dd-list">
                            <li class="dd-item">
                                <a href="dokumen/panduan/Teknik Penilaian Luaran Penelitian Tahun 2019.pdf" style="width: 100%" class="dd-handle">
                                    <span class="label bg-green"><i class="fas fa-cloud-download-alt"></i></span>Teknik Penilaian Luaran Penelitian Tahun 2019
                                </a>
                            </li>
                            <li class="dd-item">
                                <a class="dd-handle mr-2" href="dokumen/panduan/Teknik Penilaian Monev Penelitian 10 Desember 2019.pdf" style="width: 100%">
                                    <span class="label bg-green"><i class="fas fa-cloud-download-alt"></i></span>Panduan Penilaian Monev Penelitian Tahun 2019
                                </a>
                            </li>
                        </ol>
                    </div>
                </div>--%>
            </div>
            <div class="col-md-12">
                <asp:Panel runat="server" ID="pnl_panduan" Visible="false" CssClass="box-body">
                    <asp:Label ID="Label2" runat="server" Text="Materi :" CssClass="badge badge-info p-2"></asp:Label>
                    <ol class="text-white">
                        <li>
                            <a href="dokumen/panduan/Teknik Penilaian Subtansi (Rekam Jejak, Usulan, dan RAB).pdf" style="width: 100%" class="dd-handle">
                                <span class="label bg-green p-2 mr-2 text-white"><i class="fas fa-cloud-download-alt"></i></span>Materi Persamaan Persepsi Seleksi Substansi Usulan Tahun 2019 Pelaksanaan 2020
                            </a>
                        </li>
                        <li >
                            <a class="dd-handle" href="dokumen/panduan/Jurnal Akreditasi Dikti 2011-2017.pdf" style="width: 100%">
                                <span class="label bg-green p-2 mr-2 text-white"><i class="fas fa-cloud-download-alt"></i></span>Jurnal Akreditasi Dikti 2011-2017
                            </a>
                        </li>
                        <li >
                            <a class="dd-handle" href="dokumen/panduan/Jurnal Akreditasi DIkti 2008-2016 Terbaru.pdf" style="width: 100%">
                                <span class="label bg-green p-2 mr-2 text-white"><i class="fas fa-cloud-download-alt"></i></span>Jurnal Akreditasi DIkti 2008-2016 Terbaru
                            </a>
                        </li>
                        <li style="visibility: hidden">
                            <a class="dd-handle" href="dokumen/panduan/PersamaanPersepsiPMDSU.pptx" style="width: 100%">
                                <span class="label bg-green p-2 mr-2 text-white"><i class="fas fa-cloud-download-alt"></i></span>Materi Persamaan Persepsi PMDSU
                            </a>
                        </li>
                    </ol>
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
