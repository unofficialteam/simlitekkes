<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraSasaranPPMUPT.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraSasaranPPMUPT" %>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/kelompokSasaranPPMUPT.ascx" TagPrefix="uc" TagName="kelompokSasaranPPMUPT" %>

<div class="card">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Mitra Sasaran</h5>
    </div>
    <div class="card-block">
        <div class="view-info">
            <%--<div class="form-group row">
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
                <label class="col-xs-2 col-form-label form-control-label">Nama Mitra</label>
                <div class="col-sm-6 col-xs-12">
                    <asp:TextBox ID="tbNamaMitra" runat="server" CssClass="form-control" placeholder="Nama Mitra"></asp:TextBox>
                </div>
            </div>--%>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Pimpinan Desa/Kelurahan</label>
                <div class="col-sm-6 col-xs-12">
                    <asp:TextBox ID="tbNamaPimpinan" runat="server" CssClass="form-control" placeholder="Nama Pimpinan"></asp:TextBox>
                </div>
            </div>
            <%--<div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Kelompok Mitra</label>
                <div class="col-sm-6 col-xs-12">
                    <asp:TextBox ID="tbNamaKelMitra" runat="server" CssClass="form-control" placeholder="Kelompok Mitra"></asp:TextBox>
                </div>
            </div>--%>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Alamat</label>
                <div class="col-sm-10 col-xs-12">
                    <asp:TextBox ID="tbAlamatMitra" runat="server" CssClass="form-control"
                        placeholder="Alamat" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Nama Desa</label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlProvinsi" runat="server" Enabled="true" CssClass="form-control"
                        DataValueField="kd_provinsi" DataTextField="nama_provinsi"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlProvinsi_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlKota" runat="server" Enabled="true" CssClass="form-control"
                        DataValueField="kd_kota" DataTextField="nama_kota"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlKota_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2"></label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlKecamatan" runat="server" Enabled="true" CssClass="form-control"
                        DataValueField="kd_kecamatan" DataTextField="nama_kecamatan"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlKecamatan_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2"></label>
                <div class="col-sm-4 col-xs-12">
                    <asp:DropDownList ID="ddlDesa" runat="server" Enabled="true" CssClass="form-control"
                        DataValueField="kd_desa" DataTextField="nama_desa">
                    </asp:DropDownList>
                    <div class="col-sm-4 col-xs-12">
                        <asp:Label ID="lblInfoDesaPrioritas" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-xs-2 col-form-label form-control-label">Jarak Mitra ke PT</label>
                <div class="col-sm-1 col-xs-12">
                    <asp:TextBox ID="tbJarak" runat="server" CssClass="form-control jarak" TextMode="Number" placeholder="0"></asp:TextBox>
                </div><div class="col-sm-1 col-xs-12"><b>KM</b></div>
            </div>
            <div class="form-group row" style="padding-left: 15px;">
                <h5 class="card-header-text" style="color: red;">KONTRIBUSI PENDANAAN</h5>
                <i style="background-color: yellow; font-size: x-small; color: red;">Jika Ada</i>
            </div>
            <div class="form-group row">
                <asp:Label ID="lblPendanaanThn1" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 1"></asp:Label>
                <div class="col-sm-2">
                    <asp:TextBox ID="tbPendanaanThn1" runat="server" CssClass="form-control uang1" Text="0"></asp:TextBox>
                </div>
                <asp:Label ID="lblPendanaanThn2" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 2"></asp:Label>
                <div class="col-sm-2">
                    <asp:TextBox ID="tbPendanaanThn2" runat="server" CssClass="form-control uang2" Text="0"></asp:TextBox>
                </div>
                <asp:Label ID="lblPendanaanThn3" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 3"></asp:Label>
                <div class="col-sm-2">
                    <asp:TextBox ID="tbPendanaanThn3" runat="server" CssClass="form-control uang3" Text="0"></asp:TextBox>
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
<div class="card">
    <div class="card-header">
        <h5 class="card-header-text" style="color: red;">Kelompok Sasaran</h5>
    </div>
    <div class="card-block">
        <div class="view-info">
            <div class="row">
                <div class="col-sm-12">
                    <asp:RadioButtonList ID="rblTahun" runat="server" AutoPostBack="true" CssClass="radio-button-list"
                        RepeatDirection="Horizontal" RepeatLayout="Flow"
                        OnSelectedIndexChanged="rblTahun_SelectedIndexChanged">
                        <asp:ListItem Text="Tahun 1" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Tahun 2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Tahun 3" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <hr />
            <asp:MultiView ID="mvKelompokSasaran" runat="server" ActiveViewIndex="0">
                <asp:View ID="vTahun1" runat="server">
                    <h6>Mitra 1</h6>
                    <uc:kelompokSasaranPPMUPT runat="server" id="ucMitra1Tahun1" UrutanTahun="1" UrutanMitra="1"></uc:kelompokSasaranPPMUPT>
                    <hr />
                    <h6>Mitra 2</h6>
                    <uc:kelompokSasaranPPMUPT runat="server" id="ucMitra2Tahun1" UrutanTahun="1" UrutanMitra="2"></uc:kelompokSasaranPPMUPT>
                </asp:View>
                <asp:View ID="vTahun2" runat="server">
                    <h6>Mitra 1</h6>
                    <uc:kelompokSasaranPPMUPT runat="server" id="ucMitra1Tahun2" UrutanTahun="2" UrutanMitra="1"></uc:kelompokSasaranPPMUPT>
                    <hr />
                    <h6>Mitra 2</h6>
                    <uc:kelompokSasaranPPMUPT runat="server" id="ucMitra2Tahun2" UrutanTahun="2" UrutanMitra="2"></uc:kelompokSasaranPPMUPT>
                </asp:View>
                <asp:View ID="vTahun3" runat="server">
                    <h6>Mitra 1</h6>
                    <uc:kelompokSasaranPPMUPT runat="server" id="ucMitra1Tahun3" UrutanTahun="3" UrutanMitra="1"></uc:kelompokSasaranPPMUPT>
                    <hr />
                    <h6>Mitra 2</h6>
                    <uc:kelompokSasaranPPMUPT runat="server" id="ucMitra2Tahun3" UrutanTahun="3" UrutanMitra="2"></uc:kelompokSasaranPPMUPT>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</div>
