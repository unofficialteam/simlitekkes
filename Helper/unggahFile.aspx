<%@ Page Title="" Language="C#" MasterPageFile="~/Helper/MasterIFrame.Master" AutoEventWireup="true" CodeBehind="unggahFile.aspx.cs" Inherits="simlitekkes.Helper.unggahFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 5px">
        <div class="form-group">
            <div class="custom-file">
                <asp:FileUpload runat="server" ID="fileUpload1" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-info" OnClick="lbUnggahDokumen_Click">
                <i class="fas fa-cloud-upload-alt">&nbsp; Unggah</i>
            </asp:LinkButton>
        </div>
        <div class="form-group">
            <asp:Label runat="server" ID="lblInfo" Text="" ForeColor="Red" Font-Bold="true"> </asp:Label>
        </div>
    </div>
</asp:Content>
