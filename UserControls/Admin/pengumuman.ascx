<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pengumuman.ascx.cs" Inherits="simlitekkes.UserControls.Admin.pengumuman" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>
<asp:UpdatePanel ID="UPDaftarPengumuman" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="lbUpload" />
    </Triggers>
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPDaftarPengumuman">
            <ProgressTemplate>
                <div class="modal_loading">
                    <div class="center">
                        <i class="fa fa-refresh fa-spin fa-5x"></i>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header bg-info text-white">
                        <h6>Pengumuman</h6>
                    </div>
                    <div class="card-body row">
                        <asp:MultiView ID="MultiViewPengumuman" runat="server">
                            <asp:View ID="ViewDaftar" runat="server">
                                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                <div class="col-md-1">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label for="ddlJmlBaris" class="sr-only">Jumlah Baris</label>
                                            <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="True"
                                                CssClass="form-control input-sm"
                                                OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                                <asp:ListItem Text="10" Value="10" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                                <asp:ListItem Text="Semua" Value="0" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-11">
                                    <div class="form-group float-right">
                                        <asp:LinkButton ID="lbDataBaru" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbDataBaru_Click" OnClientClick="initAutoComplete()">Data Baru</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <asp:GridView ID="gvDaftarPengumuman" runat="server" GridLines="None" CssClass="table table-striped table-hover"
                                        DataKeyNames="id_pengumuman,no_surat,kd_status_publikasi,kd_status_frontpages,judul,tgl_surat" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                        OnRowDataBound="gvDaftarPengumuman_RowDataBound" OnRowUpdating="gvDaftarPengumuman_RowUpdating" OnRowDeleting="gvDaftarPengumuman_RowDeleting" OnRowCommand="gvDaftarPengumuman_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbnNomor" runat="server" Text='<%# Bind("no_baris") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pengumuman">
                                                <ItemTemplate>
                                                    Nomer Surat: <span style="color: #227722; font-style: italic;">
                                                        <asp:Label ID="lblNoSurat" runat="server" Text='<%# Bind("no_surat") %>'></asp:Label></span> | 
                                                    Tgl Berita: <span style="color: #227722; font-style: italic;">
                                                        <asp:Label ID="lblTglSurat" runat="server" Text='<%# Bind("tgl_surat") %>'></asp:Label></span> | 
                                                    Published : <span style="color: #227722; font-style: italic;">
                                                        <asp:Label ID="lblTglPemberitaan" runat="server" Text='<%# Bind("tgl_pemberitaan") %>'></asp:Label></span><br />
                                                    <span style="color: #dc3545; font-style: italic;">
                                                        <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("judul") %>'></asp:Label></span><br />
                                                    <span style="color: #888; font-style: italic;">
                                                        <asp:Label ID="lblIsiPengumuman" runat="server" Text='<%# Bind("isi_pengumuman") %>'></asp:Label></span>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File Pengumuman">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFilePengumuman" runat="server" Text='<%# Bind("file_pengumuman") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Publikasi">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbStatusPublikasi" runat="server" CommandName="StatusPublikasi" CommandArgument='<%# Bind("kd_status_publikasi") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" HorizontalAlign="Left" CssClass="gvValueText" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Frontpages">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbStatusFrontpages" runat="server" CommandName="StatusFrontpages" CommandArgument='<%# Bind("kd_status_frontpages") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle Width="50px" HorizontalAlign="Left" CssClass="gvValueText" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aksi">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbEdit" runat="server" CommandName="Update" CommandArgument='<%# Bind("id_pengumuman") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete" CommandArgument='<%# Bind("id_pengumuman") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="min-height: 100px; margin: 0 auto;">
                                                <strong>DATA TIDAK DITEMUKAN</strong>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-5">
                                    <asp:Menu ID="MenuPage" runat="server" Orientation="Horizontal" BackColor="#F7F6F3"
                                        DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="12px" ForeColor="#2C7F27"
                                        StaticSubMenuIndent="10px" OnMenuItemClick="menu_event">
                                        <Items>
                                        </Items>
                                        <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
                                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                                        <DynamicMenuStyle BackColor="#F7F6F3" />
                                        <DynamicSelectedStyle BackColor="#9D7B5D" />
                                        <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
                                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                                        <StaticSelectedStyle BackColor="#DD7B5D" Font-Bold="true" ForeColor="White" />
                                    </asp:Menu>
                                </div>
                                <div class="col-md-7" style="text-align: right;">
                                    Total&nbsp;<asp:Label ID="lblJmlRecords" runat="server" Text="0"></asp:Label>&nbsp;Data
                                </div>
                            </asp:View>
                            <asp:View ID="ViewData" runat="server">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="tbNoSurat" class="col-sm-3 control-label">Nomer Surat</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" placeholder="Nomer Surat" ID="tbNoSurat" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="tbTglSurat" class="col-sm-3 control-label">Tanggal Surat</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" ID="tbTglSurat" placeholder="Tanggal Surat" CssClass="form-control pull-right" data-provide="datepicker" data-date-format="yyyy-mm-dd"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">

                                            <label for="tbJudul" class="col-sm-3 control-label">Judul</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" placeholder="Judul" ID="tbJudul" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="tbTglPemberitaan" class="col-sm-3 control-label">Tgl Pemberitaan</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" ID="tbTglPemberitaan" placeholder="Tanggal Pemberitaan" CssClass="form-control pull-right" data-provide="datepicker" data-date-format="yyyy-mm-dd"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="tbIsiPengumuman" class="col-sm-3 control-label">Isi Pengumuman</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" ID="tbIsiPengumuman" placeholder="Isi Pengumuman" CssClass="form-control"
                                                    TextMode="MultiLine" Height="57px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">

                                            <label for="cbStatusPublikasi" class="col-sm-3 control-label">Status Publikasi</label>
                                            <div class="checkbox col-sm-4">
                                                <asp:CheckBox runat="server" ID="cbStatusPublikasi" Checked="true" Text="Aktif" CssClass="minimal" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="cbStatusFrontpages" class="col-sm-3 control-label">Status Frontpages</label>
                                            <div class="checkbox col-sm-4">
                                                <asp:CheckBox runat="server" ID="cbStatusFrontpages" Checked="true" Text="Aktif" CssClass="minimal" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="tbLampiran" class="col-sm-3 control-label">File Lampiran</label>
                                            <div class="col-sm-4">
                                                <asp:LinkButton ID="lbLampiran" runat="server" CssClass="btn btn-info pull-left" OnClick="lbLampiran_Click">Tambah File Lampiran</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblErrorFile" runat="server" ForeColor="Red"></asp:Label>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:GridView ID="gvDaftarPengumumanFile" runat="server" GridLines="None" CssClass="table table-striped table-hover"
                                                    DataKeyNames="id_pengumuman_file,judul_file,file_pengumuman" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowUpdating="gvDaftarPengumumanFile_RowUpdating" OnRowDeleting="gvDaftarPengumumanFile_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbnNomor" runat="server" Text='<%# Bind("no_baris") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px" />
                                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Judul File">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbUnduhPengumumanFile" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="unduh">
                                                                    <asp:Label ID="lblJudulFile" runat="server" Text='<%# Bind("judul_file") %>'></asp:Label>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Urutan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUrutan" runat="server" Text='<%# Bind("urutan") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Aksi">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbEditFile" runat="server" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary btn-sm" OnClientClick="initWebformPengumuman();" ToolTip="Edit Data">
                                                            <i class="fas fa-edit"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lbDeleteFile" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-danger btn-sm" OnClientClick="initWebformPengumuman();" ToolTip="Hapus Data">
                                                            <i class="fas fa-trash"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
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
                                        <!-- /.box-body -->
                                        <div class="form-group">
                                            <asp:LinkButton ID="lbBatal" runat="server" CssClass="btn btn-warning" OnClick="lbBatal_Click">Kembali</asp:LinkButton>
                                            <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-info pull-right" OnClick="lbSimpan_Click">Simpan</asp:LinkButton>
                                        </div>
                                        <!-- /.box-footer -->
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal modal-primary" id="myModal" role="dialog" aria-labelledby="myModalKonfirmasiGenerate">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title font-weight-600">Konfirmasi Hapus</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">                        
                        <asp:Label runat="server" ID="lblJudul" ForeColor="Red"></asp:Label><br />
                        No. Surat: <asp:Label runat="server" ID="lblNoSurat" ForeColor="#888"></asp:Label><br />
                        Tgl Surat: <asp:Label runat="server" ID="lblTglSurat" ForeColor="#888"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                        <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapus_Click" OnClientClick="$('#myModal').modal('hide');">Hapus</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal modal-primary" id="hapusLampiranPengumuman" role="dialog" aria-labelledby="myModalKonfirmasiGenerate">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title font-weight-600">Konfirmasi Hapus</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Hapus data Pengumuman File ?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                        <asp:LinkButton ID="lbHapusFile" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapusFile_Click" OnClientClick="$('#hapusLampiranPengumuman').modal('hide');">Hapus</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal modal-primary" id="lampiranPengumuman" role="dialog" aria-labelledby="myModalKonfirmasiGenerate">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title font-weight-600">Lampiran Pengumuman</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="tbJudulFile">Judul File</label>
                                <asp:TextBox runat="server" placeholder="Ketik judul file" ID="tbJudulFile" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="tbFilePengumuman">File Pengumuman</label><br />
                                <asp:FileUpload ID="uploadFilePengumuman" runat="server" />
                            </div>

                            <div class="form-group">
                                <label for="ddlUrutan">Urutan</label>
                                <asp:DropDownList ID="ddlUrutan" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="01">Ke-1</asp:ListItem>
                                    <asp:ListItem Value="02">Ke-2</asp:ListItem>
                                    <asp:ListItem Value="03">Ke-3</asp:ListItem>
                                    <asp:ListItem Value="04">Ke-4</asp:ListItem>
                                    <asp:ListItem Value="05">Ke-5</asp:ListItem>
                                    <asp:ListItem Value="06">Ke-6</asp:ListItem>
                                    <asp:ListItem Value="07">Ke-7</asp:ListItem>
                                    <asp:ListItem Value="08">Ke-8</asp:ListItem>
                                    <asp:ListItem Value="09">Ke-9</asp:ListItem>
                                    <asp:ListItem Value="10">Ke-10</asp:ListItem>
                                    <asp:ListItem Value="11">Ke-11</asp:ListItem>
                                    <asp:ListItem Value="12">Ke-12</asp:ListItem>
                                    <asp:ListItem Value="13">Ke-13</asp:ListItem>
                                    <asp:ListItem Value="14">Ke-14</asp:ListItem>
                                    <asp:ListItem Value="15">Ke-15</asp:ListItem>
                                    <asp:ListItem Value="16">Ke-16</asp:ListItem>
                                    <asp:ListItem Value="17">Ke-17</asp:ListItem>
                                    <asp:ListItem Value="18">Ke-18</asp:ListItem>
                                    <asp:ListItem Value="19">Ke-19</asp:ListItem>
                                    <asp:ListItem Value="20">Ke-20</asp:ListItem>
                                    <asp:ListItem Value="21">Ke-21</asp:ListItem>
                                    <asp:ListItem Value="22">Ke-22</asp:ListItem>
                                    <asp:ListItem Value="23">Ke-23</asp:ListItem>
                                    <asp:ListItem Value="24">Ke-24</asp:ListItem>
                                    <asp:ListItem Value="25">Ke-25</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <!-- /.box-body -->
                            <div class="form-group">
                                <asp:Button ID="lbUpload" runat="server" CssClass="btn btn-info btn-block" Text="Upload" OnClick="lbUpload_Click" OnClientClick="$('#lampiranPengumuman').modal('hide');" />
                            </div>
                            <!-- /.box-footer -->
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Batal</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.content -->
    </ContentTemplate>
</asp:UpdatePanel>
