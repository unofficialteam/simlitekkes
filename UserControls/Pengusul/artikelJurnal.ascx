<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="artikelJurnal.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.artikelJurnal" %>

<asp:MultiView ID="mvArtikelJurnal" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftar" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="fs-17 font-weight-600 mb-0">ARTIKEL JURNAL</h6>
                    </div>
                    <div class="col-sm-6" style="text-align: right">
                        <asp:LinkButton ID="lbDataBaru" runat="server" class="btn btn-success waves-effect waves-light f-right"
                            OnClick="lbDataBaru_Click"><i class="fa fa-plus"></i>&nbsp;&nbsp;Data Baru
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="card-block">
                <div class="view-info">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="general-info">
                                <div class="row">
                                    <div class="col-sm-12 table-responsive">
                                        <asp:ListView ID="lvArtikelJurnal" runat="server"
                                            DataKeyNames="id_publikasi_jurnal,judul,kd_sts_berkas_jurnal,
                                            kd_peran_penulis,kd_sumber_data,url"
                                            OnItemEditing="lvArtikelJurnal_ItemEditing"
                                            OnItemDeleting="lvArtikelJurnal_ItemDeleting"
                                            OnItemCommand="lvArtikelJurnal_ItemCommand" OnItemDataBound="lvArtikelJurnal_ItemDataBound">
                                            <LayoutTemplate>
                                                <table class="table table-hover">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 30px; text-align: center; padding: 0;"></td>
                                                            <td style="width: 60px; text-align: center; padding: 0;"></td>
                                                            <td style="padding: 0;"></td>
                                                            <td style="width: 40px; text-align: center; padding: 0;"></td>
                                                        </tr>
                                                        <tr id="itemPlaceHolder" runat="server"></tr>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.DataItemIndex + 1 %></td>
                                                    <td>
                                                        <asp:LinkButton ID="lbUnduh" runat="server" Font-Size="50px"
                                                            ForeColor="Red"
                                                            CssClass="far fa-file-pdf" CommandName="UnduhBerkas"
                                                            CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Unduh Berkas">
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <span style="color: forestgreen"><b><%# Eval("judul") %></b></span><br />
                                                        <b>Jurnal: </b><%# Eval("nama_jurnal") %><br />
                                                        <b>Tahun: </b><%# Eval("thn_publikasi") %> |
                                                        <b>Volume: </b><%# Eval("volume") %> |
                                                        <b>ISSN: </b><%# Eval("issn") %><br />
                                                        <b>URL: </b><a href="<%# ShortenedURL(Eval("url").ToString()) %>" target="_blank"><%# Eval("url") %></a>
                                                        <asp:Panel ID="panelAksi" runat="server">
                                                            <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete"
                                                                ToolTip="Hapus Data" ForeColor="DarkRed"
                                                                Visible='<%# (Eval("kd_sumber_data").ToString() == "1") ? false : true %>'
                                                                CssClass="fa fa-trash"><i> hapus</i>
                                                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit" ForeColor="DarkOrange"
                                                                ToolTip="Edit Data" CssClass="fa fa-edit"><i> ubah</i></asp:LinkButton>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <td colspan="4">
                                                        <div class="p-4">
                                                            <div class="alert alert-info" role="alert">
                                                                Belum ada data Artikel Jurnal...
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                                <!-- end of row -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vData" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="fs-17 font-weight-600 mb-0">DATA ARTIKEL JURNAL</h6>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="view-info">
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Judul</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbJudul" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Nama Jurnal</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbNamaJurnal" runat="server" CssClass="form-control" placeholder="Nama Jurnal"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Jenis Publikasi</label>
                        <div class="col-sm-6 col-xs-12">
                            <asp:DropDownList ID="ddlJenisPublikasi" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Peran Penulis</label>
                        <div class="col-sm-6 col-xs-12">
                            <asp:DropDownList ID="ddlPeranPenulis" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Tahun Publikasi</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbTahunPublikasi" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Volume</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbVolume" runat="server" CssClass="form-control" placeholder="Volume"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Nomor</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbNomor" runat="server" CssClass="form-control" placeholder="Nomor"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">URL Artikel</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbURL" runat="server" CssClass="form-control" placeholder="Alamat URL lengkap artikel jurnal"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">ISSN</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbISSN" runat="server" CssClass="form-control" placeholder="ISSN"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 control-label">Unggah Artikel</label>
                        <div class="col-sm-10">
                            <asp:LinkButton ID="lbUnggahDokumen" runat="server" CssClass="btn btn-success"
                                OnClick="lbUnggahDokumen_Click">
                                        Unggah
                            </asp:LinkButton>
                            <small class="text-danger">Ukuran berkas maksimal 1 MB</small>
                        </div>
                    </div>
                    <div class="clearfix" style="float: right">
                        <asp:LinkButton ID="btnSimpan" runat="server" Text="Simpan" class="btn btn-primary mr-2" OnClick="btnSimpan_Click"></asp:LinkButton>
                        <asp:LinkButton ID="btnBatal" runat="server" Text="Batal" class="btn btn-danger" OnClick="btnBatal_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vDataKinerja" runat="server">
        <div class="card">
            <div class="card-header">
                <h5 class="card-header-text">Data Artikel Jurnal</h5>
            </div>
            <div class="card-block">
                <div class="view-info">
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Judul</label>
                        <div class="col-sm-10">
                            <asp:Label ID="lblJudul" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Nama Jurnal</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:Label ID="lblNamaJurnal" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Jenis Publikasi</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:Label ID="lblJenisPublikasi" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Peran Penulis</label>
                        <div class="col-sm-6 col-xs-12">
                            <asp:DropDownList ID="ddlPeranPenulisKinerja" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Tahun Publikasi</label>
                        <div class="col-sm-2">
                            <asp:Label ID="lblThnPublikasi" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Volume</label>
                        <div class="col-sm-2">
                            <asp:Label ID="lblVolume" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Nomor</label>
                        <div class="col-sm-4">
                            <asp:Label ID="lblNomor" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">URL</label>
                        <div class="col-sm-10 col-xs-12">
                            <%--<asp:label ID="lblUrl" runat="server" text=""></asp:label>--%>
                            <asp:TextBox ID="tbUrlKinerja" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">ISSN</label>
                        <div class="col-sm-10">
                            <asp:Label ID="lblIssn" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row m-t-30">
            <div class="col-sm-12 text-center">
                <asp:Button ID="btnSimpanKinerja" runat="server" CssClass="btn btn-primary waves-effect waves-light"
                    Text="Simpan" OnClick="btnSimpanKinerja_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnBatalKinerja" runat="server" CssClass="btn btn-default waves-effect waves-light"
                    Text="Batal" OnClick="btnBatalKinerja_Click"></asp:Button>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

