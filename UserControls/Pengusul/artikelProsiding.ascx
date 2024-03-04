<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="artikelProsiding.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.artikelProsiding" %>

<asp:MultiView ID="mvIdentitas" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftar" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="fs-17 font-weight-600 mb-0">ARTIKEL PROSIDING</h6>
                    </div>
                    <div class="col-sm-6" style="text-align: right">
                        <asp:LinkButton ID="lbTambah" runat="server" OnClick="lbTambah_click" class="btn btn-success waves-effect waves-light f-right">
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
                                        <asp:GridView ID="gvProsiding" runat="server" Width="100%" BorderColor="White" BorderStyle="None"
                                            ShowHeader="False" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="id_prosiding,thn_prosiding,nama_prosiding,judul,
                                            kd_peran_penulis,kd_jenis_prosiding,volume,nomor,url,issn, kd_sts_berkas_prosiding"
                                            OnRowDeleting="gvProsiding_RowDeleting" OnRowUpdating="gvProsiding_RowUpdating" GridLines="Horizontal"
                                            OnRowCommand="gvProsiding_RowCommand" OnRowDataBound="gvProsiding_RowDataBound">
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
                                                        <asp:LinkButton ID="lbUnduhBerkas" runat="server" CommandName="unduhDokumen" Font-Size="50px" ForeColor="Red"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Berkas"
                                                            CssClass="far fa-file-pdf">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <span style="color: forestgreen">
                                                            <asp:Label ID="lblJudul" runat="server" Font-Bold="true" Text='<%# Bind("judul") %>'></asp:Label></span><br />
                                                        <b>Tahun:</b>
                                                        &nbsp;<asp:Label ID="lblThnProsiding" runat="server" Text='<%# Bind("thn_prosiding") %>'></asp:Label>&nbsp;&nbsp;|&nbsp;&nbsp
                                                    <b>Volume:</b>&nbsp;<asp:Label ID="lblVolume" runat="server" Text='<%# Bind("volume") %>'></asp:Label>&nbsp;&nbsp;|&nbsp;&nbsp
                                                    <b>Nomor:</b>&nbsp;<asp:Label ID="lblNomor" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>&nbsp;&nbsp;|&nbsp;&nbsp
                                                    <b>ISSN:</b>&nbsp;<asp:Label ID="lblIssn" runat="server" Text='<%# Bind("issn") %>'></asp:Label><br />
                                                        <b>Peran Penulis:</b>&nbsp;<asp:Label ID="lblPeranPenulis" runat="server" Text='<%# Bind("peran_penulis") %>'></asp:Label>&nbsp;&nbsp;|&nbsp;&nbsp
                                                    <b>Jenis Prosiding:</b>&nbsp;<asp:Label ID="lblJenisProsiding" runat="server" Text='<%# Bind("jenis_prosiding") %>'></asp:Label><br />
                                                        <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete"
                                                            ToolTip="Hapus Data" ForeColor="DarkRed"
                                                            CssClass="fa fa-trash"><i> hapus</i></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                                        ToolTip="Edit Data" ForeColor="DarkOrange"
                                                        CssClass="fa fa-edit"><i> ubah</i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="p-4">
                                                    <div class="alert alert-info" role="alert">
                                                        Belum ada data artikel prosiding...
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
    <asp:View ID="vInsup" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="fs-17 font-weight-600 mb-0">DATA ARTIKEL PROSIDING</h6>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="edit-info">

                    <div class="form-group row">
                        <label for="tbJudul" class="col-sm-2 control-label">Judul</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbJudul" runat="server" class="form-control" Rows="3" TextMode="MultiLine" placeholder="Judul Prosiding"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbNamaProsiding" class="col-sm-2 control-label">Nama Prosiding</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="tbNamaProsiding" runat="server" class="form-control" placeholder="Nama Prosiding"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="ddlThnProsiding" class="col-sm-2 control-label">Tahun Prosiding</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlThnProsiding" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="ddlPeranPenulis" class="col-sm-2 control-label">Peran Penulis</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlPeranPenulis" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbVolume" class="col-sm-2 control-label">Volume</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbVolume" runat="server" class="form-control" placeholder="Volume (jika ada)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbNomor" class="col-sm-2 control-label">Nomor</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbNomor" runat="server" class="form-control" placeholder="Nomor (jika ada)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbIssn" class="col-sm-2 control-label">ISBN/ISSN</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="tbIssn" runat="server" class="form-control" placeholder="ISBN/ISSN"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbUrl" class="col-sm-2 control-label">URL Artikel</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="tbUrl" runat="server" class="form-control" placeholder="URL"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="ddlJenisProsiding" class="col-sm-2 control-label">Jenis Prosiding</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlJenisProsiding" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="file" class="col-sm-2 control-label">Unggah Artikel</label>
                        <div class="col-md-8">
                            <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-success" OnClick="lbUnggahDokumen_Click">
                                  Unggah</asp:LinkButton>
                            <small class="text-danger">Ukuran berkas maksimal 1 MB</small>
                        </div>
                    </div>
                    <div class="clearfix" style="float: right">
                        <asp:LinkButton ID="lbSimpan" runat="server" Text="Simpan" OnClick="lbSimpan_click" class="btn btn-primary mr-2"></asp:LinkButton>
                        <asp:LinkButton ID="lbBatal" runat="server" Text="Batal" OnClick="lbBatal_click" class="btn btn-danger"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <%-- </div>--%>
        </div>
    </asp:View>
</asp:MultiView>

<div class="modal modal-success fade" id="modalHapus" role="dialog" aria-labelledby="myModalHapus"
    tabindex="-1" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalTitle">Konfirmasi</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus Artikel Prosiding 
                &nbsp;<b><asp:Label runat="server" ID="lblJudulProsidingHapus" Text=""></asp:Label>&nbsp;?</b>
            </div>
            <div class="modal-footer">                
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-primary" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="modalUnggahBerkas" tabindex="-1" role="dialog" aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h5>Unggah Artikel Prosiding</h5>
                            </div>
                            <div class="card border border-info">
                                <div class="card-body">
                                    <div>
                                        <iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" style="border: none; width: 100%; height: 150px;"></iframe>
                                    </div>
                                    <div style="font-size: 14px; padding: 10px;">
                                        Ekstensi: .pdf .PDF
                                        <br />
                                        Maksimal: 1 MB<br>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-info" data-dismiss="modal">
                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Selesai
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
