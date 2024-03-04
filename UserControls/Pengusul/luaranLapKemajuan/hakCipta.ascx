<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="hakCipta.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.hakCipta" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Hak Cipta">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status desain
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusHakCipta" CssClass="form-control"
                        OnSelectedIndexChanged="ddlStatusHakCipta_SelectedIndexChanged" AutoPostBack="true"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama ciptaan
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaCiptaan" CssClass="form-control" placeholder="Nama ciptaan" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Pemegang hak cipta
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbPemegangHakCipta" CssClass="form-control"
                        placeholder="Ketik nama pemegang hak cipta (pisahkan menggunakan titik koma)" Text=""></asp:TextBox>
                </div>
            </div>
            
            <asp:Panel runat="server" ID="pnlPencatatan" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        No. Pencatatan hak cipta
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="tbNoPencatatanHakCipta" CssClass="form-control" placeholder="No. Pencatatan hak cipta" Text=""></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        Tgl. Pencatatan
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="tbTglPencatatan" CssClass="form-control"
                            Type="date" placeholder="Jika ada" Text=""></asp:TextBox>
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
                        <td>
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
