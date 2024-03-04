<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraAbdimasPPPUD.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPengabdian.mitraAbdimasPPPUD" %>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraPelaksanaPemdaKota.ascx" TagPrefix="uc" TagName="mitraPelaksanaPemdaKota" %>
<%@ Register Src="~/UserControls/Pengusul/mitraPengabdian/mitraSasaranUMKMPPPUD.ascx" TagPrefix="uc" TagName="mitraSasaranUMKM" %>
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
<asp:MultiView ID="mvMitra" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarMitra" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-block accordion-block">
                        <div id="accordion1" role="tablist" aria-multiselectable="true">
                            <div class="accordion-panel">
                                <div class="accordion-heading" role="tab" id="headingOne">
                                    <h3 class="card-title accordion-title">
                                        <a class="accordion-msg scale_active" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">&nbsp;Mitra Pelaksana Penelitian                                                    
                                                <asp:Label ID="lblWajibMitra" runat="server" Text="Jika Ada" Font-Bold="true" Font-Size="X-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                                        </a>
                                    </h3>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="accordion-content accordion-desc">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <i>Pemda / Pemkot / Lembaga Penyandang CSR (<asp:Label ID="lblJmlPemda" runat="server" Text="0"></asp:Label>)</i>&nbsp;&nbsp;
                                                <%--<b style="background-color: yellow; font-size: x-small; color: red;">Jika Ada</b>--%>
                                            </div>
                                            <div class="col-sm-6 text-right">
                                                <asp:LinkButton ID="lbTambahPemdaKota" runat="server" CssClass="btn btn-primary wave"
                                                    OnClick="lbTambahPemdaKota_Click">
                                                    <i class="fa fa-plus-circle"></i>&nbsp;Tambah
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-12  m-t-25">
                                            <asp:GridView ID="gvMitraPelaksanaPemdaKota" runat="server" GridLines="None" ShowHeader="true"
                                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%"
                                                ItemType="simlitekkes.Models.Pengusul.Mitra.PemdaPemkot"
                                                DataKeyNames="IdMitraAbdimas, NamaOrganisasiInstitusi"
                                                OnRowUpdating="gvMitraPelaksanaPemdaKota_RowUpdating"
                                                OnRowDeleting="gvMitraPelaksanaPemdaKota_RowDeleting"
                                                OnRowCommand="gvMitraPelaksanaPemdaKota_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mitra">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNamaMitraPengabdian" runat="server" Font-Bold="true"
                                                                Text='<%# Item.NamaPimpinanInstitusi %>' ForeColor="DarkBlue"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblInstitusiMitraPengabdian" runat="server" Text='<%# Item.NamaOrganisasiInstitusi %>'></asp:Label><br />
                                                            <asp:LinkButton runat="server" ID="lbUnggahDokMitraPelaksanaPengabdian"
                                                                class="btn btn-primary btn-sm" data-toggle="tooltip" CommandName="unggahDokMitraPengabdian"
                                                                CommandArgument="<%# Container.DataItemIndex %>"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>
                                                            Tgl.Unggah Data:
                                                                <asp:Label ID="lblTglUnggah" runat="server" Text='<%# Item.TglUnggahPernyataan.ToString("dd/mm/yyyy") %>'>
                                                                </asp:Label>&nbsp;<i class="label bg-info fa fa-download">
                                                            <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPelaksana"
                                                                CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White">Unduh</asp:LinkButton></i>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                                        <ItemTemplate>
                                                            <div style="font-size: 12pt;">
                                                                <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1: Rp"></asp:Label>
                                                                <asp:Label ID="lblDana1" runat="server" Text='<%# ((Decimal)Item.DanaTahun1).ToString("N0") %>'></asp:Label><br>
                                                                <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2: Rp"></asp:Label>
                                                                <asp:Label ID="lblDana2" runat="server" Text='<%# ((Decimal)Item.DanaTahun2).ToString("N0") %>'></asp:Label><br>
                                                                <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3: Rp"></asp:Label>
                                                                <asp:Label ID="lblDana3" runat="server" Text='<%# ((Decimal)Item.DanaTahun3).ToString("N0") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle Font-Bold="false" Width="200px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbUbah" runat="server" CommandName="Update" Font-Bold="true"
                                                                ForeColor="Black" ToolTip="Ubah" CssClass="fa fa-edit" Font-Size="XX-Large">
                                                            </asp:LinkButton><br />
                                                            <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete" Font-Bold="true"
                                                                ForeColor="Black" ToolTip="Hapus" CssClass="fa fa-trash-o" Font-Size="XX-Large">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
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
                                        <hr />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-block accordion-block">
                        <div id="accordion2" role="tablist" aria-multiselectable="true">
                            <div class="accordion-panel">
                                <div class="accordion-heading" role="tab" id="headingTwo">
                                    <h3 class="card-title accordion-title">
                                        <a class="accordion-msg scale_active" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">&nbsp;Mitra Sasaran                                                  
                                                <asp:Label ID="lblInfoWajib" runat="server" Text="Wajib Ada" Font-Bold="true" Font-Size="X-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                                        </a>
                                    </h3>
                                </div>
                                <div id="collapseTwo" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="accordion-content accordion-desc">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <i>Kelompok Masyarakat / UMKM (<asp:Label ID="lblJmlMitraSasaran" runat="server" Text="0"></asp:Label>)</i>&nbsp;&nbsp;
                                                <%--<b style="background-color: yellow; font-size: x-small; color: red;">Wajib Ada</b>--%>
                                            </div>
                                            <div class="col-sm-6 text-right">
                                                <asp:LinkButton ID="lbKelMasyarakat" runat="server" CssClass="btn btn-primary wave"
                                                    OnClick="lbKelMasyarakat_Click">
                                                    <i class="fa fa-plus-circle"></i>&nbsp;Tambah
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-12  m-t-25">
                                            <asp:GridView ID="gvKelMasyarakat" runat="server" Width="100%" GridLines="None" ShowHeader="true"
                                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                ItemType="simlitekkes.Models.Pengusul.Mitra.UMKM"
                                                DataKeyNames="IdMitraAbdimas, NamaUMKM"
                                                OnRowUpdating="gvKelMasyarakat_RowUpdating"
                                                OnRowDeleting="gvKelMasyarakat_RowDeleting"
                                                OnRowCommand="gvKelMasyarakat_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mitra">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNamaMitraPengabdian" runat="server" Text='<%# Item.NamaPimpinan %>' ForeColor="DarkBlue"></asp:Label>
                                                            <br>
                                                            <asp:Label ID="lblInstitusiMitraPengabdian" runat="server" Text='<%# Item.NamaUMKM %>'></asp:Label><br />
                                                            <asp:LinkButton runat="server" ID="lbUnggahDokMitraPelaksanaPengabdian"
                                                                class="btn btn-primary" data-toggle="tooltip" CommandName="unggahDokMitraPengabdian"
                                                                CommandArgument="<%# Container.DataItemIndex %>"><i class="icofont icofont-upload"></i>
                                                            </asp:LinkButton>
                                                            Tgl.Unggah Data:
                                                            <asp:Label ID="lblTglUnggah" runat="server" Text='<%# Item.TglUnggahPernyataan.ToString("dd/MM/yyyy") %>'>
                                                            </asp:Label>&nbsp;<i class="label bg-info fa fa-download">
                                                            <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPelaksana"
                                                                CommandArgument="<%# Container.DataItemIndex %>" ForeColor="White">Unduh</asp:LinkButton></i>
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                        <ItemStyle VerticalAlign="Top" />
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
                                                        <ItemStyle Font-Bold="false" Width="200px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbUbah" runat="server" CommandName="Update" Font-Bold="true"
                                                                ForeColor="Black" ToolTip="Ubah" CssClass="fa fa-edit" Font-Size="XX-Large">
                                                            </asp:LinkButton><br />
                                                            <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete" Font-Bold="true"
                                                                ForeColor="Black" ToolTip="Hapus" CssClass="fa fa-trash-o" Font-Size="XX-Large">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
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
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vEditPemda" runat="server">
        <uc:mitraPelaksanaPemdaKota runat="server" ID="mitraPelaksanaPemdaKota" />
        <div class="row">
            <div class="col-sm-12 text-right m-b-20">
                <asp:LinkButton ID="lbSimpanEditPemdaKota" runat="server" CssClass="btn btn-success"
                    OnClick="lbSimpanEditPemdaKota_Click">Simpan</asp:LinkButton>
                <asp:LinkButton ID="lbBatalEditPemdaKota" runat="server" CssClass="btn btn-danger"
                    OnClick="lbBatalEditPemdaKota_Click">Batal</asp:LinkButton>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vEditKelMasyarakat" runat="server">
        <uc:mitraSasaranUMKM runat="server" ID="mitraSasaranKelMasyarakat" />
        <div class="row">
            <div class="col-sm-12 text-right m-b-20">
                <asp:LinkButton ID="lbSimpanEditKelMasyarakat" runat="server" CssClass="btn btn-success"
                    OnClick="lbSimpanEditKelMasyarakat_Click">Simpan</asp:LinkButton>
                <asp:LinkButton ID="lbBatalEditPemdaKelMasyarakat" runat="server" CssClass="btn btn-danger"
                    OnClick="lbBatalEditPemdaKelMasyarakat_Click">Batal</asp:LinkButton>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

