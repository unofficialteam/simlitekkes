<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="modalKonfirmasi.ascx.cs" Inherits="simlitekkes.Helper.modalKonfirmasi" %>

<div class="modal fade" id="confirmModal" tabindex="-1" role="dialog" aria-labelledby="confirmModalLabel" 
    aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="confirmModalLabel"><asp:Label ID="lblConfirmTitle" runat="server" Text="Konfirmasi" /></h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        <asp:Label ID="lblConfirmText" runat="server" />
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
        <asp:Button ID="btnHapus" runat="server" CssClass="btn btn-primary" Text="Hapus" OnClick="btnHapus_Click"/>
      </div>
    </div>
  </div>
</div>