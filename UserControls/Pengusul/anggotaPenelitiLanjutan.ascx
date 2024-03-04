﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="anggotaPenelitiLanjutan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.anggotaPenelitiLanjutan" %>

<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default m-t-20">
            <div class="form-row col-sm-12">
                <div class="form-group col-lg-12" style="text-align: right; padding-top:20px; te">
                    <b><asp:Label ID="lblSkema" runat="server" Text="Terapan"></asp:Label>
                    (Tahun ke
                <asp:Label ID="lblUrutanUsulan" runat="server" Text="0"></asp:Label>
                    dari
                        <asp:Label ID="lblLamaUsulan" runat="server" Text="0"></asp:Label>
                    tahun - Maks
                       <asp:Label ID="lblMaksAnggota" runat="server" Text="0" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblStsAnggotaDikti" runat="server" Text="0"></asp:Label>)</b>                
                </div>
            </div>
        </div>
    </div>
</div>
<asp:MultiView ID="mvAnggota" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarAnggota" runat="server">
        <div class="col-sm-12" style="padding-bottom: 10px;">
            <div class="panel panel-default" style="min-height: 300px;">
                <div class="panel-heading bg-default txt-white">
                    Identitas Pengusul - 
                <asp:Label ID="lblStsAnggotaDikti2" runat="server" Text="0"></asp:Label>
                    <asp:Label ID="lblJmlAnggotaPengusul" runat="server" CssClass="badge badge-md bg-danger" Enabled="false" Text="0">
                    </asp:Label>
                </div>
                <div class="panel-body">
                    <div style="text-align: right; padding: 10px 10px 10px 0px">
                        <asp:LinkButton ID="lbDataBaruAnggotaDikti" runat="server" OnClick="lbDataBaruAnggotaDikti_Click" Text="Tambah Baru"
                            CssClass="btn btn-success btn-sm">
                        </asp:LinkButton>
                    </div>
                    <div class="form-row" style="margin-left: 30px; padding-top:30px;">
                        <asp:GridView ID="gvAnggotaPengusulDikti" runat="server" GridLines="None" ShowHeader="false" ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="False" DataKeyNames="id_personil, nama_anggota, kd_sts_konfirmasi"
                            OnRowDataBound="gvAnggotaPengusulDikti_RowDataBound" OnRowUpdating="gvAnggotaPengusulDikti_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStsPersetujuan" runat="server" CssClass="fa fa-check-square" Font-Size="50px" ForeColor="green"></asp:Label><br />
                                        <b>
                                            <asp:Label ID="lblPersetujuan" runat="server" Text='<%# Bind("status_konfirmasi") %>' Font-Size="Small" ForeColor="Green"></asp:Label></b>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblNama" runat="server" Text='<%# Bind("nama_anggota") %>' ForeColor="DarkBlue"></asp:Label>
                                        </b>
                                        (<asp:Label ID="lblNIDN" runat="server" Text='<%# Bind("nidn") %>'></asp:Label>)<br>
                                        <asp:Label ID="lblInstitusi" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                        - 
                                        <asp:Label ID="lblProgStudi" runat="server" Text='<%# Bind("nama_program_studi") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblPeranPersonil" runat="server" Text='<%# Bind("peran_personil") %>' BackColor="Yellow"></asp:Label>
                                        <br>
                                        Tugas:
                                            <asp:Label ID="lblTugas" runat="server" Text='<%# Bind("bidang_tugas") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbUbahAnggota" runat="server" CommandName="update" ForeColor="Black" Font-Bold="true"
                                            CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Ubah" 
                                            CssClass="fa fa-edit" Font-Size="XX-Large">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbHapus" runat="server" CommandName="delete" ForeColor="Red" Font-Bold="true"
                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                            CssClass="fa fa-trash-o" Font-Size="XX-Large">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="min-height: 100px; margin: 0 auto;">
                                    <strong>DATA TIDAK DITEMUKAN</strong>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-12">
            <div class="panel panel-default" style="min-height: 300px;">
                <div class="panel-heading bg-default txt-white">
                    Indentitas Pengusul - 
                 <asp:Label ID="lblStsAnggotaNonDikti" runat="server" Text="0"></asp:Label>
                    <asp:Label ID="lblJmlAnggotaNonDikti" runat="server" CssClass="badge badge-md bg-danger" Enabled="false" Text="0">
                    </asp:Label>
                </div>
                <div class="panel-body">
                    <div style="text-align: right; padding: 10px 10px 10px 0px">
                        <asp:LinkButton ID="lbDataBaruNonDikti" runat="server" OnClick="lbDataBaruNonDikti_Click" Text="Tambah Baru"
                            CssClass="btn btn-success btn-sm">
                        </asp:LinkButton>
                    </div>
                    <div class="form-row" style="margin-left: 30px; padding-top:10px;">
                        <asp:GridView ID="gvAnggotaNonDikti" runat="server" GridLines="None" ShowHeader="false" ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="False" DataKeyNames="id_personil, nama, peran_personil" OnRowUpdating="gvAnggotaNonDikti_RowUpdating"
                            OnRowDataBound="gvAnggotaNonDikti_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="lblNama" runat="server" Text='<%# Bind("nama") %>' ForeColor="DarkBlue"></asp:Label>
                                        </b>
                                        (<asp:Label ID="lblNIDN" runat="server" Text='<%# Bind("no_ktp") %>'></asp:Label>)<br>
                                        <asp:Label ID="lblInstitusi" runat="server" Text='<%# Bind("nama_instansi_asal") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblPeranPersonil" runat="server" Text='<%# Bind("peran_personil") %>' BackColor="Yellow"></asp:Label><br />
                                        Tugas:
                                            <asp:Label ID="lblTugas" runat="server" Text='<%# Bind("bidang_tugas") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbUbah" runat="server" CommandName="update" ForeColor="Black" Font-Bold="true"
                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Ubah"
                                            CssClass="fa fa-edit" Font-Size="XX-Large">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbHapus" runat="server" CommandName="delete" ForeColor="Red" Font-Bold="true"
                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                            CssClass="fa fa-trash-o" Font-Size="XX-Large">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="min-height: 100px; margin: 0 auto;">
                                    <strong>DATA TIDAK DITEMUKAN</strong>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

    </asp:View>
    <asp:View ID="vIsiAnggotaDikti" runat="server">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading bg-default txt-white">
                    Identitas Pengusul -
                <asp:Label ID="lblAnggotaBaruDikti" runat="server"></asp:Label>
                </div>
                <%--<div style="text-align: left; padding: 10px 10px 10px 10px">
                    NIDN
                    <asp:TextBox ID="tbCariAnggotaBaruDikti" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:LinkButton ID="lbCariAnggotaBaruDikti" runat="server" CssClass="fa fa-search"></asp:LinkButton>
                </div>--%>
                <div class="form-group row" style="padding: 10px 0px 0px 10px">
                    <div class="col-md-1">
                        <label class="control-label">NIDN</label>
                    </div>
                    <div class="col-md-2">
                        <div class="input-group">
                            <asp:TextBox ID="tbCari" runat="server" CssClass="form-control" placeholder="NIDN" aria-describedby="btn-addon2" MaxLength="10" Enabled="false"></asp:TextBox>
                            <span class="input-group-btn" id="btn-addon2">
                                <asp:LinkButton ID="lbCari" runat="server" class="btn btn-warning addon-btn waves-effect waves-light" Enabled="false">
                                    <i class="fa fa-search"></i>
                                </asp:LinkButton>                                
                            </span>                            
                        </div>                        
                    </div>
                </div>
                <div class="panel-body">
                    <div class="general-info">
                        <div class="row" style="padding-left: 10px;">
                            <div class="col-xl-2 col-lg-2">
                                <div class="card faq-left" style="width: 150px;">
                                    <div class="social-profile">
                                        <asp:Image CssClass="img-fluid" runat="server" ID="imgProfile" ImageUrl="../../assets/images/avatar-1.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-10 col-lg-10" style="padding-top: 20px;">
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="lblNama" class="col-sm-2 control-label">Nama</label>
                                        <div class="col-sm-10">
                                            <asp:Label runat="server" ID="lblNama" Text="-"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="lblInstitusi" class="col-sm-2 control-label">Institusi</label>
                                        <div class="col-sm-10">
                                            <asp:Label runat="server" ID="lblInstitusi" Text="-"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="lblProgStudi" class="col-sm-2 control-label">Program Studi</label>
                                        <div class="col-sm-10">
                                            <asp:Label runat="server" ID="lblProgStudi" Text="-"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-left: 20px;">
                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <label for="lblKualifikasi" class="col-sm-2 control-label">Kualifikasi</label>
                                <div class="col-sm-10">
                                    <asp:Label runat="server" ID="lblKualifikasi" Text="-"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblinfoEligibleAnggota" runat="server" Visible="false" BackColor="Yellow" ForeColor="Red" Font-Size="Small" Font-Italic="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <label for="lblSurel" class="col-sm-2 control-label">Alamat Surel</label>
                                <div class="col-sm-10">
                                    <asp:Label runat="server" ID="lblSurel" Text="-"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <label for="ddlPeran" class="col-sm-2 control-label">Peran</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlPeran" runat="server" Enabled="false" ClientIDMode="Static"
                                        CssClass="form-control select2" AutoPostBack="true"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Text="-- Pilih --" Value="0" Selected="True" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <label for="tbTugasPenelitian" class="col-sm-2 control-label">Tugas dalam penelitian</label>
                                <div class="col-sm-10">
                                    <asp:TextBox runat="server" ID="tbTugasdlmPenelitian" placeholder="Tugas dalam penelitian"
                                        CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="text-align: center; padding: 20px 0px 20px 0px;">
                        <asp:LinkButton ID="lbSimpan" runat="server" type="submit" class="btn btn-success waves-effect waves-light" OnClick="lbSimpan_Click">Simpan</asp:LinkButton>
                        <asp:LinkButton ID="lbBatal" runat="server" type="submit" class="btn btn-danger waves-effect waves-light" OnClick="lbBatal_Click">Batal</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>

    <asp:View ID="vIsiAnggotaNonDikti" runat="server">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading bg-default txt-white">
                    Identitas Pengusul -
                <asp:Label ID="lblAnggotaBaruNonDikti" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="general-info">
                        <div class="row" style="padding-left: 10px;">
                            <div class="col-xl-12 col-lg-12" style="padding-top: 20px;">
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbNamaLengkap" class="col-sm-3 control-label">Nama Lengkap dan Gelar</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbNamaLengkap" placeholder="Nama Lengkap dan Gelar" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbNoIdentitas" class="col-sm-3 control-label">No. Identitas (KTP/Paspor)</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbNoIdentitas" Placeholder="No. Identitas (KTP/Passpor) 16 karakter" MaxLength="16" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbAlamatTinggal" class="col-sm-3 control-label">Alamat Tinggal</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" ID="tbAlamatTinggal" Placeholder="Alamat Tinggal" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="ddlNegara" class="col-sm-3 control-label">Negara</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlNegara" runat="server" Enabled="false" ClientIDMode="Static"
                                                CssClass="form-control select2" AutoPostBack="true"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbInstansi" class="col-sm-3 control-label">Instansi</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbInstansi" Placeholder="Instansi" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="ddlKualifikasi" class="col-sm-3 control-label">Kualifikasi</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlKualifikasi" runat="server" Enabled="false" ClientIDMode="Static"
                                                CssClass="form-control select2" AutoPostBack="true"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                        <label for="tbBidangKeahlian" class="col-sm-2 control-label" style="text-align: right;">Bidang Keahlian</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbBidangKeahlian" Placeholder="Bidang Keahlian" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbAlamatSurel" class="col-sm-3 control-label">Alamat Surel</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbAlamatSurel" Placeholder="Alamat Surel" TextMode="Email" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label for="tbNoHP" class="col-sm-1 control-label" style="text-align: right;">No. HP</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" ID="tbNoHP" Placeholder="No. HP (16 karakter)" TextMode="Number" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="ddlPeranNonDikti" class="col-sm-3 control-label">Peran</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPeranNonDikti" runat="server" Enabled="false" ClientIDMode="Static"
                                                CssClass="form-control select2" AutoPostBack="true"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbTugasPenelitian" class="col-sm-3 control-label">Tugas dalam penelitian</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" ID="tbTugasPenelitian" Placeholder="Tugas dalam penelitian" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="text-align: center; padding: 20px 0px 20px 0px;">
                        <asp:LinkButton ID="lbSimpanNonDikti" runat="server" type="submit" class="btn btn-success waves-effect waves-light" OnClick="lbSimpanNonDikti_Click">Simpan</asp:LinkButton>
                        <asp:LinkButton ID="lbBatalNonDikti" runat="server" type="submit" class="btn btn-danger waves-effect waves-light" OnClick="lbBatalNonDikti_Click">Batal</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

<div class="modal modal-danger" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalHapus">Konfirmasi Hapus Data</h4>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus data
                &nbsp;<asp:Label runat="server" ID="lblNamaPersonil" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>

            </div>
        </div>
    </div>
</div>
<div class="modal modal-danger" id="modalHapusNonDIkti" role="dialog" aria-labelledby="myModalHapusNonDIkti">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalHapusNonDIkti">Konfirmasi Hapus Data</h4>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus data
                &nbsp;<asp:Label runat="server" ID="lblNamaNonDikti" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusNonDikti" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapusNonDikti_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>

            </div>
        </div>
    </div>
</div>



