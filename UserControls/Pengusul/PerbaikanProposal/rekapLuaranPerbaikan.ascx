<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rekapLuaranPerbaikan.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.PerbaikanProposal.rekapLuaranPerbaikan" %>

<%@ Register Src="~/UserControls/Pengusul/luaran/buku.ascx" TagName="ktBuku" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran/hakKekayaanIntelektual.ascx" TagName="ktHki" TagPrefix="uc" %>
<%--<%@ Register src="luaran/luaranLain.ascx" tagname="ktlainya" tagprefix="uc" %>--%>
<%@ Register Src="~/UserControls/Pengusul/luaran/luaranLain.ascx" TagName="ktLainya" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran/publikasiJurnal.ascx" TagName="ktPublikasi" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran/publikasiProsiding.ascx" TagName="ktProsiding" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran/paten.ascx" TagName="ktPaten" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Pengusul/luaran/pvt.ascx" TagName="ktPVT" TagPrefix="uc" %>

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
                            
                            <div class="row">
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
                            </div>
                            <br />
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
                                                         CssClass="btn btn-secondary btn-sm" Font-Size="Larger"  Text=<%# Eval("jml_luaran_wajib") %>></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="80px"  />
                                                <ItemStyle HorizontalAlign="Center"  />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jumlah Luaran Tambahan">
                                                <ItemTemplate>                                                    
                                                    <div style="margin-top: 5px;">
                                                    <asp:Label ID="lbLuaranTambahan" runat="server" 
                                                         CssClass="btn btn-secondary btn-sm" Font-Size="Larger"  Text=<%# Eval("jml_luaran_tambahan") %>></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="80px"  />
                                                <ItemStyle HorizontalAlign="Center"  />
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
                <%--<table border="0" cellpadding="-3" cellspacing="0" width="100%">
                    <tr align="center" bgcolor="#cccccc">
                        <th width="650" style="background-color: #ddd; height: 30px; vertical-align: middle; text-align: left;">&nbsp;&nbsp;Luaran dan Target Capaian
                        </th>
                    </tr>
                </table>--%>

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

                        <asp:View runat="server" ID="vKelengkapanLuaran" >
                            <div class="row">
                                <div class="col-lg-6 ">
                                    <asp:Label runat="server" ID="lblKelompokLuaran" Text="Luaran Wajib" Font-Size="24px" Font-Bold="true"></asp:Label>
                                    </div>
                                <div class="col-lg-6 text-right">
                            <asp:LinkButton runat="server" ID="lbKembali" CssClass="btn btn-primary btn-sm" OnClick="lbKembali_Click">
                                                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Kembali
                                            </asp:LinkButton>
                                    </div>
                                </div>

                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:GridView runat="server" ID="gvDetail" AutoGenerateColumns="False" Width="100%"
                                        CssClass="table-striped table-hover" OnRowCommand="gvDetail_RowCommand" 
                                        DataKeyNames="id_luaran_dijanjikan,id_jenis_luaran,sts_berkas,nama_jenis_luaran,id_surat,id_dok,id_manual,surat,dok,manual,sts_didanai" 
                                        OnRowDataBound="gvDetail_RowDataBound"
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
                                            <asp:TemplateField HeaderText="Jenis luaran">
                                                <ItemTemplate>
                                                    <span style="color: black; font-weight: bold;"><%# Eval("nama_jenis_luaran") %></span>                                                   
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Target ">
                                                <ItemTemplate>
                                                    <span style="color: maroon; font-weight: bold;"><%# Eval("nama_target_capaian_luaran") %></span>                                                   
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="keterangan">
                                                <ItemTemplate>  
                                                    <%--<asp:Label runat="server" ID="lblKeterangan" Text='<%# Eval("keterangan") %>'></asp:Label>--%>
                                                    <asp:LinkButton runat="server" ID="lbKeterangan" CommandName="lengkapi" CommandArgument='<%# Container.DataItemIndex %>'>Lengkapi</asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                <ItemStyle HorizontalAlign="Left"  />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status Berkas">
                                                <ItemTemplate>  
                                                    <asp:LinkButton runat="server" ID="lbStatusBerkas" CommandArgument='<%# Container.DataItemIndex %>' Text='<%# Eval("sts_berkas") %>' Visible="false" ></asp:LinkButton> 
                                                    <asp:LinkButton runat="server" ID="lbStatusBerkasSurat" CommandName="unduhBerkasSurat" CommandArgument='<%# Container.DataItemIndex %>' CssClass="fa fa-file-pdf-o btn btn-default" Visible="false"></asp:LinkButton> 
                                                    <asp:LinkButton runat="server" ID="lbStatusBerkasDokumen" CommandName="unduhBerkasDokumen" CommandArgument='<%# Container.DataItemIndex %>' CssClass="fa fa-file-pdf-o btn btn-default" Visible="false"></asp:LinkButton>  
                                                    <asp:LinkButton runat="server" ID="lbStatusBerkasManual" CommandName="unduhBerkasManual" CommandArgument='<%# Container.DataItemIndex %>' CssClass="fa fa-file-pdf-o btn btn-default" Visible="false"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="80px"/>
                                                <ItemStyle HorizontalAlign="Left"  />
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
                                </div>
                            </div>


                        </asp:View>

                        <asp:View runat="server" ID="vIsianLuaran" >

                            <div class="row">
                                <div class="col-lg-6 ">
                                   <%-- <asp:Label runat="server" ID="Label1" Text="Luaran Wajib" Font-Size="24px" Font-Bold="true"></asp:Label>--%>
                                    </div>
                                <div class="col-lg-6 text-right">
                            <asp:LinkButton runat="server" ID="lbKembali2" CssClass="btn btn-primary btn-sm" OnClick="lbKembali2_Click">
                                                            <i class="fa fa-chevron-left"></i>&nbsp;&nbsp;Kembali
                                            </asp:LinkButton>
                                    </div>
                                </div>

                            <br />
                            <asp:MultiView runat="server" ID="mvIsianLuaran" >
                                <asp:View runat="server" ID="vBuku" >
                                    <uc:ktBuku runat="server" ID="ktBuku" />
                                </asp:View>
                                <asp:View runat="server" ID="vHki" >
                                    <uc:ktHki runat="server" ID="ktHki" />
                                </asp:View>
                                <asp:View runat="server" ID="vLuaranLainya" >
                                    <uc:ktLainya runat="server" ID="ktLainya" />
                                </asp:View>
                                <asp:View runat="server" ID="vPublikasi" >
                                    <uc:ktPublikasi runat="server" ID="ktPublikasi" />
                                </asp:View>
                                 <asp:View runat="server" ID="vProsiding" >
                                    <uc:ktProsiding runat="server" ID="ktProsiding" />
                                </asp:View>
                                <asp:View runat="server" ID="vPaten" >
                                    <uc:ktPaten runat="server" ID="ktPaten" />
                                </asp:View>
                                <asp:View runat="server" ID="vPVT" >
                                    <uc:ktPVT runat="server" ID="ktPVT" />
                                </asp:View>
                            </asp:MultiView>

                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</div>
