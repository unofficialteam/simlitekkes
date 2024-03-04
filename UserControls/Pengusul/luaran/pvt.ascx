<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pvt.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran.pvt" %>
<%@ Register Src="~/Helper/unggahKontrol.ascx" TagPrefix="uc" TagName="unggahKontrol" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default m-t-20">
            <div class="panel-heading bg-default txt-white">
                Data Perlindungan Varietas Tanaman (PVT)
            </div>
            <div class="panel-body p-15">
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Nama Varietas</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbNamaVarietas" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Nomor Permohonan</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbNoPermohonan" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Tanggal Permohonan</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="tbTglPermohonan" placeholder="Tanggal Permohonan" CssClass="form-control pull-right" data-provide="datepicker"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Nama Pemohon</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbNamaPemohon" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Alamat Pemohon</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbAlamatPemohon" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Dokumen Permohonan</label>
                        <div class="col-sm-10">
                            <%--<uc:unggahKontrol runat="server" ID="unggahDokumentasi" />--%>
                            <div class="input-group input-group-button input-group-primary">
                                <asp:FileUpload runat="server" ID="fileUploadDok" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Dokumen Sertifikat Hak PVT</label>
                        <div class="col-sm-10">
                            <%-- <uc:unggahKontrol runat="server" ID="unggahSertifikat" />--%>
                            <div class="input-group input-group-button input-group-primary">
                                <asp:FileUpload runat="server" ID="fileUploadSertifikat" CssClass="form-control" />
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
