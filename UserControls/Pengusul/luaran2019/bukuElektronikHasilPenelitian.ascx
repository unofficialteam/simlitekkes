<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bukuElektronikHasilPenelitian.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran2019.bukuElektronikHasilPenelitian" %>


<div class="row">
    <div class="col-lg-4 text-right">
        Luaran
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlJenisLuaran" CssClass="form-control" Width="300" AutoPostBack="true"
            DataTextField="nama_jenis_luaran" DataValueField="id_jenis_luaran" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>
            <asp:ListItem Text="Monograf" Value="1"></asp:ListItem>
            <asp:ListItem Text="Buku Referensi" Value="2"></asp:ListItem>
            <asp:ListItem Text="Buku Ajar" Value="3"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Rencana Nama Penerbit
    </div>
    <div class="col-lg-8 text-left">
         <asp:TextBox runat="server" ID="tbRencanaNamaPerbit" CssClass="form-control" Width="80%">
        </asp:TextBox>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Status
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" Width="300" AutoPostBack="true"
            DataTextField="nama_target_capaian_luaran"  DataValueField="id_target_capaian_luaran"
            OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Text="Online ber ISBN" Value="1"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<br />

<div class="row">
    <div class="col-lg-4 text-right">
        Bukti Luaran <span style="color: red;">(Tahun <asp:Label runat="server" ID="lblThnKe" Text=""></asp:Label>)</span>
    </div>
    <div class="col-lg-8 text-left">
       <asp:Label runat="server" ID="lblInfoBuktiLuaran" Text=""></asp:Label>
    </div>
</div>
<br />

<div class="row">
    <div class="col-lg-4 text-right">
        
    </div>
    <div class="col-lg-8 text-left">
        <asp:LinkButton runat="server" ID="lbSimpan" CssClass="btn btn-success btn-sm" Text="Simpan"
            OnClick="lbSimpan_Click" Width="100"></asp:LinkButton>&nbsp;&nbsp;
        <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger btn-sm" Text="Batal" 
            OnClick="lbBatal_Click" Width="100"></asp:LinkButton>
    </div>
</div>
<br />