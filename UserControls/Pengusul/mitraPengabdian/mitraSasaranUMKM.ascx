<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraSasaranUMKM.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraSasaranUMKM" %>

<div class="card">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Sasaran</h5>
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-4" style="display: none;">
                <div class="form-group">
                    <label>Tipe Mitra</label>
                    <asp:DropDownList ID="ddlTipeMitra" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="tipe_mitra" DataValueField="id_tipe_mitra"
                        CssClass="form-control select2" Visible="false">
                        <asp:ListItem Text="Kelompok Masyarakat" Value="5" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4" style="display: none;">
                <div class="form-group">
                    <label>Jenis Mitra</label>
                    <asp:DropDownList ID="ddlJenisMitra" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="jenis_mitra" DataValueField="id_jenis_mitra"
                        CssClass="form-control select2" Visible="false">
                        <asp:ListItem Text="Tidak ada" Value="0" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Pimpinan Mitra</label>
                    <asp:TextBox ID="tbNamaMitra" runat="server" CssClass="form-control" placeholder="Nama Pimpinan Mitra"></asp:TextBox>
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
                    <label>Nama Mitra</label>
                    <asp:TextBox ID="tbNamaKelMitra" runat="server" CssClass="form-control" placeholder="Nama Mitra"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group">
                    <label>Alamat</label>
                    <asp:TextBox ID="tbAlamatMitraSasaran" runat="server" CssClass="form-control" placeholder="Alamat Surat Mitra" TextMode="MultiLine"></asp:TextBox>
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
            <div class="col-md-8">
                <div class="form-group">
                    <label>Bidang Masalah Mitra</label>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:TextBox ID="tbBidangMasalahMitra" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div >
                            <asp:Label ID="lblinfoBidang" runat="server" class="col-xs-2 col-form-label form-control-label" Text="(Minimal 2 Bidang)" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Jarak Mitra ke PT</label>
                    <div class="row">
                        <div class="col-md-10">
                            <asp:TextBox ID="tbJarak" runat="server" CssClass="form-control jarak" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>KM</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group" style="padding-left: 15px;">
                    <h5 class="card-header-text" style="color: red;">KONTRIBUSI PENDANAAN (Jika Ada)</h5>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group row">
                    <asp:Label ID="lblPendanaanThn1" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Rp."></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn1" runat="server" CssClass="form-control uang1" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div style="display: none;">
                    <asp:Label ID="lblPendanaanThn2" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 2 Rp."></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn2" runat="server" CssClass="form-control uang2" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblPendanaanThn3" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 3 Rp."></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn3" runat="server" CssClass="form-control uang3" ClientIDMode="Static"></asp:TextBox>
                    </div>
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
