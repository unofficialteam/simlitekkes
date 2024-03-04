<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="evaluasiSubstansiPengabdian.ascx.cs" Inherits="simlitekkes.UserControls.Reviewer.evaluasiSubstansiPengabdian" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkapAbdimas.ascx" TagPrefix="uc" TagName="pdfUsulanLengkap" %>
<%@ Register Src="~/UserControls/Reviewer/report/rekamJejak.ascx" TagPrefix="uc" TagName="ktRekamJejak" %>
<%@ Register Src="~/UserControls/Reviewer/report/hasilPenilaianPengabdian.ascx" TagPrefix="uc" TagName="hasilPenilaian" %>

<style type="text/css">
    .auto-style1 {
        width: 121px;
    }
</style>
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
                        <h1 class="font-weight-bold">Evaluasi Substansi</h1>
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
                                <label for="ddlTahunUsulan" class="form-control-label">Tahun Usulan</label>
                                <asp:DropDownList ID="ddlTahunUsulan" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTahunUsulan_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                            </div>
                            <div class="form-group">
                                <label for="ddlThnPelaksanaan" class="form-control-label">Pelaksanaan</label>
                                <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="card-block table-responsive">
                        <div class="md-card-block">
                            <asp:ListView ID="lvDaftarPenugasan" runat="server" ItemPlaceholderID="itemPlaceHolder"
                                DataKeyNames="id_penugasan_reviewer,kd_sts_permanen,nama_skema"
                                OnItemUpdating="lvDaftarPenugasan_ItemUpdating"
                                OnItemDataBound="lvDaftarPenugasan_ItemDataBound">
                                <LayoutTemplate>
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
                                            <tr id="itemPlaceHolder" runat="server"></tr>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vDaftarUsulan" runat="server">
        <div class="content-header row align-items-center m-0">
            <nav aria-label="breadcrumb" class="col-sm-4 order-sm-last mb-3 mb-sm-0 p-0 text-right">
                <asp:LinkButton ID="lbKembaliKePenugasan" runat="server" CssClass="btn btn-primary btn-md"
                    OnClick="lbKembaliKePenugasan_Click"><i class="fas fa-angle-left"></i>&nbsp;Kembali</asp:LinkButton>
            </nav>
            <div class="col-sm-8 header-title p-0">
                <div class="media">
                    <div class="header-icon text-success mr-3"><i class="hvr-buzz-out fas fa-tasks"></i></div>
                    <div class="media-body">
                        <h1 class="font-weight-bold">Evaluasi Dokumen (Substansi)</h1>
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
                        <h5 class="card-header-text">Daftar Usulan <b>
                            <asp:Label ID="lblNamaSkema" runat="server"></asp:Label>&nbsp;
                            (<asp:Label ID="lblJmlUsulan" runat="server" Text="0" />)</b></h5>
                    </div>
                    <div class="card-block table-responsive">
                        <div class="md-card-block">
                            <asp:ListView ID="lvDaftarUsulan" runat="server"
                                DataKeyNames="id_plotting_reviewer, judul, id_usulan_kegiatan, 
                                jml_dievaluasi, jml_komponen"
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
                                                CssClass="fas fa-file-pdf btn btn-default" CommandName="UnduhPdf"
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
                                        <td style="width: 200px">
                                            <b>Nilai <span class="badge badge-info p-1"><%# Convert.ToDecimal(Eval("total_nilai").ToString()).ToString("N2") %></span></b><br />
                                            Komponen dinilai <b><%# Eval("jml_dievaluasi") %></b> dari <b><%# Eval("jml_komponen") %></b>
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
        <uc:hasilPenilaian runat="server" ID="ktPdfHasilPenilaian" />
        <uc:pdfUsulanLengkap runat="server" ID="pdfUsulanLengkap" />
    </asp:View>
    <asp:View ID="vEvaluasi" runat="server">
        <div class="content-header row align-items-center m-0">
            <nav aria-label="breadcrumb" class="col-sm-4 order-sm-last mb-3 mb-sm-0 p-0 text-right">
                <asp:LinkButton ID="lbKembaliKeDaftarUsulan" runat="server" CssClass="btn btn-primary"
                    OnClick="lbKembaliKeDaftarUsulan_Click"><i class="fas fa-angle-left"></i>&nbsp;Kembali</asp:LinkButton>
            </nav>
            <div class="col-sm-8 header-title p-0">
                <div class="media">
                    <div class="header-icon text-success mr-3"><i class="hvr-buzz-out fas fa-tasks"></i></div>
                    <div class="media-body">
                        <h1 class="font-weight-bold">Evaluasi Dokumen (Substansi)</h1>
                        <small>Penilaian Usulan</small>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row mb-2">
            <div class="col-sm-8 pt-3">
                <asp:RadioButtonList ID="rblEvaluasi" runat="server" CssClass="radio-button-list"
                    RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true"
                    OnSelectedIndexChanged="rblEvaluasi_SelectedIndexChanged">
                    <asp:ListItem Text="Rekam Jejak" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Usulan Pengabdian" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Usulan RAB" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Komentar" Value="4"></asp:ListItem>
                </asp:RadioButtonList>
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
                    <div class="card-block">
                        <div class="md-card-block">
                            <%--<div class="row">
                                <div class="col-sm-12">
                                    <div class="well well-sm">
                                        <asp:Label ID="lblJudul" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="card-body table-responsive">
                                <asp:MultiView ID="mvPenilaian" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="vRekamJejak" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="lbUnduhPDFRekamJejak" runat="server"
                                                    CssClass="btn btn-danger btn-sm" OnClick="lbUnduhPDFRekamJejak_Click">
                                                <i class="fas fa-file-pdf"></i>&nbsp;Unduh Rekam Jejak
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <hr />
                                        <asp:ListView ID="lvEvaluasiRekamJejak" runat="server"
                                            DataKeyNames="id_komponen_penilaian,id_opsi_komponen_penilaian"
                                            OnItemDataBound="lvEvaluasiRekamJejak_ItemDataBound">
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
                                        <div class="row">
                                            <div class="col-sm-12 text-right">
                                                <asp:LinkButton ID="lbSimpanRekamJejak" runat="server" CssClass="btn btn-primary"
                                                    OnClick="lbSimpanRekamJejak_Click">Simpan dan Lanjutkan&nbsp;<i class="fas fa-angle-right"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <uc:ktRekamJejak runat="server" ID="ktRekamJejak" Visible="false"></uc:ktRekamJejak>
                                    </asp:View>
                                    <asp:View ID="vUsulanPenelitian" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="lbUnduhPDFUsulan" runat="server"
                                                    CssClass="btn btn-danger btn-sm" OnClick="lbUnduhPDFUsulan_Click">
                                                <i class="fas fa-file-pdf"></i>&nbsp;Unduh Usulan
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <hr />
                                        <asp:ListView ID="lvEvaluasiUsulan" runat="server"
                                            DataKeyNames="id_komponen_penilaian, id_opsi_komponen_penilaian, urutan_thn_pelaksanaan"
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
                                        <div class="row">
                                            <div class="col-sm-12 text-right">
                                                <asp:LinkButton ID="lbSimpanEvaluasiUsulan" runat="server" CssClass="btn btn-primary"
                                                    OnClick="lbSimpanEvaluasiUsulan_Click">Simpan dan Lanjutkan&nbsp;<i class="fas fa-angle-right"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vUsulanRAB" runat="server">
                                        <asp:UpdatePanel ID="upRAB" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:RadioButtonList ID="rblUrutanTahunRAB" runat="server" CssClass="radio-button-list"
                                                            RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                            AutoPostBack="true" OnSelectedIndexChanged="rblUrutanTahunRAB_SelectedIndexChanged">
                                                            <asp:ListItem Text="Tahun 1" Value="1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Tahun 2" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Tahun 3" Value="3"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <hr />
                                                <asp:ListView ID="lvRekomendasiRAB" runat="server"
                                                    ItemPlaceholderID="itemPlaceHolder"
                                                    DataKeyNames="id_rab_item_belanja"
                                                    OnItemEditing="lvRekomendasiRAB_ItemEditing"
                                                    OnItemUpdating="lvRekomendasiRAB_ItemUpdating"
                                                    OnItemCanceling="lvRekomendasiRAB_ItemCanceling"
                                                    OnItemDataBound="lvRekomendasiRAB_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <table class="table table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th style="width: 30px;">No.</th>
                                                                    <th>Jenis Pembelanjaan</th>
                                                                    <th>Item</th>
                                                                    <th>Satuan</th>
                                                                    <th class="text-center">Vol.</th>
                                                                    <th class="text-right">Biaya Satuan</th>
                                                                    <th class="text-right">Total</th>
                                                                    <th class="text-right">Total Justifikasi</th>
                                                                    <th class="text-center">Aksi</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceHolder" runat="server"></tr>
                                                            </tbody>
                                                            <tfoot>
                                                                <tr style="background-color: #757575; color: #ffffff;">
                                                                    <td colspan="6" class="text-right"><b>TOTAL</b></td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblTotalBiaya" runat="server" Font-Bold="true"></asp:Label></td>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblTotalJustifikasi" runat="server" Font-Bold="true"></asp:Label></td>
                                                                    <td></td>
                                                                </tr>
                                                            </tfoot>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.DataItemIndex + 1 %></td>
                                                            <td><%# Eval("komponen_belanja") %></td>
                                                            <td><%# Eval("nama_item") %></td>
                                                            <td><%# Eval("satuan") %></td>
                                                            <td class="text-center"><%# Convert.ToDecimal(Eval("volume").ToString()).ToString("N0") %></td>
                                                            <td class="text-right"><%# FormatUang(Eval("harga_satuan").ToString()) %></td>
                                                            <td class="text-right"><%# FormatUang(Eval("total_biaya").ToString()) %></td>
                                                            <td class="text-right">
                                                                <asp:Label ID="lblTotalJustifikasiItem" runat="server">
                                                        <%# FormatUang(Eval("total_justifikasi").ToString()) %>
                                                                </asp:Label>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:LinkButton ID="lbJustifikasi" runat="server" CssClass="btn btn-primary btn-mini"
                                                                    CommandName="Edit">Justifikasi</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <tr>
                                                            <td><%# Container.DataItemIndex + 1 %></td>
                                                            <td><%# Eval("komponen_belanja") %></td>
                                                            <td><%# Eval("nama_item") %></td>
                                                            <td><%# Eval("satuan") %></td>
                                                            <td class="text-center"><%# Convert.ToDecimal(Eval("volume").ToString()).ToString("N0") %></td>
                                                            <td class="text-right"><%# FormatUang(Eval("harga_satuan").ToString()) %></td>
                                                            <td class="text-right"><%# FormatUang(Eval("total_biaya").ToString()) %></td>
                                                            <td class="text-right"><%# FormatUang(Eval("total_justifikasi").ToString()) %></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr style="background-color: #d8d8d8;">
                                                            <td></td>
                                                            <td colspan="7">
                                                                <%--<div class="row">--%>
                                                                <h5>Justifikasi</h5>
                                                                <div class="form-inline pt-2 pb-2">
                                                                    <div class="form-group pr-3">
                                                                        <label for="tbVolume" class="form-control-label">Volume&nbsp;&nbsp;</label>
                                                                        <asp:TextBox ID="tbVolume" runat="server" Width="60" CssClass="form-control volume"
                                                                            Text='<%# FormatUang(Eval("volume_rekomendasi").ToString()).Replace(".","") %>'
                                                                            onkeypress="return CheckNumber(event)"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="tbHargaSatuan" class="form-control-label">Biaya Satuan&nbsp;&nbsp;</label>
                                                                        <asp:TextBox ID="tbHargaSatuan" runat="server" Width="160" CssClass="form-control harga"
                                                                            Text='<%# FormatUang(Eval("harga_satuan_rekomendasi").ToString()).Replace(".","") %>'
                                                                            onkeypress="return CheckNumber(event)"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <span class="form-control-label">Komentar :</span>
                                                                <br />
                                                                <asp:TextBox ID="tbKomentar" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                                    Rows="3" Width="100%" Text='<%# Eval("komentar") %>'></asp:TextBox>
                                                                <%--</div>--%>
                                                            </td>
                                                            <td class="text-center">
                                                                <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-success btn-mini m-b-5"
                                                                    CommandName="Update">Simpan</asp:LinkButton><br />
                                                                <asp:LinkButton ID="lbBatal" runat="server" CssClass="btn btn-danger btn-mini"
                                                                    CommandName="Cancel">Batal</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </EditItemTemplate>
                                                </asp:ListView>
                                                <script>
                                                    //function InitAutoNumeric() {
                                                    //    if ($('.volume').length) {
                                                    //        new AutoNumeric('.volume', {
                                                    //            decimalPlaces: 0,
                                                    //            maximumValue: 9999,
                                                    //            minimumValue: 0,
                                                    //            digitGroupSeparator: ''
                                                    //        });
                                                    //        new AutoNumeric('.harga', {
                                                    //            decimalPlaces: 0,
                                                    //            digitGroupSeparator: '.',
                                                    //            decimalCharacter: ',',
                                                    //            minimumValue: 0
                                                    //        });
                                                    //    }
                                                    //}

                                                    //Sys.Application.add_load(InitAutoNumeric);

                                                    function CheckNumber(e) {
                                                        var charCode = (e.which) ? e.which : event.keyCode
                                                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                                                            return false;

                                                        return true;
                                                    };

                                                </script>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="row">
                                            <div class="col-sm-12 text-right">
                                                <asp:LinkButton ID="lbSimpanRAB" runat="server" CssClass="btn btn-primary"
                                                    OnClick="lbSimpanRAB_Click">Simpan dan Lanjutkan&nbsp;<i class="fas fa-angle-right"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vKomentar" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <label for="tbKota" class="form-control-label">Kota&nbsp;&nbsp;</label>
                                                <br />
                                                <asp:TextBox ID="tbKota" runat="server" Width="300" placeholder="Kota tempat menilai" CssClass="form-control"></asp:TextBox>
                                                <br />
                                                <label class="form-control-label">Komentar</label>
                                                <br />
                                                <asp:TextBox ID="tbKomentar" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                    Rows="3" Width="100%"></asp:TextBox>
                                            </div>
                                        </div><br />
                                        <div class="row m-t-20">
                                            <div class="col-sm-12 text-right">
                                                <asp:LinkButton ID="lbSimpanKomentar" runat="server" CssClass="btn btn-primary"
                                                    OnClick="lbSimpanKomentar_Click">Simpan
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>