<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="peranPenulis.ascx.cs" Inherits="simlitekkes.UserControls.Admin.peranPenulis" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<div class="row">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Daftar Peran Penulis</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlJmlBarisPeranPenulis">Jml Baris:</label>
                        <asp:DropDownList ID="ddlJmlBarisPeranPenulis" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlJmlBarisPeranPenulis_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDaftarPeranPenulis" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="kd_peran_penulis, peran_penulis, kd_sts_aktif"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDataBound="gvDaftarPeranPenulis_RowDataBound"
                            OnRowDeleting="gvDaftarPeranPenulis_RowDeleting"
                            OnRowEditing="gvDaftarPeranPenulis_RowEditing"
                            OnRowCancelingEdit="gvDaftarPeranPenulis_RowCancelEditing"
                            OnRowUpdating="gvDaftarPeranPenulis_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarPeranPenulis" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="text-center" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kode Peran Penulis" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKdPeranPenulis" runat="server" Text='<%# Bind("kd_peran_penulis") %>'></asp:Label><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Peran Penulis" HeaderStyle-Width="50%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeranPenulis" runat="server" Text='<%# Bind("peran_penulis") %>'></asp:Label><br />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="tbPeranPenulisEdit" CssClass="form-control" Text='<%# Bind("peran_penulis") %>'></asp:TextBox>
                                        </div>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="20%">
                                    <HeaderTemplate>
                                        <i class="fas fa-th"></i>
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="text-center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbHapusDaftarPeranPenulis" runat="server" CommandName="Delete"
                                            CssClass="btn btn-danger mr-2" ToolTip="Hapus Peran Penulis">
                                        <i class="fas fa-trash"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbEditDaftarPeranPenulis" CssClass="btn btn-primary" CommandName="Edit" ToolTip="Edit Peran Penulis">
                                        <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                            CssClass="btn btn-success mr-2" ToolTip="Update Peran Penulis">
                                        <i class="fas fa-save"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbCancelUpdate" CssClass="btn btn-danger" CommandName="Cancel" ToolTip="Batal Edit Peran Penulis">
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
                <asp:controlPaging runat="server" ID="pagingDaftarPeranPenulis" OnPageChanging="daftarPeranPenulis_PageChanging" />
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h6>Tambah Peran Penulis</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Kode Peran Penulis</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="tbKdPeranPenulisAdd" placeholder="Kode Peran Penulis"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Peran Penulis</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="tbPeranPenulisAdd" placeholder="Nama Peran Penulis"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="float-right">
                    <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger mr-2" OnClick="lbBatal_Click">
                    <i class="fas fa-undo mr-2"></i>Batal
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbSimpanPeranPenulis" CssClass="btn btn-success" OnClick="lbSimpanPeranPenulis_Click">
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
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus Peran Penulis</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus Peran Penulis
                &nbsp;<asp:Label runat="server" ID="lblPeranPenulis" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusPeranPenulis" runat="server" CssClass="btn btn-danger float-rigth" OnClick="lbHapusPeranPenulis_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>