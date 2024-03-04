<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pdfPersetujuan.aspx.cs" Inherits="simlitekkes.UserControls.Pengusul.report.pdfPersetujuan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
<style>
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
    <form id="form1" runat="server">
        <div>
        

            
        <div>&nbsp;</div>
<table>
    <tr>
        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">PERSETUJUAN USULAN
        </td>
    </tr>
</table>
<table width="100%" class="trtd">
    <tr align="center" class="trtd">
        <td width="50px" class="trtd">Tanggal Pengiriman
        </td>
        <td width="50px" class="trtd">Tanggal Persetujuan
        </td>
        <td width="120px" class="trtd">Nama Pimpinan Pemberi Persetujuan
        </td>
        <td width="120px" class="trtd">Sebutan Jabatan Unit
        </td>
        <td width="120px" class="trtd">Nama Unit Lembaga Pengusul
        </td>
    </tr>
    <tr class="trtd">
        <td class="trtd">
            <asp:Label runat="server" ID="lblTglKirim" Text="-"></asp:Label>
        </td>
        <td class="trtd">
            <asp:Label runat="server" ID="lblTglPersetujuan" Text="-"></asp:Label>
        </td>
        <td class="trtd">
            <asp:Label runat="server" ID="lblNamaPimpinan" Text="-"></asp:Label>
        </td>
        <td class="trtd">
            <asp:Label runat="server" ID="lblSebutanJabatan" Text="-"></asp:Label>
        </td>
        <td class="trtd">
            <asp:Label runat="server" ID="lblNamaUnit" Text="-"></asp:Label>
        </td>
    </tr>
</table>
       
        </div>
    </form>
</body>
</html>
