<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pendaftaranReviewerPpm.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendaftaranReviewer.pendaftaranReviewerPpm" %>
<%@ Register Src="~/UserControls/Pengusul/pendaftaranReviewer/persyaratanUmumReviewerPPM.ascx" TagPrefix="uc" TagName="ktSyaratRevPpm" %>
<%@ Register Src="~/UserControls/Pengusul/pendaftaranReviewer/penyajiSeminar.ascx" TagPrefix="uc" TagName="penyajiSeminar" %>
<%@ Register Src="~/UserControls/Pengusul/pendaftaranReviewer/dokumenPendukungReviewerPPM.ascx" TagPrefix="uc" TagName="dokumenPendukung" %>
<%@ Register Src="~/UserControls/Pengusul/pendaftaranReviewer/unggahPresentasiNPoster.ascx" TagPrefix="uc" TagName="unggahPresentasiNPoster" %>
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

                <div class="row">
                    <div class="col-lg-9">
                        <div class="text-left" style="font-size: 24px;">
                            <asp:Label runat="server" ID="lblJudulPendaftranPpm" Text="Pendaftaran Reviewer PPM"></asp:Label>
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
                <div class="md-card-block">

                    <asp:MultiView runat="server" ID="mvMain">
                        <asp:View runat="server" ID="vPersyaratan">
                            <uc:ktSyaratRevPpm runat="server" ID="ktSyaratRevPpm"></uc:ktSyaratRevPpm>
                        </asp:View>
                        <asp:View runat="server" ID="vPenyajiSeminar">
                            <uc:penyajiSeminar runat="server" ID="ktPenyajiSeminar" />


                            <div class="row">
                                <div class="col-lg-12" style="text-align: right;">
                                    <asp:LinkButton runat="server" ID="lbKembali" CssClass="btn btn-info" OnClick="lbKembali_Click">
                                            Kembali
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </asp:View>
                        <asp:View runat="server" ID="vDokumenPendukung">
                            <uc:dokumenPendukung runat="server" ID="ktDokPendukung" />
                        </asp:View>
                        <asp:View runat="server" ID="vUnggahPresentasiNPoster">
                            <uc:unggahPresentasiNPoster runat="server" ID="ktUnggahPresentasiNPoster" />
                        </asp:View>
                    </asp:MultiView>
                </div>

                <asp:Panel runat="server" ID="pnlDaftarNEdit">
                    <asp:MultiView runat="server" ID="mvDaftar">
                        <asp:View runat="server" ID="vDaftar">
                            <div class="panel panel-default" style="padding: 10px;">
                                <div class="panel-body">
                                    <div class="row" style="padding: 10px;">
                                        <div class="col-sm-3">
                                            Mendaftar sebagai Reviewer PPM: 
                                        </div>
                                        <div class="col-sm-2">

                                            <div class="form-check">
                                                <label class="form-check-label" for="cbMonoThn">
                                                    <asp:CheckBox runat="server" ID="cbMonoThn" Text="&nbsp;Mono Tahun" ClientIDMode="Static" /><br />
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-check">
                                                <label class="form-check-label" for="cbMultiThn">
                                                    <asp:CheckBox runat="server" ID="cbMultiThn" Text="&nbsp;Multi Tahun" ClientIDMode="Static" />
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:LinkButton runat="server" ID="lbDaftar" CssClass="btn btn-info" OnClick="lbDaftar_Click">
                                Daftar
                                            </asp:LinkButton>
                                            <asp:Label runat="server" ID="lblDaftar" CssClass="btn btn-disabled disabled" Visible="false" 
                                                ToolTip="Bukan dalam jadwal pendaftaran/eligibilitas sebagai reviewer belum terpenuhi.">
                            Daftar
                                            </asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View runat="server" ID="vEdit">

                            <div class="panel panel-default" style="padding: 10px;">
                                <div class="panel-body">
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

                                            <%--<asp:Panel runat="server" ID="panelPenyajiSeminarEdit4RevBaru">
                                                    PENYAJI SEMINAR: 
                                                <asp:LinkButton runat="server" ID="lbEditPenyajiSeminar" CssClass="btn btn-info" OnClick="lbEditPenyajiSeminar_Click" Font-Italic="true">
                                                    <i class="fa fa-pencil"></i>&nbsp;&nbsp;Edit
                                                </asp:LinkButton>
                                            </asp:Panel>--%>
                                        </div>
                                    </div>


                                    <div class="row" style="padding: 10px;">
                                        <div class="col-lg-3">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lbUnduhPdfDokMotivasi" Text="" Font-Bold="true" OnClick="lbUnduhMotivasi_Click" ForeColor="Gray">
                                                        <i class="fa fa-file-pdf-o" style="font-size: 60px;"></i></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lbUnduhPdfDokMotivasi2" Text="" Font-Bold="true" OnClick="lbUnduhMotivasi_Click">
                                                        Motivasi sebagai reviewer</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-lg-3">

                                            <%--<table>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lbUnduhPdfDokPaktaIntegritas" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokPaktaIntegritas_Click" ForeColor="Gray">
                                                        <i class="fa fa-file-pdf-o" style="font-size: 60px;"></i></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lbUnduhPdfDokPaktaIntegritas2" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokPaktaIntegritas_Click">
                                                        Pakta integritas</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>--%>
                                        </div>
                                        <div class="col-lg-6">

                                            <%--                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lbUnduhPdfDokPernyataan" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokPernyataan_Click" ForeColor="Gray">
                                                        <i class="fa fa-file-pdf-o" style="font-size: 60px;"></i></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lbUnduhPdfDokPernyataan2" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokPernyataan_Click">
                                                        Pernyataan mematuhi kode etik & kesanggupan melaksanakan tugas</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>--%>
                                        </div>
                                    </div>

                                    <%--<asp:Panel runat="server" ID="pnlUtkDokRevBaru" Visible="false">

                                        <div class="row" style="padding: 10px;">
                                            <div class="col-lg-3">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDokPengalaman" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokPengalaman_Click" ForeColor="Gray">
                                                        <i class="fa fa-file-pdf-o" style="font-size: 60px;"></i></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDokPengalaman2" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokPengalaman_Click">
                                                        Pengalaman sebagai reviewer internal PT</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-lg-9">

                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDokNarasumber" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokNarasumber_Click" ForeColor="Gray">
                                                        <i class="fa fa-file-pdf-o" style="font-size: 60px; "></i></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDokNarasumber2" Text="" Font-Bold="true" OnClick="lbUnduhPdfDokNarasumber_Click">
                                                        Sebagai nara sumber dalam kegiatan sosialisasi program PPM di PT atau LLDIKTI</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                    </asp:Panel>--%>

                                    <div class="row" style="padding: 10px;">

                                        <div class="col-lg-4">
                                            <hr />
                                        </div>
                                        <div class="col-lg-8">
                                        </div>
                                    </div>
                                    <div class="row" style="padding: 10px;">
                                        <div class="col-lg-4">
                                            <span class="text-primary" style="font-weight: bold; margin-right: 15px;">Mendaftar sebagai Reviewer PPM:</span>

                                        </div>

                                        <div class="col-sm-2">
                                            <div class="form-check">
                                                <label class="form-check-label" for="cbMonoEdit">
                                                    <asp:CheckBox runat="server" ID="cbMonoEdit" Text="&nbsp;Mono Tahun" Font-Bold="true" Font-Size="Larger" /><br />
                                                </label>
                                            </div>
                                        </div>

                                        <div class="col-sm-2">
                                            <div class="form-check">
                                                <label class="form-check-label" for="cbMultiEdit">
                                                    <asp:CheckBox runat="server" ID="cbMultiEdit" Text="&nbsp;Multi Tahun" Font-Bold="true" Font-Size="Larger" /><br />
                                                </label>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <span style="margin: 5px;">
                                                <asp:LinkButton runat="server" ID="lbEditMonoMulti" CssClass="btn btn-info" OnClick="lbEditMonoMulti_Click" Font-Italic="true">
                                            <i class="fa fa-pencil"></i>&nbsp;&nbsp;Update
                                                </asp:LinkButton>
                                            </span>
                                        </div>
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
                                                <asp:Label runat="server" ID="lblHeaderInfoPendaftaran" Text="Isian pendaftaran telah lengkap." Font-Size="Larger"></asp:Label><br />
                                                <asp:Label runat="server" ID="lblInfoPendaftaran" Text="Sedang menunggu proses seleksi."></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:LinkButton runat="server" ID="lbBatalkan" CssClass="btn btn-danger" OnClick="lbBatalkan_Click">
                                                        <i class="fa fa-trash"></i>&nbsp;&nbsp;Batalkan Pendaftaran
                                            </asp:LinkButton><div style="height: 10px;"></div>
                                                    <asp:LinkButton runat="server" ID="lbKirimPendaftaran" CssClass="btn btn-success" OnClick="lbKirimPendaftaran_Click"  Width="220px">
                                                        <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Pendaftaran
                                                    </asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </asp:View>

                    </asp:MultiView>
                </asp:Panel>
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


