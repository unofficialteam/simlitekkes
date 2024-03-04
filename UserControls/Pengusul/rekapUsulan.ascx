<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rekapUsulan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.rekapUsulan" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkap.ascx" TagName="pdfUsulanLengkap" TagPrefix="uc" %>

<div>
    <!-- Judul -->
    <div class="card mb-4">
        <div class="card-header">
            <div class="d-flex justify-content-between align-items-center">
                <div style="font-weight: bold; color: maroon; font-size: 18px;">
                    USULAN PENELITIAN
                </div>
                <div class="text-right">
                    <h5 class="card-header-text f-right">
                        <asp:Label runat="server" ID="lblInfoPenelitianSbk" Text="Penelitian Terapan (tahun ke 1 dari 3 tahun)"></asp:Label>
                    </h5>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-lg-12">
                    <table style="width: 100%; margin: 10px;">
                        <tr>
                            <td style="width: 70px;">
                                <asp:LinkButton runat="server" ID="lbUnduhPdfDokumen" Text="" ForeColor="Red" OnClick="lbUnduhPdfDokumenLengkap_Click">
                                            <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 60px; "></i>
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblJudul" ForeColor="Blue" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <asp:Label runat="server" ID="lblInfoSkema" Text="" ForeColor="Green" Font-Bold="true"></asp:Label>
                    <asp:Label runat="server" ID="lblInfoThn" Text="" ForeColor="Maroon" Font-Bold="true"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <!-- Identitas Pengusul -  Ketua  -->
    <div class="card mb-4">
        <div class="card-body" style="padding: 10px;">
            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul -  Ketua 
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="2">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblNamaNidnKetua" Text="Atong (9999910122)" Font-Bold="true"></asp:Label>
                                </td>
                                <td><span style="margin-left: 50px;">&nbsp;</span>ID Sinta:
                            <asp:Label runat="server" ID="lblIdSinta" Text="-" ForeColor="Green" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblNamaInstitusiNProdi" Text="Universitas Suralaya - Sistem Informasi"></asp:Label>
                                </td>
                                <td><span style="margin-left: 50px;">&nbsp;</span>Kualifikasi:
                            <asp:Label runat="server" ID="lblJenjangPendidikan" Text="S1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">Alamat Surel:
                            <asp:Label runat="server" ID="lblSurel" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">+
                            Publikasi Artikel Jurnal Internasional:&nbsp;
                            (<asp:Label runat="server" ID="lblJmlJurnalInternasional" ForeColor="Red" Text="1"></asp:Label>)
                            - 
                            Nasional Terakreditasi:&nbsp;
                            (<asp:Label runat="server" ID="lblJmlJurnalNasional" ForeColor="Red" Text="1"></asp:Label>)
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">+
                            Publikasi Artikel Prosiding&nbsp;
                            (<asp:Label runat="server" ID="lblJmlProsiding" ForeColor="Red" Text="1"></asp:Label>)
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">+
                            Kekayaan Intelektual:&nbsp;
                            (<asp:Label runat="server" ID="lblJmlHki" ForeColor="Red" Text="1"></asp:Label>)
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">+
                            Buku:&nbsp;
                            (<asp:Label runat="server" ID="lblJmlBuku" Text="(2)" ForeColor="Red"></asp:Label>)
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </div>
    </div>

    <div class="card mb-4">
        <div class="card-body" style="padding: 10px;">
            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Dokumen Usulan
                    </td>
                </tr>
                <tr>
                    <td>

                        <table>
                            <tr>
                                <td style="color: maroon;" colspan="2"><b>Dokumen usulan</b></td>
                            </tr>
                            <tr valign="top">
                                <td class="auto-style1">
                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok_Click">
                                            <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 50px; "></i>
                                    </asp:LinkButton><br />
                                </td>
                                <td width="400px">

                                    <div>Tgl. Unggah:&nbsp;<asp:Label runat="server" ID="lblTglUnggah" Text="-"></asp:Label></div>
                                    <div>Ukuran file:&nbsp;<asp:Label runat="server" ID="lblUkuranFile" Text="-"></asp:Label></div>


                                </td>
                            </tr>
                        </table>


                    </td>
                </tr>
            </table>
            <asp:Panel runat="server" ID="pnlKekDokumenUsulan" Style="margin-bottom: 30px;">
                <div style="background-color: #ffaaaa;" class="alert alert-light" role="alert">
                    <asp:Label runat="server" ID="lblInfoDokUsulan" Text="Dokumen usulan belum diunggah/Kelompok Makro Riset belum dipilih."></asp:Label>
                </div>
            </asp:Panel>
        </div>
    </div>

    <!-- Anggota Peneliti Dikti  -->
    <div class="card mb-4">
        <div class="card-body" style="padding: 10px;">
            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul - Anggota Peneliti 
                (<asp:Label runat="server" ID="lblJmlAnggota"></asp:Label>) 
                &nbsp;<span style="color: maroon; font-style: italic; font-size: 10px;"> Informasi:
                    <asp:Label runat="server" ID="lblInfoAnggotaMinMax"></asp:Label></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="2">
                            <tr>
                                <td style="width: 350px;">
                                    <table cellpadding="1">
                                        <asp:Repeater ID="rptAnggotaDikti" runat="server" OnItemDataBound="rptAnggota_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lbSetuju">
                                                        <i class="hvr-buzz-out fas fa-check-square" style="font-size: 60px; color: green;"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbTidakSetuju">
                                                        <i class="fa fa-close" style="font-size: 60px; color: red;"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbBelumSetuju">
                                                        <i class="fa fa fa-question-circle" style="font-size: 60px; color: blue;"></i>
                                                        </asp:LinkButton><br />
                                                        <asp:Label runat="server" ID="lblKdSts" Text='<%# Eval("kd_sts_konfirmasi")%>'></asp:Label>
                                                    </td>
                                                    <td align="top"><%# Eval("nama_anggota")%> - (<%# Eval("nidn")%>)<br />
                                                        <%# Eval("nama_institusi")%> - <span style="background-color: yellow; padding: 1px;"><%# Eval("peran_personil")%></span><br />
                                                        Tugas: <i><%# Eval("bidang_tugas")%></i>
                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>


            <!--Anggota peneliti tendik-->
            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul - Anggota Peneliti Tendik
                (<asp:Label runat="server" ID="lblJmlAnggotaTendik"></asp:Label>) 
                <%--&nbsp;<span style="color: maroon; font-style: italic; font-size: 10px;"> Informasi:
                    <asp:Label runat="server" ID="Label2"></asp:Label></span>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="2">
                            <tr>
                                <td style="width: 350px;">
                                    <table cellpadding="1">
                                        <asp:Repeater ID="rptAnggotaTendik" runat="server" OnItemDataBound="rptAnggota_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lbSetuju">
                                                        <i class="hvr-buzz-out fas fa-check-square" style="font-size: 60px; color: green;"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbTidakSetuju">
                                                        <i class="fa fa-close" style="font-size: 60px; color: red;"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbBelumSetuju">
                                                        <i class="fa fa fa-question-circle" style="font-size: 60px; color: blue;"></i>
                                                        </asp:LinkButton><br />
                                                        <asp:Label runat="server" ID="lblKdSts" Text='<%# Eval("kd_sts_konfirmasi")%>'></asp:Label>
                                                    </td>
                                                    <td align="top"><%# Eval("nama_anggota")%> - (<%# Eval("nomor_ktp")%>)<br />
                                                        <%# Eval("nama_institusi")%> - <span style="background-color: yellow; padding: 1px;"><%# Eval("peran_personil")%></span><br />
                                                        Tugas: <i><%# Eval("bidang_tugas")%></i>
                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>


            <!-- Anggota Peneliti Non Dikti  -->
            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul - Anggota Peneliti Non Kemkes (<asp:Label runat="server" ID="lblJmlAnggotaNonDikti"></asp:Label>)
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 350px;">
                                    <table cellpadding="2">
                                        <asp:Repeater ID="rptAnggota_nonDikti" runat="server" OnItemDataBound="rptAnggota_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="top"><span style="color: blue; font-weight: bold;"><%# Eval("nama")%></span> - (<%# Eval("no_ktp")%>)<br />
                                                        <%# Eval("nama_instansi_asal")%> - <span style="background-color: yellow; padding: 1px;"><%# Eval("peran_personil")%></span><br />
                                                        Tugas: <i><%# Eval("bidang_tugas")%></i><br />
                                                        Bidang keahlian: <i><%# Eval("bidang_keahlian")%></i>
                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <asp:Panel runat="server" ID="pnlKekAnggota">
                <div style="background-color: #ffaaaa;" class="alert alert-light" role="alert">
                    <asp:Label runat="server" ID="lbllKekAnggota" Text="Jumlah anggota tidak sesuai kuota/anggota belum melakukan persetujuan"></asp:Label>
                </div>
            </asp:Panel>

        </div>
    </div>


    <!-- Luaran dan Target Capaian  -->

    <div class="card mb-4">
        <div class="card-body" style="padding: 10px;">
            <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                <tr align="center" bgcolor="#cccccc">
                    <th width="650" style="background-color: #ddd; height: 30px; vertical-align: middle; text-align: left;">&nbsp;&nbsp;Luaran dan Target Capaian
                    </th>
                </tr>
            </table>

            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr align="center">
                    <th width="650" style="color: brown; text-align: left;">LUARAN WAJIB
                    </th>
                </tr>
                <asp:Repeater ID="rptLuaranWajib" runat="server" OnItemDataBound="rptLuaranWajib_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td class="trtd" style="text-align: left;">&nbsp;
                            Tahun
                                <asp:Label runat="server" ID="lblThnKe" Text='<%# Eval("tahun_luaran")%>'></asp:Label>:&nbsp;<%# Eval("jenis_luaran")%>;&nbsp;Karya intelektual: <%# Eval("keterangan")%>
                                <span style="background-color: yellow;">(<%# Eval("status_target_capaian")%>)</span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr align="center">
                    <th width="650" style="color: brown; text-align: left;">LUARAN TAMBAHAN
                    </th>
                </tr>
                <asp:Repeater ID="rptLuaranTambahan" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="trtd" style="text-align: left;">&nbsp;
                            Tahun <%# Eval("tahun_luaran")%>:&nbsp;<%# Eval("jenis_luaran")%>;&nbsp;Karya intelektual: <%# Eval("keterangan")%>
                                <span style="background-color: yellow;">(<%# Eval("status_target_capaian")%>)</span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

            <asp:Panel runat="server" ID="pnlKekLuaran" Style="margin-bottom: 30px;">
                <div style="background-color: #ffaaaa;" class="alert alert-light" role="alert">
                    <asp:Label runat="server" ID="lblKekLuaran" Text="Luaran wajib belum diisi dengan lengkap"></asp:Label>
                </div>
            </asp:Panel>
        </div>
    </div>

    <!-- Rencana Anggaran Biaya  -->

    <div class="card mb-4">
        <div class="card-body" style="padding: 10px;">
            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Rencana Anggaran Biaya
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Panel runat="server" ID="pnlThn1">
                                                    Tahun 1: Rp.
                                            <asp:Label runat="server" ID="lblTahun1" Text="0"></asp:Label>
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="pnlThn2">
                                                    Tahun 2: Rp.
                                            <asp:Label runat="server" ID="lblTahun2" Text="0"></asp:Label>
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="pnlThn3">
                                                    Tahun 3: Rp.
                                            <asp:Label runat="server" ID="lblTahun3" Text="0"></asp:Label>

                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="pnlTotal">
                                                    <hr />
                                                    <div style="margin-left: 20px;">
                                                        &nbsp;<asp:Label runat="server" ID="lblTotalDana" Text="Total Rp. 0"></asp:Label>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <div style="margin-left: 50px;">
                                                    Dana pertahun - bidang fokus:
                                            <asp:Label runat="server" ID="lblBidFokus"></asp:Label><br />
                                                    &nbsp;- Minimal: Rp.
                                            <asp:Label runat="server" ID="lblDanaMinimal" />
                                                    &nbsp;- Maksimal: Rp.
                                            <asp:Label runat="server" ID="lblDanaMaksimal" />
                                                </div>


                                                <%--<div style="margin-left: 50px;">
                                                    Dana pertahun - bidang fokus:
                                            <asp:Label runat="server" ID="lblBidFokus"></asp:Label><br />
                                                    &nbsp;- Minimal: Rp.
                                            <asp:Label runat="server" ID="lblDanaMinimal" />
                                                    &nbsp;- Maksimal: Rp.
                                            <asp:Label runat="server" ID="lblDanaMaksimal" />
                                                </div>--%>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <asp:Panel runat="server" ID="pnlKekRab" Style="margin-bottom: 30px;">
                <div style="background-color: #ffaaaa;" class="alert alert-light" role="alert">
                    <asp:Label runat="server" ID="lblKekRab" Text="Rab belum diisi/dana usulan di luar kisaran yang ditentukan."></asp:Label>
                </div>
            </asp:Panel>
        </div>
    </div>


    <!-- WBS -->
    <asp:Panel runat="server" ID="panelWbs" Visible="false">

        <div class="card mb-4">
            <div class="card-body" style="padding: 10px;">
                <table width="100%">
                    <tr>
                        <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Dokumen WBS (Work Breakdown Structure)
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <table>
                                <%--<tr>
                                <td style="color: maroon;" colspan="2"><b>Dokumen WBS</b></td>
                            </tr>--%>
                                <tr valign="top">
                                    <td class="auto-style1">
                                        <asp:LinkButton runat="server" ID="lbUnduhPdfDokWbs" Text="" ForeColor="Red" OnClick="lbUnduhPdfDokWbs_Click">
                                            <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 50px; "></i>
                                        </asp:LinkButton><br />
                                    </td>
                                    <td width="400px">

                                        <div>Tgl. Unggah:&nbsp;<asp:Label runat="server" ID="lblTglUnggahWbs" Text="-"></asp:Label></div>
                                        <div>Ukuran file:&nbsp;<asp:Label runat="server" ID="lblUkuranFileWbs" Text="-"></asp:Label></div>

                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                </table>
                <asp:Panel runat="server" ID="panelKekuranganWbs" Style="margin-bottom: 30px;" Visible="false">
                    <div style="background-color: #ffaaaa;" class="alert alert-light" role="alert">
                        <asp:Label runat="server" ID="Label3" Text="Dokumen WBS belum diunggah"></asp:Label>
                    </div>
                </asp:Panel>
            </div>
        </div>

    </asp:Panel>

    <!-- Dokumen Pendukung / Mitra -->

    <div class="card mb-4">
        <div class="card-body" style="padding: 10px;">
            <asp:Panel runat="server" ID="pnlDokPendukung" Visible="true">
                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <tr align="center">
                        <th style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Dokumen Pendukung
                        </th>
                    </tr>
                </table>
                <div style="padding-bottom: 10px;">
                    <asp:Label ID="lblInfoMitra" runat="server" Text="Pilihan Skema tidak Memiliki Mitra" Font-Size="Large" Font-Bold="true" ForeColor="Orange" Visible="false"></asp:Label>
                    <asp:Panel ID="panelMitraPelaksana" runat="server" Visible="true">
                        <div>
                            <div>
                                Mitra Pelaksana Penelitian&nbsp;<asp:Label ID="lblTdkWajibMitra" runat="server" Text="Tidak Wajib Ada"
                                    Font-Bold="true" Font-Size="X-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                            </div>
                            <asp:GridView ID="gvMitraPelaksanaPenelitian" runat="server" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False" DataKeyNames="id_mitra, nama_mitra"
                                OnRowDataBound="gvMitraPelaksanaPenelitian_RowDataBound" OnRowCommand="gvMitraPelaksanaPenelitian_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mitra">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("nama_mitra") %>' ForeColor="DarkBlue"></asp:Label></b>  -  
                                                                <asp:Label ID="lblSurel" runat="server" Text='<%# Bind("surel") %>'></asp:Label><br>
                                            <asp:Label ID="lblInstitusiMitraPenelitian" runat="server" Text='<%# Bind("nama_institusi_mitra") %>'></asp:Label><br />
                                            <asp:LinkButton runat="server" ID="lbUnduhDokMitraPelaksanaPenelitian"
                                                ForeColor="Red" data-toggle="tooltip" CommandName="unduhDokumenMitraPelaksana"
                                                CommandArgument="<%# Container.DataItemIndex %>"><i class="hvr-buzz-out far fa-file-pdf fa-2x"></i>
                                            </asp:LinkButton>
                                            Tgl.Unggah Data:
                                                                     <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label>
                                            <%-- <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'>
                                                                    </asp:Label> --%>
                                            <br />
                                            <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPelaksana"
                                                CommandArgument="<%# Container.DataItemIndex %>">Unduh</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="68%" />
                                        <ItemStyle Font-Bold="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:Rp"></asp:Label>
                                            <asp:Label ID="lblDana1" runat="server" Text='<%# Bind("dana_thn_1", "{0:0,00}") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:Rp"></asp:Label>
                                            <asp:Label ID="lblDana2" runat="server" Text='<%# Bind("dana_thn_2", "{0:0,00}") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:Rp"></asp:Label>
                                            <asp:Label ID="lblDana3" runat="server" Text='<%# Bind("dana_thn_3", "{0:0,00}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="false" />
                                    </asp:TemplateField>
                                </Columns>
                                <%-- <EmptyDataTemplate>
                            <div style="min-height: 100px; margin: 0 auto;">
                                <strong>DATA TIDAK DITEMUKAN</strong>
                            </div>
                        </EmptyDataTemplate>--%>
                            </asp:GridView>

                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panelMitraCalonPengguna" runat="server" Visible="true">
                        <div>
                            <div>
                                Mitra Calon Pengguna<asp:Label ID="lblWajibMCP" runat="server" Text="Wajib Ada" Font-Size="X-Small" Font-Bold="true" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                            </div>

                            <asp:GridView ID="gvMitraCalonPengguna" runat="server" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False" DataKeyNames="id_mitra, nama_mitra"
                                OnRowDataBound="gvMitraCalonPengguna_RowDataBound" OnRowCommand="gvMitraCalonPengguna_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mitra">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("nama_mitra") %>' ForeColor="DarkBlue"></asp:Label></b>  -
                                                                <asp:Label ID="lblSurel" runat="server" Text='<%# Bind("surel") %>'></asp:Label><br>
                                            <asp:Label ID="lblInstitusiMitraPenelitian" runat="server" Text='<%# Bind("nama_institusi_mitra") %>'></asp:Label><br />
                                            <asp:LinkButton runat="server" ID="lbUnduhDokMitraCalonPengguna"
                                                ForeColor="Red" data-toggle="tooltip" CommandName="unduhDokumenMitraPelaksana"
                                                CommandArgument="<%# Container.DataItemIndex %>"><i class="hvr-buzz-out far fa-file-pdf fa-2x"></i>
                                            </asp:LinkButton>
                                            Tgl.Unggah Data:
                                                                     <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label>
                                            <br />
                                            <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPelaksana"
                                                CommandArgument="<%# Container.DataItemIndex %>">Unduh</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="68%" />
                                        <ItemStyle Font-Bold="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:Rp"></asp:Label>
                                            <asp:Label ID="lblDana1" runat="server" Text='<%# Bind("dana_thn_1", "{0:0,00}") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:Rp"></asp:Label>
                                            <asp:Label ID="lblDana2" runat="server" Text='<%# Bind("dana_thn_2", "{0:0,00}") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:Rp"></asp:Label>
                                            <asp:Label ID="lblDana3" runat="server" Text='<%# Bind("dana_thn_3", "{0:0,00}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="false" />
                                    </asp:TemplateField>
                                </Columns>
                                <%--<EmptyDataTemplate>
                            <div style="min-height: 100px; margin: 0 auto;">
                                <strong>DATA TIDAK DITEMUKAN</strong>
                            </div>
                        </EmptyDataTemplate>--%>
                            </asp:GridView>

                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panelMitraInvestor" runat="server" Visible="true">
                        <div >
                            <div>
                                Mitra Investor&nbsp;<asp:Label ID="lblWajibMI" runat="server" Text="Wajib Ada" Font-Size="XX-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                            </div>
                            <asp:GridView ID="gvMitraInvestor" runat="server" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False" DataKeyNames="id_mitra, nama_mitra" OnRowDataBound="gvMitraInvestor_RowDataBound" OnRowCommand="gvMitraInvestor_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mitra">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("nama_mitra") %>' ForeColor="DarkBlue"></asp:Label></b> -
                                                                <asp:Label ID="lblSurel" runat="server" Text='<%# Bind("surel") %>'></asp:Label><br>
                                            <asp:Label ID="lblInstitusiMitraPenelitian" runat="server" Text='<%# Bind("nama_institusi_mitra") %>'></asp:Label><br />
                                            <asp:LinkButton runat="server" ID="lbUnduhDokMitraInvestorPenelitian"
                                                ForeColor="Red" data-toggle="tooltip" CommandName="unduhDokumenMitraPelaksana"
                                                CommandArgument="<%# Container.DataItemIndex %>"><i class="hvr-buzz-out far fa-file-pdf fa-2x"></i>
                                            </asp:LinkButton>
                                            Tgl.Unggah Data: 
                                                                     <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label>
                                            <%--  <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label>
                                            --%><br />
                                            <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPelaksana"
                                                CommandArgument="<%# Container.DataItemIndex %>">Unduh</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="68%" />
                                        <ItemStyle Font-Bold="false" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:Rp"></asp:Label>
                                            <asp:Label ID="lblDana1" runat="server" Text='<%# Bind("dana_thn_1", "{0:0,00}") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:Rp"></asp:Label>
                                            <asp:Label ID="lblDana2" runat="server" Text='<%# Bind("dana_thn_2", "{0:0,00}") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:Rp"></asp:Label>
                                            <asp:Label ID="lblDana3" runat="server" Text='<%# Bind("dana_thn_3", "{0:0,00}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="false" />
                                    </asp:TemplateField>
                                </Columns>
                                <%--<EmptyDataTemplate>
                                    <div style="min-height: 100px; margin: 0 auto;">
                                        <strong>DATA TIDAK DITEMUKAN</strong>
                                    </div>
                                </EmptyDataTemplate>--%>
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlKekMitra">

                <div class="row">
                    <div class="col-lg-12">
                        <div style="background-color: #ffaaaa;" class="alert alert-light" role="alert">
                            <asp:Label runat="server" ID="lblKekMitra" Text="Mitra wajib belum diisi/dokumen mitra belum diunggah"></asp:Label>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>


</div>



<div>
    <uc:pdfUsulanLengkap runat="server" ID="pdfUsulanLengkap" Visible="false" />
</div>




