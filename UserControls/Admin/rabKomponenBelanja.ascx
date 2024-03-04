<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rabKomponenBelanja.ascx.cs" Inherits="simlitekkes.UserControls.Admin.rabKomponenBelanja" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<div class="row">
    <div class="col-md-12 mb-3">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Daftar RAB Komponen Belanja</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="ddlJmlBarisKomponenBelanja">Jml Baris:</label>
                        <asp:DropDownList ID="ddlJmlBarisKomponenBelanja" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlJmlBarisKomponenBelanja_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>Jenis Kegiatan</label>
                    <asp:DropDownList runat="server" ID="ddlJenisKegiatan" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlJenisKegiatan_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label>RAB Kelompok Biaya</label>
                    <asp:DropDownList runat="server" ID="ddlRabKelompokBiaya" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlRabKelompokBiaya_SelectedIndexChanger"></asp:DropDownList>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDaftarRabKomponenBelanja" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="id_rab_komponen_belanja, komponen_belanja, id_kategori_penelitian, kategori_penelitian"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDataBound="gvDaftarRabKomponenBelanja_RowDataBound"
                            OnRowDeleting="gvDaftarRabKomponenBelanja_RowDeleting"
                            OnRowEditing="gvDaftarRabKomponenBelanja_RowEditing"
                            OnRowCancelingEdit="gvDaftarRabKomponenBelanja_RowCancelEditing"
                            OnRowUpdating="gvDaftarRabKomponenBelanja_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarKomponenBelanja" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Identitas Komponen Belanja">
                                    <ItemTemplate>
                                        Komponen Belanja : <asp:Label ID="lblKomponenBelanja" runat="server" Text='<%# Bind("komponen_belanja") %>'></asp:Label><br />
                                        Satuan : <asp:Label runat="server" ID="lblSatuan" Text='<%# Bind("satuan") %>'></asp:Label><br />
                                        Keterangan : <asp:Label runat="server" ID="lblKeterangan" Text='<%# Bind("keterangan") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="form-group">
                                            <label>Komponen Belanja</label>
                                            <asp:TextBox runat="server" ID="tbKomponenBelanjaEdit" CssClass="form-control" Text='<%# Bind("kelompok_biaya") %>'></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Satuan</label>
                                            <asp:TextBox runat="server" ID="tbSatuanEdit" CssClass="form-control" Text='<%# Bind("satuan") %>'></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Keterangan</label>
                                            <asp:TextBox runat="server" ID="tbKeteranganEdit" CssClass="form-control" Text='<%# Bind("keterangan") %>'></asp:TextBox>
                                        </div>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kategori Penelitian">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKategoriPenelitian" runat="server" Text='<%# Bind("kategori_penelitian") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ListBox runat="server" ID="lbxKategoriPenelitianEdit" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <i class="fas fa-th"></i>
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="text-center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbHapusDaftarRabKomponenBelanja" runat="server" CommandName="Delete"
                                            CssClass="btn btn-danger mr-2" ToolTip="Hapus RAB Komponen Belanja">
                                        <i class="fas fa-trash"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbEditDaftarRabKomponenBelanja" CssClass="btn btn-primary" CommandName="Edit" ToolTip="Edit RAB Komponen Belanja">
                                        <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                            CssClass="btn btn-success mr-2" ToolTip="Update RAB Komponen Belanja">
                                        <i class="fas fa-save"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbCancelUpdate" CssClass="btn btn-danger" CommandName="Cancel" ToolTip="Batal Edit RAB Komponen Belanja">
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
                <asp:controlPaging runat="server" ID="pagingDaftarRabKomponenBelanja" OnPageChanging="daftarRabKomponenBelanja_PageChanging" />
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h6>Tambah RAB Komponen Belanja</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Jenis Kegiatan</label>
                        <asp:DropDownList runat="server" ID="ddlJenisKegiatanAdd" CssClass="form-control" OnSelectedIndexChanged="ddlJenisKegiatanAdd_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>RAB Kelompok Biaya</label>
                        <asp:DropDownList runat="server" ID="ddlRabKelompokBiayaAdd" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Komponen Belanja</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="tbKomponenBelanja" placeholder="Komponen Belanja"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Satuan</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="tbSatuan" placeholder="Satuan" MaxLength="25"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Keterangan</label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="tbKeterangan" placeholder="Keterangan"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Kategori Penelitian</label>
                        <asp:ListBox runat="server" CssClass="form-control" ID="lbxKategoriPenelitian" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="float-right">
                    <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger mr-2" OnClick="lbBatal_Click">
                    <i class="fas fa-undo mr-2"></i>Batal
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbSimpanRabKomponenBelanja" CssClass="btn btn-success" OnClick="lbSimpanRabKomponenBelanja_Click">
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
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus RAB Komponen Belanja</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus Penugasan Reviewer
                &nbsp;<asp:Label runat="server" ID="lblKomponenBelanja" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusRabKomponenBelanja" runat="server" CssClass="btn btn-danger float-rigth" OnClick="lbHapusRabKomponenBelanja_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
