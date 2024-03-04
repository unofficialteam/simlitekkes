<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dokumenUsulan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.PerbaikanProposal.dokumenUsulan" %>

<div class="container-fluid">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-body">
                   <%-- <div class="row">--%>
                        <div class="col-sm-12">
                            <div class="alert alert-light" role="alert" style="background-color: lightgrey;">
                                <span style="color: maroon; font-weight: bold; font-size: 18px;">Substansi Usulan Perbaikan</span>
                            </div>
                            <section class="panels-wells">
                                <div class="col-lg-12">


                                    <%--<div class="row" style="margin: 10px;">
                                    <div class="form-group">
                                        <label class="col-sm-2 col-form-label form-control-label">Kelompok makro riset</label>
                                        <div class="col-sm-6 col-xs-12">
                                            <asp:DropDownList ID="ddlMakroRiset" runat="server" CssClass="form-control"
                                                DataTextField="makro_riset" DataValueField="id_makro_riset" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlMakroRiset_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Pilih kelompok makro riset --" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>--%>
                                    <%--<div class="alert alert-info alert-dismissible">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                    <h4><i class="icon fa fa-info">&nbsp;</i>Deskripsi</h4>
                                    <asp:Label runat="server" ID="lblDeskripsiMakroRiset" Text=""></asp:Label>
                                </div>--%>

                                    <div style="font-weight: bold; margin-left: 5px;">
                                        Catatan reviewer:
                                    </div>
                                    <div class="panel panel-default">

                                        <div class="panel-body" style="margin-left: 10px; margin-right: 10px;">
                                            <div class="row">
                                                <asp:Label ID="Label9" runat="server" ForeColor="Black" Font-Bold="true" Text="Reviewer 1:"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="komentarRev1" runat="server" ForeColor="Green" Text=""></asp:Label>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <asp:Label ID="Label10" runat="server" ForeColor="Black" Font-Bold="true" Text="Reviewer 2:"></asp:Label>
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="komentarRev2" runat="server" ForeColor="Green" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="height: 40px;"></div>
                                    <div class="row">
                                        <div class="col-lg-7">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lbUnduhTemplateDok2" Text="" Font-Bold="true" OnClick="lbUnduhTemplateDok_Click">
                                                    <i class="fa fa-file-word-o" style="font-size: 60px; color: blue;"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td style="padding-left: 5px;">
                                                        <div><b>Template konten dokumen usulan</b></div>
                                                        <asp:LinkButton runat="server" ID="lbUnduhTemplateDok" Text="Unduh" Font-Bold="true" OnClick="lbUnduhTemplateDok_Click"></asp:LinkButton>

                                                    </td>
                                                    <%--<td>
                                                    <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok_Click">
                                            <i class="fa fa-file-pdf-o" style="font-size: 70px; "></i>
                                                    </asp:LinkButton><br />
                                                </td>--%>
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
                    <%--</div>--%>
                </div>
            </div>
        </div>
    </div>
</div>
