<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IdentitasUsulanLanjutan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.IdentitasUsulanLanjutan" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

<asp:UpdatePanel ID="upIdentitasUsulanLanjutan" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-default m-t-20">
                    <div class="panel-heading bg-default txt-white">
                        Identitas Usulan Penelitian             
                    </div>
                    <div class="panel-body p-15">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Judul</label>
                                <div class="col-sm-10">
                                    <asp:Label runat="server" ID="lblJudul" Text="" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">TKT saat ini</label>
                                <div class="col-sm-2">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DdlTKTSaatIni" runat="server" CssClass="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="DdlTKTSaatIni_SelectedIndexChanged" >
                                            <asp:ListItem Text="Pilih Level" Value="0" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Target Akhir TKT</label>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlTargetTKT" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Pilih Level" Value="0" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-default m-t-20">
                    <div class="panel-heading bg-default txt-white">
                        Pemilihan Skema Penelitian
                    </div>
                    <div class="panel-body p-15">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Kategori Penelitian</label>
                                <div class="col-sm-10">
                                    <asp:Label runat="server" ID="lblKategoriPenelitian" Text="" Font-Bold="true" ForeColor="Green"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Skema Penelitian</label>
                                <div class="col-sm-10">
                                    <asp:Label runat="server" ID="lblSkemaPenelitian" Text="" Font-Bold="true" ForeColor="Green"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Rumpun Ilmu</label>
                                <div class="col-sm-10">
                                    <asp:Label runat="server" ID="lblRumpunIlmu" Text="" Font-Bold="true" ForeColor="Green"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Bidang Fokus Penelitian</label>
                                <div class="col-sm-10">
                                    <asp:Label runat="server" ID="lblBidangFokus" Text="" Font-Bold="true" ForeColor="Green"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="panelTopikPenelitian" runat="server">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 col-form-label form-control-label">Tema Penelitian</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlTemaPenelitian" runat="server" CssClass="form-control"
                                            DataValueField="id_tema" DataTextField="tema" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlTemaPenelitian_SelectedIndexChanged">
                                            <asp:ListItem Text="-- Pilih Tema Penelitian --" Value="00000000-0000-0000-0000-000000000000" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 col-form-label form-control-label">Topik Penelitian</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlTopikPenelitian" runat="server" CssClass="form-control"
                                            DataValueField="id_topik" DataTextField="topik">
                                            <asp:ListItem Text="-- Pilih Topik Penelitian --" Value="00000000-0000-0000-0000-000000000000" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="panelTopikUnggulanPT" runat="server" Visible="false">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 col-form-label form-control-label">Topik Unggulan PT</label>
                                    <div class="col-sm-4 col-xs-12">
                                        <asp:DropDownList ID="ddlBidangUnggulanPT" runat="server" CssClass="form-control"
                                            DataTextField="bidang_unggulan_perguruan_tinggi"
                                            DataValueField="id_bidang_unggulan_perguruan_tinggi" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlBidangUnggulanPT_SelectedIndexChanged">
                                            <asp:ListItem Text="-- Pilih Bidang Unggulan PT --" Value="00000000-0000-0000-0000-000000000000" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 col-form-label form-control-label"></label>
                                    <div class="col-sm-4 col-xs-12">
                                        <asp:DropDownList ID="ddlTopikUnggulanPT" runat="server" CssClass="form-control"
                                            DataTextField="topik_unggulan_perguruan_tinggi"
                                            DataValueField="id_topik_unggulan_perguruan_tinggi">
                                            <asp:ListItem Text="-- Pilih Topik Unggulan PT --" Value="00000000-0000-0000-0000-000000000000" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Tahun Pelaksanaan</label>
                                <div class="col-sm-1">
                                    <asp:Label runat="server" ID="lblThnPelaksanaan" Text="" Font-Bold="true" ForeColor="Green"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-form-label form-control-label">Lama Kegiatan</label>
                                <div class="col-sm-1">
                                    <asp:Label runat="server" ID="lblLamaKegiatan" Text="" Font-Bold="true" ForeColor="Green"></asp:Label>
                                </div>
                                <div class="col-sm-1 col-form-label form-control-label">
                                    Tahun
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
