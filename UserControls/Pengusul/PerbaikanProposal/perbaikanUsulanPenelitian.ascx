<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="perbaikanUsulanPenelitian.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.PerbaikanProposal.perbaikanUsulanPenelitian" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="ktPaging" TagPrefix="asp" %>

<!-- Row Starts -->
<!-- Row end -->
<!-- Header content -->
<section class="content-header">
</section>

<!-- Main content -->
<%--<section class="content">--%>
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
                                    <%--<asp:DropDownList ID="ddlTahunPelaksanaan" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlTahun_SelectedIndexChanged">
                                        <asp:ListItem Text="2023" Value="2023" />
                                        <asp:ListItem Text="2022" Value="2022" />
                                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlTahun_SelectedIndexChanged"
                                        CssClass="form-control input-sm">
                                        <asp:ListItem Text="2019" Value="2019" Selected="True" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 p-0">
                            <div class="main-header">
                                <h4 style="color: maroon">DAFTAR USULAN PENELITIAN DIDANAI</h4>
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
                                &nbsp; <b>|</b> <b>h-Index:</b>
                                <asp:Label runat="server" ID="lblHIndex" Text="0" Font-Bold="true" ForeColor="MediumSeaGreen"></asp:Label>
                                &nbsp; <b>ID Sinta:</b>
                                <asp:Label runat="server" ID="lblIdSinta" Text="-" Font-Bold="true" ForeColor="MediumSeaGreen"></asp:Label><br />
                                <b>Alamat Surel:</b>
                                <asp:Label runat="server" ID="lblSurel" Text="-" Font-Bold="true" Font-Italic="true"></asp:Label>
                            </div>
                        </div>
                        <div class="alert alert-light" role="alert" style="background-color: lightgrey;">
                            <%--<div class="col-sm-6">--%>
                            <span style="color: maroon; font-weight: bold; font-size: 18px;">Perbaikan Usulan Penelitian</span>
                            <%--</div>--%>
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
                                        <asp:Label ID="lblThnPelaksanaan" runat="server" Text='<%# Bind("thn_pelaksanaan_kegiatan") %>' ForeColor="Maroon"></asp:Label>
                                                - 
                                        <asp:Label ID="lblThnTerakhir" runat="server" Text='<%# Bind("thn_terakhir") %>' ForeColor="Maroon"></asp:Label></b><br />
                                            Kelompok Makro Riset: 
                                        <asp:Label ID="lblKlmpMakroRiset" runat="server" Text='<%# Bind("makro_riset") %>'></asp:Label><br />
                                            Bidang Fokus: 
                                        <i>
                                            <asp:Label ID="lblBidangFokus" runat="server" Text='<%# Bind("bidang_fokus") %>'></asp:Label></i>
                                            &nbsp;- 
                                        <b>
                                            <asp:Label ID="lblPeranPersonil" runat="server" Text='<%# Bind("peran_personil") %>' BackColor="Yellow"></asp:Label></b><br />
                                            Total Dana Disetujui: 
                                        Rp<asp:Label ID="lblTotalDanaDisetujui" runat="server" Text='<%# Bind("total_dana_disetujui", "{0:0,00}") %>'></asp:Label>
                                            &nbsp; 
                                        (<asp:Label ID="lblLamaKegiatan" runat="server" Text='<%# Bind("lama_kegiatan") %>'></asp:Label>
                                            Tahun)<br />
                                            Status perbaikan:
                                        <asp:Label ID="lblStsPerbaikan" runat="server" Text='<%# Bind("sts_perbaikan") %>' CssClass="label label-danger"></asp:Label>

                                            <%--<br />
                                        <b style="color: blue;">- Luaran Wajib:</b>
                                        <asp:GridView runat="server" ID="gvLuaranWajib" AutoGenerateColumns="false" GridLines="None" 
                                             ShowHeaderWhenEmpty="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Urutan Thn Luaran">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblNobaris" Text='<%# Bind("tahun_luaran") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Jenis Luaran">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblNobaris" ForeColor="Green" Text='<%# Bind("jenis_luaran") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status Target Capaian">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblNobaris" Font-Bold="true" ForeColor="Maroon" Text='<%# Bind("status_target_capaian") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Keterangan">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblNobaris" ForeColor="Maroon" Text='<%# Bind("keterangan") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div style="min-height: 100px; margin: 0 auto; color: brown;">
                                                    <strong>DATA TIDAK DITEMUKAN</strong>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>--%>

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
<%--</section>--%>
