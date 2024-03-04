<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rekapUsulanAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.rekapUsulanAbdimas" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkapAbdimas.ascx" TagName="pdfUsulanLengkap" TagPrefix="uc" %>

<div>
    <div class="card mb-4">
        <div class="card-header">
            <div class="d-flex justify-content-between align-items-center">
                <div style="font-weight: bold; color: maroon; font-size: 18px;">
                    USULAN PENGABDIAN
                </div>
                <div class="text-right">
                    <h5 class="card-header-text f-right">
                        <asp:Label runat="server" ID="lblInfoAtUnggahDokUsulan" Text="Penelitian terapan (tahun ke-1 dari 3 tahun)"></asp:Label>
                    </h5>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <table style="width: 100%; margin: 10px;">
                        <tr>
                            <td style="width: 70px;">
                                <asp:LinkButton runat="server" ID="lbUnduhPdfDokumen" Text="" ForeColor="Red" OnClick="lbUnduhPdfDokumenLengkap_Click"> <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 60px; "></i></asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblJudul" Font-Bold="true" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
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
            <div class="row">
                <div class="col-lg-12">
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
                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok_Click"> <i class="far fa-file-pdf" style="font-size: 50px; "></i></asp:LinkButton><br />
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
                    <asp:Panel runat="server" ID="pnlKekDokumenUsulan" Style="margin-bottom: 30px; margin-top: 30px;">
                        <div style="background-color: #ffaaaa;" class="alert alert-light" role="alert">
                            <asp:Label runat="server" ID="lblInfoDokUsulan" Text="Dokumen usulan belum diunggah"></asp:Label>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <!-- Anggota Peneliti Dikti  -->
    <div class="card mb-4">
        <div class="card-body" style="padding: 10px;">
            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul - Anggota  
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
                                                        <i class="fa fa-check-square-o" style="font-size: 60px; color: green;"></i>
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
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul - Anggota Pengabdi Tendik
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
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul - Anggota Pengabdi Non Kemkes (<asp:Label runat="server" ID="lblJmlAnggotaNonDikti"></asp:Label>)
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
            <div class="row">
                <div class="col-lg-12">

                    <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                        <tr align="center" bgcolor="#cccccc">
                            <th width="650" style="background-color: #ddd; height: 30px; vertical-align: middle; text-align: left;">&nbsp;&nbsp;Luaran dan Target Capaian
                            </th>
                        </tr>
                    </table>

                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr align="left">
                            <th width="650" style="color: brown;">LUARAN WAJIB
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvluaranwajib" runat="server" GridLines="None" Width="100%"
                                    CssClass="table table-hover"
                                    DataKeyNames="id_luaran_dijanjikan,kd_kategori_jenis_luaran,id_jenis_luaran,id_target_capaian_luaran,tahun_ke,nama_target_capaian_luaran,arr_kd_kategori_jenis_luaran,arr_nama_kategori_jenis_luaran"
                                    ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowDataBound="gvluaranwajib_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                Tahun
                                                        <asp:Label ID="lblTahunke" runat="server" Text='<%# Bind("tahun_ke") %>'></asp:Label>:
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%# Eval("id_kategori_sbk")%>.&nbsp;<asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_kategori_jenis_luaran") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblnamaJenisluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>' Style="margin-left: 20px;"></asp:Label>
                                                <asp:Label ID="lbltargetluaran" runat="server" BackColor="Yellow" Font-Bold="true" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblketerangan" runat="server" Font-Italic="True" Text='<%# Bind("keterangan") %>' Style="margin-left: 20px;"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;">
                                            <strong>DATA TIDAK DITEMUKAN</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <%--<asp:Repeater ID="rptLuaranWajib" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="trtd" style="text-align: left;">&nbsp;
                            Tahun ke-<%# Eval("tahun_ke")%><br /><asp:Label ID="lblkategori" runat="server" Font-Italic="true" Text='<%# Bind("kategori") %>'></asp:Label><br />
                                                    <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label>
                                                    <asp:Label ID="lbltargetluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_target_capaian_luaran") %>' BackColor="Yellow"></asp:Label><br />
                                                    <asp:Label ID="lblketerangan" runat="server" Text='<%# Bind("keterangan") %>' Font-Italic="True"></asp:Label>
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>--%>
                            </td>
                        </tr>
                    </table>

                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr align="left">
                            <th width="650" style="color: brown;">LUARAN TAMBAHAN
                            </th>
                        </tr>
                        <tr>
                            <td>

                                <asp:GridView ID="gvluarantambahan" runat="server" GridLines="None"
                                    CssClass="table table-hover" Font-Size="Small" Width="100%"
                                    DataKeyNames="id_luaran_dijanjikan,tahun_ke,nama_target_capaian_luaran,id_jenis_luaran,kd_kategori_jenis_luaran"
                                    ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowDataBound="gvluarantambahan_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                Tahun
                                                        <asp:Label ID="lblTahunke" runat="server" Text='<%# Bind("tahun_ke") %>'></asp:Label>:
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label>
                                                <asp:Label ID="lbltargetluaran" runat="server" BackColor="Yellow" Font-Bold="true" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblketerangan" runat="server" Font-Italic="true" Text='<%# Bind("keterangan") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;">
                                            <strong>DATA TIDAK DITEMUKAN</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <%--<asp:Repeater ID="rptLuaranTambahan" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="trtd" style="text-align: left;">&nbsp;
                            Tahun <%# Eval("tahun_ke")%><br />
                                
                                
                                                    <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label>
                                                    <asp:Label ID="lbltargetluaran" runat="server" Font-Bold="true" BackColor="Yellow" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label><br />
                                                    <asp:Label ID="lblketerangan" runat="server" Font-Italic="true" Text='<%# Bind("keterangan") %>'></asp:Label>
                                                
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>--%>
                    
                            </td>
                        </tr>
                    </table>

                    <asp:Panel runat="server" ID="pnlKekLuaran" Style="margin-bottom: 30px;">
                        <div style="background-color: #ffaaaa;" class="alert alert-light" role="alert">
                            <asp:Label runat="server" ID="lblKekLuaran" Text="Luaran wajib belum diisi dengan lengkap."></asp:Label>
                        </div>
                    </asp:Panel>
                </div>
            </div>
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
                                                    Dana pertahun:<br />
                                                    &nbsp;- Minimal: Rp.
                                                    <asp:Label runat="server" ID="lblDanaMinimal" />
                                                    &nbsp;- Maksimal: Rp.
                                                    <asp:Label runat="server" ID="lblDanaMaksimal" />
                                                </div>
                                                <%--<div style="margin-left: 50px;">
                                                    Dana pertahun:<br />
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
                    <asp:Label runat="server" ID="lblKekRab" Text="RAB belum diisi/dana usulan di luar kisaran yang ditentukan."></asp:Label>
                </div>
            </asp:Panel>
        </div>
    </div>

    <!-- Dokumen Pendukung / Mitra -->

    <div class="card">
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
                    <asp:Panel ID="panelMitra" runat="server" Visible="true">
                        <div>
                            <div>
                                Mitra&nbsp;<asp:Label ID="lblTdkWajibMitra" runat="server" Text="Tidak Wajib Ada"
                                    Font-Bold="true" Font-Size="X-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                            </div>
                            <asp:GridView ID="gvMitraAbdimas" runat="server" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False" DataKeyNames="id_mitra_abdimas, nama_pimpinan_mitra,kd_sts_surat_pernyataan" OnRowDataBound="gvMitraAbdimas_RowDataBound" OnRowCommand="gvMitraAbdimas_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblKategoriMitraPenelitian" runat="server" Text='<%# Bind("kategori_mitra") %>' ForeColor="Maroon"></asp:Label></b><br />
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("tipe_mitra") %>' ForeColor="Maroon"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="23%" />
                                        <ItemStyle Font-Bold="false" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                           <%-- <b>
                                                <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("tipe_mitra") %>' ForeColor="DarkBlue"></asp:Label></b><br />--%>
                                            <asp:LinkButton runat="server" ID="lbUnduhIcon"
                                                ForeColor="Red" data-toggle="tooltip" CommandName="unduhDokumenMitra"
                                                CommandArgument="<%# Container.DataItemIndex %>">
                                                <i class="far fa-file-pdf fa-2x"></i>
                                            </asp:LinkButton>
                                            Tgl.Unggah Data:
                                            <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                        <HeaderStyle Width="30%" />
                                        <ItemStyle Font-Bold="false" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <%# Eval("nama_pimpinan_mitra") %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="25%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDanaThn1" runat="server" Text=" Rp "></asp:Label>
                                            <asp:Label ID="lblDana1" runat="server" Text='<%# Eval("dana_thn_1", "{0:0,00}") %>'></asp:Label><br>
                                            
                                            <div style="display: none;">
                                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:Rp"></asp:Label>
                                            <asp:Label ID="lblDana2" runat="server" Text='<%# Eval("dana_thn_2", "{0:0,00}") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:Rp"></asp:Label>
                                            <asp:Label ID="lblDana3" runat="server" Text='<%# Eval("dana_thn_3", "{0:0,00}") %>'></asp:Label>
                                                </div>
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
                            <asp:GridView ID="gvKelMasyarakat" runat="server"
                                GridLines="None" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False"
                                DataKeyNames="id_mitra_abdimas, nama_pimpinan_mitra, nama_desa,tgl_unggah_pernyataan,id_mitra_abdimas_11,id_mitra_abdimas_12, id_mitra_abdimas_21, id_mitra_abdimas_22, id_mitra_abdimas_31, id_mitra_abdimas_32" OnRowDataBound="gvKelMasyarakat_RowDataBound">

                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbnNomor" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Kategori Mitra">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblKategoriMitraPenelitian" runat="server" Text='<%# Bind("kategori_mitra") %>' ForeColor="Maroon"></asp:Label></b>
                                        
                                        
                                        <br />

                                            <hr>
                                            <b>Kelompok Sasaran</b><br />
                                            <asp:Label ID="lbnama11" runat="server" Text='<%# Bind("nama_pimpinan_mitra_11") %>'></asp:Label>
                                            <br />
                                            <%--<asp:LinkButton ID="lbunggah11" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah11" data-toggle="tooltip"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>--%>
                                                            Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh11" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_11").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                            &nbsp;
                                                                <asp:LinkButton ID="lbUnduh11" runat="server" CommandName="unduh11"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White"><i class="far fa-file-pdf fa-2x"></i></asp:LinkButton>
                                            <br />
                                            <asp:Label ID="lbnama12" runat="server" Text='<%# Bind("nama_pimpinan_mitra_12") %>'></asp:Label>
                                            <br />
                                        
                                        </ItemTemplate>
                                        <HeaderStyle Width="25%" />
                                        <ItemStyle Font-Bold="false" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dokumen">
                                        <ItemTemplate>
                                            <%--<b>
                                                <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("tipe_mitra") %>' ForeColor="DarkBlue"></asp:Label></b><br />--%>
                                            <asp:LinkButton runat="server" ID="lbUnduh"
                                                ForeColor="Gray" data-toggle="tooltip" CommandName="unduhDokumenMitra"
                                                CommandArgument="<%# Container.DataItemIndex %>">
                                                <i class="far fa-file-pdf fa-2x"></i>
                                            </asp:LinkButton>
                                            Tgl.Unggah Data:
                                            <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                        <HeaderStyle Width="30%" />
                                        <ItemStyle Font-Bold="false" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mitra">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblNamaMitraPengabdian" runat="server" ForeColor="DarkBlue" Text='<%# Bind("nama_pimpinan_mitra") %>'></asp:Label>
                                            </b>
                                            <br />
                                            <asp:Label ID="lblInstitusiMitraPengabdian" runat="server" Text='<%# Bind("nama_desa") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                        <HeaderStyle Width="28%" />
                                        <ItemStyle Font-Bold="false" VerticalAlign="Top"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:" Visible="false"></asp:Label>
                                            <asp:Label ID="lblDana1" runat="server" Text='<%# Bind("dana_tahun_1","{0:C0}") %>'></asp:Label>
                                            <%--<br />
                                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:"></asp:Label>
                                            <asp:Label ID="lblDana2" runat="server" Text='<%# Bind("dana_tahun_2","{0:C0}") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:"></asp:Label>
                                            <asp:Label ID="lblDana3" runat="server" Text='<%# Bind("dana_tahun_3","{0:C0}") %>'></asp:Label>
                                            <br>
                                            <br />
                                            <br>
                                            <br />--%>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="false" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbUbah" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="update" CssClass="fa fa-edit" Font-Bold="true" Font-Size="XX-Large" ForeColor="Black" ToolTip="Ubah" Visible="true">
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbHapus" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="delete" CssClass="fa fa-trash-o" Font-Bold="true" Font-Size="XX-Large" ForeColor="Black" ToolTip="Hapus" Visible="true">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="min-height: 100px; margin: 0 auto;">
                                        <strong>DATA TIDAK DITEMUKAN</strong>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>


                            <asp:GridView ID="gvKelMasyarakatPpmUpt" runat="server" Width="100%" GridLines="None" ShowHeader="true"
                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" CellPadding="20"
                                DataKeyNames="id_mitra_abdimas, nama_organisasi_institusi" OnRowDataBound="gvKelMasyarakatPpmUpt_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mitra">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNamaMitraPengabdian" runat="server" Text='<%# Eval("nama_pimpinan_mitra") %>' ForeColor="DarkBlue"></asp:Label>
                                            <br>
                                            <asp:Label ID="lblInstitusiMitraPengabdian" runat="server" Text='<%# Eval("nama_organisasi_institusi") %>'></asp:Label><br />
                                            <%--<asp:LinkButton runat="server" ID="lbUnggah"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("id_mitra_abdimas") %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                                                Tgl.Unggah Data:
                                                                <asp:Label ID="lblTglUnggah" runat="server" Text='<%# Eval("tgl_unggah_pernyataan") %>'>
                                                                </asp:Label>&nbsp;
                                                                    <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumen"
                                                                        CommandArgument='<%# Eval("id_mitra_abdimas") + "||" + Eval("nama_organisasi_institusi") %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="far fa-file-pdf fa-2x"></i></asp:LinkButton>
                                            <br />
                                            <br />
                                            <hr />
                                            <h6 style="color: #4CAF50;">Tahun 1</h6>
                                            <span style="color: darkblue;"><%# Eval("mitra1tahun1").ToString().Split('|')[1] %></span>
                                            <br />
                                            <%# Eval("mitra1tahun1").ToString().Split('|')[2] %>
                                            <br />
                                            <%--<asp:LinkButton runat="server" ID="lbUnggahMitra11"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun1").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                                                Tgl.Unggah Data:&nbsp;<%# Eval("mitra1tahun1").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh11" runat="server" CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun1").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="far fa-file-pdf fa-2x"></i></asp:LinkButton>
                                            <br />
                                            <br />
                                            <span style="color: darkblue;"><%# Eval("mitra2tahun1").ToString().Split('|')[1] %></span>
                                            <br />
                                            <%# Eval("mitra2tahun1").ToString().Split('|')[2] %>
                                            <br />
                                            <%--<asp:LinkButton runat="server" ID="lbUnggahMitra21"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun1").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                                                Tgl.Unggah Data:&nbsp;<%# Eval("mitra2tahun1").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh12" runat="server" CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun1").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="far fa-file-pdf fa-2x"></i></asp:LinkButton>
                                            <br />
                                            <br />
                                            <hr />
                                            <%--<h6 style="color: #4CAF50;">Tahun 2</h6>
                                            <span style="color: darkblue;"><%# Eval("mitra1tahun2").ToString().Split('|')[1] %></span>
                                            <br />
                                            <%# Eval("mitra1tahun2").ToString().Split('|')[2] %>
                                            <br />--%>
                                            <%--<asp:LinkButton runat="server" ID="lbUnggahMitra12"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun2").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                            <%--                    Tgl.Unggah Data:&nbsp;<%# Eval("mitra1tahun2").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh21" runat="server" CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun2").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="far fa-file-pdf fa-2x"></i></asp:LinkButton>
                                            <br />
                                            <br />
                                            <span style="color: darkblue;"><%# Eval("mitra2tahun2").ToString().Split('|')[1] %></span>
                                            <br />
                                            <%# Eval("mitra2tahun2").ToString().Split('|')[2] %>
                                            <br />--%>
                                            <%--<asp:LinkButton runat="server" ID="lbUnggahMitra22"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun2").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                            <%--                    Tgl.Unggah Data:&nbsp;<%# Eval("mitra2tahun2").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh22" runat="server" CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun2").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="far fa-file-pdf fa-2x"></i></asp:LinkButton>
                                            <br />
                                            <br />
                                            <hr />
                                            <h6 style="color: #4CAF50;">Tahun 3</h6>
                                            <span style="color: darkblue;"><%# Eval("mitra1tahun3").ToString().Split('|')[1] %></span>
                                            <br />
                                            <%# Eval("mitra1tahun3").ToString().Split('|')[2] %>
                                            <br />--%>
                                            <%--<asp:LinkButton runat="server" ID="lbUnggahMitra13"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun3").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                            <%--                    Tgl.Unggah Data:&nbsp;<%# Eval("mitra1tahun3").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh31" runat="server" CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun3").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="far fa-file-pdf fa-2x"></i></asp:LinkButton>
                                            <br />
                                            <br />
                                            <span style="color: darkblue;"><%# Eval("mitra2tahun3").ToString().Split('|')[1] %></span>
                                            <br />
                                            <%# Eval("mitra2tahun3").ToString().Split('|')[2] %>
                                            <br />--%>
                                            <%--<asp:LinkButton runat="server" ID="lbUnggahMitra23"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun3").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                           <%--                     Tgl.Unggah Data:&nbsp;<%# Eval("mitra2tahun3").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh32" runat="server" CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun3").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="far fa-file-pdf fa-2x"></i></asp:LinkButton>--%>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp "></asp:Label><&nbsp;
                                            <asp:Label ID="lblDana1" runat="server" Text='<%# ((Decimal)Eval("dana_tahun_1")).ToString("N0") %>'></asp:Label>
                                            <%--<br>
                                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2: Rp"></asp:Label>
                                            <asp:Label ID="lblDana2" runat="server" Text='<%# ((Decimal)Eval("dana_tahun_2")).ToString("N0") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3: Rp"></asp:Label>
                                            <asp:Label ID="lblDana3" runat="server" Text='<%# ((Decimal)Eval("dana_tahun_3")).ToString("N0") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="false" VerticalAlign="Top" Width="200px" />
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="min-height: 100px; margin: 0 auto;">
                                        <strong>DATA TIDAK DITEMUKAN</strong>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>


                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlKekMitra">
                <div class="row">
                    <div class="col-lg-12">
                        <div style="background-color: #ffaaaa;" class="alert alert-light" role="alert">
                            <asp:Label runat="server" ID="lblKekMitra" Text="Mitra wajib belum diisi"></asp:Label>
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




