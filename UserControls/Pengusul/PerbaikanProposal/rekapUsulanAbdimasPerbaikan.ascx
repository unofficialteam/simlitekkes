<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rekapUsulanAbdimasPerbaikan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.PerbaikanProposal.rekapUsulanAbdimasPerbaikan" %>

<div>
    <!-- Judul -->


    <div class="row" >
        <div class="col col-md-12" style="padding: 15px;">
            <asp:Label runat="server" ID="lblJudul" Font-Bold="true" ForeColor="Maroon"></asp:Label>
        </div>
    </div>

    <!-- Identitas Pengusul -  Ketua  -->
    <div class="card">
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

    <div class="card">
        <div class="card-body" style="padding: 10px;">
            <table width="100%" >
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
                                            <i class="fa fa-file-pdf-o" style="font-size: 50px; "></i>
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
                <div style="background-color:  #ffaaaa;" class="alert alert-light" role="alert" >
                        <asp:Label runat="server" ID="lblInfoDokUsulan" Text="Dokumen usulan belum diunggah"></asp:Label>
                    </div>
            </asp:Panel>
        </div>
    </div>

    <!-- Anggota Peneliti Dikti  -->
    <div class="card">
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


            <asp:Panel runat="server" ID="pnlKekAnggota" >
                <div style="background-color:  #ffaaaa;" class="alert alert-light" role="alert" >
                        <asp:Label runat="server" ID="lbllKekAnggota" Text="Jumlah anggota tidak sesuai kuota/anggota belum melakukan persetujuan"></asp:Label>
                    </div>
            </asp:Panel>

        </div>
    </div>


    <!-- Luaran dan Target Capaian  -->

    <div class="card">
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
                <asp:Repeater ID="rptLuaranWajib" runat="server">
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
                            Tahun <%# Eval("tahun_ke")%><br />
                                
                                
                                                    <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label>
                                                    <asp:Label ID="lbltargetluaran" runat="server" Font-Bold="true" BackColor="Yellow" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label><br />
                                                    <asp:Label ID="lblketerangan" runat="server" Font-Italic="true" Text='<%# Bind("keterangan") %>'></asp:Label>
                                                
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

            <asp:Panel runat="server" ID="pnlKekLuaran" Style="margin-bottom: 30px;">
                <div style="background-color:  #ffaaaa;" class="alert alert-light" role="alert" >
                        <asp:Label runat="server" ID="lblKekLuaran" Text="Luaran wajib belum diisi"></asp:Label>
                    </div>
            </asp:Panel>
        </div>
    </div>

    <!-- Rencana Anggaran Biaya  -->

    <div class="card">
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
                <div style="background-color:  #ffaaaa;" class="alert alert-light" role="alert" >
                        <asp:Label runat="server" ID="lblKekRab" Text="Rab belum diisi"></asp:Label>
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
                                AutoGenerateColumns="False" DataKeyNames="id_mitra_abdimas, nama_pimpinan_mitra,kd_sts_surat_pernyataan" OnRowDataBound="gvMitraAbdimas_RowDataBound" OnRowCommand="gvMitraAbdimas_RowCommand"
                                >
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Kategori Mitra">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblKategoriMitraPenelitian" runat="server" Text='<%# Bind("kategori_mitra") %>' ForeColor="Maroon"></asp:Label></b>
                                            </ItemTemplate>
                                        <HeaderStyle Width="20%" />
                                        <ItemStyle Font-Bold="false" />
                                        </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipe Mitra">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("tipe_mitra") %>' ForeColor="DarkBlue"></asp:Label></b><br />
                                            <asp:LinkButton runat="server" ID="lbUnduhIcon"
                                                ForeColor="Red" data-toggle="tooltip" CommandName="unduhDokumenMitra"
                                                CommandArgument="<%# Container.DataItemIndex %>">
                                                <i class="fa fa-file-pdf-o fa-2x"></i>
                                            </asp:LinkButton>
                                            Tgl.Unggah Data: <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label>
                                            <br />
                                            <%--<asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitra"
                                                CommandArgument="<%# Container.DataItemIndex %>">Unduh</asp:LinkButton>--%>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30%" />
                                        <ItemStyle Font-Bold="false" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Nama Pimpinan mitra">
                                        <ItemTemplate>
                                            <%# Eval("nama_pimpinan_mitra") %>
                                            </ItemTemplate>
                                        <HeaderStyle Width="28%" />
                                        </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:Rp"></asp:Label>
                                            <asp:Label ID="lblDana1" runat="server" Text='<%# Eval("dana_thn_1", "{0:0,00}") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:Rp"></asp:Label>
                                            <asp:Label ID="lblDana2" runat="server" Text='<%# Eval("dana_thn_2", "{0:0,00}") %>'></asp:Label><br>
                                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:Rp"></asp:Label>
                                            <asp:Label ID="lblDana3" runat="server" Text='<%# Eval("dana_thn_3", "{0:0,00}") %>'></asp:Label>
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
                            <br />
                            <hr />
                            <br />
                                            <asp:GridView ID="gvKelMasyarakat" runat="server"
                                                GridLines="None" ShowHeaderWhenEmpty="True"
                                                AutoGenerateColumns="False"
                                                DataKeyNames="id_mitra_abdimas, nama_pimpinan_mitra, nama_desa,tgl_unggah_pernyataan,id_mitra_abdimas_11,id_mitra_abdimas_12, id_mitra_abdimas_21, id_mitra_abdimas_22, id_mitra_abdimas_31, id_mitra_abdimas_32" OnRowDataBound="gvKelMasyarakat_RowDataBound"
                                                >

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
                                            </ItemTemplate>
                                        <HeaderStyle Width="20%" />
                                        <ItemStyle Font-Bold="false" VerticalAlign="Top"/>
                                        </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tipe Mitra">
                                        <ItemTemplate>
                                            <b>
                                                <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("tipe_mitra") %>' ForeColor="DarkBlue"></asp:Label></b><br />
                                            <asp:LinkButton runat="server" ID="lbUnduh"
                                                ForeColor="Gray" data-toggle="tooltip" CommandName="unduhDokumenMitra"
                                                CommandArgument="<%# Container.DataItemIndex %>">
                                                <i class="fa fa-file-pdf-o fa-2x"></i>
                                            </asp:LinkButton>
                                            Tgl.Unggah Data: <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label>
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
                                                            <%--<br />
                                                            <asp:LinkButton ID="lbUnggahDokMitraPelaksanaPengabdian" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggahDokMitraKelMas" data-toggle="tooltip"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>
                                                            Tgl.Unggah Data:
                                                            <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                            &nbsp;<i class="label bg-info fa fa-download">
                                                                <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraSasaran"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White">Unduh</asp:LinkButton></i>--%>
                                                            <br />

                                                            <hr>
                                                            <b>Tahun 1</b><br />
                                                            <asp:Label ID="lbnama11" runat="server" Text='<%# Bind("nama_pimpinan_mitra_11") %>'></asp:Label>
                                                            <br />
                                                            <%--<asp:LinkButton ID="lbunggah11" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah11" data-toggle="tooltip"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>--%>
                                                            Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh11" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_11").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                            &nbsp;
                                                                <asp:LinkButton ID="lbUnduh11" runat="server" CommandName="unduh11"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White"><i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                                            <br />
                                                            <asp:Label ID="lbnama12" runat="server" Text='<%# Bind("nama_pimpinan_mitra_12") %>'></asp:Label>
                                                            <br />
                                                            <%--<asp:LinkButton ID="lbunggah12" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah12" data-toggle="tooltip"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>--%>
                                                            Tgl.Unggah Data:
                                                            <asp:Label ID="Label3" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_12").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                            &nbsp;
                                                                <asp:LinkButton ID="lbUnduh12" runat="server" CommandName="unduh12"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White"><i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>

                                                            <hr>
                                                            <b>Tahun 2</b><br />
                                                            <asp:Label ID="lbnama21" runat="server" Text='<%# Bind("nama_pimpinan_mitra_21") %>'></asp:Label>
                                                            <br />
                                                            <%--<asp:LinkButton ID="lbunggah21" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah21" data-toggle="tooltip"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>--%>
                                                            Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh21" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_21").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                            &nbsp;
                                                                <asp:LinkButton ID="lbUnduh21" runat="server" CommandName="unduh21"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White"><i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                                            <br />
                                                            <asp:Label ID="lbnama22" runat="server" Text='<%# Bind("nama_pimpinan_mitra_22") %>'></asp:Label>
                                                            <br />
                                                            <%--<asp:LinkButton ID="lbunggah22" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah22" data-toggle="tooltip"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>--%>
                                                            Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh22" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_22").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                            &nbsp;
                                                                <asp:LinkButton ID="lbUnduh22" runat="server" CommandName="unduh22"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White"><i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                                            <hr>
                                                            <b>Tahun 3</b><br />
                                                            <asp:Label ID="lbnama31" runat="server" Text='<%# Bind("nama_pimpinan_mitra_31") %>'></asp:Label>
                                                            <br />
                                                            <%--<asp:LinkButton ID="lbunggah31" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah31" data-toggle="tooltip"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>--%>
                                                            Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh31" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_31").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                            &nbsp;
                                                                <asp:LinkButton ID="lbUnduh31" runat="server" CommandName="unduh31"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White"><i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                                            <br />
                                                            <asp:Label ID="lbnama32" runat="server" Text='<%# Bind("nama_pimpinan_mitra_32") %>'></asp:Label>
                                                            <br />
                                                            <%--<asp:LinkButton ID="lbunggah32" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah32" data-toggle="tooltip"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>--%>
                                                            Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh32" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_32").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                            &nbsp;
                                                                <asp:LinkButton ID="lbUnduh32" runat="server" CommandName="unduh32"
                                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White"><i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="28%" />
                                                        <ItemStyle Font-Bold="false" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:"></asp:Label>
                                                            <asp:Label ID="lblDana1" runat="server" Text='<%# Bind("dana_tahun_1","{0:C0}") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:"></asp:Label>
                                                            <asp:Label ID="lblDana2" runat="server" Text='<%# Bind("dana_tahun_2","{0:C0}") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:"></asp:Label>
                                                            <asp:Label ID="lblDana3" runat="server" Text='<%# Bind("dana_tahun_3","{0:C0}") %>'></asp:Label>
                                                            <br>
                                                            <br />
                                                            <br>
                                                            <br />
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
                                                    ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                    DataKeyNames="id_mitra_abdimas, nama_organisasi_institusi" OnRowDataBound="gvKelMasyarakatPpmUpt_RowDataBound"
                                                    >
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
                                                                        <i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
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
                                                                Tgl.Unggah Data:&nbsp;<%# Eval("mitra1tahun1").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh11" runat="server"  CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun1").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
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
                                                                Tgl.Unggah Data:&nbsp;<%# Eval("mitra2tahun1").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh12" runat="server"  CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun1").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                                                <br />
                                                                <br />
                                                                <hr />
                                                                <h6 style="color: #4CAF50;">Tahun 2</h6>
                                                                <span style="color: darkblue;"><%# Eval("mitra1tahun2").ToString().Split('|')[1] %></span>
                                                                <br />
                                                                <%# Eval("mitra1tahun2").ToString().Split('|')[2] %>
                                                                <br />
                                                                <%--<asp:LinkButton runat="server" ID="lbUnggahMitra12"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun2").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                                                Tgl.Unggah Data:&nbsp;<%# Eval("mitra1tahun2").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh21" runat="server"  CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun2").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                                                <br />
                                                                <br />
                                                                <span style="color: darkblue;"><%# Eval("mitra2tahun2").ToString().Split('|')[1] %></span>
                                                                <br />
                                                                <%# Eval("mitra2tahun2").ToString().Split('|')[2] %>
                                                                <br />
                                                                <%--<asp:LinkButton runat="server" ID="lbUnggahMitra22"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun2").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                                                Tgl.Unggah Data:&nbsp;<%# Eval("mitra2tahun2").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh22" runat="server"  CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun2").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                                                <br />
                                                                <br />
                                                                <hr />
                                                                <h6 style="color: #4CAF50;">Tahun 3</h6>
                                                                <span style="color: darkblue;"><%# Eval("mitra1tahun3").ToString().Split('|')[1] %></span>
                                                                <br />
                                                                <%# Eval("mitra1tahun3").ToString().Split('|')[2] %>
                                                                <br />
                                                                <%--<asp:LinkButton runat="server" ID="lbUnggahMitra13"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun3").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                                                Tgl.Unggah Data:&nbsp;<%# Eval("mitra1tahun3").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh31" runat="server"  CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra1tahun3").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                                                <br />
                                                                <br />
                                                                <span style="color: darkblue;"><%# Eval("mitra2tahun3").ToString().Split('|')[1] %></span>
                                                                <br />
                                                                <%# Eval("mitra2tahun3").ToString().Split('|')[2] %>
                                                                <br />
                                                                <%--<asp:LinkButton runat="server" ID="lbUnggahMitra23"
                                                                    class="btn btn-primary btn-mini" data-toggle="tooltip" CommandName="unggahDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun3").ToString() %>' ToolTip="Unggah"><i class="icofont icofont-upload"></i>
                                                                </asp:LinkButton>--%>
                                                                Tgl.Unggah Data:&nbsp;<%# Eval("mitra2tahun3").ToString().Split('|')[4] %><asp:LinkButton ID="lbUnduh32" runat="server" CommandName="unduhDokumen"
                                                                    CommandArgument='<%# Eval("mitra2tahun3").ToString() %>' ForeColor="White" ToolTip="Unduh">
                                                                        <i class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle />
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp"></asp:Label>
                                                                <asp:Label ID="lblDana1" runat="server" Text='<%# ((Decimal)Eval("dana_tahun_1")).ToString("N0") %>'></asp:Label><br>
                                                                <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2: Rp"></asp:Label>
                                                                <asp:Label ID="lblDana2" runat="server" Text='<%# ((Decimal)Eval("dana_tahun_2")).ToString("N0") %>'></asp:Label><br>
                                                                <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3: Rp"></asp:Label>
                                                                <asp:Label ID="lblDana3" runat="server" Text='<%# ((Decimal)Eval("dana_tahun_3")).ToString("N0") %>'></asp:Label>
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
            <div style="height: 20px;">&nbsp;</div>
            <asp:Panel runat="server" ID="pnlKekMitra">
                <div style="background-color:  #ffaaaa;" class="alert alert-light" role="alert" >
                        <asp:Label runat="server" ID="lblKekMitra" Text="Mitra wajib belum diisi"></asp:Label>
                    </div>
            </asp:Panel>
        </div>
    </div>


</div>




