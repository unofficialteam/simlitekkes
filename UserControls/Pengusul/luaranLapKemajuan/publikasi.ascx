<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="publikasi.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.publikasi" %>


<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Artikel di Jurnal Internasional Terindeks di Pengindeks Bereputasi">
            </asp:Label>
        </div>

        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status artikel 
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlTargetJenisLuaranPublikasi" CssClass="form-control" AutoPostBack="true"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran" ClientIDMode="Static"
                        OnSelectedIndexChanged="ddlTargetJenisLuaranPublikasi_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-2">
                    Status penulis 
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlPeranPenulis" CssClass="form-control"
                        DataTextField="nama_jenis_luaran" DataValueField="id_jenis_luaran" AppendDataBoundItems="true">
                        <%--<asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>--%>
                        <asp:ListItem Text="First author" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Co-author" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Corresponding author" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama jurnal
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaJurnal" CssClass="form-control" placeholder="Nama Jurnal"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    ISSN/EISSN
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbISSN_EISSN" CssClass="form-control" placeholder="ISSN/EISSN"></asp:TextBox>
                </div>
                <div class="col-lg-2">
                    Lembaga pengindek
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbLembagaPengindek" CssClass="form-control" placeholder="Lembaga pengindek"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    URL jurnal
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbUrlJurnal" CssClass="form-control" placeholder="URL jurnal"></asp:TextBox>
                </div>
            </div>
            <asp:Panel runat="server" ID="panelPeringkatAkreditasi" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Peringkat akreditasi
                    </div>
                    <div class="col-lg-10">
                        <asp:TextBox runat="server" ID="tbPeringkatAkreditasi" CssClass="form-control" placeholder="Peringkat akreditasi" Width="150px"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Judul Artikel
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbJudulArtikel" CssClass="form-control" placeholder="Judul Artikel"></asp:TextBox>
                </div>
            </div>

            <asp:Panel runat="server" ID="panelAtributPublikasi">


                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Tahun
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox runat="server" ID="tbTahunPublikasi" CssClass="form-control" placeholder="Tahun" MaxLength="4" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        Volume
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox runat="server" ID="tbVolume" CssClass="form-control" placeholder="Volume" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        Nomor
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox runat="server" ID="tbNomor" CssClass="form-control" placeholder="Nomor" TextMode="Number"></asp:TextBox>
                    </div>
                </div>

                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Halaman awal
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox runat="server" ID="tbHalamanAwal" CssClass="form-control" placeholder="Halaman awal" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        akhir
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox runat="server" ID="tbHalamanAkhir" CssClass="form-control" placeholder="Halaman akhir" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        artikel
                    </div>
                    <div class="col-lg-2">
                    </div>
                </div>

                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        URL artikel
                    </div>
                    <div class="col-lg-10">
                        <asp:TextBox runat="server" ID="tbUrlArtikel" CssClass="form-control" placeholder="URL artikel"></asp:TextBox>
                    </div>
                </div>

                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        DOI artikel
                    </div>
                    <div class="col-lg-10">
                        <asp:TextBox runat="server" ID="tbDOI" CssClass="form-control" placeholder="DOI artikel"></asp:TextBox>
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

            <asp:Panel runat="server" ID="pnlDokumen1">

                <table style="margin-top: 10px;">
                    <tr>
                        <td>

                            <asp:LinkButton runat="server" ID="lbUnduhPdfDok" Text="" ForeColor="Gray" OnClick="lbUnduhPdfDok_Click">
										<i class="far fa-file-pdf" style="font-size: 50px; "></i>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <div style="margin-left: 10px;">
                                <asp:Label runat="server" ID="lblNamaJenisDokumen1" Text="Naskah artikel"></asp:Label>
                                <br>
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

            </asp:Panel>


            <asp:Panel runat="server" ID="pnlDokumen2">
                <table style="margin-top: 20px;">
                    <tr>
                        <td>

                            <asp:LinkButton runat="server" ID="lbUnduhPdfDok2" Text="" ForeColor="Gray" OnClick="lbUnduhPdfDok2_Click">
										<i class="far fa-file-pdf" style="font-size: 50px; "></i>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <div style="margin-left: 10px;">
                                <asp:Label runat="server" ID="lblNamaJenisDokumen2" Text="Bukti submit"></asp:Label>
                                <br>
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

