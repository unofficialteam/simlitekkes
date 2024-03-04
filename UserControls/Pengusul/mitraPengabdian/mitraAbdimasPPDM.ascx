<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraAbdimasPPDM.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraAbdimasPPDM" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraPelaksanaPT.ascx" TagPrefix="uc" TagName="mitraPelaksanaPT" %>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraPelaksanaPemdaKota.ascx" TagPrefix="uc" TagName="mitraPelaksanaPemdaKota" %>
<%--<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraPelaksanaPemdaKota.ascx" TagPrefix="uc" TagName="mitraPelaksanaCSR" %>--%>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraSasaranKelMasyarakat.ascx" TagPrefix="uc" TagName="mitraSasaranKelMasyarakat" %>
<%@ Register Src="~/Helper/unggahKontrol.ascx" TagPrefix="uc" TagName="ktUnggah" %>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/kelompoksasaranppdm.ascx" TagPrefix="uc" TagName="kelompoksasaranppdm" %>

<style>
    .accordion-title > a:before {
        float: left !important;
        font-family: FontAwesome;
        content: "\f068";
        padding-right: 8px;
    }

    .accordion-title > a.collapsed:before {
        float: left !important;
        content: "\f067";
    }
</style>

<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default m-t-20">
            <div class="form-row col-sm-12">
                <div class="form-group col-lg-12" style="text-align: right; padding-top: 20px; te">
                    <b>
                        <asp:Label ID="lblSkema" runat="server" Text="Terapan"></asp:Label>
                        (Tahun ke
                <asp:Label ID="lblUrutanUsulan" runat="server" Text="0"></asp:Label>
                        dari
                        <asp:Label ID="lblLamaUsulan" runat="server" Text="0"></asp:Label>
                        tahun)
                </div>
            </div>
        </div>
    </div>
