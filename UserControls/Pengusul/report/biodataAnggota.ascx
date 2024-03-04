<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="biodataAnggota.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.report.biodataAnggota" %>
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
        .auto-style1 {
            height: 19px;
        }
    </style>


        <!--Biodata -->
        <table  >
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                    <asp:Label runat="server" ID="lblKeteranganAnggota" Text="B. ANGGOTA PENGUSUL 1"></asp:Label>
                    
            </tr>
        </table>

        <table width="100%" class="trtd_left">
            <tr>
                <td class="trtd_left" style="width: 100px;">
                    Nama
                </td>
                <td class="trtd_left" >
                <asp:Label runat="server" ID="lblNama" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="trtd_left" >
                    NIDN/NIDK
                </td>
                <td class="trtd_left" >
                <asp:Label runat="server" ID="lblNidn" ></asp:Label>
                </td>
                <tr>
                <td class="trtd_left" >
                    Pangkat/Jabatan
                </td>
            
                <td class="trtd_left" >
                <asp:Label runat="server" ID="lblPangkatJabatan" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="trtd_left" >
                    E-mail
                </td>
                <td class="trtd_left">
                <asp:Label runat="server" ID="lblSurel" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="trtd_left" >
                    ID Sinta
                </td>
                <td class="trtd_left" >
                <asp:Label runat="server" ID="lblIdSinta" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="trtd_left" >
                    h-Index
                </td>
                <td class="trtd_left" >
                <asp:Label runat="server" ID="lblHindex" ></asp:Label>
                </td>
            </tr>
        </table>

     
    <!-- Jurnal internasional -->
   <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;" class="auto-style1">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">Publikasi di Jurnal Internasional terindeks
                </td>
            </tr>
        </table>
        
   <table width="100%" class="trtd">
       
            <tr align="center" class="trtd">
                <td width="20px" class="trtd">No
                </td>
                <td width="170px" class="trtd">Judul Artikel
                </td>
                <td width="70px" class="trtd">Peran (First author, Corresponding author, atau co-author)
                </td>
                <td width="180px" class="trtd">Nama Jurnal, Tahun terbit, Volume, Nomor, P-ISSN/E-ISSN
                </td>
                <td width="180px" class="trtd">URL artikel (jika ada)
                </td>
            </tr>

            <asp:Repeater ID="rptInter" runat="server" OnItemDataBound="rptInter_ItemDataBound">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd">
                            <%# Container.ItemIndex + 1 %>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("judul")%>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("peran_penulis")%>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("nama_jurnal")%>, <%# Eval("thn_publikasi")%>,
                            <%# Eval("volume")%>, <%# Eval("nomor")%>, <%# Eval("issn")%>
                        </td>
                        <td class="trtd" style="overflow-wrap: break-word;">
                            <asp:Label runat="server" ID="lblUrlArtikel" Text=<%# Eval("url")%> ></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>

    <!-- Jurnal nasional -->
   <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;" class="auto-style1">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">Publikasi di Jurnal Nasional Terakreditasi Peringkat 1 dan 2
                </td>
            </tr>
        </table>
        
   <table width="100%" class="trtd">
       
            <tr align="center" class="trtd">
                <td width="20px" class="trtd">No
                </td>
                <td width="170px" class="trtd">Judul Artikel
                </td>
                <td width="70px" class="trtd">Peran (First author, Corresponding author, atau co-author)
                </td>
                <td width="180px" class="trtd">Nama Jurnal, Tahun terbit, Volume, Nomor, P-ISSN/E-ISSN
                </td>
                <td width="180px" class="trtd">URL artikel (jika ada)
                </td>
            </tr>

            <asp:Repeater ID="rptNas" runat="server" OnItemDataBound="rptNas_ItemDataBound">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd">
                            <%# Container.ItemIndex + 1 %>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("judul")%>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("peran_penulis")%>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("nama_jurnal")%>, <%# Eval("thn_publikasi")%>,
                            <%# Eval("volume")%>, <%# Eval("nomor")%>, <%# Eval("issn")%>
                        </td>
                        <td class="trtd" style="overflow-wrap: break-word;">
                            <asp:Label runat="server" ID="lblUrlArtikel" Text=<%# Eval("url")%> ></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>


    <!-- Prosiding seminar/konverensi internasional terindeks -->
   <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;" class="auto-style1">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                    Prosiding seminar/konverensi internasional terindeks
                </td>
            </tr>
        </table>
        
   <table width="100%" class="trtd">
       
            <tr align="center" class="trtd">
                <td width="20px" class="trtd">No
                </td>
                <td width="170px" class="trtd">Judul Artikel
                </td>
                <td width="70px" class="trtd">Peran (First author, Corresponding author, atau co-author)
                </td>
                <td width="180px" class="trtd">Nama Jurnal, Tahun terbit, Volume, Nomor, P-ISSN/E-ISSN
                </td>
                <td width="180px" class="trtd">URL artikel (jika ada)
                </td>
            </tr>

            <asp:Repeater ID="rptPros" runat="server" OnItemDataBound="rptPros_ItemDataBound">
                <ItemTemplate>
                    <tr class="trtd">
                        
                        <td class="trtd">
                            <%# Container.ItemIndex + 1 %>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("judul")%>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("peran_penulis")%>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("nama_prosiding")%>, <%# Eval("thn_prosiding")%>,
                            <%# Eval("volume")%>, <%# Eval("nomor")%>, <%# Eval("issn")%>
                        </td>
                        <td class="trtd" style="overflow-wrap: break-word;">
                            <asp:Label runat="server" ID="lblUrlArtikel" Text=<%# Eval("url")%> ></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>


    <!-- Buku -->
   <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;" class="auto-style1">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                    Buku
                </td>
            </tr>
        </table>
        
   <table width="100%" class="trtd">
            <tr align="center" class="trtd">
                <td width="20px" class="trtd">No
                </td>
                <td width="160px" class="trtd">Judul Buku
                </td>
                <td width="50px" class="trtd">Tahun Penerbitan
                </td>
                <td width="70px" class="trtd">ISBN
                </td>
                <td width="150px" class="trtd">Penerbit
                </td>
                <td width="90px" class="trtd">URL (jika ada)
                </td>
            </tr>
            		 			
            <asp:Repeater ID="rptBuku" runat="server" OnItemDataBound="rptBuku_ItemDataBound">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd">
                            <%# Container.ItemIndex + 1 %>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("judul")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("thn_penerbitan")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("isbn")%>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("penerbit")%>
                        </td>
                        <td class="trtd_left">
                            <asp:Label runat="server" ID="lblUrlBuku" Text=<%# Eval("url")%> ></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>


    <!-- Perolehan KI -->
   <table>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;" class="auto-style1">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                    Perolehan KI
                </td>
            </tr>
        </table>
        
   <table width="100%" class="trtd">

            <tr align="center" class="trtd">
                <td width="20px" class="trtd">No
                </td>
                <td width="100px" class="trtd">Judul KI
                </td>
                <td width="50px" class="trtd">Tahun Perolehan
                </td>
                <td width="70px" class="trtd">Jenis KI
                </td>
                <td width="70px" class="trtd">Nomor
                </td>
                <td width="60px" class="trtd">Status KI (terdaftar/granted)
                </td>
                <td width="90px" class="trtd">URL (jika ada)
                </td>
            </tr>
            	
            <asp:Repeater ID="rptKI" runat="server" OnItemDataBound="rptKI_ItemDataBound">
                <ItemTemplate>
                    <tr class="trtd">
                        <td class="trtd">
                            <%# Container.ItemIndex + 1 %>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("judul_hki")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("thn_pelaksanaan")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("jenis_hki")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("no_hki")%>
                        </td>
                        <td class="trtd">
                            <%# Eval("status_hki")%>
                        </td>
                        <td class="trtd_left" style="word-wrap: break-word;">
                            <asp:Label runat="server" ID="lblUrlKI" Text=<%# Eval("url")%> ></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>


