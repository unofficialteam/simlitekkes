<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraSasaranUMKMKKNPPM.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraSasaranUMKMKKNPPM" %>

<div class="card">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Sasaran</h5>
    </div>
    <div class="card-block">
        <div class="view-info">
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Desa/Kelurahan</label>
                <div class="col-sm-6 col-xs-12">
                    <asp:TextBox ID="tbNamaDesa" runat="server" CssClass="form-control" placeholder="Desa Andongsari"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Pimpinan</label>
                <div class="col-sm-6 col-xs-12">
                    <asp:TextBox ID="tbNamaPimpinan" runat="server" CssClass="form-control" placeholder="Roy Kiypshi"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Alamat</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:TextBox ID="tbAlamatMitra" runat="server" CssClass="form-control" placeholder="Jl. Sumatera No. 34-35 Solo Jawa Tengah" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Desa</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlProvinsi" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="nama_provinsi" DataValueField="kd_provinsi" 
                        OnSelectedIndexChanged="ddlProvinsi_SelectedIndexChanged"
                        CssClass="form-control select2" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlKota" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="nama_kota" DataValueField="kd_kota" 
                        OnSelectedIndexChanged="ddlKota_SelectedIndexChanged"
                        CssClass="form-control select2" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2"></label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlKecamatan" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="nama_kecamatan" DataValueField="kd_kecamatan"
                        OnSelectedIndexChanged="ddlKecamatan_SelectedIndexChanged"
                        CssClass="form-control select2" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2"></label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlDesa" runat="server" Enabled="true" ClientIDMode="Static"
                        DataTextField="nama_desa" DataValueField="kd_desa"
                        OnSelectedIndexChanged="ddlDesa_SelectedIndexChanged"
                        CssClass="form-control select2" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <asp:Label ID="lblInfoDesaPrioritas" runat="server" BackColor="Yellow" ForeColor="red"></asp:Label>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Jarak Mitra ke PT</label>
                <div class="col-sm-1 col-xs-12">
                    <asp:TextBox ID="tbJarak" runat="server" CssClass="form-control" TextMode="Number" placeholder="50"></asp:TextBox> 
                </div><div class="col-sm-1 col-xs-12"><b>KM</b></div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Bidang Masalah Mitra</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:TextBox ID="tbBidangMasalahMitra" runat="server" CssClass="form-control" placeholder="Pendidikan, Ekonomi"></asp:TextBox>
                </div>
                <label class="col-xs-2 col-form-label form-control-label">(Minimal 1 Bidang)</label>
            </div>
            <div class="form-group row" style="padding-left: 15px;">
                <h5 class="card-header-text" style="color: red;">KONTRIBUSI PENDANAAN (jika ada)</h5>
            </div>
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
<script type="text/javascript">
    new AutoNumeric('.uang1', { decimalPlaces: 0 });
    new AutoNumeric('.uang2', { decimalPlaces: 0 });
    new AutoNumeric('.uang3', { decimalPlaces: 0 });
</script>
