%@ Control Language="C#" AutoEventWireup="true" CodeBehind="formDataPendukungPT.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPenelitianPusdik.formDataPendukungPT" %>
<div class="row">
    <div class="col-md-12">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h6>Tambah Data Pendukung PT</h6>
            </div>
            <div class="card-body row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Peran Pengguna</label>
                        <asp:ListBox ID="listBoxIdPeran" runat="server" CssClass="form-control" SelectionMode="Multiple">
                            <asp:ListItem Value="5">Manajemen PT</asp:ListItem>
                            <asp:ListItem Value="6">Opt. PT - Penelitian</asp:ListItem>
                            <asp:ListItem Value="40">Opt. PT - Pengabdian</asp:ListItem>
                        </asp:ListBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Kota Institusi Asal</label>
                        <asp:DropDownList runat="server" ID="ddlKdKotaInstitusiAsal" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlKdKotaInstitusiAsal_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Nama Institusi Asal</label>
                        <asp:DropDownList runat="server" ID="ddlIdInstitusiAsal" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Gelar Depan</label>
                        <asp:TextBox runat="server" ID="tbGelarDepan" placeholder="Gelar Depan" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Nama</label>
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
                        <label>Nomor KTP</label>
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
                        <label>Tempat Lahir</label>
                        <asp:TextBox ID="tbTempatLahir" runat="server" CssClass="form-control" placeholder="Masukkan Tempat Lahir"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Tanggal Lahir</label>
                        <asp:TextBox runat="server" ID="tbTglLahir" CssClass="form-control" placeholder="Tanggal Lahir" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Alamat</label>
                        <asp:TextBox runat="server" ID="tbAlamat" CssClass="form-control" placeholder="Masukkan Alamat Lengkap" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Kota</label>
                        <asp:DropDownList runat="server" ID="ddlKdKota" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Kode Pos</label>
                        <asp:TextBox runat="server" ID="tbKodePos" CssClass="form-control" placeholder="Kode Pos"></asp:TextBox>
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
                        <label>Nomor HP</label>
                        <asp:TextBox runat="server" ID="tbNomorHp" CssClass="form-control" placeholder="Nomor HP" TextMode="Number" MaxLength="13" minlength="9"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Surat Elektronik/ E-Mail</label>
                        <asp:TextBox runat="server" ID="tbSurel" CssClass="form-control" placeholder="Masukkan Alamat Surat Elektronik / Email" TextMode="Email"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Bidang Keahlian</label>
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
                <div class="float-right">
                    <asp:LinkButton runat="server" ID="lbBatal" CssClass="btn btn-danger mr-2" OnClick="lbBatal_Click">
                    <i class="fas fa-undo mr-2"></i>Batal
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbSimpanData" CssClass="btn btn-success" OnClick="lbSimpanData_Click">
                    <i class="fas fa-save mr-2"></i>Simpan
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</div>