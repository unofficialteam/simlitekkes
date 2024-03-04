<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="jenisProsiding.ascx.cs" Inherits="simlitekkes.UserControls.Admin.jenisProsiding" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<div class="row">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Daftar Jenis Prosiding</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlJmlBarisJenisProsiding">Jml Baris:</label>
                        <asp:DropDownList ID="ddlJmlBarisJenisProsiding" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlJmlBarisJenisProsiding_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDaftarJenisProsiding" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="kd_jenis_prosiding, jenis_prosiding"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDataBound="gvDaftarJenisProsiding_RowDataBound"
                            OnRowDeleting="gvDaftarJenisProsiding_RowDeleting"
                            OnRowEditing="gvDaftarJenisProsiding_RowEditing"
                            OnRowCancelingEdit="gvDaftarJenisProsiding_RowCancelEditing"
                            OnRowUpdating="gvDaftarJenisProsiding_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarJenisProsiding" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="text-center" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kode Jenis Prosiding" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKdJenisProsiding" runat="server" Text='<%# Bind("kd_jenis_prosiding") %>'></asp:Label><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jenis Prosiding" HeaderStyle-Width="50%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJenisProsiding" runat="server" Text='<%# Bind("jenis_prosiding") %>'></asp:Label><br />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="tbJenisProsidingEdit" CssClass="form-control" Text='<%# Bind("jenis_prosiding") %>'></asp:TextBox>
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
                                        <asp:LinkButton ID="lbHapusDaftarJenisProsiding" runat="server" CommandName="Delete"
                                            CssClass="btn btn-danger mr-2" ToolTip="Hapus Jenis Prosiding">
                                        <i class="fas fa-trash"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbEditDaftarJenisProsiding" CssClass="btn btn-primary" CommandName="Edit" ToolTip="Edit Jenis Prosiding">
                                        <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                            CssClass="btn btn-success mr-2" ToolTip="Update Jenis Prosiding">
                                        <i class="fas fa-save"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbCancelUpdate" CssClass="btn btn-danger" CommandName="Cancel" ToolTip="Batal Edit Jenis Prosiding">
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
                <asp:controlPaging runat="server" ID="pagingDaftarJenisProsiding" OnPageChanging="daftarJenisProsiding_PageChanging" />
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h6>Tambah Jenis Prosiding</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Kode Jenis Prosiding</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="tbKdJenisProsidingAdd" placeholder="Kode Jenis Prosiding"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Jenis Prosiding</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="tbJenisProsidingAdd" placeholder="Nama Jenis Prosiding"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="float-right">
                    <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger mr-2" OnClick="lbBatal_Click">
                    <i class="fas fa-undo mr-2"></i>Batal
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbSimpanJenisProsiding" CssClass="btn btn-success" OnClick="lbSimpanJenisProsiding_Click">
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
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus Jenis Prosiding</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus Jenis Prosiding
                &nbsp;<asp:Label runat="server" ID="lblJenisProsiding" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusJenisProsiding" runat="server" CssClass="btn btn-danger float-rigth" OnClick="lbHapusJenisProsiding_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>