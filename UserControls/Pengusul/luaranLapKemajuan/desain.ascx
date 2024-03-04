<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="desain.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.desain" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Desain">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status desain
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusDesain" CssClass="form-control"
                        OnSelectedIndexChanged="ddlStatusDesain_SelectedIndexChanged" AutoPostBack="true"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama desain
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaDesain" CssClass="form-control" placeholder="Nama desain" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Pemegang desain
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbPemegangDesain" CssClass="form-control"
                        placeholder="Ketik nama pemegang desain (pisahkan menggunakan titik koma" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Link video dokumentasi ujicoba/simulasi desain
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbLinkVideo" CssClass="form-control"
                        placeholder="Link video dokumentasi ujicoba/simulasi desain" Text=""></asp:TextBox>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlTglAwalDiterapkan" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Tanggal awal diterapkan
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="tbTglAwalDiterapkan" CssClass="form-control"
                            Type="date" placeholder="" Text=""></asp:TextBox>
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
                            <asp:Label runat="server" ID="lblNamaJenisDokumen1" Text="Deskripsi dan spesifikasi desain"></asp:Label><br>
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
                    <td>
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok2" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok2_Click">			
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblNamaJenisDokumen2" Text="Dokumen hasil uji coba/simulasi terakhir desain"></asp:Label><br>
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

            <table style="margin-top: 10px;">
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok3" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok3_Click">
										<i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblNamaJenisDokumen3" Text="Dokumentasi (foto) uji coba/simulasi terakhir desain"></asp:Label><br>
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
