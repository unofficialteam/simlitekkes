<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="luaranLain.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaran.luaranLain" %>
<%@ Register Src="~/Helper/unggahKontrol.ascx" TagPrefix="uc" TagName="unggahKontrol" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default m-t-20">
            <div class="panel-heading bg-default txt-white">
                Data Luaran Lainnya
            </div>
            <div class="panel-body p-15">
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Uraian Luaran</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="tbUraianLuaranLain" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                </div>                
                <%--<div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Surat Keterangan</label>
                        <div class="col-sm-10">
                            <div class="input-group input-group-button input-group-primary">
                                <asp:FileUpload runat="server" ID="fileUploadSUratKet" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="row">
                    <div class="form-group">
                        <label class="col-sm-2 col-form-label form-control-label">Unggah Berkas Luaran</label>
                        <div class="col-sm-10">
                           <%-- <uc:unggahKontrol runat="server" ID="unggahArtikel" />--%>
                            <div class="input-group input-group-button input-group-primary">
                                <asp:FileUpload runat="server" ID="fileUploadLuaranLain" CssClass="form-control" />
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
