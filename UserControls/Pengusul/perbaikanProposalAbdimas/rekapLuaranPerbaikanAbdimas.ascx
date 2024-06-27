<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rekapLuaranPerbaikanAbdimas.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.perbaikanProposalAbdimas.rekapLuaranPerbaikanAbdimas" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

<section class="content-header">
</section>
<div class="row">
    <div class="col-sm-12 p-0">
        <div class="main-header">
        </div>
    </div>
</div>

<div class="row">
    <div class="col-md-12">
        <div class="card">
            <div class="card-block">
                <div class="md-card-block">
                    <asp:MultiView runat="server" ID="mvMain">
                        <asp:View runat="server" ID="vDaftarUsulanLuaran">
                            <%--<div class="row">
                                <div class="col-lg-6">
                                    <strong style="font-size: 28px;">Rekap Luaran</strong>
                                    <br />
                                    Tahun Pelaksanaan: 
                                <asp:DropDownList ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                                    CssClass="custom-select"
                                    OnSelectedIndexChanged="ddlThnPelaksanaan_SelectedIndexChanged">
                                </asp:DropDownList>
                                </div>
                                <div class="col-lg-5" style="text-align: right;">
                                </div>
                            </div>--%>
                            <%--<br />--%>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:GridView runat="server" ID="gvLuaranDicapai" AutoGenerateColumns="False" Width="100%"
                                        CssClass="table-striped table-hover" OnRowCommand="gvLuaranDicapai_RowCommand"
                                        DataKeyNames="id_usulan_kegiatan"
                                        CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <%# Eval("no_baris") %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Judul Penelitian">
                                                <ItemTemplate>
                                                    Skema: <span style="color: blue; font-weight: bold;"><%# Eval("nama_skema") %></span>
                                                    <br />
                                                    Judul: <span style="color: maroon; font-weight: bold;"><%# Eval("judul") %></span><br />
                                                    Usulan tahun ke-<span style="color: maroon;"><%# Eval("urutan_thn_usulan_kegiatan") %>dari <%# Eval("lama_kegiatan") %></span>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="350px" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jumlah Luaran Wajib">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbLuaranWajib" runat="server"
                                                            CssClass="btn btn-secondary btn-sm" Font-Size="Larger" Text='<%# Eval("jml_luaran_wajib") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jumlah Luaran Tambahan">
                                                <ItemTemplate>
                                                    <div style="margin-top: 5px;">
                                                        <asp:Label ID="lbLuaranTambahan" runat="server"
                                                            CssClass="btn btn-secondary btn-sm" Font-Size="Larger" Text='<%# Eval("jml_luaran_tambahan") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="color: red; text-align: left;">
                                                Data tidak ditemukan.
                                            </div>
                                        </EmptyDataTemplate>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                    <br />
                                    <div class="card">
                                        <div class="card-body" style="padding: 10px;">
                                            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <tr align="center">
                                                    <th width="650" style="color: brown; text-align: left;">LUARAN WAJIB
                                                    </th>
                                                </tr>
                                                <asp:Repeater ID="rptLuaranWajib" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="trtd" style="text-align: left;">&nbsp;
                    Tahun <%# Eval("tahun_luaran")%>:&nbsp;<%# Eval("jenis_luaran")%>;&nbsp;Karya intelektual: <%# Eval("keterangan")%> ;
                            <span style="background-color: yellow;">(<%# Eval("status_target_capaian")%>)</span>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>

                                            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <tr align="center">
                                                    <th width="650" style="color: brown; text-align: left;">LUARAN TAMBAHAN
                                                    </th>
                                                </tr>
                                                <asp:Repeater ID="rptLuaranTambahan" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="trtd" style="text-align: left;">&nbsp;
                    Tahun <%# Eval("tahun_luaran")%>:&nbsp;<%# Eval("jenis_luaran")%>;&nbsp;Karya intelektual: <%# Eval("keterangan")%>
                                                                <span style="background-color: yellow;">(<%# Eval("status_target_capaian")%>)</span>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</div>