</div>
<asp:MultiView ID="mvMitra" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarMitra" runat="server">
        <div class="row">
            <div class="col-md-12" style="display: none;">
                <div class="card mb-4">
                    <div class="card-header">
                        <div class="d-flex justify-content-between align-items-center">
                            <h5>Mitra Pelaksana Pengabdian -
                                    <asp:Label ID="lblWajibMitra" runat="server" Text="Wajib ada" Font-Bold="true" Font-Size="X-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                            </h5>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <i>Perguruan Tinggi Pelaksana (<asp:Label ID="lblJumlahPTPelaksana" runat="server" Text="0"></asp:Label>)</i>&nbsp;&nbsp;
                                                <b style="background-color: yellow; font-size: x-small; color: red;">Wajib Ada</b>
                            </div>
                            <div class="col-md-6 text-right">
                                <asp:LinkButton ID="lbTambahPTPelaksana" runat="server" CssClass="btn btn-primary wave"
                                    OnClick="lbTambahPTPelaksana_Click">
                                                    <i class="fa fa-plus-circle"></i>&nbsp;Tambah
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-12  mt-2">
                                <asp:GridView ID="gvMitraPelaksanaPengabdian" runat="server"
                                    GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="False" Width="100%"
                                    ItemType="simlitekkes.Models.Pengusul.Mitra.PTPelaksana"
                                    DataKeyNames="IdMitraAbdimas, NamaPimpinanInstitusi"
                                    OnRowUpdating="gvMitraPelaksanaPengabdian_RowUpdating"
                                    OnRowDeleting="gvMitraPelaksanaPengabdian_RowDeleting"
                                    OnRowCommand="gvMitraPelaksanaPengabdian_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbnNomor" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mitra">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblNamaMitraPengabdian" runat="server" ForeColor="DarkBlue" Text="<%# Item.NamaPimpinanInstitusi %>"></asp:Label>
                                                </b>
                                                <br />
                                                <asp:Label ID="lblInstitusiMitraPengabdian" runat="server" Text="<%# Item.Jabatan %>"></asp:Label>
                                                <br />
                                                <asp:LinkButton ID="lbUnggahDokMitraPelaksanaPengabdian" runat="server" class="btn btn-primary btn-sm" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggahDokMitraPengabdian" data-toggle="tooltip"><i class="fas fa-upload"></i>
                                                </asp:LinkButton>
                                                Tgl.Unggah Data:
                                                            <asp:Label ID="lblTglUnggah" runat="server" Text='<%# Item.TglUnggahPernyataan.ToString("dd/MM/yyyy") %>'>
                                                            </asp:Label>
                                                <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPelaksana"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White" CssClass="btn btn-info btn-sm"><i class="fas fa-download mr-2"></i>Unduh</asp:LinkButton>
                                                <br />
                                                </b>
                                            </ItemTemplate>
                                            <HeaderStyle Width="55%" />
                                            <ItemStyle Font-Bold="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp"></asp:Label>
                                                <asp:Label ID="lblDana1" runat="server" Text='<%# ((Decimal)Item.DanaTahun1).ToString("N0") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="false" HorizontalAlign="Left" />
                                            <HeaderStyle Width="25%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbUbah" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="update" CssClass="fas fa-edit" Font-Bold="true" Font-Size="XX-Large" ForeColor="Black" ToolTip="Ubah" Visible="true">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbHapus" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="delete" CssClass="fas fa-trash" Font-Bold="true" Font-Size="XX-Large" ForeColor="Black" ToolTip="Hapus" Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="15%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;">
                                            <strong>DATA TIDAK DITEMUKAN</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <%--<div class="col-md-12">
                                <hr />
                            </div>
                            <div class="col-md-6">
                                <i>Pemda / Pemkot / Lembaga Penyandang CSR(<asp:Label ID="lblJmlPemda" runat="server" Text="0"></asp:Label>)</i>&nbsp;&nbsp;
                                                <b style="background-color: yellow; font-size: x-small; color: red;">Jika Ada</b>
                            </div>
                            <div class="col-md-6 text-right">
                                <asp:LinkButton ID="lbTambahPemdaKota" runat="server" CssClass="btn btn-primary wave"
                                    OnClick="lbTambahPemdaKota_Click">
                                                    <i class="fa fa-plus-circle"></i>&nbsp;Tambah
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-12  mt-2">
                                <asp:GridView ID="gvMitraPelaksanaPemdaKota" runat="server" GridLines="None" Width="100%"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    DataKeyNames="IdMitraAbdimas, idUsulanKegiatan, NamaPimpinanInstitusi"
                                    ItemType="simlitekkes.Models.Pengusul.Mitra.PemdaPemkot"
                                    OnRowUpdating="gvMitraPelaksanaPemdaKota_RowUpdating"
                                    OnRowDeleting="gvMitraPelaksanaPemdaKota_RowDeleting"
                                    OnRowCommand="gvMitraPelaksanaPemdaKota_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbnNomor" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mitra">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblNamaMitraPemdaPemkot" runat="server" ForeColor="DarkBlue" Text='<%# Item.NamaPimpinanInstitusi %>'></asp:Label>
                                                </b>
                                                <br />
                                                <asp:Label ID="lblInstitusiMitraPemdaPemkot" runat="server" Text='<%# Item.NamaOrganisasiInstitusi %>'></asp:Label>
                                                <br />
                                                <asp:LinkButton ID="lbUnggahDokMitraPemdaPemkot" runat="server" class="btn btn-primary btn-sm" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggahDokMitraPemdaPemkot" data-toggle="tooltip"><i class="fas fa-upload"></i>
                                                </asp:LinkButton>
                                                Tgl.Unggah Data:
                                                            <asp:Label ID="lblTglUnggahPemdaPemkot" runat="server" Text='<%# Item.TglUnggahPernyataan.ToString("dd/MM/yyyy") %>'>>
                                                            </asp:Label>
                                                <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPemdaPemkot"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White" CssClass="btn btn-info btn-sm"><i class="fas fa-download mr-2"></i>Unduh</asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="55%" />
                                            <ItemStyle Font-Bold="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp"></asp:Label>
                                                <asp:Label ID="lblDana1" runat="server" Text='<%# ((Decimal)Item.DanaTahun1).ToString("N0") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="false" HorizontalAlign="Left" />
                                            <HeaderStyle Width="25%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbUbahMitraPemdaPemkot" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="update" CssClass="fas fa-edit" Font-Bold="true" Font-Size="XX-Large" ForeColor="Black" ToolTip="Ubah" Visible="true">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbHapusPemdaPemkot" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="delete" CssClass="fas fa-trash" Font-Bold="true" Font-Size="XX-Large" ForeColor="Black" ToolTip="Hapus" Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="15%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;">
                                            <strong>DATA TIDAK DITEMUKAN</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card mb-4">
                    <div class="card-header">
                        <div class="d-flex justify-content-between align-items-center">
                            <h5>Mitra Sasaran -
                                    <asp:Label ID="lblInfoWajib" runat="server" Text="Wajib Ada" Font-Bold="true" Font-Size="X-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                            </h5>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <i>Kelompok Masyarakat (<asp:Label ID="Label2" runat="server" Text="0"></asp:Label>)</i>&nbsp;&nbsp;
                                                <b style="background-color: yellow; font-size: x-small; color: red;">Wajib Ada</b>
                            </div>
                            <div class="col-md-6 text-right">
                                <asp:LinkButton ID="lbKelMasyarakat" runat="server" CssClass="btn btn-primary wave"
                                    OnClick="lbKelMasyarakat_Click">
                                                    <i class="fa fa-plus-circle"></i>&nbsp;Tambah
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-12  mt-2">
                                <asp:GridView ID="gvKelMasyarakat" runat="server"
                                    GridLines="None" ShowHeaderWhenEmpty="True"
                                    AutoGenerateColumns="False"
                                    DataKeyNames="id_mitra_abdimas, id_usulan_kegiatan, nama_pimpinan_mitra, nama_desa,
                                                tgl_unggah_pernyataan,id_mitra_abdimas_11,id_mitra_abdimas_12,
                                                id_mitra_abdimas_21, id_mitra_abdimas_22, id_mitra_abdimas_31, 
                                                id_mitra_abdimas_32"
                                    OnRowCommand="gvKelMasyarakat_RowCommand"
                                    OnRowDeleting="gvKelMasyarakat_RowDeleting"
                                    OnRowUpdating="gvKelMasyarakat_RowUpdating" Width="100%">

                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbnNomor" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mitra">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblNamaMitraPengabdian" runat="server" ForeColor="DarkBlue" Text='<%# Bind("nama_pimpinan_mitra") %>'></asp:Label>
                                                </b>
                                                <br />
                                                <asp:Label ID="lblInstitusiMitraPengabdian" runat="server" Text='<%# Bind("nama_desa") %>'></asp:Label>
                                                <br />
                                                <asp:LinkButton ID="lbUnggahDokMitraPelaksanaPengabdian" runat="server" class="btn btn-primary btn-sm" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggahDokMitraKelMas" data-toggle="tooltip"><i class="fas fa-upload"></i>
                                                </asp:LinkButton>
                                                Tgl.Unggah Data:
                                                            <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraSasaran"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White" CssClass="btn btn-sm btn-info"><i class="fas fa-download mr-2"></i>Unduh</asp:LinkButton>
                                                <br />

                                                <hr>
                                                <b>Kelompok Sasaran</b>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="lbnama11" runat="server" Text='<%# Bind("nama_pimpinan_mitra_11") %>'></asp:Label>
                                                        <br />
                                                        <asp:LinkButton ID="lbunggah11" runat="server" class="btn btn-primary btn-sm" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah11" data-toggle="tooltip"><i class="fas fa-upload"></i>
                                                        </asp:LinkButton>
                                                        Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh11" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_11").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                        <asp:LinkButton ID="lbUnduh11" runat="server" CommandName="unduh11"
                                                            CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White" CssClass="btn btn-sm btn-info"><i class="fas fa-download mr-2"></i>Unduh</asp:LinkButton>
                                                    </div>
                                                    <div class="col-md-6" style="display: none;">
                                                        <asp:Label ID="lbnama12" runat="server" Text='<%# Bind("nama_pimpinan_mitra_12") %>'></asp:Label>
                                                        <br />
                                                        <asp:LinkButton ID="lbunggah12" runat="server" class="btn btn-primary btn-sm" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah12" data-toggle="tooltip"><i class="fas fa-upload"></i>
                                                        </asp:LinkButton>
                                                        Tgl.Unggah Data:
                                                            <asp:Label ID="Label3" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_12").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                        <asp:LinkButton ID="lbUnduh12" runat="server" CommandName="unduh12"
                                                            CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White" CssClass="btn btn-sm btn-info"><i class="fas fa-download mr-2"></i>Unduh</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <%--<hr>
                                                <b>Tahun 2</b><br />
                                                <asp:Label ID="lbnama21" runat="server" Text='<%# Bind("nama_pimpinan_mitra_21") %>'></asp:Label>
                                                <br />
                                                <asp:LinkButton ID="lbunggah21" runat="server" class="btn btn-primary btn-sm" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah21" data-toggle="tooltip"><i class="fas fa-upload"></i>
                                                </asp:LinkButton>
                                                Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh21" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_21").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                <asp:LinkButton ID="lbUnduh21" runat="server" CommandName="unduh21"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White" CssClass="btn btn-sm btn-info"><i class="fas fa-download mr-2"></i>Unduh</asp:LinkButton>
                                                <br />
                                                <asp:Label ID="lbnama22" runat="server" Text='<%# Bind("nama_pimpinan_mitra_22") %>'></asp:Label>
                                                <br />
                                                <asp:LinkButton ID="lbunggah22" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah22" data-toggle="tooltip"><i class="fas fa-upload"></i>
                                                </asp:LinkButton>
                                                Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh22" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_22").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                <asp:LinkButton ID="lbUnduh22" runat="server" CommandName="unduh22"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White" CssClass="btn btn-sm btn-info"><i class="fas fa-download mr-2"></i>Unduh</asp:LinkButton>
                                                <hr>
                                                <b>Tahun 3</b><br />
                                                <asp:Label ID="lbnama31" runat="server" Text='<%# Bind("nama_pimpinan_mitra_31") %>'></asp:Label>
                                                <br />
                                                <asp:LinkButton ID="lbunggah31" runat="server" class="btn btn-primary" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah31" data-toggle="tooltip"><i class="fas fa-upload"></i>
                                                </asp:LinkButton>
                                                Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh31" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_31").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                <asp:LinkButton ID="lbUnduh31" runat="server" CommandName="unduh31"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White" CssClass="btn btn-sm btn-info"><i class="fas fa-download mr-2"></i>Unduh</asp:LinkButton>
                                                <br />
                                                <asp:Label ID="lbnama32" runat="server" Text='<%# Bind("nama_pimpinan_mitra_32") %>'></asp:Label>
                                                <br />
                                                <asp:LinkButton ID="lbunggah32" runat="server" class="btn btn-primary btn-sm" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unggah32" data-toggle="tooltip"><i class="fas fa-upload"></i>
                                                </asp:LinkButton>
                                                Tgl.Unggah Data:
                                                            <asp:Label ID="lbltglunduh32" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan_32").ToString(), "dd/MM/yyyy", "id-ID") %>'>
                                                            </asp:Label>
                                                <asp:LinkButton ID="lbUnduh32" runat="server" CommandName="unduh32"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White" CssClass="btn btn-sm btn-info"><i class="fas fa-download mr-2"></i>Unduh</asp:LinkButton>--%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="55%" />
                                            <ItemStyle Font-Bold="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:" Visible="false"></asp:Label>
                                                <asp:Label ID="lblDana1" runat="server" Text='<%# Bind("dana_tahun_1","{0:C0}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="false" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <HeaderStyle Width="25%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbUbah" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="update" CssClass="fas fa-edit" Font-Bold="true" Font-Size="XX-Large" ForeColor="Black" ToolTip="Ubah" Visible="true">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbHapus" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="delete" CssClass="fas fa-trash" Font-Bold="true" Font-Size="XX-Large" ForeColor="Black" ToolTip="Hapus" Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="15%" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;">
                                            <strong>DATA TIDAK DITEMUKAN</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vEditPT" runat="server">
        <div class="card mb-4">
            <div class="card-body">
                <uc:mitraPelaksanaPT runat="server" ID="mitraPelaksanaPT" />
            </div>
            <div class="card-footer">
                <div class="row">
                    <div class="col-sm-12 text-right m-b-20">
                        <asp:LinkButton ID="lbSimpanEditPT" runat="server" CssClass="btn btn-success"
                            OnClick="lbSimpanEditPT_Click">Simpan</asp:LinkButton>
                        <asp:LinkButton ID="lbBatalEditPT" runat="server" CssClass="btn btn-danger"
                            OnClick="lbBatalEditPT_Click">Batal</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vEditPemda" runat="server">
        <div class="card mb-4">
            <div class="card-body">
                <uc:mitraPelaksanaPemdaKota runat="server" ID="mitraPelaksanaPemdaKota" IsTipeMono="true" />
            </div>
            <div class="card-footer">
                <div class="row">
                    <div class="col-sm-12 text-right m-b-20">
                        <asp:LinkButton ID="lbSimpanEditPemdaKota" runat="server" CssClass="btn btn-success"
                            OnClick="lbSimpanEditPemdaKota_Click">Simpan</asp:LinkButton>
                        <asp:LinkButton ID="lbBatalEditPemdaKota" runat="server" CssClass="btn btn-danger"
                            OnClick="lbBatalEditPemdaKota_Click">Batal</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vEditKelMasyarakat" runat="server">
        <div class="card mb-4">
            <div class="card-body">
                <uc:mitraSasaranKelMasyarakat runat="server" ID="mitraSasaranKelMasyarakat" />
                <hr />
                <h5 class="card-header-text" style="color: red;">Kelompok Sasaran</h5>
                <%--<asp:TabContainer runat="server" ID="tabKontainer" Width="100%"
                    Height="500px" ScrollBars="Horizontal">--%>
                    <%--<asp:TabPanel ID="tpRencanaKerja" runat="server" HeaderText="Tahun 1">
                        <ContentTemplate>--%>
                            <asp:Panel ID="panelkel1" runat="server">
                                <uc:kelompoksasaranppdm ID="kelompok1" runat="server" />
                            </asp:Panel>
                        <%--</ContentTemplate>
                    </asp:TabPanel>--%>

                    <%--                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Tahun 2">
                        <ContentTemplate>
                            <asp:Panel ID="panelkel2" runat="server" Enabled="true">
                                <uc:kelompoksasaranppdm ID="kelompok2" runat="server" />
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>

                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Tahun 3">
                        <ContentTemplate>
                            <asp:Panel ID="panelkel3" runat="server" Enabled="true">
                                <uc:kelompoksasaranppdm ID="kelompok3" runat="server" />
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>--%>
               <%-- </asp:TabContainer>--%>
            </div>
            <div class="card-footer">
                <div class="row">
                    <div class="col-sm-12 text-right m-b-20">
                        <asp:LinkButton ID="lbSimpanEditKelMasyarakat" runat="server" CssClass="btn btn-success"
                            OnClick="lbSimpanEditKelMasyarakat_Click">Simpan</asp:LinkButton>
                        <asp:LinkButton ID="lbBatalEditPemdaKelMasyarakat" runat="server" CssClass="btn btn-danger"
                            OnClick="lbBatalEditPemdaKelMasyarakat_Click">Batal</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

