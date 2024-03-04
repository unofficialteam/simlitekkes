﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="persyaratanUmumLanjutanAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.persyaratanUmumLanjutanAbdimas" %>
<asp:ScriptManagerProxy ID="smpBeranda" runat="server"></asp:ScriptManagerProxy>

<asp:UpdatePanel ID="upPersyaratanUmum" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12 p-0">
                <div class="main-header">
                </div>
            </div>
        </div>
        <asp:MultiView ID="mvPersyaratanUmum" runat="server" ActiveViewIndex="0">
            <asp:View ID="vPersyaratanUmum" runat="server">
                <section class="panels-wells">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header">
                                <h5 class="card-header-text f-right" style="color: darkred">Usulan Lanjutan:
                                <asp:Label ID="lblJmlUsulanLanjutan" runat="server" ForeColor="DarkRed" Font-Bold="true" Text="0"></asp:Label>
                                </h5>
                            </div>
                            <div class="card-block">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading bg-default txt-white">
                                                Persyaratan Umum
                                            </div>
                                            <div class="panel-body">
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
                                                    <asp:Label ID="lblInfoPegawai" runat="server" class="label label-inverse-danger">
                                                        <asp:Label ID="lblStsPegawai1" runat="server" Font-Bold="true" Text="-">
                                                        </asp:Label>
                                                    </asp:Label>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlKuotaUsulan" runat="server" Visible="false">
                                                    <div>
                                                        <label>
                                                            <i class="fa fa-times" style="color: red"></i>&nbsp;Kuota Usulan Sebagai Ketua&nbsp;
                                                            <b style="color: red">(</b>
                                                            <asp:Label ID="lblKuotaUsulan" runat="server" Font-Bold="true" Text="-"
                                                                class="label label-inverse-danger">
                                                            </asp:Label>
                                                            <b style="color: red">)</b>
                                                        </label>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlBlacklist" runat="server" Visible="false">
                                                    <div>
                                                        <label>
                                                            <i class="fa fa-times" style="color: red"></i>&nbsp;Blacklist:&nbsp;
                                                        </label>
                                                    </div>
                                                    <asp:Label ID="lblInfoBlacklist" runat="server" class="label label-inverse-danger">Mulai tgl.&nbsp;<asp:Label ID="lblMulaiBlacklist" runat="server" Font-Bold="true" Text="-"></asp:Label>&nbsp;
                                                        s.d.&nbsp;<asp:Label ID="lblBerakhirBlacklist" runat="server" Font-Bold="true" Text="-"></asp:Label>&nbsp;
                                                        Keterangan:&nbsp;<asp:Label ID="lblKetBlacklist" runat="server" Font-Bold="true" Text="-"></asp:Label>
                                                    </asp:Label>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlTanggungan" runat="server" Visible="true">
                                                    <div>
                                                        <label for="chkTanggungan">
                                                            <i class="fa fa-check" style="color: green"></i>&nbsp;Tanggungan Kegiatan&nbsp;
                                                        <b style="color: green">(</b><asp:Label ID="lblTanggungan" runat="server" Font-Bold="true" ForeColor="Green" Text="Tidak ada"></asp:Label><b style="color: green">)</b>
                                                        </label>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlTanggungan1" runat="server" Visible="false">
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
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>
