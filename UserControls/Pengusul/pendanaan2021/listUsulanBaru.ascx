<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="listUsulanBaru.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendanaan2021.listUsulanBaru" %>
<asp:ScriptManagerProxy ID="smpListUsulanBaru" runat="server"></asp:ScriptManagerProxy>

<div class="row">
    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>
</div>
<asp:MultiView ID="mvListUsulanBaru" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarUsulanBaru" runat="server">

        <div class="col-md-12">
            <div class="card mb-4">
                <div class="card-header">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <h5 style="color: darkred">Daftar Usulan Penelitian </h5>
                        </div>
                        <div class="text-right">
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <asp:ListView ID="lvDaftarUsulanBaru" runat="server"
                        DataKeyNames="id_usulan_kegiatan, is_sts_ketua, is_dibuka,
                                                    judul, nama_skema, kd_sts_approval,id_transaksi_kegiatan, alasan_penolakan"
                        OnItemDataBound="lvDaftarUsulanBaru_ItemDataBound"
                        OnItemEditing="lvDaftarUsulanBaru_ItemEditing"
                        OnItemDeleting="lvDaftarUsulanBaru_ItemDeleting" OnItemCommand="lvDaftarUsulanBaru_ItemCommand">
                        <LayoutTemplate>
                            <table class="table table-hover">
                                <tbody>
                                    <tr>
                                        <td style="text-align: left; padding: 0;"></td>
                                        <td style="width: 30px; text-align: left; padding: 0;"></td>
                                        <td style="width: 30px; text-align: left; padding: 0;"></td>
                                    </tr>
                                    <tr id="itemPlaceHolder" runat="server"></tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <p>
                                        <h6><b style="color: blue"><%# Eval("judul") %>&nbsp;&nbsp;  
                                            <asp:LinkButton ID="lbBatalkanusulan" runat="server" CssClass="btn btn-outline-danger fa fa-undo waves-effect waves-light"
                                                Visible="false" CommandName="batalkan" CommandArgument='<%# Eval("id_usulan_kegiatan") %>' ToolTip="Pembatalan pengiriman usulan">&nbsp;&nbsp;Batalkan
                                            </asp:LinkButton>
                                        </b></h6>
                                    </p>
                                    <h6 style="color: green"><%# Eval("nama_skema") %></h6>
                                    <h6 style="color: darkred"><%# Eval("thn_usulan_dan_pelaksanaan") %></h6>
                                    <%# Eval("bidang_fokus") %> -
                                                                <asp:Label class="label bg-info" ID="lblPeranPersonil" runat="server" Text='<%# Bind("peran_personil") %>'></asp:Label>&nbsp;
                                                                <asp:Label Visible="false" class="label bg-warning" ID="lblStsKonfirmasi" runat="server" Text='<%# Bind("sts_konfirmasi") %>'></asp:Label><br />
                                    <asp:Label ID="lblStsApproval" runat="server" Text='<%# Bind("sts_approval") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbEdit" runat="server" ForeColor="Green"
                                        Visible="false" CommandName="Edit" ToolTip="Edit usulan">
                                                                    <i class="fas fa-edit fa-3x"></i>
                                    </asp:LinkButton><div style="height: 5px; width: 5px;"></div>
                                    <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete" Visible="false"
                                        ToolTip="Hapus usulan" ForeColor="Red">
                                                                    <i class="fas fa-trash-alt fa-3x"></i>
                                    </asp:LinkButton>

                                    <asp:Panel runat="server" ID="panelUnduhPdf">
                                        <asp:LinkButton ID="lbUnduhProposalLengkap" runat="server" Font-Size="70px"
                                            CommandName="unduhProposalLengkap" CommandArgument='<%# Eval("id_usulan_kegiatan") %>' Visible="true" ToolTip="Unduh Proposal Lengkap"
                                            ForeColor="Red">
                                                                    <i class="hvr-buzz-out far fa-file-pdf"></i>
                                        </asp:LinkButton>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div class="col-sm-12">
                                <p class="text-primary">Belum ada data Usulan baru...</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>

    </asp:View>
</asp:MultiView>
<div class="modal modal-danger" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" id="dangerModalLabel">Konfirmasi Hapus Usulan</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus usulan:<br />
                Judul:&nbsp;<b><asp:Label runat="server" ID="lblModalJudul" Text=""></asp:Label></b><br />
                Skema:&nbsp;<b>
                    <asp:Label runat="server" ID="lblModalSkema" Text=""></asp:Label></b>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-success" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');" Visible="false">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="modalBatalkanUsulan" tabindex="-1" role="dialog"
    aria-labelledby="modalTitle" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalTitle">Konfirmasi</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>Apakah Anda yakin akan membatalkan pengiriman usulan ini ?</p>
                <p class="text-primary">
                    <asp:Label ID="lblJudulDibatalkan" runat="server" Font-Bold="true"></asp:Label>
                </p>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBatalkanUsulan" runat="server" CssClass="btn btn-danger"
                    Text="Batalkan" OnClick="btnBatalkanUsulan_Click" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Tutup</button>
            </div>
        </div>
    </div>
</div>
