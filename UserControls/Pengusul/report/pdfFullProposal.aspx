<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pdfFullProposal.aspx.cs" Inherits="simlitekkes.UserControls.Pengusul.report.pdfFullProposal" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfRab.ascx" TagName="pdfRab2018" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfRabPerbaikan.ascx" TagName="pdfRabPerbaikan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfRab2019.ascx" TagName="pdfRab2019" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/report/biodata.ascx" TagName="pdfBioData" TagPrefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        
    table{
        font-family: Arial;
    }
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

            <asp:Panel runat="server" ID="panelPerbaikan" Visible="false">

            <div style="text-align: right;">
                <b style="display: inline; text-align: right; background-color: black; color: white; padding: 3px; font-size: 14px; font-family: Arial;">PERBAIKAN
            </b>
            </div>
            </asp:Panel>

            <table width="100%">
                <tr>
                    <td align="center" style="font-family: Arial; font-weight: bold; font-size: 11px;">PROTEKSI ISI PROPOSAL<br />
                        <span style="font-weight: normal;">Dilarang menyalin, menyimpan, memperbanyak sebagian atau seluruh isi proposal ini dalam bentuk apapun</span><br />
                        <span style="font-weight: normal;">kecuali oleh pengusul dan pengelola administrasi penelitian</span>
                    </td>
                </tr>
            </table>
        </div>
        
        <asp:MultiView runat="server" ID="mvJenisProposalPenelitian" >
            <asp:View runat="server" ID="vJenisUsulanBaru">
        <div style="text-align: center;">
            <asp:Label runat="server" ID="lblJudulBlock" Text="PROPOSAL PENELITIAN 2018" Font-Bold="true"
                Style="display: inline; text-align: center; background-color: black; color: white; padding: 3px; font-size: 14px; font-family: Arial;"></asp:Label>
            <%--<b runat="server" id="lblJudulBlock0" style="display: inline; text-align: center; background-color: black; color: white; padding: 3px; font-size: 14px; font-family: Arial;">PROPOSAL PENELITIAN 2018
            </b>--%>
        </div>
            </asp:View>
            <asp:View runat="server" ID="vJenisUsulanPerbaikan">
        <div style="text-align: center;">
            <%--<b style="display: inline; text-align: center; background-color: black; color: white; padding: 3px; font-size: 14px; font-family: Arial;">PROPOSAL PENELITIAN 
                <asp:Label runat="server" ID="lblThnUsulanPerbaikan"></asp:Label>
            </b>--%>
            <asp:Label runat="server" ID="lblJudulBlockPerbaikan" Text="PROPOSAL PENELITIAN 2018" Font-Bold="true"
                Style="display: inline; text-align: center; background-color: black; color: white; padding: 3px; font-size: 14px; font-family: Arial;"></asp:Label>
        </div>
            </asp:View>
        </asp:MultiView>

        

        <table width="100%" cellpadding="-2" border="0" style="font-family: Arial; font-weight: normal; font-size: 11px; margin-top: 5px;">
            <tr>
                <td style="width: 32%;"></td>
                <td style="width: 68%;">ID Proposal:
                    <asp:Label runat="server" ID="lblIdUsulanKegiatan"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Rencana Pelaksanaan Penelitian:
                    <asp:Label runat="server" ID="lblRencanaPelaksanaan"></asp:Label>
                </td>
            </tr>
        </table>
        <div>&nbsp;</div>
        <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">1. JUDUL PENELITIAN
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
                <td width="120px" class="trtd">Pilar Transformasi
                </td>
                <td width="80px" class="trtd">Tema
                </td>
                <td width="80px" class="trtd">Topik
                </td>
                <td width="90px" class="trtd">Rumpun Bidang Ilmu
                </td>
            </tr>
            <tr class="trtd">
                <td class="trtd">
                    <asp:Label runat="server" ID="lblRirn"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTema"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTopik"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblRumpunIlmu"></asp:Label>
                </td>
            </tr>
        </table>
        <!-- <table width="" border="1" style="border-collapse:collapse; border-color:#ffffff; border-style:solid; border-width:1pt"> -->

        <div>&nbsp;</div>
        <table width="100%" class="trtd">

            <tr align="center" class="trtd">
                <td width="140px" class="trtd">Kategori (Desentralisasi/ Penugasan)
                </td>
                <td width="80px" class="trtd">Skema Penelitian
                </td>
                <td width="100px" class="trtd">Strata (Dasar/ Terapan/ Pengembangan)
                </td>
                <td width="100px" class="trtd">TKT saat ini
                </td>
                <td width="80px" class="trtd">Target Akhir TKT
                </td>
                <td width="70px" class="trtd">Lama Penelitian (Tahun)
                </td>
            </tr>
            <tr>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblKategori"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblNamaSkema"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblStrata"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTktSaatIni"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTkt"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblLama"></asp:Label>
                </td>
            </tr>
        </table>
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
                <td width="100px" class="trtd">Bidang Tugas
                </td>
                <td width="90px" class="trtd">ID Sinta
                </td>
                <td width="50px" class="trtd">H-Index
                </td>
            </tr>
            <asp:Repeater ID="rptAnggota" runat="server">
                <ItemTemplate>
                    <tr class="trtd">
                         <td class="trtd">
                            <%# Eval("nama")%><br /><br />
                            <%# Eval("peran_personil")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("nama_institusi")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("nama_program_studi")%>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("bidang_tugas")%>
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
                <td style="font-weight: bold;">3. MITRA KERJASAMA PENELITIAN (JIKA ADA)
                </td>
            </tr>
            <tr>
                <td style="padding: 0px;">Pelaksanaan penelitian dapat melibatkan mitra kerjasama, yaitu mitra kerjasama dalam melaksanakan penelitian, mitra sebagai calon pengguna hasil penelitian, atau mitra investor
                </td>
            </tr>
        </table>
        <table width="100%" class="trtd" border="1">

            <tr class="trtd">
                <td class="trtd" width="40%">Mitra
                </td>
                <td class="trtd" width="60%">Nama Mitra
                </td>
            </tr>

            <asp:Repeater ID="rptMitra" runat="server">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd" width="40%" style="text-align: left;">&nbsp;
                            <%# Eval("mitra")%>
                        </td>
                        <td class="trtd" width="60%" style="text-align: left;">&nbsp;
                            <%# Eval("nama_mitra")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

        
        <div>&nbsp;</div>
        <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                    4. LUARAN DAN TARGET CAPAIAN
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                    Luaran Wajib
                </td>
            </tr>
        </table>
        


        <table width="100%" class="trtd" border="1">

            <tr class="trtd">
                <td class="trtd" width="10%">Tahun Luaran
                </td>
                <td class="trtd" width="30%">Jenis Luaran
                </td>
                <td class="trtd" width="30%">
                    Status target capaian (<i>accepted, published, terdaftar atau granted, atau status lainnya</i>)
                </td>
                <td class="trtd" width="30%">
                    Keterangan (<i>url dan nama jurnal, penerbit, url paten, keterangan sejenis lainnya</i>)
                </td>
            </tr>

            <asp:Repeater ID="rptLuaranWajib" runat="server">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd"  style="text-align: center;">&nbsp;
                            <%# Eval("tahun_luaran")%>
                        </td>
                        <td class="trtd"  style="text-align: left;">&nbsp;
                            <%# Eval("jenis_luaran")%>
                        </td>
                        <td class="trtd"  style="text-align: left;">&nbsp;
                            <%# Eval("status_target_capaian")%>
                        </td>
                        <td class="trtd"  style="text-align: left;">&nbsp;
                            <%# Eval("keterangan")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        
        <table>
             <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                    Luaran Tambahan
                </td>
            </tr>
        </table>
        


        <table width="100%" class="trtd" border="1">

            <tr class="trtd">
                <td class="trtd" width="10%">Tahun Luaran
                </td>
                <td class="trtd" width="20%">Jenis Luaran
                </td>
                <td class="trtd" width="35%">
                    Status target capaian (<i>accepted, published, terdaftar atau granted, atau status lainnya</i>)
                </td>
                <td class="trtd" width="35%">
                    Keterangan (<i>url dan nama jurnal, penerbit, url paten, keterangan sejenis lainnya</i>)
                </td>
            </tr>
            
            <asp:Repeater ID="rptLuaranTambahan" runat="server">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd"  style="text-align: center;">&nbsp;
                            <%# Eval("tahun_luaran")%>
                        </td>
                        <td class="trtd"  style="text-align: left;">&nbsp;
                            <%# Eval("jenis_luaran")%>
                        </td>
                        <td class="trtd"  style="text-align: left;">&nbsp;
                            <%# Eval("status_target_capaian")%>
                        </td>
                        <td class="trtd"  style="text-align: left;">&nbsp;
                            <%# Eval("keterangan")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <uc:pdfRab2018 runat="server" ID="pdfRab2018"  Visible="false"></uc:pdfRab2018>
        <uc:pdfRabPerbaikan runat="server" ID="pdfRabPerbaikan"  Visible="false"></uc:pdfRabPerbaikan>
        <uc:pdfRab2019 runat="server" ID="pdfRab2019"  Visible="true"></uc:pdfRab2019>


        <%--<uc:pdfBioData runat="server" ID="pdfBioData" ></uc:pdfBioData>--%>

    </form>

</body>
</html>
