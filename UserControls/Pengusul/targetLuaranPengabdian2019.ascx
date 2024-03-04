<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="targetLuaranPengabdian2019.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.targetLuaranPengabdian2019" %>
<%@ Register Src="~/UserControls/Pengusul/luaranAbdimas2019/patenNHakCIpta.ascx" TagName="patenNHakCIpta" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaranAbdimas2019/publikasiNBuku.ascx" TagName="publikasiNBuku" TagPrefix="uc" %>

<div class="row" style="margin-top: 20px;">
    <div class="col-lg-12">

        <div class="row" style="margin-bottom: 20px; text-align: right;">
            <div class="col-lg-12">
                <asp:Label runat="server" ID="lblInfoAtUnggahDokUsulan" Text="Program Kemitraan Masyarakat (tahun ke-1 dari 3 tahun)"></asp:Label>
            </div>
        </div>

        <div class="card">
            <div class="card-body">




                <div class="row">

                    <div class="col-lg-12">
                        <div class="alert alert-light" role="alert" style="background-color: lightgrey;">
                            <span style="color: maroon; font-weight: bold; font-size: 18px;">Substansi Usulan Pengabdian Kepada Masyarakat </span>
                        </div>
                    </div>
                </div>

                <asp:MultiView ID="mvLuaran" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vDaftarLuaranWajib" runat="server">
                        <div style="padding: 10px;">

                            <div class="row">
                                <div class="col-lg-12">

                                    <b>Luaran dan Target Capaian
                                    </b>
                                    <br />
                                    <table class="table table-hover" style="width: 98%;">
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblJudulLuaranWajib" Text="LUARAN WAJIB" BackColor="#00dd00" ForeColor="Black"
                                                    Style="padding: 6px; text-align: center;" Width="200" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12" style="margin-left: 20px;">
                                    <asp:Label runat="server" ID="lblInfoKelengkapanLuaran" Text="Luaran Wajib Belum Lengkap"
                                        BackColor="#dd6666" ForeColor="White" Style="padding: 3px; margin: 5px; text-align: center;" Width="260"></asp:Label>
                                    <div style="margin-top: 5px; margin-bottom: 5px;">
                                        <asp:GridView ID="gvluaranwajib" runat="server" GridLines="None" Width="96%"
                                            CssClass="table table-hover"
                                            DataKeyNames="id_luaran_dijanjikan,kd_kategori_jenis_luaran,id_jenis_luaran,id_target_capaian_luaran,tahun_ke,nama_target_capaian_luaran,arr_kd_kategori_jenis_luaran,arr_nama_kategori_jenis_luaran,id_kategori_sbk"
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
                                                        <%--<asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>'></asp:Label>--%>
                                                        <%--<%# Eval("id_kategori_sbk")%>.&nbsp;--%><asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_kategori_jenis_luaran") %>'></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblnamaJenisluaran" runat="server" Font-Bold="true" Text='<%# Bind("nama_jenis_luaran") %>' Style="margin-left: 20px;"></asp:Label>
                                                        <asp:Label ID="lbltargetluaran" runat="server" BackColor="Yellow" Font-Bold="true" Text='<%# Bind("nama_target_capaian_luaran") %>'></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblketerangan" runat="server" Font-Italic="True" Text='<%# Bind("keterangan") %>' Style="margin-left: 20px;"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbEdit" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ubah" CssClass="btn btn-warning btn-sm" Font-Size="Larger" ToolTip="Edit"> <i class="fas fa-pencil-alt"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbHapus" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="hapus" CssClass="btn btn-danger btn-sm" Font-Size="Larger" ToolTip="Hapus"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton ID="lbTambah" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="tambah" CssClass="btn btn-success btn-sm" Font-Size="Larger" ToolTip="Tambah Luaran" Visible="true"><i class="fa fa-plus"></i>
                                                        </asp:LinkButton>
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
                            </div>
                            <hr />
                            <div class="row">


                                <div class="col-lg-12">
                                    <table class="table table-hover" style="width: 98%;">
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblJudulLuaranTambahan" Text="LUARAN TAMBAHAN" BackColor="#00dd00" ForeColor="Black"
                                                    Style="padding: 6px; text-align: center;" Width="200" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="text-right" style="display: none;">
                                                <b>Tambah</b>&nbsp;<asp:LinkButton ID="lbTambahLuaranTambahan" runat="server"
                                                    CssClass="btn btn-success btn-sm" ToolTip="Tambah"> <i class="fa fa-plus"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12" style="margin-left: 20px;">
                                    <div style="margin-top: 5px; margin-bottom: 5px;">
                                        <asp:GridView ID="gvluarantambahan" runat="server" GridLines="None"
                                            CssClass="table table-hover" Font-Size="Small" Width="96%"
                                            DataKeyNames="id_luaran_dijanjikan,tahun_ke,nama_target_capaian_luaran,id_jenis_luaran,kd_kategori_jenis_luaran"
                                            ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowDataBound="gvluarantambahan_RowDataBound" OnRowCommand="gvluarantambahan_RowCommand">
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
                                                        <asp:LinkButton ID="lbEdit" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ubah" CssClass="btn btn-warning btn-sm" Font-Size="Larger" ToolTip="Edit"> <i class="fas fa-pencil-alt"></i></asp:LinkButton>
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
                            </div>
                        </div>

                    </asp:View>
                    <asp:View ID="vTambahLuaran" runat="server">

                        <asp:MultiView runat="server" ID="mvJenisLuaran">
                            <asp:View runat="server" ID="vTambahLuaranForm">
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
                                        <asp:Label runat="server" ID="lblKategoriLuaran" Text="Kategori Luaran "></asp:Label>
                                    </div>
                                    <div class="col-lg-8 text-left">
                                        <asp:DropDownList runat="server" ID="ddlKategori" CssClass="form-control" Width="300"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlKategori_SelectedIndexChanged" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />





                                <div class="row">
                                    <div class="col-lg-4 text-right">
                                        <asp:Label runat="server" ID="lblJenisLuaran" Text="Jenis luaran"></asp:Label>
                                    </div>
                                    <div class="col-lg-8 text-left">
                                        <asp:DropDownList runat="server" ID="ddlJenisLuaran" CssClass="form-control" Width="300"
                                            DataTextField="nama_jenis_luaran" DataValueField="id_jenis_luaran" AutoPostBack="true" OnSelectedIndexChanged="ddlTargetStatusLuaran_SelectedIndexChanged">
                                            <asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Test" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-4 text-right">
                                        <asp:Label runat="server" ID="lblRencanaNama" Text="Rencana Nama Jurnal"></asp:Label>
                                    </div>
                                    <div class="col-lg-8 text-left">
                                        <asp:TextBox runat="server" ID="tbRencanaNama" CssClass="form-control" placeholder="" Width="300"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-4 text-right">
                                        <asp:Label runat="server" ID="lblStatus" Text="Status"></asp:Label>

                                    </div>
                                    <div class="col-lg-8 text-left">
                                        <asp:DropDownList runat="server" ID="ddlTargetStatusLuaran" CssClass="form-control" Width="300"
                                            DataTextField="nama_target_capaian_luaran" DataValueField="id_target_capaian_luaran" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlTargetStatusLuaran_SelectedIndexChanged">
                                            <asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Test" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-4 text-right">
                                        Bukti Luaran <span style="color: red;">(Tahun
                                            <asp:Label runat="server" ID="lblThnBuktiLuaran" Text=""></asp:Label>)</span>
                                    </div>
                                    <div class="col-lg-8 text-left">
                                        <asp:Label runat="server" ID="lblInfoBuktiLuaran" Text=""></asp:Label>
                                    </div>
                                </div>
                                <br />

                                <div class="row">
                                    <div class="col-lg-4 text-right">
                                    </div>
                                    <div class="col-lg-8 text-left">
                                        <asp:LinkButton runat="server" ID="lbSimpan" CssClass="btn btn-success btn-sm" Text="Simpan"
                                            OnClick="lbSimpan_Click" Width="100"></asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger btn-sm" Text="Batal"
                                            OnClick="lbBatal_Click" Width="100"></asp:LinkButton>
                                    </div>
                                </div>
                                <br />
                            </asp:View>

                            <asp:View runat="server" ID="vTambahLTambahan">


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
                                        <asp:Label runat="server" ID="Label1" Text="TAMBAH DATA LUARAN TAMBAHAN" BackColor="#00dd00" ForeColor="Black"
                                            Style="padding: 6px; text-align: center;" Width="300" Font-Bold="true"></asp:Label>

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-4 text-right">
                                        Tahun
                                    </div>
                                    <div class="col-lg-8 text-left">
                                        <asp:Label runat="server" ID="lblThnKeLTambahan" Text="0"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-4 text-right">
                                        <asp:Label runat="server" ID="Label3" Text="Kategori Luaran "></asp:Label>
                                    </div>
                                    <div class="col-lg-8 text-left">
                                        <asp:DropDownList runat="server" ID="ddlKategoriLTambahan" CssClass="form-control" Width="300"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlKategoriLTambahan_SelectedIndexChanged" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />


                                <asp:MultiView runat="server" ID="mvTambahLTambahan">
                                    <asp:View runat="server" ID="vPatenNHakcipta">
                                        <uc:patenNHakCIpta runat="server" ID="ktPatenNHakCIpta"></uc:patenNHakCIpta>
                                    </asp:View>
                                    <asp:View runat="server" ID="vPublikasiNBuku">
                                        <uc:publikasiNBuku runat="server" ID="ktPublikasiNBuku"></uc:publikasiNBuku>
                                    </asp:View>
                                </asp:MultiView>

                            </asp:View>
                            <%--<asp:View runat="server" ID="vPublikasi">
                                <uc:ktpublikasi runat="server" id="ktPublikasi"></uc:ktpublikasi>
                            </asp:View>
                            <asp:View runat="server" ID="vPublikasiPasca">
                                <uc:ktpublikasipasca runat="server" id="ktPublikasiPasca"></uc:ktpublikasipasca>
                            </asp:View>
                            <asp:View runat="server" ID="vPublikasiProsiding">
                                <uc:ktpublikasiprosiding runat="server" id="ktPublikasiProsiding"></uc:ktpublikasiprosiding>
                            </asp:View>
                            <asp:View runat="server" ID="vPublikasiProsidingLuaranTambahan">
                                <uc:ktpublikasiprosidingluarantambahan runat="server" id="ktPublikasiProsidingLuaranTambahan"></uc:ktpublikasiprosidingluarantambahan>
                            </asp:View>
                            <asp:View runat="server" ID="vBukuCetakHasilPenelitian">
                                <uc:ktbukucetakhasilpenelitian runat="server" id="ktBukuCetakHasilPenelitian"></uc:ktbukucetakhasilpenelitian>
                            </asp:View>
                            <asp:View runat="server" ID="vBukuElektronikHasilPenelitian">
                                <uc:ktbukuelektronikhasilpenelitian runat="server" id="ktBukuElektronikHasilPenelitian"></uc:ktbukuelektronikhasilpenelitian>
                            </asp:View>
                            <asp:View runat="server" ID="vBookChapter">
                                <uc:ktbookchapter runat="server" id="ktBookChapter"></uc:ktbookchapter>
                            </asp:View>
                            <asp:View runat="server" ID="vBookChapterLuaranTambahan">
                                <uc:ktbookchapterluarantambahan runat="server" id="ktBookChapterLuaranTambahan"></uc:ktbookchapterluarantambahan>
                            </asp:View>
                            <asp:View runat="server" ID="vTambahLuaranTerapan">
                                <uc:ktluaranwajibptdanpp runat="server" id="ktLuaranWajibPTdanPPFormTambah"></uc:ktluaranwajibptdanpp>
                            </asp:View>--%>
                        </asp:MultiView>
                    </asp:View>

                </asp:MultiView>


                <div class="modal modal-danger" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi</h5>
                                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                Apakah anda yakin akan menghapus luaran yang dijanjikan ini? 
                        <asp:Label runat="server" ID="lblHapus" Text="" ForeColor="Green"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-danger pull-right" OnClick="lbHapus_Click" OnClientClick="$('#myModal').modal('hide');">Hapus</asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
</div>
