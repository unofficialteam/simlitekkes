<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rekapUsulanBaru.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.rekapUsulanBaru" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="ktPaging" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkap.ascx" TagPrefix="uc" TagName="pdfUsulanLengkap" %>

<link rel="stylesheet" href="../../assets/dist/css/content.css" />

<asp:MultiView ID="mvRekapUsulanBaru" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftar" runat="server">
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
        </style>
        <div class="container-fluid">
            <div class="row">
                <div class="main-header" style="margin-top: 0px; color: #313233e3;">
                    <h4>Usulan Baru Kegiatan Penelitian</h4>
                </div>
            </div>
            <!-- awal pilih kategori -->
            <div class="row">
                <div class="col-sm-8">
                    <div class="button-list p-b-0 p-t-5">
                        <!-- start Badges Button -->
                        <asp:LinkButton ID="lbdesentralisasi" runat="server" OnClick="lbdesentralisasi_Click">Desentralisasi</asp:LinkButton>
                        <asp:LinkButton ID="lbPenugasan" runat="server" OnClick="lbPenugasan_Click">Penugasan</asp:LinkButton>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="form-inline p-t-5 float-right">
                        <div class="form-group">
                            <label class="form-control-label">Tahun Pelaksanaan&nbsp;&nbsp;</label>
                            <asp:DropDownList ID="ddlThn" AutoPostBack="true" runat="server" Enabled="true"
                                ClientIDMode="Static" Width="100px" CssClass="form-control"
                                OnSelectedIndexChanged="ddlThn_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <!-- akhir pilih kategori -->

            <!-- 4-blocks row start -->
            <div class="row m-b-30 dashboard-header" style="padding-top: 10px;">
                <div class="col-md-2 col-sm-3 col-xs-4">
                    <div class="col-sm-12 p-r-10" style="background-color: #8b8b8b; height: 100px;">
                        <div class="side-box text-right" style="line-height: 1;">
                            <i class="fa fa-file-text-o" style="color: white;"></i>
                        </div>
                        <div class="m-t-40 text-right">
                            <asp:Label ID="lblJmlUsulan" runat="server" Font-Size="28" ForeColor="White" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #323232; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                        Usulan Baru
                    </div>
                </div>
                <div class="col-md-2 col-sm-3 col-xs-4">
                    <div class="col-sm-12 p-r-10" style="background-color: #45a7c7; height: 100px;">
                        <div class="side-box text-right" style="line-height: 1;">
                            <i class="fa fa-envelope-o" style="color: white;"></i>
                        </div>
                        <div class="m-t-40 text-right">
                            <asp:Label ID="lblJmlUsulanDikirim" runat="server" Font-Size="28" ForeColor="White" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #1587a7; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                        Usulan Dikirim
                    </div>
                </div>
                <div class="col-md-2 col-sm-3 col-xs-4">
                    <div class="col-sm-12 p-r-10" style="background-color: #00a65a; height: 100px;">
                        <div class="side-box text-right" style="line-height: 1;">
                            <i class="fa fa-envelope-o" style="color: white;"></i>
                        </div>
                        <div class="m-t-40 text-right">
                            <asp:Label ID="lblJmlDisetujui" runat="server" Font-Size="28" ForeColor="White" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #00863a; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                        Disetujui untuk diseleksi
                    </div>
                </div>
                <div class="col-md-2 col-sm-3 col-xs-4">
                    <div class="col-sm-12 p-r-10" style="background-color: #ed6b59; height: 100px;">
                        <div class="side-box text-right" style="line-height: 1;">
                            <i class="fa fa-envelope-o" style="color: white;"></i>
                        </div>
                        <div class="m-t-40 text-right">
                            <asp:Label ID="lblJmlTdkSetuju" runat="server" Font-Size="28" ForeColor="White" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #dd4b39; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                        Tidak disetujui untuk diseleksi
                    </div>
                </div>
                <div class="col-md-2 col-sm-3 col-xs-4">
                    <div class="col-sm-12 p-r-10" style="background-color: #ffac32; height: 100px;">
                        <div class="side-box text-right" style="line-height: 1;">
                            <i class="fa fa-envelope-o" style="color: white;"></i>
                        </div>
                        <div class="m-t-40 text-right">
                            <asp:Label ID="lblJmlBlmDitinjau" runat="server" Font-Size="28" ForeColor="White" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #f39c12; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                        Usulan Belum Ditinjau
                    </div>
                </div>
            </div>
            <!-- 4-blocks row end -->

            <!-- awal tabel rekap usulan -->
            <div class="row" style="padding-top: 20px;">
                <div class="col-lg-12 col-md-12">
                    <div class="card">
                        <div class="card-block">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <asp:GridView runat="server" ID="gvDaftarSkema" CssClass="table table-striped table-hover"
                                        GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                        DataKeyNames="id_skema,nama_skema" HeaderStyle-HorizontalAlign="Right"
                                        OnRowUpdating="gvDaftarSkema_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <%# Eval("no_baris") %>
                                                    <itemstyle horizontalalign="Center" verticalalign="Top" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <div style="text-align: left">Nama Skema</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbSkema" runat="server" CommandName="Update" ForeColor="#941313">
                                                            <%# Eval("nama_skema") %>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Usulan">
                                                <ItemTemplate>
                                                    <%# Eval("jml_usulan") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dikirim">
                                                <ItemTemplate>
                                                    <%# Eval("jml_dikirim") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="90px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Diseleksi">
                                                <ItemTemplate>
                                                    <%# Eval("jml_disetujui") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tdk masuk seleksi">
                                                <ItemTemplate>
                                                    <%# Eval("jml_tdk_disetujui") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="110px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Belum ditinjau">
                                                <ItemTemplate>
                                                    <%# Eval("jml_belum_diapprove") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="110px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jm. Institusi">
                                                <ItemTemplate>
                                                    <%# Eval("jml_institusi") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="110px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div>
                                                <strong>TIDAK ADA DATA</strong>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- akhir tabel rekap usulan -->
        </div>
    </asp:View>

    <asp:View ID="vDaftarInstitusi" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                    <h4>
                        <asp:Label ID="lblSkemaKegiatan" runat="server"></asp:Label>
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    Thn. Pelaksanaan :
                    <asp:Label ID="lblTahunUsulan" runat="server"></asp:Label>
                    |
                    Jml. Institusi :
                    <asp:Label ID="lblJmlInstitusi" runat="server"></asp:Label>
                </div>
                <div class="col-sm-4 m-t-20 text-right">
                    <asp:LinkButton ID="lbKembaliKeSkema" runat="server" class="btn btn-info"
                        OnClick="lbKembaliKeSkema_Click">Kembali</asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-top: 20px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <asp:GridView runat="server" ID="gvDaftarInstitusi" CssClass="table table-striped table-hover"
                            GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                            DataKeyNames="id_institusi,nama_institusi" HeaderStyle-HorizontalAlign="Right"
                            OnRowUpdating="gvDaftarInstitusi_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <itemstyle horizontalalign="Center" verticalalign="Top" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div style="text-align: left">Nama Institusi</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbInstitusi" runat="server" CommandName="Update" ForeColor="#941313">
                                            <%# Eval("nama_institusi") %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Usulan">
                                    <ItemTemplate>
                                        <%# Eval("jml_usulan") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dikirim">
                                    <ItemTemplate>
                                        <%# Eval("jml_dikirim") %>
                                        <itemstyle horizontalalign="Center" verticalalign="Top" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="90px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Masuk seleksi">
                                    <ItemTemplate>
                                        <%# Eval("jml_disetujui") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tidak masuk seleksi">
                                    <ItemTemplate>
                                        <%# Eval("jml_tdk_disetujui") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="110px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Belum ditinjau">
                                    <ItemTemplate>
                                        <%# Eval("jml_blm_diapprove") %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="110px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div>
                                    <strong>TIDAK ADA DATA</strong>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>

    <asp:View ID="vDaftarUsulan" runat="server">
        <style>
            .radio-button-list label {
                border-radius: 0px;
                background: #fff;
                color: #6c757d;
                border: 1px solid #6c757d;
                font-weight: normal;
            }

            .radio-button-list input[type=radio]:checked + label {
                background: #fff;
                color: #28a745;
                font-weight: normal;
                border: 1px solid #28a745;
            }

            .radio-button-list input[type=radio]:hover + label {
                background: #6c757d;
                color: #fff;
                font-weight: normal;
                border: 1px solid #6c757d;
            }

            .radio-button-list input[type=radio]:checked:hover + label {
                background: #28a745;
                color: #fff;
                border: 1px solid #28a745;
            }
        </style>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                    <h4>
                        <asp:Label ID="lblNamaSkema" runat="server"></asp:Label>&nbsp;
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    <asp:Label ID="lblNamaInstitusi" runat="server"></asp:Label><br />
                    Thn. Usulan:
                    <asp:Label ID="lblTahunUsulan2" ForeColor="ForestGreen" runat="server"></asp:Label>
                </div>
                <div class="col-sm-4 m-t-20 text-right">
                    <asp:LinkButton ID="lbKembaliKeInstitusi" runat="server" class="btn btn-info"
                        OnClick="lbKembaliKeInstitusi_Click">Kembali</asp:LinkButton>
                </div>
            </div>
            <div class="row" style="padding-top: 10px;">
                <div class="col-sm-6 m-t-20">
                    <asp:RadioButtonList ID="rblStatusApproval" runat="server" CssClass="radio-button-list"
                        RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true"
                        OnSelectedIndexChanged="rblStatusApproval_SelectedIndexChanged">
                        <asp:ListItem Text="Usulan baru" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Dikirim" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Masuk seleksi" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Tdk masuk seleksi" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Belum Ditinjau" Value="4"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="margin: 10px 0px;">
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="form-inline" style="text-align: left;">
                            <div style="padding-right: 5px; color: #7cab3f;">
                                Jml. Data:
                                <asp:Label ID="lbJmlUsulan" ForeColor="ForestGreen" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-inline text-right" style="color: #7cab3f;">
                            Jml. Baris&nbsp;
                            <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="true" CssClass="form-control"
                                ClientIDMode="Static" OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged" Width="80px">
                                <asp:ListItem Selected="True" Text="10" Value="10" />
                                <asp:ListItem Text="50" Value="50" />
                                <asp:ListItem Text="100" Value="100" />
                                <asp:ListItem Text="200" Value="200" />
                                <asp:ListItem Text="Semua" Value="0" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;                   
                                <asp:LinkButton ID="lbExcelRekapSkema" runat="server" ToolTip="Export Excel" ForeColor="Green"
                                    OnClick="lbExcelRekapSkema_Click">
                                <i class="far fa-file-excel fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-bottom: 10px;">
                    <div class="panel panel-default" style="min-height: 100px;">
                        <asp:ListView ID="lvDaftarUsulanKonfirmasi" runat="server"
                            DataKeyNames="id_usulan_kegiatan" OnItemDataBound="lvDaftarUsulanKonfirmasi_ItemDataBound"
                            OnItemCommand="lvDaftarUsulanKonfirmasi_ItemCommand">
                            <LayoutTemplate>
                                <table class="table table-hover">
                                    <tbody>
                                        <tr>
                                            <td style="width: 30px; text-align: left; padding: 0; font-weight: bold;">No.</td>
                                            <td style="width: 30px; text-align: left; padding: 0; font-weight: bold;">Usulan</td>
                                            <td style="text-align: left; padding: 0;"></td>
                                            <td style="width: 250px;"></td>
                                        </tr>
                                        <tr id="itemPlaceHolder" runat="server"></tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNomor" runat="server" Text=''></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIsSubmitted" runat="server"
                                            ForeColor="Gray" Visible='<%# (Eval("is_submitted").ToString() == "1") ? false : true %>'><i class="far fa-file-pdf fa-3x"></i></asp:Label>
                                        <asp:LinkButton ID="lbUnduh" runat="server" ForeColor="Red" CommandName="UnduhPdf"
                                            CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Unduh Berkas"
                                            Visible='<%# (Eval("is_submitted").ToString() == "1") ? true : false %>'><i class="far fa-file-pdf fa-3x"></i> </asp:LinkButton>
                                    </td>
                                    <td>
                                        <span style="color: #941313;"><%# Eval("judul") %></span><br />
                                        Ketua: <b style="color: #7cab3f;"><%# Eval("nama_ketua") %></b><br />
                                        Thn. Usulan: <span style="color: #7cab3f;"><%# Eval("thn_usulan_kegiatan") %></span> | Pelaksanaan: <span style="color: #7cab3f;"><%# Eval("thn_pelaksanaan_kegiatan") %></span>
                                        <asp:Label ID="lblDiseleksi" runat="server"
                                            ForeColor="DarkGreen" Visible='<%# (Eval("kd_sts_approvel").ToString() == "1") ? true : false %>'> | <i class="far fa-check-square"></i> Setuju diseleksi</asp:Label>
                                        <asp:Label ID="lblTdkDiseleksi" runat="server" Text= '<%# " Tdk masuk seleksi: "  + Eval("alasan_penolakan").ToString() %>'
                                            ForeColor="DarkRed" Visible='<%# (Eval("kd_sts_approvel").ToString() == "0") ? true : false %>'> | <i class="far fa-times"></i> </asp:Label>
                                    </td>
                                    <td>Bidang fokus: <span style="color: #7cab3f;"><%# Eval("bidang_fokus") %></span><br />
                                        Jml. Anggota: <span style="color: #7cab3f;"><%# Eval("jml_anggota") %></span><br />
                                        Lama Kegiatan: <span style="color: #7cab3f;"><%# Eval("lama_kegiatan") %></span> tahun
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div class="col-sm-12">
                                    <p class="text-primary">Tidak ada data...</p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>

                    </div>
                </div>
                <div class="col-sm-12" style="padding-bottom: 10px;">
                    <div class="row">
                        <uc:ktPaging ID="ktPagging" runat="server" OnPageChanging="Paging_PageChanging" />
                    </div>
                </div>
            </div>
            <uc:pdfUsulanLengkap runat="server" ID="pdfUsulanLengkap" />
        </div>
    </asp:View>

</asp:MultiView>
