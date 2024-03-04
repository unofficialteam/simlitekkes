<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="publikasiProsidingLuaranTambahan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran2019.publikasiProsidingLuaranTambahan" %>

<div class="row">
    <div class="col-lg-4 text-right">
        Luaran
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlJenisLuaran" CssClass="form-control" Width="300" 
            DataTextField="nama_jenis_luaran" DataValueField="id_jenis_luaran" OnSelectedIndexChanged="ddlTargetStatusLuaran_SelectedIndexChanged">
            <asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>
            <asp:ListItem Text="Test" Value="1"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Rencana Nama Conference
    </div>
    <div class="col-lg-8 text-left">
       <asp:TextBox runat="server" ID="tbRencanaNamaKonferensi1" CssClass="form-control" placeholder="Nama Konferensi 1" Width="300"
           TextMode="MultiLine"></asp:TextBox>
    </div>
</div>

<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Status
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlTargetStatusLuaran" CssClass="form-control" Width="300" 
            DataTextField="nama_target_capaian_luaran"  DataValueField="id_target_capaian_luaran" AutoPostBack="true" 
           OnSelectedIndexChanged="ddlTargetStatusLuaran_SelectedIndexChanged" >
            <asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>
            <asp:ListItem Text="Test" Value="1"></asp:ListItem>
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