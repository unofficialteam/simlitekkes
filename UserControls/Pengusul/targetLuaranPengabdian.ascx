<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="targetLuaranPengabdian.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.targetLuaranPengabdian" %>

<div class="form-row col-sm-12">
    <div class="form-group col-lg-12" style="text-align: right;">
        <asp:Label ID="lblSkema" runat="server" Text="Terapan"></asp:Label>
        (Tahun ke
                <asp:Label ID="lblUrutanUsulan" runat="server" Text="0"></asp:Label>
        dari
                        <asp:Label ID="lblLamaUsulan" runat="server" Text="0"></asp:Label>
        tahun )                
    </div>
</div>



<asp:MultiView ID="mvLuaran" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarLuaranWajib" runat="server">

        <%--<div class="card">
            <div class="card-header">
                <h5 class="card-header-text">Luaran dan Target Capaian</h5>
            </div>
        </div>--%>

        <div class="col-sm-12" style="padding-bottom: 10px;">
            <div class="panel panel-default" style="min-height: 300px;">
                <div class="panel-heading bg-default txt-white" style="font-size: medium">
                    <b>Luaran dan Target Capaian</b>

                    <%--<asp:Label ID="lblStsAnggotaDikti2" runat="server" Text="0"></asp:Label>
                    <asp:Label ID="lblJmlAnggotaPengusul" runat="server" CssClass="badge badge-md bg-danger" Enabled="false" Text="0">
                    </asp:Label>--%>
                </div>

                <div class="card">
                    <div class="card-header">
                        <div class="row boot-ui">
                            <div class="col-xs-6 col-md-4 col-sm-4 waves-effect waves-light" style="font-size: medium">
                                <div class="grid-material bg-success"><b>Luaran Wajib</b></div>
                                <asp:Label ID="Label1" runat="server" Text="luaran wajib sudah lengkap" class="label label-success" Font-Size="Small"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text="luaran wajib belum lengkap" class="label label-danger" Font-Size="Small"></asp:Label>

                            </div>
                            <div class="col-xs-6 col-md-4 col-sm-4 waves-effect waves-light" style="font-size: medium">
                                <div style="text-align: left;">
                                    <%--<asp:Label ID="Label3" runat="server" CssClass="fa fa-question-circle" Font-Size="50px" ForeColor="green"></asp:Label>
                                    --%><asp:LinkButton ID="lbinfo" runat="server" ToolTip="Informasi"
                                        CssClass="fa fa-question-circle" OnClick="lbinfo_Click" Font-Size="XX-Large"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <%--<asp:LinkButton ID="lbtambahwajib" runat="server" OnClick="lbtambahwajib_Click" class="btn btn-primary waves-effect waves-light f-right">
                    <i class="icofont icofont-edit"></i>
                    </asp:LinkButton>--%>

                        <div class="card-block">
                            <div class="row">
                                <div class="col-sm-12 table-responsive">
                                    <asp:GridView ID="gvluaranwajib" runat="server" GridLines="None"
                                        CssClass="table table-hover" Font-Size="Small"
                                        DataKeyNames="id_luaran_dijanjikan,tahun_ke,nama_target_capaian_luaran,id_jenis_luaran,id_kategori"
                                        ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowDataBound="gvluaranwajib_RowDataBound" OnRowCommand="gvluaranwajib_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblTahunke" runat="server" Text='<%# Bind("tahun_ke") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkategori" runat="server" ForeColor="Blue" Font-Italic="true" Text='<%# Bind("kategori") %>'></asp:Label><br />
                                                    <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label>
                                                    <asp:Label ID="lbltargetluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_target_capaian_luaran") %>' BackColor="Yellow"></asp:Label><br />
                                                    <asp:Label ID="lblketerangan" runat="server" Text='<%# Bind("keterangan") %>' Font-Italic="True"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbTambah" runat="server" CommandName="tambah"
                                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Tambah Luaran"
                                                        CssClass="fa fa-plus" Font-Size="XX-Large">
                                                    </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbHapus" runat="server" CommandName="hapus"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="hapus"
                                                            CssClass="fa fa-trash-o" Font-Size="XX-Large">
                                                        </asp:LinkButton>&nbsp;
                                                        
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
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
                    <%--</div>


                <div class="card">--%>
                    <div class="card-header">
                        <div class="row boot-ui">
                            <div class="col-xs-6 col-md-4 col-sm-4 waves-effect waves-light" style="font-size: medium">
                                <div class="grid-material bg-success"><b>Luaran Tambahan</b></div>
                            </div>
                            <asp:LinkButton ID="lbtambahtambahan" runat="server" OnClick="lbtambahtambahan_Click" class="btn btn-primary waves-effect waves-light f-right" Font-Size="Large" ToolTip="Tambah Luaran Tambahan">
                    <i class="icofont icofont-plus"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="card-block">
                            <div class="row">
                                <div class="col-sm-12 table-responsive">

                                    <asp:GridView ID="gvluarantambahan" runat="server" GridLines="None"
                                        CssClass="table table-hover" Font-Size="Small"
                                        DataKeyNames="id_luaran_dijanjikan,tahun_ke,nama_target_capaian_luaran"
                                        ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnPreRender="gvluarantambahan_PreRender" OnRowDataBound="gvluarantambahan_RowDataBound" OnRowCommand="gvluarantambahan_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTahunke" runat="server" Text='<%# Bind("tahun_ke") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label>
                                                    <asp:Label ID="lbltargetluaran" runat="server" Font-Bold="true" BackColor="Yellow" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label><br />
                                                    <asp:Label ID="lblketerangan" runat="server" Font-Italic="true" Text='<%# Bind("keterangan") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbHapus" runat="server" CommandName="hapus"
                                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus Luaran"
                                                        CssClass="fa fa-trash-o" Font-Size="XX-Large">
                                                    </asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
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
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vTambahLuaran" runat="server">
        <%--        <div class="card">
            <div class="card-header">
                <h5 class="card-header-text">Luaran dan Target Capaian</h5>
            </div>
        </div>--%>
        <div class="card">
            <div class="card-header">
                <div class="row boot-ui">
                    <div class="col-xs-6 col-md-4 col-sm-4 waves-effect waves-light">
                        <div class="grid-material bg-success">
                            <asp:Label ID="lbKet" runat="server" Font-Bold="true" Text="Tambah Data Luaran Wajib" Style="font-size: medium"></asp:Label>
                        </div>
                    </div>

                </div>
                <div class="card-block">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <label for="ddlThn" class="col-sm-2 control-label">Tahun</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlThn" AutoPostBack="true" runat="server" Enabled="true" ClientIDMode="Static"
                                        CssClass="form-control select2" OnSelectedIndexChanged="ddlThn_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <label for="ddlJenisLuaran" class="col-sm-2 control-label">Jenis Luaran</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlJenisLuaran" AutoPostBack="true" runat="server" Enabled="true" ClientIDMode="Static"
                                        CssClass="form-control select2" OnSelectedIndexChanged="ddlJenisLuaran_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <label for="ddlTarget" class="col-sm-2 control-label">Target</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlTarget" runat="server" Enabled="true" ClientIDMode="Static"
                                        CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>



                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <asp:Label ID="lbketJanjiLuaran" runat="server" Text="Nama Konferensi" class="col-sm-2 control-label"></asp:Label>
                                <%--<label for="tbKetLuaran" class="col-sm-2 control-label" title="Nama Konferensi"><i style="color: red">*</i></label>--%>
                                <div class="col-sm-10">
                                    <asp:TextBox runat="server" ID="tbKetLuaran" placeholder=""
                                        CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <asp:Label ID="lbketJanjiLuaran2" runat="server" Text="Nama Konferensi" Visible="false" class="col-sm-2 control-label"></asp:Label>
                                <%--<label for="tbKetLuaran" class="col-sm-2 control-label" title="Nama Konferensi"><i style="color: red">*</i></label>--%>
                                <div class="col-sm-10">
                                    <asp:TextBox runat="server" Visible="false" ID="tbKetLuaran2" placeholder=""
                                        CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row col-sm-12" style="padding-bottom: 5px;">
                                <asp:Label ID="lbketJanjiLuaran3" runat="server" Visible="false" Text="Nama Konferensi" class="col-sm-2 control-label"></asp:Label>
                                <%--<label for="tbKetLuaran" class="col-sm-2 control-label" title="Nama Konferensi"><i style="color: red">*</i></label>--%>
                                <div class="col-sm-10">
                                    <asp:TextBox runat="server" Visible="false" ID="tbKetLuaran3" placeholder=""
                                        CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="text-center">
                            <asp:LinkButton ID="lbSimpan" runat="server" Text="Simpan" OnClick="lbSimpan_click" class="btn btn-primary waves-effect waves-light m-r-20"></asp:LinkButton>
                            <asp:LinkButton ID="lbBatal" runat="server" Text="Batal" OnClick="lbBatal_click" class="btn btn-default waves-effect"></asp:LinkButton>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </asp:View>
