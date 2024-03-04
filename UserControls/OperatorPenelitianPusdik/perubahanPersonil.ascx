<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="perubahanPersonil.ascx.cs" Inherits="simlitekkes.UserControls.OperatorLLDikti.perubahanPersonil" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>

<div class="box-body">
    <div class="card">
        <div class="card-block">
            <fieldset class="form-group">
                <div class="row">
                    <div class="col-sm-12 pull-left">
                        <asp:Label runat="server" ID="lblJudulHeaderUsulanDidanai" Text="PERUBAHAN PERSONIL DAN JUDUL" Font-Bold="true"
                            Font-Size="Larger"></asp:Label>
                    </div>
                </div>
            </fieldset>
            <asp:MultiView runat="server" ID="mvMain">
                <asp:View runat="server" ID="vUsulanDidanai">
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <div class="col-sm-12 pull-left">
                                        <div class="form-inline pull-left">
                                            Program Kegiatan&nbsp;
                                        <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control input-sm">
                                            <asp:ListItem Text="Penelitian Kompetitif Nasional" Value="2" Selected="True" />
                                        </asp:DropDownList>
                                            &nbsp;&nbsp;Skema:&nbsp;
                                        <asp:DropDownList ID="ddlSkema" runat="server" CssClass="form-control input-sm"
                                            OnSelectedIndexChanged="ddlSkema_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Text="PDUPT" Value="3" Selected="True" />
                                            <asp:ListItem Text="Penelitian Dosen Pemula" Value="7" />
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <div class="col-sm-12 pull-left">
                                        <div class="form-inline pull-left">
                                            Tahun Pelaksanaan&nbsp;
                                        <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                                            CssClass="form-control input-sm"
                                            OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                        </asp:DropDownList>
                                            &nbsp;&nbsp;<label for="ddlJmlBaris">Jumlah Baris:&nbsp;</label>
                                            <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="True"
                                                CssClass="form-control input-sm"
                                                OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                                <asp:ListItem Text="10" Value="10" Selected="True" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                                <asp:ListItem Text="Semua" Value="0" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <div class="col-sm-10 pull-left">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" ID="tbCariJudul" class="form-control" placeholder="Ketik judul kegiatan" aria-describedby="btn-addon">
                                            </asp:TextBox>
                                            <span class="input-group-btn" id="btn-addon">
                                                <asp:LinkButton runat="server" ID="lbCariJudul" class="btn btn-warning addon-btn waves-effect waves-light"
                                                    OnClick="lbCariJudul_Click">Cari
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbExportExcel" Font-Size="Large"
                                                    class="btn btn-success waves-effect waves-light"
                                                    OnClick="lbExportExcel_Click">
                                        <i class="far fa-file-excel "></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 pull-right" style="text-align: right;">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <div class="col-sm-12 table-responsive">
                                        <asp:GridView ID="gvUsulanDidanai" runat="server" GridLines="None"
                                            CssClass="table table-striped table-hover"
                                            DataKeyNames="id_usulan, id_usulan_kegiatan, judul, nidn, nama, nama_institusi,
                                            urutan_thn_usulan_kegiatan, lama_kegiatan, dana_disetujui, catatan, nomor_surat, 
                                            tgl_surat, id_perubahan_judul, judul_baru, catatan_perubahan_judul"
                                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                            OnRowUpdating="gvUsulanDidanai_RowUpdating"
                                            OnRowDataBound="gvUsulanDidanai_RowDataBound"
                                            OnRowCommand="gvUsulanDidanai_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoUsulanDidanai" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ketua Kegiatan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNidn" runat="server" Text='<%# Bind("nidn") %>'></asp:Label><br />
                                                        <asp:Label ID="lblNama" runat="server" Font-Bold="true" Text='<%# Bind("nama") %>'></asp:Label><br />
                                                        <asp:Label ID="lblInstitusi" runat="server" Font-Italic="true" Text='<%# Bind("nama_institusi") %>'></asp:Label><br />
                                                        Tahun ke:&nbsp;
                                                    <asp:Label ID="lblUrutanThn" runat="server" Font-Bold="true" Text='<%# Bind("urutan_thn_usulan_kegiatan") %>'></asp:Label>
                                                        &nbsp;dari&nbsp;
                                                    <asp:Label ID="lblDurasi" runat="server" Font-Bold="true" Text='<%# Bind("lama_kegiatan") %>'></asp:Label>
                                                        &nbsp;tahun<br />
                                                        Rp.&nbsp;<asp:Label ID="lblDanaDisetujui" runat="server" Text='<%# Eval("dana_disetujui", "{0:0,00}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="300px" HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Judul Kegiatan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJudul" runat="server" Font-Bold="true" ForeColor="Blue" Text='<%# Bind("judul") %>'></asp:Label><br />
                                                        Catatan:&nbsp;<asp:Label ID="lblCatatan" runat="server" ForeColor="Green" Text='<%# Bind("catatan") %>'></asp:Label><br />
                                                        No. surat:&nbsp;
                                                    <asp:Label ID="lblNoSurat" runat="server" Text='<%# Bind("nomor_surat") %>'></asp:Label>
                                                        &nbsp;|&nbsp;Tgl.&nbsp;
                                                    <asp:Label ID="lblTglSurat" runat="server" Text='<%# Bind("tgl_surat_indo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Perubahan Personil">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbEdit" runat="server" CommandName="Update" CssClass="btn btn-warning btn-sm"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Font-Size="Larger" ToolTip="Perubahan Personil">
                                                        <i class="fas fa-edit"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Perubahan Judul">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbPerubahanJudul" runat="server" CommandName="perubahanJudul" CssClass="btn btn-warning btn-sm"
                                                            CommandArgument="<%# Container.DataItemIndex %>" Font-Size="Larger" ToolTip="Perubahan Judul">
                                                        <i class="fas fa-edit"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div style="min-height: 100px; margin: 0 auto;">
                                                    <strong>BELUM ADA DATA</strong>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <asp:controlPaging runat="server" ID="pagingUsulanDidanai" OnPageChanging="pagingUsulanDidanai_PageChanging" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </asp:View>
                <asp:View runat="server" ID="vPerubahanPersonil">
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label runat="server" Font-Bold="true" ForeColor="Blue" ID="lblJudulPerubahan" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                Program:&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblProgramPerubahan" Text=""></asp:Label>&nbsp;        
                                Skema:&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblSKemaPerubahan" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10 pull-left" style="text-align: left;">
                                Tahun ke:&nbsp;
                                <asp:Label runat="server" Font-Bold="true" ID="lblTahunKePerubahan" Text=""></asp:Label>
                                &nbsp;dari&nbsp;
                                <asp:Label runat="server" Font-Bold="true" ID="lblDurasiPerubahan" Text=""></asp:Label>
                                &nbsp;tahun
                                &nbsp;|&nbsp;Tahun Pelaksanaan&nbsp;
                                <asp:Label runat="server" ID="lblThnPelaksanaanPerubahan" Text=""></asp:Label>
                                &nbsp;|&nbsp;Rp.&nbsp;
                                <asp:Label runat="server" ID="lblDanaDisetujuiPerubahan" Text=""></asp:Label>
                            </div>
                            <div class="col-sm-2 pull-right" style="text-align: right;">
                                <asp:LinkButton runat="server" ID="lbKembaliPerubahan"
                                    class="btn btn-success waves-effect waves-light" OnClick="lbKembaliPerubahan_Click">
                                            <span class="m-l-10">Kembali</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-lg-2 text-left">
                                <b>Sesuai dg surat nomor</b>
                            </div>
                            <div class="col-lg-6 text-left">
                                <asp:TextBox runat="server" ID="tbNoSurat" CssClass="form-control" Width="90%" Text=""></asp:TextBox>
                            </div>
                            <div class="col-lg-1 text-left">
                                <b>Tanggal</b>
                            </div>
                            <div class="col-lg-2 text-left">
                                <asp:TextBox ID="tbTglSurat" runat="server" class="form-control" Type="date" ToolTip="Tanggal Surat"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-lg-2 text-left">
                                <b>Catatan</b>
                            </div>
                            <div class="col-lg-10 text-left">
                                <asp:TextBox runat="server" ID="tbCatatan" CssClass="form-control max-textarea" TextMode="MultiLine" Rows="3" Width="90%" Text=""></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-sm-12 table-responsive">
                                <div class="row">
                                    <div class="col-lg-8 text-left">
                                        Jml. Personil:&nbsp;<asp:Label ID="lblJmlPersonil" Font-Bold="true" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-lg-4" style="text-align: right;">
                                        <asp:LinkButton runat="server" ID="lbTambahPersonilBaru"
                                            class="btn btn-success waves-effect waves-light" OnClick="lbTambahPersonilBaru_Click">Tambah Personil Baru
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <asp:GridView ID="gvDaftarPersonil" runat="server" GridLines="None"
                                    CssClass="table table-striped table-hover"
                                    DataKeyNames="id_personil, id_personal, id_usulan_kegiatan, 
                                    nidn, nama, nama_institusi, kd_peran_personil, urutan_peran, 
                                    peran, alokasi_waktu, bidang_tugas"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                    OnRowUpdating="gvDaftarPersonil_RowUpdating"
                                    OnRowCommand="gvDaftarPersonil_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoPerubahan" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Personil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNidnPerubahan" runat="server" Text='<%# Bind("nidn") %>'></asp:Label>&nbsp;|&nbsp;
                                                <asp:Label ID="lblNamaPerubahan" runat="server" Font-Bold="true" Text='<%# Bind("nama") %>'></asp:Label><br />
                                                <asp:Label ID="lblInstitusiPerubahan" runat="server" Font-Italic="true" Text='<%# Bind("nama_institusi") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="300px" HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Peran">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPeranPerubahan" runat="server" Text='<%# Bind("peran") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" runat="server" CommandName="Update" CssClass="btn btn-primary btn-sm"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Font-Size="Larger" ToolTip="Edit">Pilih
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbHapus" runat="server" CommandName="Hapus" CssClass="btn btn-danger waves-effect waves-light"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Font-Size="Larger" ToolTip="Hapus Personil"
                                                    Visible='<%# Eval("kd_peran_personil").ToString() != "A" ? true : false %>'>
                                                    <i class="fas fa-trash-alt"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div style="min-height: 100px; margin: 0 auto;">
                                            <strong>DATA TIDAK DITEMUKAN</strong>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-lg-12">
                                PERGANTIAN:&nbsp;<asp:Label ID="lblNamaPeranPerubahan" Font-Bold="true" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="card-header">
                                    <h5 class="card-header-text" style="color: darkgreen;">PERSONIL LAMA</h5>
                                    <%--<div class="f-right">
                                            <asp:LinkButton ID="lbSInkronisasi" runat="server" CssClass="btn btn-success waves-effect waves-light" Visible="false" OnClick="lbSInkronisasi_Click">Sinkronisasi</asp:LinkButton>
                                        </div>--%>
                                </div>
                                <div class="card-block">
                                    <div class="form-group row">
                                        <label for="lblNidnNidkLama" class="col-xs-2 col-form-label form-control-label">NIDN/NIDK</label>
                                        <div class="col-sm-10">
                                            <p class="form-control-static mb-0">
                                                <asp:Label ID="lblNidnNidkLama" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="lblNamaLama" class="col-xs-2 col-form-label form-control-label">Nama</label>
                                        <div class="col-sm-10">
                                            <p class="form-control-static mb-0">
                                                <asp:Label ID="lblNamaLama" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="lblInstitusiLama" class="col-xs-2 col-form-label form-control-label">Institusi</label>
                                        <div class="col-sm-10">
                                            <p class="form-control-static mb-0">
                                                <asp:Label ID="lblInstitusiLama" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="card-header">
                                    <h5 class="card-header-text" style="color: darkgreen;">PERSONIL BARU</h5>
                                </div>
                                <div class="card-block">
                                    <div class="form-group row">
                                        <label for="tbNidnPengganti" class="col-xs-2 col-form-label form-control-label">NIDN/NIDK</label>
                                        <div class="col-sm-6 pull-left">
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ID="tbNidnPengganti" class="form-control" placeholder="NIDK/NIDK Pengganti" aria-describedby="btn-addon1">
                                                </asp:TextBox>
                                                <span class="input-group-btn" id="btn-addon1">
                                                    <asp:LinkButton runat="server" ID="lbCariNidnPengganti" class="btn btn-warning addon-btn waves-effect waves-light"
                                                        OnClick="lbCariNidnPengganti_Click">Cari
                                                    </asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel runat="server" ID="pnlPeranPersonil" Visible="false">
                                        <div class="form-group row">
                                            <label for="ddlPeranPersonil" class="col-xs-2 col-form-label form-control-label">Peran</label>
                                            <div class="col-sm-6 pull-left">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPeranPersonil" runat="server"
                                                        CssClass="form-control input-sm" AutoPostBack="True"
                                                        DataTextField="peran_personil" DataValueField="kd_peran_personil">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="form-group row">
                                        <label for="lblNamaBaru" class="col-xs-2 col-form-label form-control-label">Nama</label>
                                        <div class="col-sm-10">
                                            <p class="form-control-static mb-0">
                                                <asp:Label ID="lblNamaBaru" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="lblInstitusiBaru" class="col-xs-2 col-form-label form-control-label">Institusi</label>
                                        <div class="col-sm-10">
                                            <p class="form-control-static mb-0">
                                                <asp:Label ID="lblInstitusiBaru" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-lg-12 pull-center" style="text-align: center">
                                <asp:LinkButton runat="server" ID="lbSimpan" Visible="false"
                                    class="btn btn-primary waves-effect waves-light" OnClick="lbSimpan_Click">Simpan
                                </asp:LinkButton>
                                <button type="button" runat="server" id="bSimpan" visible="true" class="btn btn-disable disabled">Simpan</button>
                            </div>
                        </div>
                    </fieldset>
                </asp:View>
                <asp:View runat="server" ID="vPerubahanJudul">
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-sm-12">
                                Program:&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblProgramPerubahanJudul" Text=""></asp:Label>&nbsp;        
                                Skema:&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblSkemaPerubahanJudul" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 pull-left" style="text-align: left;">
                                Tahun ke:&nbsp;
                                <asp:Label runat="server" Font-Bold="true" ID="Label5" Text=""></asp:Label>
                                &nbsp;dari&nbsp;
                                <asp:Label runat="server" Font-Bold="true" ID="Label6" Text=""></asp:Label>
                                &nbsp;tahun
                                &nbsp;|&nbsp;Tahun Pelaksanaan&nbsp;
                                <asp:Label runat="server" ID="Label7" Text=""></asp:Label>
                                &nbsp;|&nbsp;Rp.&nbsp;
                                <asp:Label runat="server" ID="Label8" Text=""></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-lg-2 text-left">
                                <b>Judul Lama</b>
                            </div>
                            <div class="col-lg-10 text-left">
                                <asp:Label runat="server" ID="lblJudulLama" Text=""></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-lg-2 text-left">
                                <b>Judul Baru</b>
                            </div>
                            <div class="col-lg-10 text-left">
                                <asp:TextBox runat="server" ID="tbJudulBaru" CssClass="form-control max-textarea" TextMode="MultiLine" Rows="3" Width="90%" Text=""></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-lg-2 text-left">
                                <b>Catatan</b>
                            </div>
                            <div class="col-lg-10 text-left">
                                <asp:TextBox runat="server" ID="tbCatatanPerubahanJudul" CssClass="form-control max-textarea" TextMode="MultiLine" Rows="3" Width="90%" Text=""></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="form-group">
                        <div class="row">
                            <div class="col-lg-12 pull-center" style="text-align: center">
                                <asp:LinkButton runat="server" ID="lbKembaliPerubahanJudul"
                                    class="btn btn-success waves-effect waves-light" OnClick="lbKembaliPerubahanJudul_Click">        
                                    <span class="m-l-10">Kembali</span>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lbSimpanPerubahanJudul"
                                    class="btn btn-primary waves-effect waves-light" OnClick="lbSimpanPerubahanJudul_Click">Simpan
                                </asp:LinkButton>
                            </div>
                        </div>
                    </fieldset>
                </asp:View>

            </asp:MultiView>
        </div>
    </div>
</div>
<div class="modal modal-danger" id="modalKonfirmasiHapus" role="dialog" aria-labelledby="mymodalKonfirmasiHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h5 class="modal-title" id="mymodalKonfirmasiHapus">Hapus Personil</h5>
            </div>
            <div class="modal-body">
                Apakah anda yakin ingin menghapus personil ini?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbModalStsKonfirmasiHapus" runat="server" CssClass="btn btn-success"
                    OnClick="lbModalStsKonfirmasiHapus_Click" OnClientClick="$('#modalKonfirmasiHapus').modal('hide');">
                    <asp:Label ID="Label3" runat="server" Text="Ya"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>
