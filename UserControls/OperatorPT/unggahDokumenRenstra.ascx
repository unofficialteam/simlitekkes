<%@ Control Language="C#" CodeBehind="unggahDokumenRenstra.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.unggahDokumenRenstra" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upUnggahDokumenRenstra" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upUnggahDokumenRenstra">
            <ProgressTemplate>
                <div class="modal_loading">
                    <div class="center">
                        <i class="fa fa-refresh fa-spin fa-5x"></i>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                    <h4>
                        <asp:Label ID="lblJudulPage" runat="server" Text=""></asp:Label>
                    </h4>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    <div class="form-inline">
                        <div class="form-group">
                            <label class="form-control-label">Thn. terbit&nbsp;</label>
                            <asp:DropDownList ID="ddlThnUpload" runat="server" AutoPostBack="True"
                                CssClass="form-control input-sm"
                                OnSelectedIndexChanged="ddlThnUpload_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4 m-t-20 text-right">
                    <asp:LinkButton ID="lbUnggah" runat="server" CssClass="btn btn-info" OnClick="lbUnggah_Click"><i class="fa fa-upload"></i> Unggah</asp:LinkButton>
                </div>
            </div>

            <section class="content mt-3">
                <asp:HiddenField runat="server" ID="hdId_sk" />
                <div style="min-height: 500px;">
                    <asp:MultiView ID="mvUnggahDokumenRenstra" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vUnggahDokumenRenstra" runat="server">
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                            <div class="box">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="alert alert-danger alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                                <h4><i class="icon fa fa-info">&nbsp;&nbsp;</i>Informasi</h4>
                                                Penelitian mengunggah 2 dokumen yaitu:<br />
                                                (1) SK Renstra (Penelitian), Maksimal 5 MB,<br />
                                                (2) Dokumen Renstra (Penelitian), Maksimal 5 MB
                                            </div>
                                            <asp:GridView ID="gvDaftarDokumenRenstra" runat="server" GridLines="None"
                                                CssClass="table table-striped table-hover"
                                                DataKeyNames="id_sk, nama_dokumen, no_sk, thn_sk"
                                                ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                OnRowCommand="gvDaftarDokumenRenstra_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoBaris" runat="server" Text='<%# Bind("no_baris") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jenis SK/Dokumen">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJenisDokumen" runat="server" Text='<%# Bind("nama_dokumen") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. SK">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNomorSk" runat="server" Text='<%# Bind("no_sk") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tahun Terbit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTahunSk" runat="server" Text='<%# Bind("thn_sk") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbUnduhDokumenRenstra" runat="server" CommandName="unduhDokumen" ForeColor="Red"
                                                                CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Dokumen"
                                                                CssClass="far fa-file-pdf fa-2x">
                                                            </asp:LinkButton>&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbHapusDokumenSKRenstra" runat="server" CommandName="hapusDokumenSK" ForeColor="Red"
                                                                CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus Dokumen"
                                                                CssClass="fas fa-trash-alt fa-2x"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
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
                        </asp:View>
                    </asp:MultiView>
                </div>
            </section>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="modalUploadDokumen" role="dialog" aria-labelledby="myModalUploadDokumen"
    aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 style="text-align: center" class="modal-title" id="myModalUploadDokumen">Unggah SK dan Dokumen Renstra
                </h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="form-inline">
                            <div class="col-md-4">
                                <label for="ddlJenisDokumen" class="">Jenis Dokumen</label>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList runat="server" ID="ddlJenisDokumen" AppendDataBoundItems="true" AutoPostBack="true"
                                    CssClass="form-control" OnSelectedIndexChanged="ddlJenisDokumen_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-inline" style="padding-top: 5px;">
                            <div class="col-md-4">
                                <asp:Label ID="lblNoSkAtauNoSuratPengantar" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="tbNomorSk" runat="server" CssClass="form-control input-sm"
                                    placeholder="Nomor SK"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-inline" style="padding-top: 5px;">
                            <div class="col-md-4">
                                <label for="ddlThnTerbit">Tahun Terbit</label>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList runat="server" ID="ddlThnTerbit" CssClass="form-control" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-inline" style="padding-top: 10px;">
                            <div class="col-md-4">
                                <label for="ddlThnTerbit" class="">Pilih Berkas</label>
                            </div>
                            <div class="col-sm-12">
                                <asp:FileUpload ID="fileUpload1" runat="server" CssClass="form-control" Height="40px" />
                                <span class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-info btn-flat" OnClick="lbUnggahDokumen_Click">
                                  <i class="fa fa-upload">&nbsp Unggah</i></asp:LinkButton>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <%--<asp:LinkButton ID="lbUnggahDokumen" runat="server" CssClass="btn btn-info pull-right"
                    OnClick="lbUnggahDokumen_Click" OnClientClick="$('#modalUploadDokumen').modal('hide');">Unggah</asp:LinkButton>--%>
            </div>
        </div>
    </div>
</div>
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