</asp:MultiView>

<div class="modal modal-warning" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel">Konfirmasi</h4>
            </div>
            <div class="modal-body">
                Data akan dihapus, anda yakin&nbsp; 
                        <asp:Label runat="server" ID="lblHapus" Text="..." ForeColor="Green"></asp:Label>
                ?
            </div>
            <div class="modal-footer">
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapus_Click" OnClientClick="$('#myModal').modal('hide');">Hapus</asp:LinkButton>
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>

<div class="modal modal-warning" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel1">Informasi</h4>
            </div>
            <div class="modal-body">
                Informasi Luaran&nbsp; 
                        <%--<asp:Label runat="server" ID="Label4" Text="..." ForeColor="Green"></asp:Label>
                        --%>

                <div class="card-header">
                    <div class="row boot-ui">
                        <%--<div class="col-xs-6 col-md-4 col-sm-4 waves-effect waves-light" style="font-size: medium">
                            <div class="grid-material bg-success"><b>Luaran Tambahan</b></div>
                        </div>--%>
                    </div>
                    <div class="card-block">
                        <div class="row">
                            <div class="col-sm-12 table-responsive">

                                <asp:GridView ID="GvInfoLuaran" runat="server" GridLines="None"
                                    CssClass="table table-hover"
                                    ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                Tahun ke
                                                <asp:Label ID="lblTahunke" runat="server" Text='<%# Bind("tahun_ke") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                Luaran ke
                                                <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("kelompok") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("informasi") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
            </div>
            <div class="modal-footer">
                <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapus_Click" OnClientClick="$('#myModal').modal('hide');">Hapus</asp:LinkButton>--%>
                <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
            </div>
        </div>
    </div>
</div>
