<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pVT.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.pVT" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Perlindungan Varietas Tanaman">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status PVT
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusPVT" CssClass="form-control"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran"
                        OnSelectedIndexChanged="ddlStatusPVT_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama Varietas
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaVarietas" CssClass="form-control" placeholder="Nama varietas" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama Pemulia Varietas
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaPemuliaVarietas" CssClass="form-control" placeholder="Ketik nama pemegang desain (pisahkan menggunakan titik koma)" Text=""></asp:TextBox>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlNomorSurat" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Nomor Surat Perlindungan Sementara
                    </div>
                    <div class="col-lg-6">
                        <asp:TextBox runat="server" ID="tbNoSurat" CssClass="form-control" placeholder="No Surat Perlindungan Sementara" Text=""></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        Tanggal terbit
                    </div>
                    <div class="col-lg-2">
                        <%--<asp:TextBox ID="tbTglTerbitSurat" runat="server" CssClass="form-control datepicker" MaxLength="10"
                            placeholder="yyyy-mm-dd" data-provide="datepicker" data-date-format="yyyy-mm-dd"
                            data-orientation="bottom auto" />--%>
                        <asp:TextBox ID="tbTglTerbitSurat" runat="server" CssClass="form-control" type="date" Text="" ></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlNoSertifikat" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Nomor Sertifikat PVT
                    </div>
                    <div class="col-lg-6">
                        <asp:TextBox runat="server" ID="tbNoSertifikatPVT" CssClass="form-control" placeholder="No Sertifikat PVT" Text=""></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        Tanggal terbit
                    </div>
                    <div class="col-lg-2">
                        <%--<asp:TextBox ID="tbTglTerbitSertifikat" runat="server" CssClass="form-control datepicker" MaxLength="10"
                            placeholder="yyyy-mm-dd" data-provide="datepicker" data-date-format="yyyy-mm-dd"
                            data-orientation="bottom auto" />--%>
                        <asp:TextBox ID="tbTglTerbitSertifikat" runat="server" CssClass="form-control" type="date" Text="" ></asp:TextBox>
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
                    <span style="font-size: 14px; padding: 10px; color: red;">(Ukuran File Maksimal
                    <asp:Label runat="server" ID="lblInfoFileUnggah1" Text=""></asp:Label>
                        MB dengan format PDF)
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
                    <span style="font-size: 14px; padding: 10px; color: red;">(Ukuran File Maksimal
                    <asp:Label runat="server" ID="lblInfoFileUnggah2" Text=""></asp:Label>
                        MB dengan format PDF
                    </span>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlUnggah3" Visible="false">
                <table style="margin-top: 20px;">
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lbUnduhPdfDok3" Text="" ForeColor="Gray" OnClick="lbUnduhPdfDok3_Click">			
                            <i class="far fa-file-pdf" style="font-size: 50px; "></i>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <div style="margin-left: 10px;">
                                <asp:Label runat="server" ID="lblJudulUnggah3" Text=""></asp:Label><br>
                                <asp:FileUpload runat="server" ID="fileUpload3" CssClass="form-control" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div>
                    <span style="font-size: 14px; padding: 10px; color: red;">(Ukuran File Maksimal
                    <asp:Label runat="server" ID="lblInfoFileUnggah3" Text=""></asp:Label>
                        MB dengan format PDF
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
