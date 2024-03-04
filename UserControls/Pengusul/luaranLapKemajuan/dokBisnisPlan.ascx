<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dokBisnisPlan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.dokBisnisPlan" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Dokumen Bisnis Plan">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status dokumen
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusDokumen" CssClass="form-control"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran"
                        OnSelectedIndexChanged="ddlStatusDokumen_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Tersedia" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Draft" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama Produk
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaProduk" CssClass="form-control" placeholder="Nama produk" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Merek
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbMerek" CssClass="form-control"
                        placeholder="Jika ada" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    No. Sertifikat merek
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbNoSertifikatMerek" CssClass="form-control" placeholder="Jika ada" Text=""></asp:TextBox>
                </div>
                <div class="col-lg-2">
                    Tgl. Sertifikat merek
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbTglSertifikatMerek" CssClass="form-control"
                        Type="date" placeholder="Jika ada" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    No. Sertifikat desain industri
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbNoSertifikatDesainIndustri" CssClass="form-control" placeholder="Jika ada" Text=""></asp:TextBox>
                </div>
                <div class="col-lg-2">
                    Tgl. Sertifikat desain industri
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbTglSertifikatDesainIndustri" CssClass="form-control"
                        Type="date" placeholder="Jika ada" Text=""></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6">
                    <hr>
                </div>
                <div class="col-lg-6">
                </div>
            </div>

            <div>Unggah Dokumen</div>
            <table style="margin-top: 10px;">
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok_Click">			
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblJudulUnggah1" Text="Dokumen bisnis plan"></asp:Label><br>
                            <asp:FileUpload runat="server" ID="fileUpload1" CssClass="form-control" />
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                <span style="font-size: 14px; padding: 10px; color: red;">
                    <asp:Label runat="server" ID="lblInfoFileUnggah1" Text="(Ukuran File Maksimal 2 MB dengan format PDF)"></asp:Label>
                </span>
            </div>

            <table style="margin-top: 20px;">
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok2" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok2_Click">			
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblJudulUnggah2" Text="Sertifikat merek (jika ada)"></asp:Label><br>
                            <asp:FileUpload runat="server" ID="fileUpload2" CssClass="form-control" />
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                <span style="font-size: 14px; padding: 10px; color: red;">
                    <asp:Label runat="server" ID="lblInfoFileUnggah2" Text="(Ukuran File Maksimal 1 MB dengan format PDF)"></asp:Label>
                </span>
            </div>

            <table style="margin-top: 10px;">
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok3" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok3_Click">
										<i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblJudulUnggah3" Text="Sertifikat desain industri (jika ada)"></asp:Label><br>
                            <asp:FileUpload runat="server" ID="fileUpload3" CssClass="form-control" />
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                <span style="font-size: 14px; padding: 10px; color: red;">
                    <asp:Label runat="server" ID="lblInfoFileUnggah3" Text="(Ukuran File Maksimal 1 MB dengan format PDF)"></asp:Label>
                </span>
            </div>

            <div style="margin: 10px;">
                <hr>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-12 text-right">
                    <asp:LinkButton runat="server" ID="lbSimpan" CssClass="btn btn-success" OnClick="lbSimpan_Click">				
                        <i class="fa fa-save""></i>&nbsp;&nbsp;Simpan
                    </asp:LinkButton>
                </div>
            </div>

        </div>
    </div>
</div>
