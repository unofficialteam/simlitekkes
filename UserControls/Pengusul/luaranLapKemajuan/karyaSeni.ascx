<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="karyaSeni.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.karyaSeni" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Karya Senis">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status Karya Seni
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
                    Nama Karya Seni
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaKaryaSeni" CssClass="form-control" placeholder="Nama produk" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Pemegang Hak Karya Seni
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbPemegang" CssClass="form-control" placeholder="Nama produk" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Tgl. pagelaran/Pameran
                </div>
                <div class="col-lg-5">
                    Tanggal Awal<asp:TextBox runat="server" ID="tbTglAwal" CssClass="form-control"
                        Type="date" placeholder="Jika ada" Text=""></asp:TextBox>
                </div>
                <div class="col-lg-5">
                    Tanggal Akhir<asp:TextBox runat="server" ID="tbTglAkhir" CssClass="form-control"
                        Type="date" placeholder="Jika ada" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama Pagelaran/Pameran
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaPagelaran" CssClass="form-control"
                        placeholder="Nama Pagelaran" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Link video dokumentasi Pagelaran
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbLinkVideo" CssClass="form-control"
                        placeholder="Link video dokumentasi Pagelaran" Text=""></asp:TextBox>
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
                            <asp:Label runat="server" ID="lblJudulUnggah1" Text="Dokumen deskripsi dan spesifikasi produk"></asp:Label><br>
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
                            <asp:Label runat="server" ID="lblJudulUnggah2" Text="Dokumen hasil uji coba di lingkungan sebenarnya"></asp:Label><br>
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

            <%--<table style="margin-top: 10px;">
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="lbUnduhPdfDok3" Text="" ForeColor="Red" OnClick="lbUnduhPdfDok3_Click">
										<i class="far fa-file-pdf" style="font-size: 50px; "></i>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <div style="margin-left: 10px;">
                            <asp:Label runat="server" ID="lblJudulUnggah3" Text="Dokumentasi (foto) pengujian produk"></asp:Label><br>
                            <asp:FileUpload runat="server" ID="fileUpload3" CssClass="form-control" />
                        </div>
                    </td>
                </tr>
            </table>
            <div>
                <span style="font-size: 14px; padding: 10px; color: red;">
                    <asp:Label runat="server" ID="lblInfoFileUnggah3" Text="(Ukuran File Maksimal 1 MB dengan format PDF)"></asp:Label>
                </span>
            </div>--%>

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
