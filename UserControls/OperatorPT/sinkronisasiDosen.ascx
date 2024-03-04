<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sinkronisasiDosen.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.sinkronisasiDosen" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asp" %>

<style>
    .btn-label-danger {
        cursor: unset;
        box-shadow: 0 2px 6px 0 #dc354580;
        font-size: 14px;
        border-radius: 2px;
        color: #fff;
        background-color: #dc3545;
        border-color: #dc3545;
        text-align: center;
        vertical-align: middle;
        user-select: none;
        border: 1px solid transparent;
        padding: .375rem .75rem;
        line-height: 1.5;
    }
</style>

<div class="container-fluid">
    <div class="row">
        <div class="main-header" style="margin-top: 0px; color: #313233e3;">
            <h4>Sinkronisasi Data Dosen</h4>
        </div>
    </div>
    <!-- awal pilih kategori -->
    <div class="row" style="padding-top: 10px;">
        <div class="col-sm-12">
            <asp:MultiView runat="server" ID="mvDaftarDosen">
                <asp:View runat="server" ID="vDaftarDosen">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="card">
                                    <div class="card-header">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <div class="form-inline" style="text-align: left">
                                                <div class="form-group">
                                                    <label for="tbNIDN" class="form-control-label">NIDN/NIDK&nbsp;</label>
                                                    <asp:TextBox runat="server" ID="tbNIDN" placeholder="Ketik NIDN/NIDK" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <asp:LinkButton ID="lbCEK" runat="server" CssClass="btn btn-info waves-effect waves-light" OnClick="lbCEK_Click">CEK DATA PDDIKTI</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-sm-12" style="margin: 10px 0px;">
                                <div class="d-flex justify-content-between align-items-center">
                                    <div class="form-inline" style="text-align: left;">
                                        <div style="padding-right: 5px; color: #7cab3f;">
                                            Jml. Data:
                                            <asp:Label runat="server" ID="lblJmlDosen" Text="0" CssClass="label" Font-Bold="true" ForeColor="#ff7f50"></asp:Label>
                                        </div>
                                        |
                                        <div class="form-group" style="padding-left: 5px; color: #7cab3f;">
                                            Pencarian data &nbsp;
                                            <asp:TextBox runat="server" ID="tbNama" placeholder="Ketikan nama" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:LinkButton ID="lbCari" runat="server" CssClass="btn btn-info waves-effect waves-light" OnClick="lbCari_Click">Cari</asp:LinkButton>
                                    </div>
                                    <div class="form-inline text-right" style="color: #7cab3f;">
                                        <label for="ddlJmlBarisDosen">Jml. Baris&nbsp;</label>
                                        <asp:DropDownList ID="ddlJmlBarisDosen" runat="server" AutoPostBack="True"
                                            CssClass="form-control input-sm"
                                            OnSelectedIndexChanged="ddlJmlBarisDosen_SelectedIndexChanged">
                                            <asp:ListItem Text="10" Value="10" Selected="True" />
                                            <asp:ListItem Text="50" Value="50" />
                                            <asp:ListItem Text="100" Value="100" />
                                            <asp:ListItem Text="Semua" Value="0" />
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;<asp:LinkButton runat="server" ID="lbExcellDaftarDosen" OnClick="lbExcellDaftarDosen_Click"
                                            ForeColor="Green"> <i class="far fa-file-excel fa-2x"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="card">
                                    <div class="card-block">
                                        <div class="form-group">
                                            <div class="col-sm-12 table-responsive" style="margin-top: 10px;">
                                                <asp:GridView ID="gvDaftarDosen" runat="server" GridLines="None"
                                                    CssClass="table table-striped table-hover"
                                                    DataKeyNames="nidn,nama,id_pdpt,kd_perguruan_tinggi, 
                                                    id_personal, kd_sts_aktif"
                                                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                    OnRowDataBound="gvDaftarDosen_RowDataBound"
                                                    OnRowUpdating="gvDaftarDosen_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNomor" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="30px" />
                                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Institusi">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKodePT" runat="server" Text='<%# Bind("kd_perguruan_tinggi") %>'></asp:Label>
                                                                |
                                                                    &nbsp;<b><asp:Label ID="lblNamaInstitusi" runat="server" Text='<%# Bind("nama_institusi") %>'></asp:Label></b><br />
                                                                Prodi:&nbsp;<asp:Label ID="lblNamaProgramStudi" ForeColor="#7cab3f" runat="server" Text='<%# Bind("nama_program_studi") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Identitas Personal">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <asp:Label ID="lblnamaDosen" runat="server" Text='<%# Bind("nama") %>'></asp:Label></b><br />
                                                                NIDN:
                                                                <asp:Label ID="lblNidn" runat="server" ForeColor="#7cab3f" Text='<%# Bind("nidn") %>'></asp:Label>
                                                                |&nbsp;
                                                                <asp:Label ID="lblJenisKelamin" runat="server" ForeColor="#7cab3f" Text='<%# Bind("jenis_kelamin") %>'></asp:Label><br />
                                                                Jabatan Fungsional:&nbsp;<asp:Label ID="lblJabatanFungsional" runat="server" ForeColor="#7cab3f" Text='<%# Bind("jabatan_fungsional") %>'></asp:Label>
                                                                |&nbsp;
                                                                <asp:Label ID="lblJenjangPendidikan" runat="server" ForeColor="#7cab3f" Text='<%# Bind("jenjang_pendidikan") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <div style="padding-right:5px;">
                                                                    Alamat:&nbsp;<asp:Label ID="lblAlamat" runat="server" ForeColor="#7cab3f" Text='<%# Bind("alamat") %>'></asp:Label><br />
                                                                    No. HP:&nbsp;<asp:Label ID="lblNomorHp" runat="server" ForeColor="#7cab3f" Text='<%# Bind("nomor_hp") %>'></asp:Label>
                                                                    |&nbsp;
                                                                Surel:&nbsp;<asp:Label ID="lblSurel" runat="server" ForeColor="#7cab3f" Text='<%# Bind("surel") %>'></asp:Label><br />
                                                                    Status:&nbsp;<asp:Label ID="lblStatusAktif" runat="server" Width="100px" CssClass="btn-label-danger"
                                                                        Text='<%# Bind("status_aktif") %>' Visible='<%# (Eval("kd_sts_aktif").ToString() == "1") ? false : true %>'></asp:Label>
                                                                    <asp:LinkButton ID="lbStatusAktif" runat="server" Width="100px" CssClass="btn btn-success btn-block"
                                                                        CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" ToolTip="Edit Status Dosen"
                                                                        Visible='<%# (Eval("kd_sts_aktif").ToString() == "1") ? true : false %>'>
                                                                        <i class="fa fa-edit mr-2"></i>
                                                                        <asp:Label runat="server" ID="lblStatusDosen" Text='<%# Bind("status_aktif") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12" style="padding-top: 10px;">
                                <div class="row">
                                    <asp:controlPaging runat="server" ID="pagingDaftarDosen" OnPageChanging="daftarDosen_PageChanging" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View runat="server" ID="vSinkronisasi">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-lg-12 pull-left">
                                <asp:Label runat="server" ID="Label2" Text="Sinkronisasi Data Dosen" Font-Bold="true"
                                    Font-Size="X-Large"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <!-- Textual inputs starts -->
                            <div class="col-lg-6">
                                <div class="card">
                                    <div class="card-header">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <div>
                                                <h5 class="card-header-text">DATA PDDIKTI</h5>
                                            </div>
                                            <div class="text-right">
                                                <asp:LinkButton ID="lbSInkronisasi" runat="server" CssClass="btn btn-success waves-effect waves-light" Visible="false" OnClick="lbSInkronisasi_Click">Sinkronisasi</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-block">
                                        <form>
                                            <div class="form-group row">
                                                <label for="lblNidnPddikti" class="col-sm-3 col-form-label form-control-label">NIDN</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblNidnPddikti" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblNamaPddikti" class="col-sm-3 col-form-label form-control-label">Nama</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblNamaPddikti" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblProdiPddikti" class="col-sm-3 col-form-label form-control-label">Program Studi</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblProdiPddikti" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblJafungPddikti" class="col-sm-3 col-form-label form-control-label">Jabatan Fungsional</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblJafungPddikti" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblJenjangPendPddikti" class="col-sm-3 col-form-label form-control-label">Jenjang Pendidikan</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblJenjangPendPddikti" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblTmpTglLahirPddikti" class="col-sm-3 col-form-label form-control-label">Tempat/Tgl. Lahir</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblTmpTglLahirPddikti" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                            <!-- Textual inputs ends -->

                            <!-- Textual inputs starts -->
                            <div class="col-lg-6">
                                <div class="card">
                                    <div class="card-header">
                                        <h5 class="card-header-text">DATA SIMLITABKES</h5>
                                    </div>
                                    <div class="card-block">
                                        <form>
                                            <div class="form-group row">
                                                <label for="lblNidnSimlitabmas" class="col-sm-3 col-form-label form-control-label">NIDN</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblNidnSimlitabmas" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblNamaSimlitabmas" class="col-sm-3 col-form-label form-control-label">Nama</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblNamaSimlitabmas" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblProdiSimlitabmas" class="col-sm-3 col-form-label form-control-label">Program Studi</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblProdiSimlitabmas" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblJafungSimlitabmas" class="col-sm-3 col-form-label form-control-label">Jabatan Fungsional</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblJafungSimlitabmas" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblJenjangPendSimlitabmas" class="col-sm-3 col-form-label form-control-label">Jenjang Pendidikan</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblJenjangPendSimlitabmas" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="lblTmpTglLahirSimlitabmas" class="col-sm-3 col-form-label form-control-label">Tempat/Tgl. Lahir</label>
                                                <div class="col-sm-9">
                                                    <p class="form-control-plaintext">
                                                        <asp:Label ID="lblTmpTglLahirSimlitabmas" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </form>
                                    </div>

                                </div>
                            </div>
                            <!-- Textual inputs ends -->
                        </div>

                    </div>
                    <div>&nbsp;</div>
                    <div class="card">
                        <div class="card-footer">
                            <div class="d-flex justify-content-between align-items-center">
                                <div>
                                </div>
                                <div class="text-right">
                                    <asp:LinkButton ID="lbKembali" runat="server" CssClass="btn btn-success waves-effect waves-light" OnClick="lbKembali_Click">Kembali</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</div>
<div class="modal modal-primary" id="modalEditStatus" role="dialog" aria-labelledby="myModalEditStatus">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title font-weight-600" id="dangerModalEditStatus">Update Status Dosen</h5>
                <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                Dengan Menekan Tombol Simpan, Dosen Atas Nama
                &nbsp;<asp:Label runat="server" ID="lblNamaDosenEdit" Text="" Font-Bold="true"></asp:Label>&nbsp; Akan di Ubah Menjadi
                <div class="form-group">
                    <asp:DropDownList runat="server" ID="ddlStsDosen" CssClass="select2 form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="modal-footer">
                <asp:LinkButton runat="server" ID="lbBatalUpdate" CssClass="btn btn-danger" OnClick="lbBatalUpdate_Click">Batal</asp:LinkButton>
                <asp:LinkButton ID="lbSimpanStsDosen" runat="server" CssClass="btn btn-primary float-rigth" OnClick="lbSimpanStsDosen_Click" OnClientClick="$('#modalEditStatus').modal('hide');"><i class="fa fa-save mr-2"></i>Simpan</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
