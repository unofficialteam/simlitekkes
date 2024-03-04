<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="publikasiPasca.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran2019.publikasiPasca" %>

<div class="row">
    <div class="col-lg-4 text-right">
        <b>Luaran 1</b>
    </div>
    <div class="col-lg-8 text-left">
    </div>
</div>
<br />

<div class="row">
    <div class="col-lg-4 text-right">
        Luaran
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlJenisLuaran" CssClass="form-control" Width="300" 
            DataTextField="nama_jenis_luaran" DataValueField="id_jenis_luaran" OnSelectedIndexChanged="ddlTargetStatusLuaran_SelectedIndexChanged"  >
            <asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>
            <asp:ListItem Text="Test" Value="1"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<br />
<%--<asp:Panel runat="server" ID="panelUrutanThnLuaranTambahan" >
<div class="row">
    <div class="col-lg-4 text-right">
        Tahun
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlUrutanTahunLuaranTambahan" CssClass="form-control" Width="120" 
            AppendDataBoundItems="true" >
        </asp:DropDownList>
    </div>
</div>
<br />
</asp:Panel>--%>
<div class="row">
    <div class="col-lg-4 text-right">
        Rencana Nama Jurnal
    </div>
    <div class="col-lg-8 text-left">
       <asp:TextBox runat="server" ID="tbRencanaNamaJurnal" CssClass="form-control" placeholder="Nama Jurnal" Width="300"></asp:TextBox>
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
        <b>Luaran 2</b>
    </div>
    <div class="col-lg-8 text-left">
    </div>
</div>
<br />

<div class="row">
    <div class="col-lg-4 text-right">
        Luaran
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" Width="300" 
            DataTextField="nama_jenis_luaran" DataValueField="id_jenis_luaran" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"  >
            <asp:ListItem Text="Artikel di jurnal internasional terindeks di pengindeks bereputasi" Value="54"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<br />
<%--<asp:Panel runat="server" ID="panelUrutanThnLuaranTambahan" >
<div class="row">
    <div class="col-lg-4 text-right">
        Tahun
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlUrutanTahunLuaranTambahan" CssClass="form-control" Width="120" 
            AppendDataBoundItems="true" >
        </asp:DropDownList>
    </div>
</div>
<br />
</asp:Panel>--%>
<div class="row">
    <div class="col-lg-4 text-right">
        Rencana Nama Jurnal
    </div>
    <div class="col-lg-8 text-left">
       <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" placeholder="Nama Jurnal" Width="300"></asp:TextBox>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Status
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-control" Width="300" 
            DataTextField="nama_target_capaian_luaran"  DataValueField="id_target_capaian_luaran" AutoPostBack="true" 
           OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" >
            <asp:ListItem Value="0">--Pilih--</asp:ListItem>
            <asp:ListItem Text="Accepted" Value="118"></asp:ListItem>
            <asp:ListItem Value="119">Published</asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Bukti Luaran <span style="color: red;">(Tahun <asp:Label runat="server" ID="Label1" Text=""></asp:Label>)</span>
    </div>
    <div class="col-lg-8 text-left">
       <asp:Label runat="server" ID="Label2" Text=""></asp:Label>
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