<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="kelompokSasaranPPMUPT.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.kelompokSasaranPPMUPT" %>

<div class="form-group row">
    <label class="col-xs-2 col-form-label form-control-label">Tipe Mitra</label>
    <div class="col-sm-4 col-xs-12">
        <asp:DropDownList ID="ddlTipeMitra" runat="server" CssClass="form-control"
            DataValueField="id_tipe_mitra" DataTextField="tipe_mitra">
        </asp:DropDownList>
    </div>
</div>
<div class="form-group row">
    <label class="col-xs-2 col-form-label form-control-label">Jenis Mitra</label>
    <div class="col-sm-5 col-xs-12">
        <asp:DropDownList ID="ddlJenisMitra" runat="server" CssClass="form-control"
            DataValueField="id_jenis_mitra" DataTextField="jenis_mitra">
            <asp:ListItem Text="Produktif Ekonomi/Wirausahawan" Value="1" Selected="True"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<div class="form-group row">
    <label class="col-xs-2 col-form-label form-control-label">Nama Kelompok Mitra</label>
    <div class="col-sm-6 col-xs-12">
        <asp:TextBox ID="tbNamaMitra" runat="server" CssClass="form-control" placeholder="Nama Mitra"></asp:TextBox>
    </div>
</div>
<div class="form-group row">
    <label class="col-xs-2 col-form-label form-control-label">Nama Pimpinan Mitra</label>
    <div class="col-sm-6 col-xs-12">
        <asp:TextBox ID="tbNamaPimpinan" runat="server" CssClass="form-control" placeholder="Nama Pimpinan"></asp:TextBox>
    </div>
</div>
<div class="form-group row">
    <label class="col-xs-2 col-form-label form-control-label">Alamat Kelompok Mitra</label>
    <div class="col-sm-10 col-xs-12">
        <asp:TextBox ID="tbAlamatMitra" runat="server" CssClass="form-control"
            placeholder="Alamat" TextMode="MultiLine"></asp:TextBox>
    </div>
</div>
<div class="form-group row">
    <label class="col-xs-2 col-form-label form-control-label">Bidang Pengembangan Mitra</label>
    <div class="col-sm-10 col-xs-12">
        <asp:TextBox ID="tbBidangUsahaMitra" runat="server" CssClass="form-control" placeholder="Bidang Usaha"></asp:TextBox>
    </div>
    <label class="col-xs-2 col-form-label form-control-label"></label>
</div>


