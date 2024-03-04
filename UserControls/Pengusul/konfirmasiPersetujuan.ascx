<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="konfirmasiPersetujuan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.konfirmasiPersetujuan" %>
<asp:ScriptManagerProxy ID="smpKonfirmasiPersetujuan" runat="server"></asp:ScriptManagerProxy>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkap.ascx" TagName="pdfUsulanLengkap" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkapAbdimas.ascx" TagName="pdfUsulanLengkapAbdimas" TagPrefix="uc" %>

<%--<asp:UpdatePanel ID="upBeranda" runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
<div class="row">
    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="card">
            <div class="card-block">
                <div class="md-card-block">
                    <b>
                        <h5 class="pl-3 pt-3">Persetujuan Sebagai Anggota</h5>
                    </b>
                    <asp:MultiView ID="mvKonfirmasiPersetujuan" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vDaftarUsulanKonfirmasi" runat="server">
                            <asp:ListView ID="lvDaftarUsulanKonfirmasi" runat="server"
                                DataKeyNames="id_personil, id_usulan_kegiatan, judul, 
                                        nama_skema, thn_usulan_kegiatan, thn_pelaksanaan_kegiatan,
                                        nama_ketua, nama_institusi_ketua, kd_jenis_kegiatan"
                                OnItemCommand="lvDaftarUsulanKonfirmasi_ItemCommand">
                                <LayoutTemplate>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr>
                                                <td style="width: 30px; text-align: left; padding: 0;"></td>
                                                <td style="text-align: left; padding: 0;"></td>
                                            </tr>
                                            <tr id="itemPlaceHolder" runat="server"></tr>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lbUnduh" runat="server" Font-Size="50px"
                                                ForeColor="Red"
                                                CssClass="fa fa-file-pdf-o btn btn-default" CommandName="UnduhPdf"
                                                CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Unduh Berkas">
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <h6><b style="color: blue"><%# Eval("judul") %></h6>
                                            </b>            
                                                    <h6 style="color: green"><%# Eval("nama_skema") %></h6>
                                            <b>Thn. Usulan: </b><%# Eval("thn_usulan_kegiatan") %> <b>Thn. Pelaksanaan: </b><%# Eval("thn_pelaksanaan_kegiatan") %><br />
                                            <b>Ketua: </b><%# Eval("nama_ketua") %> <b>Institusi: </b><%# Eval("nama_institusi_ketua") %><br />
                                            <b>Bidang fokus: </b><%# Eval("bidang_fokus") %> <b>Peran: </b><%# Eval("peran_personil") %><br />
                                            <b>Tugas: </b><%# Eval("bidang_tugas") %>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbDisetujui" runat="server" CssClass="btn btn-success fa fa-check waves-effect waves-light"
                                                CommandName="Setuju" CommandArgument="<%# Container.DataItemIndex %>">&nbsp;Disetujui
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbDitolak" runat="server" CssClass="btn btn-danger fa fa-times waves-effect waves-light"
                                                CommandName="Tolak" CommandArgument="<%# Container.DataItemIndex %>">&nbsp;Ditolak
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div class="col-sm-12">
                                        <p class="text-primary">Belum ada data Usulan baru...</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="modal modal-danger" id="modalKonfirmasi" role="dialog" aria-labelledby="mymodalKonfirmasi">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h5 class="modal-title" id="mymodalKonfirmasi">Persetujuan Sebagai Anggota</h5>
            </div>
            <div class="modal-body">
                Apakah anda yakin
                        <b>
                            <asp:Label ID="lblModalInfoStsPersetujuan" runat="server" CssClass="label bg-warning" Text=""></asp:Label></b>
                sebagai anggota pada usulan:<br />
                <asp:Label ID="lblModalJudul" runat="server" ForeColor="Blue" Text=""></asp:Label><br />
                <asp:Label ID="lblModalSkema" runat="server" ForeColor="Green" Text=""></asp:Label><br />
                <b>Thn. usulan:</b>&nbsp;<asp:Label ID="lblModalThnUsulan" runat="server" ForeColor="DarkRed" Text=""></asp:Label>&nbsp;
                        <b>Thn. Pelaksanaan:</b>&nbsp;<asp:Label ID="lblModalThnPelaksanaan" runat="server" ForeColor="DarkRed" Text=""></asp:Label><br />
                <b>Ketua:</b>&nbsp;<asp:Label ID="lblModalKetua" runat="server" Text=""></asp:Label><br />
                <b>Institusi:</b>&nbsp;<asp:Label ID="lblModalInstitusiKetua" runat="server" Text=""></asp:Label>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbModalStsKonfirmasi" runat="server" CssClass="btn btn-success"
                    OnClick="lbModalStsKonfirmasi_Click" OnClientClick="$('#modalKonfirmasi').modal('hide');">
                    <asp:Label ID="lblModalStsKonfirmasi" runat="server" Text=""></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<%--   </ContentTemplate>
</asp:UpdatePanel>--%>

<div>
    <uc:pdfUsulanLengkap runat="server" ID="pdfUsulanLengkap" Visible="false"/>
</div>
<div>
    <uc:pdfUsulanLengkapAbdimas runat="server" ID="pdfUsulanLengkapabdimas" Visible="false"/>
</div>