<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="hki.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.hki" %>

<asp:MultiView ID="mvHKI" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftar" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="fs-17 font-weight-600 mb-0">KARYA INTELEKTUAL</h6>
                    </div>
                    <div class="col-sm-6" style="text-align: right">
                        <asp:LinkButton ID="lbDataBaru" runat="server" class="btn btn-success waves-effect waves-light f-right"
                            OnClick="lbDataBaruHKI_Click">
                    <i class="fa fa-plus"></i>&nbsp;&nbsp;Data Baru
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="card-block">
                <div class="view-info">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="general-info">
                                <div class="row">
                                    <div class="col-sm-12 table-responsive">
                                        <asp:GridView ID="gvHKI" runat="server" GridLines="None" ShowHeader="false" ShowHeaderWhenEmpty="false"
                                            AutoGenerateColumns="false" DataKeyNames="id_hak_kekayaan_intelektual, sts_data, kd_sts_berkas_hki, judul_hki"
                                            OnRowDeleting="gvHKI_RowDeleting" OnRowUpdating="gvHKI_RowUpdating" OnRowDataBound="gvHKI_RowDataBound"
                                            OnRowCommand="gvHKI_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                    <ItemStyle Width="25px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbUnduhBerkas" runat="server" CommandName="unduhDokumen" Font-Size="50px"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Berkas" ForeColor="Red"
                                                            CssClass="far fa-file-pdf">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <span style="color: forestgreen">
                                                            <asp:Label ID="lblJudulHKI" runat="server" Text='<%# Bind("judul_hki") %>' Font-Bold="true"></asp:Label></span><br />
                                                        <b>
                                                            <asp:Label ID="lblJenisHKI" runat="server" Text='<%# Bind("jenis_hki") %>'></asp:Label></b>&nbsp;&nbsp;|&nbsp;&nbsp
                                                            No. Pendaftaran: &nbsp;<asp:Label ID="lblNoPendaftaran" runat="server" Text='<%# Bind("no_pendaftaran") %>'></asp:Label>&nbsp;&nbsp;|&nbsp;&nbsp
                                                            Tahun Pelaksanaan &nbsp;<asp:Label ID="lblThnPelaksanaan" runat="server" Text='<%# Bind("thn_pelaksanaan") %>'></asp:Label><br />
                                                        Status: &nbsp<asp:Label ID="lblStatusHKI" runat="server" Text='<%# Bind("status_hki") %>'></asp:Label>&nbsp;&nbsp;|&nbsp;&nbsp;
                                                            No. HKI:<asp:Label ID="lblNoHKI" runat="server" Text='<%# Bind("no_hki") %>'></asp:Label><br />
                                                        <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete" ForeColor="DarkRed" Font-Bold="true"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus Data"
                                                            CssClass="fa fa-trash"><i> hapus</i>
                                                        </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbUbah" runat="server" CommandName="Update" ForeColor="DarkOrange" Font-Bold="true"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Ubah"
                                                            CssClass="fa fa-edit"><i> ubah</i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="p-4">
                                                    <div class="alert alert-info" role="alert">
                                                        Belum ada data Karya Intelektual...
                                                    </div>
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
    </asp:View>
    <asp:View ID="vData" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="fs-17 font-weight-600 mb-0">DATA KARYA INTELEKTUAL</h6>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                            <label for="tbJudulHKI" class="col-sm-2 control-label">Judul<i style="color: red">*</i></label>
                            <div class="col-sm-10">
                                <asp:TextBox runat="server" ID="tbJudulHKI" placeholder="Judul Hak Kekayaan Intelektual"
                                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                            <label for="ddljenisHKI" class="col-sm-2 control-label">Jenis HKI<i style="color: red">*</i></label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlJenisHKI" runat="server" Enabled="true" ClientIDMode="Static"
                                    CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                            <label for="tbTahun" class="col-sm-2 control-label">Tahun Pelaksanaan<i style="color: red">*</i></label>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlTahunHKI" runat="server" Enabled="true" ClientIDMode="Static"
                                    CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                            <label for="tbNoPendaftaran" class="col-sm-2 control-label">No. Pendaftaran<i style="color: red">*</i></label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" ID="tbNoPendaftaran" placeholder="Nomor Pendaftaran" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                            <label for="rblStatusHKI" class="col-sm-2 control-label">
                                Status<i style="color: red">*</i>
                                <asp:LinkButton ID="lbStatusHKI" runat="server" CssClass="fa fa-info-circle badge-top-left"
                                    Font-Bold="true" Font-Italic="true" ForeColor="Navy" OnClick="lbStatusHKI_Click"></asp:LinkButton></label>
                            <div class="col-sm-8">
                                <asp:RadioButtonList ID="rblStatusHKI" runat="server" RepeatDirection="Horizontal" DataTextField="status_hki"
                                    CssClass="radio-button-list" DataValueField="kd_sts_hki" AutoPostBack="true"
                                    OnSelectedIndexChanged="rblStatusHKI_SelectedIndexChanged">
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                            <label for="tbNomorHKI" class="col-sm-2 control-label">
                                No. HKI<i style="color: red">*</i>
                                <asp:LinkButton ID="lbNoHKI" runat="server" CssClass="fa fa-info-circle badge-top-left"
                                    Font-Bold="true" Font-Italic="true" ForeColor="Navy" OnClick="lbNoHKI_Click"></asp:LinkButton></label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" ID="tbNomorHKI" placeholder="Nomor HKI" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                            <label for="tbURLHKI" class="col-sm-2 control-label">
                                URL<i style="color: red">**</i>
                                <asp:LinkButton ID="lbURLHKI" runat="server" CssClass="fa fa-info-circle badge-top-left"
                                    Font-Bold="true" Font-Italic="true" ForeColor="Navy" OnClick="lbURLHKI_Click"></asp:LinkButton></label>
                            <div class="col-sm-10">
                                <asp:TextBox runat="server" ID="tbURLHKI" placeholder="URL Dokumen HKI" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <label for="fileUpload1" class="control-label">
                            Unggah Dokumen <span style="color:red">*</span>
                            <asp:LinkButton runat="server" ID="lbUnggahDokHKI" OnClick="lbUnggahDokHKI_Click"
                                class="btn btn-success" data-toggle="tooltip">Unggah
                            </asp:LinkButton>
                            <small class="text-danger">Ukuran berkas maksimal 1 M</small>
                            <asp:LinkButton ID="lbUploadHKI" runat="server" CssClass="fa fa-info-circle badge-top-left"
                                Font-Bold="true" Font-Italic="true" ForeColor="Navy" OnClick="lbUploadHKI_Click"></asp:LinkButton>
                        </label>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div style="float: left;">
                                <label for="keterangan" class="control-label" style="font-size: 10px;">
                                    * Harus Diisi<br />
                                    ** Isi jika ada
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix" style="float: right">
                        <asp:LinkButton ID="lbSimpan" runat="server" Text="Simpan" class="btn btn-primary mr-2" OnClick="lbSimpan_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lbBatalHKI" runat="server" Text="Batal" class="btn btn-danger" OnClick="lbBatalHKI_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>
