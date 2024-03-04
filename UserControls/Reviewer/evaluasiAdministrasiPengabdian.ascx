<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="evaluasiAdministrasiPengabdian.ascx.cs" Inherits="simlitekkes.UserControls.Reviewer.evaluasiAdministrasiPengabdian" %>
<asp:ScriptManagerProxy ID="smpBeranda" runat="server"></asp:ScriptManagerProxy>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkapAbdimas.ascx" TagName="pdfUsulanLengkap" TagPrefix="uc" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="ktPaging" TagPrefix="asp" %>

<asp:UpdatePanel ID="upPersyaratanUmum" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
            <asp:View ID="vDaftarMonev" runat="server">
                <div class="content-header row align-items-center mb-4">
                    <nav aria-label="breadcrumb" class="col-sm-4 order-sm-last mb-3 mb-sm-0 p-0 ">
                    </nav>
                    <div class="col-sm-8 header-title p-0">
                        <div class="media">
                            <div class="header-icon text-success mr-3"><i class="hvr-buzz-out fas fa-tasks"></i></div>
                            <div class="media-body">
                                <h1 class="font-weight-bold">Evaluasi Administrasi</h1>
                                <small>Summary Usulan yang menjadi penilaian</small>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header">
                                <div class="d-flex justify-content-between align-items-center">
                                    <div class="form-inline">
                                        <label for="ddlThnUsulan">Tahun Usulan </label>
                                        &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="true"
                                            CssClass="form-control" ClientIDMode="Static"
                                            OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                                        </asp:DropDownList>&nbsp;&nbsp;
                                        <label for="ddlThnPelaksanaan">Tahun Pelaksanaan </label>
                                        &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlThnPelaksanaan" AutoPostBack="true" runat="server"
                                            Enabled="true" ClientIDMode="Static" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                            <asp:ListItem>2018</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-inline text-right">
                                        <label for="ddlInstitusiYgMenugasi">Institusi Penugasan</label>
                                        &nbsp;&nbsp;                                       
                                            <asp:DropDownList runat="server" ID="ddlInstitusiYgMenugasi" Enabled="true" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlInstitusiYgMenugasi_SelectedIndexChanged">
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <asp:GridView runat="server" ID="gvEvaluasiResume" CssClass="table table-striped table-hover"
                                    GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                    ShowFooter="True" DataKeyNames="id_skema" OnRowUpdating="gvEvaluasiResume_RowUpdating"
                                    OnRowDataBound="gvEvaluasiResume_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nama Skema">
                                            <ItemTemplate>
                                                <%# Eval("nama_skema") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Jumlah Proposal">
                                            <ItemTemplate>
                                                <%# Eval("jml_usulan") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>

                                        <asp:TemplateField HeaderText="Jml Sdh. Dinilai">
                                            <ItemTemplate>
                                                <%# Eval("jml_dievaluasi") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblStsPermanen" CssClass="badge p-2 badge-success" Text='<%# Eval("kd_sts_permanen") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="lbDaftarUsulan" runat="server" CommandName="Update" Text="Daftar Usulan"
                                                    CssClass="btn btn-primary btn-sm" ToolTip="Daftar Usulan"></asp:Button>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;" class="text-center">
                                            <strong class="text-center">TIDAK ADA DATA</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="vDaftarUsulan" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card  mb-4">
                            <div class="card-header">
                                <div class="d-flex justify-content-between align-items-center">
                                    <div class="form-inline">
                                        <asp:Label ID="lblSkema" runat="server" Font-Bold="true" Font-Size="18px"></asp:Label>
                                    </div>
                                    <div class="form-inline text-right">
                                        <asp:LinkButton ID="LinkButton2" runat="server" class="btn btn-outline-primary"  OnClick="LinkButton1_Click"><i class="fa fa-arrow-circle-left" aria-hidden="true"></i>&nbsp;&nbsp;Kembali</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-between align-items-center mb-3">
                                    <div class="form-inline">
                                        <asp:Label ID="lbTahunUsul" runat="server" Font-Size="Smaller"></asp:Label>
                                    </div>
                                    <div class="form-inline text-right">
                                    </div>
                                </div>
                                <div class="d-flex justify-content-between align-items-center mb-2">
                                    <div class="form-inline">
                                        <asp:LinkButton ID="lbSimpanPermanen" runat="server" CssClass="btn btn-outline-success"
                                            OnClick="lbSimpanPermanen_Click">
                                                <i class="fa fa-lock" aria-hidden="true"></i>&nbsp;&nbsp;Simpan Permanen</asp:LinkButton>
                                                                           
                                        &nbsp;&nbsp;<label for="ddlJmlBaris">Baris</label>
                                        &nbsp;
                                    <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="true" CssClass="form-control"
                                        ClientIDMode="Static" Enabled="true" OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged" Width="100px">
                                        <asp:ListItem Selected="True" Text="10" Value="10" />
                                        <asp:ListItem Text="50" Value="50" />
                                        <asp:ListItem Text="100" Value="100" />
                                        <asp:ListItem Text="200" Value="200" />
                                        <asp:ListItem Text="Semua" Value="0" />
                                    </asp:DropDownList>
                                    </div>
                                    <div class="form-inline text-right">
                                        <asp:Panel ID="panelStatusOpen" runat="server">
                                            <span class="label label-success" style="font-size: 14px; padding: 5px;">
                                                <i class="fa fa-unlock-alt" aria-hidden="true"></i>&nbsp;&nbsp;TERBUKA
                                            </span>
                                        </asp:Panel>
                                        <asp:Panel ID="panelStatusPermanent" runat="server" Visible="false" HorizontalAlign="Right">
                                            <span class="label label-danger" style="font-size: 14px; padding: 5px;">
                                                <i class="fa fa-lock" aria-hidden="true"></i>&nbsp;&nbsp;PERMANEN
                                            </span>
                                        </asp:Panel>
                                        <asp:Label ID="lblTahapan" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-between align-items-center mb-4">
                                    <div class="form-inline">
                                    </div>
                                    <div class="form-inline text-right">
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <asp:ListView ID="lvDaftarUsulanKonfirmasi" runat="server"
                                    DataKeyNames="id_transaksi_kegiatan, id_usulan, id_usulan_kegiatan,id_personal,judul,                     
                                                    nama_skema, thn_usulan_kegiatan, thn_pelaksanaan_kegiatan, nama_ketua,
                                                    id_plotting_reviewer, nidn"
                                    OnItemCommand="lvDaftarUsulanKonfirmasi_ItemCommand" OnItemDataBound="lvDaftarUsulanKonfirmasi_ItemDataBound" OnItemUpdating="lvDaftarUsulanKonfirmasi_ItemUpdating">
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
                                                <asp:LinkButton ID="lbUnduh" runat="server" Font-Size="50px"
                                                    ForeColor="Red"
                                                    CssClass="fas fa-file-pdf btn btn-default" CommandName="UnduhPdf"
                                                    CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Unduh Berkas">
                                                </asp:LinkButton>
                                            </td>
                                            <td>
                                                <h7><b style="color: maroon;"><%# Eval("judul") %></b></h7>
                                                <br />
                                                <h8 style="color: darkgreen;"><%# Eval("program_hibah") %> - <%# Eval("nama_skema") %></h8>
                                                &nbsp;
                                        <span style="background-color: yellow;">&nbsp;
                                            <asp:Label ID="lblNilai" runat="server" Text="Label" Font-Bold="true" BackColor="Yellow"></asp:Label>
                                        </span>&nbsp;
                                        <br />
                                                Ketua: <span style="font-weight: bold;"><%# Eval("nama_ketua") %></span>
                                                - (Thn. Usulan: <%# Eval("thn_usulan_kegiatan") %> Thn. Pelaksanaan: <%# Eval("thn_pelaksanaan_kegiatan") %>)
                                        <br />
                                                Bidang fokus: <%# Eval("bidang_fokus") %> - Jumlah Anggota: <%# Eval("jml_anggota") %><br />
                                                Lama Kegiatan: <%# Eval("lama_kegiatan") %> tahun<br />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbDisetujui" runat="server" CssClass="btn btn-success fa fa-check waves-effect waves-light"
                                                    CommandName="Nilai" CommandArgument="<%# Container.DataItemIndex %>">&nbsp;Penilaian
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
                                <asp:ktPaging ID="ktPagging" runat="server" OnPageChanging="Paging_PageChanging" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="vMonev" runat="server">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="box box-primary">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h4 class="box-title">Evaluasi Dokumen (Administrasi)</h4>
                                <div class="row mb-4">
                                    <div class="col-sm-12">
                                        <asp:Label ID="lblJudul" runat="server" ForeColor="Maroon"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:Label ID="lbskemanilai" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-inline" style="text-align: left">
                                    <asp:LinkButton ID="lbCekRiwayatPendidikan" runat="server" Visible="false" CssClass="btn btn-success waves-effect waves-light" OnClick="lbCekRiwayatPendidikan_Click">Cek Riwayat Pendidikan</asp:LinkButton>
                                </div>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <asp:GridView runat="server" ID="gvPenilaian" CssClass="table table-striped table-hover"
                                    GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    DataKeyNames="id_komponen_penilaian, skor" OnRowDataBound="gvPenilaian_RowDataBound"
                                    OnPreRender="gvPenilaian_PreRender" OnRowCreated="gvPenilaian_RowCreated">
                                    <Columns>
                                        <asp:BoundField HeaderText="No." DataField="no_urut">
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Kriteria Penilaian">
                                            <ItemTemplate>
                                                <%# Eval("kriteria_penilaian").ToString().Replace("\n", "<br />") %>
                                                <br />
                                                <asp:RadioButtonList ID="rblNilai" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">&nbsp Ya&nbsp&nbsp</asp:ListItem>
                                                    <asp:ListItem Value="0">&nbsp Tidak</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;">
                                            <strong>DATA TIDAK DITEMUKAN</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12">
                        <div class="box box-primary">
                            <div class="box-body form-horizontal">
                                <div class="form-group">
                                    <label for="tbKomentar" class="col-sm-2 control-label">Komentar</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="tbKomentar" runat="server" TextMode="MultiLine" class="form-control" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-sm-4">
                        <asp:LinkButton ID="lbKembaliKeUsulan" runat="server" CssClass="btn btn-primary"
                            ToolTip="Kembali Ke Daftar Usulan" OnClick="lbKembaliKeUsulan_Click">
                                        <i class="fa fa-arrow-circle-left" aria-hidden="true"></i>&nbsp;&nbsp;Kembali
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-success"
                            OnClick="lbSimpan_Click">
                                <i class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;&nbsp;Simpan
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="modal modal-primary" id="modalRiwayatPendidikan" role="dialog" aria-labelledby="myModalRiwayatPendidikan">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="myModalRiwayatPendidikan">Riwayat Pendidikan</h4>
                            </div>
                            <div class="modal-body">
                                <div class="col-sm-12 table-responsive">
                                    <asp:GridView ID="gvRiwayatPendidikan" runat="server" GridLines="None"
                                        CssClass="table table-striped table-hover"
                                        ItemType="simlitekkes.Models.PDDIKTI.riwayatPendidikan"
                                        ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoRiwayat" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Perguruan Tinggi">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPtRiwayat" runat="server" Text='<%# Item.pt %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gelar Akademik">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGelarRiwayat" runat="server" Text='<%# Item.gelar.singkatan %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tahun Lulus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblThnLulusRiwayat" runat="server" Text='<%# Item.thn_lulus %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jenjang">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJenjangRiwayat" runat="server" Text='<%# Item.jenjang_didik.nama %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="min-height: 100px; margin: 0 auto;">
                                                <strong>DATA TIDAK DITEMUKAN</strong>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
        <div>
            <uc:pdfUsulanLengkap runat="server" ID="pdfUsulanLengkap" Visible="false" />
        </div>
        <div class="modal modal-danger" id="modalKonfirmasiPermanen" role="dialog" aria-labelledby="myModalKonfirmasiPermanen">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalKonfirmasiPermanen">Konfirmasi Simpan Permanen</h4>
                    </div>
                    <div class="modal-body">
                        Apakah Saudara yakin akan memproses simpan permanen hasil penilaian? 
                            karena jika sudah disimpan permanen, <b>penilaian TIDAK bisa diedit</b>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                        <asp:LinkButton ID="lbKonfirmasiPermanen" runat="server" CssClass="btn btn-info pull-right" OnClick="lbKonfirmasiPermanen_Click" OnClientClick="$('#modalKonfirmasiPermanen').modal('hide');">Ya</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
