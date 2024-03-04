<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mitraPenelitian.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.mitraPenelitian" %>

<%--<style>
    /*.accordion-title > a:before {
        float: left !important;
        font-family: FontAwesome;
        content: "\f068";
        padding-right: 8px;
    }

    .accordion-title > a.collapsed:before {
        float: left !important;
        content: "\f067";
    }*/
</style>--%>


<div class="col-sm-12">
    <div class="panel panel-default m-t-20">
        <div class="form-row col-sm-12">
            <div class="form-group col-lg-12" style="text-align: right; padding-top: 20px; te">
                <%--<b>--%>
                <asp:Label ID="lblSkema" runat="server" Text="Terapan"></asp:Label>
                (Tahun ke
                <asp:Label ID="lblUrutanUsulan" runat="server" Text="0"></asp:Label>
                dari
                        <asp:Label ID="lblLamaUsulan" runat="server" Text="0"></asp:Label>
                tahun)
            </div>
        </div>
    </div>
</div>

<asp:MultiView ID="mvMitra" runat="server" ActiveViewIndex="0">
    <asp:View ID="vMitraPelaksanaPenelitian" runat="server">
        <div class="col-sm-12" style="padding-bottom: 10px;">
            <%--<div class="panel panel-default">
                <div class="panel-heading bg-default txt-white">--%>
            <asp:LinkButton ID="lbInfoMitra" runat="server" CssClass="fa fa-info-circle"
                Font-Bold="true" Font-Italic="true" OnClick="lbInfoMitra_Click" Font-Size="XX-Large"></asp:LinkButton>&nbsp;&nbsp;Dokumen Pendukung 
        </div>
        <br />
        <asp:Panel runat="server" ID="panelSuratPernyataan" Visible="false">
            <div class="row">
                <div class="col-sm-12">
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="lbUnduhTemplateDok2" Text="" Font-Bold="true"
                                    OnClick="lbUnduhTemplateDok_Click">
                            <i class="fa fa-file-word-o" style="font-size: 40px; color: blue;"></i>
                                </asp:LinkButton>

                            </td>
                            <td>
                                <div><b>Template Surat Pernyataan Mitra</b></div>
                                <asp:LinkButton runat="server" ID="lbUnduhTemplateDok" Text="Unduh" Font-Bold="true" OnClick="lbUnduhTemplateDok_Click"></asp:LinkButton>

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <br />
        <asp:Label ID="lblInfoMitra" runat="server" Text="Pilihan Skema tidak Memiliki Mitra" Font-Size="Large" Font-Bold="true" ForeColor="Orange" Visible="false"></asp:Label>
        <asp:Panel ID="panelMitraPelaksana" runat="server" Visible="true">
            <%--<div class="col-lg-12">--%>
            <div class="card mb-4">
                <div class="card-header">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <h5>Mitra Pelaksana Penelitian                                                    
                                                <asp:Label ID="lblTdkWajibMitra" runat="server" Text="Tidak Wajib Ada" Font-Bold="true" Font-Size="X-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                            </h5>
                        </div>
                        <div class="text-right">
                            <h5 class="card-header-text f-right" style="color: darkred">
                                <asp:LinkButton ID="lbTambahMitraPelaksanaPenelitian" runat="server" Font-Size="Large" OnClick="lbTambahMitraPelaksanaPenelitian_Click"><i class="fa fa-plus-circle"></i>&nbsp;Tambah</asp:LinkButton>
                            </h5>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvMitraPelaksanaPenelitian" runat="server" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False" DataKeyNames="id_mitra, nama_mitra" OnRowDeleting="gvMitraPelaksanaPenelitian_RowDeleting"
                        OnRowUpdating="gvMitraPelaksanaPenelitian_RowUpdating" OnRowDataBound="gvMitraPelaksanaPenelitian_RowDataBound"
                        OnRowCommand="gvMitraPelaksanaPenelitian_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mitra">
                                <ItemTemplate>
                                    <b>
                                        <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("nama_mitra") %>' ForeColor="DarkBlue"></asp:Label></b>  -  
                                                                <asp:Label ID="lblSurel" runat="server" Text='<%# Bind("surel") %>'></asp:Label><br>
                                    <asp:Label ID="lblInstitusiMitraPenelitian" runat="server" Text='<%# Bind("nama_institusi_mitra") %>'></asp:Label><br />
                                    <asp:LinkButton runat="server" ID="lbUnggahDokMitraPelaksanaPenelitian"
                                        class="btn btn-primary" data-toggle="tooltip" CommandName="unggahDokMitraPenelitian"
                                        CommandArgument="<%# Container.DataItemIndex %>"><i class="hvr-buzz-out fas fa-upload"></i>
                                    </asp:LinkButton>
                                    Tgl.Unggah Data:
                                                                    <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'>
                                                                    </asp:Label><br />
                                    <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPelaksana"
                                        CommandArgument="<%# Container.DataItemIndex %>">Unduh</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="68%" />
                                <ItemStyle Font-Bold="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                <ItemTemplate>
                                    <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:Rp"></asp:Label>
                                    <asp:Label ID="lblDana1" runat="server" Text='<%# Bind("dana_thn_1") %>'></asp:Label><br>
                                    <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:Rp"></asp:Label>
                                    <asp:Label ID="lblDana2" runat="server" Text='<%# Bind("dana_thn_2") %>'></asp:Label><br>
                                    <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:Rp"></asp:Label>
                                    <asp:Label ID="lblDana3" runat="server" Text='<%# Bind("dana_thn_3") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbUbah" runat="server" CommandName="update" Font-Bold="true" ForeColor="Black"
                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Ubah"
                                        CssClass="fa fa-edit" Font-Size="XX-Large">
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbHapus" runat="server" CommandName="delete" Font-Bold="true" ForeColor="Black"
                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                        CssClass="fas fa-trash-alt" Font-Size="XX-Large">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
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
        </asp:Panel>
        <%--</div>
        </div>--%>

        <asp:Panel ID="panelMitraCalonPengguna" runat="server" Visible="true">
            <%--<div class="col-lg-12">--%>
            <div class="card mb-4">
                <div class="card-header">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <h5>Mitra Calon Pengguna
                                                    <asp:Label ID="lblWajibMCP" runat="server" Text="Wajib Ada" Font-Size="X-Small" Font-Bold="true" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                            </h5>
                        </div>
                        <div class="text-right">
                            <h5 class="card-header-text f-right" style="color: darkred">
                                <asp:LinkButton ID="lbTambahMitraCalonPengguna" runat="server" Font-Size="Large" OnClick="lbTambahMitraCalonPengguna_Click"><i class="fa fa-plus-circle"></i>&nbsp;Tambah</asp:LinkButton>
                            </h5>
                        </div>
                    </div>
                </div>

                <div class="card-body">

                    <asp:GridView ID="gvMitraCalonPengguna" runat="server" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False" DataKeyNames="id_mitra, nama_mitra" OnRowDeleting="gvMitraCalonPengguna_RowDeleting" OnRowUpdating="gvMitraCalonPengguna_RowUpdating"
                        OnRowDataBound="gvMitraCalonPengguna_RowDataBound" OnRowCommand="gvMitraCalonPengguna_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mitra">
                                <ItemTemplate>
                                    <b>
                                        <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("nama_mitra") %>' ForeColor="DarkBlue"></asp:Label></b>  -
                                                                <asp:Label ID="lblSurel" runat="server" Text='<%# Bind("surel") %>'></asp:Label><br>
                                    <asp:Label ID="lblInstitusiMitraPenelitian" runat="server" Text='<%# Bind("nama_institusi_mitra") %>'></asp:Label><br />
                                    <asp:LinkButton runat="server" ID="lbUnggahDokMitraPelaksanaPenelitian"
                                        class="btn btn-primary" data-toggle="tooltip" CommandName="unggahDokMitraPenelitian"
                                        CommandArgument="<%# Container.DataItemIndex %>"><i class="hvr-buzz-out fas fa-upload"></i>
                                    </asp:LinkButton>
                                    Tgl.Unggah Data:
                                                                    <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label><br />
                                    <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPelaksana"
                                        CommandArgument="<%# Container.DataItemIndex %>">Unduh</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="68%" />
                                <ItemStyle Font-Bold="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                <ItemTemplate>
                                    <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:Rp"></asp:Label>
                                    <asp:Label ID="lblDana1" runat="server" Text='<%# Bind("dana_thn_1") %>'></asp:Label><br>
                                    <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:Rp"></asp:Label>
                                    <asp:Label ID="lblDana2" runat="server" Text='<%# Bind("dana_thn_2") %>'></asp:Label><br>
                                    <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:Rp"></asp:Label>
                                    <asp:Label ID="lblDana3" runat="server" Text='<%# Bind("dana_thn_3") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbUbah" runat="server" CommandName="update" Font-Bold="true" ForeColor="Black"
                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Ubah"
                                        CssClass="fa fa-edit" Font-Size="XX-Large">
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbHapus" runat="server" CommandName="delete" Font-Bold="true" ForeColor="Black"
                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                        CssClass="fas fa-trash-alt" Font-Size="XX-Large">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
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
            <%--</div>--%>
        </asp:Panel>
        <asp:Panel ID="panelMitraInvestor" runat="server" Visible="true">
            <%--<div class="col-lg-12">--%>
            <div class="card mb-4">
                <div class="card-header">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <h5>Mitra Investor
                                <asp:Label ID="lblWajibMI" runat="server" Text="Wajib Ada" Font-Size="XX-Small" ForeColor="Red" Font-Italic="true" BackColor="Yellow" Visible="true"></asp:Label>
                            </h5>
                        </div>
                        <div class="text-right">
                            <h5 class="card-header-text f-right" style="color: darkred">
                                <asp:LinkButton ID="lbTambahMitraInvestor" runat="server" Font-Size="Large" OnClick="lbTambahMitraInvestor_Click"><i class="fa fa-plus-circle"></i>&nbsp;Tambah</asp:LinkButton>
                            </h5>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvMitraInvestor" runat="server" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true"
                        AutoGenerateColumns="False" DataKeyNames="id_mitra, nama_mitra" OnRowDeleting="gvMitraInvestor_RowDeleting"
                        OnRowUpdating="gvMitraInvestor_RowUpdating" OnRowDataBound="gvMitraInvestor_RowDataBound" OnRowCommand="gvMitraInvestor_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbnNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mitra">
                                <ItemTemplate>
                                    <b>
                                        <asp:Label ID="lblNamaMitraPenelitian" runat="server" Text='<%# Bind("nama_mitra") %>' ForeColor="DarkBlue"></asp:Label></b> -
                                                                <asp:Label ID="lblSurel" runat="server" Text='<%# Bind("surel") %>'></asp:Label><br>
                                    <asp:Label ID="lblInstitusiMitraPenelitian" runat="server" Text='<%# Bind("nama_institusi_mitra") %>'></asp:Label><br />
                                    <asp:LinkButton runat="server" ID="lbUnggahDokMitraPelaksanaPenelitian"
                                        class="btn btn-primary" data-toggle="tooltip" CommandName="unggahDokMitraPenelitian"
                                        CommandArgument="<%# Container.DataItemIndex %>"><i class="hvr-buzz-out fas fa-upload"></i>
                                    </asp:LinkButton>
                                    Tgl.Unggah Data:
                                                                    <asp:Label ID="lblTglUnggah" runat="server" Text='<%# ConvertDateTimeToDate(Eval("tgl_unggah_pernyataan").ToString(), "dd MMMM yyyy", "id-ID") %>'></asp:Label><br />
                                    <asp:LinkButton ID="lbUnduh" runat="server" CommandName="unduhDokumenMitraPelaksana"
                                        CommandArgument="<%# Container.DataItemIndex %>">Unduh</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="68%" />
                                <ItemStyle Font-Bold="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kontribusi Pendanaan">
                                <ItemTemplate>
                                    <asp:Label ID="lblDanaThn1" runat="server" Text="Tahun 1:Rp"></asp:Label>
                                    <asp:Label ID="lblDana1" runat="server" Text='<%# Bind("dana_thn_1") %>'></asp:Label><br>
                                    <asp:Label ID="lblDanaThn2" runat="server" Text="Tahun 2:Rp"></asp:Label>
                                    <asp:Label ID="lblDana2" runat="server" Text='<%# Bind("dana_thn_2") %>'></asp:Label><br>
                                    <asp:Label ID="lblDanaThn3" runat="server" Text="Tahun 3:Rp"></asp:Label>
                                    <asp:Label ID="lblDana3" runat="server" Text='<%# Bind("dana_thn_3") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbUbah" runat="server" CommandName="update" Font-Bold="true" ForeColor="Black"
                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Ubah"
                                        CssClass="fa fa-edit" Font-Size="XX-Large">
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbHapus" runat="server" CommandName="delete" Font-Bold="true" ForeColor="Black"
                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Hapus"
                                        CssClass="fas fa-trash-alt" Font-Size="XX-Large">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
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
            <%--</div>--%>
        </asp:Panel>
    </asp:View>
    <asp:View ID="vMitraPelaksana" runat="server">
        <div class="card mb-4">
            <div class="card-header">

                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h5>Mitra Pelaksana Penelitian
                        </h5>
                    </div>
                    <div class="text-right">
                        <h5 class="card-header-text f-right" style="color: darkred"></h5>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="view-info">
                    <div class="form-group row">
                        <label class="col-sm-2">Nama Mitra</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbNamaMitraPelaksana" runat="server" CssClass="form-control" placeholder="Isikan nama PIC mitra pelaksana penelitian"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2">Alamat Surel</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbAlamatSurelMitraPelaksana" runat="server" CssClass="form-control" placeholder="Isikan alamat surel PIC mitra pelaksana penelitian"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2">Institusi</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbInstitusiMitraPelaksana" runat="server" CssClass="form-control" placeholder="Isikan nama institusi mitra"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2">Alamat Institusi</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbAlamatInstitusi" runat="server" CssClass="form-control" placeholder="Isikan alamat surat institusi mitra" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2">Negara</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlNegara" runat="server" Enabled="true" ClientIDMode="Static"
                                CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card mb-4">
            <div class="card-header">
                <h5 class="card-header-text">Kontribusi Pendanaan (Jika Ada)</h5>
            </div>
            <div class="card-body">
                <div class="view-info">
                    <div class="form-group row">
                        <asp:Label ID="lblPendanaanThn1" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 1"></asp:Label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbPendanaanThn1" runat="server" CssClass="form-control uang1" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <asp:Label ID="lblPendanaanThn2" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 2"></asp:Label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbPendanaanThn2" runat="server" CssClass="form-control uang2" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <asp:Label ID="lblPendanaanThn3" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 3"></asp:Label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbPendanaanThn3" runat="server" CssClass="form-control uang3" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row m-t-30">
            <div class="col-sm-12 text-right">
                <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-success waves-effect waves-light"
                    Text="Simpan" OnClick="btnSimpan_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnBatal" runat="server" CssClass="btn btn-danger waves-effect waves-light"
                    Text="Batal" OnClick="btnBatal_Click"></asp:Button>
            </div>
        </div>
        <script type="text/javascript">
            new AutoNumeric('.uang1', { decimalPlaces: 0 });
            new AutoNumeric('.uang2', { decimalPlaces: 0 });
            new AutoNumeric('.uang3', { decimalPlaces: 0 });
        </script>
    </asp:View>
    <asp:View ID="vMitraCalonPengguna" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <h5 class="card-header-text">Mitra Calon Pengguna</h5>
            </div>
            <div class="card-body">
                <div class="view-info">
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Nama Mitra</label>
                        <div class="col-sm-4 col-xs-12">
                            <asp:TextBox ID="tbNamaMCPengguna" runat="server" CssClass="form-control" placeholder="Isikan nama PIC mitra calon pengguna"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Alamat Surel</label>
                        <div class="col-sm-4 col-xs-12">
                            <asp:TextBox ID="tbAlamatSurelMCP" runat="server" CssClass="form-control" placeholder="Isikan alamat surel PIC mitra calon pengguna"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Institusi</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbInstitusiMCP" runat="server" CssClass="form-control" placeholder="Isikan nama institusi mitra"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Alamat Institusi</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbAlamatInstitusiMCP" runat="server" CssClass="form-control" placeholder="Isikan alamat surat institusi mitra" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Negara</label>
                        <div class="col-sm-4 col-xs-12">
                            <asp:DropDownList ID="ddlNegaraMCP" runat="server" Enabled="true" ClientIDMode="Static"
                                CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card mb-4">
            <div class="card-header">
                <h5 class="card-header-text">Kontribusi Pendanaan (Jika Ada)</h5>
            </div>
            <div class="card-body">
                <div class="view-info">
                    <div class="form-group row">
                        <asp:Label ID="lblDanaMCPThn1" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 1"></asp:Label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbDanaMCPThn1" runat="server" CssClass="form-control uang1" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <asp:Label ID="lblDanaMCPThn2" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 2"></asp:Label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbDanaMCPThn2" runat="server" CssClass="form-control uang2" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <asp:Label ID="lblDanaMCPThn3" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 3"></asp:Label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbDanaMCPThn3" runat="server" CssClass="form-control uang3" ClientIDMode="Static"></asp:TextBox>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row m-t-30">
            <div class="col-sm-12 text-right">
                <asp:Button ID="btnSimpanMCP" runat="server" CssClass="btn btn-success waves-effect waves-light"
                    Text="Simpan" OnClick="btnSimpanMCP_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnBatalMCP" runat="server" CssClass="btn btn-danger waves-effect waves-light"
                    Text="Batal" OnClick="btnBatalMCP_Click"></asp:Button>
            </div>
        </div>
        <script type="text/javascript">
            new AutoNumeric('.uang1', { decimalPlaces: 0 });
            new AutoNumeric('.uang2', { decimalPlaces: 0 });
            new AutoNumeric('.uang3', { decimalPlaces: 0 });
        </script>
    </asp:View>
    <asp:View ID="vMitraInvestor" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <h5 class="card-header-text">Mitra Investor</h5>
            </div>
            <div class="card-body">
                <div class="view-info">
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Nama Mitra</label>
                        <div class="col-sm-4 col-xs-12">
                            <asp:TextBox ID="tbNamaMI" runat="server" CssClass="form-control" placeholder="Isikan nama PIC mitra investor"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Alamat Surel</label>
                        <div class="col-sm-4 col-xs-12">
                            <asp:TextBox ID="tbAlamatSurelMI" runat="server" CssClass="form-control" placeholder="Isikan alamat surel PIC mitra investor"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Institusi</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbInstitusiMI" runat="server" CssClass="form-control" placeholder="Isikan nama institusi mitra"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Alamat Institusi</label>
                        <div class="col-sm-10 col-xs-12">
                            <asp:TextBox ID="tbAlamatInstitusiMI" runat="server" CssClass="form-control" placeholder="Isikan alamat surat institusi mitra" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-2 col-form-label form-control-label">Negara</label>
                        <div class="col-sm-4 col-xs-12">
                            <asp:DropDownList ID="ddlNegaraMI" runat="server" Enabled="true" ClientIDMode="Static"
                                CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card mb-4">
            <div class="card-header">
                <h5 class="card-header-text">Kontribusi Pendanaan (Jika Ada)</h5>
            </div>
            <div class="card-body">
                <div class="view-info">
                    <div class="form-group row">
                        <asp:Label ID="lblDanaMIThn1" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 1"></asp:Label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbDanaMIThn1" runat="server" CssClass="form-control uang1" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <asp:Label ID="lblDanaMIThn2" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 2"></asp:Label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbDanaMIThn2" runat="server" CssClass="form-control uang2" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <asp:Label ID="lblDanaMIThn3" runat="server" CssClass="col-xs-1 col-form-label form-control-label" Text="Tahun 3"></asp:Label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="tbDanaMIThn3" runat="server" CssClass="form-control uang3" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row m-t-30">
            <div class="col-sm-12 text-right">
                <asp:Button ID="btnSimpanMI" runat="server" CssClass="btn btn-success waves-effect waves-light"
                    Text="Simpan" OnClick="btnSimpanMI_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnBataMI" runat="server" CssClass="btn btn-danger waves-effect waves-light"
                    Text="Batal" OnClick="btnBataMI_Click"></asp:Button>
            </div>
        </div>
        <script type="text/javascript">
            new AutoNumeric('.uang1', { decimalPlaces: 0 });
            new AutoNumeric('.uang2', { decimalPlaces: 0 });
            new AutoNumeric('.uang3', { decimalPlaces: 0 });
        </script>
    </asp:View>
