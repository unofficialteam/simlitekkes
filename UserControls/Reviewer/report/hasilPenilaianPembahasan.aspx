<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hasilPenilaianPembahasan.aspx.cs" Inherits="simlitekkes.UserControls.Reviewer.report.hasilPenilaianPembahasan1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .trtd {
            border: 1px solid black;
            text-align: center;
            border-collapse: Collapse;
            font-family: 'Arial Narrow', Arial, sans-serif;
            font-size: 12px;
            border-color: rgba(0,0,0, 0.5);
            padding: 5px;
        }

        .trtd_left {
            border: 1px solid black;
            text-align: left;
            border-collapse: Collapse;
            font-family: 'Arial Narrow', Arial, sans-serif;
            font-size: 12px;
            border-color: rgba(0,0,0, 0.5);
            padding: 5px;
        }

        .trtd_right {
            border: 1px solid black;
            text-align: right;
            border-collapse: Collapse;
            font-family: 'Arial Narrow', Arial, sans-serif;
            font-size: 12px;
            border-color: rgba(0,0,0, 0.5);
            padding: 5px;
        }

        .auto-style1 {
            height: 18px;
        }

        .auto-style2 {
            width: 10px;
            height: 18px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server" style="margin-left: 70px; margin-right: 50px;">
        <div>
            
            <table width="100%" style="font-size: 13px; font-family: 'Calibri (Body)';">
                <tr>
                    <td>
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
            <div style="text-align: center;">
                <asp:Label runat="server" ID="lblKategoriPenelitian" Text="HASIL EVALUASI DOKUMEN PROPOSAL PENELITIAN DASAR" ForeColor="White" BackColor="Black" Style="font-size: 14px; font-family: Arial; font-weight: bold; padding: 3px;"></asp:Label>

            </div>
            <hr style="margin-top: 8px;" />
        </div>

        <table style="width: 100%; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;"
            cellpadding="-1">
            <tr>
                <td style="vertical-align: top; width: 27%;">Judul Penelitian
                </td>
                <td style="width: 10px; vertical-align: top;">:</td>
                <td>
                    <asp:Label runat="server" ID="lblJudul"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;" class="auto-style1">Bidang Penelitian
                </td>
                <td style="vertical-align: top;" class="auto-style2">:</td>
                <td class="auto-style1">
                    <asp:Label runat="server" ID="lblBidangFokus"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Perguruan Tinggi
                </td>
                <td style="width: 10px; vertical-align: top;">:</td>
                <td>
                    <asp:Label runat="server" ID="lblNamaPT"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Program Studi
                </td>
                <td style="width: 10px;">:</td>
                <td>
                    <asp:Label runat="server" ID="lblNamaProdi"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ketua Peneliti
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="padding-left: 30px;">Nama Lengkap
                </td>
                <td style="width: 10px;">:</td>
                <td>
                    <asp:Label runat="server" ID="lblNamaLengkapKetua"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 30px;">NIDN/NIDK
                </td>
                <td style="width: 10px;">:</td>
                <td>
                    <asp:Label runat="server" ID="lblNidn"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 30px;">Jabatan Fungsional
                </td>
                <td style="width: 10px;">:</td>
                <td>
                    <asp:Label runat="server" ID="lblJabatanFungsional"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Nama Mitra (jika ada)
                </td>
                <td style="width: 10px;">:</td>
                <td>
                    <asp:Label runat="server" ID="lblNamaMitra" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Institusi Mitra (jika ada)
                </td>
                <td style="width: 10px;">:</td>
                <td>
                    <asp:Label runat="server" ID="lblNamaInstitusiMitra" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Lama Penelitian Keseluruhan 
                </td>
                <td style="width: 10px;">:</td>
                <td>
                    <asp:Label runat="server" ID="lblLamaKegiatan"></asp:Label>&nbsp;tahun
                </td>
            </tr>
            <tr style="font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">
                <td>&nbsp; 
                </td>
                <td>&nbsp; 
                </td>
            </tr>
        </table>

        <asp:Panel runat="server" ID="pnlBiayaPenelitian" Visible="true">
            <table style="font-size: 12px; font-family: Arial;">
                <tr>
                    <td style="font-weight: bold;">Biaya Penelitian:</td>
                </tr>
            </table>
            <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">
                <tr class="trtd">
                    <td style="width: 60%;" class="trtd">Pendanaan
                    </td>
                    <td style="width: 10%;" class="trtd">Tahun I
                    </td>
                    <td style="width: 10%;" class="trtd">Tahun II
                    </td>
                    <td style="width: 10%;" class="trtd">Tahun III
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rptPendanaan">
                    <ItemTemplate>
                        <tr>
                            <td class="trtd_left">
                                <%# Eval("pendanaan") %>
                            </td>
                            <td class="trtd_right">
                                <%# Eval("dana_tahun_1", "{0:0,00}") %>
                            </td>
                            <td class="trtd_right">
                                <%# Eval("dana_tahun_2", "{0:0,00}") %>
                            </td>
                            <td class="trtd_right">
                                <%# Eval("dana_tahun_3", "{0:0,00}") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>


                <asp:Repeater runat="server" ID="rptDanaMitra">
                    <ItemTemplate>
                        <tr>
                            <td class="trtd_left">
                                <%# Eval("kategori_mitra") %>
                            </td>
                            <td class="trtd_right">
                                <%# Convert.ToDecimal((Eval("dana_thn_1")) == System.DBNull.Value ? 0 : Eval("dana_thn_1")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>
                                <%--<%# Eval("dana_thn_1", "{0:0,00}") %>--%>
                            </td>
                            <td class="trtd_right">
                                <%# Convert.ToDecimal((Eval("dana_thn_2")) == System.DBNull.Value ? 0 : Eval("dana_thn_2")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>
                                <%--<%# Eval("dana_thn_2", "{0:0,00}") %>--%>
                            </td>
                            <td class="trtd_right">
                                <%# Convert.ToDecimal((Eval("dana_thn_3")) == System.DBNull.Value ? 0 : Eval("dana_thn_3")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%>
                                <%--<%# Eval("dana_thn_3", "{0:0,00}") %>--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </asp:Panel>

        <table style="font-size: 12px; font-family: Arial;">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="font-weight: bold;">Penilaian:</td>
            </tr>
        </table>
        <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">

            <tr>
                <td colspan="2" class="trtd">Komponen Penilaian
                </td>
                <td class="trtd">Capaian 
                </td>
                <td class="trtd">Skor
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rptPenilaianUsulan">
                <ItemTemplate>
                    <tr>
                        <td style="width: 30px;" class="trtd">
                            <%# Eval("no_baris") %>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("kriteria_penilaian") %>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("opsi_komponen_penilaian") %>
                        </td>
                        <td class="trtd" style="text-align:right;">
                            <%# Eval("nilai") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

            <tr>
                <td colspan="3" class="trtd_right">Total</td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblSubtotalPenilaian" Text="0"></asp:Label>
                </td>
            </tr>
        </table>             

        <table style="font-size: 12px; font-family: Arial;">
            <tr>
                <td style="font-weight: bold;">Komentar Umum
                </td>
            </tr>
        </table>
        <table style="font-size: 12px; font-family: Arial; width: 100%;">
            <tr>
                <td class="trtd_left" style="padding: 10px;">
                    <asp:Label runat="server" ID="lblKomentarUmum"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
        </table>

        <table style="font-size: 12px; font-family: Arial; width: 100%;">
            <tr>
                <td style="width: 70%;"></td>
                <td style="width: 30%;">
                    <asp:Label runat="server" ID="lblKotaTglBulanTahun" Text="Kota, tanggal-bulan-tahun"></asp:Label>

                </td>
            </tr>
            <tr>
                <td style="width: 70%;"></td>
                <td style="width: 30%;">Reviewer,<br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 70%;"></td>
                <td style="width: 30%;">(<asp:Label runat="server" ID="lblNamaLengkapReviewer" Text="Nama Reviewer"></asp:Label>)
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
