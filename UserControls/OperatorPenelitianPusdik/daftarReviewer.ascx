<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="daftarReviewer.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.daftarReviewer" %>
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
                                <h6>Daftar Reviewer</h6>
                            </div>
                            <div class="card-body row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlJmlBarisData" runat="server" AutoPostBack="True"
                                            CssClass="form-control select2" OnSelectedIndexChanged="ddlJmlBarisData_SelectedIndexChanged">
                                            <asp:ListItem Text="10" Value="10" Selected="True" />
                                            <asp:ListItem Text="50" Value="50" />
                                            <asp:ListItem Text="100" Value="100" />
                                            <asp:ListItem Text="Semua" Value="99999" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddlFilterStatusKepegawaian" CssClass="form-control select2" OnSelectedIndexChanged="ddlFilterStatusKepegawaian_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddlKategoriReviewer" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlKategoriReviewer_SelectedIndexChanged">
                                            <asp:ListItem Text="Penelitian" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Pengabmas" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="tbPencarian" CssClass="form-control" placeholder="Masukkan NIDN/Nama"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
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
                                            DataKeyNames="id_personal, id_reviewer_nasional_penelitian, id_reviewer, kompetensi, no_sertifikasi, kd_sts_aktif, status_aktif, nama_institusi, nidn, nama_lengkap"
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
                                                <asp:TemplateField HeaderText="Identitas Reviewer" HeaderStyle-Width="35%">
                                                    <ItemTemplate>
                                                        <b>Nama : </b>
                                                        <asp:Label runat="server" ID="lblNamaLengkap" Text='<%# Bind("nama_lengkap") %>'></asp:Label><br />
                                                        <b>NIDN. </b>
                                                        <asp:Label runat="server" ID="lblNIDN" Text='<%# Bind("nidn") %>'></asp:Label>
                                                        | Hand Phone:
                                                        <asp:Label runat="server" ID="lblNoHP" Text='<%# Bind("nomor_hp") %>'></asp:Label>
                                                        <hr />
                                                        <b>Kompetensi : </b>
                                                        <asp:Label runat="server" ID="lblKompetensi" Text='<%# Bind("kompetensi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Keterangan" HeaderStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <b>No. Sertfikasi : </b>
                                                        <asp:Label runat="server" ID="lblNoSertifikasi" Text='<%# Bind("no_sertifikasi") %>'></asp:Label><br />
                                                        <b>Nama Institusi : </b>
                                                        <asp:Label runat="server" ID="lblNamaInstitusi" Text='<%# Bind("nama_institusi") %>'></asp:Label><br />
                                                        <hr />
                                                        <b>Status Kepegawaian :</b><br />
                                                        <asp:Label runat="server" ID="lblStatusKepegawaian" Text='<%# Bind("status_aktif") %>'></asp:Label>
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
                                        <h6>Tambah/Ubah Data Reviewer <asp:Label runat="server" ID="lblKategoriReviewer" Text=""></asp:Label> </h6>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="tbNIDN" CssClass="form-control" placeholder="Masukkan NIDN"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lbCekNIDN" CssClass="btn btn-block btn-primary" OnClick="lbCekNIDN_Click"><i class="fas fa-search mr-2"></i>Cari Dosen</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton runat="server" ID="lbDaftarData" CssClass="btn btn-success btn-block" OnClick="lbDaftarData_Click"><i class="fas fa-arrow-left mr-2"></i>Daftar Reviewer</asp:LinkButton>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="card">
                                            <div class="card-header bg-secondary text-white">
                                                <h6>Detail Dosen</h6>
                                            </div>
                                            <div class="card-body">
                                                <asp:MultiView runat="server" ID="mvResultDosen" ActiveViewIndex="0">
                                                    <asp:View runat="server" ID="vNotFound">
                                                        <label>Data Dosen Tidak Ditemukan</label>
                                                    </asp:View>
                                                    <asp:View runat="server" ID="vFound">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <b>
                                                                    <asp:Label runat="server" ID="lblDetailNamaLengkap"></asp:Label></b><br />
                                                                <small>NIDN.
                                                                    <asp:Label runat="server" ID="lblDetailNIDN"></asp:Label></small>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <small><b>Jenis Kelamin:</b><br />
                                                                    <asp:Label runat="server" ID="lblDetailJenisKelamin"></asp:Label>
                                                                </small>
                                                                <br />
                                                            </div>
                                                            <div class="col-md-2">
                                                                <small><b>Alamat Email: </b>
                                                                    <br />
                                                                    <asp:Label runat="server" ID="lblDetailAlamatEmail"></asp:Label>
                                                                </small>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <small><b>Nama Institusi:</b><br />
                                                                    <asp:Label runat="server" ID="lblDetailNamaInstitusi"></asp:Label>
                                                                </small>
                                                                <br />
                                                            </div>
                                                            <div class="col-md-1">
                                                                <small><b>Status:</b><br />
                                                                    <asp:Label runat="server" ID="lblDetailStatus"></asp:Label>
                                                                </small>
                                                            </div>
                                                            <div class="col-md-5 mt-5">
                                                                <asp:TextBox runat="server" ID="tbKompetensi" CssClass="form-control" placeholder="Masukkan Kompetensi Reviewer"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4 mt-5">
                                                                <asp:TextBox runat="server" ID="tbNoSertifikasi" CssClass="form-control" placeholder="Masukkan Nomor Sertifikasi Reviewer"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3 mt-5">
                                                                <asp:LinkButton runat="server" CssClass="float-right btn btn-primary" ID="lbSimpanData" OnClick="lbSimpanData_Click"><i class="fas fa-save mr-2"></i>Tambah sebagai Reviewer</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                </asp:MultiView>
                                            </div>
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
        <div class="modal modal-primary" id="modalEditStatus" role="dialog" aria-labelledby="myModalEditStatus">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title font-weight-600" id="dangerModalEditStatus">Ubah Status Kepegawaian</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Dengan Menekan Tombol Simpan, Pegawai Atas Nama
                &nbsp;<asp:Label runat="server" ID="lblNamaPegawai" Text="" Font-Bold="true"></asp:Label>&nbsp; Akan di Ubah Menjadi
                <div class="form-group">
                    <asp:DropDownList runat="server" ID="ddlEditStatusKepegawaian" CssClass="select2 form-control"></asp:DropDownList>
                </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Batal</button>
                        <asp:LinkButton ID="lbSimpanStatusKepegawaian" runat="server" CssClass="btn btn-primary float-rigth" OnClick="lbSimpanStatusKepegawaian_Click" OnClientClick="$('#modalEditStatus').modal('hide');"><i class="fa fa-save mr-2"></i>Simpan</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

