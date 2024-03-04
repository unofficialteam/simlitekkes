<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dokumenPendukungReviewerInternal.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendaftaranReviewer.dokumenPendukungReviewerInternal" %>

<style type="text/css">
    .auto-style1 {
        width: 10px;
    }
</style>
<div class="row">
    <div class="col-lg-12">
        <div style="margin: 20px;">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <div class="col-sm-6">
                                        <asp:Label runat="server" ID="lblJudulPendaftaran" Style="color: orangered; font-size: x-large;">Daftar Ulang Reviewer Internal PT (Penelitian)</asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-inline pull-right" style="text-align: right;">
                                            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True"
                                                CssClass="form-control input-sm" OnSelectedIndexChanged="ddlTahun_SelectedIndexChanged">
                                                <asp:ListItem Text="2019" Value="2019" Selected="True" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <div class="col-sm-8">
                                        <asp:Label runat="server" ID="lblInstitusi" Text="Universitas Suralaya" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                        <asp:Label runat="server" ID="lblProgramStudi" Text="Program Studi Sistem Informasi" ForeColor="DarkGreen" Font-Italic="true"></asp:Label><br />
                                        Pendidikan:
                                        <asp:Label runat="server" ID="lblPendidikan" Text="S-3" Font-Bold="true" ForeColor="DarkGreen"></asp:Label>
                                        -
                                        <asp:Label runat="server" ID="lblJabatanFungsional" Text="Lektor Kepala" Font-Bold="true" ForeColor="DarkGreen"></asp:Label><br />
                                        Status:
                                        <asp:Label runat="server" ID="lblStatus" Text="Aktif Mengajar" Font-Bold="true" ForeColor="DarkGreen"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;h-Index:
                                        <asp:Label runat="server" ID="lblHIndex" Text="2" Font-Bold="true" ForeColor="DarkGreen"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-inline pull-right" style="text-align: right; vertical-align: central;">
                                            <asp:LinkButton runat="server" ID="lbKembali" CssClass="btn btn-primary" OnClick="lbKembali_Click"><i class="fa fa-chevron-left"> Kembali</i></></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body" style="padding-bottom: 30px;">
                            <div style="margin: 20px;">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div><b>Template</b></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbUnduhMotivasi1" Text="" Font-Bold="true" OnClick="lbUnduhMotivasi1_Click">
                                                        <i class="fa fa-file-word-o" style="font-size: 60px; color: blue;"></i></asp:LinkButton>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <div><b>Motivasi</b></div>
                                                    <asp:LinkButton runat="server" ID="lbUnduhMotivasi2" Text="Unduh" Font-Bold="true" OnClick="lbUnduhMotivasi2_Click"></asp:LinkButton>
                                                </td>
                                                <%--<td style="padding-left: 50px">
                                                    <asp:LinkButton runat="server" ID="lbPaktaIntegritas1" Text="" Font-Bold="true" OnClick="lbPaktaIntegritas1_Click">
                                                        <i class="fa fa-file-word-o" style="font-size: 60px; color: blue;"></i></asp:LinkButton>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <div><b>Pakta Integritas</b></div>
                                                    <asp:LinkButton runat="server" ID="lbPaktaIntegritas2" Text="Unduh" Font-Bold="true" OnClick="lbPaktaIntegritas2_Click"></asp:LinkButton>
                                                </td>
                                                <td style="padding-left: 50px">
                                                    <asp:LinkButton runat="server" ID="lbMematuhiKodeEtik1" Text="" Font-Bold="true" OnClick="lbMematuhiKodeEtik1_Click">
                                                        <i class="fa fa-file-word-o" style="font-size: 60px; color: blue;"></i></asp:LinkButton>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <div>
                                                        <b>Pernyataan mematuhi kode etik dan kesanggupan melaksanakan tugas</b>
                                                    </div>
                                                    <asp:LinkButton runat="server" ID="lbMematuhiKodeEtik2" Text="Unduh" Font-Bold="true" OnClick="lbMematuhiKodeEtik2_Click"></asp:LinkButton>
                                                </td>--%>
                                            </tr>
                                        </table>
                                        <div style="height: 40px;"></div>
                                        <table>
                                            <tr>
                                                <td style="color: maroon;" colspan="2"><b style="color: blue; padding-left: 50px;">Dokumen Motivasi Sebagai Reviewer</b></td>
                                            </tr>
                                            <tr style="vertical-align: top">
                                                <td class="auto-style1" style="padding-left: 50px;">
                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDokMotivasi" Text="" ForeColor="gray" OnClick="lbUnduhPdfDokMotivasi_Click" CssClass="fa fa-file-pdf-o" Font-Size="70px">
                                                    </asp:LinkButton><br />
                                                </td>
                                                <td>
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-10">
                                                            <div class="input-group input-group-button input-group-primary">
                                                                <asp:FileUpload runat="server" ID="fileUploadMotivasi" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:LinkButton runat="server" ID="lbUnggahMotivasi" CssClass="btn btn-success" Text="Unggah" OnClick="lbUnggahMotivasi_Click"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran file maksimal 1MB dengan format PDF)
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr style="height: 20px;"></tr>
                                            <%--<tr>
                                                <td style="color: maroon;" colspan="2"><b style="color: blue; padding-left: 50px;">Dokumen Pakta Integritas</b></td>
                                            </tr>
                                            <tr style="vertical-align: top">
                                                <td class="auto-style1" style="padding-left: 50px;">
                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDokPaktaIntegritas" Text="" ForeColor="gray" OnClick="lbUnduhPdfDokPaktaIntegritas_Click" CssClass="fa fa-file-pdf-o" Font-Size="70px">
                                                    </asp:LinkButton><br />
                                                </td>
                                                <td>
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-10">
                                                            <div class="input-group input-group-button input-group-primary">
                                                                <asp:FileUpload runat="server" ID="fileUploadPaktaIntegritas" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:LinkButton runat="server" ID="lbUnggahPaktaIntegritas" CssClass="btn btn-success" Text="Unggah" OnClick="lbUnggahPaktaIntegritas_Click"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran file maksimal 1MB dengan format PDF)
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr style="height: 20px;"></tr>
                                            <tr>
                                                <td style="color: maroon;" colspan="2"><b style="color: blue; padding-left: 50px;">Dokumen Pernyataan mematuhi Kode Etik dan Kesanggupan Melaksanakan Tugas</b></td>
                                            </tr>
                                            <tr style="vertical-align: top">
                                                <td class="auto-style1" style="padding-left: 50px;">
                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDokPernyataan" Text="" ForeColor="gray" OnClick="lbUnduhPdfDokPernyataan_Click" CssClass="fa fa-file-pdf-o" Font-Size="70px">
                                                    </asp:LinkButton><br />
                                                </td>
                                                <td>
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-10">
                                                            <div class="input-group input-group-button input-group-primary">
                                                                <asp:FileUpload runat="server" ID="fileUploadPernyataan" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:LinkButton runat="server" ID="lbUnggahPernyataan" CssClass="btn btn-success" Text="Unggah" OnClick="lbUnggahPernyataan_Click"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran file maksimal 1MB dengan format PDF)
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>--%>
                                            <tr style="height: 20px;"></tr>
                                            <tr>
                                                <td style="color: maroon;" colspan="2"><b style="color: blue; padding-left: 50px;">Dokumen Setifikat Pelatihan Reviewer</b><i style="color: red"> (bila ada)</i></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding-left:50px;">No. Sertifikat:
                                                    <asp:TextBox runat="server" ID="tbNoSertifikat"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="vertical-align: top">
                                                <td class="auto-style1" style="padding-left: 50px;">
                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDokSertifikat" Text="" ForeColor="gray" OnClick="lbUnduhPdfDokSertifikat_Click" CssClass="fa fa-file-pdf-o" Font-Size="70px">
                                                    </asp:LinkButton><br />
                                                </td>
                                                <td style="padding-left: 30px;">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-10">
                                                            <div class="input-group input-group-button input-group-primary">
                                                                <asp:FileUpload runat="server" ID="fileUploadSertifikat" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:LinkButton runat="server" ID="lbUnggahSertifikat" CssClass="btn btn-success" Text="Unggah" OnClick="lbUnggahSertifikat_Click"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran file maksimal 1MB dengan format PDF)
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>

                                        
                                        <div class="col-sm-12" style="height: 20px;">&nbsp;</div>

                                        <div class="col-sm-12">
                                            <b style="color: blue; ">Pernyataan Pakta Integritas</b>
                                        </div>

                                        <div class="col-sm-12">
                                            <span style="color: #555; font-size: 13px; ">Saya akan melakukan penilaian dan menyelesaikan seluruh usulan penelitian dengan mengacu pada Buku Pedoman Penelitian dan Pengabdian kepada Masyarakat yang berlaku. </span>
                                        </div>

                                        <div class="col-sm-12" >
                                            <asp:CheckBox runat="server" ID="cbPernyataanIntegritas" Text="&nbsp;Setuju" ForeColor="Maroon" Font-Bold="true" Font-Size="18px"
                                                AutoPostBack="true" OnCheckedChanged="cbPernyataanIntegritas_CheckedChanged" />
                                        </div>

                                        <div class="col-sm-12" style="height: 20px;">&nbsp;</div>
                                        
                                        <div class="col-sm-12">
                                            <b style="color: blue; ">Pernyataan mematuhi Kode Etik dan Kesanggupan Melaksanakan Tugas</b>
                                        </div>

                                        <div class="col-sm-12">
                                            <span style="color: #555; font-size: 13px; ">Saya akan bekerja secara objektif, jujur dan adil serta akan merahasiakan seluruh proposal dan hasil yang telah saya nilai kepada siapapun. </span>
                                        </div>
                                        <div class="col-sm-12" >
                                        <asp:CheckBox runat="server" ID="cbKodeEtik" Text="&nbsp;Setuju" ForeColor="Maroon" Font-Bold="true" Font-Size="18px"
                                                            AutoPostBack="true" OnCheckedChanged="cbKodeEtik_CheckedChanged" />

                                            </div>
                                        <div class="col-sm-12" style="height: 20px;">&nbsp;</div>
										


                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label runat="server" ID="lblErrorInfo" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
