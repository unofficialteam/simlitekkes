<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="evaluasiPembahasanPenelitian.ascx.cs" Inherits="simlitekkes.UserControls.Reviewer.evaluasiPembahasanPenelitian" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkap.ascx" TagPrefix="uc" TagName="pdfUsulanLengkap" %>
<%@ Register Src="~/UserControls/Reviewer/report/rekamJejak.ascx" TagPrefix="uc" TagName="ktRekamJejak" %>
<%@ Register Src="~/UserControls/Reviewer/report/hasilPenilaianPembahasan.ascx" TagPrefix="uc" TagName="hasilPenilaian" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

<asp:MultiView ID="mvEvaluasi" runat="server" ActiveViewIndex="0">
    <asp:View ID="vRekapEvaluasi" runat="server">
        <div class="content-header row align-items-center m-0">
            <nav aria-label="breadcrumb" class="col-sm-4 order-sm-last mb-3 mb-sm-0 p-0 ">
            </nav>
            <div class="col-sm-8 header-title p-0">
                <div class="media">
                    <div class="header-icon text-success mr-3"><i class="hvr-buzz-out fas fa-tasks"></i></div>
                    <div class="media-body">
                        <h1 class="font-weight-bold">Evaluasi Pembahasan & Visitasi Penelitian</h1>
                        <small>Summary Usulan yang menjadi penilaian</small>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="form-inline">
                            <div class="form-group">
                                <label class="sr-only">Tahun Usulan</label>
                                <input type="text" readonly class="form-control-plaintext" value="Tahun Usulan" style="width: 100px;">
                            </div>
                            <asp:DropDownList ID="ddlTahunUsulan" runat="server" CssClass="form-control mr-sm-1"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlTahunUsulan_SelectedIndexChanged">
                                <asp:ListItem Text="2020" Value="2020" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                            </asp:DropDownList>
                            <div class="form-group ml-4">
                                <label class="sr-only">Pelaksanaan</label>
                                <input type="text" readonly class="form-control-plaintext" value="Pelaksanaan" style="width: 100px;">
                            </div>
                            <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" CssClass="form-control mr-sm-2"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                <asp:ListItem Text="2021" Value="2021" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="card-block">
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th style="text-align: center;">No.</th>
                                    <th>Institusi Yang Menugaskan</th>
                                    <th>Skema</th>
                                    <th>Evaluasi</th>
                                    <th style="text-align: center;">Status</th>
                                    <th style="text-align: center;"><i class="fas fa-th"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvDaftarPenugasan" runat="server" ItemPlaceholderID="itemPlaceHolder"
                                    DataKeyNames="id_penugasan_reviewer,kd_sts_permanen,nama_skema,kd_program_hibah"
                                    OnItemUpdating="lvDaftarPenugasan_ItemUpdating"
                                    OnItemDataBound="lvDaftarPenugasan_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>
                                            <td><%# Eval("nama_institusi_menugasi") %></td>
                                            <td><%# Eval("nama_skema") %></td>
                                            <td><%# Eval("jml_dievaluasi") + " dari " + Eval("jml_usulan")%></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblStatus" runat="server" CssClass="badge p-2 badge-success"><%# Eval("status_permanen") %></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton ID="lbEvaluasi" runat="server" CssClass="btn btn-primary btn-sm"
                                                    CommandName="Update">Evaluasi
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
                            </tbody>
                        </table>
                        <uc:hasilPenilaian runat="server" ID="ktPdfHasilPenilaian" />
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vDaftarUsulan" runat="server">
        <div class="content-header row align-items-center m-0">
            <nav aria-label="breadcrumb" class="col-sm-4 order-sm-last mb-3 mb-sm-0 p-0 text-right">
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary btn-md"
                    OnClick="lbKembaliKePenugasan_Click"><i class="fas fa-angle-left"></i>&nbsp;Kembali</asp:LinkButton>
            </nav>
            <div class="col-sm-8 header-title p-0">
                <div class="media">
                    <div class="header-icon text-success mr-3"><i class="hvr-buzz-out fas fa-tasks"></i></div>
                    <div class="media-body">
                        <h1 class="font-weight-bold">Evaluasi Dokumen (Pembahasan & Visitasi Penelitian)</h1>
                        <small>Daftar Usulan yang harus dinilai</small>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-sm-12">
                Tahun Usulan&nbsp;<asp:Label ID="lblTahunUsulan" runat="server" Font-Bold="true"></asp:Label>&nbsp;|&nbsp;
                Pelaksanaan&nbsp;<asp:Label ID="lblThnPelaksanaan" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
        <div class="row mt-2 mb-3">
            <div class="col-sm-12">
                <div class="form-inline">
                    <div class="col-md-4 p-t-10">
                        Status&nbsp;<asp:Label ID="lblStatus" runat="server" CssClass="badge badge-success p-2">Terbuka</asp:Label>
                    </div>
                    <div class="col-md-8 text-right">
                        <asp:LinkButton ID="lbUnduhHasilPenilaian" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lbUnduhHasilPenilaian_Click">
                    <i class="fas fa-file-pdf"></i>&nbsp;Unduh Hasil Penilaian
                        </asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbSimpanPermanen" runat="server" CssClass="btn btn-success btn-sm"
                    OnClick="lbSimpanPermanen_Click"><i class="fas fa-save mr-2"></i>Simpan Permanen</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-header-text">Daftar Usulan&nbsp;<b>
                            <asp:Label ID="lblNamaSkema" runat="server"></asp:Label>&nbsp;
                            (<asp:Label ID="lblJmlUsulan" runat="server" Text="0" />)</b>
                        </h5>
                    </div>
                    <div class="card-block">
                        <div class="md-card-block">
                            <asp:ListView ID="lvDaftarUsulan" runat="server"
                                DataKeyNames="id_plotting_reviewer,judul,id_usulan_kegiatan"
                                OnItemUpdating="lvDaftarUsulan_ItemUpdating"
                                OnItemCommand="lvDaftarUsulan_ItemCommand">
                                <LayoutTemplate>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr style="display: none;">
                                                <td style="text-align: left; padding: 0;"></td>
                                                <td style="text-align: left; padding: 0;"></td>
                                                <td></td>
                                            </tr>
                                            <tr id="itemPlaceHolder" runat="server"></tr>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="p-2" style="width: 60px;">
                                            <asp:LinkButton ID="lbUnduh" runat="server" Font-Size="50px"
                                                ForeColor="Red"
                                                CssClass="fa fa-file-pdf btn btn-default" CommandName="UnduhPdf"
                                                CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Unduh Berkas">
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <h7><b style="color: darkblue"><%# Eval("judul") %></b></h7>
                                            <br />
                                            Ketua: <b><%# Eval("nama_ketua") %></b><br />
                                            Bidang Fokus: <i><%# Eval("bidang_fokus") %></i> - Jumlah Anggota: <i><%# Eval("jml_anggota") %></i><br />
                                            Lama Kegiatan: <%# Eval("lama_kegiatan") %> tahun<br />
                                            Dana Usulan: <span style="color: darkgreen">Rp <%# Convert.ToDecimal(Eval("total_dana").ToString()).ToString("N0") %></span>&nbsp;-&nbsp;
                                            Dana Rekomendasi: <span style="color: darkred">Rp <%# Convert.ToDecimal(Eval("total_rekomendasi_dana").ToString()).ToString("N0") %></span>
                                        </td>
                                        <td>
                                            <b>Nilai <span class="badge badge-info p-1"><%# Convert.ToDecimal(Eval("total_nilai").ToString()).ToString("N2") %></span></b><br />
                                            Komponen dinilai <b><%# Eval("jml_dievaluasi") %></b> dari <b><%# Eval("jml_komponen") %></b><br />
                                            <br />
                                            <asp:LinkButton ID="lbPenilaian" runat="server" CssClass="btn btn-primary btn-mini"
                                                CommandName="Update">Penilaian</asp:LinkButton>
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
            </div>
        </div>
        <uc:pdfUsulanLengkap runat="server" ID="pdfUsulanLengkap" />
    </asp:View>
    <asp:View ID="vEvaluasi" runat="server">
        <div class="content-header row align-items-center m-0">
            <nav aria-label="breadcrumb" class="col-sm-4 order-sm-last mb-3 mb-sm-0 p-0 text-right">
                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary"
                    OnClick="lbKembaliKeDaftarUsulan_Click"><i class="fas fa-angle-left"></i>&nbsp;Kembali</asp:LinkButton>
            </nav>
            <div class="col-sm-8 header-title p-0">
                <div class="media">
                    <div class="header-icon text-success mr-3"><i class="hvr-buzz-out fas fa-tasks"></i></div>
                    <div class="media-body">
                        <h1 class="font-weight-bold">Evaluasi Dokumen (Pembahasan & Visitasi Penelitian)</h1>
                        <small>Penilaian Usulan</small>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row m-b-10">
            <div class="col-sm-8 p-t-20">
                <%--<asp:RadioButtonList ID="rblEvaluasi" runat="server" CssClass="radio-button-list"
                    RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true"
                    OnSelectedIndexChanged="rblEvaluasi_SelectedIndexChanged">
                    <asp:ListItem Text="Rekam Jejak" Value="1" ></asp:ListItem>
                    <asp:ListItem Text="Usulan Penelitian" Value="2" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Usulan RAB" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Komentar" Value="4"></asp:ListItem>
                </asp:RadioButtonList>--%>
            </div>
            <div class="col-sm-4 text-right">
                <h6>Total Nilai :
                    <asp:Label ID="lblTotalNilai" runat="server" CssClass="badge p-2 badge-success">0</asp:Label></h6>
                Item yang dinilai
                <asp:Label ID="lblJmlDievaluasi" runat="server" Font-Bold="true">0</asp:Label>&nbsp;
                dari
                <asp:Label ID="lblJmlItem" runat="server" Font-Bold="true">0</asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <h6 class="card-header-text">
                            <asp:Label ID="lblJudul" runat="server"></asp:Label>
                        </h6>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:LinkButton ID="lbUnduhPDFRekamJejak" runat="server"
                                    CssClass="btn btn-danger btn-sm" OnClick="lbUnduhPDFRekamJejak_Click">
                                                <i class="fa fa-file-pdf"></i>&nbsp;Unduh Rekam Jejak
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbUnduhPDFUsulan" runat="server"
                                    CssClass="btn btn-danger btn-sm" OnClick="lbUnduhPDFUsulan_Click">
                                                <i class="fa fa-file-pdf"></i>&nbsp;Unduh Usulan
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbBidangUnggulan" runat="server"
                                    CssClass="btn btn-danger btn-sm" OnClick="lbBidangUnggulan_Click">
                                                <i class="fa fa-list"></i>&nbsp;Bidang-Topik Unggulan PT
                                </asp:LinkButton>
                            </div>
                        </div>
                        <hr />
                        <asp:ListView ID="lvEvaluasiUsulan" runat="server"
                            DataKeyNames="id_komponen_penilaian,id_opsi_komponen_penilaian"
                            OnItemDataBound="lvEvaluasiUsulan_ItemDataBound">
                            <LayoutTemplate>
                                <table class="table table-hover">
                                    <tbody>
                                        <tr style="display: none;">
                                            <td style="width: 30px;"></td>
                                            <td></td>
                                        </tr>
                                        <tr id="itemPlaceHolder" runat="server"></tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.DataItemIndex + 1 %>
                                    </td>
                                    <td>
                                        <b><%# Eval("kriteria_penilaian") %></b>
                                        <br />
                                        <br />
                                        <asp:RadioButtonList ID="rblOpsi" runat="server" RepeatDirection="Vertical"
                                            RepeatLayout="Flow" CssClass="radio-button-list-normal">
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div class="col-sm-12">
                                    <p class="text-primary">Tidak ada data penilaian...</p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <hr />
                        <div class="row">
                            <div class="col-sm-12">
                                <label for="tbKota" class="form-control-label">Kota&nbsp;&nbsp;</label>
                                <br />
                                <asp:TextBox ID="tbKota" runat="server" Width="300" CssClass="form-control"></asp:TextBox>
                                <br />
                                <label class="form-control-label">Komentar :</label>
                                <br />
                                <asp:TextBox ID="tbKomentar" runat="server" TextMode="MultiLine" CssClass="form-control"
                                    Rows="3" Width="100%" ToolTip="Komentar minimal 50 karakter"
                                    placeholder="Komentar minimal 50 karakter"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row m-t-20">
                            <div class="col-sm-12 text-right">
                                <asp:LinkButton ID="lbSimpanKomentar" runat="server" CssClass="btn btn-primary"
                                    OnClick="lbSimpanKomentar_Click"><i class="fas fa-save"></i>&nbsp;&nbsp;Simpan
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc:ktRekamJejak runat="server" ID="ktRekamJejak" Visible="false"></uc:ktRekamJejak>
        <div class="modal fade" id="modalBidangTopikUnggulan" tabindex="-1" role="dialog"
            aria-labelledby="modalTitle" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalTitle">
                            <asp:Label runat="server" ID="lblJudulBidangFokus" Text="Daftar Bidang Fokus RIRN/Bidang Unggulan PT"></asp:Label>
                        </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <asp:GridView runat="server" ID="gvBidangTopikUnggulan" CssClass="table table-hover" ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="False" GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bidang Unggulan">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("bidang_unggulan_perguruan_tinggi")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Topik Unggulan">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("topik_unggulan_perguruan_tinggi")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="col-sm-12">
                                    <p class="text-primary">Bidang dan Topik Unggulan PT tidak ditemukan.</p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>

                        <asp:Panel runat="server" ID="panelRirn">
                            <div style="font-size: 12px;">
                                <div>
                                    Tema:<asp:Label runat="server" ID="lblTema" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                </div>
                                <div>
                                    Topik:<asp:Label runat="server" ID="lblTopik" ForeColor="Maroon" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <asp:GridView runat="server" ID="gvBidangFokusRirn" CssClass="table table-hover" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bidang Fokus">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("bidang_fokus")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="col-sm-12">
                                        <p class="text-primary">Data tidak ditemukan.</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>

                        </asp:Panel>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Tutup</button>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>

</asp:MultiView>