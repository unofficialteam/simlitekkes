<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pdfRabAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.report.pdfRabAbdimas" %>


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
<br />

<table>
    <tr>
        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">5. ANGGARAN
        </td>
    </tr>
    <tr>
        <td  style="font-family: Arial; font-size: 12px; text-align: justify;">
            Rencana anggaran biaya PPM mengacu pada PMK yang berlaku dengan besaran minimum dan maksimum sebagaimana diatur pada buku Panduan Penelitian dan Pengabdian kepada Masyarakat Edisi 12.
        </td>
    </tr>
    <tr>
        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
            <asp:Label runat="server" ID="lblTotalDana" Text="0" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
            <span style="padding-right: 74%; font-weight: bold;">Tahun 1</span>
            <label class="p-l-15 p-r-15" style="display: inline-block;">
                Total Rp.&nbsp;<asp:Label ID="lblTahun1" runat="server" Text="0"></asp:Label>
            </label>
        </td>
    </tr>
</table>


<asp:ListView ID="lvRABTahun1" runat="server" 
    ItemPlaceholderID="itemPlaceHolder"
    ItemType="simlitekkes.Models.Pengusul.ItemRencanaAnggaran">
    <LayoutTemplate>
        <table class="trtd" width="100%">
            <thead>
                <tr style="background-color: lightgray;">
                    <th class="trtd">Jenis Pembelanjaan</th>
                    <th class="trtd">Item</th>
                    <th class="trtd">Satuan</th>
                    <th class="trtd">Vol.</th>
                    <th class="trtd">Biaya Satuan</th>
                    <th class="trtd">Total</th>
                </tr>
            </thead>
            <tbody>
                <tr id="itemPlaceHolder" runat="server"></tr>
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <td class="trtd_left"><%# Item.JenisPembelanjaan %></td>
            <td class="trtd_left"><%# Item.Item %></td>
            <td class="trtd_left"><%# Item.Satuan %></td>
            <td class="trtd"><%# Item.Volume %></td>
            <td class="trtd_right"><%# Item.Honor.ToString("N0") %></td>
            <td class="trtd_right"><%# (Item.Volume * Item.Honor).ToString("N0") %></td>
        </tr>
    </ItemTemplate>
</asp:ListView>


<asp:Panel runat="server" ID="panelThn2" Visible="true">
<br />
<table>
    <tr>
        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
            <span style="padding-right: 74%; font-weight: bold;">Tahun 2</span>
            <label class="p-l-15 p-r-15" style="display: inline-block; ">
                Total Rp.&nbsp;<asp:Label ID="lblTahun2" runat="server" Text="0"></asp:Label>
            </label>
        </td>
    </tr>
</table>

<asp:ListView ID="lvRABTahun2" runat="server"
    ItemPlaceholderID="itemPlaceHolder"
    ItemType="simlitekkes.Models.Pengusul.ItemRencanaAnggaran">
    <LayoutTemplate>
        <table class="trtd"  width="100%">
            <thead>
                <tr style="background-color: lightgray;">
                    <th class="trtd">Jenis Pembelanjaan</th>
                    <th class="trtd">Item</th>
                    <th class="trtd">Satuan</th>
                    <th class="trtd">Vol.</th>
                    <th class="trtd">Biaya Satuan</th>
                    <th class="trtd">Total</th>
                </tr>
            </thead>
            <tbody>
                <tr id="itemPlaceHolder" runat="server"></tr>
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <td class="trtd_left"><%# Item.JenisPembelanjaan %></td>
            <td class="trtd_left"><%# Item.Item %></td>
            <td class="trtd_left"><%# Item.Satuan %></td>
            <td class="trtd"><%# Item.Volume %></td>
            <td class="trtd_right"><%# Item.Honor.ToString("N0") %></td>
            <td class="trtd_right"><%# (Item.Volume * Item.Honor).ToString("N0") %></td>
        </tr>
    </ItemTemplate>
</asp:ListView>

</asp:Panel>

<asp:Panel runat="server" ID="panelThn3" Visible="true">
<br />
<table>
    <tr>
        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
            <span style="padding-right: 74%; font-weight: bold;">Tahun 3</span>
            <label class="p-l-15 p-r-15" style="display: inline-block; ">
              Total Rp.&nbsp;<asp:Label ID="lblTahun3" runat="server" Text="0"></asp:Label>
            </label>
        </td>
    </tr>
</table>
    
<asp:ListView ID="lvRABTahun3" runat="server"
    ItemPlaceholderID="itemPlaceHolder"
    ItemType="simlitekkes.Models.Pengusul.ItemRencanaAnggaran">
    <LayoutTemplate>
        <table class="trtd"  width="100%">
            <thead>
                <tr style="background-color: lightgray;">
                    <th class="trtd">Jenis Pembelanjaan</th>
                    <th class="trtd">Item</th>
                    <th class="trtd">Satuan</th>
                    <th class="trtd">Vol.</th>
                    <th class="trtd">Biaya Satuan</th>
                    <th class="trtd">Total</th>
                </tr>
            </thead>
            <tbody>
                <tr id="itemPlaceHolder" runat="server"></tr>
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr class="trtd_left">
            <td class="trtd_left"><%# Item.JenisPembelanjaan %></td>
            <td class="trtd_left"><%# Item.Item %></td>
            <td class="trtd_left"><%# Item.Satuan %></td>
            <td class="trtd"><%# Item.Volume %></td>
            <td class="trtd_right"><%# Item.Honor.ToString("N0") %></td>
            <td class="trtd_right"><%# (Item.Volume * Item.Honor).ToString("N0") %></td>
        </tr>
    </ItemTemplate>
</asp:ListView>
    
</asp:Panel>
