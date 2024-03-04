<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="penugasanReviewerPT.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.penugasanReviewerPT" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="uc" %>

<style>
    .btn-link-gridview {
        color: #fff;
        background-color: #24b1b1;
        border-color: #24b1b1;
        box-shadow: none;
    }

        .btn-link-gridview:hover {
            color: #fff;
            font-weight: bold;
            background-color: #128686;
            border-color: #128686;
            box-shadow: 0 2px 6px rgb(128 128 128);
        }

    .colorTrash {
        color: #a21414;
    }

        .colorTrash:hover {
            color: #d81515;
        }

    .color-nodata-gridview {
        color: #a21414;
    }
</style>

<%--<section class="content">--%>
<asp:MultiView runat="server" ID="mvPenugasanReviewer">
    <asp:View runat="server" ID="viewRekapPenugasanSkema">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6" style="margin-top: 0px; color: #313233e3;">
                    <h4>
                        <asp:Label runat="server" ID="Label1" Text="Penugasan Reviewer"></asp:Label>
                    </h4>
                </div>
            </div>
            <div class="row" style="padding-top: 15px;">
                <div class="col-sm-6">
                    <div class="form-inline">
                        Jenis Kegiatan&nbsp;
                    <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control input-sm"
                        OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-inline">
                        Thn. Usulan&nbsp;
                            <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="True"
                                CssClass="form-control input-sm" Width="100"
                                OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                            </asp:DropDownList>
                        &nbsp;&nbsp;
                            Pelaksanaan&nbsp;
                            <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                                CssClass="form-control input-sm" Width="100"
                                OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                            </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 3px;">
                <div class="col-md-6">
                    <div class="form-inline " style="text-align: left;">
                        <label for="ddlTahapan" class="mr-2">Tahapan &nbsp;&nbsp;&nbsp;</label>
                        <asp:DropDownList ID="ddlTahapan" runat="server" CssClass="form-control input-sm"
                            OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-inline" style="float: right">
                        <div class="form-group">
                            <asp:LinkButton ID="lbExcelRekapSkema" runat="server"
                                ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcelRekapSkema_Click">
                                        <i class="far fa-file-excel fa-2x"></i>
                            </asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbBebanReviewer" runat="server"
                            CssClass="btn btn-success" ToolTip="Beban Reviewer"
                            OnClick="lbBebanReviewer_Click">
                                        <i class="fa fa-arrows-alt"></i>&nbsp;Beban Reviewer
                        </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvRekapPenugasanSkema" runat="server" GridLines="None"
                                        CssClass="table table-striped table-hover"
                                        DataKeyNames="id_institusi, id_skema, nama_skema" HeaderStyle-HorizontalAlign="Center"
                                        ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                        OnRowUpdating="gvRekapPenugasanSkema_RowUpdating" Enabled="false">
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
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reviewer">
                                                <ItemTemplate>
                                                    <asp:Button ID="lbJmlProposal" runat="server" CommandName="Update" Text='<%# Bind("jml_reviewer") %>'
                                                        CssClass="btn btn-link-gridview" ToolTip="Detail Penugasan Reviewer"></asp:Button>
                                                </ItemTemplate>
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
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="viewPenugasan">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4>
                        <asp:Label runat="server" ID="penugasan" Text="Penugasan Reviewer"></asp:Label>&nbsp;
                        <asp:Label runat="server" ID="lblThnUsulanPenugasan" Text="" ForeColor="Green"></asp:Label>
                    </h4>
                </div>
                <div class="col-sm-3 text-right">
                    <asp:Label runat="server" ID="lbltahapanPenugasan" Text="" Font-Bold="true"
                        ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    <asp:Label runat="server" ID="lblNamaSkemaPenugasan" Text=""></asp:Label>&nbsp;-&nbsp;
                        Pelaksanaan&nbsp;
                        <asp:Label runat="server" ID="lblThnPelaksanaanPenugasan" Text=""></asp:Label>
                </div>
                <div class="col-sm-4 text-right">
                    <div class="clearfix">
                        <div style="float: right">
                            <asp:LinkButton runat="server" ID="lbKembaliPenugasan"
                                class="btn btn-info" OnClick="lbKembaliPenugasan_Click">
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
                                Jml. Data:&nbsp;<asp:Label runat="server" ID="lblJmlPenugasan" Text="0"></asp:Label>
                            </div>
                            <div class="col-sm-12 col-md-8">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="tbCariPenugasan" class="form-control" placeholder="Nama Reviewer" aria-describedby="btn-addon2">
                                    </asp:TextBox>
                                    <span class="input-group-btn" id="btn-addon2">
                                        <asp:LinkButton runat="server" ID="lbCariPenugasan" class="btn btn-warning addon-btn waves-effect waves-light"
                                            OnClick="lbCariPenugasan_Click">Cari
                                        </asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline text-right" style="color: #7cab3f;">
                            Jml. Baris:&nbsp;
                                <label for="ddlJmlBarisPenugasan"></label>
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
                            <asp:LinkButton runat="server" ID="lbReviewerInternal"
                                class="btn btn-success waves-effect waves-light" OnClick="lbReviewerInternal_Click">
                            <i class="icofont icofont-plus"></i>
                            <span class="m-l-10">Tambah Reviewer</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
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
                                        <asp:Label ID="lblReviewerDaftarPenugasan" runat="server" Text='<%# Bind("nama_reviewer") %>'></asp:Label><br />
                                        <asp:Label ID="lblInstitusiPenugasan" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        Kompetensi:
                                        <asp:Label ID="lblKompetensiDaftarPenugasan" runat="server" Text='<%# Bind("kompetensi") %>'></asp:Label><br />
                                        No. HP:
                                        <asp:Label ID="lblNoHP" runat="server" Text='<%# Bind("nomor_hp") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Hapus</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbHapusDaftarPenugasan" runat="server" CommandName="Delete"
                                            ToolTip="Hapus Penugasan Reviewer">
                                            <i class="fa fa-trash fa-2x colorTrash"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
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
                <div class="col-sm-12" style="padding-bottom: 10px;">
                    <div class="row">
                        <uc:controlPaging runat="server" ID="pagingDaftarPenugasan" OnPageChanging="daftarPenugasan_PageChanging" />
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="viewDaftarReviewer">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4>
                        <asp:Label runat="server" ID="lblJenisPenugasan" Text=""></asp:Label>&nbsp;
                        <asp:Label runat="server" ID="lblThnUsulanDaftarReviewer" Text="" ForeColor="Green"></asp:Label>
                    </h4>
                </div>
                <div class="col-sm-3 text-right">
                    <asp:Label runat="server" ID="lblTahapanDaftarReviewer" Text="" Font-Bold="true"
                        ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    <asp:Label runat="server" ID="lblNamaSkemaDaftarReviewer" Text=""></asp:Label>&nbsp;-&nbsp;
                        Pelaksanaan&nbsp;
                        <asp:Label runat="server" ID="lblThnPelaksanaanDaftarReviewer" Text=""></asp:Label>
                </div>
                <div class="col-sm-4 text-right">
                    <asp:LinkButton runat="server" ID="lbKembaliDaftarReviewer"
                        class="btn btn-info" OnClick="lbKembaliDaftarReviewer_Click">
                            <span class="ml-2">Kembali</span>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="margin: 10px 0px;">
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="form-inline " style="text-align: left;">
                            <div class="col-sm-12 col-md-4" style="padding-right: 5px; color: #7cab3f;">
                                Jml. Data:&nbsp;<asp:Label runat="server" ID="lblJmlDaftarReviewer" Text="0"></asp:Label>
                            </div>
                            <div class="col-sm-12 col-md-8">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="tbCariDaftarReviewer" class="form-control" placeholder="Nama Reviewer" aria-describedby="btn-addon3">
                                    </asp:TextBox>
                                    <span class="input-group-btn" id="btn-addon3">
                                        <asp:LinkButton runat="server" ID="lbCariDaftarReviewer" class="btn btn-warning addon-btn waves-effect waves-light"
                                            OnClick="lbCariDaftarReviewer_Click">Cari
                                        </asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline text-right" style="color: #7cab3f;">
                            Jml. Baris&nbsp;
                            <asp:DropDownList ID="ddlJmlBarisDaftarReviewer" runat="server" AutoPostBack="True"
                                CssClass="form-control input-sm"
                                OnSelectedIndexChanged="ddlJmlBarisDaftarReviewer_SelectedIndexChanged">
                                <asp:ListItem Text="10" Value="10" Selected="True" />
                                <asp:ListItem Text="50" Value="50" />
                                <asp:ListItem Text="100" Value="100" />
                                <asp:ListItem Text="Semua" Value="0" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbExcelDaftarReviewer"
                                ToolTip="Export Excel" ForeColor="Green" OnClick="lbExcelDaftarReviewer_Click">
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
                            CssClass="table table-striped table-hover" HorizontalAlign="Center"
                            DataKeyNames="id_reviewer, nama"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowUpdating="gvDaftarReviewer_RowUpdating"
                            OnRowDataBound="gvDaftarReviewer_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarReviewer" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaDaftarReviewer" runat="server" Text='<%# Bind("nama") %>'></asp:Label><br />
                                        <asp:Label ID="lblInstitusiPenugasan" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label><br />
                                        <asp:Label ID="lblStsSertifikasi" runat="server" CssClass="label bg-info" Visible="false">Bersertifikat</asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        Kompetensi:
                                                    <asp:Label ID="lblKompetensiDaftarReviewer" runat="server" Text='<%# Bind("kompetensi") %>'></asp:Label><br />
                                        No. HP:
                                                    <asp:Label ID="lblNoHP" runat="server" Text='<%# Bind("nomor_hp") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aksi">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbTugaskanDaftarReviewer" runat="server" CommandName="Update"
                                            CssClass="btn btn-link-gridview" ToolTip="Tugaskan Reviewer">            
                                                        <i class="icofont icofont-plus"></i>
                                                        <span>Tugaskan</span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="55px" />
                                    <ItemStyle Width="55px" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <strong class="color-nodata-gridview">DATA TIDAK DITEMUKAN</strong>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12" style="padding-bottom: 10px;">
                    <div class="row">
                        <uc:controlPaging runat="server" ID="pagingDaftarReviewer" OnPageChanging="pagingDaftarReviewer_PageChanging" />
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="viewBebanReviewer">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-9" style="margin-top: 0px; color: #313233e3;">
                    <h4>
                        <asp:Label runat="server" ID="Label2" Text="Penugasan Reviewer"></asp:Label>&nbsp;
                        <asp:Label runat="server" ID="lblThnUsulanBebanReviewer" Text="" ForeColor="Green"></asp:Label>
                    </h4>
                </div>
                <div class="col-sm-3 text-right">
                    <asp:Label runat="server" ID="lblTahapanBebanReviewer" Text="" Font-Bold="true"
                        ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    <asp:Label runat="server" ID="Label5" Text="Sebaran Beban Reviewer"></asp:Label>&nbsp;-&nbsp;
                        Pelaksanaan&nbsp;
                        <asp:Label runat="server" ID="lblThnPelaksanaanBebanReviewer" Text=""></asp:Label>
                </div>
                <div class="col-sm-4 text-right">
                    <asp:LinkButton runat="server" ID="lbKembaliBebanReviewer"
                        class="btn btn-info" OnClick="lbKembaliBebanReviewer_Click">
                            <span class="ml-2">Kembali</span>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="margin: 10px 0px;">
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="form-inline " style="text-align: left;">
                            <div class="col-sm-12 col-md-4" style="padding-right: 5px; color: #7cab3f;">
                                Jml. Data:&nbsp;<asp:Label runat="server" ID="lblJmlBebanReviewer" Text="0" CssClass="badge badge-info"></asp:Label>
                            </div>
                            <div class="col-sm-12 col-md-8">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="tbCariBebanReviewer" class="form-control" placeholder="Nama Reviewer" aria-describedby="btn-addon4">
                                    </asp:TextBox>
                                    <span class="input-group-append">
                                        <asp:LinkButton runat="server" ID="lbCariBebanReviewer" class="btn btn-warning addon-btn waves-effect waves-light"
                                            OnClick="lbCariBebanReviewer_Click">Cari
                                        </asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline text-right" style="color: #7cab3f;">
                            Jml. Baris&nbsp;
                            <asp:DropDownList ID="ddlJmlBarisBebanReviewer" runat="server" AutoPostBack="True"
                                CssClass="form-control input-sm"
                                OnSelectedIndexChanged="ddlJmlBarisBebanReviewer_SelectedIndexChanged">
                                <asp:ListItem Text="10" Value="10" Selected="True" />
                                <asp:ListItem Text="50" Value="50" />
                                <asp:ListItem Text="100" Value="100" />
                                <asp:ListItem Text="Semua" Value="0" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbExcelBebanReviewer"
                                ToolTip="Export Excel" ForeColor="Green">
                                <i class="far fa-file-excel fa-2x"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <asp:GridView ID="gvBebanReviewer" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="id_reviewer, nama_reviewer"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDataBound="gvBebanReviewer_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoBebanReviewer" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reviewer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaBebanReviewer" runat="server" Text='<%# Bind("nama_reviewer") %>'></asp:Label><br />
                                        <asp:Label ID="lblInstitusiBebanReviewer" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kompetensi">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKomoetensiBebanReviewer" runat="server" Text='<%# Bind("kompetensi") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Beban">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJmlSkemaBebanReviewer" runat="server" Text='<%# Bind("jml_skema") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <strong class="color-nodata-gridview">DATA TIDAK DITEMUKAN</strong>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12" style="padding-bottom: 10px;">
                    <div class="row">
                        <uc:controlPaging runat="server" ID="pagingBebanReviewer" OnPageChanging="bebanReviewer_PageChanging" />
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
                <h4 class="modal-title" id="myModalHapus">Konfirmasi Hapus Penugasan Reviewer</h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus penugasan reviewer atas nama<br />
                <asp:Label runat="server" ID="lblNamaReviewerHapusDaftarPenugasan" Text="" ForeColor="DarkGreen" Font-Bold="true"></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusDaftarPenugasan" runat="server" CssClass="btn btn-info" OnClick="lbHapusDaftarPenugasan_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
