<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraPelaksanaPTMitra.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraPelaksanaPTMitra" %>

<div class="card">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Pelaksana Pengabdian</h5>
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Perguruan Tinggi</label>
                    <asp:DropDownList ID="ddlPTMitra" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="nama_institusi" DataValueField="id_institusi"
                        CssClass="form-control select2">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Pimpinan</label>
                    <asp:TextBox ID="tbNamaPimpinan" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Jabatan</label>
                    <asp:TextBox ID="tbJabatan" runat="server" CssClass="form-control" placeholder="Rektor/Wakil Rektor"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-12">
                    <label>Alamat</label>
                    <asp:TextBox ID="tbAlamatInstitusi" runat="server" CssClass="form-control" placeholder="Isikan alamat surat institusi mitra" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 mt-2" style="padding-left: 15px; display: none">
                <hr />
                <h5 class="card-header-text" style="color: red;">KONTRIBUSI PENDANAAN</h5>
            </div>
            <div class="col-md-12">
                <div class="form-group row">
                    <asp:Label ID="lblPendanaanThn1" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 1" Visible="false"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn1" runat="server" CssClass="form-control uang1" ClientIDMode="Static" Visible="false"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblPendanaanThn2" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 2" Visible="false"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn2" runat="server" CssClass="form-control uang2" ClientIDMode="Static" Visible="false"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblPendanaanThn3" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 3" Visible="false"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn3" runat="server" CssClass="form-control uang3" ClientIDMode="Static" Visible="false"></asp:TextBox>
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
                new AutoNumeric('.jarak', {
                    decimalPlaces: 0,
                    minimumValue: 1,
                    maximumValue: 200
                });
            </script>
        </div>
    </div>
</div>
