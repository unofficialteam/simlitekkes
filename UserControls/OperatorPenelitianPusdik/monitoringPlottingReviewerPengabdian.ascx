<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="monitoringPlottingReviewerPengabdian.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.monitoringPlottingReviewerPengabdian" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asc" %>
<%@ Register Src="~/Helper/modalKonfirmasi.ascx" TagPrefix="asc" TagName="modalKonfirmasi" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<asp:UpdatePanel runat="server" ID="upPlotingReviwer">
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
        <asp:MultiView runat="server" ID="mvPlotingReviewer">
            <asp:View runat="server" ID="vPlotingReviewer_perSkema">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 0px; color: #313233e3;">
                            <h4>Monitoring Ploting Reviewer</h4>
                        </div>
                        <div class="col-md-7">
                            <div class="row">
                                <div class="col-md-6">
                                    <!-- start Badges Button -->
                                    <asp:LinkButton ID="lbnasional" runat="server" OnClick="lbnasional_Click">Unggulan Nasional</asp:LinkButton>
                                    <asp:LinkButton ID="lbpt" runat="server" OnClick="lbpt_Click">Unggulan PT</asp:LinkButton>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-inline p-t-5 p-b-0 float-right">
                                        <div class="form-group">
                                            <label class="form-control-label">Tahapan Kegiatan&nbsp;&nbsp;</label>
                                            <asp:DropDownList OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged" ID="ddlTahapan" runat="server" CssClass="form-control input-sm" AutoPostBack="True">
                                                <asp:ListItem Text="--Pilih--" Value="00" Selected="True" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-inline p-t-5 float-right">
                                <div class="form-group">
                                    <label class="form-control-label">Tahun Usulan&nbsp;&nbsp;</label>
                                    <asp:DropDownList OnSelectedIndexChanged="ddlTahunUsulan_SelectedIndexChanged" ID="ddlThnUsulan" runat="server" AutoPostBack="True"
                                        CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                        Pelaksanaan&nbsp;
                        <asp:DropDownList OnSelectedIndexChanged="ddlTahunPelaksanaan_SelectedIndexChanged" ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm">
                        </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="row mt-3">
                                <div class="col-md-4">
                                    <div class="col-sm-12 p-r-10" style="background-color: #8b8b8b; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fas fa-file-text-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalProposal_perSkema" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #323232; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Total Proposal
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-sm-12 p-r-10" style="background-color: #45a7c7; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fa fa-envelope-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblPlotingReviewerLengkap_perSkema" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #1587a7; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Total Ploting Reviewer Lengkap
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-sm-12 p-r-10" style="background-color: #00a65a; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fas fa-file-text-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblPlotingReviewerBelumLengkap_perSkema" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #00863a; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Total Ploting Reviewer Belum Lengkap
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 mt-3">
                            <div class="form-inline" style="color: #7cab3f; float: right">
                                <asp:LinkButton runat="server" ID="lbExcel_perSkema"
                                    ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcel_perSkema_Click">
                                <i class="far fa-file-excel fa-2x"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-12 mt-3">
                            <div class="table-responsive">
                                <asp:GridView ID="gvPlotingReviewerPerSkema" runat="server" GridLines="None"
                                    CssClass="table table-striped table-hover table-bordered"
                                    DataKeyNames="id_skema, nama_skema, nama_singkat_skema, jml_judul, plotting_lengkap, plotting_blm_lengkap, total_judul, total_plotting_lengkap, total_plotting_blm_lengkap"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    OnRowCommand="gvPlotingReviewerPerSkema_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Skema" HeaderStyle-Width="55%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbNamaSkema" runat="server" Visible="true" CommandName="DetailPlotingPerSkema"
                                                    Text='<%# Bind("nama_skema") %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                    CssClass="custom_cell" ToolTip="Tampilkan Detail Review"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jml. Proposal" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbJmlJudul_perSkema" runat="server" Text='<%# Bind("jml_judul") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ploting Lengkap" HeaderStyle-CssClass="text-right" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPlotingLengkap" runat="server" Text='<%# Bind("plotting_lengkap") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ploting Belum Lengkap" HeaderStyle-CssClass="text-right" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPlotingBlmLengkap_perSkema" runat="server" Font-Bold="true" ForeColor="#3e86f1" Text='<%# Bind("plotting_blm_lengkap") %>'></asp:Label>
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
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View runat="server" ID="vPlotingReviewer_perPT">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                            <h4>Monitoring Plotting Reviewer</h4>
                        </div>
                        <div class="col-md-8">
                            Program Kegiatan:
                            <asp:Label ID="lblProgramKegiatan_perPT" runat="server" Font-Bold="true"></asp:Label>
                            | Skema:
                            <asp:Label Font-Bold="true" ID="lblNamaSkema_perPT" runat="server"></asp:Label><br />
                            Tahapan:
                            <asp:Label runat="server" ID="lblTahapan_perPT" Font-Bold="true"></asp:Label>
                            | Jml. Reviewer:
                            <asp:Label runat="server" ID="lblJmlReviewer_perPT" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-md-4 text-right">
                            Tahun Usulan:
                            <asp:Label runat="server" ID="lblThnUsulan_perPT" Font-Bold="true"></asp:Label>
                            | Pelaksanaan:
                            <asp:Label runat="server" ID="lblThnPelaksanaan_perPT" Text="" Font-Bold="true"></asp:Label><br />
                            <asp:LinkButton ID="lbKembali_perPT" runat="server" CssClass="btn btn-info"
                                OnClick="lbKembali_perPT_Click">Kembali</asp:LinkButton>
                        </div>
                        <div class="col-md-9">
                            <div class="row mt-3">
                                <div class="col-md-4">
                                    <div class="col-sm-12 p-r-10" style="background-color: #8b8b8b; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fas fa-file-text-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalProposal_perPT" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #323232; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Total Proposal
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-sm-12 p-r-10" style="background-color: #45a7c7; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fa fa-envelope-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblPlotingReviewerLengkap_perPT" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #1587a7; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Total Ploting Reviewer Lengkap
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-sm-12 p-r-10" style="background-color: #00a65a; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fas fa-file-text-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblPlotingReviewerBelumLengkap_perPT" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #00863a; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Total Ploting Reviewer Belum Lengkap
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 mt-3">
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Cari</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="tbPencarian_perPT" CssClass="form-control" placeholder="Nama Perguruan Tinggi"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" ID="lbPencarian_perPT" OnClick="lbPencarian_perPT_Click" CssClass="btn btn-outline-info"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8 mt-3">
                            <div class="form-inline" style="color: #7cab3f; float: right">
                                Jml. Baris:&nbsp;
                                <label for="ddlJumlahBaris_perPT"></label>
                                <asp:DropDownList ID="ddlJumlahBaris_perPT" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm" OnSelectedIndexChanged="ddlJmlBarisPerPT_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="Semua" Value="0" />
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbExcel_perPT"
                                ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcel_perPT_Click">
                                <i class="far fa-file-excel fa-2x"></i>
                            </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-12 mt-2">
                            <div class="table-responsive">
                                <asp:GridView ID="gvPlottingReviewerPerPT" runat="server" GridLines="None"
                                    CssClass="table table-striped table-hover"
                                    DataKeyNames="id_institusi, kd_perguruan_tinggi, nama_institusi, jml_proposal, jml_plotting_lengkap, jml_plotting_blm_lengkap, total_perguruan_tinggi, nama_klaster, total_proposal, total_plotting_selesai, total_plotting_blm_selesai"
                                    OnRowCommand="gvPlottingReviewerPerPT_RowCommand"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Perguruan Tinggi" HeaderStyle-Width="55%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbNamaPT" runat="server" Visible="true" CommandName="DetailReviewerPerPT"
                                                    Text='<%# String.Format("{0} | {1}", Eval("kd_perguruan_tinggi"), Eval("nama_institusi")) %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                    CssClass="custom_cell" ToolTip="Tampilkan Detail Review"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jml. Judul" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbJmlJudul_perPT" runat="server" Text='<%# Bind("jml_proposal") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plotting Lengkap" HeaderStyle-CssClass="text-right" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPenilaianSelesai_perPT" runat="server" Text='<%# Bind("jml_plotting_lengkap") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plotting Belum Lengkap" HeaderStyle-CssClass="text-right" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPenilaianBelumSelesai_perPT" Font-Bold="true" ForeColor="#3e86f1"  runat="server" Text='<%# Bind("jml_plotting_blm_lengkap") %>'></asp:Label>
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
                            <asc:controlPaging runat="server" ID="pagingPlottingReviewerPerPT" OnPageChanging="daftarDataPlottingReviewerPerPT_PageChanging" />
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View runat="server" ID="vPlotingReviewer_perKegiatan">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                            <h4>Monitoring Plotting Reviewer</h4>
                        </div>
                        <div class="col-md-8">
                            Program Kegiatan:
                            <asp:Label ID="lblProgramKegiatan_perKegiatan" runat="server" Font-Bold="true"></asp:Label>
                            | Skema:
                            <asp:Label Font-Bold="true" ID="lblSkema_perKegiatan" runat="server"></asp:Label><br />
                            Tahapan:
                            <asp:Label runat="server" ID="lblTahapan_perKegiatan" Font-Bold="true"></asp:Label>
                            | Jml. Reviewer:
                            <asp:Label runat="server" ID="lblJmlReviewer_perKegiatan" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-md-4 text-right">
                            Tahun Usulan:
                            <asp:Label runat="server" ID="lblTahunUsulan_perKegiatan" Font-Bold="true"></asp:Label>
                            | Pelaksanaan:
                            <asp:Label runat="server" ID="lblTahunPelaksanaan_perKegiatan" Text="" Font-Bold="true"></asp:Label><br />
                            <asp:LinkButton ID="lbKembali_perKegiatan" runat="server" CssClass="btn btn-info"
                                OnClick="lbKembali_perKegiatan_Click">Kembali</asp:LinkButton>
                        </div>
                        <div class="col-md-12 mt-3">
                            <h6>
                                <asp:Label runat="server" ID="lbNamaPT_perKegiatan"></asp:Label>
                            </h6>
                            <asp:LinkButton ID="lbSemua_perKegiatan" runat="server" OnClick="lbSemua_perKegiatan_Click">Semua</asp:LinkButton>
                            <asp:LinkButton ID="lbPlottingLengkap_perKegiatan" runat="server" OnClick="lbPlottingLengkap_perKegiatan_Click">Penilaian Selesai</asp:LinkButton>
                            <asp:LinkButton ID="lbPlottingBlmLengkap_perKegiatan" runat="server" OnClick="lbPlottingBlmLengkap_perKegiatan_Click">Penilaian Belum Selesai</asp:LinkButton>
                        </div>
                        <div class="col-md-12 mt-3">
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Jml. Judul :&nbsp;
                                                    <asp:Label ID="lblJumlahJudul_perKegiatan" runat="server" Font-Bold="false">5</asp:Label>
                                                    &nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;Cari</span>
                                            </div>
                                            <asp:TextBox runat="server" ID="tbPencarian_perKegiatan" CssClass="form-control" placeholder="Nama Peneliti"></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="lbPencarian_perKegiatan" CssClass="btn btn-outline-info" OnClick="lbPencarian_perKegiatan_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-inline" style="color: #7cab3f; float: right">
                                        Jml. Baris:&nbsp;
                                        <label for="ddlJmlBaris_perKegiatan"></label>
                                        <asp:DropDownList ID="ddlJmlBaris_perKegiatan" OnSelectedIndexChanged="ddlJmlBarisPerKegiatan_SelectedIndexChanged" runat="server" AutoPostBack="True"
                                            CssClass="form-control input-sm">
                                            <asp:ListItem Text="10" Value="10" Selected="True" />
                                            <asp:ListItem Text="50" Value="50" />
                                            <asp:ListItem Text="100" Value="100" />
                                            <asp:ListItem Text="Semua" Value="0" />
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton runat="server" ID="lbExcel_perKegiatan"
                                            ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcel_perKegiatan_Click">
                                            <i class="far fa-file-excel fa-2x"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvPlottingReviewer_perKegiatan" runat="server" GridLines="None"
                                            CssClass="table table-striped table-hover"
                                            OnRowCommand="gvPlottingReviewerPerKegiatan_RowCommand"
                                            DataKeyNames="id_usulan_kegiatan, judul, id_institusi, kd_perguruan_tinggi, nama_institusi, jml_personil_penelitian, nama_ketua_usulan, nidn_ketua, total_reviewer_on_usulan, total_reviewer_on_institusi, sts_plotting, nama_klaster"
                                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Judul Kegiatan" HeaderStyle-Width="55%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col-md-1 text-center">
                                                                <i class="far fa-file-pdf fa-3x mr-2"></i>
                                                            </div>
                                                            <div class="col-md-11">
                                                                <asp:LinkButton ID="lbNamaPT" runat="server" Visible="true" CommandName="DetailReviewerPerKegiatan"
                                                                    Text='<%# Bind("judul") %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                                    CssClass="custom_cell" ToolTip="Tampilkan Detail Review"></asp:LinkButton><br />
                                                                <asp:Label runat="server" ID="lbKeterangan" Text='<%# String.Format("({0}) {1} | {2}: {3}", Eval("nidn_ketua"), Eval("nama_ketua_usulan"), "Jml. Anggota", Eval("jml_personil_penelitian")) %>'></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Jml. Reviewer" HeaderStyle-CssClass="text-center" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJmlReviewer" runat="server" Font-Bold="true" ForeColor="#3e86f1"   Text='<%# Bind("total_reviewer_on_usulan") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sts. Plotting" HeaderStyle-CssClass="text-center" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbStsPlotting" runat="server" Text='<%# Bind("sts_plotting") %>'></asp:Label>
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
                                    <asc:controlPaging runat="server" ID="pagingPlottingReviewerPerKegiatan" OnPageChanging="daftarDataPlottingReviewerPerKegiatan_PageChanging" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal modal-primary" id="modalDetail" role="dialog" aria-labelledby="modalDetail">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title font-weight-600" id="dangerModalEditStatus">Monitoring Plotting Reviewer</h5>
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
                                        <asp:Label runat="server" ID="lblTahapan_modal" Font-Bold="true"></asp:Label><br />
                                        <br />
                                        Tahun Usulan:
                                        <asp:Label runat="server" ID="lblTahunUsulan_modal" Font-Bold="true"></asp:Label>
                                        | Pelaksanaan:
                                        <asp:Label runat="server" ID="lblTahunPelaksanaan_modal" Text="" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-sm-12 mt-1">
                                        <hr />
                                        <h6>
                                            <asp:Label runat="server" ID="lblJudul_modal" Text="" Font-Bold="true"></asp:Label></h6>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:Label runat="server" ID="lblNamaNIDN_modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-sm-12 mt-2">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvplottingReviewerModal" runat="server" GridLines="None"
                                                CssClass="table table-striped table-hover"
                                                DataKeyNames="id_reviewer, kompetensi, nama_reviewer, nidn_reviewer, jumlah_anggota"
                                                ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoRekapSkema" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reviewer" HeaderStyle-Width="50%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNamaReviewer" runat="server" Text='<%# String.Format("{0} | {1}", Eval("nidn_reviewer"), Eval("nama_reviewer")) %>'></asp:Label><br />
                                                            <asp:Label ID="lblNamaPT" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>                                                        
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kompetensi" HeaderStyle-CssClass="text-left" HeaderStyle-Width="45%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKomentar" runat="server" Text='<%# Bind("kompetensi") %>'></asp:Label>
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
                                        <asc:controlPaging runat="server" ID="pagingDataModal" OnPageChanging="daftarDataPlottingReviewerModal_PageChanging" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="lbExcel_perSkema" />
        <asp:PostBackTrigger ControlID="lbExcel_perPT" />
        <asp:PostBackTrigger ControlID="lbExcel_perKegiatan" />
    </Triggers>
</asp:UpdatePanel>
