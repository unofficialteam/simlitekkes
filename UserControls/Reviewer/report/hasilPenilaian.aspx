<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hasilPenilaian.aspx.cs" Inherits="simlitekkes.UserControls.Reviewer.report.hasilPenilaian1" %>

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
        <asp:Panel runat="server" ID="pnlBiayaPenelitian" Visible="false">
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
                            <td class="trtd">
                                <%# Eval("dana_tahun_1", "{0:0,00}") %>
                            </td>
                            <td class="trtd">
                                <%# Eval("dana_tahun_2", "{0:0,00}") %>
                            </td>
                            <td class="trtd">
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
                            <td class="trtd">
                                <%# Eval("dana_thn_1", "{0:0,00}") %>
                            </td>
                            <td class="trtd">
                                <%# Eval("dana_thn_2", "{0:0,00}") %>
                            </td>
                            <td class="trtd">
                                <%# Eval("dana_thn_3", "{0:0,00}") %>
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
                <td style="font-weight: bold;">Kelayakan Administrasi:
                </td>
            </tr>
        </table>
        <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">
            <tr>
                <td style="width: 30px;" class="trtd">No
                </td>
                <td style="width: 400px;" class="trtd">Komponen
                </td>
                <td colspan="2" style="text-align: center;" class="trtd">Penilaian </td>
            </tr>
            <asp:Repeater runat="server" ID="rptHasilPenilaianAdministrasi"
                OnItemDataBound="rptHasilPenilaianAdministrasi_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td class="trtd">
                            <%# Eval("no_urut") %>
                        </td>
                        <td class="trtd_left">
                            <%# Eval("kriteria_penilaian") %>
                        </td>
                        <td class="trtd">
                            <asp:Label runat="server" ID="lblYes" Text='<%# Eval("nilai") %>' Visible="false"></asp:Label>
                            <asp:RadioButton runat="server" ID="rbYes" Text="Ya" />
                        </td>
                        <td class="trtd">
                            <asp:RadioButton runat="server" ID="rbNo" Text="No" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        &nbsp;<br />
        <table border="1" style="border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">

            <tr>
                <td class="trtd">Hasil Penilaian Kelayakan Administrasi:
                </td>
                <td class="trtd">
                    <asp:RadioButton runat="server" ID="rbLayak" Text="Layak" Checked="true" />
                </td>
                <td class="trtd">
                    <asp:RadioButton runat="server" ID="rbTdkLayak" Text="Tidak Layak" Checked="false" />
                </td>
            </tr>
        </table>
        <table style="font-size: 12px; font-family: Arial;">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="font-weight: bold;">Penilaian:</td>
            </tr>
            <tr>
                <td style="font-weight: bold;">I. Kelayakan Rekam Jejak Pengusul</td>
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
            <asp:Repeater runat="server" ID="rptKelayakanRekamJejak">
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
                <td colspan="3" class="trtd_right">Sub total (40%)</td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblSubtotalRekamJejak" Text="0"></asp:Label>
                </td>
            </tr>
        </table>

        <table style="font-size: 12px; font-family: Arial;">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="font-weight: bold;">II. Kelayakan Usulan Penelitian</td>
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
                <td colspan="3" class="trtd_right">Sub Total (60%)</td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblSubtotalPenilaian" Text="0"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="trtd_right">Total (I+II)</td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTotal1N2" Text="0"></asp:Label>
                </td>
            </tr>
        </table>

        <asp:Panel runat="server" ID="pnlThn12019" Visible="false">
            <div>
                <table style="font-size: 12px; font-family: Arial; width: 100%;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">III. Kelayakan RAB</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Tahun ke-1</td>
                    </tr>
                </table>

                <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">

                    <tr class="trtd">
                        <td rowspan="2" style="width: 20px;" class="trtd">No</td>
                        <td rowspan="2" class="trtd">Item</td>
                        <td colspan="3" class="trtd">Usulan Peneliti</td>
                        <td colspan="3" class="trtd">Justifikasi Reviewer</td>
                        <td rowspan="2" class="trtd">Komentar</td>
                    </tr>
                    <tr class="trtd">
                        <td class="trtd">Harga
                        <br />
                            Satuan
                        <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                        <br />
                            (Rp)</td>
                        <td class="trtd">Harga
                        <br />
                            Satuan
                        <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                        <br />
                            (Rp)</td>
                    </tr>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Honor</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptHonorThn1">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Belanja Non Operasional Lainnya</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBelanjaNonOpLainyaThn1">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Belanja Bahan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBelanjaThn1">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Belanja Perjalanan Lainnya</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBelanjaPerjLainyaThn1">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

                <table style="font-size: 12px; font-family: Arial;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Tahun ke-2</td>
                    </tr>
                </table>

                <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">
                    <tr class="trtd">
                        <td rowspan="2" style="width: 20px;" class="trtd">No</td>
                        <td rowspan="2" class="trtd">Item</td>
                        <td colspan="3" class="trtd">Usulan Peneliti</td>
                        <td colspan="3" class="trtd">Justifikasi Reviewer</td>
                        <td rowspan="2" class="trtd">Komentar</td>
                    </tr>
                    <tr class="trtd">
                        <td class="trtd">Harga
                    <br />
                            Satuan
                    <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                    <br />
                            (Rp)</td>
                        <td class="trtd">Harga
                    <br />
                            Satuan
                    <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                    <br />
                            (Rp)</td>
                    </tr>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Honor</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptHonorThn2">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Belanja Non Operasional Lainnya</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBelanjaNonOpLainyaThn2">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("xsatuan") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Belanja Bahan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBelanjaThn2">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Belanja Perjalanan Lainnya</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBelanjaPerjLainyaThn2">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

                <table style="font-size: 12px; font-family: Arial;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Tahun ke-3</td>
                    </tr>
                </table>

                <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">
                    <tr class="trtd">
                        <td rowspan="2" style="width: 20px;" class="trtd">No</td>
                        <td rowspan="2" class="trtd">Item</td>
                        <td colspan="3" class="trtd">Usulan Peneliti</td>
                        <td colspan="3" class="trtd">Justifikasi Reviewer</td>
                        <td rowspan="2" class="trtd">Komentar</td>
                    </tr>
                    <tr class="trtd">
                        <td class="trtd">Harga
                    <br />
                            Satuan
                    <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                    <br />
                            (Rp)</td>
                        <td class="trtd">Harga
                    <br />
                            Satuan
                    <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                    <br />
                            (Rp)</td>
                    </tr>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Honor</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptHonorThn3">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Belanja Non Operasional Lainnya</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBelanjaNonOpLainyaThn3">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Belanja Bahan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBelanjaThn3">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Belanja Perjalanan Lainnya</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBelanjaPerjLainyaThn3">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan") %></td>
                                <td class="trtd"><%# Eval("xvolume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlThn12020" Visible="false">
            <div>
                <%--Tabel Tahun ke 1--%>
                <table style="font-size: 12px; font-family: Arial; width: 100%;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">III. Kelayakan RAB</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Tahun ke-1</td>
                    </tr>
                </table>
                <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">

                    <tr class="trtd">
                        <td rowspan="2" style="width: 20px;" class="trtd">No</td>
                        <td rowspan="2" class="trtd">Item</td>
                        <td colspan="3" class="trtd">Usulan Peneliti</td>
                        <td colspan="3" class="trtd">Justifikasi Reviewer</td>
                        <td rowspan="2" class="trtd">Komentar</td>
                    </tr>
                    <tr class="trtd">
                        <td class="trtd">Harga
                        <br />
                            Satuan
                        <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                        <br />
                            (Rp)</td>
                        <td class="trtd">Harga
                        <br />
                            Satuan
                        <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                        <br />
                            (Rp)</td>
                    </tr>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Bahan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBahanThn1">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Pengumpulan Data</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptPengumpulanDataThn1">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Sewa Peralatan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptSewaPeralatanThn1">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Analisis Data</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAnalisisDataThn1">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Pelaporan, Luaran Wajib, dan Luaran Tambahan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptPelaporanThn1">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

                <%--Tabel Tahun ke 2--%>
                <table style="font-size: 12px; font-family: Arial; width: 100%;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">III. Kelayakan RAB</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Tahun ke-2</td>
                    </tr>
                </table>
                <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">

                    <tr class="trtd">
                        <td rowspan="2" style="width: 20px;" class="trtd">No</td>
                        <td rowspan="2" class="trtd">Item</td>
                        <td colspan="3" class="trtd">Usulan Peneliti</td>
                        <td colspan="3" class="trtd">Justifikasi Reviewer</td>
                        <td rowspan="2" class="trtd">Komentar</td>
                    </tr>
                    <tr class="trtd">
                        <td class="trtd">Harga
                        <br />
                            Satuan
                        <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                        <br />
                            (Rp)</td>
                        <td class="trtd">Harga
                        <br />
                            Satuan
                        <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                        <br />
                            (Rp)</td>
                    </tr>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Bahan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBahanThn2">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Pengumpulan Data</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptPengumpulanDataThn2">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Sewa Peralatan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptSewaPeralatanThn2">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Analisis Data</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAnalisisDataThn2">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Pelaporan, Luaran Wajib, dan Luaran Tambahan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptPelaporanThn2">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

                <%--Tabel Tahun ke 3--%>
                <table style="font-size: 12px; font-family: Arial; width: 100%;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">III. Kelayakan RAB</td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold;">Tahun ke-3</td>
                    </tr>
                </table>
                <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">

                    <tr class="trtd">
                        <td rowspan="2" style="width: 20px;" class="trtd">No</td>
                        <td rowspan="2" class="trtd">Item</td>
                        <td colspan="3" class="trtd">Usulan Peneliti</td>
                        <td colspan="3" class="trtd">Justifikasi Reviewer</td>
                        <td rowspan="2" class="trtd">Komentar</td>
                    </tr>
                    <tr class="trtd">
                        <td class="trtd">Harga
                        <br />
                            Satuan
                        <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                        <br />
                            (Rp)</td>
                        <td class="trtd">Harga
                        <br />
                            Satuan
                        <br />
                            (Rp)</td>
                        <td class="trtd">Volume</td>
                        <td class="trtd">Total
                        <br />
                            (Rp)</td>
                    </tr>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Bahan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptBahanThn3">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Pengumpulan Data</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptPengumpulanDataThn3">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Sewa Peralatan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptSewaPeralatanThn3">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Analisis Data</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAnalisisDataThn3">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9" class="trtd_left" style="font-weight: bold;">Jenis Belanja: Pelaporan, Luaran Wajib, dan Luaran Tambahan</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptPelaporanThn3">
                        <ItemTemplate>
                            <tr class="trtd">
                                <td class="trtd"><%# Container.ItemIndex + 1 %></td>
                                <td class="trtd_left"><%# Eval("nama_item") %></td>
                                <td class="trtd"><%# Eval("harga_satuan", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume") %></td>
                                <td class="trtd"><%# Eval("total_biaya", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("harga_satuan_rekomendasi", "{0:0,00}") %></td>
                                <td class="trtd"><%# Eval("volume_rekomendasi") %></td>
                                <td class="trtd"><%# Eval("total_justifikasi", "{0:0,00}") %></td>
                                <td class="trtd_left"><%# Eval("komentar") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>

        <table style="font-size: 12px; font-family: Arial;">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="font-weight: bold;">Rekapitulasi RAB
                </td>
            </tr>
        </table>
        <table border="1" style="width: 100%; border: 1px solid lightgray; border-collapse: collapse; font-size: 12px; font-family: 'Arial Narrow', Arial, sans-serif, Times, serif;">

            <tr class="trtd">
                <td class="trtd" style="width: 30px;">No
                </td>
                <td class="trtd" style="width: 100px;">Tahun Pelaksanaan
                </td>
                <td class="trtd">Total Usulan Peneliti (Rp) </td>
                <td class="trtd">Jastifikasi Reviewer (Rp) </td>
            </tr>
            <%--<asp:Repeater runat="server" ID="Repeater14">
            <ItemTemplate>--%>
            <tr class="trtd">
                <td class="trtd">1
                </td>
                <td class="trtd">1
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTotalThn1" Text="0"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTotalThn1Rev" Text="0"></asp:Label>
                </td>
            </tr>
            <tr class="trtd">
                <td class="trtd">2
                </td>
                <td class="trtd">2
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTotalThn2" Text="0"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTotalThn2Rev" Text="0"></asp:Label>
                </td>
            </tr>
            <tr class="trtd">
                <td class="trtd">3
                </td>
                <td class="trtd">3
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTotalThn3" Text="0"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTotalThn3Rev" Text="0"></asp:Label>
                </td>
            </tr>
            <%--</ItemTemplate>
        </asp:Repeater>--%>

            <tr>
                <td class="trtd_right" colspan="2">Total (Rp)
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTotalDiusulkan" Text="0"></asp:Label>
                </td>
                <td class="trtd">
                    <asp:Label runat="server" ID="lblTotalJustifikasiReviewer" Text="0"></asp:Label>
                </td>
            </tr>
        </table>
        &nbsp;<br />

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