</asp:MultiView>




                    <asp:Panel runat="server" ID="panelUnggahDokAkreditasiTPPTPM"  >
                        <div class="row">
                            <div class="col-lg-7">
                                <table style="margin-top: 25px;">
                                    <tr>
                                        <td>
                                            <asp:LinkButton runat="server" ID="lbUnduhDokTpp" Text="" ForeColor="Red" OnClick="lbUnduhDokTpp_Click">
                                            <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 70px; "></i>
                                            </asp:LinkButton><br />
                                        </td>
                                        <td>
                                            <div>Unggah dokumen hasil akreditasi Prodi TPP</div>
                                            <div>Tgl. Unggah:&nbsp;<asp:Label runat="server" ID="lblTglUnggahTpp" Text="-"></asp:Label></div>
                                            <div>Ukuran file:&nbsp;<asp:Label runat="server" ID="lblUkuranFileTpp" Text="-"></asp:Label></div>
                                            <div>
                                                <div style="padding: 10px;">
                                                    <div>
                                                        <div class="input-group input-group-button input-group-primary">
                                                            <asp:FileUpload runat="server" ID="fileUploadTpp" CssClass="form-control" />
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton runat="server" ID="lbUnggahDokumenTpp" CssClass="btn btn-info"
                                                                    OnClick="lbUnggahDokumenTpp_Click">
                                                                        <i class="fa fa-cloud-upload">&nbsp;Unggah</i></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblInfoDokTpp" Text="" ForeColor="Red" Font-Bold="true"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">Format file PDF dengan ukuran maks 2MB
                                                </span>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-lg-5">
                                <asp:Label runat="server" ID="lblErrorDokTpp" Text="" ForeColor="Red"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-7">
                                <table style="margin-top: 25px;">
                                    <tr>
                                        <td>
                                            <asp:LinkButton runat="server" ID="lbUnduhDokTpm" Text="" ForeColor="Red" OnClick="lbUnduhDokTpm_Click">
                                            <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 70px; "></i>
                                            </asp:LinkButton><br />
                                        </td>
                                        <td>
                                            <div>Unggah dokumen hasil akreditasi Prodi TPM</div>
                                            <div>Tgl. Unggah:&nbsp;<asp:Label runat="server" ID="lblTglUnggahTpm" Text="-"></asp:Label></div>
                                            <div>Ukuran file:&nbsp;<asp:Label runat="server" ID="lblUkuranFileTpm" Text="-"></asp:Label></div>
                                            <div>
                                                <div style="padding: 10px;">
                                                    <div>
                                                        <div class="input-group input-group-button input-group-primary">
                                                            <asp:FileUpload runat="server" ID="fileUploadTPM" CssClass="form-control" />
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton runat="server" ID="lbUnggahDokumenTpm" CssClass="btn btn-info"
                                                                    OnClick="lbUnggahDokumenTpm_Click">
                                                                        <i class="fa fa-cloud-upload">&nbsp;Unggah</i></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <asp:Label runat="server" ID="lblInfoDokTpm" Text="" ForeColor="Red" Font-Bold="true"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">Format file PDF dengan ukuran maks 2MB
                                                </span>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-lg-5">
                                <asp:Label runat="server" ID="lblErrorDokTpm" Text="" ForeColor="Red"></asp:Label>
                            </div>
                        </div>

                    </asp:Panel>