<div class="modal modal-success fade" id="modalKonfirmasiHapus" tabindex="-1" role="dialog"
    aria-labelledby="modalTitle" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalTitle">Konfirmasi</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>Apakah Anda yakin akan menghapus data ini ?</p>
                <p class="text-primary">
                    <asp:Label ID="lblJudulDihapus" runat="server" Font-Bold="true"></asp:Label>
                </p>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnKonfirmasiHapus" runat="server" CssClass="btn btn-primary"
                    Text="Hapus" OnClick="btnKonfirmasiHapus_Click" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="modalUnggahBerkas" tabindex="-1" role="dialog"
    aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" 
    style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h5>Unggah Artikel Jurnal</h5>
                            </div>
                            <div class="card border border-info">
                                <div class="card-body">
                                    <div>
                                        <iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" style="border: none; width: 100%; height: 150px;"></iframe>
                                    </div>
                                    <%-- <div class="form-control">--%>
                                    <div style="font-size: 14px; padding: 10px;">
                                        Ekstensi: .pdf .PDF
                                        <br />
                                        <%--</div>
                                    <div style="font-size: 14px; padding: 10px;">--%>
                                        Maksimal: 1 MB<br>
                                    </div>
                                    <%--</div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="lbSelesaiUnggah" runat="server" CssClass="btn btn-info"
                            OnClick="lbSelesaiUnggah_Click">
                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Selesai
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
