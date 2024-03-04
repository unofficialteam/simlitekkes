<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="unggahPresentasiNPoster.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.pendaftaranReviewer.unggahPresentasiNPoster" %>
<%@ Register Src="~/Helper/modalKonfirmasi.ascx" TagPrefix="asc" TagName="modalKonfirmasi" %>

<div class="row">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row" style="padding: 20px;">
                    <div class="col-lg-8" style="margin-bottom: 20px; color: blue; font-weight: bold;">
                    Daftar dokumen presentasi dan poster terbaik
                        </div>
                     <div class="col-lg-4 text-right">
                         
                                        <div class="form-inline pull-right" style="text-align: right; vertical-align: central;">
                                            <asp:LinkButton runat="server" ID="lbKembali" CssClass="btn btn-primary" OnClick="lbKembali_Click">
                                                <i class="fa fa-chevron-left"> Kembali</i></asp:LinkButton>
                                        </div>
                     </div>
                    <div class="col-lg-12" style="margin-top: 20px;">
                    <asp:GridView ID="gvDokumen" runat="server" GridLines="None" Width="96%"
                        CssClass="table table-hover"
                        DataKeyNames="id_dokumen,kd_sts_unggah_dokumen,jenis_dokumen"
                        ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowCommand="gvDokumen_RowCommand" OnRowDeleting="gvDokumen_RowDeleting" OnRowEditing="gvDokumen_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jenis dokumen">
                                <ItemTemplate>
                                        <asp:Label ID="lblTahunke" runat="server" Text='<%# Bind("jenis_dokumen") %>' Font-Bold="true" ForeColor="Maroon"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="220px" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    Nomor SK/sertifikat: <asp:Label ID="lblnamaluaran" runat="server" Font-Bold="true" Text='<%# Bind("no_sertifikat") %>'></asp:Label><br />
                                    Tgl unggah: <asp:Label ID="lbltargetluaran" runat="server" Font-Bold="true" Text='<%# Bind("tgl_unggah_dokumen") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbUnduh" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="edit"  Font-Size="Larger" ToolTip="Unduh" CssClass="btn btn-danger btn-sm"> 
                                        <i class="fa fa-file-pdf-o"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="150" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbHapus" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="delete" CssClass="btn btn-danger btn-sm" Font-Size="Larger" ToolTip="Hapus"> <i class="fa fa-trash"></i></asp:LinkButton>
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


<div style="height: 5px;"></div>

<div class="row">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-body">
                <div class="row" style="padding: 20px;">

                    <div class="col-sm-12" style="margin-bottom: 20px;">
                        <b style="color: blue;">Unggah dokumen penyaji presentasi atau poster terbaik</b><span style="color: red; font-style: italic;">(bila ada)</span>
                    </div>
                    
                    <div class="col-sm-12" style="margin: 15px;">
                        Nomor SK/Sertifikat:
                        <asp:TextBox runat="server" ID="tbNoSertifikat"></asp:TextBox>
                    </div>
                    
                    <div class="col-sm-3">
                        <label for="ddlJenisDokumen">Jenis dokumen: </label>
                         <asp:DropDownList runat="server" ID="ddlJenisDokumen" CssClass="custom-select">
                            <asp:ListItem Text="Presentasi terbaik" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Poster terbaik" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <%--<div class="col-sm-1">
                        <asp:LinkButton runat="server" ID="lbUnduhDokPenyajiTerbaik" Text="" ForeColor="gray" OnClick="lbUnduhPenyajiTerbaik_Click" CssClass="fa fa-file-pdf-o" Font-Size="70px">
                        </asp:LinkButton>
                    </div>--%>
                    <div class="col-sm-7">
                        <asp:FileUpload runat="server" ID="fileUploadDokPenyajiTerbaik" CssClass="form-control" />
                        <div style="font-size: 14px; color: red; font-style: italic;">(Ukuran file maksimal 1MB dengan format PDF)</div>

                    </div>
                    <div class="col-sm-2">
                        <asp:LinkButton runat="server" ID="lbUnggahDokPenyajiTerbaik" CssClass="btn btn-success" Text="Unggah" OnClick="lbUnggahPenyajiTerbaik_Click"></asp:LinkButton>
                    </div>


                    <div style="height: 40px;"></div>



                </div>
            </div>
        </div>
    </div>
</div>


<asc:modalKonfirmasi runat="server" id="KonfirmasiHapus" />
