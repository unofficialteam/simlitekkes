<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="buku.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.buku" %>

<asp:MultiView ID="mvBuku" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftar" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="fs-17 font-weight-600 mb-0">BUKU</h6>
                    </div>
                    <div class="col-sm-6" style="text-align: right">
                        <asp:LinkButton ID="lbDataBaru" runat="server" class="btn btn-success waves-effect waves-light f-right"
                            OnClick="lbDataBaru_Click">
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
                                        <asp:ListView ID="lvBuku" runat="server"
                                            DataKeyNames="id_buku,judul,kd_sts_berkas_buku"
                                            OnItemEditing="lvBuku_ItemEditing"
                                            OnItemDeleting="lvBuku_ItemDeleting"
                                            OnItemCommand="lvBuku_ItemCommand" OnItemDataBound="lvBuku_ItemDataBound">
                                            <LayoutTemplate>
                                                <table class="table table-hover">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 30px; text-align: center; padding: 0;"></td>
                                                            <td style="width: 60px; text-align: center; padding: 0;"></td>
                                                            <td style="padding: 0;"></td>
                                                            <td style="width: 40px; text-align: center; padding: 0;"></td>
                                                        </tr>
                                                        <tr id="itemPlaceHolder" runat="server"></tr>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.DataItemIndex + 1 %></td>
                                                    <td>
                                                        <asp:LinkButton ID="lbUnduh" runat="server" Font-Size="50px" ForeColor="Red"
                                                            CssClass="far fa-file-pdf" CommandName="UnduhBerkas"
                                                            CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Unduh Berkas">
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <span style="color: forestgreen"><b><%# Eval("judul") %></b></span><br />
                                                        <b>Tahun Terbit: </b><%# Eval("thn_penerbitan") %> |                                                        
                                                        <b>ISBN: </b><%# Eval("isbn") %><br />
                                                        <b>Penerbit: </b><%# Eval("penerbit") %> |<br />
                                                        <asp:Panel ID="panelAksi" runat="server"
                                                            Visible='<%# (Eval("kd_sumber_data").ToString() == "1") ? false : true %>'>
                                                            <asp:LinkButton runat="server" ID="lbUnggahDokMitraPelaksanaPenelitian"
                                                                CommandName="unggahBuku" ForeColor="#0066cc"
                                                                CommandArgument="<%# Container.DataItemIndex %>">
                                                                <i class="hvr-buzz-out fas fa-upload"> unggah</i>
                                                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete"
                                                                ToolTip="Hapus Data" ForeColor="DarkRed"
                                                                CssClass="fa fa-trash"><i> hapus</i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit"
                                                                ToolTip="Edit Data" ForeColor="DarkOrange"
                                                                CommandArgument="<%# Container.DataItemIndex %>"
                                                                CssClass="fa fa-edit"><i> ubah</i></asp:LinkButton>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <div class="p-4">
                                                    <div class="alert alert-info" role="alert">
                                                        Belum ada data Buku...
                                                    </div>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                                <!-- end of row -->
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
                        <h6 class="fs-17 font-weight-600 mb-0">DATA BUKU</h6>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="view-info">
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Judul</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbJudul" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Tahun Terbit</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbTahunTerbit" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">ISBN</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbISBN" runat="server" CssClass="form-control" placeholder="ISBN"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Jumlah Halaman</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbJumlahHalaman" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Nama Penerbit</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbNamaPenerbit" runat="server" CssClass="form-control" placeholder="Nama Penerbit"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">URL</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbURL" runat="server" CssClass="form-control" placeholder="http://"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Unggah</label>
                        <div class="col-sm-10">
                            <asp:LinkButton ID="lbUnggahDokumen" runat="server" Visible="false"
                                CssClass="btn btn-success" OnClick="lbUnggahDokumen_Click">Unggah
                            </asp:LinkButton>

                            <asp:FileUpload runat="server" ID="fileUpload1" CssClass="form-control" />

                            <small class="text-danger">Ukuran berkas maksimal 50 MB</small>
                        </div>
                    </div>
                    <div class="clearfix" style="float: right">
                        <asp:LinkButton ID="btnSimpan" runat="server" Text="Simpan" class="btn btn-primary mr-2" OnClick="btnSimpan_Click"></asp:LinkButton>
                        <asp:LinkButton ID="btnBatal" runat="server" Text="Batal" class="btn btn-danger" OnClick="btnBatal_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

<div class="modal modal-success fade" id="modalKonfirmasiHapus" tabindex="-1" role="dialog"
    aria-labelledby="modalTitle" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalTitle">Konfirmasi</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>Apakah Anda yakin akan menghapus data ini ?</p>
                <p class="text-primary">
                    <asp:Label ID="lblJudulDihapus" runat="server" Font-Bold="true"></asp:Label>
                </p>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnKonfirmasiHapus" runat="server" CssClass="btn btn-primary"
                    Text="Hapus" OnClick="btnKonfirmasiHapus_Click" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="modalUnggahBerkas" tabindex="-1" role="dialog"
    aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h5>Unggah Buku</h5>
                            </div>
                            <div class="card border border-info">
                                <div class="card-body">
                                    <div>
                                        <%--<iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" style="border: none; width: 100%; height: 150px;"></iframe>--%>

                                        <div class="input-group input-group-button input-group-primary">
                                            <asp:FileUpload runat="server" ID="fileUpload1b" CssClass="form-control" />
                                            <span class="input-group-btn">
                                                <asp:LinkButton runat="server" ID="lbUnggahBuku" CssClass="btn btn-info"
                                                    OnClick="lbUnggahDokumen_Click">
                                                                        <i class="fa fa-cloud-upload">&nbsp;Unggah</i></asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                    <div style="font-size: 14px; padding: 10px;">
                                        Ekstensi: .pdf .PDF.<br />
                                        Maksimal: 1 MB<br>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="lbSelesaiUnggah" runat="server" CssClass="btn btn-info"
                            OnClick="lbSelesaiUnggah_Click">
                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Selesai
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
