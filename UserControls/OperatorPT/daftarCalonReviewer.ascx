<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="daftarCalonReviewer.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.daftarCalonReviewer" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<!-- Row Starts -->
<div class="row">
    <div class="col-sm-12">
        <div class="main-header">
            <h4>Daftar Calon Reviewer Nasional</h4>
        </div>
    </div>
</div>
<!-- Row end -->
<div class="row">
    <div class="col-md-12">
        <div class="card">
            <div class="card-block">
                <div class="md-card-block">
                    <div class="row pt-2 pl-2">
                        <div class="col-sm-12">
                            <div class="form-inline">
                                <div class="col-sm-6">
                                    <%--Tahun pencalonan: &nbsp;
                                <asp:DropDownList ID="ddlThnPencalonan" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm col-sm-2" OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                    <asp:ListItem Text="2022" Value="2022"/>
                                    <asp:ListItem Text="2021" Value="2021" Selected="True" />
                                </asp:DropDownList>&nbsp;--%>                                    
                                    Status penetapan: &nbsp;
                                <asp:DropDownList ID="ddlStatusPersetujuan" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm col-sm-4" OnSelectedIndexChanged="ddlStatusPersetujuan_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="Belum Ditetapkan"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Disetujui"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Tidak Disetujui"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                                <div class="col-sm-6 text-right">
                                    <span class="badge badge-dark">
                                        <asp:Label ID="lblJmlRecords" runat="server" Text="0" Font-Size="X-Large"
                                            ForeColor="white" Font-Bold="true"></asp:Label>&nbsp;Data
                                    <asp:LinkButton ID="lbRefresh" runat="server" ToolTip="Refresh Data"
                                        OnClick="lbRefresh_Click"><i class="typcn typcn-refresh" style="font-size:xx-large; color:white; text-align:right;"></i>
                                    </asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row pt-2 pl-2">
                        <div class="col-sm-12">
                            <div class="form-inline">
                                <div class="col-sm-6">
                                    Jumlah Baris: &nbsp;
                                <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm col-sm-2" OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="Semua" Value="-1" />
                                </asp:DropDownList>
                                    <%--<asp:TextBox runat="server" ID="tbCariReviewer" placeholder="Pencarian Nama Reviewer"
                                        CssClass="form-control" Width="70%"></asp:TextBox>
                                    <asp:LinkButton ID="lbCari" runat="server" ToolTip="Search Data">
                                                        <i class="fa fa-search" ></i>
                                    </asp:LinkButton>--%>
                                </div>
                                <div class="col-sm-6 text-right">
                                    <asp:LinkButton ID="lbDataBaru" runat="server" CssClass="btn btn-success btn-sm"
                                        OnClick="lbDataBaru_Click">Data Baru     
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvDaftarReviewer" runat="server" GridLines="None"
                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                AllowPaging="true" PageSize="10"
                                CssClass="table table-striped table-hover"
                                DataKeyNames="id_calon_reviewer_penelitian,id_personal"
                                OnRowCommand="gvDaftarReviewer_RowCommand"
                                OnPageIndexChanging="gvDaftarReviewer_PageIndexChanging"
                                OnRowDataBound="gvDaftarReviewer_RowDataBound">
                                <PagerStyle CssClass="pgr" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reviewer">
                                        <ItemTemplate>
                                            NIDN:&nbsp;<asp:Label ID="lblNIDN" runat="server" Text='<%# Bind("nidn") %>'></asp:Label><br />
                                            Nama:&nbsp;<b><asp:Label ID="lblNama" runat="server" Text='<%# Bind("nama_lengkap") %>'></asp:Label>&nbsp;
                                            </b>
                                            <br />
                                            Jabatan fungsional:&nbsp;<asp:Label ID="lblJabfung" runat="server" Text='<%# Bind("jabatan_fungsional") %>'></asp:Label><br />
                                            Kompetensi:
                                            <asp:Label ID="lblKompetensi" runat="server" Text='<%# Bind("kompetensi") %>' Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sertifikat">
                                        <ItemTemplate>
                                            Nomor Sertifikat :
                                            <asp:Label ID="lblNoSertifikat" runat="server" Text='<%# Bind("no_sertifikat") %>'></asp:Label>
                                            <br>
                                            Tanggal akhir berlaku :
                                            <asp:Label ID="lblTglAkhirBerlaku" runat="server" Text='<%# Bind("tgl_akhir_berlaku") %>'></asp:Label>
                                            <br>
                                            tgl_diusulkan :
                                            <asp:Label ID="lblTglDiusulkan" runat="server" Text='<%# Bind("tgl_diusulkan") %>'></asp:Label><br />
                                            Dokumen sertifikat:
                                            <asp:LinkButton ID="lbUnduhSertifikat" runat="server" CommandName="unduh" CommandArgument="<%# Container.DataItemIndex %>"
                                                ForeColor="Red" ToolTip="Unduh sertifikat" Font-Size="24px"><i class="hvr-buzz-out far fa-file-pdf"></i></asp:LinkButton>
                                            &nbsp;&nbsp;<asp:LinkButton ID="lbUnggah" runat="server" CommandName="unggah" CommandArgument="<%# Container.DataItemIndex %>"
                                                ForeColor="DarkOrange" ToolTip="Unggah sertifikat" Font-Size="20px"><i class="fas fa-cloud-upload-alt"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status Penetapan">
                                        <ItemTemplate>
                                            Status Penetapan :
                                            <asp:Label ID="lblStatusPersetujuan" runat="server" Text='<%# Bind("sts_persetujuan") %>'></asp:Label>
                                            <br>
                                            Tanggal penetapan :
                                            <asp:Label ID="lblTglpenetapan" runat="server" Text='<%# Bind("tgl_penetapan") %>'></asp:Label><br />
                                            <asp:LinkButton ID="lbPenetapan" runat="server" CommandName="tetapkan" CommandArgument="<%# Container.DataItemIndex %>" Visible="false"
                                                CssClass="btn btn-outline-success" ToolTip="Ubah persetujuan"><i class="hvr-buzz-out fas fa-check"></i>&nbsp;&nbsp;Edit penetapan</asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEditDataCalon" runat="server" CommandName="ubah" CommandArgument="<%# Container.DataItemIndex %>"
                                                ForeColor="DarkGreen" ToolTip="Edit data calon" Font-Size="24px"><i class="fas fa-edit"></i></asp:LinkButton>

                                            <asp:LinkButton ID="lbHapusDataCalon" runat="server" CommandName="hapus" CommandArgument="<%# Container.DataItemIndex %>"
                                                ForeColor="Red" ToolTip="Hapus data calon" Font-Size="24px"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="min-height: 100px; margin: 0 auto;">
                                        <strong>DATA DOSEN TIDAK DITEMUKAN</strong>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="ReviewerModal" tabindex="-1" role="dialog" aria-labelledby="myModalReviewer">
    <div class="modal-dialog modal-xl" role="document">
        <div class="modal-content">
            <%--<asp:UpdatePanel ID="upModal" runat="server">
                <ContentTemplate>--%>
            <div class="panel panel-primary">
                <div class="panel-header pl-3 pt-3">
                    <h4 class="modal-title" id="myModalReviewer">Data Reviewer</h4>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="row p-20">
                        <div class="col-sm-12 pl-5">
                            <div class="form-group row">
                                <label for="tbNidn" class="col-sm-2 control-label">NIDN:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="tbNidn" placeholder="NIDN"
                                        CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:LinkButton ID="lbCek" runat="server" CssClass="btn btn-primary"
                                        OnClick="lbCek_Click">
                                                    <span class="typcn typcn-input-checked" style="font-size:medium;">&nbsp;Cek</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <hr />
                            <div class="form-group row">
                                <label for="tbNama" class="col-sm-2 control-label">Nama:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbNama" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="tbJabatanFungsional" class="col-sm-2 control-label">Jabatan Fungsional:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbJabatanFungsional" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="tbNomorSertifikat" class="col-sm-2 control-label">No Sertifikat:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbNomorSertifikat" CssClass="form-control" placeholder="Nomor Sertifikat"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="tbTanggalAkhirBerlakuSertifikat" class="col-sm-2 control-label">Tgl Akhir berlaku Sertifikat:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbTanggalAkhirBerlakuSertifikat" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="tbKompetensi" class="col-sm-2 control-label">Kompetensi:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbKompetensi" placeholder="Kompetensi" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="panel-footer pl-3 pb-5 pr-3  text-right">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Batal</button>
                    <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-success"
                        OnClick="lbSimpan_Click">Simpan</asp:LinkButton>
                </div>
            </div>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>
