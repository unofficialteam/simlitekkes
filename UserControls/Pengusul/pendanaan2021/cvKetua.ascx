<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cvKetua.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendanaan2021.cvKetua" %>
<%@ Register Src="~/UserControls/Pengusul/hki.ascx" TagPrefix="uc" TagName="hki" %>
<%@ Register Src="~/UserControls/Pengusul/artikelJurnal.ascx" TagPrefix="uc" TagName="artikelJurnal" %>
<%@ Register Src="~/UserControls/Pengusul/sinta.ascx" TagPrefix="uc" TagName="sinta" %>
<%@ Register Src="~/UserControls/Pengusul/buku.ascx" TagPrefix="uc" TagName="buku" %>
<%@ Register Src="~/UserControls/Pengusul/artikelProsiding.ascx" TagPrefix="uc" TagName="prosiding" %>

<asp:ScriptManagerProxy ID="smpBeranda" runat="server"></asp:ScriptManagerProxy>

<asp:UpdatePanel ID="upPersyaratanUmum" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12 p-0">
                <div class="main-header">
                </div>
            </div>
        </div>
        <asp:MultiView ID="mvcvKetua" runat="server" ActiveViewIndex="0">
            <asp:View ID="vCvKetua" runat="server">

                <div class="col-md-12">

                    <div class="card mb-4">
                        <div class="card-header">
                            <div class="d-flex justify-content-between align-items-center">
                                <div>
                                    <h5 style="color: darkred">H-Index:
                                            <asp:Label ID="lblHindex1" runat="server" ForeColor="DarkRed" Font-Bold="true" Text="0"></asp:Label>
                                    </h5>
                                </div>
                                <div class="text-right">
                                    <h5 style="color: darkred">Usulan Baru:
                                            <asp:Label ID="lblJmlUsulanBaru1" runat="server" ForeColor="DarkRed" Font-Bold="true" Text="0"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                        </div>

                        <div class="card-body">

                            <div class="card mb-4">
                                <div class="card-header">
                                    <h5>Identitas Pengusul-Ketua
                                    </h5>
                                </div>
                                <div class="card-body">
                                    <div>
                                        <form class="form-inline">
                                            <div class="form-group m-r-6">
                                                <label for="lblNamaLengkap" class="m-r-6 form-control-label">Nama:</label>
                                                <asp:Label ID="lblNamaLengkap" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="form-group m-r-6" id="nidn_area" runat="server">
                                                <label for="lblNidn" class="m-r-6 form-control-label">NIDN/NIDK:</label>
                                                <asp:Label ID="lblNidn" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="form-group m-r-6">
                                                <label for="lblNamaInstitusi" class="m-r-6 form-control-label">Perguruan Tinggi:</label>
                                                <asp:Label ID="lblNamaInstitusi" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="form-group m-r-6" id="prodi_area" runat="server">
                                                <label for="lblProgramStudi" class="m-r-6 form-control-label">Program Studi:</label>
                                                <asp:Label ID="lblProdi" runat="server" Text=""></asp:Label>
                                            </div>

                                            <div class="form-group m-r-6">
                                                <label for="lblIdSinta1" class="m-r-6 form-control-label">ID Sinta:</label>
                                                <asp:Label ID="lblIdSinta1" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="form-group m-r-6">
                                                <label for="lblKualifikasi" class="m-r-6 form-control-label">Kualifikasi:</label>
                                                <asp:Label ID="lblKualifikasi" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="form-group m-r-6">
                                                <label for="lblSurel" class="m-r-6 form-control-label">Alamat Surel:</label>
                                                <asp:Label ID="lblSurel" runat="server" Text=""></asp:Label>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>




                    <div class="card mb-4">
                        <div class="card-header">
                            <h5>Skema Penelitian Perguruan Tinggi Kelompok I, II, dan III&nbsp;<asp:Label ID="lblKlaster" runat="server" Text="" Visible="false"></asp:Label>
                            </h5>
                        </div>
                        <div class="card-body">
                            <asp:ListView ID="lvSkema" runat="server"
                                DataKeyNames="id_skema">
                                <LayoutTemplate>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr>
                                                <td style="width: 30px; text-align: left; padding: 0;"></td>
                                                <td style="text-align: left; padding: 0;"></td>
                                            </tr>
                                            <tr id="itemPlaceHolder" runat="server"></tr>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td>
                                            <h6><%# Eval("nama_skema") %></h6>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div class="col-sm-12">
                                        <p class="text-primary">Belum ada data Skema...</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                    </div>



                    <div class="card mb-4">
                        <div class="card-header">
                            <h5>Rekam Jejak
                            </h5>
                        </div>
                        <div class="card-body">
                            <asp:ListView ID="lvRekamJejak" runat="server"
                                DataKeyNames="kd_rekam_jejak"
                                OnItemEditing="lvRekamJejak_ItemEditing">
                                <LayoutTemplate>
                                    <table class="table table-hover">
                                        <tbody>
                                            <tr>
                                                <td style="width: 30px; text-align: left; padding: 0;"></td>
                                                <td style="text-align: left; padding: 0;"></td>
                                                <td style="text-align: right; padding: 0;"></td>
                                            </tr>
                                            <tr id="itemPlaceHolder" runat="server"></tr>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td>
                                            <h6><%# Eval("jenis_rekam_jejak") %>
                                                <b style="color: red">(<%# Eval("jml_rekam_jejak") %>)</b>

                                            </h6>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit"
                                                CssClass="btn btn-sm btn-primary waves-effect m-b-5" ToolTip="Tambah data">
                                                                Tambah Data</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div class="col-sm-12">
                                        <p class="text-primary">Belum ada data Skema...</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                    </div>



                </div>

            </asp:View>
            <asp:View ID="vJurnal" runat="server">
                <uc:artikelJurnal runat="server" ID="artikelJurnal" />
                <div>
                    <asp:LinkButton ID="lbKembaliJurnal" runat="server" OnClick="lbKembaliJurnal_Click"
                        class="btn btn-info waves-effect" Text="Kembali">
                    </asp:LinkButton>
                </div>
            </asp:View>
            <asp:View ID="vProsiding" runat="server">
                <uc:prosiding runat="server" ID="prosiding" />
                <div>
                    <asp:LinkButton ID="lbKembaliProsiding" runat="server" OnClick="lbKembaliProsiding_Click"
                        class="btn btn-info waves-effect" Text="Kembali">
                    </asp:LinkButton>
                </div>
            </asp:View>
            <asp:View ID="vHki" runat="server">
                <uc:hki runat="server" ID="hki" />
                <div>
                    <asp:LinkButton ID="lbKembaliHki" runat="server" OnClick="lbKembaliHki_Click"
                        class="btn btn-info waves-effect" Text="Kembali">
                    </asp:LinkButton>
                </div>
            </asp:View>
            <asp:View ID="vBuku" runat="server">
                <uc:buku runat="server" ID="buku" />
                <div>
                    <asp:LinkButton ID="lbKembaliBuku" runat="server" OnClick="lbKembaliBuku_Click"
                        class="btn btn-info waves-effect" Text="Kembali">
                    </asp:LinkButton>
                </div>
            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>
