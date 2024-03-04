<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="visitingLecturer.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.luaranLapKemajuan.visitingLecturer" %>

<div class="row" style="padding: 10px;">
    <div class="col-lg-12">
        <div>
            <asp:Label runat="server" ID="lblJudulForm" ForeColor="Green" Font-Bold="true"
                Text="Artikel Conference/Seminar Internasional">
            </asp:Label>
        </div>

        <div style="padding: 10px;">
            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Status artikel 
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlTargetJenisLuaranPublikasi" CssClass="form-control" AutoPostBack="true"
                        DataTextField="nama_target_jenis_luaran" DataValueField="id_target_jenis_luaran" AppendDataBoundItems="true"
                        OnSelectedIndexChanged="ddlTargetJenisLuaranPublikasi_SelectedIndexChanged">
                        <%--<asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>--%>
                        <%--<asp:ListItem Text="Published" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Accepted" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Sedang direview" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Submited" Value="4"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-2" style="display: none;">
                    Status penulis 
                </div>
                <div class="col-lg-4" style="display: none;">
                    <asp:DropDownList runat="server" ID="ddlPeranPenulis" CssClass="form-control"
                        DataTextField="nama_jenis_luaran" DataValueField="id_jenis_luaran">
                        <%--<asp:ListItem Text="--Pilih--" Value="0"></asp:ListItem>--%>
                        <asp:ListItem Text="First author" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Co-author" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Corresponding author" Value="3"></asp:ListItem>


                    </asp:DropDownList>
                </div>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Nama forum
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbNamaKonferens" CssClass="form-control" placeholder="Nama forum"></asp:TextBox>
                </div>
            </div>



            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Lembaga penyelenggara
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbLembagaPenyelenggara" CssClass="form-control" placeholder="Lembaga penyelenggara"></asp:TextBox>
                </div>
            </div>


            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Tempat penyelenggaraan
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbTempatPenyelenggaraan" CssClass="form-control" placeholder="Tempat penyelenggaraan"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Tgl penyelenggaraan
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbTglPenyelenggaraanMulai" CssClass="form-control" placeholder="Tgl penyelenggaraan mulai" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-lg-2">
                    sampai
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbTglPenyelenggaraanSelesai" CssClass="form-control" placeholder="Tgl penyelenggaraan selesai" TextMode="Date"></asp:TextBox>
                </div>
            </div>


            <div class="row" style="margin-top: 10px;">
                <%--<div class="col-lg-2">
                    ISSN/EISSN
                </div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="tbISSN_EISSN" CssClass="form-control" placeholder="ISSN/EISSN"></asp:TextBox>
                </div>--%>
                <%--<div class="col-lg-2">
                    Lembaga pengindek
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbLembagaPengindek" CssClass="form-control" placeholder="Lembaga pengindek"></asp:TextBox>
                </div>--%>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    URL website forum
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbUrlKonferensi" CssClass="form-control" placeholder="URL jurnal"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 10px;">
                <div class="col-lg-2">
                    Judul topik/materi
                </div>
                <div class="col-lg-10">
                    <asp:TextBox runat="server" ID="tbJudulArtikel" CssClass="form-control" placeholder="Judul topik/ materi"></asp:TextBox>
                </div>
            </div>


            <asp:Panel runat="server" ID="pnlVideoUrl" Visible="false">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-lg-2">
                        Link video kegiatan
                    </div>
                    <div class="col-lg-10">
                        <asp:TextBox runat="server" ID="tbLinkVideo" CssClass="form-control" placeholder="Link video kegiatan"></asp:TextBox>
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


