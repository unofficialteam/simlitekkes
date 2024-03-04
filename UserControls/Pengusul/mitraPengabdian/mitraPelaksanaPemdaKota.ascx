<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraPelaksanaPemdaKota.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraPelaksanaPemdaKota" %>

<div class="card mb-4">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Pelaksana Pengabdian</h5>
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Tipe Mitra</label>
                    <asp:DropDownList ID="ddlTipeMitra" runat="server" Enabled="true"
                        DataTextField="tipe_mitra" DataValueField="id_tipe_mitra"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Lembaga Mitra</label>
                    <asp:TextBox ID="tbNamaMitra" runat="server" CssClass="form-control" placeholder="Nama Lembaga"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Pimpinan Mitra</label>
                    <asp:TextBox ID="tbNamaPimpinanMitra" runat="server" CssClass="form-control" placeholder="Nama Pimpinan Lembaga"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>Jabatan</label>
                    <asp:TextBox ID="tbJabatan" runat="server" CssClass="form-control" placeholder="Jabatan Pimpinan"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label>Alamat</label>
                    <asp:TextBox ID="tbAlamatInstitusi" runat="server" CssClass="form-control"
                        placeholder="Isikan Alamat Institusi Mitra" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group row" style="padding-left: 15px;">
                    <h5 class="card-header-text" style="color: red;">KONTRIBUSI PENDANAAN (jika ada)</h5>
                </div>
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
            <script type="text/javascript">
                new AutoNumeric('.uang1', {
                    decimalPlaces: 0,
                    digitGroupSeparator: '.',
                    decimalCharacter: ',',
                    minimumValue: 0
                });
                new AutoNumeric('.uang2', {
                    decimalPlaces: 0,
                    digitGroupSeparator: '.',
                    decimalCharacter: ',',
                    minimumValue: 0
                });
                new AutoNumeric('.uang3', {
                    decimalPlaces: 0,
                    digitGroupSeparator: '.',
                    decimalCharacter: ',',
                    minimumValue: 0
                });
            </script>
        </div>
    </div>
</div>
