<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="catatanHarian.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.catatanHarian" %>
<%@ Register Src="~/Helper/modalKonfirmasi.ascx" TagPrefix="uc" TagName="modalKonfirmasi" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

<%--<asp:UpdatePanel ID="upCatatanHarian" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="lbUnggah" />
    </Triggers>
    <ContentTemplate>--%>
<asp:MultiView ID="mvCatatanHarian" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftarSkema" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header bg-info text-white">
                        <h5>Catatan Harian</h5>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="form-inline mt-2 mb-2">
                            <label for="ddlThnPelaksanaan" class="ml-2">Tahun Pelaksanaan&nbsp;</label>
                            <asp:DropDownList runat="server" ID="ddlThnPelaksanaan" Enabled="true" CssClass="form-control basic-single mr-sm-2"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                <asp:ListItem Text="2020" Value="2020" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-block" style="min-height: 400px;">
                        <div class="md-card-block">
                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th style="width: 30px; text-align: center;">No.</th>
                                        <th style="width: 140px;">Skema</th>
                                        <th>Judul</th>
                                        <th style="width: 240px;">Keterangan</th>
                                        <th style="width: 100px; text-align: center;"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvDaftarUsulan" runat="server"
                                        DataKeyNames="id_usulan_kegiatan,jenis_kegiatan,nama_skema,judul,lama_kegiatan,urutan_thn_usulan_kegiatan"
                                        OnItemEditing="lvDaftarUsulan_ItemEditing">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>
                                                <td>
                                                    <%# Eval("jenis_kegiatan") %>&nbsp;<%# Eval("program_hibah") %>
                                                    <b><%# Eval("nama_skema") %></b>
                                                </td>
                                                <td>
                                                    <%# Eval("judul") %>
                                                    <hr />
                                                    Dana Hibah :&nbsp;<%# Eval("dana_disetujui", "{0:0,00}") %>
                                                </td>
                                                <td>Jumlah Catatan :&nbsp;<b><%# Eval("jml_catatan") %></b><br />
                                                    Persentase Capaian :&nbsp;<b><%# Eval("persentase_capaian_maks") + "%" %></b><br />
                                                    <asp:Panel ID="panelDanaTerserap" runat="server"
                                                        Visible='<%# Eval("jenis_kegiatan").ToString() != "Penelitian" %>'>
                                                        Dana Terserap :&nbsp;<b><%# Eval("persentase_dana_terserap", "{0:0}") + "%" %></b>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit"
                                                        CssClass="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td colspan="5">
                                                    <p class="text-primary text-center">Tidak ada data...</p>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View ID="vDaftarCatatan" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header bg-info text-white">
                        <h5>Catatan Harian</h5>
                    </div>
                </div>
                <div class="row mt-2 mb-2">
                    <div class="col-sm-12 text-right">
                        <asp:LinkButton ID="lbKembaliKeDaftar" runat="server" CssClass="btn btn-primary"
                            OnClick="lbKembaliKeDaftar_Click">
                                        <i class="fa fa-chevron-left"></i>&nbsp;Kembali
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-header-text">Data Kegiatan Penelitian</h5>
                    </div>
                    <div class="card-body">
                        <div class="md-card-block p-b-10">
                            <div class="row">
                                <div class="col-sm-7">
                                    <asp:Label ID="lblKegiatan" runat="server" CssClass="badge badge-success" Font-Size="Small"></asp:Label>
                                </div>
                                <div class="col-sm-5 text-right">
                                    Tahun Pelaksanaan&nbsp;<asp:Label ID="lblTahunPelaksanaan" runat="server" CssClass="badge badge-warning" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <b>Judul :</b><br />
                            <asp:Label ID="lblJudul" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-2">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-block" style="min-height: 400px;">
                        <div class="md-card-block">
                            <div class="row">
                                <div class="col-sm-12 text-center pb-3">
                                    <asp:Label runat="server" ID="lblJmlCatHarPerBln" CssClass="badge badge-info" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5 form-inline">
                                    <asp:LinkButton ID="lbTambahCatatan" runat="server" CssClass="btn btn-primary"
                                        OnClick="lbTambahCatatan_Click">
                                                <i class="fa fa-plus"></i>&nbsp;Tambah Catatan
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-7 form-inline text-right">
                                    <div class="form-group m-r-15">
                                        <label for="ddlBulan" class="m-r-15 form-control-label">Bulan&nbsp;</label>
                                        <asp:DropDownList runat="server" ID="ddlBulan" Enabled="true" CssClass="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged">
                                            <asp:ListItem Value="01">Januari</asp:ListItem>
                                            <asp:ListItem Value="02">Februari</asp:ListItem>
                                            <asp:ListItem Value="03">Maret</asp:ListItem>
                                            <asp:ListItem Value="04">April</asp:ListItem>
                                            <asp:ListItem Value="05">Mei</asp:ListItem>
                                            <asp:ListItem Value="06">Juni</asp:ListItem>
                                            <asp:ListItem Value="07">Juli</asp:ListItem>
                                            <asp:ListItem Value="08">Agustus</asp:ListItem>
                                            <asp:ListItem Value="09">September</asp:ListItem>
                                            <asp:ListItem Value="10">Oktober</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">Desember</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group m-r-15">
                                        <label for="ddlTahun" class="m-r-15 form-control-label">&nbsp;&nbsp;Tahun&nbsp;</label>
                                        <asp:DropDownList runat="server" ID="ddlTahun" Enabled="true" CssClass="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlTahun_SelectedIndexChanged">
                                            <asp:ListItem Text="2020" Value="2020" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row m-t-20">
                                <div class="col-md-12">
                                    <table class="table" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th style="width: 30px; text-align: center;">No.</th>
                                                <th style="width: 180px;">Tanggal</th>
                                                <th>Kegiatan</th>
                                                <th>Berkas Pendukung</th>
                                                <th style="width: 80px; text-align: center;">Persentase</th>
                                                <th style="width: 140px; text-align: center;"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="lvCatatanHarian" runat="server"
                                                DataKeyNames="id_catatan_harian,kegiatan_yg_dilakukan,persentase_capaian,tgl_pelaksanaan"
                                                OnItemEditing="lvCatatanHarian_ItemEditing"
                                                OnItemDeleting="lvCatatanHarian_ItemDeleting"
                                                OnItemDataBound="lvCatatanHarian_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>
                                                        <td>
                                                            <b><%# getNamaHari(DateTime.Parse(Eval("tgl_pelaksanaan").ToString())) %></b>
                                                        </td>
                                                        <td>
                                                            <%# Eval("kegiatan_yg_dilakukan") %>                                                            
                                                        </td>
                                                        <td><b>Jumlah &nbsp;<%# Eval("jml_berkas") %></b>
                                                            <table>
                                                                <tbody>
                                                                    <asp:ListView ID="lvDaftarBerkas" runat="server"
                                                                        ItemType="simlitekkes.UserControls.Pengusul.BerkasCatatanHarian"
                                                                        DataKeyNames="idBerkas,namaBerkas,tipeBerkas"
                                                                        OnItemEditing="lvDaftarBerkas_ItemEditing">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="width: 30px;">
                                                                                    <%# Container.DataItemIndex + 1%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lbBerkas" runat="server" CommandName="Edit">
                                                                                            <%# Eval ("namaBerkas") %> <i class="far fa-file-pdf"></i>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <b><%# Eval("persentase_capaian") %>&nbsp;%</b>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit"
                                                                CssClass="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete"
                                                                CssClass="btn btn-danger waves-effect waves-light"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <tr>
                                                        <td colspan="5">
                                                            <p class="text-primary text-center">Tidak ada data...</p>
                                                        </td>
                                                    </tr>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <uc:modalKonfirmasi runat="server" ID="modalHapusCatatan" />
    </asp:View>
    <asp:View ID="vCatatanHarian" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header bg-info text-white">
                        <h5>Catatan Harian</h5>
                    </div>
                </div>
                <div class="row mt-2 mb-2">
                    <div class="col-sm-12 text-right">
                        <asp:LinkButton ID="lbKembaliKeDaftarCatatan" runat="server" CssClass="btn btn-primary"
                            OnClick="lbKembaliKeDaftarCatatan_Click">
                                        <i class="fa fa-chevron-left"></i>&nbsp;Kembali
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <h5 class="badge badge-info" style="font-size: large">Catatan Kegiatan</h5>
                    </div>
                    <div class="card-body">
                        <div class="md-card-block p-b-10">
                            <div class="form-group row">
                                <label for="tbTglKegiatan" class="col-xs-2 col-form-label form-control-label">Tanggal Kegiatan</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="tbTglKegiatan" runat="server" CssClass="form-control" type="date" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="tbKegiatan" class="col-xs-2 col-form-label form-control-label">Uraian Kegiatan</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="tbKegiatan" runat="server" TextMode="MultiLine" CssClass="form-control"
                                        MaxLength="500" Rows="4" placeholder="Isikan uraian kegiatan disini" />
                                    <small class="form-text text-muted">Uraian kegiatan maksimal 500 karakter</small>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="tbPersentase" class="col-xs-2 col-form-label form-control-label">Persentase Kegiatan</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="tbPersentase" runat="server" CssClass="form-control" MaxLength="3" placeholder="%" />
                                </div>
                                <div class="col-sm-1">
                                    <p class="form-control-static mb-0">%</p>
                                </div>
                            </div>
                            <div class="text-center">
                                <asp:LinkButton ID="btSimpan" runat="server" class="btn btn-primary waves-effect waves-light text-uppercase"
                                    OnClick="btSimpan_Click">Simpan
                                </asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton ID="btBatal" runat="server" class="btn btn-default waves-effect waves-light text-uppercase"
                                            OnClick="btBatal_Click">Batal
                                        </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <asp:Panel ID="panelAnggaran" runat="server" CssClass="row mt-3" Visible="true">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <h5 class="badge badge-info" style="font-size: large">Penggunaan Anggaran</h5>
                    </div>
                    <div class="card-body">
                        <table class="table" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th style="width: 30px; text-align: center;">No.</th>
                                    <th style="width: 120px; text-align: center;">Tanggal</th>
                                    <th>Pembelanjaan</th>
                                    <th style="width: 160px; text-align: center;">Pengeluaran</th>
                                    <th style="width: 120px; text-align: center;"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvAnggaran" runat="server"
                                    DataKeyNames="id_pengeluaran_penugasan,id_catatan_harian,kd_jenis_pembelanjaan,nama_pembelanjaan,jml_pembelanjaan,no_bukti_pengeluaran,tgl_pembelanjaan"
                                    OnItemEditing="lvAnggaran_ItemEditing"
                                    OnItemDeleting="lvAnggaran_ItemDeleting">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>
                                            <td style="text-align: center;"><%# DateTime.Parse(Eval("tgl_pembelanjaan").ToString()).ToString("yyyy-MM-dd") %></td>
                                            <td>
                                                <b><%# Eval("nama_pembelanjaan") %></b><br />
                                                Jenis : <%# Eval("jenis_pembelanjaan") %><br />
                                                No. Bukti : <%# Eval("no_bukti_pengeluaran") %>
                                            </td>
                                            <td style="text-align: right;"><%# decimal.Parse(Eval("jml_pembelanjaan").ToString()).ToString("N0") %></td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit"
                                                    CssClass="btn btn-info waves-effect waves-light" ToolTip="Edit">
                                                                    <i class="fa fa-edit"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete"
                                                    CssClass="btn btn-danger waves-effect waves-light" ToolTip="Hapus">
                                                                    <i class="fa fa-trash"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="5">
                                                <p class="text-primary text-center">Data Pembelanjaan Anggaran belum ada...</p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </tbody>
                        </table>

                        <hr />

                        <div class="form-group row">
                            <label for="ddlJenis" class="form-control-label col-xs-2 col-form-label">Jenis Pembelanjaan</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlJenis" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="-- Pilih Jenis Pembelanjaan --" Value="-1" />
                                    <asp:ListItem Value="522151">BELANJA BAHAN</asp:ListItem>
                                    <asp:ListItem Value="521219">BELANJA BARANG NON OPERASIONAL LAINNYA</asp:ListItem>
                                    <asp:ListItem Value="521213">HONOR OUTPUT KEGIATAN</asp:ListItem>
                                    <asp:ListItem Value="524119">BELANJA PERJALANAN LAINNYA</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="tbTglPengeluaran" class="form-control-label col-xs-2 col-form-label">Tgl. Pengeluaran</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="tbTglPengeluaran" runat="server" CssClass="form-control" type="date" />
                            </div>
                            <label for="tbNoBukti" class="form-control-label col-xs-2 col-form-label">No. Bukti</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbNoBukti" runat="server" CssClass="form-control" placeholder="Nomor" />
                            </div>
                        </div>

                        <div class="form-group row">
                            <label for="tbPembelanjaan" class="form-control-label col-xs-2 col-form-label">Pembelanjaan</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="tbPembelanjaan" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="tbTotalPengeluaran" class="form-control-label col-xs-2 col-form-label">Total Pengeluaran</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="tbTotalPengeluaran" runat="server" CssClass="form-control"
                                    onkeypress="return CheckNumber(event)" />
                            </div>
                        </div>
                        <div class="text-center">
                            <asp:LinkButton ID="lbSimpanPembelanjaan" runat="server" CssClass="btn btn-primary" Text="Simpan"
                                OnClick="lbSimpanPembelanjaan_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="row mt-3">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <h5 class="badge badge-info" style="font-size: large">Berkas Kegiatan</h5>
                    </div>
                    <div class="card-body">
                        <%--<asp:Panel ID="panelBerkas" runat="server" CssClass="md-card-block p-b-10">--%>
                        <table class="table" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th style="width: 30px; text-align: center;">No.</th>
                                    <th>Keterangan Berkas/Foto</th>
                                    <th style="width: 150px;">Ukuran</th>
                                    <th style="width: 150px; text-align: center;">Tgl. Unggah</th>
                                    <th style="width: 140px; text-align: center;"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvBerkas" runat="server"
                                    DataKeyNames="id_berkas_catatan_harian,nama_berkas,tipe_berkas"
                                    OnItemDeleting="lvBerkas_ItemDeleting"
                                    OnItemEditing="lvBerkas_ItemEditing">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>
                                            <td>
                                                <asp:LinkButton ID="lbUnduh" runat="server" CommandName="Edit">
                                                                <%# Eval("nama_berkas") %>
                                                </asp:LinkButton>
                                            </td>
                                            <td style="text-align: right;"><%# Eval("ukuran_berkas","{0:###,###,###,###,##0}") %>&nbsp;b</td>
                                            <td style="text-align: center;"><%# Eval("tgl_unggah") %></td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton ID="lbHapus" runat="server" CommandName="Delete"
                                                    CssClass="btn btn-danger waves-effect waves-light"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="5">
                                                <p class="text-primary text-center">Tidak Ada Berkas Pendukung...</p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </tbody>
                        </table>
                        <hr />
                        <div class="form-group row">
                            <label for="tbKeteranganBerkas" class="col-xs-2 col-form-label form-control-label">Keterangan Berkas</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="tbKeteranganBerkas" runat="server" CssClass="form-control"
                                    placeholder="Isian Keterangan Berkas" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="tbTglKegiatan" class="col-xs-2 col-form-label form-control-label">Berkas</label>
                            <div class="col-sm-10">
                                <asp:FileUpload ID="fuBerkas" runat="server" CssClass="form-control" />
                                <b style="color:red;">File dalam Format PDF dengan Besar Maksimal 5 MB</b>
                            </div>
                        </div>
                        <div class="text-center">
                            <asp:LinkButton ID="lbUnggah" runat="server" class="btn btn-primary waves-effect waves-light text-uppercase"
                                OnClick="lbUnggah_Click"><i class="fa fa-upload" aria-hidden="true"></i>&nbsp;&nbsp;Unggah</asp:LinkButton>
                        </div>
                        
                        <%--</asp:Panel>--%>
                    </div>
                </div>
            </div>
        </div>

        <script>
            $('.datepicker').datepicker({
                format: "yyyy-mm-dd",
                language: "id",
                orientation: "bottom auto"
            });

            function CheckNumber(e) {
                var charCode = (e.which) ? e.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            };
        </script>

        <uc:modalKonfirmasi runat="server" ID="modalHapusBerkasPlusAnggaran" />
    </asp:View>
</asp:MultiView>

<%--</ContentTemplate>
</asp:UpdatePanel>--%>