</div>


<div class="modal fade" id="PenetapanReviewerModal" tabindex="-1" role="dialog" aria-labelledby="modalPenetapanReviewer">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <div class="panel panel-primary">
                <div class="panel-header pl-3 pt-3">
                    <h4 class="modal-title" id="modalPenetapanReviewer">Penetapan Reviewer</h4>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="row p-20">
                        <div class="col-sm-12 pl-5">
                            <div class="form-group row">
                                <label for="tbNamaAtPenetapan" class="col-sm-2 control-label">Nama:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbNamaAtPenetapan" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="tbKompetensiAtPenetapan" class="col-sm-2 control-label">Kompetensi:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbKompetensiAtPenetapan" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="ddlStatus" class="col-sm-2 control-label">Status penetapan:</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlPersetujuan" runat="server" CssClass="form-control input-sm">
                                        <asp:ListItem Value="1" Text="Disetujui" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Tidak Disetujui"></asp:ListItem>
                                        <asp:ListItem Value="" Text="Batalkan penetapan"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="panel-footer pl-3 pb-5 pr-3  text-right">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Batal</button>
                    <asp:LinkButton ID="lbSimpanPenetapan" runat="server" CssClass="btn btn-success"
                        OnClick="lbSimpanPenetapan_Click">Simpan</asp:LinkButton>
                </div>
            </div>
            <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>
