<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="buktiLuaran.ascx.cs" Inherits="simlitekkes.UserControls.Admin.buktiLuaran" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<style>
    .mycheckbox input[type="checkbox"] {
        margin-right: 5px;
    }
</style>
<div class="row">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Daftar Bukti Luaran</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlJmlBarisBuktiLuaran">Jml Baris:</label>
                        <asp:DropDownList ID="ddlJmlBarisBuktiLuaran" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlJmlBarisBuktiLuaran_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDaftarBuktiLuaran" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="id_bukti_luaran, bukti_luaran, is_luaran_th_akhir"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDataBound="gvDaftarBuktiLuaran_RowDataBound"
                            OnRowDeleting="gvDaftarBuktiLuaran_RowDeleting"
                            OnRowEditing="gvDaftarBuktiLuaran_RowEditing"
                            OnRowCancelingEdit="gvDaftarBuktiLuaran_RowCancelEditing"
                            OnRowUpdating="gvDaftarBuktiLuaran_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarBuktiLuaran" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="text-center" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bukti Luaran" HeaderStyle-Width="50%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuktiLuaran" runat="server" Text='<%# Bind("bukti_luaran") %>'></asp:Label><br />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="tbBuktiLuaranEdit" CssClass="form-control" Text='<%# Bind("bukti_luaran") %>'></asp:TextBox>
                                        </div>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status Luaran Tahun Akhir" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusLuaranAkhir" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox runat="server" ID="cbIsLuaranThnAkhirEdit" Text="Luaran Tahun Akhir" CssClass="mycheckbox"/>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="20%">
                                    <HeaderTemplate>
                                        <i class="fas fa-th"></i>
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="text-center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbHapusDaftarBuktiLuaran" runat="server" CommandName="Delete"
                                            CssClass="btn btn-danger mr-2" ToolTip="Hapus Bukti Luaran">
                                        <i class="fas fa-trash"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbEditDaftarBuktiLuaran" CssClass="btn btn-primary" CommandName="Edit" ToolTip="Edit Bukti Luaran">
                                        <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                            CssClass="btn btn-success mr-2" ToolTip="Update Bukti Luaran">
                                        <i class="fas fa-save"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbCancelUpdate" CssClass="btn btn-danger" CommandName="Cancel" ToolTip="Batal Edit Bukti Luaran">
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
                <asp:controlPaging runat="server" ID="pagingDaftarBuktiLuaran" OnPageChanging="daftarBuktiLuaran_PageChanging" />
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h6>Tambah Bukti Luaran</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Bukti Luaran</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="tbBuktiLuaranAdd" placeholder="Nama Bukti Luaran"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:CheckBox runat="server" ID="cbIsLuaranAkhirTahunAdd" CssClass="form-control mycheckbox" Text="Luaran Tahun Akhir"/>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="float-right">
                    <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger mr-2" OnClick="lbBatal_Click">
                    <i class="fas fa-undo mr-2"></i>Batal
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbSimpanBuktiLuaran" CssClass="btn btn-success" OnClick="lbSimpanBuktiLuaran_Click">
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
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus Bukti Luaran</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus Bukti Luaran
                &nbsp;<asp:Label runat="server" ID="lblBuktiLuaran" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusBuktiLuaran" runat="server" CssClass="btn btn-danger float-rigth" OnClick="lbHapusBuktiLuaran_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>