<div class="modal modal-success fade" id="modalHapus" role="dialog" aria-labelledby="myModalHapus"
        tabindex="-1" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="myModalHapus">Konfirmasi</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus data
                &nbsp;<asp:Label runat="server" ID="lblJudulHKI" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">                
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-primary" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>
<div class="modal modal-success" id="modalInfo" role="dialog" aria-labelledby="mymodalInfo">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                Hak Kekayaan Intelektual (HKI), berupa Hak Cipta maupun Hak Kekayaan Industrial (Paten, Desain Industri, Desain Tata Letak Sirkuit Terpadu, Merek, Rahasia Dagang dan PerlindunganVarietasTanaman).
            </div>
        </div>
    </div>
</div>
<div class="modal modal-success" id="modalInfoStsHKI" role="dialog" aria-labelledby="mymodalInfoStsHKI">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                Jika status Granted nomor HKI harus diisi
            </div>
        </div>
    </div>
</div>
<div class="modal modal-success" id="modalInfoNoHKI" role="dialog" aria-labelledby="mymodalInfoNoHKI">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                Dapat/Harus diisi jika status Granted
            </div>
        </div>
    </div>
</div>
<div class="modal modal-success" id="modalInfoURL" role="dialog" aria-labelledby="mymodalInfoURL">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                URL diisi jika tidak ada dokumen HKI yang diunggah, atau URL diisi dan unggah dokumen HKI
            </div>
        </div>
    </div>
</div>
<div class="modal modal-success" id="modalInfoUpload" role="dialog" aria-labelledby="mymodalInfoUpload">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                Unggah dokumen HKI jika tidak mengisi URL, atau unggah dokumen HKI dan mengisi URL HKI. Format file PDF dengan ukuran maksimal 1 MB
            </div>
        </div>
    </div>
</div>
<div class="modal fade" id="modalUnggahDokHKI" tabindex="-1" role="dialog" 
    aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" 
    style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-header">
                            <h5>Unggah Dokumen HKI</h5>
                        </div>
                        <div class="card border border-info">
                            <div class="card-body">
                                <div>
                                    <iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" style="border: none; width: 100%; height: 150px;"></iframe>
                                </div>
                                <div>
                                    <div style="font-size: 14px; padding: 10px;">
                                        Ekstensi: .pdf  .PDF<br>
                                        Maksimal: 1MB.
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input name="hid_id_identitas_jurnal" id="hid_id_identitas_jurnal" type="hidden"></input>
                        <button class="btn btn-info" data-dismiss="modal" id="selesai" runat="server">
                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Selesai
                        </button>
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
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                </tr>
            </tbody>
        </table>
    </div>
</div>