<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="formIdentitasTendikNonDosen.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.formIdentitasTendikNonDosen" %>
<div class="row">
    <div class="col-md-12">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h6>Tambah Tenaga Kependidikan (Non-Dosen)</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Gelar Depan</label>
                        <asp:TextBox runat="server" ID="tbGelarDepan" placeholder="Gelar Depan" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Nama <span style="color: red">*</span></label>
                        <asp:TextBox runat="server" ID="tbNama" CssClass="form-control" placeholder="Masukkan Nama Tendik"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Gelar Belakang</label>
                        <asp:TextBox runat="server" ID="tbGelarBelakang" CssClass="form-control" placeholder="Gelar Belakang"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Nomor KTP <span style="color: red">*</span></label>
                        <asp:TextBox runat="server" ID="tbNomorKtp" CssClass="form-control" placeholder="Masukkan Nomor KTP" MaxLength="16" minlength="16" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Jenis Kelamin</label>
                        <asp:DropDownList runat="server" ID="ddlKdJenisKelamin" CssClass="select2 form-control">
                            <asp:ListItem Value="L">Laki-Laki</asp:ListItem>
                            <asp:ListItem Value="P">Perempuan</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Tempat Lahir <span style="color: red">*</span></label>
                        <asp:TextBox ID="tbTempatLahir" runat="server" CssClass="form-control" placeholder="Masukkan Tempat Lahir"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Tanggal Lahir <span style="color: red">*</span></label>
                        <asp:TextBox runat="server" ID="tbTglLahir" CssClass="form-control" placeholder="Tanggal Lahir" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Alamat <span style="color: red">*</span></label>
                        <asp:TextBox runat="server" ID="tbAlamat" CssClass="form-control" placeholder="Masukkan Alamat Lengkap" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Kota <span style="color: red">*</span></label>
                        <asp:DropDownList runat="server" ID="ddlKdKota" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Kode Pos</label>
                        <asp:TextBox runat="server" ID="tbKodePos" CssClass="form-control" placeholder="Kode Pos" MaxLength="5"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Nomor Telepon</label>
                        <asp:TextBox runat="server" ID="tbNomorTelepon" CssClass="form-control" placeholder="Nomor Telepon" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Nomor HP <span style="color: red">*</span></label>
                        <asp:TextBox runat="server" ID="tbNomorHp" CssClass="form-control" placeholder="Nomor HP" TextMode="Number" MaxLength="13" minlength="9"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Surat Elektronik/ E-Mail <span style="color: red">*</span></label>
                        <asp:TextBox runat="server" ID="tbSurel" CssClass="form-control" placeholder="Masukkan Alamat Surat Elektronik / Email" TextMode="Email"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Bidang Keahlian <span style="color: red">*</span></label>
                        <asp:TextBox runat="server" ID="tbBidangKeahlian" CssClass="form-control" placeholder="Masukkan Bidang Keahlian"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Website Personal</label>
                        <asp:TextBox runat="server" ID="tbWebsitePersonal" CssClass="form-control" placeholder="Masukkan Alamat Website Personal Tendik" TextMode="Url"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="float-left">
                    <label><small><span style="color: red">*</span> Wajib diisi</small></label>
                </div>
                <div class="float-right">
                    <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger mr-2" OnClick="lbBatal_Click">
                    <i class="fas fa-undo mr-2"></i>Kembali
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbSimpanData" CssClass="btn btn-success" OnClick="lbSimpanData_Click">
                    <i class="fas fa-save mr-2"></i>Simpan
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</div>