<%--<div class="modal modal-danger" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;dalHapus">Konfirmasi Hapus Datanbsp;modal-footer"></span></button>
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>--%>

<div class="modal fade" id="modalUnggahDokMitra" tabindex="-1" role="dialog" aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card border border-info">
                            <div class="card-header">
                                <h2><i></i>&nbsp;&nbsp;Unggah Dokumen Mitra</h2>
                            </div>
                            <div class="card-body">
                                <uc:ktUnggah runat="server" ID="kontrolUnggah" />
                                <%--<iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" clientidmode="Static" runat="server" width="100%" frameborder="0" height="150px"></iframe>--%>
                                <div class="form-control">
                                    <div style="font-size: 14px;">
                                        Ekstensi: pdf, PDF. | Maksimal 1MB
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <input name="hid_id_identitas_jurnal"
                    id="hid_id_identitas_jurnal" type="hidden" />
                <asp:LinkButton CssClass="btn btn-info" ID="LinkButton1" runat="server">
                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Selesai
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="col-lg-12 col-xl-8">
        <table class="table m-0">
            <tbody>
                <tr>
                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <div class="form-group row">
                            <label for="file" class="col-md-2 col-form-label form-control-label">File input</label>
                            <div class="col-md-9">
                                <label for="file" class="custom-file">
                                    <input type="file" id="file" class="custom-file-input">
                                    <span class="custom-file-control"></span>
                                </label>
                                &nbsp;&nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                </tr>
            </tbody>
        </table>
    </div>
</div>

<div class="modal modal-danger" id="modalInfo" role="dialog" aria-labelledby="myModalInfo">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <b class="modal-title" id="myModalInfo" style="font-size: medium;">Pemetaan Dokumen Pendukung terhadap Skema Pengabdian kepada Masyarakat</b>
            </div>
            <div class="modal-body">
                <div>
                    <asp:Image ID="imgInfoMitra" runat="server" ImageUrl="~/Images/mitra_abdimas.png" />
                </div>
            </div>
        </div>
    </div>
</div>

<div class="modal modal-danger" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus Data</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus data
                &nbsp;<asp:Label runat="server" ID="lblNamaMitraPelaksana" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-info pull-right"
                    OnClick="lbHapus_Click">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="modal modal-danger" id="modalHapuskelmas" role="dialog" aria-labelledby="myModalHapuskelmas">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus Data</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus data
                &nbsp;<asp:Label runat="server" ID="Label1" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-info pull-right"
                    OnClick="lbHapus_Click">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
