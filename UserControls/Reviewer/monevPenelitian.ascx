<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="monevPenelitian.ascx.cs" Inherits="simlitekkes.UserControls.Reviewer.monevPenelitian" %>

<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkap.ascx" TagPrefix="uc" TagName="pdfUsulanLengkap" %>
<%@ Register Src="~/UserControls/Reviewer/luaranDicapai.ascx" TagPrefix="uc" TagName="luaranDicapai" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfLaporanKemajuanKontrol.ascx" TagName="ktPdfLaporanKemajuanKontrol" TagPrefix="uc" %>

<asp:MultiView ID="mvEvaluasi" runat="server" ActiveViewIndex="0">
    <asp:View ID="vRekapEvaluasi" runat="server">
        <div class="row">
            <div class="col-sm-12 p-0">
                <div class="main-header">
                    <h4>Monitoring dan Evaluasi</h4>
                    &nbsp;<h5>Penelitian</h5>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <%--<h5 class="card-header-text">Basic Table</h5>
                            <p>Basic example <code>without any additional modification</code> classes</p>--%>
                        <div class="form-inline">
                            <div class="form-group">
                                <label for="ddlTahunUsulan" class="form-control-label">Tahun Usulan</label>
                                <asp:DropDownList ID="ddlTahunUsulan" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTahunUsulan_SelectedIndexChanged">
                                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                    <asp:ListItem Text="2018" Value="2018" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                            </div>
                            <div class="form-group">
                                <label for="ddlThnPelaksanaan" class="form-control-label">Pelaksanaan</label>
                                <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                    <asp:ListItem Text="2019" Value="2019" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="card-block">
                        <div class="md-card-block">
                            <asp:ListView ID="lvDaftarPenugasan" runat="server" ItemPlaceholderID="itemPlaceHolder"
                                DataKeyNames="id_penugasan_reviewer,kd_sts_permanen,nama_skema"
                                OnItemUpdating="lvDaftarPenugasan_ItemUpdating">
                                <LayoutTemplate>
                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th style="width: 30px; text-align: center;">No.</th>
                                                <th>Institusi Yang Menugaskan</th>
                                                <th>Skema</th>
                                                <th style="width: 100px;">Evaluasi</th>
                                                <th style="width: 100px; text-align: center;">Status</th>
                                                <th style="width: 60px;"></th>
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
                                            <asp:Label ID="lblStatus" runat="server" CssClass="label label-md label-success"><%# Eval("status_permanen") %></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:LinkButton ID="lbEvaluasi" runat="server" CssClass="btn btn-primary btn-mini"
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
        <div class="row">
            <div class="col-sm-8 p-0">
                <div class="main-header">
                    <h4>Monitoring dan Evaluasi</h4>
                    &nbsp;<h5>Penelitian</h5>
                </div>
            </div>
            <div class="col-sm-4 p-t-20 p-r-20 text-right">
                <asp:LinkButton ID="lbKembaliKePenugasan" runat="server" CssClass="btn btn-primary btn-md"
                    OnClick="lbKembaliKePenugasan_Click"><i class="fa fa-angle-left"></i>&nbsp;Kembali</asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                Tahun Usulan&nbsp;<asp:Label ID="lblTahunUsulan" runat="server" Font-Bold="true" ForeColor="DarkGreen"></asp:Label>&nbsp;|&nbsp;
                Pelaksanaan&nbsp;<asp:Label ID="lblThnPelaksanaan" runat="server" Font-Bold="true" ForeColor="DarkGreen"></asp:Label>
            </div>
        </div>
        <div class="row m-b-15">
            <div class="col-md-4 p-t-10">
                Status&nbsp;<asp:Label ID="lblStatus" runat="server" CssClass="label label-lg label-success">Terbuka</asp:Label>
            </div>
            <div class="col-md-8 text-right">
                <%--<asp:LinkButton ID="lbUnduhHasilPenilaian" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lbUnduhHasilPenilaian_Click">
                    <i class="fa fa-file-pdf-o"></i>&nbsp;Unduh Hasil Penilaian
                </asp:LinkButton>&nbsp;&nbsp;--%>
                <asp:LinkButton ID="lbSimpanPermanen" runat="server" CssClass="btn btn-success btn-sm"
                    OnClick="lbSimpanPermanen_Click">Simpan Permanen</asp:LinkButton>
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
                        <%--<p>Basic example <code>without any additional modification</code> classes</p>--%>
                    </div>
                    <div class="card-block">
                        <div class="md-card-block">
                            <asp:ListView ID="lvDaftarUsulan" runat="server"
                                DataKeyNames="id_plotting_reviewer,id_usulan_kegiatan,jml_komponen,jml_komponen_dinilai"
                                OnItemUpdating="lvDaftarUsulan_ItemUpdating"
                                OnItemCommand="lvDaftarUsulan_ItemCommand">
                                <LayoutTemplate>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr id="itemPlaceHolder" runat="server"></tr>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lbUnduh" runat="server" Font-Size="60px"
                                                ForeColor="Red"
                                                CssClass="hvr-buzz-out far fa-file-pdf btn btn-default" CommandName="UnduhPdf"
                                                CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Unduh Berkas">
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <h7><b style="color: darkblue"><%# Eval("judul") %></b></h7>
                                            <br />
                                            Ketua: <b><%# Eval("nama_ketua") %></b><br />
                                            Bidang Fokus: <i><%# Eval("bidang_fokus") %></i><br />
                                            Lama Kegiatan: <%# Eval("urutan_thn_usulan_kegiatan") %> dari <%# Eval("lama_kegiatan") %> tahun - Jumlah Anggota: <i><%# Eval("jml_anggota") %></i><br />
                                        </td>
                                        <td style="width: 140px">
                                            <b>Nilai :&nbsp;<%# Convert.ToDecimal(Eval("total_nilai").ToString()).ToString("N2") %></b><br /><span style="color: darkred; font-weight: bold">Dinilai :&nbsp;
                                                <%# Eval("jml_komponen_dinilai") %> / <%# Eval("jml_komponen") %>
                                            </span>
                                            <br />
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
        <uc:ktPdfLaporanKemajuanKontrol runat="server" ID="pdfLaporanKemajuan" />
    </asp:View>
    <asp:View ID="vEvaluasi" runat="server">
        <div class="row">
            <div class="col-sm-8 p-0">
                <div class="main-header">
                    <h4>Monitoring dan Evaluasi</h4>
                    &nbsp;<h5>Penelitian</h5>
                </div>
            </div>
            <div class="col-sm-4 p-t-20 p-r-20 text-right">
                <asp:LinkButton ID="lbKembaliKeDaftarUsulan" runat="server" CssClass="btn btn-primary btn-md"
                    OnClick="lbKembaliKeDaftarUsulan_Click"><i class="fa fa-angle-left"></i>&nbsp;Kembali</asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <%--<div class="card-header">
                        <h5 class="card-header-text">Daftar Usulan</h5>                        
                    </div>--%>
                    <div class="card-block">
                        <div class="md-card-block panels-wells">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="jumbotron mr-2 ml-2 mt-2">
                                        <asp:Label ID="lblJudul" runat="server" Font-Bold="true" ForeColor="DarkBlue"></asp:Label><br />
                                        <span style="color: green;">
                                            <asp:Label ID="lblSkemaUsulan" runat="server"></asp:Label>&nbsp;-&nbsp;
                                        <asp:Label ID="lblKategoriPenelitian" runat="server"></asp:Label></span><br />
                                        Ketua :
                                        <asp:Label ID="lblNamaKetua" runat="server" />&nbsp;
                                        <span style="color: darkred;">(Tahun Usulan :
                                            <asp:Label ID="lblThnUsulanData" runat="server" />&nbsp;|&nbsp;
                                            Tahun Pelaksanaan :
                                            <asp:Label ID="lblThnPelaksanaanData" runat="server" />)</span><br />
                                        Tahun Kegiatan :
                                        <asp:Label ID="lblUrutanTahun" runat="server" />
                                        dari
                                        <asp:Label ID="lblNamaKegiatan" runat="server" />
                                        tahun | Jumlah Anggota :
                                        <asp:Label ID="lblJmlAnggota" runat="server" /><br />
                                        Bidang Fokus :
                                        <asp:Label ID="lblBidangFokus" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="row m-b-20">
                                <div class="col-sm-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading bg-primary">
                                            Kesesuaian Penelitian Dengan Proposal (Maks: 10)
                                        </div>
                                        <div class="panel-body">
                                            <div>
                                                <asp:LinkButton runat="server" ID="lbUnduhPdfUsulanLengkap" CssClass="btn btn-danger waves-effect waves-light" OnClick="lbUnduhPdfUsulanLengkap_Click">
                                                    <i class="hvr-buzz-out far fa-file-pdf"></i><span class="m-l-10">Proposal</span>
                                                </asp:LinkButton>
                                                &nbsp;
                                                <asp:LinkButton runat="server" ID="lbUnduhLapKemajuan" CssClass="btn btn-danger waves-effect waves-light" OnClick="lbUnduhLapKemajuan_Click">
                                                    <i class="hvr-buzz-out far fa-file-pdf"></i><span class="m-l-10">
                                                        <asp:Label runat="server" ID="lblUndulLapKemajuan" Text="Laporan kemajuan"></asp:Label>
                                                    </span>
                                                </asp:LinkButton>
                                                &nbsp;
                                                <asp:LinkButton runat="server" ID="lbCatatanHarian" CssClass="btn btn-danger waves-effect waves-light" OnClick="lbCatatanHarian_Click">
                                                    <i class="fa fa-edit"></i><span class="m-l-10">
                                                        <asp:Label runat="server" ID="Label1" Text="Catatan Harian"></asp:Label>
                                                    </span>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="jumbotron mr-2 ml-2 mt-2">
                                                <h6>PENILAIAN</h6>
                                                <div class="row">
                                                    <div class="col-sm-10">
                                                        <div class="form-group row">
                                                            <label for="ddlStatus" class="col-xs-3 col-form-label form-control-label">
                                                                a. Status</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlSkorID1" runat="server" CssClass="form-control-sm"
                                                                    DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                    AppendDataBoundItems="true">
                                                                </asp:DropDownList>
                                                                <small class="text-muted">(skor)</small>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="ddlStatus" class="col-xs-3 col-form-label form-control-label">
                                                                b. Kualitas Kesesuaian</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlBobotID2" runat="server" CssClass="form-control-sm"
                                                                    DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                    AppendDataBoundItems="true">
                                                                </asp:DropDownList>
                                                                <small class="text-muted">(bobot)</small>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-sm-2 text-center font-weight-bold ">
                                                        Nilai :<br />
                                                        <asp:Label ID="lblNilaiKesesuaian" runat="server" Text="0" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-footer text-right font-weight-bold ">
                                            Total Nilai :
                                            <asp:Label ID="lblTotalNilaiKesesuaian" runat="server" Text="0" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="panelPengembangan" runat="server">
                                <div class="row m-b-20">
                                    <div class="col-sm-12">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading bg-primary">
                                                Integritas, Dedikasi dan Kekompakan Tim Peneliti (Maks: 10)
                                            </div>
                                            <div class="panel-body">
                                                <div class="jumbotron mr-2 ml-2 mt-2">
                                                    <h6>PENILAIAN</h6>
                                                    <div class="row">
                                                        <div class="col-sm-10">
                                                            <div class="form-group row">
                                                                <label for="ddlSkorID11" class="col-xs-3 col-form-label form-control-label">
                                                                    a. Status</label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlSkorID11" runat="server" CssClass="form-control-sm"
                                                                        DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                        AppendDataBoundItems="true">
                                                                    </asp:DropDownList>
                                                                    <small class="text-muted">(skor)</small>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label for="ddlStatus" class="col-xs-3 col-form-label form-control-label">
                                                                    b. Kondisi Kekuatan Tim</label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlBobotID12" runat="server" CssClass="form-control-sm"
                                                                        DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                        AppendDataBoundItems="true">
                                                                    </asp:DropDownList>
                                                                    <small class="text-muted">(bobot)</small>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-2 text-center font-weight-bold ">
                                                            Nilai :<br />
                                                            <asp:Label ID="lblNilaiIntegritas" runat="server" Text="0" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-footer text-right font-weight-bold ">
                                                Total Nilai :
                                                <asp:Label ID="lblTotalNilaiIntegritas" runat="server" Text="0" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row m-b-20">
                                    <div class="col-sm-12">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading bg-primary">
                                                Realisasi Kerjasama (Maks: 10)
                                            </div>
                                            <div class="panel-body">
                                                <div class="jumbotron mr-2 ml-2 mt-2">
                                                    <h6>PENILAIAN</h6>
                                                    <div class="row">
                                                        <div class="col-sm-10">
                                                            <div class="form-group row">
                                                                <label for="ddlStatus" class="col-xs-3 col-form-label form-control-label">
                                                                    a. Status</label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlSkorID13" runat="server" CssClass="form-control-sm"
                                                                        DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                        AppendDataBoundItems="true">
                                                                    </asp:DropDownList>
                                                                    <small class="text-muted">(skor)</small>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label for="ddlStatus" class="col-xs-3 col-form-label form-control-label">
                                                                    b. Kesesuaian Kerjasama yang dijanjikan dalam Pelaksanaan</label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlBobotID14" runat="server" CssClass="form-control-sm"
                                                                        DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                        AppendDataBoundItems="true">
                                                                    </asp:DropDownList>
                                                                    <small class="text-muted">(bobot)</small>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-2 text-center font-weight-bold ">
                                                            Nilai :<br />
                                                            <asp:Label ID="lblNilaiRealisasiKerjasama" runat="server" Text="0" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-footer text-right font-weight-bold ">
                                                Total Nilai :
                                                <asp:Label ID="lblTotalNilaiRealisasiKerjasama" runat="server" Text="0" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="panelTerapan" runat="server" CssClass="row m-b-20">
                                <div class="col-sm-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading bg-primary">
                                            Realisasi Keterlibatan / Kontribusi Mitra (Maks: 10)
                                        </div>
                                        <div class="panel-body">
                                            <div>
                                                <asp:LinkButton runat="server" ID="lbUnduhPdfRealisasiMitra" CssClass="btn btn-danger waves-effect waves-light" OnClick="lbUnduhPdfRealisasiMitra_Click">
                                                    <i class="hvr-buzz-out far fa-file-pdf"></i><span class="m-l-10">Dokumen Realisasi Keterlibatan/Kontribusi Mitra</span>
                                                </asp:LinkButton>
                                                <%--<button type="button" class="btn btn-danger waves-effect waves-light" data-toggle="tooltip" data-placement="top" title="Proposal">
                                                    <i class="fa fa-file-pdf-o"></i><span class="m-l-10">Dokumen Realisasi Keterlibatan/Kontribusi Mitra</span>
                                                </button>--%>
                                            </div>
                                            <%--<hr />--%>
                                            <div class="jumbotron mr-2 ml-2 mt-2">
                                                <h6>PENILAIAN</h6>
                                                <div class="row">
                                                    <div class="col-sm-10">
                                                        <div class="form-group row">
                                                            <label for="ddlStatus" class="col-xs-3 col-form-label form-control-label">
                                                                a. Status</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlSkorID9" runat="server" CssClass="form-control-sm"
                                                                    DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                    AppendDataBoundItems="true">
                                                                </asp:DropDownList>
                                                                <small class="text-muted">(skor)</small>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="ddlStatus" class="col-xs-3 col-form-label form-control-label">
                                                                b. Kesesuaian Janji Keterlibatan / Kontribusi Mitra</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlBobotID10" runat="server" CssClass="form-control-sm"
                                                                    DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                    AppendDataBoundItems="true">
                                                                </asp:DropDownList>
                                                                <small class="text-muted">(bobot)</small>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-sm-2 text-center font-weight-bold ">
                                                        Nilai :<br />
                                                        <asp:Label ID="lblNilaiRealisasiMitra" runat="server" Text="0" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-footer text-right font-weight-bold ">
                                            Total Nilai :
                                            <asp:Label ID="lblTotalNilaiRealisasiMitra" runat="server" Text="0" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="row m-b-20">
                                <div class="col-sm-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading bg-primary">
                                            Kemajuan Ketercapaian Luaran Wajib Yang Dijanjikan 
                                        </div>
                                        <div class="panel-body">
                                            <table style="width: 100%">
                                                <asp:ListView ID="lvLuaranWajib" runat="server"
                                                    DataKeyNames="id_luaran_dijanjikan"
                                                    OnItemDataBound="lvLuaranWajib_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="font-weight-bold" style="width: 30px; vertical-align: top;">
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>
                                                            <td style="vertical-align: top;">
                                                                <!-- Jenis Luaran : <%# Eval("nama_jenis_luaran")%><br />
                                                                Target Luaran : <%# Eval("nama_target_capaian_luaran") %> -->
                                                                <uc:luaranDicapai runat="server" ID="kontrolLuaranDicapai" />

                                                                <div class="jumbotron mr-2 ml-2 mt-2">
                                                                    <h6>PENILAIAN</h6>

                                                                    <div class="form-group row">
                                                                        <label for="ddlStatusID3" class="col-xs-2 col-form-label form-control-label">
                                                                            a. Status</label>
                                                                        <div class="col-sm-10">
                                                                            <asp:DropDownList ID="ddlSkorID3" runat="server" CssClass="form-control-sm"
                                                                                DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                                AppendDataBoundItems="true">
                                                                            </asp:DropDownList>
                                                                            <small class="text-muted">(skor)</small>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="ddlItemID4" class="col-xs-2 col-form-label form-control-label">
                                                                            b. Bobot Luaran</label>
                                                                        <div class="col-sm-10">
                                                                            Kualitas Dokumen Luaran<br />
                                                                            <asp:DropDownList ID="ddlBobotID4" runat="server" CssClass="form-control-sm"
                                                                                DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                                AppendDataBoundItems="true">
                                                                            </asp:DropDownList><br />
                                                                            Kesesuaian Isi Dokumen Dengan Substansi Penelitian<br />
                                                                            <asp:DropDownList ID="ddlBobotID5" runat="server" CssClass="form-control-sm"
                                                                                DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                                AppendDataBoundItems="true">
                                                                            </asp:DropDownList><br />
                                                                            Kesesuaian Dengan Periode Pendanaan<br />
                                                                            <asp:DropDownList ID="ddlBobotID6" runat="server" CssClass="form-control-sm"
                                                                                DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                                AppendDataBoundItems="true">
                                                                            </asp:DropDownList><br />
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </td>
                                                            <td class="text-center font-weight-bold" style="width: 100px; vertical-align: top;">Nilai :<br />
                                                                <asp:Label ID="lblNilaiLuaranWajib" runat="server" Text="0" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <tr>
                                                            <td colspan="3">
                                                                <div class="jumbotron mr-2 ml-2 mt-2 text-center">
                                                                    <h6>Data Luaran tidak ditemukan</h6>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </table>
                                        </div>
                                        <div class="panel-footer text-right font-weight-bold ">
                                            Total Nilai :
                                            <asp:Label ID="lblTotalNilaiLuaranWajib" runat="server" Text="0" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row m-b-20">
                                <div class="col-sm-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading bg-primary">
                                            Kemajuan Ketercapaian Luaran Tambahan Yang Dijanjikan 
                                        </div>
                                        <div class="panel-body">
                                            <table style="width: 100%">
                                                <asp:ListView ID="lvLuaranTambahan" runat="server"
                                                    DataKeyNames="id_luaran_dijanjikan"
                                                    OnItemDataBound="lvLuaranTambahan_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="font-weight-bold" style="width: 30px; vertical-align: top;">
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>
                                                            <td style="vertical-align: top;"><%--Jenis Luaran : <%# Eval("nama_jenis_luaran")%><br />
                                                                Target Luaran : <%# Eval("nama_target_capaian_luaran") %>--%>
                                                                <asp:PlaceHolder ID="phLuaran" runat="server" />
                                                                <uc:luaranDicapai runat="server" ID="kontrolLuaranDicapai" />
                                                                <div class="jumbotron mr-2 ml-2 mt-2">
                                                                    <h6>PENILAIAN</h6>

                                                                    <div class="form-group row">
                                                                        <label for="ddlStatus" class="col-xs-2 col-form-label form-control-label">
                                                                            a. Status</label>
                                                                        <div class="col-sm-10">
                                                                            <asp:DropDownList ID="ddlSkorID7" runat="server" CssClass="form-control-sm"
                                                                                DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                                AppendDataBoundItems="true">
                                                                            </asp:DropDownList>
                                                                            <small class="text-muted">(skor)</small>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="ddlStatus" class="col-xs-2 col-form-label form-control-label">
                                                                            b. Bobot Luaran</label>
                                                                        <div class="col-sm-10">
                                                                            Kesesuaian Jenis Luaran Tambahan Dengan Substansi Penelitian<br />
                                                                            <asp:DropDownList ID="ddlBobotID8" runat="server" CssClass="form-control-sm"
                                                                                DataTextField="status_capaian" DataValueField="id_opsi_nilai_monev"
                                                                                AppendDataBoundItems="true">
                                                                            </asp:DropDownList><br />
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </td>
                                                            <td class="text-center font-weight-bold" style="width: 100px; vertical-align: top;">Nilai :<br />
                                                                <asp:Label ID="lblNilaiLuaranTambahan" runat="server" Text="0" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <tr>
                                                            <td colspan="3">
                                                                <div class="jumbotron mr-2 ml-2 mt-2 text-center">
                                                                    <h6>Data Luaran tidak ditemukan</h6>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </table>
                                        </div>
                                        <div class="panel-footer text-right font-weight-bold ">
                                            Total Nilai :
                                            <asp:Label ID="lblTotalNilaiLuaranTambahan" runat="server" Text="0" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row m-b-20">
                                <div class="col-sm-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading bg-primary">
                                            Komentar
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <%--<label for="tbKota" class="form-control-label">Kota&nbsp;&nbsp;</label>
                                                    <br />
                                                    <asp:TextBox ID="tbKota" runat="server" Width="300" CssClass="form-control"></asp:TextBox>
                                                    <br />--%>
                                                    <label class="form-control-label">Komentar :</label>
                                                    <br />
                                                    <asp:TextBox ID="tbKomentar" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                        Rows="3" Width="100%" ToolTip="Komentar minimal 50 karakter"
                                                        placeholder="Komentar minimal 50 karakter"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="panel-footer txt-primary">
                                            Panel Footer
                                        </div>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="row m-t-20">
                                <div class="col-sm-12 text-right">
                                    <asp:LinkButton ID="lbSimpanKomentar" runat="server" CssClass="btn btn-primary"
                                        OnClick="lbSimpanKomentar_Click">Simpan
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>