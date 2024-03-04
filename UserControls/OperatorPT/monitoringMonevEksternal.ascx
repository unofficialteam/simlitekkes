<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="monitoringMonevEksternal.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.monitoringMonevEksternal" %>
<%@ Register Src="~/Helper/controlPaging.ascx" TagName="controlPaging" TagPrefix="asc" %>

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12" style="margin-top: 0px; color: #313233e3;">
            <h4>Hasil Penilaian Monev Eksternal</h4>
        </div>
        <div class="col-md-12">
            <div class="form-inline p-t-5">
                <div class="form-group">
                    <label class="form-control-label">Tahun Pelaksanaan&nbsp;&nbsp;</label>
                    <asp:DropDownList OnSelectedIndexChanged="ddlTahunPelaksanaan_SelectedIndexChanged" ID="ddlThnPelaksanaan" runat="server" AutoPostBack="True"
                        CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
                &nbsp;&nbsp;
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Cari</span>
                        </div>
                        <asp:TextBox runat="server" ID="tbCari" CssClass="form-control" placeholder="Judul"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:LinkButton runat="server" ID="lbCari" OnClick="lbCari_Click" CssClass="btn btn-outline-info"><i class="fas fa-search"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
                &nbsp;&nbsp;
                <div class="form-group">
                    <div class="form-inline" style="color: #7cab3f; float: right">
                        Jml. Baris:&nbsp;<label for="ddlJmlBaris"></label>
                        <asp:DropDownList ID="ddlJmlBaris" runat="server" AutoPostBack="True"
                            CssClass="form-control input-sm" OnSelectedIndexChanged="ddlJmlBaris_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" Selected="True" />
                            <asp:ListItem Text="50" Value="50" />
                            <asp:ListItem Text="100" Value="100" />
                            <asp:ListItem Text="Semua" Value="0" />
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                <asp:LinkButton runat="server" ID="lbUnduhExcel"
                    ToolTip="Export Excel" ForeColor="Green" OnClick="lbUnduhExcel_Click">
                    <i class="far fa-file-excel fa-2x"></i>
                </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 mt-2">
            <div class="table-responsive">
                <asp:GridView ID="gvData" runat="server" GridLines="None"
                    CssClass="table table-striped table-hover"
                    OnRowDataBound="gvData_RowDataBound"
                    ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pengusul" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblNidn" runat="server" Text='<%# Eval("nidn")%>'></asp:Label>
                                - <b>
                                    <asp:Label ID="lblNamaKetua" runat="server" Text='<%# Eval("nama_ketua")%>'></asp:Label>
                                </b>
                                <br />
                                <span style="font-style: italic; color: darkblue;">
                                    <asp:Label ID="lblNamaInstitusi" runat="server" Text='<%# Eval("nama_institusi") %>'></asp:Label>
                                </span>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Usulan" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <b>
                                    <asp:Label ID="lblNamaSkema" runat="server" Text='<%# Eval("nama_skema")%>'></asp:Label>
                                </b>
                                <br />
                                <span style="color: green;">
                                    <asp:Label ID="lblJudul" runat="server" Text='<%# Eval("judul") %>'></asp:Label>
                                </span>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bidang" HeaderStyle-Width="55%" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                Bidang fokus:&nbsp;<asp:Label ID="lblBidangFokus" runat="server" Text='<%# Bind("bidang_fokus") %>'></asp:Label>
                                <br />
                                Tema:&nbsp;<asp:Label ID="lblTema" runat="server" Text='<%# Bind("tema") %>'></asp:Label>
                                <br />
                                Topik:&nbsp;
                                <asp:Label ID="lblTopik" runat="server" Text='<%# Bind("topik") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Penilaian" HeaderStyle-Width="55%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblNamaReviewer" runat="server" Text='<%# Eval("nama_reviewer")%>'></asp:Label>&nbsp;
                                <b>
                                    <asp:Label ID="lblNilai" runat="server" Text='<%# Eval("total_nilai")%>'></asp:Label>
                                </b>
                                <br />
                                <span style="color: green;">
                                    <asp:Label ID="lblKomentar" runat="server" Text='<%# Eval("komentar") %>'></asp:Label>
                                </span>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <strong>DATA TIDAK DITEMUKAN</strong>
                    </EmptyDataTemplate>
                    <EmptyDataRowStyle CssClass="text-center" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
            <asc:controlPaging runat="server" ID="pagingData" OnPageChanging="pagingData_PageChanging" />
        </div>
    </div>
</div>
