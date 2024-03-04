<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraAbdimasPPUPIK.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraAbdimasPPUPIK" %>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraPelaksanaPT.ascx" TagPrefix="uc" TagName="mitraPelaksanaPT" %>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraPelaksanaPemdaKota.ascx" TagPrefix="uc" TagName="mitraPelaksanaPemdaKota" %>
<%@ Register Src="~/Helper/unggahKontrol.ascx" TagPrefix="uc" TagName="ktUnggah" %>

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
<asp:MultiView ID="mvMitraPPUPIK" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarMitra" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="card mb-4">
                    <div class="card-header">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <h5>Mitra Pelaksana -
                                    <asp:Label ID="lblWajibMitra" runat="server" Text="Wajib ada" Font-Bold="true" Font-Size="X-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                                </h5>
                            </div>
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
                                <asp:GridView ID="gvMitraPelaksanaPT" runat="server" GridLines="None"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    ItemType="simlitekkes.Models.Pengusul.Mitra.PTPelaksana"
                                    DataKeyNames="IdMitraAbdimas, idUsulanKegiatan, NamaPimpinanInstitusi"
                                    OnRowUpdating="gvMitraPelaksanaPT_RowUpdating"
                                    OnRowDeleting="gvMitraPelaksanaPT_RowDeleting"
                                    OnRowCommand="gvMitraPelaksanaPT_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mitra">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblNamaMitraPelaksanaPT" runat="server" Text='<%# Item.NamaPimpinanInstitusi %>' ForeColor="DarkBlue"></asp:Label></b><br>
                                                <asp:Label ID="lblJabatanMitraPelaksanaPT" runat="server" Text='<%# Item.Jabatan %>'></asp:Label><br />
                                                <asp:LinkButton runat="server" ID="lbUnggahDokMitraPelaksanaPT"
                                                    class="btn btn-primary" data-toggle="tooltip" CommandName="unggahDokMitraPelaksanaPT"
                                                    CommandArgument="<%# Container.DataItemIndex %>"><i class="icofont icofont-upload"></i>
                                                </asp:LinkButton>
                                                Tgl.Unggah Data:
                                                                    <asp:Label ID="lblTglUnggahMitraPelaksanaPT" runat="server" Text='<%# Item.TglUnggahPernyataan.ToString("dd/MM/yyyy") %>'>
                                                                    </asp:Label><br />
                                                <asp:LinkButton ID="lbUnduhMitraPelaksanaPT" runat="server" CommandName="unduhDokumenMitraPelaksanaPT"
                                                    CommandArgument="<%# Container.DataItemIndex %>">Unduh</asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="68%" />
                                            <ItemStyle Font-Bold="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp"></asp:Label>
                                                <asp:Label ID="lblDana1" runat="server" Text='<%# ((Decimal)Item.DanaTahun1).ToString("N0") %>'></asp:Label><br>
                                                <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2: Rp"></asp:Label>
                                                <asp:Label ID="lblDana2" runat="server" Text='<%# ((Decimal)Item.DanaTahun2).ToString("N0") %>'></asp:Label><br>
                                                <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3: Rp"></asp:Label>
                                                <asp:Label ID="lblDana3" runat="server" Text='<%# ((Decimal)Item.DanaTahun3).ToString("N0") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbUbahMitraPelaksanaPT" runat="server" CommandName="update" Font-Bold="true" ForeColor="Black"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Ubah"
                                                    CssClass="fa fa-edit" Font-Size="XX-Large">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbHapusMitraPelaksanaPT" runat="server" CommandName="delete" Font-Bold="true" ForeColor="Black"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                                    CssClass="fa fa-trash-o" Font-Size="XX-Large">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;">
                                            <strong>DATA TIDAK DITEMUKAN</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <div class="col-md-12 mt-2">
                                <hr />
                            </div>
                            <div class="col-md-6">
                                <i>Pemda/Pemkot/Lembaga Penyandang CSR (<asp:Label ID="lblJmlPemda" runat="server" Text="0"></asp:Label>)</i>&nbsp;&nbsp;
                                                <b style="background-color: yellow; font-size: x-small; color: red;">Jika Ada</b>
                            </div>
                            <div class="col-md-6 text-right">
                                <asp:LinkButton ID="lbTambahPemdaKota" runat="server" CssClass="btn btn-primary wave"
                                    OnClick="lbTambahPemdaKota_Click">
                                                    <i class="fa fa-plus-circle"></i>&nbsp;Tambah
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-12  mt-2">
                                <asp:GridView ID="gvMitraPelaksanaPemdaKota" runat="server" GridLines="None"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    ItemType="simlitekkes.Models.Pengusul.Mitra.PemdaPemkot"
                                    DataKeyNames="IdMitraAbdimas, idUsulanKegiatan, NamaPimpinanInstitusi"
                                    OnRowUpdating="gvMitraPelaksanaPemdaKota_RowUpdating"
                                    OnRowDeleting="gvMitraPelaksanaPemdaKota_RowDeleting"
                                    OnRowCommand="gvMitraPelaksanaPemdaKota_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mitra">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblNamaMitraPemdaKota" runat="server" Text='<%# Item.NamaPimpinanInstitusi %>' ForeColor="DarkBlue"></asp:Label></b><br />
                                                <asp:Label ID="lblInstitusiMitraPengabdian" runat="server" Text='<%# Item.NamaOrganisasiInstitusi %>'></asp:Label><br />
                                                <asp:LinkButton runat="server" ID="lbUnggahDokMitraPemdaKota"
                                                    class="btn btn-primary" data-toggle="tooltip" CommandName="unggahDokMitraPemdaKota"
                                                    CommandArgument="<%# Container.DataItemIndex %>"><i class="icofont icofont-upload"></i>
                                                </asp:LinkButton>
                                                Tgl.Unggah Data:
                                                                    <asp:Label ID="lblTglUnggahMitraPemdaKota" runat="server" Text='<%# Item.TglUnggahPernyataan.ToString("dd/mm/yyyy") %>'>
                                                                    </asp:Label><br />
                                                <asp:LinkButton ID="lbUnduhMitraPemdaKota" runat="server" CommandName="unduhDokumenMitraPemdaKota"
                                                    CommandArgument="<%# Container.DataItemIndex %>">Unduh</asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="68%" />
                                            <ItemStyle Font-Bold="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp"></asp:Label>
                                                <asp:Label ID="lblDana1" runat="server" Text='<%# ((Decimal)Item.DanaTahun1).ToString("N0") %>'></asp:Label><br>
                                                <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2: Rp"></asp:Label>
                                                <asp:Label ID="lblDana2" runat="server" Text='<%# ((Decimal)Item.DanaTahun2).ToString("N0") %>'></asp:Label><br>
                                                <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3: Rp"></asp:Label>
                                                <asp:Label ID="lblDana3" runat="server" Text='<%# ((Decimal)Item.DanaTahun3).ToString("N0") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbUbahMitraPemdaKota" runat="server" CommandName="update" Font-Bold="true" ForeColor="Black"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Ubah"
                                                    CssClass="fa fa-edit" Font-Size="XX-Large">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbHapusMitraPemdaKota" runat="server" CommandName="delete" Font-Bold="true" ForeColor="Black"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                                    CssClass="fa fa-trash-o" Font-Size="XX-Large">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" />
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
    <asp:View ID="vEditPelPT" runat="server">
        <div class="card mb-4">
            <div class="card-body">
                <uc:mitraPelaksanaPT runat="server" ID="mitraPelPT" />
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
    <asp:View ID="vEditPelPemda" runat="server">
        <div class="card mb-4">
            <div class="card-body">
                <uc:mitraPelaksanaPemdaKota runat="server" ID="mitraPelPemdaKota" />
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
</asp:MultiView>
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
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

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
                                <div class="form-control">
                                    <div style="font-size: 14px;">
                                        Ekstensi: pdf, PDF. | Maksimal: 1MB
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
                <asp:LinkButton CssClass="btn btn-info" ID="selesai" runat="server">
                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Selesai
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="col-lg-12 col-xl-8">
        <table class="table m-0">
            <tbody>
                <tr>
                    <asp:Panel ID="pnlUnggah" runat="server" Visible="false">
                        <div class="form-group row">
                            <label for="file" class="col-md-2 col-form-label form-control-label">File input</label>
                            <div class="col-md-9">
                                <label for="file" class="custom-file">
                                    <input type="file" id="file" class="custom-file-input">
                                    <span class="custom-file-control"></span>
                                </label>
                                &nbsp;
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
