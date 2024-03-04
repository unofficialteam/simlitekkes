<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pdfFullProposalAbdimas.aspx.cs" Inherits="simlitekkes.UserControls.Pengusul.report.pdfFullProposalAbdimas" %>

<%@ Register Src="~/UserControls/Pengusul/report/pdfRab.ascx" TagName="pdfRab" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/report/biodata.ascx" TagName="pdfBioData" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfRab2019.ascx" TagName="pdfRab2019" TagPrefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        /*
table{
    border-collapse: collapse;
    width: 100%;
}
table, td, th{
    border: 1px solid gray;
}
*/

        .trtd {
            border: 1px solid black;
            text-align: center;
            border-collapse: Collapse;
            font-family: Arial;
            font-size: 12px;
            border-color: rgba(0,0,0, 0.5);
            padding: 5px;
        }

        .trtd_left {
            border: 1px solid black;
            text-align: left;
            border-collapse: Collapse;
            font-family: Arial;
            font-size: 12px;
            border-color: rgba(0,0,0, 0.5);
            padding: 5px;
        }

        .trtd_right {
            border: 1px solid black;
            text-align: right;
            border-collapse: Collapse;
            font-family: Arial;
            font-size: 12px;
            border-color: rgba(0,0,0, 0.5);
            padding: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin-left: 70px; margin-right: 50px;">
        <div>            
            <table width="100%" style="font-size: 13px; font-family: 'Calibri (Body)';">
                <tr>
                    <td>
                        <%--<img src="~/Images/icon/ristekdikti.png" alt=""/>--%>
                        <asp:Image runat="server" ID="imgKop" ImageUrl="~/assets/dist/img/kemenkes.png" Width="164" Height="92" />
                    </td>
                    <td width="500px" style="font-size: 13px; font-family: 'Calibri (Body)'">Pusat Pendidikan SDM Kesehatan
                        <br />
                        Jln. Hang Jebat III Blok F3, Kebayoran Baru Jakarta Selatan – 12120
                        <br />
                        Telepon (021) 726 0401; Faksimile (021) 726 0485
                        <br />
                        Website : http://bppsdmk.kemkes.go.id/pusdiksdmk/
                    </td>
                </tr>
            </table>
            <hr style="margin-top: 0px;" />
            <table width="100%">
                <tr>
                    <td align="center" style="font-family: Arial; font-weight: bold; font-size: 11px;">PROTEKSI ISI PROPOSAL<br />
                        <span style="font-weight: normal;">Dilarang menyalin, menyimpan, memperbanyak sebagian atau seluruh isi proposal ini dalam bentuk apapun</span><br />
                        <span style="font-weight: normal;">kecuali oleh pengusul dan pengelola administrasi pengabdian kepada masyarakat</span>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center;">
            <asp:Label runat="server" ID="lblJudulBlock" Text="PROPOSAL PENGABDIAN KEPADA MASYARAKAT (PPM) 2019" Font-Bold="true"
                Style="display: inline; text-align: center; background-color: black; color: white; padding: 3px; font-size: 14px; font-family: Arial;"></asp:Label>
        </div>
        <table width="100%" cellpadding="-2" border="0" style="font-family: Arial; font-weight: normal; font-size: 11px; margin-top: 5px;">
            <tr>
                <td style="width: 23%;"></td>
                <td style="width: 77%;">ID Proposal:
                    <asp:Label runat="server" ID="lblIdUsulanKegiatan"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Rencana Pelaksanaan PPM:
                    <asp:Label runat="server" ID="lblRencanaPelaksanaan"></asp:Label>
                </td>
            </tr>
        </table>
        <div>&nbsp;</div>
        <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">1. JUDUL PPM
                </td>
            </tr>
        </table>
        <table width="100%" class="trtd">
            <tr>
                <td style="text-align: left; padding: 5px;">
                    <asp:Label runat="server" ID="lblJudul"></asp:Label>
                </td>
            </tr>
        </table>

        <div>&nbsp;</div>

        <table width="100%" class="trtd">

            <tr align="center" class="trtd">
                <td width="120px" class="trtd">Bidang Fokus
                </td>
                <td width="160px" class="trtd">Kategori (Kompetitif Nasional/ Desentralisasi/ Penugasan)
                </td>
                <td width="100px" class="trtd">Skema
                </td>
                <td width="50px" class="trtd">Lama Kegiatan (Tahun),<br />
                    Jumlah keterlibatan<br />
                    mahasiswa (Orang)
                </td>
            </tr>
            <tr class="trtd">
                <td class="trtd">
                    <asp:Label runat="server" ID="lblBidangFokus"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblKategori"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblNamaSkema"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblLamaKegiatan"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="lblJmlMhs"></asp:Label>
                </td>
            </tr>
        </table>
        <!-- <table width="" border="1" style="border-collapse:collapse; border-color:#ffffff; border-style:solid; border-width:1pt"> -->

        <div>&nbsp;</div>
        <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">2. IDENTITAS PENGUSUL
                </td>
            </tr>
        </table>

        <table width="100%" class="trtd">

            <tr align="center" class="trtd">
                <td width="100px" class="trtd">Nama, Peran
                </td>
                <td width="80px" class="trtd">Perguruan Tinggi/ Institusi
                </td>
                <td width="90px" class="trtd">Program Studi/ Bagian
                </td>
                <td width="100px" class="trtd">Bidang Tugas</td>
                <td width="90px" class="trtd">ID Sinta
                </td>
                <td width="50px" class="trtd">H-Index
                </td>
            </tr>
            <asp:Repeater ID="rptAnggota" runat="server">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd">
                            <%# Eval("nama")%><br />
                            <br />
                            <%# Eval("peran_personil")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("nama_institusi")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("nama_program_studi")%>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("bidang_tugas")%><br />

                        </td>
                        <td class="trtd">
                            <%# Eval("id_sinta")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("hindex")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <div>&nbsp;</div>
        <table width="100%" style="font-family: Arial; font-size: 12px;">

            <tr>
                <td style="font-weight: bold;">3. MITRA PPM
                </td>
            </tr>
            <tr>
                <td style="padding: 0px; text-align: justify;">Pelaksanaan PPM melibatkan mitra kerjasama, yaitu mitra kerjasama dalam melaksanakan PPM dan mitra sebagai sasaran PPM
                </td>
            </tr>
        </table>
        <table width="100%" class="trtd" border="1">

            <tr class="trtd">
                <td class="trtd" width="35%">Kategori Mitra, Tipe Mitra
                </td>
                <td class="trtd" width="35%">Mitra
                </td>
                <td class="trtd" width="30%">Dana
                </td>
            </tr>

            <asp:Repeater ID="rptMitra" runat="server" OnItemDataBound="rptMitra_ItemDataBound">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd" width="35%" style="text-align: left;">&nbsp;- <%# Eval("kategori_mitra")%>
                            <br />
                            &nbsp;- <%# Eval("tipe_mitra")%><br />
                        </td>
                        <td class="trtd" width="35%" style="text-align: left;">&nbsp;- <%# Eval("nama_pimpinan_mitra")%>
                            <br />
                            &nbsp;- <%# Eval("nama_organisasi_institusi")%>
                            <br />
                            <%--&nbsp;- <%# Eval("alamat_organisasi_institusi")%>--%>
                            
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp "></asp:Label>
                            <asp:Label ID="lblDana1" runat="server" Text='<%# Eval("dana_thn_1", "{0:0,00}") %>'></asp:Label><br>
                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2: Rp "></asp:Label>
                            <asp:Label ID="lblDana2" runat="server" Text='<%# Eval("dana_thn_2", "{0:0,00}") %>'></asp:Label><br>
                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3: Rp "></asp:Label>
                            <asp:Label ID="lblDana3" runat="server" Text='<%# Eval("dana_thn_3", "{0:0,00}") %>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>


            <asp:Repeater ID="rptMitraSasaranPPDM" runat="server">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd" width="35%" style="text-align: left; vertical-align: top;">&nbsp;- <%# Eval("kategori_mitra")%>
                            <br />
                            &nbsp;- <%# Eval("tipe_mitra")%><br />
                        </td>
                        <td class="trtd" width="35%" style="text-align: left;">
                            <b>&nbsp;- 
                            
                            
                                                                <asp:Label ID="lblNamaMitraPengabdian" runat="server" Text='<%# Bind("nama_pimpinan_mitra") %>'></asp:Label>
                            </b>
                            <br />
                            -
                            <asp:Label ID="lblInstitusiMitraPengabdian" runat="server" Text='<%# Bind("nama_desa") %>'></asp:Label>
                            <br />

                            <hr>
                            <b>Tahun 1</b><br />
                            -
                            <asp:Label ID="lbnama11" runat="server" Text='<%# Bind("nama_pimpinan_mitra_11") %>'></asp:Label>
                            <br />
                            -
                            <asp:Label ID="lbnama12" runat="server" Text='<%# Bind("nama_pimpinan_mitra_12") %>'></asp:Label>
                            <br />
                            <hr>
                            <b>Tahun 2</b><br />
                            -
                            <asp:Label ID="lbnama21" runat="server" Text='<%# Bind("nama_pimpinan_mitra_21") %>'></asp:Label>
                            <br />
                            -
                            <asp:Label ID="lbnama22" runat="server" Text='<%# Bind("nama_pimpinan_mitra_22") %>'></asp:Label>
                            <br />
                            <hr>
                            <b>Tahun 3</b><br />
                            -
                            <asp:Label ID="lbnama31" runat="server" Text='<%# Bind("nama_pimpinan_mitra_31") %>'></asp:Label>
                            <br />
                            -
                            <asp:Label ID="lbnama32" runat="server" Text='<%# Bind("nama_pimpinan_mitra_32") %>'></asp:Label>

                        </td>
                        <td style="text-align: left; vertical-align: top; padding: 5px;">

                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp "></asp:Label>
                            <asp:Label ID="lblDana1" runat="server" Text='<%# Eval("dana_tahun_1", "{0:0,00}") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2: Rp "></asp:Label>
                            <asp:Label ID="lblDana2" runat="server" Text='<%# Eval("dana_tahun_2", "{0:0,00}") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3: Rp "></asp:Label>
                            <asp:Label ID="lblDana3" runat="server" Text='<%# Eval("dana_tahun_3", "{0:0,00}") %>'></asp:Label>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Repeater ID="rptMitraSasaranPpmUpt" runat="server">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd" width="35%" style="text-align: left; vertical-align: top;">&nbsp;- <%# Eval("kategori_mitra")%>
                            <br />
                            &nbsp;- <%# Eval("tipe_mitra")%><br />
                        </td>
                        <td class="trtd" width="40%" style="text-align: left; vertical-align: top;">&nbsp;- <%# Eval("nama_pimpinan_mitra")%>
                            <br />

                            <br />
                            <hr />
                            Tahun 1<br />
                            <%--<span >- <%# Eval("mitra1tahun1").ToString().Split('|')[1] %></span>--%>
                                                               
                                                                - <%# Eval("mitra1tahun1").ToString().Split('|')[2] %>
                            <br />
                            <%--<span>- <%# Eval("mitra2tahun1").ToString().Split('|')[1] %></span>--%>
                                                             
                                                                - <%# Eval("mitra2tahun1").ToString().Split('|')[2] %>
                            <br />
                            <hr />
                            Tahun 2<br />
                            <%--<span >- <%# Eval("mitra1tahun2").ToString().Split('|')[1] %></span>--%>
                                                               
                                                                - <%# Eval("mitra1tahun2").ToString().Split('|')[2] %>
                            <br />
                            <%--<span >- <%# Eval("mitra2tahun2").ToString().Split('|')[1] %></span>--%>
                                                                
                                                                - <%# Eval("mitra2tahun2").ToString().Split('|')[2] %>
                            <br />
                            <hr />
                            Tahun 3<br />
                            <%--<span >- <%# Eval("mitra1tahun3").ToString().Split('|')[1] %></span>
                                                                <br />--%>
                                                                - <%# Eval("mitra1tahun3").ToString().Split('|')[2] %>
                            <br />
                            <%-- <br />
                                                                <span >- <%# Eval("mitra2tahun3").ToString().Split('|')[1] %></span>
                                                                <br />--%>
                                                                - <%# Eval("mitra2tahun3").ToString().Split('|')[2] %>


                        </td>
                        <td style="text-align: left; vertical-align: top; padding: 5px;">

                            <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp "></asp:Label>
                            <asp:Label ID="lblDana1" runat="server" Text='<%# Eval("dana_tahun_1", "{0:0,00}") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2: Rp "></asp:Label>
                            <asp:Label ID="lblDana2" runat="server" Text='<%# Eval("dana_tahun_2", "{0:0,00}") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3: Rp "></asp:Label>
                            <asp:Label ID="lblDana3" runat="server" Text='<%# Eval("dana_tahun_3", "{0:0,00}") %>'></asp:Label>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>


        <div>&nbsp;</div>
        <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">4. LUARAN DAN TARGET CAPAIAN
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; text-align: justify;">Pada bagian ini, pengusul wajib mengisi luaran wajib dan tambahan, tahun capaian, dan status pencapaiannya. Luaran PPM berupa artikel diwajibkan menyebutkan nama jurnal yang dituju dan untuk luaran berupa buku harus mencantumkan nama penerbit yang dituju.
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">Luaran Wajib
                </td>
            </tr>
        </table>



        <table width="100%" class="trtd" border="1">

            <tr class="trtd">
                <td class="trtd" width="10%">Tahun Luaran
                </td>
                <td class="trtd" width="30%">Jenis Luaran
                </td>
                <td class="trtd" width="30%">Status target capaian (<i>sudah terbit, sudah diunggah, sudah tercapai, terdaftar/granted</i>)
                </td>
                <td class="trtd" width="30%">Keterangan (<i>url dan nama jurnal, penerbit, url paten, keterangan sejenis lainnya</i>)
                </td>
            </tr>

            <asp:Repeater ID="rptLuaranWajib" runat="server">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd" style="text-align: center;">&nbsp;
                            <%# Eval("tahun_ke")%>
                        </td>
                        <td class="trtd" style="text-align: left;">&nbsp;
                            <%# Eval("nama_kategori_jenis_luaran")%><br />
                            <%# Eval("nama_jenis_luaran")%>
                        </td>
                        <td class="trtd" style="text-align: left;">&nbsp;
                            <%# Eval("nama_target_capaian_luaran")%>
                        </td>
                        <td class="trtd" style="text-align: left;">&nbsp;
                            <%# Eval("keterangan")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">Luaran Tambahan
                </td>
            </tr>
        </table>



        <table width="100%" class="trtd" border="1">

            <tr class="trtd">
                <td class="trtd" width="10%">Tahun Luaran
                </td>
                <td class="trtd" width="20%">Jenis Luaran
                </td>
                <td class="trtd" width="35%">Status target capaian (<i>sudah terbit, sudah diunggah, sudah tercapai, terdaftar/granted</i>)
                </td>
                <td class="trtd" width="35%">Keterangan (<i>url dan nama jurnal, penerbit, url paten, keterangan sejenis lainnya</i>)
                </td>
            </tr>

            <asp:Repeater ID="rptLuaranTambahan" runat="server">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd" style="text-align: center;">&nbsp;
                            <%# Eval("tahun_ke")%>
                        </td>
                        <td class="trtd" style="text-align: left;">&nbsp;
                            <%# Eval("nama_jenis_luaran")%>
                        </td>
                        <td class="trtd" style="text-align: left;">&nbsp;
                            <%# Eval("nama_target_capaian_luaran")%>
                        </td>
                        <td class="trtd" style="text-align: left;">&nbsp;
                            <%# Eval("keterangan")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <uc:pdfRab runat="server" ID="pdfRab" Visible="false"></uc:pdfRab>
        <uc:pdfRab2019 runat="server" ID="pdfRab2019" Visible="true"></uc:pdfRab2019>
        <%--<uc:pdfBioData runat="server" ID="pdfBioData" ></uc:pdfBioData>--%>
    </form>

</body>
</html>

