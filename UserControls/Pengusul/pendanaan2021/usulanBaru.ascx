<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="usulanBaru.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendanaan2021.usulanBaru" %>

<%--<%@ Register Src="~/UserControls/Pengusul/dokumenUsulan.ascx" TagPrefix="uc" TagName="dokUsulan" %>--%>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/dokumenUsulan2019.ascx" TagPrefix="uc" TagName="dokUsulan" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/persyaratanUmum.ascx" TagPrefix="uc" TagName="persyaratan" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/IdentitasUsulan.ascx" TagPrefix="uc" TagName="IdentitasUsulan" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/cvKetua.ascx" TagPrefix="uc" TagName="cvKetua" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/anggotaPeneliti.ascx" TagPrefix="uc" TagName="anggotaPeneliti" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/targetLuaran2019.ascx" TagPrefix="uc" TagName="ktLuaran" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/listUsulanBaru.ascx" TagPrefix="uc" TagName="lstUsulanBaru" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/rab.ascx" TagPrefix="uc" TagName="rab" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/mitraPenelitian.ascx" TagPrefix="uc" TagName="mitraPenelitian" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/rekapUsulan.ascx" TagName="rekapUsulan" TagPrefix="uc" %>
<%--<%@ Register Src="~/UserControls/Pengusul/cvKetua.ascx" TagPrefix="uc" TagName="cvKetua" %>--%>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkap.ascx" TagName="pdfUsulanLengkap" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/pendanaan2021/dokumenWbs.ascx" TagName="dokumenWbs" TagPrefix="uc" %>
<asp:scriptmanagerproxy id="smpUsulanBaru" runat="server"></asp:scriptmanagerproxy>

<div class="row">
    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>
</div>
<!-- Row end -->

