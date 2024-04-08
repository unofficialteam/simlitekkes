<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="anggotaAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.anggotaAbdimas" %>

<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default m-t-20">
            <div class="form-row col-sm-12">
                <div class="form-group col-lg-12" style="text-align: right; padding-top: 20px; te">
                    <b>
                        <asp:Label ID="lblSkema" runat="server" Text="Terapan"></asp:Label>
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
        
        
        <div class="card mb-4">
            <div class="card-header">
                <h5>Identitas Ketua</h5>
            </div>
            <div class="card-body">
                <div class="panel-body" style="padding: 20px;">
                    <div class="row mb-2">
                        <label class="col-md-2">Nama Ketua</label>
                        <div class="col-md-10">
                            <asp:Label ID="lblNamaKetua" runat="server" Text="-"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-2">Uraian tugas dalam penelitian</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="tbBidangTugasKetua" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="text-align: right; padding: 10px 10px 10px 0px">
                            <asp:LinkButton runat="server" ID="lbSimpanBidangTugasKetua" CssClass="btn btn-outline-success" OnClick="lbSimpanBidangTugasKetua_Click">Simpan</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h5>Identitas Pengusul - 
                <asp:Label ID="lblStsAnggotaDikti2" runat="server" Text="0"></asp:Label>
                            <asp:Label ID="lblJmlAnggotaPengusul" runat="server" CssClass="badge badge-md bg-danger" Enabled="false" Text="0">
                            </asp:Label>
                        </h5>
                    </div>
                    <div class="text-right">
                        <div class="card-header-text f-right">
                            <asp:LinkButton ID="lbDataBaruAnggotaDikti" runat="server" OnClick="lbDataBaruAnggotaDikti_Click" Text="Tambah Baru"
                                CssClass="btn btn-success btn-sm">
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvAnggotaPengusulDikti" runat="server" GridLines="None" ShowHeader="false" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False" DataKeyNames="id_personil, nama_anggota, kd_sts_konfirmasi"
                        OnRowDeleting="gvAnggotaPengusulDikti_RowDeleting" OnRowDataBound="gvAnggotaPengusulDikti_RowDataBound">
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
                                    <asp:LinkButton ID="lbHapus" runat="server" CommandName="delete" ForeColor="Red" Font-Bold="true"
                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                        CssClass="fas fa-trash-alt" Font-Size="XX-Large">
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
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h5>Indentitas Pengusul - 
                 <asp:Label ID="lbAnggotaTendikKemkes" runat="server" Text="0"></asp:Label>
                            <asp:Label ID="lbJmlAnggotaTendikKemkes" runat="server" CssClass="badge badge-md bg-danger" Enabled="false" Text="0">
                            </asp:Label>
                        </h5>
                    </div>
                    <div class="text-right">
                        <h5 class="card-header-text f-right">
                            <asp:LinkButton ID="lbDataBaruTendikKemkes" runat="server" OnClick="lbDataBaruTendikKemkes_Click" Text="Tambah Baru"
                                CssClass="btn btn-success btn-sm">
                            </asp:LinkButton>
                        </h5>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvAnggotaTendikKemkes" runat="server" GridLines="None" ShowHeader="false" ShowHeaderWhenEmpty="true"
                    AutoGenerateColumns="False" DataKeyNames="id_personil, nama_anggota, kd_sts_konfirmasi" OnRowDeleting="gvAnggotaTendikKemkes_RowDeleting" OnRowDataBound="gvAnggotaPengusulTendik_RowDataBound">
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
                                (<asp:Label ID="lblNIDN" runat="server" Text='<%# Bind("nomor_ktp") %>'></asp:Label>)<br>
                                <asp:Label ID="lblInstitusi" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                -
                                <asp:Label ID="lblPeranPersonil" runat="server" Text='<%# Bind("peran_personil") %>' BackColor="Yellow"></asp:Label>
                                <br>
                                Tugas:
                                            <asp:Label ID="lblTugas" runat="server" Text='<%# Bind("bidang_tugas") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbHapus" runat="server" CommandName="delete" ForeColor="Red" Font-Bold="true"
                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                    CssClass="fas fa-trash-alt" Font-Size="XX-Large">
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
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h5>Indentitas Pengusul - 
                 <asp:Label ID="lblStsAnggotaNonDikti" runat="server" Text="0"></asp:Label>
                            <asp:Label ID="lblJmlAnggotaNonDikti" runat="server" CssClass="badge badge-md bg-danger" Enabled="false" Text="0">
                            </asp:Label>
                        </h5>
                    </div>
                    <div class="text-right">
                        <h5 class="card-header-text f-right">
                            <asp:LinkButton ID="lbDataBaruNonDikti" runat="server" OnClick="lbDataBaruNonDikti_Click" Text="Tambah Baru"
                                CssClass="btn btn-success btn-sm">
                            </asp:LinkButton>
                        </h5>
                    </div>
                </div>
            </div>
            <div class="card-body">

                <asp:GridView ID="gvAnggotaNonDikti" runat="server" GridLines="None" ShowHeader="false" ShowHeaderWhenEmpty="true"
                    AutoGenerateColumns="False" DataKeyNames="id_personil, nama" OnRowDeleting="gvAnggotaNonDikti_RowDeleting">
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
                                <asp:LinkButton ID="lbHapus" runat="server" CommandName="delete" ForeColor="Red" Font-Bold="true"
                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                    CssClass="fas fa-trash-alt" Font-Size="XX-Large">
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
    </asp:View>
    <asp:View ID="vIsiAnggotaDikti" runat="server">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading bg-default txt-white">
                    Identitas Pengusul -
                <asp:Label ID="lblAnggotaBaruDikti" runat="server"></asp:Label>
                </div>
                <div class="form-group row" style="padding: 10px 0px 0px 10px">
                    <div class="col-md-1">
                        <label class="control-label">NIDN</label>
                    </div>
                    <div class="col-md-8">
                        <div class="input-group">
                            <asp:TextBox ID="tbCari" runat="server" CssClass="form-control" placeholder="NIDN" aria-describedby="btn-addon2" MaxLength="10"></asp:TextBox>
                            <span class="input-group-btn" id="btn-addon2">
                                <asp:LinkButton ID="lbCari" runat="server" class="btn btn-warning addon-btn waves-effect waves-light"
                                    OnClick="lbCari_Click">
                                    <i class="fa fa-search"></i>
                                </asp:LinkButton>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="general-info">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="card" style="width: 150px;">
                                    <div class="social-profile">
                                        <asp:Image CssClass="img-fluid" runat="server" ID="imgProfile" ImageUrl="~/assets/dist/img/avatar/avatar-1.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-9" style="padding-top: 20px;">
                                <div class="form-group row">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="lblNama" class="col-sm-2 control-label">Nama</label>
                                        <div class="col-sm-10">
                                            <asp:Label runat="server" ID="lblNama" Text="-"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="lblInstitusi" class="col-sm-2 control-label">Institusi</label>
                                        <div class="col-sm-10">
                                            <asp:Label runat="server" ID="lblInstitusi" Text="-"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="lblProgStudi" class="col-sm-2 control-label">Program Studi</label>
                                        <div class="col-sm-10">
                                            <asp:Label runat="server" ID="lblProgStudi" Text="-"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label for="lblKualifikasi" class="col-sm-2 control-label">Kualifikasi</label>
                                    <asp:Label runat="server" ID="lblKualifikasi" Text="-"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblinfoEligibleAnggota" runat="server" Visible="false" BackColor="Yellow" ForeColor="Red" Font-Size="Small" Font-Italic="true"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label for="lblSurel" class="col-sm-2 control-label">Alamat Surel</label>
                                    <asp:Label runat="server" ID="lblSurel" Text="-"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label for="ddlPeran" class="col-sm-2 control-label">Peran</label>
                                    <asp:DropDownList ID="ddlPeran" runat="server" Enabled="true" ClientIDMode="Static"
                                        CssClass="form-control select2" AutoPostBack="true"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Text="-- Pilih --" Value="0" Selected="True" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label for="tbJudulHKI" class="col-sm-12 control-label">Tugas dalam pengabdian</label>
                                    <asp:TextBox runat="server" ID="tbTugasdlmPengabdian" placeholder="Tugas dalam pengabdian kepada masyarakat"
                                        CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="text-align: center; padding: 20px 0px 20px 0px;">
                        <asp:LinkButton ID="lbSimpan" runat="server" type="submit" class="btn btn-success waves-effect waves-light mr-2" OnClick="lbSimpan_Click">Simpan</asp:LinkButton>
                        <asp:LinkButton ID="lbBatal" runat="server" type="submit" class="btn btn-danger waves-effect waves-light" OnClick="lbBatal_Click">Batal</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vIsiAnggotaTendikKemkes" runat="server">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading bg-default txt-white">
                    Identitas Pengusul -
                <asp:Label ID="lblAnggotaBaruTendikKemkes" runat="server"></asp:Label>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="general-info">
                        <div class="row" style="padding-left: 10px;">
                            <div class="col-xl-12 col-lg-12" style="padding-top: 20px;">
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbNoIdentitas" class="col-sm-3 control-label">No. Identitas (KTP/Paspor)</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbNoKTPTendikKemkes" Placeholder="No. Identitas (KTP/Passpor) 16 karakter" MaxLength="16" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:LinkButton runat="server" ID="lbCariTendikKemkes" CssClass="btn btn-block btn-info" OnClick="lbCariTendik_Click"><i class="fas fa-search mr-2"></i>Cari Tendik</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbNamaLengkap" class="col-sm-3 control-label">Nama Lengkap dan Gelar</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" Enabled="false" ID="tbNamaLengkapTendikKemkes" placeholder="Nama Lengkap dan Gelar" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbAlamatTinggal" class="col-sm-3 control-label">Alamat Tinggal</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" Enabled="false" ID="tbALamatTendikKemkes" Placeholder="Alamat Tinggal" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="ddlNegara" class="col-sm-3 control-label">Negara</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlNegara" Enabled="false" runat="server" ClientIDMode="Static"
                                                CssClass="form-control select2" AutoPostBack="true"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbInstansi" class="col-sm-3 control-label">Instansi</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" Enabled="false" ID="tbInstansiTendikKemkes" Placeholder="Instansi" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <%--<label for="ddlKualifikasi" class="col-sm-3 control-label">Kualifikasi</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlKualifikasiTendikKemkes" runat="server" Enabled="true" ClientIDMode="Static"
                                                CssClass="form-control select2" AutoPostBack="true"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>--%>
                                        <label for="tbBidangKeahlian" class="col-sm-3 control-label">Bidang Keahlian</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox Enabled="false" runat="server" ID="tbBidangKeahlianTendikKemkes" Placeholder="Bidang Keahlian" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbAlamatSurel" class="col-sm-3 control-label">Alamat Surel</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox Enabled="false" runat="server" ID="tbAlamatSurelTendikKemkes" Placeholder="Alamat Surel" TextMode="Email" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label for="tbNoHP" class="col-sm-1 control-label" style="text-align: right;">No. HP</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox Enabled="false" runat="server" ID="tbNoHpTendikKemkes" Placeholder="No. HP (16 karakter)" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="ddlPeranNonDikti" class="col-sm-3 control-label">Peran</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPeranTendikKemkes" runat="server" Enabled="true" ClientIDMode="Static"
                                                CssClass="form-control select2" AutoPostBack="true"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label ID="lblinfoEligibleAnggotaTendikKemkes" runat="server" Visible="false" BackColor="Yellow" ForeColor="Red" Font-Size="Small" Font-Italic="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbTugasPenelitian" class="col-sm-3 control-label">Tugas dalam penelitian</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbTugasPenelitianTendikKemkes" Placeholder="Tugas dalam penelitian" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer clearfix mb-3">
                    <div class="row">
                        <div class="col-md-11">
                            <div class="float-right">
                                <asp:LinkButton ID="lbBatalTendikKemkes" runat="server" type="submit" class="btn btn-danger waves-effect waves-light mr-2" OnClick="lbBatalTendikKemkes_Click">Batal</asp:LinkButton>
                                <asp:LinkButton ID="lbSimpanTendikKemkes" runat="server" type="submit" class="btn btn-success waves-effect waves-light" OnClick="lbSimpanTendikKemkes_Click">Simpan</asp:LinkButton>
                            </div>
                        </div>
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
                <hr />
                <div class="panel-body">
                    <div class="general-info">
                        <div class="row" style="padding-left: 10px;">
                            <div class="col-xl-12 col-lg-12" style="padding-top: 20px;">
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbNoIdentitas" class="col-sm-3 control-label">No. Identitas (KTP/Paspor)</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="tbNoIdentitas" Placeholder="No. Identitas (KTP/Passpor) 16 karakter" MaxLength="16" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbNamaLengkap" class="col-sm-3 control-label">Nama Lengkap dan Gelar</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" Enabled="true" ID="tbNamaLengkap" placeholder="Nama Lengkap dan Gelar" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbAlamatTinggal" class="col-sm-3 control-label">Alamat Tinggal</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" Enabled="true" ID="tbAlamatTinggal" Placeholder="Alamat Tinggal" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="ddlNegara" class="col-sm-3 control-label">Negara</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlNegara" Enabled="true" runat="server" ClientIDMode="Static"
                                                CssClass="form-control select2" AutoPostBack="false"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbInstansi" class="col-sm-3 control-label">Instansi</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" Enabled="true" ID="tbInstansi" Placeholder="Instansi" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="ddlKualifikasi" class="col-sm-3 control-label">Kualifikasi</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlKualifikasi" runat="server" Enabled="true" ClientIDMode="Static"
                                                CssClass="form-control select2" AutoPostBack="false"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                        <label for="tbBidangKeahlian" class="col-sm-2 control-label" style="text-align: right;">Bidang Keahlian</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox Enabled="true" runat="server" ID="tbBidangKeahlian" Placeholder="Bidang Keahlian" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbAlamatSurel" class="col-sm-3 control-label">Alamat Surel</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox Enabled="true" runat="server" ID="tbAlamatSurel" Placeholder="Alamat Surel" TextMode="Email" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label for="tbNoHP" class="col-sm-1 control-label" style="text-align: right;">No. HP</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox Enabled="true" runat="server" ID="tbNoHP" Placeholder="No. HP (16 karakter)" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="ddlPeranNonDikti" class="col-sm-3 control-label">Peran</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlPeranNonDikti" runat="server" Enabled="true" ClientIDMode="Static"
                                                CssClass="form-control select2" AutoPostBack="false"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row col-sm-12" style="padding-bottom: 5px;">
                                        <label for="tbTugasPenelitian" class="col-sm-3 control-label">Tugas dalam penelitian</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbTugasPenelitian" Placeholder="Tugas dalam penelitian" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer clearfix mb-3">
                    <div class="row">
                        <div class="col-md-11">
                            <div class="float-right">
                                <asp:LinkButton ID="lbBatalNonDikti" runat="server" type="submit" class="btn btn-danger waves-effect waves-light mr-2" OnClick="lbBatalNonDikti_Click">Batal</asp:LinkButton>
                                <asp:LinkButton ID="lbSimpanNonDikti" runat="server" type="submit" class="btn btn-success waves-effect waves-light" OnClick="lbSimpanNonDikti_Click">Simpan</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>

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

<div class="modal modal-primary" id="modalHapusNonDIkti" role="dialog" aria-labelledby="myModalHapusNonDIkti">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" id="dangerModalLabel2">Konfirmasi Hapus Data</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
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
<div class="modal modal-primary" id="modalHapusTendikKemkes" role="dialog" aria-labelledby="myModalHapusNonDIkti">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" id="dangerModalLabel3">Konfirmasi Hapus Data</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus data
                &nbsp;<asp:Label runat="server" ID="lblNamaTendikKemkes" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusTendikKemkes" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapusTendikKemkes_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>

            </div>
        </div>
    </div>
</div>