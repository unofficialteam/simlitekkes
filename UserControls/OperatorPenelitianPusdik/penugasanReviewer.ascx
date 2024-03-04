<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="penugasanReviewer.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.penugasanReviewer" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagPrefix="uc" TagName="controlPaging" %>

<link rel="stylesheet" href="../../assets/dist/css/content.css" />

<asp:MultiView runat="server" ID="mvMain">
    <asp:View runat="server" ID="vRekapPenugasanSkema">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6" style="margin-top: 0px; color: #313233e3;">
                    <h4>Penugasan Reviewer</h4>
                </div>
            </div>
            <div class="row form-inline">
                <div class="col-sm-12 col-md-6">
                    Program Kegiatan&nbsp;
                    <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control input-sm"
                        OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlTahapan" runat="server" CssClass="form-control input-sm"
                        OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Text="--Pilih--" Value="00" Selected="True" />
                    </asp:DropDownList>
                </div>
                <div class="col-sm-12 col-md-6 text-right">
                    Thn. Usulan&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm"
                            OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                        </asp:DropDownList>
                    &nbsp;&nbsp;
                        Pelaksanaan&nbsp;
                        <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm"
                            OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                        </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-sm-12 col-md-12 text-right">
                    <asp:LinkButton ID="lbExcelUsulanBaru" runat="server"
                        CssClass="btn btn-success mr-2" ToolTip="Export Excel Usulan Baru"
                        OnClick="lbExcelUsulanBaru_Click">
                                        <i class="fas fa-file-excel mr-2"></i> Usulan Baru
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbExcelDaftarReviewer" runat="server"
                        CssClass="btn btn-success" ToolTip="Export Excel Daftar Reviewer"
                        OnClick="lbExcelDaftarReviewer_Click">
                                        <i class="fas fa-file-excel mr-2"></i> Reviewer
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <asp:GridView ID="gvRekapPenugasanSkema" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="id_skema, nama_skema, nama_singkat_skema"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowUpdating="gvRekapPenugasanSkema_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoRekapSkema" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Skema">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaSkema" runat="server" Text='<%# Bind("nama_skema") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Proposal">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJmlProposal" runat="server" Text='<%# Bind("jml_proposal") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer">
                                    <ItemTemplate>
                                        <asp:Button ID="lbJmlReviewer" runat="server" Visible="true" CommandName="Update" Text='<%# Bind("jml_reviewer") %>'
                                            CssClass="btn btn-link-gridview" ToolTip="Penugasan Reviewer"></asp:Button>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <strong class="color-nodata-gridview">DATA TIDAK DITEMUKAN</strong>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vDaftarSudahDitugaskan">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6" style="margin-top: 0px; color: #313233e3;">
                    <h4>Penugasan Reviewer
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    <asp:Label runat="server" ID="lblProgramPenugasan" Text=""></asp:Label>
                    -
                    <asp:Label runat="server" ID="lblSKemaPenugasan" Text=""></asp:Label><br />
                    <asp:Label runat="server" Font-Bold="true" ForeColor="Blue" ID="lblTahapPenugasan" Text=""></asp:Label>
                    Thn. Usulan:&nbsp;
                                <asp:Label runat="server" ID="lblThnUsulanPenugasan" Text=""></asp:Label>
                    &nbsp;|&nbsp;Pelaksanaan:&nbsp;
                                <asp:Label runat="server" ID="lblThnPelaksanaanPenugasan" Text=""></asp:Label>
                </div>
                <div class="col-sm-4 text-right">
                    <div class="clearfix">
                        <div style="float: right">
                            <asp:LinkButton runat="server" ID="lbKembaliPenugasan1" class="btn btn-info" OnClick="lbKembaliPenugasan_Click">
                                    <span class="ml-2">Kembali</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row form-inline" style="padding-top: 10px;">
                <div class="col-sm-12 col-md-12 text-right">
                    Jml. Baris:&nbsp;
                    <asp:DropDownList ID="ddlJmlBarisPenugasan" runat="server" AutoPostBack="True"
                        CssClass="form-control input-sm"
                        OnSelectedIndexChanged="ddlJmlBarisPenugasan_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" Selected="True" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                        <asp:ListItem Text="Semua" Value="0" />
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lbExcelPenugasan"
                        ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcelPenugasan_Click">    
                        <i class="far fa-file-excel fa-2x"></i>
                    </asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lbPenugasanReviewer"
                        class="btn btn-success waves-effect waves-light" OnClick="lbPenugasanReviewer_Click">
                        <i class="fas fa-user-plus mr-2"></i>Tambah Reviewer
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <asp:GridView ID="gvDaftarPenugasan" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="id_penugasan_reviewer, nama_reviewer"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDeleting="gvDaftarPenugasan_RowDeleting"
                            OnRowDataBound="gvDaftarPenugasan_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarPenugasan" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNidnDaftarPenugasan" runat="server" Text='<%# Bind("nidn") %>'></asp:Label>&nbsp;|&nbsp;
                                                <asp:Label ID="lblReviewerDaftarPenugasan" runat="server" Text='<%# Bind("nama_reviewer") %>'></asp:Label><br />
                                        <asp:Label ID="lblInstitusiPenugasan" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kompetensi">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKompetensiDaftarPenugasan" runat="server" Text='<%# Bind("kompetensi") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hapus">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbHapusDaftarPenugasan" runat="server" CommandName="Delete"
                                            ToolTip="Hapus Penugasan Reviewer">
                                            <i class="fa fa-trash fa-2x colorTrash"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <strong class="color-nodata-gridview">DATA TIDAK DITEMUKAN</strong>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="padding-bottom: 10px;">
                <div class="row">
                    <uc:controlPaging runat="server" ID="pagingDaftarPenugasan" OnPageChanging="daftarPenugasan_PageChanging" />
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vReviewerBlmDitugaskan">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6" style="margin-top: 0px; color: #313233e3;">
                    <h4>Penugasan Reviewer
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    Program:&nbsp;<asp:Label runat="server" ID="lblProgramReviewer" Text=""></asp:Label>
                    -
                    <asp:Label runat="server" ID="lblSkemaReviewer" Text=""></asp:Label><br />
                    <asp:Label runat="server" Font-Bold="true" ForeColor="Blue" ID="lblTahapanReviewer" Text=""></asp:Label>
                    Tahun Usulan&nbsp;
                                <asp:Label runat="server" ID="lblThnUsulanReviewer" Text=""></asp:Label>
                    &nbsp;|&nbsp;Tahun Pelaksanaan&nbsp;
                                <asp:Label runat="server" ID="lblThnPelaksanaanReviewer" Text=""></asp:Label>
                </div>
                <div class="col-sm-4 text-right">
                    <div class="clearfix">
                        <div style="float: right">
                            <asp:LinkButton runat="server" ID="lbKembaliReviewer"
                                class="btn btn-info" OnClick="lbKembaliReviewer_Click">
                                    <span class="ml-2">Kembali</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="margin: 10px 0px;">
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="form-inline " style="text-align: left;">
                            <div class="col-sm-12 col-md-4" style="padding-right: 5px; color: #7cab3f;">
                                Jml. Baris:&nbsp;
                            </div>
                            <div class="col-sm-12 col-md-8">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="tbCariReviewer" class="form-control" placeholder="Ketik nama reviewer" aria-describedby="btn-addon">
                                    </asp:TextBox>
                                    <span class="input-group">
                                        <asp:LinkButton runat="server" ID="lbCariJudul" class="btn btn-warning addon-btn waves-effect waves-light"
                                            OnClick="lbCariReviewer_Click">Cari
                                        </asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline text-right" style="color: #7cab3f;">
                            Jml. Baris:&nbsp;
                            <asp:DropDownList ID="ddlJmlBarisReviewer" runat="server" AutoPostBack="True"
                                CssClass="form-control input-sm"
                                OnSelectedIndexChanged="ddlJmlBarisReviewer_SelectedIndexChanged">
                                <asp:ListItem Text="10" Value="10" Selected="True" />
                                <asp:ListItem Text="50" Value="50" />
                                <asp:ListItem Text="100" Value="100" />
                                <asp:ListItem Text="Semua" Value="0" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbExcelReviewer"
                                ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcelReviewer_Click">    
                                    <i class="far fa-file-excel fa-2x"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <asp:GridView ID="gvDaftarReviewer" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="id_reviewer"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowUpdating="gvDaftarReviewer_RowUpdating"
                            OnRowDataBound="gvDaftarReviewer_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoReviewer" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNidnReviewer" runat="server" Text='<%# Bind("nidn") %>'></asp:Label>&nbsp;|&nbsp;
                                                <asp:Label ID="lblReviewerReviewer" runat="server" Text='<%# Bind("nama") %>'></asp:Label><br />
                                        <asp:Label ID="lblInstitusiReviewer" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kompetensi">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKompetensiReviewer" runat="server" Text='<%# Bind("kompetensi") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Beban Plotting">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbBebanPlottingReviewer" runat="server" ToolTip="Info jumlah proposal yang telah diplot untuk direview">
                                            Dari institusi ini: <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Maroon" Text="0"></asp:Label><br />
                                            Secara nasional: <asp:Label ID="lblJmlBebanNasional" runat="server" Font-Bold="true" ForeColor="Maroon" Text="0"></asp:Label>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbTugaskanReviewer" runat="server" CommandName="Update"
                                            CssClass="btn btn-success" ToolTip="Tugaskan Reviewer">
                                                    <i class="icofont icofont-plus"></i>
                                                    <span>Tugaskan</span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <strong class="color-nodata-gridview">DATA TIDAK DITEMUKAN</strong>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-md-5">
                    <uc:controlPaging runat="server" ID="PagingReviewer" OnPageChanging="PagingReviewer_PageChanging" />
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

<div class="modal modal-primary fade" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" id="myModalHapus">Konfirmasi Hapus Penugasan Reviewer</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus penugasan reviewer
                &nbsp;<asp:Label runat="server" ID="lblNamaReviewerHapusDaftarPenugasan" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusDaftarPenugasan" runat="server" CssClass="btn btn-danger" OnClick="lbHapusDaftarPenugasan_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
