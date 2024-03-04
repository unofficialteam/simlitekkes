<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bookChapter.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran2019.bookChapter" %>


<div class="row">
    <div class="col-lg-4 text-right">
        Luaran
    </div>
    <div class="col-lg-8 text-left">
        <asp:DropDownList runat="server" ID="ddlJenisLuaran" CssClass="form-control" Width="300" AutoPostBack="true"
            DataTextField="nama_jenis_luaran" DataValueField="id_jenis_luaran" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>
            <asp:ListItem Text="Book Chapter" Value="1"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Rencana Nama Penerbit 1<br />
        <b style="color:red; font-size:x-small;">untuk book chapter 1</b>
    </div>
    <div class="col-lg-8 text-left">
        <asp:TextBox runat="server" ID="tbRencanaNamaPerbit1" CssClass="form-control" Width="80%" TextMode="MultiLine">
        </asp:TextBox>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Rencana Nama Penerbit 2<br />
        <b style="color:red; font-size:x-small;">untuk book chapter 2</b>
    </div>
    <div class="col-lg-8 text-left">
        <asp:TextBox runat="server" ID="tbRencanaNamaPerbit2" CssClass="form-control" Width="80%" TextMode="MultiLine">
        </asp:TextBox>
    </div>
</div>
<br />
<div class="row">
    <div class="col-lg-4 text-right">
        Rencana Nama Penerbit 3<br />
        <b style="color:red; font-size:x-small;">untuk book chapter 3</b>
    </div>
    <div class="col-lg-8 text-left">
        <asp:TextBox runat="server" ID="tbRencanaNamaPerbit3" CssClass="form-control" Width="80%" TextMode="MultiLine">
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
            <asp:ListItem Text="Terbit" Value="1"></asp:ListItem>
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