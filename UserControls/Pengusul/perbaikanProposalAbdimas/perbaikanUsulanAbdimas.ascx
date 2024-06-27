<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="perbaikanUsulanAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.perbaikanProposalAbdimas.perbaikanUsulanAbdimas" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="ktPaging" TagPrefix="asp" %>

<section class="content-header">
</section>
<div class="container-fluid">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-block">
                    <div class="md-card-block">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-inline pull-right" style="text-align: right;">
                                    <b>Tahun Pelaksanaan:</b>
                                    <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged"
                                        CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 p-0">
                            <div class="main-header">
                                <h4 style="color: maroon">DAFTAR USULAN PENGABDIAN KEPADA MASYARAKAT DIDANAI</h4>
                                <br />
                                <br />
                                <b>NIDN/NIDK:</b>
                                <asp:Label runat="server" ID="lblNIDN" Text="- " Font-Bold="true" ForeColor="MediumSeaGreen"></asp:Label><br />
                                <b>Perguruan Tinggi:</b>
                                <asp:Label runat="server" ID="lblInstitusi" Text="-" Font-Bold="true" ForeColor="MediumSeaGreen"></asp:Label>
                                &nbsp; <b>|</b> <b>Program Studi:</b>
                                <asp:Label runat="server" ID="lblProgramStudi" Text="-" Font-Bold="true" ForeColor="MediumSeaGreen"></asp:Label><br />
                                <b>Kualifikasi:</b>
                                <asp:Label runat="server" ID="lblPendidikan" Text="-" Font-Bold="true" ForeColor="MediumSeaGreen"></asp:Label>
                                &nbsp; <b>Alamat Surel:</b>
                                <asp:Label runat="server" ID="lblSurel" Text="-" Font-Bold="true" Font-Italic="true"></asp:Label>
                            </div>
                        </div>
                        <div class="alert alert-light" role="alert" style="background-color: lightgrey;">
                            <span style="color: maroon; font-weight: bold; font-size: 18px;">Perbaikan Usulan Pengabdian kepada Masyarakat</span>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="gvUsulanPerbaikan" runat="server" GridLines="None" ShowHeader="false" ShowHeaderWhenEmpty="true"
                                AutoGenerateColumns="False" OnRowUpdating="gvUsulanPerbaikan_RowUpdating"
                                OnRowDataBound="gvUsulanPerbaikan_RowDataBound"
                                OnRowCommand="gvUsulanPerbaikan_RowCommand"
                                DataKeyNames="id_usulan,id_usulan_kegiatan,id_skema,id_transaksi_kegiatan
                                            ,thn_pelaksanaan_kegiatan,lama_kegiatan,urutan_thn_usulan_kegiatan
                                             ,nama_skema,level_tkt,thn_pertama_usulan,judul,sts_perbaikan">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <b style="color: blue; font-size: large;">
                                                <asp:Label ID="lblJudul" runat="server" Text='<%# Bind("judul") %>'></asp:Label></b>&nbsp;&nbsp;
                                            <asp:LinkButton ID="lbUbah" runat="server" CommandName="update" ForeColor="white" Font-Bold="true" CssClass="btn btn-primary"
                                                CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Perbaiki"><i> perbaiki</i>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="lbBatalkan" runat="server" CommandName="batalkan" ForeColor="White" Font-Bold="true" CssClass="btn btn-danger"
                                                CommandArgument="<%# Container.DataItemIndex %>" Visible="false" ToolTip="Batalkan"><i> Batalkan</i>
                                            </asp:LinkButton>
                                            <br />
                                            <b>
                                                <asp:Label ID="lblSkema" runat="server" Text='<%# Bind("nama_skema") %>' ForeColor="MediumSeaGreen"></asp:Label></b>
                                            &nbsp; <b style="color: maroon;">Thn Usulan: 
                                        <asp:Label ID="lblThnUsulan" runat="server" Text='<%# Bind("thn_pertama_usulan") %>' ForeColor="Maroon"></asp:Label>
                                                &nbsp; | Thn Pelaksanaan: 
                                        <asp:Label ID="lblThnPelaksanaan" runat="server" Text='<%# Bind("thn_pelaksanaan_kegiatan") %>' ForeColor="Maroon"></asp:Label><br />
                                                <b>
                                                    <asp:Label ID="lblPeranPersonil" runat="server" Text='<%# Bind("peran_personil") %>' BackColor="Yellow"></asp:Label></b>
                                                &nbsp;Total Dana Disetujui: 
                                        Rp<asp:Label ID="lblTotalDanaDisetujui" runat="server" Text='<%# Bind("total_dana_disetujui", "{0:0,00}") %>'></asp:Label><br />
                                                Status perbaikan:
                                        <asp:Label ID="lblStsPerbaikan" runat="server" Text='<%# Bind("sts_perbaikan") %>' CssClass="label label-danger"></asp:Label>
                                                <hr />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbUnduhBerkas" runat="server" CommandName="unduhDokumen" Font-Size="70px"
                                                CommandArgument="<%# Container.DataItemIndex %>" Visible="true" ToolTip="Unduh Berkas"
                                                CssClass="hvr-buzz-out far fa-file-pdf" ForeColor="Red">
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="150" />
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
    </div>
</div>
