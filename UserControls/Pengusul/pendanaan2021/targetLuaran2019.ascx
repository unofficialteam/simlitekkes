<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="targetLuaran2019.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendanaan2021.targetLuaran2019" %>
<%@ Register Src="~/UserControls/Pengusul/luaran2019/publikasiJurnal.ascx" TagName="ktPublikasi" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran2019/publikasiProsiding.ascx" TagName="ktPublikasiProsiding" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran2019/bukuCetakHasilPenelitian.ascx" TagName="ktBukuCetakHasilPenelitian" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran2019/bukuElektronikHasilPenelitian.ascx" TagName="ktBukuElektronikHasilPenelitian" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran2019/bookChapter.ascx" TagName="ktBookChapter" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran2019/luaranWajibPtDanPp.ascx" TagName="ktLuaranWajibPTdanPP" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran2019/publikasiProsidingLuaranTambahan.ascx" TagName="ktPublikasiProsidingLuaranTambahan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran2019/bookChapterLuaranTambahan.ascx" TagName="ktBookChapterLuaranTambahan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran2019/publikasiPasca.ascx" TagName="ktPublikasiPasca" TagPrefix="uc" %>

<div class="card mb-4">
    <div class="card-body">
        <div class="alert alert-light" role="alert" style="background-color: lightgrey;">
            <span style="color: maroon; font-weight: bold; font-size: 18px;">Substansi Usulan Penelitian </span>
        </div>
    </div>
</div>