<div class="modal modal-danger" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">

                <h4 class="modal-title" id="myModalHapus">Konfirmasi Hapus Data</h4>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus data
                &nbsp;<asp:Label runat="server" ID="lblNamaMitraPelaksana" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>

            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="modalUnggahDokMitra" tabindex="-1" role="dialog" aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card border border-info">
                            <div class="card-header">
                                <h2><i></i>&nbsp;&nbsp;Unggah Dokumen Mitra</h2>
                            </div>
                            <div class="card-body">
                                <div>
                                    <iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" width="100%" frameborder="0" height="150px"></iframe>
                                </div>
                                <div class="form-control">
                                    <div style="font-size: 14px; padding: 10px;">
                                        Ekstensi: pdf, PDF.
                                    </div>
                                    <div style="font-size: 14px; padding: 10px;">
                                        Maksimal: 1MB.<br>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input name="hid_id_identitas_jurnal"
                            id="hid_id_identitas_jurnal" type="hidden"></input>
                        <asp:LinkButton CssClass="btn btn-info" ID="selesai" runat="server" OnClick="selesai_Click">
                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Selesai
                        </asp:LinkButton>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-12 col-xl-8">
        <table class="table m-0">
            <tbody>
                <tr>
                    <td>
                        <asp:Panel ID="pnlUnggah" runat="server" Visible="false">
                            <div class="form-group row">
                                <label for="file" class="col-md-2 col-form-label form-control-label">File input</label>
                                <div class="col-md-9">
                                    <label for="file" class="custom-file">
                                        <input type="file" id="file" class="custom-file-input">
                                        <span class="custom-file-control"></span>
                                    </label>
                                    &nbsp;
                                </div>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>

<div class="modal modal-danger" id="modalInfo" role="dialog" aria-labelledby="myModalInfo">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <b class="modal-title" id="myModalInfo" style="font-size: medium;">Pemetaan Dokumen Pendukung terhadap Skema Penelitian</b>
            </div>
            <div class="modal-body">
                <div>
                    <asp:Image ID="imgInfoMitra" runat="server" ImageUrl="~/Images/mitraPenelitian.png" />
                </div>
            </div>
        </div>
    </div>
</div>



