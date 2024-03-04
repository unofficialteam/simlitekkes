<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dokumenWbs.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendanaan2021.dokumenWbs" %>

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
                    <asp:Label runat="server" ID="lblInfoAtUnggahDokUsulan" Text="Penelitian terapan (tahun ke-1 dari 3 tahun)" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <%--<h4>Substansi usulan <asp:Label runat="server" ID="lblJenisKegiatan" Text="penelitian"></asp:Label> </h4>--%>
                            <h4>Unggah dokumen WBS (Work Breakdown Structure)</h4>
                        </div>
                        <div class="card-body">

                            <div style="margin: 20px;">
                                <div class="row">
                                    <div class="col-lg-6">
                                    
                                <table>
                                    <%--<tr>
                                        <td style="color: maroon;" colspan="2"><b>Dokumen usulan</b></td>
                                    </tr>--%>
                                    <tr valign="top">
                                        <td class="auto-style1">
                                            <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok_Click">
                                            <i class="fa fa-file-pdf-o" style="font-size: 70px; "></i>
                                            </asp:LinkButton><br />
                                        </td>
                                        <td width="400px">

                                            <div>Tgl. Unggah:&nbsp;<asp:Label runat="server" ID="lblTglUnggah" Text="-"></asp:Label></div>
                                            <div>Ukuran file:&nbsp;<asp:Label runat="server" ID="lblUkuranFile" Text="-"></asp:Label></div>
                                            <div>

                                                <uc:ktUnggah runat="server" ID="ktUnggah" />
                                                <%-- <iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" scrolling="no" style="overflow: hidden;"
                                                clientidmode="Static" runat="server" width="100%" frameborder="0" height="100px"></iframe>--%>
                                            </div>
                                            <div>
                                                <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">Format file PDF dengan ukuran maks 5MB
                                                </span>
                                            </div>

                                        </td>
                                    </tr>
                                </table>

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
