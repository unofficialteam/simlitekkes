<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="usulanBaruPengabdian.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.usulanBaruPengabdian" %>
<%@ Register Src="~/UserControls/Pengusul/cvKetuaAbdimas.ascx" TagPrefix="uc" TagName="cvKetuaAbdimas" %>
<%@ Register Src="~/UserControls/Pengusul/IdentitasUsulanAbdimas.ascx" TagPrefix="uc" TagName="IdentitasUsulanAbdimas" %>
<%@ Register Src="~/UserControls/Pengusul/anggotaAbdimas.ascx" TagPrefix="uc" TagName="anggotaAbdimas" %>
<%@ Register Src="~/UserControls/Pengusul/persyaratanUmumAbdimas.ascx" TagName="persyaratan" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/listUsulanBaruAbdimas.ascx" TagPrefix="uc" TagName="lstUsulanBaru" %>
<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkapAbdimas.ascx" TagName="pdfUsulanLengkap" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/mitraAbdimas.ascx" TagPrefix="uc" TagName="mitraAbdimas" %>
<%@ Register Src="~/UserControls/Pengusul/dokumenUsulan.ascx" TagPrefix="uc" TagName="dokUsulan" %>
<%@ Register Src="~/UserControls/Pengusul/rab.ascx" TagPrefix="uc" TagName="rab" %>
<%@ Register Src="~/UserControls/Pengusul/rekapUsulanAbdimas.ascx" TagName="rekapUsulan" TagPrefix="uc" %>
<%--<%@ Register Src="~/UserControls/Pengusul/targetLuaranPengabdian.ascx" TagName="ktLuaran" TagPrefix="uc" %>--%>
<%@ Register Src="~/UserControls/Pengusul/targetLuaranPengabdian2019.ascx" TagName="ktLuaran" TagPrefix="uc" %>
<div class="row">
    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>