<asp:MultiView ID="mvLuaran" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarLuaranWajib" runat="server">

        <div class="card mb-4">
            <div class="card-header">
                <div style="font-weight: bold;">
                    Luaran dan Target Capaian
                </div>
            </div>
            <div class="card-body">
                <table class="table table-hover" style="width: 98%;">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblJudulLuaranWajib" Text="LUARAN WAJIB" BackColor="#00dd00" ForeColor="Black"
                                Style="padding: 6px; text-align: center;" Width="200" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Label runat="server" ID="lblInfoKelengkapanLuaran" Text="Luaran Wajib Belum Lengkap"
                    BackColor="#dd6666" ForeColor="White" Style="padding: 3px; margin: 5px; text-align: center;" Width="260"></asp:Label>

                <%--<asp:Panel runat="server" ID="panelPenelitianTerapanPengembangan" Style="margin-top: 20px;">
                    <div class="form-inline">
                        <div class="col-lg-2 text-right">
                            Kategori Luaran 
                        </div>
                        <div class="col-lg-8 text-left">
                            <asp:DropDownList runat="server" ID="ddlKategoriLuaranTerapan" CssClass="form-control" Width="300"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlKategoriLuaranTerapan_SelectedIndexChanged" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>&nbsp;</div>
                    <asp:MultiView runat="server" ID="mvJenisLuaranTerapan">
                        <asp:View runat="server" ID="vLuaranWajibPTdanPP">
                            <uc:ktLuaranWajibPTdanPP runat="server" ID="ktLuaranWajibPTdanPP"></uc:ktLuaranWajibPTdanPP>
                        </asp:View>
                    </asp:MultiView>
                </asp:Panel>--%>

                <asp:Panel runat="server" ID="panelPenelitianDasar">
                    <asp:GridView ID="gvluaranwajib" runat="server" GridLines="None" Width="96%"
                        CssClass="table table-hover"
                        DataKeyNames="id_luaran_dijanjikan,tahun_ke,nama_target_capaian_luaran,id_jenis_luaran,kd_kategori_jenis_luaran"
                        ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowDataBound="gvluaranwajib_RowDataBound" OnRowCommand="gvluaranwajib_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    Tahun
                                    <asp:Label ID="lblTahunke" runat="server" Text='<%# Bind("tahun_ke") %>'></asp:Label>:
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label>
                                    <asp:Label ID="lbltargetluaran" runat="server" BackColor="Yellow" Font-Bold="true" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="lblketerangan" runat="server" Font-Italic="True" Text='<%# Bind("keterangan") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbTambah" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="tambah" CssClass="btn btn-success btn-sm" Font-Size="Larger" ToolTip="Tambah Luaran" Visible="true"><i class="fa fa-plus"></i>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ubah" CssClass="btn btn-warning btn-sm" Font-Size="Larger" ToolTip="Edit"> <i class="fas fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbHapus" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="hapus" CssClass="btn btn-danger btn-sm" Font-Size="Larger" ToolTip="Hapus"> <i class="fa fa-trash"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="150" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="min-height: 100px; margin: 0 auto;">
                                <strong>DATA TIDAK DITEMUKAN</strong>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>

            </div>
        </div>

        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <table class="table table-hover" style="width: 98%;">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblJudulLuaranTambahan" Text="LUARAN TAMBAHAN" BackColor="#00dd00" ForeColor="Black"
                                        Style="padding: 6px; text-align: center;" Width="200" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="text-right" style="display: none;">
                                    <b>Tambah</b>&nbsp;<asp:LinkButton ID="lbTambahLuaranTambahan" runat="server"
                                        CssClass="btn btn-success btn-sm" ToolTip="Tambah" OnClick="lbTambahLuaranTambahan_Click"> <i class="fa fa-plus"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvluarantambahan" runat="server" GridLines="None"
                    CssClass="table table-hover" Font-Size="Small" Width="96%"
                    DataKeyNames="id_luaran_dijanjikan,tahun_ke,nama_target_capaian_luaran,id_jenis_luaran,kd_kategori_jenis_luaran"
                    ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnPreRender="gvluarantambahan_PreRender" OnRowDataBound="gvluarantambahan_RowDataBound" OnRowCommand="gvluarantambahan_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                Tahun
                                                        <asp:Label ID="lblTahunke" runat="server" Text='<%# Bind("tahun_ke") %>'></asp:Label>:
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label>
                                <asp:Label ID="lbltargetluaran" runat="server" BackColor="Yellow" Font-Bold="true" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblketerangan" runat="server" Font-Italic="true" Text='<%# Bind("keterangan") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbHapus" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="hapus"
                                                            CssClass="fa fa-trash-o" Font-Size="XX-Large" ToolTip="Hapus Luaran" Visible="false">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="150" />
                                                </asp:TemplateField>--%>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbTambah" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="tambah" CssClass="btn btn-success btn-sm" Font-Size="Larger" ToolTip="Tambah Luaran" Visible="true"><i class="fa fa-plus"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbEdit" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ubah" CssClass="btn btn-warning btn-sm" Font-Size="Larger" ToolTip="Edit"> <i class="fa fa-pencil"></i></asp:LinkButton>
                                <asp:LinkButton ID="lbHapus" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="hapus" CssClass="btn btn-danger btn-sm" Font-Size="Larger" ToolTip="Hapus"> <i class="fa fa-trash"></i></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="150" />
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

    <asp:View ID="vTambahLuaran" runat="server">
        <div class="card mb-4">
            <div class="card-body">
                <br />
                <div class="row">
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-8">
                        <b style="color: maroon;">Luaran dan Target Capaian
                        </b>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-8">
                        <asp:Label runat="server" ID="lblJudulTambahLuaran" Text="TAMBAH DATA LUARAN WAJIB" BackColor="#00dd00" ForeColor="Black"
                            Style="padding: 6px; text-align: center;" Width="300" Font-Bold="true"></asp:Label>

                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-4 text-right">
                        Tahun
                    </div>
                    <div class="col-lg-8 text-left">
                        <asp:Label runat="server" ID="lblThnKe" Text="0"></asp:Label>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-4 text-right">
                        Kategori Luaran 
                    </div>
                    <div class="col-lg-8 text-left">
                        <asp:DropDownList runat="server" ID="ddlKategoriPenelitianDasar" CssClass="form-control" Width="300"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlKategoriPenelitianDasar_SelectedIndexChanged" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />

                <asp:MultiView runat="server" ID="mvJenisLuaran">
                    <asp:View runat="server" ID="vBlank">
                        <div></div>
                    </asp:View>
                    <asp:View runat="server" ID="vPublikasi">
                        <uc:ktPublikasi runat="server" ID="ktPublikasi"></uc:ktPublikasi>
                    </asp:View>
                    <asp:View runat="server" ID="vPublikasiPasca">
                        <uc:ktPublikasiPasca runat="server" ID="ktPublikasiPasca"></uc:ktPublikasiPasca>
                    </asp:View>
                    <asp:View runat="server" ID="vPublikasiProsiding">
                        <uc:ktPublikasiProsiding runat="server" ID="ktPublikasiProsiding"></uc:ktPublikasiProsiding>
                    </asp:View>
                    <asp:View runat="server" ID="vPublikasiProsidingLuaranTambahan">
                        <uc:ktPublikasiProsidingLuaranTambahan runat="server" ID="ktPublikasiProsidingLuaranTambahan"></uc:ktPublikasiProsidingLuaranTambahan>
                    </asp:View>
                    <asp:View runat="server" ID="vBukuCetakHasilPenelitian">
                        <uc:ktBukuCetakHasilPenelitian runat="server" ID="ktBukuCetakHasilPenelitian"></uc:ktBukuCetakHasilPenelitian>
                    </asp:View>
                    <asp:View runat="server" ID="vBukuElektronikHasilPenelitian">
                        <uc:ktBukuElektronikHasilPenelitian runat="server" ID="ktBukuElektronikHasilPenelitian"></uc:ktBukuElektronikHasilPenelitian>
                    </asp:View>
                    <asp:View runat="server" ID="vBookChapter">
                        <uc:ktBookChapter runat="server" ID="ktBookChapter"></uc:ktBookChapter>
                    </asp:View>
                    <asp:View runat="server" ID="vBookChapterLuaranTambahan">
                        <uc:ktBookChapterLuaranTambahan runat="server" ID="ktBookChapterLuaranTambahan"></uc:ktBookChapterLuaranTambahan>
                    </asp:View>
                    <asp:View runat="server" ID="vTambahLuaranTerapan">
                        <uc:ktLuaranWajibPTdanPP runat="server" ID="ktLuaranWajibPTdanPPFormTambah"></uc:ktLuaranWajibPTdanPP>
                    </asp:View>
                    <asp:View runat="server" ID="vLuaranWajibPTdanPPDasar">
                        <uc:ktLuaranWajibPTdanPP runat="server" ID="ktLuaranWajibPTdanPPDasar"></uc:ktLuaranWajibPTdanPP>
                    </asp:View>

                </asp:MultiView>
            </div>
        </div>
    </asp:View>

</asp:MultiView>

<div class="modal modal-warning" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah anda yakin akan menghapus luaran yang dijanjikan ini? 
                        <asp:Label runat="server" ID="lblHapus" Text="" ForeColor="Green"></asp:Label>

                <div class="modal-footer">
                    <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-danger pull-right" OnClick="lbHapus_Click" OnClientClick="$('#myModal').modal('hide');">Hapus</asp:LinkButton>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                </div>
            </div>
        </div>
    </div>
</div>
