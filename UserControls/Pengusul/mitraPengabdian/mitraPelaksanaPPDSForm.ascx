<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraPelaksanaPPDSForm.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraPelaksanaPPDSForm" %>

<div class="card mb-4">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Pelaksana</h5>
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Stakeholder</label>
                    <asp:TextBox ID="tbNamaStakeholder" runat="server" CssClass="form-control" placeholder="Nama Stakeholder"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Pimpinan</label>
                    <asp:TextBox ID="tbNamaPimpinan" runat="server" CssClass="form-control" placeholder="Nama Pimpinan"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Jabatan</label>
                    <asp:TextBox ID="tbJabatan" runat="server" CssClass="form-control" placeholder="Jabatan"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Alamat</label>
                    <asp:TextBox ID="tbAlamatMitraSasaran" runat="server" CssClass="form-control" placeholder="Alamat"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4" style="display:none;">
                <div class="form-group">
                    <label>Jarak</label>
                    <div class="row">
                        <div class="col-md-10">
                            <asp:TextBox ID="tbJarak" runat="server" CssClass="form-control jarak" TextMode="Number" palceholder="jarak (isi hanya angka)"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            KM
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="display:none;">
                <div class="form-group">
                    <label>Nama Desa</label>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlProvinsi" runat="server" Enabled="true" ClientIDMode="Static"
                                DataTextField="nama_provinsi" DataValueField="kd_provinsi" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlProvinsi_SelectedIndexChanged"
                                CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlKota" runat="server" Enabled="true" ClientIDMode="Static"
                                DataTextField="nama_kota" DataValueField="kd_kota" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlKota_SelectedIndexChanged"
                                CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlKecamatan" runat="server" Enabled="true" ClientIDMode="Static"
                                DataTextField="nama_kecamatan" DataValueField="kd_kecamatan" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlKecamatan_SelectedIndexChanged"
                                CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDesa" runat="server" Enabled="true" ClientIDMode="Static"
                                DataTextField="nama_desa" DataValueField="kd_desa" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlDesa_SelectedIndexChanged"
                                CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblInfoDesaPrioritas" runat="server" BackColor="Yellow" ForeColor="red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="padding-left: 15px;">
                <h5 class="card-header-text" style="color: red;">KONTRIBUSI PENDANAAN (JIka ada)</h5>
            </div>
            <div class="col-md-12">
                <div class="form-group row">
                    <asp:Label ID="lblPendanaanThn1" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 1" Visible="false"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn1" runat="server" CssClass="form-control uang1" ClientIDMode="Static"></asp:TextBox>
                    </div>

                    <%--<asp:Label ID="lblPendanaanThn2" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 2"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn2" runat="server" CssClass="form-control uang2" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblPendanaanThn3" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 3"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn3" runat="server" CssClass="form-control uang3" ClientIDMode="Static"></asp:TextBox>
                    </div>--%>
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
    new AutoNumeric('.jarak', {
        decimalPlaces: 0,
        digitGroupSeparator: '.',
        decimalCharacter: ',',
        minimumValue: 0 });
</script>
