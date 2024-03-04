<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dokumenUsulan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.dokumenUsulan" %>
<%@ Register Src="~/Helper/unggahKontrol.ascx" TagPrefix="uc" TagName="ktUnggah" %>
<style type="text/css">
    .auto-style1 {
        width: 10px;
    }
</style>
<div class="row">
    <div class="col-lg-12">
        <div style="margin: 20px;">
            <div class="row" style="margin-bottom: 20px; text-align: right;">
                <div class="col-lg-12">
                    <asp:Label runat="server" ID="lblInfoAtUnggahDokUsulan" Text="Penelitian terapan (tahun ke-1 dari 3 tahun)"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h4>Substansi usulan
                                <asp:Label runat="server" ID="lblJenisKegiatan" Text="penelitian"></asp:Label>
                            </h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:LinkButton runat="server" ID="lbUnduhTemplateDok2" CssClass="mr-2" Text="" Font-Bold="true" OnClick="lbUnduhTemplateDok_Click"><i class="far fa-file-word" 
                                                style="font-size: 60px; color: blue;"></i></asp:LinkButton>
                                            </td>
                                            <td>
                                                <div><b>Template konten dokumen usulan</b></div>
                                                <asp:LinkButton runat="server" ID="lbUnduhTemplateDok" Text="Unduh" Font-Bold="true" OnClick="lbUnduhTemplateDok_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-8">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <b>Dokumen usulan</b>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok_Click">
                                            <i class="far fa-file-pdf" style="font-size: 70px; "></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-md-8">
                                            Tgl. Unggah:&nbsp;<asp:Label runat="server" ID="lblTglUnggah" Text="-"></asp:Label><br />
                                            Ukuran file:&nbsp;<asp:Label runat="server" ID="lblUkuranFile" Text="-"></asp:Label>
                                            <uc:ktUnggah runat="server" ID="ktUnggah" />
                                            <div>
                                                <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">Format file PDF dengan ukuran maks 5MB
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
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
</div>
