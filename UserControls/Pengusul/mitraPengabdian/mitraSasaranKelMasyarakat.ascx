<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraSasaranKelMasyarakat.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraSasaranKelMasyarakat" %>

<div class="card mb-4">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Sasaran</h5>
    </div>
    <div class="card-body">
        <div class="row">
            <%--<div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Jenis Mitra</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlJenisMitra" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="jenis_mitra" DataValueField="id_jenis_mitra" AutoPostBack="false"
                        CssClass="form-control select2">
                    </asp:DropDownList>
                </div>
            </div>--%>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Pimpinan Desa</label>
                    <asp:TextBox ID="tbNamaPimpinanDesa" runat="server" CssClass="form-control" placeholder="Pimpinan"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Alamat</label>
                    <asp:TextBox ID="tbAlamatMitraSasaran" runat="server" CssClass="form-control" placeholder="Alamat"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
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
            <div class="col-md-12">
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
            <%--            <div class="form-group row" style="padding-left: 15px;">
                <h5 class="card-header-text" style="color: red;">KELOMPOK SASARAN</h5>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Kelompok Mitra</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:TextBox ID="tbNamaKelMitra" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Pimpinan Mitra</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:TextBox ID="tbNamaPimpinanMitra" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Alamat</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:TextBox ID="tbAlamatKelSasaran" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Desa</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlProvinsiKelSasaran" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="nama_provinsi" DataValueField="kd_provinsi" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProvinsiKelSasaran_SelectedIndexChanged"
                        CssClass="form-control select2">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlKotaKelSasaran" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="nama_kota" DataValueField="kd_kota" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlKotaKelSasaran_SelectedIndexChanged"
                        CssClass="form-control select2">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2"></label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlKecamatanSasaran" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="nama_kecamatan" DataValueField="kd_kecamatan" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlKecamatanSasaran_SelectedIndexChanged"
                        CssClass="form-control select2">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2"></label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlDesaSasaran" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="nama_desa" DataValueField="kd_desa" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDesaSasaran_SelectedIndexChanged"
                        CssClass="form-control select2">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <asp:Label ID="lblInfoDesaPrioritas1" runat="server" BackColor="Yellow" ForeColor="red"></asp:Label>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Jenis Mitra</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlJenisMitraKelSasaran" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="jenis_mitra" DataValueField="id_jenis_mitra" AutoPostBack="true"
                        CssClass="form-control select2">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Bidang Pengembangan Mitra</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:TextBox ID="tbBidPengembanganMitra" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div>
                    <label class="col-xs-2 col-form-label form-control-label">(Minimal 1 bidang)</label>
                </div>
            </div>--%>
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
