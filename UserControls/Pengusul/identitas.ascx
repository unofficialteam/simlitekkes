<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="identitas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.identitas" %>
<asp:MultiView ID="mvIdentitas" runat="server" ActiveViewIndex="0">
    <asp:View ID="vDaftar" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="fs-17 font-weight-600 mb-0">IDENTITAS PERSONAL</h6>
                    </div>
                    <div class="text-right">
                        <div class="actions">
                            <asp:LinkButton ID="lbEdit" runat="server" CssClass="action-item" ToolTip="Edit Identitas Personal" OnClick="lbEdit_click">
                        <i class="fas fa-edit"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-4 mr-2 text-center">
                        <asp:LinkButton ID="lbEditFoto" runat="server" ToolTip="Edit Foto Profil" OnClick="lbEditFoto_click">
                            <asp:Image CssClass="img-thumbnail rounded-circle" runat="server" ID="imgProfile" ImageUrl="~/assets/dist/img/avatar-1.jpg" />
                        </asp:LinkButton>
                        <hr />
                        <asp:Label ID="lblNidn" runat="server" Text="NIDN. 142643415483" CssClass="text-info font-weight-bold"></asp:Label>
                    </div>
                    <div class="col-8 row">
                        <div class="col-6">
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Intitusi</h6>
                                <asp:Label ID="lblInstitusi" runat="server" Text="STIE Mandala" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2" runat="server" id="prodi_area">
                                <h6 class="fs-15 text-muted mb-0">Program Studi</h6>
                                <asp:Label ID="lblProdi" runat="server" Text="Bisnis Digital" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2" runat="server" id="jenjang_pendidikan_area">
                                <h6 class="fs-15 text-muted mb-0">Jenjang Pendidikan</h6>
                                <asp:Label ID="lblJenjangPendidikan" runat="server" Text="S2" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2" runat="server" id="jabatan_akademik_area">
                                <h6 class="fs-15 text-muted mb-0">Jabatan Akademik</h6>
                                <asp:Label ID="lblJabatanAkademik" runat="server" Text="Lektor Kepala" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Alamat</h6>
                                <asp:Label ID="lblAlamat" runat="server" Text="Jl. Bondoyudo, No.51 RT 1/ RW 23, Patrang - Jember" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Tempat/Tanggal Lahir </h6>
                                <asp:Label ID="lblTempat" runat="server" Text="Lumajang" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                                /
                        <asp:Label ID="lblTglLahir" runat="server" Text="08 Februari 1995" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Nomor KTP</h6>
                                <asp:Label ID="lblNoKtp" runat="server" Text="330121641643914" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Nomor Telepon</h6>
                                <asp:Label ID="lblNoTelepon" runat="server" Text="-" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Nomor HP</h6>
                                <asp:Label ID="lblNoHp" runat="server" Text="089767876765" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Alamat Surel/Email</h6>
                                <asp:Label ID="lblSurel" runat="server" Text="email@notprovided.com" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                            <div class="flex-fill ml-3 mb-2">
                                <h6 class="fs-15 text-muted mb-0">Website Personal</h6>
                                <%--<asp:LinkButton runat="server" ID="lbWebPersonal" OnClick="lbWebPersonal_Click"></asp:LinkButton>--%>
                                <asp:Label ID="lblWebPersonal" runat="server" Text="" CssClass="font-weight-bold" Font-Size="Small"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modalUpdateFoto" tabindex="-1" role="dialog" aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header">
                                        <%--<h4><i class="fa fa-user fa-lg"></i>&nbsp;&nbsp;Ubah foto profile</h4>--%>
                                        <h4><i class="fa fa-user fa-lg"></i>&nbsp;&nbsp;Ubah foto profile</h4>
                                    </div>
                                    <div class="card-body">
                                        <div>
                                            <iframe src="../../Helper/unggahFile.aspx" id="iframeUpload" style="border: none; width: 100%; height: 150px;"></iframe>
                                        </div>
                                        <label style="font-size: 14px; padding: 10px;">
                                            Ekstensi: JPG;JPEG;PNG.<br />
                                            Maksimal: 100 KByte.
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-secondary" data-dismiss="modal">
                                    <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Selesai
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-xl-8">
            <table class="table m-0">
                <tbody>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlUnggah" runat="server" Visible="false">
                                <div class="form-group row">
                                    <label for="file" class="col-md-2 col-form-label form-control-label">File input</label>
                                    <div class="col-md-9">
                                        <label for="file" class="custom-file">
                                            <input type="file" id="file" class="custom-file-input">
                                            <span class="custom-file-control"></span>
                                        </label>
                                        &nbsp;&nbsp;
                                    </div>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:View>
    <asp:View ID="vEdit" runat="server">
        <div class="card mb-4">
            <div class="card-header">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h6 class="fs-17 font-weight-600 mb-0">EDIT IDENTITAS PERSONAL</h6>
                    </div>
                    <div class="text-right">
                        <div class="actions">
                            <asp:LinkButton ID="lbClose" runat="server" CssClass="action-item" ToolTip="Tutup Form Edit" OnClick="lbClose_click">
                        <i class="fas fa-window-close"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="card-body">
                    <div class="form-group row">
                        <label for="example-search-input" class="col-sm-3 col-form-label font-weight-600 text-left">Nomor KTP</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="tbNomorKtp" runat="server" class="form-control" placeholder="Nomor KTP"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-search-input" class="col-sm-3 col-form-label font-weight-600 text-left">Alamat</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="tbAlamat" runat="server" class="form-control" Rows="3" TextMode="MultiLine" placeholder="Alamat"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-search-input" class="col-sm-3 col-form-label font-weight-600 text-left">Tempat/Tanggal Lahir</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="tbTempatLahir" runat="server" class="form-control" placeholder="Tempat Lahir"></asp:TextBox>
                        </div>
                        <div class="col-sm-5">
                            <asp:TextBox ID="tbTglLahir" runat="server" class="form-control" Type="date" ToolTip="Tanggal"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-email-input" class="col-sm-3 col-form-label font-weight-600 text-left">Surel/Email</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="tbAlamatSurel" runat="server" class="form-control" placeholder="Alamat Surel"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-url-input" class="col-sm-3 col-form-label font-weight-600 text-left">Nomor Telepon</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="tbNoTelepon" runat="server" class="form-control" placeholder="No. Telepon"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-url-input" class="col-sm-3 col-form-label font-weight-600 text-left">Nomor HP</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="tbNoHp" runat="server" class="form-control" placeholder="No. HP"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-url-input" class="col-sm-3 col-form-label font-weight-600 text-left">Website Personal</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="tbWebsitePersonal" runat="server" class="form-control" placeholder="Website Personal"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix" style="float: right">
                        <asp:LinkButton ID="lbSimpan" runat="server" Text="Simpan" class="btn btn-primary mr-2" OnClick="lbSimpan_click"></asp:LinkButton>
                        <asp:LinkButton ID="lbBatal" runat="server" Text="Batal" class="btn btn-danger" OnClick="lbBatal_click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>