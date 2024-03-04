<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tanggungJawabBelanja.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.tanggungJawabBelanja" %>


<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

<section class="content-header">
</section>
<div class="row">
    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-12">
        <div class="card">
            <div class="card-block">
                <div class="md-card-block">
                    <asp:MultiView runat="server" ID="mvMain">
                        <asp:View runat="server" ID="vDaftarUsulanLuaran">

                            <div class="row">
                                <div class="col-lg-8">
                                    <strong style="font-size: 28px;">Surat Pernyataan Tanggung Jawab Belanja</strong>
                                    <br />
                                    Tahun Pelaksanaan: 
                                        <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                                            CssClass="custom-select"
                                            OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                        </asp:DropDownList>
                                </div>
                                <div class="col-lg-5" style="text-align: right;">
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12 table-responsive">
                                    <asp:GridView runat="server" ID="gvSPTB" AutoGenerateColumns="False" Width="100%"
                                        CssClass="table-striped table-hover"
                                        DataKeyNames="id_usulan_kegiatan, id_skema, nama_skema, kd_program_hibah, program_hibah, judul, kd_sts_unggah,kd_sts_unggah_2,kd_sts_unggah_1, thn_pelaksanaan_kegiatan,status_tunggal"
                                        CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gvSPTB_RowDataBound" OnRowCommand="gvSPTB_RowCommand">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNomor" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="25px" />
                                                <ItemStyle Width="25px" HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Program">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProgamHibah" runat="server" Font-Bold="true" Text='<%# Bind("program_hibah") %>'></asp:Label><br />
                                                    <asp:Label ID="lblSkema" runat="server" ForeColor="DarkGreen" Text='<%# Bind("nama_skema") %>'></asp:Label>
                                                    <br>
                                                    Usulan tahun ke <span style="color: maroon;"><%# Eval("urutan_thn_usulan_kegiatan") %>&nbsp;dari <%# Eval("lama_kegiatan") %>&nbsp;tahun</span>
                                                </ItemTemplate>
                                                <HeaderStyle Width="250px" />
                                                <ItemStyle Width="250px" HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Judul">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("judul") %>'></asp:Label><br />
                                                    <span style="background-color: red">
                                                        <asp:Label ID="lblStsAktif" runat="server" Font-Bold="true" Visible="false" Text='<%# Bind("kd_sts_unggah") %>'></asp:Label>
                                                    </span>
                                                </ItemTemplate>
                                                <HeaderStyle Width="550px" />
                                                <ItemStyle Width="550px" HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aksi">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblketsptb100" runat="server" Font-Bold="true" Text='SPTB 100%' Font-Size="Smaller"></asp:Label>
                                                    <asp:LinkButton ID="lbCetakPengesahan100" runat="server" CommandName="cetakPengesahan100"
                                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Cetak Surat Pernyataan Tanggung Jawab Belanja 100%"
                                                        CssClass="fa fa-print btn btn-primary">
                                                    </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbUnggahTanggungJawabBelanja100" runat="server" CommandName="unggahDokumen100"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unggah Surat Pernyataan Tanggung Jawab Belanja 100%"
                                                            CssClass="fa fa-upload btn btn-primary">
                                                        </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbUnduhTanggungJawabBelanja100" runat="server" CommandName="unduhDokumen100"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Surat Pernyataan Tanggung Jawab Belanja 100%"
                                                            CssClass="fa fa-file-pdf-o btn btn-default">
                                                        </asp:LinkButton><br />


                                                    <asp:Label ID="lblketsptb70" runat="server" Font-Bold="true" Text='SPTB 70%' Font-Size="Smaller"></asp:Label>&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbCetakPengesahan70" runat="server" CommandName="cetakPengesahan70"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Cetak Surat Pernyataan Tanggung Jawab Belanja 70%"
                                                            CssClass="fa fa-print btn btn-primary">
                                                        </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbUnggahTanggungJawabBelanja70" runat="server" CommandName="unggahDokumen70"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unggah Surat Pernyataan Tanggung Jawab Belanja 70%"
                                                            CssClass="fa fa-upload btn btn-primary">
                                                        </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbUnduhTanggungJawabBelanja70" runat="server" CommandName="unduhDokumen70"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Surat Pernyataan Tanggung Jawab Belanja 70%"
                                                            CssClass="fa fa-file-pdf-o btn btn-default">
                                                        </asp:LinkButton>
                                                    <br />

                                                    <asp:Label ID="lblketsptb30" runat="server" Font-Bold="true" Text='SPTB 100%' Font-Size="Smaller"></asp:Label>&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbCetakPengesahan30" runat="server" CommandName="cetakPengesahan30"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Cetak Surat Pernyataan Tanggung Jawab Belanja 100%"
                                                            CssClass="fa fa-print btn btn-primary">
                                                        </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbUnggahTanggungJawabBelanja30" runat="server" CommandName="unggahDokumen30"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unggah Surat Pernyataan Tanggung Jawab Belanja 100%"
                                                            CssClass="fa fa-upload btn btn-primary">
                                                        </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lbUnduhTanggungJawabBelanja30" runat="server" CommandName="unduhDokumen30"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Surat Pernyataan Tanggung Jawab Belanja 100%"
                                                            CssClass="fa fa-file-pdf-o btn btn-default">
                                                        </asp:LinkButton>
                                                    <br />




                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="color: red; text-align: left;">
                                                Data tidak ditemukan.
                                            </div>
                                        </EmptyDataTemplate>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </asp:View>
                        <asp:View runat="server" ID="vPengesahan">
                            <div style="margin-left: auto; margin-right: auto; width: 800px;">
                                <div style="text-align: left; float: left;">
                                    <asp:LinkButton ID="btKembali" runat="server" Text="Kembali" OnClick="btKembali_Click"
                                        CssClass="btn btn-primary btn-xs"><i class="fa fa-undo"></i>&nbsp;Kembali </asp:LinkButton>
                                </div>
                                <div style="text-align: right; float: right;">
                                    <asp:LinkButton ID="btnExport" runat="server" Text="Cetak PDF" OnClick="btnExport_Click"
                                        CssClass="btn btn-primary btn-xs"><i class="fa fa-file-pdf-o"></i>&nbsp;Cetak PDF</asp:LinkButton>
                                </div>                                
                                <div style="text-align: right; float: right;">
                                    <asp:LinkButton ID="btSave" runat="server" Text="Simpan" 
                                        CssClass="btn btn-primary btn-xs" OnClick="btSave_Click"><i class="fa fa-floppy-o"></i>&nbsp;Simpan</asp:LinkButton>
                                </div>                                

                                <div style="clear: both;"></div>
                            </div>

                            <div style="margin-left: auto; margin-right: auto; width: 800px; border: 1px solid gray; padding: 5px; text-align: left">
                                <asp:Panel ID="panelPengesahan" runat="server" Font-Names="Times">
                                    <table width="100%" cellpadding="-3">
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Label runat="server" ID="lbTitle" Style="font-weight: bold; text-decoration: underline;" Text="SURAT PERNYATAAN TANGGUNG JAWAB BELANJA"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="color: White;">.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Yang bertanda tangan di bawah ini :</td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="-3" cellspacing="0">
                                        <tr>
                                            <td width="10%" style="text-align: left;">
                                                <asp:Label runat="server" ID="lbNama" Text="Nama"></asp:Label>
                                            </td>
                                            <td style="text-align: left;" width="3%">:
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label runat="server" ID="lblNamaLengkapKetua" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td style="text-align: left;" width="10%" valign="top">
                                                <asp:Label runat="server" ID="lbAlamat" Text="Alamat"></asp:Label>
                                            </td>
                                            <td style="text-align: left;" width="3%" valign="top">:
                                            </td>
                                            <td style="text-align: left" valign="top">
                                                <asp:Label runat="server" ID="lblAlamat" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="10%">&nbsp;</td>
                                            <td width="3%">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">berdasarkan Surat Keputusan Nomor&nbsp;
                                            <asp:TextBox ID="tbNomorSuratKeputusan" runat="server" Text="" Width="200" />
                                                <asp:Label runat="server" ID="lbNomorSuratKeputusan" Text="" Visible="false" Width="200"></asp:Label>
                                                &nbsp; dan Perjanjian / Kontrak Nomor&nbsp;
                                            <asp:TextBox ID="tbNomorKontrak" runat="server" Text="" Width="200" />
                                                <asp:Label runat="server" ID="lbNomorKontrak" Text="" Visible="false" Width="200"></asp:Label>
                                                mendapatkan Anggaran Penelitian&nbsp;
                                            <asp:Label ID="lblJudul" runat="server" Text=""></asp:Label>
                                                &nbsp;sebesar
                                            <asp:TextBox ID="tbAnggaran" runat="server" Text="" Width="150px" onkeyup="insertcommas(this);"></asp:TextBox>
                                                <asp:Label runat="server" ID="lbAnggaran" Text="" Visible="false" Width="200"></asp:Label>
                                                .</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="font-weight: normal;">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="font-weight: normal;">Dengan ini menyatakan bahwa :</td>
                                        </tr>
                                        <br />
                                        <tr>
                                            <td colspan="3" style="font-weight: normal;">1. Biaya kegiatan penelitian di bawah ini meliputi :</td>
                                        </tr>
                                        <tr style="color: White;">
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%" border="1" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <th width="10%" align="center">No</th>
                                            <th>Uraian</th>
                                            <th width="20%" align="center">Jumlah</th>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 13px;" align="center" valign="top" width="10%">01
                                            </td>
                                            <td style="line-height: 13px;" valign="top">
                                                <b>
                                                    <asp:Label runat="server" ID="lblHonorarium" Text="Bahan"></asp:Label></b><br />
                                                <textarea runat="server" id="taUraian01" cols="65" rows="4"></textarea>
                                                <asp:Label runat="server" ID="lblUraian1" Text="" Visible="false"></asp:Label>
                                            </td>
                                            <td style="line-height: 13px;" width="20%" align="center">
                                                <asp:TextBox ID="tbJumlah1" runat="server" Text="" onkeyup="insertcommas(this);"></asp:TextBox>
                                                <asp:Label runat="server" ID="lblJumlah1" Text="" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 13px;" align="center" valign="top" width="10%">02
                                            </td>
                                            <td style="line-height: 13px;" valign="top">
                                                <b>
                                                    <asp:Label runat="server" ID="lblPeralatanPenunjang" Text="Pengumpulan Data"></asp:Label></b><br />
                                                <textarea runat="server" id="taUraian02" cols="65" rows="4"></textarea>
                                                <asp:Label runat="server" ID="lblUraian2" Text="" Visible="false"></asp:Label>
                                            </td>
                                            <td style="line-height: 13px;" width="20%" align="center">
                                                <asp:TextBox ID="tbJumlah2" runat="server" Text="" onkeyup="insertcommas(this);"></asp:TextBox>
                                                <asp:Label runat="server" ID="lblJumlah2" Text="" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 13px;" align="center" valign="top" width="10%">03
                                            </td>
                                            <td style="line-height: 13px;" valign="top">
                                                <b>
                                                    <asp:Label runat="server" ID="lblBahanHabisPakai" Text="Analisis Data (Termasuk Sewa Peralatan)"></asp:Label></b><br />
                                                <textarea runat="server" id="taUraian03" cols="65" rows="4"></textarea>
                                                <asp:Label runat="server" ID="lblUraian3" Text="" Visible="false"></asp:Label>
                                            </td>
                                            <td style="line-height: 13px;" width="20%" align="center">
                                                <asp:TextBox ID="tbJumlah3" runat="server" Text="" onkeyup="insertcommas(this);"></asp:TextBox>
                                                <asp:Label runat="server" ID="lblJumlah3" Text="" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 13px;" align="center" valign="top" width="10%">04
                                            </td>
                                            <td style="line-height: 13px;" valign="top">
                                                <b>
                                                    <asp:Label runat="server" ID="lblPerjalanan" Text="Pelaporan, Luaran Wajib dan Luaran Tambahan"></asp:Label></b><br />
                                                <textarea runat="server" id="taUraian04" cols="65" rows="4"></textarea>
                                                <asp:Label runat="server" ID="lblUraian4" Text="" Visible="false"></asp:Label>
                                            </td>
                                            <td style="line-height: 13px;" width="20%" align="center">
                                                <asp:TextBox ID="tbJumlah4" runat="server" Text="" onkeyup="insertcommas(this);"></asp:TextBox>
                                                <asp:Label runat="server" ID="lblJumlah4" Text="" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 13px;" align="center" valign="top" width="10%"></td>
                                            <td style="line-height: 13px;" valign="top" align="center"><b>Jumlah</b></td>
                                            <td style="line-height: 13px;" width="20%" align="center">
                                                <b>
                                                    <asp:Label ID="lblJumlahTotal" runat="server" Text="" Visible="false"></asp:Label></b>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="-3" width="100%">
                                        <tr style="color: White;">
                                            <td>.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>2. Jumlah uang tersebut pada angka 1, benar-benar dikeluarkan untuk pelaksanaan kegiatan penelitian dimaksud.</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Demikian surat pernyataan ini dibuat dengan sebenarnya.</td>
                                        </tr>
                                        <tr style="color: White;">
                                            <td>.
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" border="0" cellspacing="3" cellpadding="-3">
                                        <tr>
                                            <td style="line-height: 13px; width: 50%; text-align: center">&nbsp;</td>
                                            <td style="line-height: 13px; width: 50%; text-align: center">
                                                <asp:TextBox runat="server" ID="tbKotaNTgl" Text="Kota, tgl-bln-thn" placeholder="Kota, tgl-bln-thn" />
                                                <asp:Label runat="server" ID="lblKotaNTgl" Text="" Visible="false"></asp:Label><br />
                                                <asp:Label runat="server" ID="lblKetua" Text="Ketua,"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: White;">
                                            <td>&nbsp;<asp:Label ID="Label1" runat="server" Text="." ForeColor="White"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="." ForeColor="White"></asp:Label></td>
                                        </tr>
                                        <tr style="color: White;">
                                            <td>&nbsp;<asp:Label ID="Label3" runat="server" Text="." ForeColor="White"></asp:Label></td>
                                            <td>&nbsp;<asp:Label ID="Label4" runat="server" Text="." ForeColor="White"></asp:Label></td>
                                        </tr>
                                        <tr style="color: White;">
                                            <td>&nbsp;<asp:Label ID="Label5" runat="server" Text="." ForeColor="White"></asp:Label></td>
                                            <td>&nbsp;<asp:Label ID="Label6" runat="server" Text="." ForeColor="White"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 13px; text-align: center"></td>
                                            <td style="line-height: 13px; text-align: center">(<asp:Label runat="server" ID="lblNamKetua" Text=" "></asp:Label>)<br />
                                                NIP/NIK
                                            <asp:TextBox runat="server" ID="tbNipKetua" Text="" />
                                                <asp:Label runat="server" ID="lblNipKetua" Text="---" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="color: White;">
                                            <td>&nbsp;</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </asp:View>
                        <asp:View runat="server" ID="vKelengkapanLuaran">
                            <div class="row">
                                <%--<div class="col-lg-6 ">
                                    <asp:Label runat="server" ID="lblKelompokLuaran" Text="Luaran Wajib" Font-Size="24px" Font-Bold="true"></asp:Label>
                                    </div>
                                <div class="col-lg-6 text-right">
                            <asp:LinkButton runat="server" ID="lbKembali" CssClass="btn btn-primary btn-sm" OnClick="lbKembali_Click">
                                                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Kembali
                                            </asp:LinkButton>
                                    </div>--%>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                </div>
                            </div>


                        </asp:View>

                        <asp:View runat="server" ID="vIsianLuaran">

                            <div class="row">
                                <div class="col-lg-6 ">
                                </div>
                                <div class="col-lg-6 text-right">
                                    <asp:LinkButton runat="server" ID="lbKembali2" CssClass="btn btn-primary btn-sm" OnClick="lbKembali2_Click">
                                                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Kembali
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <br />


                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="modal fade" id="modalUnggahDokumen" role="dialog" aria-labelledby="myModalUploadDokumen">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button runat="server" type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true" align: center" class="modal-title" id="myModalUploadDokumen">Unggah Tanggung Jawab Belanja</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div class="box-body">
                        <ol>
                            <div class="row">
                                <div class="col-sm-2"><i class="fa fa-caret-right"></i>&nbsp;&nbsp;&nbsp;Skema</div>
                                <div class="col-sm-10">
                                    <asp:Label ID="lblSkemaUnggah" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2"><i class="fa fa-caret-right"></i>&nbsp;&nbsp;&nbsp;Judul</div>
                                <div class="col-sm-10">
                                    <asp:Label ID="lblJudulUnggah" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </ol>

                        <div class="form-group">
                            <label for="fileUpload1" class="col-sm-4 control-label">Pilih Berkas</label>
                            <div class="col-sm-8">
                                <asp:FileUpload ID="fileUpload1" runat="server" CssClass="form-control" Height="40px" />
                                <span class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="lbUnggahDokumen" CssClass="btn btn-info btn-flat" OnClick="lbUnggahDokumen_Click">
                                  <i class="fa fa-cloud-upload">&nbsp; Unggah</i></asp:LinkButton>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
            </div>
        </div>
    </div>
