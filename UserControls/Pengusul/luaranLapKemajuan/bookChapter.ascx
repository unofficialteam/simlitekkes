<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bookChapter.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.bookChapter" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Book Chapter">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status book chapter
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusBookChapter" CssClass="form-control"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran"
                        OnSelectedIndexChanged="ddlStatusBookChapter_SelectedIndexChanged" 
                        AutoPostBack="true" >
                        <%--<asp:ListItem Text="Belum Terbit" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Terbit" Value="2"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 10px;">
            <div class="col-lg-2">
                Judul buku
            </div>
            <div class="col-lg-10">
                <asp:TextBox runat="server" ID="tbJudulBuku" CssClass="form-control"
                    placeholder="Judul buku" Text=""></asp:TextBox>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlISBN" Visible="false">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    ISBN
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbISBN" CssClass="form-control" placeholder="ISBN" Text=""></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <div class="row" style="margin-top: 10px;">
            <div class="col-lg-2">
                Judul book chapter
            </div>
            <div class="col-lg-10">
                <asp:TextBox runat="server" ID="tbJudulBookChapter" CssClass="form-control max-textarea"
                    placeholder="Judul book chapter" TextMode="MultiLine" Rows="3" Text=""></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-top: 10px;">
            <div class="col-lg-2">
                Nama penerbit
            </div>
            <div class="col-lg-10">
                <asp:TextBox runat="server" ID="tbNamaPenerbit" CssClass="form-control"
                    placeholder="Nama penerbit" Text=""></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-top: 10px;">
            <div class="col-lg-2">
                URL website penerbit
            </div>
            <div class="col-lg-10">
                <asp:TextBox runat="server" ID="tbURLWebsitePenerbit" CssClass="form-control"
                    placeholder="Apabila ada" Text=""></asp:TextBox>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlUrlWebsiteBuku" Visible="false">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    URL website buku
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbURLWebsiteBuku" CssClass="form-control"
                        placeholder="Apabila ada" Text=""></asp:TextBox>
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
        <asp:Panel runat="server" ID="pnlDokumen1" Visible="false">
            <table style="margin-top: 10px;">
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Gray" OnClick="lbUnduhPdfDok_Click">			
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblJudulUnggah1" Text=""></asp:Label><br>
                            <asp:FileUpload runat="server" ID="fileUpload1" CssClass="form-control" />
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                <div>
                    <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran File Maksimal <asp:Label runat="server" ID="lblUkuranMaksDok1" ></asp:Label> MB dengan format PDF)
                    </span>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDokumen2" Visible="false">
            <table style="margin-top: 20px;">
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok2" Text="" ForeColor="Gray" OnClick="lbUnduhPdfDok2_Click">			
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblJudulUnggah2" Text=""></asp:Label><br>
                            <asp:FileUpload runat="server" ID="fileUpload2" CssClass="form-control" />
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                    <span style="font-size: 14px; padding: 10px; color: red; font-style: italic;">(Ukuran File Maksimal <asp:Label runat="server" ID="lblUkuranMaksDok2" ></asp:Label> MB dengan format PDF)
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
