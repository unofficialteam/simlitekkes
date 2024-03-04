<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="monitoringPenugasanReviewerPengabdian.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.monitoringPenugasanReviewerPengabdian" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asc" %>
<%@ Register Src="~/Helper/modalKonfirmasi.ascx" TagPrefix="asc" TagName="modalKonfirmasi" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <style>
            .table a {
                color: #415dfe;
            }

                .table a:hover {
                    color: #8d9efe;
                }

            .table th {
                vertical-align: middle;
            }

            .custom_cell {
                font-weight: bold;
                color: #3e86f1;
            }
        </style>
        <asp:MultiView ID="mvMonitoringPenugasanReviewer" runat="server" ActiveViewIndex="0">
            <asp:View ID="vRekapPenugasanReviewer" runat="server">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-6" style="margin-top: 0px; color: #313233e3;">
                            <h4>Monitoring Penugasan Reviewer Perguruan Tinggi</h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-7">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="button-list p-b-0 p-t-5">
                                        <!-- start Badges Button -->
                                        <asp:LinkButton ID="lbnasional" runat="server" OnClick="lbnasional_Click">Unggulan Nasional</asp:LinkButton>
                                        <asp:LinkButton ID="lbpt" runat="server" OnClick="lbpt_Click">Unggulan PT</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-inline p-t-5 p-b-0 float-right">
                                        <div class="form-group">
                                            <label class="form-control-label">Tahapan Kegiatan&nbsp;&nbsp;</label>
                                            <asp:DropDownList ID="ddlTahapan" runat="server" CssClass="form-control input-sm"
                                                OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Text="--Pilih--" Value="00" Selected="True" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-5">
                            <div class="form-inline p-t-5 float-right">
                                <div class="form-group">
                                    <label class="form-control-label">Tahun Usulan&nbsp;&nbsp;</label>
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
                        </div>
                        <div class="col-sm-7">
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <div class="col-sm-12 p-r-10" style="background-color: #8b8b8b; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fas fa-file-text-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalProposal_listSkema" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #323232; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Total Proposal
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-sm-12 p-r-10" style="background-color: #45a7c7; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fa fa-envelope-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalPenugasan_listSkema" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #1587a7; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Total Penugasan
                                    </div>
                                </div>
                            </div>
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
                                <asp:GridView ID="gvMonitoringPenugasanReviewer" runat="server" GridLines="None"
                                    CssClass="table table-striped table-hover"
                                    DataKeyNames="id_skema, nama_skema, jml_proposal, jml_reviewer"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    OnRowCommand="gvMonitoringPenugasanReviewer_RowCommand"
                                    OnRowUpdating="gvMonitoringPenugasanReviewer_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoRekapSkema" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Skema" HeaderStyle-Width="75%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbNamaSkema" runat="server" Visible="true" CommandName="detailPenugasan"
                                                    Text='<%# Bind("nama_skema") %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                    CssClass="btn btn-link-gridview custom_cell" ToolTip="Tampilkan Detail Penugasan"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jml. Proposal" HeaderStyle-CssClass="text-center" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJmlProposal" runat="server" Text='<%# Bind("jml_proposal") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jml. Penugasan" HeaderStyle-CssClass="text-center" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbJmlReviewer" runat="server" Visible="true" CommandName="gvMonitoringPenugasanReviewer_RowCommand"
                                                    Text='<%# Bind("jml_reviewer") %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                    CssClass="btn btn-link-gridview custom_cell" ToolTip="Tampilkan penugasan reviewer di semua poltekks"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
            <asp:View ID="vRekapPenugasanReviewerPerSkema" runat="server">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                            <h4>Monitoring Penugasan Reviewer Perguruan Tinggi</h4>
                        </div>
                        <div class="col-sm-8">
                            Program Kegiatan:
                            <asp:Label ID="lblProgramKegiatan" runat="server" Font-Bold="true"></asp:Label>
                            | Skema:
                            <asp:Label Font-Bold="true" ID="lblNamaSkema" runat="server"></asp:Label><br />
                            Tahapan:
                            <asp:Label runat="server" ID="lblTahapanPenugasanPerSkema" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-sm-4 text-right">
                            Tahun Usulan:
                            <asp:Label runat="server" ID="lblThnUsulan" Font-Bold="true"></asp:Label>
                            | Pelaksanaan:
                            <asp:Label runat="server" ID="lblThnPelaksanaan" Text="" Font-Bold="true"></asp:Label><br />
                            <asp:LinkButton ID="lbKembaliPerSkema" runat="server" CssClass="btn btn-info"
                                OnClick="lbKembaliPerSkema_Click">Kembali</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row mt-3 mb-5">
                        <div class="col-md-3">
                            <div class="col-sm-12 p-r-10" style="background-color: #8b8b8b; height: 100px;">
                                <div class="side-box text-right" style="line-height: 1;">
                                    <i class="fas fa-file-text-o" style="color: white;"></i>
                                </div>
                                <div class="m-t-40 text-right">
                                    <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalPT_listPT" Text="0" ForeColor="White"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #323232; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                Total Perguruan Tinggi
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-sm-12 p-r-10" style="background-color: #45a7c7; height: 100px;">
                                <div class="side-box text-right" style="line-height: 1;">
                                    <i class="fa fa-envelope-o" style="color: white;"></i>
                                </div>
                                <div class="m-t-40 text-right">
                                    <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalProposal_listPT" Text="0" ForeColor="White"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #1587a7; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                Total Proposal
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-sm-12 p-r-10" style="background-color: #ed6b59; height: 100px;">
                                <div class="side-box text-right" style="line-height: 1;">
                                    <i class="fa fa-envelope-o" style="color: white;"></i>
                                </div>
                                <div class="m-t-40 text-right">
                                    <asp:Label ID="lblTotalPenugasan_listPT" runat="server" Font-Size="28" ForeColor="White" Font-Bold="true" Text="0"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #dd4b39; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                Total Penugasan
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Cari</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="tbPencarianPenugasanReviewerBySkema" CssClass="form-control" placeholder="Nama Perguruan Tinggi"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" ID="lbPencarianPenugasanReviewerBySkema" CssClass="btn btn-outline-info" OnClick="lbPencarianPenugasanReviewerBySkema_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="form-inline" style="color: #7cab3f; float: right">
                                Jml. Baris:&nbsp;
                                <label for="ddlJmlBarisPenugasan"></label>
                                <asp:DropDownList ID="ddlJmlBarisPenugasanPerskema" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="ddlJmlBarisPenugasanPerskema_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="Semua" Value="0" />
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbExcelPenugasanPerSkema"
                                ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcelPenugasanPerSkema_Click">
                                <i class="far fa-file-excel fa-2x"></i>
                            </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvDaftarPenugasanReviewerBySkema" runat="server" GridLines="None"
                                    CssClass="table table-striped table-hover"
                                    DataKeyNames="id_institusi, kd_perguruan_tinggi, nama_institusi, jml_proposal, jml_reviewer, nama_klaster"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    OnRowCommand="gvDaftarPenugasanReviewerBySkema_RowCommand"
                                    OnRowUpdating="gvMonitoringPenugasanReviewer_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoRekapSkema" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Perguruan Tinggi" HeaderStyle-Width="75%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbNamaInstitusi_listPT" runat="server" Visible="true" CommandName="detailPenugasan"
                                                    Text='<%# String.Format("{0} | {1}", Eval("kd_perguruan_tinggi"), Eval("nama_institusi")) %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                    CssClass="btn btn-link-gridview custom_cell" ToolTip="Tampilkan Detail Penugasan"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jml. Proposal" HeaderStyle-CssClass="text-center" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJmlProposal_listPT" runat="server" Text='<%# Bind("jml_proposal") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jml. Penugasan" HeaderStyle-CssClass="text-center" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbJmlReviewer_listPT" runat="server" Visible="true" CommandName="gvMonitoringPenugasanReviewer_RowCommand"
                                                    Text='<%# Bind("jml_reviewer") %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                    CssClass="btn btn-link-gridview custom_cell" ToolTip="Tampilkan penugasan reviewer di semua poltekks"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <strong>DATA TIDAK DITEMUKAN</strong>
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:GridView>
                            </div>
                            <asc:controlPaging runat="server" ID="pagingListPT" OnPageChanging="daftarDataPenugasanReviewerBySkema_PageChanging" />
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View runat="server" ID="vRekapPenugasanReviewerPerPT">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                            <h4>Monitoring Penugasan Reviewer Perguruan Tinggi</h4>
                        </div>
                        <div class="col-sm-8">
                            Program Kegiatan:
                            <asp:Label ID="lblProgramKegiatan_perPT" runat="server" Font-Bold="true"></asp:Label>
                            | Skema:
                            <asp:Label Font-Bold="true" ID="lblNamaSkeam_perPT" runat="server"></asp:Label><br />
                            Tahapan:
                            <asp:Label runat="server" ID="lblTahapan_perPT" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-sm-4 text-right">
                            Tahun Usulan:
                            <asp:Label runat="server" ID="lblThnUsulan_perPT" Font-Bold="true"></asp:Label>
                            | Pelaksanaan:
                            <asp:Label runat="server" ID="lblThnPelaksanaan_perPT" Text="" Font-Bold="true"></asp:Label><br />
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info"
                                OnClick="lbKembaliPerPerPT_Click">Kembali</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12" style="margin-top: 10px; color: #313233e3;">
                            <h5>
                                <asp:Label runat="server" ID="lblNamaInstitusiPenugasan"></asp:Label></h5>
                        </div>
                        <div class="col-sm-5">
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Jumlah Penugasan :&nbsp;
                            <asp:Label ID="lblJumlahPenugasan_perPT" runat="server" Font-Bold="false"></asp:Label>
                                            &nbsp;| Cari</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="tbPencarianNamaReviewer" CssClass="form-control" placeholder="Nama Reviewer"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" ID="lbPencarianNamaReviewer" CssClass="btn btn-outline-info" OnClick="lbPencarianPenugasanReviewerByPT_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="form-inline" style="color: #7cab3f; float: right">
                                Jml. Baris:&nbsp;
                                <label for="ddlJmlBarisPenugasan_perPT"></label>
                                <asp:DropDownList ID="ddlJmlBarisPerPT" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="ddlJmlBarisPenugasanPerPT_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="Semua" Value="0" />
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbExcelPenugasanPerPT"
                                ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcelPenugasanPerPT_Click">
                                <i class="far fa-file-excel fa-2x"></i>
                            </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvDaftarPenugasanReviewerByPT" runat="server" GridLines="None"
                                    CssClass="table table-striped table-hover"
                                    DataKeyNames="id_reviewer, nama, kd_perguruan_tinggi, nama_institusi, bidang_keahlian, nama_klaster, nidn, jml_institusi_yg_menugasi"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    OnRowCommand="gvDaftarPenugasanReviewerByInstitusi_RowCommand"
                                    OnRowUpdating="gvMonitoringPenugasanReviewer_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoRekapSkema" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reviewer" HeaderStyle-Width="35%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbNamaReviewer_perPT" runat="server" Visible="true" CommandName="detailPenugasan"
                                                    Text='<%# String.Format("{0} | {1}", Eval("nidn"), Eval("nama")) %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                    CssClass="custom_cell" ToolTip="Tampilkan Detail Penugasan"></asp:LinkButton><br />
                                                <asp:Label ID="lblNamaPT_perPT" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kompetensi" HeaderStyle-CssClass="text-left" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBidangKeahlian_perPT" runat="server" Text='<%# Bind("bidang_keahlian") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Yang Menugasi" HeaderStyle-CssClass="text-left" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNamaPTYangMenugasi_perPT" runat="server" Text='<%# Bind("institusi_yg_menugasi") %>'></asp:Label><br />
                                                Jml. Institusi: 
                                                <asp:LinkButton ID="lbJmlInstitusiYgMenugasi_perPT" runat="server" Visible="true" CommandName="gvMonitoringPenugasanReviewer_RowCommand"
                                                    Text='<%# Bind("jml_institusi_yg_menugasi") %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                    CssClass="btn btn-link-gridview custom_cell" ToolTip="Tampilkan penugasan reviewer di semua poltekks"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <strong>DATA TIDAK DITEMUKAN</strong>
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:GridView>
                            </div>
                            <asc:controlPaging runat="server" ID="pagingListReviewer_perPT" OnPageChanging="daftarDataPenugasanReviewerByInstitusi_PageChanging" />
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
        <div class="modal modal-primary" id="modalDetail" role="dialog" aria-labelledby="modalDetail">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title font-weight-600" id="dangerModalEditStatus">Penugasan Reviewer Perguruan Tinggi</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                Program Kegiatan:
                            <asp:Label ID="lblProgramKegiatan_modal" runat="server" Font-Bold="true"></asp:Label>
                                | Skema:
                            <asp:Label Font-Bold="true" ID="lblSkema_modal" runat="server"></asp:Label><br />
                                Tahapan:
                            <asp:Label runat="server" ID="lblTahapan_modal" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-sm-12">
                                Tahun Usulan:
                            <asp:Label runat="server" ID="lblTahunUsulan_modal" Font-Bold="true"></asp:Label>
                                | Pelaksanaan:
                            <asp:Label runat="server" ID="lblPelaksanaan_modal" Text="" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-sm-12 mt-5">
                                <h6><asp:Label runat="server" ID="lblNidnNama_modal" Text="" Font-Bold="true"></asp:Label></h6>
                            </div>
                            <div class="col-sm-6">
                                <asp:Label runat="server" ID="lblNamaInstitusi_modal" Text="" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-sm-6 text-right">
                                Jml. Data: 
                                <asp:Label runat="server" ID="lblJmlData_modal" Text="" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvListInstitusiModal" runat="server" GridLines="None"
                                    CssClass="table table-striped table-hover"
                                    DataKeyNames="id_institusi, kd_perguruan_tinggi, nama_institusi, jml_skema"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoRekapSkema" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Perguruan Tinggi" HeaderStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNamaPT_perPT" runat="server" Text='<%# String.Format("{0} | {1}", Eval("kd_perguruan_tinggi"), Eval("nama_institusi")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jml. Skema" HeaderStyle-CssClass="text-left" HeaderStyle-Width="35%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBidangKeahlian_perPT" runat="server" Text='<%# Bind("jml_skema") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <strong>DATA TIDAK DITEMUKAN</strong>
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle CssClass="text-center" HorizontalAlign="Center" />
                                </asp:GridView>
                            </div>
                            <asc:controlPaging runat="server" ID="pagingDataModal" OnPageChanging="daftarDataListInstitusiModal_PageChanging" />
                        </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="lbExcelDaftarReviewer" />
        <asp:PostBackTrigger ControlID="lbExcelPenugasanPerSkema" />
        <asp:PostBackTrigger ControlID="lbExcelPenugasanPerPT" />
        <asp:PostBackTrigger ControlID="lbExcelUsulanBaru" />
    </Triggers>
</asp:UpdatePanel>
