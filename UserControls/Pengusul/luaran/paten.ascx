<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="paten.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran.paten" %>
<%@ Register Src="~/Helper/unggahKontrol.ascx" TagPrefix="uc" TagName="unggahKontrol" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default m-t-20">
            <div class="panel-heading bg-default txt-white">
                Data Paten
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
                        <label class="col-sm-2 col-form-label form-control-label">No. Pendaftaran</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbNoPendaftaran" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div> 
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">No. Paten (jika status granted)</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbNoHKI" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Nama Pemegang Paten</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbPemegangHKI" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">URL Paten (Terdaftar atau Granted)</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbURL" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Dokumen Pendaftaran</label>
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
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Sertifikat Paten (jika Granted)</label>
                        <div class="col-sm-10">
                            <%-- <uc:unggahKontrol runat="server" ID="unggahSertifikat" />--%>
                            <div class="input-group input-group-button input-group-primary">
                                <asp:FileUpload runat="server" ID="fileUploadSertifikat" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>                
                <%--<div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Manual Book</label>
                        <div class="col-sm-10">
                            <div class="input-group input-group-button input-group-primary">
                                <asp:FileUpload runat="server" ID="fileUploadManualBook" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>--%>
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
