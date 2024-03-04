<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraSasaranKelMasyarakatPKW.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraSasaranKelMasyarakatPKW" %>

<div class="card">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Sasaran</h5>
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Pimpinan Mitra Desa/Kelurahan/Kecamatan</label>
                    <asp:TextBox ID="tbNamaMitra" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Jabatan</label>
                    <asp:TextBox ID="tbJabatan" runat="server" CssClass="form-control" placeholder="Kepala Desa/Lurah"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Nama Mitra</label>
                    <asp:TextBox ID="tbNamaKelMitra" runat="server" CssClass="form-control" placeholder="Nama Desa/Kelurahan"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group">
                    <label>Alamat</label>
                    <asp:TextBox ID="tbAlamatMitraSasaran" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
            <div class="col-md-4">
                <div class="from-group">
                    <label>Jarak Mitra Ke PT</label>
                    <div class="row">
                        <div class="col-md-10">
                            <asp:TextBox ID="tbJarak" runat="server" CssClass="form-control jarak" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            KM
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="form-group">
                    <label>Bidang Mitra</label>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:TextBox ID="tbBidangMasalahMitra" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div style="display: none;">
                            (Minimal 2 bidang kewilayahan)
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="padding-left: 15px;">
                <h5 class="card-header-text" style="color: red;">KONTRIBUSI PENDANAAN (Jika Ada)</h5>
            </div>
            <div class="col-md-12">
                <div class="form-group row">
                    <asp:Label ID="lblPendanaanThn1" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 1" Visible="false"></asp:Label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="tbPendanaanThn1" runat="server" CssClass="form-control uang1" ClientIDMode="Static"></asp:TextBox>
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
