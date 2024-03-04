<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="riwayatUsulan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.riwayatUsulan" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="ktPaging" TagPrefix="asp" %>

<%@ Register Src="~/UserControls/Pengusul/report/pdfUsulanLengkap.ascx" TagName="pdfUsulanLengkap" TagPrefix="uc" %>

<div class="row">
    <div class="col-sm-12">
        <div class="card">
            <div class="card-header bg-info text-white">
                <h5>Riwayat Usulan</h5>
            </div>
            <div class="card-body">
                <asp:GridView runat="server" ID="gvUsulan" AutoGenerateColumns="false" Width="100%"
                    BorderWidth="0" OnRowDataBound="gvUsulan_RowDataBound"
                    DataKeyNames="id_transaksi_unggah_proposal,kd_sts_unggah_proposal,judul,
                                program_hibah,kd_peran_personil,thn_usulan_kegiatan,thn_pelaksanaan_kegiatan"
                    OnRowCommand="gvUsulan_RowCommand" GridLines="None" CssClass="table" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label CssClass="badge badge-info p-2" runat="server" ID="lblNoBaris" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label runat="server" ID="lblJudul" Text='<%# Eval("judul") %>' CssClass="card-title font-weight-600 mb-2 text-primary" Font-Size="Large"></asp:Label><br />
                                        <p class="text-muted"><%# Eval("nama_skema") %> | <%# Eval("bidang_fokus") %></p>
                                        <hr />
                                        <div class="row">
                                            <div class="col-md-8">
                                                <span><span class="mr-2">Thn Usulan: </span><%# Eval("thn_usulan_kegiatan") %></span> - Pelaksanaan:
                                            <%# Eval("thn_pelaksanaan_kegiatan") %><br />
                                                <span class="progress-label text-muted">Tahun Ke <b class="badge badge-success text-white"><%# Eval("urutan_thn_usulan_kegiatan") %></b> dari <b class="badge badge-success text-white"><%# Eval("lama_kegiatan") %></b> Tahun Kegiatan</span>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:LinkButton ID="lbUnduhProposal" runat="server" CssClass="btn btn-success float-right" CommandName="unduhProposal" CommandArgument='<%# Eval("id_usulan_kegiatan") %>'>
                                                        <i class="fas fa-file-pdf mr-2"></i>Unduh Proposal</asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:Label runat="server" ID="lblUsulanPersonil" Text='<%# Eval("personil") %>'></asp:Label>
                                            </div>
                                            <div class="col-md-8" style="margin-top:15px;">
                                                <asp:Label ID="lblStatusUsulan" runat="server" Text='<%# Eval("status_usulan") %>'></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Panel ID="panelInfoKosong" runat="server" CssClass="callout callout-danger" Visible="true">
                            <p>
                                - Belum ada usulan didaftarkan <i class="fa fa-frown-o"></i>
                            </p>
                        </asp:Panel>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <div class="card-footer">
                <div class="row">
                    <div class="col-sm-5">
                        <asp:ktPaging runat="server" ID="ktPagging" OnPageChanging="Paging_PageChanging" />
                    </div>
                    <div class="col-sm-7" style="text-align: right;">
                        Total&nbsp;<asp:Label ID="lblJmlRecords" runat="server" Text="0"></asp:Label>&nbsp;Usulan   
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div>
    <uc:pdfUsulanLengkap runat="server" ID="pdfUsulanLengkap" Visible="false" />
</div>

