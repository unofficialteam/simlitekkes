<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="penyajiSeminar.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendaftaranReviewer.penyajiSeminar" %>



<asp:MultiView ID="mvIdentitas" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftar" runat="server">
        <div class="card">
            <div class="card-header">
                <h5 class="card-header-text">
                    <asp:Label ID="lbjudul" runat="server" Text="Pendaftaran Reviewer Nasional (PPM)"></asp:Label></h5>
                <%--                <asp:LinkButton ID="lbTambah" runat="server" OnClick="lbTambah_click" Text="Tambah" class="btn btn-success waves-effect waves-light f-right">
                    <i class="fa fa-plus">&nbsp;&nbsp;DATA BARU</i>
                </asp:LinkButton>--%>
            </div>
            <div class="card-block">
                <div class="view-info">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="Label1" runat="server" Text="Kegiatan Seminal Ilmiah Internasional Nasional dengan Prosiding" Font-Bold="true"></asp:Label>
                            <div class="general-info">
                                <div class="row">
                                    <div class="col-sm-12 table-responsive">
                                        <asp:GridView ID="gvProsiding" runat="server" Width="100%" BorderColor="White" BorderStyle="None"
                                            ShowHeader="False" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="id_prosiding,thn_prosiding,nama_prosiding,judul,kd_peran_penulis,kd_jenis_prosiding,volume,nomor,url,issn,kd_sts_berkas_prosiding,is_penyaji"
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
                                                <%--<asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbUnduhBerkas" runat="server" CommandName="unduhDokumen" Font-Size="50px" ForeColor="Red"
                                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Berkas"
                                                        CssClass="fa fa-file-pdf-o btn btn-default">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <b style="color: forestgreen">
                                                            <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("judul") %>'></asp:Label></b><br />
                                                        <b>
                                                            <asp:Label ID="lblJenisProsiding" runat="server" Text='<%# Bind("nama_prosiding") %>'></asp:Label></b><br />
                                                        <b>Tahun:
                                                    &nbsp;<asp:Label ID="lblThnProsiding" runat="server" Text='<%# Bind("thn_prosiding") %>'></asp:Label></b><br />
                                                        Sebagai Penyaji
                                            <asp:RadioButtonList ID="rbProsiding" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblEvaluasi_SelectedIndexChanged">
                                                <asp:ListItem Value="1">&nbsp Ya&nbsp&nbsp</asp:ListItem>
                                                <asp:ListItem Value="0">&nbsp Tidak</asp:ListItem>
                                            </asp:RadioButtonList>
                                                        <asp:LinkButton ID="lbUnduhBerkas" runat="server" CommandName="unduhDokumen" Font-Size="12px" ForeColor="Red"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Berkas"
                                                            CssClass="fa fa-file-pdf-o btn btn-default">
                                                        </asp:LinkButton>
                                                        <%--<br />
                                                        <div class="form-group row">
                                                            <label for="file" class="col-xs-4 col-form-label form-control-label">Dokumen Pendukung</label>
                                                            <div class="col-md-8">--%>
                                                              Dokumen Pendukung&nbsp;&nbsp;&nbsp; 
                                                        <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-info btn-flat" CommandName="unggahDokumen" CommandArgument="<%# Container.DataItemIndex %>">
                                  Pilih File</asp:LinkButton>&nbsp;<asp:Label ID="lbjudul" runat="server" Text="(Ukuran file maksimal 1 MB dengan format PDF)" Font-Size="Smaller"></asp:Label>
                                                        <%--</div>
                                                        </div>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <%--                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                                            CssClass="btn btn-sm btn-primary waves-effect m-b-5" ToolTip="Edit Data">
                                                                <i class="fa fa-pencil fa-2x"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete"
                                                            CssClass="btn btn-sm btn-danger waves-effect" ToolTip="Hapus Data">
                                                                <i class="fa fa-trash fa-2x"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60px" />
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div style="min-height: 100px; margin: 0 auto;">
                                                    <strong>Belum ada data artikel prosiding...</strong>
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
            <div class="card-block">
                <div class="view-info">
                    <div class="row">
                        <div class="col-lg-12">
                            <h5 class="card-header-text">Artikel Prosiding</h5>
                            <asp:LinkButton ID="lbTambah" runat="server" OnClick="lbTambah_click" Text="Tambah" class="btn btn-success waves-effect waves-light f-right">
                    <i class="fa fa-plus">&nbsp;&nbsp;DATA BARU</i>
                            </asp:LinkButton>

                            <asp:Label ID="Label2" runat="server" Text="Kegiatan Seminal Ilmiah Internasional Nasional Tanpa Prosiding" Font-Bold="true"></asp:Label>
                            <div class="general-info">
                                <div class="row">
                                    <div class="col-sm-12 table-responsive">
                                        <asp:GridView ID="gvPemakalah" runat="server" Width="100%" BorderColor="White" BorderStyle="None"
                                            ShowHeader="False" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False"
                                            DataKeyNames="id_pemakalah,thn_pemakalah,judul,nama_forum,is_penyaji,kd_sts_berkas_pemakalah"
                                            GridLines="Horizontal"
                                            OnRowCommand="gvPemakalah_RowCommand" OnRowDataBound="gvPemakalah_RowDataBound" OnRowDeleting="gvPemakalah_RowDeleting" OnRowUpdating="gvPemakalah_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="25px" />
                                                    <ItemStyle Width="25px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbUnduhBerkas" runat="server" CommandName="unduhDokumen" Font-Size="50px" ForeColor="Red"
                                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Berkas"
                                                        CssClass="fa fa-file-pdf-o btn btn-default">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <b style="color: forestgreen">
                                                            <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("judul") %>'></asp:Label></b><br />
                                                        <b>
                                                            <asp:Label ID="lblJenisProsiding" runat="server" Text='<%# Bind("nama_forum") %>'></asp:Label></b><br />
                                                        <b>Tahun:
                                                    &nbsp;<asp:Label ID="lblThnProsiding" runat="server" Text='<%# Bind("thn_pemakalah") %>'></asp:Label></b><br />
                                                        Sebagai Penyaji
                                            <asp:RadioButtonList ID="rbPemakalah" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                                <asp:ListItem Value="1">&nbsp Ya&nbsp&nbsp</asp:ListItem>
                                                <asp:ListItem Value="0">&nbsp Tidak</asp:ListItem>
                                            </asp:RadioButtonList>
                                                        <asp:LinkButton ID="lbUnduhBerkas" runat="server" CommandName="unduhDokumen" Font-Size="12px" ForeColor="Red"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Berkas"
                                                            CssClass="fa fa-file-pdf-o btn btn-default">
                                                        </asp:LinkButton>&nbsp;Dokumen Pendukung
                                                        <%--<br />
                                                        <div class="form-group row">
                                                            <label for="file" class="col-xs-4 col-form-label form-control-label">Dokumen Pendukung</label>
                                                            <div class="col-md-8">
                                                              Dokumen Pendukung&nbsp;&nbsp;&nbsp; 
                                                        <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-info btn-flat" CommandName="unggahDokumen" CommandArgument="<%# Container.DataItemIndex %>">
                                  Pilih File</asp:LinkButton>&nbsp;<asp:Label ID="lbjudul" runat="server" Text="(Ukuran file maksimal 1 MB dengan format PDF)" Font-Size="Smaller"></asp:Label>--%>
                                                        <%--</div>
                                                        </div>--%>
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
                                                    <strong>Belum ada data pemakalah...</strong>
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
                <h5 class="card-header-text">Data Pemakalah</h5>
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
                                                            <label for="tbNamaProsiding" class="col-xs-4 col-form-label form-control-label">Nama Forum</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="tbNamaProsiding" runat="server" class="form-control" placeholder="Nama Forum"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="ddlThnProsiding" class="col-xs-4 col-form-label form-control-label">Tahun</label>
                                                            <div class="col-sm-2">
                                                                <asp:DropDownList ID="ddlThnProsiding" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="ddlPeranPenulis" class="col-xs-4 col-form-label form-control-label">Penyaji?</label>
                                                            <div class="col-sm-4">
                                                                <asp:RadioButtonList ID="rbispemakalah" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1">&nbsp Ya&nbsp&nbsp</asp:ListItem>
                                                                    <asp:ListItem Value="0">&nbsp Tidak</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>


                                                <%--<tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="ddlPeranPenulis" class="col-xs-4 col-form-label form-control-label">Peran Penulis</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="ddlPeranPenulis" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="tbVolume" class="col-xs-4 col-form-label form-control-label">Volume</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="tbVolume" runat="server" class="form-control" placeholder="Volume (jika ada)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="tbNomor" class="col-xs-4 col-form-label form-control-label">Nomor</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="tbNomor" runat="server" class="form-control" placeholder="Nomor (jika ada)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="tbIssn" class="col-xs-4 col-form-label form-control-label">ISBN/ISSN</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="tbIssn" runat="server" class="form-control" placeholder="ISBN/ISSN"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="tbUrl" class="col-xs-4 col-form-label form-control-label">URL</label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="tbUrl" runat="server" class="form-control" placeholder="URL"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="ddlJenisProsiding" class="col-xs-4 col-form-label form-control-label">Jenis Prosiding</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="ddlJenisProsiding" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <div class="form-group row">
                                                            <label for="file" class="col-xs-4 col-form-label form-control-label">Unggah Berkas</label>
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
                <h4 class="modal-title" id="myModalHapus">Konfirmasi Hapus Pemakalah</h4>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus pemakalah 
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
                                <h4><i class="fa fa-book fa-lg"></i>&nbsp;&nbsp;Unggah Berkas Pemakalah</h4>
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
                    <%--                    <div class="modal-footer">
                        <button class="btn btn-secondary" data-dismiss="modal">
                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Selesai
                        </button>--%>
                                    &nbsp;&nbsp;<asp:LinkButton ID="lbsimpanberkas" runat="server" CssClass="btn btn-success"
                                        Text="Simpan" OnClick="lbsimpanberkas_Click"></asp:LinkButton>

                    <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Batal</button>

                </div>
            </div>
        </div>
    </div>
</div>


<div id="PopupValidasi" class="modal fade" role="dialog">

    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h5 class="modal-title" id="mymodalKonfirmasi">Update data
                </h5>
            </div>
            <div class="modal-body">
                <asp:Label ID="lbltanya" runat="server" Text=""></asp:Label>
                <br />
<%--                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Batal</button>--%>
                <asp:LinkButton ID="LinkButton1" runat="server" Text="Batal" OnClick="LinkButton1_Click" class="btn btn-default waves-effect"></asp:LinkButton>
                &nbsp;&nbsp;<asp:LinkButton ID="lbModalStsKonfirmasi" runat="server" CssClass="btn btn-success"
                    OnClick="lbModalStsKonfirmasi_Click" Text="Simpan">
                    <asp:Label ID="lblModalStsKonfirmasi" runat="server" Text=""></asp:Label>
                </asp:LinkButton>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>
</div>
