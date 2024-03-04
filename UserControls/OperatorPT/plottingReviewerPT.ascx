<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="plottingReviewerPT.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.plottingReviewerPT" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asc" %>

<asp:MultiView runat="server" ID="mvDaftarPlotting">
    <asp:View runat="server" ID="vDaftarSkema">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6" style="margin-top: 0px; color: #313233e3;">
                    <h4>
                        <asp:Label runat="server" ID="Label1" Text="Plotting Reviewer"></asp:Label>
                    </h4>
                </div>
            </div>
            <div class="row" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="form-inline " style="text-align: left;">
                        Thn. Usulan&nbsp;
                        <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        Pelaksanaan&nbsp;
                        <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm"
                            OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 3px;">
                <div class="col-md-6">
                    <div class="form-inline " style="text-align: left;">
                        Program&nbsp;
                        <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control input-sm"
                            OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Text="Penelitian" Value="1" Selected="True" />
                            <asp:ListItem Text="Pengabdian" Value="2" />
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        Tahapan &nbsp;
                        <asp:DropDownList ID="ddlTahapan" runat="server" CssClass="form-control input-sm"
                            OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-inline" style="float: right">
                        <div class="form-group">
                            <asp:LinkButton ID="lbExcellSkema" runat="server" ToolTip="Export Excel"
                                ForeColor="Green" OnClick="lbExcellSkema_Click">
                                <i class="far fa-file-excel fa-2x"></i>
                            </asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbBebanReviewer" runat="server"
                                CssClass="btn btn-success" ToolTip="Beban Reviewer"
                                OnClick="lbBebanReviewer_Click">
                                <i class="fa fa-arrows-alt mr-2"></i>Beban Reviewer
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <asp:GridView runat="server" ID="gvSkemaNJmlReviewer" GridLines="None"
                                        CssClass="table table-striped table-hover" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="id_skema,nama_skema" HeaderStyle-HorizontalAlign="Center"
                                        OnRowCommand="gvSkemaNJmlReviewer_RowCommand" CellPadding="4" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                        Enabled="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Skema">
                                                <ItemTemplate>
                                                    <span style="color: #229922;"><%# Eval("nama_skema") %></span><br />
                                                    Jml. Proposal:
                                                    <asp:Label runat="server" Font-Bold="true" ForeColor="Maroon" ID="lbJmlUsulan" Text='<%# Eval("jml_usulan") %>'></asp:Label>
                                                    | Jml. Reviewer ditugaskan: <span style="color: maroon; font-weight: bold;"><%# Eval("jml_reviewer") %></span>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Plotting Rev. 1">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbPlotRev1" runat="server" CssClass="btn btn-sm text-white btn-primary rounded-pill w-100p mb-2 mr-1"
                                                        CommandName="tambah_reviewer" CommandArgument="<%# Container.DataItemIndex %>"><i class="fas fa-users mr-2"></i><%# Eval("jml_plotting_reviewer_1") %>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Plotting Rev. 2" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbPlotRev2" runat="server" CssClass="btn btn-sm text-white btn-primary rounded-pill w-100p mb-2 mr-1"
                                                        CommandName="tambah_reviewer" CommandArgument="<%# Container.DataItemIndex %>"><i class="fas fa-users mr-2"></i><%# Eval("jml_plotting_reviewer_2") %>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <strong>DATA TIDAK DITEMUKAN</strong>
                                        </EmptyDataTemplate>
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vBebanReviewer">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4>Plotting Reviewer&nbsp;
                        <asp:Label runat="server" ID="lblThnUsulan3" Text=""></asp:Label>
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="font-size: 16px;">
                    <asp:Label runat="server" ID="lblNamaSkemaNTahun3"></asp:Label>
                </div>
                <div class="col-md-6">
                    <div class="form-inline" style="float: right">
                        <div class="form-group">
                            <asp:LinkButton runat="server" ID="lbExportExcelBeban" ForeColor="Green"
                                ToolTip="Excel beban reviewer" OnClick="lbExportExcelBeban_Click">
                                    <i class="far fa-file-excel fa-2x"></i>
                            </asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbKembali3" CssClass="btn btn-info" OnClick="lbKembali_Click">
                                <span class="ml-2">Kembali</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-sm-12 col-md-12 col-lg-12 table-responsive" style="padding-bottom: 10px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <asp:GridView ID="gvBebanReviewer" runat="server" AutoGenerateColumns="False"
                            GridLines="None" CssClass="table table-striped table-hover" HeaderStyle-HorizontalAlign="Center"
                            DataKeyNames="id_reviewer"
                            ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%# Eval("nomor_baris") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer">
                                    <ItemTemplate>
                                        <span style="color: #229922;">
                                            <asp:Label ID="lblNidn" runat="server" Font-Bold="true" Text='<%# Bind("nidn") %>'></asp:Label>&nbsp;
                                            <asp:Label ID="lblNamaReviewer" runat="server" Text='<%# Bind("nama_reviewer") %>'></asp:Label>
                                        </span>
                                        <br />
                                        <asp:Label ID="lbinstitusi" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kompetensi">
                                    <ItemTemplate>
                                        <%# Eval("kompetensi") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Beban">
                                    <ItemTemplate>
                                        <span style="color: #ff5200;">
                                            <%# Eval("jml_skema") %> Skema | <%# Eval("jml_proposal") %> Proposal
                                        </span>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="text-align: center;">
                                    DATA TIDAK DITEMUKAN !!!
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12" style="padding-bottom: 10px;">
                    <div class="form-inline text-left" style="text-align: left;">
                        <asc:controlPaging runat="server" ID="ktPagingBebanRev" OnPageChanging="menu_paging_beban_rev" />
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="viewDaftarPlotting">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4 style="font-size: 24px;">Plotting Reviewer&nbsp;
                        <asp:Label runat="server" ID="lblThnUsulan" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>
                    </h4>
                </div>
                <div class="col-sm-3 text-right">
                    <asp:Label runat="server" ID="lblTahapan" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    <asp:Label runat="server" ID="lblNamaSkemaNTahun" CssClass="text-muted"></asp:Label>
                </div>
                <div class="col-sm-4 text-right">
                    <asp:LinkButton runat="server" ID="lbKembali" CssClass="btn btn-info" OnClick="lbKembali_Click">
                        <i class="ml-2"></i>Kembali
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="form-inline " style="text-align: left;">
                        <div class="col-sm-6" style="padding-right: 5px; color: #7cab3f;">
                            Jml. Data:&nbsp;<asp:Label runat="server" ID="lblJmlUsulan" Text="0" Font-Size="Small" Font-Bold="true"></asp:Label>
                            | Jml Reviewer 1:&nbsp;
                            <asp:Label runat="server" ID="lblJmlRev1" Text="0" Font-Size="Small" Font-Bold="true" CssClass="badge badge-success p-2" />
                            <asp:Label runat="server" ID="lblJudulJmlRev2" Text="| Jml Reviewer 2:"></asp:Label>
                            <asp:Label runat="server" ID="lblJmlRev2" Text="0" Font-Size="Small" Font-Bold="true" CssClass="badge badge-success p-2" />
                        </div>
                        <div class="col-sm-6">
                            <div class="form-inline" style="color: #7cab3f; float: right;">
                                Jml. Baris&nbsp;
                                <asp:DropDownList runat="server" ID="ddlJmlBaris" Enabled="true" CssClass="custom-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="Semua" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="lbExportExcel" ForeColor="Green" OnClick="lbExportExcel_Click">
                                    <i class="far fa-file-excel fa-2x"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <asp:GridView ID="gvPlotting" runat="server" AutoGenerateColumns="False"
                            GridLines="None" CssClass="table table-striped table-hover"
                            BorderStyle="None" Width="100%"
                            DataKeyNames="id_usulan_kegiatan, id_transaksi_kegiatan, kd_klaster"
                            ShowHeaderWhenEmpty="True" OnRowCommand="gvPlotting_RowCommand"
                            OnRowDataBound="gvPlotting_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbnNomor" runat="server" Text='<%# Bind("nomor_baris") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Judul">
                                    <ItemTemplate>
                                        <span style="color: #229922;">
                                            <asp:Label ID="lblKodePerguruanTinggi" runat="server" Font-Bold="true" Text='<%# Bind("kd_perguruan_tinggi") %>'></asp:Label>&nbsp;
                                                    <asp:Label ID="lblInstitusi" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                        </span>
                                        <br />
                                        <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("judul") %>'></asp:Label>
                                        <br />
                                        <span style="color: Maroon;">Bidang fokus: </span>
                                        <asp:Label ID="lblBidFokus" runat="server" Text='<%# Bind("bidang_fokus") %>'
                                            ForeColor="Maroon" Font-Italic="true" Font-Bold="true"></asp:Label>

                                        <asp:Label ID="lblStatusPelaksanaan" runat="server" ForeColor="DarkGreen"
                                            Visible='<%# (Eval("kd_sts_pelaksanaan").ToString() == "1") ? true: false %>'>&nbsp;
                                            <i class="fa fa-check"></i>
                                            Selesai (<%# Eval("tgl_pelaksanaan") %>)
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer 1">
                                    <ItemTemplate>
                                        <div class="row form-inline">
                                            <div style="float: left; vertical-align: top; width: 30px;">
                                                <asp:LinkButton runat="server" ID="lbDelReviewer1" CommandName="hapus_rev1" ForeColor="Red"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Hapus Reviewer" Visible="false">
                                                                <i class="fa fa-trash fa-2x"></i>
                                                </asp:LinkButton>
                                            </div>
                                            <div style="float: left; vertical-align: top; width: 120px;">
                                                <asp:Label ID="lblNamaReviewer1" runat="server" Text='<%# Bind("nama_reviewer_1") %>'></asp:Label>
                                            </div>
                                            <div class="col-sm-12 col-md-12 col-lg-12 text-left" style="padding-left: 0px;">
                                                <asp:Label ID="lblTglReviewRev1" runat="server" Text='<%# (String.IsNullOrEmpty(Eval("tgl_review_rev1").ToString())) ? "Belum menilai" : DateTime.Parse(Eval("tgl_review_rev1").ToString()).ToString("d") %>'
                                                    Visible='<%# (Eval("nama_reviewer_1").ToString() == "") ? false:true %>'></asp:Label>
                                            </div>
                                            <asp:LinkButton runat="server" ID="lbTambahReviewer1" CommandName="tambah_rev1" ForeColor="White"
                                                CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-success btn-sm" ToolTip="Tambah Reviewer">
                                                                <i class="fas fa-user-plus"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer 2">
                                    <ItemTemplate>
                                        <div class="row form-inline">
                                            <div style="float: left; vertical-align: top; width: 30px;">
                                                <asp:LinkButton runat="server" ID="lbDelReviewer2" CommandName="hapus_rev2" ForeColor="Red"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Hapus Reviewer" Visible="false">
                                                                <i class="fa fa-trash fa-2x"></i>
                                                </asp:LinkButton>
                                            </div>
                                            <div style="float: left; vertical-align: top; width: 120px;">
                                                <asp:Label ID="lblNamaReviewer2" runat="server" Text='<%# Bind("nama_reviewer_2") %>'></asp:Label>
                                            </div>
                                            <div class="col-sm-12 col-md-12 col-lg-12 text-left" style="padding-left:0px;">
                                                <asp:Label ID="lblTglReviewRev2" runat="server" Text='<%# (String.IsNullOrEmpty(Eval("tgl_review_rev2").ToString())) ? "Belum menilai" : DateTime.Parse(Eval("tgl_review_rev2").ToString()).ToString("d") %>'
                                                    visible='<%# (Eval("nama_reviewer_2").ToString() == "") ? false:true %>'></asp:Label>
                                            </div>
                                            <asp:LinkButton runat="server" ID="lbTambahReviewer2" CommandName="tambah_rev2" ForeColor="White"
                                                CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-success btn-sm" ToolTip="Tambah Reviewer">
                                                                <i class="fas fa-user-plus"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="text-align: center;">
                                    DATA TIDAK DITEMUKAN
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12 text-left">
                    <div class="form-inline text-left" style="text-align: left;">
                        <asc:controlPaging runat="server" ID="ktPaging" OnPageChanging="menu_evt_paging" />
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="viewDaftarReviewer">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4 style="font-size: 24px;">Plotting Reviewer&nbsp; 
                        <asp:Label runat="server" ID="lblThnUsulan2" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>
                    </h4>
                </div>
                <div class="col-sm-3 text-right">
                    <asp:Label runat="server" ID="lblTahapan2" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    <asp:Label runat="server" ID="lblNamaSkemaNTahun2" CssClass="text-muted"></asp:Label>
                </div>
                <div class="col-sm-4 text-right">
                    <asp:LinkButton runat="server" ID="lbKembali2" CssClass="btn btn-info" OnClick="lbKembali2_Click">
                        Kembali
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="alert alert-light" role="alert" style="border: 1px solid gray; margin-top: 5px; border-radius: 5px; background-color: #eee;">
                            <asp:Label runat="server" ID="lblJudul" ForeColor="Green" Font-Bold="true"></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="lblNamaPt"></asp:Label><%-- (<asp:Label runat="server" ID="lblNamaPt" ForeColor="DarkGray"></asp:Label> )--%><br />
                            <span style="color: maroon;">Bidang fokus:
                                <asp:Label runat="server" ID="lblBidangFokus" ForeColor="Maroon"></asp:Label>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="form-inline " style="text-align: left;">
                        <div class="col-sm-6" style="padding-right: 5px; color: #7cab3f;">
                            Jml. Data:&nbsp;
                            <asp:Label ID="lblJmlRecords" runat="server" Text="0"></asp:Label>
                            &nbsp;&nbsp;|&nbsp;&nbsp;Nama Reviewer&nbsp;
                            <asp:TextBox ID="tbnama" runat="server" placeholder="Ketik nama reviewer" Width="200"></asp:TextBox>&nbsp;
                            <asp:LinkButton runat="server" ID="lbcari" CssClass="btn btn-primary" OnClick="lbcari_Click">            
                                <i class="fas fa-search mr-2"></i>Cari
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-warning text-white" OnClick="lbBatal_Click" Visible="false">
                                <i class="fas fa-window-close mr-2"></i>Batal
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-inline" style="color: #7cab3f; float: right;">
                                Jml. baris&nbsp;
                                <asp:DropDownList runat="server" ID="ddljmlbarisrev" Enabled="true" CssClass="custom-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddljmlbarisrev_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="Semua" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <asp:GridView ID="gvDaftarReviewer" runat="server" AutoGenerateColumns="False"
                            GridLines="None" CssClass="table table-striped table-hover"
                            BorderStyle="None" Width="100%" DataKeyNames="id_reviewer, id_penugasan_reviewer"
                            ShowHeaderWhenEmpty="True" OnRowCommand="gvDaftarReviewer_RowCommand"
                            OnRowDataBound="gvDaftarReviewer_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer">
                                    <ItemTemplate>
                                        <span style="color: #229922;">
                                            <asp:Label ID="lblNidn" runat="server" Font-Bold="true" Text='<%# Bind("nidn") %>'></asp:Label>&nbsp;
                                                    <asp:Label ID="lblNamaReviewer" runat="server" Text='<%# Bind("nama_reviewer") %>'></asp:Label>&nbsp;
                                        </span>
                                        <br />
                                        <asp:Label ID="lbinstitusi" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("kompetensi") %>'></asp:Label>
                                        <br />
                                        <small class="label pull-left bg-green">
                                            <%--<asp:Label ID="lblStatusSertifikasi" runat="server" Font-Bold="true" Text='<%# Bind("sts_sertifikasi") %>'></asp:Label>--%></small>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbTambahReviewer2" CommandName="simpan_rev"
                                            CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-success" ToolTip="Simpan Reviewer">
                                                                <i class="fas fa-plus-circle"></i>&nbsp;&nbsp;Plotting
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                <div style="text-align: center;">
                                    DATA TIDAK DITEMUKAN !!!
                                </div>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle CssClass="text-center" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12 text-left">
                    <div class="form-inline text-left" style="text-align: left;">
                        <asc:controlPaging runat="server" ID="ControlPaging1" OnPageChanging="menu_paging_evt" />
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

<div class="modal modal-primary" id="modalHapus" tabindex="-1" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Hapus data plotting  ini?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-danger pull-right" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
