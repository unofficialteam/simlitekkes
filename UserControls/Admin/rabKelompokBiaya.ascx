<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rabKelompokBiaya.ascx.cs" Inherits="simlitekkes.UserControls.Admin.rabKelompokBiaya" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<div class="row">
    <div class="col-md-7">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Daftar RAB Kelompok Biaya</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlJmlBarisKelompokBiaya">Jml Baris:</label>
                        <asp:DropDownList ID="ddlJmlBarisKelompokBiaya" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlJmlBarisKelompokBiaya_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-8">
                    <label>Jenis Kegiatan</label>
                    <asp:DropDownList runat="server" ID="ddlJenisKegiatan" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlJenisKegiatan_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDaftarRabKelompokBiaya" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="id_rab_kelompok_biaya, kelompok_biaya"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDataBound="gvDaftarRabKelompokBiaya_RowDataBound"
                            OnRowDeleting="gvDaftarRabKelompokBiaya_RowDeleting"
                            OnRowEditing="gvDaftarRabKelompokBiaya_RowEditing"
                            OnRowCancelingEdit="gvDaftarRabKelompokBiaya_RowCancelEditing"
                            OnRowUpdating="gvDaftarRabKelompokBiaya_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarKelompokBiaya" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="text-center" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kelompok Biaya">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKelompokBiaya" runat="server" Text='<%# Bind("kelompok_biaya") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="tbKelompokBiayaEdit" CssClass="form-control" Text='<%# Bind("kelompok_biaya") %>'></asp:TextBox>
                                        </div>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <i class="fas fa-th"></i>
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="text-center"/>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbHapusDaftarRabKelompokBiaya" runat="server" CommandName="Delete"
                                            CssClass="btn btn-danger mr-2" ToolTip="Hapus RAB Kelompok Biaya">
                                        <i class="fas fa-trash"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbEditDaftarRabKelompokBiaya" CssClass="btn btn-primary" CommandName="Edit" ToolTip="Edit RAB Kelompok Biaya">
                                        <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                            CssClass="btn btn-success mr-2" ToolTip="Update RAB Kelompok Biaya">
                                        <i class="fas fa-save"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbCancelUpdate" CssClass="btn btn-danger" CommandName="Cancel" ToolTip="Batal Edit RAB Kelompok Biaya">
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
                <asp:controlPaging runat="server" ID="pagingDaftarRabKelompokBiaya" OnPageChanging="daftarRabKelompokBiaya_PageChanging" />
            </div>
        </div>
    </div>
    <div class="col-md-5">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h6>Tambah RAB Kelompok Biaya</h6>
            </div>
            <div class="card-body">
                <div class="form-group">
                    <label>Jenis Kegiatan</label>
                    <asp:DropDownList runat="server" ID="ddlJenisKegiatanAdd" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Kelompok Biaya</label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="tbKelompokBiaya" placeholder="Kelompok Biaya"></asp:TextBox>
                </div>
            </div>
            <div class="card-footer">
                <div class="float-right">
                    <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger mr-2" OnClick="lbBatal_Click">
                    <i class="fas fa-undo mr-2"></i>Batal
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbSimpanRabKelompokBiaya" CssClass="btn btn-success" OnClick="lbSimpanRabKelompokBiaya_Click">
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
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus RAB Kelompok Biaya</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus Penugasan Reviewer
                &nbsp;<asp:Label runat="server" ID="lblKelompokBiaya" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusRabKelompokBiaya" runat="server" CssClass="btn btn-danger float-rigth" OnClick="lbHapusRabKelompokBiaya_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
