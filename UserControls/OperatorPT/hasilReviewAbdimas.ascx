<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="hasilReviewAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.hasilReviewAbdimas" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asc" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<asp:UpdatePanel runat="server" ID="upHasilReview">
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
        <asp:MultiView runat="server" ID="mvMain">
            <asp:View runat="server" ID="vHasilReviewPerSkema">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 0px; color: #313233e3;">
                            <h4>Hasil Review</h4>
                        </div>
                        <div class="col-md-7">
                            <div class="row">
                                <div class="col-md-6">
                                    <!-- start Badges Button -->
                                    <asp:LinkButton ID="lbPerguruanTinggi" runat="server" OnClick="lbPerguruanTinggi_Click">Perguruan Tinggi</asp:LinkButton>
                                    <asp:LinkButton ID="lbUnggulanNasional" runat="server" OnClick="lbUnggulanNasional_Click">Unggulan Nasional</asp:LinkButton>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-inline p-t-5 p-b-0 float-right">
                                        <div class="form-group">
                                            <label class="form-control-label">Tahapan Kegiatan&nbsp;&nbsp;</label>
                                            <asp:DropDownList ID="ddlTahapan" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlTahapan_SelectedIndexChanged">
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
                                    <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTahunUsulan_SelectedIndexChanged"
                                        CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                Pelaksanaan&nbsp;
                                <asp:DropDownList ID="ddlThnPelaksanaan" OnSelectedIndexChanged="ddlTahunPelaksanaan_SelectedIndexChanged" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm">
                                </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <div class="col-sm-12 p-r-10" style="background-color: #8b8b8b; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fas fa-file-text-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblTotalJudulKegiatan_perSkema" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #323232; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Total Judul Kegiatan
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="col-sm-12 p-r-10" style="background-color: #45a7c7; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fa fa-envelope-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblPenilaianSelesai_perSkema" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #1587a7; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Penilaian Selesai
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="col-sm-12 p-r-10" style="background-color: #00a65a; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fas fa-file-text-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblPenilaianBelumSelesai_perSkema" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #00863a; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Penilaian Belum Selesai
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="col-sm-12 p-r-10" style="background-color: #ed6b59; height: 100px;">
                                        <div class="side-box text-right" style="line-height: 1;">
                                            <i class="fa fa-envelope-o" style="color: white;"></i>
                                        </div>
                                        <div class="m-t-40 text-right">
                                            <asp:Label runat="server" Font-Size="28" Font-Bold="true" ID="lblPenilaianTelahDitetapkan_perSkema" Text="0" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-l-10 p-t-5" style="background-color: #dd4b39; color: white; height: 50px; line-height: 1.2; padding-top: 5px;">
                                        Penilaian Telah Ditetapkan
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 mt-3">
                            <div class="table-responsive">
                                <asp:GridView ID="gvHasilReviewPerSkema" runat="server" GridLines="None"
                                    CssClass="table table-striped table-hover table-bordered"
                                    DataKeyNames="id_skema, nama_skema, nama_singkat_skema, jml_judul, penilaian_selesai, penilaian_blm_selesai, penilaian_telah_ditetapkan, total_judul, total_data_selesai, total_data_blm_selesai, total_data_ditetapkan, nama_klaster"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    OnRowCommand="gvHasilReviewPerSkema_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Skema" HeaderStyle-Width="55%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbNamaSkema" runat="server" Visible="true" CommandName="DetailReviewPerSkema"
                                                    Text='<%# Bind("nama_skema") %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                    CssClass="custom_cell" ToolTip="Tampilkan Detail Review"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jml. Judul" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbJmlJudul_perSkema" runat="server" Text='<%# Bind("jml_judul") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Penilaian Selesai" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPenilaianSelesai_perSkema" runat="server" Text='<%# Bind("penilaian_selesai") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Penilaian Belum Selesai" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPenilaianBelumSelesai_perSkema" runat="server" Text='<%# Bind("penilaian_blm_selesai") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Penilaian Telah Ditetapkan" HeaderStyle-CssClass="text-right" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPenilaianTelahDitetapkan_perSkema" runat="server" Text='<%# Bind("penilaian_telah_ditetapkan") %>'></asp:Label>
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
            <asp:View runat="server" ID="vHasilReviewPerKegiatan">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12" style="margin-top: 0px; color: #313233e3;">
                            <h4>Hasil Review</h4>
                        </div>
                        <div class="col-md-8">
                            Program Kegiatan:
                <asp:Label ID="lblProgramKegiatan_perKegiatan" runat="server" Font-Bold="true"></asp:Label>
                            <br />Skema:
                <asp:Label Font-Bold="true" ID="lblSkema_perKegiatan" runat="server"></asp:Label><br />
                            Tahapan:
                <asp:Label runat="server" ID="lblTahapan_perKegiatan" Font-Bold="true"></asp:Label>
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
                            <asp:LinkButton ID="lbPenilaianSelesai_perKegiatan" runat="server" OnClick="lbPenilaianSelesai_perKegiatan_Click">Penilaian Selesai</asp:LinkButton>
                            <asp:LinkButton ID="lbPenilaianBelumSelesai_perKegiatan" runat="server" OnClick="lbPenilaianBelumSelesai_perKegiatan_Click">Penilaian Belum Selesai</asp:LinkButton>
                            <asp:LinkButton ID="lbTelahDitetapkan_perKegiatan" runat="server" OnClick="lbTelahDitetapkan_perKegiatan_Click">Telah Ditetapkan</asp:LinkButton>
                            <asp:LinkButton ID="lbBelumDitetapkan_perKegiatan" runat="server" OnClick="lbBelumDitetapkan_perKegiatan_Click">Belum Ditetapkan</asp:LinkButton>
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
                                        <asp:GridView ID="gvHasilReview_perKegiatan" runat="server" GridLines="None"
                                            CssClass="table table-striped table-hover"
                                            OnRowDataBound="gvHasilReview_perKegiatan_RowDataBound"
                                            OnRowCommand="gvHasilReviewPerKegiatan_RowCommand"
                                            DataKeyNames="id_usulan_kegiatan, judul, id_institusi, kd_perguruan_tinggi, nama_institusi, id_skema, jml_personil_penelitian, nama_ketua_usulan, hasil_review, status_penetapan"
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
                                                                <asp:LinkButton ID="lbNamaPT" runat="server" Visible="true" CommandName="DetailReviewPerKegiatan"
                                                                    Text='<%# Bind("judul") %>' CommandArgument='<%# Container.DataItemIndex %>'
                                                                    CssClass="custom_cell" ToolTip="Tampilkan Detail Review"></asp:LinkButton><br />
                                                                <asp:Label runat="server" ID="lbKeterangan" Text='<%# String.Format("{0} | {1}: {2}", Eval("nama_ketua_usulan"), "Jml. Anggota", Eval("jml_personil_penelitian")) %>'></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hasil Review" HeaderStyle-CssClass="text-center" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbHasilReview" runat="server" Text='<%# Bind("hasil_review") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status Penetapan" HeaderStyle-CssClass="text-center" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbStatusPenetapan" runat="server" Text='<%# Bind("status_penetapan") %>'></asp:Label>
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
                                    <asc:controlPaging runat="server" ID="pagingHasilReviewPerKegiatan" OnPageChanging="daftarDataHasilReviewPerKegiatan_PageChanging" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal modal-primary" id="modalDetail" role="dialog" aria-labelledby="modalDetail">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title font-weight-600" id="dangerModalEditStatus">Hasil Review</h5>
                                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-9">
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
                                    <%--<div class="col-sm-3 text-right">
                            <asp:Label runat="server" CssClass="mt-5" ID="lblNilai_modal" Text="" Font-Bold="true" Font-Size="30pt"></asp:Label>
                        </div>--%>
                                    <div class="col-sm-12 mt-1">
                                        <hr />
                                        <h6>
                                            <asp:Label runat="server" ID="lblNidnNama_modal" Text="" Font-Bold="true"></asp:Label></h6>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:Label runat="server" ID="lblJudul_modal" Text="" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-sm-12 mt-2">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvHasilReviewModal" runat="server" GridLines="None"
                                                CssClass="table table-striped table-hover"
                                                OnRowDataBound="gvHasilReviewModal_RowDataBound"
                                                ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoRekapSkema" runat="server" Text='<%# Bind("nomor") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reviewer" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnomorReviewer" runat="server" Text='<%# Bind("reviewer") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Komentar" HeaderStyle-CssClass="text-left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKomentar" runat="server" Text='<%# Bind("komentar") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hasil" HeaderStyle-CssClass="text-left" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHasilReview" runat="server" Text='<%# Bind("hasil_review") %>'></asp:Label>
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
                                        <asc:controlPaging runat="server" ID="pagingDataModal" OnPageChanging="daftarDataHasilReviewModal_PageChanging" />
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
        <asp:PostBackTrigger ControlID="lbExcel_perKegiatan" />
    </Triggers>
</asp:UpdatePanel>


