<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bukuAjar.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.bukuAjar" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Buku Ajar">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status buku ajar
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusBukuAjar" CssClass="form-control"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran"
                        OnSelectedIndexChanged="ddlStatusBukuAjar_SelectedIndexChanged" AutoPostBack="true">
                        <%--<asp:ListItem Text="Tahap review" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Tahap editing" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Tahap terbit" Value="3"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Judul buku
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbJudulBuku" CssClass="form-control" placeholder="Judul buku" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama penerbit
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaPenerbit" CssClass="form-control" placeholder="Nama penerbit" Text=""></asp:TextBox>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlIsbn" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        ISBN
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox runat="server" ID="tbIsbn" CssClass="form-control" placeholder="ISBN" Text=""></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        Tahun terbit
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox runat="server" ID="tbTahunTerbit" CssClass="form-control" TextMode="Number"
                            placeholder="Diisi dengan angka" Text=""></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        Jumlah halaman
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox runat="server" ID="tbJmlHalaman" CssClass="form-control"
                            placeholder="Diisi dengan angka" Text="" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    URL website penerbit
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbUrlWebsitePenerbit" CssClass="form-control" placeholder="Apabila ada" Text=""></asp:TextBox>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlUrlBuku" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        URL buku
                    </div>
                    <div class="col-lg-10">
                        <asp:TextBox runat="server" ID="tbUrlBuku" CssClass="form-control" placeholder="URL buku" Text=""></asp:TextBox>
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
            <asp:Panel runat="server" ID="pnlUnggah1" Visible="false">
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
                    <span style="font-size: 14px; padding: 10px; color: red;">
                        (Ukuran File Maksimal <asp:Label runat="server" ID="lblInfoFileUnggah1" Text=""></asp:Label> MB dengan format PDF)
                    </span>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlUnggah2" Visible="false">
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
                    <span style="font-size: 14px; padding: 10px; color: red;">
                        (Ukuran File Maksimal <asp:Label runat="server" ID="lblInfoFileUnggah2" Text=""></asp:Label> MB dengan format PDF
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