<asp:Panel runat="server" ID="pnlRiwayatAbdimas">
    <div>&nbsp;</div>
    <table>
        <tr>
            <td style="font-family: Arial; font-size: 12px; font-weight: bold;">Riwayat Pengabdian Kepada Masyarakat
            </td>
        </tr>
    </table>
    <table width="100%" class="trtd">
        <tr align="center" class="trtd">
            <td width="20px" class="trtd">No
            </td>
            <td width="150px" class="trtd">Peran, Tahun
            </td>
            <td width="140px" class="trtd">Nama Skema
            </td>
            <td width="200px" class="trtd">Judul
            </td>
            <td width="40px" class="trtd">Dana Disetujui
            </td>
        </tr>
        <asp:Repeater ID="rptRiwayatPPM" runat="server" OnItemDataBound="rptKI_ItemDataBound">
            <ItemTemplate>
                <tr class="trtd">
                    <td class="trtd">
                        <%# Container.ItemIndex + 1 %>
                    </td>
                    <td class="trtd_left">
                        <%# Eval("peran_personil")%><br />
                        <br />
                        Tahun ke-<%# Eval("urutan_thn_usulan_kegiatan")%> dari <%# Eval("lama_kegiatan")%>
                        <br />
                        Tahun pelaksanaan: <%# Eval("thn_pelaksanaan_kegiatan")%>
                    </td>
                    <td class="trtd_left">
                        <%# Eval("nama_skema")%>
                    </td>
                    <td class="trtd_left">
                        <%# Eval("judul")%>
                    </td>
                    <td class="trtd_right" style="word-wrap: break-word;">
                        <%# Eval("dana_disetujui",  "{0:0,00}")  %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>


