﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="penelitianNonKemkes.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.penelitianNonKemkes" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<div class="row">
    <div class="col-md-12">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h6>Daftar Penelitian Non Kemkes</h6>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-1">
                        <asp:DropDownList ID="ddlJmlBarisData" runat="server" AutoPostBack="True"
                            CssClass="form-control" OnSelectedIndexChanged="ddlJmlBarisData_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="99999" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="ddlTahunKegiatan"
                            CssClass="form-control select2"
                            OnSelectedIndexChanged="ddlTahunKegiatan_SelectedIndexChanged"
                            AutoPostBack="true" />
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <asp:TextBox runat="server" ID="tbPencarian" CssClass="form-control" placeholder="Masukkan Judul Penelitian"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:LinkButton runat="server" ID="lbCariData" CssClass="btn btn-primary" OnClick="lbCariData_Click"><i class="fa fa-search mr-2"></i>Cari Data</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-3 text-right">
                        <asp:LinkButton runat="server" ID="lbTambahData" CssClass="btn btn-success" OnClick="lbTambahData_Click"><i class="fa fa-plus mr-2"></i>Tambah Data Baru</asp:LinkButton>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px;">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvDaftarData" runat="server" GridLines="None"
                                CssClass="table table-striped table-hover"
                                DataKeyNames="id_personal, jenis_kelamin, nama_lengkap, tempat_lahir, tanggal_lahir, alamat, nomor_ktp, surel, website_personal, nomor_telepon, bidang_keahlian, kd_sts_aktif, status_aktif"
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
                                    <asp:TemplateField HeaderText="Judul">
                                        <ItemTemplate>
                                            <%# Eval("judul")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Personil" HeaderStyle-Width="30%">
                                        <ItemTemplate>
                                            Ketua :&nbsp; <span class="font-color-blue">
                                                <%# Eval("nama_ketua")%></span>
                                            <br />
                                            Anggota :&nbsp; <span class="font-color-blue">
                                                <%# Eval("nama_anggota")%></span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penelitian">
                                        <ItemTemplate>
                                            Jenis :&nbsp;<span class="font-color-blue"><%# Eval("jenis_penelitian")%></span><br />
                                            Bidang :&nbsp;<span class="font-color-blue"><%# Eval("bidang_penelitian")%></span>
                                            <asp:Panel ID="PanelBidangLainnya" runat="server" Visible="false">
                                                &nbsp;-&nbsp;<span class="font-color-blue"><%# Eval("bidang_penelitian_lain")%></span>
                                            </asp:Panel>
                                            <br />
                                            TSE :&nbsp;<span class="font-color-blue"><%# Eval("tujuan_sosial_ekonomi")%></span><asp:Panel
                                                ID="PanelTSELainnya" runat="server" Visible="false">
                                                &nbsp;-&nbsp;<span class="font-color-blue"><%# Eval("tujuan_sosial_ekonomi_lain")%></span>
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="200px" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dana">
                                        <ItemTemplate>
                                            Sumber :&nbsp;<span class="font-color-blue"><%# Eval("sumber_dana")%></span><br />
                                            Institusi : &nbsp;<span class="font-color-blue"><%# Eval("institusi_sumber_dana")%></span><br />
                                            Jumlah :&nbsp; <span class="font-color-blue">
                                                <%# Convert.ToDecimal(Eval("jumlah_dana")).ToString("N0", new System.Globalization.CultureInfo("id-ID"))%></span>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="150px" VerticalAlign="Top" />
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
            </div>
            <div class="card-footer">
                <asp:controlPaging runat="server" ID="pagingDaftarData" OnPageChanging="daftarData_PageChanging" />
            </div>
        </div>
    </div>
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
