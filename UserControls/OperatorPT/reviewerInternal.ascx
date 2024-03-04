<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="reviewerInternal.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.reviewerInternal" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<!-- Row Starts -->
<div class="row">
    <div class="col-sm-12">
        <div class="main-header">
            <h4>Reviewer Internal</h4>
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
                                    Status Penilai &nbsp;&nbsp;
                                    <asp:RadioButtonList ID="rblAktif" runat="server" RepeatDirection="Horizontal" CssClass="radio-button-list"
                                        RepeatLayout="Flow"
                                        AutoPostBack="true" OnSelectedIndexChanged="rblAktif_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Selected="True">Aktif</asp:ListItem>
                                        <asp:ListItem Value="0">Tidak Aktif</asp:ListItem>
                                    </asp:RadioButtonList>                                    
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
                        <div class="col-sm-6">
                            <div class="form-inline pull-right">
                            </div>
                        </div>
                    </div>
                    <div class="row pt-3 pb-1">
                        <div class="col-sm-6">
                            <div class="form-inline">
                                <div class="col-sm-12 text-left">
                                    <asp:TextBox runat="server" ID="tbCariReviewer" placeholder="Pencarian Nama Reviewer"
                                        CssClass="form-control" Width="70%"></asp:TextBox>
                                    <asp:LinkButton ID="lbCari" runat="server" ToolTip="Search Data"
                                        OnClick="lbCari_Click">
                                                        <i class="fa fa-search" ></i>
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-inline">
                                <div class="col-sm-12 text-right">
                                    Jumlah Baris: &nbsp;
                                <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="True"
                                    CssClass="form-control input-sm col-sm-2" OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                                    <asp:ListItem Text="10" Value="10" Selected="True" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                    <asp:ListItem Text="Semua" Value="-1" />
                                </asp:DropDownList>&nbsp;
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
                                DataKeyNames="id_personal, id_reviewer, nidn, nama, kd_sts_aktif_reviewer_internal, kd_sts_sertifikasi"
                                OnRowUpdating="gvDaftarReviewer_RowUpdating"
                                OnPageIndexChanging="gvDaftarReviewer_PageIndexChanging"
                                OnRowDataBound="gvDaftarReviewer_RowDataBound">
                                <PagerStyle CssClass="pgr" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%--<%# Eval("no_baris") %>--%>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reviewer">
                                        <ItemTemplate>
                                            NIDN:&nbsp;<asp:Label ID="lblNIDN" runat="server" Text='<%# Bind("nidn") %>'></asp:Label><br />
                                            Nama:&nbsp;<b><asp:Label ID="lblGelarAkademikDepan" runat="server" Text='<%# Bind("gelar_akademik_depan") %>'></asp:Label>&nbsp;
                                            <asp:Label ID="lblNama" runat="server" Text='<%# Bind("nama") %>'></asp:Label>&nbsp;
                                            <asp:Label ID="lblGelarAkademikBelakang" runat="server" Text='<%# Bind("gelar_akademik_belakang") %>'></asp:Label></b><br />
                                            <asp:Label ID="lblStatusSertifikasi" runat="server" Visible="false" CssClass="badge badge-pill badge-info" ForeColor="White">Bersertifikat</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kompetensi">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKompetensi" runat="server" Text='<%# Bind("kompetensi") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kontak">
                                        <ItemTemplate>
                                            Nomor HP :
                                            <asp:Label ID="lblHP" runat="server" Text='<%# Bind("nomor_hp") %>'></asp:Label>
                                            <br>
                                            Nomor Telephone :
                                            <asp:Label ID="lblTelp" runat="server" Text='<%# Bind("nomor_telepon") %>'></asp:Label>
                                            <br>
                                            Surel :
                                            <asp:Label ID="lblSurel" runat="server" Text='<%# Bind("surel") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbStatus" runat="server" CommandName="Update"
                                                CssClass="btn btn-success" ToolTip="Status Penilai"><i class="fa fa-user"></i></asp:LinkButton>
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
            <asp:UpdatePanel ID="upModal" runat="server">
                <contenttemplate>
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
                                    <div class="form-group row">
                                        <label for="ddlStatus" class="col-sm-2 control-label">Status:</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control input-sm">
                                                <asp:ListItem Value="1" Text="Aktif" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Tidak Aktif"></asp:ListItem>
                                            </asp:DropDownList>
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
                                        <label for="tbJenisKelamin" class="col-sm-2 control-label">Jenis Kelamin:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbJenisKelamin" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="tbAlamat" class="col-sm-2 control-label">Alamat:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbAlamat" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="tbNoHP" class="col-sm-2 control-label">Nomor HP:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbNoHP" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="tbSurel" class="col-sm-2 control-label">Surel:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbSurel" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="tbKompetensi" class="col-sm-2 control-label">Kompetensi:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbKompetensi" placeholder="Kompetensi" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="tbJenjangPendidikan" class="col-sm-2 control-label">Jenjang Pendidikan:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="tbJenjangPendidikan" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                </contenttemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
