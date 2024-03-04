<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="listUsulanLanjutanAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.listUsulanLanjutanAbdimas" %>
<asp:ScriptManagerProxy ID="smpListUsulanBaruAbdimas" runat="server"></asp:ScriptManagerProxy>

<div class="row">
    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>
</div>
<asp:MultiView ID="mvListUsulanBaru" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarUsulanBaru" runat="server">
        <section class="panels-wells">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-block">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">

                                    
                                    <div class="panel-heading bg-default txt-white">
                                        Pengajuan Usulan Pengabdian Kepada Masyarakat Lanjutan
                                    </div>
                                    <div class="panel-body">
                                        <asp:ListView ID="lvDaftarUsulanLanjutan" runat="server" Style="width: 100%;"
                                            DataKeyNames="id_usulan,urutan_thn_usulan_kegiatan,id_skema,thn_usulan_kegiatan,thn_pelaksanaan_kegiatan,peran_personil,judul"
                                            OnItemDataBound="lvDaftarUsulanLanjutan_ItemDataBound"
                                            OnItemEditing="lvDaftarUsulanLanjutan_ItemEditing">
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
                                                                &nbsp;&nbsp;                                                                  
                                                                    <%--<asp:LinkButton ID="lbUnduhProposalLengkap" runat="server" CssClass="btn btn-outline-danger fa fa-file-pdf-o waves-effect waves-light"
                                                                    CommandName="unduhProposalLengkap" CommandArgument=<%# Eval("id_usulan_kegiatan") %> ToolTip="Unduh Proposal Lengkap">&nbsp;&nbsp;Unduh Proposal Lengkap
                                                                </asp:LinkButton>&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lbBatalkanusulan" runat="server" CssClass="btn btn-outline-danger fa fa-undo waves-effect waves-light"
                                                                    Visible="false" CommandName="batalkan" CommandArgument=<%# Eval("id_usulan_kegiatan") %> ToolTip="Pembatalan pengiriman usulan">&nbsp;&nbsp;Batalkan
                                                                </asp:LinkButton> --%>                                                                   
                                                            </b></h6>
                                                        </p>
                                                        <h6 style="color: green"><%# Eval("nama_skema") %></h6>&nbsp;<h6>(Tahun ke <%# Eval("urutan_thn_usulan_kegiatan") %> dari <%# Eval("lama_kegiatan") %> tahun)
                                                        <asp:Label ID="lblInfoBatasUsulan" runat="server" Font-Bold="true" ForeColor="Red" Text="(Batas Maksimal Usulan 2 Tahun)" visible="false"
                                                                class="label label-inverse-danger">
                                                            </asp:Label></h6> 
                                                        <%--<h6>--%>
                                                            <%--<i class="fa fa-times" style="color: red"></i>&nbsp;Kuota Usulan Sebagai Ketua&nbsp;--%>
                                                            <%--<b style="color: red">
                                                            
                                                            </b>
                                                        </h6>--%>

                                                        <%--<h6 style="color: darkred"><%# Eval("thn_usulan_dan_pelaksanaan") %></h6>--%>
                                                        <%# Eval("bidang_fokus") %> -
                                                                <asp:Label class="label bg-info" ID="lblPeranPersonil" runat="server" Text='<%# Bind("peran_personil") %>'></asp:Label>&nbsp;
                                                                <%--<asp:Label Visible="false" class="label bg-warning" ID="lblStsKonfirmasi" runat="server" Text='<%# Bind("sts_konfirmasi") %>'></asp:Label><br />
                                                                <asp:Label ID="lblStsApproval" runat="server" Text='<%# Bind("sts_approval") %>'></asp:Label>--%>
                                                    </td>
                                                    <%--<td>
                                                        <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary"
                                                            Visible="false" CommandName="Edit" ToolTip="Edit usulan"><i class="fa fa-pencil fa-2x"></i>
                                                        </asp:LinkButton>

                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete" Visible="false"
                                                            CssClass="btn btn-danger" ToolTip="Hapus usulan">
                                                                    <i class="fa fa-trash fa-2x"></i>
                                                        </asp:LinkButton>
                                                    </td>--%>
                                                    <td>
                                                        <asp:LinkButton ID="lbAjukan" runat="server" CommandName="Edit"
                                                            CssClass="btn btn-success" ToolTip="Ajukan usulan">
                                                                Ajukan >>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <div class="alert alert-light text-primary" role="alert">
                                                        Belum ada data Usulan lanjutan.
                                                    </div>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>



                                    <div class="panel-heading bg-default txt-white">
                                        Daftar Usulan Pengabdian Kepada Masyarakat Lanjutan
                                    </div>
                                    <div class="panel-body">
                                        <asp:ListView ID="lvDaftarUsulanBaru" runat="server"
                                            DataKeyNames="id_usulan_kegiatan, is_sts_ketua, is_dibuka,
                                                    judul, nama_skema, kd_sts_approval,id_transaksi_kegiatan"
                                            OnItemDataBound="lvDaftarUsulanBaru_ItemDataBound"
                                            OnItemEditing="lvDaftarUsulanBaru_ItemEditing"
                                            OnItemDeleting="lvDaftarUsulanBaru_ItemDeleting"
                                            OnItemCommand="lvDaftarUsulanBaru_ItemCommand">
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
                                                                    <asp:LinkButton ID="lbUnduhProposalLengkap" runat="server" CssClass="btn btn-outline-danger fa fa-file-pdf-o waves-effect waves-light"
                                                                        CommandName="unduhProposalLengkap" CommandArgument='<%# Eval("id_usulan_kegiatan") %>' ToolTip="Unduh Proposal Lengkap">&nbsp;&nbsp;Unduh Proposal Lengkap
                                                                    </asp:LinkButton>&nbsp;&nbsp;
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
                                                        <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary"
                                                            Visible="false" CommandName="Edit" ToolTip="Edit usulan"><i class="fa fa-pencil fa-2x"></i>
                                                        </asp:LinkButton>

                                                    </td>
                                                    <%--<td>
                                                        <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete" Visible="false"
                                                            CssClass="btn btn-danger" ToolTip="Hapus usulan">
                                                                    <i class="fa fa-trash fa-2x"></i>
                                                        </asp:LinkButton>
                                                    </td>--%>
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
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </asp:View>
</asp:MultiView>
<div class="modal modal-danger" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalHapus">Konfirmasi Hapus Usulan</h4>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus usulan:<br />
                Judul:&nbsp;<b><asp:Label runat="server" ID="lblModalJudul" Text=""></asp:Label></b><br />
                Skema:&nbsp;<b>
                    <asp:Label runat="server" ID="lblModalSkema" Text=""></asp:Label></b>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapus" runat="server" CssClass="btn btn-success" OnClick="lbHapus_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
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
                    <asp:Label ID="lblJudulDibatalkan" runat="server" Font-Bold="true"></asp:Label></p>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnBatalkanUsulan" runat="server" CssClass="btn btn-danger"
                    Text="Batalkan" OnClick="btnBatalkanUsulan_Click" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Tutup</button>
            </div>
        </div>
    </div>
</div>


<div class="modal fade" id="modalKonfirmasiUsulanLanjutan" tabindex="-1" role="dialog"
    aria-labelledby="modalTitle" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalTitle3">Konfirmasi</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>Apakah Anda yakin akan mengajukan usulan lanjutan ini?</p>
                <p class="text-primary">
                    <asp:Label ID="lblInfoUsulanLanjutan" runat="server" Font-Bold="true"></asp:Label></p>
                <%--<p class="text-primary">
                    <asp:Label ID="lblInfoSkema" runat="server" Font-Bold="true" Text="Jika ingin melanjutkan usulan silahkan pilih skema" Visible="false"></asp:Label></p>
                <p><asp:RadioButtonList runat="server" ID="rblSkemaPasca" Visible="false">
                    <asp:ListItem Text="Penelitian Disertasi Doktor" Value ="6"></asp:ListItem>
                </asp:RadioButtonList></p>--%>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btAjukan" runat="server" CssClass="btn btn-success"
                    Text="Ajukan >>" OnClick="btAjukan_Click" />
                <button type="button" class="btn btn-warning" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>