</div>
<!-- Row end -->
<asp:MultiView runat="server" ID="mvMain">
    <asp:View runat="server" ID="vPersyaratan">
        <uc:persyaratan runat="server" ID="persyaratan" />
        <div class="col-md-12">
            <div class="card mb-4">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="form-inline">
                            <label class="mr-2">Tahun Usulan: </label>
                            <asp:DropDownList ID="ddlThnUsulan" runat="server" AutoPostBack="true" Width="100px"
                                CssClass="form-control basic-single mr-2" OnSelectedIndexChanged="ddlThnUsulan_SelectedIndexChanged">
                                <asp:ListItem Text="2019" Value="2019" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                            </asp:DropDownList>
                            <label class="mr-2">Pelaksanaan: </label>
                            <asp:DropDownList ID="ddlThnPelaksanaanKegiatan" runat="server" AutoPostBack="true" Width="100px"
                                CssClass="form-control basic-single mr-2" OnSelectedIndexChanged="ddlThnPelaksanaanKegiatan_SelectedIndexChanged">
                                <asp:ListItem Text="2020" Value="2020" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="text-right">
                            <asp:Label ID="lblPengajuanBaru" runat="server" CssClass="btn btn-default waves-effect waves-light f-right"
                                Text="Lanjutkan" Visible="false">
                            </asp:Label>
                            <asp:LinkButton runat="server" ID="lbPengajuanBaru" CssClass="btn btn-success" OnClick="lbLanjutkanAtPersyaratanUmum_Click">
                                            <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
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
            <uc:cvKetuaAbdimas runat="server" ID="cvKetuaAbdimas" />
        </div>
        <div class="card-footer col-md-12" style="text-align: right;">
            <asp:LinkButton runat="server" ID="lbLanjutkanAtCVKetua" CssClass="btn btn-success" OnClick="lbLanjutkanAtCVKetua_Click">
                                                <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
            </asp:LinkButton>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vUsulan">
        <!-- Progress wizard -->
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

        <%--                                    <div class="card-header col-md-12" style="text-align: right;">

                                        <asp:Label runat="server" ID="lblKirimUsulan" CssClass="btn btn-secondary disabled">
                                            <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Usulan
                                        </asp:Label>
                                        <asp:LinkButton runat="server" ID="lbKirimUsulan" Text="4" CssClass="btn btn-success" OnClick="lbKirimUsulan_Click" Visible="false">
                                         <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Usulan
                                        </asp:LinkButton>
                                    </div>--%>

        <!-- Isian Usulan baru-->
        <div>

            <asp:Panel runat="server" ID="panelUsulan">

                <asp:MultiView runat="server" ID="mvUsulan">

                    <asp:View runat="server" ID="vIdentitas">

                        <asp:MultiView runat="server" ID="mvIdentitas">
                            <asp:View runat="server" ID="vIDUsulan">
                                <!-- 1.1 -->
                                <uc:IdentitasUsulanAbdimas runat="server" ID="IdentitasUsulanAbdimas" />
                                <div class="card mb-4">
                                    <div class="card-footer col-md-12" style="text-align: right;">
                                        <asp:LinkButton runat="server" ID="lbLanjutkanAtIDUsulan" CssClass="btn btn-success"
                                            OnClick="lbLanjutkanAtIDUsulan_Click">
                                                                    <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View runat="server" ID="vCVAnggota">
                                <!-- 1.2 -->
                                <uc:anggotaAbdimas runat="server" ID="ktAnggota" />
                                <div class="card mb-4">
                                    <div class="card-footer col-md-12" style="text-align: right;">
                                        <asp:LinkButton runat="server" ID="lbLanjutkanAtAnggota" CssClass="btn btn-success" OnClick="lbLanjutkanAtAnggota_Click">
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
                                <div>
                                    <uc:dokUsulan runat="server" ID="ktDokUsulan" />
                                </div>
                                <div class="card-footer col-md-12" style="text-align: right;">
                                    <asp:LinkButton runat="server" ID="lbLanjutkanAtUnggahDokUsulan" CssClass="btn btn-success" OnClick="lbLanjutkanAtUnggahDokUsulan_Click">
                                                                    <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                    </asp:LinkButton>
                                </div>
                            </asp:View>

                            <asp:View runat="server" ID="vLuaran">
                                <!-- 1.2 -->
                                <div class="row">
                                    <div class="col-lg-12">
                                        <uc:ktLuaran runat="server" ID="ktLuaran" />

                                    </div>
                                </div>
                                <div class="col-md-12 card-footer" style="text-align: right;">
                                    <asp:LinkButton runat="server" ID="lbLanjutkanAtUnggahDokLuaran" CssClass="btn btn-success" OnClick="lbLanjutkanAtUnggahDokLuaran_Click">
                                                                    <i class="fa fa-chevron-right"></i>&nbsp;&nbsp;Lanjutkan
                                    </asp:LinkButton>
                                </div>
                            </asp:View>

                        </asp:MultiView>


                    </asp:View>

                    <asp:View runat="server" ID="vRAB">
                        <asp:MultiView runat="server" ID="mvRAB">
                            <asp:View runat="server" ID="vRABIsi">
                                <!-- 1.2 -->
                                <uc:rab runat="server" ID="rab" KodeJenisKegiatan="2" />
                                <div class="card mb-4">
                                    <div class="card-footer col-md-12" style="text-align: right;">
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
                                <uc:mitraAbdimas runat="server" ID="mitraAbdimas" />
                                <div class="card mb-4">
                                    <div class="card-footer col-md-12" style="text-align: right;">
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
                                        <table cellspacing="2">
                                            <tr>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDokLengkap" Text="" ForeColor="Red" OnClick="lbUnduhPdfDokLengkap_Click" Width="40px" Height="50px">
                                                        <i class="far fa-file-pdf" style="font-size: 45px; "></i>
                                                    </asp:LinkButton>
                                                </td>
                                                <td>

                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDokLengkap2" Text="" ForeColor="Red" OnClick="lbUnduhPdfDokLengkap_Click">
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
</asp:MultiView>

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
                    <asp:Label ID="lblJudulDiKirim" runat="server" Font-Bold="true"></asp:Label>
                </p>
            </div>
            <div class="modal-footer">

                <asp:LinkButton runat="server" ID="lbKirimUsulan" CssClass="btn btn-success" OnClick="lbSubmitUsulan_Click">
                                                                    <i class="fa fa-paper-plane"></i>&nbsp;&nbsp;Kirim Usulan
                </asp:LinkButton>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>


<div>
    <uc:pdfUsulanLengkap runat="server" ID="pdfUsulanLengkap" Visible="false" />
</div>
