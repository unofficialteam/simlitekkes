<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pendaftaranReviewerInternal.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendaftaranReviewer.pendaftaranReviewerInternal" %>
<%@ Register Src="~/UserControls/Pengusul/pendaftaranReviewer/persyaratanUmumReviewerInternal.ascx" TagPrefix="uc" TagName="ktSyaratUmumRevPenelitian" %>
<%@ Register Src="~/UserControls/Pengusul/pendaftaranReviewer/dokumenPendukungReviewerInternal.ascx" TagPrefix="uc" TagName="dokumenPendukung" %>
<%@ Register Src="~/UserControls/Pengusul/pendaftaranReviewer/karyaSeni.ascx" TagPrefix="uc" TagName="ktkaryaSeniMonumental" %>

<section class="content-header">
</section>
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

                    <div class="row">
                        <div class="col-lg-9">
                            <div class="text-left" style="font-size: 24px;">
                                <asp:Label runat="server" ID="lblJudulForm" Text="Daftar Ulang Reviewer Internal PT (Penelitian)" ForeColor="Orange" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 text-right">
                            <asp:DropDownList runat="server" ID="ddlTahunUsulan" CssClass="custom-select" 
                                OnSelectedIndexChanged="ddlTahunUsulan_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <hr />

                    <asp:MultiView runat="server" ID="mvMain">
                        <asp:View runat="server" ID="vPenelitian">
                            <uc:ktSyaratUmumRevPenelitian runat="server" ID="ktSyaratUmumRevPenelitian"></uc:ktSyaratUmumRevPenelitian>
                        </asp:View>
                        <asp:View runat="server" ID="vKaryaSeniMonumental">
                            <uc:ktkaryaSeniMonumental runat="server" ID="ktkaryaSeniMonumental" />
                            <div class="row">
                                <div class="col-lg-12" style="text-align: right;">
                                    <asp:LinkButton runat="server" ID="lbKembali" CssClass="btn btn-info" OnClick="lbKembali_Click">
                                            ;&nbsp;&nbsp;Kembali
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View runat="server" ID="vDokumenPendukung">
                            <uc:dokumenPendukung runat="server" ID="ktDokPendukung" />
                        </asp:View>
                    </asp:MultiView>

                    <asp:Panel runat="server" ID="pnlDaftarNEdit">
                        <asp:MultiView runat="server" ID="mvDaftar">
                            <asp:View runat="server" ID="vDaftar">
                                <asp:UpdatePanel runat="server" ID="upKepakaran">
                                    <ContentTemplate>
                                        <div class="panel panel-default" style="padding: 10px;">
                                            <div class="panel-body">
                                                <div class="row" style="padding: 10px;">
                                                    <div class="col-lg-2">
                                                        Bidang kepakaran: 
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <link rel="stylesheet" href="assets/select/css/bootstrap-select.min.css"> 
                                                        <asp:DropDownList runat="server" ID="ddlKepakaran" CssClass="selectpicker" Width="500px" data-live-search="true" 
                                                            data-size="8" ClientIDMode="Static"
                                                            DataTextField="rumpun_ilmu" DataValueField="id_rumpun_ilmu">
                                                            <asp:ListItem Text="-- Pilih --" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:LinkButton runat="server" ID="lbTambahKepakaran" CssClass="btn btn-success" OnClick="lbTambahKepakaran_Click">
                                                            Tambah
                                                        </asp:LinkButton>
                                                        <script src="assets/select/js/bootstrap-select.min.js"></script>
                                                        <script type="text/javascript">
                                                            function pageLoad() {
                                                                $('.selectpicker').selectpicker(); 
                                                            }
                                                        </script>
                                                        <br />
                                                        <table>
                                                            <tr>
                                                                <td>

                                                                    <asp:ListBox runat="server" ID="lstKepakaran" Width="500px" CssClass="scroll-list" Style="margin-top: 10px;"></asp:ListBox>
                                                                </td>
                                                                <td style="vertical-align: middle;">
                                                                    <asp:LinkButton runat="server" ID="lbHapusKepakaran" CssClass="btn btn-outline-warning btn-mini" OnClick="lbHapusKepakaran_Click">
                                                                    Hapus semua
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                        <br />
                                                        <asp:LinkButton runat="server" ID="lbDaftar" CssClass="btn btn-info" OnClick="lbDaftar_Click">
                                                                Daftar
                                                        </asp:LinkButton>
                                                        <asp:Label runat="server" ID="lblDaftar" CssClass="btn btn-disabled disabled" Visible="false" Style="margin-top: 20px;" 
                                                            ToolTip="Bukan dalam jadwal pendaftaran/anda belum terdaftar sebagai reviewer internal.">
                                                                Daftar
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:View>
                            <asp:View runat="server" ID="vEdit">
                                <div class="panel panel-default" style="padding: 10px;">
                                    <div class="panel-body">           
                                        
                                        <div class="row" style="padding: 10px;">
                                            <div class="col-lg-2">
                                                Bidang kepakaran:&nbsp;&nbsp;
                                            </div>
                                            <div class="col-lg-10">
                                                <div class="form-inline">
                                                    <asp:Label runat="server" ID="lblBidangKepakaran"></asp:Label>&nbsp;&nbsp;
                                                <asp:LinkButton runat="server" ID="lbEditBidKepakaran" CssClass="btn btn-info" OnClick="lbEditBidKepakaran_Click1" Font-Italic="true">
                                                    <i class="fa fa-pencil"></i>&nbsp;&nbsp;Edit
                                                </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="padding: 10px;">
                                            <div class="col-lg-4">
                                                <hr />
                                            </div>
                                            <div class="col-lg-8">
                                            </div>
                                        </div>
                                       
                                        <div class="row" style="padding: 10px;">
                                            <div class="col-lg-4 font-weight-bold">
                                                DOKUMEN PENDUKUNG: 
                                        <asp:LinkButton runat="server" ID="lbEdit" CssClass="btn btn-info" OnClick="lbEdit_Click" Font-Italic="true">
                                            <i class="fa fa-pencil"></i>&nbsp;&nbsp;Edit
                                        </asp:LinkButton>
                                            </div>
                                            <div class="col-lg-8">
                                            </div>
                                        </div>
                                        
                                        <div class="row" style="padding: 10px;">
                                            <div class="col-lg-3">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDokMotivasi" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokMotivasi_Click" ForeColor="Gray">
                                                        <i class="fa fa-file-pdf-o" style="font-size: 60px;"></i></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDokMotivasi2" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokMotivasi2_Click">
                                                        Motivasi sebagai reviewer</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-lg-3">

                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton runat="server" ID="lbUnduhPdfDokSertifikat" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokSertifikat_Click" ForeColor="Gray">
                                                        <i class="fa fa-file-pdf-o" style="font-size: 60px;"></i></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton runat="server" ID="lbUnduhPdfDokSertifikat2" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokSertifikat2_Click">
                                                        Dokumen Sertifikat Pelatihan Reviewer</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                               
                                            </div>
                                            <div class="col-lg-6">

                                            </div>
                                        </div>
                                        

                                        <asp:Panel runat="server" ID="panelKelengkapan">

                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <hr />
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 10px;">
                                                <div class="col-lg-6">
                                                    <div style="background-color: #ffffcc;" class="alert alert-light" role="alert">
                                                        <asp:Label runat="server" ID="lblHeaderInfoPendaftaran" Text="Isian pendaftaran telah lengkap." Font-Size="Larger"></asp:Label>&nbsp;
                                                        <%--<asp:LinkButton runat="server" ID="lbEdit" CssClass="btn btn-info" OnClick="lbEdit_Click" Font-Italic="true">
                                                            <i class="fa fa-pencil"></i>&nbsp;&nbsp;Edit
                                                        </asp:LinkButton>--%>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblInfoPendaftaran" Text="Sedang menunggu proses seleksi."></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:LinkButton runat="server" ID="lbBatalkan" CssClass="btn btn-danger" OnClick="lbBatalkan_Click" Width="220px">
                                                        <i class="fa fa-trash"></i>&nbsp;&nbsp;Batalkan Pendaftaran
                                                    </asp:LinkButton> <div style="height: 10px;"></div>
                                                    <asp:LinkButton runat="server" ID="lbKirimPendaftaran" CssClass="btn btn-success" OnClick="lbKirimPendaftaran_Click"  Width="220px">
                                                        <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Pendaftaran
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    


        
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</div>


<div class="modal fade" id="modalHapusPendaftaran" tabindex="-1" role="dialog"
    aria-labelledby="modalTitle" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalTitle"> 
                    <asp:Label runat="server" ID="lblModalTitle" Text="Konfirmasi pembatalan pendaftaran"></asp:Label>
                    </h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>
                    
                    
                    <asp:Label runat="server" ID="lblModalInfo" Text="Apakah Anda yakin akan membatalkan usulan pendaftaran sebagai reviewer ini?<br />Data isian dan dokumen akan dihapus."></asp:Label>
                </p>
                <p class="text-primary">
                    <%--<asp:Label ID="lblJudulDiKirim" runat="server" Font-Bold="true"></asp:Label>--%>
                </p>
            </div>
            <div class="modal-footer">

                <asp:LinkButton runat="server" ID="lbHapusPendaftaranModal" CssClass="btn btn-danger" OnClick="lbHapusPendaftaranModal_Click">
                                                                    <i class="fa fa-trash"></i>&nbsp;&nbsp;Hapus pendaftaran
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbKirimPendaftaranModal" CssClass="btn btn-success" OnClick="lbKirimPendaftaranFinal_Click">
                                                                    <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim pendaftaran
                </asp:LinkButton>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>

