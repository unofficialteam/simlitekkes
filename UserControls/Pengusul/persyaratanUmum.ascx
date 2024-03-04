<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="persyaratanUmum.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.persyaratanUmum" %>
<asp:ScriptManagerProxy ID="smpBeranda" runat="server"></asp:ScriptManagerProxy>

<asp:UpdatePanel ID="upPersyaratanUmum" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12 p-0">
                <div class="main-header">
                </div>
            </div>
        </div>
        <asp:MultiView ID="mvUsulanBaru" runat="server" ActiveViewIndex="0">
            <asp:View ID="vPersyaratanUmum" runat="server">
                <%--<section class="panels-wells">--%>
                    <div class="col-md-12">
                        <div class="card mb-4">
                            <div class="card-header">
                            
                             <div class="d-flex justify-content-between align-items-center">
                                    <div >
                                        <h5 style="color: darkred">H-Index:
                                <asp:Label ID="lblHindex" runat="server" ForeColor="DarkRed" Font-Bold="true" Text="0"></asp:Label>
                                    &nbsp;(<asp:Label ID="lblKelompokBidang" runat="server" ForeColor="DarkRed" Font-Bold="true" Text=""></asp:Label>)
                                </h5>
                                    </div>
                                    <div class="text-right">
                                        <h5 class="card-header-text f-right" style="color: darkred">Usulan Baru:
                                        <asp:Label ID="lblJmlUsulanBaru" runat="server" ForeColor="DarkRed" Font-Bold="true" Text="0"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                            </div>
							
                            <div class="card-body">
                            
                                    <div class="card mb-4">
                                        <div class="card-header">
                                            <h5>Persyaratan Umum</h5>
                                        </div>
                                        <div class="card-body">
                                           
                                                <asp:Panel ID="pnlCekSinta" runat="server" Visible="true">
                                                    <div>
                                                        <label for="chkSinta">
                                                            <i class="fa fa-check" style="color: green"></i>&nbsp;Terdaftar dalam Sinta&nbsp;
                                                        <b style="color: green">(</b><asp:Label ID="lblIdSinta" runat="server" Font-Bold="true" ForeColor="Green" Text="-"></asp:Label><b style="color: green">)</b>
                                                        </label>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlCekSinta1" runat="server" Visible="false">
                                                    <div>
                                                        <label for="chkSinta1">
                                                            <i class="fa fa-times" style="color: red"></i>&nbsp;Terdaftar dalam Sinta&nbsp;
                                                        <b style="color: red">(</b><asp:Label ID="lblIdSinta11" runat="server" Font-Bold="true" ForeColor="Red" Text="-"></asp:Label><b style="color: red">)</b>
                                                        </label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblInfoSinta" runat="server" class="label label-inverse-danger">
                                                            Anda belum terdaftar dalam SINTA.
                                                        </asp:Label>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlPegawai" runat="server" Visible="true">
                                                    <div>
                                                        <label for="chkStatusPegawai">
                                                            <i class="fa fa-check" style="color: green"></i>&nbsp;Status Pegawai&nbsp;
                                                        <b style="color: green">(</b><asp:Label ID="lblStsPegawai" runat="server" Font-Bold="true" ForeColor="Green" Text="-"></asp:Label><b style="color: green">)</b>
                                                        </label>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlPegawai1" runat="server" Visible="false">
                                                    <div>
                                                        <label>
                                                            <i class="fa fa-times" style="color: red"></i>&nbsp;Status Pegawai&nbsp;
                                                            <b style="color: red">(</b>-
                                                            <b style="color: red">)</b>
                                                        </label>
                                                    </div>
                                                    <asp:Label ID="Label1" runat="server" class="label label-inverse-danger">
                                                        <asp:Label ID="lblStsPegawai1" runat="server" Font-Bold="true" Text="-">
                                                        </asp:Label>
                                                    </asp:Label>
                                                </asp:Panel>
                                                <%--<asp:Panel ID="pnlBlacklist" runat="server" Visible="false">
                                                    <div>
                                                        <label>
                                                            <i class="fa fa-times" style="color: red"></i>&nbsp;Blacklist:&nbsp;
                                                        </label>
                                                    </div>
                                                    <asp:Label ID="Label2" runat="server" class="label label-inverse-danger">
                                                        Mulai tgl.&nbsp;<asp:Label ID="lblMulaiBlacklist" runat="server" Font-Bold="true" Text="-"></asp:Label>&nbsp;
                                                        s.d.&nbsp;<asp:Label ID="lblBerakhirBlacklist" runat="server" Font-Bold="true" Text="-"></asp:Label>&nbsp;
                                                        Keterangan:&nbsp;<asp:Label ID="lblKetBlacklist" runat="server" Font-Bold="true" Text="-"></asp:Label>
                                                    </asp:Label>
                                                </asp:Panel>--%>
                                                <%--<asp:Panel ID="pnlTanggungan" runat="server" Visible="true">
                                                    <div>
                                                        <label for="chkTanggungan">
                                                            <i class="fa fa-check" style="color: green"></i>&nbsp;Tanggungan Kegiatan&nbsp;
                                                        <b style="color: green">(</b><asp:Label ID="lblTanggungan" runat="server" Font-Bold="true" ForeColor="Green" Text="Tidak ada"></asp:Label><b style="color: green">)</b>
                                                        </label>
                                                    </div>
                                                </asp:Panel>--%>
                                                <%--<asp:Panel ID="pnlTanggungan1" runat="server" Visible="false">
                                                    <div>
                                                        <div>
                                                            <label for="chkTanggungan1">
                                                                <i class="fa fa-times" style="color: red"></i>&nbsp;Tanggungan Kegiatan&nbsp;
                                                        <b style="color: red">(</b><asp:Label ID="lblTanggungan1" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label><b style="color: red">)</b>
                                                            </label>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:ListView ID="lvTanggungan" runat="server"
                                                                DataKeyNames="kd_jenis_tanggungan">
                                                                <LayoutTemplate>
                                                                    <table class="table table-hover">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 30px; text-align: left; padding: 0;"></td>
                                                                                <td style="text-align: left; padding: 0;"></td>
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
                                                                            <h6><%# Eval("jenis_tanggungan") %></h6>
                                                                            <br />
                                                                            <b>Tahun: </b><%# Eval("thn_pelaksanaan_kegiatan") %> |
                                                                            <b>Skema: </b><%# Eval("nama_skema") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("judul") %>
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
                                                </asp:Panel>--%>
                                                <%--<asp:Panel ID="pnlCekKuotaUsulan" runat="server" Visible="true">
                                                    <div>
                                                        <label for="lblCekKuotaUsulan">
                                                            <i class="fa fa-check" style="color: green"></i>&nbsp;Kuota Uuslan&nbsp;
                                                        <b style="color: green">(</b><asp:Label ID="lblCekKuotaUsulan" runat="server" Font-Bold="true" ForeColor="Green" Text="-"></asp:Label><b style="color: green">)</b>
                                                        </label>
                                                    </div>
                                                </asp:Panel>--%>
                                                <%--<asp:Panel ID="pnlCekKuotaUsulan1" runat="server" Visible="false">
                                                    <div>
                                                        <label>
                                                            <i class="fa fa-times" style="color: red"></i>&nbsp;Kuota Usulan&nbsp;
                                                            <b style="color: red">(</b>-
                                                            <b style="color: red">)</b>
                                                        </label>
                                                    </div>
                                                    <asp:Label ID="lblCekKuotaUsulan1" runat="server" class="label label-inverse-danger">
                                                        <asp:Label ID="lblInfoCekKuotaUsulan" runat="server" Font-Bold="true" Text="-">
                                                        </asp:Label>
                                                    </asp:Label>
                                                </asp:Panel>--%>
                                           
                                        </div>
                                    </div>
                            
                            
                                <br />
                                <%--<div class="row">
                                    <div class="form-group">
                                        <label class="col-sm-2 col-form-label form-control-label">Tahun Pelaksanaan</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlThnPelaksanaanKegiatan" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="2019" Value="2019" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                    </div>
                                </div>--%>
                                <%--<br />
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading bg-default txt-white">
                                                Daftar Usulan Penelitian Baru
                                            </div>
                                            <div class="panel-body">
                                                <asp:ListView ID="lvDaftarUsulanBaru" runat="server"
                                                    DataKeyNames="id_usulan_kegiatan">
                                                    <LayoutTemplate>
                                                        <table class="table table-hover">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 200px; text-align: left; padding: 0;"></td>
                                                                </tr>
                                                                <tr id="itemPlaceHolder" runat="server"></tr>
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <h5><b style="color: blue"><%# Eval("judul") %></h5>
                                                                </b>
                                                                <h6 style="color: green"><%# Eval("nama_skema") %></h6>
                                                                <h6 style="color: darkred"><%# Eval("thn_usulan_dan_pelaksanaan") %></h6>
                                                                <%# Eval("bidang_fokus") %> -
                                                                <asp:Label class="label bg-info" ID="lblPeranPersonil" runat="server" Text='<%# Bind("peran_personil") %>'></asp:Label>&nbsp;
                                                                <asp:Label class="label bg-Success" ID="lblStsKonfirmasi" runat="server" Text='<%# Bind("sts_konfirmasi") %>'></asp:Label>
                                                            </td>
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
                                </div>--%>
                            </div>
                        </div>
                    </div>
                <%--</section>--%>
            </asp:View>

        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>
