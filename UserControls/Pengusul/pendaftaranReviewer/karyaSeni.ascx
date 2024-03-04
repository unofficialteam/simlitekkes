<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="karyaSeni.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendaftaranReviewer.karyaSeni" %>
<asp:MultiView ID="mvIdentitas" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftar" runat="server">
        <div class="card">
            <div class="card-header">
                <h5 class="card-header-text">
                    <asp:Label ID="lbjudul" runat="server" Text="Pendaftaran Reviewer Nasional (Penelitian)"></asp:Label></h5>
            </div>

            <div class="card-block">
                <div class="view-info">
                    <div class="row">
                        <div class="col-lg-12">
                            <h5 class="card-header-text">Luaran Kegiatan Penelitian - Karya Seni Monumental</h5>
                            <asp:LinkButton ID="lbTambah" runat="server" OnClick="lbTambah_click" Text="Tambah" class="btn btn-success waves-effect waves-light f-right">
                    <i class="fa fa-plus">&nbsp;&nbsp;DATA BARU</i>
                            </asp:LinkButton>
                            <%--<asp:Label ID="Label2" runat="server" Text="Kegiatan Seminal Ilmiah Internasional Nasional Tanpa Prosiding" Font-Bold="true"></asp:Label>--%>
                            <div class="general-info">
                                <div class="row">
                                    <div class="col-sm-12 table-responsive">
                                        <asp:GridView ID="gvDaftar" runat="server" Width="100%" BorderColor="White" BorderStyle="None"
                                            ShowHeader="False" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="id_karya_seni,kd_jn_karya_seni,kd_tingkat_pementasan,judul_pementasan,tempat_pelaksanaan,kd_sts_berkas,tgl_mulai,tgl_akhir"
                                            GridLines="Horizontal"
                                            OnRowCommand="gvDaftar_RowCommand" OnRowDataBound="gvDaftar_RowDataBound" OnRowDeleting="gvDaftar_RowDeleting" OnRowUpdating="gvDaftar_RowUpdating">
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
                                                        <b style="color: blue">
                                                            <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("judul_pementasan") %>'></asp:Label></b><br />
                                                        <b style="color: forestgreen">
                                                            <asp:Label ID="LbJnks" runat="server" Text='<%# Bind("jn_karya_seni") %>'></asp:Label></b><br />
                                                        Tingkat Kegiatan:&nbsp;<asp:Label ID="lblTingkat" runat="server" Text='<%# Bind("tingkat_pementasan") %>'></asp:Label><br />
                                                        Tanggal Kegiatan:&nbsp;<asp:Label ID="lblTglKegmulai" runat="server" Text='<%# Bind("tgl_mulai_ind") %>'></asp:Label>&nbsp;sampai
                                                                            &nbsp;<asp:Label ID="lblTglKegselesai" runat="server" Text='<%# Bind("tgl_akhir_ind") %>'></asp:Label><br />
                                                        Lokasi:&nbsp;<asp:Label ID="lblLokasi" runat="server" Text='<%# Bind("tempat_pelaksanaan") %>'></asp:Label><br />
                                                        Dokumen Pendukung:&nbsp;
                                                        <asp:LinkButton ID="lbUnduhBerkas" runat="server" CommandName="unduhDokumen" Font-Size="12px" ForeColor="Red"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Berkas"
                                                            CssClass="fa fa-file-pdf-o btn btn-default">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                                            CssClass="btn btn-sm btn-primary waves-effect m-b-5" ToolTip="Edit Data">
                                                                <i class="fa fa-pencil fa-2x"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete"
                                                            CssClass="btn btn-sm btn-danger waves-effect" ToolTip="Hapus Data">
                                                                <i class="fa fa-trash fa-2x"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div style="min-height: 100px; margin: 0 auto;">
                                                    <strong>Belum ada data karya seni...</strong>
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
        <div class="card">
            <div class="card-header">
                <h5 class="card-header-text">Luaran Kegiatan Penelitian - Karya seni Monumental</h5>
                <asp:LinkButton ID="lbClose" runat="server" OnClick="lbClose_click" class="btn btn-primary waves-effect waves-light f-right">
                    <i class="icofont icofont-close"></i>
                </asp:LinkButton>
            </div>
            <div class="card-block">
                <div class="edit-info">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="general-info">
                                <div class="row">
                                    <div class="col-lg-8">
                                        <table class="table">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="tbJudul" class="col-xs-4 col-form-label form-control-label">Judul</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="tbJudul" runat="server" class="form-control" Rows="3" TextMode="MultiLine" placeholder="Judul"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="ddlJnKaryaSeni" class="col-xs-4 col-form-label form-control-label">Jenis Karya Seni</label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddlJnKaryaSeni" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="ddlTingkat" class="col-xs-4 col-form-label form-control-label">Tingkat Kegiatan</label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddlTingkat" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="tbTglMulai" class="col-xs-4 col-form-label form-control-label">Tgl Pelaksanaan</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="tbTglMulai" runat="server" class="form-control" Type="date" ToolTip="Tanggal Mulai"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                &nbsp;sampai&nbsp;
                                                                </div>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="tbTglAkhir" runat="server" class="form-control" Type="date" ToolTip="Tanggal Akhir"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="tbLokasi" class="col-xs-4 col-form-label form-control-label">Lokasi</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="tbLokasi" runat="server" class="form-control" placeholder="Apabila kegiatan berupa pentas/pameran/ dan sejenisnya"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="file" class="col-xs-4 col-form-label form-control-label">Dokumen Pendukung</label>
                                                            <div class="col-md-8">
                                                                <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-info btn-flat" OnClick="lbUnggahDokumen_Click">
                                  <i class="fa fa-cloud-upload">&nbsp; Unggah</i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="text-center">
                                    <asp:LinkButton ID="lbSimpan" runat="server" Text="Simpan" OnClick="lbSimpan_click" class="btn btn-primary waves-effect waves-light m-r-20"></asp:LinkButton>
                                    <asp:LinkButton ID="lbBatal" runat="server" Text="Batal" OnClick="lbBatal_click" class="btn btn-default waves-effect"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
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
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalHapus">Konfirmasi Hapus Karya Seni</h4>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus karya seni 
                &nbsp;<b><asp:Label runat="server" ID="lblJudulProsidingHapus" Text=""></asp:Label>&nbsp;?</b>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
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
                                <h4><i class="fa fa-book fa-lg"></i>&nbsp;&nbsp;Unggah Berkas Karya Seni</h4>
                            </div>
                            <div class="card-body">
                                <div>
                                    <iframe src="../../../Helper/unggahFile.aspx" id="iframeUpload" clientidmode="Static" runat="server" width="100%" frameborder="0" height="150px"></iframe>
                                </div>
                                <div class="form-control">
                                    <div style="font-size: 14px; padding: 10px;">
                                        Ekstensi: pdf;PDF.
                                    </div>
                                    <div style="font-size: 14px; padding: 10px;">
                                        Maksimal: 1 MB<br>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    &nbsp;&nbsp;<asp:LinkButton ID="lbsimpanberkas" runat="server" CssClass="btn btn-success"
                        Text="Simpan" OnClick="lbsimpanberkas_Click"></asp:LinkButton>

                    <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Batal</button>

                </div>
            </div>
        </div>
    </div>
</div>



