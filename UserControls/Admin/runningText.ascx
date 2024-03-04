<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="runningText.ascx.cs" Inherits="simlitekkes.UserControls.Admin.runningText" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="UPDaftarText" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPDaftarText">
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
                        <h6>Running Text</h6>
                    </div>
                    <div class="card-body row">
                        <asp:MultiView ID="MultiViewText" runat="server">
                            <asp:View ID="ViewDaftar" runat="server">
                                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                <div class="col-md-1">
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
                                <div class="col-md-11">
                                    <div class="form-group float-right">
                                        <asp:LinkButton ID="lbDataBaru" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbDataBaru_Click" OnClientClick="initAutoComplete()">Data Baru</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDaftarText" runat="server" GridLines="None" CssClass="table table-striped table-hover"
                                            DataKeyNames="id_running_text,item_running_text" ShowHeader="true" ShowHeaderWhenEmTexty="true" AutoGenerateColumns="False"
                                            OnRowUpdating="gvDaftarText_RowUpdating" OnRowDeleting="gvDaftarText_RowDeleting" OnRowDataBound="gvDaftarText_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbnNomor" runat="server" Text='<%# Bind("no_baris") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Running Text">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRunningText" runat="server" Text='<%# Bind("item_running_text") %>'></asp:Label><br />
                                                        <asp:Label ID="lblStsAktif" runat="server" Text='<%# Bind("sts_aktif") %>' Font-Bold="True"
                                                            ForeColor="#227722"></asp:Label>&nbsp;-&nbsp; <span style="color: #227722; font-style: italic;">
                                                                <asp:Label ID="lblJmlKelompokPeran" runat="server" Text='<%# Bind("jml_peran") %>'></asp:Label>
                                                                Kelompok Peran </span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Jadwal Penayangan">
                                                    <ItemTemplate>
                                                        Mulai:
                                                            <asp:Label ID="lblTglMulaiAktif" runat="server" Text='<%# Eval("tgl_mulai_aktif") %>' ForeColor="#227722" Font-Bold="True"></asp:Label><br />
                                                        Berakhir:
                                                            <asp:Label ID="lblTglBerakhirAktif" runat="server" Text='<%# Eval("tgl_berakhir_aktif") %>' ForeColor="#227722" Font-Bold="True"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="300px" />
                                                    <ItemStyle Width="300px" HorizontalAlign="Left" CssClass="gvValueText" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aksi">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbEdit" runat="server" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary btn-sm" ToolTip="Edit Data">
                                                            <i class="fas fa-edit"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-danger btn-sm" ToolTip="Delete Data">
                                                            <i class="fas fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="Right" CssClass="gvValueText" VerticalAlign="Top" />
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
                                        <asp:TextBox ID="tbHTMLRunningText" runat="server" TextMode="MultiLine" Rows="5"
                                            Width="100%" EnableTheming="true"></asp:TextBox><br />
                                        <asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="tbHTMLRunningText"
                                            EnableSanitization="false">
                                        </asp:HtmlEditorExtender>
                                    </div>
                                    <div class="form-group">
                                        <label for="tbPeran" style="font-weight: bold">Diterapkan pada peran:</label>
                                    </div>
                                    <div class="form-group">
                                        <asp:CheckBoxList ID="cblPeranPengguna" runat="server" RepeatColumns="3" Width="100%" CellSpacing="5">
                                        </asp:CheckBoxList>
                                    </div>
                                    <hr />
                                    <div class="form-group">
                                        <div class="row">
                                            <label for="tbPenayangan" class="col-md-2">Tgl Penayangan:</label>
                                            <div class="col-md-10">
                                                <asp:TextBox runat="server" ID="tbTglMulaiTayang" placeholder="Tgl mulai" Width="200px"></asp:TextBox>
                                                <asp:CalendarExtender ID="tbTglMulaiTayang_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="tbTglMulaiTayang" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                <b>-</b>
                                                <asp:TextBox runat="server" ID="tbTglAkhirTayang" placeholder="Tgl akhir" Width="200px"></asp:TextBox>
                                                <asp:CalendarExtender ID="tbTglAkhirTayang_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="tbTglAkhirTayang" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group float-right">
                                        <asp:LinkButton ID="lbBatal" runat="server" CssClass="btn btn-danger" OnClick="lbBatal_Click">Kembali</asp:LinkButton>
                                        <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-success pull-right" OnClick="lbSimpan_Click">Simpan</asp:LinkButton>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Konfirmasi Hapus</h4>
                    </div>
                    <div class="modal-body">
                        Hapus data 
                        <asp:Label runat="server" ID="lblHapus" Text="..." ForeColor="Red"></asp:Label>
                        ?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                        <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-info pull-right" OnClick="lbHapus_Click" OnClientClick="$('#myModal').modal('hide');">Hapus</asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
