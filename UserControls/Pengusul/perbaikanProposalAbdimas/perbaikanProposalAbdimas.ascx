<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="perbaikanProposalAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.perbaikanProposalAbdimas.perbaikanProposalAbdimas" %>
<%@ Register Src="~/UserControls/Pengusul/perbaikanProposalAbdimas/perbaikanUsulanAbdimas.ascx" TagName="listUsulan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/perbaikanProposalAbdimas/rekapUsulanPerbaikanAbdimas.ascx" TagName="kirimUsulan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/perbaikanProposalAbdimas/dokumenUsulanAbdimas.ascx" TagName="dokumenUsulan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/perbaikanProposalAbdimas/rabRevisiAbdimas.ascx" TagPrefix="uc" TagName="rabRevisi" %>
<%@ Register Src="~/UserControls/Pengusul/perbaikanProposalAbdimas/rekapLuaranPerbaikanAbdimas.ascx" TagPrefix="uc" TagName="rekapLuaran" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkapPerbaikan.ascx" TagPrefix="uc" TagName="pdfUsulanPerbaikan" %>

<div class="row">
    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>
</div>
<!-- Row end -->

<div class="container-fluid">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-block">
                    <div class="md-card-block">
                        <asp:MultiView runat="server" ID="mvMain">
                            <asp:View runat="server" ID="vListUsulan">
                                <uc:listUsulan runat="server" ID="ktListUsulan" />
                                <uc:pdfusulanperbaikan runat="server" id="ktPdfLengkap"></uc:pdfusulanperbaikan>
                            </asp:View>
                            <asp:View runat="server" ID="vWizardUsulan">
                                <div class="card-header col-md-12" style="text-align: right;">
                                    <span style="width: 100px;"></span>
                                    <asp:LinkButton runat="server" ID="lbSubstansi1" Style="margin-left: 25px;" OnClick="lbSubstansi1_Click">Substansi Usulan</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbSubstansi2" Text="1" CssClass="btn btn-outline-primary" Style="border-radius: 50%;"
                                        OnClick="lbSubstansi1_Click" />

                                    <span style="width: 100px;"></span>
                                    <asp:LinkButton runat="server" ID="lbRab1" Style="margin-left: 25px;" OnClick="lbRab1_Click">RAB</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbRab2" Text="2" CssClass="btn btn-outline-primary" Style="border-radius: 50%;"
                                        OnClick="lbRab1_Click" />

                                    <span style="width: 100px;"></span>
                                    <asp:LinkButton runat="server" ID="lbKirimUsulan1" Style="margin-left: 25px;" OnClick="lbKirimUsulan1_Click">Kirim Usulan</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbKirimUsulan2" Text="3" CssClass="btn btn-outline-primary" Style="border-radius: 50%;"
                                        OnClick="lbKirimUsulan1_Click" />

                                </div>

                                <div class="row">
                                    <div class="col-lg-6" style="font-weight: bold; color: maroon; font-size: 18px;">
                                        PERBAIKAN USULAN
                                    </div>
                                    <div class="col-lg-6" style="text-align: right;">
                                        <asp:Label runat="server" ID="lblInfoPenelitianSbk" Text=""></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <table style="width: 100%; margin: 10px;">
                                            <tr>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok_Click">
                                                        <i class="fa fa-file-pdf-o" style="font-size: 60px; "></i></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblJudul" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" ID="lblInfoSkema" Text="" ForeColor="Green" Font-Bold="true"></asp:Label>
                                        <asp:Label runat="server" ID="lblInfoThn" Text="" ForeColor="Maroon" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>

                                <!-- WIZARD  -->
                                <div class="card-body">
                                    <%--style="height: 300px;"--%>
                                    <asp:MultiView runat="server" ID="mvWizard">
                                        <asp:View runat="server" ID="vDokumenUsulan">
                                            <uc:dokumenUsulan runat="server" ID="ktDokumenUsulan"></uc:dokumenUsulan>
                                        </asp:View>
                                        <asp:View runat="server" ID="vRekapLuaran">
                                            <uc:rekapLuaran runat="server" ID="ktRekapLuaran"></uc:rekapLuaran>
                                        </asp:View>
                                        <asp:View runat="server" ID="vRab">
                                            <uc:rabRevisi runat="server" ID="rabRevisiControl" />
                                        </asp:View>
                                        <asp:View runat="server" ID="vSubmitUsulan">
                                            <uc:kirimUsulan runat="server" ID="ktKirimUsulan" />
                                        </asp:View>
                                    </asp:MultiView>
                                </div>

                                <!-- END WIZARD  -->
                                <div class="card-footer col-md-12" style="text-align: right;">
                                    <asp:LinkButton runat="server" ID="lbLanjutkan" CssClass="btn btn-success" OnClick="lbLanjutkan_Click"> <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan </asp:LinkButton>
                                    <asp:Label runat="server" ID="lblKirimUsulan" CssClass="btn btn-secondary"
                                        Visible="false" ToolTip="Isian usulan belum lengkap"> <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Usulan </asp:Label>
                                    <asp:LinkButton runat="server" ID="lbKirimUsulan" CssClass="btn btn-success" OnClick="lbSubmitUsulan_Click"
                                        Visible="false"> <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Usulan </asp:LinkButton>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
