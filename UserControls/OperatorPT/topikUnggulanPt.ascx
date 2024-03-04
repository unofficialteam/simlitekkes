<%@ Control Language="C#" CodeBehind="topikUnggulanPt.ascx.cs" Inherits="simlitekkes.UserControls.OperatorPT.topikUnggulanPt" %>

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upTopikUnggulanPt" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upTopikUnggulanPt">
            <ProgressTemplate>
                <div class="modal_loading">
                    <div class="center">
                        <i class="fa fa-refresh fa-spin fa-5x"></i>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <section class="content-header">
            <h1>
                <asp:Label ID="lblJudulPage" runat="server" Text="Topik Unggulan PT"></asp:Label></h1>
            <div style="float: right; background: transparent; margin-top: 0; margin-bottom: 0; font-size: 12px; position: absolute; top: 15px; right: 10px; border-radius: 2px; padding: 0 5px;">
                <asp:LinkButton ID="lbTambahTopikUnggulan" runat="server" CssClass="btn btn-success btn-sm" Visible="false" OnClick="lbTambahTopikUnggulan_Click">Tambah Topik Unggulan PT</asp:LinkButton>
            </div>
        </section>

        <section class="content">
            <div>
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                <asp:MultiView ID="mvTopikUnggulanPt" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vDaftarTopikUnggulanPt" runat="server">
                        <div class="box">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvBidangUnggulan" runat="server" GridLines="None" CssClass="table table-striped table-hover"
                                            DataKeyNames="id_bidang_unggulan_perguruan_tinggi,kode_status_aktif"
                                            ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                            OnRowDataBound="gvBidangUnggulan_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbnNomor" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bidang Unggulan">
                                                    <ItemTemplate>
                                                        <span style="color: #229922;">
                                                            <asp:Label ID="lblThnPenetapan" runat="server" Text='<%# Bind("tahun_penetapan") %>'></asp:Label>
                                                            -&nbsp;
                                                            <asp:Label ID="lblStsAktifBidang" runat="server" Text='<%# Bind("kode_status_aktif") %>'></asp:Label>
                                                        </span>
                                                        <br />
                                                        <asp:Label ID="lblBidangUnggulanPt" ForeColor="Blue" Font-Bold="true" runat="server" Text='<%# Bind("bidang_unggulan_perguruan_tinggi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Topik Unggulan">
                                                    <ItemTemplate>
                                                        <asp:GridView ID="gvTopikUnggulan" runat="server" GridLines="None" CssClass="table table-striped table-hover"
                                                            DataKeyNames="id_topik_unggulan_perguruan_tinggi, topik_unggulan_perguruan_tinggi, kode_status_aktif, id_bidang_unggulan_perguruan_tinggi"
                                                            ShowHeader="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                            OnRowUpdating="gvTopikUnggulan_RowUpdating" OnRowDataBound="gvTopikUnggulan_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Bidang Unggulan">
                                                                    <ItemTemplate>
                                                                        -&nbsp;<asp:Label ID="lblTopikUnggulanPt" runat="server" Text='<%# Bind("topik_unggulan_perguruan_tinggi") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bidang Unggulan">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStsAktifTopik" runat="server" Text='<%# Bind("status_aktif_topik") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="70px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Update">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbEditTopik" runat="server" CommandName="Update" CssClass="btn btn-primary" Visible="false" ToolTip="Edit Data">
                                                                            <%--CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'>--%> <%--CommandArgument='<%# Container.DataItemIndex %>'>--%>
                                                                            <i class="fa fa-edit"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <div style="min-height: 100px; margin: 0 auto;">
                                                                    <strong>DATA TIDAK DITEMUKAN</strong>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
                    </asp:View>
                </asp:MultiView>

                <div class="modal fade" id="modalInsupTopik" role="dialog" aria-labelledby="myModalInsupTopik" 
                    aria-hidden="true" data-backdrop="false" style="background-color: rgba(0, 0, 0, 0.5);">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">

                                <h4 style="text-align: center" class="modal-title" id="myModalInsupTopik">Topik Unggulan PT
                                </h4>

                                <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label for="ddlBidangUnggulan" class="col-sm-4 control-label">Bidang Unggulan</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList runat="server" ID="ddlBidangUnggulan" AppendDataBoundItems="true" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlBidangUnggulan_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="tbTopikUnggulanPt" class="col-sm-4 control-label">Topik Unggulan</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="tbTopikUnggulanPt" runat="server" CssClass="form-control input-sm"
                                                    placeholder="Topik Unggulan"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="rblStsAktif" class="col-sm-4 control-label">Sts Aktif</label>
                                            <div class="col-sm-5">
                                                <asp:RadioButtonList runat="server" ID="rblStsAktif" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="&nbsp;Aktif" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="&nbsp;Tidak aktif" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                                <asp:LinkButton ID="lbInsupTopikUnggulan" runat="server" CssClass="btn btn-info pull-right" Text="Simpan"
                                    OnClick="lbInsupTopikUnggulan_Click" OnClientClick="$('#btnClose').click();"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </section>
    </ContentTemplate>
</asp:UpdatePanel>
