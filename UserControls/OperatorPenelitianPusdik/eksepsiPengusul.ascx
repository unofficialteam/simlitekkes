<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="eksepsiPengusul.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.eksepsiPengusul" %>

<div class="row">
    <div class="col-md-12">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h5 style="color: darkred">Eksepsi Pengusul</h5>
                    </div>
                    <div class="text-right">
                        <asp:LinkButton runat="server" ID="lbTambah" CssClass="btn btn-success"
                            OnClick="lbTambah_Click">
                        <i class="fa fa-plus"></i>&nbsp;Tambah
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbKembali" runat="server" type="submit" Visible="false"
                            class="btn btn-dark waves-effect waves-light"
                            OnClick="lbKembali_Click"><i class="fa fa-undo"> </i>&nbsp;Kembali
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="row ml-3">
                <div class="col-md-3">
                    <asp:Label runat="server" ID="lblThnUsulan">Tahun Usulan</asp:Label>
                    <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="True"
                        CssClass="form-control input-sm"
                        OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label runat="server" ID="lblThnPelaksanaan">Tahun Pelaksanaan</asp:label>
                    <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                        CssClass="form-control input-sm"
                        OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="card-body">
                <asp:MultiView runat="server" ID="mvMain">
                    <asp:View runat="server" ID="vDaftarEksepsi">
                        <asp:GridView ID="gvDaftarEksepsi" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                            CssClass="table table-striped table-hover" DataKeyNames="id_whitelist_usulan_personal, status, kd_sts_pengusul"
                            AutoGenerateColumns="False" Width="100%"
                            OnRowDataBound="gvDaftarEksepsi_RowDataBound"
                            OnRowUpdating="gvDaftarEksepsi_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Perguruan Tinggi">
                                    <ItemTemplate>
                                        <%# Eval("nama") %><br />
                                        <%# Eval("kd_perguruan_tinggi") %> - <%# Eval("nama_institusi") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Skema">
                                    <ItemTemplate>
                                        <%# Eval("nama_skema") %><br />
                                        <asp:Label ID="lblStsPengusul" runat="server"
                                            CssClass='<%# (Eval("kd_sts_pengusul").ToString() == "0") ? 
                                                            "badge badge-success" : "badge badge-primary" %>'
                                            Text='<%# Bind("sts_pengusul") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tahapan">
                                    <ItemTemplate>
                                        <%# Eval("tahapan") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tanggal Mulai">
                                    <ItemTemplate>
                                        <%# Eval("tgl_mulai") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tanggal Berakhir">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTglBerakhir" runat="server" Text='<%# Bind("tgl_berakhir") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary waves-effect waves-light" CommandName="update"
                                            CommandArgument="<%# Container.DataItemIndex %>" ToolTip="edit">
                                            <i class="fa fa-edit"></i>
                                        </asp:LinkButton>
                                        <asp:Label runat="server" ID="lblStatus" CssClass="badge badge-danger">Tidak Aktif</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="col-sm-12">
                                    <p class="text-primary">Data tidak ditemukan</p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="vIsiEksepsiDosen" runat="server">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading bg-default txt-white">
                                        Identitas Pengusul Dosen
                                    </div>
                                    <div class="form-group row" style="padding: 10px 0px 0px 10px">
                                        <div class="col-md-1">
                                            <label class="control-label">NIDN</label>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="tbCari" runat="server" CssClass="form-control" placeholder="NIDN" aria-describedby="btn-addon2" MaxLength="10"></asp:TextBox>
                                                <span class="input-group-btn" id="btn-addon2">
                                                    <asp:LinkButton ID="lbCari" runat="server" class="btn btn-warning addon-btn waves-effect waves-light"
                                                        OnClick="lbCari_Click"><i class="fa fa-search"></i>
                                                    </asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="general-info">
                                            <div class="row" style="padding-left: 10px;">
                                                <div class="col-xl-10 col-lg-10" style="padding-top: 20px;">
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
                                                            <label for="lblSkema" class="col-sm-2 control-label">Skema</label>
                                                            <div class="col-sm-10">
                                                                <asp:DropDownList runat="server" ID="ddlSkema" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblTahapan" class="col-sm-2 control-label">Tahapan</label>
                                                            <div class="col-sm-10">
                                                                <asp:DropDownList runat="server" ID="ddlTahapan" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblTglMulai" class="col-sm-2 control-label">Tgl. Mulai</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox runat="server" ID="tbTglMulai" CssClass="form-control" Type="date" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblTglBerakhir" class="col-sm-2 control-label">Tgl. Berakhir</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox runat="server" ID="tbTglBerakhir" Type="date" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="text-align: center; padding: 20px 0px 20px 0px;">
                                            <asp:LinkButton ID="lbSimpan" runat="server" type="submit" class="btn btn-success waves-effect waves-light" OnClick="lbSimpan_Click">Simpan</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading bg-default txt-white">
                                        Identitas Pengusul Tendik
                                    </div>
                                    <div class="form-group row" style="padding: 10px 0px 0px 10px">
                                        <div class="col-md-1">
                                            <label class="control-label">NIDN</label>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="tbCariNoKTP" runat="server" CssClass="form-control" placeholder="Nomor KTP" aria-describedby="btn-addon2" MaxLength="16"></asp:TextBox>
                                                <span class="input-group-btn" id="btn-addon3">
                                                    <asp:LinkButton ID="lbCariNoKTP" runat="server" class="btn btn-warning addon-btn waves-effect waves-light"
                                                        OnClick="lbCariNoKTP_Click"><i class="fa fa-search"></i>
                                                    </asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="general-info">
                                            <div class="row" style="padding-left: 10px;">
                                                <div class="col-xl-10 col-lg-10" style="padding-top: 20px;">
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblNamaTendik" class="col-sm-2 control-label">Nama</label>
                                                            <div class="col-sm-10">
                                                                <asp:Label runat="server" ID="lblNamaTendik" Text="-"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblInstitusiTendik" class="col-sm-2 control-label">Institusi</label>
                                                            <div class="col-sm-10">
                                                                <asp:Label runat="server" ID="lblInstitusiTendik" Text="-"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblSkemaTendik" class="col-sm-2 control-label">Skema</label>
                                                            <div class="col-sm-10">
                                                                <asp:DropDownList runat="server" ID="ddlSkemaTendik" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblTahapanTendik" class="col-sm-2 control-label">Tahapan</label>
                                                            <div class="col-sm-10">
                                                                <asp:DropDownList runat="server" ID="ddlTahapanTendik" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblTglMulaiTendik" class="col-sm-2 control-label">Tgl. Mulai</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox runat="server" ID="tbTglMulaiTendik" CssClass="form-control" Type="date" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblTglBerakhirTendik" class="col-sm-2 control-label">Tgl. Berakhir</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox runat="server" ID="tbTglBerakhirTendik" Type="date" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="text-align: center; padding: 20px 0px 20px 0px;">
                                            <asp:LinkButton ID="lbSimpanTendik" runat="server" type="submit" class="btn btn-success waves-effect waves-light" OnClick="lbSimpanTendik_Click">Simpan</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vUpdateEksepsi" runat="server">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading bg-default txt-white">
                                        Identitas Pengusul
                                    </div>
                                    <%--<div class="form-group row" style="padding: 10px 0px 0px 10px">
                                        <div class="col-md-2">
                                            <label class="control-label">NIDN/Nomor KTP</label>
                                            <asp:Label ID="lblNIDNKTP" runat="server"></asp:Label>
                                        </div>
                                    </div>--%>
                                    <div class="panel-body">
                                        <div class="general-info">
                                            <div class="row" style="padding-left: 10px;">
                                                <div class="col-xl-10 col-lg-10" style="padding-top: 20px;">
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblNamaEdit" class="col-sm-3 control-label">Nama</label>
                                                            <div class="col-sm-9">
                                                                <asp:Label runat="server" ID="lblNamaEdit" Text="-"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="lblInstitusi" class="col-sm-3 control-label">Institusi</label>
                                                            <div class="col-sm-9">
                                                                <asp:Label runat="server" ID="lblInstitusiEdit" Text="-"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                   <%-- <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="ddlThnUsulanEdit" class="col-sm-3 control-label">Tahun Usulan</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList runat="server" ID="ddlThnUsulanEdit" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="ddlThnPelaksanaanEdit" class="col-sm-3 control-label">Tahun Pelaksanaan</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList runat="server" ID="ddlThnPelaksanaanEdit" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="ddlSkemaEdit" class="col-sm-3 control-label">Skema</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList runat="server" ID="ddlSkemaEdit" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="ddlTahapanEdit" class="col-sm-3 control-label">Tahapan</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList runat="server" ID="ddlTahapanEdit" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="tbtglMulaiEdit" class="col-sm-3 control-label">Tgl. Mulai</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox runat="server" ID="tbtglMulaiEdit" CssClass="form-control" Type="date"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="tbTglBerakhirEdit" class="col-sm-3 control-label">Tgl. Berakhir</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox runat="server" ID="tbTglBerakhirEdit" Type="date" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="row col-sm-12" style="padding-bottom: 5px;">
                                                            <label for="ddlStatus" class="col-sm-3 control-label">Status Aktif</label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="text-align: center; padding: 20px 0px 20px 0px;">
                                            <asp:LinkButton ID="lbSimpanEdit" runat="server" type="submit" class="btn btn-success waves-effect waves-light" OnClick="lbSimpanEdit_Click">Update</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
</div>
