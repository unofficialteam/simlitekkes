<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="unggahKontrol.ascx.cs" Inherits="simlitekkes.Helper.unggahKontrol" %>

<div style="padding: 10px;">
    <div class="col-lg-12">
        <div class="input-group input-group-button input-group-primary">
            <asp:FileUpload runat="server" ID="fileUpload1" CssClass="form-control" />
            <span class="input-group-btn">
                <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-info"
                    OnClick="lbUnggahDokumen_Click">
                <i class="fa fa-cloud-upload">&nbsp;Unggah</i></asp:LinkButton>
            </span>
        </div>
    </div>
    <div>
        <asp:Label runat="server" ID="lblInfo" Text="" ForeColor="Red" Font-Bold="true"> </asp:Label>
    </div>
</div>
