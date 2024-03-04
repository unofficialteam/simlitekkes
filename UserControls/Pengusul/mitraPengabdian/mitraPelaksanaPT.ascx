<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraPelaksanaPT.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraPelaksanaPT" %>

<div class="card">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Pelaksana Pengabdian</h5>
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-4">
                <label>Nama Pimpinan</label>
                <asp:TextBox ID="tbNamaPimpinan" runat="server" CssClass="form-control" placeholder="Isikan Nama Pimpinan Mitra"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label>Jabatan</label>
                <asp:TextBox ID="tbJabatan" runat="server" CssClass="form-control" placeholder="Isikan jabatan"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label>Alamat</label>
                <asp:TextBox ID="tbAlamatInstitusi" runat="server" CssClass="form-control" placeholder="Isikan alamat surat institusi mitra" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
            <div class="col-md-12" style="padding-left: 15px;">
                <h5 class="card-header-text" style="color: red;">KONTRIBUSI PENDANAAN</h5>
            </div>
            <div class="col-md-12">
                <div class="form-group row">
                    <asp:Label ID="lblPendanaanThn1" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 1"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn1" runat="server" CssClass="form-control uang1" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblPendanaanThn2" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 2"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn2" runat="server" CssClass="form-control uang2" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblPendanaanThn3" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 3"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn3" runat="server" CssClass="form-control uang3" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    new AutoNumeric('.uang1', {
        decimalPlaces: 0,
        digitGroupSeparator: '.',
        decimalCharacter: ',',
        minimumValue: 0 });
    new AutoNumeric('.uang2', {
        decimalPlaces: 0,
        digitGroupSeparator: '.',
        decimalCharacter: ',',
        minimumValue: 0 });
    new AutoNumeric('.uang3', {
        decimalPlaces: 0,
        digitGroupSeparator: '.',
        decimalCharacter: ',',
        minimumValue: 0 });
</script>
