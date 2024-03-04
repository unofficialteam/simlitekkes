<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="unduhPdfUsulan.aspx.cs" Inherits="simlitekkes.UserControls.Pengusul.unduhPdfUsulan" %>
<%@ Register Src="~/UserControls/Pengusul/rekapUsulan.ascx" TagName="rekapUsulan" TagPrefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 73px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <uc:rekapUsulan runat="server" ID="ktRekapUsulan" />

            <table width="100%">
                <tr>
                    <td width="50">
                        <asp:Image runat="server" ID="ImgPdf" Width="40px" Height="56px" ImageUrl="~/assets/dist/img/pdf-red.png" />
                    </td>
                    <td width="600" align="top">
                        <asp:Label runat="server" ID="lblJudul" Text="Judul" ForeColor="Blue"></asp:Label><br />

                        <asp:Label runat="server" ID="lblSkema" Text="Nama skema - kategori penelitian" ForeColor="Green"></asp:Label>

                        &nbsp;<asp:Label runat="server" ID="lblUrutanDanLamaKegiatan" Text="(tahun ke-2 dari 3 tahun)" ForeColor="Black"></asp:Label>

                    </td>
                </tr>
            </table>

            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul -  Ketua 
                  
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="2">
                            <tr>
                                <td>&nbsp;&nbsp;<asp:Label runat="server" ID="lblNamaKetua" Text="Atong"></asp:Label>
                                    <asp:Label runat="server" ID="lblNidnKetua" Text="(9999910122)" Font-Bold="true"></asp:Label>
                                </td>
                                <td>ID Sinta:
                            <asp:Label runat="server" ID="lblIdSinta" Text="0000000" ForeColor="Green"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;<asp:Label runat="server" ID="lblNamaPtDanProdi" Text="Universitas Suralaya - Sistem Informasi"></asp:Label>
                                </td>
                                <td>Kualifikasi:
                            <asp:Label runat="server" ID="lblKualifikasi" Text="S1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;Alamat Surel:
                                    <asp:Label runat="server" ID="lblAlamatSurel" Text="atong@gmail.com"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">&nbsp;&nbsp;+ Publikasi Artikel Jurnal Internasional
                            (<asp:Label runat="server" ID="lblJmlPubJurnalInternasional" ForeColor="Red" Text="1"></asp:Label>)
                            &nbsp;-&nbsp;
                            Nasional Terakreditasi
                            (<asp:Label runat="server" ID="lblJmlPubNasTerakreditasi" ForeColor="Red" Text="2"></asp:Label>)
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;+ Publikasi Artikel Prosiding&nbsp;
                            (<asp:Label runat="server" ID="lblJmlProsiding" ForeColor="Red" Text="2"></asp:Label>)
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;+ Kekayaan Intelektual
                            (<asp:Label runat="server" ID="lblJmlHki" ForeColor="Red" Text="1"></asp:Label>)
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;+ Buku
                            (<asp:Label runat="server" ID="lblJmlBuku" ForeColor="Red" Text="2"></asp:Label>)
                                </td>
                            </tr>
                        </table>


                    </td>
                </tr>
            </table>


            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul - Anggota Peneliti (<asp:Label runat="server" ID="lblTotalAnggota" Text="--"></asp:Label>)
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 350px;">
                                    <span style="border: 1px solid gray;">
                                        <table width="100%">
                                            <asp:Repeater ID="rptAnggota" runat="server" OnItemDataBound="rptAnggota_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td width="50" align="left" border="1">
                                                            <span style="padding-left: 5px;">
                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("status")%>' Visible="false"></asp:Label>
                                                                <asp:Image runat="server" ID="imgSetujuAnggota" Width="48px" Height="64px" ImageUrl="~/assets/dist/img/setuju.png" />
                                                            </span>
                                                        </td>
                                                        <td width="300"><span style="color: blue;"><%# Eval("nama_anggota")%></span>
                                                            <br />
                                                            <%# Eval("nama_institusi")%> - <span style="background-color: yellow;">&nbsp;anggota <%# Eval("urutan_anggota")%>&nbsp;</span><br />
                                                            Tugas: <span style="font-style: italic"><%# Eval("tugas")%></span>

                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </span>
                                </td>
                            </tr>
                        </table>


                    </td>
                </tr>
            </table>


            <table width="100%">
                <tr>
                    <td style="background-color: #ddd; height: 30px; vertical-align: middle;">&nbsp;&nbsp;Identitas Pengusul - Anggota Peneliti Non-Ristekdikti (<asp:Label runat="server" ID="lblTotalNonDrpm" Text="--"></asp:Label>)
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 350px;">
                                    <table width="100%">
                                        <asp:Repeater ID="rptAnggotaNonRistekDikti" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="50">
                                                        <span style="color: blue;"><%# Eval("nama_non_drpm")%></span>&nbsp;(<%# Eval("no_ktp_non_drpm")%>)<br />
                                                        <%# Eval("nama_institusi")%>&nbsp;&nbsp;<span style="background-color: yellow;">&nbsp;<%# Eval("peran")%> </span>
                                                        <br />
                                                        Tugas: <span style="font-style: italic"><%# Eval("tugas")%></span><br />
                                                        Bidang keahlian: <%# Eval("bidang_keahlian")%>
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











            <div style="text-align: left;">

                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <tr align="center" bgcolor="#cccccc">
                        <th width="650" style="background-color: #ddd; height: 30px; vertical-align: middle;  text-align: left;">&nbsp;&nbsp;Luaran dan Target Capaian
                        </th>
                    </tr>
                </table>

                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <tr align="center">
                        <th width="650" style="color: brown; text-align: left;">LUARAN WAJIB
                        </th>
                    </tr>
                    <asp:Repeater ID="rptLuaranWajib" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: left;">
                                    <%# Eval("luaran_wajib")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>


                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <tr align="center">
                        <th width="650" style="color: brown; text-align: left;">LUARAN TAMBAHAN
                        </th>
                    </tr>
                    <asp:Repeater ID="rptLuaranTambahan" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Eval("luaran_tambahan")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>



                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <tr align="center" bgcolor="#cccccc">
                        <th width="650" style="background-color: #ddd; height: 30px; vertical-align: middle;  text-align: left;">&nbsp;&nbsp;Rencana Anggaran Biaya
                        </th>
                    </tr>
                </table>

                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: left;">
                                    <%# Eval("luaran_wajib")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>


                
                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <tr align="center" bgcolor="#cccccc">
                        <th width="650" style="background-color: #ddd; height: 30px; vertical-align: middle;  text-align: left;">&nbsp;&nbsp;Dokumen Pendukung
                        </th>
                    </tr>
                </table>
                
                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <tr align="center">
                        <th width="650" style="text-align: left;">
                            - Mitra Pelaksana Penelitian  (<asp:Label runat="server" ID="lblJmlMitraPelaksana" Text="0"></asp:Label>)
                        </th>
                    </tr>
                </table>
                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: left;">
                                    <%# Eval("luaran_wajib")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                
                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <tr align="center">
                        <th width="650" style=" text-align: left;">
                            - Mitra Calon Pengguna (<asp:Label runat="server" ID="lblJmlMitraCalonPengguna" Text="0"></asp:Label>)
                        </th>
                    </tr>
                </table>
                <table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <asp:Repeater ID="Repeater3" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: left;">
                                    <%# Eval("luaran_wajib")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>



            </div>

        </div>
    </form>
</body>
</html>
