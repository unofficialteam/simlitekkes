<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="klasterPerguruanTinggi.ascx.cs" Inherits="simlitekkes.UserControls.Admin.klasterPerguruanTinggi" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="row">
            <asp:MultiView runat="server" ID="mvDaftarReviewer" ActiveViewIndex="0">
                <asp:View ID="vListData" runat="server">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header bg-info text-white">
                                <h6>Daftar Klaster Perguruan Tinggi</h6>
                            </div>
                            <div class="card-body row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlJmlBarisData" runat="server" AutoPostBack="True"
                                            CssClass="form-control select2" OnSelectedIndexChanged="ddlJmlBarisData_SelectedIndexChanged">
                                            <asp:ListItem Text="10" Value="10" Selected="True" />
                                            <asp:ListItem Text="50" Value="50" />
                                            <asp:ListItem Text="100" Value="100" />
                                            <asp:ListItem Text="Semua" Value="0" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddlFilterInstitusi" CssClass="form-control select2" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddlFilterKlaster" CssClass="form-control select2" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddlFilterTahunKlaster" CssClass="form-control select2" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="tbPencarian" CssClass="form-control" placeholder="Masukkan Nama PT / Nama Klaster"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:LinkButton runat="server" ID="lbCariData" CssClass="btn btn-block btn-primary" OnClick="lbCariData_Click"><i class="fa fa-search mr-2"></i>Cari Data</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="clearfix">
                                        <div class="float-right">
                                            <asp:LinkButton runat="server" ID="lbTambahData" CssClass="btn btn-success" OnClick="lbTambahData_Click"><i class="fa fa-plus mr-2"></i>Tambah Data Baru</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDaftarData" runat="server" GridLines="None"
                                            CssClass="table table-striped table-hover"
                                            DataKeyNames="id_klaster_perguruan_tinggi, id_institusi, nama_institusi, thn_klaster, kd_klaster, nama_klaster, tgl_data, kd_program_hibah, program_hibah"
                                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                            OnRowDataBound="gvDaftarData_RowDataBound"
                                            OnRowDeleting="gvDaftarData_RowDeleting"
                                            OnRowCommand="gvDaftarData_RowCommand"
                                            OnRowEditing="gvDaftarData_RowEditing">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoDaftarData" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="ext-center" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nama Institusi" HeaderStyle-Width="35%">
                                                    <ItemTemplate>
                                                        <b>
                                                            <asp:Label runat="server" ID="lblNamaInstitusi" Text='<%# Bind("nama_institusi") %>'></asp:Label></b><br />
                                                        <b>Klaster. </b>
                                                        <asp:Label runat="server" ID="lblNamaKlaster" Text='<%# Bind("nama_klaster") %>'></asp:Label>
                                                        <hr />
                                                        <b>Tahun : </b>
                                                        <asp:Label runat="server" ID="lblTahunKlaster" Text='<%# Bind("thn_klaster") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Program Hibah" HeaderStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <b>
                                                            <asp:Label runat="server" ID="lblProgramHibah" Text='<%# Bind("program_hibah") %>'></asp:Label></b><br />
                                                        <b>Jenis Kegiatan : </b>
                                                        <asp:Label runat="server" ID="lblJenisKegiatan" Text='<%# Bind("jenis_kegiatan") %>'></asp:Label><br />
                                                        <hr />
                                                        <b>Jenis Institusi Penyelenggara :</b><br />
                                                        <asp:Label runat="server" ID="lblJenisInstitusi" Text='<%# Bind("jenis_institusi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="20%">
                                                    <HeaderTemplate>
                                                        <i class="fas fa-th"></i>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="text-center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbHapusDaftarData" runat="server" CommandName="Delete"
                                                            CssClass="btn btn-danger btn-block" ToolTip="Hapus Data">
                                        <i class="fas fa-trash mr-2"></i>Hapus Data
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbEditDaftarData" CssClass="btn btn-primary btn-block" CommandName="Edit" ToolTip="Edit Jenis Luaran">
                                        <i class="fas fa-edit mr-2"></i>Edit Data
                                                        </asp:LinkButton>
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
                            <div class="card-footer">
                                <asp:controlPaging runat="server" ID="pagingDaftarData" OnPageChanging="daftarData_PageChanging" />
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vInsupData" runat="server">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header bg-info text-white">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h6>Tambah/Ubah Data Klaster Perguruan Tinggi</h6>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Klaster</label>
                                            <asp:DropDownList runat="server" ID="ddlAddKlaster" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Program Hibah</label>
                                            <asp:DropDownList runat="server" ID="ddlAddProgramHibah" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Nama Institusi</label>
                                            <asp:DropDownList runat="server" ID="ddlAddIdInstitusi" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Tanggal Data</label>
                                            <asp:TextBox runat="server" ID="tbAddTanggalData" TextMode="Date" CssClass="form-control" placeholder="Masukkan Tanggal Data"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Tahun Klaster</label>
                                            <asp:TextBox runat="server" ID="tbAddTahunKlaster" TextMode="Number" MaxLength="4" MinLength="4" CssClass="form-control" placeholder="Masukkan Tahun Klaster"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="clearfix">
                                            <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-info float-right ml-2" OnClick="lbBatal_Click"><i class="fas fa-arrow-left mr-2"></i>Batal</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbAddData" CssClass="btn btn-primary float-right" OnClick="lbSimpanData_Click"><i class="fas fa-save mr-2"></i>Simpan</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
        <div class="modal modal-primary" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus Data</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Apakah yakin akan menghapus Data
                &nbsp;<asp:Label runat="server" ID="lblData" Text=""></asp:Label>&nbsp;?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                        <asp:LinkButton ID="lbHapusData" runat="server" CssClass="btn btn-danger float-rigth" OnClick="lbHapusData_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
