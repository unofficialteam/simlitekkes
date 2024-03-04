<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pengembalianDana.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pengembalianDana" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

<asp:MultiView ID="mvPengembalianDana" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarUsulan" runat="server">
        <div class="row">
            <div class="col-sm-12 p-0">
                <div class="main-header">
                    <h4>Pengembalian Dana</h4>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-block">
                        <div class="md-card-block">
                            <div class="row">
                                <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="True"
                                    CssClass="custom-select" OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                    <asp:ListItem Text="Semua" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div class="row">
                                <asp:GridView runat="server" ID="gvUsulan" CssClass="table table-striped table-hover"
                                    GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                                    ShowFooter="True" DataKeyNames="id_usulan_kegiatan, sts_unggah_berkas, jml_setoran, no_ntpn"
                                    OnRowDataBound="gvUsulan_RowDataBound"
                                    OnRowEditing="gvUsulan_RowEditing" OnRowCancelingEdit="gvUsulan_RowCancelingEdit"
                                    OnRowUpdating="gvUsulan_RowUpdating" OnRowCommand="gvUsulan_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNomor" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                            <ItemStyle Width="25px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Usulan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSkema" runat="server" ForeColor="DarkGreen" Text='<%# Bind("nama_skema") %>'></asp:Label><br />
                                                <asp:Label ID="lblJudul" runat="server" Font-Bold="true" Text='<%# Bind("judul") %>'></asp:Label><br />
                                                <br />
                                                Kategori Pembatalan: &nbsp<asp:Label ID="lblKategoriPembatalan" runat="server" Text='<%# Bind("kategori_pembatalan") %>'></asp:Label><br />
                                                Tahun Pendanaan: &nbsp<asp:Label ID="lblThnUsulan" runat="server" Text='<%# Bind("thn_pelaksanaan_kegiatan") %>'></asp:Label><br />
                                                Usulan Tahun Ke &nbsp<asp:Label ID="lblUsulanThnKe" runat="server" Font-Bold="true" Text='<%# Bind("urutan_thn_usulan_kegiatan") %>'></asp:Label>&nbsp
                                                Dari Rencana Kegiatan &nbsp<asp:Label ID="lblLamaKegiatan" runat="server" Font-Bold="true" Text='<%# Bind("lama_kegiatan") %>'></asp:Label>&nbsp Tahun<br />
                                                Dana Disetujui: &nbsp <b>Rp</b><asp:Label ID="lblDanaDisetujui" runat="server" Font-Bold="true" Text='<%# Eval("dana_disetujui", "{0:0,00}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nomor NTPN (Nomor Transaksi Penerimaan Negara)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoNTPN" runat="server" Text='<%# Bind("no_ntpn") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="tbNoNTPNEdit" runat="server" Width="95px" Text='<%# Bind("no_ntpn") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Setoran">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJmlSetoran" runat="server" Text='<%# Eval("jml_setoran", "{0:0,00}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="tbJmlSetoranEdit" runat="server" Width="95px" CssClass="form-control uang1" Text='<%# Bind("jml_setoran") %>'></asp:TextBox> <%--onkeyup="insertcommas(this);"--%>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbUbahPengembalianDana" runat="server" CommandName="Edit" CausesValidation="false"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Ubah"
                                                    CssClass="fa fa-edit btn btn-primary">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbUnggahPengembalianDana" runat="server" CommandName="unggah"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unggah"
                                                    CssClass="fa fa-upload btn btn-success">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbUnduhPengembalianDana" runat="server" CommandName="unduhBerkas"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh"
                                                    CssClass="fa fa-download btn btn-default">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lbCancelEdit" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false"
                                                    CommandName="Cancel" ToolTip="Batal">
                                                        <i class="fa fa-times"></i>
                                                </asp:LinkButton>&nbsp; &nbsp;
                                                        <asp:LinkButton ID="lbSaveEdit" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false"
                                                            CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>' ToolTip="Simpan">
                                                        <i class="fa fa-floppy-o"></i>
                                                        </asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="200px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;">
                                            <strong>TIDAK ADA DATA</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <script type="text/javascript">
                                new AutoNumeric('.uang1', {
                                    decimalPlaces: 0,
                                    digitGroupSeparator: '.',
                                    decimalCharacter: ',',
                                    minimumValue: 0
                                });
                            </script>
                            <div class="row">
                                <div class="col-sm-5">
                                    <asp:controlPaging runat="server" ID="pagingDaftarUsulan" OnPageChanging="pagingDaftarUsulan_PageChanging" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>
<div class="modal fade" id="modalPengembalianDana" role="dialog" aria-labelledby="myModalPengembalianDana">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 style="text-align: center" class="modal-title" id="myModalPengembalianDana">UNGGAH BERKAS PENGEMBALIAN DANA
                </h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="row">
                            <div class="form-group">
                                <label for="fileUpload1" class="col-sm-4 control-label">Pilih Berkas</label>
                                <div class="col-sm-10">
                                    <asp:FileUpload ID="fileUpload1" runat="server" CssClass="form-control" Height="40px" />
                                    <span class="input-group-btn">
                                        <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-info btn-flat" OnClick="lbUnggahDokumen_Click">
                                                <i class="fa fa-cloud-upload"> &nbsp; Unggah</i></asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function insertcommas(nField) {

        if (/^0/.test(nField.value)) {
            nField.value = nField.value.substring(0, 1);
        }
        if (Number(nField.value.replace(/,/g, ""))) {
            var tmp = nField.value.replace(/,/g, "");
            tmp = tmp.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,').split('').reverse().join('').replace(/^,/, '');
            if (/\./g.test(tmp)) {
                tmp = tmp.split(".");
                tmp[1] = tmp[1].replace(/\,/g, "").replace(/ /, "");
                nField.value = tmp[0] + "." + tmp[1]
            }
            else {
                nField.value = tmp.replace(/ /, "");
            }
        }
        else {
            nField.value = nField.value.replace(/[^\d\,\.]/g, "").replace(/ /, "");
        }
    }
</script>
