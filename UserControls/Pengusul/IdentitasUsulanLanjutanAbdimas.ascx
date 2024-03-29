﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IdentitasUsulanLanjutanAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.IdentitasUsulanLanjutanAbdimas" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upIdentitasUsulan" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-default m-t-20">
                    <div class="panel-heading bg-default txt-white">
                        Identitas Usulan Pengabdian Kepada Masyarakat             
                    </div>
                    <div class="panel-body p-15">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Judul</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="tbJudul" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Kategori </label>
                                <div class="col-sm-10">
                                    <asp:RadioButtonList ID="rblKategoriPengabdian" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblKategoriPengabdian_SelectedIndexChanged">
                                        <asp:ListItem Text="Kompetitif Nasional" Value="3" class="radio-inline" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Desentralisasi" Value="7" class="radio-inline"></asp:ListItem>
                                        <%--<asp:ListItem Text="Penugasan" Value="8" class="radio-inline"></asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Skema</label>
                                <div class="col-sm-10 col-xs-12">
                                    <asp:DropDownList ID="ddlSkemaPenelitian" runat="server" CssClass="form-control"
                                        DataTextField="nama_skema" DataValueField="id_skema" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSkemaPenelitian_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Pilih Skema Kegiatan --" Value="-1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="panelTopikUnggulanPT" runat="server" Visible="false">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 col-form-label form-control-label">Bidang Unggulan PT</label>
                                    <div class="col-sm-10 col-xs-12">
                                        <asp:DropDownList ID="ddlBidangUnggulanPT" runat="server" CssClass="form-control"
                                            DataTextField="bidang_unggulan_perguruan_tinggi"
                                            DataValueField="id_bidang_unggulan_perguruan_tinggi" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlBidangUnggulanPT_SelectedIndexChanged">
                                            <asp:ListItem Text="-- Pilih Bidang Unggulan PT --" Value="-1" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 col-form-label form-control-label">Topik Unggulan PT</label>
                                    <div class="col-sm-10 col-xs-12">
                                        <asp:DropDownList ID="ddlTopikUnggulanPT" runat="server" CssClass="form-control"
                                            DataTextField="topik_unggulan_perguruan_tinggi"
                                            DataValueField="id_topik_unggulan_perguruan_tinggi">
                                            <asp:ListItem Text="-- Pilih Topik Unggulan PT --" Value="-1" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Bidang Pengabdian</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlBidangFokus" runat="server" CssClass="form-control"
                                        DataValueField="id_bidang_fokus" DataTextField="bidang_fokus" Enabled="false">
                                        <asp:ListItem Text="-- Pilih Bidang Abdimas --" Value="-1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Tahun Pelaksanaan</label>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlTahunKegiatan" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="2019" Value="2019" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Lama Kegiatan</label>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlLamaKegiatan" runat="server" CssClass="form-control" Enabled="false">
                                        <asp:ListItem Text="--" Value="-1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1 col-form-label form-control-label">
                                    Tahun
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 col-form-label form-control-label">
                                    Jumlah Mahasiswa yang terlibat
                                </label>
                                <div class="col-sm-1">
                                    <asp:TextBox ID="tbJumlahMhs" runat="server" CssClass="form-control angka" MaxLength="2"></asp:TextBox>
                                    <script type="text/javascript">
                                        new AutoNumeric('.angka', {
                                            decimalPlaces: 0,
                                            maximumValue: 99,
                                            minimumValue: 1
                                        });
                                    </script>
                                </div>
                                <div class="col-sm-7 p-t-5">
                                    <small class="form-text text-muted">* minimal <asp:Label ID="lblJumlahMhsMin" runat="server" Text="0"></asp:Label> orang</small>
                                </div>

                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
