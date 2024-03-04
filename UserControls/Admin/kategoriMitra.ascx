<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="kategoriMitra.ascx.cs" Inherits="simlitekkes.UserControls.Admin.kategoriMitra" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<div class="row">
    <div class="col-md-12 mb-3">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Daftar Kategori Mitra</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlJmlBarisKategoriMitra">Jml Baris:</label>
                        <asp:DropDownList ID="ddlJmlBarisKategoriMitra" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlJmlBarisKategoriMitra_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDaftarKategoriMitra" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="kd_kategori_mitra, kategori_mitra, id_skema, nama_skema"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDataBound="gvDaftarKategoriMitra_RowDataBound"
                            OnRowDeleting="gvDaftarKategoriMitra_RowDeleting"
                            OnRowEditing="gvDaftarKategoriMitra_RowEditing"
                            OnRowCancelingEdit="gvDaftarKategoriMitra_RowCancelEditing"
                            OnRowUpdating="gvDaftarKategoriMitra_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarKategoriMitra" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="text-center"/>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kategori Mitra" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKategoriMitra" runat="server" Text='<%# Bind("kategori_mitra") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="tbKategoriMitraEdit" CssClass="form-control" Text='<%# Bind("kategori_mitra") %>'></asp:TextBox>
                                        </div>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nama Skema" HeaderStyle-Width="50%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNamaSkema" runat="server" Text='<%# Bind("nama_skema") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ListBox runat="server" ID="lbxIdSkemaEdit" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="20%">
                                    <HeaderTemplate>
                                        <i class="fas fa-th"></i>
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="text-center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbHapusDaftarKategoriMitra" runat="server" CommandName="Delete"
                                            CssClass="btn btn-danger mr-2" ToolTip="Hapus Kategori Mitra">
                                        <i class="fas fa-trash"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbEditDaftarKategoriMitra" CssClass="btn btn-primary" CommandName="Edit" ToolTip="Edit Kategori Mitra">
                                        <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                            CssClass="btn btn-success mr-2" ToolTip="Update Kategori Mitra">
                                        <i class="fas fa-save"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbCancelUpdate" CssClass="btn btn-danger" CommandName="Cancel" ToolTip="Batal Edit Kategori Mitra">
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
                <asp:controlPaging runat="server" ID="pagingDaftarKategoriMitra" OnPageChanging="daftarKategoriMitra_PageChanging" />
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h6>Tambah Kategori Mitra</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Nama Skema</label>
                        <asp:ListBox runat="server" CssClass="form-control" ID="lbxNamaSkema" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Kategori Mitra</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="tbKategoriMitra" placeholder="Kategori Mitra"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="float-right">
                    <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger mr-2" OnClick="lbBatal_Click">
                    <i class="fas fa-undo mr-2"></i>Batal
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbSimpanKategoriMitra" CssClass="btn btn-success" OnClick="lbSimpanKategoriMitra_Click">
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
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus Kategori Mitra</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus Kategori Mitra
                &nbsp;<asp:Label runat="server" ID="lblKategoriMitra" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusKategoriMitra" runat="server" CssClass="btn btn-danger float-rigth" OnClick="lbHapusKategoriMitra_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
