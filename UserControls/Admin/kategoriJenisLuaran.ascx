<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="kategoriJenisLuaran.ascx.cs" Inherits="simlitekkes.UserControls.Admin.kategoriJenisLuaran" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<div class="row">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Daftar Kategori Jenis Luaran</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlJmlBarisKategoriJenisLuaran">Jml Baris:</label>
                        <asp:DropDownList ID="ddlJmlBarisKategoriJenisLuaran" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlJmlBarisKategoriJenisLuaran_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlStatusData">Status Data</label>
                        <asp:DropDownList ID="ddlStatusData" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlStatusData_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDaftarKategoriJenisLuaran" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="kd_kategori_jenis_luaran, nama_kategori_jenis_luaran, kd_sts_aktif"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDataBound="gvDaftarKategoriJenisLuaran_RowDataBound"
                            OnRowDeleting="gvDaftarKategoriJenisLuaran_RowDeleting"
                            OnRowEditing="gvDaftarKategoriJenisLuaran_RowEditing"
                            OnRowCancelingEdit="gvDaftarKategoriJenisLuaran_RowCancelEditing"
                            OnRowUpdating="gvDaftarKategoriJenisLuaran_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarKategoriJenisLuaran" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="text-center" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nama Kategori Jenis Luaran" HeaderStyle-Width="50%">
                                    <ItemTemplate>
                                        Kode :
                                        <asp:Label ID="lblKdKategoriJenisLuaran" runat="server" Text='<%# Bind("kd_kategori_jenis_luaran") %>'></asp:Label><br />
                                        Nama Jenis Luaran :
                                        <br />
                                        <asp:Label ID="lblKategoriJenisLuaran" runat="server" Text='<%# Bind("nama_kategori_jenis_luaran") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="tbKategoriJenisLuaranEdit" CssClass="form-control" Text='<%# Bind("nama_kategori_jenis_luaran") %>'></asp:TextBox>
                                        </div>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status Data" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusData" runat="server" Text='<%# Bind("status_aktif") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlStatusDataEdit" CssClass="form-control"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="20%">
                                    <HeaderTemplate>
                                        <i class="fas fa-th"></i>
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="text-center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbHapusDaftarKategoriJenisLuaran" runat="server" CommandName="Delete"
                                            CssClass="btn btn-danger mr-2" ToolTip="Hapus Kategori Jenis Luaran">
                                        <i class="fas fa-trash"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbEditDaftarKategoriJenisLuaran" CssClass="btn btn-primary" CommandName="Edit" ToolTip="Edit Kategori Jenis Luaran">
                                        <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                            CssClass="btn btn-success mr-2" ToolTip="Update Kategori Jenis Luaran">
                                        <i class="fas fa-save"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbCancelUpdate" CssClass="btn btn-danger" CommandName="Cancel" ToolTip="Batal Edit Kategori Jenis Luaran">
                                        <i class="fas fa-undo"></i>
                                        </asp:LinkButton>
                                    </EditItemTemplate>
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
                <asp:controlPaging runat="server" ID="pagingDaftarKategoriJenisLuaran" OnPageChanging="daftarKategoriJenisLuaran_PageChanging" />
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h6>Tambah Kategori Jenis Luaran</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Kode Kategori Jenis Luaran</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="tbKdKategoriJenisLuaranAdd" placeholder="Kode Kategori Jenis Luaran"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Kategori Jenis Luaran</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="tbKategoriJenisLuaran" placeholder="Kategori Jenis Luaran"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="float-right">
                    <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger mr-2" OnClick="lbBatal_Click">
                    <i class="fas fa-undo mr-2"></i>Batal
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbSimpanKategoriJenisLuaran" CssClass="btn btn-success" OnClick="lbSimpanKategoriJenisLuaran_Click">
                    <i class="fas fa-save mr-2"></i>Simpan
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="modal modal-primary" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus Kategori Jenis Luaran</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus Kategori Jenis Luaran
                &nbsp;<asp:Label runat="server" ID="lblKategoriJenisLuaran" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusKategoriJenisLuaran" runat="server" CssClass="btn btn-danger float-rigth" OnClick="lbHapusKategoriJenisLuaran_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