<asp:multiview runat="server" id="mvMain">

    <asp:View runat="server" ID="vPersyaratan">

        <uc:persyaratan runat="server" ID="persyaratan" />

        <div class="col-md-12">
            <div class="card mb-4">
                <div class="card-body">

                    <div class="d-flex justify-content-between align-items-center">
                        <div class="form-inline">
                            <label for="inputPassword">Tahun Usulan: </label>
                            <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="true" Width="80px"
                                CssClass="form-control basic-single mr-sm-2" OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                                <asp:ListItem Text="2020" Value="2020" Selected="True"></asp:ListItem>
                               
                            </asp:DropDownList>
                            <label for="inputPassword">Tahun Pelaksanaan: </label>
                            <asp:DropDownList ID="ddlThnPelaksanaanKegiatan" runat="server" AutoPostBack="true" Width="80px"
                                CssClass="form-control basic-single mr-sm-2" OnSelectedIndexChanged="ddlThnPelaksanaanKegiatan_SelectedIndexChanged">
                                <asp:ListItem Text="2021" Value="2021" Selected="True"></asp:ListItem>
                                
                            </asp:DropDownList>
                        </div>
                        <div class="text-right">
                            <asp:Label ID="lblPengajuanBaru" runat="server" CssClass="btn btn-default waves-effect waves-light f-right"
                                Text="Lanjutkan" Visible="false">
                            </asp:Label>
                            <asp:LinkButton runat="server" ID="lbPengajuanBaru" CssClass="btn btn-success" OnClick="lbLanjutkanAtPersyaratanUmum_Click">
                                                                Lanjutkan
                            </asp:LinkButton>
                        </div>
                    </div>


                </div>
            </div>
        </div>

        <uc:lstUsulanBaru runat="server" ID="lstUsulanBaru"></uc:lstUsulanBaru>

    </asp:View>

    <asp:View runat="server" ID="vCVKetua">
        <div class="card-body">
            <uc:cvKetua runat="server" ID="cvKetua" />
        </div>
        <div class="card-footer col-md-12" style="text-align: right;">
            <asp:LinkButton runat="server" ID="lbLanjutkanAtCVKetua" CssClass="btn btn-success" OnClick="lbLanjutkanAtCVKetua_Click">
                                                <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
            </asp:LinkButton>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vUsulan">

        <!-- Progress wizard -->
        <div class="card mb-4">

            <div class="card-header col-md-12" style="text-align: right;">
                <asp:LinkButton runat="server" ID="lbIdentitas1" OnClick="lbLanjutkanAtCVKetua_Click">
                                            Identitas Usulan
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbIdentitas2" Text="1" CssClass="btn btn-outline-primary" Style="border-radius: 50%;"
                    OnClick="lbLanjutkanAtCVKetua_Click" />

                <span style="width: 100px;"></span>
                <asp:LinkButton runat="server" ID="lbSubstansi1" OnClick="lbLanjutkanAtAnggota_Click" Style="margin-left: 25px;">
                                            Substansi Usulan
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbSubstansi2" Text="2" CssClass="btn btn-outline-primary" Style="border-radius: 50%;"
                    OnClick="lbLanjutkanAtAnggota_Click" />

                <span style="width: 100px;"></span>
                <asp:LinkButton runat="server" ID="lbRab1" OnClick="lbLanjutkanAtUnggahDokLuaran_Click" Style="margin-left: 25px;">
                                            RAB
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbRab2" Text="3" CssClass="btn btn-outline-primary" Style="border-radius: 50%;"
                    OnClick="lbLanjutkanAtUnggahDokLuaran_Click" />

                <span style="width: 100px;"></span>
                <asp:LinkButton runat="server" ID="lbDokPendukung1" OnClick="lbLanjutkanAtIsiRab_Click" Style="margin-left: 25px;">
                                            Dokumen Pendukung
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbDokPendukung2" Text="4" CssClass="btn btn-outline-primary" Style="border-radius: 50%;"
                    OnClick="lbLanjutkanAtIsiRab_Click" />
                <span style="width: 100px;"></span>
                <asp:LinkButton runat="server" ID="lbKirimUsulan1" OnClick="lbLanjutkanAtDataPendukung_Click" Style="margin-left: 25px;">
                                            Kirim Usulan
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbKirimUsulan2" Text="5" CssClass="btn btn-outline-primary" Style="border-radius: 50%;"
                    OnClick="lbLanjutkanAtDataPendukung_Click" />
            </div>
        </div>
        <div>
            <asp:Panel runat="server" ID="panelUsulan">

                <asp:MultiView runat="server" ID="mvUsulan">

                    <asp:View runat="server" ID="vIdentitas">

                        <asp:MultiView runat="server" ID="mvIdentitas">
                            <asp:View runat="server" ID="vIDUsulan">
                                <!-- 1.1 -->

                                <uc:IdentitasUsulan runat="server" ID="IdentitasUsulan" />

                                <div class="card mb-4">
                                    <div class="card-footer text-right">
                                        <asp:LinkButton runat="server" ID="lbLanjutkanAtIDUsulan" CssClass="btn btn-success"
                                            OnClick="lbLanjutkanAtIDUsulan_Click">
                                                                        <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View runat="server" ID="vCVAnggota">
                                <!-- 1.2 -->
                                <uc:anggotaPeneliti runat="server" ID="ktAnggota" />

                                <div class="card mb-4">
                                    <div class="card-footer text-right">
                                        <%--<asp:LinkButton runat="server" ID="lbLanjutkanAtAnggota" CssClass="btn btn-success" OnClick="lbLanjutkanAtAnggota_Click">
                                                                    <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                        </asp:LinkButton>--%>
                                        <asp:LinkButton runat="server" ID="lbLanjutkanAtAnggota" CssClass="btn btn-success"
                                            OnClick="lbLanjutkanAtAnggota_Click">
                                            <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>

                    </asp:View>

                    <asp:View runat="server" ID="vSubstansi">
                        <asp:MultiView runat="server" ID="mvSubstansi">
                            <asp:View runat="server" ID="vUnggahDokUsulan">
                                <!-- 1.2 -->
                                <uc:dokUsulan runat="server" ID="ktDokUsulan" />

                                <div class="card mb-4">
                                    <div class="card-footer text-right">
                                        <asp:LinkButton runat="server" ID="lbLanjutkanAtUnggahDokUsulan" CssClass="btn btn-success" OnClick="lbLanjutkanAtUnggahDokUsulan_Click">
                                                                    <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </asp:View>

                            <asp:View runat="server" ID="vLuaran">
                                <!-- 1.2 -->
                                <uc:ktLuaran runat="server" ID="ktLuaran" />
                                <div class="card mb-4">
                                    <div class="card-footer text-right">
                                        <asp:LinkButton runat="server" ID="lbLanjutkanAtUnggahDokLuaran" CssClass="btn btn-success" OnClick="lbLanjutkanAtUnggahDokLuaran_Click">
                                                                    <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>

                    <asp:View runat="server" ID="vRAB">
                        <asp:MultiView runat="server" ID="mvRAB">
                            <asp:View runat="server" ID="vRABIsi">
                                <!-- 1.2 -->
                                <uc:rab runat="server" ID="rab" />
                                <div class="card mb-4">
                                    <div class="card-footer text-right">
                                        <asp:LinkButton runat="server" ID="lbLanjutkanAtIsiRab" CssClass="btn btn-success"
                                            OnClick="lbLanjutkanAtIsiRab_Click">
                                              <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>

                    <asp:View runat="server" ID="vUnggahDokPendukung">
                        <asp:MultiView runat="server" ID="mvDokPendukung">
                            <asp:View runat="server" ID="vUnggahDokPendukungIsi">
                                <!-- 4.2 -->
                                <uc:dokumenWbs runat="server" ID="ktDokumenWbs" Visible="false" />
                                <uc:mitraPenelitian runat="server" ID="mitraPenelitian" />

                                <div class="card mb-4">
                                    <div class="card-footer text-right">
                                        <asp:LinkButton runat="server" ID="lbLanjutkanAtDataPendukung" CssClass="btn btn-success" OnClick="lbLanjutkanAtDataPendukung_Click">
                                                                        <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </asp:View>


                    <asp:View runat="server" ID="viewKirimUsulan">

                        <uc:rekapUsulan runat="server" ID="ktRekapUsulan" />

                        <div class="card mb-4">
                            <div class="card-footer">
                                <div class="d-flex justify-content-between align-items-center">
                                    <div>

                                        <table style="width: 50%;" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDokLengkap" Text="" ForeColor="Red" Visible="false"
                                                        OnClick="lbUnduhPdfDokLengkap_Click" Width="40px" Height="50px">
                                                        <i class="hvr-buzz-out far fa-file-pdf" style="font-size: 45px; "></i>
                                                    </asp:LinkButton>
                                                </td>
                                                <td>

                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDokLengkap2" Text="" ForeColor="Red" Visible="false"
                                                        OnClick="lbUnduhPdfDokLengkap_Click">
                                            Unduh<br />Dokumen usulan lengkap
                                                    </asp:LinkButton>

                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                    <div class="text-right">
                                        <h5 class="card-header-text f-right">
                                            <asp:LinkButton runat="server" ID="lbSubmitUsulan" CssClass="btn btn-success" OnClick="lbSubmitUsulanModal_Click">
                                                                    <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Usulan
                                            </asp:LinkButton>
                                            <asp:Label runat="server" ID="lblSubmitUsulan" CssClass="btn btn-secondary" ToolTip="Usulan belum lengkap">
                                                    <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Usulan
                                            </asp:Label>

                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                </asp:MultiView>

            </asp:Panel>
        </div>
    </asp:View>
</asp:multiview>

<div class="modal fade" id="modalKonfirmasiKirim" tabindex="-1" role="dialog"
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
                <p>
                    Apakah Anda yakin akan mengirim usulan ini?
                    <br />
                    Usulan tidak dapat diubah jika status sudah dikirim
                </p>
                <p class="text-primary">
                    <asp:label id="lblJudulDiKirim" runat="server" font-bold="true"></asp:label>
                </p>
            </div>
            <div class="modal-footer">

                <asp:linkbutton runat="server" id="lbKirimUsulan" cssclass="btn btn-success" onclick="lbSubmitUsulan_Click">
                                                                    <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Usulan
                </asp:linkbutton>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>


<%--</ContentTemplate>
</asp:UpdatePanel>--%>

<div>
    <uc:pdfusulanlengkap runat="server" id="pdfUsulanLengkap" visible="false" />
</div>