</div>


<div class="modal fade" id="ModalHapusDataPencalonan" tabindex="-1" role="dialog" aria-labelledby="mdlHapusDataPencalonan">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>--%>
            <div class="panel panel-primary">
                <div class="panel-header pl-3 pt-3">
                    <h4 class="modal-title" id="mdlHapusDataPencalonan">Hapus Data Pencalonan Reviewer ini?</h4>
                </div>
                <hr />
                <div class="panel-body">
                    <div class="row p-20">
                        <div class="col-sm-12 pl-5">
                            <div class="form-group row">
                                <label for="tbNamaAtHapus" class="col-sm-2 control-label">Nama:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbNamaAtHapus" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="tbKompetensiAtHapus" class="col-sm-2 control-label">Kompetensi:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="tbKompetensiAtHapus" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="panel-footer pl-3 pb-5 pr-3  text-right">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
                    <asp:LinkButton ID="lbHapusDataCalonReviewer" runat="server" CssClass="btn btn-danger"
                        OnClick="lbHapusDataCalonReviewer_Click">Hapus</asp:LinkButton>
                </div>
            </div>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>
</div>


<div class="modal fade" id="modalUnggahBerkas" tabindex="-1" role="dialog"
    aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-dialog modal-md" role="document">
        <div class="modal-content">
            <div class="modal-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h5>Unggah Sertifikat Reviewer</h5>
                            </div>
                            <div class="card border border-info">
                                <div class="card-body">
                                    <div>
                                        <%--<iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" style="border: none; width: 100%; height: 150px;"></iframe>--%>

                                        <asp:FileUpload runat="server" ID="fileUploadsertifikat" CssClass="form-control" />
                                        <br />
                                        <asp:LinkButton ID="lbUnggah" runat="server" OnClick="lbUnggah_Click"
                                            CssClass="btn btn-info" ToolTip="Unggah sertifikat">
                                            <i class="fas fa-cloud-upload-alt"></i>&nbsp;Unggah</asp:LinkButton>
                                    </div>
                                    <div style="font-size: 14px; padding: 10px;">
                                        Ekstensi: .pdf .PDF.<br />
                                        Maksimal: 1 MB<br>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="lbSelesaiUnggah" runat="server" CssClass="btn btn-info"
                            data-dismiss="modal">
                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Kembali
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
