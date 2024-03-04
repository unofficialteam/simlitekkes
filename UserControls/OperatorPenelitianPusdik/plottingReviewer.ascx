<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="plottingReviewer.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.plottingReviewer" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asc" %>
<%@ Register Src="~/Helper/modalKonfirmasi.ascx" TagPrefix="asc" TagName="modalKonfirmasi" %>

<asp:MultiView ID="mvPlotAdministrasi" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarSkema" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4>Plotting Reviewer</h4>
                </div>
            </div>
            <div class="row form-inline">
                <div class="col-sm-12 col-md-6">
                    Tahapan &nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlTahapan" runat="server" CssClass="form-control input-sm"
                            OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                </div>
                <div class="col-sm-12 col-md-6 text-right">
                    Thn. Usulan&nbsp;
                        <asp:DropDownList runat="server" ID="ddlThnUsulan" Enabled="true" CssClass="form-control input-sm"
                            AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                        </asp:DropDownList>
                    &nbsp;&nbsp;
                        Pelaksanaan&nbsp;
                        <asp:DropDownList runat="server" ID="ddlThnPelaksanaan" Enabled="true" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                        </asp:DropDownList>
                </div>
            </div>

            <div class="row" style="padding-top: 10px;">
                <div class="table-responsive">
                    <div class="col-sm-12">
                        <asp:GridView ID="gvRekapSkema" runat="server" GridLines="None"
                            CssClass="table table-striped table-bordered"
                            DataKeyNames="id_skema, nama_skema"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowUpdating="gvRekapSkema_RowUpdating" OnRowCommand="gvRekapSkema_RowCommand">
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
                                        <asp:Label ID="lblNamaSkemaRekap" ForeColor="#229922" runat="server" Text='<%# Eval("nama_skema") %>'></asp:Label><br />
                                        Jml. Proposal:
                                        <asp:Label ID="lblJmlProposalRekap" Font-Bold="true" ForeColor="Maroon" runat="server" Text='<%# Eval("jml_proposal") %>'></asp:Label>
                                        | Jml. Reviewer ditugaskan:
                                        <asp:Label ID="lblJmlPenugasanRekap" Font-Bold="true" ForeColor="Maroon" runat="server" Text='<%# Eval("jml_penugasan_reviewer") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plotting Rev. 1">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbPlotRev1" runat="server" CssClass="btn btn-sm text-white btn-primary rounded-pill w-100p mb-2 mr-1" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="TambahReviewer"><i class="fas fa-users mr-2"></i><%# Eval("jml_plotting_reviewer_1") %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plotting Rev. 2" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbPlotRev2" runat="server" CssClass="btn btn-sm text-white btn-primary rounded-pill w-100p mb-2 mr-1" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="TambahReviewer"><i class="fas fa-users mr-2"></i><%# Eval("jml_plotting_reviewer_2") %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="min-height: 100px; margin: 0 auto;">
                                    <strong>Tidak ada data...</strong>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>

    <%--    <asp:View runat="server" ID="viewDaftarPlotting">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4 style="font-size: 24px;">Plotting Reviewer&nbsp;
                        <asp:Label runat="server" ID="Label5" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>
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
                    <asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-info" OnClick="lbKembali_Click">
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
    </asp:View>--%>

    <%--    <asp:View ID="vDaftarReviewer" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                    <h4>Plotting Reviewer
                        <asp:Label ID="lblTahun" runat="server" CssClass="text-warning"></asp:Label></h4>
                </div>
                <div class="col-sm-3 text-right">
                    <asp:Label runat="server" ID="lblTahapanViewReviewer" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="font-size: 16px;">
                    <asp:Label ID="lblNamaSkema" runat="server"></asp:Label>
                </div>
                <div class="col-md-6">
                    <div class="form-inline" style="float: right">
                        <div class="form-group">
                            <asp:LinkButton ID="lbKembaliKeSkema" runat="server" CssClass="btn btn-info"
                                OnClick="lbKembaliKeSkema_Click">
                        <i class="ml-2"></i>Kembali
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="text-white">
                    Sebaran Beban Reviewer - Usulan
                            <asp:Label ID="lblThnUsulan" runat="server"></asp:Label>
                    | 
                            Pelaksanaan
                            <asp:Label ID="lblThnPelaksanaan" runat="server"></asp:Label>
                </div>
                <div class="text-white">
                    Jumlah Proposal 
                            <asp:Label ID="lblJmlProposal" runat="server" Font-Bold="true" Text="0" />
                    | 
                            Terdistribusi <b>
                                <asp:Label ID="lblJmlTerdistribusi" runat="server" Font-Bold="true" Text="0" /></b>
                </div>

            </div>
            <div class="col-md-3">
                <div class="float-right">
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="float-right">
                        <asp:LinkButton ID="lbPlotOtomatis" runat="server" CssClass="btn btn-primary mr-2">Plotting Otomatis</asp:LinkButton>
                        <div class="input-group">
                            <asp:TextBox ID="tbKataCari" runat="server" CssClass="form-control" placeholder="Nama Reviewer" aria-describedby="btn-addon3.1" />
                            <div class="input-group-append">
                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Cari"></asp:Button>
                            </div>
                            <div class="input-group-append">
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-success" Text="Clear"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th style="text-align: center;">No.</th>
                                    <th>Reviewer</th>
                                    <th style="">Kompetensi</th>
                                    <th style="text-align: center;">Beban Skema Ini</th>
                                    <th style="text-align: center;">Beban Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvReviewer" runat="server"
                                    DataKeyNames="id_penugasan_reviewer"
                                    OnItemUpdating="lvReviewer_ItemUpdating">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>
                                            <td>
                                                <b><%# Eval("nama_reviewer") %></b><br />
                                                <%# Eval("nama_institusi") %>
                                            </td>
                                            <td><%# Eval("kompetensi") %></td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton ID="lbProposal" runat="server"
                                                    CommandName="Update"><%# Eval("jml_plotting") %>&nbsp;Proposal
                                                </asp:LinkButton>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("jml_skema") %>&nbsp;Skema | <%# Eval("jml_proposal") %>&nbsp;Proposal
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="5">
                                                <p class="text-primary">Tidak ada data...</p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </asp:View>
    --%>

    <asp:View ID="vPlotting" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4>Plotting Reviewer</h4>
                </div>
                <div class="col-sm-3 text-right">
                    <asp:Label runat="server" ID="lblTahapanViewPlotting" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    <asp:Label ID="lblSkemaPlotting" runat="server"></asp:Label><br />
                    Thn. Usulan&nbsp;
                    <asp:Label ID="lblThnUsulanPlot" runat="server" Font-Bold="true"></asp:Label>&nbsp;|&nbsp; 
                    Pelaksanaan&nbsp;<asp:Label ID="lblThnPelaksanaanPlot" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-4 text-right">
                    <asp:LinkButton ID="lbKembali" runat="server" CssClass="btn btn-info" OnClick="lbKembaliKeSkema_Click">
                        Kembali
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="form-inline " style="text-align: left;">
                        <div class="col-sm-9" style="padding-right: 5px; color: #7cab3f;">
                            <div class="row form-inline">
                                Jml. Proposal:
                                <asp:Label ID="lblJumlahProposal" runat="server" Font-Bold="true" Text="0" />&nbsp;
                                | Terdistribusi:
                                <asp:Label ID="lblJJumlahTerdistribusi" runat="server" Font-Bold="true" Text="0" />&nbsp;&nbsp;                            
                                <div class="input-group">
                                    <asp:TextBox ID="tbCariProposal" runat="server" CssClass="form-control" placeholder="Judul Proposal" Width="200" />
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" ID="lbCariProposal" CssClass="btn btn-primary" Text="Cari" OnClick="lbCariProposal_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3" style="padding-right: 5px; color: #7cab3f;">
                            <div class="form-inline" style="color: #7cab3f; float: right;">
                                Jml. Baris&nbsp;
                                <asp:DropDownList runat="server" ID="ddlJumlahBaris" Enabled="true" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlJumlahBaris_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="200" Value="200" />
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
                        <asp:GridView ID="gvPlotting" runat="server" AutoGenerateColumns="False"
                            GridLines="None" CssClass="table table-striped table-hover"
                            BorderStyle="None" Width="100%"
                            DataKeyNames="id_usulan_kegiatan, id_transaksi_kegiatan"
                            ShowHeaderWhenEmpty="True" OnRowCommand="gvPlotting_RowCommand" OnRowDataBound="gvPlotting_RowDataBound"
                            OnInit=" gvPlotting_Init">
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
                                    <HeaderStyle Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer 2">
                                    <ItemTemplate>
                                        <div class="row form-inline">
                                            <div style="float: left; vertical-align: top; width: 30px;">
                                                <div>
                                                    <asp:LinkButton runat="server" ID="lbDelReviewer2" CommandName="hapus_rev2" ForeColor="Red"
                                                        CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Hapus Reviewer" Visible="false">
                                                                <i class="fa fa-trash fa-2x"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div style="float: left; vertical-align: top; width: 120px;">
                                                <asp:Label ID="lblNamaReviewer2" runat="server" Text='<%# Bind("nama_reviewer_2") %>'></asp:Label>
                                            </div>
                                            <div class="col-sm-12 col-md-12 col-lg-12 text-left" style="padding-left: 0px;">
                                                <asp:Label ID="lblTglReviewRev2" runat="server" Text='<%# (String.IsNullOrEmpty(Eval("tgl_review_rev2").ToString())) ? "Belum menilai" : DateTime.Parse(Eval("tgl_review_rev2").ToString()).ToString("d") %>'
                                                    Visible='<%# (Eval("nama_reviewer_2").ToString() == "") ? false:true %>'></asp:Label>
                                            </div>
                                            <asp:LinkButton runat="server" ID="lbTambahReviewer2" CommandName="tambah_rev2" ForeColor="White"
                                                CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-success btn-sm" ToolTip="Tambah Reviewer">
                                                                <i class="fas fa-user-plus"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
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
                    <div class="col-sm-12 text-left">
                        <div class="form-inline text-left" style="text-align: left;">
                            <asc:controlPaging runat="server" ID="ktPagingPlotting" OnPageChanging="ktPagingPlotting_PageChanging" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vPilihReviewer" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4>Plotting Reviewer
                        <asp:Label ID="lblJudulPage" runat="server" Text=""></asp:Label>
                    </h4>
                </div>
                <div class="col-sm-3 text-right">
                    <asp:Label runat="server" ID="lblTahapanViewPilih" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-10" style="font-size: 16px;">
                    <asp:Label ID="lblJudulProposal" runat="server" Font-Bold="true"></asp:Label><br />
                    Institusi:
                    <asp:Label ID="lblInstitusiPengusul" runat="server"></asp:Label><br />
                    Bidang Fokus:
                    <asp:Label ID="lblBidangFokus" runat="server"></asp:Label>
                </div>
                <div class="col-sm-2 text-right">
                    <div class="float-right">
                        <asp:LinkButton ID="lblKembaliKePlotting" runat="server" CssClass="btn btn-info" OnClick="lblKembaliKePlotting_Click">Kembali</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="form-inline " style="text-align: left;">
                        <asp:TextBox ID="tbCariReviewer" runat="server" CssClass="form-control" placeholder="Nama Reviewer" />
                        <div class="input-group-append">
                            <asp:Button ID="btnCariReviewer" runat="server" CssClass="btn btn-primary" Text="Cari"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th style="text-align: center;">No.</th>
                                    <th>Nama Reviewer</th>
                                    <th>Kompetensi</th>
                                    <th style="text-align: center;"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvPilihReviewer" runat="server"
                                    DataKeyNames="id_reviewer,id_penugasan_reviewer"
                                    OnItemUpdating="lvPilihReviewer_ItemUpdating">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </td>
                                            <td>
                                                <span style="color: #229922;">
                                                    <asp:Label ID="lblNidn" runat="server" Font-Bold="true" Text='<%# Eval("nidn") %>'></asp:Label>&nbsp;-&nbsp;
                                                <asp:Label ID="lblNamaReviewer" runat="server" Text='<%# Eval("nama_reviewer") %>'></asp:Label>
                                                </span>
                                                <br />
                                                <asp:Label ID="lbinstitusi" runat="server" ForeColor="Maroon" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                            </td>
                                            <td><%# Eval("kompetensi") %></td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton runat="server" ID="lbPilihReviewer" CommandName="Update"
                                                    ToolTip="Pilih Reviewer" CssClass="btn btn-success">
                                                    <i class="fas fa-plus-circle"></i>&nbsp;&nbsp;Plotting
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="4">
                                                <p class="text-primary">Tidak ada data...</p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <%--    <asp:View ID="vProposalReviewer" runat="server">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <div class="row">
                    <div class="col-md-9">
                        <h4>Plotting Reviewer</h4>
                        <div class="text-white">
                            Jumlah Proposal
                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="0" />
                        </div>
                        <h6>
                            <asp:Label ID="Label1" runat="server"></asp:Label></h6>
                        <div class="text-white">
                            Tahun Usulan&nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true"></asp:Label>&nbsp;|&nbsp; 
                            Tahun Pelaksanaan&nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="float-right">
                            <asp:LinkButton ID="lbKembaliKeReviewer" runat="server" CssClass="btn btn-violet"
                                OnClick="lbKembaliKeReviewer_Click"><i class="fas fa-angel-left mr-2"></i>Kembali</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group row">
                            <div class="col-md-4">
                                <label for="inline2mail" class="m-r-15 form-control-label">Baris</label>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList runat="server" ID="ddlJmlBarisProposal" Enabled="true" CssClass="form-control"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="40" Value="60"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="float-right">
                            <div class="input-group">
                                <asp:TextBox ID="tbCariProposalReviewer" runat="server" CssClass="form-control" placeholder="Judul Proposal" />
                                <div class="input-group-append">
                                    <asp:LinkButton runat="server" ID="lbCariProposalReviewer" CssClass="btn btn-primary" Text="Cari" OnClick="lbCariProposalReviewer_Click"></asp:LinkButton>
                                </div>
                                <div class="input-group-append">
                                    <asp:Button ID="btnClearCariProposalReviewer" runat="server" CssClass="btn btn-success"
                                        Text="Clear" OnClick="btnClearCariProposalReviewer_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th style="text-align: center;">No.</th>
                                        <th>Judul</th>
                                        <th><i class="fas fa-th"></i></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvProposalReviewer" runat="server"
                                        DataKeyNames="id_usulan_kegiatan,id_transaksi_kegiatan"
                                        OnItemUpdating="lvPlotting_ItemUpdating"
                                        OnItemDeleting="lvPlotting_ItemDeleting"
                                        OnItemDataBound="lvPlotting_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <span style="color: #229922;">
                                                        <asp:Label ID="lblKodePerguruanTinggi" runat="server" Font-Bold="true" Text='<%# Eval("kd_perguruan_tinggi") %>'></asp:Label>&nbsp;
                                                        <asp:Label ID="lblInstitusi" runat="server" Text='<%# Eval("nama_institusi") %>'></asp:Label>
                                                    </span>
                                                    <br />
                                                    <asp:Label ID="lblJudul" runat="server" Text='<%# Eval("judul") %>'></asp:Label><br />
                                                    <span style="color: Maroon;">Bidang fokus: </span>
                                                    <asp:Label ID="lblRumpunIlmu" runat="server" Text='<%# Eval("bidang_fokus") %>' ForeColor="Maroon" Font-Italic="true" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:LinkButton runat="server" ID="lbDelReviewer" CommandName="Delete"
                                                        CssClass="btn btn-danger btn-mini" ToolTip="Hapus Reviewer" Visible="false">
                                                        <i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td colspan="3">
                                                    <p class="text-primary text-center">Tidak ada data...</p>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asc:controlPaging runat="server" ID="ControlPaging1" OnPageChanging="ktPagingProposal_PageChanging" />
                    </div>
                </div>
            </div>
        </div>
    </asp:View>--%>
</asp:MultiView>
<%--<asc:modalKonfirmasi runat="server" ID="KonfirmasiHapus" />--%>

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
                <asp:Label ID="lblJudulDihapus" runat="server" Text="Label"></asp:Label><br />
                <br />
                Reviewer:
                <asp:Label ID="lblPlottingDihapus" runat="server" Text="" Font-Bold="true" ForeColor="Maroon"></asp:Label><br />
                Hapus data plotting  ini?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-danger pull-right" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

