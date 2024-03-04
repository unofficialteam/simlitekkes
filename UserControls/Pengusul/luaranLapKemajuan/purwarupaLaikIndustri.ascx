<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="purwarupaLaikIndustri.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.purwarupaLaikIndustri" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Purwarupa Laik Industri">
            </asp:Label>
        </div>
        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status Purwarupa Laik Industri
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlStatusPurwarupaLaikIndustri" CssClass="form-control"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran"
                        OnSelectedIndexChanged="ddlStatusPurwarupaLaikIndustri_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama Purwarupa Laik Industri
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaPurwarupa" CssClass="form-control" placeholder="Nama Laik Industri" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Pemegang Purwarupa Laik Industri
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbPemegangPurwarupa" CssClass="form-control" placeholder="Ketik nama pemegang Purwarupa Laik Industri(pisahkan menggunakan titik koma)" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-3">
                   <asp:Label runat="server" ID="lblPeriodePenerapan"> Periode uji penerapan </asp:Label>
                    <asp:Label runat="server" ID="lblPeriodeCoba"> Periode uji coba </asp:Label>
                </div>
                <div class="col-lg-2">
                    Tanggal awal
                </div>
                <div class="col-lg-3">
                    <%--<asp:TextBox ID="tbTglAwal" runat="server" CssClass="form-control datepicker" MaxLength="10"
                        placeholder="yyyy-mm-dd" data-provide="datepicker" data-date-format="yyyy-mm-dd"
                        data-orientation="bottom auto" />--%>
                    <asp:TextBox ID="tbTglAwal" runat="server" CssClass="form-control" type="date" Text="" ></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    akhir
                </div>
                <div class="col-lg-3">
                    <%--<asp:TextBox ID="tbTglAkhir" runat="server" CssClass="form-control datepicker" MaxLength="10"
                        placeholder="yyyy-mm-dd" data-provide="datepicker" data-date-format="yyyy-mm-dd"
                        data-orientation="bottom auto" />--%>
                    <asp:TextBox ID="tbTglAkhir" runat="server" CssClass="form-control" type="date" Text="" ></asp:TextBox>
                </div>
            </div>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-4">
                    <asp:Label runat="server" ID="lblLinkVideoProduk">Link video dokumentasi pengujian</asp:Label>
                    <asp:Label runat="server" ID="lblLinkVideoModel">Link video dokumentasi pengujian</asp:Label>
                </div>
                <div class="col-lg-8">
                    <asp:TextBox runat="server" ID="tbLinkVideo" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
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
