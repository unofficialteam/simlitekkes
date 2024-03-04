<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="profilLembaga.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.profilLembaga" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<%--<asp:UpdatePanel ID="upProfile" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upProfile">
            <ProgressTemplate>
                <div class="modal_loading">
                    <div class="center">
                        <i class="fa fa-refresh fa-spin fa-5x"></i>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        <h4>
            <label for="rblLembaga" class="col-sm-12 control-label"><b>Pusat Penelitian</b></label></h4>
        <section class="content-header">
        </section>
        <br />
        <section class="content">
            <div>
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

                <asp:MultiView ID="mvProfile" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vProfile" runat="server">
                        <div class="card">
                            <div class="card-block">
                                <div class="md-card-block">
                                    <div class="form-horizontal">
                                        <div class="card-body">
                                            <h5>Identitas Pusat</h5>
                                            <asp:RadioButtonList ID="rbKDJenisKegiatan" runat="server" RepeatDirection="Horizontal"
                                                CssClass="radio-button-list" RepeatLayout="Flow"
                                                AutoPostBack="true" OnSelectedIndexChanged="rbKDJenisKegiatan_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">Pusat Penelitian</asp:ListItem>
                                                <%--<asp:ListItem Value="2">Pusat Pengabdian Kepada Masyarakat</asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                            <div style="float: right; background: transparent; margin-top: 0; margin-bottom: 0; font-size: 12px; position: absolute; top: 15px; right: 10px; border-radius: 2px; padding: 0 5px;">
                                                <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbEdit_Click">Edit Profile Pusat</asp:LinkButton>
                                            </div>
                                            <hr />
                                            <div class="form-group row">
                                                <label for="tbNoSKPendirian" class="col-sm-2 control-label"><b>No SK Pendirian:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblNoSKPendirian" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbNamaLembaga" class="col-sm-2 control-label"><b>Nama Pusat:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblNamaLembaga" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbAlamatLembaga" class="col-sm-2 control-label"><b>Alamat Pusat:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblAlamatLembaga" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbNoTelepon" class="col-sm-2 control-label"><b>No Telepon:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblNoTelepon" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbNoFax" class="col-sm-2 control-label"><b>No Faksimili:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblNoFax" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbSurel" class="col-sm-2 control-label"><b>Email:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblSurel" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label for="tbURL" class="col-sm-2 control-label"><b>Web Site:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblURL" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <h5>Identitas Pimpinan</h5>
                                            <hr />
                                            <div class="form-horizontal">
                                                <div class="box-body">
                                                    <div class="form-group row">
                                                        <label for="tbNamaJabatan" class="col-sm-2 control-label"><b>Nama Jabatan:</b></label>
                                                        <div class="col-sm-5">
                                                            <asp:Label ID="lblNamaJabatan" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="tbNidn" class="col-sm-2 control-label"><b>NIDN:</b></label>
                                                        <div class="col-sm-5">
                                                            <asp:Label ID="lblNidn" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="tbNama" class="col-sm-2 control-label"><b>Nama:</b></label>
                                                        <div class="col-sm-5">
                                                            <asp:Label ID="lblNama" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="tbJenisKelamin" class="col-sm-2 control-label"><b>Jenis Kelamin:</b></label>
                                                        <div class="col-sm-5">
                                                            <asp:Label ID="lblJenisKelamin" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="tbJenjangPendidikan" class="col-sm-2 control-label"><b>Jenjang Pendidikan:</b></label>
                                                        <div class="col-sm-5">
                                                            <asp:Label ID="lblJenjangPendidikan" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <asp:View ID="vEditProfile" runat="server">
                        <div class="card">
                            <div class="card-block">
                                <div class="md-card-block">
                                    <div class="form-horizontal">
                                        <div class="card-body">
                                            <h5>Identitas Pusat</h5>
                                            <hr />
                                            <div class="form-group row">
                                                <label for="tbNoSKPendirian" class="col-sm-2 control-label"><b>No SK Pendirian:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="tbNoSKPendirian" placeholder="No SK Pendirian" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbNamaLembaga" class="col-sm-2 control-label"><b>Nama Pusat</b></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="tbNamaLembaga" placeholder="Nama Pusat" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbAlamatLembaga" class="col-sm-2 control-label"><b>Alamat Pusat:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="tbAlamatLembaga" placeholder="Alamat Pusat" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbNoTelepon" class="col-sm-2 control-label"><b>No Telepon:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="tbNoTelepon" placeholder="No Telepon" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbNoFax" class="col-sm-2 control-label"><b>No Faksimili:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="tbNoFax" placeholder="No Fax" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label for="tbSurel" class="col-sm-2 control-label"><b>Email:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="tbSurel" placeholder="Email" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label for="tbURL" class="col-sm-2 control-label"><b>Web Site:</b></label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="tbURL" placeholder="Web Site" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <h5>Identitas Pimpinan</h5>
                                            <hr />
                                            <div class="form-horizontal">


                                                <div class="form-group row">
                                                    <label for="tbNamaJabatan" class="col-sm-2 control-label"><b>Nama Jabatan:</b></label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox runat="server" ID="tbNamaJabatan" placeholder="Nama Jabatan" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="tbNidn" class="col-sm-2 control-label"><b>NIDN:</b></label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox runat="server" ID="tbNidn" placeholder="NIDN" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                    <div>
                                                        <asp:LinkButton ID="lbCek" runat="server" CssClass="btn btn-primary" OnClick="lbCek_Click">
                                                                <span class="typcn typcn-input-checked" style="font-size:16px;">&nbsp Cek</span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="lblError" class="col-sm-4 control-label"></label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="tbNama" class="col-sm-2 control-label"><b>Nama&nbsp;:</b></label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox runat="server" ID="tbNama" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="tbJenisKelamin" class="col-sm-2 control-label"><b>Jenis Kelamin&nbsp;:</b></label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox runat="server" ID="tbJenisKelamin" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="tbJenjangPendidikan" class="col-sm-2 control-label"><b>Jenjang Pendidikan&nbsp;:</b></label>
                                                    <div class="col-sm-5">
                                                        <asp:TextBox runat="server" ID="tbJenjangPendidikan" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="clearfix pb-3" style="float: right">
                                                <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-info pull-right" OnClick="lbSimpan_Click">Simpan</asp:LinkButton>
                                                <asp:LinkButton ID="lbBatal" runat="server" CssClass="btn btn-danger" OnClick="lbBatal_Click">Batal</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                </asp:MultiView>

            </div>
        </section>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>