</div>
<div class="modal modal-danger" id="modalKonfirmasi" role="dialog" aria-labelledby="mymodalKonfirmasi">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h5 class="modal-title" id="mymodalKonfirmasi">Simpan Isian SPTB</h5>
            </div>
            <div class="modal-body">
                Dana Anggaran&nbsp;:&nbsp;<asp:Label ID="lbldanaanggaran" runat="server" Text="0"></asp:Label><br/>
                Dana Pemakaian&nbsp;<asp:Label ID="lbpersen" runat="server" Text="0" Visible="false"></asp:Label>&nbsp:&nbsp
                <asp:Label ID="lbpemakaian" runat="server" Text="0"></asp:Label>&nbsp<asp:Label ID="lbterbilang" runat="server" Text="0" Visible="false"></asp:Label>
                <br/>
                Anda Yakin akan menyimpan?
                
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbModalStsKonfirmasi" runat="server" CssClass="btn btn-success"
                    OnClick="lbModalStsKonfirmasi_Click" OnClientClick="$('#modalKonfirmasi').modal('hide');">
                    <asp:Label ID="lblModalStsKonfirmasi" runat="server" Text="Ya"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>
        <script type="text/javascript">
            function insertcommas(nField) {
                if (/^0/.test(nField.value)) {
                    nField.value = nField.value.substring(0, 1);
                }
                if (Number(nField.value.replace(/,/g, ""))) {
                    var tmp = nField.value.replace(/,/g, "");
                    tmp = tmp.toString().split('').reverse().join('').replace(/(\d{3})/g, '$1,').split('').reverse().join('').replace(/^,/, '');
                    if (/\./g.test(tmp)) {
                        tmp = tmp.split(".");
                        tmp[1] = tmp[1].replace(/\,/g, "").replace(/ /, "");
                        nField.value = tmp[0] + "." + tmp[1]
                    }
                    else {
                        nField.value = tmp.replace(/ /, "");
                    }
                }
                else {
                    nField.value = nField.value.replace(/[^\d\,\.]/g, "").replace(/ /, "");
                }
            }
        </script>
