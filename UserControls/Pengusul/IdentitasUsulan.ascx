<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IdentitasUsulan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.IdentitasUsulan" %>
<%@ Register Src="~/UserControls/Pengusul/tkt.ascx" TagPrefix="uc" TagName="tkt" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upIdentitasUsulan" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="tkt" EventName="OnSimpanClick" />
    </Triggers>
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <div class="card mb-4">
                    <div class="card-header">
                        <h5>Identitas Usulan Penelitian </h5>
                    </div>
                    <div class="card-body">
                        <div class="form-group row">
                            <label for="judul" class="col-sm-2">Judul</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="tbJudul" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-inline">
                            <label for="tkt_saat_ini" class="col-sm-2">TKT saat ini</label>
                            <%--<input id="btnaddon2" class="form-control" aria-describedby="btn-addon2" type="text">--%>
                            <asp:TextBox ID="tbLevelTKT" runat="server" CssClass="form-control" disabled></asp:TextBox>

                            <asp:Button ID="btnUkurTKT" runat="server" Text="Ukur" OnClick="btnUkurTKT_Click"
                                CssClass="btn btn-warning shadow-none addon-btn waves-effect waves-light" />

                            <label for="tkt_target" class="col-sm-2">Target Akhir TKT</label>
                            <asp:DropDownList ID="ddlTargetTKT" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Pilih Level" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>


                        <div class="form text-center" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-primary"
                                OnClick="lbSimpan_Click">Simpan</asp:LinkButton>
                        </div>

                    </div>
                </div>
                <%--<div class="panel-footer text-center">
            <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-primary">Simpan</asp:LinkButton>
        </div>--%>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="card mb-4">
                    <div class="card-header">
                        <h5>Pemilihan Skema Penelitian</h5>
                    </div>
                    <div class="card-body">
                        <form>
                            <div class="form-group row">
                                <label for="1" class="col-sm-2">Kategori Penelitian</label>
                                <div class="col-sm-10">
                                    <asp:RadioButtonList ID="rblKategoriPenelitian" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblKategoriPenelitian_SelectedIndexChanged">
                                        <%--<asp:ListItem Text="Kompetitif Nasional" Value="2" class="radio-inline" Selected="True"></asp:ListItem>--%>
                                        <asp:ListItem Text="Desentralisasi" Value="1" class="radio-inline" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Penugasan" Value="6" class="radio-inline"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label for="2" class="col-sm-2">Skema Penelitian</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlSkemaPenelitian" runat="server" CssClass="form-control"
                                        DataTextField="nama_skema" DataValueField="id_skema" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSkemaPenelitian_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Pilih Skema Kegiatan --" Value="-1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </form>


                        <div class="form-group row">
                            <label class="col-sm-2">Standar Biaya Keluaran (SBK)</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlSBK" runat="server" CssClass="form-control"
                                    DataValueField="id_kategori_sbk" DataTextField="kategori_sbk" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSBK_SelectedIndexChanged">
                                    <asp:ListItem Text="-- Pilih SBK --" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="form-group row">
                            <label class="col-sm-2">Bidang Fokus Penelitian</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlBidangFokus" runat="server" CssClass="form-control"
                                    DataValueField="id_bidang_fokus" DataTextField="bidang_fokus" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlBidangFokus_SelectedIndexChanged">
                                    <asp:ListItem Text="-- Pilih Bidang Fokus --" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <hr />
                        <asp:Panel ID="panelTopikUnggulanPT" runat="server" Visible="true">

                            <div class="form-group row">
                                <label for="3" class="col-sm-2">Pilar Transformasi/Bidang Prioritas</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlPilarTransformasi" runat="server" CssClass="form-control"
                                        DataTextField="nama_pilar_transformasi"
                                        DataValueField="id_pilar_transformasi" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlPilarTransformasi_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Pilih Pilar Transformasi --" Value="0" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label for="3" class="col-sm-2"></label><%--Tema Pilar Transformasi--%>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlBidangUnggulanPT" runat="server" CssClass="form-control"
                                        DataTextField="bidang_unggulan_perguruan_tinggi"
                                        DataValueField="id_bidang_unggulan_perguruan_tinggi" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlBidangUnggulanPT_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Pilih Tema Pilar Transformasi --" Value="00000000-0000-0000-0000-000000000000" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="col-sm-2"></label> <%--Topik Pilar Transformasi--%>
                                <div class="col-sm-10 col-xs-12">
                                    <asp:DropDownList ID="ddlTopikUnggulanPT" runat="server" CssClass="form-control"
                                        DataTextField="topik_unggulan_perguruan_tinggi"
                                        DataValueField="id_topik_unggulan_perguruan_tinggi">
                                        <asp:ListItem Text="-- Pilih Topik Pilar Transformasi --" Value="0" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                        <hr />
                        <div class="form-group row">
                            <label class="col-sm-2">Rumpun Ilmu</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlRumpunIlmuLevel1" runat="server" CssClass="form-control"
                                    DataValueField="id_rumpun_ilmu" DataTextField="rumpun_ilmu" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRumpunIlmuLevel1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="col-sm-2"></label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlRumpunIlmuLevel2" runat="server" CssClass="form-control"
                                    DataValueField="id_rumpun_ilmu" DataTextField="rumpun_ilmu" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRumpunIlmuLevel2_SelectedIndexChanged">
                                    <asp:ListItem Text="-- Pilih Rumpun Ilmu Level 2 --" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="col-sm-2"></label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlRumpunIlmuLevel3" runat="server" CssClass="form-control"
                                    DataValueField="id_rumpun_ilmu" DataTextField="rumpun_ilmu">
                                    <asp:ListItem Text="-- Pilih Rumpun Ilmu Level 3 --" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <hr />




                        <asp:Panel ID="panelTopikPenelitian" runat="server" Visible="false">

                            <div class="form-group row">
                                <label class="col-sm-2">Tema Penelitian</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlTemaPenelitian" runat="server" CssClass="form-control"
                                        DataValueField="id_tema" DataTextField="tema" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlTemaPenelitian_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Pilih Tema Penelitian --" Value="-1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="form-group row">
                                <label class="col-sm-2">Topik Penelitian</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlTopikPenelitian" runat="server" CssClass="form-control"
                                        DataValueField="id_topik" DataTextField="topik">
                                        <asp:ListItem Text="-- Pilih Topik Penelitian --" Value="-1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </asp:Panel>

                        <%--<div class="form-group">

                                
                                <label class="col-sm-2 col-form-label form-control-label">Tahun Usulan</label>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlThnUsulan" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="2019" Value="2019" ></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <label class="col-sm-2 col-form-label form-control-label">Tahun Pelaksanaan</label>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlTahunKegiatan" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="2019" Value="2019" ></asp:ListItem>
                                        <asp:ListItem Text="2020" Value="2020" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>--%>

                        <div class="form-group row">
                            <label class="col-sm-2">Lama Kegiatan</label>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlLamaKegiatan" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2">
                                &nbsp;tahun
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalTKT" tabindex="-1" role="dialog"
            aria-labelledby="staticModalLabel" aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Perhitungan TKT</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <uc:tkt runat="server" ID="tkt" />
                    </div>
                    <div class="modal-footer">
                        <%--<asp:LinkButton ID="lbSimpanLevelTKT" runat="server" CssClass="btn btn-primary"
                            OnClick="lbSimpanLevelTKT_Click">
                    <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Simpan
                        </asp:LinkButton>--%>
                        <asp:Panel runat="server" ID="panelCloseModal" Visible="false">

                            <button type="button" class="btn btn-danger" data-dismiss="modal">Selesai</button>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
