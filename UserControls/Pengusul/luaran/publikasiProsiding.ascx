<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="publikasiProsiding.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran.publikasiProsiding" %>
<%@ Register Src="~/Helper/unggahKontrol.ascx" TagPrefix="uc" TagName="unggahKontrol" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default m-t-20">
            <div class="panel-heading bg-default txt-white">
                <asp:Label ID="lblProsidingInternasional" runat="server">Data Prosiding dalam Pertemuan Ilmiah Internasional 
                </asp:Label>
                <asp:Label ID="lblProsidingLokal" runat="server">Data Prosiding dalam Pertemuan Ilmiah Lokal 
                </asp:Label>
                 <asp:Label ID="lblProsidingNasional" runat="server">Data Prosiding dalam Pertemuan Ilmiah Nasional
                </asp:Label>
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
                        <label class="col-sm-2 col-form-label form-control-label">Tahun Publikasi</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlThnPublikasi" runat="server" CssClass="custom-select">
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">ISBN/ISSN</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbISSN" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Volume <b style="color: red">(jika ada)</b></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="tbVolume" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-1 col-form-label form-control-label">Nomor</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="tbNomor" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Halaman</label>
                        <div class="form-group">
                            <label class="col-sm-1 col-form-label form-control-label">Awal</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="tbHalAwal" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-1 col-form-label form-control-label">Akhir</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="tbHalAkhir" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">URL <b style="color: red">(jika ada)</b></label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbURL" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Surat</label>
                        <div class="col-sm-10">
                            <%--<uc:unggahKontrol runat="server" ID="unggahSuratKeterangan" />--%>
                            <div class="input-group input-group-button input-group-primary">
                                <asp:FileUpload runat="server" ID="fileUploadSUratKet" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Artikel</label>
                        <div class="col-sm-10">
                            <%--<uc:unggahKontrol runat="server" ID="unggahArtikel" />--%>
                            <div class="input-group input-group-button input-group-primary">
                                <asp:FileUpload runat="server" ID="fileUploadArtikel" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row m-t-30">
                <div class="col-sm-12 text-center">
                    <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary waves-effect waves-light"
                        Text="Simpan" OnClick="btnSimpan_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnBatal" runat="server" CssClass="btn btn-default waves-effect waves-light"
                    Text="Batal" OnClick="btnBatal_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</div>