<div class="modal modal-danger" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalHapus">Konfirmasi Hapus Data</h4>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus data
                &nbsp;<asp:Label runat="server" ID="lblNamaMitraPelaksana" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info pull-right"
                    OnClick="lbHapus_Click">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<%--<div class="modal fade" id="modalUnggahDokMitra" tabindex="-1" role="dialog" aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
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
                                <div>
                                    <iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" clientidmode="Static" runat="server" width="100%" frameborder="0" height="150px"></iframe>
                                </div>
                                <div class="form-control">
                                    <div style="font-size: 14px; padding: 10px;">
                                        Ekstensi: pdf, PDF.
                                    </div>
                                    <div style="font-size: 14px; padding: 10px;">
                                        Maksimal: 1MB.<br>
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
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                </tr>
            </tbody>
        </table>
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
                                <div>
                                    <uc:ktUnggah runat="server" ID="kontrolUnggah" />
                                    <%--<iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" clientidmode="Static" runat="server" width="100%" frameborder="0" height="150px"></iframe>--%>
                                </div>
                                <div class="form-control">
                                    <div style="font-size: 14px; padding: 10px;">
                                        Ekstensi: pdf, PDF.
                                    </div>
                                    <div style="font-size: 14px; padding: 10px;">
                                        Maksimal: 1MB.<br>
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








