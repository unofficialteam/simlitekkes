﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="kebijakan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.kebijakan" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Kebijakan">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status naskah kebijakan
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusKebijakan" CssClass="form-control"
                        OnSelectedIndexChanged="ddlStatusKebijakan_SelectedIndexChanged" AutoPostBack="true"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Jenis naskah kebijakan
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlJenisNaskahKebijakan" CssClass="form-control"
                        DataTextField="naskah_kebijakan" DataValueField="id_jenis_naskah_kebijakan" ClientIDMode="Static"
                        AppendDataBoundItems="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    <asp:Label runat="server" ID="lblNamaLembaga" Text=""></asp:Label>
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaLembaga" CssClass="form-control" Text=""></asp:TextBox>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlProduk" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Nama pejabat penerima produk naskah kebijakan
                    </div>
                    <div class="col-lg-10">
                        <asp:TextBox runat="server" ID="tbNamaPejabatPenerima" CssClass="form-control" Text=""></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Nama jabatan penerima
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="tbJabatanPenerima" CssClass="form-control" Text=""></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        Tgl. Penyerahan
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="tbTglPenyerahan" CssClass="form-control"
                            Type="date" Text=""></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlPenerapan" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Tgl. Mulai penerapan
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="tbTglMulaiPenerapan" CssClass="form-control"
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

            <asp:Panel runat="server" ID="pnlDokumen3" Visible="false">
                <table style="margin-top: 20px;">
                    <tr>
                        <td class="auto-style1">
                            <asp:LinkButton runat="server" ID="lbUnduhPdfDok3" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok3_Click">
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <div style="margin-left: 10px;">
                                <asp:Label runat="server" ID="lblNamaJenisDokumen3" Text=""></asp:Label><br>
                                <asp:FileUpload runat="server" ID="fileUpload3" CssClass="form-control" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div>
                    <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran File Maksimal
                    <asp:Label runat="server" ID="lblUkuranMaksDok3"></asp:Label>
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
