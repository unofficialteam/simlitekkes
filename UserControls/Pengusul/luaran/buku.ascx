<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="buku.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran.buku" %>
<%@ Register Src="~/Helper/unggahKontrol.ascx" TagPrefix="uc" TagName="unggahKontrol" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default m-t-20">
            <div class="panel-heading bg-default txt-white">
                Data Buku
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
                        <label class="col-sm-2 col-form-label form-control-label">ISBN</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbISBN" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Jumlah Halaman</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="tbJmlHalaman" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Penerbit</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbPenerbit" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Surat Keterangan Terbit</label>
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
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Buku <b style="color:red">(Maks. 10MB)</b></label>
                        <div class="col-sm-10">
                            <%--<uc:unggahKontrol runat="server" ID="unggahBuku" />--%>
                            <div class="input-group input-group-button input-group-primary">
                                <asp:FileUpload runat="server" ID="fileUploadBuku" CssClass="form-control" />
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
