<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="operatorPT.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.dataPendukungPT" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<div class="row">
    <div class="col-md-12">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Daftar Data Pendukung PT</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-1">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlJmlBarisData" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" ClientIDMode="Static" OnSelectedIndexChanged="ddlJmlBarisData_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:DropDownList runat="server" ID="ddlKdJenisKegiatan" CssClass="form-control select2" OnSelectedIndexChanged="ddlKdJenisKegiatan_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="1">Penelitian</asp:ListItem>
                            <asp:ListItem Value="2">Pengabdian</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="tbPencarian" CssClass="form-control" placeholder="Masukkan Nama Perguruan Tinggi"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:LinkButton runat="server" ID="lbCariData" CssClass="btn btn-block btn-primary" OnClick="lbCariData_Click"><i class="fa fa-search mr-2"></i>Cari Data</asp:LinkButton>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDaftarData" runat="server" GridLines="None"
                            CssClass="table table-striped table-hover"
                            DataKeyNames="id_institusi, kd_institusi, nama_institusi, alamat, jml_pengguna, telepon, fax, surel, jml_pengiriman, waktu_pengiriman_terakhir, array_username, array_password, id_kontak_pic_pengguna_institusi"
                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                            OnRowDataBound="gvDaftarData_RowDataBound"
                            OnRowEditing="gvDaftarData_RowEditing"
                            OnRowCancelingEdit="gvDaftarData_RowCancelEditing"
                            OnRowUpdating="gvDaftarData_RowUpdating"
                            OnRowCommand="gvDaftarData_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDaftarData" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="ext-center" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Identitas Institusi" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label runat="server" ID="lblNamaInstitusi" Text='<%# Bind("nama_institusi") %>'></asp:Label></b><br />
                                        <b>Alamat : </b><br />
                                        <asp:Label runat="server" ID="lblAlamat" Text='<%# Bind("alamat") %>'></asp:Label><br />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jumlah Akun" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblJumlahAkun" Text='<%# Bind("jml_pengguna") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kontak Lembaga" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <b>Nomor Telepon : </b><br />
                                        <small><asp:Label runat="server" ID="lblNomorTelepon" Text='<%# Bind("telepon") %>'></asp:Label></small><br />
                                        <b>Fax : </b><br />                                        
                                        <small><asp:Label runat="server" ID="lblFax" Text='<%# Bind("fax") %>'></asp:Label></small><br />
                                        <b>Surel/Email : </b><br />
                                        <small><asp:Label runat="server" ID="lblSurel" Text='<%# Bind("surel") %>'></asp:Label></small>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="tbEditTelepon" placeholder="Masukkan Nomor Telepon Lembaga Penelitian" CssClass="form-control mb-2" Text='<%# Bind("telepon") %>'></asp:TextBox>
                                        <asp:TextBox runat="server" ID="tbEditFax" placeholder="Masukkan Fax Lembaga Penelitian" CssClass="form-control mb-2" Text='<%# Bind("fax") %>'></asp:TextBox>
                                        <asp:TextBox runat="server" ID="tbEditEmail" placeholder="Masukkan Surel/Email Lembaga Penelitian" CssClass="form-control" Text='<%# Bind("surel") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Detail Pengiriman" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <b>Jumlah Pengiriman : </b><br />
                                        <small><asp:Label runat="server" ID="lblJumlahPengiriman" Text='<%# Bind("jml_pengiriman") %>'></asp:Label></small><br />
                                        <b>Terakhir Dikirim : </b><br />
                                        <small><asp:Label runat="server" ID="lblTerakhirDikirim" Text='<%# Bind("waktu_pengiriman_terakhir") %>'></asp:Label></small><br />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="20%">
                                    <HeaderTemplate>
                                        <i class="fas fa-th"></i>
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="text-center" />
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbEditData" CssClass="btn btn-sm btn-primary mr-2" CommandName="Edit" ToolTip="Edit Data">
                                        <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbTambahAkun" CssClass="btn btn-sm btn-primary mr-2" CommandName="Add" ToolTip="Tambah Akun">
                                        <i class="fas fa-user-plus"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbKirimEmail" CssClass="btn btn-sm btn-primary" CommandName="SendMail" ToolTip="Kirim Akun Melalui Email">
                                        <i class="fas fa-paper-plane"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update"
                                            CssClass="btn btn-sm btn-success mr-2" ToolTip="Update Data"><i class="fas fa-save"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbCancelUpdate"
                                            CssClass="btn btn-sm btn-danger" CommandName="Cancel" ToolTip="Batal Edit"><i class="fas fa-undo"></i>
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
                <asp:controlPaging runat="server" ID="pagingDaftarData" OnPageChanging="daftarData_PageChanging" />
            </div>
        </div>
    </div>
</div>
<div class="modal modal-primary" id="modalKonfirmasiGenerate" role="dialog" aria-labelledby="myModalKonfirmasiGenerate">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" >Konfirmasi Generate Akun</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan membuat 
                <div class="form-group">
                    <asp:TextBox runat="server" ID="tbJumlahAkun" CssClass="form-control" Text="1"></asp:TextBox>
                </div>
                akun operator
                &nbsp;<asp:Label runat="server" ID="lblJenisKegiatan" Text=""></asp:Label>&nbsp;untuk Institusi&nbsp;<asp:Label runat="server" ID="lblNamaInstitusi" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbGenerateAkun" runat="server" CssClass="btn btn-danger float-rigth" OnClick="lbGenerateData_Click" OnClientClick="$('#modalKonfirmasiGenerate').modal('hide');">Buat Akun</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
