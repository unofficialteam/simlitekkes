<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tkt.ascx.cs" Inherits="simlitekkes.UserControls.Pengusul.tkt" %>
<asp:UpdatePanel ID="upTKT" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:MultiView ID="mvTKT" runat="server" ActiveViewIndex="0">
            <asp:View ID="vKategori" runat="server">
                <div class="card">
                    <div class="card-header">
                        <h6 class="fs-17 font-weight-600 mb-0">Indikator TKT</h6>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="tbTeknologi" class="font-weight-600">
                                Teknologi yang dikembangkan dalam riset yang akan diukur TKT-nya</label>
                            <asp:TextBox ID="tbTeknologi" runat="server" CssClass="form-control"
                                TextMode="MultiLine" placeholder="Isi Teknologi yang dikembangkan disini">
                            </asp:TextBox>
                        </div>
                        <div class="form-group row">
                            <label for="exampleInputEmail1" class="col-sm-2 font-weight-600">Kategori Indikator TKT</label>
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="rblKategori" runat="server" RepeatLayout="Flow" CssClass="radio-button-list" />
                            </div>
                        </div>
                    </div>
                    <div class="card-footer text-right">
                        <asp:LinkButton ID="lbGetKategori" runat="server" CssClass="btn btn-primary"
                            OnClick="lbGetKategori_Click">Selanjutnya&nbsp;&nbsp;<i class="fas fa-angle-right"></i></asp:LinkButton>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="vIndikator" runat="server">
                <div class="card">
                    <div class="card-header">
                        <h6 class="fs-17 font-weight-600 mb-0">Capaian Indikator TKT</h6>
                    </div>
                    <div class="card-body">
                        <div class="row mb-3">
                            <div class="col-sm-9">
                                <h6 class="font-weight-600">Kategori :&nbsp;<asp:Label ID="lblKategori" runat="server"></asp:Label></h6>
                            </div>
                            <div class="col-sm-3 text-right">
                                <h4>
                                    <span class="badge badge-success">
                                        Level TKT&nbsp;<asp:Label ID="lblLevelTKT" runat="server"></asp:Label>
                                    </span>
                                </h4>
                            </div>
                        </div>
                        <asp:GridView ID="gvIndikator" runat="server" AutoGenerateColumns="false" GridLines="None"
                            CssClass="table table-hover"
                            DataKeyNames="id_kategori,id_level">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Eval("urutan") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30" />
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Indikator">
                                    <ItemTemplate>
                                        <%# Eval("indikator") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPersentase" runat="server" Enabled="true" ClientIDMode="Static" CssClass="form-control select2">
                                            <asp:ListItem Text="0%" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="20%" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="40%" Value="40"></asp:ListItem>
                                            <asp:ListItem Text="60%" Value="60"></asp:ListItem>
                                            <asp:ListItem Text="80%" Value="80"></asp:ListItem>
                                            <asp:ListItem Text="100%" Value="100"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="120" />
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="row mt-10 mb-20">
                            <div class="col-sm-12 text-right mt-10">
                                <asp:LinkButton ID="lbHitungTKT" runat="server" CssClass="btn btn-primary"
                                    OnClick="lbHitungTKT_Click"><i class="fas fa-percentage"></i>&nbsp;&nbsp;Hitung</asp:LinkButton>
                            </div>
                            <div class="col-sm-12 mt-3">
                                <asp:Panel ID="panelInfoLanjutan" runat="server" CssClass="alert alert-success" Visible="false">
                                    Nilai Indikator TKT Anda <strong>
                                        <asp:Label ID="lblNilaiTKTLanjutan" runat="server"></asp:Label></strong>.
                                    Silahkan klik tombol <strong>Selanjutnya</strong> untuk mengisi indikator di 
                                    <strong>Level TKT
                                        <asp:Label ID="lblInfoLevelTKT" runat="server"></asp:Label></strong>
                                </asp:Panel>
                                <asp:Panel ID="panelInfoMaksimal" runat="server" CssClass="alert alert-info" Visible="false">
                                    Nilai Indikator TKT Anda <strong>
                                        <asp:Label ID="lblNilaiTKTMaksimal" runat="server"></asp:Label></strong>.
                                    Level TKT yang dicapai adalah <strong>
                                        <asp:Label ID="lblLevelTKTMaksimal" runat="server"></asp:Label></strong>. 
                                    Silahkan klik tombol <strong>Simpan</strong>.
                                </asp:Panel>
                            </div>
                            <div class="col-sm-12 mt-10 text-right">
                                <asp:LinkButton ID="lbLanjut" runat="server" CssClass="btn btn-primary"
                                    OnClick="lbLanjut_Click">Selanjutnya&nbsp;&nbsp;<i class="fas fa-angle-right"></i></asp:LinkButton>
                                <asp:LinkButton ID="lbSimpan" runat="server" CssClass="btn btn-primary" Visible="false"
                                    OnClick="lbSimpan_Click"><i class="fa fa-save"></i>&nbsp;&nbsp;Simpan</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>
