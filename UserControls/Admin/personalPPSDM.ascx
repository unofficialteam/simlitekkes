<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="personalPPSDM.ascx.cs" Inherits="simlitekkes.UserControls.Admin.personalPPSDM" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>

<asp:MultiView runat="server" ID="mvMain">
    <asp:View runat="server" ID="vDaftarPersonal">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header bg-info text-white">
                        <h6>Personal Pusdik SDM Kesehatan</h6>
                    </div>
                    <div class="card-body row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm" ClientIDMode="Static"
                                    OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="Semua" Value="999999" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="tbPencarian" CssClass="form-control" placeholder="Masukkan Nama"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton runat="server" ID="lbCariData" CssClass="btn btn-block btn-primary"
                                    OnClick="lbCariData_Click"><i class="fa fa-search mr-2"></i>Cari Data</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-4" style="text-align: right;">
                            <div class="clearfix">
                                <div class="float-right">
                                    <asp:LinkButton runat="server" ID="lbTambahData" CssClass="btn btn-success"
                                        OnClick="lbTambahData_Click"><i class="fa fa-plus mr-2"></i>Tambah Data Baru</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <table class="table table-hover table-striped mb-4">
                                    <thead>
                                        <tr>
                                            <th class="text-center" style="width: 40px"><b>No.</b></th>
                                            <th><b>Identitas</b></th>
                                            <th><b>Alamat</b></th>
                                            <th><b>Kontak</b></th>
                                            <th style="width: 200px"><b>Kirim Akun</b></th>
                                            <th class="text-center" style="width: 100px"><b>Jml. Peran</b></th>
                                            <th class="text-center" style="width: 100px"><b>Jml. Unit</b></th>
                                            <th class="text-center" style="width: 140px"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvPersonal" runat="server"
                                            DataKeyNames="id_personal, kd_sts_aktif, nama, gelar_akademik_depan, 
                                            gelar_akademik_belakang, nomor_ktp, kd_jenis_kelamin, tempat_lahir, 
                                            tanggal_lahir, alamat, kd_kota, kd_pos, kd_provinsi, nomor_telepon, 
                                            nomor_hp, surel, website_personal, id_institusi, 
                                            id_kontak_pic_pengguna_personal"
                                            OnItemUpdating="lvPersonal_ItemUpdating"
                                            OnItemDeleting="lvPersonal_ItemDeleting"
                                            OnItemCommand="lvPersonal_ItemCommand"
                                            OnItemDataBound="lvPersonal_ItemDataBound">
                                            <itemtemplate>
                                                <tr>
                                                    <td class="text-center">
                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStsAktif" runat="server" Text='<%# Eval("status_aktif") %>'
                                                            CssClass='<%# (Eval("kd_sts_aktif").ToString() == "1") ? 
                                                    "badge badge-info" : "badge badge-danger" %>' /><br />
                                                        <asp:Label ID="lblNamaLengkap" Font-Bold="true" ForeColor="Blue" runat="server" Text='<%# Eval("nama_lengkap") %>' /><br />
                                                        KTP:&nbsp;<asp:Label ID="lblNoKTP" runat="server" Text='<%# Eval("nomor_ktp") %>' Font-Bold="true" /><br />
                                                        Jenis Kelamin:&nbsp;<asp:Label ID="lblJenisKelamin" runat="server" Text='<%# Eval("jenis_kelamin") %>' Font-Bold="true" /><br />
                                                        Tempat/Tgl Lahir:&nbsp;
                                                        <asp:Label ID="lblTmpTglLahir" runat="server" Text='<%# Eval("tempat_lahir") %>' Font-Bold="true" />
                                                        <asp:Label ID="lblTglLahir" runat="server" Text='<%# Eval("tanggal_lahir_indo") %>' Font-Bold="true" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAlamatLengkap" runat="server" Text='<%# Eval("alamat_lengkap") %>' />
                                                    </td>
                                                    <td>Telepon:&nbsp;<asp:Label ID="lblNoTelepon" runat="server" Text='<%# Eval("nomor_telepon") %>' /><br />
                                                        HP:&nbsp;<asp:Label ID="lblNoHP" runat="server" Text='<%# Eval("nomor_hp") %>' Font-Bold="true" /><br />
                                                        Surel:&nbsp;<asp:Label ID="lblSurel" runat="server" Text='<%# Bind("surel") %>'></asp:Label><br />
                                                        Website Personal:&nbsp;<asp:Label ID="lblWebPersonal" runat="server" Text='<%# Eval("website_personal") %>' Font-Bold="true" />
                                                    </td>
                                                    <td>Jml. Pengiriman:
                                                        <asp:Label ID="lblJmlPengiriman" runat="server" Text='<%# Bind("jml_pengiriman") %>'></asp:Label><br />
                                                        <asp:Label ID="lblWaktuPengirimanTerakhir" runat="server"
                                                            Text='<%# Bind("waktu_pengiriman_terakhir_indo") %>'></asp:Label><br />
                                                        <asp:LinkButton ID="lbKirimAkun" runat="server" CommandName="kirimAkun"
                                                            ToolTip="Kirim Akun Personal"
                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                            CssClass="btn btn-outline-success btn-sm"
                                                            Visible='<%# (Eval("jml_akun").ToString() == "0") ? false : true ||
                                                                (Eval("kd_sts_aktif").ToString() == "1") ? true : false ||
                                                                (Eval("surel").ToString() == "") ? false : true %>'>
                                                            <i class="fas fa-envelope"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td class="text-center">
                                                        <asp:LinkButton ID="lbJmlPeran" runat="server" CommandName="jmlPeran"
                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                            ToolTip="Detail Peran">
                                                            <span class="badge badge-pill badge-info">
                                                                <asp:Label ID="lblJmlPeran" Font-Bold="true" runat="server" Text='<%# Bind("jml_peran") %>'></asp:Label>
                                                            </span>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td class="text-center">
                                                        <asp:LinkButton ID="lbJmlUnitKegiatan" runat="server" CommandName="unitKegiatan"
                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                            ToolTip="Detail Unit">
                                                            <span class="badge badge-pill badge-info">
                                                                <asp:Label ID="lblJmlUnitKegiatan" Font-Bold="true" runat="server" Text='<%# Bind("jml_unit_kegiatan") %>'></asp:Label>
                                                            </span>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Update" ToolTip="Edit"
                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                            CssClass="btn btn-outline-primary btn-sm">
                                                            <i class="fas fa-pencil-alt"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete" ToolTip="Hapus"
                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                            CssClass="btn btn-outline-danger btn-sm">
                                                            <i class="fas fa-trash-alt"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </itemtemplate>
                                            <emptydatatemplate>
                                                <tr>
                                                    <td colspan="8" class="text-center">
                                                        <h5>Belum ada Data</h5>
                                                    </td>
                                                </tr>
                                            </emptydatatemplate>
                                        </asp:ListView>
                                    </tbody>
                                </table>


                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:controlPaging runat="server" ID="paging" OnPageChanging="Paging_PageChanging" />
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vperan">
        <div class="col-lg-12">
            <div class="statbox widget box box-shadow">
                <div class="widget-content widget-content-area bx-top-6">
                    <div class="table-responsive">
                        <table class="table table-hover table-striped mb-2">
                            <thead>
                                <tr>
                                    <th class="text-center" style="width: 40px">No.</th>
                                    <th>Peran</th>
                                    <th>Keterangan</th>
                                    <th class="text-center" style="width: 140px">Status</th>
                                    <th class="text-center" style="width: 145px">Default</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvPeran" runat="server"
                                    DataKeyNames="id_peran, peran_aktif"
                                    OnItemDataBound="lvPeran_ItemDataBound">
                                    <itemtemplate>
                                        <tr>
                                            <td class="text-center"><%# Container.DataItemIndex + 1 %></td>
                                            <td>ID:&nbsp;<asp:Label ID="lblIdPeran" runat="server" Font-Bold="true" Text='<%# Eval("id_peran") %>' /><br />
                                                <asp:Label ID="lblNamaPeran" runat="server" Text='<%# Eval("nama_peran") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblKeterangan" runat="server" Text='<%# Eval("keterangan") %>' />
                                            </td>
                                            <td class="text-center">
                                                <label class="switch s-icons s-outline s-outline-success">
                                                    <asp:CheckBox ID="cbPeranAktif" runat="server"
                                                        OnCheckedChanged="cbPeranAktif_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </label>
                                            </td>
                                            <td class="text-center">
                                                <label class="switch s-icons s-outline s-outline-success">
                                                    <asp:CheckBox ID="cbStsDefault" runat="server"
                                                        OnCheckedChanged="cbStsDefault_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </label>
                                            </td>
                                        </tr>
                                    </itemtemplate>
                                    <edititemtemplate>
                                        <tr>
                                            <td class="text-center" style="padding-right: 1rem; padding-left: 1rem;">
                                                <asp:LinkButton ID="lbUpdatePeran" runat="server" CommandName="Update"
                                                    CssClass="btn btn-outline-success btn-sm">
                                                <i class="fas fa-check"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbCancelPeran" runat="server" CommandName="Cancel"
                                                    CssClass="btn btn-outline-warning btn-sm">
                                                <i class="fas fa-times"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </edititemtemplate>
                                </asp:ListView>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td class="text-center">
                                        <asp:LinkButton ID="lbKembaliPeran" runat="server"
                                            CssClass="btn btn-outline-danger btn-block btn-sm"
                                            OnClick="lbKembaliPeran_Click">
                                        <i class="fas fa-backward"></i> Batal
                                        </asp:LinkButton>
                                    </td>
                                    <td class="text-center">
                                        <asp:LinkButton ID="lbSimpanPeran" runat="server"
                                            CssClass="btn btn-outline-success btn-block btn-sm"
                                            OnClick="lbSimpanPeran_Click">
                                        <i class="fas fa-save"></i> simpan
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vUnitKegiatan">
        <div class="col-lg-12">
            <div class="statbox widget box box-shadow">
                <div class="widget-content widget-content-area bx-top-6">
                    <div class="table-responsive">
                        <table class="table table-hover table-striped mb-2">
                            <thead>
                                <tr>
                                    <th class="text-center" style="width: 40px">No.</th>
                                    <th>Unit</th>
                                    <th class="text-center" style="width: 340px">Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="lvUnitKegiatan" runat="server"
                                    DataKeyNames="id_unit_kegiatan, unit_kegiatan_aktif"
                                    OnItemDataBound="lvUnitKegiatan_ItemDataBound">
                                    <itemtemplate>
                                        <tr>
                                            <td class="text-center"><%# Container.DataItemIndex + 1 %></td>
                                            <td>
                                                <asp:Label ID="lblIdUnitKegiatan" runat="server" Visible="false" Text='<%# Eval("id_unit_kegiatan") %>' />
                                                <asp:Label ID="lblNamaUnit" runat="server" Text='<%# Eval("nama_unit") %>' />
                                            </td>
                                            <td class="text-center">
                                                <label class="switch s-icons s-outline s-outline-success">
                                                    <asp:CheckBox ID="cbStsUnitPengguna" runat="server"
                                                        AutoPostBack="true" />
                                                    <%--OnCheckedChanged="cbStsUnitPengguna_CheckedChanged"--%>
                                                </label>
                                            </td>
                                        </tr>
                                    </itemtemplate>
                                    <edititemtemplate>
                                        <tr>
                                            <td class="text-center" style="padding-right: 1rem; padding-left: 1rem;">
                                                <asp:LinkButton ID="lbUpdateUnitKegiatan" runat="server" CommandName="Update"
                                                    CssClass="btn btn-outline-success btn-sm">
                                                <i class="fas fa-check"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbCancleUnitKegiatan" runat="server" CommandName="Cancel"
                                                    CssClass="btn btn-outline-warning btn-sm">
                                                <i class="fas fa-times"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </edititemtemplate>
                                </asp:ListView>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td></td>
                                    <td class="text-center"></td>
                                    <td class="text-right">
                                        <asp:LinkButton ID="lbKembaliUnitKegiatan" runat="server" Width="100px"
                                            CssClass="btn btn-outline-danger btn-block btn-sm"
                                            OnClick="lbKembaliUnitKegiatan_Click">
                                        <i class="fas fa-backward"></i> Batal
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbSimpanUnitKegiatan" runat="server" Width="100px"
                                            CssClass="btn btn-outline-success btn-block btn-sm"
                                            OnClick="lbSimpanUnitKegiatan_Click">
                                        <i class="fas fa-save"></i> simpan
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </asp:View>
    <asp:View runat="server" ID="vInsupPersonal">
        <div class="col-lg-12">
            <div class="alert alert-info" role="alert">
                Isian dengan tanda * wajib diisi
            </div>
            <form class="form-horizontal">
                <div>
                    <div class="form-group row">
                        <label for="tbNama" class="col-sm-2 text-right control-label col-form-label">
                            Nama Lengkap
                            <asp:Label runat="server" ID="Label0" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-sm-2">
                            <asp:TextBox runat="server" ID="tbGelarDepan" CssClass="form-control" placeholder="Gelar depan"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox runat="server" ID="tbNama" CssClass="form-control" placeholder="Nama"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox runat="server" ID="tbGelarBelakang" CssClass="form-control" placeholder="Gelar belakang"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="ddlJenisKelamin" class="col-sm-2 text-right control-label col-form-label">Jenis Kelamin</label>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlJenisKelamin" runat="server" CssClass="form-control"
                                placeholder="Jenis Kelamin">
                                <asp:ListItem Text="Laki laki" Value="L" Selected="True" />
                                <asp:ListItem Text="Perempuan" Value="P" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbNoKTP" class="col-sm-2 text-right control-label col-form-label">
                            No. KTP
                            <asp:Label runat="server" ID="Label5" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-sm-3">
                            <asp:TextBox runat="server" ID="tbNoKTP" CssClass="form-control" TextMode="Number" placeholder="Diisi dengan angka"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbTempatLahir" class="col-sm-2 text-right control-label col-form-label">Tempat/Tgl. Lahir</label>
                        <div class="col-sm-2">
                            <asp:TextBox runat="server" ID="tbTempatLahir" CssClass="form-control" placeholder="Tempat"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox runat="server" ID="tbTglLahir" Type="date" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbAlamat" class="col-sm-2 text-right control-label col-form-label">Alamat</label>
                        <div class="col-sm-5">
                            <asp:TextBox runat="server" ID="tbAlamat" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="ddlPropinsi" class="col-sm-2 text-right control-label col-form-label">
                            Kabupaten/Kota
                            <asp:Label runat="server" ID="Label2" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlPropinsi" runat="server" CssClass="form-control mb-1" AutoPostBack="true"
                                placeholder="Propinsi" OnSelectedIndexChanged="ddlPropinsi_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlKabKota" runat="server" CssClass="form-control"
                                placeholder="Kapupaten/Kota">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbKodePos" class="col-sm-2 text-right control-label col-form-label">Kode Pos
                            <asp:Label runat="server" ID="Label1" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-sm-2">
                            <asp:TextBox runat="server" ID="tbKodePos" CssClass="form-control" 
                                MaxLength="5" placeholder="Isian harus 5 karakter"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbNoTlp" class="col-sm-2 text-right control-label col-form-label">Kontak</label>
                        <div class="col-sm-2">
                            <asp:TextBox runat="server" ID="tbNoTlp" CssClass="form-control" placeholder="No. Telepon"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox runat="server" ID="tbNoHP" CssClass="form-control" placeholder="No. Handphone"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbSurel" class="col-sm-2 text-right control-label col-form-label">Surel
                            <asp:Label runat="server" ID="Label4" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-sm-2">
                            <asp:TextBox runat="server" ID="tbSurel" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="tbWebsite" class="col-sm-2 text-right control-label col-form-label"><i>Website</i></label>
                        <div class="col-sm-3">
                            <asp:TextBox runat="server" ID="tbWebsite" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="ddlStatusEdit" class="col-sm-2 text-right control-label col-form-label">
                            Status Aktif
                            <asp:Label runat="server" ID="Label3" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlStatusEdit" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Aktif" Value="1" Selected="True" />
                                <asp:ListItem Text="Tidak aktif" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="text-right">
                    <asp:LinkButton ID="lbBatalPersonal" runat="server"
                        CssClass="btn btn-outline-danger btn-sm"
                        OnClick="lbBatalPersonal_Click" Width="100px">
                        <i class="fas fa-backward"></i> Batal
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbSimpanPersonal" runat="server"
                        CssClass="btn btn-outline-success btn-sm"
                        OnClick="lbSimpanPersonal_Click" Width="100px">
                        <i class="fas fa-save"></i>
                        <asp:Label runat="server" ID="lblSimpan" Text="Simpan"></asp:Label>
                    </asp:LinkButton>
                </div>
            </form>
        </div>
    </asp:View>
</asp:MultiView>

<div class="modal modal-danger" id="modalHapus" role="dialog" aria-labelledby="myModalHapus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalHapus">Konfirmasi Hapus</h4>
            </div>
            <div class="modal-body">
                Apakah yakin akan menghapus Penugasan Reviewer
                &nbsp;<asp:Label runat="server" ID="lblNamaHapus" Font-Bold="true" ForeColor="Blue" Text=""></asp:Label>&nbsp;?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                <asp:LinkButton ID="lbHapusDataPersonal" runat="server" CssClass="btn btn-info" OnClick="lbHapusDataPersonal_Click" OnClientClick="$('#modalHapus').modal('hide');">Hapus</asp:LinkButton>
            </div>
        </div>
    </div>
</div>