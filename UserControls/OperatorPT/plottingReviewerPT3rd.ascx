<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="plottingReviewerPT3rd.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.plottingReviewerPT3rd" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagPrefix="asp" TagName="controlPaging" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="UpPlottingReviewerPT3rd" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpPlottingReviewerPT3rd">
            <ProgressTemplate>
                <div class="modal_loading">
                    <div class="center">
                        <i class="fa fa-refresh fa-spin fa-5x"></i>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <div class="card">
            <div class="card-header">
                <h3>Plotting Reviewer (3rd Opinion)</h3>
            </div>
            <div class="card-body">
                <section class="content">
                    <asp:MultiView runat="server" ID="mvPlottingReviewerPT3rd">
                        <asp:View runat="server" ID="viewDaftarPlottingReviewerPT3rd">
                            <div class="box-body row">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label for="ddlThnUsulan">Tahun Usulan:&nbsp;</label>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="True"
                                                    CssClass="form-control input-sm"
                                                    OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-1 text-center">-</div>
                                            <div class="col-md-5">
                                                <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                                                    CssClass="form-control input-sm"
                                                    OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="ddlTahapan">Tahapan:&nbsp;</label>
                                        <asp:DropDownList ID="ddlTahapan" runat="server" CssClass="form-control input-sm"
                                            OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label for="ddlProgram">Program:</label>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="True"
                                                    CssClass="form-control input-sm"
                                                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-1 text-center">-</div>
                                            <div class="col-md-5">
                                                <asp:DropDownList ID="ddlSkemaKegiatan" runat="server" AutoPostBack="True"
                                                    CssClass="form-control input-sm"
                                                    OnSelectedIndexChanged="ddlSkemaKegiatan_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="ddlJmlBaris">Jml Baris:&nbsp;</label>
                                        <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="True"
                                            CssClass="form-control input-sm"
                                            OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                            <asp:ListItem Text="10" Value="10" Selected="True" />
                                            <asp:ListItem Text="1" Value="1" />
                                            <asp:ListItem Text="50" Value="50" />
                                            <asp:ListItem Text="100" Value="100" />
                                            <asp:ListItem Text="Semua" Value="9999" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvDaftarPlottingReviewerPT3rd" runat="server" GridLines="None" CssClass="table table-striped table-hover"
                                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                            DataKeyNames="id_usulan_kegiatan, id_transaksi_kegiatan, nama_ketua, judul,
                                            bidang_fokus, id_plotting_reviewer1, kd_sts_permanen_reviewer1, reviewer1, nilai_reviewer1,
                                            id_plotting_reviewer2, kd_sts_permanen_reviewer2, reviewer2, nilai_reviewer2, deviasi_nilai,
                                            id_plotting_reviewer3, kd_sts_permanen_reviewer3, reviewer3, nilai_reviewer3, id_reviewer3"
                                            OnRowUpdating="gvDaftarPlottingReviewerPT3rd_RowUpdating" OnRowDeleting="gvDaftarPlottingReviewerPT3rd_RowDeleting"
                                            OnRowDataBound="gvDaftarPlottingReviewerPT3rd_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbnNomor" runat="server" Text='<%# Bind("no_baris") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Usulan">
                                                    <ItemTemplate>
                                                        <span style="color: #227722;">
                                                            <b>
                                                                <asp:Label ID="lblNamaKetua" runat="server" Text='<%# Bind("nama_ketua") %>'></asp:Label>
                                                            </b>
                                                            <br />
                                                            <asp:Label ID="lblBidangFokus" runat="server" Text='<%# Bind("bidang_fokus") %>'></asp:Label>
                                                        </span>
                                                        <br />
                                                        <i>
                                                            <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("judul") %>'></asp:Label>
                                                        </i>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hasil Review">
                                                    <ItemTemplate>
                                                        Skor 1:<asp:Label ID="lblNilaiReviewer1" runat="server" Text='<%# Bind("nilai_reviewer1") %>'
                                                            Font-Bold="true"></asp:Label>
                                                        -
                                                        <asp:Label ID="lblReviewer1" runat="server" Text='<%# Bind("reviewer1") %>'></asp:Label><br />
                                                        Skor 2:
                                                        <asp:Label ID="lblNilaiReviewer2" runat="server" Text='<%# Bind("nilai_reviewer2") %>'
                                                            Font-Bold="true"></asp:Label>
                                                        -
                                                        <asp:Label ID="lblReviewer2" runat="server" Text='<%# Bind("reviewer2") %>'></asp:Label><br />
                                                        Skor 3:
                                                        <asp:Label ID="lblNilaiReviewer3" runat="server" Text='<%# Bind("nilai_reviewer3") %>'
                                                            Font-Bold="true"></asp:Label>
                                                        -
                                                        <asp:Label ID="lblReviewer3" runat="server" Text='<%# Bind("reviewer3") %>'></asp:Label><br />
                                                        Deviasi:
                                                        <asp:Label ID="lblDeviasi" runat="server" Text='<%# Bind("deviasi_nilai") %>'
                                                            Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbPlotting" runat="server" CommandName="Update" CssClass="btn btn-primary" ToolTip="Plotting">
                                                            <i class="fa fa-plus"></i>
                                                        </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete" CssClass="btn btn-danger" ToolTip="Hapus Data">
                                                            <i class="fa fa-remove"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:controlPaging runat="server" ID="PagingPlottingReviewerPT3rd" OnPageChanging="Paging_PageChanging" />
                                        </div>
                                        <div class="col-md-7" style="text-align: right; float: right">
                                            Total&nbsp;<asp:Label ID="lblJmlRecords" runat="server" Text="0"></asp:Label>&nbsp;Data
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                    <div class="modal fade" id="modalTambahReviewer3rd" role="dialog" aria-labelledby="myModalTambahReviewer3rd">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 style="text-align: center" class="modal-title" id="myModalTambahReviewer3rd">
                                        <asp:Label ID="lblNamaSkim" runat="server" Text="-" Font-Bold="true"></asp:Label>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-horizontal">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <label for="lblEditNamaKetua" class="col-sm-4 control-label">Usulan</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblEditNamaKetua" runat="server" Text="-"></asp:Label><br />
                                                    <asp:Label ID="lblEditBidangFokus" runat="server" Text="-"></asp:Label><br />
                                                    <asp:Label ID="lblEditJudul" runat="server" Text="-"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="lblSkorRev1" class="col-sm-4 control-label">Perbedaan Nilai</label>
                                                <div class="col-sm-8">
                                                    Skor 1:<asp:Label ID="lblSkorRev1" runat="server" Font-Bold="true" Text="-"></asp:Label>&nbsp;-&nbsp;
                                            <asp:Label ID="lblNamaRev1" runat="server" Text="-"></asp:Label><br />
                                                    Skor 2:<asp:Label ID="lblSkorRev2" runat="server" Font-Bold="true" Text="-"></asp:Label>&nbsp;-&nbsp;
                                            <asp:Label ID="lblNamaRev2" runat="server" Text="-"></asp:Label><br />
                                                    Deviasi:<asp:Label ID="lblEditBedaNilai" runat="server" Font-Bold="true" ForeColor="Blue" Text="-"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="gvKandidatReviewer3" class="col-sm-4 control-label">Reviewer Ketiga</label>
                                                <div class="col-sm-8">
                                                    <asp:GridView ID="gvKandidatReviewer3" runat="server" GridLines="None" CssClass="table table-striped table-hover"
                                                        ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                        DataKeyNames="id_reviewer, nama_reviewer, nama_institusi, id_penugasan_reviewer"
                                                        OnRowUpdating="gvKandidatReviewer3_RowUpdating">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNo" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="30px" />
                                                                <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nama Reviewer">
                                                                <ItemTemplate>
                                                                    <span style="color: #227722;">
                                                                        <b>
                                                                            <asp:Label ID="lblKandidatNamaReviewer" runat="server" Text='<%# Bind("nama_reviewer") %>'></asp:Label>
                                                                        </b>
                                                                    </span>
                                                                    <br />
                                                                    <i>
                                                                        <asp:Label ID="lblKandidatNamaInstitusi" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                                                    </i>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Plotting">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbPlottingKandidatReviewer3rd" runat="server" CommandName="Update"
                                                                        CssClass="btn btn-primary" ToolTip="Plotting Reviewer 3"
                                                                        OnClientClick="$('#modalTambahReviewer3rd').modal('hide');">
                                                            <i class="fa fa-plus"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal modal-danger" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="myModalHapus">Konfirmasi Hapus Penugasan Reviewer</h4>
                                </div>
                                <div class="modal-body">
                                    Apakah yakin akan menghapus data tersebut?
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                                    <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
