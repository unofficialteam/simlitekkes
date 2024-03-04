<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pemakalahForumIlmiah.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran.pemakalahForumIlmiah" %>
<%@ Register Src="~/Helper/unggahKontrol.ascx" TagPrefix="uc" TagName="unggahKontrol" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default m-t-20">
            <div class="panel-heading bg-default txt-white">
                Data Publikasi Jurnal
            </div>
            <div class="panel-body p-15">
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Judul</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbJudul" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Nama Jurnal</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbNamaJurnal" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Penulis Utama</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbNamaPenulis" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">ISSN</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbISSN" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Volume</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="tbVolume" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Nomor</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="tbNomor" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Halaman</label>
                        <div class="col-sm-10">
                            <%--<asp:TextBox ID="tbNomor" runat="server" CssClass="form-control"></asp:TextBox>--%>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">URL</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbURL" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Surat Keterangan</label>
                        <div class="col-sm-10">
                            <uc:unggahKontrol runat="server" ID="unggahSuratKeterangan" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Artikel</label>
                        <div class="col-sm-10">
                            <uc:unggahKontrol runat="server" ID="unggahArtikel" />
                        </div>
                    </div>
                </div>
            </div>
            <%--<div class="row m-t-10 m-b-20">
                <div class="col-sm-12 text-center">
                    <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-primary">Simpan</asp:LinkButton>
                </div>
            </div>--%>
        </div>
    </div>
</div>
