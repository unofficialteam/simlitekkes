<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="paten.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.paten" %>

<style type="text/css">
    .auto-style1 {
        width: 10px;
    }
</style>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Paten">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    <asp:Label runat="server" ID="lblStatusPaten" Text=""></asp:Label>
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusPaten" CssClass="form-control"
                        OnSelectedIndexChanged="ddlStatusPaten_SelectedIndexChanged" AutoPostBack="true"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran" >
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    <asp:Label runat="server" ID="lblNamaPaten" Text=""></asp:Label>
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaPaten" CssClass="form-control" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    <asp:Label runat="server" ID="lblPemegangPaten" Text=""></asp:Label>
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbPemegangPaten" CssClass="form-control"
                        placeholder="Ketik nama pemegang paten (pisahkan menggunakan titik koma)" Text=""></asp:TextBox>
                </div>
            </div>

            <asp:Panel runat="server" ID="pnlNoPendaftaran" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Nomor pendaftaran
                    </div>
                    <div class="col-lg-10">
                        <asp:TextBox runat="server" ID="tbNomorPendaftaran" CssClass="form-control" Text=""></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlGranted" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Nomor Granted/sertifikat
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="tbNoGrantedSertifikat" CssClass="form-control" Text=""></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        Tgl. Pencatatan
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="tbTglPencatatan" CssClass="form-control"
                            Type="date" Text=""></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

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
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok1" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok1_Click">
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblNamaJenisDokumen1" Text=""></asp:Label><br>
                            <asp:FileUpload runat="server" ID="fileUpload1" CssClass="form-control" />
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran File Maksimal
                    <asp:Label runat="server" ID="lblUkuranMaksDok1"></asp:Label>
                    MB dengan format PDF)
                </span>
            </div>

            <asp:Panel runat="server" ID="pnlDokumen2" Visible="false">
                <table style="margin-top: 20px;">
                    <tr>
                        <td class="auto-style1">
                            <asp:LinkButton runat="server" ID="lbUnduhPdfDok2" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok2_Click">
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <div style="margin-left: 10px;">
                                <asp:Label runat="server" ID="lblNamaJenisDokumen2" Text=""></asp:Label><br>
                                <asp:FileUpload runat="server" ID="fileUpload2" CssClass="form-control" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div>
                    <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran File Maksimal
                    <asp:Label runat="server" ID="lblUkuranMaksDok2"></asp:Label>
                        MB dengan format PDF)
                    </span>
                </div>
            </asp:Panel>

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
