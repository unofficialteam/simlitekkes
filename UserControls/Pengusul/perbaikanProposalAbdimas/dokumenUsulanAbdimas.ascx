<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dokumenUsulanAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.perbaikanProposalAbdimas.dokumenUsulanAbdimas" %>

<div class="container-fluid">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-body">
                    <div class="col-sm-12">
                        <div class="alert alert-light" role="alert" style="background-color: lightgrey;">
                            <span style="color: maroon; font-weight: bold; font-size: 18px;">Substansi Usulan Perbaikan</span>
                        </div>
                        <section class="panels-wells">
                            <div class="col-lg-12 table-responsive">
                                <div style="height: 40px;"></div>
                                <div class="row">
                                    <div class="col-lg-7">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lbUnduhTemplateDok2" CssClass="mr-2" Text="" Font-Bold="true" OnClick="lbUnduhTemplateDok_Click">
                                                        <i class="far fa-file-word" style="font-size: 60px; color: blue;"></i></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <div><b>Template konten dokumen usulan</b></div>
                                                    <asp:LinkButton runat="server" ID="lbUnduhTemplateDok" Text="Unduh" Font-Bold="true" 
                                                        OnClick="lbUnduhTemplateDok_Click"></asp:LinkButton>
                                                </td>
                                                <td>

                                                    <div>Tgl. Unggah:&nbsp;<asp:Label runat="server" ID="lblTglUnggah" Text="-"></asp:Label></div>
                                                    <div>Ukuran file:&nbsp;<asp:Label runat="server" ID="lblUkuranFile" Text="-"></asp:Label></div>
                                                    <div>
                                                        <div style="padding: 10px;">
                                                            <div>
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
                                                    </div>
                                                    <div>
                                                        <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">Format file PDF dengan ukuran maks 5MB
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:Label runat="server" ID="lblErrorInfo" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                            </div>

                        </section>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
