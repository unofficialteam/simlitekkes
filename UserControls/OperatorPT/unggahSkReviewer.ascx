<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="unggahSkReviewer.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.unggahSkReviewer" %>
<div class="content-header row align-items-center m-0">
    <nav aria-label="breadcrumb" class="col-sm-4 order-sm-last mb-3 mb-sm-0 p-0 ">
       <%-- <ol class="breadcrumb d-inline-flex font-weight-600 fs-13 bg-white mb-0 float-sm-right">
            <li class="breadcrumb-item"><a href="#">Home</a></li>
            <li class="breadcrumb-item"><a href="#">Page</a></li>
            <li class="breadcrumb-item active">Blank</li>
        </ol>--%>
    </nav>
    <div class="col-sm-8 header-title p-0">
        <div class="media">
            <div class="header-icon text-success mr-3"><i class="fas fa-cloud-upload-alt"></i></div>
            <div class="media-body">
                <h1 class="font-weight-bold">Unggah SK Rektor Penilai/Reviewer Internal</h1>
                <small>Unggah SK Rektor Penilai/Reviewer Internal setiap tahun kegiatan</small>
            </div>
        </div>
    </div>
</div>

<div class="row mt-3">
    <div class="col-md-12">
        <div class="card">
            <div class="card-header">
                <%--<h1>Unggah SK Rektor Penilai/Reviewer Internal</h1>--%>
                <div class="row">
                    <div class="col-sm-8">
                        <div class="form-inline p-t-5">
                            Tahun:&nbsp;
                            <asp:DropDownList runat="server" ID="ddlThnSk" Enabled="true" CssClass="form-control" Width="100px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlThnSk_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4 text-right">
                        <asp:LinkButton runat="server" ID="lbUnggahSk" CssClass="btn btn-success btn-sm" OnClick="lbUnggahSk_Click">
                                <i class="fas fa-cloud-upload-alt"></i>&nbsp;&nbsp;Unggah SK
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="card-body p-2">
                <asp:GridView ID="gvDaftarDokumenSkReviewer" runat="server" GridLines="None" Width="100%"
                    CssClass="table table-striped table-hover"
                    DataKeyNames="id_sk, nama_dokumen, no_sk, thn_sk"
                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                    OnRowCommand="gvDaftarDokumenSkReviewer_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblNoBaris" runat="server" Text='<%# Bind("no_baris") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SK">
                            <ItemTemplate>
                                <asp:Label ID="lblJenisDokumen" runat="server" Text='<%# Bind("nama_dokumen") %>'></asp:Label><br />
                                No. SK:&nbsp;<asp:Label ID="lblNomorSk" Font-Bold="true" runat="server" Text='<%# Bind("no_sk") %>'></asp:Label><br />
                                Thn. SK:&nbsp;<asp:Label ID="lblTahunSk" ForeColor="Blue" runat="server" Text='<%# Bind("thn_sk") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unduh">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbUnduhDokumenSK" runat="server" CommandName="unduhDokumen"
                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Dokumen"
                                    CssClass="btn btn-danger">
                                     <i class="hvr-buzz-out fa-2x fas fa-file-pdf"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update SK">
                            <ItemTemplate>
                                <asp:LinkButton ID="updateDokumen" runat="server" CommandName="updateDokumen"
                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Update Dokumen SK"
                                    CssClass="btn btn-success">
                                    <i class="hvr-buzz-out fa-2x fas fa-file-upload"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center" style="min-height: 100px; margin: 0 auto;">
                            <strong>DATA TIDAK DITEMUKAN</strong>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
                <%--                <asp:MultiView runat="server" ID="mvUnggahSkReviewer" ActiveViewIndex="0">
                    <asp:View runat="server" ID="viewDaftarUnggahSkReviewer">
                        <div class="form-horizontal">
                            <div class="box-body">
                                <div class="form-group">
                                    <div class="col-sm-4 pull-left">
                                    </div>
                                    <div class="col-sm-2 pull-right">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="form-inline">
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>--%>
            </div>
        </div>
    </div>
</div>

<div class="modal fade show" id="modalUploadDokumen" tabindex="-1" role="dialog" aria-labelledby="myModalUploadDokumen">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <%--<div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 style="text-align: center" class="modal-title" id="myModalUploadDokumen">Unggah SK Rektor Penilai/Reviewer
                </h4>
            </div>--%>
            <div class="modal-header">
                <h5 class="modal-title font-weight-600">Unggah SK Rektor Penilai/Reviewer</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="form-group">
                            <label for="tbNomorSk">Nomor SK</label>
                                <asp:TextBox ID="tbNomorSk" runat="server" CssClass="form-control input-sm"
                                    placeholder="Nomor SK"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="ddlThnSkUnggah">Tahun SK</label>
                                <asp:DropDownList runat="server" ID="ddlThnSkUnggah" CssClass="form-control" AppendDataBoundItems="true">
                                </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="fileUpload1">Pilih Berkas</label>
                                <asp:FileUpload ID="fileUpload1" runat="server" CssClass="form-control" Height="40px" />
                                <br />
                                <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-success btn-sm" OnClick="lbUnggahDokumen_Click">
                                    <i class="fas fa-upload"></i>&nbsp;&nbsp;Unggah</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>
