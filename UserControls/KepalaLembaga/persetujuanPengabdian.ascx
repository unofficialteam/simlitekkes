<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="persetujuanPengabdian.ascx.cs" Inherits="simlitekkes.UserControls.KepalaLembaga.persetujuanPengabdian" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="ktPaging" TagPrefix="asp" %>
<%@ Register Src="~/userControls/Pengusul/report/pdfUsulanLengkap.ascx" TagName="pdfUsulanLengkap" TagPrefix="uc" %>

<asp:MultiView ID="mvSinta" runat="server" ActiveViewIndex="0">
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
                    <h4 style="font-family: Tahoma">Usulan Baru Kegiatan Pengabdian</h4>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="button-list p-b-0 p-t-5">
                        <!-- start Badges Button -->
                        <asp:LinkButton ID="lbnasional" runat="server" OnClick="lbnasional_Click">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Unggulan Nasional&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>
                        <asp:LinkButton ID="lbpt" runat="server" OnClick="lbpt_Click">Unggulan Perguruan Tinggi</asp:LinkButton>
                        <%--<asp:LinkButton ID="lbdesentralisasi" runat="server" OnClick="lbdesentralisasi_Click">Desentralisasi</asp:LinkButton>
                        <asp:LinkButton ID="lbPenugasan" runat="server" OnClick="lbPenugasan_Click">Penugasan</asp:LinkButton>
                        --%>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-inline p-t-5 float-right">
                        <div class="form-group">
                            <label class="form-control-label">Thn. Usulan&nbsp;&nbsp;</label>
                            <asp:DropDownList ID="ddlThnUsulan" AutoPostBack="true" runat="server" Enabled="true"
                                ClientIDMode="Static" Width="100px" CssClass="form-control"
                                OnSelectedIndexChanged="ddlThn_SelectedIndexChanged">
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                            </asp:DropDownList>
                            <label class="form-control-label">Tahun Pelaksanaan&nbsp;&nbsp;</label>
                            <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                                CssClass="form-control input-sm"
                                OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
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
                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #323232; color: white; height: 55px; line-height: 1.2; padding-top: 5px;">
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
                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #1587a7; color: white; height: 55px; line-height: 1.2; padding-top: 5px;">
                        Usulan Dikirim
                    </div>
                </div>
                <div class="col-md-2 col-sm-3 col-xs-4">
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbjmldisetujui_Click" ToolTip="Buka daftar usulan disetujui untuk diseleksi">
                        <div class="col-sm-12 p-r-10" style="background-color: #00a65a; height: 100px;">
                            <div class="side-box text-right" style="line-height: 1;">
                                <i class="fa fa-envelope-o" style="color: white;"></i>
                            </div>
                            <div class="m-t-40 text-right">
                                <asp:Label ID="lblJmlDisetujui" runat="server" Text="" Font-Size="28" ForeColor="White" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #00863a; color: white; height: 55px; line-height: 1.2; padding-top: 5px;">
                            Disetujui untuk diseleksi
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="col-md-2 col-sm-3 col-xs-4" style="min-width: 100px;">
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lbjmltdksetuju_Click" ToolTip="Buka daftar usulan tidak disetujui untuk diseleksi">
                        <div class="col-sm-12 p-r-10" style="background-color: #ed6b59; height: 100px;">
                            <div class="side-box text-right" style="line-height: 1;">
                                <i class="fa fa-envelope-o" style="color: white;"></i>
                            </div>
                            <div class="m-t-40 text-right">
                                <asp:Label ID="lblJmlTdkSetuju" runat="server" Text="" Font-Size="28" ForeColor="White" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #dd4b39; color: white; height: 55px; line-height: 1.2; padding-top: 5px;">
                            Tidak disetujui untuk diseleksi
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="col-md-2 col-sm-3 col-xs-4">
                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lbjmlusulan_Click" ToolTip="Buka daftar usulan belum ditinjau">
                        <div class="col-sm-12 p-r-10" style="background-color: #ffac32; height: 100px;">
                            <div class="side-box text-right" style="line-height: 1;">
                                <i class="fa fa-envelope-o" style="color: white;"></i>
                            </div>
                            <div class="m-t-40 text-right">
                                <asp:Label ID="lblJmlBlmDitinjau" runat="server" Text="" Font-Size="28" ForeColor="White" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #f39c12; color: white; height: 55px; line-height: 1.2; padding-top: 5px;">
                            Usulan belum ditinjau
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
            <!-- 4-blocks row end -->

            <!-- 1-3-block row start -->
            <div class="row" style="padding-top: 15px;">
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <div class="card">
                        <div class="card-block">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <asp:GridView runat="server" ID="gvDataResume" CssClass="table table-striped table-hover"
                                        GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                        ShowFooter="True" HeaderStyle-HorizontalAlign="Right">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <%# Eval("no_baris") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <div style="text-align: left">Nama Skema</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <strong style="color: #941313"><%# Eval("nama_skema") %></strong>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Usulan">
                                                <ItemTemplate>
                                                    <%# Eval("jml_identitas") %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dikirim">
                                                <ItemTemplate>
                                                    <%# Eval("jml_usulan") %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Diseleksi">
                                                <ItemTemplate>
                                                    <%# Eval("jml_disetujui") %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tdk masuk seleksi">
                                                <ItemTemplate>
                                                    <%# Eval("jml_tdk_disetujui") %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Belum ditinjau">
                                                <ItemTemplate>
                                                    <%# Eval("jml_belum_diapprove") %>
                                                </ItemTemplate>
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
        </div>
    </asp:View>

    <asp:View ID="vDaftarUsulanKonfirmasi" runat="server">
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
        <div class="container-fluid mb-2">
            <div class="row">
                <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                    <h4>
                        <asp:Label ID="lbjudul" runat="server" Text="(17)"></asp:Label></h4>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8" style="font-size: 16px;">
                    Jml Usulan:
                    <asp:Label ID="lbjml" runat="server" Text="(17)"></asp:Label>
                </div>
                <div class="col-sm-4 m-t-20 text-right">
                    <asp:LinkButton ID="lbKembali" runat="server" class="btn btn-info" OnClick="lbKembali_Click">
                        Kembali
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12" style="margin: 10px 0px;">
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="form-inline" style="text-align: left;">
                            <div style="padding-right: 5px; color: #7cab3f;">
                            </div>
                        </div>
                        <div class="form-inline text-right" style="color: #7cab3f;">
                            Jml. Baris&nbsp;
                        <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="true" ClientIDMode="Static"
                            Enabled="true" OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Selected="True" Text="10" Value="10" />
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
                <div class="panel panel-default" style="min-height: 300px;">
                    <div class="panel-heading bg-default txt-white" style="font-size: medium">
                        <asp:ListView ID="lvDaftarUsulanKonfirmasi" runat="server"
                            DataKeyNames="id_transaksi_kegiatan, id_usulan, id_usulan_kegiatan,id_personal,judul,                     
                                        nama_skema, thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, nama_ketua"
                            OnItemCommand="lvDaftarUsulanKonfirmasi_ItemCommand" OnItemDataBound="lvDaftarUsulanKonfirmasi_ItemDataBound">
                            <LayoutTemplate>
                                <table class="table table-hover">
                                    <tbody>
                                        <tr>
                                            <td style="width: 30px; text-align: left; padding: 0;"></td>
                                            <td style="text-align: left; padding: 0;"></td>
                                        </tr>
                                        <tr id="itemPlaceHolder" runat="server"></tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbUnduh" runat="server" Font-Size="50px" ForeColor="Red"
                                            CssClass="hvr-buzz-out far fa-file-pdf" CommandName="UnduhPdf"
                                            CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Unduh Berkas">
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <h7><b style="color: blue"><%# Eval("judul") %></b></h7>
                                        <br />
                                        <h8 style="color: green"><%# Eval("program_hibah") %> - <%# Eval("nama_skema") %></h8>
                                        <br />
                                        Ketua: <%# Eval("nama_ketua") %>                             
                                            ( Thn. Usulan: <%# Eval("thn_usulan_kegiatan") %> Thn. Pelaksanaan: <%# Eval("thn_pelaksanaan_kegiatan") %> )<br />
                                        Bidang fokus: <%# Eval("bidang_fokus") %> Jumlah Anggota: <%# Eval("jml_anggota") %><br />
                                        Lama Kegiatan: <%# Eval("lama_kegiatan") %> tahun<br />
                                        <%# Eval("alasan") %> 
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbDisetujui" runat="server" CssClass="btn btn-success fa fa-check waves-effect waves-light"
                                            CommandName="Setuju" CommandArgument="<%# Container.DataItemIndex %>">&nbsp;Disetujui
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbDitolak" runat="server" CssClass="btn btn-danger fa fa-times waves-effect waves-light"
                                            CommandName="Tolak" CommandArgument="<%# Container.DataItemIndex %>">&nbsp;Ditolak
                                        </asp:LinkButton>
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
            </div>
            <div class="col-sm-12" style="padding-bottom: 10px;">
                <div class="row">
                    <asp:ktPaging ID="ktPagging" runat="server" OnPageChanging="Paging_PageChanging" />
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

<div class="modal modal-danger" id="modalKonfirmasi" role="dialog" aria-labelledby="mymodalKonfirmasi">
    <div class="modal-dialog" role="document">
        <div class="modal-content" style="border-color: #6c757d">
            <div class="modal-header">
                <h5 class="modal-title" id="mymodalKonfirmasi">Persetujuan Usulan Penelitian</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                <div>
                    Apakah anda yakin <b>
                        <asp:Label ID="lblModalInfoStsPersetujuan" runat="server" CssClass="label bg-warning" Text=""></asp:Label></b>
                    usulan penelitian:
                </div>
                <div>
                    <asp:Label ID="lblModalJudul" runat="server" ForeColor="Blue" Text=""></asp:Label><br />
                    <asp:Label ID="lblModalSkema" runat="server" ForeColor="Green" Text=""></asp:Label>
                </div>
                <div style="padding-top: 5px;">
                    <b>Ketua:</b>&nbsp;<asp:Label ID="lblModalKetua" runat="server" Text=""></asp:Label><br />
                    <b>Thn. usulan:</b>&nbsp;<asp:Label ID="lblModalThnUsulan" runat="server" ForeColor="DarkRed" Text=""></asp:Label>&nbsp;
                        <b>Thn. Pelaksanaan:</b>&nbsp;<asp:Label ID="lblModalThnPelaksanaan" runat="server" ForeColor="DarkRed" Text=""></asp:Label>
                </div>
                <div style="padding-top: 10px;">
                    <asp:Label ID="Label10" runat="server" Text="Alasan penolakan usulan penelitian:" Font-Bold="true" ForeColor="#cc6600"></asp:Label><br />
                    <asp:CheckBoxList ID="cblPeranPengguna" runat="server" RepeatColumns="1" Width="100%" ForeColor="#cc6600" CellSpacing="10" OnSelectedIndexChanged="cblPeranPengguna_SelectedIndexChanged">
                    </asp:CheckBoxList>
                </div>
                <div style="padding-top: 10px;">
                    <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Batal</button>
                    <asp:LinkButton ID="lbModalStsKonfirmasi" runat="server" CssClass="btn btn-success"
                        OnClick="lbModalStsKonfirmasi_Click">
                        <asp:Label ID="lblModalStsKonfirmasi" runat="server" Text=""></asp:Label>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>
</div>

<div>
    <uc:pdfusulanlengkap runat="server" id="pdfUsulanLengkap" visible="false" />
</div>

